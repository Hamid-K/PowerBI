using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200018E RID: 398
	public class IndexAttributeConvention : AttributeToColumnAnnotationConvention<IndexAttribute, IndexAnnotation>
	{
		// Token: 0x06001729 RID: 5929 RVA: 0x0003DAA3 File Offset: 0x0003BCA3
		public IndexAttributeConvention()
			: base("Index", (PropertyInfo p, IList<IndexAttribute> a) => new IndexAnnotation(p, a.OrderBy((IndexAttribute i) => i.ToString())))
		{
		}
	}
}
