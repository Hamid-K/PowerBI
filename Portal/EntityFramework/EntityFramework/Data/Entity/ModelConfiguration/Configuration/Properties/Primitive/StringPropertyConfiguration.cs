using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x02000207 RID: 519
	internal class StringPropertyConfiguration : LengthPropertyConfiguration
	{
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0004BE84 File Offset: 0x0004A084
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x0004BE8C File Offset: 0x0004A08C
		public bool? IsUnicode { get; set; }

		// Token: 0x06001B85 RID: 7045 RVA: 0x0004BE95 File Offset: 0x0004A095
		public StringPropertyConfiguration()
		{
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x0004BE9D File Offset: 0x0004A09D
		private StringPropertyConfiguration(StringPropertyConfiguration source)
			: base(source)
		{
			this.IsUnicode = source.IsUnicode;
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x0004BEB2 File Offset: 0x0004A0B2
		internal override PrimitivePropertyConfiguration Clone()
		{
			return new StringPropertyConfiguration(this);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x0004BEBC File Offset: 0x0004A0BC
		protected override void ConfigureProperty(EdmProperty property)
		{
			base.ConfigureProperty(property);
			if (this.IsUnicode != null)
			{
				property.IsUnicode = this.IsUnicode;
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0004BEEC File Offset: 0x0004A0EC
		internal override void Configure(EdmProperty column, FacetDescription facetDescription)
		{
			base.Configure(column, facetDescription);
			string facetName = facetDescription.FacetName;
			if (facetName != null && facetName == "Unicode")
			{
				bool? flag;
				if (!facetDescription.IsConstant)
				{
					bool? isUnicode = this.IsUnicode;
					flag = ((isUnicode != null) ? isUnicode : column.IsUnicode);
				}
				else
				{
					flag = null;
				}
				column.IsUnicode = flag;
			}
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0004BF4C File Offset: 0x0004A14C
		internal override void CopyFrom(PrimitivePropertyConfiguration other)
		{
			base.CopyFrom(other);
			StringPropertyConfiguration stringPropertyConfiguration = other as StringPropertyConfiguration;
			if (stringPropertyConfiguration != null)
			{
				this.IsUnicode = stringPropertyConfiguration.IsUnicode;
			}
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0004BF78 File Offset: 0x0004A178
		internal override void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.FillFrom(other, inCSpace);
			StringPropertyConfiguration stringPropertyConfiguration = other as StringPropertyConfiguration;
			if (stringPropertyConfiguration != null && this.IsUnicode == null)
			{
				this.IsUnicode = stringPropertyConfiguration.IsUnicode;
			}
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0004BFB4 File Offset: 0x0004A1B4
		internal override void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			base.MakeCompatibleWith(other, inCSpace);
			StringPropertyConfiguration stringPropertyConfiguration = other as StringPropertyConfiguration;
			if (stringPropertyConfiguration == null)
			{
				return;
			}
			if (stringPropertyConfiguration.IsUnicode != null)
			{
				this.IsUnicode = null;
			}
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0004BFF4 File Offset: 0x0004A1F4
		internal override bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			StringPropertyConfiguration stringPropertyConfiguration = other as StringPropertyConfiguration;
			bool flag = base.IsCompatible(other, inCSpace, out errorMessage);
			bool flag2 = stringPropertyConfiguration == null || base.IsCompatible<bool, StringPropertyConfiguration>((StringPropertyConfiguration c) => c.IsUnicode, stringPropertyConfiguration, ref errorMessage);
			return flag && flag2;
		}
	}
}
