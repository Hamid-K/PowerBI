using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001F4 RID: 500
	public class NumericTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000AD1 RID: 2769 RVA: 0x00020BD8 File Offset: 0x0001EDD8
		[JsonConstructor]
		public NumericTokenizer()
			: base(OverlapStrategy.Subsumption)
		{
			this._scientificTokenPattern = new TokenPattern(NumericTokenizer.ScientificNumberPatternString, null, null, false, false, Array.Empty<RegexOptions>());
			this._hexTokenPattern = new TokenPattern(NumericTokenizer.HexNumberString, "(?:^|[^\\dxX0-9a-fA-F])", "(?:$|[^\\dxX0-9a-fA-F])", false, false, Array.Empty<RegexOptions>());
			IEnumerable<TokenPattern> enumerable = NumericTokenizer.AllSeparatedPatternStrings.Select((string p) => new TokenPattern(p, null, null, false, false, Array.Empty<RegexOptions>())).Concat(Seq.Of<TokenPattern>(new TokenPattern[] { this._scientificTokenPattern, this._hexTokenPattern }));
			base.Initialize(new RegexBasedTokenizer.TokenFactoryDelegate(this.ProcessMatch), enumerable);
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00020C85 File Offset: 0x0001EE85
		private static string FirstDigitGroupName { get; } = "FirstDigitGroup";

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00020C8C File Offset: 0x0001EE8C
		private static string SecondDigitGroupName { get; } = "SecondDigitGroup";

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00020C93 File Offset: 0x0001EE93
		private static string ThirdDigitGroupName { get; } = "ThirdDigitGroup";

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00020C9A File Offset: 0x0001EE9A
		private static string SeparatedNumberPatternFormatString { get; } = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>(?<{1}>\\d+)(?:{{0}}(?<{2}>\\d{{{{2,3}}}}))+(?:{{1}}(?<{3}>\\d+))?)", new object[]
		{
			"FullNumberGroup",
			NumericTokenizer.FirstDigitGroupName,
			NumericTokenizer.SecondDigitGroupName,
			NumericTokenizer.ThirdDigitGroupName
		}));

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00020CA1 File Offset: 0x0001EEA1
		private static IReadOnlyList<Record<string, string>> AllSeparatorPairs { get; } = (from l in new string[][]
			{
				NumericTokenizer.ThousandsSeparators,
				NumericTokenizer.DecimalSeparators
			}.CartesianProduct<string>()
			select l.ToList<string>() into l
			where l[0] != l[1]
			select Record.Create<string, string>(l[0], l[1])).ToImmutableList<Record<string, string>>();

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00020CA8 File Offset: 0x0001EEA8
		internal static IReadOnlyList<string> AllSeparatedPatternStrings { get; } = NumericTokenizer.AllSeparatorPairs.Select((Record<string, string> pair) => string.Format(CultureInfo.InvariantCulture, NumericTokenizer.SeparatedNumberPatternFormatString, pair.Item1, pair.Item2)).ToImmutableList<string>();

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00020CB0 File Offset: 0x0001EEB0
		private IEnumerable<EntityToken> ProcessMatch(TokenPatternMatch m)
		{
			if (m.Value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
			{
				ulong num = Convert.ToUInt64(m.Value, 16);
				return Seq.Of<NumericToken>(new NumericToken[]
				{
					NumericToken.Create(m.Source, m.Start, m.End, num, false),
					new HexNumberToken(m.Source, m.Start, m.End, num)
				});
			}
			bool flag = m.Pattern != this._scientificTokenPattern && m.Pattern != this._hexTokenPattern;
			double num2;
			if (!NumericTokenizer.TryParseNumberMatch(m.FullMatch, out num2))
			{
				return Enumerable.Empty<EntityToken>();
			}
			return Seq.Of<NumericToken>(new NumericToken[] { NumericToken.Create(m.Source, m.Start, m.End, num2, flag) });
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00020D94 File Offset: 0x0001EF94
		internal static bool TryParseNumberMatch(Match m, out double result)
		{
			if (double.TryParse(m.Value, out result))
			{
				return true;
			}
			if (double.TryParse(m.Groups["FullNumberGroup"].Value, out result))
			{
				return true;
			}
			Group group = m.Groups[NumericTokenizer.FirstDigitGroupName];
			Group second = m.Groups[NumericTokenizer.SecondDigitGroupName];
			Group group2 = m.Groups[NumericTokenizer.ThirdDigitGroupName];
			return group.Success && double.TryParse(group.Value + string.Join(string.Empty, from idx in Enumerable.Range(0, second.Captures.Count)
				select second.Captures[idx].Value) + "." + group2.Value, out result);
		}

		// Token: 0x04000561 RID: 1377
		private const string HexNumberLeftContextPattern = "(?:^|[^\\dxX0-9a-fA-F])";

		// Token: 0x04000562 RID: 1378
		private const string HexNumberRightContextPattern = "(?:$|[^\\dxX0-9a-fA-F])";

		// Token: 0x04000563 RID: 1379
		private static readonly string[] ThousandsSeparators = new string[] { ",", "\\.", " ", "'" };

		// Token: 0x04000564 RID: 1380
		private static readonly string[] DecimalSeparators = new string[] { "\\.", "," };

		// Token: 0x04000565 RID: 1381
		private static readonly string ScientificNumberPatternString = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>(?:\\-|\\+)?\\d+(?:\\.\\d+(?:[eE][\\+\\-]\\d+)?)?)", new object[] { "FullNumberGroup" }));

		// Token: 0x04000566 RID: 1382
		private static readonly string HexNumberString = FormattableString.Invariant(FormattableStringFactory.Create("0[xX](?<{0}>[0-9a-fA-F])+", new object[] { "HexNumberGroup" }));

		// Token: 0x04000567 RID: 1383
		private readonly TokenPattern _hexTokenPattern;

		// Token: 0x04000568 RID: 1384
		private readonly TokenPattern _scientificTokenPattern;

		// Token: 0x0400056C RID: 1388
		private const string FullNumberMatchGroupName = "FullNumberGroup";

		// Token: 0x0400056D RID: 1389
		private const string HexNumberGroupName = "HexNumberGroup";
	}
}
