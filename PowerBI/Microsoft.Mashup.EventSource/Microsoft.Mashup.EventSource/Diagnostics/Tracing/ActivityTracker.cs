using System;
using System.Security;
using System.Threading;
using Microsoft.Diagnostics.Contracts.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200000C RID: 12
	internal class ActivityTracker
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002314 File Offset: 0x00000514
		public void OnStart(string providerName, string activityName, int task, ref Guid activityId, ref Guid relatedActivityId, EventActivityOptions options)
		{
			if (this.m_current == null)
			{
				return;
			}
			Contract.Assert((options & EventActivityOptions.Disable) == EventActivityOptions.None);
			ActivityTracker.ActivityInfo activityInfo = this.m_current.Value;
			string text = this.NormalizeActivityName(providerName, activityName, task);
			TplEtwProvider.Logger log = TplEtwProvider.Log;
			if (log.Debug)
			{
				log.DebugFacilityMessage(new object[] { "OnStartEnter", text });
				log.DebugFacilityMessage(new object[]
				{
					"OnStartEnterActivityState",
					ActivityTracker.ActivityInfo.LiveActivities(activityInfo)
				});
			}
			if (activityInfo != null)
			{
				if (activityInfo.m_level >= 100)
				{
					activityId = Guid.Empty;
					relatedActivityId = Guid.Empty;
					if (log.Debug)
					{
						log.DebugFacilityMessage(new object[] { "OnStartRET", "Fail" });
					}
					return;
				}
				if ((options & EventActivityOptions.Recursive) == EventActivityOptions.None && this.FindActiveActivity(text, activityInfo) != null)
				{
					this.OnStop(providerName, activityName, task, ref activityId);
					activityInfo = this.m_current.Value;
				}
			}
			long num;
			if (activityInfo == null)
			{
				num = Interlocked.Increment(ref ActivityTracker.m_nextId);
			}
			else
			{
				num = Interlocked.Increment(ref activityInfo.m_lastChildID);
			}
			relatedActivityId = ((activityInfo != null) ? activityInfo.ActivityId : EventSource.CurrentThreadActivityId);
			ActivityTracker.ActivityInfo activityInfo2 = new ActivityTracker.ActivityInfo(text, num, activityInfo, options);
			this.m_current.Value = activityInfo2;
			activityId = activityInfo2.ActivityId;
			if (log.Debug)
			{
				log.DebugFacilityMessage(new object[]
				{
					"OnStartRetActivityState",
					ActivityTracker.ActivityInfo.LiveActivities(activityInfo2)
				});
				log.DebugFacilityMessage(new object[]
				{
					"OnStartRet",
					activityId.ToString(),
					relatedActivityId.ToString()
				});
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024B4 File Offset: 0x000006B4
		public void OnStop(string providerName, string activityName, int task, ref Guid activityId)
		{
			if (this.m_current == null)
			{
				return;
			}
			string text = this.NormalizeActivityName(providerName, activityName, task);
			TplEtwProvider.Logger log = TplEtwProvider.Log;
			if (log.Debug)
			{
				log.DebugFacilityMessage(new object[] { "OnStopEnter", text });
				log.DebugFacilityMessage(new object[]
				{
					"OnStopEnterActivityState",
					ActivityTracker.ActivityInfo.LiveActivities(this.m_current.Value)
				});
			}
			ActivityTracker.ActivityInfo activityInfo;
			for (;;)
			{
				ActivityTracker.ActivityInfo value = this.m_current.Value;
				activityInfo = null;
				ActivityTracker.ActivityInfo activityInfo2 = this.FindActiveActivity(text, value);
				if (activityInfo2 == null)
				{
					break;
				}
				activityId = activityInfo2.ActivityId;
				ActivityTracker.ActivityInfo activityInfo3 = value;
				while (activityInfo3 != activityInfo2 && activityInfo3 != null)
				{
					if (activityInfo3.m_stopped != 0)
					{
						activityInfo3 = activityInfo3.m_creator;
					}
					else
					{
						if (activityInfo3.CanBeOrphan())
						{
							if (activityInfo == null)
							{
								activityInfo = activityInfo3;
							}
						}
						else
						{
							activityInfo3.m_stopped = 1;
							Contract.Assert(activityInfo3.m_stopped != 0);
						}
						activityInfo3 = activityInfo3.m_creator;
					}
				}
				if (Interlocked.CompareExchange(ref activityInfo2.m_stopped, 1, 0) == 0)
				{
					goto Block_9;
				}
			}
			activityId = Guid.Empty;
			if (log.Debug)
			{
				log.DebugFacilityMessage(new object[] { "OnStopRET", "Fail" });
			}
			return;
			Block_9:
			if (activityInfo == null)
			{
				ActivityTracker.ActivityInfo activityInfo2;
				activityInfo = activityInfo2.m_creator;
			}
			this.m_current.Value = activityInfo;
			if (log.Debug)
			{
				log.DebugFacilityMessage(new object[]
				{
					"OnStopRetActivityState",
					ActivityTracker.ActivityInfo.LiveActivities(activityInfo)
				});
				log.DebugFacilityMessage(new object[]
				{
					"OnStopRet",
					activityId.ToString()
				});
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002643 File Offset: 0x00000843
		[SecuritySafeCritical]
		public void Enable()
		{
			if (this.m_current == null)
			{
				this.m_current = new AsyncLocal<ActivityTracker.ActivityInfo>(new Action<AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo>>(this.ActivityChanging));
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002664 File Offset: 0x00000864
		public static ActivityTracker Instance
		{
			get
			{
				return ActivityTracker.s_activityTrackerInstance;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000266B File Offset: 0x0000086B
		private Guid CurrentActivityId
		{
			get
			{
				return this.m_current.Value.ActivityId;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002680 File Offset: 0x00000880
		private ActivityTracker.ActivityInfo FindActiveActivity(string name, ActivityTracker.ActivityInfo startLocation)
		{
			for (ActivityTracker.ActivityInfo activityInfo = startLocation; activityInfo != null; activityInfo = activityInfo.m_creator)
			{
				if (name == activityInfo.m_name && activityInfo.m_stopped == 0)
				{
					return activityInfo;
				}
			}
			return null;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026B4 File Offset: 0x000008B4
		private string NormalizeActivityName(string providerName, string activityName, int task)
		{
			if (activityName.EndsWith("Start"))
			{
				activityName = activityName.Substring(0, activityName.Length - "Start".Length);
			}
			else if (activityName.EndsWith("Stop"))
			{
				activityName = activityName.Substring(0, activityName.Length - "Stop".Length);
			}
			else if (task != 0)
			{
				activityName = "task" + task.ToString();
			}
			return providerName + activityName;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002730 File Offset: 0x00000930
		private void ActivityChanging(AsyncLocalValueChangedArgs<ActivityTracker.ActivityInfo> args)
		{
			if (args.PreviousValue == args.CurrentValue)
			{
				return;
			}
			if (args.CurrentValue != null)
			{
				EventSource.SetCurrentThreadActivityId(args.CurrentValue.ActivityId);
				return;
			}
			EventSource.SetCurrentThreadActivityId(Guid.Empty);
		}

		// Token: 0x0400000E RID: 14
		private AsyncLocal<ActivityTracker.ActivityInfo> m_current;

		// Token: 0x0400000F RID: 15
		private static ActivityTracker s_activityTrackerInstance = new ActivityTracker();

		// Token: 0x04000010 RID: 16
		private static long m_nextId = 0L;

		// Token: 0x04000011 RID: 17
		private const ushort MAX_ACTIVITY_DEPTH = 100;

		// Token: 0x0200007E RID: 126
		private class ActivityInfo
		{
			// Token: 0x060002ED RID: 749 RVA: 0x0000E678 File Offset: 0x0000C878
			public ActivityInfo(string name, long uniqueId, ActivityTracker.ActivityInfo creator, EventActivityOptions options)
			{
				this.m_name = name;
				this.m_eventOptions = options;
				this.m_creator = creator;
				this.m_uniqueId = uniqueId;
				this.m_level = ((creator != null) ? (creator.m_level + 1) : 0);
				this.CreateActivityPathGuid(out this.m_guid, out this.m_activityPathGuidOffset);
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x060002EE RID: 750 RVA: 0x0000E6CE File Offset: 0x0000C8CE
			public Guid ActivityId
			{
				get
				{
					return this.m_guid;
				}
			}

			// Token: 0x060002EF RID: 751 RVA: 0x0000E6D8 File Offset: 0x0000C8D8
			public static string Path(ActivityTracker.ActivityInfo activityInfo)
			{
				if (activityInfo == null)
				{
					return "";
				}
				return ActivityTracker.ActivityInfo.Path(activityInfo.m_creator) + "/" + activityInfo.m_uniqueId.ToString();
			}

			// Token: 0x060002F0 RID: 752 RVA: 0x0000E714 File Offset: 0x0000C914
			public override string ToString()
			{
				string text = "";
				if (this.m_stopped != 0)
				{
					text = ",DEAD";
				}
				return string.Concat(new string[]
				{
					this.m_name,
					"(",
					ActivityTracker.ActivityInfo.Path(this),
					text,
					")"
				});
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x0000E766 File Offset: 0x0000C966
			public static string LiveActivities(ActivityTracker.ActivityInfo list)
			{
				if (list == null)
				{
					return "";
				}
				return list.ToString() + ";" + ActivityTracker.ActivityInfo.LiveActivities(list.m_creator);
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x0000E78C File Offset: 0x0000C98C
			public bool CanBeOrphan()
			{
				return (this.m_eventOptions & EventActivityOptions.Detachable) != EventActivityOptions.None;
			}

			// Token: 0x060002F3 RID: 755 RVA: 0x0000E79C File Offset: 0x0000C99C
			[SecuritySafeCritical]
			private unsafe void CreateActivityPathGuid(out Guid idRet, out int activityPathGuidOffset)
			{
				fixed (Guid* ptr = &idRet)
				{
					Guid* ptr2 = ptr;
					int num = 0;
					if (this.m_creator != null)
					{
						num = this.m_creator.m_activityPathGuidOffset;
						idRet = this.m_creator.m_guid;
					}
					else
					{
						int num2 = 0;
						num = ActivityTracker.ActivityInfo.AddIdToGuid(ptr2, num, (uint)num2, false);
					}
					activityPathGuidOffset = ActivityTracker.ActivityInfo.AddIdToGuid(ptr2, num, (uint)this.m_uniqueId, false);
					if (12 < activityPathGuidOffset)
					{
						this.CreateOverflowGuid(ptr2);
					}
				}
			}

			// Token: 0x060002F4 RID: 756 RVA: 0x0000E804 File Offset: 0x0000CA04
			[SecurityCritical]
			private unsafe void CreateOverflowGuid(Guid* outPtr)
			{
				for (ActivityTracker.ActivityInfo activityInfo = this.m_creator; activityInfo != null; activityInfo = activityInfo.m_creator)
				{
					if (activityInfo.m_activityPathGuidOffset <= 10)
					{
						uint num = (uint)Interlocked.Increment(ref activityInfo.m_lastChildID);
						*outPtr = activityInfo.m_guid;
						if (ActivityTracker.ActivityInfo.AddIdToGuid(outPtr, activityInfo.m_activityPathGuidOffset, num, true) <= 12)
						{
							break;
						}
					}
				}
			}

			// Token: 0x060002F5 RID: 757 RVA: 0x0000E85C File Offset: 0x0000CA5C
			[SecurityCritical]
			private unsafe static int AddIdToGuid(Guid* outPtr, int whereToAddId, uint id, bool overflow = false)
			{
				byte* ptr = (byte*)outPtr;
				byte* ptr2 = ptr + 12;
				ptr += whereToAddId;
				if (ptr2 == ptr)
				{
					return 13;
				}
				if (0U < id && id <= 10U && !overflow)
				{
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, id);
				}
				else
				{
					uint num = 4U;
					if (id <= 255U)
					{
						num = 1U;
					}
					else if (id <= 65535U)
					{
						num = 2U;
					}
					else if (id <= 16777215U)
					{
						num = 3U;
					}
					if (overflow)
					{
						if (ptr2 == ptr + 2)
						{
							return 13;
						}
						ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 11U);
					}
					ActivityTracker.ActivityInfo.WriteNibble(ref ptr, ptr2, 12U + (num - 1U));
					if (ptr < ptr2 && *ptr != 0)
					{
						if (id < 4096U)
						{
							*ptr = (byte)(192U + (id >> 8));
							id &= 255U;
						}
						ptr++;
					}
					while (0U < num)
					{
						if (ptr2 == ptr)
						{
							ptr++;
							break;
						}
						*(ptr++) = (byte)id;
						id >>= 8;
						num -= 1U;
					}
				}
				*(int*)(outPtr + (IntPtr)3 * 4 / (IntPtr)sizeof(Guid)) = (int)(*(uint*)outPtr + *(uint*)(outPtr + 4 / sizeof(Guid)) + *(uint*)(outPtr + (IntPtr)2 * 4 / (IntPtr)sizeof(Guid)) + 1503500717U);
				return (int)((long)((byte*)ptr - (byte*)outPtr));
			}

			// Token: 0x060002F6 RID: 758 RVA: 0x0000E94C File Offset: 0x0000CB4C
			[SecurityCritical]
			private unsafe static void WriteNibble(ref byte* ptr, byte* endPtr, uint value)
			{
				Contract.Assert(0U <= value && value < 16U);
				Contract.Assert(ptr < endPtr);
				if (*ptr != 0)
				{
					byte* ptr2 = ptr;
					ptr = ptr2 + 1;
					byte* ptr3 = ptr2;
					*ptr3 |= (byte)value;
					return;
				}
				*ptr = (byte)(value << 4);
			}

			// Token: 0x04000186 RID: 390
			internal readonly string m_name;

			// Token: 0x04000187 RID: 391
			private readonly long m_uniqueId;

			// Token: 0x04000188 RID: 392
			internal readonly Guid m_guid;

			// Token: 0x04000189 RID: 393
			internal readonly int m_activityPathGuidOffset;

			// Token: 0x0400018A RID: 394
			internal readonly int m_level;

			// Token: 0x0400018B RID: 395
			internal readonly EventActivityOptions m_eventOptions;

			// Token: 0x0400018C RID: 396
			internal long m_lastChildID;

			// Token: 0x0400018D RID: 397
			internal int m_stopped;

			// Token: 0x0400018E RID: 398
			internal readonly ActivityTracker.ActivityInfo m_creator;

			// Token: 0x02000097 RID: 151
			private enum NumberListCodes : byte
			{
				// Token: 0x040001D0 RID: 464
				End,
				// Token: 0x040001D1 RID: 465
				LastImmediateValue = 10,
				// Token: 0x040001D2 RID: 466
				PrefixCode,
				// Token: 0x040001D3 RID: 467
				MultiByte1
			}
		}
	}
}
