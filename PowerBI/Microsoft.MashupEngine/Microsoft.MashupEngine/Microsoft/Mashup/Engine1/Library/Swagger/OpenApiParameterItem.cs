using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x0200038D RID: 909
	internal class OpenApiParameterItem : OpenApiPartialSchema
	{
		// Token: 0x06001FD6 RID: 8150 RVA: 0x00052F53 File Offset: 0x00051153
		public OpenApiParameterItem(RecordValue schemaRecord, OpenApiDocument document)
			: base(schemaRecord, document.UserSettings)
		{
			this.document = document;
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x00052F6C File Offset: 0x0005116C
		public OpenApiParameterItem Items
		{
			get
			{
				Value value;
				if (this.items == null && base.RawObject.TryGetValue("items", out value))
				{
					this.items = new OpenApiParameterItem(value.AsRecord, this.document);
				}
				return this.items;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x00052FB4 File Offset: 0x000511B4
		public string CollectionFormat
		{
			get
			{
				if (this.collectionFormat == null)
				{
					Value value;
					this.collectionFormat = (base.RawObject.TryGetValue("collectionFormat", out value) ? value.AsString : "csv");
				}
				return this.collectionFormat;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x00052FF6 File Offset: 0x000511F6
		protected override IEngineHost EngineHost
		{
			get
			{
				return this.document.EngineHost;
			}
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x00053003 File Offset: 0x00051203
		protected override TypeValue ConstructObjectType()
		{
			throw ValueException.NewDataSourceError<Message0>(Strings.OpenApiObjectTypeOnlyPermittedInBodyParameterItem, base.RawObject, null);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00053018 File Offset: 0x00051218
		protected override TypeValue ConstructArrayElementType()
		{
			OpenApiParameterItem openApiParameterItem = this.Items;
			if (openApiParameterItem == null)
			{
				return TypeValue.Any;
			}
			return openApiParameterItem.Type;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0005303B File Offset: 0x0005123B
		protected override List<RecordKeyDefinition> GetMetaDataRecords()
		{
			List<RecordKeyDefinition> metaDataRecords = base.GetMetaDataRecords();
			metaDataRecords.Add(new RecordKeyDefinition("collectionFormat", TextValue.New(this.CollectionFormat), TypeValue.Text));
			return metaDataRecords;
		}

		// Token: 0x04000C13 RID: 3091
		private readonly OpenApiDocument document;

		// Token: 0x04000C14 RID: 3092
		private string collectionFormat;

		// Token: 0x04000C15 RID: 3093
		private OpenApiParameterItem items;
	}
}
