using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000D4 RID: 212
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class LogEventArgs : EventArgs
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x00026889 File Offset: 0x00024A89
		public LogEventArgs(LogEntryType entryType, string source, string message)
			: this(entryType, source, message, 0)
		{
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00026895 File Offset: 0x00024A95
		public LogEventArgs(LogEntryType entryType, string source, string message, int indent)
		{
			this.m_entryType = entryType;
			this.m_source = source;
			this.m_message = message;
			this.m_indent = indent;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x000268BA File Offset: 0x00024ABA
		public LogEntryType EntryType
		{
			get
			{
				return this.m_entryType;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x000268C2 File Offset: 0x00024AC2
		public string Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x000268CA File Offset: 0x00024ACA
		public string Message
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x000268D2 File Offset: 0x00024AD2
		public int Indent
		{
			get
			{
				return this.m_indent;
			}
		}

		// Token: 0x040004C4 RID: 1220
		private LogEntryType m_entryType;

		// Token: 0x040004C5 RID: 1221
		private string m_source;

		// Token: 0x040004C6 RID: 1222
		private string m_message;

		// Token: 0x040004C7 RID: 1223
		private int m_indent;
	}
}
