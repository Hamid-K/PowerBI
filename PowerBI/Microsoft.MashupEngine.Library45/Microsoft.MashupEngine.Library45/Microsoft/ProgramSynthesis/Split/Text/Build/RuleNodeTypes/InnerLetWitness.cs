using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200135D RID: 4957
	public struct InnerLetWitness : IProgramNodeBuilder, IEquatable<InnerLetWitness>
	{
		// Token: 0x17001A62 RID: 6754
		// (get) Token: 0x0600990D RID: 39181 RVA: 0x00207A6A File Offset: 0x00205C6A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600990E RID: 39182 RVA: 0x00207A72 File Offset: 0x00205C72
		private InnerLetWitness(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600990F RID: 39183 RVA: 0x00207A7B File Offset: 0x00205C7B
		public static InnerLetWitness CreateUnsafe(ProgramNode node)
		{
			return new InnerLetWitness(node);
		}

		// Token: 0x06009910 RID: 39184 RVA: 0x00207A84 File Offset: 0x00205C84
		public static InnerLetWitness? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.InnerLetWitness)
			{
				return null;
			}
			return new InnerLetWitness?(InnerLetWitness.CreateUnsafe(node));
		}

		// Token: 0x06009911 RID: 39185 RVA: 0x00207AB9 File Offset: 0x00205CB9
		public InnerLetWitness(GrammarBuilders g, _LetB0 value0, _LetB1 value1)
		{
			this._node = new LetNode(g.Rule.InnerLetWitness, value0.Node, value1.Node);
		}

		// Token: 0x06009912 RID: 39186 RVA: 0x00207ADF File Offset: 0x00205CDF
		public static implicit operator _LetB3(InnerLetWitness arg)
		{
			return _LetB3.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A63 RID: 6755
		// (get) Token: 0x06009913 RID: 39187 RVA: 0x00207AED File Offset: 0x00205CED
		public _LetB0 _LetB0
		{
			get
			{
				return _LetB0.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A64 RID: 6756
		// (get) Token: 0x06009914 RID: 39188 RVA: 0x00207B01 File Offset: 0x00205D01
		public _LetB1 _LetB1
		{
			get
			{
				return _LetB1.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06009915 RID: 39189 RVA: 0x00207B15 File Offset: 0x00205D15
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009916 RID: 39190 RVA: 0x00207B28 File Offset: 0x00205D28
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009917 RID: 39191 RVA: 0x00207B52 File Offset: 0x00205D52
		public bool Equals(InnerLetWitness other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DD4 RID: 15828
		private ProgramNode _node;
	}
}
