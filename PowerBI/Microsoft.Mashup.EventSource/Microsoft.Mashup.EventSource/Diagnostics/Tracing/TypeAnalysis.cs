using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000075 RID: 117
	internal sealed class TypeAnalysis
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000E378 File Offset: 0x0000C578
		public TypeAnalysis(Type dataType, EventDataAttribute eventAttrib, List<Type> recursionCheck)
		{
			IEnumerable<PropertyInfo> enumerable = Statics.GetProperties(dataType);
			List<PropertyAnalysis> list = new List<PropertyAnalysis>();
			foreach (PropertyInfo propertyInfo in enumerable)
			{
				if (!Statics.HasCustomAttribute(propertyInfo, typeof(EventIgnoreAttribute)) && propertyInfo.CanRead && propertyInfo.GetIndexParameters().Length == 0)
				{
					MethodInfo getMethod = Statics.GetGetMethod(propertyInfo);
					if (!(getMethod == null) && !getMethod.IsStatic && getMethod.IsPublic)
					{
						TraceLoggingTypeInfo typeInfoInstance = Statics.GetTypeInfoInstance(propertyInfo.PropertyType, recursionCheck);
						EventFieldAttribute customAttribute = Statics.GetCustomAttribute<EventFieldAttribute>(propertyInfo);
						string text = ((customAttribute != null && customAttribute.Name != null) ? customAttribute.Name : (Statics.ShouldOverrideFieldName(propertyInfo.Name) ? typeInfoInstance.Name : propertyInfo.Name));
						list.Add(new PropertyAnalysis(text, getMethod, typeInfoInstance, customAttribute));
					}
				}
			}
			this.properties = list.ToArray();
			PropertyAnalysis[] array = this.properties;
			for (int i = 0; i < array.Length; i++)
			{
				TraceLoggingTypeInfo typeInfo = array[i].typeInfo;
				this.level = (EventLevel)Statics.Combine((int)typeInfo.Level, (int)this.level);
				this.opcode = (EventOpcode)Statics.Combine((int)typeInfo.Opcode, (int)this.opcode);
				this.keywords |= typeInfo.Keywords;
				this.tags |= typeInfo.Tags;
			}
			if (eventAttrib != null)
			{
				this.level = (EventLevel)Statics.Combine((int)eventAttrib.Level, (int)this.level);
				this.opcode = (EventOpcode)Statics.Combine((int)eventAttrib.Opcode, (int)this.opcode);
				this.keywords |= eventAttrib.Keywords;
				this.tags |= eventAttrib.Tags;
				this.name = eventAttrib.Name;
			}
			if (this.name == null)
			{
				this.name = dataType.Name;
			}
		}

		// Token: 0x0400014F RID: 335
		internal readonly PropertyAnalysis[] properties;

		// Token: 0x04000150 RID: 336
		internal readonly string name;

		// Token: 0x04000151 RID: 337
		internal readonly EventKeywords keywords;

		// Token: 0x04000152 RID: 338
		internal readonly EventLevel level = (EventLevel)(-1);

		// Token: 0x04000153 RID: 339
		internal readonly EventOpcode opcode = (EventOpcode)(-1);

		// Token: 0x04000154 RID: 340
		internal readonly EventTags tags;
	}
}
