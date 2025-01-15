using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003C4 RID: 964
	public sealed class DataSource
	{
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x0007DF83 File Offset: 0x0007C183
		// (set) Token: 0x06001F0F RID: 7951 RVA: 0x0007DF8B File Offset: 0x0007C18B
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x0007DF94 File Offset: 0x0007C194
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x0007DFB5 File Offset: 0x0007C1B5
		[DefaultValue("")]
		[XmlElement("DataSourceID")]
		[DesignOnly(true)]
		public string _DataSourceID
		{
			get
			{
				return this.DataSourceID.ToString();
			}
			set
			{
				this.DataSourceID = new Guid(value);
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x0007DFC3 File Offset: 0x0007C1C3
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x0007DFE8 File Offset: 0x0007C1E8
		[Browsable(false)]
		[XmlIgnore]
		public Guid DataSourceID
		{
			get
			{
				if (this.m_DataSourceID == Guid.Empty)
				{
					this.m_DataSourceID = Guid.NewGuid();
				}
				return this.m_DataSourceID;
			}
			set
			{
				this.m_DataSourceID = value;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0007DFF1 File Offset: 0x0007C1F1
		// (set) Token: 0x06001F15 RID: 7957 RVA: 0x0007DFF9 File Offset: 0x0007C1F9
		[DefaultValue(false)]
		public bool Transaction
		{
			get
			{
				return this.m_transaction;
			}
			set
			{
				this.m_transaction = value;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0007E002 File Offset: 0x0007C202
		// (set) Token: 0x06001F17 RID: 7959 RVA: 0x0007E00A File Offset: 0x0007C20A
		[DefaultValue("")]
		public string DataSourceReference
		{
			get
			{
				return this.m_dataSourceReference;
			}
			set
			{
				this.m_dataSourceReference = value;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x0007E013 File Offset: 0x0007C213
		// (set) Token: 0x06001F19 RID: 7961 RVA: 0x0007E01B File Offset: 0x0007C21B
		public ConnectionProperties ConnectionProperties
		{
			get
			{
				return this.m_connectionProperties;
			}
			set
			{
				this.m_connectionProperties = value;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0007E024 File Offset: 0x0007C224
		// (set) Token: 0x06001F1B RID: 7963 RVA: 0x0007E031 File Offset: 0x0007C231
		[XmlIgnore]
		public string DataProvider
		{
			get
			{
				return this.m_connectionProperties.DataProvider;
			}
			set
			{
				this.m_connectionProperties.DataProvider = value;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x0007E03F File Offset: 0x0007C23F
		// (set) Token: 0x06001F1D RID: 7965 RVA: 0x0007E04C File Offset: 0x0007C24C
		[XmlIgnore]
		public string ConnectString
		{
			get
			{
				return this.m_connectionProperties.ConnectString;
			}
			set
			{
				this.m_connectionProperties.ConnectString = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x0007E05A File Offset: 0x0007C25A
		// (set) Token: 0x06001F1F RID: 7967 RVA: 0x0007E067 File Offset: 0x0007C267
		[XmlIgnore]
		public bool IntegratedSecurity
		{
			get
			{
				return this.m_connectionProperties.IntegratedSecurity;
			}
			set
			{
				this.m_connectionProperties.IntegratedSecurity = value;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0007E075 File Offset: 0x0007C275
		// (set) Token: 0x06001F21 RID: 7969 RVA: 0x0007E082 File Offset: 0x0007C282
		[XmlIgnore]
		public string Prompt
		{
			get
			{
				return this.m_connectionProperties.Prompt;
			}
			set
			{
				this.m_connectionProperties.Prompt = value;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x0007E090 File Offset: 0x0007C290
		[XmlIgnore]
		public bool IsReference
		{
			get
			{
				return this.DataSourceReference != null && this.DataSourceReference.Length > 0;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x0007E0AA File Offset: 0x0007C2AA
		// (set) Token: 0x06001F24 RID: 7972 RVA: 0x0007E0B2 File Offset: 0x0007C2B2
		[XmlIgnore]
		public bool Dirty
		{
			get
			{
				return this.m_dirty;
			}
			set
			{
				this.m_dirty = value;
			}
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0007E0BB File Offset: 0x0007C2BB
		public DataSource()
		{
			this.m_connectionProperties = new ConnectionProperties();
			this.m_connectionProperties.DataProvider = "SQL";
			this.m_dirty = false;
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0007E0E8 File Offset: 0x0007C2E8
		public DataSource GetEffectiveDataSource(DataSources references)
		{
			if (references == null || !this.IsReference)
			{
				return this;
			}
			DataSource dataSource = references.Find(this.DataSourceReference);
			if (dataSource != null)
			{
				return dataSource;
			}
			return this;
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0007E115 File Offset: 0x0007C315
		public bool ShouldSerializeConnectionProperties()
		{
			return !this.IsReference;
		}

		// Token: 0x04000D85 RID: 3461
		private string m_name;

		// Token: 0x04000D86 RID: 3462
		private Guid m_DataSourceID;

		// Token: 0x04000D87 RID: 3463
		private bool m_transaction;

		// Token: 0x04000D88 RID: 3464
		private string m_dataSourceReference;

		// Token: 0x04000D89 RID: 3465
		private ConnectionProperties m_connectionProperties;

		// Token: 0x04000D8A RID: 3466
		private bool m_dirty;
	}
}
