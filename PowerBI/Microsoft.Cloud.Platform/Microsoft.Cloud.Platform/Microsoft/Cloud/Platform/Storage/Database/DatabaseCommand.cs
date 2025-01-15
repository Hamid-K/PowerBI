using System;
using System.Data.SqlClient;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200002F RID: 47
	internal sealed class DatabaseCommand
	{
		// Token: 0x060000FB RID: 251 RVA: 0x000046D5 File Offset: 0x000028D5
		public DatabaseCommand(SqlCommand cmd, IDatabaseSpecification specification)
		{
			this.Command = cmd;
			this.Specification = specification;
			cmd.CommandTimeout = specification.CommandTimeout;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000046F7 File Offset: 0x000028F7
		public SqlConnection Connection
		{
			get
			{
				return this.Command.Connection;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004704 File Offset: 0x00002904
		public void Open()
		{
			this.Command.Connection.Open();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004718 File Offset: 0x00002918
		public void Recycle()
		{
			SqlCommand command = this.Command;
			this.Command = command.Clone();
			command.Cancel();
			command.Dispose();
			this.Command.Connection.Dispose();
			this.Command.Connection = new SqlConnection(this.Specification.ConnectionString);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000476F File Offset: 0x0000296F
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00004777 File Offset: 0x00002977
		public SqlCommand Command { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004780 File Offset: 0x00002980
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00004788 File Offset: 0x00002988
		public IDatabaseSpecification Specification { get; private set; }
	}
}
