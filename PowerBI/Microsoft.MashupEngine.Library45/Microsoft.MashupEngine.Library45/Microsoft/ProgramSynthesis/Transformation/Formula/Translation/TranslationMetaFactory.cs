using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Formula.Exceptions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Explanations;
using Microsoft.ProgramSynthesis.Transformation.Formula.Explanations.Default;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x02001812 RID: 6162
	internal class TranslationMetaFactory
	{
		// Token: 0x0600CA7C RID: 51836 RVA: 0x002B4128 File Offset: 0x002B2328
		private TranslationMetaFactory(Program program, TargetLanguage target, FormulaExpression expression, ITranslationOptions options, ILogger logger, CancellationToken cancellation)
		{
			this._program = program;
			this._target = target;
			this._expression = expression;
			this._options = options;
			this._logger = logger;
			this._cancellation = cancellation;
		}

		// Token: 0x0600CA7D RID: 51837 RVA: 0x002B415D File Offset: 0x002B235D
		public static TranslationMeta Compute(Program program, TargetLanguage target, FormulaExpression expression, ITranslationOptions options, ILogger logger, CancellationToken cancellation)
		{
			return new TranslationMetaFactory(program, target, expression, options, logger, cancellation).ComputeInternal();
		}

		// Token: 0x0600CA7E RID: 51838 RVA: 0x002B4174 File Offset: 0x002B2374
		private TranslationMeta ComputeInternal()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			if (this._expression == null)
			{
				stopwatch.Stop();
				return new TranslationMeta
				{
					Valid = false,
					SuppressReason = new SuppressReason?(SuppressReason.Unknown),
					MetadataTime = new double?(stopwatch.ElapsedMillisecondsAsDouble())
				};
			}
			SuppressReason? suppressReason = null;
			bool? flag = null;
			float? num = null;
			bool? flag2 = null;
			float? num2 = null;
			int? num3 = null;
			int? num4 = null;
			int? num5 = null;
			IExplanationMeta explanationMeta = null;
			try
			{
				this._formulaExtractItems = this._expression.NodeDetails;
				IReadOnlyList<FormulaExpressionDetail> formulaExtractItems = this._formulaExtractItems;
				if (formulaExtractItems == null || !formulaExtractItems.Any<FormulaExpressionDetail>())
				{
					return new TranslationMeta
					{
						Valid = false
					};
				}
				num3 = new int?(this._expression.ToString().Length);
				num4 = new int?(this._formulaExtractItems.Count((FormulaExpressionDetail n) => n.Node is FormulaFunc));
				num5 = new int?(this._formulaExtractItems.Max((FormulaExpressionDetail i) => i.Depth));
				this._cancellation.ThrowIfCancellationRequested();
				ProgramMeta meta = this._program.Meta;
				bool valueOrDefault = ((meta != null) ? meta.ConstantOutput : null).GetValueOrDefault();
				bool valueOrDefault2 = ((meta != null) ? meta.WholeColumnOutput : null).GetValueOrDefault();
				bool flag3 = ((meta != null) ? meta.ConsistentOutput : null) ?? true;
				if (this._options.SuppressConstantOutput && valueOrDefault)
				{
					suppressReason = new SuppressReason?(SuppressReason.ConstantOutput);
				}
				if (suppressReason == null && this._options.SuppressWholeColumnOutput && valueOrDefault2)
				{
					suppressReason = new SuppressReason?(SuppressReason.WholeColumnOutput);
				}
				if (suppressReason == null && this._options.SuppressInconsistentOutput && !flag3)
				{
					suppressReason = new SuppressReason?(SuppressReason.InconsistentOutput);
				}
				try
				{
					SuppressionBehavior suppressionBehavior = this._options.SuppressLowPrecision;
					bool flag4 = suppressionBehavior - SuppressionBehavior.Predict <= 1;
					if (flag4)
					{
						flag = this.PredictHighPrecision(meta, num3.Value, num4.Value, num5.Value, out num);
					}
				}
				catch (Exception ex)
				{
					if (this._options.SuppressLowPrecision == SuppressionBehavior.Suppress)
					{
						throw;
					}
					ILogger logger = this._logger;
					if (logger != null)
					{
						logger.TrackException(ex);
					}
				}
				if (this._options.SuppressLowPrecision == SuppressionBehavior.Suppress)
				{
					if (flag == null)
					{
						throw new SuppressionNotAvailable("SuppressLowPrecision not available. Suppression is only available for environments running netstandard2.0 and above.");
					}
					if (suppressReason == null && !flag.Value)
					{
						suppressReason = new SuppressReason?(SuppressReason.LowPrecision);
					}
				}
				this._cancellation.ThrowIfCancellationRequested();
				try
				{
					SuppressionBehavior suppressionBehavior = this._options.SuppressLowAcceptance;
					bool flag4 = suppressionBehavior - SuppressionBehavior.Predict <= 1;
					if (flag4)
					{
						flag2 = this.PredictHighAcceptance(meta, num3.Value, num4.Value, num5.Value, out num2);
					}
				}
				catch (Exception ex2)
				{
					if (this._options.SuppressLowAcceptance == SuppressionBehavior.Suppress)
					{
						throw;
					}
					ILogger logger2 = this._logger;
					if (logger2 != null)
					{
						logger2.TrackException(ex2);
					}
				}
				if (this._options.SuppressLowAcceptance == SuppressionBehavior.Suppress)
				{
					if (flag2 == null)
					{
						throw new SuppressionNotAvailable("SuppressLowAcceptance not available. ");
					}
					if (suppressReason == null && !flag2.Value)
					{
						suppressReason = new SuppressReason?(SuppressReason.LowAcceptance);
					}
				}
				this._cancellation.ThrowIfCancellationRequested();
				IExplanationMeta explanationMeta3;
				if (this._options.ExplanationTemplateProvider == null)
				{
					IExplanationMeta explanationMeta2 = new EmptyExplanationMeta();
					explanationMeta3 = explanationMeta2;
				}
				else
				{
					explanationMeta3 = ExplanationMetaFactory.Compute(this._program, this._options.ExplanationTemplateProvider, this._logger);
				}
				explanationMeta = explanationMeta3;
				this._cancellation.ThrowIfCancellationRequested();
			}
			catch (Exception ex3) when (ex3 is OperationCanceledException)
			{
				ILogger logger3 = this._logger;
				if (logger3 != null)
				{
					logger3.TrackException(ex3);
				}
				throw;
			}
			catch (Exception ex4)
			{
				ILogger logger4 = this._logger;
				if (logger4 != null)
				{
					logger4.TrackException(ex4);
				}
			}
			stopwatch.Stop();
			return new TranslationMeta
			{
				Valid = (suppressReason == null),
				SuppressReason = suppressReason,
				Expression = this._expression,
				MetadataTime = new double?(stopwatch.ElapsedMillisecondsAsDouble()),
				HighPrecision = flag,
				HighPrecisionScore = num,
				HighAcceptance = flag2,
				HighAcceptanceScore = num2,
				Length = num3,
				FunctionCount = num4,
				MaxDepth = num5,
				Explanation = explanationMeta
			};
		}

		// Token: 0x0600CA7F RID: 51839 RVA: 0x002B4684 File Offset: 0x002B2884
		private bool? PredictHighAcceptance(ProgramMeta programMeta, int formulaLength, int formulaFunctionCount, int formulaMaxDepth, out float? score)
		{
			score = null;
			if (this._options.SuppressLowAcceptance == SuppressionBehavior.None)
			{
				return null;
			}
			bool? flag;
			if (this._target == TargetLanguage.Excel)
			{
				flag = TranslationMetaFactory.PredictHighAcceptanceExcel(programMeta, formulaLength, formulaFunctionCount, formulaMaxDepth, out score);
			}
			else
			{
				flag = null;
			}
			return flag;
		}

		// Token: 0x0600CA80 RID: 51840 RVA: 0x002B46D4 File Offset: 0x002B28D4
		private bool? PredictHighPrecision(ProgramMeta programMeta, int formulaLength, int formulaFunctionCount, int formulaMaxDepth, out float? score)
		{
			score = null;
			if (this._options.SuppressLowPrecision == SuppressionBehavior.None)
			{
				return null;
			}
			bool? flag;
			if (this._target == TargetLanguage.Excel)
			{
				flag = TranslationMetaFactory.PredictHighPrecisionExcel(programMeta, formulaLength, formulaFunctionCount, formulaMaxDepth, out score);
			}
			else
			{
				flag = null;
			}
			return flag;
		}

		// Token: 0x0600CA81 RID: 51841 RVA: 0x002B4724 File Offset: 0x002B2924
		private static bool? PredictHighAcceptanceExcel(ProgramMeta programMeta, int formulaLength, int formulaFunctionCount, int formulaMaxDepth, out float? score)
		{
			score = null;
			return null;
		}

		// Token: 0x0600CA82 RID: 51842 RVA: 0x002B4744 File Offset: 0x002B2944
		private static bool? PredictHighPrecisionExcel(ProgramMeta programMeta, int formulaLength, int formulaFunctionCount, int formulaMaxDepth, out float? score)
		{
			score = null;
			return null;
		}

		// Token: 0x04004F88 RID: 20360
		private readonly CancellationToken _cancellation;

		// Token: 0x04004F89 RID: 20361
		private readonly FormulaExpression _expression;

		// Token: 0x04004F8A RID: 20362
		private IReadOnlyList<FormulaExpressionDetail> _formulaExtractItems;

		// Token: 0x04004F8B RID: 20363
		private readonly ILogger _logger;

		// Token: 0x04004F8C RID: 20364
		private readonly ITranslationOptions _options;

		// Token: 0x04004F8D RID: 20365
		private readonly Program _program;

		// Token: 0x04004F8E RID: 20366
		private readonly TargetLanguage _target;
	}
}
