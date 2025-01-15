using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016D1 RID: 5841
	public class DateTimeSource : InputValueDetail<DateTime>
	{
		// Token: 0x0600C2ED RID: 49901 RVA: 0x002A00E9 File Offset: 0x0029E2E9
		public DateTimeSource(string columnName, DateTime value, DateTimeSourceKind source)
			: base(columnName, value)
		{
			this.Source = source;
		}

		// Token: 0x17002127 RID: 8487
		// (get) Token: 0x0600C2EE RID: 49902 RVA: 0x002A00FA File Offset: 0x0029E2FA
		public DateTimeSourceKind Source { get; }
	}
}
