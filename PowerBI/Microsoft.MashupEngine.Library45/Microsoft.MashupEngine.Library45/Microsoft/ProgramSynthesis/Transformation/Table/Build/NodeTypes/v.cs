using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001AC3 RID: 6851
	public struct v : IProgramNodeBuilder, IEquatable<v>
	{
		// Token: 0x170025EB RID: 9707
		// (get) Token: 0x0600E2A5 RID: 58021 RVA: 0x00301FBA File Offset: 0x003001BA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E2A6 RID: 58022 RVA: 0x00301FC2 File Offset: 0x003001C2
		private v(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E2A7 RID: 58023 RVA: 0x00301FCB File Offset: 0x003001CB
		public static v CreateUnsafe(ProgramNode node)
		{
			return new v(node);
		}

		// Token: 0x0600E2A8 RID: 58024 RVA: 0x00301FD4 File Offset: 0x003001D4
		public static v? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.v)
			{
				return null;
			}
			return new v?(v.CreateUnsafe(node));
		}

		// Token: 0x0600E2A9 RID: 58025 RVA: 0x0030200E File Offset: 0x0030020E
		public static v CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new v(new Hole(g.Symbol.v, holeId));
		}

		// Token: 0x0600E2AA RID: 58026 RVA: 0x00302026 File Offset: 0x00300226
		public v(GrammarBuilders g)
		{
			this = new v(new VariableNode(g.Symbol.v));
		}

		// Token: 0x170025EC RID: 9708
		// (get) Token: 0x0600E2AB RID: 58027 RVA: 0x0030203E File Offset: 0x0030023E
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600E2AC RID: 58028 RVA: 0x0030204B File Offset: 0x0030024B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E2AD RID: 58029 RVA: 0x00302060 File Offset: 0x00300260
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E2AE RID: 58030 RVA: 0x0030208A File Offset: 0x0030028A
		public bool Equals(v other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005582 RID: 21890
		private ProgramNode _node;
	}
}
