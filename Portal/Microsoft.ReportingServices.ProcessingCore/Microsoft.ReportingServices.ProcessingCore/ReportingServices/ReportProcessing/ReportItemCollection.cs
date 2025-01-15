using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006CD RID: 1741
	[Serializable]
	internal sealed class ReportItemCollection : IDOwner, IRunningValueHolder
	{
		// Token: 0x06005D8B RID: 23947 RVA: 0x0017DDD8 File Offset: 0x0017BFD8
		internal ReportItemCollection()
		{
		}

		// Token: 0x06005D8C RID: 23948 RVA: 0x0017DDE7 File Offset: 0x0017BFE7
		internal ReportItemCollection(int id, bool normal)
			: base(id)
		{
			this.m_runningValues = new RunningValueInfoList();
			this.m_normal = normal;
			this.m_unpopulated = true;
			this.m_entries = new ReportItemList();
		}

		// Token: 0x170020B9 RID: 8377
		internal ReportItem this[int index]
		{
			get
			{
				if (this.m_unpopulated)
				{
					Global.Tracer.Assert(this.m_entries != null);
					return this.m_entries[index];
				}
				bool flag;
				int num;
				ReportItem reportItem;
				this.GetReportItem(index, out flag, out num, out reportItem);
				return reportItem;
			}
		}

		// Token: 0x170020BA RID: 8378
		// (get) Token: 0x06005D8E RID: 23950 RVA: 0x0017DE5F File Offset: 0x0017C05F
		internal int Count
		{
			get
			{
				if (this.m_unpopulated)
				{
					Global.Tracer.Assert(this.m_entries != null);
					return this.m_entries.Count;
				}
				if (this.m_sortedReportItemList == null)
				{
					return 0;
				}
				return this.m_sortedReportItemList.Count;
			}
		}

		// Token: 0x170020BB RID: 8379
		// (get) Token: 0x06005D8F RID: 23951 RVA: 0x0017DE9D File Offset: 0x0017C09D
		// (set) Token: 0x06005D90 RID: 23952 RVA: 0x0017DEB8 File Offset: 0x0017C0B8
		internal ReportItemList ComputedReportItems
		{
			get
			{
				Global.Tracer.Assert(!this.m_unpopulated);
				return this.m_computedReportItems;
			}
			set
			{
				this.m_computedReportItems = value;
			}
		}

		// Token: 0x170020BC RID: 8380
		// (get) Token: 0x06005D91 RID: 23953 RVA: 0x0017DEC1 File Offset: 0x0017C0C1
		// (set) Token: 0x06005D92 RID: 23954 RVA: 0x0017DEDC File Offset: 0x0017C0DC
		internal ReportItemList NonComputedReportItems
		{
			get
			{
				Global.Tracer.Assert(!this.m_unpopulated);
				return this.m_nonComputedReportItems;
			}
			set
			{
				this.m_nonComputedReportItems = value;
			}
		}

		// Token: 0x170020BD RID: 8381
		// (get) Token: 0x06005D93 RID: 23955 RVA: 0x0017DEE5 File Offset: 0x0017C0E5
		// (set) Token: 0x06005D94 RID: 23956 RVA: 0x0017DF00 File Offset: 0x0017C100
		internal ReportItemIndexerList SortedReportItems
		{
			get
			{
				Global.Tracer.Assert(!this.m_unpopulated);
				return this.m_sortedReportItemList;
			}
			set
			{
				this.m_sortedReportItemList = value;
			}
		}

		// Token: 0x170020BE RID: 8382
		// (get) Token: 0x06005D95 RID: 23957 RVA: 0x0017DF09 File Offset: 0x0017C109
		// (set) Token: 0x06005D96 RID: 23958 RVA: 0x0017DF11 File Offset: 0x0017C111
		internal RunningValueInfoList RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
			set
			{
				this.m_runningValues = value;
			}
		}

		// Token: 0x170020BF RID: 8383
		// (get) Token: 0x06005D97 RID: 23959 RVA: 0x0017DF1A File Offset: 0x0017C11A
		// (set) Token: 0x06005D98 RID: 23960 RVA: 0x0017DF22 File Offset: 0x0017C122
		internal bool FirstInstance
		{
			get
			{
				return this.m_firstInstance;
			}
			set
			{
				this.m_firstInstance = value;
			}
		}

		// Token: 0x170020C0 RID: 8384
		// (set) Token: 0x06005D99 RID: 23961 RVA: 0x0017DF2B File Offset: 0x0017C12B
		internal string LinkToChild
		{
			set
			{
				this.m_linkToChildName = value;
			}
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x0017DF34 File Offset: 0x0017C134
		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x0017DF3C File Offset: 0x0017C13C
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_runningValues != null);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x0017DF65 File Offset: 0x0017C165
		internal void AddReportItem(ReportItem reportItem)
		{
			Global.Tracer.Assert(this.m_unpopulated);
			Global.Tracer.Assert(reportItem != null);
			Global.Tracer.Assert(this.m_entries != null);
			this.m_entries.Add(reportItem);
		}

		// Token: 0x06005D9D RID: 23965 RVA: 0x0017DFA8 File Offset: 0x0017C1A8
		internal void AddCustomRenderItem(ReportItem reportItem)
		{
			Global.Tracer.Assert(reportItem != null);
			this.m_unpopulated = false;
			if (this.m_sortedReportItemList == null)
			{
				this.m_nonComputedReportItems = new ReportItemList();
				this.m_computedReportItems = new ReportItemList();
				this.m_sortedReportItemList = new ReportItemIndexerList();
			}
			ReportItemIndexer reportItemIndexer = default(ReportItemIndexer);
			if (reportItem.Computed)
			{
				reportItemIndexer.Index = this.m_computedReportItems.Add(reportItem);
			}
			else
			{
				reportItemIndexer.Index = this.m_nonComputedReportItems.Add(reportItem);
			}
			reportItemIndexer.IsComputed = reportItem.Computed;
			this.m_sortedReportItemList.Add(reportItemIndexer);
		}

		// Token: 0x06005D9E RID: 23966 RVA: 0x0017E04A File Offset: 0x0017C24A
		internal bool Initialize(InitializationContext context, bool registerRunningValues)
		{
			return this.Initialize(context, registerRunningValues, null);
		}

		// Token: 0x06005D9F RID: 23967 RVA: 0x0017E058 File Offset: 0x0017C258
		internal bool Initialize(InitializationContext context, bool registerRunningValues, bool[] tableColumnVisiblity)
		{
			Global.Tracer.Assert(this.m_unpopulated);
			if (registerRunningValues)
			{
				context.RegisterRunningValues(this.m_runningValues);
			}
			if ((context.Location & LocationFlags.InPageSection) == (LocationFlags)0)
			{
				context.RegisterPeerScopes(this);
			}
			Global.Tracer.Assert(this.m_entries != null);
			int count = this.m_entries.Count;
			bool flag = true;
			bool flag2 = false;
			SortedReportItemIndexList sortedReportItemIndexList = new SortedReportItemIndexList(count);
			bool flag3 = true;
			bool tableColumnVisible = context.TableColumnVisible;
			for (int i = 0; i < count; i++)
			{
				ReportItem reportItem = this.m_entries[i];
				Global.Tracer.Assert(reportItem != null);
				if (tableColumnVisiblity != null && i < tableColumnVisiblity.Length && tableColumnVisible)
				{
					context.TableColumnVisible = tableColumnVisiblity[i];
				}
				if (!reportItem.Initialize(context))
				{
					flag3 = false;
				}
				if (i == 0 && reportItem.Parent != null)
				{
					if ((context.Location & LocationFlags.InMatrixOrTable) != (LocationFlags)0)
					{
						flag2 = true;
					}
					if (reportItem.Parent.HeightValue < reportItem.Parent.WidthValue)
					{
						flag = false;
					}
				}
				sortedReportItemIndexList.Add(this.m_entries, i, flag);
			}
			if (registerRunningValues)
			{
				context.UnRegisterRunningValues(this.m_runningValues);
			}
			if (count > 1 && !flag2)
			{
				this.RegisterOverlappingItems(context, count, sortedReportItemIndexList, flag);
			}
			return flag3;
		}

		// Token: 0x06005DA0 RID: 23968 RVA: 0x0017E198 File Offset: 0x0017C398
		private void RegisterOverlappingItems(InitializationContext context, int count, SortedReportItemIndexList sortedTop, bool isSortedVertically)
		{
			Hashtable hashtable = new Hashtable(count);
			for (int i = 0; i < count - 1; i++)
			{
				int num = sortedTop[i];
				double num2 = (isSortedVertically ? this.m_entries[num].AbsoluteBottomValue : this.m_entries[num].AbsoluteRightValue);
				bool flag = true;
				int num3 = i + 1;
				while (num3 < count && flag)
				{
					int num4 = sortedTop[num3];
					Global.Tracer.Assert(num != num4, "(currentIndex != peerIndex)");
					double num5 = (isSortedVertically ? this.m_entries[num4].AbsoluteTopValue : this.m_entries[num4].AbsoluteLeftValue);
					if (num2 > num5)
					{
						int num6 = Math.Min(num, num4);
						int num7 = Math.Max(num, num4);
						IntList intList = hashtable[num6] as IntList;
						if (intList == null)
						{
							intList = new IntList();
							hashtable[num6] = intList;
						}
						intList.Add(num7);
					}
					else
					{
						flag = false;
					}
					num3++;
				}
			}
			foreach (object obj in hashtable.Keys)
			{
				int num8 = (int)obj;
				IntList intList2 = hashtable[num8] as IntList;
				double num9 = (isSortedVertically ? this.m_entries[num8].AbsoluteLeftValue : this.m_entries[num8].AbsoluteTopValue);
				double num10 = (isSortedVertically ? this.m_entries[num8].AbsoluteRightValue : this.m_entries[num8].AbsoluteBottomValue);
				for (int j = 0; j < intList2.Count; j++)
				{
					int num11 = intList2[j];
					double num12 = (isSortedVertically ? this.m_entries[num11].AbsoluteLeftValue : this.m_entries[num11].AbsoluteTopValue);
					double num13 = (isSortedVertically ? this.m_entries[num11].AbsoluteRightValue : this.m_entries[num11].AbsoluteBottomValue);
					if ((num12 > num9 && num12 < num10) || (num13 > num9 && num13 < num10) || (num12 <= num9 && num10 <= num13) || (num9 <= num12 && num13 <= num10))
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsOverlappingReportItems, Severity.Warning, this.m_entries[num8].ObjectType, this.m_entries[num8].Name, null, new string[]
						{
							ErrorContext.GetLocalizedObjectTypeString(this.m_entries[num11].ObjectType),
							this.m_entries[num11].Name
						});
					}
				}
			}
		}

		// Token: 0x06005DA1 RID: 23969 RVA: 0x0017E4A0 File Offset: 0x0017C6A0
		internal void CalculateSizes(InitializationContext context, bool overwrite)
		{
			Global.Tracer.Assert(this.m_unpopulated);
			Global.Tracer.Assert(this.m_entries != null);
			for (int i = 0; i < this.m_entries.Count; i++)
			{
				ReportItem reportItem = this.m_entries[i];
				Global.Tracer.Assert(reportItem != null);
				reportItem.CalculateSizes(context, overwrite);
			}
		}

		// Token: 0x06005DA2 RID: 23970 RVA: 0x0017E50C File Offset: 0x0017C70C
		internal void RegisterReceiver(InitializationContext context)
		{
			Global.Tracer.Assert(this.m_unpopulated);
			Global.Tracer.Assert(this.m_entries != null);
			for (int i = 0; i < this.m_entries.Count; i++)
			{
				ReportItem reportItem = this.m_entries[i];
				Global.Tracer.Assert(reportItem != null);
				reportItem.RegisterReceiver(context);
			}
		}

		// Token: 0x06005DA3 RID: 23971 RVA: 0x0017E574 File Offset: 0x0017C774
		internal void MarkChildrenComputed()
		{
			Global.Tracer.Assert(this.m_unpopulated);
			Global.Tracer.Assert(this.m_entries != null);
			for (int i = 0; i < this.m_entries.Count; i++)
			{
				ReportItem reportItem = this.m_entries[i];
				Global.Tracer.Assert(reportItem != null);
				if (reportItem is TextBox)
				{
					reportItem.Computed = true;
				}
			}
		}

		// Token: 0x06005DA4 RID: 23972 RVA: 0x0017E5E4 File Offset: 0x0017C7E4
		internal void Populate(ErrorContext errorContext)
		{
			Global.Tracer.Assert(this.m_unpopulated);
			Global.Tracer.Assert(this.m_entries != null);
			Hashtable hashtable = new Hashtable();
			int num = -1;
			if (0 < this.m_entries.Count)
			{
				if (this.m_normal)
				{
					this.m_entries.Sort();
				}
				this.m_nonComputedReportItems = new ReportItemList();
				this.m_computedReportItems = new ReportItemList();
				this.m_sortedReportItemList = new ReportItemIndexerList();
				for (int i = 0; i < this.m_entries.Count; i++)
				{
					ReportItem reportItem = this.m_entries[i];
					Global.Tracer.Assert(reportItem != null);
					if (reportItem is DataRegion)
					{
						hashtable[reportItem.Name] = reportItem;
					}
					ReportItemIndexer reportItemIndexer = default(ReportItemIndexer);
					if (reportItem.Computed)
					{
						reportItemIndexer.Index = this.m_computedReportItems.Add(reportItem);
					}
					else
					{
						reportItemIndexer.Index = this.m_nonComputedReportItems.Add(reportItem);
					}
					reportItemIndexer.IsComputed = reportItem.Computed;
					this.m_sortedReportItemList.Add(reportItemIndexer);
				}
			}
			this.m_unpopulated = false;
			this.m_entries = null;
			for (int j = 0; j < this.Count; j++)
			{
				ReportItem reportItem2 = this[j];
				Global.Tracer.Assert(reportItem2 != null);
				if (reportItem2.RepeatWith != null)
				{
					if (reportItem2 is DataRegion || reportItem2 is SubReport || (reportItem2 is Rectangle && ((Rectangle)reportItem2).ContainsDataRegionOrSubReport()))
					{
						errorContext.Register(ProcessingErrorCode.rsInvalidRepeatWith, Severity.Error, reportItem2.ObjectType, reportItem2.Name, "RepeatWith", Array.Empty<string>());
					}
					if (!this.m_normal || !hashtable.ContainsKey(reportItem2.RepeatWith))
					{
						errorContext.Register(ProcessingErrorCode.rsRepeatWithNotPeerDataRegion, Severity.Error, reportItem2.ObjectType, reportItem2.Name, "RepeatWith", new string[] { reportItem2.RepeatWith });
					}
					DataRegion dataRegion = (DataRegion)hashtable[reportItem2.RepeatWith];
					if (dataRegion != null)
					{
						if (dataRegion.RepeatSiblings == null)
						{
							dataRegion.RepeatSiblings = new IntList();
						}
						dataRegion.RepeatSiblings.Add(j);
					}
				}
				if (this.m_linkToChildName != null && num < 0 && reportItem2.Name.Equals(this.m_linkToChildName, StringComparison.Ordinal))
				{
					num = j;
					((Rectangle)reportItem2.Parent).LinkToChild = j;
				}
			}
		}

		// Token: 0x06005DA5 RID: 23973 RVA: 0x0017E860 File Offset: 0x0017CA60
		internal bool IsReportItemComputed(int index)
		{
			Global.Tracer.Assert(!this.m_unpopulated);
			Global.Tracer.Assert(0 <= index);
			return this.m_sortedReportItemList[index].IsComputed;
		}

		// Token: 0x06005DA6 RID: 23974 RVA: 0x0017E897 File Offset: 0x0017CA97
		internal ReportItem GetUnsortedReportItem(int index, bool computed)
		{
			Global.Tracer.Assert(!this.m_unpopulated);
			Global.Tracer.Assert(0 <= index);
			return this.InternalGet(index, computed);
		}

		// Token: 0x06005DA7 RID: 23975 RVA: 0x0017E8C8 File Offset: 0x0017CAC8
		internal void GetReportItem(int index, out bool computed, out int internalIndex, out ReportItem reportItem)
		{
			Global.Tracer.Assert(!this.m_unpopulated);
			computed = false;
			internalIndex = -1;
			reportItem = null;
			if (this.m_sortedReportItemList != null && 0 <= index && index < this.m_sortedReportItemList.Count)
			{
				ReportItemIndexer reportItemIndexer = this.m_sortedReportItemList[index];
				if (0 <= reportItemIndexer.Index)
				{
					computed = reportItemIndexer.IsComputed;
					internalIndex = reportItemIndexer.Index;
					reportItem = this.InternalGet(internalIndex, computed);
				}
			}
		}

		// Token: 0x06005DA8 RID: 23976 RVA: 0x0017E940 File Offset: 0x0017CB40
		private ReportItem InternalGet(int index, bool computed)
		{
			Global.Tracer.Assert(this.m_computedReportItems != null);
			Global.Tracer.Assert(this.m_nonComputedReportItems != null);
			if (computed)
			{
				return this.m_computedReportItems[index];
			}
			return this.m_nonComputedReportItems[index];
		}

		// Token: 0x06005DA9 RID: 23977 RVA: 0x0017E990 File Offset: 0x0017CB90
		internal void ProcessDrillthroughAction(ReportProcessing.ProcessingContext processingContext, NonComputedUniqueNames[] nonCompNames)
		{
			if (nonCompNames == null)
			{
				return;
			}
			if (this.m_nonComputedReportItems == null || this.m_nonComputedReportItems.Count == 0)
			{
				return;
			}
			for (int i = 0; i < this.m_nonComputedReportItems.Count; i++)
			{
				NonComputedUniqueNames nonComputedUniqueNames = nonCompNames[i];
				this.m_nonComputedReportItems[i].ProcessDrillthroughAction(processingContext, nonComputedUniqueNames);
			}
		}

		// Token: 0x06005DAA RID: 23978 RVA: 0x0017E9E8 File Offset: 0x0017CBE8
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.IDOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.NonComputedReportItems, ObjectType.ReportItemList),
				new MemberInfo(MemberName.ComputedReportItems, ObjectType.ReportItemList),
				new MemberInfo(MemberName.SortedReportItems, ObjectType.ReportItemIndexerList),
				new MemberInfo(MemberName.RunningValues, ObjectType.RunningValueInfoList)
			});
		}

		// Token: 0x04002FD9 RID: 12249
		private ReportItemList m_nonComputedReportItems;

		// Token: 0x04002FDA RID: 12250
		private ReportItemList m_computedReportItems;

		// Token: 0x04002FDB RID: 12251
		private ReportItemIndexerList m_sortedReportItemList;

		// Token: 0x04002FDC RID: 12252
		private RunningValueInfoList m_runningValues;

		// Token: 0x04002FDD RID: 12253
		[NonSerialized]
		private bool m_normal;

		// Token: 0x04002FDE RID: 12254
		[NonSerialized]
		private bool m_unpopulated;

		// Token: 0x04002FDF RID: 12255
		[NonSerialized]
		private ReportItemList m_entries;

		// Token: 0x04002FE0 RID: 12256
		[NonSerialized]
		private string m_linkToChildName;

		// Token: 0x04002FE1 RID: 12257
		[NonSerialized]
		private bool m_firstInstance = true;
	}
}
