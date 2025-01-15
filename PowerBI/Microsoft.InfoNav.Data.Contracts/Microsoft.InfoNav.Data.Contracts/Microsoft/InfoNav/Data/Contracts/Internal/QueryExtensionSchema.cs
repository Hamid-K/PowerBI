using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common.Internal;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001FB RID: 507
	[DataContract(Name = "Extension", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryExtensionSchema : IDataContractValidatable, IEquatable<QueryExtensionSchema>
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0001B199 File Offset: 0x00019399
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x0001B1A1 File Offset: 0x000193A1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public int? Version { get; set; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0001B1AA File Offset: 0x000193AA
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x0001B1B2 File Offset: 0x000193B2
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public string Name { get; set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0001B1BB File Offset: 0x000193BB
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x0001B1C3 File Offset: 0x000193C3
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string Extends { get; set; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0001B1CC File Offset: 0x000193CC
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x0001B1D4 File Offset: 0x000193D4
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<QueryExtensionEntity> Entities { get; set; }

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0001B1E0 File Offset: 0x000193E0
		public bool IsValid()
		{
			if (this.Version != null && this.Version.Value < 0)
			{
				return false;
			}
			if (this.Name == null)
			{
				return false;
			}
			if (ConceptualNameComparer.Instance.Equals(this.Name, this.Extends))
			{
				return false;
			}
			if (this.Entities != null)
			{
				for (int i = 0; i < this.Entities.Count; i++)
				{
					if (!this.Entities[i].IsValid())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0001B268 File Offset: 0x00019468
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryExtensionSchema);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0001B276 File Offset: 0x00019476
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this.Name, ConceptualNameComparer.Instance), Hashing.GetHashCode<string>(this.Extends, ConceptualNameComparer.Instance), (this.Entities != null) ? Hashing.CombineHash<QueryExtensionEntity>(this.Entities, null) : 0);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0001B2B4 File Offset: 0x000194B4
		public bool Equals(QueryExtensionSchema other)
		{
			bool? flag = Util.AreEqual<QueryExtensionSchema>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			if (!ConceptualNameComparer.Instance.Equals(this.Name, other.Name))
			{
				return false;
			}
			if (!ConceptualNameComparer.Instance.Equals(this.Extends, other.Extends))
			{
				return false;
			}
			bool? flag2 = Util.AreEqual<IList<QueryExtensionEntity>>(this.Entities, other.Entities);
			return (flag2 == null || flag2.Value) && (flag2 != null || this.Entities.SequenceEqual(other.Entities));
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0001B354 File Offset: 0x00019554
		public static bool operator ==(QueryExtensionSchema left, QueryExtensionSchema right)
		{
			bool? flag = Util.AreEqual<QueryExtensionSchema>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0001B381 File Offset: 0x00019581
		public static bool operator !=(QueryExtensionSchema left, QueryExtensionSchema right)
		{
			return !(left == right);
		}
	}
}
