using System;
using System.Linq;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000046 RID: 70
	internal static class ReportParameterDefinitionPatchExtension
	{
		// Token: 0x06000272 RID: 626 RVA: 0x00010BD4 File Offset: 0x0000EDD4
		public static ItemParameter ToSoapItemParameter(this ReportParameterProperties reportParameterProperties, ReportParameterType reportParameterType)
		{
			return ReportParameterDefinitionPatchExtension.ToSoapItemParameter(reportParameterProperties.OverrideDefaultValues ? reportParameterProperties.DefaultValues.Select((string defaultValue) => ParameterValueExtensions.ToSoapValue(defaultValue, reportParameterType)).ToArray<string>() : null, reportParameterProperties.ParameterName, reportParameterProperties.ParameterVisibility, reportParameterProperties.Prompt);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00010C2C File Offset: 0x0000EE2C
		public static ItemParameter ToSoapItemParameter(this ReportParameterDefinitionPatch reportParameterDefinitionPatch, ReportParameterType reportParameterType)
		{
			return ReportParameterDefinitionPatchExtension.ToSoapItemParameter((reportParameterDefinitionPatch.DefaultValues == null) ? null : reportParameterDefinitionPatch.DefaultValues.Select((string defaultValue) => ParameterValueExtensions.ToSoapValue(defaultValue, reportParameterType)).ToArray<string>(), reportParameterDefinitionPatch.Name, reportParameterDefinitionPatch.ParameterVisibility, reportParameterDefinitionPatch.Prompt);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00010C84 File Offset: 0x0000EE84
		private static ItemParameter ToSoapItemParameter(string[] defaultValues, string name, ReportParameterVisibility visibility, string prompt)
		{
			ItemParameter itemParameter = new ItemParameter
			{
				Name = name,
				DefaultValues = defaultValues,
				PromptUserSpecified = true
			};
			if (visibility != ReportParameterVisibility.Visible)
			{
				if (visibility != ReportParameterVisibility.Hidden)
				{
					itemParameter.Prompt = "";
					itemParameter.PromptUser = false;
				}
				else
				{
					itemParameter.Prompt = "";
					itemParameter.PromptUser = true;
				}
			}
			else
			{
				itemParameter.Prompt = prompt;
				itemParameter.PromptUser = true;
			}
			return itemParameter;
		}
	}
}
