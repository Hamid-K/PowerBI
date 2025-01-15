using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin.Host.HttpListener.RequestProcessing
{
	// Token: 0x02000012 RID: 18
	internal class OwinHttpListenerContext : IDisposable, CallEnvironment.IPropertySource
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00005AFC File Offset: 0x00003CFC
		internal OwinHttpListenerContext(HttpListenerContext httpListenerContext, string basePath, string path, string query, DisconnectHandler disconnectHandler)
		{
			this._httpListenerContext = httpListenerContext;
			this._environment = new CallEnvironment(this);
			this._owinRequest = new OwinHttpListenerRequest(this._httpListenerContext.Request, basePath, path, query, this._environment);
			this._owinResponse = new OwinHttpListenerResponse(this._httpListenerContext, this._environment);
			this._disconnectHandler = disconnectHandler;
			this._environment.OwinVersion = "1.0";
			this.SetServerUser(this._httpListenerContext.User);
			this._environment.RequestContext = this._httpListenerContext;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005B93 File Offset: 0x00003D93
		internal CallEnvironment Environment
		{
			get
			{
				return this._environment;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005B9B File Offset: 0x00003D9B
		internal OwinHttpListenerRequest Request
		{
			get
			{
				return this._owinRequest;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005BA3 File Offset: 0x00003DA3
		internal OwinHttpListenerResponse Response
		{
			get
			{
				return this._owinResponse;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005BAB File Offset: 0x00003DAB
		internal void End(Exception ex)
		{
			if (ex != null)
			{
				this.CancelDisconnectToken();
			}
			this.End();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005BBC File Offset: 0x00003DBC
		internal void End()
		{
			try
			{
				this._disconnectRegistration.Dispose();
			}
			catch (ObjectDisposedException)
			{
			}
			this._owinResponse.End();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005BF4 File Offset: 0x00003DF4
		private static void SetDisconnected(object state)
		{
			OwinHttpListenerContext context = (OwinHttpListenerContext)state;
			context.CancelDisconnectToken();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005C10 File Offset: 0x00003E10
		private void CancelDisconnectToken()
		{
			if (this._cts != null)
			{
				try
				{
					this._cts.Cancel();
				}
				catch (ObjectDisposedException)
				{
				}
				catch (AggregateException)
				{
				}
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005C54 File Offset: 0x00003E54
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005C60 File Offset: 0x00003E60
		public virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					this._disconnectRegistration.Dispose();
				}
				catch (ObjectDisposedException)
				{
				}
				if (this._cts != null)
				{
					this._cts.Dispose();
				}
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public CancellationToken GetCallCancelled()
		{
			this._cts = new CancellationTokenSource();
			this._disconnectRegistration = this._disconnectHandler.GetDisconnectToken(this._httpListenerContext).Register(new Action<object>(OwinHttpListenerContext.SetDisconnected), this);
			return this._cts.Token;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005CF3 File Offset: 0x00003EF3
		public Stream GetRequestBody()
		{
			return this._owinRequest.GetRequestBody();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005D00 File Offset: 0x00003F00
		public string GetServerRemoteIpAddress()
		{
			return this._owinRequest.GetRemoteIpAddress();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005D0D File Offset: 0x00003F0D
		public string GetServerRemotePort()
		{
			return this._owinRequest.GetRemotePort();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005D1A File Offset: 0x00003F1A
		public string GetServerLocalIpAddress()
		{
			return this._owinRequest.GetLocalIpAddress();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005D27 File Offset: 0x00003F27
		public string GetServerLocalPort()
		{
			return this._owinRequest.GetLocalPort();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005D34 File Offset: 0x00003F34
		public bool GetServerIsLocal()
		{
			return this._owinRequest.GetIsLocal();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005D41 File Offset: 0x00003F41
		public IPrincipal GetServerUser()
		{
			return this._user;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005D49 File Offset: 0x00003F49
		public void SetServerUser(IPrincipal user)
		{
			this._user = user;
			Thread.CurrentPrincipal = this._user;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005D60 File Offset: 0x00003F60
		public bool TryGetClientCert(ref X509Certificate value)
		{
			Exception clientCertErrors = null;
			bool result = this._owinRequest.TryGetClientCert(ref value, ref clientCertErrors);
			this.Environment.ClientCertErrors = clientCertErrors;
			return result;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005D8C File Offset: 0x00003F8C
		public bool TryGetClientCertErrors(ref Exception value)
		{
			X509Certificate clientCert = null;
			bool result = this._owinRequest.TryGetClientCert(ref clientCert, ref value);
			this.Environment.ClientCert = clientCert;
			return result;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005DB7 File Offset: 0x00003FB7
		public bool TryGetWebSocketAccept(ref Action<IDictionary<string, object>, Func<IDictionary<string, object>, Task>> websocketAccept)
		{
			return this._owinResponse.TryGetWebSocketAccept(ref websocketAccept);
		}

		// Token: 0x04000087 RID: 135
		private readonly HttpListenerContext _httpListenerContext;

		// Token: 0x04000088 RID: 136
		private readonly OwinHttpListenerRequest _owinRequest;

		// Token: 0x04000089 RID: 137
		private readonly OwinHttpListenerResponse _owinResponse;

		// Token: 0x0400008A RID: 138
		private readonly CallEnvironment _environment;

		// Token: 0x0400008B RID: 139
		private readonly DisconnectHandler _disconnectHandler;

		// Token: 0x0400008C RID: 140
		private CancellationTokenSource _cts;

		// Token: 0x0400008D RID: 141
		private CancellationTokenRegistration _disconnectRegistration;

		// Token: 0x0400008E RID: 142
		private IPrincipal _user;
	}
}
