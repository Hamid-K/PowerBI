using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000139 RID: 313
	internal sealed class UShortNormalizer : Normalizer
	{
		// Token: 0x06001869 RID: 6249 RVA: 0x00065E04 File Offset: 0x00064004
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((ushort)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00065E40 File Offset: 0x00064040
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[2];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt16(array, 0));
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600186B RID: 6251 RVA: 0x00065DFF File Offset: 0x00063FFF
		internal override int Size
		{
			get
			{
				return 2;
			}
		}
	}
}
