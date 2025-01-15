using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Owin.Properties;
using Microsoft.Owin.Security;

namespace System.Web.Http
{
	// Token: 0x0200000A RID: 10
	public class HostAuthenticationFilter : IAuthenticationFilter, IFilter
	{
		// Token: 0x06000058 RID: 88 RVA: 0x0000288C File Offset: 0x00000A8C
		public HostAuthenticationFilter(string authenticationType)
		{
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			this._authenticationType = authenticationType;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000028A9 File Offset: 0x00000AA9
		public string AuthenticationType
		{
			get
			{
				return this._authenticationType;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000028B4 File Offset: 0x00000AB4
		public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			HttpRequestMessage request = context.Request;
			if (request == null)
			{
				throw new InvalidOperationException(OwinResources.HttpAuthenticationContext_RequestMustNotBeNull);
			}
			IAuthenticationManager authenticationManagerOrThrow = HostAuthenticationFilter.GetAuthenticationManagerOrThrow(request);
			cancellationToken.ThrowIfCancellationRequested();
			AuthenticateResult authenticateResult = await authenticationManagerOrThrow.AuthenticateAsync(this._authenticationType);
			if (authenticateResult != null)
			{
				IIdentity identity = authenticateResult.Identity;
				if (identity != null)
				{
					context.Principal = new ClaimsPrincipal(identity);
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000290C File Offset: 0x00000B0C
		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			HttpRequestMessage request = context.Request;
			if (request == null)
			{
				throw new InvalidOperationException(OwinResources.HttpAuthenticationChallengeContext_RequestMustNotBeNull);
			}
			IAuthenticationManager authenticationManagerOrThrow = HostAuthenticationFilter.GetAuthenticationManagerOrThrow(request);
			authenticationManagerOrThrow.AuthenticationResponseChallenge = HostAuthenticationFilter.AddChallengeAuthenticationType(authenticationManagerOrThrow.AuthenticationResponseChallenge, this._authenticationType);
			return TaskHelpers.Completed();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000283A File Offset: 0x00000A3A
		public bool AllowMultiple
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000295C File Offset: 0x00000B5C
		private static AuthenticationResponseChallenge AddChallengeAuthenticationType(AuthenticationResponseChallenge challenge, string authenticationType)
		{
			List<string> list = new List<string>();
			AuthenticationProperties authenticationProperties;
			if (challenge != null)
			{
				string[] authenticationTypes = challenge.AuthenticationTypes;
				if (authenticationTypes != null)
				{
					list.AddRange(authenticationTypes);
				}
				authenticationProperties = challenge.Properties;
			}
			else
			{
				authenticationProperties = new AuthenticationProperties();
			}
			list.Add(authenticationType);
			return new AuthenticationResponseChallenge(list.ToArray(), authenticationProperties);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000029A5 File Offset: 0x00000BA5
		private static IAuthenticationManager GetAuthenticationManagerOrThrow(HttpRequestMessage request)
		{
			IAuthenticationManager authenticationManager = request.GetAuthenticationManager();
			if (authenticationManager == null)
			{
				throw new InvalidOperationException(OwinResources.IAuthenticationManagerNotAvailable);
			}
			return authenticationManager;
		}

		// Token: 0x0400000C RID: 12
		private readonly string _authenticationType;
	}
}
