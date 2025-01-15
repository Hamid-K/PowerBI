using System;
using System.Net;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BE1 RID: 3041
	internal static class ExchangeExceptions
	{
		// Token: 0x060052E5 RID: 21221 RVA: 0x0011815C File Offset: 0x0011635C
		public static ValueException NewAutodiscoverServiceFailedException(IEngineHost engineHost, string details, IResource resource)
		{
			Message2 message = ExchangeExceptions.CreateDataSourceExceptionMessage(Strings.Resource_AutoDiscoverService_Failed);
			return DataSourceException.NewDataSourceError<Message2>(engineHost, message, resource, "Details", TextValue.NewOrNull(details), TypeValue.Text, null);
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x00118194 File Offset: 0x00116394
		public static ValueException NewInvalidEmailAddressException(IEngineHost engineHost, string emailAddress, IResource resource)
		{
			Message2 message = ExchangeExceptions.CreateDataSourceExceptionMessage(Strings.Resource_ExchangeCredental_Invalid);
			return DataSourceException.NewDataSourceError<Message2>(engineHost, message, resource, "Email", TextValue.New(emailAddress), TypeValue.Text, null);
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x001181CC File Offset: 0x001163CC
		public static ValueException NewExchangeVersionNotSupportedException(IEngineHost engineHost, ServiceVersionException exception, IResource resource)
		{
			Message2 message = ExchangeExceptions.CreateDataSourceExceptionMessage(Strings.Resource_ExchangeServerVersion_NotSupported);
			return DataSourceException.NewDataSourceError<Message2>(engineHost, message, resource, null, exception);
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x001181F3 File Offset: 0x001163F3
		public static ValueException NewExchangeDeserializationException(IEngineHost engineHost, Exception exception, IResource resource)
		{
			return DataSourceException.NewDataSourceError<Message0>(engineHost, Strings.ExchangeDeserializationException, resource, "Message", TextValue.New(exception.Message), TypeValue.Text, exception);
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x00118218 File Offset: 0x00116418
		public static ValueException NewExchangeServiceResponseException(IEngineHost engineHost, ServiceResponseException exception, IResource resource)
		{
			ServerBusyException ex = exception as ServerBusyException;
			Value value = ((ex == null) ? Value.Null : RecordValue.New(new NamedValue[]
			{
				new NamedValue("BackOffSeconds", NumberValue.New(ex.BackOffMilliseconds / 1000))
			}));
			return DataSourceException.NewDataSourceError<Message2>(engineHost, Strings.DataSourceExceptionMessage(exception.ErrorCode, exception.Message), resource, "Details", value, value.Type, exception);
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x00118290 File Offset: 0x00116490
		public static Exception NewExchangeServiceRequestException(IEngineHost engineHost, ServiceRequestException e, IResource resource)
		{
			if (e.InnerException != null)
			{
				WebException ex = e.InnerException as WebException;
				if (ex != null)
				{
					HttpServices.ThrowIfAuthorizationError(engineHost, ex, resource);
				}
			}
			return DataSourceException.NewDataSourceError(engineHost, e.Message, resource, null, e);
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x001182CB File Offset: 0x001164CB
		private static Message2 CreateDataSourceExceptionMessage(string message)
		{
			return DataSourceException.DataSourceMessage("Exchange", message);
		}

		// Token: 0x04002DBE RID: 11710
		public const string DetailsKey = "Details";

		// Token: 0x04002DBF RID: 11711
		public const string EmailKey = "Email";

		// Token: 0x04002DC0 RID: 11712
		public const string MessageKey = "Message";
	}
}
