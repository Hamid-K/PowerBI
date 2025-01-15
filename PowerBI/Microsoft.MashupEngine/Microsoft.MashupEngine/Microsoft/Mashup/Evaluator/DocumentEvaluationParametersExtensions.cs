using System;
using System.Globalization;
using System.Threading;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C8C RID: 7308
	public static class DocumentEvaluationParametersExtensions
	{
		// Token: 0x0600B5E0 RID: 46560 RVA: 0x0024EDAE File Offset: 0x0024CFAE
		public static void SetUiCulture(this DocumentEvaluationParameters parameters)
		{
			if (parameters.uiCulture != null && parameters.uiCulture != CultureInfo.CurrentUICulture.Name)
			{
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(parameters.uiCulture);
			}
		}
	}
}
