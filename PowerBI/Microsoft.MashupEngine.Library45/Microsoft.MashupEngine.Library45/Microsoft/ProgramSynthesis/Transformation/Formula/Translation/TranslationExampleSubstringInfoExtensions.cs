using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x02001810 RID: 6160
	internal static class TranslationExampleSubstringInfoExtensions
	{
		// Token: 0x0600CA78 RID: 51832 RVA: 0x002B40D8 File Offset: 0x002B22D8
		public static string Render(this IEnumerable<ExampleSubstringInfo> infos)
		{
			return infos.Select((ExampleSubstringInfo info) => info.ToString().Replace("\n", "\\n")).RenderNumbered(1);
		}
	}
}
