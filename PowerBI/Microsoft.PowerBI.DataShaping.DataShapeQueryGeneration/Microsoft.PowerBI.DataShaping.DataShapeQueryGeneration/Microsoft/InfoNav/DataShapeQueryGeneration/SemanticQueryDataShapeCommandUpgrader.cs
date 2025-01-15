using System;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000E1 RID: 225
	internal class SemanticQueryDataShapeCommandUpgrader : SemanticQueryDataShapeCommandVisitor
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x0001D226 File Offset: 0x0001B426
		private SemanticQueryDataShapeCommandUpgrader(DataShapeGenerationErrorContext errorContext, IFederatedConceptualSchema federatedSchema, ITracer tracer)
		{
			this._errorContext = errorContext;
			this._federatedSchema = federatedSchema;
			this._tracer = tracer;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001D243 File Offset: 0x0001B443
		internal static void Upgrade(DataShapeGenerationErrorContext errorContext, IFederatedConceptualSchema federatedSchema, ITracer tracer, SemanticQueryDataShapeCommand command)
		{
			new SemanticQueryDataShapeCommandUpgrader(errorContext, federatedSchema, tracer).Visit(command);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001D254 File Offset: 0x0001B454
		protected override void Visit(QueryDefinition query)
		{
			if (!QueryDefinitionUpgrader.TryUpgrade(new DataShapeGenerationErrorContextAdapter(this._errorContext, DataShapeGenerationErrorCode.CouldNotUpgradeSemanticQueryDefinition, ErrorSourceCategory.InputDoesNotMatchModel), query, this._federatedSchema, null) && !this._errorContext.HasError)
			{
				this._tracer.SanitizedTrace(TraceLevel.Error, "QueryDefinition Upgrade failed without registering an error.");
				this._errorContext.Register(DataShapeGenerationMessages.CouldNotUpgradeSemanticQueryDefinition(EngineMessageSeverity.Error));
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		protected override void Visit(DataShapeBinding binding)
		{
			if (!DataShapeBindingUpgrader.TryUpgrade(new DataShapeGenerationErrorContextAdapter(this._errorContext, DataShapeGenerationErrorCode.CouldNotUpgradeDataShapeBinding, ErrorSourceCategory.InputDoesNotMatchModel), binding, this._federatedSchema) && !this._errorContext.HasError)
			{
				this._tracer.SanitizedTrace(TraceLevel.Error, "DataShapeBinding Upgrade failed without registering an error.");
				this._errorContext.Register(DataShapeGenerationMessages.CouldNotUpgradeDataShapeBinding(EngineMessageSeverity.Error));
			}
		}

		// Token: 0x04000411 RID: 1041
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x04000412 RID: 1042
		private readonly IFederatedConceptualSchema _federatedSchema;

		// Token: 0x04000413 RID: 1043
		private readonly ITracer _tracer;
	}
}
