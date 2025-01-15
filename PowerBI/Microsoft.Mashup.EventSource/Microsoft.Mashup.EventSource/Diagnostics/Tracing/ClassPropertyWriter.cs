using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200003C RID: 60
	internal class ClassPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x0000C29A File Offset: 0x0000A49A
		public ClassPropertyWriter(PropertyAnalysis property)
		{
			this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>)property.typeInfo;
			this.getter = (ClassPropertyWriter<ContainerType, ValueType>.Getter)Statics.CreateDelegate(typeof(ClassPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			ValueType valueType = ((container == null) ? default(ValueType) : this.getter(container));
			this.valueTypeInfo.WriteData(collector, ref valueType);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C31C File Offset: 0x0000A51C
		public override object GetData(ContainerType container)
		{
			return (container == null) ? default(ValueType) : this.getter(container);
		}

		// Token: 0x040000F7 RID: 247
		private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;

		// Token: 0x040000F8 RID: 248
		private readonly ClassPropertyWriter<ContainerType, ValueType>.Getter getter;

		// Token: 0x0200008F RID: 143
		// (Invoke) Token: 0x0600031B RID: 795
		private delegate ValueType Getter(ContainerType container);
	}
}
