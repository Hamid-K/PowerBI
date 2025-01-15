using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001F2 RID: 498
	public class NumericToken : EntityToken
	{
		// Token: 0x06000ABE RID: 2750 RVA: 0x00020669 File Offset: 0x0001E869
		protected NumericToken(string source, int start, int end, double numericValue)
			: base(source, start, end)
		{
			this.NumericValue = numericValue;
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0002067C File Offset: 0x0001E87C
		public double NumericValue { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0001C6F9 File Offset: 0x0001A8F9
		public override double ScoreMultiplier
		{
			get
			{
				return 2.0;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00020684 File Offset: 0x0001E884
		public override string EntityName
		{
			get
			{
				return "Numeric";
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002068B File Offset: 0x0001E88B
		protected IEnumerable<CompletionInfo> GetSearchTreeEntries()
		{
			yield return new CompletionInfo(this.NumericValue.ToString(CultureInfo.InvariantCulture), this, 1.0, null);
			long floorValue = (long)Math.Floor(this.NumericValue);
			yield return new CompletionInfo(FormattableString.Invariant(FormattableStringFactory.Create("{0:n}", new object[] { floorValue })), this, 0.5, null);
			yield return new CompletionInfo(FormattableString.Invariant(FormattableStringFactory.Create("{0:n0}", new object[] { floorValue })), this, 0.5, null);
			yield return new CompletionInfo(FormattableString.Invariant(FormattableStringFactory.Create("{0:x}", new object[] { floorValue })), this, 0.5, null);
			long ceilValue = (long)Math.Ceiling(this.NumericValue);
			if (ceilValue != floorValue)
			{
				yield return new CompletionInfo(FormattableString.Invariant(FormattableStringFactory.Create("{0:n}", new object[] { ceilValue })), this, 0.5, null);
				yield return new CompletionInfo(FormattableString.Invariant(FormattableStringFactory.Create("{0:n0}", new object[] { ceilValue })), this, 0.5, null);
				string text = FormattableString.Invariant(FormattableStringFactory.Create("{0:x}", new object[] { ceilValue }));
				double num = 0.5;
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["Provenance"] = FormattableString.Invariant(FormattableStringFactory.Create("Hex of {0}", new object[] { ceilValue }));
				yield return new CompletionInfo(text, this, num, dictionary);
			}
			yield return new CompletionInfo(FormattableString.Invariant(FormattableStringFactory.Create("{0:g}", new object[] { this.NumericValue })), this, 0.7, null);
			yield return new CompletionInfo(FormattableString.Invariant(FormattableStringFactory.Create("{0:e}", new object[] { this.NumericValue })), this, 0.7, null);
			yield return new CompletionInfo(FormattableString.Invariant(FormattableStringFactory.Create("{0:f1}", new object[] { this.NumericValue })), this, 0.7, null);
			if (this.NumericValue > 0.0)
			{
				yield return new CompletionInfo(FormattableString.Invariant(FormattableStringFactory.Create("+{0}", new object[] { this.NumericValue })), this, 1.0, null);
			}
			yield break;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002069C File Offset: 0x0001E89C
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			foreach (CompletionInfo completionInfo in this.GetSearchTreeEntries())
			{
				tree.Add(completionInfo.Key, completionInfo);
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000206F0 File Offset: 0x0001E8F0
		public override bool Equals(EntityToken other)
		{
			return other != null && (other == this || (!(base.GetType() != other.GetType()) && ((NumericToken)other).NumericValue == this.NumericValue));
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00020725 File Offset: 0x0001E925
		public override bool ValueBasedEquality(EntityToken other)
		{
			return this.Equals(other);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00020730 File Offset: 0x0001E930
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ (this.NumericValue.GetHashCode() * 2731);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002075D File Offset: 0x0001E95D
		public override int ValueBasedHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00020765 File Offset: 0x0001E965
		public static NumericToken Create(string source, int start, int end, double numericValue, bool isFormatted)
		{
			if (!isFormatted)
			{
				return new NumericToken(source, start, end, numericValue);
			}
			return new FormattedNumberToken(source, start, end, numericValue);
		}
	}
}
