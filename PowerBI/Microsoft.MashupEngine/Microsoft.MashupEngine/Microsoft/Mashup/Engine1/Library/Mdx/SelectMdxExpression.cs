using System;
using System.Globalization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009A1 RID: 2465
	internal sealed class SelectMdxExpression : MdxExpression
	{
		// Token: 0x0600467F RID: 18047 RVA: 0x000EC876 File Offset: 0x000EAA76
		public SelectMdxExpression(MdxDeclaration[] declarations, AxisMdxExpression[] axes, MdxExpression from, MdxExpression where = null, string[] cellProperties = null)
		{
			this.declarations = declarations;
			this.axes = axes;
			this.from = from;
			this.where = where;
			this.cellProperties = cellProperties ?? EmptyArray<string>.Instance;
		}

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06004680 RID: 18048 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsComplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x000EC8AC File Offset: 0x000EAAAC
		public MdxDeclaration[] Declarations
		{
			get
			{
				return this.declarations;
			}
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06004682 RID: 18050 RVA: 0x000EC8B4 File Offset: 0x000EAAB4
		public AxisMdxExpression[] Axes
		{
			get
			{
				return this.axes;
			}
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06004683 RID: 18051 RVA: 0x000EC8BC File Offset: 0x000EAABC
		public MdxExpression From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06004684 RID: 18052 RVA: 0x000EC8C4 File Offset: 0x000EAAC4
		public MdxExpression Where
		{
			get
			{
				return this.where;
			}
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06004685 RID: 18053 RVA: 0x000EC8CC File Offset: 0x000EAACC
		public string[] CellProperties
		{
			get
			{
				return this.cellProperties;
			}
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x000EC8D4 File Offset: 0x000EAAD4
		public override void Write(MdxExpressionWriter writer)
		{
			if (this.Declarations.Length != 0)
			{
				writer.Write("WITH");
				MdxDeclaration[] array = this.Declarations;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Write(writer);
				}
			}
			writer.Write("SELECT");
			for (int j = 0; j < this.Axes.Length; j++)
			{
				using (writer.NewScope())
				{
					this.Axes[j].Write(writer);
					writer.Write("ON");
					writer.Write(j.ToString(CultureInfo.InvariantCulture));
					if (j < this.Axes.Length - 1)
					{
						writer.Write(",");
					}
				}
			}
			writer.Write("FROM");
			if (this.From is IdentifierMdxExpression)
			{
				this.From.Write(writer);
			}
			else
			{
				writer.Write("(");
				using (writer.NewScope())
				{
					this.From.Write(writer);
				}
				writer.Write(")");
			}
			if (this.Where != null)
			{
				writer.Write("WHERE");
				using (writer.NewScope())
				{
					if (this.Where is IdentifierMdxExpression)
					{
						writer.Write("(");
						this.Where.Write(writer);
						writer.Write(")");
					}
					else
					{
						this.Where.Write(writer);
					}
				}
			}
			for (int k = 0; k < this.CellProperties.Length; k++)
			{
				if (k == 0)
				{
					writer.Write("CELL PROPERTIES");
				}
				else
				{
					writer.Write(", ");
				}
				writer.Write(this.CellProperties[k]);
			}
		}

		// Token: 0x04002546 RID: 9542
		private readonly MdxDeclaration[] declarations;

		// Token: 0x04002547 RID: 9543
		private readonly AxisMdxExpression[] axes;

		// Token: 0x04002548 RID: 9544
		private readonly MdxExpression from;

		// Token: 0x04002549 RID: 9545
		private readonly MdxExpression where;

		// Token: 0x0400254A RID: 9546
		private readonly string[] cellProperties;
	}
}
