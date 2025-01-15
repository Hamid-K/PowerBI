using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x0200054E RID: 1358
	public class Service : ConfigurationElement
	{
		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06002DDD RID: 11741 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x0009AD51 File Offset: 0x00098F51
		// (set) Token: 0x06002DDF RID: 11743 RVA: 0x0009AD63 File Offset: 0x00098F63
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("connectionManager", IsRequired = true)]
		public ConnectionManager ConnectionManager
		{
			get
			{
				return (ConnectionManager)base["connectionManager"];
			}
			set
			{
				base["connectionManager"] = value;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x0009AD71 File Offset: 0x00098F71
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x0009AD83 File Offset: 0x00098F83
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("securityManager", IsRequired = true)]
		public SecurityManager SecurityManager
		{
			get
			{
				return (SecurityManager)base["securityManager"];
			}
			set
			{
				base["securityManager"] = value;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x0009AD91 File Offset: 0x00098F91
		// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x0009ADA3 File Offset: 0x00098FA3
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sqlApplicationManager", IsRequired = true)]
		public SqlApplicationManager SqlApplicationManager
		{
			get
			{
				return (SqlApplicationManager)base["sqlApplicationManager"];
			}
			set
			{
				base["sqlApplicationManager"] = value;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x0009ADB1 File Offset: 0x00098FB1
		// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x0009ADC3 File Offset: 0x00098FC3
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("resynchronizationManager", IsRequired = true)]
		public ResynchronizationManager ResynchronizationManager
		{
			get
			{
				return (ResynchronizationManager)base["resynchronizationManager"];
			}
			set
			{
				base["resynchronizationManager"] = value;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x0009ADD1 File Offset: 0x00098FD1
		// (set) Token: 0x06002DE7 RID: 11751 RVA: 0x00097EDA File Offset: 0x000960DA
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("database", IsRequired = true)]
		public Database Database
		{
			get
			{
				return (Database)base["database"];
			}
			set
			{
				base["database"] = value;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x0009ADE3 File Offset: 0x00098FE3
		// (set) Token: 0x06002DE9 RID: 11753 RVA: 0x0009ADF5 File Offset: 0x00098FF5
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sqlSet", IsRequired = false)]
		public SqlSet SqlSet
		{
			get
			{
				return (SqlSet)base["sqlSet"];
			}
			set
			{
				base["sqlSet"] = value;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x0009AE03 File Offset: 0x00099003
		// (set) Token: 0x06002DEB RID: 11755 RVA: 0x0009AE15 File Offset: 0x00099015
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("applicationEncodings", IsRequired = false)]
		public ApplicationEncodingCollection ApplicationEncodings
		{
			get
			{
				return (ApplicationEncodingCollection)base["applicationEncodings"];
			}
			set
			{
				base["applicationEncodings"] = value;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x0009AE23 File Offset: 0x00099023
		// (set) Token: 0x06002DED RID: 11757 RVA: 0x0009AE35 File Offset: 0x00099035
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("collationNames", IsRequired = false)]
		public CollationNameCollection CollationNames
		{
			get
			{
				return (CollationNameCollection)base["collationNames"];
			}
			set
			{
				base["collationNames"] = value;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x0009AE43 File Offset: 0x00099043
		// (set) Token: 0x06002DEF RID: 11759 RVA: 0x0009AE55 File Offset: 0x00099055
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("conversionFormats", IsRequired = false)]
		public ConversionFormats ConversionFormats
		{
			get
			{
				return (ConversionFormats)base["conversionFormats"];
			}
			set
			{
				base["conversionFormats"] = value;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x00096E0B File Offset: 0x0009500B
		// (set) Token: 0x06002DF1 RID: 11761 RVA: 0x00096E1D File Offset: 0x0009501D
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("conversionBehavior", IsRequired = false)]
		public ConversionBehavior ConversionBehavior
		{
			get
			{
				return (ConversionBehavior)base["conversionBehavior"];
			}
			set
			{
				base["conversionBehavior"] = value;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x0009AE63 File Offset: 0x00099063
		// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x0009AE75 File Offset: 0x00099075
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("databaseAliases", IsRequired = false)]
		public DatabaseAliasCollection DatabaseAliases
		{
			get
			{
				return (DatabaseAliasCollection)base["databaseAliases"];
			}
			set
			{
				base["databaseAliases"] = value;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x0009AE83 File Offset: 0x00099083
		// (set) Token: 0x06002DF5 RID: 11765 RVA: 0x0009AE95 File Offset: 0x00099095
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("drdaServiceTraceListeners", IsRequired = false)]
		public DrdaServiceTraceListenerCollection DrdaServiceTraceListeners
		{
			get
			{
				return (DrdaServiceTraceListenerCollection)base["drdaServiceTraceListeners"];
			}
			set
			{
				base["drdaServiceTraceListeners"] = value;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x0009AEA3 File Offset: 0x000990A3
		// (set) Token: 0x06002DF7 RID: 11767 RVA: 0x0009AEB5 File Offset: 0x000990B5
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("packageBindListeners", IsRequired = false)]
		public PackageBindListenerCollection PackageBindListeners
		{
			get
			{
				return (PackageBindListenerCollection)base["packageBindListeners"];
			}
			set
			{
				base["packageBindListeners"] = value;
			}
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x0009AEC3 File Offset: 0x000990C3
		public object GetElementKey()
		{
			return this.Name;
		}
	}
}
