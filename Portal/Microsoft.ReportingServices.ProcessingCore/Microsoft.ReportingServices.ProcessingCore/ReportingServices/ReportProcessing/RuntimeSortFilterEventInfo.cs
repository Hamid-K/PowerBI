using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200077D RID: 1917
	internal sealed class RuntimeSortFilterEventInfo
	{
		// Token: 0x06006ADB RID: 27355 RVA: 0x001AF718 File Offset: 0x001AD918
		internal RuntimeSortFilterEventInfo(TextBox eventSource, int oldUniqueName, bool sortDirection, VariantList[] sortSourceScopeInfo)
		{
			Global.Tracer.Assert(eventSource != null && eventSource.UserSort != null, "(null != eventSource && null != eventSource.UserSort)");
			this.m_eventSource = eventSource;
			this.m_oldUniqueName = oldUniqueName;
			this.m_sortDirection = sortDirection;
			this.m_sortSourceScopeInfo = sortSourceScopeInfo;
		}

		// Token: 0x17002554 RID: 9556
		// (get) Token: 0x06006ADC RID: 27356 RVA: 0x001AF77B File Offset: 0x001AD97B
		internal TextBox EventSource
		{
			get
			{
				return this.m_eventSource;
			}
		}

		// Token: 0x17002555 RID: 9557
		// (get) Token: 0x06006ADD RID: 27357 RVA: 0x001AF783 File Offset: 0x001AD983
		// (set) Token: 0x06006ADE RID: 27358 RVA: 0x001AF78B File Offset: 0x001AD98B
		internal ReportProcessing.IScope EventSourceScope
		{
			get
			{
				return this.m_eventSourceScope;
			}
			set
			{
				this.m_eventSourceScope = value;
			}
		}

		// Token: 0x17002556 RID: 9558
		// (get) Token: 0x06006ADF RID: 27359 RVA: 0x001AF794 File Offset: 0x001AD994
		// (set) Token: 0x06006AE0 RID: 27360 RVA: 0x001AF79C File Offset: 0x001AD99C
		internal int EventSourceDetailIndex
		{
			get
			{
				return this.m_eventSourceDetailIndex;
			}
			set
			{
				this.m_eventSourceDetailIndex = value;
			}
		}

		// Token: 0x17002557 RID: 9559
		// (get) Token: 0x06006AE1 RID: 27361 RVA: 0x001AF7A5 File Offset: 0x001AD9A5
		// (set) Token: 0x06006AE2 RID: 27362 RVA: 0x001AF7AD File Offset: 0x001AD9AD
		internal ReportProcessing.RuntimeDataRegionObjList DetailScopes
		{
			get
			{
				return this.m_detailScopes;
			}
			set
			{
				this.m_detailScopes = value;
			}
		}

		// Token: 0x17002558 RID: 9560
		// (get) Token: 0x06006AE3 RID: 27363 RVA: 0x001AF7B6 File Offset: 0x001AD9B6
		// (set) Token: 0x06006AE4 RID: 27364 RVA: 0x001AF7BE File Offset: 0x001AD9BE
		internal IntList DetailScopeIndices
		{
			get
			{
				return this.m_detailScopeIndices;
			}
			set
			{
				this.m_detailScopeIndices = value;
			}
		}

		// Token: 0x17002559 RID: 9561
		// (get) Token: 0x06006AE5 RID: 27365 RVA: 0x001AF7C7 File Offset: 0x001AD9C7
		// (set) Token: 0x06006AE6 RID: 27366 RVA: 0x001AF7CF File Offset: 0x001AD9CF
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

		// Token: 0x1700255A RID: 9562
		// (get) Token: 0x06006AE7 RID: 27367 RVA: 0x001AF7D8 File Offset: 0x001AD9D8
		internal VariantList[] SortSourceScopeInfo
		{
			get
			{
				return this.m_sortSourceScopeInfo;
			}
		}

		// Token: 0x1700255B RID: 9563
		// (get) Token: 0x06006AE8 RID: 27368 RVA: 0x001AF7E0 File Offset: 0x001AD9E0
		// (set) Token: 0x06006AE9 RID: 27369 RVA: 0x001AF7E8 File Offset: 0x001AD9E8
		internal ReportProcessing.IHierarchyObj EventTarget
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

		// Token: 0x1700255C RID: 9564
		// (get) Token: 0x06006AEA RID: 27370 RVA: 0x001AF7F1 File Offset: 0x001AD9F1
		// (set) Token: 0x06006AEB RID: 27371 RVA: 0x001AF7F9 File Offset: 0x001AD9F9
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

		// Token: 0x1700255D RID: 9565
		// (get) Token: 0x06006AEC RID: 27372 RVA: 0x001AF802 File Offset: 0x001ADA02
		// (set) Token: 0x06006AED RID: 27373 RVA: 0x001AF80A File Offset: 0x001ADA0A
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

		// Token: 0x1700255E RID: 9566
		// (get) Token: 0x06006AEE RID: 27374 RVA: 0x001AF813 File Offset: 0x001ADA13
		// (set) Token: 0x06006AEF RID: 27375 RVA: 0x001AF81B File Offset: 0x001ADA1B
		internal int OldUniqueName
		{
			get
			{
				return this.m_oldUniqueName;
			}
			set
			{
				this.m_oldUniqueName = value;
			}
		}

		// Token: 0x1700255F RID: 9567
		// (get) Token: 0x06006AF0 RID: 27376 RVA: 0x001AF824 File Offset: 0x001ADA24
		// (set) Token: 0x06006AF1 RID: 27377 RVA: 0x001AF82C File Offset: 0x001ADA2C
		internal int NewUniqueName
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

		// Token: 0x17002560 RID: 9568
		// (get) Token: 0x06006AF2 RID: 27378 RVA: 0x001AF835 File Offset: 0x001ADA35
		// (set) Token: 0x06006AF3 RID: 27379 RVA: 0x001AF83D File Offset: 0x001ADA3D
		internal int Page
		{
			get
			{
				return this.m_page;
			}
			set
			{
				this.m_page = value;
			}
		}

		// Token: 0x17002561 RID: 9569
		// (get) Token: 0x06006AF4 RID: 27380 RVA: 0x001AF846 File Offset: 0x001ADA46
		// (set) Token: 0x06006AF5 RID: 27381 RVA: 0x001AF84E File Offset: 0x001ADA4E
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

		// Token: 0x06006AF6 RID: 27382 RVA: 0x001AF858 File Offset: 0x001ADA58
		internal void RegisterSortFilterExpressionScope(ref int containerSortFilterExprScopeIndex, ReportProcessing.RuntimeDataRegionObj scopeObj, VariantList[] scopeValues, int sortFilterInfoIndex)
		{
			if (this.m_eventTarget != null && !this.m_targetSortFilterInfoAdded)
			{
				this.m_eventTarget.AddSortInfoIndex(sortFilterInfoIndex, this);
			}
			RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj sortFilterExpressionScopeObj;
			if (-1 != containerSortFilterExprScopeIndex)
			{
				sortFilterExpressionScopeObj = (RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj)this.m_sortFilterExpressionScopeObjects[containerSortFilterExprScopeIndex];
			}
			else
			{
				if (this.m_sortFilterExpressionScopeObjects == null)
				{
					this.m_sortFilterExpressionScopeObjects = new ArrayList();
				}
				containerSortFilterExprScopeIndex = this.m_sortFilterExpressionScopeObjects.Count;
				sortFilterExpressionScopeObj = new RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj(this);
				this.m_sortFilterExpressionScopeObjects.Add(sortFilterExpressionScopeObj);
			}
			sortFilterExpressionScopeObj.RegisterScopeInstance(scopeObj, scopeValues);
		}

		// Token: 0x06006AF7 RID: 27383 RVA: 0x001AF8DC File Offset: 0x001ADADC
		internal void PrepareForSorting(ReportProcessing.ProcessingContext processingContext)
		{
			Global.Tracer.Assert(!this.m_processed, "(!m_processed)");
			if (this.m_eventTarget != null && this.m_sortFilterExpressionScopeObjects != null)
			{
				processingContext.UserSortFilterContext.CurrentSortFilterEventSource = this.m_eventSource;
				for (int i = 0; i < this.m_sortFilterExpressionScopeObjects.Count; i++)
				{
					((RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj)this.m_sortFilterExpressionScopeObjects[i]).SortSEScopes(processingContext, this.m_eventSource);
				}
				GroupingList groupsInSortTarget = this.m_eventSource.UserSort.GroupsInSortTarget;
				if (groupsInSortTarget != null && 0 < groupsInSortTarget.Count)
				{
					this.m_groupExpressionsInSortTarget = new ReportProcessing.RuntimeExpressionInfoList();
					for (int j = 0; j < groupsInSortTarget.Count; j++)
					{
						Grouping grouping = groupsInSortTarget[j];
						for (int k = 0; k < grouping.GroupExpressions.Count; k++)
						{
							this.m_groupExpressionsInSortTarget.Add(new ReportProcessing.RuntimeExpressionInfo(grouping.GroupExpressions, grouping.ExprHost, null, k));
						}
					}
				}
				this.CollectSortOrders();
			}
		}

		// Token: 0x06006AF8 RID: 27384 RVA: 0x001AF9DC File Offset: 0x001ADBDC
		private void CollectSortOrders()
		{
			this.m_currentSortIndex = 1;
			for (int i = 0; i < this.m_sortFilterExpressionScopeObjects.Count; i++)
			{
				((ReportProcessing.IHierarchyObj)this.m_sortFilterExpressionScopeObjects[i]).Traverse(ReportProcessing.ProcessingStages.UserSortFilter);
			}
			this.m_sortFilterExpressionScopeObjects = null;
		}

		// Token: 0x06006AF9 RID: 27385 RVA: 0x001AFA24 File Offset: 0x001ADC24
		internal bool ProcessSorting(ReportProcessing.ProcessingContext processingContext)
		{
			Global.Tracer.Assert(!this.m_processed, "(!m_processed)");
			if (this.m_eventTarget == null)
			{
				return false;
			}
			this.m_eventTarget.ProcessUserSort();
			this.m_sortOrders = null;
			return true;
		}

		// Token: 0x06006AFA RID: 27386 RVA: 0x001AFA5C File Offset: 0x001ADC5C
		private void AddSortOrder(VariantList[] scopeValues, bool incrementCounter)
		{
			if (this.m_sortOrders == null)
			{
				this.m_sortOrders = new ScopeLookupTable();
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

		// Token: 0x06006AFB RID: 27387 RVA: 0x001AFB18 File Offset: 0x001ADD18
		internal object GetSortOrder(ReportRuntime runtime)
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
				GroupingList groupsInSortTarget = this.m_eventSource.UserSort.GroupsInSortTarget;
				if (groupsInSortTarget == null || groupsInSortTarget.Count == 0)
				{
					obj = this.m_sortOrders.LookupTable;
				}
				else
				{
					bool flag = true;
					bool flag2 = false;
					int num = 0;
					Hashtable hashtable = (Hashtable)this.m_sortOrders.LookupTable;
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
						Grouping grouping = groupsInSortTarget[i];
						for (int k = 0; k < grouping.GroupExpressions.Count; k++)
						{
							object obj2 = runtime.EvaluateRuntimeExpression(this.m_groupExpressionsInSortTarget[num], ObjectType.Grouping, grouping.Name, "GroupExpression");
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

		// Token: 0x06006AFC RID: 27388 RVA: 0x001AFCEC File Offset: 0x001ADEEC
		internal void MatchEventSource(TextBox textBox, TextBoxInstance textBoxInstance, ReportProcessing.IScope containingScope, ReportProcessing.ProcessingContext processingContext)
		{
			bool flag = false;
			if (!(containingScope is ReportProcessing.RuntimePivotCell))
			{
				while (containingScope != null && !(containingScope is ReportProcessing.RuntimeGroupLeafObj) && !(containingScope is ReportProcessing.RuntimeDetailObj) && !(containingScope is ReportProcessing.RuntimeOnePassDetailObj))
				{
					containingScope = containingScope.GetOuterScope(true);
				}
			}
			if (containingScope == null)
			{
				if (this.m_eventSource.ContainingScopes == null || this.m_eventSource.ContainingScopes.Count == 0)
				{
					flag = true;
				}
			}
			else if (this.m_eventSourceScope == containingScope)
			{
				flag = true;
				DataRegion dataRegion = null;
				if (containingScope is ReportProcessing.RuntimeDetailObj)
				{
					dataRegion = ((ReportProcessing.RuntimeDetailObj)containingScope).DataRegionDef;
				}
				else if (containingScope is ReportProcessing.RuntimeOnePassDetailObj)
				{
					dataRegion = ((ReportProcessing.RuntimeOnePassDetailObj)containingScope).DataRegionDef;
				}
				if (dataRegion != null && dataRegion.CurrentDetailRowIndex != this.m_eventSourceDetailIndex)
				{
					flag = false;
				}
			}
			if (flag)
			{
				if (textBox == this.m_eventSource)
				{
					this.m_newUniqueName = textBoxInstance.UniqueName;
					this.m_page = processingContext.Pagination.GetTextBoxStartPage(textBox);
					return;
				}
				if (this.m_peerSortFilters != null && this.m_peerSortFilters.Contains(textBox.ID))
				{
					this.m_peerSortFilters[textBox.ID] = textBoxInstance.UniqueName;
				}
			}
		}

		// Token: 0x040035E4 RID: 13796
		private TextBox m_eventSource;

		// Token: 0x040035E5 RID: 13797
		private int m_oldUniqueName;

		// Token: 0x040035E6 RID: 13798
		private VariantList[] m_sortSourceScopeInfo;

		// Token: 0x040035E7 RID: 13799
		private bool m_sortDirection;

		// Token: 0x040035E8 RID: 13800
		private ReportProcessing.IScope m_eventSourceScope;

		// Token: 0x040035E9 RID: 13801
		private int m_eventSourceDetailIndex = -1;

		// Token: 0x040035EA RID: 13802
		private ReportProcessing.RuntimeDataRegionObjList m_detailScopes;

		// Token: 0x040035EB RID: 13803
		private IntList m_detailScopeIndices;

		// Token: 0x040035EC RID: 13804
		private ReportProcessing.IHierarchyObj m_eventTarget;

		// Token: 0x040035ED RID: 13805
		private bool m_targetSortFilterInfoAdded;

		// Token: 0x040035EE RID: 13806
		private ReportProcessing.RuntimeExpressionInfoList m_groupExpressionsInSortTarget;

		// Token: 0x040035EF RID: 13807
		private ArrayList m_sortFilterExpressionScopeObjects;

		// Token: 0x040035F0 RID: 13808
		private int m_currentSortIndex = 1;

		// Token: 0x040035F1 RID: 13809
		private int m_currentInstanceIndex;

		// Token: 0x040035F2 RID: 13810
		private ScopeLookupTable m_sortOrders;

		// Token: 0x040035F3 RID: 13811
		private bool m_processed;

		// Token: 0x040035F4 RID: 13812
		private int m_nullScopeCount;

		// Token: 0x040035F5 RID: 13813
		private int m_newUniqueName = -1;

		// Token: 0x040035F6 RID: 13814
		private int m_page;

		// Token: 0x040035F7 RID: 13815
		private Hashtable m_peerSortFilters;

		// Token: 0x02000CE2 RID: 3298
		private class SortFilterExpressionScopeObj : ReportProcessing.IHierarchyObj
		{
			// Token: 0x06008D43 RID: 36163 RVA: 0x0023EBD7 File Offset: 0x0023CDD7
			internal SortFilterExpressionScopeObj(RuntimeSortFilterEventInfo owner)
			{
				this.m_owner = owner;
				this.m_scopeInstances = new ReportProcessing.RuntimeDataRegionObjList();
				this.m_scopeValuesList = new ArrayList();
			}

			// Token: 0x17002B57 RID: 11095
			// (get) Token: 0x06008D44 RID: 36164 RVA: 0x0023EC03 File Offset: 0x0023CE03
			internal int CurrentScopeInstanceIndex
			{
				get
				{
					return this.m_currentScopeInstanceIndex;
				}
			}

			// Token: 0x17002B58 RID: 11096
			// (get) Token: 0x06008D45 RID: 36165 RVA: 0x0023EC0B File Offset: 0x0023CE0B
			internal bool SortDirection
			{
				get
				{
					return this.m_owner.SortDirection;
				}
			}

			// Token: 0x17002B59 RID: 11097
			// (get) Token: 0x06008D46 RID: 36166 RVA: 0x0023EC18 File Offset: 0x0023CE18
			ReportProcessing.IHierarchyObj ReportProcessing.IHierarchyObj.HierarchyRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17002B5A RID: 11098
			// (get) Token: 0x06008D47 RID: 36167 RVA: 0x0023EC1B File Offset: 0x0023CE1B
			ReportProcessing.ProcessingContext ReportProcessing.IHierarchyObj.ProcessingContext
			{
				get
				{
					Global.Tracer.Assert(0 < this.m_scopeInstances.Count, "(0 < m_scopeInstances.Count)");
					return this.m_scopeInstances[0].ProcessingContext;
				}
			}

			// Token: 0x17002B5B RID: 11099
			// (get) Token: 0x06008D48 RID: 36168 RVA: 0x0023EC4B File Offset: 0x0023CE4B
			// (set) Token: 0x06008D49 RID: 36169 RVA: 0x0023EC53 File Offset: 0x0023CE53
			ReportProcessing.BTreeNode ReportProcessing.IHierarchyObj.SortTree
			{
				get
				{
					return this.m_sortTree;
				}
				set
				{
					this.m_sortTree = value;
				}
			}

			// Token: 0x17002B5C RID: 11100
			// (get) Token: 0x06008D4A RID: 36170 RVA: 0x0023EC5C File Offset: 0x0023CE5C
			int ReportProcessing.IHierarchyObj.ExpressionIndex
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17002B5D RID: 11101
			// (get) Token: 0x06008D4B RID: 36171 RVA: 0x0023EC5F File Offset: 0x0023CE5F
			IntList ReportProcessing.IHierarchyObj.SortFilterInfoIndices
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002B5E RID: 11102
			// (get) Token: 0x06008D4C RID: 36172 RVA: 0x0023EC62 File Offset: 0x0023CE62
			bool ReportProcessing.IHierarchyObj.IsDetail
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008D4D RID: 36173 RVA: 0x0023EC65 File Offset: 0x0023CE65
			ReportProcessing.IHierarchyObj ReportProcessing.IHierarchyObj.CreateHierarchyObj()
			{
				return new RuntimeSortFilterEventInfo.SortExpressionScopeInstanceHolder(this);
			}

			// Token: 0x06008D4E RID: 36174 RVA: 0x0023EC6D File Offset: 0x0023CE6D
			ProcessingMessageList ReportProcessing.IHierarchyObj.RegisterComparisonError(string propertyName)
			{
				return ((ReportProcessing.IHierarchyObj)this).ProcessingContext.RegisterComparisonErrorForSortFilterEvent(propertyName);
			}

			// Token: 0x06008D4F RID: 36175 RVA: 0x0023EC7B File Offset: 0x0023CE7B
			void ReportProcessing.IHierarchyObj.NextRow()
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008D50 RID: 36176 RVA: 0x0023EC88 File Offset: 0x0023CE88
			void ReportProcessing.IHierarchyObj.Traverse(ReportProcessing.ProcessingStages operation)
			{
				if (this.m_sortTree != null)
				{
					this.m_sortTree.Traverse(operation, this.m_owner.SortDirection);
				}
			}

			// Token: 0x06008D51 RID: 36177 RVA: 0x0023ECA9 File Offset: 0x0023CEA9
			void ReportProcessing.IHierarchyObj.ReadRow()
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008D52 RID: 36178 RVA: 0x0023ECB6 File Offset: 0x0023CEB6
			void ReportProcessing.IHierarchyObj.ProcessUserSort()
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008D53 RID: 36179 RVA: 0x0023ECC3 File Offset: 0x0023CEC3
			void ReportProcessing.IHierarchyObj.MarkSortInfoProcessed(RuntimeSortFilterEventInfoList runtimeSortFilterInfo)
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008D54 RID: 36180 RVA: 0x0023ECD0 File Offset: 0x0023CED0
			void ReportProcessing.IHierarchyObj.AddSortInfoIndex(int sortFilterInfoIndex, RuntimeSortFilterEventInfo sortInfo)
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008D55 RID: 36181 RVA: 0x0023ECDD File Offset: 0x0023CEDD
			internal void RegisterScopeInstance(ReportProcessing.RuntimeDataRegionObj scopeObj, VariantList[] scopeValues)
			{
				this.m_scopeInstances.Add(scopeObj);
				this.m_scopeValuesList.Add(scopeValues);
			}

			// Token: 0x06008D56 RID: 36182 RVA: 0x0023ECFC File Offset: 0x0023CEFC
			internal void SortSEScopes(ReportProcessing.ProcessingContext processingContext, TextBox eventSource)
			{
				this.m_sortTree = new ReportProcessing.BTreeNode(this);
				for (int i = 0; i < this.m_scopeInstances.Count; i++)
				{
					ReportProcessing.RuntimeDataRegionObj runtimeDataRegionObj = this.m_scopeInstances[i];
					this.m_currentScopeInstanceIndex = i;
					runtimeDataRegionObj.SetupEnvironment();
					this.m_sortTree.NextRow(processingContext.ReportRuntime.EvaluateUserSortExpression(eventSource));
				}
			}

			// Token: 0x06008D57 RID: 36183 RVA: 0x0023ED5A File Offset: 0x0023CF5A
			internal void AddSortOrder(int scopeInstanceIndex, bool incrementCounter)
			{
				this.m_owner.AddSortOrder((VariantList[])this.m_scopeValuesList[scopeInstanceIndex], incrementCounter);
			}

			// Token: 0x04004F3F RID: 20287
			private RuntimeSortFilterEventInfo m_owner;

			// Token: 0x04004F40 RID: 20288
			private ReportProcessing.RuntimeDataRegionObjList m_scopeInstances;

			// Token: 0x04004F41 RID: 20289
			private ArrayList m_scopeValuesList;

			// Token: 0x04004F42 RID: 20290
			private ReportProcessing.BTreeNode m_sortTree;

			// Token: 0x04004F43 RID: 20291
			private int m_currentScopeInstanceIndex = -1;
		}

		// Token: 0x02000CE3 RID: 3299
		private class SortExpressionScopeInstanceHolder : ReportProcessing.IHierarchyObj
		{
			// Token: 0x06008D58 RID: 36184 RVA: 0x0023ED79 File Offset: 0x0023CF79
			internal SortExpressionScopeInstanceHolder(RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj owner)
			{
				this.m_owner = owner;
				this.m_scopeInstanceIndices = new IntList();
			}

			// Token: 0x17002B5F RID: 11103
			// (get) Token: 0x06008D59 RID: 36185 RVA: 0x0023ED93 File Offset: 0x0023CF93
			ReportProcessing.IHierarchyObj ReportProcessing.IHierarchyObj.HierarchyRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17002B60 RID: 11104
			// (get) Token: 0x06008D5A RID: 36186 RVA: 0x0023ED96 File Offset: 0x0023CF96
			ReportProcessing.ProcessingContext ReportProcessing.IHierarchyObj.ProcessingContext
			{
				get
				{
					return ((ReportProcessing.IHierarchyObj)this.m_owner).ProcessingContext;
				}
			}

			// Token: 0x17002B61 RID: 11105
			// (get) Token: 0x06008D5B RID: 36187 RVA: 0x0023EDA3 File Offset: 0x0023CFA3
			// (set) Token: 0x06008D5C RID: 36188 RVA: 0x0023EDA6 File Offset: 0x0023CFA6
			ReportProcessing.BTreeNode ReportProcessing.IHierarchyObj.SortTree
			{
				get
				{
					return null;
				}
				set
				{
					Global.Tracer.Assert(false);
				}
			}

			// Token: 0x17002B62 RID: 11106
			// (get) Token: 0x06008D5D RID: 36189 RVA: 0x0023EDB3 File Offset: 0x0023CFB3
			int ReportProcessing.IHierarchyObj.ExpressionIndex
			{
				get
				{
					return -1;
				}
			}

			// Token: 0x17002B63 RID: 11107
			// (get) Token: 0x06008D5E RID: 36190 RVA: 0x0023EDB6 File Offset: 0x0023CFB6
			IntList ReportProcessing.IHierarchyObj.SortFilterInfoIndices
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17002B64 RID: 11108
			// (get) Token: 0x06008D5F RID: 36191 RVA: 0x0023EDB9 File Offset: 0x0023CFB9
			bool ReportProcessing.IHierarchyObj.IsDetail
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008D60 RID: 36192 RVA: 0x0023EDBC File Offset: 0x0023CFBC
			ReportProcessing.IHierarchyObj ReportProcessing.IHierarchyObj.CreateHierarchyObj()
			{
				Global.Tracer.Assert(false);
				return null;
			}

			// Token: 0x06008D61 RID: 36193 RVA: 0x0023EDCA File Offset: 0x0023CFCA
			ProcessingMessageList ReportProcessing.IHierarchyObj.RegisterComparisonError(string propertyName)
			{
				Global.Tracer.Assert(false);
				return null;
			}

			// Token: 0x06008D62 RID: 36194 RVA: 0x0023EDD8 File Offset: 0x0023CFD8
			void ReportProcessing.IHierarchyObj.NextRow()
			{
				this.m_scopeInstanceIndices.Add(this.m_owner.CurrentScopeInstanceIndex);
			}

			// Token: 0x06008D63 RID: 36195 RVA: 0x0023EDF8 File Offset: 0x0023CFF8
			void ReportProcessing.IHierarchyObj.Traverse(ReportProcessing.ProcessingStages operation)
			{
				if (this.m_owner.SortDirection)
				{
					for (int i = 0; i < this.m_scopeInstanceIndices.Count; i++)
					{
						this.m_owner.AddSortOrder(this.m_scopeInstanceIndices[i], i == this.m_scopeInstanceIndices.Count - 1);
					}
					return;
				}
				for (int j = this.m_scopeInstanceIndices.Count - 1; j >= 0; j--)
				{
					this.m_owner.AddSortOrder(this.m_scopeInstanceIndices[j], j == 0);
				}
			}

			// Token: 0x06008D64 RID: 36196 RVA: 0x0023EE83 File Offset: 0x0023D083
			void ReportProcessing.IHierarchyObj.ReadRow()
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008D65 RID: 36197 RVA: 0x0023EE90 File Offset: 0x0023D090
			void ReportProcessing.IHierarchyObj.ProcessUserSort()
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008D66 RID: 36198 RVA: 0x0023EE9D File Offset: 0x0023D09D
			void ReportProcessing.IHierarchyObj.MarkSortInfoProcessed(RuntimeSortFilterEventInfoList runtimeSortFilterInfo)
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x06008D67 RID: 36199 RVA: 0x0023EEAA File Offset: 0x0023D0AA
			void ReportProcessing.IHierarchyObj.AddSortInfoIndex(int sortFilterInfoIndex, RuntimeSortFilterEventInfo sortInfo)
			{
				Global.Tracer.Assert(false);
			}

			// Token: 0x04004F44 RID: 20292
			private RuntimeSortFilterEventInfo.SortFilterExpressionScopeObj m_owner;

			// Token: 0x04004F45 RID: 20293
			private IntList m_scopeInstanceIndices;
		}
	}
}
