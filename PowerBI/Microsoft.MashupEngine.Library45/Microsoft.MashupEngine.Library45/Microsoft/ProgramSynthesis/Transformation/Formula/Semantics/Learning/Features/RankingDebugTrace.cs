using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;
using Microsoft.ProgramSynthesis.Utils.Text;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Features
{
	// Token: 0x020016F5 RID: 5877
	public class RankingDebugTrace
	{
		// Token: 0x0600C3F6 RID: 50166 RVA: 0x002A3063 File Offset: 0x002A1263
		public void Record(RankingDebugAggregateBuffer item)
		{
			this._aggregates[RankingDebugTrace.ResolveKey(item.Node)] = item;
		}

		// Token: 0x0600C3F7 RID: 50167 RVA: 0x002A307C File Offset: 0x002A127C
		public void RecordRatio(ProgramNode node, double? ratio, RankingRatioKind kind)
		{
			if (this._toStringCache.ContainsKey(node.ToString()))
			{
				this._toStringCache.Remove(node.ToString());
			}
			if (kind == RankingRatioKind.Award)
			{
				this._awardRatios[RankingDebugTrace.ResolveKey(node)] = ratio;
				return;
			}
			this._penaltyRatios[RankingDebugTrace.ResolveKey(node)] = ratio;
		}

		// Token: 0x0600C3F8 RID: 50168 RVA: 0x002A30D6 File Offset: 0x002A12D6
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x0600C3F9 RID: 50169 RVA: 0x002A30E0 File Offset: 0x002A12E0
		public string ToString(ProgramNode sourceNode)
		{
			string text = sourceNode.ToString();
			string text2;
			if (this._toStringCache.TryGetValue(text, out text2))
			{
				return text2;
			}
			RankingDebugAggregateBuffer rankingDebugAggregateBuffer;
			if (!this._aggregates.TryGetValue(RankingDebugTrace.ResolveKey(sourceNode), out rankingDebugAggregateBuffer))
			{
				return string.Empty;
			}
			ITextRowBuilder textRowBuilder = TextTableBuilder.Create(TextTableBorder.Column, null, null).AddColumn("Node", 0, null, false, null, null, null, null).AddColumn("Value", 0, new int?(30), true, null, null, null, null)
				.AddNumberColumn("Award", "N4", 0, "--", null, null)
				.AddNumberColumn("Penalty", "N4", 0, "--", null, null)
				.AddHeadingRow()
				.AddBorderRow();
			IEnumerable<ProgramNode> enumerable = ProgramExtractVisitor.ExtractNodes(sourceNode, false);
			textRowBuilder.AddDataRows((from child in enumerable
				let key = RankingDebugTrace.ResolveKey(child)
				let hasAward = this._awardRatios.ContainsKey(key)
				let award = hasAward ? this._awardRatios[key] : null
				let hasPenalty = this._penaltyRatios.ContainsKey(key)
				let penalty = hasPenalty ? this._penaltyRatios[key] : null
				where !(child is VariableNode)
				select <>h__TransparentIdentifier4).Select(delegate(<>h__TransparentIdentifier4)
			{
				object[] array = new object[4];
				array[0] = RankingDebugTrace.ResolveLabel(<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.child);
				int num = 1;
				object obj = RankingDebugTrace.ResolveValue(<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.child);
				array[num] = ((obj != null) ? obj.ToCSharpPseudoLiteral() : null);
				array[2] = <>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.award;
				array[3] = <>h__TransparentIdentifier4.penalty;
				return array;
			}).ToList<object[]>(), null);
			textRowBuilder.AddDoubleBorderRow().AddDataRow(new object[]
			{
				string.Empty,
				string.Empty,
				rankingDebugAggregateBuffer.AwardRatio,
				rankingDebugAggregateBuffer.PenaltyRatio.ToString("N4")
			}).AddDataRow(new object[]
			{
				string.Empty,
				string.Format("{0:N0}", rankingDebugAggregateBuffer.MaxScore),
				string.Format("+{0:N4}", rankingDebugAggregateBuffer.AwardScore),
				string.Format("-{0:N4}", rankingDebugAggregateBuffer.PenaltyScore)
			})
				.AddDataRow(new object[]
				{
					string.Empty,
					string.Empty,
					string.Empty,
					string.Format("{0:N4}", rankingDebugAggregateBuffer.Score)
				});
			return this._toStringCache[text] = Environment.NewLine + textRowBuilder.Render() + Environment.NewLine;
		}

		// Token: 0x0600C3FA RID: 50170 RVA: 0x002A33C4 File Offset: 0x002A15C4
		private static string ResolveKey(ProgramNode node)
		{
			LiteralNode literalNode = node as LiteralNode;
			string text;
			if (literalNode != null)
			{
				text = string.Format("{0}:{1}", literalNode.Symbol.Name, literalNode);
			}
			else
			{
				text = node.ToString();
			}
			return text;
		}

		// Token: 0x0600C3FB RID: 50171 RVA: 0x002A33FC File Offset: 0x002A15FC
		private static string ResolveLabel(ProgramNode node)
		{
			LiteralNode literalNode = node as LiteralNode;
			string text;
			if (literalNode == null)
			{
				VariableNode variableNode = node as VariableNode;
				if (variableNode == null)
				{
					NonterminalNode nonterminalNode = node as NonterminalNode;
					if (nonterminalNode == null)
					{
						text = null;
					}
					else
					{
						text = nonterminalNode.Rule.Id;
					}
				}
				else
				{
					text = variableNode.Symbol.Name;
				}
			}
			else
			{
				text = literalNode.Symbol.Name;
			}
			return text;
		}

		// Token: 0x0600C3FC RID: 50172 RVA: 0x002A3458 File Offset: 0x002A1658
		private static object ResolveValue(ProgramNode node)
		{
			LiteralNode literalNode = node as LiteralNode;
			object obj;
			if (literalNode != null)
			{
				obj = literalNode.Value;
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x04004C6D RID: 19565
		private readonly Dictionary<string, RankingDebugAggregateBuffer> _aggregates = new Dictionary<string, RankingDebugAggregateBuffer>();

		// Token: 0x04004C6E RID: 19566
		private readonly Dictionary<string, double?> _awardRatios = new Dictionary<string, double?>();

		// Token: 0x04004C6F RID: 19567
		private readonly Dictionary<string, double?> _penaltyRatios = new Dictionary<string, double?>();

		// Token: 0x04004C70 RID: 19568
		private readonly Dictionary<string, string> _toStringCache = new Dictionary<string, string>();
	}
}
