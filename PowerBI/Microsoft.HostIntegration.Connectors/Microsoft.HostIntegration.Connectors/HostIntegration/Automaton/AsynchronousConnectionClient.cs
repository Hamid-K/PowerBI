using System;

namespace Microsoft.HostIntegration.Automaton
{
	// Token: 0x020004BF RID: 1215
	public abstract class AsynchronousConnectionClient
	{
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002975 RID: 10613
		public abstract string Name { get; }

		// Token: 0x06002976 RID: 10614
		public abstract void MessageReceived(ConnectionLocation connectionLocation);
	}
}
