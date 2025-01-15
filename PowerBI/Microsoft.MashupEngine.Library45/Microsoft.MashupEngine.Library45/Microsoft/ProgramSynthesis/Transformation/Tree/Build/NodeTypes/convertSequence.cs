using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E82 RID: 7810
	public struct convertSequence : IProgramNodeBuilder, IEquatable<convertSequence>
	{
		// Token: 0x17002BDB RID: 11227
		// (get) Token: 0x060107D2 RID: 67538 RVA: 0x0038C932 File Offset: 0x0038AB32
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060107D3 RID: 67539 RVA: 0x0038C93A File Offset: 0x0038AB3A
		private convertSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060107D4 RID: 67540 RVA: 0x0038C943 File Offset: 0x0038AB43
		public static convertSequence CreateUnsafe(ProgramNode node)
		{
			return new convertSequence(node);
		}

		// Token: 0x060107D5 RID: 67541 RVA: 0x0038C94C File Offset: 0x0038AB4C
		public static convertSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.convertSequence)
			{
				return null;
			}
			return new convertSequence?(convertSequence.CreateUnsafe(node));
		}

		// Token: 0x060107D6 RID: 67542 RVA: 0x0038C986 File Offset: 0x0038AB86
		public static convertSequence CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new convertSequence(new Hole(g.Symbol.convertSequence, holeId));
		}

		// Token: 0x060107D7 RID: 67543 RVA: 0x0038C99E File Offset: 0x0038AB9E
		public ConvertSequence Cast_ConvertSequence()
		{
			return ConvertSequence.CreateUnsafe(this.Node);
		}

		// Token: 0x060107D8 RID: 67544 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_ConvertSequence(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x060107D9 RID: 67545 RVA: 0x0038C9AB File Offset: 0x0038ABAB
		public bool Is_ConvertSequence(GrammarBuilders g, out ConvertSequence value)
		{
			value = ConvertSequence.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x060107DA RID: 67546 RVA: 0x0038C9BF File Offset: 0x0038ABBF
		public ConvertSequence? As_ConvertSequence(GrammarBuilders g)
		{
			return new ConvertSequence?(ConvertSequence.CreateUnsafe(this.Node));
		}

		// Token: 0x060107DB RID: 67547 RVA: 0x0038C9D1 File Offset: 0x0038ABD1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060107DC RID: 67548 RVA: 0x0038C9E4 File Offset: 0x0038ABE4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060107DD RID: 67549 RVA: 0x0038CA0E File Offset: 0x0038AC0E
		public bool Equals(convertSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062C1 RID: 25281
		private ProgramNode _node;
	}
}
