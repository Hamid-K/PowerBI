using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D66 RID: 7526
	internal class SoftCancellingDocumentEvaluator : IDocumentEvaluator, IDocumentEvaluator<IPreviewValueSource>, IDocumentEvaluator<IDataReaderSource>, IDocumentEvaluator<IStreamSource>
	{
		// Token: 0x0600BB16 RID: 47894 RVA: 0x0025E3DA File Offset: 0x0025C5DA
		public SoftCancellingDocumentEvaluator(IEngineHost engineHost, IEngine engine, Func<IEngineHost, IEngine, IDocumentEvaluator> innerEvaluatorCtor, TimeSpan cancelDelay)
		{
			this.engineHost = engineHost;
			this.engine = engine;
			this.innerEvaluatorCtor = innerEvaluatorCtor;
			this.cancelDelay = cancelDelay;
		}

		// Token: 0x0600BB17 RID: 47895 RVA: 0x0025E3FF File Offset: 0x0025C5FF
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IPreviewValueSource>> callback)
		{
			return this.BeginGetResult<IPreviewValueSource>(new SoftCancellingDocumentEvaluator.PreviewValueSourceEvaluation(this), parameters, callback);
		}

		// Token: 0x0600BB18 RID: 47896 RVA: 0x0025E40F File Offset: 0x0025C60F
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IDataReaderSource>> callback)
		{
			return this.BeginGetResult<IDataReaderSource>(new SoftCancellingDocumentEvaluator.DataReaderSourceEvaluation(this), parameters, callback);
		}

		// Token: 0x0600BB19 RID: 47897 RVA: 0x0025E41F File Offset: 0x0025C61F
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IStreamSource>> callback)
		{
			return this.BeginGetResult<IStreamSource>(new SoftCancellingDocumentEvaluator.StreamSourceEvaluation(this), parameters, callback);
		}

		// Token: 0x0600BB1A RID: 47898 RVA: 0x0025E430 File Offset: 0x0025C630
		private IEvaluation BeginGetResult<T>(SoftCancellingDocumentEvaluator.Evaluation<T> evaluation, DocumentEvaluationParameters parameters, Action<EvaluationResult2<T>> callback)
		{
			ICancellationService cancellationService = new CancellationService();
			IEngineHost engineHost = new CompositeEngineHost(new IEngineHost[]
			{
				new SimpleEngineHost<ICancellationService>(cancellationService),
				this.engineHost
			});
			evaluation.CancellationService = cancellationService;
			IEvaluation evaluation2 = ((IDocumentEvaluator<T>)this.innerEvaluatorCtor(engineHost, this.engine)).BeginGetResult(parameters, delegate(EvaluationResult2<T> result)
			{
				if (result.Exception == null)
				{
					result = new EvaluationResult2<T>(evaluation.WrapResult(result.Result));
				}
				else if (evaluation.WasCancelled && Microsoft.Mashup.Common.SafeExceptions.IsSafeException(result.Exception))
				{
					result = new EvaluationResult2<T>(new OperationCanceledException());
				}
				callback.InvokeThenOnDispose(result, delegate
				{
					try
					{
						result.Dispose<T>();
					}
					finally
					{
						evaluation.Complete();
					}
				});
			});
			evaluation.InnerEvaluation = evaluation2;
			return evaluation;
		}

		// Token: 0x04005F3F RID: 24383
		private readonly IEngineHost engineHost;

		// Token: 0x04005F40 RID: 24384
		private readonly IEngine engine;

		// Token: 0x04005F41 RID: 24385
		private readonly Func<IEngineHost, IEngine, IDocumentEvaluator> innerEvaluatorCtor;

		// Token: 0x04005F42 RID: 24386
		private readonly TimeSpan cancelDelay;

		// Token: 0x02001D67 RID: 7527
		private abstract class Evaluation<T> : IEvaluation
		{
			// Token: 0x0600BB1B RID: 47899 RVA: 0x0025E4BD File Offset: 0x0025C6BD
			protected Evaluation(SoftCancellingDocumentEvaluator evaluator)
			{
				this.syncRoot = new object();
				this.evaluator = evaluator;
			}

			// Token: 0x17002E33 RID: 11827
			// (set) Token: 0x0600BB1C RID: 47900 RVA: 0x0025E4D8 File Offset: 0x0025C6D8
			public ICancellationService CancellationService
			{
				set
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						this.cancellationService = value;
					}
				}
			}

			// Token: 0x17002E34 RID: 11828
			// (set) Token: 0x0600BB1D RID: 47901 RVA: 0x0025E51C File Offset: 0x0025C71C
			public IEvaluation InnerEvaluation
			{
				set
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						this.innerEvaluation = value;
					}
				}
			}

			// Token: 0x17002E35 RID: 11829
			// (get) Token: 0x0600BB1E RID: 47902 RVA: 0x0025E560 File Offset: 0x0025C760
			public bool WasCancelled
			{
				get
				{
					object obj = this.syncRoot;
					bool flag2;
					lock (obj)
					{
						flag2 = this.cancelled;
					}
					return flag2;
				}
			}

			// Token: 0x0600BB1F RID: 47903
			public abstract T WrapResult(T result);

			// Token: 0x0600BB20 RID: 47904 RVA: 0x0025E5A4 File Offset: 0x0025C7A4
			public void CheckCancelled()
			{
				if (this.WasCancelled)
				{
					throw new OperationCanceledException();
				}
			}

			// Token: 0x0600BB21 RID: 47905 RVA: 0x0025E5B4 File Offset: 0x0025C7B4
			public void Complete()
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.innerEvaluation = null;
					if (this.timer != null)
					{
						this.timer.Dispose();
						this.timer = null;
					}
				}
			}

			// Token: 0x0600BB22 RID: 47906 RVA: 0x0025E610 File Offset: 0x0025C810
			public void Cancel()
			{
				using (EvaluatorTracing.CreateTrace("SoftCancellingDocumentEvaluator/Evaluation/Cancel", this.evaluator.engineHost, TraceEventType.Information, null))
				{
					ICancellationService cancellationService = null;
					object obj = this.syncRoot;
					lock (obj)
					{
						this.cancelled = true;
						if (this.innerEvaluation != null && this.timer == null)
						{
							this.timer = new Timer(new TimerCallback(this.CancelInnerEvaluation), null, this.evaluator.cancelDelay, TimeSpan.FromMilliseconds(-1.0));
							cancellationService = this.cancellationService;
						}
					}
					if (cancellationService != null && cancellationService.CancelAll() == 0)
					{
						this.CancelInnerEvaluation(null);
					}
				}
			}

			// Token: 0x0600BB23 RID: 47907 RVA: 0x0025E6E0 File Offset: 0x0025C8E0
			private void CancelInnerEvaluation(object state)
			{
				object obj = this.syncRoot;
				IEvaluation evaluation;
				lock (obj)
				{
					evaluation = this.innerEvaluation;
				}
				this.Complete();
				if (evaluation != null)
				{
					using (EvaluatorTracing.CreateTrace("SoftCancellingDocumentEvaluator/Evaluation/CancelInnerEvaluation", this.evaluator.engineHost, TraceEventType.Information, null))
					{
						evaluation.Cancel();
					}
				}
			}

			// Token: 0x04005F43 RID: 24387
			private readonly object syncRoot;

			// Token: 0x04005F44 RID: 24388
			private readonly SoftCancellingDocumentEvaluator evaluator;

			// Token: 0x04005F45 RID: 24389
			private ICancellationService cancellationService;

			// Token: 0x04005F46 RID: 24390
			private Timer timer;

			// Token: 0x04005F47 RID: 24391
			private IEvaluation innerEvaluation;

			// Token: 0x04005F48 RID: 24392
			private bool cancelled;
		}

		// Token: 0x02001D68 RID: 7528
		private class PreviewValueSourceEvaluation : SoftCancellingDocumentEvaluator.Evaluation<IPreviewValueSource>
		{
			// Token: 0x0600BB24 RID: 47908 RVA: 0x0025E760 File Offset: 0x0025C960
			public PreviewValueSourceEvaluation(SoftCancellingDocumentEvaluator evaluator)
				: base(evaluator)
			{
			}

			// Token: 0x0600BB25 RID: 47909 RVA: 0x0000A6A5 File Offset: 0x000088A5
			public override IPreviewValueSource WrapResult(IPreviewValueSource result)
			{
				return result;
			}
		}

		// Token: 0x02001D69 RID: 7529
		private class DataReaderSourceEvaluation : SoftCancellingDocumentEvaluator.Evaluation<IDataReaderSource>
		{
			// Token: 0x0600BB26 RID: 47910 RVA: 0x0025E769 File Offset: 0x0025C969
			public DataReaderSourceEvaluation(SoftCancellingDocumentEvaluator evaluator)
				: base(evaluator)
			{
			}

			// Token: 0x0600BB27 RID: 47911 RVA: 0x0025E772 File Offset: 0x0025C972
			public override IDataReaderSource WrapResult(IDataReaderSource result)
			{
				return new SoftCancellingDocumentEvaluator.DataReaderSourceEvaluation.SoftCancellingDataReaderSource(this, result);
			}

			// Token: 0x02001D6A RID: 7530
			private class SoftCancellingDataReaderSource : IDataReaderSource, IDisposable
			{
				// Token: 0x0600BB28 RID: 47912 RVA: 0x0025E77B File Offset: 0x0025C97B
				public SoftCancellingDataReaderSource(SoftCancellingDocumentEvaluator.DataReaderSourceEvaluation evaluation, IDataReaderSource dataReaderSource)
				{
					this.evaluation = evaluation;
					this.dataReaderSource = dataReaderSource;
				}

				// Token: 0x17002E36 RID: 11830
				// (get) Token: 0x0600BB29 RID: 47913 RVA: 0x0025E791 File Offset: 0x0025C991
				public ITableSource TableSource
				{
					get
					{
						return this.dataReaderSource.TableSource;
					}
				}

				// Token: 0x17002E37 RID: 11831
				// (get) Token: 0x0600BB2A RID: 47914 RVA: 0x0025E79E File Offset: 0x0025C99E
				public IPageReader PageReader
				{
					get
					{
						return new SoftCancellingDocumentEvaluator.DataReaderSourceEvaluation.SoftCancellingDataReaderSource.SoftCancellingPageReader(this.evaluation, this.dataReaderSource.PageReader);
					}
				}

				// Token: 0x0600BB2B RID: 47915 RVA: 0x0025E7B6 File Offset: 0x0025C9B6
				public void Dispose()
				{
					this.dataReaderSource.Dispose();
				}

				// Token: 0x04005F49 RID: 24393
				private readonly SoftCancellingDocumentEvaluator.DataReaderSourceEvaluation evaluation;

				// Token: 0x04005F4A RID: 24394
				private readonly IDataReaderSource dataReaderSource;

				// Token: 0x02001D6B RID: 7531
				private class SoftCancellingPageReader : DelegatingPageReader
				{
					// Token: 0x0600BB2C RID: 47916 RVA: 0x0025E7C3 File Offset: 0x0025C9C3
					public SoftCancellingPageReader(SoftCancellingDocumentEvaluator.DataReaderSourceEvaluation evaluation, IPageReader pageReader)
						: base(pageReader)
					{
						this.evaluation = evaluation;
					}

					// Token: 0x0600BB2D RID: 47917 RVA: 0x0025E7D4 File Offset: 0x0025C9D4
					public override void Read(IPage page)
					{
						this.evaluation.CheckCancelled();
						try
						{
							base.PageReader.Read(page);
						}
						catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
						{
							this.evaluation.CheckCancelled();
							throw;
						}
						this.evaluation.CheckCancelled();
					}

					// Token: 0x0600BB2E RID: 47918 RVA: 0x0025E83C File Offset: 0x0025CA3C
					public override IPageReader NextResult()
					{
						this.evaluation.CheckCancelled();
						IPageReader pageReader;
						try
						{
							pageReader = base.PageReader.NextResult();
						}
						catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
						{
							this.evaluation.CheckCancelled();
							throw;
						}
						this.evaluation.CheckCancelled();
						return pageReader;
					}

					// Token: 0x04005F4B RID: 24395
					private readonly SoftCancellingDocumentEvaluator.DataReaderSourceEvaluation evaluation;
				}
			}
		}

		// Token: 0x02001D6C RID: 7532
		private class StreamSourceEvaluation : SoftCancellingDocumentEvaluator.Evaluation<IStreamSource>
		{
			// Token: 0x0600BB2F RID: 47919 RVA: 0x0025E8A4 File Offset: 0x0025CAA4
			public StreamSourceEvaluation(SoftCancellingDocumentEvaluator evaluator)
				: base(evaluator)
			{
			}

			// Token: 0x0600BB30 RID: 47920 RVA: 0x0025E8AD File Offset: 0x0025CAAD
			public override IStreamSource WrapResult(IStreamSource result)
			{
				return new SoftCancellingDocumentEvaluator.StreamSourceEvaluation.SoftCancellingStreamSource(this, result);
			}

			// Token: 0x02001D6D RID: 7533
			private class SoftCancellingStreamSource : IStreamSource, IDisposable
			{
				// Token: 0x0600BB31 RID: 47921 RVA: 0x0025E8B6 File Offset: 0x0025CAB6
				public SoftCancellingStreamSource(SoftCancellingDocumentEvaluator.StreamSourceEvaluation evaluation, IStreamSource streamSource)
				{
					this.evaluation = evaluation;
					this.streamSource = streamSource;
				}

				// Token: 0x17002E38 RID: 11832
				// (get) Token: 0x0600BB32 RID: 47922 RVA: 0x0025E8CC File Offset: 0x0025CACC
				public Stream Stream
				{
					get
					{
						return new SoftCancellingDocumentEvaluator.StreamSourceEvaluation.SoftCancellingStreamSource.SoftCancellingStream(this.evaluation, this.streamSource.Stream);
					}
				}

				// Token: 0x0600BB33 RID: 47923 RVA: 0x0025E8E4 File Offset: 0x0025CAE4
				public void Dispose()
				{
					this.streamSource.Dispose();
				}

				// Token: 0x04005F4C RID: 24396
				private readonly SoftCancellingDocumentEvaluator.StreamSourceEvaluation evaluation;

				// Token: 0x04005F4D RID: 24397
				private readonly IStreamSource streamSource;

				// Token: 0x02001D6E RID: 7534
				private class SoftCancellingStream : DelegatingStream
				{
					// Token: 0x0600BB34 RID: 47924 RVA: 0x0025E8F1 File Offset: 0x0025CAF1
					public SoftCancellingStream(SoftCancellingDocumentEvaluator.StreamSourceEvaluation evaluation, Stream stream)
						: base(stream)
					{
						this.evaluation = evaluation;
					}

					// Token: 0x0600BB35 RID: 47925 RVA: 0x0025E904 File Offset: 0x0025CB04
					public override int Read(byte[] buffer, int offset, int count)
					{
						this.evaluation.CheckCancelled();
						int num;
						try
						{
							num = base.Read(buffer, offset, count);
						}
						catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
						{
							this.evaluation.CheckCancelled();
							throw;
						}
						this.evaluation.CheckCancelled();
						return num;
					}

					// Token: 0x0600BB36 RID: 47926 RVA: 0x0025E968 File Offset: 0x0025CB68
					public override int ReadByte()
					{
						this.evaluation.CheckCancelled();
						int num;
						try
						{
							num = base.ReadByte();
						}
						catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
						{
							this.evaluation.CheckCancelled();
							throw;
						}
						this.evaluation.CheckCancelled();
						return num;
					}

					// Token: 0x04005F4E RID: 24398
					private readonly SoftCancellingDocumentEvaluator.StreamSourceEvaluation evaluation;
				}
			}
		}
	}
}
