using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x020003F8 RID: 1016
	public abstract class ConfigurationManagerBase : IConfigurationManager, IConfigurationDiagnostics, IConfigurationProviderOwner, IShuttable, IIdentifiable
	{
		// Token: 0x06001F2E RID: 7982 RVA: 0x00074B58 File Offset: 0x00072D58
		protected ConfigurationManagerBase(IConfigurationManagerHost host, IEnumerable<IConfigurationProvider> providers)
		{
			this.m_changeHandlerToGroup = new Dictionary<CcsEventHandler, HashSet<Type>>();
			this.m_changeHandlerToSection = new Dictionary<ConfigurationSectionEventHandler, HashSet<string>>();
			this.m_providerConfigurations = new Dictionary<Type, Dictionary<string, IConfigurationClass>>();
			this.m_configurationSnapshot = new Dictionary<Type, IConfigurationClass>();
			this.m_settingsSnapshot = new Dictionary<string, ConfigurationSection>();
			this.m_workTicketManager = new WorkTicketManager(base.GetType().Name);
			this.m_restartRequiredConfigurationClasses = new List<Type>();
			this.m_callgate = new CallGate();
			this.m_lock = new object();
			this.m_providers = providers.ToList<IConfigurationProvider>();
			this.ConfigurationManagerHost = host;
			foreach (IConfigurationProvider configurationProvider in this.m_providers)
			{
				this.UpdateConfiguration(configurationProvider, configurationProvider.Start(this), NotificationOptions.Sync);
				this.UpdateConfiguration(configurationProvider.GetInitialConfiguration(), NotificationOptions.Sync);
			}
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00074C48 File Offset: 0x00072E48
		public void Subscribe([NotNull] IEnumerable<Type> configurationTypes, [NotNull] CcsEventHandler registeredEventHandler)
		{
			using (this.ConfigurationManagerHost.ActivityFactory.CreateSyncActivity(SingletonActivityType<CcsSubscribeActivity>.Instance))
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<Type>>(configurationTypes, "configurationTypes");
				ExtendedDiagnostics.EnsureArgumentNotNull<CcsEventHandler>(registeredEventHandler, "registeredEventHandler");
				TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Subscribing to {0} configuration class types: {1}", new object[]
				{
					configurationTypes.Count<Type>(),
					string.Join<Type>(", ", configurationTypes)
				});
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
					if (this.m_changeHandlerToGroup.ContainsKey(registeredEventHandler))
					{
						throw new CcsEventAlreadySubscribedException("Registered event handler can be used only once");
					}
					foreach (Type type2 in hashSet)
					{
						TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Configuration subscription - Adding type {0}", new object[] { type2.FullName });
						if (!typeof(IConfigurationClass).IsAssignableFrom(type2))
						{
							throw new ConfigurationException("The type {0} is not a valid configuration class type".FormatWithInvariantCulture(new object[] { type2 }));
						}
						if (!this.m_providerConfigurations.ContainsKey(type2))
						{
							throw new CcsNoConfigurationDataExistException("The configuration type {0} was not found".FormatWithInvariantCulture(new object[] { type2.FullName }));
						}
						bool flag2 = ConfigurationRootAttribute.GetConfigurationRootDefinition(type2).Options.HasFlag(ConfigurationOptions.AutoReconfigure);
						if (!this.m_restartRequiredConfigurationClasses.Contains(type2) && !flag2)
						{
							TraceSourceBase<ConfigurationTrace>.Tracer.TraceVerbose("Configuration of type {0} is not AutoReconfigurable", new object[] { type2.FullName });
							this.m_restartRequiredConfigurationClasses.Add(type2);
						}
					}
					this.m_changeHandlerToGroup.Add(registeredEventHandler, hashSet);
					TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Calling group event handler (first time)");
					try
					{
						this.NotifySubscribers(new Dictionary<CcsEventHandler, HashSet<Type>> { { registeredEventHandler, hashSet } }, this.m_configurationSnapshot.Keys, NotificationOptions.Sync);
					}
					catch (CallgateAlreadyOwnedException ex)
					{
						NestedSubscribeException ex2 = new NestedSubscribeException(null, ex);
						TraceSourceBase<ConfigurationTrace>.Tracer.TraceError("Caught CallgateAlreadyOwnedException and throwing wrapped Exception: {0}", new object[] { ex2 });
						throw ex2;
					}
				}
			}
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00074F94 File Offset: 0x00073194
		public void Subscribe([NotNull] IEnumerable<string> configurationSections, [NotNull] ConfigurationSectionEventHandler registeredEventHandler)
		{
			using (this.ConfigurationManagerHost.ActivityFactory.CreateSyncActivity(SingletonActivityType<CcsSubscribeActivity>.Instance))
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<string>>(configurationSections, "configurationSections");
				ExtendedDiagnostics.EnsureArgumentNotNull<ConfigurationSectionEventHandler>(registeredEventHandler, "registeredEventHandler");
				TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Subscribing to {0} configuration class types: {1}", new object[]
				{
					configurationSections.Count<string>(),
					string.Join(", ", configurationSections)
				});
				object @lock = this.m_lock;
				lock (@lock)
				{
					if (this.m_changeHandlerToSection.ContainsKey(registeredEventHandler))
					{
						throw new CcsEventAlreadySubscribedException("Registered event handler can be used only once");
					}
					this.m_changeHandlerToSection.Add(registeredEventHandler, configurationSections.ToHashSet<string>());
					TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Calling group event handler (first time)");
					try
					{
						this.NotifySubscribers(new Dictionary<ConfigurationSectionEventHandler, HashSet<string>> { 
						{
							registeredEventHandler,
							configurationSections.ToHashSet<string>()
						} }, this.m_settingsSnapshot.Keys, NotificationOptions.Sync);
					}
					catch (CallgateAlreadyOwnedException ex)
					{
						NestedSubscribeException ex2 = new NestedSubscribeException(null, ex);
						TraceSourceBase<ConfigurationTrace>.Tracer.TraceError("Caught CallgateAlreadyOwnedException and throwing wrapped Exception: {0}", new object[] { ex2 });
						throw ex2;
					}
				}
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000750D4 File Offset: 0x000732D4
		private void NotifySubscribers(Dictionary<ConfigurationSectionEventHandler, HashSet<string>> subscriptions, IEnumerable<string> updates, NotificationOptions options)
		{
			object @lock = this.m_lock;
			bool flag = false;
			try
			{
				Monitor.Enter(@lock, ref flag);
				IEnumerable<KeyValuePair<ConfigurationSectionEventHandler, HashSet<string>>> enumerable = subscriptions.Where((KeyValuePair<ConfigurationSectionEventHandler, HashSet<string>> ctg) => ctg.Value.Where(new Func<string, bool>(updates.Contains<string>)).Any<string>());
				Func<string, ConfigurationSection> <>9__5;
				IEnumerable<<>f__AnonymousType2<ConfigurationSectionEventHandler, Dictionary<string, ConfigurationSection>>> groupsToBeUpdated = enumerable.Select(delegate(KeyValuePair<ConfigurationSectionEventHandler, HashSet<string>> g)
				{
					ConfigurationSectionEventHandler key = g.Key;
					IEnumerable<string> value = g.Value;
					Func<string, string> func = (string k) => k;
					Func<string, ConfigurationSection> func2;
					if ((func2 = <>9__5) == null)
					{
						func2 = (<>9__5 = (string v) => this.m_settingsSnapshot[v]);
					}
					return new
					{
						Handler = key,
						UpdatedSettings = value.ToDictionary(func, func2)
					};
				});
				if (!options.Equals(NotificationOptions.Sync))
				{
					this.m_callgate.CallAsync(delegate(object context)
					{
						foreach (var <>f__AnonymousType in groupsToBeUpdated)
						{
							foreach (KeyValuePair<string, ConfigurationSection> keyValuePair in <>f__AnonymousType.UpdatedSettings)
							{
								<>f__AnonymousType.Handler(keyValuePair.Key, keyValuePair.Value.Settings);
							}
						}
					}, null, CallGateAsyncOptions.PropagateCallContext);
				}
				else
				{
					this.m_callgate.CallSync(delegate(object context)
					{
						foreach (var <>f__AnonymousType2 in groupsToBeUpdated)
						{
							foreach (KeyValuePair<string, ConfigurationSection> keyValuePair2 in <>f__AnonymousType2.UpdatedSettings)
							{
								<>f__AnonymousType2.Handler(keyValuePair2.Key, keyValuePair2.Value.Settings);
							}
						}
					}, null);
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(@lock);
				}
			}
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000751A0 File Offset: 0x000733A0
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

		// Token: 0x06001F33 RID: 7987 RVA: 0x0007521C File Offset: 0x0007341C
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

		// Token: 0x06001F34 RID: 7988 RVA: 0x00058580 File Offset: 0x00056780
		public IConfigurationDiagnostics GetDiagnostics()
		{
			return this;
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00075298 File Offset: 0x00073498
		public IDictionary<Type, IConfigurationClass> GetConfigurationSnapshot()
		{
			object @lock = this.m_lock;
			IDictionary<Type, IConfigurationClass> dictionary;
			lock (@lock)
			{
				dictionary = this.m_configurationSnapshot.ToDictionary((KeyValuePair<Type, IConfigurationClass> kvp) => kvp.Key, (KeyValuePair<Type, IConfigurationClass> kvp) => kvp.Value.Clone() as IConfigurationClass);
			}
			return dictionary;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00075320 File Offset: 0x00073520
		public void Stop()
		{
			this.m_providers.ForEach(delegate(IConfigurationProvider p)
			{
				p.Stop();
			});
			this.m_workTicketManager.Stop();
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00075357 File Offset: 0x00073557
		public void WaitForStopToComplete()
		{
			this.m_providers.ForEach(delegate(IConfigurationProvider p)
			{
				p.WaitForStopToComplete();
			});
			this.m_workTicketManager.WaitForStopToComplete();
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00075390 File Offset: 0x00073590
		public void Shutdown()
		{
			this.m_providers.ForEach(delegate(IConfigurationProvider p)
			{
				p.Shutdown();
			});
			this.m_workTicketManager.Shutdown();
			this.m_restartRequiredConfigurationClasses = null;
			this.m_changeHandlerToGroup = null;
			this.m_providerConfigurations = null;
			this.m_configurationSnapshot = null;
			this.ConfigurationManagerHost = null;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x0000E56B File Offset: 0x0000C76B
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x000753F8 File Offset: 0x000735F8
		public void UpdateConfiguration(IConfigurationProvider provider, Dictionary<Type, IConfigurationClass> newConfigurationPairs, NotificationOptions options)
		{
			WorkTicket workTicket = this.m_workTicketManager.TryCreateWorkTicket(this);
			if (workTicket == null)
			{
				TraceSourceBase<ConfigurationTrace>.Tracer.TraceWarning("UpdateConfiguration is invoked during shutdown, ignoring");
				return;
			}
			using (workTicket)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					if (!this.ValidateConfigurationCanUpdateAtRuntime(provider, newConfigurationPairs))
					{
						ExtendedDiagnostics.TerminateCurrentProcess(0);
					}
					else
					{
						List<Type> list = new List<Type>();
						foreach (KeyValuePair<Type, IConfigurationClass> keyValuePair in newConfigurationPairs)
						{
							Type key = keyValuePair.Key;
							IConfigurationClass value = keyValuePair.Value;
							Dictionary<string, IConfigurationClass> dictionary;
							if (!this.m_providerConfigurations.TryGetValue(key, out dictionary))
							{
								dictionary = new Dictionary<string, IConfigurationClass>();
								this.m_providerConfigurations.Add(key, dictionary);
							}
							dictionary[provider.Name] = value;
							IConfigurationClass configurationClass;
							if (!this.m_configurationSnapshot.TryGetValue(key, out configurationClass) || !configurationClass.Equals(value))
							{
								list.Add(key);
								this.m_configurationSnapshot[key] = value;
							}
						}
						TraceSourceBase<ConfigurationTrace>.Tracer.TraceInformation("Updated types are: [{0}]", new object[] { list.StringJoin(", ") });
						this.NotifySubscribers(this.m_changeHandlerToGroup, list, options);
					}
				}
			}
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00075598 File Offset: 0x00073798
		public void UpdateConfiguration(IEnumerable<ConfigurationSection> newSettings, NotificationOptions options)
		{
			WorkTicket workTicket = this.m_workTicketManager.TryCreateWorkTicket(this);
			if (workTicket == null)
			{
				TraceSourceBase<ConfigurationTrace>.Tracer.TraceWarning("UpdateConfiguration is invoked during shutdown, ignoring");
				return;
			}
			using (workTicket)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					List<string> list = new List<string>();
					foreach (ConfigurationSection configurationSection in newSettings)
					{
						ConfigurationSection configurationSection2;
						if (!this.m_settingsSnapshot.TryGetValue(configurationSection.SectionName, out configurationSection2) || !configurationSection2.Equals(configurationSection))
						{
							list.Add(configurationSection.SectionName);
							this.m_settingsSnapshot[configurationSection.SectionName] = configurationSection;
						}
					}
					TraceSourceBase<ConfigurationTrace>.Tracer.TraceInformation("Updated types are: [{0}]", new object[] { list.StringJoin(", ") });
					this.NotifySubscribers(this.m_changeHandlerToSection, list, options);
				}
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x000756C0 File Offset: 0x000738C0
		// (set) Token: 0x06001F3D RID: 7997 RVA: 0x000756C8 File Offset: 0x000738C8
		public IConfigurationManagerHost ConfigurationManagerHost { get; private set; }

		// Token: 0x06001F3E RID: 7998 RVA: 0x000756D4 File Offset: 0x000738D4
		private void NotifySubscribers(Dictionary<CcsEventHandler, HashSet<Type>> subscriptions, IEnumerable<Type> updates, NotificationOptions options)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				Func<Type, IConfigurationClass> <>9__3;
				IEnumerable<ConfigurationContainer> enumerable = subscriptions.Where((KeyValuePair<CcsEventHandler, HashSet<Type>> ctg) => ctg.Value.Where(new Func<Type, bool>(updates.Contains<Type>)).Any<Type>()).Select(delegate(KeyValuePair<CcsEventHandler, HashSet<Type>> g)
				{
					CcsEventHandler key = g.Key;
					IEnumerable<Type> value = g.Value;
					Func<Type, Type> func = (Type k) => k;
					Func<Type, IConfigurationClass> func2;
					if ((func2 = <>9__3) == null)
					{
						func2 = (<>9__3 = (Type v) => this.m_configurationSnapshot[v].Clone() as IConfigurationClass);
					}
					return new ConfigurationContainer(key, value.ToDictionary(func, func2));
				}).Materialize<ConfigurationContainer>();
				if (!options.Equals(NotificationOptions.Sync))
				{
					this.m_callgate.CallAsync(new Action<object>(this.UpdateGroupsWithNewConfiguration), enumerable, CallGateAsyncOptions.PropagateCallContext);
				}
				else
				{
					this.m_callgate.CallSync(new Action<object>(this.UpdateGroupsWithNewConfiguration), enumerable);
				}
			}
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00075794 File Offset: 0x00073994
		private bool ValidateConfigurationCanUpdateAtRuntime(IConfigurationProvider provider, Dictionary<Type, IConfigurationClass> newConfigurationPairs)
		{
			if (this.m_changeHandlerToGroup.None<KeyValuePair<CcsEventHandler, HashSet<Type>>>())
			{
				return true;
			}
			IEnumerable<Type> enumerable = from cp in newConfigurationPairs
				where !this.m_providerConfigurations.ContainsKey(cp.Key) || !this.m_providerConfigurations[cp.Key].ContainsKey(provider.Name) || !this.m_providerConfigurations[cp.Key][provider.Name].Equals(cp.Value)
				select cp into p
				select p.Key into m
				join r in this.m_restartRequiredConfigurationClasses on m equals r
				select m;
			if (enumerable.Any<Type>())
			{
				TraceSourceBase<ConfigurationTrace>.Tracer.TraceInformation("{0} got request to update configuration with types [{1}] that require restart in order to take place", new object[]
				{
					base.GetType(),
					enumerable.StringJoin(", ")
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x0007589C File Offset: 0x00073A9C
		private void UpdateGroupsWithNewConfiguration(object context)
		{
			IEnumerable<ConfigurationContainer> enumerable = context as IEnumerable<ConfigurationContainer>;
			TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Info, "About to notify {0} groups of configuration changes", new object[] { enumerable.Count<ConfigurationContainer>() });
			if (enumerable.Any<ConfigurationContainer>())
			{
				using (this.ConfigurationManagerHost.ActivityFactory.CreateSyncActivity(SingletonActivityType<CcsSubscriberNotificationsActivity>.Instance))
				{
					foreach (ConfigurationContainer configurationContainer in enumerable)
					{
						configurationContainer.RaiseEvent();
					}
				}
			}
		}

		// Token: 0x04000AEE RID: 2798
		private Dictionary<CcsEventHandler, HashSet<Type>> m_changeHandlerToGroup;

		// Token: 0x04000AEF RID: 2799
		private Dictionary<ConfigurationSectionEventHandler, HashSet<string>> m_changeHandlerToSection;

		// Token: 0x04000AF0 RID: 2800
		private Dictionary<Type, Dictionary<string, IConfigurationClass>> m_providerConfigurations;

		// Token: 0x04000AF1 RID: 2801
		private Dictionary<Type, IConfigurationClass> m_configurationSnapshot;

		// Token: 0x04000AF2 RID: 2802
		private Dictionary<string, ConfigurationSection> m_settingsSnapshot;

		// Token: 0x04000AF3 RID: 2803
		private List<Type> m_restartRequiredConfigurationClasses;

		// Token: 0x04000AF4 RID: 2804
		private readonly WorkTicketManager m_workTicketManager;

		// Token: 0x04000AF5 RID: 2805
		private readonly CallGate m_callgate;

		// Token: 0x04000AF6 RID: 2806
		private readonly object m_lock;

		// Token: 0x04000AF7 RID: 2807
		private readonly List<IConfigurationProvider> m_providers;
	}
}
