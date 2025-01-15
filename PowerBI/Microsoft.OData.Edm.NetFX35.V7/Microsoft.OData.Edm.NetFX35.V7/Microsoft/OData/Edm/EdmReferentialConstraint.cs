using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000072 RID: 114
	public class EdmReferentialConstraint : IEdmReferentialConstraint
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000C07D File Offset: 0x0000A27D
		public EdmReferentialConstraint(IEnumerable<EdmReferentialConstraintPropertyPair> propertyPairs)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<EdmReferentialConstraintPropertyPair>>(propertyPairs, "propertyPairs");
			this.propertyPairs = Enumerable.ToList<EdmReferentialConstraintPropertyPair>(propertyPairs);
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000C09D File Offset: 0x0000A29D
		public IEnumerable<EdmReferentialConstraintPropertyPair> PropertyPairs
		{
			get
			{
				return this.propertyPairs;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		public static EdmReferentialConstraint Create(IEnumerable<IEdmStructuralProperty> dependentProperties, IEnumerable<IEdmStructuralProperty> principalProperties)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmStructuralProperty>>(dependentProperties, "dependentProperties");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmStructuralProperty>>(principalProperties, "principalProperties");
			List<IEdmStructuralProperty> list = new List<IEdmStructuralProperty>(dependentProperties);
			List<IEdmStructuralProperty> principalPropertyList = new List<IEdmStructuralProperty>(principalProperties);
			if (list.Count != principalPropertyList.Count)
			{
				throw new ArgumentException(Strings.Constructable_DependentPropertyCountMustMatchNumberOfPropertiesOnPrincipalType(principalPropertyList.Count, list.Count));
			}
			return new EdmReferentialConstraint(Enumerable.Select<IEdmStructuralProperty, EdmReferentialConstraintPropertyPair>(list, (IEdmStructuralProperty d, int i) => new EdmReferentialConstraintPropertyPair(d, principalPropertyList[i])));
		}

		// Token: 0x040000FC RID: 252
		private readonly IEnumerable<EdmReferentialConstraintPropertyPair> propertyPairs;
	}
}
