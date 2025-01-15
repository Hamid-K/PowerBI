using System;
using System.Collections.ObjectModel;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000199 RID: 409
	public class EventBatchEventArgs<T> : EventArgs
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00024098 File Offset: 0x00022298
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x000240A0 File Offset: 0x000222A0
		public ReadOnlyCollection<T> Data { get; private set; }

		// Token: 0x06000A7F RID: 2687 RVA: 0x000240A9 File Offset: 0x000222A9
		public EventBatchEventArgs([NotNull] ReadOnlyCollection<T> data)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ReadOnlyCollection<T>>(data, "data");
			this.Data = data;
		}
	}
}
