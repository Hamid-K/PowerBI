using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200012F RID: 303
	public class EdmStructuredValue : EdmValue, IEdmStructuredValue, IEdmValue, IEdmElement
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x00012378 File Offset: 0x00010578
		public EdmStructuredValue(IEdmStructuredTypeReference type, IEnumerable<IEdmPropertyValue> propertyValues)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmPropertyValue>>(propertyValues, "propertyValues");
			this.propertyValues = propertyValues;
			if (propertyValues != null)
			{
				int num = propertyValues.Count<IEdmPropertyValue>();
				if (num > 5)
				{
					this.propertiesDictionaryCache = new Cache<EdmStructuredValue, Dictionary<string, IEdmPropertyValue>>();
				}
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x000123B8 File Offset: 0x000105B8
		public IEnumerable<IEdmPropertyValue> PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00011F07 File Offset: 0x00010107
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Structured;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x000123C0 File Offset: 0x000105C0
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

		// Token: 0x060007CF RID: 1999 RVA: 0x000123E0 File Offset: 0x000105E0
		public IEdmPropertyValue FindPropertyValue(string propertyName)
		{
			Dictionary<string, IEdmPropertyValue> propertiesDictionary = this.PropertiesDictionary;
			if (propertiesDictionary != null)
			{
				IEdmPropertyValue edmPropertyValue;
				propertiesDictionary.TryGetValue(propertyName, out edmPropertyValue);
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

		// Token: 0x060007D0 RID: 2000 RVA: 0x00012454 File Offset: 0x00010654
		private Dictionary<string, IEdmPropertyValue> ComputePropertiesDictionary()
		{
			Dictionary<string, IEdmPropertyValue> dictionary = new Dictionary<string, IEdmPropertyValue>();
			foreach (IEdmPropertyValue edmPropertyValue in this.propertyValues)
			{
				dictionary[edmPropertyValue.Name] = edmPropertyValue;
			}
			return dictionary;
		}

		// Token: 0x04000336 RID: 822
		private readonly IEnumerable<IEdmPropertyValue> propertyValues;

		// Token: 0x04000337 RID: 823
		private readonly Cache<EdmStructuredValue, Dictionary<string, IEdmPropertyValue>> propertiesDictionaryCache;

		// Token: 0x04000338 RID: 824
		private static readonly Func<EdmStructuredValue, Dictionary<string, IEdmPropertyValue>> ComputePropertiesDictionaryFunc = (EdmStructuredValue me) => me.ComputePropertiesDictionary();
	}
}
