using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.Http;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Extensions
{
	// Token: 0x020001C4 RID: 452
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpErrorExtensions
	{
		// Token: 0x06000EEC RID: 3820 RVA: 0x0003D7C8 File Offset: 0x0003B9C8
		public static ODataError CreateODataError(this HttpError httpError)
		{
			if (httpError == null)
			{
				throw Error.ArgumentNull("httpError");
			}
			return new ODataError
			{
				Message = httpError.GetPropertyValue<string>(HttpErrorKeys.MessageKey),
				ErrorCode = httpError.GetPropertyValue<string>(HttpErrorKeys.ErrorCodeKey),
				InnerError = HttpErrorExtensions.ToODataInnerError(httpError)
			};
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0003D818 File Offset: 0x0003BA18
		private static ODataInnerError ToODataInnerError(HttpError httpError)
		{
			string propertyValue = httpError.GetPropertyValue<string>(HttpErrorKeys.ExceptionMessageKey);
			if (propertyValue != null)
			{
				ODataInnerError odataInnerError = new ODataInnerError();
				odataInnerError.Message = propertyValue;
				odataInnerError.TypeName = httpError.GetPropertyValue<string>(HttpErrorKeys.ExceptionTypeKey);
				odataInnerError.StackTrace = httpError.GetPropertyValue<string>(HttpErrorKeys.StackTraceKey);
				HttpError propertyValue2 = httpError.GetPropertyValue<HttpError>(HttpErrorKeys.InnerExceptionKey);
				if (propertyValue2 != null)
				{
					odataInnerError.InnerError = HttpErrorExtensions.ToODataInnerError(propertyValue2);
				}
				return odataInnerError;
			}
			string propertyValue3 = httpError.GetPropertyValue<string>(HttpErrorKeys.MessageDetailKey);
			if (propertyValue3 != null)
			{
				return new ODataInnerError
				{
					Message = propertyValue3
				};
			}
			HttpError propertyValue4 = httpError.GetPropertyValue<HttpError>(HttpErrorKeys.ModelStateKey);
			if (propertyValue4 != null)
			{
				return new ODataInnerError
				{
					Message = HttpErrorExtensions.ConvertModelStateErrors(propertyValue4)
				};
			}
			return null;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0003D8C4 File Offset: 0x0003BAC4
		private static string ConvertModelStateErrors(HttpError error)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, object> keyValuePair in error)
			{
				if (keyValuePair.Value != null)
				{
					stringBuilder.Append(keyValuePair.Key);
					stringBuilder.Append(" : ");
					IEnumerable<string> enumerable = keyValuePair.Value as IEnumerable<string>;
					if (enumerable != null)
					{
						using (IEnumerator<string> enumerator2 = enumerable.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string text = enumerator2.Current;
								stringBuilder.AppendLine(text);
							}
							continue;
						}
					}
					stringBuilder.AppendLine(keyValuePair.Value.ToString());
				}
			}
			return stringBuilder.ToString();
		}
	}
}
