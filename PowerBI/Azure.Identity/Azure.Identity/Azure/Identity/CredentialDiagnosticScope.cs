using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Identity
{
	// Token: 0x0200002C RID: 44
	internal readonly struct CredentialDiagnosticScope : IDisposable
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00004CA6 File Offset: 0x00002EA6
		public CredentialDiagnosticScope(ClientDiagnostics diagnostics, string name, TokenRequestContext context, IScopeHandler scopeHandler)
		{
			this._name = name;
			this._scope = scopeHandler.CreateScope(diagnostics, name);
			this._context = context;
			this._scopeHandler = scopeHandler;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004CCD File Offset: 0x00002ECD
		public void Start()
		{
			AzureIdentityEventSource.Singleton.GetToken(this._name, this._context);
			this._scopeHandler.Start(this._name, in this._scope);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004CFC File Offset: 0x00002EFC
		public AccessToken Succeeded(AccessToken token)
		{
			AzureIdentityEventSource.Singleton.GetTokenSucceeded(this._name, this._context, token.ExpiresOn);
			return token;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004D1C File Offset: 0x00002F1C
		public Exception FailWrapAndThrow(Exception ex, string additionalMessage = null, bool isCredentialUnavailable = false)
		{
			bool flag = this.TryWrapException(ref ex, additionalMessage, false);
			this.RegisterFailed(ex);
			if (!flag)
			{
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
			throw ex;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004D3D File Offset: 0x00002F3D
		private void RegisterFailed(Exception ex)
		{
			AzureIdentityEventSource.Singleton.GetTokenFailed(this._name, this._context, ex);
			this._scopeHandler.Fail(this._name, in this._scope, ex);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004D70 File Offset: 0x00002F70
		private bool TryWrapException(ref Exception exception, string additionalMessageText = null, bool isCredentialUnavailable = false)
		{
			if (exception is OperationCanceledException || exception is AuthenticationFailedException)
			{
				return false;
			}
			AggregateException ex = exception as AggregateException;
			if (ex != null)
			{
				CredentialUnavailableException ex2 = ex.Flatten().InnerExceptions.OfType<CredentialUnavailableException>().FirstOrDefault<CredentialUnavailableException>();
				if (ex2 != null)
				{
					exception = new CredentialUnavailableException(ex2.Message, ex);
					return true;
				}
			}
			string text = this._name.Substring(0, this._name.IndexOf('.')) + " authentication failed: " + exception.Message;
			if (additionalMessageText != null)
			{
				text = text + "\n" + additionalMessageText;
			}
			exception = (isCredentialUnavailable ? new CredentialUnavailableException(text, exception) : new AuthenticationFailedException(text, exception));
			return true;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004E18 File Offset: 0x00003018
		public void Dispose()
		{
			this._scopeHandler.Dispose(this._name, in this._scope);
		}

		// Token: 0x040000B2 RID: 178
		private readonly string _name;

		// Token: 0x040000B3 RID: 179
		private readonly DiagnosticScope _scope;

		// Token: 0x040000B4 RID: 180
		private readonly TokenRequestContext _context;

		// Token: 0x040000B5 RID: 181
		private readonly IScopeHandler _scopeHandler;
	}
}
