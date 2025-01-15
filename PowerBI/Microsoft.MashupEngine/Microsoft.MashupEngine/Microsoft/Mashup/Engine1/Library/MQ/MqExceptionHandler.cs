using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000922 RID: 2338
	internal static class MqExceptionHandler
	{
		// Token: 0x060042B3 RID: 17075 RVA: 0x000E09B4 File Offset: 0x000DEBB4
		public static ValueException ToValueException(IEngineHost engineHost, Exception exception, IResource resource)
		{
			if (exception is ValueException)
			{
				return (ValueException)exception;
			}
			Exception ex = MqExceptionHandler.ProcessMqException(engineHost, exception, resource);
			if (ex is ValueException)
			{
				return (ValueException)ex;
			}
			return DataSourceException.NewDataSourceError<Message2>(engineHost, MqExceptionHandler.GetMessage(exception), resource, null, exception);
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x000E09F8 File Offset: 0x000DEBF8
		public static Exception ProcessMqException(IEngineHost engineHost, Exception exception, IResource resource)
		{
			MqException ex = exception as MqException;
			if (ex == null)
			{
				return DataSourceException.NewDataSourceError<Message2>(engineHost, MqExceptionHandler.GetMessage(exception), resource, null, exception);
			}
			if (ex.ReasonCode == 2035)
			{
				return DataSourceException.NewAccessAuthorizationError(engineHost, resource, exception.Message, null, exception);
			}
			if (ex.IsCustomMqClientException)
			{
				IList<RecordKeyDefinition> list = new List<RecordKeyDefinition>();
				list.Add(new RecordKeyDefinition("Message", TextValue.New(exception.Message), TypeValue.Text));
				list.Add(new RecordKeyDefinition("ReasonCode", NumberValue.New(ex.ReasonCode), TypeValue.Number));
				return DataSourceException.NewDataSourceError<Message2>(engineHost, MqExceptionHandler.GetMessage(exception), resource, list, null);
			}
			return DataSourceException.NewDataSourceError<Message2>(engineHost, MqExceptionHandler.GetMessage(exception), resource, null, ex.ExceptionValue);
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x000E0AB1 File Offset: 0x000DECB1
		private static Message2 GetMessage(Exception exception)
		{
			return DataSourceException.DataSourceMessage("MQ", exception.Message);
		}
	}
}
