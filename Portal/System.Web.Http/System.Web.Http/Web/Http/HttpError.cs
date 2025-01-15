using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Http
{
	// Token: 0x02000025 RID: 37
	[XmlRoot("Error")]
	public sealed class HttpError : Dictionary<string, object>, IXmlSerializable
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00003EA5 File Offset: 0x000020A5
		public HttpError()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003EB2 File Offset: 0x000020B2
		public HttpError(string message)
			: this()
		{
			if (message == null)
			{
				throw Error.ArgumentNull("message");
			}
			this.Message = message;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003ED0 File Offset: 0x000020D0
		public HttpError(Exception exception, bool includeErrorDetail)
			: this()
		{
			if (exception == null)
			{
				throw Error.ArgumentNull("exception");
			}
			this.Message = SRResources.ErrorOccurred;
			if (includeErrorDetail)
			{
				base.Add(HttpErrorKeys.ExceptionMessageKey, exception.Message);
				base.Add(HttpErrorKeys.ExceptionTypeKey, exception.GetType().FullName);
				base.Add(HttpErrorKeys.StackTraceKey, exception.StackTrace);
				if (exception.InnerException != null)
				{
					base.Add(HttpErrorKeys.InnerExceptionKey, new HttpError(exception.InnerException, includeErrorDetail));
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003F58 File Offset: 0x00002158
		public HttpError(ModelStateDictionary modelState, bool includeErrorDetail)
			: this()
		{
			if (modelState == null)
			{
				throw Error.ArgumentNull("modelState");
			}
			if (modelState.IsValid)
			{
				throw Error.Argument("modelState", SRResources.ValidModelState, new object[0]);
			}
			this.Message = SRResources.BadRequest;
			HttpError httpError = new HttpError();
			Func<ModelError, string> <>9__0;
			foreach (KeyValuePair<string, ModelState> keyValuePair in modelState)
			{
				string key = keyValuePair.Key;
				ModelErrorCollection errors = keyValuePair.Value.Errors;
				if (errors != null && errors.Count > 0)
				{
					IEnumerable<ModelError> enumerable = errors;
					Func<ModelError, string> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = delegate(ModelError error)
						{
							if (includeErrorDetail && error.Exception != null)
							{
								return error.Exception.Message;
							}
							if (!string.IsNullOrEmpty(error.ErrorMessage))
							{
								return error.ErrorMessage;
							}
							return SRResources.ErrorOccurred;
						});
					}
					IEnumerable<string> enumerable2 = enumerable.Select(func).ToArray<string>();
					httpError.Add(key, enumerable2);
				}
			}
			base.Add(HttpErrorKeys.ModelStateKey, httpError);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004054 File Offset: 0x00002254
		internal HttpError(string message, string messageDetail)
			: this(message)
		{
			if (messageDetail == null)
			{
				throw Error.ArgumentNull("message");
			}
			base.Add(HttpErrorKeys.MessageDetailKey, messageDetail);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004077 File Offset: 0x00002277
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004084 File Offset: 0x00002284
		public string Message
		{
			get
			{
				return this.GetPropertyValue<string>(HttpErrorKeys.MessageKey);
			}
			set
			{
				base[HttpErrorKeys.MessageKey] = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004092 File Offset: 0x00002292
		public HttpError ModelState
		{
			get
			{
				return this.GetPropertyValue<HttpError>(HttpErrorKeys.ModelStateKey);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000409F File Offset: 0x0000229F
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000040AC File Offset: 0x000022AC
		public string MessageDetail
		{
			get
			{
				return this.GetPropertyValue<string>(HttpErrorKeys.MessageDetailKey);
			}
			set
			{
				base[HttpErrorKeys.MessageDetailKey] = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000040BA File Offset: 0x000022BA
		// (set) Token: 0x060000CD RID: 205 RVA: 0x000040C7 File Offset: 0x000022C7
		public string ExceptionMessage
		{
			get
			{
				return this.GetPropertyValue<string>(HttpErrorKeys.ExceptionMessageKey);
			}
			set
			{
				base[HttpErrorKeys.ExceptionMessageKey] = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000040D5 File Offset: 0x000022D5
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000040E2 File Offset: 0x000022E2
		public string ExceptionType
		{
			get
			{
				return this.GetPropertyValue<string>(HttpErrorKeys.ExceptionTypeKey);
			}
			set
			{
				base[HttpErrorKeys.ExceptionTypeKey] = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000040F0 File Offset: 0x000022F0
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000040FD File Offset: 0x000022FD
		public string StackTrace
		{
			get
			{
				return this.GetPropertyValue<string>(HttpErrorKeys.StackTraceKey);
			}
			set
			{
				base[HttpErrorKeys.StackTraceKey] = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000410B File Offset: 0x0000230B
		public HttpError InnerException
		{
			get
			{
				return this.GetPropertyValue<HttpError>(HttpErrorKeys.InnerExceptionKey);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004118 File Offset: 0x00002318
		public TValue GetPropertyValue<TValue>(string key)
		{
			TValue tvalue;
			if (this.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			return default(TValue);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000413B File Offset: 0x0000233B
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004140 File Offset: 0x00002340
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			reader.ReadStartElement();
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				string text = XmlConvert.DecodeName(reader.LocalName);
				string text2 = reader.ReadInnerXml();
				base.Add(text, text2);
				reader.MoveToContent();
			}
			reader.ReadEndElement();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004198 File Offset: 0x00002398
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			foreach (KeyValuePair<string, object> keyValuePair in this)
			{
				string key = keyValuePair.Key;
				object value = keyValuePair.Value;
				writer.WriteStartElement(XmlConvert.EncodeLocalName(key));
				if (value != null)
				{
					HttpError httpError = value as HttpError;
					if (httpError == null)
					{
						writer.WriteValue(value);
					}
					else
					{
						((IXmlSerializable)httpError).WriteXml(writer);
					}
				}
				writer.WriteEndElement();
			}
		}
	}
}
