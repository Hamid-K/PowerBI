using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000132 RID: 306
	internal class EndpointProtocolOptionsHelper : OptionsHelper<EndpointProtocolOptions>
	{
		// Token: 0x060014C6 RID: 5318 RVA: 0x00090E9C File Offset: 0x0008F09C
		private EndpointProtocolOptionsHelper()
		{
			base.AddOptionMapping(EndpointProtocolOptions.HttpAuthenticationRealm, "AUTH_REALM");
			base.AddOptionMapping(EndpointProtocolOptions.HttpAuthentication, "AUTHENTICATION");
			base.AddOptionMapping(EndpointProtocolOptions.HttpClearPort, "CLEAR_PORT");
			base.AddOptionMapping(EndpointProtocolOptions.HttpCompression, "COMPRESSION");
			base.AddOptionMapping(EndpointProtocolOptions.HttpDefaultLogonDomain, "DEFAULT_LOGON_DOMAIN");
			base.AddOptionMapping(EndpointProtocolOptions.HttpPath, "PATH");
			base.AddOptionMapping(EndpointProtocolOptions.HttpPorts, "PORTS");
			base.AddOptionMapping(EndpointProtocolOptions.HttpSite, "SITE");
			base.AddOptionMapping(EndpointProtocolOptions.HttpSslPort, "SSL_PORT");
			base.AddOptionMapping(EndpointProtocolOptions.TcpListenerIP, "LISTENER_IP");
			base.AddOptionMapping(EndpointProtocolOptions.TcpListenerPort, "LISTENER_PORT");
		}

		// Token: 0x04001170 RID: 4464
		internal static readonly EndpointProtocolOptionsHelper Instance = new EndpointProtocolOptionsHelper();
	}
}
