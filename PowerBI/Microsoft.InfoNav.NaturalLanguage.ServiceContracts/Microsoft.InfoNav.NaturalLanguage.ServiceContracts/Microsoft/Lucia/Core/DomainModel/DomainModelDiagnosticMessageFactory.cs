using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Lucia.Diagnostics;
using Newtonsoft.Json;
using YamlDotNet.Core;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x02000183 RID: 387
	internal static class DomainModelDiagnosticMessageFactory
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x0000E14C File Offset: 0x0000C34C
		public static DomainModelDiagnosticMessage InternalErrorTimedOut()
		{
			return DomainModelDiagnosticMessageFactory.CreateFromString(DiagnosticSeverity.Error, DomainModelDiagnosticCode.InternalError, "The operation timed out.", default(DomainModelSchemaLocation));
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000E170 File Offset: 0x0000C370
		public static DomainModelDiagnosticMessage InternalErrorNoInfo()
		{
			return DomainModelDiagnosticMessageFactory.CreateFromString(DiagnosticSeverity.Error, DomainModelDiagnosticCode.InternalError, "Received invalid domain model with no additional information.", default(DomainModelSchemaLocation));
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0000E194 File Offset: 0x0000C394
		public static DomainModelDiagnosticMessage ConceptualSchemaDeserializationError(string message, string location)
		{
			return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Error, DomainModelDiagnosticCode.ConceptualSchemaDeserializationError, FormattableStringFactory.Create("{0:ccon} {1}", new object[] { message, location }), default(DomainModelSchemaLocation));
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0000E1CC File Offset: 0x0000C3CC
		public static DomainModelDiagnosticMessage LinguisticSchemaDeserializationErrorYaml(YamlException exception)
		{
			return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaDeserializationError, FormattableStringFactory.Create("{0:ccon}", new object[] { exception.Message }), (exception.Start != null) ? new DomainModelSchemaLocation(exception.Start.Line, exception.Start.Column) : DomainModelSchemaLocation.Empty);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0000E223 File Offset: 0x0000C423
		public static DomainModelDiagnosticMessage LinguisticSchemaDeserializationErrorJson(JsonReaderException exception)
		{
			return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaDeserializationError, FormattableStringFactory.Create("{0:ccon}", new object[] { exception.Message }), new DomainModelSchemaLocation(exception.LineNumber, exception.LinePosition));
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000E256 File Offset: 0x0000C456
		public static DomainModelDiagnosticMessage LinguisticSchemaDeserializationErrorXml(XmlException exception)
		{
			return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaDeserializationError, FormattableStringFactory.Create("{0:ccon}", new object[] { exception.Message }), new DomainModelSchemaLocation(exception.LineNumber, exception.LinePosition));
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000E28C File Offset: 0x0000C48C
		public static DomainModelDiagnosticMessage LinguisticSchemaDeserializationError(Exception exception)
		{
			return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaDeserializationError, FormattableStringFactory.Create("{0:ccon}", new object[] { exception.Message }), default(DomainModelSchemaLocation));
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		public static DomainModelDiagnosticMessage LinguisticSchemaDeserializationErrorInvalidContentType(string contentType)
		{
			return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaDeserializationError, FormattableStringFactory.Create("Invalid content type '{0}' for linguistic schema.", new object[] { contentType }), default(DomainModelSchemaLocation));
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		public static DomainModelDiagnosticMessage LinguisticSchemaVersionNotSupported()
		{
			return DomainModelDiagnosticMessageFactory.CreateFromString(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaVersionNotSupported, "The specified version of linguistic schema is not supported.", default(DomainModelSchemaLocation));
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0000E31C File Offset: 0x0000C51C
		public static DomainModelDiagnosticMessage LinguisticSchemaUpgradeError(FormattableString message)
		{
			return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaUpgradeError, message, default(DomainModelSchemaLocation));
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0000E33C File Offset: 0x0000C53C
		public static DomainModelDiagnosticMessage LinguisticSchemaUpgradeInternalError()
		{
			return DomainModelDiagnosticMessageFactory.CreateFromString(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaUpgradeError, "Internal error in linguistic schema upgrade.", default(DomainModelSchemaLocation));
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000E360 File Offset: 0x0000C560
		public static DomainModelDiagnosticMessage LinguisticSchemaUpgradeFromIncorrectXmlVersionError()
		{
			return DomainModelDiagnosticMessageFactory.CreateFromString(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaUpgradeError, "To upgrade the linguistic schema from XML to JSON format, it must be in the latest version.", default(DomainModelSchemaLocation));
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000E384 File Offset: 0x0000C584
		public static DomainModelDiagnosticMessage LinguisticSchemaUpgradeWarning(FormattableString message)
		{
			return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Warning, DomainModelDiagnosticCode.LinguisticSchemaUpgradeWarning, message, default(DomainModelSchemaLocation));
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public static DomainModelDiagnosticMessage LinguisticSchemaSyntacticValidationErrorJson(string message, string path, [Nullable] IJsonLineInfo lineInfo)
		{
			if (lineInfo != null)
			{
				return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaSyntacticValidationError, FormattableStringFactory.Create("{0:ccon} Path '{1:ccon}', line {2}, position {3}.", new object[] { message, path, lineInfo.LineNumber, lineInfo.LinePosition }), new DomainModelSchemaLocation(lineInfo.LineNumber, lineInfo.LinePosition));
			}
			return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaSyntacticValidationError, FormattableStringFactory.Create("{0:ccon} Path '{1:ccon}'", new object[] { message, path }), default(DomainModelSchemaLocation));
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000E42C File Offset: 0x0000C62C
		internal static DomainModelDiagnosticMessage LinguisticSchemaSyntacticValidationErrorXml(ValidationEventArgs e)
		{
			XmlSchemaException exception = e.Exception;
			if (exception != null && exception.LineNumber > 0)
			{
				return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaSyntacticValidationError, FormattableStringFactory.Create("{0:ccon} Line {1}, position {2}.", new object[]
				{
					e.Message,
					e.Exception.LineNumber,
					e.Exception.LinePosition
				}), new DomainModelSchemaLocation(e.Exception.LineNumber, e.Exception.LinePosition));
			}
			return DomainModelDiagnosticMessageFactory.Create(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaSyntacticValidationError, FormattableStringFactory.Create("{0:ccon}", new object[] { e.Message }), default(DomainModelSchemaLocation));
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000E4DC File Offset: 0x0000C6DC
		public static DomainModelDiagnosticMessage LinguisticSchemaSyntacticValidationErrorMissingRootElement()
		{
			return DomainModelDiagnosticMessageFactory.CreateFromString(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaSyntacticValidationError, "Missing root element.", default(DomainModelSchemaLocation));
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0000E500 File Offset: 0x0000C700
		public static DomainModelDiagnosticMessage LinguisticSchemaLanguageNotSupported()
		{
			return DomainModelDiagnosticMessageFactory.CreateFromString(DiagnosticSeverity.Warning, DomainModelDiagnosticCode.LinguisticSchemaLanguageNotSupported, "The specified language in the linguistic schema is not supported in this environment so Q&A will not be available.", default(DomainModelSchemaLocation));
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000E524 File Offset: 0x0000C724
		public static DomainModelDiagnosticMessage LinguisticSchemaNotAvailable()
		{
			return DomainModelDiagnosticMessageFactory.CreateFromString(DiagnosticSeverity.Error, DomainModelDiagnosticCode.LinguisticSchemaNotAvailable, "Linguistic schema was not available.", default(DomainModelSchemaLocation));
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0000E548 File Offset: 0x0000C748
		public static DomainModelDiagnosticMessage LinguisticSchemaServicesWarning(string message)
		{
			return DomainModelDiagnosticMessageFactory.CreateFromString(DiagnosticSeverity.Warning, DomainModelDiagnosticCode.LinguisticSchemaServicesWarning, message, default(DomainModelSchemaLocation));
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000E567 File Offset: 0x0000C767
		private static DomainModelDiagnosticMessage Create(DiagnosticSeverity severity, DomainModelDiagnosticCode code, FormattableString message, DomainModelSchemaLocation location = default(DomainModelSchemaLocation))
		{
			return new DomainModelDiagnosticMessage(severity, code, message, location);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0000E572 File Offset: 0x0000C772
		private static DomainModelDiagnosticMessage CreateFromString(DiagnosticSeverity severity, DomainModelDiagnosticCode code, string message, DomainModelSchemaLocation location = default(DomainModelSchemaLocation))
		{
			return new DomainModelDiagnosticMessage(severity, code, message, location);
		}
	}
}
