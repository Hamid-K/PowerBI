using System;
using System.Fabric;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000087 RID: 135
	internal class ServiceFactoryInterfaceToDelegate
	{
		// Token: 0x06000501 RID: 1281 RVA: 0x00010B9F File Offset: 0x0000ED9F
		public ServiceFactoryInterfaceToDelegate(IStatefulServiceFactory statefulServiceFactory)
		{
			this.statefulServiceFactory = statefulServiceFactory;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00010BAE File Offset: 0x0000EDAE
		public IStatefulServiceReplica StatefulServiceFactory(string serviceType, Uri serviceName, byte[] initializationData, Guid partitionId, long replicaId)
		{
			return this.statefulServiceFactory.CreateReplica(serviceType, serviceName, initializationData, partitionId, replicaId);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00010BC2 File Offset: 0x0000EDC2
		public ServiceFactoryInterfaceToDelegate(IStatelessServiceFactory statelessServiceFactory)
		{
			this.statelessServiceFactory = statelessServiceFactory;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00010BD1 File Offset: 0x0000EDD1
		public IStatelessServiceInstance StatelessServiceFactory(string serviceType, Uri serviceName, byte[] initializationData, Guid partitionId, long replicaId)
		{
			return this.statelessServiceFactory.CreateInstance(serviceType, serviceName, initializationData, partitionId, replicaId);
		}

		// Token: 0x040001FF RID: 511
		private IStatefulServiceFactory statefulServiceFactory;

		// Token: 0x04000200 RID: 512
		private IStatelessServiceFactory statelessServiceFactory;
	}
}
