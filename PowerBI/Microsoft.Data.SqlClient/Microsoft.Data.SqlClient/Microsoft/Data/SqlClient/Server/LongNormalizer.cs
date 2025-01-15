using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200013C RID: 316
	internal sealed class LongNormalizer : Normalizer
	{
		// Token: 0x06001875 RID: 6261 RVA: 0x00065FA8 File Offset: 0x000641A8
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((long)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00065FF4 File Offset: 0x000641F4
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt64(array, 0));
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0002D263 File Offset: 0x0002B463
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
