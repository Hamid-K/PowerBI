using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200002E RID: 46
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public class EventDataAttribute : Attribute
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000B4E3 File Offset: 0x000096E3
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000B4EB File Offset: 0x000096EB
		public string Name { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000B4F4 File Offset: 0x000096F4
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000B4FC File Offset: 0x000096FC
		internal EventLevel Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000B505 File Offset: 0x00009705
		// (set) Token: 0x0600017A RID: 378 RVA: 0x0000B50D File Offset: 0x0000970D
		internal EventOpcode Opcode
		{
			get
			{
				return this.opcode;
			}
			set
			{
				this.opcode = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000B516 File Offset: 0x00009716
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000B51E File Offset: 0x0000971E
		internal EventKeywords Keywords { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000B527 File Offset: 0x00009727
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000B52F File Offset: 0x0000972F
		internal EventTags Tags { get; set; }

		// Token: 0x040000C0 RID: 192
		private EventLevel level = (EventLevel)(-1);

		// Token: 0x040000C1 RID: 193
		private EventOpcode opcode = (EventOpcode)(-1);
	}
}
