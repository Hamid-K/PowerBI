using System;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003C7 RID: 967
	public interface IOnBehalfOfEventService
	{
		// Token: 0x06001DEE RID: 7662
		void FireOnBehalfOfEvent(string source, EtwEvent etwEvent);
	}
}
