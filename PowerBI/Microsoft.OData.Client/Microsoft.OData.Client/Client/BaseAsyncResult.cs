using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;

namespace Microsoft.OData.Client
{
	// Token: 0x020000B0 RID: 176
	internal abstract class BaseAsyncResult : IAsyncResult
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x00018ED0 File Offset: 0x000170D0
		internal BaseAsyncResult(object source, string method, AsyncCallback callback, object state)
		{
			this.Source = source;
			this.Method = method;
			this.userCallback = callback;
			this.userState = state;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x00018EFC File Offset: 0x000170FC
		public object AsyncState
		{
			get
			{
				return this.userState;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00018F04 File Offset: 0x00017104
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.asyncWait == null)
				{
					Interlocked.CompareExchange<ManualResetEvent>(ref this.asyncWait, new ManualResetEvent(this.IsCompleted), null);
					if (this.IsCompleted)
					{
						this.SetAsyncWaitHandle();
					}
				}
				return this.asyncWait;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00018F3A File Offset: 0x0001713A
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously == 1;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00018F45 File Offset: 0x00017145
		public bool IsCompleted
		{
			get
			{
				return this.userCompleted;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00018F4D File Offset: 0x0001714D
		internal bool IsCompletedInternally
		{
			get
			{
				return this.completed != 0;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00018F58 File Offset: 0x00017158
		internal bool IsAborted
		{
			get
			{
				return 2 == this.completed;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00018F63 File Offset: 0x00017163
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x00018F6B File Offset: 0x0001716B
		internal ODataRequestMessageWrapper Abortable
		{
			get
			{
				return this.abortable;
			}
			set
			{
				this.abortable = value;
				if (value != null && this.IsAborted)
				{
					value.Abort();
				}
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x00018F85 File Offset: 0x00017185
		internal Exception Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00018F90 File Offset: 0x00017190
		internal static T EndExecute<T>(object source, string method, IAsyncResult asyncResult) where T : BaseAsyncResult
		{
			Util.CheckArgumentNull<IAsyncResult>(asyncResult, "asyncResult");
			T t = asyncResult as T;
			if (t == null || source != t.Source || t.Method != method)
			{
				throw Error.Argument(Strings.Context_DidNotOriginateAsync, "asyncResult");
			}
			if (!t.IsCompleted)
			{
				t.AsyncWaitHandle.WaitOne();
			}
			if (Interlocked.Exchange(ref t.done, 1) != 0)
			{
				throw Error.Argument(Strings.Context_AsyncAlreadyDone, "asyncResult");
			}
			if (t.asyncWait != null)
			{
				Interlocked.CompareExchange(ref t.asyncWaitDisposeLock, new object(), null);
				object obj = t.asyncWaitDisposeLock;
				lock (obj)
				{
					t.asyncWaitDisposed = true;
					Util.Dispose<ManualResetEvent>(t.asyncWait);
				}
			}
			if (t.IsAborted)
			{
				throw Error.InvalidOperation(Strings.Context_OperationCanceled);
			}
			if (t.Failure == null)
			{
				return t;
			}
			if (Util.IsKnownClientExcption(t.Failure))
			{
				throw t.Failure;
			}
			throw Error.InvalidOperation(Strings.DataServiceException_GeneralError, t.Failure);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000190FC File Offset: 0x000172FC
		internal static IAsyncResult InvokeAsync(Func<AsyncCallback, object, IAsyncResult> asyncAction, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult = asyncAction(BaseAsyncResult.GetDataServiceAsyncCallback(callback), state);
			return BaseAsyncResult.PostInvokeAsync(asyncResult, callback);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00019120 File Offset: 0x00017320
		internal static IAsyncResult InvokeAsync(BaseAsyncResult.AsyncAction asyncAction, byte[] buffer, int offset, int length, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult = asyncAction(buffer, offset, length, BaseAsyncResult.GetDataServiceAsyncCallback(callback), state);
			return BaseAsyncResult.PostInvokeAsync(asyncResult, callback);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00019148 File Offset: 0x00017348
		internal void SetCompletedSynchronously(bool isCompletedSynchronously)
		{
			Interlocked.CompareExchange(ref this.completedSynchronously, isCompletedSynchronously ? 1 : 0, 1);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00019160 File Offset: 0x00017360
		internal void HandleCompleted()
		{
			if (this.IsCompletedInternally && Interlocked.Exchange(ref this.userNotified, 1) == 0)
			{
				this.abortable = null;
				try
				{
					if (CommonUtil.IsCatchableExceptionType(this.Failure))
					{
						this.CompletedRequest();
					}
				}
				catch (Exception ex)
				{
					if (this.HandleFailure(ex))
					{
						throw;
					}
				}
				finally
				{
					this.userCompleted = true;
					this.SetAsyncWaitHandle();
					if (this.userCallback != null && !(this.Failure is ThreadAbortException) && !(this.Failure is StackOverflowException))
					{
						this.userCallback(this);
					}
				}
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00019208 File Offset: 0x00017408
		internal bool HandleFailure(Exception e)
		{
			Interlocked.CompareExchange<Exception>(ref this.failure, e, null);
			this.SetCompleted();
			return !CommonUtil.IsCatchableExceptionType(e);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00019227 File Offset: 0x00017427
		internal void SetAborted()
		{
			Interlocked.Exchange(ref this.completed, 2);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00019236 File Offset: 0x00017436
		internal void SetCompleted()
		{
			Interlocked.CompareExchange(ref this.completed, 1, 0);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00019246 File Offset: 0x00017446
		protected static void EqualRefCheck(BaseAsyncResult.PerRequest actual, BaseAsyncResult.PerRequest expected, InternalError errorcode)
		{
			if (actual != expected)
			{
				Error.ThrowInternalError(errorcode);
			}
		}

		// Token: 0x060005C1 RID: 1473
		protected abstract void CompletedRequest();

		// Token: 0x060005C2 RID: 1474
		protected abstract void HandleCompleted(BaseAsyncResult.PerRequest pereq);

		// Token: 0x060005C3 RID: 1475
		protected abstract void AsyncEndGetResponse(IAsyncResult asyncResult);

		// Token: 0x060005C4 RID: 1476 RVA: 0x00019252 File Offset: 0x00017452
		protected virtual void CompleteCheck(BaseAsyncResult.PerRequest value, InternalError errorcode)
		{
			if (value == null || value.RequestCompleted)
			{
				Error.ThrowInternalError(errorcode);
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00019268 File Offset: 0x00017468
		protected virtual void FinishCurrentChange(BaseAsyncResult.PerRequest pereq)
		{
			if (!pereq.RequestCompleted)
			{
				Error.ThrowInternalError(InternalError.SaveNextChangeIncomplete);
			}
			BaseAsyncResult.PerRequest perRequest = this.perRequest;
			if (perRequest != null)
			{
				BaseAsyncResult.EqualRefCheck(perRequest, pereq, InternalError.InvalidSaveNextChange);
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00019297 File Offset: 0x00017497
		protected bool HandleFailure(BaseAsyncResult.PerRequest pereq, Exception e)
		{
			if (pereq != null)
			{
				if (this.IsAborted)
				{
					pereq.SetAborted();
				}
				else
				{
					pereq.SetComplete();
				}
			}
			return this.HandleFailure(e);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000192BC File Offset: 0x000174BC
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "required for this feature")]
		protected void AsyncEndGetRequestStream(IAsyncResult asyncResult)
		{
			BaseAsyncResult.AsyncStateBag asyncStateBag = asyncResult.AsyncState as BaseAsyncResult.AsyncStateBag;
			BaseAsyncResult.PerRequest perRequest = ((asyncStateBag == null) ? null : asyncStateBag.PerRequest);
			try
			{
				this.CompleteCheck(perRequest, InternalError.InvalidEndGetRequestCompleted);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				BaseAsyncResult.EqualRefCheck(this.perRequest, perRequest, InternalError.InvalidEndGetRequestStream);
				ODataRequestMessageWrapper odataRequestMessageWrapper = Util.NullCheck<ODataRequestMessageWrapper>(perRequest.Request, InternalError.InvalidEndGetRequestStreamRequest);
				Stream stream = Util.NullCheck<Stream>(odataRequestMessageWrapper.EndGetRequestStream(asyncResult), InternalError.InvalidEndGetRequestStreamStream);
				perRequest.RequestStream = stream;
				ContentStream requestContentStream = perRequest.RequestContentStream;
				Util.NullCheck<ContentStream>(requestContentStream, InternalError.InvalidEndGetRequestStreamContent);
				Util.NullCheck<Stream>(requestContentStream.Stream, InternalError.InvalidEndGetRequestStreamContent);
				if (requestContentStream.IsKnownMemoryStream)
				{
					MemoryStream memoryStream = requestContentStream.Stream as MemoryStream;
					byte[] buffer = memoryStream.GetBuffer();
					int num = checked((int)memoryStream.Position);
					int num2 = checked((int)memoryStream.Length) - num;
					if (buffer == null || num2 == 0)
					{
						Error.ThrowInternalError(InternalError.InvalidEndGetRequestStreamContentLength);
					}
				}
				perRequest.RequestContentBufferValidLength = -1;
				asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(requestContentStream.Stream.BeginRead), perRequest.RequestContentBuffer, 0, perRequest.RequestContentBuffer.Length, new AsyncCallback(this.AsyncRequestContentEndRead), asyncStateBag);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
			}
			catch (Exception ex)
			{
				if (this.HandleFailure(perRequest, ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00019428 File Offset: 0x00017628
		private static IAsyncResult PostInvokeAsync(IAsyncResult asyncResult, AsyncCallback callback)
		{
			if (asyncResult.CompletedSynchronously)
			{
				callback(asyncResult);
			}
			return asyncResult;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001943C File Offset: 0x0001763C
		private static AsyncCallback GetDataServiceAsyncCallback(AsyncCallback callback)
		{
			return delegate(IAsyncResult asyncResult)
			{
				if (asyncResult.CompletedSynchronously)
				{
					return;
				}
				callback(asyncResult);
			};
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00019464 File Offset: 0x00017664
		private void SetAsyncWaitHandle()
		{
			if (this.asyncWait != null)
			{
				Interlocked.CompareExchange(ref this.asyncWaitDisposeLock, new object(), null);
				object obj = this.asyncWaitDisposeLock;
				lock (obj)
				{
					if (!this.asyncWaitDisposed)
					{
						this.asyncWait.Set();
					}
				}
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000194CC File Offset: 0x000176CC
		private void AsyncRequestContentEndRead(IAsyncResult asyncResult)
		{
			BaseAsyncResult.AsyncStateBag asyncStateBag = asyncResult.AsyncState as BaseAsyncResult.AsyncStateBag;
			BaseAsyncResult.PerRequest perRequest = ((asyncStateBag == null) ? null : asyncStateBag.PerRequest);
			try
			{
				this.CompleteCheck(perRequest, InternalError.InvalidEndReadCompleted);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				BaseAsyncResult.EqualRefCheck(this.perRequest, perRequest, InternalError.InvalidEndRead);
				ContentStream requestContentStream = perRequest.RequestContentStream;
				Util.NullCheck<ContentStream>(requestContentStream, InternalError.InvalidEndReadStream);
				Util.NullCheck<Stream>(requestContentStream.Stream, InternalError.InvalidEndReadStream);
				Stream stream = Util.NullCheck<Stream>(perRequest.RequestStream, InternalError.InvalidEndReadStream);
				int num = requestContentStream.Stream.EndRead(asyncResult);
				if (0 < num)
				{
					bool flag = perRequest.RequestContentBufferValidLength == -1;
					perRequest.RequestContentBufferValidLength = num;
					if (!asyncResult.CompletedSynchronously || flag)
					{
						do
						{
							asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(stream.BeginWrite), perRequest.RequestContentBuffer, 0, perRequest.RequestContentBufferValidLength, new AsyncCallback(this.AsyncEndWrite), asyncStateBag);
							perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
							if (asyncResult.CompletedSynchronously && !perRequest.RequestCompleted && !this.IsCompletedInternally)
							{
								asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(requestContentStream.Stream.BeginRead), perRequest.RequestContentBuffer, 0, perRequest.RequestContentBuffer.Length, new AsyncCallback(this.AsyncRequestContentEndRead), asyncStateBag);
								perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
							}
							if (!asyncResult.CompletedSynchronously || perRequest.RequestCompleted || this.IsCompletedInternally)
							{
								break;
							}
						}
						while (perRequest.RequestContentBufferValidLength > 0);
					}
				}
				else
				{
					perRequest.RequestContentBufferValidLength = 0;
					perRequest.RequestStream = null;
					stream.Close();
					ODataRequestMessageWrapper odataRequestMessageWrapper = Util.NullCheck<ODataRequestMessageWrapper>(perRequest.Request, InternalError.InvalidEndWriteRequest);
					asyncResult = BaseAsyncResult.InvokeAsync(new Func<AsyncCallback, object, IAsyncResult>(odataRequestMessageWrapper.BeginGetResponse), new AsyncCallback(this.AsyncEndGetResponse), asyncStateBag);
					perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				}
			}
			catch (Exception ex)
			{
				if (this.HandleFailure(perRequest, ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000196D4 File Offset: 0x000178D4
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "required for this feature")]
		private void AsyncEndWrite(IAsyncResult asyncResult)
		{
			BaseAsyncResult.AsyncStateBag asyncStateBag = asyncResult.AsyncState as BaseAsyncResult.AsyncStateBag;
			BaseAsyncResult.PerRequest perRequest = ((asyncStateBag == null) ? null : asyncStateBag.PerRequest);
			try
			{
				this.CompleteCheck(perRequest, InternalError.InvalidEndWriteCompleted);
				perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				BaseAsyncResult.EqualRefCheck(this.perRequest, perRequest, InternalError.InvalidEndWrite);
				ContentStream requestContentStream = perRequest.RequestContentStream;
				Util.NullCheck<ContentStream>(requestContentStream, InternalError.InvalidEndWriteStream);
				Util.NullCheck<Stream>(requestContentStream.Stream, InternalError.InvalidEndWriteStream);
				Stream stream = Util.NullCheck<Stream>(perRequest.RequestStream, InternalError.InvalidEndWriteStream);
				stream.EndWrite(asyncResult);
				if (!asyncResult.CompletedSynchronously)
				{
					asyncResult = BaseAsyncResult.InvokeAsync(new BaseAsyncResult.AsyncAction(requestContentStream.Stream.BeginRead), perRequest.RequestContentBuffer, 0, perRequest.RequestContentBuffer.Length, new AsyncCallback(this.AsyncRequestContentEndRead), asyncStateBag);
					perRequest.SetRequestCompletedSynchronously(asyncResult.CompletedSynchronously);
				}
			}
			catch (Exception ex)
			{
				if (this.HandleFailure(perRequest, ex))
				{
					throw;
				}
			}
			finally
			{
				this.HandleCompleted(perRequest);
			}
		}

		// Token: 0x0400028B RID: 651
		internal readonly object Source;

		// Token: 0x0400028C RID: 652
		internal readonly string Method;

		// Token: 0x0400028D RID: 653
		protected BaseAsyncResult.PerRequest perRequest;

		// Token: 0x0400028E RID: 654
		private const int True = 1;

		// Token: 0x0400028F RID: 655
		private const int False = 0;

		// Token: 0x04000290 RID: 656
		private readonly AsyncCallback userCallback;

		// Token: 0x04000291 RID: 657
		private readonly object userState;

		// Token: 0x04000292 RID: 658
		private ManualResetEvent asyncWait;

		// Token: 0x04000293 RID: 659
		private Exception failure;

		// Token: 0x04000294 RID: 660
		private ODataRequestMessageWrapper abortable;

		// Token: 0x04000295 RID: 661
		private int completedSynchronously = 1;

		// Token: 0x04000296 RID: 662
		private bool userCompleted;

		// Token: 0x04000297 RID: 663
		private int completed;

		// Token: 0x04000298 RID: 664
		private int userNotified;

		// Token: 0x04000299 RID: 665
		private int done;

		// Token: 0x0400029A RID: 666
		private bool asyncWaitDisposed;

		// Token: 0x0400029B RID: 667
		private object asyncWaitDisposeLock;

		// Token: 0x0200018B RID: 395
		// (Invoke) Token: 0x06000E21 RID: 3617
		internal delegate IAsyncResult AsyncAction(byte[] buffer, int offset, int length, AsyncCallback asyncCallback, object state);

		// Token: 0x0200018C RID: 396
		protected sealed class AsyncStateBag
		{
			// Token: 0x06000E24 RID: 3620 RVA: 0x00030C66 File Offset: 0x0002EE66
			internal AsyncStateBag(BaseAsyncResult.PerRequest pereq)
			{
				this.PerRequest = pereq;
			}

			// Token: 0x04000764 RID: 1892
			internal readonly BaseAsyncResult.PerRequest PerRequest;
		}

		// Token: 0x0200018D RID: 397
		protected sealed class PerRequest
		{
			// Token: 0x06000E25 RID: 3621 RVA: 0x00030C75 File Offset: 0x0002EE75
			internal PerRequest()
			{
				this.requestCompletedSynchronously = 1;
			}

			// Token: 0x17000360 RID: 864
			// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00030C8F File Offset: 0x0002EE8F
			// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00030C97 File Offset: 0x0002EE97
			internal ODataRequestMessageWrapper Request { get; set; }

			// Token: 0x17000361 RID: 865
			// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00030CA0 File Offset: 0x0002EEA0
			// (set) Token: 0x06000E29 RID: 3625 RVA: 0x00030CA8 File Offset: 0x0002EEA8
			internal Stream RequestStream { get; set; }

			// Token: 0x17000362 RID: 866
			// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00030CB1 File Offset: 0x0002EEB1
			// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00030CB9 File Offset: 0x0002EEB9
			internal ContentStream RequestContentStream { get; set; }

			// Token: 0x17000363 RID: 867
			// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00030CC2 File Offset: 0x0002EEC2
			// (set) Token: 0x06000E2D RID: 3629 RVA: 0x00030CCA File Offset: 0x0002EECA
			internal IODataResponseMessage ResponseMessage { get; set; }

			// Token: 0x17000364 RID: 868
			// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00030CD3 File Offset: 0x0002EED3
			// (set) Token: 0x06000E2F RID: 3631 RVA: 0x00030CDB File Offset: 0x0002EEDB
			internal Stream ResponseStream { get; set; }

			// Token: 0x17000365 RID: 869
			// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00030CE4 File Offset: 0x0002EEE4
			internal bool RequestCompletedSynchronously
			{
				get
				{
					return this.requestCompletedSynchronously == 1;
				}
			}

			// Token: 0x17000366 RID: 870
			// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00030CEF File Offset: 0x0002EEEF
			internal bool RequestCompleted
			{
				get
				{
					return this.requestStatus != 0;
				}
			}

			// Token: 0x17000367 RID: 871
			// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00030CFA File Offset: 0x0002EEFA
			internal bool RequestAborted
			{
				get
				{
					return this.requestStatus == 2;
				}
			}

			// Token: 0x17000368 RID: 872
			// (get) Token: 0x06000E33 RID: 3635 RVA: 0x00030D05 File Offset: 0x0002EF05
			internal byte[] RequestContentBuffer
			{
				get
				{
					if (this.requestContentBuffer == null)
					{
						this.requestContentBuffer = new byte[65536];
					}
					return this.requestContentBuffer;
				}
			}

			// Token: 0x17000369 RID: 873
			// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00030D25 File Offset: 0x0002EF25
			// (set) Token: 0x06000E35 RID: 3637 RVA: 0x00030D2D File Offset: 0x0002EF2D
			internal int RequestContentBufferValidLength { get; set; }

			// Token: 0x06000E36 RID: 3638 RVA: 0x00030D36 File Offset: 0x0002EF36
			internal void SetRequestCompletedSynchronously(bool completedSynchronously)
			{
				Interlocked.CompareExchange(ref this.requestCompletedSynchronously, completedSynchronously ? 1 : 0, 1);
			}

			// Token: 0x06000E37 RID: 3639 RVA: 0x00030D4C File Offset: 0x0002EF4C
			internal void SetComplete()
			{
				Interlocked.CompareExchange(ref this.requestStatus, 1, 0);
			}

			// Token: 0x06000E38 RID: 3640 RVA: 0x00030D5C File Offset: 0x0002EF5C
			internal void SetAborted()
			{
				Interlocked.Exchange(ref this.requestStatus, 2);
			}

			// Token: 0x06000E39 RID: 3641 RVA: 0x00030D6C File Offset: 0x0002EF6C
			internal void Dispose()
			{
				if (this.isDisposed)
				{
					return;
				}
				object obj = this.disposeLock;
				lock (obj)
				{
					if (!this.isDisposed)
					{
						this.isDisposed = true;
						if (this.ResponseStream != null)
						{
							this.ResponseStream.Dispose();
							this.ResponseStream = null;
						}
						if (this.RequestContentStream != null)
						{
							if (this.RequestContentStream.Stream != null && this.RequestContentStream.IsKnownMemoryStream)
							{
								this.RequestContentStream.Stream.Dispose();
							}
							this.RequestContentStream = null;
						}
						if (this.RequestStream != null)
						{
							try
							{
								this.RequestStream.Dispose();
								this.RequestStream = null;
							}
							catch (Exception ex)
							{
								if (!this.RequestAborted || !CommonUtil.IsCatchableExceptionType(ex))
								{
									throw;
								}
							}
						}
						WebUtil.DisposeMessage(this.ResponseMessage);
						this.Request = null;
						this.SetComplete();
					}
				}
			}

			// Token: 0x04000765 RID: 1893
			private const int True = 1;

			// Token: 0x04000766 RID: 1894
			private const int False = 0;

			// Token: 0x04000767 RID: 1895
			private int requestStatus;

			// Token: 0x04000768 RID: 1896
			private byte[] requestContentBuffer;

			// Token: 0x04000769 RID: 1897
			private bool isDisposed;

			// Token: 0x0400076A RID: 1898
			private object disposeLock = new object();

			// Token: 0x0400076B RID: 1899
			private int requestCompletedSynchronously;
		}
	}
}
