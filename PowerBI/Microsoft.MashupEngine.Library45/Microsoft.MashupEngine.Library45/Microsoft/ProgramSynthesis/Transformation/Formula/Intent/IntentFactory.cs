using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Intent
{
	// Token: 0x020019B1 RID: 6577
	internal class IntentFactory
	{
		// Token: 0x0600D6C5 RID: 54981 RVA: 0x002DA509 File Offset: 0x002D8709
		private IntentFactory(IEnumerable<ProgramNodeDetail> nodeDetails, CancellationToken cancellation, ILogger logger)
		{
			this._nodes = nodeDetails.ToReadOnlyList<ProgramNodeDetail>();
			this._cancellation = cancellation;
			this._logger = logger;
		}

		// Token: 0x0600D6C6 RID: 54982 RVA: 0x002DA52B File Offset: 0x002D872B
		public static ProgramIntentSummary Compute(Program program, CancellationToken cancellation, ILogger logger = null)
		{
			return new IntentFactory(program.Meta.Nodes, cancellation, logger).ComputeIntenal();
		}

		// Token: 0x0600D6C7 RID: 54983 RVA: 0x002DA544 File Offset: 0x002D8744
		public static ProgramIntentSummary Compute(IEnumerable<ProgramNodeDetail> nodeDetail, CancellationToken cancellation, ILogger logger = null)
		{
			return new IntentFactory(nodeDetail, cancellation, logger).ComputeIntenal();
		}

		// Token: 0x0600D6C8 RID: 54984 RVA: 0x002DA554 File Offset: 0x002D8754
		private ProgramIntentSummary ComputeIntenal()
		{
			IReadOnlyList<ProgramNodeDetail> nodes = this._nodes;
			bool? flag = ((nodes != null) ? new bool?(nodes.None<ProgramNodeDetail>()) : null);
			bool flag2 = flag == null || flag.GetValueOrDefault();
			if (flag2)
			{
				return new ProgramIntentSummary();
			}
			ProgramIntent programIntent = ProgramIntent.Unknown;
			ProgramIntent programIntent2 = ProgramIntent.Unknown;
			try
			{
				IReadOnlyList<ProgramNodeDetail> readOnlyList = this._nodes.Where((ProgramNodeDetail nd) => nd.Node.IsFrom()).ToReadOnlyList<ProgramNodeDetail>();
				if (readOnlyList.Any((ProgramNodeDetail nd) => nd.Node.IsFromStr()))
				{
					programIntent |= ProgramIntent.FromStr;
				}
				if (readOnlyList.Any((ProgramNodeDetail nd) => nd.Node.IsFromNumber()))
				{
					programIntent |= ProgramIntent.FromNumber;
				}
				if (this._nodes.Any<FromNumbers>())
				{
					programIntent |= ProgramIntent.FromNumbers;
				}
				if (this._nodes.Any<FromDateTime>())
				{
					programIntent |= ProgramIntent.FromDateTime;
				}
				if (this._nodes.Any<FromRowNumber>())
				{
					programIntent |= ProgramIntent.FromRowNumber;
				}
				if (this._nodes.Any<Str>())
				{
					programIntent |= ProgramIntent.ConstantString;
				}
				if (this._nodes.Any((ProgramNodeDetail nd) => nd.Node.IsConstantNumber()))
				{
					programIntent |= ProgramIntent.ConstantNumber;
				}
				if (this._nodes.Any<FormatNumber>())
				{
					programIntent |= ProgramIntent.NumberFormat;
				}
				if (this._nodes.Any<RoundNumber>())
				{
					programIntent |= ProgramIntent.NumberRound;
				}
				if (this._nodes.Any<RowNumberLinearTransform>())
				{
					programIntent |= ProgramIntent.ForwardFillLinear;
				}
				if (this._nodes.Any<FormatDateTime>())
				{
					programIntent |= ProgramIntent.DateTimeFormat;
				}
				if (this._nodes.Any<RoundDateTime>())
				{
					programIntent |= ProgramIntent.DateTimeRound;
				}
				if (this._nodes.Any<DateTimePart>())
				{
					programIntent |= ProgramIntent.DateTimePart;
				}
				if (this._nodes.Any<TimePart>())
				{
					programIntent |= ProgramIntent.TimePart;
				}
				if (this._nodes.Any((ProgramNodeDetail nd) => nd.Node.IsCase()))
				{
					programIntent |= ProgramIntent.Case;
				}
				if (this._nodes.Any((ProgramNodeDetail nd) => nd.Node.IsTrim()))
				{
					programIntent |= ProgramIntent.Trim;
				}
				if (this._nodes.Any<ParseNumber>())
				{
					programIntent |= ProgramIntent.ParseNumber;
				}
				if (this._nodes.Any<ParseDateTime>())
				{
					programIntent |= ProgramIntent.ParseDateTime;
				}
				if (this._nodes.Any<Replace>())
				{
					programIntent |= ProgramIntent.Replace;
				}
				if (this._nodes.Any((ProgramNodeDetail nd) => nd.Node.IsSubstring()))
				{
					programIntent |= (ProgramIntent)((ulong)int.MinValue);
				}
				if (this._nodes.Any<Length>())
				{
					programIntent |= ProgramIntent.Length;
				}
				if (this._nodes.Any((ProgramNodeDetail nd) => nd.Node.IsArithmetic()))
				{
					programIntent |= ProgramIntent.Arithmetic;
				}
				if (this._nodes.Any((ProgramNodeDetail nd) => nd.Node.IsArithmeticAggregate()))
				{
					programIntent |= ProgramIntent.ArithmeticAggregate;
				}
				if (this._nodes.Any<Concat>())
				{
					programIntent |= ProgramIntent.Concat;
				}
				if (this._nodes.Any<If>())
				{
					programIntent |= ProgramIntent.If;
				}
				if (readOnlyList.Any((ProgramNodeDetail nd) => nd.Ancestors.All((ProgramNodeDetail ancestor) => ancestor.Node.IsOutputCast())))
				{
					programIntent |= ProgramIntent.WholeColumn;
				}
				int num = this._nodes.SelectValues<columnName, string>().Concat(this._nodes.SelectValues<columnNames, string[]>().SelectMany((string[] c) => c)).Distinct<string>()
					.Count<string>();
				if (num == 1)
				{
					programIntent |= ProgramIntent.SingleColumn;
				}
				if (num > 1)
				{
					programIntent |= ProgramIntent.MultiColumn;
				}
				if (this._nodes.Count<LetX>() > 1)
				{
					programIntent |= ProgramIntent.MultiSubstring;
				}
				if (this._nodes.Any<ToStr>())
				{
					programIntent |= ProgramIntent.ToStr;
				}
				if (this._nodes.Any<ToInt>())
				{
					programIntent |= ProgramIntent.ToInt;
				}
				if (this._nodes.Any<ToDouble>())
				{
					programIntent |= ProgramIntent.ToDouble;
				}
				if (this._nodes.Any<ToDecimal>())
				{
					programIntent |= ProgramIntent.ToDecimal;
				}
				if (this._nodes.Any<ToDateTime>())
				{
					programIntent |= ProgramIntent.ToDateTime;
				}
				this._cancellation.ThrowIfCancellationRequested();
				ProgramIntent allowedIntentRoots = (programIntent.HasFlag(ProgramIntent.If) ? ProgramIntent.If : (programIntent.HasFlag(ProgramIntent.Concat) ? ProgramIntent.Concat : (programIntent.HasFlag(ProgramIntent.Arithmetic) ? ProgramIntent.Arithmetic : (programIntent.HasFlag(ProgramIntent.ArithmeticAggregate) ? ProgramIntent.ArithmeticAggregate : ProgramIntent.CompositeRoots))));
				ProgramIntent programIntent3 = (from i in typeof(ProgramIntent).Values<ProgramIntent>()
					where i != ProgramIntent.CompositeRoots
					where i.AnyFlags(allowedIntentRoots)
					select i).Aggregate(ProgramIntent.Unknown, (ProgramIntent current, ProgramIntent next) => current | next);
				programIntent2 = programIntent & programIntent3;
				this._cancellation.ThrowIfCancellationRequested();
			}
			catch (Exception ex) when (ex is OperationCanceledException)
			{
				ILogger logger = this._logger;
				if (logger != null)
				{
					logger.TrackException(ex);
				}
				throw;
			}
			catch (Exception ex2)
			{
				ILogger logger2 = this._logger;
				if (logger2 != null)
				{
					logger2.TrackException(ex2);
				}
			}
			return new ProgramIntentSummary
			{
				Intent = programIntent2,
				IntentFlags = programIntent
			};
		}

		// Token: 0x0400523D RID: 21053
		private readonly CancellationToken _cancellation;

		// Token: 0x0400523E RID: 21054
		private readonly ILogger _logger;

		// Token: 0x0400523F RID: 21055
		private readonly IReadOnlyList<ProgramNodeDetail> _nodes;
	}
}
