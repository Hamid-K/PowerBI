using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x0200053E RID: 1342
	public class Database : ConfigurationElement
	{
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002D34 RID: 11572 RVA: 0x0002038A File Offset: 0x0001E58A
		// (set) Token: 0x06002D35 RID: 11573 RVA: 0x0002039C File Offset: 0x0001E59C
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002D36 RID: 11574 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
		// (set) Token: 0x06002D37 RID: 11575 RVA: 0x0001EACA File Offset: 0x0001CCCA
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("connectionString", IsRequired = true)]
		public string ConnectionString
		{
			get
			{
				return (string)base["connectionString"];
			}
			set
			{
				base["connectionString"] = value;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x000982E7 File Offset: 0x000964E7
		// (set) Token: 0x06002D39 RID: 11577 RVA: 0x000982F9 File Offset: 0x000964F9
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("commandParameterNameCase", IsRequired = false)]
		public string CommandParameterNameCase
		{
			get
			{
				return (string)base["commandParameterNameCase"];
			}
			set
			{
				base["commandParameterNameCase"] = value;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x00098307 File Offset: 0x00096507
		// (set) Token: 0x06002D3B RID: 11579 RVA: 0x00098319 File Offset: 0x00096519
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("hostInitiatedAffiliateApplication", IsRequired = false)]
		public string HostInitiatedAffiliateApplication
		{
			get
			{
				return (string)base["hostInitiatedAffiliateApplication"];
			}
			set
			{
				base["hostInitiatedAffiliateApplication"] = value;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x00098327 File Offset: 0x00096527
		// (set) Token: 0x06002D3D RID: 11581 RVA: 0x00098339 File Offset: 0x00096539
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("windowsInitiatedAffiliateApplication", IsRequired = false)]
		public string WindowsInitiatedAffiliateApplication
		{
			get
			{
				return (string)base["windowsInitiatedAffiliateApplication"];
			}
			set
			{
				base["windowsInitiatedAffiliateApplication"] = value;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002D3E RID: 11582 RVA: 0x00098347 File Offset: 0x00096547
		// (set) Token: 0x06002D3F RID: 11583 RVA: 0x00098359 File Offset: 0x00096559
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("storedProcedureNameSeparator", IsRequired = false, DefaultValue = "_")]
		public string StoredProcedureNameSeparator
		{
			get
			{
				return (string)base["storedProcedureNameSeparator"];
			}
			set
			{
				base["storedProcedureNameSeparator"] = value;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x00098367 File Offset: 0x00096567
		// (set) Token: 0x06002D41 RID: 11585 RVA: 0x00098379 File Offset: 0x00096579
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("createPackageXml", IsRequired = false)]
		public bool CreatePackageXml
		{
			get
			{
				return (bool)base["createPackageXml"];
			}
			set
			{
				base["createPackageXml"] = value;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002D42 RID: 11586 RVA: 0x0009838C File Offset: 0x0009658C
		// (set) Token: 0x06002D43 RID: 11587 RVA: 0x0009839E File Offset: 0x0009659E
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("createPackageProcedure", IsRequired = false, DefaultValue = true)]
		public bool CreatePackageProcedure
		{
			get
			{
				return (bool)base["createPackageProcedure"];
			}
			set
			{
				base["createPackageProcedure"] = value;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002D44 RID: 11588 RVA: 0x000983B1 File Offset: 0x000965B1
		// (set) Token: 0x06002D45 RID: 11589 RVA: 0x000983C3 File Offset: 0x000965C3
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("packageXmlFormat", IsRequired = false, DefaultValue = "v90")]
		public PackageXmlFormat PackageXmlFormat
		{
			get
			{
				return (PackageXmlFormat)base["packageXmlFormat"];
			}
			set
			{
				base["packageXmlFormat"] = value;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002D46 RID: 11590 RVA: 0x000983D6 File Offset: 0x000965D6
		// (set) Token: 0x06002D47 RID: 11591 RVA: 0x000983E8 File Offset: 0x000965E8
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("clientApplicationName", IsRequired = false, DefaultValue = "empty")]
		public ClientApplicationName ClientApplicationName
		{
			get
			{
				return (ClientApplicationName)base["clientApplicationName"];
			}
			set
			{
				base["clientApplicationName"] = value;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002D48 RID: 11592 RVA: 0x000983FB File Offset: 0x000965FB
		// (set) Token: 0x06002D49 RID: 11593 RVA: 0x0009840D File Offset: 0x0009660D
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("createPackageProcedureWithCustomSqlScripts", IsRequired = false, DefaultValue = false)]
		public bool CreatePackageProcedureWithCustomSqlScripts
		{
			get
			{
				return (bool)base["createPackageProcedureWithCustomSqlScripts"];
			}
			set
			{
				base["createPackageProcedureWithCustomSqlScripts"] = value;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x00098420 File Offset: 0x00096620
		// (set) Token: 0x06002D4B RID: 11595 RVA: 0x00098432 File Offset: 0x00096632
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("packageXmlLocation", IsRequired = false, DefaultValue = "c:\\temp")]
		public string PackageXmlLocation
		{
			get
			{
				return (string)base["packageXmlLocation"];
			}
			set
			{
				base["packageXmlLocation"] = value;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x00098440 File Offset: 0x00096640
		// (set) Token: 0x06002D4D RID: 11597 RVA: 0x00098452 File Offset: 0x00096652
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("packageProcedureSchemaList", IsRequired = false)]
		public string PackageProcedureSchemaList
		{
			get
			{
				return (string)base["packageProcedureSchemaList"];
			}
			set
			{
				base["packageProcedureSchemaList"] = value;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002D4E RID: 11598 RVA: 0x00098460 File Offset: 0x00096660
		// (set) Token: 0x06002D4F RID: 11599 RVA: 0x00098472 File Offset: 0x00096672
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("defaultCollationName", IsRequired = false)]
		public string DefaultCollationName
		{
			get
			{
				return (string)base["defaultCollationName"];
			}
			set
			{
				base["defaultCollationName"] = value;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x00098480 File Offset: 0x00096680
		// (set) Token: 0x06002D51 RID: 11601 RVA: 0x00098492 File Offset: 0x00096692
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("storedProcedureCallTimeout", IsRequired = false, DefaultValue = 30)]
		public int StoredProcedureCallTimeout
		{
			get
			{
				return (int)base["storedProcedureCallTimeout"];
			}
			set
			{
				base["storedProcedureCallTimeout"] = value;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000984A5 File Offset: 0x000966A5
		// (set) Token: 0x06002D53 RID: 11603 RVA: 0x000984B7 File Offset: 0x000966B7
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sqlTransforms", IsRequired = false, DefaultValue = "Service")]
		public SqlTransforms SqlTransforms
		{
			get
			{
				return (SqlTransforms)base["sqlTransforms"];
			}
			set
			{
				base["sqlTransforms"] = value;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002D54 RID: 11604 RVA: 0x000984CA File Offset: 0x000966CA
		// (set) Token: 0x06002D55 RID: 11605 RVA: 0x000984DC File Offset: 0x000966DC
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("createPackageProcedureWithExtendedProperties", IsRequired = false, DefaultValue = false)]
		public bool CreatePackageProcedureWithExtendedProperties
		{
			get
			{
				return (bool)base["createPackageProcedureWithExtendedProperties"];
			}
			set
			{
				base["createPackageProcedureWithExtendedProperties"] = value;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x000984EF File Offset: 0x000966EF
		// (set) Token: 0x06002D57 RID: 11607 RVA: 0x00098501 File Offset: 0x00096701
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("sqlTransformsUnicodeOutput", IsRequired = false, DefaultValue = false)]
		public bool SqlTransformsUnicodeOutput
		{
			get
			{
				return (bool)base["sqlTransformsUnicodeOutput"];
			}
			set
			{
				base["sqlTransformsUnicodeOutput"] = value;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x00098514 File Offset: 0x00096714
		// (set) Token: 0x06002D59 RID: 11609 RVA: 0x00098526 File Offset: 0x00096726
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("packageProcedureCacheFlush", IsRequired = false, DefaultValue = "P1D")]
		public string PackageProcedureCacheFlushString
		{
			get
			{
				return (string)base["packageProcedureCacheFlush"];
			}
			set
			{
				base["packageProcedureCacheFlush"] = value;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x00098534 File Offset: 0x00096734
		// (set) Token: 0x06002D5B RID: 11611 RVA: 0x00098541 File Offset: 0x00096741
		public TimeSpan PackageProcedureCacheFlush
		{
			get
			{
				return SoapDuration.Parse(this.PackageProcedureCacheFlushString);
			}
			set
			{
				this.PackageProcedureCacheFlushString = SoapDuration.ToString(value);
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x0009854F File Offset: 0x0009674F
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x00098561 File Offset: 0x00096761
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("packageProcedureLastInvoke", IsRequired = false, DefaultValue = "P7D")]
		public string PackageProcedureLastInvokeString
		{
			get
			{
				return (string)base["packageProcedureLastInvoke"];
			}
			set
			{
				base["packageProcedureLastInvoke"] = value;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x0009856F File Offset: 0x0009676F
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x0009857C File Offset: 0x0009677C
		public TimeSpan PackageProcedureLastInvoke
		{
			get
			{
				return SoapDuration.Parse(this.PackageProcedureLastInvokeString);
			}
			set
			{
				this.PackageProcedureLastInvokeString = SoapDuration.ToString(value);
			}
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x0009858A File Offset: 0x0009678A
		public object GetElementKey()
		{
			return this.Type;
		}
	}
}
