using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common.Internal;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001FA RID: 506
	[DataContract(Name = "Measure", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryExtensionMeasure : IDataContractValidatable, IEquatable<QueryExtensionMeasure>
	{
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0001B05D File Offset: 0x0001925D
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0001B065 File Offset: 0x00019265
		[DataMember(IsRequired = true, Order = 0)]
		public string Name { get; set; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0001B06E File Offset: 0x0001926E
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0001B076 File Offset: 0x00019276
		[DataMember(IsRequired = true, Order = 10)]
		public string Expression { get; set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0001B07F File Offset: 0x0001927F
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0001B087 File Offset: 0x00019287
		[DataMember(IsRequired = true, Order = 20)]
		public ConceptualPrimitiveType DataType { get; set; }

		// Token: 0x06000DDA RID: 3546 RVA: 0x0001B090 File Offset: 0x00019290
		public bool IsValid()
		{
			return this.Name != null && this.Expression != null && this.DataType.IsValid();
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0001B0B4 File Offset: 0x000192B4
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryExtensionMeasure);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0001B0C2 File Offset: 0x000192C2
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this.Name, ConceptualNameComparer.Instance), Hashing.GetHashCode<string>(this.Expression, null), Hashing.GetHashCode<ConceptualPrimitiveType>(this.DataType, null));
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0001B0F4 File Offset: 0x000192F4
		public bool Equals(QueryExtensionMeasure other)
		{
			bool? flag = Util.AreEqual<QueryExtensionMeasure>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(this.Name, other.Name) && this.Expression == other.Expression && this.DataType == other.DataType;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0001B158 File Offset: 0x00019358
		public static bool operator ==(QueryExtensionMeasure left, QueryExtensionMeasure right)
		{
			bool? flag = Util.AreEqual<QueryExtensionMeasure>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0001B185 File Offset: 0x00019385
		public static bool operator !=(QueryExtensionMeasure left, QueryExtensionMeasure right)
		{
			return !(left == right);
		}
	}
}
