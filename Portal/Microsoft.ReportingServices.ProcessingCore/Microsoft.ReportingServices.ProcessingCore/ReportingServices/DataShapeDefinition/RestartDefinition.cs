using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x020005A5 RID: 1445
	[DataContract]
	internal class RestartDefinition
	{
		// Token: 0x06005210 RID: 21008 RVA: 0x0015A54F File Offset: 0x0015874F
		internal RestartDefinition(string dataMemberId, bool isTotal)
		{
			this.m_dataMemberId = dataMemberId;
			this.m_isTotal = isTotal;
		}

		// Token: 0x17001E8F RID: 7823
		// (get) Token: 0x06005211 RID: 21009 RVA: 0x0015A565 File Offset: 0x00158765
		public bool IsTotal
		{
			get
			{
				return this.m_isTotal;
			}
		}

		// Token: 0x17001E90 RID: 7824
		// (get) Token: 0x06005212 RID: 21010 RVA: 0x0015A56D File Offset: 0x0015876D
		internal string DataMemberId
		{
			get
			{
				return this.m_dataMemberId;
			}
		}

		// Token: 0x04002974 RID: 10612
		[DataMember(Name = "DataMemberId", Order = 1)]
		private readonly string m_dataMemberId;

		// Token: 0x04002975 RID: 10613
		[DataMember(Name = "IsTotal", Order = 2)]
		private readonly bool m_isTotal;
	}
}
