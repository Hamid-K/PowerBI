using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x020009AB RID: 2475
	internal class ConstantMdxExpression : MdxExpression
	{
		// Token: 0x060046AC RID: 18092 RVA: 0x000ED247 File Offset: 0x000EB447
		public static ConstantMdxExpression New(bool boolean)
		{
			if (!boolean)
			{
				return ConstantMdxExpression.False;
			}
			return ConstantMdxExpression.True;
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x000ED257 File Offset: 0x000EB457
		private ConstantMdxExpression(MdxConstantType type)
		{
			this.type = type;
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x000ED266 File Offset: 0x000EB466
		public ConstantMdxExpression(string value)
		{
			this.type = MdxConstantType.String;
			this.value = value;
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x000ED27C File Offset: 0x000EB47C
		public ConstantMdxExpression(int value)
		{
			this.type = MdxConstantType.Int32;
			this.value = value;
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x000ED297 File Offset: 0x000EB497
		public ConstantMdxExpression(bool boolean)
		{
			this.type = MdxConstantType.Boolean;
			this.value = boolean;
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x000ED2B2 File Offset: 0x000EB4B2
		public ConstantMdxExpression(double dbl)
		{
			this.type = MdxConstantType.Double;
			this.value = dbl;
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x000ED2CD File Offset: 0x000EB4CD
		public ConstantMdxExpression(decimal dec)
		{
			this.type = MdxConstantType.Decimal;
			this.value = dec;
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x060046B3 RID: 18099 RVA: 0x000ED2E8 File Offset: 0x000EB4E8
		public MdxConstantType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x060046B4 RID: 18100 RVA: 0x000ED2F0 File Offset: 0x000EB4F0
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x000ED2F8 File Offset: 0x000EB4F8
		public bool Equals(ConstantMdxExpression other)
		{
			return other != null && this.type == other.type && ((this.value == null && other.value == null) || (this.value != null && this.value.Equals(other.value)));
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x000ED345 File Offset: 0x000EB545
		public override bool Equals(object other)
		{
			return this.Equals(other as ConstantMdxExpression);
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x000ED354 File Offset: 0x000EB554
		public override int GetHashCode()
		{
			return ((this.value == null) ? 0 : this.value.GetHashCode()) + this.type.GetHashCode();
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x000ED38C File Offset: 0x000EB58C
		public override void Write(MdxExpressionWriter writer)
		{
			switch (this.Type)
			{
			case MdxConstantType.Int32:
			case MdxConstantType.Double:
			case MdxConstantType.Decimal:
				writer.Write(string.Format(CultureInfo.InvariantCulture, "{0}", this.Value));
				return;
			case MdxConstantType.String:
			{
				string text = "\"" + ((string)this.Value).Replace("\"", "\"\"") + "\"";
				writer.Write(text);
				return;
			}
			case MdxConstantType.Boolean:
				writer.Write(((bool)this.Value) ? "true" : "false");
				return;
			case MdxConstantType.Null:
				writer.Write("null");
				return;
			case MdxConstantType.Basc:
				writer.Write("BASC");
				return;
			case MdxConstantType.Bdesc:
				writer.Write("BDESC");
				return;
			case MdxConstantType.Leaves:
				writer.Write("LEAVES");
				return;
			case MdxConstantType.All:
				writer.Write("ALL");
				return;
			case MdxConstantType.IncludeEmpty:
				writer.Write("INCLUDEEMPTY");
				return;
			case MdxConstantType.OnePointZero:
				writer.Write("1.0");
				return;
			default:
				throw new InvalidOperationException(this.Type.ToString());
			}
		}

		// Token: 0x04002584 RID: 9604
		public static readonly ConstantMdxExpression All = new ConstantMdxExpression(MdxConstantType.All);

		// Token: 0x04002585 RID: 9605
		public static readonly ConstantMdxExpression Null = new ConstantMdxExpression(MdxConstantType.Null);

		// Token: 0x04002586 RID: 9606
		public static readonly ConstantMdxExpression Basc = new ConstantMdxExpression(MdxConstantType.Basc);

		// Token: 0x04002587 RID: 9607
		public static readonly ConstantMdxExpression Bdesc = new ConstantMdxExpression(MdxConstantType.Bdesc);

		// Token: 0x04002588 RID: 9608
		public static readonly ConstantMdxExpression True = new ConstantMdxExpression(true);

		// Token: 0x04002589 RID: 9609
		public static readonly ConstantMdxExpression False = new ConstantMdxExpression(false);

		// Token: 0x0400258A RID: 9610
		public static readonly ConstantMdxExpression Leaves = new ConstantMdxExpression(MdxConstantType.Leaves);

		// Token: 0x0400258B RID: 9611
		public static readonly ConstantMdxExpression IncludeEmpty = new ConstantMdxExpression(MdxConstantType.IncludeEmpty);

		// Token: 0x0400258C RID: 9612
		public static readonly ConstantMdxExpression OnePointZero = new ConstantMdxExpression(MdxConstantType.OnePointZero);

		// Token: 0x0400258D RID: 9613
		private readonly MdxConstantType type;

		// Token: 0x0400258E RID: 9614
		private readonly object value;
	}
}
