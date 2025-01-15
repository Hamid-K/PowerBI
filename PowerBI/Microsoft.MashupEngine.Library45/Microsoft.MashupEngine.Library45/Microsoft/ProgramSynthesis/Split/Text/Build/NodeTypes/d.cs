using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001366 RID: 4966
	public struct d : IProgramNodeBuilder, IEquatable<d>
	{
		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x0600999F RID: 39327 RVA: 0x002091C6 File Offset: 0x002073C6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060099A0 RID: 39328 RVA: 0x002091CE File Offset: 0x002073CE
		private d(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060099A1 RID: 39329 RVA: 0x002091D7 File Offset: 0x002073D7
		public static d CreateUnsafe(ProgramNode node)
		{
			return new d(node);
		}

		// Token: 0x060099A2 RID: 39330 RVA: 0x002091E0 File Offset: 0x002073E0
		public static d? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.d)
			{
				return null;
			}
			return new d?(d.CreateUnsafe(node));
		}

		// Token: 0x060099A3 RID: 39331 RVA: 0x0020921A File Offset: 0x0020741A
		public static d CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new d(new Hole(g.Symbol.d, holeId));
		}

		// Token: 0x060099A4 RID: 39332 RVA: 0x00209232 File Offset: 0x00207432
		public bool Is_LookAround(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LookAround;
		}

		// Token: 0x060099A5 RID: 39333 RVA: 0x0020924C File Offset: 0x0020744C
		public bool Is_LookAround(GrammarBuilders g, out LookAround value)
		{
			if (this.Node.GrammarRule == g.Rule.LookAround)
			{
				value = LookAround.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LookAround);
			return false;
		}

		// Token: 0x060099A6 RID: 39334 RVA: 0x00209284 File Offset: 0x00207484
		public LookAround? As_LookAround(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LookAround)
			{
				return null;
			}
			return new LookAround?(LookAround.CreateUnsafe(this.Node));
		}

		// Token: 0x060099A7 RID: 39335 RVA: 0x002092C4 File Offset: 0x002074C4
		public LookAround Cast_LookAround(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LookAround)
			{
				return LookAround.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LookAround is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060099A8 RID: 39336 RVA: 0x00209319 File Offset: 0x00207519
		public bool Is_FieldEndPoints(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FieldEndPoints;
		}

		// Token: 0x060099A9 RID: 39337 RVA: 0x00209333 File Offset: 0x00207533
		public bool Is_FieldEndPoints(GrammarBuilders g, out FieldEndPoints value)
		{
			if (this.Node.GrammarRule == g.Rule.FieldEndPoints)
			{
				value = FieldEndPoints.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FieldEndPoints);
			return false;
		}

		// Token: 0x060099AA RID: 39338 RVA: 0x00209368 File Offset: 0x00207568
		public FieldEndPoints? As_FieldEndPoints(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FieldEndPoints)
			{
				return null;
			}
			return new FieldEndPoints?(FieldEndPoints.CreateUnsafe(this.Node));
		}

		// Token: 0x060099AB RID: 39339 RVA: 0x002093A8 File Offset: 0x002075A8
		public FieldEndPoints Cast_FieldEndPoints(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FieldEndPoints)
			{
				return FieldEndPoints.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FieldEndPoints is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060099AC RID: 39340 RVA: 0x002093FD File Offset: 0x002075FD
		public bool Is_FieldLookAroundEndPoints(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FieldLookAroundEndPoints;
		}

		// Token: 0x060099AD RID: 39341 RVA: 0x00209417 File Offset: 0x00207617
		public bool Is_FieldLookAroundEndPoints(GrammarBuilders g, out FieldLookAroundEndPoints value)
		{
			if (this.Node.GrammarRule == g.Rule.FieldLookAroundEndPoints)
			{
				value = FieldLookAroundEndPoints.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FieldLookAroundEndPoints);
			return false;
		}

		// Token: 0x060099AE RID: 39342 RVA: 0x0020944C File Offset: 0x0020764C
		public FieldLookAroundEndPoints? As_FieldLookAroundEndPoints(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FieldLookAroundEndPoints)
			{
				return null;
			}
			return new FieldLookAroundEndPoints?(FieldLookAroundEndPoints.CreateUnsafe(this.Node));
		}

		// Token: 0x060099AF RID: 39343 RVA: 0x0020948C File Offset: 0x0020768C
		public FieldLookAroundEndPoints Cast_FieldLookAroundEndPoints(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FieldLookAroundEndPoints)
			{
				return FieldLookAroundEndPoints.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FieldLookAroundEndPoints is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060099B0 RID: 39344 RVA: 0x002094E4 File Offset: 0x002076E4
		public T Switch<T>(GrammarBuilders g, Func<LookAround, T> func0, Func<FieldEndPoints, T> func1, Func<FieldLookAroundEndPoints, T> func2)
		{
			LookAround lookAround;
			if (this.Is_LookAround(g, out lookAround))
			{
				return func0(lookAround);
			}
			FieldEndPoints fieldEndPoints;
			if (this.Is_FieldEndPoints(g, out fieldEndPoints))
			{
				return func1(fieldEndPoints);
			}
			FieldLookAroundEndPoints fieldLookAroundEndPoints;
			if (this.Is_FieldLookAroundEndPoints(g, out fieldLookAroundEndPoints))
			{
				return func2(fieldLookAroundEndPoints);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol d");
		}

		// Token: 0x060099B1 RID: 39345 RVA: 0x00209550 File Offset: 0x00207750
		public void Switch(GrammarBuilders g, Action<LookAround> func0, Action<FieldEndPoints> func1, Action<FieldLookAroundEndPoints> func2)
		{
			LookAround lookAround;
			if (this.Is_LookAround(g, out lookAround))
			{
				func0(lookAround);
				return;
			}
			FieldEndPoints fieldEndPoints;
			if (this.Is_FieldEndPoints(g, out fieldEndPoints))
			{
				func1(fieldEndPoints);
				return;
			}
			FieldLookAroundEndPoints fieldLookAroundEndPoints;
			if (this.Is_FieldLookAroundEndPoints(g, out fieldLookAroundEndPoints))
			{
				func2(fieldLookAroundEndPoints);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol d");
		}

		// Token: 0x060099B2 RID: 39346 RVA: 0x002095BB File Offset: 0x002077BB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060099B3 RID: 39347 RVA: 0x002095D0 File Offset: 0x002077D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060099B4 RID: 39348 RVA: 0x002095FA File Offset: 0x002077FA
		public bool Equals(d other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DDD RID: 15837
		private ProgramNode _node;
	}
}
