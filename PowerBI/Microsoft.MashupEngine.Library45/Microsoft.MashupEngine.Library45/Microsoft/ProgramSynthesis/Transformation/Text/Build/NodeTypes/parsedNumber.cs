using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C52 RID: 7250
	public struct parsedNumber : IProgramNodeBuilder, IEquatable<parsedNumber>
	{
		// Token: 0x170028E8 RID: 10472
		// (get) Token: 0x0600F548 RID: 62792 RVA: 0x003476EA File Offset: 0x003458EA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F549 RID: 62793 RVA: 0x003476F2 File Offset: 0x003458F2
		private parsedNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F54A RID: 62794 RVA: 0x003476FB File Offset: 0x003458FB
		public static parsedNumber CreateUnsafe(ProgramNode node)
		{
			return new parsedNumber(node);
		}

		// Token: 0x0600F54B RID: 62795 RVA: 0x00347704 File Offset: 0x00345904
		public static parsedNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.parsedNumber)
			{
				return null;
			}
			return new parsedNumber?(parsedNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F54C RID: 62796 RVA: 0x0034773E File Offset: 0x0034593E
		public static parsedNumber CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new parsedNumber(new Hole(g.Symbol.parsedNumber, holeId));
		}

		// Token: 0x0600F54D RID: 62797 RVA: 0x00347756 File Offset: 0x00345956
		public ParseNumber Cast_ParseNumber()
		{
			return ParseNumber.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F54E RID: 62798 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_ParseNumber(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F54F RID: 62799 RVA: 0x00347763 File Offset: 0x00345963
		public bool Is_ParseNumber(GrammarBuilders g, out ParseNumber value)
		{
			value = ParseNumber.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F550 RID: 62800 RVA: 0x00347777 File Offset: 0x00345977
		public ParseNumber? As_ParseNumber(GrammarBuilders g)
		{
			return new ParseNumber?(ParseNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F551 RID: 62801 RVA: 0x00347789 File Offset: 0x00345989
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F552 RID: 62802 RVA: 0x0034779C File Offset: 0x0034599C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F553 RID: 62803 RVA: 0x003477C6 File Offset: 0x003459C6
		public bool Equals(parsedNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B41 RID: 23361
		private ProgramNode _node;
	}
}
