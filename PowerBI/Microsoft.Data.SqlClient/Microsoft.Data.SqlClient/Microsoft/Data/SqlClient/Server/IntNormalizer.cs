using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200013A RID: 314
	internal sealed class IntNormalizer : Normalizer
	{
		// Token: 0x0600186D RID: 6253 RVA: 0x00065E84 File Offset: 0x00064084
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((int)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse(bytes);
				byte[] array = bytes;
				int num = 0;
				array[num] ^= 128;
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00065ED0 File Offset: 0x000640D0
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				byte[] array2 = array;
				int num = 0;
				array2[num] ^= 128;
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToInt32(array, 0));
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x00065F23 File Offset: 0x00064123
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
