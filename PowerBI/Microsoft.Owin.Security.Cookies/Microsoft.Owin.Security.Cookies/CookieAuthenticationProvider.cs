using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.Cookies
{
	// Token: 0x0200000A RID: 10
	public class CookieAuthenticationProvider : ICookieAuthenticationProvider
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002614 File Offset: 0x00000814
		public CookieAuthenticationProvider()
		{
			this.OnValidateIdentity = (CookieValidateIdentityContext context) => Task.FromResult<object>(null);
			this.OnResponseSignIn = delegate(CookieResponseSignInContext context)
			{
			};
			this.OnResponseSignedIn = delegate(CookieResponseSignedInContext context)
			{
			};
			this.OnResponseSignOut = delegate(CookieResponseSignOutContext context)
			{
			};
			this.OnApplyRedirect = DefaultBehavior.ApplyRedirect;
			this.OnException = delegate(CookieExceptionContext context)
			{
			};
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000026EB File Offset: 0x000008EB
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000026F3 File Offset: 0x000008F3
		public Func<CookieValidateIdentityContext, Task> OnValidateIdentity { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000026FC File Offset: 0x000008FC
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002704 File Offset: 0x00000904
		public Action<CookieResponseSignInContext> OnResponseSignIn { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000270D File Offset: 0x0000090D
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002715 File Offset: 0x00000915
		public Action<CookieResponseSignedInContext> OnResponseSignedIn { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000271E File Offset: 0x0000091E
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002726 File Offset: 0x00000926
		public Action<CookieResponseSignOutContext> OnResponseSignOut { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000272F File Offset: 0x0000092F
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002737 File Offset: 0x00000937
		public Action<CookieApplyRedirectContext> OnApplyRedirect { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002740 File Offset: 0x00000940
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002748 File Offset: 0x00000948
		public Action<CookieExceptionContext> OnException { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x00002751 File Offset: 0x00000951
		public virtual Task ValidateIdentity(CookieValidateIdentityContext context)
		{
			return this.OnValidateIdentity(context);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000275F File Offset: 0x0000095F
		public virtual void ResponseSignIn(CookieResponseSignInContext context)
		{
			this.OnResponseSignIn(context);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000276D File Offset: 0x0000096D
		public virtual void ResponseSignedIn(CookieResponseSignedInContext context)
		{
			this.OnResponseSignedIn(context);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000277B File Offset: 0x0000097B
		public virtual void ResponseSignOut(CookieResponseSignOutContext context)
		{
			this.OnResponseSignOut(context);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002789 File Offset: 0x00000989
		public virtual void ApplyRedirect(CookieApplyRedirectContext context)
		{
			this.OnApplyRedirect(context);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002797 File Offset: 0x00000997
		public virtual void Exception(CookieExceptionContext context)
		{
			this.OnException(context);
		}
	}
}
