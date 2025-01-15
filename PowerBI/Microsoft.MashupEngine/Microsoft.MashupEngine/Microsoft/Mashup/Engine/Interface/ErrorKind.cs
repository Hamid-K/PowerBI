using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000078 RID: 120
	public enum ErrorKind
	{
		// Token: 0x04000145 RID: 325
		SyntaxError,
		// Token: 0x04000146 RID: 326
		DuplicateParameter,
		// Token: 0x04000147 RID: 327
		DuplicateLocal,
		// Token: 0x04000148 RID: 328
		DuplicateField,
		// Token: 0x04000149 RID: 329
		DuplicateExport,
		// Token: 0x0400014A RID: 330
		DuplicateMember,
		// Token: 0x0400014B RID: 331
		DuplicateSection,
		// Token: 0x0400014C RID: 332
		UnknownIdentifier,
		// Token: 0x0400014D RID: 333
		UnknownSection,
		// Token: 0x0400014E RID: 334
		Generic
	}
}
