using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C58 RID: 7256
	public struct _LetB2 : IProgramNodeBuilder, IEquatable<_LetB2>
	{
		// Token: 0x170028EE RID: 10478
		// (get) Token: 0x0600F59C RID: 62876 RVA: 0x00348122 File Offset: 0x00346322
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F59D RID: 62877 RVA: 0x0034812A File Offset: 0x0034632A
		private _LetB2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F59E RID: 62878 RVA: 0x00348133 File Offset: 0x00346333
		public static _LetB2 CreateUnsafe(ProgramNode node)
		{
			return new _LetB2(node);
		}

		// Token: 0x0600F59F RID: 62879 RVA: 0x0034813C File Offset: 0x0034633C
		public static _LetB2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB2)
			{
				return null;
			}
			return new _LetB2?(_LetB2.CreateUnsafe(node));
		}

		// Token: 0x0600F5A0 RID: 62880 RVA: 0x00348176 File Offset: 0x00346376
		public static _LetB2 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB2(new Hole(g.Symbol._LetB2, holeId));
		}

		// Token: 0x0600F5A1 RID: 62881 RVA: 0x0034818E File Offset: 0x0034638E
		public Add Cast_Add()
		{
			return Add.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F5A2 RID: 62882 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Add(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F5A3 RID: 62883 RVA: 0x0034819B File Offset: 0x0034639B
		public bool Is_Add(GrammarBuilders g, out Add value)
		{
			value = Add.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F5A4 RID: 62884 RVA: 0x003481AF File Offset: 0x003463AF
		public Add? As_Add(GrammarBuilders g)
		{
			return new Add?(Add.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F5A5 RID: 62885 RVA: 0x003481C1 File Offset: 0x003463C1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F5A6 RID: 62886 RVA: 0x003481D4 File Offset: 0x003463D4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F5A7 RID: 62887 RVA: 0x003481FE File Offset: 0x003463FE
		public bool Equals(_LetB2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B47 RID: 23367
		private ProgramNode _node;
	}
}
