using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Security;
using Microsoft.Diagnostics.Contracts.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000018 RID: 24
	public class EventWrittenEventArgs : EventArgs
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00008788 File Offset: 0x00006988
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000087BF File Offset: 0x000069BF
		public string EventName
		{
			get
			{
				if (this.m_eventName != null || this.EventId < 0)
				{
					return this.m_eventName;
				}
				return this.m_eventSource.m_eventData[this.EventId].Name;
			}
			internal set
			{
				this.m_eventName = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000087C8 File Offset: 0x000069C8
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000087D0 File Offset: 0x000069D0
		public int EventId { get; internal set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000087D9 File Offset: 0x000069D9
		public Guid ActivityId
		{
			[SecurityCritical]
			get
			{
				return EventSource.CurrentThreadActivityId;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000087E0 File Offset: 0x000069E0
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000087E8 File Offset: 0x000069E8
		public Guid RelatedActivityId
		{
			[SecurityCritical]
			get;
			internal set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000087F1 File Offset: 0x000069F1
		// (set) Token: 0x060000ED RID: 237 RVA: 0x000087F9 File Offset: 0x000069F9
		public ReadOnlyCollection<object> Payload { get; internal set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00008804 File Offset: 0x00006A04
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000887E File Offset: 0x00006A7E
		public ReadOnlyCollection<string> PayloadNames
		{
			get
			{
				if (this.m_payloadNames == null)
				{
					Contract.Assert(this.EventId != -1);
					List<string> list = new List<string>();
					foreach (ParameterInfo parameterInfo in this.m_eventSource.m_eventData[this.EventId].Parameters)
					{
						list.Add(parameterInfo.Name);
					}
					this.m_payloadNames = new ReadOnlyCollection<string>(list);
				}
				return this.m_payloadNames;
			}
			internal set
			{
				this.m_payloadNames = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00008887 File Offset: 0x00006A87
		public EventSource EventSource
		{
			get
			{
				return this.m_eventSource;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000888F File Offset: 0x00006A8F
		public EventKeywords Keywords
		{
			get
			{
				if (this.EventId < 0)
				{
					return this.m_keywords;
				}
				return (EventKeywords)this.m_eventSource.m_eventData[this.EventId].Descriptor.Keywords;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000088C3 File Offset: 0x00006AC3
		public EventOpcode Opcode
		{
			get
			{
				if (this.EventId < 0)
				{
					return this.m_opcode;
				}
				return (EventOpcode)this.m_eventSource.m_eventData[this.EventId].Descriptor.Opcode;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000088F7 File Offset: 0x00006AF7
		public EventTask Task
		{
			get
			{
				if (this.EventId < 0)
				{
					return EventTask.None;
				}
				return (EventTask)this.m_eventSource.m_eventData[this.EventId].Descriptor.Task;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00008926 File Offset: 0x00006B26
		public EventTags Tags
		{
			get
			{
				if (this.EventId < 0)
				{
					return this.m_tags;
				}
				return this.m_eventSource.m_eventData[this.EventId].Tags;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00008955 File Offset: 0x00006B55
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00008984 File Offset: 0x00006B84
		public string Message
		{
			get
			{
				if (this.EventId < 0)
				{
					return this.m_message;
				}
				return this.m_eventSource.m_eventData[this.EventId].Message;
			}
			internal set
			{
				this.m_message = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000898D File Offset: 0x00006B8D
		public EventChannel Channel
		{
			get
			{
				if (this.EventId < 0)
				{
					return EventChannel.None;
				}
				return (EventChannel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Channel;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000089BC File Offset: 0x00006BBC
		public byte Version
		{
			get
			{
				if (this.EventId < 0)
				{
					return 0;
				}
				return this.m_eventSource.m_eventData[this.EventId].Descriptor.Version;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000089EB File Offset: 0x00006BEB
		public EventLevel Level
		{
			get
			{
				if (this.EventId >= this.m_eventSource.m_eventData.Length)
				{
					return EventLevel.LogAlways;
				}
				return (EventLevel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Level;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008A28 File Offset: 0x00006C28
		internal EventWrittenEventArgs(EventSource eventSource)
		{
			this.m_eventSource = eventSource;
		}

		// Token: 0x04000066 RID: 102
		private string m_message;

		// Token: 0x04000067 RID: 103
		private string m_eventName;

		// Token: 0x04000068 RID: 104
		private EventSource m_eventSource;

		// Token: 0x04000069 RID: 105
		private ReadOnlyCollection<string> m_payloadNames;

		// Token: 0x0400006A RID: 106
		internal EventTags m_tags;

		// Token: 0x0400006B RID: 107
		internal EventOpcode m_opcode;

		// Token: 0x0400006C RID: 108
		internal EventKeywords m_keywords;
	}
}
