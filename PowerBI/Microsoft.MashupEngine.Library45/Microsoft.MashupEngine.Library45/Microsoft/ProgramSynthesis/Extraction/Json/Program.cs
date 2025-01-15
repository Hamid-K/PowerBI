using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Schema;
using Microsoft.ProgramSynthesis.Wrangling.Schema.Element;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TreeOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json
{
	// Token: 0x02000B2E RID: 2862
	public class Program : TableProgram<string>, IEquatable<Program>
	{
		// Token: 0x06004766 RID: 18278 RVA: 0x000DFEFC File Offset: 0x000DE0FC
		public Program(output program, bool acceptNdJson, string startDelimiter, string endDelimiter, bool handleInvalidJson, double score, MultiTargetCode targetCodes = null)
			: base(program.Node, score)
		{
			this.AcceptNdJson = acceptNdJson;
			this.StartDelimiter = startDelimiter;
			this.EndDelimiter = endDelimiter;
			this.HandleInvalidJson = handleInvalidJson;
			this.TopLevelKind = program.Node.AcceptVisitor<Program.ExtractionKind>(new ExtractionKindVisitor());
			this.Schema = SchemaParser.ParseOutput(Language.Build.Node.Cast.output(base.ProgramNode));
			this.ColumnNames = this.Schema.DescendantOutputFields;
			this._targetCodes = targetCodes ?? new MultiTargetCode();
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004767 RID: 18279 RVA: 0x000DFF94 File Offset: 0x000DE194
		internal ISchemaElement<JsonRegion> Schema { get; }

		// Token: 0x06004768 RID: 18280 RVA: 0x000DFF9C File Offset: 0x000DE19C
		public string GetPandasCode(string input = "file", string encoding = "utf-8", bool inputIsFileName = true)
		{
			TargetCode pandas = this._targetCodes.Pandas;
			if (pandas == null)
			{
				return null;
			}
			string import = pandas.Import;
			string text = pandas.Code;
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendIndented(import);
			if (inputIsFileName)
			{
				string text2 = ((input == "f") ? "file" : "f");
				using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("with open({0}, encoding=\"{1}\") as {2}:", new object[] { input, encoding, text2 })), 1U))
				{
					text = text.Replace(TargetLearner.FileObjectId, text2);
					codeBuilder.AppendBlock(text);
					codeBuilder.AppendLine();
					goto IL_00B9;
				}
			}
			text = text.Replace(TargetLearner.FileObjectId, input);
			codeBuilder.AppendBlock(text);
			codeBuilder.AppendLine();
			IL_00B9:
			codeBuilder.AppendLine("df");
			return codeBuilder.GetCode();
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x000E0084 File Offset: 0x000DE284
		public string GetPySparkCode(string input = "file")
		{
			TargetCode pySpark = this._targetCodes.PySpark;
			if (pySpark == null)
			{
				return null;
			}
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			codeBuilder.AppendIndented(pySpark.Import);
			codeBuilder.AppendLine("spark = SparkSession.builder.getOrCreate()");
			codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("df = spark.read.json({0})", new object[] { this.AcceptNdJson ? input : (input + ", multiLine=True") })));
			string code = pySpark.Code;
			codeBuilder.AppendBlock(code);
			codeBuilder.AppendLine();
			codeBuilder.AppendLine("df");
			return codeBuilder.GetCode();
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x000E0119 File Offset: 0x000DE319
		public string GetPowerQueryMCode(string binaryContent)
		{
			TargetCode powerQueryM = this._targetCodes.PowerQueryM;
			if (powerQueryM == null)
			{
				return null;
			}
			return powerQueryM.Code.Replace(TargetLearner.FileObjectId, binaryContent);
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x0600476B RID: 18283 RVA: 0x000E013C File Offset: 0x000DE33C
		public IReadOnlyList<string> ColumnNames { get; }

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x0600476C RID: 18284 RVA: 0x000E0144 File Offset: 0x000DE344
		public bool AcceptNdJson { get; }

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x0600476D RID: 18285 RVA: 0x000E014C File Offset: 0x000DE34C
		public string StartDelimiter { get; }

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x0600476E RID: 18286 RVA: 0x000E0154 File Offset: 0x000DE354
		public string EndDelimiter { get; }

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x0600476F RID: 18287 RVA: 0x000E015C File Offset: 0x000DE35C
		public bool HandleInvalidJson { get; }

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x000E0164 File Offset: 0x000DE364
		public Program.ExtractionKind TopLevelKind { get; }

		// Token: 0x06004771 RID: 18289 RVA: 0x000E016C File Offset: 0x000DE36C
		public bool Equals(Program other)
		{
			return other != null && base.Equals(other) && ((this.ColumnNames == null && other.ColumnNames == null) || (this.ColumnNames != null && this.ColumnNames.SequenceEqual(other.ColumnNames))) && this.AcceptNdJson == other.AcceptNdJson && this.StartDelimiter == other.StartDelimiter && this.EndDelimiter == other.EndDelimiter && this.HandleInvalidJson == other.HandleInvalidJson && this.TopLevelKind == other.TopLevelKind;
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x000E020C File Offset: 0x000DE40C
		public override ITable<string> Run(string input)
		{
			ITable<string> table;
			using (StringReader stringReader = new StringReader(input))
			{
				IEnumerable<IEnumerable<string>> enumerable = this.RunTable(stringReader, this.ColumnNames.Count);
				List<IEnumerable<string>> list = ((enumerable != null) ? enumerable.ToList<IEnumerable<string>>() : null);
				table = ((list == null) ? null : new Table<string>(this.ColumnNames, list, null));
			}
			return table;
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x000E0270 File Offset: 0x000DE470
		public override ITable<string> Run(TextReader inputReader)
		{
			IEnumerable<IEnumerable<string>> enumerable = this.RunTable(inputReader, this.ColumnNames.Count);
			if (enumerable != null)
			{
				return new Table<string>(this.ColumnNames, enumerable, null);
			}
			return null;
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x000E02A4 File Offset: 0x000DE4A4
		private IEnumerable<IEnumerable<string>> RunTable(TextReader inputReader, int columnCount)
		{
			Program.<>c__DisplayClass38_0 CS$<>8__locals1 = new Program.<>c__DisplayClass38_0();
			CS$<>8__locals1.columnCount = columnCount;
			CS$<>8__locals1.<>4__this = this;
			Func<string, IEnumerable<IEnumerable<string>>> func;
			if (this.StartDelimiter == null && this.EndDelimiter == null)
			{
				func = new Func<string, IEnumerable<IEnumerable<string>>>(this.RunTable);
			}
			else
			{
				func = delegate(string s)
				{
					string startDelimiter = CS$<>8__locals1.<>4__this.StartDelimiter;
					int num = ((startDelimiter != null) ? startDelimiter.Length : 0);
					string endDelimiter = CS$<>8__locals1.<>4__this.EndDelimiter;
					int num2 = ((endDelimiter != null) ? endDelimiter.Length : 0);
					int num3 = s.Length - num - num2;
					if (num3 > 0)
					{
						return CS$<>8__locals1.<>4__this.RunTable(s.Substring(num, num3));
					}
					return null;
				};
			}
			if (!this.AcceptNdJson)
			{
				return func(inputReader.ReadToEnd());
			}
			return CS$<>8__locals1.<RunTable>g__RunNdJson|0(inputReader, func);
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x000E0310 File Offset: 0x000DE510
		private IEnumerable<IEnumerable<string>> RunTable(string singleInput)
		{
			JsonRegion jsonRegion;
			ParsedJson parsedJson;
			if (this.HandleInvalidJson)
			{
				jsonRegion = Utils.Parse(singleInput);
			}
			else if (Utils.TryParse(singleInput, out parsedJson))
			{
				jsonRegion = ((parsedJson.Errors != JsonErrors.None) ? null : parsedJson.Regions.OnlyOrDefault<JsonRegion>());
			}
			else
			{
				jsonRegion = null;
			}
			if (jsonRegion == null)
			{
				return null;
			}
			State state = State.CreateForExecution(Language.Grammar.InputSymbol, jsonRegion);
			ITreeOutput<JsonRegion> treeOutput = base.ProgramNode.Invoke(state) as ITreeOutput<JsonRegion>;
			if (treeOutput == null)
			{
				return null;
			}
			return from row in treeOutput.ToTable(this.Schema, TreeToTableSemantics.OuterJoin)
				select row.Select(delegate(JsonRegion cell)
				{
					if (cell == null)
					{
						return null;
					}
					return cell.Value;
				}).ToList<string>();
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x000E03B8 File Offset: 0x000DE5B8
		public override string Serialize(ASTSerializationSettings serializationSettings)
		{
			string text = base.ProgramNode.PrintAST(serializationSettings);
			XNode xnode;
			if (serializationSettings.HasHumanReadable)
			{
				xnode = new XCData(text);
			}
			else
			{
				xnode = XElement.Parse(text);
			}
			return new XDocument(new object[]
			{
				new XElement("JsonExtraction", new object[]
				{
					new XAttribute("version", base.Version),
					new XAttribute("symbol", base.ProgramNode.Symbol.Name),
					new XAttribute("score", base.Score),
					new XAttribute("ndjson", this.AcceptNdJson),
					new XAttribute("start-delimiter", this.StartDelimiter ?? string.Empty),
					new XAttribute("end-delimiter", this.EndDelimiter ?? string.Empty),
					new XAttribute("invalid-json", this.HandleInvalidJson),
					xnode
				})
			}).ToString();
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x000E04EA File Offset: 0x000DE6EA
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Program);
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x000E04F8 File Offset: 0x000DE6F8
		public override int GetHashCode()
		{
			return (((((((1949213049 * -10937 + base.GetHashCode()) * -10937 + EqualityComparer<ISchemaElement<JsonRegion>>.Default.GetHashCode(this.Schema)) * -10937 + this.ColumnNames.OrderDependentHashCode<string>()) * -10937 + this.AcceptNdJson.GetHashCode()) * -10937 + EqualityComparer<string>.Default.GetHashCode(this.StartDelimiter)) * -10937 + EqualityComparer<string>.Default.GetHashCode(this.EndDelimiter)) * -10937 + this.HandleInvalidJson.GetHashCode()) * -10937 + this.TopLevelKind.GetHashCode();
		}

		// Token: 0x0400209C RID: 8348
		private readonly MultiTargetCode _targetCodes;

		// Token: 0x0400209D RID: 8349
		internal const string DSLName = "JsonExtraction";

		// Token: 0x0400209E RID: 8350
		internal const string VersionName = "version";

		// Token: 0x0400209F RID: 8351
		internal const string SymbolName = "symbol";

		// Token: 0x040020A0 RID: 8352
		internal const string ScoreName = "score";

		// Token: 0x040020A1 RID: 8353
		internal const string NdJsonName = "ndjson";

		// Token: 0x040020A2 RID: 8354
		internal const string StartDelimiterName = "start-delimiter";

		// Token: 0x040020A3 RID: 8355
		internal const string EndDelimiterName = "end-delimiter";

		// Token: 0x040020A4 RID: 8356
		internal const string HandleInvalidJsonName = "invalid-json";

		// Token: 0x02000B2F RID: 2863
		public enum ExtractionKind
		{
			// Token: 0x040020AD RID: 8365
			Object,
			// Token: 0x040020AE RID: 8366
			Array,
			// Token: 0x040020AF RID: 8367
			SingleArrayObject,
			// Token: 0x040020B0 RID: 8368
			Unknown
		}
	}
}
