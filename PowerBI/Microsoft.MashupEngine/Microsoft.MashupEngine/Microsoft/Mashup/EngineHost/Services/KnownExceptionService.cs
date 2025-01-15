using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019F6 RID: 6646
	public class KnownExceptionService : IKnownExceptionService
	{
		// Token: 0x0600A80F RID: 43023 RVA: 0x000020FD File Offset: 0x000002FD
		private KnownExceptionService()
		{
		}

		// Token: 0x0600A810 RID: 43024 RVA: 0x0022BC9B File Offset: 0x00229E9B
		public bool IsKnownException(Exception e)
		{
			return e.IsEvaluationException();
		}

		// Token: 0x0400577E RID: 22398
		public static readonly KnownExceptionService Instance = new KnownExceptionService();
	}
}
