using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A33 RID: 6707
	public struct ToArray : IProgramNodeBuilder, IEquatable<ToArray>
	{
		// Token: 0x170024F6 RID: 9462
		// (get) Token: 0x0600DC73 RID: 56435 RVA: 0x002EEF0A File Offset: 0x002ED10A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC74 RID: 56436 RVA: 0x002EEF12 File Offset: 0x002ED112
		private ToArray(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC75 RID: 56437 RVA: 0x002EEF1B File Offset: 0x002ED11B
		public static ToArray CreateUnsafe(ProgramNode node)
		{
			return new ToArray(node);
		}

		// Token: 0x0600DC76 RID: 56438 RVA: 0x002EEF24 File Offset: 0x002ED124
		public static ToArray? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ToArray)
			{
				return null;
			}
			return new ToArray?(ToArray.CreateUnsafe(node));
		}

		// Token: 0x0600DC77 RID: 56439 RVA: 0x002EEF59 File Offset: 0x002ED159
		public ToArray(GrammarBuilders g, x value0, path value1)
		{
			this._node = g.Rule.ToArray.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600DC78 RID: 56440 RVA: 0x002EEF7F File Offset: 0x002ED17F
		public static implicit operator selectArray(ToArray arg)
		{
			return selectArray.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024F7 RID: 9463
		// (get) Token: 0x0600DC79 RID: 56441 RVA: 0x002EEF8D File Offset: 0x002ED18D
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170024F8 RID: 9464
		// (get) Token: 0x0600DC7A RID: 56442 RVA: 0x002EEFA1 File Offset: 0x002ED1A1
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DC7B RID: 56443 RVA: 0x002EEFB5 File Offset: 0x002ED1B5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC7C RID: 56444 RVA: 0x002EEFC8 File Offset: 0x002ED1C8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC7D RID: 56445 RVA: 0x002EEFF2 File Offset: 0x002ED1F2
		public bool Equals(ToArray other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005424 RID: 21540
		private ProgramNode _node;
	}
}
