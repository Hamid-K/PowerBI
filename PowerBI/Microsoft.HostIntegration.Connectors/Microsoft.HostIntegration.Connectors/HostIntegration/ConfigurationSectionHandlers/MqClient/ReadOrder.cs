using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.MqClient
{
	// Token: 0x02000514 RID: 1300
	public class ReadOrder : ConfigurationElement
	{
		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002BE9 RID: 11241 RVA: 0x00096E2B File Offset: 0x0009502B
		// (set) Token: 0x06002BEA RID: 11242 RVA: 0x00096E3D File Offset: 0x0009503D
		[ConfigurationProperty("appConfig", IsRequired = false, DefaultValue = "first")]
		internal string appConfig
		{
			get
			{
				return (string)base["appConfig"];
			}
			set
			{
				base["appConfig"] = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x00096E4B File Offset: 0x0009504B
		// (set) Token: 0x06002BEC RID: 11244 RVA: 0x00096E78 File Offset: 0x00095078
		[Description("Setting appConfig to First or Second causes the Microsoft Client for MQ Runtime to use the App or Web Configuration file to obtain configuration information. Setting config to Unused prevents the Microsoft Client for MQ Runtime from using App or Web Configuration file.")]
		[Category("General")]
		[DisplayName("App Config")]
		public MqClientConfigurationPriority AppConfig
		{
			get
			{
				if (this.appConfig == "first")
				{
					return MqClientConfigurationPriority.First;
				}
				if (this.appConfig == "second")
				{
					return MqClientConfigurationPriority.Second;
				}
				return MqClientConfigurationPriority.Unused;
			}
			set
			{
				if (this.Cache == MqClientConfigurationPriority.Unused)
				{
					this.appConfig = "first";
					return;
				}
				string text = this.appConfig.ToLowerInvariant();
				if (text != null)
				{
					if (!(text == "first"))
					{
						if (!(text == "second"))
						{
							if (!(text == "unused"))
							{
								return;
							}
							if (value == MqClientConfigurationPriority.First)
							{
								if (this.Cache == MqClientConfigurationPriority.First)
								{
									this.cache = "second";
								}
								this.appConfig = "first";
								return;
							}
							if (value == MqClientConfigurationPriority.Second)
							{
								if (this.Cache == MqClientConfigurationPriority.First)
								{
									this.appConfig = "second";
								}
								return;
							}
							if (value == MqClientConfigurationPriority.Unused)
							{
								this.appConfig = "unused";
								return;
							}
						}
						else if (value == MqClientConfigurationPriority.First)
						{
							if (this.Cache == MqClientConfigurationPriority.First)
							{
								this.appConfig = "first";
								this.cache = "second";
								return;
							}
						}
						else
						{
							if (value == MqClientConfigurationPriority.Second)
							{
								this.appConfig = "second";
								return;
							}
							if (value == MqClientConfigurationPriority.Unused)
							{
								this.appConfig = "unused";
								return;
							}
						}
					}
					else
					{
						if (value == MqClientConfigurationPriority.First)
						{
							this.appConfig = "first";
							return;
						}
						if (value == MqClientConfigurationPriority.Second)
						{
							if (this.Cache == MqClientConfigurationPriority.Second)
							{
								this.cache = "first";
								this.appConfig = "second";
								return;
							}
							this.appConfig = "first";
							return;
						}
						else if (value == MqClientConfigurationPriority.Unused)
						{
							if (this.Cache == MqClientConfigurationPriority.Second)
							{
								this.cache = "first";
								this.appConfig = "unused";
								return;
							}
							this.appConfig = "first";
							return;
						}
					}
				}
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x00096FDD File Offset: 0x000951DD
		// (set) Token: 0x06002BEE RID: 11246 RVA: 0x00096FEF File Offset: 0x000951EF
		[ConfigurationProperty("cache", IsRequired = false, DefaultValue = "unused")]
		internal string cache
		{
			get
			{
				return (string)base["cache"];
			}
			set
			{
				base["cache"] = value;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x00096FFD File Offset: 0x000951FD
		// (set) Token: 0x06002BF0 RID: 11248 RVA: 0x00097028 File Offset: 0x00095228
		[Description("Setting cache to First or  Second causes the Microsoft Client for MQ Runtime to use the AppFabric Cache to obtain configuration information. Setting Cache to Unused prevents the Microsoft Client for MQ Runtime from using the AppFabric Cache. The location of the Cache is obtained from the AppFabricCache properties. If the AppFabricCache properties are not set, then Cache should be set to Unused.")]
		[Category("General")]
		[DisplayName("Cache")]
		public MqClientConfigurationPriority Cache
		{
			get
			{
				if (this.cache == "first")
				{
					return MqClientConfigurationPriority.First;
				}
				if (this.cache == "second")
				{
					return MqClientConfigurationPriority.Second;
				}
				return MqClientConfigurationPriority.Unused;
			}
			set
			{
				string text = this.cache.ToLowerInvariant();
				if (text != null)
				{
					if (!(text == "first"))
					{
						if (!(text == "second"))
						{
							if (!(text == "unused"))
							{
								return;
							}
							if (value == MqClientConfigurationPriority.First)
							{
								if (this.AppConfig == MqClientConfigurationPriority.First)
								{
									this.appConfig = "second";
									this.cache = "first";
								}
								return;
							}
							if (value == MqClientConfigurationPriority.Second)
							{
								if (this.AppConfig == MqClientConfigurationPriority.First)
								{
									this.cache = "second";
								}
								return;
							}
							if (value == MqClientConfigurationPriority.Unused)
							{
								this.cache = "unused";
								return;
							}
						}
						else if (value == MqClientConfigurationPriority.First)
						{
							if (this.AppConfig == MqClientConfigurationPriority.First)
							{
								this.cache = "first";
								this.appConfig = "second";
								return;
							}
						}
						else
						{
							if (value == MqClientConfigurationPriority.Second)
							{
								this.cache = "second";
								return;
							}
							if (value == MqClientConfigurationPriority.Unused)
							{
								this.cache = "unused";
								return;
							}
						}
					}
					else
					{
						if (value == MqClientConfigurationPriority.First)
						{
							this.cache = "first";
							return;
						}
						if (value == MqClientConfigurationPriority.Second)
						{
							if (this.AppConfig == MqClientConfigurationPriority.Second)
							{
								this.appConfig = "first";
								this.cache = "second";
								return;
							}
							this.cache = "first";
							return;
						}
						else if (value == MqClientConfigurationPriority.Unused)
						{
							if (this.AppConfig == MqClientConfigurationPriority.Second)
							{
								this.appConfig = "first";
								this.cache = "unused";
								return;
							}
							this.cache = "first";
							return;
						}
					}
				}
			}
		}
	}
}
