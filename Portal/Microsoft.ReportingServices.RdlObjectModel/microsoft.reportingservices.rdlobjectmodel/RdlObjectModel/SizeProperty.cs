using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001B9 RID: 441
	public class SizeProperty : ComparablePropertyDefinition<ReportSize>
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x00023AE3 File Offset: 0x00021CE3
		public SizeProperty(string name, ReportSize? defaultValue)
			: base(name, defaultValue)
		{
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00023AED File Offset: 0x00021CED
		public SizeProperty(string name, ReportSize? defaultValue, ReportSize? minimum, ReportSize? maximum)
			: base(name, defaultValue, minimum, maximum)
		{
		}
	}
}
