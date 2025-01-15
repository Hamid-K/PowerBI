using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x02001984 RID: 6532
	public class PipelineFactory
	{
		// Token: 0x0600D5BC RID: 54716 RVA: 0x002D8739 File Offset: 0x002D6939
		private PipelineFactory(IEnumerable<ProgramNodeDetail> nodes, CancellationToken cancellation)
		{
			this._nodes = nodes;
			this._cancellation = cancellation;
		}

		// Token: 0x0600D5BD RID: 54717 RVA: 0x002D874F File Offset: 0x002D694F
		public static PipelineModel Compute(Program program, IEnumerable<ProgramNodeDetail> nodeDetails, CancellationToken cancellation)
		{
			return new PipelineFactory(nodeDetails, cancellation).ComputeInternal();
		}

		// Token: 0x0600D5BE RID: 54718 RVA: 0x002D875D File Offset: 0x002D695D
		public static PipelineModel Compute(Program program, CancellationToken cancellation)
		{
			return PipelineFactory.Compute(program, ProgramExtractVisitor.Extract(program, false), cancellation);
		}

		// Token: 0x0600D5BF RID: 54719 RVA: 0x002D8770 File Offset: 0x002D6970
		private PipelineModel ComputeInternal()
		{
			if (this._nodes.None<ProgramNodeDetail>())
			{
				return null;
			}
			ProgramNodeDetail programNodeDetail = this._nodes.First<ProgramNodeDetail>();
			this._cancellation.ThrowIfCancellationRequested();
			IfModel ifModel;
			if ((ifModel = this.ResolveIf(programNodeDetail)) == null && (ifModel = this.ResolveConcat(programNodeDetail)) == null)
			{
				ifModel = this.ResolveArithmetic(programNodeDetail) ?? this.ResolveSource(programNodeDetail);
			}
			return ifModel;
		}

		// Token: 0x0600D5C0 RID: 54720 RVA: 0x002D87D0 File Offset: 0x002D69D0
		public PipelineModel ResolveArithmetic(ProgramNodeDetail rootNodeDetail)
		{
			ProgramNodeDetail programNodeDetail;
			if (!rootNodeDetail.SelfAndDescendents.TryFirst((ProgramNodeDetail localNodeDetail) => localNodeDetail.Node.Is<Add, Subtract, Multiply, Divide>() || localNodeDetail.Node.Is<Sum, Product, Average>(), out programNodeDetail))
			{
				return null;
			}
			if (programNodeDetail.Is<Add, Subtract, Multiply, Divide>())
			{
				return new ArithmeticModel
				{
					Operator = programNodeDetail.Node.GrammarRule.Id,
					Left = this.ResolveSource(programNodeDetail.Children[0]),
					Right = (this.ResolveSource(programNodeDetail.Children[1]) ?? this.ResolveTerminal(programNodeDetail.Children[1])),
					NumberTransform = this.ResolveNumberTransform(null, programNodeDetail, true)
				};
			}
			if (programNodeDetail.Is<Sum, Product, Average>())
			{
				return new ArithmeticAggregateModel
				{
					Operator = programNodeDetail.Node.GrammarRule.Id,
					ColumnNames = programNodeDetail.Descendents.FirstOrDefaultValue<columnNames, string[]>(),
					NumberTransform = this.ResolveNumberTransform(null, programNodeDetail, true)
				};
			}
			return null;
		}

		// Token: 0x0600D5C1 RID: 54721 RVA: 0x002D88CC File Offset: 0x002D6ACC
		public ConcatModel ResolveConcat(ProgramNodeDetail rootNodeDetail)
		{
			ProgramNodeDetail programNodeDetail;
			if (!rootNodeDetail.SelfAndDescendents.TryFirst(out programNodeDetail))
			{
				return null;
			}
			IReadOnlyList<ProgramNodeDetail> readOnlyList = programNodeDetail.Yield<ProgramNodeDetail>().Concat(programNodeDetail.Descendents.OfType<Concat>()).ToReadOnlyList<ProgramNodeDetail>();
			IEnumerable<PipelineModel> enumerable = readOnlyList.Select((ProgramNodeDetail concatNode) => this.ResolveSource(concatNode.Children[0])).Concat(this.ResolveSource(readOnlyList.Last<ProgramNodeDetail>().Children[1]).Yield<PipelineModel>());
			return new ConcatModel
			{
				Children = enumerable.Collect<PipelineModel>().ToReadOnlyList<PipelineModel>()
			};
		}

		// Token: 0x0600D5C2 RID: 54722 RVA: 0x002D8950 File Offset: 0x002D6B50
		public IfModel ResolveIf(ProgramNodeDetail rootNodeDetail)
		{
			IReadOnlyList<ProgramNodeDetail> readOnlyList = rootNodeDetail.SelfAndDescendents.OfType<If>().ToReadOnlyList<ProgramNodeDetail>();
			if (readOnlyList.None<ProgramNodeDetail>())
			{
				return null;
			}
			ProgramNodeDetail programNodeDetail = readOnlyList.Last<ProgramNodeDetail>();
			List<PipelineModel> list = readOnlyList.Select((ProgramNodeDetail ifNode) => new IfTrueBranchModel
			{
				Condition = ifNode.Children[0].ToString().Replace("row, ", string.Empty),
				Body = (this.ResolveConcat(ifNode.Children[1]) ?? this.ResolveSource(ifNode.Children[1]))
			}).Cast<PipelineModel>().Concat(new IfFalseBranchModel
			{
				Body = (this.ResolveConcat(programNodeDetail.Children[2]) ?? this.ResolveSource(programNodeDetail.Children[2]))
			}.Yield<IfFalseBranchModel>())
				.ToList<PipelineModel>();
			return new IfModel
			{
				Children = list
			};
		}

		// Token: 0x0600D5C3 RID: 54723 RVA: 0x002D89E8 File Offset: 0x002D6BE8
		public PipelineModel ResolveTerminal(ProgramNodeDetail rootNodeDetail)
		{
			this._cancellation.ThrowIfCancellationRequested();
			ProgramNodeDetail programNodeDetail;
			if (!rootNodeDetail.SelfAndDescendents.TryFirst((ProgramNodeDetail localNodeDetail) => localNodeDetail.Node.Is<Find, Abs>() || localNodeDetail.Node is LiteralNode || localNodeDetail.Node.Is<Str, Null>() || localNodeDetail.Node.Is<Number>() || localNodeDetail.Node.IsArithmeticRightNumber() || localNodeDetail.Node.Is<Date>(), out programNodeDetail))
			{
				return null;
			}
			ProgramNode node = programNodeDetail.Node;
			Find find;
			if (programNodeDetail.Is(out find))
			{
				return new FindModel
				{
					Delimiter = find.findDelimiter.Value,
					Instance = find.findInstance.Value,
					Offset = find.findOffset.Value
				};
			}
			Abs abs;
			if (programNodeDetail.Is(out abs))
			{
				return new IntModel
				{
					Value = abs.absPos.Value
				};
			}
			LiteralNode literalNode = node as LiteralNode;
			if (literalNode != null && literalNode.Value is int)
			{
				return new IntModel
				{
					Value = (int)literalNode.Value
				};
			}
			Str str;
			if (programNodeDetail.Is(out str))
			{
				return new StrModel
				{
					Value = str.constStr.Value
				};
			}
			Number number;
			if (programNodeDetail.Is(out number))
			{
				return new NumberModel
				{
					Value = number.constNum.Value,
					NumberTransform = this.ResolveNumberTransform(null, programNodeDetail, true)
				};
			}
			decimal num;
			if (programNodeDetail.Node.IsArithmeticRightNumber(out num))
			{
				return new NumberModel
				{
					Value = num,
					NumberTransform = this.ResolveNumberTransform(null, programNodeDetail, true)
				};
			}
			Date date;
			if (programNodeDetail.Is(out date))
			{
				return new DateTimeModel
				{
					Value = date.constDt.Value,
					DateTimeTransform = this.ResolveDateTimeTransform(null, programNodeDetail)
				};
			}
			if (programNodeDetail.Is<Null>())
			{
				return new NullModel();
			}
			return null;
		}

		// Token: 0x0600D5C4 RID: 54724 RVA: 0x002D8BB0 File Offset: 0x002D6DB0
		private PipelineModel ResolveSource(ProgramNodeDetail rootNodeDetail)
		{
			this._cancellation.ThrowIfCancellationRequested();
			ProgramNodeDetail programNodeDetail;
			if (!rootNodeDetail.SelfAndDescendents.TryFirst((ProgramNodeDetail localNodeDetail) => localNodeDetail.Node.Is<FromStr, FromNumberStr>() || localNodeDetail.Node.Is<FromNumber, FromNumberCoalesced>() || localNodeDetail.Node.Is<FromRowNumber>() || localNodeDetail.Node.Is<FromDateTime>() || localNodeDetail.Node.Is<FromTime>(), out programNodeDetail))
			{
				return this.ResolveTerminal(rootNodeDetail);
			}
			ProgramNodeDetail programNodeDetail2 = programNodeDetail;
			ProgramNodeDetail programNodeDetail3;
			if (programNodeDetail.Ancestors.TryLast(out programNodeDetail3) && !programNodeDetail3.Descendents.TryFirst(out programNodeDetail2))
			{
				return null;
			}
			string text = programNodeDetail.Descendents.FirstOrDefaultValue<columnName, string>();
			if (programNodeDetail.Is<FromStr, FromNumberStr>())
			{
				return new FromStrModel
				{
					ColumnName = text,
					StringTransform = this.ResolveStringTransform(text, programNodeDetail2),
					NumberTransform = this.ResolveNumberTransform(text, programNodeDetail2, true),
					DateTimeTransform = this.ResolveDateTimeTransform(text, programNodeDetail2)
				};
			}
			if (programNodeDetail.Is<FromNumber, FromNumberCoalesced>())
			{
				return new FromNumberModel
				{
					ColumnName = text,
					NumberTransform = this.ResolveNumberTransform(text, programNodeDetail2, true)
				};
			}
			if (programNodeDetail.Is<FromRowNumber>())
			{
				return new FromRowNumberModel
				{
					ColumnName = text,
					NumberTransform = this.ResolveNumberTransform(text, programNodeDetail2, true)
				};
			}
			if (programNodeDetail.Is<FromDateTime>())
			{
				return new FromDateTimeModel
				{
					ColumnName = text,
					DateTimeTransform = this.ResolveDateTimeTransform(text, programNodeDetail2)
				};
			}
			if (programNodeDetail.Is<FromTime>())
			{
				return new FromTimeModel
				{
					ColumnName = text,
					DateTimeTransform = this.ResolveDateTimeTransform(text, programNodeDetail2)
				};
			}
			return null;
		}

		// Token: 0x0600D5C5 RID: 54725 RVA: 0x002D8D00 File Offset: 0x002D6F00
		public DateTimeTransformModel ResolveDateTimeTransform(string columnName, ProgramNodeDetail nodeDetail)
		{
			this._cancellation.ThrowIfCancellationRequested();
			if (nodeDetail.Parent == null)
			{
				return null;
			}
			IReadOnlyList<ProgramNodeDetail> ancestors = nodeDetail.Ancestors;
			FormatDateTimeTransformModel formatDateTimeTransformModel = null;
			FormatDateTime formatDateTime;
			if (ancestors.TryLast(out formatDateTime))
			{
				formatDateTimeTransformModel = new FormatDateTimeTransformModel
				{
					ColumnName = columnName,
					FormatMask = formatDateTime.dateTimeFormatDesc.Value.Mask,
					Locale = formatDateTime.dateTimeFormatDesc.Value.Locale
				};
			}
			RoundDateTimeTransformModel roundDateTimeTransformModel = null;
			RoundDateTime roundDateTime;
			if (ancestors.TryLast(out roundDateTime))
			{
				roundDateTimeTransformModel = new RoundDateTimeTransformModel
				{
					ColumnName = columnName,
					Mode = roundDateTime.dateTimeRoundDesc.Value.Mode,
					Period = roundDateTime.dateTimeRoundDesc.Value.Period,
					Ceiling = roundDateTime.dateTimeRoundDesc.Value.Ceiling
				};
			}
			DateTimePartTransformModel dateTimePartTransformModel = null;
			DateTimePart dateTimePart;
			if (ancestors.TryLast(out dateTimePart))
			{
				dateTimePartTransformModel = new DateTimePartTransformModel
				{
					ColumnName = columnName,
					Kind = dateTimePart.dateTimePartKind.Value
				};
			}
			if (formatDateTimeTransformModel == null && roundDateTimeTransformModel == null && dateTimePartTransformModel == null)
			{
				return null;
			}
			return new DateTimeTransformModel
			{
				ColumnName = columnName,
				Format = formatDateTimeTransformModel,
				Round = roundDateTimeTransformModel,
				Part = dateTimePartTransformModel
			};
		}

		// Token: 0x0600D5C6 RID: 54726 RVA: 0x002D8E4C File Offset: 0x002D704C
		public NumberTransformModel ResolveNumberTransform(string columnName, ProgramNodeDetail nodeDetail, bool inspectAncestors = true)
		{
			this._cancellation.ThrowIfCancellationRequested();
			if (nodeDetail.Parent == null)
			{
				return null;
			}
			IReadOnlyList<ProgramNodeDetail> readOnlyList = nodeDetail.Ancestors.TakeWhile((ProgramNodeDetail a) => !a.Node.IsArithmetic()).ToReadOnlyList<ProgramNodeDetail>();
			FormatNumberTransformModel formatNumberTransformModel = null;
			FormatNumber formatNumber;
			if (readOnlyList.TryLast(out formatNumber))
			{
				formatNumberTransformModel = new FormatNumberTransformModel
				{
					ColumnName = columnName,
					FormatMask = formatNumber.numberFormatDesc.Value.ToFormatString(),
					SimplifiedFormatMask = formatNumber.numberFormatDesc.Value.ToSimplifiedFormatString(),
					Locale = formatNumber.numberFormatDesc.Value.Locale
				};
			}
			RoundNumberTransform roundNumberTransform = null;
			RoundNumber roundNumber;
			if (readOnlyList.TryLast(out roundNumber))
			{
				roundNumberTransform = new RoundNumberTransform
				{
					ColumnName = columnName,
					Mode = roundNumber.numberRoundDesc.Value.Mode,
					Delta = roundNumber.numberRoundDesc.Value.Delta
				};
			}
			ForwardFillLinearTransform forwardFillLinearTransform = null;
			RowNumberLinearTransform rowNumberLinearTransform;
			if (readOnlyList.TryLast(out rowNumberLinearTransform))
			{
				forwardFillLinearTransform = new ForwardFillLinearTransform
				{
					ColumnName = columnName,
					Intercept = rowNumberLinearTransform.rowNumberLinearTransformDesc.Value.Intercept,
					Gradient = rowNumberLinearTransform.rowNumberLinearTransformDesc.Value.Gradient
				};
			}
			if (formatNumberTransformModel == null && roundNumberTransform == null && forwardFillLinearTransform == null)
			{
				return null;
			}
			return new NumberTransformModel
			{
				ColumnName = columnName,
				Format = formatNumberTransformModel,
				Round = roundNumberTransform,
				ForwardFillLinear = forwardFillLinearTransform
			};
		}

		// Token: 0x0600D5C7 RID: 54727 RVA: 0x002D8FE0 File Offset: 0x002D71E0
		public StringTransformModel ResolveStringTransform(string columnName, ProgramNodeDetail nodeDetail)
		{
			this._cancellation.ThrowIfCancellationRequested();
			SplitTransformModel splitTransformModel = null;
			SliceTransformModel sliceTransformModel = null;
			SliceBetweenTransformModel sliceBetweenTransformModel = null;
			IReadOnlyList<ProgramNodeDetail> ancestors = nodeDetail.Ancestors;
			Split split;
			if (ancestors.TryLast(out split))
			{
				splitTransformModel = new SplitTransformModel
				{
					ColumnName = columnName,
					Delimiter = split.splitDelimiter.Value,
					Instance = split.splitInstance.Value
				};
			}
			ProgramNodeDetail programNodeDetail;
			if (ancestors.TryLast(out programNodeDetail))
			{
				sliceTransformModel = new SliceTransformModel
				{
					ColumnName = columnName,
					StartPosition = this.ResolveTerminal(programNodeDetail.Children[1]),
					EndPosition = this.ResolveTerminal(programNodeDetail.Children[2])
				};
			}
			ProgramNodeDetail programNodeDetail2;
			if (ancestors.TryLast(out programNodeDetail2))
			{
				sliceTransformModel = new SliceTransformModel
				{
					ColumnName = columnName,
					EndPosition = this.ResolveTerminal(programNodeDetail2.Children[1])
				};
			}
			ProgramNodeDetail programNodeDetail3;
			if (ancestors.TryLast(out programNodeDetail3))
			{
				sliceTransformModel = new SliceTransformModel
				{
					ColumnName = columnName,
					EndPosition = this.ResolveTerminal(programNodeDetail3.Children[1])
				};
			}
			ProgramNodeDetail programNodeDetail4;
			if (ancestors.TryLast(out programNodeDetail4))
			{
				sliceTransformModel = new SliceTransformModel
				{
					ColumnName = columnName,
					StartPosition = this.ResolveTerminal(programNodeDetail4.Children[1])
				};
			}
			ProgramNodeDetail programNodeDetail5;
			if (ancestors.TryLast(out programNodeDetail5))
			{
				sliceBetweenTransformModel = new SliceBetweenTransformModel
				{
					ColumnName = columnName,
					StartText = programNodeDetail5.Children[1].Cast<sliceBetweenStartText>().Value,
					EndText = programNodeDetail5.Children[2].Cast<sliceBetweenEndText>().Value
				};
			}
			ParseNumberTransformModel parseNumberTransformModel = null;
			ParseDateTimeTransformModel parseDateTimeTransformModel = null;
			ReplaceTransformModel replaceTransformModel = null;
			ParseNumber parseNumber;
			if (ancestors.TryLast(out parseNumber))
			{
				parseNumberTransformModel = new ParseNumberTransformModel
				{
					ColumnName = columnName,
					Locale = parseNumber.locale.Value
				};
			}
			ParseDateTime parseDateTime;
			if (ancestors.TryLast(out parseDateTime))
			{
				parseDateTimeTransformModel = new ParseDateTimeTransformModel
				{
					ColumnName = columnName,
					Locale = parseDateTime.dateTimeParseDesc.Value.Locale,
					FormatMask = parseDateTime.dateTimeParseDesc.Value.Mask
				};
			}
			Replace replace;
			if (ancestors.TryLast(out replace))
			{
				replaceTransformModel = new ReplaceTransformModel
				{
					ColumnName = columnName,
					FindText = replace.replaceFindText.Value,
					ReplaceText = replace.replaceText.Value
				};
			}
			bool flag;
			if (!ancestors.Any((ProgramNodeDetail n) => n.Node.IsTrim()))
			{
				flag = ancestors.Any((ProgramNodeDetail n) => n.Node.IsTrim());
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			bool flag3 = ancestors.Any((ProgramNodeDetail n) => n.Node.Is<Length>());
			CaseTransformModel caseTransformModel = null;
			bool flag4 = ancestors.Any((ProgramNodeDetail n) => n.Node.IsUpperCase());
			bool flag5 = ancestors.Any((ProgramNodeDetail n) => n.Node.IsLowerCase());
			bool flag6 = ancestors.Any((ProgramNodeDetail n) => n.Node.IsProperCase());
			if (flag4 || flag5 || flag6)
			{
				caseTransformModel = new CaseTransformModel
				{
					ColumnName = columnName,
					UpperCase = flag4,
					LowerCase = flag5,
					ProperCase = flag6
				};
			}
			if (splitTransformModel == null && sliceTransformModel == null && sliceBetweenTransformModel == null && caseTransformModel == null && parseNumberTransformModel == null && parseDateTimeTransformModel == null && replaceTransformModel == null && !flag2 && !flag3)
			{
				return null;
			}
			return new StringTransformModel
			{
				ColumnName = columnName,
				Case = caseTransformModel,
				Split = splitTransformModel,
				Slice = sliceTransformModel,
				SliceBetween = sliceBetweenTransformModel,
				ParseNumber = parseNumberTransformModel,
				ParseDateTime = parseDateTimeTransformModel,
				Replace = replaceTransformModel,
				Trim = flag2,
				Length = flag3
			};
		}

		// Token: 0x040051E8 RID: 20968
		private readonly CancellationToken _cancellation;

		// Token: 0x040051E9 RID: 20969
		private readonly IEnumerable<ProgramNodeDetail> _nodes;
	}
}
