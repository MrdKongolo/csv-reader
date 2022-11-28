using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetroUI
{
    public partial class frmAccueil : MetroFramework.Forms.MetroForm
    {
        public frmAccueil()
        {
            InitializeComponent();
        }
        public DataTable Lire(string fichier)
        {
            DataTable dt = new DataTable("Data");
            using (OleDbConnection cn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = \"" + 
                Path.GetDirectoryName(fichier) + "\"; Extended Properties='text;HDR=yes;FMT=Delimited(;)';"))
            {
                using (OleDbCommand cmd = new OleDbCommand(string.Format("select * from [{0}]", new FileInfo(fichier).Name), cn))
                {
                    cn.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        private void btnOuvrir_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Fichiers (csv)|*.csv", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        metroGrid.DataSource = Lire(ofd.FileName);
                }
            }
            catch (Exception exc)
            {
               MetroFramework.MetroMessageBox.Show(this,exc.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
