using System;
using Microsoft.InfoNav;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000157 RID: 343
	internal sealed class GeneratedEntityDeclaration
	{
		// Token: 0x06000C9D RID: 3229 RVA: 0x000343A1 File Offset: 0x000325A1
		internal GeneratedEntityDeclaration(string planName, IConceptualEntity entity, GeneratedColumnMap columnMap)
		{
			this.PlanName = planName;
			this.Entity = entity;
			this.ColumnMap = columnMap;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x000343BE File Offset: 0x000325BE
		internal string PlanName { get; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x000343C6 File Offset: 0x000325C6
		internal IConceptualEntity Entity { get; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x000343CE File Offset: 0x000325CE
		internal GeneratedColumnMap ColumnMap { get; }
	}
}
