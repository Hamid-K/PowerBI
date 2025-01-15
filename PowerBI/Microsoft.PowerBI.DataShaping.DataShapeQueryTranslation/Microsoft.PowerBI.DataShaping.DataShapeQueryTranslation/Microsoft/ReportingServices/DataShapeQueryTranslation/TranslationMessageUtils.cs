using System;
using System.Globalization;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Utils;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000060 RID: 96
	internal static class TranslationMessageUtils
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000F952 File Offset: 0x0000DB52
		internal static CultureInfo FormatCulture
		{
			get
			{
				return CultureInfo.CurrentCulture;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000F959 File Offset: 0x0000DB59
		internal static string FormatMessage(string template, object[] args)
		{
			TranslationMessageUtils.ProcessMessageArgs(args);
			return string.Format(TranslationMessageUtils.FormatCulture, template, args);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000F970 File Offset: 0x0000DB70
		private static void ProcessMessageArgs(object[] args)
		{
			for (int i = 0; i < args.Length; i++)
			{
				args[i] = TranslationMessageUtils.ProcessMessageArg(args[i]);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000F998 File Offset: 0x0000DB98
		private static object ProcessMessageArg(object argVal)
		{
			Identifier identifier = argVal as Identifier;
			if (identifier != null)
			{
				argVal = TranslationMessageUtils.GetDisplayString(identifier);
			}
			TranslationMessagePhrase translationMessagePhrase = argVal as TranslationMessagePhrase;
			if (translationMessagePhrase != null)
			{
				argVal = translationMessagePhrase.FormattedString;
			}
			IContainsTelemetryMarkup containsTelemetryMarkup = argVal as IContainsTelemetryMarkup;
			if (containsTelemetryMarkup != null)
			{
				argVal = containsTelemetryMarkup.ToCustomerContentString();
			}
			return argVal;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000F9E2 File Offset: 0x0000DBE2
		internal static string GetDisplayString(Identifier identifier)
		{
			if (!(identifier == null))
			{
				return identifier.Value;
			}
			return null;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		internal static ScrubbedEntityPropertyReference GetPropertyNameForError(IConceptualProperty property)
		{
			string edmName = property.Entity.EdmName;
			string edmName2 = property.EdmName;
			return TranslationMessageUtils.GetPropertyNameForError(edmName, edmName2);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000FA1D File Offset: 0x0000DC1D
		internal static ScrubbedEntityPropertyReference GetPropertyNameForError(ResolvedPropertyExpressionNode node)
		{
			return TranslationMessageUtils.GetPropertyNameForError(node.Property);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000FA2A File Offset: 0x0000DC2A
		internal static ScrubbedEntityPropertyReference GetPropertyNameForError(string entitySetName, string propertyName)
		{
			return new ScrubbedEntityPropertyReference(Microsoft.DataShaping.StringUtil.FormatInvariant("'{0}'", new object[] { entitySetName }), propertyName, null);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000FA47 File Offset: 0x0000DC47
		internal static ScrubbedString GetEntityNameForError(string entitySetFullName)
		{
			return new ScrubbedString(entitySetFullName);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000FA4F File Offset: 0x0000DC4F
		internal static ScrubbedString MarkAsExpressionContent(this string content)
		{
			return new ScrubbedString(content);
		}
	}
}
