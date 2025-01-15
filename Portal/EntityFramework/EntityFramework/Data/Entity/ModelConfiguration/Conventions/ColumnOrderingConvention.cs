using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001A0 RID: 416
	public class ColumnOrderingConvention : IStoreModelConvention<EntityType>, IConvention
	{
		// Token: 0x0600175C RID: 5980 RVA: 0x0003E904 File Offset: 0x0003CB04
		public virtual void Apply(EntityType item, DbModel model)
		{
			Check.NotNull<EntityType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			this.ValidateColumns(item, model.StoreModel.GetEntitySet(item).Table);
			ColumnOrderingConvention.OrderColumns(item.Properties).Each(delegate(EdmProperty c)
			{
				bool isPrimaryKeyColumn = c.IsPrimaryKeyColumn;
				item.RemoveMember(c);
				item.AddMember(c);
				if (isPrimaryKeyColumn)
				{
					item.AddKeyMember(c);
				}
			});
			item.ForeignKeyBuilders.Each((ForeignKeyBuilder fk) => fk.DependentColumns = ColumnOrderingConvention.OrderColumns(fk.DependentColumns));
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0003E9AD File Offset: 0x0003CBAD
		protected virtual void ValidateColumns(EntityType table, string tableName)
		{
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0003E9B0 File Offset: 0x0003CBB0
		private static IEnumerable<EdmProperty> OrderColumns(IEnumerable<EdmProperty> columns)
		{
			return (from c in columns
				select new
				{
					Column = c,
					Order = (c.GetOrder() ?? int.MaxValue)
				} into c
				orderby c.Order
				select c.Column).ToList<EdmProperty>();
		}
	}
}
