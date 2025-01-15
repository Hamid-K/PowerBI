using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000188 RID: 392
	public static class DomainModelDiagnosticExtensions
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x0000E698 File Offset: 0x0000C898
		public static string ToFormattedString(this IReadOnlyList<DomainModelDiagnosticMessage> diagnosticMessages, bool includeCodes = false, bool yamlHeaderFormat = false)
		{
			if (diagnosticMessages.IsNullOrEmpty<DomainModelDiagnosticMessage>())
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(40 * diagnosticMessages.Count);
			string text = (yamlHeaderFormat ? "# " : string.Empty);
			if (yamlHeaderFormat)
			{
				stringBuilder.Append(text);
				stringBuilder.AppendLine("Diagnostic messages");
			}
			foreach (DomainModelDiagnosticMessage domainModelDiagnosticMessage in diagnosticMessages)
			{
				stringBuilder.Append(text);
				stringBuilder.AppendLine(domainModelDiagnosticMessage.ToString(includeCodes));
			}
			if (yamlHeaderFormat)
			{
				stringBuilder.AppendLine();
			}
			else
			{
				stringBuilder.TrimEnd();
			}
			return stringBuilder.ToString();
		}
	}
}
