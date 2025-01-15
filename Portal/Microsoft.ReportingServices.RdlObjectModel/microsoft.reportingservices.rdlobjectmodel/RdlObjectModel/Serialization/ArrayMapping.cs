using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002E1 RID: 737
	public class ArrayMapping : TypeMapping
	{
		// Token: 0x060016B8 RID: 5816 RVA: 0x00035B88 File Offset: 0x00033D88
		public ArrayMapping(Type type)
			: base(type)
		{
			if (type.IsArray)
			{
				this.Name = this.Type.GetElementType().Name + "Array";
				return;
			}
			if (type.IsGenericType)
			{
				Type[] genericArguments = type.GetGenericArguments();
				if (genericArguments != null && genericArguments.Length != 0)
				{
					this.Name = genericArguments[0].Name + "Collection";
				}
			}
		}

		// Token: 0x04000704 RID: 1796
		public Dictionary<string, Type> ElementTypes;

		// Token: 0x04000705 RID: 1797
		public Type ItemType;
	}
}
