using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Parquet;
using ParquetSharp;
using ParquetSharp.Schema;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001FD5 RID: 8149
	internal abstract class SchemaElement
	{
		// Token: 0x06011067 RID: 69735 RVA: 0x003AB3F2 File Offset: 0x003A95F2
		protected SchemaElement(string name, Repetition repetition, LogicalTypeEnum logicalTypeType, Func<LogicalType> logicalTypeCtor, RepeatedTypeKind repeatedTypeKind = RepeatedTypeKind.Default)
		{
			this.name = name;
			this.repetition = repetition;
			this.repeatedTypeKind = repeatedTypeKind;
			this.logicalTypeType = logicalTypeType;
			this.logicalTypeCtor = logicalTypeCtor;
		}

		// Token: 0x06011068 RID: 69736 RVA: 0x003AB42D File Offset: 0x003A962D
		public static GroupSchemaElement CreateSchema(Node node, TableTypeValue typeValue, SchemaConfig config = null)
		{
			return SchemaElement.SchemaElementBuilder.BuildSchema(node, typeValue, config);
		}

		// Token: 0x17002CDE RID: 11486
		// (get) Token: 0x06011069 RID: 69737
		public abstract NodeType ElementType { get; }

		// Token: 0x17002CDF RID: 11487
		// (get) Token: 0x0601106A RID: 69738 RVA: 0x003AB437 File Offset: 0x003A9637
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002CE0 RID: 11488
		// (get) Token: 0x0601106B RID: 69739 RVA: 0x003AB43F File Offset: 0x003A963F
		public Repetition Repetition
		{
			get
			{
				return this.repetition;
			}
		}

		// Token: 0x17002CE1 RID: 11489
		// (get) Token: 0x0601106C RID: 69740 RVA: 0x003AB447 File Offset: 0x003A9647
		public LogicalTypeEnum LogicalTypeType
		{
			get
			{
				return this.logicalTypeType;
			}
		}

		// Token: 0x17002CE2 RID: 11490
		// (get) Token: 0x0601106D RID: 69741 RVA: 0x003AB44F File Offset: 0x003A964F
		public SchemaElement Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17002CE3 RID: 11491
		// (get) Token: 0x0601106E RID: 69742
		public abstract IList<PrimitiveSchemaElement> PrimitiveElements { get; }

		// Token: 0x17002CE4 RID: 11492
		// (get) Token: 0x0601106F RID: 69743
		public abstract TypeValue ItemTypeValue { get; }

		// Token: 0x17002CE5 RID: 11493
		// (get) Token: 0x06011070 RID: 69744 RVA: 0x003AB457 File Offset: 0x003A9657
		public TypeValue TypeValue
		{
			get
			{
				if (this.typeValue == null)
				{
					this.typeValue = SchemaElement.ApplyRepetition(this.ItemTypeValue, this.Repetition, this.repeatedTypeKind);
				}
				return this.typeValue;
			}
		}

		// Token: 0x17002CE6 RID: 11494
		// (get) Token: 0x06011071 RID: 69745 RVA: 0x003AB484 File Offset: 0x003A9684
		public SchemaElement[] Ancestors
		{
			get
			{
				if (this.ancestors == null)
				{
					if (this.Parent == null)
					{
						this.ancestors = new SchemaElement[1];
					}
					else
					{
						this.ancestors = new SchemaElement[this.Parent.Ancestors.Length + 1];
						Array.Copy(this.Parent.Ancestors, this.ancestors, this.Parent.Ancestors.Length);
					}
					this.ancestors[this.ancestors.Length - 1] = this;
				}
				return this.ancestors;
			}
		}

		// Token: 0x17002CE7 RID: 11495
		// (get) Token: 0x06011072 RID: 69746 RVA: 0x003AB504 File Offset: 0x003A9704
		public string[] Path
		{
			get
			{
				if (this.path == null)
				{
					this.path = new string[this.Ancestors.Length - 1];
					for (int i = 1; i < this.Ancestors.Length; i++)
					{
						this.path[i - 1] = this.Ancestors[i].Name;
					}
				}
				return this.path;
			}
		}

		// Token: 0x17002CE8 RID: 11496
		// (get) Token: 0x06011073 RID: 69747 RVA: 0x003AB55E File Offset: 0x003A975E
		public string PathString
		{
			get
			{
				return string.Join(".", this.Path);
			}
		}

		// Token: 0x17002CE9 RID: 11497
		// (get) Token: 0x06011074 RID: 69748 RVA: 0x003AB570 File Offset: 0x003A9770
		public short DefinitionLevel
		{
			get
			{
				if (this.definitionLevel < 0)
				{
					if (this.Parent == null)
					{
						this.definitionLevel = 0;
					}
					else
					{
						this.definitionLevel = ((this.Repetition == Repetition.Required) ? this.Parent.DefinitionLevel : (this.Parent.DefinitionLevel + 1));
					}
				}
				return this.definitionLevel;
			}
		}

		// Token: 0x17002CEA RID: 11498
		// (get) Token: 0x06011075 RID: 69749 RVA: 0x003AB5C8 File Offset: 0x003A97C8
		public short RepetitionLevel
		{
			get
			{
				if (this.repetitionLevel < 0)
				{
					if (this.Parent == null)
					{
						this.repetitionLevel = 0;
					}
					else
					{
						this.repetitionLevel = ((this.repetition == Repetition.Repeated) ? (this.Parent.RepetitionLevel + 1) : this.Parent.RepetitionLevel);
					}
				}
				return this.repetitionLevel;
			}
		}

		// Token: 0x06011076 RID: 69750
		public abstract Value ToValue(object raw);

		// Token: 0x06011077 RID: 69751
		public abstract object FromValue(IAllocator allocator, Value value);

		// Token: 0x06011078 RID: 69752
		public abstract bool TrySelectColumns(NestedColumnSelection columnSelection, out SchemaElement schemaElement);

		// Token: 0x06011079 RID: 69753 RVA: 0x003AB61F File Offset: 0x003A981F
		public LogicalType CreateLogicalType()
		{
			return this.logicalTypeCtor();
		}

		// Token: 0x0601107A RID: 69754
		public abstract Node CreateNode();

		// Token: 0x0601107B RID: 69755 RVA: 0x003AB62C File Offset: 0x003A982C
		protected static void SetParent(SchemaElement schemaElement, SchemaElement parent)
		{
			schemaElement.parent = parent;
		}

		// Token: 0x0601107C RID: 69756 RVA: 0x003AB638 File Offset: 0x003A9838
		private static TypeValue ApplyRepetition(TypeValue typeValue, Repetition repetition, RepeatedTypeKind repeatedTypeKind = RepeatedTypeKind.Default)
		{
			switch (repetition)
			{
			case Repetition.Required:
				return typeValue;
			case Repetition.Optional:
				return typeValue.Nullable.NewMeta(typeValue.MetaValue).AsType;
			case Repetition.Repeated:
				switch (repeatedTypeKind)
				{
				case RepeatedTypeKind.Default:
					if (typeValue.IsRecordType && !typeValue.IsNullable)
					{
						return TableTypeValue.New(typeValue.AsRecordType);
					}
					return ListTypeValue.New(typeValue);
				case RepeatedTypeKind.List:
					return ListTypeValue.New(typeValue);
				case RepeatedTypeKind.Table:
					return TableTypeValue.New(typeValue.AsRecordType);
				}
				break;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x040066DA RID: 26330
		private readonly string name;

		// Token: 0x040066DB RID: 26331
		private readonly Repetition repetition;

		// Token: 0x040066DC RID: 26332
		private readonly LogicalTypeEnum logicalTypeType;

		// Token: 0x040066DD RID: 26333
		protected readonly RepeatedTypeKind repeatedTypeKind;

		// Token: 0x040066DE RID: 26334
		protected readonly Func<LogicalType> logicalTypeCtor;

		// Token: 0x040066DF RID: 26335
		private SchemaElement parent;

		// Token: 0x040066E0 RID: 26336
		private SchemaElement[] ancestors;

		// Token: 0x040066E1 RID: 26337
		private string[] path;

		// Token: 0x040066E2 RID: 26338
		private TypeValue typeValue;

		// Token: 0x040066E3 RID: 26339
		private short definitionLevel = -1;

		// Token: 0x040066E4 RID: 26340
		private short repetitionLevel = -1;

		// Token: 0x02001FD6 RID: 8150
		private sealed class SchemaElementBuilder
		{
			// Token: 0x0601107D RID: 69757 RVA: 0x003AB6C4 File Offset: 0x003A98C4
			private SchemaElementBuilder(string debugName = null, SchemaElement.SchemaElementBuilder parent = null, SchemaConfig config = null)
			{
				this.debugName = debugName;
				this.parent = parent;
				this.config = config ?? ((parent != null) ? parent.config : null) ?? SchemaConfig.Default;
				this.repeatedTypeKind = RepeatedTypeKind.Default;
				this.seen = ((parent != null) ? parent.seen : null);
				this.depth = ((parent != null) ? parent.depth : 0);
			}

			// Token: 0x0601107E RID: 69758 RVA: 0x003AB730 File Offset: 0x003A9930
			public static GroupSchemaElement BuildSchema(Node node, TableTypeValue typeValue, SchemaConfig config = null)
			{
				return SchemaElement.SchemaElementBuilder.BuildSchema(node, (typeValue != null) ? typeValue.ItemType : null, config);
			}

			// Token: 0x0601107F RID: 69759 RVA: 0x003AB748 File Offset: 0x003A9948
			public static GroupSchemaElement BuildSchema(Node node, RecordTypeValue typeValue, SchemaConfig config = null)
			{
				if (node == null && typeValue == null)
				{
					throw new InvalidOperationException();
				}
				if (node != null && node.Parent != null)
				{
					throw new InvalidOperationException();
				}
				SchemaElement.SchemaElementBuilder schemaElementBuilder = new SchemaElement.SchemaElementBuilder("table", null, config);
				if (node == null)
				{
					schemaElementBuilder.seen = new Dictionary<TypeValue, SchemaElement.SchemaElementBuilder>();
					schemaElementBuilder.seen.Add(typeValue, schemaElementBuilder);
				}
				schemaElementBuilder.name = ((node == null) ? "schema" : node.Name);
				schemaElementBuilder.repetition = Repetition.Required;
				TypeValue typeValue2 = ((typeValue != null) ? typeValue.NewFacets(TypeFacets.None) : null);
				return (GroupSchemaElement)schemaElementBuilder.BuildElement(node, typeValue2);
			}

			// Token: 0x06011080 RID: 69760 RVA: 0x003AB7D8 File Offset: 0x003A99D8
			private SchemaElement BuildField(string name, Node node, TypeValue typeValue)
			{
				if (node == null && typeValue == null)
				{
					throw this.AddPositionInfo(ParquetTypeErrors.InsufficientTypeError());
				}
				SchemaElement.SchemaElementBuilder schemaElementBuilder = new SchemaElement.SchemaElementBuilder(name, this, null);
				if (node == null)
				{
					SchemaElement.SchemaElementBuilder schemaElementBuilder2;
					if (this.seen.TryGetValue(typeValue, out schemaElementBuilder2))
					{
						throw schemaElementBuilder.AddPositionInfo(ParquetTypeErrors.CyclicType(schemaElementBuilder2.GetDebugPath()));
					}
					this.seen.Add(typeValue, schemaElementBuilder);
					if (!ParquetGroupTypeMap.IsGenerated(typeValue))
					{
						schemaElementBuilder.depth++;
					}
					if (schemaElementBuilder.depth > this.config.MaxDepth)
					{
						throw schemaElementBuilder.AddPositionInfo(ParquetTypeErrors.MaxDepthExceeded(this.config.MaxDepth));
					}
				}
				schemaElementBuilder.name = ((node == null) ? name : node.Name);
				TypeValue typeValue2 = schemaElementBuilder.SetRepetition(node, typeValue);
				SchemaElement schemaElement = schemaElementBuilder.BuildElement(node, typeValue2);
				if (typeValue != null && schemaElement.TypeValue.TypeKind != typeValue.TypeKind && !schemaElement.TypeValue.IsCompatibleWith(typeValue))
				{
					schemaElement = schemaElementBuilder.BuildElement(node, TypeValue.None);
				}
				if (node == null)
				{
					this.seen.Remove(typeValue);
				}
				return schemaElement;
			}

			// Token: 0x06011081 RID: 69761 RVA: 0x003AB8D8 File Offset: 0x003A9AD8
			private SchemaElement BuildElement(Node node, TypeValue itemTypeValue)
			{
				this.typeMap = this.GetTypeMap(node, itemTypeValue);
				if (this.typeMap is ParquetGroupTypeMap)
				{
					GroupNode groupNode = node as GroupNode;
					return this.BuildGroup(groupNode, itemTypeValue);
				}
				if (this.typeMap is ParquetPrimitiveTypeMap)
				{
					PrimitiveNode primitiveNode = node as PrimitiveNode;
					return this.BuildPrimitive(primitiveNode, itemTypeValue);
				}
				throw new InvalidOperationException();
			}

			// Token: 0x06011082 RID: 69762 RVA: 0x003AB932 File Offset: 0x003A9B32
			private PrimitiveSchemaElement BuildPrimitive(PrimitiveNode node, TypeValue itemTypeValue)
			{
				return new PrimitiveSchemaElement(this.name, this.repetition, (ParquetPrimitiveTypeMap)this.typeMap, (node != null) ? node.ColumnOrder : ColumnOrder.Undefined, this.repeatedTypeKind);
			}

			// Token: 0x06011083 RID: 69763 RVA: 0x003AB964 File Offset: 0x003A9B64
			private GroupSchemaElement BuildGroup(GroupNode node, TypeValue itemTypeValue)
			{
				RecordTypeValue recordTypeValue = null;
				Keys keys = null;
				if (itemTypeValue != null)
				{
					recordTypeValue = ((ParquetGroupTypeMap)this.typeMap).FromTypeValue(itemTypeValue);
					if (recordTypeValue.NewFacets(TypeFacets.None).Equals(RecordTypeValue.Any))
					{
						recordTypeValue = null;
					}
					else
					{
						if (recordTypeValue.Open)
						{
							throw this.AddPositionInfo(ParquetTypeErrors.UnmappedTypeError(itemTypeValue, "IsOpenRecord", LogicalValue.True));
						}
						keys = recordTypeValue.FieldKeys;
					}
				}
				if (node != null)
				{
					int fieldCount = node.FieldCount;
					string[] array = new string[fieldCount];
					for (int i = 0; i < fieldCount; i++)
					{
						using (Node node2 = node.Field(i))
						{
							array[i] = node2.Name;
						}
					}
					Keys keys2 = ParquetGroupTypeMap.GenerateKeys(array);
					if (keys != null && !keys.Equals(keys2))
					{
						throw this.AddPositionInfo(ParquetTypeErrors.IncompatibleTypeError(itemTypeValue, "Fields", ListValue.New(keys.ToArray<string>()), ListValue.New(keys2.ToArray<string>())));
					}
					keys = keys2;
				}
				if (keys == null)
				{
					throw this.AddPositionInfo(ParquetTypeErrors.InsufficientTypeError(itemTypeValue, Array.Empty<string>()));
				}
				SchemaElement[] array2 = new SchemaElement[keys.Length];
				for (int j = 0; j < keys.Length; j++)
				{
					string text = keys[j];
					TypeValue typeValue = null;
					if (recordTypeValue != null)
					{
						bool flag;
						typeValue = recordTypeValue.GetFieldType(recordTypeValue.FieldKeys.IndexOfKey(text), out flag);
					}
					using (Node node3 = ((node != null) ? node.Field(j) : null))
					{
						array2[j] = this.BuildField(text, node3, typeValue);
					}
				}
				return new GroupSchemaElement(this.name, this.repetition, (ParquetGroupTypeMap)this.typeMap, keys, array2, this.repeatedTypeKind);
			}

			// Token: 0x06011084 RID: 69764 RVA: 0x003ABB24 File Offset: 0x003A9D24
			private TypeValue SetRepetition(Node node, TypeValue typeValue)
			{
				if (typeValue == null)
				{
					this.repetition = node.Repetition;
					return null;
				}
				if (node == null)
				{
					if (typeValue.IsNullable)
					{
						this.repetition = Repetition.Optional;
						return typeValue.NonNullable;
					}
					if (!typeValue.Facets.IsEmpty)
					{
						this.repetition = Repetition.Required;
						return typeValue;
					}
					if (typeValue.IsTableType && this.AllowRepeated(typeValue))
					{
						this.repetition = Repetition.Repeated;
						this.repeatedTypeKind = RepeatedTypeKind.Table;
						return typeValue.AsTableType.ItemType;
					}
					if (typeValue.IsListType && !typeValue.AsListType.ItemType.IsNullable && this.AllowRepeated(typeValue))
					{
						this.repetition = Repetition.Repeated;
						this.repeatedTypeKind = RepeatedTypeKind.List;
						return typeValue.AsListType.ItemType;
					}
					this.repetition = Repetition.Required;
					return typeValue;
				}
				else
				{
					Repetition repetition = ((node.Parent == null) ? Repetition.Required : node.Repetition);
					switch (repetition)
					{
					case Repetition.Required:
						this.repetition = repetition;
						return typeValue.NonNullable;
					case Repetition.Optional:
						if (!typeValue.IsNullable)
						{
							throw this.AddPositionInfo(ParquetTypeErrors.IncompatibleTypeError(typeValue, "Repetition", TextValue.New(Repetition.Required.ToString()), TextValue.New(Repetition.Optional.ToString())));
						}
						this.repetition = repetition;
						return typeValue.NonNullable;
					case Repetition.Repeated:
						if (typeValue.Facets.IsEmpty)
						{
							if (typeValue.TypeKind == ValueKind.Any)
							{
								this.repetition = repetition;
								return typeValue.NonNullable;
							}
							if (typeValue.IsListType)
							{
								this.repetition = repetition;
								this.repeatedTypeKind = RepeatedTypeKind.List;
								return typeValue.AsListType.ItemType.NonNullable;
							}
							if (typeValue.IsTableType)
							{
								this.repetition = repetition;
								this.repeatedTypeKind = RepeatedTypeKind.Table;
								return typeValue.AsTableType.ItemType;
							}
						}
						throw this.AddPositionInfo(ParquetTypeErrors.IncompatibleTypeError(typeValue, "Repetition", TextValue.New(typeValue.IsNullable ? Repetition.Optional.ToString() : Repetition.Required.ToString()), TextValue.New(Repetition.Repeated.ToString())));
					default:
						throw new InvalidOperationException();
					}
				}
			}

			// Token: 0x06011085 RID: 69765 RVA: 0x003ABD31 File Offset: 0x003A9F31
			private bool AllowRepeated(TypeValue typeValue)
			{
				return ParquetGroupTypeMap.IsGenerated(typeValue);
			}

			// Token: 0x06011086 RID: 69766 RVA: 0x003ABD3C File Offset: 0x003A9F3C
			private ParquetTypeMap GetTypeMap(Node node, TypeValue itemTypeValue)
			{
				ParquetTypeMap parquetTypeMap;
				try
				{
					parquetTypeMap = ParquetTypeMap.Map(node, itemTypeValue, this.config);
				}
				catch (ValueException ex)
				{
					throw this.AddPositionInfo(ex);
				}
				return parquetTypeMap;
			}

			// Token: 0x06011087 RID: 69767 RVA: 0x003ABD74 File Offset: 0x003A9F74
			private ValueException AddPositionInfo(ValueException exception)
			{
				string debugPath = this.GetDebugPath();
				RecordValue recordValue = RecordValue.New(new NamedValue[]
				{
					new NamedValue("SchemaElement", TextValue.New(debugPath))
				});
				Value value = exception.Value["Detail"];
				if (value.IsRecord)
				{
					recordValue = recordValue.Concatenate(Library.Record.RemoveFields.Invoke(value, ListValue.New(new string[] { "SchemaElement", "Inner" }), Library.MissingField.Ignore)).AsRecord;
				}
				recordValue = Library.Record.AddField.Invoke(recordValue, TextValue.New("Inner"), exception.Value).AsRecord;
				return ValueException.NewExpressionError<Message2>(Resources.UserSchemaError(debugPath, exception.MessageString ?? ""), recordValue, exception);
			}

			// Token: 0x06011088 RID: 69768 RVA: 0x003ABE38 File Offset: 0x003AA038
			private string GetDebugPath()
			{
				Stack<string> stack = new Stack<string>();
				SchemaElement.SchemaElementBuilder schemaElementBuilder = this;
				while (schemaElementBuilder != null && schemaElementBuilder.debugName != null)
				{
					stack.Push(schemaElementBuilder.debugName);
					schemaElementBuilder = schemaElementBuilder.parent;
				}
				return string.Join(".", stack);
			}

			// Token: 0x040066E5 RID: 26341
			private readonly string debugName;

			// Token: 0x040066E6 RID: 26342
			private readonly SchemaElement.SchemaElementBuilder parent;

			// Token: 0x040066E7 RID: 26343
			private readonly SchemaConfig config;

			// Token: 0x040066E8 RID: 26344
			private string name;

			// Token: 0x040066E9 RID: 26345
			private Repetition repetition;

			// Token: 0x040066EA RID: 26346
			private RepeatedTypeKind repeatedTypeKind;

			// Token: 0x040066EB RID: 26347
			private ParquetTypeMap typeMap;

			// Token: 0x040066EC RID: 26348
			private Dictionary<TypeValue, SchemaElement.SchemaElementBuilder> seen;

			// Token: 0x040066ED RID: 26349
			private int depth;
		}
	}
}
