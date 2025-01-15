using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;
using Microsoft.Mashup.Shims.Interprocess;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D08 RID: 7432
	internal sealed class ProcessContainerFactory : IProcessContainerFactory, IContainerFactory, IDisposable
	{
		// Token: 0x0600B960 RID: 47456 RVA: 0x00258C4C File Offset: 0x00256E4C
		static ProcessContainerFactory()
		{
			AppDomain.CurrentDomain.DomainUnload += delegate(object sender, EventArgs args)
			{
				ProcessContainerFactory.appDomainUnloadEvent.Set();
				try
				{
					ProcessContainerFactory.counter.EnterWriteLock();
					ProcessContainerFactory.counter.ExitWriteLock();
				}
				finally
				{
					ProcessContainerFactory.appDomainUnloadEvent.Close();
					ProcessContainerFactory.counter.Dispose();
				}
			};
		}

		// Token: 0x0600B961 RID: 47457 RVA: 0x00258C80 File Offset: 0x00256E80
		public ProcessContainerFactory(EvaluatorConfiguration configuration)
		{
			this.configuration = configuration.Clone();
			this.configuration.ContainerLogFolderPath = ContainerProcessLog.Setup(this.configuration.ContainerLogFolderPath);
			this.exitMutexName = Guid.NewGuid().ToString();
			this.exitEvent = new ManualResetEvent(false);
			this.job = new ContainerJob(this.configuration.SharedMaxCommitInMB, this.configuration.ContainerMaxCommitInMB, this.configuration.ContainerProcessorAffinity);
			new Thread(SafeThread2.CreateThreadStart(new ThreadStart(this.MutexThread)))
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x0600B962 RID: 47458 RVA: 0x00258D2D File Offset: 0x00256F2D
		public IContainer CreateContainer()
		{
			return this.CreateProcessContainer();
		}

		// Token: 0x0600B963 RID: 47459 RVA: 0x00258D38 File Offset: 0x00256F38
		public IProcessContainer CreateProcessContainer()
		{
			if (this.job == null)
			{
				throw new ObjectDisposedException("ProcessContainerFactory");
			}
			IProcessContainer processContainer;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerFactory/CreateContainer", null, TraceEventType.Information, null))
			{
				ProcessContainerFactory.Container container = new ProcessContainerFactory.Container(this.configuration, this.exitMutexName, ProcessContainerFactory.NextContainerID(), this.job);
				hostTrace.Add("containerID", container.ContainerID, false);
				hostTrace.Add("containerPid", container.ProcessId, false);
				processContainer = container;
			}
			return processContainer;
		}

		// Token: 0x0600B964 RID: 47460 RVA: 0x00258DD0 File Offset: 0x00256FD0
		public void Dispose()
		{
			this.exitEvent.Set();
			if (this.job != null)
			{
				this.job.Dispose();
			}
		}

		// Token: 0x0600B965 RID: 47461 RVA: 0x00258DF1 File Offset: 0x00256FF1
		private static int NextContainerID()
		{
			return Interlocked.Increment(ref ProcessContainerFactory.nextContainerId);
		}

		// Token: 0x0600B966 RID: 47462 RVA: 0x00258E00 File Offset: 0x00257000
		private void MutexThread()
		{
			Mutex mutex = MutexFactory.Create(true, this.exitMutexName);
			ProcessContainerFactory.counter.EnterReadLock();
			try
			{
				WaitHandle[] array = new ManualResetEvent[]
				{
					this.exitEvent,
					ProcessContainerFactory.appDomainUnloadEvent
				};
				WaitHandle.WaitAny(array);
				mutex.ReleaseMutex();
			}
			finally
			{
				ProcessContainerFactory.counter.ExitReadLock();
				mutex.Close();
			}
		}

		// Token: 0x04005E59 RID: 24153
		private static ManualResetEvent appDomainUnloadEvent = new ManualResetEvent(false);

		// Token: 0x04005E5A RID: 24154
		private static ReaderWriterLockSlim counter = new ReaderWriterLockSlim();

		// Token: 0x04005E5B RID: 24155
		private static int nextContainerId;

		// Token: 0x04005E5C RID: 24156
		private readonly EvaluatorConfiguration configuration;

		// Token: 0x04005E5D RID: 24157
		private readonly string exitMutexName;

		// Token: 0x04005E5E RID: 24158
		private readonly ManualResetEvent exitEvent;

		// Token: 0x04005E5F RID: 24159
		private readonly ContainerJob job;

		// Token: 0x02001D09 RID: 7433
		private class Container : IProcessContainer, IContainer, IDisposable, IEvaluationMonitor, ISignalEvaluationCanceled
		{
			// Token: 0x17002DE4 RID: 11748
			// (get) Token: 0x0600B967 RID: 47463 RVA: 0x00258E6C File Offset: 0x0025706C
			public int ProcessId
			{
				get
				{
					return this.process.Id;
				}
			}

			// Token: 0x0600B968 RID: 47464 RVA: 0x00258E7C File Offset: 0x0025707C
			public Container(EvaluatorConfiguration configuration, string exitMutexName, int containerID, ContainerJob job)
			{
				this.syncRoot = new object();
				this.containerID = containerID;
				this.features = new FeatureLoggingService();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(new string[]
				{
					exitMutexName,
					" ",
					containerID.ToString(CultureInfo.InvariantCulture)
				});
				this.process = ProcessContainerFactory.Container.CreateProcess(configuration, stringBuilder.ToString(), job);
				this.messenger = new ErrorTranslatingMessenger(new StreamMessenger
				{
					InputStream = this.process.ContainerToHostStream,
					OutputStream = this.process.HostToContainerStream
				}, new Func<Exception, Exception>(this.TranslateMessengerException));
			}

			// Token: 0x17002DE5 RID: 11749
			// (get) Token: 0x0600B969 RID: 47465 RVA: 0x00258F2E File Offset: 0x0025712E
			public int ContainerID
			{
				get
				{
					return this.containerID;
				}
			}

			// Token: 0x17002DE6 RID: 11750
			// (get) Token: 0x0600B96A RID: 47466 RVA: 0x00258F36 File Offset: 0x00257136
			public bool IsHealthy
			{
				get
				{
					return !this.process.HasExited;
				}
			}

			// Token: 0x17002DE7 RID: 11751
			// (get) Token: 0x0600B96B RID: 47467 RVA: 0x00258F46 File Offset: 0x00257146
			public IFeatureLoggingService Features
			{
				get
				{
					return this.features;
				}
			}

			// Token: 0x17002DE8 RID: 11752
			// (get) Token: 0x0600B96C RID: 47468 RVA: 0x00258F50 File Offset: 0x00257150
			public WaitHandle EvaluationCanceled
			{
				get
				{
					object obj = this.syncRoot;
					WaitHandle waitHandle;
					lock (obj)
					{
						if (this.evaluationCanceled == null && this.process != null)
						{
							this.evaluationCanceled = new ManualResetEvent(this.killed);
						}
						waitHandle = this.evaluationCanceled;
					}
					return waitHandle;
				}
			}

			// Token: 0x17002DE9 RID: 11753
			// (get) Token: 0x0600B96D RID: 47469 RVA: 0x00258FB4 File Offset: 0x002571B4
			public IMessenger Messenger
			{
				get
				{
					return this.messenger;
				}
			}

			// Token: 0x0600B96E RID: 47470 RVA: 0x00258FBC File Offset: 0x002571BC
			public static ContainerProcess CreateProcess(EvaluatorConfiguration configuration, string arguments, ContainerJob job)
			{
				ContainerProcess containerProcess = null;
				ContainerProcess containerProcess2;
				try
				{
					containerProcess = new ContainerProcess(configuration, arguments, job);
					containerProcess.Start();
					containerProcess2 = containerProcess;
				}
				catch
				{
					if (containerProcess != null)
					{
						containerProcess.Dispose();
					}
					throw;
				}
				return containerProcess2;
			}

			// Token: 0x0600B96F RID: 47471 RVA: 0x00258FFC File Offset: 0x002571FC
			public void SetProcessWorkingSetSize(int maxWorkingSetInMB)
			{
				this.process.SetProcessWorkingSetSize(maxWorkingSetInMB);
			}

			// Token: 0x0600B970 RID: 47472 RVA: 0x00182EDC File Offset: 0x001810DC
			public bool TryGetAs<T>(out T result) where T : class
			{
				result = this as T;
				return result != null;
			}

			// Token: 0x0600B971 RID: 47473 RVA: 0x0025900C File Offset: 0x0025720C
			public void Kill()
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerFactory/Container/Kill", null, TraceEventType.Information, null))
				{
					hostTrace.Add("containerID", this.containerID, false);
					hostTrace.Add("containerPid", this.process.Id, false);
					object obj = this.syncRoot;
					lock (obj)
					{
						if (this.process != null)
						{
							this.SafeKillProcess(hostTrace);
						}
						if (this.evaluationCanceled != null)
						{
							this.evaluationCanceled.Set();
						}
						this.killed = true;
					}
				}
			}

			// Token: 0x0600B972 RID: 47474 RVA: 0x002590C8 File Offset: 0x002572C8
			public IDisposable BeginEvaluation(IEngineHost engineHost)
			{
				IContainerEvaluationMonitorService containerEvaluationMonitorService = engineHost.QueryService<IContainerEvaluationMonitorService>();
				if (containerEvaluationMonitorService == null)
				{
					return null;
				}
				object obj = this.syncRoot;
				SafeHandle safeHandle;
				lock (obj)
				{
					ContainerProcess containerProcess = this.process;
					safeHandle = ((containerProcess != null) ? containerProcess.GetProcessHandle() : null);
				}
				IDisposable disposable;
				using (safeHandle)
				{
					disposable = new ProcessContainerFactory.Container.EndOfEvaluation(containerEvaluationMonitorService.BeginEvaluation(safeHandle));
				}
				return disposable;
			}

			// Token: 0x0600B973 RID: 47475 RVA: 0x00259150 File Offset: 0x00257350
			public void Dispose()
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerFactory/Container/Dispose", null, TraceEventType.Information, null))
				{
					hostTrace.Add("containerID", this.containerID, false);
					object obj = this.syncRoot;
					lock (obj)
					{
						if (this.process != null)
						{
							try
							{
								this.SafeKillProcess(hostTrace);
							}
							finally
							{
								this.process.Dispose();
								this.process = null;
							}
						}
						EventWaitHandle eventWaitHandle = this.evaluationCanceled;
						if (eventWaitHandle != null)
						{
							eventWaitHandle.Close();
						}
						this.evaluationCanceled = null;
					}
				}
			}

			// Token: 0x0600B974 RID: 47476 RVA: 0x00259210 File Offset: 0x00257410
			private void SafeKillProcess(IHostTrace trace)
			{
				try
				{
					this.process.Kill();
				}
				catch (Exception ex)
				{
					if (!SafeExceptions.TraceIsSafeException(trace, ex))
					{
						throw;
					}
				}
			}

			// Token: 0x0600B975 RID: 47477 RVA: 0x00259248 File Offset: 0x00257448
			private Exception TranslateMessengerException(Exception exception)
			{
				Exception ex;
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("ContainerFactory/Container/TranslateMessengerException", null, TraceEventType.Information, null))
				{
					int id = this.process.Id;
					try
					{
						this.process.WaitForExit();
						int exitCode = (int)this.process.ExitCode;
						hostTrace.Add("exitCode", exitCode, false);
						if (exitCode <= -2147023895)
						{
							if (exitCode == -2147024882)
							{
								return new ErrorException(Strings.Evaluation_OutOfMemory, false, true);
							}
							if (exitCode != -2147023895)
							{
								goto IL_00A0;
							}
						}
						else if (exitCode != -1073741571)
						{
							if (exitCode != -467599358)
							{
								goto IL_00A0;
							}
							return new ContainerTerminatedException(Strings.Container_Terminated);
						}
						return new ErrorException(Strings.Evaluation_StackOverflow, false, true);
						IL_00A0:
						string text = this.process.ReadContainerLog();
						ex = new ErrorException(Strings.Evaluation_ContainerExitedUnexpectedly(exitCode, id), null, null, text, false, false, null);
					}
					catch (Exception ex2)
					{
						if (!SafeExceptions.TraceIsSafeException(hostTrace, ex2))
						{
							throw;
						}
						ex = new ErrorException(Strings.Evaluation_TerminatedUnexpectedly(id, ex2.ToString()), exception.ToErrorException());
					}
				}
				return ex;
			}

			// Token: 0x04005E60 RID: 24160
			private readonly object syncRoot;

			// Token: 0x04005E61 RID: 24161
			private int containerID;

			// Token: 0x04005E62 RID: 24162
			private ContainerProcess process;

			// Token: 0x04005E63 RID: 24163
			private IFeatureLoggingService features;

			// Token: 0x04005E64 RID: 24164
			private IMessenger messenger;

			// Token: 0x04005E65 RID: 24165
			private bool killed;

			// Token: 0x04005E66 RID: 24166
			private EventWaitHandle evaluationCanceled;

			// Token: 0x02001D0A RID: 7434
			private class EndOfEvaluation : IDisposable
			{
				// Token: 0x0600B976 RID: 47478 RVA: 0x00259378 File Offset: 0x00257578
				public EndOfEvaluation(IDisposable callback)
				{
					this.callback = callback;
				}

				// Token: 0x0600B977 RID: 47479 RVA: 0x00259387 File Offset: 0x00257587
				public void Dispose()
				{
					if (this.callback != null)
					{
						this.callback.Dispose();
						this.callback = null;
					}
				}

				// Token: 0x04005E67 RID: 24167
				private IDisposable callback;
			}
		}
	}
}
