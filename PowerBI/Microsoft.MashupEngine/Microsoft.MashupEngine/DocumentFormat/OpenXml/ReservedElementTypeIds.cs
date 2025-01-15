using System;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200214D RID: 8525
	internal static class ReservedElementTypeIds
	{
		// Token: 0x0600D3DB RID: 54235 RVA: 0x002A1AB6 File Offset: 0x0029FCB6
		internal static bool IsStrongTypedElement(this OpenXmlElement element)
		{
			return element.ElementTypeId > 10000;
		}

		// Token: 0x040069AC RID: 27052
		internal const int OpenXmlElementId = 9000;

		// Token: 0x040069AD RID: 27053
		internal const int OpenXmlMiscNodeId = 9001;

		// Token: 0x040069AE RID: 27054
		internal const int OpenXmlUnknownElementId = 9002;

		// Token: 0x040069AF RID: 27055
		internal const int AlternateContentId = 9003;

		// Token: 0x040069B0 RID: 27056
		internal const int AlternateContentChoiceId = 9004;

		// Token: 0x040069B1 RID: 27057
		internal const int AlternateContentFallbackId = 9005;

		// Token: 0x040069B2 RID: 27058
		internal const int MaxReservedId = 10000;
	}
}
