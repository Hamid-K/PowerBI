using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x0200001B RID: 27
	public abstract class AuthenticationHandler
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000027B0 File Offset: 0x000009B0
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000027B8 File Offset: 0x000009B8
		private protected IOwinContext Context { protected get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000027C1 File Offset: 0x000009C1
		protected IOwinRequest Request
		{
			get
			{
				return this.Context.Request;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000027CE File Offset: 0x000009CE
		protected IOwinResponse Response
		{
			get
			{
				return this.Context.Response;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000027DB File Offset: 0x000009DB
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000027E3 File Offset: 0x000009E3
		private protected PathString RequestPathBase { protected get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000027EC File Offset: 0x000009EC
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000027F4 File Offset: 0x000009F4
		private protected SecurityHelper Helper { protected get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000027FD File Offset: 0x000009FD
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002805 File Offset: 0x00000A05
		protected bool Faulted { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000280E File Offset: 0x00000A0E
		internal AuthenticationOptions BaseOptions
		{
			get
			{
				return this._baseOptions;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002818 File Offset: 0x00000A18
		protected async Task BaseInitializeAsync(AuthenticationOptions options, IOwinContext context)
		{
			this._baseOptions = options;
			this.Context = context;
			this.Helper = new SecurityHelper(context);
			this.RequestPathBase = this.Request.PathBase;
			this._registration = this.Request.RegisterAuthenticationHandler(this);
			this.Response.OnSendingHeaders(new Action<object>(AuthenticationHandler.OnSendingHeaderCallback), this);
			await this.InitializeCoreAsync();
			if (this.BaseOptions.AuthenticationMode == AuthenticationMode.Active)
			{
				AuthenticationTicket ticket = await this.AuthenticateAsync();
				if (ticket != null && ticket.Identity != null)
				{
					this.Helper.AddUserIdentity(ticket.Identity);
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000286C File Offset: 0x00000A6C
		private static void OnSendingHeaderCallback(object state)
		{
			AuthenticationHandler handler = (AuthenticationHandler)state;
			handler.ApplyResponseAsync().Wait();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000288B File Offset: 0x00000A8B
		protected virtual Task InitializeCoreAsync()
		{
			return Task.FromResult<object>(null);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002894 File Offset: 0x00000A94
		internal async Task TeardownAsync()
		{
			await this.ApplyResponseAsync();
			await this.TeardownCoreAsync();
			this.Request.UnregisterAuthenticationHandler(this._registration);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000028D7 File Offset: 0x00000AD7
		protected virtual Task TeardownCoreAsync()
		{
			return Task.FromResult<object>(null);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000028DF File Offset: 0x00000ADF
		public virtual Task<bool> InvokeAsync()
		{
			return Task.FromResult<bool>(false);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000028E7 File Offset: 0x00000AE7
		public Task<AuthenticationTicket> AuthenticateAsync()
		{
			return LazyInitializer.EnsureInitialized<Task<AuthenticationTicket>>(ref this._authenticate, ref this._authenticateInitialized, ref this._authenticateSyncLock, new Func<Task<AuthenticationTicket>>(this.AuthenticateCoreAsync));
		}

		// Token: 0x06000071 RID: 113
		protected abstract Task<AuthenticationTicket> AuthenticateCoreAsync();

		// Token: 0x06000072 RID: 114 RVA: 0x00002910 File Offset: 0x00000B10
		private async Task ApplyResponseAsync()
		{
			try
			{
				if (!this.Faulted)
				{
					await LazyInitializer.EnsureInitialized<Task>(ref this._applyResponse, ref this._applyResponseInitialized, ref this._applyResponseSyncLock, new Func<Task>(this.ApplyResponseCoreAsync));
				}
			}
			catch (Exception)
			{
				this.Faulted = true;
				throw;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002954 File Offset: 0x00000B54
		protected virtual async Task ApplyResponseCoreAsync()
		{
			await this.ApplyResponseGrantAsync();
			await this.ApplyResponseChallengeAsync();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002997 File Offset: 0x00000B97
		protected virtual Task ApplyResponseGrantAsync()
		{
			return Task.FromResult<object>(null);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000299F File Offset: 0x00000B9F
		protected virtual Task ApplyResponseChallengeAsync()
		{
			return Task.FromResult<object>(null);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000029A8 File Offset: 0x00000BA8
		protected void GenerateCorrelationId(AuthenticationProperties properties)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			string correlationKey = ".AspNet.Correlation." + this.BaseOptions.AuthenticationType;
			byte[] nonceBytes = new byte[32];
			AuthenticationHandler.Random.GetBytes(nonceBytes);
			string correlationId = TextEncodings.Base64Url.Encode(nonceBytes);
			CookieOptions cookieOptions = new CookieOptions
			{
				SameSite = new SameSiteMode?(SameSiteMode.None),
				HttpOnly = true,
				Secure = this.Request.IsSecure
			};
			properties.Dictionary[correlationKey] = correlationId;
			this.Response.Cookies.Append(correlationKey, correlationId, cookieOptions);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002A44 File Offset: 0x00000C44
		protected void GenerateCorrelationId(ICookieManager cookieManager, AuthenticationProperties properties)
		{
			if (cookieManager == null)
			{
				throw new ArgumentNullException("cookieManager");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			string correlationKey = ".AspNet.Correlation." + this.BaseOptions.AuthenticationType;
			byte[] nonceBytes = new byte[32];
			AuthenticationHandler.Random.GetBytes(nonceBytes);
			string correlationId = TextEncodings.Base64Url.Encode(nonceBytes);
			CookieOptions cookieOptions = new CookieOptions
			{
				SameSite = new SameSiteMode?(SameSiteMode.None),
				HttpOnly = true,
				Secure = this.Request.IsSecure
			};
			properties.Dictionary[correlationKey] = correlationId;
			cookieManager.AppendResponseCookie(this.Context, correlationKey, correlationId, cookieOptions);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002AE8 File Offset: 0x00000CE8
		protected bool ValidateCorrelationId(AuthenticationProperties properties, ILogger logger)
		{
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			string correlationKey = ".AspNet.Correlation." + this.BaseOptions.AuthenticationType;
			string correlationCookie = this.Request.Cookies[correlationKey];
			if (string.IsNullOrWhiteSpace(correlationCookie))
			{
				logger.WriteWarning("{0} cookie not found.", new string[] { correlationKey });
				return false;
			}
			CookieOptions cookieOptions = new CookieOptions
			{
				SameSite = new SameSiteMode?(SameSiteMode.None),
				HttpOnly = true,
				Secure = this.Request.IsSecure
			};
			this.Response.Cookies.Delete(correlationKey, cookieOptions);
			string correlationExtra;
			if (!properties.Dictionary.TryGetValue(correlationKey, out correlationExtra))
			{
				logger.WriteWarning("{0} state property not found.", new string[] { correlationKey });
				return false;
			}
			properties.Dictionary.Remove(correlationKey);
			if (!string.Equals(correlationCookie, correlationExtra, StringComparison.Ordinal))
			{
				logger.WriteWarning("{0} correlation cookie and state property mismatch.", new string[] { correlationKey });
				return false;
			}
			return true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002BEC File Offset: 0x00000DEC
		protected bool ValidateCorrelationId(ICookieManager cookieManager, AuthenticationProperties properties, ILogger logger)
		{
			if (cookieManager == null)
			{
				throw new ArgumentNullException("cookieManager");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			string correlationKey = ".AspNet.Correlation." + this.BaseOptions.AuthenticationType;
			string correlationCookie = cookieManager.GetRequestCookie(this.Context, correlationKey);
			if (string.IsNullOrWhiteSpace(correlationCookie))
			{
				logger.WriteWarning("{0} cookie not found.", new string[] { correlationKey });
				return false;
			}
			CookieOptions cookieOptions = new CookieOptions
			{
				SameSite = new SameSiteMode?(SameSiteMode.None),
				HttpOnly = true,
				Secure = this.Request.IsSecure
			};
			cookieManager.DeleteCookie(this.Context, correlationKey, cookieOptions);
			string correlationExtra;
			if (!properties.Dictionary.TryGetValue(correlationKey, out correlationExtra))
			{
				logger.WriteWarning("{0} state property not found.", new string[] { correlationKey });
				return false;
			}
			properties.Dictionary.Remove(correlationKey);
			if (!string.Equals(correlationCookie, correlationExtra, StringComparison.Ordinal))
			{
				logger.WriteWarning("{0} correlation cookie and state property mismatch.", new string[] { correlationKey });
				return false;
			}
			return true;
		}

		// Token: 0x0400002B RID: 43
		private static readonly RNGCryptoServiceProvider Random = new RNGCryptoServiceProvider();

		// Token: 0x0400002C RID: 44
		private object _registration;

		// Token: 0x0400002D RID: 45
		private Task<AuthenticationTicket> _authenticate;

		// Token: 0x0400002E RID: 46
		private bool _authenticateInitialized;

		// Token: 0x0400002F RID: 47
		private object _authenticateSyncLock;

		// Token: 0x04000030 RID: 48
		private Task _applyResponse;

		// Token: 0x04000031 RID: 49
		private bool _applyResponseInitialized;

		// Token: 0x04000032 RID: 50
		private object _applyResponseSyncLock;

		// Token: 0x04000033 RID: 51
		private AuthenticationOptions _baseOptions;
	}
}
