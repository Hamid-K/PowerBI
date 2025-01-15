using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x020005B0 RID: 1456
	public class ExecutionParameters : ConfigurationElement
	{
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x000ACF5A File Offset: 0x000AB15A
		// (set) Token: 0x060032F2 RID: 13042 RVA: 0x000ACF6C File Offset: 0x000AB16C
		[ConfigurationProperty("programmingModel", IsRequired = true)]
		public ConfigProgrammingModels ConfigProgrammingModel
		{
			get
			{
				return (ConfigProgrammingModels)base["programmingModel"];
			}
			set
			{
				base["programmingModel"] = value;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x000ACF7F File Offset: 0x000AB17F
		// (set) Token: 0x060032F4 RID: 13044 RVA: 0x000ACF91 File Offset: 0x000AB191
		[ConfigurationProperty("connectionType", IsRequired = false, DefaultValue = "NonPersistent")]
		public ConfigConnectionTypes ConnectionType
		{
			get
			{
				return (ConfigConnectionTypes)base["connectionType"];
			}
			set
			{
				base["connectionType"] = value;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000ACFA4 File Offset: 0x000AB1A4
		// (set) Token: 0x060032F6 RID: 13046 RVA: 0x000ACFB6 File Offset: 0x000AB1B6
		[ConfigurationProperty("userId", IsRequired = false)]
		public string UserId
		{
			get
			{
				return (string)base["userId"];
			}
			set
			{
				base["userId"] = value;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x000ACFC4 File Offset: 0x000AB1C4
		// (set) Token: 0x060032F8 RID: 13048 RVA: 0x000ACFD6 File Offset: 0x000AB1D6
		[ConfigurationProperty("password", IsRequired = false)]
		public string Password
		{
			get
			{
				return (string)base["password"];
			}
			set
			{
				base["password"] = value;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x000ACFE4 File Offset: 0x000AB1E4
		// (set) Token: 0x060032FA RID: 13050 RVA: 0x000ACFF6 File Offset: 0x000AB1F6
		[ConfigurationProperty("trmTransactionIdOverride", IsRequired = false)]
		public string TrmTransactionIdOverride
		{
			get
			{
				return (string)base["trmTransactionIdOverride"];
			}
			set
			{
				base["trmTransactionIdOverride"] = value;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060032FB RID: 13051 RVA: 0x000AD004 File Offset: 0x000AB204
		// (set) Token: 0x060032FC RID: 13052 RVA: 0x000AD016 File Offset: 0x000AB216
		[ConfigurationProperty("iterations", IsRequired = true, DefaultValue = 1)]
		public int Iterations
		{
			get
			{
				return (int)base["iterations"];
			}
			set
			{
				base["iterations"] = value;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x000AD029 File Offset: 0x000AB229
		// (set) Token: 0x060032FE RID: 13054 RVA: 0x000AD03B File Offset: 0x000AB23B
		[ConfigurationProperty("requireResponseToContinue", IsRequired = false, DefaultValue = true)]
		public bool RequireResponseToContinue
		{
			get
			{
				return (bool)base["requireResponseToContinue"];
			}
			set
			{
				base["requireResponseToContinue"] = value;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000AD04E File Offset: 0x000AB24E
		// (set) Token: 0x06003300 RID: 13056 RVA: 0x000AD060 File Offset: 0x000AB260
		[ConfigurationProperty("useSpaForImsConversationalTransactions", IsRequired = false, DefaultValue = false)]
		public bool UseSpaForImsConversationalTransactions
		{
			get
			{
				return (bool)base["useSpaForImsConversationalTransactions"];
			}
			set
			{
				base["useSpaForImsConversationalTransactions"] = value;
			}
		}
	}
}
