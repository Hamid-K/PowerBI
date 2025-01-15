using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000397 RID: 919
	internal class OpenApiHttpRequestPathParameterBuilder : IOpenApiHttpRequestBuilderWithParameter
	{
		// Token: 0x06002016 RID: 8214 RVA: 0x000541A4 File Offset: 0x000523A4
		public OpenApiHttpRequestPathParameterBuilder(OpenApiDocument document, string operationPathTemplate)
		{
			this.document = document;
			this.operationPathTemplate = operationPathTemplate;
			this.pathPatternToValue = new Dictionary<string, string>();
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x000541C8 File Offset: 0x000523C8
		public void Add(OpenApiParameterDefinition parameter, Value value)
		{
			if (parameter.In != "path")
			{
				return;
			}
			OpenApiParameterItem openApiParameterItem = parameter.PartialSchema as OpenApiParameterItem;
			if (openApiParameterItem == null)
			{
				return;
			}
			string text = openApiParameterItem.ConvertValueToString(this.document, parameter.Name, value);
			string text2 = "{" + parameter.Name + "}";
			this.pathPatternToValue.Add(text2, text);
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x00054230 File Offset: 0x00052430
		public string Build()
		{
			string text = this.operationPathTemplate;
			foreach (KeyValuePair<string, string> keyValuePair in this.pathPatternToValue)
			{
				if (text.IndexOf(keyValuePair.Key, StringComparison.Ordinal) != -1)
				{
					text = text.Replace(keyValuePair.Key, Uri.EscapeDataString(keyValuePair.Value));
				}
			}
			return text;
		}

		// Token: 0x04000C33 RID: 3123
		private readonly OpenApiDocument document;

		// Token: 0x04000C34 RID: 3124
		private readonly string operationPathTemplate;

		// Token: 0x04000C35 RID: 3125
		private readonly IDictionary<string, string> pathPatternToValue;
	}
}
