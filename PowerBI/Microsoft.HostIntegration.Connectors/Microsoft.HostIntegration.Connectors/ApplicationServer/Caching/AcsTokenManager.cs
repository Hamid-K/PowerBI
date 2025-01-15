using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001A5 RID: 421
	internal sealed class AcsTokenManager : IDisposable
	{
		// Token: 0x06000DC8 RID: 3528 RVA: 0x0002ED00 File Offset: 0x0002CF00
		public AcsTokenManager(Uri acsUri, string issuerName, string issuerKey, string appliesTo)
		{
			this._acsUri = acsUri;
			this._requestData.Add("wrap_name", issuerName);
			this._requestData.Add("wrap_password", issuerKey);
			this._requestData.Add("wrap_scope", appliesTo);
			this._logSource += appliesTo;
			this._webClient.UploadValuesCompleted += this.WebClient_UploadValuesCompleted;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0002EDA8 File Offset: 0x0002CFA8
		public static AcsTokenManager Parse(string authConfigEncoded)
		{
			try
			{
				string @string = AcsTokenManager._defaultTokenTextEncoding.GetString(Convert.FromBase64String(authConfigEncoded));
				if (!string.IsNullOrEmpty(@string) && @string.StartsWith("acs:", StringComparison.OrdinalIgnoreCase))
				{
					string text = @string.Substring("acs:".Length);
					string[] array = text.Split(AcsTokenManager._acsConfigSeparator);
					if (array.Length >= 4)
					{
						Uri uri = new Uri(array[0]);
						string text2 = array[1];
						string text3 = array[2];
						string text4 = array[3];
						return new AcsTokenManager(uri, text2, text3, text4);
					}
				}
			}
			catch (FormatException ex)
			{
				throw new DataCacheException("ParseAuthorizationInfo", 29, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 29), ex);
			}
			return null;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0002EE5C File Offset: 0x0002D05C
		public string GetOrRefreshToken(TimeSpan timeout)
		{
			if (timeout > AcsTokenManager._maxExpectedCacheRequestTimeout)
			{
				timeout = AcsTokenManager._maxExpectedCacheRequestTimeout;
			}
			AcsTokenManager.TokenRefreshState tokenRefreshState;
			for (;;)
			{
				DateTime utcNow = DateTime.UtcNow;
				tokenRefreshState = this._tokenState;
				bool flag = tokenRefreshState.IsExpired(utcNow.Add(timeout));
				bool flag2 = flag || tokenRefreshState.IsDueForRenewal(utcNow);
				if (flag2)
				{
					if (tokenRefreshState.WaitHandle == null)
					{
						AcsTokenManager.TokenRefreshState tokenRefreshState2 = new AcsTokenManager.TokenRefreshState(tokenRefreshState.Token, new LightWeightEventMonitorBased(), null);
						AcsTokenManager.TokenRefreshState tokenRefreshState3 = Interlocked.CompareExchange<AcsTokenManager.TokenRefreshState>(ref this._tokenState, tokenRefreshState2, tokenRefreshState);
						if (object.ReferenceEquals(tokenRefreshState3, tokenRefreshState))
						{
							bool flag3 = false;
							SynchronizationContext synchronizationContext = SynchronizationContext.Current;
							try
							{
								SynchronizationContext.SetSynchronizationContext(null);
								this._webClient.UploadValuesAsync(this._acsUri, "POST", this._requestData);
								tokenRefreshState = tokenRefreshState2;
								flag3 = true;
							}
							catch (WebException ex)
							{
								flag3 = true;
								this._tokenState = new AcsTokenManager.TokenRefreshState(tokenRefreshState.Token, null, ex);
								if (!flag)
								{
									AcsTokenManager.ThrowLastAcsException(ex);
								}
							}
							finally
							{
								SynchronizationContext.SetSynchronizationContext(synchronizationContext);
								if (!flag3)
								{
									Interlocked.CompareExchange<AcsTokenManager.TokenRefreshState>(ref this._tokenState, tokenRefreshState3, tokenRefreshState2);
								}
							}
							if (Provider.IsEnabled(TraceLevel.Verbose))
							{
								EventLogWriter.WriteVerbose<bool>(this._logSource, "ACS token refresh request initiated. Old token expired: {0}", flag);
							}
						}
						else
						{
							tokenRefreshState = tokenRefreshState3;
							if (tokenRefreshState.WaitHandle == null)
							{
								continue;
							}
						}
					}
					if (flag)
					{
						if (tokenRefreshState.WaitHandle.WaitOne(timeout))
						{
							tokenRefreshState = this._tokenState;
							if (tokenRefreshState.LastException != null)
							{
								AcsTokenManager.ThrowLastAcsException(tokenRefreshState.LastException);
							}
						}
						else
						{
							AcsTokenManager.ThrowLastAcsException(null);
						}
					}
				}
				if (tokenRefreshState.Token != null)
				{
					break;
				}
				AcsTokenManager.ThrowLastAcsException(tokenRefreshState.LastException);
			}
			return tokenRefreshState.Token.TokenStr;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0002EFFC File Offset: 0x0002D1FC
		public void ForceAuthRetry(string existingToken)
		{
			if (Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning(this._logSource, "ForceAuthRetry for existing token", new object[0]);
			}
			AcsTokenManager.TokenRefreshState tokenRefreshState = this._tokenState;
			while (tokenRefreshState.Token != null && tokenRefreshState.Token.TokenStr == existingToken)
			{
				AcsTokenManager.TokenRefreshState tokenRefreshState2 = new AcsTokenManager.TokenRefreshState(null, tokenRefreshState.WaitHandle, tokenRefreshState.LastException);
				if (object.ReferenceEquals(Interlocked.CompareExchange<AcsTokenManager.TokenRefreshState>(ref this._tokenState, tokenRefreshState2, tokenRefreshState), tokenRefreshState))
				{
					return;
				}
				tokenRefreshState = this._tokenState;
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0002F07C File Offset: 0x0002D27C
		private void WebClient_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
		{
			AcsTokenManager.TokenRefreshState tokenState = this._tokenState;
			if (tokenState.WaitHandle == null)
			{
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning(this._logSource, "No ACS token request in progress, at WebClient_UploadValuesCompleted", new object[0]);
				}
				return;
			}
			bool flag = false;
			try
			{
				AcsTokenManager.TokenRefreshState tokenRefreshState;
				if (e.Error == null)
				{
					string @string = AcsTokenManager._defaultAcsTextEncoding.GetString(e.Result);
					NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(@string, AcsTokenManager._defaultAcsTextEncoding);
					string text = nameValueCollection["wrap_access_token"];
					ulong maxValue;
					if (!ulong.TryParse(nameValueCollection["wrap_access_token_expires_in"], out maxValue))
					{
						maxValue = ulong.MaxValue;
					}
					tokenRefreshState = new AcsTokenManager.TokenRefreshState(new Token(text, maxValue), null, null);
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<ulong>(this._logSource, "ACS token refresh request completed successfully. Token valid for {0}", maxValue);
					}
				}
				else
				{
					if (Provider.IsEnabled(TraceLevel.Warning))
					{
						EventLogWriter.WriteWarning(this._logSource, "ACS token refresh request failed with exception: {0}", new object[] { e.Error });
					}
					tokenRefreshState = new AcsTokenManager.TokenRefreshState(tokenState.Token, null, e.Error);
				}
				this._tokenState = tokenRefreshState;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (Provider.IsEnabled(TraceLevel.Error))
					{
						EventLogWriter.WriteError(this._logSource, "ACS token refresh callback - unknown error. Reverting to old token.", new object[0]);
					}
					AcsTokenManager.TokenRefreshState tokenRefreshState2 = new AcsTokenManager.TokenRefreshState(tokenState.Token, null, null);
					Interlocked.CompareExchange<AcsTokenManager.TokenRefreshState>(ref this._tokenState, tokenRefreshState2, tokenState);
				}
				tokenState.WaitHandle.Set();
			}
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0002F1DC File Offset: 0x0002D3DC
		private static void ThrowLastAcsException(Exception exception)
		{
			string text = string.Empty;
			WebException ex = exception as WebException;
			if (ex != null && ex.Response != null)
			{
				using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
				{
					text = streamReader.ReadToEnd();
				}
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				if (httpWebResponse != null)
				{
					switch (httpWebResponse.StatusCode)
					{
					case HttpStatusCode.BadRequest:
					case HttpStatusCode.Unauthorized:
					case HttpStatusCode.NotFound:
						throw AcsTokenManager.GetAcsError(29, text, exception);
					}
				}
			}
			throw AcsTokenManager.GetAcsError(30, text, exception);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0002F284 File Offset: 0x0002D484
		private static Exception GetAcsError(int errorCode, string acsMessage, Exception innerException)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				Utility.GetErrorMessage(errorCode, -1),
				acsMessage
			});
			return new DataCacheException("AcsTokenManager", errorCode, text, innerException);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0002F2C4 File Offset: 0x0002D4C4
		public void Dispose()
		{
			this._webClient.Dispose();
		}

		// Token: 0x0400097E RID: 2430
		private const int _acsMinTokenCount = 4;

		// Token: 0x0400097F RID: 2431
		private const string _acsConfigPrefix = "acs:";

		// Token: 0x04000980 RID: 2432
		private const string _wrapAccessTokenProperty = "wrap_access_token";

		// Token: 0x04000981 RID: 2433
		private const string _wrapAccessTokenExpiresInProperty = "wrap_access_token_expires_in";

		// Token: 0x04000982 RID: 2434
		private static readonly char[] _acsConfigSeparator = new char[] { '&' };

		// Token: 0x04000983 RID: 2435
		private static readonly Encoding _defaultTokenTextEncoding = Encoding.ASCII;

		// Token: 0x04000984 RID: 2436
		private static readonly Encoding _defaultAcsTextEncoding = new UTF8Encoding(false, false);

		// Token: 0x04000985 RID: 2437
		private static readonly TimeSpan _maxExpectedCacheRequestTimeout = TimeSpan.FromMinutes(10.0);

		// Token: 0x04000986 RID: 2438
		private readonly string _logSource = "AcsTokenManager.";

		// Token: 0x04000987 RID: 2439
		private readonly Uri _acsUri;

		// Token: 0x04000988 RID: 2440
		private readonly NameValueCollection _requestData = new NameValueCollection();

		// Token: 0x04000989 RID: 2441
		private readonly WebClient _webClient = new WebClient();

		// Token: 0x0400098A RID: 2442
		private AcsTokenManager.TokenRefreshState _tokenState = new AcsTokenManager.TokenRefreshState(null, null, null);

		// Token: 0x020001A6 RID: 422
		private sealed class TokenRefreshState
		{
			// Token: 0x06000DD1 RID: 3537 RVA: 0x0002F31C File Offset: 0x0002D51C
			public TokenRefreshState(Token token, LightWeightEventMonitorBased handle, Exception lastError)
			{
				this.Token = token;
				this.WaitHandle = handle;
				this.LastException = lastError;
			}

			// Token: 0x17000321 RID: 801
			// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0002F339 File Offset: 0x0002D539
			// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0002F341 File Offset: 0x0002D541
			public Token Token { get; private set; }

			// Token: 0x17000322 RID: 802
			// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0002F34A File Offset: 0x0002D54A
			// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0002F352 File Offset: 0x0002D552
			public Exception LastException { get; private set; }

			// Token: 0x17000323 RID: 803
			// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0002F35B File Offset: 0x0002D55B
			// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0002F363 File Offset: 0x0002D563
			public LightWeightEventMonitorBased WaitHandle { get; private set; }

			// Token: 0x06000DD8 RID: 3544 RVA: 0x0002F36C File Offset: 0x0002D56C
			public bool IsDueForRenewal(DateTime now)
			{
				return this.Token == null || this.LastException != null || this.Token.IsDueForRenewal(now);
			}

			// Token: 0x06000DD9 RID: 3545 RVA: 0x0002F38C File Offset: 0x0002D58C
			public bool IsExpired(DateTime now)
			{
				return this.Token == null || this.Token.IsExpired(now);
			}
		}
	}
}
