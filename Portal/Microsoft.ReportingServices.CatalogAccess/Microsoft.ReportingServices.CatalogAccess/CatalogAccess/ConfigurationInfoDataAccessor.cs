using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.BIServer.HostingEnvironment.Storage;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x0200002A RID: 42
	internal sealed class ConfigurationInfoDataAccessor : IConfigurationInfoDataAccessor, IDisposable
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00006C25 File Offset: 0x00004E25
		public ConfigurationInfoDataAccessor()
		{
			this._sqlAccess = CatalogAccessFactory.NewConnection();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006C38 File Offset: 0x00004E38
		public ConfigurationInfoDataAccessor(ISqlAccess existingSqlAccess)
		{
			this._sqlAccess = ReferenceSqlAccess.UseButDoNotDispose(existingSqlAccess);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006C4C File Offset: 0x00004E4C
		public async Task<IDictionary<string, string>> GetConfigInfoValuesAsync()
		{
			return (await this._sqlAccess.QueryAsync<ConfigurationInfoEntity>("GetAllConfigurationInfo")).ToDictionary((ConfigurationInfoEntity e) => e.Name, (ConfigurationInfoEntity e) => e.Value);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006C94 File Offset: 0x00004E94
		public async Task<int> EnsureDefaults()
		{
			int affectedRows = 0;
			IDictionary<string, string> dictionary = await this.GetConfigInfoValuesAsync();
			IDictionary<string, string> persistedConfig = dictionary;
			foreach (KeyValuePair<string, string> keyValuePair in ConfigSwitchDefaults.Current)
			{
				int num = affectedRows;
				affectedRows = num + await this.EnsureDefaultValueExistsInCatalogInfo(persistedConfig, keyValuePair.Key.ToString(CultureInfo.InvariantCulture), keyValuePair.Value.ToString());
			}
			IEnumerator<KeyValuePair<string, string>> enumerator = null;
			foreach (KeyValuePair<string, string> keyValuePair2 in CatalogConfigDefaults.Current)
			{
				int num = affectedRows;
				affectedRows = num + await this.EnsureDefaultValueExistsInCatalogInfo(persistedConfig, keyValuePair2.Key.ToString(CultureInfo.InvariantCulture), keyValuePair2.Value);
			}
			enumerator = null;
			dictionary = await this.GetConfigInfoValuesAsync();
			Dictionary<string, string> actualConfig = new Dictionary<string, string>(dictionary);
			Dictionary<string, string> newConfig = new Dictionary<string, string>(actualConfig);
			CatalogConfigUpgrade.Upgrade(newConfig);
			foreach (string difference in newConfig.Differences(actualConfig))
			{
				if (newConfig.ContainsKey(difference))
				{
					Interlocked.Add(ref affectedRows, await this.UpdateExistingConfigurationValueInCatalog(difference, newConfig[difference]));
					Logger.Info("Upgrading ConfigurationInfo value. Setting {0} to '{1}'. Original value: {2}", new object[]
					{
						difference,
						newConfig[difference],
						actualConfig[difference]
					});
				}
				else
				{
					Interlocked.Add(ref affectedRows, await this.RemoveConfigurationValue(difference));
					Logger.Info("Removing ConfigurationInfo setting: {0}", new object[] { difference });
				}
				difference = null;
			}
			IEnumerator<string> enumerator2 = null;
			return affectedRows;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006CDC File Offset: 0x00004EDC
		private async Task<int> EnsureDefaultValueExistsInCatalogInfo(IDictionary<string, string> persistedConfig, string key, string defaultValue)
		{
			int num = 0;
			if (!persistedConfig.ContainsKey(key))
			{
				var <>f__AnonymousType = new
				{
					name = key,
					value = defaultValue
				};
				num = await this._sqlAccess.ExecuteAsync("SetConfigurationInfo", <>f__AnonymousType);
			}
			return await Task.FromResult<int>(num);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006D3C File Offset: 0x00004F3C
		private async Task<int> RemoveConfigurationValue(string key)
		{
			var <>f__AnonymousType = new
			{
				Name = key
			};
			return await Task.FromResult<int>(await this._sqlAccess.ExecuteAsync("RemoveConfigurationInfoValue", <>f__AnonymousType));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006D8C File Offset: 0x00004F8C
		private async Task<int> UpdateExistingConfigurationValueInCatalog(string key, string newValue)
		{
			var <>f__AnonymousType = new
			{
				ConfigName = key,
				ConfigValue = newValue
			};
			return await Task.FromResult<int>(await this._sqlAccess.ExecuteAsync("SetConfigurationInfoValue", <>f__AnonymousType));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006DE1 File Offset: 0x00004FE1
		public void Dispose()
		{
			this._sqlAccess.Dispose();
		}

		// Token: 0x040000A6 RID: 166
		private readonly ISqlAccess _sqlAccess;
	}
}
