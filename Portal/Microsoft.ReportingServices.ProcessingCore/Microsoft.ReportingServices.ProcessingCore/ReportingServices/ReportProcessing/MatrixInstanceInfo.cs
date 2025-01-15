using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000739 RID: 1849
	[Serializable]
	internal sealed class MatrixInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x060066C6 RID: 26310 RVA: 0x001922BC File Offset: 0x001904BC
		internal MatrixInstanceInfo(ReportProcessing.ProcessingContext pc, Matrix reportItemDef, MatrixInstance owner)
			: base(pc, reportItemDef, owner, false)
		{
			if (0 < reportItemDef.CornerReportItems.Count && !reportItemDef.CornerReportItems.IsReportItemComputed(0))
			{
				this.m_cornerNonComputedNames = NonComputedUniqueNames.CreateNonComputedUniqueNames(pc, reportItemDef.CornerReportItems[0]);
			}
			reportItemDef.CornerNonComputedUniqueNames = this.m_cornerNonComputedNames;
			if (!pc.DelayAddingInstanceInfo)
			{
				if (reportItemDef.FirstInstance)
				{
					pc.ChunkManager.AddInstanceToFirstPage(this, owner, pc.InPageSection);
					reportItemDef.FirstInstance = false;
				}
				else
				{
					pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
				}
			}
			this.m_noRows = pc.ReportRuntime.EvaluateDataRegionNoRowsExpression(reportItemDef, reportItemDef.ObjectType, reportItemDef.Name, "NoRows");
		}

		// Token: 0x060066C7 RID: 26311 RVA: 0x00192375 File Offset: 0x00190575
		internal MatrixInstanceInfo(Matrix reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x17002456 RID: 9302
		// (get) Token: 0x060066C8 RID: 26312 RVA: 0x0019237E File Offset: 0x0019057E
		// (set) Token: 0x060066C9 RID: 26313 RVA: 0x00192386 File Offset: 0x00190586
		internal NonComputedUniqueNames CornerNonComputedNames
		{
			get
			{
				return this.m_cornerNonComputedNames;
			}
			set
			{
				this.m_cornerNonComputedNames = value;
			}
		}

		// Token: 0x17002457 RID: 9303
		// (get) Token: 0x060066CA RID: 26314 RVA: 0x0019238F File Offset: 0x0019058F
		// (set) Token: 0x060066CB RID: 26315 RVA: 0x00192397 File Offset: 0x00190597
		internal string NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		// Token: 0x060066CC RID: 26316 RVA: 0x001923A0 File Offset: 0x001905A0
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.CornerNonComputedNames, ObjectType.NonComputedUniqueNames),
				new MemberInfo(MemberName.NoRows, Token.String)
			});
		}

		// Token: 0x04003318 RID: 13080
		private NonComputedUniqueNames m_cornerNonComputedNames;

		// Token: 0x04003319 RID: 13081
		private string m_noRows;
	}
}
