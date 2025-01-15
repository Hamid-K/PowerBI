using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016CE RID: 5838
	public class NumberSourceList : InputValueList<NumberSource, decimal>
	{
		// Token: 0x0600C2E7 RID: 49895 RVA: 0x002A00AE File Offset: 0x0029E2AE
		public NumberSourceList()
		{
		}

		// Token: 0x0600C2E8 RID: 49896 RVA: 0x002A00B6 File Offset: 0x0029E2B6
		public NumberSourceList(IEnumerable<NumberSource> details)
			: base(details)
		{
		}
	}
}
