using System;
using System.Windows.Forms;
using System.ServiceModel;

namespace Kalkulator
{
    public partial class Form1 : Form
    {
        double z1r, z1i, z2r, z2i;
        ServiceReference.Zespolone z, z1, z2;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btPlus_Click(object sender, EventArgs e)
        {
            double.TryParse(textReal1.Text, out z1r);
            double.TryParse(textImag1.Text, out z1i);
            double.TryParse(textReal2.Text, out z2r);
            double.TryParse(textImag2.Text, out z2i);

            z1 = new ServiceReference.Zespolone();
            z1.R = z1r;
            z1.I = z1i;

            z2 = new ServiceReference.Zespolone();
            z2.R = z2r;
            z2.I = z2i;

            ServiceReference.Service1Client client = new ServiceReference.Service1Client();

            if (checkBox1.Checked)
            {
                // async
                try
                {
                    client.BeginZDodaj(z1, z2, ZDodajCallback, client);
                }
                catch(CommunicationException ex)
                {
                    MessageBox.Show(ex.Message, "Błąd komunikacji.", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                try
                {
                    client.Open();
                    z = client.ZDodaj(z1, z2);
                    client.Close();
                    textResponse.Text = ZespolonaToString(z);
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show(ex.Message, "Błąd komunikacji.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    client.Abort();
                }
            }
        }

        private void btMinus_Click(object sender, EventArgs e)
        {
            double.TryParse(textReal1.Text, out z1r);
            double.TryParse(textImag1.Text, out z1i);
            double.TryParse(textReal2.Text, out z2r);
            double.TryParse(textImag2.Text, out z2i);

            z1 = new ServiceReference.Zespolone();
            z1.R = z1r;
            z1.I = z1i;

            z2 = new ServiceReference.Zespolone();
            z2.R = z2r;
            z2.I = z2i;

            ServiceReference.Service1Client client = new ServiceReference.Service1Client();

            if (checkBox1.Checked)
            {
                // async
                try
                {
                    client.BeginZMinus(z1, z2, ZMinusCallback, client);
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show(ex.Message, "Błąd komunikacji.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                try
                {
                    client.Open();
                    z = client.ZMinus(z1, z2);
                    client.Close();
                    textResponse.Text = ZespolonaToString(z);
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show(ex.Message, "Błąd komunikacji.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    client.Abort();
                }
            }
        }

        private void btMnoz_Click(object sender, EventArgs e)
        {
            double.TryParse(textReal1.Text, out z1r);
            double.TryParse(textImag1.Text, out z1i);
            double.TryParse(textReal2.Text, out z2r);
            double.TryParse(textImag2.Text, out z2i);

            z1 = new ServiceReference.Zespolone();
            z1.R = z1r;
            z1.I = z1i;

            z2 = new ServiceReference.Zespolone();
            z2.R = z2r;
            z2.I = z2i;

            ServiceReference.Service1Client client = new ServiceReference.Service1Client();

            if (checkBox1.Checked)
            {
                // async
                try
                {
                    client.BeginZMnoz(z1, z2, ZMnozCallback, client);
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show(ex.Message, "Błąd komunikacji.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    client.Open();
                    z = client.ZMnoz(z1, z2);
                    client.Close();
                    textResponse.Text = ZespolonaToString(z);
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show(ex.Message, "Błąd komunikacji.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    client.Abort();
                }
            }
        }

        private void btDziel_Click(object sender, EventArgs e)
        {
            double.TryParse(textReal1.Text, out z1r);
            double.TryParse(textImag1.Text, out z1i);
            double.TryParse(textReal2.Text, out z2r);
            double.TryParse(textImag2.Text, out z2i);

            z1 = new ServiceReference.Zespolone();
            z1.R = z1r;
            z1.I = z1i;

            z2 = new ServiceReference.Zespolone();
            z2.R = z2r;
            z2.I = z2i;

            ServiceReference.Service1Client client = new ServiceReference.Service1Client();

            if (checkBox1.Checked)
            {
                // async
                try
                {
                    client.BeginZDziel(z1, z2, ZDzielCallback, client);
                }
                catch (FaultException<ServiceReference.AboutException> ex)
                {
                    MessageBox.Show(ex.Detail.Message, "Błąd usługi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    client.Open();
                    z = client.ZDziel(z1, z2);
                    client.Close();
                    textResponse.Text = ZespolonaToString(z);
                }

                catch (FaultException<ServiceReference.AboutException> ex)
                {
                    MessageBox.Show(ex.Detail.Message, "Błąd usługi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    client.Abort();
                }
            }
        }

        // toString do wypisywania wyniku liczby zespolonej
        private string ZespolonaToString(ServiceReference.Zespolone z)
        {
            string wynik = z.R.ToString();

            if (z.I > 0)
                wynik += "+";
            if (z.I == 0)
                return wynik;

            return wynik += z.I.ToString() + "i";
        }

        private void ZDodajCallback(IAsyncResult result)
        {
            ServiceReference.Service1Client client = (ServiceReference.Service1Client)result.AsyncState;

            try
            {
                z = client.EndZDodaj(result);
                client.Close();
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show(ex.Message, "Błąd komunikacji.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client.Abort();
            }

            textResponse.BeginInvoke((Action)(() => textResponse.Text = ZespolonaToString(z)));
        }

        private void ZMinusCallback(IAsyncResult result)
        {
            ServiceReference.Service1Client client = (ServiceReference.Service1Client)result.AsyncState;

            try
            {
                z = client.EndZMinus(result);
                client.Close();
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show(ex.Message, "Błąd komunikacji.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client.Abort();
            }

            textResponse.BeginInvoke((Action)(() => textResponse.Text = ZespolonaToString(z)));
        }

        private void ZMnozCallback(IAsyncResult result)
        {
            ServiceReference.Service1Client client = (ServiceReference.Service1Client)result.AsyncState;

            try
            {
                z = client.EndZMnoz(result);
                client.Close();
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show(ex.Message, "Błąd komunikacji.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client.Abort();
            }

            textResponse.BeginInvoke((Action)(() => textResponse.Text = ZespolonaToString(z)));
        }

        private void ZDzielCallback(IAsyncResult result)
        {
            ServiceReference.Service1Client client = (ServiceReference.Service1Client)result.AsyncState;

            try
            {
                z = client.EndZDziel(result);
                client.Close();
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show(ex.Message, "Błąd komunikacji.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                client.Abort();
            }

            textResponse.BeginInvoke((Action)(() => textResponse.Text = ZespolonaToString(z)));
        }
    }
}
