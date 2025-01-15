using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C4E RID: 7246
	public struct regexPair : IProgramNodeBuilder, IEquatable<regexPair>
	{
		// Token: 0x170028E4 RID: 10468
		// (get) Token: 0x0600F50C RID: 62732 RVA: 0x00346E92 File Offset: 0x00345092
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F50D RID: 62733 RVA: 0x00346E9A File Offset: 0x0034509A
		private regexPair(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F50E RID: 62734 RVA: 0x00346EA3 File Offset: 0x003450A3
		public static regexPair CreateUnsafe(ProgramNode node)
		{
			return new regexPair(node);
		}

		// Token: 0x0600F50F RID: 62735 RVA: 0x00346EAC File Offset: 0x003450AC
		public static regexPair? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.regexPair)
			{
				return null;
			}
			return new regexPair?(regexPair.CreateUnsafe(node));
		}

		// Token: 0x0600F510 RID: 62736 RVA: 0x00346EE6 File Offset: 0x003450E6
		public static regexPair CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new regexPair(new Hole(g.Symbol.regexPair, holeId));
		}

		// Token: 0x0600F511 RID: 62737 RVA: 0x00346EFE File Offset: 0x003450FE
		public RegexPair Cast_RegexPair()
		{
			return RegexPair.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F512 RID: 62738 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_RegexPair(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F513 RID: 62739 RVA: 0x00346F0B File Offset: 0x0034510B
		public bool Is_RegexPair(GrammarBuilders g, out RegexPair value)
		{
			value = RegexPair.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F514 RID: 62740 RVA: 0x00346F1F File Offset: 0x0034511F
		public RegexPair? As_RegexPair(GrammarBuilders g)
		{
			return new RegexPair?(RegexPair.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F515 RID: 62741 RVA: 0x00346F31 File Offset: 0x00345131
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F516 RID: 62742 RVA: 0x00346F44 File Offset: 0x00345144
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F517 RID: 62743 RVA: 0x00346F6E File Offset: 0x0034516E
		public bool Equals(regexPair other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B3D RID: 23357
		private ProgramNode _node;
	}
}
