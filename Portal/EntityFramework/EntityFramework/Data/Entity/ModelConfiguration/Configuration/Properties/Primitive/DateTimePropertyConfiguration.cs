using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x02000202 RID: 514
	internal class DateTimePropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x0004A621 File Offset: 0x00048821
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x0004A629 File Offset: 0x00048829
		public byte? Precision { get; set; }

		// Token: 0x06001B33 RID: 6963 RVA: 0x0004A632 File Offset: 0x00048832
		public DateTimePropertyConfiguration()
		{
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0004A63A File Offset: 0x0004883A
		private DateTimePropertyConfiguration(DateTimePropertyConfiguration source)
			: base(source)
		{
			this.Precision = source.Precision;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0004A64F File Offset: 0x0004884F
		internal override PrimitivePropertyConfiguration Clone()
		{
			return new DateTimePropertyConfiguration(this);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0004A658 File Offset: 0x00048858
		protected override void ConfigureProperty(EdmProperty property)
		{
			base.ConfigureProperty(property);
			if (this.Precision != null)
			{
				property.Precision = this.Precision;
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0004A688 File Offset: 0x00048888
		internal override void Configure(EdmProperty column, FacetDescription facetDescription)
		{
			base.Configure(column, facetDescription);
			string facetName = facetDescription.FacetName;
			if (facetName != null && facetName == "Precision")
			{
				byte? b;
				if (!facetDescription.IsConstant)
				{
					byte? precision = this.Precision;
					b = ((precision != null) ? precision : column.Precision);
				}
				else
				{
					b = null;
				}
				column.Precision = b;
			}
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0004A6E8 File Offset: 0x000488E8
		internal override void CopyFrom(PrimitivePropertyConfiguration other)
		{
			base.CopyFrom(other);
			DateTimePropertyConfiguration dateTimePropertyConfiguration = other as DateTimePropertyConfiguration;
			if (dateTimePropertyConfiguration != null)
			{
				this.Precision = dateTimePropertyConfiguration.Precision;
			}
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0004A714 File Offset: 0x00048914
		internal override void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.FillFrom(other, inCSpace);
			DateTimePropertyConfiguration dateTimePropertyConfiguration = other as DateTimePropertyConfiguration;
			if (dateTimePropertyConfiguration != null && this.Precision == null)
			{
				this.Precision = dateTimePropertyConfiguration.Precision;
			}
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0004A750 File Offset: 0x00048950
		internal override void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.MakeCompatibleWith(other, inCSpace);
			DateTimePropertyConfiguration dateTimePropertyConfiguration = other as DateTimePropertyConfiguration;
			if (dateTimePropertyConfiguration == null)
			{
				return;
			}
			if (dateTimePropertyConfiguration.Precision != null)
			{
				this.Precision = null;
			}
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0004A790 File Offset: 0x00048990
		internal override bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			DateTimePropertyConfiguration dateTimePropertyConfiguration = other as DateTimePropertyConfiguration;
			bool flag = base.IsCompatible(other, inCSpace, out errorMessage);
			bool flag2 = dateTimePropertyConfiguration == null || base.IsCompatible<byte, DateTimePropertyConfiguration>((DateTimePropertyConfiguration c) => c.Precision, dateTimePropertyConfiguration, ref errorMessage);
			return flag && flag2;
		}
	}
}
