using System;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x02000012 RID: 18
	public sealed class ExecuteSemanticQueryResult
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002E6D File Offset: 0x0000106D
		private ExecuteSemanticQueryResult(DataShapeEngineErrorInfo errorInfo)
		{
			this.ErrorInfo = errorInfo;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E7C File Offset: 0x0000107C
		public static ExecuteSemanticQueryResult ForSuccess()
		{
			return new ExecuteSemanticQueryResult(null);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E84 File Offset: 0x00001084
		public static ExecuteSemanticQueryResult ForError(DataShapeEngineErrorInfo errorInfo)
		{
			return new ExecuteSemanticQueryResult(errorInfo);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002E8C File Offset: 0x0000108C
		public bool Succeeded
		{
			get
			{
				return this.ErrorInfo == null;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002E97 File Offset: 0x00001097
		public DataShapeEngineErrorInfo ErrorInfo { get; }
	}
}
