using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000397 RID: 919
	[BlockServiceProvider(typeof(IEventsQuery))]
	public class EventsQuery : Block, IEventsQuery
	{
		// Token: 0x06001C4D RID: 7245 RVA: 0x0006BEDC File Offset: 0x0006A0DC
		public EventsQuery()
			: base(typeof(EventsQuery).Name)
		{
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0006BEF3 File Offset: 0x0006A0F3
		public IAsyncResult BeginQueryEvents(DateTime from, DateTime to, EventsQueryFilter filter, string downloadDirectory, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			return SequencerInvoker<EventsQuery.QueryEventsByTimeRange>.BeginExecute(new EventsQuery.QueryEventsByTimeRange(this.m_eventingRepository, this.m_eventsReader, filter, from, to, downloadDirectory, options, notifications), base.WorkTicketManager.CreateWorkTicket(this), callback, context);
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0006BF24 File Offset: 0x0006A124
		public IAsyncResult BeginQueryEvents(string connectionString, DateTime from, DateTime to, EventsQueryFilter filter, string downloadDirectory, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			return SequencerInvoker<EventsQuery.QueryEventsByTimeRange>.BeginExecute(new EventsQuery.QueryEventsByTimeRange(this.m_eventingRepository, this.m_eventsReader, filter, from, to, downloadDirectory, options, notifications, connectionString), base.WorkTicketManager.CreateWorkTicket(this), callback, context);
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0006BF62 File Offset: 0x0006A162
		public IEnumerable<EtwEvent> EndQueryEvents(IAsyncResult asyncResult)
		{
			return SequencerInvoker<EventsQuery.QueryEventsByTimeRange>.EndExecute(asyncResult).Events;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x0006BF6F File Offset: 0x0006A16F
		public IAsyncResult BeginQueryEventsContinued(DateTime from, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			return SequencerInvoker<EventsQuery.QueryEventsByContinuation>.BeginExecute(new EventsQuery.QueryEventsByContinuation(this.m_eventingRepository, this.m_eventsReader, from, downloadDirectory, maxSizeToDownloadInMb, options, notifications), base.WorkTicketManager.CreateWorkTicket(this), callback, context);
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x0006BF9E File Offset: 0x0006A19E
		public IAsyncResult BeginQueryEventsContinued(string connectionString, DateTime from, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			return SequencerInvoker<EventsQuery.QueryEventsByContinuation>.BeginExecute(new EventsQuery.QueryEventsByContinuation(this.m_eventingRepository, this.m_eventsReader, from, downloadDirectory, maxSizeToDownloadInMb, options, notifications, connectionString), base.WorkTicketManager.CreateWorkTicket(this), callback, context);
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0006BFCF File Offset: 0x0006A1CF
		public IAsyncResult BeginQueryEventsContinued([NotNull] IEventingRepositoryContinuation continuation, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventingRepositoryContinuation>(continuation, "continuation");
			return SequencerInvoker<EventsQuery.QueryEventsByContinuation>.BeginExecute(new EventsQuery.QueryEventsByContinuation(this.m_eventingRepository, this.m_eventsReader, continuation, downloadDirectory, maxSizeToDownloadInMb, options, notifications), base.WorkTicketManager.CreateWorkTicket(this), callback, context);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0006C009 File Offset: 0x0006A209
		public IAsyncResult BeginQueryEventsContinued(string connectionString, [NotNull] IEventingRepositoryContinuation continuation, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventingRepositoryContinuation>(continuation, "continuation");
			return SequencerInvoker<EventsQuery.QueryEventsByContinuation>.BeginExecute(new EventsQuery.QueryEventsByContinuation(this.m_eventingRepository, this.m_eventsReader, continuation, downloadDirectory, maxSizeToDownloadInMb, options, notifications, connectionString), base.WorkTicketManager.CreateWorkTicket(this), callback, context);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0006C048 File Offset: 0x0006A248
		public EventsQueryResult EndQueryEventsContinued(IAsyncResult asyncResult)
		{
			EventsQuery.QueryEventsByContinuation queryEventsByContinuation = SequencerInvoker<EventsQuery.QueryEventsByContinuation>.EndExecute(asyncResult);
			return new EventsQueryResult(queryEventsByContinuation.Events, queryEventsByContinuation.ContinuationStatus);
		}

		// Token: 0x04000985 RID: 2437
		[BlockServiceDependency]
		private IEtwEventsReader m_eventsReader;

		// Token: 0x04000986 RID: 2438
		[BlockServiceDependency]
		private IEventingRepository m_eventingRepository;

		// Token: 0x020007C0 RID: 1984
		private abstract class QueryEventsFlowBase : Sequencer
		{
			// Token: 0x1700076E RID: 1902
			// (get) Token: 0x0600317A RID: 12666 RVA: 0x000A7F17 File Offset: 0x000A6117
			// (set) Token: 0x0600317B RID: 12667 RVA: 0x000A7F1F File Offset: 0x000A611F
			private protected IEventingRepository EventingRepository { protected get; private set; }

			// Token: 0x1700076F RID: 1903
			// (get) Token: 0x0600317C RID: 12668 RVA: 0x000A7F28 File Offset: 0x000A6128
			// (set) Token: 0x0600317D RID: 12669 RVA: 0x000A7F30 File Offset: 0x000A6130
			private protected IEtwEventsReader EventsReader { protected get; private set; }

			// Token: 0x17000770 RID: 1904
			// (get) Token: 0x0600317E RID: 12670 RVA: 0x000A7F39 File Offset: 0x000A6139
			protected EventsQueryOptions Options
			{
				get
				{
					return this.m_options;
				}
			}

			// Token: 0x17000771 RID: 1905
			// (get) Token: 0x0600317F RID: 12671 RVA: 0x000A7F41 File Offset: 0x000A6141
			// (set) Token: 0x06003180 RID: 12672 RVA: 0x000A7F49 File Offset: 0x000A6149
			private protected IEventingRepositoryDownloadNotifications Notifications { protected get; private set; }

			// Token: 0x17000772 RID: 1906
			// (get) Token: 0x06003181 RID: 12673 RVA: 0x000A7F52 File Offset: 0x000A6152
			// (set) Token: 0x06003182 RID: 12674 RVA: 0x000A7F5A File Offset: 0x000A615A
			public IEnumerable<EtwEvent> Events { get; private set; }

			// Token: 0x06003183 RID: 12675 RVA: 0x000A7F63 File Offset: 0x000A6163
			protected QueryEventsFlowBase(IEventingRepository repository, IEtwEventsReader eventsReader, string downloadDirectory, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications)
			{
				this.EventingRepository = repository;
				this.EventsReader = eventsReader;
				this.Notifications = notifications;
				this.m_downloadDirectory = downloadDirectory;
				this.m_options = options;
				this.CreateDirectories();
				this.SetDownloadDelegates();
			}

			// Token: 0x06003184 RID: 12676 RVA: 0x000A7F9C File Offset: 0x000A619C
			protected QueryEventsFlowBase(IEventingRepository repository, IEtwEventsReader eventsReader, string downloadDirectory, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, string connectionString)
				: this(repository, eventsReader, downloadDirectory, options, notifications)
			{
				this.m_connectionString = connectionString;
			}

			// Token: 0x06003185 RID: 12677
			protected abstract IAsyncResult BeginDownloadProvidersManifests(string downloadDirectory, AsyncCallback callback, object context);

			// Token: 0x06003186 RID: 12678
			protected abstract IAsyncResult BeginDownloadProvidersManifests(string connectionString, string downloadDirectory, AsyncCallback callback, object context);

			// Token: 0x06003187 RID: 12679
			protected abstract IEnumerable<string> EndDownloadProvidersManifests(IAsyncResult asyncResult);

			// Token: 0x06003188 RID: 12680
			protected abstract IAsyncResult BeginDownloadEventFiles(string downloadDirectory, AsyncCallback callback, object context);

			// Token: 0x06003189 RID: 12681
			protected abstract IAsyncResult BeginDownloadEventFiles(string connectionString, string downloadDirectory, AsyncCallback callback, object context);

			// Token: 0x0600318A RID: 12682
			protected abstract IEnumerable<string> EndDownloadEventFiles(IAsyncResult asyncResult);

			// Token: 0x0600318B RID: 12683
			protected abstract IAsyncResult BeginReadEvents(EtwProvidersManifests manifests, IEnumerable<string> files, AsyncCallback callback, object context);

			// Token: 0x0600318C RID: 12684
			protected abstract IEnumerable<EtwEvent> EndReadEvents(IAsyncResult asyncResult);

			// Token: 0x0600318D RID: 12685 RVA: 0x000A7FB3 File Offset: 0x000A61B3
			protected override IEnumerable<IFlowStep> Run()
			{
				IEnumerable<string> manifests = null;
				yield return base.RunAsyncStep<string>("Get Providers Manifests", this.m_beginDownloadManifestsFunc, delegate(IAsyncResult ar)
				{
					manifests = this.EndDownloadProvidersManifests(ar);
				}, this.m_manifestsDirectory);
				EtwProvidersManifests providersManifests = null;
				yield return base.RunAsyncStep<IEnumerable<string>>("Parse providers manifests", new Sequencer.AsyncBeginFunction<IEnumerable<string>>(this.EventsReader.BeginReadProvidersManifests), delegate(IAsyncResult ar)
				{
					providersManifests = this.EventsReader.EndReadProvidersManifests(ar);
				}, manifests);
				EventsQuery.QueryEventsFlowBase.DeleteDownloadedFilesIfRequired(this.m_options, manifests.Concat(manifests.Select((string m) => Path.ChangeExtension(m, ".etlx"))).Materialize<string>());
				IEnumerable<string> eventFiles = null;
				yield return base.RunAsyncStep<string>("Download Event Files", this.m_beginDownloadEventsFunc, delegate(IAsyncResult ar)
				{
					eventFiles = this.EndDownloadEventFiles(ar);
				}, this.m_eventsDirectory);
				yield return base.RunAsyncStep<EtwProvidersManifests, IEnumerable<string>>("Read etl files", new Sequencer.AsyncBeginFunction<EtwProvidersManifests, IEnumerable<string>>(this.BeginReadEvents), delegate(IAsyncResult ar)
				{
					this.Events = this.EndReadEvents(ar);
				}, providersManifests, eventFiles);
				EventsQuery.QueryEventsFlowBase.DeleteDownloadedFilesIfRequired(this.m_options, eventFiles);
				yield break;
			}

			// Token: 0x0600318E RID: 12686 RVA: 0x000A7FC4 File Offset: 0x000A61C4
			protected EtwEventsReaderOptions GetEventsReaderOptions()
			{
				EtwEventsReaderOptions etwEventsReaderOptions = EtwEventsReaderOptions.None;
				if (this.Options.HasFlag(EventsQueryOptions.ReconstructMessage))
				{
					etwEventsReaderOptions |= EtwEventsReaderOptions.CreateEtwEventMessageField;
				}
				if (this.Options.HasFlag(EventsQueryOptions.SwallowDeleteFilesErrors))
				{
					etwEventsReaderOptions |= EtwEventsReaderOptions.SwallowDeleteFilesErrors;
				}
				return etwEventsReaderOptions;
			}

			// Token: 0x0600318F RID: 12687 RVA: 0x000A800C File Offset: 0x000A620C
			private void CreateDirectories()
			{
				if (!string.IsNullOrEmpty(this.m_downloadDirectory))
				{
					this.m_manifestsDirectory = Path.Combine(this.m_downloadDirectory, "ProvidersManifests");
					this.m_eventsDirectory = Path.Combine(this.m_downloadDirectory, "EventFiles");
					Directory.CreateDirectory(this.m_manifestsDirectory);
					Directory.CreateDirectory(this.m_eventsDirectory);
				}
			}

			// Token: 0x06003190 RID: 12688 RVA: 0x000A806C File Offset: 0x000A626C
			private static void DeleteDownloadedFilesIfRequired(EventsQueryOptions options, IEnumerable<string> files)
			{
				if (!options.HasFlag(EventsQueryOptions.RetainDownloadedFiles))
				{
					foreach (string text in files)
					{
						try
						{
							if (File.Exists(text))
							{
								File.Delete(text);
							}
						}
						catch (IOException ex)
						{
							TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Failed deleting file {0}. Exception: {1}", new object[] { text, ex.Message });
							if (!options.HasFlag(EventsQueryOptions.SwallowDeleteFilesErrors))
							{
								throw;
							}
						}
						catch (UnauthorizedAccessException ex2)
						{
							TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Failed deleting file {0}. Exception: {1}", new object[] { text, ex2.Message });
							if (!options.HasFlag(EventsQueryOptions.SwallowDeleteFilesErrors))
							{
								throw;
							}
						}
					}
				}
			}

			// Token: 0x06003191 RID: 12689 RVA: 0x000A8168 File Offset: 0x000A6368
			private void SetDownloadDelegates()
			{
				this.SetDownloadManifestsDelegate();
				this.SetDownloadEventFilesDelegate();
			}

			// Token: 0x06003192 RID: 12690 RVA: 0x000A8178 File Offset: 0x000A6378
			private void SetDownloadManifestsDelegate()
			{
				if (string.IsNullOrEmpty(this.m_connectionString))
				{
					if (this.Options.HasFlag(EventsQueryOptions.UseDeployTimeManifests))
					{
						this.m_beginDownloadManifestsFunc = (string dir, AsyncCallback callback, object context) => this.EventingRepository.BeginGetDeploymentProvidersManifestsFiles(dir, callback, context);
						return;
					}
					this.m_beginDownloadManifestsFunc = (string dir, AsyncCallback callback, object context) => this.BeginDownloadProvidersManifests(dir, callback, context);
					return;
				}
				else
				{
					if (this.Options.HasFlag(EventsQueryOptions.UseDeployTimeManifests))
					{
						this.m_beginDownloadManifestsFunc = (string dir, AsyncCallback callback, object context) => this.EventingRepository.BeginGetDeploymentProvidersManifestsFiles(this.m_connectionString, dir, callback, context);
						return;
					}
					this.m_beginDownloadManifestsFunc = (string dir, AsyncCallback callback, object context) => this.BeginDownloadProvidersManifests(this.m_connectionString, dir, callback, context);
					return;
				}
			}

			// Token: 0x06003193 RID: 12691 RVA: 0x000A820D File Offset: 0x000A640D
			private void SetDownloadEventFilesDelegate()
			{
				if (string.IsNullOrEmpty(this.m_connectionString))
				{
					this.m_beginDownloadEventsFunc = (string dir, AsyncCallback callback, object context) => this.BeginDownloadEventFiles(dir, callback, context);
					return;
				}
				this.m_beginDownloadEventsFunc = (string dir, AsyncCallback callback, object context) => this.BeginDownloadEventFiles(this.m_connectionString, dir, callback, context);
			}

			// Token: 0x040016DD RID: 5853
			private const string c_manifestsDirectory = "ProvidersManifests";

			// Token: 0x040016DE RID: 5854
			private const string c_eventsDirectory = "EventFiles";

			// Token: 0x040016DF RID: 5855
			private string m_downloadDirectory;

			// Token: 0x040016E0 RID: 5856
			private string m_connectionString;

			// Token: 0x040016E1 RID: 5857
			private EventsQueryOptions m_options;

			// Token: 0x040016E2 RID: 5858
			private Sequencer.AsyncBeginFunction<string> m_beginDownloadManifestsFunc;

			// Token: 0x040016E3 RID: 5859
			private Sequencer.AsyncBeginFunction<string> m_beginDownloadEventsFunc;

			// Token: 0x040016E4 RID: 5860
			private string m_manifestsDirectory;

			// Token: 0x040016E5 RID: 5861
			private string m_eventsDirectory;
		}

		// Token: 0x020007C1 RID: 1985
		private class QueryEventsByTimeRange : EventsQuery.QueryEventsFlowBase
		{
			// Token: 0x0600319A RID: 12698 RVA: 0x000A82A0 File Offset: 0x000A64A0
			public QueryEventsByTimeRange(IEventingRepository repository, IEtwEventsReader eventsReader, EventsQueryFilter filter, DateTime from, DateTime to, string downloadDirectory, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications)
				: this(repository, eventsReader, filter, from, to, downloadDirectory, options, notifications, string.Empty)
			{
			}

			// Token: 0x0600319B RID: 12699 RVA: 0x000A82C5 File Offset: 0x000A64C5
			public QueryEventsByTimeRange(IEventingRepository repository, IEtwEventsReader eventsReader, EventsQueryFilter filter, DateTime from, DateTime to, string downloadDirectory, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, string connectionString)
				: base(repository, eventsReader, downloadDirectory, options, notifications, connectionString)
			{
				this.m_filter = filter;
				this.m_from = from;
				this.m_to = to;
			}

			// Token: 0x0600319C RID: 12700 RVA: 0x000A82EE File Offset: 0x000A64EE
			protected override IAsyncResult BeginDownloadProvidersManifests(string downloadDirectory, AsyncCallback callback, object context)
			{
				return base.EventingRepository.BeginGetProvidersManifestsFiles(downloadDirectory, callback, context);
			}

			// Token: 0x0600319D RID: 12701 RVA: 0x000A82FE File Offset: 0x000A64FE
			protected override IAsyncResult BeginDownloadProvidersManifests(string connectionString, string downloadDirectory, AsyncCallback callback, object context)
			{
				return base.EventingRepository.BeginGetProvidersManifestsFiles(connectionString, downloadDirectory, callback, context);
			}

			// Token: 0x0600319E RID: 12702 RVA: 0x000A8310 File Offset: 0x000A6510
			protected override IEnumerable<string> EndDownloadProvidersManifests(IAsyncResult asyncResult)
			{
				if (base.Options.HasFlag(EventsQueryOptions.UseDeployTimeManifests))
				{
					return base.EventingRepository.EndGetDeploymentProvidersManifestsFiles(asyncResult);
				}
				return base.EventingRepository.EndGetProvidersManifestsFiles(asyncResult);
			}

			// Token: 0x0600319F RID: 12703 RVA: 0x000A8343 File Offset: 0x000A6543
			protected override IAsyncResult BeginDownloadEventFiles(string downloadDirectory, AsyncCallback callback, object context)
			{
				return base.EventingRepository.BeginGetEventFiles(downloadDirectory, this.GetEventsRepositoryOptions(), this.m_from, this.m_to, base.Notifications, callback, context);
			}

			// Token: 0x060031A0 RID: 12704 RVA: 0x000A836C File Offset: 0x000A656C
			protected override IAsyncResult BeginDownloadEventFiles(string connectionString, string downloadDirectory, AsyncCallback callback, object context)
			{
				return base.EventingRepository.BeginGetEventFiles(connectionString, downloadDirectory, this.GetEventsRepositoryOptions(), this.m_from, this.m_to, base.Notifications, callback, context);
			}

			// Token: 0x060031A1 RID: 12705 RVA: 0x000A83A1 File Offset: 0x000A65A1
			protected override IEnumerable<string> EndDownloadEventFiles(IAsyncResult asyncResult)
			{
				return base.EventingRepository.EndGetEventFiles(asyncResult);
			}

			// Token: 0x060031A2 RID: 12706 RVA: 0x000A83B0 File Offset: 0x000A65B0
			protected override IAsyncResult BeginReadEvents(EtwProvidersManifests manifests, IEnumerable<string> files, AsyncCallback callback, object context)
			{
				return base.EventsReader.BeginReadEtwEvents(manifests, files, this.m_from, this.m_to, this.m_filter, base.GetEventsReaderOptions(), callback, context);
			}

			// Token: 0x060031A3 RID: 12707 RVA: 0x000A83E5 File Offset: 0x000A65E5
			protected override IEnumerable<EtwEvent> EndReadEvents(IAsyncResult asyncResult)
			{
				return base.EventsReader.EndReadEtwEvents(asyncResult);
			}

			// Token: 0x060031A4 RID: 12708 RVA: 0x000A83F4 File Offset: 0x000A65F4
			private EventsRepositoryOptions GetEventsRepositoryOptions()
			{
				EventsRepositoryOptions eventsRepositoryOptions = EventsRepositoryOptions.DecompressOnEventFilesDownload;
				if (base.Options.HasFlag(EventsQueryOptions.SkipCorruptFiles))
				{
					eventsRepositoryOptions |= EventsRepositoryOptions.SkipCorruptFiles;
				}
				return eventsRepositoryOptions;
			}

			// Token: 0x040016EA RID: 5866
			private DateTime m_from;

			// Token: 0x040016EB RID: 5867
			private DateTime m_to;

			// Token: 0x040016EC RID: 5868
			private EventsQueryFilter m_filter;
		}

		// Token: 0x020007C2 RID: 1986
		private class QueryEventsByContinuation : EventsQuery.QueryEventsFlowBase
		{
			// Token: 0x17000773 RID: 1907
			// (get) Token: 0x060031A5 RID: 12709 RVA: 0x000A8421 File Offset: 0x000A6621
			// (set) Token: 0x060031A6 RID: 12710 RVA: 0x000A8429 File Offset: 0x000A6629
			public IEventingRepositoryContinuation ContinuationStatus { get; private set; }

			// Token: 0x060031A7 RID: 12711 RVA: 0x000A8434 File Offset: 0x000A6634
			public QueryEventsByContinuation(IEventingRepository repository, IEtwEventsReader eventsReader, DateTime from, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications)
				: this(repository, eventsReader, from, downloadDirectory, maxSizeToDownloadInMb, options, notifications, string.Empty)
			{
			}

			// Token: 0x060031A8 RID: 12712 RVA: 0x000A8457 File Offset: 0x000A6657
			public QueryEventsByContinuation(IEventingRepository repository, IEtwEventsReader eventsReader, DateTime from, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, string connectionString)
				: base(repository, eventsReader, downloadDirectory, options, notifications, connectionString)
			{
				this.m_from = from;
				this.m_maxSizeToDownloadInMb = maxSizeToDownloadInMb;
			}

			// Token: 0x060031A9 RID: 12713 RVA: 0x000A8478 File Offset: 0x000A6678
			public QueryEventsByContinuation(IEventingRepository repository, IEtwEventsReader eventsReader, IEventingRepositoryContinuation continuation, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications)
				: this(repository, eventsReader, continuation, downloadDirectory, maxSizeToDownloadInMb, options, notifications, string.Empty)
			{
			}

			// Token: 0x060031AA RID: 12714 RVA: 0x000A849B File Offset: 0x000A669B
			public QueryEventsByContinuation(IEventingRepository repository, IEtwEventsReader eventsReader, IEventingRepositoryContinuation continuation, string downloadDirectory, int maxSizeToDownloadInMb, EventsQueryOptions options, IEventingRepositoryDownloadNotifications notifications, string connectionString)
				: base(repository, eventsReader, downloadDirectory, options, notifications, connectionString)
			{
				this.m_continuation = continuation;
				this.m_maxSizeToDownloadInMb = maxSizeToDownloadInMb;
			}

			// Token: 0x060031AB RID: 12715 RVA: 0x000A84BC File Offset: 0x000A66BC
			protected override IAsyncResult BeginDownloadProvidersManifests(string downloadDirectory, AsyncCallback callback, object context)
			{
				if (this.m_continuation != null)
				{
					return base.EventingRepository.BeginGetProvidersManifestsFiles(downloadDirectory, this.m_continuation, callback, context);
				}
				return base.EventingRepository.BeginGetProvidersManifestsFiles(downloadDirectory, callback, context);
			}

			// Token: 0x060031AC RID: 12716 RVA: 0x000A84E9 File Offset: 0x000A66E9
			protected override IAsyncResult BeginDownloadProvidersManifests(string connectionString, string downloadDirectory, AsyncCallback callback, object context)
			{
				if (this.m_continuation != null)
				{
					return base.EventingRepository.BeginGetProvidersManifestsFiles(connectionString, downloadDirectory, this.m_continuation, callback, context);
				}
				return base.EventingRepository.BeginGetProvidersManifestsFiles(connectionString, downloadDirectory, callback, context);
			}

			// Token: 0x060031AD RID: 12717 RVA: 0x000A8310 File Offset: 0x000A6510
			protected override IEnumerable<string> EndDownloadProvidersManifests(IAsyncResult asyncResult)
			{
				if (base.Options.HasFlag(EventsQueryOptions.UseDeployTimeManifests))
				{
					return base.EventingRepository.EndGetDeploymentProvidersManifestsFiles(asyncResult);
				}
				return base.EventingRepository.EndGetProvidersManifestsFiles(asyncResult);
			}

			// Token: 0x060031AE RID: 12718 RVA: 0x000A851C File Offset: 0x000A671C
			protected override IAsyncResult BeginDownloadEventFiles(string downloadDirectory, AsyncCallback callback, object context)
			{
				EventsRepositoryOptions eventsRepositoryOptions = this.GetEventsRepositoryOptions();
				if (this.m_continuation == null)
				{
					return base.EventingRepository.BeginGetEventFilesContinued(downloadDirectory, eventsRepositoryOptions, this.m_from, this.m_maxSizeToDownloadInMb, base.Notifications, callback, context);
				}
				return base.EventingRepository.BeginGetEventFilesContinued(downloadDirectory, eventsRepositoryOptions, this.m_continuation, this.m_maxSizeToDownloadInMb, base.Notifications, callback, context);
			}

			// Token: 0x060031AF RID: 12719 RVA: 0x000A857C File Offset: 0x000A677C
			protected override IAsyncResult BeginDownloadEventFiles(string connectionString, string downloadDirectory, AsyncCallback callback, object context)
			{
				EventsRepositoryOptions eventsRepositoryOptions = this.GetEventsRepositoryOptions();
				if (this.m_continuation == null)
				{
					return base.EventingRepository.BeginGetEventFilesContinued(connectionString, downloadDirectory, eventsRepositoryOptions, this.m_from, this.m_maxSizeToDownloadInMb, base.Notifications, callback, context);
				}
				return base.EventingRepository.BeginGetEventFilesContinued(connectionString, downloadDirectory, eventsRepositoryOptions, this.m_continuation, this.m_maxSizeToDownloadInMb, base.Notifications, callback, context);
			}

			// Token: 0x060031B0 RID: 12720 RVA: 0x000A85E0 File Offset: 0x000A67E0
			protected override IEnumerable<string> EndDownloadEventFiles(IAsyncResult asyncResult)
			{
				EventFilesDownloadResult eventFilesDownloadResult = base.EventingRepository.EndGetEventFilesContinued(asyncResult);
				this.ContinuationStatus = eventFilesDownloadResult.Continuation;
				return eventFilesDownloadResult.Paths;
			}

			// Token: 0x060031B1 RID: 12721 RVA: 0x000A860C File Offset: 0x000A680C
			protected override IAsyncResult BeginReadEvents(EtwProvidersManifests manifests, IEnumerable<string> files, AsyncCallback callback, object context)
			{
				if (this.m_continuation == null)
				{
					return base.EventsReader.BeginReadEtwEvents(manifests, files, this.m_from, DateTime.MaxValue, EventsQueryFilter.Empty, base.GetEventsReaderOptions(), callback, context);
				}
				return base.EventsReader.BeginReadEtwEvents(manifests, files, EventsQueryFilter.Empty, base.GetEventsReaderOptions(), callback, context);
			}

			// Token: 0x060031B2 RID: 12722 RVA: 0x000A83E5 File Offset: 0x000A65E5
			protected override IEnumerable<EtwEvent> EndReadEvents(IAsyncResult asyncResult)
			{
				return base.EventsReader.EndReadEtwEvents(asyncResult);
			}

			// Token: 0x060031B3 RID: 12723 RVA: 0x000A8664 File Offset: 0x000A6864
			private EventsRepositoryOptions GetEventsRepositoryOptions()
			{
				EventsRepositoryOptions eventsRepositoryOptions = EventsRepositoryOptions.DecompressOnEventFilesDownload;
				if (base.Options.HasFlag(EventsQueryOptions.SkipCorruptFiles))
				{
					eventsRepositoryOptions |= EventsRepositoryOptions.SkipCorruptFiles;
				}
				return eventsRepositoryOptions;
			}

			// Token: 0x040016ED RID: 5869
			private DateTime m_from;

			// Token: 0x040016EE RID: 5870
			private IEventingRepositoryContinuation m_continuation;

			// Token: 0x040016EF RID: 5871
			private int m_maxSizeToDownloadInMb;
		}
	}
}
