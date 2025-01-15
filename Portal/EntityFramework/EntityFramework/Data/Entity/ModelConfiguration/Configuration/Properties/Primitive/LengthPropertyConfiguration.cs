using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x02000204 RID: 516
	internal abstract class LengthPropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x0004AAE5 File Offset: 0x00048CE5
		// (set) Token: 0x06001B4A RID: 6986 RVA: 0x0004AAED File Offset: 0x00048CED
		public bool? IsFixedLength { get; set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001B4B RID: 6987 RVA: 0x0004AAF6 File Offset: 0x00048CF6
		// (set) Token: 0x06001B4C RID: 6988 RVA: 0x0004AAFE File Offset: 0x00048CFE
		public int? MaxLength { get; set; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0004AB07 File Offset: 0x00048D07
		// (set) Token: 0x06001B4E RID: 6990 RVA: 0x0004AB0F File Offset: 0x00048D0F
		public bool? IsMaxLength { get; set; }

		// Token: 0x06001B4F RID: 6991 RVA: 0x0004AB18 File Offset: 0x00048D18
		protected LengthPropertyConfiguration()
		{
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0004AB20 File Offset: 0x00048D20
		protected LengthPropertyConfiguration(LengthPropertyConfiguration source)
			: base(source)
		{
			Check.NotNull<LengthPropertyConfiguration>(source, "source");
			this.IsFixedLength = source.IsFixedLength;
			this.MaxLength = source.MaxLength;
			this.IsMaxLength = source.IsMaxLength;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0004AB5C File Offset: 0x00048D5C
		protected override void ConfigureProperty(EdmProperty property)
		{
			base.ConfigureProperty(property);
			if (this.IsFixedLength != null)
			{
				property.IsFixedLength = this.IsFixedLength;
			}
			if (this.MaxLength != null)
			{
				property.MaxLength = this.MaxLength;
			}
			if (this.IsMaxLength != null)
			{
				property.IsMaxLength = this.IsMaxLength.Value;
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0004ABCC File Offset: 0x00048DCC
		internal override void Configure(EdmProperty column, FacetDescription facetDescription)
		{
			base.Configure(column, facetDescription);
			string facetName = facetDescription.FacetName;
			if (facetName != null)
			{
				if (facetName == "FixedLength")
				{
					bool? flag;
					if (!facetDescription.IsConstant)
					{
						bool? isFixedLength = this.IsFixedLength;
						flag = ((isFixedLength != null) ? isFixedLength : column.IsFixedLength);
					}
					else
					{
						flag = null;
					}
					column.IsFixedLength = flag;
					return;
				}
				if (!(facetName == "MaxLength"))
				{
					return;
				}
				int? num;
				if (!facetDescription.IsConstant)
				{
					int? maxLength = this.MaxLength;
					num = ((maxLength != null) ? maxLength : column.MaxLength);
				}
				else
				{
					num = null;
				}
				column.MaxLength = num;
				column.IsMaxLength = !facetDescription.IsConstant && (this.IsMaxLength ?? column.IsMaxLength);
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0004ACA0 File Offset: 0x00048EA0
		internal override void CopyFrom(PrimitivePropertyConfiguration other)
		{
			base.CopyFrom(other);
			LengthPropertyConfiguration lengthPropertyConfiguration = other as LengthPropertyConfiguration;
			if (lengthPropertyConfiguration != null)
			{
				this.IsFixedLength = lengthPropertyConfiguration.IsFixedLength;
				this.MaxLength = lengthPropertyConfiguration.MaxLength;
				this.IsMaxLength = lengthPropertyConfiguration.IsMaxLength;
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0004ACE4 File Offset: 0x00048EE4
		internal override void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.FillFrom(other, inCSpace);
			LengthPropertyConfiguration lengthPropertyConfiguration = other as LengthPropertyConfiguration;
			if (lengthPropertyConfiguration != null)
			{
				if (this.IsFixedLength == null)
				{
					this.IsFixedLength = lengthPropertyConfiguration.IsFixedLength;
				}
				if (this.MaxLength == null)
				{
					this.MaxLength = lengthPropertyConfiguration.MaxLength;
				}
				if (this.IsMaxLength == null)
				{
					this.IsMaxLength = lengthPropertyConfiguration.IsMaxLength;
				}
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0004AD58 File Offset: 0x00048F58
		internal override void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.MakeCompatibleWith(other, inCSpace);
			LengthPropertyConfiguration lengthPropertyConfiguration = other as LengthPropertyConfiguration;
			if (lengthPropertyConfiguration == null)
			{
				return;
			}
			if (lengthPropertyConfiguration.IsFixedLength != null)
			{
				this.IsFixedLength = null;
			}
			if (lengthPropertyConfiguration.MaxLength != null)
			{
				this.MaxLength = null;
			}
			if (lengthPropertyConfiguration.IsMaxLength != null)
			{
				this.IsMaxLength = null;
			}
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0004ADD8 File Offset: 0x00048FD8
		internal override bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			LengthPropertyConfiguration lengthPropertyConfiguration = other as LengthPropertyConfiguration;
			bool flag = base.IsCompatible(other, inCSpace, out errorMessage);
			bool flag2 = lengthPropertyConfiguration == null || base.IsCompatible<bool, LengthPropertyConfiguration>((LengthPropertyConfiguration c) => c.IsFixedLength, lengthPropertyConfiguration, ref errorMessage);
			bool flag3 = lengthPropertyConfiguration == null || base.IsCompatible<bool, LengthPropertyConfiguration>((LengthPropertyConfiguration c) => c.IsMaxLength, lengthPropertyConfiguration, ref errorMessage);
			bool flag4 = lengthPropertyConfiguration == null || base.IsCompatible<int, LengthPropertyConfiguration>((LengthPropertyConfiguration c) => c.MaxLength, lengthPropertyConfiguration, ref errorMessage);
			return flag && flag2 && flag3 && flag4;
		}
	}
}
