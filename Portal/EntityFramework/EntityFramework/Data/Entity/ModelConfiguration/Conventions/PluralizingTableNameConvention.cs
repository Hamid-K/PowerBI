using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001A6 RID: 422
	public class PluralizingTableNameConvention : IStoreModelConvention<EntityType>, IConvention
	{
		// Token: 0x0600176C RID: 5996 RVA: 0x0003EF34 File Offset: 0x0003D134
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			this._pluralizationService = DbConfiguration.DependencyResolver.GetService<IPluralizationService>();
			if (item.GetTableName() == null)
			{
				EntitySet entitySet = model.StoreModel.GetEntitySet(item);
				entitySet.Table = (from n in (from es in model.StoreModel.GetEntitySets()
						where es.Schema == entitySet.Schema
						select es).Except(new EntitySet[] { entitySet })
					select n.Table).Uniquify(this._pluralizationService.Pluralize(entitySet.Table));
			}
		}

		// Token: 0x04000A32 RID: 2610
		private IPluralizationService _pluralizationService = DbConfiguration.DependencyResolver.GetService<IPluralizationService>();
	}
}
