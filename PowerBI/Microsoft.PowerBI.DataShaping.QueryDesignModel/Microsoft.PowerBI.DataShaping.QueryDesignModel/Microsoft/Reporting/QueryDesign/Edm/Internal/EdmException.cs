using System;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200023B RID: 571
	public class EdmException : Exception
	{
		// Token: 0x0600193C RID: 6460 RVA: 0x00044D82 File Offset: 0x00042F82
		internal EdmException(string message)
			: base(message)
		{
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00044D8B File Offset: 0x00042F8B
		internal EdmException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
