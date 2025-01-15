using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x0200037A RID: 890
	internal class OpenApiAuthBuilder
	{
		// Token: 0x06001F6C RID: 8044 RVA: 0x00051697 File Offset: 0x0004F897
		public OpenApiAuthBuilder(OpenApiDocument document, OpenApiOperationDefinition operation)
		{
			this.operation = operation;
			this.document = document;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x000516B0 File Offset: 0x0004F8B0
		public bool TryGetApiKey(out RecordKeyDefinition apiKeyHeader)
		{
			List<IDictionary<string, IEnumerable<string>>> list = new List<IDictionary<string, IEnumerable<string>>>();
			if (this.operation.Security != null)
			{
				list.AddRange(this.operation.Security);
			}
			if (this.document.Security != null)
			{
				list.AddRange(this.document.Security);
			}
			if (list.Count > 0)
			{
				string text = string.Empty;
				string securityDefinitionName = this.document.UserSettings.SecurityDefinitionName;
				if (securityDefinitionName != null)
				{
					text = securityDefinitionName;
				}
				else
				{
					IEnumerator enumerator = list[0].Keys.GetEnumerator();
					if (enumerator.MoveNext())
					{
						text = enumerator.Current.ToString();
					}
				}
				OpenApiSecurityDefinition openApiSecurityDefinition = null;
				if (!string.IsNullOrEmpty(text))
				{
					if (!this.document.SecurityDefinitions.TryGetValue(text, out openApiSecurityDefinition))
					{
						throw DataSourceException.NewDataSourceError<Message0>(this.document.EngineHost, Strings.OpenApiSecurityDefinitionNotDefined, this.document.Resource, "Security Definition", TextValue.New(text), TypeValue.Text, null);
					}
					if (openApiSecurityDefinition.Type == "apiKey")
					{
						if (openApiSecurityDefinition.In == "query")
						{
							apiKeyHeader = new RecordKeyDefinition("ApiKeyName", TextValue.New(openApiSecurityDefinition.Name), TypeValue.Text);
							return true;
						}
						if (openApiSecurityDefinition.In == "header")
						{
							throw DataSourceException.NewDataSourceError<Message0>(this.document.EngineHost, Strings.OpenApiApiKeyNotSupportedInHeader, this.document.Resource, null, null);
						}
						throw DataSourceException.NewDataSourceError<Message1>(this.document.EngineHost, Strings.OpenApiApiAuthInFieldMustBeQueryOrHeader(openApiSecurityDefinition.In), this.document.Resource, null, null);
					}
					else
					{
						if (openApiSecurityDefinition.Type == "basic")
						{
							apiKeyHeader = default(RecordKeyDefinition);
							return false;
						}
						throw DataSourceException.NewDataSourceError<Message1>(this.document.EngineHost, Strings.OpenApiAuthNotSupported(TextValue.New(openApiSecurityDefinition.Type)), this.document.Resource, "Security", TextValue.New(openApiSecurityDefinition.Type), TypeValue.Text, null);
					}
				}
			}
			apiKeyHeader = default(RecordKeyDefinition);
			return false;
		}

		// Token: 0x04000B6C RID: 2924
		private const string SecurityKey = "Security";

		// Token: 0x04000B6D RID: 2925
		private const string SecurityDefinitionKey = "Security Definition";

		// Token: 0x04000B6E RID: 2926
		private readonly OpenApiDocument document;

		// Token: 0x04000B6F RID: 2927
		private readonly OpenApiOperationDefinition operation;
	}
}
