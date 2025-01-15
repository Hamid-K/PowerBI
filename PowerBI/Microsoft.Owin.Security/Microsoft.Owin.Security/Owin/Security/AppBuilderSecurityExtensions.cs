using System;
using Owin;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000003 RID: 3
	public static class AppBuilderSecurityExtensions
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002050 File Offset: 0x00000250
		public static string GetDefaultSignInAsAuthenticationType(this IAppBuilder app)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			object value;
			if (app.Properties.TryGetValue("Microsoft.Owin.Security.Constants.DefaultSignInAsAuthenticationType", out value))
			{
				string authenticationType = value as string;
				if (!string.IsNullOrEmpty(authenticationType))
				{
					return authenticationType;
				}
			}
			throw new InvalidOperationException(Resources.Exception_MissingDefaultSignInAsAuthenticationType);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000209A File Offset: 0x0000029A
		public static void SetDefaultSignInAsAuthenticationType(this IAppBuilder app, string authenticationType)
		{
			if (app == null)
			{
				throw new ArgumentNullException("app");
			}
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			app.Properties["Microsoft.Owin.Security.Constants.DefaultSignInAsAuthenticationType"] = authenticationType;
		}
	}
}
