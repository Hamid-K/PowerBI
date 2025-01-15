using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;
using Microsoft.ProgramSynthesis.Transformation.Text.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Text.Translation;
using Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Microsoft.ProgramSynthesis.Wrangling.SignificantInputs;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Text
{
	// Token: 0x02001BAA RID: 7082
	public class Session : NonInteractiveSession<Program, IRow, object>
	{
		// Token: 0x0600E7F3 RID: 59379 RVA: 0x00312B54 File Offset: 0x00310D54
		public Session(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null, IEnumerable<EntityDetector> entityDetectorObjects = null)
			: base(Learner.Instance, Loader.Instance, journalStorage, culture, "Transformation.Text", logger, true)
		{
			if (entityDetectorObjects != null)
			{
				EntityDetectorsMap entityDetectorsMap = new EntityDetectorsMap(entityDetectorObjects);
				this.DeserializationContext = DeserializationContext.Create<EntityDetectorsMap>(entityDetectorsMap);
				base.Constraints.Add(new EntityDetectorsMapConstraint<IRow, object>(entityDetectorsMap));
			}
		}

		// Token: 0x170026A4 RID: 9892
		// (get) Token: 0x0600E7F4 RID: 59380 RVA: 0x00312BA3 File Offset: 0x00310DA3
		public override DeserializationContext DeserializationContext { get; }

		// Token: 0x170026A5 RID: 9893
		// (get) Token: 0x0600E7F5 RID: 59381 RVA: 0x00312BAB File Offset: 0x00310DAB
		public new static JsonSerializerSettings JsonSerializerSettings
		{
			get
			{
				return Session.LazyJsonSerializerSettings.Value;
			}
		}

		// Token: 0x170026A6 RID: 9894
		// (get) Token: 0x0600E7F6 RID: 59382 RVA: 0x00312BB7 File Offset: 0x00310DB7
		protected override JsonSerializerSettings JsonSerializerSettingsInstance
		{
			get
			{
				return Session.JsonSerializerSettings;
			}
		}

		// Token: 0x170026A7 RID: 9895
		// (get) Token: 0x0600E7F7 RID: 59383 RVA: 0x00312BBE File Offset: 0x00310DBE
		private ColumnPriority ColumnPriorityConstraint
		{
			get
			{
				return base.Constraints.OfType<ColumnPriority>().LastOrDefault<ColumnPriority>();
			}
		}

		// Token: 0x170026A8 RID: 9896
		// (get) Token: 0x0600E7F8 RID: 59384 RVA: 0x00312BD0 File Offset: 0x00310DD0
		private IEnumerable<MergeColumns> MergesWithColumns
		{
			get
			{
				return from m in base.Constraints.OfType<MergeColumns>()
					where m.Columns != null
					select m;
			}
		}

		// Token: 0x170026A9 RID: 9897
		// (get) Token: 0x0600E7F9 RID: 59385 RVA: 0x00312C04 File Offset: 0x00310E04
		private IEnumerable<IEnumerable<string>> ColumnPriority
		{
			get
			{
				ColumnPriority columnPriorityConstraint = this.ColumnPriorityConstraint;
				if (((columnPriorityConstraint != null) ? columnPriorityConstraint.Priority : null) != null)
				{
					ColumnPriority columnPriorityConstraint2 = this.ColumnPriorityConstraint;
					if (columnPriorityConstraint2 == null)
					{
						return null;
					}
					return columnPriorityConstraint2.Priority;
				}
				else
				{
					MergeColumns mergeColumns = this.MergesWithColumns.FirstOrDefault<MergeColumns>();
					IEnumerable<string> enumerable;
					if ((enumerable = ((mergeColumns != null) ? mergeColumns.Columns : null)) == null)
					{
						IRow row = base.InputsAndConstraintInputs.FirstOrDefault((IRow input) => input.ColumnNames != null);
						enumerable = ((row != null) ? row.ColumnNames : null);
					}
					IEnumerable<string> enumerable2 = enumerable;
					if (enumerable2 != null)
					{
						return Seq.Of<IEnumerable<string>>(new IEnumerable<string>[] { enumerable2 });
					}
					return null;
				}
			}
		}

		// Token: 0x170026AA RID: 9898
		// (get) Token: 0x0600E7FA RID: 59386 RVA: 0x00312C9D File Offset: 0x00310E9D
		private IEnumerable<string> AllColumns
		{
			get
			{
				IEnumerable<IEnumerable<string>> columnPriority = this.ColumnPriority;
				if (columnPriority == null)
				{
					return null;
				}
				return columnPriority.Union<string>();
			}
		}

		// Token: 0x0600E7FB RID: 59387 RVA: 0x00312CB0 File Offset: 0x00310EB0
		public IEnumerable<string> GetAllColumns()
		{
			return this.AllColumns;
		}

		// Token: 0x0600E7FC RID: 59388 RVA: 0x00312CB8 File Offset: 0x00310EB8
		public Translation Translate(TargetLanguage targetLanguage, Program program, OptimizeFor? optimizeFor = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			Example[] array = base.Constraints.OfType<Example>().ToArray<Example>();
			if (array == null || !array.Any<Example>())
			{
				throw new Exception("Unable to determine examples.");
			}
			bool flag = true;
			string text = null;
			Stopwatch stopwatch = Stopwatch.StartNew();
			Translation translation;
			try
			{
				if (targetLanguage != TargetLanguage.Python)
				{
					throw new ApplicationException(string.Format("Unsupported target language ({0})", targetLanguage));
				}
				string text2 = Session.PythonTranslate(program, optimizeFor ?? OptimizeFor.Readability);
				text = text2;
				translation = new Translation(program, targetLanguage, text);
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				stopwatch.Stop();
				if (base.Logger != null)
				{
					string text3 = string.Join(Environment.NewLine, array.Select((Example example) => example.ToString()));
					string text4 = string.Join(Environment.NewLine, array.Select((Example example) => example.ToAnonymizedString()));
					base.Logger.TrackEvent("Translate", new KeyValuePair<string, double>[] { KVP.Create<string, double>("TranslateTime", stopwatch.ElapsedMillisecondsAsDouble()) }, new KeyValuePair<string, string>[]
					{
						KVP.Create<string, string>("Success", flag.ToString()),
						KVP.Create<string, string>("ExamplesAnonymized", text4)
					}, new KeyValuePair<string, string>[]
					{
						KVP.Create<string, string>("Examples", text3),
						KVP.Create<string, string>("TranslatedProgram", text)
					});
				}
			}
			return translation;
		}

		// Token: 0x0600E7FD RID: 59389 RVA: 0x00312E64 File Offset: 0x00311064
		private static string PythonTranslate(Program program, OptimizeFor optimizeFor)
		{
			string text = "transformation_text_program";
			string text2 = "transformation_text";
			string text3 = "prose_semantics";
			PythonTranslator pythonTranslator = new PythonTranslator(null);
			PythonModule pythonModule = pythonTranslator.CreateModule(text2, text3, "ttext");
			GeneratedFunction generatedFunction = pythonTranslator.Translate(program, pythonModule, optimizeFor);
			pythonModule.Bind(text, generatedFunction);
			return pythonModule.GenerateUnisolatedCode(optimizeFor);
		}

		// Token: 0x0600E7FE RID: 59390 RVA: 0x00312EB4 File Offset: 0x003110B4
		protected override IEnumerable<KeyValuePair<string, double>> TrackedLearningMetrics(LearnProgramRequest<Program, IRow, object> request, Program topProgram)
		{
			Session.<>c__DisplayClass23_0 CS$<>8__locals1 = new Session.<>c__DisplayClass23_0();
			Session.<>c__DisplayClass23_0 CS$<>8__locals2 = CS$<>8__locals1;
			IEnumerable<string> allColumns = this.AllColumns;
			CS$<>8__locals2.allColumns = ((allColumns != null) ? allColumns.ToList<string>() : null);
			IReadOnlyList<int> readOnlyList2;
			if (CS$<>8__locals1.allColumns != null)
			{
				IReadOnlyList<int> readOnlyList = base.InputsAndConstraintInputs.Select((IRow input) => CS$<>8__locals1.allColumns.Sum(delegate(string col)
				{
					object obj;
					if (input != null && input.TryGetValue(col, out obj))
					{
						string text2 = obj as string;
						if (text2 != null)
						{
							return text2.Length;
						}
					}
					return 0;
				})).ToList<int>();
				readOnlyList2 = readOnlyList;
			}
			else
			{
				IReadOnlyList<int> readOnlyList = new int[1];
				readOnlyList2 = readOnlyList;
			}
			IReadOnlyList<int> readOnlyList3 = readOnlyList2;
			IReadOnlyList<int> readOnlyList4 = base.Constraints.OfType<Example<IRow, object>>().Select(delegate(Example<IRow, object> ex)
			{
				string text3 = ex.Output as string;
				if (text3 == null)
				{
					return 0;
				}
				return text3.Length;
			}).ToList<int>();
			Session.SummaryStatistics summaryStatistics = Session.<TrackedLearningMetrics>g__ComputeSummaryStatistics|23_2(readOnlyList3);
			Session.SummaryStatistics summaryStatistics2 = Session.<TrackedLearningMetrics>g__ComputeSummaryStatistics|23_2(readOnlyList4);
			IEnumerable<KeyValuePair<string, double>> enumerable = base.TrackedLearningMetrics(request, topProgram);
			IEnumerable<KeyValuePair<string, double>> enumerable2;
			if (!(topProgram == null))
			{
				KeyValuePair<string, double>[] array = new KeyValuePair<string, double>[4];
				array[0] = KVP.Create<string, double>("NumColumnsUsed", (double)topProgram.ColumnsUsed.Count);
				array[1] = KVP.Create<string, double>("NumBranches", (double)topProgram.Branches.Count);
				array[2] = KVP.Create<string, double>("MaxNumAtoms", (double)topProgram.Branches.Max((Branch b) => b.Body.Node.AcceptVisitor<int>(Session.AtomCountVisitor.Instance)));
				enumerable2 = array;
				array[3] = KVP.Create<string, double>("NumDistinctTokens", (double)topProgram.ProgramNode.AcceptVisitor<IEnumerable<RegularExpression>>(Session.RegularExpressionCollector.Instance).SelectMany((RegularExpression r) => r.Tokens).Distinct<Token>()
					.Count<Token>());
			}
			else
			{
				enumerable2 = new KeyValuePair<string, double>[0];
			}
			IEnumerable<KeyValuePair<string, double>> enumerable3 = enumerable.Concat(enumerable2);
			KeyValuePair<string, double>[] array2 = new KeyValuePair<string, double>[12];
			array2[0] = KVP.Create<string, double>("NumExamples", (double)summaryStatistics2.Count);
			int num = 1;
			string text = "NumInputColumns";
			IReadOnlyList<string> allColumns2 = CS$<>8__locals1.allColumns;
			int? num2 = ((allColumns2 != null) ? new int?(allColumns2.Count) : null);
			array2[num] = KVP.Create<string, double>(text, ((num2 != null) ? new double?((double)num2.GetValueOrDefault()) : null).GetValueOrDefault());
			array2[2] = KVP.Create<string, double>("MinInputLength", (double)summaryStatistics.Min);
			array2[3] = KVP.Create<string, double>("MaxInputLength", (double)summaryStatistics.Max);
			array2[4] = KVP.Create<string, double>("MeanInputLength", summaryStatistics.Mean);
			array2[5] = KVP.Create<string, double>("MedianInputLength", summaryStatistics.Median);
			array2[6] = KVP.Create<string, double>("StdDevInputLength", summaryStatistics.StdDev);
			array2[7] = KVP.Create<string, double>("MinOutputLength", (double)summaryStatistics2.Min);
			array2[8] = KVP.Create<string, double>("MaxOutputLength", (double)summaryStatistics2.Max);
			array2[9] = KVP.Create<string, double>("MeanOutputLength", summaryStatistics2.Mean);
			array2[10] = KVP.Create<string, double>("MedianOutputLength", summaryStatistics2.Median);
			array2[11] = KVP.Create<string, double>("StdDevOutputLength", summaryStatistics2.StdDev);
			return enumerable3.Concat(array2);
		}

		// Token: 0x0600E7FF RID: 59391 RVA: 0x003131BC File Offset: 0x003113BC
		protected override IEnumerable<KeyValuePair<string, string>> TrackedLearningProperties(LearnProgramRequest<Program, IRow, object> request, Program topProgram)
		{
			IEnumerable<KeyValuePair<string, string>> enumerable = base.TrackedLearningProperties(request, topProgram);
			if (topProgram != null)
			{
				IEnumerable<KeyValuePair<string, string>> enumerable2 = enumerable;
				KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[1];
				array[0] = KVP.Create<string, string>("TransformationsUsed", topProgram.AllTransformations.Select((TransformationDescription t) => t.Kind).Aggregate((TransformationKind a, TransformationKind b) => a | b).ToString());
				return enumerable2.Concat(array);
			}
			return enumerable;
		}

		// Token: 0x0600E800 RID: 59392 RVA: 0x00313258 File Offset: 0x00311458
		protected override IReadOnlyList<SignificantInput<IRow>> GetSignificantInputs(IReadOnlyList<Distinguisher<IRow>> distinguishers, LearnProgramRequest<Program, IRow, object> request, SignificantInputProgramProfile<Program, IRow, object> programProfile, CancellationToken cancel)
		{
			Program anyTopProgram = programProfile.AnyTopProgram;
			Lookup lookup = ((anyTopProgram != null) ? anyTopProgram.AllTransformations.OfType<Lookup>().OnlyOrDefault<Lookup>() : null);
			if (lookup != null)
			{
				return this.SignificantInputsForLookup(request, programProfile, lookup, cancel);
			}
			List<Distinguisher<IRow>> list = new List<Distinguisher<IRow>>();
			Func<Program, Distinguisher<IRow>> func = (Program p) => new Distinguisher<IRow>(1U, delegate(IRow input)
			{
				if (p.Run(input) != null || request.IsConstrained(input))
				{
					return null;
				}
				return new uint?(0U);
			}, true);
			list.AddRange(new Program[] { programProfile.TopProgram, programProfile.TopProgramNoInputs }.Where((Program p) => p != null).Select(func));
			Program anyTopProgram2 = programProfile.AnyTopProgram;
			if (anyTopProgram2 != null)
			{
				using (IEnumerator<ParseDateTime> enumerator = (from g in anyTopProgram2.AllTransformations.OfType<ParseDateTime>().GroupBy((ParseDateTime p) => p.ProgramNode, IdentityEquality.Comparer)
					select g.First<ParseDateTime>()).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ParseDateTime pdt = enumerator.Current;
						Distinguisher<IRow> distinguisher = new Distinguisher<IRow>((uint)(pdt.Formats.Count + 2), delegate(IRow input)
						{
							ProgramNode node = pdt.ParseProgramNode.SS.Node;
							State state = State.CreateForLearning(Language.Build.Symbol.x, Semantics.ChooseInput(input, pdt.ColumnName));
							ValueSubstring valueSubstring = node.Invoke(state) as ValueSubstring;
							if (valueSubstring == null)
							{
								return null;
							}
							PartialDateTime partialDateTime = null;
							uint num = (uint)(pdt.Formats.Count + 1);
							foreach (Record<int, DateTimeFormat> record in pdt.Formats.Enumerate<DateTimeFormat>())
							{
								int num2;
								DateTimeFormat dateTimeFormat;
								record.Deconstruct(out num2, out dateTimeFormat);
								int num3 = num2;
								Optional<DateTimeFormatMatch> optional = DateFormatCache.Parse(dateTimeFormat, valueSubstring);
								if (optional.HasValue)
								{
									if (partialDateTime != null && !object.Equals(optional.Value.PartialDateTime, partialDateTime))
									{
										return new uint?((uint)pdt.Formats.Count);
									}
									partialDateTime = optional.Value.PartialDateTime;
									num = (uint)num3;
								}
							}
							return new uint?(num);
						}, false);
						list.Add(distinguisher);
					}
				}
			}
			distinguishers = distinguishers.Concat(list).ToList<Distinguisher<IRow>>();
			return base.GetSignificantInputs(distinguishers, request, programProfile, cancel);
		}

		// Token: 0x0600E801 RID: 59393 RVA: 0x00313400 File Offset: 0x00311600
		private IReadOnlyList<SignificantInput<IRow>> SignificantInputsForLookup(LearnProgramRequest<Program, IRow, object> request, SignificantInputProgramProfile<Program, IRow, object> programProfile, Lookup lookup, CancellationToken cancel)
		{
			if (request.Constraints.OfType<Example<IRow, object>>().Count<Example<IRow, object>>() <= 1)
			{
				return null;
			}
			Program anyTopProgram = programProfile.AnyTopProgram;
			string columnName = anyTopProgram.ColumnsUsed.Single<string>();
			var enumerable = request.Inputs.Select((IRow input) => new
			{
				input = input,
				cellValue = Semantics.ChooseInput(input, columnName)
			}).Distinct(obj => obj.cellValue);
			cancel.ThrowIfCancellationRequested();
			HashSet<ValueSubstring> lookupKeys = lookup.LookupDictionary.Keys.Select(delegate(Optional<string> s)
			{
				if (!s.HasValue)
				{
					return null;
				}
				return ValueSubstring.Create(s.Value, null, null, null, null);
			}).ConvertToHashSet<ValueSubstring>();
			return enumerable.Where(obj => !lookupKeys.Contains(obj.cellValue)).Select(delegate(obj)
			{
				IReadOnlyList<IRow> readOnlyList = new List<IRow> { obj.input };
				return new SignificantInput<IRow>(1.0, readOnlyList.First<IRow>(), readOnlyList.Some<IReadOnlyList<IRow>>());
			}).ToList<SignificantInput<IRow>>();
		}

		// Token: 0x0600E802 RID: 59394 RVA: 0x003134F8 File Offset: 0x003116F8
		protected override IRow ChooseSignificantInputFromCluster(IReadOnlyList<IRow> cluster)
		{
			Session.<>c__DisplayClass27_0 CS$<>8__locals1 = new Session.<>c__DisplayClass27_0();
			Session.<>c__DisplayClass27_0 CS$<>8__locals2 = CS$<>8__locals1;
			IEnumerable<string> allColumns = this.AllColumns;
			CS$<>8__locals2.columns = ((allColumns != null) ? allColumns.ToList<string>() : null);
			if (CS$<>8__locals1.columns == null || !CS$<>8__locals1.columns.Any<string>())
			{
				return base.ChooseSignificantInputFromCluster(cluster);
			}
			return cluster.ArgMin((IRow row) => CS$<>8__locals1.columns.Sum(delegate(string column)
			{
				ValueSubstring valueSubstring = Semantics.ChooseInput(row, column);
				return (long)((ulong)((valueSubstring != null) ? valueSubstring.Length : 0U));
			}));
		}

		// Token: 0x0600E804 RID: 59396 RVA: 0x00313570 File Offset: 0x00311770
		[CompilerGenerated]
		internal static Session.SummaryStatistics <TrackedLearningMetrics>g__ComputeSummaryStatistics|23_2(IReadOnlyList<int> numbers)
		{
			if (numbers.Count == 0)
			{
				return new Session.SummaryStatistics(0, 0, 0.0, 0, 0.0, 0.0);
			}
			int num = 0;
			int num2 = 0;
			int num3 = int.MaxValue;
			int num4 = int.MinValue;
			foreach (int num5 in numbers)
			{
				num++;
				num2 += num5;
				if (num5 < num3)
				{
					num3 = num5;
				}
				if (num5 > num4)
				{
					num4 = num5;
				}
			}
			if (num3 > num4)
			{
				num4 = (num3 = 0);
			}
			double num6 = (double)num2 / (double)num;
			double value = numbers.Median().Value;
			return new Session.SummaryStatistics(num3, num4, num6, num, value, numbers.StandardDeviation(new double?(num6)));
		}

		// Token: 0x04005850 RID: 22608
		private static readonly Lazy<JsonSerializerSettings> LazyJsonSerializerSettings = new Lazy<JsonSerializerSettings>(() => new SessionJsonSerializerSettings().Initialize());

		// Token: 0x02001BAB RID: 7083
		private class AtomCountVisitor : ProgramNodeVisitor<int>
		{
			// Token: 0x0600E805 RID: 59397 RVA: 0x00313648 File Offset: 0x00311848
			public override int VisitNonterminal(NonterminalNode node)
			{
				if (Language.Build.Node.IsRule.Atom(node))
				{
					return 1;
				}
				return node.Children.Sum((ProgramNode c) => c.AcceptVisitor<int>(this));
			}

			// Token: 0x0600E806 RID: 59398 RVA: 0x0000FA11 File Offset: 0x0000DC11
			public override int VisitLet(LetNode node)
			{
				return 0;
			}

			// Token: 0x0600E807 RID: 59399 RVA: 0x0000FA11 File Offset: 0x0000DC11
			public override int VisitLambda(LambdaNode node)
			{
				return 0;
			}

			// Token: 0x0600E808 RID: 59400 RVA: 0x0000FA11 File Offset: 0x0000DC11
			public override int VisitLiteral(LiteralNode node)
			{
				return 0;
			}

			// Token: 0x0600E809 RID: 59401 RVA: 0x0000FA11 File Offset: 0x0000DC11
			public override int VisitVariable(VariableNode node)
			{
				return 0;
			}

			// Token: 0x0600E80A RID: 59402 RVA: 0x0000FA11 File Offset: 0x0000DC11
			public override int VisitHole(Hole node)
			{
				return 0;
			}

			// Token: 0x04005851 RID: 22609
			public static readonly Session.AtomCountVisitor Instance = new Session.AtomCountVisitor();
		}

		// Token: 0x02001BAC RID: 7084
		internal class RegularExpressionCollector : ProgramNodeVisitor<IEnumerable<RegularExpression>>
		{
			// Token: 0x0600E80E RID: 59406 RVA: 0x00313697 File Offset: 0x00311897
			public override IEnumerable<RegularExpression> VisitNonterminal(NonterminalNode node)
			{
				return node.Children.SelectMany((ProgramNode c) => c.AcceptVisitor<IEnumerable<RegularExpression>>(this));
			}

			// Token: 0x0600E80F RID: 59407 RVA: 0x003136B0 File Offset: 0x003118B0
			public override IEnumerable<RegularExpression> VisitLet(LetNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x0600E810 RID: 59408 RVA: 0x003136B0 File Offset: 0x003118B0
			public override IEnumerable<RegularExpression> VisitLambda(LambdaNode node)
			{
				return this.VisitNonterminal(node);
			}

			// Token: 0x0600E811 RID: 59409 RVA: 0x003136BC File Offset: 0x003118BC
			public override IEnumerable<RegularExpression> VisitLiteral(LiteralNode node)
			{
				RegularExpression regularExpression = node.Value as RegularExpression;
				if (regularExpression != null)
				{
					return new RegularExpression[] { regularExpression };
				}
				return Session.RegularExpressionCollector.Empty;
			}

			// Token: 0x0600E812 RID: 59410 RVA: 0x003136E8 File Offset: 0x003118E8
			public override IEnumerable<RegularExpression> VisitVariable(VariableNode node)
			{
				return Session.RegularExpressionCollector.Empty;
			}

			// Token: 0x0600E813 RID: 59411 RVA: 0x003136E8 File Offset: 0x003118E8
			public override IEnumerable<RegularExpression> VisitHole(Hole node)
			{
				return Session.RegularExpressionCollector.Empty;
			}

			// Token: 0x04005852 RID: 22610
			public static readonly Session.RegularExpressionCollector Instance = new Session.RegularExpressionCollector();

			// Token: 0x04005853 RID: 22611
			private static readonly RegularExpression[] Empty = new RegularExpression[0];
		}

		// Token: 0x02001BAD RID: 7085
		private struct SummaryStatistics
		{
			// Token: 0x0600E817 RID: 59415 RVA: 0x00313717 File Offset: 0x00311917
			public SummaryStatistics(int min, int max, double mean, int count, double median, double stdDev)
			{
				this.Min = min;
				this.Max = max;
				this.Mean = mean;
				this.Count = count;
				this.Median = median;
				this.StdDev = stdDev;
			}

			// Token: 0x170026AB RID: 9899
			// (get) Token: 0x0600E818 RID: 59416 RVA: 0x00313746 File Offset: 0x00311946
			public readonly int Min { get; }

			// Token: 0x170026AC RID: 9900
			// (get) Token: 0x0600E819 RID: 59417 RVA: 0x0031374E File Offset: 0x0031194E
			public readonly int Max { get; }

			// Token: 0x170026AD RID: 9901
			// (get) Token: 0x0600E81A RID: 59418 RVA: 0x00313756 File Offset: 0x00311956
			public readonly double Mean { get; }

			// Token: 0x170026AE RID: 9902
			// (get) Token: 0x0600E81B RID: 59419 RVA: 0x0031375E File Offset: 0x0031195E
			public readonly int Count { get; }

			// Token: 0x170026AF RID: 9903
			// (get) Token: 0x0600E81C RID: 59420 RVA: 0x00313766 File Offset: 0x00311966
			public readonly double Median { get; }

			// Token: 0x170026B0 RID: 9904
			// (get) Token: 0x0600E81D RID: 59421 RVA: 0x0031376E File Offset: 0x0031196E
			public readonly double StdDev { get; }
		}
	}
}
