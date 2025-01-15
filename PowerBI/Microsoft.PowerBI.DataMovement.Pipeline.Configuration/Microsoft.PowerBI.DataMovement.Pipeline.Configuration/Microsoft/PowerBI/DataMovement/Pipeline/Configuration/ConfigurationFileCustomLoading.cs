using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Configuration
{
	// Token: 0x02000007 RID: 7
	[NullableContext(1)]
	[Nullable(0)]
	public class ConfigurationFileCustomLoading : IComponent, IDisposable, ISite, IServiceProvider
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		public ConfigurationFileCustomLoading(Configuration configuration, Type type)
			: this(configuration, (type != null) ? type.FullName : null)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020C8 File Offset: 0x000002C8
		public ConfigurationFileCustomLoading(Configuration configuration, string sectionName)
		{
			this.m_configuration = configuration;
			this.Init(this.m_configuration, sectionName);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020E4 File Offset: 0x000002E4
		public ConfigurationFileCustomLoading(Assembly assembly, Type type)
			: this(assembly, (type != null) ? type.FullName : null)
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020FC File Offset: 0x000002FC
		public ConfigurationFileCustomLoading(Assembly assembly, string sectionName)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (string.IsNullOrEmpty(sectionName))
			{
				throw new ArgumentNullException("sectionName");
			}
			if (assembly.GlobalAssemblyCache)
			{
				throw new ArgumentException("Assemblies loaded from the GAC are not supported.");
			}
			string text = new Uri(assembly.CodeBase).LocalPath;
			if (string.IsNullOrEmpty(assembly.Location))
			{
				string directoryName = Path.GetDirectoryName(text);
				string text2 = ((assembly.EntryPoint == null) ? ".dll" : ".exe");
				text = Path.Combine(directoryName, assembly.GetName().Name + text2 + ".config");
				this.m_configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
				{
					ExeConfigFilename = text
				}, ConfigurationUserLevel.None);
			}
			else
			{
				this.m_configuration = ConfigurationManager.OpenExeConfiguration(text);
			}
			this.Init(this.m_configuration, sectionName);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D7 File Offset: 0x000003D7
		public ConfigurationFileCustomLoading(string path, Type type)
			: this(path, (type != null) ? type.FullName : null)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021EC File Offset: 0x000003EC
		public ConfigurationFileCustomLoading(string path, string sectionName)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException(path);
			}
			if (string.IsNullOrEmpty(sectionName))
			{
				throw new ArgumentNullException(sectionName);
			}
			path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
			this.m_configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
			{
				ExeConfigFilename = path
			}, ConfigurationUserLevel.None);
			this.Init(this.m_configuration, sectionName);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600000D RID: 13 RVA: 0x00002254 File Offset: 0x00000454
		// (remove) Token: 0x0600000E RID: 14 RVA: 0x0000228C File Offset: 0x0000048C
		public event EventHandler Disposed;

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022C1 File Offset: 0x000004C1
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000022C4 File Offset: 0x000004C4
		ISite IComponent.Site
		{
			get
			{
				return this;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022CB File Offset: 0x000004CB
		IComponent ISite.Component
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022CE File Offset: 0x000004CE
		IContainer ISite.Container
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022D1 File Offset: 0x000004D1
		bool ISite.DesignMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022D4 File Offset: 0x000004D4
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000022DC File Offset: 0x000004DC
		string ISite.Name { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x000022E5 File Offset: 0x000004E5
		private void Init(Configuration configuration, string sectionName)
		{
			this.m_provider = new ConfigurationFileCustomLoading.Provider(configuration, sectionName);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022F4 File Offset: 0x000004F4
		object IServiceProvider.GetService(Type serviceType)
		{
			if (serviceType == typeof(ISettingsProviderService))
			{
				return this.m_provider;
			}
			return null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002310 File Offset: 0x00000510
		void IDisposable.Dispose()
		{
			EventHandler disposed = this.Disposed;
			if (disposed != null)
			{
				disposed(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002334 File Offset: 0x00000534
		public Binding GetBinding(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			ServiceModelSectionGroup sectionGroup = ServiceModelSectionGroup.GetSectionGroup(this.m_configuration);
			if (sectionGroup == null)
			{
				return null;
			}
			BindingsSection bindings = sectionGroup.Bindings;
			if (bindings == null)
			{
				return null;
			}
			foreach (BindingCollectionElement bindingCollectionElement in bindings.BindingCollections)
			{
				for (int i = 0; i < bindingCollectionElement.ConfiguredBindings.Count; i++)
				{
					if (bindingCollectionElement.ConfiguredBindings[i].Name == name)
					{
						IBindingConfigurationElement bindingConfigurationElement = bindingCollectionElement.ConfiguredBindings[0];
						Binding binding = (Binding)Activator.CreateInstance(bindingCollectionElement.BindingType);
						binding.Name = bindingConfigurationElement.Name;
						bindingConfigurationElement.ApplyConfiguration(binding);
						return binding;
					}
				}
			}
			return null;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002424 File Offset: 0x00000624
		public ServiceElement GetServiceConfiguration(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			ServiceModelSectionGroup sectionGroup = ServiceModelSectionGroup.GetSectionGroup(this.m_configuration);
			if (sectionGroup == null)
			{
				return null;
			}
			ServicesSection services = sectionGroup.Services;
			if (services == null)
			{
				return null;
			}
			ServiceElementCollection services2 = services.Services;
			if (services2 == null)
			{
				return null;
			}
			for (int i = 0; i < services2.Count; i++)
			{
				if (services2[i].Name == name)
				{
					return services2[i];
				}
			}
			return null;
		}

		// Token: 0x04000011 RID: 17
		private readonly Configuration m_configuration;

		// Token: 0x04000012 RID: 18
		private ConfigurationFileCustomLoading.Provider m_provider;

		// Token: 0x02000008 RID: 8
		[Nullable(0)]
		private class Provider : SettingsProvider, ISettingsProviderService
		{
			// Token: 0x0600001B RID: 27 RVA: 0x00002498 File Offset: 0x00000698
			internal Provider(Configuration configuration, string sectionName)
			{
				if (configuration == null)
				{
					throw new ArgumentNullException("configuration");
				}
				if (string.IsNullOrEmpty(sectionName))
				{
					throw new ArgumentNullException("sectionName");
				}
				this.m_configuration = configuration;
				this.m_sectionName = sectionName;
				ConfigurationSectionGroup configurationSectionGroup = configuration.GetSectionGroup("applicationSettings");
				this.m_applicationSection = ((configurationSectionGroup == null) ? null : (configurationSectionGroup.Sections[this.m_sectionName] as ClientSettingsSection));
				configurationSectionGroup = configuration.GetSectionGroup("userSettings");
				this.m_userSection = ((configurationSectionGroup == null) ? null : (configurationSectionGroup.Sections[this.m_sectionName] as ClientSettingsSection));
				this.Initialize(base.GetType().Name, null);
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600001C RID: 28 RVA: 0x00002548 File Offset: 0x00000748
			// (set) Token: 0x0600001D RID: 29 RVA: 0x00002550 File Offset: 0x00000750
			public override string ApplicationName { get; set; }

			// Token: 0x0600001E RID: 30 RVA: 0x0000255C File Offset: 0x0000075C
			public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
			{
				SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
				foreach (object obj in collection)
				{
					SettingsProperty settingsProperty = (SettingsProperty)obj;
					SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
					SpecialSettingAttribute specialSettingAttribute = settingsProperty.Attributes[typeof(SpecialSettingAttribute)] as SpecialSettingAttribute;
					if (specialSettingAttribute != null && specialSettingAttribute.SpecialSetting == SpecialSetting.ConnectionString)
					{
						ConnectionStringSettingsCollection connectionStrings = this.m_configuration.ConnectionStrings.ConnectionStrings;
						string text = this.m_sectionName + "." + settingsProperty.Name;
						if (connectionStrings != null && connectionStrings[text] != null)
						{
							settingsPropertyValue.PropertyValue = connectionStrings[text].ConnectionString;
						}
						else if (settingsProperty.DefaultValue != null && settingsProperty.DefaultValue is string)
						{
							settingsPropertyValue.PropertyValue = settingsProperty.DefaultValue;
						}
						else
						{
							settingsPropertyValue.PropertyValue = string.Empty;
						}
						settingsPropertyValue.IsDirty = false;
						settingsPropertyValueCollection.Add(settingsPropertyValue);
					}
					else
					{
						ClientSettingsSection clientSettingsSection = (this.IsUserSetting(settingsProperty) ? this.m_userSection : this.m_applicationSection);
						SettingElement settingElement = null;
						if (clientSettingsSection != null)
						{
							foreach (object obj2 in clientSettingsSection.Settings)
							{
								SettingElement settingElement2 = (SettingElement)obj2;
								if (settingElement2.Name == settingsProperty.Name)
								{
									settingElement = settingElement2;
									break;
								}
							}
						}
						if (settingElement != null)
						{
							string text2 = settingElement.Value.ValueXml.InnerXml;
							if (settingElement.SerializeAs == SettingsSerializeAs.String)
							{
								text2 = settingElement.Value.ValueXml.InnerText;
							}
							settingsPropertyValue.SerializedValue = text2;
						}
						else if (settingsProperty.DefaultValue != null)
						{
							settingsPropertyValue.SerializedValue = settingsProperty.DefaultValue;
						}
						else
						{
							settingsPropertyValue.PropertyValue = null;
						}
						settingsPropertyValue.IsDirty = false;
						settingsPropertyValueCollection.Add(settingsPropertyValue);
					}
				}
				return settingsPropertyValueCollection;
			}

			// Token: 0x0600001F RID: 31 RVA: 0x00002784 File Offset: 0x00000984
			public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000020 RID: 32 RVA: 0x0000278B File Offset: 0x0000098B
			SettingsProvider ISettingsProviderService.GetSettingsProvider(SettingsProperty property)
			{
				return this;
			}

			// Token: 0x06000021 RID: 33 RVA: 0x00002790 File Offset: 0x00000990
			private bool IsUserSetting(SettingsProperty setting)
			{
				bool flag = setting.Attributes[typeof(UserScopedSettingAttribute)] is UserScopedSettingAttribute;
				bool flag2 = setting.Attributes[typeof(ApplicationScopedSettingAttribute)] is ApplicationScopedSettingAttribute;
				if (flag && flag2)
				{
					throw new ConfigurationErrorsException(string.Format(CultureInfo.InvariantCulture, "Both application and user scope is specified for setting '{0}'.", setting.Name));
				}
				if (!flag && !flag2)
				{
					throw new ConfigurationErrorsException(string.Format(CultureInfo.InvariantCulture, "No scope is specified for setting '{0}'.", setting.Name));
				}
				return flag;
			}

			// Token: 0x04000015 RID: 21
			private Configuration m_configuration;

			// Token: 0x04000016 RID: 22
			private ClientSettingsSection m_applicationSection;

			// Token: 0x04000017 RID: 23
			private ClientSettingsSection m_userSection;

			// Token: 0x04000018 RID: 24
			private string m_sectionName;
		}
	}
}
