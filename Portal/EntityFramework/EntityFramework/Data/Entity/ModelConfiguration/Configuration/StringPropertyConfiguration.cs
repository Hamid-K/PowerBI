using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001F7 RID: 503
	public class StringPropertyConfiguration : LengthPropertyConfiguration
	{
		// Token: 0x06001A50 RID: 6736 RVA: 0x00046EBC File Offset: 0x000450BC
		internal StringPropertyConfiguration(StringPropertyConfiguration configuration)
			: base(configuration)
		{
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00046EC5 File Offset: 0x000450C5
		public new StringPropertyConfiguration IsMaxLength()
		{
			base.IsMaxLength();
			return this;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00046ECF File Offset: 0x000450CF
		public new StringPropertyConfiguration HasMaxLength(int? value)
		{
			base.HasMaxLength(value);
			return this;
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00046EDA File Offset: 0x000450DA
		public new StringPropertyConfiguration IsFixedLength()
		{
			base.IsFixedLength();
			return this;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00046EE4 File Offset: 0x000450E4
		public new StringPropertyConfiguration IsVariableLength()
		{
			base.IsVariableLength();
			return this;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00046EEE File Offset: 0x000450EE
		public new StringPropertyConfiguration IsOptional()
		{
			base.IsOptional();
			return this;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00046EF8 File Offset: 0x000450F8
		public new StringPropertyConfiguration IsRequired()
		{
			base.IsRequired();
			return this;
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00046F02 File Offset: 0x00045102
		public new StringPropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption? databaseGeneratedOption)
		{
			base.HasDatabaseGeneratedOption(databaseGeneratedOption);
			return this;
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00046F0D File Offset: 0x0004510D
		public new StringPropertyConfiguration IsConcurrencyToken()
		{
			base.IsConcurrencyToken();
			return this;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00046F17 File Offset: 0x00045117
		public new StringPropertyConfiguration IsConcurrencyToken(bool? concurrencyToken)
		{
			base.IsConcurrencyToken(concurrencyToken);
			return this;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00046F22 File Offset: 0x00045122
		public new StringPropertyConfiguration HasColumnName(string columnName)
		{
			base.HasColumnName(columnName);
			return this;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00046F2D File Offset: 0x0004512D
		public new StringPropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			base.HasColumnAnnotation(name, value);
			return this;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00046F39 File Offset: 0x00045139
		public new StringPropertyConfiguration HasColumnType(string columnType)
		{
			base.HasColumnType(columnType);
			return this;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00046F44 File Offset: 0x00045144
		public new StringPropertyConfiguration HasColumnOrder(int? columnOrder)
		{
			base.HasColumnOrder(columnOrder);
			return this;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00046F4F File Offset: 0x0004514F
		public StringPropertyConfiguration IsUnicode()
		{
			this.IsUnicode(new bool?(true));
			return this;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00046F5F File Offset: 0x0004515F
		public StringPropertyConfiguration IsUnicode(bool? unicode)
		{
			this.Configuration.IsUnicode = unicode;
			return this;
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x00046F6E File Offset: 0x0004516E
		internal new StringPropertyConfiguration Configuration
		{
			get
			{
				return (StringPropertyConfiguration)base.Configuration;
			}
		}
	}
}
