using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200013B RID: 315
	internal sealed class UIntNormalizer : Normalizer
	{
		// Token: 0x06001871 RID: 6257 RVA: 0x00065F28 File Offset: 0x00064128
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte[] bytes = BitConverter.GetBytes((uint)base.GetValue(fi, obj));
			if (!this._skipNormalize)
			{
				Array.Reverse(bytes);
			}
			s.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x00065F64 File Offset: 0x00064164
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte[] array = new byte[4];
			s.Read(array, 0, array.Length);
			if (!this._skipNormalize)
			{
				Array.Reverse(array);
			}
			base.SetValue(fi, recvr, BitConverter.ToUInt32(array, 0));
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x00065F23 File Offset: 0x00064123
		internal override int Size
		{
			get
			{
				return 4;
			}
		}
	}
}
