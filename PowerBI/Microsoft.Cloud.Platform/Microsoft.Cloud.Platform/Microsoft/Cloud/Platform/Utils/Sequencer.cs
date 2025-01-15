using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000282 RID: 642
	public abstract class Sequencer : ISequencer
	{
		// Token: 0x06001114 RID: 4372 RVA: 0x0003ABCC File Offset: 0x00038DCC
		protected Sequencer()
		{
			this.ShouldTraceAsyncSteps = true;
			this.m_flowCompletedSynchronously = true;
			this.m_flowStopRequested = new InterlockedBool(false);
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0003ABEE File Offset: 0x00038DEE
		protected Exception LastStepExceptionUnwrapped
		{
			get
			{
				return this.m_lastStepExceptionUnwrapped;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0003ABF6 File Offset: 0x00038DF6
		public DateTime ExecutionStartTime
		{
			get
			{
				return this.m_executionStartTime;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x0003ABFE File Offset: 0x00038DFE
		public DateTime LastStepStartTime
		{
			get
			{
				return this.m_lastStepStartTime;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x0003AC06 File Offset: 0x00038E06
		public DateTime LastStepCompletionTime
		{
			get
			{
				return this.m_lastStepCompletionTime;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0003AC0E File Offset: 0x00038E0E
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x0003AC16 File Offset: 0x00038E16
		protected bool ShouldTraceAsyncSteps { get; set; }

		// Token: 0x0600111B RID: 4379 RVA: 0x0003AC1F File Offset: 0x00038E1F
		protected Exception SwallowExceptionOfType<TException>(string step, Exception ex) where TException : Exception
		{
			if (ex is TException)
			{
				ex = null;
			}
			return ex;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0003AC2D File Offset: 0x00038E2D
		protected Exception SwallowExceptionOfType<TEx1, TEx2>(string step, Exception ex)
		{
			if (ex is TEx1 || ex is TEx2)
			{
				ex = null;
			}
			return ex;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0003AC43 File Offset: 0x00038E43
		protected Exception SwallowExceptionOfType<TEx1, TEx2, TEx3>(string step, Exception ex)
		{
			if (ex is TEx1 || ex is TEx2 || ex is TEx3)
			{
				ex = null;
			}
			return ex;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0003AC61 File Offset: 0x00038E61
		protected Exception SwallowExceptionOfType<TEx1, TEx2, TEx3, TEx4>(string step, Exception ex)
		{
			if (ex is TEx1 || ex is TEx2 || ex is TEx3 || ex is TEx4)
			{
				ex = null;
			}
			return ex;
		}

		// Token: 0x0600111F RID: 4383
		protected abstract IEnumerable<IFlowStep> Run();

		// Token: 0x06001120 RID: 4384 RVA: 0x000034FD File Offset: 0x000016FD
		protected virtual bool CanRunAsyncStep(string name)
		{
			return true;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0003AC88 File Offset: 0x00038E88
		protected internal virtual void Dump(TraceDump dumper)
		{
			dumper.Add(base.GetType().ToString() + " (Sequencer):");
			dumper.AddNameValue("  m_flowResult                ", this.m_flowResult);
			dumper.AddNameValue("  m_flowTaskCompletionSource  ", this.m_flowTaskCompletionSource);
			dumper.AddNameValue("  m_flowCompletedSynchronously", this.m_flowCompletedSynchronously);
			dumper.AddNameValue("  m_flowStopRequested         ", this.m_flowStopRequested);
			dumper.AddNameValue("  m_lastStepExceptionUnwrapped", this.m_lastStepExceptionUnwrapped);
			dumper.AddNameValue("  m_currentStep               ", this.m_currentStep);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0003AD20 File Offset: 0x00038F20
		public virtual Task ExecuteAsync(object state)
		{
			TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "ExecuteAsync on flow");
			this.EnsureSequenceNotStarted();
			this.m_flowTaskCompletionSource = new TaskCompletionSource<int>(state, TaskCreationOptions.None);
			this.ExecuteFirstAsyncStep();
			return this.m_flowTaskCompletionSource.Task;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0003AD57 File Offset: 0x00038F57
		public virtual void ExecuteWait()
		{
			Ensure.IsTrue(this.m_flowTaskCompletionSource != null, "Cannot call ExecuteWait without calling ExecuteAsync beforehand");
			this.m_flowTaskCompletionSource.Task.ExtendedWait();
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0003AD7C File Offset: 0x00038F7C
		public virtual IAsyncResult BeginExecute(AsyncCallback userCallback, object userContext)
		{
			TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "BeginExecute on flow");
			this.EnsureSequenceNotStarted();
			this.m_flowResult = new FlowExecuteAsyncResult(userCallback, userContext);
			this.ExecuteFirstAsyncStep();
			return this.m_flowResult;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0003ADAE File Offset: 0x00038FAE
		public virtual void EndExecute(IAsyncResult asyncResult)
		{
			TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "EndExecute on flow");
			ExtendedDiagnostics.EnsureArgument("asyncResult", asyncResult == this.m_flowResult, "The IAsyncResult inputted to the Sequencer EndExecute function should be the one returned from the BeginExecute function");
			this.m_flowResult.End();
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0003ADE4 File Offset: 0x00038FE4
		private bool ExecuteFirstAsyncStep()
		{
			this.m_executionStartTime = ExtendedDateTime.UtcNow;
			try
			{
				this.m_enumeratorSteps = this.Run().GetEnumerator();
				if (this.m_enumeratorSteps == null)
				{
					throw new InvalidOperationException("Sequencer.Run().GetEnumerator() returned an empty enumerator");
				}
			}
			catch (Exception ex)
			{
				this.CompleteFlow(ex);
				return false;
			}
			this.Advance();
			return true;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0003AE48 File Offset: 0x00039048
		private void RunAsyncStepCallbackApm(IAsyncResult ar)
		{
			Sequencer.FlowStepApm flowStepApm = ar.AsyncState as Sequencer.FlowStepApm;
			flowStepApm.AsyncResult = ar;
			this.RunAsyncStepCallback(flowStepApm);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0003AE70 File Offset: 0x00039070
		private void RunAsyncStepCallback(Sequencer.FlowStep flowStep)
		{
			using (flowStep.RestoreCapturedStackIfNeeded())
			{
				bool flag = Thread.CurrentThread.ManagedThreadId == flowStep.BeginThreadId;
				flowStep.CompletedSynchronously = flag;
				IEnumerator<IFlowStep> enumeratorSteps = this.m_enumeratorSteps;
				lock (enumeratorSteps)
				{
					this.m_flowCompletedSynchronously = this.m_flowCompletedSynchronously && flag;
				}
				try
				{
					Exception ex = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
					{
						this.InvokeAsyncEndFunc(flowStep);
					});
					if (ex != null)
					{
						this.CompleteFlow(ex);
						return;
					}
				}
				catch (CrashException ex2)
				{
					if (CurrentProcess.WellKnownHost == ProcessWellKnownHost.MSTest || CurrentProcess.WellKnownHost == ProcessWellKnownHost.UIAF)
					{
						this.CompleteFlow(ex2);
						return;
					}
					throw;
				}
				if (!flag)
				{
					this.Advance();
				}
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0003AF74 File Offset: 0x00039174
		protected virtual void Advance()
		{
			bool flag = true;
			IEnumerator<IFlowStep> enumeratorSteps = this.m_enumeratorSteps;
			lock (enumeratorSteps)
			{
				try
				{
					while (!this.m_flowStopRequested.InterlockedRead())
					{
						flag = this.m_enumeratorSteps.MoveNext();
						if (this.m_enumeratorSteps == null)
						{
							return;
						}
						IFlowStep flowStep = this.m_enumeratorSteps.Current;
						bool flag3;
						if (flowStep == null)
						{
							flag3 = true;
						}
						else
						{
							flag3 = flowStep.CompletedSynchronously;
							flag &= flowStep.ContinueRunning;
						}
						if (!flag || !flag3)
						{
							goto IL_0085;
						}
					}
					flag = false;
					TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Advance: Flow is stopped");
				}
				catch (Exception ex)
				{
					this.CompleteFlow(ex);
					return;
				}
				IL_0085:
				if (!flag)
				{
					this.CompleteFlow(null);
				}
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0003B038 File Offset: 0x00039238
		private void InvokeAsyncEndFunc(Sequencer.FlowStep step)
		{
			if (this.ShouldTraceAsyncSteps)
			{
				TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Info, "End running step: {0}", new object[] { step.Name });
			}
			try
			{
				step.InvokeEndFunc();
			}
			catch (Exception ex)
			{
				if (this.PropagateFlowStepException(step.Name, step.ExceptionHandler, ex))
				{
					throw;
				}
			}
			finally
			{
				this.m_currentStep = null;
				this.m_lastStepCompletionTime = ExtendedDateTime.UtcNow;
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0003B0C0 File Offset: 0x000392C0
		private void CompleteFlow(Exception exception)
		{
			TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Advance: Enumeration completed; no additional steps to run. Disposing the enumerator");
			if (this.m_enumeratorSteps != null)
			{
				this.m_enumeratorSteps.Dispose();
				this.m_enumeratorSteps = null;
			}
			if (!(exception is CrashException))
			{
				ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(this, exception);
			}
			if (this.m_flowResult != null)
			{
				this.m_flowResult.SignalCompletion(this.m_flowCompletedSynchronously, exception);
				return;
			}
			if (exception != null)
			{
				this.m_flowTaskCompletionSource.SetException(exception);
				return;
			}
			this.m_flowTaskCompletionSource.SetResult(0);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0003B13D File Offset: 0x0003933D
		public void MarkFlowAsStopped()
		{
			TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Info, "MarkFlowAsStopped Has been called...");
			this.m_flowStopRequested.InterlockedWrite(true);
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0003B15C File Offset: 0x0003935C
		private void EnsureSequenceNotStarted()
		{
			if (this.m_flowResult != null || this.m_flowTaskCompletionSource != null)
			{
				throw new InvalidOperationException("Sequencer.BeginExecute can only be called once in the lifetime of the object.");
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0003B17C File Offset: 0x0003937C
		private bool PropagateFlowStepException(string stepName, Sequencer.FlowStepExceptionHandler exceptionHandler, Exception ex)
		{
			this.m_lastStepExceptionUnwrapped = ex;
			ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(this, ex);
			if (exceptionHandler != null)
			{
				Exception ex2 = exceptionHandler(stepName, ex);
				if (ex2 == null)
				{
					TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Flow step {0} had an exception which the exception handler swallowed: {1}", new object[]
					{
						stepName,
						ex.GetType().Name
					});
					return false;
				}
				if (ex2 != ex)
				{
					TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Warning, "Flow step {0} had an exception {1} which the exception handler translated to: {2}", new object[]
					{
						stepName,
						ex.GetType().Name,
						ex2.GetType().Name
					});
					this.WarnIfExceptionDoestWrap(ex2, ex);
					throw ex2;
				}
			}
			TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Warning, "Flow step {0} had an exception: {1}", new object[]
			{
				stepName,
				ex.GetType().Name
			});
			return true;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0003B240 File Offset: 0x00039440
		private void WarnIfExceptionDoestWrap(Exception outer, Exception inner)
		{
			Exception ex = outer;
			while (outer != inner)
			{
				if (outer == null)
				{
					TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Warning, "Exception {0} should have exception {1} as an inner exception", new object[]
					{
						ex.GetType().Name,
						inner.GetType().Name
					});
					return;
				}
				outer = outer.InnerException;
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0003B294 File Offset: 0x00039494
		private IFlowStep RunAsyncStepApm([NotNull] string name, Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] Sequencer.AsyncBeginFunctionInternal begin, [NotNull] Sequencer.AsyncEndFunction end)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunctionInternal>(begin, "begin");
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncEndFunction>(end, "end");
			Sequencer.FlowStep flowStep = new Sequencer.FlowStepApm(name, begin, end, exceptionHandler);
			return this.RunAsyncStepInternal(name, flowStep, exceptionHandler);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0003B2D8 File Offset: 0x000394D8
		private IFlowStep RunAsyncStepInternal(string name, Sequencer.FlowStep flowStep, Sequencer.FlowStepExceptionHandler exceptionHandler)
		{
			Ensure.IsNull<IFlowStep>(this.m_currentStep, "m_currentStep");
			this.m_lastStepExceptionUnwrapped = null;
			if (this.m_flowStopRequested.InterlockedRead())
			{
				this.m_currentStep = new Sequencer.SkippedFlowStep(name, false);
				return this.m_currentStep;
			}
			try
			{
				if (!this.CanRunAsyncStep(name))
				{
					this.m_currentStep = new Sequencer.SkippedFlowStep(name, false);
					return this.m_currentStep;
				}
			}
			catch (Exception ex)
			{
				if (this.PropagateFlowStepException(name, exceptionHandler, ex))
				{
					throw;
				}
				this.m_currentStep = new Sequencer.SkippedFlowStep(name, true);
				return this.m_currentStep;
			}
			Sequencer.FlowStep flowStep2 = null;
			flowStep2 = flowStep;
			this.m_currentStep = flowStep;
			this.m_lastStepStartTime = ExtendedDateTime.UtcNow;
			try
			{
				if (this.ShouldTraceAsyncSteps)
				{
					TraceSourceBase<SequencerTrace>.Tracer.Trace(TraceVerbosity.Info, "Begin running step: {0}", new object[] { name });
				}
				flowStep.InvokeBeginFunc();
			}
			catch (Exception ex2)
			{
				this.m_currentStep = null;
				flowStep2.CompletedSynchronously = true;
				if (this.PropagateFlowStepException(name, exceptionHandler, ex2))
				{
					throw;
				}
			}
			finally
			{
				flowStep2.MarkAsyncCompletion();
			}
			return flowStep2;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0003B3FC File Offset: 0x000395FC
		[Pure]
		protected IFlowStep RunAsyncStep([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [InstantHandle] [NotNull] Sequencer.TaskBeginFunction begin)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.TaskBeginFunction>(begin, "begin");
			return this.RunAsyncStep(name, exceptionHandler, (AsyncCallback c, object s) => begin().ToApm(c, s), delegate(IAsyncResult ar)
			{
				((Task)ar).ExtendedWait();
			});
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0003B464 File Offset: 0x00039664
		[Pure]
		protected IFlowStep RunAsyncStep<TResult>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.TaskBeginFunction<TResult> begin, [NotNull] [InstantHandle] Action<TResult> end)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.TaskBeginFunction<TResult>>(begin, "begin");
			ExtendedDiagnostics.EnsureArgumentNotNull<Action<TResult>>(end, "end");
			return this.RunAsyncStep<TResult>(name, null, begin, end);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0003B494 File Offset: 0x00039694
		[Pure]
		protected IFlowStep RunAsyncStep<TResult>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.TaskBeginFunction<TResult> begin, [NotNull] [InstantHandle] Action<TResult> end)
		{
			Sequencer.<>c__DisplayClass78_0<TResult> CS$<>8__locals1 = new Sequencer.<>c__DisplayClass78_0<TResult>();
			CS$<>8__locals1.begin = begin;
			CS$<>8__locals1.end = end;
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.TaskBeginFunction<TResult>>(CS$<>8__locals1.begin, "begin");
			ExtendedDiagnostics.EnsureArgumentNotNull<Action<TResult>>(CS$<>8__locals1.end, "end");
			Sequencer.TaskBeginFunction taskBeginFunction = delegate
			{
				Sequencer.<>c__DisplayClass78_0<TResult>.<<RunAsyncStep>b__0>d <<RunAsyncStep>b__0>d;
				<<RunAsyncStep>b__0>d.<>4__this = CS$<>8__locals1;
				<<RunAsyncStep>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<RunAsyncStep>b__0>d.<>1__state = -1;
				AsyncTaskMethodBuilder <>t__builder = <<RunAsyncStep>b__0>d.<>t__builder;
				<>t__builder.Start<Sequencer.<>c__DisplayClass78_0<TResult>.<<RunAsyncStep>b__0>d>(ref <<RunAsyncStep>b__0>d);
				return <<RunAsyncStep>b__0>d.<>t__builder.Task;
			};
			return this.RunAsyncStep(name, exceptionHandler, taskBeginFunction);
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0003B4F8 File Offset: 0x000396F8
		[Pure]
		protected IFlowStep RunAsyncStep([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction begin, [InstantHandle] Sequencer.AsyncEndFunction end)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0003B540 File Offset: 0x00039740
		[Pure]
		protected IFlowStep RunAsyncStep<T1>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0003B590 File Offset: 0x00039790
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0003B5E8 File Offset: 0x000397E8
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0003B648 File Offset: 0x00039848
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0003B6B0 File Offset: 0x000398B0
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0003B720 File Offset: 0x00039920
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0003B798 File Offset: 0x00039998
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0003B818 File Offset: 0x00039A18
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0003B8A0 File Offset: 0x00039AA0
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0003B930 File Offset: 0x00039B30
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0003B9C8 File Offset: 0x00039BC8
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0003BA68 File Offset: 0x00039C68
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0003BB10 File Offset: 0x00039D10
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0003BBC0 File Offset: 0x00039DC0
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0003BC78 File Offset: 0x00039E78
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0003BD38 File Offset: 0x00039F38
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0003BE00 File Offset: 0x0003A000
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0003BED0 File Offset: 0x0003A0D0
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>([NotNull] string name, [InstantHandle] Sequencer.FlowStepExceptionHandler exceptionHandler, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>>(begin, "begin");
			return this.RunAsyncStepApm(name, exceptionHandler, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0003BFA8 File Offset: 0x0003A1A8
		[Pure]
		protected IFlowStep RunAsyncStep([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction begin, [InstantHandle] Sequencer.AsyncEndFunction end)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0003BFF0 File Offset: 0x0003A1F0
		[Pure]
		protected IFlowStep RunAsyncStep<T1>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0003C040 File Offset: 0x0003A240
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0003C098 File Offset: 0x0003A298
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0003C0F8 File Offset: 0x0003A2F8
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0003C160 File Offset: 0x0003A360
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0003C1D0 File Offset: 0x0003A3D0
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0003C248 File Offset: 0x0003A448
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0003C350 File Offset: 0x0003A550
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0003C3E0 File Offset: 0x0003A5E0
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003C478 File Offset: 0x0003A678
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0003C518 File Offset: 0x0003A718
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0003C5C0 File Offset: 0x0003A7C0
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0003C670 File Offset: 0x0003A870
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0003C728 File Offset: 0x0003A928
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0003C7E8 File Offset: 0x0003A9E8
		[Pure]
		protected IFlowStep RunAsyncStep<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([NotNull] string name, [NotNull] [InstantHandle] Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> begin, [InstantHandle] Sequencer.AsyncEndFunction end, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Sequencer.AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>>(begin, "begin");
			return this.RunAsyncStepApm(name, null, delegate(IFlowStep step)
			{
				begin(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, new AsyncCallback(this.RunAsyncStepCallbackApm), step);
			}, end);
		}

		// Token: 0x04000646 RID: 1606
		private IEnumerator<IFlowStep> m_enumeratorSteps;

		// Token: 0x04000647 RID: 1607
		private FlowExecuteAsyncResult m_flowResult;

		// Token: 0x04000648 RID: 1608
		private TaskCompletionSource<int> m_flowTaskCompletionSource;

		// Token: 0x04000649 RID: 1609
		private bool m_flowCompletedSynchronously;

		// Token: 0x0400064A RID: 1610
		private InterlockedBool m_flowStopRequested;

		// Token: 0x0400064B RID: 1611
		private Exception m_lastStepExceptionUnwrapped;

		// Token: 0x0400064C RID: 1612
		private DateTime m_executionStartTime;

		// Token: 0x0400064D RID: 1613
		private DateTime m_lastStepStartTime;

		// Token: 0x0400064E RID: 1614
		private DateTime m_lastStepCompletionTime;

		// Token: 0x04000650 RID: 1616
		private IFlowStep m_currentStep;

		// Token: 0x020006EF RID: 1775
		private abstract class FlowStep : IFlowStep
		{
			// Token: 0x17000745 RID: 1861
			// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x000A34E6 File Offset: 0x000A16E6
			// (set) Token: 0x06002EF2 RID: 12018 RVA: 0x000A34EE File Offset: 0x000A16EE
			public string Name { get; private set; }

			// Token: 0x17000746 RID: 1862
			// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000A34F7 File Offset: 0x000A16F7
			// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x000A34FF File Offset: 0x000A16FF
			public bool CompletedSynchronously { get; set; }

			// Token: 0x17000747 RID: 1863
			// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000034FD File Offset: 0x000016FD
			public bool ContinueRunning
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000748 RID: 1864
			// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000A3508 File Offset: 0x000A1708
			// (set) Token: 0x06002EF7 RID: 12023 RVA: 0x000A3510 File Offset: 0x000A1710
			public int BeginThreadId { get; private set; }

			// Token: 0x17000749 RID: 1865
			// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x000A3519 File Offset: 0x000A1719
			// (set) Token: 0x06002EF9 RID: 12025 RVA: 0x000A3521 File Offset: 0x000A1721
			public Sequencer.FlowStepExceptionHandler ExceptionHandler { get; private set; }

			// Token: 0x06002EFA RID: 12026 RVA: 0x000A352A File Offset: 0x000A172A
			public FlowStep(string name, Sequencer.FlowStepExceptionHandler exceptionHandler)
			{
				this.Name = name;
				this.BeginThreadId = Thread.CurrentThread.ManagedThreadId;
				this.ExceptionHandler = exceptionHandler;
				this.m_creationContextStack = UtilsContext.Current.CaptureStack();
			}

			// Token: 0x06002EFB RID: 12027 RVA: 0x000A3560 File Offset: 0x000A1760
			public void MarkAsyncCompletion()
			{
				this.BeginThreadId = -1;
			}

			// Token: 0x06002EFC RID: 12028 RVA: 0x000A3569 File Offset: 0x000A1769
			[CanBeNull]
			public IDisposable RestoreCapturedStackIfNeeded()
			{
				if (!UtilsContext.Current.IsCurrentActivityEqualToCaptured(this.m_creationContextStack))
				{
					return this.m_creationContextStack.Restore();
				}
				return null;
			}

			// Token: 0x06002EFD RID: 12029
			public abstract void InvokeBeginFunc();

			// Token: 0x06002EFE RID: 12030 RVA: 0x00009B3B File Offset: 0x00007D3B
			public virtual void InvokeEndFunc()
			{
			}

			// Token: 0x040013BA RID: 5050
			private IContextStack m_creationContextStack;
		}

		// Token: 0x020006F0 RID: 1776
		private class FlowStepApm : Sequencer.FlowStep, ITraceDumpable
		{
			// Token: 0x1700074A RID: 1866
			// (get) Token: 0x06002EFF RID: 12031 RVA: 0x000A358A File Offset: 0x000A178A
			// (set) Token: 0x06002F00 RID: 12032 RVA: 0x000A3592 File Offset: 0x000A1792
			public IAsyncResult AsyncResult { get; set; }

			// Token: 0x1700074B RID: 1867
			// (get) Token: 0x06002F01 RID: 12033 RVA: 0x000A359B File Offset: 0x000A179B
			// (set) Token: 0x06002F02 RID: 12034 RVA: 0x000A35A3 File Offset: 0x000A17A3
			private Sequencer.AsyncBeginFunctionInternal BeginFunc { get; set; }

			// Token: 0x1700074C RID: 1868
			// (get) Token: 0x06002F03 RID: 12035 RVA: 0x000A35AC File Offset: 0x000A17AC
			// (set) Token: 0x06002F04 RID: 12036 RVA: 0x000A35B4 File Offset: 0x000A17B4
			public Sequencer.AsyncEndFunction EndFunc { get; private set; }

			// Token: 0x06002F05 RID: 12037 RVA: 0x000A35BD File Offset: 0x000A17BD
			public FlowStepApm(string name, Sequencer.AsyncBeginFunctionInternal begin, Sequencer.AsyncEndFunction end, Sequencer.FlowStepExceptionHandler exceptionHandler)
				: base(name, exceptionHandler)
			{
				this.BeginFunc = begin;
				this.EndFunc = end;
			}

			// Token: 0x06002F06 RID: 12038 RVA: 0x000A35D6 File Offset: 0x000A17D6
			public override void InvokeBeginFunc()
			{
				this.BeginFunc(this);
			}

			// Token: 0x06002F07 RID: 12039 RVA: 0x000A35E4 File Offset: 0x000A17E4
			public override void InvokeEndFunc()
			{
				this.EndFunc(this.AsyncResult);
			}

			// Token: 0x06002F08 RID: 12040 RVA: 0x000A35F8 File Offset: 0x000A17F8
			public void Dump(TraceDump dumper)
			{
				dumper.Add(string.Concat(new string[]
				{
					base.GetType().ToString(),
					": step='",
					TraceDump.Dump(base.Name),
					"', endMethod='",
					TraceDump.Dump(this.EndFunc),
					"'"
				}));
			}
		}

		// Token: 0x020006F1 RID: 1777
		private class SkippedFlowStep : IFlowStep
		{
			// Token: 0x1700074D RID: 1869
			// (get) Token: 0x06002F09 RID: 12041 RVA: 0x000A3658 File Offset: 0x000A1858
			// (set) Token: 0x06002F0A RID: 12042 RVA: 0x000A3660 File Offset: 0x000A1860
			public string Name { get; private set; }

			// Token: 0x1700074E RID: 1870
			// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000034FD File Offset: 0x000016FD
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700074F RID: 1871
			// (get) Token: 0x06002F0C RID: 12044 RVA: 0x000A3669 File Offset: 0x000A1869
			// (set) Token: 0x06002F0D RID: 12045 RVA: 0x000A3671 File Offset: 0x000A1871
			public bool ContinueRunning { get; private set; }

			// Token: 0x06002F0E RID: 12046 RVA: 0x000A367A File Offset: 0x000A187A
			public SkippedFlowStep(string name, bool continueRunning)
			{
				this.Name = name;
				this.ContinueRunning = continueRunning;
			}

			// Token: 0x06002F0F RID: 12047 RVA: 0x000A3690 File Offset: 0x000A1890
			public void Dump(TraceDump dumper)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "{0}: step='{1}' CompletedSynchronously={2} ContinueRunning={3}", new object[]
				{
					base.GetType().ToString(),
					TraceDump.Dump(this.Name),
					this.CompletedSynchronously,
					this.ContinueRunning
				});
				dumper.Add(text);
			}
		}

		// Token: 0x020006F2 RID: 1778
		// (Invoke) Token: 0x06002F11 RID: 12049
		public delegate Exception FlowStepExceptionHandler(string name, Exception exception);

		// Token: 0x020006F3 RID: 1779
		// (Invoke) Token: 0x06002F15 RID: 12053
		private delegate void AsyncBeginFunctionInternal(IFlowStep step);

		// Token: 0x020006F4 RID: 1780
		// (Invoke) Token: 0x06002F19 RID: 12057
		public delegate IAsyncResult AsyncBeginFunction(AsyncCallback userCallback, object obj);

		// Token: 0x020006F5 RID: 1781
		// (Invoke) Token: 0x06002F1D RID: 12061
		public delegate Task TaskBeginFunction();

		// Token: 0x020006F6 RID: 1782
		// (Invoke) Token: 0x06002F21 RID: 12065
		public delegate Task<TResult> TaskBeginFunction<TResult>();

		// Token: 0x020006F7 RID: 1783
		// (Invoke) Token: 0x06002F25 RID: 12069
		public delegate IAsyncResult AsyncBeginFunction<T1>(T1 t1, AsyncCallback userCallback, object obj);

		// Token: 0x020006F8 RID: 1784
		// (Invoke) Token: 0x06002F29 RID: 12073
		public delegate IAsyncResult AsyncBeginFunction<T1, T2>(T1 t1, T2 t2, AsyncCallback userCallback, object obj);

		// Token: 0x020006F9 RID: 1785
		// (Invoke) Token: 0x06002F2D RID: 12077
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3>(T1 t1, T2 t2, T3 t3, AsyncCallback userCallback, object obj);

		// Token: 0x020006FA RID: 1786
		// (Invoke) Token: 0x06002F31 RID: 12081
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4, AsyncCallback userCallback, object obj);

		// Token: 0x020006FB RID: 1787
		// (Invoke) Token: 0x06002F35 RID: 12085
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, AsyncCallback userCallback, object obj);

		// Token: 0x020006FC RID: 1788
		// (Invoke) Token: 0x06002F39 RID: 12089
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, AsyncCallback userCallback, object obj);

		// Token: 0x020006FD RID: 1789
		// (Invoke) Token: 0x06002F3D RID: 12093
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, AsyncCallback userCallback, object obj);

		// Token: 0x020006FE RID: 1790
		// (Invoke) Token: 0x06002F41 RID: 12097
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, AsyncCallback userCallback, object obj);

		// Token: 0x020006FF RID: 1791
		// (Invoke) Token: 0x06002F45 RID: 12101
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, AsyncCallback userCallback, object obj);

		// Token: 0x02000700 RID: 1792
		// (Invoke) Token: 0x06002F49 RID: 12105
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, AsyncCallback userCallback, object obj);

		// Token: 0x02000701 RID: 1793
		// (Invoke) Token: 0x06002F4D RID: 12109
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, AsyncCallback userCallback, object obj);

		// Token: 0x02000702 RID: 1794
		// (Invoke) Token: 0x06002F51 RID: 12113
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, AsyncCallback userCallback, object obj);

		// Token: 0x02000703 RID: 1795
		// (Invoke) Token: 0x06002F55 RID: 12117
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, AsyncCallback userCallback, object obj);

		// Token: 0x02000704 RID: 1796
		// (Invoke) Token: 0x06002F59 RID: 12121
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, AsyncCallback userCallback, object obj);

		// Token: 0x02000705 RID: 1797
		// (Invoke) Token: 0x06002F5D RID: 12125
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, AsyncCallback userCallback, object obj);

		// Token: 0x02000706 RID: 1798
		// (Invoke) Token: 0x06002F61 RID: 12129
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, AsyncCallback userCallback, object obj);

		// Token: 0x02000707 RID: 1799
		// (Invoke) Token: 0x06002F65 RID: 12133
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, AsyncCallback userCallback, object obj);

		// Token: 0x02000708 RID: 1800
		// (Invoke) Token: 0x06002F69 RID: 12137
		public delegate IAsyncResult AsyncBeginFunction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12, T13 t13, T14 t14, T15 t15, T16 t16, T17 t17, T18 t18, AsyncCallback userCallback, object obj);

		// Token: 0x02000709 RID: 1801
		// (Invoke) Token: 0x06002F6D RID: 12141
		public delegate void AsyncEndFunction(IAsyncResult ar);

		// Token: 0x0200070A RID: 1802
		// (Invoke) Token: 0x06002F71 RID: 12145
		public delegate TResult AsyncEndFunction<TResult>(IAsyncResult ar);

		// Token: 0x0200070B RID: 1803
		protected class TraceExceptionWithHandler
		{
			// Token: 0x06002F74 RID: 12148 RVA: 0x000A36F2 File Offset: 0x000A18F2
			public TraceExceptionWithHandler([NotNull] ITraceSource trace, [NotNull] Func<string, Exception, Exception> handler)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<ITraceSource>(trace, "trace");
				ExtendedDiagnostics.EnsureArgumentNotNull<Func<string, Exception, Exception>>(handler, "handler");
				this.m_trace = trace;
				this.m_exceptionHandler = handler;
			}

			// Token: 0x06002F75 RID: 12149 RVA: 0x000A3720 File Offset: 0x000A1920
			public Exception Handler(string step, Exception ex)
			{
				if (ex == null)
				{
					return null;
				}
				Exception ex2 = this.m_exceptionHandler(step, ex);
				this.m_trace.TraceWarning(new StringBuilder().Append("Step '{0}'".FormatWithInvariantCulture(new object[] { step })).Append(' ').Append((ex2 == null) ? "Exception swallowed {" : "Original exception {")
					.Append(ex.ToString())
					.Append("}")
					.ToString());
				if (ex2 != null && ex != ex2)
				{
					this.m_trace.TraceWarning(new StringBuilder().Append("Step '{0}'".FormatWithInvariantCulture(new object[] { step })).Append(' ').Append("Translated exception {")
						.Append(ex2.ToString())
						.Append("}")
						.ToString());
				}
				return ex2;
			}

			// Token: 0x040013C4 RID: 5060
			private readonly ITraceSource m_trace;

			// Token: 0x040013C5 RID: 5061
			private readonly Func<string, Exception, Exception> m_exceptionHandler;
		}

		// Token: 0x0200070C RID: 1804
		protected class WrapExceptionOfType<TException> where TException : Exception
		{
			// Token: 0x06002F76 RID: 12150 RVA: 0x000A37FA File Offset: 0x000A19FA
			public WrapExceptionOfType([NotNull] Func<TException, Exception> wrapper)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<Func<TException, Exception>>(wrapper, "wrapper");
				this.m_wrapper = wrapper;
			}

			// Token: 0x06002F77 RID: 12151 RVA: 0x000A3814 File Offset: 0x000A1A14
			public Exception Handler(string step, Exception ex)
			{
				TException ex2 = ex as TException;
				if (ex2 != null)
				{
					ex = this.m_wrapper(ex2);
				}
				return ex;
			}

			// Token: 0x040013C6 RID: 5062
			private Func<TException, Exception> m_wrapper;
		}
	}
}
