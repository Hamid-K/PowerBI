using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.OAuth;
using Microsoft.Mashup.Storage;
using Microsoft.Mashup.Storage.Memory;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000050 RID: 80
	internal class ResourceCredentialHandlers
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x0000E37C File Offset: 0x0000C57C
		public ResourceCredentialCollection RefreshCredential(ConnectionContext context, ResourceCredentialCollection previousCredentials)
		{
			DataSource dataSource = new DataSource(previousCredentials.Resource);
			IDictionary<DataSource, DataSourceSetting> newDataSourceSettings = this.GetNewDataSourceSettings(ProviderErrorStrings.AboutToExpire, "CredentialNeedsRefresh", dataSource);
			DataSourceSetting dataSourceSetting;
			if (newDataSourceSettings != null && newDataSourceSettings.TryGetValue(dataSource, out dataSourceSetting))
			{
				if (previousCredentials.FirstOrDefault<IResourceCredential>() is OAuthCredential)
				{
					TokenCredential tokenCredential = dataSourceSetting.AsTokenCredential();
					if (((tokenCredential != null) ? tokenCredential.AccessToken : null) == null)
					{
						goto IL_005B;
					}
				}
				return dataSourceSetting.CreateCredentialCollection(previousCredentials.Resource, dataSource);
			}
			IL_005B:
			return null;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
		public ResourceCredentialCollection GetCredentials(ConnectionContext context, IResource resource)
		{
			DataSource dataSource = new DataSource(resource);
			IDictionary<DataSource, DataSourceSetting> newDataSourceSettings = this.GetNewDataSourceSettings(ProviderErrorStrings.Evaluation_Challenge_Result_PermissionRequired(resource.Kind, resource.Path), "CredentialMissing", dataSource);
			ResourceCredentialCollection resourceCredentialCollection;
			if (newDataSourceSettings != null && this.GetCredentialManager(context, newDataSourceSettings).TryGetCredentials(new Resource(resource), out resourceCredentialCollection))
			{
				return resourceCredentialCollection;
			}
			return null;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000E438 File Offset: 0x0000C638
		public bool IsQueryExecutionPermitted(ConnectionContext context, IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
		{
			EventHandler<DataSourceSettingNeededEventArgs> eventHandler = this.DataSourceSettingNeeded.EventHandler;
			if (eventHandler == null || (!MashupConnection.EnableInformationProtection && resource.Kind == "MicrosoftInformationProtection"))
			{
				return false;
			}
			DataSource dataSource = new DataSource(resource);
			string text = DataSourceProperties.FromQueryPermission(type);
			MashupPermission mashupPermission = new MashupPermission(text, query);
			DataSourceSettingNeededEventArgs dataSourceSettingNeededEventArgs = new DataSourceSettingNeededEventArgs(new MashupPermissionException(ProviderErrorStrings.MissingPermission(resource.Kind, resource.Path, text), dataSource, mashupPermission));
			eventHandler(this, dataSourceSettingNeededEventArgs);
			DataSourceSetting dataSourceSetting;
			return dataSourceSettingNeededEventArgs.NewSettings != null && dataSourceSettingNeededEventArgs.NewSettings.TryGetValue(dataSource, out dataSourceSetting) && dataSourceSetting.Permissions.Contains(mashupPermission);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
		public bool TryUpdateFirewallGroup(ConnectionContext context, IResource resource, FirewallGroup2 originalGroup, IValue traits, out FirewallGroup2 newGroup)
		{
			EventHandler<DataSourceSettingNeededEventArgs> eventHandler = this.DataSourceSettingNeeded.EventHandler;
			if (eventHandler != null)
			{
				DataSource dataSource = new DataSource(resource);
				MashupSecurityException ex;
				if (traits == null)
				{
					ex = new MashupPrivacySettingException(ProviderErrorStrings.MissingPrivacySetting, new DataSource[] { dataSource }, null);
				}
				else
				{
					ex = new MashupPrivacyTraitException(ProviderErrorStrings.MissingPrivacySetting, dataSource, originalGroup.ToDataSourceSetting(), Util.ToJsonText(traits), null);
				}
				DataSourceSettingNeededEventArgs dataSourceSettingNeededEventArgs = new DataSourceSettingNeededEventArgs(ex);
				eventHandler(this, dataSourceSettingNeededEventArgs);
				DataSourceSetting dataSourceSetting;
				if (dataSourceSettingNeededEventArgs.NewSettings != null && dataSourceSettingNeededEventArgs.NewSettings.TryGetValue(dataSource, out dataSourceSetting) && dataSourceSetting.PrivacySetting != null)
				{
					FirewallRule firewallRule = dataSourceSetting.ToFirewallRule(dataSource);
					newGroup = new FirewallGroup2((FirewallGroupType2)firewallRule.GroupType, firewallRule.IsTrusted.GetValueOrDefault(), firewallRule.Resource, firewallRule.GroupName);
					return true;
				}
			}
			newGroup = null;
			return false;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		private CredentialManager GetCredentialManager(ConnectionContext context, IDictionary<DataSource, DataSourceSetting> newSettings)
		{
			List<Credential> list = new List<Credential>(newSettings.Count);
			foreach (KeyValuePair<DataSource, DataSourceSetting> keyValuePair in newSettings)
			{
				DataSource key = keyValuePair.Key;
				DataSourceSetting value = keyValuePair.Value;
				if (value.AuthenticationKind != null)
				{
					MashupConnection.ValidateSetting(key, value);
					list.Add(value.MakeCredential(key.NormalizedResource, key));
				}
			}
			return new CredentialManager(null, new MemoryCredentialsStorage(list), false, MashupConnection.AllowWindowsCredentials, context.ThreadIdentity);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E640 File Offset: 0x0000C840
		private IDictionary<DataSource, DataSourceSetting> GetNewDataSourceSettings(string message, string reason, DataSource dataSource)
		{
			EventHandler<DataSourceSettingNeededEventArgs> eventHandler = this.DataSourceSettingNeeded.EventHandler;
			if (eventHandler == null || (!MashupConnection.EnableInformationProtection && dataSource.Kind == "MicrosoftInformationProtection"))
			{
				return null;
			}
			DataSourceSettingNeededEventArgs dataSourceSettingNeededEventArgs = new DataSourceSettingNeededEventArgs(new MashupCredentialException(message, reason, dataSource));
			eventHandler(this, dataSourceSettingNeededEventArgs);
			return dataSourceSettingNeededEventArgs.NewSettings;
		}

		// Token: 0x040001DB RID: 475
		public ContextAwareEvent<DataSourceSettingNeededEventArgs> DataSourceSettingNeeded = new ContextAwareEvent<DataSourceSettingNeededEventArgs>();
	}
}
