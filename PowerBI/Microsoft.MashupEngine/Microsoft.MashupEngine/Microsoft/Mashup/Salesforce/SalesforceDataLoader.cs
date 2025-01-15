using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Library.Web;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001DF RID: 479
	internal class SalesforceDataLoader
	{
		// Token: 0x06000978 RID: 2424 RVA: 0x00013438 File Offset: 0x00011638
		public SalesforceDataLoader(IEngineHost host, string loginUrl, OptionsRecord optionsMap)
		{
			this.host = host;
			this.loginUrl = loginUrl;
			this.resource = Resource.New("Salesforce", loginUrl);
			this.ValidateSalesforceLoginUrl(this.resource, this.loginUrl);
			this.instanceUrl = this.GetInstanceUrl(this.resource, out this.credentials);
			object obj;
			if (optionsMap.TryGetValue("ApiVersion", out obj))
			{
				this.apiVersion = (double)obj;
				if (this.apiVersion < 29.0 || this.apiVersion > 99.9 || !SalesforceDataLoader.IsInteger(this.apiVersion * 10.0))
				{
					throw ValueException.NewExpressionError<Message1>(Strings.InvalidSalesforceApiVersion(this.apiVersion.ToString(CultureInfo.InvariantCulture)), null, null);
				}
				this.isApiVersionSpecified = true;
			}
			else
			{
				this.apiVersion = 29.0;
				this.isApiVersionSpecified = false;
			}
			Value value;
			if (optionsMap.TryGetValue("Timeout", out value))
			{
				this.timeout = value;
			}
			this.servicePath = string.Format(CultureInfo.InvariantCulture, "/services/data/v{0:0.0}", this.apiVersion);
			this.credentialHash = this.credentials.GetHash();
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001356C File Offset: 0x0001176C
		public double ApiVersion
		{
			get
			{
				return this.apiVersion;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00013574 File Offset: 0x00011774
		public string InstanceUrl
		{
			get
			{
				return this.instanceUrl;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0001357C File Offset: 0x0001177C
		public string LoginUrl
		{
			get
			{
				return this.loginUrl;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00013584 File Offset: 0x00011784
		public IThreadPoolService ThreadPool
		{
			get
			{
				return this.host.QueryService<IThreadPoolService>();
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00013591 File Offset: 0x00011791
		public string ServicePath
		{
			get
			{
				return this.servicePath;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00013599 File Offset: 0x00011799
		public string ObjectListPath
		{
			get
			{
				return this.ServicePath + "/" + "sobjects";
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x000135B0 File Offset: 0x000117B0
		public string SoqlPath
		{
			get
			{
				return this.ServicePath + "/query?q=";
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x000135C2 File Offset: 0x000117C2
		public string ReportsPath
		{
			get
			{
				return this.ServicePath + "/analytics/reports";
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x000135D4 File Offset: 0x000117D4
		public string CredentialHash
		{
			get
			{
				return this.credentialHash;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x000135DC File Offset: 0x000117DC
		public IEngineHost EngineHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000135E4 File Offset: 0x000117E4
		public string DefaultDescribePath(string name)
		{
			return string.Concat(new string[] { this.ObjectListPath, "/", name, "/", "describe" });
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00013618 File Offset: 0x00011818
		public Value LoadJsonValue(string path, string cacheKey)
		{
			Value value;
			using (StreamReader streamReader = this.OpenStream(path, cacheKey))
			{
				value = JsonParser.Parse(streamReader, null);
			}
			return value;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00013654 File Offset: 0x00011854
		public JsonTokenizer StreamJsonValue(string path, string cacheKey)
		{
			return new JsonTokenizer(this.OpenStream(path, cacheKey), true, true, null);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00013668 File Offset: 0x00011868
		public JsonTokenizer StreamJsonValueOutsideOfEngineContext(string path, string cacheKey)
		{
			StreamReader streamReader;
			using (EngineContext.Enter())
			{
				streamReader = this.OpenStream(path, cacheKey);
			}
			return new JsonTokenizer(streamReader, true, true, null);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x000136B0 File Offset: 0x000118B0
		public string CreateCacheKey(params string[] keyParts)
		{
			PersistentCacheKeyBuilder persistentCacheKeyBuilder = new PersistentCacheKeyBuilder();
			persistentCacheKeyBuilder.Add("Salesforce.Data/3");
			persistentCacheKeyBuilder.Add(this.ApiVersion.ToString(CultureInfo.InvariantCulture));
			persistentCacheKeyBuilder.Add(this.CredentialHash);
			foreach (string text in keyParts)
			{
				persistentCacheKeyBuilder.Add(text);
			}
			return persistentCacheKeyBuilder.ToString();
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00013716 File Offset: 0x00011916
		private static bool IsInteger(double value)
		{
			return (double)((int)value) == value;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00013720 File Offset: 0x00011920
		private string GetWarningMessage(Value responseHeaders, string message)
		{
			Value value;
			if (responseHeaders != null && responseHeaders.IsRecord && responseHeaders.AsRecord.TryGetValue("Warning", out value))
			{
				message = string.Format(CultureInfo.InvariantCulture, Strings.Salesforce_Warning(message, value.AsString), Array.Empty<object>());
			}
			return message;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00013770 File Offset: 0x00011970
		private StreamReader OpenStream(string path, string cacheKey)
		{
			path = this.InstanceUrl + path;
			Uri uri;
			if (!UriHelper.TryCreateAbsoluteUri(path, out uri))
			{
				throw UriErrors.InputInvalid(path);
			}
			IEngineHost engineHost = this.host;
			string kind = this.resource.Kind;
			string nonNormalizedPath = this.resource.NonNormalizedPath;
			TextValue textValue = TextValue.New(path);
			Value value = null;
			Value value2 = null;
			string text = null;
			Value value3 = null;
			int[] array = SalesforceDataLoader.expectedErrors;
			BinaryValue binaryValue = WebContentsBinaryValue.New(Request.Create(engineHost, kind, nonNormalizedPath, textValue, value, value2, text, value3, this.timeout, array, null, null, null, null), this.credentials, Value.Null, false, null);
			if (cacheKey != null)
			{
				binaryValue = new CachedMetadataBinaryValue(this.host.GetPersistentCache(), cacheKey, binaryValue);
			}
			StreamReader streamReader2;
			try
			{
				StreamReader streamReader = binaryValue.OpenText(Encoding.UTF8);
				Value value4;
				binaryValue.TryGetMetaField("Headers", out value4);
				Value value5;
				if (binaryValue.TryGetMetaField("Response.Status", out value5) && SalesforceDataLoader.expectedErrors.Contains(value5.AsNumber.AsInteger32))
				{
					string text2 = streamReader.ReadToEnd();
					using (new StringReader(text2))
					{
						Value value6;
						Value value7;
						if (JsonParser.TryParse(new StringReader(text2), out value6) && value6.IsList && value6.AsList[0].IsRecord && value6.AsList[0].AsRecord.TryGetValue("message", out value7) && value7.IsText)
						{
							throw ValueException.NewDataSourceError(this.GetWarningMessage(value4, value7.AsString), value6, null);
						}
						throw ValueException.NewDataSourceError(this.GetWarningMessage(value4, text2), Value.Null, null);
					}
				}
				streamReader2 = streamReader;
			}
			catch (ValueException ex)
			{
				throw this.TranslateValueException(ex);
			}
			return streamReader2;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00013918 File Offset: 0x00011B18
		private string GetInstanceUrl(IResource resource, out ResourceCredentialCollection credentials)
		{
			credentials = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, resource, null);
			string text;
			if (credentials.Count != 1 || !(credentials[0] is OAuthCredential) || !((OAuthCredential)credentials[0]).Properties.TryGetValue("instance_url", out text))
			{
				throw DataSourceException.NewInvalidCredentialsError(this.host, resource, null, null, null);
			}
			Uri uri;
			if (!UriHelper.TryCreateAbsoluteUri(text, out uri))
			{
				throw DataSourceException.NewDataSourceError<Message1>(this.host, Strings.InvalidSalesforceUri(text), resource, null, null);
			}
			return text.TrimEnd(new char[] { '/' });
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000139B0 File Offset: 0x00011BB0
		private Uri ValidateSalesforceLoginUrl(IResource resource, string resourceUrl)
		{
			try
			{
				Uri uri = new Uri(resourceUrl);
				if (uri.Host.EndsWith("salesforce.com", StringComparison.Ordinal) || uri.Host.EndsWith("cloudforce.com", StringComparison.Ordinal))
				{
					if (uri.Scheme != Uri.UriSchemeHttps)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.UriInvalidHttps, TextValue.New(resourceUrl), null);
					}
					return uri;
				}
			}
			catch (UriFormatException)
			{
			}
			catch (ArgumentException)
			{
			}
			throw DataSourceException.NewDataSourceError<Message1>(this.host, Strings.InvalidSalesforceLoginUri(resourceUrl), resource, null, null);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00013A4C File Offset: 0x00011C4C
		private ValueException TranslateValueException(ValueException valueException)
		{
			WebException ex = valueException.InnerException as WebException;
			if (this.ApiVersion < 31.0 && ex != null)
			{
				WrappingHttpWebResponse wrappingHttpWebResponse = ex.Response as WrappingHttpWebResponse;
				if (wrappingHttpWebResponse != null && (wrappingHttpWebResponse.StatusCode == HttpStatusCode.Gone || wrappingHttpWebResponse.StatusCode == HttpStatusCode.NotFound))
				{
					if (this.isApiVersionSpecified)
					{
						return ValueException.NewDataSourceError<Message1>(Strings.InvalidSalesforceApiVersion(this.apiVersion), valueException.Value, valueException);
					}
					return ValueException.NewDataSourceError<Message0>(Strings.SalesforceApiVersionRequired, valueException.Value, valueException);
				}
			}
			return valueException;
		}

		// Token: 0x040005AD RID: 1453
		private const string SalesforceDomain = "salesforce.com";

		// Token: 0x040005AE RID: 1454
		private const string CloudforceDomain = "cloudforce.com";

		// Token: 0x040005AF RID: 1455
		private const string InstanceUrlKey = "instance_url";

		// Token: 0x040005B0 RID: 1456
		private static readonly int[] expectedErrors = new int[] { 400, 500 };

		// Token: 0x040005B1 RID: 1457
		private readonly IEngineHost host;

		// Token: 0x040005B2 RID: 1458
		private readonly IResource resource;

		// Token: 0x040005B3 RID: 1459
		private readonly string instanceUrl;

		// Token: 0x040005B4 RID: 1460
		private readonly string loginUrl;

		// Token: 0x040005B5 RID: 1461
		private readonly ResourceCredentialCollection credentials;

		// Token: 0x040005B6 RID: 1462
		private readonly string servicePath;

		// Token: 0x040005B7 RID: 1463
		private readonly string credentialHash;

		// Token: 0x040005B8 RID: 1464
		private readonly double apiVersion;

		// Token: 0x040005B9 RID: 1465
		private readonly bool isApiVersionSpecified;

		// Token: 0x040005BA RID: 1466
		private readonly Value timeout;
	}
}
