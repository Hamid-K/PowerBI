using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Data.Common;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000166 RID: 358
	internal sealed class FieldNameLookup
	{
		// Token: 0x06001A9E RID: 6814 RVA: 0x0006D119 File Offset: 0x0006B319
		public FieldNameLookup(string[] fieldNames, int defaultLocaleID)
		{
			this._defaultLocaleID = defaultLocaleID;
			if (fieldNames == null)
			{
				throw ADP.ArgumentNull("fieldNames");
			}
			this._fieldNames = fieldNames;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0006D140 File Offset: 0x0006B340
		public FieldNameLookup(IDataReader reader, int defaultLocaleID)
		{
			this._defaultLocaleID = defaultLocaleID;
			string[] array = new string[reader.FieldCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = reader.GetName(i);
			}
			this._fieldNames = array;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0006D188 File Offset: 0x0006B388
		public int GetOrdinal(string fieldName)
		{
			if (fieldName == null)
			{
				throw ADP.ArgumentNull("fieldName");
			}
			int num = this.IndexOf(fieldName);
			if (num == -1)
			{
				throw ADP.IndexOutOfRange(fieldName);
			}
			return num;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0006D1B8 File Offset: 0x0006B3B8
		private int IndexOf(string fieldName)
		{
			if (this._fieldNameLookup == null)
			{
				this.GenerateLookup();
			}
			int num;
			if (!this._fieldNameLookup.TryGetValue(fieldName, out num))
			{
				num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase);
				if (num == -1)
				{
					num = this.LinearIndexOf(fieldName, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
				}
			}
			return num;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0006D1FB File Offset: 0x0006B3FB
		private CompareInfo GetCompareInfo()
		{
			if (this._defaultLocaleID != -1)
			{
				return CompareInfo.GetCompareInfo(this._defaultLocaleID);
			}
			return CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0006D21C File Offset: 0x0006B41C
		private int LinearIndexOf(string fieldName, CompareOptions compareOptions)
		{
			if (this._compareInfo == null)
			{
				this._compareInfo = this.GetCompareInfo();
			}
			for (int i = 0; i < this._fieldNames.Length; i++)
			{
				if (this._compareInfo.Compare(fieldName, this._fieldNames[i], compareOptions) == 0)
				{
					this._fieldNameLookup[fieldName] = i;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0006D278 File Offset: 0x0006B478
		private void GenerateLookup()
		{
			int num = this._fieldNames.Length;
			Dictionary<string, int> dictionary = new Dictionary<string, int>(num);
			int num2 = num - 1;
			while (0 <= num2)
			{
				string text = this._fieldNames[num2];
				dictionary[text] = num2;
				num2--;
			}
			this._fieldNameLookup = dictionary;
		}

		// Token: 0x04000AE1 RID: 2785
		private readonly string[] _fieldNames;

		// Token: 0x04000AE2 RID: 2786
		private readonly int _defaultLocaleID;

		// Token: 0x04000AE3 RID: 2787
		private Dictionary<string, int> _fieldNameLookup;

		// Token: 0x04000AE4 RID: 2788
		private CompareInfo _compareInfo;
	}
}
