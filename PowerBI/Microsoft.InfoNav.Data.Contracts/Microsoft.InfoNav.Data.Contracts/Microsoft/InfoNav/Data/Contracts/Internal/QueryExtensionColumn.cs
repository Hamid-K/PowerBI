using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExtensionSchema;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001F8 RID: 504
	[DataContract(Name = "Column", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryExtensionColumn : IDataContractValidatable, IEquatable<QueryExtensionColumn>
	{
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0001ACAC File Offset: 0x00018EAC
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x0001ACB4 File Offset: 0x00018EB4
		[DataMember(IsRequired = true, Order = 0)]
		public string Name { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0001ACBD File Offset: 0x00018EBD
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x0001ACC5 File Offset: 0x00018EC5
		[DataMember(IsRequired = true, Order = 10)]
		public string Expression { get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0001ACCE File Offset: 0x00018ECE
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x0001ACD6 File Offset: 0x00018ED6
		[DataMember(IsRequired = true, Order = 20)]
		public ConceptualPrimitiveType DataType { get; set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0001ACDF File Offset: 0x00018EDF
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0001ACE7 File Offset: 0x00018EE7
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public QueryExtensionNamingBehavior NamingBehavior { get; set; }

		// Token: 0x06000DBC RID: 3516 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		public bool IsValid()
		{
			return this.Name != null && this.Expression != null && this.DataType.IsValid() && this.NamingBehavior.IsValid();
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0001AD26 File Offset: 0x00018F26
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryExtensionColumn);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0001AD34 File Offset: 0x00018F34
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this.Name, ConceptualNameComparer.Instance), Hashing.GetHashCode<string>(this.Expression, null), Hashing.GetHashCode<ConceptualPrimitiveType>(this.DataType, null), this.NamingBehavior.GetHashCode());
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0001AD84 File Offset: 0x00018F84
		public bool Equals(QueryExtensionColumn other)
		{
			bool? flag = Util.AreEqual<QueryExtensionColumn>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(this.Name, other.Name) && this.Expression == other.Expression && this.DataType == other.DataType && this.NamingBehavior == other.NamingBehavior;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0001ADF4 File Offset: 0x00018FF4
		public static bool operator ==(QueryExtensionColumn left, QueryExtensionColumn right)
		{
			bool? flag = Util.AreEqual<QueryExtensionColumn>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0001AE21 File Offset: 0x00019021
		public static bool operator !=(QueryExtensionColumn left, QueryExtensionColumn right)
		{
			return !(left == right);
		}
	}
}
