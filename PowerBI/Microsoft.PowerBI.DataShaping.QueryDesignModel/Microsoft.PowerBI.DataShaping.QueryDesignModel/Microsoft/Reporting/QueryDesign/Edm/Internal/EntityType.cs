using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.Edm.ExtendedProperties.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000240 RID: 576
	public sealed class EntityType : StructuralType, ISupportsDisplayFolders
	{
		// Token: 0x06001955 RID: 6485 RVA: 0x00044FD8 File Offset: 0x000431D8
		private EntityType(EntityType entityType)
		{
			this._entityType = ArgumentValidation.CheckNotNull<EntityType>(entityType, "entityType");
			XElement xelementMetadataProperty = this.GetXElementMetadataProperty("http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:EntityType");
			this._contents = xelementMetadataProperty.GetEnumAttributeOrNull(Extensions.ContentsAttr);
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00045019 File Offset: 0x00043219
		public ReadOnlyCollection<EdmField> KeyFields
		{
			get
			{
				return this._keyFields;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x00045021 File Offset: 0x00043221
		public ReadOnlyCollection<EdmProperty> DisplayKey
		{
			get
			{
				return this._displayKey;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x00045029 File Offset: 0x00043229
		public ReadOnlyCollection<EdmProperty> DefaultDetails
		{
			get
			{
				return this._defaultDetails;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x00045031 File Offset: 0x00043231
		// (set) Token: 0x0600195A RID: 6490 RVA: 0x00045039 File Offset: 0x00043239
		public EdmField DefaultImage
		{
			get
			{
				return this._defaultImage;
			}
			private set
			{
				ArgumentValidation.CheckCondition(value == null || (value.DeclaringType == this && value.IsImageField()), SR.DefaultImageFieldIsNotAnImage(value.Name.MarkAsModelInfo()));
				this._defaultImage = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0004506F File Offset: 0x0004326F
		public EdmHierarchyCollection Hierarchies
		{
			get
			{
				return this._hierarchies;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x00045077 File Offset: 0x00043277
		internal EntityType InternalEntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0004507F File Offset: 0x0004327F
		internal sealed override StructuralType InternalStructuralType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00045087 File Offset: 0x00043287
		public EntityContentType? Contents
		{
			[DebuggerStepThrough]
			get
			{
				return this._contents;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0004508F File Offset: 0x0004328F
		public EdmDisplayFolderCollection DisplayFolders
		{
			get
			{
				return this._displayFolders;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x00045097 File Offset: 0x00043297
		public ReadOnlyCollection<EdmNavigationProperty> NavigationProperties
		{
			get
			{
				return this._navigationProperties;
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000450A0 File Offset: 0x000432A0
		internal override void InternalInit(Version version)
		{
			base.InternalInit(version);
			EdmField[] array = new EdmField[this._entityType.KeyMembers.Count];
			for (int i = 0; i < array.Length; i++)
			{
				EdmMember keyProp = this._entityType.KeyMembers[i];
				array[i] = base.Fields.Single((EdmField f) => f.InternalEdmProperty == keyProp);
			}
			if (array.Length == 1 && array[0].Stability == Stability.RowNumber)
			{
				this._keyFields = Util.EmptyReadOnlyCollection<EdmField>();
				this._isKeyStable = new bool?(false);
			}
			else
			{
				this._keyFields = array.ToReadOnlyCollection<EdmField>();
			}
			XElement xelementMetadataProperty = this.GetXElementMetadataProperty("http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:EntityType");
			this._displayKey = this.GetReadOnlyMemberCollection<EdmProperty>(xelementMetadataProperty, Extensions.DisplayKeyElem);
			XElement elementOrNull = xelementMetadataProperty.GetElementOrNull(Extensions.DefaultImageElem);
			if (elementOrNull != null)
			{
				this.DefaultImage = this.GetMemberFromMemberRef(elementOrNull.Element(Extensions.MemberRefElem)) as EdmField;
			}
			foreach (EdmField edmField in base.Fields)
			{
				edmField.Grouping.CompleteInitialization();
			}
			this._defaultDetails = this.GetReadOnlyMemberCollection<EdmProperty>(xelementMetadataProperty, Extensions.DefaultDetailsElem);
			List<EdmHierarchy> list = new List<EdmHierarchy>();
			if (version > EntityDataModel.VersionOnePointZero && xelementMetadataProperty != null)
			{
				IEnumerable<XElement> enumerable = xelementMetadataProperty.Elements(Extensions.HierarchyElem);
				if (enumerable != null)
				{
					foreach (XElement xelement in enumerable)
					{
						list.Add(new EdmHierarchy(xelement, this));
					}
				}
			}
			this._hierarchies = new EdmHierarchyCollection(list);
			List<EdmDisplayFolder> list2 = new List<EdmDisplayFolder>();
			XElement elementOrNull2 = xelementMetadataProperty.GetElementOrNull(Extensions.DisplayFoldersElem);
			if (elementOrNull2 != null)
			{
				foreach (XElement xelement2 in elementOrNull2.Elements(Extensions.DisplayFolderElem))
				{
					list2.Add(new EdmDisplayFolder(xelement2, this));
				}
			}
			this._displayFolders = new EdmDisplayFolderCollection(list2);
			List<EdmNavigationProperty> list3 = null;
			if (this._entityType.NavigationProperties != null)
			{
				list3 = new List<EdmNavigationProperty>(this._entityType.NavigationProperties.Count);
				foreach (NavigationProperty navigationProperty in this._entityType.NavigationProperties)
				{
					XElement xelementMetadataProperty2 = navigationProperty.GetXElementMetadataProperty("http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:NavigationProperty");
					list3.Add(new EdmNavigationProperty(navigationProperty, xelementMetadataProperty2));
				}
			}
			this._navigationProperties = list3.AsReadOnlyCollection<EdmNavigationProperty>();
			this._conceptualType = this.CreateConceptualType();
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x00045380 File Offset: 0x00043580
		private ReadOnlyCollection<T> GetReadOnlyMemberCollection<T>(XElement extensionElem, XName collectionElemName) where T : EdmMember
		{
			IEnumerable<T> enumerable = null;
			XElement elementOrNull = extensionElem.GetElementOrNull(collectionElemName);
			if (elementOrNull != null)
			{
				enumerable = from e in elementOrNull.Elements(Extensions.MemberRefElem)
					select (T)((object)this.GetMemberFromMemberRef(e));
			}
			return enumerable.ToReadOnlyCollection<T>();
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000453C0 File Offset: 0x000435C0
		internal IEntityMemberItem GetEntityMemberItemFromRef(XElement memberRef)
		{
			ArgumentValidation.CheckNotNull<XElement>(memberRef, "memberRef");
			if (memberRef.Name == Extensions.PropertyRefElem || memberRef.Name == Extensions.KpiRefElem)
			{
				return this.GetMemberFromMemberRef(memberRef);
			}
			if (memberRef.Name == Extensions.HierarchyRefElem)
			{
				return this.GetHierarchyFromHierarchyRef(memberRef);
			}
			throw new ArgumentException(DevErrors.EntityType.UnsupportedElementType(memberRef.Name.ToString().MarkAsModelInfo()));
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0004543C File Offset: 0x0004363C
		internal EdmMember GetMemberFromMemberRef(XElement memberRef)
		{
			ArgumentValidation.CheckNotNull<XElement>(memberRef, "memberRef");
			XAttribute xattribute = memberRef.Attribute(Extensions.NameAttr);
			ArgumentValidation.CheckCondition(xattribute != null, "memberRef");
			EdmMember edmMember = base.Members[xattribute.Value];
			if (edmMember == null)
			{
				throw new ArgumentException(DevErrors.EntityType.UnknownMemberReference(xattribute.Value));
			}
			return edmMember;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00045494 File Offset: 0x00043694
		internal EdmHierarchy GetHierarchyFromHierarchyRef(XElement hierarchyRef)
		{
			ArgumentValidation.CheckNotNull<XElement>(hierarchyRef, "hierarchyRef");
			XAttribute xattribute = hierarchyRef.Attribute(Extensions.NameAttr);
			ArgumentValidation.CheckCondition(xattribute != null, "hierarchyRef");
			EdmHierarchy edmHierarchy = this.Hierarchies[xattribute.Value];
			if (edmHierarchy == null)
			{
				throw new ArgumentException(DevErrors.EntityType.UnknownHierarchyReference(xattribute.Value));
			}
			return edmHierarchy;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x000454EC File Offset: 0x000436EC
		internal static EntityType Create(EntityType entityType, Version version)
		{
			EntityType entityType2 = new EntityType(entityType);
			entityType2.InternalInit(version);
			return entityType2;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x000454FC File Offset: 0x000436FC
		public bool IsKeyStable()
		{
			bool? isKeyStable = this._isKeyStable;
			if (isKeyStable == null)
			{
				IEnumerable<EdmField> keyFields = this._keyFields;
				Func<EdmField, bool> func;
				if ((func = EntityType.<>O.<0>__IsStable) == null)
				{
					func = (EntityType.<>O.<0>__IsStable = new Func<EdmField, bool>(Extensions.IsStable));
				}
				return keyFields.All(func);
			}
			return isKeyStable.GetValueOrDefault();
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00045548 File Offset: 0x00043748
		private ConceptualRowType CreateConceptualType()
		{
			List<ConceptualTypeColumn> list = new List<ConceptualTypeColumn>(base.Fields.Count);
			foreach (EdmField edmField in base.Fields)
			{
				list.Add(edmField.Column);
			}
			return new ConceptualRowType(list);
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x000455B4 File Offset: 0x000437B4
		internal ConceptualRowType ConceptualType
		{
			get
			{
				return this._conceptualType;
			}
		}

		// Token: 0x04000DCD RID: 3533
		private readonly EntityType _entityType;

		// Token: 0x04000DCE RID: 3534
		private readonly EntityContentType? _contents;

		// Token: 0x04000DCF RID: 3535
		private ReadOnlyCollection<EdmField> _keyFields;

		// Token: 0x04000DD0 RID: 3536
		private ReadOnlyCollection<EdmProperty> _displayKey;

		// Token: 0x04000DD1 RID: 3537
		private ReadOnlyCollection<EdmProperty> _defaultDetails;

		// Token: 0x04000DD2 RID: 3538
		private EdmHierarchyCollection _hierarchies;

		// Token: 0x04000DD3 RID: 3539
		private ReadOnlyCollection<EdmNavigationProperty> _navigationProperties;

		// Token: 0x04000DD4 RID: 3540
		private EdmDisplayFolderCollection _displayFolders;

		// Token: 0x04000DD5 RID: 3541
		private EdmField _defaultImage;

		// Token: 0x04000DD6 RID: 3542
		private ConceptualRowType _conceptualType;

		// Token: 0x04000DD7 RID: 3543
		private bool? _isKeyStable;

		// Token: 0x020003D6 RID: 982
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040013DA RID: 5082
			public static Func<EdmField, bool> <0>__IsStable;
		}
	}
}
