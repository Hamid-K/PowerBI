using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExtensionSchema;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001F9 RID: 505
	[DataContract(Name = "Entity", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryExtensionEntity : IDataContractValidatable, IEquatable<QueryExtensionEntity>
	{
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0001AE35 File Offset: 0x00019035
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x0001AE3D File Offset: 0x0001903D
		[DataMember(IsRequired = true, Order = 0)]
		public string Name { get; set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0001AE46 File Offset: 0x00019046
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x0001AE4E File Offset: 0x0001904E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string Extends { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0001AE57 File Offset: 0x00019057
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x0001AE5F File Offset: 0x0001905F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 11)]
		public QueryExtensionNamingBehavior NamingBehavior { get; set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0001AE68 File Offset: 0x00019068
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x0001AE70 File Offset: 0x00019070
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<QueryExtensionMeasure> Measures { get; set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0001AE79 File Offset: 0x00019079
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x0001AE81 File Offset: 0x00019081
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<QueryExtensionColumn> Columns { get; set; }

		// Token: 0x06000DCD RID: 3533 RVA: 0x0001AE8C File Offset: 0x0001908C
		public bool IsValid()
		{
			if (this.Name == null || !this.NamingBehavior.IsValid())
			{
				return false;
			}
			if (this.Measures != null)
			{
				for (int i = 0; i < this.Measures.Count; i++)
				{
					if (!this.Measures[i].IsValid())
					{
						return false;
					}
				}
			}
			if (this.Columns != null)
			{
				for (int j = 0; j < this.Columns.Count; j++)
				{
					if (!this.Columns[j].IsValid())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0001AF1C File Offset: 0x0001911C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryExtensionEntity);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0001AF2C File Offset: 0x0001912C
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this.Name, ConceptualNameComparer.Instance), Hashing.GetHashCode<string>(this.Extends, ConceptualNameComparer.Instance), Hashing.CombineHash<QueryExtensionMeasure>(this.Measures, null), Hashing.CombineHash<QueryExtensionColumn>(this.Columns, null), this.NamingBehavior.GetHashCode());
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0001AF8C File Offset: 0x0001918C
		public bool Equals(QueryExtensionEntity other)
		{
			bool? flag = Util.AreEqual<QueryExtensionEntity>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(this.Name, other.Name) && ConceptualNameComparer.Instance.Equals(this.Extends, other.Extends) && this.NamingBehavior == other.NamingBehavior && this.Measures.SequenceEqual(other.Measures) && this.Columns.SequenceEqual(other.Columns);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0001B01C File Offset: 0x0001921C
		public static bool operator ==(QueryExtensionEntity left, QueryExtensionEntity right)
		{
			bool? flag = Util.AreEqual<QueryExtensionEntity>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0001B049 File Offset: 0x00019249
		public static bool operator !=(QueryExtensionEntity left, QueryExtensionEntity right)
		{
			return !(left == right);
		}
	}
}
