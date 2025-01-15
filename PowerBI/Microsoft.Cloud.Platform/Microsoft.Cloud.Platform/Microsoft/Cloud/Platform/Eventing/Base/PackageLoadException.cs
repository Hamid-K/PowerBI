using System;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003D2 RID: 978
	[Serializable]
	public class PackageLoadException : MonitoredException
	{
		// Token: 0x06001E38 RID: 7736 RVA: 0x0000EB75 File Offset: 0x0000CD75
		public PackageLoadException()
		{
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x0000EB7D File Offset: 0x0000CD7D
		public PackageLoadException(string message)
			: base(message)
		{
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0000EB86 File Offset: 0x0000CD86
		public PackageLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0000EB9E File Offset: 0x0000CD9E
		protected PackageLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
