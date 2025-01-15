using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A0E RID: 6670
	internal sealed class LifetimeService : TrackingService<IDisposable>, ILifetimeService, ITrackingService<IDisposable>, IDisposable
	{
		// Token: 0x0600A8E3 RID: 43235 RVA: 0x0000EE09 File Offset: 0x0000D009
		protected override void RegisterAfterClose(IDisposable item)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600A8E4 RID: 43236 RVA: 0x0022F46C File Offset: 0x0022D66C
		public void Dispose()
		{
			foreach (IDisposable disposable in base.RemoveAllThenClose())
			{
				disposable.Dispose();
			}
		}
	}
}
