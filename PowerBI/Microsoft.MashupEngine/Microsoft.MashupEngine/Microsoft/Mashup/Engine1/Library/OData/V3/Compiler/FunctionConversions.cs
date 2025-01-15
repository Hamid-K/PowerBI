using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3.Compiler
{
	// Token: 0x020008EB RID: 2283
	internal static class FunctionConversions
	{
		// Token: 0x0600412E RID: 16686 RVA: 0x000DA524 File Offset: 0x000D8724
		static FunctionConversions()
		{
			FunctionConversions.AddConversion(Library.Text.Contains, new FunctionConversions.TextContainsConversion());
			FunctionConversions.AddConversion(Library.Text.EndsWith, "endswith", 2);
			FunctionConversions.AddConversion(Library.Text.StartsWith, "startswith", 2);
			FunctionConversions.AddConversion(Library.Text.Length, "length", 1);
			FunctionConversions.AddConversion(Library.Text.PositionOf, "indexof", 2);
			FunctionConversions.AddConversion(Library.Text.Replace, "replace", 3);
			FunctionConversions.AddConversion(Library.Text.Range, "substring", 2, new int?(3));
			FunctionConversions.AddConversion(CultureSpecificFunction.TextLower, "tolower", 1);
			FunctionConversions.AddConversion(CultureSpecificFunction.TextUpper, "toupper", 1);
			FunctionConversions.AddConversion(Library.Text.Trim, "trim", 1);
			FunctionConversions.AddConversion(Library.Text.Combine, new FunctionConversions.TextConcatConversion());
			FunctionConversions.AddConversion(Library.Date.Day, "day", 1);
			FunctionConversions.AddConversion(Library.Time.Hour, "hour", 1);
			FunctionConversions.AddConversion(Library.Time.Minute, "minute", 1);
			FunctionConversions.AddConversion(Library.Date.Month, "month", 1);
			FunctionConversions.AddConversion(Library.Time.Second, "second", 1);
			FunctionConversions.AddConversion(Library.Date.Year, "year", 1);
			FunctionConversions.AddConversion(CultureSpecificFunction.DateFrom, new FunctionConversions.DateTimeConversion());
			FunctionConversions.AddConversion(CultureSpecificFunction.DateTimeFrom, new FunctionConversions.DateTimeConversion());
			FunctionConversions.AddConversion(CultureSpecificFunction.DateTimeZoneFrom, new FunctionConversions.DateTimeZoneConversion());
			FunctionConversions.AddConversion(Library.Number.Round, "round", 1);
			FunctionConversions.AddConversion(Library.Number.RoundDown, "floor", 1);
			FunctionConversions.AddConversion(Library.Number.RoundUp, "ceiling", 1);
			FunctionConversions.AddConversion(CultureSpecificFunction.DateTimeFromText, new FunctionConversions.FromTextConversion());
			FunctionConversions.AddConversion(CultureSpecificFunction.DateTimeZoneFromText, new FunctionConversions.FromTextConversion());
			FunctionConversions.AddConversion(CultureSpecificFunction.TimeFromText, new FunctionConversions.FromTextConversion());
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x000DA6D9 File Offset: 0x000D88D9
		private static void AddConversion(FunctionValue fromFunction, FunctionConversions.Conversion conversion)
		{
			FunctionConversions.conversions.Add(fromFunction, conversion);
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x000DA6E7 File Offset: 0x000D88E7
		private static void AddConversion(FunctionValue fromFunction, string to, int args)
		{
			FunctionConversions.conversions.Add(fromFunction, new FunctionConversions.Conversion(to, args));
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x000DA6FB File Offset: 0x000D88FB
		private static void AddConversion(FunctionValue fromFunction, string to, int minArgs, int? maxArgs)
		{
			FunctionConversions.conversions.Add(fromFunction, new FunctionConversions.Conversion(to, minArgs, maxArgs, false));
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x000DA711 File Offset: 0x000D8911
		internal static QueryToken Convert(FunctionValue fromFunction, params QueryToken[] arguments)
		{
			return FunctionConversions.conversions[fromFunction].Convert(arguments);
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x000DA724 File Offset: 0x000D8924
		internal static bool TryGetConversion(FunctionValue fromFunction, out FunctionConversions.Conversion conversion)
		{
			return FunctionConversions.conversions.TryGetValue(fromFunction, out conversion);
		}

		// Token: 0x0400223E RID: 8766
		private static readonly Dictionary<FunctionValue, FunctionConversions.Conversion> conversions = new Dictionary<FunctionValue, FunctionConversions.Conversion>();

		// Token: 0x0400223F RID: 8767
		private const string concat = "concat";

		// Token: 0x04002240 RID: 8768
		private const string endswith = "endswith";

		// Token: 0x04002241 RID: 8769
		private const string indexof = "indexof";

		// Token: 0x04002242 RID: 8770
		private const string length = "length";

		// Token: 0x04002243 RID: 8771
		private const string replace = "replace";

		// Token: 0x04002244 RID: 8772
		private const string startswith = "startswith";

		// Token: 0x04002245 RID: 8773
		private const string substring = "substring";

		// Token: 0x04002246 RID: 8774
		private const string substringof = "substringof";

		// Token: 0x04002247 RID: 8775
		private const string tolower = "tolower";

		// Token: 0x04002248 RID: 8776
		private const string toupper = "toupper";

		// Token: 0x04002249 RID: 8777
		private const string trim = "trim";

		// Token: 0x0400224A RID: 8778
		private const string day = "day";

		// Token: 0x0400224B RID: 8779
		private const string hour = "hour";

		// Token: 0x0400224C RID: 8780
		private const string minute = "minute";

		// Token: 0x0400224D RID: 8781
		private const string month = "month";

		// Token: 0x0400224E RID: 8782
		private const string second = "second";

		// Token: 0x0400224F RID: 8783
		private const string year = "year";

		// Token: 0x04002250 RID: 8784
		private const string round = "round";

		// Token: 0x04002251 RID: 8785
		private const string floor = "floor";

		// Token: 0x04002252 RID: 8786
		private const string ceiling = "ceiling";

		// Token: 0x020008EC RID: 2284
		internal class Conversion
		{
			// Token: 0x06004134 RID: 16692 RVA: 0x000DA732 File Offset: 0x000D8932
			internal Conversion(string to, int argCount)
				: this(to, argCount, new int?(argCount), false)
			{
			}

			// Token: 0x06004135 RID: 16693 RVA: 0x000DA743 File Offset: 0x000D8943
			internal Conversion(string to, int minArgs, int? maxArgs, bool requiresTypes = false)
			{
				this.to = to;
				this.minArgs = minArgs;
				this.maxArgs = maxArgs;
				this.requiresTypes = requiresTypes;
			}

			// Token: 0x170014F4 RID: 5364
			// (get) Token: 0x06004136 RID: 16694 RVA: 0x000DA768 File Offset: 0x000D8968
			internal int? MaxArgs
			{
				get
				{
					return this.maxArgs;
				}
			}

			// Token: 0x170014F5 RID: 5365
			// (get) Token: 0x06004137 RID: 16695 RVA: 0x000DA770 File Offset: 0x000D8970
			internal int MinArgs
			{
				get
				{
					return this.minArgs;
				}
			}

			// Token: 0x170014F6 RID: 5366
			// (get) Token: 0x06004138 RID: 16696 RVA: 0x000DA778 File Offset: 0x000D8978
			internal string To
			{
				get
				{
					return this.to;
				}
			}

			// Token: 0x170014F7 RID: 5367
			// (get) Token: 0x06004139 RID: 16697 RVA: 0x000DA780 File Offset: 0x000D8980
			internal bool RequiresTypes
			{
				get
				{
					return this.requiresTypes;
				}
			}

			// Token: 0x0600413A RID: 16698 RVA: 0x000DA788 File Offset: 0x000D8988
			internal virtual bool Validate(IList<IExpression> arguments, FoldingTracingService tracingService)
			{
				using (tracingService.NewScope("FunctionConversions.Conversion.Validate"))
				{
					if (arguments.Count >= this.minArgs)
					{
						if (this.maxArgs != null)
						{
							int count = arguments.Count;
							int? num = this.maxArgs;
							if ((count > num.GetValueOrDefault()) & (num != null))
							{
								goto IL_0047;
							}
						}
						return false;
					}
					IL_0047:
					throw tracingService.NewFoldingFailureException(null);
				}
				bool flag;
				return flag;
			}

			// Token: 0x0600413B RID: 16699 RVA: 0x000DA804 File Offset: 0x000D8A04
			internal virtual bool ValidateWithTypes(IList<IExpression> arguments, TypeValue[] types, FoldingTracingService tracingService)
			{
				using (tracingService.NewScope("FunctionConversions.Conversion.ValidateWithTypes"))
				{
					if (arguments.Count >= this.minArgs)
					{
						if (this.maxArgs != null)
						{
							int count = arguments.Count;
							int? num = this.maxArgs;
							if ((count > num.GetValueOrDefault()) & (num != null))
							{
								goto IL_0052;
							}
						}
						if (arguments.Count == types.Length)
						{
							return false;
						}
					}
					IL_0052:
					throw tracingService.NewFoldingFailureException(null);
				}
				bool flag;
				return flag;
			}

			// Token: 0x0600413C RID: 16700 RVA: 0x000DA88C File Offset: 0x000D8A8C
			internal virtual QueryToken Convert(IEnumerable<QueryToken> arguments)
			{
				return new FunctionCallQueryToken(this.to, arguments);
			}

			// Token: 0x0600413D RID: 16701 RVA: 0x000DA89C File Offset: 0x000D8A9C
			protected static string TryGetArgumentasTextLiteral(IExpression expression)
			{
				Value value;
				if (!expression.TryGetConstant(out value) || !value.IsText)
				{
					return null;
				}
				return value.AsString;
			}

			// Token: 0x04002253 RID: 8787
			private readonly int? maxArgs;

			// Token: 0x04002254 RID: 8788
			private readonly int minArgs;

			// Token: 0x04002255 RID: 8789
			private readonly string to;

			// Token: 0x04002256 RID: 8790
			private readonly bool requiresTypes;
		}

		// Token: 0x020008ED RID: 2285
		private sealed class TextConcatConversion : FunctionConversions.Conversion
		{
			// Token: 0x0600413E RID: 16702 RVA: 0x000DA8C3 File Offset: 0x000D8AC3
			internal TextConcatConversion()
				: base("concat", 1, new int?(2), false)
			{
			}

			// Token: 0x0600413F RID: 16703 RVA: 0x000DA8D8 File Offset: 0x000D8AD8
			internal override bool Validate(IList<IExpression> arguments, FoldingTracingService tracingService)
			{
				bool flag;
				using (tracingService.NewScope("FunctionConversions.TextConcatConversion.Validate"))
				{
					base.Validate(arguments, tracingService);
					if (arguments.Count == 2 && FunctionConversions.Conversion.TryGetArgumentasTextLiteral(arguments[1]) != string.Empty)
					{
						throw tracingService.NewFoldingFailureException(null);
					}
					flag = true;
				}
				return flag;
			}

			// Token: 0x06004140 RID: 16704 RVA: 0x000DA944 File Offset: 0x000D8B44
			internal override QueryToken Convert(IEnumerable<QueryToken> arguments)
			{
				QueryToken queryToken;
				using (IEnumerator<QueryToken> enumerator = arguments.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						queryToken = LiteralConverter.LiteralTokenEmptyString;
					}
					else
					{
						QueryToken queryToken2 = enumerator.Current;
						if (!enumerator.MoveNext())
						{
							queryToken = queryToken2;
						}
						else
						{
							do
							{
								queryToken2 = new FunctionCallQueryToken(base.To, new QueryToken[] { queryToken2, enumerator.Current });
							}
							while (enumerator.MoveNext());
							queryToken = queryToken2;
						}
					}
				}
				return queryToken;
			}
		}

		// Token: 0x020008EE RID: 2286
		private sealed class TextContainsConversion : FunctionConversions.Conversion
		{
			// Token: 0x06004141 RID: 16705 RVA: 0x000DA9C4 File Offset: 0x000D8BC4
			internal TextContainsConversion()
				: base("substringof", 2)
			{
			}

			// Token: 0x06004142 RID: 16706 RVA: 0x000DA9D2 File Offset: 0x000D8BD2
			internal override QueryToken Convert(IEnumerable<QueryToken> arguments)
			{
				return base.Convert(arguments.Reverse<QueryToken>());
			}
		}

		// Token: 0x020008EF RID: 2287
		private sealed class FromTextConversion : FunctionConversions.Conversion
		{
			// Token: 0x06004143 RID: 16707 RVA: 0x000DA9E0 File Offset: 0x000D8BE0
			public FromTextConversion()
				: base(string.Empty, 1)
			{
			}

			// Token: 0x06004144 RID: 16708 RVA: 0x000DA9F0 File Offset: 0x000D8BF0
			internal override bool Validate(IList<IExpression> arguments, FoldingTracingService tracingService)
			{
				bool flag;
				using (tracingService.NewScope("FunctionConversions.FromTextConversion.Validate"))
				{
					if (arguments.Count != 1 || FunctionConversions.Conversion.TryGetArgumentasTextLiteral(arguments[0]) == null)
					{
						throw tracingService.NewFoldingFailureException(null);
					}
					flag = true;
				}
				return flag;
			}

			// Token: 0x06004145 RID: 16709 RVA: 0x000DAA48 File Offset: 0x000D8C48
			internal override QueryToken Convert(IEnumerable<QueryToken> arguments)
			{
				return LiteralConverter.Convert(((IConstantExpression)arguments.First<QueryToken>()).Value, TypeValue.DateTime);
			}
		}

		// Token: 0x020008F0 RID: 2288
		private sealed class DateTimeConversion : FunctionConversions.Conversion
		{
			// Token: 0x06004146 RID: 16710 RVA: 0x000DAA64 File Offset: 0x000D8C64
			public DateTimeConversion()
				: base(string.Empty, 1, new int?(1), true)
			{
			}

			// Token: 0x06004147 RID: 16711 RVA: 0x000DAA7C File Offset: 0x000D8C7C
			internal override bool ValidateWithTypes(IList<IExpression> arguments, TypeValue[] types, FoldingTracingService tracingService)
			{
				bool flag;
				using (tracingService.NewScope("FunctionConversions.DateTimeConversion.ValidateWithTypes"))
				{
					if (arguments.Count != 1 || types.Length != 1 || types[0].TypeKind != ValueKind.DateTime)
					{
						throw tracingService.NewFoldingFailureException(null);
					}
					flag = true;
				}
				return flag;
			}

			// Token: 0x06004148 RID: 16712 RVA: 0x000DAAD8 File Offset: 0x000D8CD8
			internal override QueryToken Convert(IEnumerable<QueryToken> arguments)
			{
				return arguments.First<QueryToken>();
			}
		}

		// Token: 0x020008F1 RID: 2289
		private sealed class DateTimeZoneConversion : FunctionConversions.Conversion
		{
			// Token: 0x06004149 RID: 16713 RVA: 0x000DAA64 File Offset: 0x000D8C64
			public DateTimeZoneConversion()
				: base(string.Empty, 1, new int?(1), true)
			{
			}

			// Token: 0x0600414A RID: 16714 RVA: 0x000DAAE0 File Offset: 0x000D8CE0
			internal override bool ValidateWithTypes(IList<IExpression> arguments, TypeValue[] types, FoldingTracingService tracingService)
			{
				bool flag;
				using (tracingService.NewScope("FunctionConversions.DateTimeZoneConversion.ValidateWithTypes"))
				{
					if (arguments.Count != 1 || types.Length != 1 || types[0].TypeKind != ValueKind.DateTimeZone)
					{
						throw tracingService.NewFoldingFailureException(null);
					}
					flag = true;
				}
				return flag;
			}

			// Token: 0x0600414B RID: 16715 RVA: 0x000DAAD8 File Offset: 0x000D8CD8
			internal override QueryToken Convert(IEnumerable<QueryToken> arguments)
			{
				return arguments.First<QueryToken>();
			}
		}
	}
}
