using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001B4 RID: 436
	public class SqlCePropertyMaxLengthConvention : IConceptualModelConvention<EntityType>, IConvention, IConceptualModelConvention<ComplexType>
	{
		// Token: 0x06001795 RID: 6037 RVA: 0x0003FE62 File Offset: 0x0003E062
		public SqlCePropertyMaxLengthConvention()
			: this(4000)
		{
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0003FE6F File Offset: 0x0003E06F
		public SqlCePropertyMaxLengthConvention(int length)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", Strings.InvalidMaxLengthSize);
			}
			this._length = length;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0003FE94 File Offset: 0x0003E094
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			DbProviderInfo providerInfo = model.ProviderInfo;
			if (providerInfo != null && providerInfo.IsSqlCe())
			{
				this.SetLength(item.DeclaredProperties);
			}
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0003FED8 File Offset: 0x0003E0D8
		public virtual void Apply(ComplexType item, DbModel model)
		{
			Check.NotNull<ComplexType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			DbProviderInfo providerInfo = model.ProviderInfo;
			if (providerInfo != null && providerInfo.IsSqlCe())
			{
				this.SetLength(item.Properties);
			}
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0003FF1C File Offset: 0x0003E11C
		private void SetLength(IEnumerable<EdmProperty> properties)
		{
			foreach (EdmProperty edmProperty in properties)
			{
				if (edmProperty.IsPrimitiveType && (edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String) || edmProperty.PrimitiveType == PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Binary)))
				{
					this.SetDefaults(edmProperty);
				}
			}
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0003FF8C File Offset: 0x0003E18C
		private void SetDefaults(EdmProperty property)
		{
			if (property.MaxLength == null && !property.IsMaxLength)
			{
				property.MaxLength = new int?(this._length);
			}
		}

		// Token: 0x04000A39 RID: 2617
		private const int DefaultLength = 4000;

		// Token: 0x04000A3A RID: 2618
		private readonly int _length;
	}
}
