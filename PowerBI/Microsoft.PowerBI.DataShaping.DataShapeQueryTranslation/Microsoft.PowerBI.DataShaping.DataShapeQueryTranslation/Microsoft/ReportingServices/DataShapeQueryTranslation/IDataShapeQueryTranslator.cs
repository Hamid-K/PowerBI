using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000058 RID: 88
	internal interface IDataShapeQueryTranslator
	{
		// Token: 0x060003FF RID: 1023
		DataShapeQueryTranslationResult Translate(DataShapeQueryTranslationContext translationContext);
	}
}
