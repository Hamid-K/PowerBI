using System;
using System.Runtime.Serialization;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public class XEventTooMayObjectsExcpeption : Exception
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00003C74 File Offset: 0x00003C74
		protected XEventTooMayObjectsExcpeption(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00003C20 File Offset: 0x00003C20
		public XEventTooMayObjectsExcpeption(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00003C04 File Offset: 0x00003C04
		public XEventTooMayObjectsExcpeption(string message)
			: base(message)
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00003BE8 File Offset: 0x00003BE8
		public XEventTooMayObjectsExcpeption()
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00003F24 File Offset: 0x00003F24
		public override string ToString()
		{
			return string.Format(ResourcesMgr.GetString("TooManyObjectsException"), 524287U);
		}
	}
}
