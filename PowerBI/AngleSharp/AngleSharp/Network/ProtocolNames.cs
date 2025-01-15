using System;
using AngleSharp.Extensions;

namespace AngleSharp.Network
{
	// Token: 0x0200009B RID: 155
	public static class ProtocolNames
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x0001E5B7 File Offset: 0x0001C7B7
		public static bool IsRelative(string protocol)
		{
			return ProtocolNames.RelativeProtocols.Contains(protocol, StringComparison.Ordinal);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001E5C5 File Offset: 0x0001C7C5
		public static bool IsOriginable(string protocol)
		{
			return ProtocolNames.OriginalableProtocols.Contains(protocol, StringComparison.Ordinal);
		}

		// Token: 0x040003A2 RID: 930
		public static readonly string Http = "http";

		// Token: 0x040003A3 RID: 931
		public static readonly string Https = "https";

		// Token: 0x040003A4 RID: 932
		public static readonly string Ftp = "ftp";

		// Token: 0x040003A5 RID: 933
		public static readonly string JavaScript = "javascript";

		// Token: 0x040003A6 RID: 934
		public static readonly string Data = "data";

		// Token: 0x040003A7 RID: 935
		public static readonly string Mailto = "mailto";

		// Token: 0x040003A8 RID: 936
		public static readonly string File = "file";

		// Token: 0x040003A9 RID: 937
		public static readonly string Ws = "ws";

		// Token: 0x040003AA RID: 938
		public static readonly string Wss = "wss";

		// Token: 0x040003AB RID: 939
		public static readonly string Telnet = "telnet";

		// Token: 0x040003AC RID: 940
		public static readonly string Ssh = "ssh";

		// Token: 0x040003AD RID: 941
		public static readonly string Gopher = "gopher";

		// Token: 0x040003AE RID: 942
		public static readonly string Blob = "blob";

		// Token: 0x040003AF RID: 943
		private static readonly string[] RelativeProtocols = new string[]
		{
			ProtocolNames.Http,
			ProtocolNames.Https,
			ProtocolNames.Ftp,
			ProtocolNames.File,
			ProtocolNames.Ws,
			ProtocolNames.Wss,
			ProtocolNames.Gopher
		};

		// Token: 0x040003B0 RID: 944
		private static readonly string[] OriginalableProtocols = new string[]
		{
			ProtocolNames.Http,
			ProtocolNames.Https,
			ProtocolNames.Ftp,
			ProtocolNames.Ws,
			ProtocolNames.Wss,
			ProtocolNames.Gopher
		};
	}
}
