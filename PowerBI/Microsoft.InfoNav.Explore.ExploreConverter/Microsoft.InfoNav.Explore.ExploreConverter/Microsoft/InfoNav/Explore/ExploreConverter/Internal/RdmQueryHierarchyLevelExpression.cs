using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200007F RID: 127
	internal sealed class RdmQueryHierarchyLevelExpression : IRdmQueryExpression
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000C498 File Offset: 0x0000A698
		internal RdmQueryHierarchyLevelExpression(string hierarchy, string hierarchyLevel, IRdmQueryExpression instance)
		{
			this._hierarchy = hierarchy;
			this._hierarchyLevel = hierarchyLevel;
			this._instance = instance;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000C4B5 File Offset: 0x0000A6B5
		internal string Hierarchy
		{
			get
			{
				return this._hierarchy;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000C4BD File Offset: 0x0000A6BD
		internal string HierarchyLevel
		{
			get
			{
				return this._hierarchyLevel;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000C4C5 File Offset: 0x0000A6C5
		internal IRdmQueryExpression Instance
		{
			get
			{
				return this._instance;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C4CD File Offset: 0x0000A6CD
		public void FindFormulaComponents(FormulaParserContext context)
		{
			context.ContainerName = this._hierarchy;
			context.PropertyName = this._hierarchyLevel;
			context.EdmReferenceKind = new FormulaEdmReferenceKind?(FormulaEdmReferenceKind.Hierarchy);
			if (this._instance != null)
			{
				this._instance.FindFormulaComponents(context);
			}
		}

		// Token: 0x04000199 RID: 409
		private readonly string _hierarchy;

		// Token: 0x0400019A RID: 410
		private readonly string _hierarchyLevel;

		// Token: 0x0400019B RID: 411
		private readonly IRdmQueryExpression _instance;
	}
}
