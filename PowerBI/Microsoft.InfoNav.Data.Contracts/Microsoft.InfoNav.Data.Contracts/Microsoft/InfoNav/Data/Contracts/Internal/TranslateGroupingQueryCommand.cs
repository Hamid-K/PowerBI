using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002EF RID: 751
	[DataContract(Name = "Translate", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class TranslateGroupingQueryCommand : IEquatable<TranslateGroupingQueryCommand>
	{
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x0002CFB7 File Offset: 0x0002B1B7
		// (set) Token: 0x0600190B RID: 6411 RVA: 0x0002CFBF File Offset: 0x0002B1BF
		[DataMember(IsRequired = false, Order = 10)]
		public QueryDefinition Query { get; set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600190C RID: 6412 RVA: 0x0002CFC8 File Offset: 0x0002B1C8
		// (set) Token: 0x0600190D RID: 6413 RVA: 0x0002CFD0 File Offset: 0x0002B1D0
		[DataMember(IsRequired = false, Order = 20)]
		public GroupingDefinition GroupingDefinition { get; set; }

		// Token: 0x0600190E RID: 6414 RVA: 0x0002CFDC File Offset: 0x0002B1DC
		public bool Equals(TranslateGroupingQueryCommand other)
		{
			bool? flag = Util.AreEqual<TranslateGroupingQueryCommand>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Query == other.Query && this.GroupingDefinition == other.GroupingDefinition;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0002D028 File Offset: 0x0002B228
		public override bool Equals(object other)
		{
			return this.Equals(other as TranslateGroupingQueryCommand);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0002D036 File Offset: 0x0002B236
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<QueryDefinition>(this.Query, null), Hashing.GetHashCode<GroupingDefinition>(this.GroupingDefinition, null));
		}
	}
}
