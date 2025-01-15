using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010A8 RID: 4264
	internal class DbValueVersions : ValueVersions
	{
		// Token: 0x06006F7F RID: 28543 RVA: 0x00181124 File Offset: 0x0017F324
		public DbValueVersions(DbEnvironment environment, Func<DbEnvironment, Value> valueCtor)
		{
			this.environment = environment;
			this.valueCtor = valueCtor;
		}

		// Token: 0x17001F65 RID: 8037
		// (get) Token: 0x06006F80 RID: 28544 RVA: 0x0018113A File Offset: 0x0017F33A
		protected override IEngineHost Host
		{
			get
			{
				return this.environment.Host;
			}
		}

		// Token: 0x06006F81 RID: 28545 RVA: 0x00181147 File Offset: 0x0017F347
		protected override void VerifyActionPermitted()
		{
			this.environment.VerifyActionPermitted();
		}

		// Token: 0x06006F82 RID: 28546 RVA: 0x00181154 File Offset: 0x0017F354
		protected override bool TryCreateVersion(string identity)
		{
			return this.environment.TryCreateVersion(identity);
		}

		// Token: 0x06006F83 RID: 28547 RVA: 0x00181162 File Offset: 0x0017F362
		protected override IEnumerable<ValueVersions.ValueVersion> GetVersions()
		{
			foreach (DbTransactionInfo dbTransactionInfo in new DbTransactionInfo[1].Concat(this.environment.GetRegisteredTransactions()))
			{
				yield return new DbValueVersions.DbValueVersion(this, dbTransactionInfo);
			}
			IEnumerator<DbTransactionInfo> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04003DE5 RID: 15845
		private readonly DbEnvironment environment;

		// Token: 0x04003DE6 RID: 15846
		private readonly Func<DbEnvironment, Value> valueCtor;

		// Token: 0x020010A9 RID: 4265
		private class DbValueVersion : ValueVersions.ValueVersion
		{
			// Token: 0x06006F84 RID: 28548 RVA: 0x00181172 File Offset: 0x0017F372
			public DbValueVersion(DbValueVersions valueVersions, DbTransactionInfo transaction)
			{
				this.valueVersions = valueVersions;
				this.transaction = transaction;
			}

			// Token: 0x17001F66 RID: 8038
			// (get) Token: 0x06006F85 RID: 28549 RVA: 0x00181188 File Offset: 0x0017F388
			public override string Identity
			{
				get
				{
					DbTransactionInfo dbTransactionInfo = this.transaction;
					if (dbTransactionInfo == null)
					{
						return null;
					}
					return dbTransactionInfo.Identity;
				}
			}

			// Token: 0x06006F86 RID: 28550 RVA: 0x0018119C File Offset: 0x0017F39C
			public override bool TryCreateValue(out IValueReference value)
			{
				DbEnvironment dbEnvironment;
				if (this.valueVersions.environment.TryCreateTransactedEnvironment(this.transaction, out dbEnvironment))
				{
					value = this.valueVersions.valueCtor(dbEnvironment);
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x06006F87 RID: 28551 RVA: 0x001811DC File Offset: 0x0017F3DC
			public override bool TryCommit()
			{
				DbEnvironment dbEnvironment;
				return this.valueVersions.environment.TryCreateTransactedEnvironment(this.transaction, out dbEnvironment) && dbEnvironment.TryCommitVersion();
			}

			// Token: 0x04003DE7 RID: 15847
			private readonly DbValueVersions valueVersions;

			// Token: 0x04003DE8 RID: 15848
			private readonly DbTransactionInfo transaction;
		}
	}
}
