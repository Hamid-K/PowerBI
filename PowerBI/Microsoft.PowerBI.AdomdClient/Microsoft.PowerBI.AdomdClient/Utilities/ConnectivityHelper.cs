using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Threading;
using Microsoft.Win32;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000142 RID: 322
	internal static class ConnectivityHelper
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x00036BF8 File Offset: 0x00034DF8
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

		// Token: 0x06001001 RID: 4097 RVA: 0x00036C60 File Offset: 0x00034E60
		public static bool HasUriProtocolScheme(string url, string scheme)
		{
			return !string.IsNullOrEmpty(url) && (url.StartsWith(scheme, StringComparison.InvariantCultureIgnoreCase) && url.Length > scheme.Length + "://".Length) && string.Compare(url, scheme.Length, "://", 0, "://".Length, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00036CBC File Offset: 0x00034EBC
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

		// Token: 0x06001003 RID: 4099 RVA: 0x00036D46 File Offset: 0x00034F46
		public static bool IsHttpUri(string url)
		{
			return ConnectivityHelper.HasUriProtocolScheme(url, Uri.UriSchemeHttp);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00036D53 File Offset: 0x00034F53
		public static bool IsHttpsUri(string url)
		{
			return ConnectivityHelper.HasUriProtocolScheme(url, Uri.UriSchemeHttps);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00036D60 File Offset: 0x00034F60
		public static bool IsHttpConnection(string url)
		{
			return !string.IsNullOrEmpty(url) && (ConnectivityHelper.HasUriProtocolScheme(url, Uri.UriSchemeHttp) || ConnectivityHelper.HasUriProtocolScheme(url, Uri.UriSchemeHttps) || ConnectivityHelper.HasUriProtocolScheme(url, "asazure") || ConnectivityHelper.HasUriProtocolScheme(url, "pbidedicated") || ConnectivityHelper.HasUriProtocolScheme(url, "powerbi") || ConnectivityHelper.HasUriProtocolScheme(url, "pbiazure") || ConnectivityHelper.HasUriProtocolScheme(url, "link"));
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00036DD2 File Offset: 0x00034FD2
		public static bool IsHttpsConnection(string url)
		{
			return !string.IsNullOrEmpty(url) && (ConnectivityHelper.HasUriProtocolScheme(url, Uri.UriSchemeHttps) || ConnectivityHelper.HasUriProtocolScheme(url, "powerbi"));
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00036DF8 File Offset: 0x00034FF8
		public static bool IsPaaSInfrastructureConnection(string url)
		{
			return !string.IsNullOrEmpty(url) && (ConnectivityHelper.HasUriProtocolScheme(url, "asazure") || ConnectivityHelper.HasUriProtocolScheme(url, "pbidedicated") || ConnectivityHelper.HasUriProtocolScheme(url, "powerbi") || ConnectivityHelper.HasUriProtocolScheme(url, "pbiazure"));
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00036E38 File Offset: 0x00035038
		public static bool IsPbiPremiumConnection(string url)
		{
			return !string.IsNullOrEmpty(url) && (ConnectivityHelper.HasUriProtocolScheme(url, "pbidedicated") || ConnectivityHelper.HasUriProtocolScheme(url, "powerbi"));
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00036E60 File Offset: 0x00035060
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

		// Token: 0x0600100A RID: 4106 RVA: 0x00036F40 File Offset: 0x00035140
		public static HttpStatusCode ExecuteJsonBasedHttpGetRequest<TResult>(Uri uri, IDictionary<string, string> headers, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<object, object>(uri, "GET", headers, null, options, timeout, null, null, false, false, out httpStatusCode, out technicalDetails, out webHeaderCollection);
			return httpStatusCode;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00036F68 File Offset: 0x00035168
		public static TResult ExecuteJsonBasedHttpGetRequest<TResult>(Uri uri, IDictionary<string, string> headers, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer responseSerializer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			return ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<object, TResult>(uri, "GET", headers, null, options, timeout, null, responseSerializer, false, true, out httpStatusCode, out technicalDetails, out webHeaderCollection);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00036F90 File Offset: 0x00035190
		public static HttpStatusCode ExecuteJsonBasedHttpPostRequest<TRequest>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, object>(uri, "POST", headers, requestBody, options, timeout, requestSeriralizer, null, false, false, out httpStatusCode, out technicalDetails, out webHeaderCollection);
			return httpStatusCode;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00036FBC File Offset: 0x000351BC
		public static TResult ExecuteJsonBasedHttpPostRequest<TRequest, TResult>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, DataContractJsonSerializer responseSerializer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			return ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, TResult>(uri, "POST", headers, requestBody, options, timeout, requestSeriralizer, responseSerializer, false, true, out httpStatusCode, out technicalDetails, out webHeaderCollection);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00036FE8 File Offset: 0x000351E8
		public static HttpStatusCode ExecuteJsonBasedHttpPutRequest<TRequest>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, object>(uri, "PUT", headers, requestBody, options, timeout, requestSeriralizer, null, false, false, out httpStatusCode, out technicalDetails, out webHeaderCollection);
			return httpStatusCode;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00037014 File Offset: 0x00035214
		public static TResult ExecuteJsonBasedHttpPutRequest<TRequest, TResult>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, DataContractJsonSerializer responseSerializer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			return ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, TResult>(uri, "PUT", headers, requestBody, options, timeout, requestSeriralizer, responseSerializer, false, true, out httpStatusCode, out technicalDetails, out webHeaderCollection);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0003703D File Offset: 0x0003523D
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

		// Token: 0x06001011 RID: 4113 RVA: 0x00037050 File Offset: 0x00035250
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
					httpWebRequest.UserAgent = "ADOMD.NET";
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

		// Token: 0x06001012 RID: 4114 RVA: 0x000373D4 File Offset: 0x000355D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsOptionEnabled(ConnectivityHelper.JsonHttpRequestOptions options, ConnectivityHelper.JsonHttpRequestOptions mask)
		{
			return (options & mask) == mask;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000373DC File Offset: 0x000355DC
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

		// Token: 0x06001014 RID: 4116 RVA: 0x0003747C File Offset: 0x0003567C
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

		// Token: 0x06001015 RID: 4117 RVA: 0x0003761C File Offset: 0x0003581C
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

		// Token: 0x04000AF6 RID: 2806
		private const int BackoffTimeInMsForAttempt = 2000;

		// Token: 0x04000AF7 RID: 2807
		private const int ServiceUnavailableAttemptCount = 3;

		// Token: 0x04000AF8 RID: 2808
		private const string UriSchemeSuffix = "://";

		// Token: 0x04000AF9 RID: 2809
		private const char EQUAL_SIGN = '=';

		// Token: 0x04000AFA RID: 2810
		private const char SEMICOLON = ';';

		// Token: 0x04000AFB RID: 2811
		private const char SINGLE_QUOTE = '\'';

		// Token: 0x04000AFC RID: 2812
		private const char DOUBLE_QUOTE = '"';

		// Token: 0x020001FC RID: 508
		[Flags]
		public enum JsonHttpRequestOptions
		{
			// Token: 0x04000ECD RID: 3789
			None = 0,
			// Token: 0x04000ECE RID: 3790
			SetContentLength = 1,
			// Token: 0x04000ECF RID: 3791
			AllowAutoRedirect = 2,
			// Token: 0x04000ED0 RID: 3792
			RetryOnServiceUnavailable = 4,
			// Token: 0x04000ED1 RID: 3793
			GetTechnicalDetails = 16,
			// Token: 0x04000ED2 RID: 3794
			TargetingPbiShared = 256,
			// Token: 0x04000ED3 RID: 3795
			TargetingPaasInfra = 512,
			// Token: 0x04000ED4 RID: 3796
			TargetingDataverse = 1024,
			// Token: 0x04000ED5 RID: 3797
			RequestTarget = 3840,
			// Token: 0x04000ED6 RID: 3798
			Default = 3
		}
	}
}
