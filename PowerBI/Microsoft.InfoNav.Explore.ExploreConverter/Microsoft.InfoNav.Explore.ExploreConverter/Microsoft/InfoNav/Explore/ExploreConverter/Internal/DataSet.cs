using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000061 RID: 97
	internal sealed class DataSet
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0000B72A File Offset: 0x0000992A
		internal DataSet(string name, Query query, List<Field> fields)
		{
			this._name = name;
			this._query = query;
			this._fields = fields;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000B747 File Offset: 0x00009947
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000B74F File Offset: 0x0000994F
		public List<Field> Fields
		{
			get
			{
				return this._fields;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000B757 File Offset: 0x00009957
		public Query Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000B760 File Offset: 0x00009960
		public Field FindField(string fieldName)
		{
			return this._fields.FirstOrDefault((Field field) => field.Name == fieldName);
		}

		// Token: 0x04000166 RID: 358
		private readonly string _name;

		// Token: 0x04000167 RID: 359
		private readonly Query _query;

		// Token: 0x04000168 RID: 360
		private readonly List<Field> _fields;
	}
}
