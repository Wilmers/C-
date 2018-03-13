using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Configuration;
namespace Aplicacion_Crud
{
    public partial class Form1 : Form
    {
        ConnectionStringSettings cadena = ConfigurationManager.ConnectionStrings["cnSQL"];
        SqlConnection cn = new SqlConnection();
        public Form1()
        {
            InitializeComponent();
            cn = new SqlConnection(cadena.ConnectionString);
        }

        DataTable lisatdo()
        {
            SqlDataAdapter da = new SqlDataAdapter("ups_listado_pais",cn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("ups_agregar_pais", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@codigo", SqlDbType.Char, 3).Value = txtCodigo.Text;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = txtNombre.Text;
            cn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                MessageBox.Show(i.ToString() + "se registro corectamente");
            }catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
                dgLista.DataSource = lisatdo();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgLista.DataSource = lisatdo();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("ups_eliminar_pais", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@codigo", SqlDbType.Char, 3).Value = txtCodigo.Text;
            cn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                MessageBox.Show(i.ToString() + "registro eliminado");
            }catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
                dgLista.DataSource = lisatdo();
            }

        }

        private void btnactulizar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("usp_actualizar_pais", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@codigo", SqlDbType.Char, 3).Value = txtCodigo.Text;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = txtNombre.Text;

            cn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                MessageBox.Show(i.ToString() + "registro actualizado");
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
                dgLista.DataSource = lisatdo();
            }
        }

        private void btnListadoNombre_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("ups_listado_nombrepais", cn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@nombre", SqlDbType.VarChar, 50).Value = txtNombre.Text;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgLista.DataSource = dt;

        }
    }
}
