using System;
using System.IO;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B2A RID: 2858
	internal class GoogleAnalyticsRequestPolicy
	{
		// Token: 0x06004F4A RID: 20298 RVA: 0x00108DF4 File Offset: 0x00106FF4
		public GoogleAnalyticsRequestPolicy(IEngineHost engineHost)
		{
			this.engineHost = engineHost;
			this.RetryPolicy = new RetryPolicy(GoogleAnalyticsRequestPolicy.AttemptLimit, new Func<Exception, RetryHandlerResult>(this.RetryHandler));
			this.attempts = 0;
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x06004F4B RID: 20299 RVA: 0x00108E26 File Offset: 0x00107026
		public RetryPolicy RetryPolicy { get; }

		// Token: 0x06004F4C RID: 20300 RVA: 0x00108E30 File Offset: 0x00107030
		private RetryHandlerResult RetryHandler(Exception e)
		{
			this.attempts++;
			WebException ex = e as WebException;
			if (ex != null)
			{
				if (ex.Status == WebExceptionStatus.Timeout || ex.Status == WebExceptionStatus.ConnectionClosed)
				{
					return RetryHandlerResult.RetryAfterDefaultDelay;
				}
				MashupHttpWebResponse mashupHttpWebResponse = ex.Response as MashupHttpWebResponse;
				if (mashupHttpWebResponse != null)
				{
					HttpStatusCode statusCode = mashupHttpWebResponse.StatusCode;
					if (statusCode == HttpStatusCode.Forbidden)
					{
						using (StreamReader streamReader = new StreamReader(mashupHttpWebResponse.GetDecompressedResponseStream()))
						{
							Value value = null;
							Value value2;
							if (!JsonParser.TryParse(streamReader, out value) || !value.TryGetValue(GoogleAnalyticsRequestPolicy.Error, out value2) || !value2.TryGetValue(GoogleAnalyticsRequestPolicy.Errors, out value2) || !value2.TryGetValue(NumberValue.Zero, out value2) || !value2.TryGetValue(GoogleAnalyticsRequestPolicy.Reason, out value2) || !(value2.AsString == GoogleAnalyticsRequestPolicy.UserRateLimitExceeded))
							{
								string text = null;
								if (value != null)
								{
									GoogleAnalyticsRequestPolicy.TryGetErrorMessage(value, out text);
								}
								return RetryHandlerResult.FailWithException(DataSourceException.NewAccessForbiddenError(this.engineHost, GoogleAnalyticsServiceV1.resource, text, text, null));
							}
							if (this.attempts < GoogleAnalyticsRequestPolicy.AttemptLimit)
							{
								return RetryHandlerResult.RetryAfterDefaultDelay;
							}
							string text2 = null;
							GoogleAnalyticsRequestPolicy.TryGetErrorMessage(value, out text2);
							return RetryHandlerResult.FailWithException(DataSourceException.NewDataSourceError(this.engineHost, text2, GoogleAnalyticsServiceV1.resource, GoogleAnalyticsRequestPolicy.Error, value, TypeValue.Any, null));
						}
					}
					if (statusCode == HttpStatusCode.RequestTimeout || statusCode == HttpStatusCode.ServiceUnavailable || statusCode == HttpStatusCode.GatewayTimeout || statusCode == (HttpStatusCode)429 || statusCode == (HttpStatusCode)509)
					{
						TimeSpan? retryAfter = RetryPolicy.GetRetryAfter(mashupHttpWebResponse);
						if (retryAfter == null)
						{
							return RetryHandlerResult.RetryAfterDefaultDelay;
						}
						return RetryHandlerResult.RetryAfterDelay(retryAfter.Value);
					}
				}
			}
			return RetryHandlerResult.FailWithOriginalException;
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x00108FF8 File Offset: 0x001071F8
		public bool TryGetSecurityException(Request request, WebException exception, out ResourceSecurityException resourceSecurityException)
		{
			if (exception.Status == WebExceptionStatus.ProtocolError)
			{
				MashupHttpWebResponse mashupHttpWebResponse = exception.Response as MashupHttpWebResponse;
				if (mashupHttpWebResponse != null)
				{
					return this.TryGetResourceSecurityException(mashupHttpWebResponse, GoogleAnalyticsServiceV1.resource, out resourceSecurityException);
				}
			}
			resourceSecurityException = null;
			return false;
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x00109030 File Offset: 0x00107230
		private static bool TryGetErrorMessage(Value errorResponse, out string message)
		{
			Value value;
			Value value2;
			if (errorResponse.TryGetValue(GoogleAnalyticsRequestPolicy.Error, out value) && value.TryGetValue(GoogleAnalyticsRequestPolicy.Message, out value2))
			{
				Value value3;
				if (value.TryGetValue(GoogleAnalyticsRequestPolicy.Code, out value3) && value3.IsNumber)
				{
					message = Strings.RequestFailedWithStatusCode("GoogleAnalytics", value3.AsNumber, value2.AsString);
				}
				else
				{
					message = Strings.RequestFailedWithoutStatusCode("GoogleAnalytics", value2.AsString);
				}
				return true;
			}
			message = null;
			return false;
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x001090B0 File Offset: 0x001072B0
		private bool TryGetResourceSecurityException(MashupHttpWebResponse response, IResource resource, out ResourceSecurityException exception)
		{
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				string text = null;
				using (StreamReader streamReader = new StreamReader(response.GetDecompressedResponseStream()))
				{
					Value value;
					if (JsonParser.TryParse(streamReader, out value))
					{
						GoogleAnalyticsRequestPolicy.TryGetErrorMessage(value, out text);
					}
					else
					{
						text = null;
					}
					exception = DataSourceException.NewAccessAuthorizationError(this.engineHost, resource, text, text, null);
					return true;
				}
			}
			exception = null;
			return false;
		}

		// Token: 0x04002A91 RID: 10897
		private static readonly string Code = "code";

		// Token: 0x04002A92 RID: 10898
		private static readonly string Error = "error";

		// Token: 0x04002A93 RID: 10899
		private static readonly string Errors = "errors";

		// Token: 0x04002A94 RID: 10900
		private static readonly string Reason = "reason";

		// Token: 0x04002A95 RID: 10901
		private static readonly string Message = "message";

		// Token: 0x04002A96 RID: 10902
		private static readonly string UserRateLimitExceeded = "userRateLimitExceeded";

		// Token: 0x04002A97 RID: 10903
		private static readonly int AttemptLimit = 3;

		// Token: 0x04002A98 RID: 10904
		private readonly IEngineHost engineHost;

		// Token: 0x04002A99 RID: 10905
		private int attempts;
	}
}
