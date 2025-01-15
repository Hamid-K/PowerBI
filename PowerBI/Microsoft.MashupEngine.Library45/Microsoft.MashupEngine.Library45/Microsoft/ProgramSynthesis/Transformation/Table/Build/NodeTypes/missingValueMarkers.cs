using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001ABF RID: 6847
	public struct missingValueMarkers : IProgramNodeBuilder, IEquatable<missingValueMarkers>
	{
		// Token: 0x170025E3 RID: 9699
		// (get) Token: 0x0600E27D RID: 57981 RVA: 0x00301BF6 File Offset: 0x002FFDF6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E27E RID: 57982 RVA: 0x00301BFE File Offset: 0x002FFDFE
		private missingValueMarkers(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E27F RID: 57983 RVA: 0x00301C07 File Offset: 0x002FFE07
		public static missingValueMarkers CreateUnsafe(ProgramNode node)
		{
			return new missingValueMarkers(node);
		}

		// Token: 0x0600E280 RID: 57984 RVA: 0x00301C10 File Offset: 0x002FFE10
		public static missingValueMarkers? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.missingValueMarkers)
			{
				return null;
			}
			return new missingValueMarkers?(missingValueMarkers.CreateUnsafe(node));
		}

		// Token: 0x0600E281 RID: 57985 RVA: 0x00301C4A File Offset: 0x002FFE4A
		public static missingValueMarkers CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new missingValueMarkers(new Hole(g.Symbol.missingValueMarkers, holeId));
		}

		// Token: 0x0600E282 RID: 57986 RVA: 0x00301C62 File Offset: 0x002FFE62
		public missingValueMarkers(GrammarBuilders g, IEnumerable<object> value)
		{
			this = new missingValueMarkers(new LiteralNode(g.Symbol.missingValueMarkers, value));
		}

		// Token: 0x170025E4 RID: 9700
		// (get) Token: 0x0600E283 RID: 57987 RVA: 0x00301C7B File Offset: 0x002FFE7B
		public IEnumerable<object> Value
		{
			get
			{
				return (IEnumerable<object>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600E284 RID: 57988 RVA: 0x00301C92 File Offset: 0x002FFE92
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E285 RID: 57989 RVA: 0x00301CA8 File Offset: 0x002FFEA8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E286 RID: 57990 RVA: 0x00301CD2 File Offset: 0x002FFED2
		public bool Equals(missingValueMarkers other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400557E RID: 21886
		private ProgramNode _node;
	}
}
