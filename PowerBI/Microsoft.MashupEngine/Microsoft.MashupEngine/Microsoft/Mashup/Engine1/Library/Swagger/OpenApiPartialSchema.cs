using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Swagger
{
	// Token: 0x0200038F RID: 911
	internal abstract class OpenApiPartialSchema : OpenApiSpecObject
	{
		// Token: 0x06001FE0 RID: 8160 RVA: 0x000531DB File Offset: 0x000513DB
		protected OpenApiPartialSchema(RecordValue rawSchema, OpenApiUserSettings userSettings)
			: base(rawSchema, userSettings)
		{
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x000531E8 File Offset: 0x000513E8
		public TypeValue Type
		{
			get
			{
				if (this.type == null)
				{
					if (this.TypeConstructionBegan)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.OpenApiCyclicallyDefinedTypeCantBeCreated, base.RawObject, null);
					}
					this.TypeConstructionBegan = true;
					Value value;
					if (base.RawObject.TryGetValue("type", out value))
					{
						this.type = this.ConstructType(value.ToString(), this.Format);
					}
					else
					{
						this.type = TypeValue.Any;
					}
					this.type = this.AddEnumMetadata(this.type);
					this.type = BinaryOperator.AddMeta.Invoke(this.type, base.GetMetadata()).AsType;
					this.type = this.type.NewFacets(this.GetFacets()).AsType;
				}
				return this.type;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x000532B0 File Offset: 0x000514B0
		public Value DefaultValue
		{
			get
			{
				if (this.defaultValue == null)
				{
					if (base.RawObject.TryGetValue("default", out this.defaultValue))
					{
						this.defaultValue = this.Type.Cast(this.defaultValue, this.EngineHost);
					}
					else
					{
						this.defaultValue = Value.Null;
					}
				}
				return this.defaultValue;
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x0005330D File Offset: 0x0005150D
		public Value Maximum
		{
			get
			{
				if (this.maximum == null && !base.RawObject.TryGetValue("maximum", out this.maximum))
				{
					this.maximum = Value.Null;
				}
				return this.maximum;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x00053340 File Offset: 0x00051540
		public Value ExclusiveMaximum
		{
			get
			{
				if (this.exclusiveMaximum == null && !base.RawObject.TryGetValue("exclusiveMaximum", out this.exclusiveMaximum))
				{
					this.exclusiveMaximum = Value.Null;
				}
				return this.exclusiveMaximum;
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00053373 File Offset: 0x00051573
		public Value Minimum
		{
			get
			{
				if (this.minimum == null && !base.RawObject.TryGetValue("minimum", out this.minimum))
				{
					this.minimum = Value.Null;
				}
				return this.minimum;
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x000533A6 File Offset: 0x000515A6
		public Value ExclusiveMinimum
		{
			get
			{
				if (this.exclusiveMinimum == null && !base.RawObject.TryGetValue("exclusiveMinimum", out this.exclusiveMinimum))
				{
					this.exclusiveMinimum = Value.Null;
				}
				return this.exclusiveMinimum;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x000533D9 File Offset: 0x000515D9
		public Value MaxLength
		{
			get
			{
				if (this.maxLength == null && !base.RawObject.TryGetValue("maxLength", out this.maxLength))
				{
					this.maxLength = Value.Null;
				}
				return this.maxLength;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x0005340C File Offset: 0x0005160C
		public Value MinLength
		{
			get
			{
				if (this.minLength == null && !base.RawObject.TryGetValue("minLength", out this.minLength))
				{
					this.minLength = Value.Null;
				}
				return this.minLength;
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x0005343F File Offset: 0x0005163F
		public Value MaxItems
		{
			get
			{
				if (this.maxItems == null && !base.RawObject.TryGetValue("maxItems", out this.maxItems))
				{
					this.maxItems = Value.Null;
				}
				return this.maxItems;
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x00053472 File Offset: 0x00051672
		public Value MinItems
		{
			get
			{
				if (this.minItems == null && !base.RawObject.TryGetValue("minItems", out this.minItems))
				{
					this.minItems = Value.Null;
				}
				return this.minItems;
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x000534A5 File Offset: 0x000516A5
		public Value UniqueItems
		{
			get
			{
				if (this.uniqueItems == null && !base.RawObject.TryGetValue("uniqueItems", out this.uniqueItems))
				{
					this.uniqueItems = Value.Null;
				}
				return this.uniqueItems;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x000534D8 File Offset: 0x000516D8
		public Value EnumType
		{
			get
			{
				if (this.enumType == null && !base.RawObject.TryGetValue("enum", out this.enumType))
				{
					this.enumType = Value.Null;
				}
				return this.enumType;
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x0005350B File Offset: 0x0005170B
		public Value MultipleOf
		{
			get
			{
				if (this.multipleOf == null && !base.RawObject.TryGetValue("multipleOf", out this.multipleOf))
				{
					this.multipleOf = Value.Null;
				}
				return this.multipleOf;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06001FEE RID: 8174
		protected abstract IEngineHost EngineHost { get; }

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x0005353E File Offset: 0x0005173E
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x00053546 File Offset: 0x00051746
		private bool TypeConstructionBegan { get; set; }

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x00053550 File Offset: 0x00051750
		private string Format
		{
			get
			{
				if (this.format == null)
				{
					Value value;
					this.format = (base.RawObject.TryGetValue("format", out value) ? value.AsString : string.Empty);
				}
				return this.format;
			}
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x00053594 File Offset: 0x00051794
		public TypeFacets GetFacets()
		{
			List<RecordKeyDefinition> facetsRecords = this.GetFacetsRecords();
			RecordBuilder recordBuilder = new RecordBuilder(facetsRecords.Count);
			recordBuilder.Add(facetsRecords);
			return TypeFacets.FromRecord(recordBuilder.ToRecord());
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000535CC File Offset: 0x000517CC
		protected virtual List<RecordKeyDefinition> GetFacetsRecords()
		{
			List<RecordKeyDefinition> list = new List<RecordKeyDefinition>();
			if (this.MaxLength != Value.Null)
			{
				list.Add(new RecordKeyDefinition("MaxLength", this.MaxLength.AsNumber, TypeValue.Number));
			}
			return list;
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00053610 File Offset: 0x00051810
		protected override List<RecordKeyDefinition> GetMetaDataRecords()
		{
			List<RecordKeyDefinition> metaDataRecords = base.GetMetaDataRecords();
			if (this.DefaultValue != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("default", this.DefaultValue, TypeValue.Any));
			}
			if (this.Maximum != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("maximum", this.Maximum as NumberValue, TypeValue.Number));
			}
			if (this.Minimum != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("minimum", this.Minimum as NumberValue, TypeValue.Number));
			}
			if (this.ExclusiveMaximum != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("exclusiveMaximum", this.ExclusiveMaximum as LogicalValue, TypeValue.Logical));
			}
			if (this.ExclusiveMinimum != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("exclusiveMinimum", this.ExclusiveMinimum as LogicalValue, TypeValue.Logical));
			}
			if (this.MaxLength != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("maxLength", this.MaxLength as NumberValue, TypeValue.Number));
			}
			if (this.MinLength != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("minLength", this.MinLength as NumberValue, TypeValue.Number));
			}
			if (this.MaxItems != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("maxItems", this.MaxItems as NumberValue, TypeValue.Number));
			}
			if (this.MinItems != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("minItems", this.MinItems as NumberValue, TypeValue.Number));
			}
			if (this.UniqueItems != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("uniqueItems", this.UniqueItems as LogicalValue, TypeValue.Logical));
			}
			if (this.MultipleOf != Value.Null)
			{
				metaDataRecords.Add(new RecordKeyDefinition("multipleOf", this.MultipleOf as NumberValue, TypeValue.Number));
			}
			return metaDataRecords;
		}

		// Token: 0x06001FF5 RID: 8181
		protected abstract TypeValue ConstructObjectType();

		// Token: 0x06001FF6 RID: 8182
		protected abstract TypeValue ConstructArrayElementType();

		// Token: 0x06001FF7 RID: 8183 RVA: 0x00053810 File Offset: 0x00051A10
		private static TypeValue ConstructTypeOfStringByFormat(string format)
		{
			if ("date-time".Equals(format))
			{
				return TypeValue.DateTimeZone;
			}
			if ("full-date".Equals(format))
			{
				return TypeValue.Date;
			}
			if ("full-time".Equals(format))
			{
				return TypeValue.Time;
			}
			if ("uri".Equals(format))
			{
				return TypeValue.Uri;
			}
			return TypeValue.Text;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0005386E File Offset: 0x00051A6E
		private static TypeValue ConstructTypeOfNumberByFormat(string format)
		{
			return TypeValue.Double;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x00053875 File Offset: 0x00051A75
		private static TypeValue ConstructTypeOfIntegerByFormat(string format)
		{
			if ("int64".Equals(format))
			{
				return TypeValue.Int64;
			}
			return TypeValue.Int32;
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x00053890 File Offset: 0x00051A90
		private TypeValue ConstructType(string type, string format)
		{
			if (type != null)
			{
				switch (type.Length)
				{
				case 4:
				{
					char c = type[0];
					if (c != 'f')
					{
						if (c == 'n')
						{
							if (type == "null")
							{
								return TypeValue.Null;
							}
						}
					}
					else if (!(type == "file"))
					{
					}
					break;
				}
				case 5:
					if (type == "array")
					{
						return this.ConstructArrayType();
					}
					break;
				case 6:
				{
					char c = type[0];
					if (c != 'n')
					{
						if (c != 'o')
						{
							if (c == 's')
							{
								if (type == "string")
								{
									return OpenApiPartialSchema.ConstructTypeOfStringByFormat(format);
								}
							}
						}
						else if (type == "object")
						{
							return this.ConstructObjectType();
						}
					}
					else if (type == "number")
					{
						return OpenApiPartialSchema.ConstructTypeOfNumberByFormat(format);
					}
					break;
				}
				case 7:
				{
					char c = type[0];
					if (c != 'b')
					{
						if (c == 'i')
						{
							if (type == "integer")
							{
								return OpenApiPartialSchema.ConstructTypeOfIntegerByFormat(format);
							}
						}
					}
					else if (type == "boolean")
					{
						return TypeValue.Logical;
					}
					break;
				}
				}
			}
			return TypeValue.Any;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000539C4 File Offset: 0x00051BC4
		private TypeValue ConstructArrayType()
		{
			TypeValue typeValue = this.ConstructArrayElementType();
			if (typeValue == null)
			{
				return TypeValue.List;
			}
			if (typeValue.IsRecordType)
			{
				return TableTypeValue.New(typeValue.AsRecordType);
			}
			return ListTypeValue.New(typeValue);
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x000539FC File Offset: 0x00051BFC
		private TypeValue AddEnumMetadata(TypeValue typeValue)
		{
			ListValue listValue = this.EnumType as ListValue;
			if (listValue != null)
			{
				List<Value> list = new List<Value>();
				foreach (IValueReference valueReference in listValue)
				{
					list.Add(valueReference.Value);
				}
				typeValue = ValueHelper.AdjustEnumTypeMetavalues<Value>(typeValue, list.ToArray());
			}
			return typeValue;
		}

		// Token: 0x04000C17 RID: 3095
		private TypeValue type;

		// Token: 0x04000C18 RID: 3096
		private string format;

		// Token: 0x04000C19 RID: 3097
		private Value defaultValue;

		// Token: 0x04000C1A RID: 3098
		private Value maximum;

		// Token: 0x04000C1B RID: 3099
		private Value exclusiveMaximum;

		// Token: 0x04000C1C RID: 3100
		private Value minimum;

		// Token: 0x04000C1D RID: 3101
		private Value exclusiveMinimum;

		// Token: 0x04000C1E RID: 3102
		private Value maxLength;

		// Token: 0x04000C1F RID: 3103
		private Value minLength;

		// Token: 0x04000C20 RID: 3104
		private Value maxItems;

		// Token: 0x04000C21 RID: 3105
		private Value minItems;

		// Token: 0x04000C22 RID: 3106
		private Value uniqueItems;

		// Token: 0x04000C23 RID: 3107
		private Value enumType;

		// Token: 0x04000C24 RID: 3108
		private Value multipleOf;
	}
}
