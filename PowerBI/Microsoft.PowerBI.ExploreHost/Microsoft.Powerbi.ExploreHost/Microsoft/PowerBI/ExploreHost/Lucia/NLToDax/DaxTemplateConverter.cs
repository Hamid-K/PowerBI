using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.PowerBI.Lucia.NLToDax;
using Microsoft.PowerBI.NaturalLanguage.NLToDax;

namespace Microsoft.PowerBI.ExploreHost.Lucia.NLToDax
{
	// Token: 0x02000075 RID: 117
	internal static class DaxTemplateConverter
	{
		// Token: 0x06000335 RID: 821 RVA: 0x0000A531 File Offset: 0x00008731
		internal static DaxTemplateRequest<DaxTemplateRequestContext> Convert(DaxTemplateRequest request)
		{
			return new DaxTemplateRequest<DaxTemplateRequestContext>
			{
				Context = new DaxTemplateRequestContext(),
				Version = DaxTemplateVersion.V202107,
				Query = request.Query
			};
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000A55C File Offset: 0x0000875C
		internal static DaxTemplateResponse Convert(DaxTemplateResponse<DaxTemplateResultContext> response)
		{
			if (response.Results.IsNullOrEmpty<DaxTemplateResult<DaxTemplateResultContext>>())
			{
				return new DaxTemplateResponse(Util.EmptyReadOnlyCollection<DaxTemplateResult>(), DaxTemplateConverter.Convert(response.DiagnosticMessages));
			}
			List<DaxTemplateResult> list = new List<DaxTemplateResult>(response.Results.Count);
			foreach (DaxTemplateResult<DaxTemplateResultContext> daxTemplateResult in response.Results)
			{
				list.Add(new DaxTemplateResult(daxTemplateResult.Template, daxTemplateResult.Score));
			}
			return new DaxTemplateResponse(list, DaxTemplateConverter.Convert(response.DiagnosticMessages));
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000A604 File Offset: 0x00008804
		private static IReadOnlyList<DaxTemplateDiagnosticMessage> Convert(IList<DaxTemplateDiagnosticMessage> messages)
		{
			if (messages.IsNullOrEmptyCollection<DaxTemplateDiagnosticMessage>())
			{
				return Util.EmptyReadOnlyCollection<DaxTemplateDiagnosticMessage>();
			}
			List<DaxTemplateDiagnosticMessage> list = new List<DaxTemplateDiagnosticMessage>(messages.Count);
			foreach (DaxTemplateDiagnosticMessage daxTemplateDiagnosticMessage in messages)
			{
				if (daxTemplateDiagnosticMessage != null)
				{
					DaxTemplateDiagnosticMessage daxTemplateDiagnosticMessage2 = new DaxTemplateDiagnosticMessage(DaxTemplateConverter.Convert(daxTemplateDiagnosticMessage.Code), daxTemplateDiagnosticMessage.Message);
					list.Add(daxTemplateDiagnosticMessage2);
				}
			}
			return list;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000A684 File Offset: 0x00008884
		private static DaxTemplateDiagnosticCode Convert(DaxTemplateDiagnosticCode code)
		{
			switch (code)
			{
			case 0:
				return 0;
			case 1:
				return 1;
			case 2:
				return 2;
			default:
				return 0;
			}
		}
	}
}
