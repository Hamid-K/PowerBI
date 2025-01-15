using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes
{
	// Token: 0x0200013F RID: 319
	public sealed class ConceptualPrimitiveResultType : ConceptualResultType
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x00010EDF File Offset: 0x0000F0DF
		private ConceptualPrimitiveResultType(ConceptualPrimitiveType dataType)
		{
			this.ConceptualDataType = dataType;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		public static ConceptualPrimitiveResultType FromPrimitive(ConceptualPrimitiveType primitive)
		{
			switch (primitive)
			{
			case ConceptualPrimitiveType.Null:
				return ConceptualPrimitiveResultType.Null;
			case ConceptualPrimitiveType.Text:
				return ConceptualPrimitiveResultType.Text;
			case ConceptualPrimitiveType.Decimal:
				return ConceptualPrimitiveResultType.Decimal;
			case ConceptualPrimitiveType.Double:
				return ConceptualPrimitiveResultType.Double;
			case ConceptualPrimitiveType.Integer:
				return ConceptualPrimitiveResultType.Integer;
			case ConceptualPrimitiveType.Boolean:
				return ConceptualPrimitiveResultType.Boolean;
			case ConceptualPrimitiveType.Date:
				return ConceptualPrimitiveResultType.Date;
			case ConceptualPrimitiveType.DateTime:
				return ConceptualPrimitiveResultType.DateTime;
			case ConceptualPrimitiveType.DateTimeZone:
				return ConceptualPrimitiveResultType.DateTimeZone;
			case ConceptualPrimitiveType.Time:
				return ConceptualPrimitiveResultType.Time;
			case ConceptualPrimitiveType.Duration:
				return ConceptualPrimitiveResultType.Duration;
			case ConceptualPrimitiveType.Binary:
				return ConceptualPrimitiveResultType.Binary;
			case ConceptualPrimitiveType.None:
				return ConceptualPrimitiveResultType.None;
			case ConceptualPrimitiveType.Variant:
				return ConceptualPrimitiveResultType.Variant;
			default:
				throw new InvalidOperationException("Unexpected Primitive type for PrimitiveResult " + primitive.ToString());
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00010FAD File Offset: 0x0000F1AD
		public override ConceptualResultTypeKind Kind
		{
			get
			{
				return ConceptualResultTypeKind.Primitive;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00010FB0 File Offset: 0x0000F1B0
		public ConceptualPrimitiveType ConceptualDataType { get; }

		// Token: 0x0600082F RID: 2095 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		public override bool Equals(ConceptualResultType other)
		{
			ConceptualPrimitiveResultType conceptualPrimitiveResultType = other as ConceptualPrimitiveResultType;
			return conceptualPrimitiveResultType != null && this.ConceptualDataType.Equals(conceptualPrimitiveResultType.ConceptualDataType);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		public override int GetHashCode()
		{
			return this.ConceptualDataType.GetHashCode();
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00011014 File Offset: 0x0000F214
		public override string ToString()
		{
			return "Primitive(" + this.ConceptualDataType.ToString() + ")";
		}

		// Token: 0x040003CB RID: 971
		public static readonly ConceptualPrimitiveResultType Text = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.Text);

		// Token: 0x040003CC RID: 972
		public static readonly ConceptualPrimitiveResultType Decimal = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.Decimal);

		// Token: 0x040003CD RID: 973
		public static readonly ConceptualPrimitiveResultType Double = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.Double);

		// Token: 0x040003CE RID: 974
		public static readonly ConceptualPrimitiveResultType Date = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.Date);

		// Token: 0x040003CF RID: 975
		public static readonly ConceptualPrimitiveResultType DateTime = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.DateTime);

		// Token: 0x040003D0 RID: 976
		public static readonly ConceptualPrimitiveResultType DateTimeZone = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.DateTimeZone);

		// Token: 0x040003D1 RID: 977
		public static readonly ConceptualPrimitiveResultType Duration = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.Duration);

		// Token: 0x040003D2 RID: 978
		public static readonly ConceptualPrimitiveResultType Time = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.Time);

		// Token: 0x040003D3 RID: 979
		public static readonly ConceptualPrimitiveResultType Binary = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.Binary);

		// Token: 0x040003D4 RID: 980
		public static readonly ConceptualPrimitiveResultType Boolean = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.Boolean);

		// Token: 0x040003D5 RID: 981
		public static readonly ConceptualPrimitiveResultType Integer = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.Integer);

		// Token: 0x040003D6 RID: 982
		public static readonly ConceptualPrimitiveResultType Null = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.Null);

		// Token: 0x040003D7 RID: 983
		public static readonly ConceptualPrimitiveResultType None = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.None);

		// Token: 0x040003D8 RID: 984
		public static readonly ConceptualPrimitiveResultType Variant = new ConceptualPrimitiveResultType(ConceptualPrimitiveType.Variant);
	}
}
