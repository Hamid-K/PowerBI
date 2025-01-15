using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016CF RID: 5839
	public class NumberSource : InputValueDetail<decimal>
	{
		// Token: 0x0600C2E9 RID: 49897 RVA: 0x002A00BF File Offset: 0x0029E2BF
		public NumberSource(string columnName, decimal value, NumberSourceKind source)
			: base(columnName, value)
		{
			this.Source = source;
		}

		// Token: 0x17002126 RID: 8486
		// (get) Token: 0x0600C2EA RID: 49898 RVA: 0x002A00D0 File Offset: 0x0029E2D0
		public NumberSourceKind Source { get; }
	}
}
