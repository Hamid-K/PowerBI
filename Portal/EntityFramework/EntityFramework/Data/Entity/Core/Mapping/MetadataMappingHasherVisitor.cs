using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200054C RID: 1356
	internal class MetadataMappingHasherVisitor : BaseMetadataMappingVisitor
	{
		// Token: 0x06004276 RID: 17014 RVA: 0x000E4B04 File Offset: 0x000E2D04
		private MetadataMappingHasherVisitor(double mappingVersion, bool sortSequence)
			: base(sortSequence)
		{
			this.m_MappingVersion = mappingVersion;
			this.m_hashSourceBuilder = new CompressingHashBuilder(MetadataHelper.CreateMetadataHashAlgorithm(this.m_MappingVersion));
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x000E4B38 File Offset: 0x000E2D38
		protected override void Visit(EntityContainerMapping entityContainerMapping)
		{
			this.m_MappingVersion = entityContainerMapping.StorageMappingItemCollection.MappingVersion;
			this.m_EdmItemCollection = entityContainerMapping.StorageMappingItemCollection.EdmItemCollection;
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(entityContainerMapping, out num))
			{
				return;
			}
			if (this.m_itemsAlreadySeen.Count > 1)
			{
				this.Clean();
				this.Visit(entityContainerMapping);
				return;
			}
			this.AddObjectStartDumpToHashBuilder(entityContainerMapping, num);
			this.AddObjectContentToHashBuilder(entityContainerMapping.Identity);
			this.AddV2ObjectContentToHashBuilder(entityContainerMapping.GenerateUpdateViews, this.m_MappingVersion);
			base.Visit(entityContainerMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x000E4BC8 File Offset: 0x000E2DC8
		protected override void Visit(EntityContainer entityContainer)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(entityContainer, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(entityContainer, num);
			this.AddObjectContentToHashBuilder(entityContainer.Identity);
			base.Visit(entityContainer);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x000E4C04 File Offset: 0x000E2E04
		protected override void Visit(EntitySetBaseMapping setMapping)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(setMapping, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(setMapping, num);
			base.Visit(setMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x000E4C34 File Offset: 0x000E2E34
		protected override void Visit(TypeMapping typeMapping)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(typeMapping, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(typeMapping, num);
			base.Visit(typeMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x000E4C64 File Offset: 0x000E2E64
		protected override void Visit(MappingFragment mappingFragment)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(mappingFragment, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(mappingFragment, num);
			this.AddV2ObjectContentToHashBuilder(mappingFragment.IsSQueryDistinct, this.m_MappingVersion);
			base.Visit(mappingFragment);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x000E4CA9 File Offset: 0x000E2EA9
		protected override void Visit(PropertyMapping propertyMapping)
		{
			base.Visit(propertyMapping);
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x000E4CB4 File Offset: 0x000E2EB4
		protected override void Visit(ComplexPropertyMapping complexPropertyMapping)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(complexPropertyMapping, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(complexPropertyMapping, num);
			base.Visit(complexPropertyMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600427E RID: 17022 RVA: 0x000E4CE4 File Offset: 0x000E2EE4
		protected override void Visit(ComplexTypeMapping complexTypeMapping)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(complexTypeMapping, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(complexTypeMapping, num);
			base.Visit(complexTypeMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x000E4D14 File Offset: 0x000E2F14
		protected override void Visit(ConditionPropertyMapping conditionPropertyMapping)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(conditionPropertyMapping, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(conditionPropertyMapping, num);
			this.AddObjectContentToHashBuilder(conditionPropertyMapping.IsNull);
			this.AddObjectContentToHashBuilder(conditionPropertyMapping.Value);
			base.Visit(conditionPropertyMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x000E4D60 File Offset: 0x000E2F60
		protected override void Visit(ScalarPropertyMapping scalarPropertyMapping)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(scalarPropertyMapping, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(scalarPropertyMapping, num);
			base.Visit(scalarPropertyMapping);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x000E4D8E File Offset: 0x000E2F8E
		protected override void Visit(EntitySetBase entitySetBase)
		{
			base.Visit(entitySetBase);
		}

		// Token: 0x06004282 RID: 17026 RVA: 0x000E4D98 File Offset: 0x000E2F98
		protected override void Visit(EntitySet entitySet)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(entitySet, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(entitySet, num);
			this.AddObjectContentToHashBuilder(entitySet.Name);
			this.AddObjectContentToHashBuilder(entitySet.Schema);
			this.AddObjectContentToHashBuilder(entitySet.Table);
			base.Visit(entitySet);
			IEnumerable<EdmType> enumerable = from type in MetadataHelper.GetTypeAndSubtypesOf(entitySet.ElementType, this.m_EdmItemCollection, false)
				where type != entitySet.ElementType
				select type;
			foreach (EdmType edmType in base.GetSequence<EdmType>(enumerable, (EdmType it) => it.Identity))
			{
				this.Visit(edmType);
			}
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x000E4EA0 File Offset: 0x000E30A0
		protected override void Visit(AssociationSet associationSet)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(associationSet, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(associationSet, num);
			this.AddObjectContentToHashBuilder(associationSet.Identity);
			this.AddObjectContentToHashBuilder(associationSet.Schema);
			this.AddObjectContentToHashBuilder(associationSet.Table);
			base.Visit(associationSet);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x000E4EF4 File Offset: 0x000E30F4
		protected override void Visit(EntityType entityType)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(entityType, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(entityType, num);
			this.AddObjectContentToHashBuilder(entityType.Abstract);
			this.AddObjectContentToHashBuilder(entityType.Identity);
			base.Visit(entityType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x000E4F40 File Offset: 0x000E3140
		protected override void Visit(AssociationSetEnd associationSetEnd)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(associationSetEnd, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(associationSetEnd, num);
			this.AddObjectContentToHashBuilder(associationSetEnd.Identity);
			base.Visit(associationSetEnd);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x000E4F7C File Offset: 0x000E317C
		protected override void Visit(AssociationType associationType)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(associationType, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(associationType, num);
			this.AddObjectContentToHashBuilder(associationType.Abstract);
			this.AddObjectContentToHashBuilder(associationType.Identity);
			base.Visit(associationType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x000E4FC8 File Offset: 0x000E31C8
		protected override void Visit(EdmProperty edmProperty)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(edmProperty, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(edmProperty, num);
			this.AddObjectContentToHashBuilder(edmProperty.DefaultValue);
			this.AddObjectContentToHashBuilder(edmProperty.Identity);
			this.AddObjectContentToHashBuilder(edmProperty.IsStoreGeneratedComputed);
			this.AddObjectContentToHashBuilder(edmProperty.IsStoreGeneratedIdentity);
			this.AddObjectContentToHashBuilder(edmProperty.Nullable);
			base.Visit(edmProperty);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x000E5041 File Offset: 0x000E3241
		protected override void Visit(NavigationProperty navigationProperty)
		{
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x000E5044 File Offset: 0x000E3244
		protected override void Visit(EdmMember edmMember)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(edmMember, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(edmMember, num);
			this.AddObjectContentToHashBuilder(edmMember.Identity);
			this.AddObjectContentToHashBuilder(edmMember.IsStoreGeneratedComputed);
			this.AddObjectContentToHashBuilder(edmMember.IsStoreGeneratedIdentity);
			base.Visit(edmMember);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x000E50A0 File Offset: 0x000E32A0
		protected override void Visit(AssociationEndMember associationEndMember)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(associationEndMember, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(associationEndMember, num);
			this.AddObjectContentToHashBuilder(associationEndMember.DeleteBehavior);
			this.AddObjectContentToHashBuilder(associationEndMember.Identity);
			this.AddObjectContentToHashBuilder(associationEndMember.IsStoreGeneratedComputed);
			this.AddObjectContentToHashBuilder(associationEndMember.IsStoreGeneratedIdentity);
			this.AddObjectContentToHashBuilder(associationEndMember.RelationshipMultiplicity);
			base.Visit(associationEndMember);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x000E5120 File Offset: 0x000E3320
		protected override void Visit(ReferentialConstraint referentialConstraint)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(referentialConstraint, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(referentialConstraint, num);
			this.AddObjectContentToHashBuilder(referentialConstraint.Identity);
			base.Visit(referentialConstraint);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x000E515C File Offset: 0x000E335C
		protected override void Visit(RelationshipEndMember relationshipEndMember)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(relationshipEndMember, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(relationshipEndMember, num);
			this.AddObjectContentToHashBuilder(relationshipEndMember.DeleteBehavior);
			this.AddObjectContentToHashBuilder(relationshipEndMember.Identity);
			this.AddObjectContentToHashBuilder(relationshipEndMember.IsStoreGeneratedComputed);
			this.AddObjectContentToHashBuilder(relationshipEndMember.IsStoreGeneratedIdentity);
			this.AddObjectContentToHashBuilder(relationshipEndMember.RelationshipMultiplicity);
			base.Visit(relationshipEndMember);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x000E51DC File Offset: 0x000E33DC
		protected override void Visit(TypeUsage typeUsage)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(typeUsage, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(typeUsage, num);
			base.Visit(typeUsage);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x000E520A File Offset: 0x000E340A
		protected override void Visit(RelationshipType relationshipType)
		{
			base.Visit(relationshipType);
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x000E5213 File Offset: 0x000E3413
		protected override void Visit(EdmType edmType)
		{
			base.Visit(edmType);
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x000E521C File Offset: 0x000E341C
		protected override void Visit(EnumType enumType)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(enumType, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(enumType, num);
			this.AddObjectContentToHashBuilder(enumType.Identity);
			this.Visit(enumType.UnderlyingType);
			base.Visit(enumType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x000E5264 File Offset: 0x000E3464
		protected override void Visit(EnumMember enumMember)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(enumMember, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(enumMember, num);
			this.AddObjectContentToHashBuilder(enumMember.Name);
			this.AddObjectContentToHashBuilder(enumMember.Value);
			base.Visit(enumMember);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x000E52AC File Offset: 0x000E34AC
		protected override void Visit(CollectionType collectionType)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(collectionType, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(collectionType, num);
			this.AddObjectContentToHashBuilder(collectionType.Identity);
			base.Visit(collectionType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x000E52E8 File Offset: 0x000E34E8
		protected override void Visit(RefType refType)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(refType, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(refType, num);
			this.AddObjectContentToHashBuilder(refType.Identity);
			base.Visit(refType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x000E5322 File Offset: 0x000E3522
		protected override void Visit(EntityTypeBase entityTypeBase)
		{
			base.Visit(entityTypeBase);
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x000E532C File Offset: 0x000E352C
		protected override void Visit(Facet facet)
		{
			if (facet.Name != "Nullable")
			{
				return;
			}
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(facet, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(facet, num);
			this.AddObjectContentToHashBuilder(facet.Identity);
			this.AddObjectContentToHashBuilder(facet.Value);
			base.Visit(facet);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x000E5385 File Offset: 0x000E3585
		protected override void Visit(EdmFunction edmFunction)
		{
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x000E5388 File Offset: 0x000E3588
		protected override void Visit(ComplexType complexType)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(complexType, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(complexType, num);
			this.AddObjectContentToHashBuilder(complexType.Abstract);
			this.AddObjectContentToHashBuilder(complexType.Identity);
			base.Visit(complexType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x000E53D4 File Offset: 0x000E35D4
		protected override void Visit(PrimitiveType primitiveType)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(primitiveType, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(primitiveType, num);
			this.AddObjectContentToHashBuilder(primitiveType.Name);
			this.AddObjectContentToHashBuilder(primitiveType.NamespaceName);
			base.Visit(primitiveType);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x000E541C File Offset: 0x000E361C
		protected override void Visit(FunctionParameter functionParameter)
		{
			int num;
			if (!this.AddObjectToSeenListAndHashBuilder(functionParameter, out num))
			{
				return;
			}
			this.AddObjectStartDumpToHashBuilder(functionParameter, num);
			this.AddObjectContentToHashBuilder(functionParameter.Identity);
			this.AddObjectContentToHashBuilder(functionParameter.Mode);
			base.Visit(functionParameter);
			this.AddObjectEndDumpToHashBuilder();
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x000E5467 File Offset: 0x000E3667
		protected override void Visit(DbProviderManifest providerManifest)
		{
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x000E5469 File Offset: 0x000E3669
		internal string HashValue
		{
			get
			{
				return this.m_hashSourceBuilder.ComputeHash();
			}
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x000E5476 File Offset: 0x000E3676
		private void Clean()
		{
			this.m_hashSourceBuilder = new CompressingHashBuilder(MetadataHelper.CreateMetadataHashAlgorithm(this.m_MappingVersion));
			this.m_instanceNumber = 0;
			this.m_itemsAlreadySeen = new Dictionary<object, int>();
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x000E54A0 File Offset: 0x000E36A0
		private bool TryAddSeenItem(object o, out int indexSeen)
		{
			if (!this.m_itemsAlreadySeen.TryGetValue(o, out indexSeen))
			{
				this.m_itemsAlreadySeen.Add(o, this.m_instanceNumber);
				indexSeen = this.m_instanceNumber;
				this.m_instanceNumber++;
				return true;
			}
			return false;
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x000E54DC File Offset: 0x000E36DC
		private bool AddObjectToSeenListAndHashBuilder(object o, out int instanceIndex)
		{
			if (o == null)
			{
				instanceIndex = -1;
				return false;
			}
			if (!this.TryAddSeenItem(o, out instanceIndex))
			{
				this.AddObjectStartDumpToHashBuilder(o, instanceIndex);
				this.AddSeenObjectToHashBuilder(instanceIndex);
				this.AddObjectEndDumpToHashBuilder();
				return false;
			}
			return true;
		}

		// Token: 0x0600429F RID: 17055 RVA: 0x000E550A File Offset: 0x000E370A
		private void AddSeenObjectToHashBuilder(int instanceIndex)
		{
			this.m_hashSourceBuilder.AppendLine("Instance Reference: " + instanceIndex.ToString());
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x000E5528 File Offset: 0x000E3728
		private void AddObjectStartDumpToHashBuilder(object o, int objectIndex)
		{
			this.m_hashSourceBuilder.AppendObjectStartDump(o, objectIndex);
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x000E5537 File Offset: 0x000E3737
		private void AddObjectEndDumpToHashBuilder()
		{
			this.m_hashSourceBuilder.AppendObjectEndDump();
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x000E5544 File Offset: 0x000E3744
		private void AddObjectContentToHashBuilder(object content)
		{
			if (content == null)
			{
				this.m_hashSourceBuilder.AppendLine("NULL");
				return;
			}
			IFormattable formattable = content as IFormattable;
			if (formattable != null)
			{
				this.m_hashSourceBuilder.AppendLine(formattable.ToString(null, CultureInfo.InvariantCulture));
				return;
			}
			this.m_hashSourceBuilder.AppendLine(content.ToString());
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x000E5598 File Offset: 0x000E3798
		private void AddV2ObjectContentToHashBuilder(object content, double version)
		{
			if (version >= 2.0)
			{
				this.AddObjectContentToHashBuilder(content);
			}
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x000E55AD File Offset: 0x000E37AD
		internal static string GetMappingClosureHash(double mappingVersion, EntityContainerMapping entityContainerMapping, bool sortSequence = true)
		{
			MetadataMappingHasherVisitor metadataMappingHasherVisitor = new MetadataMappingHasherVisitor(mappingVersion, sortSequence);
			metadataMappingHasherVisitor.Visit(entityContainerMapping);
			return metadataMappingHasherVisitor.HashValue;
		}

		// Token: 0x04001772 RID: 6002
		private CompressingHashBuilder m_hashSourceBuilder;

		// Token: 0x04001773 RID: 6003
		private Dictionary<object, int> m_itemsAlreadySeen = new Dictionary<object, int>();

		// Token: 0x04001774 RID: 6004
		private int m_instanceNumber;

		// Token: 0x04001775 RID: 6005
		private EdmItemCollection m_EdmItemCollection;

		// Token: 0x04001776 RID: 6006
		private double m_MappingVersion;
	}
}
