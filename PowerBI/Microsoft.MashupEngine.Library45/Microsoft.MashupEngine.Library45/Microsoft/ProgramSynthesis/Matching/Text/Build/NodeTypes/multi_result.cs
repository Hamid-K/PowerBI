using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011F3 RID: 4595
	public struct multi_result : IProgramNodeBuilder, IEquatable<multi_result>
	{
		// Token: 0x170017C1 RID: 6081
		// (get) Token: 0x06008A4A RID: 35402 RVA: 0x001D09C2 File Offset: 0x001CEBC2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008A4B RID: 35403 RVA: 0x001D09CA File Offset: 0x001CEBCA
		private multi_result(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008A4C RID: 35404 RVA: 0x001D09D3 File Offset: 0x001CEBD3
		public static multi_result CreateUnsafe(ProgramNode node)
		{
			return new multi_result(node);
		}

		// Token: 0x06008A4D RID: 35405 RVA: 0x001D09DC File Offset: 0x001CEBDC
		public static multi_result? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.multi_result)
			{
				return null;
			}
			return new multi_result?(multi_result.CreateUnsafe(node));
		}

		// Token: 0x06008A4E RID: 35406 RVA: 0x001D0A16 File Offset: 0x001CEC16
		public static multi_result CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new multi_result(new Hole(g.Symbol.multi_result, holeId));
		}

		// Token: 0x06008A4F RID: 35407 RVA: 0x001D0A2E File Offset: 0x001CEC2E
		public LetMultiResult Cast_LetMultiResult()
		{
			return LetMultiResult.CreateUnsafe(this.Node);
		}

		// Token: 0x06008A50 RID: 35408 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetMultiResult(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06008A51 RID: 35409 RVA: 0x001D0A3B File Offset: 0x001CEC3B
		public bool Is_LetMultiResult(GrammarBuilders g, out LetMultiResult value)
		{
			value = LetMultiResult.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06008A52 RID: 35410 RVA: 0x001D0A4F File Offset: 0x001CEC4F
		public LetMultiResult? As_LetMultiResult(GrammarBuilders g)
		{
			return new LetMultiResult?(LetMultiResult.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A53 RID: 35411 RVA: 0x001D0A61 File Offset: 0x001CEC61
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008A54 RID: 35412 RVA: 0x001D0A74 File Offset: 0x001CEC74
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008A55 RID: 35413 RVA: 0x001D0A9E File Offset: 0x001CEC9E
		public bool Equals(multi_result other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038A7 RID: 14503
		private ProgramNode _node;
	}
}
