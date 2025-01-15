using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001E2 RID: 482
	internal sealed class EdmConceptualEntity : IConceptualEntity, IConceptualDisplayItem, IEquatable<IConceptualEntity>
	{
		// Token: 0x060016E3 RID: 5859 RVA: 0x0003F230 File Offset: 0x0003D430
		internal EdmConceptualEntity(EntitySet edmEntitySet, IReadOnlyList<IConceptualProperty> properties, IReadOnlyList<IConceptualHierarchy> hierarchies)
		{
			this._edmEntitySet = edmEntitySet;
			this._properties = properties;
			this._hierarchies = hierarchies;
			this._propertiesByEdmName = new Dictionary<string, IConceptualProperty>(EdmItem.IdentityComparer);
			this._propertiesByReferenceName = new Dictionary<string, IConceptualProperty>(EdmItem.ReferenceNameComparer);
			foreach (IConceptualProperty conceptualProperty in properties)
			{
				this._propertiesByEdmName.Add(conceptualProperty.EdmName, conceptualProperty);
				this._propertiesByReferenceName.Add(conceptualProperty.Name, conceptualProperty);
			}
			if (edmEntitySet.ElementType.IsKeyStable())
			{
				this._keyColumns = this.GetColumns(edmEntitySet.GetKeyFieldInstances());
			}
			else
			{
				this._keyColumns = Util.EmptyReadOnlyCollection<IConceptualColumn>();
			}
			IList<EdmField> list = edmEntitySet.GetDisplayKeyFields().Evaluate<EdmField>();
			if (list.Count >= 1)
			{
				this._defaultLabel = this.CheckedGetProperty(list[0]) as IConceptualColumn;
			}
			this._navigationProperties = new List<IConceptualNavigationProperty>();
			this._navigationPropertiesByEdmName = new Dictionary<string, IConceptualNavigationProperty>(EdmItem.IdentityComparer);
			this._conceptualType = edmEntitySet.ElementType.ConceptualType.Table();
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x0003F35C File Offset: 0x0003D55C
		public string Name
		{
			get
			{
				return this._edmEntitySet.ReferenceName;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x0003F369 File Offset: 0x0003D569
		public string EdmName
		{
			get
			{
				return this._edmEntitySet.Name;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x0003F376 File Offset: 0x0003D576
		public string DisplayName
		{
			get
			{
				return this._edmEntitySet.Caption;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0003F383 File Offset: 0x0003D583
		public string Description
		{
			get
			{
				return this._edmEntitySet.Description;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x0003F390 File Offset: 0x0003D590
		public string EntityContainerName
		{
			get
			{
				return this._edmEntitySet.InternalEntitySet.EntityContainer.Name;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0003F3A7 File Offset: 0x0003D5A7
		public IConceptualSchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0003F3AF File Offset: 0x0003D5AF
		public string Extends
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x0003F3B2 File Offset: 0x0003D5B2
		public IReadOnlyList<IConceptualProperty> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x0003F3BA File Offset: 0x0003D5BA
		public IReadOnlyList<IConceptualNavigationProperty> NavigationProperties
		{
			get
			{
				return this._navigationProperties;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0003F3C2 File Offset: 0x0003D5C2
		public IReadOnlyList<IConceptualHierarchy> Hierarchies
		{
			get
			{
				return this._hierarchies;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0003F3CA File Offset: 0x0003D5CA
		public IReadOnlyList<IConceptualDisplayFolder> DisplayFolders
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0003F3D4 File Offset: 0x0003D5D4
		public ConceptualEntityVisibilityType Visibility
		{
			get
			{
				ConceptualEntityVisibilityType conceptualEntityVisibilityType = ConceptualEntityVisibilityType.AlwaysVisible;
				if (this._edmEntitySet.Hidden)
				{
					conceptualEntityVisibilityType |= ConceptualEntityVisibilityType.Hidden;
				}
				if (this._edmEntitySet.ShowAsVariationsOnly)
				{
					conceptualEntityVisibilityType |= ConceptualEntityVisibilityType.ShowAsVariationsOnly;
				}
				if (this._edmEntitySet.Private)
				{
					conceptualEntityVisibilityType |= ConceptualEntityVisibilityType.Private;
				}
				return conceptualEntityVisibilityType;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0003F417 File Offset: 0x0003D617
		public bool IsDateTable
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0003F41E File Offset: 0x0003D61E
		public IConceptualColumn DefaultImageColumn
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0003F421 File Offset: 0x0003D621
		public IConceptualColumn DefaultLabelColumn
		{
			get
			{
				return this._defaultLabel;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0003F429 File Offset: 0x0003D629
		public IReadOnlyList<IConceptualProperty> DefaultFieldProperties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0003F430 File Offset: 0x0003D630
		public IReadOnlyList<IConceptualColumn> KeyColumns
		{
			get
			{
				return this._keyColumns;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0003F438 File Offset: 0x0003D638
		public ConceptualEntityStatistics Statistics
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x0003F43F File Offset: 0x0003D63F
		public ConceptualTableType ConceptualResultType
		{
			get
			{
				return this._conceptualType;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0003F447 File Offset: 0x0003D647
		public string StableName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0003F44A File Offset: 0x0003D64A
		public bool TryGetProperty(string referenceName, out IConceptualProperty conceptualProp)
		{
			return this._propertiesByReferenceName.TryGetValue(referenceName, out conceptualProp);
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0003F459 File Offset: 0x0003D659
		public bool TryGetPropertyByEdmName(string propName, out IConceptualProperty conceptualProp)
		{
			return this._propertiesByEdmName.TryGetValue(propName, out conceptualProp);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0003F468 File Offset: 0x0003D668
		public bool TryGetHierarchy(string name, out IConceptualHierarchy conceptualHierarchy)
		{
			for (int i = 0; i < this._hierarchies.Count; i++)
			{
				IConceptualHierarchy conceptualHierarchy2 = this._hierarchies[i];
				if (EdmItem.ReferenceNameComparer.Equals(conceptualHierarchy2.Name, name))
				{
					conceptualHierarchy = conceptualHierarchy2;
					return true;
				}
			}
			conceptualHierarchy = null;
			return false;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0003F4B4 File Offset: 0x0003D6B4
		public bool TryGetHierarchyByEdmName(string edmName, out IConceptualHierarchy conceptualHierarchy)
		{
			for (int i = 0; i < this._hierarchies.Count; i++)
			{
				IConceptualHierarchy conceptualHierarchy2 = this._hierarchies[i];
				if (EdmItem.IdentityComparer.Equals(conceptualHierarchy2.EdmName, edmName))
				{
					conceptualHierarchy = conceptualHierarchy2;
					return true;
				}
			}
			conceptualHierarchy = null;
			return false;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0003F500 File Offset: 0x0003D700
		public string GetFullName()
		{
			return this._edmEntitySet.FullName;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0003F512 File Offset: 0x0003D712
		public bool Equals(IConceptualEntity other)
		{
			return this == other;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0003F518 File Offset: 0x0003D718
		internal void CompleteInitialization(IConceptualSchema schema, IEnumerable<IConceptualNavigationProperty> navigationProperties)
		{
			this._schema = schema;
			foreach (IConceptualNavigationProperty conceptualNavigationProperty in navigationProperties)
			{
				this._navigationProperties.Add(conceptualNavigationProperty);
				this._navigationPropertiesByEdmName.Add(conceptualNavigationProperty.EdmName, conceptualNavigationProperty);
			}
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0003F580 File Offset: 0x0003D780
		internal bool TryGetNavigationPropertyByEdmName(string name, out IConceptualNavigationProperty navigationProperty)
		{
			return this._navigationPropertiesByEdmName.TryGetValue(name, out navigationProperty);
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0003F58F File Offset: 0x0003D78F
		private IConceptualProperty CheckedGetProperty(EdmProperty property)
		{
			return this._propertiesByEdmName[property.Name];
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0003F5A4 File Offset: 0x0003D7A4
		private IReadOnlyList<IConceptualColumn> GetColumns(IEnumerable<EdmFieldInstance> edmFields)
		{
			if (edmFields == null)
			{
				return Util.EmptyReadOnlyCollection<IConceptualColumn>();
			}
			List<IConceptualColumn> list = new List<IConceptualColumn>();
			foreach (EdmFieldInstance edmFieldInstance in edmFields)
			{
				IConceptualColumn conceptualColumn = this.CheckedGetProperty(edmFieldInstance.Field) as IConceptualColumn;
				if (conceptualColumn != null)
				{
					list.Add(conceptualColumn);
				}
			}
			return list;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0003F614 File Offset: 0x0003D814
		public override string ToString()
		{
			return this.GetFullName();
		}

		// Token: 0x04000C45 RID: 3141
		private readonly EntitySet _edmEntitySet;

		// Token: 0x04000C46 RID: 3142
		private readonly IReadOnlyList<IConceptualHierarchy> _hierarchies;

		// Token: 0x04000C47 RID: 3143
		private readonly IReadOnlyList<IConceptualProperty> _properties;

		// Token: 0x04000C48 RID: 3144
		private readonly IDictionary<string, IConceptualProperty> _propertiesByEdmName;

		// Token: 0x04000C49 RID: 3145
		private readonly IDictionary<string, IConceptualProperty> _propertiesByReferenceName;

		// Token: 0x04000C4A RID: 3146
		private readonly IReadOnlyList<IConceptualColumn> _keyColumns;

		// Token: 0x04000C4B RID: 3147
		private readonly IConceptualColumn _defaultLabel;

		// Token: 0x04000C4C RID: 3148
		private readonly List<IConceptualNavigationProperty> _navigationProperties;

		// Token: 0x04000C4D RID: 3149
		private readonly IDictionary<string, IConceptualNavigationProperty> _navigationPropertiesByEdmName;

		// Token: 0x04000C4E RID: 3150
		private readonly ConceptualTableType _conceptualType;

		// Token: 0x04000C4F RID: 3151
		private IConceptualSchema _schema;
	}
}
