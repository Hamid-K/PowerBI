using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004D9 RID: 1241
	public abstract class MetadataItem
	{
		// Token: 0x06003D9C RID: 15772 RVA: 0x000CBDAC File Offset: 0x000C9FAC
		internal MetadataItem()
		{
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x000CBDB4 File Offset: 0x000C9FB4
		internal MetadataItem(MetadataItem.MetadataFlags flags)
		{
			this._flags = (int)flags;
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06003D9E RID: 15774 RVA: 0x000CBDC3 File Offset: 0x000C9FC3
		internal virtual IEnumerable<MetadataProperty> Annotations
		{
			get
			{
				return from p in this.GetMetadataProperties()
					where p.IsAnnotation
					select p;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06003D9F RID: 15775
		public abstract BuiltInTypeKind BuiltInTypeKind { get; }

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06003DA0 RID: 15776 RVA: 0x000CBDEF File Offset: 0x000C9FEF
		[MetadataProperty(BuiltInTypeKind.MetadataProperty, true)]
		public virtual ReadOnlyMetadataCollection<MetadataProperty> MetadataProperties
		{
			get
			{
				return this.GetMetadataProperties().AsReadOnlyMetadataCollection();
			}
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x000CBDFC File Offset: 0x000C9FFC
		internal MetadataPropertyCollection GetMetadataProperties()
		{
			if (this._itemAttributes == null)
			{
				MetadataPropertyCollection metadataPropertyCollection = new MetadataPropertyCollection(this);
				if (this.IsReadOnly)
				{
					metadataPropertyCollection.SetReadOnly();
				}
				Interlocked.CompareExchange<MetadataPropertyCollection>(ref this._itemAttributes, metadataPropertyCollection, null);
			}
			return this._itemAttributes;
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x000CBE3C File Offset: 0x000CA03C
		public void AddAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			MetadataProperty metadataProperty = this.Annotations.FirstOrDefault((MetadataProperty a) => a.Name == name);
			if (metadataProperty == null)
			{
				if (value != null)
				{
					this.GetMetadataProperties().Add(MetadataProperty.CreateAnnotation(name, value));
				}
				return;
			}
			if (value == null)
			{
				this.RemoveAnnotation(name);
				return;
			}
			metadataProperty.Value = value;
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x000CBEB8 File Offset: 0x000CA0B8
		public bool RemoveAnnotation(string name)
		{
			Check.NotEmpty(name, "name");
			MetadataPropertyCollection metadataProperties = this.GetMetadataProperties();
			MetadataProperty metadataProperty;
			return metadataProperties.TryGetValue(name, false, out metadataProperty) && metadataProperties.Remove(metadataProperty);
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x000CBEED File Offset: 0x000CA0ED
		internal MetadataCollection<MetadataProperty> RawMetadataProperties
		{
			get
			{
				return this._itemAttributes;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06003DA5 RID: 15781 RVA: 0x000CBEF5 File Offset: 0x000CA0F5
		// (set) Token: 0x06003DA6 RID: 15782 RVA: 0x000CBEFD File Offset: 0x000CA0FD
		public Documentation Documentation { get; set; }

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06003DA7 RID: 15783
		internal abstract string Identity { get; }

		// Token: 0x06003DA8 RID: 15784 RVA: 0x000CBF06 File Offset: 0x000CA106
		internal virtual bool EdmEquals(MetadataItem item)
		{
			return item != null && (this == item || (this.BuiltInTypeKind == item.BuiltInTypeKind && this.Identity == item.Identity));
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x000CBF34 File Offset: 0x000CA134
		internal bool IsReadOnly
		{
			get
			{
				return this.GetFlag(MetadataItem.MetadataFlags.Readonly);
			}
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x000CBF3D File Offset: 0x000CA13D
		internal virtual void SetReadOnly()
		{
			if (!this.IsReadOnly)
			{
				if (this._itemAttributes != null)
				{
					this._itemAttributes.SetReadOnly();
				}
				this.SetFlag(MetadataItem.MetadataFlags.Readonly, true);
			}
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x000CBF63 File Offset: 0x000CA163
		internal virtual void BuildIdentity(StringBuilder builder)
		{
			builder.Append(this.Identity);
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x000CBF72 File Offset: 0x000CA172
		internal void AddMetadataProperties(IEnumerable<MetadataProperty> metadataProperties)
		{
			this.GetMetadataProperties().AddRange(metadataProperties);
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x000CBF80 File Offset: 0x000CA180
		internal DataSpace GetDataSpace()
		{
			switch (this._flags & 7)
			{
			case 1:
				return DataSpace.CSpace;
			case 2:
				return DataSpace.OSpace;
			case 3:
				return DataSpace.OCSpace;
			case 4:
				return DataSpace.SSpace;
			case 5:
				return DataSpace.CSSpace;
			default:
				return (DataSpace)(-1);
			}
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x000CBFBD File Offset: 0x000CA1BD
		internal void SetDataSpace(DataSpace space)
		{
			this._flags = (this._flags & -8) | (int)(MetadataItem.MetadataFlags.DataSpace & MetadataItem.Convert(space));
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x000CBFD7 File Offset: 0x000CA1D7
		private static MetadataItem.MetadataFlags Convert(DataSpace space)
		{
			switch (space)
			{
			case DataSpace.OSpace:
				return MetadataItem.MetadataFlags.OSpace;
			case DataSpace.CSpace:
				return MetadataItem.MetadataFlags.CSpace;
			case DataSpace.SSpace:
				return MetadataItem.MetadataFlags.SSpace;
			case DataSpace.OCSpace:
				return MetadataItem.MetadataFlags.OCSpace;
			case DataSpace.CSSpace:
				return MetadataItem.MetadataFlags.CSSpace;
			default:
				return MetadataItem.MetadataFlags.None;
			}
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x000CC000 File Offset: 0x000CA200
		internal ParameterMode GetParameterMode()
		{
			MetadataItem.MetadataFlags metadataFlags = (MetadataItem.MetadataFlags)(this._flags & 3584);
			if (metadataFlags <= MetadataItem.MetadataFlags.Out)
			{
				if (metadataFlags == MetadataItem.MetadataFlags.In)
				{
					return ParameterMode.In;
				}
				if (metadataFlags == MetadataItem.MetadataFlags.Out)
				{
					return ParameterMode.Out;
				}
			}
			else
			{
				if (metadataFlags == MetadataItem.MetadataFlags.InOut)
				{
					return ParameterMode.InOut;
				}
				if (metadataFlags == MetadataItem.MetadataFlags.ReturnValue)
				{
					return ParameterMode.ReturnValue;
				}
			}
			return (ParameterMode)(-1);
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x000CC04D File Offset: 0x000CA24D
		internal void SetParameterMode(ParameterMode mode)
		{
			this._flags = (this._flags & -3585) | (int)(MetadataItem.MetadataFlags.ParameterMode & MetadataItem.Convert(mode));
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000CC06E File Offset: 0x000CA26E
		private static MetadataItem.MetadataFlags Convert(ParameterMode mode)
		{
			switch (mode)
			{
			case ParameterMode.In:
				return MetadataItem.MetadataFlags.In;
			case ParameterMode.Out:
				return MetadataItem.MetadataFlags.Out;
			case ParameterMode.InOut:
				return MetadataItem.MetadataFlags.InOut;
			case ParameterMode.ReturnValue:
				return MetadataItem.MetadataFlags.ReturnValue;
			default:
				return MetadataItem.MetadataFlags.ParameterMode;
			}
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000CC0A3 File Offset: 0x000CA2A3
		internal bool GetFlag(MetadataItem.MetadataFlags flag)
		{
			return flag == (MetadataItem.MetadataFlags)(this._flags & (int)flag);
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000CC0B0 File Offset: 0x000CA2B0
		internal void SetFlag(MetadataItem.MetadataFlags flag, bool value)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int flags = this._flags;
				int num = (value ? (flags | (int)flag) : (flags & (int)(~(int)flag)));
				if ((flags & 8) == 8)
				{
					break;
				}
				if (flags == Interlocked.CompareExchange(ref this._flags, num, flags))
				{
					return;
				}
				spinWait.SpinOnce();
			}
			if ((flag & MetadataItem.MetadataFlags.Readonly) == MetadataItem.MetadataFlags.Readonly)
			{
				return;
			}
			throw new InvalidOperationException(Strings.OperationOnReadOnlyItem);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000CC10C File Offset: 0x000CA30C
		static MetadataItem()
		{
			MetadataItem._builtInTypes[0] = new ComplexType();
			MetadataItem._builtInTypes[2] = new ComplexType();
			MetadataItem._builtInTypes[1] = new ComplexType();
			MetadataItem._builtInTypes[3] = new ComplexType();
			MetadataItem._builtInTypes[3] = new ComplexType();
			MetadataItem._builtInTypes[7] = new EnumType();
			MetadataItem._builtInTypes[6] = new ComplexType();
			MetadataItem._builtInTypes[8] = new ComplexType();
			MetadataItem._builtInTypes[9] = new ComplexType();
			MetadataItem._builtInTypes[10] = new EnumType();
			MetadataItem._builtInTypes[11] = new ComplexType();
			MetadataItem._builtInTypes[12] = new ComplexType();
			MetadataItem._builtInTypes[13] = new ComplexType();
			MetadataItem._builtInTypes[14] = new ComplexType();
			MetadataItem._builtInTypes[4] = new ComplexType();
			MetadataItem._builtInTypes[5] = new ComplexType();
			MetadataItem._builtInTypes[15] = new ComplexType();
			MetadataItem._builtInTypes[16] = new ComplexType();
			MetadataItem._builtInTypes[17] = new ComplexType();
			MetadataItem._builtInTypes[18] = new ComplexType();
			MetadataItem._builtInTypes[19] = new ComplexType();
			MetadataItem._builtInTypes[20] = new ComplexType();
			MetadataItem._builtInTypes[21] = new ComplexType();
			MetadataItem._builtInTypes[22] = new ComplexType();
			MetadataItem._builtInTypes[23] = new ComplexType();
			MetadataItem._builtInTypes[24] = new ComplexType();
			MetadataItem._builtInTypes[25] = new EnumType();
			MetadataItem._builtInTypes[26] = new ComplexType();
			MetadataItem._builtInTypes[27] = new EnumType();
			MetadataItem._builtInTypes[28] = new ComplexType();
			MetadataItem._builtInTypes[29] = new ComplexType();
			MetadataItem._builtInTypes[30] = new ComplexType();
			MetadataItem._builtInTypes[31] = new ComplexType();
			MetadataItem._builtInTypes[32] = new ComplexType();
			MetadataItem._builtInTypes[33] = new EnumType();
			MetadataItem._builtInTypes[34] = new ComplexType();
			MetadataItem._builtInTypes[35] = new ComplexType();
			MetadataItem._builtInTypes[36] = new ComplexType();
			MetadataItem._builtInTypes[37] = new ComplexType();
			MetadataItem._builtInTypes[38] = new ComplexType();
			MetadataItem._builtInTypes[39] = new ComplexType();
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem), "ItemType", false, null);
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataProperty), "MetadataProperty", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.GlobalItem), "GlobalItem", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.TypeUsage), "TypeUsage", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType), "EdmType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.GlobalItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.SimpleType), "SimpleType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EnumType), "EnumType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.SimpleType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.PrimitiveType), "PrimitiveType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.SimpleType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.CollectionType), "CollectionType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RefType), "RefType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember), "EdmMember", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmProperty), "EdmProperty", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.NavigationProperty), "NavigationProperty", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.ProviderManifest), "ProviderManifest", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipEndMember), "RelationshipEnd", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationEndMember), "AssociationEnd", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipEndMember));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EnumMember), "EnumMember", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.ReferentialConstraint), "ReferentialConstraint", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.StructuralType), "StructuralType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RowType), "RowType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.StructuralType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.ComplexType), "ComplexType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.StructuralType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityTypeBase), "ElementType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.StructuralType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityType), "EntityType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityTypeBase));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipType), "RelationshipType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityTypeBase));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationType), "AssociationType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.Facet), "Facet", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityContainer), "EntityContainerType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.GlobalItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySetBase), "BaseEntitySetType", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySet), "EntitySetType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySetBase));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipSet), "RelationshipSet", true, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySetBase));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationSet), "AssociationSetType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipSet));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationSetEnd), "AssociationSetEndType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.FunctionParameter), "FunctionParameter", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmFunction), "EdmFunction", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.Documentation), "Documentation", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeEnumType(BuiltInTypeKind.OperationAction, "DeleteAction", new string[] { "None", "Cascade" });
			MetadataItem.InitializeEnumType(BuiltInTypeKind.RelationshipMultiplicity, "RelationshipMultiplicity", new string[] { "One", "ZeroToOne", "Many" });
			MetadataItem.InitializeEnumType(BuiltInTypeKind.ParameterMode, "ParameterMode", new string[] { "In", "Out", "InOut" });
			MetadataItem.InitializeEnumType(BuiltInTypeKind.CollectionKind, "CollectionKind", new string[] { "None", "List", "Bag" });
			MetadataItem.InitializeEnumType(BuiltInTypeKind.PrimitiveTypeKind, "PrimitiveTypeKind", Enum.GetNames(typeof(PrimitiveTypeKind)));
			FacetDescription[] array = new FacetDescription[2];
			MetadataItem._nullableFacetDescription = new FacetDescription("Nullable", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, true);
			array[0] = MetadataItem._nullableFacetDescription;
			MetadataItem._defaultValueFacetDescription = new FacetDescription("DefaultValue", MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType), null, null, null);
			array[1] = MetadataItem._defaultValueFacetDescription;
			MetadataItem._generalFacetDescriptions = new ReadOnlyCollection<FacetDescription>(array);
			MetadataItem._collectionKindFacetDescription = new FacetDescription("CollectionKind", MetadataItem.GetBuiltInType(BuiltInTypeKind.EnumType), null, null, null);
			TypeUsage typeUsage = TypeUsage.Create(MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.String));
			TypeUsage typeUsage2 = TypeUsage.Create(MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean));
			TypeUsage typeUsage3 = TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			TypeUsage typeUsage4 = TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.TypeUsage));
			TypeUsage typeUsage5 = TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.ComplexType));
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.MetadataProperty, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("TypeUsage", typeUsage4),
				new EdmProperty("Value", typeUsage5)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.MetadataItem, new EdmProperty[]
			{
				new EdmProperty("MetadataProperties", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataProperty).GetCollectionType())),
				new EdmProperty("Documentation", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.Documentation)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.TypeUsage, new EdmProperty[]
			{
				new EdmProperty("EdmType", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType))),
				new EdmProperty("Facets", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.Facet)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EdmType, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("Namespace", typeUsage),
				new EdmProperty("Abstract", typeUsage2),
				new EdmProperty("Sealed", typeUsage2),
				new EdmProperty("BaseType", typeUsage5)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EnumType, new EdmProperty[]
			{
				new EdmProperty("EnumMembers", typeUsage)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.CollectionType, new EdmProperty[]
			{
				new EdmProperty("TypeUsage", typeUsage4)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.RefType, new EdmProperty[]
			{
				new EdmProperty("EntityType", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityType)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EdmMember, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("TypeUsage", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.TypeUsage)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EdmProperty, new EdmProperty[]
			{
				new EdmProperty("Nullable", typeUsage),
				new EdmProperty("DefaultValue", typeUsage5)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.NavigationProperty, new EdmProperty[]
			{
				new EdmProperty("RelationshipTypeName", typeUsage),
				new EdmProperty("ToEndMemberName", typeUsage)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.RelationshipEndMember, new EdmProperty[]
			{
				new EdmProperty("OperationBehaviors", typeUsage5),
				new EdmProperty("RelationshipMultiplicity", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EnumType)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EnumMember, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.ReferentialConstraint, new EdmProperty[]
			{
				new EdmProperty("ToRole", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipEndMember))),
				new EdmProperty("FromRole", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipEndMember))),
				new EdmProperty("ToProperties", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmProperty).GetCollectionType())),
				new EdmProperty("FromProperties", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmProperty).GetCollectionType()))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.StructuralType, new EdmProperty[]
			{
				new EdmProperty("Members", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EntityTypeBase, new EdmProperty[]
			{
				new EdmProperty("KeyMembers", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmMember)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.Facet, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("EdmType", typeUsage3),
				new EdmProperty("Value", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EntityContainer, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("EntitySets", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySet)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EntitySetBase, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("EntityType", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EntityType))),
				new EdmProperty("Schema", typeUsage),
				new EdmProperty("Table", typeUsage)
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.AssociationSet, new EdmProperty[]
			{
				new EdmProperty("AssociationSetEnds", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationSetEnd).GetCollectionType()))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.AssociationSetEnd, new EdmProperty[]
			{
				new EdmProperty("Role", typeUsage),
				new EdmProperty("EntitySetType", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EntitySet)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.FunctionParameter, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("Mode", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.EnumType))),
				new EdmProperty("TypeUsage", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.TypeUsage)))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.EdmFunction, new EdmProperty[]
			{
				new EdmProperty("Name", typeUsage),
				new EdmProperty("Namespace", typeUsage),
				new EdmProperty("ReturnParameter", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.FunctionParameter))),
				new EdmProperty("Parameters", TypeUsage.Create(MetadataItem.GetBuiltInType(BuiltInTypeKind.FunctionParameter).GetCollectionType()))
			});
			MetadataItem.AddBuiltInTypeProperties(BuiltInTypeKind.Documentation, new EdmProperty[]
			{
				new EdmProperty("Summary", typeUsage),
				new EdmProperty("LongDescription", typeUsage)
			});
			for (int i = 0; i < MetadataItem._builtInTypes.Length; i++)
			{
				MetadataItem._builtInTypes[i].SetReadOnly();
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06003DB6 RID: 15798 RVA: 0x000CCEB1 File Offset: 0x000CB0B1
		internal static FacetDescription DefaultValueFacetDescription
		{
			get
			{
				return MetadataItem._defaultValueFacetDescription;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06003DB7 RID: 15799 RVA: 0x000CCEB8 File Offset: 0x000CB0B8
		internal static FacetDescription CollectionKindFacetDescription
		{
			get
			{
				return MetadataItem._collectionKindFacetDescription;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06003DB8 RID: 15800 RVA: 0x000CCEBF File Offset: 0x000CB0BF
		internal static FacetDescription NullableFacetDescription
		{
			get
			{
				return MetadataItem._nullableFacetDescription;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06003DB9 RID: 15801 RVA: 0x000CCEC6 File Offset: 0x000CB0C6
		internal static EdmProviderManifest EdmProviderManifest
		{
			get
			{
				return EdmProviderManifest.Instance;
			}
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x000CCECD File Offset: 0x000CB0CD
		public static EdmType GetBuiltInType(BuiltInTypeKind builtInTypeKind)
		{
			return MetadataItem._builtInTypes[(int)builtInTypeKind];
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x000CCED6 File Offset: 0x000CB0D6
		public static ReadOnlyCollection<FacetDescription> GetGeneralFacetDescriptions()
		{
			return MetadataItem._generalFacetDescriptions;
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x000CCEDD File Offset: 0x000CB0DD
		private static void InitializeBuiltInTypes(ComplexType builtInType, string name, bool isAbstract, ComplexType baseType)
		{
			EdmType.Initialize(builtInType, name, "Edm", DataSpace.CSpace, isAbstract, baseType);
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x000CCEF0 File Offset: 0x000CB0F0
		private static void AddBuiltInTypeProperties(BuiltInTypeKind builtInTypeKind, EdmProperty[] properties)
		{
			ComplexType complexType = (ComplexType)MetadataItem.GetBuiltInType(builtInTypeKind);
			if (properties != null)
			{
				for (int i = 0; i < properties.Length; i++)
				{
					complexType.AddMember(properties[i]);
				}
			}
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x000CCF24 File Offset: 0x000CB124
		private static void InitializeEnumType(BuiltInTypeKind builtInTypeKind, string name, string[] enumMemberNames)
		{
			EnumType enumType = (EnumType)MetadataItem.GetBuiltInType(builtInTypeKind);
			EdmType.Initialize(enumType, name, "Edm", DataSpace.CSpace, false, null);
			for (int i = 0; i < enumMemberNames.Length; i++)
			{
				enumType.AddMember(new EnumMember(enumMemberNames[i], i));
			}
		}

		// Token: 0x040014FF RID: 5375
		private int _flags;

		// Token: 0x04001500 RID: 5376
		private MetadataPropertyCollection _itemAttributes;

		// Token: 0x04001502 RID: 5378
		private static readonly EdmType[] _builtInTypes = new EdmType[40];

		// Token: 0x04001503 RID: 5379
		private static readonly ReadOnlyCollection<FacetDescription> _generalFacetDescriptions;

		// Token: 0x04001504 RID: 5380
		private static readonly FacetDescription _nullableFacetDescription;

		// Token: 0x04001505 RID: 5381
		private static readonly FacetDescription _defaultValueFacetDescription;

		// Token: 0x04001506 RID: 5382
		private static readonly FacetDescription _collectionKindFacetDescription;

		// Token: 0x02000AF6 RID: 2806
		[Flags]
		internal enum MetadataFlags
		{
			// Token: 0x04002C55 RID: 11349
			None = 0,
			// Token: 0x04002C56 RID: 11350
			CSpace = 1,
			// Token: 0x04002C57 RID: 11351
			OSpace = 2,
			// Token: 0x04002C58 RID: 11352
			OCSpace = 3,
			// Token: 0x04002C59 RID: 11353
			SSpace = 4,
			// Token: 0x04002C5A RID: 11354
			CSSpace = 5,
			// Token: 0x04002C5B RID: 11355
			DataSpace = 7,
			// Token: 0x04002C5C RID: 11356
			Readonly = 8,
			// Token: 0x04002C5D RID: 11357
			IsAbstract = 16,
			// Token: 0x04002C5E RID: 11358
			In = 512,
			// Token: 0x04002C5F RID: 11359
			Out = 1024,
			// Token: 0x04002C60 RID: 11360
			InOut = 1536,
			// Token: 0x04002C61 RID: 11361
			ReturnValue = 2048,
			// Token: 0x04002C62 RID: 11362
			ParameterMode = 3584
		}
	}
}
