using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000585 RID: 1413
	[DataContract]
	internal sealed class ConnectionProperties
	{
		// Token: 0x06005173 RID: 20851 RVA: 0x00159A91 File Offset: 0x00157C91
		internal ConnectionProperties(string dataProvider, string connectionString)
		{
			this.m_dataProvider = dataProvider;
			this.m_connectionString = connectionString;
		}

		// Token: 0x06005174 RID: 20852 RVA: 0x00159AA7 File Offset: 0x00157CA7
		internal ConnectionProperties(string dataProvider, string connectionString, bool integratedSecurity)
			: this(dataProvider, connectionString)
		{
			this.m_integratedSecurity = integratedSecurity;
		}

		// Token: 0x17001E36 RID: 7734
		// (get) Token: 0x06005175 RID: 20853 RVA: 0x00159AB8 File Offset: 0x00157CB8
		internal string DataProvider
		{
			get
			{
				return this.m_dataProvider;
			}
		}

		// Token: 0x17001E37 RID: 7735
		// (get) Token: 0x06005176 RID: 20854 RVA: 0x00159AC0 File Offset: 0x00157CC0
		internal string ConnectionString
		{
			get
			{
				return this.m_connectionString;
			}
		}

		// Token: 0x17001E38 RID: 7736
		// (get) Token: 0x06005177 RID: 20855 RVA: 0x00159AC8 File Offset: 0x00157CC8
		internal bool IntegratedSecurity
		{
			get
			{
				return this.m_integratedSecurity;
			}
		}

		// Token: 0x04002918 RID: 10520
		[DataMember(Name = "Name", Order = 1)]
		private readonly string m_dataProvider;

		// Token: 0x04002919 RID: 10521
		[DataMember(Name = "ConnectionString", Order = 2)]
		private readonly string m_connectionString;

		// Token: 0x0400291A RID: 10522
		[DataMember(Name = "IntegratedSecurity", Order = 3)]
		private readonly bool m_integratedSecurity;
	}
}
