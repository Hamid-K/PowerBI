using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CE0 RID: 7392
	public abstract class LimitedDocumentEvaluatorFactory : IDisposable
	{
		// Token: 0x0600B886 RID: 47238 RVA: 0x00255ED3 File Offset: 0x002540D3
		protected LimitedDocumentEvaluatorFactory(string identity)
		{
			this.syncRoot = new object();
			this.identity = identity;
			this.pending = new List<LimitedDocumentEvaluatorFactory.Evaluation>();
			this.running = new List<LimitedDocumentEvaluatorFactory.Evaluation>();
		}

		// Token: 0x0600B887 RID: 47239 RVA: 0x00255F03 File Offset: 0x00254103
		public IDocumentEvaluator CreateDocumentEvaluator(IEngineHost engineHost, IEngine engine, Func<IEngineHost, IEngine, IDocumentEvaluator> evaluatorCtor)
		{
			return new LimitedDocumentEvaluatorFactory.Evaluator(this, engineHost, engine, evaluatorCtor);
		}

		// Token: 0x0600B888 RID: 47240 RVA: 0x00255F10 File Offset: 0x00254110
		public virtual void Dispose()
		{
			List<LimitedDocumentEvaluatorFactory.Evaluation> list = new List<LimitedDocumentEvaluatorFactory.Evaluation>();
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.pending != null)
				{
					list.AddRange(this.running);
					while (this.pending.Count > 0)
					{
						list.Add(LimitedDocumentEvaluatorFactory.Dequeue<LimitedDocumentEvaluatorFactory.Evaluation>(this.pending));
					}
					this.running = null;
					this.pending = null;
				}
			}
			foreach (LimitedDocumentEvaluatorFactory.Evaluation evaluation in list)
			{
				evaluation.Cancel();
			}
		}

		// Token: 0x17002DB7 RID: 11703
		// (get) Token: 0x0600B889 RID: 47241 RVA: 0x00255FD0 File Offset: 0x002541D0
		protected int PendingCount
		{
			get
			{
				return this.pending.Count;
			}
		}

		// Token: 0x17002DB8 RID: 11704
		// (get) Token: 0x0600B88A RID: 47242 RVA: 0x00255FDD File Offset: 0x002541DD
		protected int RunningCount
		{
			get
			{
				return this.running.Count;
			}
		}

		// Token: 0x0600B88B RID: 47243
		protected abstract bool ShouldEvaluateNextPending(IEvaluation evaluation);

		// Token: 0x0600B88C RID: 47244 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void OnEvaluationStarted(IEvaluation evaluation)
		{
		}

		// Token: 0x0600B88D RID: 47245 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void OnEvaluationCompleted(IEvaluation evaluation)
		{
		}

		// Token: 0x0600B88E RID: 47246 RVA: 0x00255FEA File Offset: 0x002541EA
		protected void CheckEvaluations()
		{
			this.EvaluationComplete(null);
		}

		// Token: 0x0600B88F RID: 47247 RVA: 0x00255FF3 File Offset: 0x002541F3
		private IEvaluation BeginGetResult<T>(LimitedDocumentEvaluatorFactory.Evaluator evaluator, DocumentEvaluationParameters parameters, Action<EvaluationResult2<T>> callback)
		{
			return this.BeginGetResult(new LimitedDocumentEvaluatorFactory.Evaluation<T>(evaluator, parameters.Clone(), callback));
		}

		// Token: 0x0600B890 RID: 47248 RVA: 0x00256008 File Offset: 0x00254208
		private IEvaluation BeginGetResult(LimitedDocumentEvaluatorFactory.Evaluation evaluation)
		{
			bool flag = false;
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.pending != null)
				{
					LimitedDocumentEvaluatorFactory.Enqueue<LimitedDocumentEvaluatorFactory.Evaluation>(this.pending, evaluation);
					flag = true;
				}
			}
			if (flag)
			{
				this.EvaluationComplete(null);
			}
			else
			{
				evaluation.Cancel();
			}
			return evaluation;
		}

		// Token: 0x0600B891 RID: 47249 RVA: 0x00256070 File Offset: 0x00254270
		private void EvaluationComplete(LimitedDocumentEvaluatorFactory.Evaluation evaluation = null)
		{
			for (;;)
			{
				LimitedDocumentEvaluatorFactory.Evaluation evaluation2 = null;
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.pending != null)
					{
						bool flag2 = this.running.Remove(evaluation);
						this.pending.Remove(evaluation);
						if (flag2 && evaluation != null)
						{
							using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace(base.GetType().Name + "/OnEvaluationCompleted", null, TraceEventType.Information, null))
							{
								this.OnEvaluationCompleted(evaluation);
								hostTrace.Add("identity", this.identity, false);
								hostTrace.Add("runningCount", this.running.Count, false);
								hostTrace.Add("pendingCount", this.pending.Count, false);
							}
						}
						if (this.pending.Count > 0 && this.ShouldEvaluateNextPending(LimitedDocumentEvaluatorFactory.Peek<LimitedDocumentEvaluatorFactory.Evaluation>(this.pending)))
						{
							evaluation2 = LimitedDocumentEvaluatorFactory.Dequeue<LimitedDocumentEvaluatorFactory.Evaluation>(this.pending);
							this.running.Add(evaluation2);
							using (IHostTrace hostTrace2 = EvaluatorTracing.CreateTrace(base.GetType().Name + "/OnEvaluationStarted", null, TraceEventType.Information, null))
							{
								this.OnEvaluationStarted(evaluation2);
								hostTrace2.Add("identity", this.identity, false);
								hostTrace2.Add("runningCount", this.running.Count, false);
								hostTrace2.Add("pendingCount", this.pending.Count, false);
							}
						}
					}
					if (evaluation2 == null)
					{
						break;
					}
				}
				if (evaluation2 != null)
				{
					evaluation2.BeginEvaluation();
				}
			}
		}

		// Token: 0x0600B892 RID: 47250 RVA: 0x00256264 File Offset: 0x00254464
		private static T Peek<T>(IList<T> queue)
		{
			return queue[0];
		}

		// Token: 0x0600B893 RID: 47251 RVA: 0x0025626D File Offset: 0x0025446D
		private static T Dequeue<T>(IList<T> queue)
		{
			T t = queue[0];
			queue.RemoveAt(0);
			return t;
		}

		// Token: 0x0600B894 RID: 47252 RVA: 0x0025627D File Offset: 0x0025447D
		private static void Enqueue<T>(IList<T> queue, T item)
		{
			queue.Add(item);
		}

		// Token: 0x04005DE0 RID: 24032
		private readonly object syncRoot;

		// Token: 0x04005DE1 RID: 24033
		private readonly string identity;

		// Token: 0x04005DE2 RID: 24034
		private List<LimitedDocumentEvaluatorFactory.Evaluation> pending;

		// Token: 0x04005DE3 RID: 24035
		private List<LimitedDocumentEvaluatorFactory.Evaluation> running;

		// Token: 0x02001CE1 RID: 7393
		private class Evaluator : IDocumentEvaluator, IDocumentEvaluator<IPreviewValueSource>, IDocumentEvaluator<IDataReaderSource>, IDocumentEvaluator<IStreamSource>
		{
			// Token: 0x0600B895 RID: 47253 RVA: 0x00256286 File Offset: 0x00254486
			public Evaluator(LimitedDocumentEvaluatorFactory factory, IEngineHost engineHost, IEngine engine, Func<IEngineHost, IEngine, IDocumentEvaluator> evaluatorCtor)
			{
				this.factory = factory;
				this.engineHost = engineHost;
				this.engine = engine;
				this.evaluatorCtor = evaluatorCtor;
			}

			// Token: 0x0600B896 RID: 47254 RVA: 0x002562AB File Offset: 0x002544AB
			public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IPreviewValueSource>> callback)
			{
				return this.factory.BeginGetResult<IPreviewValueSource>(this, parameters, callback);
			}

			// Token: 0x0600B897 RID: 47255 RVA: 0x002562BB File Offset: 0x002544BB
			public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IDataReaderSource>> callback)
			{
				return this.factory.BeginGetResult<IDataReaderSource>(this, parameters, callback);
			}

			// Token: 0x0600B898 RID: 47256 RVA: 0x002562CB File Offset: 0x002544CB
			public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IStreamSource>> callback)
			{
				return this.factory.BeginGetResult<IStreamSource>(this, parameters, callback);
			}

			// Token: 0x0600B899 RID: 47257 RVA: 0x002562DB File Offset: 0x002544DB
			public IDocumentEvaluator CreateDocumentEvaluator()
			{
				return this.evaluatorCtor(this.engineHost, this.engine);
			}

			// Token: 0x0600B89A RID: 47258 RVA: 0x002562F4 File Offset: 0x002544F4
			public void EvaluationComplete(LimitedDocumentEvaluatorFactory.Evaluation evaluation)
			{
				this.factory.EvaluationComplete(evaluation);
			}

			// Token: 0x04005DE4 RID: 24036
			private readonly LimitedDocumentEvaluatorFactory factory;

			// Token: 0x04005DE5 RID: 24037
			private readonly IEngineHost engineHost;

			// Token: 0x04005DE6 RID: 24038
			private readonly IEngine engine;

			// Token: 0x04005DE7 RID: 24039
			private readonly Func<IEngineHost, IEngine, IDocumentEvaluator> evaluatorCtor;
		}

		// Token: 0x02001CE2 RID: 7394
		private abstract class Evaluation : IEvaluation
		{
			// Token: 0x0600B89B RID: 47259
			public abstract void BeginEvaluation();

			// Token: 0x0600B89C RID: 47260
			public abstract void Cancel();
		}

		// Token: 0x02001CE3 RID: 7395
		private class Evaluation<T> : LimitedDocumentEvaluatorFactory.Evaluation
		{
			// Token: 0x0600B89E RID: 47262 RVA: 0x00256302 File Offset: 0x00254502
			public Evaluation(LimitedDocumentEvaluatorFactory.Evaluator evaluator, DocumentEvaluationParameters parameters, Action<EvaluationResult2<T>> callback)
			{
				this.evaluator = evaluator;
				this.parameters = parameters;
				this.callback = callback;
				this.evaluation = new CompositeEvaluation();
			}

			// Token: 0x0600B89F RID: 47263 RVA: 0x0025632C File Offset: 0x0025452C
			public override void BeginEvaluation()
			{
				CompositeEvaluation compositeEvaluation = this.evaluation;
				bool flag2;
				lock (compositeEvaluation)
				{
					flag2 = !this.complete && !this.cancelled;
					if (flag2)
					{
						this.started = true;
					}
				}
				if (flag2)
				{
					IDocumentEvaluator<T> documentEvaluator = (IDocumentEvaluator<T>)this.evaluator.CreateDocumentEvaluator();
					this.evaluation.Add(documentEvaluator.BeginGetResult(this.parameters, new Action<EvaluationResult2<T>>(this.Complete)));
				}
			}

			// Token: 0x0600B8A0 RID: 47264 RVA: 0x002563BC File Offset: 0x002545BC
			public override void Cancel()
			{
				CompositeEvaluation compositeEvaluation = this.evaluation;
				bool flag2;
				lock (compositeEvaluation)
				{
					flag2 = this.started;
					this.cancelled = true;
				}
				if (flag2)
				{
					this.evaluation.Cancel();
					return;
				}
				this.Complete(new EvaluationResult2<T>(new OperationCanceledException(Strings.Evaluation_Canceled)));
			}

			// Token: 0x0600B8A1 RID: 47265 RVA: 0x00256428 File Offset: 0x00254628
			private void Complete(EvaluationResult2<T> result)
			{
				Action<EvaluationResult2<T>> action = null;
				CompositeEvaluation compositeEvaluation = this.evaluation;
				lock (compositeEvaluation)
				{
					if (!this.complete)
					{
						this.complete = true;
						action = this.callback;
					}
				}
				if (action != null)
				{
					action.InvokeThenOnDispose(result, delegate
					{
						try
						{
							result.Dispose<T>();
						}
						finally
						{
							this.evaluator.EvaluationComplete(this);
						}
					});
				}
			}

			// Token: 0x04005DE8 RID: 24040
			private readonly LimitedDocumentEvaluatorFactory.Evaluator evaluator;

			// Token: 0x04005DE9 RID: 24041
			private readonly DocumentEvaluationParameters parameters;

			// Token: 0x04005DEA RID: 24042
			private readonly Action<EvaluationResult2<T>> callback;

			// Token: 0x04005DEB RID: 24043
			private readonly CompositeEvaluation evaluation;

			// Token: 0x04005DEC RID: 24044
			private bool started;

			// Token: 0x04005DED RID: 24045
			private bool complete;

			// Token: 0x04005DEE RID: 24046
			private bool cancelled;
		}
	}
}
