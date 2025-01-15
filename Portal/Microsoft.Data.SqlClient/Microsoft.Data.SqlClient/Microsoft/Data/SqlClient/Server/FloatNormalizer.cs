using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200013E RID: 318
	internal sealed class FloatNormalizer : Normalizer
	{
		// Token: 0x0600187D RID: 6269 RVA: 0x000660C8 File Offset: 0x000642C8
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			float num = (float)base.GetValue(fi, obj);
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
				else if (num < 0f)
				{
					base.FlipAllBits(bytes);
				}
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00066130 File Offset: 0x00064330
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
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
			base.SetValue(fi, recvr, BitConverter.ToSingle(array, 0));
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00065F23 File Offset: 0x00064123
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
