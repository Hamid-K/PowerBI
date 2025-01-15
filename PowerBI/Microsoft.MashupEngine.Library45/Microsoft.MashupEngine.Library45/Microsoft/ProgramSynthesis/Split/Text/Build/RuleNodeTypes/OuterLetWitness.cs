using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200135E RID: 4958
	public struct OuterLetWitness : IProgramNodeBuilder, IEquatable<OuterLetWitness>
	{
		// Token: 0x17001A65 RID: 6757
		// (get) Token: 0x06009918 RID: 39192 RVA: 0x00207B66 File Offset: 0x00205D66
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009919 RID: 39193 RVA: 0x00207B6E File Offset: 0x00205D6E
		private OuterLetWitness(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600991A RID: 39194 RVA: 0x00207B77 File Offset: 0x00205D77
		public static OuterLetWitness CreateUnsafe(ProgramNode node)
		{
			return new OuterLetWitness(node);
		}

		// Token: 0x0600991B RID: 39195 RVA: 0x00207B80 File Offset: 0x00205D80
		public static OuterLetWitness? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.OuterLetWitness)
			{
				return null;
			}
			return new OuterLetWitness?(OuterLetWitness.CreateUnsafe(node));
		}

		// Token: 0x0600991C RID: 39196 RVA: 0x00207BB5 File Offset: 0x00205DB5
		public OuterLetWitness(GrammarBuilders g, _LetB2 value0, _LetB3 value1)
		{
			this._node = new LetNode(g.Rule.OuterLetWitness, value0.Node, value1.Node);
		}

		// Token: 0x0600991D RID: 39197 RVA: 0x00207BDB File Offset: 0x00205DDB
		public static implicit operator output(OuterLetWitness arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A66 RID: 6758
		// (get) Token: 0x0600991E RID: 39198 RVA: 0x00207BE9 File Offset: 0x00205DE9
		public _LetB2 _LetB2
		{
			get
			{
				return _LetB2.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A67 RID: 6759
		// (get) Token: 0x0600991F RID: 39199 RVA: 0x00207BFD File Offset: 0x00205DFD
		public _LetB3 _LetB3
		{
			get
			{
				return _LetB3.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06009920 RID: 39200 RVA: 0x00207C11 File Offset: 0x00205E11
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009921 RID: 39201 RVA: 0x00207C24 File Offset: 0x00205E24
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009922 RID: 39202 RVA: 0x00207C4E File Offset: 0x00205E4E
		public bool Equals(OuterLetWitness other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DD5 RID: 15829
		private ProgramNode _node;
	}
}
