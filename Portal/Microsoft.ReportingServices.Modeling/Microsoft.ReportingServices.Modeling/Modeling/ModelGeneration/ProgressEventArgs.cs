using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000D6 RID: 214
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class ProgressEventArgs : EventArgs
	{
		// Token: 0x06000BA6 RID: 2982 RVA: 0x00026915 File Offset: 0x00024B15
		public ProgressEventArgs(string line1, string line2, int progress, int total)
		{
			this.m_line1 = line1;
			this.m_line2 = line2;
			this.m_progress = progress;
			this.m_total = total;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0002693A File Offset: 0x00024B3A
		public string Line1
		{
			get
			{
				return this.m_line1;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00026942 File Offset: 0x00024B42
		public string Line2
		{
			get
			{
				return this.m_line2;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0002694A File Offset: 0x00024B4A
		public int Progress
		{
			get
			{
				return this.m_progress;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00026952 File Offset: 0x00024B52
		public int Total
		{
			get
			{
				return this.m_total;
			}
		}

		// Token: 0x040004C9 RID: 1225
		private string m_line1;

		// Token: 0x040004CA RID: 1226
		private string m_line2;

		// Token: 0x040004CB RID: 1227
		private int m_progress;

		// Token: 0x040004CC RID: 1228
		private int m_total;
	}
}
