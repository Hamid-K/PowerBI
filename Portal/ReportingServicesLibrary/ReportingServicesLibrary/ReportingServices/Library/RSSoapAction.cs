using System;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000A6 RID: 166
	internal abstract class RSSoapAction<P> where P : RSSoapActionParameters, new()
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x0001FD38 File Offset: 0x0001DF38
		internal RSSoapAction(string actionName, RSService service)
		{
			this.m_actionName = actionName;
			this.m_service = service;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001FD64 File Offset: 0x0001DF64
		internal virtual ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionManager.DefaultTransactionType;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0001FD6B File Offset: 0x0001DF6B
		protected virtual IsolationLevel IsolationLevel
		{
			get
			{
				return ConnectionManager.DefaultIsolationLevel;
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected virtual void FinalizeAction()
		{
		}

		// Token: 0x060007B1 RID: 1969
		internal abstract void PerformActionNow();

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001FD72 File Offset: 0x0001DF72
		protected virtual void AddActionToBatch()
		{
			throw new InternalCatalogException(this.ActionName + " SOAP action cannot be part of a batch.");
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001FD72 File Offset: 0x0001DF72
		internal virtual void PerformActionInBatch(CallParameters parameters)
		{
			throw new InternalCatalogException(this.ActionName + " SOAP action cannot be part of a batch.");
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001FD8C File Offset: 0x0001DF8C
		public void Execute()
		{
			this.ActionParameters.Validate();
			try
			{
				this.Service.WillDisconnectStorage();
				if (this.BatchID != Guid.Empty)
				{
					this.AddActionToBatch();
				}
				else
				{
					if (RSTrace.CatalogTrace.TraceInfo)
					{
						this.TraceInput();
					}
					this.Service.SetDatabaseConnectionSettings(this.TransactionType, this.IsolationLevel);
					this.PerformActionNow();
					if (RSTrace.CatalogTrace.TraceVerbose)
					{
						this.TraceOutput();
					}
				}
			}
			catch (Exception ex)
			{
				this.Service.AbortTransaction();
				if (ex is RSException)
				{
					if (ex is ReportServerStorageException && RSTrace.CatalogTrace.TraceError)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Error, ex.ToString());
					}
					throw;
				}
				throw new InternalCatalogException(ex, null);
			}
			finally
			{
				this.Service.DisconnectStorage();
				this.FinalizeAction();
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001FE84 File Offset: 0x0001E084
		private void TraceInput()
		{
			RSTrace.CatalogTrace.Trace("Call to {0}({1}). User: {2}.", new object[]
			{
				this.ActionName,
				StringUtils.RemoveControlAndNonSpacesWhitespaceCharacters(this.ActionParameters.InputTrace),
				this.Service.UserName
			});
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		protected void BatchTraceInput()
		{
			if (this.BatchID != Guid.Empty && RSTrace.CatalogTrace.TraceVerbose)
			{
				RSTrace.CatalogTrace.Trace("Batch excution call to {0}({1}). User: {2}.", new object[]
				{
					this.ActionName,
					StringUtils.RemoveControlAndNonSpacesWhitespaceCharacters(this.ActionParameters.InputTrace),
					this.Service.UserName
				});
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001FF48 File Offset: 0x0001E148
		private void TraceOutput()
		{
			string outputTrace = this.ActionParameters.OutputTrace;
			if (outputTrace != null && outputTrace.Length > 0)
			{
				RSTrace.CatalogTrace.Trace("Call to {0} completed. Returns {1}.", new object[]
				{
					this.ActionName,
					StringUtils.RemoveControlAndNonSpacesWhitespaceCharacters(outputTrace)
				});
				return;
			}
			RSTrace.CatalogTrace.Trace("Call to {0} completed.", new object[] { this.ActionName });
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001FFB8 File Offset: 0x0001E1B8
		public P ActionParameters
		{
			get
			{
				return this.m_actionParameters;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0001FFC8 File Offset: 0x0001E1C8
		public Guid BatchID
		{
			get
			{
				return this.m_batchID;
			}
			set
			{
				this.m_batchID = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0001FFD1 File Offset: 0x0001E1D1
		protected RSService Service
		{
			get
			{
				return this.m_service;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001FFD9 File Offset: 0x0001E1D9
		protected string ActionName
		{
			get
			{
				return this.m_actionName;
			}
		}

		// Token: 0x040003F8 RID: 1016
		protected P m_actionParameters = new P();

		// Token: 0x040003F9 RID: 1017
		private Guid m_batchID = Guid.Empty;

		// Token: 0x040003FA RID: 1018
		private RSService m_service;

		// Token: 0x040003FB RID: 1019
		private string m_actionName;
	}
}
