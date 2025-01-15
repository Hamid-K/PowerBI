using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes
{
	// Token: 0x02000A4B RID: 2635
	public struct output : IProgramNodeBuilder, IEquatable<output>
	{
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060040E1 RID: 16609 RVA: 0x000CB60A File Offset: 0x000C980A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x000CB612 File Offset: 0x000C9812
		private output(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x000CB61B File Offset: 0x000C981B
		public static output CreateUnsafe(ProgramNode node)
		{
			return new output(node);
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x000CB624 File Offset: 0x000C9824
		public static output? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.output)
			{
				return null;
			}
			return new output?(output.CreateUnsafe(node));
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x000CB65E File Offset: 0x000C985E
		public static output CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new output(new Hole(g.Symbol.output, holeId));
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x000CB676 File Offset: 0x000C9876
		public Start Cast_Start()
		{
			return Start.CreateUnsafe(this.Node);
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Start(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x000CB683 File Offset: 0x000C9883
		public bool Is_Start(GrammarBuilders g, out Start value)
		{
			value = Start.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x000CB697 File Offset: 0x000C9897
		public Start? As_Start(GrammarBuilders g)
		{
			return new Start?(Start.CreateUnsafe(this.Node));
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x000CB6A9 File Offset: 0x000C98A9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x000CB6BC File Offset: 0x000C98BC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x000CB6E6 File Offset: 0x000C98E6
		public bool Equals(output other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D86 RID: 7558
		private ProgramNode _node;
	}
}
