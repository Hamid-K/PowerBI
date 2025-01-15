using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000CF RID: 207
	internal class SqlAuthenticationProviderManager
	{
		// Token: 0x06000EDF RID: 3807 RVA: 0x0002F0E8 File Offset: 0x0002D2E8
		static SqlAuthenticationProviderManager()
		{
			SqlAuthenticationProviderConfigurationSection sqlAuthenticationProviderConfigurationSection = null;
			try
			{
				sqlAuthenticationProviderConfigurationSection = SqlAuthenticationProviderManager.FetchConfigurationSection<SqlClientAuthenticationProviderConfigurationSection>("SqlClientAuthenticationProviders");
				if (sqlAuthenticationProviderConfigurationSection == null)
				{
					sqlAuthenticationProviderConfigurationSection = SqlAuthenticationProviderManager.FetchConfigurationSection<SqlAuthenticationProviderConfigurationSection>("SqlAuthenticationProviders");
				}
			}
			catch (ConfigurationErrorsException ex)
			{
				SqlClientEventSource.Log.TryTraceEvent<ConfigurationErrorsException>("static SqlAuthenticationProviderManager: Unable to load custom SqlAuthenticationProviders or SqlClientAuthenticationProviders. ConfigurationManager failed to load due to configuration errors: {0}", ex);
			}
			SqlAuthenticationProviderManager.Instance = new SqlAuthenticationProviderManager(sqlAuthenticationProviderConfigurationSection);
			ActiveDirectoryAuthenticationProvider activeDirectoryAuthenticationProvider = new ActiveDirectoryAuthenticationProvider(SqlAuthenticationProviderManager.Instance._applicationClientId);
			SqlAuthenticationProviderManager.Instance.SetProvider(SqlAuthenticationMethod.ActiveDirectoryIntegrated, activeDirectoryAuthenticationProvider);
			SqlAuthenticationProviderManager.Instance.SetProvider(SqlAuthenticationMethod.ActiveDirectoryPassword, activeDirectoryAuthenticationProvider);
			SqlAuthenticationProviderManager.Instance.SetProvider(SqlAuthenticationMethod.ActiveDirectoryInteractive, activeDirectoryAuthenticationProvider);
			SqlAuthenticationProviderManager.Instance.SetProvider(SqlAuthenticationMethod.ActiveDirectoryServicePrincipal, activeDirectoryAuthenticationProvider);
			SqlAuthenticationProviderManager.Instance.SetProvider(SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow, activeDirectoryAuthenticationProvider);
			SqlAuthenticationProviderManager.Instance.SetProvider(SqlAuthenticationMethod.ActiveDirectoryManagedIdentity, activeDirectoryAuthenticationProvider);
			SqlAuthenticationProviderManager.Instance.SetProvider(SqlAuthenticationMethod.ActiveDirectoryMSI, activeDirectoryAuthenticationProvider);
			SqlAuthenticationProviderManager.Instance.SetProvider(SqlAuthenticationMethod.ActiveDirectoryDefault, activeDirectoryAuthenticationProvider);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0002F1BC File Offset: 0x0002D3BC
		public SqlAuthenticationProviderManager(SqlAuthenticationProviderConfigurationSection configSection)
		{
			this._typeName = base.GetType().Name;
			string text = "Ctor";
			this._providers = new ConcurrentDictionary<SqlAuthenticationMethod, SqlAuthenticationProvider>();
			HashSet<SqlAuthenticationMethod> hashSet = new HashSet<SqlAuthenticationMethod>();
			this._authenticationsWithAppSpecifiedProvider = hashSet;
			if (configSection == null)
			{
				this._sqlAuthLogger.LogInfo(this._typeName, text, "Neither SqlClientAuthenticationProviders nor SqlAuthenticationProviders configuration section found.");
				return;
			}
			if (!string.IsNullOrEmpty(configSection.ApplicationClientId))
			{
				this._applicationClientId = configSection.ApplicationClientId;
				this._sqlAuthLogger.LogInfo(this._typeName, text, "Received user-defined Application Client Id");
			}
			else
			{
				this._sqlAuthLogger.LogInfo(this._typeName, text, "No user-defined Application Client Id found.");
			}
			if (!string.IsNullOrEmpty(configSection.InitializerType))
			{
				try
				{
					Type type = Type.GetType(configSection.InitializerType, true);
					this._initializer = (SqlAuthenticationInitializer)Activator.CreateInstance(type);
					this._initializer.Initialize();
				}
				catch (Exception ex)
				{
					throw SQL.CannotCreateSqlAuthInitializer(configSection.InitializerType, ex);
				}
				this._sqlAuthLogger.LogInfo(this._typeName, text, "Created user-defined SqlAuthenticationInitializer.");
			}
			else
			{
				this._sqlAuthLogger.LogInfo(this._typeName, text, "No user-defined SqlAuthenticationInitializer found.");
			}
			if (configSection.Providers != null && configSection.Providers.Count > 0)
			{
				using (IEnumerator enumerator = configSection.Providers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ProviderSettings providerSettings = (ProviderSettings)obj;
						SqlAuthenticationMethod sqlAuthenticationMethod = SqlAuthenticationProviderManager.AuthenticationEnumFromString(providerSettings.Name);
						SqlAuthenticationProvider sqlAuthenticationProvider;
						try
						{
							Type type2 = Type.GetType(providerSettings.Type, true);
							sqlAuthenticationProvider = (SqlAuthenticationProvider)Activator.CreateInstance(type2);
						}
						catch (Exception ex2)
						{
							throw SQL.CannotCreateAuthProvider(sqlAuthenticationMethod.ToString(), providerSettings.Type, ex2);
						}
						if (!sqlAuthenticationProvider.IsSupported(sqlAuthenticationMethod))
						{
							throw SQL.UnsupportedAuthenticationByProvider(sqlAuthenticationMethod.ToString(), providerSettings.Type);
						}
						this._providers[sqlAuthenticationMethod] = sqlAuthenticationProvider;
						hashSet.Add(sqlAuthenticationMethod);
						this._sqlAuthLogger.LogInfo(this._typeName, text, string.Format("Added user-defined auth provider: {0} for authentication {1}.", (providerSettings != null) ? providerSettings.Type : null, sqlAuthenticationMethod));
					}
					return;
				}
			}
			this._sqlAuthLogger.LogInfo(this._typeName, text, "No user-defined auth providers.");
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0002F448 File Offset: 0x0002D648
		public SqlAuthenticationProvider GetProvider(SqlAuthenticationMethod authenticationMethod)
		{
			SqlAuthenticationProvider sqlAuthenticationProvider;
			if (!this._providers.TryGetValue(authenticationMethod, out sqlAuthenticationProvider))
			{
				return null;
			}
			return sqlAuthenticationProvider;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0002F468 File Offset: 0x0002D668
		public bool SetProvider(SqlAuthenticationMethod authenticationMethod, SqlAuthenticationProvider provider)
		{
			if (!provider.IsSupported(authenticationMethod))
			{
				throw SQL.UnsupportedAuthenticationByProvider(authenticationMethod.ToString(), provider.GetType().Name);
			}
			string methodName = "SetProvider";
			if (this._authenticationsWithAppSpecifiedProvider.Contains(authenticationMethod))
			{
				this._sqlAuthLogger.LogError(this._typeName, methodName, string.Format("Failed to add provider {0} because a user-defined provider with type {1} already existed for authentication {2}.", SqlAuthenticationProviderManager.GetProviderType(provider), SqlAuthenticationProviderManager.GetProviderType(this._providers[authenticationMethod]), authenticationMethod));
				return false;
			}
			this._providers.AddOrUpdate(authenticationMethod, provider, delegate(SqlAuthenticationMethod key, SqlAuthenticationProvider oldProvider)
			{
				if (oldProvider != null)
				{
					oldProvider.BeforeUnload(authenticationMethod);
				}
				if (provider != null)
				{
					provider.BeforeLoad(authenticationMethod);
				}
				this._sqlAuthLogger.LogInfo(this._typeName, methodName, string.Format("Added auth provider {0}, overriding existed provider {1} for authentication {2}.", SqlAuthenticationProviderManager.GetProviderType(provider), SqlAuthenticationProviderManager.GetProviderType(oldProvider), authenticationMethod));
				return provider;
			});
			return true;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0002F55C File Offset: 0x0002D75C
		private static T FetchConfigurationSection<T>(string name)
		{
			Type typeFromHandle = typeof(T);
			object section = ConfigurationManager.GetSection(name);
			if (section != null)
			{
				ConfigurationSection configurationSection = section as ConfigurationSection;
				if (configurationSection != null && configurationSection.GetType() == typeFromHandle)
				{
					return (T)((object)section);
				}
				SqlClientEventSource.Log.TraceEvent<string, string>("Found a custom {0} configuration but it is not of type {1}.", name, typeFromHandle.FullName);
			}
			return default(T);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0002F5BC File Offset: 0x0002D7BC
		private static SqlAuthenticationMethod AuthenticationEnumFromString(string authentication)
		{
			string text = authentication.ToLowerInvariant();
			if (text != null)
			{
				int length = text.Length;
				switch (length)
				{
				case 20:
					if (text == "active directory msi")
					{
						return SqlAuthenticationMethod.ActiveDirectoryMSI;
					}
					break;
				case 21:
				case 22:
				case 23:
				case 26:
					break;
				case 24:
					if (text == "active directory default")
					{
						return SqlAuthenticationMethod.ActiveDirectoryDefault;
					}
					break;
				case 25:
					if (text == "active directory password")
					{
						return SqlAuthenticationMethod.ActiveDirectoryPassword;
					}
					break;
				case 27:
					if (text == "active directory integrated")
					{
						return SqlAuthenticationMethod.ActiveDirectoryIntegrated;
					}
					break;
				case 28:
					if (text == "active directory interactive")
					{
						return SqlAuthenticationMethod.ActiveDirectoryInteractive;
					}
					break;
				default:
					if (length != 33)
					{
						if (length == 34)
						{
							if (text == "active directory service principal")
							{
								return SqlAuthenticationMethod.ActiveDirectoryServicePrincipal;
							}
						}
					}
					else
					{
						char c = text[17];
						if (c != 'd')
						{
							if (c == 'm')
							{
								if (text == "active directory managed identity")
								{
									return SqlAuthenticationMethod.ActiveDirectoryManagedIdentity;
								}
							}
						}
						else if (text == "active directory device code flow")
						{
							return SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow;
						}
					}
					break;
				}
			}
			throw SQL.UnsupportedAuthentication(authentication);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0002F6C0 File Offset: 0x0002D8C0
		private static string GetProviderType(SqlAuthenticationProvider provider)
		{
			if (provider == null)
			{
				return "null";
			}
			return provider.GetType().FullName;
		}

		// Token: 0x0400064D RID: 1613
		private const string ActiveDirectoryPassword = "active directory password";

		// Token: 0x0400064E RID: 1614
		private const string ActiveDirectoryIntegrated = "active directory integrated";

		// Token: 0x0400064F RID: 1615
		private const string ActiveDirectoryInteractive = "active directory interactive";

		// Token: 0x04000650 RID: 1616
		private const string ActiveDirectoryServicePrincipal = "active directory service principal";

		// Token: 0x04000651 RID: 1617
		private const string ActiveDirectoryDeviceCodeFlow = "active directory device code flow";

		// Token: 0x04000652 RID: 1618
		private const string ActiveDirectoryManagedIdentity = "active directory managed identity";

		// Token: 0x04000653 RID: 1619
		private const string ActiveDirectoryMSI = "active directory msi";

		// Token: 0x04000654 RID: 1620
		private const string ActiveDirectoryDefault = "active directory default";

		// Token: 0x04000655 RID: 1621
		public static readonly SqlAuthenticationProviderManager Instance;

		// Token: 0x04000656 RID: 1622
		private readonly string _typeName;

		// Token: 0x04000657 RID: 1623
		private readonly SqlAuthenticationInitializer _initializer;

		// Token: 0x04000658 RID: 1624
		private readonly IReadOnlyCollection<SqlAuthenticationMethod> _authenticationsWithAppSpecifiedProvider;

		// Token: 0x04000659 RID: 1625
		private readonly ConcurrentDictionary<SqlAuthenticationMethod, SqlAuthenticationProvider> _providers;

		// Token: 0x0400065A RID: 1626
		private readonly SqlClientLogger _sqlAuthLogger = new SqlClientLogger();

		// Token: 0x0400065B RID: 1627
		private readonly string _applicationClientId = "2fd908ad-0664-4344-b9be-cd3e8b574c38";
	}
}
