using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001B9A RID: 7066
	[DebuggerDisplay("{ProgramNode}")]
	public class Program : TransformationProgram<Program, IRow, object>, ITypedProgram<IRow, object>
	{
		// Token: 0x1700269C RID: 9884
		// (get) Token: 0x0600E752 RID: 59218 RVA: 0x0031075C File Offset: 0x0030E95C
		internal @switch? IndexInputProgramNode
		{
			get
			{
				return this._indexInputProgramNode.Value;
			}
		}

		// Token: 0x0600E753 RID: 59219 RVA: 0x0031076C File Offset: 0x0030E96C
		public Program(@switch programNode, double? score = null)
		{
			ProgramNode node = programNode.Node;
			double num = score ?? programNode.Node.GetFeatureValue<double>(Learner.Instance.ScoreFeature, null);
			Func<ProgramNode, ProgramNode> func;
			if ((func = Program.<>O.<0>__Rewrite) == null)
			{
				func = (Program.<>O.<0>__Rewrite = new Func<ProgramNode, ProgramNode>(Rewriter.Rewrite));
			}
			base..ctor(node, num, func);
			this._typedProgram = programNode;
			this._inputColumnTypes = new Lazy<IImmutableDictionary<string, IImmutableList<IType>>>(new Func<IImmutableDictionary<string, IImmutableList<IType>>>(this.ComputeInputColumnTypes));
			this._outputTypes = new Lazy<IEnumerable<IType>>(new Func<IEnumerable<IType>>(this.ComputeOutputTypes));
			this._descriptionLookup = new Lazy<DescriptionLookup>(() => DescriptionLookup.Create(base.ProgramNode), LazyThreadSafetyMode.ExecutionAndPublication);
			this._description = new Lazy<string>(new Func<string>(this._Describe));
			this._branches = new Lazy<IReadOnlyList<Branch>>(() => base.ProgramNode.AcceptVisitor<IEnumerable<Branch>>(Program.BranchCollector.Instance).ToList<Branch>());
			this._indexInputProgramNode = new Lazy<@switch?>(new Func<@switch?>(this.BuildIndexInputProgramNode));
			this._inputRowProgramNode = new Lazy<@switch?>(new Func<@switch?>(this.BuildInputRowProgramNode));
			this.ColumnsUsed = programNode.Node.GetFeatureValue<IImmutableSet<string>>(Program.InputsUsed, null).OrderBy((string col) => col, StringComparer.Ordinal).ToList<string>();
		}

		// Token: 0x0600E754 RID: 59220 RVA: 0x003108E8 File Offset: 0x0030EAE8
		private @switch? BuildIndexInputProgramNode(IEnumerable<KeyValuePair<string, int>> columnIndexMapping)
		{
			v chooseInputNode = this._rule.ChooseInput(this._var.vs, this._var.columnName);
			lookupInput lookupInputNode = this._rule.LookupInput(this._var.vs, this._var.columnName);
			conv convHole = Language.Build.Node.Hole.conv;
			IEnumerable<RewriteRule> enumerable = columnIndexMapping.SelectMany(delegate(KeyValuePair<string, int> kv)
			{
				string key = kv.Key;
				int value = kv.Value;
				v v = Language.Build.Node.UnnamedConversion.v_indexInputString(this._rule.IndexInputString(this._var.vs, this._rule.columnIdx(value)));
				lookupInput lookupInput = Language.Build.Node.UnnamedConversion.lookupInput_indexInputString(this._rule.IndexInputString(this._var.vs, this._rule.columnIdx(value)));
				return new RewriteRule[]
				{
					new RewriteRule(this._rule.LetColumnName(this._rule.idx(key), this._rule.LetX(chooseInputNode, convHole)).Node, this._rule.LetColumnName(this._rule.idx(key), this._rule.LetX(v, convHole)).Node),
					new RewriteRule(this._rule.LetColumnName(this._rule.idx(key), this._rule.LetCell(lookupInputNode, convHole)).Node, this._rule.LetColumnName(this._rule.idx(key), this._rule.LetCell(lookupInput, convHole)).Node),
					new RewriteRule(this._rule.SelectInput(this._var.vs, this._rule.name(key)).Node, this._rule.SelectIndexedInput(v).Node)
				};
			});
			ProgramNode programNode = base.ProgramNode;
			Func<ProgramNode, RewriteRule, ProgramNode> func;
			if ((func = Program.<>O.<1>__Rewrite) == null)
			{
				func = (Program.<>O.<1>__Rewrite = new Func<ProgramNode, RewriteRule, ProgramNode>(ProgramSetRewriter.Rewrite));
			}
			ProgramNode programNode2 = enumerable.Aggregate(programNode, func);
			return new @switch?(Language.Build.Node.Cast.@switch(programNode2));
		}

		// Token: 0x0600E755 RID: 59221 RVA: 0x003109BD File Offset: 0x0030EBBD
		private @switch? BuildIndexInputProgramNode()
		{
			IEnumerable<string> columnsUsed = this.ColumnsUsed;
			Func<string, int, KeyValuePair<string, int>> func;
			if ((func = Program.<>O.<2>__Create) == null)
			{
				func = (Program.<>O.<2>__Create = new Func<string, int, KeyValuePair<string, int>>(KVP.Create<string, int>));
			}
			return this.BuildIndexInputProgramNode(columnsUsed.Select(func));
		}

		// Token: 0x0600E756 RID: 59222 RVA: 0x003109EC File Offset: 0x0030EBEC
		private @switch? BuildInputRowProgramNode()
		{
			List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(this.ColumnsUsed.Count);
			foreach (string text in this.ColumnsUsed)
			{
				int num;
				if (!int.TryParse(text, out num))
				{
					return null;
				}
				list.Add(KVP.Create<string, int>(text, num));
			}
			return this.BuildIndexInputProgramNode(list);
		}

		// Token: 0x1700269D RID: 9885
		// (get) Token: 0x0600E757 RID: 59223 RVA: 0x00310A74 File Offset: 0x0030EC74
		public IReadOnlyList<string> ColumnsUsed { get; }

		// Token: 0x1700269E RID: 9886
		// (get) Token: 0x0600E758 RID: 59224 RVA: 0x00310A7C File Offset: 0x0030EC7C
		public IEnumerable<IType> OutputTypes
		{
			get
			{
				return this._outputTypes.Value;
			}
		}

		// Token: 0x0600E759 RID: 59225 RVA: 0x00310A89 File Offset: 0x0030EC89
		public override object Run(IRow input)
		{
			ValueSubstring valueSubstring = (ValueSubstring)base.ProgramNode.Invoke(input.AsStateForExecution());
			if (valueSubstring == null)
			{
				return null;
			}
			return valueSubstring.Value;
		}

		// Token: 0x0600E75A RID: 59226 RVA: 0x00310AAC File Offset: 0x0030ECAC
		public object Run(IIndexableRow input)
		{
			@switch? @switch;
			ValueSubstring valueSubstring = (ValueSubstring)((this._indexInputProgramNode.Value != null) ? @switch.GetValueOrDefault().Node.Invoke(input.AsStateForExecution()) : null);
			if (valueSubstring == null)
			{
				return null;
			}
			return valueSubstring.Value;
		}

		// Token: 0x0600E75B RID: 59227 RVA: 0x00310AFB File Offset: 0x0030ECFB
		private IImmutableSet<IType> ComputeOutputTypes()
		{
			return this.Branches.SelectMany((Branch branch) => Program.ComputeSingleBranchOutputTypes(branch.Body)).ToImmutableHashSet<IType>();
		}

		// Token: 0x0600E75C RID: 59228 RVA: 0x00310B2C File Offset: 0x0030ED2C
		private static HashSet<IType> ComputeSingleBranchOutputTypes(st programNode)
		{
			HashSet<IType> hashSet = new HashSet<IType> { UnknownType.Instance };
			conv? conv;
			Atom? atom;
			if (programNode.Cast_Transformation().e.As_Atom(Language.Build) == null)
			{
				conv = null;
			}
			else if (atom.GetValueOrDefault().f.As_LetColumnName(Language.Build) == null)
			{
				conv = null;
			}
			else
			{
				LetColumnName? letColumnName;
				conv = new conv?(letColumnName.GetValueOrDefault().letOptions.Switch<conv>(Language.Build, (LetCell letCell) => letCell.conv, (LetX letX) => letX.conv));
			}
			conv? conv2 = conv;
			if (conv2 == null)
			{
				return hashSet;
			}
			FormatPartialDateTime formatPartialDateTime;
			if (conv2.Value.Is_FormatPartialDateTime(Language.Build, out formatPartialDateTime))
			{
				DateTimeFormat value = formatPartialDateTime.outputDtFormat.Value;
				return new HashSet<IType>
				{
					new FormattedPartialDateTimeType(value)
				};
			}
			Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatNumber formatNumber;
			if (conv2.Value.Is_FormatNumber(Language.Build, out formatNumber))
			{
				NumberFormat numberFormat = (NumberFormat)formatNumber.numberFormat.Node.Invoke(null);
				return new HashSet<IType>
				{
					new NumberType(numberFormat)
				};
			}
			return hashSet;
		}

		// Token: 0x0600E75D RID: 59229 RVA: 0x00310CB0 File Offset: 0x0030EEB0
		private IImmutableDictionary<string, IImmutableList<IType>> ComputeInputColumnTypes()
		{
			Dictionary<string, ImmutableList<IType>> dateTypes = (from pdt in this.AllTransformations.OfType<ParseDateTime>()
				where Language.Build.Node.CastRule.ParsePartialDateTime(pdt.ProgramNode).SS.Is_WholeColumn(Language.Build)
				group pdt by pdt.ColumnName).ToDictionary((IGrouping<string, ParseDateTime> g) => g.Key, (IGrouping<string, ParseDateTime> g) => (from f in g.SelectMany((ParseDateTime pdt) => pdt.Formats).Distinct<DateTimeFormat>()
				select new FormattedPartialDateTimeType(f)).ToImmutableList<IType>());
			return this.ColumnsUsed.ToImmutableDictionary((string c) => c, (string c) => (from l in dateTypes.MaybeGet(c)
				where l.Any<IType>()
				select l).OrElse(ImmutableList<IType>.Empty.Add(UnknownType.Instance)));
		}

		// Token: 0x0600E75E RID: 59230 RVA: 0x00310D98 File Offset: 0x0030EF98
		public IType GetInputType(IRow row, string columnName)
		{
			List<IType> list = this.GetInputTypes(columnName).Where(delegate(IType t)
			{
				object obj = row.Get(columnName);
				return t.IsValidObject(ValueSubstring.Create((obj != null) ? obj.ToString() : null, null, null, t, null));
			}).ToList<IType>();
			if (list.Count == 1)
			{
				return list.Single<IType>();
			}
			IType type;
			if ((type = list.Except(Seq.Of<IType>(new IType[] { UnknownType.Instance })).FirstOrDefault<IType>()) == null)
			{
				type = list.FirstOrDefault<IType>() ?? UnknownType.Instance;
			}
			return type;
		}

		// Token: 0x0600E75F RID: 59231 RVA: 0x00310E1D File Offset: 0x0030F01D
		public IImmutableList<IType> GetInputTypes(string columnName)
		{
			return this._inputColumnTypes.Value[columnName];
		}

		// Token: 0x0600E760 RID: 59232 RVA: 0x00310E30 File Offset: 0x0030F030
		public IEnumerable<OutputProvenance> ComputeOutputProvenance(IRow input)
		{
			State state = input.AsStateForExecution();
			Func<b?, bool> func = delegate(b? pred)
			{
				if (pred == null)
				{
					return true;
				}
				object obj = pred.Value.Node.Invoke(state);
				return obj != null && (bool)obj;
			};
			foreach (Branch branch in this.Branches)
			{
				if (func(branch.Predicate))
				{
					ValueSubstring valueSubstring = branch.Body.Node.Invoke(state) as ValueSubstring;
					return this.ComputeOutputProvenance(branch.Body, input, valueSubstring);
				}
			}
			return Enumerable.Empty<OutputProvenance>();
		}

		// Token: 0x0600E761 RID: 59233 RVA: 0x00310EE0 File Offset: 0x0030F0E0
		internal IEnumerable<OutputProvenance> ComputeOutputProvenance(st node, IRow input, ValueSubstring output)
		{
			return OutputProvenance.Compute(Language.Grammar, node, this._descriptionLookup.Value, input, output);
		}

		// Token: 0x1700269F RID: 9887
		// (get) Token: 0x0600E762 RID: 59234 RVA: 0x00310EFA File Offset: 0x0030F0FA
		public IReadOnlyList<TransformationDescription> AllTransformations
		{
			get
			{
				return this._descriptionLookup.Value.Descriptions;
			}
		}

		// Token: 0x0600E763 RID: 59235 RVA: 0x00310F0C File Offset: 0x0030F10C
		public override string Describe(CultureInfo cultureInfo = null)
		{
			return this._description.Value;
		}

		// Token: 0x170026A0 RID: 9888
		// (get) Token: 0x0600E764 RID: 59236 RVA: 0x00310F19 File Offset: 0x0030F119
		public IReadOnlyList<Branch> Branches
		{
			get
			{
				return this._branches.Value;
			}
		}

		// Token: 0x0600E765 RID: 59237 RVA: 0x00310F26 File Offset: 0x0030F126
		private static string HumanReadableDateTimeFormat(DateTimeFormat format)
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("\"{0}\" (e.g. \"{1}\")", new object[]
			{
				format.FormatString,
				format.ToString(Program.ExampleDateTime)
			}));
		}

		// Token: 0x0600E766 RID: 59238 RVA: 0x00310F54 File Offset: 0x0030F154
		private static string GetOrdinalString(int num)
		{
			switch (num)
			{
			case -1:
				return "last";
			case 0:
				return null;
			case 1:
				return "first";
			case 2:
				return "second";
			case 3:
				return "third";
			case 4:
				return "fourth";
			case 5:
				return "fifth";
			default:
				if (num < 0)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("{0} to last", new object[] { Program.GetOrdinalString(-num) }));
				}
				switch (num % 10)
				{
				case 0:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}th", new object[] { num }));
				case 1:
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}st", new object[] { num }));
				case 2:
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}nd", new object[] { num }));
				case 3:
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}rd", new object[] { num }));
				default:
					return null;
				}
				break;
			}
		}

		// Token: 0x0600E767 RID: 59239 RVA: 0x00311084 File Offset: 0x0030F284
		private static string DescribePosition(Microsoft.ProgramSynthesis.Transformation.Text.Description.Substring.Position pos, bool isLeftPos)
		{
			if (pos.RegexPair != null)
			{
				Microsoft.ProgramSynthesis.Transformation.Text.Description.Substring.RegexPair value = pos.RegexPair.Value;
				string ordinalString = Program.GetOrdinalString(value.MatchNum);
				if (ordinalString == null)
				{
					return null;
				}
				if (value.AfterMatch.Count == 0)
				{
					RegularExpression beforeMatch = value.BeforeMatch;
					return FormattableString.Invariant(FormattableStringFactory.Create("the{0} {1} match of the \"{2}\" regex", new object[]
					{
						isLeftPos ? "" : " end of the",
						ordinalString,
						beforeMatch
					}));
				}
				if (value.BeforeMatch.Count == 0)
				{
					RegularExpression afterMatch = value.AfterMatch;
					return FormattableString.Invariant(FormattableStringFactory.Create("the{0} {1} match of the \"{2}\" regex", new object[]
					{
						(!isLeftPos) ? "" : " start of the",
						ordinalString,
						afterMatch
					}));
				}
			}
			if (pos.AbsolutePosition == null)
			{
				return null;
			}
			string ordinalString2 = Program.GetOrdinalString(pos.AbsolutePosition.Value + ((isLeftPos > false) ? 1 : 0));
			if (ordinalString2 == null)
			{
				return null;
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("the {0} character", new object[] { ordinalString2 }));
		}

		// Token: 0x0600E768 RID: 59240 RVA: 0x003111A8 File Offset: 0x0030F3A8
		private string _Describe()
		{
			if (this.Branches.Count == 1)
			{
				return this.DescribeBranchBody(Language.Build.Node.Cast.@switch(base.ProgramNode).Cast_SingleBranch(Language.Build).st);
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = this.DescribeBranch(this.Branches[0], "if");
			if (text == null)
			{
				return null;
			}
			stringBuilder.Append(text);
			for (int i = 1; i < this.Branches.Count - 1; i++)
			{
				string text2 = this.DescribeBranch(this.Branches[i], " else if");
				if (text2 == null)
				{
					return null;
				}
				stringBuilder.Append(text2);
			}
			string text3 = this.DescribeBranch(this.Branches.Last<Branch>(), null);
			if (text3 == null)
			{
				return null;
			}
			stringBuilder.Append(text3);
			return stringBuilder.ToString();
		}

		// Token: 0x0600E769 RID: 59241 RVA: 0x00311290 File Offset: 0x0030F490
		private string DescribeBranch(Branch branch, string stmt = null)
		{
			if (branch.Predicate == null)
			{
				string text = this.DescribeBranchBody(branch.Body);
				if (text != null)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create(" else {0}", new object[] { text }));
				}
				return null;
			}
			else
			{
				string text2 = Program.DescribePredicate(branch.Predicate.Value);
				string text3 = this.DescribeBranchBody(branch.Body);
				if (text2 == null || text3 == null)
				{
					return null;
				}
				return FormattableString.Invariant(FormattableStringFactory.Create("{0} {1} then {2}", new object[] { stmt, text2, text3 }));
			}
		}

		// Token: 0x0600E76A RID: 59242 RVA: 0x00311324 File Offset: 0x0030F524
		private static string DescribePredicate(b predicate)
		{
			return PredicateProgram.DescribeConjunct(predicate.Cast_LetPredicate().pred.Cast_Predicate().conjunct);
		}

		// Token: 0x0600E76B RID: 59243 RVA: 0x00311358 File Offset: 0x0030F558
		private string DescribeBranchBody(st node)
		{
			if (node.Node.GetFeatureValue<double>(Program.HasConcatenations, null) > 0.0)
			{
				return null;
			}
			if (node.Node.GetFeatureValue<IImmutableSet<string>>(Program.InputsUsed, null).Count != 0)
			{
				if (this.ColumnsUsed.Count == 1)
				{
					string text = this.ColumnsUsed.Single<string>();
					bool flag = (int)node.Node.GetFeatureValue<double>(Program.WholeColumnCount, null) == 1;
					string text2;
					if (flag)
					{
						text2 = FormattableString.Invariant(FormattableStringFactory.Create("column \"{0}\"", new object[] { text }));
					}
					else
					{
						Microsoft.ProgramSynthesis.Transformation.Text.Description.Substring substring = this.AllTransformations.OfType<Microsoft.ProgramSynthesis.Transformation.Text.Description.Substring>().OnlyOrDefault<Microsoft.ProgramSynthesis.Transformation.Text.Description.Substring>();
						if (substring == null)
						{
							return null;
						}
						if (substring.LeftPos.RegexPair != null && substring.RightPos.RegexPair != null && substring.LeftPos.RegexPair.Value.MatchNum == substring.RightPos.RegexPair.Value.MatchNum && substring.LeftPos.RegexPair.Value.BeforeMatch.Count == 0 && substring.RightPos.RegexPair.Value.AfterMatch.Count == 0 && object.Equals(substring.LeftPos.RegexPair.Value.AfterMatch, substring.RightPos.RegexPair.Value.BeforeMatch))
						{
							RegularExpression afterMatch = substring.LeftPos.RegexPair.Value.AfterMatch;
							string ordinalString = Program.GetOrdinalString(substring.LeftPos.RegexPair.Value.MatchNum);
							if (ordinalString == null)
							{
								return null;
							}
							text2 = FormattableString.Invariant(FormattableStringFactory.Create("the {0} match of the \"{1}\" regex in column \"{2}\"", new object[] { ordinalString, afterMatch, text }));
						}
						else
						{
							string text3 = Program.DescribePosition(substring.LeftPos, true);
							string text4 = Program.DescribePosition(substring.RightPos, false);
							if (text3 == null || text4 == null)
							{
								return null;
							}
							text2 = FormattableString.Invariant(FormattableStringFactory.Create("between {0} and {1} in column \"{2}\"", new object[] { text3, text4, text }));
						}
					}
					bool flag2 = (int)node.Node.GetFeatureValue<double>(Program.FormatDateCount, null) == 1;
					Atom? atom;
					LetColumnName? letColumnName;
					LetX? letX;
					SubString? subString;
					if (this.AllTransformations.Count == 1 || ((node.Cast_Transformation().e.As_Atom(Language.Build) != null) ? ((atom.GetValueOrDefault().f.As_LetColumnName(Language.Build) != null) ? ((letColumnName.GetValueOrDefault().letOptions.As_LetX(Language.Build) != null) ? ((letX.GetValueOrDefault().conv.As_SubString(Language.Build) != null) ? new bool?(subString.GetValueOrDefault().SS.Is_WholeColumn(Language.Build)) : null) : null) : null) : null).GetValueOrDefault())
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0} {1}.", new object[]
						{
							flag ? "Copy" : "Extract",
							text2
						}));
					}
					if (flag2)
					{
						FormatDateTime formatDateTime = this.AllTransformations.OfType<FormatDateTime>().FirstOrDefault<FormatDateTime>();
						ParseDateTime parseDateTime = this.AllTransformations.OfType<ParseDateTime>().FirstOrDefault<ParseDateTime>();
						if (formatDateTime != null && parseDateTime != null)
						{
							DateTimeFormat wholeFormat = formatDateTime.WholeFormat;
							if (wholeFormat.FormatParts.Count == 1)
							{
								return FormattableString.Invariant(FormattableStringFactory.Create("Extract {0} (e.g. \"{1}\") from datetime from {2}.", new object[]
								{
									wholeFormat.FormatParts.Single<DateTimeFormatPart>().MatchedPart.Value,
									wholeFormat.ToString(Program.ExampleDateTime),
									text2
								}));
							}
							return FormattableString.Invariant(FormattableStringFactory.Create("Convert datetime from {0} to the format {1}.", new object[]
							{
								text2,
								Program.HumanReadableDateTimeFormat(wholeFormat)
							}));
						}
					}
				}
				return null;
			}
			Constant constant = this.AllTransformations.OfType<Constant>().OnlyOrDefault<Constant>();
			if (constant == null)
			{
				return null;
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("constant \"{0}\"", new object[] { constant.ConstantString }));
		}

		// Token: 0x04005814 RID: 22548
		private static readonly InputsUsed InputsUsed = new InputsUsed(Language.Grammar);

		// Token: 0x04005815 RID: 22549
		private readonly @switch _typedProgram;

		// Token: 0x04005816 RID: 22550
		private readonly Lazy<IImmutableDictionary<string, IImmutableList<IType>>> _inputColumnTypes;

		// Token: 0x04005817 RID: 22551
		private readonly Lazy<DescriptionLookup> _descriptionLookup;

		// Token: 0x04005818 RID: 22552
		private readonly Lazy<string> _description;

		// Token: 0x04005819 RID: 22553
		private readonly Lazy<IEnumerable<IType>> _outputTypes;

		// Token: 0x0400581A RID: 22554
		private readonly Lazy<IReadOnlyList<Branch>> _branches;

		// Token: 0x0400581B RID: 22555
		private readonly Lazy<@switch?> _indexInputProgramNode;

		// Token: 0x0400581C RID: 22556
		private readonly Lazy<@switch?> _inputRowProgramNode;

		// Token: 0x0400581D RID: 22557
		private readonly GrammarBuilders.Nodes.NodeVariables _var = Language.Build.Node.Variable;

		// Token: 0x0400581E RID: 22558
		private readonly GrammarBuilders.Nodes.NodeRules _rule = Language.Build.Node.Rule;

		// Token: 0x04005820 RID: 22560
		private static readonly DateTime ExampleDateTime = new DateTime(2001, 2, 3, 16, 5, 6, 789, DateTimeKind.Utc);

		// Token: 0x04005821 RID: 22561
		private static readonly Feature<double> FormatDateCount = new FormatDateCount(Language.Grammar);

		// Token: 0x04005822 RID: 22562
		private static readonly Feature<double> WholeColumnCount = new WholeColumnCount(Language.Grammar);

		// Token: 0x04005823 RID: 22563
		private static readonly Feature<double> HasConcatenations = new HasConcatenations(Language.Grammar);

		// Token: 0x02001B9B RID: 7067
		internal class BranchCollector : ProgramNodeVisitor<IEnumerable<Branch>>
		{
			// Token: 0x0600E76F RID: 59247 RVA: 0x003118D8 File Offset: 0x0030FAD8
			private BranchCollector()
			{
			}

			// Token: 0x170026A1 RID: 9889
			// (get) Token: 0x0600E770 RID: 59248 RVA: 0x003118E0 File Offset: 0x0030FAE0
			public static Program.BranchCollector Instance { get; } = new Program.BranchCollector();

			// Token: 0x0600E771 RID: 59249 RVA: 0x003118E8 File Offset: 0x0030FAE8
			public override IEnumerable<Branch> VisitNonterminal(NonterminalNode node)
			{
				IfThenElse ifThenElse;
				if (Language.Build.Node.IsRule.IfThenElse(node, out ifThenElse))
				{
					return ifThenElse.@switch.Node.AcceptVisitor<IEnumerable<Branch>>(this).PrependItem(new Branch(new b?(ifThenElse.b), ifThenElse.st));
				}
				SingleBranch singleBranch;
				if (Language.Build.Node.IsRule.SingleBranch(node, out singleBranch))
				{
					return singleBranch.st.Node.AcceptVisitor<IEnumerable<Branch>>(this);
				}
				switch_ite switch_ite;
				if (Language.Build.Node.IsRule.switch_ite(node, out switch_ite))
				{
					return switch_ite.ite.Node.AcceptVisitor<IEnumerable<Branch>>(this);
				}
				Transformation transformation;
				if (Language.Build.Node.IsRule.Transformation(node, out transformation))
				{
					return new Branch[]
					{
						new Branch(null, transformation)
					};
				}
				return Enumerable.Empty<Branch>();
			}

			// Token: 0x0600E772 RID: 59250 RVA: 0x003119DF File Offset: 0x0030FBDF
			public override IEnumerable<Branch> VisitLet(LetNode node)
			{
				return Enumerable.Empty<Branch>();
			}

			// Token: 0x0600E773 RID: 59251 RVA: 0x003119DF File Offset: 0x0030FBDF
			public override IEnumerable<Branch> VisitLambda(LambdaNode node)
			{
				return Enumerable.Empty<Branch>();
			}

			// Token: 0x0600E774 RID: 59252 RVA: 0x003119DF File Offset: 0x0030FBDF
			public override IEnumerable<Branch> VisitLiteral(LiteralNode node)
			{
				return Enumerable.Empty<Branch>();
			}

			// Token: 0x0600E775 RID: 59253 RVA: 0x003119DF File Offset: 0x0030FBDF
			public override IEnumerable<Branch> VisitVariable(VariableNode node)
			{
				return Enumerable.Empty<Branch>();
			}

			// Token: 0x0600E776 RID: 59254 RVA: 0x003119DF File Offset: 0x0030FBDF
			public override IEnumerable<Branch> VisitHole(Hole node)
			{
				return Enumerable.Empty<Branch>();
			}
		}

		// Token: 0x02001B9C RID: 7068
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005825 RID: 22565
			public static Func<ProgramNode, ProgramNode> <0>__Rewrite;

			// Token: 0x04005826 RID: 22566
			public static Func<ProgramNode, RewriteRule, ProgramNode> <1>__Rewrite;

			// Token: 0x04005827 RID: 22567
			public static Func<string, int, KeyValuePair<string, int>> <2>__Create;
		}
	}
}
