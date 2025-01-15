using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200056D RID: 1389
	public class ReadOrder : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x06002F25 RID: 12069 RVA: 0x000A1CE4 File Offset: 0x0009FEE4
		public void SetReadOrderCallBack(GetReadOrderCallbackType getReadOrderCallback)
		{
			this._getReadOrderCallback = getReadOrderCallback;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000A1CED File Offset: 0x0009FEED
		public ReadOrder GetReadOrder()
		{
			return this._getReadOrderCallback();
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x00096E2B File Offset: 0x0009502B
		// (set) Token: 0x06002F28 RID: 12072 RVA: 0x00096E3D File Offset: 0x0009503D
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

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x000A1CFA File Offset: 0x0009FEFA
		// (set) Token: 0x06002F2A RID: 12074 RVA: 0x000A1D28 File Offset: 0x0009FF28
		[TypeConverter(typeof(ReadOrderAppConfigDropDownList))]
		[Description("Setting appConfig to First or Second causes the TI Runtime to use the App or Web Configuration file to obtain configuration information. Setting config to Unused prevents the TI Runtime from using App or Web Configuration file.")]
		[Category("General")]
		[DisplayName("App Config")]
		public HipConfigurationPriority AppConfig
		{
			get
			{
				if (this.appConfig == "first")
				{
					return HipConfigurationPriority.First;
				}
				if (this.appConfig == "second")
				{
					return HipConfigurationPriority.Second;
				}
				return HipConfigurationPriority.Unused;
			}
			set
			{
				if (this.Cache == HipConfigurationPriority.Unused)
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
							if (value == HipConfigurationPriority.First)
							{
								if (this.Cache == HipConfigurationPriority.First)
								{
									this.cache = "second";
								}
								this.appConfig = "first";
								return;
							}
							if (value == HipConfigurationPriority.Second)
							{
								if (this.Cache == HipConfigurationPriority.First)
								{
									this.appConfig = "second";
								}
								return;
							}
							if (value == HipConfigurationPriority.Unused)
							{
								this.appConfig = "unused";
								return;
							}
						}
						else if (value == HipConfigurationPriority.First)
						{
							if (this.Cache == HipConfigurationPriority.First)
							{
								this.appConfig = "first";
								this.cache = "second";
								return;
							}
						}
						else
						{
							if (value == HipConfigurationPriority.Second)
							{
								this.appConfig = "second";
								return;
							}
							if (value == HipConfigurationPriority.Unused)
							{
								this.appConfig = "unused";
								return;
							}
						}
					}
					else
					{
						if (value == HipConfigurationPriority.First)
						{
							this.appConfig = "first";
							return;
						}
						if (value == HipConfigurationPriority.Second)
						{
							if (this.Cache == HipConfigurationPriority.Second)
							{
								this.cache = "first";
								this.appConfig = "second";
								return;
							}
							this.appConfig = "first";
							return;
						}
						else if (value == HipConfigurationPriority.Unused)
						{
							if (this.Cache == HipConfigurationPriority.Second)
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

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x00096FDD File Offset: 0x000951DD
		// (set) Token: 0x06002F2C RID: 12076 RVA: 0x00096FEF File Offset: 0x000951EF
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

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x000A1E8D File Offset: 0x000A008D
		// (set) Token: 0x06002F2E RID: 12078 RVA: 0x000A1EB8 File Offset: 0x000A00B8
		[TypeConverter(typeof(ReadOrderCacheDropDownList))]
		[Description("Setting cache to First or  Second causes the TI Runtime to use the AppFabric Cache to obtain configuration information. Setting Cache to Unused prevents the TI Runtime from using the AppFabric Cache. The location of the Cache is obtained from the AppFabricCache properties. If the AppFabricCache properties are not set, then Cache should be set to Unused.")]
		[Category("General")]
		[DisplayName("Cache")]
		public HipConfigurationPriority Cache
		{
			get
			{
				if (this.cache == "first")
				{
					return HipConfigurationPriority.First;
				}
				if (this.cache == "second")
				{
					return HipConfigurationPriority.Second;
				}
				return HipConfigurationPriority.Unused;
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
							if (value == HipConfigurationPriority.First)
							{
								if (this.AppConfig == HipConfigurationPriority.First)
								{
									this.appConfig = "second";
									this.cache = "first";
								}
								return;
							}
							if (value == HipConfigurationPriority.Second)
							{
								if (this.AppConfig == HipConfigurationPriority.First)
								{
									this.cache = "second";
								}
								return;
							}
							if (value == HipConfigurationPriority.Unused)
							{
								this.cache = "unused";
								return;
							}
						}
						else if (value == HipConfigurationPriority.First)
						{
							if (this.AppConfig == HipConfigurationPriority.First)
							{
								this.cache = "first";
								this.appConfig = "second";
								return;
							}
						}
						else
						{
							if (value == HipConfigurationPriority.Second)
							{
								this.cache = "second";
								return;
							}
							if (value == HipConfigurationPriority.Unused)
							{
								this.cache = "unused";
								return;
							}
						}
					}
					else
					{
						if (value == HipConfigurationPriority.First)
						{
							this.cache = "first";
							return;
						}
						if (value == HipConfigurationPriority.Second)
						{
							if (this.AppConfig == HipConfigurationPriority.Second)
							{
								this.appConfig = "first";
								this.cache = "second";
								return;
							}
							this.cache = "first";
							return;
						}
						else if (value == HipConfigurationPriority.Unused)
						{
							if (this.AppConfig == HipConfigurationPriority.Second)
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

		// Token: 0x06002F2F RID: 12079 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000A200C File Offset: 0x000A020C
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

		// Token: 0x06002F39 RID: 12089 RVA: 0x000A208C File Offset: 0x000A028C
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

		// Token: 0x06002F3A RID: 12090 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C25 RID: 7205
		private GetReadOrderCallbackType _getReadOrderCallback;
	}
}
