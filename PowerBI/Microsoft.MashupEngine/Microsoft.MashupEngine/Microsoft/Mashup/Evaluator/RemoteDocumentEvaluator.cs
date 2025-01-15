using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D11 RID: 7441
	internal class RemoteDocumentEvaluator : IDocumentEvaluator, IDocumentEvaluator<IPreviewValueSource>, IDocumentEvaluator<IDataReaderSource>, IDocumentEvaluator<IStreamSource>
	{
		// Token: 0x0600B990 RID: 47504 RVA: 0x002598C5 File Offset: 0x00257AC5
		public RemoteDocumentEvaluator(string identity, IContainerFactory containerFactory, IEngine engine, IEngineHost engineHost, bool enableFirewall)
		{
			this.syncRoot = new object();
			this.identity = identity;
			this.containerFactory = containerFactory;
			this.engine = engine;
			this.engineHost = engineHost;
			this.enableFirewall = enableFirewall;
		}

		// Token: 0x17002DEB RID: 11755
		// (get) Token: 0x0600B991 RID: 47505 RVA: 0x002598FD File Offset: 0x00257AFD
		public IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x0600B992 RID: 47506 RVA: 0x00259905 File Offset: 0x00257B05
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IPreviewValueSource>> callback)
		{
			return this.BeginGetResult<IPreviewValueSource>(new RemoteDocumentEvaluator.PreviewValueSourceRemoteEvaluation(this, RemoteDocumentEvaluator.NextEvaluationID(), parameters, callback));
		}

		// Token: 0x0600B993 RID: 47507 RVA: 0x0025991A File Offset: 0x00257B1A
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IDataReaderSource>> callback)
		{
			return this.BeginGetResult<IDataReaderSource>(new RemoteDocumentEvaluator.DataReaderSourceRemoteEvaluation(this, RemoteDocumentEvaluator.NextEvaluationID(), parameters, callback));
		}

		// Token: 0x0600B994 RID: 47508 RVA: 0x0025992F File Offset: 0x00257B2F
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IStreamSource>> callback)
		{
			return this.BeginGetResult<IStreamSource>(new RemoteDocumentEvaluator.StreamSourceRemoteEvaluation(this, RemoteDocumentEvaluator.NextEvaluationID(), parameters, callback));
		}

		// Token: 0x0600B995 RID: 47509 RVA: 0x00259944 File Offset: 0x00257B44
		private IEvaluation BeginGetResult<T>(RemoteDocumentEvaluator.RemoteEvaluation<T> evaluation)
		{
			evaluation.Trace.Add("evaluationID", evaluation.EvaluationID, false);
			try
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.evaluation != null)
					{
						throw new InvalidOperationException("RemoteDocumentEvaluator is currently in use.");
					}
					this.evaluation = evaluation;
				}
				GlobalizedEvaluatorThreadPool.Start(new ParameterizedThreadStart(this.EvaluationThread), evaluation);
			}
			catch
			{
				evaluation.Trace.Dispose();
				throw;
			}
			return evaluation;
		}

		// Token: 0x0600B996 RID: 47510 RVA: 0x002599E4 File Offset: 0x00257BE4
		private void EvaluationThread(object state)
		{
			((RemoteDocumentEvaluator.RemoteEvaluation)state).Evaluate(this.enableFirewall);
		}

		// Token: 0x0600B997 RID: 47511 RVA: 0x002599F7 File Offset: 0x00257BF7
		private void CreateContainer()
		{
			this.container = this.containerFactory.CreateContainer();
			this.container.Messenger.AddHandler(new Action<IMessageChannel, RemoteDocumentEvaluator.TranslateSourceLocationRequestMessage>(this.OnTranslateSourceLocationRequest));
		}

		// Token: 0x0600B998 RID: 47512 RVA: 0x00259A28 File Offset: 0x00257C28
		private void DisposeContainer()
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteDocumentEvaluator/DisposeContainer", this.EngineHost, TraceEventType.Information, null))
			{
				hostTrace.Add("identity", this.identity, false);
				if (this.container != null)
				{
					hostTrace.Add("containerID", this.container.ContainerID, false);
					this.container.Messenger.RemoveHandler<RemoteDocumentEvaluator.TranslateSourceLocationRequestMessage>();
					this.container.Dispose();
					this.container = null;
				}
			}
		}

		// Token: 0x0600B999 RID: 47513 RVA: 0x00259ABC File Offset: 0x00257CBC
		private void KillContainer(RemoteDocumentEvaluator.RemoteEvaluation evaluation = null)
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteDocumentEvaluator/KillContainer", this.EngineHost, TraceEventType.Information, null))
			{
				hostTrace.Add("identity", this.identity, false);
				IHostTrace hostTrace2 = hostTrace;
				string text = "containerID";
				IContainer container = this.container;
				hostTrace2.Add(text, (container != null) ? new int?(container.ContainerID) : null, false);
				IHostTrace hostTrace3 = hostTrace;
				string text2 = "evaluationID";
				RemoteDocumentEvaluator.RemoteEvaluation remoteEvaluation = this.evaluation;
				hostTrace3.Add(text2, (remoteEvaluation != null) ? new int?(remoteEvaluation.EvaluationID) : null, false);
				IContainer container2 = null;
				object obj = this.syncRoot;
				lock (obj)
				{
					if (evaluation == null || evaluation == this.evaluation)
					{
						container2 = this.container;
					}
				}
				if (container2 != null)
				{
					hostTrace.Add("containerIDToKill", container2.ContainerID, false);
					container2.Kill();
				}
			}
		}

		// Token: 0x0600B99A RID: 47514 RVA: 0x00259BCC File Offset: 0x00257DCC
		private void DisposeEvaluation()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.evaluation = null;
			}
		}

		// Token: 0x0600B99B RID: 47515 RVA: 0x00259C10 File Offset: 0x00257E10
		private void OnTranslateSourceLocationRequest(IMessageChannel channel, RemoteDocumentEvaluator.TranslateSourceLocationRequestMessage message)
		{
			IDocumentHost documentHost = new TextDocumentHost(default(SegmentedString));
			if (this.evaluation != null)
			{
				IPackage package = this.evaluation.Parameters.document.Package;
				string uniqueID = message.Location.Document.UniqueID;
				IDocumentHost documentHost2 = (from sn in package.SectionNames
					select package.GetSection(sn) into s
					where s.UniqueID == uniqueID
					select s).FirstOrDefault<IPackageSection>();
				documentHost = documentHost2 ?? documentHost;
			}
			SourceLocation sourceLocation = new SourceLocation(documentHost, message.Location.Range);
			ITranslateSourceLocation translateSourceLocation = documentHost as ITranslateSourceLocation;
			if (translateSourceLocation != null)
			{
				sourceLocation = translateSourceLocation.TranslateSourceLocation(sourceLocation);
			}
			channel.Post(new RemoteDocumentEvaluator.TranslateSourceLocationResponseMessage
			{
				Location = sourceLocation
			});
		}

		// Token: 0x0600B99C RID: 47516 RVA: 0x00259CE3 File Offset: 0x00257EE3
		private static int NextEvaluationID()
		{
			return Interlocked.Increment(ref RemoteDocumentEvaluator.nextEvaluationId);
		}

		// Token: 0x0600B99D RID: 47517 RVA: 0x00259CF0 File Offset: 0x00257EF0
		private static bool IsExceptionRecoverable(Exception exception)
		{
			if (exception is RuntimeException)
			{
				return true;
			}
			ErrorException ex = exception as ErrorException;
			return ex != null && ex.IsRecoverable && ex.IsExpected;
		}

		// Token: 0x04005E7B RID: 24187
		private static int nextEvaluationId;

		// Token: 0x04005E7C RID: 24188
		private readonly object syncRoot;

		// Token: 0x04005E7D RID: 24189
		private readonly string identity;

		// Token: 0x04005E7E RID: 24190
		private readonly IContainerFactory containerFactory;

		// Token: 0x04005E7F RID: 24191
		private readonly IEngine engine;

		// Token: 0x04005E80 RID: 24192
		private readonly IEngineHost engineHost;

		// Token: 0x04005E81 RID: 24193
		private readonly bool enableFirewall;

		// Token: 0x04005E82 RID: 24194
		private IContainer container;

		// Token: 0x04005E83 RID: 24195
		private RemoteDocumentEvaluator.RemoteEvaluation evaluation;

		// Token: 0x02001D12 RID: 7442
		private abstract class RemoteEvaluation : IEvaluation
		{
			// Token: 0x0600B99E RID: 47518 RVA: 0x00259D24 File Offset: 0x00257F24
			protected RemoteEvaluation(RemoteDocumentEvaluator evaluator, string traceLocation, int evaluationID, DocumentEvaluationParameters parameters)
			{
				this.evaluator = evaluator;
				this.evaluationID = evaluationID;
				this.parameters = parameters;
				this.trace = EvaluatorTracing.CreatePerformanceTrace(traceLocation, this.EngineHost, TraceEventType.Information, null);
				this.trace.Add("identity", this.evaluator.identity, false);
			}

			// Token: 0x17002DEC RID: 11756
			// (get) Token: 0x0600B99F RID: 47519 RVA: 0x00259D7D File Offset: 0x00257F7D
			public IEngine Engine
			{
				get
				{
					return this.evaluator.engine;
				}
			}

			// Token: 0x17002DED RID: 11757
			// (get) Token: 0x0600B9A0 RID: 47520 RVA: 0x00259D8A File Offset: 0x00257F8A
			public IEngineHost EngineHost
			{
				get
				{
					return this.evaluator.EngineHost;
				}
			}

			// Token: 0x17002DEE RID: 11758
			// (get) Token: 0x0600B9A1 RID: 47521 RVA: 0x00259D97 File Offset: 0x00257F97
			public DocumentEvaluationParameters Parameters
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x17002DEF RID: 11759
			// (get) Token: 0x0600B9A2 RID: 47522 RVA: 0x00259D9F File Offset: 0x00257F9F
			public int EvaluationID
			{
				get
				{
					return this.evaluationID;
				}
			}

			// Token: 0x17002DF0 RID: 11760
			// (get) Token: 0x0600B9A3 RID: 47523 RVA: 0x00259DA7 File Offset: 0x00257FA7
			public IHostTrace Trace
			{
				get
				{
					return this.trace;
				}
			}

			// Token: 0x17002DF1 RID: 11761
			// (get) Token: 0x0600B9A4 RID: 47524 RVA: 0x00259DAF File Offset: 0x00257FAF
			public IMessageChannel Channel
			{
				get
				{
					return this.channel;
				}
			}

			// Token: 0x0600B9A5 RID: 47525 RVA: 0x00259DB8 File Offset: 0x00257FB8
			public void Evaluate(bool enableFirewall)
			{
				try
				{
					this.evaluator.CreateContainer();
					this.channel = this.evaluator.container.Messenger.CreateChannel();
					this.trace.Add("containerID", this.evaluator.container.ContainerID, false);
					object syncRoot = this.evaluator.syncRoot;
					bool flag2;
					lock (syncRoot)
					{
						flag2 = this.cancelled;
					}
					if (flag2)
					{
						this.Cancel();
					}
					this.GetResult(enableFirewall);
				}
				catch (Exception ex)
				{
					if (this.exception == null)
					{
						this.exception = ex;
					}
					this.Finish();
					if (!Microsoft.Mashup.Common.SafeExceptions.TraceIsSafeException(this.Trace, ex))
					{
						throw;
					}
					if (!this.TryCompleteWithException(ex))
					{
						throw;
					}
				}
			}

			// Token: 0x0600B9A6 RID: 47526
			public abstract bool TryCompleteWithException(Exception e);

			// Token: 0x0600B9A7 RID: 47527 RVA: 0x00259E9C File Offset: 0x0025809C
			public void Cancel()
			{
				object syncRoot = this.evaluator.syncRoot;
				lock (syncRoot)
				{
					this.cancelled = true;
				}
				this.evaluator.KillContainer(this);
			}

			// Token: 0x0600B9A8 RID: 47528 RVA: 0x00259EF0 File Offset: 0x002580F0
			public void ResultDisposed()
			{
				if (this.channel != null)
				{
					this.channel.Dispose();
					this.channel = null;
				}
				this.evaluator.DisposeContainer();
				this.evaluator.DisposeEvaluation();
			}

			// Token: 0x0600B9A9 RID: 47529 RVA: 0x00259F22 File Offset: 0x00258122
			public void WaitForEndGetResult()
			{
				this.IgnoreCancelExceptions(delegate
				{
					this.channel.WaitFor<RemoteDocumentEvaluator.EndGetResultMessage>();
				});
			}

			// Token: 0x0600B9AA RID: 47530 RVA: 0x00259F38 File Offset: 0x00258138
			public void IgnoreCancelExceptions(Action action)
			{
				try
				{
					this.TranslateCancelExceptions(action);
				}
				catch (OperationCanceledException)
				{
				}
			}

			// Token: 0x0600B9AB RID: 47531 RVA: 0x00259F64 File Offset: 0x00258164
			public void TranslateCancelExceptions(Action action)
			{
				try
				{
					action();
				}
				catch (Exception ex)
				{
					Exception ex2 = this.TranslateCancelExceptions(ex);
					if (ex2 != ex)
					{
						throw ex2;
					}
					throw;
				}
			}

			// Token: 0x0600B9AC RID: 47532 RVA: 0x00259F9C File Offset: 0x0025819C
			public Exception TranslateCancelExceptions(Exception exception)
			{
				Exception ex;
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteDocumentEvaluator/RemoteEvaluation/TranslateCancelExceptions", this.EngineHost, TraceEventType.Information, null))
				{
					object syncRoot = this.evaluator.syncRoot;
					bool flag2;
					lock (syncRoot)
					{
						flag2 = this.cancelled;
					}
					hostTrace.Add("identity", this.evaluator.identity, false);
					hostTrace.Add("evaluationID", this.evaluationID, false);
					IHostTrace hostTrace2 = hostTrace;
					string text = "containerID";
					IContainer container = this.evaluator.container;
					hostTrace2.Add(text, (container != null) ? new int?(container.ContainerID) : null, false);
					hostTrace.Add("cancelled", flag2, false);
					if (!Microsoft.Mashup.Common.SafeExceptions.TraceIsSafeException(hostTrace, exception) || !flag2)
					{
						ex = exception;
					}
					else
					{
						ex = new OperationCanceledException(Strings.Evaluation_Canceled);
					}
				}
				return ex;
			}

			// Token: 0x0600B9AD RID: 47533
			protected abstract void GetResult(bool enableFirewall);

			// Token: 0x0600B9AE RID: 47534 RVA: 0x0025A0A4 File Offset: 0x002582A4
			protected bool HandleException(Exception exception, bool disposing)
			{
				bool flag;
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteDocumentEvaluator/RemoteEvaluation/HandleException", this.EngineHost, TraceEventType.Information, null))
				{
					hostTrace.Add("identity", this.evaluator.identity, false);
					hostTrace.Add("evaluationID", this.evaluationID, false);
					IHostTrace hostTrace2 = hostTrace;
					string text = "containerID";
					IContainer container = this.evaluator.container;
					hostTrace2.Add(text, (container != null) ? new int?(container.ContainerID) : null, false);
					hostTrace.Add("disposing", disposing, false);
					if (!Microsoft.Mashup.Common.SafeExceptions.TraceIsSafeException(hostTrace, exception))
					{
						flag = false;
					}
					else
					{
						Exception ex = this.TranslateCancelExceptions(exception);
						this.exception = this.exception ?? ex;
						if (!disposing && ex != exception)
						{
							throw ex;
						}
						flag = disposing;
					}
				}
				return flag;
			}

			// Token: 0x0600B9AF RID: 47535 RVA: 0x0025A188 File Offset: 0x00258388
			protected void Finish()
			{
				if (!this.finished)
				{
					try
					{
						this.finished = true;
						if (this.exception != null && !RemoteDocumentEvaluator.IsExceptionRecoverable(this.exception))
						{
							this.evaluator.KillContainer(this);
						}
						else
						{
							this.WaitForEndGetResult();
						}
						this.ResultDisposed();
					}
					finally
					{
						this.trace.Dispose();
					}
				}
			}

			// Token: 0x04005E84 RID: 24196
			protected readonly IHostTrace trace;

			// Token: 0x04005E85 RID: 24197
			protected readonly RemoteDocumentEvaluator evaluator;

			// Token: 0x04005E86 RID: 24198
			protected readonly int evaluationID;

			// Token: 0x04005E87 RID: 24199
			protected readonly DocumentEvaluationParameters parameters;

			// Token: 0x04005E88 RID: 24200
			protected IMessageChannel channel;

			// Token: 0x04005E89 RID: 24201
			protected Exception exception;

			// Token: 0x04005E8A RID: 24202
			protected bool complete;

			// Token: 0x04005E8B RID: 24203
			protected bool cancelled;

			// Token: 0x04005E8C RID: 24204
			protected bool finished;
		}

		// Token: 0x02001D13 RID: 7443
		private abstract class RemoteEvaluation<T> : RemoteDocumentEvaluator.RemoteEvaluation
		{
			// Token: 0x0600B9B1 RID: 47537 RVA: 0x0025A202 File Offset: 0x00258402
			protected RemoteEvaluation(RemoteDocumentEvaluator evaluator, int evaluationID, DocumentEvaluationParameters parameters, Action<EvaluationResult2<T>> callback)
				: base(evaluator, "RemoteDocumentEvaluator/GetResult<" + typeof(T).FullName + ">", evaluationID, parameters)
			{
				this.callback = callback;
			}

			// Token: 0x0600B9B2 RID: 47538 RVA: 0x0025A233 File Offset: 0x00258433
			public override bool TryCompleteWithException(Exception exception)
			{
				return this.TryComplete(new EvaluationResult2<T>(exception.ToCallbackException()));
			}

			// Token: 0x0600B9B3 RID: 47539 RVA: 0x0025A246 File Offset: 0x00258446
			protected void Complete(EvaluationResult2<T> result)
			{
				this.TryComplete(result);
			}

			// Token: 0x0600B9B4 RID: 47540 RVA: 0x0025A250 File Offset: 0x00258450
			private bool TryComplete(EvaluationResult2<T> result)
			{
				Action<EvaluationResult2<T>> action = null;
				object syncRoot = this.evaluator.syncRoot;
				lock (syncRoot)
				{
					if (!this.complete)
					{
						this.complete = true;
						action = this.callback;
						if (this.cancelled && result.Exception != null)
						{
							result = new EvaluationResult2<T>(new OperationCanceledException(Strings.Evaluation_Canceled));
						}
					}
				}
				if (action != null)
				{
					base.Trace.Suspend();
					action(result);
					return true;
				}
				return false;
			}

			// Token: 0x04005E8D RID: 24205
			private readonly Action<EvaluationResult2<T>> callback;
		}

		// Token: 0x02001D14 RID: 7444
		private sealed class PreviewValueSourceRemoteEvaluation : RemoteDocumentEvaluator.RemoteEvaluation<IPreviewValueSource>
		{
			// Token: 0x0600B9B5 RID: 47541 RVA: 0x0025A2E4 File Offset: 0x002584E4
			public PreviewValueSourceRemoteEvaluation(RemoteDocumentEvaluator evaluator, int evaluationID, DocumentEvaluationParameters parameters, Action<EvaluationResult2<IPreviewValueSource>> callback)
				: base(evaluator, evaluationID, parameters, callback)
			{
			}

			// Token: 0x0600B9B6 RID: 47542 RVA: 0x0025A2F4 File Offset: 0x002584F4
			protected override void GetResult(bool enableFirewall)
			{
				base.Channel.Post(new RemoteDocumentEvaluator.BeginGetPreviewValueSourceMessage
				{
					EvaluationID = this.evaluationID,
					EnableFirewall = enableFirewall,
					PartitioningScheme = this.parameters.document.PartitioningScheme,
					Package = this.parameters.document.Package,
					Parameters = this.parameters
				});
				IPreviewValueSource previewValueSource = RemotePreviewValueSource.CreateProxy(base.EngineHost, base.Channel, new ExceptionHandler(base.HandleException)).TraceTo(base.Trace);
				base.Complete(new EvaluationResult2<IPreviewValueSource>(previewValueSource.AfterDispose(new Action(base.Finish))));
			}
		}

		// Token: 0x02001D15 RID: 7445
		private sealed class DataReaderSourceRemoteEvaluation : RemoteDocumentEvaluator.RemoteEvaluation<IDataReaderSource>
		{
			// Token: 0x0600B9B7 RID: 47543 RVA: 0x0025A3A2 File Offset: 0x002585A2
			public DataReaderSourceRemoteEvaluation(RemoteDocumentEvaluator evaluator, int evaluationID, DocumentEvaluationParameters parameters, Action<EvaluationResult2<IDataReaderSource>> callback)
				: base(evaluator, evaluationID, parameters, callback)
			{
			}

			// Token: 0x0600B9B8 RID: 47544 RVA: 0x0025A3B0 File Offset: 0x002585B0
			protected override void GetResult(bool enableFirewall)
			{
				base.Channel.Post(new RemoteDocumentEvaluator.BeginGetDataReaderSourceMessage
				{
					EvaluationID = this.evaluationID,
					EnableFirewall = enableFirewall,
					PartitioningScheme = this.parameters.document.PartitioningScheme,
					Package = this.parameters.document.Package,
					Parameters = this.parameters
				});
				IDataReaderSource dataReaderSource = new RemoteDocumentEvaluator.DataReaderSourceRemoteEvaluation.PageReaderDataReaderSource(RemotePageReader.CreateProxy(base.EngineHost, base.Channel, new ExceptionHandler(base.HandleException))).TraceTo(base.Trace);
				base.Complete(new EvaluationResult2<IDataReaderSource>(dataReaderSource.AfterDispose(new Action(base.Finish))));
			}

			// Token: 0x02001D16 RID: 7446
			private sealed class PageReaderDataReaderSource : IDataReaderSource, IDisposable
			{
				// Token: 0x0600B9B9 RID: 47545 RVA: 0x0025A463 File Offset: 0x00258663
				public PageReaderDataReaderSource(IPageReader pageReader)
				{
					this.pageReader = pageReader;
				}

				// Token: 0x17002DF2 RID: 11762
				// (get) Token: 0x0600B9BA RID: 47546 RVA: 0x0025A474 File Offset: 0x00258674
				public ITableSource TableSource
				{
					get
					{
						IPageReaderWithTableSource pageReaderWithTableSource = this.pageReader as IPageReaderWithTableSource;
						if (pageReaderWithTableSource == null)
						{
							return null;
						}
						return pageReaderWithTableSource.TableSource;
					}
				}

				// Token: 0x17002DF3 RID: 11763
				// (get) Token: 0x0600B9BB RID: 47547 RVA: 0x0025A498 File Offset: 0x00258698
				public IPageReader PageReader
				{
					get
					{
						return this.pageReader;
					}
				}

				// Token: 0x0600B9BC RID: 47548 RVA: 0x0025A4A0 File Offset: 0x002586A0
				public void Dispose()
				{
					this.pageReader.Dispose();
				}

				// Token: 0x04005E8E RID: 24206
				private readonly IPageReader pageReader;
			}
		}

		// Token: 0x02001D17 RID: 7447
		private sealed class StreamSourceRemoteEvaluation : RemoteDocumentEvaluator.RemoteEvaluation<IStreamSource>
		{
			// Token: 0x0600B9BD RID: 47549 RVA: 0x0025A4AD File Offset: 0x002586AD
			public StreamSourceRemoteEvaluation(RemoteDocumentEvaluator evaluator, int evaluationID, DocumentEvaluationParameters parameters, Action<EvaluationResult2<IStreamSource>> callback)
				: base(evaluator, evaluationID, parameters, callback)
			{
			}

			// Token: 0x0600B9BE RID: 47550 RVA: 0x0025A4BC File Offset: 0x002586BC
			protected override void GetResult(bool enableFirewall)
			{
				base.Channel.Post(new RemoteDocumentEvaluator.BeginGetStreamSourceMessage
				{
					EvaluationID = this.evaluationID,
					EnableFirewall = enableFirewall,
					PartitioningScheme = this.parameters.document.PartitioningScheme,
					Package = this.parameters.document.Package,
					Parameters = this.parameters
				});
				IStreamSource streamSource = new RemoteDocumentEvaluator.StreamSourceRemoteEvaluation.StreamStreamSource(RemoteStream.CreateReaderProxy(base.EngineHost, base.Channel, new ExceptionHandler(base.HandleException))).TraceTo(base.Trace);
				base.Complete(new EvaluationResult2<IStreamSource>(streamSource.AfterDispose(new Action(base.Finish))));
			}

			// Token: 0x02001D18 RID: 7448
			private sealed class StreamStreamSource : IStreamSource, IDisposable
			{
				// Token: 0x0600B9BF RID: 47551 RVA: 0x0025A56F File Offset: 0x0025876F
				public StreamStreamSource(Stream stream)
				{
					this.stream = stream;
				}

				// Token: 0x17002DF4 RID: 11764
				// (get) Token: 0x0600B9C0 RID: 47552 RVA: 0x0025A57E File Offset: 0x0025877E
				public Stream Stream
				{
					get
					{
						return this.stream;
					}
				}

				// Token: 0x0600B9C1 RID: 47553 RVA: 0x0025A586 File Offset: 0x00258786
				public void Dispose()
				{
					this.stream.Dispose();
				}

				// Token: 0x04005E8F RID: 24207
				private readonly Stream stream;
			}
		}

		// Token: 0x02001D19 RID: 7449
		public sealed class Service : IDisposable
		{
			// Token: 0x0600B9C2 RID: 47554 RVA: 0x0025A594 File Offset: 0x00258794
			public Service(IEngineHost engineHost, IMessenger messenger, Action evaluationComplete)
			{
				this.engineHost = engineHost;
				this.engine = engineHost.QueryService<IEngine>();
				this.previousGetExceptionDetails = TracingHandlers.GetExceptionDetails;
				TracingHandlers.GetExceptionDetails = delegate(Exception value)
				{
					string text = null;
					try
					{
						if (!this.engine.TryGetExceptionDetails(value, out text))
						{
							text = null;
						}
					}
					catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
					{
						using (EvaluatorTracing.CreateTrace("RemoteDocumentEvaluator/Service/GetExceptionDetails/DoubleFault", this.engineHost, TraceEventType.Information, null))
						{
						}
					}
					return text;
				};
				this.messenger = messenger;
				this.evaluationComplete = evaluationComplete;
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteDocumentEvaluator.BeginGetPreviewValueSourceMessage>(this.OnBeginGetPreviewValueSource));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteDocumentEvaluator.BeginGetDataReaderSourceMessage>(this.OnBeginGetDataReaderSource));
				this.messenger.AddHandler(new Action<IMessageChannel, RemoteDocumentEvaluator.BeginGetStreamSourceMessage>(this.OnBeginGetStreamSource));
			}

			// Token: 0x0600B9C3 RID: 47555 RVA: 0x0025A62C File Offset: 0x0025882C
			public void Dispose()
			{
				if (this.messenger != null)
				{
					this.messenger.RemoveHandler<RemoteDocumentEvaluator.BeginGetPreviewValueSourceMessage>();
					this.messenger.RemoveHandler<RemoteDocumentEvaluator.BeginGetDataReaderSourceMessage>();
					this.messenger.RemoveHandler<RemoteDocumentEvaluator.BeginGetStreamSourceMessage>();
					this.messenger = null;
					this.evaluationComplete = null;
					TracingHandlers.GetExceptionDetails = this.previousGetExceptionDetails;
				}
			}

			// Token: 0x0600B9C4 RID: 47556 RVA: 0x0025A67C File Offset: 0x0025887C
			private void OnBeginGetPreviewValueSource(IMessageChannel channel, RemoteDocumentEvaluator.BeginGetPreviewValueSourceMessage message)
			{
				this.OnBeginGetResult<IPreviewValueSource>(channel, message, delegate(EvaluationResult2<IPreviewValueSource> result)
				{
					RemotePreviewValueSource.RunStub(this.engineHost, channel, () => result.Result);
				});
			}

			// Token: 0x0600B9C5 RID: 47557 RVA: 0x0025A6B8 File Offset: 0x002588B8
			private void OnBeginGetDataReaderSource(IMessageChannel channel, RemoteDocumentEvaluator.BeginGetDataReaderSourceMessage message)
			{
				this.OnBeginGetResult<IDataReaderSource>(channel, message, delegate(EvaluationResult2<IDataReaderSource> result)
				{
					RemotePageReader.RunStub(this.engineHost, channel, () => new DataReaderSourcePageReader(result.Result));
				});
			}

			// Token: 0x0600B9C6 RID: 47558 RVA: 0x0025A6F4 File Offset: 0x002588F4
			private void OnBeginGetStreamSource(IMessageChannel channel, RemoteDocumentEvaluator.BeginGetStreamSourceMessage message)
			{
				this.OnBeginGetResult<IStreamSource>(channel, message, delegate(EvaluationResult2<IStreamSource> result)
				{
					RemoteStream.RunStub(this.engineHost, channel, () => new StreamSourceStream(result.Result));
				});
			}

			// Token: 0x0600B9C7 RID: 47559 RVA: 0x0025A730 File Offset: 0x00258930
			private void OnBeginGetResult<T>(IMessageChannel channel, RemoteDocumentEvaluator.BeginGetResultMessage message, Action<EvaluationResult2<T>> action)
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteDocumentEvaluator/Service/OnBeginGetResult", this.engineHost, TraceEventType.Information, null))
				{
					hostTrace.Add("evaluationID", message.EvaluationID, false);
					if (this.evaluating)
					{
						throw new InvalidOperationException("RemoteDocumentEvaluator.Service is currently in use.");
					}
					this.evaluating = true;
					DocumentEvaluationParameters parameters = message.Parameters;
					parameters.document = message.Package.PartitionedDocument(message.PartitioningScheme, this.engine);
					IDocumentEvaluator documentEvaluator = RemoteDocumentEvaluator.Service.CreateDocumentEvaluator(this.engineHost, this.engine, message.EnableFirewall);
					EvaluationResult2<T> result = ((IDocumentEvaluator<T>)documentEvaluator).GetResult(this.AddDocumentHost(parameters));
					using (IHostTrace hostTrace2 = EvaluatorTracing.CreateTrace("RemoteDocumentEvaluator/Service/OnBeginGetResult/result", this.engineHost, TraceEventType.Information, null))
					{
						hostTrace2.Add("evaluationID", message.EvaluationID, false);
						EvaluationHost.ReportExceptions(hostTrace2, this.engineHost, channel, delegate
						{
							action(result);
						});
					}
					channel.Post(new RemoteDocumentEvaluator.EndGetResultMessage());
					this.evaluating = false;
					this.evaluationComplete();
				}
			}

			// Token: 0x0600B9C8 RID: 47560 RVA: 0x0025A87C File Offset: 0x00258A7C
			private DocumentEvaluationParameters AddDocumentHost(DocumentEvaluationParameters parameters)
			{
				Dictionary<string, IPackageSection> dictionary = new Dictionary<string, IPackageSection>();
				foreach (string text in parameters.document.Package.SectionNames)
				{
					IPackageSection section = parameters.document.Package.GetSection(text);
					RemoteDocumentEvaluator.Service.RemotePackageSection remotePackageSection = null;
					ICacheableDocumentHost cacheableDocumentHost = section as ICacheableDocumentHost;
					if (cacheableDocumentHost != null)
					{
						RemoteDocumentEvaluator.Service.SectionKey sectionKey = new RemoteDocumentEvaluator.Service.SectionKey(cacheableDocumentHost, section.Text);
						if (!RemoteDocumentEvaluator.Service.sectionCache.TryGetValue(sectionKey, out remotePackageSection))
						{
							remotePackageSection = new RemoteDocumentEvaluator.Service.RemotePackageSection();
							RemoteDocumentEvaluator.Service.sectionCache.Add(sectionKey, remotePackageSection);
						}
					}
					else
					{
						remotePackageSection = new RemoteDocumentEvaluator.Service.RemotePackageSection();
					}
					remotePackageSection.Attach(this.messenger, section);
					dictionary.Add(text, remotePackageSection);
				}
				parameters = parameters.Clone();
				parameters.document = new RemoteDocumentEvaluator.Service.RemoteMemberLetPartitionedDocument(this.engine, (IMemberLetPartitionedDocument)parameters.document, new Package(dictionary));
				return parameters;
			}

			// Token: 0x0600B9C9 RID: 47561 RVA: 0x0025A978 File Offset: 0x00258B78
			private static IDocumentEvaluator CreateDocumentEvaluator(IEngineHost engineHost, IEngine engine, bool enableFirewall)
			{
				if (enableFirewall)
				{
					return new FirewallDocumentEvaluator(engineHost, engine, (IEngineHost h, IEngine e) => new SimpleDocumentEvaluator(h, e));
				}
				return new SimpleDocumentEvaluator(engineHost, engine);
			}

			// Token: 0x04005E90 RID: 24208
			private static readonly LruCache<RemoteDocumentEvaluator.Service.SectionKey, RemoteDocumentEvaluator.Service.RemotePackageSection> sectionCache = new LruCache<RemoteDocumentEvaluator.Service.SectionKey, RemoteDocumentEvaluator.Service.RemotePackageSection>(16, null);

			// Token: 0x04005E91 RID: 24209
			private readonly IEngineHost engineHost;

			// Token: 0x04005E92 RID: 24210
			private readonly IEngine engine;

			// Token: 0x04005E93 RID: 24211
			private readonly Func<Exception, string> previousGetExceptionDetails;

			// Token: 0x04005E94 RID: 24212
			private bool evaluating;

			// Token: 0x04005E95 RID: 24213
			private IMessenger messenger;

			// Token: 0x04005E96 RID: 24214
			private Action evaluationComplete;

			// Token: 0x02001D1A RID: 7450
			private class SectionKey : IEquatable<RemoteDocumentEvaluator.Service.SectionKey>
			{
				// Token: 0x0600B9CC RID: 47564 RVA: 0x0025AA34 File Offset: 0x00258C34
				public SectionKey(ICacheableDocumentHost host, SegmentedString text)
				{
					this.host = host;
					this.text = text;
				}

				// Token: 0x0600B9CD RID: 47565 RVA: 0x0025AA4C File Offset: 0x00258C4C
				public bool Equals(RemoteDocumentEvaluator.Service.SectionKey other)
				{
					return other != null && this.host.CacheIdentity.Equals(other.host.CacheIdentity) && this.text.Equals(other.text);
				}

				// Token: 0x0600B9CE RID: 47566 RVA: 0x0025AA8F File Offset: 0x00258C8F
				public override bool Equals(object other)
				{
					return this.Equals(other as RemoteDocumentEvaluator.Service.SectionKey);
				}

				// Token: 0x0600B9CF RID: 47567 RVA: 0x0025AAA0 File Offset: 0x00258CA0
				public override int GetHashCode()
				{
					return this.host.CacheIdentity.GetHashCode() + 37 * this.text.GetHashCode();
				}

				// Token: 0x04005E97 RID: 24215
				private readonly ICacheableDocumentHost host;

				// Token: 0x04005E98 RID: 24216
				private readonly SegmentedString text;
			}

			// Token: 0x02001D1B RID: 7451
			private class RemoteMemberLetPartitionedDocument : IMemberLetPartitionedDocument, IPartitionedDocument
			{
				// Token: 0x0600B9D0 RID: 47568 RVA: 0x0025AAD5 File Offset: 0x00258CD5
				public RemoteMemberLetPartitionedDocument(IEngine engine, IMemberLetPartitionedDocument document, IPackage package)
				{
					this.engine = engine;
					this.document = document;
					this.package = package;
				}

				// Token: 0x17002DF5 RID: 11765
				// (get) Token: 0x0600B9D1 RID: 47569 RVA: 0x0025AAF2 File Offset: 0x00258CF2
				public IPackage Package
				{
					get
					{
						return this.package;
					}
				}

				// Token: 0x17002DF6 RID: 11766
				// (get) Token: 0x0600B9D2 RID: 47570 RVA: 0x0025AAFA File Offset: 0x00258CFA
				public PartitioningScheme PartitioningScheme
				{
					get
					{
						return this.document.PartitioningScheme;
					}
				}

				// Token: 0x17002DF7 RID: 11767
				// (get) Token: 0x0600B9D3 RID: 47571 RVA: 0x0025AB07 File Offset: 0x00258D07
				public IEnumerable<IPartitionKey> PartitionKeys
				{
					get
					{
						return this.document.PartitionKeys;
					}
				}

				// Token: 0x0600B9D4 RID: 47572 RVA: 0x0025AB14 File Offset: 0x00258D14
				public IEnumerable<IPartitionKey> GetPartitionInputs(IPartitionKey partitionKey)
				{
					return this.document.GetPartitionInputs(partitionKey);
				}

				// Token: 0x0600B9D5 RID: 47573 RVA: 0x0025AB22 File Offset: 0x00258D22
				public bool IsPartitionInError(IPartitionKey partitionKey)
				{
					return this.document.IsPartitionInError(partitionKey);
				}

				// Token: 0x0600B9D6 RID: 47574 RVA: 0x0025AB30 File Offset: 0x00258D30
				public SegmentedString GetPartition(IPartitionKey partitionKey)
				{
					return this.document.GetPartition(partitionKey);
				}

				// Token: 0x0600B9D7 RID: 47575 RVA: 0x0025AB3E File Offset: 0x00258D3E
				public string GetPartitionSection(IPartitionKey partitionKey)
				{
					return this.document.GetPartitionSection(partitionKey);
				}

				// Token: 0x0600B9D8 RID: 47576 RVA: 0x0025AB4C File Offset: 0x00258D4C
				public string GetPartitionSectionOffsetAndLength(IPartitionKey partitionKey, out int offset, out int length)
				{
					return this.document.GetPartitionSectionOffsetAndLength(partitionKey, out offset, out length);
				}

				// Token: 0x0600B9D9 RID: 47577 RVA: 0x0025AB5C File Offset: 0x00258D5C
				public bool TryGetOffsetAndLength(string sectionName, TextRange range, out int offset, out int length)
				{
					return this.document.TryGetOffsetAndLength(sectionName, range, out offset, out length);
				}

				// Token: 0x0600B9DA RID: 47578 RVA: 0x0025AB6E File Offset: 0x00258D6E
				public IPartitionKey GetPartitionKeyAndOffset(string sectionName, int offset, int length, out int partitionOffset)
				{
					return this.GetPartitionKeyAndOffset(sectionName, offset, length, out partitionOffset);
				}

				// Token: 0x0600B9DB RID: 47579 RVA: 0x0025AB7B File Offset: 0x00258D7B
				public IEnumerable<PackageEdit> ReplacePartition(IPartitionKey partitionKey, SegmentedString expression)
				{
					return this.document.ReplacePartition(partitionKey, expression);
				}

				// Token: 0x0600B9DC RID: 47580 RVA: 0x0025AB8A File Offset: 0x00258D8A
				public IEnumerable<PackageEdit> ReferencePartition(IPartitionKey partitionKey, out string referencingExpression)
				{
					return this.document.ReferencePartition(partitionKey, out referencingExpression);
				}

				// Token: 0x0600B9DD RID: 47581 RVA: 0x0025AB99 File Offset: 0x00258D99
				public IPartitionedDocument ApplyEdits(IEnumerable<PackageEdit> edits)
				{
					return new MemberLetPartitionedDocument(this.engine, this.package.ApplyEdits(edits));
				}

				// Token: 0x0600B9DE RID: 47582 RVA: 0x0025ABB2 File Offset: 0x00258DB2
				public bool IsPartitionResultOfMember(IPartitionKey partitionKey)
				{
					return this.document.IsPartitionResultOfMember(partitionKey);
				}

				// Token: 0x0600B9DF RID: 47583 RVA: 0x0025ABC0 File Offset: 0x00258DC0
				public bool ArePartitionsOfSameMember(IPartitionKey partitionKey1, IPartitionKey partitionKey2)
				{
					return this.document.ArePartitionsOfSameMember(partitionKey1, partitionKey2);
				}

				// Token: 0x0600B9E0 RID: 47584 RVA: 0x0025ABCF File Offset: 0x00258DCF
				public IMemberLetPartitionKey GetSpecificPartitionKey(IMemberLetPartitionKey partitionKey)
				{
					return this.document.GetSpecificPartitionKey(partitionKey);
				}

				// Token: 0x04005E99 RID: 24217
				private readonly IEngine engine;

				// Token: 0x04005E9A RID: 24218
				private readonly IMemberLetPartitionedDocument document;

				// Token: 0x04005E9B RID: 24219
				private readonly IPackage package;
			}

			// Token: 0x02001D1C RID: 7452
			private sealed class RemotePackageSection : IPackageSection, IDocumentHost, ITranslateSourceLocation, ICacheableDocumentHost
			{
				// Token: 0x0600B9E1 RID: 47585 RVA: 0x0025ABDD File Offset: 0x00258DDD
				public RemotePackageSection()
				{
					this.cache = new Dictionary<SourceLocation, SourceLocation>(RemoteDocumentEvaluator.Service.RemotePackageSection.SourceLocationEqualityComparer.Instance);
				}

				// Token: 0x17002DF8 RID: 11768
				// (get) Token: 0x0600B9E2 RID: 47586 RVA: 0x0025ABF5 File Offset: 0x00258DF5
				public string UniqueID
				{
					get
					{
						return this.section.UniqueID;
					}
				}

				// Token: 0x17002DF9 RID: 11769
				// (get) Token: 0x0600B9E3 RID: 47587 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
				public object CacheIdentity
				{
					get
					{
						return this;
					}
				}

				// Token: 0x17002DFA RID: 11770
				// (get) Token: 0x0600B9E4 RID: 47588 RVA: 0x0025AC02 File Offset: 0x00258E02
				public IPackageSectionConfig Config
				{
					get
					{
						return this.section.Config;
					}
				}

				// Token: 0x17002DFB RID: 11771
				// (get) Token: 0x0600B9E5 RID: 47589 RVA: 0x0025AC0F File Offset: 0x00258E0F
				public SegmentedString Text
				{
					get
					{
						return this.section.Text;
					}
				}

				// Token: 0x0600B9E6 RID: 47590 RVA: 0x0025AC1C File Offset: 0x00258E1C
				public void Attach(IMessenger messenger, IPackageSection section)
				{
					this.messenger = messenger;
					this.section = section;
					this.cache.Clear();
				}

				// Token: 0x0600B9E7 RID: 47591 RVA: 0x0025AC38 File Offset: 0x00258E38
				public SourceLocation TranslateSourceLocation(SourceLocation location)
				{
					SourceLocation location2;
					if (!this.cache.TryGetValue(location, out location2))
					{
						using (IMessageChannel messageChannel = this.messenger.CreateChannel())
						{
							messageChannel.Post(new RemoteDocumentEvaluator.TranslateSourceLocationRequestMessage
							{
								Location = location
							});
							location2 = messageChannel.WaitFor<RemoteDocumentEvaluator.TranslateSourceLocationResponseMessage>().Location;
						}
						this.cache.Add(location, location2);
					}
					return location2;
				}

				// Token: 0x04005E9C RID: 24220
				private readonly Dictionary<SourceLocation, SourceLocation> cache;

				// Token: 0x04005E9D RID: 24221
				private IMessenger messenger;

				// Token: 0x04005E9E RID: 24222
				private IPackageSection section;

				// Token: 0x02001D1D RID: 7453
				private sealed class SourceLocationEqualityComparer : IEqualityComparer<SourceLocation>
				{
					// Token: 0x0600B9E8 RID: 47592 RVA: 0x000020FD File Offset: 0x000002FD
					private SourceLocationEqualityComparer()
					{
					}

					// Token: 0x0600B9E9 RID: 47593 RVA: 0x0025ACAC File Offset: 0x00258EAC
					public int GetHashCode(SourceLocation location)
					{
						int num = 0;
						if (location.Document.UniqueID != null)
						{
							num += location.Document.UniqueID.GetHashCode();
						}
						num += 1000 * location.Range.Start.Row + location.Range.Start.Column;
						return num + (1000 * location.Range.End.Row + location.Range.End.Column);
					}

					// Token: 0x0600B9EA RID: 47594 RVA: 0x0025AD48 File Offset: 0x00258F48
					public bool Equals(SourceLocation location1, SourceLocation location2)
					{
						return location1 == location2 || (location1 != null && location2 != null && (location1.Document.UniqueID == location2.Document.UniqueID && location1.Range.Start == location2.Range.Start) && location1.Range.End == location2.Range.End);
					}

					// Token: 0x04005E9F RID: 24223
					public static readonly IEqualityComparer<SourceLocation> Instance = new RemoteDocumentEvaluator.Service.RemotePackageSection.SourceLocationEqualityComparer();
				}
			}
		}

		// Token: 0x02001D26 RID: 7462
		public abstract class BeginGetResultMessage : BufferedMessage
		{
			// Token: 0x17002DFC RID: 11772
			// (get) Token: 0x0600B9FD RID: 47613 RVA: 0x0025AEE5 File Offset: 0x002590E5
			// (set) Token: 0x0600B9FE RID: 47614 RVA: 0x0025AEED File Offset: 0x002590ED
			public int EvaluationID { get; set; }

			// Token: 0x17002DFD RID: 11773
			// (get) Token: 0x0600B9FF RID: 47615 RVA: 0x0025AEF6 File Offset: 0x002590F6
			// (set) Token: 0x0600BA00 RID: 47616 RVA: 0x0025AEFE File Offset: 0x002590FE
			public bool EnableFirewall { get; set; }

			// Token: 0x17002DFE RID: 11774
			// (get) Token: 0x0600BA01 RID: 47617 RVA: 0x0025AF07 File Offset: 0x00259107
			// (set) Token: 0x0600BA02 RID: 47618 RVA: 0x0025AF0F File Offset: 0x0025910F
			public PartitioningScheme PartitioningScheme { get; set; }

			// Token: 0x17002DFF RID: 11775
			// (get) Token: 0x0600BA03 RID: 47619 RVA: 0x0025AF18 File Offset: 0x00259118
			// (set) Token: 0x0600BA04 RID: 47620 RVA: 0x0025AF20 File Offset: 0x00259120
			public IPackage Package { get; set; }

			// Token: 0x17002E00 RID: 11776
			// (get) Token: 0x0600BA05 RID: 47621 RVA: 0x0025AF29 File Offset: 0x00259129
			// (set) Token: 0x0600BA06 RID: 47622 RVA: 0x0025AF31 File Offset: 0x00259131
			public DocumentEvaluationParameters Parameters { get; set; }

			// Token: 0x0600BA07 RID: 47623 RVA: 0x0025AF3C File Offset: 0x0025913C
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteInt32(this.EvaluationID);
				writer.WriteBool(this.EnableFirewall);
				writer.WriteInt32((int)this.PartitioningScheme);
				writer.WriteIPackage(this.Parameters.document.Package);
				writer.WriteDocumentEvaluationParameters(this.Parameters);
			}

			// Token: 0x0600BA08 RID: 47624 RVA: 0x0025AF8F File Offset: 0x0025918F
			public override void Deserialize(BinaryReader reader)
			{
				this.EvaluationID = reader.ReadInt32();
				this.EnableFirewall = reader.ReadBool();
				this.PartitioningScheme = (PartitioningScheme)reader.ReadInt32();
				this.Package = reader.ReadIPackage();
				this.Parameters = reader.ReadDocumentEvaluationParameters();
			}
		}

		// Token: 0x02001D27 RID: 7463
		private sealed class BeginGetPreviewValueSourceMessage : RemoteDocumentEvaluator.BeginGetResultMessage
		{
		}

		// Token: 0x02001D28 RID: 7464
		private sealed class BeginGetDataReaderSourceMessage : RemoteDocumentEvaluator.BeginGetResultMessage
		{
		}

		// Token: 0x02001D29 RID: 7465
		private sealed class BeginGetStreamSourceMessage : RemoteDocumentEvaluator.BeginGetResultMessage
		{
		}

		// Token: 0x02001D2A RID: 7466
		private sealed class EndGetResultMessage : BufferedMessage
		{
			// Token: 0x0600BA0D RID: 47629 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600BA0E RID: 47630 RVA: 0x0000336E File Offset: 0x0000156E
			public override void Deserialize(BinaryReader reader)
			{
			}
		}

		// Token: 0x02001D2B RID: 7467
		private sealed class TranslateSourceLocationRequestMessage : BufferedMessage
		{
			// Token: 0x17002E01 RID: 11777
			// (get) Token: 0x0600BA10 RID: 47632 RVA: 0x0025AFD5 File Offset: 0x002591D5
			// (set) Token: 0x0600BA11 RID: 47633 RVA: 0x0025AFDD File Offset: 0x002591DD
			public SourceLocation Location { get; set; }

			// Token: 0x0600BA12 RID: 47634 RVA: 0x0025AFE6 File Offset: 0x002591E6
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteSourceLocation(this.Location);
			}

			// Token: 0x0600BA13 RID: 47635 RVA: 0x0025AFF4 File Offset: 0x002591F4
			public override void Deserialize(BinaryReader reader)
			{
				this.Location = reader.ReadSourceLocation();
			}
		}

		// Token: 0x02001D2C RID: 7468
		private sealed class TranslateSourceLocationResponseMessage : BufferedMessage
		{
			// Token: 0x17002E02 RID: 11778
			// (get) Token: 0x0600BA15 RID: 47637 RVA: 0x0025B002 File Offset: 0x00259202
			// (set) Token: 0x0600BA16 RID: 47638 RVA: 0x0025B00A File Offset: 0x0025920A
			public SourceLocation Location { get; set; }

			// Token: 0x0600BA17 RID: 47639 RVA: 0x0025B013 File Offset: 0x00259213
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteSourceLocation(this.Location);
			}

			// Token: 0x0600BA18 RID: 47640 RVA: 0x0025B021 File Offset: 0x00259221
			public override void Deserialize(BinaryReader reader)
			{
				this.Location = reader.ReadSourceLocation();
			}
		}
	}
}
