using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.EntitySql.AST;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200065A RID: 1626
	internal abstract class InlineFunctionInfo
	{
		// Token: 0x06004DF8 RID: 19960 RVA: 0x0011871C File Offset: 0x0011691C
		internal InlineFunctionInfo(FunctionDefinition functionDef, List<DbVariableReferenceExpression> parameters)
		{
			this.FunctionDefAst = functionDef;
			this.Parameters = parameters;
		}

		// Token: 0x06004DF9 RID: 19961
		internal abstract DbLambda GetLambda(SemanticResolver sr);

		// Token: 0x04001C45 RID: 7237
		internal readonly FunctionDefinition FunctionDefAst;

		// Token: 0x04001C46 RID: 7238
		internal readonly List<DbVariableReferenceExpression> Parameters;
	}
}
