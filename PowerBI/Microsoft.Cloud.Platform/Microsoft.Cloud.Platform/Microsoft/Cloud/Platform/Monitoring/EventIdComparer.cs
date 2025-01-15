using System;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200008A RID: 138
	internal class EventIdComparer<T> : ComparerBase<T, EventIdentifier> where T : class, IEventIdentifier
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0000E881 File Offset: 0x0000CA81
		protected override EventIdentifier GetProperty(T obj)
		{
			return obj.EventId;
		}
	}
}
