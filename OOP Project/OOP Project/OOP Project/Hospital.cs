using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OOP_Project
{

    abstract public class Person //Defining class Data members
    {
        private string Name;
        private string Age;
        private string ID;
        private string Phone_number;

        public Person() // Default Constructor
        {
            Name = "";
            Age = "";
            ID = "";
            Phone_number = "";
        }
        public Person(string Name, string Age, string ID, string Phone_number) // Parametric Constructor
        {
            this.Name = Name;
            this.Age = Age;
            this.ID = ID;
            this.Phone_number = Phone_number;
        }
        public string get_id() { return ID; }
        public void set_id(string id) { ID = id; }

        public string get_Name() { return Name; }
        public void set_Name(string name) { Name = name; }

        public string get_Age() { return Age; }
        public void set_Age(string age) { Age = age; }

        public string get_phone() { return Phone_number; }
        public void set_phone(string phone) { Phone_number = phone; }

        // public abstract void  Display_Info(); // pure virtual function (you must create same function in each sub class 
    }
    // Department class
    public class Department
    {
        // Department data members
        private string Department_name;
        private string Department_number;
        public string Getname()
        {
            return Department_name;
        }
        public string Getnumber()
        {
            return Department_number;
        }
        public Department() // default constructor
        {
            Department_name = "";
            Department_number = "";
        }
        public Department(string Department_name, string Department_number) // parametric constructor
        {
            this.Department_name = Department_name;
            this.Department_number = Department_number;
        }
    } 
    // Patients classes
    public class Patient : Person
    {
       string history;
        string room;
        List<string> Patient_Doctors;
        private string Department;
        public void set_department(string dep) { Department = dep; }
        public string get_department() { return Department; }
        public void Add_Doctor(string doctor_id) // adds Doctor to Patient vector
        {
            this.Patient_Doctors.Add(doctor_id);
        }
        public Patient() // default Constructor of patient
        {
            this.Patient_Doctors = new List<string>();
        }
        // parametric constructor
        public Patient(string Name, string Age, string ID, string Phone_number,string dep , string history ,string room)
            : base(Name, Age, ID, Phone_number)
        {
            this.Patient_Doctors = new List<string>();
            this.Department = dep;
            this.history = history;
            this.room = room;
        }
        public string get_room() { return room; }

    }


   
    // Doctor class
    public class Doctor : Person
    {
        public List<string> Doctor_Patients; // data members
        private string Doctor_Department;

        public bool Is_head;

        // Inherited Constructor 
        public Doctor(string Name, string Age, string ID, string Phone_number, string Doctor_Department, bool Is_head)
            : base(Name, Age, ID, Phone_number)
        {

            this.Doctor_Department = Doctor_Department;
            this.Doctor_Patients = new List<string>();
            this.Is_head = Is_head;
        }

        public string get_department() { return Doctor_Department; }
        public void set_department(string department) { this.Doctor_Department = department; }
        public Doctor() // default constructor 
        {
            Doctor_Patients = null;
            Doctor_Department = null;
            Is_head = false;
        }
        public void Add_Patient(string patient) // adds new patient to the doctor 
        {
            this.Doctor_Patients.Add(patient);
        }
    

    }
    // Nurse class
    public class Nurse : Person
    {
        private string Nurse_department;
        private List<string> Nurse_rooms;

        public List<string> Nurse_patients;
       
        // Default Constructor
        public Nurse()
        {
            Nurse_department = null;
            this.Nurse_rooms = new List<string>();
            Nurse_patients = new List<string>(); 


        }

        public List<string> get_nurs_patients() { return this.Nurse_patients; }
        public string get_deparment() { return Nurse_department; }
        // Nurse Parametric Constructor
        public Nurse(string Name, string Age, string ID, string Phone_number, string Nurse_department)
            : base(Name, Age, ID, Phone_number)
        {
            this.Nurse_department = Nurse_department;
            this.Nurse_rooms = new List<string>();
          
            Nurse_patients = new List<string>();

        }
        // add standard room
        public void Add_room(string Room)
        {
            this.Nurse_rooms.Add(Room);
        }
         public string get_room()
        {
            return this.Nurse_rooms[0];
        }
 
        public void Add_patient(string ID)
        {
            this.Nurse_patients.Add(ID);
        }
    }
    // Room classes ABSTRACT 
    public abstract class Room
    {
        public int capacity;
        public int avilableBeds;
        public float price;
        

        public Room() // Constructor
        {
          
            capacity = 0;
            price = 0;
            avilableBeds = 0;
        }
        public abstract List<Nurse> get_Nurses();
        public abstract bool Check_full(); // Abstract Function
    }

    public class Standard_room : Room
    {
        string Standard_ward;
        private List<Nurse> room_nurses;
        public Standard_room() : base()
        {
            this.room_nurses = null;
            capacity = 4;
            price = 500;
            avilableBeds = 4;
            Standard_ward = "Standard ward";
        }
        public Standard_room(List<Nurse> standard_nurses) : base()
        {
            this.room_nurses = standard_nurses;
            capacity = 4;
            price = 500;
            avilableBeds = 4;
            Standard_ward = "Standard ward";
        }
        public string get_type() { return Standard_ward; }
        public override List<Nurse> get_Nurses() { return room_nurses; }
        public int get_capacity() { return capacity; }
        public void set_Nurses(List<Nurse> N) { this.room_nurses = N; }

        // adds nurse to room
        public override bool Check_full()
        {
            if (avilableBeds > 0)
            {
                avilableBeds--;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Semi_private_room : Room
    {
        string Semi_Private;
        private List<Nurse> room_nurses;
        public Semi_private_room() : base()
        {
            this.room_nurses = null;
            capacity = 2;
            price = 1000;
            avilableBeds = 2;
            Semi_Private = "Semi Private";
        }
        public Semi_private_room(List<Nurse> semi_nurses) :base()
        {
            this.room_nurses = semi_nurses;
            capacity = 2;
            price = 1000;
            avilableBeds = 2;
            Semi_Private = "Semi Private";
        }
        public string get_type() { return Semi_Private; }
        public int get_capacity() { return capacity; }
        public void set_Nurses(List<Nurse> N) { this.room_nurses = N; }

        public override List<Nurse> get_Nurses() { return room_nurses; }
        // adds nurse to room
        public override bool Check_full()
        {
            if (avilableBeds > 0)
            {
                avilableBeds--;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Private_room : Room
    {
        string Private;
        public Private_room() : base()
        {
            this.room_nurses = null;
            capacity = 1;
            price = 2000;
            avilableBeds = 1;
            Private = "Private";
        }
        private List<Nurse> room_nurses;

        // para constructor

        public Private_room(List<Nurse> private_nurses) : base()
        {
            this.room_nurses = private_nurses;
            capacity = 1;
            price = 2000;
            avilableBeds = 1;
            Private = "Private";
        }
        public int get_capacity () { return capacity; }
        public string get_type() { return Private; }
        public void set_Nurses(List<Nurse> N) {  this.room_nurses = N; }
        public override List<Nurse> get_Nurses() { return room_nurses; }

        public override bool Check_full()
        {
            if (avilableBeds > 0)
            {
                avilableBeds--;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    // Appointment class


    public class Appointment
    {

        public DateTime Datetime; // date of appointment   
        string id_pateint;
        string id_doctor;
    
        public Appointment()
        {
            this.Datetime = new DateTime();

        }
        public string get_id_pateint() { return id_pateint; }
        public void set_id_pateint(string id) { id_pateint = id; }
        public string get_id_doctor() { return id_doctor; }
        public void set_id_doctor(string id) { id_doctor = id; }
      





        // Parametric Constructor
        public Appointment(DateTime Datetime, string id_pateint, string id_doctor)
        {
            this.id_doctor = id_doctor;
          
            this.Datetime = Datetime;
            this.id_pateint = id_pateint;


        }
    }

}
    

