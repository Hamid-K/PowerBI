using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Data
{
	// Token: 0x020003BC RID: 956
	[Serializable]
	internal class LookupNamespaceNotFoundException : LookupException
	{
		// Token: 0x060021B2 RID: 8626 RVA: 0x00067F95 File Offset: 0x00066195
		public LookupNamespaceNotFoundException()
			: base("Service Namespace not found")
		{
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x00067F78 File Offset: 0x00066178
		public LookupNamespaceNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x00067F81 File Offset: 0x00066181
		public LookupNamespaceNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x00067F8B File Offset: 0x0006618B
		protected LookupNamespaceNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
