using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000828 RID: 2088
	internal sealed class TreePartitionManager : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600753D RID: 30013 RVA: 0x001E5A20 File Offset: 0x001E3C20
		internal TreePartitionManager()
		{
		}

		// Token: 0x0600753E RID: 30014 RVA: 0x001E5A28 File Offset: 0x001E3C28
		internal TreePartitionManager(List<long> partitionTable)
		{
			this.m_treePartitionOffsets = partitionTable;
		}

		// Token: 0x17002793 RID: 10131
		// (get) Token: 0x0600753F RID: 30015 RVA: 0x001E5A37 File Offset: 0x001E3C37
		// (set) Token: 0x06007540 RID: 30016 RVA: 0x001E5A3F File Offset: 0x001E3C3F
		internal bool TreeHasChanged
		{
			get
			{
				return this.m_treeChanged;
			}
			set
			{
				this.m_treeChanged = value;
			}
		}

		// Token: 0x06007541 RID: 30017 RVA: 0x001E5A48 File Offset: 0x001E3C48
		internal ReferenceID AllocateNewTreePartition()
		{
			this.m_treeChanged = true;
			if (this.m_treePartitionOffsets == null)
			{
				this.m_treePartitionOffsets = new List<long>();
			}
			int count = this.m_treePartitionOffsets.Count;
			this.m_treePartitionOffsets.Add(TreePartitionManager.EmptyTreePartitionOffset);
			return new ReferenceID
			{
				HasMultiPart = true,
				IsTemporary = false,
				PartitionID = count
			};
		}

		// Token: 0x06007542 RID: 30018 RVA: 0x001E5AAC File Offset: 0x001E3CAC
		internal void UpdateTreePartitionOffset(ReferenceID id, long offset)
		{
			int partitionIndex = this.GetPartitionIndex(id);
			Global.Tracer.Assert(offset >= 0L, "Invalid offset for Tree partition. ID: {0} Offset: {1}", new object[] { id, offset });
			Global.Tracer.Assert(this.m_treePartitionOffsets[partitionIndex] == TreePartitionManager.EmptyTreePartitionOffset, "Cannot update offset for already persisted tree partition");
			this.m_treeChanged = true;
			this.m_treePartitionOffsets[partitionIndex] = offset;
		}

		// Token: 0x06007543 RID: 30019 RVA: 0x001E5B28 File Offset: 0x001E3D28
		internal long GetTreePartitionOffset(ReferenceID id)
		{
			int partitionIndex = this.GetPartitionIndex(id);
			return this.m_treePartitionOffsets[partitionIndex];
		}

		// Token: 0x06007544 RID: 30020 RVA: 0x001E5B4C File Offset: 0x001E3D4C
		private int GetPartitionIndex(ReferenceID id)
		{
			int partitionID = id.PartitionID;
			Global.Tracer.Assert(partitionID >= 0, "Invalid tree partition id: {0}", new object[] { partitionID });
			Global.Tracer.Assert(this.m_treePartitionOffsets != null && partitionID < this.m_treePartitionOffsets.Count, "Cannot update Tree partition: {0} without first allocating it", new object[] { partitionID });
			return partitionID;
		}

		// Token: 0x06007545 RID: 30021 RVA: 0x001E5BC0 File Offset: 0x001E3DC0
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(TreePartitionManager.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.TreePartitionOffsets)
				{
					writer.WriteListOfPrimitives<long>(this.m_treePartitionOffsets);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06007546 RID: 30022 RVA: 0x001E5C14 File Offset: 0x001E3E14
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(TreePartitionManager.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.TreePartitionOffsets)
				{
					this.m_treePartitionOffsets = reader.ReadListOfPrimitives<long>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06007547 RID: 30023 RVA: 0x001E5C65 File Offset: 0x001E3E65
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007548 RID: 30024 RVA: 0x001E5C67 File Offset: 0x001E3E67
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TreePartitionManager;
		}

		// Token: 0x06007549 RID: 30025 RVA: 0x001E5C70 File Offset: 0x001E3E70
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (TreePartitionManager.m_Declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TreePartitionManager, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.TreePartitionOffsets, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int64)
				});
			}
			return TreePartitionManager.m_Declaration;
		}

		// Token: 0x04003B83 RID: 15235
		private List<long> m_treePartitionOffsets;

		// Token: 0x04003B84 RID: 15236
		[NonSerialized]
		private bool m_treeChanged;

		// Token: 0x04003B85 RID: 15237
		[NonSerialized]
		internal static readonly long EmptyTreePartitionOffset = -1L;

		// Token: 0x04003B86 RID: 15238
		[NonSerialized]
		internal static readonly ReferenceID EmptyTreePartitionID = new ReferenceID(true, false, -1);

		// Token: 0x04003B87 RID: 15239
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = TreePartitionManager.GetDeclaration();
	}
}
