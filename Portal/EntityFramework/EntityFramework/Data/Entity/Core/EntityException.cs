using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002CF RID: 719
	[Serializable]
	public class EntityException : DataException
	{
		// Token: 0x060022B6 RID: 8886 RVA: 0x00062048 File Offset: 0x00060248
		public EntityException()
			: base(Strings.EntityClient_ProviderGeneralError)
		{
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x00062055 File Offset: 0x00060255
		public EntityException(string message)
			: base(message)
		{
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x0006205E File Offset: 0x0006025E
		public EntityException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x00062068 File Offset: 0x00060268
		protected EntityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
