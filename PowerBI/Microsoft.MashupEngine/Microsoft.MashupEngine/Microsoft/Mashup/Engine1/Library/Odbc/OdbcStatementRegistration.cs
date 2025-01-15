using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000668 RID: 1640
	internal sealed class OdbcStatementRegistration : ICancellable
	{
		// Token: 0x060033B6 RID: 13238 RVA: 0x000A5554 File Offset: 0x000A3754
		public OdbcStatementRegistration(ICancellationService cancellationService, OdbcStatementHandle statement)
		{
			this.syncRoot = new object();
			this.cancellationService = cancellationService;
			this.statement = statement;
			this.cancellationService.Register(this);
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x000A5584 File Offset: 0x000A3784
		public OdbcStatementHandle Statement
		{
			get
			{
				object obj = this.syncRoot;
				OdbcStatementHandle odbcStatementHandle;
				lock (obj)
				{
					if (this.cancelled)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.EvaluationCanceled, null, null);
					}
					odbcStatementHandle = this.statement;
				}
				return odbcStatementHandle;
			}
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000A55DC File Offset: 0x000A37DC
		public bool Cancel()
		{
			object obj = this.syncRoot;
			OdbcStatementHandle odbcStatementHandle;
			lock (obj)
			{
				odbcStatementHandle = this.statement;
				this.statement = null;
				this.cancelled = true;
			}
			if (odbcStatementHandle != null)
			{
				Odbc32.RetCode retCode = odbcStatementHandle.Cancel();
				return retCode != Odbc32.RetCode.ERROR && retCode != Odbc32.RetCode.INVALID_HANDLE;
			}
			return false;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000A5648 File Offset: 0x000A3848
		public void Unregister()
		{
			object obj = this.syncRoot;
			OdbcStatementHandle odbcStatementHandle;
			lock (obj)
			{
				odbcStatementHandle = this.statement;
				this.statement = null;
			}
			if (odbcStatementHandle != null)
			{
				this.cancellationService.Unregister(this);
			}
		}

		// Token: 0x04001701 RID: 5889
		private readonly object syncRoot;

		// Token: 0x04001702 RID: 5890
		private readonly ICancellationService cancellationService;

		// Token: 0x04001703 RID: 5891
		private OdbcStatementHandle statement;

		// Token: 0x04001704 RID: 5892
		private bool cancelled;
	}
}
