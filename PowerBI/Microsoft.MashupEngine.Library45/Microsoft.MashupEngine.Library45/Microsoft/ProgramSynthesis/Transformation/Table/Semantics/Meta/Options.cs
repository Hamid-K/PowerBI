using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta
{
	// Token: 0x02001AF5 RID: 6901
	public class Options : DSLOptions
	{
		// Token: 0x170025FD RID: 9725
		// (get) Token: 0x0600E387 RID: 58247 RVA: 0x00304D10 File Offset: 0x00302F10
		// (set) Token: 0x0600E388 RID: 58248 RVA: 0x00304D18 File Offset: 0x00302F18
		public bool DestructiveSuggestion { get; set; } = true;

		// Token: 0x170025FE RID: 9726
		// (get) Token: 0x0600E389 RID: 58249 RVA: 0x00304D21 File Offset: 0x00302F21
		// (set) Token: 0x0600E38A RID: 58250 RVA: 0x00304D29 File Offset: 0x00302F29
		public IEnumerable<string> FocusedColumnNames { get; set; }

		// Token: 0x170025FF RID: 9727
		// (get) Token: 0x0600E38B RID: 58251 RVA: 0x00304D32 File Offset: 0x00302F32
		// (set) Token: 0x0600E38C RID: 58252 RVA: 0x00304D3A File Offset: 0x00302F3A
		public bool UseAllDataForLearning { get; set; }

		// Token: 0x17002600 RID: 9728
		// (get) Token: 0x0600E38D RID: 58253 RVA: 0x00304D43 File Offset: 0x00302F43
		// (set) Token: 0x0600E38E RID: 58254 RVA: 0x00304D4B File Offset: 0x00302F4B
		public int NumSamplesForLearning { get; internal set; } = 1;

		// Token: 0x17002601 RID: 9729
		// (get) Token: 0x0600E38F RID: 58255 RVA: 0x00304D54 File Offset: 0x00302F54
		// (set) Token: 0x0600E390 RID: 58256 RVA: 0x00304D5C File Offset: 0x00302F5C
		public int? NumRowsToConsiderForLearning { get; internal set; }

		// Token: 0x17002602 RID: 9730
		// (get) Token: 0x0600E391 RID: 58257 RVA: 0x00304D65 File Offset: 0x00302F65
		// (set) Token: 0x0600E392 RID: 58258 RVA: 0x00304D6D File Offset: 0x00302F6D
		public int? NumRowsToSampleForLearning { get; internal set; }

		// Token: 0x17002603 RID: 9731
		// (get) Token: 0x0600E393 RID: 58259 RVA: 0x00304D76 File Offset: 0x00302F76
		// (set) Token: 0x0600E394 RID: 58260 RVA: 0x00304D7E File Offset: 0x00302F7E
		public bool UseAllDataForRanking { get; set; }

		// Token: 0x17002604 RID: 9732
		// (get) Token: 0x0600E395 RID: 58261 RVA: 0x00304D87 File Offset: 0x00302F87
		// (set) Token: 0x0600E396 RID: 58262 RVA: 0x00304D8F File Offset: 0x00302F8F
		public bool UseLearningDataForRanking { get; set; } = true;

		// Token: 0x17002605 RID: 9733
		// (get) Token: 0x0600E397 RID: 58263 RVA: 0x00304D98 File Offset: 0x00302F98
		// (set) Token: 0x0600E398 RID: 58264 RVA: 0x00304DA0 File Offset: 0x00302FA0
		public int NumSamplesForRanking { get; internal set; } = 1;

		// Token: 0x17002606 RID: 9734
		// (get) Token: 0x0600E399 RID: 58265 RVA: 0x00304DA9 File Offset: 0x00302FA9
		// (set) Token: 0x0600E39A RID: 58266 RVA: 0x00304DB1 File Offset: 0x00302FB1
		public int? NumRowsToConsiderForRanking { get; internal set; }

		// Token: 0x17002607 RID: 9735
		// (get) Token: 0x0600E39B RID: 58267 RVA: 0x00304DBA File Offset: 0x00302FBA
		// (set) Token: 0x0600E39C RID: 58268 RVA: 0x00304DC2 File Offset: 0x00302FC2
		public int? NumRowsToSampleForRanking { get; internal set; }

		// Token: 0x17002608 RID: 9736
		// (get) Token: 0x0600E39D RID: 58269 RVA: 0x00304DCB File Offset: 0x00302FCB
		// (set) Token: 0x0600E39E RID: 58270 RVA: 0x00304DD3 File Offset: 0x00302FD3
		public Operators AllowedOperators { get; set; } = Operators.All;

		// Token: 0x0600E39F RID: 58271 RVA: 0x00304DDC File Offset: 0x00302FDC
		public override int GetHashCode()
		{
			string[] array = new string[12];
			array[0] = this.DestructiveSuggestion.ToString();
			int num = 1;
			IEnumerable<string> focusedColumnNames = this.FocusedColumnNames;
			array[num] = ((focusedColumnNames != null) ? focusedColumnNames.ToString() : null);
			array[2] = this.UseAllDataForLearning.ToString();
			array[3] = this.NumSamplesForLearning.ToString();
			int num2 = 4;
			int? num3 = this.NumRowsToConsiderForLearning;
			array[num2] = ((num3 != null) ? num3.GetValueOrDefault().ToString() : null);
			int num4 = 5;
			num3 = this.NumRowsToSampleForLearning;
			array[num4] = ((num3 != null) ? num3.GetValueOrDefault().ToString() : null);
			array[6] = this.UseAllDataForRanking.ToString();
			array[7] = this.UseLearningDataForRanking.ToString();
			array[8] = this.NumSamplesForRanking.ToString();
			int num5 = 9;
			num3 = this.NumRowsToConsiderForRanking;
			array[num5] = ((num3 != null) ? num3.GetValueOrDefault().ToString() : null);
			int num6 = 10;
			num3 = this.NumRowsToSampleForRanking;
			array[num6] = ((num3 != null) ? num3.GetValueOrDefault().ToString() : null);
			array[11] = this.AllowedOperators.ToString();
			return array.OrderDependentHashCode<string>();
		}
	}
}
