using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200003B RID: 59
	internal class StructPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		public StructPropertyWriter(PropertyAnalysis property)
		{
			this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>)property.typeInfo;
			this.getter = (StructPropertyWriter<ContainerType, ValueType>.Getter)Statics.CreateDelegate(typeof(StructPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C228 File Offset: 0x0000A428
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			ValueType valueType = ((container == null) ? default(ValueType) : this.getter(ref container));
			this.valueTypeInfo.WriteData(collector, ref valueType);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C268 File Offset: 0x0000A468
		public override object GetData(ContainerType container)
		{
			return (container == null) ? default(ValueType) : this.getter(ref container);
		}

		// Token: 0x040000F5 RID: 245
		private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;

		// Token: 0x040000F6 RID: 246
		private readonly StructPropertyWriter<ContainerType, ValueType>.Getter getter;

		// Token: 0x0200008E RID: 142
		// (Invoke) Token: 0x06000317 RID: 791
		private delegate ValueType Getter(ref ContainerType container);
	}
}
