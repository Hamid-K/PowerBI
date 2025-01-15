using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000188 RID: 392
	public class AttributeToColumnAnnotationConvention<TAttribute, TAnnotation> : Convention where TAttribute : Attribute
	{
		// Token: 0x0600171F RID: 5919 RVA: 0x0003D7F0 File Offset: 0x0003B9F0
		public AttributeToColumnAnnotationConvention(string annotationName, Func<PropertyInfo, IList<TAttribute>, TAnnotation> annotationFactory)
		{
			Check.NotEmpty(annotationName, "annotationName");
			Check.NotNull<Func<PropertyInfo, IList<TAttribute>, TAnnotation>>(annotationFactory, "annotationFactory");
			AttributeProvider attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
			base.Properties().Having<List<TAttribute>>((PropertyInfo pi) => attributeProvider.GetAttributes(pi).OfType<TAttribute>().ToList<TAttribute>()).Configure(delegate(ConventionPrimitivePropertyConfiguration c, List<TAttribute> a)
			{
				if (a.Any<TAttribute>())
				{
					c.HasColumnAnnotation(annotationName, annotationFactory(c.ClrPropertyInfo, a));
				}
			});
		}
	}
}
