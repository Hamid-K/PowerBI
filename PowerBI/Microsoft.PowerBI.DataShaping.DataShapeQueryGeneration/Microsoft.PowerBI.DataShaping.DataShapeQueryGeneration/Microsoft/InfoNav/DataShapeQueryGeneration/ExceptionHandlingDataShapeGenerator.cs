using System;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000074 RID: 116
	internal class ExceptionHandlingDataShapeGenerator : IDataShapeGenerator
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x00012391 File Offset: 0x00010591
		public ExceptionHandlingDataShapeGenerator(ITracer tracer, IDumper dumper, DataShapeGenerationErrorContext errorContext, IDataShapeGenerator inner)
		{
			this._tracer = tracer;
			this._dumper = dumper;
			this._errorContext = errorContext;
			this._inner = inner;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000123B8 File Offset: 0x000105B8
		public DataShapeGenerationResult GenerateDataShapeFromCommand(DataShapeGenerationContext context, SemanticQueryDataShapeCommand command, DataReductionConfiguration dataReductionConfig = null, DataReductionConfiguration dataReductionConfigForLegacyLimits = null)
		{
			return this.ExecuteInTryCatch<DataShapeGenerationResult>(() => this._inner.GenerateDataShapeFromCommand(context, command, dataReductionConfig, dataReductionConfigForLegacyLimits), this._errorContext);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00012408 File Offset: 0x00010608
		public DataShapeGenerationResult GenerateDataShapeFromQuery(DataShapeGenerationContext context, ResolvedQueryDefinition resolvedQuery, DataShapeGenerationOptions generationOptions)
		{
			return this.ExecuteInTryCatch<DataShapeGenerationResult>(() => this._inner.GenerateDataShapeFromQuery(context, resolvedQuery, generationOptions), this._errorContext);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00012450 File Offset: 0x00010650
		private T ExecuteInTryCatch<T>(Func<T> func, DataShapeGenerationErrorContext errorContext)
		{
			T t;
			try
			{
				t = func();
			}
			catch (DataShapeEngineException ex)
			{
				string text = "DataShapeEngineException exception in DataShapeQueryGeneration";
				this._tracer.TraceSanitizedError(ex, text);
				throw;
			}
			catch (Exception ex2) when (!ex2.IsStoppingException())
			{
				string text2 = "Unexpected exception in DataShapeQueryGeneration";
				this._tracer.TraceSanitizedError(ex2, text2);
				this._dumper.Dump(text2, ex2);
				throw DataShapeGenerationException.Create(ex2);
			}
			if (errorContext.HasError)
			{
				throw DataShapeGenerationException.Create(errorContext);
			}
			if (t == null)
			{
				throw DataShapeGenerationException.Create(new NullReferenceException("DataShapeGenerationResult should have value if ErrorContext has no error."));
			}
			return t;
		}

		// Token: 0x040002AC RID: 684
		private readonly ITracer _tracer;

		// Token: 0x040002AD RID: 685
		private readonly IDumper _dumper;

		// Token: 0x040002AE RID: 686
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x040002AF RID: 687
		private readonly IDataShapeGenerator _inner;
	}
}
