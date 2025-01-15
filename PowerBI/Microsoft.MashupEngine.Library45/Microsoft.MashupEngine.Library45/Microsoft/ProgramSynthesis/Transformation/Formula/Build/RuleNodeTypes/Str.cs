using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001595 RID: 5525
	public struct Str : IProgramNodeBuilder, IEquatable<Str>
	{
		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x0600B50F RID: 46351 RVA: 0x00275B62 File Offset: 0x00273D62
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B510 RID: 46352 RVA: 0x00275B6A File Offset: 0x00273D6A
		private Str(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B511 RID: 46353 RVA: 0x00275B73 File Offset: 0x00273D73
		public static Str CreateUnsafe(ProgramNode node)
		{
			return new Str(node);
		}

		// Token: 0x0600B512 RID: 46354 RVA: 0x00275B7C File Offset: 0x00273D7C
		public static Str? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Str)
			{
				return null;
			}
			return new Str?(Str.CreateUnsafe(node));
		}

		// Token: 0x0600B513 RID: 46355 RVA: 0x00275BB1 File Offset: 0x00273DB1
		public Str(GrammarBuilders g, constStr value0)
		{
			this._node = g.Rule.Str.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B514 RID: 46356 RVA: 0x00275BD0 File Offset: 0x00273DD0
		public static implicit operator constString(Str arg)
		{
			return constString.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x0600B515 RID: 46357 RVA: 0x00275BDE File Offset: 0x00273DDE
		public constStr constStr
		{
			get
			{
				return constStr.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B516 RID: 46358 RVA: 0x00275BF2 File Offset: 0x00273DF2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B517 RID: 46359 RVA: 0x00275C08 File Offset: 0x00273E08
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B518 RID: 46360 RVA: 0x00275C32 File Offset: 0x00273E32
		public bool Equals(Str other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004643 RID: 17987
		private ProgramNode _node;
	}
}
