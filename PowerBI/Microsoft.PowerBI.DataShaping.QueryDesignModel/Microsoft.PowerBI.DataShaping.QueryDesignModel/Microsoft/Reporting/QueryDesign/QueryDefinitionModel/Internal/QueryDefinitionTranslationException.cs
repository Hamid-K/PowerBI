using System;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000F8 RID: 248
	public sealed class QueryDefinitionTranslationException : Exception
	{
		// Token: 0x06000E6A RID: 3690 RVA: 0x0002617E File Offset: 0x0002437E
		internal QueryDefinitionTranslationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00026187 File Offset: 0x00024387
		internal QueryDefinitionTranslationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
