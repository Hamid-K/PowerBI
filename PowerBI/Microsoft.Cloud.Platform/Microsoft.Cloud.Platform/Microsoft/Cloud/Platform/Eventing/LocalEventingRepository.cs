using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x020003A5 RID: 933
	[BlockServiceProvider(typeof(IEventingRepository))]
	public class LocalEventingRepository : Block, IEventingRepository
	{
		// Token: 0x06001C99 RID: 7321 RVA: 0x0006C368 File Offset: 0x0006A568
		public LocalEventingRepository()
			: base(typeof(LocalEventingRepository).Name)
		{
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x0006C37F File Offset: 0x0006A57F
		public IAsyncResult BeginGetEventFiles(string storageConnectionString, string downloadDirectory, EventsRepositoryOptions options, DateTime from, DateTime to, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			return this.BeginGetEventFiles(downloadDirectory, options, from, to, notifications, callback, context);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0006C394 File Offset: 0x0006A594
		public IAsyncResult BeginGetEventFiles(string downloadDirectory, EventsRepositoryOptions options, DateTime from, DateTime to, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			IEnumerable<string> enumerable = LocalEventingRepository.GetAllFilesInDirectory(this.m_eventingDirectoriesManager.EventingFilesTargetDirectory, "*.etl").Where(delegate(string file)
			{
				FileInfo fileInfo = new FileInfo(file);
				return fileInfo.CreationTimeUtc <= to && fileInfo.LastWriteTimeUtc >= from;
			});
			LocalEventingRepository.NotifyDownloadedFiles(notifications, enumerable);
			return new CompletedAsyncResult<IEnumerable<string>>(callback, context, enumerable);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x0006C3EF File Offset: 0x0006A5EF
		public IEnumerable<string> EndGetEventFiles(IAsyncResult asyncResult)
		{
			return ((CompletedAsyncResult<IEnumerable<string>>)asyncResult).End();
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x0006C3FC File Offset: 0x0006A5FC
		public IAsyncResult BeginGetEventFilesContinued(string storageConnectionString, string downloadDirectory, EventsRepositoryOptions options, IEventingRepositoryContinuation continuation, int maxSizeToDownloadInMb, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			return this.BeginGetEventFilesContinued(downloadDirectory, options, continuation, maxSizeToDownloadInMb, notifications, callback, context);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x0006C410 File Offset: 0x0006A610
		public IAsyncResult BeginGetEventFilesContinued(string downloadDirectory, EventsRepositoryOptions options, IEventingRepositoryContinuation continuation, int maxSizeToDownloadInMb, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			LocalEventingRepository.LocalEventingRepositoryContinuation localEventingRepositoryContinuation = new LocalEventingRepository.LocalEventingRepositoryContinuation(Enumerable.Empty<string>());
			if (continuation != null)
			{
				ExtendedDiagnostics.EnsureArgumentIsOfType(continuation, typeof(LocalEventingRepository.LocalEventingRepositoryContinuation), "continuation");
				localEventingRepositoryContinuation = (LocalEventingRepository.LocalEventingRepositoryContinuation)continuation;
			}
			IEnumerable<string> allFilesInDirectory = LocalEventingRepository.GetAllFilesInDirectory(this.m_eventingDirectoriesManager.EventingFilesTargetDirectory, "*.etl");
			IEnumerable<string> enumerable = allFilesInDirectory.Except(localEventingRepositoryContinuation.Paths).Materialize<string>();
			LocalEventingRepository.NotifyDownloadedFiles(notifications, enumerable);
			return new CompletedAsyncResult<EventFilesDownloadResult>(callback, context, new EventFilesDownloadResult(enumerable, new LocalEventingRepository.LocalEventingRepositoryContinuation(allFilesInDirectory)));
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0006C48C File Offset: 0x0006A68C
		public IAsyncResult BeginGetEventFilesContinued(string storageConnectionString, string downloadDirectory, EventsRepositoryOptions options, DateTime from, int maxSizeToDownloadInMb, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			return this.BeginGetEventFilesContinued(downloadDirectory, options, from, maxSizeToDownloadInMb, notifications, callback, context);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0006C4A0 File Offset: 0x0006A6A0
		public IAsyncResult BeginGetEventFilesContinued(string downloadDirectory, EventsRepositoryOptions options, DateTime from, int maxSizeToDownloadInMb, IEventingRepositoryDownloadNotifications notifications, AsyncCallback callback, object context)
		{
			IEnumerable<string> enumerable = from file in LocalEventingRepository.GetAllFilesInDirectory(this.m_eventingDirectoriesManager.EventingFilesTargetDirectory, "*.etl")
				where new FileInfo(file).LastWriteTimeUtc >= @from
				select file;
			LocalEventingRepository.NotifyDownloadedFiles(notifications, enumerable);
			return new CompletedAsyncResult<EventFilesDownloadResult>(callback, context, new EventFilesDownloadResult(enumerable, new LocalEventingRepository.LocalEventingRepositoryContinuation(enumerable)));
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0006C4FE File Offset: 0x0006A6FE
		public EventFilesDownloadResult EndGetEventFilesContinued(IAsyncResult result)
		{
			return CompletedAsyncResult<EventFilesDownloadResult>.End(result);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x0006C506 File Offset: 0x0006A706
		public IAsyncResult BeginGetProvidersManifestsFiles(string storageConnectionString, string downloadDirectory, AsyncCallback callback, object context)
		{
			return this.BeginGetProvidersManifestsFiles(downloadDirectory, callback, context);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x0006C512 File Offset: 0x0006A712
		public IAsyncResult BeginGetProvidersManifestsFiles(string downloadDirectory, AsyncCallback callback, object context)
		{
			return new CompletedAsyncResult<IEnumerable<string>>(callback, context, LocalEventingRepository.GetAllFilesInDirectory(this.m_eventingDirectoriesManager.ProvidersManifestDirectory, "*.etl"));
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0006C530 File Offset: 0x0006A730
		public IAsyncResult BeginGetProvidersManifestsFiles(string downloadDirectory, IEventingRepositoryContinuation continuation, AsyncCallback callback, object context)
		{
			return this.BeginGetProvidersManifestsFiles(downloadDirectory, callback, context);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x0006C53C File Offset: 0x0006A73C
		public IAsyncResult BeginGetProvidersManifestsFiles(string connecntionString, string downloadDirectory, IEventingRepositoryContinuation continuation, AsyncCallback callback, object context)
		{
			return this.BeginGetProvidersManifestsFiles(connecntionString, downloadDirectory, callback, context);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x0006C54A File Offset: 0x0006A74A
		public IEnumerable<string> EndGetProvidersManifestsFiles(IAsyncResult result)
		{
			return CompletedAsyncResult<IEnumerable<string>>.End(result);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x0006C552 File Offset: 0x0006A752
		public IAsyncResult BeginGetDeploymentProvidersManifestsFiles(string storageConnectionString, string downloadDirectory, AsyncCallback callback, object context)
		{
			return this.BeginGetDeploymentProvidersManifestsFiles(downloadDirectory, callback, context);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0006C55E File Offset: 0x0006A75E
		public IAsyncResult BeginGetDeploymentProvidersManifestsFiles(string downloadDirectory, AsyncCallback callback, object context)
		{
			return new CompletedAsyncResult<IEnumerable<string>>(callback, context, LocalEventingRepository.GetAllFilesInDirectory(this.m_eventingDirectoriesManager.ProvidersManifestDirectory, "*.xml"));
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x0006C54A File Offset: 0x0006A74A
		public IEnumerable<string> EndGetDeploymentProvidersManifestsFiles(IAsyncResult result)
		{
			return CompletedAsyncResult<IEnumerable<string>>.End(result);
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00014B8A File Offset: 0x00012D8A
		public IEventingRepositoryContinuation DeserializeContinuation(string serializedContinuation)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0006C57C File Offset: 0x0006A77C
		private static IEnumerable<string> GetAllFilesInDirectory(string directoryName, string searchPattern)
		{
			return ExtendedDirectory.GetFilesExact(directoryName, searchPattern, SearchOption.TopDirectoryOnly);
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0006C588 File Offset: 0x0006A788
		private static void NotifyDownloadedFiles(IEventingRepositoryDownloadNotifications notifications, IEnumerable<string> filesToProcess)
		{
			if (notifications != null)
			{
				int num = filesToProcess.Count<string>();
				int num2 = 0;
				foreach (string text in filesToProcess)
				{
					num2++;
					notifications.OnEventFileDownloadCompleted("Local", text, num2, num);
				}
			}
		}

		// Token: 0x040009A3 RID: 2467
		[BlockServiceDependency]
		private IEventingDirectoriesManager m_eventingDirectoriesManager;

		// Token: 0x040009A4 RID: 2468
		private const string c_xmlFilesSearchPattern = "*.xml";

		// Token: 0x040009A5 RID: 2469
		private const string c_etlFilesSearchPattern = "*.etl";

		// Token: 0x040009A6 RID: 2470
		private const string c_localRoleInstanceName = "Local";

		// Token: 0x020007C3 RID: 1987
		private class LocalEventingRepositoryContinuation : IEventingRepositoryContinuation
		{
			// Token: 0x17000774 RID: 1908
			// (get) Token: 0x060031B4 RID: 12724 RVA: 0x000A8691 File Offset: 0x000A6891
			// (set) Token: 0x060031B5 RID: 12725 RVA: 0x000A8699 File Offset: 0x000A6899
			public IEnumerable<string> Paths { get; private set; }

			// Token: 0x060031B6 RID: 12726 RVA: 0x000A86A2 File Offset: 0x000A68A2
			public LocalEventingRepositoryContinuation(IEnumerable<string> paths)
			{
				this.Paths = paths.Materialize<string>();
			}

			// Token: 0x060031B7 RID: 12727 RVA: 0x00014B8A File Offset: 0x00012D8A
			public string ToSerializedString()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060031B8 RID: 12728 RVA: 0x00014B8A File Offset: 0x00012D8A
			public IEnumerable<string> GetAllRoleInstances()
			{
				throw new NotImplementedException();
			}

			// Token: 0x060031B9 RID: 12729 RVA: 0x00014B8A File Offset: 0x00012D8A
			public IEnumerable<ElementId> GetAllElementInstances(string roleInstance)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060031BA RID: 12730 RVA: 0x00014B8A File Offset: 0x00012D8A
			public DateTime GetLastEventTimeForElementInstance(string roleInstance, ElementId instance)
			{
				throw new NotImplementedException();
			}
		}
	}
}
