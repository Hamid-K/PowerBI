using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000399 RID: 921
	internal class OpenApiResponseDefinition : OpenApiSpecObject
	{
		// Token: 0x0600201E RID: 8222 RVA: 0x0005451E File Offset: 0x0005271E
		public OpenApiResponseDefinition(RecordValue rawResponseDefinition, OpenApiDocument document)
			: base(rawResponseDefinition, document.UserSettings)
		{
			this.document = document;
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x00054534 File Offset: 0x00052734
		public Value Description()
		{
			Value value;
			if (base.RawObject.TryGetValue("description", out value))
			{
				return value.AsText;
			}
			return Value.Null;
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x00054564 File Offset: 0x00052764
		public OpenApiSchema Schema
		{
			get
			{
				if (this.schema == null)
				{
					Value empty;
					if (!base.RawObject.TryGetValue("schema", out empty))
					{
						empty = RecordValue.Empty;
					}
					this.schema = this.document.GetOrCreateSchema(empty.AsRecord);
				}
				return this.schema;
			}
		}

		// Token: 0x04000C3B RID: 3131
		private readonly OpenApiDocument document;

		// Token: 0x04000C3C RID: 3132
		private OpenApiSchema schema;
	}
}
