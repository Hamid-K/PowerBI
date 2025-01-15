using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x0200038C RID: 908
	internal class OpenApiParameterDefinition : OpenApiSpecObject
	{
		// Token: 0x06001FCD RID: 8141 RVA: 0x00052C97 File Offset: 0x00050E97
		public OpenApiParameterDefinition(RecordValue rawParameter, OpenApiDocument document)
			: base(rawParameter, document.UserSettings)
		{
			this.document = document;
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x00052CB0 File Offset: 0x00050EB0
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					Value value;
					this.name = (base.RawObject.TryGetValue("name", out value) ? value.AsString : string.Empty);
				}
				return this.name;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x00052CF4 File Offset: 0x00050EF4
		public string In
		{
			get
			{
				if (this.inValue == null)
				{
					Value value;
					this.inValue = (base.RawObject.TryGetValue("in", out value) ? value.AsString : string.Empty);
				}
				return this.inValue;
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x00052D36 File Offset: 0x00050F36
		public Value Description
		{
			get
			{
				if (this.description == null && base.RawObject.TryGetValue("description", out this.description))
				{
					this.description = Value.Null;
				}
				return this.description;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x00052D6C File Offset: 0x00050F6C
		public bool? Required
		{
			get
			{
				if (this.required == null)
				{
					Value value;
					this.required = new bool?(base.RawObject.TryGetValue("required", out value) && value.AsBoolean);
				}
				return this.required;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x00052DB4 File Offset: 0x00050FB4
		public bool? AllowEmptyValue
		{
			get
			{
				if (this.allowEmptyValue == null)
				{
					Value value;
					this.allowEmptyValue = new bool?(base.RawObject.TryGetValue("allowEmptyValue", out value) && value.AsBoolean);
				}
				return this.allowEmptyValue;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x00052DFC File Offset: 0x00050FFC
		public bool? Deprecated
		{
			get
			{
				if (this.deprecated == null)
				{
					Value value;
					this.deprecated = new bool?(base.RawObject.TryGetValue("deprecated", out value) && value.AsBoolean);
				}
				return this.deprecated;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x00052E44 File Offset: 0x00051044
		public OpenApiPartialSchema PartialSchema
		{
			get
			{
				if (this.partialSchema == null)
				{
					if ("body".Equals(this.In))
					{
						throw ValueException.NotImplemented<Message1>(Strings.OpenApiUnsupportedParameterPropertyIn(this.In));
					}
					this.partialSchema = new OpenApiParameterItem(base.RawObject, this.document);
				}
				return this.partialSchema;
			}
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x00052E9C File Offset: 0x0005109C
		protected override List<RecordKeyDefinition> GetMetaDataRecords()
		{
			List<RecordKeyDefinition> metaDataRecords = base.GetMetaDataRecords();
			if (this.Name != string.Empty)
			{
				metaDataRecords.Add(new RecordKeyDefinition("name", TextValue.New(this.Name), TypeValue.Text));
			}
			if (this.Description != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("description", this.Description, TypeValue.Text));
			}
			metaDataRecords.Add(new RecordKeyDefinition("deprecated", LogicalValue.New(this.Deprecated.Value), TypeValue.Logical));
			metaDataRecords.Add(new RecordKeyDefinition("schema", this.PartialSchema.GetMetadata(), TypeValue.Record));
			return metaDataRecords;
		}

		// Token: 0x04000C0B RID: 3083
		private readonly OpenApiDocument document;

		// Token: 0x04000C0C RID: 3084
		private bool? required;

		// Token: 0x04000C0D RID: 3085
		private bool? deprecated;

		// Token: 0x04000C0E RID: 3086
		private bool? allowEmptyValue;

		// Token: 0x04000C0F RID: 3087
		private string name;

		// Token: 0x04000C10 RID: 3088
		private string inValue;

		// Token: 0x04000C11 RID: 3089
		private Value description;

		// Token: 0x04000C12 RID: 3090
		private OpenApiPartialSchema partialSchema;
	}
}
