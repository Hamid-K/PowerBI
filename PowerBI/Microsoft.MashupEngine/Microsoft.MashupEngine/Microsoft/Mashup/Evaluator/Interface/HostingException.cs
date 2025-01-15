using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DE4 RID: 7652
	[Serializable]
	public class HostingException : Exception
	{
		// Token: 0x0600BD93 RID: 48531 RVA: 0x00266CD1 File Offset: 0x00264ED1
		public HostingException(string message, string reason)
			: base(message)
		{
			this.reason = reason;
		}

		// Token: 0x0600BD94 RID: 48532 RVA: 0x00266CE1 File Offset: 0x00264EE1
		protected HostingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.reason = info.GetString("Reason");
		}

		// Token: 0x17002EA4 RID: 11940
		// (get) Token: 0x0600BD95 RID: 48533 RVA: 0x00266CFC File Offset: 0x00264EFC
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0600BD96 RID: 48534 RVA: 0x00266D04 File Offset: 0x00264F04
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Reason", this.reason);
			base.GetObjectData(info, context);
		}

		// Token: 0x040060B2 RID: 24754
		private const string ReasonName = "Reason";

		// Token: 0x040060B3 RID: 24755
		private readonly string reason;
	}
}
