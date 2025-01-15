using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200108B RID: 4235
	public struct _LetB0 : IProgramNodeBuilder, IEquatable<_LetB0>
	{
		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06007F73 RID: 32627 RVA: 0x001AC02A File Offset: 0x001AA22A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007F74 RID: 32628 RVA: 0x001AC032 File Offset: 0x001AA232
		private _LetB0(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007F75 RID: 32629 RVA: 0x001AC03B File Offset: 0x001AA23B
		public static _LetB0 CreateUnsafe(ProgramNode node)
		{
			return new _LetB0(node);
		}

		// Token: 0x06007F76 RID: 32630 RVA: 0x001AC044 File Offset: 0x001AA244
		public static _LetB0? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB0)
			{
				return null;
			}
			return new _LetB0?(_LetB0.CreateUnsafe(node));
		}

		// Token: 0x06007F77 RID: 32631 RVA: 0x001AC07E File Offset: 0x001AA27E
		public static _LetB0 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB0(new Hole(g.Symbol._LetB0, holeId));
		}

		// Token: 0x06007F78 RID: 32632 RVA: 0x001AC096 File Offset: 0x001AA296
		public NodeRegionToWebRegion Cast_NodeRegionToWebRegion()
		{
			return NodeRegionToWebRegion.CreateUnsafe(this.Node);
		}

		// Token: 0x06007F79 RID: 32633 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_NodeRegionToWebRegion(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007F7A RID: 32634 RVA: 0x001AC0A3 File Offset: 0x001AA2A3
		public bool Is_NodeRegionToWebRegion(GrammarBuilders g, out NodeRegionToWebRegion value)
		{
			value = NodeRegionToWebRegion.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007F7B RID: 32635 RVA: 0x001AC0B7 File Offset: 0x001AA2B7
		public NodeRegionToWebRegion? As_NodeRegionToWebRegion(GrammarBuilders g)
		{
			return new NodeRegionToWebRegion?(NodeRegionToWebRegion.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F7C RID: 32636 RVA: 0x001AC0C9 File Offset: 0x001AA2C9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007F7D RID: 32637 RVA: 0x001AC0DC File Offset: 0x001AA2DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007F7E RID: 32638 RVA: 0x001AC106 File Offset: 0x001AA306
		public bool Equals(_LetB0 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033A4 RID: 13220
		private ProgramNode _node;
	}
}
