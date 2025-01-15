using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003D9 RID: 985
	public class StringExprPropertyDef : StringPropertyDef, IExpressionDef
	{
		// Token: 0x06001F62 RID: 8034 RVA: 0x0007E507 File Offset: 0x0007C707
		public StringExprPropertyDef(string name, string defaultValue)
			: base(name, defaultValue)
		{
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x000297A6 File Offset: 0x000279A6
		public ValidationResult Validate(string expr, ValidationContext vc)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
