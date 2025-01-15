using System;
using System.ComponentModel;
using System.Web.Http.Owin;

namespace System.Web.Http
{
	// Token: 0x02000009 RID: 9
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class OwinHttpConfigurationExtensions
	{
		// Token: 0x06000057 RID: 87 RVA: 0x0000286B File Offset: 0x00000A6B
		public static void SuppressDefaultHostAuthentication(this HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			configuration.MessageHandlers.Insert(0, new PassiveAuthenticationMessageHandler());
		}
	}
}
