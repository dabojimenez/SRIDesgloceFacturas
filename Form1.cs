using Data;
using Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRidesgloceFacturas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarArchivo();
        }
        private void CargarArchivo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Documentos|*.txt;*.csv";
            openFileDialog.Title = "Cargar Canciones";
            string filePath ;
            List<Recibo> recibos = new List<Recibo>();
            List<Comprobante> comprobantes = new List<Comprobante>();
            List<string> formateadoTexto = new List<string>();
            ArchivoTXT archivoTXT = new ArchivoTXT();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                
                List<string> lineOfText = File.ReadAllText(filePath, Encoding.UTF8).Replace(',', '.')
                    .Split(new Char[] { '\t', '\r', '\n' }).ToList();

                //ArchivoTXT archivoTXT = new ArchivoTXT();

                archivoTXT.FormatearTexto(ref formateadoTexto, lineOfText);
                archivoTXT.InsertarRegistrosEnLista(ref recibos,ref comprobantes, formateadoTexto);

                dgvComprobantes.DataSource = comprobantes;
                dgvRecibos.DataSource = recibos;
                pnlRecibo.Visible = true;
            }
            lblTotalRegistros.Text = Convert.ToString(dgvRecibos.RowCount);
            lblTotalComprobantes.Text = Convert.ToString(dgvComprobantes.RowCount);
            MostrarConteosTotales(recibos);
        }
        private void MostrarConteosTotales(List<Recibo> recibos)
        {
            string total = "";
            string totalIVA = "";
            string totalBase = "";
            ArchivoTXT archivoTXT = new ArchivoTXT();
            archivoTXT.SumatoriaValorTotalEnRecibos(recibos, ref total);
            lblTotal.Text = total;
            archivoTXT.SumatoriaValorTotalIVAEnRecibos(recibos, ref totalIVA);
            lblTotalIVA.Text = totalIVA;
            archivoTXT.SumatoriaValorTotalBaseEnRecibos(recibos, ref totalBase);
            lblTotalBase.Text = totalBase;
        }

        private void pnlRecibo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            btnAgregar.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
            pnlReciboDatos.Visible = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlReciboDatos.Visible = false;
            btnAgregar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
