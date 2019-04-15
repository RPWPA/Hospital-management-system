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

        public void WritePatient(Dictionary<string, Patient> p, Dictionary<string, Doctor> doc)
        {
            FileStream fs = new FileStream("patient.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);

            foreach (KeyValuePair<string, Patient> pa in p)
            {
                string doctors_ID = "";
                foreach (KeyValuePair<string, Doctor> d in doc)
                {
                    doctors_ID += d.Key + '&';
                }
                sw.WriteLine(p[pa.Key].get_Name() + '@' + p[pa.Key].get_Age() + '@' + p[pa.Key].get_id() + '@' + p[pa.Key].get_phone() + '@' + doctors_ID );
            }
            sw.Close();
        }
    }
    }
