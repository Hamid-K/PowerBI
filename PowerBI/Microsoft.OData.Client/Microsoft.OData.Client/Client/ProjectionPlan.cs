using System;
using Microsoft.OData.Client.Materialization;

namespace Microsoft.OData.Client
{
	// Token: 0x0200005F RID: 95
	internal class ProjectionPlan
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000B690 File Offset: 0x00009890
		// (set) Token: 0x06000303 RID: 771 RVA: 0x0000B698 File Offset: 0x00009898
		internal Type LastSegmentType { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000B6A1 File Offset: 0x000098A1
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000B6A9 File Offset: 0x000098A9
		internal Func<object, object, Type, object> Plan { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000B6B2 File Offset: 0x000098B2
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000B6BA File Offset: 0x000098BA
		internal Type ProjectedType { get; set; }

		// Token: 0x06000308 RID: 776 RVA: 0x0000B6C3 File Offset: 0x000098C3
		internal object Run(ODataEntityMaterializer materializer, ODataResource entry, Type expectedType)
		{
			return this.Plan(materializer, entry, expectedType);
		}
	}
}
