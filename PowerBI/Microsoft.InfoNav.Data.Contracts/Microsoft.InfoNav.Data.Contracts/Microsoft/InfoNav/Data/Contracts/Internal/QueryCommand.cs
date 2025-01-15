using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001D0 RID: 464
	[DataContract(Name = "QueryCommand", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public class QueryCommand : IEquatable<QueryCommand>
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00018249 File Offset: 0x00016449
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x00018251 File Offset: 0x00016451
		[DataMember(Name = "SemanticQueryDataShapeCommand", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public SemanticQueryDataShapeCommand SemanticQueryDataShapeCommand { get; set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0001825A File Offset: 0x0001645A
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x00018262 File Offset: 0x00016462
		[DataMember(Name = "ScriptVisualCommand", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public ScriptVisualCommand ScriptVisualCommand { get; set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0001826B File Offset: 0x0001646B
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x00018273 File Offset: 0x00016473
		[DataMember(Name = "ExportDataCommand", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public ExportDataCommand ExportDataCommand { get; set; }

		// Token: 0x06000C50 RID: 3152 RVA: 0x0001827C File Offset: 0x0001647C
		public bool Equals(QueryCommand other)
		{
			bool? flag = Util.AreEqual<QueryCommand>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.SemanticQueryDataShapeCommand == other.SemanticQueryDataShapeCommand && this.ScriptVisualCommand == other.ScriptVisualCommand && this.ExportDataCommand == other.ExportDataCommand;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000182DB File Offset: 0x000164DB
		public override bool Equals(object other)
		{
			return this.Equals(other as QueryCommand);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000182EC File Offset: 0x000164EC
		public override int GetHashCode()
		{
			return Hashing.CombineHash((this.SemanticQueryDataShapeCommand != null) ? this.SemanticQueryDataShapeCommand.GetHashCode() : 0, (this.ScriptVisualCommand != null) ? this.ScriptVisualCommand.GetHashCode() : 0, (this.ExportDataCommand != null) ? this.ExportDataCommand.GetHashCode() : 0);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00018354 File Offset: 0x00016554
		public static bool operator ==(QueryCommand left, QueryCommand right)
		{
			bool? flag = Util.AreEqual<QueryCommand>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00018381 File Offset: 0x00016581
		public static bool operator !=(QueryCommand left, QueryCommand right)
		{
			return !(left == right);
		}
	}
}
