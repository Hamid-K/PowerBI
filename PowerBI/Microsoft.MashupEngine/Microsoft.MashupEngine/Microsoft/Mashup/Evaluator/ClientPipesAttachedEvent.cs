using System;
using Microsoft.Mashup.Shims.Interprocess;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C4C RID: 7244
	public static class ClientPipesAttachedEvent
	{
		// Token: 0x0600B4C5 RID: 46277 RVA: 0x0024A6EA File Offset: 0x002488EA
		public static ManualResetWaitableEvent Create(string inputPipeName)
		{
			return ManualResetWaitableEvent.Create("Microsoft.Mashup.Evaluator.ContainerProcess." + inputPipeName, false);
		}
	}
}
