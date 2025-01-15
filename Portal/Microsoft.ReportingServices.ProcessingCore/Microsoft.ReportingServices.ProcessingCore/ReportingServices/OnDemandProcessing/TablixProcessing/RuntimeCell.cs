using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008B7 RID: 2231
	[PersistedWithinRequestOnly]
	internal abstract class RuntimeCell : IScope, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ISelfReferential, IDataRowHolder, IOnDemandScopeInstance
	{
		// Token: 0x060079AA RID: 31146 RVA: 0x001F4F7C File Offset: 0x001F317C
		protected RuntimeCell()
		{
		}

		// Token: 0x060079AB RID: 31147 RVA: 0x001F4F8C File Offset: 0x001F318C
		internal RuntimeCell(RuntimeDataTablixGroupLeafObjReference owner, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode outerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode innerGroupingMember, bool innermost)
		{
			this.m_owner = owner;
			this.m_outerGroupDynamicIndex = outerGroupingMember.HierarchyDynamicIndex;
			this.m_innermost = innermost;
			this.m_dataAction = DataActions.None;
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef = owner.Value().DataRegionDef;
			OnDemandProcessingContext odpContext = owner.Value().OdpContext;
			RuntimeCell.GetCellIndexes(outerGroupingMember, innerGroupingMember, dataRegionDef, out this.m_rowIndexes, out this.m_colIndexes);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num = this.m_rowIndexes.Count * this.m_colIndexes.Count;
			foreach (int num2 in this.m_rowIndexes)
			{
				foreach (int num3 in this.m_colIndexes)
				{
					Cell cell = dataRegionDef.Rows[num2].Cells[num3];
					if (cell != null && this.m_canonicalCellScopeDef == null && cell.DataScopeInfo != null)
					{
						this.m_canonicalCellScopeDef = cell;
						if (this.m_canonicalCellScopeDef.CanonicalDataScopeInfo == null)
						{
							if (num == 1)
							{
								this.m_canonicalCellScopeDef.CanonicalDataScopeInfo = cell.DataScopeInfo;
							}
							else
							{
								flag3 = true;
								this.m_canonicalCellScopeDef.CanonicalDataScopeInfo = new DataScopeInfo(cell.DataScopeInfo.ScopeID);
							}
						}
					}
					if (cell != null && !cell.SimpleGroupTreeCell)
					{
						if (cell.AggregateIndexes != null)
						{
							RuntimeDataRegionObj.CreateAggregates(odpContext, dataRegionDef.CellAggregates, cell.AggregateIndexes, ref this.m_cellNonCustomAggObjs, ref this.m_cellCustomAggObjs);
						}
						if (cell.DataScopeInfo != null)
						{
							DataScopeInfo dataScopeInfo = cell.DataScopeInfo;
							if (flag3)
							{
								cell.CanonicalDataScopeInfo = this.m_canonicalCellScopeDef.CanonicalDataScopeInfo;
								this.m_canonicalCellScopeDef.CanonicalDataScopeInfo.MergeFrom(dataScopeInfo);
							}
							if (dataScopeInfo.AggregatesOfAggregates != null)
							{
								RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(odpContext, dataScopeInfo.AggregatesOfAggregates, ref this.m_cellAggregatesOfAggregates);
							}
							if (dataScopeInfo.PostSortAggregatesOfAggregates != null)
							{
								RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(odpContext, dataScopeInfo.PostSortAggregatesOfAggregates, ref this.m_cellPostSortAggregatesOfAggregates);
							}
						}
						if (cell.PostSortAggregateIndexes != null)
						{
							flag = true;
						}
						if (!flag2)
						{
							flag2 = ((IRIFReportScope)cell).NeedToCacheDataRows;
						}
						this.ConstructCellContents(cell, ref this.m_dataAction);
					}
				}
			}
			if (flag)
			{
				this.m_cellAggValueList = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[dataRegionDef.CellPostSortAggregates.Count];
				this.m_dataAction |= DataActions.PostSortAggregates;
			}
			if (!FlagUtils.HasFlag(this.m_dataAction, DataActions.PostSortAggregates) && dataRegionDef.CellRunningValues != null && flag2)
			{
				this.m_dataAction |= DataActions.PostSortAggregates;
			}
			this.HandleSortFilterEvent();
			if (this.m_canonicalCellScopeDef != null && this.m_canonicalCellScopeDef.CanonicalDataScopeInfo != null && this.m_canonicalCellScopeDef.CanonicalDataScopeInfo.HasAggregatesToUpdateAtRowScope)
			{
				this.m_dataAction |= DataActions.AggregatesOfAggregates;
			}
			if (this.m_dataAction != DataActions.None)
			{
				this.m_dataRows = new ScalableList<DataFieldRow>(this.m_outerGroupDynamicIndex + 1, odpContext.TablixProcessingScalabilityCache, 30);
			}
			odpContext.CreatedScopeInstance(this.m_canonicalCellScopeDef);
			this.m_scopeInstanceNumber = RuntimeDataRegionObj.AssignScopeInstanceNumber(this.GetCanonicalDataScopeInfo());
		}

		// Token: 0x060079AC RID: 31148 RVA: 0x001F52D0 File Offset: 0x001F34D0
		internal static void GetCellIndexes(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode outerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode innerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, out List<int> rowIndexes, out List<int> colIndexes)
		{
			if (innerGroupingMember.IsColumn)
			{
				rowIndexes = ((outerGroupingMember != null) ? outerGroupingMember.GetCellIndexes() : dataRegionDef.OutermostStaticRowIndexes);
				colIndexes = innerGroupingMember.GetCellIndexes();
				return;
			}
			rowIndexes = innerGroupingMember.GetCellIndexes();
			colIndexes = ((outerGroupingMember != null) ? outerGroupingMember.GetCellIndexes() : dataRegionDef.OutermostStaticColumnIndexes);
		}

		// Token: 0x060079AD RID: 31149 RVA: 0x001F5320 File Offset: 0x001F3520
		internal static bool HasOnlySimpleGroupTreeCells(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode outerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode innerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef)
		{
			List<int> list;
			List<int> list2;
			RuntimeCell.GetCellIndexes(outerGroupingMember, innerGroupingMember, dataRegionDef, out list, out list2);
			foreach (int num in list)
			{
				foreach (int num2 in list2)
				{
					Cell cell = dataRegionDef.Rows[num].Cells[num2];
					if (cell != null && !cell.SimpleGroupTreeCell)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x17002831 RID: 10289
		// (get) Token: 0x060079AE RID: 31150 RVA: 0x001F53DC File Offset: 0x001F35DC
		// (set) Token: 0x060079AF RID: 31151 RVA: 0x001F53E4 File Offset: 0x001F35E4
		internal int NextCell
		{
			get
			{
				return this.m_nextCell;
			}
			set
			{
				this.m_nextCell = value;
			}
		}

		// Token: 0x060079B0 RID: 31152
		protected abstract void ConstructCellContents(Cell cell, ref DataActions dataAction);

		// Token: 0x060079B1 RID: 31153
		protected abstract void CreateInstanceCellContents(Cell cell, DataCellInstance cellInstance, OnDemandProcessingContext odpContext);

		// Token: 0x060079B2 RID: 31154 RVA: 0x001F53F0 File Offset: 0x001F35F0
		internal virtual bool NextRow()
		{
			OnDemandProcessingContext odpContext = this.m_owner.Value().OdpContext;
			RuntimeDataRegionObj.CommonFirstRow(odpContext, ref this.m_firstRowIsAggregate, ref this.m_firstRow);
			this.NextAggregateRow();
			if (!odpContext.ReportObjectModel.FieldsImpl.IsAggregateRow)
			{
				this.NextNonAggregateRow();
			}
			return true;
		}

		// Token: 0x060079B3 RID: 31155 RVA: 0x001F5440 File Offset: 0x001F3640
		private void NextNonAggregateRow()
		{
			OnDemandProcessingContext odpContext = this.m_owner.Value().OdpContext;
			RuntimeDataRegionObj.UpdateAggregates(odpContext, this.m_cellNonCustomAggObjs, false);
			if (this.m_dataRows != null)
			{
				RuntimeDataTablixObj.SaveData(this.m_dataRows, odpContext);
			}
		}

		// Token: 0x060079B4 RID: 31156 RVA: 0x001F5480 File Offset: 0x001F3680
		private void NextAggregateRow()
		{
			OnDemandProcessingContext odpContext = this.m_owner.Value().OdpContext;
			FieldsImpl fieldsImpl = odpContext.ReportObjectModel.FieldsImpl;
			if (fieldsImpl.ValidAggregateRow && fieldsImpl.AggregationFieldCount == 0)
			{
				this.m_hasProcessedAggregateRow = true;
				if (this.m_cellCustomAggObjs != null)
				{
					RuntimeDataRegionObj.UpdateAggregates(odpContext, this.m_cellCustomAggObjs, false);
				}
			}
		}

		// Token: 0x060079B5 RID: 31157 RVA: 0x001F54D8 File Offset: 0x001F36D8
		internal void SortAndFilter(AggregateUpdateContext aggContext)
		{
			this.SetupEnvironment();
			AggregateUpdateQueue aggregateUpdateQueue = RuntimeDataRegionObj.AggregateOfAggregatesStart(aggContext, this, this.GetCanonicalDataScopeInfo(), this.m_cellAggregatesOfAggregates, AggregateUpdateFlags.Both, false);
			this.TraverseCellContents(ProcessingStages.SortAndFilter, aggContext);
			RuntimeDataRegionObj.AggregatesOfAggregatesEnd(this, aggContext, aggregateUpdateQueue, this.GetCanonicalDataScopeInfo(), this.m_cellAggregatesOfAggregates, true);
		}

		// Token: 0x060079B6 RID: 31158 RVA: 0x001F551E File Offset: 0x001F371E
		protected virtual void TraverseCellContents(ProcessingStages operation, AggregateUpdateContext context)
		{
		}

		// Token: 0x060079B7 RID: 31159 RVA: 0x001F5520 File Offset: 0x001F3720
		public void UpdateAggregates(AggregateUpdateContext aggContext)
		{
			this.SetupEnvironment();
			if (RuntimeDataRegionObj.UpdateAggregatesAtScope(aggContext, this, this.GetCanonicalDataScopeInfo(), AggregateUpdateFlags.Both, false))
			{
				this.TraverseCellContents(ProcessingStages.UpdateAggregates, aggContext);
			}
		}

		// Token: 0x060079B8 RID: 31160 RVA: 0x001F5541 File Offset: 0x001F3741
		private DataScopeInfo GetCanonicalDataScopeInfo()
		{
			if (this.m_canonicalCellScopeDef == null)
			{
				return null;
			}
			return this.m_canonicalCellScopeDef.CanonicalDataScopeInfo;
		}

		// Token: 0x060079B9 RID: 31161 RVA: 0x001F5558 File Offset: 0x001F3758
		protected void HandleSortFilterEvent()
		{
			using (this.m_owner.PinValue())
			{
				RuntimeDataTablixGroupLeafObj runtimeDataTablixGroupLeafObj = this.m_owner.Value();
				if (runtimeDataTablixGroupLeafObj.NeedHandleCellSortFilterEvent())
				{
					OnDemandProcessingContext odpContext = runtimeDataTablixGroupLeafObj.OdpContext;
					int count = odpContext.RuntimeSortFilterInfo.Count;
					for (int i = 0; i < count; i++)
					{
						IReference<RuntimeSortFilterEventInfo> reference = odpContext.RuntimeSortFilterInfo[i];
						using (reference.PinValue())
						{
							RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = reference.Value();
							if (runtimeSortFilterEventInfo.EventSource.IsTablixCellScope)
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem = runtimeSortFilterEventInfo.EventSource.Parent;
								while (reportItem != null && !reportItem.IsDataRegion)
								{
									reportItem = reportItem.Parent;
								}
								if (reportItem == runtimeDataTablixGroupLeafObj.DataRegionDef && ((IScope)this).TargetScopeMatched(i, false) && !runtimeDataTablixGroupLeafObj.GetOwnerDataTablix().Value().TargetForNonDetailSort && !runtimeSortFilterEventInfo.HasEventSourceScope)
								{
									runtimeSortFilterEventInfo.SetEventSourceScope(false, this.SelfReference, -1);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060079BA RID: 31162 RVA: 0x001F567C File Offset: 0x001F387C
		internal void CalculateRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
			OnDemandProcessingContext odpContext = this.m_owner.Value().OdpContext;
			if (this.GetCanonicalDataScopeInfo() != null)
			{
				List<string> list = null;
				List<string> list2 = null;
				RuntimeDataTablixObj.AddRunningValues(odpContext, this.GetCanonicalDataScopeInfo().RunningValuesOfAggregates, ref list, ref list2, groupCol, lastGroup);
			}
			bool flag = this.m_dataRows != null && FlagUtils.HasFlag(this.m_dataAction, DataActions.PostSortAggregates);
			AggregateUpdateQueue aggregateUpdateQueue = RuntimeDataRegionObj.AggregateOfAggregatesStart(aggContext, this, this.GetCanonicalDataScopeInfo(), this.m_cellPostSortAggregatesOfAggregates, flag ? AggregateUpdateFlags.ScopedAggregates : AggregateUpdateFlags.Both, true);
			if (flag)
			{
				DataActions dataActions = DataActions.PostSortAggregates;
				if (aggContext.LastScopeNeedsRowAggregateProcessing())
				{
					dataActions |= DataActions.PostSortAggregatesOfAggregates;
				}
				this.ReadRows(dataActions, aggContext);
				this.m_dataRows.Clear();
				this.m_dataRows = null;
			}
			using (this.m_owner.PinValue())
			{
				RuntimeDataTablixGroupLeafObj runtimeDataTablixGroupLeafObj = this.m_owner.Value();
				if (this.m_cellAggValueList != null)
				{
					List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> cellPostSortAggregates = runtimeDataTablixGroupLeafObj.CellPostSortAggregates;
					if (cellPostSortAggregates != null && 0 < cellPostSortAggregates.Count)
					{
						for (int i = 0; i < cellPostSortAggregates.Count; i++)
						{
							this.m_cellAggValueList[i] = cellPostSortAggregates[i].AggregateResult();
							cellPostSortAggregates[i].Init();
						}
					}
				}
			}
			this.CalculateInnerRunningValues(groupCol, lastGroup, aggContext);
			RuntimeDataRegionObj.AggregatesOfAggregatesEnd(this, aggContext, aggregateUpdateQueue, this.GetCanonicalDataScopeInfo(), this.m_cellPostSortAggregatesOfAggregates, true);
			if (odpContext.HasPreviousAggregates)
			{
				this.CalculatePreviousAggregates();
			}
			this.DoneReadingRows();
		}

		// Token: 0x060079BB RID: 31163 RVA: 0x001F57E8 File Offset: 0x001F39E8
		internal void DoneReadingRows()
		{
			bool flag = this.m_runningValueValues != null;
			bool flag2 = this.m_runningValueValues != null;
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef = this.m_owner.Value().DataRegionDef;
			List<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo> cellRunningValues = dataRegionDef.CellRunningValues;
			if (cellRunningValues != null || (this.m_canonicalCellScopeDef != null && this.m_canonicalCellScopeDef.CanonicalDataScopeInfo != null && this.m_canonicalCellScopeDef.CanonicalDataScopeInfo.HasRunningValues))
			{
				AggregatesImpl aggregatesImpl = this.m_owner.Value().OdpContext.ReportObjectModel.AggregatesImpl;
				RowList rows = dataRegionDef.Rows;
				if (this.m_runningValueValues == null)
				{
					this.m_runningValueValues = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[this.m_rowIndexes.Count, this.m_colIndexes.Count][];
				}
				if (this.m_runningValueOfAggregateValues == null)
				{
					this.m_runningValueOfAggregateValues = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[this.m_rowIndexes.Count, this.m_colIndexes.Count][];
				}
				for (int i = 0; i < this.m_rowIndexes.Count; i++)
				{
					for (int j = 0; j < this.m_colIndexes.Count; j++)
					{
						int num = this.m_rowIndexes[i];
						int num2 = this.m_colIndexes[j];
						Cell cell = rows[num].Cells[num2];
						List<int> runningValueIndexes = cell.RunningValueIndexes;
						if (runningValueIndexes != null)
						{
							flag = true;
							Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] array = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[runningValueIndexes.Count];
							this.m_runningValueValues[i, j] = array;
							for (int k = 0; k < runningValueIndexes.Count; k++)
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo runningValueInfo = cellRunningValues[runningValueIndexes[k]];
								Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj = aggregatesImpl.GetAggregateObj(runningValueInfo.Name);
								array[k] = aggregateObj.AggregateResult();
							}
						}
						if (cell.DataScopeInfo != null)
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] array2 = null;
							RuntimeRICollection.StoreRunningValues(aggregatesImpl, cell.DataScopeInfo.RunningValuesOfAggregates, ref array2);
							if (array2 != null)
							{
								flag2 = true;
								this.m_runningValueOfAggregateValues[i, j] = array2;
							}
						}
					}
				}
			}
			if (!flag)
			{
				this.m_runningValueValues = null;
			}
			if (!flag2)
			{
				this.m_runningValueOfAggregateValues = null;
			}
		}

		// Token: 0x060079BC RID: 31164 RVA: 0x001F59FC File Offset: 0x001F3BFC
		protected virtual void CalculateInnerRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
		}

		// Token: 0x060079BD RID: 31165 RVA: 0x001F59FE File Offset: 0x001F3BFE
		private void CalculatePreviousAggregates()
		{
			this.SetupEnvironment();
			((IScope)this).CalculatePreviousAggregates();
		}

		// Token: 0x060079BE RID: 31166 RVA: 0x001F5A0C File Offset: 0x001F3C0C
		public void ReadRows(DataActions action, ITraversalContext context)
		{
			for (int i = 0; i < this.m_dataRows.Count; i++)
			{
				this.m_dataRows[i].SetFields(this.m_owner.Value().OdpContext.ReportObjectModel.FieldsImpl);
				this.ReadRow(action, context);
			}
		}

		// Token: 0x060079BF RID: 31167 RVA: 0x001F5A64 File Offset: 0x001F3C64
		protected void SetupAggregates(List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> aggregates, Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] aggValues)
		{
			if (aggregates != null)
			{
				for (int i = 0; i < aggregates.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = aggregates[i];
					this.m_owner.Value().OdpContext.ReportObjectModel.AggregatesImpl.Set(dataAggregateObj.Name, dataAggregateObj.AggregateDef, dataAggregateObj.DuplicateNames, (aggValues == null) ? dataAggregateObj.AggregateResult() : aggValues[i]);
				}
			}
		}

		// Token: 0x060079C0 RID: 31168 RVA: 0x001F5ACC File Offset: 0x001F3CCC
		public void SetupEnvironment()
		{
			this.SetupAggregates(this.m_cellNonCustomAggObjs, null);
			this.SetupAggregates(this.m_cellCustomAggObjs, null);
			RuntimeDataRegionObj.SetupAggregates(this.m_owner.Value().OdpContext, this.m_cellAggregatesOfAggregates);
			RuntimeDataRegionObj.SetupAggregates(this.m_owner.Value().OdpContext, this.m_cellPostSortAggregatesOfAggregates);
			OnDemandProcessingContext odpContext = this.m_owner.Value().OdpContext;
			if (this.m_cellAggValueList != null)
			{
				using (this.m_owner.PinValue())
				{
					this.SetupAggregates(this.m_owner.Value().CellPostSortAggregates, this.m_cellAggValueList);
				}
			}
			if (this.m_canonicalCellScopeDef != null && this.m_canonicalCellScopeDef.DataScopeInfo != null && this.m_canonicalCellScopeDef.DataScopeInfo.DataSet != null && this.m_canonicalCellScopeDef.DataScopeInfo.DataSet.DataSetCore.FieldsContext != null)
			{
				odpContext.ReportObjectModel.RestoreFields(this.m_canonicalCellScopeDef.DataScopeInfo.DataSet.DataSetCore.FieldsContext);
			}
			if (this.m_firstRow != null)
			{
				this.m_firstRow.SetFields(odpContext.ReportObjectModel.FieldsImpl);
			}
			else
			{
				odpContext.ReportObjectModel.ResetFieldValues();
			}
			odpContext.ReportRuntime.CurrentScope = this;
		}

		// Token: 0x060079C1 RID: 31169
		public abstract IOnDemandMemberOwnerInstanceReference GetDataRegionInstance(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion);

		// Token: 0x060079C2 RID: 31170
		public abstract IReference<IDataCorrelation> GetIdcReceiver(IRIFReportDataScope scope);

		// Token: 0x17002832 RID: 10290
		// (get) Token: 0x060079C3 RID: 31171 RVA: 0x001F5C28 File Offset: 0x001F3E28
		public bool IsNoRows
		{
			get
			{
				return this.m_firstRow == null;
			}
		}

		// Token: 0x17002833 RID: 10291
		// (get) Token: 0x060079C4 RID: 31172 RVA: 0x001F5C33 File Offset: 0x001F3E33
		public bool IsMostRecentlyCreatedScopeInstance
		{
			get
			{
				return this.m_canonicalCellScopeDef.CanonicalDataScopeInfo.IsLastScopeInstanceNumber(this.m_scopeInstanceNumber);
			}
		}

		// Token: 0x17002834 RID: 10292
		// (get) Token: 0x060079C5 RID: 31173 RVA: 0x001F5C4B File Offset: 0x001F3E4B
		public bool HasUnProcessedServerAggregate
		{
			get
			{
				return this.m_cellCustomAggObjs != null && this.m_cellCustomAggObjs.Count > 0 && !this.m_hasProcessedAggregateRow;
			}
		}

		// Token: 0x060079C6 RID: 31174 RVA: 0x001F5C70 File Offset: 0x001F3E70
		internal virtual void CreateInstance(IMemberHierarchy dataRegionOrRowMemberInstance, int columnMemberSequenceId)
		{
			this.SetupEnvironment();
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef = this.m_owner.Value().DataRegionDef;
			OnDemandProcessingContext odpContext = this.m_owner.Value().OdpContext;
			for (int i = 0; i < this.m_rowIndexes.Count; i++)
			{
				for (int j = 0; j < this.m_colIndexes.Count; j++)
				{
					int num = this.m_rowIndexes[i];
					int num2 = this.m_colIndexes[j];
					Cell cell = dataRegionDef.Rows[num].Cells[num2];
					if (cell != null)
					{
						DataCellInstance dataCellInstance = null;
						if (cell.SimpleGroupTreeCell)
						{
							if (this.m_firstRow != null && cell.InDynamicRowAndColumnContext)
							{
								dataCellInstance = DataCellInstance.CreateInstance(dataRegionOrRowMemberInstance, odpContext, cell, this.m_firstRow.StreamOffset, columnMemberSequenceId);
							}
						}
						else
						{
							dataCellInstance = DataCellInstance.CreateInstance(dataRegionOrRowMemberInstance, odpContext, cell, (this.m_runningValueValues != null) ? this.m_runningValueValues[i, j] : null, (this.m_runningValueOfAggregateValues != null) ? this.m_runningValueOfAggregateValues[i, j] : null, (this.m_firstRow != null) ? this.m_firstRow.StreamOffset : 0L, columnMemberSequenceId);
						}
						if (dataCellInstance != null)
						{
							if (!cell.SimpleGroupTreeCell)
							{
								this.CreateInstanceCellContents(cell, dataCellInstance, odpContext);
							}
							dataCellInstance.InstanceComplete();
						}
						if (cell.InScopeEventSources != null)
						{
							UserSortFilterContext.ProcessEventSources(odpContext, this, cell.InScopeEventSources);
						}
					}
				}
			}
		}

		// Token: 0x060079C7 RID: 31175 RVA: 0x001F5DDC File Offset: 0x001F3FDC
		private Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.Grouping> GetOuterScopes()
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef = this.m_owner.Value().DataRegionDef;
			Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode memberDef = this.m_owner.Value().MemberDef;
			if (memberDef.CellScopes == null)
			{
				memberDef.CellScopes = new Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.Grouping>[dataRegionDef.OuterGroupingDynamicMemberCount];
			}
			Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.Grouping> dictionary = memberDef.CellScopes[this.m_outerGroupDynamicIndex];
			if (dictionary == null)
			{
				IReference<RuntimeDataTablixGroupRootObj> reference = dataRegionDef.CurrentOuterGroupRootObjs[this.m_outerGroupDynamicIndex];
				Global.Tracer.Assert(reference != null, "(null != outerGroupRoot)");
				Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode hierarchyDef = reference.Value().HierarchyDef;
				Global.Tracer.Assert(hierarchyDef != null, "(null != outerGrouping)");
				dictionary = hierarchyDef.GetScopeNames();
				memberDef.CellScopes[this.m_outerGroupDynamicIndex] = dictionary;
			}
			return dictionary;
		}

		// Token: 0x17002835 RID: 10293
		// (get) Token: 0x060079C8 RID: 31176 RVA: 0x001F5E90 File Offset: 0x001F4090
		bool IScope.TargetForNonDetailSort
		{
			get
			{
				return this.m_owner.Value().GetCellTargetForNonDetailSort();
			}
		}

		// Token: 0x17002836 RID: 10294
		// (get) Token: 0x060079C9 RID: 31177 RVA: 0x001F5EA4 File Offset: 0x001F40A4
		int[] IScope.SortFilterExpressionScopeInfoIndices
		{
			get
			{
				if (this.m_sortFilterExpressionScopeInfoIndices == null)
				{
					OnDemandProcessingContext odpContext = this.m_owner.Value().OdpContext;
					this.m_sortFilterExpressionScopeInfoIndices = new int[odpContext.RuntimeSortFilterInfo.Count];
					for (int i = 0; i < odpContext.RuntimeSortFilterInfo.Count; i++)
					{
						this.m_sortFilterExpressionScopeInfoIndices[i] = -1;
					}
				}
				return this.m_sortFilterExpressionScopeInfoIndices;
			}
		}

		// Token: 0x17002837 RID: 10295
		// (get) Token: 0x060079CA RID: 31178 RVA: 0x001F5F08 File Offset: 0x001F4108
		IRIFReportScope IScope.RIFReportScope
		{
			get
			{
				int num = this.m_rowIndexes[0];
				int num2 = this.m_colIndexes[0];
				return this.m_owner.Value().DataRegionDef.Rows[num].Cells[num2];
			}
		}

		// Token: 0x060079CB RID: 31179 RVA: 0x001F5F55 File Offset: 0x001F4155
		bool IScope.IsTargetForSort(int index, bool detailSort)
		{
			return this.m_owner.Value().GetCellTargetForSort(index, detailSort);
		}

		// Token: 0x060079CC RID: 31180 RVA: 0x001F5F69 File Offset: 0x001F4169
		string IScope.GetScopeName()
		{
			return null;
		}

		// Token: 0x060079CD RID: 31181 RVA: 0x001F5F6C File Offset: 0x001F416C
		IReference<IScope> IScope.GetOuterScope(bool includeSubReportContainingScope)
		{
			return this.m_owner;
		}

		// Token: 0x060079CE RID: 31182 RVA: 0x001F5F74 File Offset: 0x001F4174
		public void ReadRow(DataActions dataAction, ITraversalContext context)
		{
			if (FlagUtils.HasFlag(dataAction, DataActions.PostSortAggregatesOfAggregates) || FlagUtils.HasFlag(dataAction, DataActions.AggregatesOfAggregates))
			{
				((AggregateUpdateContext)context).UpdateAggregatesForRow();
			}
			if (FlagUtils.HasFlag(dataAction, DataActions.PostSortAggregates))
			{
				using (this.m_owner.PinValue())
				{
					this.m_owner.Value().ReadRow(DataActions.PostSortAggregates, context);
				}
			}
		}

		// Token: 0x060079CF RID: 31183 RVA: 0x001F5FE4 File Offset: 0x001F41E4
		void IScope.CalculatePreviousAggregates()
		{
			using (this.m_owner.PinValue())
			{
				this.m_owner.Value().CalculatePreviousAggregates();
			}
		}

		// Token: 0x060079D0 RID: 31184 RVA: 0x001F602C File Offset: 0x001F422C
		bool IScope.InScope(string scope)
		{
			if (this.m_owner.Value().InScope(scope))
			{
				return true;
			}
			Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.Grouping> outerScopes = this.GetOuterScopes();
			return outerScopes != null && outerScopes.Count != 0 && outerScopes.ContainsKey(scope);
		}

		// Token: 0x060079D1 RID: 31185 RVA: 0x001F606C File Offset: 0x001F426C
		int IScope.RecursiveLevel(string scope)
		{
			if (scope == null)
			{
				return 0;
			}
			int num = ((IScope)this.m_owner).RecursiveLevel(scope);
			if (-1 != num)
			{
				return num;
			}
			Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.Grouping> outerScopes = this.GetOuterScopes();
			if (outerScopes == null || outerScopes.Count == 0)
			{
				return -1;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping;
			if (outerScopes.TryGetValue(scope, out grouping))
			{
				return grouping.RecursiveLevel;
			}
			return -1;
		}

		// Token: 0x060079D2 RID: 31186 RVA: 0x001F60C0 File Offset: 0x001F42C0
		bool IScope.TargetScopeMatched(int index, bool detailSort)
		{
			if (!this.m_owner.Value().TargetScopeMatched(index, detailSort))
			{
				return false;
			}
			IDictionaryEnumerator dictionaryEnumerator = this.GetOuterScopes().GetEnumerator();
			while (dictionaryEnumerator.MoveNext())
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = (Microsoft.ReportingServices.ReportIntermediateFormat.Grouping)dictionaryEnumerator.Value;
				if ((!detailSort || grouping.SortFilterScopeInfo != null) && grouping.SortFilterScopeMatched != null && !grouping.SortFilterScopeMatched[index])
				{
					return false;
				}
			}
			if (detailSort)
			{
				return true;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef = this.m_owner.Value().DataRegionDef;
			IReference<RuntimeSortFilterEventInfo> reference = this.m_owner.Value().OdpContext.RuntimeSortFilterInfo[index];
			using (reference.PinValue())
			{
				List<object>[] sortSourceScopeInfo = reference.Value().SortSourceScopeInfo;
				Microsoft.ReportingServices.ReportIntermediateFormat.Grouping groupingDef = this.m_owner.Value().GroupingDef;
				if (groupingDef.SortFilterScopeIndex != null && -1 != groupingDef.SortFilterScopeIndex[index])
				{
					int num = groupingDef.SortFilterScopeIndex[index] + 1;
					if (!this.m_innermost)
					{
						int innerGroupingMaximumDynamicLevel = dataRegionDef.InnerGroupingMaximumDynamicLevel;
						int num2 = this.m_owner.Value().HeadingLevel + 1;
						while (num2 < innerGroupingMaximumDynamicLevel && num < sortSourceScopeInfo.Length)
						{
							if (sortSourceScopeInfo[num] != null)
							{
								return false;
							}
							num2++;
							num++;
						}
					}
				}
				Global.Tracer.Assert(dataRegionDef.CurrentOuterGroupRootObjs[this.m_outerGroupDynamicIndex] != null, "(null != dataRegionDef.CurrentOuterGroupRootObjs[m_cellLevel])");
				if (this.m_outerGroupDynamicIndex + 1 < dataRegionDef.OuterGroupingDynamicMemberCount)
				{
					IReference<RuntimeDataTablixGroupRootObj> reference2 = dataRegionDef.CurrentOuterGroupRootObjs[this.m_outerGroupDynamicIndex + 1];
					Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode reportHierarchyNode = ((reference2 != null) ? reference2.Value().HierarchyDef : null);
					if (reportHierarchyNode != null && reportHierarchyNode.Grouping.SortFilterScopeIndex != null && -1 != reportHierarchyNode.Grouping.SortFilterScopeIndex[index])
					{
						int outerGroupingMaximumDynamicLevel = dataRegionDef.OuterGroupingMaximumDynamicLevel;
						int num = reportHierarchyNode.Grouping.SortFilterScopeIndex[index];
						int num3 = this.m_outerGroupDynamicIndex + 1;
						while (num3 < outerGroupingMaximumDynamicLevel && num < sortSourceScopeInfo.Length)
						{
							if (sortSourceScopeInfo[num] != null)
							{
								return false;
							}
							num3++;
							num++;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060079D3 RID: 31187 RVA: 0x001F62E0 File Offset: 0x001F44E0
		void IScope.GetScopeValues(IReference<IHierarchyObj> targetScopeObj, List<object>[] scopeValues, ref int index)
		{
			this.m_owner.Value().GetScopeValues(targetScopeObj, scopeValues, ref index);
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef = this.m_owner.Value().DataRegionDef;
			if (!this.m_innermost)
			{
				int innerGroupingMaximumDynamicLevel = dataRegionDef.InnerGroupingMaximumDynamicLevel;
				int num = this.m_owner.Value().HeadingLevel + 1;
				while (num < innerGroupingMaximumDynamicLevel && index < scopeValues.Length)
				{
					int num2 = index;
					index = num2 + 1;
					scopeValues[num2] = null;
					num++;
				}
			}
			Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.Grouping> outerScopes = this.GetOuterScopes();
			IDictionaryEnumerator dictionaryEnumerator = outerScopes.GetEnumerator();
			while (dictionaryEnumerator.MoveNext())
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = (Microsoft.ReportingServices.ReportIntermediateFormat.Grouping)dictionaryEnumerator.Value;
				if (index < scopeValues.Length)
				{
					Global.Tracer.Assert(index < scopeValues.Length, "Inner groupings");
					int num2 = index;
					index = num2 + 1;
					scopeValues[num2] = grouping.CurrentGroupExpressionValues;
				}
			}
			int outerGroupingMaximumDynamicLevel = dataRegionDef.OuterGroupingMaximumDynamicLevel;
			int num3 = outerScopes.Count;
			while (num3 < outerGroupingMaximumDynamicLevel && index < scopeValues.Length)
			{
				int num2 = index;
				index = num2 + 1;
				scopeValues[num2] = null;
				num3++;
			}
		}

		// Token: 0x17002838 RID: 10296
		// (get) Token: 0x060079D4 RID: 31188 RVA: 0x001F63E7 File Offset: 0x001F45E7
		int IScope.Depth
		{
			get
			{
				return this.m_outerGroupDynamicIndex;
			}
		}

		// Token: 0x060079D5 RID: 31189 RVA: 0x001F63F0 File Offset: 0x001F45F0
		void IScope.GetGroupNameValuePairs(Dictionary<string, object> pairs)
		{
			((IScope)this.m_owner).GetGroupNameValuePairs(pairs);
			Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.Grouping> outerScopes = this.GetOuterScopes();
			if (outerScopes != null || outerScopes.Count != 0)
			{
				foreach (object obj in outerScopes.Values)
				{
					RuntimeDataRegionObj.AddGroupNameValuePair(this.m_owner.Value().OdpContext, obj as Microsoft.ReportingServices.ReportIntermediateFormat.Grouping, pairs);
				}
			}
		}

		// Token: 0x060079D6 RID: 31190 RVA: 0x001F645C File Offset: 0x001F465C
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeCell.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.AggregatesOfAggregates)
				{
					if (memberName <= MemberName.DataAction)
					{
						switch (memberName)
						{
						case MemberName.Owner:
							writer.Write(this.m_owner);
							continue;
						case MemberName.ScopeInstances:
						case MemberName.ScopeValuesList:
						case MemberName.SortTree:
						case MemberName.CurrentScopeIndex:
						case MemberName.ScopeInstanceIndices:
						case MemberName.Tuples:
						case MemberName.IndexInParent:
						case MemberName.List:
						case MemberName.Capacity:
						case MemberName.Child:
						case MemberName.Key:
						case MemberName.HierarchyNode:
							break;
						case MemberName.OuterGroupDynamicIndex:
							writer.Write(this.m_outerGroupDynamicIndex);
							continue;
						case MemberName.RowIndexes:
							writer.WriteListOfPrimitives<int>(this.m_rowIndexes);
							continue;
						case MemberName.ColumnIndexes:
							writer.WriteListOfPrimitives<int>(this.m_colIndexes);
							continue;
						case MemberName.CellNonCustomAggObjs:
							writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_cellNonCustomAggObjs);
							continue;
						case MemberName.CellCustomAggObjs:
							writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_cellCustomAggObjs);
							continue;
						case MemberName.CellAggValueList:
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] cellAggValueList = this.m_cellAggValueList;
							writer.Write(cellAggValueList);
							continue;
						}
						case MemberName.RunningValueValues:
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[,][] array = this.m_runningValueValues;
							writer.Write(array);
							continue;
						}
						case MemberName.Innermost:
							writer.Write(this.m_innermost);
							continue;
						case MemberName.FirstRow:
							writer.Write(this.m_firstRow);
							continue;
						case MemberName.FirstRowIsAggregate:
							writer.Write(this.m_firstRowIsAggregate);
							continue;
						case MemberName.NextCell:
							writer.Write(this.m_nextCell);
							continue;
						case MemberName.SortFilterExpressionScopeInfoIndices:
							writer.Write(this.m_sortFilterExpressionScopeInfoIndices);
							continue;
						default:
							if (memberName == MemberName.DataAction)
							{
								writer.WriteEnum((int)this.m_dataAction);
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.DataRows)
						{
							writer.Write(this.m_dataRows);
							continue;
						}
						if (memberName == MemberName.AggregatesOfAggregates)
						{
							writer.Write(this.m_cellAggregatesOfAggregates);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.RunningValueOfAggregateValues)
				{
					if (memberName == MemberName.PostSortAggregatesOfAggregates)
					{
						writer.Write(this.m_cellPostSortAggregatesOfAggregates);
						continue;
					}
					if (memberName == MemberName.RunningValueOfAggregateValues)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[,][] array = this.m_runningValueOfAggregateValues;
						writer.Write(array);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.CanonicalCellScope)
					{
						int num = scalabilityCache.StoreStaticReference(this.m_canonicalCellScopeDef);
						writer.Write(num);
						continue;
					}
					if (memberName == MemberName.ScopeInstanceNumber)
					{
						writer.Write(this.m_scopeInstanceNumber);
						continue;
					}
					if (memberName == MemberName.HasProcessedAggregateRow)
					{
						writer.Write(this.m_hasProcessedAggregateRow);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060079D7 RID: 31191 RVA: 0x001F670C File Offset: 0x001F490C
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeCell.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.AggregatesOfAggregates)
				{
					if (memberName <= MemberName.DataAction)
					{
						switch (memberName)
						{
						case MemberName.Owner:
							this.m_owner = (RuntimeDataTablixGroupLeafObjReference)reader.ReadRIFObject();
							continue;
						case MemberName.ScopeInstances:
						case MemberName.ScopeValuesList:
						case MemberName.SortTree:
						case MemberName.CurrentScopeIndex:
						case MemberName.ScopeInstanceIndices:
						case MemberName.Tuples:
						case MemberName.IndexInParent:
						case MemberName.List:
						case MemberName.Capacity:
						case MemberName.Child:
						case MemberName.Key:
						case MemberName.HierarchyNode:
							break;
						case MemberName.OuterGroupDynamicIndex:
							this.m_outerGroupDynamicIndex = reader.ReadInt32();
							continue;
						case MemberName.RowIndexes:
							this.m_rowIndexes = reader.ReadListOfPrimitives<int>();
							continue;
						case MemberName.ColumnIndexes:
							this.m_colIndexes = reader.ReadListOfPrimitives<int>();
							continue;
						case MemberName.CellNonCustomAggObjs:
							this.m_cellNonCustomAggObjs = reader.ReadListOfRIFObjects<List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>>();
							continue;
						case MemberName.CellCustomAggObjs:
							this.m_cellCustomAggObjs = reader.ReadListOfRIFObjects<List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>>();
							continue;
						case MemberName.CellAggValueList:
							this.m_cellAggValueList = reader.ReadArrayOfRIFObjects<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>();
							continue;
						case MemberName.RunningValueValues:
							this.m_runningValueValues = reader.Read2DArrayOfArrayOfRIFObjects<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>();
							continue;
						case MemberName.Innermost:
							this.m_innermost = reader.ReadBoolean();
							continue;
						case MemberName.FirstRow:
							this.m_firstRow = (DataFieldRow)reader.ReadRIFObject();
							continue;
						case MemberName.FirstRowIsAggregate:
							this.m_firstRowIsAggregate = reader.ReadBoolean();
							continue;
						case MemberName.NextCell:
							this.m_nextCell = reader.ReadInt32();
							continue;
						case MemberName.SortFilterExpressionScopeInfoIndices:
							this.m_sortFilterExpressionScopeInfoIndices = reader.ReadInt32Array();
							continue;
						default:
							if (memberName == MemberName.DataAction)
							{
								this.m_dataAction = (DataActions)reader.ReadEnum();
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.DataRows)
						{
							this.m_dataRows = reader.ReadRIFObject<ScalableList<DataFieldRow>>();
							continue;
						}
						if (memberName == MemberName.AggregatesOfAggregates)
						{
							this.m_cellAggregatesOfAggregates = (BucketedDataAggregateObjs)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.RunningValueOfAggregateValues)
				{
					if (memberName == MemberName.PostSortAggregatesOfAggregates)
					{
						this.m_cellPostSortAggregatesOfAggregates = (BucketedDataAggregateObjs)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.RunningValueOfAggregateValues)
					{
						this.m_runningValueOfAggregateValues = reader.Read2DArrayOfArrayOfRIFObjects<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.CanonicalCellScope)
					{
						int num = reader.ReadInt32();
						this.m_canonicalCellScopeDef = (Cell)scalabilityCache.FetchStaticReference(num);
						continue;
					}
					if (memberName == MemberName.ScopeInstanceNumber)
					{
						this.m_scopeInstanceNumber = reader.ReadInt64();
						continue;
					}
					if (memberName == MemberName.HasProcessedAggregateRow)
					{
						this.m_hasProcessedAggregateRow = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060079D8 RID: 31192 RVA: 0x001F69CF File Offset: 0x001F4BCF
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x060079D9 RID: 31193 RVA: 0x001F69D1 File Offset: 0x001F4BD1
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCell;
		}

		// Token: 0x060079DA RID: 31194 RVA: 0x001F69D8 File Offset: 0x001F4BD8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeCell.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Owner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupLeafObjReference),
					new MemberInfo(MemberName.OuterGroupDynamicIndex, Token.Int32),
					new MemberInfo(MemberName.RowIndexes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32),
					new MemberInfo(MemberName.ColumnIndexes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32),
					new MemberInfo(MemberName.CellNonCustomAggObjs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.CellCustomAggObjs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.CellAggValueList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjResult),
					new MemberInfo(MemberName.RunningValueValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Array2D, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray),
					new MemberInfo(MemberName.DataRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList),
					new MemberInfo(MemberName.Innermost, Token.Boolean),
					new MemberInfo(MemberName.FirstRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRow),
					new MemberInfo(MemberName.FirstRowIsAggregate, Token.Boolean),
					new MemberInfo(MemberName.NextCell, Token.Int32),
					new MemberInfo(MemberName.SortFilterExpressionScopeInfoIndices, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Int32),
					new MemberInfo(MemberName.AggregatesOfAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateObjs),
					new MemberInfo(MemberName.PostSortAggregatesOfAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateObjs),
					new MemberInfo(MemberName.CanonicalCellScope, Token.Int32),
					new MemberInfo(MemberName.DataAction, Token.Enum),
					new MemberInfo(MemberName.RunningValueOfAggregateValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Array2D, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray),
					new MemberInfo(MemberName.ScopeInstanceNumber, Token.Int64),
					new MemberInfo(MemberName.HasProcessedAggregateRow, Token.Boolean)
				});
			}
			return RuntimeCell.m_declaration;
		}

		// Token: 0x17002839 RID: 10297
		// (get) Token: 0x060079DB RID: 31195 RVA: 0x001F6B88 File Offset: 0x001F4D88
		internal RuntimeCellReference SelfReference
		{
			get
			{
				return this.m_selfReference;
			}
		}

		// Token: 0x060079DC RID: 31196 RVA: 0x001F6B90 File Offset: 0x001F4D90
		public void SetReference(IReference selfRef)
		{
			this.m_selfReference = (RuntimeCellReference)selfRef;
		}

		// Token: 0x1700283A RID: 10298
		// (get) Token: 0x060079DD RID: 31197 RVA: 0x001F6BA0 File Offset: 0x001F4DA0
		public virtual int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_owner) + 4 + ItemSizes.SizeOf(this.m_rowIndexes) + ItemSizes.SizeOf(this.m_colIndexes) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_cellNonCustomAggObjs) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_cellCustomAggObjs) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>(this.m_cellAggValueList) + ItemSizes.SizeOf(this.m_runningValueValues) + ItemSizes.SizeOf(this.m_runningValueOfAggregateValues) + ItemSizes.SizeOf<DataFieldRow>(this.m_dataRows) + 1 + ItemSizes.SizeOf(this.m_firstRow) + 1 + 4 + ItemSizes.SizeOf(this.m_sortFilterExpressionScopeInfoIndices) + ItemSizes.SizeOf(this.m_selfReference) + ItemSizes.SizeOf(this.m_cellAggregatesOfAggregates) + ItemSizes.SizeOf(this.m_cellPostSortAggregatesOfAggregates) + ItemSizes.ReferenceSize + 4 + 8 + 1;
			}
		}

		// Token: 0x04003D0A RID: 15626
		protected RuntimeDataTablixGroupLeafObjReference m_owner;

		// Token: 0x04003D0B RID: 15627
		protected int m_outerGroupDynamicIndex;

		// Token: 0x04003D0C RID: 15628
		protected List<int> m_rowIndexes;

		// Token: 0x04003D0D RID: 15629
		protected List<int> m_colIndexes;

		// Token: 0x04003D0E RID: 15630
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_cellNonCustomAggObjs;

		// Token: 0x04003D0F RID: 15631
		protected BucketedDataAggregateObjs m_cellAggregatesOfAggregates;

		// Token: 0x04003D10 RID: 15632
		protected BucketedDataAggregateObjs m_cellPostSortAggregatesOfAggregates;

		// Token: 0x04003D11 RID: 15633
		protected Cell m_canonicalCellScopeDef;

		// Token: 0x04003D12 RID: 15634
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_cellCustomAggObjs;

		// Token: 0x04003D13 RID: 15635
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] m_cellAggValueList;

		// Token: 0x04003D14 RID: 15636
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[,][] m_runningValueValues;

		// Token: 0x04003D15 RID: 15637
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[,][] m_runningValueOfAggregateValues;

		// Token: 0x04003D16 RID: 15638
		protected ScalableList<DataFieldRow> m_dataRows;

		// Token: 0x04003D17 RID: 15639
		protected DataActions m_dataAction;

		// Token: 0x04003D18 RID: 15640
		protected bool m_innermost;

		// Token: 0x04003D19 RID: 15641
		protected DataFieldRow m_firstRow;

		// Token: 0x04003D1A RID: 15642
		protected bool m_firstRowIsAggregate;

		// Token: 0x04003D1B RID: 15643
		private int m_nextCell = -1;

		// Token: 0x04003D1C RID: 15644
		protected int[] m_sortFilterExpressionScopeInfoIndices;

		// Token: 0x04003D1D RID: 15645
		private long m_scopeInstanceNumber;

		// Token: 0x04003D1E RID: 15646
		private bool m_hasProcessedAggregateRow;

		// Token: 0x04003D1F RID: 15647
		[NonSerialized]
		protected RuntimeCellReference m_selfReference;

		// Token: 0x04003D20 RID: 15648
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeCell.GetDeclaration();
	}
}
