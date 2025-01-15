using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common.Internal;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001E3 RID: 483
	[DataContract(Name = "PartitionTable", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class PartitionTable : IDataContractValidatable, IEquatable<PartitionTable>
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00019C58 File Offset: 0x00017E58
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x00019C60 File Offset: 0x00017E60
		[DataMember(IsRequired = true, Order = 10)]
		public PartitionTableDefinition Definition { get; set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00019C69 File Offset: 0x00017E69
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x00019C71 File Offset: 0x00017E71
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public PartitionTableResult Result { get; set; }

		// Token: 0x06000D1E RID: 3358 RVA: 0x00019C7A File Offset: 0x00017E7A
		public bool IsValid()
		{
			return this.Definition != null && this.Definition.IsValid() && (this.Result == null || this.Result.IsValid());
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00019CB4 File Offset: 0x00017EB4
		public bool Equals(PartitionTable other)
		{
			return other != null && (this == other || (this.Definition.Equals(other.Definition) && this.Result == other.Result));
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00019CE7 File Offset: 0x00017EE7
		public override bool Equals(object other)
		{
			return this.Equals(other as PartitionTable);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00019CF5 File Offset: 0x00017EF5
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Definition.GetHashCode(), Hashing.GetHashCode<PartitionTableResult>(this.Result, null));
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00019D13 File Offset: 0x00017F13
		public static bool operator ==(PartitionTable left, PartitionTable right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00019D29 File Offset: 0x00017F29
		public static bool operator !=(PartitionTable left, PartitionTable right)
		{
			return !(left == right);
		}
	}
}
