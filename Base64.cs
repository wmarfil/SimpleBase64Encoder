using System.Text;

namespace Base64EncoderSimple
{
    public static class Base64
    {
        private const string encodingLookupTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/+";
        public static string EncodeSafe(byte[] source)
        {
            var outputLength = source.Length / 3 * 4;
            var sb = new StringBuilder(null, outputLength);
            var len = source.Length - 3;
            for (int i = 0; i <= len; i += 3)
            {
                sb.Append(encodingLookupTable[source[i] >> 2]);
                sb.Append(encodingLookupTable[((source[i] & 0b_0000_0011) << 4) | ((source[i + 1] & 0b_1111_0000) >> 4)]);
                sb.Append(encodingLookupTable[((source[i + 1] & 0b_0000_1111) << 2) | ((source[i + 2] & 0b_1100_0000) >> 6)]);
                sb.Append(encodingLookupTable[source[i + 2] & 0b_0011_1111]);
            }

            return sb.ToString();
        }

        public static string Encode(byte[] source)
        {
            if (source == null || source.Length == 0)
                return string.Empty;
            int length = source.Length;

            unsafe
            {
                fixed (char* _lookupTable = encodingLookupTable) // using a pointer to bypass range validation
                fixed (byte* _src = source)
                {
                    var outputLength = (length / 3) * 4;
                    char[] outputArr = new char[outputLength];

                    fixed (char* _output = outputArr)
                    {
                        char* output = _output;
                        for (int i = 0; i <= length - 3; i += 3)
                        {
                            *output++ = _lookupTable[_src[i] >> 2];
                            *output++ = _lookupTable[((_src[i] & 0b_0000_0011) << 4) | ((_src[i + 1] & 0b_1111_0000) >> 4)];
                            *output++ = _lookupTable[((_src[i + 1] & 0b_0000_1111) << 2) | ((_src[i + 2] & 0b_1100_0000) >> 6)];
                            *output++ = _lookupTable[_src[i + 2] & 0b_0011_1111];
                        }
                    }
                    return new string(outputArr, 0, outputLength);
                }
            }
        }
    }



}

