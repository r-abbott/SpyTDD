namespace Spy.Intercept
{
    public class Uplink
    {
        private readonly IDecryptor _decryptor;

        public Uplink(IDecryptor decryptor)
        {
            _decryptor = decryptor;
        }


    }
}
