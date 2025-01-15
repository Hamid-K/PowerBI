using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008F8 RID: 2296
	[PersistedWithinRequestOnly]
	public sealed class RuntimeUserSortTargetInfo : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007E7C RID: 32380 RVA: 0x0020A308 File Offset: 0x00208508
		internal RuntimeUserSortTargetInfo()
		{
		}

		// Token: 0x06007E7D RID: 32381 RVA: 0x0020A310 File Offset: 0x00208510
		internal RuntimeUserSortTargetInfo(IReference<IHierarchyObj> owner, int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
		{
			this.AddSortInfo(owner, sortInfoIndex, sortInfo);
		}

		// Token: 0x1700291B RID: 10523
		// (get) Token: 0x06007E7E RID: 32382 RVA: 0x0020A321 File Offset: 0x00208521
		// (set) Token: 0x06007E7F RID: 32383 RVA: 0x0020A329 File Offset: 0x00208529
		internal BTree SortTree
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

		// Token: 0x1700291C RID: 10524
		// (get) Token: 0x06007E80 RID: 32384 RVA: 0x0020A332 File Offset: 0x00208532
		// (set) Token: 0x06007E81 RID: 32385 RVA: 0x0020A33A File Offset: 0x0020853A
		internal List<AggregateRow> AggregateRows
		{
			get
			{
				return this.m_aggregateRows;
			}
			set
			{
				this.m_aggregateRows = value;
			}
		}

		// Token: 0x1700291D RID: 10525
		// (get) Token: 0x06007E82 RID: 32386 RVA: 0x0020A343 File Offset: 0x00208543
		// (set) Token: 0x06007E83 RID: 32387 RVA: 0x0020A34B File Offset: 0x0020854B
		internal List<int> SortFilterInfoIndices
		{
			get
			{
				return this.m_sortFilterInfoIndices;
			}
			set
			{
				this.m_sortFilterInfoIndices = value;
			}
		}

		// Token: 0x1700291E RID: 10526
		// (get) Token: 0x06007E84 RID: 32388 RVA: 0x0020A354 File Offset: 0x00208554
		internal bool TargetForNonDetailSort
		{
			get
			{
				return this.m_targetForNonDetailSort != null;
			}
		}

		// Token: 0x06007E85 RID: 32389 RVA: 0x0020A360 File Offset: 0x00208560
		internal void AddSortInfo(IReference<IHierarchyObj> owner, int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
		{
			IInScopeEventSource eventSource = sortInfo.Value().EventSource;
			if (eventSource.UserSort.SortExpressionScope != null || owner.Value().IsDetail)
			{
				if (eventSource.UserSort.SortExpressionScope == null)
				{
					this.AddSortInfoIndex(sortInfoIndex, sortInfo);
				}
				if (this.m_sortTree == null)
				{
					IHierarchyObj hierarchyObj = owner.Value();
					this.m_sortTree = new BTree(hierarchyObj, hierarchyObj.OdpContext, hierarchyObj.Depth + 1);
				}
			}
			if (eventSource.UserSort.SortExpressionScope != null)
			{
				if (this.m_targetForNonDetailSort == null)
				{
					this.m_targetForNonDetailSort = new Hashtable();
				}
				this.m_targetForNonDetailSort.Add(sortInfoIndex, null);
				return;
			}
			if (this.m_targetForDetailSort == null)
			{
				this.m_targetForDetailSort = new Hashtable();
			}
			this.m_targetForDetailSort.Add(sortInfoIndex, null);
		}

		// Token: 0x06007E86 RID: 32390 RVA: 0x0020A42C File Offset: 0x0020862C
		internal void AddSortInfoIndex(int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfoRef)
		{
			using (sortInfoRef.PinValue())
			{
				RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = sortInfoRef.Value();
				Global.Tracer.Assert(runtimeSortFilterEventInfo.EventSource.UserSort.SortExpressionScope == null || !runtimeSortFilterEventInfo.TargetSortFilterInfoAdded);
				if (this.m_sortFilterInfoIndices == null)
				{
					this.m_sortFilterInfoIndices = new List<int>();
				}
				this.m_sortFilterInfoIndices.Add(sortInfoIndex);
				runtimeSortFilterEventInfo.TargetSortFilterInfoAdded = true;
			}
		}

		// Token: 0x06007E87 RID: 32391 RVA: 0x0020A4B4 File Offset: 0x002086B4
		internal void ResetTargetForNonDetailSort()
		{
			this.m_targetForNonDetailSort = null;
		}

		// Token: 0x06007E88 RID: 32392 RVA: 0x0020A4C0 File Offset: 0x002086C0
		internal bool IsTargetForSort(int index, bool detailSort)
		{
			Hashtable hashtable = this.m_targetForNonDetailSort;
			if (detailSort)
			{
				hashtable = this.m_targetForDetailSort;
			}
			return hashtable != null && hashtable.Contains(index);
		}

		// Token: 0x06007E89 RID: 32393 RVA: 0x0020A4F2 File Offset: 0x002086F2
		internal void MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo, IReference<IHierarchyObj> sortTarget)
		{
			if (this.m_targetForNonDetailSort != null)
			{
				this.MarkSortInfoProcessed(runtimeSortFilterInfo, sortTarget, this.m_targetForNonDetailSort.Keys);
			}
			if (this.m_targetForDetailSort != null)
			{
				this.MarkSortInfoProcessed(runtimeSortFilterInfo, sortTarget, this.m_targetForDetailSort.Keys);
			}
		}

		// Token: 0x06007E8A RID: 32394 RVA: 0x0020A52C File Offset: 0x0020872C
		private void MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo, IReference<IHierarchyObj> sortTarget, ICollection indices)
		{
			foreach (object obj in indices)
			{
				int num = (int)obj;
				IReference<RuntimeSortFilterEventInfo> reference = runtimeSortFilterInfo[num];
				using (reference.PinValue())
				{
					RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = reference.Value();
					if (runtimeSortFilterEventInfo.EventTarget.Equals(sortTarget))
					{
						Global.Tracer.Assert(!runtimeSortFilterEventInfo.Processed, "(!runtimeSortInfo.Processed)");
						runtimeSortFilterEventInfo.Processed = true;
					}
				}
			}
		}

		// Token: 0x06007E8B RID: 32395 RVA: 0x0020A5D8 File Offset: 0x002087D8
		internal void EnterProcessUserSortPhase(OnDemandProcessingContext odpContext)
		{
			if (this.m_sortFilterInfoIndices != null)
			{
				int count = this.m_sortFilterInfoIndices.Count;
				for (int i = 0; i < count; i++)
				{
					odpContext.UserSortFilterContext.EnterProcessUserSortPhase(this.m_sortFilterInfoIndices[i]);
				}
			}
		}

		// Token: 0x06007E8C RID: 32396 RVA: 0x0020A61C File Offset: 0x0020881C
		internal void LeaveProcessUserSortPhase(OnDemandProcessingContext odpContext)
		{
			if (this.m_sortFilterInfoIndices != null)
			{
				int count = this.m_sortFilterInfoIndices.Count;
				for (int i = 0; i < count; i++)
				{
					odpContext.UserSortFilterContext.LeaveProcessUserSortPhase(this.m_sortFilterInfoIndices[i]);
				}
			}
		}

		// Token: 0x06007E8D RID: 32397 RVA: 0x0020A660 File Offset: 0x00208860
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeUserSortTargetInfo.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.SortTree)
				{
					switch (memberName)
					{
					case MemberName.AggregateRows:
						writer.Write<AggregateRow>(this.m_aggregateRows);
						break;
					case MemberName.SortFilterInfoIndices:
						writer.WriteListOfPrimitives<int>(this.m_sortFilterInfoIndices);
						break;
					case MemberName.TargetForNonDetailSort:
						writer.WriteVariantVariantHashtable(this.m_targetForNonDetailSort);
						break;
					case MemberName.TargetForDetailSort:
						writer.WriteVariantVariantHashtable(this.m_targetForDetailSort);
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
				else
				{
					writer.Write(this.m_sortTree);
				}
			}
		}

		// Token: 0x06007E8E RID: 32398 RVA: 0x0020A710 File Offset: 0x00208910
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeUserSortTargetInfo.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.SortTree)
				{
					switch (memberName)
					{
					case MemberName.AggregateRows:
						this.m_aggregateRows = reader.ReadListOfRIFObjects<List<AggregateRow>>();
						break;
					case MemberName.SortFilterInfoIndices:
						this.m_sortFilterInfoIndices = reader.ReadListOfPrimitives<int>();
						break;
					case MemberName.TargetForNonDetailSort:
						this.m_targetForNonDetailSort = reader.ReadVariantVariantHashtable();
						break;
					case MemberName.TargetForDetailSort:
						this.m_targetForDetailSort = reader.ReadVariantVariantHashtable();
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
				else
				{
					this.m_sortTree = (BTree)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x0020A7C5 File Offset: 0x002089C5
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007E90 RID: 32400 RVA: 0x0020A7C7 File Offset: 0x002089C7
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeUserSortTargetInfo;
		}

		// Token: 0x06007E91 RID: 32401 RVA: 0x0020A7CC File Offset: 0x002089CC
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeUserSortTargetInfo.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeUserSortTargetInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.SortTree, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTree),
					new MemberInfo(MemberName.AggregateRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.AggregateRow),
					new MemberInfo(MemberName.SortFilterInfoIndices, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32),
					new MemberInfo(MemberName.TargetForNonDetailSort, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantVariantHashtable),
					new MemberInfo(MemberName.TargetForDetailSort, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantVariantHashtable)
				});
			}
			return RuntimeUserSortTargetInfo.m_declaration;
		}

		// Token: 0x1700291F RID: 10527
		// (get) Token: 0x06007E92 RID: 32402 RVA: 0x0020A851 File Offset: 0x00208A51
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_sortTree) + ItemSizes.SizeOf<AggregateRow>(this.m_aggregateRows) + ItemSizes.SizeOf(this.m_sortFilterInfoIndices) + ItemSizes.SizeOf(this.m_targetForNonDetailSort) + ItemSizes.SizeOf(this.m_targetForDetailSort);
			}
		}

		// Token: 0x04003E2B RID: 15915
		private BTree m_sortTree;

		// Token: 0x04003E2C RID: 15916
		private List<AggregateRow> m_aggregateRows;

		// Token: 0x04003E2D RID: 15917
		private List<int> m_sortFilterInfoIndices;

		// Token: 0x04003E2E RID: 15918
		private Hashtable m_targetForNonDetailSort;

		// Token: 0x04003E2F RID: 15919
		private Hashtable m_targetForDetailSort;

		// Token: 0x04003E30 RID: 15920
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeUserSortTargetInfo.GetDeclaration();
	}
}
