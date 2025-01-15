using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000395 RID: 917
	internal class OpenApiHttpRequestQueryBuilder : OpenApiHttpRequestRecordBuilder, IOpenApiHttpRequestBuilderWithParameter
	{
		// Token: 0x06002012 RID: 8210 RVA: 0x00054067 File Offset: 0x00052267
		public OpenApiHttpRequestQueryBuilder(OpenApiDocument document)
		{
			this.document = document;
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00054078 File Offset: 0x00052278
		public void Add(OpenApiParameterDefinition parameter, Value value)
		{
			if (parameter.In != "query")
			{
				return;
			}
			OpenApiParameterItem openApiParameterItem = parameter.PartialSchema as OpenApiParameterItem;
			if (openApiParameterItem == null)
			{
				return;
			}
			if (openApiParameterItem.Type.IsListType && openApiParameterItem.CollectionFormat == "multi")
			{
				List<string> list = openApiParameterItem.ConvertListValueToStringList(this.document, parameter.Name, value.AsList);
				base.AddRecordKeyDefinition(new RecordKeyDefinition(parameter.Name, ListValue.New(list.ToArray()), ListTypeValue.Text));
				return;
			}
			string text = openApiParameterItem.ConvertValueToString(this.document, parameter.Name, value);
			base.AddRecordKeyDefinition(new RecordKeyDefinition(parameter.Name, TextValue.New(text), TypeValue.Text));
		}

		// Token: 0x04000C31 RID: 3121
		private readonly OpenApiDocument document;
	}
}
