using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000D5 RID: 213
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class ModelGenLogEventArgs : LogEventArgs
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x000268DA File Offset: 0x00024ADA
		public ModelGenLogEventArgs(LogEntryType entryType, string source, string dsvItemName, string message)
			: base(entryType, source, message)
		{
			this.m_dsvItemName = dsvItemName;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000268ED File Offset: 0x00024AED
		public ModelGenLogEventArgs(LogEventArgs e)
			: base(e.EntryType, e.Source, e.Message, e.Indent)
		{
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0002690D File Offset: 0x00024B0D
		public string DsvItemName
		{
			get
			{
				return this.m_dsvItemName;
			}
		}

		// Token: 0x040004C8 RID: 1224
		private readonly string m_dsvItemName;
	}
}
