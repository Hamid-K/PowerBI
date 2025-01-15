using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004CD RID: 1229
	public class SapBwModuleHelper
	{
		// Token: 0x06002839 RID: 10297 RVA: 0x00076DF3 File Offset: 0x00074FF3
		public SapBwModuleHelper(IEngineHost host, IResource resource)
		{
			this.engineHost = host;
			if (host != null)
			{
				this.tracer = new Tracer(host, "Engine/IO/SapBwProvider/", resource, null, null);
			}
			this.resource = resource;
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00076E20 File Offset: 0x00075020
		public void Trace(string method, Action<IHostTrace> action)
		{
			Tracer tracer = this.tracer;
			if (tracer != null && tracer.Enabled)
			{
				this.tracer.Trace(method, action);
			}
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x00076E43 File Offset: 0x00075043
		public Exception NewDataSourceError(string message)
		{
			return DataSourceException.NewDataSourceError(this.engineHost, message, this.resource, null, null);
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x00076E5C File Offset: 0x0007505C
		public Exception NewSapBwError(string message, Dictionary<string, string> exceptionDetails, string lookupString, string additionalInfo)
		{
			IList<RecordKeyDefinition> list = new List<RecordKeyDefinition>();
			foreach (KeyValuePair<string, string> keyValuePair in exceptionDetails)
			{
				list.Add(new RecordKeyDefinition(keyValuePair.Key, TextValue.New(keyValuePair.Value), TypeValue.Text));
			}
			IEngineHost engineHost = this.engineHost;
			IUserMessage userMessage2;
			if (string.IsNullOrEmpty(additionalInfo))
			{
				IUserMessage userMessage = Strings.SapBwBapiExecutionError(message, lookupString);
				userMessage2 = userMessage;
			}
			else
			{
				IUserMessage userMessage = Strings.SapBwBapiExecutionErrorAdditionalInfo(message, lookupString, additionalInfo);
				userMessage2 = userMessage;
			}
			return DataSourceException.NewDataSourceError<IUserMessage>(engineHost, userMessage2, this.resource, list, null);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x00076F08 File Offset: 0x00075108
		public bool TryWrapException(Exception exception, out Exception wrappedException)
		{
			if (!SafeExceptions.IsSafeException(exception) || exception is ValueException)
			{
				wrappedException = null;
				return false;
			}
			wrappedException = this.WrapException(exception);
			return true;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x00076F2C File Offset: 0x0007512C
		public Exception WrapException(Exception exception)
		{
			if (exception is ResourceAccessAuthorizationException || exception is ValueException)
			{
				return exception;
			}
			string text;
			if (SapBwModuleHelper.TryGetString(exception.Data, "ExceptionKind", out text) && text == "Authorization")
			{
				return DataSourceException.NewAccessAuthorizationError(this.engineHost, this.resource, exception.Message, null, exception);
			}
			IList<RecordKeyDefinition> list = new List<RecordKeyDefinition>();
			list.Add(new RecordKeyDefinition("Message", TextValue.New(exception.Message), TypeValue.Text));
			foreach (object obj in exception.Data)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text2 = dictionaryEntry.Key as string;
				if (text2 != null)
				{
					int num;
					if (SapBwModuleHelper.TryConvertToInt(dictionaryEntry.Value, out num))
					{
						list.Add(new RecordKeyDefinition(text2, NumberValue.New(num), TypeValue.Number));
					}
					else
					{
						string text3 = dictionaryEntry.Value as string;
						if (!string.IsNullOrEmpty(text3))
						{
							list.Add(new RecordKeyDefinition(text2, TextValue.New(text3), TypeValue.Text));
						}
					}
				}
			}
			return DataSourceException.NewDataSourceError<Message2>(this.engineHost, SapBwModuleHelper.GetMessage(exception), this.resource, list, exception.InnerException ?? exception);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00077088 File Offset: 0x00075288
		private static Message2 GetMessage(Exception exception)
		{
			return DataSourceException.DataSourceMessage("SAP Business Warehouse", exception.Message);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0007709A File Offset: 0x0007529A
		public bool IsSafeException(Exception exception)
		{
			return SafeExceptions.IsSafeException(exception);
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000770A2 File Offset: 0x000752A2
		private static bool TryConvertToInt(object objValue, out int value)
		{
			if (objValue == null)
			{
				value = 0;
				return false;
			}
			if (objValue is int)
			{
				value = (int)objValue;
				return true;
			}
			return int.TryParse(objValue.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out value);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000770D0 File Offset: 0x000752D0
		private static bool TryGetString(IDictionary dictionary, string key, out string value)
		{
			if (dictionary != null && dictionary.Contains(key))
			{
				value = dictionary[key] as string;
				return value != null;
			}
			value = null;
			return false;
		}

		// Token: 0x04001126 RID: 4390
		private readonly IEngineHost engineHost;

		// Token: 0x04001127 RID: 4391
		private readonly Tracer tracer;

		// Token: 0x04001128 RID: 4392
		private readonly IResource resource;
	}
}
