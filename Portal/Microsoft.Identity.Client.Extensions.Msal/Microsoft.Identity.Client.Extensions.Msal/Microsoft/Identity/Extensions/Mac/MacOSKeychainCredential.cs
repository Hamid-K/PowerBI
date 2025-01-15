using System;
using System.Diagnostics;

namespace Microsoft.Identity.Extensions.Mac
{
	// Token: 0x02000008 RID: 8
	[DebuggerDisplay("{DebuggerDisplay}")]
	internal class MacOSKeychainCredential
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000212E File Offset: 0x0000032E
		internal MacOSKeychainCredential(string service, string account, byte[] password, string label)
		{
			this.Service = service;
			this.Account = account;
			this.Password = password;
			this.Label = label;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002153 File Offset: 0x00000353
		public string Service { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000215B File Offset: 0x0000035B
		public string Account { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002163 File Offset: 0x00000363
		public string Label { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000216B File Offset: 0x0000036B
		public byte[] Password { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002173 File Offset: 0x00000373
		private string DebuggerDisplay
		{
			get
			{
				return string.Concat(new string[] { this.Label, " [Service: ", this.Service, ", Account: ", this.Account, "]" });
			}
		}
	}
}
