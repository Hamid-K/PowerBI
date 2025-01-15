using System;
using System.Globalization;
using System.IO;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Xml;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000398 RID: 920
	internal class OpenApiResponse
	{
		// Token: 0x06002019 RID: 8217 RVA: 0x000542AC File Offset: 0x000524AC
		public OpenApiResponse(BinaryValue binaryContents, OpenApiDocument document, OpenApiOperationDefinition operation)
		{
			this.binaryContents = binaryContents;
			this.document = document;
			this.operation = operation;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x000542CC File Offset: 0x000524CC
		public Value GetValue()
		{
			Value value3;
			try
			{
				Value value;
				this.binaryContents.TryGetMetaField("Content.Type", out value);
				string text = string.Empty;
				if (value != null && !value.IsNull)
				{
					text = value.AsString;
				}
				if (text == "application/json")
				{
					using (StreamReader streamReader = this.binaryContents.OpenText(Value.Null))
					{
						Value value2;
						if (streamReader.Peek() == -1)
						{
							value2 = TextValue.Empty;
						}
						else
						{
							value2 = JsonParser.Parse(streamReader, null);
						}
						return this.CastToResponseType(value2);
					}
				}
				if (!(text == "application/xml") && !(text == "text/xml"))
				{
					if (text == "text/plain")
					{
						using (StreamReader streamReader2 = this.binaryContents.OpenText(Value.Null))
						{
							return TextValue.New(streamReader2.ReadToEnd());
						}
					}
					throw DataSourceException.NewDataSourceError<Message0>(this.document.EngineHost, Strings.OpenApiContentTypeNotSupported, this.document.Resource, "ContentType", TextValue.New(text), TypeValue.Text, null);
				}
				value3 = new XmlModule.XmlDocumentFunctionValue().TypedInvoke(this.binaryContents, Value.Null);
			}
			catch (ResponseException ex)
			{
				throw DataSourceException.NewDataSourceError(this.document.EngineHost, ex.Message, this.document.Resource, null, ex);
			}
			return value3;
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0005446C File Offset: 0x0005266C
		private Value CastToResponseType(Value value)
		{
			OpenApiResponseDefinition openApiResponseDefinition;
			if (!this.TryGetResponse(out openApiResponseDefinition))
			{
				return value;
			}
			return openApiResponseDefinition.Schema.Type.Cast(value, this.document.EngineHost);
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x000544A4 File Offset: 0x000526A4
		private string GetResponseStatus()
		{
			Value value;
			if (!this.binaryContents.TryGetMetaField("Response.Status", out value) || !value.IsNumber)
			{
				return "default";
			}
			return value.AsNumber.ToInt32().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x000544EB File Offset: 0x000526EB
		private bool TryGetResponse(out OpenApiResponseDefinition response)
		{
			return this.operation.ResponseDefinitions.TryGetValue(this.GetResponseStatus(), out response) || this.operation.ResponseDefinitions.TryGetValue("default", out response);
		}

		// Token: 0x04000C36 RID: 3126
		private const string ContentTypeKey = "ContentType";

		// Token: 0x04000C37 RID: 3127
		private const string Default = "default";

		// Token: 0x04000C38 RID: 3128
		private readonly BinaryValue binaryContents;

		// Token: 0x04000C39 RID: 3129
		private readonly OpenApiDocument document;

		// Token: 0x04000C3A RID: 3130
		private readonly OpenApiOperationDefinition operation;
	}
}
