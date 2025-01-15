using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000948 RID: 2376
	public abstract class Message
	{
		// Token: 0x060043AA RID: 17322 RVA: 0x000E45D0 File Offset: 0x000E27D0
		static Message()
		{
			Type type = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.Message");
			Message.CcsidInfo = type.GetProperty("Ccsid");
			Message.CorrelatorInfo = type.GetProperty("Correlator");
			Message.DataInfo = type.GetProperty("Data");
			Message.ExpirationIntervalInfo = type.GetProperty("ExpirationInterval");
			Message.FormatInfo = type.GetProperty("Format");
			Message.GroupIdInfo = type.GetProperty("GroupId");
			Message.HeadersInfo = type.GetProperty("Headers");
			Message.IdentityContextInfo = type.GetProperty("IdentityContext");
			Message.MessageIdInfo = type.GetProperty("MessageId");
			Message.MessageTypeInfo = type.GetProperty("MessageType");
			Message.OffsetInfo = type.GetProperty("Offset");
			Message.OriginContextInfo = type.GetProperty("OriginContext");
			Message.PersistenceInfo = type.GetProperty("Persistence");
			Message.PriorityInfo = type.GetProperty("Priority");
			Message.ReplyToQueueInfo = type.GetProperty("ReplyToQueue");
			Message.ReplyToQueueManagerInfo = type.GetProperty("ReplyToQueueManager");
			Message.SequenceNumberInfo = type.GetProperty("SequenceNumber");
			Message.TotalHeaderLengthInfo = type.GetProperty("TotalHeaderLength");
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x000E4773 File Offset: 0x000E2973
		// (set) Token: 0x060043AC RID: 17324 RVA: 0x000E477B File Offset: 0x000E297B
		public object RealValue { get; protected set; }

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x000E4784 File Offset: 0x000E2984
		// (set) Token: 0x060043AE RID: 17326 RVA: 0x000E479C File Offset: 0x000E299C
		public int Ccsid
		{
			get
			{
				return (int)Message.CcsidInfo.GetValue(this.RealValue, null);
			}
			set
			{
				Message.CcsidInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x000E47B5 File Offset: 0x000E29B5
		// (set) Token: 0x060043B0 RID: 17328 RVA: 0x000E47CD File Offset: 0x000E29CD
		public byte[] Correlator
		{
			get
			{
				return (byte[])Message.CorrelatorInfo.GetValue(this.RealValue, null);
			}
			set
			{
				Message.CorrelatorInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x060043B1 RID: 17329 RVA: 0x000E47E1 File Offset: 0x000E29E1
		// (set) Token: 0x060043B2 RID: 17330 RVA: 0x000E47F9 File Offset: 0x000E29F9
		public byte[] Data
		{
			get
			{
				return (byte[])Message.DataInfo.GetValue(this.RealValue, null);
			}
			set
			{
				Message.DataInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x000E480D File Offset: 0x000E2A0D
		// (set) Token: 0x060043B4 RID: 17332 RVA: 0x000E4825 File Offset: 0x000E2A25
		public int ExpirationInterval
		{
			get
			{
				return (int)Message.ExpirationIntervalInfo.GetValue(this.RealValue, null);
			}
			set
			{
				Message.ExpirationIntervalInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x060043B5 RID: 17333 RVA: 0x000E483E File Offset: 0x000E2A3E
		// (set) Token: 0x060043B6 RID: 17334 RVA: 0x000E4856 File Offset: 0x000E2A56
		public string Format
		{
			get
			{
				return (string)Message.FormatInfo.GetValue(this.RealValue, null);
			}
			set
			{
				Message.FormatInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x060043B7 RID: 17335 RVA: 0x000E486A File Offset: 0x000E2A6A
		public byte[] GroupId
		{
			get
			{
				return (byte[])Message.GroupIdInfo.GetValue(this.RealValue, null);
			}
		}

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x060043B8 RID: 17336 RVA: 0x000E4882 File Offset: 0x000E2A82
		// (set) Token: 0x060043B9 RID: 17337 RVA: 0x000E489A File Offset: 0x000E2A9A
		public IdentityContext IdentityContext
		{
			get
			{
				return new IdentityContext(Message.IdentityContextInfo.GetValue(this.RealValue, null));
			}
			set
			{
				Message.IdentityContextInfo.SetValue(this.RealValue, value.RealValue, null);
			}
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x060043BA RID: 17338 RVA: 0x000E48B3 File Offset: 0x000E2AB3
		// (set) Token: 0x060043BB RID: 17339 RVA: 0x000E48CB File Offset: 0x000E2ACB
		public byte[] MessageId
		{
			get
			{
				return (byte[])Message.MessageIdInfo.GetValue(this.RealValue, null);
			}
			set
			{
				Message.MessageIdInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x060043BC RID: 17340 RVA: 0x000E48E0 File Offset: 0x000E2AE0
		public Value MessageTypeString
		{
			get
			{
				MessageType messageType = (MessageType)Message.MessageTypeInfo.GetValue(this.RealValue, null);
				string text;
				if (Message.messageTypeStrings.TryGetValue(messageType, out text))
				{
					return TextValue.New(text);
				}
				return Value.Null;
			}
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x060043BD RID: 17341 RVA: 0x000E491F File Offset: 0x000E2B1F
		// (set) Token: 0x060043BE RID: 17342 RVA: 0x000E4937 File Offset: 0x000E2B37
		public MessageType MessageType
		{
			get
			{
				return (MessageType)Message.MessageTypeInfo.GetValue(this.RealValue, null);
			}
			set
			{
				Message.MessageTypeInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x000E4950 File Offset: 0x000E2B50
		// (set) Token: 0x060043C0 RID: 17344 RVA: 0x000E4968 File Offset: 0x000E2B68
		public int Offset
		{
			get
			{
				return (int)Message.OffsetInfo.GetValue(this.RealValue, null);
			}
			set
			{
				Message.OffsetInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x000E4981 File Offset: 0x000E2B81
		// (set) Token: 0x060043C2 RID: 17346 RVA: 0x000E4999 File Offset: 0x000E2B99
		public OriginContext OriginContext
		{
			get
			{
				return new OriginContext(Message.OriginContextInfo.GetValue(this.RealValue, null));
			}
			set
			{
				Message.OriginContextInfo.SetValue(this.RealValue, value.RealValue, null);
			}
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x060043C3 RID: 17347 RVA: 0x000E49B4 File Offset: 0x000E2BB4
		public string PersistenceString
		{
			get
			{
				int num = (int)Message.PersistenceInfo.GetValue(this.RealValue, null);
				string text;
				if (Message.persistenceStrings.TryGetValue(num, out text))
				{
					return text;
				}
				return null;
			}
		}

		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x060043C4 RID: 17348 RVA: 0x000E49EA File Offset: 0x000E2BEA
		public int PriorityInt
		{
			get
			{
				return (int)Message.PriorityInfo.GetValue(this.RealValue, null);
			}
		}

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x060043C5 RID: 17349 RVA: 0x000E4A02 File Offset: 0x000E2C02
		// (set) Token: 0x060043C6 RID: 17350 RVA: 0x000E4A1A File Offset: 0x000E2C1A
		public string ReplyToQueue
		{
			get
			{
				return (string)Message.ReplyToQueueInfo.GetValue(this.RealValue, null);
			}
			set
			{
				Message.ReplyToQueueInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x060043C7 RID: 17351 RVA: 0x000E4A2E File Offset: 0x000E2C2E
		// (set) Token: 0x060043C8 RID: 17352 RVA: 0x000E4A46 File Offset: 0x000E2C46
		public string ReplyToQueueManager
		{
			get
			{
				return (string)Message.ReplyToQueueManagerInfo.GetValue(this.RealValue, null);
			}
			set
			{
				Message.ReplyToQueueManagerInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x060043C9 RID: 17353 RVA: 0x000E4A5A File Offset: 0x000E2C5A
		public int SequenceNumber
		{
			get
			{
				return (int)Message.SequenceNumberInfo.GetValue(this.RealValue, null);
			}
		}

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x060043CA RID: 17354 RVA: 0x000E4A74 File Offset: 0x000E2C74
		public List<MqHeader> Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new List<MqHeader>();
					foreach (object obj in ((IEnumerable<object>)Message.HeadersInfo.GetValue(this.RealValue, null)))
					{
						if (obj.GetType() == CicsBridgeHeader.RealType)
						{
							this.headers.Add(new CicsBridgeHeader(obj));
						}
						else if (obj.GetType() == RulesAndFormattingHeader.RealType)
						{
							this.headers.Add(new RulesAndFormattingHeader(obj));
						}
					}
				}
				return this.headers;
			}
		}

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x060043CB RID: 17355 RVA: 0x000E4B30 File Offset: 0x000E2D30
		public int TotalHeaderLength
		{
			get
			{
				return (int)Message.TotalHeaderLengthInfo.GetValue(this.RealValue, null);
			}
		}

		// Token: 0x040023B0 RID: 9136
		public static readonly PropertyInfo CcsidInfo;

		// Token: 0x040023B1 RID: 9137
		public static readonly PropertyInfo CorrelatorInfo;

		// Token: 0x040023B2 RID: 9138
		public static readonly PropertyInfo DataInfo;

		// Token: 0x040023B3 RID: 9139
		public static readonly PropertyInfo ExpirationIntervalInfo;

		// Token: 0x040023B4 RID: 9140
		public static readonly PropertyInfo FormatInfo;

		// Token: 0x040023B5 RID: 9141
		public static readonly PropertyInfo GroupIdInfo;

		// Token: 0x040023B6 RID: 9142
		public static readonly PropertyInfo HeadersInfo;

		// Token: 0x040023B7 RID: 9143
		public static readonly PropertyInfo IdentityContextInfo;

		// Token: 0x040023B8 RID: 9144
		public static readonly PropertyInfo MessageIdInfo;

		// Token: 0x040023B9 RID: 9145
		public static readonly PropertyInfo MessageTypeInfo;

		// Token: 0x040023BA RID: 9146
		public static readonly PropertyInfo OffsetInfo;

		// Token: 0x040023BB RID: 9147
		public static readonly PropertyInfo OriginContextInfo;

		// Token: 0x040023BC RID: 9148
		public static readonly PropertyInfo PersistenceInfo;

		// Token: 0x040023BD RID: 9149
		public static readonly PropertyInfo PriorityInfo;

		// Token: 0x040023BE RID: 9150
		public static readonly PropertyInfo ReplyToQueueInfo;

		// Token: 0x040023BF RID: 9151
		public static readonly PropertyInfo ReplyToQueueManagerInfo;

		// Token: 0x040023C0 RID: 9152
		public static readonly PropertyInfo SequenceNumberInfo;

		// Token: 0x040023C1 RID: 9153
		public static readonly PropertyInfo TotalHeaderLengthInfo;

		// Token: 0x040023C2 RID: 9154
		private static readonly Dictionary<int, string> persistenceStrings = new Dictionary<int, string>
		{
			{ -1, "AsParent" },
			{ 0, "None" },
			{ 1, "Persistent" },
			{ 2, "AsQueueDefinition" }
		};

		// Token: 0x040023C3 RID: 9155
		private static readonly Dictionary<MessageType, string> messageTypeStrings = new Dictionary<MessageType, string>
		{
			{
				MessageType.Datagram,
				"Datagram"
			},
			{
				MessageType.Reply,
				"Reply"
			},
			{
				MessageType.Request,
				"Request"
			}
		};

		// Token: 0x040023C4 RID: 9156
		private List<MqHeader> headers;
	}
}
