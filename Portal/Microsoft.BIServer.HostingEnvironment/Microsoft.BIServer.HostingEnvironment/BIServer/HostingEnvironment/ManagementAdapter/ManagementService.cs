using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment.Cryptography;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.BIServer.HostingEnvironment.Request;
using Newtonsoft.Json;

namespace Microsoft.BIServer.HostingEnvironment.ManagementAdapter
{
	// Token: 0x0200002E RID: 46
	internal class ManagementService : IManagementService
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00004F4E File Offset: 0x0000314E
		public ManagementService(Uri managementUrl)
		{
			if (managementUrl == null)
			{
				throw new ArgumentNullException("managementUrl");
			}
			this._managementUrl = managementUrl;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004F74 File Offset: 0x00003174
		public async Task<Crypto> GetCatalogCrypto()
		{
			if (!SymmetricKeyCrypto.Instance.IsInitialized)
			{
				try
				{
					await ManagementService._cryptoSemaphore.WaitAsync();
					if (!SymmetricKeyCrypto.Instance.IsInitialized)
					{
						using (HttpClient httpClient = new HttpClient(new HttpClientHandler
						{
							UseDefaultCredentials = true
						}))
						{
							RequestContext.PassOnCurrentRequestContext(httpClient);
							ManagementService.SetPublicEncryptedSymmetricKey(await ManagementService.GetSymmetricKey(httpClient, this.GetEncryptedSymmetricKeyUrl()));
						}
						HttpClient httpClient = null;
					}
				}
				finally
				{
					ManagementService._cryptoSemaphore.Release();
				}
			}
			return SymmetricKeyCrypto.Instance;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004FBC File Offset: 0x000031BC
		internal static async Task<byte[]> GetSymmetricKey(HttpClient httpClient, Uri encryptedSymmetricKeyUrl)
		{
			HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(encryptedSymmetricKeyUrl);
			HttpResponseMessage response = httpResponseMessage;
			string text = await response.Content.ReadAsStringAsync();
			if (response.StatusCode != HttpStatusCode.OK)
			{
				string text2 = string.Format("Error getting symmetric key. Status code {0}", response.StatusCode);
				Logger.Error(text2, Array.Empty<object>());
				throw new ManagementException(text2);
			}
			return Convert.FromBase64String(text.Substring(1, text.Length - 2));
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000500C File Offset: 0x0000320C
		private static void SetPublicEncryptedSymmetricKey(byte[] symmetricKey)
		{
			try
			{
				SymmetricKeyCrypto.Instance.SetPublicKeyEncryptedSymmetricKey(symmetricKey);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Couldn't import symmetric key", Array.Empty<object>());
				throw;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005048 File Offset: 0x00003248
		private Uri GetEncryptedSymmetricKeyUrl()
		{
			Guid empty = Guid.Empty;
			return new Uri(string.Format("{0}/api/v1.0/Keys/{1}", this._managementUrl, empty));
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005078 File Offset: 0x00003278
		public async Task<Dictionary<ConfigSettings, bool>> GetConfigSwitches()
		{
			HttpResponseMessage httpResponseMessage;
			using (HttpClient httpClient = new HttpClient(new HttpClientHandler
			{
				UseDefaultCredentials = true
			}))
			{
				RequestContext.PassOnCurrentRequestContext(httpClient);
				httpResponseMessage = await httpClient.GetAsync(this.GetConfigInfo());
				if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
				{
					string text = string.Format("Error getting configuration info. Status code {0}", httpResponseMessage.StatusCode);
					Logger.Error(text, Array.Empty<object>());
					throw new ManagementException(text);
				}
			}
			HttpClient httpClient = null;
			Dictionary<ConfigSettings, bool> dictionary2;
			try
			{
				string text2 = await httpResponseMessage.Content.ReadAsStringAsync();
				string[] names = Enum.GetNames(typeof(ConfigSettings));
				IEnumerable<KeyValuePair<string, string>> enumerable = JsonConvert.DeserializeObject<IDictionary<string, string>>(text2);
				Dictionary<ConfigSettings, bool> dictionary = new Dictionary<ConfigSettings, bool>();
				foreach (KeyValuePair<string, string> keyValuePair in enumerable)
				{
					if (names.Contains(keyValuePair.Key))
					{
						dictionary.Add((ConfigSettings)Enum.Parse(typeof(ConfigSettings), keyValuePair.Key), keyValuePair.Value.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase));
					}
				}
				dictionary2 = dictionary;
			}
			catch (Exception)
			{
				string text3 = string.Format("Can't read configuration properties", Array.Empty<object>());
				Logger.Error(text3, Array.Empty<object>());
				throw new ManagementException(text3);
			}
			return dictionary2;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000050BD File Offset: 0x000032BD
		private Uri GetConfigInfo()
		{
			return new Uri(string.Format("{0}/api/v1.0/DBConfigInfo", this._managementUrl));
		}

		// Token: 0x04000090 RID: 144
		private static SemaphoreSlim _cryptoSemaphore = new SemaphoreSlim(1, 1);

		// Token: 0x04000091 RID: 145
		private readonly Uri _managementUrl;
	}
}
