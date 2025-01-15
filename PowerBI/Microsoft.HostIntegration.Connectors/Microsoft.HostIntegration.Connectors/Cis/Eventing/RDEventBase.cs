using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x02000481 RID: 1153
	[RDEvent(64256, TraceEventType.Verbose)]
	public abstract class RDEventBase
	{
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060027F0 RID: 10224 RVA: 0x00078934 File Offset: 0x00076B34
		// (set) Token: 0x060027F1 RID: 10225 RVA: 0x0007893C File Offset: 0x00076B3C
		public DateTime InitiationTime { get; private set; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060027F2 RID: 10226 RVA: 0x00078945 File Offset: 0x00076B45
		// (set) Token: 0x060027F3 RID: 10227 RVA: 0x0007894D File Offset: 0x00076B4D
		public long SeqNo { get; private set; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060027F4 RID: 10228 RVA: 0x00078958 File Offset: 0x00076B58
		[RDEventProperty(ExcludeFromConsole = true)]
		public string OrigTS
		{
			get
			{
				return this.InitiationTime.ToString("yyyy/MM/dd HH:mm:ss.fff");
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x00078978 File Offset: 0x00076B78
		public static void MakeEventingAsynchronous()
		{
			lock (RDEventBase.EventBufferLock)
			{
				if (RDEventBase.eventBuffer == null)
				{
					RDEventBase.eventBuffer = new EventBuffer("Generic");
				}
			}
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000789C8 File Offset: 0x00076BC8
		public static void MakeEventingSynchronous()
		{
			lock (RDEventBase.EventBufferLock)
			{
				if (RDEventBase.eventBuffer != null)
				{
					EventBuffer eventBuffer = RDEventBase.eventBuffer;
					RDEventBase.eventBuffer = null;
					eventBuffer.Flush();
					eventBuffer.StopThread();
				}
			}
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x00078A24 File Offset: 0x00076C24
		public static void Flush()
		{
			lock (RDEventBase.EventBufferLock)
			{
				if (RDEventBase.eventBuffer != null)
				{
					RDEventBase.eventBuffer.Flush();
				}
			}
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x00078A70 File Offset: 0x00076C70
		private void GetEventProperties(out List<PropertyInfo> prop)
		{
			Type type = base.GetType();
			RDEventBase.EventDetails eventDetails = (RDEventBase.EventDetails)RDEventBase.EventDefaults[type];
			if (eventDetails == null)
			{
				lock (RDEventBase.EventDefaultsSyncRoot)
				{
					eventDetails = (RDEventBase.EventDetails)RDEventBase.EventDefaults[type];
					if (eventDetails == null)
					{
						eventDetails = new RDEventBase.EventDetails(TraceEventType.Verbose, 64256);
						RDEvent[] array = (RDEvent[])type.GetCustomAttributes(typeof(RDEvent), true);
						eventDetails.properties = new List<PropertyInfo>();
						if (array.Length > 0)
						{
							eventDetails.Type = array[0].RDEventType;
							eventDetails.Id = array[0].RDEventID;
						}
						foreach (PropertyInfo propertyInfo in type.GetProperties())
						{
							if (propertyInfo.GetCustomAttributes(typeof(RDEventProperty), true).Length > 0 && propertyInfo.DeclaringType != typeof(RDEventBase))
							{
								eventDetails.properties.Add(propertyInfo);
							}
						}
						RDEventBase.EventDefaults[type] = eventDetails;
					}
				}
			}
			prop = eventDetails.properties;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x00078BA4 File Offset: 0x00076DA4
		private bool GetDefaults(out TraceEventType eventType, out int id)
		{
			Type type = base.GetType();
			RDEventBase.EventDetails eventDetails = (RDEventBase.EventDetails)RDEventBase.EventDefaults[type];
			if (eventDetails == null)
			{
				lock (RDEventBase.EventDefaultsSyncRoot)
				{
					eventDetails = (RDEventBase.EventDetails)RDEventBase.EventDefaults[type];
					if (eventDetails == null)
					{
						eventDetails = new RDEventBase.EventDetails(TraceEventType.Verbose, 64256);
						RDEvent[] array = (RDEvent[])type.GetCustomAttributes(typeof(RDEvent), true);
						eventDetails.properties = new List<PropertyInfo>();
						if (array.Length > 0)
						{
							eventDetails.Type = array[0].RDEventType;
							eventDetails.Id = array[0].RDEventID;
						}
						foreach (PropertyInfo propertyInfo in type.GetProperties())
						{
							if (propertyInfo.GetCustomAttributes(typeof(RDEventProperty), true).Length > 0 && propertyInfo.DeclaringType != typeof(RDEventBase))
							{
								eventDetails.properties.Add(propertyInfo);
							}
						}
						RDEventBase.EventDefaults[type] = eventDetails;
					}
				}
			}
			eventType = eventDetails.Type;
			id = eventDetails.Id;
			return eventDetails.Id != 64256;
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x00078CF0 File Offset: 0x00076EF0
		public void TraceTo(TraceSource source)
		{
			TraceEventType traceEventType;
			int num;
			if (this.GetDefaults(out traceEventType, out num))
			{
				this.TraceTo(source, traceEventType, num);
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x00078D14 File Offset: 0x00076F14
		public void TraceTo(TraceSource source, TraceEventType eventType)
		{
			TraceEventType traceEventType;
			int num;
			if (this.GetDefaults(out traceEventType, out num))
			{
				this.TraceTo(source, eventType, num);
			}
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x00078D38 File Offset: 0x00076F38
		public void TraceTo(TraceSource source, int id)
		{
			TraceEventType traceEventType;
			int num;
			if (this.GetDefaults(out traceEventType, out num))
			{
				this.TraceTo(source, traceEventType, id);
			}
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x00078D5C File Offset: 0x00076F5C
		public void TraceTo(TraceSource source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (args.Length == 0)
			{
				MessageEvent messageEvent = new MessageEvent(format);
				messageEvent.TraceTo(source, eventType, id);
				return;
			}
			FormattedMessageEvent formattedMessageEvent = new FormattedMessageEvent(format, args);
			formattedMessageEvent.TraceTo(source, eventType, id);
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x00078D94 File Offset: 0x00076F94
		private void SetEventAttributes()
		{
			this.SeqNo = Interlocked.Increment(ref RDEventBase.EventSequenceNumber);
			this.InitiationTime = DateTime.UtcNow;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x00078DB1 File Offset: 0x00076FB1
		public void TraceTo(TraceSource source, TraceEventType eventType, int id)
		{
			this.SetEventAttributes();
			if (RDEventBase.eventBuffer == null)
			{
				source.TraceData(eventType, id, this);
				return;
			}
			RDEventBase.eventBuffer.AddEntry(source, eventType, id, this);
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x00078DD8 File Offset: 0x00076FD8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			base.GetType();
			stringBuilder.AppendFormat("OrigTS : {0}; ", (this.OrigTS == null) ? string.Empty : this.OrigTS);
			List<PropertyInfo> list = null;
			this.GetEventProperties(out list);
			if (list != null)
			{
				foreach (PropertyInfo propertyInfo in list)
				{
					stringBuilder.AppendFormat("{0} : {1}; ", propertyInfo.Name, propertyInfo.GetValue(this, null));
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001785 RID: 6021
		private static object EventDefaultsSyncRoot = new object();

		// Token: 0x04001786 RID: 6022
		private static Hashtable EventDefaults = new Hashtable();

		// Token: 0x04001787 RID: 6023
		private static object EventBufferLock = new object();

		// Token: 0x04001788 RID: 6024
		private static long EventSequenceNumber = 0L;

		// Token: 0x04001789 RID: 6025
		private static EventBuffer eventBuffer = null;

		// Token: 0x02000482 RID: 1154
		private class EventDetails
		{
			// Token: 0x06002803 RID: 10243 RVA: 0x00078EC1 File Offset: 0x000770C1
			public EventDetails(TraceEventType type, int id)
			{
				this.Type = type;
				this.Id = id;
				this.properties = null;
			}

			// Token: 0x170007E4 RID: 2020
			// (get) Token: 0x06002804 RID: 10244 RVA: 0x00078EDE File Offset: 0x000770DE
			// (set) Token: 0x06002805 RID: 10245 RVA: 0x00078EE6 File Offset: 0x000770E6
			public TraceEventType Type { get; set; }

			// Token: 0x170007E5 RID: 2021
			// (get) Token: 0x06002806 RID: 10246 RVA: 0x00078EEF File Offset: 0x000770EF
			// (set) Token: 0x06002807 RID: 10247 RVA: 0x00078EF7 File Offset: 0x000770F7
			public int Id { get; set; }

			// Token: 0x0400178C RID: 6028
			public List<PropertyInfo> properties;
		}
	}
}
