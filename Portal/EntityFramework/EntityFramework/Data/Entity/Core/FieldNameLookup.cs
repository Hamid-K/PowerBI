using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core
{
	// Token: 0x020002CC RID: 716
	internal sealed class FieldNameLookup
	{
		// Token: 0x060022A8 RID: 8872 RVA: 0x00061E24 File Offset: 0x00060024
		public FieldNameLookup(ReadOnlyCollection<string> columnNames)
		{
			int count = columnNames.Count;
			this._fieldNames = new string[count];
			for (int i = 0; i < count; i++)
			{
				this._fieldNames[i] = columnNames[i];
			}
			this.GenerateLookup();
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x00061E78 File Offset: 0x00060078
		public FieldNameLookup(IDataRecord reader)
		{
			int fieldCount = reader.FieldCount;
			this._fieldNames = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				this._fieldNames[i] = reader.GetName(i);
			}
			this.GenerateLookup();
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x00061ECA File Offset: 0x000600CA
		public int GetOrdinal(string fieldName)
		{
			Check.NotNull<string>(fieldName, "fieldName");
			int num = this.IndexOf(fieldName);
			if (num == -1)
			{
				throw new IndexOutOfRangeException(fieldName);
			}
			return num;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x00061EEC File Offset: 0x000600EC
		private int IndexOf(string fieldName)
		{
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

		// Token: 0x060022AC RID: 8876 RVA: 0x00061F24 File Offset: 0x00060124
		private int LinearIndexOf(string fieldName, CompareOptions compareOptions)
		{
			for (int i = 0; i < this._fieldNames.Length; i++)
			{
				if (CultureInfo.InvariantCulture.CompareInfo.Compare(fieldName, this._fieldNames[i], compareOptions) == 0)
				{
					this._fieldNameLookup[fieldName] = i;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x00061F70 File Offset: 0x00060170
		private void GenerateLookup()
		{
			int num = this._fieldNames.Length - 1;
			while (0 <= num)
			{
				this._fieldNameLookup[this._fieldNames[num]] = num;
				num--;
			}
		}

		// Token: 0x04000BF3 RID: 3059
		private readonly Dictionary<string, int> _fieldNameLookup = new Dictionary<string, int>();

		// Token: 0x04000BF4 RID: 3060
		private readonly string[] _fieldNames;
	}
}
