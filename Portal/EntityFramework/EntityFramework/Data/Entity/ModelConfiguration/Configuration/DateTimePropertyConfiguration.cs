using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001F2 RID: 498
	public class DateTimePropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x06001A1E RID: 6686 RVA: 0x00046B2F File Offset: 0x00044D2F
		internal DateTimePropertyConfiguration(DateTimePropertyConfiguration configuration)
			: base(configuration)
		{
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00046B38 File Offset: 0x00044D38
		public new DateTimePropertyConfiguration IsOptional()
		{
			base.IsOptional();
			return this;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00046B42 File Offset: 0x00044D42
		public new DateTimePropertyConfiguration IsRequired()
		{
			base.IsRequired();
			return this;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x00046B4C File Offset: 0x00044D4C
		public new DateTimePropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption? databaseGeneratedOption)
		{
			base.HasDatabaseGeneratedOption(databaseGeneratedOption);
			return this;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00046B57 File Offset: 0x00044D57
		public new DateTimePropertyConfiguration IsConcurrencyToken()
		{
			base.IsConcurrencyToken();
			return this;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00046B61 File Offset: 0x00044D61
		public new DateTimePropertyConfiguration IsConcurrencyToken(bool? concurrencyToken)
		{
			base.IsConcurrencyToken(concurrencyToken);
			return this;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x00046B6C File Offset: 0x00044D6C
		public new DateTimePropertyConfiguration HasColumnName(string columnName)
		{
			base.HasColumnName(columnName);
			return this;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x00046B77 File Offset: 0x00044D77
		public new DateTimePropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			base.HasColumnAnnotation(name, value);
			return this;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00046B83 File Offset: 0x00044D83
		public new DateTimePropertyConfiguration HasColumnType(string columnType)
		{
			base.HasColumnType(columnType);
			return this;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00046B8E File Offset: 0x00044D8E
		public new DateTimePropertyConfiguration HasColumnOrder(int? columnOrder)
		{
			base.HasColumnOrder(columnOrder);
			return this;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00046B99 File Offset: 0x00044D99
		public DateTimePropertyConfiguration HasPrecision(byte value)
		{
			this.Configuration.Precision = new byte?(value);
			return this;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x00046BAD File Offset: 0x00044DAD
		internal new DateTimePropertyConfiguration Configuration
		{
			get
			{
				return (DateTimePropertyConfiguration)base.Configuration;
			}
		}
	}
}
