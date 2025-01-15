using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001D7 RID: 471
	[DataContract(Name = "Execute", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class SemanticQueryDataShapeCommand : IEquatable<SemanticQueryDataShapeCommand>
	{
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00018BDD File Offset: 0x00016DDD
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x00018BE5 File Offset: 0x00016DE5
		[DataMember(IsRequired = false, Order = 10)]
		public QueryDefinition Query { get; set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x00018BEE File Offset: 0x00016DEE
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x00018BF6 File Offset: 0x00016DF6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataShapeBinding Binding { get; set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00018BFF File Offset: 0x00016DFF
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x00018C07 File Offset: 0x00016E07
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 25)]
		public QueryExtensionSchema Extension { get; set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00018C10 File Offset: 0x00016E10
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x00018C18 File Offset: 0x00016E18
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public int? MaxRowCount { get; set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x00018C21 File Offset: 0x00016E21
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x00018C29 File Offset: 0x00016E29
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public string AdditionalODataFilter { get; set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x00018C32 File Offset: 0x00016E32
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x00018C3A File Offset: 0x00016E3A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public string DataSourceVariables { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00018C43 File Offset: 0x00016E43
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x00018C4B File Offset: 0x00016E4B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public ExecutionMetricsKind ExecutionMetricsKind { get; set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00018C54 File Offset: 0x00016E54
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x00018C5C File Offset: 0x00016E5C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public string AnchorTime { get; set; }

		// Token: 0x06000CAD RID: 3245 RVA: 0x00018C68 File Offset: 0x00016E68
		public bool Equals(SemanticQueryDataShapeCommand other)
		{
			bool? flag = Util.AreEqual<SemanticQueryDataShapeCommand>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			if (this.Query == other.Query && this.Binding == other.Binding && this.Extension == other.Extension && this.DataSourceVariables == other.DataSourceVariables)
			{
				int? maxRowCount = this.MaxRowCount;
				int? maxRowCount2 = other.MaxRowCount;
				if (((maxRowCount.GetValueOrDefault() == maxRowCount2.GetValueOrDefault()) & (maxRowCount != null == (maxRowCount2 != null))) && string.Equals(this.AdditionalODataFilter, other.AdditionalODataFilter, StringComparison.Ordinal) && this.ExecutionMetricsKind == other.ExecutionMetricsKind)
				{
					return string.Equals(this.AnchorTime, other.AnchorTime, StringComparison.Ordinal);
				}
			}
			return false;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00018D47 File Offset: 0x00016F47
		public override bool Equals(object other)
		{
			return this.Equals(other as SemanticQueryDataShapeCommand);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00018D58 File Offset: 0x00016F58
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<QueryDefinition>(this.Query, null), Hashing.GetHashCode<DataShapeBinding>(this.Binding, null), Hashing.GetHashCode<QueryExtensionSchema>(this.Extension, null), Hashing.GetHashCode<string>(this.DataSourceVariables, null), Hashing.GetHashCode<int?>(this.MaxRowCount, null), Hashing.GetHashCode<string>(this.AdditionalODataFilter, null), Hashing.GetHashCode<ExecutionMetricsKind>(this.ExecutionMetricsKind, null), Hashing.GetHashCode<string>(this.AnchorTime, null));
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00018DCC File Offset: 0x00016FCC
		public static bool operator ==(SemanticQueryDataShapeCommand left, SemanticQueryDataShapeCommand right)
		{
			bool? flag = Util.AreEqual<SemanticQueryDataShapeCommand>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00018DF9 File Offset: 0x00016FF9
		public static bool operator !=(SemanticQueryDataShapeCommand left, SemanticQueryDataShapeCommand right)
		{
			return !(left == right);
		}
	}
}
