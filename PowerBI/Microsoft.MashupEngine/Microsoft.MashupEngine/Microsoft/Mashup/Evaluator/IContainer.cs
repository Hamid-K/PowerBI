using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CCF RID: 7375
	internal interface IContainer : IDisposable
	{
		// Token: 0x17002DAC RID: 11692
		// (get) Token: 0x0600B7BD RID: 47037
		int ContainerID { get; }

		// Token: 0x17002DAD RID: 11693
		// (get) Token: 0x0600B7BE RID: 47038
		bool IsHealthy { get; }

		// Token: 0x17002DAE RID: 11694
		// (get) Token: 0x0600B7BF RID: 47039
		IFeatureLoggingService Features { get; }

		// Token: 0x17002DAF RID: 11695
		// (get) Token: 0x0600B7C0 RID: 47040
		IMessenger Messenger { get; }

		// Token: 0x0600B7C1 RID: 47041
		bool TryGetAs<T>(out T result) where T : class;

		// Token: 0x0600B7C2 RID: 47042
		void Kill();
	}
}
