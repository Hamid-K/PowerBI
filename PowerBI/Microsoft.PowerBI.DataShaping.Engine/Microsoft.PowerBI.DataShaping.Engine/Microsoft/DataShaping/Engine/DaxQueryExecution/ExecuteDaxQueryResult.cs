using System;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.Engine.DaxQueryExecution
{
	// Token: 0x02000026 RID: 38
	public sealed class ExecuteDaxQueryResult
	{
		// Token: 0x060000EC RID: 236 RVA: 0x000038A8 File Offset: 0x00001AA8
		private ExecuteDaxQueryResult(DataShapeEngineErrorInfo errorInfo)
		{
			this.ErrorInfo = errorInfo;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000038B7 File Offset: 0x00001AB7
		public static ExecuteDaxQueryResult ForSuccess()
		{
			return new ExecuteDaxQueryResult(null);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000038BF File Offset: 0x00001ABF
		public static ExecuteDaxQueryResult ForError(DataShapeEngineErrorInfo errorInfo)
		{
			return new ExecuteDaxQueryResult(errorInfo);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000038C7 File Offset: 0x00001AC7
		public bool Succeeded
		{
			get
			{
				return this.ErrorInfo == null;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000038D2 File Offset: 0x00001AD2
		public DataShapeEngineErrorInfo ErrorInfo { get; }
	}
}
