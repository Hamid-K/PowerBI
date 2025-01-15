using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Explanations
{
	// Token: 0x020019BF RID: 6591
	public interface IExplanationMeta
	{
		// Token: 0x170023C5 RID: 9157
		// (get) Token: 0x0600D738 RID: 55096
		string Key { get; }

		// Token: 0x170023C6 RID: 9158
		// (get) Token: 0x0600D739 RID: 55097
		IReadOnlyList<string> OrderedReplacements { get; }

		// Token: 0x170023C7 RID: 9159
		// (get) Token: 0x0600D73A RID: 55098
		IDictionary<string, string> Replacements { get; }

		// Token: 0x170023C8 RID: 9160
		// (get) Token: 0x0600D73B RID: 55099
		string Text { get; }
	}
}
