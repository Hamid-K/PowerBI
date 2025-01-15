using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x0200047C RID: 1148
	public class EventBuffer : CircularAsynchronousBuffer
	{
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060027D7 RID: 10199 RVA: 0x000783E4 File Offset: 0x000765E4
		protected override int numelem
		{
			get
			{
				return 100000;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x000783EB File Offset: 0x000765EB
		protected override int MaximumWaitingTimeMillis
		{
			get
			{
				return 5000;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060027D9 RID: 10201 RVA: 0x000783F2 File Offset: 0x000765F2
		// (set) Token: 0x060027DA RID: 10202 RVA: 0x000783FA File Offset: 0x000765FA
		public string Name { get; set; }

		// Token: 0x060027DB RID: 10203 RVA: 0x00078404 File Offset: 0x00076604
		public EventBuffer(string bufferName)
		{
			this.Name = bufferName;
			EventBuffer.verboseEntries = new EventBuffer.BaseData[this.numelem];
			EventBuffer.nonVerboseEntries = new EventBuffer.BaseData[this.numelem];
			for (int i = 0; i < this.numelem; i++)
			{
				EventBuffer.verboseEntries[i].source = null;
				EventBuffer.verboseEntries[i].id = 0;
				EventBuffer.verboseEntries[i].theevent = null;
				EventBuffer.verboseEntries[i].eventType = TraceEventType.Verbose;
				EventBuffer.verboseEntries[i].message = null;
				EventBuffer.nonVerboseEntries[i].source = null;
				EventBuffer.nonVerboseEntries[i].id = 0;
				EventBuffer.nonVerboseEntries[i].theevent = null;
				EventBuffer.nonVerboseEntries[i].eventType = TraceEventType.Information;
				EventBuffer.nonVerboseEntries[i].message = null;
			}
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x00078500 File Offset: 0x00076700
		private CircularAsynchronousBuffer.BufferType GetBufferTypeForEventType(TraceEventType traceEventType)
		{
			if (traceEventType != TraceEventType.Verbose)
			{
				return CircularAsynchronousBuffer.BufferType.B;
			}
			return CircularAsynchronousBuffer.BufferType.A;
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x0007850C File Offset: 0x0007670C
		protected override CircularAsynchronousBuffer.BufferType PickBufferToProcess(int[] indices)
		{
			int num = indices[0];
			int num2 = indices[1];
			long num3 = long.MaxValue;
			if (EventBuffer.verboseEntries[num].theevent != null)
			{
				num3 = EventBuffer.verboseEntries[num].theevent.SeqNo;
			}
			long num4 = long.MaxValue;
			if (EventBuffer.nonVerboseEntries[num2].theevent != null)
			{
				num4 = EventBuffer.nonVerboseEntries[num2].theevent.SeqNo;
			}
			if (num3 >= num4)
			{
				return CircularAsynchronousBuffer.BufferType.B;
			}
			return CircularAsynchronousBuffer.BufferType.A;
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x0007858C File Offset: 0x0007678C
		public void AddEntry(bool ensureInAndConsumed, TraceSource source, TraceEventType eventType, int id, RDEventBase evt, EventBuffer.DelegateFormatter formatter)
		{
			bool flag = true;
			CircularAsynchronousBuffer.BufferType bufferTypeForEventType = this.GetBufferTypeForEventType(eventType);
			while (flag)
			{
				flag = !base.AddEntry(bufferTypeForEventType, ensureInAndConsumed, new object[] { source, eventType, id, evt, formatter }) && ensureInAndConsumed;
				if (flag)
				{
					Thread.Sleep(500);
				}
			}
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000036A9 File Offset: 0x000018A9
		protected override void ProcessException(CircularAsynchronousBuffer.BufferType bufferType, Exception e)
		{
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000785F0 File Offset: 0x000767F0
		protected override void ProcessEntry(CircularAsynchronousBuffer.BufferType bufferType, int avail)
		{
			EventBuffer.BaseData[] baseData = this.GetBaseData(bufferType);
			if (baseData[avail].source != null)
			{
				EventBuffer.FormatStringAndParams formatStringAndParams = null;
				if (baseData[avail].message != null)
				{
					formatStringAndParams = baseData[avail].message();
				}
				if (formatStringAndParams != null && !string.IsNullOrEmpty(formatStringAndParams.FormatMessage))
				{
					if ((formatStringAndParams.Params != null) & (formatStringAndParams.Params.Length != 0))
					{
						baseData[avail].source.TraceEvent(baseData[avail].eventType, baseData[avail].id, formatStringAndParams.FormatMessage, formatStringAndParams.Params);
					}
					else
					{
						baseData[avail].source.TraceEvent(baseData[avail].eventType, baseData[avail].id, formatStringAndParams.FormatMessage);
					}
				}
				else
				{
					baseData[avail].source.TraceData(baseData[avail].eventType, baseData[avail].id, baseData[avail].theevent);
				}
				int dropped = base.GetDropped(bufferType, avail);
				if (dropped > 0)
				{
					baseData[avail].source.TraceEvent(TraceEventType.Warning, 0, "{0}: {1} events were dropped", new object[]
					{
						bufferType.ToString(),
						dropped
					});
				}
			}
			baseData[avail].source = null;
			baseData[avail].id = 0;
			baseData[avail].theevent = null;
			baseData[avail].eventType = ((bufferType == CircularAsynchronousBuffer.BufferType.A) ? TraceEventType.Verbose : TraceEventType.Information);
			baseData[avail].message = null;
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x00078794 File Offset: 0x00076994
		private EventBuffer.BaseData[] GetBaseData(CircularAsynchronousBuffer.BufferType bufferType)
		{
			if (bufferType != CircularAsynchronousBuffer.BufferType.A)
			{
				return EventBuffer.nonVerboseEntries;
			}
			return EventBuffer.verboseEntries;
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000787A4 File Offset: 0x000769A4
		protected override void FillEntry(CircularAsynchronousBuffer.BufferType bufferType, int entrynum, params object[] parameters)
		{
			EventBuffer.BaseData[] baseData = this.GetBaseData(bufferType);
			baseData[entrynum].source = (TraceSource)parameters[0];
			baseData[entrynum].eventType = (TraceEventType)parameters[1];
			baseData[entrynum].id = (int)parameters[2];
			baseData[entrynum].theevent = (RDEventBase)parameters[3];
			if (baseData[entrynum].theevent == null)
			{
				baseData[entrynum].theevent = new EmptyEvent();
			}
			if (parameters.Length == 5)
			{
				baseData[entrynum].message = (EventBuffer.DelegateFormatter)parameters[4];
				return;
			}
			baseData[entrynum].message = null;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x00078850 File Offset: 0x00076A50
		public void AddEntry(TraceSource source, TraceEventType eventType, int id, RDEventBase evt, string message, params object[] objs)
		{
			this.AddEntry(source, eventType, id, evt, () => new EventBuffer.FormatStringAndParams(message, objs));
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x0007888A File Offset: 0x00076A8A
		public void AddEntry(TraceSource source, TraceEventType eventType, int id, RDEventBase evt)
		{
			this.AddEntry(source, eventType, id, evt, new EventBuffer.DelegateFormatter(EventBuffer.NullFormatter));
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000788A4 File Offset: 0x00076AA4
		protected void AddEntry(TraceSource source, TraceEventType eventType, int id, RDEventBase evt, EventBuffer.DelegateFormatter formatter)
		{
			bool flag = eventType == TraceEventType.Critical;
			this.AddEntry(flag, source, eventType, id, evt, formatter);
			if (flag)
			{
				this.Flush(CircularAsynchronousBuffer.BufferType.A);
			}
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000189CC File Offset: 0x00016BCC
		private static EventBuffer.FormatStringAndParams NullFormatter()
		{
			return null;
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000788CE File Offset: 0x00076ACE
		private void Flush(CircularAsynchronousBuffer.BufferType bufferType)
		{
			this.AddEntry(true, null, (bufferType == CircularAsynchronousBuffer.BufferType.A) ? TraceEventType.Verbose : TraceEventType.Information, 0, null, new EventBuffer.DelegateFormatter(EventBuffer.NullFormatter));
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000788EE File Offset: 0x00076AEE
		public void Flush()
		{
			this.Flush(CircularAsynchronousBuffer.BufferType.A);
			this.Flush(CircularAsynchronousBuffer.BufferType.B);
		}

		// Token: 0x04001779 RID: 6009
		private static EventBuffer.BaseData[] verboseEntries;

		// Token: 0x0400177A RID: 6010
		private static EventBuffer.BaseData[] nonVerboseEntries;

		// Token: 0x0200047D RID: 1149
		private struct BaseData
		{
			// Token: 0x0400177C RID: 6012
			public TraceSource source;

			// Token: 0x0400177D RID: 6013
			public TraceEventType eventType;

			// Token: 0x0400177E RID: 6014
			public int id;

			// Token: 0x0400177F RID: 6015
			public RDEventBase theevent;

			// Token: 0x04001780 RID: 6016
			public EventBuffer.DelegateFormatter message;
		}

		// Token: 0x0200047E RID: 1150
		public class FormatStringAndParams
		{
			// Token: 0x060027E9 RID: 10217 RVA: 0x000788FE File Offset: 0x00076AFE
			public FormatStringAndParams(string message, object[] args)
			{
				this.FormatMessage = message;
				this.Params = args;
			}

			// Token: 0x04001781 RID: 6017
			public string FormatMessage;

			// Token: 0x04001782 RID: 6018
			public object[] Params;
		}

		// Token: 0x0200047F RID: 1151
		// (Invoke) Token: 0x060027EB RID: 10219
		public delegate EventBuffer.FormatStringAndParams DelegateFormatter();
	}
}
