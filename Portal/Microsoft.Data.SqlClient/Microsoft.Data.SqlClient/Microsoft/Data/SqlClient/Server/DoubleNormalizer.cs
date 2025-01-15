using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200013F RID: 319
	internal sealed class DoubleNormalizer : Normalizer
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x00066198 File Offset: 0x00064398
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			double num = (double)base.GetValue(fi, obj);
			byte[] bytes = BitConverter.GetBytes(num);
			if (!this._skipNormalize)
			{
				Array.Reverse(bytes);
				if ((bytes[0] & 128) == 0)
				{
					byte[] array = bytes;
					int num2 = 0;
					array[num2] ^= 128;
				}
				else if (num < 0.0)
				{
					base.FlipAllBits(bytes);
				}
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x00066204 File Offset: 0x00064404
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				if ((array[0] & 128) > 0)
				{
					byte[] array2 = array;
					int num = 0;
					array2[num] ^= 128;
				}
				else
				{
					base.FlipAllBits(array);
				}
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToDouble(array, 0));
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0002D263 File Offset: 0x0002B463
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
