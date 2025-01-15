using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003E4 RID: 996
	public sealed class MIMETypeExpr : MIMEType
	{
		// Token: 0x06001FB3 RID: 8115 RVA: 0x0007EC35 File Offset: 0x0007CE35
		public void Set(MIMEType mimetype)
		{
			base.Copy(mimetype);
		}
	}
}
