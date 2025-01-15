using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003C7 RID: 967
	internal sealed class DataShapeLimit
	{
		// Token: 0x06002733 RID: 10035 RVA: 0x000BA624 File Offset: 0x000B8824
		internal DataShapeLimit(string id, DataShapeLimitOperator op, string target, string resetTarget)
		{
			this.m_id = id;
			this.m_operator = op;
			this.m_target = target;
			this.m_within = resetTarget;
		}

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x000BA649 File Offset: 0x000B8849
		internal string ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x000BA651 File Offset: 0x000B8851
		internal DataShapeLimitOperator Operator
		{
			get
			{
				return this.m_operator;
			}
		}

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x000BA659 File Offset: 0x000B8859
		internal string Target
		{
			get
			{
				return this.m_target;
			}
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x000BA661 File Offset: 0x000B8861
		internal string Within
		{
			get
			{
				return this.m_within;
			}
		}

		// Token: 0x04001676 RID: 5750
		private readonly string m_id;

		// Token: 0x04001677 RID: 5751
		private readonly DataShapeLimitOperator m_operator;

		// Token: 0x04001678 RID: 5752
		private readonly string m_target;

		// Token: 0x04001679 RID: 5753
		private readonly string m_within;
	}
}
