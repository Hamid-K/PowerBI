using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Mappers;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001A8 RID: 424
	public class DeclaredPropertyOrderingConvention : IConceptualModelConvention<EntityType>, IConvention
	{
		// Token: 0x06001771 RID: 6001 RVA: 0x0003F0B8 File Offset: 0x0003D2B8
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.BaseType == null)
			{
				foreach (EdmProperty edmProperty in item.KeyProperties)
				{
					item.RemoveMember(edmProperty);
					item.AddKeyMember(edmProperty);
				}
				using (IEnumerator<PropertyInfo> enumerator2 = new PropertyFilter(DbModelBuilderVersion.Latest).GetProperties(item.GetClrType(), false, null, null, true).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						PropertyInfo p = enumerator2.Current;
						EdmProperty edmProperty2 = item.DeclaredProperties.SingleOrDefault((EdmProperty ep) => ep.Name == p.Name);
						if (edmProperty2 != null && !item.KeyProperties.Contains(edmProperty2))
						{
							item.RemoveMember(edmProperty2);
							item.AddMember(edmProperty2);
						}
					}
				}
			}
		}
	}
}
