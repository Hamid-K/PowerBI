using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003D8 RID: 984
	public class EnumExprPropertyDef : EnumPropertyDef, IExpressionDef
	{
		// Token: 0x06001F60 RID: 8032 RVA: 0x0007E4FC File Offset: 0x0007C6FC
		public EnumExprPropertyDef(string name, string defaultValue, string[] values)
			: base(name, defaultValue, values)
		{
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x000297A6 File Offset: 0x000279A6
		public ValidationResult Validate(string expr, ValidationContext vc)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
