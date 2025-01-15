using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004FD RID: 1277
	[Serializable]
	internal sealed class ReportItemCollection : IDOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IStaticReferenceable, IEnumerable<ReportItem>, IEnumerable
	{
		// Token: 0x060041FE RID: 16894 RVA: 0x00115557 File Offset: 0x00113757
		internal ReportItemCollection()
		{
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x00115571 File Offset: 0x00113771
		internal ReportItemCollection(int id, bool normal)
			: base(id)
		{
			this.m_normal = normal;
			this.m_unpopulated = true;
			this.m_entries = new List<ReportItem>();
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x001155A5 File Offset: 0x001137A5
		public IEnumerator<ReportItem> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x001155B4 File Offset: 0x001137B4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17001BC7 RID: 7111
		internal ReportItem this[int index]
		{
			get
			{
				if (this.m_unpopulated)
				{
					Global.Tracer.Assert(this.m_entries != null, "(null != m_entries)");
					return this.m_entries[index];
				}
				bool flag;
				int num;
				ReportItem reportItem;
				this.GetReportItem(index, out flag, out num, out reportItem);
				return reportItem;
			}
		}

		// Token: 0x17001BC8 RID: 7112
		// (get) Token: 0x06004203 RID: 16899 RVA: 0x00115604 File Offset: 0x00113804
		internal int Count
		{
			get
			{
				if (this.m_unpopulated)
				{
					Global.Tracer.Assert(this.m_entries != null, "(null != m_entries)");
					return this.m_entries.Count;
				}
				if (this.m_sortedReportItemList == null)
				{
					return 0;
				}
				return this.m_sortedReportItemList.Count;
			}
		}

		// Token: 0x17001BC9 RID: 7113
		// (get) Token: 0x06004204 RID: 16900 RVA: 0x00115652 File Offset: 0x00113852
		// (set) Token: 0x06004205 RID: 16901 RVA: 0x00115672 File Offset: 0x00113872
		internal List<ReportItem> ComputedReportItems
		{
			get
			{
				Global.Tracer.Assert(!this.m_unpopulated, "(!m_unpopulated)");
				return this.m_computedReportItems;
			}
			set
			{
				this.m_computedReportItems = value;
			}
		}

		// Token: 0x17001BCA RID: 7114
		// (get) Token: 0x06004206 RID: 16902 RVA: 0x0011567B File Offset: 0x0011387B
		// (set) Token: 0x06004207 RID: 16903 RVA: 0x0011569B File Offset: 0x0011389B
		internal List<ReportItem> NonComputedReportItems
		{
			get
			{
				Global.Tracer.Assert(!this.m_unpopulated, "(!m_unpopulated)");
				return this.m_nonComputedReportItems;
			}
			set
			{
				this.m_nonComputedReportItems = value;
			}
		}

		// Token: 0x17001BCB RID: 7115
		// (get) Token: 0x06004208 RID: 16904 RVA: 0x001156A4 File Offset: 0x001138A4
		// (set) Token: 0x06004209 RID: 16905 RVA: 0x001156C4 File Offset: 0x001138C4
		internal List<ReportItemIndexer> SortedReportItems
		{
			get
			{
				Global.Tracer.Assert(!this.m_unpopulated, "(!m_unpopulated)");
				return this.m_sortedReportItemList;
			}
			set
			{
				this.m_sortedReportItemList = value;
			}
		}

		// Token: 0x17001BCC RID: 7116
		// (get) Token: 0x0600420A RID: 16906 RVA: 0x001156CD File Offset: 0x001138CD
		internal List<int> ROMIndexMap
		{
			get
			{
				return this.m_romIndexMap;
			}
		}

		// Token: 0x17001BCD RID: 7117
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x001156D5 File Offset: 0x001138D5
		// (set) Token: 0x0600420C RID: 16908 RVA: 0x001156DD File Offset: 0x001138DD
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

		// Token: 0x17001BCE RID: 7118
		// (set) Token: 0x0600420D RID: 16909 RVA: 0x001156E6 File Offset: 0x001138E6
		internal string LinkToChild
		{
			set
			{
				this.m_linkToChildName = value;
			}
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x001156F0 File Offset: 0x001138F0
		internal void AddReportItem(ReportItem reportItem)
		{
			Global.Tracer.Assert(this.m_unpopulated, "(m_unpopulated)");
			Global.Tracer.Assert(reportItem != null, "(null != reportItem)");
			Global.Tracer.Assert(this.m_entries != null, "(null != m_entries)");
			this.m_entries.Add(reportItem);
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x0011574C File Offset: 0x0011394C
		internal void AddCustomRenderItem(ReportItem reportItem)
		{
			Global.Tracer.Assert(reportItem != null, "(null != reportItem)");
			this.m_unpopulated = false;
			if (this.m_sortedReportItemList == null)
			{
				this.m_nonComputedReportItems = new List<ReportItem>();
				this.m_computedReportItems = new List<ReportItem>();
				this.m_sortedReportItemList = new List<ReportItemIndexer>();
			}
			ReportItemIndexer reportItemIndexer = default(ReportItemIndexer);
			if (reportItem.Computed)
			{
				this.m_computedReportItems.Add(reportItem);
				reportItemIndexer.Index = this.m_computedReportItems.Count - 1;
			}
			else
			{
				this.m_nonComputedReportItems.Add(reportItem);
				reportItemIndexer.Index = this.m_nonComputedReportItems.Count - 1;
			}
			reportItemIndexer.IsComputed = reportItem.Computed;
			this.m_sortedReportItemList.Add(reportItemIndexer);
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x00115808 File Offset: 0x00113A08
		internal bool Initialize(InitializationContext context)
		{
			Global.Tracer.Assert(this.m_unpopulated, "(m_unpopulated)");
			if ((context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				context.RegisterPeerScopes(this);
			}
			Global.Tracer.Assert(this.m_entries != null, "(null != m_entries)");
			int count = this.m_entries.Count;
			bool flag = true;
			bool flag2 = false;
			SortedReportItemIndexList sortedReportItemIndexList = new SortedReportItemIndexList(count);
			bool flag3 = true;
			for (int i = 0; i < count; i++)
			{
				ReportItem reportItem = this.m_entries[i];
				Global.Tracer.Assert(reportItem != null, "(null != item)");
				if (!reportItem.Initialize(context))
				{
					flag3 = false;
				}
				if (i == 0 && reportItem.Parent != null)
				{
					if ((context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablix) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
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
			if (count > 1 && !flag2 && !context.PublishingContext.IsRdlx)
			{
				this.RegisterOverlappingItems(context, count, sortedReportItemIndexList, flag);
			}
			return flag3;
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x00115920 File Offset: 0x00113B20
		internal void InitializeRVDirectionDependentItems(InitializationContext context)
		{
			for (int i = 0; i < this.m_entries.Count; i++)
			{
				this.m_entries[i].InitializeRVDirectionDependentItems(context);
			}
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x00115958 File Offset: 0x00113B58
		internal void DetermineGroupingExprValueCount(InitializationContext context, int groupingExprCount)
		{
			for (int i = 0; i < this.m_entries.Count; i++)
			{
				this.m_entries[i].DetermineGroupingExprValueCount(context, groupingExprCount);
			}
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x00115990 File Offset: 0x00113B90
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
						List<int> list = hashtable[num6] as List<int>;
						if (list == null)
						{
							list = new List<int>();
							hashtable[num6] = list;
						}
						list.Add(num7);
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
				List<int> list2 = hashtable[num8] as List<int>;
				double num9 = (isSortedVertically ? this.m_entries[num8].AbsoluteLeftValue : this.m_entries[num8].AbsoluteTopValue);
				double num10 = (isSortedVertically ? this.m_entries[num8].AbsoluteRightValue : this.m_entries[num8].AbsoluteBottomValue);
				for (int j = 0; j < list2.Count; j++)
				{
					int num11 = list2[j];
					double num12 = (isSortedVertically ? this.m_entries[num11].AbsoluteLeftValue : this.m_entries[num11].AbsoluteTopValue);
					double num13 = (isSortedVertically ? this.m_entries[num11].AbsoluteRightValue : this.m_entries[num11].AbsoluteBottomValue);
					if (((num12 > num9 && num12 < num10) || (num13 > num9 && num13 < num10) || (num12 <= num9 && num10 <= num13) || (num9 <= num12 && num13 <= num10)) && (this.m_entries[num8].ObjectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem || ((CustomReportItem)this.m_entries[num8]).AltReportItem != this.m_entries[num11]) && (this.m_entries[num11].ObjectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem || ((CustomReportItem)this.m_entries[num11]).AltReportItem != this.m_entries[num8]))
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

		// Token: 0x06004214 RID: 16916 RVA: 0x00115D14 File Offset: 0x00113F14
		internal void CalculateSizes(InitializationContext context, bool overwrite)
		{
			Global.Tracer.Assert(this.m_unpopulated, "(m_unpopulated)");
			Global.Tracer.Assert(this.m_entries != null, "(null != m_entries)");
			for (int i = 0; i < this.m_entries.Count; i++)
			{
				ReportItem reportItem = this.m_entries[i];
				Global.Tracer.Assert(reportItem != null, "(null != item)");
				reportItem.CalculateSizes(context, overwrite);
			}
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x00115D8C File Offset: 0x00113F8C
		internal void MarkChildrenComputed()
		{
			Global.Tracer.Assert(this.m_unpopulated, "(m_unpopulated)");
			Global.Tracer.Assert(this.m_entries != null, "(null != m_entries)");
			for (int i = 0; i < this.m_entries.Count; i++)
			{
				ReportItem reportItem = this.m_entries[i];
				Global.Tracer.Assert(reportItem != null, "(null != item)");
				if (reportItem is TextBox)
				{
					reportItem.Computed = true;
				}
			}
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x00115E0C File Offset: 0x0011400C
		internal void Populate(ErrorContext errorContext)
		{
			Global.Tracer.Assert(this.m_unpopulated, "(m_unpopulated)");
			Global.Tracer.Assert(this.m_entries != null, "(null != m_entries)");
			Hashtable hashtable = new Hashtable();
			int num = -1;
			if (0 < this.m_entries.Count)
			{
				if (this.m_normal)
				{
					this.m_entries.Sort();
				}
				this.m_nonComputedReportItems = new List<ReportItem>();
				this.m_computedReportItems = new List<ReportItem>();
				this.m_sortedReportItemList = new List<ReportItemIndexer>();
				List<CustomReportItem> list = new List<CustomReportItem>();
				for (int i = 0; i < this.m_entries.Count; i++)
				{
					ReportItem reportItem = this.m_entries[i];
					Global.Tracer.Assert(reportItem != null, "(null != item)");
					if (reportItem.IsDataRegion)
					{
						hashtable[reportItem.Name] = reportItem;
					}
					if (reportItem.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem)
					{
						list.Add((CustomReportItem)reportItem);
					}
					ReportItemIndexer reportItemIndexer = default(ReportItemIndexer);
					if (reportItem.Computed)
					{
						this.m_computedReportItems.Add(reportItem);
						reportItemIndexer.Index = this.m_computedReportItems.Count - 1;
					}
					else
					{
						this.m_nonComputedReportItems.Add(reportItem);
						reportItemIndexer.Index = this.m_nonComputedReportItems.Count - 1;
					}
					reportItemIndexer.IsComputed = reportItem.Computed;
					this.m_sortedReportItemList.Add(reportItemIndexer);
				}
				if (list.Count > 0)
				{
					bool[] array = new bool[this.m_sortedReportItemList.Count];
					foreach (CustomReportItem customReportItem in list)
					{
						int num2 = this.m_entries.IndexOf(customReportItem.AltReportItem);
						customReportItem.AltReportItemIndexInParentCollectionDef = num2;
						array[num2] = true;
					}
					this.m_romIndexMap = new List<int>(this.m_sortedReportItemList.Count - list.Count);
					for (int j = 0; j < this.m_sortedReportItemList.Count; j++)
					{
						if (!array[j])
						{
							this.m_romIndexMap.Add(j);
						}
					}
					Global.Tracer.Assert(this.m_romIndexMap.Count + list.Count == this.m_sortedReportItemList.Count);
				}
			}
			this.m_unpopulated = false;
			this.m_entries = null;
			for (int k = 0; k < this.Count; k++)
			{
				ReportItem reportItem2 = this[k];
				Global.Tracer.Assert(reportItem2 != null, "(null != item)");
				if (reportItem2.RepeatWith != null)
				{
					if (reportItem2.IsDataRegion || reportItem2 is SubReport || (reportItem2 is Rectangle && ((Rectangle)reportItem2).ContainsDataRegionOrSubReport()))
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
							dataRegion.RepeatSiblings = new List<int>();
						}
						dataRegion.RepeatSiblings.Add((this.m_romIndexMap == null) ? k : this.m_romIndexMap.IndexOf(k));
					}
				}
				if (this.m_linkToChildName != null && num < 0 && reportItem2.Name.Equals(this.m_linkToChildName, StringComparison.Ordinal))
				{
					num = k;
					((Rectangle)reportItem2.Parent).LinkToChild = k;
				}
			}
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x001161DC File Offset: 0x001143DC
		internal bool IsReportItemComputed(int index)
		{
			Global.Tracer.Assert(!this.m_unpopulated, "(!m_unpopulated)");
			Global.Tracer.Assert(0 <= index, "(0 <= index)");
			return this.m_sortedReportItemList[index].IsComputed;
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x00116228 File Offset: 0x00114428
		internal ReportItem GetUnsortedReportItem(int index, bool computed)
		{
			Global.Tracer.Assert(!this.m_unpopulated, "(!m_unpopulated)");
			Global.Tracer.Assert(0 <= index, "(0 <= index)");
			return this.InternalGet(index, computed);
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x00116260 File Offset: 0x00114460
		internal void GetReportItem(int index, out bool computed, out int internalIndex, out ReportItem reportItem)
		{
			Global.Tracer.Assert(!this.m_unpopulated, "(!m_unpopulated)");
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

		// Token: 0x0600421A RID: 16922 RVA: 0x001162DC File Offset: 0x001144DC
		private ReportItem InternalGet(int index, bool computed)
		{
			Global.Tracer.Assert(this.m_computedReportItems != null, "(null != m_computedReportItems)");
			Global.Tracer.Assert(this.m_nonComputedReportItems != null, "(null != m_nonComputedReportItems)");
			if (computed)
			{
				return this.m_computedReportItems[index];
			}
			return this.m_nonComputedReportItems[index];
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x00116338 File Offset: 0x00114538
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ReportItemCollection reportItemCollection = (ReportItemCollection)base.PublishClone(context);
			context.AddReportItemCollection(reportItemCollection);
			if (this.m_entries != null)
			{
				CustomReportItem customReportItem = null;
				reportItemCollection.m_entries = new List<ReportItem>();
				foreach (ReportItem reportItem in this.m_entries)
				{
					ReportItem reportItem2 = (ReportItem)reportItem.PublishClone(context);
					reportItemCollection.m_entries.Add(reportItem2);
					if (reportItem2 is CustomReportItem)
					{
						Global.Tracer.Assert(customReportItem == null, "(lastCriPublishClone == null)");
						customReportItem = (CustomReportItem)reportItem2;
					}
					else if (customReportItem != null)
					{
						customReportItem.AltReportItem = reportItem2;
						customReportItem = null;
					}
				}
				Global.Tracer.Assert(customReportItem == null, "(lastCriPublishClone == null)");
			}
			if (this.m_linkToChildName != null)
			{
				reportItemCollection.m_linkToChildName = context.GetNewReportItemName(this.m_linkToChildName);
			}
			return reportItemCollection;
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x00116428 File Offset: 0x00114628
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemCollection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, new List<MemberInfo>
			{
				new MemberInfo(MemberName.NonComputedReportItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem),
				new MemberInfo(MemberName.ComputedReportItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem),
				new MemberInfo(MemberName.SortedReportItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemIndexer),
				new MemberInfo(MemberName.ROMIndexMap, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32)
			});
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x001164A4 File Offset: 0x001146A4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ReportItemCollection.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.NonComputedReportItems:
					writer.Write<ReportItem>(this.m_nonComputedReportItems);
					break;
				case MemberName.ComputedReportItems:
					writer.Write<ReportItem>(this.m_computedReportItems);
					break;
				case MemberName.SortedReportItems:
					writer.Write<ReportItemIndexer>(this.m_sortedReportItemList);
					break;
				default:
					if (memberName != MemberName.ROMIndexMap)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.WriteListOfPrimitives<int>(this.m_romIndexMap);
					}
					break;
				}
			}
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x00116548 File Offset: 0x00114748
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ReportItemCollection.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.NonComputedReportItems:
					this.m_nonComputedReportItems = reader.ReadGenericListOfRIFObjects<ReportItem>();
					break;
				case MemberName.ComputedReportItems:
					this.m_computedReportItems = reader.ReadGenericListOfRIFObjects<ReportItem>();
					break;
				case MemberName.SortedReportItems:
					this.m_sortedReportItemList = reader.ReadGenericListOfRIFObjects<ReportItemIndexer>();
					break;
				default:
					if (memberName != MemberName.ROMIndexMap)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_romIndexMap = reader.ReadListOfPrimitives<int>();
					}
					break;
				}
			}
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x001165E9 File Offset: 0x001147E9
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x001165F6 File Offset: 0x001147F6
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemCollection;
		}

		// Token: 0x17001BCF RID: 7119
		// (get) Token: 0x06004221 RID: 16929 RVA: 0x001165FD File Offset: 0x001147FD
		int IStaticReferenceable.ID
		{
			get
			{
				return this.m_staticRefId;
			}
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x00116605 File Offset: 0x00114805
		void IStaticReferenceable.SetID(int id)
		{
			this.m_staticRefId = id;
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x0011660E File Offset: 0x0011480E
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType IStaticReferenceable.GetObjectType()
		{
			return this.GetObjectType();
		}

		// Token: 0x04001E31 RID: 7729
		private List<ReportItem> m_nonComputedReportItems;

		// Token: 0x04001E32 RID: 7730
		private List<ReportItem> m_computedReportItems;

		// Token: 0x04001E33 RID: 7731
		private List<ReportItemIndexer> m_sortedReportItemList;

		// Token: 0x04001E34 RID: 7732
		private List<int> m_romIndexMap;

		// Token: 0x04001E35 RID: 7733
		[NonSerialized]
		private bool m_normal;

		// Token: 0x04001E36 RID: 7734
		[NonSerialized]
		private bool m_unpopulated;

		// Token: 0x04001E37 RID: 7735
		[NonSerialized]
		private List<ReportItem> m_entries;

		// Token: 0x04001E38 RID: 7736
		[NonSerialized]
		private string m_linkToChildName;

		// Token: 0x04001E39 RID: 7737
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ReportItemCollection.GetDeclaration();

		// Token: 0x04001E3A RID: 7738
		[NonSerialized]
		private bool m_firstInstance = true;

		// Token: 0x04001E3B RID: 7739
		[NonSerialized]
		private int m_staticRefId = int.MinValue;
	}
}
