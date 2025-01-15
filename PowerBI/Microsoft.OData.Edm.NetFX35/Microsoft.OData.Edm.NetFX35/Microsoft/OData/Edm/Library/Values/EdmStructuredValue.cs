using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library.Values
{
	// Token: 0x02000208 RID: 520
	public class EdmStructuredValue : EdmValue, IEdmStructuredValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000C40 RID: 3136 RVA: 0x000228C4 File Offset: 0x00020AC4
		public EdmStructuredValue(IEdmStructuredTypeReference type, IEnumerable<IEdmPropertyValue> propertyValues)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmPropertyValue>>(propertyValues, "propertyValues");
			this.propertyValues = propertyValues;
			if (propertyValues != null)
			{
				int num = 0;
				foreach (IEdmPropertyValue edmPropertyValue in propertyValues)
				{
					num++;
					if (num > 5)
					{
						this.propertiesDictionaryCache = new Cache<EdmStructuredValue, Dictionary<string, IEdmPropertyValue>>();
						break;
					}
				}
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0002293C File Offset: 0x00020B3C
		public IEnumerable<IEdmPropertyValue> PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00022944 File Offset: 0x00020B44
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Structured;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00022948 File Offset: 0x00020B48
		private Dictionary<string, IEdmPropertyValue> PropertiesDictionary
		{
			get
			{
				if (this.propertiesDictionaryCache != null)
				{
					return this.propertiesDictionaryCache.GetValue(this, EdmStructuredValue.ComputePropertiesDictionaryFunc, null);
				}
				return null;
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00022968 File Offset: 0x00020B68
		public IEdmPropertyValue FindPropertyValue(string propertyName)
		{
			Dictionary<string, IEdmPropertyValue> propertiesDictionary = this.PropertiesDictionary;
			if (propertiesDictionary != null)
			{
				IEdmPropertyValue edmPropertyValue;
				propertiesDictionary.TryGetValue(propertyName, ref edmPropertyValue);
				return edmPropertyValue;
			}
			foreach (IEdmPropertyValue edmPropertyValue2 in this.propertyValues)
			{
				if (edmPropertyValue2.Name == propertyName)
				{
					return edmPropertyValue2;
				}
			}
			return null;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000229E0 File Offset: 0x00020BE0
		private Dictionary<string, IEdmPropertyValue> ComputePropertiesDictionary()
		{
			Dictionary<string, IEdmPropertyValue> dictionary = new Dictionary<string, IEdmPropertyValue>();
			foreach (IEdmPropertyValue edmPropertyValue in this.propertyValues)
			{
				dictionary[edmPropertyValue.Name] = edmPropertyValue;
			}
			return dictionary;
		}

		// Token: 0x04000594 RID: 1428
		private readonly IEnumerable<IEdmPropertyValue> propertyValues;

		// Token: 0x04000595 RID: 1429
		private readonly Cache<EdmStructuredValue, Dictionary<string, IEdmPropertyValue>> propertiesDictionaryCache;

		// Token: 0x04000596 RID: 1430
		private static readonly Func<EdmStructuredValue, Dictionary<string, IEdmPropertyValue>> ComputePropertiesDictionaryFunc = (EdmStructuredValue me) => me.ComputePropertiesDictionary();
	}
}
