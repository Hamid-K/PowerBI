using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Detection.Translation.Python
{
	// Token: 0x02000B0C RID: 2828
	internal static class ColumnInfoExtensions
	{
		// Token: 0x060046A5 RID: 18085 RVA: 0x000DCADD File Offset: 0x000DACDD
		public static string ParseFunctionName(this IPythonColumnInfo columnInfo)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("_parse_value_from_{0}", new object[] { columnInfo.ColumnNameForIdentifier }));
		}
	}
}
