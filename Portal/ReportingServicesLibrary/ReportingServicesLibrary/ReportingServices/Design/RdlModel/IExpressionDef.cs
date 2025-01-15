using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003D6 RID: 982
	public interface IExpressionDef
	{
		// Token: 0x06001F5D RID: 8029
		ValidationResult Validate(string expr, ValidationContext vc);
	}
}
