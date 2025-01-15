using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E88 RID: 7816
	public struct pos : IProgramNodeBuilder, IEquatable<pos>
	{
		// Token: 0x17002BE1 RID: 11233
		// (get) Token: 0x06010820 RID: 67616 RVA: 0x0038D11E File Offset: 0x0038B31E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010821 RID: 67617 RVA: 0x0038D126 File Offset: 0x0038B326
		private pos(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010822 RID: 67618 RVA: 0x0038D12F File Offset: 0x0038B32F
		public static pos CreateUnsafe(ProgramNode node)
		{
			return new pos(node);
		}

		// Token: 0x06010823 RID: 67619 RVA: 0x0038D138 File Offset: 0x0038B338
		public static pos? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pos)
			{
				return null;
			}
			return new pos?(pos.CreateUnsafe(node));
		}

		// Token: 0x06010824 RID: 67620 RVA: 0x0038D172 File Offset: 0x0038B372
		public static pos CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pos(new Hole(g.Symbol.pos, holeId));
		}

		// Token: 0x06010825 RID: 67621 RVA: 0x0038D18A File Offset: 0x0038B38A
		public AbsPos Cast_AbsPos()
		{
			return AbsPos.CreateUnsafe(this.Node);
		}

		// Token: 0x06010826 RID: 67622 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_AbsPos(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06010827 RID: 67623 RVA: 0x0038D197 File Offset: 0x0038B397
		public bool Is_AbsPos(GrammarBuilders g, out AbsPos value)
		{
			value = AbsPos.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06010828 RID: 67624 RVA: 0x0038D1AB File Offset: 0x0038B3AB
		public AbsPos? As_AbsPos(GrammarBuilders g)
		{
			return new AbsPos?(AbsPos.CreateUnsafe(this.Node));
		}

		// Token: 0x06010829 RID: 67625 RVA: 0x0038D1BD File Offset: 0x0038B3BD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601082A RID: 67626 RVA: 0x0038D1D0 File Offset: 0x0038B3D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601082B RID: 67627 RVA: 0x0038D1FA File Offset: 0x0038B3FA
		public bool Equals(pos other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062C7 RID: 25287
		private ProgramNode _node;
	}
}
