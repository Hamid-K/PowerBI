using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public class MashupHostingException : MashupException
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000AE7E File Offset: 0x0000907E
		public MashupHostingException(string message, string reason)
			: base(message)
		{
			this.Reason = reason;
			this.Data[MashupHostingException.ReasonKey] = reason;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000AE9F File Offset: 0x0000909F
		protected MashupHostingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Reason = info.GetString("Reason");
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000AEBA File Offset: 0x000090BA
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000AEC2 File Offset: 0x000090C2
		public string Reason { get; private set; }

		// Token: 0x0600028E RID: 654 RVA: 0x0000AECB File Offset: 0x000090CB
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Reason", this.Reason);
			base.GetObjectData(info, context);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000AEE6 File Offset: 0x000090E6
		internal static string HostingDataKey(string key)
		{
			return MashupException.DataKey(new string[] { "HostingError", key });
		}

		// Token: 0x04000159 RID: 345
		internal const string HostingErrorPart = "HostingError";

		// Token: 0x0400015A RID: 346
		internal const string ReasonName = "Reason";

		// Token: 0x0400015B RID: 347
		internal static string ReasonKey = MashupHostingException.HostingDataKey("Reason");
	}
}
