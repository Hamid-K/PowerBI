using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008BC RID: 2236
	[PersistedWithinRequestOnly]
	public abstract class RuntimeDataRegionObj : IScope, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ISelfReferential
	{
		// Token: 0x06007A0E RID: 31246 RVA: 0x001F71BD File Offset: 0x001F53BD
		protected RuntimeDataRegionObj()
		{
		}

		// Token: 0x06007A0F RID: 31247 RVA: 0x001F71C5 File Offset: 0x001F53C5
		protected RuntimeDataRegionObj(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, int depth)
		{
			this.m_odpContext = odpContext;
			this.m_objectType = objectType;
			this.m_depth = depth;
			this.m_odpContext.TablixProcessingScalabilityCache.AllocateAndPin<RuntimeDataRegionObj>(this, this.m_depth);
		}

		// Token: 0x1700283C RID: 10300
		// (get) Token: 0x06007A10 RID: 31248 RVA: 0x001F71FA File Offset: 0x001F53FA
		public RuntimeDataRegionObjReference SelfReference
		{
			get
			{
				return this.m_selfReference;
			}
		}

		// Token: 0x1700283D RID: 10301
		// (get) Token: 0x06007A11 RID: 31249 RVA: 0x001F7202 File Offset: 0x001F5402
		internal OnDemandProcessingContext OdpContext
		{
			get
			{
				return this.m_odpContext;
			}
		}

		// Token: 0x1700283E RID: 10302
		// (get) Token: 0x06007A12 RID: 31250
		protected abstract IReference<IScope> OuterScope { get; }

		// Token: 0x1700283F RID: 10303
		// (get) Token: 0x06007A13 RID: 31251 RVA: 0x001F720A File Offset: 0x001F540A
		protected virtual string ScopeName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002840 RID: 10304
		// (get) Token: 0x06007A14 RID: 31252 RVA: 0x001F720D File Offset: 0x001F540D
		internal virtual bool TargetForNonDetailSort
		{
			get
			{
				return this.OuterScope != null && this.OuterScope.Value().TargetForNonDetailSort;
			}
		}

		// Token: 0x17002841 RID: 10305
		// (get) Token: 0x06007A15 RID: 31253 RVA: 0x001F7229 File Offset: 0x001F5429
		protected virtual int[] SortFilterExpressionScopeInfoIndices
		{
			get
			{
				Global.Tracer.Assert(false);
				return null;
			}
		}

		// Token: 0x06007A16 RID: 31254 RVA: 0x001F7237 File Offset: 0x001F5437
		internal virtual bool IsTargetForSort(int index, bool detailSort)
		{
			return this.OuterScope != null && this.OuterScope.Value().IsTargetForSort(index, detailSort);
		}

		// Token: 0x17002842 RID: 10306
		// (get) Token: 0x06007A17 RID: 31255 RVA: 0x001F7255 File Offset: 0x001F5455
		internal Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
		}

		// Token: 0x17002843 RID: 10307
		// (get) Token: 0x06007A18 RID: 31256 RVA: 0x001F725D File Offset: 0x001F545D
		public int Depth
		{
			get
			{
				return this.m_depth;
			}
		}

		// Token: 0x17002844 RID: 10308
		// (get) Token: 0x06007A19 RID: 31257 RVA: 0x001F7265 File Offset: 0x001F5465
		internal virtual IRIFReportScope RIFReportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007A1A RID: 31258
		internal abstract void NextRow();

		// Token: 0x06007A1B RID: 31259
		internal abstract bool SortAndFilter(AggregateUpdateContext aggContext);

		// Token: 0x06007A1C RID: 31260
		internal abstract void CalculateRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext);

		// Token: 0x06007A1D RID: 31261
		public abstract void SetupEnvironment();

		// Token: 0x06007A1E RID: 31262
		internal abstract void CalculatePreviousAggregates();

		// Token: 0x06007A1F RID: 31263
		public abstract void UpdateAggregates(AggregateUpdateContext context);

		// Token: 0x17002845 RID: 10309
		// (get) Token: 0x06007A20 RID: 31264 RVA: 0x001F7268 File Offset: 0x001F5468
		bool IScope.TargetForNonDetailSort
		{
			get
			{
				return this.TargetForNonDetailSort;
			}
		}

		// Token: 0x17002846 RID: 10310
		// (get) Token: 0x06007A21 RID: 31265 RVA: 0x001F7270 File Offset: 0x001F5470
		int[] IScope.SortFilterExpressionScopeInfoIndices
		{
			get
			{
				return this.SortFilterExpressionScopeInfoIndices;
			}
		}

		// Token: 0x17002847 RID: 10311
		// (get) Token: 0x06007A22 RID: 31266 RVA: 0x001F7278 File Offset: 0x001F5478
		IRIFReportScope IScope.RIFReportScope
		{
			get
			{
				return this.RIFReportScope;
			}
		}

		// Token: 0x06007A23 RID: 31267 RVA: 0x001F7280 File Offset: 0x001F5480
		bool IScope.IsTargetForSort(int index, bool detailSort)
		{
			return this.IsTargetForSort(index, detailSort);
		}

		// Token: 0x06007A24 RID: 31268 RVA: 0x001F728A File Offset: 0x001F548A
		void IScope.CalculatePreviousAggregates()
		{
			this.CalculatePreviousAggregates();
		}

		// Token: 0x06007A25 RID: 31269 RVA: 0x001F7292 File Offset: 0x001F5492
		bool IScope.InScope(string scope)
		{
			return this.InScope(scope);
		}

		// Token: 0x06007A26 RID: 31270 RVA: 0x001F729B File Offset: 0x001F549B
		IReference<IScope> IScope.GetOuterScope(bool includeSubReportContainingScope)
		{
			return this.OuterScope;
		}

		// Token: 0x06007A27 RID: 31271 RVA: 0x001F72A3 File Offset: 0x001F54A3
		string IScope.GetScopeName()
		{
			return this.ScopeName;
		}

		// Token: 0x06007A28 RID: 31272 RVA: 0x001F72AB File Offset: 0x001F54AB
		int IScope.RecursiveLevel(string scope)
		{
			return this.GetRecursiveLevel(scope);
		}

		// Token: 0x06007A29 RID: 31273 RVA: 0x001F72B4 File Offset: 0x001F54B4
		bool IScope.TargetScopeMatched(int index, bool detailSort)
		{
			return this.TargetScopeMatched(index, detailSort);
		}

		// Token: 0x06007A2A RID: 31274 RVA: 0x001F72BE File Offset: 0x001F54BE
		void IScope.GetScopeValues(IReference<IHierarchyObj> targetScopeObj, List<object>[] scopeValues, ref int index)
		{
			this.GetScopeValues(targetScopeObj, scopeValues, ref index);
		}

		// Token: 0x06007A2B RID: 31275 RVA: 0x001F72C9 File Offset: 0x001F54C9
		void IScope.GetGroupNameValuePairs(Dictionary<string, object> pairs)
		{
			this.GetGroupNameValuePairs(pairs);
		}

		// Token: 0x06007A2C RID: 31276 RVA: 0x001F72D2 File Offset: 0x001F54D2
		internal static bool UpdateAggregatesAtScope(AggregateUpdateContext aggContext, IDataRowHolder scope, DataScopeInfo scopeInfo, AggregateUpdateFlags updateFlags, bool needsSetupEnvironment)
		{
			return aggContext.UpdateAggregates(scopeInfo, scope, updateFlags, needsSetupEnvironment);
		}

		// Token: 0x06007A2D RID: 31277 RVA: 0x001F72DF File Offset: 0x001F54DF
		internal static void AggregatesOfAggregatesEnd(IScope scopeObj, AggregateUpdateContext aggContext, AggregateUpdateQueue workQueue, DataScopeInfo dataScopeInfo, BucketedDataAggregateObjs aggregatesOfAggregates, bool updateAggsIfNeeded)
		{
			if (dataScopeInfo == null)
			{
				return;
			}
			if (updateAggsIfNeeded)
			{
				while (aggContext.AdvanceQueue(workQueue))
				{
					scopeObj.UpdateAggregates(aggContext);
				}
			}
			aggContext.RestoreOriginalState(workQueue);
			if (aggContext.Mode == AggregateMode.Aggregates && dataScopeInfo.NeedsSeparateAofAPass && updateAggsIfNeeded)
			{
				scopeObj.UpdateAggregates(aggContext);
			}
		}

		// Token: 0x06007A2E RID: 31278 RVA: 0x001F7320 File Offset: 0x001F5520
		internal static AggregateUpdateQueue AggregateOfAggregatesStart(AggregateUpdateContext aggContext, IDataRowHolder scope, DataScopeInfo dataScopeInfo, BucketedDataAggregateObjs aggregatesOfAggregates, AggregateUpdateFlags updateFlags, bool needsSetupEnvironment)
		{
			if (dataScopeInfo == null)
			{
				return null;
			}
			AggregateUpdateQueue aggregateUpdateQueue = null;
			if (aggContext.Mode == AggregateMode.Aggregates)
			{
				if (dataScopeInfo.NeedsSeparateAofAPass)
				{
					aggregateUpdateQueue = aggContext.ReplaceAggregatesToUpdate(aggregatesOfAggregates);
				}
				else
				{
					aggregateUpdateQueue = aggContext.RegisterAggregatesToUpdate(aggregatesOfAggregates);
					if (updateFlags != AggregateUpdateFlags.None)
					{
						RuntimeDataRegionObj.UpdateAggregatesAtScope(aggContext, scope, dataScopeInfo, updateFlags, needsSetupEnvironment);
					}
				}
			}
			else if (aggContext.Mode == AggregateMode.PostSortAggregates)
			{
				aggregateUpdateQueue = aggContext.RegisterAggregatesToUpdate(aggregatesOfAggregates);
				aggregateUpdateQueue = aggContext.RegisterRunningValuesToUpdate(aggregateUpdateQueue, dataScopeInfo.RunningValuesOfAggregates);
				if (updateFlags != AggregateUpdateFlags.None)
				{
					RuntimeDataRegionObj.UpdateAggregatesAtScope(aggContext, scope, dataScopeInfo, updateFlags, needsSetupEnvironment);
				}
			}
			else
			{
				Global.Tracer.Assert(false, "Unknown AggregateMode for AggregateOfAggregatesStart");
			}
			return aggregateUpdateQueue;
		}

		// Token: 0x06007A2F RID: 31279 RVA: 0x001F73AC File Offset: 0x001F55AC
		internal static void AddAggregate(ref List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> aggregates, Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregate)
		{
			if (aggregates == null)
			{
				aggregates = new List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>();
			}
			aggregates.Add(aggregate);
		}

		// Token: 0x06007A30 RID: 31280 RVA: 0x001F73C4 File Offset: 0x001F55C4
		internal static void CreateAggregates(OnDemandProcessingContext odpContext, List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo> aggDefs, ref List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> nonCustomAggregates, ref List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> customAggregates)
		{
			if (aggDefs != null && 0 < aggDefs.Count)
			{
				for (int i = 0; i < aggDefs.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj(aggDefs[i], odpContext);
					if (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Aggregate == aggDefs[i].AggregateType)
					{
						RuntimeDataRegionObj.AddAggregate(ref customAggregates, dataAggregateObj);
					}
					else
					{
						RuntimeDataRegionObj.AddAggregate(ref nonCustomAggregates, dataAggregateObj);
					}
				}
			}
		}

		// Token: 0x06007A31 RID: 31281 RVA: 0x001F741C File Offset: 0x001F561C
		internal static void CreateAggregates(OnDemandProcessingContext odpContext, List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo> aggDefs, List<int> aggregateIndexes, ref List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> nonCustomAggregates, ref List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> customAggregates)
		{
			if (aggregateIndexes != null && 0 < aggregateIndexes.Count)
			{
				for (int i = 0; i < aggregateIndexes.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo dataAggregateInfo = aggDefs[aggregateIndexes[i]];
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj(dataAggregateInfo, odpContext);
					if (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Aggregate == dataAggregateInfo.AggregateType)
					{
						RuntimeDataRegionObj.AddAggregate(ref customAggregates, dataAggregateObj);
					}
					else
					{
						RuntimeDataRegionObj.AddAggregate(ref nonCustomAggregates, dataAggregateObj);
					}
				}
			}
		}

		// Token: 0x06007A32 RID: 31282 RVA: 0x001F7478 File Offset: 0x001F5678
		internal static void CreateAggregates<AggregateType>(OnDemandProcessingContext odpContext, List<AggregateType> aggDefs, ref List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> aggregates) where AggregateType : Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo
		{
			if (aggDefs != null && 0 < aggDefs.Count)
			{
				for (int i = 0; i < aggDefs.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj(aggDefs[i], odpContext);
					RuntimeDataRegionObj.AddAggregate(ref aggregates, dataAggregateObj);
				}
			}
		}

		// Token: 0x06007A33 RID: 31283 RVA: 0x001F74BC File Offset: 0x001F56BC
		internal static void CreateAggregates<AggregateType>(OnDemandProcessingContext odpContext, BucketedAggregatesCollection<AggregateType> aggDefs, ref BucketedDataAggregateObjs aggregates) where AggregateType : Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo
		{
			if (aggDefs != null && !aggDefs.IsEmpty)
			{
				if (aggregates == null)
				{
					aggregates = new BucketedDataAggregateObjs();
				}
				foreach (AggregateBucket<AggregateType> aggregateBucket in aggDefs.Buckets)
				{
					foreach (AggregateType aggregateType in aggregateBucket.Aggregates)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj(aggregateType, odpContext);
						aggregates.GetOrCreateBucket(aggregateBucket.Level).Aggregates.Add(dataAggregateObj);
					}
				}
			}
		}

		// Token: 0x06007A34 RID: 31284 RVA: 0x001F7584 File Offset: 0x001F5784
		internal static void CreateAggregates<AggregateType>(OnDemandProcessingContext odpContext, List<AggregateType> aggDefs, List<int> aggregateIndexes, ref List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> aggregates) where AggregateType : Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo
		{
			if (aggregateIndexes != null && 0 < aggregateIndexes.Count)
			{
				for (int i = 0; i < aggregateIndexes.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj(aggDefs[aggregateIndexes[i]], odpContext);
					RuntimeDataRegionObj.AddAggregate(ref aggregates, dataAggregateObj);
				}
			}
		}

		// Token: 0x06007A35 RID: 31285 RVA: 0x001F75D0 File Offset: 0x001F57D0
		internal static void UpdateAggregates(OnDemandProcessingContext odpContext, List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> aggregates, bool updateAndSetup)
		{
			if (aggregates != null)
			{
				for (int i = 0; i < aggregates.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = aggregates[i];
					dataAggregateObj.Update();
					if (updateAndSetup)
					{
						odpContext.ReportObjectModel.AggregatesImpl.Set(dataAggregateObj.Name, dataAggregateObj.AggregateDef, dataAggregateObj.DuplicateNames, dataAggregateObj.AggregateResult());
					}
				}
			}
		}

		// Token: 0x06007A36 RID: 31286 RVA: 0x001F762C File Offset: 0x001F582C
		protected void SetupAggregates(List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> aggregates)
		{
			if (aggregates != null)
			{
				for (int i = 0; i < aggregates.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = aggregates[i];
					this.m_odpContext.ReportObjectModel.AggregatesImpl.Set(dataAggregateObj.Name, dataAggregateObj.AggregateDef, dataAggregateObj.DuplicateNames, dataAggregateObj.AggregateResult());
				}
			}
		}

		// Token: 0x06007A37 RID: 31287 RVA: 0x001F7682 File Offset: 0x001F5882
		protected void SetupAggregates(BucketedDataAggregateObjs aggregates)
		{
			RuntimeDataRegionObj.SetupAggregates(this.m_odpContext, aggregates);
		}

		// Token: 0x06007A38 RID: 31288 RVA: 0x001F7690 File Offset: 0x001F5890
		internal static void SetupAggregates(OnDemandProcessingContext odpContext, BucketedDataAggregateObjs aggregates)
		{
			if (aggregates != null)
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj in aggregates)
				{
					odpContext.ReportObjectModel.AggregatesImpl.Set(dataAggregateObj.Name, dataAggregateObj.AggregateDef, dataAggregateObj.DuplicateNames, dataAggregateObj.AggregateResult());
				}
			}
		}

		// Token: 0x06007A39 RID: 31289 RVA: 0x001F76FC File Offset: 0x001F58FC
		protected void SetupNewDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
		{
			this.m_odpContext.EnsureRuntimeEnvironmentForDataSet(dataSet, false);
		}

		// Token: 0x06007A3A RID: 31290 RVA: 0x001F770B File Offset: 0x001F590B
		protected void SetupEnvironment(List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> nonCustomAggregates, List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> customAggregates, DataFieldRow dataRow)
		{
			this.SetupAggregates(nonCustomAggregates);
			this.SetupAggregates(customAggregates);
			this.SetupFields(dataRow);
			this.m_odpContext.ReportRuntime.CurrentScope = this;
		}

		// Token: 0x06007A3B RID: 31291 RVA: 0x001F7733 File Offset: 0x001F5933
		protected void SetupFields(DataFieldRow dataRow)
		{
			if (dataRow == null)
			{
				this.m_odpContext.ReportObjectModel.CreateNoRows();
				return;
			}
			dataRow.SetFields(this.m_odpContext.ReportObjectModel.FieldsImpl);
		}

		// Token: 0x06007A3C RID: 31292 RVA: 0x001F7760 File Offset: 0x001F5960
		protected void SetupRunningValues(List<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo> rvDefs, Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] rvValues)
		{
			int num = 0;
			RuntimeDataRegionObj.SetupRunningValues(this.m_odpContext, ref num, rvDefs, rvValues);
		}

		// Token: 0x06007A3D RID: 31293 RVA: 0x001F7780 File Offset: 0x001F5980
		private static void SetupRunningValues(OnDemandProcessingContext odpContext, ref int startIndex, List<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo> rvDefs, Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] rvValues)
		{
			if (rvDefs != null && rvValues != null)
			{
				AggregatesImpl aggregatesImpl = odpContext.ReportObjectModel.AggregatesImpl;
				for (int i = 0; i < rvDefs.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo runningValueInfo = rvDefs[i];
					aggregatesImpl.Set(runningValueInfo.Name, runningValueInfo, runningValueInfo.DuplicateNames, rvValues[startIndex + i]);
				}
				startIndex += rvDefs.Count;
			}
		}

		// Token: 0x06007A3E RID: 31294 RVA: 0x001F77DC File Offset: 0x001F59DC
		protected static IOnDemandMemberInstanceReference GetFirstMemberInstance(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode rifMember, IReference<RuntimeMemberObj>[] memberCol)
		{
			IOnDemandMemberInstanceReference onDemandMemberInstanceReference = null;
			RuntimeDataTablixGroupRootObjReference groupRoot = RuntimeDataRegionObj.GetGroupRoot(rifMember, memberCol);
			using (groupRoot.PinValue())
			{
				RuntimeGroupLeafObjReference firstChild = groupRoot.Value().FirstChild;
				if (firstChild != null)
				{
					onDemandMemberInstanceReference = (IOnDemandMemberInstanceReference)firstChild;
				}
			}
			return onDemandMemberInstanceReference;
		}

		// Token: 0x06007A3F RID: 31295 RVA: 0x001F7834 File Offset: 0x001F5A34
		protected static RuntimeDataTablixGroupRootObjReference GetGroupRoot(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode rifMember, IReference<RuntimeMemberObj>[] memberCol)
		{
			Global.Tracer.Assert(!rifMember.IsStatic, "Cannot GetGroupRoot of a static member");
			return memberCol[rifMember.IndexInCollection].Value().GroupRoot;
		}

		// Token: 0x06007A40 RID: 31296 RVA: 0x001F7860 File Offset: 0x001F5A60
		internal static long AssignScopeInstanceNumber(DataScopeInfo dataScopeInfo)
		{
			if (dataScopeInfo == null)
			{
				return 0L;
			}
			return dataScopeInfo.AssignScopeInstanceNumber();
		}

		// Token: 0x06007A41 RID: 31297
		public abstract void ReadRow(DataActions dataAction, ITraversalContext context);

		// Token: 0x06007A42 RID: 31298
		internal abstract bool InScope(string scope);

		// Token: 0x06007A43 RID: 31299 RVA: 0x001F7870 File Offset: 0x001F5A70
		protected Hashtable GetScopeNames(RuntimeDataRegionObjReference currentScope, string targetScope, out bool inScope)
		{
			inScope = false;
			Hashtable hashtable = new Hashtable();
			IScope scope;
			for (IReference<IScope> reference = currentScope; reference != null; reference = scope.GetOuterScope(false))
			{
				scope = reference.Value();
				string scopeName = scope.GetScopeName();
				if (scopeName != null)
				{
					if (!inScope && scopeName.Equals(targetScope))
					{
						inScope = true;
					}
					Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = null;
					if (scope is RuntimeGroupLeafObj)
					{
						grouping = ((RuntimeGroupLeafObj)scope).GroupingDef;
					}
					hashtable.Add(scopeName, grouping);
				}
				else if (scope is RuntimeTablixCell && !inScope)
				{
					inScope = scope.InScope(targetScope);
				}
			}
			return hashtable;
		}

		// Token: 0x06007A44 RID: 31300 RVA: 0x001F78F4 File Offset: 0x001F5AF4
		protected Hashtable GetScopeNames(RuntimeDataRegionObjReference currentScope, string targetScope, out int level)
		{
			level = -1;
			Hashtable hashtable = new Hashtable();
			IScope scope;
			for (IReference<IScope> reference = currentScope; reference != null; reference = scope.GetOuterScope(false))
			{
				scope = reference.Value();
				string scopeName = scope.GetScopeName();
				if (scopeName != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = null;
					if (scope is RuntimeGroupLeafObj)
					{
						grouping = ((RuntimeGroupLeafObj)scope).GroupingDef;
						if (-1 == level && scopeName.Equals(targetScope))
						{
							level = grouping.RecursiveLevel;
						}
					}
					hashtable.Add(scopeName, grouping);
				}
				else if (scope is RuntimeTablixCell && -1 == level)
				{
					level = scope.RecursiveLevel(targetScope);
				}
			}
			return hashtable;
		}

		// Token: 0x06007A45 RID: 31301 RVA: 0x001F7980 File Offset: 0x001F5B80
		protected Hashtable GetScopeNames(RuntimeDataRegionObjReference currentScope, Dictionary<string, object> nameValuePairs)
		{
			Hashtable hashtable = new Hashtable();
			IScope scope;
			for (IReference<IScope> reference = currentScope; reference != null; reference = scope.GetOuterScope(false))
			{
				scope = reference.Value();
				string scopeName = scope.GetScopeName();
				if (scopeName != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = null;
					if (scope is RuntimeGroupLeafObj)
					{
						grouping = ((RuntimeGroupLeafObj)scope).GroupingDef;
						RuntimeDataRegionObj.AddGroupNameValuePair(this.m_odpContext, grouping, nameValuePairs);
					}
					hashtable.Add(scopeName, grouping);
				}
				else if (scope is RuntimeTablixCell)
				{
					scope.GetGroupNameValuePairs(nameValuePairs);
				}
			}
			return hashtable;
		}

		// Token: 0x06007A46 RID: 31302 RVA: 0x001F79F8 File Offset: 0x001F5BF8
		internal static void AddGroupNameValuePair(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping, Dictionary<string, object> nameValuePairs)
		{
			if (grouping != null)
			{
				Global.Tracer.Assert(grouping.GroupExpressions != null && 0 < grouping.GroupExpressions.Count);
				Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = grouping.GroupExpressions[0];
				if (expressionInfo.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Field)
				{
					try
					{
						FieldImpl fieldImpl = odpContext.ReportObjectModel.FieldsImpl[expressionInfo.IntValue];
						if (fieldImpl.FieldDef != null)
						{
							object value = fieldImpl.Value;
							if (!nameValuePairs.ContainsKey(fieldImpl.FieldDef.DataField))
							{
								nameValuePairs.Add(fieldImpl.FieldDef.DataField, (value is DBNull) ? null : value);
							}
						}
					}
					catch (Exception ex)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex))
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x06007A47 RID: 31303 RVA: 0x001F7AB8 File Offset: 0x001F5CB8
		protected bool DataRegionInScope(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, string scope)
		{
			if (dataRegionDef.ScopeNames == null)
			{
				bool flag;
				dataRegionDef.ScopeNames = this.GetScopeNames(this.SelfReference, scope, out flag);
				return flag;
			}
			return dataRegionDef.ScopeNames.Contains(scope);
		}

		// Token: 0x06007A48 RID: 31304 RVA: 0x001F7AF0 File Offset: 0x001F5CF0
		protected virtual int GetRecursiveLevel(string scope)
		{
			return -1;
		}

		// Token: 0x06007A49 RID: 31305 RVA: 0x001F7AF4 File Offset: 0x001F5CF4
		protected int DataRegionRecursiveLevel(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, string scope)
		{
			if (scope == null)
			{
				return -1;
			}
			if (dataRegionDef.ScopeNames == null)
			{
				int num;
				dataRegionDef.ScopeNames = this.GetScopeNames(this.SelfReference, scope, out num);
				return num;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = dataRegionDef.ScopeNames[scope] as Microsoft.ReportingServices.ReportIntermediateFormat.Grouping;
			if (grouping != null)
			{
				return grouping.RecursiveLevel;
			}
			return -1;
		}

		// Token: 0x06007A4A RID: 31306 RVA: 0x001F7B44 File Offset: 0x001F5D44
		protected void DataRegionGetGroupNameValuePairs(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, Dictionary<string, object> nameValuePairs)
		{
			if (dataRegionDef.ScopeNames == null)
			{
				dataRegionDef.ScopeNames = this.GetScopeNames(this.SelfReference, nameValuePairs);
				return;
			}
			foreach (object obj in dataRegionDef.ScopeNames.Values)
			{
				RuntimeDataRegionObj.AddGroupNameValuePair(this.m_odpContext, obj as Microsoft.ReportingServices.ReportIntermediateFormat.Grouping, nameValuePairs);
			}
		}

		// Token: 0x06007A4B RID: 31307 RVA: 0x001F7B9F File Offset: 0x001F5D9F
		protected void ScopeNextNonAggregateRow(List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> aggregates, ScalableList<DataFieldRow> dataRows)
		{
			RuntimeDataRegionObj.UpdateAggregates(this.m_odpContext, aggregates, true);
			this.CommonNextRow(dataRows);
		}

		// Token: 0x06007A4C RID: 31308 RVA: 0x001F7BB5 File Offset: 0x001F5DB5
		internal static void CommonFirstRow(OnDemandProcessingContext odpContext, ref bool firstRowIsAggregate, ref DataFieldRow firstRow)
		{
			if (firstRowIsAggregate || firstRow == null)
			{
				firstRow = new DataFieldRow(odpContext.ReportObjectModel.FieldsImpl, true);
				firstRowIsAggregate = odpContext.ReportObjectModel.FieldsImpl.IsAggregateRow;
			}
		}

		// Token: 0x06007A4D RID: 31309 RVA: 0x001F7BE4 File Offset: 0x001F5DE4
		protected void CommonNextRow(ScalableList<DataFieldRow> dataRows)
		{
			if (dataRows != null)
			{
				RuntimeDataTablixObj.SaveData(dataRows, this.m_odpContext);
			}
			this.SendToInner();
		}

		// Token: 0x06007A4E RID: 31310 RVA: 0x001F7BFB File Offset: 0x001F5DFB
		protected virtual void SendToInner()
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007A4F RID: 31311 RVA: 0x001F7C08 File Offset: 0x001F5E08
		protected void ScopeNextAggregateRow(RuntimeUserSortTargetInfo sortTargetInfo)
		{
			if (sortTargetInfo != null)
			{
				if (sortTargetInfo.AggregateRows == null)
				{
					sortTargetInfo.AggregateRows = new List<AggregateRow>();
				}
				AggregateRow aggregateRow = new AggregateRow(this.m_odpContext.ReportObjectModel.FieldsImpl, true);
				sortTargetInfo.AggregateRows.Add(aggregateRow);
				if (!sortTargetInfo.TargetForNonDetailSort)
				{
					return;
				}
			}
			this.SendToInner();
		}

		// Token: 0x06007A50 RID: 31312 RVA: 0x001F7C60 File Offset: 0x001F5E60
		protected void ScopeFinishSorting(ref DataFieldRow firstRow, RuntimeUserSortTargetInfo sortTargetInfo)
		{
			Global.Tracer.Assert(sortTargetInfo != null, "(null != sortTargetInfo)");
			firstRow = null;
			sortTargetInfo.SortTree.Traverse(ProcessingStages.UserSortFilter, true, null);
			sortTargetInfo.SortTree.Dispose();
			sortTargetInfo.SortTree = null;
			if (sortTargetInfo.AggregateRows != null)
			{
				for (int i = 0; i < sortTargetInfo.AggregateRows.Count; i++)
				{
					sortTargetInfo.AggregateRows[i].SetFields(this.m_odpContext.ReportObjectModel.FieldsImpl);
					this.SendToInner();
				}
				sortTargetInfo.AggregateRows = null;
			}
		}

		// Token: 0x06007A51 RID: 31313 RVA: 0x001F7CEF File Offset: 0x001F5EEF
		internal virtual bool TargetScopeMatched(int index, bool detailSort)
		{
			Global.Tracer.Assert(false);
			return false;
		}

		// Token: 0x06007A52 RID: 31314 RVA: 0x001F7CFD File Offset: 0x001F5EFD
		internal virtual void GetScopeValues(IReference<IHierarchyObj> targetScopeObj, List<object>[] scopeValues, ref int index)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007A53 RID: 31315 RVA: 0x001F7D0A File Offset: 0x001F5F0A
		protected void ReleaseDataRows(DataActions finishedDataAction, ref DataActions dataAction, ref ScalableList<DataFieldRow> dataRows)
		{
			dataAction &= ~finishedDataAction;
			if (dataAction == DataActions.None)
			{
				dataRows.Clear();
				dataRows = null;
			}
		}

		// Token: 0x06007A54 RID: 31316 RVA: 0x001F7D24 File Offset: 0x001F5F24
		protected void DetailHandleSortFilterEvent(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, IReference<IScope> outerScope, bool isColumnAxis, int rowIndex)
		{
			using (outerScope.PinValue())
			{
				IScope scope = outerScope.Value();
				List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo = this.m_odpContext.RuntimeSortFilterInfo;
				if (runtimeSortFilterInfo != null && dataRegionDef.SortFilterSourceDetailScopeInfo != null && !scope.TargetForNonDetailSort)
				{
					int count = runtimeSortFilterInfo.Count;
					for (int i = 0; i < count; i++)
					{
						IReference<RuntimeSortFilterEventInfo> reference = runtimeSortFilterInfo[i];
						using (reference.PinValue())
						{
							RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = reference.Value();
							if (runtimeSortFilterEventInfo.EventSource.ContainingScopes != null && 0 < runtimeSortFilterEventInfo.EventSource.ContainingScopes.Count && -1 != dataRegionDef.SortFilterSourceDetailScopeInfo[i] && scope.TargetScopeMatched(i, false) && this.m_odpContext.ReportObjectModel.FieldsImpl.GetRowIndex() == dataRegionDef.SortFilterSourceDetailScopeInfo[i])
							{
								if (runtimeSortFilterEventInfo.EventSource.ContainingScopes.LastEntry == null)
								{
									Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem = runtimeSortFilterEventInfo.EventSource.Parent;
									if (runtimeSortFilterEventInfo.EventSource.IsSubReportTopLevelScope)
									{
										while (reportItem != null && !(reportItem is Microsoft.ReportingServices.ReportIntermediateFormat.SubReport))
										{
											reportItem = reportItem.Parent;
										}
										Global.Tracer.Assert(reportItem is Microsoft.ReportingServices.ReportIntermediateFormat.SubReport, "(parent is SubReport)");
										reportItem = reportItem.Parent;
									}
									if (reportItem == dataRegionDef)
									{
										runtimeSortFilterEventInfo.SetEventSourceScope(isColumnAxis, this.SelfReference, rowIndex);
									}
								}
								runtimeSortFilterEventInfo.AddDetailScopeInfo(isColumnAxis, this.SelfReference, rowIndex);
							}
						}
					}
				}
			}
		}

		// Token: 0x06007A55 RID: 31317 RVA: 0x001F7EE0 File Offset: 0x001F60E0
		protected void DetailGetScopeValues(IReference<IScope> outerScope, IReference<IHierarchyObj> targetScopeObj, List<object>[] scopeValues, ref int index)
		{
			Global.Tracer.Assert(targetScopeObj == null, "(null == targetScopeObj)");
			outerScope.Value().GetScopeValues(targetScopeObj, scopeValues, ref index);
			Global.Tracer.Assert(index < scopeValues.Length, "(index < scopeValues.Length)");
			List<object> list = new List<object>(1);
			list.Add(this.m_odpContext.ReportObjectModel.FieldsImpl.GetRowIndex());
			int num = index;
			index = num + 1;
			scopeValues[num] = list;
		}

		// Token: 0x06007A56 RID: 31318 RVA: 0x001F7F5C File Offset: 0x001F615C
		protected bool DetailTargetScopeMatched(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, IReference<IScope> outerScope, bool isColumnAxis, int index)
		{
			if (this.m_odpContext.RuntimeSortFilterInfo != null)
			{
				IReference<RuntimeSortFilterEventInfo> reference = this.m_odpContext.RuntimeSortFilterInfo[index];
				using (reference.PinValue())
				{
					RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = reference.Value();
					if (runtimeSortFilterEventInfo != null)
					{
						List<IReference<RuntimeDataRegionObj>> list;
						List<int> list2;
						int num;
						if (isColumnAxis)
						{
							list = runtimeSortFilterEventInfo.DetailColScopes;
							list2 = runtimeSortFilterEventInfo.DetailColScopeIndices;
							num = dataRegionDef.CurrentColDetailIndex;
						}
						else
						{
							list = runtimeSortFilterEventInfo.DetailRowScopes;
							list2 = runtimeSortFilterEventInfo.DetailRowScopeIndices;
							num = dataRegionDef.CurrentRowDetailIndex;
						}
						if (list != null)
						{
							for (int i = 0; i < list.Count; i++)
							{
								if (this.SelfReference.Equals(list[i]) && num == list2[i])
								{
									return true;
								}
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06007A57 RID: 31319 RVA: 0x001F8038 File Offset: 0x001F6238
		protected virtual void GetGroupNameValuePairs(Dictionary<string, object> pairs)
		{
		}

		// Token: 0x06007A58 RID: 31320 RVA: 0x001F803C File Offset: 0x001F623C
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeDataRegionObj.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.OdpContext)
				{
					if (memberName != MemberName.ObjectType)
					{
						if (memberName != MemberName.Depth)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_depth);
						}
					}
					else
					{
						writer.WriteEnum((int)this.m_objectType);
					}
				}
				else
				{
					int num = scalabilityCache.StoreStaticReference(this.m_odpContext);
					writer.Write(num);
				}
			}
		}

		// Token: 0x06007A59 RID: 31321 RVA: 0x001F80D4 File Offset: 0x001F62D4
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeDataRegionObj.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.OdpContext)
				{
					if (memberName != MemberName.ObjectType)
					{
						if (memberName != MemberName.Depth)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_depth = reader.ReadInt32();
						}
					}
					else
					{
						this.m_objectType = (Microsoft.ReportingServices.ReportProcessing.ObjectType)reader.ReadEnum();
					}
				}
				else
				{
					int num = reader.ReadInt32();
					this.m_odpContext = (OnDemandProcessingContext)scalabilityCache.FetchStaticReference(num);
				}
			}
		}

		// Token: 0x06007A5A RID: 31322 RVA: 0x001F816E File Offset: 0x001F636E
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007A5B RID: 31323 RVA: 0x001F8170 File Offset: 0x001F6370
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObj;
		}

		// Token: 0x06007A5C RID: 31324 RVA: 0x001F8178 File Offset: 0x001F6378
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDataRegionObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.OdpContext, Token.Int32),
					new MemberInfo(MemberName.ObjectType, Token.Enum),
					new MemberInfo(MemberName.Depth, Token.Int32)
				});
			}
			return RuntimeDataRegionObj.m_declaration;
		}

		// Token: 0x17002848 RID: 10312
		// (get) Token: 0x06007A5D RID: 31325 RVA: 0x001F81DC File Offset: 0x001F63DC
		public virtual int Size
		{
			get
			{
				return 8 + ItemSizes.SizeOf(this.m_selfReference);
			}
		}

		// Token: 0x06007A5E RID: 31326 RVA: 0x001F81EB File Offset: 0x001F63EB
		public virtual void SetReference(IReference selfRef)
		{
			this.m_selfReference = (RuntimeDataRegionObjReference)selfRef;
		}

		// Token: 0x04003D26 RID: 15654
		[StaticReference]
		protected OnDemandProcessingContext m_odpContext;

		// Token: 0x04003D27 RID: 15655
		protected Microsoft.ReportingServices.ReportProcessing.ObjectType m_objectType;

		// Token: 0x04003D28 RID: 15656
		[NonSerialized]
		protected RuntimeDataRegionObjReference m_selfReference;

		// Token: 0x04003D29 RID: 15657
		protected int m_depth;

		// Token: 0x04003D2A RID: 15658
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDataRegionObj.GetDeclaration();
	}
}
