using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000871 RID: 2161
	internal class ODataPropertyWrapper : IODataPropertyWrapper
	{
		// Token: 0x06003E39 RID: 15929 RVA: 0x000CB6C3 File Offset: 0x000C98C3
		public ODataPropertyWrapper(ODataProperty property)
		{
			this.property = property;
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x000CB6D2 File Offset: 0x000C98D2
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x06003E3B RID: 15931 RVA: 0x000CB6DF File Offset: 0x000C98DF
		public object Value
		{
			get
			{
				return ODataWrapperHelper.WrapValueIfNecessary(this.property.Value);
			}
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x000CB6F4 File Offset: 0x000C98F4
		public RecordValue Annotations
		{
			get
			{
				List<Value> list = new List<Value>();
				List<string> list2 = new List<string>();
				foreach (ODataInstanceAnnotation odataInstanceAnnotation in this.property.InstanceAnnotations)
				{
					ODataPrimitiveValue odataPrimitiveValue = odataInstanceAnnotation.Value as ODataPrimitiveValue;
					if (odataPrimitiveValue != null)
					{
						list2.Add(odataInstanceAnnotation.Name);
						Value value = ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(odataPrimitiveValue.Value, TypeValue.Any, new Func<object, Value>(ODataTypeServices.MarshalFromClr), null);
						list.Add(value);
					}
				}
				return RecordValue.New(Keys.New(list2.ToArray()), list.ToArray());
			}
		}

		// Token: 0x040020BB RID: 8379
		private readonly ODataProperty property;
	}
}
