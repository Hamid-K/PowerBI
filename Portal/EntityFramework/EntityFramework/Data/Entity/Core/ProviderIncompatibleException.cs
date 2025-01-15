using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002DF RID: 735
	[Serializable]
	public sealed class ProviderIncompatibleException : EntityException
	{
		// Token: 0x06002342 RID: 9026 RVA: 0x00063760 File Offset: 0x00061960
		public ProviderIncompatibleException()
		{
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x00063768 File Offset: 0x00061968
		public ProviderIncompatibleException(string message)
			: base(message)
		{
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x00063771 File Offset: 0x00061971
		public ProviderIncompatibleException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0006377B File Offset: 0x0006197B
		private ProviderIncompatibleException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
