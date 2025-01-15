using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000696 RID: 1686
	internal sealed class QueryParameter : Node
	{
		// Token: 0x06004F7B RID: 20347 RVA: 0x00120854 File Offset: 0x0011EA54
		internal QueryParameter(string parameterName, string query, int inputPos)
			: base(query, inputPos)
		{
			this._name = parameterName.Substring(1);
			if (this._name.StartsWith("_", StringComparison.OrdinalIgnoreCase) || char.IsDigit(this._name, 0))
			{
				ErrorContext errCtx = base.ErrCtx;
				string text = Strings.InvalidParameterFormat(this._name);
				throw EntitySqlException.Create(errCtx, text, null);
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06004F7C RID: 20348 RVA: 0x001208B1 File Offset: 0x0011EAB1
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04001D1F RID: 7455
		private readonly string _name;
	}
}
