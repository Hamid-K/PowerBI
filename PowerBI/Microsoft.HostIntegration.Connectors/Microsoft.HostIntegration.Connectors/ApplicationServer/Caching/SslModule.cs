using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200018A RID: 394
	internal class SslModule : IChannelSecurityModule
	{
		// Token: 0x06000CBD RID: 3261 RVA: 0x0002BAE7 File Offset: 0x00029CE7
		internal SslModule(string logPrefix)
		{
			this._logSource = logPrefix + "SslModule";
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002BB0C File Offset: 0x00029D0C
		internal void Initialize(string certificateSubjectName)
		{
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.ReadOnly);
			X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, certificateSubjectName, false);
			if (x509Certificate2Collection.Count == 1)
			{
				this._serverCertificate = x509Certificate2Collection[0];
				return;
			}
			string text = null;
			if (x509Certificate2Collection.Count == 0)
			{
				text = string.Format(CultureInfo.InvariantCulture, "Certificate with name {0} doesn't exist", new object[] { certificateSubjectName });
			}
			else if (x509Certificate2Collection.Count > 1)
			{
				text = string.Format(CultureInfo.InvariantCulture, "More than one certificate with the name {0} exist", new object[] { certificateSubjectName });
			}
			if (Provider.IsEnabled(TraceLevel.Error))
			{
				EventLogWriter.WriteError(this._logSource, text, new object[0]);
			}
			throw new DataCacheException(this._logSource, 9013, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9013, certificateSubjectName));
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002BBD8 File Offset: 0x00029DD8
		public void BeginAuthenticateAsClient(Socket socket, DataCacheSecurity securityProperties, AsyncCallback callback, object state, TimeSpan timeout)
		{
			SslStream sslStream = null;
			SslModule.ClientAuthOperationState clientAuthOperationState = null;
			try
			{
				SslModule.SslCertificateValidator sslCertificateValidator = new SslModule.SslCertificateValidator();
				sslStream = new SslStream(new NetworkStream(socket, true), false, new RemoteCertificateValidationCallback(sslCertificateValidator.ValidateServerCertificate), null);
				AsyncResult<SecureStream> asyncResult = new AsyncResult<SecureStream>(callback, state);
				clientAuthOperationState = new SslModule.ClientAuthOperationState(socket, sslStream, asyncResult, sslCertificateValidator);
				clientAuthOperationState.AuthenticationTimer = new global::System.Threading.Timer(new TimerCallback(this.OnAuthTimerExpired), clientAuthOperationState, timeout, TimeSpan.FromMilliseconds(-1.0));
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<EndPoint>(this._logSource, "Beginning Authentication to {0}", socket.RemoteEndPoint);
				}
				sslStream.BeginAuthenticateAsClient(securityProperties.SslSubjectIdentity, null, SslProtocols.Tls, true, new AsyncCallback(this.ClientAuthenticationCallback), clientAuthOperationState);
			}
			catch
			{
				if (sslStream != null)
				{
					sslStream.Dispose();
				}
				if (clientAuthOperationState != null && clientAuthOperationState.AuthenticationTimer != null)
				{
					clientAuthOperationState.AuthenticationTimer.Dispose();
				}
				throw;
			}
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002BCB8 File Offset: 0x00029EB8
		private void OnAuthTimerExpired(object state)
		{
			SslModule.AuthOperationState authOperationState = (SslModule.AuthOperationState)state;
			if (!authOperationState.TryComplete())
			{
				return;
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>(this._logSource, "Authentication Timer expired for connection {0}", authOperationState.RemoteEndPoint);
			}
			authOperationState.Stream.Close();
			authOperationState.AuthenticationTimer.Dispose();
			authOperationState.AsyncResult.SetAsCompleted(new TimeoutException("Authentication timed out"), false);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002BD1F File Offset: 0x00029F1F
		public SecureStream EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			return ((AsyncResult<SecureStream>)asyncResult).EndInvoke();
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002BD2C File Offset: 0x00029F2C
		private void ClientAuthenticationCallback(IAsyncResult asyncResult)
		{
			SslModule.ClientAuthOperationState clientAuthOperationState = (SslModule.ClientAuthOperationState)asyncResult.AsyncState;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>(this._logSource, "AuthenticationCallback called for endpoint {0}", clientAuthOperationState.RemoteEndPoint);
			}
			if (!clientAuthOperationState.TryComplete())
			{
				return;
			}
			clientAuthOperationState.AuthenticationTimer.Dispose();
			try
			{
				SslStream stream = clientAuthOperationState.Stream;
				stream.EndAuthenticateAsClient(asyncResult);
				this.CompleteAuthentication(clientAuthOperationState);
			}
			catch (Exception ex)
			{
				clientAuthOperationState.Stream.Close();
				if (!TcpUtility.IsAuthenticationFailure(ex))
				{
					throw;
				}
				if (clientAuthOperationState.certificateValidator.IsRevocationServerOffline)
				{
					ChannelAuthenticationException ex2 = new ChannelAuthenticationException(ErrStatus.CHANNEL_AUTH_CRL_OFFLINE, "CRL Server is offline", ex);
					clientAuthOperationState.AsyncResult.SetAsCompleted(ex2, false);
				}
				else if (ex is AuthenticationException)
				{
					ChannelAuthenticationException ex3 = new ChannelAuthenticationException(ErrStatus.CHANNEL_AUTH_FAILED, "SSL Authentication failed", ex);
					clientAuthOperationState.AsyncResult.SetAsCompleted(ex3, false);
				}
				else
				{
					clientAuthOperationState.AsyncResult.SetAsCompleted(ex, false);
				}
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002BE18 File Offset: 0x0002A018
		private void CompleteAuthentication(SslModule.AuthOperationState authState)
		{
			SslStream stream = authState.Stream;
			if (SslModule.ValidateSslStreamSecurity(stream))
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string>(this._logSource, "SSL Authentication completed for endpoint {0}", authState.RemoteEndPoint);
				}
				SecureStream secureStream = new SecureStream(stream, authState.Socket);
				authState.AsyncResult.SetAsCompleted(secureStream, false);
				return;
			}
			ChannelAuthenticationException ex = new ChannelAuthenticationException(ErrStatus.CHANNEL_AUTH_FAILED, "SSL Authentication failed to establish a Secure Stream");
			stream.Close();
			authState.AsyncResult.SetAsCompleted(ex, false);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002BE8D File Offset: 0x0002A08D
		private static bool ValidateSslStreamSecurity(SslStream stream)
		{
			return stream.IsAuthenticated && stream.IsEncrypted && stream.IsSigned;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		public void BeginAuthenticateAsServer(Socket socket, DataCacheSecurity securityProperties, AsyncCallback callback, object state, TimeSpan timeout)
		{
			SslStream sslStream = null;
			SslModule.AuthOperationState authOperationState = null;
			try
			{
				sslStream = new SslStream(new NetworkStream(socket, true), false);
				AsyncResult<SecureStream> asyncResult = new AsyncResult<SecureStream>(callback, state);
				authOperationState = new SslModule.AuthOperationState(socket, sslStream, asyncResult);
				authOperationState.AuthenticationTimer = new global::System.Threading.Timer(new TimerCallback(this.OnAuthTimerExpired), authOperationState, timeout, TimeSpan.FromMilliseconds(-1.0));
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<EndPoint>(this._logSource, "Beginning Authentication with Client - {0}", socket.RemoteEndPoint);
				}
				sslStream.BeginAuthenticateAsServer(this._serverCertificate, false, SslProtocols.Tls, true, new AsyncCallback(this.ServerAuthenticationCallback), authOperationState);
			}
			catch
			{
				if (sslStream != null)
				{
					sslStream.Dispose();
				}
				if (authOperationState != null && authOperationState.AuthenticationTimer != null)
				{
					authOperationState.AuthenticationTimer.Dispose();
				}
				throw;
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002BD1F File Offset: 0x00029F1F
		public SecureStream EndAuthenticateAsServer(IAsyncResult asyncResult)
		{
			return ((AsyncResult<SecureStream>)asyncResult).EndInvoke();
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002BF74 File Offset: 0x0002A174
		private void ServerAuthenticationCallback(IAsyncResult asyncResult)
		{
			SslModule.AuthOperationState authOperationState = (SslModule.AuthOperationState)asyncResult.AsyncState;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>(this._logSource, "AuthenticationCallback called for endpoint - {0}", authOperationState.RemoteEndPoint);
			}
			if (!authOperationState.TryComplete())
			{
				return;
			}
			authOperationState.AuthenticationTimer.Dispose();
			try
			{
				SslStream stream = authOperationState.Stream;
				stream.EndAuthenticateAsServer(asyncResult);
				this.CompleteAuthentication(authOperationState);
			}
			catch (Exception ex)
			{
				authOperationState.Stream.Close();
				if (!TcpUtility.IsAuthenticationFailure(ex))
				{
					throw;
				}
				authOperationState.AsyncResult.SetAsCompleted(ex, false);
			}
		}

		// Token: 0x04000907 RID: 2311
		private string _logSource = "";

		// Token: 0x04000908 RID: 2312
		private X509Certificate _serverCertificate;

		// Token: 0x0200018B RID: 395
		private class AuthOperationState
		{
			// Token: 0x06000CC8 RID: 3272 RVA: 0x0002C010 File Offset: 0x0002A210
			public AuthOperationState(Socket socket, SslStream stream, AsyncResult<SecureStream> userState)
			{
				this.Stream = stream;
				this.AsyncResult = userState;
				this.Socket = socket;
				this.RemoteEndPoint = socket.RemoteEndPoint.ToString();
				this._completed = 0;
			}

			// Token: 0x06000CC9 RID: 3273 RVA: 0x0002C045 File Offset: 0x0002A245
			public bool TryComplete()
			{
				return Interlocked.CompareExchange(ref this._completed, 1, 0) == 0;
			}

			// Token: 0x04000909 RID: 2313
			internal readonly SslStream Stream;

			// Token: 0x0400090A RID: 2314
			internal readonly AsyncResult<SecureStream> AsyncResult;

			// Token: 0x0400090B RID: 2315
			internal global::System.Threading.Timer AuthenticationTimer;

			// Token: 0x0400090C RID: 2316
			internal readonly Socket Socket;

			// Token: 0x0400090D RID: 2317
			internal readonly string RemoteEndPoint;

			// Token: 0x0400090E RID: 2318
			private int _completed;
		}

		// Token: 0x0200018C RID: 396
		private class ClientAuthOperationState : SslModule.AuthOperationState
		{
			// Token: 0x06000CCA RID: 3274 RVA: 0x0002C057 File Offset: 0x0002A257
			public ClientAuthOperationState(Socket socket, SslStream stream, AsyncResult<SecureStream> userState, SslModule.SslCertificateValidator validator)
				: base(socket, stream, userState)
			{
				this.certificateValidator = validator;
			}

			// Token: 0x0400090F RID: 2319
			internal readonly SslModule.SslCertificateValidator certificateValidator;
		}

		// Token: 0x0200018D RID: 397
		private class SslCertificateValidator
		{
			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0002C06A File Offset: 0x0002A26A
			internal bool IsRevocationServerOffline
			{
				get
				{
					return this._isRevocationServerOffline;
				}
			}

			// Token: 0x06000CCC RID: 3276 RVA: 0x0002C074 File Offset: 0x0002A274
			public bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				if (sslPolicyErrors == SslPolicyErrors.None)
				{
					return true;
				}
				foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
				{
					if (x509ChainStatus.Status == X509ChainStatusFlags.OfflineRevocation)
					{
						this._isRevocationServerOffline = true;
					}
				}
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(this._logSource, "Certificate Validation failed because of {0} ; Chain Details - {1}", new object[]
					{
						sslPolicyErrors,
						SslModule.SslCertificateValidator.GetCertificateChainDetails(chain)
					});
				}
				return false;
			}

			// Token: 0x06000CCD RID: 3277 RVA: 0x0002C0F4 File Offset: 0x0002A2F4
			private static string GetCertificateChainDetails(X509Chain chain)
			{
				string text = null;
				foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
				{
					text += string.Format(CultureInfo.InvariantCulture, "Status:{0} ; ", new object[] { x509ChainStatus.Status });
				}
				foreach (X509ChainElement x509ChainElement in chain.ChainElements)
				{
					text += string.Format(CultureInfo.InvariantCulture, "Certificate - Sub:{0} , Expiry:{1} ; ", new object[]
					{
						x509ChainElement.Certificate.Subject,
						x509ChainElement.Certificate.GetExpirationDateString()
					});
				}
				return text;
			}

			// Token: 0x04000910 RID: 2320
			private string _logSource = "SslCertificateValidator";

			// Token: 0x04000911 RID: 2321
			private bool _isRevocationServerOffline;
		}

		// Token: 0x0200018E RID: 398
		internal static class SslAuthenticationProperties
		{
			// Token: 0x04000912 RID: 2322
			internal const SslProtocols SslProtocol = SslProtocols.Tls;

			// Token: 0x04000913 RID: 2323
			internal const bool CheckForCertRevocation = true;

			// Token: 0x04000914 RID: 2324
			internal const StoreName CertificateStoreName = StoreName.My;

			// Token: 0x04000915 RID: 2325
			internal const StoreLocation CertificateStoreLocation = StoreLocation.LocalMachine;
		}
	}
}
