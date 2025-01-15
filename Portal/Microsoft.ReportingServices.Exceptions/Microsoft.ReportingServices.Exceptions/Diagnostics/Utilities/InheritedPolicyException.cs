using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200006D RID: 109
	[Serializable]
	internal sealed class InheritedPolicyException : ReportCatalogException
	{
		// Token: 0x0600020F RID: 527 RVA: 0x00004988 File Offset: 0x00002B88
		public InheritedPolicyException(string itemPath)
			: base(ErrorCode.rsInheritedPolicy, ErrorStringsWrapper.rsInheritedPolicy(itemPath), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000499F File Offset: 0x00002B9F
		public InheritedPolicyException(string itemPath, string itemID)
			: base(ErrorCode.rsInheritedPolicy, ErrorStringsWrapper.rsInheritedPolicyModelItem(itemPath, itemID), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000049B7 File Offset: 0x00002BB7
		private InheritedPolicyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
