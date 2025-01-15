using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003FB RID: 1019
	[DebuggerDisplay("{{{ToString()}}}")]
	internal class VarList : List<Var>
	{
		// Token: 0x06002F75 RID: 12149 RVA: 0x00095D27 File Offset: 0x00093F27
		internal VarList()
		{
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x00095D2F File Offset: 0x00093F2F
		internal VarList(IEnumerable<Var> vars)
			: base(vars)
		{
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x00095D38 File Offset: 0x00093F38
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			foreach (Var var in this)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[] { text, var.Id });
				text = ",";
			}
			return stringBuilder.ToString();
		}
	}
}
