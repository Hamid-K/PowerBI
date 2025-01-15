using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000C2 RID: 194
	public interface IValidationScope
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B3A RID: 2874
		string ObjectType { get; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B3B RID: 2875
		string ObjectID { get; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B3C RID: 2876
		string ObjectName { get; }
	}
}
