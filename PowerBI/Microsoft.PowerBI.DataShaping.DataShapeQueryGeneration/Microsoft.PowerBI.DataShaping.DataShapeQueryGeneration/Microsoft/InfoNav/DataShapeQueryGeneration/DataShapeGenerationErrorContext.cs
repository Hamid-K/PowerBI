using System;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	internal sealed class DataShapeGenerationErrorContext : EngineErrorContextBase<DataShapeGenerationMessage>
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000A868 File Offset: 0x00008A68
		internal DataShapeGenerationErrorContext(ITracer contextTracer)
		{
			this.contextTracer = contextTracer;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A877 File Offset: 0x00008A77
		public void Register(DataShapeGenerationMessage message)
		{
			this.contextTracer.SanitizedTrace(message);
			base.Add(message);
		}

		// Token: 0x040001F5 RID: 501
		[NonSerialized]
		private ITracer contextTracer;
	}
}
