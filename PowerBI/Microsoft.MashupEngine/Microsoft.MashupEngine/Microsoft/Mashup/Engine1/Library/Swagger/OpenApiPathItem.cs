using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000390 RID: 912
	internal class OpenApiPathItem : OpenApiSpecObject
	{
		// Token: 0x06001FFD RID: 8189 RVA: 0x00053A70 File Offset: 0x00051C70
		public OpenApiPathItem(RecordValue rawPathItem, OpenApiDocument document)
			: base(rawPathItem ?? RecordValue.Empty, document.UserSettings)
		{
			this.document = document;
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x00053A90 File Offset: 0x00051C90
		public OpenApiOperationDefinition Get
		{
			get
			{
				Value value;
				if (this.get == null && base.RawObject.TryGetValue("get", out value))
				{
					this.get = new OpenApiOperationDefinition(value.AsRecord, this.document, this);
				}
				return this.get;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x00053AD8 File Offset: 0x00051CD8
		public IList<OpenApiParameterDefinition> Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					IList<OpenApiParameterDefinition> list = new List<OpenApiParameterDefinition>();
					Value value;
					if (base.RawObject.TryGetValue("parameters", out value))
					{
						OpenApiHelper.AppendToResolvedParameterList(list, value.AsList, this.document);
					}
					this.parameters = list;
				}
				return this.parameters;
			}
		}

		// Token: 0x04000C26 RID: 3110
		private readonly OpenApiDocument document;

		// Token: 0x04000C27 RID: 3111
		private IList<OpenApiParameterDefinition> parameters;

		// Token: 0x04000C28 RID: 3112
		private OpenApiOperationDefinition get;
	}
}
