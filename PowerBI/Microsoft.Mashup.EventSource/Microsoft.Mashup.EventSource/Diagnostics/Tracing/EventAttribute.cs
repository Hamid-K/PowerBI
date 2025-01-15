using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class EventAttribute : Attribute
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00008A72 File Offset: 0x00006C72
		public EventAttribute(int eventId)
		{
			this.EventId = eventId;
			this.Level = EventLevel.Informational;
			this.m_opcodeSet = false;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00008A8F File Offset: 0x00006C8F
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00008A97 File Offset: 0x00006C97
		public int EventId { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00008AA0 File Offset: 0x00006CA0
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00008AA8 File Offset: 0x00006CA8
		public EventLevel Level { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00008AB1 File Offset: 0x00006CB1
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00008AB9 File Offset: 0x00006CB9
		public EventKeywords Keywords { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00008AC2 File Offset: 0x00006CC2
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00008ACA File Offset: 0x00006CCA
		public EventOpcode Opcode
		{
			get
			{
				return this.m_opcode;
			}
			set
			{
				this.m_opcode = value;
				this.m_opcodeSet = true;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00008ADA File Offset: 0x00006CDA
		internal bool IsOpcodeSet
		{
			get
			{
				return this.m_opcodeSet;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00008AE2 File Offset: 0x00006CE2
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00008AEA File Offset: 0x00006CEA
		public EventTask Task { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00008AF3 File Offset: 0x00006CF3
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00008AFB File Offset: 0x00006CFB
		public EventChannel Channel { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00008B04 File Offset: 0x00006D04
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00008B0C File Offset: 0x00006D0C
		public byte Version { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00008B15 File Offset: 0x00006D15
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00008B1D File Offset: 0x00006D1D
		public string Message { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00008B26 File Offset: 0x00006D26
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00008B2E File Offset: 0x00006D2E
		public EventTags Tags { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00008B37 File Offset: 0x00006D37
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00008B3F File Offset: 0x00006D3F
		public EventActivityOptions ActivityOptions { get; set; }

		// Token: 0x04000079 RID: 121
		private EventOpcode m_opcode;

		// Token: 0x0400007A RID: 122
		private bool m_opcodeSet;
	}
}
