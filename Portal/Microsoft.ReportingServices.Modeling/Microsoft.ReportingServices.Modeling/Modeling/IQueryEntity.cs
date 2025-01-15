using System;
using Microsoft.ReportingServices.Modeling.Linguistics;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000BA RID: 186
	public interface IQueryEntity : IValidationScope
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A5B RID: 2651
		ModelEntity ModelEntity { get; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A5C RID: 2652
		string Name { get; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A5D RID: 2653
		ILinguisticInfo Linguistics { get; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A5E RID: 2654
		SemanticModel Model { get; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A5F RID: 2655
		IQueryEntity InheritsFrom { get; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A60 RID: 2656
		bool DisjointInheritance { get; }

		// Token: 0x06000A61 RID: 2657
		IQueryEntity GetInheritanceRoot();

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A62 RID: 2658
		bool IsInvalidRefTarget { get; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A63 RID: 2659
		bool EntityRefIsNullable { get; }
	}
}
