using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.AdoDotNet
{
	// Token: 0x02000F3E RID: 3902
	internal class AdoDotNetConnectionStringHandler : ConnectionStringHandler
	{
		// Token: 0x06006730 RID: 26416 RVA: 0x001632F0 File Offset: 0x001614F0
		public AdoDotNetConnectionStringHandler()
			: base(false, true, "User ID", "Password", "Trusted_Connection", new string[] { "Integrated Security" }, new string[0], new string[] { "Persist Security Info" })
		{
		}

		// Token: 0x17001DDE RID: 7646
		// (get) Token: 0x06006731 RID: 26417 RVA: 0x00087012 File Offset: 0x00085212
		protected override IEnumerable<string> HostNameKeys
		{
			get
			{
				return new string[] { "Hostname", "Host", "Server", "Data Source", "ServerName", "ServerNode" };
			}
		}
	}
}
