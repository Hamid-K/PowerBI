using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000048 RID: 72
	public class EdmReferentialConstraint : IEdmReferentialConstraint
	{
		// Token: 0x06000176 RID: 374 RVA: 0x0000497C File Offset: 0x00002B7C
		public EdmReferentialConstraint(IEnumerable<EdmReferentialConstraintPropertyPair> propertyPairs)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<EdmReferentialConstraintPropertyPair>>(propertyPairs, "propertyPairs");
			this.propertyPairs = propertyPairs.ToList<EdmReferentialConstraintPropertyPair>();
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000499C File Offset: 0x00002B9C
		public IEnumerable<EdmReferentialConstraintPropertyPair> PropertyPairs
		{
			get
			{
				return this.propertyPairs;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000049A4 File Offset: 0x00002BA4
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
			return new EdmReferentialConstraint(list.Select((IEdmStructuralProperty d, int i) => new EdmReferentialConstraintPropertyPair(d, principalPropertyList[i])));
		}

		// Token: 0x0400008C RID: 140
		private readonly IEnumerable<EdmReferentialConstraintPropertyPair> propertyPairs;
	}
}
