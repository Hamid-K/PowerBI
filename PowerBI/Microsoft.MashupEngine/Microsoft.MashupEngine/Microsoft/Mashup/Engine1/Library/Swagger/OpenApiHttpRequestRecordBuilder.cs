using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x02000393 RID: 915
	internal abstract class OpenApiHttpRequestRecordBuilder
	{
		// Token: 0x0600200D RID: 8205 RVA: 0x00053F9C File Offset: 0x0005219C
		public OpenApiHttpRequestRecordBuilder()
		{
			this.list = new List<RecordKeyDefinition>();
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x00053FAF File Offset: 0x000521AF
		public void AddRecordKeyDefinition(RecordKeyDefinition definition)
		{
			this.list.Add(definition);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00053FC0 File Offset: 0x000521C0
		public void AddRecord(RecordValue record)
		{
			foreach (NamedValue namedValue in record.GetFields())
			{
				this.AddRecordKeyDefinition(new RecordKeyDefinition(namedValue.Key, namedValue.Value, namedValue.Value.Type));
			}
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00054034 File Offset: 0x00052234
		public RecordValue Build()
		{
			RecordBuilder recordBuilder = new RecordBuilder(this.list.Count);
			recordBuilder.Add(this.list);
			return recordBuilder.ToRecord();
		}

		// Token: 0x04000C30 RID: 3120
		private readonly IList<RecordKeyDefinition> list;
	}
}
