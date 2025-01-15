using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000135 RID: 309
	internal sealed class BooleanNormalizer : Normalizer
	{
		// Token: 0x06001859 RID: 6233 RVA: 0x00065C50 File Offset: 0x00063E50
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			bool flag = (bool)base.GetValue(fi, obj);
			s.WriteByte((flag > false) ? 1 : 0);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00065C78 File Offset: 0x00063E78
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			byte b = (byte)s.ReadByte();
			base.SetValue(fi, recvr, b == 1);
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal override int Size
		{
			get
			{
				return 1;
			}
		}
	}
}
