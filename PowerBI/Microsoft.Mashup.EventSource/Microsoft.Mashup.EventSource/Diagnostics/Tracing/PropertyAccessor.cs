using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000039 RID: 57
	internal abstract class PropertyAccessor<ContainerType>
	{
		// Token: 0x060001CB RID: 459
		public abstract void Write(TraceLoggingDataCollector collector, ref ContainerType value);

		// Token: 0x060001CC RID: 460
		public abstract object GetData(ContainerType value);

		// Token: 0x060001CD RID: 461 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		public static PropertyAccessor<ContainerType> Create(PropertyAnalysis property)
		{
			Type returnType = property.getterInfo.ReturnType;
			if (!Statics.IsValueType(typeof(ContainerType)))
			{
				if (returnType == typeof(int))
				{
					return new ClassPropertyWriter<ContainerType, int>(property);
				}
				if (returnType == typeof(long))
				{
					return new ClassPropertyWriter<ContainerType, long>(property);
				}
				if (returnType == typeof(string))
				{
					return new ClassPropertyWriter<ContainerType, string>(property);
				}
			}
			return new NonGenericProperytWriter<ContainerType>(property);
		}
	}
}
