using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace UslugiSieciowe
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Zespolone ZDodaj(Zespolone z1, Zespolone z2);

        [OperationContract]
        Zespolone ZMinus(Zespolone z1, Zespolone z2);

        [OperationContract]
        Zespolone ZMnoz(Zespolone z1, Zespolone z2);

        [OperationContract]
        [FaultContract(typeof(AboutException))]
        Zespolone ZDziel(Zespolone z1, Zespolone z2);
    }

    [DataContract]
    public class Zespolone
    {
        double r, i;

        [DataMember]
        public double R
        {
            get { return r; }

            set { r = value; }
        }

        [DataMember]
        public double I
        {
            get { return i; }

            set { i = value; }
        }


        public Zespolone(double r, double i)
        {
            this.r = r;
            this.i = i;
        }

        public string ToString()
        {
            string wynik = R.ToString();

            if (I > 0)
                wynik += "+";
            if (I == 0)
                return wynik;

            return wynik += I.ToString() + "i";
        }

        public static Zespolone operator +(Zespolone z1, Zespolone z2)
        {
            return new Zespolone(z1.r + z2.r, z1.i + z2.i);
        }

        public static Zespolone operator -(Zespolone z1, Zespolone z2)
        {
            return new Zespolone(z1.r - z2.r, z1.i - z2.i);
        }

        public static Zespolone operator *(Zespolone z1, Zespolone z2)
        {
            return new Zespolone((z1.r * z2.r) - (z1.i * z2.i), (z1.r * z2.i) + (z2.r * z1.i));
        }

        public static Zespolone operator /(Zespolone z1, Zespolone z2)
        {
            double dzielnik = (Math.Pow(z2.r, 2) + Math.Pow(z2.i, 2));

            if (dzielnik == 0)
            {
                AboutException abex = new AboutException(z1, z2);
                throw new FaultException<AboutException>(abex, new FaultReason("błąd"));
            }

            Zespolone z = new Zespolone((z1.r * z2.r) + (z1.i * z2.i), (z2.r * z1.i) - (z1.r * z2.i));
            return new Zespolone(z.r / dzielnik, z.i / dzielnik);
        }
    }

    [DataContract]
    public class AboutException
    {
        Zespolone z1, z2;
        string message;

        [DataMember]
        public string Message { get { return message; } set { message = value; } }

        public AboutException(Zespolone z1, Zespolone z2)
        {
            this.z1 = z1;
            this.z2 = z2;
            message = "Wynik dzielenia jest nie poprawny, ponieważ dzielnikiem liczb zespolonych " + z1.ToString() + " i " + z2.ToString() + " jest liczba równa 0. Sprawdź równanie";
        }
    }
}
