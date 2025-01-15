using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000396 RID: 918
	internal class OpenApiHttpRequestHeaderBuilder : OpenApiHttpRequestRecordBuilder, IOpenApiHttpRequestBuilderWithParameter
	{
		// Token: 0x06002014 RID: 8212 RVA: 0x00054132 File Offset: 0x00052332
		public OpenApiHttpRequestHeaderBuilder(OpenApiDocument document)
		{
			this.document = document;
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x00054144 File Offset: 0x00052344
		public void Add(OpenApiParameterDefinition parameter, Value value)
		{
			if (parameter.In != "header")
			{
				return;
			}
			OpenApiParameterItem openApiParameterItem = parameter.PartialSchema as OpenApiParameterItem;
			if (openApiParameterItem == null)
			{
				return;
			}
			string text = openApiParameterItem.ConvertValueToString(this.document, parameter.Name, value);
			base.AddRecordKeyDefinition(new RecordKeyDefinition(parameter.Name, TextValue.New(text), TypeValue.Text));
		}

		// Token: 0x04000C32 RID: 3122
		private readonly OpenApiDocument document;
	}
}
