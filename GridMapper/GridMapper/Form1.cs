﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GridMapper.NetworkRepository;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;

namespace GridMapper
{
	public partial class GridWindow : Form
	{
		Option _startUpOption;
		Execution _exe;

		// définition du delegate qui sera utilisé pour traiter les events
		private delegate void UpdateDataGrid<T>( object sender, T e );
		// **************** 

		public GridWindow(Option StartUpOptions)
		{
			_startUpOption = StartUpOptions;
			_exe = new Execution( StartUpOptions );

			InitializeComponent();
			InitializeDataGridView();
		}

		void InitializeDataGridView()
		{
			dataGridView1.AllowUserToResizeColumns = false;
			dataGridView1.AllowUserToResizeRows = false;
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.AutoGenerateColumns = false;
			dataGridView1.AllowUserToAddRows = false;

			dataGridView1.ColumnCount = 4;
			dataGridView1.Columns[0].DataPropertyName = "Id";
			dataGridView1.Columns[0].Name = "Id";
			dataGridView1.Columns[0].Visible = false;
			dataGridView1.Columns[1].DataPropertyName = "IPAddress";
			dataGridView1.Columns[1].Name = "IPAddress";
			dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
			dataGridView1.Columns[2].DataPropertyName = "MacAddress";
			dataGridView1.Columns[2].Name = "MacAddress";
			dataGridView1.Columns[3].DataPropertyName = "HostName";
			dataGridView1.Columns[3].Name = "HostName";

			//PingSender.PingCompleted += UpdateDataGridView;
			//ARPSender.MacCompleted += UpdateDataGridView;
			Repository.OnRepositoryUpdated += UpdateDataGridView;
			
		}

		public void UpdateDataGridView( object sender, PingCompletedEventArgs e )
		{
			dataGridView1.Invoke( new UpdateDataGrid<PingCompletedEventArgs>( UpdateDataGridView2 ), new object[]{ sender, e } );
		}
		public void UpdateDataGridView( object sender, MacCompletedEventArgs e )
		{
			dataGridView1.Invoke( new UpdateDataGrid<MacCompletedEventArgs>( UpdateDataGridView2 ), new object[] { sender, e } );
		}
		public void UpdateDataGridView( object sender, RepositoryUpdatedEventArg e )
		{
			dataGridView1.Invoke( new UpdateDataGrid<RepositoryUpdatedEventArg>( UpdateDataGridView2 ), new object[] { sender, e } );
		}

		public void UpdateDataGridView2( object sender, PingCompletedEventArgs e )
		{
			byte[] b = e.PingReply.Address.GetAddressBytes();
			IPAddressV4 ip = new IPAddressV4();
			ip.B0 = b[0];
			ip.B1 = b[1];
			ip.B2 = b[2];
			ip.B3 = b[3];
			string[] row = { ip.Address.ToString(), e.PingReply.Address.ToString(), "" , "" };
			dataGridView1.Rows.Add( row );
		}
		public void UpdateDataGridView2( object sender, MacCompletedEventArgs e )
		{
		}
		public void UpdateDataGridView2( object sender, RepositoryUpdatedEventArg e )
		{
			dataGridView1.DataSource = null;
			dataGridView1.DataMember = null;
			foreach( INetworkDictionaryItem item in e.ReadOnlyRepository )
			{
				byte[] b = item.IPAddress.GetAddressBytes();
				IPAddressV4 ip = new IPAddressV4();
				ip.B0 = b[0];
				ip.B1 = b[1];
				ip.B2 = b[2];
				ip.B3 = b[3];
				string[] row = { ip.Address.ToString(), item.IPAddress.ToString(), "", "" };
				dataGridView1.Rows.Add( row );
			}
		}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void ipAddressControl1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML File|*.xml|Gridmap File|*.gmp|Text File|*.txt|Other XML File|*.*";
            openFileDialog1.Title = "Select a Map File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                this.Cursor = new Cursor(openFileDialog1.OpenFile());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // take a look
            //System.Diagnostics.Process.Start(@"C:\Windows");
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML File|*.xml|Gridmap File|*.gmp|Text File|*.txt|Other XML File|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                this.Cursor = new Cursor(openFileDialog1.OpenFile());
            }
        }

        private void SaveScan_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML File|*.xml|Gridmap File|*.gmp|Text File|*.txt";
            saveFileDialog1.Title = "Save a Map File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();

                fs.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML File|*.xml|Gridmap File|*.gmp|Text File|*.txt";
            saveFileDialog1.Title = "Save a Map File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();

                fs.Close();
            }
        }

        private void startScanToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

		private void fastScanToolStripMenuItem_Click( object sender, EventArgs e )
		{
			_exe.StartScan();
		}

		private void fastScanToolStripMenuItem_Click_1( object sender, EventArgs e )
		{
			dataGridView1.DataSource = null;
			dataGridView1.DataMember = null;
			_exe.StartScan();
		}

        private void ProgressScan_Click(object sender, EventArgs e)
        {
            ProgressScan.Minimum = 0;
            ProgressScan.Maximum = 100;
            ProgressScan.Value = ProgressScan.Minimum;
            while (ProgressScan.Value < ProgressScan.Maximum)
            {
                ProgressScan.Value = _exe.Progress();
                System.Threading.Thread.Sleep(1000);
            }

            // ce code degeux marche 
            //ProgressScan.Minimum = 0;
            //ProgressScan.Maximum = 10;
            //ProgressScan.Value = ProgressScan.Minimum;
            //while (ProgressScan.Value < ProgressScan.Maximum)
            //{
            //    ProgressScan.Value += 1;
            //    System.Threading.Thread.Sleep(1); //Cette ligne ne sert qu'a stopper l'exécution 1 seconde entre chaque changement de la progressBar.
            //}
        }
	}
}
