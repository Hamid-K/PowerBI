using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016D2 RID: 5842
	public class StringInputList : InputValueList<StringInput, string>
	{
		// Token: 0x0600C2EF RID: 49903 RVA: 0x002A0102 File Offset: 0x0029E302
		public StringInputList()
		{
		}

		// Token: 0x0600C2F0 RID: 49904 RVA: 0x002A010A File Offset: 0x0029E30A
		public StringInputList(IEnumerable<StringInput> details)
			: base(details)
		{
		}
	}
}
