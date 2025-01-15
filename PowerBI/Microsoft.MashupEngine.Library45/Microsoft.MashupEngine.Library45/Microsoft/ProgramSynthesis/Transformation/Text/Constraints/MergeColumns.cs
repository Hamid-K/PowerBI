using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Constraints
{
	// Token: 0x02001DE7 RID: 7655
	public class MergeColumns : Constraint<IRow, object>, IOptionConstraint<Witnesses.Options>, IEquatable<MergeColumns>
	{
		// Token: 0x06010092 RID: 65682 RVA: 0x003712B0 File Offset: 0x0036F4B0
		[JsonConstructor]
		private MergeColumns(IEnumerable<string> columns = null, string separator = null, IEnumerable<string> constants = null, bool isSoft = false)
		{
			this.Separator = separator;
			this.IsSoft = isSoft;
			this._columns = ((columns != null) ? columns.ToImmutableList<string>() : null);
			this._constants = ((constants != null) ? constants.ToImmutableList<string>() : null);
			ImmutableList<string> columns2 = this._columns;
			int? num = ((columns2 != null) ? new int?(columns2.Distinct<string>().Count<string>()) : null);
			ImmutableList<string> columns3 = this._columns;
			int? num2 = ((columns3 != null) ? new int?(columns3.Count) : null);
			if (!((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))))
			{
				throw new ArgumentException("MergeColumns can only merge distinct columns.", "columns");
			}
			ImmutableList<string> columns4 = this._columns;
			if (columns4 != null && columns4.Count < 2)
			{
				throw new ArgumentException("MergeColumns only makes sense with multiple columns.", "columns");
			}
			if (this._constants != null && this._constants.Count < 3)
			{
				throw new ArgumentException("MergeColumns only makes sense with multiple columns, which requires at least 3 constants.", "constants");
			}
			if (this.Separator == null && this._constants != null && this._constants.First<string>() == string.Empty && this._constants.Last<string>() == string.Empty)
			{
				this.Separator = this._constants.Skip(1).DropLast<string>().Distinct<string>()
					.OnlyOrDefault<string>();
			}
			if (this._columns != null && this._constants == null && this.Separator != null)
			{
				this._constants = Enumerable.Repeat<string>(this.Separator, this._columns.Count - 1).PrependItem(string.Empty).AppendItem(string.Empty)
					.ToImmutableList<string>();
			}
		}

		// Token: 0x06010093 RID: 65683 RVA: 0x003714A8 File Offset: 0x0036F6A8
		public MergeColumns(IEnumerable<string> columns = null, string separator = null, bool isSoft = false)
			: this(columns, separator, null, isSoft)
		{
		}

		// Token: 0x06010094 RID: 65684 RVA: 0x003714B4 File Offset: 0x0036F6B4
		public MergeColumns(IEnumerable<string> columns, IEnumerable<string> constants, bool isSoft = false)
			: this(columns, null, constants, isSoft)
		{
		}

		// Token: 0x17002A94 RID: 10900
		// (get) Token: 0x06010095 RID: 65685 RVA: 0x003714C0 File Offset: 0x0036F6C0
		public string Separator { get; }

		// Token: 0x17002A95 RID: 10901
		// (get) Token: 0x06010096 RID: 65686 RVA: 0x003714C8 File Offset: 0x0036F6C8
		public IEnumerable<string> Constants
		{
			get
			{
				return this._constants;
			}
		}

		// Token: 0x17002A96 RID: 10902
		// (get) Token: 0x06010097 RID: 65687 RVA: 0x003714D0 File Offset: 0x0036F6D0
		public override bool IsSoft { get; }

		// Token: 0x17002A97 RID: 10903
		// (get) Token: 0x06010098 RID: 65688 RVA: 0x003714D8 File Offset: 0x0036F6D8
		public IEnumerable<string> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17002A98 RID: 10904
		// (get) Token: 0x06010099 RID: 65689 RVA: 0x003714E0 File Offset: 0x0036F6E0
		private IEnumerable<f> ColumnProgramNodes
		{
			get
			{
				ImmutableList<string> columns = this._columns;
				if (columns == null)
				{
					return null;
				}
				return columns.Select((string c) => this._rule.LetColumnName(this._rule.idx(c), this._rule.LetX(this._rule.ChooseInput(this._var.vs, this._var.columnName), this._rule.SubString(this._rule.WholeColumn(this._var.x)))));
			}
		}

		// Token: 0x0601009A RID: 65690 RVA: 0x00371500 File Offset: 0x0036F700
		public bool Equals(MergeColumns other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (!object.Equals(this.Separator, other.Separator))
			{
				return false;
			}
			if (this._constants != null != (other._constants != null))
			{
				return false;
			}
			if (this._constants != null && !this._constants.SequenceEqual(other._constants))
			{
				return false;
			}
			if (this._columns == null)
			{
				return other._columns == null;
			}
			return other._columns != null && this._columns.SequenceEqual(other._columns);
		}

		// Token: 0x0601009B RID: 65691 RVA: 0x00371590 File Offset: 0x0036F790
		public void SetOptions(Witnesses.Options options)
		{
			options.AllowedTransformations = TransformationKind.Constant | TransformationKind.Concat | TransformationKind.WholeColumn;
			options.LookupFallbackMode = LookupFallbackMode.Never;
			if (options.RequiredColumns.Any<string>())
			{
				throw new ArgumentException("Options may not already have RequireColumns set.", "options");
			}
			if (this._columns != null)
			{
				options.RequiredColumns = this._columns.ToImmutableHashSet<string>();
			}
		}

		// Token: 0x0601009C RID: 65692 RVA: 0x003715E4 File Offset: 0x0036F7E4
		public MergeColumns CombineWith(MergeColumns other)
		{
			if (other == null || this.ConflictsWith(other))
			{
				return null;
			}
			if (this._constants != null || other._constants != null)
			{
				return new MergeColumns(this.Columns ?? other.Columns, this._constants ?? other._constants, this.IsSoft && other.IsSoft);
			}
			return new MergeColumns(this.Columns ?? other.Columns, this.Separator ?? other.Separator, this.IsSoft && other.IsSoft);
		}

		// Token: 0x0601009D RID: 65693 RVA: 0x00371684 File Offset: 0x0036F884
		public override bool ConflictsWith(Constraint<IRow, object> other)
		{
			MergeColumns mergeColumns = other as MergeColumns;
			if (mergeColumns != null)
			{
				return (this._columns != null && mergeColumns._columns != null && !this._columns.SequenceEqual(mergeColumns._columns)) || (this.Separator != null && mergeColumns.Separator != null && !this.Separator.Equals(mergeColumns.Separator)) || (this._constants != null && mergeColumns._constants != null && !this._constants.SequenceEqual(mergeColumns._constants)) || (this._constants != null && mergeColumns.Separator != null) || (mergeColumns._constants != null && this.Separator != null);
			}
			OutputIs<IRow, object> outputIs = other as OutputIs<IRow, object>;
			if (outputIs != null)
			{
				return !outputIs.Type.Equals(UnknownType.Instance);
			}
			Example<IRow, object> example = other as Example<IRow, object>;
			if (!(example != null) || this._columns == null)
			{
				return false;
			}
			string text = example.Output as string;
			if (text == null)
			{
				return false;
			}
			IReadOnlyList<string> readOnlyList = this._columns.Select(delegate(string columnName)
			{
				ValueSubstring valueSubstring = Semantics.ChooseInput(example.Input, columnName);
				if (valueSubstring == null)
				{
					return null;
				}
				return valueSubstring.Value;
			}).ToList<string>();
			if (readOnlyList.Any((string i) => i == null))
			{
				return false;
			}
			if (this.Separator != null)
			{
				return string.Join(this.Separator, readOnlyList) != text;
			}
			if (this._constants != null)
			{
				return string.Concat(this._constants.Interleave(readOnlyList)) != text;
			}
			int num = 0;
			foreach (string text2 in readOnlyList)
			{
				int num2 = text.IndexOf(text2, num, StringComparison.Ordinal);
				if (num2 == -1)
				{
					return true;
				}
				num = num2 + text2.Length;
			}
			return false;
		}

		// Token: 0x0601009E RID: 65694 RVA: 0x00371890 File Offset: 0x0036FA90
		public override bool Equals(Constraint<IRow, object> other)
		{
			return this.Equals(other as MergeColumns);
		}

		// Token: 0x0601009F RID: 65695 RVA: 0x0037189E File Offset: 0x0036FA9E
		public override int GetHashCode()
		{
			int num = 7352537;
			string separator = this.Separator;
			int num2 = num ^ ((separator != null) ? separator.GetHashCode() : 0);
			ImmutableList<string> columns = this._columns;
			int num3 = num2 ^ ((columns != null) ? columns.OrderIndependentHashCode<string>() : 0);
			ImmutableList<string> constants = this._constants;
			return num3 ^ ((constants != null) ? constants.OrderIndependentHashCode<string>() : 0);
		}

		// Token: 0x060100A0 RID: 65696 RVA: 0x003718E0 File Offset: 0x0036FAE0
		public override bool Valid(Program<IRow, object> program)
		{
			Program program2 = program as Program;
			if (program2 == null)
			{
				return false;
			}
			if (program2.ColumnsUsed.Count < 2)
			{
				return false;
			}
			if (this._columns != null && !program2.ColumnsUsed.ConvertToHashSet<string>().SetEquals(this._columns))
			{
				return false;
			}
			if (this._constants != null)
			{
				return MergeColumns.ConstantsUsed.Compute(program.ProgramNode).SequenceEqual(this._constants);
			}
			return (this.Separator == null || !(program2.ProgramNode.AcceptVisitor<string>(new MergeColumns.SingleSeparatorUsed()) != this.Separator)) && program2.ProgramNode.AcceptVisitor<IImmutableSet<string>>(new MergeColumns.DistinctWholeColumnsUsed()) != null;
		}

		// Token: 0x060100A1 RID: 65697 RVA: 0x0037198C File Offset: 0x0036FB8C
		public override string ToString()
		{
			string text = "merge columns";
			string text2 = ((this.Separator == null) ? "" : FormattableString.Invariant(FormattableStringFactory.Create(" with \"{0}\"", new object[] { this.Separator })));
			string text3;
			if (this._columns != null)
			{
				text3 = " {" + string.Join(", ", this._columns.Select((string c) => FormattableString.Invariant(FormattableStringFactory.Create("\"{0}\"", new object[] { c })))) + "}";
			}
			else
			{
				text3 = "";
			}
			return text + text2 + text3;
		}

		// Token: 0x060100A2 RID: 65698 RVA: 0x00371A24 File Offset: 0x0036FC24
		private Optional<ProgramSetBuilder<e>> BuildASTs(LearningTask<ExampleSpec> task, MergeColumns.IColumnsProvider columns, bool mayBeJustConstant, bool isFirst, ImmutableList<string> remainingConstants, CancellationToken cancel)
		{
			cancel.ThrowIfCancellationRequested();
			if (remainingConstants != null && remainingConstants.IsEmpty)
			{
				return Optional<ProgramSetBuilder<e>>.Nothing;
			}
			ExampleSpec spec = task.Spec;
			var hashSet = new <87edbf4c-9528-4307-aaf5-1c71002bfc76><>f__AnonymousType0<string, string>[]
			{
				new
				{
					column = string.Empty,
					constant = string.Empty
				}
			}.ConvertToHashSet();
			hashSet.Clear();
			string text = ((isFirst && this.Separator != null) ? "" : (this.Separator ?? ((remainingConstants != null) ? remainingConstants.First<string>() : null)));
			Optional<KeyValuePair<State, object>> optional = spec.Examples.MaybeFirst((KeyValuePair<State, object> ex) => ex.Value != null);
			if (!optional.HasValue)
			{
				return Optional<ProgramSetBuilder<e>>.Nothing;
			}
			KeyValuePair<State, object> value = optional.Value;
			string text2 = (value.Value as string) ?? ((ValueSubstring)value.Value).Value;
			if (text != null && !text2.StartsWith(text, StringComparison.Ordinal))
			{
				return Optional<ProgramSetBuilder<e>>.Nothing;
			}
			foreach (string text3 in columns.NextColumnPossibilities)
			{
				ValueSubstring valueSubstring = Semantics.ChooseInput((IRow)value.Key[Language.Grammar.InputSymbol], text3);
				string text4 = ((valueSubstring != null) ? valueSubstring.Value : null);
				if (text4 != null)
				{
					if (text != null)
					{
						if (text2.StartsWith(text + text4, StringComparison.Ordinal))
						{
							hashSet.Add(new
							{
								column = text3,
								constant = text
							});
						}
					}
					else
					{
						int num;
						for (int i = 0; i <= text2.Length - text4.Length; i = num + 1)
						{
							num = text2.IndexOf(text4, i, StringComparison.Ordinal);
							if (num == -1)
							{
								break;
							}
							string text5 = text2.Substring(0, num);
							hashSet.Add(new
							{
								column = text3,
								constant = text5
							});
						}
					}
				}
			}
			if (mayBeJustConstant && (text == null || (this.Separator == null && string.Equals(text, text2, StringComparison.Ordinal))))
			{
				hashSet.Add(new
				{
					column = null,
					constant = text2
				});
			}
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				if (!hashSet.Any())
				{
					break;
				}
				string text6;
				if ((text6 = keyValuePair.Value as string) == null)
				{
					ValueSubstring valueSubstring2 = (ValueSubstring)keyValuePair.Value;
					text6 = ((valueSubstring2 != null) ? valueSubstring2.Value : null);
				}
				string text7 = text6;
				if (text7 == null)
				{
					hashSet.RemoveWhere(o => o.column == null);
				}
				else
				{
					foreach (var grouping in from o in hashSet
						group o by o.column)
					{
						string key = grouping.Key;
						string text8;
						if (key != null)
						{
							ValueSubstring valueSubstring3 = Semantics.ChooseInput((IRow)keyValuePair.Key[Language.Grammar.InputSymbol], key);
							text8 = ((valueSubstring3 != null) ? valueSubstring3.Value : null);
						}
						else
						{
							text8 = null;
						}
						string text9 = text8;
						foreach (var <87edbf4c-9528-4307-aaf5-1c71002bfc76><>f__AnonymousType in grouping)
						{
							string constant = <87edbf4c-9528-4307-aaf5-1c71002bfc76><>f__AnonymousType.constant;
							if ((key == null && !string.Equals(text7, constant, StringComparison.Ordinal)) || (key != null && text9 == null) || (key != null && !text7.StartsWith(constant + text9, StringComparison.Ordinal)))
							{
								hashSet.Remove(<87edbf4c-9528-4307-aaf5-1c71002bfc76><>f__AnonymousType);
							}
						}
					}
				}
			}
			GrammarBuilders.Sets set = Language.Build.Set;
			GrammarBuilders.Nodes.NodeRules rule = Language.Build.Node.Rule;
			GrammarBuilders.Nodes.NodeVariables variable = Language.Build.Node.Variable;
			ImmutableList<string> immutableList = ((remainingConstants != null) ? remainingConstants.RemoveAt(0) : null);
			MultiValueDictionary<string, ProgramSetBuilder<e>> multiValueDictionary = new MultiValueDictionary<string, ProgramSetBuilder<e>>();
			using (var enumerator5 = hashSet.GetEnumerator())
			{
				while (enumerator5.MoveNext())
				{
					<87edbf4c-9528-4307-aaf5-1c71002bfc76><>f__AnonymousType0<string, string> obj = enumerator5.Current;
					if (obj.column != null)
					{
						Dictionary<State, object> dictionary = spec.Examples.SelectMany(delegate(KeyValuePair<State, object> example)
						{
							string text11;
							if ((text11 = example.Value as string) == null)
							{
								ValueSubstring valueSubstring4 = (ValueSubstring)example.Value;
								text11 = ((valueSubstring4 != null) ? valueSubstring4.Value : null);
							}
							string text12 = text11;
							ValueSubstring valueSubstring5 = Semantics.ChooseInput((IRow)example.Key[Language.Grammar.InputSymbol], obj.column);
							string text13 = ((valueSubstring5 != null) ? valueSubstring5.Value : null);
							if (text13 == null)
							{
								return Enumerable.Empty<KeyValuePair<State, object>>();
							}
							return Seq.Of<KeyValuePair<State, object>>(new KeyValuePair<State, object>[] { KVP.Create<State, object>(example.Key, (text12 != null) ? text12.Substring(obj.constant.Length + text13.Length) : null) });
						}).ToDictionary<State, object>();
						f f = rule.LetColumnName(rule.idx(obj.column), rule.LetX(rule.ChooseInput(variable.vs, variable.columnName), rule.SubString(rule.WholeColumn(variable.x))));
						MergeColumns.IColumnsProvider columnsProvider = columns.Remove(obj.column);
						if (!isFirst)
						{
							if (dictionary.All((KeyValuePair<State, object> ex) => ex.Value as string == string.Empty) && (immutableList == null || immutableList.OnlyOrDefault<string>() == string.Empty) && !columnsProvider.MustUseMoreColumns)
							{
								multiValueDictionary.Add(obj.constant, ProgramSetBuilder.List<e>(new e[] { rule.Atom(f) }));
								continue;
							}
						}
						Optional<ProgramSetBuilder<e>> optional2 = this.BuildASTs(task.Clone(null, new ExampleSpec(dictionary)).Cast<ExampleSpec>(), columnsProvider, !isFirst && ((immutableList != null) ? immutableList.Count : 1) == 1 && !columnsProvider.MustUseMoreColumns, false, immutableList, cancel);
						if (optional2.HasValue)
						{
							multiValueDictionary.Add(obj.constant, set.Join.Concat(ProgramSetBuilder.List<f>(new f[] { f }), optional2.Value));
						}
					}
					else
					{
						if (obj.constant == string.Empty)
						{
							throw new Exception("null programset for empty constant should never happen");
						}
						multiValueDictionary.Add(obj.constant, null);
					}
				}
			}
			foreach (string text10 in multiValueDictionary.Keys.Where((string k) => !string.IsNullOrEmpty(k)).ToList<string>())
			{
				ProgramSetBuilder<e> programSetBuilder = multiValueDictionary[text10].NormalizedUnion<e>();
				ProgramSetBuilder<f> programSetBuilder2 = ProgramSetBuilder.List<f>(new f[] { rule.ConstStr(rule.s(text10)) });
				if (multiValueDictionary[text10].Contains(null))
				{
					multiValueDictionary.Add(string.Empty, set.Join.Atom(programSetBuilder2));
				}
				if (!ProgramSetBuilder.IsNullOrEmpty<e>(programSetBuilder))
				{
					multiValueDictionary.Add(string.Empty, set.Join.Concat(programSetBuilder2, programSetBuilder));
				}
			}
			if (multiValueDictionary.ContainsKey(string.Empty))
			{
				return multiValueDictionary[string.Empty].NormalizedUnion<e>().Some<ProgramSetBuilder<e>>();
			}
			return Optional<ProgramSetBuilder<e>>.Nothing;
		}

		// Token: 0x060100A3 RID: 65699 RVA: 0x003721B0 File Offset: 0x003703B0
		internal ProgramSetBuilder<e> BuildASTs(LearningTask task, IEnumerable<string> columnNames, CancellationToken cancel)
		{
			cancel.ThrowIfCancellationRequested();
			if (task.Spec is ExampleSpec)
			{
				LearningTask<ExampleSpec> learningTask = task.Cast<ExampleSpec>();
				MergeColumns.IColumnsProvider columnsProvider2;
				if (this._columns == null)
				{
					MergeColumns.IColumnsProvider columnsProvider = new MergeColumns.UnorderedColumnsProvider(columnNames);
					columnsProvider2 = columnsProvider;
				}
				else
				{
					MergeColumns.IColumnsProvider columnsProvider = new MergeColumns.OrderedColumnsProvider(this._columns);
					columnsProvider2 = columnsProvider;
				}
				return this.BuildASTs(learningTask, columnsProvider2, false, true, this._constants, cancel).OrElseDefault<ProgramSetBuilder<e>>();
			}
			if (this._columns == null)
			{
				return null;
			}
			IEnumerable<f> columnProgramNodes = this.ColumnProgramNodes;
			e? e2 = this.BuildConstantsProgram();
			if (e2 != null)
			{
				return ProgramSetBuilder.List<e>(Language.Build.Symbol.e, new e[] { e2.Value });
			}
			IReadOnlyList<string> readOnlyList;
			if (this.Separator != null)
			{
				readOnlyList = new List<string> { this.Separator };
			}
			else
			{
				HashSet<Record<string, string>> hashSet = MergeColumns.Separators.Select(delegate(string sep)
				{
					string text2 = sep.Trim();
					return Record.Create<string, string>(sep, (text2.Length == 0) ? sep : text2);
				}).ConvertToHashSet<Record<string, string>>();
				foreach (State state in task.ProvidedInputs)
				{
					IRow row = (IRow)state[Language.Grammar.InputSymbol];
					foreach (string text in this._columns)
					{
						ValueSubstring cell = Semantics.ChooseInput(row, text);
						if (!(cell == null))
						{
							hashSet.ExceptWith(hashSet.Where((Record<string, string> s) => cell.Value.Contains(s.Item2)).ToList<Record<string, string>>());
						}
					}
				}
				readOnlyList = MergeColumns.Separators.Intersect(hashSet.Select2((string sep, string _) => sep)).AppendItem("").ToList<string>();
			}
			ProgramSetBuilder<f> separatorsPSet = this._joinRule.ConstStr(ProgramSetBuilder.List<s>(Language.Build.Symbol.s, (from sep in readOnlyList.Except(new string[] { "" })
				select this._rule.s(sep)).ToArray<s>()));
			List<ProgramSetBuilder<f>> list = columnProgramNodes.Select((f n) => ProgramSetBuilder.List<f>(new f[] { n })).Reverse<ProgramSetBuilder<f>>().ToList<ProgramSetBuilder<f>>();
			bool flag = readOnlyList.Contains("");
			return new ProgramSetBuilder<e>[]
			{
				list.AggregateSeedFunc(new Func<ProgramSetBuilder<f>, ProgramSetBuilder<e>>(this._joinRule.Atom), (ProgramSetBuilder<e> e, ProgramSetBuilder<f> f) => this._joinRule.Concat(f, this._joinRule.Concat(separatorsPSet, e))),
				flag ? list.AggregateSeedFunc(new Func<ProgramSetBuilder<f>, ProgramSetBuilder<e>>(this._joinRule.Atom), (ProgramSetBuilder<e> e, ProgramSetBuilder<f> f) => this._joinRule.Concat(f, e)) : null
			}.NormalizedUnion<e>();
		}

		// Token: 0x060100A4 RID: 65700 RVA: 0x003724C0 File Offset: 0x003706C0
		private e? BuildConstantsProgram()
		{
			ImmutableList<string> constants = this._constants;
			IEnumerable<f?> enumerable = ((constants != null) ? constants.Select(delegate(string c)
			{
				if (!string.IsNullOrEmpty(c))
				{
					return new f?(this._rule.ConstStr(this._rule.s(c)));
				}
				return null;
			}) : null);
			if (enumerable == null)
			{
				return null;
			}
			return new e?((from pset in enumerable.Interleave(this.ColumnProgramNodes.Select((f p) => new f?(p)))
				where pset != null
				select pset.Value).Reverse<f>().AggregateSeedFunc(new Func<f, e>(this._rule.Atom), (e e, f f) => this._rule.Concat(f, e)));
		}

		// Token: 0x04006082 RID: 24706
		private static readonly string[] Separators = new string[] { " ", ", ", "; ", "|" };

		// Token: 0x04006083 RID: 24707
		private readonly GrammarBuilders.Nodes.NodeRules _rule = Language.Build.Node.Rule;

		// Token: 0x04006084 RID: 24708
		private readonly GrammarBuilders.Nodes.NodeVariables _var = Language.Build.Node.Variable;

		// Token: 0x04006085 RID: 24709
		private readonly GrammarBuilders.Sets.Joins _joinRule = Language.Build.Set.Join;

		// Token: 0x04006086 RID: 24710
		private readonly ImmutableList<string> _columns;

		// Token: 0x04006087 RID: 24711
		private readonly ImmutableList<string> _constants;

		// Token: 0x02001DE8 RID: 7656
		private interface IColumnsProvider
		{
			// Token: 0x17002A99 RID: 10905
			// (get) Token: 0x060100A9 RID: 65705
			IEnumerable<string> NextColumnPossibilities { get; }

			// Token: 0x060100AA RID: 65706
			MergeColumns.IColumnsProvider Remove(string columnName);

			// Token: 0x17002A9A RID: 10906
			// (get) Token: 0x060100AB RID: 65707
			bool MustUseMoreColumns { get; }
		}

		// Token: 0x02001DE9 RID: 7657
		private class UnorderedColumnsProvider : MergeColumns.IColumnsProvider
		{
			// Token: 0x060100AC RID: 65708 RVA: 0x0037268A File Offset: 0x0037088A
			private UnorderedColumnsProvider(ImmutableHashSet<string> unordered)
			{
				this._unordered = unordered;
			}

			// Token: 0x060100AD RID: 65709 RVA: 0x00372699 File Offset: 0x00370899
			public UnorderedColumnsProvider(IEnumerable<string> unordered)
				: this((unordered as ImmutableHashSet<string>) ?? unordered.ToImmutableHashSet<string>())
			{
			}

			// Token: 0x17002A9B RID: 10907
			// (get) Token: 0x060100AE RID: 65710 RVA: 0x003726B1 File Offset: 0x003708B1
			public IEnumerable<string> NextColumnPossibilities
			{
				get
				{
					return this._unordered;
				}
			}

			// Token: 0x060100AF RID: 65711 RVA: 0x003726B9 File Offset: 0x003708B9
			public MergeColumns.IColumnsProvider Remove(string columnName)
			{
				return new MergeColumns.UnorderedColumnsProvider(this._unordered.Remove(columnName));
			}

			// Token: 0x17002A9C RID: 10908
			// (get) Token: 0x060100B0 RID: 65712 RVA: 0x0000FA11 File Offset: 0x0000DC11
			public bool MustUseMoreColumns
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400608A RID: 24714
			private readonly ImmutableHashSet<string> _unordered;
		}

		// Token: 0x02001DEA RID: 7658
		private class OrderedColumnsProvider : MergeColumns.IColumnsProvider
		{
			// Token: 0x060100B1 RID: 65713 RVA: 0x003726CC File Offset: 0x003708CC
			public OrderedColumnsProvider(ImmutableList<string> ordered)
			{
				this._ordered = ordered;
			}

			// Token: 0x17002A9D RID: 10909
			// (get) Token: 0x060100B2 RID: 65714 RVA: 0x003726DB File Offset: 0x003708DB
			public IEnumerable<string> NextColumnPossibilities
			{
				get
				{
					return this._ordered.Take(1);
				}
			}

			// Token: 0x060100B3 RID: 65715 RVA: 0x003726E9 File Offset: 0x003708E9
			public MergeColumns.IColumnsProvider Remove(string columnName)
			{
				return new MergeColumns.OrderedColumnsProvider(this._ordered.RemoveAt(0));
			}

			// Token: 0x17002A9E RID: 10910
			// (get) Token: 0x060100B4 RID: 65716 RVA: 0x003726FC File Offset: 0x003708FC
			public bool MustUseMoreColumns
			{
				get
				{
					return !this._ordered.IsEmpty;
				}
			}

			// Token: 0x0400608B RID: 24715
			private readonly ImmutableList<string> _ordered;
		}

		// Token: 0x02001DEB RID: 7659
		private class ConstantsUsed : ProgramNodeVisitor<IImmutableList<string>>
		{
			// Token: 0x060100B5 RID: 65717 RVA: 0x0037270C File Offset: 0x0037090C
			private ConstantsUsed()
			{
			}

			// Token: 0x060100B6 RID: 65718 RVA: 0x00372714 File Offset: 0x00370914
			public static IImmutableList<string> Compute(ProgramNode node)
			{
				ImmutableList<string> immutableList = node.AcceptVisitor<IImmutableList<string>>(MergeColumns.ConstantsUsed.Instance).Aggregate(ImmutableList<string>.Empty, delegate(ImmutableList<string> constantsSoFar, string nextConstant)
				{
					bool flag = constantsSoFar.LastOrDefault<string>() == null;
					bool flag2 = nextConstant == null;
					if (flag && flag2)
					{
						return constantsSoFar.Add("").Add(null);
					}
					if (flag || flag2)
					{
						return constantsSoFar.Add(nextConstant);
					}
					return constantsSoFar.RemoveAt(constantsSoFar.Count - 1).Add(constantsSoFar.Last<string>() + nextConstant);
				});
				if (immutableList.LastOrDefault<string>() == null)
				{
					immutableList = immutableList.Add("");
				}
				return immutableList.Where((string c) => c != null).ToImmutableList<string>();
			}

			// Token: 0x060100B7 RID: 65719 RVA: 0x00372794 File Offset: 0x00370994
			public override IImmutableList<string> VisitNonterminal(NonterminalNode node)
			{
				Concat concat;
				if (Language.Build.Node.IsRule.Concat(node, out concat))
				{
					return concat.f.Node.AcceptVisitor<IImmutableList<string>>(this).AddRange(concat.e.Node.AcceptVisitor<IImmutableList<string>>(this));
				}
				if (node.GrammarRule == Language.Build.Rule.LetColumnName)
				{
					return ImmutableList<string>.Empty.Add(null);
				}
				ConstStr constStr;
				if (Language.Build.Node.IsRule.ConstStr(node, out constStr))
				{
					return constStr.s.Node.AcceptVisitor<IImmutableList<string>>(this);
				}
				if (node.GrammarRule is ConversionRule)
				{
					return node.Children[0].AcceptVisitor<IImmutableList<string>>(this);
				}
				string text = "Unexpected grammar rule: ";
				GrammarRule grammarRule = node.GrammarRule;
				throw new NotImplementedException(text + ((grammarRule != null) ? grammarRule.ToString() : null) + ", node=" + ((node != null) ? node.ToString() : null));
			}

			// Token: 0x060100B8 RID: 65720 RVA: 0x0037288C File Offset: 0x00370A8C
			public override IImmutableList<string> VisitLet(LetNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x060100B9 RID: 65721 RVA: 0x0037288C File Offset: 0x00370A8C
			public override IImmutableList<string> VisitLambda(LambdaNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x060100BA RID: 65722 RVA: 0x00372898 File Offset: 0x00370A98
			public override IImmutableList<string> VisitLiteral(LiteralNode node)
			{
				s? s = Language.Build.Node.As.s(node);
				if (s != null)
				{
					return ImmutableList<string>.Empty.Add(s.Value.Value);
				}
				return ImmutableList<string>.Empty;
			}

			// Token: 0x060100BB RID: 65723 RVA: 0x003728E3 File Offset: 0x00370AE3
			public override IImmutableList<string> VisitVariable(VariableNode node)
			{
				return ImmutableList<string>.Empty;
			}

			// Token: 0x060100BC RID: 65724 RVA: 0x003728E3 File Offset: 0x00370AE3
			public override IImmutableList<string> VisitHole(Hole node)
			{
				return ImmutableList<string>.Empty;
			}

			// Token: 0x0400608C RID: 24716
			private static readonly MergeColumns.ConstantsUsed Instance = new MergeColumns.ConstantsUsed();
		}

		// Token: 0x02001DED RID: 7661
		private class DistinctWholeColumnsUsed : ProgramNodeVisitor<IImmutableSet<string>>
		{
			// Token: 0x060100C2 RID: 65730 RVA: 0x00372964 File Offset: 0x00370B64
			public override IImmutableSet<string> VisitNonterminal(NonterminalNode node)
			{
				LetColumnName letColumnName;
				if (!Language.Build.Node.IsRule.LetColumnName(node, out letColumnName))
				{
					return node.Children.Select((ProgramNode a) => a.AcceptVisitor<IImmutableSet<string>>(this)).Aggregate(delegate(IImmutableSet<string> a, IImmutableSet<string> b)
					{
						if (b != null && a != null && !b.Overlaps(a))
						{
							return a.Union(b);
						}
						return null;
					});
				}
				if (!letColumnName.letOptions.Is_LetX(Language.Build))
				{
					return null;
				}
				IImmutableSet<string> featureValue = node.GetFeatureValue<IImmutableSet<string>>(MergeColumns.DistinctWholeColumnsUsed.WholeColumnsUsed, null);
				IImmutableSet<string> featureValue2 = node.GetFeatureValue<IImmutableSet<string>>(MergeColumns.DistinctWholeColumnsUsed.InputsUsed, null);
				if (!featureValue.SetEquals(featureValue2))
				{
					return null;
				}
				return featureValue;
			}

			// Token: 0x060100C3 RID: 65731 RVA: 0x00372A04 File Offset: 0x00370C04
			public override IImmutableSet<string> VisitLet(LetNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x060100C4 RID: 65732 RVA: 0x00372A04 File Offset: 0x00370C04
			public override IImmutableSet<string> VisitLambda(LambdaNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x060100C5 RID: 65733 RVA: 0x0034C0A2 File Offset: 0x0034A2A2
			public override IImmutableSet<string> VisitLiteral(LiteralNode node)
			{
				return ImmutableHashSet<string>.Empty;
			}

			// Token: 0x060100C6 RID: 65734 RVA: 0x0034C0A2 File Offset: 0x0034A2A2
			public override IImmutableSet<string> VisitVariable(VariableNode node)
			{
				return ImmutableHashSet<string>.Empty;
			}

			// Token: 0x060100C7 RID: 65735 RVA: 0x0034C0A2 File Offset: 0x0034A2A2
			public override IImmutableSet<string> VisitHole(Hole node)
			{
				return ImmutableHashSet<string>.Empty;
			}

			// Token: 0x04006090 RID: 24720
			private static readonly InputsUsed InputsUsed = new InputsUsed(Language.Grammar);

			// Token: 0x04006091 RID: 24721
			private static readonly WholeColumnUsed WholeColumnUsed = new WholeColumnUsed(Language.Grammar);

			// Token: 0x04006092 RID: 24722
			private static readonly WholeColumnsUsed WholeColumnsUsed = new WholeColumnsUsed(Language.Grammar, MergeColumns.DistinctWholeColumnsUsed.WholeColumnUsed, MergeColumns.DistinctWholeColumnsUsed.InputsUsed);
		}

		// Token: 0x02001DEF RID: 7663
		private class SingleSeparatorUsed : ProgramNodeVisitor<string>
		{
			// Token: 0x060100CE RID: 65742 RVA: 0x00372A80 File Offset: 0x00370C80
			public override string VisitNonterminal(NonterminalNode node)
			{
				if (node.Rule is ConversionRule)
				{
					return node.Children[0].AcceptVisitor<string>(this);
				}
				ConstStr constStr;
				if (Language.Build.Node.IsRule.ConstStr(node, out constStr))
				{
					return constStr.s.Node.AcceptVisitor<string>(this);
				}
				Concat concat;
				if (!Language.Build.Node.IsRule.Concat(node, out concat))
				{
					return null;
				}
				if (!concat.f.Is_LetColumnName(Language.Build))
				{
					return null;
				}
				Concat concat2;
				if (!concat.e.Is_Concat(Language.Build, out concat2))
				{
					return null;
				}
				string text = concat2.f.Node.AcceptVisitor<string>(this);
				if (text == null)
				{
					return null;
				}
				Concat concat3;
				if (concat2.e.Is_Concat(Language.Build, out concat3))
				{
					string text2 = concat3.Node.AcceptVisitor<string>(this);
					if (text != text2)
					{
						return null;
					}
				}
				else if (concat2.e.Node.AcceptVisitor<string>(this) != null)
				{
					return null;
				}
				return text;
			}

			// Token: 0x060100CF RID: 65743 RVA: 0x00372B93 File Offset: 0x00370D93
			public override string VisitLet(LetNode node)
			{
				return node.BodyNode.AcceptVisitor<string>(this);
			}

			// Token: 0x060100D0 RID: 65744 RVA: 0x00372BA1 File Offset: 0x00370DA1
			public override string VisitLambda(LambdaNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x060100D1 RID: 65745 RVA: 0x00372BAC File Offset: 0x00370DAC
			public override string VisitLiteral(LiteralNode node)
			{
				if (Language.Build.Node.As.s(node) == null)
				{
					return null;
				}
				s? s;
				return s.GetValueOrDefault().Value;
			}

			// Token: 0x060100D2 RID: 65746 RVA: 0x00002188 File Offset: 0x00000388
			public override string VisitVariable(VariableNode node)
			{
				return null;
			}

			// Token: 0x060100D3 RID: 65747 RVA: 0x00002188 File Offset: 0x00000388
			public override string VisitHole(Hole node)
			{
				return null;
			}
		}
	}
}
