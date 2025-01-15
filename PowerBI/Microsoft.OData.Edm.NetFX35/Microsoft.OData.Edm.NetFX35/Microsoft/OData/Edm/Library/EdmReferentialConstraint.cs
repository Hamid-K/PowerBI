using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000114 RID: 276
	public class EdmReferentialConstraint : IEdmReferentialConstraint
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x0000DC7A File Offset: 0x0000BE7A
		public EdmReferentialConstraint(IEnumerable<EdmReferentialConstraintPropertyPair> propertyPairs)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<EdmReferentialConstraintPropertyPair>>(propertyPairs, "propertyPairs");
			this.propertyPairs = Enumerable.ToList<EdmReferentialConstraintPropertyPair>(propertyPairs);
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000DC9A File Offset: 0x0000BE9A
		public IEnumerable<EdmReferentialConstraintPropertyPair> PropertyPairs
		{
			get
			{
				return this.propertyPairs;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
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

		// Token: 0x04000218 RID: 536
		private readonly IEnumerable<EdmReferentialConstraintPropertyPair> propertyPairs;
	}
}
