using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200041E RID: 1054
	[Serializable]
	internal class MessageSignatureException : CommunicationException
	{
		// Token: 0x060024C1 RID: 9409 RVA: 0x000708CB File Offset: 0x0006EACB
		public MessageSignatureException()
			: base("Bad message signature")
		{
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000708D8 File Offset: 0x0006EAD8
		public MessageSignatureException(string message)
			: base(message)
		{
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000708E1 File Offset: 0x0006EAE1
		public MessageSignatureException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000708EB File Offset: 0x0006EAEB
		protected MessageSignatureException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
