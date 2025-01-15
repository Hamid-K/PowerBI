using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001F8 RID: 504
	public class ConventionPrimitivePropertyConfiguration
	{
		// Token: 0x06001A61 RID: 6753 RVA: 0x00046F7C File Offset: 0x0004517C
		internal ConventionPrimitivePropertyConfiguration(PropertyInfo propertyInfo, Func<PrimitivePropertyConfiguration> configuration)
		{
			this._propertyInfo = propertyInfo;
			this._configuration = configuration;
			this._binaryConfiguration = new Lazy<BinaryPropertyConfiguration>(() => this._configuration() as BinaryPropertyConfiguration);
			this._dateTimeConfiguration = new Lazy<DateTimePropertyConfiguration>(() => this._configuration() as DateTimePropertyConfiguration);
			this._decimalConfiguration = new Lazy<DecimalPropertyConfiguration>(() => this._configuration() as DecimalPropertyConfiguration);
			this._lengthConfiguration = new Lazy<LengthPropertyConfiguration>(() => this._configuration() as LengthPropertyConfiguration);
			this._stringConfiguration = new Lazy<StringPropertyConfiguration>(() => this._configuration() as StringPropertyConfiguration);
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x00047010 File Offset: 0x00045210
		public virtual PropertyInfo ClrPropertyInfo
		{
			get
			{
				return this._propertyInfo;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x00047018 File Offset: 0x00045218
		internal Func<PrimitivePropertyConfiguration> Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00047020 File Offset: 0x00045220
		public virtual ConventionPrimitivePropertyConfiguration HasColumnName(string columnName)
		{
			Check.NotEmpty(columnName, "columnName");
			if (this._configuration() != null && this._configuration().ColumnName == null)
			{
				this._configuration().ColumnName = columnName;
			}
			return this;
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00047060 File Offset: 0x00045260
		public virtual ConventionPrimitivePropertyConfiguration HasColumnAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			if (this._configuration() != null && !this._configuration().Annotations.ContainsKey(name))
			{
				this._configuration().SetAnnotation(name, value);
			}
			return this;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000470B1 File Offset: 0x000452B1
		public virtual ConventionPrimitivePropertyConfiguration HasParameterName(string parameterName)
		{
			Check.NotEmpty(parameterName, "parameterName");
			if (this._configuration() != null && this._configuration().ParameterName == null)
			{
				this._configuration().ParameterName = parameterName;
			}
			return this;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x000470F0 File Offset: 0x000452F0
		public virtual ConventionPrimitivePropertyConfiguration HasColumnOrder(int columnOrder)
		{
			if (columnOrder < 0)
			{
				throw new ArgumentOutOfRangeException("columnOrder");
			}
			if (this._configuration() != null && this._configuration().ColumnOrder == null)
			{
				this._configuration().ColumnOrder = new int?(columnOrder);
			}
			return this;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0004714A File Offset: 0x0004534A
		public virtual ConventionPrimitivePropertyConfiguration HasColumnType(string columnType)
		{
			Check.NotEmpty(columnType, "columnType");
			if (this._configuration() != null && this._configuration().ColumnType == null)
			{
				this._configuration().ColumnType = columnType;
			}
			return this;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00047189 File Offset: 0x00045389
		public virtual ConventionPrimitivePropertyConfiguration IsConcurrencyToken()
		{
			return this.IsConcurrencyToken(true);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00047194 File Offset: 0x00045394
		public virtual ConventionPrimitivePropertyConfiguration IsConcurrencyToken(bool concurrencyToken)
		{
			if (this._configuration() != null && this._configuration().ConcurrencyMode == null)
			{
				this._configuration().ConcurrencyMode = new ConcurrencyMode?(concurrencyToken ? ConcurrencyMode.Fixed : ConcurrencyMode.None);
			}
			return this;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000471E8 File Offset: 0x000453E8
		public virtual ConventionPrimitivePropertyConfiguration HasDatabaseGeneratedOption(DatabaseGeneratedOption databaseGeneratedOption)
		{
			if (!Enum.IsDefined(typeof(DatabaseGeneratedOption), databaseGeneratedOption))
			{
				throw new ArgumentOutOfRangeException("databaseGeneratedOption");
			}
			if (this._configuration() != null && this._configuration().DatabaseGeneratedOption == null)
			{
				this._configuration().DatabaseGeneratedOption = new DatabaseGeneratedOption?(databaseGeneratedOption);
			}
			return this;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00047258 File Offset: 0x00045458
		public virtual ConventionPrimitivePropertyConfiguration IsOptional()
		{
			if (this._configuration() != null && this._configuration().IsNullable == null)
			{
				if (!this._propertyInfo.PropertyType.IsNullable())
				{
					Type declaringType = this._propertyInfo.DeclaringType;
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_NonNullableProperty(((declaringType != null) ? declaringType.ToString() : null) + "." + this._propertyInfo.Name, this._propertyInfo.PropertyType.Name));
				}
				this._configuration().IsNullable = new bool?(true);
			}
			return this;
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00047300 File Offset: 0x00045500
		public virtual ConventionPrimitivePropertyConfiguration IsRequired()
		{
			if (this._configuration() != null && this._configuration().IsNullable == null)
			{
				this._configuration().IsNullable = new bool?(false);
			}
			return this;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0004734B File Offset: 0x0004554B
		public virtual ConventionPrimitivePropertyConfiguration IsUnicode()
		{
			return this.IsUnicode(true);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00047354 File Offset: 0x00045554
		public virtual ConventionPrimitivePropertyConfiguration IsUnicode(bool unicode)
		{
			if (this._configuration() != null)
			{
				if (this._stringConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_IsUnicodeNonString(this._propertyInfo.Name));
				}
				if (this._stringConfiguration.Value.IsUnicode == null)
				{
					this._stringConfiguration.Value.IsUnicode = new bool?(unicode);
				}
			}
			return this;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000473C4 File Offset: 0x000455C4
		public virtual ConventionPrimitivePropertyConfiguration IsFixedLength()
		{
			if (this._configuration() != null)
			{
				if (this._lengthConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_NonLength(this._propertyInfo.Name));
				}
				if (this._lengthConfiguration.Value.IsFixedLength == null)
				{
					this._lengthConfiguration.Value.IsFixedLength = new bool?(true);
				}
			}
			return this;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00047434 File Offset: 0x00045634
		public virtual ConventionPrimitivePropertyConfiguration IsVariableLength()
		{
			if (this._configuration() != null)
			{
				if (this._lengthConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_NonLength(this._propertyInfo.Name));
				}
				if (this._lengthConfiguration.Value.IsFixedLength == null)
				{
					this._lengthConfiguration.Value.IsFixedLength = new bool?(false);
				}
			}
			return this;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x000474A4 File Offset: 0x000456A4
		public virtual ConventionPrimitivePropertyConfiguration HasMaxLength(int maxLength)
		{
			if (maxLength < 1)
			{
				throw new ArgumentOutOfRangeException("maxLength");
			}
			if (this._configuration() != null)
			{
				if (this._lengthConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_NonLength(this._propertyInfo.Name));
				}
				if (this._lengthConfiguration.Value.MaxLength == null && this._lengthConfiguration.Value.IsMaxLength == null)
				{
					this._lengthConfiguration.Value.MaxLength = new int?(maxLength);
					if (this._lengthConfiguration.Value.IsFixedLength == null)
					{
						this._lengthConfiguration.Value.IsFixedLength = new bool?(false);
					}
				}
			}
			return this;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00047570 File Offset: 0x00045770
		public virtual ConventionPrimitivePropertyConfiguration IsMaxLength()
		{
			if (this._configuration() != null)
			{
				if (this._lengthConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_NonLength(this._propertyInfo.Name));
				}
				if (this._lengthConfiguration.Value.IsMaxLength == null && this._lengthConfiguration.Value.MaxLength == null)
				{
					this._lengthConfiguration.Value.IsMaxLength = new bool?(true);
				}
			}
			return this;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000475F8 File Offset: 0x000457F8
		public virtual ConventionPrimitivePropertyConfiguration HasPrecision(byte value)
		{
			if (this._configuration() != null)
			{
				if (this._dateTimeConfiguration.Value == null)
				{
					if (this._decimalConfiguration.Value != null)
					{
						throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_DecimalNoScale(this._propertyInfo.Name));
					}
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_HasPrecisionNonDateTime(this._propertyInfo.Name));
				}
				else if (this._dateTimeConfiguration.Value.Precision == null)
				{
					this._dateTimeConfiguration.Value.Precision = new byte?(value);
				}
			}
			return this;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0004768C File Offset: 0x0004588C
		public virtual ConventionPrimitivePropertyConfiguration HasPrecision(byte precision, byte scale)
		{
			if (this._configuration() != null)
			{
				if (this._decimalConfiguration.Value == null)
				{
					if (this._dateTimeConfiguration.Value != null)
					{
						throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_DateTimeScale(this._propertyInfo.Name));
					}
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_HasPrecisionNonDecimal(this._propertyInfo.Name));
				}
				else if (this._decimalConfiguration.Value.Precision == null && this._decimalConfiguration.Value.Scale == null)
				{
					this._decimalConfiguration.Value.Precision = new byte?(precision);
					this._decimalConfiguration.Value.Scale = new byte?(scale);
				}
			}
			return this;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00047750 File Offset: 0x00045950
		public virtual ConventionPrimitivePropertyConfiguration IsRowVersion()
		{
			if (this._configuration() != null)
			{
				if (this._binaryConfiguration.Value == null)
				{
					throw new InvalidOperationException(Strings.LightweightPrimitivePropertyConfiguration_IsRowVersionNonBinary(this._propertyInfo.Name));
				}
				if (this._binaryConfiguration.Value.IsRowVersion == null)
				{
					this._binaryConfiguration.Value.IsRowVersion = new bool?(true);
				}
			}
			return this;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000477C0 File Offset: 0x000459C0
		public virtual ConventionPrimitivePropertyConfiguration IsKey()
		{
			if (this._configuration() != null)
			{
				EntityTypeConfiguration entityTypeConfiguration = this._configuration().TypeConfiguration as EntityTypeConfiguration;
				if (entityTypeConfiguration != null && !entityTypeConfiguration.IsKeyConfigured)
				{
					entityTypeConfiguration.Key(this.ClrPropertyInfo);
				}
			}
			return this;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00047808 File Offset: 0x00045A08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00047810 File Offset: 0x00045A10
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00047819 File Offset: 0x00045A19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00047821 File Offset: 0x00045A21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A91 RID: 2705
		private readonly PropertyInfo _propertyInfo;

		// Token: 0x04000A92 RID: 2706
		private readonly Func<PrimitivePropertyConfiguration> _configuration;

		// Token: 0x04000A93 RID: 2707
		private readonly Lazy<BinaryPropertyConfiguration> _binaryConfiguration;

		// Token: 0x04000A94 RID: 2708
		private readonly Lazy<DateTimePropertyConfiguration> _dateTimeConfiguration;

		// Token: 0x04000A95 RID: 2709
		private readonly Lazy<DecimalPropertyConfiguration> _decimalConfiguration;

		// Token: 0x04000A96 RID: 2710
		private readonly Lazy<LengthPropertyConfiguration> _lengthConfiguration;

		// Token: 0x04000A97 RID: 2711
		private readonly Lazy<StringPropertyConfiguration> _stringConfiguration;
	}
}
