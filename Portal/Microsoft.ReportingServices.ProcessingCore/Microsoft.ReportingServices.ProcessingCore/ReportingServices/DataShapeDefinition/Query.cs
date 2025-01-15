using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x020005A3 RID: 1443
	[DataContract]
	internal sealed class Query
	{
		// Token: 0x06005209 RID: 21001 RVA: 0x0015A4EC File Offset: 0x001586EC
		internal Query(string dataSourceId, string commandText)
		{
			this.m_dataSourceId = dataSourceId;
			this.m_commandText = commandText;
		}

		// Token: 0x17001E8A RID: 7818
		// (get) Token: 0x0600520A RID: 21002 RVA: 0x0015A502 File Offset: 0x00158702
		internal string DataSourceId
		{
			get
			{
				return this.m_dataSourceId;
			}
		}

		// Token: 0x17001E8B RID: 7819
		// (get) Token: 0x0600520B RID: 21003 RVA: 0x0015A50A File Offset: 0x0015870A
		internal string CommandText
		{
			get
			{
				return this.m_commandText;
			}
		}

		// Token: 0x0400296F RID: 10607
		[DataMember(Name = "DataSourceId", Order = 1)]
		private readonly string m_dataSourceId;

		// Token: 0x04002970 RID: 10608
		[DataMember(Name = "CommandText", Order = 2)]
		private readonly string m_commandText;
	}
}
