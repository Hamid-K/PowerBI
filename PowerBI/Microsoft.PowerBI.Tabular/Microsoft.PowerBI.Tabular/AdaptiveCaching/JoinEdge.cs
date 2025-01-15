using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Tabular.AdaptiveCaching
{
	// Token: 0x0200012A RID: 298
	[DataContract]
	[Serializable]
	internal struct JoinEdge : IEquatable<JoinEdge>
	{
		// Token: 0x06001481 RID: 5249 RVA: 0x0008B2FC File Offset: 0x000894FC
		public override bool Equals(object obj)
		{
			if (obj is JoinEdge)
			{
				JoinEdge joinEdge = (JoinEdge)obj;
				return this.Equals(joinEdge);
			}
			return false;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0008B324 File Offset: 0x00089524
		public bool Equals(JoinEdge other)
		{
			return this.FromTable == other.FromTable && this.FromColumn == other.FromColumn && this.ToTable == other.ToTable && this.ToColumn == other.ToColumn;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0008B380 File Offset: 0x00089580
		public override int GetHashCode()
		{
			return (((-976901748 * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.FromTable)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.FromColumn)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.ToTable)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.ToColumn);
		}

		// Token: 0x0400032E RID: 814
		[DataMember(Name = "fromTable")]
		public string FromTable;

		// Token: 0x0400032F RID: 815
		[DataMember(Name = "fromColumn")]
		public string FromColumn;

		// Token: 0x04000330 RID: 816
		[DataMember(Name = "toTable")]
		public string ToTable;

		// Token: 0x04000331 RID: 817
		[DataMember(Name = "toColumn")]
		public string ToColumn;
	}
}
