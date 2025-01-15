using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000BFF RID: 3071
	public struct tableBounds : IProgramNodeBuilder, IEquatable<tableBounds>
	{
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06004F03 RID: 20227 RVA: 0x000F98EA File Offset: 0x000F7AEA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004F04 RID: 20228 RVA: 0x000F98F2 File Offset: 0x000F7AF2
		private tableBounds(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x000F98FB File Offset: 0x000F7AFB
		public static tableBounds CreateUnsafe(ProgramNode node)
		{
			return new tableBounds(node);
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x000F9904 File Offset: 0x000F7B04
		public static tableBounds? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.tableBounds)
			{
				return null;
			}
			return new tableBounds?(tableBounds.CreateUnsafe(node));
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x000F993E File Offset: 0x000F7B3E
		public static tableBounds CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new tableBounds(new Hole(g.Symbol.tableBounds, holeId));
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x000F9956 File Offset: 0x000F7B56
		public bool Is_SnapToGlyphs(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SnapToGlyphs;
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x000F9970 File Offset: 0x000F7B70
		public bool Is_SnapToGlyphs(GrammarBuilders g, out SnapToGlyphs value)
		{
			if (this.Node.GrammarRule == g.Rule.SnapToGlyphs)
			{
				value = SnapToGlyphs.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SnapToGlyphs);
			return false;
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x000F99A8 File Offset: 0x000F7BA8
		public SnapToGlyphs? As_SnapToGlyphs(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SnapToGlyphs)
			{
				return null;
			}
			return new SnapToGlyphs?(SnapToGlyphs.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x000F99E8 File Offset: 0x000F7BE8
		public SnapToGlyphs Cast_SnapToGlyphs(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SnapToGlyphs)
			{
				return SnapToGlyphs.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SnapToGlyphs is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x000F9A3D File Offset: 0x000F7C3D
		public bool Is_tableBounds_expandedBounds(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.tableBounds_expandedBounds;
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x000F9A57 File Offset: 0x000F7C57
		public bool Is_tableBounds_expandedBounds(GrammarBuilders g, out tableBounds_expandedBounds value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.tableBounds_expandedBounds)
			{
				value = tableBounds_expandedBounds.CreateUnsafe(this.Node);
				return true;
			}
			value = default(tableBounds_expandedBounds);
			return false;
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x000F9A8C File Offset: 0x000F7C8C
		public tableBounds_expandedBounds? As_tableBounds_expandedBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.tableBounds_expandedBounds)
			{
				return null;
			}
			return new tableBounds_expandedBounds?(tableBounds_expandedBounds.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x000F9ACC File Offset: 0x000F7CCC
		public tableBounds_expandedBounds Cast_tableBounds_expandedBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.tableBounds_expandedBounds)
			{
				return tableBounds_expandedBounds.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_tableBounds_expandedBounds is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x000F9B24 File Offset: 0x000F7D24
		public T Switch<T>(GrammarBuilders g, Func<SnapToGlyphs, T> func0, Func<tableBounds_expandedBounds, T> func1)
		{
			SnapToGlyphs snapToGlyphs;
			if (this.Is_SnapToGlyphs(g, out snapToGlyphs))
			{
				return func0(snapToGlyphs);
			}
			tableBounds_expandedBounds tableBounds_expandedBounds;
			if (this.Is_tableBounds_expandedBounds(g, out tableBounds_expandedBounds))
			{
				return func1(tableBounds_expandedBounds);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol tableBounds");
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x000F9B7C File Offset: 0x000F7D7C
		public void Switch(GrammarBuilders g, Action<SnapToGlyphs> func0, Action<tableBounds_expandedBounds> func1)
		{
			SnapToGlyphs snapToGlyphs;
			if (this.Is_SnapToGlyphs(g, out snapToGlyphs))
			{
				func0(snapToGlyphs);
				return;
			}
			tableBounds_expandedBounds tableBounds_expandedBounds;
			if (this.Is_tableBounds_expandedBounds(g, out tableBounds_expandedBounds))
			{
				func1(tableBounds_expandedBounds);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol tableBounds");
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x000F9BD3 File Offset: 0x000F7DD3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x000F9BE8 File Offset: 0x000F7DE8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x000F9C12 File Offset: 0x000F7E12
		public bool Equals(tableBounds other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002327 RID: 8999
		private ProgramNode _node;
	}
}
