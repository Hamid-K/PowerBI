using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008F6 RID: 2294
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeRICollection : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007E42 RID: 32322 RVA: 0x00208D7F File Offset: 0x00206F7F
		internal RuntimeRICollection()
		{
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x00208D87 File Offset: 0x00206F87
		internal RuntimeRICollection(int capacity)
		{
			this.m_dataRegionObjs = new List<RuntimeDataTablixObjReference>(capacity);
		}

		// Token: 0x06007E44 RID: 32324 RVA: 0x00208D9B File Offset: 0x00206F9B
		internal RuntimeRICollection(IReference<IScope> owner, List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> reportItems, ref DataActions dataAction, OnDemandProcessingContext odpContext)
		{
			this.m_dataRegionObjs = new List<RuntimeDataTablixObjReference>();
			this.AddItems(owner, reportItems, ref dataAction, odpContext);
		}

		// Token: 0x06007E45 RID: 32325 RVA: 0x00208DBC File Offset: 0x00206FBC
		internal RuntimeRICollection(IReference<IScope> outerScope, List<Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion> dataRegionDefs, OnDemandProcessingContext odpContext, bool onePass)
		{
			this.m_dataRegionObjs = new List<RuntimeDataTablixObjReference>(dataRegionDefs.Count);
			DataActions dataActions = DataActions.None;
			for (int i = 0; i < dataRegionDefs.Count; i++)
			{
				this.CreateDataRegions(outerScope, dataRegionDefs[i], odpContext, onePass, ref dataActions);
			}
		}

		// Token: 0x06007E46 RID: 32326 RVA: 0x00208E06 File Offset: 0x00207006
		public void AddItems(IReference<IScope> owner, List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> reportItems, ref DataActions dataAction, OnDemandProcessingContext odpContext)
		{
			if (reportItems != null && reportItems.Count > 0)
			{
				this.CreateDataRegions(owner, reportItems, odpContext, false, ref dataAction);
			}
		}

		// Token: 0x06007E47 RID: 32327 RVA: 0x00208E20 File Offset: 0x00207020
		private void CreateDataRegions(IReference<IScope> owner, List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> computedRIs, OnDemandProcessingContext odpContext, bool onePass, ref DataActions dataAction)
		{
			if (computedRIs == null)
			{
				return;
			}
			for (int i = 0; i < computedRIs.Count; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem = computedRIs[i];
				this.CreateDataRegions(owner, reportItem, odpContext, onePass, ref dataAction);
			}
		}

		// Token: 0x06007E48 RID: 32328 RVA: 0x00208E58 File Offset: 0x00207058
		private void CreateDataRegions(IReference<IScope> owner, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem, OnDemandProcessingContext odpContext, bool onePass, ref DataActions dataAction)
		{
			RuntimeDataTablixObj runtimeDataTablixObj = null;
			Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = reportItem.ObjectType;
			if (objectType <= Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem)
			{
				if (objectType <= Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel)
				{
					if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle)
					{
						if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel)
						{
							runtimeDataTablixObj = new RuntimeGaugePanelObj(owner, (GaugePanel)reportItem, ref dataAction, odpContext, onePass);
						}
					}
					else
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection reportItems = ((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)reportItem).ReportItems;
						if (reportItems != null && reportItems.ComputedReportItems != null)
						{
							this.CreateDataRegions(owner, reportItems.ComputedReportItems, odpContext, onePass, ref dataAction);
						}
					}
				}
				else if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart)
				{
					if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem)
					{
						if (reportItem.IsDataRegion)
						{
							runtimeDataTablixObj = new RuntimeCriObj(owner, (Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem)reportItem, ref dataAction, odpContext, onePass);
						}
					}
				}
				else
				{
					runtimeDataTablixObj = new RuntimeChartObj(owner, (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)reportItem, ref dataAction, odpContext, onePass);
				}
			}
			else if (objectType <= Microsoft.ReportingServices.ReportProcessing.ObjectType.Map)
			{
				if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix)
				{
					if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Map)
					{
						List<MapDataRegion> mapDataRegions = ((Map)reportItem).MapDataRegions;
						if (mapDataRegions != null)
						{
							this.CreateMapDataRegions(owner, mapDataRegions, odpContext, onePass, ref dataAction);
						}
					}
				}
				else
				{
					runtimeDataTablixObj = new RuntimeTablixObj(owner, (Microsoft.ReportingServices.ReportIntermediateFormat.Tablix)reportItem, ref dataAction, odpContext, onePass);
				}
			}
			else if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.MapDataRegion)
			{
				if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.DataShape)
				{
					runtimeDataTablixObj = new RuntimeDataShapeObj(owner, (DataShape)reportItem, ref dataAction, odpContext, onePass);
				}
			}
			else
			{
				runtimeDataTablixObj = new RuntimeMapDataRegionObj(owner, (MapDataRegion)reportItem, ref dataAction, odpContext, onePass);
			}
			if (runtimeDataTablixObj != null)
			{
				this.AddDataRegion(runtimeDataTablixObj, (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)reportItem);
			}
		}

		// Token: 0x06007E49 RID: 32329 RVA: 0x00208FAC File Offset: 0x002071AC
		private void CreateMapDataRegions(IReference<IScope> owner, List<MapDataRegion> mapDataRegions, OnDemandProcessingContext odpContext, bool onePass, ref DataActions dataAction)
		{
			foreach (MapDataRegion mapDataRegion in mapDataRegions)
			{
				RuntimeDataTablixObj runtimeDataTablixObj = new RuntimeMapDataRegionObj(owner, mapDataRegion, ref dataAction, odpContext, onePass);
				this.AddDataRegion(runtimeDataTablixObj, mapDataRegion);
			}
		}

		// Token: 0x06007E4A RID: 32330 RVA: 0x0020900C File Offset: 0x0020720C
		private void AddDataRegion(RuntimeDataTablixObj dataRegion, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef)
		{
			RuntimeDataTablixObjReference runtimeDataTablixObjReference = (RuntimeDataTablixObjReference)dataRegion.SelfReference;
			runtimeDataTablixObjReference.UnPinValue();
			int indexInCollection = dataRegionDef.IndexInCollection;
			ListUtils.AdjustLength<RuntimeDataTablixObjReference>(this.m_dataRegionObjs, indexInCollection);
			this.m_dataRegionObjs[indexInCollection] = runtimeDataTablixObjReference;
		}

		// Token: 0x06007E4B RID: 32331 RVA: 0x0020904C File Offset: 0x0020724C
		internal void FirstPassNextDataRow(OnDemandProcessingContext odpContext)
		{
			AggregateRowInfo aggregateRowInfo = AggregateRowInfo.CreateAndSaveAggregateInfo(odpContext);
			for (int i = 0; i < this.m_dataRegionObjs.Count; i++)
			{
				RuntimeDataRegionObjReference runtimeDataRegionObjReference = this.m_dataRegionObjs[i];
				if (runtimeDataRegionObjReference != null)
				{
					using (runtimeDataRegionObjReference.PinValue())
					{
						runtimeDataRegionObjReference.Value().NextRow();
					}
					aggregateRowInfo.RestoreAggregateInfo(odpContext);
				}
			}
		}

		// Token: 0x06007E4C RID: 32332 RVA: 0x002090C4 File Offset: 0x002072C4
		internal void SortAndFilter(AggregateUpdateContext aggContext)
		{
			this.Traverse(ProcessingStages.SortAndFilter, aggContext);
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x002090D0 File Offset: 0x002072D0
		private void Traverse(ProcessingStages operation, AggregateUpdateContext context)
		{
			for (int i = 0; i < this.m_dataRegionObjs.Count; i++)
			{
				RuntimeDataRegionObjReference runtimeDataRegionObjReference = this.m_dataRegionObjs[i];
				if (runtimeDataRegionObjReference != null)
				{
					using (runtimeDataRegionObjReference.PinValue())
					{
						if (operation != ProcessingStages.SortAndFilter)
						{
							if (operation != ProcessingStages.UpdateAggregates)
							{
								Global.Tracer.Assert(false, "Unknown ProcessingStage in Traverse");
							}
							else
							{
								runtimeDataRegionObjReference.Value().UpdateAggregates(context);
							}
						}
						else
						{
							runtimeDataRegionObjReference.Value().SortAndFilter(context);
						}
					}
				}
			}
		}

		// Token: 0x06007E4E RID: 32334 RVA: 0x00209164 File Offset: 0x00207364
		internal void UpdateAggregates(AggregateUpdateContext aggContext)
		{
			this.Traverse(ProcessingStages.UpdateAggregates, aggContext);
		}

		// Token: 0x06007E4F RID: 32335 RVA: 0x00209170 File Offset: 0x00207370
		internal void CalculateRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCollection, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
			for (int i = 0; i < this.m_dataRegionObjs.Count; i++)
			{
				RuntimeDataRegionObjReference runtimeDataRegionObjReference = this.m_dataRegionObjs[i];
				if (runtimeDataRegionObjReference != null)
				{
					using (runtimeDataRegionObjReference.PinValue())
					{
						runtimeDataRegionObjReference.Value().CalculateRunningValues(groupCollection, lastGroup, aggContext);
					}
				}
			}
		}

		// Token: 0x06007E50 RID: 32336 RVA: 0x002091DC File Offset: 0x002073DC
		internal static void StoreRunningValues(AggregatesImpl globalRVCol, List<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo> runningValues, ref Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] runningValueValues)
		{
			if (runningValues != null && 0 < runningValues.Count)
			{
				if (runningValueValues == null)
				{
					runningValueValues = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[runningValues.Count];
				}
				for (int i = 0; i < runningValues.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo runningValueInfo = runningValues[i];
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj = globalRVCol.GetAggregateObj(runningValueInfo.Name);
					if (aggregateObj != null)
					{
						runningValueValues[i] = aggregateObj.AggregateResult();
					}
				}
				return;
			}
			runningValueValues = null;
		}

		// Token: 0x06007E51 RID: 32337 RVA: 0x00209240 File Offset: 0x00207440
		internal void CreateAllDataRegionInstances(ScopeInstance parentInstance, OnDemandProcessingContext odpContext, IReference<IScope> owner)
		{
			for (int i = 0; i < this.m_dataRegionObjs.Count; i++)
			{
				RuntimeRICollection.CreateDataRegionInstance(parentInstance, odpContext, this.m_dataRegionObjs[i]);
			}
			this.m_dataRegionObjs = null;
		}

		// Token: 0x06007E52 RID: 32338 RVA: 0x00209280 File Offset: 0x00207480
		internal void CreateInstances(ScopeInstance parentInstance, OnDemandProcessingContext odpContext, IReference<IScope> owner, List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> reportItems)
		{
			if (reportItems == null)
			{
				return;
			}
			for (int i = 0; i < reportItems.Count; i++)
			{
				this.CreateInstance(parentInstance, reportItems[i], odpContext, owner);
			}
		}

		// Token: 0x06007E53 RID: 32339 RVA: 0x002092B8 File Offset: 0x002074B8
		private static void CreateDataRegionInstance(ScopeInstance parentInstance, OnDemandProcessingContext odpContext, RuntimeDataRegionObjReference dataRegionObjRef)
		{
			if (dataRegionObjRef == null)
			{
				return;
			}
			using (dataRegionObjRef.PinValue())
			{
				RuntimeDataTablixObj runtimeDataTablixObj = (RuntimeDataTablixObj)dataRegionObjRef.Value();
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef = runtimeDataTablixObj.DataRegionDef;
				runtimeDataTablixObj.SetupEnvironment();
				DataRegionInstance dataRegionInstance = DataRegionInstance.CreateInstance(parentInstance, odpContext.OdpMetadata, dataRegionDef, odpContext.CurrentDataSetIndex).Value();
				runtimeDataTablixObj.CreateInstances(dataRegionInstance);
				dataRegionInstance.InstanceComplete();
				dataRegionDef.RuntimeDataRegionObj = null;
			}
		}

		// Token: 0x06007E54 RID: 32340 RVA: 0x00209338 File Offset: 0x00207538
		public static void MergeDataProcessingItems(List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> candidateItems, ref List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> results)
		{
			if (candidateItems == null)
			{
				return;
			}
			for (int i = 0; i < candidateItems.Count; i++)
			{
				RuntimeRICollection.MergeDataProcessingItem(candidateItems[i], ref results);
			}
		}

		// Token: 0x06007E55 RID: 32341 RVA: 0x00209368 File Offset: 0x00207568
		public static void MergeDataProcessingItem(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem item, ref List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> results)
		{
			if (item == null)
			{
				return;
			}
			if (item.IsDataRegion)
			{
				RuntimeRICollection.AddItem(item, ref results);
				return;
			}
			Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = item.ObjectType;
			if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle)
			{
				RuntimeRICollection.MergeDataProcessingItems(((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)item).ReportItems.ComputedReportItems, ref results);
				return;
			}
			if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Subreport && objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Map)
			{
				return;
			}
			RuntimeRICollection.AddItem(item, ref results);
		}

		// Token: 0x06007E56 RID: 32342 RVA: 0x002093BC File Offset: 0x002075BC
		private static void AddItem(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem item, ref List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> results)
		{
			if (results == null)
			{
				results = new List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem>();
			}
			results.Add(item);
		}

		// Token: 0x06007E57 RID: 32343 RVA: 0x002093D4 File Offset: 0x002075D4
		private void CreateInstance(ScopeInstance parentInstance, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem, OnDemandProcessingContext odpContext, IReference<IScope> owner)
		{
			if (reportItem == null)
			{
				return;
			}
			if (reportItem.IsDataRegion)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)reportItem;
				RuntimeDataRegionObjReference runtimeDataRegionObjReference = this.m_dataRegionObjs[dataRegion.IndexInCollection];
				RuntimeRICollection.CreateDataRegionInstance(parentInstance, odpContext, runtimeDataRegionObjReference);
				return;
			}
			Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = reportItem.ObjectType;
			if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle rectangle = (Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)reportItem;
				this.CreateInstances(parentInstance, odpContext, owner, rectangle.ReportItems.ComputedReportItems);
				return;
			}
			if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Subreport)
			{
				this.CreateSubReportInstance((Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)reportItem, parentInstance, odpContext, owner);
				return;
			}
			if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Map)
			{
				return;
			}
			List<MapDataRegion> mapDataRegions = ((Map)reportItem).MapDataRegions;
			for (int i = 0; i < mapDataRegions.Count; i++)
			{
				this.CreateInstance(parentInstance, mapDataRegions[i], odpContext, owner);
			}
		}

		// Token: 0x06007E58 RID: 32344 RVA: 0x00209488 File Offset: 0x00207688
		private void CreateSubReportInstance(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport, ScopeInstance parentInstance, OnDemandProcessingContext odpContext, IReference<IScope> owner)
		{
			if (subReport.ExceededMaxLevel)
			{
				return;
			}
			IReference<Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance> reference = Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance.CreateInstance(parentInstance, subReport, odpContext.OdpMetadata);
			subReport.CurrentSubReportInstance = reference;
			subReport.OdpContext.UserSortFilterContext.CurrentContainingScope = owner;
			odpContext.LastTablixProcessingReportScope = parentInstance.RIFReportScope;
			if (SubReportInitializer.InitializeSubReport(subReport))
			{
				IReference<Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance> reportInstance = reference.Value().ReportInstance;
				Merge.PreProcessTablixes(subReport.Report, subReport.OdpContext, !odpContext.ReprocessSnapshot);
				if (subReport.Report.HasSubReports)
				{
					SubReportInitializer.InitializeSubReports(subReport.Report, reportInstance.Value(), subReport.OdpContext, false, true);
				}
			}
			if (reference != null)
			{
				reference.Value().InstanceComplete();
			}
			odpContext.EnsureCultureIsSetOnCurrentThread();
		}

		// Token: 0x06007E59 RID: 32345 RVA: 0x0020953C File Offset: 0x0020773C
		public RuntimeDataTablixObjReference GetDataRegionObj(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion)
		{
			int indexInCollection = rifDataRegion.IndexInCollection;
			return this.m_dataRegionObjs[indexInCollection];
		}

		// Token: 0x06007E5A RID: 32346 RVA: 0x0020955C File Offset: 0x0020775C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeRICollection.m_declaration);
			PersistenceHelper persistenceHelper = writer.PersistenceHelper;
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.DataRegionObjs)
				{
					writer.Write<RuntimeDataTablixObjReference>(this.m_dataRegionObjs);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06007E5B RID: 32347 RVA: 0x002095B8 File Offset: 0x002077B8
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeRICollection.m_declaration);
			PersistenceHelper persistenceHelper = reader.PersistenceHelper;
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.DataRegionObjs)
				{
					this.m_dataRegionObjs = reader.ReadListOfRIFObjects<List<RuntimeDataTablixObjReference>>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06007E5C RID: 32348 RVA: 0x00209611 File Offset: 0x00207811
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007E5D RID: 32349 RVA: 0x00209613 File Offset: 0x00207813
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRICollection;
		}

		// Token: 0x06007E5E RID: 32350 RVA: 0x00209618 File Offset: 0x00207818
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeRICollection.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRICollection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.DataRegionObjs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixObjReference)
				});
			}
			return RuntimeRICollection.m_declaration;
		}

		// Token: 0x17002914 RID: 10516
		// (get) Token: 0x06007E5F RID: 32351 RVA: 0x00209657 File Offset: 0x00207857
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf<RuntimeDataTablixObjReference>(this.m_dataRegionObjs);
			}
		}

		// Token: 0x04003E22 RID: 15906
		private List<RuntimeDataTablixObjReference> m_dataRegionObjs;

		// Token: 0x04003E23 RID: 15907
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeRICollection.GetDeclaration();
	}
}
