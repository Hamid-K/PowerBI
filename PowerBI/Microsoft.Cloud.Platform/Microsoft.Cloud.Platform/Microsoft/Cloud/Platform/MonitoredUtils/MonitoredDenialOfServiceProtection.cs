using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000142 RID: 322
	public sealed class MonitoredDenialOfServiceProtection<TKey> : IDenialOfServiceProtection<TKey>, IIdentifiable
	{
		// Token: 0x06000862 RID: 2146 RVA: 0x0001C470 File Offset: 0x0001A670
		public MonitoredDenialOfServiceProtection([NotNull] string name, TimeSpan probationDuration, int maxProbationKeys, int maxViolationEventsPerProbation, TimeSpan blockingDuration, int maxBlockedKeys, [NotNull] IEnumerable<TKey> alwaysBlockedKeys, [NotNull] IEnumerable<TKey> neverBlockedKeys, [NotNull] IEventsKitFactory eventsKitFactory)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgumentIsBetween(probationDuration, MonitoredDenialOfServiceProtection<TKey>.s_minProbationDuration, MonitoredDenialOfServiceProtection<TKey>.s_maxProbationDuration, "probationDuration");
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxProbationKeys, "maxProbationKeys");
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxViolationEventsPerProbation, "maxViolationEventsPerProbation");
			ExtendedDiagnostics.EnsureArgumentIsBetween(blockingDuration, MonitoredDenialOfServiceProtection<TKey>.s_minBlockingDuration, MonitoredDenialOfServiceProtection<TKey>.s_maxBlockingDuration, "blockingDuration");
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxBlockedKeys, "maxBlockedKeys");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<TKey>>(alwaysBlockedKeys, "alwaysBlockedKeys");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<TKey>>(neverBlockedKeys, "neverBlockedKeys");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEventsKitFactory>(eventsKitFactory, "eventsKitFactory");
			this.Name = name;
			this.m_probationDuration = probationDuration;
			this.m_maxViolationEventsPerProbation = maxViolationEventsPerProbation;
			this.m_blockingDuration = blockingDuration;
			this.m_alwaysBlockedKeys = alwaysBlockedKeys.ToHashSet<TKey>();
			this.m_neverBlockedKeys = neverBlockedKeys.ToHashSet<TKey>();
			this.m_eventsKit = eventsKitFactory.CreateEventsKit<IMonitoredDenialOfServiceProtectionEventsKit>(this.Name, PerformanceCounterPrefixSetting.ElementName);
			this.m_violationEventsMidThreshold = this.m_maxViolationEventsPerProbation / 2;
			this.m_probationKeys = new QuickAccessPool<TKey, EventRateLimiter>(maxProbationKeys, PoolPolicy.PreferMostRecentlyUsed, delegate(TKey key, EventRateLimiter erl)
			{
				this.m_eventsKit.NotifyKeyRemovedFromBlocking(this.GetScrubbedKeyIfNeeded(key), this.m_blockedKeys.Count);
			});
			this.m_blockedKeys = new QuickAccessPool<TKey, DateTime>(maxBlockedKeys, PoolPolicy.PreferMostRecentlyUsed, delegate(TKey key, DateTime erl)
			{
				this.m_eventsKit.NotifyKeyRemovedFromBlocking(this.GetScrubbedKeyIfNeeded(key), this.m_blockedKeys.Count);
			});
			this.Tracer.TraceInformation("DoSP of type {0} was configured with <ProbationDuration={1} MaxProbationKeys={2} m_maxViolationEventsPerProbation={3} m_blockingDuration={4} MaxBlockedKeys={5}>", new object[]
			{
				typeof(TKey),
				this.m_probationDuration,
				maxProbationKeys,
				this.m_maxViolationEventsPerProbation,
				this.m_blockingDuration,
				maxBlockedKeys
			});
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0001C5F8 File Offset: 0x0001A7F8
		public int ProbationKeysCount
		{
			get
			{
				object @lock = this.m_lock;
				int count;
				lock (@lock)
				{
					count = this.m_probationKeys.Count;
				}
				return count;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001C640 File Offset: 0x0001A840
		public int BlockedKeysCount
		{
			get
			{
				object @lock = this.m_lock;
				int count;
				lock (@lock)
				{
					count = this.m_blockedKeys.Count;
				}
				return count;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001C688 File Offset: 0x0001A888
		private DenialOfServiceProtectionTrace Tracer
		{
			get
			{
				return TraceSourceBase<DenialOfServiceProtectionTrace>.Tracer;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001C690 File Offset: 0x0001A890
		public BlockingStatus<TKey> QueryBlockingStatus(TKey key, DateTime when)
		{
			this.Tracer.TraceVerbose("Querying key blocking status <Key={0} When={1}>", new object[] { key, when });
			object @lock = this.m_lock;
			BlockingStatus<TKey> blockingStatus;
			lock (@lock)
			{
				DateTime dateTime;
				if (this.m_alwaysBlockedKeys.Contains(key))
				{
					this.Tracer.TraceVerbose("Key is part of always blocked keys list <Key={0}>", new object[] { key });
					blockingStatus = new BlockingStatus<TKey>(key, DateTime.MaxValue);
				}
				else if (this.m_neverBlockedKeys.Contains(key))
				{
					this.Tracer.TraceVerbose("Key is part of never blocked keys list <Key={0}>", new object[] { key });
					blockingStatus = new BlockingStatus<TKey>(key);
				}
				else if (!this.m_blockedKeys.TryCheckOut(key, out dateTime))
				{
					this.Tracer.TraceVerbose("Key is not blocked <Key={0}>", new object[] { key });
					blockingStatus = new BlockingStatus<TKey>(key);
				}
				else
				{
					this.Tracer.TraceVerbose("Existing key blocking was found <Key={0} BlockingEndTime={1}>", new object[] { key, dateTime });
					string scrubbedKeyIfNeeded = this.GetScrubbedKeyIfNeeded(key);
					if (when > dateTime)
					{
						this.m_eventsKit.NotifyKeyRemovedFromBlocking(scrubbedKeyIfNeeded, this.m_blockedKeys.Count);
						blockingStatus = new BlockingStatus<TKey>(key);
					}
					else
					{
						this.m_blockedKeys.CheckIn(key, dateTime);
						this.m_eventsKit.NotifyBlocking(scrubbedKeyIfNeeded, dateTime);
						blockingStatus = new BlockingStatus<TKey>(key, dateTime);
					}
				}
			}
			return blockingStatus;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001C834 File Offset: 0x0001AA34
		public BlockingStatus<TKey> ReportAndCheckRequestBlockingStatus(TKey key, DateTime when, string clientIdentifier = null)
		{
			this.Tracer.TraceVerbose("Reporting event <Key={0} When={1}>", new object[] { key, when });
			object @lock = this.m_lock;
			BlockingStatus<TKey> blockingStatus;
			lock (@lock)
			{
				if (this.m_neverBlockedKeys.Contains(key))
				{
					this.Tracer.TraceVerbose("Key is part of never blocked keys list <Key={0}>", new object[] { key });
					blockingStatus = new BlockingStatus<TKey>(key);
				}
				else if (this.m_alwaysBlockedKeys.Contains(key))
				{
					this.Tracer.TraceError("Key is part of always blocked keys list. This considered to be an error situation since always blocked key should be blocked before reaching the stage of reporting violation <Key={0}>", new object[] { key });
					blockingStatus = new BlockingStatus<TKey>(key, DateTime.MaxValue);
				}
				else
				{
					string scrubbedKeyIfNeeded = this.GetScrubbedKeyIfNeeded(key);
					DateTime dateTime;
					if (this.m_blockedKeys.TryCheckOut(key, out dateTime))
					{
						if (!(when > dateTime))
						{
							this.m_blockedKeys.CheckIn(key, dateTime);
							return new BlockingStatus<TKey>(key, dateTime);
						}
						this.Tracer.TraceInformation("Existing key blocking has been completed <Key={0} BlockingEndTime={1}>", new object[] { key, dateTime });
						this.m_eventsKit.NotifyKeyRemovedFromBlocking(scrubbedKeyIfNeeded, this.m_blockedKeys.Count);
					}
					EventRateLimiter eventRateLimiter;
					if (!this.m_probationKeys.TryCheckOut(key, out eventRateLimiter))
					{
						eventRateLimiter = new EventRateLimiter(this.m_probationDuration, this.m_maxViolationEventsPerProbation);
						this.Tracer.TraceVerbose("Key has not been reported for previous violation events <Key={0}>", new object[] { key });
					}
					else
					{
						this.Tracer.TraceVerbose("Key has been reported for previous violation events <Key={0}>", new object[] { key });
					}
					if (eventRateLimiter.ShouldProcessEvent(when))
					{
						eventRateLimiter.UpdateTimeslotState(when);
						this.m_probationKeys.CheckIn(key, eventRateLimiter);
						this.Tracer.TraceInformation("Key has not exceeded max number of violation events in current probation time slot <Key={0} CurrentNumberOfViolationEvents={1} MaxNumberOfViolationEvents={2}> - key will not be blocked", new object[] { key, eventRateLimiter.TimeslotEventCount, eventRateLimiter.MaxSlotEvents });
						if (eventRateLimiter.TimeslotEventCount > this.m_violationEventsMidThreshold)
						{
							this.Tracer.TraceWarning("Key has exceeded 50% of max number of violation events in current probation time slot " + this.GetTraceInfo(key, eventRateLimiter, clientIdentifier));
						}
						blockingStatus = new BlockingStatus<TKey>(key);
					}
					else
					{
						DateTime dateTime2 = when + this.m_blockingDuration;
						this.m_blockedKeys.CheckIn(key, dateTime2);
						this.m_eventsKit.NotifyKeyAddedToBlocking(scrubbedKeyIfNeeded, this.m_maxViolationEventsPerProbation, this.m_probationDuration.ToString("c", CultureInfo.InvariantCulture), dateTime2, this.m_blockedKeys.Count);
						blockingStatus = new BlockingStatus<TKey>(key, dateTime2);
					}
				}
			}
			return blockingStatus;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001CAF4 File Offset: 0x0001ACF4
		public void Block(TKey key)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_neverBlockedKeys.Contains(key))
				{
					this.Tracer.TraceVerbose(string.Format("Block called with a key that is part of never blocked keys list <Key={0}>", key));
				}
				else if (!this.m_blockedKeys.Contains(key))
				{
					string scrubbedKeyIfNeeded = this.GetScrubbedKeyIfNeeded(key);
					DateTime dateTime = ExtendedDateTime.UtcNow + this.m_blockingDuration;
					this.m_blockedKeys.CheckIn(key, dateTime);
					this.m_eventsKit.NotifyKeyAddedToBlocking(scrubbedKeyIfNeeded, 1, this.m_probationDuration.ToString("c", CultureInfo.InvariantCulture), dateTime, this.m_blockedKeys.Count);
				}
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		private string GetTraceInfo(TKey key, EventRateLimiter eventRateLimiter)
		{
			return this.GetTraceInfo(key, eventRateLimiter, null);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001CBCC File Offset: 0x0001ADCC
		private string GetTraceInfo(TKey key, EventRateLimiter eventRateLimiter, string clientIdentifier)
		{
			if (clientIdentifier == null)
			{
				return "<Key={0} CurrentNumberOfViolationEvents={1} MaxNumberOfViolationEvents={2}>".FormatWithInvariantCulture(new object[] { key, eventRateLimiter.TimeslotEventCount, eventRateLimiter.MaxSlotEvents });
			}
			return "<Key={0} CurrentNumberOfViolationEvents={1} MaxNumberOfViolationEvents={2} ClientIP={3}>".FormatWithInvariantCulture(new object[]
			{
				key,
				eventRateLimiter.TimeslotEventCount,
				eventRateLimiter.MaxSlotEvents,
				clientIdentifier.MarkAsInternal()
			});
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001CC50 File Offset: 0x0001AE50
		private string GetScrubbedKeyIfNeeded(TKey key)
		{
			if (typeof(TKey) == typeof(IPAddress))
			{
				return new ScrubbedIPEndPoint(key as IPAddress).ToPrivateString();
			}
			return key.ToString();
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0001CC90 File Offset: 0x0001AE90
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x0001CC98 File Offset: 0x0001AE98
		public string Name { get; private set; }

		// Token: 0x04000315 RID: 789
		private static readonly TimeSpan s_minProbationDuration = TimeSpan.FromMilliseconds(250.0);

		// Token: 0x04000316 RID: 790
		private static readonly TimeSpan s_maxProbationDuration = TimeSpan.FromDays(1.0);

		// Token: 0x04000317 RID: 791
		private static readonly TimeSpan s_minBlockingDuration = TimeSpan.FromMilliseconds(250.0);

		// Token: 0x04000318 RID: 792
		private static readonly TimeSpan s_maxBlockingDuration = TimeSpan.FromDays(1.0);

		// Token: 0x04000319 RID: 793
		private readonly TimeSpan m_probationDuration;

		// Token: 0x0400031A RID: 794
		private readonly int m_maxViolationEventsPerProbation;

		// Token: 0x0400031B RID: 795
		private readonly TimeSpan m_blockingDuration;

		// Token: 0x0400031C RID: 796
		private readonly HashSet<TKey> m_alwaysBlockedKeys;

		// Token: 0x0400031D RID: 797
		private readonly HashSet<TKey> m_neverBlockedKeys;

		// Token: 0x0400031E RID: 798
		private readonly QuickAccessPool<TKey, EventRateLimiter> m_probationKeys;

		// Token: 0x0400031F RID: 799
		private readonly QuickAccessPool<TKey, DateTime> m_blockedKeys;

		// Token: 0x04000320 RID: 800
		private readonly object m_lock = new object();

		// Token: 0x04000321 RID: 801
		private readonly IMonitoredDenialOfServiceProtectionEventsKit m_eventsKit;

		// Token: 0x04000322 RID: 802
		private readonly int m_violationEventsMidThreshold;
	}
}
