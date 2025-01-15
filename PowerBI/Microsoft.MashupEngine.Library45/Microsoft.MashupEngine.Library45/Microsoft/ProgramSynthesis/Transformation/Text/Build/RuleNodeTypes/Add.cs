using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C14 RID: 7188
	public struct Add : IProgramNodeBuilder, IEquatable<Add>
	{
		// Token: 0x17002864 RID: 10340
		// (get) Token: 0x0600F1E3 RID: 61923 RVA: 0x003403BA File Offset: 0x0033E5BA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F1E4 RID: 61924 RVA: 0x003403C2 File Offset: 0x0033E5C2
		private Add(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F1E5 RID: 61925 RVA: 0x003403CB File Offset: 0x0033E5CB
		public static Add CreateUnsafe(ProgramNode node)
		{
			return new Add(node);
		}

		// Token: 0x0600F1E6 RID: 61926 RVA: 0x003403D4 File Offset: 0x0033E5D4
		public static Add? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Add)
			{
				return null;
			}
			return new Add?(Add.CreateUnsafe(node));
		}

		// Token: 0x0600F1E7 RID: 61927 RVA: 0x00340409 File Offset: 0x0033E609
		public Add(GrammarBuilders g, pl1 value0, pl2 value1)
		{
			this._node = g.Rule.Add.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F1E8 RID: 61928 RVA: 0x0034042F File Offset: 0x0033E62F
		public static implicit operator _LetB2(Add arg)
		{
			return _LetB2.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002865 RID: 10341
		// (get) Token: 0x0600F1E9 RID: 61929 RVA: 0x0034043D File Offset: 0x0033E63D
		public pl1 pl1
		{
			get
			{
				return pl1.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002866 RID: 10342
		// (get) Token: 0x0600F1EA RID: 61930 RVA: 0x00340451 File Offset: 0x0033E651
		public pl2 pl2
		{
			get
			{
				return pl2.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F1EB RID: 61931 RVA: 0x00340465 File Offset: 0x0033E665
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F1EC RID: 61932 RVA: 0x00340478 File Offset: 0x0033E678
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F1ED RID: 61933 RVA: 0x003404A2 File Offset: 0x0033E6A2
		public bool Equals(Add other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B03 RID: 23299
		private ProgramNode _node;
	}
}
