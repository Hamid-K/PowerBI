using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Infrastructure
{
	// Token: 0x02000023 RID: 35
	internal static class OwinRequestExtensions
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003008 File Offset: 0x00001208
		public static object RegisterAuthenticationHandler(this IOwinRequest request, AuthenticationHandler handler)
		{
			Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task> chained = request.Get<Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task>>(Constants.SecurityAuthenticate);
			OwinRequestExtensions.Hook hook = new OwinRequestExtensions.Hook(handler, chained);
			request.Set<Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task>>(Constants.SecurityAuthenticate, new Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task>(hook.AuthenticateAsync));
			return hook;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003044 File Offset: 0x00001244
		public static void UnregisterAuthenticationHandler(this IOwinRequest request, object registration)
		{
			OwinRequestExtensions.Hook hook = registration as OwinRequestExtensions.Hook;
			if (hook == null)
			{
				throw new InvalidOperationException(Resources.Exception_UnhookAuthenticationStateType);
			}
			request.Set<Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task>>(Constants.SecurityAuthenticate, hook.Chained);
		}

		// Token: 0x02000042 RID: 66
		private class Hook
		{
			// Token: 0x060000F2 RID: 242 RVA: 0x000045AE File Offset: 0x000027AE
			public Hook(AuthenticationHandler handler, Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task> chained)
			{
				this._handler = handler;
				this.Chained = chained;
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000F3 RID: 243 RVA: 0x000045C4 File Offset: 0x000027C4
			// (set) Token: 0x060000F4 RID: 244 RVA: 0x000045CC File Offset: 0x000027CC
			public Func<string[], Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object>, object, Task> Chained { get; private set; }

			// Token: 0x060000F5 RID: 245 RVA: 0x000045D8 File Offset: 0x000027D8
			public async Task AuthenticateAsync(string[] authenticationTypes, Action<IIdentity, IDictionary<string, string>, IDictionary<string, object>, object> callback, object state)
			{
				if (authenticationTypes == null)
				{
					callback(null, null, this._handler.BaseOptions.Description.Properties, state);
				}
				else if (authenticationTypes.Contains(this._handler.BaseOptions.AuthenticationType, StringComparer.Ordinal))
				{
					AuthenticationTicket authenticationTicket = await this._handler.AuthenticateAsync();
					AuthenticationTicket ticket = authenticationTicket;
					if (ticket != null && ticket.Identity != null)
					{
						callback(ticket.Identity, ticket.Properties.Dictionary, this._handler.BaseOptions.Description.Properties, state);
					}
				}
				if (this.Chained != null)
				{
					await this.Chained(authenticationTypes, callback, state);
				}
			}

			// Token: 0x04000090 RID: 144
			private readonly AuthenticationHandler _handler;
		}
	}
}
