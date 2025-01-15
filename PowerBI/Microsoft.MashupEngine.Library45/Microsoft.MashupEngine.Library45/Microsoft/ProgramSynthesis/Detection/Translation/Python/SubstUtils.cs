using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Translation.Python;

namespace Microsoft.ProgramSynthesis.Detection.Translation.Python
{
	// Token: 0x02000B0D RID: 2829
	internal static class SubstUtils
	{
		// Token: 0x060046A6 RID: 18086 RVA: 0x000DCAFD File Offset: 0x000DACFD
		public static string Apply(this IEnumerable<KeyValuePair<string, string>> substitutions, string valueString)
		{
			return substitutions.Aggregate(valueString, (string acc, KeyValuePair<string, string> kvp) => FormattableString.Invariant(FormattableStringFactory.Create("{0}.replace({1}, {2})", new object[]
			{
				acc,
				kvp.Key.ToPythonLiteral(),
				kvp.Value.ToPythonLiteral()
			})));
		}
	}
}
