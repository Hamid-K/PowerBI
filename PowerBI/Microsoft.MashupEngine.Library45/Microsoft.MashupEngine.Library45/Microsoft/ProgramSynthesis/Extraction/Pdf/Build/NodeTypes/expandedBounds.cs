using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C01 RID: 3073
	public struct expandedBounds : IProgramNodeBuilder, IEquatable<expandedBounds>
	{
		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06004F2F RID: 20271 RVA: 0x000FA17A File Offset: 0x000F837A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x000FA182 File Offset: 0x000F8382
		private expandedBounds(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x000FA18B File Offset: 0x000F838B
		public static expandedBounds CreateUnsafe(ProgramNode node)
		{
			return new expandedBounds(node);
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x000FA194 File Offset: 0x000F8394
		public static expandedBounds? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.expandedBounds)
			{
				return null;
			}
			return new expandedBounds?(expandedBounds.CreateUnsafe(node));
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x000FA1CE File Offset: 0x000F83CE
		public static expandedBounds CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new expandedBounds(new Hole(g.Symbol.expandedBounds, holeId));
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x000FA1E6 File Offset: 0x000F83E6
		public bool Is_expandedBounds_selectedBounds(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.expandedBounds_selectedBounds;
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x000FA200 File Offset: 0x000F8400
		public bool Is_expandedBounds_selectedBounds(GrammarBuilders g, out expandedBounds_selectedBounds value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.expandedBounds_selectedBounds)
			{
				value = expandedBounds_selectedBounds.CreateUnsafe(this.Node);
				return true;
			}
			value = default(expandedBounds_selectedBounds);
			return false;
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x000FA238 File Offset: 0x000F8438
		public expandedBounds_selectedBounds? As_expandedBounds_selectedBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.expandedBounds_selectedBounds)
			{
				return null;
			}
			return new expandedBounds_selectedBounds?(expandedBounds_selectedBounds.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x000FA278 File Offset: 0x000F8478
		public expandedBounds_selectedBounds Cast_expandedBounds_selectedBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.expandedBounds_selectedBounds)
			{
				return expandedBounds_selectedBounds.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_expandedBounds_selectedBounds is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x000FA2CD File Offset: 0x000F84CD
		public bool Is_CombineBounds(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.CombineBounds;
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x000FA2E7 File Offset: 0x000F84E7
		public bool Is_CombineBounds(GrammarBuilders g, out CombineBounds value)
		{
			if (this.Node.GrammarRule == g.Rule.CombineBounds)
			{
				value = CombineBounds.CreateUnsafe(this.Node);
				return true;
			}
			value = default(CombineBounds);
			return false;
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x000FA31C File Offset: 0x000F851C
		public CombineBounds? As_CombineBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.CombineBounds)
			{
				return null;
			}
			return new CombineBounds?(CombineBounds.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x000FA35C File Offset: 0x000F855C
		public CombineBounds Cast_CombineBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.CombineBounds)
			{
				return CombineBounds.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_CombineBounds is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F3C RID: 20284 RVA: 0x000FA3B4 File Offset: 0x000F85B4
		public T Switch<T>(GrammarBuilders g, Func<expandedBounds_selectedBounds, T> func0, Func<CombineBounds, T> func1)
		{
			expandedBounds_selectedBounds expandedBounds_selectedBounds;
			if (this.Is_expandedBounds_selectedBounds(g, out expandedBounds_selectedBounds))
			{
				return func0(expandedBounds_selectedBounds);
			}
			CombineBounds combineBounds;
			if (this.Is_CombineBounds(g, out combineBounds))
			{
				return func1(combineBounds);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol expandedBounds");
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x000FA40C File Offset: 0x000F860C
		public void Switch(GrammarBuilders g, Action<expandedBounds_selectedBounds> func0, Action<CombineBounds> func1)
		{
			expandedBounds_selectedBounds expandedBounds_selectedBounds;
			if (this.Is_expandedBounds_selectedBounds(g, out expandedBounds_selectedBounds))
			{
				func0(expandedBounds_selectedBounds);
				return;
			}
			CombineBounds combineBounds;
			if (this.Is_CombineBounds(g, out combineBounds))
			{
				func1(combineBounds);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol expandedBounds");
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x000FA463 File Offset: 0x000F8663
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x000FA478 File Offset: 0x000F8678
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x000FA4A2 File Offset: 0x000F86A2
		public bool Equals(expandedBounds other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002329 RID: 9001
		private ProgramNode _node;
	}
}
