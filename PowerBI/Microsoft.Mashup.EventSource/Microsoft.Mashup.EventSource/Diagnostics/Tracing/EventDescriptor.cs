using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Diagnostics.Tracing.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000011 RID: 17
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct EventDescriptor
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000027FC File Offset: 0x000009FC
		public EventDescriptor(int traceloggingId, byte level, byte opcode, long keywords)
		{
			this.m_id = 0;
			this.m_version = 0;
			this.m_channel = 0;
			this.m_traceloggingId = traceloggingId;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_task = 0;
			this.m_keywords = keywords;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002838 File Offset: 0x00000A38
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			if (id < 0)
			{
				throw new ArgumentOutOfRangeException("id", Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum", Array.Empty<object>()));
			}
			if (id > 65535)
			{
				throw new ArgumentOutOfRangeException("id", Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("ArgumentOutOfRange_NeedValidId", new object[] { 1, ushort.MaxValue }));
			}
			this.m_traceloggingId = 0;
			this.m_id = (ushort)id;
			this.m_version = version;
			this.m_channel = channel;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_keywords = keywords;
			if (task < 0)
			{
				throw new ArgumentOutOfRangeException("task", Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum", Array.Empty<object>()));
			}
			if (task > 65535)
			{
				throw new ArgumentOutOfRangeException("task", Microsoft.Diagnostics.Tracing.Internal.Environment.GetResourceString("ArgumentOutOfRange_NeedValidId", new object[] { 1, ushort.MaxValue }));
			}
			this.m_task = (ushort)task;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002933 File Offset: 0x00000B33
		public int EventId
		{
			get
			{
				return (int)this.m_id;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000293B File Offset: 0x00000B3B
		public byte Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002943 File Offset: 0x00000B43
		public byte Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000294B File Offset: 0x00000B4B
		public byte Level
		{
			get
			{
				return this.m_level;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002953 File Offset: 0x00000B53
		public byte Opcode
		{
			get
			{
				return this.m_opcode;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000295B File Offset: 0x00000B5B
		public int Task
		{
			get
			{
				return (int)this.m_task;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002963 File Offset: 0x00000B63
		public long Keywords
		{
			get
			{
				return this.m_keywords;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000296B File Offset: 0x00000B6B
		public override bool Equals(object obj)
		{
			return obj is EventDescriptor && this.Equals((EventDescriptor)obj);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002983 File Offset: 0x00000B83
		public override int GetHashCode()
		{
			return (int)(this.m_id ^ (ushort)this.m_version ^ (ushort)this.m_channel ^ (ushort)this.m_level ^ (ushort)this.m_opcode ^ this.m_task) ^ (int)this.m_keywords;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029B8 File Offset: 0x00000BB8
		public bool Equals(EventDescriptor other)
		{
			return this.m_id == other.m_id && this.m_version == other.m_version && this.m_channel == other.m_channel && this.m_level == other.m_level && this.m_opcode == other.m_opcode && this.m_task == other.m_task && this.m_keywords == other.m_keywords;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A2A File Offset: 0x00000C2A
		public static bool operator ==(EventDescriptor event1, EventDescriptor event2)
		{
			return event1.Equals(event2);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A34 File Offset: 0x00000C34
		public static bool operator !=(EventDescriptor event1, EventDescriptor event2)
		{
			return !event1.Equals(event2);
		}

		// Token: 0x04000018 RID: 24
		[FieldOffset(0)]
		private int m_traceloggingId;

		// Token: 0x04000019 RID: 25
		[FieldOffset(0)]
		private ushort m_id;

		// Token: 0x0400001A RID: 26
		[FieldOffset(2)]
		private byte m_version;

		// Token: 0x0400001B RID: 27
		[FieldOffset(3)]
		private byte m_channel;

		// Token: 0x0400001C RID: 28
		[FieldOffset(4)]
		private byte m_level;

		// Token: 0x0400001D RID: 29
		[FieldOffset(5)]
		private byte m_opcode;

		// Token: 0x0400001E RID: 30
		[FieldOffset(6)]
		private ushort m_task;

		// Token: 0x0400001F RID: 31
		[FieldOffset(8)]
		private long m_keywords;
	}
}
