using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000082 RID: 130
	internal sealed class RSExceptionHandler
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x000106E6 File Offset: 0x0000E8E6
		public RSExceptionHandler(Func<string> getActorUriFunc)
		{
			this.m_actorUriFunc = getActorUriFunc;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000106F8 File Offset: 0x0000E8F8
		internal void HandleException(object sender, RSExceptionCreatedEventArgs e)
		{
			RSException ex = CallContext.GetData("RSExceptionHandler") as RSException;
			if (ex != null)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "RSExceptionHandler: secondary exception generated which may cause infinite recursion.\nPrimary exception={0}\nSecondary exception={1}\n", ex, e.Exception);
				Dumper.Current.DumpHere(e.Exception, text, true);
				return;
			}
			try
			{
				CallContext.SetData("RSExceptionHandler", e.Exception);
				string text2 = this.m_actorUriFunc();
				string serverProductName = RSConfiguration.ServerProductName;
				string serverProductVersion = Globals.Configuration.ServerProductVersion;
				ex = e.Exception;
				ex.SetExceptionProperties(text2, serverProductName, serverProductVersion);
			}
			finally
			{
				CallContext.SetData("RSExceptionHandler", null);
			}
		}

		// Token: 0x04000372 RID: 882
		private const string RSExceptionHandlerCallContextKey = "RSExceptionHandler";

		// Token: 0x04000373 RID: 883
		private readonly Func<string> m_actorUriFunc;
	}
}
