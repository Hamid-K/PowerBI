using System;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	internal sealed class RDLUpgradeException : XmlException
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x000170C7 File Offset: 0x000152C7
		internal RDLUpgradeException(string msg)
			: base(msg)
		{
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000170D0 File Offset: 0x000152D0
		internal RDLUpgradeException(string msg, Exception inner)
			: base(msg, inner)
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000170DA File Offset: 0x000152DA
		private RDLUpgradeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
