using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.StaticSqlUtil;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000900 RID: 2304
	public interface IRequester
	{
		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x060048A7 RID: 18599
		HostType HostType { get; }

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x060048A8 RID: 18600
		string ServerClass { get; }

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x060048A9 RID: 18601
		string ServerVersion { get; }

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x060048AA RID: 18602
		DrdaFlavor Flavor { get; }

		// Token: 0x17001196 RID: 4502
		object this[RequesterProperties attribute] { get; set; }

		// Token: 0x060048AD RID: 18605
		void SetCustomPackageData(XmlReader packageData);

		// Token: 0x060048AE RID: 18606
		ISqlStatement CreateStatement();

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x060048AF RID: 18607
		// (set) Token: 0x060048B0 RID: 18608
		bool UseHIS2013Constants { get; set; }

		// Token: 0x060048B1 RID: 18609
		void SetProviderName(string providerName);

		// Token: 0x060048B2 RID: 18610
		Task ConnectAsync(X509Certificate clientCert, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048B3 RID: 18611
		Task Disconnect(bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048B4 RID: 18612
		Task EnlistAsync(Transaction transaction, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048B5 RID: 18613
		Task EnlistAsync(Transaction transaction, int timeOut, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048B6 RID: 18614
		Task CommitAsync(bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048B7 RID: 18615
		Task RollbackAsync(bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048B8 RID: 18616
		Task InterruptAsync(bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048B9 RID: 18617
		Task BindPackageAsync(Package package, Options options, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048BA RID: 18618
		Task DropPackageAsync(Package package, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048BB RID: 18619
		Task CopyPackageAsync(Package package, Options options, string targetRdbName, string targetCollectionId, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048BC RID: 18620
		Task RebindPackageAsync(Package package, Options options, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken);

		// Token: 0x060048BD RID: 18621
		Task SetupHostPackagesAsync(bool releaseCommit, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken);
	}
}
