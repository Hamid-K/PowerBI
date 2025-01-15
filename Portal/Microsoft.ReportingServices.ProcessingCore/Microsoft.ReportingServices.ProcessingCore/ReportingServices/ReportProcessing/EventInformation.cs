using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005FE RID: 1534
	[Serializable]
	public sealed class EventInformation
	{
		// Token: 0x06005477 RID: 21623 RVA: 0x001628C0 File Offset: 0x00160AC0
		public EventInformation()
		{
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x001628C8 File Offset: 0x00160AC8
		public EventInformation(EventInformation copy)
		{
			Global.Tracer.Assert(copy != null, "(null != copy)");
			this.m_hasShowHideInfo = copy.m_hasShowHideInfo;
			if (copy.m_toggleStateInfo != null)
			{
				this.m_toggleStateInfo = (Hashtable)copy.m_toggleStateInfo.Clone();
			}
			if (copy.m_hiddenInfo != null)
			{
				this.m_hiddenInfo = (Hashtable)copy.m_hiddenInfo.Clone();
			}
			this.m_hasSortInfo = copy.m_hasSortInfo;
			if (copy.m_sortInfo != null)
			{
				this.m_sortInfo = copy.m_sortInfo.Clone();
			}
			else if (copy.m_odpSortInfo != null)
			{
				this.m_odpSortInfo = copy.m_odpSortInfo.Clone();
			}
			if (copy.m_rendererEventInformation != null)
			{
				this.m_rendererEventInformation = new Dictionary<string, EventInformation.RendererEventInformation>(copy.m_rendererEventInformation.Count);
				foreach (string text in copy.m_rendererEventInformation.Keys)
				{
					EventInformation.RendererEventInformation rendererEventInformation = new EventInformation.RendererEventInformation(copy.m_rendererEventInformation[text]);
					this.m_rendererEventInformation[text] = rendererEventInformation;
				}
			}
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x001629F8 File Offset: 0x00160BF8
		public byte[] Serialize()
		{
			Global.Tracer.Assert(this.m_hasShowHideInfo || this.m_hasSortInfo || this.m_rendererEventInformation != null, "(m_hasShowHideInfo || m_hasSortInfo || m_rendererEventInformation != null)");
			MemoryStream memoryStream = null;
			byte[] array;
			try
			{
				if (this.m_hasShowHideInfo)
				{
					Global.Tracer.Assert(this.m_toggleStateInfo != null, "(null != m_toggleStateInfo)");
				}
				if (this.m_hasSortInfo)
				{
					Global.Tracer.Assert(this.m_sortInfo != null || this.m_odpSortInfo != null, "(null != m_sortInfo || null != m_odpSortInfo)");
				}
				memoryStream = new MemoryStream();
				new BinaryFormatter().Serialize(memoryStream, this);
				array = memoryStream.ToArray();
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			return array;
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x00162AB4 File Offset: 0x00160CB4
		public static EventInformation Deserialize(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			MemoryStream stream = null;
			EventInformation result = null;
			try
			{
				stream = new MemoryStream(data, false);
				BinaryFormatter bFormatter = new BinaryFormatter();
				RevertImpersonationContext.Run(delegate
				{
					EventInformation eventInformation = (EventInformation)bFormatter.Deserialize(stream);
					eventInformation.m_changed = false;
					result = eventInformation;
				});
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			return result;
		}

		// Token: 0x17001F11 RID: 7953
		// (get) Token: 0x0600547B RID: 21627 RVA: 0x00162B30 File Offset: 0x00160D30
		// (set) Token: 0x0600547C RID: 21628 RVA: 0x00162B38 File Offset: 0x00160D38
		public Hashtable ToggleStateInfo
		{
			get
			{
				return this.m_toggleStateInfo;
			}
			set
			{
				this.m_hasShowHideInfo = value != null;
				this.m_toggleStateInfo = value;
			}
		}

		// Token: 0x17001F12 RID: 7954
		// (get) Token: 0x0600547D RID: 21629 RVA: 0x00162B4B File Offset: 0x00160D4B
		// (set) Token: 0x0600547E RID: 21630 RVA: 0x00162B53 File Offset: 0x00160D53
		internal Hashtable HiddenInfo
		{
			get
			{
				return this.m_hiddenInfo;
			}
			set
			{
				this.m_hiddenInfo = value;
			}
		}

		// Token: 0x17001F13 RID: 7955
		// (get) Token: 0x0600547F RID: 21631 RVA: 0x00162B5C File Offset: 0x00160D5C
		// (set) Token: 0x06005480 RID: 21632 RVA: 0x00162B64 File Offset: 0x00160D64
		internal EventInformation.SortEventInfo SortInfo
		{
			get
			{
				return this.m_sortInfo;
			}
			set
			{
				this.m_hasSortInfo = value != null;
				this.m_sortInfo = value;
			}
		}

		// Token: 0x17001F14 RID: 7956
		// (get) Token: 0x06005481 RID: 21633 RVA: 0x00162B77 File Offset: 0x00160D77
		// (set) Token: 0x06005482 RID: 21634 RVA: 0x00162B7F File Offset: 0x00160D7F
		public EventInformation.OdpSortEventInfo OdpSortInfo
		{
			get
			{
				return this.m_odpSortInfo;
			}
			set
			{
				this.m_hasSortInfo = value != null;
				this.m_odpSortInfo = value;
			}
		}

		// Token: 0x17001F15 RID: 7957
		// (get) Token: 0x06005483 RID: 21635 RVA: 0x00162B92 File Offset: 0x00160D92
		// (set) Token: 0x06005484 RID: 21636 RVA: 0x00162B9A File Offset: 0x00160D9A
		public bool Changed
		{
			get
			{
				return this.m_changed;
			}
			set
			{
				this.m_changed = value;
			}
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x00162BA4 File Offset: 0x00160DA4
		internal EventInformation.RendererEventInformation GetRendererEventInformation(string aRenderFormat)
		{
			if (this.m_rendererEventInformation == null)
			{
				this.m_rendererEventInformation = new Dictionary<string, EventInformation.RendererEventInformation>();
			}
			EventInformation.RendererEventInformation rendererEventInformation = null;
			if (!this.m_rendererEventInformation.TryGetValue(aRenderFormat, out rendererEventInformation))
			{
				rendererEventInformation = new EventInformation.RendererEventInformation();
				this.m_rendererEventInformation[aRenderFormat] = rendererEventInformation;
			}
			return rendererEventInformation;
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x00162BEC File Offset: 0x00160DEC
		internal bool ValidToggleSender(string senderId)
		{
			if (this.m_rendererEventInformation == null)
			{
				return false;
			}
			foreach (string text in this.m_rendererEventInformation.Keys)
			{
				if (this.m_rendererEventInformation[text].ValidToggleSender(senderId))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x00162C64 File Offset: 0x00160E64
		internal DrillthroughInfo GetDrillthroughInfo(string drillthroughId)
		{
			if (this.m_rendererEventInformation != null)
			{
				foreach (string text in this.m_rendererEventInformation.Keys)
				{
					DrillthroughInfo drillthroughInfo = this.m_rendererEventInformation[text].GetDrillthroughInfo(drillthroughId);
					if (drillthroughInfo != null)
					{
						return drillthroughInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x04002CF2 RID: 11506
		private bool m_hasShowHideInfo;

		// Token: 0x04002CF3 RID: 11507
		private Hashtable m_toggleStateInfo;

		// Token: 0x04002CF4 RID: 11508
		private Hashtable m_hiddenInfo;

		// Token: 0x04002CF5 RID: 11509
		private bool m_hasSortInfo;

		// Token: 0x04002CF6 RID: 11510
		private EventInformation.SortEventInfo m_sortInfo;

		// Token: 0x04002CF7 RID: 11511
		private EventInformation.OdpSortEventInfo m_odpSortInfo;

		// Token: 0x04002CF8 RID: 11512
		private Dictionary<string, EventInformation.RendererEventInformation> m_rendererEventInformation;

		// Token: 0x04002CF9 RID: 11513
		[NonSerialized]
		private bool m_changed;

		// Token: 0x02000C13 RID: 3091
		[Serializable]
		internal class SortEventInfo
		{
			// Token: 0x06008671 RID: 34417 RVA: 0x0021A127 File Offset: 0x00218327
			internal SortEventInfo()
			{
				this.m_collection = new ArrayList();
				this.m_nameMap = new Hashtable();
			}

			// Token: 0x06008672 RID: 34418 RVA: 0x0021A145 File Offset: 0x00218345
			private SortEventInfo(EventInformation.SortEventInfo copy)
			{
				this.m_collection = (ArrayList)copy.m_collection.Clone();
				this.m_nameMap = (Hashtable)copy.m_nameMap.Clone();
			}

			// Token: 0x170029DC RID: 10716
			// (get) Token: 0x06008673 RID: 34419 RVA: 0x0021A179 File Offset: 0x00218379
			internal int Count
			{
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x06008674 RID: 34420 RVA: 0x0021A188 File Offset: 0x00218388
			internal void Add(int uniqueName, bool direction, Hashtable peerSorts)
			{
				this.Remove(uniqueName);
				this.m_nameMap.Add(uniqueName, this.m_collection.Count);
				this.m_collection.Add(new EventInformation.SortEventInfo.SortInfoStruct(uniqueName, direction, peerSorts));
			}

			// Token: 0x06008675 RID: 34421 RVA: 0x0021A1D8 File Offset: 0x002183D8
			internal bool Remove(int uniqueName)
			{
				object obj = this.m_nameMap[uniqueName];
				if (obj != null)
				{
					this.m_nameMap.Remove(uniqueName);
					this.m_collection.RemoveAt((int)obj);
					for (int i = (int)obj; i < this.m_collection.Count; i++)
					{
						EventInformation.SortEventInfo.SortInfoStruct sortInfoStruct = (EventInformation.SortEventInfo.SortInfoStruct)this.m_collection[i];
						this.m_nameMap[sortInfoStruct.ReportItemUniqueName] = i;
					}
					return true;
				}
				return false;
			}

			// Token: 0x06008676 RID: 34422 RVA: 0x0021A268 File Offset: 0x00218468
			internal bool ClearPeerSorts(int uniqueName)
			{
				bool flag = false;
				IntList intList = null;
				for (int i = 0; i < this.m_collection.Count; i++)
				{
					EventInformation.SortEventInfo.SortInfoStruct sortInfoStruct = (EventInformation.SortEventInfo.SortInfoStruct)this.m_collection[i];
					Hashtable peerSorts = sortInfoStruct.PeerSorts;
					if (peerSorts != null)
					{
						if (intList == null)
						{
							intList = new IntList();
						}
						if (peerSorts.Contains(uniqueName))
						{
							intList.Add(sortInfoStruct.ReportItemUniqueName);
						}
					}
				}
				if (intList != null)
				{
					if (0 < intList.Count)
					{
						for (int j = 0; j < intList.Count; j++)
						{
							this.Remove(intList[j]);
						}
						flag = true;
					}
				}
				else if (this.m_collection.Count > 0)
				{
					this.m_nameMap.Clear();
					this.m_collection.Clear();
					flag = true;
				}
				return flag;
			}

			// Token: 0x06008677 RID: 34423 RVA: 0x0021A332 File Offset: 0x00218532
			internal int GetUniqueNameAt(int index)
			{
				Global.Tracer.Assert(0 <= index && index < this.m_collection.Count, "(0 <= index && index < m_collection.Count)");
				return ((EventInformation.SortEventInfo.SortInfoStruct)this.m_collection[index]).ReportItemUniqueName;
			}

			// Token: 0x06008678 RID: 34424 RVA: 0x0021A36E File Offset: 0x0021856E
			internal bool GetSortDirectionAt(int index)
			{
				Global.Tracer.Assert(0 <= index && index < this.m_collection.Count, "(0 <= index && index < m_collection.Count)");
				return ((EventInformation.SortEventInfo.SortInfoStruct)this.m_collection[index]).SortDirection;
			}

			// Token: 0x06008679 RID: 34425 RVA: 0x0021A3AC File Offset: 0x002185AC
			internal SortOptions GetSortState(int uniqueName)
			{
				if (this.m_nameMap != null)
				{
					Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
					object obj = this.m_nameMap[uniqueName];
					if (obj != null)
					{
						if (!((EventInformation.SortEventInfo.SortInfoStruct)this.m_collection[(int)obj]).SortDirection)
						{
							return SortOptions.Descending;
						}
						return SortOptions.Ascending;
					}
				}
				return SortOptions.None;
			}

			// Token: 0x0600867A RID: 34426 RVA: 0x0021A410 File Offset: 0x00218610
			internal EventInformation.SortEventInfo Clone()
			{
				return new EventInformation.SortEventInfo(this);
			}

			// Token: 0x04004838 RID: 18488
			private ArrayList m_collection;

			// Token: 0x04004839 RID: 18489
			private Hashtable m_nameMap;

			// Token: 0x02000D3A RID: 3386
			[Serializable]
			private struct SortInfoStruct
			{
				// Token: 0x06008F62 RID: 36706 RVA: 0x00246E4B File Offset: 0x0024504B
				internal SortInfoStruct(int uniqueName, bool direction, Hashtable peerSorts)
				{
					this.ReportItemUniqueName = uniqueName;
					this.SortDirection = direction;
					this.PeerSorts = peerSorts;
				}

				// Token: 0x040050AC RID: 20652
				internal int ReportItemUniqueName;

				// Token: 0x040050AD RID: 20653
				internal bool SortDirection;

				// Token: 0x040050AE RID: 20654
				internal Hashtable PeerSorts;
			}
		}

		// Token: 0x02000C14 RID: 3092
		[Serializable]
		public class OdpSortEventInfo
		{
			// Token: 0x0600867B RID: 34427 RVA: 0x0021A418 File Offset: 0x00218618
			internal OdpSortEventInfo()
			{
				this.m_collection = new ArrayList();
				this.m_uniqueNameMap = new Dictionary<string, int>();
			}

			// Token: 0x0600867C RID: 34428 RVA: 0x0021A438 File Offset: 0x00218638
			private OdpSortEventInfo(EventInformation.OdpSortEventInfo copy)
			{
				this.m_collection = (ArrayList)copy.m_collection.Clone();
				if (copy.m_uniqueNameMap != null)
				{
					this.m_uniqueNameMap = new Dictionary<string, int>(copy.m_uniqueNameMap.Count);
					IDictionaryEnumerator dictionaryEnumerator = copy.m_uniqueNameMap.GetEnumerator();
					while (dictionaryEnumerator.MoveNext())
					{
						if (dictionaryEnumerator.Key != null)
						{
							this.m_uniqueNameMap.Add(dictionaryEnumerator.Key as string, (int)dictionaryEnumerator.Value);
						}
					}
				}
			}

			// Token: 0x170029DD RID: 10717
			// (get) Token: 0x0600867D RID: 34429 RVA: 0x0021A4C3 File Offset: 0x002186C3
			internal int Count
			{
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x0600867E RID: 34430 RVA: 0x0021A4D0 File Offset: 0x002186D0
			internal void Add(string uniqueNameString, bool direction, Hashtable peerSorts)
			{
				this.Remove(uniqueNameString);
				this.m_uniqueNameMap.Add(uniqueNameString, this.m_collection.Count);
				this.m_collection.Add(new EventInformation.OdpSortEventInfo.SortInfoStruct(uniqueNameString, direction, peerSorts));
			}

			// Token: 0x0600867F RID: 34431 RVA: 0x0021A50A File Offset: 0x0021870A
			internal bool Remove(int id, List<InstancePathItem> instancePath)
			{
				return this.Remove(InstancePathItem.GenerateUniqueNameString(id, instancePath));
			}

			// Token: 0x06008680 RID: 34432 RVA: 0x0021A51C File Offset: 0x0021871C
			internal bool Remove(string uniqueNameString)
			{
				int num;
				if (!this.m_uniqueNameMap.TryGetValue(uniqueNameString, out num))
				{
					return false;
				}
				this.m_uniqueNameMap.Remove(uniqueNameString);
				this.m_collection.RemoveAt(num);
				for (int i = num; i < this.m_collection.Count; i++)
				{
					EventInformation.OdpSortEventInfo.SortInfoStruct sortInfoStruct = (EventInformation.OdpSortEventInfo.SortInfoStruct)this.m_collection[i];
					this.m_uniqueNameMap[sortInfoStruct.EventSourceUniqueName] = i;
				}
				return true;
			}

			// Token: 0x06008681 RID: 34433 RVA: 0x0021A590 File Offset: 0x00218790
			internal bool ClearPeerSorts(string uniqueNameString)
			{
				bool flag = false;
				List<string> list = null;
				int num = 0;
				for (int i = 0; i < this.m_collection.Count; i++)
				{
					EventInformation.OdpSortEventInfo.SortInfoStruct sortInfoStruct = (EventInformation.OdpSortEventInfo.SortInfoStruct)this.m_collection[i];
					Hashtable peerSorts = sortInfoStruct.PeerSorts;
					if (peerSorts != null && peerSorts.Count != 0)
					{
						if (list == null)
						{
							list = new List<string>();
						}
						if (peerSorts.Contains(uniqueNameString))
						{
							list.Add(sortInfoStruct.EventSourceUniqueName);
							num++;
						}
					}
				}
				if (num != 0)
				{
					for (int j = 0; j < num; j++)
					{
						this.Remove(list[j]);
					}
					flag = true;
				}
				return flag;
			}

			// Token: 0x06008682 RID: 34434 RVA: 0x0021A62B File Offset: 0x0021882B
			internal string GetUniqueNameAt(int index)
			{
				Global.Tracer.Assert(0 <= index && index < this.m_collection.Count, "(0 <= index && index < m_collection.Count)");
				return ((EventInformation.OdpSortEventInfo.SortInfoStruct)this.m_collection[index]).EventSourceUniqueName;
			}

			// Token: 0x06008683 RID: 34435 RVA: 0x0021A667 File Offset: 0x00218867
			internal bool GetSortDirectionAt(int index)
			{
				Global.Tracer.Assert(0 <= index && index < this.m_collection.Count, "(0 <= index && index < m_collection.Count)");
				return ((EventInformation.OdpSortEventInfo.SortInfoStruct)this.m_collection[index]).SortDirection;
			}

			// Token: 0x06008684 RID: 34436 RVA: 0x0021A6A4 File Offset: 0x002188A4
			public SortOptions GetSortState(string eventSourceUniqueName)
			{
				if (this.m_uniqueNameMap != null)
				{
					Global.Tracer.Assert(this.m_collection != null, "(null != m_collection)");
					int num;
					if (this.m_uniqueNameMap.TryGetValue(eventSourceUniqueName, out num))
					{
						if (!((EventInformation.OdpSortEventInfo.SortInfoStruct)this.m_collection[num]).SortDirection)
						{
							return SortOptions.Descending;
						}
						return SortOptions.Ascending;
					}
				}
				return SortOptions.None;
			}

			// Token: 0x06008685 RID: 34437 RVA: 0x0021A6FE File Offset: 0x002188FE
			internal EventInformation.OdpSortEventInfo Clone()
			{
				return new EventInformation.OdpSortEventInfo(this);
			}

			// Token: 0x0400483A RID: 18490
			private ArrayList m_collection;

			// Token: 0x0400483B RID: 18491
			private Dictionary<string, int> m_uniqueNameMap;

			// Token: 0x02000D3B RID: 3387
			[Serializable]
			private struct SortInfoStruct
			{
				// Token: 0x06008F63 RID: 36707 RVA: 0x00246E62 File Offset: 0x00245062
				internal SortInfoStruct(string uniqueName, bool direction, Hashtable peerSorts)
				{
					this.EventSourceUniqueName = uniqueName;
					this.SortDirection = direction;
					this.PeerSorts = peerSorts;
				}

				// Token: 0x040050AF RID: 20655
				internal string EventSourceUniqueName;

				// Token: 0x040050B0 RID: 20656
				internal bool SortDirection;

				// Token: 0x040050B1 RID: 20657
				internal Hashtable PeerSorts;
			}
		}

		// Token: 0x02000C15 RID: 3093
		[Serializable]
		internal class RendererEventInformation
		{
			// Token: 0x06008686 RID: 34438 RVA: 0x0021A706 File Offset: 0x00218906
			internal RendererEventInformation()
			{
			}

			// Token: 0x06008687 RID: 34439 RVA: 0x0021A710 File Offset: 0x00218910
			internal RendererEventInformation(EventInformation.RendererEventInformation copy)
			{
				Global.Tracer.Assert(copy != null, "(null != copy)");
				if (copy.m_validToggleSenders != null)
				{
					this.m_validToggleSenders = (Hashtable)copy.m_validToggleSenders.Clone();
				}
				if (copy.m_drillthroughInfo != null)
				{
					this.m_drillthroughInfo = (Hashtable)copy.m_drillthroughInfo.Clone();
				}
			}

			// Token: 0x170029DE RID: 10718
			// (get) Token: 0x06008688 RID: 34440 RVA: 0x0021A772 File Offset: 0x00218972
			// (set) Token: 0x06008689 RID: 34441 RVA: 0x0021A77A File Offset: 0x0021897A
			internal Hashtable ValidToggleSenders
			{
				get
				{
					return this.m_validToggleSenders;
				}
				set
				{
					this.m_validToggleSenders = value;
				}
			}

			// Token: 0x170029DF RID: 10719
			// (get) Token: 0x0600868A RID: 34442 RVA: 0x0021A783 File Offset: 0x00218983
			// (set) Token: 0x0600868B RID: 34443 RVA: 0x0021A78B File Offset: 0x0021898B
			internal Hashtable DrillthroughInfo
			{
				get
				{
					return this.m_drillthroughInfo;
				}
				set
				{
					this.m_drillthroughInfo = value;
				}
			}

			// Token: 0x0600868C RID: 34444 RVA: 0x0021A794 File Offset: 0x00218994
			internal void Reset()
			{
				this.m_validToggleSenders = null;
				this.m_drillthroughInfo = null;
			}

			// Token: 0x0600868D RID: 34445 RVA: 0x0021A7A4 File Offset: 0x002189A4
			internal bool ValidToggleSender(string senderId)
			{
				return this.m_validToggleSenders != null && this.m_validToggleSenders.ContainsKey(senderId);
			}

			// Token: 0x0600868E RID: 34446 RVA: 0x0021A7BC File Offset: 0x002189BC
			internal DrillthroughInfo GetDrillthroughInfo(string drillthroughId)
			{
				if (this.m_drillthroughInfo != null)
				{
					return (DrillthroughInfo)this.m_drillthroughInfo[drillthroughId];
				}
				return null;
			}

			// Token: 0x0400483C RID: 18492
			private Hashtable m_validToggleSenders;

			// Token: 0x0400483D RID: 18493
			private Hashtable m_drillthroughInfo;
		}
	}
}
