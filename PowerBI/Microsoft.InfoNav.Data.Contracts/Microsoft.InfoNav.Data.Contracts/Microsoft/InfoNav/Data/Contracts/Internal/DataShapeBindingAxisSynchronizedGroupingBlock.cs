using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200019C RID: 412
	[DataContract(Name = "SynchronizedGroupingBlock", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingAxisSynchronizedGroupingBlock : IEquatable<DataShapeBindingAxisSynchronizedGroupingBlock>
	{
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001601D File Offset: 0x0001421D
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x00016025 File Offset: 0x00014225
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public IList<int> Groupings { get; set; }

		// Token: 0x06000B2C RID: 2860 RVA: 0x0001602E File Offset: 0x0001422E
		public override int GetHashCode()
		{
			return Hashing.CombineHash<int>(this.Groupings, null);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0001603C File Offset: 0x0001423C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingAxisSynchronizedGroupingBlock);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0001604C File Offset: 0x0001424C
		public bool Equals(DataShapeBindingAxisSynchronizedGroupingBlock other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxisSynchronizedGroupingBlock>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Groupings.BagEquals(other.Groupings, EqualityComparer<int>.Default);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00016090 File Offset: 0x00014290
		public static bool operator ==(DataShapeBindingAxisSynchronizedGroupingBlock left, DataShapeBindingAxisSynchronizedGroupingBlock right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxisSynchronizedGroupingBlock>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000160BD File Offset: 0x000142BD
		public static bool operator !=(DataShapeBindingAxisSynchronizedGroupingBlock left, DataShapeBindingAxisSynchronizedGroupingBlock right)
		{
			return !(left == right);
		}
	}
}
