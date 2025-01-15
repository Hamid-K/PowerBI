using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000189 RID: 393
	public class AttributeToTableAnnotationConvention<TAttribute, TAnnotation> : Convention where TAttribute : Attribute
	{
		// Token: 0x06001720 RID: 5920 RVA: 0x0003D878 File Offset: 0x0003BA78
		public AttributeToTableAnnotationConvention(string annotationName, Func<Type, IList<TAttribute>, TAnnotation> annotationFactory)
		{
			Check.NotEmpty(annotationName, "annotationName");
			Check.NotNull<Func<Type, IList<TAttribute>, TAnnotation>>(annotationFactory, "annotationFactory");
			AttributeProvider attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
			base.Types().Having<List<TAttribute>>((Type t) => attributeProvider.GetAttributes(t).OfType<TAttribute>().ToList<TAttribute>()).Configure(delegate(ConventionTypeConfiguration c, List<TAttribute> a)
			{
				if (a.Any<TAttribute>())
				{
					c.HasTableAnnotation(annotationName, annotationFactory(c.ClrType, a));
				}
			});
		}
	}
}
