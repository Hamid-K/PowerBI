using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200077B RID: 1915
	internal sealed class UserSortFilterContext
	{
		// Token: 0x06006ABF RID: 27327 RVA: 0x001AEDD3 File Offset: 0x001ACFD3
		internal UserSortFilterContext()
		{
		}

		// Token: 0x06006AC0 RID: 27328 RVA: 0x001AEDE4 File Offset: 0x001ACFE4
		internal UserSortFilterContext(UserSortFilterContext copy)
		{
			this.m_runtimeSortFilterInfo = copy.RuntimeSortFilterInfo;
			this.m_currentContainingScope = copy.CurrentContainingScope;
			this.m_containingScopes = copy.ContainingScopes;
			this.m_dataSetID = copy.DataSetID;
			this.m_detailScopeSubReports = copy.DetailScopeSubReports;
			this.m_inProcessUserSortPhase = copy.m_inProcessUserSortPhase;
		}

		// Token: 0x06006AC1 RID: 27329 RVA: 0x001AEE48 File Offset: 0x001AD048
		internal UserSortFilterContext(UserSortFilterContext parentContext, SubReport subReport)
		{
			this.m_runtimeSortFilterInfo = parentContext.RuntimeSortFilterInfo;
			this.m_dataSetID = parentContext.DataSetID;
			this.m_containingScopes = subReport.ContainingScopes;
			this.m_detailScopeSubReports = subReport.DetailScopeSubReports;
			this.m_inProcessUserSortPhase = parentContext.m_inProcessUserSortPhase;
		}

		// Token: 0x1700254D RID: 9549
		// (get) Token: 0x06006AC2 RID: 27330 RVA: 0x001AEE9E File Offset: 0x001AD09E
		// (set) Token: 0x06006AC3 RID: 27331 RVA: 0x001AEEA6 File Offset: 0x001AD0A6
		internal TextBox CurrentSortFilterEventSource
		{
			get
			{
				return this.m_currentSortFilterEventSource;
			}
			set
			{
				this.m_currentSortFilterEventSource = value;
			}
		}

		// Token: 0x1700254E RID: 9550
		// (get) Token: 0x06006AC4 RID: 27332 RVA: 0x001AEEAF File Offset: 0x001AD0AF
		// (set) Token: 0x06006AC5 RID: 27333 RVA: 0x001AEEB7 File Offset: 0x001AD0B7
		internal RuntimeSortFilterEventInfoList RuntimeSortFilterInfo
		{
			get
			{
				return this.m_runtimeSortFilterInfo;
			}
			set
			{
				this.m_runtimeSortFilterInfo = value;
			}
		}

		// Token: 0x1700254F RID: 9551
		// (get) Token: 0x06006AC6 RID: 27334 RVA: 0x001AEEC0 File Offset: 0x001AD0C0
		// (set) Token: 0x06006AC7 RID: 27335 RVA: 0x001AEEC8 File Offset: 0x001AD0C8
		internal int DataSetID
		{
			get
			{
				return this.m_dataSetID;
			}
			set
			{
				this.m_dataSetID = value;
			}
		}

		// Token: 0x17002550 RID: 9552
		// (get) Token: 0x06006AC8 RID: 27336 RVA: 0x001AEED1 File Offset: 0x001AD0D1
		// (set) Token: 0x06006AC9 RID: 27337 RVA: 0x001AEED9 File Offset: 0x001AD0D9
		internal ReportProcessing.IScope CurrentContainingScope
		{
			get
			{
				return this.m_currentContainingScope;
			}
			set
			{
				this.m_currentContainingScope = value;
			}
		}

		// Token: 0x17002551 RID: 9553
		// (get) Token: 0x06006ACA RID: 27338 RVA: 0x001AEEE2 File Offset: 0x001AD0E2
		// (set) Token: 0x06006ACB RID: 27339 RVA: 0x001AEEEA File Offset: 0x001AD0EA
		internal GroupingList ContainingScopes
		{
			get
			{
				return this.m_containingScopes;
			}
			set
			{
				this.m_containingScopes = value;
			}
		}

		// Token: 0x17002552 RID: 9554
		// (get) Token: 0x06006ACC RID: 27340 RVA: 0x001AEEF3 File Offset: 0x001AD0F3
		// (set) Token: 0x06006ACD RID: 27341 RVA: 0x001AEEFB File Offset: 0x001AD0FB
		internal SubReportList DetailScopeSubReports
		{
			get
			{
				return this.m_detailScopeSubReports;
			}
			set
			{
				this.m_detailScopeSubReports = value;
			}
		}

		// Token: 0x06006ACE RID: 27342 RVA: 0x001AEF04 File Offset: 0x001AD104
		internal bool PopulateRuntimeSortFilterEventInfo(ReportProcessing.ProcessingContext pc, DataSet myDataSet)
		{
			if (pc.UserSortFilterInfo == null || pc.UserSortFilterInfo.SortInfo == null || pc.OldSortFilterEventInfo == null)
			{
				return false;
			}
			if (this.m_dataSetID != -1)
			{
				return false;
			}
			this.m_runtimeSortFilterInfo = null;
			EventInformation.SortEventInfo sortInfo = pc.UserSortFilterInfo.SortInfo;
			for (int i = 0; i < sortInfo.Count; i++)
			{
				int uniqueNameAt = sortInfo.GetUniqueNameAt(i);
				SortFilterEventInfo sortFilterEventInfo = pc.OldSortFilterEventInfo[uniqueNameAt];
				if (sortFilterEventInfo != null && sortFilterEventInfo.EventSource.UserSort != null && sortFilterEventInfo.EventSource.UserSort.DataSetID == myDataSet.ID)
				{
					if (this.m_runtimeSortFilterInfo == null)
					{
						this.m_runtimeSortFilterInfo = new RuntimeSortFilterEventInfoList();
					}
					this.m_runtimeSortFilterInfo.Add(new RuntimeSortFilterEventInfo(sortFilterEventInfo.EventSource, uniqueNameAt, sortInfo.GetSortDirectionAt(i), sortFilterEventInfo.EventSourceScopeInfo));
				}
			}
			if (this.m_runtimeSortFilterInfo != null)
			{
				int count = this.m_runtimeSortFilterInfo.Count;
				for (int j = 0; j < count; j++)
				{
					TextBox eventSource = this.m_runtimeSortFilterInfo[j].EventSource;
					ISortFilterScope sortExpressionScope = eventSource.UserSort.SortExpressionScope;
					if (sortExpressionScope != null)
					{
						sortExpressionScope.IsSortFilterExpressionScope = this.SetSortFilterInfo(sortExpressionScope.IsSortFilterExpressionScope, count, j);
					}
					ISortFilterScope sortTarget = eventSource.UserSort.SortTarget;
					if (sortTarget != null)
					{
						sortTarget.IsSortFilterTarget = this.SetSortFilterInfo(sortTarget.IsSortFilterTarget, count, j);
					}
					if (eventSource.ContainingScopes != null && 0 < eventSource.ContainingScopes.Count)
					{
						int num = 0;
						for (int k = 0; k < eventSource.ContainingScopes.Count; k++)
						{
							Grouping grouping = eventSource.ContainingScopes[k];
							VariantList variantList = this.m_runtimeSortFilterInfo[j].SortSourceScopeInfo[k];
							if (grouping != null)
							{
								if (grouping.SortFilterScopeInfo == null)
								{
									grouping.SortFilterScopeInfo = new VariantList[count];
									for (int l = 0; l < count; l++)
									{
										grouping.SortFilterScopeInfo[l] = null;
									}
									grouping.SortFilterScopeIndex = new int[count];
									for (int m = 0; m < count; m++)
									{
										grouping.SortFilterScopeIndex[m] = -1;
									}
								}
								grouping.SortFilterScopeInfo[j] = variantList;
								grouping.SortFilterScopeIndex[j] = k;
							}
							else
							{
								SubReportList detailScopeSubReports = eventSource.UserSort.DetailScopeSubReports;
								ReportItem reportItem;
								if (detailScopeSubReports != null && num < detailScopeSubReports.Count)
								{
									reportItem = detailScopeSubReports[num++].Parent;
								}
								else
								{
									Global.Tracer.Assert(k == eventSource.ContainingScopes.Count - 1, "(j == eventSource.ContainingScopes.Count - 1)");
									reportItem = eventSource.Parent;
								}
								while (reportItem != null && !(reportItem is DataRegion))
								{
									reportItem = reportItem.Parent;
								}
								Global.Tracer.Assert(reportItem is DataRegion, "(parent is DataRegion)");
								DataRegion dataRegion = (DataRegion)reportItem;
								if (dataRegion.SortFilterSourceDetailScopeInfo == null)
								{
									dataRegion.SortFilterSourceDetailScopeInfo = new int[count];
									for (int n = 0; n < count; n++)
									{
										dataRegion.SortFilterSourceDetailScopeInfo[n] = -1;
									}
								}
								Global.Tracer.Assert(variantList != null && 1 == variantList.Count, "(null != scopeValues && 1 == scopeValues.Count)");
								dataRegion.SortFilterSourceDetailScopeInfo[j] = (int)variantList[0];
							}
						}
					}
					GroupingList groupsInSortTarget = eventSource.UserSort.GroupsInSortTarget;
					if (groupsInSortTarget != null)
					{
						for (int num2 = 0; num2 < groupsInSortTarget.Count; num2++)
						{
							groupsInSortTarget[num2].NeedScopeInfoForSortFilterExpression = this.SetSortFilterInfo(groupsInSortTarget[num2].NeedScopeInfoForSortFilterExpression, count, j);
						}
					}
					IntList peerSortFilters = eventSource.GetPeerSortFilters(false);
					if (peerSortFilters != null)
					{
						if (this.m_runtimeSortFilterInfo[j].PeerSortFilters == null)
						{
							this.m_runtimeSortFilterInfo[j].PeerSortFilters = new Hashtable();
						}
						for (int num3 = 0; num3 < peerSortFilters.Count; num3++)
						{
							if (eventSource.ID != peerSortFilters[num3])
							{
								this.m_runtimeSortFilterInfo[j].PeerSortFilters.Add(peerSortFilters[num3], null);
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06006ACF RID: 27343 RVA: 0x001AF330 File Offset: 0x001AD530
		private bool[] SetSortFilterInfo(bool[] source, int count, int index)
		{
			bool[] array = source;
			if (array == null)
			{
				array = new bool[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = false;
				}
			}
			array[index] = true;
			return array;
		}

		// Token: 0x06006AD0 RID: 27344 RVA: 0x001AF360 File Offset: 0x001AD560
		internal bool IsSortFilterTarget(bool[] isSortFilterTarget, ReportProcessing.IScope outerScope, ReportProcessing.IHierarchyObj target, ref RuntimeUserSortTargetInfo userSortTargetInfo)
		{
			bool flag = false;
			if (this.m_runtimeSortFilterInfo != null && isSortFilterTarget != null && (outerScope == null || !outerScope.TargetForNonDetailSort))
			{
				for (int i = 0; i < this.m_runtimeSortFilterInfo.Count; i++)
				{
					RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = this.m_runtimeSortFilterInfo[i];
					if (runtimeSortFilterEventInfo.EventTarget == null && isSortFilterTarget[i] && (outerScope == null || outerScope.TargetScopeMatched(i, false)))
					{
						runtimeSortFilterEventInfo.EventTarget = target;
						if (userSortTargetInfo == null)
						{
							userSortTargetInfo = new RuntimeUserSortTargetInfo(target, i, runtimeSortFilterEventInfo);
						}
						else
						{
							userSortTargetInfo.AddSortInfo(target, i, runtimeSortFilterEventInfo);
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06006AD1 RID: 27345 RVA: 0x001AF3EC File Offset: 0x001AD5EC
		internal void RegisterSortFilterExpressionScope(ReportProcessing.IScope container, ReportProcessing.RuntimeDataRegionObj scopeObj, bool[] isSortFilterExpressionScope)
		{
			RuntimeSortFilterEventInfoList runtimeSortFilterInfo = this.m_runtimeSortFilterInfo;
			if (runtimeSortFilterInfo != null && isSortFilterExpressionScope != null && scopeObj != null)
			{
				VariantList[] array = null;
				for (int i = 0; i < runtimeSortFilterInfo.Count; i++)
				{
					if (isSortFilterExpressionScope[i] && scopeObj.IsTargetForSort(i, false) && scopeObj.TargetScopeMatched(i, false))
					{
						if (array == null && runtimeSortFilterInfo[i].EventSource.UserSort.GroupsInSortTarget != null)
						{
							int num = 0;
							array = new VariantList[runtimeSortFilterInfo[i].EventSource.UserSort.GroupsInSortTarget.Count];
							scopeObj.GetScopeValues(runtimeSortFilterInfo[i].EventTarget, array, ref num);
						}
						runtimeSortFilterInfo[i].RegisterSortFilterExpressionScope(ref container.SortFilterExpressionScopeInfoIndices[i], scopeObj, array, i);
					}
				}
			}
		}

		// Token: 0x06006AD2 RID: 27346 RVA: 0x001AF4B8 File Offset: 0x001AD6B8
		internal bool ProcessUserSort(ReportProcessing.ProcessingContext processingContext)
		{
			bool flag = false;
			RuntimeSortFilterEventInfoList runtimeSortFilterInfo = this.m_runtimeSortFilterInfo;
			if (runtimeSortFilterInfo != null)
			{
				bool flag2;
				bool flag3;
				do
				{
					flag2 = false;
					flag3 = true;
					this.ProcessUserSort(processingContext, ref flag2, ref flag3, ref flag);
				}
				while (flag2 && !flag3);
			}
			return flag;
		}

		// Token: 0x06006AD3 RID: 27347 RVA: 0x001AF4EC File Offset: 0x001AD6EC
		private void ProcessUserSort(ReportProcessing.ProcessingContext processingContext, ref bool processed, ref bool canStop, ref bool processedAny)
		{
			RuntimeSortFilterEventInfoList runtimeSortFilterInfo = this.m_runtimeSortFilterInfo;
			for (int i = 0; i < runtimeSortFilterInfo.Count; i++)
			{
				if (!runtimeSortFilterInfo[i].Processed)
				{
					runtimeSortFilterInfo[i].PrepareForSorting(processingContext);
				}
			}
			for (int j = 0; j < runtimeSortFilterInfo.Count; j++)
			{
				if (!runtimeSortFilterInfo[j].Processed)
				{
					if (runtimeSortFilterInfo[j].ProcessSorting(processingContext))
					{
						processedAny = true;
						processed = true;
					}
					else
					{
						canStop = false;
					}
				}
			}
		}

		// Token: 0x06006AD4 RID: 27348 RVA: 0x001AF568 File Offset: 0x001AD768
		internal void ProcessUserSortForTarget(ObjectModelImpl reportObjectModel, ReportRuntime reportRuntime, ReportProcessing.IHierarchyObj target, ref ReportProcessing.DataRowList dataRows, bool targetForNonDetailSort)
		{
			if (targetForNonDetailSort && dataRows != null && 0 < dataRows.Count)
			{
				RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = null;
				IntList sortFilterInfoIndices = target.SortFilterInfoIndices;
				Global.Tracer.Assert(target.SortTree != null, "(null != target.SortTree)");
				if (sortFilterInfoIndices != null)
				{
					runtimeSortFilterEventInfo = this.m_runtimeSortFilterInfo[sortFilterInfoIndices[0]];
				}
				for (int i = 0; i < dataRows.Count; i++)
				{
					reportObjectModel.FieldsImpl.SetFields(dataRows[i]);
					object obj = DBNull.Value;
					if (runtimeSortFilterEventInfo != null)
					{
						obj = runtimeSortFilterEventInfo.GetSortOrder(reportRuntime);
					}
					target.SortTree.NextRow(obj);
				}
				dataRows = null;
			}
			target.MarkSortInfoProcessed(this.m_runtimeSortFilterInfo);
		}

		// Token: 0x06006AD5 RID: 27349 RVA: 0x001AF61C File Offset: 0x001AD81C
		internal void EnterProcessUserSortPhase(int index)
		{
			if (this.m_inProcessUserSortPhase == null)
			{
				if (this.m_runtimeSortFilterInfo == null || this.m_runtimeSortFilterInfo.Count == 0)
				{
					return;
				}
				this.m_inProcessUserSortPhase = new int[this.m_runtimeSortFilterInfo.Count];
				for (int i = 0; i < this.m_runtimeSortFilterInfo.Count; i++)
				{
					this.m_inProcessUserSortPhase[i] = 0;
				}
			}
			this.m_inProcessUserSortPhase[index]++;
		}

		// Token: 0x06006AD6 RID: 27350 RVA: 0x001AF68D File Offset: 0x001AD88D
		internal void LeaveProcessUserSortPhase(int index)
		{
			if (this.m_inProcessUserSortPhase != null)
			{
				this.m_inProcessUserSortPhase[index]--;
				Global.Tracer.Assert(0 <= this.m_inProcessUserSortPhase[index], "(0 <= m_inProcessUserSortPhase[index])");
			}
		}

		// Token: 0x06006AD7 RID: 27351 RVA: 0x001AF6C5 File Offset: 0x001AD8C5
		internal bool InProcessUserSortPhase(int index)
		{
			return this.m_inProcessUserSortPhase != null && this.m_inProcessUserSortPhase[index] > 0;
		}

		// Token: 0x06006AD8 RID: 27352 RVA: 0x001AF6DC File Offset: 0x001AD8DC
		internal void UpdateContextFromDataSet(UserSortFilterContext dataSetContext)
		{
			if (-1 == this.m_dataSetID)
			{
				this.m_dataSetID = dataSetContext.DataSetID;
				this.m_runtimeSortFilterInfo = dataSetContext.RuntimeSortFilterInfo;
			}
		}

		// Token: 0x040035DD RID: 13789
		private TextBox m_currentSortFilterEventSource;

		// Token: 0x040035DE RID: 13790
		private RuntimeSortFilterEventInfoList m_runtimeSortFilterInfo;

		// Token: 0x040035DF RID: 13791
		private ReportProcessing.IScope m_currentContainingScope;

		// Token: 0x040035E0 RID: 13792
		private GroupingList m_containingScopes;

		// Token: 0x040035E1 RID: 13793
		private int m_dataSetID = -1;

		// Token: 0x040035E2 RID: 13794
		private SubReportList m_detailScopeSubReports;

		// Token: 0x040035E3 RID: 13795
		private int[] m_inProcessUserSortPhase;
	}
}
