using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Diagnostics.Contracts.Internal;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000071 RID: 113
	internal class TraceLoggingEventTypes
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000DAC6 File Offset: 0x0000BCC6
		internal TraceLoggingEventTypes(string name, EventTags tags, params Type[] types)
			: this(tags, name, TraceLoggingEventTypes.MakeArray(types))
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000DAD6 File Offset: 0x0000BCD6
		internal TraceLoggingEventTypes(string name, EventTags tags, params TraceLoggingTypeInfo[] typeInfos)
			: this(tags, name, TraceLoggingEventTypes.MakeArray(typeInfos))
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
		internal TraceLoggingEventTypes(string name, EventTags tags, ParameterInfo[] paramInfos)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Contract.EndContractBlock();
			this.typeInfos = this.MakeArray(paramInfos);
			this.name = name;
			this.tags = tags;
			this.level = 5;
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = new TraceLoggingMetadataCollector();
			for (int i = 0; i < this.typeInfos.Length; i++)
			{
				TraceLoggingTypeInfo traceLoggingTypeInfo = this.typeInfos[i];
				this.level = Statics.Combine((int)traceLoggingTypeInfo.Level, this.level);
				this.opcode = Statics.Combine((int)traceLoggingTypeInfo.Opcode, this.opcode);
				this.keywords |= traceLoggingTypeInfo.Keywords;
				string text = paramInfos[i].Name;
				if (Statics.ShouldOverrideFieldName(text))
				{
					text = traceLoggingTypeInfo.Name;
				}
				traceLoggingTypeInfo.WriteMetadata(traceLoggingMetadataCollector, text, EventFieldFormat.Default);
			}
			this.typeMetadata = traceLoggingMetadataCollector.GetMetadata();
			this.scratchSize = traceLoggingMetadataCollector.ScratchSize;
			this.dataCount = traceLoggingMetadataCollector.DataCount;
			this.pinCount = traceLoggingMetadataCollector.PinCount;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000DBE4 File Offset: 0x0000BDE4
		private TraceLoggingEventTypes(EventTags tags, string defaultName, TraceLoggingTypeInfo[] typeInfos)
		{
			if (defaultName == null)
			{
				throw new ArgumentNullException("defaultName");
			}
			Contract.EndContractBlock();
			this.typeInfos = typeInfos;
			this.name = defaultName;
			this.tags = tags;
			this.level = 5;
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = new TraceLoggingMetadataCollector();
			foreach (TraceLoggingTypeInfo traceLoggingTypeInfo in typeInfos)
			{
				this.level = Statics.Combine((int)traceLoggingTypeInfo.Level, this.level);
				this.opcode = Statics.Combine((int)traceLoggingTypeInfo.Opcode, this.opcode);
				this.keywords |= traceLoggingTypeInfo.Keywords;
				traceLoggingTypeInfo.WriteMetadata(traceLoggingMetadataCollector, null, EventFieldFormat.Default);
			}
			this.typeMetadata = traceLoggingMetadataCollector.GetMetadata();
			this.scratchSize = traceLoggingMetadataCollector.ScratchSize;
			this.dataCount = traceLoggingMetadataCollector.DataCount;
			this.pinCount = traceLoggingMetadataCollector.PinCount;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000DCBA File Offset: 0x0000BEBA
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000DCC2 File Offset: 0x0000BEC2
		internal EventLevel Level
		{
			get
			{
				return (EventLevel)this.level;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000DCCA File Offset: 0x0000BECA
		internal EventOpcode Opcode
		{
			get
			{
				return (EventOpcode)this.opcode;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000DCD2 File Offset: 0x0000BED2
		internal EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000DCDA File Offset: 0x0000BEDA
		internal EventTags Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		internal NameInfo GetNameInfo(string name, EventTags tags)
		{
			NameInfo nameInfo = this.nameInfos.TryGet(new KeyValuePair<string, EventTags>(name, tags));
			if (nameInfo == null)
			{
				nameInfo = this.nameInfos.GetOrAdd(new NameInfo(name, tags, this.typeMetadata.Length));
			}
			return nameInfo;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000DD24 File Offset: 0x0000BF24
		private TraceLoggingTypeInfo[] MakeArray(ParameterInfo[] paramInfos)
		{
			if (paramInfos == null)
			{
				throw new ArgumentNullException("paramInfos");
			}
			Contract.EndContractBlock();
			List<Type> list = new List<Type>(paramInfos.Length);
			TraceLoggingTypeInfo[] array = new TraceLoggingTypeInfo[paramInfos.Length];
			for (int i = 0; i < paramInfos.Length; i++)
			{
				array[i] = Statics.GetTypeInfoInstance(paramInfos[i].ParameterType, list);
			}
			return array;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000DD78 File Offset: 0x0000BF78
		private static TraceLoggingTypeInfo[] MakeArray(Type[] types)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			Contract.EndContractBlock();
			List<Type> list = new List<Type>(types.Length);
			TraceLoggingTypeInfo[] array = new TraceLoggingTypeInfo[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				array[i] = Statics.GetTypeInfoInstance(types[i], list);
			}
			return array;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000DDC5 File Offset: 0x0000BFC5
		private static TraceLoggingTypeInfo[] MakeArray(TraceLoggingTypeInfo[] typeInfos)
		{
			if (typeInfos == null)
			{
				throw new ArgumentNullException("typeInfos");
			}
			Contract.EndContractBlock();
			return (TraceLoggingTypeInfo[])typeInfos.Clone();
		}

		// Token: 0x04000139 RID: 313
		internal readonly TraceLoggingTypeInfo[] typeInfos;

		// Token: 0x0400013A RID: 314
		internal readonly string name;

		// Token: 0x0400013B RID: 315
		internal readonly EventTags tags;

		// Token: 0x0400013C RID: 316
		internal readonly byte level;

		// Token: 0x0400013D RID: 317
		internal readonly byte opcode;

		// Token: 0x0400013E RID: 318
		internal readonly EventKeywords keywords;

		// Token: 0x0400013F RID: 319
		internal readonly byte[] typeMetadata;

		// Token: 0x04000140 RID: 320
		internal readonly int scratchSize;

		// Token: 0x04000141 RID: 321
		internal readonly int dataCount;

		// Token: 0x04000142 RID: 322
		internal readonly int pinCount;

		// Token: 0x04000143 RID: 323
		private ConcurrentSet<KeyValuePair<string, EventTags>, NameInfo> nameInfos;
	}
}
