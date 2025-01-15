using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util
{
	// Token: 0x02001AD9 RID: 6873
	public class DataQuality
	{
		// Token: 0x170025F5 RID: 9717
		// (get) Token: 0x0600E318 RID: 58136 RVA: 0x00303235 File Offset: 0x00301435
		// (set) Token: 0x0600E319 RID: 58137 RVA: 0x0030323D File Offset: 0x0030143D
		public double Missingness { get; internal set; }

		// Token: 0x170025F6 RID: 9718
		// (get) Token: 0x0600E31A RID: 58138 RVA: 0x00303246 File Offset: 0x00301446
		// (set) Token: 0x0600E31B RID: 58139 RVA: 0x0030324E File Offset: 0x0030144E
		public double Stringness { get; internal set; }

		// Token: 0x170025F7 RID: 9719
		// (get) Token: 0x0600E31C RID: 58140 RVA: 0x00303257 File Offset: 0x00301457
		// (set) Token: 0x0600E31D RID: 58141 RVA: 0x0030325F File Offset: 0x0030145F
		public double Concentration { get; internal set; }

		// Token: 0x170025F8 RID: 9720
		// (get) Token: 0x0600E31E RID: 58142 RVA: 0x00303268 File Offset: 0x00301468
		// (set) Token: 0x0600E31F RID: 58143 RVA: 0x00303270 File Offset: 0x00301470
		public double Redundancy { get; internal set; }

		// Token: 0x170025F9 RID: 9721
		// (get) Token: 0x0600E320 RID: 58144 RVA: 0x00303279 File Offset: 0x00301479
		// (set) Token: 0x0600E321 RID: 58145 RVA: 0x00303281 File Offset: 0x00301481
		public double Indexness { get; internal set; }

		// Token: 0x170025FA RID: 9722
		// (get) Token: 0x0600E322 RID: 58146 RVA: 0x0030328A File Offset: 0x0030148A
		// (set) Token: 0x0600E323 RID: 58147 RVA: 0x00303292 File Offset: 0x00301492
		public double Splitness { get; internal set; }

		// Token: 0x170025FB RID: 9723
		// (get) Token: 0x0600E324 RID: 58148 RVA: 0x0030329B File Offset: 0x0030149B
		// (set) Token: 0x0600E325 RID: 58149 RVA: 0x003032A3 File Offset: 0x003014A3
		public double DtypesVariability { get; internal set; }

		// Token: 0x170025FC RID: 9724
		// (get) Token: 0x0600E326 RID: 58150 RVA: 0x003032AC File Offset: 0x003014AC
		public double Score
		{
			get
			{
				if (this._score < 0.0)
				{
					this._score = (this._weights[0] * (1.0 - this.Missingness) + this._weights[1] * (1.0 - this.Stringness) + this._weights[2] * (1.0 - this.Concentration) + this._weights[3] * (1.0 - this.Redundancy) + this._weights[4] * (1.0 - this.Indexness) + this._weights[5] * (1.0 - this.Splitness) + this._weights[6] * (1.0 - this.DtypesVariability)) / this._weights.Sum();
				}
				return this._score;
			}
		}

		// Token: 0x0600E327 RID: 58151 RVA: 0x003033B8 File Offset: 0x003015B8
		public DataQuality()
		{
			this.Missingness = 0.0;
			this.Stringness = 0.0;
			this.Concentration = 0.0;
			this.Redundancy = 0.0;
			this.Indexness = 0.0;
			this.Splitness = 0.0;
			this.DtypesVariability = 0.0;
			this._score = -1.0;
		}

		// Token: 0x0600E328 RID: 58152 RVA: 0x003034B8 File Offset: 0x003016B8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Overall data quality score:",
				this.Score.ToString(),
				"===========================\nDetails of Data Quality:\n\tMissingness: ",
				this.Missingness.ToString(),
				"\n\tStringness: ",
				this.Stringness.ToString(),
				"\n\tConcentration: ",
				this.Concentration.ToString(),
				"\n\tRedundancy: ",
				this.Redundancy.ToString(),
				"\n\tIndexness: ",
				this.Indexness.ToString(),
				"\n\tSplitness: ",
				this.Splitness.ToString(),
				"\n\tDType Variability: ",
				this.DtypesVariability.ToString()
			});
		}

		// Token: 0x040055C7 RID: 21959
		private double _score;

		// Token: 0x040055C8 RID: 21960
		private List<double> _weights = new List<double> { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
	}
}
