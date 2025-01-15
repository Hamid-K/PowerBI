using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Threading;
using Microsoft.Win32;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000137 RID: 311
	internal static class ConnectivityHelper
	{
		// Token: 0x0600109B RID: 4251 RVA: 0x0003982C File Offset: 0x00037A2C
		public static AsInstanceType GetAsInstanceType(string url)
		{
			if (ConnectivityHelper.HasUriProtocolScheme(url, "asazure"))
			{
				return AsInstanceType.AsAzure;
			}
			if (ConnectivityHelper.HasUriProtocolScheme(url, "pbidedicated"))
			{
				return AsInstanceType.PbiPremiumInternal;
			}
			if (ConnectivityHelper.HasUriProtocolScheme(url, "powerbi"))
			{
				return AsInstanceType.PbiPremiumXmlaEp;
			}
			if (ConnectivityHelper.HasUriProtocolScheme(url, "pbiazure") || string.Compare(url, "https://analysis.windows.net/powerbi/api", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(url, "https://analysis.windows-int.net/powerbi/api", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return AsInstanceType.PbiDataset;
			}
			return AsInstanceType.Other;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00039894 File Offset: 0x00037A94
		public static bool HasUriProtocolScheme(string url, string scheme)
		{
			return !string.IsNullOrEmpty(url) && (url.StartsWith(scheme, StringComparison.InvariantCultureIgnoreCase) && url.Length > scheme.Length + "://".Length) && string.Compare(url, scheme.Length, "://", 0, "://".Length, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x000398F0 File Offset: 0x00037AF0
		public static int UriIndexOfQuery(string url, string query)
		{
			if (string.IsNullOrEmpty(url))
			{
				return -1;
			}
			int num = url.IndexOf('#');
			int num2 = ((num != -1) ? num : url.Length);
			int num3 = url.IndexOf('?');
			if (num3 == -1 || num3 >= num2 - 1)
			{
				return -1;
			}
			int num4 = url.IndexOf(query, num3 + 1, StringComparison.OrdinalIgnoreCase);
			if (num4 > num3 && num4 < num2 && (num4 == num3 + 1 || url[num4 - 1] == '&') && (num4 + query.Length == num2 || url[num4 + query.Length] == '&'))
			{
				return num4;
			}
			return -1;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0003997A File Offset: 0x00037B7A
		public static bool IsHttpUri(string url)
		{
			return ConnectivityHelper.HasUriProtocolScheme(url, Uri.UriSchemeHttp);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00039987 File Offset: 0x00037B87
		public static bool IsHttpsUri(string url)
		{
			return ConnectivityHelper.HasUriProtocolScheme(url, Uri.UriSchemeHttps);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00039994 File Offset: 0x00037B94
		public static bool IsHttpConnection(string url)
		{
			return !string.IsNullOrEmpty(url) && (ConnectivityHelper.HasUriProtocolScheme(url, Uri.UriSchemeHttp) || ConnectivityHelper.HasUriProtocolScheme(url, Uri.UriSchemeHttps) || ConnectivityHelper.HasUriProtocolScheme(url, "asazure") || ConnectivityHelper.HasUriProtocolScheme(url, "pbidedicated") || ConnectivityHelper.HasUriProtocolScheme(url, "powerbi") || ConnectivityHelper.HasUriProtocolScheme(url, "pbiazure") || ConnectivityHelper.HasUriProtocolScheme(url, "link"));
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00039A06 File Offset: 0x00037C06
		public static bool IsHttpsConnection(string url)
		{
			return !string.IsNullOrEmpty(url) && (ConnectivityHelper.HasUriProtocolScheme(url, Uri.UriSchemeHttps) || ConnectivityHelper.HasUriProtocolScheme(url, "powerbi"));
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00039A2C File Offset: 0x00037C2C
		public static bool IsPaaSInfrastructureConnection(string url)
		{
			return !string.IsNullOrEmpty(url) && (ConnectivityHelper.HasUriProtocolScheme(url, "asazure") || ConnectivityHelper.HasUriProtocolScheme(url, "pbidedicated") || ConnectivityHelper.HasUriProtocolScheme(url, "powerbi") || ConnectivityHelper.HasUriProtocolScheme(url, "pbiazure"));
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00039A6C File Offset: 0x00037C6C
		public static bool IsPbiPremiumConnection(string url)
		{
			return !string.IsNullOrEmpty(url) && (ConnectivityHelper.HasUriProtocolScheme(url, "pbidedicated") || ConnectivityHelper.HasUriProtocolScheme(url, "powerbi"));
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00039A94 File Offset: 0x00037C94
		public static Uri GetV3DiscoveryUri()
		{
			UriBuilder uriBuilder = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Microsoft Power BI"))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue("PowerBIDiscoveryUrl", null);
						if (value != null)
						{
							uriBuilder = new UriBuilder(value.ToString());
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (ObjectDisposedException)
			{
			}
			catch (IOException)
			{
			}
			if (uriBuilder == null)
			{
				uriBuilder = new UriBuilder("https://api.powerbi.com/");
			}
			if ("pbiazure".Equals(uriBuilder.Scheme, StringComparison.OrdinalIgnoreCase))
			{
				uriBuilder.Scheme = Uri.UriSchemeHttps;
			}
			uriBuilder.Path = "powerbi/globalservice/v201606/environments/discover";
			uriBuilder.Query = "client=powerbi-msolap";
			return uriBuilder.Uri;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x00039B74 File Offset: 0x00037D74
		public static HttpStatusCode ExecuteJsonBasedHttpGetRequest<TResult>(Uri uri, IDictionary<string, string> headers, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<object, object>(uri, "GET", headers, null, options, timeout, null, null, false, false, out httpStatusCode, out technicalDetails, out webHeaderCollection);
			return httpStatusCode;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00039B9C File Offset: 0x00037D9C
		public static TResult ExecuteJsonBasedHttpGetRequest<TResult>(Uri uri, IDictionary<string, string> headers, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer responseSerializer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			return ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<object, TResult>(uri, "GET", headers, null, options, timeout, null, responseSerializer, false, true, out httpStatusCode, out technicalDetails, out webHeaderCollection);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00039BC4 File Offset: 0x00037DC4
		public static HttpStatusCode ExecuteJsonBasedHttpPostRequest<TRequest>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, object>(uri, "POST", headers, requestBody, options, timeout, requestSeriralizer, null, false, false, out httpStatusCode, out technicalDetails, out webHeaderCollection);
			return httpStatusCode;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00039BF0 File Offset: 0x00037DF0
		public static TResult ExecuteJsonBasedHttpPostRequest<TRequest, TResult>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, DataContractJsonSerializer responseSerializer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			return ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, TResult>(uri, "POST", headers, requestBody, options, timeout, requestSeriralizer, responseSerializer, false, true, out httpStatusCode, out technicalDetails, out webHeaderCollection);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00039C1C File Offset: 0x00037E1C
		public static HttpStatusCode ExecuteJsonBasedHttpPutRequest<TRequest>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, object>(uri, "PUT", headers, requestBody, options, timeout, requestSeriralizer, null, false, false, out httpStatusCode, out technicalDetails, out webHeaderCollection);
			return httpStatusCode;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00039C48 File Offset: 0x00037E48
		public static TResult ExecuteJsonBasedHttpPutRequest<TRequest, TResult>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, DataContractJsonSerializer responseSerializer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			return ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, TResult>(uri, "PUT", headers, requestBody, options, timeout, requestSeriralizer, responseSerializer, false, true, out httpStatusCode, out technicalDetails, out webHeaderCollection);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00039C71 File Offset: 0x00037E71
		public static IEnumerable<KeyValuePair<string, string>> ParseKeyValueSet(string keyValueSet)
		{
			int index = 0;
			int length = keyValueSet.Length;
			char[] buffer = new char[length];
			while (index < length)
			{
				while (index < length && char.IsWhiteSpace(keyValueSet, index))
				{
					int num = index;
					index = num + 1;
				}
				if (index == length)
				{
					break;
				}
				if (keyValueSet[index] == ';')
				{
					int num = index;
					index = num + 1;
				}
				else
				{
					int num2;
					index = ConnectivityHelper.SearchForKeyEnd(keyValueSet, index, buffer, out num2);
					string text = new string(buffer, 0, num2);
					if (index == -1 || keyValueSet[index] == ';')
					{
						yield return new KeyValuePair<string, string>(text, null);
						if (index == -1)
						{
							break;
						}
						int num = index;
						index = num + 1;
					}
					else
					{
						int num = index;
						index = num + 1;
						while (index < length && char.IsWhiteSpace(keyValueSet, index))
						{
							num = index;
							index = num + 1;
						}
						if (index == length)
						{
							yield return new KeyValuePair<string, string>(text, null);
							break;
						}
						index = ConnectivityHelper.SearchForValueEnd(keyValueSet, index, buffer, out num2);
						string text2 = ((num2 > 0) ? new string(buffer, 0, num2) : string.Empty);
						yield return new KeyValuePair<string, string>(text, text2);
						if (index == -1)
						{
							break;
						}
						num = index;
						index = num + 1;
					}
				}
			}
			yield break;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00039C84 File Offset: 0x00037E84
		internal static TResult ExecuteJsonBasedHttpRequestImpl<TRequest, TResult>(Uri uri, string method, IDictionary<string, string> headers, TRequest requestContent, ConnectivityHelper.JsonHttpRequestOptions options, int timeoutInMs, DataContractJsonSerializer requestSerializer, DataContractJsonSerializer responseSerializer, bool getResponseHeaders, bool getResponseContent, out HttpStatusCode status, out string technicalDetails, out WebHeaderCollection responseHeaders)
		{
			int num = 1;
			HttpWebResponse httpWebResponse = null;
			while (httpWebResponse == null)
			{
				object obj;
				WebException ex;
				bool flag;
				WebException ex2;
				HttpWebResponse httpWebResponse2;
				int num2;
				try
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
					httpWebRequest.Method = method;
					httpWebRequest.ContentType = "application/json";
					if (httpWebRequest.Proxy != null && httpWebRequest.Proxy.Credentials == null)
					{
						httpWebRequest.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
					}
					if (!ConnectivityHelper.IsOptionEnabled(options, ConnectivityHelper.JsonHttpRequestOptions.AllowAutoRedirect))
					{
						httpWebRequest.AllowAutoRedirect = false;
					}
					if (timeoutInMs != -1)
					{
						httpWebRequest.Timeout = timeoutInMs;
					}
					httpWebRequest.UserAgent = "AMO";
					if (headers != null)
					{
						foreach (KeyValuePair<string, string> keyValuePair in headers)
						{
							httpWebRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					if (method != "GET" && requestContent != null)
					{
						if (requestSerializer == null)
						{
							requestSerializer = new DataContractJsonSerializer(typeof(TRequest));
						}
						if (ConnectivityHelper.IsOptionEnabled(options, ConnectivityHelper.JsonHttpRequestOptions.SetContentLength))
						{
							using (MemoryStream memoryStream = new MemoryStream())
							{
								requestSerializer.WriteObject(memoryStream, requestContent);
								memoryStream.Seek(0L, SeekOrigin.Begin);
								httpWebRequest.ContentLength = memoryStream.Length;
								using (Stream requestStream = httpWebRequest.GetRequestStream())
								{
									memoryStream.CopyTo(requestStream);
								}
								goto IL_0171;
							}
						}
						using (Stream requestStream2 = httpWebRequest.GetRequestStream())
						{
							requestSerializer.WriteObject(requestStream2, requestContent);
						}
					}
					IL_0171:
					httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					ex = obj as WebException;
					if (ex == null)
					{
						flag = false;
					}
					else
					{
						ex2 = ex;
						if (ConnectivityHelper.IsOptionEnabled(options, ConnectivityHelper.JsonHttpRequestOptions.RetryOnServiceUnavailable))
						{
							httpWebResponse2 = ex2.Response as HttpWebResponse;
							if (httpWebResponse2 != null && httpWebResponse2.StatusCode == HttpStatusCode.ServiceUnavailable)
							{
								num2 = ((num < 3) ? 1 : 0);
								goto IL_01BE;
							}
						}
						num2 = 0;
						IL_01BE:
						flag = num2 != 0;
					}
					endfilter(flag);
				})
				{
					Thread.Sleep(2000 * num);
					num++;
				}
			}
			if (ConnectivityHelper.IsOptionEnabled(options, ConnectivityHelper.JsonHttpRequestOptions.GetTechnicalDetails))
			{
				ConnectivityHelper.JsonHttpRequestOptions jsonHttpRequestOptions = options & ConnectivityHelper.JsonHttpRequestOptions.RequestTarget;
				if (jsonHttpRequestOptions != ConnectivityHelper.JsonHttpRequestOptions.TargetingPbiShared)
				{
					if (jsonHttpRequestOptions != ConnectivityHelper.JsonHttpRequestOptions.TargetingPaasInfra)
					{
						if (jsonHttpRequestOptions != ConnectivityHelper.JsonHttpRequestOptions.TargetingDataverse)
						{
							technicalDetails = null;
						}
						else
						{
							technicalDetails = AsPaasHelper.GetTechnicalDetailsFromDataverseResponse(httpWebResponse, headers);
						}
					}
					else
					{
						technicalDetails = AsPaasHelper.GetTechnicalDetailsFromPaasInfraResponse(httpWebResponse);
					}
				}
				else
				{
					technicalDetails = AsPaasHelper.GetTechnicalDetailsFromPbiSharedResponse(httpWebResponse);
				}
			}
			else
			{
				technicalDetails = null;
			}
			status = httpWebResponse.StatusCode;
			responseHeaders = (getResponseHeaders ? httpWebResponse.Headers : null);
			if (getResponseContent && status >= HttpStatusCode.OK && status < HttpStatusCode.MultipleChoices)
			{
				try
				{
					if (responseSerializer == null)
					{
						responseSerializer = new DataContractJsonSerializer(typeof(TResult));
					}
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						return (TResult)((object)responseSerializer.ReadObject(responseStream));
					}
				}
				catch (ProtocolViolationException)
				{
					return default(TResult);
				}
			}
			httpWebResponse.Close();
			return default(TResult);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0003A008 File Offset: 0x00038208
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsOptionEnabled(ConnectivityHelper.JsonHttpRequestOptions options, ConnectivityHelper.JsonHttpRequestOptions mask)
		{
			return (options & mask) == mask;
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0003A010 File Offset: 0x00038210
		private static int SearchForKeyEnd(string keyValueSet, int index, char[] buffer, out int bufferSize)
		{
			int length = keyValueSet.Length;
			bufferSize = 0;
			bool flag = false;
			while (!flag)
			{
				if (index >= length)
				{
					break;
				}
				char c = keyValueSet[index];
				if (c != ';')
				{
					if (c == '=')
					{
						if (index == length - 1 || keyValueSet[index + 1] != '=')
						{
							flag = true;
						}
						else
						{
							index += 2;
						}
					}
					else
					{
						index++;
					}
				}
				else
				{
					flag = true;
				}
				if (!flag)
				{
					int num = bufferSize;
					bufferSize = num + 1;
					buffer[num] = c;
				}
			}
			while (bufferSize > 0 && char.IsWhiteSpace(buffer[bufferSize - 1]))
			{
				bufferSize--;
			}
			if (bufferSize == 0)
			{
				throw new ArgumentException(RuntimeSR.Exception_InvalidKeyValueSet(keyValueSet), "keyValueSet");
			}
			if (!flag)
			{
				return -1;
			}
			return index;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0003A0B0 File Offset: 0x000382B0
		private static int SearchForValueEnd(string keyValueSet, int index, char[] buffer, out int bufferSize)
		{
			int length = keyValueSet.Length;
			bool flag = keyValueSet[index] == '"';
			bool flag2 = keyValueSet[index] == '\'';
			if (flag || flag2)
			{
				index++;
			}
			bufferSize = 0;
			while (index < length)
			{
				char c = keyValueSet[index];
				if (c != '"')
				{
					if (c != '\'')
					{
						if (c != ';')
						{
							if (char.IsControl(c) && !char.IsWhiteSpace(c))
							{
								throw new ArgumentException(RuntimeSR.Exception_InvalidKeyValueSet(keyValueSet), "keyValueSet");
							}
						}
						else if (!flag && !flag2)
						{
							while (bufferSize > 0 && char.IsWhiteSpace(buffer[bufferSize - 1]))
							{
								bufferSize--;
							}
							return index;
						}
					}
					else if (flag2)
					{
						if (index == length - 1 || keyValueSet[index + 1] != '\'')
						{
							return ConnectivityHelper.EnsureValidQuotedValueEnd(keyValueSet, index + 1);
						}
						index++;
					}
					else if (!flag)
					{
						if (index == length - 1 || keyValueSet[index + 1] != '\'')
						{
							throw new ArgumentException(RuntimeSR.Exception_InvalidKeyValueSet(keyValueSet), "keyValueSet");
						}
						index++;
					}
				}
				else if (flag)
				{
					if (index == length - 1 || keyValueSet[index + 1] != '"')
					{
						return ConnectivityHelper.EnsureValidQuotedValueEnd(keyValueSet, index + 1);
					}
					index++;
				}
				else if (!flag2)
				{
					if (index == length - 1 || keyValueSet[index + 1] != '"')
					{
						throw new ArgumentException(RuntimeSR.Exception_InvalidKeyValueSet(keyValueSet), "keyValueSet");
					}
					index++;
				}
				index++;
				int num = bufferSize;
				bufferSize = num + 1;
				buffer[num] = c;
			}
			if (flag || flag2)
			{
				throw new ArgumentException(RuntimeSR.Exception_InvalidKeyValueSet(keyValueSet), "keyValueSet");
			}
			while (bufferSize > 0 && char.IsWhiteSpace(buffer[bufferSize - 1]))
			{
				bufferSize--;
			}
			return -1;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0003A250 File Offset: 0x00038450
		private static int EnsureValidQuotedValueEnd(string keyValueSet, int index)
		{
			int length = keyValueSet.Length;
			while (index < length && char.IsWhiteSpace(keyValueSet, index))
			{
				index++;
			}
			if (index >= length)
			{
				return -1;
			}
			if (keyValueSet[index] != ';')
			{
				throw new ArgumentException(RuntimeSR.Exception_InvalidKeyValueSet(keyValueSet), "keyValueSet");
			}
			return index;
		}

		// Token: 0x04000ABC RID: 2748
		private const int BackoffTimeInMsForAttempt = 2000;

		// Token: 0x04000ABD RID: 2749
		private const int ServiceUnavailableAttemptCount = 3;

		// Token: 0x04000ABE RID: 2750
		private const string UriSchemeSuffix = "://";

		// Token: 0x04000ABF RID: 2751
		private const char EQUAL_SIGN = '=';

		// Token: 0x04000AC0 RID: 2752
		private const char SEMICOLON = ';';

		// Token: 0x04000AC1 RID: 2753
		private const char SINGLE_QUOTE = '\'';

		// Token: 0x04000AC2 RID: 2754
		private const char DOUBLE_QUOTE = '"';

		// Token: 0x020001D9 RID: 473
		[Flags]
		public enum JsonHttpRequestOptions
		{
			// Token: 0x04001199 RID: 4505
			None = 0,
			// Token: 0x0400119A RID: 4506
			SetContentLength = 1,
			// Token: 0x0400119B RID: 4507
			AllowAutoRedirect = 2,
			// Token: 0x0400119C RID: 4508
			RetryOnServiceUnavailable = 4,
			// Token: 0x0400119D RID: 4509
			GetTechnicalDetails = 16,
			// Token: 0x0400119E RID: 4510
			TargetingPbiShared = 256,
			// Token: 0x0400119F RID: 4511
			TargetingPaasInfra = 512,
			// Token: 0x040011A0 RID: 4512
			TargetingDataverse = 1024,
			// Token: 0x040011A1 RID: 4513
			RequestTarget = 3840,
			// Token: 0x040011A2 RID: 4514
			Default = 3
		}
	}
}
