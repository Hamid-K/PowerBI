using System;
using System.Collections.Generic;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000037 RID: 55
	internal sealed class InvokeTypeInfo<ContainerType> : TraceLoggingTypeInfo<ContainerType>
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		public InvokeTypeInfo(TypeAnalysis typeAnalysis)
			: base(typeAnalysis.name, typeAnalysis.level, typeAnalysis.opcode, typeAnalysis.keywords, typeAnalysis.tags)
		{
			if (typeAnalysis.properties.Length != 0)
			{
				this.properties = typeAnalysis.properties;
				this.accessors = new PropertyAccessor<ContainerType>[this.properties.Length];
				for (int i = 0; i < this.accessors.Length; i++)
				{
					this.accessors[i] = PropertyAccessor<ContainerType>.Create(this.properties[i]);
				}
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000BE74 File Offset: 0x0000A074
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			if (this.properties != null)
			{
				foreach (PropertyAnalysis propertyAnalysis in this.properties)
				{
					EventFieldFormat eventFieldFormat = EventFieldFormat.Default;
					EventFieldAttribute fieldAttribute = propertyAnalysis.fieldAttribute;
					if (fieldAttribute != null)
					{
						traceLoggingMetadataCollector.Tags = fieldAttribute.Tags;
						eventFieldFormat = fieldAttribute.Format;
					}
					propertyAnalysis.typeInfo.WriteMetadata(traceLoggingMetadataCollector, propertyAnalysis.name, eventFieldFormat);
				}
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public override void WriteData(TraceLoggingDataCollector collector, ref ContainerType value)
		{
			if (this.accessors != null)
			{
				PropertyAccessor<ContainerType>[] array = this.accessors;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Write(collector, ref value);
				}
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000BF18 File Offset: 0x0000A118
		public override object GetData(object value)
		{
			if (this.properties != null)
			{
				List<string> list = new List<string>();
				List<object> list2 = new List<object>();
				for (int i = 0; i < this.properties.Length; i++)
				{
					object data = this.accessors[i].GetData((ContainerType)((object)value));
					list.Add(this.properties[i].name);
					list2.Add(this.properties[i].typeInfo.GetData(data));
				}
				return new EventPayload(list, list2);
			}
			return null;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000BF98 File Offset: 0x0000A198
		public override void WriteObjectData(TraceLoggingDataCollector collector, object valueObj)
		{
			if (this.accessors != null)
			{
				ContainerType containerType = ((valueObj == null) ? default(ContainerType) : ((ContainerType)((object)valueObj)));
				this.WriteData(collector, ref containerType);
			}
		}

		// Token: 0x040000EC RID: 236
		private readonly PropertyAnalysis[] properties;

		// Token: 0x040000ED RID: 237
		private readonly PropertyAccessor<ContainerType>[] accessors;
	}
}
