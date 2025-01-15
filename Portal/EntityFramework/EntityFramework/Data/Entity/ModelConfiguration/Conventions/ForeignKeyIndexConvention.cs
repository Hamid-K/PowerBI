using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001A2 RID: 418
	public class ForeignKeyIndexConvention : IStoreModelConvention<AssociationType>, IConvention
	{
		// Token: 0x06001762 RID: 5986 RVA: 0x0003EAEC File Offset: 0x0003CCEC
		public virtual void Apply(AssociationType item, DbModel model)
		{
			Check.NotNull<AssociationType>(item, "item");
			if (item.Constraint == null)
			{
				return;
			}
			IEnumerable<ConsolidatedIndex> enumerable = ConsolidatedIndex.BuildIndexes(item.Name, item.Constraint.ToProperties.Select((EdmProperty p) => Tuple.Create<string, EdmProperty>(p.Name, p)));
			IEnumerable<string> dependentColumnNames = item.Constraint.ToProperties.Select((EdmProperty p) => p.Name);
			if (!enumerable.Any((ConsolidatedIndex c) => c.Columns.SequenceEqual(dependentColumnNames)))
			{
				string text = IndexOperation.BuildDefaultName(dependentColumnNames);
				int num = 0;
				foreach (EdmProperty edmProperty in item.Constraint.ToProperties)
				{
					IndexAnnotation indexAnnotation = new IndexAnnotation(new IndexAttribute(text, num++));
					object annotation = edmProperty.Annotations.GetAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:Index");
					if (annotation != null)
					{
						indexAnnotation = (IndexAnnotation)((IndexAnnotation)annotation).MergeWith(indexAnnotation);
					}
					edmProperty.AddAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:Index", indexAnnotation);
				}
			}
		}
	}
}
