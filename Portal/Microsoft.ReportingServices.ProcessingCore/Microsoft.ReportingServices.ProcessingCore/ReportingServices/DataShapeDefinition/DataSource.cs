using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x02000590 RID: 1424
	[DataContract]
	internal sealed class DataSource
	{
		// Token: 0x060051BB RID: 20923 RVA: 0x0015A066 File Offset: 0x00158266
		internal DataSource(string id, Guid dataSourceInfoId)
		{
			this.m_id = id;
			this.m_dataSourceInfoId = dataSourceInfoId;
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x0015A07C File Offset: 0x0015827C
		internal DataSource(string id, ConnectionProperties connectionProperties)
			: this(id, Guid.Empty)
		{
			this.m_connectionProperties = connectionProperties;
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x0015A091 File Offset: 0x00158291
		internal DataSource(string id, string dataSourceReference)
			: this(id, Guid.Empty)
		{
			this.m_dataSourceReference = dataSourceReference;
		}

		// Token: 0x17001E5F RID: 7775
		// (get) Token: 0x060051BE RID: 20926 RVA: 0x0015A0A6 File Offset: 0x001582A6
		internal string ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001E60 RID: 7776
		// (get) Token: 0x060051BF RID: 20927 RVA: 0x0015A0AE File Offset: 0x001582AE
		internal ConnectionProperties ConnectionProperties
		{
			get
			{
				return this.m_connectionProperties;
			}
		}

		// Token: 0x17001E61 RID: 7777
		// (get) Token: 0x060051C0 RID: 20928 RVA: 0x0015A0B6 File Offset: 0x001582B6
		internal string DataSourceReference
		{
			get
			{
				return this.m_dataSourceReference;
			}
		}

		// Token: 0x17001E62 RID: 7778
		// (get) Token: 0x060051C1 RID: 20929 RVA: 0x0015A0BE File Offset: 0x001582BE
		// (set) Token: 0x060051C2 RID: 20930 RVA: 0x0015A0C6 File Offset: 0x001582C6
		internal Guid DataSourceInfoId
		{
			get
			{
				return this.m_dataSourceInfoId;
			}
			set
			{
				this.m_dataSourceInfoId = value;
			}
		}

		// Token: 0x04002944 RID: 10564
		[DataMember(Name = "ID", Order = 1)]
		private readonly string m_id;

		// Token: 0x04002945 RID: 10565
		[DataMember(Name = "DataSourceReference", Order = 2)]
		private readonly string m_dataSourceReference;

		// Token: 0x04002946 RID: 10566
		[DataMember(Name = "ConnectionProperties", Order = 3)]
		private readonly ConnectionProperties m_connectionProperties;

		// Token: 0x04002947 RID: 10567
		private Guid m_dataSourceInfoId;
	}
}
