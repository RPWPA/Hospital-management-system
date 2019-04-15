using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace OOP_Project
{
    class Read_Write
    {
        public void WriteRooms(List<Room> R)
        {
            FileStream fs = new FileStream("Rooms.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < R.Count; i++)
            {
                sw.WriteLine(R[i].capacity+'@'+R[i].avilableBeds+'@'+R[i].price);
          
            }
            sw.Close();
        }
        public void readRooms(List<Room> R)
        {
            string outputs;
            string[] output;
            FileStream fs = new FileStream("Rooms.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            while (sr.Peek() != -1)
            {
                outputs = sr.ReadLine();
                output = outputs.Split('@');
                string a= output[0];
                int b = int.Parse(a);
                if(b==4)
                {
                    Standard_room r = new Standard_room();
                    r.capacity = int.Parse(output[0]);
                    r.avilableBeds = int.Parse(output[1]);
                    r.price = float.Parse(output[2]);
                    R.Add(r);
                }
                else if(b==2)
                {
                    Semi_private_room r = new Semi_private_room();
                    r.capacity = int.Parse(output[0]);
                    r.avilableBeds = int.Parse(output[1]);
                    r.price = float.Parse(output[2]);
                    R.Add(r);
                }
                else if(b==1)
                {
                    Private_room r = new Private_room();
                    r.capacity = int.Parse(output[0]);
                    r.avilableBeds = int.Parse(output[1]);
                    r.price = float.Parse(output[2]);
                    R.Add(r);
                }
            }
            sr.Close();
        }



        public void write_doctor_data(Dictionary<string, Doctor> doc)
        {
            FileStream file = new FileStream("Doctor File.txt", FileMode.Create);
            StreamWriter write = new StreamWriter(file);
            try
            {

                Doctor[] D;
                string doctor = "", head = "";
                D = doc.Values.ToArray();
                for (int i = 0; i < D.Length; i++)
                {
                    doctor = "";
                    if (D[i].Is_head)
                        head = "true";
                    else
                        head = "false";

                    doctor = D[i].get_id() + "," + D[i].get_Name() + "," + D[i].get_Age() + "," + D[i].get_department() + "," + D[i].get_phone() + "," + head;
                    

                    write.WriteLine(doctor);
                }
            }
            finally
            {
                write.Close();
            }
        }
        public Dictionary<string, Doctor> read_doctor_data()
        {
            Dictionary<string, Doctor> doc = new Dictionary<string, Doctor>();

            FileStream filee = new FileStream("Doctor File.txt", FileMode.Open);
            StreamReader read = new StreamReader(filee);
            string record = "";
            string[] doctor_info;
            string[] doctor_personal_info;
            Doctor doctor;
            bool head;
            try
            {
                while (read.Peek() != -1)
                {
                    record = read.ReadLine();
                    doctor_info = record.Split('/');
                    if (doctor_info.Length == 0)
                    {
                        doctor_personal_info = record.Split(',');
                        if (doctor_personal_info[5] == "true") head = true;
                        else head = false;
                        doctor = new Doctor(doctor_personal_info[1], doctor_personal_info[2], doctor_personal_info[0], doctor_personal_info[4], doctor_personal_info[3], head);
                        doc.Add(doctor_personal_info[0], doctor);

                    }
                    doctor_personal_info = doctor_info[0].Split(',');

                    if (doctor_personal_info[5] == "true") head = true;
                    else head = false;
                    doctor = new Doctor(doctor_personal_info[1], doctor_personal_info[2], doctor_personal_info[0], doctor_personal_info[4], doctor_personal_info[3], head);
                    doc.Add(doctor_personal_info[0], doctor);

                }
                return doc;
            }
            finally { read.Close(); }
        }


        public void readdepartment(List<Department> dep)
        {
            FileStream fs = new FileStream("Departments.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (sr.Peek() != -1)
            {
                Department a = new Department(sr.ReadLine(), sr.ReadLine());
                dep.Add(a);
            }
            sr.Close();
        }
        public void writedepartment(List<Department> dep)
        {
            FileStream fs = new FileStream("Departments.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var a in dep)
            {
                sw.WriteLine(a.Getname());
                sw.WriteLine(a.Getnumber());
            }
            sw.Close();
        }

        public void WritePatient(Dictionary<string, Patient> p)
        {
            if (p.Count != 0)
            {
                FileStream fs = new FileStream("patient.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                try
                {
                    foreach (var pa in p)
                    {
                        string doctors_ID = "";
                        if (pa.Value.get_doctors().Count != 0)
                        {
                            var arr = pa.Value.get_doctors();

                            for (var i = 0; i < arr.Count - 1; i++)
                            {
                                doctors_ID += arr[i] + '/';
                            }
                            doctors_ID += arr[arr.Count - 1];
                        }
                        if (doctors_ID != "")
                        {
                            sw.WriteLine(pa.Value.get_Name() + '/' + pa.Value.get_Age() + '/' + pa.Key + '/' + pa.Value.get_phone() + '/' + pa.Value.get_department() + '/' + pa.Value.get_histroy() + '/' + pa.Value.get_room() + '/' + pa.Value.get_type() + '/' + doctors_ID);
                        }
                        else
                        {
                            sw.WriteLine(pa.Value.get_Name() + '/' + pa.Value.get_Age() + '/' + pa.Key + '/' + pa.Value.get_phone() + '/' + pa.Value.get_department() + '/' + pa.Value.get_histroy() + '/' + pa.Value.get_type() + '/' + pa.Value.get_room());
                        }
                    }
                } 
                finally
                {
                    sw.Close();
                }
            }
        }
        public Dictionary<string, Patient> readPatient()
        {
            Dictionary<string, Patient> p = new Dictionary<string, Patient>();
            FileStream fs = new FileStream("patient.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            try
            {
                string record = "";
                string[] arr;
                while(sr.Peek() != -1)
                {
                    record = sr.ReadLine();
                    arr = record.Split('/');
                    if(arr.Length == 7)
                    {
                        Patient pa = new Patient(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6],arr[7]);
                        p.Add(arr[2], pa);
                    }
                    else
                    {
                        Patient pa = new Patient(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
                        for (int i=7; i<arr.Length;i++)
                        {
                            pa.Add_Doctor(arr[i]);
                        }
                        p.Add(arr[2], pa);
                    }
                }

                return p;
            }
            finally
            {
                sr.Close();
            }
           
        }

        public void WriteNurse(Dictionary<string, Nurse> N)
        {
            if (N.Count != 0)
            {
                FileStream fs = new FileStream("Nurse.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                try
                {
                    foreach (var na in N)
                    {
                        string patients_ID = "";
                        if (na.Value.get_nurs_patients().Count != 0)
                        {
                            var arr = na.Value.get_nurs_patients();

                            for (var i = 0; i < arr.Count - 1; i++)
                            {
                                patients_ID += arr[i] + '/';
                            }
                            patients_ID += arr[arr.Count - 1];
                        }
                        if (patients_ID != "")
                        {
                            sw.WriteLine(na.Value.get_Name() + '/' + na.Value.get_Age() + '/' + na.Key + '/' + na.Value.get_phone() + '/' + na.Value.get_deparment() + '/' + na.Value.get_room()+ '/'  + patients_ID);
                        }
                        else
                        {
                            sw.WriteLine(na.Value.get_Name() + '/' + na.Value.get_Age() + '/' + na.Key + '/' + na.Value.get_phone() + '/' + na.Value.get_deparment() + '/' + na.Value.get_room() );

                        }
                    }
                }
                finally
                {
                    sw.Close();
                }

        }   }
        public Dictionary<string, Nurse> readNurse()
        {
            Dictionary<string, Nurse> N = new Dictionary<string, Nurse>();
            FileStream fs = new FileStream("Nurse.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            try
            {
                string record = "";
                string[] arr;
                while (sr.Peek() != -1)
                {
                    record = sr.ReadLine();
                    arr = record.Split('/');
                    if(arr.Length == 6)
                    {
                        Nurse na = new Nurse(arr[0], arr[1], arr[2], arr[3], arr[4]);
                        na.Add_room(arr[5]);
                        N.Add(na.get_id(), na);
                    }
                    else
                    {
                        Nurse na = new Nurse(arr[0], arr[1], arr[2], arr[3], arr[4]);
                        na.Add_room(arr[5]);
                        for(int i = 6; i<arr.Length;i++)
                        {
                            na.Add_patient(arr[i]);
                        }
                        N.Add(na.get_id(), na);
                    }
                }
                return N;
            }
            finally
            {
                sr.Close();
            }
        }

        public void Writeappo(List<Appointment> ap)
        {
            if (ap.Count != 0)
            {
                FileStream fs = new FileStream("Appointment.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                try
                {
                    foreach (var a in ap)
                    {
                        string d = a.get_date().ToString();
                        sw.WriteLine(d + '&' + a.get_id_doctor() + '&' + a.get_id_pateint());
                    }
                }
                finally
                {
                    sw.Close();
                }
            }
        }
        public List<Appointment> readappo()
        {
            List<Appointment> appo = new List<Appointment>();
            FileStream fs = new FileStream("Appointment.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            try
            {
               
                while(sr.Peek() != -1)
                {
                    string record = sr.ReadLine();
                    string[] arr = record.Split('&');
                    string[] time = arr[0].Split(' ');
                    string[] days = time[0].Split('/');
                    string[] hourse = time[1].Split(':');
                    int daa = int.Parse(days[2]);
                    DateTime date = new DateTime(int.Parse(days[2]), int.Parse(days[1]), int.Parse(days[0]), int.Parse(hourse[0]), int.Parse(hourse[1]), 0);
                    Appointment s = new Appointment(date, arr[2], arr[1]);
                    appo.Add(s);
                }
                return appo;
            }
            finally
            {
                sr.Close();
            }
        }

    }
}
