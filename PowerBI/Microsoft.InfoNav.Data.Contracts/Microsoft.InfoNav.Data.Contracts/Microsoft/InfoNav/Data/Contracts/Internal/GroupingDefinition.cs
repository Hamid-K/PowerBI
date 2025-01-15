using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common.Internal;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001DF RID: 479
	[DataContract(Name = "GroupingDefinition", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class GroupingDefinition : IDataContractValidatable, IEquatable<GroupingDefinition>
	{
		// Token: 0x06000CE4 RID: 3300 RVA: 0x0001936C File Offset: 0x0001756C
		public GroupingDefinition()
		{
			this.Sources = new List<EntitySource>();
			this.GroupedColumns = new List<QueryExpressionContainer>();
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0001938A File Offset: 0x0001758A
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x00019392 File Offset: 0x00017592
		[DataMember(IsRequired = true, Order = 0)]
		public int Version { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0001939B File Offset: 0x0001759B
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x000193A3 File Offset: 0x000175A3
		[DataMember(IsRequired = true, Order = 1)]
		public List<EntitySource> Sources { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x000193AC File Offset: 0x000175AC
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x000193B4 File Offset: 0x000175B4
		[DataMember(IsRequired = true, Order = 2)]
		public List<QueryExpressionContainer> GroupedColumns { get; set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x000193BD File Offset: 0x000175BD
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x000193C5 File Offset: 0x000175C5
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public List<GroupItem> GroupItems { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x000193CE File Offset: 0x000175CE
		// (set) Token: 0x06000CEE RID: 3310 RVA: 0x000193D6 File Offset: 0x000175D6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public BinItem BinItem { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x000193DF File Offset: 0x000175DF
		// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x000193E7 File Offset: 0x000175E7
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public PartitionTable PartitionTable { get; set; }

		// Token: 0x06000CF1 RID: 3313 RVA: 0x000193F0 File Offset: 0x000175F0
		public bool IsValid()
		{
			if (this.Version < 0)
			{
				return false;
			}
			if (this.Sources != null && this.Sources.Count > 0)
			{
				if (this.Sources.All((EntitySource source) => QueryDefinitionValidator.IsValid(source)))
				{
					return this.GroupedColumns != null && this.GroupedColumns.Count > 0 && QueryExpressionValidator.AreValid(this.GroupedColumns) && (this.GroupItems != null || !(this.BinItem == null) || !(this.PartitionTable == null)) && (this.GroupItems == null || !(this.BinItem != null)) && (this.GroupItems == null || !(this.PartitionTable != null)) && (!(this.BinItem != null) || !(this.PartitionTable != null)) && (this.GroupItems == null || this.GroupItems.AreAllValid<GroupItem>()) && (!(this.BinItem != null) || this.BinItem.IsValid()) && (!(this.PartitionTable != null) || this.PartitionTable.IsValid());
				}
			}
			return false;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00019534 File Offset: 0x00017734
		public bool Equals(GroupingDefinition other)
		{
			return other != null && (this == other || (this.Version == other.Version && this.Sources.SequenceEqual(other.Sources) && this.GroupedColumns.SequenceEqual(other.GroupedColumns) && this.GroupItems.SequenceEqual(other.GroupItems) && this.BinItem == other.BinItem && this.PartitionTable == other.PartitionTable));
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000195B9 File Offset: 0x000177B9
		public override bool Equals(object other)
		{
			return this.Equals(other as GroupingDefinition);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000195C8 File Offset: 0x000177C8
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Version.GetHashCode(), (this.Sources != null) ? Hashing.CombineHash<EntitySource>(this.Sources, null) : 0, (this.GroupedColumns != null) ? Hashing.CombineHash<QueryExpressionContainer>(this.GroupedColumns, null) : 0, (this.GroupItems != null) ? Hashing.CombineHash<GroupItem>(this.GroupItems, null) : 0, Hashing.GetHashCode<BinItem>(this.BinItem, null), Hashing.GetHashCode<PartitionTable>(this.PartitionTable, null));
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00019645 File Offset: 0x00017845
		public static bool operator ==(GroupingDefinition left, GroupingDefinition right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0001965B File Offset: 0x0001785B
		public static bool operator !=(GroupingDefinition left, GroupingDefinition right)
		{
			return !(left == right);
		}
	}
}
