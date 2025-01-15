using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000151 RID: 337
	public sealed class ConcurrentSingleResourceCache<TVersion, TResource> where TVersion : class, IComparable<TVersion> where TResource : class
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x0001F222 File Offset: 0x0001D422
		public ConcurrentSingleResourceCache(ConcurrentSingleResourceCache<TVersion, TResource>.ProvideResourceCallback provideResourceCallback, ConcurrentSingleResourceCache<TVersion, TResource>.ResourceReleasedCallback resourceReleasedCallback, TimeSpan resourceUpdateTimeout)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ConcurrentSingleResourceCache<TVersion, TResource>.ProvideResourceCallback>(provideResourceCallback, "provideResourceCallback");
			ExtendedDiagnostics.EnsureArgumentNotNull<ConcurrentSingleResourceCache<TVersion, TResource>.ResourceReleasedCallback>(resourceReleasedCallback, "resourceReleasedCallback");
			this.m_resourceUpdateTimeout = resourceUpdateTimeout;
			this.m_provideResourceCallback = provideResourceCallback;
			this.m_resourceReleasedCallback = resourceReleasedCallback;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001F261 File Offset: 0x0001D461
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TResource GetCurrentResource()
		{
			return this.m_currentResource;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001F269 File Offset: 0x0001D469
		[MethodImpl(MethodImplOptions.NoInlining)]
		public TVersion GetCurrentVersion()
		{
			return this.m_currentVersion;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001F274 File Offset: 0x0001D474
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool HasVersion(TVersion minimumVersion)
		{
			TVersion currentVersion = this.m_currentVersion;
			return currentVersion != null && currentVersion.CompareTo(minimumVersion) >= 0;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001F2A4 File Offset: 0x0001D4A4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public bool HasHigherVersion(TVersion minimumVersion)
		{
			TVersion currentVersion = this.m_currentVersion;
			return currentVersion != null && currentVersion.CompareTo(minimumVersion) > 0;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001F2D4 File Offset: 0x0001D4D4
		public async Task<EnsureVersionResult> EnsureVersion(TVersion minimumVersion, bool forceOnEqual)
		{
			Func<TVersion, bool> predicate = new Func<TVersion, bool>(this.HasVersion);
			if (forceOnEqual)
			{
				predicate = new Func<TVersion, bool>(this.HasHigherVersion);
			}
			if (!predicate(minimumVersion))
			{
				using (CancellationTokenSource cts = new CancellationTokenSource(this.m_resourceUpdateTimeout))
				{
					TResource tresource = await this.m_provideResourceCallback(minimumVersion, cts.Token);
					cts.Token.ThrowIfCancellationRequested();
					if (!predicate(minimumVersion))
					{
						if (!this.m_swapLock.TryEnterWriteLock(this.m_resourceUpdateTimeout))
						{
							throw new MutexTimeoutException(this.m_resourceUpdateTimeout.TotalSeconds);
						}
						try
						{
							if (!predicate(minimumVersion))
							{
								TVersion currentVersion = this.m_currentVersion;
								TResource currentResource = this.m_currentResource;
								this.m_currentResource = tresource;
								this.m_currentVersion = minimumVersion;
								if (currentResource != null)
								{
									this.m_resourceReleasedCallback(currentResource);
								}
								if (currentVersion != null)
								{
									int num = this.m_currentVersion.CompareTo(currentVersion);
									if (num > 0)
									{
										return EnsureVersionResult.UpdatedWithNewerVersion;
									}
									if (num == 0)
									{
										return EnsureVersionResult.UpdatedWithSameVersion;
									}
								}
							}
						}
						finally
						{
							this.m_swapLock.ExitWriteLock();
						}
					}
				}
				CancellationTokenSource cts = null;
			}
			return EnsureVersionResult.NotUpdated;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001F32C File Offset: 0x0001D52C
		public TResource ForceResource(TVersion requiredVersion, ConcurrentSingleResourceCache<TVersion, TResource>.ForcedResourceProviderCallback forcedResourceCallback)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<TVersion>(requiredVersion, "requiredVersion");
			ExtendedDiagnostics.EnsureArgumentNotNull<ConcurrentSingleResourceCache<TVersion, TResource>.ForcedResourceProviderCallback>(forcedResourceCallback, "forcedResourceCallback");
			ExtendedDiagnostics.EnsureArgumentNotNull<TVersion>(this.m_currentVersion, "Forcing a resource can only happen when one already exists");
			ExtendedDiagnostics.EnsureArgumentNotNull<TResource>(this.m_currentResource, "Forcing a resource can only happen when one already exists");
			TResource tresource = default(TResource);
			if (requiredVersion.CompareTo(this.m_currentVersion) == 0)
			{
				if (!this.m_swapLock.TryEnterWriteLock(this.m_resourceUpdateTimeout))
				{
					throw new MutexTimeoutException(this.m_resourceUpdateTimeout.TotalSeconds);
				}
				try
				{
					if (requiredVersion.CompareTo(this.m_currentVersion) == 0)
					{
						tresource = this.m_currentResource;
						this.m_currentResource = forcedResourceCallback(tresource);
					}
				}
				finally
				{
					this.m_swapLock.ExitWriteLock();
				}
			}
			return tresource;
		}

		// Token: 0x0400035C RID: 860
		private readonly ReaderWriterLockSlim m_swapLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

		// Token: 0x0400035D RID: 861
		private readonly TimeSpan m_resourceUpdateTimeout;

		// Token: 0x0400035E RID: 862
		private readonly ConcurrentSingleResourceCache<TVersion, TResource>.ProvideResourceCallback m_provideResourceCallback;

		// Token: 0x0400035F RID: 863
		private readonly ConcurrentSingleResourceCache<TVersion, TResource>.ResourceReleasedCallback m_resourceReleasedCallback;

		// Token: 0x04000360 RID: 864
		private TVersion m_currentVersion;

		// Token: 0x04000361 RID: 865
		private TResource m_currentResource;

		// Token: 0x02000629 RID: 1577
		// (Invoke) Token: 0x06002CA4 RID: 11428
		public delegate Task<TResource> ProvideResourceCallback(TVersion requestedVersion, CancellationToken cancellationToken);

		// Token: 0x0200062A RID: 1578
		// (Invoke) Token: 0x06002CA8 RID: 11432
		public delegate void ResourceReleasedCallback(TResource releasedResource);

		// Token: 0x0200062B RID: 1579
		// (Invoke) Token: 0x06002CAC RID: 11436
		public delegate TResource ForcedResourceProviderCallback(TResource currentResource);
	}
}
