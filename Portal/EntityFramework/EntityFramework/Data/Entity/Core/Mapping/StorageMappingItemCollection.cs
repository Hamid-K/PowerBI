using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200055B RID: 1371
	public class StorageMappingItemCollection : MappingItemCollection
	{
		// Token: 0x060042E0 RID: 17120 RVA: 0x000E5D23 File Offset: 0x000E3F23
		internal StorageMappingItemCollection()
			: base(DataSpace.CSSpace)
		{
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x000E5D44 File Offset: 0x000E3F44
		public StorageMappingItemCollection(EdmItemCollection edmCollection, StoreItemCollection storeCollection, params string[] filePaths)
			: base(DataSpace.CSSpace)
		{
			Check.NotNull<EdmItemCollection>(edmCollection, "edmCollection");
			Check.NotNull<StoreItemCollection>(storeCollection, "storeCollection");
			Check.NotNull<string[]>(filePaths, "filePaths");
			this._edmCollection = edmCollection;
			this._storeItemCollection = storeCollection;
			List<XmlReader> list = null;
			try
			{
				MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromFilePaths(filePaths, ".msl");
				list = metadataArtifactLoader.CreateReaders(DataSpace.CSSpace);
				this.Init(edmCollection, storeCollection, list, metadataArtifactLoader.GetPaths(DataSpace.CSSpace), true);
			}
			finally
			{
				if (list != null)
				{
					Helper.DisposeXmlReaders(list);
				}
			}
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x000E5DE8 File Offset: 0x000E3FE8
		public StorageMappingItemCollection(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable<XmlReader> xmlReaders)
			: base(DataSpace.CSSpace)
		{
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			MetadataArtifactLoader metadataArtifactLoader = MetadataArtifactLoader.CreateCompositeFromXmlReaders(xmlReaders);
			this.Init(edmCollection, storeCollection, metadataArtifactLoader.GetReaders(), metadataArtifactLoader.GetPaths(), true);
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x000E5E3B File Offset: 0x000E403B
		private StorageMappingItemCollection(EdmItemCollection edmItemCollection, StoreItemCollection storeItemCollection, IEnumerable<XmlReader> xmlReaders, IList<string> filePaths, out IList<EdmSchemaError> errors)
			: base(DataSpace.CSSpace)
		{
			errors = this.Init(edmItemCollection, storeItemCollection, xmlReaders, filePaths, false);
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x000E5E69 File Offset: 0x000E4069
		internal StorageMappingItemCollection(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable<XmlReader> xmlReaders, IList<string> filePaths)
			: base(DataSpace.CSSpace)
		{
			this.Init(edmCollection, storeCollection, xmlReaders, filePaths, true);
		}

		// Token: 0x060042E5 RID: 17125 RVA: 0x000E5E98 File Offset: 0x000E4098
		private IList<EdmSchemaError> Init(EdmItemCollection edmCollection, StoreItemCollection storeCollection, IEnumerable<XmlReader> xmlReaders, IList<string> filePaths, bool throwOnError)
		{
			this._edmCollection = edmCollection;
			this._storeItemCollection = storeCollection;
			Dictionary<EntitySetBase, GeneratedView> dictionary;
			Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> dictionary2;
			this.m_viewDictionary = new StorageMappingItemCollection.ViewDictionary(this, out dictionary, out dictionary2);
			List<EdmSchemaError> list = new List<EdmSchemaError>();
			if (this._edmCollection.EdmVersion != 0.0 && this._storeItemCollection.StoreSchemaVersion != 0.0 && this._edmCollection.EdmVersion != this._storeItemCollection.StoreSchemaVersion)
			{
				list.Add(new EdmSchemaError(Strings.Mapping_DifferentEdmStoreVersion, 2102, EdmSchemaErrorSeverity.Error));
			}
			else
			{
				double num = ((this._edmCollection.EdmVersion != 0.0) ? this._edmCollection.EdmVersion : this._storeItemCollection.StoreSchemaVersion);
				list.AddRange(this.LoadItems(xmlReaders, filePaths, dictionary, dictionary2, num));
			}
			if (list.Count > 0 && throwOnError && !MetadataHelper.CheckIfAllErrorsAreWarnings(list))
			{
				throw new MappingException(string.Format(CultureInfo.CurrentCulture, EntityRes.GetString("InvalidSchemaEncountered"), new object[] { Helper.CombineErrorMessage(list) }));
			}
			return list;
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060042E6 RID: 17126 RVA: 0x000E5FA6 File Offset: 0x000E41A6
		// (set) Token: 0x060042E7 RID: 17127 RVA: 0x000E5FAE File Offset: 0x000E41AE
		public DbMappingViewCacheFactory MappingViewCacheFactory
		{
			get
			{
				return this._mappingViewCacheFactory;
			}
			set
			{
				Check.NotNull<DbMappingViewCacheFactory>(value, "value");
				Interlocked.CompareExchange<DbMappingViewCacheFactory>(ref this._mappingViewCacheFactory, value, null);
				if (!this._mappingViewCacheFactory.Equals(value))
				{
					throw new ArgumentException(Strings.MappingViewCacheFactory_MustNotChange, "value");
				}
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x060042E8 RID: 17128 RVA: 0x000E5FE8 File Offset: 0x000E41E8
		internal MetadataWorkspace Workspace
		{
			get
			{
				if (this._workspace == null)
				{
					this._workspace = new MetadataWorkspace(() => this._edmCollection, () => this._storeItemCollection, () => this);
				}
				return this._workspace;
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x060042E9 RID: 17129 RVA: 0x000E6027 File Offset: 0x000E4227
		internal EdmItemCollection EdmItemCollection
		{
			get
			{
				return this._edmCollection;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x060042EA RID: 17130 RVA: 0x000E602F File Offset: 0x000E422F
		public double MappingVersion
		{
			get
			{
				return this.m_mappingVersion;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x000E6037 File Offset: 0x000E4237
		internal StoreItemCollection StoreItemCollection
		{
			get
			{
				return this._storeItemCollection;
			}
		}

		// Token: 0x060042EC RID: 17132 RVA: 0x000E603F File Offset: 0x000E423F
		internal override MappingBase GetMap(string identity, DataSpace typeSpace, bool ignoreCase)
		{
			if (typeSpace != DataSpace.CSpace)
			{
				throw new InvalidOperationException(Strings.Mapping_Storage_InvalidSpace(typeSpace));
			}
			return base.GetItem<MappingBase>(identity, ignoreCase);
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x000E605E File Offset: 0x000E425E
		internal override bool TryGetMap(string identity, DataSpace typeSpace, bool ignoreCase, out MappingBase map)
		{
			if (typeSpace != DataSpace.CSpace)
			{
				throw new InvalidOperationException(Strings.Mapping_Storage_InvalidSpace(typeSpace));
			}
			return base.TryGetItem<MappingBase>(identity, ignoreCase, out map);
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x000E607F File Offset: 0x000E427F
		internal override MappingBase GetMap(string identity, DataSpace typeSpace)
		{
			return this.GetMap(identity, typeSpace, false);
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x000E608A File Offset: 0x000E428A
		internal override bool TryGetMap(string identity, DataSpace typeSpace, out MappingBase map)
		{
			return this.TryGetMap(identity, typeSpace, false, out map);
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x000E6098 File Offset: 0x000E4298
		internal override MappingBase GetMap(GlobalItem item)
		{
			DataSpace dataSpace = item.DataSpace;
			if (dataSpace != DataSpace.CSpace)
			{
				throw new InvalidOperationException(Strings.Mapping_Storage_InvalidSpace(dataSpace));
			}
			return this.GetMap(item.Identity, dataSpace);
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x000E60D0 File Offset: 0x000E42D0
		internal override bool TryGetMap(GlobalItem item, out MappingBase map)
		{
			if (item == null)
			{
				map = null;
				return false;
			}
			DataSpace dataSpace = item.DataSpace;
			if (dataSpace != DataSpace.CSpace)
			{
				map = null;
				return false;
			}
			return this.TryGetMap(item.Identity, dataSpace, out map);
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x000E6104 File Offset: 0x000E4304
		internal ReadOnlyCollection<EdmMember> GetInterestingMembers(EntitySetBase entitySet, EntityTypeBase entityType, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind)
		{
			Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind> tuple = new Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind>(entitySet, entityType, interestingMembersKind);
			return this._cachedInterestingMembers.GetOrAdd(tuple, this.FindInterestingMembers(entitySet, entityType, interestingMembersKind));
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x000E6130 File Offset: 0x000E4330
		private ReadOnlyCollection<EdmMember> FindInterestingMembers(EntitySetBase entitySet, EntityTypeBase entityType, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind)
		{
			List<EdmMember> list = new List<EdmMember>();
			foreach (TypeMapping typeMapping in MappingMetadataHelper.GetMappingsForEntitySetAndSuperTypes(this, entitySet.EntityContainer, entitySet, entityType))
			{
				AssociationTypeMapping associationTypeMapping = typeMapping as AssociationTypeMapping;
				if (associationTypeMapping != null)
				{
					StorageMappingItemCollection.FindInterestingAssociationMappingMembers(associationTypeMapping, list);
				}
				else
				{
					StorageMappingItemCollection.FindInterestingEntityMappingMembers((EntityTypeMapping)typeMapping, interestingMembersKind, list);
				}
			}
			if (interestingMembersKind != StorageMappingItemCollection.InterestingMembersKind.RequiredOriginalValueMembers)
			{
				StorageMappingItemCollection.FindForeignKeyProperties(entitySet, entityType, list);
			}
			foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in from functionMappings in MappingMetadataHelper.GetModificationFunctionMappingsForEntitySetAndType(this, entitySet.EntityContainer, entitySet, entityType)
				where functionMappings.UpdateFunctionMapping != null
				select functionMappings)
			{
				StorageMappingItemCollection.FindInterestingFunctionMappingMembers(entityTypeModificationFunctionMapping, interestingMembersKind, ref list);
			}
			return new ReadOnlyCollection<EdmMember>(list.Distinct<EdmMember>().ToList<EdmMember>());
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x000E622C File Offset: 0x000E442C
		private static void FindInterestingAssociationMappingMembers(AssociationTypeMapping associationTypeMapping, List<EdmMember> interestingMembers)
		{
			interestingMembers.AddRange(from epm in associationTypeMapping.MappingFragments.SelectMany((MappingFragment m) => m.AllProperties).OfType<EndPropertyMapping>()
				select epm.AssociationEnd);
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000E6294 File Offset: 0x000E4494
		private static void FindInterestingEntityMappingMembers(EntityTypeMapping entityTypeMapping, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind, List<EdmMember> interestingMembers)
		{
			foreach (PropertyMapping propertyMapping in entityTypeMapping.MappingFragments.SelectMany((MappingFragment mf) => mf.AllProperties))
			{
				ScalarPropertyMapping scalarPropertyMapping = propertyMapping as ScalarPropertyMapping;
				ComplexPropertyMapping complexPropertyMapping = propertyMapping as ComplexPropertyMapping;
				ConditionPropertyMapping conditionPropertyMapping = propertyMapping as ConditionPropertyMapping;
				if (scalarPropertyMapping != null && scalarPropertyMapping.Property != null)
				{
					if (MetadataHelper.IsPartOfEntityTypeKey(scalarPropertyMapping.Property))
					{
						if (interestingMembersKind == StorageMappingItemCollection.InterestingMembersKind.RequiredOriginalValueMembers)
						{
							interestingMembers.Add(scalarPropertyMapping.Property);
						}
					}
					else if (MetadataHelper.GetConcurrencyMode(scalarPropertyMapping.Property) == ConcurrencyMode.Fixed)
					{
						interestingMembers.Add(scalarPropertyMapping.Property);
					}
				}
				else if (complexPropertyMapping != null)
				{
					if (interestingMembersKind == StorageMappingItemCollection.InterestingMembersKind.PartialUpdate || MetadataHelper.GetConcurrencyMode(complexPropertyMapping.Property) == ConcurrencyMode.Fixed || StorageMappingItemCollection.HasFixedConcurrencyModeInAnyChildProperty(complexPropertyMapping))
					{
						interestingMembers.Add(complexPropertyMapping.Property);
					}
				}
				else if (conditionPropertyMapping != null && conditionPropertyMapping.Property != null)
				{
					interestingMembers.Add(conditionPropertyMapping.Property);
				}
			}
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x000E63A0 File Offset: 0x000E45A0
		private static bool HasFixedConcurrencyModeInAnyChildProperty(ComplexPropertyMapping complexMapping)
		{
			foreach (PropertyMapping propertyMapping in complexMapping.TypeMappings.SelectMany((ComplexTypeMapping m) => m.AllProperties))
			{
				ScalarPropertyMapping scalarPropertyMapping = propertyMapping as ScalarPropertyMapping;
				ComplexPropertyMapping complexPropertyMapping = propertyMapping as ComplexPropertyMapping;
				if (scalarPropertyMapping != null && MetadataHelper.GetConcurrencyMode(scalarPropertyMapping.Property) == ConcurrencyMode.Fixed)
				{
					return true;
				}
				if (complexPropertyMapping != null && (MetadataHelper.GetConcurrencyMode(complexPropertyMapping.Property) == ConcurrencyMode.Fixed || StorageMappingItemCollection.HasFixedConcurrencyModeInAnyChildProperty(complexPropertyMapping)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x000E644C File Offset: 0x000E464C
		private static void FindForeignKeyProperties(EntitySetBase entitySetBase, EntityTypeBase entityType, List<EdmMember> interestingMembers)
		{
			EntitySet entitySet = entitySetBase as EntitySet;
			if (entitySet != null && entitySet.HasForeignKeyRelationships)
			{
				interestingMembers.AddRange(from p in MetadataHelper.GetTypeAndParentTypesOf(entityType, true).SelectMany((EdmType e) => ((EntityType)e).Properties)
					where entitySet.ForeignKeyDependents.SelectMany((Tuple<AssociationSet, global::System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint> fk) => fk.Item2.ToProperties).Contains(p)
					select p);
			}
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x000E64C4 File Offset: 0x000E46C4
		private static void FindInterestingFunctionMappingMembers(EntityTypeModificationFunctionMapping functionMappings, StorageMappingItemCollection.InterestingMembersKind interestingMembersKind, ref List<EdmMember> interestingMembers)
		{
			if (interestingMembersKind == StorageMappingItemCollection.InterestingMembersKind.PartialUpdate)
			{
				interestingMembers.AddRange(functionMappings.UpdateFunctionMapping.ParameterBindings.Select((ModificationFunctionParameterBinding p) => p.MemberPath.Members.Last<EdmMember>()));
				return;
			}
			foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in functionMappings.UpdateFunctionMapping.ParameterBindings.Where((ModificationFunctionParameterBinding p) => !p.IsCurrent))
			{
				interestingMembers.Add(modificationFunctionParameterBinding.MemberPath.Members.Last<EdmMember>());
			}
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x000E6588 File Offset: 0x000E4788
		internal GeneratedView GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace)
		{
			return this.m_viewDictionary.GetGeneratedView(extent, workspace, this);
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000E6598 File Offset: 0x000E4798
		private void AddInternal(MappingBase storageMap)
		{
			storageMap.DataSpace = DataSpace.CSSpace;
			try
			{
				base.AddInternal(storageMap);
			}
			catch (ArgumentException ex)
			{
				throw new MappingException(Strings.Mapping_Duplicate_Type(storageMap.EdmItem.Identity), ex);
			}
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x000E65E0 File Offset: 0x000E47E0
		internal bool ContainsStorageEntityContainer(string storageEntityContainerName)
		{
			return this.GetItems<EntityContainerMapping>().Any((EntityContainerMapping map) => map.StorageEntityContainer.Name.Equals(storageEntityContainerName, StringComparison.Ordinal));
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x000E6614 File Offset: 0x000E4814
		private List<EdmSchemaError> LoadItems(IEnumerable<XmlReader> xmlReaders, IList<string> mappingSchemaUris, Dictionary<EntitySetBase, GeneratedView> userDefinedQueryViewsDict, Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> userDefinedQueryViewsOfTypeDict, double expectedVersion)
		{
			List<EdmSchemaError> list = new List<EdmSchemaError>();
			int num = -1;
			foreach (XmlReader xmlReader in xmlReaders)
			{
				num++;
				string text = null;
				if (mappingSchemaUris == null)
				{
					SchemaManager.TryGetBaseUri(xmlReader, out text);
				}
				else
				{
					text = mappingSchemaUris[num];
				}
				MappingItemLoader mappingItemLoader = new MappingItemLoader(xmlReader, this, text, this.m_memberMappings);
				list.AddRange(mappingItemLoader.ParsingErrors);
				this.CheckIsSameVersion(expectedVersion, mappingItemLoader.MappingVersion, list);
				EntityContainerMapping containerMapping = mappingItemLoader.ContainerMapping;
				if (mappingItemLoader.HasQueryViews && containerMapping != null)
				{
					StorageMappingItemCollection.CompileUserDefinedQueryViews(containerMapping, userDefinedQueryViewsDict, userDefinedQueryViewsOfTypeDict, list);
				}
				if (MetadataHelper.CheckIfAllErrorsAreWarnings(list) && !base.Contains(containerMapping))
				{
					containerMapping.SetReadOnly();
					this.AddInternal(containerMapping);
				}
			}
			StorageMappingItemCollection.CheckForDuplicateItems(this.EdmItemCollection, this.StoreItemCollection, list);
			return list;
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x000E6704 File Offset: 0x000E4904
		private static void CompileUserDefinedQueryViews(EntityContainerMapping entityContainerMapping, Dictionary<EntitySetBase, GeneratedView> userDefinedQueryViewsDict, Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> userDefinedQueryViewsOfTypeDict, IList<EdmSchemaError> errors)
		{
			ConfigViewGenerator configViewGenerator = new ConfigViewGenerator();
			foreach (EntitySetBaseMapping entitySetBaseMapping in entityContainerMapping.AllSetMaps)
			{
				GeneratedView generatedView;
				if (entitySetBaseMapping.QueryView != null && !userDefinedQueryViewsDict.TryGetValue(entitySetBaseMapping.Set, out generatedView))
				{
					if (GeneratedView.TryParseUserSpecifiedView(entitySetBaseMapping, entitySetBaseMapping.Set.ElementType, entitySetBaseMapping.QueryView, true, entityContainerMapping.StorageMappingItemCollection, configViewGenerator, errors, out generatedView))
					{
						userDefinedQueryViewsDict.Add(entitySetBaseMapping.Set, generatedView);
					}
					foreach (Pair<EntitySetBase, Pair<EntityTypeBase, bool>> pair in entitySetBaseMapping.GetTypeSpecificQVKeys())
					{
						if (GeneratedView.TryParseUserSpecifiedView(entitySetBaseMapping, pair.Second.First, entitySetBaseMapping.GetTypeSpecificQueryView(pair), pair.Second.Second, entityContainerMapping.StorageMappingItemCollection, configViewGenerator, errors, out generatedView))
						{
							userDefinedQueryViewsOfTypeDict.Add(pair, generatedView);
						}
					}
				}
			}
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x000E681C File Offset: 0x000E4A1C
		private void CheckIsSameVersion(double expectedVersion, double currentLoaderVersion, IList<EdmSchemaError> errors)
		{
			if (this.m_mappingVersion == 0.0)
			{
				this.m_mappingVersion = currentLoaderVersion;
			}
			if (expectedVersion != 0.0 && currentLoaderVersion != 0.0 && currentLoaderVersion != expectedVersion)
			{
				errors.Add(new EdmSchemaError(Strings.Mapping_DifferentMappingEdmStoreVersion, 2101, EdmSchemaErrorSeverity.Error));
			}
			if (currentLoaderVersion != this.m_mappingVersion && currentLoaderVersion != 0.0)
			{
				errors.Add(new EdmSchemaError(Strings.CannotLoadDifferentVersionOfSchemaInTheSameItemCollection, 2100, EdmSchemaErrorSeverity.Error));
			}
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x000E689E File Offset: 0x000E4A9E
		internal ViewLoader GetUpdateViewLoader()
		{
			if (this._viewLoader == null)
			{
				this._viewLoader = new ViewLoader(this);
			}
			return this._viewLoader;
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x000E68BA File Offset: 0x000E4ABA
		internal bool TryGetGeneratedViewOfType(EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
		{
			return this.m_viewDictionary.TryGetGeneratedViewOfType(entity, type, includeSubtypes, out generatedView);
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x000E68CC File Offset: 0x000E4ACC
		private static void CheckForDuplicateItems(EdmItemCollection edmItemCollection, StoreItemCollection storeItemCollection, List<EdmSchemaError> errorCollection)
		{
			foreach (GlobalItem globalItem in edmItemCollection)
			{
				if (storeItemCollection.Contains(globalItem.Identity))
				{
					errorCollection.Add(new EdmSchemaError(Strings.Mapping_ItemWithSameNameExistsBothInCSpaceAndSSpace(globalItem.Identity), 2070, EdmSchemaErrorSeverity.Error));
				}
			}
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x000E6940 File Offset: 0x000E4B40
		public string ComputeMappingHashValue(string conceptualModelContainerName, string storeModelContainerName)
		{
			Check.NotEmpty(conceptualModelContainerName, "conceptualModelContainerName");
			Check.NotEmpty(storeModelContainerName, "storeModelContainerName");
			EntityContainerMapping entityContainerMapping = this.GetItems<EntityContainerMapping>().SingleOrDefault((EntityContainerMapping m) => m.EdmEntityContainer.Name == conceptualModelContainerName && m.StorageEntityContainer.Name == storeModelContainerName);
			if (entityContainerMapping == null)
			{
				throw new InvalidOperationException(Strings.HashCalcContainersNotFound(conceptualModelContainerName, storeModelContainerName));
			}
			return MetadataMappingHasherVisitor.GetMappingClosureHash(this.MappingVersion, entityContainerMapping, true);
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x000E69C2 File Offset: 0x000E4BC2
		public string ComputeMappingHashValue()
		{
			if (this.GetItems<EntityContainerMapping>().Count != 1)
			{
				throw new InvalidOperationException(Strings.HashCalcMultipleContainers);
			}
			return MetadataMappingHasherVisitor.GetMappingClosureHash(this.MappingVersion, this.GetItems<EntityContainerMapping>().Single<EntityContainerMapping>(), true);
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x000E69F4 File Offset: 0x000E4BF4
		public Dictionary<EntitySetBase, DbMappingView> GenerateViews(string conceptualModelContainerName, string storeModelContainerName, IList<EdmSchemaError> errors)
		{
			Check.NotEmpty(conceptualModelContainerName, "conceptualModelContainerName");
			Check.NotEmpty(storeModelContainerName, "storeModelContainerName");
			Check.NotNull<IList<EdmSchemaError>>(errors, "errors");
			EntityContainerMapping entityContainerMapping = this.GetItems<EntityContainerMapping>().SingleOrDefault((EntityContainerMapping m) => m.EdmEntityContainer.Name == conceptualModelContainerName && m.StorageEntityContainer.Name == storeModelContainerName);
			if (entityContainerMapping == null)
			{
				throw new InvalidOperationException(Strings.ViewGenContainersNotFound(conceptualModelContainerName, storeModelContainerName));
			}
			return StorageMappingItemCollection.GenerateViews(entityContainerMapping, errors);
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x000E6A7A File Offset: 0x000E4C7A
		public Dictionary<EntitySetBase, DbMappingView> GenerateViews(IList<EdmSchemaError> errors)
		{
			Check.NotNull<IList<EdmSchemaError>>(errors, "errors");
			if (this.GetItems<EntityContainerMapping>().Count != 1)
			{
				throw new InvalidOperationException(Strings.ViewGenMultipleContainers);
			}
			return StorageMappingItemCollection.GenerateViews(this.GetItems<EntityContainerMapping>().Single<EntityContainerMapping>(), errors);
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x000E6AB4 File Offset: 0x000E4CB4
		internal static Dictionary<EntitySetBase, DbMappingView> GenerateViews(EntityContainerMapping containerMapping, IList<EdmSchemaError> errors)
		{
			Dictionary<EntitySetBase, DbMappingView> dictionary = new Dictionary<EntitySetBase, DbMappingView>();
			if (!containerMapping.HasViews)
			{
				return dictionary;
			}
			if (!containerMapping.HasMappingFragments())
			{
				errors.Add(new EdmSchemaError(Strings.Mapping_AllQueryViewAtCompileTime(containerMapping.Identity), 2088, EdmSchemaErrorSeverity.Warning));
				return dictionary;
			}
			ViewGenResults viewGenResults = ViewgenGatekeeper.GenerateViewsFromMapping(containerMapping, new ConfigViewGenerator
			{
				GenerateEsql = true
			});
			if (viewGenResults.HasErrors)
			{
				viewGenResults.Errors.Each(delegate(EdmSchemaError e)
				{
					errors.Add(e);
				});
			}
			foreach (KeyValuePair<EntitySetBase, List<GeneratedView>> keyValuePair in viewGenResults.Views.KeyValuePairs)
			{
				dictionary.Add(keyValuePair.Key, new DbMappingView(keyValuePair.Value[0].eSQL));
			}
			return dictionary;
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x000E6BA0 File Offset: 0x000E4DA0
		public static StorageMappingItemCollection Create(EdmItemCollection edmItemCollection, StoreItemCollection storeItemCollection, IEnumerable<XmlReader> xmlReaders, IList<string> filePaths, out IList<EdmSchemaError> errors)
		{
			Check.NotNull<EdmItemCollection>(edmItemCollection, "edmItemCollection");
			Check.NotNull<StoreItemCollection>(storeItemCollection, "storeItemCollection");
			Check.NotNull<IEnumerable<XmlReader>>(xmlReaders, "xmlReaders");
			EntityUtil.CheckArgumentContainsNull<XmlReader>(ref xmlReaders, "xmlReaders");
			StorageMappingItemCollection storageMappingItemCollection = new StorageMappingItemCollection(edmItemCollection, storeItemCollection, xmlReaders, filePaths, out errors);
			if (errors == null || errors.Count <= 0)
			{
				return storageMappingItemCollection;
			}
			return null;
		}

		// Token: 0x040017E6 RID: 6118
		private EdmItemCollection _edmCollection;

		// Token: 0x040017E7 RID: 6119
		private StoreItemCollection _storeItemCollection;

		// Token: 0x040017E8 RID: 6120
		private StorageMappingItemCollection.ViewDictionary m_viewDictionary;

		// Token: 0x040017E9 RID: 6121
		private double m_mappingVersion;

		// Token: 0x040017EA RID: 6122
		private MetadataWorkspace _workspace;

		// Token: 0x040017EB RID: 6123
		private readonly Dictionary<EdmMember, KeyValuePair<TypeUsage, TypeUsage>> m_memberMappings = new Dictionary<EdmMember, KeyValuePair<TypeUsage, TypeUsage>>();

		// Token: 0x040017EC RID: 6124
		private ViewLoader _viewLoader;

		// Token: 0x040017ED RID: 6125
		private readonly ConcurrentDictionary<Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind>, ReadOnlyCollection<EdmMember>> _cachedInterestingMembers = new ConcurrentDictionary<Tuple<EntitySetBase, EntityTypeBase, StorageMappingItemCollection.InterestingMembersKind>, ReadOnlyCollection<EdmMember>>();

		// Token: 0x040017EE RID: 6126
		private DbMappingViewCacheFactory _mappingViewCacheFactory;

		// Token: 0x02000B5E RID: 2910
		// (Invoke) Token: 0x060065B4 RID: 26036
		internal delegate bool TryGetUserDefinedQueryView(EntitySetBase extent, out GeneratedView generatedView);

		// Token: 0x02000B5F RID: 2911
		// (Invoke) Token: 0x060065B8 RID: 26040
		internal delegate bool TryGetUserDefinedQueryViewOfType(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> extent, out GeneratedView generatedView);

		// Token: 0x02000B60 RID: 2912
		internal class ViewDictionary
		{
			// Token: 0x060065BB RID: 26043 RVA: 0x0015DE04 File Offset: 0x0015C004
			internal ViewDictionary(StorageMappingItemCollection storageMappingItemCollection, out Dictionary<EntitySetBase, GeneratedView> userDefinedQueryViewsDict, out Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> userDefinedQueryViewsOfTypeDict)
			{
				this._storageMappingItemCollection = storageMappingItemCollection;
				this._generatedViewsMemoizer = new Memoizer<global::System.Data.Entity.Core.Metadata.Edm.EntityContainer, Dictionary<EntitySetBase, GeneratedView>>(new Func<global::System.Data.Entity.Core.Metadata.Edm.EntityContainer, Dictionary<EntitySetBase, GeneratedView>>(this.SerializedGetGeneratedViews), null);
				this._generatedViewOfTypeMemoizer = new Memoizer<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView>(new Func<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView>(this.SerializedGeneratedViewOfType), Pair<EntitySetBase, Pair<EntityTypeBase, bool>>.PairComparer.Instance);
				userDefinedQueryViewsDict = new Dictionary<EntitySetBase, GeneratedView>(EqualityComparer<EntitySetBase>.Default);
				userDefinedQueryViewsOfTypeDict = new Dictionary<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView>(Pair<EntitySetBase, Pair<EntityTypeBase, bool>>.PairComparer.Instance);
				this._tryGetUserDefinedQueryView = new StorageMappingItemCollection.TryGetUserDefinedQueryView(userDefinedQueryViewsDict.TryGetValue);
				this._tryGetUserDefinedQueryViewOfType = new StorageMappingItemCollection.TryGetUserDefinedQueryViewOfType(userDefinedQueryViewsOfTypeDict.TryGetValue);
			}

			// Token: 0x060065BC RID: 26044 RVA: 0x0015DE9C File Offset: 0x0015C09C
			private Dictionary<EntitySetBase, GeneratedView> SerializedGetGeneratedViews(global::System.Data.Entity.Core.Metadata.Edm.EntityContainer container)
			{
				EntityContainerMapping entityContainerMap = MappingMetadataHelper.GetEntityContainerMap(this._storageMappingItemCollection, container);
				global::System.Data.Entity.Core.Metadata.Edm.EntityContainer entityContainer = ((container.DataSpace == DataSpace.CSpace) ? entityContainerMap.StorageEntityContainer : entityContainerMap.EdmEntityContainer);
				Dictionary<EntitySetBase, GeneratedView> dictionary;
				if (this._generatedViewsMemoizer.TryGetValue(entityContainer, out dictionary))
				{
					return dictionary;
				}
				dictionary = new Dictionary<EntitySetBase, GeneratedView>();
				if (!entityContainerMap.HasViews)
				{
					return dictionary;
				}
				if (this._generatedViewsMode && this._storageMappingItemCollection.MappingViewCacheFactory != null)
				{
					this.SerializedCollectViewsFromCache(entityContainerMap, dictionary);
				}
				if (dictionary.Count == 0)
				{
					this._generatedViewsMode = false;
					StorageMappingItemCollection.ViewDictionary.SerializedGenerateViews(entityContainerMap, dictionary);
				}
				return dictionary;
			}

			// Token: 0x060065BD RID: 26045 RVA: 0x0015DF24 File Offset: 0x0015C124
			private static void SerializedGenerateViews(EntityContainerMapping entityContainerMap, Dictionary<EntitySetBase, GeneratedView> resultDictionary)
			{
				ViewGenResults viewGenResults = ViewgenGatekeeper.GenerateViewsFromMapping(entityContainerMap, StorageMappingItemCollection.ViewDictionary._config);
				KeyToListMap<EntitySetBase, GeneratedView> views = viewGenResults.Views;
				if (viewGenResults.HasErrors)
				{
					throw new MappingException(Helper.CombineErrorMessage(viewGenResults.Errors));
				}
				foreach (KeyValuePair<EntitySetBase, List<GeneratedView>> keyValuePair in views.KeyValuePairs)
				{
					GeneratedView generatedView;
					if (!resultDictionary.TryGetValue(keyValuePair.Key, out generatedView))
					{
						generatedView = keyValuePair.Value[0];
						resultDictionary.Add(keyValuePair.Key, generatedView);
					}
				}
			}

			// Token: 0x060065BE RID: 26046 RVA: 0x0015DFC8 File Offset: 0x0015C1C8
			private bool TryGenerateQueryViewOfType(global::System.Data.Entity.Core.Metadata.Edm.EntityContainer entityContainer, EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
			{
				if (type.Abstract)
				{
					generatedView = null;
					return false;
				}
				bool flag;
				ViewGenResults viewGenResults = ViewgenGatekeeper.GenerateTypeSpecificQueryView(MappingMetadataHelper.GetEntityContainerMap(this._storageMappingItemCollection, entityContainer), StorageMappingItemCollection.ViewDictionary._config, entity, type, includeSubtypes, out flag);
				if (!flag)
				{
					generatedView = null;
					return false;
				}
				KeyToListMap<EntitySetBase, GeneratedView> views = viewGenResults.Views;
				if (viewGenResults.HasErrors)
				{
					throw new MappingException(Helper.CombineErrorMessage(viewGenResults.Errors));
				}
				generatedView = views.AllValues.First<GeneratedView>();
				return true;
			}

			// Token: 0x060065BF RID: 26047 RVA: 0x0015E038 File Offset: 0x0015C238
			internal bool TryGetGeneratedViewOfType(EntitySetBase entity, EntityTypeBase type, bool includeSubtypes, out GeneratedView generatedView)
			{
				Pair<EntitySetBase, Pair<EntityTypeBase, bool>> pair = new Pair<EntitySetBase, Pair<EntityTypeBase, bool>>(entity, new Pair<EntityTypeBase, bool>(type, includeSubtypes));
				generatedView = this._generatedViewOfTypeMemoizer.Evaluate(pair);
				return generatedView != null;
			}

			// Token: 0x060065C0 RID: 26048 RVA: 0x0015E068 File Offset: 0x0015C268
			private GeneratedView SerializedGeneratedViewOfType(Pair<EntitySetBase, Pair<EntityTypeBase, bool>> arg)
			{
				GeneratedView generatedView;
				if (this._tryGetUserDefinedQueryViewOfType(arg, out generatedView))
				{
					return generatedView;
				}
				EntitySetBase first = arg.First;
				EntityTypeBase first2 = arg.Second.First;
				bool second = arg.Second.Second;
				if (!this.TryGenerateQueryViewOfType(first.EntityContainer, first, first2, second, out generatedView))
				{
					generatedView = null;
				}
				return generatedView;
			}

			// Token: 0x060065C1 RID: 26049 RVA: 0x0015E0BC File Offset: 0x0015C2BC
			internal GeneratedView GetGeneratedView(EntitySetBase extent, MetadataWorkspace workspace, StorageMappingItemCollection storageMappingItemCollection)
			{
				GeneratedView generatedView;
				if (this._tryGetUserDefinedQueryView(extent, out generatedView))
				{
					return generatedView;
				}
				if (extent.BuiltInTypeKind == BuiltInTypeKind.AssociationSet)
				{
					AssociationSet aSet = (AssociationSet)extent;
					if (aSet.ElementType.IsForeignKey)
					{
						if (StorageMappingItemCollection.ViewDictionary._config.IsViewTracing)
						{
							Helpers.StringTraceLine(string.Empty);
							Helpers.StringTraceLine(string.Empty);
							Helpers.FormatTraceLine("================= Generating FK Query View for: {0} =================", new object[] { aSet.Name });
							Helpers.StringTraceLine(string.Empty);
							Helpers.StringTraceLine(string.Empty);
						}
						global::System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint rc = aSet.ElementType.ReferentialConstraints.Single<global::System.Data.Entity.Core.Metadata.Edm.ReferentialConstraint>();
						EntitySet dependentSet = aSet.AssociationSetEnds[rc.ToRole.Name].EntitySet;
						EntitySet principalSet = aSet.AssociationSetEnds[rc.FromRole.Name].EntitySet;
						DbExpression dbExpression = dependentSet.Scan();
						EntityType dependentType = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)rc.ToRole);
						EntityType principalType = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)rc.FromRole);
						if (dependentSet.ElementType.IsBaseTypeOf(dependentType))
						{
							dbExpression = dbExpression.OfType(TypeUsage.Create(dependentType));
						}
						if (rc.FromRole.RelationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
						{
							dbExpression = dbExpression.Where(delegate(DbExpression e)
							{
								DbExpression dbExpression2 = null;
								foreach (EdmProperty edmProperty in rc.ToProperties)
								{
									DbExpression dbExpression3 = e.Property(edmProperty).IsNull().Not();
									dbExpression2 = ((dbExpression2 == null) ? dbExpression3 : dbExpression2.And(dbExpression3));
								}
								return dbExpression2;
							});
						}
						dbExpression = dbExpression.Select(delegate(DbExpression e)
						{
							List<DbExpression> list = new List<DbExpression>();
							using (ReadOnlyMetadataCollection<AssociationEndMember>.Enumerator enumerator2 = aSet.ElementType.AssociationEndMembers.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current.Name == rc.ToRole.Name)
									{
										List<KeyValuePair<string, DbExpression>> list2 = new List<KeyValuePair<string, DbExpression>>();
										foreach (EdmMember edmMember in dependentSet.ElementType.KeyMembers)
										{
											list2.Add(e.Property((EdmProperty)edmMember));
										}
										list.Add(dependentSet.RefFromKey(DbExpressionBuilder.NewRow(list2), dependentType));
									}
									else
									{
										List<KeyValuePair<string, DbExpression>> list3 = new List<KeyValuePair<string, DbExpression>>();
										foreach (EdmMember edmMember2 in principalSet.ElementType.KeyMembers)
										{
											int num = rc.FromProperties.IndexOf((EdmProperty)edmMember2);
											list3.Add(e.Property(rc.ToProperties[num]));
										}
										list.Add(principalSet.RefFromKey(DbExpressionBuilder.NewRow(list3), principalType));
									}
								}
							}
							return TypeUsage.Create(aSet.ElementType).New(list);
						});
						return GeneratedView.CreateGeneratedViewForFKAssociationSet(aSet, aSet.ElementType, new DbQueryCommandTree(workspace, DataSpace.SSpace, dbExpression), storageMappingItemCollection, StorageMappingItemCollection.ViewDictionary._config);
					}
				}
				if (!this._generatedViewsMemoizer.Evaluate(extent.EntityContainer).TryGetValue(extent, out generatedView))
				{
					throw new InvalidOperationException(Strings.Mapping_Views_For_Extent_Not_Generated((extent.EntityContainer.DataSpace == DataSpace.SSpace) ? "Table" : "EntitySet", extent.Name));
				}
				return generatedView;
			}

			// Token: 0x060065C2 RID: 26050 RVA: 0x0015E2E4 File Offset: 0x0015C4E4
			private void SerializedCollectViewsFromCache(EntityContainerMapping containerMapping, Dictionary<EntitySetBase, GeneratedView> extentMappingViews)
			{
				DbMappingViewCache dbMappingViewCache = this._storageMappingItemCollection.MappingViewCacheFactory.Create(containerMapping);
				if (dbMappingViewCache == null)
				{
					return;
				}
				if (MetadataMappingHasherVisitor.GetMappingClosureHash(containerMapping.StorageMappingItemCollection.MappingVersion, containerMapping, true) != dbMappingViewCache.MappingHashValue)
				{
					throw new MappingException(Strings.ViewGen_HashOnMappingClosure_Not_Matching(dbMappingViewCache.GetType().Name));
				}
				foreach (EntitySetBase entitySetBase in containerMapping.StorageEntityContainer.BaseEntitySets.Union(containerMapping.EdmEntityContainer.BaseEntitySets))
				{
					GeneratedView generatedView;
					if (!extentMappingViews.TryGetValue(entitySetBase, out generatedView))
					{
						DbMappingView view = dbMappingViewCache.GetView(entitySetBase);
						if (view != null)
						{
							generatedView = GeneratedView.CreateGeneratedView(entitySetBase, null, null, view.EntitySql, this._storageMappingItemCollection, new ConfigViewGenerator());
							extentMappingViews.Add(entitySetBase, generatedView);
						}
					}
				}
			}

			// Token: 0x04002DA0 RID: 11680
			private readonly StorageMappingItemCollection.TryGetUserDefinedQueryView _tryGetUserDefinedQueryView;

			// Token: 0x04002DA1 RID: 11681
			private readonly StorageMappingItemCollection.TryGetUserDefinedQueryViewOfType _tryGetUserDefinedQueryViewOfType;

			// Token: 0x04002DA2 RID: 11682
			private readonly StorageMappingItemCollection _storageMappingItemCollection;

			// Token: 0x04002DA3 RID: 11683
			private static readonly ConfigViewGenerator _config = new ConfigViewGenerator();

			// Token: 0x04002DA4 RID: 11684
			private bool _generatedViewsMode = true;

			// Token: 0x04002DA5 RID: 11685
			private readonly Memoizer<global::System.Data.Entity.Core.Metadata.Edm.EntityContainer, Dictionary<EntitySetBase, GeneratedView>> _generatedViewsMemoizer;

			// Token: 0x04002DA6 RID: 11686
			private readonly Memoizer<Pair<EntitySetBase, Pair<EntityTypeBase, bool>>, GeneratedView> _generatedViewOfTypeMemoizer;
		}

		// Token: 0x02000B61 RID: 2913
		internal enum InterestingMembersKind
		{
			// Token: 0x04002DA8 RID: 11688
			RequiredOriginalValueMembers,
			// Token: 0x04002DA9 RID: 11689
			FullUpdate,
			// Token: 0x04002DAA RID: 11690
			PartialUpdate
		}
	}
}
