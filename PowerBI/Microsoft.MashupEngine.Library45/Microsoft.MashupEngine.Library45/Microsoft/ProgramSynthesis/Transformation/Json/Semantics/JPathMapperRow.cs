using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Semantics
{
	// Token: 0x02001A66 RID: 6758
	public class JPathMapperRow : IRow, IEquatable<IRow>
	{
		// Token: 0x0600DEA7 RID: 56999 RVA: 0x002F4D33 File Offset: 0x002F2F33
		public JPathMapperRow(JToken root, IEnumerable<string> columnNames = null)
		{
			this._root = root;
			this.ColumnNames = columnNames;
		}

		// Token: 0x17002528 RID: 9512
		// (get) Token: 0x0600DEA8 RID: 57000 RVA: 0x002F4D49 File Offset: 0x002F2F49
		public IEnumerable<string> ColumnNames { get; }

		// Token: 0x0600DEA9 RID: 57001 RVA: 0x002F4D54 File Offset: 0x002F2F54
		public bool TryGetValue(string columnName, out object value)
		{
			JPath jpath = JPath.ParseFromHumanReadable(columnName);
			if (jpath == null)
			{
				value = null;
				return false;
			}
			JValue jvalue = this._root.SelectFirstToken(jpath) as JValue;
			value = ((jvalue != null) ? jvalue.ToString() : null);
			return value != null;
		}

		// Token: 0x0600DEAA RID: 57002 RVA: 0x002F4D94 File Offset: 0x002F2F94
		public bool Equals(IRow other)
		{
			JPathMapperRow jpathMapperRow = other as JPathMapperRow;
			return jpathMapperRow != null && object.Equals(this._root, jpathMapperRow._root) && ((this.ColumnNames == null && other.ColumnNames == null) || this.ColumnNames.SequenceEqual(other.ColumnNames));
		}

		// Token: 0x04005498 RID: 21656
		private readonly JToken _root;
	}
}
