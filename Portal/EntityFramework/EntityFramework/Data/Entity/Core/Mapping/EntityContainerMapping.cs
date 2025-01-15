using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000528 RID: 1320
	public class EntityContainerMapping : MappingBase
	{
		// Token: 0x06004109 RID: 16649 RVA: 0x000DC5F4 File Offset: 0x000DA7F4
		public EntityContainerMapping(EntityContainer conceptualEntityContainer, EntityContainer storeEntityContainer, StorageMappingItemCollection mappingItemCollection, bool generateUpdateViews)
			: this(conceptualEntityContainer, storeEntityContainer, mappingItemCollection, true, generateUpdateViews)
		{
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x000DC604 File Offset: 0x000DA804
		internal EntityContainerMapping(EntityContainer entityContainer, EntityContainer storageEntityContainer, StorageMappingItemCollection storageMappingItemCollection, bool validate, bool generateUpdateViews)
		{
			this.m_entitySetMappings = new Dictionary<string, EntitySetBaseMapping>(StringComparer.Ordinal);
			this.m_associationSetMappings = new Dictionary<string, EntitySetBaseMapping>(StringComparer.Ordinal);
			this.m_functionImportMappings = new Dictionary<EdmFunction, FunctionImportMapping>();
			base..ctor(MetadataItem.MetadataFlags.CSSpace);
			Check.NotNull<EntityContainer>(entityContainer, "entityContainer");
			this.m_entityContainer = entityContainer;
			this.m_storageEntityContainer = storageEntityContainer;
			this.m_storageMappingItemCollection = storageMappingItemCollection;
			this.m_memoizedCellGroupEvaluator = new Memoizer<InputForComputingCellGroups, OutputFromComputeCellGroups>(new Func<InputForComputingCellGroups, OutputFromComputeCellGroups>(this.ComputeCellGroups), default(InputForComputingCellGroups));
			this.identity = entityContainer.Identity;
			this.m_validate = validate;
			this.m_generateUpdateViews = generateUpdateViews;
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x000DC6A5 File Offset: 0x000DA8A5
		internal EntityContainerMapping(EntityContainer entityContainer)
			: this(entityContainer, null, null, false, false)
		{
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x000DC6B2 File Offset: 0x000DA8B2
		internal EntityContainerMapping()
		{
			this.m_entitySetMappings = new Dictionary<string, EntitySetBaseMapping>(StringComparer.Ordinal);
			this.m_associationSetMappings = new Dictionary<string, EntitySetBaseMapping>(StringComparer.Ordinal);
			this.m_functionImportMappings = new Dictionary<EdmFunction, FunctionImportMapping>();
			base..ctor();
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x000DC6E5 File Offset: 0x000DA8E5
		public StorageMappingItemCollection MappingItemCollection
		{
			get
			{
				return this.m_storageMappingItemCollection;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x0600410E RID: 16654 RVA: 0x000DC6ED File Offset: 0x000DA8ED
		internal StorageMappingItemCollection StorageMappingItemCollection
		{
			get
			{
				return this.MappingItemCollection;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x000DC6F5 File Offset: 0x000DA8F5
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataItem;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004110 RID: 16656 RVA: 0x000DC6F9 File Offset: 0x000DA8F9
		internal override MetadataItem EdmItem
		{
			get
			{
				return this.m_entityContainer;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004111 RID: 16657 RVA: 0x000DC701 File Offset: 0x000DA901
		internal override string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004112 RID: 16658 RVA: 0x000DC709 File Offset: 0x000DA909
		internal bool IsEmpty
		{
			get
			{
				return this.m_entitySetMappings.Count == 0 && this.m_associationSetMappings.Count == 0;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004113 RID: 16659 RVA: 0x000DC728 File Offset: 0x000DA928
		internal bool HasViews
		{
			get
			{
				if (!this.HasMappingFragments())
				{
					return this.AllSetMaps.Any((EntitySetBaseMapping setMap) => setMap.QueryView != null);
				}
				return true;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004114 RID: 16660 RVA: 0x000DC75E File Offset: 0x000DA95E
		// (set) Token: 0x06004115 RID: 16661 RVA: 0x000DC766 File Offset: 0x000DA966
		internal string SourceLocation { get; set; }

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004116 RID: 16662 RVA: 0x000DC76F File Offset: 0x000DA96F
		public EntityContainer ConceptualEntityContainer
		{
			get
			{
				return this.m_entityContainer;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004117 RID: 16663 RVA: 0x000DC777 File Offset: 0x000DA977
		internal EntityContainer EdmEntityContainer
		{
			get
			{
				return this.ConceptualEntityContainer;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x000DC77F File Offset: 0x000DA97F
		public EntityContainer StoreEntityContainer
		{
			get
			{
				return this.m_storageEntityContainer;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x000DC787 File Offset: 0x000DA987
		internal EntityContainer StorageEntityContainer
		{
			get
			{
				return this.StoreEntityContainer;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x000DC78F File Offset: 0x000DA98F
		internal ReadOnlyCollection<EntitySetBaseMapping> EntitySetMaps
		{
			get
			{
				return new ReadOnlyCollection<EntitySetBaseMapping>(new List<EntitySetBaseMapping>(this.m_entitySetMappings.Values));
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x000DC7A6 File Offset: 0x000DA9A6
		public virtual IEnumerable<EntitySetMapping> EntitySetMappings
		{
			get
			{
				return this.EntitySetMaps.OfType<EntitySetMapping>();
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x0600411C RID: 16668 RVA: 0x000DC7B3 File Offset: 0x000DA9B3
		public virtual IEnumerable<AssociationSetMapping> AssociationSetMappings
		{
			get
			{
				return this.RelationshipSetMaps.OfType<AssociationSetMapping>();
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x0600411D RID: 16669 RVA: 0x000DC7C0 File Offset: 0x000DA9C0
		public IEnumerable<FunctionImportMapping> FunctionImportMappings
		{
			get
			{
				return this.m_functionImportMappings.Values;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x0600411E RID: 16670 RVA: 0x000DC7CD File Offset: 0x000DA9CD
		internal ReadOnlyCollection<EntitySetBaseMapping> RelationshipSetMaps
		{
			get
			{
				return new ReadOnlyCollection<EntitySetBaseMapping>(new List<EntitySetBaseMapping>(this.m_associationSetMappings.Values));
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x000DC7E4 File Offset: 0x000DA9E4
		internal IEnumerable<EntitySetBaseMapping> AllSetMaps
		{
			get
			{
				return this.m_entitySetMappings.Values.Concat(this.m_associationSetMappings.Values);
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004120 RID: 16672 RVA: 0x000DC801 File Offset: 0x000DAA01
		// (set) Token: 0x06004121 RID: 16673 RVA: 0x000DC809 File Offset: 0x000DAA09
		internal int StartLineNumber { get; set; }

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004122 RID: 16674 RVA: 0x000DC812 File Offset: 0x000DAA12
		// (set) Token: 0x06004123 RID: 16675 RVA: 0x000DC81A File Offset: 0x000DAA1A
		internal int StartLinePosition { get; set; }

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x000DC823 File Offset: 0x000DAA23
		internal bool Validate
		{
			get
			{
				return this.m_validate;
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x000DC82B File Offset: 0x000DAA2B
		public bool GenerateUpdateViews
		{
			get
			{
				return this.m_generateUpdateViews;
			}
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x000DC834 File Offset: 0x000DAA34
		internal EntitySetBaseMapping GetEntitySetMapping(string setName)
		{
			EntitySetBaseMapping entitySetBaseMapping = null;
			this.m_entitySetMappings.TryGetValue(setName, out entitySetBaseMapping);
			return entitySetBaseMapping;
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x000DC854 File Offset: 0x000DAA54
		internal EntitySetBaseMapping GetAssociationSetMapping(string setName)
		{
			EntitySetBaseMapping entitySetBaseMapping = null;
			this.m_associationSetMappings.TryGetValue(setName, out entitySetBaseMapping);
			return entitySetBaseMapping;
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x000DC874 File Offset: 0x000DAA74
		internal IEnumerable<AssociationSetMapping> GetRelationshipSetMappingsFor(EntitySetBase edmEntitySet, EntitySetBase storeEntitySet)
		{
			Func<AssociationSetEnd, bool> <>9__2;
			return (from AssociationSetMapping w in this.m_associationSetMappings.Values
				where w.StoreEntitySet != null && w.StoreEntitySet == storeEntitySet
				select w).Where(delegate(AssociationSetMapping associationSetMap)
			{
				IEnumerable<AssociationSetEnd> associationSetEnds = (associationSetMap.Set as AssociationSet).AssociationSetEnds;
				Func<AssociationSetEnd, bool> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (AssociationSetEnd associationSetEnd) => associationSetEnd.EntitySet == edmEntitySet);
				}
				return associationSetEnds.Any(func);
			});
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x000DC8C8 File Offset: 0x000DAAC8
		internal EntitySetBaseMapping GetSetMapping(string setName)
		{
			EntitySetBaseMapping entitySetBaseMapping = this.GetEntitySetMapping(setName);
			if (entitySetBaseMapping == null)
			{
				entitySetBaseMapping = this.GetAssociationSetMapping(setName);
			}
			return entitySetBaseMapping;
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x000DC8EC File Offset: 0x000DAAEC
		public void AddSetMapping(EntitySetMapping setMapping)
		{
			Check.NotNull<EntitySetMapping>(setMapping, "setMapping");
			Util.ThrowIfReadOnly(this);
			if (!this.m_entitySetMappings.ContainsKey(setMapping.Set.Name))
			{
				this.m_entitySetMappings.Add(setMapping.Set.Name, setMapping);
			}
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x000DC93A File Offset: 0x000DAB3A
		public void RemoveSetMapping(EntitySetMapping setMapping)
		{
			Check.NotNull<EntitySetMapping>(setMapping, "setMapping");
			Util.ThrowIfReadOnly(this);
			this.m_entitySetMappings.Remove(setMapping.Set.Name);
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x000DC968 File Offset: 0x000DAB68
		public void AddSetMapping(AssociationSetMapping setMapping)
		{
			Check.NotNull<AssociationSetMapping>(setMapping, "setMapping");
			Util.ThrowIfReadOnly(this);
			if (!this.m_associationSetMappings.ContainsKey(setMapping.Set.Name))
			{
				this.m_associationSetMappings.Add(setMapping.Set.Name, setMapping);
			}
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x000DC9B6 File Offset: 0x000DABB6
		public void RemoveSetMapping(AssociationSetMapping setMapping)
		{
			Check.NotNull<AssociationSetMapping>(setMapping, "setMapping");
			Util.ThrowIfReadOnly(this);
			this.m_associationSetMappings.Remove(setMapping.Set.Name);
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x000DC9E1 File Offset: 0x000DABE1
		internal bool ContainsAssociationSetMapping(AssociationSet associationSet)
		{
			return this.m_associationSetMappings.ContainsKey(associationSet.Name);
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x000DC9F4 File Offset: 0x000DABF4
		public void AddFunctionImportMapping(FunctionImportMapping functionImportMapping)
		{
			Check.NotNull<FunctionImportMapping>(functionImportMapping, "functionImportMapping");
			Util.ThrowIfReadOnly(this);
			this.m_functionImportMappings.Add(functionImportMapping.FunctionImport, functionImportMapping);
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x000DCA1A File Offset: 0x000DAC1A
		public void RemoveFunctionImportMapping(FunctionImportMapping functionImportMapping)
		{
			Check.NotNull<FunctionImportMapping>(functionImportMapping, "functionImportMapping");
			Util.ThrowIfReadOnly(this);
			this.m_functionImportMappings.Remove(functionImportMapping.FunctionImport);
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x000DCA40 File Offset: 0x000DAC40
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this.m_entitySetMappings.Values);
			MappingItem.SetReadOnly(this.m_associationSetMappings.Values);
			MappingItem.SetReadOnly(this.m_functionImportMappings.Values);
			base.SetReadOnly();
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x000DCA78 File Offset: 0x000DAC78
		internal bool HasQueryViewForSetMap(string setName)
		{
			EntitySetBaseMapping setMapping = this.GetSetMapping(setName);
			return setMapping != null && setMapping.QueryView != null;
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x000DCA9C File Offset: 0x000DAC9C
		internal bool HasMappingFragments()
		{
			foreach (EntitySetBaseMapping entitySetBaseMapping in this.AllSetMaps)
			{
				using (IEnumerator<TypeMapping> enumerator2 = entitySetBaseMapping.TypeMappings.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.MappingFragments.Count > 0)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x000DCB28 File Offset: 0x000DAD28
		internal virtual bool TryGetFunctionImportMapping(EdmFunction functionImport, out FunctionImportMapping mapping)
		{
			return this.m_functionImportMappings.TryGetValue(functionImport, out mapping);
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x000DCB37 File Offset: 0x000DAD37
		internal OutputFromComputeCellGroups GetCellgroups(InputForComputingCellGroups args)
		{
			return this.m_memoizedCellGroupEvaluator.Evaluate(args);
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x000DCB48 File Offset: 0x000DAD48
		private OutputFromComputeCellGroups ComputeCellGroups(InputForComputingCellGroups args)
		{
			OutputFromComputeCellGroups outputFromComputeCellGroups = default(OutputFromComputeCellGroups);
			outputFromComputeCellGroups.Success = true;
			CellCreator cellCreator = new CellCreator(args.ContainerMapping);
			outputFromComputeCellGroups.Cells = cellCreator.GenerateCells();
			outputFromComputeCellGroups.Identifiers = cellCreator.Identifiers;
			if (outputFromComputeCellGroups.Cells.Count <= 0)
			{
				outputFromComputeCellGroups.Success = false;
				return outputFromComputeCellGroups;
			}
			outputFromComputeCellGroups.ForeignKeyConstraints = ForeignConstraint.GetForeignConstraints(args.ContainerMapping.StorageEntityContainer);
			List<Set<Cell>> list = new CellPartitioner(outputFromComputeCellGroups.Cells, outputFromComputeCellGroups.ForeignKeyConstraints).GroupRelatedCells();
			outputFromComputeCellGroups.CellGroups = list.Select((Set<Cell> setOfCells) => new Set<Cell>(setOfCells.Select((Cell cell) => new Cell(cell)))).ToList<Set<Cell>>();
			return outputFromComputeCellGroups;
		}

		// Token: 0x0400168E RID: 5774
		private readonly string identity;

		// Token: 0x0400168F RID: 5775
		private readonly bool m_validate;

		// Token: 0x04001690 RID: 5776
		private readonly bool m_generateUpdateViews;

		// Token: 0x04001691 RID: 5777
		private readonly EntityContainer m_entityContainer;

		// Token: 0x04001692 RID: 5778
		private readonly EntityContainer m_storageEntityContainer;

		// Token: 0x04001693 RID: 5779
		private readonly Dictionary<string, EntitySetBaseMapping> m_entitySetMappings;

		// Token: 0x04001694 RID: 5780
		private readonly Dictionary<string, EntitySetBaseMapping> m_associationSetMappings;

		// Token: 0x04001695 RID: 5781
		private readonly Dictionary<EdmFunction, FunctionImportMapping> m_functionImportMappings;

		// Token: 0x04001696 RID: 5782
		private readonly StorageMappingItemCollection m_storageMappingItemCollection;

		// Token: 0x04001697 RID: 5783
		private readonly Memoizer<InputForComputingCellGroups, OutputFromComputeCellGroups> m_memoizedCellGroupEvaluator;
	}
}
