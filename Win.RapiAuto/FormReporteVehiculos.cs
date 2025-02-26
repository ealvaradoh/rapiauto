﻿using BL.RapiAuto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.RapiAuto
{
    public partial class FormReporteVehiculos : Form
    {
        VehiculosBL _VehiculosBL;
        BindingSource bindingsource;
        ReporteVehiculos reporte;
        public FormReporteVehiculos()
        {
            InitializeComponent();

            List<TextBox> BuscarTlist = new List<TextBox>();
            List<string> BuscarSlist = new List<String>();
            BuscarTlist.Add(txtBuscarVehiculo);
            BuscarSlist.Add("Filtrar por modelo de vehículo");
            SetCueBanner(ref BuscarTlist, BuscarSlist);

            _VehiculosBL = new VehiculosBL();
            bindingsource = new BindingSource();
            bindingsource.DataSource = _VehiculosBL.ObtenerVehiculos();

            reporte = new ReporteVehiculos();
            reporte.SetDataSource(bindingsource);
                
            crystalReportViewer1.ReportSource = reporte;
            crystalReportViewer1.RefreshReport();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr i, string str);

        void SetCueBanner(ref List<TextBox> textBox, List<string> CueText)
        {
            for (int x = 0; x < textBox.Count; x++)
            {
                SendMessage(textBox[x].Handle, 0x1501, (IntPtr)1, CueText[x]);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            _VehiculosBL = new VehiculosBL();
            var textoABuscar = txtBuscarVehiculo.Text;
            bindingsource.DataSource =
                _VehiculosBL.ObtenerVehiculos(textoABuscar);

            reporte.SetDataSource(bindingsource);
            crystalReportViewer1.ReportSource = reporte;
            crystalReportViewer1.RefreshReport();
        }
    }
}
