using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200018B RID: 395
	[DataContract(Name = "DataWindow", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionDataWindow : IEquatable<DataReductionDataWindow>
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0001505D File Offset: 0x0001325D
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x00015065 File Offset: 0x00013265
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public int? Count { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0001506E File Offset: 0x0001326E
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x00015076 File Offset: 0x00013276
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<IList<string>> RestartTokens { get; set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0001507F File Offset: 0x0001327F
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x00015087 File Offset: 0x00013287
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public RestartMatchingBehavior? RestartMatchingBehavior { get; set; }

		// Token: 0x06000A94 RID: 2708 RVA: 0x00015090 File Offset: 0x00013290
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionDataWindow);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001509E File Offset: 0x0001329E
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<int?>(this.Count, null), Hashing.GetHashCode<IList<IList<string>>>(this.RestartTokens, null), Hashing.GetHashCode<RestartMatchingBehavior?>(this.RestartMatchingBehavior, null));
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x000150C9 File Offset: 0x000132C9
		private static int GetRestartTokensHashCode(IList<IList<string>> restartTokens)
		{
			return Hashing.CombineHash<IList<string>>(restartTokens, DataReductionDataWindow.RestartTokenEqualityComparer.Instance);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000150D8 File Offset: 0x000132D8
		public bool Equals(DataReductionDataWindow other)
		{
			bool? flag = Util.AreEqual<DataReductionDataWindow>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			int? count = this.Count;
			int? count2 = other.Count;
			if (((count.GetValueOrDefault() == count2.GetValueOrDefault()) & (count != null == (count2 != null))) && DataReductionDataWindow.EqualRestartTokens(this.RestartTokens, other.RestartTokens))
			{
				RestartMatchingBehavior? restartMatchingBehavior = this.RestartMatchingBehavior;
				RestartMatchingBehavior? restartMatchingBehavior2 = other.RestartMatchingBehavior;
				return (restartMatchingBehavior.GetValueOrDefault() == restartMatchingBehavior2.GetValueOrDefault()) & (restartMatchingBehavior != null == (restartMatchingBehavior2 != null));
			}
			return false;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00015174 File Offset: 0x00013374
		private static bool EqualRestartTokens(IList<IList<string>> tokens, IList<IList<string>> otherTokens)
		{
			bool? flag = Util.AreEqual<IList<IList<string>>>(tokens, otherTokens);
			if (flag != null)
			{
				return flag.Value;
			}
			DataReductionDataWindow.RestartTokenEqualityComparer instance = DataReductionDataWindow.RestartTokenEqualityComparer.Instance;
			return tokens.SequenceEqual(otherTokens, DataReductionDataWindow.RestartTokenEqualityComparer.Instance);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000151AC File Offset: 0x000133AC
		public static bool operator ==(DataReductionDataWindow left, DataReductionDataWindow right)
		{
			bool? flag = Util.AreEqual<DataReductionDataWindow>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000151D9 File Offset: 0x000133D9
		public static bool operator !=(DataReductionDataWindow left, DataReductionDataWindow right)
		{
			return !(left == right);
		}

		// Token: 0x02000322 RID: 802
		private sealed class RestartTokenEqualityComparer : IEqualityComparer<IList<string>>
		{
			// Token: 0x060019C5 RID: 6597 RVA: 0x0002E682 File Offset: 0x0002C882
			private RestartTokenEqualityComparer()
			{
			}

			// Token: 0x1700054F RID: 1359
			// (get) Token: 0x060019C6 RID: 6598 RVA: 0x0002E68A File Offset: 0x0002C88A
			internal static DataReductionDataWindow.RestartTokenEqualityComparer Instance
			{
				get
				{
					if (DataReductionDataWindow.RestartTokenEqualityComparer._instance == null)
					{
						DataReductionDataWindow.RestartTokenEqualityComparer._instance = new DataReductionDataWindow.RestartTokenEqualityComparer();
					}
					return DataReductionDataWindow.RestartTokenEqualityComparer._instance;
				}
			}

			// Token: 0x060019C7 RID: 6599 RVA: 0x0002E6A4 File Offset: 0x0002C8A4
			public bool Equals(IList<string> left, IList<string> right)
			{
				bool? flag = Util.AreEqual<IList<string>>(left, right);
				if (flag != null)
				{
					return flag.Value;
				}
				return left.SequenceEqual(right);
			}

			// Token: 0x060019C8 RID: 6600 RVA: 0x0002E6D1 File Offset: 0x0002C8D1
			public int GetHashCode(IList<string> token)
			{
				if (token == null)
				{
					return 0;
				}
				return Hashing.CombineHash<string>(token, null);
			}

			// Token: 0x04000996 RID: 2454
			private static DataReductionDataWindow.RestartTokenEqualityComparer _instance;
		}
	}
}
