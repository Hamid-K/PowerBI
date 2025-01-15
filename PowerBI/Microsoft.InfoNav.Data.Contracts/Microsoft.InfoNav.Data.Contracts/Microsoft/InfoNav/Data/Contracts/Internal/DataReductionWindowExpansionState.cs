using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001C7 RID: 455
	[DataContract(Name = "WindowExpansionState", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionWindowExpansionState : IEquatable<DataReductionWindowExpansionState>
	{
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00017881 File Offset: 0x00015A81
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00017889 File Offset: 0x00015A89
		[DataMember(IsRequired = false, Order = 5)]
		public List<EntitySource> From { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00017892 File Offset: 0x00015A92
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0001789A File Offset: 0x00015A9A
		[DataMember(IsRequired = false, Order = 10)]
		public IList<DataShapeBindingAxisExpansionLevel> Levels { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x000178A3 File Offset: 0x00015AA3
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x000178AB File Offset: 0x00015AAB
		[DataMember(IsRequired = true, Order = 30)]
		public DataReductionWindowExpansionInstance WindowInstances { get; set; }

		// Token: 0x06000C0B RID: 3083 RVA: 0x000178B4 File Offset: 0x00015AB4
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionWindowExpansionState);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x000178C2 File Offset: 0x00015AC2
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<EntitySource>(this.From, null), Hashing.CombineHash<DataShapeBindingAxisExpansionLevel>(this.Levels, null), Hashing.GetHashCode<DataReductionWindowExpansionInstance>(this.WindowInstances, null));
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x000178F0 File Offset: 0x00015AF0
		public bool Equals(DataReductionWindowExpansionState other)
		{
			bool? flag = Util.AreEqual<DataReductionWindowExpansionState>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.From.SequenceEqual(other.From) && this.Levels.SequenceEqual(other.Levels) && this.WindowInstances == other.WindowInstances;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00017950 File Offset: 0x00015B50
		public static bool operator ==(DataReductionWindowExpansionState left, DataReductionWindowExpansionState right)
		{
			bool? flag = Util.AreEqual<DataReductionWindowExpansionState>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0001797D File Offset: 0x00015B7D
		public static bool operator !=(DataReductionWindowExpansionState left, DataReductionWindowExpansionState right)
		{
			return !(left == right);
		}
	}
}
