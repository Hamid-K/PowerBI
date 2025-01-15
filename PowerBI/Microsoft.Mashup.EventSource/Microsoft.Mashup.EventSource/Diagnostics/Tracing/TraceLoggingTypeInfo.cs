using System;
using Microsoft.Diagnostics.Contracts.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000073 RID: 115
	internal abstract class TraceLoggingTypeInfo
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		internal TraceLoggingTypeInfo(Type dataType)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			Contract.EndContractBlock();
			this.name = dataType.Name;
			this.dataType = dataType;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E210 File Offset: 0x0000C410
		internal TraceLoggingTypeInfo(Type dataType, string name, EventLevel level, EventOpcode opcode, EventKeywords keywords, EventTags tags)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			if (name == null)
			{
				throw new ArgumentNullException("eventName");
			}
			Contract.EndContractBlock();
			Statics.CheckName(name);
			this.name = name;
			this.keywords = keywords;
			this.level = level;
			this.opcode = opcode;
			this.tags = tags;
			this.dataType = dataType;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000E28B File Offset: 0x0000C48B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000E293 File Offset: 0x0000C493
		public EventLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000E29B File Offset: 0x0000C49B
		public EventOpcode Opcode
		{
			get
			{
				return this.opcode;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000E2A3 File Offset: 0x0000C4A3
		public EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000E2AB File Offset: 0x0000C4AB
		public EventTags Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000E2B3 File Offset: 0x0000C4B3
		internal Type DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x060002D8 RID: 728
		public abstract void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format);

		// Token: 0x060002D9 RID: 729
		public abstract void WriteObjectData(TraceLoggingDataCollector collector, object value);

		// Token: 0x060002DA RID: 730 RVA: 0x0000E2BB File Offset: 0x0000C4BB
		public virtual object GetData(object value)
		{
			return value;
		}

		// Token: 0x04000148 RID: 328
		private readonly string name;

		// Token: 0x04000149 RID: 329
		private readonly EventKeywords keywords;

		// Token: 0x0400014A RID: 330
		private readonly EventLevel level = (EventLevel)(-1);

		// Token: 0x0400014B RID: 331
		private readonly EventOpcode opcode = (EventOpcode)(-1);

		// Token: 0x0400014C RID: 332
		private readonly EventTags tags;

		// Token: 0x0400014D RID: 333
		private readonly Type dataType;
	}
}
