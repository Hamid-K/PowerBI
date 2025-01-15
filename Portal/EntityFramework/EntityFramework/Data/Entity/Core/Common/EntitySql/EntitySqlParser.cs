using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200064B RID: 1611
	public sealed class EntitySqlParser
	{
		// Token: 0x06004DCB RID: 19915 RVA: 0x00117E24 File Offset: 0x00116024
		internal EntitySqlParser(Perspective perspective)
		{
			this._perspective = perspective;
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x00117E34 File Offset: 0x00116034
		public ParseResult Parse(string query, params DbParameterReferenceExpression[] parameters)
		{
			Check.NotNull<string>(query, "query");
			if (parameters != null)
			{
				IEnumerable<DbParameterReferenceExpression> enumerable = parameters;
				EntityUtil.CheckArgumentContainsNull<DbParameterReferenceExpression>(ref enumerable, "parameters");
			}
			return CqlQuery.Compile(query, this._perspective, null, parameters);
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x00117E70 File Offset: 0x00116070
		public DbLambda ParseLambda(string query, params DbVariableReferenceExpression[] variables)
		{
			Check.NotNull<string>(query, "query");
			if (variables != null)
			{
				IEnumerable<DbVariableReferenceExpression> enumerable = variables;
				EntityUtil.CheckArgumentContainsNull<DbVariableReferenceExpression>(ref enumerable, "variables");
			}
			return CqlQuery.CompileQueryCommandLambda(query, this._perspective, null, null, variables);
		}

		// Token: 0x04001C21 RID: 7201
		private readonly Perspective _perspective;
	}
}
