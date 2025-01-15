using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200107E RID: 4222
	internal sealed class WrappedDbCommand : DelegatingDbCommand
	{
		// Token: 0x06006E9A RID: 28314 RVA: 0x0017DF20 File Offset: 0x0017C120
		public WrappedDbCommand(DbEnvironment environment, DbCommand baseCommand, Func<IDisposable> impersonate)
			: base(baseCommand)
		{
			this.environment = environment;
			this.impersonate = impersonate;
			this.CommandTimeout = environment.CommandTimeout.GetValueOrDefault(600);
		}

		// Token: 0x06006E9B RID: 28315 RVA: 0x0017DF5C File Offset: 0x0017C15C
		public override void Cancel()
		{
			using (this.impersonate())
			{
				base.Cancel();
			}
		}

		// Token: 0x06006E9C RID: 28316 RVA: 0x0017DF98 File Offset: 0x0017C198
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			DbDataReader dbDataReader;
			using (this.impersonate())
			{
				dbDataReader = this.environment.WrapDbDataReader(base.ExecuteDbDataReader(behavior).WithTableSchema());
			}
			return dbDataReader;
		}

		// Token: 0x06006E9D RID: 28317 RVA: 0x0017DFE8 File Offset: 0x0017C1E8
		public override int ExecuteNonQuery()
		{
			int num;
			using (this.impersonate())
			{
				num = base.ExecuteNonQuery();
			}
			return num;
		}

		// Token: 0x06006E9E RID: 28318 RVA: 0x0017E028 File Offset: 0x0017C228
		public override object ExecuteScalar()
		{
			object obj;
			using (this.impersonate())
			{
				obj = base.ExecuteScalar();
			}
			return obj;
		}

		// Token: 0x06006E9F RID: 28319 RVA: 0x0017E068 File Offset: 0x0017C268
		public override void Prepare()
		{
			using (this.impersonate())
			{
				base.Prepare();
			}
		}

		// Token: 0x06006EA0 RID: 28320 RVA: 0x0017E0A4 File Offset: 0x0017C2A4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				using (this.impersonate())
				{
					base.Dispose(disposing);
					return;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04003D5E RID: 15710
		private const int DefaultCommandTimeoutInSeconds = 600;

		// Token: 0x04003D5F RID: 15711
		private readonly DbEnvironment environment;

		// Token: 0x04003D60 RID: 15712
		private readonly Func<IDisposable> impersonate;
	}
}
