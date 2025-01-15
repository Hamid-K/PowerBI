using System;
using System.Runtime.Serialization;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000052 RID: 82
	[Serializable]
	public class XEventTooManyKeywordsException : Exception
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00003C74 File Offset: 0x00003C74
		protected XEventTooManyKeywordsException(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00003C20 File Offset: 0x00003C20
		public XEventTooManyKeywordsException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00003C04 File Offset: 0x00003C04
		public XEventTooManyKeywordsException(string message)
			: base(message)
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00003BE8 File Offset: 0x00003BE8
		public XEventTooManyKeywordsException()
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00003F04 File Offset: 0x00003F04
		public override string ToString()
		{
			return ResourcesMgr.GetString("PackageRegistrationFailureExceptionExtended");
		}
	}
}
