using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x02000203 RID: 515
	internal class DecimalPropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0004A7F9 File Offset: 0x000489F9
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x0004A801 File Offset: 0x00048A01
		public byte? Precision { get; set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0004A80A File Offset: 0x00048A0A
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x0004A812 File Offset: 0x00048A12
		public byte? Scale { get; set; }

		// Token: 0x06001B40 RID: 6976 RVA: 0x0004A81B File Offset: 0x00048A1B
		public DecimalPropertyConfiguration()
		{
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0004A823 File Offset: 0x00048A23
		private DecimalPropertyConfiguration(DecimalPropertyConfiguration source)
			: base(source)
		{
			this.Precision = source.Precision;
			this.Scale = source.Scale;
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0004A844 File Offset: 0x00048A44
		internal override PrimitivePropertyConfiguration Clone()
		{
			return new DecimalPropertyConfiguration(this);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0004A84C File Offset: 0x00048A4C
		protected override void ConfigureProperty(EdmProperty property)
		{
			base.ConfigureProperty(property);
			if (this.Precision != null)
			{
				property.Precision = this.Precision;
			}
			if (this.Scale != null)
			{
				property.Scale = this.Scale;
			}
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0004A898 File Offset: 0x00048A98
		internal override void Configure(EdmProperty column, FacetDescription facetDescription)
		{
			base.Configure(column, facetDescription);
			string facetName = facetDescription.FacetName;
			if (facetName != null)
			{
				if (facetName == "Precision")
				{
					byte? b2;
					if (!facetDescription.IsConstant)
					{
						byte? b = this.Precision;
						b2 = ((b != null) ? b : column.Precision);
					}
					else
					{
						b2 = null;
					}
					column.Precision = b2;
					return;
				}
				if (!(facetName == "Scale"))
				{
					return;
				}
				byte? b3;
				if (!facetDescription.IsConstant)
				{
					byte? b = this.Scale;
					b3 = ((b != null) ? b : column.Scale);
				}
				else
				{
					b3 = null;
				}
				column.Scale = b3;
			}
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0004A93C File Offset: 0x00048B3C
		internal override void CopyFrom(PrimitivePropertyConfiguration other)
		{
			base.CopyFrom(other);
			DecimalPropertyConfiguration decimalPropertyConfiguration = other as DecimalPropertyConfiguration;
			if (decimalPropertyConfiguration != null)
			{
				this.Precision = decimalPropertyConfiguration.Precision;
				this.Scale = decimalPropertyConfiguration.Scale;
			}
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0004A974 File Offset: 0x00048B74
		internal override void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.FillFrom(other, inCSpace);
			DecimalPropertyConfiguration decimalPropertyConfiguration = other as DecimalPropertyConfiguration;
			if (decimalPropertyConfiguration != null)
			{
				if (this.Precision == null)
				{
					this.Precision = decimalPropertyConfiguration.Precision;
				}
				if (this.Scale == null)
				{
					this.Scale = decimalPropertyConfiguration.Scale;
				}
			}
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0004A9CC File Offset: 0x00048BCC
		internal override void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.MakeCompatibleWith(other, inCSpace);
			DecimalPropertyConfiguration decimalPropertyConfiguration = other as DecimalPropertyConfiguration;
			if (decimalPropertyConfiguration == null)
			{
				return;
			}
			if (decimalPropertyConfiguration.Precision != null)
			{
				this.Precision = null;
			}
			if (decimalPropertyConfiguration.Scale != null)
			{
				this.Scale = null;
			}
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0004AA2C File Offset: 0x00048C2C
		internal override bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			DecimalPropertyConfiguration decimalPropertyConfiguration = other as DecimalPropertyConfiguration;
			bool flag = base.IsCompatible(other, inCSpace, out errorMessage);
			bool flag2 = decimalPropertyConfiguration == null || base.IsCompatible<byte, DecimalPropertyConfiguration>((DecimalPropertyConfiguration c) => c.Precision, decimalPropertyConfiguration, ref errorMessage);
			bool flag3 = decimalPropertyConfiguration == null || base.IsCompatible<byte, DecimalPropertyConfiguration>((DecimalPropertyConfiguration c) => c.Scale, decimalPropertyConfiguration, ref errorMessage);
			return flag && flag2 && flag3;
		}
	}
}
