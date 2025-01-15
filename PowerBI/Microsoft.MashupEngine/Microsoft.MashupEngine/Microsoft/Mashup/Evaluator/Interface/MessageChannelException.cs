using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E11 RID: 7697
	[Serializable]
	public class MessageChannelException : Exception
	{
		// Token: 0x0600BDE3 RID: 48611 RVA: 0x00002FDF File Offset: 0x000011DF
		public MessageChannelException(string message)
			: base(message)
		{
		}

		// Token: 0x0600BDE4 RID: 48612 RVA: 0x00005F3B File Offset: 0x0000413B
		public MessageChannelException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600BDE5 RID: 48613 RVA: 0x00005F45 File Offset: 0x00004145
		protected MessageChannelException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600BDE6 RID: 48614 RVA: 0x00266EC1 File Offset: 0x002650C1
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
