using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000731 RID: 1841
	[Serializable]
	internal sealed class SubReportInstanceInfo : ReportItemInstanceInfo
	{
		// Token: 0x06006661 RID: 26209 RVA: 0x00191488 File Offset: 0x0018F688
		internal SubReportInstanceInfo(ReportProcessing.ProcessingContext pc, SubReport reportItemDef, SubReportInstance owner, int index)
			: base(pc, reportItemDef, owner, index)
		{
			this.m_noRows = pc.ReportRuntime.EvaluateSubReportNoRowsExpression(reportItemDef, reportItemDef.Name, "NoRows");
		}

		// Token: 0x06006662 RID: 26210 RVA: 0x001914B2 File Offset: 0x0018F6B2
		internal SubReportInstanceInfo(SubReport reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x17002433 RID: 9267
		// (get) Token: 0x06006663 RID: 26211 RVA: 0x001914BB File Offset: 0x0018F6BB
		// (set) Token: 0x06006664 RID: 26212 RVA: 0x001914C3 File Offset: 0x0018F6C3
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

		// Token: 0x06006665 RID: 26213 RVA: 0x001914CC File Offset: 0x0018F6CC
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.NoRows, Token.String)
			});
		}

		// Token: 0x040032FA RID: 13050
		private string m_noRows;
	}
}
