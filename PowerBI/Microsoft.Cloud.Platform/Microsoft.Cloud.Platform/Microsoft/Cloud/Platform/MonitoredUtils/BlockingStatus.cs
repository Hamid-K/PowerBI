using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000126 RID: 294
	[CannotApplyEqualityOperator]
	public sealed class BlockingStatus<TKey> : IEquatable<BlockingStatus<TKey>>
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x0001B5AC File Offset: 0x000197AC
		public BlockingStatus([NotNull] TKey key)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<TKey>(key, "key");
			this.Key = key;
			this.IsBlocked = false;
			this.BlockingEndTime = null;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001B5E7 File Offset: 0x000197E7
		public BlockingStatus([NotNull] TKey key, DateTime blockingEndTime)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<TKey>(key, "key");
			this.Key = key;
			this.IsBlocked = true;
			this.BlockingEndTime = new DateTime?(blockingEndTime);
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001B614 File Offset: 0x00019814
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0001B61C File Offset: 0x0001981C
		public TKey Key { get; private set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001B625 File Offset: 0x00019825
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0001B62D File Offset: 0x0001982D
		public bool IsBlocked { get; private set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x0001B636 File Offset: 0x00019836
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x0001B63E File Offset: 0x0001983E
		public DateTime? BlockingEndTime { get; private set; }

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001B648 File Offset: 0x00019848
		public bool Equals(BlockingStatus<TKey> other)
		{
			if (other != null)
			{
				TKey key = this.Key;
				return key.Equals(other.Key) && this.IsBlocked.Equals(other.IsBlocked) && this.BlockingEndTime.Equals(other.BlockingEndTime);
			}
			return false;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001B6B2 File Offset: 0x000198B2
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BlockingStatus<TKey>);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001B6C0 File Offset: 0x000198C0
		public override int GetHashCode()
		{
			TKey key = this.Key;
			return key.GetHashCode();
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001B6E1 File Offset: 0x000198E1
		public override string ToString()
		{
			return "<Key={0} IsBlocked={1} BlockingEndTime={2}>".FormatWithInvariantCulture(new object[] { this.Key, this.IsBlocked, this.BlockingEndTime });
		}
	}
}
