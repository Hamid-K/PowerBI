using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200013D RID: 317
	internal sealed class ULongNormalizer : Normalizer
	{
		// Token: 0x06001879 RID: 6265 RVA: 0x00066048 File Offset: 0x00064248
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((ulong)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00066084 File Offset: 0x00064284
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[8];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt64(array, 0));
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0002D263 File Offset: 0x0002B463
		internal override int Size
		{
			get
			{
				return 8;
			}
		}
	}
}
