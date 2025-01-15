using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008F9 RID: 2297
	[PersistedWithinRequestOnly]
	public sealed class RuntimeSortFilterEventInfo : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ISelfReferential
	{
		// Token: 0x06007E94 RID: 32404 RVA: 0x0020A89A File Offset: 0x00208A9A
		internal RuntimeSortFilterEventInfo()
		{
		}

		// Token: 0x06007E95 RID: 32405 RVA: 0x0020A8B8 File Offset: 0x00208AB8
		internal RuntimeSortFilterEventInfo(IInScopeEventSource eventSource, string oldUniqueName, bool sortDirection, List<object>[] sortSourceScopeInfo, OnDemandProcessingContext odpContext, int depth)
		{
			this.m_depth = depth;
			odpContext.TablixProcessingScalabilityCache.AllocateAndPin<RuntimeSortFilterEventInfo>(this, this.m_depth);
			this.m_eventSource = eventSource;
			this.m_oldUniqueName = oldUniqueName;
			this.m_sortDirection = sortDirection;
			this.m_sortSourceScopeInfo = sortSourceScopeInfo;
		}

		// Token: 0x17002920 RID: 10528
		// (get) Token: 0x06007E96 RID: 32406 RVA: 0x0020A919 File Offset: 0x00208B19
		internal IInScopeEventSource EventSource
		{
			get
			{
				return this.m_eventSource;
			}
		}

		// Token: 0x06007E97 RID: 32407 RVA: 0x0020A921 File Offset: 0x00208B21
		internal IReference<IScope> GetEventSourceScope(bool isColumnAxis)
		{
			if (!isColumnAxis)
			{
				return this.m_eventSourceRowScope;
			}
			return this.m_eventSourceColScope;
		}

		// Token: 0x06007E98 RID: 32408 RVA: 0x0020A934 File Offset: 0x00208B34
		internal void SetEventSourceScope(bool isColumnAxis, IReference<IScope> eventSourceScope, int rowIndex)
		{
			if (isColumnAxis)
			{
				Global.Tracer.Assert(this.m_eventSourceColScope == null);
				this.m_eventSourceColScope = eventSourceScope;
				this.m_eventSourceColDetailIndex = rowIndex;
				return;
			}
			Global.Tracer.Assert(this.m_eventSourceRowScope == null);
			this.m_eventSourceRowScope = eventSourceScope;
			this.m_eventSourceRowDetailIndex = rowIndex;
		}

		// Token: 0x06007E99 RID: 32409 RVA: 0x0020A987 File Offset: 0x00208B87
		internal void UpdateEventSourceScope(bool isColumnAxis, IReference<IScope> eventSourceScope, int rootRowCount)
		{
			if (isColumnAxis)
			{
				this.m_eventSourceColScope = eventSourceScope;
				this.m_eventSourceColDetailIndex += rootRowCount;
				return;
			}
			this.m_eventSourceRowScope = eventSourceScope;
			this.m_eventSourceRowDetailIndex += rootRowCount;
		}

		// Token: 0x06007E9A RID: 32410 RVA: 0x0020A9B8 File Offset: 0x00208BB8
		internal void AddDetailScopeInfo(bool isColumnAxis, RuntimeDataRegionObjReference dataRegionReference, int detailRowIndex)
		{
			if (this.m_detailRowScopes == null)
			{
				this.m_detailRowScopes = new List<IReference<RuntimeDataRegionObj>>();
				this.m_detailRowScopeIndices = new List<int>();
				this.m_detailColScopes = new List<IReference<RuntimeDataRegionObj>>();
				this.m_detailColScopeIndices = new List<int>();
			}
			if (isColumnAxis)
			{
				this.m_detailColScopes.Add(dataRegionReference);
				this.m_detailColScopeIndices.Add(detailRowIndex);
				return;
			}
			this.m_detailRowScopes.Add(dataRegionReference);
			this.m_detailRowScopeIndices.Add(detailRowIndex);
		}

		// Token: 0x06007E9B RID: 32411 RVA: 0x0020AA30 File Offset: 0x00208C30
		internal void UpdateDetailScopeInfo(RuntimeGroupRootObj detailRoot, bool isColumnAxis, int rootRowCount, RuntimeDataRegionObjReference selfReference)
		{
			List<IReference<RuntimeDataRegionObj>> list;
			List<int> list2;
			if (isColumnAxis)
			{
				list = this.m_detailColScopes;
				list2 = this.m_detailColScopeIndices;
			}
			else
			{
				list = this.m_detailRowScopes;
				list2 = this.m_detailRowScopeIndices;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (selfReference == list[i])
				{
					list[i] = detailRoot.SelfReference;
					List<int> list3 = list2;
					int num = i;
					list3[num] += rootRowCount;
				}
			}
		}

		// Token: 0x17002921 RID: 10529
		// (get) Token: 0x06007E9C RID: 32412 RVA: 0x0020AAA3 File Offset: 0x00208CA3
		internal bool HasEventSourceScope
		{
			get
			{
				return this.m_eventSourceColScope != null || this.m_eventSourceRowScope != null;
			}
		}

		// Token: 0x17002922 RID: 10530
		// (get) Token: 0x06007E9D RID: 32413 RVA: 0x0020AAB8 File Offset: 0x00208CB8
		internal bool HasDetailScopeInfo
		{
			get
			{
				return this.m_detailColScopes != null || this.m_detailRowScopes != null;
			}
		}

		// Token: 0x17002923 RID: 10531
		// (get) Token: 0x06007E9E RID: 32414 RVA: 0x0020AACD File Offset: 0x00208CCD
		// (set) Token: 0x06007E9F RID: 32415 RVA: 0x0020AAD5 File Offset: 0x00208CD5
		internal int EventSourceColDetailIndex
		{
			get
			{
				return this.m_eventSourceColDetailIndex;
			}
			set
			{
				this.m_eventSourceColDetailIndex = value;
			}
		}

		// Token: 0x17002924 RID: 10532
		// (get) Token: 0x06007EA0 RID: 32416 RVA: 0x0020AADE File Offset: 0x00208CDE
		// (set) Token: 0x06007EA1 RID: 32417 RVA: 0x0020AAE6 File Offset: 0x00208CE6
		internal int EventSourceRowDetailIndex
		{
			get
			{
				return this.m_eventSourceRowDetailIndex;
			}
			set
			{
				this.m_eventSourceRowDetailIndex = value;
			}
		}

		// Token: 0x17002925 RID: 10533
		// (get) Token: 0x06007EA2 RID: 32418 RVA: 0x0020AAEF File Offset: 0x00208CEF
		// (set) Token: 0x06007EA3 RID: 32419 RVA: 0x0020AAF7 File Offset: 0x00208CF7
		internal List<IReference<RuntimeDataRegionObj>> DetailRowScopes
		{
			get
			{
				return this.m_detailRowScopes;
			}
			set
			{
				this.m_detailRowScopes = value;
			}
		}

		// Token: 0x17002926 RID: 10534
		// (get) Token: 0x06007EA4 RID: 32420 RVA: 0x0020AB00 File Offset: 0x00208D00
		// (set) Token: 0x06007EA5 RID: 32421 RVA: 0x0020AB08 File Offset: 0x00208D08
		internal List<IReference<RuntimeDataRegionObj>> DetailColScopes
		{
			get
			{
				return this.m_detailColScopes;
			}
			set
			{
				this.m_detailColScopes = value;
			}
		}

		// Token: 0x17002927 RID: 10535
		// (get) Token: 0x06007EA6 RID: 32422 RVA: 0x0020AB11 File Offset: 0x00208D11
		// (set) Token: 0x06007EA7 RID: 32423 RVA: 0x0020AB19 File Offset: 0x00208D19
		internal List<int> DetailRowScopeIndices
		{
			get
			{
				return this.m_detailRowScopeIndices;
			}
			set
			{
				this.m_detailRowScopeIndices = value;
			}
		}

		// Token: 0x17002928 RID: 10536
		// (get) Token: 0x06007EA8 RID: 32424 RVA: 0x0020AB22 File Offset: 0x00208D22
		// (set) Token: 0x06007EA9 RID: 32425 RVA: 0x0020AB2A File Offset: 0x00208D2A
		internal List<int> DetailColScopeIndices
		{
			get
			{
				return this.m_detailColScopeIndices;
			}
			set
			{
				this.m_detailColScopeIndices = value;
			}
		}

		// Token: 0x17002929 RID: 10537
		// (get) Token: 0x06007EAA RID: 32426 RVA: 0x0020AB33 File Offset: 0x00208D33
		// (set) Token: 0x06007EAB RID: 32427 RVA: 0x0020AB3B File Offset: 0x00208D3B
		internal bool SortDirection
		{
			get
			{
				return this.m_sortDirection;
			}
			set
			{
				this.m_sortDirection = value;
			}
		}

		// Token: 0x1700292A RID: 10538
		// (get) Token: 0x06007EAC RID: 32428 RVA: 0x0020AB44 File Offset: 0x00208D44
		internal List<object>[] SortSourceScopeInfo
		{
			get
			{
				return this.m_sortSourceScopeInfo;
			}
		}

		// Token: 0x1700292B RID: 10539
		// (get) Token: 0x06007EAD RID: 32429 RVA: 0x0020AB4C File Offset: 0x00208D4C
		// (set) Token: 0x06007EAE RID: 32430 RVA: 0x0020AB54 File Offset: 0x00208D54
		internal IReference<IHierarchyObj> EventTarget
		{
			get
			{
				return this.m_eventTarget;
			}
			set
			{
				this.m_eventTarget = value;
			}
		}

		// Token: 0x1700292C RID: 10540
		// (get) Token: 0x06007EAF RID: 32431 RVA: 0x0020AB5D File Offset: 0x00208D5D
		// (set) Token: 0x06007EB0 RID: 32432 RVA: 0x0020AB65 File Offset: 0x00208D65
		internal bool TargetSortFilterInfoAdded
		{
			get
			{
				return this.m_targetSortFilterInfoAdded;
			}
			set
			{
				this.m_targetSortFilterInfoAdded = value;
			}
		}

		// Token: 0x1700292D RID: 10541
		// (get) Token: 0x06007EB1 RID: 32433 RVA: 0x0020AB6E File Offset: 0x00208D6E
		// (set) Token: 0x06007EB2 RID: 32434 RVA: 0x0020AB76 File Offset: 0x00208D76
		internal bool Processed
		{
			get
			{
				return this.m_processed;
			}
			set
			{
				this.m_processed = value;
			}
		}

		// Token: 0x1700292E RID: 10542
		// (get) Token: 0x06007EB3 RID: 32435 RVA: 0x0020AB7F File Offset: 0x00208D7F
		internal string OldUniqueName
		{
			get
			{
				return this.m_oldUniqueName;
			}
		}

		// Token: 0x1700292F RID: 10543
		// (get) Token: 0x06007EB4 RID: 32436 RVA: 0x0020AB87 File Offset: 0x00208D87
		// (set) Token: 0x06007EB5 RID: 32437 RVA: 0x0020AB8F File Offset: 0x00208D8F
		internal string NewUniqueName
		{
			get
			{
				return this.m_newUniqueName;
			}
			set
			{
				this.m_newUniqueName = value;
			}
		}

		// Token: 0x17002930 RID: 10544
		// (get) Token: 0x06007EB6 RID: 32438 RVA: 0x0020AB98 File Offset: 0x00208D98
		// (set) Token: 0x06007EB7 RID: 32439 RVA: 0x0020ABA0 File Offset: 0x00208DA0
		internal Hashtable PeerSortFilters
		{
			get
			{
				return this.m_peerSortFilters;
			}
			set
			{
				this.m_peerSortFilters = value;
			}
		}

		// Token: 0x06007EB8 RID: 32440 RVA: 0x0020ABAC File Offset: 0x00208DAC
		internal void RegisterSortFilterExpressionScope(ref int containerSortFilterExprScopeIndex, IReference<RuntimeDataRegionObj> scopeObj, List<object>[] scopeValues, int sortFilterInfoIndex)
		{
			if (this.m_eventTarget != null && !this.m_targetSortFilterInfoAdded)
			{
				using (this.m_eventTarget.PinValue())
				{
					this.m_eventTarget.Value().AddSortInfoIndex(sortFilterInfoIndex, this.SelfReference);
				}
			}
			RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj sortFilterExpressionScopeObj;
			if (-1 != containerSortFilterExprScopeIndex)
			{
				sortFilterExpressionScopeObj = this.m_sortFilterExpressionScopeObjects[containerSortFilterExprScopeIndex];
			}
			else
			{
				if (this.m_sortFilterExpressionScopeObjects == null)
				{
					this.m_sortFilterExpressionScopeObjects = new List<RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj>();
				}
				containerSortFilterExprScopeIndex = this.m_sortFilterExpressionScopeObjects.Count;
				sortFilterExpressionScopeObj = new RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj(this.m_selfReference, scopeObj.Value().OdpContext, this.m_depth + 1);
				this.m_sortFilterExpressionScopeObjects.Add(sortFilterExpressionScopeObj);
			}
			sortFilterExpressionScopeObj.RegisterScopeInstance(scopeObj, scopeValues);
		}

		// Token: 0x06007EB9 RID: 32441 RVA: 0x0020AC74 File Offset: 0x00208E74
		internal void PrepareForSorting(OnDemandProcessingContext odpContext)
		{
			Global.Tracer.Assert(!this.m_processed, "(!m_processed)");
			if (this.m_eventTarget != null && this.m_sortFilterExpressionScopeObjects != null)
			{
				odpContext.UserSortFilterContext.CurrentSortFilterEventSource = this.m_eventSource;
				for (int i = 0; i < this.m_sortFilterExpressionScopeObjects.Count; i++)
				{
					this.m_sortFilterExpressionScopeObjects[i].SortSEScopes(odpContext, this.m_eventSource);
				}
				Microsoft.ReportingServices.ReportIntermediateFormat.GroupingList groupsInSortTarget = this.m_eventSource.UserSort.GroupsInSortTarget;
				if (groupsInSortTarget != null && 0 < groupsInSortTarget.Count)
				{
					this.m_groupExpressionsInSortTarget = new List<RuntimeExpressionInfo>();
					for (int j = 0; j < groupsInSortTarget.Count; j++)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = groupsInSortTarget[j];
						for (int k = 0; k < grouping.GroupExpressions.Count; k++)
						{
							this.m_groupExpressionsInSortTarget.Add(new RuntimeExpressionInfo(grouping.GroupExpressions, grouping.ExprHost, null, k));
						}
					}
				}
				this.CollectSortOrders();
			}
		}

		// Token: 0x06007EBA RID: 32442 RVA: 0x0020AD70 File Offset: 0x00208F70
		private void CollectSortOrders()
		{
			this.m_currentSortIndex = 1;
			UserSortFilterTraversalContext userSortFilterTraversalContext = new UserSortFilterTraversalContext(this);
			for (int i = 0; i < this.m_sortFilterExpressionScopeObjects.Count; i++)
			{
				this.m_sortFilterExpressionScopeObjects[i].Traverse(ProcessingStages.UserSortFilter, userSortFilterTraversalContext);
			}
			this.m_sortFilterExpressionScopeObjects = null;
		}

		// Token: 0x06007EBB RID: 32443 RVA: 0x0020ADBC File Offset: 0x00208FBC
		internal bool ProcessSorting(OnDemandProcessingContext odpContext)
		{
			Global.Tracer.Assert(!this.m_processed, "(!m_processed)");
			if (this.m_eventTarget == null)
			{
				return false;
			}
			using (this.m_eventTarget.PinValue())
			{
				this.m_eventTarget.Value().ProcessUserSort();
			}
			this.m_sortOrders = null;
			return true;
		}

		// Token: 0x06007EBC RID: 32444 RVA: 0x0020AE2C File Offset: 0x0020902C
		private void AddSortOrder(List<object>[] scopeValues, bool incrementCounter)
		{
			if (this.m_sortOrders == null)
			{
				this.m_sortOrders = new Microsoft.ReportingServices.ReportIntermediateFormat.ScopeLookupTable();
			}
			if (scopeValues == null || scopeValues.Length == 0)
			{
				this.m_sortOrders.Add(this.m_eventSource.UserSort.GroupsInSortTarget, scopeValues, this.m_currentSortIndex);
			}
			else
			{
				int num = 0;
				for (int i = 0; i < scopeValues.Length; i++)
				{
					if (scopeValues[i] == null)
					{
						num++;
					}
				}
				if (num >= this.m_nullScopeCount)
				{
					if (num > this.m_nullScopeCount)
					{
						this.m_sortOrders.Clear();
						this.m_nullScopeCount = num;
					}
					this.m_sortOrders.Add(this.m_eventSource.UserSort.GroupsInSortTarget, scopeValues, this.m_currentSortIndex);
				}
			}
			if (incrementCounter)
			{
				this.m_currentSortIndex++;
			}
		}

		// Token: 0x06007EBD RID: 32445 RVA: 0x0020AEE8 File Offset: 0x002090E8
		internal object GetSortOrder(Microsoft.ReportingServices.RdlExpressions.ReportRuntime runtime)
		{
			object obj = null;
			if (this.m_eventSource.UserSort.SortExpressionScope == null)
			{
				obj = runtime.EvaluateUserSortExpression(this.m_eventSource);
			}
			else if (this.m_sortOrders == null)
			{
				obj = null;
			}
			else
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.GroupingList groupsInSortTarget = this.m_eventSource.UserSort.GroupsInSortTarget;
				if (groupsInSortTarget == null || groupsInSortTarget.Count == 0)
				{
					obj = this.m_sortOrders.LookupTable;
				}
				else
				{
					bool flag = true;
					bool flag2 = false;
					int num = 0;
					Hashtable hashtable = this.m_sortOrders.LookupTable;
					int i = 0;
					while (i < groupsInSortTarget.Count)
					{
						IEnumerator enumerator = hashtable.Keys.GetEnumerator();
						enumerator.MoveNext();
						int num2 = (int)enumerator.Current;
						for (int j = 0; j < num2; j++)
						{
							num += groupsInSortTarget[i++].GroupExpressions.Count;
						}
						hashtable = (Hashtable)hashtable[num2];
						if (i >= groupsInSortTarget.Count)
						{
							flag2 = true;
							break;
						}
						Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = groupsInSortTarget[i];
						for (int k = 0; k < grouping.GroupExpressions.Count; k++)
						{
							object obj2 = runtime.EvaluateRuntimeExpression(this.m_groupExpressionsInSortTarget[num], Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping, grouping.Name, "GroupExpression");
							num++;
							obj = hashtable[obj2];
							if (obj == null)
							{
								flag = false;
								break;
							}
							if (num < this.m_groupExpressionsInSortTarget.Count)
							{
								hashtable = (Hashtable)obj;
							}
						}
						i++;
						if (!flag)
						{
							break;
						}
					}
					if (flag && flag2)
					{
						obj = hashtable[1];
						if (obj == null)
						{
							flag = false;
						}
					}
					if (flag)
					{
						this.m_currentInstanceIndex = this.m_currentSortIndex + 1;
					}
					else
					{
						obj = this.m_currentInstanceIndex;
					}
				}
			}
			if (obj == null)
			{
				obj = DBNull.Value;
			}
			return obj;
		}

		// Token: 0x06007EBE RID: 32446 RVA: 0x0020B0B8 File Offset: 0x002092B8
		internal void MatchEventSource(IInScopeEventSource eventSource, string eventSourceUniqueNameString, IScope containingScope, OnDemandProcessingContext odpContext)
		{
			bool flag = false;
			if (!(containingScope is RuntimeCell))
			{
				while (containingScope != null && !(containingScope is RuntimeGroupLeafObj) && !(containingScope is RuntimeDetailObj))
				{
					IReference<IScope> outerScope = containingScope.GetOuterScope(true);
					if (outerScope == null)
					{
						containingScope = null;
					}
					else
					{
						containingScope = outerScope.Value();
					}
				}
			}
			if (containingScope == null)
			{
				if (this.m_eventSource.ContainingScopes == null || this.m_eventSource.ContainingScopes.Count == 0)
				{
					flag = true;
				}
			}
			else if ((this.m_eventSourceRowScope != null && this.m_eventSourceRowScope.Value() == containingScope) || (this.m_eventSourceColScope != null && this.m_eventSourceColScope.Value() == containingScope))
			{
				flag = true;
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = null;
				bool flag2 = false;
				RuntimeGroupLeafObj runtimeGroupLeafObj = containingScope as RuntimeGroupLeafObj;
				if (runtimeGroupLeafObj != null && runtimeGroupLeafObj.MemberDef.Grouping.IsDetail)
				{
					dataRegion = runtimeGroupLeafObj.MemberDef.DataRegionDef;
					flag2 = runtimeGroupLeafObj.MemberDef.IsColumn;
				}
				if (dataRegion != null)
				{
					if (flag2 && dataRegion.CurrentColDetailIndex != this.m_eventSourceColDetailIndex)
					{
						flag = false;
					}
					else if (!flag2 && dataRegion.CurrentRowDetailIndex != this.m_eventSourceRowDetailIndex)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				if (eventSource == this.m_eventSource)
				{
					this.m_newUniqueName = eventSourceUniqueNameString;
					return;
				}
				if (this.m_peerSortFilters != null && this.m_peerSortFilters.Contains(eventSource.ID))
				{
					this.m_peerSortFilters[eventSource.ID] = eventSourceUniqueNameString;
				}
			}
		}

		// Token: 0x06007EBF RID: 32447 RVA: 0x0020B20C File Offset: 0x0020940C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeSortFilterEventInfo.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.PeerSortFilters)
				{
					if (memberName == MemberName.EventSource)
					{
						int num = scalabilityCache.StoreStaticReference(this.m_eventSource);
						writer.Write(num);
						continue;
					}
					switch (memberName)
					{
					case MemberName.OldUniqueName:
						writer.Write(this.m_oldUniqueName);
						continue;
					case MemberName.SortSourceScopeInfo:
						writer.WriteArrayOfListsOfPrimitives<object>(this.m_sortSourceScopeInfo);
						continue;
					case MemberName.SortDirection:
						writer.Write(this.m_sortDirection);
						continue;
					case MemberName.EventSourceRowScope:
						writer.Write(this.m_eventSourceRowScope);
						continue;
					case MemberName.EventSourceColDetailIndex:
						writer.Write(this.m_eventSourceColDetailIndex);
						continue;
					case MemberName.EventSourceRowDetailIndex:
						writer.Write(this.m_eventSourceRowDetailIndex);
						continue;
					case MemberName.DetailRowScopes:
						writer.Write<IReference<RuntimeDataRegionObj>>(this.m_detailRowScopes);
						continue;
					case MemberName.DetailRowScopeIndices:
						writer.WriteListOfPrimitives<int>(this.m_detailRowScopeIndices);
						continue;
					case MemberName.DetailColScopeIndices:
						writer.WriteListOfPrimitives<int>(this.m_detailColScopeIndices);
						continue;
					case MemberName.EventTarget:
						writer.Write(this.m_eventTarget);
						continue;
					case MemberName.TargetSortFilterInfoAdded:
						writer.Write(this.m_targetSortFilterInfoAdded);
						continue;
					case MemberName.GroupExpressionsInSortTarget:
						writer.Write<RuntimeExpressionInfo>(this.m_groupExpressionsInSortTarget);
						continue;
					case MemberName.SortFilterExpressionScopeObjects:
						writer.Write<RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj>(this.m_sortFilterExpressionScopeObjects);
						continue;
					case MemberName.CurrentSortIndex:
						writer.Write(this.m_currentSortIndex);
						continue;
					case MemberName.CurrentInstanceIndex:
						writer.Write(this.m_currentInstanceIndex);
						continue;
					case MemberName.SortOrders:
						writer.Write(this.m_sortOrders);
						continue;
					case MemberName.Processed:
						writer.Write(this.m_processed);
						continue;
					case MemberName.NullScopeCount:
						writer.Write(this.m_nullScopeCount);
						continue;
					case MemberName.NewUniqueName:
						writer.Write(this.m_newUniqueName);
						continue;
					case MemberName.PeerSortFilters:
						writer.WriteInt32StringHashtable(this.m_peerSortFilters);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Depth)
					{
						writer.Write(this.m_depth);
						continue;
					}
					if (memberName == MemberName.EventSourceColScope)
					{
						writer.Write(this.m_eventSourceColScope);
						continue;
					}
					if (memberName == MemberName.DetailColScopes)
					{
						writer.Write<IReference<RuntimeDataRegionObj>>(this.m_detailColScopes);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007EC0 RID: 32448 RVA: 0x0020B494 File Offset: 0x00209694
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeSortFilterEventInfo.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.PeerSortFilters)
				{
					if (memberName == MemberName.EventSource)
					{
						int num = reader.ReadInt32();
						this.m_eventSource = (IInScopeEventSource)scalabilityCache.FetchStaticReference(num);
						continue;
					}
					switch (memberName)
					{
					case MemberName.OldUniqueName:
						this.m_oldUniqueName = reader.ReadString();
						continue;
					case MemberName.SortSourceScopeInfo:
						this.m_sortSourceScopeInfo = reader.ReadArrayOfListsOfPrimitives<object>();
						continue;
					case MemberName.SortDirection:
						this.m_sortDirection = reader.ReadBoolean();
						continue;
					case MemberName.EventSourceRowScope:
						this.m_eventSourceRowScope = (IReference<IScope>)reader.ReadRIFObject();
						continue;
					case MemberName.EventSourceColDetailIndex:
						this.m_eventSourceColDetailIndex = reader.ReadInt32();
						continue;
					case MemberName.EventSourceRowDetailIndex:
						this.m_eventSourceRowDetailIndex = reader.ReadInt32();
						continue;
					case MemberName.DetailRowScopes:
						this.m_detailRowScopes = reader.ReadListOfRIFObjects<List<IReference<RuntimeDataRegionObj>>>();
						continue;
					case MemberName.DetailRowScopeIndices:
						this.m_detailRowScopeIndices = reader.ReadListOfPrimitives<int>();
						continue;
					case MemberName.DetailColScopeIndices:
						this.m_detailColScopeIndices = reader.ReadListOfPrimitives<int>();
						continue;
					case MemberName.EventTarget:
						this.m_eventTarget = (IReference<IHierarchyObj>)reader.ReadRIFObject();
						continue;
					case MemberName.TargetSortFilterInfoAdded:
						this.m_targetSortFilterInfoAdded = reader.ReadBoolean();
						continue;
					case MemberName.GroupExpressionsInSortTarget:
						this.m_groupExpressionsInSortTarget = reader.ReadListOfRIFObjects<List<RuntimeExpressionInfo>>();
						continue;
					case MemberName.SortFilterExpressionScopeObjects:
						this.m_sortFilterExpressionScopeObjects = reader.ReadListOfRIFObjects<List<RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj>>();
						continue;
					case MemberName.CurrentSortIndex:
						this.m_currentSortIndex = reader.ReadInt32();
						continue;
					case MemberName.CurrentInstanceIndex:
						this.m_currentInstanceIndex = reader.ReadInt32();
						continue;
					case MemberName.SortOrders:
						this.m_sortOrders = (Microsoft.ReportingServices.ReportIntermediateFormat.ScopeLookupTable)reader.ReadRIFObject();
						continue;
					case MemberName.Processed:
						this.m_processed = reader.ReadBoolean();
						continue;
					case MemberName.NullScopeCount:
						this.m_nullScopeCount = reader.ReadInt32();
						continue;
					case MemberName.NewUniqueName:
						this.m_newUniqueName = reader.ReadString();
						continue;
					case MemberName.PeerSortFilters:
						this.m_peerSortFilters = reader.ReadInt32StringHashtable();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Depth)
					{
						this.m_depth = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.EventSourceColScope)
					{
						this.m_eventSourceColScope = (IReference<IScope>)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.DetailColScopes)
					{
						this.m_detailColScopes = reader.ReadListOfRIFObjects<List<IReference<RuntimeDataRegionObj>>>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007EC1 RID: 32449 RVA: 0x0020B733 File Offset: 0x00209933
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007EC2 RID: 32450 RVA: 0x0020B735 File Offset: 0x00209935
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortFilterEventInfo;
		}

		// Token: 0x06007EC3 RID: 32451 RVA: 0x0020B73C File Offset: 0x0020993C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeSortFilterEventInfo.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortFilterEventInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.EventSource, Token.Int32),
					new MemberInfo(MemberName.OldUniqueName, Token.String),
					new MemberInfo(MemberName.SortSourceScopeInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList),
					new MemberInfo(MemberName.SortDirection, Token.Boolean),
					new MemberInfo(MemberName.EventSourceRowScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IScopeReference),
					new MemberInfo(MemberName.EventSourceColScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IScopeReference),
					new MemberInfo(MemberName.EventSourceRowDetailIndex, Token.Int32),
					new MemberInfo(MemberName.EventSourceColDetailIndex, Token.Int32),
					new MemberInfo(MemberName.DetailRowScopes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObjReference),
					new MemberInfo(MemberName.DetailColScopes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObjReference),
					new MemberInfo(MemberName.DetailRowScopeIndices, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32),
					new MemberInfo(MemberName.DetailColScopeIndices, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32),
					new MemberInfo(MemberName.EventTarget, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IHierarchyObjReference),
					new MemberInfo(MemberName.TargetSortFilterInfoAdded, Token.Boolean),
					new MemberInfo(MemberName.GroupExpressionsInSortTarget, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeExpressionInfo),
					new MemberInfo(MemberName.SortFilterExpressionScopeObjects, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterExpressionScopeObj),
					new MemberInfo(MemberName.CurrentSortIndex, Token.Int32),
					new MemberInfo(MemberName.CurrentInstanceIndex, Token.Int32),
					new MemberInfo(MemberName.SortOrders, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeLookupTable),
					new MemberInfo(MemberName.Processed, Token.Boolean),
					new MemberInfo(MemberName.NullScopeCount, Token.Int32),
					new MemberInfo(MemberName.NewUniqueName, Token.String),
					new MemberInfo(MemberName.PeerSortFilters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Int32StringHashtable),
					new MemberInfo(MemberName.Depth, Token.Int32)
				});
			}
			return RuntimeSortFilterEventInfo.m_declaration;
		}

		// Token: 0x06007EC4 RID: 32452 RVA: 0x0020B95A File Offset: 0x00209B5A
		public void SetReference(IReference selfRef)
		{
			this.m_selfReference = (IReference<RuntimeSortFilterEventInfo>)selfRef;
		}

		// Token: 0x17002931 RID: 10545
		// (get) Token: 0x06007EC5 RID: 32453 RVA: 0x0020B968 File Offset: 0x00209B68
		internal IReference<RuntimeSortFilterEventInfo> SelfReference
		{
			get
			{
				return this.m_selfReference;
			}
		}

		// Token: 0x17002932 RID: 10546
		// (get) Token: 0x06007EC6 RID: 32454 RVA: 0x0020B970 File Offset: 0x00209B70
		public int Size
		{
			get
			{
				return ItemSizes.ReferenceSize + 4 + ItemSizes.SizeOf(this.m_sortSourceScopeInfo) + 1 + ItemSizes.SizeOf(this.m_eventSourceRowScope) + ItemSizes.SizeOf(this.m_eventSourceColScope) + 4 + 4 + ItemSizes.SizeOf<IReference<RuntimeDataRegionObj>>(this.m_detailRowScopes) + ItemSizes.SizeOf<IReference<RuntimeDataRegionObj>>(this.m_detailColScopes) + ItemSizes.SizeOf(this.m_detailRowScopeIndices) + ItemSizes.SizeOf(this.m_detailColScopeIndices) + ItemSizes.SizeOf(this.m_eventTarget) + 1 + ItemSizes.SizeOf<RuntimeExpressionInfo>(this.m_groupExpressionsInSortTarget) + ItemSizes.SizeOf<RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj>(this.m_sortFilterExpressionScopeObjects) + 4 + 4 + ItemSizes.SizeOf(this.m_sortOrders) + 1 + 4 + 4 + 4 + ItemSizes.SizeOf(this.m_peerSortFilters) + 4 + ItemSizes.SizeOf(this.m_selfReference);
			}
		}

		// Token: 0x04003E31 RID: 15921
		[StaticReference]
		private IInScopeEventSource m_eventSource;

		// Token: 0x04003E32 RID: 15922
		private string m_oldUniqueName;

		// Token: 0x04003E33 RID: 15923
		private List<object>[] m_sortSourceScopeInfo;

		// Token: 0x04003E34 RID: 15924
		private bool m_sortDirection;

		// Token: 0x04003E35 RID: 15925
		private IReference<IScope> m_eventSourceRowScope;

		// Token: 0x04003E36 RID: 15926
		private IReference<IScope> m_eventSourceColScope;

		// Token: 0x04003E37 RID: 15927
		private int m_eventSourceColDetailIndex = -1;

		// Token: 0x04003E38 RID: 15928
		private int m_eventSourceRowDetailIndex = -1;

		// Token: 0x04003E39 RID: 15929
		private List<IReference<RuntimeDataRegionObj>> m_detailRowScopes;

		// Token: 0x04003E3A RID: 15930
		private List<IReference<RuntimeDataRegionObj>> m_detailColScopes;

		// Token: 0x04003E3B RID: 15931
		private List<int> m_detailRowScopeIndices;

		// Token: 0x04003E3C RID: 15932
		private List<int> m_detailColScopeIndices;

		// Token: 0x04003E3D RID: 15933
		private IReference<IHierarchyObj> m_eventTarget;

		// Token: 0x04003E3E RID: 15934
		private bool m_targetSortFilterInfoAdded;

		// Token: 0x04003E3F RID: 15935
		private List<RuntimeExpressionInfo> m_groupExpressionsInSortTarget;

		// Token: 0x04003E40 RID: 15936
		private List<RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj> m_sortFilterExpressionScopeObjects;

		// Token: 0x04003E41 RID: 15937
		private int m_currentSortIndex = 1;

		// Token: 0x04003E42 RID: 15938
		private int m_currentInstanceIndex;

		// Token: 0x04003E43 RID: 15939
		private Microsoft.ReportingServices.ReportIntermediateFormat.ScopeLookupTable m_sortOrders;

		// Token: 0x04003E44 RID: 15940
		private bool m_processed;

		// Token: 0x04003E45 RID: 15941
		private int m_nullScopeCount;

		// Token: 0x04003E46 RID: 15942
		private string m_newUniqueName;

		// Token: 0x04003E47 RID: 15943
		private int m_depth;

		// Token: 0x04003E48 RID: 15944
		private Hashtable m_peerSortFilters;

		// Token: 0x04003E49 RID: 15945
		[NonSerialized]
		private IReference<RuntimeSortFilterEventInfo> m_selfReference;

		// Token: 0x04003E4A RID: 15946
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeSortFilterEventInfo.GetDeclaration();

		// Token: 0x02000D20 RID: 3360
		[PersistedWithinRequestOnly]
		internal class SortFilterExpressionScopeObj : IHierarchyObj, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
		{
			// Token: 0x06008F0B RID: 36619 RVA: 0x0024659F File Offset: 0x0024479F
			internal SortFilterExpressionScopeObj()
			{
			}

			// Token: 0x06008F0C RID: 36620 RVA: 0x002465AE File Offset: 0x002447AE
			internal SortFilterExpressionScopeObj(IReference<RuntimeSortFilterEventInfo> owner, OnDemandProcessingContext odpContext, int depth)
			{
				this.m_scopeInstances = new ScalableList<IReference<RuntimeDataRegionObj>>(depth, odpContext.TablixProcessingScalabilityCache);
				this.m_scopeValuesList = new ScalableList<RuntimeSortFilterEventInfo.SortScopeValuesHolder>(depth, odpContext.TablixProcessingScalabilityCache);
			}

			// Token: 0x17002BDD RID: 11229
			// (get) Token: 0x06008F0D RID: 36621 RVA: 0x002465E1 File Offset: 0x002447E1
			public int Depth
			{
				get
				{
					return -1;
				}
			}

			// Token: 0x17002BDE RID: 11230
			// (get) Token: 0x06008F0E RID: 36622 RVA: 0x002465E4 File Offset: 0x002447E4
			internal int CurrentScopeInstanceIndex
			{
				get
				{
					return this.m_currentScopeInstanceIndex;
				}
			}

			// Token: 0x17002BDF RID: 11231
			// (get) Token: 0x06008F0F RID: 36623 RVA: 0x002465EC File Offset: 0x002447EC
			IReference<IHierarchyObj> IHierarchyObj.HierarchyRoot
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002BE0 RID: 11232
			// (get) Token: 0x06008F10 RID: 36624 RVA: 0x002465EF File Offset: 0x002447EF
			OnDemandProcessingContext IHierarchyObj.OdpContext
			{
				get
				{
					Global.Tracer.Assert(0 < this.m_scopeInstances.Count, "(0 < m_scopeInstances.Count)");
					return this.m_scopeInstances[0].Value().OdpContext;
				}
			}

			// Token: 0x17002BE1 RID: 11233
			// (get) Token: 0x06008F11 RID: 36625 RVA: 0x00246624 File Offset: 0x00244824
			BTree IHierarchyObj.SortTree
			{
				get
				{
					return this.m_sortTree;
				}
			}

			// Token: 0x17002BE2 RID: 11234
			// (get) Token: 0x06008F12 RID: 36626 RVA: 0x0024662C File Offset: 0x0024482C
			int IHierarchyObj.ExpressionIndex
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17002BE3 RID: 11235
			// (get) Token: 0x06008F13 RID: 36627 RVA: 0x0024662F File Offset: 0x0024482F
			List<int> IHierarchyObj.SortFilterInfoIndices
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002BE4 RID: 11236
			// (get) Token: 0x06008F14 RID: 36628 RVA: 0x00246632 File Offset: 0x00244832
			bool IHierarchyObj.IsDetail
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002BE5 RID: 11237
			// (get) Token: 0x06008F15 RID: 36629 RVA: 0x00246635 File Offset: 0x00244835
			bool IHierarchyObj.InDataRowSortPhase
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008F16 RID: 36630 RVA: 0x00246638 File Offset: 0x00244838
			IHierarchyObj IHierarchyObj.CreateHierarchyObjForSortTree()
			{
				return new RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder(null);
			}

			// Token: 0x06008F17 RID: 36631 RVA: 0x00246640 File Offset: 0x00244840
			ProcessingMessageList IHierarchyObj.RegisterComparisonError(string propertyName)
			{
				return ((IHierarchyObj)this).OdpContext.RegisterComparisonErrorForSortFilterEvent(propertyName);
			}

			// Token: 0x06008F18 RID: 36632 RVA: 0x0024664E File Offset: 0x0024484E
			void IHierarchyObj.NextRow(IHierarchyObj owner)
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008F19 RID: 36633 RVA: 0x0024665C File Offset: 0x0024485C
			public void Traverse(ProcessingStages operation, ITraversalContext traversalContext)
			{
				UserSortFilterTraversalContext userSortFilterTraversalContext = (UserSortFilterTraversalContext)traversalContext;
				userSortFilterTraversalContext.ExpressionScope = this;
				if (this.m_sortTree != null)
				{
					this.m_sortTree.Traverse(operation, userSortFilterTraversalContext.EventInfo.SortDirection, traversalContext);
				}
			}

			// Token: 0x06008F1A RID: 36634 RVA: 0x00246697 File Offset: 0x00244897
			void IHierarchyObj.ReadRow()
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008F1B RID: 36635 RVA: 0x002466A4 File Offset: 0x002448A4
			void IHierarchyObj.ProcessUserSort()
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008F1C RID: 36636 RVA: 0x002466B1 File Offset: 0x002448B1
			void IHierarchyObj.MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo)
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008F1D RID: 36637 RVA: 0x002466BE File Offset: 0x002448BE
			void IHierarchyObj.AddSortInfoIndex(int sortFilterInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008F1E RID: 36638 RVA: 0x002466CB File Offset: 0x002448CB
			internal void RegisterScopeInstance(IReference<RuntimeDataRegionObj> scopeObj, List<object>[] scopeValues)
			{
				this.m_scopeInstances.Add(scopeObj);
				this.m_scopeValuesList.Add(new RuntimeSortFilterEventInfo.SortScopeValuesHolder(scopeValues));
			}

			// Token: 0x06008F1F RID: 36639 RVA: 0x002466EC File Offset: 0x002448EC
			internal void SortSEScopes(OnDemandProcessingContext odpContext, IInScopeEventSource eventSource)
			{
				this.m_sortTree = new BTree(this, odpContext, this.Depth + 1);
				for (int i = 0; i < this.m_scopeInstances.Count; i++)
				{
					IReference<RuntimeDataRegionObj> reference = this.m_scopeInstances[i];
					this.m_currentScopeInstanceIndex = i;
					using (reference.PinValue())
					{
						reference.Value().SetupEnvironment();
					}
					this.m_sortTree.NextRow(odpContext.ReportRuntime.EvaluateUserSortExpression(eventSource), this);
				}
			}

			// Token: 0x06008F20 RID: 36640 RVA: 0x00246780 File Offset: 0x00244980
			internal void AddSortOrder(RuntimeSortFilterEventInfo owner, int scopeInstanceIndex, bool incrementCounter)
			{
				owner.AddSortOrder(this.m_scopeValuesList[scopeInstanceIndex].Values, incrementCounter);
			}

			// Token: 0x06008F21 RID: 36641 RVA: 0x0024679C File Offset: 0x0024499C
			public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj.m_declaration);
				while (writer.NextMember())
				{
					switch (writer.CurrentMember.MemberName)
					{
					case MemberName.ScopeInstances:
						writer.Write(this.m_scopeInstances);
						break;
					case MemberName.ScopeValuesList:
						writer.Write(this.m_scopeValuesList);
						break;
					case MemberName.SortTree:
						writer.Write(this.m_sortTree);
						break;
					case MemberName.CurrentScopeIndex:
						writer.Write(this.m_currentScopeInstanceIndex);
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
			}

			// Token: 0x06008F22 RID: 36642 RVA: 0x00246830 File Offset: 0x00244A30
			public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj.m_declaration);
				while (reader.NextMember())
				{
					switch (reader.CurrentMember.MemberName)
					{
					case MemberName.ScopeInstances:
						this.m_scopeInstances = reader.ReadRIFObject<ScalableList<IReference<RuntimeDataRegionObj>>>();
						break;
					case MemberName.ScopeValuesList:
						this.m_scopeValuesList = reader.ReadRIFObject<ScalableList<RuntimeSortFilterEventInfo.SortScopeValuesHolder>>();
						break;
					case MemberName.SortTree:
						this.m_sortTree = (BTree)reader.ReadRIFObject();
						break;
					case MemberName.CurrentScopeIndex:
						this.m_currentScopeInstanceIndex = reader.ReadInt32();
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
			}

			// Token: 0x06008F23 RID: 36643 RVA: 0x002468C8 File Offset: 0x00244AC8
			public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
			{
			}

			// Token: 0x06008F24 RID: 36644 RVA: 0x002468CA File Offset: 0x00244ACA
			public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterExpressionScopeObj;
			}

			// Token: 0x06008F25 RID: 36645 RVA: 0x002468D0 File Offset: 0x00244AD0
			internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
			{
				if (RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj.m_declaration == null)
				{
					return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterExpressionScopeObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.ScopeInstances, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObjReference),
						new MemberInfo(MemberName.ScopeValuesList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortScopeValuesHolder),
						new MemberInfo(MemberName.SortTree, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTree),
						new MemberInfo(MemberName.CurrentScopeIndex, Token.Int32)
					});
				}
				return RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj.m_declaration;
			}

			// Token: 0x17002BE6 RID: 11238
			// (get) Token: 0x06008F26 RID: 36646 RVA: 0x00246942 File Offset: 0x00244B42
			public int Size
			{
				get
				{
					return ItemSizes.SizeOf<IReference<RuntimeDataRegionObj>>(this.m_scopeInstances) + ItemSizes.SizeOf<RuntimeSortFilterEventInfo.SortScopeValuesHolder>(this.m_scopeValuesList) + ItemSizes.SizeOf(this.m_sortTree) + 4;
				}
			}

			// Token: 0x04005066 RID: 20582
			private ScalableList<IReference<RuntimeDataRegionObj>> m_scopeInstances;

			// Token: 0x04005067 RID: 20583
			private ScalableList<RuntimeSortFilterEventInfo.SortScopeValuesHolder> m_scopeValuesList;

			// Token: 0x04005068 RID: 20584
			private BTree m_sortTree;

			// Token: 0x04005069 RID: 20585
			private int m_currentScopeInstanceIndex = -1;

			// Token: 0x0400506A RID: 20586
			[NonSerialized]
			private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj.GetDeclaration();
		}

		// Token: 0x02000D21 RID: 3361
		[PersistedWithinRequestOnly]
		internal class SortScopeValuesHolder : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
		{
			// Token: 0x06008F28 RID: 36648 RVA: 0x00246975 File Offset: 0x00244B75
			public SortScopeValuesHolder()
			{
			}

			// Token: 0x06008F29 RID: 36649 RVA: 0x0024697D File Offset: 0x00244B7D
			public SortScopeValuesHolder(List<object>[] values)
			{
				this.m_values = values;
			}

			// Token: 0x17002BE7 RID: 11239
			// (get) Token: 0x06008F2A RID: 36650 RVA: 0x0024698C File Offset: 0x00244B8C
			public List<object>[] Values
			{
				get
				{
					return this.m_values;
				}
			}

			// Token: 0x17002BE8 RID: 11240
			// (get) Token: 0x06008F2B RID: 36651 RVA: 0x00246994 File Offset: 0x00244B94
			public int Size
			{
				get
				{
					return ItemSizes.SizeOf(this.m_values);
				}
			}

			// Token: 0x06008F2C RID: 36652 RVA: 0x002469A4 File Offset: 0x00244BA4
			public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(RuntimeSortFilterEventInfo.SortScopeValuesHolder.m_declaration);
				while (writer.NextMember())
				{
					if (writer.CurrentMember.MemberName == MemberName.Values)
					{
						writer.WriteArrayOfListsOfPrimitives<object>(this.m_values);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}

			// Token: 0x06008F2D RID: 36653 RVA: 0x002469F8 File Offset: 0x00244BF8
			public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(RuntimeSortFilterEventInfo.SortScopeValuesHolder.m_declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName == MemberName.Values)
					{
						this.m_values = reader.ReadArrayOfListsOfPrimitives<object>();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}

			// Token: 0x06008F2E RID: 36654 RVA: 0x00246A49 File Offset: 0x00244C49
			public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
			{
			}

			// Token: 0x06008F2F RID: 36655 RVA: 0x00246A4B File Offset: 0x00244C4B
			public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortScopeValuesHolder;
			}

			// Token: 0x06008F30 RID: 36656 RVA: 0x00246A54 File Offset: 0x00244C54
			internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
			{
				if (RuntimeSortFilterEventInfo.SortScopeValuesHolder.m_declaration == null)
				{
					return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortScopeValuesHolder, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.Values, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.Object, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList)
					});
				}
				return RuntimeSortFilterEventInfo.SortScopeValuesHolder.m_declaration;
			}

			// Token: 0x0400506B RID: 20587
			private List<object>[] m_values;

			// Token: 0x0400506C RID: 20588
			[NonSerialized]
			private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeSortFilterEventInfo.SortScopeValuesHolder.GetDeclaration();
		}

		// Token: 0x02000D22 RID: 3362
		[PersistedWithinRequestOnly]
		internal class SortExpressionScopeInstanceHolder : IHierarchyObj, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
		{
			// Token: 0x06008F32 RID: 36658 RVA: 0x00246A9F File Offset: 0x00244C9F
			internal SortExpressionScopeInstanceHolder()
			{
			}

			// Token: 0x06008F33 RID: 36659 RVA: 0x00246AA7 File Offset: 0x00244CA7
			internal SortExpressionScopeInstanceHolder(OnDemandProcessingContext odpContext)
			{
				this.m_scopeInstanceIndices = new List<int>();
			}

			// Token: 0x17002BE9 RID: 11241
			// (get) Token: 0x06008F34 RID: 36660 RVA: 0x00246ABA File Offset: 0x00244CBA
			public int Depth
			{
				get
				{
					return -1;
				}
			}

			// Token: 0x17002BEA RID: 11242
			// (get) Token: 0x06008F35 RID: 36661 RVA: 0x00246ABD File Offset: 0x00244CBD
			IReference<IHierarchyObj> IHierarchyObj.HierarchyRoot
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002BEB RID: 11243
			// (get) Token: 0x06008F36 RID: 36662 RVA: 0x00246AC0 File Offset: 0x00244CC0
			OnDemandProcessingContext IHierarchyObj.OdpContext
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002BEC RID: 11244
			// (get) Token: 0x06008F37 RID: 36663 RVA: 0x00246AC3 File Offset: 0x00244CC3
			BTree IHierarchyObj.SortTree
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002BED RID: 11245
			// (get) Token: 0x06008F38 RID: 36664 RVA: 0x00246AC6 File Offset: 0x00244CC6
			int IHierarchyObj.ExpressionIndex
			{
				get
				{
					return -1;
				}
			}

			// Token: 0x17002BEE RID: 11246
			// (get) Token: 0x06008F39 RID: 36665 RVA: 0x00246AC9 File Offset: 0x00244CC9
			List<int> IHierarchyObj.SortFilterInfoIndices
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002BEF RID: 11247
			// (get) Token: 0x06008F3A RID: 36666 RVA: 0x00246ACC File Offset: 0x00244CCC
			bool IHierarchyObj.IsDetail
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002BF0 RID: 11248
			// (get) Token: 0x06008F3B RID: 36667 RVA: 0x00246ACF File Offset: 0x00244CCF
			bool IHierarchyObj.InDataRowSortPhase
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008F3C RID: 36668 RVA: 0x00246AD2 File Offset: 0x00244CD2
			IHierarchyObj IHierarchyObj.CreateHierarchyObjForSortTree()
			{
				Global.Tracer.Assert(false);
				return null;
			}

			// Token: 0x06008F3D RID: 36669 RVA: 0x00246AE0 File Offset: 0x00244CE0
			ProcessingMessageList IHierarchyObj.RegisterComparisonError(string propertyName)
			{
				Global.Tracer.Assert(false);
				return null;
			}

			// Token: 0x06008F3E RID: 36670 RVA: 0x00246AEE File Offset: 0x00244CEE
			void IHierarchyObj.NextRow(IHierarchyObj owner)
			{
				this.m_scopeInstanceIndices.Add(((RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj)owner).CurrentScopeInstanceIndex);
			}

			// Token: 0x06008F3F RID: 36671 RVA: 0x00246B08 File Offset: 0x00244D08
			void IHierarchyObj.Traverse(ProcessingStages operation, ITraversalContext traversalContext)
			{
				UserSortFilterTraversalContext userSortFilterTraversalContext = (UserSortFilterTraversalContext)traversalContext;
				RuntimeSortFilterEventInfo eventInfo = userSortFilterTraversalContext.EventInfo;
				RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj expressionScope = userSortFilterTraversalContext.ExpressionScope;
				if (eventInfo.SortDirection)
				{
					for (int i = 0; i < this.m_scopeInstanceIndices.Count; i++)
					{
						expressionScope.AddSortOrder(eventInfo, this.m_scopeInstanceIndices[i], i == this.m_scopeInstanceIndices.Count - 1);
					}
					return;
				}
				for (int j = this.m_scopeInstanceIndices.Count - 1; j >= 0; j--)
				{
					expressionScope.AddSortOrder(eventInfo, this.m_scopeInstanceIndices[j], j == 0);
				}
			}

			// Token: 0x06008F40 RID: 36672 RVA: 0x00246B99 File Offset: 0x00244D99
			void IHierarchyObj.ReadRow()
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008F41 RID: 36673 RVA: 0x00246BA6 File Offset: 0x00244DA6
			void IHierarchyObj.ProcessUserSort()
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008F42 RID: 36674 RVA: 0x00246BB3 File Offset: 0x00244DB3
			void IHierarchyObj.MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo)
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008F43 RID: 36675 RVA: 0x00246BC0 File Offset: 0x00244DC0
			void IHierarchyObj.AddSortInfoIndex(int sortFilterInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008F44 RID: 36676 RVA: 0x00246BD0 File Offset: 0x00244DD0
			void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder.m_declaration);
				while (writer.NextMember())
				{
					if (writer.CurrentMember.MemberName == MemberName.ScopeInstanceIndices)
					{
						writer.WriteListOfPrimitives<int>(this.m_scopeInstanceIndices);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}

			// Token: 0x06008F45 RID: 36677 RVA: 0x00246C20 File Offset: 0x00244E20
			void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder.m_declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName == MemberName.ScopeInstanceIndices)
					{
						this.m_scopeInstanceIndices = reader.ReadListOfPrimitives<int>();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}

			// Token: 0x06008F46 RID: 36678 RVA: 0x00246C6E File Offset: 0x00244E6E
			void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
			{
			}

			// Token: 0x06008F47 RID: 36679 RVA: 0x00246C70 File Offset: 0x00244E70
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortExpressionScopeInstanceHolder;
			}

			// Token: 0x06008F48 RID: 36680 RVA: 0x00246C74 File Offset: 0x00244E74
			internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
			{
				if (RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder.m_declaration == null)
				{
					return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortExpressionScopeInstanceHolder, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.ScopeInstanceIndices, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32)
					});
				}
				return RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder.m_declaration;
			}

			// Token: 0x17002BF1 RID: 11249
			// (get) Token: 0x06008F49 RID: 36681 RVA: 0x00246CB0 File Offset: 0x00244EB0
			public int Size
			{
				get
				{
					return ItemSizes.SizeOf(this.m_scopeInstanceIndices);
				}
			}

			// Token: 0x0400506D RID: 20589
			private List<int> m_scopeInstanceIndices;

			// Token: 0x0400506E RID: 20590
			[NonSerialized]
			private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder.GetDeclaration();
		}
	}
}
