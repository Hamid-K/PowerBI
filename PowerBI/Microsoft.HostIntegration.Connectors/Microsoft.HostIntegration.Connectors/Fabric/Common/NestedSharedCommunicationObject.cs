using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200041A RID: 1050
	internal sealed class NestedSharedCommunicationObject : SharedCommunicationObject
	{
		// Token: 0x060024A2 RID: 9378 RVA: 0x00070468 File Offset: 0x0006E668
		public NestedSharedCommunicationObject(ISharedCommunicationObjectCore outer, TimeSpan defaultOpenTimeout, TimeSpan defaultCloseTimeout)
		{
			if (outer == null)
			{
				throw new ArgumentNullException("outer");
			}
			this.m_outer = outer;
			this.m_defaultOpenTimeout = defaultOpenTimeout;
			this.m_defaultCloseTimeout = defaultCloseTimeout;
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x00070493 File Offset: 0x0006E693
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.m_defaultCloseTimeout;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x0007049B File Offset: 0x0006E69B
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return this.m_defaultOpenTimeout;
			}
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000704A3 File Offset: 0x0006E6A3
		protected override bool OnOpen()
		{
			return this.m_outer.OnOpen();
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000704B0 File Offset: 0x0006E6B0
		protected override bool OnClose()
		{
			return this.m_outer.OnClose();
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000704BD File Offset: 0x0006E6BD
		protected override void OnAbort()
		{
			this.m_outer.OnAbort();
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000704CA File Offset: 0x0006E6CA
		public override string ToString()
		{
			return this.m_outer.ToString();
		}

		// Token: 0x04001673 RID: 5747
		private ISharedCommunicationObjectCore m_outer;

		// Token: 0x04001674 RID: 5748
		private TimeSpan m_defaultOpenTimeout;

		// Token: 0x04001675 RID: 5749
		private TimeSpan m_defaultCloseTimeout;
	}
}
