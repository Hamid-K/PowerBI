using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000035 RID: 53
	public struct EventSourceOptions
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000BAAE File Offset: 0x00009CAE
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000BAB6 File Offset: 0x00009CB6
		public EventLevel Level
		{
			get
			{
				return (EventLevel)this.level;
			}
			set
			{
				this.level = checked((byte)value);
				this.valuesSet |= 4;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000BACF File Offset: 0x00009CCF
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000BAD7 File Offset: 0x00009CD7
		public EventOpcode Opcode
		{
			get
			{
				return (EventOpcode)this.opcode;
			}
			set
			{
				this.opcode = checked((byte)value);
				this.valuesSet |= 8;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		internal bool IsOpcodeSet
		{
			get
			{
				return (this.valuesSet & 8) > 0;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000BAFD File Offset: 0x00009CFD
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000BB05 File Offset: 0x00009D05
		public EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
			set
			{
				this.keywords = value;
				this.valuesSet |= 1;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000BB1D File Offset: 0x00009D1D
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000BB25 File Offset: 0x00009D25
		public EventTags Tags
		{
			get
			{
				return this.tags;
			}
			set
			{
				this.tags = value;
				this.valuesSet |= 2;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000BB3D File Offset: 0x00009D3D
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000BB45 File Offset: 0x00009D45
		public EventActivityOptions ActivityOptions
		{
			get
			{
				return this.activityOptions;
			}
			set
			{
				this.activityOptions = value;
				this.valuesSet |= 16;
			}
		}

		// Token: 0x040000DA RID: 218
		internal EventKeywords keywords;

		// Token: 0x040000DB RID: 219
		internal EventTags tags;

		// Token: 0x040000DC RID: 220
		internal EventActivityOptions activityOptions;

		// Token: 0x040000DD RID: 221
		internal byte level;

		// Token: 0x040000DE RID: 222
		internal byte opcode;

		// Token: 0x040000DF RID: 223
		internal byte valuesSet;

		// Token: 0x040000E0 RID: 224
		internal const byte keywordsSet = 1;

		// Token: 0x040000E1 RID: 225
		internal const byte tagsSet = 2;

		// Token: 0x040000E2 RID: 226
		internal const byte levelSet = 4;

		// Token: 0x040000E3 RID: 227
		internal const byte opcodeSet = 8;

		// Token: 0x040000E4 RID: 228
		internal const byte activityOptionsSet = 16;
	}
}
