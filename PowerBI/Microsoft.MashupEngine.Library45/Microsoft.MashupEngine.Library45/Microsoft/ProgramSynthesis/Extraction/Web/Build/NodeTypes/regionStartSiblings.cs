using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001068 RID: 4200
	public struct regionStartSiblings : IProgramNodeBuilder, IEquatable<regionStartSiblings>
	{
		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06007D1D RID: 32029 RVA: 0x001A644E File Offset: 0x001A464E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007D1E RID: 32030 RVA: 0x001A6456 File Offset: 0x001A4656
		private regionStartSiblings(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007D1F RID: 32031 RVA: 0x001A645F File Offset: 0x001A465F
		public static regionStartSiblings CreateUnsafe(ProgramNode node)
		{
			return new regionStartSiblings(node);
		}

		// Token: 0x06007D20 RID: 32032 RVA: 0x001A6468 File Offset: 0x001A4668
		public static regionStartSiblings? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.regionStartSiblings)
			{
				return null;
			}
			return new regionStartSiblings?(regionStartSiblings.CreateUnsafe(node));
		}

		// Token: 0x06007D21 RID: 32033 RVA: 0x001A64A2 File Offset: 0x001A46A2
		public static regionStartSiblings CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new regionStartSiblings(new Hole(g.Symbol.regionStartSiblings, holeId));
		}

		// Token: 0x06007D22 RID: 32034 RVA: 0x001A64BA File Offset: 0x001A46BA
		public YoungerSiblingsOf Cast_YoungerSiblingsOf()
		{
			return YoungerSiblingsOf.CreateUnsafe(this.Node);
		}

		// Token: 0x06007D23 RID: 32035 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_YoungerSiblingsOf(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007D24 RID: 32036 RVA: 0x001A64C7 File Offset: 0x001A46C7
		public bool Is_YoungerSiblingsOf(GrammarBuilders g, out YoungerSiblingsOf value)
		{
			value = YoungerSiblingsOf.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007D25 RID: 32037 RVA: 0x001A64DB File Offset: 0x001A46DB
		public YoungerSiblingsOf? As_YoungerSiblingsOf(GrammarBuilders g)
		{
			return new YoungerSiblingsOf?(YoungerSiblingsOf.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D26 RID: 32038 RVA: 0x001A64ED File Offset: 0x001A46ED
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007D27 RID: 32039 RVA: 0x001A6500 File Offset: 0x001A4700
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007D28 RID: 32040 RVA: 0x001A652A File Offset: 0x001A472A
		public bool Equals(regionStartSiblings other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003381 RID: 13185
		private ProgramNode _node;
	}
}
