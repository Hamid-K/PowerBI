using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E52 RID: 3666
	public struct trim : IProgramNodeBuilder, IEquatable<trim>
	{
		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06006287 RID: 25223 RVA: 0x00140B06 File Offset: 0x0013ED06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006288 RID: 25224 RVA: 0x00140B0E File Offset: 0x0013ED0E
		private trim(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006289 RID: 25225 RVA: 0x00140B17 File Offset: 0x0013ED17
		public static trim CreateUnsafe(ProgramNode node)
		{
			return new trim(node);
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x00140B20 File Offset: 0x0013ED20
		public static trim? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.trim)
			{
				return null;
			}
			return new trim?(trim.CreateUnsafe(node));
		}

		// Token: 0x0600628B RID: 25227 RVA: 0x00140B5A File Offset: 0x0013ED5A
		public static trim CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new trim(new Hole(g.Symbol.trim, holeId));
		}

		// Token: 0x0600628C RID: 25228 RVA: 0x00140B72 File Offset: 0x0013ED72
		public bool Is_Trim(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Trim;
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x00140B8C File Offset: 0x0013ED8C
		public bool Is_Trim(GrammarBuilders g, out Trim value)
		{
			if (this.Node.GrammarRule == g.Rule.Trim)
			{
				value = Trim.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Trim);
			return false;
		}

		// Token: 0x0600628E RID: 25230 RVA: 0x00140BC4 File Offset: 0x0013EDC4
		public Trim? As_Trim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Trim)
			{
				return null;
			}
			return new Trim?(Trim.CreateUnsafe(this.Node));
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x00140C04 File Offset: 0x0013EE04
		public Trim Cast_Trim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Trim)
			{
				return Trim.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Trim is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006290 RID: 25232 RVA: 0x00140C59 File Offset: 0x0013EE59
		public bool Is_TrimHidden(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimHidden;
		}

		// Token: 0x06006291 RID: 25233 RVA: 0x00140C73 File Offset: 0x0013EE73
		public bool Is_TrimHidden(GrammarBuilders g, out TrimHidden value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimHidden)
			{
				value = TrimHidden.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimHidden);
			return false;
		}

		// Token: 0x06006292 RID: 25234 RVA: 0x00140CA8 File Offset: 0x0013EEA8
		public TrimHidden? As_TrimHidden(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimHidden)
			{
				return null;
			}
			return new TrimHidden?(TrimHidden.CreateUnsafe(this.Node));
		}

		// Token: 0x06006293 RID: 25235 RVA: 0x00140CE8 File Offset: 0x0013EEE8
		public TrimHidden Cast_TrimHidden(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimHidden)
			{
				return TrimHidden.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimHidden is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006294 RID: 25236 RVA: 0x00140D40 File Offset: 0x0013EF40
		public T Switch<T>(GrammarBuilders g, Func<Trim, T> func0, Func<TrimHidden, T> func1)
		{
			Trim trim;
			if (this.Is_Trim(g, out trim))
			{
				return func0(trim);
			}
			TrimHidden trimHidden;
			if (this.Is_TrimHidden(g, out trimHidden))
			{
				return func1(trimHidden);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol trim");
		}

		// Token: 0x06006295 RID: 25237 RVA: 0x00140D98 File Offset: 0x0013EF98
		public void Switch(GrammarBuilders g, Action<Trim> func0, Action<TrimHidden> func1)
		{
			Trim trim;
			if (this.Is_Trim(g, out trim))
			{
				func0(trim);
				return;
			}
			TrimHidden trimHidden;
			if (this.Is_TrimHidden(g, out trimHidden))
			{
				func1(trimHidden);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol trim");
		}

		// Token: 0x06006296 RID: 25238 RVA: 0x00140DEF File Offset: 0x0013EFEF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006297 RID: 25239 RVA: 0x00140E04 File Offset: 0x0013F004
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006298 RID: 25240 RVA: 0x00140E2E File Offset: 0x0013F02E
		public bool Equals(trim other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BFC RID: 11260
		private ProgramNode _node;
	}
}
