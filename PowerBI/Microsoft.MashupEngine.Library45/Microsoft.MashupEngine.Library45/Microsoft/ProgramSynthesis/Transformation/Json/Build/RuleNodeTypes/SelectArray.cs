using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A32 RID: 6706
	public struct SelectArray : IProgramNodeBuilder, IEquatable<SelectArray>
	{
		// Token: 0x170024F3 RID: 9459
		// (get) Token: 0x0600DC68 RID: 56424 RVA: 0x002EEE0E File Offset: 0x002ED00E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC69 RID: 56425 RVA: 0x002EEE16 File Offset: 0x002ED016
		private SelectArray(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC6A RID: 56426 RVA: 0x002EEE1F File Offset: 0x002ED01F
		public static SelectArray CreateUnsafe(ProgramNode node)
		{
			return new SelectArray(node);
		}

		// Token: 0x0600DC6B RID: 56427 RVA: 0x002EEE28 File Offset: 0x002ED028
		public static SelectArray? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectArray)
			{
				return null;
			}
			return new SelectArray?(SelectArray.CreateUnsafe(node));
		}

		// Token: 0x0600DC6C RID: 56428 RVA: 0x002EEE5D File Offset: 0x002ED05D
		public SelectArray(GrammarBuilders g, x value0, path value1)
		{
			this._node = g.Rule.SelectArray.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600DC6D RID: 56429 RVA: 0x002EEE83 File Offset: 0x002ED083
		public static implicit operator selectArray(SelectArray arg)
		{
			return selectArray.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024F4 RID: 9460
		// (get) Token: 0x0600DC6E RID: 56430 RVA: 0x002EEE91 File Offset: 0x002ED091
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170024F5 RID: 9461
		// (get) Token: 0x0600DC6F RID: 56431 RVA: 0x002EEEA5 File Offset: 0x002ED0A5
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DC70 RID: 56432 RVA: 0x002EEEB9 File Offset: 0x002ED0B9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC71 RID: 56433 RVA: 0x002EEECC File Offset: 0x002ED0CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC72 RID: 56434 RVA: 0x002EEEF6 File Offset: 0x002ED0F6
		public bool Equals(SelectArray other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005423 RID: 21539
		private ProgramNode _node;
	}
}
