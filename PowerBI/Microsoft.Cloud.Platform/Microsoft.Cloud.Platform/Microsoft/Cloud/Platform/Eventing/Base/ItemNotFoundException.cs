using System;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003BC RID: 956
	[Serializable]
	public abstract class ItemNotFoundException : MonitoredException
	{
		// Token: 0x06001D95 RID: 7573 RVA: 0x0000EB75 File Offset: 0x0000CD75
		protected ItemNotFoundException()
		{
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0000EB7D File Offset: 0x0000CD7D
		protected ItemNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0000EB86 File Offset: 0x0000CD86
		protected ItemNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0000EB9E File Offset: 0x0000CD9E
		protected ItemNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
