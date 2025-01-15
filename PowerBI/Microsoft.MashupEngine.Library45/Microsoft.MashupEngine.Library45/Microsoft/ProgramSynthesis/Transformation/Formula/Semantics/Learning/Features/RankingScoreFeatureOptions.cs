using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Features
{
	// Token: 0x0200170C RID: 5900
	public class RankingScoreFeatureOptions : FeatureOptions
	{
		// Token: 0x17002176 RID: 8566
		// (get) Token: 0x0600C467 RID: 50279 RVA: 0x002A499C File Offset: 0x002A2B9C
		public IReadOnlyList<IRow> AllInputs
		{
			get
			{
				IReadOnlyList<IRow> readOnlyList;
				if ((readOnlyList = this._allInputs) == null)
				{
					IReadOnlyList<Example> examples = this.Examples;
					IReadOnlyList<IRow> readOnlyList2;
					if (examples == null)
					{
						readOnlyList2 = null;
					}
					else
					{
						readOnlyList2 = examples.Select((Example e) => e.Input).Concat(this.Inputs).ToReadOnlyList<IRow>();
					}
					readOnlyList = (this._allInputs = readOnlyList2);
				}
				return readOnlyList;
			}
		}

		// Token: 0x17002177 RID: 8567
		// (get) Token: 0x0600C468 RID: 50280 RVA: 0x002A49FD File Offset: 0x002A2BFD
		// (set) Token: 0x0600C469 RID: 50281 RVA: 0x002A4A05 File Offset: 0x002A2C05
		public IReadOnlyList<string> ColumnNamePriority { get; set; }

		// Token: 0x17002178 RID: 8568
		// (get) Token: 0x0600C46A RID: 50282 RVA: 0x002A4A0E File Offset: 0x002A2C0E
		// (set) Token: 0x0600C46B RID: 50283 RVA: 0x002A4A16 File Offset: 0x002A2C16
		public IReadOnlyList<CultureInfo> DataCultures { get; set; }

		// Token: 0x17002179 RID: 8569
		// (get) Token: 0x0600C46C RID: 50284 RVA: 0x002A4A1F File Offset: 0x002A2C1F
		// (set) Token: 0x0600C46D RID: 50285 RVA: 0x002A4A27 File Offset: 0x002A2C27
		public IReadOnlyList<Example> Examples { get; set; }

		// Token: 0x1700217A RID: 8570
		// (get) Token: 0x0600C46E RID: 50286 RVA: 0x002A4A30 File Offset: 0x002A2C30
		// (set) Token: 0x0600C46F RID: 50287 RVA: 0x002A4A38 File Offset: 0x002A2C38
		public IReadOnlyList<IRow> Inputs { get; set; }

		// Token: 0x0600C470 RID: 50288 RVA: 0x002A4A41 File Offset: 0x002A2C41
		public RankingScoreFeatureOptions()
			: base(null)
		{
		}

		// Token: 0x04004CA3 RID: 19619
		private IReadOnlyList<IRow> _allInputs;
	}
}
