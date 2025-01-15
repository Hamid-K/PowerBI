using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000129 RID: 297
	internal class SmiOrderProperty : SmiMetaDataProperty
	{
		// Token: 0x060016F8 RID: 5880 RVA: 0x00061FF8 File Offset: 0x000601F8
		internal SmiOrderProperty(IList<SmiOrderProperty.SmiColumnOrder> columnOrders)
		{
			this._columns = new ReadOnlyCollection<SmiOrderProperty.SmiColumnOrder>(columnOrders);
		}

		// Token: 0x17000944 RID: 2372
		internal SmiOrderProperty.SmiColumnOrder this[int ordinal]
		{
			get
			{
				if (this._columns.Count <= ordinal)
				{
					return new SmiOrderProperty.SmiColumnOrder
					{
						_order = SortOrder.Unspecified,
						_sortOrdinal = -1
					};
				}
				return this._columns[ordinal];
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0000BB08 File Offset: 0x00009D08
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00062050 File Offset: 0x00060250
		internal override string TraceString()
		{
			string text = "SortOrder(";
			bool flag = false;
			foreach (SmiOrderProperty.SmiColumnOrder smiColumnOrder in this._columns)
			{
				if (flag)
				{
					text += ",";
				}
				else
				{
					flag = true;
				}
				if (SortOrder.Unspecified != smiColumnOrder._order)
				{
					text += smiColumnOrder.TraceString();
				}
			}
			text += ")";
			return text;
		}

		// Token: 0x04000964 RID: 2404
		private readonly IList<SmiOrderProperty.SmiColumnOrder> _columns;

		// Token: 0x02000268 RID: 616
		internal struct SmiColumnOrder
		{
			// Token: 0x06001F1F RID: 7967 RVA: 0x0007F19A File Offset: 0x0007D39A
			internal string TraceString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1}", this._sortOrdinal, this._order);
			}

			// Token: 0x0400171C RID: 5916
			internal int _sortOrdinal;

			// Token: 0x0400171D RID: 5917
			internal SortOrder _order;
		}
	}
}
