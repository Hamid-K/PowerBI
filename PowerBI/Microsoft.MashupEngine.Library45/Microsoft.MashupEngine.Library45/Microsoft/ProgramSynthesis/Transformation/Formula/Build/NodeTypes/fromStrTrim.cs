using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015C2 RID: 5570
	public struct fromStrTrim : IProgramNodeBuilder, IEquatable<fromStrTrim>
	{
		// Token: 0x17001FE8 RID: 8168
		// (get) Token: 0x0600B890 RID: 47248 RVA: 0x0027FC26 File Offset: 0x0027DE26
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B891 RID: 47249 RVA: 0x0027FC2E File Offset: 0x0027DE2E
		private fromStrTrim(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B892 RID: 47250 RVA: 0x0027FC37 File Offset: 0x0027DE37
		public static fromStrTrim CreateUnsafe(ProgramNode node)
		{
			return new fromStrTrim(node);
		}

		// Token: 0x0600B893 RID: 47251 RVA: 0x0027FC40 File Offset: 0x0027DE40
		public static fromStrTrim? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fromStrTrim)
			{
				return null;
			}
			return new fromStrTrim?(fromStrTrim.CreateUnsafe(node));
		}

		// Token: 0x0600B894 RID: 47252 RVA: 0x0027FC7A File Offset: 0x0027DE7A
		public static fromStrTrim CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fromStrTrim(new Hole(g.Symbol.fromStrTrim, holeId));
		}

		// Token: 0x0600B895 RID: 47253 RVA: 0x0027FC92 File Offset: 0x0027DE92
		public bool Is_fromStrTrim_fromStr(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.fromStrTrim_fromStr;
		}

		// Token: 0x0600B896 RID: 47254 RVA: 0x0027FCAC File Offset: 0x0027DEAC
		public bool Is_fromStrTrim_fromStr(GrammarBuilders g, out fromStrTrim_fromStr value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.fromStrTrim_fromStr)
			{
				value = fromStrTrim_fromStr.CreateUnsafe(this.Node);
				return true;
			}
			value = default(fromStrTrim_fromStr);
			return false;
		}

		// Token: 0x0600B897 RID: 47255 RVA: 0x0027FCE4 File Offset: 0x0027DEE4
		public fromStrTrim_fromStr? As_fromStrTrim_fromStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.fromStrTrim_fromStr)
			{
				return null;
			}
			return new fromStrTrim_fromStr?(fromStrTrim_fromStr.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B898 RID: 47256 RVA: 0x0027FD24 File Offset: 0x0027DF24
		public fromStrTrim_fromStr Cast_fromStrTrim_fromStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.fromStrTrim_fromStr)
			{
				return fromStrTrim_fromStr.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_fromStrTrim_fromStr is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B899 RID: 47257 RVA: 0x0027FD79 File Offset: 0x0027DF79
		public bool Is_fromStrTrim_fromNumberStr(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.fromStrTrim_fromNumberStr;
		}

		// Token: 0x0600B89A RID: 47258 RVA: 0x0027FD93 File Offset: 0x0027DF93
		public bool Is_fromStrTrim_fromNumberStr(GrammarBuilders g, out fromStrTrim_fromNumberStr value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.fromStrTrim_fromNumberStr)
			{
				value = fromStrTrim_fromNumberStr.CreateUnsafe(this.Node);
				return true;
			}
			value = default(fromStrTrim_fromNumberStr);
			return false;
		}

		// Token: 0x0600B89B RID: 47259 RVA: 0x0027FDC8 File Offset: 0x0027DFC8
		public fromStrTrim_fromNumberStr? As_fromStrTrim_fromNumberStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.fromStrTrim_fromNumberStr)
			{
				return null;
			}
			return new fromStrTrim_fromNumberStr?(fromStrTrim_fromNumberStr.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B89C RID: 47260 RVA: 0x0027FE08 File Offset: 0x0027E008
		public fromStrTrim_fromNumberStr Cast_fromStrTrim_fromNumberStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.fromStrTrim_fromNumberStr)
			{
				return fromStrTrim_fromNumberStr.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_fromStrTrim_fromNumberStr is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B89D RID: 47261 RVA: 0x0027FE5D File Offset: 0x0027E05D
		public bool Is_TrimFull(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimFull;
		}

		// Token: 0x0600B89E RID: 47262 RVA: 0x0027FE77 File Offset: 0x0027E077
		public bool Is_TrimFull(GrammarBuilders g, out TrimFull value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimFull)
			{
				value = TrimFull.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimFull);
			return false;
		}

		// Token: 0x0600B89F RID: 47263 RVA: 0x0027FEAC File Offset: 0x0027E0AC
		public TrimFull? As_TrimFull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimFull)
			{
				return null;
			}
			return new TrimFull?(TrimFull.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B8A0 RID: 47264 RVA: 0x0027FEEC File Offset: 0x0027E0EC
		public TrimFull Cast_TrimFull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimFull)
			{
				return TrimFull.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimFull is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B8A1 RID: 47265 RVA: 0x0027FF41 File Offset: 0x0027E141
		public bool Is_Trim(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Trim;
		}

		// Token: 0x0600B8A2 RID: 47266 RVA: 0x0027FF5B File Offset: 0x0027E15B
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

		// Token: 0x0600B8A3 RID: 47267 RVA: 0x0027FF90 File Offset: 0x0027E190
		public Trim? As_Trim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Trim)
			{
				return null;
			}
			return new Trim?(Trim.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B8A4 RID: 47268 RVA: 0x0027FFD0 File Offset: 0x0027E1D0
		public Trim Cast_Trim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Trim)
			{
				return Trim.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Trim is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B8A5 RID: 47269 RVA: 0x00280028 File Offset: 0x0027E228
		public T Switch<T>(GrammarBuilders g, Func<fromStrTrim_fromStr, T> func0, Func<fromStrTrim_fromNumberStr, T> func1, Func<TrimFull, T> func2, Func<Trim, T> func3)
		{
			fromStrTrim_fromStr fromStrTrim_fromStr;
			if (this.Is_fromStrTrim_fromStr(g, out fromStrTrim_fromStr))
			{
				return func0(fromStrTrim_fromStr);
			}
			fromStrTrim_fromNumberStr fromStrTrim_fromNumberStr;
			if (this.Is_fromStrTrim_fromNumberStr(g, out fromStrTrim_fromNumberStr))
			{
				return func1(fromStrTrim_fromNumberStr);
			}
			TrimFull trimFull;
			if (this.Is_TrimFull(g, out trimFull))
			{
				return func2(trimFull);
			}
			Trim trim;
			if (this.Is_Trim(g, out trim))
			{
				return func3(trim);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol fromStrTrim");
		}

		// Token: 0x0600B8A6 RID: 47270 RVA: 0x002800A8 File Offset: 0x0027E2A8
		public void Switch(GrammarBuilders g, Action<fromStrTrim_fromStr> func0, Action<fromStrTrim_fromNumberStr> func1, Action<TrimFull> func2, Action<Trim> func3)
		{
			fromStrTrim_fromStr fromStrTrim_fromStr;
			if (this.Is_fromStrTrim_fromStr(g, out fromStrTrim_fromStr))
			{
				func0(fromStrTrim_fromStr);
				return;
			}
			fromStrTrim_fromNumberStr fromStrTrim_fromNumberStr;
			if (this.Is_fromStrTrim_fromNumberStr(g, out fromStrTrim_fromNumberStr))
			{
				func1(fromStrTrim_fromNumberStr);
				return;
			}
			TrimFull trimFull;
			if (this.Is_TrimFull(g, out trimFull))
			{
				func2(trimFull);
				return;
			}
			Trim trim;
			if (this.Is_Trim(g, out trim))
			{
				func3(trim);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol fromStrTrim");
		}

		// Token: 0x0600B8A7 RID: 47271 RVA: 0x00280127 File Offset: 0x0027E327
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B8A8 RID: 47272 RVA: 0x0028013C File Offset: 0x0027E33C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B8A9 RID: 47273 RVA: 0x00280166 File Offset: 0x0027E366
		public bool Equals(fromStrTrim other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004670 RID: 18032
		private ProgramNode _node;
	}
}
