using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Translation.Python
{
	// Token: 0x020012CF RID: 4815
	public class PythonTranslator
	{
		// Token: 0x06009144 RID: 37188 RVA: 0x001E948E File Offset: 0x001E768E
		public PythonTranslator(Program program, TranslationOptions options = null)
		{
			this._program = program as SimpleProgram;
			this._options = options ?? new TranslationOptions();
		}

		// Token: 0x06009145 RID: 37189 RVA: 0x001E94B4 File Offset: 0x001E76B4
		public PythonSnippet GenerateCode(PythonTarget target, string encoding, string input)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			PythonImports pythonImports = new PythonImports();
			if (target == PythonTarget.Pandas)
			{
				pythonImports.AddImport("pandas");
				CsvProgram csvProgram = this._program as CsvProgram;
				if (csvProgram != null && string.IsNullOrEmpty(csvProgram.Delimiter))
				{
					this.GeneratePandasNoSplitCode(codeBuilder, encoding, input);
				}
				else
				{
					this.GeneratePandas(codeBuilder, pythonImports, encoding, input);
				}
			}
			else
			{
				if (target != PythonTarget.PySpark)
				{
					throw new ArgumentException(string.Format("Unsupported target: {0}", target), "target");
				}
				if (!this._program.IsPySparkSupported())
				{
					throw new Exception(FormattableString.Invariant(FormattableStringFactory.Create("Program cannot be translated to {0}", new object[] { target })));
				}
				if (!string.IsNullOrEmpty(encoding) && PythonTranslator.NormalizeEncoding(encoding) != "utf8")
				{
					throw new Exception(string.Format("{0} supports only utf8 encoding", target));
				}
				this.GeneratePySpark(codeBuilder, pythonImports, encoding, input);
			}
			return new PythonSnippet(pythonImports, codeBuilder.GetCode(), this._options.ResultVar, null);
		}

		// Token: 0x06009146 RID: 37190 RVA: 0x001E95BA File Offset: 0x001E77BA
		private static string NormalizeEncoding(string encoding)
		{
			if (encoding == null)
			{
				return null;
			}
			return encoding.Replace("-", "").ToLower();
		}

		// Token: 0x06009147 RID: 37191 RVA: 0x001E95D8 File Offset: 0x001E77D8
		private void GeneratePySpark(CodeBuilder builder, PythonImports imports, string encoding, string input)
		{
			if (this._options.CreateNewSparkSession)
			{
				imports.AddFromImport("pyspark.sql", "SparkSession");
				builder.AppendLine(this._options.SparkSession + " = SparkSession.builder.getOrCreate()");
				builder.AppendLine();
			}
			int skip = this._program.Skip - this._program.SkipEmptyAndCommentCount;
			this._program.Switch(delegate(CsvProgram csvProg)
			{
				if (string.IsNullOrEmpty(csvProg.Delimiter))
				{
					this.GeneratePySparkNoSplit(builder, imports, skip, input);
					return;
				}
				this.GeneratePySparkCsv(builder, imports, csvProg, skip, encoding, input);
			}, delegate(FwProgram fwProg)
			{
				this.GeneratePySparkFw(builder, imports, fwProg, skip, input);
			}, delegate(ExtractionTextProgram etextProg)
			{
				throw new NotImplementedException("Python translation unsupported.");
			});
		}

		// Token: 0x06009148 RID: 37192 RVA: 0x001E96BC File Offset: 0x001E78BC
		private void GeneratePySparkNoSplit(CodeBuilder builder, PythonImports imports, int skip, string input)
		{
			string resultVar = this._options.ResultVar;
			builder.AppendLine(string.Concat(new string[]
			{
				resultVar,
				" = ",
				this._options.SparkSession,
				".read.text(",
				input,
				")"
			}));
			string text = this._program.ColumnNames.Single<string>().ToPythonLiteral();
			this.GeneratePySparkTrim(builder, imports);
			builder.AppendLine(string.Concat(new string[]
			{
				resultVar,
				" = ",
				resultVar,
				".select(",
				this._options.TrimFnc,
				"(",
				resultVar,
				".value).alias(",
				text,
				"))"
			}));
			if (this._program.FilterEmptyLines)
			{
				imports.AddFromImports("pyspark.sql.functions", new string[] { "col" });
				builder.AppendLine(string.Concat(new string[] { resultVar, " = ", resultVar, ".filter(col(", text, ") != \"\")" }));
			}
			if (this._program.CommentStr.HasValue)
			{
				imports.AddFromImports("pyspark.sql.types", new string[] { "BooleanType" });
				builder.AppendLine(this._options.IsNotCommentFnc + " = udf(lambda s: not s.startswith(" + this._program.CommentStr.Value.ToPythonLiteral() + "), BooleanType())");
				builder.AppendLine(string.Concat(new string[]
				{
					resultVar,
					" = ",
					resultVar,
					".filter(",
					this._options.IsNotCommentFnc,
					"(col(",
					text,
					")))"
				}));
			}
			if (skip > 0)
			{
				this.GeneratePySparkSkip(builder, imports, skip);
			}
		}

		// Token: 0x06009149 RID: 37193 RVA: 0x001E98A8 File Offset: 0x001E7AA8
		private void GeneratePySparkCsv(CodeBuilder builder, PythonImports imports, CsvProgram program, int skip, string encoding, string input)
		{
			this.GeneratePySparkSchema(builder, imports);
			PythonTranslator.ArgumentList argumentList = new PythonTranslator.ArgumentList();
			argumentList.Add(input);
			if (encoding != null)
			{
				argumentList.AddLiteral("encoding", encoding);
			}
			argumentList.AddLiteral("sep", program.Delimiter);
			argumentList.AddLiteral("header", skip == 1);
			argumentList.Add("schema", this._options.SchemaVar);
			argumentList.AddLiteral("quote", program.QuoteChar);
			argumentList.AddLiteral("escape", program.DoubleQuote ? "\"" : "");
			if (program.CommentStr.HasValue)
			{
				argumentList.AddLiteral("comment", program.CommentStr.Value);
			}
			argumentList.AddLiteral("ignoreLeadingWhiteSpace", true);
			argumentList.AddLiteral("multiLine", true);
			builder.AppendLine();
			builder.AppendDelimitedList(this._options.ResultVar + " = " + this._options.SparkSession + ".read.csv", argumentList.Arguments, ",", "(", ")", null, 80);
			if (skip > 1)
			{
				this.GeneratePySparkSkip(builder, imports, skip);
			}
			this.GeneratePySparkStripCR(builder, imports);
		}

		// Token: 0x0600914A RID: 37194 RVA: 0x001E99E4 File Offset: 0x001E7BE4
		private void GeneratePySparkFw(CodeBuilder builder, PythonImports imports, FwProgram program, int skip, string input)
		{
			string df = this._options.ResultVar;
			builder.AppendLine(string.Concat(new string[]
			{
				df,
				" = ",
				this._options.SparkSession,
				".read.text(",
				input,
				")"
			}));
			this.GeneratePySparkTrim(builder, imports);
			if (program.FilterEmptyLines)
			{
				builder.AppendLine(string.Concat(new string[]
				{
					df,
					" = ",
					df,
					".filter(",
					this._options.TrimFnc,
					"(",
					df,
					".value) != \"\")"
				}));
			}
			if (program.CommentStr.HasValue)
			{
				imports.AddFromImports("pyspark.sql.functions", new string[] { "udf" });
				imports.AddFromImports("pyspark.sql.types", new string[] { "BooleanType" });
				builder.AppendLine("is_not_comment = udf(lambda s: not s.startswith(" + program.CommentStr.Value.ToPythonLiteral() + "), BooleanType())");
				builder.AppendLine(string.Concat(new string[] { df, " = ", df, ".filter(is_not_comment(trim(", df, ".value)))" }));
			}
			if (program.FilterEmptyLines || program.CommentStr.HasValue)
			{
				builder.AppendLine();
			}
			IReadOnlyList<string> readOnlyList = program.FieldPositions.Zip(program.ColumnNames, delegate(Record<int, int?> positions, string columnName)
			{
				int num;
				int? num2;
				positions.Deconstruct(out num, out num2);
				num++;
				int? num3 = num2;
				int num4 = num;
				int num5 = ((num3 != null) ? new int?(num3.GetValueOrDefault() - num4 + 1) : null) ?? int.MaxValue;
				return string.Concat(new string[]
				{
					"trim(",
					df,
					".value.substr(",
					num.ToLiteral(null),
					", ",
					num5.ToLiteral(null),
					")).alias(",
					columnName.ToPythonLiteral(),
					")"
				});
			}).ToList<string>();
			IReadOnlyList<string> readOnlyList2 = this.GenerateColumnNameComments();
			builder.AppendDelimitedList(df + " = " + df + ".select", readOnlyList, ",", "(", ")", readOnlyList2, 80);
			if (skip > 0)
			{
				this.GeneratePySparkSkip(builder, imports, skip);
			}
		}

		// Token: 0x0600914B RID: 37195 RVA: 0x001E9BF4 File Offset: 0x001E7DF4
		private void GeneratePySparkSkip(CodeBuilder builder, PythonImports imports, int skip)
		{
			string resultVar = this._options.ResultVar;
			imports.AddFromImport("pyspark.sql.functions", "monotonically_increasing_id");
			builder.AppendLine();
			builder.AppendLine(resultVar + " = " + resultVar + ".withColumn(\"_skip_index\", monotonically_increasing_id())");
			builder.AppendLine(string.Format("{0} = {1}.filter(\"_skip_index >= {2}\").drop(\"_skip_index\")", resultVar, resultVar, skip));
		}

		// Token: 0x0600914C RID: 37196 RVA: 0x001E9C54 File Offset: 0x001E7E54
		private void GeneratePySparkSchema(CodeBuilder builder, PythonImports imports)
		{
			imports.AddFromImports("pyspark.sql.types", new string[] { "StructType", "StructField", "StringType" });
			IReadOnlyList<string> readOnlyList = this.GenerateColumnNameComments();
			IReadOnlyList<string> readOnlyList2 = this._program.ColumnNames.Select((string col) => "StructField(" + col.ToPythonLiteral() + ", StringType(), True)").ToList<string>();
			using (builder.NewScope(this._options.SchemaVar + " = StructType(", 1U))
			{
				builder.AppendDelimitedList(string.Empty, readOnlyList2, ",", "[", "]", readOnlyList, 80);
			}
			builder.AppendLine(")");
		}

		// Token: 0x0600914D RID: 37197 RVA: 0x001E9D28 File Offset: 0x001E7F28
		private void GeneratePySparkTrim(CodeBuilder builder, PythonImports imports)
		{
			imports.AddFromImports("pyspark.sql.functions", new string[] { "udf" });
			imports.AddFromImports("pyspark.sql.types", new string[] { "StringType" });
			builder.AppendLine();
			builder.AppendLine(this._options.TrimFnc + " = udf(lambda s: s.strip(), StringType())");
		}

		// Token: 0x0600914E RID: 37198 RVA: 0x001E9D88 File Offset: 0x001E7F88
		private void GeneratePySparkStripCR(CodeBuilder builder, PythonImports imports)
		{
			bool flag;
			if (this._program.NewLineStrings != null)
			{
				flag = this._program.NewLineStrings.Any((string x) => x.Contains('\r'));
			}
			else
			{
				flag = false;
			}
			if (!flag)
			{
				return;
			}
			imports.AddFromImports("pyspark.sql.functions", new string[] { "col", "udf" });
			imports.AddFromImport("pyspark.sql.types", "StringType");
			builder.AppendLine();
			string rstripCRFnc = this._options.RStripCRFnc;
			builder.AppendLine(rstripCRFnc + " = udf(lambda s: None if s is None else s.rstrip(\"\\r\"), StringType())");
			string resultVar = this._options.ResultVar;
			builder.AppendLine(string.Concat(new string[] { resultVar, " = ", resultVar, ".select(*(", rstripCRFnc, "(col(c)).alias(c) for c in ", resultVar, ".columns))" }));
		}

		// Token: 0x0600914F RID: 37199 RVA: 0x001E9E78 File Offset: 0x001E8078
		private static string GenerateColumnNameComment(string originalName, string newName)
		{
			if (string.IsNullOrEmpty(originalName) || originalName == newName)
			{
				return null;
			}
			bool flag;
			string text = PythonTranslator.ToUnicodeEscapedPythonLiteral(originalName, out flag);
			string text2;
			if (flag)
			{
				text2 = originalName.ToPythonLiteral() + " --- " + text;
			}
			else
			{
				text2 = text;
			}
			return "  # " + text2;
		}

		// Token: 0x06009150 RID: 37200 RVA: 0x001E9EC8 File Offset: 0x001E80C8
		private IReadOnlyList<string> GenerateColumnNameComments()
		{
			if (this._program.RawColumnNames == null)
			{
				return null;
			}
			IEnumerable<string> rawColumnNames = this._program.RawColumnNames;
			IEnumerable<string> columnNames = this._program.ColumnNames;
			Func<string, string, string> func;
			if ((func = PythonTranslator.<>O.<0>__GenerateColumnNameComment) == null)
			{
				func = (PythonTranslator.<>O.<0>__GenerateColumnNameComment = new Func<string, string, string>(PythonTranslator.GenerateColumnNameComment));
			}
			IReadOnlyList<string> readOnlyList = rawColumnNames.Zip(columnNames, func).ToList<string>();
			if (!readOnlyList.Any((string x) => x != null))
			{
				return null;
			}
			return readOnlyList;
		}

		// Token: 0x06009151 RID: 37201 RVA: 0x001E9F4C File Offset: 0x001E814C
		private void GeneratePandas(CodeBuilder builder, PythonImports imports, string encoding, string input)
		{
			IReadOnlyList<string> columnNames = this._program.ColumnNames.Select((string c) => c.ToPythonLiteral()).ToList<string>();
			IReadOnlyList<string> columnNameComments = this.GenerateColumnNameComments();
			this._program.Switch(delegate(CsvProgram csvProg)
			{
				builder.AppendDelimitedList(this._options.NamesVar + " = ", columnNames, ",", "[", "]", columnNameComments, 80);
			}, delegate(FwProgram fwProg)
			{
				IEnumerable<Record<int, int?>> fieldPositions = fwProg.FieldPositions;
				Func<Record<int, int?>, string> func;
				if ((func = PythonTranslator.<>O.<1>__FieldPairToLiteral) == null)
				{
					func = (PythonTranslator.<>O.<1>__FieldPairToLiteral = new Func<Record<int, int?>, string>(PythonTranslator.FieldPairToLiteral));
				}
				IReadOnlyList<string> readOnlyList = fieldPositions.Select(func).ToList<string>();
				IReadOnlyList<string> readOnlyList2 = columnNames.Zip(readOnlyList, (string c, string f) => string.Concat(new string[] { "(", c, ", ", f, ")" })).ToList<string>();
				builder.AppendDelimitedList(this._options.ColumnsVar + " = ", readOnlyList2, ",", "[", "]", columnNameComments, 80);
				builder.AppendLine(string.Concat(new string[]
				{
					this._options.NamesVar,
					", ",
					this._options.ColspecsVar,
					" = zip(*",
					this._options.ColumnsVar,
					")"
				}));
			}, delegate(ExtractionTextProgram etextProg)
			{
				throw new NotImplementedException("Python translation unsupported.");
			});
			builder.AppendLine();
			string text = PythonTranslator.NormalizeEncoding(encoding);
			bool usePythonEngine = text != null && text.StartsWith("utf32");
			PythonTranslator.ArgumentList argListist = new PythonTranslator.ArgumentList();
			argListist.Add(input);
			if (encoding != null)
			{
				argListist.AddLiteral("encoding", encoding);
			}
			argListist.AddLiteral("skiprows", this._program.Skip);
			argListist.AddLiteral("header", null);
			argListist.Add("names", this._options.NamesVar);
			if (this._program.SkipFooter > 0)
			{
				usePythonEngine = true;
				argListist.AddLiteral("skipfooter", this._program.SkipFooter);
			}
			string fncName = null;
			this._program.Switch(delegate(CsvProgram csvProg)
			{
				fncName = "pandas.read_csv";
				bool flag;
				string text2 = PythonTranslator.ToUnicodeEscapedPythonLiteral(csvProg.Delimiter, out flag);
				usePythonEngine = usePythonEngine || flag;
				if (csvProg.Delimiter.Length > 1)
				{
					imports.AddImport("re");
					text2 = "re.escape(" + text2 + ")";
					usePythonEngine = true;
				}
				argListist.Add("delimiter", text2);
				if (csvProg.QuoteChar.HasValue)
				{
					if (csvProg.QuoteChar.Value != '"')
					{
						argListist.AddLiteral("quotechar", csvProg.QuoteChar.Value);
					}
					if (!csvProg.DoubleQuote)
					{
						argListist.AddLiteral("doublequote", false);
					}
				}
				else
				{
					imports.AddImport("csv");
					argListist.Add("quoting", "csv.QUOTE_NONE");
				}
				if (csvProg.EscapeChar.HasValue)
				{
					argListist.AddLiteral("escapechar", csvProg.EscapeChar.Value);
				}
			}, delegate(FwProgram fwProg)
			{
				fncName = "pandas.read_fwf";
				argListist.Add("colspecs", this._options.ColspecsVar);
			}, delegate(ExtractionTextProgram etextProg)
			{
				throw new NotImplementedException("Python translation unsupported.");
			});
			if (this._program.CommentStr.HasValue)
			{
				usePythonEngine = true;
				argListist.AddLiteral("comment", this._program.CommentStr.Value);
			}
			if (!this._program.FilterEmptyLines)
			{
				argListist.AddLiteral("skip_blank_lines", false);
			}
			if (usePythonEngine)
			{
				argListist.AddLiteral("engine", "python");
			}
			if (this._options.SuppressTypeDetection)
			{
				this.AddSupressPandasTypeDetectionParams(argListist);
			}
			builder.AppendDelimitedList(this._options.ResultVar + " = " + fncName, argListist.Arguments, ",", "(", ")", null, 80);
			string resultVar = this._options.ResultVar;
			if (this._program is FwProgram && this._program.FilterEmptyLines && this._program.HasEmptyLines)
			{
				builder.AppendLine();
				builder.AppendLine("# Remove empty rows using this manual step,");
				builder.AppendLine("# because skip_empty_lines does not work with read_fwf.");
				builder.AppendLine(string.Concat(new string[] { resultVar, " = ", resultVar, ".mask((", resultVar, " == \"\").all(axis=1)).dropna(how=\"all\")" }));
			}
		}

		// Token: 0x06009152 RID: 37202 RVA: 0x001EA280 File Offset: 0x001E8480
		private static string FieldPairToLiteral(Record<int, int?> pair)
		{
			string text = pair.Item1.ToLiteral(null);
			string text2 = ((pair.Item2 != null) ? pair.Item2.Value.ToLiteral(null) : PythonStringUtils.ToPythonLiteral((string)null));
			return string.Concat(new string[] { "(", text, ", ", text2, ")" });
		}

		// Token: 0x06009153 RID: 37203 RVA: 0x001EA2F8 File Offset: 0x001E84F8
		private void GeneratePandasNoSplitCode(CodeBuilder builder, string encoding, string input)
		{
			string linesVar = this._options.LinesVar;
			PythonTranslator.ArgumentList argumentList = new PythonTranslator.ArgumentList();
			argumentList.Add(input);
			argumentList.AddLiteral("r");
			if (encoding != null)
			{
				argumentList.AddLiteral("encoding", encoding);
			}
			string text = "open(" + string.Join(", ", argumentList.Arguments) + ")";
			using (builder.NewScope("with " + text + " as f:", 1U))
			{
				builder.AppendLine(linesVar + " = f.readlines()");
			}
			builder.AppendLine();
			if (this._program.Skip > 0)
			{
				builder.AppendLine(string.Format("{0} = {1}[{2}:]", linesVar, linesVar, this._program.Skip));
			}
			builder.AppendLine(linesVar + " = map(lambda line: line.strip(), " + linesVar + ")");
			if (this._program.FilterEmptyLines)
			{
				builder.AppendLine(linesVar + " = filter(None, " + linesVar + ")");
			}
			if (this._program.CommentStr.HasValue)
			{
				builder.AppendLine(string.Concat(new string[]
				{
					linesVar,
					" = filter(lambda line: not line.startswith(",
					this._program.CommentStr.Value.ToPythonLiteral(),
					"), ",
					linesVar,
					")"
				}));
			}
			builder.AppendLine(string.Concat(new string[]
			{
				this._options.ResultVar,
				" = pandas.DataFrame(data=list(",
				linesVar,
				"), columns=[",
				this._program.ColumnNames.First<string>().ToPythonLiteral(),
				"], dtype=str)"
			}));
		}

		// Token: 0x06009154 RID: 37204 RVA: 0x001EA4C4 File Offset: 0x001E86C4
		private static string ToUnicodeEscapedPythonLiteral(string delimiter, out bool hasNonAscii)
		{
			hasNonAscii = false;
			if (delimiter.All((char ch) => ch <= '\u007f'))
			{
				return delimiter.ToPythonLiteral();
			}
			hasNonAscii = true;
			string text = delimiter.Normalize().ToPythonLiteral();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in text)
			{
				if (c > '\u007f')
				{
					string text3 = "\\u";
					int num = (int)c;
					string text4 = text3 + num.ToString("x4");
					stringBuilder.Append(text4);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06009155 RID: 37205 RVA: 0x001EA56C File Offset: 0x001E876C
		private void AddSupressPandasTypeDetectionParams(PythonTranslator.ArgumentList argList)
		{
			argList.AddLiteral("index_col", false);
			argList.Add("dtype", "str");
			argList.Add("na_values", "[]");
			argList.AddLiteral("keep_default_na", false);
			argList.AddLiteral("skipinitialspace", true);
		}

		// Token: 0x04003BA9 RID: 15273
		private const string PySparkSqlFunctions = "pyspark.sql.functions";

		// Token: 0x04003BAA RID: 15274
		private const string PySparkSqlTypes = "pyspark.sql.types";

		// Token: 0x04003BAB RID: 15275
		private const char PandasDefaultQuoteChar = '"';

		// Token: 0x04003BAC RID: 15276
		private readonly SimpleProgram _program;

		// Token: 0x04003BAD RID: 15277
		private readonly TranslationOptions _options;

		// Token: 0x020012D0 RID: 4816
		private class ArgumentList
		{
			// Token: 0x06009156 RID: 37206 RVA: 0x001EA5BD File Offset: 0x001E87BD
			public ArgumentList()
			{
				this._args = new List<string>();
			}

			// Token: 0x170018F8 RID: 6392
			// (get) Token: 0x06009157 RID: 37207 RVA: 0x001EA5D0 File Offset: 0x001E87D0
			public IReadOnlyList<string> Arguments
			{
				get
				{
					return this._args.ToList<string>();
				}
			}

			// Token: 0x06009158 RID: 37208 RVA: 0x001EA5DD File Offset: 0x001E87DD
			public void Add(string arg)
			{
				this._args.Add(arg);
			}

			// Token: 0x06009159 RID: 37209 RVA: 0x001EA5EB File Offset: 0x001E87EB
			public void AddLiteral(string literalValue)
			{
				this._args.Add(literalValue.ToPythonLiteral());
			}

			// Token: 0x0600915A RID: 37210 RVA: 0x001EA5FE File Offset: 0x001E87FE
			public void Add(string name, string value)
			{
				this._args.Add(name + "=" + value);
			}

			// Token: 0x0600915B RID: 37211 RVA: 0x001EA617 File Offset: 0x001E8817
			public void AddLiteral(string name, string literalValue)
			{
				this.Add(name, literalValue.ToPythonLiteral());
			}

			// Token: 0x0600915C RID: 37212 RVA: 0x001EA626 File Offset: 0x001E8826
			public void AddLiteral(string name, char literalValue)
			{
				this.Add(name, literalValue.ToPythonLiteral());
			}

			// Token: 0x0600915D RID: 37213 RVA: 0x001EA635 File Offset: 0x001E8835
			public void AddLiteral(string name, Optional<char> literalValue)
			{
				this.Add(name, literalValue.HasValue ? literalValue.Value.ToPythonLiteral() : string.Empty.ToPythonLiteral());
			}

			// Token: 0x0600915E RID: 37214 RVA: 0x001EA65F File Offset: 0x001E885F
			public void AddLiteral(string name, int literalValue)
			{
				this.Add(name, literalValue.ToLiteral(null));
			}

			// Token: 0x0600915F RID: 37215 RVA: 0x001EA674 File Offset: 0x001E8874
			public void AddLiteral(string name, bool literalValue)
			{
				string text = (literalValue ? "True" : "False");
				this.Add(name, text);
			}

			// Token: 0x04003BAE RID: 15278
			private readonly IList<string> _args;
		}

		// Token: 0x020012D1 RID: 4817
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04003BAF RID: 15279
			public static Func<string, string, string> <0>__GenerateColumnNameComment;

			// Token: 0x04003BB0 RID: 15280
			public static Func<Record<int, int?>, string> <1>__FieldPairToLiteral;
		}
	}
}
