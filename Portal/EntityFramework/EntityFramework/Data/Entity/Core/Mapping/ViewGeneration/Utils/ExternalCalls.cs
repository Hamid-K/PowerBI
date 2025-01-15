using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Utils
{
	// Token: 0x02000573 RID: 1395
	internal static class ExternalCalls
	{
		// Token: 0x060043C1 RID: 17345 RVA: 0x000EC436 File Offset: 0x000EA636
		internal static bool IsReservedKeyword(string name)
		{
			return CqlLexer.IsReservedKeyword(name);
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x000EC440 File Offset: 0x000EA640
		internal static DbCommandTree CompileView(string viewDef, StorageMappingItemCollection mappingItemCollection, ParserOptions.CompilationMode compilationMode)
		{
			Perspective perspective = new TargetPerspective(mappingItemCollection.Workspace);
			return CqlQuery.Compile(viewDef, perspective, new ParserOptions
			{
				ParserCompilationMode = compilationMode
			}, null).CommandTree;
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x000EC474 File Offset: 0x000EA674
		internal static DbExpression CompileFunctionView(string viewDef, StorageMappingItemCollection mappingItemCollection, ParserOptions.CompilationMode compilationMode, IEnumerable<DbParameterReferenceExpression> parameters)
		{
			Perspective perspective = new TargetPerspective(mappingItemCollection.Workspace);
			ParserOptions parserOptions = new ParserOptions();
			parserOptions.ParserCompilationMode = compilationMode;
			return CqlQuery.CompileQueryCommandLambda(viewDef, perspective, parserOptions, null, parameters.Select((DbParameterReferenceExpression pInfo) => pInfo.ResultType.Variable(pInfo.ParameterName))).Invoke(parameters);
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x000EC4D0 File Offset: 0x000EA6D0
		internal static DbLambda CompileFunctionDefinition(string functionDefinition, IList<FunctionParameter> functionParameters, EdmItemCollection edmItemCollection)
		{
			ModelPerspective modelPerspective = new ModelPerspective(new MetadataWorkspace(() => edmItemCollection, () => null, () => null));
			return CqlQuery.CompileQueryCommandLambda(functionDefinition, modelPerspective, null, null, functionParameters.Select((FunctionParameter pInfo) => pInfo.TypeUsage.Variable(pInfo.Name)));
		}
	}
}
