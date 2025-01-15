using System;
using System.Runtime.Remoting.Messaging;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x0200013E RID: 318
	public class AsAzureActivityScope : IDisposable
	{
		// Token: 0x06001148 RID: 4424 RVA: 0x0004675C File Offset: 0x0004495C
		public AsAzureActivityScope(object asAzureClientActivityId)
		{
			this.m_existingClientActivityId = CallContext.LogicalGetData("AnalysisServices.ClientActivityId");
			CallContext.LogicalSetData("AnalysisServices.ClientActivityId", asAzureClientActivityId);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0004677F File Offset: 0x0004497F
		public void Dispose()
		{
			CallContext.LogicalSetData("AnalysisServices.ClientActivityId", this.m_existingClientActivityId);
		}

		// Token: 0x040003DC RID: 988
		private const string ASClientActivityId = "AnalysisServices.ClientActivityId";

		// Token: 0x040003DD RID: 989
		private object m_existingClientActivityId;
	}
}
