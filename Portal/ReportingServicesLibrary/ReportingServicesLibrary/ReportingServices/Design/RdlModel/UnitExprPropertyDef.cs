using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003D7 RID: 983
	public class UnitExprPropertyDef : UnitPropertyDef, IExpressionDef
	{
		// Token: 0x06001F5E RID: 8030 RVA: 0x0007E4EF File Offset: 0x0007C6EF
		public UnitExprPropertyDef(string name, Unit min, Unit max, Unit defaultValue)
			: base(name, min, max, defaultValue)
		{
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x000297A6 File Offset: 0x000279A6
		public ValidationResult Validate(string expr, ValidationContext vc)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
