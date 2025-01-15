using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using NLog.Common;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x0200015A RID: 346
	internal class SslSocketProxy : ISocket, IDisposable
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x00029EF0 File Offset: 0x000280F0
		public SslSocketProxy(string host, SslProtocols sslProtocol, SocketProxy socketProxy)
		{
			this._socketProxy = socketProxy;
			this._host = host;
			this._sslProtocol = sslProtocol;
			this._sendCompleted = delegate(IAsyncResult ar)
			{
				this.SocketProxySendCompleted(ar);
			};
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00029F1F File Offset: 0x0002811F
		public void Close()
		{
			if (this._sslStream != null)
			{
				this._sslStream.Close();
				return;
			}
			this._socketProxy.Close();
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00029F40 File Offset: 0x00028140
		public bool ConnectAsync(SocketAsyncEventArgs args)
		{
			TcpNetworkSender.MySocketAsyncEventArgs mySocketAsyncEventArgs = new TcpNetworkSender.MySocketAsyncEventArgs();
			mySocketAsyncEventArgs.RemoteEndPoint = args.RemoteEndPoint;
			mySocketAsyncEventArgs.Completed += this.SocketProxyConnectCompleted;
			mySocketAsyncEventArgs.UserToken = args;
			if (!this._socketProxy.ConnectAsync(mySocketAsyncEventArgs))
			{
				this.SocketProxyConnectCompleted(this, mySocketAsyncEventArgs);
				return false;
			}
			return true;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00029F94 File Offset: 0x00028194
		private void SocketProxySendCompleted(IAsyncResult asyncResult)
		{
			TcpNetworkSender.MySocketAsyncEventArgs mySocketAsyncEventArgs = asyncResult.AsyncState as TcpNetworkSender.MySocketAsyncEventArgs;
			try
			{
				this._sslStream.EndWrite(asyncResult);
			}
			catch (SocketException ex)
			{
				if (mySocketAsyncEventArgs != null)
				{
					mySocketAsyncEventArgs.SocketError = ex.SocketErrorCode;
				}
			}
			catch (Exception ex2)
			{
				if (mySocketAsyncEventArgs != null)
				{
					SocketException ex3;
					if ((ex3 = ex2.InnerException as SocketException) != null)
					{
						mySocketAsyncEventArgs.SocketError = ex3.SocketErrorCode;
					}
					else
					{
						mySocketAsyncEventArgs.SocketError = SocketError.OperationAborted;
					}
				}
			}
			finally
			{
				if (mySocketAsyncEventArgs != null)
				{
					mySocketAsyncEventArgs.RaiseCompleted();
				}
			}
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0002A030 File Offset: 0x00028230
		private void SocketProxyConnectCompleted(object sender, SocketAsyncEventArgs e)
		{
			TcpNetworkSender.MySocketAsyncEventArgs mySocketAsyncEventArgs = e.UserToken as TcpNetworkSender.MySocketAsyncEventArgs;
			if (e.SocketError != SocketError.Success)
			{
				if (mySocketAsyncEventArgs != null)
				{
					mySocketAsyncEventArgs.SocketError = e.SocketError;
					mySocketAsyncEventArgs.RaiseCompleted();
					return;
				}
			}
			else
			{
				try
				{
					this._sslStream = new SslStream(new NetworkStream(this._socketProxy.UnderlyingSocket), false, new RemoteCertificateValidationCallback(SslSocketProxy.UserCertificateValidationCallback))
					{
						ReadTimeout = 20000
					};
					SslSocketProxy.AuthenticateAsClient(this._sslStream, this._host, this._sslProtocol);
				}
				catch (SocketException ex)
				{
					if (mySocketAsyncEventArgs != null)
					{
						mySocketAsyncEventArgs.SocketError = ex.SocketErrorCode;
					}
				}
				catch (Exception ex2)
				{
					if (mySocketAsyncEventArgs != null)
					{
						mySocketAsyncEventArgs.SocketError = SslSocketProxy.GetSocketError(ex2);
					}
				}
				finally
				{
					if (mySocketAsyncEventArgs != null)
					{
						mySocketAsyncEventArgs.RaiseCompleted();
					}
				}
			}
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0002A110 File Offset: 0x00028310
		private static SocketError GetSocketError(Exception ex)
		{
			SocketException ex2;
			SocketError socketError;
			if ((ex2 = ex.InnerException as SocketException) != null)
			{
				socketError = ex2.SocketErrorCode;
			}
			else
			{
				socketError = SocketError.ConnectionRefused;
			}
			return socketError;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0002A13C File Offset: 0x0002833C
		private static void AuthenticateAsClient(SslStream sslStream, string targetHost, SslProtocols enabledSslProtocols)
		{
			if (enabledSslProtocols != SslProtocols.Default)
			{
				sslStream.AuthenticateAsClient(targetHost, null, enabledSslProtocols, false);
				return;
			}
			sslStream.AuthenticateAsClient(targetHost);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0002A158 File Offset: 0x00028358
		private static bool UserCertificateValidationCallback(object sender, object certificate, object chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			InternalLogger.Debug<SslPolicyErrors, object>("SSL certificate errors were encountered when establishing connection to the server: {0}, Certificate: {1}", sslPolicyErrors, certificate);
			return false;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0002A16C File Offset: 0x0002836C
		public bool SendAsync(SocketAsyncEventArgs args)
		{
			this._sslStream.BeginWrite(args.Buffer, args.Offset, args.Count, this._sendCompleted, args);
			return true;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0002A194 File Offset: 0x00028394
		public bool SendToAsync(SocketAsyncEventArgs args)
		{
			return this.SendAsync(args);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002A19D File Offset: 0x0002839D
		public void Dispose()
		{
			if (this._sslStream != null)
			{
				this._sslStream.Dispose();
				return;
			}
			this._socketProxy.Dispose();
		}

		// Token: 0x0400045B RID: 1115
		private readonly AsyncCallback _sendCompleted;

		// Token: 0x0400045C RID: 1116
		private readonly SocketProxy _socketProxy;

		// Token: 0x0400045D RID: 1117
		private readonly string _host;

		// Token: 0x0400045E RID: 1118
		private readonly SslProtocols _sslProtocol;

		// Token: 0x0400045F RID: 1119
		private SslStream _sslStream;
	}
}
