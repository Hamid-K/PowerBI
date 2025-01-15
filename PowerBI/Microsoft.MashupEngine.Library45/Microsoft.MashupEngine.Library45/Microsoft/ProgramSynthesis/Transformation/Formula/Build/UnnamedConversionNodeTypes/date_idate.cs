using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200153C RID: 5436
	public struct date_idate : IProgramNodeBuilder, IEquatable<date_idate>
	{
		// Token: 0x17001EBE RID: 7870
		// (get) Token: 0x0600B14F RID: 45391 RVA: 0x002704DA File Offset: 0x0026E6DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B150 RID: 45392 RVA: 0x002704E2 File Offset: 0x0026E6E2
		private date_idate(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B151 RID: 45393 RVA: 0x002704EB File Offset: 0x0026E6EB
		public static date_idate CreateUnsafe(ProgramNode node)
		{
			return new date_idate(node);
		}

		// Token: 0x0600B152 RID: 45394 RVA: 0x002704F4 File Offset: 0x0026E6F4
		public static date_idate? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.date_idate)
			{
				return null;
			}
			return new date_idate?(date_idate.CreateUnsafe(node));
		}

		// Token: 0x0600B153 RID: 45395 RVA: 0x00270529 File Offset: 0x0026E729
		public date_idate(GrammarBuilders g, idate value0)
		{
			this._node = g.UnnamedConversion.date_idate.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B154 RID: 45396 RVA: 0x00270548 File Offset: 0x0026E748
		public static implicit operator date(date_idate arg)
		{
			return date.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EBF RID: 7871
		// (get) Token: 0x0600B155 RID: 45397 RVA: 0x00270556 File Offset: 0x0026E756
		public idate idate
		{
			get
			{
				return idate.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B156 RID: 45398 RVA: 0x0027056A File Offset: 0x0026E76A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B157 RID: 45399 RVA: 0x00270580 File Offset: 0x0026E780
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B158 RID: 45400 RVA: 0x002705AA File Offset: 0x0026E7AA
		public bool Equals(date_idate other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045EA RID: 17898
		private ProgramNode _node;
	}
}
