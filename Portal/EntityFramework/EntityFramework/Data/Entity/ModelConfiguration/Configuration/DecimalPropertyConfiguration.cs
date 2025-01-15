using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001F3 RID: 499
	public class DecimalPropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x06001A2A RID: 6698 RVA: 0x00046BBA File Offset: 0x00044DBA
		internal DecimalPropertyConfiguration(DecimalPropertyConfiguration configuration)
			: base(configuration)
		{
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00046BC3 File Offset: 0x00044DC3
		public new DecimalPropertyConfiguration IsOptional()
		{
			base.IsOptional();
			return this;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00046BCD File Offset: 0x00044DCD
		public new DecimalPropertyConfiguration IsRequired()
		{
			base.IsRequired();
			return this;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00046BD7 File Offset: 0x00044DD7
		public new DecimalPropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption? databaseGeneratedOption)
		{
			base.HasDatabaseGeneratedOption(databaseGeneratedOption);
			return this;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00046BE2 File Offset: 0x00044DE2
		public new DecimalPropertyConfiguration IsConcurrencyToken()
		{
			base.IsConcurrencyToken();
			return this;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00046BEC File Offset: 0x00044DEC
		public new DecimalPropertyConfiguration IsConcurrencyToken(bool? concurrencyToken)
		{
			base.IsConcurrencyToken(concurrencyToken);
			return this;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00046BF7 File Offset: 0x00044DF7
		public new DecimalPropertyConfiguration HasColumnName(string columnName)
		{
			base.HasColumnName(columnName);
			return this;
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00046C02 File Offset: 0x00044E02
		public new DecimalPropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			base.HasColumnAnnotation(name, value);
			return this;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00046C0E File Offset: 0x00044E0E
		public new DecimalPropertyConfiguration HasColumnType(string columnType)
		{
			base.HasColumnType(columnType);
			return this;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00046C19 File Offset: 0x00044E19
		public new DecimalPropertyConfiguration HasColumnOrder(int? columnOrder)
		{
			base.HasColumnOrder(columnOrder);
			return this;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00046C24 File Offset: 0x00044E24
		public DecimalPropertyConfiguration HasPrecision(byte precision, byte scale)
		{
			this.Configuration.Precision = new byte?(precision);
			this.Configuration.Scale = new byte?(scale);
			return this;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00046C49 File Offset: 0x00044E49
		internal new DecimalPropertyConfiguration Configuration
		{
			get
			{
				return (DecimalPropertyConfiguration)base.Configuration;
			}
		}
	}
}
