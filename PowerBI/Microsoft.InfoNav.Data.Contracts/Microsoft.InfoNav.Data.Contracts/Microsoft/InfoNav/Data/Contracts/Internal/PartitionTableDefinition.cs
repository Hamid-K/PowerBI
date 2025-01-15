using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common.Internal;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001E4 RID: 484
	[DataContract(Name = "PartitionTableDefinition", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class PartitionTableDefinition : IDataContractValidatable, IEquatable<PartitionTableDefinition>
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00019D3D File Offset: 0x00017F3D
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x00019D45 File Offset: 0x00017F45
		[DataMember(IsRequired = true, Order = 10)]
		public QueryDefinition TableDefinition { get; set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00019D4E File Offset: 0x00017F4E
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x00019D56 File Offset: 0x00017F56
		[DataMember(IsRequired = true, Order = 20)]
		public List<string> ItemIdColumns { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00019D5F File Offset: 0x00017F5F
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x00019D67 File Offset: 0x00017F67
		[DataMember(IsRequired = true, Order = 30)]
		public string PartitionIdColumn { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00019D70 File Offset: 0x00017F70
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x00019D78 File Offset: 0x00017F78
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public List<Partition> Partitions { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00019D81 File Offset: 0x00017F81
		// (set) Token: 0x06000D2E RID: 3374 RVA: 0x00019D89 File Offset: 0x00017F89
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public string DefaultPartitionPrefix { get; set; }

		// Token: 0x06000D2F RID: 3375 RVA: 0x00019D94 File Offset: 0x00017F94
		public bool IsValid()
		{
			return !(this.TableDefinition == null) && QueryDefinitionValidator.IsValid(this.TableDefinition) && !this.ItemIdColumns.IsNullOrEmpty<string>() && this.PartitionIdColumn != null && (this.Partitions.IsNullOrEmpty<Partition>() || this.Partitions.AreAllValid<Partition>());
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00019DF4 File Offset: 0x00017FF4
		public bool Equals(PartitionTableDefinition other)
		{
			return other != null && (this == other || (this.TableDefinition.Equals(other.TableDefinition) && this.ItemIdColumns.SequenceEqual(other.ItemIdColumns, QueryNameComparer.Instance) && QueryNameComparer.Instance.Equals(this.PartitionIdColumn, other.PartitionIdColumn) && this.Partitions.SequenceEqual(other.Partitions) && this.DefaultPartitionPrefix == other.DefaultPartitionPrefix));
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00019E75 File Offset: 0x00018075
		public override bool Equals(object other)
		{
			return this.Equals(other as PartitionTableDefinition);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00019E84 File Offset: 0x00018084
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.TableDefinition.GetHashCode(), (this.ItemIdColumns != null) ? Hashing.CombineHash<string>(this.ItemIdColumns, QueryNameComparer.Instance) : 0, this.PartitionIdColumn.GetHashCode(), (this.Partitions != null) ? Hashing.CombineHash<Partition>(this.Partitions, null) : 0, (this.DefaultPartitionPrefix != null) ? this.DefaultPartitionPrefix.GetHashCode() : 0);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00019EF4 File Offset: 0x000180F4
		public static bool operator ==(PartitionTableDefinition left, PartitionTableDefinition right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00019F0A File Offset: 0x0001810A
		public static bool operator !=(PartitionTableDefinition left, PartitionTableDefinition right)
		{
			return !(left == right);
		}
	}
}
