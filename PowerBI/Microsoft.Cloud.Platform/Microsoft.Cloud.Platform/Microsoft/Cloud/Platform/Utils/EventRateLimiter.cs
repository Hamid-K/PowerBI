using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001FB RID: 507
	public class EventRateLimiter
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0002EF7A File Offset: 0x0002D17A
		public int TimeslotEventCount
		{
			get
			{
				return this.m_timeSlotEventCount;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0002EF82 File Offset: 0x0002D182
		public int MaxSlotEvents
		{
			get
			{
				return this.m_slotMaxEvents;
			}
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002EF8C File Offset: 0x0002D18C
		public EventRateLimiter(TimeSpan slotDuration, int slotMaxEvents)
		{
			ExtendedDiagnostics.EnsureOperation(slotDuration >= new TimeSpan(0, 0, 0, 0, 250) && slotDuration <= new TimeSpan(1, 1, 0, 0, 0), "Slot duration value (" + slotDuration + ") is not between 0.25s and 1d1h");
			ExtendedDiagnostics.EnsureArgumentIsPositive(slotMaxEvents, "slotMaxEvents");
			this.m_slotDuration = slotDuration;
			this.m_slotMaxEvents = slotMaxEvents;
			DateTime dateTime = EventRateLimiter.s_timeInitialSlot;
			this.StartNewSlot(dateTime, 0);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0002F00C File Offset: 0x0002D20C
		public bool ShouldProcessEvent(DateTime eventTime)
		{
			DateTime eventProcessingTime = this.GetEventProcessingTime(eventTime);
			return eventProcessingTime == eventTime || eventProcessingTime == this.m_timeSlotHorizon;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002F038 File Offset: 0x0002D238
		public DateTime GetEventProcessingTime(DateTime eventTime)
		{
			if (eventTime <= this.m_timeSlotHorizon)
			{
				eventTime = this.m_timeSlotHorizon;
			}
			if (eventTime > this.m_timeSlotEnds)
			{
				return eventTime;
			}
			if (this.m_timeSlotEventCount < this.m_slotMaxEvents)
			{
				return eventTime;
			}
			return this.m_timeSlotEnds.AddTicks(1L);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0002F088 File Offset: 0x0002D288
		public void UpdateTimeslotState(DateTime eventTime)
		{
			if (eventTime <= this.m_timeSlotHorizon)
			{
				eventTime = this.m_timeSlotHorizon;
			}
			if (eventTime > this.m_timeSlotEnds)
			{
				this.StartNewSlot(eventTime, 1);
				return;
			}
			this.AddEventToCurrentTimeSlot(eventTime);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002F0C0 File Offset: 0x0002D2C0
		private void StartNewSlot(DateTime firstItemInSlot, int initialEventCount)
		{
			if (initialEventCount == 0 || this.m_timeSlotBegins == EventRateLimiter.s_timeInitialSlot)
			{
				this.m_timeSlotBegins = firstItemInSlot;
			}
			else if (firstItemInSlot < this.m_timeSlotEnds.Add(this.m_slotDuration))
			{
				this.m_timeSlotBegins = this.m_timeSlotEnds.AddTicks(1L);
			}
			else
			{
				this.m_timeSlotBegins = firstItemInSlot.Subtract(this.m_slotDuration);
			}
			this.m_timeSlotEnds = this.m_timeSlotBegins.Add(this.m_slotDuration);
			this.m_timeSlotHorizon = firstItemInSlot;
			this.m_timeSlotEventCount = initialEventCount;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0002F151 File Offset: 0x0002D351
		private void AddEventToCurrentTimeSlot(DateTime eventTime)
		{
			this.m_timeSlotEventCount++;
			this.m_timeSlotHorizon = eventTime;
		}

		// Token: 0x0400053E RID: 1342
		private readonly TimeSpan m_slotDuration;

		// Token: 0x0400053F RID: 1343
		private readonly int m_slotMaxEvents;

		// Token: 0x04000540 RID: 1344
		private static DateTime s_timeInitialSlot = DateTime.MinValue;

		// Token: 0x04000541 RID: 1345
		private DateTime m_timeSlotBegins;

		// Token: 0x04000542 RID: 1346
		private DateTime m_timeSlotEnds;

		// Token: 0x04000543 RID: 1347
		private DateTime m_timeSlotHorizon;

		// Token: 0x04000544 RID: 1348
		private int m_timeSlotEventCount;
	}
}
