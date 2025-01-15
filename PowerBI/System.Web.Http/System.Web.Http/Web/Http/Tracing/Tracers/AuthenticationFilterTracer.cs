using System;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000121 RID: 289
	internal class AuthenticationFilterTracer : FilterTracer, IAuthenticationFilter, IFilter, IDecorator<IAuthenticationFilter>
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x000135F9 File Offset: 0x000117F9
		public AuthenticationFilterTracer(IAuthenticationFilter innerFilter, ITraceWriter traceWriter)
			: base(innerFilter, traceWriter)
		{
			this._innerFilter = innerFilter;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001360A File Offset: 0x0001180A
		public new IAuthenticationFilter Inner
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00013614 File Offset: 0x00011814
		public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
		{
			IPrincipal originalPrincipal = null;
			return base.TraceWriter.TraceBeginEndAsync((context != null) ? context.Request : null, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, "AuthenticateAsync", delegate(TraceRecord tr)
			{
				if (context != null)
				{
					originalPrincipal = context.Principal;
				}
			}, () => this._innerFilter.AuthenticateAsync(context, cancellationToken), delegate(TraceRecord tr)
			{
				if (context != null)
				{
					if (context.ErrorResult != null)
					{
						tr.Message = string.Format(CultureInfo.CurrentCulture, SRResources.AuthenticationFilterErrorResult, new object[] { context.ErrorResult });
						return;
					}
					if (context.Principal != originalPrincipal)
					{
						if (context.Principal == null || context.Principal.Identity == null)
						{
							tr.Message = SRResources.AuthenticationFilterSetPrincipalToUnknownIdentity;
							return;
						}
						tr.Message = string.Format(CultureInfo.CurrentCulture, SRResources.AuthenticationFilterSetPrincipalToKnownIdentity, new object[]
						{
							context.Principal.Identity.Name,
							context.Principal.Identity.AuthenticationType
						});
						return;
					}
					else
					{
						tr.Message = SRResources.AuthenticationFilterDidNothing;
					}
				}
			}, null);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000136A4 File Offset: 0x000118A4
		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
		{
			return base.TraceWriter.TraceBeginEndAsync((context != null) ? context.Request : null, TraceCategories.FiltersCategory, TraceLevel.Info, this._innerFilter.GetType().Name, "ChallengeAsync", null, () => this._innerFilter.ChallengeAsync(context, cancellationToken), null, null);
		}

		// Token: 0x04000206 RID: 518
		private const string AuthenticateAsyncMethodName = "AuthenticateAsync";

		// Token: 0x04000207 RID: 519
		private const string ChallengeAsyncMethodName = "ChallengeAsync";

		// Token: 0x04000208 RID: 520
		private readonly IAuthenticationFilter _innerFilter;
	}
}
