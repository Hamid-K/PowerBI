using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Intent;
using Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014EB RID: 5355
	public class ProgramMetaFactory
	{
		// Token: 0x0600A409 RID: 41993 RVA: 0x0022B542 File Offset: 0x00229742
		private ProgramMetaFactory(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, LearnConfidenceResult learnConfidence, ILogger logger, CancellationToken cancellation)
		{
			this._program = program;
			this._learnConfidence = learnConfidence;
			this._examples = examples.ToReadOnlyList<Example>();
			this._inputs = inputs.ToReadOnlyList<IRow>();
			this._cancellation = cancellation;
			this._logger = logger;
		}

		// Token: 0x0600A40A RID: 41994 RVA: 0x0022B581 File Offset: 0x00229781
		public static ProgramMeta Compute(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, LearnConfidenceResult learnConfidence, ILogger logger, CancellationToken cancellation)
		{
			return new ProgramMetaFactory(program, examples, inputs, learnConfidence, logger, cancellation).ComputeInternal();
		}

		// Token: 0x0600A40B RID: 41995 RVA: 0x0022B598 File Offset: 0x00229798
		private ProgramMeta ComputeInternal()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			bool? flag = null;
			bool? flag2 = null;
			bool? flag3 = null;
			bool? flag4 = null;
			double? num = null;
			double? num2 = null;
			int? num3 = null;
			double? num4 = null;
			int? num5 = null;
			int? num6 = null;
			int? num7 = null;
			int? num8 = null;
			int? num9 = null;
			PipelineModel pipelineModel = null;
			ProgramIntentSummary programIntentSummary = null;
			IEnumerable<ExampleSubstringInfo> enumerable = null;
			try
			{
				if (this._additionalExamples == null)
				{
					this._additionalExamples = this.ResolveAdditionalExamples();
				}
				IEnumerable<Example> examples = this._examples;
				IEnumerable<Example> additionalExamples = this._additionalExamples;
				this._allExamples = examples.Concat(additionalExamples ?? Enumerable.Empty<Example>()).ToReadOnlyList<Example>();
				this._nodes = ProgramExtractVisitor.Extract(this._program, false).ToReadOnlyList<ProgramNodeDetail>();
				this._columnsUsed = this._nodes.SelectValues<columnName, string>().Concat(this._nodes.SelectValues<columnNames, string[]>().SelectMany((string[] v) => v)).Distinct<string>()
					.ToReadOnlyList<string>();
				this._cancellation.ThrowIfCancellationRequested();
				num6 = new int?(this._nodes.Count<Str>());
				num5 = new int?(this._nodes.Count<Concat>());
				num7 = new int?(this._nodes.Count((ProgramNodeDetail n) => n.Node.IsMatch()));
				num8 = this.ResolveDistinctExampleCount();
				num9 = this.ResolveDistinctExampleOutputCount();
				flag = new bool?(this.HasConstantOutput());
				flag2 = new bool?(this.HasWholeColumnOutput());
				flag3 = new bool?(this.HasConsistentOutput());
				flag4 = new bool?(this.HasConsistentInput());
				num3 = new int?(this.ResolveConstantStringLength());
				num = this.ResolveOutputLengthStdDev();
				num2 = this.ResolveConstantStringRatio(num3.Value);
				num4 = this.ResolveConstantStringRatioAvg(num3.Value);
				this._cancellation.ThrowIfCancellationRequested();
				try
				{
					pipelineModel = PipelineFactory.Compute(this._program, this._nodes, this._cancellation);
				}
				catch (Exception ex)
				{
					ILogger logger = this._logger;
					if (logger != null)
					{
						logger.TrackException(ex);
					}
				}
				this._cancellation.ThrowIfCancellationRequested();
				try
				{
					programIntentSummary = IntentFactory.Compute(this._nodes, this._cancellation, this._logger);
				}
				catch (Exception ex2)
				{
					ILogger logger2 = this._logger;
					if (logger2 != null)
					{
						logger2.TrackException(ex2);
					}
				}
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
			return new ProgramMeta
			{
				Nodes = this._nodes,
				MetadataTime = new double?(stopwatch.ElapsedMillisecondsAsDouble()),
				ColumnsUsed = this._columnsUsed,
				ExampleCount = new int?(this._examples.Count),
				DistinctExampleCount = num8,
				DistinctOutputCount = num9,
				InputCount = new int?(this._inputs.Count),
				ConstantOutput = flag,
				WholeColumnOutput = flag2,
				ConsistentOutput = flag3,
				ConsistentInput = flag4,
				OutputLengthStdDev = num,
				OutputConstStrLength = num3,
				OutputConstStrRatio = num2,
				ConstStrLength = num3,
				OutputConstStrRatioAvg = num4,
				ConcatCount = num5,
				StrCount = num6,
				MatchCount = num7,
				LearnConfidence = this._learnConfidence,
				Pipeline = pipelineModel,
				IntentSummary = programIntentSummary,
				ExampleSubstrings = enumerable
			};
		}

		// Token: 0x0600A40C RID: 41996 RVA: 0x0022B9CC File Offset: 0x00229BCC
		private bool HasConsistentInput()
		{
			return this._inputs == null || !this._inputs.Any<IRow>() || this._inputs.All((IRow row) => this._columnsUsed.All(delegate(string columnName)
			{
				object obj;
				return row.TryGetValue(columnName, out obj) && obj != null;
			}));
		}

		// Token: 0x0600A40D RID: 41997 RVA: 0x0022B9FC File Offset: 0x00229BFC
		private bool HasConsistentOutput()
		{
			if (!ProgramExtractVisitor.ExtractNodes(this._program, (ProgramNode node) => node.Is<Null>(), false).Any<ProgramNode>())
			{
				return this._allExamples.All((Example e) => e.Output != null);
			}
			return true;
		}

		// Token: 0x0600A40E RID: 41998 RVA: 0x0022BA67 File Offset: 0x00229C67
		private bool HasConstantOutput()
		{
			return this._nodes.All(delegate(ProgramNodeDetail node)
			{
				GrammarRule grammarRule = node.Node.GrammarRule;
				return ((grammarRule != null) ? grammarRule.Id : null) != node.Node.Grammar.InputSymbol.Name;
			});
		}

		// Token: 0x0600A40F RID: 41999 RVA: 0x0022BA93 File Offset: 0x00229C93
		private bool HasWholeColumnOutput()
		{
			if (this._nodes.Any<If>())
			{
				return false;
			}
			return this._nodes.All((ProgramNodeDetail node) => node.Node is TerminalNode || node.Node.GrammarRule is ConversionRule || node.Node.IsOutputCast() || node.Node.IsFrom());
		}

		// Token: 0x0600A410 RID: 42000 RVA: 0x0022BACE File Offset: 0x00229CCE
		private IReadOnlyList<Example> ResolveAdditionalExamples()
		{
			if (this._inputs == null || !this._inputs.Any<IRow>())
			{
				return Enumerable.Empty<Example>().ToList<Example>();
			}
			return this._inputs.Select((IRow row) => new Example(row, this._program.Run(row), false)).ToList<Example>();
		}

		// Token: 0x0600A411 RID: 42001 RVA: 0x0022BB0C File Offset: 0x00229D0C
		private int ResolveConstantStringLength()
		{
			if (this._nodes == null || this._nodes.Count == 0)
			{
				return 0;
			}
			return this._nodes.Sum(delegate(ProgramNodeDetail node)
			{
				constStr constStr;
				if (!node.Is(out constStr))
				{
					return 0;
				}
				return constStr.Value.Length;
			});
		}

		// Token: 0x0600A412 RID: 42002 RVA: 0x0022BB5C File Offset: 0x00229D5C
		private double? ResolveConstantStringRatio(int constStrLength)
		{
			try
			{
				List<string> list = (from e in this._examples
					where e.Output is string
					select e.Output as string).ToList<string>();
				double num = (double)list.Sum(delegate(string o)
				{
					if (o == null)
					{
						return 0;
					}
					return o.Length;
				});
				double? num2;
				if (num != 0.0)
				{
					num2 = new double?((double)(list.Count * constStrLength) / num);
				}
				else
				{
					num2 = null;
				}
				return num2;
			}
			catch (Exception ex)
			{
				ILogger logger = this._logger;
				if (logger != null)
				{
					logger.TrackException(ex);
				}
			}
			return null;
		}

		// Token: 0x0600A413 RID: 42003 RVA: 0x0022BC40 File Offset: 0x00229E40
		private double? ResolveConstantStringRatioAvg(int constStrLength)
		{
			try
			{
				List<string> list = (from e in this._allExamples
					where e.Output is string
					select e.Output as string).ToList<string>();
				double num = (double)list.Sum(delegate(string o)
				{
					if (o == null)
					{
						return 0;
					}
					return o.Length;
				});
				double? num2;
				if (num != 0.0)
				{
					num2 = new double?((double)(list.Count * constStrLength) / num);
				}
				else
				{
					num2 = null;
				}
				return num2;
			}
			catch (Exception ex)
			{
				ILogger logger = this._logger;
				if (logger != null)
				{
					logger.TrackException(ex);
				}
			}
			return null;
		}

		// Token: 0x0600A414 RID: 42004 RVA: 0x0022BD24 File Offset: 0x00229F24
		private int? ResolveDistinctExampleCount()
		{
			int? num;
			try
			{
				if (!this._columnsUsed.Any<string>())
				{
					num = null;
					num = num;
				}
				else
				{
					num = new int?((from example in this._examples
						let input = new InputRow(this._columnsUsed.Select((string col) => KVP.Create<string, object>(col, example.Input.Get(col))))
						select new Example(input, example.Output, false)).Distinct<Example>().Count<Example>());
				}
			}
			catch (Exception ex)
			{
				this._logger.TrackException(ex);
				num = null;
			}
			return num;
		}

		// Token: 0x0600A415 RID: 42005 RVA: 0x0022BDC4 File Offset: 0x00229FC4
		private int? ResolveDistinctExampleOutputCount()
		{
			int? num;
			try
			{
				num = new int?(this._examples.Select((Example e) => e.Output).Distinct<object>().Count<object>());
			}
			catch (Exception ex)
			{
				this._logger.TrackException(ex);
				num = null;
			}
			return num;
		}

		// Token: 0x0600A416 RID: 42006 RVA: 0x0022BE38 File Offset: 0x0022A038
		private double? ResolveOutputLengthStdDev()
		{
			double? num = null;
			try
			{
				List<int> list = (from example in this._allExamples
					let output = ((example != null) ? example.Output : null) as string
					where output != null
					select output.Length).ToList<int>();
				num = new double?(list.Any<int>() ? list.StandardDeviation(null) : 0.0);
			}
			catch (Exception ex)
			{
				ILogger logger = this._logger;
				if (logger != null)
				{
					logger.TrackException(ex);
				}
			}
			return num;
		}

		// Token: 0x0600A417 RID: 42007 RVA: 0x0022BF18 File Offset: 0x0022A118
		private IEnumerable<ExampleSubstringInfo> ResolveExampleSubstrings()
		{
			List<ProgramNode> list = (from item in this._nodes
				let node = item.Node
				let isLetChild = item.Ancestors.Any<LetX>()
				let isTransformChild = item.Ancestors.Any((ProgramNodeDetail a) => ProgramMetaFactory.<ResolveExampleSubstrings>g__IsTransformRule|24_0(a.Node))
				let isSubstring = !isTransformChild && node.Is<LetX>()
				let isWholeColumn = !isTransformChild && !isLetChild && node.Is<FromStr>()
				let isTransform = ProgramMetaFactory.<ResolveExampleSubstrings>g__IsTransformRule|24_0(node)
				let isConstStr = node.Is<Str>()
				where isSubstring || isWholeColumn || isConstStr || isTransform
				orderby item.Order
				select item.Node).ToList<ProgramNode>();
			if (!list.Any<ProgramNode>())
			{
				return Enumerable.Empty<ExampleSubstringInfo>();
			}
			List<ExampleSubstringInfo> list2 = new List<ExampleSubstringInfo>();
			int num = 1;
			foreach (Example example in this._allExamples)
			{
				List<ExampleSubstring> list3 = new List<ExampleSubstring>();
				int num2 = 1;
				int num3 = 1;
				int num4 = 1;
				foreach (ProgramNode programNode in list)
				{
					string text = (programNode.Is<Str>() ? string.Format("c{0}", num3++) : string.Format("{0}", num2++));
					ExampleToken exampleToken = ProgramMetaFactory.ResolveOutputToken(example, programNode, new int?(num4), text);
					if (exampleToken != null)
					{
						int? end = exampleToken.End;
						int num5 = num4;
						if (((end.GetValueOrDefault() > num5) & (end != null)) && exampleToken.End != null)
						{
							num4 = exampleToken.End.Value;
						}
					}
					list3.Add(new ExampleSubstring
					{
						Input = this.ResolveInputToken(example, programNode, text),
						Output = exampleToken
					});
				}
				if (list3.Any<ExampleSubstring>())
				{
					list2.Add(new ExampleSubstringInfo
					{
						ExampleId = num++,
						Example = example,
						Tokens = list3
					});
				}
			}
			return list2;
		}

		// Token: 0x0600A418 RID: 42008 RVA: 0x0022C244 File Offset: 0x0022A444
		private ExampleInputToken ResolveInputToken(Example example, ProgramNode stringNode, string tokenId)
		{
			int? num = null;
			int? num2 = null;
			string text = null;
			ProgramNode programNode = ProgramExtractVisitor.ExtractNodes(stringNode, (ProgramNode node) => node.Is<Split>() || node.Is<Slice>() || node.Is<SlicePrefix>() || node.Is<SliceSuffix>(), false).FirstOrDefault<ProgramNode>();
			if (programNode == null)
			{
				programNode = ProgramExtractVisitor.ExtractNodes(stringNode, (ProgramNode node) => node.Is<FromStr>(), false).FirstOrDefault<ProgramNode>();
			}
			State state = State.CreateForExecution(Language.Grammar.InputSymbol, example.Input);
			if (!(stringNode.Invoke(state) is string))
			{
				return null;
			}
			if (programNode == null)
			{
				return null;
			}
			string text2 = (from node in this._nodes.OfGrammarType<FromStr>()
				select node.columnName.Value).FirstOrDefault<string>();
			object obj;
			if (example.Input.TryGetValue(text2, out obj))
			{
				text = obj as string;
			}
			if (text == null)
			{
				return null;
			}
			State state2 = state.Bind(Language.Build.Symbol.x, text);
			Split split;
			if (programNode.Is(out split))
			{
				string value = split.splitDelimiter.Value;
				int num3 = split.splitInstance.Value;
				List<int?> list = (from i in text.AllIndexesOf(value, StringComparison.Ordinal)
					select i + 1).Cast<int?>().ToList<int?>();
				if (num3 < 0)
				{
					num3 = list.Count + 2 + num3;
				}
				num = list.ElementAtOrDefault(num3 - 2) + 1;
				num2 = list.ElementAtOrDefault(num3 - 1);
			}
			SlicePrefix slicePrefix;
			if (programNode.Is(out slicePrefix))
			{
				num2 = slicePrefix.pos.Node.Invoke(state2) as int?;
			}
			SliceSuffix sliceSuffix;
			if (programNode.Is(out sliceSuffix))
			{
				num = sliceSuffix.pos.Node.Invoke(state2) as int?;
			}
			Slice slice;
			if (programNode.Is(out slice))
			{
				num = slice.pos1.Node.Invoke(state2) as int?;
				num2 = slice.pos2.Node.Invoke(state2) as int?;
			}
			int? num4 = num;
			int num5 = 0;
			int? num6;
			if (!((num4.GetValueOrDefault() <= num5) & (num4 != null)))
			{
				num4 = num;
				num5 = text.Length;
				if (!((num4.GetValueOrDefault() > num5) & (num4 != null)))
				{
					num6 = num;
					goto IL_02BE;
				}
			}
			num6 = null;
			IL_02BE:
			num = num6;
			num4 = num2;
			num5 = 0;
			int? num7;
			if (!((num4.GetValueOrDefault() <= num5) & (num4 != null)))
			{
				num4 = num2;
				num5 = text.Length;
				if (!((num4.GetValueOrDefault() > num5) & (num4 != null)))
				{
					num7 = num2;
					goto IL_030A;
				}
			}
			num7 = null;
			IL_030A:
			num2 = num7;
			return new ExampleInputToken
			{
				Id = tokenId,
				ColumnName = text2,
				Start = num,
				End = num2
			};
		}

		// Token: 0x0600A419 RID: 42009 RVA: 0x0022C580 File Offset: 0x0022A780
		private static ExampleToken ResolveOutputToken(Example example, ProgramNode stringNode, int? startPosition, string tokenId)
		{
			string text = example.Output as string;
			if (text != null)
			{
				string text2 = stringNode.Run(example.Input) as string;
				if (text2 != null)
				{
					int? num = startPosition + text2.Length;
					int? num2 = startPosition;
					int num3 = 0;
					int? num4;
					if (!((num2.GetValueOrDefault() <= num3) & (num2 != null)))
					{
						num2 = startPosition;
						num3 = text.Length;
						if (!((num2.GetValueOrDefault() > num3) & (num2 != null)))
						{
							num4 = startPosition;
							goto IL_009D;
						}
					}
					num4 = null;
					IL_009D:
					startPosition = num4;
					num2 = num;
					num3 = 0;
					int? num5;
					if (!((num2.GetValueOrDefault() <= num3) & (num2 != null)))
					{
						num2 = num;
						num3 = text.Length;
						if (!((num2.GetValueOrDefault() > num3) & (num2 != null)))
						{
							num5 = num;
							goto IL_00E7;
						}
					}
					num5 = null;
					IL_00E7:
					num = num5;
					return new ExampleToken
					{
						Id = tokenId,
						Start = startPosition,
						End = num,
						IsConstant = stringNode.Is<Str>()
					};
				}
			}
			return null;
		}

		// Token: 0x0600A41D RID: 42013 RVA: 0x0022C725 File Offset: 0x0022A925
		[CompilerGenerated]
		internal static bool <ResolveExampleSubstrings>g__IsTransformRule|24_0(ProgramNode node)
		{
			return node.IsCase() || node.Is<Trim>() || node.Is<TrimFull>() || node.Is<TrimSplit>() || node.Is<TrimSlice>() || node.Is<FormatNumber>() || node.Is<FormatDateTime>();
		}

		// Token: 0x040042A0 RID: 17056
		private IReadOnlyList<Example> _additionalExamples;

		// Token: 0x040042A1 RID: 17057
		private IReadOnlyList<Example> _allExamples;

		// Token: 0x040042A2 RID: 17058
		private readonly CancellationToken _cancellation;

		// Token: 0x040042A3 RID: 17059
		private IReadOnlyList<string> _columnsUsed;

		// Token: 0x040042A4 RID: 17060
		private readonly IReadOnlyList<Example> _examples;

		// Token: 0x040042A5 RID: 17061
		private readonly IReadOnlyList<IRow> _inputs;

		// Token: 0x040042A6 RID: 17062
		private readonly ILogger _logger;

		// Token: 0x040042A7 RID: 17063
		private readonly Program _program;

		// Token: 0x040042A8 RID: 17064
		private readonly LearnConfidenceResult _learnConfidence;

		// Token: 0x040042A9 RID: 17065
		private IReadOnlyList<ProgramNodeDetail> _nodes;
	}
}
