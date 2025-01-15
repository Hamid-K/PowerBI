using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.InfoNav;
using Microsoft.PowerBI.Lucia.NLToDax;
using Microsoft.PowerBI.NaturalLanguage.NLToDax;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.Lucia.NLToDax
{
	// Token: 0x02000074 RID: 116
	internal sealed class DaxServices : IDaxServices
	{
		// Token: 0x06000332 RID: 818 RVA: 0x0000A49B File Offset: 0x0000869B
		internal DaxServices(Func<string, Task<string>> getDaxTemplate, ITracer tracer)
		{
			this.m_getDaxTemplate = getDaxTemplate;
			this.m_tracer = tracer;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000A4B4 File Offset: 0x000086B4
		public async Task<DaxTemplateResponse> GetDaxTemplateAsync(DaxTemplateRequest request, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			DaxTemplateResponse daxTemplateResponse;
			if (this.m_getDaxTemplate == null)
			{
				daxTemplateResponse = DaxServices.CreateMockResponse();
			}
			else
			{
				try
				{
					DaxTemplateRequest<DaxTemplateRequestContext> daxTemplateRequest = DaxTemplateConverter.Convert(request);
					string serializedRequest = JsonConvert.SerializeObject(daxTemplateRequest);
					daxTemplateResponse = DaxTemplateConverter.Convert(JsonConvert.DeserializeObject<DaxTemplateResponse<DaxTemplateResultContext>>(await AsyncUtils.AsCancellable<string>(() => this.m_getDaxTemplate(serializedRequest), cancellationToken)));
				}
				catch (OperationCanceledException)
				{
					this.m_tracer.TraceWarning("Operation GetDaxTemplateAsync was cancelled.");
					throw;
				}
				catch (Exception ex) when (!ex.IsStoppingException())
				{
					this.m_tracer.TraceError("Exception while attempting to perform GetDaxTemplateAsync: " + ex.ToTelemetrySafeString());
					daxTemplateResponse = new DaxTemplateResponse(new DaxTemplateDiagnosticMessage(0, null));
				}
			}
			return daxTemplateResponse;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000A507 File Offset: 0x00008707
		private static DaxTemplateResponse CreateMockResponse()
		{
			return new DaxTemplateResponse(new List<DaxTemplateResult>
			{
				new DaxTemplateResult("COUNT(@Column@1)", 0.42)
			}, Util.EmptyReadOnlyCollection<DaxTemplateDiagnosticMessage>());
		}

		// Token: 0x04000171 RID: 369
		private readonly Func<string, Task<string>> m_getDaxTemplate;

		// Token: 0x04000172 RID: 370
		private readonly ITracer m_tracer;
	}
}
