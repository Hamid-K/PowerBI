using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x02000045 RID: 69
	internal static class ReportParameterExtensions
	{
		// Token: 0x06000270 RID: 624 RVA: 0x00010A1C File Offset: 0x0000EC1C
		public static ReportParameterDefinition ToWebApiReportParameter(this ItemParameter itemParameter)
		{
			if (itemParameter == null)
			{
				throw new ArgumentNullException("itemParameter");
			}
			ReportParameterType reportParameterType = (ReportParameterType)Enum.Parse(typeof(ReportParameterType), itemParameter.ParameterTypeName);
			ReportParameterDefinition reportParameterDefinition = new ReportParameterDefinition();
			reportParameterDefinition.Name = itemParameter.Name;
			reportParameterDefinition.ParameterType = reportParameterType;
			reportParameterDefinition.ParameterState = (ReportParameterState)Enum.Parse(typeof(ReportParameterState), itemParameter.ParameterStateName);
			ReportParameterDefinition reportParameterDefinition2 = reportParameterDefinition;
			IEnumerable<global::Model.ValidValue> enumerable2;
			if (itemParameter.ValidValues == null)
			{
				IEnumerable<global::Model.ValidValue> enumerable = new global::Model.ValidValue[0];
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = itemParameter.ValidValues.Select((Microsoft.SqlServer.ReportingServices2010.ValidValue validValue) => validValue.ToWebApiValidValue(reportParameterType));
			}
			reportParameterDefinition2.ValidValues = enumerable2;
			reportParameterDefinition.ValidValuesIsNull = itemParameter.ValidValues == null;
			reportParameterDefinition.Nullable = itemParameter.Nullable;
			reportParameterDefinition.AllowBlank = itemParameter.AllowBlank;
			reportParameterDefinition.MultiValue = itemParameter.MultiValue;
			reportParameterDefinition.Prompt = itemParameter.Prompt;
			reportParameterDefinition.PromptUser = itemParameter.PromptUser;
			reportParameterDefinition.ParameterVisibility = ReportParameterExtensions.GetVisiblity(itemParameter.PromptUser, itemParameter.Prompt);
			reportParameterDefinition.QueryParameter = itemParameter.QueryParameter;
			reportParameterDefinition.DefaultValuesQueryBased = itemParameter.DefaultValuesQueryBased;
			reportParameterDefinition.ValidValuesQueryBased = itemParameter.ValidValuesQueryBased;
			reportParameterDefinition.Dependencies = itemParameter.Dependencies ?? new string[0];
			ReportParameterDefinition reportParameterDefinition3 = reportParameterDefinition;
			IEnumerable<string> enumerable4;
			if (itemParameter.DefaultValues == null)
			{
				IEnumerable<string> enumerable3 = new string[0];
				enumerable4 = enumerable3;
			}
			else
			{
				enumerable4 = itemParameter.DefaultValues.Select((string defaultValue) => ParameterValueExtensions.ToWebApiValue(defaultValue, reportParameterType, null));
			}
			reportParameterDefinition3.DefaultValues = enumerable4;
			reportParameterDefinition.DefaultValuesIsNull = itemParameter.DefaultValues == null;
			reportParameterDefinition.ErrorMessage = itemParameter.ErrorMessage;
			return reportParameterDefinition;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00010BB1 File Offset: 0x0000EDB1
		private static ReportParameterVisibility GetVisiblity(bool promptUser, string prompt)
		{
			if (prompt == null)
			{
				throw new ArgumentNullException("prompt");
			}
			if (!promptUser)
			{
				return ReportParameterVisibility.Internal;
			}
			if (prompt.Length <= 0)
			{
				return ReportParameterVisibility.Hidden;
			}
			return ReportParameterVisibility.Visible;
		}
	}
}
