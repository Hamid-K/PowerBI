using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureBase
{
	// Token: 0x02000F00 RID: 3840
	internal static class AzureBaseHelper
	{
		// Token: 0x060065BF RID: 26047 RVA: 0x0015E36C File Offset: 0x0015C56C
		public static Request CreateRequest(IEngineHost engineHost, IResource resource, TextValue url, TextValue version, Value query = null, Value headers = null, Value content = null, bool isOneLake = false)
		{
			string text = DateTime.UtcNow.ToString("R", CultureInfo.InvariantCulture);
			IEvaluationConstants evaluationConstants = engineHost.QueryService<IEvaluationConstants>();
			string text2 = ((evaluationConstants == null) ? null : evaluationConstants.ActivityId.ToString());
			RecordValue recordValue = RecordValue.New(AzureBaseHelper.commonBlobKeys, new Value[]
			{
				TextValue.New(text),
				version,
				TextValue.NewOrNull(text2)
			});
			if (headers == null || headers.IsNull)
			{
				headers = recordValue;
			}
			else
			{
				headers = headers.AsRecord.Concatenate(recordValue);
			}
			Uri uri = UriHelper.CreateUriFromValue(url);
			TextValue textValue = url;
			Value value = Value.Null;
			if (uri.Query.Contains("sig="))
			{
				textValue = TextValue.New(uri.GetLeftPart(UriPartial.Path));
				value = UriHelper.CreateQueryRecord(uri.Query, false);
			}
			Value value2 = ListValue.New(new Value[]
			{
				TextValue.New("x-ms-client-request-id"),
				TextValue.New("x-ms-date")
			});
			IList<string> list = new string[] { "x-ms-request-id" };
			IList<string> list2;
			if (isOneLake)
			{
				headers = headers.AsRecord.Concatenate(AzureBaseHelper.GenerateTracingHeaders(engineHost));
				value2 = value2.Concatenate(ListValue.New(new Value[]
				{
					TextValue.New("x-ms-root-activity-id"),
					TextValue.New("x-ms-parent-activity-id")
				}));
				list2 = new string[] { "x-ms-parent-activity-id" };
			}
			else
			{
				list2 = null;
			}
			Request request = Request.Create(engineHost, resource.Kind, resource.NonNormalizedPath, textValue, query ?? Value.Null, content ?? Value.Null, null, headers, null, null, null, value, null, AzureBaseHelper.retryPolicy);
			request.ExcludedHeaders = value2;
			request.SafeResponseHeaders = list;
			request.SafeRequestHeaders = list2;
			request.TraceData = RecordValue.New(Keys.New("StorageVersion"), new Value[] { version.NewMeta(ValueException.NonPii) });
			return request;
		}

		// Token: 0x060065C0 RID: 26048 RVA: 0x0015E554 File Offset: 0x0015C754
		public static Response GetResponse(Request request, Request.SecurityExceptionCreator securityExceptionCreator, int[] ignoredStatusCodes = null)
		{
			ResourceCredentialCollection resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(request.Host, request.RequestResource, null);
			Response response2;
			try
			{
				Response response = request.GetResponse(resourceCredentialCollection, securityExceptionCreator, false);
				if (ignoredStatusCodes != null && ignoredStatusCodes.Contains(response.StatusCode))
				{
					response2 = response;
				}
				else
				{
					if (request.IsFailedStatusCode(response))
					{
						Message4 message = Strings.HDInsightFailed(request.ResourceKind, request.InitialUri, response.StatusCode, response.StatusDescription);
						throw HttpServices.NewDataSourceError<Message4>(request.Host, message, request.RequestResource, request.InitialUri);
					}
					response2 = response;
				}
			}
			catch (FormatException)
			{
				throw DataSourceException.NewAccessAuthorizationError(request.Host, request.RequestResource, Strings.HttpCredentialsBasicAuthRequiresHttps, null, null);
			}
			catch (ResponseException ex)
			{
				Message2 message2 = Strings.WebRequestFailed(request.ResourceKind, ex.InnerException.Message);
				throw HttpServices.NewDataSourceError<Message2>(request.Host, message2, request.RequestResource, request.InitialUri);
			}
			return response2;
		}

		// Token: 0x060065C1 RID: 26049 RVA: 0x0015E650 File Offset: 0x0015C850
		public static void ExecuteRequest(Request request, Request.SecurityExceptionCreator securityExceptionCreator, int[] ignoredStatusCodes = null)
		{
			using (AzureBaseHelper.GetResponse(request, securityExceptionCreator, ignoredStatusCodes))
			{
			}
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x0015E684 File Offset: 0x0015C884
		public static string EscapeBlobName(string blobName)
		{
			string[] array = blobName.Split(new char[] { '/' });
			StringBuilder stringBuilder = new StringBuilder();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (i == num - 1)
				{
					stringBuilder.Append(Uri.EscapeDataString(array[i]));
				}
				else
				{
					stringBuilder.Append(Uri.EscapeDataString(array[i]));
					stringBuilder.Append("/");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x0015E6F4 File Offset: 0x0015C8F4
		public static RecordValue GenerateTracingHeaders(IEngineHost host)
		{
			IEvaluationConstants evaluationConstants = host.QueryService<IEvaluationConstants>();
			return RecordValue.New(Keys.New("x-ms-parent-activity-id", "x-ms-root-activity-id"), new Value[]
			{
				TextValue.New(evaluationConstants.ActivityId.ToString()),
				TextValue.New(Guid.NewGuid().ToString())
			});
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x0015E75C File Offset: 0x0015C95C
		public static Value ConvertToDateTime(string dateModified)
		{
			DateTime dateTime;
			if (DateTime.TryParseExact(dateModified, "r", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dateTime))
			{
				return DateTimeValue.New(dateTime);
			}
			throw ValueException.NewDataFormatError<Message0>(Strings.DateTime_CannotParseTextAsDateTimeError, TextValue.New(dateModified), null);
		}

		// Token: 0x060065C5 RID: 26053 RVA: 0x0015E796 File Offset: 0x0015C996
		public static bool IsValidPrefix(string prefixValue, string containerUrl)
		{
			return prefixValue.StartsWith(containerUrl, StringComparison.Ordinal) && prefixValue != containerUrl && prefixValue + "/" != containerUrl;
		}

		// Token: 0x060065C6 RID: 26054 RVA: 0x0015E7BE File Offset: 0x0015C9BE
		public static string FormatContainerString(string containerUrlString)
		{
			if (containerUrlString[containerUrlString.Length - 1] != '/')
			{
				return containerUrlString += "/";
			}
			return containerUrlString;
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x0015E7E4 File Offset: 0x0015C9E4
		public static string GetUrlWithSelectEscaping(string endpointUri, string inputUri)
		{
			int num = 0;
			for (int i = 0; i < endpointUri.Length; i++)
			{
				if (endpointUri[i] == '/')
				{
					num++;
				}
			}
			int num2 = -1;
			for (int j = 0; j < inputUri.Length; j++)
			{
				if (inputUri[j] == '/')
				{
					num--;
					if (num == 0)
					{
						num2 = j;
						break;
					}
				}
			}
			if (num != 0)
			{
				return endpointUri.Substring(0, endpointUri.Length - 1);
			}
			string text = inputUri.Substring(num2 + 1);
			return endpointUri + AzureBaseHelper.EscapeBlobName(text);
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x0015E86C File Offset: 0x0015CA6C
		private static RetryHandlerResult IsTransient(Exception ex)
		{
			while (ex != null)
			{
				WebException ex2 = ex as WebException;
				if (ex2 != null)
				{
					if (ex2.Status == WebExceptionStatus.Timeout)
					{
						return RetryHandlerResult.RetryAfterDefaultDelay;
					}
					MashupHttpWebResponse mashupHttpWebResponse = ex2.Response as MashupHttpWebResponse;
					if (mashupHttpWebResponse != null)
					{
						HttpStatusCode statusCode = mashupHttpWebResponse.StatusCode;
						if (statusCode == HttpStatusCode.RequestTimeout || statusCode == HttpStatusCode.InternalServerError || statusCode == HttpStatusCode.BadGateway || statusCode == HttpStatusCode.ServiceUnavailable || statusCode == HttpStatusCode.GatewayTimeout)
						{
							return RetryHandlerResult.RetryAfterDefaultDelay;
						}
					}
				}
				else if (ex is IOException || ex is SocketException)
				{
					return RetryHandlerResult.RetryAfterDefaultDelay;
				}
				ex = ex.InnerException;
			}
			return RetryHandlerResult.FailWithOriginalException;
		}

		// Token: 0x040037DA RID: 14298
		public const int MaxTryAttempt = 3;

		// Token: 0x040037DB RID: 14299
		public const int BlobMaxResult = 5000;

		// Token: 0x040037DC RID: 14300
		public const string CapacityIdKey = "CapacityId";

		// Token: 0x040037DD RID: 14301
		public const string ContentTypeKey = "Content-Type";

		// Token: 0x040037DE RID: 14302
		public const string ETagKey = "etag";

		// Token: 0x040037DF RID: 14303
		public const string FolderPathKey = "FolderPath";

		// Token: 0x040037E0 RID: 14304
		public const string LastModifiedKey = "Last-Modified";

		// Token: 0x040037E1 RID: 14305
		public const string MarkerKey = "marker";

		// Token: 0x040037E2 RID: 14306
		public const string MaxResultsKey = "maxresults";

		// Token: 0x040037E3 RID: 14307
		public const string NameKey = "Name";

		// Token: 0x040037E4 RID: 14308
		public const string PathSeparatorStringKey = "/";

		// Token: 0x040037E5 RID: 14309
		public const string PrefixKey = "prefix";

		// Token: 0x040037E6 RID: 14310
		public const string PrivateLinkServiceAccessTokenKey = "ServiceAccessToken";

		// Token: 0x040037E7 RID: 14311
		public const string SizeKey = "Content-Length";

		// Token: 0x040037E8 RID: 14312
		public const string UrlKey = "Url";

		// Token: 0x040037E9 RID: 14313
		private const string ErrorKey = "Error";

		// Token: 0x040037EA RID: 14314
		public static readonly Keys AttributeKeys = Keys.New("Content Type", "Kind", "Size");

		// Token: 0x040037EB RID: 14315
		public static readonly Keys ContentsAttributeKeys = Keys.New("Kind", "Size");

		// Token: 0x040037EC RID: 14316
		public static readonly Keys RecordKeys = Keys.New(new string[] { "Name", "Url", "Last-Modified", "Content-Length", "Content-Type", "FolderPath" });

		// Token: 0x040037ED RID: 14317
		public static readonly Keys ListEntryRecordKeys = Keys.New(new string[] { "Name", "Url", "Last-Modified", "Content-Length", "FolderPath" });

		// Token: 0x040037EE RID: 14318
		private static readonly RetryPolicy retryPolicy = new RetryPolicy(3, new Func<Exception, RetryHandlerResult>(AzureBaseHelper.IsTransient));

		// Token: 0x040037EF RID: 14319
		private static readonly Keys commonBlobKeys = Keys.New("x-ms-date", "x-ms-version", "x-ms-client-request-id");
	}
}
