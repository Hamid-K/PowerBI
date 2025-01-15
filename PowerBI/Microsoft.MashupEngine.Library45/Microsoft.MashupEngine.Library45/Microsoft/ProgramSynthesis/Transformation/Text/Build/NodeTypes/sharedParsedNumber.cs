using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C75 RID: 7285
	public struct sharedParsedNumber : IProgramNodeBuilder, IEquatable<sharedParsedNumber>
	{
		// Token: 0x17002922 RID: 10530
		// (get) Token: 0x0600F6CA RID: 63178 RVA: 0x00349C4E File Offset: 0x00347E4E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F6CB RID: 63179 RVA: 0x00349C56 File Offset: 0x00347E56
		private sharedParsedNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F6CC RID: 63180 RVA: 0x00349C5F File Offset: 0x00347E5F
		public static sharedParsedNumber CreateUnsafe(ProgramNode node)
		{
			return new sharedParsedNumber(node);
		}

		// Token: 0x0600F6CD RID: 63181 RVA: 0x00349C68 File Offset: 0x00347E68
		public static sharedParsedNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sharedParsedNumber)
			{
				return null;
			}
			return new sharedParsedNumber?(sharedParsedNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F6CE RID: 63182 RVA: 0x00349CA2 File Offset: 0x00347EA2
		public static sharedParsedNumber CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sharedParsedNumber(new Hole(g.Symbol.sharedParsedNumber, holeId));
		}

		// Token: 0x0600F6CF RID: 63183 RVA: 0x00349CBA File Offset: 0x00347EBA
		public sharedParsedNumber(GrammarBuilders g)
		{
			this = new sharedParsedNumber(new VariableNode(g.Symbol.sharedParsedNumber));
		}

		// Token: 0x17002923 RID: 10531
		// (get) Token: 0x0600F6D0 RID: 63184 RVA: 0x00349CD2 File Offset: 0x00347ED2
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F6D1 RID: 63185 RVA: 0x00349CDF File Offset: 0x00347EDF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F6D2 RID: 63186 RVA: 0x00349CF4 File Offset: 0x00347EF4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F6D3 RID: 63187 RVA: 0x00349D1E File Offset: 0x00347F1E
		public bool Equals(sharedParsedNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B64 RID: 23396
		private ProgramNode _node;
	}
}
