using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000122 RID: 290
	public class EdmStructuredValue : EdmValue, IEdmStructuredValue, IEdmValue, IEdmElement
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00013E98 File Offset: 0x00012098
		public EdmStructuredValue(IEdmStructuredTypeReference type, IEnumerable<IEdmPropertyValue> propertyValues)
			: base(type)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmPropertyValue>>(propertyValues, "propertyValues");
			this.propertyValues = propertyValues;
			if (propertyValues != null)
			{
				int num = Enumerable.Count<IEdmPropertyValue>(propertyValues);
				if (num > 5)
				{
					this.propertiesDictionaryCache = new Cache<EdmStructuredValue, Dictionary<string, IEdmPropertyValue>>();
				}
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00013ED8 File Offset: 0x000120D8
		public IEnumerable<IEdmPropertyValue> PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00013A23 File Offset: 0x00011C23
		public override EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.Structured;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00013EE0 File Offset: 0x000120E0
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

		// Token: 0x0600078F RID: 1935 RVA: 0x00013F00 File Offset: 0x00012100
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

		// Token: 0x06000790 RID: 1936 RVA: 0x00013F74 File Offset: 0x00012174
		private Dictionary<string, IEdmPropertyValue> ComputePropertiesDictionary()
		{
			Dictionary<string, IEdmPropertyValue> dictionary = new Dictionary<string, IEdmPropertyValue>();
			foreach (IEdmPropertyValue edmPropertyValue in this.propertyValues)
			{
				dictionary[edmPropertyValue.Name] = edmPropertyValue;
			}
			return dictionary;
		}

		// Token: 0x04000431 RID: 1073
		private readonly IEnumerable<IEdmPropertyValue> propertyValues;

		// Token: 0x04000432 RID: 1074
		private readonly Cache<EdmStructuredValue, Dictionary<string, IEdmPropertyValue>> propertiesDictionaryCache;

		// Token: 0x04000433 RID: 1075
		private static readonly Func<EdmStructuredValue, Dictionary<string, IEdmPropertyValue>> ComputePropertiesDictionaryFunc = (EdmStructuredValue me) => me.ComputePropertiesDictionary();
	}
}
