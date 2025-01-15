using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016D0 RID: 5840
	public class DateTimeSourceList : InputValueList<DateTimeSource, DateTime>
	{
		// Token: 0x0600C2EB RID: 49899 RVA: 0x002A00D8 File Offset: 0x0029E2D8
		public DateTimeSourceList()
		{
		}

		// Token: 0x0600C2EC RID: 49900 RVA: 0x002A00E0 File Offset: 0x0029E2E0
		public DateTimeSourceList(IEnumerable<DateTimeSource> details)
			: base(details)
		{
		}
	}
}
