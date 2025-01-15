using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Explanations.Default
{
	// Token: 0x020019C7 RID: 6599
	public class ExplanationMetaFactory
	{
		// Token: 0x0600D770 RID: 55152 RVA: 0x002DC568 File Offset: 0x002DA768
		private ExplanationMetaFactory(PipelineModel pipelineModel, IExplanationTemplateProvider templateProvider, ILogger logger)
		{
			this._pipelineModel = pipelineModel;
			this._logger = logger;
			this._templateProvider = templateProvider;
		}

		// Token: 0x0600D771 RID: 55153 RVA: 0x002DC585 File Offset: 0x002DA785
		public static IExplanationMeta Compute(Program program, IExplanationTemplateProvider templateProvider, ILogger logger = null)
		{
			ProgramMeta meta = program.Meta;
			return ExplanationMetaFactory.Compute((meta != null) ? meta.Pipeline : null, templateProvider, logger);
		}

		// Token: 0x0600D772 RID: 55154 RVA: 0x002DC5A0 File Offset: 0x002DA7A0
		public static IExplanationMeta Compute(PipelineModel pipelineModel, IExplanationTemplateProvider templateProvider, ILogger logger = null)
		{
			if (pipelineModel != null)
			{
				return new ExplanationMetaFactory(pipelineModel, templateProvider, logger).ComputeIntental();
			}
			return null;
		}

		// Token: 0x0600D773 RID: 55155 RVA: 0x002DC5B4 File Offset: 0x002DA7B4
		private IExplanationMeta ComputeIntental()
		{
			if (this._pipelineModel != null)
			{
				return this.ComputeIntental(this._pipelineModel);
			}
			return null;
		}

		// Token: 0x0600D774 RID: 55156 RVA: 0x002DC5CC File Offset: 0x002DA7CC
		private IExplanationMeta ComputeIntental(PipelineModel model)
		{
			IExplanationMeta explanationMeta = new EmptyExplanationMeta();
			try
			{
				ConcatModel concatModel = model as ConcatModel;
				IExplanationMeta explanationMeta2;
				if (concatModel == null)
				{
					ArithmeticModel arithmeticModel = model as ArithmeticModel;
					if (arithmeticModel == null)
					{
						ArithmeticAggregateModel arithmeticAggregateModel = model as ArithmeticAggregateModel;
						if (arithmeticAggregateModel == null)
						{
							explanationMeta2 = this.ResolveTransform(model);
						}
						else
						{
							explanationMeta2 = this.ResolveArithmeticAggregate(arithmeticAggregateModel);
						}
					}
					else
					{
						explanationMeta2 = this.ResolveArithmetic(arithmeticModel);
					}
				}
				else
				{
					explanationMeta2 = this.ResolveConcat(concatModel);
				}
				explanationMeta = explanationMeta2;
			}
			catch (Exception ex)
			{
				ILogger logger = this._logger;
				if (logger != null)
				{
					logger.TrackException(ex);
				}
			}
			return explanationMeta;
		}

		// Token: 0x0600D775 RID: 55157 RVA: 0x002DC658 File Offset: 0x002DA858
		private IExplanationMeta ResolveArithmetic(ArithmeticModel model)
		{
			List<string> list = new List<string> { model.Operator };
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (model.Left is FromNumberModel)
			{
				list.Add("LeftWholeColumn");
			}
			FromStrModel fromStrModel = model.Left as FromStrModel;
			if (fromStrModel != null)
			{
				StringTransformModel stringTransform = fromStrModel.StringTransform;
				if (stringTransform != null && stringTransform.HasExtractNumber)
				{
					list.Add("LeftExtractNumber");
				}
				else
				{
					StringTransformModel stringTransform2 = fromStrModel.StringTransform;
					if (stringTransform2 != null && stringTransform2.HasParseNumber)
					{
						list.Add("LeftParseNumber");
					}
				}
			}
			PipelineModel left = model.Left;
			if (!string.IsNullOrEmpty((left != null) ? left.ColumnName : null))
			{
				Dictionary<string, object> dictionary2 = dictionary;
				string text = "LeftColumnName";
				PipelineModel left2 = model.Left;
				dictionary2.Add(text, (left2 != null) ? left2.ColumnName : null);
			}
			if (model.Right is FromNumberModel)
			{
				list.Add("RightWholeColumn");
			}
			FromStrModel fromStrModel2 = model.Right as FromStrModel;
			if (fromStrModel2 != null)
			{
				StringTransformModel stringTransform3 = fromStrModel2.StringTransform;
				if (stringTransform3 != null && stringTransform3.HasExtractNumber)
				{
					list.Add("RightExtractNumber");
				}
				else
				{
					StringTransformModel stringTransform4 = fromStrModel2.StringTransform;
					if (stringTransform4 != null && stringTransform4.HasParseNumber)
					{
						list.Add("RightParseNumber");
					}
				}
			}
			NumberModel numberModel = model.Right as NumberModel;
			if (numberModel != null)
			{
				list.Add("RightConstant");
				dictionary.Add("RightConstant", numberModel.Value);
			}
			PipelineModel right = model.Right;
			if (!string.IsNullOrEmpty((right != null) ? right.ColumnName : null))
			{
				Dictionary<string, object> dictionary3 = dictionary;
				string text2 = "RightColumnName";
				PipelineModel right2 = model.Right;
				dictionary3.Add(text2, (right2 != null) ? right2.ColumnName : null);
			}
			return new ExplanationMeta(this._templateProvider, list, dictionary);
		}

		// Token: 0x0600D776 RID: 55158 RVA: 0x002DC7F8 File Offset: 0x002DA9F8
		private IExplanationMeta ResolveArithmeticAggregate(ArithmeticAggregateModel model)
		{
			List<string> list = new List<string> { model.Operator };
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ColumnNames"] = model.ColumnNames.ToJoinString(", ");
			Dictionary<string, object> dictionary2 = dictionary;
			return new ExplanationMeta(this._templateProvider, list, dictionary2);
		}

		// Token: 0x0600D777 RID: 55159 RVA: 0x002DC848 File Offset: 0x002DAA48
		private IExplanationMeta ResolveConcat(ConcatModel model)
		{
			List<string> list = new List<string> { "Concat" };
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			List<string> list2 = model.Children.Select(delegate(PipelineModel c)
			{
				StrModel strModel = c as StrModel;
				string text;
				if (strModel != null)
				{
					text = strModel.Value.ToCSharpPseudoLiteral();
				}
				else
				{
					IExplanationMeta explanationMeta = this.ComputeIntental(c);
					text = ((explanationMeta != null) ? explanationMeta.Text : null);
				}
				return text;
			}).ToList<string>();
			IEnumerable<string> enumerable = list2;
			Func<string, bool> func;
			if ((func = ExplanationMetaFactory.<>O.<0>__IsNullOrEmpty) == null)
			{
				func = (ExplanationMetaFactory.<>O.<0>__IsNullOrEmpty = new Func<string, bool>(string.IsNullOrEmpty));
			}
			if (enumerable.Any(func))
			{
				return new EmptyExplanationMeta();
			}
			dictionary.Add("List", list2.ToJoinNewlineString().Indent(2, false));
			return new ExplanationMeta(this._templateProvider, list, dictionary);
		}

		// Token: 0x0600D778 RID: 55160 RVA: 0x002DC8D8 File Offset: 0x002DAAD8
		private IExplanationMeta ResolveTransform(PipelineModel model)
		{
			List<string> list = new List<string>();
			if (model is FromNumberModel)
			{
				list.Add("Number");
			}
			if (model is FromDateTimeModel)
			{
				list.Add("DateTime");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ColumnName"] = model.ColumnName;
			Dictionary<string, object> dictionary2 = dictionary;
			if (model.HasStringTransform)
			{
				StringTransformModel stringTransform = model.StringTransform;
				if (stringTransform.HasSubstring)
				{
					if (stringTransform.HasSlice && stringTransform.Slice.IsPrefix)
					{
						list.Add("SlicePrefix");
						IntModel intModel = stringTransform.Slice.EndPosition as IntModel;
						if (intModel != null)
						{
							list.Add("Absolute");
							if (intModel.Value == 2)
							{
								list.Add("1");
							}
							else if (intModel.Value == -1)
							{
								list.Add("-1");
							}
							else
							{
								list.Add((intModel.Value > 0) ? "N" : "-N");
								dictionary2.Add("N", (intModel.Value > 0) ? (intModel.Value - 1) : (-intModel.Value));
							}
						}
						FindModel findModel = stringTransform.Slice.EndPosition as FindModel;
						bool flag;
						if (findModel != null)
						{
							int num = findModel.Offset;
							if (num <= 1 && findModel.Instance == 1)
							{
								flag = true;
								goto IL_014C;
							}
						}
						flag = false;
						IL_014C:
						if (flag)
						{
							list.Add("Find");
							dictionary2.Add("Delimiter", this._templateProvider.FormatDelimiter(findModel.Delimiter));
						}
					}
					if (stringTransform.HasSlice && stringTransform.Slice.IsSuffix)
					{
						list.Add("SliceSuffix");
						IntModel intModel2 = stringTransform.Slice.StartPosition as IntModel;
						if (intModel2 != null)
						{
							list.Add("Absolute");
							if (intModel2.Value == 2)
							{
								list.Add("1");
							}
							else if (intModel2.Value == -1)
							{
								list.Add("-1");
							}
							else
							{
								list.Add((intModel2.Value > 0) ? "N" : "-N");
								dictionary2.Add("N", (intModel2.Value > 0) ? (intModel2.Value - 1) : (-intModel2.Value));
							}
						}
						FindModel findModel2 = stringTransform.Slice.StartPosition as FindModel;
						bool flag;
						if (findModel2 != null)
						{
							int num = findModel2.Offset;
							if (num <= 1 && findModel2.Instance == -1)
							{
								flag = true;
								goto IL_026F;
							}
						}
						flag = false;
						IL_026F:
						if (flag)
						{
							list.Add("Find");
							dictionary2.Add("Delimiter", this._templateProvider.FormatDelimiter(findModel2.Delimiter));
						}
					}
					if (stringTransform.HasSlice && stringTransform.Slice.IsInfix)
					{
						list.Add("SliceInfix");
						IntModel intModel3 = stringTransform.Slice.StartPosition as IntModel;
						if (intModel3 != null)
						{
							IntModel intModel4 = stringTransform.Slice.EndPosition as IntModel;
							if (intModel4 != null)
							{
								list.Add((intModel3.Value > 0) ? "LeftAbsolute" : "-LeftAbsolute");
								dictionary2.Add("LeftPosition", (intModel3.Value > 0) ? intModel3.Value : (-intModel3.Value));
								list.Add((intModel4.Value > 0) ? "RightAbsolute" : "-RightAbsolute");
								dictionary2.Add("RightPosition", (intModel4.Value > 0) ? intModel4.Value : (-intModel4.Value));
								if (intModel3.Value > 0 && intModel4.Value > 0)
								{
									dictionary2.Add("Length", intModel4.Value - intModel3.Value);
								}
							}
						}
					}
					if (stringTransform.HasSplit)
					{
						list.Add("Split");
						int instance = stringTransform.Split.Instance;
						dictionary2.Add("Delimiter", this._templateProvider.FormatDelimiter(stringTransform.Split.Delimiter));
						list.Add((instance == 1) ? "1" : ((instance == -1) ? "-1" : ((instance > 0) ? "N" : "-N")));
						dictionary2.Add("N", Math.Abs(instance));
					}
				}
				if (!stringTransform.HasSubstring)
				{
					if (stringTransform.HasParseNumber)
					{
						list.Add("ParseNumber");
					}
					if (stringTransform.HasExtractNumber)
					{
						list.Add("ExtractNumber");
					}
					if (stringTransform.HasParseDateTime)
					{
						list.Add("ParseDateTime");
					}
					if (stringTransform.HasExtractDateTime)
					{
						list.Add("ExtractDateTime");
					}
					if (stringTransform.HasTrim)
					{
						list.Add("Trim");
					}
					if (stringTransform.HasLength)
					{
						list.Add("Length");
					}
					if (stringTransform.HasCase)
					{
						if (stringTransform.Case.LowerCase)
						{
							list.Add("LowerCase");
						}
						if (stringTransform.Case.UpperCase)
						{
							list.Add("UpperCase");
						}
						if (stringTransform.Case.ProperCase)
						{
							list.Add("ProperCase");
						}
					}
					if (stringTransform.HasReplace)
					{
						list.Add("Replace");
						dictionary2.Add("FindText", this._templateProvider.FormatDelimiter(stringTransform.Replace.FindText));
						dictionary2.Add("ReplaceText", this._templateProvider.FormatDelimiter(stringTransform.Replace.ReplaceText));
					}
				}
			}
			if (model.HasNumberTransform)
			{
				NumberTransformModel numberTransform = model.NumberTransform;
				if (numberTransform.HasRound)
				{
					list.Add("Round");
					list.Add(numberTransform.Round.Mode.ToString());
					dictionary2.Add("Delta", numberTransform.Round.Delta);
				}
				if (numberTransform.HasFormat)
				{
					list.Add("Format");
					dictionary2.Add("FormatMask", numberTransform.Format.FormatMask);
				}
			}
			if (model.HasDateTimeTransform)
			{
				DateTimeTransformModel dateTimeTransform = model.DateTimeTransform;
				if (dateTimeTransform.HasRound)
				{
					list.Add("Round");
					list.Add(dateTimeTransform.Round.Mode.ToString());
					list.Add(dateTimeTransform.Round.Period.ToString());
					if (dateTimeTransform.Round.Ceiling == RoundDatePeriodCeiling.LastDay)
					{
						list.Add("LastDay");
					}
				}
				if (dateTimeTransform.HasFormat)
				{
					list.Add("Format");
					dictionary2.Add("FormatMask", dateTimeTransform.Format.FormatMask);
				}
				if (dateTimeTransform.HasPart)
				{
					list.Add("Part");
					list.Add(dateTimeTransform.Part.Kind.ToString());
				}
			}
			if (model is FromStrModel && !model.HasStringTransform && !model.HasNumberTransform && !model.HasDateTimeTransform)
			{
				list.Add("WholeColumnString");
			}
			if (!list.Any<string>())
			{
				return new EmptyExplanationMeta();
			}
			return new ExplanationMeta(this._templateProvider, list, dictionary2);
		}

		// Token: 0x040052BA RID: 21178
		private readonly ILogger _logger;

		// Token: 0x040052BB RID: 21179
		private readonly PipelineModel _pipelineModel;

		// Token: 0x040052BC RID: 21180
		private readonly IExplanationTemplateProvider _templateProvider;

		// Token: 0x020019C8 RID: 6600
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040052BD RID: 21181
			public static Func<string, bool> <0>__IsNullOrEmpty;
		}
	}
}
