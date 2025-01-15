using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	public class Activity : IDisposable
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003211 File Offset: 0x00001411
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003218 File Offset: 0x00001418
		public static bool ForceDefaultIdFormat { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003220 File Offset: 0x00001420
		public ActivityStatusCode Status
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003228 File Offset: 0x00001428
		[Nullable(2)]
		public string StatusDescription
		{
			[NullableContext(2)]
			get
			{
				return this._statusDescription;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003230 File Offset: 0x00001430
		public Activity SetStatus(ActivityStatusCode code, [Nullable(2)] string description = null)
		{
			this._statusCode = code;
			this._statusDescription = ((code == ActivityStatusCode.Error) ? description : null);
			return this;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003248 File Offset: 0x00001448
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003250 File Offset: 0x00001450
		public ActivityKind Kind { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003259 File Offset: 0x00001459
		public string OperationName { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003261 File Offset: 0x00001461
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003273 File Offset: 0x00001473
		public string DisplayName
		{
			get
			{
				return this._displayName ?? this.OperationName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._displayName = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000328B File Offset: 0x0000148B
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003293 File Offset: 0x00001493
		public ActivitySource Source { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000329C File Offset: 0x0000149C
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000032A4 File Offset: 0x000014A4
		[Nullable(2)]
		public Activity Parent
		{
			[NullableContext(2)]
			get;
			private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000032AD File Offset: 0x000014AD
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000032B5 File Offset: 0x000014B5
		public TimeSpan Duration { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000032BE File Offset: 0x000014BE
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000032C6 File Offset: 0x000014C6
		public DateTime StartTimeUtc { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000032D0 File Offset: 0x000014D0
		[Nullable(2)]
		public unsafe string Id
		{
			[NullableContext(2)]
			[SecuritySafeCritical]
			get
			{
				if (this._id == null && this._spanId != null)
				{
					Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)4], 2);
					Span<char> span2 = span;
					HexConverter.ToCharsBuffer((byte)(-129 & (int)this._w3CIdFlags), span2, 0, HexConverter.Casing.Lower);
					string text = string.Concat(new string[]
					{
						"00-",
						this._traceId,
						"-",
						this._spanId,
						"-",
						span2.ToString()
					});
					Interlocked.CompareExchange<string>(ref this._id, text, null);
				}
				return this._id;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003370 File Offset: 0x00001570
		[Nullable(2)]
		public unsafe string ParentId
		{
			[NullableContext(2)]
			[SecuritySafeCritical]
			get
			{
				if (this._parentId == null)
				{
					if (this._parentSpanId != null)
					{
						Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)4], 2);
						Span<char> span2 = span;
						HexConverter.ToCharsBuffer((byte)(-129 & (int)this._parentTraceFlags), span2, 0, HexConverter.Casing.Lower);
						string text = string.Concat(new string[]
						{
							"00-",
							this._traceId,
							"-",
							this._parentSpanId,
							"-",
							span2.ToString()
						});
						Interlocked.CompareExchange<string>(ref this._parentId, text, null);
					}
					else if (this.Parent != null)
					{
						Interlocked.CompareExchange<string>(ref this._parentId, this.Parent.Id, null);
					}
				}
				return this._parentId;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003434 File Offset: 0x00001634
		[Nullable(2)]
		public string RootId
		{
			[NullableContext(2)]
			get
			{
				if (this._rootId == null)
				{
					string text = null;
					if (this.Id != null)
					{
						text = this.GetRootId(this.Id);
					}
					else if (this.ParentId != null)
					{
						text = this.GetRootId(this.ParentId);
					}
					if (text != null)
					{
						Interlocked.CompareExchange<string>(ref this._rootId, text, null);
					}
				}
				return this._rootId;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000348E File Offset: 0x0000168E
		[Nullable(new byte[] { 1, 0, 1, 2 })]
		public IEnumerable<KeyValuePair<string, string>> Tags
		{
			[return: Nullable(new byte[] { 1, 0, 1, 2 })]
			get
			{
				Activity.TagsLinkedList tags = this._tags;
				return ((tags != null) ? tags.EnumerateStringValues() : null) ?? Activity.s_emptyBaggageTags;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000034AC File Offset: 0x000016AC
		[Nullable(new byte[] { 1, 0, 1, 2 })]
		public IEnumerable<KeyValuePair<string, object>> TagObjects
		{
			[return: Nullable(new byte[] { 1, 0, 1, 2 })]
			get
			{
				IEnumerable<KeyValuePair<string, object>> tags = this._tags;
				return tags ?? Activity.s_emptyTagObjects;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000034CC File Offset: 0x000016CC
		public IEnumerable<ActivityEvent> Events
		{
			get
			{
				IEnumerable<ActivityEvent> events = this._events;
				return events ?? Activity.s_emptyEvents;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000034EC File Offset: 0x000016EC
		public IEnumerable<ActivityLink> Links
		{
			get
			{
				IEnumerable<ActivityLink> links = this._links;
				return links ?? Activity.s_emptyLinks;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000350C File Offset: 0x0000170C
		[Nullable(new byte[] { 1, 0, 1, 2 })]
		public IEnumerable<KeyValuePair<string, string>> Baggage
		{
			[return: Nullable(new byte[] { 1, 0, 1, 2 })]
			get
			{
				for (Activity activity = this; activity != null; activity = activity.Parent)
				{
					if (activity._baggage != null)
					{
						return Activity.<get_Baggage>g__Iterate|80_0(activity);
					}
				}
				return Activity.s_emptyBaggageTags;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000353C File Offset: 0x0000173C
		[return: Nullable(2)]
		public string GetBaggageItem(string key)
		{
			foreach (KeyValuePair<string, string> keyValuePair in this.Baggage)
			{
				if (key == keyValuePair.Key)
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000035A0 File Offset: 0x000017A0
		[return: Nullable(2)]
		public object GetTagItem(string key)
		{
			Activity.TagsLinkedList tags = this._tags;
			return ((tags != null) ? tags.Get(key) : null) ?? null;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000035BA File Offset: 0x000017BA
		public Activity(string operationName)
		{
			this.Source = Activity.s_defaultSource;
			this.IsAllDataRequested = true;
			if (string.IsNullOrEmpty(operationName))
			{
				Activity.NotifyError(new ArgumentException(SR.OperationNameInvalid));
			}
			this.OperationName = operationName;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000035F2 File Offset: 0x000017F2
		public Activity AddTag(string key, [Nullable(2)] string value)
		{
			return this.AddTag(key, value);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000035FC File Offset: 0x000017FC
		public Activity AddTag(string key, [Nullable(2)] object value)
		{
			KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(key, value);
			if (this._tags != null || Interlocked.CompareExchange<Activity.TagsLinkedList>(ref this._tags, new Activity.TagsLinkedList(keyValuePair, false), null) != null)
			{
				this._tags.Add(keyValuePair);
			}
			return this;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000363C File Offset: 0x0000183C
		public Activity SetTag(string key, [Nullable(2)] object value)
		{
			KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(key, value);
			if (this._tags != null || Interlocked.CompareExchange<Activity.TagsLinkedList>(ref this._tags, new Activity.TagsLinkedList(keyValuePair, true), null) != null)
			{
				this._tags.Set(keyValuePair);
			}
			return this;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000367C File Offset: 0x0000187C
		public Activity AddEvent(ActivityEvent e)
		{
			if (this._events != null || Interlocked.CompareExchange<DiagLinkedList<ActivityEvent>>(ref this._events, new DiagLinkedList<ActivityEvent>(e), null) != null)
			{
				this._events.Add(e);
			}
			return this;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000036A8 File Offset: 0x000018A8
		public Activity AddBaggage(string key, [Nullable(2)] string value)
		{
			KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(key, value);
			if (this._baggage != null || Interlocked.CompareExchange<Activity.BaggageLinkedList>(ref this._baggage, new Activity.BaggageLinkedList(keyValuePair, false), null) != null)
			{
				this._baggage.Add(keyValuePair);
			}
			return this;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000036E8 File Offset: 0x000018E8
		public Activity SetBaggage(string key, [Nullable(2)] string value)
		{
			KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(key, value);
			if (this._baggage != null || Interlocked.CompareExchange<Activity.BaggageLinkedList>(ref this._baggage, new Activity.BaggageLinkedList(keyValuePair, true), null) != null)
			{
				this._baggage.Set(keyValuePair);
			}
			return this;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003728 File Offset: 0x00001928
		public Activity SetParentId(string parentId)
		{
			if (this.Parent != null)
			{
				Activity.NotifyError(new InvalidOperationException(SR.SetParentIdOnActivityWithParent));
			}
			else if (this.ParentId != null || this._parentSpanId != null)
			{
				Activity.NotifyError(new InvalidOperationException(SR.ParentIdAlreadySet));
			}
			else if (string.IsNullOrEmpty(parentId))
			{
				Activity.NotifyError(new ArgumentException(SR.ParentIdInvalid));
			}
			else
			{
				this._parentId = parentId;
			}
			return this;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003790 File Offset: 0x00001990
		public Activity SetParentId(ActivityTraceId traceId, ActivitySpanId spanId, ActivityTraceFlags activityTraceFlags = ActivityTraceFlags.None)
		{
			if (this.Parent != null)
			{
				Activity.NotifyError(new InvalidOperationException(SR.SetParentIdOnActivityWithParent));
			}
			else if (this.ParentId != null || this._parentSpanId != null)
			{
				Activity.NotifyError(new InvalidOperationException(SR.ParentIdAlreadySet));
			}
			else
			{
				this._traceId = traceId.ToHexString();
				this._parentSpanId = spanId.ToHexString();
				this.ActivityTraceFlags = activityTraceFlags;
				this._parentTraceFlags = (byte)activityTraceFlags;
			}
			return this;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003801 File Offset: 0x00001A01
		public Activity SetStartTime(DateTime startTimeUtc)
		{
			if (startTimeUtc.Kind != DateTimeKind.Utc)
			{
				Activity.NotifyError(new InvalidOperationException(SR.StartTimeNotUtc));
			}
			else
			{
				this.StartTimeUtc = startTimeUtc;
			}
			return this;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003828 File Offset: 0x00001A28
		public Activity SetEndTime(DateTime endTimeUtc)
		{
			if (endTimeUtc.Kind != DateTimeKind.Utc)
			{
				Activity.NotifyError(new InvalidOperationException(SR.EndTimeNotUtc));
			}
			else
			{
				this.Duration = endTimeUtc - this.StartTimeUtc;
				if (this.Duration.Ticks <= 0L)
				{
					this.Duration = new TimeSpan(1L);
				}
			}
			return this;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003882 File Offset: 0x00001A82
		public ActivityContext Context
		{
			get
			{
				return new ActivityContext(this.TraceId, this.SpanId, this.ActivityTraceFlags, this.TraceStateString, false);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000038A4 File Offset: 0x00001AA4
		public Activity Start()
		{
			if (this._id != null || this._spanId != null)
			{
				Activity.NotifyError(new InvalidOperationException(SR.ActivityStartAlreadyStarted));
			}
			else
			{
				this._previousActiveActivity = Activity.Current;
				if (this._parentId == null && this._parentSpanId == null && this._previousActiveActivity != null)
				{
					this.Parent = this._previousActiveActivity;
				}
				if (this.StartTimeUtc == default(DateTime))
				{
					this.StartTimeUtc = Activity.GetUtcNow();
				}
				if (this.IdFormat == ActivityIdFormat.Unknown)
				{
					this.IdFormat = (Activity.ForceDefaultIdFormat ? Activity.DefaultIdFormat : ((this.Parent != null) ? this.Parent.IdFormat : ((this._parentSpanId != null) ? ActivityIdFormat.W3C : ((this._parentId == null) ? Activity.DefaultIdFormat : (Activity.IsW3CId(this._parentId) ? ActivityIdFormat.W3C : ActivityIdFormat.Hierarchical)))));
				}
				if (this.IdFormat == ActivityIdFormat.W3C)
				{
					this.GenerateW3CId();
				}
				else
				{
					this._id = this.GenerateHierarchicalId();
				}
				Activity.SetCurrent(this);
				this.Source.NotifyActivityStart(this);
			}
			return this;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000039B4 File Offset: 0x00001BB4
		public void Stop()
		{
			if (this._id == null && this._spanId == null)
			{
				Activity.NotifyError(new InvalidOperationException(SR.ActivityNotStarted));
				return;
			}
			if (!this.IsFinished)
			{
				this.IsFinished = true;
				if (this.Duration == TimeSpan.Zero)
				{
					this.SetEndTime(Activity.GetUtcNow());
				}
				this.Source.NotifyActivityStop(this);
				Activity.SetCurrent(this._previousActiveActivity);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003A28 File Offset: 0x00001C28
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003A50 File Offset: 0x00001C50
		[Nullable(2)]
		public string TraceStateString
		{
			[NullableContext(2)]
			get
			{
				for (Activity activity = this; activity != null; activity = activity.Parent)
				{
					string traceState = activity._traceState;
					if (traceState != null)
					{
						return traceState;
					}
				}
				return null;
			}
			[NullableContext(2)]
			set
			{
				this._traceState = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003A5C File Offset: 0x00001C5C
		public ActivitySpanId SpanId
		{
			[SecuritySafeCritical]
			get
			{
				if (this._spanId == null && this._id != null && this.IdFormat == ActivityIdFormat.W3C)
				{
					string text = ActivitySpanId.CreateFromString(MemoryExtensions.AsSpan(this._id, 36, 16)).ToHexString();
					Interlocked.CompareExchange<string>(ref this._spanId, text, null);
				}
				return new ActivitySpanId(this._spanId);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public ActivityTraceId TraceId
		{
			get
			{
				if (this._traceId == null)
				{
					this.TrySetTraceIdFromParent();
				}
				return new ActivityTraceId(this._traceId);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public bool Recorded
		{
			get
			{
				return (this.ActivityTraceFlags & ActivityTraceFlags.Recorded) > ActivityTraceFlags.None;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003AE1 File Offset: 0x00001CE1
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003AE9 File Offset: 0x00001CE9
		public bool IsAllDataRequested { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003AF2 File Offset: 0x00001CF2
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003B0E File Offset: 0x00001D0E
		public ActivityTraceFlags ActivityTraceFlags
		{
			get
			{
				if (!this.W3CIdFlagsSet)
				{
					this.TrySetTraceFlagsFromParent();
				}
				return (ActivityTraceFlags)(-129) & (ActivityTraceFlags)this._w3CIdFlags;
			}
			set
			{
				this._w3CIdFlags = 128 | (byte)value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003B20 File Offset: 0x00001D20
		public ActivitySpanId ParentSpanId
		{
			[SecuritySafeCritical]
			get
			{
				if (this._parentSpanId == null)
				{
					string text = null;
					if (this._parentId != null && Activity.IsW3CId(this._parentId))
					{
						try
						{
							text = ActivitySpanId.CreateFromString(MemoryExtensions.AsSpan(this._parentId, 36, 16)).ToHexString();
							goto IL_006B;
						}
						catch
						{
							goto IL_006B;
						}
					}
					if (this.Parent != null && this.Parent.IdFormat == ActivityIdFormat.W3C)
					{
						text = this.Parent.SpanId.ToHexString();
					}
					IL_006B:
					if (text != null)
					{
						Interlocked.CompareExchange<string>(ref this._parentSpanId, text, null);
					}
				}
				return new ActivitySpanId(this._parentSpanId);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003BC4 File Offset: 0x00001DC4
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003BCB File Offset: 0x00001DCB
		[Nullable(2)]
		public static Func<ActivityTraceId> TraceIdGenerator
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003BD3 File Offset: 0x00001DD3
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003BE7 File Offset: 0x00001DE7
		public static ActivityIdFormat DefaultIdFormat
		{
			get
			{
				if (Activity.s_defaultIdFormat == ActivityIdFormat.Unknown)
				{
					Activity.s_defaultIdFormat = ActivityIdFormat.Hierarchical;
				}
				return Activity.s_defaultIdFormat;
			}
			set
			{
				if (ActivityIdFormat.Hierarchical > value || value > ActivityIdFormat.W3C)
				{
					throw new ArgumentException(SR.ActivityIdFormatInvalid);
				}
				Activity.s_defaultIdFormat = value;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003C02 File Offset: 0x00001E02
		public Activity SetIdFormat(ActivityIdFormat format)
		{
			if (this._id != null || this._spanId != null)
			{
				Activity.NotifyError(new InvalidOperationException(SR.SetFormatOnStartedActivity));
			}
			else
			{
				this.IdFormat = format;
			}
			return this;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003C30 File Offset: 0x00001E30
		private static bool IsW3CId(string id)
		{
			return id.Length == 55 && (('0' <= id[0] && id[0] <= '9') || ('a' <= id[0] && id[0] <= 'f')) && (('0' <= id[1] && id[1] <= '9') || ('a' <= id[1] && id[1] <= 'f')) && (id[0] != 'f' || id[1] != 'f');
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003CBC File Offset: 0x00001EBC
		[SecuritySafeCritical]
		internal static bool TryConvertIdToContext(string traceParent, string traceState, out ActivityContext context)
		{
			context = default(ActivityContext);
			if (!Activity.IsW3CId(traceParent))
			{
				return false;
			}
			ReadOnlySpan<char> readOnlySpan = MemoryExtensions.AsSpan(traceParent, 3, 32);
			ReadOnlySpan<char> readOnlySpan2 = MemoryExtensions.AsSpan(traceParent, 36, 16);
			if (!ActivityTraceId.IsLowerCaseHexAndNotAllZeros(readOnlySpan) || !ActivityTraceId.IsLowerCaseHexAndNotAllZeros(readOnlySpan2) || !HexConverter.IsHexLowerChar((int)traceParent[53]) || !HexConverter.IsHexLowerChar((int)traceParent[54]))
			{
				return false;
			}
			context = new ActivityContext(new ActivityTraceId(readOnlySpan.ToString()), new ActivitySpanId(readOnlySpan2.ToString()), (ActivityTraceFlags)ActivityTraceId.HexByteFromChars(traceParent[53], traceParent[54]), traceState, false);
			return true;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003D66 File Offset: 0x00001F66
		public void Dispose()
		{
			if (!this.IsFinished)
			{
				this.Stop();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003D83 File Offset: 0x00001F83
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003D88 File Offset: 0x00001F88
		public void SetCustomProperty(string propertyName, [Nullable(2)] object propertyValue)
		{
			if (this._customProperties == null)
			{
				Interlocked.CompareExchange<Dictionary<string, object>>(ref this._customProperties, new Dictionary<string, object>(), null);
			}
			Dictionary<string, object> customProperties = this._customProperties;
			lock (customProperties)
			{
				if (propertyValue == null)
				{
					this._customProperties.Remove(propertyName);
				}
				else
				{
					this._customProperties[propertyName] = propertyValue;
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003DFC File Offset: 0x00001FFC
		[return: Nullable(2)]
		public object GetCustomProperty(string propertyName)
		{
			if (this._customProperties == null)
			{
				return null;
			}
			Dictionary<string, object> customProperties = this._customProperties;
			object obj;
			lock (customProperties)
			{
				object obj2;
				obj = (this._customProperties.TryGetValue(propertyName, out obj2) ? obj2 : null);
			}
			return obj;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003E58 File Offset: 0x00002058
		internal static Activity Create(ActivitySource source, string name, ActivityKind kind, string parentId, ActivityContext parentContext, IEnumerable<KeyValuePair<string, object>> tags, IEnumerable<ActivityLink> links, DateTimeOffset startTime, ActivityTagsCollection samplerTags, ActivitySamplingResult request, bool startIt, ActivityIdFormat idFormat)
		{
			Activity activity = new Activity(name);
			activity.Source = source;
			activity.Kind = kind;
			activity.IdFormat = idFormat;
			if (links != null)
			{
				using (IEnumerator<ActivityLink> enumerator = links.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						activity._links = new DiagLinkedList<ActivityLink>(enumerator);
					}
				}
			}
			if (tags != null)
			{
				using (IEnumerator<KeyValuePair<string, object>> enumerator2 = tags.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						activity._tags = new Activity.TagsLinkedList(enumerator2);
					}
				}
			}
			if (samplerTags != null)
			{
				if (activity._tags == null)
				{
					activity._tags = new Activity.TagsLinkedList(samplerTags);
				}
				else
				{
					activity._tags.Add(samplerTags);
				}
			}
			if (parentId != null)
			{
				activity._parentId = parentId;
			}
			else if (parentContext != default(ActivityContext))
			{
				activity._traceId = parentContext.TraceId.ToString();
				if (parentContext.SpanId != default(ActivitySpanId))
				{
					activity._parentSpanId = parentContext.SpanId.ToString();
				}
				activity.ActivityTraceFlags = parentContext.TraceFlags;
				activity._parentTraceFlags = (byte)parentContext.TraceFlags;
				activity._traceState = parentContext.TraceState;
			}
			activity.IsAllDataRequested = request == ActivitySamplingResult.AllData || request == ActivitySamplingResult.AllDataAndRecorded;
			if (request == ActivitySamplingResult.AllDataAndRecorded)
			{
				activity.ActivityTraceFlags |= ActivityTraceFlags.Recorded;
			}
			if (startTime != default(DateTimeOffset))
			{
				activity.StartTimeUtc = startTime.UtcDateTime;
			}
			if (startIt)
			{
				activity.Start();
			}
			return activity;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000400C File Offset: 0x0000220C
		private void GenerateW3CId()
		{
			if (this._traceId == null && !this.TrySetTraceIdFromParent())
			{
				Func<ActivityTraceId> traceIdGenerator = Activity.TraceIdGenerator;
				this._traceId = ((traceIdGenerator == null) ? ActivityTraceId.CreateRandom() : traceIdGenerator()).ToHexString();
			}
			if (!this.W3CIdFlagsSet)
			{
				this.TrySetTraceFlagsFromParent();
			}
			this._spanId = ActivitySpanId.CreateRandom().ToHexString();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004070 File Offset: 0x00002270
		private static void NotifyError(Exception exception)
		{
			try
			{
				throw exception;
			}
			catch
			{
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004094 File Offset: 0x00002294
		private string GenerateHierarchicalId()
		{
			string text;
			if (this.Parent != null)
			{
				text = this.AppendSuffix(this.Parent.Id, Interlocked.Increment(ref this.Parent._currentChildId).ToString(), '.');
			}
			else if (this.ParentId != null)
			{
				string text2 = ((this.ParentId[0] == '|') ? this.ParentId : ("|" + this.ParentId));
				char c = text2[text2.Length - 1];
				if (c != '.' && c != '_')
				{
					text2 += ".";
				}
				text = this.AppendSuffix(text2, Interlocked.Increment(ref Activity.s_currentRootId).ToString("x"), '_');
			}
			else
			{
				text = Activity.GenerateRootId();
			}
			return text;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000415C File Offset: 0x0000235C
		private string GetRootId(string id)
		{
			if (this.IdFormat == ActivityIdFormat.W3C)
			{
				return id.Substring(3, 32);
			}
			int num = id.IndexOf('.');
			if (num < 0)
			{
				num = id.Length;
			}
			int num2 = ((id[0] == '|') ? 1 : 0);
			return id.Substring(num2, num - num2);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000041AC File Offset: 0x000023AC
		private string AppendSuffix(string parentId, string suffix, char delimiter)
		{
			if (parentId.Length + suffix.Length < 1024)
			{
				return parentId + suffix + delimiter.ToString();
			}
			int num = 1015;
			while (num > 1 && parentId[num - 1] != '.' && parentId[num - 1] != '_')
			{
				num--;
			}
			if (num == 1)
			{
				return Activity.GenerateRootId();
			}
			string text = ((int)Activity.GetRandomNumber()).ToString("x8");
			return parentId.Substring(0, num) + text + "#";
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004238 File Offset: 0x00002438
		[SecuritySafeCritical]
		private unsafe static long GetRandomNumber()
		{
			Guid guid = Guid.NewGuid();
			return *(long*)(&guid);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004250 File Offset: 0x00002450
		private static bool ValidateSetCurrent(Activity activity)
		{
			bool flag = activity == null || (activity.Id != null && !activity.IsFinished);
			if (!flag)
			{
				Activity.NotifyError(new InvalidOperationException(SR.ActivityNotRunning));
			}
			return flag;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000428C File Offset: 0x0000248C
		[SecuritySafeCritical]
		private bool TrySetTraceIdFromParent()
		{
			if (this.Parent != null && this.Parent.IdFormat == ActivityIdFormat.W3C)
			{
				this._traceId = this.Parent.TraceId.ToHexString();
			}
			else if (this._parentId != null && Activity.IsW3CId(this._parentId))
			{
				try
				{
					this._traceId = ActivityTraceId.CreateFromString(MemoryExtensions.AsSpan(this._parentId, 3, 32)).ToHexString();
				}
				catch
				{
				}
			}
			return this._traceId != null;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004320 File Offset: 0x00002520
		[SecuritySafeCritical]
		private void TrySetTraceFlagsFromParent()
		{
			if (!this.W3CIdFlagsSet)
			{
				if (this.Parent != null)
				{
					this.ActivityTraceFlags = this.Parent.ActivityTraceFlags;
					return;
				}
				if (this._parentId != null && Activity.IsW3CId(this._parentId))
				{
					if (HexConverter.IsHexLowerChar((int)this._parentId[53]) && HexConverter.IsHexLowerChar((int)this._parentId[54]))
					{
						this._w3CIdFlags = ActivityTraceId.HexByteFromChars(this._parentId[53], this._parentId[54]) | 128;
						return;
					}
					this._w3CIdFlags = 128;
				}
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000043C7 File Offset: 0x000025C7
		private bool W3CIdFlagsSet
		{
			get
			{
				return (this._w3CIdFlags & 128) > 0;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000043D8 File Offset: 0x000025D8
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000043E9 File Offset: 0x000025E9
		private bool IsFinished
		{
			get
			{
				return (this._state & Activity.State.IsFinished) > Activity.State.None;
			}
			set
			{
				if (value)
				{
					this._state |= Activity.State.IsFinished;
					return;
				}
				this._state &= ~Activity.State.IsFinished;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004410 File Offset: 0x00002610
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000441A File Offset: 0x0000261A
		public ActivityIdFormat IdFormat
		{
			get
			{
				return (ActivityIdFormat)(this._state & Activity.State.FormatFlags);
			}
			private set
			{
				this._state = (this._state & ~(Activity.State.FormatHierarchical | Activity.State.FormatW3C)) | (Activity.State)((byte)value & 3);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004434 File Offset: 0x00002634
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004440 File Offset: 0x00002640
		[Nullable(2)]
		public static Activity Current
		{
			[NullableContext(2)]
			get
			{
				return Activity.s_current.Value;
			}
			[NullableContext(2)]
			set
			{
				if (Activity.ValidateSetCurrent(value))
				{
					Activity.SetCurrent(value);
				}
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004450 File Offset: 0x00002650
		private static void SetCurrent(Activity activity)
		{
			Activity.s_current.Value = activity;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004460 File Offset: 0x00002660
		private static string GenerateRootId()
		{
			return "|" + Interlocked.Increment(ref Activity.s_currentRootId).ToString("x") + Activity.s_uniqSuffix;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004494 File Offset: 0x00002694
		internal static DateTime GetUtcNow()
		{
			Activity.TimeSync timeSync = Activity.timeSync;
			long num = (long)((double)((Stopwatch.GetTimestamp() - timeSync.SyncStopwatchTicks) * 10000000L) / (double)Stopwatch.Frequency);
			return timeSync.SyncUtcNow.AddTicks(num);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000044D3 File Offset: 0x000026D3
		private static void Sync()
		{
			Thread.Sleep(1);
			Activity.timeSync = new Activity.TimeSync();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000044E8 File Offset: 0x000026E8
		[SecuritySafeCritical]
		private static Timer InitalizeSyncTimer()
		{
			bool flag = false;
			Timer timer;
			try
			{
				if (!ExecutionContext.IsFlowSuppressed())
				{
					ExecutionContext.SuppressFlow();
					flag = true;
				}
				timer = new Timer(delegate(object s)
				{
					Activity.Sync();
				}, null, 0, 7200000);
			}
			finally
			{
				if (flag)
				{
					ExecutionContext.RestoreFlow();
				}
			}
			return timer;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000045F0 File Offset: 0x000027F0
		[CompilerGenerated]
		internal static IEnumerable<KeyValuePair<string, string>> <get_Baggage>g__Iterate|80_0(Activity activity)
		{
			do
			{
				if (activity._baggage != null)
				{
					DiagNode<KeyValuePair<string, string>> current;
					for (current = activity._baggage.First; current != null; current = current.Next)
					{
						yield return current.Value;
					}
					current = null;
				}
				activity = activity.Parent;
			}
			while (activity != null);
			yield break;
		}

		// Token: 0x0400001D RID: 29
		private static readonly IEnumerable<KeyValuePair<string, string>> s_emptyBaggageTags = new KeyValuePair<string, string>[0];

		// Token: 0x0400001E RID: 30
		private static readonly IEnumerable<KeyValuePair<string, object>> s_emptyTagObjects = new KeyValuePair<string, object>[0];

		// Token: 0x0400001F RID: 31
		private static readonly IEnumerable<ActivityLink> s_emptyLinks = new ActivityLink[0];

		// Token: 0x04000020 RID: 32
		private static readonly IEnumerable<ActivityEvent> s_emptyEvents = new ActivityEvent[0];

		// Token: 0x04000021 RID: 33
		private static readonly ActivitySource s_defaultSource = new ActivitySource(string.Empty, "");

		// Token: 0x04000022 RID: 34
		private const byte ActivityTraceFlagsIsSet = 128;

		// Token: 0x04000023 RID: 35
		private const int RequestIdMaxLength = 1024;

		// Token: 0x04000024 RID: 36
		private static readonly string s_uniqSuffix = "-" + Activity.GetRandomNumber().ToString("x") + ".";

		// Token: 0x04000025 RID: 37
		private static long s_currentRootId = (long)((ulong)((uint)Activity.GetRandomNumber()));

		// Token: 0x04000026 RID: 38
		private static ActivityIdFormat s_defaultIdFormat;

		// Token: 0x04000028 RID: 40
		private string _traceState;

		// Token: 0x04000029 RID: 41
		private Activity.State _state;

		// Token: 0x0400002A RID: 42
		private int _currentChildId;

		// Token: 0x0400002B RID: 43
		private string _id;

		// Token: 0x0400002C RID: 44
		private string _rootId;

		// Token: 0x0400002D RID: 45
		private string _parentId;

		// Token: 0x0400002E RID: 46
		private string _parentSpanId;

		// Token: 0x0400002F RID: 47
		private string _traceId;

		// Token: 0x04000030 RID: 48
		private string _spanId;

		// Token: 0x04000031 RID: 49
		private byte _w3CIdFlags;

		// Token: 0x04000032 RID: 50
		private byte _parentTraceFlags;

		// Token: 0x04000033 RID: 51
		private Activity.TagsLinkedList _tags;

		// Token: 0x04000034 RID: 52
		private Activity.BaggageLinkedList _baggage;

		// Token: 0x04000035 RID: 53
		private DiagLinkedList<ActivityLink> _links;

		// Token: 0x04000036 RID: 54
		private DiagLinkedList<ActivityEvent> _events;

		// Token: 0x04000037 RID: 55
		private Dictionary<string, object> _customProperties;

		// Token: 0x04000038 RID: 56
		private string _displayName;

		// Token: 0x04000039 RID: 57
		private ActivityStatusCode _statusCode;

		// Token: 0x0400003A RID: 58
		private string _statusDescription;

		// Token: 0x0400003B RID: 59
		private Activity _previousActiveActivity;

		// Token: 0x04000044 RID: 68
		private static readonly AsyncLocal<Activity> s_current = new AsyncLocal<Activity>();

		// Token: 0x04000045 RID: 69
		private static Activity.TimeSync timeSync = new Activity.TimeSync();

		// Token: 0x04000046 RID: 70
		private static readonly Timer syncTimeUpdater = Activity.InitalizeSyncTimer();

		// Token: 0x0200007A RID: 122
		private sealed class BaggageLinkedList : IEnumerable<KeyValuePair<string, string>>, IEnumerable
		{
			// Token: 0x0600031B RID: 795 RVA: 0x0000C3CA File Offset: 0x0000A5CA
			public BaggageLinkedList(KeyValuePair<string, string> firstValue, bool set = false)
			{
				this._first = ((set && firstValue.Value == null) ? null : new DiagNode<KeyValuePair<string, string>>(firstValue));
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x0600031C RID: 796 RVA: 0x0000C3ED File Offset: 0x0000A5ED
			public DiagNode<KeyValuePair<string, string>> First
			{
				get
				{
					return this._first;
				}
			}

			// Token: 0x0600031D RID: 797 RVA: 0x0000C3F8 File Offset: 0x0000A5F8
			public void Add(KeyValuePair<string, string> value)
			{
				DiagNode<KeyValuePair<string, string>> diagNode = new DiagNode<KeyValuePair<string, string>>(value);
				lock (this)
				{
					diagNode.Next = this._first;
					this._first = diagNode;
				}
			}

			// Token: 0x0600031E RID: 798 RVA: 0x0000C448 File Offset: 0x0000A648
			public void Set(KeyValuePair<string, string> value)
			{
				if (value.Value == null)
				{
					this.Remove(value.Key);
					return;
				}
				lock (this)
				{
					for (DiagNode<KeyValuePair<string, string>> diagNode = this._first; diagNode != null; diagNode = diagNode.Next)
					{
						if (diagNode.Value.Key == value.Key)
						{
							diagNode.Value = value;
							return;
						}
					}
					this._first = new DiagNode<KeyValuePair<string, string>>(value)
					{
						Next = this._first
					};
				}
			}

			// Token: 0x0600031F RID: 799 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
			public void Remove(string key)
			{
				lock (this)
				{
					if (this._first != null)
					{
						if (this._first.Value.Key == key)
						{
							this._first = this._first.Next;
						}
						else
						{
							DiagNode<KeyValuePair<string, string>> diagNode = this._first;
							while (diagNode.Next != null)
							{
								if (diagNode.Next.Value.Key == key)
								{
									diagNode.Next = diagNode.Next.Next;
									break;
								}
								diagNode = diagNode.Next;
							}
						}
					}
				}
			}

			// Token: 0x06000320 RID: 800 RVA: 0x0000C594 File Offset: 0x0000A794
			public Enumerator<KeyValuePair<string, string>> GetEnumerator()
			{
				return new Enumerator<KeyValuePair<string, string>>(this._first);
			}

			// Token: 0x06000321 RID: 801 RVA: 0x0000C5A1 File Offset: 0x0000A7A1
			IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000322 RID: 802 RVA: 0x0000C5AE File Offset: 0x0000A7AE
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400017E RID: 382
			private DiagNode<KeyValuePair<string, string>> _first;
		}

		// Token: 0x0200007B RID: 123
		private sealed class TagsLinkedList : IEnumerable<KeyValuePair<string, object>>, IEnumerable
		{
			// Token: 0x06000323 RID: 803 RVA: 0x0000C5BC File Offset: 0x0000A7BC
			public TagsLinkedList(KeyValuePair<string, object> firstValue, bool set = false)
			{
				this._last = (this._first = ((set && firstValue.Value == null) ? null : new DiagNode<KeyValuePair<string, object>>(firstValue)));
			}

			// Token: 0x06000324 RID: 804 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
			public TagsLinkedList(IEnumerator<KeyValuePair<string, object>> e)
			{
				this._last = (this._first = new DiagNode<KeyValuePair<string, object>>(e.Current));
				while (e.MoveNext())
				{
					KeyValuePair<string, object> keyValuePair = e.Current;
					this._last.Next = new DiagNode<KeyValuePair<string, object>>(keyValuePair);
					this._last = this._last.Next;
				}
			}

			// Token: 0x06000325 RID: 805 RVA: 0x0000C652 File Offset: 0x0000A852
			public TagsLinkedList(IEnumerable<KeyValuePair<string, object>> list)
			{
				this.Add(list);
			}

			// Token: 0x06000326 RID: 806 RVA: 0x0000C664 File Offset: 0x0000A864
			public void Add(IEnumerable<KeyValuePair<string, object>> list)
			{
				IEnumerator<KeyValuePair<string, object>> enumerator = list.GetEnumerator();
				if (!enumerator.MoveNext())
				{
					return;
				}
				if (this._first == null)
				{
					this._last = (this._first = new DiagNode<KeyValuePair<string, object>>(enumerator.Current));
				}
				else
				{
					this._last.Next = new DiagNode<KeyValuePair<string, object>>(enumerator.Current);
					this._last = this._last.Next;
				}
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, object> keyValuePair = enumerator.Current;
					this._last.Next = new DiagNode<KeyValuePair<string, object>>(keyValuePair);
					this._last = this._last.Next;
				}
			}

			// Token: 0x06000327 RID: 807 RVA: 0x0000C700 File Offset: 0x0000A900
			public void Add(KeyValuePair<string, object> value)
			{
				DiagNode<KeyValuePair<string, object>> diagNode = new DiagNode<KeyValuePair<string, object>>(value);
				lock (this)
				{
					if (this._first == null)
					{
						this._first = (this._last = diagNode);
					}
					else
					{
						this._last.Next = diagNode;
						this._last = diagNode;
					}
				}
			}

			// Token: 0x06000328 RID: 808 RVA: 0x0000C76C File Offset: 0x0000A96C
			public object Get(string key)
			{
				for (DiagNode<KeyValuePair<string, object>> diagNode = this._first; diagNode != null; diagNode = diagNode.Next)
				{
					if (diagNode.Value.Key == key)
					{
						return diagNode.Value.Value;
					}
				}
				return null;
			}

			// Token: 0x06000329 RID: 809 RVA: 0x0000C7AC File Offset: 0x0000A9AC
			public void Remove(string key)
			{
				lock (this)
				{
					if (this._first != null)
					{
						if (this._first.Value.Key == key)
						{
							this._first = this._first.Next;
							if (this._first == null)
							{
								this._last = null;
							}
						}
						else
						{
							DiagNode<KeyValuePair<string, object>> diagNode = this._first;
							while (diagNode.Next != null)
							{
								if (diagNode.Next.Value.Key == key)
								{
									if (this._last == diagNode.Next)
									{
										this._last = diagNode;
									}
									diagNode.Next = diagNode.Next.Next;
									break;
								}
								diagNode = diagNode.Next;
							}
						}
					}
				}
			}

			// Token: 0x0600032A RID: 810 RVA: 0x0000C880 File Offset: 0x0000AA80
			public void Set(KeyValuePair<string, object> value)
			{
				if (value.Value == null)
				{
					this.Remove(value.Key);
					return;
				}
				lock (this)
				{
					for (DiagNode<KeyValuePair<string, object>> diagNode = this._first; diagNode != null; diagNode = diagNode.Next)
					{
						if (diagNode.Value.Key == value.Key)
						{
							diagNode.Value = value;
							return;
						}
					}
					DiagNode<KeyValuePair<string, object>> diagNode2 = new DiagNode<KeyValuePair<string, object>>(value);
					if (this._first == null)
					{
						this._first = (this._last = diagNode2);
					}
					else
					{
						this._last.Next = diagNode2;
						this._last = diagNode2;
					}
				}
			}

			// Token: 0x0600032B RID: 811 RVA: 0x0000C938 File Offset: 0x0000AB38
			public Enumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				return new Enumerator<KeyValuePair<string, object>>(this._first);
			}

			// Token: 0x0600032C RID: 812 RVA: 0x0000C945 File Offset: 0x0000AB45
			IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600032D RID: 813 RVA: 0x0000C952 File Offset: 0x0000AB52
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600032E RID: 814 RVA: 0x0000C95F File Offset: 0x0000AB5F
			public IEnumerable<KeyValuePair<string, string>> EnumerateStringValues()
			{
				for (DiagNode<KeyValuePair<string, object>> current = this._first; current != null; current = current.Next)
				{
					if (current.Value.Value is string || current.Value.Value == null)
					{
						yield return new KeyValuePair<string, string>(current.Value.Key, (string)current.Value.Value);
					}
				}
				yield break;
			}

			// Token: 0x0600032F RID: 815 RVA: 0x0000C970 File Offset: 0x0000AB70
			public override string ToString()
			{
				string text;
				lock (this)
				{
					if (this._first == null)
					{
						text = string.Empty;
					}
					else
					{
						if (this._stringBuilder == null)
						{
							this._stringBuilder = new StringBuilder();
						}
						this._stringBuilder.Append(this._first.Value.Key);
						this._stringBuilder.Append(':');
						this._stringBuilder.Append(this._first.Value.Value);
						for (DiagNode<KeyValuePair<string, object>> diagNode = this._first.Next; diagNode != null; diagNode = diagNode.Next)
						{
							this._stringBuilder.Append(", ");
							this._stringBuilder.Append(diagNode.Value.Key);
							this._stringBuilder.Append(':');
							this._stringBuilder.Append(diagNode.Value.Value);
						}
						string text2 = this._stringBuilder.ToString();
						this._stringBuilder.Clear();
						text = text2;
					}
				}
				return text;
			}

			// Token: 0x0400017F RID: 383
			private DiagNode<KeyValuePair<string, object>> _first;

			// Token: 0x04000180 RID: 384
			private DiagNode<KeyValuePair<string, object>> _last;

			// Token: 0x04000181 RID: 385
			private StringBuilder _stringBuilder;
		}

		// Token: 0x0200007C RID: 124
		[Flags]
		private enum State : byte
		{
			// Token: 0x04000183 RID: 387
			None = 0,
			// Token: 0x04000184 RID: 388
			FormatUnknown = 0,
			// Token: 0x04000185 RID: 389
			FormatHierarchical = 1,
			// Token: 0x04000186 RID: 390
			FormatW3C = 2,
			// Token: 0x04000187 RID: 391
			FormatFlags = 3,
			// Token: 0x04000188 RID: 392
			IsFinished = 128
		}

		// Token: 0x0200007D RID: 125
		private sealed class TimeSync
		{
			// Token: 0x04000189 RID: 393
			public readonly DateTime SyncUtcNow = DateTime.UtcNow;

			// Token: 0x0400018A RID: 394
			public readonly long SyncStopwatchTicks = Stopwatch.GetTimestamp();
		}
	}
}
