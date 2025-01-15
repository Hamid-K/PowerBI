using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000735 RID: 1845
	[Serializable]
	internal sealed class ListInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x06006680 RID: 26240 RVA: 0x001917AB File Offset: 0x0018F9AB
		internal ListInstanceInfo(ReportProcessing.ProcessingContext pc, List reportItemDef, ListInstance owner)
			: base(pc, reportItemDef, owner, true)
		{
			this.m_noRows = pc.ReportRuntime.EvaluateDataRegionNoRowsExpression(reportItemDef, reportItemDef.ObjectType, reportItemDef.Name, "NoRows");
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x001917DA File Offset: 0x0018F9DA
		internal ListInstanceInfo(List reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x1700243B RID: 9275
		// (get) Token: 0x06006682 RID: 26242 RVA: 0x001917E3 File Offset: 0x0018F9E3
		// (set) Token: 0x06006683 RID: 26243 RVA: 0x001917EB File Offset: 0x0018F9EB
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

		// Token: 0x06006684 RID: 26244 RVA: 0x001917F4 File Offset: 0x0018F9F4
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.NoRows, Token.String)
			});
		}

		// Token: 0x04003301 RID: 13057
		private string m_noRows;
	}
}
