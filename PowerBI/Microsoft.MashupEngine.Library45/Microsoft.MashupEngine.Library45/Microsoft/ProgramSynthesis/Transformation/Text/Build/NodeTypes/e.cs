using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C3B RID: 7227
	public struct e : IProgramNodeBuilder, IEquatable<e>
	{
		// Token: 0x170028D1 RID: 10449
		// (get) Token: 0x0600F39A RID: 62362 RVA: 0x00342CA2 File Offset: 0x00340EA2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F39B RID: 62363 RVA: 0x00342CAA File Offset: 0x00340EAA
		private e(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F39C RID: 62364 RVA: 0x00342CB3 File Offset: 0x00340EB3
		public static e CreateUnsafe(ProgramNode node)
		{
			return new e(node);
		}

		// Token: 0x0600F39D RID: 62365 RVA: 0x00342CBC File Offset: 0x00340EBC
		public static e? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.e)
			{
				return null;
			}
			return new e?(e.CreateUnsafe(node));
		}

		// Token: 0x0600F39E RID: 62366 RVA: 0x00342CF6 File Offset: 0x00340EF6
		public static e CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new e(new Hole(g.Symbol.e, holeId));
		}

		// Token: 0x0600F39F RID: 62367 RVA: 0x00342D0E File Offset: 0x00340F0E
		public bool Is_Atom(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Atom;
		}

		// Token: 0x0600F3A0 RID: 62368 RVA: 0x00342D28 File Offset: 0x00340F28
		public bool Is_Atom(GrammarBuilders g, out Atom value)
		{
			if (this.Node.GrammarRule == g.Rule.Atom)
			{
				value = Atom.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Atom);
			return false;
		}

		// Token: 0x0600F3A1 RID: 62369 RVA: 0x00342D60 File Offset: 0x00340F60
		public Atom? As_Atom(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Atom)
			{
				return null;
			}
			return new Atom?(Atom.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F3A2 RID: 62370 RVA: 0x00342DA0 File Offset: 0x00340FA0
		public Atom Cast_Atom(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Atom)
			{
				return Atom.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Atom is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F3A3 RID: 62371 RVA: 0x00342DF5 File Offset: 0x00340FF5
		public bool Is_Concat(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Concat;
		}

		// Token: 0x0600F3A4 RID: 62372 RVA: 0x00342E0F File Offset: 0x0034100F
		public bool Is_Concat(GrammarBuilders g, out Concat value)
		{
			if (this.Node.GrammarRule == g.Rule.Concat)
			{
				value = Concat.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Concat);
			return false;
		}

		// Token: 0x0600F3A5 RID: 62373 RVA: 0x00342E44 File Offset: 0x00341044
		public Concat? As_Concat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Concat)
			{
				return null;
			}
			return new Concat?(Concat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F3A6 RID: 62374 RVA: 0x00342E84 File Offset: 0x00341084
		public Concat Cast_Concat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Concat)
			{
				return Concat.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Concat is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F3A7 RID: 62375 RVA: 0x00342EDC File Offset: 0x003410DC
		public T Switch<T>(GrammarBuilders g, Func<Atom, T> func0, Func<Concat, T> func1)
		{
			Atom atom;
			if (this.Is_Atom(g, out atom))
			{
				return func0(atom);
			}
			Concat concat;
			if (this.Is_Concat(g, out concat))
			{
				return func1(concat);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol e");
		}

		// Token: 0x0600F3A8 RID: 62376 RVA: 0x00342F34 File Offset: 0x00341134
		public void Switch(GrammarBuilders g, Action<Atom> func0, Action<Concat> func1)
		{
			Atom atom;
			if (this.Is_Atom(g, out atom))
			{
				func0(atom);
				return;
			}
			Concat concat;
			if (this.Is_Concat(g, out concat))
			{
				func1(concat);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol e");
		}

		// Token: 0x0600F3A9 RID: 62377 RVA: 0x00342F8B File Offset: 0x0034118B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F3AA RID: 62378 RVA: 0x00342FA0 File Offset: 0x003411A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F3AB RID: 62379 RVA: 0x00342FCA File Offset: 0x003411CA
		public bool Equals(e other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B2A RID: 23338
		private ProgramNode _node;
	}
}
