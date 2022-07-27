using NHibernateSample.Daos;
using NHibernateSample.Domain;
using NHibernateSample.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace NHibernateSample
{
    public partial class AddPersonView : Form
    {
        private HomeView homeView;
        public AddPersonView(HomeView homeView)
        {
            InitializeComponent();
            IList<string> genderList = Enum.GetNames(typeof(Gender));
            cbGender.DataSource = genderList;
            this.homeView = homeView;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Person person = new Person
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Age = Int16.Parse(txtAge.Text),
                Gender = ((Gender)cbGender.SelectedIndex).ToString()
            };

            person = PersonDao.GetInstance().AddOrUpdate(person);
            Debug.WriteLine("Person stored in database. Id = " + person.Id);

            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cbGender.ResetText();

            homeView.AddNewPerson(person);
        }
    }
}
