using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Linq;
using System.Text;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200009B RID: 155
	public abstract class MetadataItem
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x00019FF0 File Offset: 0x000181F0
		internal MetadataItem()
		{
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001A003 File Offset: 0x00018203
		internal MetadataItem(MetadataItem.MetadataFlags flags)
		{
			this._flags = flags;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000AE2 RID: 2786
		public abstract BuiltInTypeKind BuiltInTypeKind { get; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0001A020 File Offset: 0x00018220
		[MetadataProperty(BuiltInTypeKind.MetadataProperty, true)]
		public ReadOnlyMetadataCollection<MetadataProperty> MetadataProperties
		{
			get
			{
				if (this._itemAttributes == null)
				{
					MetadataPropertyCollection metadataPropertyCollection = new MetadataPropertyCollection(this);
					if (this.IsReadOnly)
					{
						metadataPropertyCollection.SetReadOnly();
					}
					Interlocked.CompareExchange<MetadataCollection<MetadataProperty>>(ref this._itemAttributes, metadataPropertyCollection, null);
				}
				return this._itemAttributes.AsReadOnlyMetadataCollection();
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0001A064 File Offset: 0x00018264
		internal MetadataCollection<MetadataProperty> RawMetadataProperties
		{
			get
			{
				return this._itemAttributes;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0001A06C File Offset: 0x0001826C
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x0001A074 File Offset: 0x00018274
		public Documentation Documentation
		{
			get
			{
				return this._documentation;
			}
			set
			{
				this._documentation = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000AE7 RID: 2791
		internal abstract string Identity { get; }

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001A07D File Offset: 0x0001827D
		internal virtual bool EdmEquals(MetadataItem item)
		{
			return item != null && (this == item || (this.BuiltInTypeKind == item.BuiltInTypeKind && this.Identity == item.Identity));
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0001A0AB File Offset: 0x000182AB
		internal bool IsReadOnly
		{
			get
			{
				return this.GetFlag(MetadataItem.MetadataFlags.Readonly);
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001A0B4 File Offset: 0x000182B4
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

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001A0DA File Offset: 0x000182DA
		internal virtual void BuildIdentity(StringBuilder builder)
		{
			builder.Append(this.Identity);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001A0E9 File Offset: 0x000182E9
		internal void AddMetadataProperties(List<MetadataProperty> metadataProperties)
		{
			this.MetadataProperties.Source.AtomicAddRange(metadataProperties);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001A100 File Offset: 0x00018300
		internal DataSpace GetDataSpace()
		{
			switch (this._flags & MetadataItem.MetadataFlags.DataSpace)
			{
			case MetadataItem.MetadataFlags.CSpace:
				return DataSpace.CSpace;
			case MetadataItem.MetadataFlags.OSpace:
				return DataSpace.OSpace;
			case MetadataItem.MetadataFlags.OCSpace:
				return DataSpace.OCSpace;
			case MetadataItem.MetadataFlags.SSpace:
				return DataSpace.SSpace;
			case MetadataItem.MetadataFlags.CSSpace:
				return DataSpace.CSSpace;
			default:
				return (DataSpace)(-1);
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001A13D File Offset: 0x0001833D
		internal void SetDataSpace(DataSpace space)
		{
			this._flags = (this._flags & ~(MetadataItem.MetadataFlags.CSpace | MetadataItem.MetadataFlags.OSpace | MetadataItem.MetadataFlags.SSpace)) | (MetadataItem.MetadataFlags.DataSpace & MetadataItem.Convert(space));
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0001A157 File Offset: 0x00018357
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

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001A180 File Offset: 0x00018380
		internal ParameterMode GetParameterMode()
		{
			MetadataItem.MetadataFlags metadataFlags = this._flags & MetadataItem.MetadataFlags.ParameterMode;
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

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001A1CD File Offset: 0x000183CD
		internal void SetParameterMode(ParameterMode mode)
		{
			this._flags = (this._flags & ~(MetadataItem.MetadataFlags.In | MetadataItem.MetadataFlags.Out | MetadataItem.MetadataFlags.ReturnValue)) | (MetadataItem.MetadataFlags.ParameterMode & MetadataItem.Convert(mode));
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001A1EE File Offset: 0x000183EE
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

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001A223 File Offset: 0x00018423
		internal bool GetFlag(MetadataItem.MetadataFlags flag)
		{
			return flag == (this._flags & flag);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001A230 File Offset: 0x00018430
		internal void SetFlag(MetadataItem.MetadataFlags flag, bool value)
		{
			MetadataItem.MetadataFlags metadataFlags = flag & MetadataItem.MetadataFlags.Readonly;
			object flagsLock = this._flagsLock;
			lock (flagsLock)
			{
				if (!this.IsReadOnly || (flag & MetadataItem.MetadataFlags.Readonly) != MetadataItem.MetadataFlags.Readonly)
				{
					Util.ThrowIfReadOnly(this);
					if (value)
					{
						this._flags |= flag;
					}
					else
					{
						this._flags &= ~flag;
					}
				}
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001A2A8 File Offset: 0x000184A8
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
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationSet), "AssocationSetType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.RelationshipSet));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.AssociationSetEnd), "AssociationSetEndType", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.FunctionParameter), "FunctionParameter", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmFunction), "EdmFunction", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType));
			MetadataItem.InitializeBuiltInTypes((ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.Documentation), "Documentation", false, (ComplexType)MetadataItem.GetBuiltInType(BuiltInTypeKind.MetadataItem));
			MetadataItem.InitializeEnumType(BuiltInTypeKind.OperationAction, "DeleteAction", new string[] { "None", "Cascade", "Restrict" });
			MetadataItem.InitializeEnumType(BuiltInTypeKind.RelationshipMultiplicity, "RelationshipMultiplicity", new string[] { "One", "ZeroToOne", "Many" });
			MetadataItem.InitializeEnumType(BuiltInTypeKind.ParameterMode, "ParameterMode", new string[] { "In", "Out", "InOut" });
			MetadataItem.InitializeEnumType(BuiltInTypeKind.CollectionKind, "CollectionKind", new string[] { "None", "List", "Bag" });
			MetadataItem.InitializeEnumType(BuiltInTypeKind.PrimitiveTypeKind, "PrimitiveTypeKind", MetadataHelper.PrimitiveTypesNames.ToArray<string>());
			FacetDescription[] array = new FacetDescription[2];
			MetadataItem._nullableFacetDescription = new FacetDescription("Nullable", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.Boolean), null, null, true);
			array[0] = MetadataItem._nullableFacetDescription;
			MetadataItem._defaultValueFacetDescription = new FacetDescription("DefaultValue", MetadataItem.GetBuiltInType(BuiltInTypeKind.EdmType), null, null, null);
			array[1] = MetadataItem._defaultValueFacetDescription;
			MetadataItem._generalFacetDescriptions = Array.AsReadOnly<FacetDescription>(array);
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
			MetadataItem.InitializeNullTypeUsage();
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0001B055 File Offset: 0x00019255
		internal static FacetDescription DefaultValueFacetDescription
		{
			get
			{
				return MetadataItem._defaultValueFacetDescription;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0001B05C File Offset: 0x0001925C
		internal static FacetDescription CollectionKindFacetDescription
		{
			get
			{
				return MetadataItem._collectionKindFacetDescription;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0001B063 File Offset: 0x00019263
		internal static FacetDescription NullableFacetDescription
		{
			get
			{
				return MetadataItem._nullableFacetDescription;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0001B06A File Offset: 0x0001926A
		internal static EdmProviderManifest EdmProviderManifest
		{
			get
			{
				return EdmProviderManifest.Instance;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0001B071 File Offset: 0x00019271
		internal static TypeUsage NullType
		{
			get
			{
				return MetadataItem._nullTypeUsage;
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001B078 File Offset: 0x00019278
		public static EdmType GetBuiltInType(BuiltInTypeKind builtInTypeKind)
		{
			return MetadataItem._builtInTypes[(int)builtInTypeKind];
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001B081 File Offset: 0x00019281
		public static ReadOnlyCollection<FacetDescription> GetGeneralFacetDescriptions()
		{
			return MetadataItem._generalFacetDescriptions;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0001B088 File Offset: 0x00019288
		private static void InitializeBuiltInTypes(ComplexType builtInType, string name, bool isAbstract, ComplexType baseType)
		{
			EdmType.Initialize(builtInType, name, "Edm", DataSpace.CSpace, isAbstract, baseType);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0001B09C File Offset: 0x0001929C
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

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001B0D0 File Offset: 0x000192D0
		private static void InitializeEnumType(BuiltInTypeKind builtInTypeKind, string name, string[] enumMemberNames)
		{
			EnumType enumType = (EnumType)MetadataItem.GetBuiltInType(builtInTypeKind);
			EdmType.Initialize(enumType, name, "Edm", DataSpace.CSpace, false, null);
			for (int i = 0; i < enumMemberNames.Length; i++)
			{
				enumType.AddMember(new EnumMember(enumMemberNames[i]));
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001B114 File Offset: 0x00019314
		private static void InitializeNullTypeUsage()
		{
			MetadataItem._nullTypeUsage = TypeUsage.Create(new ComplexType("NullType", string.Empty, DataSpace.CSpace)
			{
				Abstract = true
			});
		}

		// Token: 0x0400085D RID: 2141
		private MetadataItem.MetadataFlags _flags;

		// Token: 0x0400085E RID: 2142
		private object _flagsLock = new object();

		// Token: 0x0400085F RID: 2143
		private MetadataCollection<MetadataProperty> _itemAttributes;

		// Token: 0x04000860 RID: 2144
		private Documentation _documentation;

		// Token: 0x04000861 RID: 2145
		private static EdmType[] _builtInTypes = new EdmType[40];

		// Token: 0x04000862 RID: 2146
		private static readonly ReadOnlyCollection<FacetDescription> _generalFacetDescriptions;

		// Token: 0x04000863 RID: 2147
		private static TypeUsage _nullTypeUsage;

		// Token: 0x04000864 RID: 2148
		private static FacetDescription _nullableFacetDescription;

		// Token: 0x04000865 RID: 2149
		private static FacetDescription _defaultValueFacetDescription;

		// Token: 0x04000866 RID: 2150
		private static FacetDescription _collectionKindFacetDescription;

		// Token: 0x020002BF RID: 703
		[Flags]
		internal enum MetadataFlags
		{
			// Token: 0x04000FC6 RID: 4038
			None = 0,
			// Token: 0x04000FC7 RID: 4039
			CSpace = 1,
			// Token: 0x04000FC8 RID: 4040
			OSpace = 2,
			// Token: 0x04000FC9 RID: 4041
			OCSpace = 3,
			// Token: 0x04000FCA RID: 4042
			SSpace = 4,
			// Token: 0x04000FCB RID: 4043
			CSSpace = 5,
			// Token: 0x04000FCC RID: 4044
			DataSpace = 7,
			// Token: 0x04000FCD RID: 4045
			Readonly = 8,
			// Token: 0x04000FCE RID: 4046
			IsAbstract = 16,
			// Token: 0x04000FCF RID: 4047
			In = 512,
			// Token: 0x04000FD0 RID: 4048
			Out = 1024,
			// Token: 0x04000FD1 RID: 4049
			InOut = 1536,
			// Token: 0x04000FD2 RID: 4050
			ReturnValue = 2048,
			// Token: 0x04000FD3 RID: 4051
			ParameterMode = 3584
		}
	}
}
