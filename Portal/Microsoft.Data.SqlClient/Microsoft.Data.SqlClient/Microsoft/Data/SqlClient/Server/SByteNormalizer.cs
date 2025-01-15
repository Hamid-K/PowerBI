using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000136 RID: 310
	internal sealed class SByteNormalizer : Normalizer
	{
		// Token: 0x0600185D RID: 6237 RVA: 0x00065CA8 File Offset: 0x00063EA8
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			sbyte b = (sbyte)base.GetValue(fi, obj);
			byte b2 = (byte)b;
			if (!this._skipNormalize)
			{
				b2 ^= 128;
			}
			s.WriteByte(b2);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00065CE0 File Offset: 0x00063EE0
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			if (!this._skipNormalize)
			{
				b ^= 128;
			}
			sbyte b2 = (sbyte)b;
			base.SetValue(fi, recvr, b2);
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal override int Size
		{
			get
			{
				return 1;
			}
		}
	}
}
