using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001B7 RID: 439
	public class DoubleProperty : ComparablePropertyDefinition<double>
	{
		// Token: 0x06000E82 RID: 3714 RVA: 0x00023AB5 File Offset: 0x00021CB5
		public DoubleProperty(string name, double? defaultValue)
			: base(name, defaultValue)
		{
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00023ABF File Offset: 0x00021CBF
		public DoubleProperty(string name, double? defaultValue, double? minimum, double? maximum)
			: base(name, defaultValue, minimum, maximum)
		{
		}
	}
}
