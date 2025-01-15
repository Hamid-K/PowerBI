using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Web.Learning;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FB7 RID: 4023
	public class LearningOptions : Constraint<WebRegion, IEnumerable<IEnumerable<string>>>, IOptionConstraint<Witnesses.Options>
	{
		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06006F11 RID: 28433 RVA: 0x0016B477 File Offset: 0x00169677
		// (set) Token: 0x06006F12 RID: 28434 RVA: 0x0016B47F File Offset: 0x0016967F
		public int MaxExampleSatisfactionOffset { get; set; } = 5;

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06006F13 RID: 28435 RVA: 0x0016B488 File Offset: 0x00169688
		// (set) Token: 0x06006F14 RID: 28436 RVA: 0x0016B490 File Offset: 0x00169690
		public StringComparer TextComparer { get; set; } = StringComparer.CurrentCultureIgnoreCase;

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06006F15 RID: 28437 RVA: 0x0016B499 File Offset: 0x00169699
		// (set) Token: 0x06006F16 RID: 28438 RVA: 0x0016B4A1 File Offset: 0x001696A1
		public int PredictiveLogicalTableInferenceTimeout { get; set; } = 30000;

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06006F17 RID: 28439 RVA: 0x0016B4AA File Offset: 0x001696AA
		// (set) Token: 0x06006F18 RID: 28440 RVA: 0x0016B4B2 File Offset: 0x001696B2
		public string[] PermittedNodeAttributes { get; set; } = new string[0];

		// Token: 0x06006F19 RID: 28441 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<WebRegion, IEnumerable<IEnumerable<string>>> program)
		{
			return true;
		}

		// Token: 0x06006F1A RID: 28442 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<WebRegion, IEnumerable<IEnumerable<string>>> other)
		{
			return false;
		}

		// Token: 0x06006F1B RID: 28443 RVA: 0x0016B4BB File Offset: 0x001696BB
		public void SetOptions(Witnesses.Options options)
		{
			options.MaxExampleSatisfactionOffset = this.MaxExampleSatisfactionOffset;
			options.TextComparer = this.TextComparer;
			options.PredictiveLogicalTableInferenceTimeout = this.PredictiveLogicalTableInferenceTimeout;
			options.PermittedNodeAttributes = this.PermittedNodeAttributes;
		}

		// Token: 0x06006F1C RID: 28444 RVA: 0x0016B4F0 File Offset: 0x001696F0
		public bool Equals(LearningOptions other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.MaxExampleSatisfactionOffset != other.MaxExampleSatisfactionOffset || this.PredictiveLogicalTableInferenceTimeout != other.PredictiveLogicalTableInferenceTimeout || !this.PermittedNodeAttributes.SequenceEqual(other.PermittedNodeAttributes))
			{
				return false;
			}
			StringComparer textComparer = this.TextComparer;
			if (textComparer == null)
			{
				return other.TextComparer == null;
			}
			return textComparer.Equals(other.TextComparer);
		}

		// Token: 0x06006F1D RID: 28445 RVA: 0x0016B558 File Offset: 0x00169758
		public override bool Equals(Constraint<WebRegion, IEnumerable<IEnumerable<string>>> other)
		{
			return this.Equals(other as LearningOptions);
		}

		// Token: 0x06006F1E RID: 28446 RVA: 0x0016B566 File Offset: 0x00169766
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || this.Equals(obj as LearningOptions));
		}

		// Token: 0x06006F1F RID: 28447 RVA: 0x0016B580 File Offset: 0x00169780
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + this.MaxExampleSatisfactionOffset;
			num = num * 23 + this.PredictiveLogicalTableInferenceTimeout;
			foreach (string text in this.PermittedNodeAttributes)
			{
				num = num * 23 + text.GetHashCode();
			}
			if (this.TextComparer != null)
			{
				num = num * 23 + this.TextComparer.GetHashCode();
			}
			return num;
		}
	}
}
