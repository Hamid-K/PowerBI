using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200059E RID: 1438
	[DataContract]
	internal sealed class Limit
	{
		// Token: 0x060051F9 RID: 20985 RVA: 0x0015A433 File Offset: 0x00158633
		internal Limit(string id, LimitOperator op, string targetName, string within)
		{
			this.m_id = id;
			this.m_operator = op;
			this.m_target = targetName;
			this.m_within = within;
		}

		// Token: 0x17001E83 RID: 7811
		// (get) Token: 0x060051FA RID: 20986 RVA: 0x0015A458 File Offset: 0x00158658
		internal string ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001E84 RID: 7812
		// (get) Token: 0x060051FB RID: 20987 RVA: 0x0015A460 File Offset: 0x00158660
		internal string Target
		{
			get
			{
				return this.m_target;
			}
		}

		// Token: 0x17001E85 RID: 7813
		// (get) Token: 0x060051FC RID: 20988 RVA: 0x0015A468 File Offset: 0x00158668
		internal LimitOperator Operator
		{
			get
			{
				return this.m_operator;
			}
		}

		// Token: 0x17001E86 RID: 7814
		// (get) Token: 0x060051FD RID: 20989 RVA: 0x0015A470 File Offset: 0x00158670
		internal string Within
		{
			get
			{
				return this.m_within;
			}
		}

		// Token: 0x04002968 RID: 10600
		[DataMember(Name = "ID", Order = 1)]
		private readonly string m_id;

		// Token: 0x04002969 RID: 10601
		[DataMember(Name = "LimitOperator", Order = 2)]
		private readonly LimitOperator m_operator;

		// Token: 0x0400296A RID: 10602
		[DataMember(Name = "Target", Order = 3)]
		private readonly string m_target;

		// Token: 0x0400296B RID: 10603
		[DataMember(Name = "Within", Order = 4)]
		private readonly string m_within;
	}
}
