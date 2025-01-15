using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001AD RID: 429
	public abstract class KeyDiscoveryConvention : IConceptualModelConvention<EntityType>, IConvention
	{
		// Token: 0x0600177E RID: 6014 RVA: 0x0003F7DC File Offset: 0x0003D9DC
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.KeyProperties.Count > 0 || item.BaseType != null)
			{
				return;
			}
			foreach (EdmProperty edmProperty in this.MatchKeyProperty(item, item.GetDeclaredPrimitiveProperties()))
			{
				edmProperty.Nullable = false;
				item.AddKeyMember(edmProperty);
			}
		}

		// Token: 0x0600177F RID: 6015
		protected abstract IEnumerable<EdmProperty> MatchKeyProperty(EntityType entityType, IEnumerable<EdmProperty> primitiveProperties);
	}
}
