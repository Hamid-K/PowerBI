using System;
using System.Reflection;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200003A RID: 58
	internal class NonGenericProperytWriter<ContainerType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000C16B File Offset: 0x0000A36B
		public NonGenericProperytWriter(PropertyAnalysis property)
		{
			this.getterInfo = property.getterInfo;
			this.typeInfo = property.typeInfo;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000C18C File Offset: 0x0000A38C
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			object obj = ((container == null) ? null : this.getterInfo.Invoke(container, null));
			this.typeInfo.WriteObjectData(collector, obj);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000C1CE File Offset: 0x0000A3CE
		public override object GetData(ContainerType container)
		{
			if (container != null)
			{
				return this.getterInfo.Invoke(container, null);
			}
			return null;
		}

		// Token: 0x040000F3 RID: 243
		private readonly TraceLoggingTypeInfo typeInfo;

		// Token: 0x040000F4 RID: 244
		private readonly MethodInfo getterInfo;
	}
}
