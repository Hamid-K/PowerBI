using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A48 RID: 6728
	public struct _LetB0 : IProgramNodeBuilder, IEquatable<_LetB0>
	{
		// Token: 0x17002517 RID: 9495
		// (get) Token: 0x0600DDBA RID: 56762 RVA: 0x002F2046 File Offset: 0x002F0246
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DDBB RID: 56763 RVA: 0x002F204E File Offset: 0x002F024E
		private _LetB0(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DDBC RID: 56764 RVA: 0x002F2057 File Offset: 0x002F0257
		public static _LetB0 CreateUnsafe(ProgramNode node)
		{
			return new _LetB0(node);
		}

		// Token: 0x0600DDBD RID: 56765 RVA: 0x002F2060 File Offset: 0x002F0260
		public static _LetB0? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB0)
			{
				return null;
			}
			return new _LetB0?(_LetB0.CreateUnsafe(node));
		}

		// Token: 0x0600DDBE RID: 56766 RVA: 0x002F209A File Offset: 0x002F029A
		public static _LetB0 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB0(new Hole(g.Symbol._LetB0, holeId));
		}

		// Token: 0x0600DDBF RID: 56767 RVA: 0x002F20B2 File Offset: 0x002F02B2
		public SelectStringValues Cast_SelectStringValues()
		{
			return SelectStringValues.CreateUnsafe(this.Node);
		}

		// Token: 0x0600DDC0 RID: 56768 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SelectStringValues(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600DDC1 RID: 56769 RVA: 0x002F20BF File Offset: 0x002F02BF
		public bool Is_SelectStringValues(GrammarBuilders g, out SelectStringValues value)
		{
			value = SelectStringValues.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600DDC2 RID: 56770 RVA: 0x002F20D3 File Offset: 0x002F02D3
		public SelectStringValues? As_SelectStringValues(GrammarBuilders g)
		{
			return new SelectStringValues?(SelectStringValues.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DDC3 RID: 56771 RVA: 0x002F20E5 File Offset: 0x002F02E5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DDC4 RID: 56772 RVA: 0x002F20F8 File Offset: 0x002F02F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DDC5 RID: 56773 RVA: 0x002F2122 File Offset: 0x002F0322
		public bool Equals(_LetB0 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005439 RID: 21561
		private ProgramNode _node;
	}
}
