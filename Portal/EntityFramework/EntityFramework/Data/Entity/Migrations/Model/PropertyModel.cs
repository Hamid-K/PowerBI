using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020000C5 RID: 197
	public abstract class PropertyModel
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x00020CB4 File Offset: 0x0001EEB4
		protected PropertyModel(PrimitiveTypeKind type, TypeUsage typeUsage)
		{
			this._type = type;
			this._typeUsage = typeUsage;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00020CCA File Offset: 0x0001EECA
		public virtual PrimitiveTypeKind Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00020CD4 File Offset: 0x0001EED4
		public TypeUsage TypeUsage
		{
			get
			{
				TypeUsage typeUsage;
				if ((typeUsage = this._typeUsage) == null)
				{
					typeUsage = (this._typeUsage = this.BuildTypeUsage());
				}
				return typeUsage;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00020CFA File Offset: 0x0001EEFA
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00020D02 File Offset: 0x0001EF02
		public virtual string Name { get; set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00020D0B File Offset: 0x0001EF0B
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00020D13 File Offset: 0x0001EF13
		public virtual string StoreType { get; set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x00020D1C File Offset: 0x0001EF1C
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x00020D24 File Offset: 0x0001EF24
		public virtual int? MaxLength { get; set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x00020D2D File Offset: 0x0001EF2D
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x00020D35 File Offset: 0x0001EF35
		public virtual byte? Precision { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00020D3E File Offset: 0x0001EF3E
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x00020D46 File Offset: 0x0001EF46
		public virtual byte? Scale { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00020D4F File Offset: 0x0001EF4F
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x00020D57 File Offset: 0x0001EF57
		public virtual object DefaultValue { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00020D60 File Offset: 0x0001EF60
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x00020D68 File Offset: 0x0001EF68
		public virtual string DefaultValueSql { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00020D71 File Offset: 0x0001EF71
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x00020D79 File Offset: 0x0001EF79
		public virtual bool? IsFixedLength { get; set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00020D82 File Offset: 0x0001EF82
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x00020D8A File Offset: 0x0001EF8A
		public virtual bool? IsUnicode { get; set; }

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00020D94 File Offset: 0x0001EF94
		private TypeUsage BuildTypeUsage()
		{
			PrimitiveType edmPrimitiveType = PrimitiveType.GetEdmPrimitiveType(this.Type);
			if (this.Type == PrimitiveTypeKind.Binary)
			{
				if (this.MaxLength != null)
				{
					return TypeUsage.CreateBinaryTypeUsage(edmPrimitiveType, this.IsFixedLength.GetValueOrDefault(), this.MaxLength.Value);
				}
				return TypeUsage.CreateBinaryTypeUsage(edmPrimitiveType, this.IsFixedLength.GetValueOrDefault());
			}
			else if (this.Type == PrimitiveTypeKind.String)
			{
				if (this.MaxLength != null)
				{
					return TypeUsage.CreateStringTypeUsage(edmPrimitiveType, this.IsUnicode ?? true, this.IsFixedLength.GetValueOrDefault(), this.MaxLength.Value);
				}
				return TypeUsage.CreateStringTypeUsage(edmPrimitiveType, this.IsUnicode ?? true, this.IsFixedLength.GetValueOrDefault());
			}
			else
			{
				if (this.Type == PrimitiveTypeKind.DateTime)
				{
					return TypeUsage.CreateDateTimeTypeUsage(edmPrimitiveType, this.Precision);
				}
				if (this.Type == PrimitiveTypeKind.DateTimeOffset)
				{
					return TypeUsage.CreateDateTimeOffsetTypeUsage(edmPrimitiveType, this.Precision);
				}
				if (this.Type == PrimitiveTypeKind.Decimal)
				{
					if (this.Precision != null || this.Scale != null)
					{
						return TypeUsage.CreateDecimalTypeUsage(edmPrimitiveType, this.Precision ?? 18, this.Scale.GetValueOrDefault());
					}
					return TypeUsage.CreateDecimalTypeUsage(edmPrimitiveType);
				}
				else
				{
					if (this.Type != PrimitiveTypeKind.Time)
					{
						return TypeUsage.CreateDefaultTypeUsage(edmPrimitiveType);
					}
					return TypeUsage.CreateTimeTypeUsage(edmPrimitiveType, this.Precision);
				}
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00020F30 File Offset: 0x0001F130
		internal virtual FacetValues ToFacetValues()
		{
			FacetValues facetValues = new FacetValues();
			if (this.DefaultValue != null)
			{
				facetValues.DefaultValue = this.DefaultValue;
			}
			if (this.IsFixedLength != null)
			{
				facetValues.FixedLength = new bool?(this.IsFixedLength.Value);
			}
			if (this.IsUnicode != null)
			{
				facetValues.Unicode = new bool?(this.IsUnicode.Value);
			}
			if (this.MaxLength != null)
			{
				facetValues.MaxLength = new int?(this.MaxLength.Value);
			}
			if (this.Precision != null)
			{
				facetValues.Precision = new byte?(this.Precision.Value);
			}
			if (this.Scale != null)
			{
				facetValues.Scale = new byte?(this.Scale.Value);
			}
			return facetValues;
		}

		// Token: 0x0400087F RID: 2175
		private readonly PrimitiveTypeKind _type;

		// Token: 0x04000880 RID: 2176
		private TypeUsage _typeUsage;
	}
}
