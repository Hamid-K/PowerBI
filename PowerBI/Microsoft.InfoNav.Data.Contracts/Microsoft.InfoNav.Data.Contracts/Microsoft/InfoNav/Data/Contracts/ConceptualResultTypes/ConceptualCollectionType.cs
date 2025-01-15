using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes
{
	// Token: 0x0200013E RID: 318
	public sealed class ConceptualCollectionType : ConceptualResultType
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x00010D2A File Offset: 0x0000EF2A
		private ConceptualCollectionType(ConceptualPrimitiveResultType primitive)
		{
			this.PrimitiveType = primitive;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00010D39 File Offset: 0x0000EF39
		public override ConceptualResultTypeKind Kind
		{
			get
			{
				return ConceptualResultTypeKind.Collection;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00010D3C File Offset: 0x0000EF3C
		public ConceptualPrimitiveResultType PrimitiveType { get; }

		// Token: 0x06000826 RID: 2086 RVA: 0x00010D44 File Offset: 0x0000EF44
		public static ConceptualCollectionType FromPrimitive(ConceptualPrimitiveResultType primitive)
		{
			switch (primitive.ConceptualDataType)
			{
			case ConceptualPrimitiveType.Text:
				return ConceptualCollectionType.Text;
			case ConceptualPrimitiveType.Decimal:
				return ConceptualCollectionType.Decimal;
			case ConceptualPrimitiveType.Double:
				return ConceptualCollectionType.Double;
			case ConceptualPrimitiveType.Integer:
				return ConceptualCollectionType.Integer;
			case ConceptualPrimitiveType.Boolean:
				return ConceptualCollectionType.Boolean;
			case ConceptualPrimitiveType.DateTime:
				return ConceptualCollectionType.DateTime;
			case ConceptualPrimitiveType.DateTimeZone:
				return ConceptualCollectionType.DateTimeZone;
			case ConceptualPrimitiveType.Time:
				return ConceptualCollectionType.Time;
			case ConceptualPrimitiveType.Binary:
				return ConceptualCollectionType.Binary;
			case ConceptualPrimitiveType.Variant:
				return ConceptualCollectionType.Variant;
			}
			throw new InvalidOperationException("Unexpected Primitive type for collection " + ((primitive != null) ? primitive.ToString() : null));
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00010DF0 File Offset: 0x0000EFF0
		public override bool Equals(ConceptualResultType other)
		{
			ConceptualCollectionType conceptualCollectionType = other as ConceptualCollectionType;
			return conceptualCollectionType != null && this.PrimitiveType.Equals(conceptualCollectionType.PrimitiveType);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00010E1A File Offset: 0x0000F01A
		public override int GetHashCode()
		{
			return this.PrimitiveType.GetHashCode();
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00010E27 File Offset: 0x0000F027
		public override string ToString()
		{
			return StringUtil.FormatInvariant("Collection[{0}]", this.PrimitiveType);
		}

		// Token: 0x040003C0 RID: 960
		public static readonly ConceptualCollectionType Text = new ConceptualCollectionType(ConceptualPrimitiveResultType.Text);

		// Token: 0x040003C1 RID: 961
		public static readonly ConceptualCollectionType Decimal = new ConceptualCollectionType(ConceptualPrimitiveResultType.Decimal);

		// Token: 0x040003C2 RID: 962
		public static readonly ConceptualCollectionType Double = new ConceptualCollectionType(ConceptualPrimitiveResultType.Double);

		// Token: 0x040003C3 RID: 963
		public static readonly ConceptualCollectionType DateTime = new ConceptualCollectionType(ConceptualPrimitiveResultType.DateTime);

		// Token: 0x040003C4 RID: 964
		public static readonly ConceptualCollectionType DateTimeZone = new ConceptualCollectionType(ConceptualPrimitiveResultType.DateTimeZone);

		// Token: 0x040003C5 RID: 965
		public static readonly ConceptualCollectionType Time = new ConceptualCollectionType(ConceptualPrimitiveResultType.Time);

		// Token: 0x040003C6 RID: 966
		public static readonly ConceptualCollectionType Binary = new ConceptualCollectionType(ConceptualPrimitiveResultType.Binary);

		// Token: 0x040003C7 RID: 967
		public static readonly ConceptualCollectionType Boolean = new ConceptualCollectionType(ConceptualPrimitiveResultType.Boolean);

		// Token: 0x040003C8 RID: 968
		public static readonly ConceptualCollectionType Integer = new ConceptualCollectionType(ConceptualPrimitiveResultType.Integer);

		// Token: 0x040003C9 RID: 969
		public static readonly ConceptualCollectionType Variant = new ConceptualCollectionType(ConceptualPrimitiveResultType.Variant);
	}
}
