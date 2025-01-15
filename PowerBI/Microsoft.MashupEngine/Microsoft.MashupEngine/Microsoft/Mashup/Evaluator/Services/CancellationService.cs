using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D7F RID: 7551
	public class CancellationService : TrackingService<ICancellable>, ICancellationService, ITrackingService<ICancellable>
	{
		// Token: 0x0600BBA7 RID: 48039 RVA: 0x0025F9C6 File Offset: 0x0025DBC6
		protected override void RegisterAfterClose(ICancellable item)
		{
			item.Cancel();
		}

		// Token: 0x0600BBA8 RID: 48040 RVA: 0x0025F9D0 File Offset: 0x0025DBD0
		public int CancelAll()
		{
			IEnumerable<ICancellable> enumerable = base.RemoveAllThenClose();
			int num = 0;
			foreach (ICancellable cancellable in enumerable)
			{
				try
				{
					if (cancellable.Cancel())
					{
						num++;
					}
				}
				catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
				{
				}
			}
			return num;
		}
	}
}
