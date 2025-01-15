using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Common.Internal;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001E6 RID: 486
	[DataContract(Name = "PartitionTableResult", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class PartitionTableResult : IDataContractValidatable, IEquatable<PartitionTableResult>
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0001A035 File Offset: 0x00018235
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x0001A03D File Offset: 0x0001823D
		[DataMember(IsRequired = true, Order = 10)]
		public string TableName { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0001A046 File Offset: 0x00018246
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x0001A04E File Offset: 0x0001824E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string PartitionIdColumn { get; set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0001A057 File Offset: 0x00018257
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x0001A05F File Offset: 0x0001825F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<IList<PartitionTableIdentityMapping>> ItemIdMappings { get; set; }

		// Token: 0x06000D47 RID: 3399 RVA: 0x0001A068 File Offset: 0x00018268
		public bool IsValid()
		{
			if (this.TableName == null)
			{
				return false;
			}
			if (this.ItemIdMappings != null)
			{
				return this.ItemIdMappings.SelectMany<PartitionTableIdentityMapping>().All((PartitionTableIdentityMapping mapping) => mapping.IsValid());
			}
			return true;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0001A0B8 File Offset: 0x000182B8
		public bool Equals(PartitionTableResult other)
		{
			return other != null && (this == other || (StringUtil.EqualsOrdinal(this.TableName, other.TableName) && StringUtil.EqualsOrdinal(this.PartitionIdColumn, other.PartitionIdColumn) && PartitionTableResult._identityMappingsComparer.Equals(this.ItemIdMappings, other.ItemIdMappings)));
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0001A10E File Offset: 0x0001830E
		public override bool Equals(object other)
		{
			return this.Equals(other as PartitionTableResult);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0001A11C File Offset: 0x0001831C
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.TableName.GetHashCode(), Hashing.GetHashCode<string>(this.PartitionIdColumn, null), (this.ItemIdMappings != null) ? PartitionTableResult._identityMappingsComparer.GetHashCode(this.ItemIdMappings) : (-48879));
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0001A159 File Offset: 0x00018359
		public static bool operator ==(PartitionTableResult left, PartitionTableResult right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0001A16F File Offset: 0x0001836F
		public static bool operator !=(PartitionTableResult left, PartitionTableResult right)
		{
			return !(left == right);
		}

		// Token: 0x040006C3 RID: 1731
		private static readonly ListEqualityComparer<IList<PartitionTableIdentityMapping>> _identityMappingsComparer = new ListEqualityComparer<IList<PartitionTableIdentityMapping>>(ListEqualityComparer<PartitionTableIdentityMapping>.Default);
	}
}
