using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001BF RID: 447
	[DataContract(Name = "QueryReference", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingQueryReference : IEquatable<DataShapeBindingQueryReference>
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x000174B9 File Offset: 0x000156B9
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x000174C1 File Offset: 0x000156C1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public string SourceName { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x000174CA File Offset: 0x000156CA
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x000174D2 File Offset: 0x000156D2
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public string ExpressionName { get; set; }

		// Token: 0x06000BDC RID: 3036 RVA: 0x000174DB File Offset: 0x000156DB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingQueryReference);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000174E9 File Offset: 0x000156E9
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this.SourceName, null), Hashing.GetHashCode<string>(this.ExpressionName, null));
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00017508 File Offset: 0x00015708
		public bool Equals(DataShapeBindingQueryReference other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingQueryReference>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(this.SourceName, other.SourceName) && QueryNameComparer.Instance.Equals(this.ExpressionName, other.ExpressionName);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00017560 File Offset: 0x00015760
		public static bool operator ==(DataShapeBindingQueryReference left, DataShapeBindingQueryReference right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingQueryReference>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001758D File Offset: 0x0001578D
		public static bool operator !=(DataShapeBindingQueryReference left, DataShapeBindingQueryReference right)
		{
			return !(left == right);
		}
	}
}
