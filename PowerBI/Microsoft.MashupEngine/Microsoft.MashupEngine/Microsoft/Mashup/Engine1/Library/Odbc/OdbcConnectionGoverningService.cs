using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005CF RID: 1487
	internal sealed class OdbcConnectionGoverningService : OdbcDelegatingService
	{
		// Token: 0x06002E77 RID: 11895 RVA: 0x0008D946 File Offset: 0x0008BB46
		public OdbcConnectionGoverningService(IEngineHost host, IResource resource, IOdbcService odbc)
			: base(odbc)
		{
			this.host = host;
			this.resource = resource;
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x0008D95D File Offset: 0x0008BB5D
		public override IOdbcConnection CreateConnection(OdbcConnectionProperties args)
		{
			return new OdbcConnectionGoverningService.GovernedOdbcConnection(this.host, this.resource, base.InnerService.CreateConnection(args), args.IsMetadataConnection);
		}

		// Token: 0x04001477 RID: 5239
		private readonly IEngineHost host;

		// Token: 0x04001478 RID: 5240
		private readonly IResource resource;

		// Token: 0x020005D0 RID: 1488
		private class GovernedOdbcConnection : OdbcDelegatingConnection
		{
			// Token: 0x06002E79 RID: 11897 RVA: 0x0008D982 File Offset: 0x0008BB82
			public GovernedOdbcConnection(IEngineHost host, IResource resource, IOdbcConnection connection, bool isMetadataConnection)
				: base(connection)
			{
				this.host = host;
				this.resource = resource;
				this.isMetadataConnection = isMetadataConnection;
			}

			// Token: 0x06002E7A RID: 11898 RVA: 0x0008D9A1 File Offset: 0x0008BBA1
			public override void Open()
			{
				if (!this.isMetadataConnection && this.handle == null)
				{
					this.handle = HostResourcePermissionService.WaitForGovernedHandle(this.host, this.resource);
				}
				base.Open();
			}

			// Token: 0x06002E7B RID: 11899 RVA: 0x0008D9D0 File Offset: 0x0008BBD0
			public override void Dispose()
			{
				base.Dispose();
				if (this.handle != null)
				{
					this.handle.Dispose();
					this.handle = null;
				}
			}

			// Token: 0x06002E7C RID: 11900 RVA: 0x0008D9F2 File Offset: 0x0008BBF2
			public override IPageReader Execute(OdbcStatementHandle statement, IList<OdbcParameter> parameters, RowRange rowRange)
			{
				if (this.isMetadataConnection)
				{
					throw new Exception("Method not allowed for metadata connection");
				}
				return base.Execute(statement, parameters, rowRange);
			}

			// Token: 0x06002E7D RID: 11901 RVA: 0x0008DA10 File Offset: 0x0008BC10
			public override IPageReader ExecuteDirect(string commandText, IList<OdbcParameter> parameters, RowRange rowRange, IOdbcStatementRegistrar statementRegistrar)
			{
				if (this.isMetadataConnection)
				{
					throw new Exception("Method not allowed for metadata connection");
				}
				return base.ExecuteDirect(commandText, parameters, rowRange, statementRegistrar);
			}

			// Token: 0x06002E7E RID: 11902 RVA: 0x0008DA30 File Offset: 0x0008BC30
			public override long ExecuteNonQueryDirect(string commandText, IList<OdbcParameter> parameters, IOdbcStatementRegistrar statementRegistrar)
			{
				if (this.isMetadataConnection)
				{
					throw new Exception("Method not allowed for metadata connection");
				}
				return base.ExecuteNonQueryDirect(commandText, parameters, statementRegistrar);
			}

			// Token: 0x04001479 RID: 5241
			private readonly IEngineHost host;

			// Token: 0x0400147A RID: 5242
			private readonly IResource resource;

			// Token: 0x0400147B RID: 5243
			private readonly bool isMetadataConnection;

			// Token: 0x0400147C RID: 5244
			private IDisposable handle;
		}
	}
}
