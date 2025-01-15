using System;

namespace Microsoft.ReportingServices.Design
{
	// Token: 0x02000388 RID: 904
	public abstract class DefinitionBase
	{
		// Token: 0x06001DFC RID: 7676 RVA: 0x00005C88 File Offset: 0x00003E88
		public virtual ValidationResult Validate(object obj, ValidationContext vc)
		{
			return null;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x0007AD54 File Offset: 0x00078F54
		public ValidationResult Validate(object obj)
		{
			ValidationContext validationContext = new ValidationContext();
			return this.Validate(obj, validationContext);
		}
	}
}
