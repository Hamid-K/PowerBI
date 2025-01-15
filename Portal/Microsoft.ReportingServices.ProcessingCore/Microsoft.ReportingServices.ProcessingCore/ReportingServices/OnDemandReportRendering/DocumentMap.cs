using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002B6 RID: 694
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DocumentMap : IEnumerator<DocumentMapNode>, IDisposable, IEnumerator
	{
		// Token: 0x06001A87 RID: 6791
		public abstract void Close();

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0006AFF6 File Offset: 0x000691F6
		internal bool IsClosed
		{
			get
			{
				return this.m_isClosed;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06001A89 RID: 6793
		public abstract DocumentMapNode Current { get; }

		// Token: 0x06001A8A RID: 6794
		public abstract void Dispose();

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0006AFFE File Offset: 0x000691FE
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06001A8C RID: 6796
		public abstract bool MoveNext();

		// Token: 0x06001A8D RID: 6797
		public abstract void Reset();

		// Token: 0x04000D37 RID: 3383
		protected bool m_isClosed;
	}
}
