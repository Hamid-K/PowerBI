using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000137 RID: 311
	internal sealed class ByteNormalizer : Normalizer
	{
		// Token: 0x06001861 RID: 6241 RVA: 0x00065D18 File Offset: 0x00063F18
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			byte b = (byte)base.GetValue(fi, obj);
			s.WriteByte(b);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00065D3C File Offset: 0x00063F3C
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			base.SetValue(fi, recvr, b);
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal override int Size
		{
			get
			{
				return 1;
			}
		}
	}
}
