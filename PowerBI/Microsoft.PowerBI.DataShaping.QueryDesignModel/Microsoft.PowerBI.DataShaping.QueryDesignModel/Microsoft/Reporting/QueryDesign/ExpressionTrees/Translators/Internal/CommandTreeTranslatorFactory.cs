using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000122 RID: 290
	internal static class CommandTreeTranslatorFactory
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x0002C803 File Offset: 0x0002AA03
		internal static CommandTreeTranslator CreateDaxTranslator()
		{
			return new DaxTranslator();
		}
	}
}
