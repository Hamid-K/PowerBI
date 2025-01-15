using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003E3 RID: 995
	public class MIMEType : EnumString
	{
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x0007EBB0 File Offset: 0x0007CDB0
		public MIMEType.Enum Value
		{
			get
			{
				return (MIMEType.Enum)this.m_Key;
			}
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0007EBB8 File Offset: 0x0007CDB8
		public MIMEType()
		{
			if (EnumString.m_Dictionary == null)
			{
				EnumString.m_Dictionary = new Dictionary<int, string>();
				EnumString.m_Dictionary.Add(0, "image/bmp");
				EnumString.m_Dictionary.Add(2, "image/gif");
				EnumString.m_Dictionary.Add(1, "image/jpeg");
				EnumString.m_Dictionary.Add(3, "image/png");
				EnumString.m_Dictionary.Add(4, "image/x-png");
			}
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0007EC2C File Offset: 0x0007CE2C
		public void Set(MIMEType.Enum mimetype)
		{
			base.Set((int)mimetype);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0007EC35 File Offset: 0x0007CE35
		public void Set(MIMETypeExpr mimeTypeExpr)
		{
			base.Copy(mimeTypeExpr);
		}

		// Token: 0x0200051A RID: 1306
		public enum Enum
		{
			// Token: 0x04001283 RID: 4739
			Bmp,
			// Token: 0x04001284 RID: 4740
			Jpeg,
			// Token: 0x04001285 RID: 4741
			Gif,
			// Token: 0x04001286 RID: 4742
			Png,
			// Token: 0x04001287 RID: 4743
			Xpng,
			// Token: 0x04001288 RID: 4744
			Expression = -1,
			// Token: 0x04001289 RID: 4745
			Invalid = -2
		}
	}
}
