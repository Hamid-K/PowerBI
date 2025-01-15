using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
	// Token: 0x020002C3 RID: 707
	internal abstract class Parameter
	{
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060015E5 RID: 5605
		internal abstract object Value { get; }

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060015E6 RID: 5606
		internal abstract object Label { get; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060015E7 RID: 5607
		internal abstract int Count { get; }

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060015E8 RID: 5608
		internal abstract bool IsMultiValue { get; }
	}
}
