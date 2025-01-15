using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000086 RID: 134
	internal sealed class DictionaryEncodingHandler
	{
		// Token: 0x06000320 RID: 800 RVA: 0x000090CE File Offset: 0x000072CE
		internal DictionaryEncodingHandler()
		{
			this._valueDictionaries = new Dictionary<string, List<object>>(StringComparer.Ordinal);
			this._parsedValueDictionaries = new Dictionary<string, List<object>>(StringComparer.Ordinal);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000090F6 File Offset: 0x000072F6
		internal void AddValues(string dictId, List<object> values)
		{
			this._valueDictionaries.Add(dictId, values);
			this._parsedValueDictionaries.Add(dictId, new List<object>(values.Count));
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000911C File Offset: 0x0000731C
		internal bool TryHandleValue(CalculationMetadata calcMetadata, object encodedCalcValue, Func<CalculationMetadata, object, object> parseValue, out object parsedValue)
		{
			parsedValue = null;
			if (calcMetadata.DictionaryId == null || !(encodedCalcValue is long))
			{
				return false;
			}
			int num = (int)((long)encodedCalcValue);
			if (this.TryGetParsedValue(calcMetadata.DictionaryId, num, out parsedValue))
			{
				return true;
			}
			object obj = this._valueDictionaries[calcMetadata.DictionaryId][num];
			parsedValue = parseValue(calcMetadata, obj);
			this.AddParsedValue(calcMetadata.DictionaryId, num, parsedValue);
			return true;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00009198 File Offset: 0x00007398
		private bool TryGetParsedValue(string dictId, int dictIndex, out object parsedValue)
		{
			parsedValue = null;
			List<object> list = this._parsedValueDictionaries[dictId];
			if (list.Count <= dictIndex)
			{
				return false;
			}
			parsedValue = list[dictIndex];
			return true;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000091CA File Offset: 0x000073CA
		private void AddParsedValue(string dictId, int dictIndex, object parsedValue)
		{
			this._parsedValueDictionaries[dictId].Add(parsedValue);
		}

		// Token: 0x040001BC RID: 444
		private Dictionary<string, List<object>> _valueDictionaries;

		// Token: 0x040001BD RID: 445
		private Dictionary<string, List<object>> _parsedValueDictionaries;
	}
}
