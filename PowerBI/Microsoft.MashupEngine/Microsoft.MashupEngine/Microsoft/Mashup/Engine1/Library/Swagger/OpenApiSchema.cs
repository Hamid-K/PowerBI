using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x0200039A RID: 922
	internal class OpenApiSchema : OpenApiPartialSchema
	{
		// Token: 0x06002021 RID: 8225 RVA: 0x000545B0 File Offset: 0x000527B0
		public OpenApiSchema(RecordValue schemaRecord, OpenApiDocument document)
			: base(schemaRecord, document.UserSettings)
		{
			this.document = document;
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x000545C6 File Offset: 0x000527C6
		protected override IEngineHost EngineHost
		{
			get
			{
				return this.document.EngineHost;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x000545D4 File Offset: 0x000527D4
		private RecordValue Properties
		{
			get
			{
				Value value;
				if (base.RawObject.TryGetValue("properties", out value))
				{
					return value.AsRecord;
				}
				return null;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x00054600 File Offset: 0x00052800
		private Value AdditonalProperties
		{
			get
			{
				Value value;
				if (base.RawObject.TryGetValue("additionalProperties", out value))
				{
					return value;
				}
				return null;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x00054624 File Offset: 0x00052824
		private ICollection<string> Required
		{
			get
			{
				if (this.required == null)
				{
					this.required = new HashSet<string>();
					Value value;
					if (base.RawObject.TryGetValue("required", out value))
					{
						foreach (IValueReference valueReference in value.AsList)
						{
							this.required.Add(valueReference.Value.AsString);
						}
					}
				}
				return this.required;
			}
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000546B0 File Offset: 0x000528B0
		protected override TypeValue ConstructObjectType()
		{
			List<NamedValue> list = new List<NamedValue>();
			RecordValue properties = this.Properties;
			if (properties != null)
			{
				foreach (NamedValue namedValue in properties.GetFields())
				{
					RecordValue asRecord = namedValue.Value.AsRecord;
					TypeValue typeValue = this.document.GetOrCreateSchema(asRecord).Type;
					typeValue = this.FinalizeType(typeValue, namedValue.Key, asRecord);
					list.Add(new NamedValue(namedValue.Key, RecordTypeValue.NewField(typeValue, null)));
				}
			}
			if (!base.UserSettings.IncludeMoreColumns)
			{
				return RecordTypeValue.New(RecordValue.New(list.ToArray()));
			}
			return RecordValueTransformer.CreateMoreColumnsRecordType(list);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00054788 File Offset: 0x00052988
		protected override TypeValue ConstructArrayElementType()
		{
			Value value;
			if (base.RawObject.TryGetValue("items", out value) && !value.IsList)
			{
				return this.document.GetOrCreateSchema(value.AsRecord).Type;
			}
			return TypeValue.Any;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000547CD File Offset: 0x000529CD
		private TypeValue FinalizeType(TypeValue type, string field, RecordValue fieldProperty)
		{
			if (!this.IsNonNullable(field, fieldProperty))
			{
				return type.Nullable;
			}
			return type.NonNullable;
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000547E6 File Offset: 0x000529E6
		private bool IsNonNullable(string field, RecordValue fieldProperty)
		{
			return this.Required.Contains(field) || (fieldProperty.Keys.Contains("required") && fieldProperty["required"].AsLogical.Boolean);
		}

		// Token: 0x04000C3D RID: 3133
		private readonly OpenApiDocument document;

		// Token: 0x04000C3E RID: 3134
		private ICollection<string> required;
	}
}
