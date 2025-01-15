using System;
using System.Collections.Generic;

namespace AngleSharp.Network
{
	// Token: 0x0200009A RID: 154
	internal static class PortNumbers
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
		public static string GetDefaultPort(string protocol)
		{
			string text = null;
			PortNumbers.Ports.TryGetValue(protocol, out text);
			return text;
		}

		// Token: 0x040003A1 RID: 929
		private static readonly Dictionary<string, string> Ports = new Dictionary<string, string>
		{
			{
				ProtocolNames.Http,
				"80"
			},
			{
				ProtocolNames.Https,
				"443"
			},
			{
				ProtocolNames.Ftp,
				"21"
			},
			{
				ProtocolNames.File,
				""
			},
			{
				ProtocolNames.Ws,
				"80"
			},
			{
				ProtocolNames.Wss,
				"443"
			},
			{
				ProtocolNames.Gopher,
				"70"
			},
			{
				ProtocolNames.Telnet,
				"23"
			},
			{
				ProtocolNames.Ssh,
				"22"
			}
		};
	}
}
