using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C64 RID: 7268
	public struct dtRoundingSpec : IProgramNodeBuilder, IEquatable<dtRoundingSpec>
	{
		// Token: 0x17002900 RID: 10496
		// (get) Token: 0x0600F620 RID: 63008 RVA: 0x00348C66 File Offset: 0x00346E66
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F621 RID: 63009 RVA: 0x00348C6E File Offset: 0x00346E6E
		private dtRoundingSpec(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F622 RID: 63010 RVA: 0x00348C77 File Offset: 0x00346E77
		public static dtRoundingSpec CreateUnsafe(ProgramNode node)
		{
			return new dtRoundingSpec(node);
		}

		// Token: 0x0600F623 RID: 63011 RVA: 0x00348C80 File Offset: 0x00346E80
		public static dtRoundingSpec? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.dtRoundingSpec)
			{
				return null;
			}
			return new dtRoundingSpec?(dtRoundingSpec.CreateUnsafe(node));
		}

		// Token: 0x0600F624 RID: 63012 RVA: 0x00348CBA File Offset: 0x00346EBA
		public static dtRoundingSpec CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new dtRoundingSpec(new Hole(g.Symbol.dtRoundingSpec, holeId));
		}

		// Token: 0x0600F625 RID: 63013 RVA: 0x00348CD2 File Offset: 0x00346ED2
		public dtRoundingSpec(GrammarBuilders g, DateTimeRoundingSpec value)
		{
			this = new dtRoundingSpec(new LiteralNode(g.Symbol.dtRoundingSpec, value));
		}

		// Token: 0x17002901 RID: 10497
		// (get) Token: 0x0600F626 RID: 63014 RVA: 0x00348CEB File Offset: 0x00346EEB
		public DateTimeRoundingSpec Value
		{
			get
			{
				return (DateTimeRoundingSpec)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F627 RID: 63015 RVA: 0x00348D02 File Offset: 0x00346F02
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F628 RID: 63016 RVA: 0x00348D18 File Offset: 0x00346F18
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F629 RID: 63017 RVA: 0x00348D42 File Offset: 0x00346F42
		public bool Equals(dtRoundingSpec other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B53 RID: 23379
		private ProgramNode _node;
	}
}
