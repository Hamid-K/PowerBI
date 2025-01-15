using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200012A RID: 298
	internal class SmiDefaultFieldsProperty : SmiMetaDataProperty
	{
		// Token: 0x060016FC RID: 5884 RVA: 0x000620D8 File Offset: 0x000602D8
		internal SmiDefaultFieldsProperty(IList<bool> defaultFields)
		{
			this._defaults = new ReadOnlyCollection<bool>(defaultFields);
		}

		// Token: 0x17000945 RID: 2373
		internal bool this[int ordinal]
		{
			get
			{
				return this._defaults.Count > ordinal && this._defaults[ordinal];
			}
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0000BB08 File Offset: 0x00009D08
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0006210C File Offset: 0x0006030C
		internal override string TraceString()
		{
			string text = "DefaultFields(";
			bool flag = false;
			for (int i = 0; i < this._defaults.Count; i++)
			{
				if (flag)
				{
					text += ",";
				}
				else
				{
					flag = true;
				}
				if (this._defaults[i])
				{
					text += i.ToString();
				}
			}
			return text + ")";
		}

		// Token: 0x04000965 RID: 2405
		private readonly IList<bool> _defaults;
	}
}
