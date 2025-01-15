using System;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000102 RID: 258
	public class ModelOperationResult : IModelOperationResult
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x0007B343 File Offset: 0x00079543
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x0007B34B File Offset: 0x0007954B
		public ObjectImpact Impact { get; internal set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0007B354 File Offset: 0x00079554
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x0007B35C File Offset: 0x0007955C
		public XmlaResultCollection XmlaResults { get; internal set; }

		// Token: 0x06001112 RID: 4370 RVA: 0x0007B365 File Offset: 0x00079565
		internal static ModelOperationResult ConvertFrom(IModelOperationResult result)
		{
			ModelOperationResult modelOperationResult = result as ModelOperationResult;
			if (result == null)
			{
				throw new InvalidOperationException("result must be of concrete type ModelOperationResult");
			}
			return modelOperationResult;
		}
	}
}
