using System;
using System.Reflection;
using System.Transactions;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000036 RID: 54
	internal sealed class SqlRetryLogic : SqlRetryLogicBase
	{
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0000E975 File Offset: 0x0000CB75
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0000E97D File Offset: 0x0000CB7D
		public Predicate<string> PreCondition { get; private set; }

		// Token: 0x06000716 RID: 1814 RVA: 0x0000E988 File Offset: 0x0000CB88
		public SqlRetryLogic(int numberOfTries, SqlRetryIntervalBaseEnumerator enumerator, Predicate<Exception> transientPredicate, Predicate<string> preCondition)
		{
			if (numberOfTries <= 0 || numberOfTries > 60)
			{
				throw SqlReliabilityUtil.ArgumentOutOfRange("numberOfTries", numberOfTries, 1, 60);
			}
			base.NumberOfTries = numberOfTries;
			base.RetryIntervalEnumerator = enumerator;
			base.TransientPredicate = transientPredicate;
			this.PreCondition = preCondition;
			base.Current = 0;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0000E9D7 File Offset: 0x0000CBD7
		public SqlRetryLogic(int numberOfTries, SqlRetryIntervalBaseEnumerator enumerator, Predicate<Exception> transientPredicate)
			: this(numberOfTries, enumerator, transientPredicate, null)
		{
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000E9E3 File Offset: 0x0000CBE3
		public SqlRetryLogic(SqlRetryIntervalBaseEnumerator enumerator, Predicate<Exception> transientPredicate)
			: this(1, enumerator, transientPredicate)
		{
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0000E9EE File Offset: 0x0000CBEE
		public override void Reset()
		{
			base.Current = 0;
			base.RetryIntervalEnumerator.Reset();
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000EA04 File Offset: 0x0000CC04
		public override bool TryNextInterval(out TimeSpan intervalTime)
		{
			intervalTime = TimeSpan.Zero;
			bool flag = base.Current < base.NumberOfTries - 1;
			if (flag)
			{
				int num = base.Current;
				base.Current = num + 1;
				base.RetryIntervalEnumerator.MoveNext();
				intervalTime = base.RetryIntervalEnumerator.Current;
				SqlClientEventSource.Log.TryTraceEvent<string, string, TimeSpan, int>("<sc.{0}.{1}|INFO> Next gap time will be '{2}' before the next retry number {3}", "SqlRetryLogic", MethodBase.GetCurrentMethod().Name, intervalTime, base.Current);
			}
			else
			{
				SqlClientEventSource.Log.TryTraceEvent<string, string, int, int>("<sc.{0}.{1}|INFO> Current retry ({2}) has reached the maximum attempts (total attempts excluding the first run = {3}).", "SqlRetryLogic", MethodBase.GetCurrentMethod().Name, base.Current, base.NumberOfTries - 1);
			}
			return flag;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000EAB8 File Offset: 0x0000CCB8
		public override bool RetryCondition(object sender)
		{
			bool flag = true;
			SqlCommand sqlCommand = sender as SqlCommand;
			if (sqlCommand != null)
			{
				flag = Transaction.Current == null && sqlCommand.Transaction == null && (this.PreCondition == null || this.PreCondition(sqlCommand.CommandText));
				SqlClientEventSource.Log.TryTraceEvent<string, string, bool>("<sc.{0}.{1}|INFO> (retry condition = '{2}') Avoids retry if it runs in a transaction or is skipped in the command's statement checking.", "SqlRetryLogic", MethodBase.GetCurrentMethod().Name, flag);
			}
			return flag;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public override object Clone()
		{
			SqlRetryLogic sqlRetryLogic = new SqlRetryLogic(base.NumberOfTries, base.RetryIntervalEnumerator.Clone() as SqlRetryIntervalBaseEnumerator, base.TransientPredicate, this.PreCondition);
			sqlRetryLogic.RetryIntervalEnumerator.Reset();
			return sqlRetryLogic;
		}

		// Token: 0x040000BD RID: 189
		private const int counterDefaultValue = 0;

		// Token: 0x040000BE RID: 190
		private const int maxAttempts = 60;

		// Token: 0x040000BF RID: 191
		private const string TypeName = "SqlRetryLogic";
	}
}
