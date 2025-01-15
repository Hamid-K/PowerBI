using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Library.Web;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000391 RID: 913
	internal class OpenApiRequest
	{
		// Token: 0x06002000 RID: 8192 RVA: 0x00053B26 File Offset: 0x00051D26
		public OpenApiRequest(OpenApiDocument document, string operationPathTemplate, OpenApiOperationDefinition operation, Keys requestKeys, Value[] requestValues)
		{
			this.document = document;
			this.operationPathTemplate = operationPathTemplate;
			this.operation = operation;
			this.requestKeys = requestKeys;
			this.requestValues = requestValues;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00053B54 File Offset: 0x00051D54
		public OpenApiResponse GetResponse()
		{
			IList<OpenApiParameterDefinition> parameterMapByValueIndexAndValidateValues = this.GetParameterMapByValueIndexAndValidateValues();
			Uri uri = UriHelper.Combine(this.document.ServiceUrl, TextValue.New(this.GetHttpRequestOperationPath(parameterMapByValueIndexAndValidateValues)));
			return new OpenApiResponse(new WebModule.WebContentsFunctionValue(this.document.EngineHost).TypedInvoke(TextValue.New(uri.OriginalString), this.GetHttpRequestOptions(parameterMapByValueIndexAndValidateValues)), this.document, this.operation);
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x00053BBD File Offset: 0x00051DBD
		private static bool ThrowIfNotValidValue(OpenApiParameterDefinition parameter, Value value)
		{
			if (parameter != null)
			{
				bool isNull = value.IsNull;
			}
			return true;
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x00053BCC File Offset: 0x00051DCC
		private IList<OpenApiParameterDefinition> GetParameterMapByValueIndexAndValidateValues()
		{
			IList<OpenApiParameterDefinition> list = new List<OpenApiParameterDefinition>();
			IDictionary<string, OpenApiParameterDefinition> dictionary = this.operation.ParameterDefinitions.ToDictionary((OpenApiParameterDefinition p) => p.Name);
			for (int i = 0; i < this.requestKeys.Count<string>(); i++)
			{
				OpenApiParameterDefinition openApiParameterDefinition = dictionary[this.requestKeys[i]];
				Value value = this.requestValues[i];
				OpenApiRequest.ThrowIfNotValidValue(openApiParameterDefinition, value);
				list.Add(openApiParameterDefinition);
			}
			return list;
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00053C54 File Offset: 0x00051E54
		private RecordValue GetHttpRequestOptions(IList<OpenApiParameterDefinition> valueIndexedParameterMap)
		{
			RecordBuilder recordBuilder = new RecordBuilder(4);
			recordBuilder.Add(new RecordKeyDefinition("Headers", this.GetHttpRequestHeaders(valueIndexedParameterMap), TypeValue.Record));
			recordBuilder.Add(new RecordKeyDefinition("Query", this.GetHttpRequestQuery(valueIndexedParameterMap), TypeValue.Record));
			if (!this.document.UserSettings.ManualCredentials)
			{
				RecordKeyDefinition recordKeyDefinition;
				if (new OpenApiAuthBuilder(this.document, this.operation).TryGetApiKey(out recordKeyDefinition))
				{
					recordBuilder.Add(recordKeyDefinition);
				}
			}
			else
			{
				recordBuilder.Add(new RecordKeyDefinition("ManualCredentials", LogicalValue.True, TypeValue.Logical));
			}
			if (this.document.UserSettings.ManualStatusHandling != null)
			{
				recordBuilder.Add(new RecordKeyDefinition("ManualStatusHandling", this.document.UserSettings.ManualStatusHandling, TypeValue.List));
			}
			return recordBuilder.ToRecord();
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00053D34 File Offset: 0x00051F34
		private RecordValue GetHttpRequestHeaders(IList<OpenApiParameterDefinition> valueIndexedParameterMap)
		{
			OpenApiHttpRequestHeaderBuilder openApiHttpRequestHeaderBuilder = new OpenApiHttpRequestHeaderBuilder(this.document);
			openApiHttpRequestHeaderBuilder.AddRecord(this.document.UserSettings.Headers);
			this.AddAcceptHeadersToComponentsBuilder(openApiHttpRequestHeaderBuilder);
			this.AddParametersToBuilder(openApiHttpRequestHeaderBuilder, valueIndexedParameterMap);
			return openApiHttpRequestHeaderBuilder.Build();
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x00053D78 File Offset: 0x00051F78
		private RecordValue GetHttpRequestQuery(IList<OpenApiParameterDefinition> valueIndexedParameterMap)
		{
			OpenApiHttpRequestQueryBuilder openApiHttpRequestQueryBuilder = new OpenApiHttpRequestQueryBuilder(this.document);
			openApiHttpRequestQueryBuilder.AddRecord(this.document.UserSettings.Query);
			this.AddParametersToBuilder(openApiHttpRequestQueryBuilder, valueIndexedParameterMap);
			return openApiHttpRequestQueryBuilder.Build();
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x00053DB8 File Offset: 0x00051FB8
		private string GetHttpRequestOperationPath(IList<OpenApiParameterDefinition> valueIndexedParameterMap)
		{
			OpenApiHttpRequestPathParameterBuilder openApiHttpRequestPathParameterBuilder = new OpenApiHttpRequestPathParameterBuilder(this.document, this.operationPathTemplate);
			this.AddParametersToBuilder(openApiHttpRequestPathParameterBuilder, valueIndexedParameterMap);
			return openApiHttpRequestPathParameterBuilder.Build();
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00053DE8 File Offset: 0x00051FE8
		private void AddParametersToBuilder(IOpenApiHttpRequestBuilderWithParameter builder, IList<OpenApiParameterDefinition> valueIndexedParameterMap)
		{
			for (int i = 0; i < this.requestKeys.Count<string>(); i++)
			{
				OpenApiParameterDefinition openApiParameterDefinition = valueIndexedParameterMap[i];
				Value value = this.requestValues[i];
				bool? required = openApiParameterDefinition.Required;
				bool flag = false;
				if (!((required.GetValueOrDefault() == flag) & (required != null)) || !value.IsNull || openApiParameterDefinition.PartialSchema.DefaultValue != Value.Null)
				{
					if (value.IsNull && openApiParameterDefinition.PartialSchema.DefaultValue != Value.Null)
					{
						value = openApiParameterDefinition.PartialSchema.DefaultValue;
					}
					builder.Add(openApiParameterDefinition, value);
				}
			}
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00053E88 File Offset: 0x00052088
		private void AddAcceptHeadersToComponentsBuilder(OpenApiHttpRequestHeaderBuilder builder)
		{
			List<string> list = new List<string>();
			if (this.operation.Produces != null)
			{
				list.AddRange(this.operation.Produces);
			}
			else if (this.document.Produces != null)
			{
				list.AddRange(this.document.Produces);
			}
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string text in list)
			{
				if (!(text == "application/json"))
				{
					if (!(text == "application/xml") && !(text == "text/plain"))
					{
					}
				}
				else
				{
					hashSet.Add("application/json");
				}
			}
			if (hashSet.Count > 0)
			{
				string text2 = string.Join(",", hashSet.ToArray<string>());
				builder.AddRecordKeyDefinition(new RecordKeyDefinition("Accept", TextValue.New(text2), TypeValue.Text));
			}
		}

		// Token: 0x04000C29 RID: 3113
		private readonly OpenApiDocument document;

		// Token: 0x04000C2A RID: 3114
		private readonly string operationPathTemplate;

		// Token: 0x04000C2B RID: 3115
		private readonly OpenApiOperationDefinition operation;

		// Token: 0x04000C2C RID: 3116
		private readonly Keys requestKeys;

		// Token: 0x04000C2D RID: 3117
		private readonly Value[] requestValues;
	}
}
