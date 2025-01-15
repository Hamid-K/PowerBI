using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x02000026 RID: 38
	internal static class ConnectivityHelper
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00005F64 File Offset: 0x00004164
		public static HttpStatusCode ExecuteJsonBasedHttpGetRequest<TResult>(Uri uri, IDictionary<string, string> headers, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<object, object>(uri, "GET", headers, null, options, timeout, null, null, false, false, out httpStatusCode, out technicalDetails, out webHeaderCollection);
			return httpStatusCode;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005F8C File Offset: 0x0000418C
		public static TResult ExecuteJsonBasedHttpGetRequest<TResult>(Uri uri, IDictionary<string, string> headers, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer responseSerializer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			return ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<object, TResult>(uri, "GET", headers, null, options, timeout, null, responseSerializer, false, true, out httpStatusCode, out technicalDetails, out webHeaderCollection);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005FB4 File Offset: 0x000041B4
		public static HttpStatusCode ExecuteJsonBasedHttpPostRequest<TRequest>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, object>(uri, "POST", headers, requestBody, options, timeout, requestSeriralizer, null, false, false, out httpStatusCode, out technicalDetails, out webHeaderCollection);
			return httpStatusCode;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005FE0 File Offset: 0x000041E0
		public static TResult ExecuteJsonBasedHttpPostRequest<TRequest, TResult>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, DataContractJsonSerializer responseSerializer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			return ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, TResult>(uri, "POST", headers, requestBody, options, timeout, requestSeriralizer, responseSerializer, false, true, out httpStatusCode, out technicalDetails, out webHeaderCollection);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000600C File Offset: 0x0000420C
		public static HttpStatusCode ExecuteJsonBasedHttpPutRequest<TRequest>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, object>(uri, "PUT", headers, requestBody, options, timeout, requestSeriralizer, null, false, false, out httpStatusCode, out technicalDetails, out webHeaderCollection);
			return httpStatusCode;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006038 File Offset: 0x00004238
		public static TResult ExecuteJsonBasedHttpPutRequest<TRequest, TResult>(Uri uri, IDictionary<string, string> headers, TRequest requestBody, ConnectivityHelper.JsonHttpRequestOptions options, int timeout, DataContractJsonSerializer requestSeriralizer, DataContractJsonSerializer responseSerializer, out string technicalDetails)
		{
			HttpStatusCode httpStatusCode;
			WebHeaderCollection webHeaderCollection;
			return ConnectivityHelper.ExecuteJsonBasedHttpRequestImpl<TRequest, TResult>(uri, "PUT", headers, requestBody, options, timeout, requestSeriralizer, responseSerializer, false, true, out httpStatusCode, out technicalDetails, out webHeaderCollection);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006061 File Offset: 0x00004261
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

		// Token: 0x06000127 RID: 295 RVA: 0x00006074 File Offset: 0x00004274
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
					httpWebRequest.UserAgent = "AzureClient";
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

		// Token: 0x06000128 RID: 296 RVA: 0x000063E4 File Offset: 0x000045E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsOptionEnabled(ConnectivityHelper.JsonHttpRequestOptions options, ConnectivityHelper.JsonHttpRequestOptions mask)
		{
			return (options & mask) == mask;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000063EC File Offset: 0x000045EC
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

		// Token: 0x0600012A RID: 298 RVA: 0x0000648C File Offset: 0x0000468C
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

		// Token: 0x0600012B RID: 299 RVA: 0x0000662C File Offset: 0x0000482C
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

		// Token: 0x040000C2 RID: 194
		private const int BackoffTimeInMsForAttempt = 2000;

		// Token: 0x040000C3 RID: 195
		private const int ServiceUnavailableAttemptCount = 3;

		// Token: 0x040000C4 RID: 196
		private const char EQUAL_SIGN = '=';

		// Token: 0x040000C5 RID: 197
		private const char SEMICOLON = ';';

		// Token: 0x040000C6 RID: 198
		private const char SINGLE_QUOTE = '\'';

		// Token: 0x040000C7 RID: 199
		private const char DOUBLE_QUOTE = '"';

		// Token: 0x0200006E RID: 110
		[Flags]
		public enum JsonHttpRequestOptions
		{
			// Token: 0x0400020C RID: 524
			None = 0,
			// Token: 0x0400020D RID: 525
			SetContentLength = 1,
			// Token: 0x0400020E RID: 526
			AllowAutoRedirect = 2,
			// Token: 0x0400020F RID: 527
			RetryOnServiceUnavailable = 4,
			// Token: 0x04000210 RID: 528
			GetTechnicalDetails = 16,
			// Token: 0x04000211 RID: 529
			TargetingPbiShared = 256,
			// Token: 0x04000212 RID: 530
			TargetingDataverse = 1024,
			// Token: 0x04000213 RID: 531
			RequestTarget = 3840,
			// Token: 0x04000214 RID: 532
			Default = 3
		}
	}
}
