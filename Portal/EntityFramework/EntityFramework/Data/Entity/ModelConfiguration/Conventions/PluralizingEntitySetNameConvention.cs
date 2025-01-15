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
	// Token: 0x020001B1 RID: 433
	public class PluralizingEntitySetNameConvention : IConceptualModelConvention<EntitySet>, IConvention
	{
		// Token: 0x06001788 RID: 6024 RVA: 0x0003FADC File Offset: 0x0003DCDC
		public virtual void Apply(EntitySet item, DbModel model)
		{
			Check.NotNull<EntitySet>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.GetConfiguration() == null)
			{
				item.Name = model.ConceptualModel.GetEntitySets().Except(new EntitySet[] { item }).UniquifyName(PluralizingEntitySetNameConvention._pluralizationService.Pluralize(item.Name));
			}
		}

		// Token: 0x04000A36 RID: 2614
		private static readonly IPluralizationService _pluralizationService = DbConfiguration.DependencyResolver.GetService<IPluralizationService>();
	}
}
