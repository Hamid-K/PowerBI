using System;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009A0 RID: 2464
	internal sealed class AxisMdxExpression : MdxExpression
	{
		// Token: 0x06004678 RID: 18040 RVA: 0x000EC78A File Offset: 0x000EA98A
		public AxisMdxExpression(MdxExpression set, params string[] properties)
			: this(false, set, properties)
		{
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x000EC795 File Offset: 0x000EA995
		public AxisMdxExpression(bool nonEmpty, MdxExpression set, params string[] properties)
		{
			this.nonEmpty = nonEmpty;
			this.set = set;
			this.properties = properties;
		}

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x0600467A RID: 18042 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsComplex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x0600467B RID: 18043 RVA: 0x000EC7B2 File Offset: 0x000EA9B2
		public bool NonEmpty
		{
			get
			{
				return this.nonEmpty;
			}
		}

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x0600467C RID: 18044 RVA: 0x000EC7BA File Offset: 0x000EA9BA
		public MdxExpression Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x0600467D RID: 18045 RVA: 0x000EC7C2 File Offset: 0x000EA9C2
		public string[] Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x000EC7CC File Offset: 0x000EA9CC
		public override void Write(MdxExpressionWriter writer)
		{
			if (this.NonEmpty)
			{
				writer.Write("NON EMPTY");
			}
			if (this.Set is IdentifierMdxExpression)
			{
				writer.Write(" {");
				this.Set.Write(writer);
				writer.Write("}");
			}
			else
			{
				this.Set.Write(writer);
			}
			if (this.Properties.Length != 0)
			{
				writer.WriteLine();
				writer.Write("PROPERTIES");
				string text = string.Empty;
				foreach (string text2 in this.Properties)
				{
					writer.Write(text);
					writer.Write(text2);
					text = ", ";
				}
			}
		}

		// Token: 0x04002543 RID: 9539
		private readonly bool nonEmpty;

		// Token: 0x04002544 RID: 9540
		private readonly MdxExpression set;

		// Token: 0x04002545 RID: 9541
		private readonly string[] properties;
	}
}
