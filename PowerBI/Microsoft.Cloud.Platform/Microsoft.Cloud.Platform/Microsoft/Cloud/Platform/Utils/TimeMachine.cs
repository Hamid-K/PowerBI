using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002C6 RID: 710
	internal class TimeMachine
	{
		// Token: 0x06001318 RID: 4888 RVA: 0x0004213C File Offset: 0x0004033C
		public TimeMachine(DateTime now)
		{
			this.m_now = now;
			this.m_alarms = new TimeMachine.AlarmClockSortedList();
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00042158 File Offset: 0x00040358
		public object AddAlarmClock(DateTime alarmTime, TimeSpan snooze, [NotNull] Action alarmCallback)
		{
			ExtendedDiagnostics.EnsureArgument("snooze", snooze.Ticks > 0L || !snooze.IsValidAsTimerPeriod(), "TimeSpan used as a period must be positive or set to -1msec");
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(alarmCallback, "alarmCallback");
			if (alarmTime <= this.m_now)
			{
				if (!snooze.IsValidAsTimerPeriod())
				{
					return TimeMachine.s_nullAlarmClock;
				}
				TimeSpan timeSpan = this.m_now.Subtract(alarmTime);
				long ticks = snooze.Ticks;
				long num = timeSpan.Ticks / ticks;
				alarmTime = alarmTime.AddTicks(ticks * num);
				if (alarmTime <= this.m_now)
				{
					alarmTime = alarmTime.AddTicks(ticks);
				}
			}
			TimeMachine.AlarmClock alarmClock = new TimeMachine.AlarmClock();
			alarmClock.NextAlarmTime = alarmTime;
			alarmClock.SnoozePeriod = snooze;
			alarmClock.AlarmCallback = alarmCallback;
			this.m_alarms.Add(alarmClock);
			return alarmClock;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0004221D File Offset: 0x0004041D
		public void RemoveAlarmClock(object alarmClock)
		{
			if (alarmClock == TimeMachine.s_nullAlarmClock)
			{
				return;
			}
			this.m_alarms.RemoveIfExists((TimeMachine.AlarmClock)alarmClock);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00042239 File Offset: 0x00040439
		public void JumpToTime(DateTime value)
		{
			this.m_now = value;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00042244 File Offset: 0x00040444
		public void AdvanceInTime(TimeSpan period)
		{
			if (period.Ticks == 0L)
			{
				return;
			}
			DateTime dateTime = this.m_now.Add(period);
			while (this.m_alarms.HasValues())
			{
				TimeMachine.AlarmClock alarmClock = this.m_alarms.PeekFirst();
				DateTime dateTime2 = alarmClock.NextAlarmTime;
				if (dateTime2 > dateTime)
				{
					break;
				}
				this.m_alarms.RemoveFirst();
				alarmClock.AlarmCallback();
				if (alarmClock.SnoozePeriod.IsValidAsTimerPeriod())
				{
					dateTime2 = alarmClock.NextAlarmTime.Add(alarmClock.SnoozePeriod);
					alarmClock.NextAlarmTime = dateTime2;
					this.m_alarms.Add(alarmClock);
				}
			}
			this.m_now = dateTime;
		}

		// Token: 0x0400071B RID: 1819
		private DateTime m_now;

		// Token: 0x0400071C RID: 1820
		private TimeMachine.AlarmClockSortedList m_alarms;

		// Token: 0x0400071D RID: 1821
		private static TimeMachine.AlarmClock s_nullAlarmClock = new TimeMachine.AlarmClock();

		// Token: 0x0200077F RID: 1919
		private class AlarmClock
		{
			// Token: 0x1700075B RID: 1883
			// (get) Token: 0x06003087 RID: 12423 RVA: 0x000A68A3 File Offset: 0x000A4AA3
			// (set) Token: 0x06003088 RID: 12424 RVA: 0x000A68AB File Offset: 0x000A4AAB
			public DateTime NextAlarmTime { get; set; }

			// Token: 0x1700075C RID: 1884
			// (get) Token: 0x06003089 RID: 12425 RVA: 0x000A68B4 File Offset: 0x000A4AB4
			// (set) Token: 0x0600308A RID: 12426 RVA: 0x000A68BC File Offset: 0x000A4ABC
			public TimeSpan SnoozePeriod { get; set; }

			// Token: 0x1700075D RID: 1885
			// (get) Token: 0x0600308B RID: 12427 RVA: 0x000A68C5 File Offset: 0x000A4AC5
			// (set) Token: 0x0600308C RID: 12428 RVA: 0x000A68CD File Offset: 0x000A4ACD
			public Action AlarmCallback { get; set; }
		}

		// Token: 0x02000780 RID: 1920
		private class AlarmClockSortedList
		{
			// Token: 0x0600308E RID: 12430 RVA: 0x000A68D8 File Offset: 0x000A4AD8
			public void Add(TimeMachine.AlarmClock alarmClock)
			{
				TimeMachine.AlarmClockSortedList.UniqueDateTime uniqueDateTime = default(TimeMachine.AlarmClockSortedList.UniqueDateTime);
				uniqueDateTime.m_value = alarmClock.NextAlarmTime;
				long num = this.m_unique + 1L;
				this.m_unique = num;
				uniqueDateTime.m_unique = num;
				TimeMachine.AlarmClockSortedList.UniqueDateTime uniqueDateTime2 = uniqueDateTime;
				this.m_sortedList.Add(uniqueDateTime2, alarmClock);
			}

			// Token: 0x0600308F RID: 12431 RVA: 0x000A6922 File Offset: 0x000A4B22
			public bool HasValues()
			{
				return this.m_sortedList.Count > 0;
			}

			// Token: 0x06003090 RID: 12432 RVA: 0x000A6932 File Offset: 0x000A4B32
			public TimeMachine.AlarmClock PeekFirst()
			{
				return this.m_sortedList.Values[0];
			}

			// Token: 0x06003091 RID: 12433 RVA: 0x000A6945 File Offset: 0x000A4B45
			public void RemoveFirst()
			{
				this.m_sortedList.RemoveAt(0);
			}

			// Token: 0x06003092 RID: 12434 RVA: 0x000A6954 File Offset: 0x000A4B54
			public void RemoveIfExists(TimeMachine.AlarmClock alarmClock)
			{
				int num = this.m_sortedList.IndexOfValue(alarmClock);
				if (num >= 0)
				{
					this.m_sortedList.RemoveAt(num);
				}
			}

			// Token: 0x04001634 RID: 5684
			private long m_unique;

			// Token: 0x04001635 RID: 5685
			private SortedList<TimeMachine.AlarmClockSortedList.UniqueDateTime, TimeMachine.AlarmClock> m_sortedList = new SortedList<TimeMachine.AlarmClockSortedList.UniqueDateTime, TimeMachine.AlarmClock>(new TimeMachine.AlarmClockSortedList.UniqueDateTimeComparer());

			// Token: 0x02000886 RID: 2182
			private struct UniqueDateTime
			{
				// Token: 0x04001A27 RID: 6695
				public DateTime m_value;

				// Token: 0x04001A28 RID: 6696
				public long m_unique;
			}

			// Token: 0x02000887 RID: 2183
			private class UniqueDateTimeComparer : IComparer<TimeMachine.AlarmClockSortedList.UniqueDateTime>
			{
				// Token: 0x060033D3 RID: 13267 RVA: 0x000AD674 File Offset: 0x000AB874
				public int Compare(TimeMachine.AlarmClockSortedList.UniqueDateTime x, TimeMachine.AlarmClockSortedList.UniqueDateTime y)
				{
					int num = DateTime.Compare(x.m_value, y.m_value);
					if (num != 0)
					{
						return num;
					}
					if (x.m_unique == y.m_unique)
					{
						return 0;
					}
					if (x.m_unique < y.m_unique)
					{
						return -1;
					}
					return 1;
				}
			}
		}
	}
}
