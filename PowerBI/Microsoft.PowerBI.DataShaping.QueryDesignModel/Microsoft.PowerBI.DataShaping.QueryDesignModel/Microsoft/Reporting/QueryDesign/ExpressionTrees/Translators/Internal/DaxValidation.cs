using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200014F RID: 335
	internal static class DaxValidation
	{
		// Token: 0x0600128B RID: 4747 RVA: 0x00035C20 File Offset: 0x00033E20
		internal static void CheckCondition(bool condition, string errorMessage)
		{
			if (!condition)
			{
				DaxValidation.Fail(errorMessage);
			}
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00035C2B File Offset: 0x00033E2B
		internal static void Fail(string errorMessage)
		{
			throw new DaxTranslationException(errorMessage);
		}
	}
}
