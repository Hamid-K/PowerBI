using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001980 RID: 6528
	internal interface INotifyTaskCompletion
	{
		// Token: 0x0600A59E RID: 42398
		void Notify(ITask antecedent);
	}
}
