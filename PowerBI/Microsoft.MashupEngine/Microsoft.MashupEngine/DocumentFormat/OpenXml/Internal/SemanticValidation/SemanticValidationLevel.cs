using System;

namespace DocumentFormat.OpenXml.Internal.SemanticValidation
{
	// Token: 0x020030DE RID: 12510
	[Flags]
	internal enum SemanticValidationLevel
	{
		// Token: 0x0400B411 RID: 46097
		PackageOnly = 1,
		// Token: 0x0400B412 RID: 46098
		PartOnly = 2,
		// Token: 0x0400B413 RID: 46099
		ElementOnly = 4,
		// Token: 0x0400B414 RID: 46100
		Package = 1,
		// Token: 0x0400B415 RID: 46101
		Part = 3,
		// Token: 0x0400B416 RID: 46102
		Element = 7
	}
}
