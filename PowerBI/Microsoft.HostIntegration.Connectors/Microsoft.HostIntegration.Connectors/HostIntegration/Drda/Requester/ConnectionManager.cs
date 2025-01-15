using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.DDM;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200091E RID: 2334
	internal abstract class ConnectionManager : Manager
	{
		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06004998 RID: 18840
		public abstract Stream Stream { get; }

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06004999 RID: 18841
		public abstract DdmWriter DdmWriter { get; }

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x0600499A RID: 18842
		public abstract DdmReader DdmReader { get; }

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x0600499B RID: 18843
		public abstract bool Connected { get; }

		// Token: 0x0600499C RID: 18844 RVA: 0x001128DA File Offset: 0x00110ADA
		public ConnectionManager(Requester requester)
			: base(requester)
		{
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x001128E3 File Offset: 0x00110AE3
		public override void Initialize()
		{
			base.Initialize();
			this._loadBalancing = Utility.ParseBoolean(this._requester.ConnectionInfo[43]);
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x00112904 File Offset: 0x00110B04
		public static ConnectionManager GetInstance(Requester requester)
		{
			return new TcpConnectionManager(requester);
		}

		// Token: 0x0600499F RID: 18847
		public abstract void Disconnect();

		// Token: 0x060049A0 RID: 18848
		public abstract Task ConnectAsync(X509Certificate clientCert, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060049A1 RID: 18849
		public abstract bool ProcessDdmObject(AbstractDdmObject ddmObject);

		// Token: 0x04003720 RID: 14112
		protected bool _loadBalancing;
	}
}
