using System;
using System.Drawing;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003DA RID: 986
	public class ColorExprPropertyDef : ColorPropertyDef, IExpressionDef
	{
		// Token: 0x06001F64 RID: 8036 RVA: 0x0007E511 File Offset: 0x0007C711
		public ColorExprPropertyDef(string name, Color defaultValue)
			: base(name, defaultValue)
		{
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000297A6 File Offset: 0x000279A6
		public ValidationResult Validate(string expr, ValidationContext vc)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
