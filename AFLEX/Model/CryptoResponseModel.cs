using AFLEX.Enumerations;

namespace AFLEX.Model
{
    public class CryptoResponseModel
    {
        public CryptoStatusCodeEnum Status { get; set; }
        public string Message { get; set; }
        public string Value { get; set; }
    }
}
