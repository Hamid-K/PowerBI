using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003D0 RID: 976
	internal sealed class RestartDefinition
	{
		// Token: 0x0600277A RID: 10106 RVA: 0x000BAAE4 File Offset: 0x000B8CE4
		internal RestartDefinition(DataShapeMember dataMember, bool isTotal)
		{
			this.m_dataMember = dataMember;
			this.m_isTotal = isTotal;
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x000BAAFA File Offset: 0x000B8CFA
		public bool IsTotal
		{
			get
			{
				return this.m_isTotal;
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x000BAB02 File Offset: 0x000B8D02
		internal DataShapeMember DataMember
		{
			get
			{
				return this.m_dataMember;
			}
		}

		// Token: 0x04001684 RID: 5764
		private readonly DataShapeMember m_dataMember;

		// Token: 0x04001685 RID: 5765
		private readonly bool m_isTotal;
	}
}
