using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Data.SqlClient;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200007D RID: 125
	[Serializable]
	internal sealed class ReportServerStorageException : ReportCatalogException
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00004BD6 File Offset: 0x00002DD6
		public ReportServerStorageException(Exception innerException)
			: this(innerException, (innerException != null) ? innerException.Message : null)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00004BEB File Offset: 0x00002DEB
		public ReportServerStorageException(Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsReportServerDatabaseError, (innerException != null && innerException is SqlTypeException) ? innerException.Message : ErrorStringsWrapper.rsReportServerDatabaseError, innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00004C17 File Offset: 0x00002E17
		private ReportServerStorageException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00004C21 File Offset: 0x00002E21
		public bool IsSqlException
		{
			[DebuggerStepThrough]
			get
			{
				return base.InnerException is SqlException;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00004C31 File Offset: 0x00002E31
		public int SqlErrorNumber
		{
			get
			{
				if (this.IsSqlException)
				{
					return (base.InnerException as SqlException).Number;
				}
				return 0;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00004C4D File Offset: 0x00002E4D
		public string SqlErrorMessage
		{
			get
			{
				if (this.IsSqlException)
				{
					return (base.InnerException as SqlException).Message;
				}
				return null;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00004C69 File Offset: 0x00002E69
		public SqlErrorCollection SqlErrors
		{
			get
			{
				if (this.IsSqlException)
				{
					return (base.InnerException as SqlException).Errors;
				}
				return null;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00004C85 File Offset: 0x00002E85
		protected override bool TraceFullException
		{
			get
			{
				return false;
			}
		}
	}
}
