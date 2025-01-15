using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.OleDb
{
	// Token: 0x0200056F RID: 1391
	internal class OleDbConnectionStringHandler : ConnectionStringHandler
	{
		// Token: 0x06002C4D RID: 11341 RVA: 0x00086F2C File Offset: 0x0008512C
		public OleDbConnectionStringHandler()
			: base(false, true, "User ID", "Password", "Integrated Security", new string[] { "Cache Authentication", "Encrypt Password", "Mask Password", "Protection Level", "SSPI", "Use Encryption for Data", "Impersonation Level" }, new string[] { "Provider", "Initial Catalog", "Data Source", "Locale Identifier", "Location", "Auto Translate", "Network Address" }, new string[] { "Asynchronous Processing", "Window Handle", "Lock Owner", "Bind Flags", "OLE DB Services", "Persist Security Info", "Persist Encrypted", "Prompt" })
		{
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x00087012 File Offset: 0x00085212
		protected override IEnumerable<string> HostNameKeys
		{
			get
			{
				return new string[] { "Hostname", "Host", "Server", "Data Source", "ServerName", "ServerNode" };
			}
		}
	}
}
