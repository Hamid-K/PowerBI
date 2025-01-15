using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.TableToJson
{
	// Token: 0x02001A7A RID: 6778
	[JsonObject(MemberSerialization.OptIn)]
	public class Table : Table<string>, IEquatable<Table>
	{
		// Token: 0x0600DF1F RID: 57119 RVA: 0x002F5AF1 File Offset: 0x002F3CF1
		[JsonConstructor]
		public Table(IEnumerable<string> columnNames, IEnumerable<IEnumerable<string>> rows)
			: base(columnNames, rows, null)
		{
		}

		// Token: 0x0600DF20 RID: 57120 RVA: 0x002F5AFC File Offset: 0x002F3CFC
		public bool Equals(Table other)
		{
			return other != null && (this == other || (object.Equals(base.ColumnNames, other.ColumnNames) && object.Equals(base.Rows, other.Rows)));
		}

		// Token: 0x0600DF21 RID: 57121 RVA: 0x002F5B30 File Offset: 0x002F3D30
		public JArray ToJArray()
		{
			if (this._array != null)
			{
				return this._array;
			}
			List<JObject> list = new List<JObject>(base.Rows.Count<IEnumerable<string>>());
			foreach (IEnumerable<string> enumerable in base.Rows)
			{
				if (base.ColumnNames.Count<string>() != enumerable.Count<string>())
				{
					throw new InvalidOperationException("Row data does not match column");
				}
				list.Add(new JObject(from tup in base.ColumnNames.ZipWith(enumerable)
					select new JProperty(tup.Item1, tup.Item2)));
			}
			this._array = new JArray(list);
			return this._array;
		}

		// Token: 0x0600DF22 RID: 57122 RVA: 0x002F5C04 File Offset: 0x002F3E04
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Table)obj)));
		}

		// Token: 0x0600DF23 RID: 57123 RVA: 0x002F5C32 File Offset: 0x002F3E32
		public override int GetHashCode()
		{
			IEnumerable<string> columnNames = base.ColumnNames;
			int num = ((columnNames != null) ? columnNames.GetHashCode() : 0) * 9938801;
			IEnumerable<IEnumerable<string>> rows = base.Rows;
			return num ^ ((rows != null) ? rows.GetHashCode() : 0);
		}

		// Token: 0x040054AE RID: 21678
		private JArray _array;
	}
}
