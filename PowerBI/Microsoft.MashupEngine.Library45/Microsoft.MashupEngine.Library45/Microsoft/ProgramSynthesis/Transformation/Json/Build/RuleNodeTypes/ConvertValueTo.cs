using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A2F RID: 6703
	public struct ConvertValueTo : IProgramNodeBuilder, IEquatable<ConvertValueTo>
	{
		// Token: 0x170024EB RID: 9451
		// (get) Token: 0x0600DC48 RID: 56392 RVA: 0x002EEB2E File Offset: 0x002ECD2E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC49 RID: 56393 RVA: 0x002EEB36 File Offset: 0x002ECD36
		private ConvertValueTo(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC4A RID: 56394 RVA: 0x002EEB3F File Offset: 0x002ECD3F
		public static ConvertValueTo CreateUnsafe(ProgramNode node)
		{
			return new ConvertValueTo(node);
		}

		// Token: 0x0600DC4B RID: 56395 RVA: 0x002EEB48 File Offset: 0x002ECD48
		public static ConvertValueTo? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConvertValueTo)
			{
				return null;
			}
			return new ConvertValueTo?(ConvertValueTo.CreateUnsafe(node));
		}

		// Token: 0x0600DC4C RID: 56396 RVA: 0x002EEB7D File Offset: 0x002ECD7D
		public ConvertValueTo(GrammarBuilders g, x value0, t value1, path value2)
		{
			this._node = g.Rule.ConvertValueTo.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600DC4D RID: 56397 RVA: 0x002EEBAA File Offset: 0x002ECDAA
		public static implicit operator selectValue(ConvertValueTo arg)
		{
			return selectValue.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024EC RID: 9452
		// (get) Token: 0x0600DC4E RID: 56398 RVA: 0x002EEBB8 File Offset: 0x002ECDB8
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170024ED RID: 9453
		// (get) Token: 0x0600DC4F RID: 56399 RVA: 0x002EEBCC File Offset: 0x002ECDCC
		public t t
		{
			get
			{
				return t.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x170024EE RID: 9454
		// (get) Token: 0x0600DC50 RID: 56400 RVA: 0x002EEBE0 File Offset: 0x002ECDE0
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600DC51 RID: 56401 RVA: 0x002EEBF4 File Offset: 0x002ECDF4
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC52 RID: 56402 RVA: 0x002EEC08 File Offset: 0x002ECE08
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC53 RID: 56403 RVA: 0x002EEC32 File Offset: 0x002ECE32
		public bool Equals(ConvertValueTo other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005420 RID: 21536
		private ProgramNode _node;
	}
}
