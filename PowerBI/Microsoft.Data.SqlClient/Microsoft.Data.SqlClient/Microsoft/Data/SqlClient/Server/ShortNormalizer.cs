using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000138 RID: 312
	internal sealed class ShortNormalizer : Normalizer
	{
		// Token: 0x06001865 RID: 6245 RVA: 0x00065D60 File Offset: 0x00063F60
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((short)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00065DAC File Offset: 0x00063FAC
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[2];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt16(array, 0));
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x00065DFF File Offset: 0x00063FFF
		internal override int Size
		{
			get
			{
				return 2;
			}
		}
	}
}
