using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ProgressivePackaging;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200018B RID: 395
	internal abstract class ProgressivePackageActionBase : IDisposable
	{
		// Token: 0x06000E8E RID: 3726 RVA: 0x0003549F File Offset: 0x0003369F
		public ProgressivePackageActionBase(Stream outputStream, IList<string> responseFlags, RSService service)
			: this(outputStream, responseFlags, service, false)
		{
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000354AC File Offset: 0x000336AC
		public ProgressivePackageActionBase(Stream outputStream, IList<string> responseFlags, RSService service, bool messageWriterLazyInitialization)
		{
			RSTrace.CatalogTrace.Assert(outputStream != null, "ProgressivePackageActionBase.ctor: outputStream != null");
			RSTrace.CatalogTrace.Assert(responseFlags != null, "ProgressivePackageActionBase.ctor: responseFlags != null");
			RSTrace.CatalogTrace.Assert(service != null, "ProgressivePackageActionBase.ctor: service != null");
			this.m_messageWriterLazyInitialization = messageWriterLazyInitialization;
			this.m_outputStream = outputStream;
			this.m_responseFlags = responseFlags;
			this.m_service = service;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00035518 File Offset: 0x00033718
		protected bool Initialize()
		{
			if (!this.m_messageWriterLazyInitialization)
			{
				try
				{
					this.m_messageWriter = MessageFormatterFactory.CreateWriter(this.m_outputStream, "Progressive", 1, 0);
				}
				catch (Exception ex)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0} failed to initialize message writer: {1}", new object[]
					{
						this.OperationName,
						ex.ToString()
					});
					return false;
				}
			}
			this.m_provider = new RSServiceDataProvider(this.m_service);
			return this.InitializeAction();
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000355A4 File Offset: 0x000337A4
		protected void MessageWriterIsNull()
		{
			RSTrace.CatalogTrace.Assert(this.m_messageWriter == null, "message writer has already been initialized.");
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x000355C0 File Offset: 0x000337C0
		protected IMessageWriter MessageWriter
		{
			get
			{
				if (this.m_messageWriter == null)
				{
					try
					{
						this.m_messageWriter = MessageFormatterFactory.CreateWriter(this.m_outputStream, "Progressive", 1, 0);
					}
					catch (Exception ex)
					{
						throw new ProgressiveMessageWriteException(this.OperationName, ex, "Failed to delay initialize message writer.");
					}
				}
				return this.m_messageWriter;
			}
		}

		// Token: 0x06000E93 RID: 3731
		protected abstract bool InitializeAction();

		// Token: 0x06000E94 RID: 3732 RVA: 0x00005BF2 File Offset: 0x00003DF2
		public virtual void AddMetadata(string name, string value)
		{
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00035618 File Offset: 0x00033818
		public void Execute()
		{
			ErrorCode errorCode = ErrorCode.rsSuccess;
			this.m_service.WillDisconnectStorage();
			try
			{
				if (this.Initialize())
				{
					this.ExecuteAction();
				}
			}
			catch (ProgressiveMessageWriteException ex)
			{
				this.m_service.AbortTransaction();
				errorCode = ex.Code;
				ProgressiveReportCounters.Current.FailuresTotal.Increment();
				this.CleanupForException();
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0} encountered ProgressiveMessageWriteException {1}", new object[]
				{
					this.OperationName,
					ex.ToString()
				});
			}
			catch (Exception ex2)
			{
				this.m_service.AbortTransaction();
				this.CleanupForException();
				Exception exceptionToWrite = StreamRequestHandler.GetExceptionToWrite(ex2, out errorCode);
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0} encountered Exception {1}", new object[]
				{
					this.OperationName,
					exceptionToWrite.ToString()
				});
				if (this.ThrowOnExecutionError)
				{
					if (ex2 == exceptionToWrite)
					{
						throw;
					}
					throw exceptionToWrite;
				}
				else
				{
					try
					{
						this.WriteServerError(exceptionToWrite);
					}
					catch (Exception ex3)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0} failed to return error message {1}", new object[]
						{
							this.OperationName,
							ex3.ToString()
						});
					}
				}
			}
			finally
			{
				this.m_service.DisconnectStorage();
				this.FinalCleanup(errorCode);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected virtual bool ThrowOnExecutionError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00035774 File Offset: 0x00033974
		protected void WriteServerError(Exception e)
		{
			ProgressiveReportCounters.Current.FailuresTotal.Increment();
			RSException ex = e as RSException;
			if (ex == null)
			{
				ex = new InternalCatalogException(e, null);
			}
			this.WriteErrorMessage("serverErrorCode", ex.Code.ToString());
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x000357C1 File Offset: 0x000339C1
		protected void WriteSessionNotFoundError()
		{
			this.WriteSessionError("SessionNotFound");
		}

		// Token: 0x06000E99 RID: 3737
		protected abstract void ExecuteAction();

		// Token: 0x06000E9A RID: 3738
		protected abstract void CleanupForException();

		// Token: 0x06000E9B RID: 3739
		protected abstract void FinalCleanup(ErrorCode status);

		// Token: 0x06000E9C RID: 3740 RVA: 0x000357CE File Offset: 0x000339CE
		protected virtual void WriteErrorMessage(string errorKey, string error)
		{
			this.MessageWriter.WriteMessage(errorKey, error);
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000E9D RID: 3741
		protected abstract string OperationName { get; }

		// Token: 0x06000E9E RID: 3742 RVA: 0x000357DD File Offset: 0x000339DD
		public void Dispose()
		{
			if (!this.m_disposed)
			{
				if (this.m_messageWriter != null)
				{
					this.m_messageWriter.Dispose();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00035804 File Offset: 0x00033A04
		protected bool EnsureValidSessionExists(IRenderEditSession session, out ProgressiveCacheEntry entry)
		{
			entry = null;
			try
			{
				session.EnsureValidSessionExists(this.m_service.UserName, this.OperationName, out entry);
			}
			catch (SessionNotFoundException)
			{
				this.WriteSessionNotFoundError();
				return false;
			}
			catch (InvalidSessionIdException)
			{
				this.WriteErrorMessage("serverErrorCode", "InvalidSessionId");
				return false;
			}
			return true;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00035870 File Offset: 0x00033A70
		protected virtual void WriteSessionError(string error)
		{
			this.WriteErrorMessage("serverErrorCode", error);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00035880 File Offset: 0x00033A80
		protected bool ValidateSession(IRenderEditSession session)
		{
			bool flag;
			try
			{
				session.ValidateSession();
				flag = true;
			}
			catch (InvalidSessionIdException)
			{
				this.WriteSessionError("InvalidSessionId");
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000358B8 File Offset: 0x00033AB8
		protected bool TryGetProgressivePackageReader(Stream inputStream, out ProgressivePackageReader reader)
		{
			try
			{
				reader = new ProgressivePackageReader(inputStream);
			}
			catch (Exception ex)
			{
				this.ProcessReaderException(ex);
				reader = null;
				return false;
			}
			return true;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000358F4 File Offset: 0x00033AF4
		private void ProcessReaderException(Exception e)
		{
			RSException ex = new InvalidProgressiveFormatException(this.OperationName, e, null);
			try
			{
				this.WriteServerError(ex);
			}
			catch (Exception ex2)
			{
				ex = new ProgressiveMessageWriteException(this.OperationName, ex, ex2.ToString());
			}
		}

		// Token: 0x04000600 RID: 1536
		protected readonly Stream m_outputStream;

		// Token: 0x04000601 RID: 1537
		protected readonly RSService m_service;

		// Token: 0x04000602 RID: 1538
		private readonly IList<string> m_responseFlags;

		// Token: 0x04000603 RID: 1539
		private bool m_messageWriterLazyInitialization;

		// Token: 0x04000604 RID: 1540
		private IMessageWriter m_messageWriter;

		// Token: 0x04000605 RID: 1541
		private bool m_disposed;

		// Token: 0x04000606 RID: 1542
		protected IExecutionDataProvider m_provider;
	}
}
