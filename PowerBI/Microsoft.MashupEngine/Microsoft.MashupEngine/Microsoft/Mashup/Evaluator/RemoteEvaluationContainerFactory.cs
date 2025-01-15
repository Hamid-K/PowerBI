using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D2E RID: 7470
	internal sealed class RemoteEvaluationContainerFactory : IContainerFactory, IDisposable
	{
		// Token: 0x0600BA1D RID: 47645 RVA: 0x0025B050 File Offset: 0x00259250
		public RemoteEvaluationContainerFactory(IContainerFactory containerFactory, IEngineHost engineHost)
		{
			this.containerFactory = containerFactory;
			this.engineHost = engineHost;
		}

		// Token: 0x0600BA1E RID: 47646 RVA: 0x0025B068 File Offset: 0x00259268
		public IContainer CreateContainer()
		{
			RemoteEvaluationContainerFactory.Container container = new RemoteEvaluationContainerFactory.Container(this.containerFactory.CreateContainer(), this.engineHost);
			try
			{
				container.Initialize();
			}
			catch
			{
				container.Dispose();
				throw;
			}
			return container;
		}

		// Token: 0x0600BA1F RID: 47647 RVA: 0x0025B0B0 File Offset: 0x002592B0
		public void Dispose()
		{
			if (this.containerFactory != null)
			{
				this.containerFactory = null;
				this.engineHost = null;
			}
		}

		// Token: 0x04005EB6 RID: 24246
		private IContainerFactory containerFactory;

		// Token: 0x04005EB7 RID: 24247
		private IEngineHost engineHost;

		// Token: 0x02001D2F RID: 7471
		private sealed class Container : IContainer, IDisposable
		{
			// Token: 0x0600BA20 RID: 47648 RVA: 0x0025B0C8 File Offset: 0x002592C8
			public Container(IContainer container, IEngineHost engineHost)
			{
				this.syncRoot = new object();
				this.container = container;
				this.containerID = container.ContainerID;
				this.container.Messenger.AddHandler(new Action<IMessageChannel, EvaluationHost.ExceptionMessage>(this.OnException));
				this.errorTranslatingMessenger = new ErrorTranslatingMessenger(this.container.Messenger, new Func<Exception, Exception>(this.TranslateMessengerException));
				this.channel = this.errorTranslatingMessenger.CreateChannel();
				this.channelMessenger = new ChannelMessenger(this.errorTranslatingMessenger.Handlers, this.channel, true);
				this.engineHost = new CompositeEngineHost(new IEngineHost[]
				{
					new SimpleEngineHost<IFeatureLoggingService>(container.Features),
					engineHost
				});
				ISignalEvaluationCanceled signalEvaluationCanceled;
				if (this.TryGetAs<ISignalEvaluationCanceled>(out signalEvaluationCanceled))
				{
					this.engineHost = new CompositeEngineHost(new IEngineHost[]
					{
						new SimpleEngineHost<ISignalEvaluationCanceled>(signalEvaluationCanceled),
						this.engineHost
					});
				}
			}

			// Token: 0x0600BA21 RID: 47649 RVA: 0x0025B1B8 File Offset: 0x002593B8
			public void Initialize()
			{
				IEvaluationMonitor evaluationMonitor;
				if (this.TryGetAs<IEvaluationMonitor>(out evaluationMonitor))
				{
					this.evaluationMonitorScope = evaluationMonitor.BeginEvaluation(this.engineHost);
				}
				this.services = RemoteServiceEnvironment.CreateServiceStubs(this.channelMessenger, this.engineHost);
			}

			// Token: 0x17002E03 RID: 11779
			// (get) Token: 0x0600BA22 RID: 47650 RVA: 0x0025B1F8 File Offset: 0x002593F8
			public int ContainerID
			{
				get
				{
					return this.containerID;
				}
			}

			// Token: 0x17002E04 RID: 11780
			// (get) Token: 0x0600BA23 RID: 47651 RVA: 0x0025B200 File Offset: 0x00259400
			public bool IsHealthy
			{
				get
				{
					return this.container.IsHealthy;
				}
			}

			// Token: 0x17002E05 RID: 11781
			// (get) Token: 0x0600BA24 RID: 47652 RVA: 0x0025B20D File Offset: 0x0025940D
			public IFeatureLoggingService Features
			{
				get
				{
					return this.container.Features;
				}
			}

			// Token: 0x17002E06 RID: 11782
			// (get) Token: 0x0600BA25 RID: 47653 RVA: 0x0025B21A File Offset: 0x0025941A
			public IMessenger Messenger
			{
				get
				{
					return this.channelMessenger;
				}
			}

			// Token: 0x0600BA26 RID: 47654 RVA: 0x0025B222 File Offset: 0x00259422
			public bool TryGetAs<T>(out T result) where T : class
			{
				return this.container.TryGetAs<T>(out result);
			}

			// Token: 0x0600BA27 RID: 47655 RVA: 0x0025B230 File Offset: 0x00259430
			public void Kill()
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteEvaluationContainerFactory/Container/Kill", this.engineHost, TraceEventType.Information, null))
				{
					hostTrace.Add("containerID", this.containerID, false);
					SafeExceptions.IgnoreSafeExceptions(this.engineHost, hostTrace, delegate
					{
						object obj = this.syncRoot;
						lock (obj)
						{
							if (this.container != null)
							{
								this.container.Kill();
							}
						}
					});
				}
			}

			// Token: 0x0600BA28 RID: 47656 RVA: 0x0025B29C File Offset: 0x0025949C
			public void Dispose()
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("RemoteEvaluationContainerFactory/Container/Dispose", this.engineHost, TraceEventType.Information, null))
				{
					hostTrace.Add("containerID", this.containerID, false);
					if (this.services != null)
					{
						RemoteServiceEnvironment.DisposeServiceStubs(this.channelMessenger, this.services, this.engineHost);
						this.services = null;
					}
					if (this.channelMessenger != null)
					{
						SafeExceptions.IgnoreSafeExceptions(this.engineHost, hostTrace, new Action(this.channelMessenger.Dispose));
						this.channelMessenger = null;
					}
					if (this.channel != null)
					{
						SafeExceptions.IgnoreSafeExceptions(this.engineHost, hostTrace, new Action(this.channel.Dispose));
						this.channel = null;
					}
					this.errorTranslatingMessenger = null;
					object obj = this.syncRoot;
					IDisposable disposable;
					IContainer container;
					lock (obj)
					{
						disposable = this.evaluationMonitorScope;
						this.evaluationMonitorScope = null;
						container = this.container;
						this.container = null;
					}
					if (disposable != null)
					{
						SafeExceptions.IgnoreSafeExceptions(this.engineHost, hostTrace, new Action(disposable.Dispose));
					}
					if (container != null)
					{
						container.Messenger.RemoveHandler<EvaluationHost.ExceptionMessage>();
						SafeExceptions.IgnoreSafeExceptions(this.engineHost, hostTrace, new Action(container.Dispose));
					}
				}
			}

			// Token: 0x0600BA29 RID: 47657 RVA: 0x0025B418 File Offset: 0x00259618
			private Exception TranslateMessengerException(Exception exception)
			{
				ErrorException ex = exception as ErrorException;
				if (ex != null && !ex.IsExpected)
				{
					IFeatureLoggingService featureLoggingService = this.engineHost.QueryService<IFeatureLoggingService>();
					if (featureLoggingService != null)
					{
						string[] array = featureLoggingService.GetLoggedFeatures().ToArray<string>();
						string text;
						if (array.Length != 0)
						{
							text = string.Join(", ", array);
						}
						else
						{
							text = Strings.Evaluation_NoFeatures;
						}
						exception = new ErrorException(Strings.Evaluation_UsedFeatures(exception.Message, text), ex);
					}
				}
				return exception;
			}

			// Token: 0x0600BA2A RID: 47658 RVA: 0x0025B47F File Offset: 0x0025967F
			private void OnException(IMessageChannel channel, EvaluationHost.ExceptionMessage message)
			{
				EvaluationHost.OnException(this.engineHost, channel, message);
			}

			// Token: 0x04005EB8 RID: 24248
			private readonly object syncRoot;

			// Token: 0x04005EB9 RID: 24249
			private readonly int containerID;

			// Token: 0x04005EBA RID: 24250
			private IContainer container;

			// Token: 0x04005EBB RID: 24251
			private IMessenger errorTranslatingMessenger;

			// Token: 0x04005EBC RID: 24252
			private IRemoteServiceStub[] services;

			// Token: 0x04005EBD RID: 24253
			private IMessageChannel channel;

			// Token: 0x04005EBE RID: 24254
			private IMessenger channelMessenger;

			// Token: 0x04005EBF RID: 24255
			private IEngineHost engineHost;

			// Token: 0x04005EC0 RID: 24256
			private IDisposable evaluationMonitorScope;
		}
	}
}
