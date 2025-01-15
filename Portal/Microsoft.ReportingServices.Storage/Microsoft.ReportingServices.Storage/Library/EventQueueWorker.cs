using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200000B RID: 11
	internal class EventQueueWorker : QueuePollWorker
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00004510 File Offset: 0x00002710
		public EventQueueWorker()
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004518 File Offset: 0x00002718
		public EventQueueWorker(IEventResolver eventResolver)
		{
			this.EventResolver = eventResolver;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004528 File Offset: 0x00002728
		protected override void CleanInactiveRows(int minutes)
		{
			base.ExecuteStoredProcedure("CleanEventRecords", new Dictionary<string, object> { { "@MaxAgeMinutes", minutes } });
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004558 File Offset: 0x00002758
		protected override string QueueIDColumnName
		{
			get
			{
				return "EventID";
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000455F File Offset: 0x0000275F
		protected override string QueueTableName
		{
			get
			{
				return "Event";
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004566 File Offset: 0x00002766
		protected override string PollingTraceName
		{
			get
			{
				return "EventPolling";
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000456D File Offset: 0x0000276D
		public override QueueItem GetNextQueueItem(IDataRecord record)
		{
			return new EventQueueItem
			{
				ID = record.GetGuid(0),
				EventType = record.GetString(1),
				EventData = record.GetString(2)
			};
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000459C File Offset: 0x0000279C
		public override bool QueueWorker(QueueItem item)
		{
			EventQueueItem eventQueueItem = (EventQueueItem)item;
			if (RSTrace.ScheduleTracer.TraceInfo)
			{
				RSTrace.ScheduleTracer.Trace("Handling event with data: {0}.", new object[] { eventQueueItem.ItemString() });
			}
			IEventHandler eventHandler = null;
			if (this.EventResolver != null)
			{
				eventHandler = this.EventResolver.ResolveEvent(eventQueueItem.EventType);
			}
			if (eventHandler == null)
			{
				if (RSTrace.ScheduleTracer.TraceInfo)
				{
					RSTrace.ScheduleTracer.Trace("No extension defined for event {0}", new object[] { eventQueueItem.EventType });
				}
				return true;
			}
			return this.ProcessSingleEvent(eventHandler, eventQueueItem);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004630 File Offset: 0x00002830
		private bool ProcessSingleEvent(IEventHandler eventHandler, EventQueueItem item)
		{
			try
			{
				CatalogQuery catalogQuery = new CatalogQuery();
				catalogQuery.WillClose();
				try
				{
					eventHandler.HandleEvent(catalogQuery, item.EventType, item.EventData);
					catalogQuery.Commit();
				}
				catch
				{
					catalogQuery.AbortTransaction();
					throw;
				}
				finally
				{
					catalogQuery.Close();
				}
				if (this.EventResolver != null)
				{
					this.EventResolver.ItemPlacedInEventQueue();
				}
			}
			catch (Exception ex) when (this.IsDeadLockException(ex))
			{
				RSTrace.ScheduleTracer.Trace(TraceLevel.Warning, string.Format("Deadlock detected running event: {0}. Error: {1}", item.ItemString(), ex.ToString()));
				RSTrace.ScheduleTracer.Trace(string.Format("Error processing event with data: {0}. Error: {1}", item.ItemString(), ex.ToString()));
				return false;
			}
			catch (Exception ex2)
			{
				RSTrace.ScheduleTracer.Trace(string.Format("Error processing event with data: {0}. Error: {1}", item.ItemString(), ex2.ToString()));
			}
			return true;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004748 File Offset: 0x00002948
		private bool IsDeadLockException(Exception ex)
		{
			SqlException ex2;
			if (ex is SqlException)
			{
				ex2 = ex as SqlException;
			}
			else
			{
				if (!(ex is ReportServerStorageException) || !(((ex != null) ? ex.InnerException : null) is SqlException))
				{
					return false;
				}
				ex2 = ex.InnerException as SqlException;
			}
			return ex2.Number == 1205;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000047A0 File Offset: 0x000029A0
		public override void DeleteQueueItem(QueueItem item)
		{
			base.ExecuteStoredProcedure("DeleteEvent", new Dictionary<string, object> { { "@ID", item.ID } });
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000047D5 File Offset: 0x000029D5
		public override InstrumentedSqlCommand PollCommand
		{
			get
			{
				InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("PollEventsForRSProcess"));
				instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
				instrumentedSqlCommand.AddParameter("@NumberOfEvents", SqlDbType.Int, base.NumberOfItemsToRetrieve);
				return instrumentedSqlCommand;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004805 File Offset: 0x00002A05
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000480D File Offset: 0x00002A0D
		internal IEventResolver EventResolver { get; private set; }

		// Token: 0x0200004A RID: 74
		private enum EventProjection
		{
			// Token: 0x040001DF RID: 479
			ID,
			// Token: 0x040001E0 RID: 480
			Type,
			// Token: 0x040001E1 RID: 481
			Data
		}
	}
}
