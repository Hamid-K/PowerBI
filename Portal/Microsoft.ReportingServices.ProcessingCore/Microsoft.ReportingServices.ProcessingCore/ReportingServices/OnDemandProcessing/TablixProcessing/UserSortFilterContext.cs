using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008F7 RID: 2295
	internal sealed class UserSortFilterContext
	{
		// Token: 0x06007E61 RID: 32353 RVA: 0x00209670 File Offset: 0x00207870
		internal UserSortFilterContext()
		{
		}

		// Token: 0x06007E62 RID: 32354 RVA: 0x00209680 File Offset: 0x00207880
		internal UserSortFilterContext(UserSortFilterContext parentContext, Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport)
		{
			this.m_runtimeSortFilterInfo = parentContext.RuntimeSortFilterInfo;
			this.m_dataSetGlobalID = parentContext.DataSetGlobalId;
			this.m_inProcessUserSortPhase = parentContext.m_inProcessUserSortPhase;
			subReport.UpdateSubReportScopes(parentContext);
			this.m_containingScopes = subReport.ContainingScopes;
			this.m_detailScopeSubReports = subReport.DetailScopeSubReports;
		}

		// Token: 0x17002915 RID: 10517
		// (get) Token: 0x06007E63 RID: 32355 RVA: 0x002096DD File Offset: 0x002078DD
		// (set) Token: 0x06007E64 RID: 32356 RVA: 0x002096E5 File Offset: 0x002078E5
		internal IInScopeEventSource CurrentSortFilterEventSource
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

		// Token: 0x17002916 RID: 10518
		// (get) Token: 0x06007E65 RID: 32357 RVA: 0x002096EE File Offset: 0x002078EE
		internal List<IReference<RuntimeSortFilterEventInfo>> RuntimeSortFilterInfo
		{
			get
			{
				return this.m_runtimeSortFilterInfo;
			}
		}

		// Token: 0x17002917 RID: 10519
		// (get) Token: 0x06007E66 RID: 32358 RVA: 0x002096F6 File Offset: 0x002078F6
		// (set) Token: 0x06007E67 RID: 32359 RVA: 0x002096FE File Offset: 0x002078FE
		internal int DataSetGlobalId
		{
			get
			{
				return this.m_dataSetGlobalID;
			}
			set
			{
				this.m_dataSetGlobalID = value;
			}
		}

		// Token: 0x17002918 RID: 10520
		// (get) Token: 0x06007E68 RID: 32360 RVA: 0x00209707 File Offset: 0x00207907
		// (set) Token: 0x06007E69 RID: 32361 RVA: 0x0020970F File Offset: 0x0020790F
		internal IReference<IScope> CurrentContainingScope
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

		// Token: 0x17002919 RID: 10521
		// (get) Token: 0x06007E6A RID: 32362 RVA: 0x00209718 File Offset: 0x00207918
		// (set) Token: 0x06007E6B RID: 32363 RVA: 0x00209720 File Offset: 0x00207920
		internal Microsoft.ReportingServices.ReportIntermediateFormat.GroupingList ContainingScopes
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

		// Token: 0x1700291A RID: 10522
		// (get) Token: 0x06007E6C RID: 32364 RVA: 0x00209729 File Offset: 0x00207929
		// (set) Token: 0x06007E6D RID: 32365 RVA: 0x00209731 File Offset: 0x00207931
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport> DetailScopeSubReports
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

		// Token: 0x06007E6E RID: 32366 RVA: 0x0020973C File Offset: 0x0020793C
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport> CloneDetailScopeSubReports()
		{
			if (this.m_detailScopeSubReports == null)
			{
				return null;
			}
			int count = this.m_detailScopeSubReports.Count;
			List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport> list = new List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport>(count);
			for (int i = 0; i < count; i++)
			{
				list.Add(this.m_detailScopeSubReports[i]);
			}
			return list;
		}

		// Token: 0x06007E6F RID: 32367 RVA: 0x00209785 File Offset: 0x00207985
		internal void ResetContextForTopLevelDataSet()
		{
			this.m_dataSetGlobalID = -1;
			this.m_currentSortFilterEventSource = null;
			this.m_runtimeSortFilterInfo = null;
			this.m_currentContainingScope = null;
			this.m_containingScopes = null;
			this.m_inProcessUserSortPhase = null;
		}

		// Token: 0x06007E70 RID: 32368 RVA: 0x002097B1 File Offset: 0x002079B1
		internal void UpdateContextForFirstSubreportInstance(UserSortFilterContext parentContext)
		{
			if (-1 == this.m_dataSetGlobalID)
			{
				this.m_dataSetGlobalID = parentContext.DataSetGlobalId;
				this.m_runtimeSortFilterInfo = parentContext.RuntimeSortFilterInfo;
				this.m_inProcessUserSortPhase = parentContext.m_inProcessUserSortPhase;
			}
		}

		// Token: 0x06007E71 RID: 32369 RVA: 0x002097E0 File Offset: 0x002079E0
		internal static void ProcessEventSources(OnDemandProcessingContext odpContext, IScope containingScope, List<IInScopeEventSource> inScopeEventSources)
		{
			if (inScopeEventSources == null || inScopeEventSources.Count == 0)
			{
				return;
			}
			foreach (IInScopeEventSource inScopeEventSource in inScopeEventSources)
			{
				if (inScopeEventSource.UserSort != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.SortFilterEventInfo sortFilterEventInfo = new Microsoft.ReportingServices.ReportIntermediateFormat.SortFilterEventInfo(inScopeEventSource);
					sortFilterEventInfo.EventSourceScopeInfo = odpContext.GetScopeValues(inScopeEventSource.ContainingScopes, containingScope);
					if (odpContext.TopLevelContext.NewSortFilterEventInfo == null)
					{
						odpContext.TopLevelContext.NewSortFilterEventInfo = new SortFilterEventInfoMap();
					}
					string text = InstancePathItem.GenerateUniqueNameString(inScopeEventSource.ID, inScopeEventSource.InstancePath);
					odpContext.TopLevelContext.NewSortFilterEventInfo.Add(text, sortFilterEventInfo);
					List<IReference<RuntimeSortFilterEventInfo>> list = odpContext.RuntimeSortFilterInfo;
					if (list == null && odpContext.SubReportUniqueName == null)
					{
						list = odpContext.TopLevelContext.ReportRuntimeUserSortFilterInfo;
					}
					if (list != null)
					{
						for (int i = 0; i < list.Count; i++)
						{
							IReference<RuntimeSortFilterEventInfo> reference = list[i];
							using (reference.PinValue())
							{
								reference.Value().MatchEventSource(inScopeEventSource, text, containingScope, odpContext);
							}
						}
					}
				}
			}
		}

		// Token: 0x06007E72 RID: 32370 RVA: 0x00209918 File Offset: 0x00207B18
		internal bool PopulateRuntimeSortFilterEventInfo(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet myDataSet)
		{
			if (odpContext.TopLevelContext.UserSortFilterInfo == null || odpContext.TopLevelContext.UserSortFilterInfo.OdpSortInfo == null || odpContext.TopLevelContext.OldSortFilterEventInfo == null)
			{
				return false;
			}
			if (-1 != this.m_dataSetGlobalID)
			{
				return false;
			}
			this.m_runtimeSortFilterInfo = null;
			EventInformation.OdpSortEventInfo odpSortInfo = odpContext.TopLevelContext.UserSortFilterInfo.OdpSortInfo;
			for (int i = 0; i < odpSortInfo.Count; i++)
			{
				string uniqueNameAt = odpSortInfo.GetUniqueNameAt(i);
				Microsoft.ReportingServices.ReportIntermediateFormat.SortFilterEventInfo sortFilterEventInfo = odpContext.TopLevelContext.OldSortFilterEventInfo[uniqueNameAt];
				if (sortFilterEventInfo != null && sortFilterEventInfo.EventSource.UserSort != null)
				{
					int num = sortFilterEventInfo.EventSource.UserSort.SubReportDataSetGlobalId;
					if (-1 == num)
					{
						num = sortFilterEventInfo.EventSource.UserSort.DataSet.GlobalID;
					}
					if (num == myDataSet.GlobalID)
					{
						if (this.m_runtimeSortFilterInfo == null)
						{
							this.m_runtimeSortFilterInfo = new List<IReference<RuntimeSortFilterEventInfo>>();
						}
						RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = new RuntimeSortFilterEventInfo(sortFilterEventInfo.EventSource, uniqueNameAt, odpSortInfo.GetSortDirectionAt(i), sortFilterEventInfo.EventSourceScopeInfo, odpContext, (this.m_currentContainingScope != null) ? this.m_currentContainingScope.Value().Depth : 1);
						runtimeSortFilterEventInfo.SelfReference.UnPinValue();
						this.m_runtimeSortFilterInfo.Add(runtimeSortFilterEventInfo.SelfReference);
					}
				}
			}
			if (this.m_runtimeSortFilterInfo != null)
			{
				int count = this.m_runtimeSortFilterInfo.Count;
				for (int j = 0; j < count; j++)
				{
					IReference<RuntimeSortFilterEventInfo> reference = this.m_runtimeSortFilterInfo[j];
					using (reference.PinValue())
					{
						RuntimeSortFilterEventInfo runtimeSortFilterEventInfo2 = reference.Value();
						IInScopeEventSource eventSource = runtimeSortFilterEventInfo2.EventSource;
						Microsoft.ReportingServices.ReportIntermediateFormat.ISortFilterScope sortExpressionScope = eventSource.UserSort.SortExpressionScope;
						if (sortExpressionScope != null)
						{
							sortExpressionScope.IsSortFilterExpressionScope = this.SetSortFilterInfo(sortExpressionScope.IsSortFilterExpressionScope, count, j);
						}
						Microsoft.ReportingServices.ReportIntermediateFormat.ISortFilterScope sortTarget = eventSource.UserSort.SortTarget;
						if (sortTarget != null)
						{
							sortTarget.IsSortFilterTarget = this.SetSortFilterInfo(sortTarget.IsSortFilterTarget, count, j);
						}
						if (eventSource.ContainingScopes != null && 0 < eventSource.ContainingScopes.Count)
						{
							int num2 = 0;
							int num3 = 0;
							for (int k = 0; k < eventSource.ContainingScopes.Count; k++)
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = eventSource.ContainingScopes[k];
								if (grouping == null || !grouping.IsDetail)
								{
									List<object> list = runtimeSortFilterEventInfo2.SortSourceScopeInfo[num2];
									if (grouping != null)
									{
										if (grouping.SortFilterScopeInfo == null)
										{
											grouping.SortFilterScopeInfo = new List<object>[count];
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
										grouping.SortFilterScopeInfo[j] = list;
										grouping.SortFilterScopeIndex[j] = num2;
									}
									else
									{
										List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport> detailScopeSubReports = eventSource.UserSort.DetailScopeSubReports;
										Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem;
										if (detailScopeSubReports != null && num3 < detailScopeSubReports.Count)
										{
											reportItem = detailScopeSubReports[num3++].Parent;
										}
										else
										{
											reportItem = eventSource.Parent;
										}
										while (reportItem != null && !reportItem.IsDataRegion)
										{
											reportItem = reportItem.Parent;
										}
										Global.Tracer.Assert(reportItem.IsDataRegion, "(parent.IsDataRegion)");
										Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)reportItem;
										if (dataRegion.SortFilterSourceDetailScopeInfo == null)
										{
											dataRegion.SortFilterSourceDetailScopeInfo = new int[count];
											for (int n = 0; n < count; n++)
											{
												dataRegion.SortFilterSourceDetailScopeInfo[n] = -1;
											}
										}
										Global.Tracer.Assert(list != null && 1 == list.Count, "(null != scopeValues && 1 == scopeValues.Count)");
										dataRegion.SortFilterSourceDetailScopeInfo[j] = (int)list[0];
									}
									num2++;
								}
							}
						}
						Microsoft.ReportingServices.ReportIntermediateFormat.GroupingList groupsInSortTarget = eventSource.UserSort.GroupsInSortTarget;
						if (groupsInSortTarget != null)
						{
							for (int num4 = 0; num4 < groupsInSortTarget.Count; num4++)
							{
								groupsInSortTarget[num4].NeedScopeInfoForSortFilterExpression = this.SetSortFilterInfo(groupsInSortTarget[num4].NeedScopeInfoForSortFilterExpression, count, j);
							}
						}
						List<int> peerSortFilters = eventSource.GetPeerSortFilters(false);
						if (peerSortFilters != null && peerSortFilters.Count != 0)
						{
							if (runtimeSortFilterEventInfo2.PeerSortFilters == null)
							{
								runtimeSortFilterEventInfo2.PeerSortFilters = new Hashtable();
							}
							for (int num5 = 0; num5 < peerSortFilters.Count; num5++)
							{
								if (eventSource.ID != peerSortFilters[num5])
								{
									runtimeSortFilterEventInfo2.PeerSortFilters.Add(peerSortFilters[num5], null);
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06007E73 RID: 32371 RVA: 0x00209DC8 File Offset: 0x00207FC8
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

		// Token: 0x06007E74 RID: 32372 RVA: 0x00209DF8 File Offset: 0x00207FF8
		internal bool IsSortFilterTarget(bool[] isSortFilterTarget, IReference<IScope> outerScope, IReference<IHierarchyObj> target, ref RuntimeUserSortTargetInfo userSortTargetInfo)
		{
			bool flag = false;
			if (this.m_runtimeSortFilterInfo != null && isSortFilterTarget != null && (outerScope == null || !outerScope.Value().TargetForNonDetailSort))
			{
				for (int i = 0; i < this.m_runtimeSortFilterInfo.Count; i++)
				{
					IReference<RuntimeSortFilterEventInfo> reference = this.m_runtimeSortFilterInfo[i];
					using (reference.PinValue())
					{
						RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = reference.Value();
						if (isSortFilterTarget[i] && (outerScope == null || outerScope.Value().TargetScopeMatched(i, false)))
						{
							runtimeSortFilterEventInfo.EventTarget = target;
							runtimeSortFilterEventInfo.Processed = false;
							if (userSortTargetInfo == null)
							{
								userSortTargetInfo = new RuntimeUserSortTargetInfo(target, i, reference);
							}
							else
							{
								userSortTargetInfo.AddSortInfo(target, i, reference);
							}
							flag = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06007E75 RID: 32373 RVA: 0x00209EC0 File Offset: 0x002080C0
		internal void RegisterSortFilterExpressionScope(IReference<IScope> containerRef, IReference<RuntimeDataRegionObj> scopeRef, bool[] isSortFilterExpressionScope)
		{
			List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo = this.m_runtimeSortFilterInfo;
			if (runtimeSortFilterInfo != null && isSortFilterExpressionScope != null && scopeRef != null)
			{
				List<object>[] array = null;
				using (scopeRef.PinValue())
				{
					RuntimeDataRegionObj runtimeDataRegionObj = scopeRef.Value();
					using (containerRef.PinValue())
					{
						IScope scope = containerRef.Value();
						for (int i = 0; i < runtimeSortFilterInfo.Count; i++)
						{
							if (isSortFilterExpressionScope[i] && runtimeDataRegionObj.IsTargetForSort(i, false) && runtimeDataRegionObj.TargetScopeMatched(i, false))
							{
								IReference<RuntimeSortFilterEventInfo> reference = runtimeSortFilterInfo[i];
								using (reference.PinValue())
								{
									RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = reference.Value();
									if (array == null && runtimeSortFilterEventInfo.EventSource.UserSort.GroupsInSortTarget != null)
									{
										int num = 0;
										array = new List<object>[runtimeSortFilterEventInfo.EventSource.UserSort.GroupsInSortTarget.Count];
										runtimeDataRegionObj.GetScopeValues(runtimeSortFilterEventInfo.EventTarget, array, ref num);
									}
									runtimeSortFilterEventInfo.RegisterSortFilterExpressionScope(ref scope.SortFilterExpressionScopeInfoIndices[i], scopeRef, array, i);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06007E76 RID: 32374 RVA: 0x0020A010 File Offset: 0x00208210
		internal bool ProcessUserSort(OnDemandProcessingContext odpContext)
		{
			bool flag = false;
			bool flag2 = false;
			List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo = this.m_runtimeSortFilterInfo;
			if (runtimeSortFilterInfo != null)
			{
				bool flag3;
				bool flag4;
				do
				{
					flag3 = flag2;
					flag2 = false;
					flag4 = true;
					this.ProcessUserSort(odpContext, ref flag2, ref flag4, ref flag);
				}
				while (flag2 || (!flag4 && flag3));
			}
			return flag;
		}

		// Token: 0x06007E77 RID: 32375 RVA: 0x0020A04C File Offset: 0x0020824C
		private void ProcessUserSort(OnDemandProcessingContext odpContext, ref bool processed, ref bool canStop, ref bool processedAny)
		{
			List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo = this.m_runtimeSortFilterInfo;
			for (int i = 0; i < runtimeSortFilterInfo.Count; i++)
			{
				IReference<RuntimeSortFilterEventInfo> reference = runtimeSortFilterInfo[i];
				using (reference.PinValue())
				{
					RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = reference.Value();
					if (!runtimeSortFilterEventInfo.Processed)
					{
						runtimeSortFilterEventInfo.PrepareForSorting(odpContext);
					}
				}
			}
			for (int j = 0; j < runtimeSortFilterInfo.Count; j++)
			{
				IReference<RuntimeSortFilterEventInfo> reference2 = runtimeSortFilterInfo[j];
				using (reference2.PinValue())
				{
					RuntimeSortFilterEventInfo runtimeSortFilterEventInfo2 = reference2.Value();
					if (!runtimeSortFilterEventInfo2.Processed)
					{
						if (runtimeSortFilterEventInfo2.ProcessSorting(odpContext))
						{
							processedAny = true;
							processed = true;
							odpContext.FirstPassPostProcess();
							break;
						}
						canStop = false;
					}
				}
			}
		}

		// Token: 0x06007E78 RID: 32376 RVA: 0x0020A124 File Offset: 0x00208324
		internal void ProcessUserSortForTarget(ObjectModelImpl reportObjectModel, Microsoft.ReportingServices.RdlExpressions.ReportRuntime reportRuntime, IReference<IHierarchyObj> target, ref ScalableList<DataFieldRow> dataRows, bool targetForNonDetailSort)
		{
			using (target.PinValue())
			{
				IHierarchyObj hierarchyObj = target.Value();
				if (targetForNonDetailSort && dataRows != null && 0 < dataRows.Count)
				{
					IReference<RuntimeSortFilterEventInfo> reference = null;
					try
					{
						List<int> sortFilterInfoIndices = hierarchyObj.SortFilterInfoIndices;
						Global.Tracer.Assert(hierarchyObj.SortTree != null, "(null != targetObj.SortTree)");
						if (sortFilterInfoIndices != null)
						{
							reference = this.m_runtimeSortFilterInfo[sortFilterInfoIndices[0]];
						}
						RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = null;
						if (reference != null)
						{
							reference.PinValue();
							runtimeSortFilterEventInfo = reference.Value();
						}
						for (int i = 0; i < dataRows.Count; i++)
						{
							dataRows[i].SetFields(reportObjectModel.FieldsImpl);
							object obj = DBNull.Value;
							if (runtimeSortFilterEventInfo != null)
							{
								obj = runtimeSortFilterEventInfo.GetSortOrder(reportRuntime);
							}
							hierarchyObj.SortTree.NextRow(obj, hierarchyObj);
						}
					}
					finally
					{
						if (reference != null)
						{
							reference.UnPinValue();
						}
					}
					if (dataRows != null)
					{
						dataRows.Dispose();
					}
					dataRows = null;
				}
				hierarchyObj.MarkSortInfoProcessed(this.m_runtimeSortFilterInfo);
			}
		}

		// Token: 0x06007E79 RID: 32377 RVA: 0x0020A248 File Offset: 0x00208448
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

		// Token: 0x06007E7A RID: 32378 RVA: 0x0020A2B9 File Offset: 0x002084B9
		internal void LeaveProcessUserSortPhase(int index)
		{
			if (this.m_inProcessUserSortPhase != null)
			{
				this.m_inProcessUserSortPhase[index]--;
				Global.Tracer.Assert(0 <= this.m_inProcessUserSortPhase[index], "(0 <= m_inProcessUserSortPhase[index])");
			}
		}

		// Token: 0x06007E7B RID: 32379 RVA: 0x0020A2F1 File Offset: 0x002084F1
		internal bool InProcessUserSortPhase(int index)
		{
			return this.m_inProcessUserSortPhase != null && this.m_inProcessUserSortPhase[index] > 0;
		}

		// Token: 0x04003E24 RID: 15908
		private IInScopeEventSource m_currentSortFilterEventSource;

		// Token: 0x04003E25 RID: 15909
		private List<IReference<RuntimeSortFilterEventInfo>> m_runtimeSortFilterInfo;

		// Token: 0x04003E26 RID: 15910
		private IReference<IScope> m_currentContainingScope;

		// Token: 0x04003E27 RID: 15911
		private Microsoft.ReportingServices.ReportIntermediateFormat.GroupingList m_containingScopes;

		// Token: 0x04003E28 RID: 15912
		private int m_dataSetGlobalID = -1;

		// Token: 0x04003E29 RID: 15913
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.SubReport> m_detailScopeSubReports;

		// Token: 0x04003E2A RID: 15914
		private int[] m_inProcessUserSortPhase;
	}
}
