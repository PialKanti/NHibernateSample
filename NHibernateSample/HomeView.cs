using NHibernateSample.Daos;
using NHibernateSample.Domain;
using NHibernateSample.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NHibernateSample
{
    public partial class HomeView : Form
    {
        public HomeView()
        {
            InitializeComponent();
            SetUpDataGridView();
            PopulateDataGridView();
        }        

        private void SetUpDataGridView()
        {
            personDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            personDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            personDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            personDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(personDataGridView.Font, FontStyle.Bold);
            personDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            personDataGridView.ReadOnly = true;
            personDataGridView.ColumnCount = 5;
            personDataGridView.CellContentClick += PersonDataGridView_CellContentClick;

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn
            {
                HeaderText = "#",
                Text = "Delete",
                Name = "btnDelete",
                UseColumnTextForButtonValue = true
            };
            personDataGridView.Columns.Add(btnDelete);

            personDataGridView.Columns[0].Name = "Id";
            personDataGridView.Columns[1].Name = "First name";
            personDataGridView.Columns[2].Name = "Last name";
            personDataGridView.Columns[3].Name = "Age";
            personDataGridView.Columns[4].Name = "Gender";
        }

        private void PersonDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if(e.ColumnIndex == 5)
                {
                    DataGridViewRow row = personDataGridView.Rows[e.RowIndex];
                    int personId = ((int)row.Cells["Id"].Value);
                    PersonDao.GetInstance().Delete(personId);
                    RemovePerson(e.RowIndex);
                }
            }
        }

        private void PopulateDataGridView()
        {
            IList<Person> personList = PersonDao.GetInstance().GetAllPersons();

            foreach(Person person in personList)
            {
                AddNewPerson(person);
            }
        }

        public void AddNewPerson(Person person)
        {
            personDataGridView.Rows.Add(person.Id, person.FirstName, person.LastName, person.Age, person.Gender);
        }

        public void RemovePerson(int personId)
        {
            personDataGridView.Rows.RemoveAt(personId);
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            AddPersonView addPersonView = new AddPersonView(this);
            addPersonView.Show();
        }
    }
}
