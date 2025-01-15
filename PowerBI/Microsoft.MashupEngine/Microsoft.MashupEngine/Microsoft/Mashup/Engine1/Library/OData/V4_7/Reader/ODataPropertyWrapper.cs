using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x02000798 RID: 1944
	internal class ODataPropertyWrapper : IODataPropertyWrapper
	{
		// Token: 0x060038FD RID: 14589 RVA: 0x000B78FE File Offset: 0x000B5AFE
		public ODataPropertyWrapper(ODataProperty property)
		{
			this.property = property;
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x000B790D File Offset: 0x000B5B0D
		public string Name
		{
			get
			{
				return this.property.Name;
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x000B791A File Offset: 0x000B5B1A
		public object Value
		{
			get
			{
				return ODataWrapperHelper.WrapValueIfNecessary(this.property.Value);
			}
		}

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000B792C File Offset: 0x000B5B2C
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

		// Token: 0x04001D66 RID: 7526
		private readonly ODataProperty property;
	}
}
