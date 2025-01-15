using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000722 RID: 1826
	[Serializable]
	internal sealed class ReportItemColInstanceInfo : InstanceInfo
	{
		// Token: 0x060065E9 RID: 26089 RVA: 0x001908C4 File Offset: 0x0018EAC4
		internal ReportItemColInstanceInfo(ReportProcessing.ProcessingContext pc, ReportItemCollection reportItemsDef, ReportItemColInstance owner)
		{
			if (pc != null)
			{
				this.m_childrenNonComputedUniqueNames = owner.ChildrenNonComputedUniqueNames;
				if (pc.ChunkManager != null && !pc.DelayAddingInstanceInfo)
				{
					if (reportItemsDef.FirstInstance)
					{
						pc.ChunkManager.AddInstanceToFirstPage(this, owner, pc.InPageSection);
						reportItemsDef.FirstInstance = false;
						return;
					}
					pc.ChunkManager.AddInstance(this, owner, pc.InPageSection);
				}
			}
		}

		// Token: 0x060065EA RID: 26090 RVA: 0x0019092C File Offset: 0x0018EB2C
		internal ReportItemColInstanceInfo()
		{
		}

		// Token: 0x17002412 RID: 9234
		// (get) Token: 0x060065EB RID: 26091 RVA: 0x00190934 File Offset: 0x0018EB34
		// (set) Token: 0x060065EC RID: 26092 RVA: 0x0019093C File Offset: 0x0018EB3C
		internal NonComputedUniqueNames[] ChildrenNonComputedUniqueNames
		{
			get
			{
				return this.m_childrenNonComputedUniqueNames;
			}
			set
			{
				this.m_childrenNonComputedUniqueNames = value;
			}
		}

		// Token: 0x060065ED RID: 26093 RVA: 0x00190948 File Offset: 0x0018EB48
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.ChildrenNonComputedUniqueNames, Token.Array, ObjectType.NonComputedUniqueNames)
			});
		}

		// Token: 0x040032DD RID: 13021
		private NonComputedUniqueNames[] m_childrenNonComputedUniqueNames;
	}
}
