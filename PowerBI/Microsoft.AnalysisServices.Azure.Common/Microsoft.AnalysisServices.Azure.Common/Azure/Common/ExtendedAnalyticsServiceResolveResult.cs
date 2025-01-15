using System;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Azure.WindowsFabric;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000059 RID: 89
	[CLSCompliant(true)]
	[DataContract]
	public sealed class ExtendedAnalyticsServiceResolveResult
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x0001002E File Offset: 0x0000E22E
		public ExtendedAnalyticsServiceResolveResult(WindowsFabricEndpoint endpoint, string localDatabaseId)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(localDatabaseId, "localDatabaseId");
			ExtendedDiagnostics.EnsureNotNull<WindowsFabricEndpoint>(endpoint, "endpoint");
			this.LocalDatabaseId = localDatabaseId;
			this.Endpoint = endpoint;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001005A File Offset: 0x0000E25A
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x00010062 File Offset: 0x0000E262
		[DataMember]
		public string LocalDatabaseId { get; private set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0001006B File Offset: 0x0000E26B
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00010073 File Offset: 0x0000E273
		[DataMember]
		public WindowsFabricEndpoint Endpoint { get; private set; }
	}
}
