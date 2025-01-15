using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004AC RID: 1196
	public sealed class SapBwDestinationTracker : IDisposable
	{
		// Token: 0x0600275D RID: 10077 RVA: 0x00073AC3 File Offset: 0x00071CC3
		public SapBwDestinationTracker(Action<string> removeAction)
		{
			this.destinations = new HashSet<string>();
			this.removeAction = removeAction;
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x00073ADD File Offset: 0x00071CDD
		public void AddOrEditDestination(string destinationName)
		{
			if (!string.IsNullOrEmpty(destinationName))
			{
				this.destinations.Add(destinationName);
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x00073AF4 File Offset: 0x00071CF4
		public void Dispose()
		{
			if (this.destinations != null)
			{
				this.destinations.RemoveWhere(delegate(string d)
				{
					this.removeAction(d);
					return true;
				});
			}
		}

		// Token: 0x04001089 RID: 4233
		private readonly HashSet<string> destinations;

		// Token: 0x0400108A RID: 4234
		private readonly Action<string> removeAction;
	}
}
