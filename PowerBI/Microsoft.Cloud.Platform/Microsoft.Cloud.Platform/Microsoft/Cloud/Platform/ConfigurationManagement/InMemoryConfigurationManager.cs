using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x0200040C RID: 1036
	public sealed class InMemoryConfigurationManager : IConfigurationManager
	{
		// Token: 0x06001F81 RID: 8065 RVA: 0x00075F90 File Offset: 0x00074190
		public InMemoryConfigurationManager()
		{
			this.m_changeHandlerToGroup = new Dictionary<CcsEventHandler, HashSet<Type>>();
			this.m_changeHandlerToSection = new Dictionary<ConfigurationSectionEventHandler, HashSet<string>>();
			this.m_updatedSettings = new Dictionary<string, ConfigurationSection>();
			this.m_updatedConfigurations = new Dictionary<Type, IConfigurationClass>();
			this.m_restartRequiredConfigurationClasses = new List<Type>();
			this.m_lock = new object();
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00075FE8 File Offset: 0x000741E8
		public void UpdateConfiguration(IEnumerable<IConfigurationClass> configurationClasses)
		{
			List<Type> list = new List<Type>();
			object @lock = this.m_lock;
			lock (@lock)
			{
				foreach (KeyValuePair<Type, IConfigurationClass> keyValuePair in configurationClasses.ToDictionary((IConfigurationClass configClass) => configClass.GetType(), (IConfigurationClass configClass) => configClass))
				{
					Type key = keyValuePair.Key;
					IConfigurationClass value = keyValuePair.Value;
					if (this.m_restartRequiredConfigurationClasses.Contains(key))
					{
						throw new ConfigurationException("Cannot update configuration of type {0} since it's not auto-reconfigure".FormatWithInvariantCulture(new object[] { key.Name }));
					}
					if (!this.m_updatedConfigurations.ContainsKey(key) || !this.m_updatedConfigurations[key].Equals(value))
					{
						this.m_updatedConfigurations[key] = value;
						list.Add(key);
					}
				}
				this.NotifySubscribers(list);
			}
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00076144 File Offset: 0x00074344
		public void InvokeConfigurationChangeEvent<T>()
		{
			this.NotifySubscribers(new List<Type> { typeof(T) });
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00076164 File Offset: 0x00074364
		public void Subscribe(IEnumerable<Type> configurationTypes, CcsEventHandler registeredEventHandler)
		{
			HashSet<Type> hashSet = new HashSet<Type>();
			foreach (Type type in configurationTypes)
			{
				if (typeof(IConfigurationParser).IsAssignableFrom(type))
				{
					object[] customAttributes = type.GetCustomAttributes(typeof(ConfigurationParserAttribute), true);
					hashSet.AddRange(from attribute in customAttributes
						select attribute as ConfigurationParserAttribute into configParserAttribute
						select configParserAttribute.ConfigurationType);
				}
				else
				{
					hashSet.Add(type);
				}
			}
			object @lock = this.m_lock;
			lock (@lock)
			{
				foreach (Type type2 in hashSet)
				{
					if (this.m_changeHandlerToGroup.ContainsKey(registeredEventHandler))
					{
						throw new CcsEventAlreadySubscribedException("Registered event handler can be used only once");
					}
					if (!typeof(IConfigurationClass).IsAssignableFrom(type2))
					{
						throw new ConfigurationException(string.Format(CultureInfo.CurrentCulture, "The type {0} is not a valid configuration class type", new object[] { type2 }));
					}
					bool flag2 = ConfigurationRootAttribute.GetConfigurationRootDefinition(type2).Options.HasFlag(ConfigurationOptions.AutoReconfigure);
					if (!this.m_restartRequiredConfigurationClasses.Contains(type2) && !flag2)
					{
						this.m_restartRequiredConfigurationClasses.Add(type2);
					}
				}
				this.m_changeHandlerToGroup.Add(registeredEventHandler, hashSet);
				this.NotifySubscribers(hashSet);
			}
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00076338 File Offset: 0x00074538
		public void Subscribe(IEnumerable<string> sections, ConfigurationSectionEventHandler registerEventHandler)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_changeHandlerToSection.ContainsKey(registerEventHandler))
				{
					throw new CcsEventAlreadySubscribedException("Registered event handler can be used only once");
				}
				this.m_changeHandlerToSection.Add(registerEventHandler, sections.ToHashSet<string>());
				this.NotifySubscribers(sections);
			}
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000763A4 File Offset: 0x000745A4
		public void Unsubscribe([NotNull] CcsEventHandler registeredEventHandler)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<CcsEventHandler>(registeredEventHandler, "registeredEventHandler");
			TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Unsubscribing configuration group");
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_changeHandlerToGroup.ContainsKey(registeredEventHandler))
				{
					throw new ConfigurationException("The event handler given was not subscribed");
				}
				this.m_changeHandlerToGroup.Remove(registeredEventHandler);
			}
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00076420 File Offset: 0x00074620
		public void Unsubscribe([NotNull] ConfigurationSectionEventHandler registeredEventHandler)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ConfigurationSectionEventHandler>(registeredEventHandler, "registeredEventHandler");
			TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Unsubscribing configuration group");
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (!this.m_changeHandlerToSection.ContainsKey(registeredEventHandler))
				{
					throw new ConfigurationException("The event handler given was not subscribed");
				}
				this.m_changeHandlerToSection.Remove(registeredEventHandler);
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00014B8A File Offset: 0x00012D8A
		public IConfigurationDiagnostics GetDiagnostics()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0007649C File Offset: 0x0007469C
		private void NotifySubscribers(IEnumerable<Type> modifiedTypes)
		{
			foreach (ConfigurationContainer configurationContainer in from ctg in this.m_changeHandlerToGroup
				where ctg.Value.Where(new Func<Type, bool>(modifiedTypes.Contains<Type>)).Any<Type>()
				select ctg into g
				select new ConfigurationContainer(g.Key, g.Value.ToDictionary((Type k) => k, new Func<Type, IConfigurationClass>(this.CloneUpdatedConfiguration))))
			{
				configurationContainer.RaiseEvent();
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0007651C File Offset: 0x0007471C
		private void NotifySubscribers(IEnumerable<string> modifiedSections)
		{
			Func<string, ConfigurationSection> <>9__3;
			foreach (var <>f__AnonymousType in this.m_changeHandlerToSection.Where((KeyValuePair<ConfigurationSectionEventHandler, HashSet<string>> ctg) => ctg.Value.Where(new Func<string, bool>(modifiedSections.Contains<string>)).Any<string>()).Select(delegate(KeyValuePair<ConfigurationSectionEventHandler, HashSet<string>> g)
			{
				ConfigurationSectionEventHandler key = g.Key;
				IEnumerable<string> value = g.Value;
				Func<string, string> func = (string k) => k;
				Func<string, ConfigurationSection> func2;
				if ((func2 = <>9__3) == null)
				{
					func2 = (<>9__3 = (string v) => this.m_updatedSettings[v]);
				}
				return new
				{
					Handler = key,
					UpdatedSettings = value.ToDictionary(func, func2)
				};
			}))
			{
				foreach (KeyValuePair<string, ConfigurationSection> keyValuePair in <>f__AnonymousType.UpdatedSettings)
				{
					<>f__AnonymousType.Handler(keyValuePair.Key, keyValuePair.Value.Settings);
				}
			}
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000765F4 File Offset: 0x000747F4
		private IConfigurationClass CloneUpdatedConfiguration(Type key)
		{
			IConfigurationClass configurationClass;
			try
			{
				configurationClass = this.m_updatedConfigurations[key].Clone() as IConfigurationClass;
			}
			catch (KeyNotFoundException ex)
			{
				throw new KeyNotFoundException(string.Concat(new object[] { ex.Message, " Key value is: '", key, "'" }));
			}
			return configurationClass;
		}

		// Token: 0x04000B08 RID: 2824
		private Dictionary<CcsEventHandler, HashSet<Type>> m_changeHandlerToGroup;

		// Token: 0x04000B09 RID: 2825
		private Dictionary<ConfigurationSectionEventHandler, HashSet<string>> m_changeHandlerToSection;

		// Token: 0x04000B0A RID: 2826
		private Dictionary<string, ConfigurationSection> m_updatedSettings;

		// Token: 0x04000B0B RID: 2827
		private Dictionary<Type, IConfigurationClass> m_updatedConfigurations;

		// Token: 0x04000B0C RID: 2828
		private List<Type> m_restartRequiredConfigurationClasses;

		// Token: 0x04000B0D RID: 2829
		private object m_lock;
	}
}
