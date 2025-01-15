using System;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F0D RID: 3853
	internal static class AnalysisServicesExceptions
	{
		// Token: 0x0600660B RID: 26123 RVA: 0x0015F6A4 File Offset: 0x0015D8A4
		public static ValueException NewDataSourceError(IEngineHost engineHost, AnalysisServicesException exception, IResource resource)
		{
			return DataSourceException.NewDataSourceError<Message2>(engineHost, AnalysisServicesExceptions.FormatDataSourceExceptionMessage(exception.Message), resource, null, exception);
		}

		// Token: 0x0600660C RID: 26124 RVA: 0x0015F6BA File Offset: 0x0015D8BA
		public static ValueException NewInvalidServerException(TextValue serverName, Exception innerException)
		{
			return ValueException.NewExpressionError<Message0>(Strings.AnalysisServicesInvalidServer, serverName, innerException);
		}

		// Token: 0x0600660D RID: 26125 RVA: 0x0015F6C8 File Offset: 0x0015D8C8
		public static ValueException NewInvalidDatabaseNameException(TextValue databaseName, Exception innerException)
		{
			return ValueException.NewExpressionError<Message0>(Strings.AnalysisServicesInvalidDatabaseName, databaseName, innerException);
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x0015F6D8 File Offset: 0x0015D8D8
		public static ValueException NewProviderMissingException(IEngineHost engineHost, Exception innerException, IResource resource)
		{
			string text = Strings.AnalysisServicesProviderMissing(X64Helper.Is64BitProcess ? "x64" : "x86", "ADOMD.NET 11.0", "https://www.microsoft.com/en-us/download/details.aspx?id=35580");
			return DataSourceException.NewDataSourceError<Message2>(engineHost, AnalysisServicesExceptions.FormatDataSourceExceptionMessage(text), resource, null, innerException);
		}

		// Token: 0x0600660F RID: 26127 RVA: 0x0015F71C File Offset: 0x0015D91C
		public static ValueException NewInvalidResponseFromService(IEngineHost engineHost, Exception innerException, IResource resource)
		{
			string text = Strings.AnalysisServicesInvalidServiceResponse(innerException.Message);
			return DataSourceException.NewDataSourceError<Message2>(engineHost, AnalysisServicesExceptions.FormatDataSourceExceptionMessage(text), resource, null, innerException);
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x0015F749 File Offset: 0x0015D949
		public static ValueException NewServiceVersionNotSupported(IEngineHost engineHost, IResource resource)
		{
			return DataSourceException.NewDataSourceError<Message2>(engineHost, AnalysisServicesExceptions.FormatDataSourceExceptionMessage(Strings.AnalysisServicesUnsupportedServiceVersion), resource, null, null);
		}

		// Token: 0x06006611 RID: 26129 RVA: 0x0015F763 File Offset: 0x0015D963
		public static ValueException NewUnsupportedCulture(string cultureName)
		{
			return ValueException.NewExpressionError<Message2>(AnalysisServicesExceptions.FormatDataSourceExceptionMessage(Strings.AnalysisServicesCultureNotSupported(cultureName)), Value.Null, null);
		}

		// Token: 0x06006612 RID: 26130 RVA: 0x0015F780 File Offset: 0x0015D980
		private static Message2 FormatDataSourceExceptionMessage(string message)
		{
			return DataSourceException.DataSourceMessage("AnalysisServices", message);
		}

		// Token: 0x0400380C RID: 14348
		private const string providerName = "ADOMD.NET 11.0";

		// Token: 0x0400380D RID: 14349
		private const string providerUrl = "https://www.microsoft.com/en-us/download/details.aspx?id=35580";
	}
}
