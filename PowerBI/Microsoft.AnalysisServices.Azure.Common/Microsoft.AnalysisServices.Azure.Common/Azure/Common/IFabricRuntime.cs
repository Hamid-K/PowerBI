using System;
using System.Fabric;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000082 RID: 130
	public interface IFabricRuntime
	{
		// Token: 0x060004ED RID: 1261
		string GetCodePath(string serviceType);

		// Token: 0x060004EE RID: 1262
		void RegisterStatefulServiceFactory(string serviceTypeName, IStatefulServiceFactory statefulServiceFactory);

		// Token: 0x060004EF RID: 1263
		void RegisterStatelessServiceFactory(string serviceTypeName, IStatelessServiceFactory statelessServiceFactory);
	}
}
