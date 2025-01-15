using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ActivitySource : IDisposable
	{
		// Token: 0x06000143 RID: 323 RVA: 0x000054CC File Offset: 0x000036CC
		public ActivitySource(string name, [Nullable(2)] string version = "")
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Name = name;
			this.Version = version;
			ActivitySource.s_activeSources.Add(this);
			if (ActivitySource.s_allListeners.Count > 0)
			{
				ActivitySource.s_allListeners.EnumWithAction(delegate(ActivityListener listener, object source)
				{
					Func<ActivitySource, bool> shouldListenTo = listener.ShouldListenTo;
					if (shouldListenTo != null)
					{
						ActivitySource activitySource = (ActivitySource)source;
						if (shouldListenTo(activitySource))
						{
							activitySource.AddListener(listener);
						}
					}
				}, this);
			}
			GC.KeepAlive(DiagnosticSourceEventSource.Log);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005547 File Offset: 0x00003747
		public string Name { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000554F File Offset: 0x0000374F
		[Nullable(2)]
		public string Version
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005558 File Offset: 0x00003758
		public bool HasListeners()
		{
			SynchronizedList<ActivityListener> listeners = this._listeners;
			return listeners != null && listeners.Count > 0;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000557C File Offset: 0x0000377C
		[return: Nullable(2)]
		public Activity CreateActivity(string name, ActivityKind kind)
		{
			return this.CreateActivity(name, kind, default(ActivityContext), null, null, null, default(DateTimeOffset), false, ActivityIdFormat.Unknown);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000055A8 File Offset: 0x000037A8
		[NullableContext(2)]
		public Activity CreateActivity([Nullable(1)] string name, ActivityKind kind, ActivityContext parentContext, [Nullable(new byte[] { 2, 0, 1, 2 })] IEnumerable<KeyValuePair<string, object>> tags = null, IEnumerable<ActivityLink> links = null, ActivityIdFormat idFormat = ActivityIdFormat.Unknown)
		{
			return this.CreateActivity(name, kind, parentContext, null, tags, links, default(DateTimeOffset), false, idFormat);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000055D0 File Offset: 0x000037D0
		[return: Nullable(2)]
		public Activity CreateActivity(string name, ActivityKind kind, string parentId, [Nullable(new byte[] { 2, 0, 1, 2 })] IEnumerable<KeyValuePair<string, object>> tags = null, [Nullable(2)] IEnumerable<ActivityLink> links = null, ActivityIdFormat idFormat = ActivityIdFormat.Unknown)
		{
			return this.CreateActivity(name, kind, default(ActivityContext), parentId, tags, links, default(DateTimeOffset), false, idFormat);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005600 File Offset: 0x00003800
		[return: Nullable(2)]
		public Activity StartActivity([CallerMemberName] string name = "", ActivityKind kind = ActivityKind.Internal)
		{
			return this.CreateActivity(name, kind, default(ActivityContext), null, null, null, default(DateTimeOffset), true, ActivityIdFormat.Unknown);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000562C File Offset: 0x0000382C
		[NullableContext(2)]
		public Activity StartActivity([Nullable(1)] string name, ActivityKind kind, ActivityContext parentContext, [Nullable(new byte[] { 2, 0, 1, 2 })] IEnumerable<KeyValuePair<string, object>> tags = null, IEnumerable<ActivityLink> links = null, DateTimeOffset startTime = default(DateTimeOffset))
		{
			return this.CreateActivity(name, kind, parentContext, null, tags, links, startTime, true, ActivityIdFormat.Unknown);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000564C File Offset: 0x0000384C
		[return: Nullable(2)]
		public Activity StartActivity(string name, ActivityKind kind, string parentId, [Nullable(new byte[] { 2, 0, 1, 2 })] IEnumerable<KeyValuePair<string, object>> tags = null, [Nullable(2)] IEnumerable<ActivityLink> links = null, DateTimeOffset startTime = default(DateTimeOffset))
		{
			return this.CreateActivity(name, kind, default(ActivityContext), parentId, tags, links, startTime, true, ActivityIdFormat.Unknown);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005674 File Offset: 0x00003874
		[NullableContext(2)]
		public Activity StartActivity(ActivityKind kind, ActivityContext parentContext = default(ActivityContext), [Nullable(new byte[] { 2, 0, 1, 2 })] IEnumerable<KeyValuePair<string, object>> tags = null, IEnumerable<ActivityLink> links = null, DateTimeOffset startTime = default(DateTimeOffset), [Nullable(1)] [CallerMemberName] string name = "")
		{
			return this.CreateActivity(name, kind, parentContext, null, tags, links, startTime, true, ActivityIdFormat.Unknown);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005694 File Offset: 0x00003894
		private Activity CreateActivity(string name, ActivityKind kind, ActivityContext context, string parentId, IEnumerable<KeyValuePair<string, object>> tags, IEnumerable<ActivityLink> links, DateTimeOffset startTime, bool startIt = true, ActivityIdFormat idFormat = ActivityIdFormat.Unknown)
		{
			SynchronizedList<ActivityListener> listeners = this._listeners;
			if (listeners == null || listeners.Count == 0)
			{
				return null;
			}
			Activity activity = null;
			ActivitySamplingResult activitySamplingResult = ActivitySamplingResult.None;
			ActivityTagsCollection activityTagsCollection;
			if (parentId != null)
			{
				ActivityCreationOptions<string> activityCreationOptions = default(ActivityCreationOptions<string>);
				ActivityCreationOptions<ActivityContext> activityCreationOptions2 = default(ActivityCreationOptions<ActivityContext>);
				activityCreationOptions = new ActivityCreationOptions<string>(this, name, parentId, kind, tags, links, idFormat);
				if (activityCreationOptions.IdFormat == ActivityIdFormat.W3C)
				{
					activityCreationOptions2 = new ActivityCreationOptions<ActivityContext>(this, name, activityCreationOptions.GetContext(), kind, tags, links, ActivityIdFormat.W3C);
				}
				listeners.EnumWithFunc<string>(delegate(ActivityListener listener, ref ActivityCreationOptions<string> data, ref ActivitySamplingResult result, ref ActivityCreationOptions<ActivityContext> dataWithContext)
				{
					SampleActivity<string> sampleUsingParentId = listener.SampleUsingParentId;
					if (sampleUsingParentId != null)
					{
						ActivitySamplingResult activitySamplingResult2 = sampleUsingParentId(ref data);
						if (activitySamplingResult2 > result)
						{
							result = activitySamplingResult2;
							return;
						}
					}
					else if (data.IdFormat == ActivityIdFormat.W3C)
					{
						SampleActivity<ActivityContext> sample = listener.Sample;
						if (sample != null)
						{
							ActivitySamplingResult activitySamplingResult3 = sample(ref dataWithContext);
							if (activitySamplingResult3 > result)
							{
								result = activitySamplingResult3;
							}
						}
					}
				}, ref activityCreationOptions, ref activitySamplingResult, ref activityCreationOptions2);
				if (context == default(ActivityContext))
				{
					if (activityCreationOptions.GetContext() != default(ActivityContext))
					{
						context = activityCreationOptions.GetContext();
						parentId = null;
					}
					else if (activityCreationOptions2.GetContext() != default(ActivityContext))
					{
						context = activityCreationOptions2.GetContext();
						parentId = null;
					}
				}
				activityTagsCollection = activityCreationOptions.GetSamplingTags();
				ActivityTagsCollection samplingTags = activityCreationOptions2.GetSamplingTags();
				if (samplingTags != null)
				{
					if (activityTagsCollection == null)
					{
						activityTagsCollection = samplingTags;
					}
					else
					{
						foreach (KeyValuePair<string, object> keyValuePair in samplingTags)
						{
							activityTagsCollection.Add(keyValuePair);
						}
					}
				}
				idFormat = activityCreationOptions.IdFormat;
			}
			else
			{
				bool flag = context == default(ActivityContext) && Activity.Current != null;
				ActivityCreationOptions<ActivityContext> activityCreationOptions3 = new ActivityCreationOptions<ActivityContext>(this, name, flag ? Activity.Current.Context : context, kind, tags, links, idFormat);
				listeners.EnumWithFunc<ActivityContext>(delegate(ActivityListener listener, ref ActivityCreationOptions<ActivityContext> data, ref ActivitySamplingResult result, ref ActivityCreationOptions<ActivityContext> unused)
				{
					SampleActivity<ActivityContext> sample2 = listener.Sample;
					if (sample2 != null)
					{
						ActivitySamplingResult activitySamplingResult4 = sample2(ref data);
						if (activitySamplingResult4 > result)
						{
							result = activitySamplingResult4;
						}
					}
				}, ref activityCreationOptions3, ref activitySamplingResult, ref activityCreationOptions3);
				if (!flag)
				{
					context = activityCreationOptions3.GetContext();
				}
				activityTagsCollection = activityCreationOptions3.GetSamplingTags();
				idFormat = activityCreationOptions3.IdFormat;
			}
			if (activitySamplingResult != ActivitySamplingResult.None)
			{
				activity = Activity.Create(this, name, kind, parentId, context, tags, links, startTime, activityTagsCollection, activitySamplingResult, startIt, idFormat);
			}
			return activity;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000058A0 File Offset: 0x00003AA0
		public void Dispose()
		{
			this._listeners = null;
			ActivitySource.s_activeSources.Remove(this);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000058B8 File Offset: 0x00003AB8
		public static void AddActivityListener(ActivityListener listener)
		{
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			if (ActivitySource.s_allListeners.AddIfNotExist(listener))
			{
				ActivitySource.s_activeSources.EnumWithAction(delegate(ActivitySource source, object obj)
				{
					Func<ActivitySource, bool> shouldListenTo = ((ActivityListener)obj).ShouldListenTo;
					if (shouldListenTo != null && shouldListenTo(source))
					{
						source.AddListener((ActivityListener)obj);
					}
				}, listener);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000590A File Offset: 0x00003B0A
		internal void AddListener(ActivityListener listener)
		{
			if (this._listeners == null)
			{
				Interlocked.CompareExchange<SynchronizedList<ActivityListener>>(ref this._listeners, new SynchronizedList<ActivityListener>(), null);
			}
			this._listeners.AddIfNotExist(listener);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005933 File Offset: 0x00003B33
		internal static void DetachListener(ActivityListener listener)
		{
			ActivitySource.s_allListeners.Remove(listener);
			ActivitySource.s_activeSources.EnumWithAction(delegate(ActivitySource source, object obj)
			{
				SynchronizedList<ActivityListener> listeners = source._listeners;
				if (listeners == null)
				{
					return;
				}
				listeners.Remove((ActivityListener)obj);
			}, listener);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000596C File Offset: 0x00003B6C
		internal void NotifyActivityStart(Activity activity)
		{
			SynchronizedList<ActivityListener> listeners = this._listeners;
			if (listeners != null && listeners.Count > 0)
			{
				listeners.EnumWithAction(delegate(ActivityListener listener, object obj)
				{
					Action<Activity> activityStarted = listener.ActivityStarted;
					if (activityStarted == null)
					{
						return;
					}
					activityStarted((Activity)obj);
				}, activity);
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000059B4 File Offset: 0x00003BB4
		internal void NotifyActivityStop(Activity activity)
		{
			SynchronizedList<ActivityListener> listeners = this._listeners;
			if (listeners != null && listeners.Count > 0)
			{
				listeners.EnumWithAction(delegate(ActivityListener listener, object obj)
				{
					Action<Activity> activityStopped = listener.ActivityStopped;
					if (activityStopped == null)
					{
						return;
					}
					activityStopped((Activity)obj);
				}, activity);
			}
		}

		// Token: 0x04000079 RID: 121
		private static readonly SynchronizedList<ActivitySource> s_activeSources = new SynchronizedList<ActivitySource>();

		// Token: 0x0400007A RID: 122
		private static readonly SynchronizedList<ActivityListener> s_allListeners = new SynchronizedList<ActivityListener>();

		// Token: 0x0400007B RID: 123
		private SynchronizedList<ActivityListener> _listeners;

		// Token: 0x02000081 RID: 129
		// (Invoke) Token: 0x06000343 RID: 835
		internal delegate void Function<T, TParent>(T item, ref ActivityCreationOptions<TParent> data, ref ActivitySamplingResult samplingResult, ref ActivityCreationOptions<ActivityContext> dataWithContext);
	}
}
