using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace System.Web.Http
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public sealed class HostAuthenticationAttribute : Attribute, IAuthenticationFilter, IFilter
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002808 File Offset: 0x00000A08
		public HostAuthenticationAttribute(string authenticationType)
			: this(new HostAuthenticationFilter(authenticationType))
		{
			this._authenticationType = authenticationType;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000281D File Offset: 0x00000A1D
		internal HostAuthenticationAttribute(IAuthenticationFilter innerFilter)
		{
			if (innerFilter == null)
			{
				throw new ArgumentNullException("innerFilter");
			}
			this._innerFilter = innerFilter;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000283A File Offset: 0x00000A3A
		public bool AllowMultiple
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000283D File Offset: 0x00000A3D
		public string AuthenticationType
		{
			get
			{
				return this._authenticationType;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002845 File Offset: 0x00000A45
		internal IAuthenticationFilter InnerFilter
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000284D File Offset: 0x00000A4D
		public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
		{
			return this._innerFilter.AuthenticateAsync(context, cancellationToken);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000285C File Offset: 0x00000A5C
		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
		{
			return this._innerFilter.ChallengeAsync(context, cancellationToken);
		}

		// Token: 0x0400000A RID: 10
		private readonly IAuthenticationFilter _innerFilter;

		// Token: 0x0400000B RID: 11
		private readonly string _authenticationType;
	}
}
