using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001F1 RID: 497
	public class BinaryPropertyConfiguration : LengthPropertyConfiguration
	{
		// Token: 0x06001A0E RID: 6670 RVA: 0x00046A7B File Offset: 0x00044C7B
		internal BinaryPropertyConfiguration(BinaryPropertyConfiguration configuration)
			: base(configuration)
		{
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00046A84 File Offset: 0x00044C84
		public new BinaryPropertyConfiguration IsMaxLength()
		{
			base.IsMaxLength();
			return this;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00046A8E File Offset: 0x00044C8E
		public new BinaryPropertyConfiguration HasMaxLength(int? value)
		{
			base.HasMaxLength(value);
			return this;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00046A99 File Offset: 0x00044C99
		public new BinaryPropertyConfiguration IsFixedLength()
		{
			base.IsFixedLength();
			return this;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00046AA3 File Offset: 0x00044CA3
		public new BinaryPropertyConfiguration IsVariableLength()
		{
			base.IsVariableLength();
			return this;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x00046AAD File Offset: 0x00044CAD
		public new BinaryPropertyConfiguration IsOptional()
		{
			base.IsOptional();
			return this;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00046AB7 File Offset: 0x00044CB7
		public new BinaryPropertyConfiguration IsRequired()
		{
			base.IsRequired();
			return this;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00046AC1 File Offset: 0x00044CC1
		public new BinaryPropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption? databaseGeneratedOption)
		{
			base.HasDatabaseGeneratedOption(databaseGeneratedOption);
			return this;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00046ACC File Offset: 0x00044CCC
		public new BinaryPropertyConfiguration IsConcurrencyToken()
		{
			base.IsConcurrencyToken();
			return this;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x00046AD6 File Offset: 0x00044CD6
		public new BinaryPropertyConfiguration IsConcurrencyToken(bool? concurrencyToken)
		{
			base.IsConcurrencyToken(concurrencyToken);
			return this;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00046AE1 File Offset: 0x00044CE1
		public new BinaryPropertyConfiguration HasColumnName(string columnName)
		{
			base.HasColumnName(columnName);
			return this;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00046AEC File Offset: 0x00044CEC
		public new BinaryPropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			base.HasColumnAnnotation(name, value);
			return this;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00046AF8 File Offset: 0x00044CF8
		public new BinaryPropertyConfiguration HasColumnType(string columnType)
		{
			base.HasColumnType(columnType);
			return this;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00046B03 File Offset: 0x00044D03
		public new BinaryPropertyConfiguration HasColumnOrder(int? columnOrder)
		{
			base.HasColumnOrder(columnOrder);
			return this;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00046B0E File Offset: 0x00044D0E
		public BinaryPropertyConfiguration IsRowVersion()
		{
			this.Configuration.IsRowVersion = new bool?(true);
			return this;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x00046B22 File Offset: 0x00044D22
		internal new BinaryPropertyConfiguration Configuration
		{
			get
			{
				return (BinaryPropertyConfiguration)base.Configuration;
			}
		}
	}
}
