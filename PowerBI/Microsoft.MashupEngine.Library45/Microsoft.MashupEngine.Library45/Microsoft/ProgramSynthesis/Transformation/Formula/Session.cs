using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Formula.Intent;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Features;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Exceptions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014EF RID: 5359
	public class Session : NonInteractiveSession<Program, IRow, object>
	{
		// Token: 0x0600A445 RID: 42053 RVA: 0x0022CAE0 File Offset: 0x0022ACE0
		public Session(ILogger logger = null, IJournalStorage journalStorage = null, CultureInfo culture = null)
			: base(new Learner(), Loader.Instance, journalStorage, culture, null, logger, true)
		{
		}

		// Token: 0x0600A446 RID: 42054 RVA: 0x0022CAF7 File Offset: 0x0022ACF7
		public Session(LearnDebugTrace debugTrace, RankingDebugTrace rankingDebugTrace, ILogger logger = null, IJournalStorage journalStorage = null, CultureInfo culture = null)
			: base(new Learner(debugTrace, rankingDebugTrace, logger), Loader.Instance, journalStorage, culture, null, logger, true)
		{
		}

		// Token: 0x0600A447 RID: 42055 RVA: 0x0022CB13 File Offset: 0x0022AD13
		public IReadOnlyList<IOutputSuggestion> SuggestOutput(IRow inputRow, SuggestOutputOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.SuggestOutput(inputRow, null, options, cancellationToken);
		}

		// Token: 0x0600A448 RID: 42056 RVA: 0x0022CB20 File Offset: 0x0022AD20
		public IReadOnlyList<IOutputSuggestion> SuggestOutput(IRow inputRow, string prefix, SuggestOutputOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			IReadOnlyList<OutputSuggestion> readOnlyList = null;
			if (options == null)
			{
				options = new SuggestOutputOptions();
			}
			IReadOnlyList<IOutputSuggestion> readOnlyList2;
			try
			{
				if (base.Constraints.OfType<Example<IRow, object>>().ToList<Example<IRow, object>>().Any<Example<IRow, object>>())
				{
					IProgram program = base.Learn(null, cancellationToken, null);
					if (program == null)
					{
						readOnlyList2 = Enumerable.Empty<IOutputSuggestion>().ToArray<IOutputSuggestion>();
					}
					else
					{
						readOnlyList = (readOnlyList2 = (from <>h__TransparentIdentifier0 in (from output in program.Run(inputRow).Yield<object>()
								select new
								{
									output = output,
									stringOutput = (output as string)
								}).Where(delegate(<>h__TransparentIdentifier0)
							{
								string stringOutput = <>h__TransparentIdentifier0.stringOutput;
								return stringOutput != null && stringOutput.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
							})
							select new OutputSuggestion
							{
								Score = 1.0,
								Text = <>h__TransparentIdentifier0.stringOutput
							}).ToList<OutputSuggestion>());
					}
				}
				else
				{
					readOnlyList = (readOnlyList2 = new SuggestOutput(inputRow, options, Learner.ResolveLearnOptions(base.Constraints, base.Inputs), cancellationToken).Get(prefix, cancellationToken).Take(options.SuggestionLimit).ToList<OutputSuggestion>());
				}
			}
			catch (Exception ex)
			{
				ILogger logger = base.Logger;
				if (logger != null)
				{
					logger.TrackException(ex);
				}
				throw;
			}
			finally
			{
				try
				{
					this.TrackSuggestOutput(inputRow, prefix, options, readOnlyList, false);
				}
				catch (Exception ex2)
				{
					ILogger logger2 = base.Logger;
					if (logger2 != null)
					{
						logger2.TrackException(ex2);
					}
				}
			}
			return readOnlyList2;
		}

		// Token: 0x0600A449 RID: 42057 RVA: 0x0022CCC8 File Offset: 0x0022AEC8
		public IReadOnlyList<IOutputSuggestion> SuggestOutputTokens(IRow inputRow, SuggestOutputOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			IReadOnlyList<OutputSuggestion> readOnlyList = null;
			if (options == null)
			{
				options = new SuggestOutputOptions();
			}
			IReadOnlyList<IOutputSuggestion> readOnlyList2;
			try
			{
				readOnlyList = (readOnlyList2 = new SuggestOutput(inputRow, options, Learner.ResolveLearnOptions(base.Constraints, base.Inputs), cancellationToken).GetTokens(cancellationToken).Take(options.SuggestionLimit).ToList<OutputSuggestion>());
			}
			catch (Exception ex)
			{
				ILogger logger = base.Logger;
				if (logger != null)
				{
					logger.TrackException(ex);
				}
				throw;
			}
			finally
			{
				try
				{
					this.TrackSuggestOutput(inputRow, null, options, readOnlyList, true);
				}
				catch (Exception ex2)
				{
					ILogger logger2 = base.Logger;
					if (logger2 != null)
					{
						logger2.TrackException(ex2);
					}
				}
			}
			return readOnlyList2;
		}

		// Token: 0x0600A44A RID: 42058 RVA: 0x0022CD78 File Offset: 0x0022AF78
		public static void WarmUp(TargetLanguage? target = null, IEnumerable<CultureInfo> cultures = null)
		{
			IReadOnlyList<CultureInfo> readOnlyList;
			if ((readOnlyList = ((cultures != null) ? cultures.ToReadOnlyList<CultureInfo>() : null)) == null)
			{
				CultureInfo[] array = new CultureInfo[2];
				array[0] = new CultureInfo("en-US");
				readOnlyList = array;
				array[1] = new CultureInfo("en-GB");
			}
			IReadOnlyList<CultureInfo> readOnlyList2 = readOnlyList;
			Session session = new Session(null, null, null);
			session.Constraints.Add(new LearnConstraint
			{
				DataCultures = readOnlyList2
			});
			DateTime dateTime = new DateTime(2021, 10, 31);
			DateTime dateTime2 = new DateTime(2020, 1, 22);
			DateTime dateTime3 = new DateTime(1992, 6, 4);
			DateTime dateTime4 = new DateTime(1974, 12, 25);
			Program program;
			foreach (CultureInfo cultureInfo in readOnlyList2)
			{
				session.Constraints.Add(new Example(new InputRow(new object[] { "Birthday1: " + dateTime.ToString("MM/dd/yyyy", cultureInfo) }), dateTime.ToString("MMMM yyyy", cultureInfo), false));
				session.Constraints.Add(new Example(new InputRow(new object[] { "Birthday2: " + dateTime2.ToString("MM/dd/yyyy", cultureInfo) }), dateTime2.ToString("MMMM yyyy", cultureInfo), false));
				session.Constraints.Add(new Example(new InputRow(new object[] { "Birthday3: " + dateTime3.ToString("MM/dd/yyyy", cultureInfo) }), dateTime3.ToString("MMMM yyyy", cultureInfo), false));
				session.Constraints.Add(new Example(new InputRow(new object[] { "Birthday4: " + dateTime4.ToString("MM/dd/yyyy", cultureInfo) }), dateTime4.ToString("MMMM yyyy", cultureInfo), false));
				program = session.Learn(null, default(CancellationToken), null);
				if (program != null && target != null)
				{
					session.Translate(target.Value, program, default(CancellationToken));
				}
				session.Constraints.Remove(session.Constraints.OfType<Example>());
				session.Constraints.Add(new Example(new InputRow(new object[] { "Total: " + 1000.1.ToString(cultureInfo) }), 1000.1.ToString("#,##0.00", cultureInfo), false));
				session.Constraints.Add(new Example(new InputRow(new object[] { "Total: " + 2020.87.ToString(cultureInfo) }), 2020.87.ToString("#,##0.00", cultureInfo), false));
				program = session.Learn(null, default(CancellationToken), null);
				if (program != null && target != null)
				{
					session.Translate(target.Value, program, default(CancellationToken));
				}
			}
			session.Constraints.Remove(session.Constraints.OfType<Example>());
			session.Constraints.Add(new Example(new InputRow(new object[] { "John", "Smith/11" }), "Smith, John 11", false));
			session.Constraints.Add(new Example(new InputRow(new object[] { "Julia", "Lewis/22" }), "Lewis, Julia 22", false));
			program = session.Learn(null, default(CancellationToken), null);
			if (program != null && target != null)
			{
				session.Translate(target.Value, program, default(CancellationToken));
			}
		}

		// Token: 0x0600A44B RID: 42059 RVA: 0x0022D190 File Offset: 0x0022B390
		public FormulaTranslation LearnAndTranslate(TargetLanguage target, RankingMode learnRankingMode = null, CancellationToken cancellationToken = default(CancellationToken), Guid? guid = null)
		{
			this.EnsureTargetConstraints(target);
			Program program = base.Learn(learnRankingMode, cancellationToken, guid);
			if (!(program == null))
			{
				return this.Translate(target, program, cancellationToken);
			}
			return null;
		}

		// Token: 0x0600A44C RID: 42060 RVA: 0x0022D1C3 File Offset: 0x0022B3C3
		public CSharpTranslation LearnAndTranslateCSharp(RankingMode learnRankingMode = null, CancellationToken cancellationToken = default(CancellationToken), Guid? guid = null)
		{
			return (CSharpTranslation)this.LearnAndTranslate(TargetLanguage.CSharp, learnRankingMode, cancellationToken, guid);
		}

		// Token: 0x0600A44D RID: 42061 RVA: 0x0022D1D5 File Offset: 0x0022B3D5
		public ExcelTranslation LearnAndTranslateExcel(RankingMode learnRankingMode = null, CancellationToken cancellationToken = default(CancellationToken), Guid? guid = null)
		{
			return (ExcelTranslation)this.LearnAndTranslate(TargetLanguage.Excel, learnRankingMode, cancellationToken, guid);
		}

		// Token: 0x0600A44E RID: 42062 RVA: 0x0022D1E6 File Offset: 0x0022B3E6
		public PandasTranslation LearnAndTranslatePandas(RankingMode learnRankingMode = null, CancellationToken cancellationToken = default(CancellationToken), Guid? guid = null)
		{
			return (PandasTranslation)this.LearnAndTranslate(TargetLanguage.Pandas, learnRankingMode, cancellationToken, guid);
		}

		// Token: 0x0600A44F RID: 42063 RVA: 0x0022D1F7 File Offset: 0x0022B3F7
		public PowerAutomateTranslation LearnAndTranslatePowerAutomate(RankingMode learnRankingMode = null, CancellationToken cancellationToken = default(CancellationToken), Guid? guid = null)
		{
			return (PowerAutomateTranslation)this.LearnAndTranslate(TargetLanguage.PowerAutomate, learnRankingMode, cancellationToken, guid);
		}

		// Token: 0x0600A450 RID: 42064 RVA: 0x0022D20C File Offset: 0x0022B40C
		public PowerFxTranslation LearnAndTranslatePowerFx(RankingMode learnRankingMode = null, CancellationToken cancellationToken = default(CancellationToken), Guid? guid = null)
		{
			return (PowerFxTranslation)this.LearnAndTranslate(TargetLanguage.PowerApps, learnRankingMode, cancellationToken, null);
		}

		// Token: 0x0600A451 RID: 42065 RVA: 0x0022D230 File Offset: 0x0022B430
		public PowerQueryTranslation LearnAndTranslatePowerQueryM(RankingMode learnRankingMode = null, CancellationToken cancellationToken = default(CancellationToken), Guid? guid = null)
		{
			return (PowerQueryTranslation)this.LearnAndTranslate(TargetLanguage.PowerQueryM, learnRankingMode, cancellationToken, guid);
		}

		// Token: 0x0600A452 RID: 42066 RVA: 0x0022D241 File Offset: 0x0022B441
		public PySparkTranslation LearnAndTranslatePySpark(RankingMode learnRankingMode = null, CancellationToken cancellationToken = default(CancellationToken), Guid? guid = null)
		{
			return (PySparkTranslation)this.LearnAndTranslate(TargetLanguage.PySpark, learnRankingMode, cancellationToken, guid);
		}

		// Token: 0x0600A453 RID: 42067 RVA: 0x0022D252 File Offset: 0x0022B452
		public PythonTranslation LearnAndTranslatePython(RankingMode learnRankingMode = null, CancellationToken cancellationToken = default(CancellationToken), Guid? guid = null)
		{
			return (PythonTranslation)this.LearnAndTranslate(TargetLanguage.Python, learnRankingMode, cancellationToken, guid);
		}

		// Token: 0x0600A454 RID: 42068 RVA: 0x0022D263 File Offset: 0x0022B463
		public SqlTranslation LearnAndTranslateSql(RankingMode learnRankingMode = null, CancellationToken cancellationToken = default(CancellationToken), Guid? guid = null)
		{
			return (SqlTranslation)this.LearnAndTranslate(TargetLanguage.Sql, learnRankingMode, cancellationToken, guid);
		}

		// Token: 0x0600A455 RID: 42069 RVA: 0x0022D274 File Offset: 0x0022B474
		public FormulaTranslation Translate(TargetLanguage target, Program program, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (program == null)
			{
				return null;
			}
			IReadOnlyList<Example> readOnlyList = base.Constraints.OfType<Example>().ToList<Example>();
			if (!readOnlyList.Any<Example>())
			{
				throw new FormulaLearnException("Unable to determine examples.");
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.EnsureTargetConstraints(target);
			FormulaTranslation formulaTranslation = null;
			cancellationToken.ThrowIfCancellationRequested();
			try
			{
				ITranslationOptions translationOptions = this.ResolveTranslationOptions(target);
				LearnConstraint learnConstraint = base.Constraints.OfType<LearnConstraint>().FirstOrDefault<LearnConstraint>();
				bool flag = learnConstraint == null || learnConstraint.EnableMatchUnicode;
				FormulaExpression formulaExpression;
				switch (target)
				{
				case TargetLanguage.Python:
					formulaExpression = PythonProgramTranslator.Translate(program, readOnlyList, base.Inputs, (IPythonTranslationOptions)translationOptions, flag, base.Logger, cancellationToken);
					goto IL_01DB;
				case TargetLanguage.Pandas:
					formulaExpression = PandasProgramTranslator.Translate(program, (IPandasTranslationOptions)translationOptions, flag, readOnlyList, base.Inputs, base.Logger, cancellationToken);
					goto IL_01DB;
				case TargetLanguage.PySpark:
					formulaExpression = PySparkProgramTranslator.Translate(program, (IPySparkTranslationOptions)translationOptions, flag, readOnlyList, base.Inputs, base.Logger, cancellationToken);
					goto IL_01DB;
				case TargetLanguage.PowerQueryM:
					formulaExpression = PowerQueryProgramTranslator.Translate(program, readOnlyList, base.Inputs, (IPowerQueryTranslationOptions)translationOptions, base.Logger, cancellationToken);
					goto IL_01DB;
				case TargetLanguage.PowerApps:
					formulaExpression = PowerFxProgramTranslator.Translate(program, readOnlyList, base.Inputs, (IPowerFxTranslationOptions)translationOptions, base.Logger, cancellationToken);
					goto IL_01DB;
				case TargetLanguage.Excel:
					formulaExpression = ExcelProgramTranslator.Translate(program, readOnlyList, base.Inputs, (IExcelTranslationOptions)translationOptions, base.Logger, cancellationToken);
					goto IL_01DB;
				case TargetLanguage.Sql:
					formulaExpression = SqlProgramTranslator.Translate(program, (ISqlTranslationOptions)translationOptions, readOnlyList, base.Inputs, base.Logger, cancellationToken);
					goto IL_01DB;
				case TargetLanguage.PowerAutomate:
					formulaExpression = PowerAutomateProgramTranslator.Translate(program, readOnlyList, base.Inputs, (IPowerAutomateTranslationOptions)translationOptions, base.Logger, cancellationToken);
					goto IL_01DB;
				case TargetLanguage.CSharp:
					formulaExpression = CSharpProgramTranslator.Translate(program, readOnlyList, base.Inputs, (ICSharpTranslationOptions)translationOptions, base.Logger, cancellationToken);
					goto IL_01DB;
				}
				throw new ApplicationException(string.Format("Unsupported target language ({0})", target));
				IL_01DB:
				FormulaExpression formulaExpression2 = formulaExpression;
				cancellationToken.ThrowIfCancellationRequested();
				TranslationMeta translationMeta = TranslationMetaFactory.Compute(program, target, formulaExpression2, translationOptions, base.Logger, cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
				FormulaTranslation formulaTranslation2;
				switch (target)
				{
				case TargetLanguage.Python:
					formulaTranslation2 = new PythonTranslation(program, (PythonProgram)formulaExpression2, (IPythonTranslationOptions)translationOptions, translationMeta);
					goto IL_02FA;
				case TargetLanguage.Pandas:
					formulaTranslation2 = new PandasTranslation(program, (PythonProgram)formulaExpression2, (IPandasTranslationOptions)translationOptions, translationMeta);
					goto IL_02FA;
				case TargetLanguage.PySpark:
					formulaTranslation2 = new PySparkTranslation(program, (PythonProgram)formulaExpression2, (IPySparkTranslationOptions)translationOptions, translationMeta);
					goto IL_02FA;
				case TargetLanguage.PowerQueryM:
					formulaTranslation2 = new PowerQueryTranslation(program, formulaExpression2, translationMeta);
					goto IL_02FA;
				case TargetLanguage.PowerApps:
					formulaTranslation2 = new PowerFxTranslation(program, formulaExpression2, translationMeta);
					goto IL_02FA;
				case TargetLanguage.Excel:
					formulaTranslation2 = new ExcelTranslation(program, formulaExpression2, translationMeta);
					goto IL_02FA;
				case TargetLanguage.Sql:
					formulaTranslation2 = new SqlTranslation(program, formulaExpression2, translationMeta);
					goto IL_02FA;
				case TargetLanguage.PowerAutomate:
					formulaTranslation2 = new PowerAutomateTranslation(program, formulaExpression2, translationMeta);
					goto IL_02FA;
				case TargetLanguage.CSharp:
					formulaTranslation2 = new CSharpTranslation(program, (CSharpProgram)formulaExpression2, translationMeta);
					goto IL_02FA;
				}
				throw new ApplicationException(string.Format("Unsupported translation target language ({0})", target));
				IL_02FA:
				formulaTranslation = formulaTranslation2;
				cancellationToken.ThrowIfCancellationRequested();
				return formulaTranslation;
			}
			catch (Exception ex)
			{
				ILogger logger = base.Logger;
				if (logger != null)
				{
					logger.TrackException(ex);
				}
			}
			finally
			{
				stopwatch.Stop();
				this.TrackTranslation(formulaTranslation, new double?(stopwatch.ElapsedMillisecondsAsDouble()));
			}
			return formulaTranslation;
		}

		// Token: 0x0600A456 RID: 42070 RVA: 0x0022D5F4 File Offset: 0x0022B7F4
		public CSharpTranslation TranslateCSharp(Program program, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (CSharpTranslation)this.Translate(TargetLanguage.CSharp, program, cancellationToken);
		}

		// Token: 0x0600A457 RID: 42071 RVA: 0x0022D605 File Offset: 0x0022B805
		public ExcelTranslation TranslateExcel(Program program, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (ExcelTranslation)this.Translate(TargetLanguage.Excel, program, cancellationToken);
		}

		// Token: 0x0600A458 RID: 42072 RVA: 0x0022D615 File Offset: 0x0022B815
		public PandasTranslation TranslatePandas(Program program, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (PandasTranslation)this.Translate(TargetLanguage.Pandas, program, cancellationToken);
		}

		// Token: 0x0600A459 RID: 42073 RVA: 0x0022D625 File Offset: 0x0022B825
		public PowerAutomateTranslation TranslatePowerAutomate(Program program, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (PowerAutomateTranslation)this.Translate(TargetLanguage.PowerAutomate, program, cancellationToken);
		}

		// Token: 0x0600A45A RID: 42074 RVA: 0x0022D636 File Offset: 0x0022B836
		public PowerFxTranslation TranslatePowerFx(Program program, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (PowerFxTranslation)this.Translate(TargetLanguage.PowerApps, program, cancellationToken);
		}

		// Token: 0x0600A45B RID: 42075 RVA: 0x0022D646 File Offset: 0x0022B846
		public PowerQueryTranslation TranslatePowerQueryM(Program program, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (PowerQueryTranslation)this.Translate(TargetLanguage.PowerQueryM, program, cancellationToken);
		}

		// Token: 0x0600A45C RID: 42076 RVA: 0x0022D656 File Offset: 0x0022B856
		public PySparkTranslation TranslatePySpark(Program program, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (PySparkTranslation)this.Translate(TargetLanguage.PySpark, program, cancellationToken);
		}

		// Token: 0x0600A45D RID: 42077 RVA: 0x0022D666 File Offset: 0x0022B866
		public PythonTranslation TranslatePython(Program program, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (PythonTranslation)this.Translate(TargetLanguage.Python, program, cancellationToken);
		}

		// Token: 0x0600A45E RID: 42078 RVA: 0x0022D676 File Offset: 0x0022B876
		public SqlTranslation TranslateSql(Program program, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (SqlTranslation)this.Translate(TargetLanguage.Sql, program, cancellationToken);
		}

		// Token: 0x0600A45F RID: 42079 RVA: 0x0022D688 File Offset: 0x0022B888
		private void TrackSuggestOutput(IRow inputRow, string prefix, SuggestOutputOptions options, IEnumerable<OutputSuggestion> suggestions, bool includeAll)
		{
			if (base.Logger == null)
			{
				return;
			}
			var enumerable = suggestions.Select((OutputSuggestion suggestion) => new
			{
				Score = suggestion.Score,
				Text = suggestion.Text.ToAnonymizedString()
			});
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["InputRow"] = inputRow.ToString().ToAnonymizedString();
			dictionary["Prefix"] = prefix.ToAnonymizedString();
			dictionary["Options"] = JsonConvert.SerializeObject(options, Formatting.None);
			dictionary["IncludeAll"] = includeAll.ToString();
			dictionary["Suggestions"] = JsonConvert.SerializeObject(enumerable, Formatting.None);
			Dictionary<string, string> dictionary2 = dictionary;
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
			dictionary3["InputRow"] = inputRow.ToString();
			dictionary3["Prefix"] = prefix;
			dictionary3["Options"] = JsonConvert.SerializeObject(options, Formatting.None);
			dictionary3["IncludeAll"] = includeAll.ToString();
			dictionary3["Suggestions"] = JsonConvert.SerializeObject(suggestions, Formatting.None);
			Dictionary<string, string> dictionary4 = dictionary3;
			base.Logger.TrackEvent("AutoCompleterSuggestEvent", null, dictionary2, dictionary4);
		}

		// Token: 0x0600A460 RID: 42080 RVA: 0x0022D798 File Offset: 0x0022B998
		private void TrackTranslation(FormulaTranslation translation, double? translateTime = null)
		{
			if (base.Logger == null || translation == null)
			{
				return;
			}
			Session.<>c__DisplayClass27_0 CS$<>8__locals1;
			CS$<>8__locals1.trackMetrics = new Dictionary<string, double>();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			TranslationMeta meta = translation.Meta;
			Session.<TrackTranslation>g__AddMetric|27_1("Success", Session.<TrackTranslation>g__ConvertBool|27_3(new bool?(meta != null && meta.Valid)), ref CS$<>8__locals1);
			if (meta != null)
			{
				Session.<TrackTranslation>g__AddMetric|27_1("TranslateTime", translateTime, ref CS$<>8__locals1);
				Session.<TrackTranslation>g__AddMetric|27_1("MetadataTime", meta.MetadataTime, ref CS$<>8__locals1);
				string text = "SuppressReason";
				SuppressReason? suppressReason = meta.SuppressReason;
				Session.<TrackTranslation>g__AddIntMetric|27_0(text, (suppressReason != null) ? new uint?((uint)suppressReason.GetValueOrDefault()) : null, ref CS$<>8__locals1);
				Session.<TrackTranslation>g__AddMetric|27_1("HighPrecision", Session.<TrackTranslation>g__ConvertBool|27_3(meta.HighPrecision), ref CS$<>8__locals1);
				string text2 = "HighPrecisionScore";
				float? num = meta.HighPrecisionScore;
				Session.<TrackTranslation>g__AddMetric|27_1(text2, (num != null) ? new double?((double)num.GetValueOrDefault()) : null, ref CS$<>8__locals1);
				Session.<TrackTranslation>g__AddMetric|27_1("HighAcceptance", Session.<TrackTranslation>g__ConvertBool|27_3(meta.HighAcceptance), ref CS$<>8__locals1);
				string text3 = "HighAcceptanceScore";
				num = meta.HighAcceptanceScore;
				Session.<TrackTranslation>g__AddMetric|27_1(text3, (num != null) ? new double?((double)num.GetValueOrDefault()) : null, ref CS$<>8__locals1);
				string text4 = "Length";
				int? num2 = meta.Length;
				Session.<TrackTranslation>g__AddMetric|27_1(text4, (num2 != null) ? new double?((double)num2.GetValueOrDefault()) : null, ref CS$<>8__locals1);
				string text5 = "FunctionCount";
				num2 = meta.FunctionCount;
				Session.<TrackTranslation>g__AddMetric|27_1(text5, (num2 != null) ? new double?((double)num2.GetValueOrDefault()) : null, ref CS$<>8__locals1);
				string text6 = "MaxDepth";
				num2 = meta.MaxDepth;
				Session.<TrackTranslation>g__AddMetric|27_1(text6, (num2 != null) ? new double?((double)num2.GetValueOrDefault()) : null, ref CS$<>8__locals1);
			}
			string text7 = null;
			try
			{
				FormulaExpression expression = translation.Meta.Expression;
				text7 = ((expression != null) ? expression.ToString() : null);
			}
			catch (Exception ex)
			{
				base.Logger.TrackException(ex);
			}
			string text8 = null;
			try
			{
				FormulaExpression expression2 = translation.Meta.Expression;
				text8 = ((expression2 != null) ? expression2.ToAnonymizedString() : null);
			}
			catch (Exception ex2)
			{
				base.Logger.TrackException(ex2);
			}
			Session.<TrackTranslation>g__AddProperty|27_2("Success", ((meta != null) ? new bool?(meta.Valid) : null).ToString(), dictionary);
			Session.<TrackTranslation>g__AddProperty|27_2("TranslatedProgramAnonymized", text8, dictionary);
			Session.<TrackTranslation>g__AddProperty|27_2("TranslatedProgramTarget", translation.Target.ToString(), dictionary);
			string text9 = "TranslatedProgramSuppressReason";
			string text10;
			if (meta == null)
			{
				text10 = null;
			}
			else
			{
				SuppressReason? suppressReason = meta.SuppressReason;
				text10 = ((suppressReason != null) ? suppressReason.GetValueOrDefault().ToString() : null);
			}
			Session.<TrackTranslation>g__AddProperty|27_2(text9, text10, dictionary);
			Session.<TrackTranslation>g__AddProperty|27_2("TranslatedProgram", text7, dictionary2);
			base.Logger.TrackEvent("Translate", CS$<>8__locals1.trackMetrics, dictionary, dictionary2);
		}

		// Token: 0x0600A461 RID: 42081 RVA: 0x0022DACC File Offset: 0x0022BCCC
		protected override IEnumerable<KeyValuePair<string, double>> TrackedLearningMetrics(LearnProgramRequest<Program, IRow, object> request, Program topProgram)
		{
			Session.<>c__DisplayClass28_0 CS$<>8__locals1;
			CS$<>8__locals1.metrics = new List<KeyValuePair<string, double>>(base.TrackedLearningMetrics(request, topProgram));
			try
			{
				List<Example<IRow, object>> list = base.Constraints.OfType<Example<IRow, object>>().ToList<Example<IRow, object>>();
				int num = list.Count<Example<IRow, object>>();
				CS$<>8__locals1.metrics.Add(KVP.Create<string, double>("NumExamples", Convert.ToDouble(num)));
				Example<IRow, object> example = list.FirstOrDefault<Example<IRow, object>>();
				int? num2;
				if (example == null)
				{
					num2 = null;
				}
				else
				{
					IEnumerable<string> columnNames = example.Input.ColumnNames;
					num2 = ((columnNames != null) ? new int?(columnNames.Count<string>()) : null);
				}
				int? num3 = num2;
				if (num3 != null)
				{
					CS$<>8__locals1.metrics.Add(KVP.Create<string, double>("NumInputColumns", Convert.ToDouble(num3.Value)));
				}
				Session.<TrackedLearningMetrics>g__AddMetric|28_0("Success", Session.<TrackedLearningMetrics>g__ConvertBool|28_1(new bool?(topProgram != null)), ref CS$<>8__locals1);
				Learner learner = this.LearnerFor(RankingMode.MostLikely) as Learner;
				bool flag;
				if (learner != null)
				{
					LearnConfidenceResult learnConfidence = learner.LearnConfidence;
					if (learnConfidence != null)
					{
						double? num4 = learnConfidence.Score;
						if (num4 == null || !double.IsNaN(num4.GetValueOrDefault()))
						{
							flag = true;
							goto IL_010F;
						}
					}
				}
				flag = false;
				IL_010F:
				if (flag)
				{
					string text = "LearnConfidence";
					LearnConfidenceResult learnConfidence2 = learner.LearnConfidence;
					double? num5;
					if (learnConfidence2 == null)
					{
						double? num4 = null;
						num5 = num4;
					}
					else
					{
						double? num4 = learnConfidence2.Score;
						num5 = ((num4 != null) ? new double?(num4.GetValueOrDefault().Truncate(4)) : null);
					}
					Session.<TrackedLearningMetrics>g__AddMetric|28_0(text, num5, ref CS$<>8__locals1);
				}
				ProgramMeta programMeta = ((topProgram != null) ? topProgram.Meta : null);
				if (programMeta != null)
				{
					string text2 = "NumDistinctExamples";
					int? num6 = programMeta.DistinctExampleCount;
					double? num4;
					double? num7;
					if (num6 == null)
					{
						num4 = null;
						num7 = num4;
					}
					else
					{
						num7 = new double?((double)num6.GetValueOrDefault());
					}
					Session.<TrackedLearningMetrics>g__AddMetric|28_0(text2, num7, ref CS$<>8__locals1);
					string text3 = "NumDistinctOutputs";
					num6 = programMeta.DistinctOutputCount;
					double? num8;
					if (num6 == null)
					{
						num4 = null;
						num8 = num4;
					}
					else
					{
						num8 = new double?((double)num6.GetValueOrDefault());
					}
					Session.<TrackedLearningMetrics>g__AddMetric|28_0(text3, num8, ref CS$<>8__locals1);
					string text4 = "NumConcats";
					num6 = programMeta.ConcatCount;
					double? num9;
					if (num6 == null)
					{
						num4 = null;
						num9 = num4;
					}
					else
					{
						num9 = new double?((double)num6.GetValueOrDefault());
					}
					Session.<TrackedLearningMetrics>g__AddMetric|28_0(text4, num9, ref CS$<>8__locals1);
					string text5 = "NumConstStrings";
					num6 = programMeta.StrCount;
					double? num10;
					if (num6 == null)
					{
						num4 = null;
						num10 = num4;
					}
					else
					{
						num10 = new double?((double)num6.GetValueOrDefault());
					}
					Session.<TrackedLearningMetrics>g__AddMetric|28_0(text5, num10, ref CS$<>8__locals1);
					string text6 = "NumMatches";
					num6 = programMeta.MatchCount;
					double? num11;
					if (num6 == null)
					{
						num4 = null;
						num11 = num4;
					}
					else
					{
						num11 = new double?((double)num6.GetValueOrDefault());
					}
					Session.<TrackedLearningMetrics>g__AddMetric|28_0(text6, num11, ref CS$<>8__locals1);
					string text7 = "MetadataTime";
					num4 = programMeta.MetadataTime;
					Session.<TrackedLearningMetrics>g__AddMetric|28_0(text7, (num4 != null) ? new double?(num4.GetValueOrDefault().Truncate(4)) : null, ref CS$<>8__locals1);
					Session.<TrackedLearningMetrics>g__AddMetric|28_0("ConstantOutput", Session.<TrackedLearningMetrics>g__ConvertBool|28_1(programMeta.ConstantOutput), ref CS$<>8__locals1);
					Session.<TrackedLearningMetrics>g__AddMetric|28_0("WholeColumnOutput", Session.<TrackedLearningMetrics>g__ConvertBool|28_1(programMeta.WholeColumnOutput), ref CS$<>8__locals1);
					Session.<TrackedLearningMetrics>g__AddMetric|28_0("ConsistentInput", Session.<TrackedLearningMetrics>g__ConvertBool|28_1(programMeta.ConsistentInput), ref CS$<>8__locals1);
					Session.<TrackedLearningMetrics>g__AddMetric|28_0("ConsistentOutput", Session.<TrackedLearningMetrics>g__ConvertBool|28_1(programMeta.ConsistentOutput), ref CS$<>8__locals1);
					string text8 = "OutputConstStrLength";
					num6 = programMeta.OutputConstStrLength;
					double? num12;
					if (num6 == null)
					{
						num4 = null;
						num12 = num4;
					}
					else
					{
						num12 = new double?((double)num6.GetValueOrDefault());
					}
					Session.<TrackedLearningMetrics>g__AddMetric|28_0(text8, num12, ref CS$<>8__locals1);
					string text9 = "OutputLengthStdDev";
					num4 = programMeta.OutputLengthStdDev;
					Session.<TrackedLearningMetrics>g__AddMetric|28_0(text9, (num4 != null) ? new double?(num4.GetValueOrDefault().Truncate(4)) : null, ref CS$<>8__locals1);
					string text10 = "OutputConstStrRatio";
					num4 = programMeta.OutputConstStrRatio;
					Session.<TrackedLearningMetrics>g__AddMetric|28_0(text10, (num4 != null) ? new double?(num4.GetValueOrDefault().Truncate(4)) : null, ref CS$<>8__locals1);
					string text11 = "OutputConstStrRatioAvg";
					num4 = programMeta.OutputConstStrRatioAvg;
					Session.<TrackedLearningMetrics>g__AddMetric|28_0(text11, (num4 != null) ? new double?(num4.GetValueOrDefault().Truncate(4)) : null, ref CS$<>8__locals1);
					ProgramIntentSummary intentSummary = programMeta.IntentSummary;
					if (intentSummary != null)
					{
						Session.<TrackedLearningMetrics>g__AddMetric|28_0("Intent", new double?((double)intentSummary.Intent), ref CS$<>8__locals1);
						Session.<TrackedLearningMetrics>g__AddMetric|28_0("IntentFlags", new double?((double)intentSummary.IntentFlags), ref CS$<>8__locals1);
					}
				}
			}
			catch (Exception ex)
			{
				base.Logger.TrackException(ex);
			}
			return CS$<>8__locals1.metrics;
		}

		// Token: 0x0600A462 RID: 42082 RVA: 0x0022DF60 File Offset: 0x0022C160
		protected override IEnumerable<KeyValuePair<string, string>> TrackedLearningProperties(LearnProgramRequest<Program, IRow, object> request, Program topProgram)
		{
			IReadOnlyList<Example> readOnlyList = base.Constraints.OfType<Example>().ToList<Example>();
			Session.<>c__DisplayClass29_0 CS$<>8__locals1;
			CS$<>8__locals1.properties = new List<KeyValuePair<string, string>>(base.TrackedLearningProperties(request, topProgram));
			try
			{
				Learner learner = this.LearnerFor(RankingMode.MostLikely) as Learner;
				if (learner != null && learner.LearnConfidence != null && learner.LearnConfidence.Reason != LearnConfidenceReason.None)
				{
					string text = "LearnConfidenceReason";
					LearnConfidenceResult learnConfidence = learner.LearnConfidence;
					Session.<TrackedLearningProperties>g__AddProperty|29_0(text, (learnConfidence != null) ? learnConfidence.Reason.ToString() : null, ref CS$<>8__locals1);
				}
				Session.<TrackedLearningProperties>g__AddProperty|29_0("ExamplesAnonymized", readOnlyList.ToAnonymizedJson().ToString(Formatting.None, Array.Empty<JsonConverter>()), ref CS$<>8__locals1);
				string text2 = "IntentName";
				string text3;
				if (topProgram == null)
				{
					text3 = null;
				}
				else
				{
					ProgramMeta meta = topProgram.Meta;
					text3 = ((meta != null) ? meta.IntentName : null);
				}
				Session.<TrackedLearningProperties>g__AddProperty|29_0(text2, text3, ref CS$<>8__locals1);
				string text4 = "IntentFlagNames";
				string text5;
				if (topProgram == null)
				{
					text5 = null;
				}
				else
				{
					ProgramMeta meta2 = topProgram.Meta;
					if (meta2 == null)
					{
						text5 = null;
					}
					else
					{
						ProgramIntentSummary intentSummary = meta2.IntentSummary;
						text5 = ((intentSummary != null) ? intentSummary.IntentFlags.ToString() : null);
					}
				}
				Session.<TrackedLearningProperties>g__AddProperty|29_0(text4, text5, ref CS$<>8__locals1);
				LearnOptions learnOptions = Learner.ResolveLearnOptions(base.Constraints, base.Inputs);
				if (learnOptions != null)
				{
					string text6 = learnOptions.ArithmeticStrategy.ToString();
					int arithmeticMinExampleCount = learnOptions.ArithmeticMinExampleCount;
					string text7 = learnOptions.DateTimeSources.ToString();
					bool enableArithmetic = learnOptions.EnableArithmetic;
					bool enableArithmeticConstants = learnOptions.EnableArithmeticConstants;
					bool enableConcat = learnOptions.EnableConcat;
					bool enableDateTimePart = learnOptions.EnableDateTimePart;
					bool enableDefaultColumnNamePriority = learnOptions.EnableDefaultColumnNamePriority;
					bool enableFromDateTimePart = learnOptions.EnableFromDateTimePart;
					bool enableFromNumberStr = learnOptions.EnableFromNumberStr;
					bool enableLearningShortCircuit = learnOptions.EnableLearningShortCircuit;
					bool enableLength = learnOptions.EnableLength;
					string text8 = learnOptions.EnableMatchNames.ToString();
					bool enableMatchUnicode = learnOptions.EnableMatchUnicode;
					bool enableNegativePosition = learnOptions.EnableNegativePosition;
					bool enableParseDateTimePartial = learnOptions.EnableParseDateTimePartial;
					bool enableProperCase = learnOptions.EnableProperCase;
					bool enableReplace = learnOptions.EnableReplace;
					bool enableRoundDateTime = learnOptions.EnableRoundDateTime;
					bool enableRoundNumber = learnOptions.EnableRoundNumber;
					bool enableSlice = learnOptions.EnableSlice;
					bool enableSliceBetween = learnOptions.EnableSliceBetween;
					bool enableSplit = learnOptions.EnableSplit;
					bool enableTimePart = learnOptions.EnableTimePart;
					bool enableTrim = learnOptions.EnableTrim;
					int numberFormatMaxLeadingDigits = learnOptions.NumberFormatMaxLeadingDigits;
					int numberRoundMinExampleCount = learnOptions.NumberRoundMinExampleCount;
					string text9 = learnOptions.NumberSources.ToString();
					IReadOnlyList<CultureInfo> dataCultures = learnOptions.DataCultures;
					IEnumerable<string> enumerable;
					if (dataCultures == null)
					{
						enumerable = null;
					}
					else
					{
						enumerable = dataCultures.Select((CultureInfo c) => c.Name);
					}
					string text10 = JObject.FromObject(new
					{
						ArithmeticStrategy = text6,
						ArithmeticMinExampleCount = arithmeticMinExampleCount,
						DateTimeSources = text7,
						EnableArithmetic = enableArithmetic,
						EnableArithmeticConstants = enableArithmeticConstants,
						EnableConcat = enableConcat,
						EnableDateTimePart = enableDateTimePart,
						EnableDefaultColumnNamePriority = enableDefaultColumnNamePriority,
						EnableFromDateTimePart = enableFromDateTimePart,
						EnableFromNumberStr = enableFromNumberStr,
						EnableLearningShortCircuit = enableLearningShortCircuit,
						EnableLength = enableLength,
						EnableMatchNames = text8,
						EnableMatchUnicode = enableMatchUnicode,
						EnableNegativePosition = enableNegativePosition,
						EnableParseDateTimePartial = enableParseDateTimePartial,
						EnableProperCase = enableProperCase,
						EnableReplace = enableReplace,
						EnableRoundDateTime = enableRoundDateTime,
						EnableRoundNumber = enableRoundNumber,
						EnableSlice = enableSlice,
						EnableSliceBetween = enableSliceBetween,
						EnableSplit = enableSplit,
						EnableTimePart = enableTimePart,
						EnableTrim = enableTrim,
						NumberFormatMaxLeadingDigits = numberFormatMaxLeadingDigits,
						NumberRoundMinExampleCount = numberRoundMinExampleCount,
						NumberSources = text9,
						DataCultures = enumerable.ToJoinString(","),
						FromNumbersColumnLimit = learnOptions.FromNumbersColumnLimit,
						EnableConditional = learnOptions.EnableConditional,
						ConditionalMaxBranches = learnOptions.ConditionalMaxBranches,
						ConditionalBranchMinExampleCount = learnOptions.ConditionalBranchMinExampleCount,
						MinLearnConfidence = learnOptions.MinLearnConfidence
					}).ToString(Formatting.None, Array.Empty<JsonConverter>());
					Session.<TrackedLearningProperties>g__AddProperty|29_0("LearnOptions", text10, ref CS$<>8__locals1);
				}
			}
			catch (Exception ex)
			{
				base.Logger.TrackException(ex);
			}
			return CS$<>8__locals1.properties;
		}

		// Token: 0x0600A463 RID: 42083 RVA: 0x0022E218 File Offset: 0x0022C418
		protected override IEnumerable<KeyValuePair<string, string>> TrackedLearningUserProperties(LearnProgramRequest<Program, IRow, object> request, Program topProgram, bool includeConstraints = true)
		{
			IReadOnlyList<Example> readOnlyList = base.Constraints.OfType<Example>().ToList<Example>();
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>(base.TrackedLearningUserProperties(request, topProgram, includeConstraints));
			try
			{
				list.Add(KVP.Create<string, string>("Examples", readOnlyList.ToJson().ToString(Formatting.None, Array.Empty<JsonConverter>())));
				list.Add(KVP.Create<string, string>("TopProgramString", (topProgram != null) ? topProgram.ToString() : null));
			}
			catch (Exception ex)
			{
				base.Logger.TrackException(ex);
			}
			return list;
		}

		// Token: 0x0600A464 RID: 42084 RVA: 0x0022E2A4 File Offset: 0x0022C4A4
		private void EnsureTargetConstraints(TargetLanguage target)
		{
			if (target == TargetLanguage.CSharp)
			{
				this.<EnsureTargetConstraints>g__ApplyTargetConstraints|31_0<CSharpLearnConstraint, CSharpTranslationConstraint>();
				return;
			}
			if (target == TargetLanguage.Excel)
			{
				this.<EnsureTargetConstraints>g__ApplyTargetConstraints|31_0<ExcelLearnConstraint, ExcelTranslationConstraint>();
				return;
			}
			if (target == TargetLanguage.PowerApps)
			{
				this.<EnsureTargetConstraints>g__ApplyTargetConstraints|31_0<PowerFxLearnConstraint, PowerFxTranslationConstraint>();
				return;
			}
			if (target == TargetLanguage.PowerAutomate)
			{
				this.<EnsureTargetConstraints>g__ApplyTargetConstraints|31_0<PowerAutomateLearnConstraint, PowerAutomateTranslationConstraint>();
				return;
			}
			if (target == TargetLanguage.PowerQueryM)
			{
				this.<EnsureTargetConstraints>g__ApplyTargetConstraints|31_0<PowerQueryLearnConstraint, PowerQueryTranslationConstraint>();
				return;
			}
			if (target == TargetLanguage.Python)
			{
				this.<EnsureTargetConstraints>g__ApplyTargetConstraints|31_0<PythonLearnConstraint, PythonTranslationConstraint>();
				return;
			}
			if (target == TargetLanguage.PySpark)
			{
				this.<EnsureTargetConstraints>g__ApplyTargetConstraints|31_0<PySparkLearnConstraint, PySparkTranslationConstraint>();
				return;
			}
			if (target == TargetLanguage.Pandas)
			{
				this.<EnsureTargetConstraints>g__ApplyTargetConstraints|31_0<PandasLearnConstraint, PandasTranslationConstraint>();
				return;
			}
			if (target == TargetLanguage.Sql)
			{
				this.<EnsureTargetConstraints>g__ApplyTargetConstraints|31_0<SqlLearnConstraint, SqlTranslationConstraint>();
			}
		}

		// Token: 0x0600A465 RID: 42085 RVA: 0x0022E314 File Offset: 0x0022C514
		private ITranslationOptions ResolveTranslationOptions(TargetLanguage target)
		{
			switch (target)
			{
			case TargetLanguage.Python:
				return base.Constraints.OfType<IPythonTranslationOptions>().SingleOrDefault<IPythonTranslationOptions>();
			case TargetLanguage.Pandas:
				return base.Constraints.OfType<IPandasTranslationOptions>().SingleOrDefault<IPandasTranslationOptions>();
			case TargetLanguage.PySpark:
				return base.Constraints.OfType<IPySparkTranslationOptions>().SingleOrDefault<IPySparkTranslationOptions>();
			case TargetLanguage.PowerQueryM:
				return base.Constraints.OfType<IPowerQueryTranslationOptions>().SingleOrDefault<IPowerQueryTranslationOptions>();
			case TargetLanguage.PowerApps:
				return base.Constraints.OfType<IPowerFxTranslationOptions>().SingleOrDefault<IPowerFxTranslationOptions>();
			case TargetLanguage.Excel:
				return base.Constraints.OfType<IExcelTranslationOptions>().SingleOrDefault<IExcelTranslationOptions>();
			case TargetLanguage.Sql:
				return base.Constraints.OfType<ISqlTranslationOptions>().SingleOrDefault<ISqlTranslationOptions>();
			case TargetLanguage.PowerAutomate:
				return base.Constraints.OfType<IPowerAutomateTranslationOptions>().SingleOrDefault<IPowerAutomateTranslationOptions>();
			case TargetLanguage.CSharp:
				return base.Constraints.OfType<ICSharpTranslationOptions>().SingleOrDefault<ICSharpTranslationOptions>();
			}
			throw new Exception(string.Format("Unable to determine ITranslationOptions ({0})", target));
		}

		// Token: 0x0600A466 RID: 42086 RVA: 0x0022E424 File Offset: 0x0022C624
		[CompilerGenerated]
		internal static void <TrackTranslation>g__AddIntMetric|27_0(string name, uint? metricValue, ref Session.<>c__DisplayClass27_0 A_2)
		{
			if (metricValue == null)
			{
				return;
			}
			uint? num = metricValue;
			Session.<TrackTranslation>g__AddMetric|27_1(name, (num != null) ? new double?(num.GetValueOrDefault()) : null, ref A_2);
		}

		// Token: 0x0600A467 RID: 42087 RVA: 0x0022E466 File Offset: 0x0022C666
		[CompilerGenerated]
		internal static void <TrackTranslation>g__AddMetric|27_1(string name, double? metricValue, ref Session.<>c__DisplayClass27_0 A_2)
		{
			if (metricValue == null)
			{
				return;
			}
			A_2.trackMetrics[name] = metricValue.Value;
		}

		// Token: 0x0600A468 RID: 42088 RVA: 0x0022E485 File Offset: 0x0022C685
		[CompilerGenerated]
		internal static void <TrackTranslation>g__AddProperty|27_2(string name, string propValue, IDictionary<string, string> dict)
		{
			if (propValue == null)
			{
				return;
			}
			dict[name] = propValue;
		}

		// Token: 0x0600A469 RID: 42089 RVA: 0x0022E494 File Offset: 0x0022C694
		[CompilerGenerated]
		internal static double? <TrackTranslation>g__ConvertBool|27_3(bool? value)
		{
			if (value != null)
			{
				return new double?(value.Value > false);
			}
			return null;
		}

		// Token: 0x0600A46A RID: 42090 RVA: 0x0022E4C4 File Offset: 0x0022C6C4
		[CompilerGenerated]
		internal static void <TrackedLearningMetrics>g__AddMetric|28_0(string name, double? metricValue, ref Session.<>c__DisplayClass28_0 A_2)
		{
			if (metricValue == null)
			{
				return;
			}
			A_2.metrics.Add(KVP.Create<string, double>(name, metricValue.Value));
		}

		// Token: 0x0600A46B RID: 42091 RVA: 0x0022E4E8 File Offset: 0x0022C6E8
		[CompilerGenerated]
		internal static double? <TrackedLearningMetrics>g__ConvertBool|28_1(bool? value)
		{
			if (value != null)
			{
				return new double?(value.Value > false);
			}
			return null;
		}

		// Token: 0x0600A46C RID: 42092 RVA: 0x0022E518 File Offset: 0x0022C718
		[CompilerGenerated]
		internal static void <TrackedLearningProperties>g__AddProperty|29_0(string name, string propertyValue, ref Session.<>c__DisplayClass29_0 A_2)
		{
			if (propertyValue == null)
			{
				return;
			}
			A_2.properties.Add(KVP.Create<string, string>(name, propertyValue));
		}

		// Token: 0x0600A46D RID: 42093 RVA: 0x0022E530 File Offset: 0x0022C730
		[CompilerGenerated]
		private void <EnsureTargetConstraints>g__ApplyTargetConstraints|31_0<TLearnConstraint, TTranslationConstraint>() where TLearnConstraint : LearnConstraint, new() where TTranslationConstraint : Constraint<IRow, object>, IUniqueConstraint<TTranslationConstraint>, new()
		{
			if (!base.Constraints.OfType<LearnConstraint>().Any<LearnConstraint>())
			{
				base.Constraints.Add(new TLearnConstraint());
			}
			if (!base.Constraints.OfType<TTranslationConstraint>().Any<TTranslationConstraint>())
			{
				base.Constraints.Add(new TTranslationConstraint());
			}
		}
	}
}
