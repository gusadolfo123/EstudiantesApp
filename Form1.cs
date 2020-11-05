using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstudiantesApp
{
    public partial class Form1 : Form
    {

        private EstudiantesBDDataSet.EstudianteDataTable _currentStudents;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'estudiantesBDDataSet.Estudiante' Puede moverla o quitarla según sea necesario.
            this.estudianteTableAdapter.Fill(this.estudiantesBDDataSet.Estudiante);
            _currentStudents = this.estudiantesBDDataSet.Estudiante;

            txtCode.Enabled = true;
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;

        }

        private void dgwStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // evento cuadno se selecciona un registor de la tabla 
            if (dgwStudents.SelectedCells.Count > 0)
            {
                txtCode.Text = dgwStudents.SelectedCells[0].Value.ToString();
                txtNames.Text = dgwStudents.SelectedCells[1].Value.ToString();
                txtLastNames.Text = dgwStudents.SelectedCells[2].Value.ToString();
                txtEmail.Text = dgwStudents.SelectedCells[3].Value.ToString();
                txtPhoneNumber.Text = dgwStudents.SelectedCells[4].Value.ToString();
                txtCarrer.Text = dgwStudents.SelectedCells[5].Value.ToString();

                txtCode.Enabled = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            txtNames.Text = "";
            txtLastNames.Text = "";
            txtEmail.Text = "";
            txtPhoneNumber.Text = "";
            txtCarrer.Text = "";

            txtCode.Enabled = true;
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.estudianteTableAdapter.Insert(
                long.Parse(txtCode.Text),
                txtNames.Text,
                txtLastNames.Text,
                txtEmail.Text,
                long.Parse(txtPhoneNumber.Text),
                txtCarrer.Text
                );

            this.estudianteTableAdapter.Fill(this.estudiantesBDDataSet.Estudiante);
            _currentStudents = this.estudiantesBDDataSet.Estudiante;

            this.btnClear_Click(sender, e);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // se selecciona el row del estudiante seleccionado para ser actualizado
            EstudiantesBDDataSet.EstudianteRow selected_student = _currentStudents.Where(x => x.Codigo == long.Parse(txtCode.Text)).FirstOrDefault();
            selected_student.Nombres = txtNames.Text;
            selected_student.Apellidos = txtLastNames.Text;
            selected_student.Correo = txtEmail.Text;
            selected_student.Telefono = long.Parse(txtPhoneNumber.Text);
            selected_student.Carrera = txtCarrer.Text;

            int result = this.estudianteTableAdapter.Update(selected_student);

            if (result > 0)
            {
                MessageBox.Show($"Estudiante con codigo {txtCode.Text} Actualizado correctamente");

                this.estudianteTableAdapter.Fill(this.estudiantesBDDataSet.Estudiante);
                _currentStudents = this.estudiantesBDDataSet.Estudiante;

                this.btnClear_Click(sender, e);
            }
            else
                MessageBox.Show($"No se pudo actualizar el estudiante con codigo {txtCode.Text}");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // se selecciona el row del estudiante seleccionado para ser eliminado
            EstudiantesBDDataSet.EstudianteRow selected_student = _currentStudents.Where(x => x.Codigo == long.Parse(txtCode.Text)).FirstOrDefault();

            int result = this.estudianteTableAdapter.Delete(selected_student.Codigo, selected_student.Nombres, selected_student.Apellidos, selected_student.Correo, selected_student.Telefono, selected_student.Carrera);

            if(result > 0)
            {                
                MessageBox.Show($"Estudiante con codigo {txtCode.Text} Eliminado correctamente");

                this.estudianteTableAdapter.Fill(this.estudiantesBDDataSet.Estudiante);
                _currentStudents = this.estudiantesBDDataSet.Estudiante;

                this.btnClear_Click(sender, e);
            }
            else
                MessageBox.Show($"No se elimino el estudiante con codigo {txtCode.Text}");

            
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
