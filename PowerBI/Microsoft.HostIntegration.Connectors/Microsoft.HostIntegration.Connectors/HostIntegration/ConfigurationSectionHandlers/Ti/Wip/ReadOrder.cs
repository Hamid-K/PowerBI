using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x02000599 RID: 1433
	public class ReadOrder : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x06003182 RID: 12674 RVA: 0x000A5529 File Offset: 0x000A3729
		public void SetReadOrderCallBack(GetReadOrderCallbackType getReadOrderCallback)
		{
			this._getReadOrderCallback = getReadOrderCallback;
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000A5532 File Offset: 0x000A3732
		public ReadOrder GetReadOrder()
		{
			return this._getReadOrderCallback();
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x00096E2B File Offset: 0x0009502B
		// (set) Token: 0x06003185 RID: 12677 RVA: 0x00096E3D File Offset: 0x0009503D
		[ConfigurationProperty("appConfig", IsRequired = true, DefaultValue = "first")]
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

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x000A553F File Offset: 0x000A373F
		// (set) Token: 0x06003187 RID: 12679 RVA: 0x000A5580 File Offset: 0x000A3780
		[TypeConverter(typeof(ReadOrderAppConfigDropDownList))]
		[Description("Setting appConfig to first, second, third causes the TI Runtime to use the App or Web Configuration file to obtain configuration information. Setting config to unused prevents the TI Runtime from using App or Web Configuration file.")]
		[Category("General")]
		[DisplayName("App Config")]
		public WipConfigurationPriority AppConfig
		{
			get
			{
				if (this.appConfig == "first")
				{
					return WipConfigurationPriority.First;
				}
				if (this.appConfig == "second")
				{
					return WipConfigurationPriority.Second;
				}
				if (this.appConfig == "third")
				{
					return WipConfigurationPriority.Third;
				}
				return WipConfigurationPriority.Unused;
			}
			set
			{
				if (this.Registry == WipConfigurationPriority.Unused && this.Cache == WipConfigurationPriority.Unused)
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
							if (!(text == "third"))
							{
								if (!(text == "unused"))
								{
									return;
								}
								if (value == WipConfigurationPriority.First)
								{
									if (this.Registry == WipConfigurationPriority.Third)
									{
										this.registry = "unused";
									}
									else if (this.Cache == WipConfigurationPriority.Third)
									{
										this.cache = "unused";
									}
									if (this.Registry == WipConfigurationPriority.Second)
									{
										this.registry = "third";
									}
									else if (this.Cache == WipConfigurationPriority.Second)
									{
										this.cache = "third";
									}
									if (this.Registry == WipConfigurationPriority.First)
									{
										this.registry = "second";
									}
									else if (this.Cache == WipConfigurationPriority.First)
									{
										this.cache = "second";
									}
									this.appConfig = "first";
									return;
								}
								if (value == WipConfigurationPriority.Second)
								{
									if (this.Registry == WipConfigurationPriority.Third)
									{
										this.registry = "unused";
									}
									else if (this.Cache == WipConfigurationPriority.Third)
									{
										this.cache = "unused";
									}
									if (this.Registry == WipConfigurationPriority.Second)
									{
										this.registry = "third";
									}
									else if (this.Cache == WipConfigurationPriority.Second)
									{
										this.cache = "third";
									}
									this.appConfig = "second";
									return;
								}
								if (value == WipConfigurationPriority.Third)
								{
									if (this.Registry == WipConfigurationPriority.Third || this.Cache == WipConfigurationPriority.Third)
									{
										if (this.Registry == WipConfigurationPriority.Third)
										{
											this.registry = "unused";
										}
										else if (this.Cache == WipConfigurationPriority.Third)
										{
											this.cache = "unused";
										}
										this.appConfig = "third";
										return;
									}
									if (this.Registry == WipConfigurationPriority.Second || this.Cache == WipConfigurationPriority.Second)
									{
										this.appConfig = "third";
										return;
									}
									this.appConfig = "second";
									return;
								}
								else if (value == WipConfigurationPriority.Unused)
								{
									this.appConfig = "unused";
									return;
								}
							}
							else
							{
								if (value == WipConfigurationPriority.First)
								{
									if (this.Registry == WipConfigurationPriority.Third)
									{
										this.registry = "unused";
									}
									else if (this.Cache == WipConfigurationPriority.Third)
									{
										this.cache = "unused";
									}
									if (this.Registry == WipConfigurationPriority.Second)
									{
										this.registry = "third";
									}
									else if (this.Cache == WipConfigurationPriority.Second)
									{
										this.cache = "third";
									}
									if (this.Registry == WipConfigurationPriority.First)
									{
										this.registry = "second";
									}
									else if (this.Cache == WipConfigurationPriority.First)
									{
										this.cache = "second";
									}
									this.appConfig = "first";
									return;
								}
								if (value == WipConfigurationPriority.Second)
								{
									if (this.Registry == WipConfigurationPriority.Second)
									{
										this.appConfig = "second";
										this.registry = "third";
										return;
									}
									if (this.Cache == WipConfigurationPriority.Second)
									{
										this.appConfig = "second";
										this.cache = "third";
										return;
									}
									this.appConfig = "first";
									return;
								}
								else
								{
									if (value == WipConfigurationPriority.Third)
									{
										this.appConfig = "third";
										return;
									}
									if (value == WipConfigurationPriority.Unused)
									{
										this.appConfig = "unused";
										return;
									}
								}
							}
						}
						else if (value == WipConfigurationPriority.First)
						{
							if (this.Registry == WipConfigurationPriority.First)
							{
								this.appConfig = "first";
								this.registry = "second";
								return;
							}
							if (this.Cache == WipConfigurationPriority.First)
							{
								this.appConfig = "first";
								this.cache = "second";
								return;
							}
						}
						else
						{
							if (value == WipConfigurationPriority.Second)
							{
								this.appConfig = "second";
								return;
							}
							if (value == WipConfigurationPriority.Third)
							{
								if (this.Registry == WipConfigurationPriority.Third || this.Cache == WipConfigurationPriority.Third)
								{
									if (this.Registry == WipConfigurationPriority.Third)
									{
										this.registry = "second";
									}
									else if (this.Cache == WipConfigurationPriority.Third)
									{
										this.cache = "second";
									}
									this.appConfig = "third";
									return;
								}
								if (this.Registry == WipConfigurationPriority.Second || this.Cache == WipConfigurationPriority.Second)
								{
									if (this.Registry == WipConfigurationPriority.Second)
									{
										this.registry = "first";
									}
									else if (this.Cache == WipConfigurationPriority.Second)
									{
										this.cache = "first";
									}
									this.appConfig = "second";
									return;
								}
								this.appConfig = "second";
								return;
							}
							else if (value == WipConfigurationPriority.Unused)
							{
								if (this.Registry == WipConfigurationPriority.Third || this.Cache == WipConfigurationPriority.Third)
								{
									if (this.Registry == WipConfigurationPriority.Third)
									{
										this.registry = "second";
									}
									else if (this.Cache == WipConfigurationPriority.Third)
									{
										this.cache = "second";
									}
									this.appConfig = "unused";
									return;
								}
								this.appConfig = "unused";
								return;
							}
						}
					}
					else
					{
						if (value == WipConfigurationPriority.First)
						{
							this.appConfig = "first";
							return;
						}
						if (value == WipConfigurationPriority.Second)
						{
							if (this.Registry == WipConfigurationPriority.Second)
							{
								this.registry = "first";
								this.appConfig = "second";
								return;
							}
							if (this.Cache == WipConfigurationPriority.Second)
							{
								this.cache = "first";
								this.appConfig = "second";
								return;
							}
							this.appConfig = "first";
							return;
						}
						else if (value == WipConfigurationPriority.Third)
						{
							if (this.Registry != WipConfigurationPriority.Third && this.Registry != WipConfigurationPriority.Third && this.Cache != WipConfigurationPriority.Third && this.Cache != WipConfigurationPriority.Third && this.Registry != WipConfigurationPriority.Second && this.Registry != WipConfigurationPriority.Second && this.Cache != WipConfigurationPriority.Second && this.Cache != WipConfigurationPriority.Second)
							{
								this.appConfig = "first";
								return;
							}
							if (this.Registry == WipConfigurationPriority.Second)
							{
								this.registry = "first";
								this.appConfig = "second";
							}
							else
							{
								if (this.Cache != WipConfigurationPriority.Second)
								{
									this.appConfig = "first";
									return;
								}
								this.cache = "first";
								this.appConfig = "second";
							}
							if (this.Registry == WipConfigurationPriority.Third)
							{
								this.registry = "second";
								this.appConfig = "third";
								return;
							}
							if (this.Cache == WipConfigurationPriority.Third)
							{
								this.cache = "second";
								this.appConfig = "third";
								return;
							}
							this.appConfig = "second";
							return;
						}
						else if (value == WipConfigurationPriority.Unused)
						{
							if (this.Registry != WipConfigurationPriority.Third && this.Cache != WipConfigurationPriority.Third && this.Registry != WipConfigurationPriority.Second && this.Cache != WipConfigurationPriority.Second)
							{
								this.appConfig = "first";
								return;
							}
							if (this.Registry == WipConfigurationPriority.Second)
							{
								this.registry = "first";
								this.appConfig = "unused";
							}
							else
							{
								if (this.Cache != WipConfigurationPriority.Second)
								{
									this.appConfig = "first";
									return;
								}
								this.cache = "first";
								this.appConfig = "unused";
							}
							if (this.Registry == WipConfigurationPriority.Third)
							{
								this.registry = "second";
								this.appConfig = "unused";
								return;
							}
							if (this.Cache == WipConfigurationPriority.Third)
							{
								this.cache = "second";
								this.appConfig = "unused";
								return;
							}
							this.appConfig = "unused";
							return;
						}
					}
				}
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000A5BEF File Offset: 0x000A3DEF
		// (set) Token: 0x06003189 RID: 12681 RVA: 0x000A5C01 File Offset: 0x000A3E01
		[ConfigurationProperty("registry", IsRequired = false, DefaultValue = "unused")]
		internal string registry
		{
			get
			{
				return (string)base["registry"];
			}
			set
			{
				base["registry"] = value;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x000A5C0F File Offset: 0x000A3E0F
		// (set) Token: 0x0600318B RID: 12683 RVA: 0x000A5C50 File Offset: 0x000A3E50
		[TypeConverter(typeof(ReadOrderRegistryDropDownList))]
		[Description("Setting registry to first, second, third causes the TI Runtime to use the machines Windows Registry to obtain configuration information that was placed there by TI Manager. Setting Registry to unused prevents the TI Runtime from using Windows Registry.")]
		[Category("General")]
		[DisplayName("Registry")]
		public WipConfigurationPriority Registry
		{
			get
			{
				if (this.registry == "first")
				{
					return WipConfigurationPriority.First;
				}
				if (this.registry == "second")
				{
					return WipConfigurationPriority.Second;
				}
				if (this.registry == "third")
				{
					return WipConfigurationPriority.Third;
				}
				return WipConfigurationPriority.Unused;
			}
			set
			{
				if (this.AppConfig == WipConfigurationPriority.Unused && this.Cache == WipConfigurationPriority.Unused)
				{
					this.registry = "first";
					return;
				}
				string text = this.registry.ToLowerInvariant();
				if (text != null)
				{
					if (!(text == "first"))
					{
						if (!(text == "second"))
						{
							if (!(text == "third"))
							{
								if (!(text == "unused"))
								{
									return;
								}
								if (value == WipConfigurationPriority.First)
								{
									if (this.AppConfig == WipConfigurationPriority.Third)
									{
										this.appConfig = "unused";
									}
									else if (this.Cache == WipConfigurationPriority.Third)
									{
										this.cache = "unused";
									}
									if (this.AppConfig == WipConfigurationPriority.Second)
									{
										this.appConfig = "third";
									}
									else if (this.Cache == WipConfigurationPriority.Second)
									{
										this.cache = "third";
									}
									if (this.AppConfig == WipConfigurationPriority.First)
									{
										this.appConfig = "second";
									}
									else if (this.Cache == WipConfigurationPriority.First)
									{
										this.cache = "second";
									}
									this.registry = "first";
									return;
								}
								if (value == WipConfigurationPriority.Second)
								{
									if (this.AppConfig == WipConfigurationPriority.Third)
									{
										this.appConfig = "unused";
									}
									else if (this.Cache == WipConfigurationPriority.Third)
									{
										this.cache = "unused";
									}
									if (this.AppConfig == WipConfigurationPriority.Second)
									{
										this.appConfig = "third";
									}
									else if (this.Cache == WipConfigurationPriority.Second)
									{
										this.cache = "third";
									}
									this.registry = "second";
									return;
								}
								if (value == WipConfigurationPriority.Third)
								{
									if (this.AppConfig == WipConfigurationPriority.Third || this.Cache == WipConfigurationPriority.Third)
									{
										if (this.AppConfig == WipConfigurationPriority.Third)
										{
											this.appConfig = "unused";
										}
										else if (this.Cache == WipConfigurationPriority.Third)
										{
											this.cache = "unused";
										}
										this.registry = "third";
										return;
									}
									if (this.AppConfig == WipConfigurationPriority.Second || this.Cache == WipConfigurationPriority.Second)
									{
										this.registry = "third";
										return;
									}
									this.registry = "second";
									return;
								}
								else if (value == WipConfigurationPriority.Unused)
								{
									this.registry = "unused";
									return;
								}
							}
							else
							{
								if (value == WipConfigurationPriority.First)
								{
									if (this.AppConfig == WipConfigurationPriority.Third)
									{
										this.appConfig = "unused";
									}
									else if (this.Cache == WipConfigurationPriority.Third)
									{
										this.cache = "unused";
									}
									if (this.AppConfig == WipConfigurationPriority.Second)
									{
										this.appConfig = "third";
									}
									else if (this.Cache == WipConfigurationPriority.Second)
									{
										this.cache = "third";
									}
									if (this.AppConfig == WipConfigurationPriority.First)
									{
										this.appConfig = "second";
									}
									else if (this.Cache == WipConfigurationPriority.First)
									{
										this.cache = "second";
									}
									this.registry = "first";
									return;
								}
								if (value == WipConfigurationPriority.Second)
								{
									if (this.AppConfig == WipConfigurationPriority.Second)
									{
										this.registry = "second";
										this.appConfig = "third";
										return;
									}
									if (this.Cache == WipConfigurationPriority.Second)
									{
										this.registry = "second";
										this.cache = "third";
										return;
									}
									this.registry = "first";
									return;
								}
								else
								{
									if (value == WipConfigurationPriority.Third)
									{
										this.registry = "third";
										return;
									}
									if (value == WipConfigurationPriority.Unused)
									{
										this.registry = "unused";
										return;
									}
								}
							}
						}
						else if (value == WipConfigurationPriority.First)
						{
							if (this.AppConfig == WipConfigurationPriority.First)
							{
								this.registry = "first";
								this.appConfig = "second";
								return;
							}
							if (this.Cache == WipConfigurationPriority.First)
							{
								this.registry = "first";
								this.cache = "second";
								return;
							}
						}
						else
						{
							if (value == WipConfigurationPriority.Second)
							{
								this.registry = "second";
								return;
							}
							if (value == WipConfigurationPriority.Third)
							{
								if (this.AppConfig == WipConfigurationPriority.Third || this.Cache == WipConfigurationPriority.Third)
								{
									if (this.AppConfig == WipConfigurationPriority.Third)
									{
										this.appConfig = "second";
									}
									else if (this.Cache == WipConfigurationPriority.Third)
									{
										this.cache = "second";
									}
									this.registry = "third";
									return;
								}
								if (this.AppConfig == WipConfigurationPriority.Second || this.Cache == WipConfigurationPriority.Second)
								{
									if (this.AppConfig == WipConfigurationPriority.Second)
									{
										this.appConfig = "first";
									}
									else if (this.Cache == WipConfigurationPriority.Second)
									{
										this.cache = "first";
									}
									this.registry = "second";
									return;
								}
								this.registry = "second";
								return;
							}
							else if (value == WipConfigurationPriority.Unused)
							{
								if (this.AppConfig == WipConfigurationPriority.Third || this.Cache == WipConfigurationPriority.Third)
								{
									if (this.AppConfig == WipConfigurationPriority.Third)
									{
										this.appConfig = "second";
									}
									else if (this.Cache == WipConfigurationPriority.Third)
									{
										this.cache = "second";
									}
									this.registry = "unused";
									return;
								}
								this.registry = "unused";
								return;
							}
						}
					}
					else
					{
						if (value == WipConfigurationPriority.First)
						{
							this.registry = "first";
							return;
						}
						if (value == WipConfigurationPriority.Second)
						{
							if (this.AppConfig == WipConfigurationPriority.Second)
							{
								this.appConfig = "first";
								this.registry = "second";
								return;
							}
							if (this.Cache == WipConfigurationPriority.Second)
							{
								this.cache = "first";
								this.registry = "second";
								return;
							}
							this.registry = "first";
							return;
						}
						else if (value == WipConfigurationPriority.Third)
						{
							if (this.AppConfig != WipConfigurationPriority.Third && this.Cache != WipConfigurationPriority.Third && this.AppConfig != WipConfigurationPriority.Second && this.Cache != WipConfigurationPriority.Second)
							{
								this.registry = "first";
								return;
							}
							if (this.AppConfig == WipConfigurationPriority.Second)
							{
								this.appConfig = "first";
								this.registry = "second";
							}
							else
							{
								if (this.Cache != WipConfigurationPriority.Second)
								{
									this.registry = "first";
									return;
								}
								this.cache = "first";
								this.registry = "second";
							}
							if (this.AppConfig == WipConfigurationPriority.Third)
							{
								this.appConfig = "second";
								this.registry = "third";
								return;
							}
							if (this.Cache == WipConfigurationPriority.Third)
							{
								this.cache = "second";
								this.registry = "third";
								return;
							}
							this.registry = "second";
							return;
						}
						else if (value == WipConfigurationPriority.Unused)
						{
							if (this.AppConfig != WipConfigurationPriority.Third && this.Cache != WipConfigurationPriority.Third && this.AppConfig != WipConfigurationPriority.Second && this.Cache != WipConfigurationPriority.Second)
							{
								this.registry = "first";
								return;
							}
							if (this.AppConfig == WipConfigurationPriority.Second)
							{
								this.appConfig = "first";
								this.registry = "unused";
							}
							else
							{
								if (this.Cache != WipConfigurationPriority.Second)
								{
									this.registry = "first";
									return;
								}
								this.cache = "first";
								this.registry = "unused";
							}
							if (this.AppConfig == WipConfigurationPriority.Third)
							{
								this.appConfig = "second";
								this.registry = "unused";
								return;
							}
							if (this.Cache == WipConfigurationPriority.Third)
							{
								this.cache = "second";
								this.registry = "unused";
								return;
							}
							this.registry = "unused";
							return;
						}
					}
				}
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x00096FDD File Offset: 0x000951DD
		// (set) Token: 0x0600318D RID: 12685 RVA: 0x00096FEF File Offset: 0x000951EF
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

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600318E RID: 12686 RVA: 0x000A629B File Offset: 0x000A449B
		// (set) Token: 0x0600318F RID: 12687 RVA: 0x000A62DC File Offset: 0x000A44DC
		[TypeConverter(typeof(ReadOrderCacheDropDownList))]
		[Description("Setting cache to first, second, third causes the TI Runtime to use the AppFabric Cache to obtain configuration information. Setting Cache to unused prevents the TI Runtime from using the AppFabric Cache. The location of the Cache is obtained from the AppFabricCache properties. If the AppFabricCache properties are not set, then Cache should be set to Unused.")]
		[Category("General")]
		[DisplayName("Cache")]
		public WipConfigurationPriority Cache
		{
			get
			{
				if (this.cache == "first")
				{
					return WipConfigurationPriority.First;
				}
				if (this.cache == "second")
				{
					return WipConfigurationPriority.Second;
				}
				if (this.cache == "third")
				{
					return WipConfigurationPriority.Third;
				}
				return WipConfigurationPriority.Unused;
			}
			set
			{
				if (this.AppConfig == WipConfigurationPriority.Unused && this.Registry == WipConfigurationPriority.Unused)
				{
					this.cache = "first";
					return;
				}
				string text = this.cache.ToLowerInvariant();
				if (text != null)
				{
					if (!(text == "first"))
					{
						if (!(text == "second"))
						{
							if (!(text == "third"))
							{
								if (!(text == "unused"))
								{
									return;
								}
								if (value == WipConfigurationPriority.First)
								{
									if (this.Registry == WipConfigurationPriority.Third)
									{
										this.registry = "unused";
									}
									else if (this.AppConfig == WipConfigurationPriority.Third)
									{
										this.appConfig = "unused";
									}
									if (this.Registry == WipConfigurationPriority.Second)
									{
										this.registry = "third";
									}
									else if (this.AppConfig == WipConfigurationPriority.Second)
									{
										this.appConfig = "third";
									}
									if (this.Registry == WipConfigurationPriority.First)
									{
										this.registry = "second";
									}
									else if (this.AppConfig == WipConfigurationPriority.First)
									{
										this.appConfig = "second";
									}
									this.cache = "first";
									return;
								}
								if (value == WipConfigurationPriority.Second)
								{
									if (this.Registry == WipConfigurationPriority.Third)
									{
										this.cache = "unused";
									}
									else if (this.AppConfig == WipConfigurationPriority.Third)
									{
										this.cache = "unused";
									}
									if (this.Registry == WipConfigurationPriority.Second)
									{
										this.cache = "third";
									}
									else if (this.AppConfig == WipConfigurationPriority.Second)
									{
										this.cache = "third";
									}
									this.cache = "second";
									return;
								}
								if (value == WipConfigurationPriority.Third)
								{
									if (this.Registry == WipConfigurationPriority.Third || this.AppConfig == WipConfigurationPriority.Third)
									{
										if (this.Registry == WipConfigurationPriority.Third)
										{
											this.cache = "unused";
										}
										else if (this.AppConfig == WipConfigurationPriority.Third)
										{
											this.cache = "unused";
										}
										this.cache = "third";
										return;
									}
									if (this.Registry == WipConfigurationPriority.Second || this.AppConfig == WipConfigurationPriority.Second)
									{
										this.cache = "third";
										return;
									}
									this.cache = "second";
									return;
								}
								else if (value == WipConfigurationPriority.Unused)
								{
									this.cache = "unused";
									return;
								}
							}
							else
							{
								if (value == WipConfigurationPriority.First)
								{
									if (this.AppConfig == WipConfigurationPriority.Third)
									{
										this.appConfig = "unused";
									}
									else if (this.Registry == WipConfigurationPriority.Third)
									{
										this.registry = "unused";
									}
									if (this.AppConfig == WipConfigurationPriority.Second)
									{
										this.appConfig = "third";
									}
									else if (this.Registry == WipConfigurationPriority.Second)
									{
										this.registry = "third";
									}
									if (this.AppConfig == WipConfigurationPriority.First)
									{
										this.appConfig = "second";
									}
									else if (this.Registry == WipConfigurationPriority.First)
									{
										this.registry = "second";
									}
									this.cache = "first";
									return;
								}
								if (value == WipConfigurationPriority.Second)
								{
									if (this.AppConfig == WipConfigurationPriority.Second)
									{
										this.cache = "second";
										this.appConfig = "third";
										return;
									}
									if (this.Registry == WipConfigurationPriority.Second)
									{
										this.cache = "second";
										this.registry = "third";
										return;
									}
									this.cache = "first";
									return;
								}
								else
								{
									if (value == WipConfigurationPriority.Third)
									{
										this.cache = "third";
										return;
									}
									if (value == WipConfigurationPriority.Unused)
									{
										this.cache = "unused";
										return;
									}
								}
							}
						}
						else if (value == WipConfigurationPriority.First)
						{
							if (this.AppConfig == WipConfigurationPriority.First)
							{
								this.cache = "first";
								this.appConfig = "second";
								return;
							}
							if (this.Registry == WipConfigurationPriority.First)
							{
								this.cache = "first";
								this.registry = "second";
								return;
							}
						}
						else
						{
							if (value == WipConfigurationPriority.Second)
							{
								this.cache = "second";
								return;
							}
							if (value == WipConfigurationPriority.Third)
							{
								if (this.AppConfig == WipConfigurationPriority.Third || this.Registry == WipConfigurationPriority.Third)
								{
									if (this.AppConfig == WipConfigurationPriority.Third)
									{
										this.appConfig = "second";
									}
									else if (this.Registry == WipConfigurationPriority.Third)
									{
										this.registry = "second";
									}
									this.cache = "third";
									return;
								}
								if (this.AppConfig == WipConfigurationPriority.Second || this.Registry == WipConfigurationPriority.Second)
								{
									if (this.AppConfig == WipConfigurationPriority.Second)
									{
										this.appConfig = "first";
									}
									else if (this.Registry == WipConfigurationPriority.Second)
									{
										this.registry = "first";
									}
									this.cache = "second";
									return;
								}
								this.cache = "second";
								return;
							}
							else if (value == WipConfigurationPriority.Unused)
							{
								if (this.AppConfig == WipConfigurationPriority.Third || this.Registry == WipConfigurationPriority.Third)
								{
									if (this.AppConfig == WipConfigurationPriority.Third)
									{
										this.appConfig = "second";
									}
									else if (this.Registry == WipConfigurationPriority.Third)
									{
										this.registry = "second";
									}
									this.cache = "unused";
									return;
								}
								this.cache = "unused";
								return;
							}
						}
					}
					else
					{
						if (value == WipConfigurationPriority.First)
						{
							this.cache = "first";
							return;
						}
						if (value == WipConfigurationPriority.Second)
						{
							if (this.AppConfig == WipConfigurationPriority.Second)
							{
								this.appConfig = "first";
								this.cache = "second";
								return;
							}
							if (this.Registry == WipConfigurationPriority.Second)
							{
								this.registry = "first";
								this.cache = "second";
								return;
							}
							this.cache = "first";
							return;
						}
						else if (value == WipConfigurationPriority.Third)
						{
							if (this.AppConfig != WipConfigurationPriority.Third && this.Registry != WipConfigurationPriority.Third && this.AppConfig != WipConfigurationPriority.Second && this.Registry != WipConfigurationPriority.Second)
							{
								this.cache = "first";
								return;
							}
							if (this.AppConfig == WipConfigurationPriority.Second)
							{
								this.appConfig = "first";
								this.cache = "second";
							}
							else
							{
								if (this.Registry != WipConfigurationPriority.Second)
								{
									this.cache = "first";
									return;
								}
								this.registry = "first";
								this.cache = "second";
							}
							if (this.AppConfig == WipConfigurationPriority.Third)
							{
								this.appConfig = "second";
								this.cache = "third";
								return;
							}
							if (this.Registry == WipConfigurationPriority.Third)
							{
								this.registry = "second";
								this.cache = "third";
								return;
							}
							this.cache = "second";
							return;
						}
						else if (value == WipConfigurationPriority.Unused)
						{
							if (this.AppConfig != WipConfigurationPriority.Third && this.Registry != WipConfigurationPriority.Third && this.AppConfig != WipConfigurationPriority.Second && this.Registry != WipConfigurationPriority.Second)
							{
								this.cache = "first";
								return;
							}
							if (this.AppConfig == WipConfigurationPriority.Second)
							{
								this.appConfig = "first";
								this.cache = "unused";
							}
							else
							{
								if (this.Registry != WipConfigurationPriority.Second)
								{
									this.cache = "first";
									return;
								}
								this.registry = "first";
								this.cache = "unused";
							}
							if (this.AppConfig == WipConfigurationPriority.Third)
							{
								this.appConfig = "second";
								this.cache = "unused";
								return;
							}
							if (this.Registry == WipConfigurationPriority.Third)
							{
								this.registry = "second";
								this.cache = "unused";
								return;
							}
							this.cache = "unused";
							return;
						}
					}
				}
			}
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000A6928 File Offset: 0x000A4B28
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x000A69A8 File Offset: 0x000A4BA8
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C5D RID: 7261
		private GetReadOrderCallbackType _getReadOrderCallback;
	}
}
