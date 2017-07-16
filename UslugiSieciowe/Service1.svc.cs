namespace UslugiSieciowe
{
    public class Service1 : IService1
    {
        public Zespolone ZDodaj(Zespolone z1, Zespolone z2)
        {
            return z1 + z2;
        }

        public Zespolone ZMinus(Zespolone z1, Zespolone z2)
        {
            return z1 - z2;
        }

        public Zespolone ZMnoz(Zespolone z1, Zespolone z2)
        {
            return z1 * z2;
        }

        public Zespolone ZDziel(Zespolone z1, Zespolone z2)
        {
            //Thread.Sleep(5000);
            return z1 / z2;
        }
    }
}
