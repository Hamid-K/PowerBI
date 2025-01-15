using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011F0 RID: 4592
	public struct result : IProgramNodeBuilder, IEquatable<result>
	{
		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x06008A16 RID: 35350 RVA: 0x001D014E File Offset: 0x001CE34E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008A17 RID: 35351 RVA: 0x001D0156 File Offset: 0x001CE356
		private result(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008A18 RID: 35352 RVA: 0x001D015F File Offset: 0x001CE35F
		public static result CreateUnsafe(ProgramNode node)
		{
			return new result(node);
		}

		// Token: 0x06008A19 RID: 35353 RVA: 0x001D0168 File Offset: 0x001CE368
		public static result? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.result)
			{
				return null;
			}
			return new result?(result.CreateUnsafe(node));
		}

		// Token: 0x06008A1A RID: 35354 RVA: 0x001D01A2 File Offset: 0x001CE3A2
		public static result CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new result(new Hole(g.Symbol.result, holeId));
		}

		// Token: 0x06008A1B RID: 35355 RVA: 0x001D01BA File Offset: 0x001CE3BA
		public LetResult Cast_LetResult()
		{
			return LetResult.CreateUnsafe(this.Node);
		}

		// Token: 0x06008A1C RID: 35356 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetResult(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06008A1D RID: 35357 RVA: 0x001D01C7 File Offset: 0x001CE3C7
		public bool Is_LetResult(GrammarBuilders g, out LetResult value)
		{
			value = LetResult.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06008A1E RID: 35358 RVA: 0x001D01DB File Offset: 0x001CE3DB
		public LetResult? As_LetResult(GrammarBuilders g)
		{
			return new LetResult?(LetResult.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A1F RID: 35359 RVA: 0x001D01ED File Offset: 0x001CE3ED
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008A20 RID: 35360 RVA: 0x001D0200 File Offset: 0x001CE400
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008A21 RID: 35361 RVA: 0x001D022A File Offset: 0x001CE42A
		public bool Equals(result other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038A4 RID: 14500
		private ProgramNode _node;
	}
}
