using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010AE RID: 4270
	public abstract class DelegatingDbCommand : DbCommand
	{
		// Token: 0x06006F99 RID: 28569 RVA: 0x00181400 File Offset: 0x0017F600
		public DelegatingDbCommand(DbCommand command)
		{
			this.command = command;
		}

		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x06006F9A RID: 28570 RVA: 0x0018140F File Offset: 0x0017F60F
		public DbCommand InnerCommand
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x06006F9B RID: 28571 RVA: 0x00181417 File Offset: 0x0017F617
		public override void Cancel()
		{
			this.command.Cancel();
		}

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x06006F9C RID: 28572 RVA: 0x00181424 File Offset: 0x0017F624
		// (set) Token: 0x06006F9D RID: 28573 RVA: 0x00181431 File Offset: 0x0017F631
		public override string CommandText
		{
			get
			{
				return this.command.CommandText;
			}
			set
			{
				this.command.CommandText = value;
			}
		}

		// Token: 0x17001F6B RID: 8043
		// (get) Token: 0x06006F9E RID: 28574 RVA: 0x0018143F File Offset: 0x0017F63F
		// (set) Token: 0x06006F9F RID: 28575 RVA: 0x0018144C File Offset: 0x0017F64C
		public override int CommandTimeout
		{
			get
			{
				return this.command.CommandTimeout;
			}
			set
			{
				this.command.CommandTimeout = value;
			}
		}

		// Token: 0x17001F6C RID: 8044
		// (get) Token: 0x06006FA0 RID: 28576 RVA: 0x0018145A File Offset: 0x0017F65A
		// (set) Token: 0x06006FA1 RID: 28577 RVA: 0x00181467 File Offset: 0x0017F667
		public override CommandType CommandType
		{
			get
			{
				return this.command.CommandType;
			}
			set
			{
				this.command.CommandType = value;
			}
		}

		// Token: 0x06006FA2 RID: 28578 RVA: 0x00181475 File Offset: 0x0017F675
		protected override DbParameter CreateDbParameter()
		{
			return this.command.CreateParameter();
		}

		// Token: 0x17001F6D RID: 8045
		// (get) Token: 0x06006FA3 RID: 28579 RVA: 0x00181482 File Offset: 0x0017F682
		// (set) Token: 0x06006FA4 RID: 28580 RVA: 0x0018148F File Offset: 0x0017F68F
		protected override DbConnection DbConnection
		{
			get
			{
				return this.command.Connection;
			}
			set
			{
				this.command.Connection = value;
			}
		}

		// Token: 0x17001F6E RID: 8046
		// (get) Token: 0x06006FA5 RID: 28581 RVA: 0x0018149D File Offset: 0x0017F69D
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.command.Parameters;
			}
		}

		// Token: 0x17001F6F RID: 8047
		// (get) Token: 0x06006FA6 RID: 28582 RVA: 0x001814AA File Offset: 0x0017F6AA
		// (set) Token: 0x06006FA7 RID: 28583 RVA: 0x001814B7 File Offset: 0x0017F6B7
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.command.Transaction;
			}
			set
			{
				this.command.Transaction = value;
			}
		}

		// Token: 0x17001F70 RID: 8048
		// (get) Token: 0x06006FA8 RID: 28584 RVA: 0x000091AE File Offset: 0x000073AE
		// (set) Token: 0x06006FA9 RID: 28585 RVA: 0x000091AE File Offset: 0x000073AE
		public override bool DesignTimeVisible
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06006FAA RID: 28586 RVA: 0x001814C5 File Offset: 0x0017F6C5
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.command.ExecuteReader(behavior);
		}

		// Token: 0x06006FAB RID: 28587 RVA: 0x001814D3 File Offset: 0x0017F6D3
		public override int ExecuteNonQuery()
		{
			return this.command.ExecuteNonQuery();
		}

		// Token: 0x06006FAC RID: 28588 RVA: 0x001814E0 File Offset: 0x0017F6E0
		public override object ExecuteScalar()
		{
			return this.command.ExecuteScalar();
		}

		// Token: 0x06006FAD RID: 28589 RVA: 0x001814ED File Offset: 0x0017F6ED
		public override void Prepare()
		{
			this.command.Prepare();
		}

		// Token: 0x17001F71 RID: 8049
		// (get) Token: 0x06006FAE RID: 28590 RVA: 0x001814FA File Offset: 0x0017F6FA
		// (set) Token: 0x06006FAF RID: 28591 RVA: 0x00181507 File Offset: 0x0017F707
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this.command.UpdatedRowSource;
			}
			set
			{
				this.command.UpdatedRowSource = value;
			}
		}

		// Token: 0x06006FB0 RID: 28592 RVA: 0x00181515 File Offset: 0x0017F715
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.command.Dispose();
			}
		}

		// Token: 0x04003DF1 RID: 15857
		private readonly DbCommand command;
	}
}
