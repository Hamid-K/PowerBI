using System;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000859 RID: 2137
	internal class RuntimeReferenceCreator : IReferenceCreator
	{
		// Token: 0x06007711 RID: 30481 RVA: 0x001ECFC0 File Offset: 0x001EB1C0
		private RuntimeReferenceCreator()
		{
		}

		// Token: 0x170027CF RID: 10191
		// (get) Token: 0x06007712 RID: 30482 RVA: 0x001ECFC8 File Offset: 0x001EB1C8
		internal static RuntimeReferenceCreator Instance
		{
			get
			{
				return RuntimeReferenceCreator.m_instance;
			}
		}

		// Token: 0x06007713 RID: 30483 RVA: 0x001ECFD0 File Offset: 0x001EB1D0
		public bool TryCreateReference(IStorable refTarget, out BaseReference newReference)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = refTarget.GetObjectType();
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType2;
			if (this.TryMapObjectTypeToReferenceType(objectType, out objectType2))
			{
				return this.TryCreateReference(objectType2, out newReference);
			}
			newReference = null;
			return false;
		}

		// Token: 0x06007714 RID: 30484 RVA: 0x001ECFFC File Offset: 0x001EB1FC
		public bool TryCreateReference(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType referenceObjectType, out BaseReference reference)
		{
			if (referenceObjectType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriGroupLeafObjReference)
			{
				if (referenceObjectType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference)
				{
					if (referenceObjectType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None)
					{
						Global.Tracer.Assert(false, "Cannot create reference to Nothing or Null");
						reference = null;
						return false;
					}
					if (referenceObjectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference)
					{
						reference = new ScalableDictionaryNodeReference();
						return true;
					}
				}
				else
				{
					if (referenceObjectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArrayReference)
					{
						reference = new SimpleReference<StorableArray>(referenceObjectType);
						return true;
					}
					switch (referenceObjectType)
					{
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRowReference:
						reference = new SimpleReference<DataFieldRow>(referenceObjectType);
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortHierarchyObjReference:
						reference = new RuntimeSortHierarchyObjReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortFilterEventInfoReference:
						reference = new SimpleReference<Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.RuntimeSortFilterEventInfo>(referenceObjectType);
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterExpressionScopeObjReference:
						reference = new SortFilterExpressionScopeObjReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortExpressionScopeInstanceHolderReference:
						reference = new SortExpressionScopeInstanceHolderReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjReference:
						reference = new SimpleReference<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(referenceObjectType);
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRICollectionReference:
						reference = new SimpleReference<RuntimeRICollection>(referenceObjectType);
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixCellReference:
						reference = new RuntimeTablixCellReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriCellReference:
						reference = new RuntimeChartCriCellReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeUserSortTargetInfoReference:
						reference = new SimpleReference<Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing.RuntimeUserSortTargetInfo>(referenceObjectType);
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AggregateRowReference:
						reference = new SimpleReference<AggregateRow>(referenceObjectType);
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCellsReference:
						reference = new SimpleReference<RuntimeCells>(referenceObjectType);
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObjReference:
						reference = new RuntimeHierarchyObjReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjReference:
						reference = new SimpleReference<RuntimeGroupingObj>(referenceObjectType);
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupRootObjReference:
						reference = new RuntimeDataTablixGroupRootObjReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixMemberObjReference:
						reference = new RuntimeDataTablixMemberObjReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixObjReference:
						reference = new RuntimeTablixObjReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartObjReference:
						reference = new RuntimeChartObjReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCriObjReference:
						reference = new RuntimeCriObjReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixGroupLeafObjReference:
						reference = new RuntimeTablixGroupLeafObjReference();
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriGroupLeafObjReference:
						reference = new RuntimeChartCriGroupLeafObjReference();
						return true;
					}
				}
			}
			else if (referenceObjectType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGaugePanelObjReference)
			{
				switch (referenceObjectType)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeOnDemandDataSetObjReference:
					reference = new RuntimeOnDemandDataSetObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IHierarchyObjReference:
					reference = new SimpleReference<IHierarchyObj>(referenceObjectType);
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCellReference:
					reference = new RuntimeCellReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRDLDataRegionObjReference:
					reference = new SimpleReference<RuntimeRDLDataRegionObj>(referenceObjectType);
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IScopeReference:
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ISortDataHolderReference:
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObj:
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupLeafObjReference:
					reference = new RuntimeDataTablixGroupLeafObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObjReference:
					reference = new RuntimeGroupLeafObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupObjReference:
					reference = new RuntimeGroupObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDetailObjReference:
					reference = new RuntimeDetailObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupRootObjReference:
					reference = new RuntimeGroupRootObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMemberObjReference:
					reference = new RuntimeMemberObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixObjReference:
					reference = new RuntimeDataTablixObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriObjReference:
					reference = new RuntimeChartCriObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObjReference:
					reference = new RuntimeDataRegionObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateReference:
					reference = new SimpleReference<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregate>(referenceObjectType);
					return true;
				default:
					if (referenceObjectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGaugePanelObjReference)
					{
						reference = new RuntimeGaugePanelObjReference();
						return true;
					}
					break;
				}
			}
			else
			{
				if (referenceObjectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTableReference)
				{
					reference = new SimpleReference<LookupTable>(referenceObjectType);
					return true;
				}
				if (referenceObjectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMapDataRegionObjReference)
				{
					reference = new RuntimeMapDataRegionObjReference();
					return true;
				}
				switch (referenceObjectType)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeObjReference:
					reference = new RuntimeDataShapeObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeGroupLeafObjReference:
					reference = new RuntimeDataShapeGroupLeafObjReference();
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeIntersectionReference:
					reference = new RuntimeDataShapeIntersectionReference();
					return true;
				}
			}
			reference = null;
			return false;
		}

		// Token: 0x06007715 RID: 30485 RVA: 0x001ED338 File Offset: 0x001EB538
		private bool TryMapObjectTypeToReferenceType(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType targetType, out Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType referenceType)
		{
			if (targetType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupLeafObj)
			{
				if (targetType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArray)
				{
					if (targetType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNode)
					{
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableDictionaryNodeReference;
						return true;
					}
					if (targetType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArray)
					{
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StorableArrayReference;
						return true;
					}
				}
				else
				{
					switch (targetType)
					{
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRow:
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRowReference;
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FieldImpl:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNode:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeTupleList:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeTuple:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNodeValue:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableHybridListEntry:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FilterKey:
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortHierarchyStruct:
						break;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortHierarchyObj:
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortHierarchyObjReference;
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortFilterEventInfo:
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortFilterEventInfoReference;
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterExpressionScopeObj:
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterExpressionScopeObjReference;
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortExpressionScopeInstanceHolder:
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortExpressionScopeInstanceHolderReference;
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj:
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjReference;
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRICollection:
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRICollectionReference;
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixCell:
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixCellReference;
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriCell:
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriCellReference;
						return true;
					case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeUserSortTargetInfo:
						referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeUserSortTargetInfoReference;
						return true;
					default:
						switch (targetType)
						{
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AggregateRow:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AggregateRowReference;
							return true;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCells:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCellsReference;
							return true;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeExpressionInfo:
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Int32StringHashtable:
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantRifObjectDictionary:
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantListOfRifObjectDictionary:
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AggregatesImpl:
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixMemberObj:
							break;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObj:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObjReference;
							return true;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjReference;
							return true;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupRootObj:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupRootObjReference;
							return true;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixMemberObj:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixMemberObjReference;
							return true;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixObj:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixObjReference;
							return true;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixObj:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixObjReference;
							return true;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartObj:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartObjReference;
							return true;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCriObj:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCriObjReference;
							return true;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixGroupLeafObj:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixGroupLeafObjReference;
							return true;
						case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriGroupLeafObj:
							referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriGroupLeafObjReference;
							return true;
						default:
							switch (targetType)
							{
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ObjectModelImpl:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeOnDemandDataSetObjReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeOnDemandDataSetObj:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeOnDemandDataSetObjReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IHierarchyObj:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IHierarchyObjReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRDLDataRegionObj:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRDLDataRegionObjReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCell:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCellReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IScope:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IScopeReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObj:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObjReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupObj:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupObjReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDetailObj:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDetailObjReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupRootObj:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupRootObjReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMemberObj:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMemberObjReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriObj:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriObjReference;
								return true;
							case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupLeafObj:
								referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupLeafObjReference;
								return true;
							}
							break;
						}
						break;
					}
				}
			}
			else if (targetType <= Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGaugePanelObj)
			{
				if (targetType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObj)
				{
					referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObjReference;
					return true;
				}
				if (targetType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGaugePanelObj)
				{
					referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGaugePanelObjReference;
					return true;
				}
			}
			else
			{
				if (targetType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTable)
				{
					referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTableReference;
					return true;
				}
				if (targetType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMapDataRegionObj)
				{
					referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMapDataRegionObjReference;
					return true;
				}
				switch (targetType)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeObj:
					referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeObjReference;
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeGroupLeafObj:
					referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeGroupLeafObjReference;
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeIntersection:
					referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeIntersectionReference;
					return true;
				}
			}
			referenceType = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None;
			return false;
		}

		// Token: 0x04003C42 RID: 15426
		private static RuntimeReferenceCreator m_instance = new RuntimeReferenceCreator();
	}
}
