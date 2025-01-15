using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001AF RID: 431
	public class OneToManyCascadeDeleteConvention : IConceptualModelConvention<AssociationType>, IConvention
	{
		// Token: 0x06001784 RID: 6020 RVA: 0x0003F91C File Offset: 0x0003DB1C
		public virtual void Apply(AssociationType item, DbModel model)
		{
			Check.NotNull<AssociationType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.IsSelfReferencing())
			{
				return;
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = item.GetConfiguration() as NavigationPropertyConfiguration;
			if (navigationPropertyConfiguration != null && navigationPropertyConfiguration.DeleteAction != null)
			{
				return;
			}
			AssociationEndMember associationEndMember = null;
			if (item.IsRequiredToMany())
			{
				associationEndMember = item.SourceEnd;
			}
			else if (item.IsManyToRequired())
			{
				associationEndMember = item.TargetEnd;
			}
			if (associationEndMember != null)
			{
				associationEndMember.DeleteBehavior = OperationAction.Cascade;
			}
		}
	}
}
