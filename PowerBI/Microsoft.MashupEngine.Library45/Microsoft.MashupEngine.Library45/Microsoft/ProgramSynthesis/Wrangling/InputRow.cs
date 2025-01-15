using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling
{
	// Token: 0x020000AD RID: 173
	[DataContract]
	public class InputRow : IRow, IEquatable<IRow>, IEquatable<InputRow>
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x0000DFEF File Offset: 0x0000C1EF
		[JsonConstructor]
		public InputRow(IReadOnlyDictionary<string, object> data)
		{
			this._data = data.ToImmutableDictionary<string, object>();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000E003 File Offset: 0x0000C203
		public InputRow(InputRow row)
			: this(row._data)
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000DFEF File Offset: 0x0000C1EF
		public InputRow(IEnumerable<KeyValuePair<string, object>> data)
		{
			this._data = data.ToImmutableDictionary<string, object>();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000E011 File Offset: 0x0000C211
		public InputRow(IRow row)
			: this(row.KeyValuePairs())
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000E01F File Offset: 0x0000C21F
		public InputRow(IEnumerable<object> data)
		{
			this._data = this.AddDefaultIndex(data).ToImmutableDictionary<string, object>();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000E039 File Offset: 0x0000C239
		public InputRow(params object[] data)
			: this(data)
		{
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000E042 File Offset: 0x0000C242
		public IEnumerable<string> ColumnNames
		{
			get
			{
				return this._data.Keys;
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000E04F File Offset: 0x0000C24F
		public bool TryGetValue(string columnName, out object value)
		{
			return this._data.TryGetValue(columnName, out value);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000E060 File Offset: 0x0000C260
		public bool Equals(IRow row)
		{
			InputRow inputRow = row as InputRow;
			bool flag;
			if (inputRow == null)
			{
				flag = row != null && row.ColumnNames.Count<string>() == this.ColumnNames.Count<string>() && this.ColumnNames.All(delegate(string columnName)
				{
					object obj;
					object obj2;
					return this.TryGetValue(columnName, out obj) && row.TryGetValue(columnName, out obj2) && object.Equals(obj, obj2);
				});
			}
			else
			{
				flag = this.Equals(inputRow);
			}
			return flag;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			IRow row = obj as IRow;
			return row != null && this.Equals(row);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000E10F File Offset: 0x0000C30F
		public override int GetHashCode()
		{
			return -1945990370 ^ this._data.OrderIndependentKeyValueHashCode<string, object>();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000E124 File Offset: 0x0000C324
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("{");
			int num = 0;
			foreach (KeyValuePair<string, object> keyValuePair in this._data.OrderBy((KeyValuePair<string, object> l) => l.Key))
			{
				string text;
				object obj;
				keyValuePair.Deconstruct(out text, out obj);
				string text2 = text;
				object obj2 = obj;
				if (num++ > 0)
				{
					stringBuilder.Append(", ");
				}
				string text3 = ((obj2 is string) ? string.Format("\"{0}\"", obj2) : ((obj2 != null) ? obj2.ToLiteral(null) : null));
				stringBuilder.Append(text2 + ":" + text3);
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000E20C File Offset: 0x0000C40C
		public bool Equals(InputRow other)
		{
			return this._data.DictionaryEquals(other._data, null);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000E220 File Offset: 0x0000C420
		private IEnumerable<KeyValuePair<string, object>> AddDefaultIndex(IEnumerable<object> data)
		{
			int i = 0;
			foreach (object obj in data)
			{
				yield return KVP.Create<string, object>(i.ToString(CultureInfo.InvariantCulture), obj);
				int num = i;
				i = num + 1;
			}
			IEnumerator<object> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040001C7 RID: 455
		[DataMember(Order = 0, Name = "data")]
		private readonly ImmutableDictionary<string, object> _data;
	}
}
