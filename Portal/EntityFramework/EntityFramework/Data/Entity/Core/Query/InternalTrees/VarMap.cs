using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003FC RID: 1020
	internal class VarMap : Dictionary<Var, Var>
	{
		// Token: 0x06002F78 RID: 12152 RVA: 0x00095DC0 File Offset: 0x00093FC0
		internal VarMap GetReverseMap()
		{
			VarMap varMap = new VarMap();
			foreach (KeyValuePair<Var, Var> keyValuePair in this)
			{
				Var var;
				if (!varMap.TryGetValue(keyValuePair.Value, out var))
				{
					varMap[keyValuePair.Value] = keyValuePair.Key;
				}
			}
			return varMap;
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x00095E34 File Offset: 0x00094034
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			foreach (Var var in base.Keys)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}({1},{2})", new object[]
				{
					text,
					var.Id,
					base[var].Id
				});
				text = ",";
			}
			return stringBuilder.ToString();
		}
	}
}
