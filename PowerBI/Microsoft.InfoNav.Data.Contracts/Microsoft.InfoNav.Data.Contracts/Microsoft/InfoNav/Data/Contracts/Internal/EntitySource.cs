using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002D2 RID: 722
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class EntitySource : IEquatable<EntitySource>
	{
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x0002AE3D File Offset: 0x0002903D
		// (set) Token: 0x0600180D RID: 6157 RVA: 0x0002AE45 File Offset: 0x00029045
		[DataMember(IsRequired = true, Order = 1)]
		public string Name { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x0002AE4E File Offset: 0x0002904E
		// (set) Token: 0x0600180F RID: 6159 RVA: 0x0002AE56 File Offset: 0x00029056
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string EntitySet { get; set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x0002AE5F File Offset: 0x0002905F
		// (set) Token: 0x06001811 RID: 6161 RVA: 0x0002AE67 File Offset: 0x00029067
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public string Entity { get; set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x0002AE70 File Offset: 0x00029070
		// (set) Token: 0x06001813 RID: 6163 RVA: 0x0002AE78 File Offset: 0x00029078
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public string Schema { get; set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0002AE81 File Offset: 0x00029081
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x0002AE89 File Offset: 0x00029089
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public EntitySourceType Type { get; set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x0002AE92 File Offset: 0x00029092
		// (set) Token: 0x06001817 RID: 6167 RVA: 0x0002AE9A File Offset: 0x0002909A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 6)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x06001818 RID: 6168 RVA: 0x0002AEA4 File Offset: 0x000290A4
		internal void WriteQueryString(QueryStringWriter w)
		{
			try
			{
				w.WriteIdentifierCustomerContent(this.Name);
				w.Write(" in ");
				EntitySourceType type = this.Type;
				if (type > EntitySourceType.Pod)
				{
					if (type == EntitySourceType.Expression)
					{
						this.Expression.WriteQueryString(w);
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(this.Schema))
					{
						w.WriteIdentifierCustomerContent(this.Schema);
						w.Write('.');
					}
					if (!string.IsNullOrEmpty(this.EntitySet))
					{
						w.WriteCustomerContent(this.EntitySet);
					}
					else if (!string.IsNullOrEmpty(this.Entity))
					{
						w.WriteIdentifierCustomerContent(this.Entity);
					}
				}
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
				if (w.TraceString)
				{
					w.Write(ex.ToString());
				}
				throw;
			}
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0002AF80 File Offset: 0x00029180
		public bool Equals(EntitySource other)
		{
			return other != null && (this == other || (ConceptualNameComparer.Instance.Equals(this.Entity, other.Entity) && QueryNameComparer.Instance.Equals(this.Name, other.Name) && ConceptualNameComparer.Instance.Equals(this.Schema, other.Schema) && ConceptualNameComparer.Instance.Equals(this.EntitySet, other.EntitySet) && this.Type == other.Type && this.Expression == other.Expression));
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0002B019 File Offset: 0x00029219
		public override bool Equals(object other)
		{
			return this.Equals(other as EntitySource);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0002B028 File Offset: 0x00029228
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this.Entity, ConceptualNameComparer.Instance), Hashing.GetHashCode<string>(this.Name, QueryNameComparer.Instance), Hashing.GetHashCode<string>(this.EntitySet, ConceptualNameComparer.Instance), Hashing.GetHashCode<string>(this.Schema, ConceptualNameComparer.Instance), Hashing.GetHashCode<EntitySourceType>(this.Type, null), Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null));
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x0002B092 File Offset: 0x00029292
		public static bool operator ==(EntitySource left, EntitySource right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0002B0A8 File Offset: 0x000292A8
		public static bool operator !=(EntitySource left, EntitySource right)
		{
			return !(left == right);
		}
	}
}
