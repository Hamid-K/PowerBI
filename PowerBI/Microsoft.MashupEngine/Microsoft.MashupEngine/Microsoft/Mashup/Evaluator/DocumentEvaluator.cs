using System;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C91 RID: 7313
	internal sealed class DocumentEvaluator : IDocumentEvaluator, IDocumentEvaluator<IPreviewValueSource>, IDocumentEvaluator<IDataReaderSource>, IDocumentEvaluator<IStreamSource>
	{
		// Token: 0x0600B5EE RID: 46574 RVA: 0x0024F00B File Offset: 0x0024D20B
		public DocumentEvaluator(IEngineHost engineHost, IEngine engine, Func<IEngineHost, IEngine, bool, IDocumentEvaluator> evaluatorCtor)
		{
			this.engineHost = engineHost;
			this.engine = engine;
			this.evaluatorCtor = evaluatorCtor;
		}

		// Token: 0x0600B5EF RID: 46575 RVA: 0x0024F028 File Offset: 0x0024D228
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IPreviewValueSource>> callback)
		{
			return this.BeginGetResult<IPreviewValueSource>(parameters, callback);
		}

		// Token: 0x0600B5F0 RID: 46576 RVA: 0x0024F032 File Offset: 0x0024D232
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IDataReaderSource>> callback)
		{
			return this.BeginGetResult<IDataReaderSource>(parameters, callback);
		}

		// Token: 0x0600B5F1 RID: 46577 RVA: 0x0024F03C File Offset: 0x0024D23C
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IStreamSource>> callback)
		{
			return this.BeginGetResult<IStreamSource>(parameters, callback);
		}

		// Token: 0x0600B5F2 RID: 46578 RVA: 0x0024F048 File Offset: 0x0024D248
		private IEvaluation BeginGetResult<T>(DocumentEvaluationParameters parameters, Action<EvaluationResult2<T>> callback) where T : IDisposable
		{
			IHostTrace trace = EvaluatorTracing.CreatePerformanceTrace("DocumentEvaluator/GetResult<" + typeof(T).FullName + ">", this.engineHost, TraceEventType.Information, null);
			IEvaluation evaluation;
			try
			{
				parameters.SetUiCulture();
				parameters = parameters.Clone();
				try
				{
					this.engineHost.QueryService<IDocumentValidator>().ValidateDocument(parameters.document);
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.TraceIsSafeException(trace, ex))
					{
						throw;
					}
					trace.Dispose();
					callback(new EvaluationResult2<T>(ex.ToCallbackException()));
					return new EmptyEvaluation();
				}
				ISourceErrorExceptionService serviceForDocument = this.engineHost.QueryService<IPartitionedDocumentSourceErrorExceptionService>().GetServiceForDocument(parameters.document);
				IPartitionDisplayNameService serviceForDocument2 = this.engineHost.QueryService<IPartitionedDocumentDisplayNameService>().GetServiceForDocument(parameters.document);
				IProgressService2 partitionProgressService = this.engineHost.QueryService<IPartitionProgressService>().GetPartitionProgressService(parameters.partitionKey);
				IGetStackFrameExtendedInfo partitionStackFrameExtendedInfo = this.engineHost.QueryService<IPartitionStackFrameExtendedInfo>().GetPartitionStackFrameExtendedInfo(parameters.document, parameters.partitionKey);
				IEngineHost engineHost = new CompositeEngineHost(new IEngineHost[]
				{
					new SimpleEngineHost<ISourceErrorExceptionService>(serviceForDocument),
					new SimpleEngineHost<IPartitionDisplayNameService>(serviceForDocument2),
					new SimpleEngineHost<IProgressService>(partitionProgressService),
					new SimpleEngineHost<IGetStackFrameExtendedInfo>(partitionStackFrameExtendedInfo),
					this.engineHost
				});
				RelationshipIdentityAscriber relationshipIdentityAscriber = new RelationshipIdentityAscriber(this.engine);
				parameters.document = relationshipIdentityAscriber.AscribeRelationshipIdentity(parameters.document);
				evaluation = ((IDocumentEvaluator<T>)this.evaluatorCtor(engineHost, this.engine, parameters.config.enableFirewall)).BeginGetResult(parameters, delegate(EvaluationResult2<T> result)
				{
					trace.Suspend();
					callback.InvokeThenOnDispose(result, delegate
					{
						using (trace)
						{
							T result;
							if (result.Exception == null)
							{
								result = result.Result;
								result.Dispose();
							}
						}
					});
				});
			}
			catch
			{
				trace.Dispose();
				throw;
			}
			return evaluation;
		}

		// Token: 0x04005CED RID: 23789
		private readonly IEngineHost engineHost;

		// Token: 0x04005CEE RID: 23790
		private readonly IEngine engine;

		// Token: 0x04005CEF RID: 23791
		private readonly Func<IEngineHost, IEngine, bool, IDocumentEvaluator> evaluatorCtor;
	}
}
