using System;

namespace Microsoft.ReportingServices.Modeling.Linguistics
{
	// Token: 0x020000C7 RID: 199
	public interface ILinguisticInfo
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B51 RID: 2897
		// (set) Token: 0x06000B52 RID: 2898
		string SingularName { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B53 RID: 2899
		bool IsSingularNameSet { get; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B54 RID: 2900
		// (set) Token: 0x06000B55 RID: 2901
		string PluralName { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B56 RID: 2902
		bool IsPluralNameSet { get; }
	}
}
