using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001B8 RID: 440
	public class IntProperty : ComparablePropertyDefinition<int>
	{
		// Token: 0x06000E84 RID: 3716 RVA: 0x00023ACC File Offset: 0x00021CCC
		public IntProperty(string name, int? defaultValue)
			: base(name, defaultValue)
		{
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00023AD6 File Offset: 0x00021CD6
		public IntProperty(string name, int? defaultValue, int? minimum, int? maximum)
			: base(name, defaultValue, minimum, maximum)
		{
		}
	}
}
