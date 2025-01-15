using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200071E RID: 1822
	[Serializable]
	internal sealed class NonComputedUniqueNames
	{
		// Token: 0x060065B1 RID: 26033 RVA: 0x00190056 File Offset: 0x0018E256
		private NonComputedUniqueNames(int uniqueName, NonComputedUniqueNames[] childrenUniqueNames)
		{
			this.m_uniqueName = uniqueName;
			this.m_childrenUniqueNames = childrenUniqueNames;
		}

		// Token: 0x060065B2 RID: 26034 RVA: 0x0019006C File Offset: 0x0018E26C
		internal NonComputedUniqueNames()
		{
		}

		// Token: 0x17002401 RID: 9217
		// (get) Token: 0x060065B3 RID: 26035 RVA: 0x00190074 File Offset: 0x0018E274
		// (set) Token: 0x060065B4 RID: 26036 RVA: 0x0019007C File Offset: 0x0018E27C
		internal int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		// Token: 0x17002402 RID: 9218
		// (get) Token: 0x060065B5 RID: 26037 RVA: 0x00190085 File Offset: 0x0018E285
		// (set) Token: 0x060065B6 RID: 26038 RVA: 0x0019008D File Offset: 0x0018E28D
		internal NonComputedUniqueNames[] ChildrenUniqueNames
		{
			get
			{
				return this.m_childrenUniqueNames;
			}
			set
			{
				this.m_childrenUniqueNames = value;
			}
		}

		// Token: 0x060065B7 RID: 26039 RVA: 0x00190098 File Offset: 0x0018E298
		internal static NonComputedUniqueNames[] CreateNonComputedUniqueNames(ReportProcessing.ProcessingContext pc, ReportItemCollection reportItemsDef)
		{
			if (reportItemsDef == null || pc == null)
			{
				return null;
			}
			ReportItemList nonComputedReportItems = reportItemsDef.NonComputedReportItems;
			if (nonComputedReportItems == null)
			{
				return null;
			}
			if (nonComputedReportItems.Count == 0)
			{
				return null;
			}
			NonComputedUniqueNames[] array = new NonComputedUniqueNames[nonComputedReportItems.Count];
			for (int i = 0; i < nonComputedReportItems.Count; i++)
			{
				array[i] = NonComputedUniqueNames.CreateNonComputedUniqueNames(pc, nonComputedReportItems[i]);
			}
			return array;
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x001900F4 File Offset: 0x0018E2F4
		internal static NonComputedUniqueNames CreateNonComputedUniqueNames(ReportProcessing.ProcessingContext pc, ReportItem reportItemDef)
		{
			if (reportItemDef == null || pc == null)
			{
				return null;
			}
			NonComputedUniqueNames[] array = null;
			if (reportItemDef is Rectangle)
			{
				array = NonComputedUniqueNames.CreateNonComputedUniqueNames(pc, ((Rectangle)reportItemDef).ReportItems);
			}
			return new NonComputedUniqueNames(pc.CreateUniqueName(), array);
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x00190134 File Offset: 0x0018E334
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32),
				new MemberInfo(MemberName.ChildrenUniqueNames, Token.Array, ObjectType.NonComputedUniqueNames)
			});
		}

		// Token: 0x040032CD RID: 13005
		private int m_uniqueName;

		// Token: 0x040032CE RID: 13006
		private NonComputedUniqueNames[] m_childrenUniqueNames;
	}
}
