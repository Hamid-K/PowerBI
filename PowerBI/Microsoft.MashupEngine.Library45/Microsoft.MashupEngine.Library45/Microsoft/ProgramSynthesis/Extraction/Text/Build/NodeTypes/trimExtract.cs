using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F3A RID: 3898
	public struct trimExtract : IProgramNodeBuilder, IEquatable<trimExtract>
	{
		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06006C2B RID: 27691 RVA: 0x00162322 File Offset: 0x00160522
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006C2C RID: 27692 RVA: 0x0016232A File Offset: 0x0016052A
		private trimExtract(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006C2D RID: 27693 RVA: 0x00162333 File Offset: 0x00160533
		public static trimExtract CreateUnsafe(ProgramNode node)
		{
			return new trimExtract(node);
		}

		// Token: 0x06006C2E RID: 27694 RVA: 0x0016233C File Offset: 0x0016053C
		public static trimExtract? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.trimExtract)
			{
				return null;
			}
			return new trimExtract?(trimExtract.CreateUnsafe(node));
		}

		// Token: 0x06006C2F RID: 27695 RVA: 0x00162376 File Offset: 0x00160576
		public static trimExtract CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new trimExtract(new Hole(g.Symbol.trimExtract, holeId));
		}

		// Token: 0x06006C30 RID: 27696 RVA: 0x0016238E File Offset: 0x0016058E
		public bool Is_trimExtract_extract(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.trimExtract_extract;
		}

		// Token: 0x06006C31 RID: 27697 RVA: 0x001623A8 File Offset: 0x001605A8
		public bool Is_trimExtract_extract(GrammarBuilders g, out trimExtract_extract value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.trimExtract_extract)
			{
				value = trimExtract_extract.CreateUnsafe(this.Node);
				return true;
			}
			value = default(trimExtract_extract);
			return false;
		}

		// Token: 0x06006C32 RID: 27698 RVA: 0x001623E0 File Offset: 0x001605E0
		public trimExtract_extract? As_trimExtract_extract(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.trimExtract_extract)
			{
				return null;
			}
			return new trimExtract_extract?(trimExtract_extract.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C33 RID: 27699 RVA: 0x00162420 File Offset: 0x00160620
		public trimExtract_extract Cast_trimExtract_extract(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.trimExtract_extract)
			{
				return trimExtract_extract.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_trimExtract_extract is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C34 RID: 27700 RVA: 0x00162475 File Offset: 0x00160675
		public bool Is_Trim(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Trim;
		}

		// Token: 0x06006C35 RID: 27701 RVA: 0x0016248F File Offset: 0x0016068F
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

		// Token: 0x06006C36 RID: 27702 RVA: 0x001624C4 File Offset: 0x001606C4
		public Trim? As_Trim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Trim)
			{
				return null;
			}
			return new Trim?(Trim.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C37 RID: 27703 RVA: 0x00162504 File Offset: 0x00160704
		public Trim Cast_Trim(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Trim)
			{
				return Trim.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Trim is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C38 RID: 27704 RVA: 0x0016255C File Offset: 0x0016075C
		public T Switch<T>(GrammarBuilders g, Func<trimExtract_extract, T> func0, Func<Trim, T> func1)
		{
			trimExtract_extract trimExtract_extract;
			if (this.Is_trimExtract_extract(g, out trimExtract_extract))
			{
				return func0(trimExtract_extract);
			}
			Trim trim;
			if (this.Is_Trim(g, out trim))
			{
				return func1(trim);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol trimExtract");
		}

		// Token: 0x06006C39 RID: 27705 RVA: 0x001625B4 File Offset: 0x001607B4
		public void Switch(GrammarBuilders g, Action<trimExtract_extract> func0, Action<Trim> func1)
		{
			trimExtract_extract trimExtract_extract;
			if (this.Is_trimExtract_extract(g, out trimExtract_extract))
			{
				func0(trimExtract_extract);
				return;
			}
			Trim trim;
			if (this.Is_Trim(g, out trim))
			{
				func1(trim);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol trimExtract");
		}

		// Token: 0x06006C3A RID: 27706 RVA: 0x0016260B File Offset: 0x0016080B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006C3B RID: 27707 RVA: 0x00162620 File Offset: 0x00160820
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006C3C RID: 27708 RVA: 0x0016264A File Offset: 0x0016084A
		public bool Equals(trimExtract other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F25 RID: 12069
		private ProgramNode _node;
	}
}
