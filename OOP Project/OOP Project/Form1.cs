using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Project
{
    public partial class Form1 : Form
    {
        public static Standard_room sr = new Standard_room();
        public static Semi_private_room spr = new Semi_private_room();
        public static Private_room pr = new Private_room();
        public static Dictionary<string, Doctor> doc = new Dictionary<string, Doctor>();// static object doctor
        public static Dictionary<string, Doctor> head_doctors = new Dictionary<string, Doctor>();// static object head doctors
        public static Dictionary<string, Nurse> nurse = new Dictionary<string, Nurse>();// static object nurse
        public static Dictionary<string, Patient> pat = new Dictionary<string, Patient>();// static object Patient
        public static List<Department> dplist = new List<Department>(); // static list of departments.
        Read_Write swer = new Read_Write(); // readclass
        public static List<Appointment> appo = new List<Appointment>();
        public List<Room> Rooms = new List<Room>();


        //constructor form
        public Form1()
        {
            InitializeComponent();
            Rooms.Add(sr);
            Rooms.Add(spr);
            Rooms.Add(pr);
            swer.readRooms(Rooms);
            swer.readdepartment(dplist);   // reading from file.
            appo = swer.readappo();
            pat = swer.readPatient();
            nurse = swer.readNurse();
            doc = swer.read_doctor_data();

        }

        private void AdminButton_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void ExitButt_Click(object sender, EventArgs e)
        {
            if (doc != null)
            {
                swer.write_doctor_data(doc);
            }
            swer.WriteRooms(Rooms);
            swer.WritePatient(pat);
            swer.writedepartment(dplist);         // Department Panel Save.
            swer.WriteNurse(nurse);
            swer.Writeappo(appo);
            this.Close();
        }

        private void AddRoomButt_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddDoctorButt_Click(object sender, EventArgs e)
        {
            if (dplist.Count == 0)
            {
                MessageBox.Show("Please Enter a department first!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            comboBox_doctor_department.Items.Clear();
            foreach (var dep in dplist)
            {
                comboBox_doctor_department.Items.Add(dep.Getname());
            }
            Doctor_Panel.Visible = true;
            add_doctor_panel.Visible = true;
            panel2.Visible = false;
        }

        private void save_doc_data_Click(object sender, EventArgs e)
        {
            //check if all fields are fill
            bool done = true;
            if (text_doc_id.Text == "" || text_doc_age.Text == "" || text_doc_name.Text == "" || text_doc_phone.Text == "" || comboBox_doctor_department.Text == "")
            {
                MessageBox.Show(" pleas fill all fields", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //check if id is uniqe 
                foreach (KeyValuePair<string, Doctor> pair in doc)
                {
                    if (text_doc_id.Text == pair.Key)
                    {
                        MessageBox.Show("ID is existed chose another ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        done = false;
                    }
                }
                // head is a boolean to check if doctor is a head or not
                bool head = true;
                bool ok = true;
                if (yes_a_head.Checked == true)
                {
                    foreach (KeyValuePair<string, Doctor> search in doc)
                    {
                        if (search.Value.Is_head == true && search.Value.get_department() == comboBox_doctor_department.Text)
                        {
                            MessageBox.Show("there is a head for this department", "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ok = false;
                            done = false;
                            break;
                        }
                    }
                    if (ok)
                    {
                        head = true;
                    }

                }
                else if (no_a_head.Checked == true)
                {
                    head = false;
                }
                else
                {
                    done = false;
                    MessageBox.Show("chose if doctor a head or not !", "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                // check if all data are right
                if (done)
                {
                    Doctor D = new Doctor(text_doc_name.Text, text_doc_age.Text, text_doc_id.Text, text_doc_phone.Text, comboBox_doctor_department.Text, head);
                    doc.Add(text_doc_id.Text, D);
                    text_doc_name.Text = "";
                    text_doc_id.Text = "";
                    text_doc_age.Text = "";
                    text_doc_phone.Text = "";
                    comboBox_doctor_department.Text = "";
                }

            }
        }

        private void back_to_panel1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            Doctor_Panel.Visible = false;
            add_doctor_panel.Visible = false;
            text_doc_name.Text = "";
            text_doc_id.Text = "";
            text_doc_age.Text = "";
            text_doc_phone.Text = "";
            comboBox_doctor_department.Items.Clear();
        }

        private void EditDoctorButt_Click(object sender, EventArgs e)
        {
            update_doc_panel.Visible = true;
            Doctor_Panel.Visible = true;
            panel2.Visible = false;
        }
        bool done_update = false;

        private void accept_doc_id_to_update_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Doctor> search in doc)
            {
                if (search.Key == text_update_doc_id.Text)
                {
                    text_update_doc_name.Text = search.Value.get_Name();
                    text_update_doc_phone.Text = search.Value.get_phone();
                    text_update_doc_age.Text = search.Value.get_Age();
                    done_update = true;
                    break;
                }
            }
            if (done_update == false)
            {
                MessageBox.Show("this id is not found!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Update_doc_data_Click(object sender, EventArgs e)
        {
            if (done_update)
            {
                foreach (KeyValuePair<string, Doctor> search in doc)
                {

                    if (search.Key == text_update_doc_id.Text)
                    {
                        search.Value.set_Name(text_update_doc_name.Text);
                        search.Value.set_phone(text_update_doc_phone.Text);
                        search.Value.set_Age(text_update_doc_age.Text);

                        break;
                    }
                }

                text_update_doc_name.Text = "";
                text_update_doc_id.Text = "";
                text_update_doc_age.Text = "";
                text_update_doc_phone.Text = "";
            }
        }

        private void Back_to_ubdate_panel_Click(object sender, EventArgs e)
        {
            update_doc_panel.Visible = false;
            Doctor_Panel.Visible = false;
            panel2.Visible = true;
            text_update_doc_name.Text = "";
            text_update_doc_id.Text = "";
            text_update_doc_age.Text = "";
            text_update_doc_phone.Text = "";
        }

        private void DisplayDoctorButt_Click(object sender, EventArgs e)
        {
            panel_to_display_doctor.Visible = true;
            Doctor_Panel.Visible = true;
            panel2.Visible = false;
        }

        bool doc_id_ok = false;
        private void Display_doc_btn_Click(object sender, EventArgs e)
        {
            ListViewItem vb;
            listview_display_doctor.Scrollable = true;
            bool done = false;
            foreach (KeyValuePair<string, Doctor> search in doc)
            {
                if (search.Key == text_display_doctor.Text)
                {
                    vb = new ListViewItem(search.Value.get_Name());
                    vb.SubItems.Add(search.Value.get_id());
                    vb.SubItems.Add(search.Value.get_Age());
                    vb.SubItems.Add(search.Value.get_phone());
                    vb.SubItems.Add(search.Value.get_department());
                    listview_display_doctor.Items.Add(vb);
                    done = true;
                    doc_id_ok = true;
                }
            }
            if (!done)
            {
                MessageBox.Show("There is no doctor with that id", "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                text_display_doctor.Text = "";
            }
        }

        private void display_all_doctor_btn_Click(object sender, EventArgs e)
        {
            ListViewItem ls;
            foreach (var search in doc)
            {
                ls = new ListViewItem(search.Value.get_Name());
                ls.SubItems.Add(search.Key);
                ls.SubItems.Add(search.Value.get_Age());
                ls.SubItems.Add(search.Value.get_phone());
                ls.SubItems.Add(search.Value.get_department());
                listView_display_all_doctors.Items.Add(ls);
            }
            display_all_doctors_panel.Visible = true;
        }

        private void back_from_display_all_doc_panel_Click(object sender, EventArgs e)
        {
            display_all_doctors_panel.Visible = false;
            listView_display_all_doctors.Items.Clear();
        }

        private void back_display_btn_Click(object sender, EventArgs e)
        {
            listview_display_doctor.Items.Clear();
            Doctor_Panel.Visible = false;
            panel2.Visible = true;
            panel_to_display_doctor.Visible = false;
            text_display_doctor.Text = "";

        }

        public static bool deleted = false;
        private void button_to_Add_doc_del_Click(object sender, EventArgs e)
        {
            listview_deleted_doc.Scrollable = true;
            bool done = false;
            ListViewItem vb;
            if (text_doc_ID_delete.Text == "")
            {
                MessageBox.Show("Please enter doctor id!");

            }
            else
            {
                foreach (KeyValuePair<string, Doctor> search in doc)
                {
                    if (search.Key == text_doc_ID_delete.Text)
                    {
                        vb = new ListViewItem(search.Value.get_Name());
                        vb.SubItems.Add(search.Value.get_id());
                        vb.SubItems.Add(search.Value.get_Age());
                        vb.SubItems.Add(search.Value.get_phone());
                        vb.SubItems.Add(search.Value.get_department());
                        listview_deleted_doc.Items.Add(vb);
                        done = true;
                        deleted = true;
                    }
                }
                if (!done)
                    MessageBox.Show("Sorry there is no doctor with that id");
            }
        }

        private void back_to_doc_panel_Click(object sender, EventArgs e)
        {
            text_doc_ID_delete.Text = "";
            listview_deleted_doc.Items.Clear();
            delet_doctor_panel.Visible = false;
            Doctor_Panel.Visible = false;
            panel2.Visible = true;
        }

        private void DeleteDoctorButt_Click(object sender, EventArgs e)
        {
            delet_doctor_panel.Visible = true;
            Doctor_Panel.Visible = true;
            panel2.Visible = false;
        }

        private void Delete_doc_btn_Click(object sender, EventArgs e)
        {
            if (deleted)
            {
                string[] key;
                string[] patient_key;
                int count = 0;
                foreach (KeyValuePair<string, Doctor> search in doc)
                {
                    if (search.Key == text_doc_ID_delete.Text)
                    {
                        if (search.Value.Doctor_Patients != null)
                        {
                            if (doc.Count != 1)
                            {
                                // make deleted doctor patients assign to another doctor
                                patient_key = search.Value.Doctor_Patients.ToArray();
                              
                                count = patient_key.Length;
                                key = doc.Keys.ToArray();
                                for (int i = 0; i < 5; i++)
                                {
                                    if (key[i] != text_doc_ID_delete.Text)
                                    {
                                        for (int j = 0; j < count; j++)
                                        {
                                            doc[key[i]].Doctor_Patients.Add(patient_key[j]);
                                        }
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                search.Value.Doctor_Patients.Clear();
                                //// patients should deleted from static patients list
                            }
                        }
                        doc.Remove(search.Key);
                        text_doc_ID_delete.Text = "";
                        break;

                    }

                }
                text_doc_ID_delete.Text = "";
            }
        }

        private void done_head_btn_Click(object sender, EventArgs e)
        {
           
            foreach (var search in doc)
            {
                if (search.Value.get_department() == comboBox_head_department.Text)
                {
                    search.Value.set_Age(text_head_age.Text);
                    search.Value.set_Name(text_head_name.Text);
                    search.Value.set_phone(text_head_phone.Text);
                    text_head_name.Text = "";
                    text_head_age.Text = "";
                    text_head_id.Text = "";
                    text_head_phone.Text = "";
                    comboBox_head_department.Text = "";
                  
                    break;
                }
            }
            
        }

        private void back_from_head_panel_btn_Click(object sender, EventArgs e)
        {
            head_doc_panel.Visible = false;
            Doctor_Panel.Visible = false;
            panel2.Visible = true;
            text_head_name.Text = "";
            text_head_age.Text = "";
            text_head_id.Text = "";
            text_head_phone.Text = "";
            comboBox_head_department.Text = "";
        }

        private void AddHeadButt_Click(object sender, EventArgs e)
        {
            if (dplist.Count == 0)
            {
                MessageBox.Show("Please Enter a department first!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            comboBox_head_department.Items.Clear();
            foreach (var dep in dplist)
            {
                comboBox_head_department.Items.Add(dep.Getname());
            }
            head_doc_panel.Visible = true;
            panel2.Visible = false;
            Doctor_Panel.Visible = true;
        }

        private void AddNurseButt_Click(object sender, EventArgs e)
        {
            if (dplist.Count == 0)
            {
                MessageBox.Show("Please Enter a department first!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            comboBox_nurse_department.Items.Clear();
            foreach (var dep in dplist)
            {
                comboBox_nurse_department.Items.Add(dep.Getname());
            }
            panel2.Visible = false;
            nurse_panel.Visible = true;
            add_nurse_panel.Visible = true;
        }

        private void back_from_addnurse_panel_Click(object sender, EventArgs e)
        {
            add_nurse_panel.Visible = false;
            nurse_age_text.Text = "";
            nurse_ID_text.Text = "";
            nurse_name_text.Text = "";
            nurse_phone_text.Text = "";
            comboBox_nurse_department.Text = "";
            nurse_panel.Visible = false;
            panel2.Visible = true;
        }

        private void save_nurse_data_Click(object sender, EventArgs e)
        {
            if (nurse_name_text.Text == "" || nurse_age_text.Text == "" || comboBox_pat_room.Text == ""|| nurse_ID_text.Text == "" || nurse_phone_text.Text == "" || comboBox_nurse_department.Text == "")
            {
                MessageBox.Show("pleas fill all data", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                bool done = true;
                foreach (KeyValuePair<string, Nurse> search in nurse)
                {
                    if (search.Key == nurse_ID_text.Text)
                    {
                        MessageBox.Show("this Id is existed chose anther one", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        done = false;
                    }
                }
                if (done)
                {
                    Nurse N = new Nurse(nurse_name_text.Text, nurse_age_text.Text, nurse_ID_text.Text, nurse_phone_text.Text, comboBox_nurse_department.Text);
                    string s = comboBox_pat_room.Text;
                    N.Add_room(s);

                    nurse.Add(nurse_ID_text.Text, N);
                    nurse_age_text.Text = "";
                    nurse_ID_text.Text = "";
                    nurse_name_text.Text = "";
                    nurse_phone_text.Text = "";
                    comboBox_nurse_department.Text = "";
                    comboBox_pat_room.Text = "";
                }
            }

        }

        private void DeleteNurseButt_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            nurse_panel.Visible = true;
            delete_nurse_panel.Visible = true;
        }

        private void delete_a_nurse_btn_Click(object sender, EventArgs e)
        {
            if (text_delete_nurse_id.Text == "")
            {
                MessageBox.Show("Enter nurse id", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

                string[] N;
                string[] patient_key;

                int count = 0;
                ListViewItem vb;
                bool done = false;
                foreach (KeyValuePair<string, Nurse> search in nurse)
                {
                    if (text_delete_nurse_id.Text == search.Key)
                    {
                        done = true;
                        vb = new ListViewItem(search.Value.get_Name());
                        vb.SubItems.Add(search.Value.get_id());
                        vb.SubItems.Add(search.Value.get_Age());
                        vb.SubItems.Add(search.Value.get_phone());
                        vb.SubItems.Add(search.Value.get_deparment());
                        listView_delete_nurse.Items.Add(vb);
                        if (search.Value.get_nurs_patients() != null)
                        {

                            // make deleted nurse patients assign to another nurse
                            patient_key = search.Value.get_nurs_patients().ToArray();

                            count = patient_key.Length;
                            N = nurse.Keys.ToArray();
                            int Nlen = N.Length;
                            for (int i = 0; i < Nlen; i++)
                            {
                                if (N[i] != text_delete_nurse_id.Text)
                                {
                                    for (int j = 0; j < count; j++)
                                    {
                                        nurse[N[i]].Nurse_patients.Add(patient_key[j]);

                                    }
                                    break;
                                }
                            }

                        }
                        nurse.Remove(search.Key);
                        break;
                    }
                }

                text_delete_nurse_id.Text = "";
                if (!done)
                {
                    MessageBox.Show("there is no nurse with that id", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
        }

        private void back_from_delete_nurse_btn_Click(object sender, EventArgs e)
        {
            delete_nurse_panel.Visible = false;
            listView_delete_nurse.Items.Clear();
            text_delete_nurse_id.Text = "";
            nurse_panel.Visible = false;
            panel2.Visible = true;
        }

        private void radioButton_display_nurse_CheckedChanged(object sender, EventArgs e)
        {
            listView_display_all_nurses.Items.Clear();
            if (radioButton_display_nurse.Checked)
            {
                listView_display_a_nurse.Scrollable = true;
                display_a_nurse_panel.Visible = true;
                display_all_nurses_panel.Visible = false;

            }
        }

        private void radioButton_all_nurses_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_all_nurses.Checked)
            {
                listView_display_all_nurses.Scrollable = true;
                display_all_nurses_panel.Visible = true;
                display_a_nurse_panel.Visible = false;
                ListViewItem box;
                foreach (KeyValuePair<string, Nurse> Nurses in nurse)
                {
                    box = new ListViewItem(Nurses.Value.get_Name());
                    box.SubItems.Add(Nurses.Key);
                    box.SubItems.Add(Nurses.Value.get_Age());
                    box.SubItems.Add(Nurses.Value.get_deparment());
                    box.SubItems.Add(Nurses.Value.get_phone());
                    box.SubItems.Add(Nurses.Value.get_room());

                    listView_display_all_nurses.Items.Add(box);

                }
            }
        }

        private void display_a_nurse_button_Click(object sender, EventArgs e)
        {
            if (text_display_a_nurse_ID.Text == "")
            {
                MessageBox.Show("pleas enter nurse's id", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                bool done = false;
                ListViewItem box;
                listView_display_a_nurse.Items.Clear();
                foreach (KeyValuePair<string, Nurse> search in nurse)
                {
                    if (text_display_a_nurse_ID.Text == search.Key)
                    {
                        box = new ListViewItem(search.Value.get_Name());
                        box.SubItems.Add(search.Key);
                        box.SubItems.Add(search.Value.get_Age());
                        box.SubItems.Add(search.Value.get_deparment());
                        box.SubItems.Add(search.Value.get_phone());
                        box.SubItems.Add(search.Value.get_room());
                        listView_display_a_nurse.Items.Add(box);
                        done = true;
                    }
                }
                if (!done)
                {
                    MessageBox.Show("There is no nurse with that id", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (text_display_a_nurse_ID.Text == "")
            {
                MessageBox.Show("pleas enter nurse's id", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                bool done = false;
                ListViewItem box;
                List<string> P;
                foreach (KeyValuePair<string, Nurse> search in nurse)
                {
                    if (text_display_a_nurse_ID.Text == search.Key)
                    {
                        if (search.Value.get_nurs_patients() == null)
                        {
                            MessageBox.Show("there is no patients for that nurse", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                        else
                        {
                            ///////////////////////////////////////////////////////////////////////////////
                            listView_display_a_nurse.Items.Clear();
                            P = search.Value.get_nurs_patients();
                            if (P.Count == 0)
                            {
                                MessageBox.Show("there is no patients !", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            for (int i = 0; i < P.Count; i++)
                            {
                                box = new ListViewItem(pat[P[i]].get_Name());
                                box.SubItems.Add(pat[P[i]].get_id());
                                box.SubItems.Add(pat[P[i]].get_Age());
                                box.SubItems.Add(pat[P[i]].get_department());
                                box.SubItems.Add(pat[P[i]].get_phone());
                                box.SubItems.Add(pat[P[i]].get_room());

                                listView_display_a_nurse.Items.Add(box);

                            }
                        }
                        done = true;
                    }
                }
                if (!done)
                {
                    MessageBox.Show("There is no nurse with that id", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void back_from_display_nurse_panel_Click(object sender, EventArgs e)
        {
            display_nurse_panel.Visible = false;
            display_all_nurses_panel.Visible = false;
            display_a_nurse_panel.Visible = false;
            listView_display_all_nurses.Items.Clear();
            listView_display_a_nurse.Items.Clear();
            radioButton_all_nurses.Checked = false;
            radioButton_display_nurse.Checked = false;
            nurse_panel.Visible = false;
            panel2.Visible = true;
            text_display_a_nurse_ID.Text = "";
        }

        private void DisplayNurseButt_Click(object sender, EventArgs e)
        {
            display_nurse_panel.Visible = true;
            nurse_panel.Visible = true;
            panel2.Visible = false;
        }

        private void EditNurseButt_Click(object sender, EventArgs e)
        {
            update_nurse_panel.Visible = true;
            nurse_panel.Visible = true;
            panel2.Visible = false;
        }

        private void update_nurse_id_btn_Click(object sender, EventArgs e)
        {
            if (text_update_nuse_ID.Text == "")
            {
                MessageBox.Show("Pleas enter nurse id", "waning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                bool done = false;
                foreach (KeyValuePair<string, Nurse> search in nurse)
                {
                    if (search.Key == text_update_nuse_ID.Text)
                    {
                        text_update_nuse_name.Text = search.Value.get_Name();
                        text_update_nuse_phone.Text = search.Value.get_phone();
                        text_update_nuse_age.Text = search.Value.get_Age();
                        done = true;
                        break;
                    }
                }
                if (!done)
                {
                    MessageBox.Show("there is no nurse with that id", "waning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void back_from_update_nurse_panel_Click(object sender, EventArgs e)
        {
            update_nurse_panel.Visible = false;
            nurse_panel.Visible = false;
            panel2.Visible = true;
        }

        private void update_nurse_data_btn_Click(object sender, EventArgs e)
        {
            if (text_update_nuse_ID.Text == "" || text_update_nuse_name.Text == "" || text_update_nuse_age.Text == "" || text_update_nuse_phone.Text == "")
            {
                MessageBox.Show("please fill all data", "waning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                foreach (KeyValuePair<string, Nurse> search in nurse)
                {
                    if (text_update_nuse_ID.Text == search.Key)
                    {
                        search.Value.set_Age(text_update_nuse_age.Text);
                        search.Value.set_Name(text_update_nuse_name.Text);
                        search.Value.set_phone(text_update_nuse_phone.Text);
                        break;
                    }
                }
                text_update_nuse_phone.Text = "";
                text_update_nuse_name.Text = "";
                text_update_nuse_ID.Text = "";
                text_update_nuse_age.Text = "";
            }
        }

        private void AddDepartmentButt_Click(object sender, EventArgs e)
        {
            Add.Visible = true;
            Department.Visible = true;
            panel2.Visible = false;
        }

        private void add_save_Click(object sender, EventArgs e)
        {
            if (add_depname.Text == "" || add_depnum.Text == "")
            {
                MessageBox.Show("Please Enter All data");
            }
            else
            {
                if (dplist.Count() == 0)
                {// Checking if the list is empty.
                    Department a;
                    a = new Department(add_depname.Text, add_depnum.Text);
                    dplist.Add(a);
                    MessageBox.Show("Added Successfully.");
                }
                else
                {
                    // checking if the department name or number is taken/exists already.
                    bool sa = true;
                    foreach (var b in dplist)
                    {
                        if (add_depname.Text == b.Getname() || add_depnum.Text == b.Getnumber())
                        {
                            sa = false;
                            break;
                            //Found the name or the number.
                        }
                    }
                    if (sa == true)
                    {
                        Department a;
                        a = new Department(add_depname.Text, add_depnum.Text);
                        dplist.Add(a);
                        MessageBox.Show("Added Successfully.");
                        add_depname.Clear();
                        add_depnum.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Department Already Exists.");
                    }
                }
            }
        }

        private void add_back_Click(object sender, EventArgs e)
        {
            add_depname.Clear();
            add_depnum.Clear();
            Department.Visible = false;
            Add.Visible = false;
            panel2.Visible = true;
        }

        private void EditDepartmentButt_Click(object sender, EventArgs e)
        {
            Edit.Visible = true;
            Department.Visible = true;
            panel2.Visible = false;
            if (dplist.Count() == 0)
            {
                MessageBox.Show("There exists no departments.");
                edit_combobox.Items.Clear();
            }
            else
            {
                edit_combobox.Items.Clear();
                foreach (var a in dplist)
                {
                    if (!edit_combobox.Items.Contains(a.Getname()))
                    {
                        edit_combobox.Items.Add(a.Getname());
                    }
                }
            }
        }

        private void edit_save_Click(object sender, EventArgs e)
        {
            bool newdata = true;
            foreach (var b in dplist)
            {
                if (edit_textbox1.Text == b.Getname() || edit_textbox2.Text == b.Getnumber())
                {
                    newdata = false;
                    //entering the same data.
                }
            }
            if (newdata == false)
            {
                MessageBox.Show("Please enter new data if you want to edit.");
            }
            else
            {
               
                if (pat.Count() != 0)
                {
                    var pa = pat.Keys.ToArray();
                    foreach (var bc in pa)
                    {// deleting the patients inside of it.
                        if (pat[bc].get_department() == edit_combobox.Text)
                        {
                            pat.Remove(bc);
                        }
                    }
                }
                if (doc.Count() != 0)
                {
                    var pa = doc.Keys.ToArray();
                    foreach (var bc in pa)
                    {// deleting the patients inside of it.
                        if (doc[bc].get_department() == edit_combobox.Text)
                        {
                            doc.Remove(bc);
                        }
                    }
                }
                if (nurse.Count() != 0)
                {
                    var pa = nurse.Keys.ToArray();
                    foreach (var bc in pa)
                    {// deleting the patients inside of it.
                        if (nurse[bc].get_deparment() == edit_combobox.Text)
                        {
                            nurse.Remove(bc);
                        }
                    }
                }
                if (dplist.Count() != 0)
                {
                    foreach (var bc in dplist)
                    {// deleting the department.
                        if (bc.Getname() == edit_combobox.Text)
                        {
                            dplist.Remove(bc);
                            break;
                        }
                    }
                }
                if (edit_textbox1.Text == "" || edit_textbox2.Text == "")
                {
                    MessageBox.Show("Please Enter All data");
                }
                else
                {
                    if (dplist.Count() == 0)
                    {// Checking if the list is empty.
                        Department a;
                        a = new Department(edit_textbox1.Text, edit_textbox2.Text);
                        dplist.Add(a);
                        MessageBox.Show("Saved Successfully.");
                    }
                    else
                    {
                        // checking if the department name or number is taken/exists already.
                        bool sa = true;
                        foreach (var b in dplist)
                        {
                            if (edit_textbox1.Text == b.Getname() || edit_textbox2.Text == b.Getnumber())
                            {
                                sa = false;
                                break;
                                //Found the name or the number.
                            }
                        }
                        if (sa == true)
                        {
                            Department a;
                            a = new Department(edit_textbox1.Text, edit_textbox2.Text);
                            dplist.Add(a);
                            MessageBox.Show("Saved Successfully.");
                            edit_textbox1.Clear();
                            edit_textbox2.Clear();
                            edit_combobox.Items.Clear();
                            edit_combobox.Text = null;
                            if (dplist.Count() == 0)
                            {

                                edit_combobox.Items.Clear();
                            }
                            else
                            {
                                edit_combobox.Items.Clear();
                                foreach (var asc in dplist)
                                {
                                    if (!edit_combobox.Items.Contains(asc.Getname()))
                                    {
                                        edit_combobox.Items.Add(asc.Getname());
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Department Already Exists.");
                            sa = true;
                        }
                    }
                }
            }
        }

        private void edit_back_Click(object sender, EventArgs e)
        {
            edit_combobox.Items.Clear();
            edit_textbox1.Clear();
            edit_textbox2.Clear();
            Department.Visible = false;
            Edit.Visible = false;
            panel2.Visible = true;
        }
        // Edit Panel END.

        // Delete Panel START.
        private void button3_Click(object sender, EventArgs e)
        {
            Delete.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (nurse.Count() != 0)
            {
                var pa = nurse.Keys.ToArray();
                foreach (var bc in pa)
                {// deleting the patients inside of it.
                    if (nurse[bc].get_deparment() == delete_combobox.Text)
                    {
                        nurse.Remove(bc);
                    }
                }
            }
            if (pat.Count() != 0)
            {
                var pa = pat.Keys.ToArray();
                foreach (var bc in pa)
                {// deleting the patients inside of it.
                    if (pat[bc].get_department() == delete_combobox.Text)
                    {
                        pat.Remove(bc);
                    }
                }
            }
            if (doc.Count() != 0)
            {
                var pa = doc.Keys.ToArray();
                foreach (var bc in pa)
                {// deleting the patients inside of it.
                    if (doc[bc].get_department() == delete_combobox.Text)
                    {
                        doc.Remove(bc);
                    }
                }
            }
            foreach (var bc in dplist)
            {// deleting the list.
                if (bc.Getname() == delete_combobox.Text)
                {
                    dplist.Remove(bc);
                    MessageBox.Show("Deleted Successfully.");
                    delete_combobox.Items.Clear();
                    delete_combobox.Text = null;
                    if (dplist.Count() == 0)
                    {
                        delete_combobox.Items.Clear();
                    }
                    else
                    {
                        delete_combobox.Items.Clear();
                        foreach (var a in dplist)
                        {
                            if (!delete_combobox.Items.Contains(a.Getname()))
                            {
                                delete_combobox.Items.Add(a.Getname());
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void DisplayDepartmentButt_Click(object sender, EventArgs e)
        {
            if (dplist.Count() == 0)
            {
                MessageBox.Show("There exists no departments.");
                display_combox.Items.Clear();
            }
            else
            {
                display_combox.Items.Clear();
                foreach (var a in dplist)
                {
                    if (!display_combox.Items.Contains(a.Getname()))
                    {
                        display_combox.Items.Add(a.Getname());
                    }
                }
            }
            Display.Visible = true;
            Department.Visible = true;
            panel2.Visible = false;
        }

        private void display_return_Click(object sender, EventArgs e)
        {
            display_combox.Text = null;
            view_department.Items.Clear();
            view_doctors.Items.Clear();
            view_patients.Items.Clear();
            display_combox.Items.Clear();
            Department.Visible = false;
            Display.Visible = false;
            panel2.Visible = true;
        }

        private void display_combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            view_department.Items.Clear();
            view_doctors.Items.Clear();
            view_patients.Items.Clear();
            foreach (var bc in dplist)
            {// finding the Department.
                if (bc.Getname() == display_combox.Text)
                {// adding the department to the list.
                    ListViewItem lvi = new ListViewItem(bc.Getname());
                    lvi.SubItems.Add(bc.Getnumber());
                    foreach (var dc in doc)
                    {// adding the head doctor to the list.
                        if (dc.Value.get_department() == display_combox.Text)
                        {
                            if (dc.Value.Is_head == true)
                            {
                                lvi.SubItems.Add(dc.Value.get_Name());
                                break;
                            }
                        }
                    }
                    view_department.Items.Add(lvi);
                    if (doctor_checkbox.CheckState == CheckState.Checked)
                    {// if the doctor checkbox is clicked then we add the doctors.
                        ListViewItem lvi1;
                        foreach (var dc in doc)
                        {
                            if (dc.Value.get_department() == display_combox.Text)
                            {
                                lvi1 = new ListViewItem(dc.Value.get_id());
                                lvi1.SubItems.Add(dc.Value.get_Name());
                                view_doctors.Items.Add(lvi1);
                            }
                        }
                    }
                    if (patient_checkbox.CheckState == CheckState.Checked)
                    {// if the patients checkbox is clicked then we add the patients.
                        ListViewItem lvi1;
                        foreach (var dc in pat)
                        {
                            if (dc.Value.get_department() == display_combox.Text)
                            {
                                lvi1 = new ListViewItem(dc.Value.get_id());
                                lvi1.SubItems.Add(dc.Value.get_Name());
                                view_patients.Items.Add(lvi1);
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void DeleteDepartmentButt_Click(object sender, EventArgs e)
        {
            Delete.Visible = true;
            Department.Visible = true;
            panel2.Visible = false;
            if (dplist.Count() == 0)
            {
                MessageBox.Show("There exists no departments.");
                delete_combobox.Items.Clear();
            }
            else
            {
                delete_combobox.Items.Clear();
                foreach (var a in dplist)
                {
                    if (!delete_combobox.Items.Contains(a.Getname()))
                    {
                        delete_combobox.Items.Add(a.Getname());
                    }
                }
            }
        }

      

        private void delete_back_Click(object sender, EventArgs e)
        {
            delete_combobox.Items.Clear();
            Department.Visible = false;
            Delete.Visible = false;
            panel2.Visible = true;
        }

        private void delete_save_Click(object sender, EventArgs e)
        {
            var pa = pat.Keys.ToArray();
            foreach (var bc in pa)
            {// deleting the patients inside of it.
                if (pat[bc].get_department() == delete_combobox.Text)
                {
                    pat.Remove(bc);
                }
            }
            var D = doc.Keys.ToArray();
           foreach(var Do in D)
            {
                if(doc[Do].get_department() == delete_combobox.Text)
                {
                    doc.Remove(Do);
                }
            }
            foreach (var bc in dplist)
            {// deleting the list.
                if (bc.Getname() == delete_combobox.Text)
                {
                    dplist.Remove(bc);
                    MessageBox.Show("Deleted Successfully.");
                    delete_combobox.Items.Clear();
                    delete_combobox.Text = null;
                    if (dplist.Count() == 0)
                    {
                        delete_combobox.Items.Clear();
                    }
                    else
                    {
                        delete_combobox.Items.Clear();
                        foreach (var a in dplist)
                        {
                            if (!delete_combobox.Items.Contains(a.Getname()))
                            {
                                delete_combobox.Items.Add(a.Getname());
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void AppointmentButt_Click(object sender, EventArgs e)
        {
            Appointment.Visible = true;
            main_appointment_panel.Visible = true;
            panel2.Visible = false;
        }
    
        private void Bck_to_main_project_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            main_appointment_panel.Visible = false;
            Appointment.Visible = false;
        }

        

        private void back_from_main_panel_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;

            Appointment.Visible = false;
            
        }

        private void Done_choosedoc_button_Click(object sender, EventArgs e)
        {
            bool ok = true;
            if (view_doctor_list.Items.Count == 0 )
            {
                ok = false ;
            }
            if (view_doctor_list.Items.Count == 0)
            {
                ok = false;
                MessageBox.Show("choose a department ", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (view_doctor_list.SelectedItems.Count == 0)
            {
                ok = false;
                MessageBox.Show("please select doctor ", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (ok)
            {
                add_appointment_panel.Visible = true;
                add_appointment_to_patient.Visible = false;

            }
        }

        private void back_tomainpanel_button_Click(object sender, EventArgs e)
        {
           
            add_appointment_to_patient.Visible = false;
        }

        private void back_fadd_tmain_Click(object sender, EventArgs e)
        {
            add_appointment_panel.Visible = false;
           
        }
        public static bool ok = false;
        private void done_button_to_add_appointment_Click(object sender, EventArgs e)
        {
            if(Patient_ID_TxtBox.Text == "")
            {
                MessageBox.Show("please enter patient's id !", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool okk = true;
            if (hours_textbox.Text == " " || min_textbox.Text == "" || day_textbox.Text == " " || month_textbox.Text == " " || year_textbox.Text == "")
            {
                MessageBox.Show("please fill the empty box", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (int.Parse(day_textbox.Text) > 31 || int.Parse(day_textbox.Text) < 1 || int.Parse(month_textbox.Text) > 12 || int.Parse(month_textbox.Text) < 1 ||
                int.Parse(year_textbox.Text) < 2018 || int.Parse(hours_textbox.Text) < 0 || int.Parse(hours_textbox.Text) > 24 || int.Parse(min_textbox.Text) < 1 ||
                int.Parse(min_textbox.Text) > 60)
            {
                MessageBox.Show("please enter the time or date richtig ", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DateTime date1 = new DateTime(int.Parse(year_textbox.Text), int.Parse(month_textbox.Text), int.Parse(day_textbox.Text), int.Parse(hours_textbox.Text), int.Parse(min_textbox.Text), 0);
                foreach (Appointment search in appo)
                {
                    for (int index = 0; index < view_doctor_list.SelectedItems.Count; index++)
                    {
                        if (date1.ToString() == search.Datetime.ToString()&& search.get_id_doctor().ToString()==view_doctor_list.SelectedItems[index].SubItems[0].Text
                            && search.get_id_pateint().ToString()==Patient_ID_TxtBox.Text)
                        {

                            okk = false;
                        }
                    }
                }
                if (!okk)
                {
                    MessageBox.Show("sorry this time is assigned please choose another ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                   
                    for (int index = 0; index < view_doctor_list.SelectedItems.Count; index++)
                    {
                        appo.Add(new Appointment(date1, Patient_ID_TxtBox.Text, view_doctor_list.SelectedItems[index].SubItems[1].Text));
                    }
                    ok = true;
                        min_textbox.Text = "";
                    hours_textbox.Text = "";
                    day_textbox.Text = "";
                    month_textbox.Text = "";
                    year_textbox.Text = "";
                }
            }
        }

        private void display_appointment_mainbutton_Click(object sender, EventArgs e)
        {
            main_appointment_panel.Visible = false;
            display_appointments_panel.Visible = true;
        }

        private void Back_from_display_to_main_apointment_Click(object sender, EventArgs e)
        {
            main_appointment_panel.Visible = true;
            display_appointments_panel.Visible = false;
            display_appoinment_listview.Items.Clear();   
        }

        private void Display_oneapp_button_Click(object sender, EventArgs e)
        {
            bool done = false;
            if (id_patient_text_box_todisplay.Text == " ")
            {
                MessageBox.Show("please fill  box ", "waning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ListViewItem dp;
                foreach (Appointment search in appo)
                {
                    if (id_patient_text_box_todisplay.Text == search.get_id_pateint())
                    {

                        dp = new ListViewItem(search.get_id_doctor().ToString());
                        dp.SubItems.Add(search.Datetime.Day.ToString());
                        dp.SubItems.Add(search.Datetime.Month.ToString());
                        dp.SubItems.Add(search.Datetime.Year.ToString());
                        dp.SubItems.Add(search.Datetime.Hour.ToString());
                        dp.SubItems.Add(search.Datetime.Minute.ToString());
                        dp.SubItems.Add(search.get_id_pateint().ToString());
                        display_appoinment_listview.Items.Add(dp);
                        done = true;

                    }

                }
                if (!done)
                {
                    MessageBox.Show("non appointments in this day", "waning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }

            id_patient_text_box_todisplay.Text = "";

        }

        private void display_all_app_button_Click(object sender, EventArgs e)
        {
            ListViewItem dp;
            foreach (Appointment search in appo)
            {


                dp = new ListViewItem(search.get_id_doctor().ToString());
                dp.SubItems.Add(search.Datetime.Day.ToString());
                dp.SubItems.Add(search.Datetime.Month.ToString());
                dp.SubItems.Add(search.Datetime.Year.ToString());
                dp.SubItems.Add(search.Datetime.Hour.ToString());
                dp.SubItems.Add(search.Datetime.Minute.ToString());
                dp.SubItems.Add(search.get_id_pateint().ToString());
                display_appoinment_listview.Items.Add(dp);

            }
        }

        private void delete_appointment_mainbutton_Click(object sender, EventArgs e)
        {
            delete_appointment_panel.Visible = true;
            main_appointment_panel.Visible = false;
        }

        private void back_from_deletepanel_button_Click(object sender, EventArgs e)
        {
            delete_appointment_panel.Visible = false;
            main_appointment_panel.Visible = true;
        }

        private void delete_app_button_Click(object sender, EventArgs e)
        {
            bool done = false;
            if (get_min_todel.Text == " " || get_month_todel.Text == "" || get_day_todel.Text == "" || get_year_todel.Text == "" || get_hours_todel.Text == " " || get_pateint_id_todel.Text == " ")
            {
                MessageBox.Show("please fill all boxes ", "waning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                        
                        for (int i = 0; i < appo.Count; i++)
                        {
                            if (get_pateint_id_todel.Text.ToString() == appo[i].get_id_pateint().ToString() && get_min_todel.Text.ToString() == appo[i].Datetime.Minute.ToString()
                               && get_hours_todel.Text.ToString() == appo[i].Datetime.Hour.ToString() && get_month_todel.Text.ToString() == appo[i].Datetime.Month.ToString()
                                && get_year_todel.Text.ToString() == appo[i].Datetime.Year.ToString() && get_day_todel.Text.ToString() ==appo[i].Datetime.Day.ToString())
                            {
                                appo.Remove(appo[i]);
                                done = true;
                            }

                        }
                    
                
                if (!done)
                {
                    MessageBox.Show("gibt es nicht zeit please enter the time ", "waning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                get_min_todel.Text = "";
                get_month_todel.Text = "";
                get_day_todel.Text = "";
                get_year_todel.Text = "";
                get_hours_todel.Text = "";
                get_pateint_id_todel.Text = "";
            }
        }

        private void AddPatientButt_Click(object sender, EventArgs e)
        {
            if (dplist.Count == 0)
            {
                MessageBox.Show("Please Enter a department first!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(nurse.Count == 0)
            {
                MessageBox.Show("Please Enter a nurse first!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(doc.Count==0)
            {
                MessageBox.Show("Please Enter a doctor first!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            comboBox_select_nurse.Items.Clear();
            combobox_patient_department.Items.Clear();
           
            foreach (var dep in dplist)
            {
                combobox_patient_department.Items.Add(dep.Getname());
            }
            Patient_Panal.Visible = true;
            Add_Patient_Panal.Visible = true;
            panel2.Visible = false;
        }

        private void Add_Patient_Back_btn_Click(object sender, EventArgs e)
        {
          
            Appointment_radBtn.Checked = false;
            Resident_radBtn.Checked = false;
            Patient_Panal.Visible = false;
            Add_Patient_Panal.Visible = false;
            panel2.Visible = true;
            Dodctor_comboBox.Visible = false;
            label34.Visible = false;
            label31.Visible = false;

            comboBox_select_room.Visible = false;
            combobox_patient_department.Text = "";
        }

        private void Next_Patient_btn_Click(object sender, EventArgs e)
        {
            bool done = true, id_done = true;

            //check whether the data are filled or not

            if (Patient_Name_TxtBox.Text == "" || comboBox_select_nurse.Text == "" || patiet_history.Text == "" || Patient_Phone_TxtBox.Text == "" || Patient_ID_TxtBox.Text == "" || Patient_Age_TxtBox.Text == "" || combobox_patient_department.Text == "" || (Appointment_radBtn.Checked == false && Resident_radBtn.Checked == false))
            {
                MessageBox.Show("Please Enter All Data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                done = false;
            }
            if(Resident_radBtn.Checked)
            {
                if ( comboBox_select_room.Text == "" || Dodctor_comboBox.Text == "")
                {
                    MessageBox.Show("Please Enter All Data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    done = false;
                }
            }
            //check whether the entered ID has been used before or not

            if (pat.ContainsKey(Patient_ID_TxtBox.Text))
            {
                MessageBox.Show("ID has been used before", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                id_done = false;
            }
            else
            {
                id_done = true;
            }

            //All is good, Creating a new Patient

            if (done && id_done)
            {
                if(Resident_radBtn.Checked == true)
                {


                    if (comboBox_select_room.Text == "Standard ward")
                    {
                        if (!sr.Check_full())
                        {
                            MessageBox.Show("sorry,this room is full !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else if (comboBox_select_room.Text == "Private")
                    {
                        if (!pr.Check_full())
                        {
                            MessageBox.Show("sorry,this room is full !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else if(comboBox_select_room.Text == "Semi Private")
                    {
                        if (!spr.Check_full())
                        {
                            MessageBox.Show("sorry,this room is full !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    Patient p = new Patient(Patient_Name_TxtBox.Text, Patient_Age_TxtBox.Text, Patient_ID_TxtBox.Text, Patient_Phone_TxtBox.Text, combobox_patient_department.Text, patiet_history.Text, comboBox_select_room.Text, "Resident");
                    string s = Dodctor_comboBox.Text;
                    string[] a = s.Split(' ');
                    string m = a[0];
                    doc[m].Add_Patient(p.get_id());
                    p.Add_Doctor(m);
                     s = comboBox_select_nurse.Text;
                    string[] b = s.Split(' ');
                     m = b[0];
                    nurse[m].Add_patient(p.get_id());
                   
                    pat.Add(p.get_id(), p);
                }
                else if (Appointment_radBtn.Checked == true)
                {
                    if (ok)
                    {
                        string q = "";
                        Patient p = new Patient(Patient_Name_TxtBox.Text, Patient_Age_TxtBox.Text, Patient_ID_TxtBox.Text, Patient_Phone_TxtBox.Text, combobox_patient_department.Text, patiet_history.Text, "", "Appointment");
                        for (int index = 0; index < view_doctor_list.SelectedItems.Count; index++)
                        {
                           q = view_doctor_list.SelectedItems[index].SubItems[1].Text;
                        }
                        p.Add_Doctor(q);
                        doc[q].Add_Patient(p.get_id());
                        pat.Add(p.get_id(), p);
                    }
                    else
                    {
                        MessageBox.Show("there is a problem with appoientment !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                Patient_Name_TxtBox.Clear();
                Patient_Phone_TxtBox.Clear();
                Patient_ID_TxtBox.Clear();
                Patient_Age_TxtBox.Clear();
                combobox_patient_department.Text = "";
                Dodctor_comboBox.Items.Clear();
                Dodctor_comboBox.Text = "";
                Dodctor_comboBox.Visible = false;
                Resident_radBtn.Checked = false;
                Appointment_radBtn.Checked = false;
                comboBox_select_nurse.Text = "";
                comboBox_select_nurse.Items.Clear();
                comboBox_select_room.Text = "";
                label31.Visible = false;
                label34.Visible = false;
                comboBox_select_room.Visible = false;
                patiet_history.Text = "";

               MessageBox.Show("Patient Has Been Added!", "Successful Process", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Search_PatientToDelete_btn_Click(object sender, EventArgs e)
        {
            if (Patient_ID_toDelete_txtBox.Text == "")
            {
                MessageBox.Show("Please Enter ID First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!pat.ContainsKey(Patient_ID_toDelete_txtBox.Text))
                {
                    MessageBox.Show("There Is No Patient With This ID!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Patient display_patient = pat[Patient_ID_toDelete_txtBox.Text];
                    ListViewItem patient_list = new ListViewItem(display_patient.get_id());
                    patient_list.SubItems.Add(display_patient.get_Name());
                    patient_list.SubItems.Add(display_patient.get_phone());
                    patient_list.SubItems.Add(display_patient.get_Age());
                    patient_list.SubItems.Add(display_patient.get_type());



                    Viewing_PatientToDelete_List.Items.Add(patient_list);

                    Viewing_PatientToDelete_List.Visible = true;
                    Delete_Patient_Asking_Label.Visible = true;
                    Yes_Delete_Patient_btn.Visible = true;
                    No_Delete_Patient_btn.Visible = true;

                }
            }
        }

        private void DeletePatientButt_Click(object sender, EventArgs e)
        {
            Patient_Panal.Visible = true;
            Delete_Patient_Panel.Visible = true;
            panel2.Visible = false;
        }

        private void Back_FromDeletePatient_Panel_Click(object sender, EventArgs e)
        {
            Patient_ID_toDelete_txtBox.Clear();
            Patient_Panal.Visible = false;
            Delete_Patient_Panel.Visible = false;
            panel2.Visible = true;
        }

        private void Yes_Delete_Patient_btn_Click(object sender, EventArgs e)
        {
            pat.Remove(Patient_ID_toDelete_txtBox.Text);
            Viewing_PatientToDelete_List.Clear();
            Patient_ID_toDelete_txtBox.Clear();
            MessageBox.Show("Patient Has Been Deleted!", "Successful Process", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void No_Delete_Patient_btn_Click(object sender, EventArgs e)
        {
            Viewing_PatientToDelete_List.Clear();
            Viewing_PatientToDelete_List.Visible = false;
            Delete_Patient_Asking_Label.Visible = false;
            Yes_Delete_Patient_btn.Visible = false;
            No_Delete_Patient_btn.Visible = false;
        }

        private void EditPatientButt_Click(object sender, EventArgs e)
        {
            Patient_Panal.Visible = true;
            Update_Patient_Panal.Visible = true;
            panel2.Visible = false;
        }

        private void Patient_IDSearchToUpdate_btn_Click(object sender, EventArgs e)
        {
            if (!pat.ContainsKey(Patient_ID_ToUpdate_txtBox.Text))
            {
                MessageBox.Show("ID Is Not Found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                enterdep_pat_toupdate_label.Visible = true;
                enterphone_pat_toupdate_label.Visible = true;
                enterage_pat_toupdate_label.Visible = true;
                entername_pat_toudpate_label.Visible = true;
                Patient_NameToUpdate_txtBox.Visible = true;
                Patient_AgeToUpdate_txtBox.Visible = true;
                Patient_PhoneToUpdate_txtBox.Visible = true;
                Patient_DepartmentToUpdate_txtBox.Visible = true;
                UpdatePatientBtn.Visible = true;

                Patient_NameToUpdate_txtBox.Text = pat[Patient_ID_ToUpdate_txtBox.Text].get_Name();
                Patient_AgeToUpdate_txtBox.Text = pat[Patient_ID_ToUpdate_txtBox.Text].get_Age();
                Patient_PhoneToUpdate_txtBox.Text = pat[Patient_ID_ToUpdate_txtBox.Text].get_phone();
                Patient_DepartmentToUpdate_txtBox.Text = pat[Patient_ID_ToUpdate_txtBox.Text].get_department();

            }
        }

        private void UpdatePatientBtn_Click(object sender, EventArgs e)
        {
            string patientID = Patient_ID_ToUpdate_txtBox.Text;
            Patient_ID_ToUpdate_txtBox.Clear();

            pat[patientID].set_Name(Patient_NameToUpdate_txtBox.Text);
            pat[patientID].set_Age(Patient_AgeToUpdate_txtBox.Text);
            pat[patientID].set_phone(Patient_PhoneToUpdate_txtBox.Text);
            pat[patientID].set_department(Patient_DepartmentToUpdate_txtBox.Text);

            MessageBox.Show("Patient Has Been Updated!", "Sucessful Process", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Back_FromUpdatePatientPanal_btn_Click(object sender, EventArgs e)
        {
            Patient_NameToUpdate_txtBox.Clear();
            Patient_AgeToUpdate_txtBox.Clear();
            Patient_PhoneToUpdate_txtBox.Clear();
            Patient_DepartmentToUpdate_txtBox.Clear();
            Patient_Panal.Visible = false;
            Update_Patient_Panal.Visible = false;
            panel2.Visible = true;

        }

        private void DisplayPatientButt_Click(object sender, EventArgs e)
        {
            Patient_Panal.Visible = true;
            Display_Patient_Panel.Visible = true;
            panel2.Visible = false;
        }

        private void Back_From_DisplayPatientPanel_Btn_Click(object sender, EventArgs e)
        {
            Patient_Display_ListView.Items.Clear();
            Patient_Display_ListView.Visible = false;
            panel2.Visible = true;
            Display_Patient_Panel.Visible = false;
            Patient_Panal.Visible = false;
        }

        private void Patient_innerDisplay_btn_Click(object sender, EventArgs e)
        {
            //check if the ID is entered
            if (Patient_ID_Display_txtBox.Text == "")
            {
                MessageBox.Show("Please Enter ID First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //check if the entered ID exists

                if (!pat.ContainsKey(Patient_ID_Display_txtBox.Text))
                {
                    MessageBox.Show("There Is No Patient With This ID!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //display the patient data

                else
                {

                    Patient display_patient = pat[Patient_ID_Display_txtBox.Text];
                    ListViewItem patient_list = new ListViewItem(display_patient.get_id());
                    patient_list.SubItems.Add(display_patient.get_Name());
                    patient_list.SubItems.Add(display_patient.get_phone());
                    patient_list.SubItems.Add(display_patient.get_Age());
                    patient_list.SubItems.Add(display_patient.get_type());


                    Patient_Display_ListView.Items.Add(patient_list);

                    Patient_ID_Display_txtBox.Clear();

                    Patient_Display_ListView.Visible = true;
                }
            }
        }

        private void edit_label2_Click(object sender, EventArgs e)
        {

        }

        private void delete_choose_Click(object sender, EventArgs e)
        {

        }

        private void add_doctor_panel_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void Resident_radBtn_Click(object sender, EventArgs e)
        {
            Dodctor_comboBox.Items.Clear();
            if (Resident_radBtn.Checked == true)
            {
                foreach(var D in doc)
                {
                    if (D.Value.get_department() == combobox_patient_department.Text)
                    {
                        Dodctor_comboBox.Items.Add(D.Value.get_id() + " " +D.Value.get_Name());
                    }
                }
                Dodctor_comboBox.Visible = true;
                label34.Visible = true;
 
                label31.Visible = true;
 
                comboBox_select_room.Visible = true;
            }
        }

        private void get_dep_textboxx_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void get_dep_from_doc_compobox_Click(object sender, EventArgs e)
        {
          
        }

        private void Appointment_radBtn_Click(object sender, EventArgs e)
        {
            view_doctor_list.Items.Clear();
            combobox_patient_department.Items.Clear();
            foreach (var dep in dplist)
            {
 
                    
                    combobox_patient_department.Items.Add(dep.Getname());
                
            }
            ListViewItem s;
            foreach (var D in doc)
            {
                if (D.Value.get_department() == combobox_patient_department.Text)
                {
                    s = new ListViewItem(D.Value.get_Name());
                    s.SubItems.Add(D.Value.get_id());
                    view_doctor_list.Items.Add(s);
                }
            }

            add_appointment_to_patient.Visible = true;
            Dodctor_comboBox.Visible = false;
            label34.Visible = false;

            label31.Visible = false;

            comboBox_select_room.Visible = false;

        }

        private void comboBox_head_department_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool done = false;
            foreach (var search in doc)
            {
                if (search.Value.get_department() == comboBox_head_department.Text && search.Value.Is_head == true)
                {
                    text_head_name.Text = search.Value.get_Name();
                    text_head_age.Text = search.Value.get_Age();
                    text_head_id.Text = search.Key;
                    text_head_phone.Text = search.Value.get_phone();
                    done = true;
                    break;
                }
            }
            if (!done)
            {
                MessageBox.Show("There is no head for this department", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                text_head_name.Text = "";
                text_head_age.Text = "";
                text_head_id.Text = "";
                text_head_phone.Text = "";
                comboBox_head_department.Text = "";
            }
        }

        private void Appointment_radBtn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void doctor_checkbox_Click(object sender, EventArgs e)
        {

        }

        private void doctor_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            view_doctors.Items.Clear();
            if (doctor_checkbox.CheckState == CheckState.Checked)
            {// if the doctor checkbox is clicked then we add the doctors.
                ListViewItem lvi1;
                foreach (var dc in doc)
                {
                    if (dc.Value.get_department() == display_combox.Text)
                    {
                        lvi1 = new ListViewItem(dc.Value.get_id());
                        lvi1.SubItems.Add(dc.Value.get_Name());
                        view_doctors.Items.Add(lvi1);
                    }
                }
            }
        }

        private void patient_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            view_patients.Items.Clear();
            if (patient_checkbox.CheckState == CheckState.Checked)
            {// if the patients checkbox is clicked then we add the patients.
                ListViewItem lvi1;
                foreach (var dc in pat)
                {
                    if (dc.Value.get_department() == display_combox.Text)
                    {
                        lvi1 = new ListViewItem(dc.Value.get_id());
                        lvi1.SubItems.Add(dc.Value.get_Name());
                        view_patients.Items.Add(lvi1);
                    }
                }
            }
        }

        private void Dodctor_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Resident_radBtn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void combobox_patient_department_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox_select_nurse_Click(object sender, EventArgs e)
        {
           
                
        }

        private void combobox_patient_department_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void comboBox_select_nurse_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            comboBox_select_nurse.Items.Clear();
            if (combobox_patient_department.Text != "")
            {
                foreach (var n in nurse)
                {
                    if (combobox_patient_department.Text == n.Value.get_deparment())
                    {
                        comboBox_select_nurse.Items.Add(n.Key + ' ' + n.Value.get_Name());
                    }
                }
            }
            else
                MessageBox.Show("Pleas select a department !");
        }
    }
}

