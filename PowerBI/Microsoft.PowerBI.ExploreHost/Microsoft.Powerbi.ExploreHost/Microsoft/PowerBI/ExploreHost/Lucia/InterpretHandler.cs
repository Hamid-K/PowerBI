using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.TermIndex;
using Microsoft.PowerBI.Lucia.Hosting;
using Microsoft.PowerBI.Lucia.Interpret;
using Microsoft.PowerBI.NaturalLanguage.NLToDax;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000068 RID: 104
	internal sealed class InterpretHandler : IInterpretHandler
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00009040 File Offset: 0x00007240
		internal InterpretHandler(Lazy<INaturalLanguageServicesFactory> serviceFactory, INLToDaxRuntimeFactory<DesktopRequestContext, DesktopResultContext> nlToDaxRuntimeFactory, ISchemaMetadataProvider schemaMetadataProvider, IDependentSchemasProvider dependentSchemaProvider, IDataInstanceFilter dataInstanceFilter, IReadOnlyList<FeatureSwitch> featureSwitches, Func<TimeSpan, CancellationTokenSource> createTimeoutCancellationSource = null)
		{
			this.m_serviceFactory = serviceFactory;
			this.m_nlToDaxRuntimeFactory = nlToDaxRuntimeFactory;
			this.m_schemaMetadataProvider = schemaMetadataProvider;
			this.m_dependentSchemaProvider = dependentSchemaProvider;
			this.m_dataInstanceFilter = dataInstanceFilter;
			this.m_featureSwitches = featureSwitches;
			Func<TimeSpan, CancellationTokenSource> func = createTimeoutCancellationSource;
			if (createTimeoutCancellationSource == null && (func = InterpretHandler.<>c.<>9__9_0) == null)
			{
				func = (InterpretHandler.<>c.<>9__9_0 = (TimeSpan timeout) => new CancellationTokenSource(timeout));
			}
			this.m_createTimeoutCancellationSource = func;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000090E0 File Offset: 0x000072E0
		public Task<InterpretResponse<DesktopResultContext>> InterpretAsync(InterpretRequest<DesktopRequestContext> interpretRequest, IDatabaseContext databaseContext, IDataIndexContainer dataIndexContainer)
		{
			IntentContainer intent = interpretRequest.Intent;
			if (((intent != null) ? intent.DefineCalculation : null) != null)
			{
				return this.InterpretCalculationIntentAsync(interpretRequest, databaseContext, dataIndexContainer);
			}
			return this.InterpretQnaIntentAsync(interpretRequest, databaseContext, dataIndexContainer);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000910C File Offset: 0x0000730C
		private Task<InterpretResponse<DesktopResultContext>> InterpretQnaIntentAsync(InterpretRequest<DesktopRequestContext> interpretRequest, IDatabaseContext databaseContext, IDataIndexContainer dataIndexContainer)
		{
			if (interpretRequest.Options == null)
			{
				interpretRequest.Options = new InterpretRequestOptions
				{
					QueryMetadata = true,
					VisualConfiguration = true
				};
			}
			IInterpretationService interpretationService = this.m_serviceFactory.Value.CreateInterpretationService(dataIndexContainer.Index, this.m_dataInstanceFilter, this.m_schemaMetadataProvider);
			return Task.FromResult<InterpretResponse<DesktopResultContext>>(this._qnaInterpretFlow.Interpret(interpretationService, interpretRequest, databaseContext, this.m_featureSwitches.ToList<FeatureSwitch>(), this.m_dependentSchemaProvider.GetDependentSchemas()));
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00009188 File Offset: 0x00007388
		private async Task<InterpretResponse<DesktopResultContext>> InterpretCalculationIntentAsync(InterpretRequest<DesktopRequestContext> interpretRequest, IDatabaseContext databaseContext, IDataIndexContainer dataIndexContainer)
		{
			INaturalLanguageServicesFactory value = this.m_serviceFactory.Value;
			IInterpretationService interpretationService = value.CreateInterpretationService(dataIndexContainer.Index, this.m_dataInstanceFilter, this.m_schemaMetadataProvider);
			IManagementService managementService = value.CreateManagementService(FeatureSwitchProvider.Create(this.m_featureSwitches), LinguisticSchemaServicesBuilderOptions.None);
			INLToDaxRuntime<DesktopRequestContext, DesktopResultContext> inltoDaxRuntime = this.m_nlToDaxRuntimeFactory.CreateRuntime(interpretationService, managementService);
			InterpretResponse<DesktopResultContext> interpretResponse;
			using (CancellationTokenSource cancellationSource = this.m_createTimeoutCancellationSource(InterpretHandler.DefineCalculationIntentTimeout))
			{
				try
				{
					interpretResponse = await inltoDaxRuntime.InterpretAsync(interpretRequest, databaseContext, this.m_featureSwitches, cancellationSource.Token);
				}
				catch (OperationCanceledException)
				{
					interpretResponse = InterpretResponseFactory<DesktopResultContext>.Create(interpretRequest.Version, InterpretDiagnosticMessageFactory.InterpretCancelled(), null);
				}
			}
			return interpretResponse;
		}

		// Token: 0x0400013B RID: 315
		private static readonly TimeSpan DefineCalculationIntentTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400013C RID: 316
		private readonly InterpretFlow _qnaInterpretFlow = new InterpretFlow(TimeSpan.FromSeconds(30.0), TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(1.0));

		// Token: 0x0400013D RID: 317
		private readonly Lazy<INaturalLanguageServicesFactory> m_serviceFactory;

		// Token: 0x0400013E RID: 318
		private readonly INLToDaxRuntimeFactory<DesktopRequestContext, DesktopResultContext> m_nlToDaxRuntimeFactory;

		// Token: 0x0400013F RID: 319
		private readonly ISchemaMetadataProvider m_schemaMetadataProvider;

		// Token: 0x04000140 RID: 320
		private readonly IDependentSchemasProvider m_dependentSchemaProvider;

		// Token: 0x04000141 RID: 321
		private readonly IDataInstanceFilter m_dataInstanceFilter;

		// Token: 0x04000142 RID: 322
		private readonly IReadOnlyList<FeatureSwitch> m_featureSwitches;

		// Token: 0x04000143 RID: 323
		private readonly Func<TimeSpan, CancellationTokenSource> m_createTimeoutCancellationSource;
	}
}
