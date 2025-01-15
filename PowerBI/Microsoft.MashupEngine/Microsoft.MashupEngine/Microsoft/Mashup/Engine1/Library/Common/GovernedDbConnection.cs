using System;
using System.Data.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010CD RID: 4301
	internal class GovernedDbConnection : DelegatingDbConnection
	{
		// Token: 0x060070BE RID: 28862 RVA: 0x001837F6 File Offset: 0x001819F6
		public GovernedDbConnection(IEngineHost host, IResource resource, DbConnection conn)
			: base(conn)
		{
			this.host = host;
			this.resource = resource;
		}

		// Token: 0x060070BF RID: 28863 RVA: 0x0018380D File Offset: 0x00181A0D
		public override void Open()
		{
			if (this.governedHandle == null)
			{
				this.governedHandle = HostResourcePermissionService.WaitForGovernedHandle(this.host, this.resource);
			}
			base.Open();
		}

		// Token: 0x060070C0 RID: 28864 RVA: 0x00183834 File Offset: 0x00181A34
		public override void Close()
		{
			base.Close();
			if (this.governedHandle != null)
			{
				this.governedHandle.Dispose();
				this.governedHandle = null;
			}
		}

		// Token: 0x060070C1 RID: 28865 RVA: 0x00183856 File Offset: 0x00181A56
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.governedHandle != null)
			{
				this.governedHandle.Dispose();
				this.governedHandle = null;
			}
		}

		// Token: 0x04003E2A RID: 15914
		private readonly IEngineHost host;

		// Token: 0x04003E2B RID: 15915
		private readonly IResource resource;

		// Token: 0x04003E2C RID: 15916
		private IDisposable governedHandle;
	}
}
