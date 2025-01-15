using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Extraction.Web.Translation.CSS;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FD7 RID: 4055
	public class TextTableProgram : Program<WebRegion, IEnumerable<IEnumerable<string>>>
	{
		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06006FE8 RID: 28648 RVA: 0x0016DA9D File Offset: 0x0016BC9D
		internal resultSequence[] ColumnSelectorNodes { get; }

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06006FE9 RID: 28649 RVA: 0x0016DAA5 File Offset: 0x0016BCA5
		internal resultSequence? RowSelectorNode { get; }

		// Token: 0x06006FEA RID: 28650 RVA: 0x0016DAB0 File Offset: 0x0016BCB0
		public TextTableProgram(resultTable programNode)
			: base(programNode.Node, 0.0, null)
		{
			columnSelectors columnSelectors = programNode.Switch<columnSelectors>(Language.Build, (ExtractTable extractTable) => extractTable.columnSelectors, (ExtractRowBasedTable extractRowBasedTable) => extractRowBasedTable.columnSelectors);
			this.ColumnSelectorNodes = TextTableProgram.GetColumnSelectorNodes(columnSelectors).ToArray<resultSequence>();
			string[] array = this.ColumnSelectorNodes.Select(delegate(resultSequence p)
			{
				if (!p.Is_EmptySequence(Language.Build))
				{
					return CSSTranslator.TranslateToCssSelector(p.Node);
				}
				return null;
			}).ToArray<string>();
			this.RowSelectorNode = programNode.Switch<resultSequence?>(Language.Build, (ExtractTable extractTable) => null, (ExtractRowBasedTable extractRowBasedTable) => new resultSequence?(extractRowBasedTable.resultSequence));
			string text = ((this.RowSelectorNode == null) ? null : CSSTranslator.TranslateToCssSelector(this.RowSelectorNode.Value.Node));
			int num = TextTableProgram.GetNumOperators(this.RowSelectorNode) + this.ColumnSelectorNodes.Sum((resultSequence p) => TextTableProgram.GetNumOperators(new resultSequence?(p)));
			ProgramMetadata programMetadata = programNode.Node.Metadata as ProgramMetadata;
			this.Properties = new ProgramProperties(array, text, num, (programMetadata != null) ? programMetadata.TableTitle : null, (programMetadata != null) ? new TableKind?(programMetadata.TableKind) : null, (programMetadata != null) ? programMetadata.RowCount : (-1), (programMetadata != null) ? programMetadata.Attributes : null);
		}

		// Token: 0x06006FEB RID: 28651 RVA: 0x0016DC80 File Offset: 0x0016BE80
		public static int GetNumOperators(resultSequence? prog)
		{
			if (prog == null || prog.Value.Is_EmptySequence(Language.Build))
			{
				return 0;
			}
			int num = 0;
			num += TextTableProgram.AllNodes(prog.Value.Node).Collect(new Func<ProgramNode, nodeCollection?>(Language.Build.Node.As.nodeCollection)).Count<nodeCollection>();
			if (num > 0)
			{
				num--;
			}
			num += TextTableProgram.AllNodes(prog.Value.Node).Collect(new Func<ProgramNode, atomExpr?>(Language.Build.Node.As.atomExpr)).Count<atomExpr>();
			HashSet<string> opNames = new HashSet<string> { "LeafChildrenOf", "LeafChildrenOf1", "LeafChildrenOf2", "LeafChildrenOf3", "LeafChildrenOf4" };
			return num + TextTableProgram.AllNodes(prog.Value.Node).Count(delegate(ProgramNode n)
			{
				HashSet<string> opNames2 = opNames;
				BlackBoxRule blackBoxRule = n.GrammarRule as BlackBoxRule;
				return opNames2.Contains((blackBoxRule != null) ? blackBoxRule.Name : null);
			});
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06006FEC RID: 28652 RVA: 0x0016DDA4 File Offset: 0x0016BFA4
		public static Symbol ProgramSymbol { get; } = Language.Build.Symbol.resultTable;

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06006FED RID: 28653 RVA: 0x0016DDAB File Offset: 0x0016BFAB
		public ProgramProperties Properties { get; }

		// Token: 0x06006FEE RID: 28654 RVA: 0x0016DDB4 File Offset: 0x0016BFB4
		public override IEnumerable<IEnumerable<string>> Run(WebRegion input)
		{
			State state = State.CreateForExecution(base.ProgramNode.Grammar.InputSymbol, input.GetAllChildrenAndSelf().ToArray<IDomNode>());
			return (base.ProgramNode.Invoke(state) as IEnumerable<IEnumerable<WebRegion>>).Select((IEnumerable<WebRegion> c, int k) => c.Select(delegate(WebRegion r)
			{
				string text = this.<Run>g__GetTextValue|14_0(r, k);
				return ((text != null) ? text.Trim() : null) ?? string.Empty;
			}).ToList<string>()).ToList<List<string>>();
		}

		// Token: 0x06006FEF RID: 28655 RVA: 0x0016DE09 File Offset: 0x0016C009
		private static IEnumerable<ProgramNode> AllNodes(ProgramNode p)
		{
			IEnumerable<ProgramNode> children = p.Children;
			Func<ProgramNode, IEnumerable<ProgramNode>> func;
			if ((func = TextTableProgram.<>O.<0>__AllNodes) == null)
			{
				func = (TextTableProgram.<>O.<0>__AllNodes = new Func<ProgramNode, IEnumerable<ProgramNode>>(TextTableProgram.AllNodes));
			}
			return children.SelectMany(func).AppendItem(p);
		}

		// Token: 0x06006FF0 RID: 28656 RVA: 0x0016DE38 File Offset: 0x0016C038
		private static IEnumerable<resultSequence> GetColumnSelectorNodes(columnSelectors colSelectorsNode)
		{
			return colSelectorsNode.Switch<IEnumerable<resultSequence>>(Language.Build, (SingleColumn singleCol) => new resultSequence[] { singleCol.resultSequence }, (ColumnSequence colSeq) => TextTableProgram.GetColumnSelectorNodes(colSeq.columnSelectors).AppendItem(colSeq.resultSequence));
		}

		// Token: 0x06006FF2 RID: 28658 RVA: 0x0016DEA8 File Offset: 0x0016C0A8
		[CompilerGenerated]
		private string <Run>g__GetTextValue|14_0(WebRegion r, int colIndex)
		{
			string[] attributes = this.Properties.Attributes;
			string text = ((attributes != null) ? attributes[colIndex] : null);
			if (text == null)
			{
				if (r == null)
				{
					return null;
				}
				IDomNode beginNode = r.BeginNode;
				if (beginNode == null)
				{
					return null;
				}
				return beginNode.InnerText;
			}
			else
			{
				if (r == null)
				{
					return null;
				}
				IDomNode beginNode2 = r.BeginNode;
				if (beginNode2 == null)
				{
					return null;
				}
				return beginNode2.GetAttribute(text);
			}
		}

		// Token: 0x02000FD8 RID: 4056
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040030BB RID: 12475
			public static Func<ProgramNode, IEnumerable<ProgramNode>> <0>__AllNodes;
		}
	}
}
