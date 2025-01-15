using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000858 RID: 2136
	internal sealed class StorageObjectCreator : IScalabilityObjectCreator
	{
		// Token: 0x0600770B RID: 30475 RVA: 0x001EC639 File Offset: 0x001EA839
		private StorageObjectCreator()
		{
		}

		// Token: 0x0600770C RID: 30476 RVA: 0x001EC644 File Offset: 0x001EA844
		public bool TryCreateObject(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType, out Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable persistObj)
		{
			if (objectType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGaugePanelObj)
			{
				if (objectType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeLookupTable)
				{
					switch (objectType)
					{
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorageItem:
						persistObj = new StorageItem();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Reference:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IScalableDictionaryEntry:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryValues:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNode:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionary:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArray:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArrayReference:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Array2D:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionInstanceReference:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInstanceReference:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportInstanceReference:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeInstanceReference:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeValue:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableHybridListEntry:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantVariantHashtable:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VarBase:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Int32StringHashtable:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantRifObjectDictionary:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantListOfRifObjectDictionary:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AggregatesImpl:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixMemberObj:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixObj:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantListVariantDictionary:
						break;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRow:
						persistObj = new DataFieldRow();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FieldImpl:
						persistObj = new FieldImpl();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNode:
						persistObj = new BTreeNode();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeTupleList:
						persistObj = new BTreeNodeTupleList();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeTuple:
						persistObj = new BTreeNodeTuple();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FilterKey:
						persistObj = new Filters.FilterKey();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortHierarchyObj:
						persistObj = new RuntimeSortHierarchyObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortHierarchyStruct:
						persistObj = new RuntimeSortHierarchyObj.SortHierarchyStructure();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortFilterEventInfo:
						persistObj = new RuntimeSortFilterEventInfo();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterExpressionScopeObj:
						persistObj = new RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortExpressionScopeInstanceHolder:
						persistObj = new RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj:
						persistObj = new DataAggregateObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRICollection:
						persistObj = new RuntimeRICollection();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixCell:
						persistObj = new RuntimeTablixCell();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriCell:
						persistObj = new RuntimeChartCriCell();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeUserSortTargetInfo:
						persistObj = new RuntimeUserSortTargetInfo();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Aggregate:
						persistObj = new Aggregate();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.First:
						persistObj = new First();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Last:
						persistObj = new Last();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Sum:
						persistObj = new Sum();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Avg:
						persistObj = new Avg();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Max:
						persistObj = new Max();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Min:
						persistObj = new Min();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Count:
						persistObj = new Count();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CountDistinct:
						persistObj = new CountDistinct();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CountRows:
						persistObj = new CountRows();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Var:
						persistObj = new Var();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StDev:
						persistObj = new StDev();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VarP:
						persistObj = new VarP();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StDevP:
						persistObj = new StDevP();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Previous:
						persistObj = new Previous();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AggregateRow:
						persistObj = new AggregateRow();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCells:
						persistObj = new RuntimeCells();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeExpressionInfo:
						persistObj = new RuntimeExpressionInfo();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObj:
						persistObj = new RuntimeHierarchyObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupRootObj:
						persistObj = new RuntimeDataTablixGroupRootObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixMemberObj:
						persistObj = new RuntimeDataTablixMemberObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixObj:
						persistObj = new RuntimeTablixObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartObj:
						persistObj = new RuntimeChartObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCriObj:
						persistObj = new RuntimeCriObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixGroupLeafObj:
						persistObj = new RuntimeTablixGroupLeafObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriGroupLeafObj:
						persistObj = new RuntimeChartCriGroupLeafObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CalculatedFieldWrapperImpl:
						persistObj = new CalculatedFieldWrapperImpl();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortDataHolder:
						persistObj = new RuntimeSortDataHolder();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTree:
						persistObj = new BTree();
						return true;
					default:
						if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeHierarchyObj)
						{
							persistObj = new BTreeNodeHierarchyObj();
							return true;
						}
						if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeLookupTable)
						{
							persistObj = new ScopeLookupTable();
							return true;
						}
						break;
					}
				}
				else
				{
					if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionMemberInstance)
					{
						persistObj = new DataRegionMemberInstance();
						return true;
					}
					if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjResult)
					{
						persistObj = new DataAggregateObjResult();
						return true;
					}
					if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGaugePanelObj)
					{
						persistObj = new RuntimeGaugePanelObj();
						return true;
					}
				}
			}
			else if (objectType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMapDataRegionObj)
			{
				if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChildLeafInfo)
				{
					persistObj = new ChildLeafInfo();
					return true;
				}
				switch (objectType)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTable:
					persistObj = new LookupTable();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTableReference:
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupObjResult:
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupMatches:
					persistObj = new LookupMatches();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupMatchesWithRows:
					persistObj = new LookupMatchesWithRows();
					return true;
				default:
					if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMapDataRegionObj)
					{
						persistObj = new RuntimeMapDataRegionObj();
						return true;
					}
					break;
				}
			}
			else if (objectType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjBucket)
			{
				if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Union)
				{
					persistObj = new Union();
					return true;
				}
				switch (objectType)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataScopeInfo:
					persistObj = new DataScopeInfo();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateObjs:
					persistObj = new BucketedDataAggregateObjs();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjBucket:
					persistObj = new DataAggregateObjBucket();
					return true;
				}
			}
			else
			{
				switch (objectType)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortScopeValuesHolder:
					persistObj = new RuntimeSortFilterEventInfo.SortScopeValuesHolder();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRowSortHierarchyObj:
					persistObj = new RuntimeDataRowSortHierarchyObj();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SyntheticTriangulatedCellReference:
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjHash:
					persistObj = new RuntimeGroupingObjHash();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjTree:
					persistObj = new RuntimeGroupingObjTree();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjDetail:
					persistObj = new RuntimeGroupingObjDetail();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjLinkedList:
					persistObj = new RuntimeGroupingObjLinkedList();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjDetailUserSort:
					persistObj = new RuntimeGroupingObjDetailUserSort();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjNaturalGroup:
					persistObj = new RuntimeGroupingObjNaturalGroup();
					return true;
				default:
					switch (objectType)
					{
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeObj:
						persistObj = new RuntimeDataShapeObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeGroupLeafObj:
						persistObj = new RuntimeDataShapeGroupLeafObj();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeIntersection:
						persistObj = new RuntimeDataShapeIntersection();
						return true;
					}
					break;
				}
			}
			persistObj = null;
			return false;
		}

		// Token: 0x0600770D RID: 30477 RVA: 0x001ECBC6 File Offset: 0x001EADC6
		public List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> GetDeclarations()
		{
			return StorageObjectCreator.m_declarations;
		}

		// Token: 0x170027CE RID: 10190
		// (get) Token: 0x0600770E RID: 30478 RVA: 0x001ECBCD File Offset: 0x001EADCD
		internal static StorageObjectCreator Instance
		{
			get
			{
				if (StorageObjectCreator.m_instance == null)
				{
					StorageObjectCreator.m_instance = new StorageObjectCreator();
				}
				return StorageObjectCreator.m_instance;
			}
		}

		// Token: 0x0600770F RID: 30479 RVA: 0x001ECBE8 File Offset: 0x001EADE8
		private static List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> BuildDeclarations()
		{
			return new List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration>(86)
			{
				Aggregate.GetDeclaration(),
				AggregateRow.GetDeclaration(),
				Avg.GetDeclaration(),
				BTree.GetDeclaration(),
				BTreeNode.GetDeclaration(),
				BTreeNodeTuple.GetDeclaration(),
				BTreeNodeTupleList.GetDeclaration(),
				BTreeNodeHierarchyObj.GetDeclaration(),
				CalculatedFieldWrapperImpl.GetDeclaration(),
				ChildLeafInfo.GetDeclaration(),
				Count.GetDeclaration(),
				CountDistinct.GetDeclaration(),
				CountRows.GetDeclaration(),
				DataAggregateObj.GetDeclaration(),
				DataAggregateObjResult.GetDeclaration(),
				DataRegionMemberInstance.GetDeclaration(),
				DataFieldRow.GetDeclaration(),
				FieldImpl.GetDeclaration(),
				First.GetDeclaration(),
				Last.GetDeclaration(),
				Max.GetDeclaration(),
				Min.GetDeclaration(),
				Previous.GetDeclaration(),
				RuntimeCell.GetDeclaration(),
				RuntimeCells.GetDeclaration(),
				RuntimeCellWithContents.GetDeclaration(),
				RuntimeChartCriCell.GetDeclaration(),
				RuntimeChartCriGroupLeafObj.GetDeclaration(),
				RuntimeChartCriObj.GetDeclaration(),
				RuntimeChartObj.GetDeclaration(),
				RuntimeCriObj.GetDeclaration(),
				RuntimeDataRegionObj.GetDeclaration(),
				RuntimeDataShapeGroupLeafObj.GetDeclaration(),
				RuntimeDataShapeIntersection.GetDeclaration(),
				RuntimeDataShapeObj.GetDeclaration(),
				RuntimeDataTablixObj.GetDeclaration(),
				RuntimeDataTablixGroupLeafObj.GetDeclaration(),
				RuntimeDataTablixGroupRootObj.GetDeclaration(),
				RuntimeDataTablixMemberObj.GetDeclaration(),
				RuntimeDataTablixWithScopedItemsObj.GetDeclaration(),
				RuntimeDataTablixWithScopedItemsGroupLeafObj.GetDeclaration(),
				RuntimeDetailObj.GetDeclaration(),
				RuntimeExpressionInfo.GetDeclaration(),
				RuntimeGroupLeafObj.GetDeclaration(),
				RuntimeGroupObj.GetDeclaration(),
				RuntimeGroupRootObj.GetDeclaration(),
				RuntimeGroupingObj.GetDeclaration(),
				RuntimeHierarchyObj.GetDeclaration(),
				RuntimeMemberObj.GetDeclaration(),
				RuntimeRDLDataRegionObj.GetDeclaration(),
				RuntimeRICollection.GetDeclaration(),
				RuntimeSortDataHolder.GetDeclaration(),
				RuntimeSortFilterEventInfo.GetDeclaration(),
				RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder.GetDeclaration(),
				RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj.GetDeclaration(),
				RuntimeSortFilterEventInfo.SortScopeValuesHolder.GetDeclaration(),
				RuntimeSortHierarchyObj.GetDeclaration(),
				RuntimeSortHierarchyObj.SortHierarchyStructure.GetDeclaration(),
				RuntimeDataRowSortHierarchyObj.GetDeclaration(),
				RuntimeTablixCell.GetDeclaration(),
				RuntimeTablixGroupLeafObj.GetDeclaration(),
				RuntimeTablixObj.GetDeclaration(),
				RuntimeUserSortTargetInfo.GetDeclaration(),
				ScopeInstance.GetDeclaration(),
				ScopeLookupTable.GetDeclaration(),
				StDev.GetDeclaration(),
				StDevP.GetDeclaration(),
				Sum.GetDeclaration(),
				Var.GetDeclaration(),
				VarBase.GetDeclaration(),
				VarP.GetDeclaration(),
				Filters.FilterKey.GetDeclaration(),
				RuntimeGaugePanelObj.GetDeclaration(),
				LookupMatches.GetDeclaration(),
				LookupMatchesWithRows.GetDeclaration(),
				LookupTable.GetDeclaration(),
				RuntimeMapDataRegionObj.GetDeclaration(),
				DataScopeInfo.GetDeclaration(),
				BucketedDataAggregateObjs.GetDeclaration(),
				DataAggregateObjBucket.GetDeclaration(),
				RuntimeGroupingObjHash.GetDeclaration(),
				RuntimeGroupingObjTree.GetDeclaration(),
				RuntimeGroupingObjDetail.GetDeclaration(),
				RuntimeGroupingObjDetailUserSort.GetDeclaration(),
				RuntimeGroupingObjLinkedList.GetDeclaration(),
				RuntimeGroupingObjNaturalGroup.GetDeclaration()
			};
		}

		// Token: 0x04003C40 RID: 15424
		private static List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration> m_declarations = StorageObjectCreator.BuildDeclarations();

		// Token: 0x04003C41 RID: 15425
		private static StorageObjectCreator m_instance = null;
	}
}
