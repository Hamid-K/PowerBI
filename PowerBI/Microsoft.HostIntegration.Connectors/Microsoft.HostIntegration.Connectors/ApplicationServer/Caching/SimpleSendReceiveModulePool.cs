using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020003A5 RID: 933
	internal class SimpleSendReceiveModulePool
	{
		// Token: 0x06002128 RID: 8488 RVA: 0x00065E64 File Offset: 0x00064064
		internal SimpleSendReceiveModule GetItemFromPool(string name, DataCacheSecurity dataCacheSecurity, DataCacheTransportProperties transportProps, string id, object verifyObject, VerifyResponseCallback verifyCallback, TimeSpan chnlOpenTimeout, TimeSpan sendTimeout, int maxChannelCount, IEndpointIdentityProvider endpointIdentityProvider, bool useLegacyProtocol)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			SimpleSendReceiveModule simpleSendReceiveModule2;
			lock (this._poolItems)
			{
				KeyValuePair<SimpleSendReceiveModule, int> keyValuePair;
				SimpleSendReceiveModule simpleSendReceiveModule;
				if (this._poolItems.TryGetValue(name, out keyValuePair))
				{
					simpleSendReceiveModule = keyValuePair.Key;
				}
				else
				{
					simpleSendReceiveModule = new SimpleSendReceiveModule(dataCacheSecurity, transportProps, id, verifyObject, verifyCallback, chnlOpenTimeout, sendTimeout, maxChannelCount, endpointIdentityProvider, useLegacyProtocol);
					keyValuePair = new KeyValuePair<SimpleSendReceiveModule, int>(simpleSendReceiveModule, 0);
					this._poolItems[name] = keyValuePair;
				}
				this.IncrementRefCount(name);
				simpleSendReceiveModule2 = simpleSendReceiveModule;
			}
			return simpleSendReceiveModule2;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00065F08 File Offset: 0x00064108
		internal bool ReturnItemToPool(string name)
		{
			bool flag2;
			lock (this._poolItems)
			{
				flag2 = this.DecrementRefCount(name);
			}
			return flag2;
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00065F4C File Offset: 0x0006414C
		private void IncrementRefCount(string name)
		{
			KeyValuePair<SimpleSendReceiveModule, int> keyValuePair;
			if (this._poolItems.TryGetValue(name, out keyValuePair))
			{
				KeyValuePair<SimpleSendReceiveModule, int> keyValuePair2 = new KeyValuePair<SimpleSendReceiveModule, int>(keyValuePair.Key, keyValuePair.Value + 1);
				this._poolItems[name] = keyValuePair2;
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00065F90 File Offset: 0x00064190
		private bool DecrementRefCount(string name)
		{
			KeyValuePair<SimpleSendReceiveModule, int> keyValuePair;
			if (!this._poolItems.TryGetValue(name, out keyValuePair))
			{
				return false;
			}
			if (keyValuePair.Value == 1)
			{
				keyValuePair.Key.Dispose();
				this._poolItems.Remove(name);
				return true;
			}
			new KeyValuePair<SimpleSendReceiveModule, int>(keyValuePair.Key, keyValuePair.Value - 1);
			return true;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00065FEC File Offset: 0x000641EC
		protected override void Finalize()
		{
			try
			{
				lock (this._poolItems)
				{
					foreach (KeyValuePair<string, KeyValuePair<SimpleSendReceiveModule, int>> keyValuePair in this._poolItems)
					{
						keyValuePair.Value.Key.Dispose();
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04001528 RID: 5416
		private const string _logSource = "SimpleSendReceiveModulePool";

		// Token: 0x04001529 RID: 5417
		private readonly Dictionary<string, KeyValuePair<SimpleSendReceiveModule, int>> _poolItems = new Dictionary<string, KeyValuePair<SimpleSendReceiveModule, int>>();
	}
}
