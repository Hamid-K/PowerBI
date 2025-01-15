using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000128 RID: 296
	internal class SmiUniqueKeyProperty : SmiMetaDataProperty
	{
		// Token: 0x060016F4 RID: 5876 RVA: 0x00061F59 File Offset: 0x00060159
		internal SmiUniqueKeyProperty(IList<bool> columnIsKey)
		{
			this._columns = new ReadOnlyCollection<bool>(columnIsKey);
		}

		// Token: 0x17000943 RID: 2371
		internal bool this[int ordinal]
		{
			get
			{
				return this._columns.Count > ordinal && this._columns[ordinal];
			}
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0000BB08 File Offset: 0x00009D08
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00061F8C File Offset: 0x0006018C
		internal override string TraceString()
		{
			string text = "UniqueKey(";
			bool flag = false;
			for (int i = 0; i < this._columns.Count; i++)
			{
				if (flag)
				{
					text += ",";
				}
				else
				{
					flag = true;
				}
				if (this._columns[i])
				{
					text += i.ToString(CultureInfo.InvariantCulture);
				}
			}
			return text + ")";
		}

		// Token: 0x04000963 RID: 2403
		private readonly IList<bool> _columns;
	}
}
