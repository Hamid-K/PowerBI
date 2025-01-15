using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Extensions;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Tmdl;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000169 RID: 361
	internal sealed class TmdlObjectReader : IMetadataReader
	{
		// Token: 0x060016B3 RID: 5811 RVA: 0x00099565 File Offset: 0x00097765
		public TmdlObjectReader(TmdlObject tmdlObject)
			: this(tmdlObject, false)
		{
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0009956F File Offset: 0x0009776F
		private TmdlObjectReader(TmdlObject tmdlObject, bool isCalcTableColumn)
		{
			this.tmdlObject = tmdlObject;
			this.Initialize(isCalcTableColumn);
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x00099585 File Offset: 0x00097785
		public string PropertyName
		{
			get
			{
				return this.GetCurrentPropertyName();
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0009958D File Offset: 0x0009778D
		public bool CanReset
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00099590 File Offset: 0x00097790
		public ObjectId ReadObjectIdProperty()
		{
			throw new TomInternalException("ReadObjectIdProperty should never be called in TMDL serialization");
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x0009959C File Offset: 0x0009779C
		public ObjectType ReadObjectTypeProperty()
		{
			throw new TomInternalException("ReadObjectTypeProperty should never be called in TMDL serialization");
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000995A8 File Offset: 0x000977A8
		public string ReadStringProperty()
		{
			this.EnsureCanRead();
			return this.ReadStringPropertyImpl();
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000995B6 File Offset: 0x000977B6
		public int ReadInt32Property()
		{
			this.EnsureCanRead();
			return this.ReadInt32PropertyImpl();
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000995C4 File Offset: 0x000977C4
		public uint ReadUInt32Property()
		{
			this.EnsureCanRead();
			return this.ReadUInt32PropertyImpl();
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000995D2 File Offset: 0x000977D2
		public long ReadInt64Property()
		{
			this.EnsureCanRead();
			return this.ReadInt64PropertyImpl();
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x000995E0 File Offset: 0x000977E0
		public ulong ReadUInt64Property()
		{
			this.EnsureCanRead();
			return this.ReadUInt64PropertyImpl();
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000995EE File Offset: 0x000977EE
		public bool ReadBooleanProperty()
		{
			this.EnsureCanRead();
			return this.ReadBooleanPropertyImpl();
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x000995FC File Offset: 0x000977FC
		public DateTime ReadDateTimeProperty()
		{
			this.EnsureCanRead();
			return this.ReadDateTimePropertyImpl();
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x0009960A File Offset: 0x0009780A
		public double ReadDoubleProperty()
		{
			this.EnsureCanRead();
			return this.ReadDoublePropertyImpl();
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00099618 File Offset: 0x00097818
		public TEnum ReadEnumProperty<TEnum>()
		{
			this.EnsureCanRead();
			return this.ReadEnumPropertyImpl<TEnum>();
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00099626 File Offset: 0x00097826
		public TPropertyValue ReadProperty<TPropertyValue>()
		{
			this.EnsureCanRead();
			return (TPropertyValue)((object)this.ReadPropertyImpl(typeof(TPropertyValue)));
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00099643 File Offset: 0x00097843
		public ObjectPath ReadCrossLinkProperty()
		{
			this.EnsureCanRead();
			return this.ReadCrossLinkPropertyImpl();
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00099654 File Offset: 0x00097854
		public ObjectPath ReadCrossLinkProperty(Func<string, ObjectPath> nameToPathConverter)
		{
			this.EnsureCanRead();
			this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.CrossLinkProperty, new TmdlValueType?(TmdlValueType.ModelReference));
			TmdlModelReferenceValue tmdlModelReferenceValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlModelReferenceValue;
			if (tmdlModelReferenceValue == null || tmdlModelReferenceValue.ObjectName.IsEmpty)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			return this.ReturnValueAndMoveReaderPosition<ObjectPath>(nameToPathConverter(tmdlModelReferenceValue.ObjectName.Name));
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x000996DC File Offset: 0x000978DC
		public bool TryReadCustomJsonProperty(out JToken token)
		{
			this.EnsureCanRead();
			this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString, null);
			if (!this.properties[this.propertyIndex].TryGetCustomJsonPropertyToken(out token))
			{
				return false;
			}
			token = new JObject((JObject)token);
			return this.ReturnValueAndMoveReaderPosition<bool>(true);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00099738 File Offset: 0x00097938
		public TMetadataObject ReadSingleChildProperty<TMetadataObject>(SerializationActivityContext context) where TMetadataObject : MetadataObject
		{
			this.EnsureCanRead();
			bool flag = this.tmdlObject.ObjectType == ObjectType.Table;
			if (flag && !this.tmdlObject.Name.IsEmpty)
			{
				context.ActivityInfo.Add("SerializationActivity::ReaderActiveTable", this.tmdlObject.Name.Name);
			}
			TMetadataObject tmetadataObject;
			try
			{
				MetadataPropertyNature nature = this.properties[this.propertyIndex].Nature;
				if (nature != MetadataPropertyNature.ChildProperty && nature != (MetadataPropertyNature)268435462)
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchNature(this.GetCurrentPropertyName()), this.GetCurrentLocation());
				}
				TmdlObject child = this.properties[this.propertyIndex].GetChild();
				context.ActivityInfo["SerializationActivity::ChildDeserialization"] = child;
				tmetadataObject = this.ReturnValueAndMoveReaderPosition<TMetadataObject>(MetadataObject.CreateFromMetadataStream<TMetadataObject>(context, child.ObjectType, new TmdlObjectReader(child)));
			}
			finally
			{
				if (flag)
				{
					context.ActivityInfo.Remove("SerializationActivity::ReaderActiveTable");
				}
			}
			return tmetadataObject;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00099848 File Offset: 0x00097A48
		public IEnumerable<TMetadataObject> ReadChildCollectionProperty<TMetadataObject>(SerializationActivityContext context) where TMetadataObject : MetadataObject
		{
			this.EnsureCanRead();
			if (((this.tmdlObject.ObjectType == ObjectType.RelatedColumnDetails && string.Compare(this.GetCurrentPropertyName(), "groupByColumns", StringComparison.Ordinal) == 0) || (this.tmdlObject.ObjectType == ObjectType.Role && string.Compare(this.GetCurrentPropertyName(), "members", StringComparison.Ordinal) == 0)) && this.properties[this.propertyIndex].IsProperty)
			{
				this.EnsureValidPropertyNatureAndValueType((MetadataPropertyNature)536870912, new TmdlValueType?(TmdlValueType.Collection));
			}
			else
			{
				this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.ChildCollection, null);
				Utils.Verify(this.properties[this.propertyIndex].IsChildCollectionProperty);
			}
			bool flag = this.tmdlObject.ObjectType == ObjectType.Table;
			if (flag && !this.tmdlObject.Name.IsEmpty)
			{
				context.ActivityInfo.Add("SerializationActivity::ReaderActiveTable", this.tmdlObject.Name.Name);
			}
			IEnumerable<TMetadataObject> enumerable;
			try
			{
				ObjectType objectType = this.tmdlObject.ObjectType;
				if (objectType != ObjectType.Table)
				{
					if (objectType != ObjectType.Role)
					{
						if (objectType == ObjectType.RelatedColumnDetails)
						{
							if (string.Compare(this.GetCurrentPropertyName(), "groupByColumns", StringComparison.Ordinal) == 0)
							{
								TmdlCollectionValue tmdlCollectionValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlCollectionValue;
								if (tmdlCollectionValue == null)
								{
									throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("groupByColumns"), this.GetCurrentLocation());
								}
								string text;
								if (!context.TryGetActivityInfo<string>("SerializationActivity::ReaderActiveTable", out text))
								{
									throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyRequiresTableOnRead("groupByColumns"), this.GetCurrentLocation());
								}
								return this.ReturnValueAndMoveReaderPosition<IEnumerable<TMetadataObject>>(TmdlObjectReader.ReadRelatedColumnDetailsGroupByColumnsProperty<TMetadataObject>(context, text, tmdlCollectionValue, this.GetCurrentLocation()));
							}
						}
					}
					else if (string.Compare(this.GetCurrentPropertyName(), "members", StringComparison.Ordinal) == 0)
					{
						if (this.properties[this.propertyIndex].IsChildCollectionProperty)
						{
							return this.ReturnValueAndMoveReaderPosition<IEnumerable<TMetadataObject>>(TmdlObjectReader.ReadRoleMembersProperty<TMetadataObject>(context, this.properties[this.propertyIndex].GetChildren()));
						}
						if (!this.properties[this.propertyIndex].IsProperty)
						{
							throw new TomInternalException("How can that be? These are the only two valid options that should pass the parser!");
						}
						TmdlCollectionValue tmdlCollectionValue2 = this.properties[this.propertyIndex].GetProperty().Value as TmdlCollectionValue;
						if (tmdlCollectionValue2 == null)
						{
							throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("members"), this.GetCurrentLocation());
						}
						return this.ReturnValueAndMoveReaderPosition<IEnumerable<TMetadataObject>>(TmdlObjectReader.ReadRoleMembersProperty<TMetadataObject>(context, tmdlCollectionValue2, this.GetCurrentLocation()));
					}
				}
				else if (string.Compare(this.GetCurrentPropertyName(), "columns", StringComparison.Ordinal) == 0)
				{
					bool flag2 = TmdlObjectReader.IsCalcTableObject(this.tmdlObject);
					return this.ReturnValueAndMoveReaderPosition<IEnumerable<TMetadataObject>>(TmdlObjectReader.ReadTableColumnsProperty<TMetadataObject>(context, flag2, this.properties[this.propertyIndex].GetChildren()));
				}
				enumerable = this.ReturnValueAndMoveReaderPosition<IEnumerable<TMetadataObject>>(TmdlObjectReader.ReadChildCollectionPropertyImpl<TMetadataObject>(context, flag, this.properties[this.propertyIndex].GetChildren()));
			}
			catch (Exception)
			{
				if (flag)
				{
					context.ActivityInfo.Remove("SerializationActivity::ReaderActiveTable");
				}
				throw;
			}
			return enumerable;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00099B8C File Offset: 0x00097D8C
		public IMetadataReader ReadComplexProperty(bool canReset)
		{
			this.EnsureCanRead();
			MetadataPropertyNature nature = this.properties[this.propertyIndex].Nature;
			if ((nature & (MetadataPropertyNature)67108864) != (MetadataPropertyNature)67108864)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchNature(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			MetadataPropertyNature metadataPropertyNature = nature & (MetadataPropertyNature)(-67108865);
			if (metadataPropertyNature != MetadataPropertyNature.RegularProperty)
			{
				if (metadataPropertyNature == MetadataPropertyNature.ChildProperty)
				{
					if (this.tmdlObject.ObjectType == ObjectType.Culture && string.Compare(this.GetCurrentPropertyName(), "translations", StringComparison.Ordinal) == 0)
					{
						Utils.Verify(this.properties[this.propertyIndex].IsProperty);
						TmdlProperty property = this.properties[this.propertyIndex].GetProperty();
						Utils.Verify(property.Value != null && property.Value.Type == TmdlValueType.TranslationRoot);
						TmdlTranslationRootValue tmdlTranslationRootValue = (TmdlTranslationRootValue)property.Value;
						return this.ReturnValueAndMoveReaderPosition<TmdlObjectReader.TranslationsRootReader>(new TmdlObjectReader.TranslationsRootReader(tmdlTranslationRootValue, this.GetCurrentLocationImpl(property.SourceLocation)));
					}
				}
			}
			else if (this.tmdlObject.ObjectType == ObjectType.Partition && string.Compare(this.GetCurrentPropertyName(), "source", StringComparison.Ordinal) == 0)
			{
				Utils.Verify(this.properties[this.propertyIndex].IsProperty);
				TmdlProperty property2 = this.properties[this.propertyIndex].GetProperty();
				Utils.Verify(property2.Value != null && property2.Value.Type == TmdlValueType.Struct);
				TmdlStructValue tmdlStructValue = (TmdlStructValue)property2.Value;
				return this.ReturnValueAndMoveReaderPosition<TmdlObjectReader.StructPropertyReader>(new TmdlObjectReader.StructPropertyReader(tmdlStructValue, this.GetCurrentLocationImpl(property2.SourceLocation)));
			}
			throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyNoComplexProperty(this.GetCurrentPropertyName()), this.GetCurrentLocation());
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00099D58 File Offset: 0x00097F58
		public IEnumerable<IMetadataReader> ReadComplexPropertyCollection(bool canReset)
		{
			this.EnsureCanRead();
			this.EnsureValidPropertyNatureAndValueType((MetadataPropertyNature)67108872, null);
			throw new TomInternalException("This should only be called for a translations reader!");
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00099D89 File Offset: 0x00097F89
		public void Skip()
		{
			this.EnsureCanRead();
			this.propertyIndex++;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00099D9F File Offset: 0x00097F9F
		public void Reset()
		{
			this.propertyIndex = 0;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00099DA8 File Offset: 0x00097FA8
		public Exception CreateUnexpectedPropertyException(SerializationActivityContext context, UnexpectedPropertyClassification classification)
		{
			if (classification == UnexpectedPropertyClassification.IncompatiblePropertyValue)
			{
				int num = this.propertyIndex - 1;
				TmdlProperty property = this.properties[num].GetProperty();
				return TmdlObjectReader.CreateUnexpectedPropertyExceptionImpl(context, classification, this.properties[num].Name, property.Value.Value, this.GetCurrentLocationImpl(property.SourceLocation));
			}
			return TmdlObjectReader.CreateUnexpectedPropertyExceptionImpl(context, classification, this.GetCurrentPropertyName(), null, this.GetCurrentLocation());
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00099E1E File Offset: 0x0009801E
		public Exception CreateInvalidDataException(SerializationActivityContext context, string error, Exception e = null)
		{
			return TmdlObjectReader.CreateInvalidDataExceptionImpl(context, error, this.GetCurrentLocation(), e);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00099E30 File Offset: 0x00098030
		public Exception CreateInvalidChildException(SerializationActivityContext context, MetadataObject child, string error, Exception e = null)
		{
			TmdlSourceLocation tmdlSourceLocation = default(TmdlSourceLocation);
			ObjectType objectType = child.ObjectType;
			object obj;
			if (objectType != ObjectType.Role)
			{
				TmdlProperty tmdlProperty;
				if (objectType != ObjectType.GroupByColumn)
				{
					TmdlObject tmdlObject;
					if (context.TryGetActivityInfo<TmdlObject>("SerializationActivity::ChildDeserialization", out tmdlObject))
					{
						tmdlSourceLocation = tmdlObject.SourceLocation;
					}
				}
				else if (context.TryGetActivityInfo<TmdlProperty>("SerializationActivity::ChildDeserialization", out tmdlProperty))
				{
					tmdlSourceLocation = tmdlProperty.SourceLocation;
				}
			}
			else if (context.TryGetActivityInfo<object>("SerializationActivity::ChildDeserialization", out obj))
			{
				TmdlObject tmdlObject2 = obj as TmdlObject;
				if (tmdlObject2 != null)
				{
					tmdlSourceLocation = tmdlObject2.SourceLocation;
				}
				else
				{
					TmdlProperty tmdlProperty2 = obj as TmdlProperty;
					if (tmdlProperty2 != null)
					{
						tmdlSourceLocation = tmdlProperty2.SourceLocation;
					}
				}
			}
			if (e != null)
			{
				return new TmdlSerializationException(error, this.GetCurrentLocationImpl(tmdlSourceLocation), e);
			}
			return new TmdlSerializationException(error, this.GetCurrentLocationImpl(tmdlSourceLocation));
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00099EE8 File Offset: 0x000980E8
		internal static bool IsCalcGroupPartition(TmdlObject partition)
		{
			TmdlProperty propertyByName = partition.GetPropertyByName("sourceType", StringComparison.InvariantCultureIgnoreCase);
			if (propertyByName != null && propertyByName.Value != null)
			{
				TmdlEnumValue tmdlEnumValue = propertyByName.Value as TmdlEnumValue;
				if (tmdlEnumValue != null)
				{
					object obj = tmdlEnumValue.Value;
					if (obj is PartitionSourceType)
					{
						PartitionSourceType partitionSourceType = (PartitionSourceType)obj;
						if (partitionSourceType == PartitionSourceType.CalculationGroup)
						{
							TmdlProperty propertyByName2 = partition.GetPropertyByName("mode", StringComparison.InvariantCultureIgnoreCase);
							if (propertyByName2 != null && propertyByName2.Value != null)
							{
								TmdlEnumValue tmdlEnumValue2 = propertyByName2.Value as TmdlEnumValue;
								if (tmdlEnumValue2 != null)
								{
									obj = tmdlEnumValue2.Value;
									if (obj is ModeType)
									{
										ModeType modeType = (ModeType)obj;
										if (modeType == ModeType.Import)
										{
											return true;
										}
									}
								}
							}
							return false;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00099F86 File Offset: 0x00098186
		internal static TmdlObjectReader CreateReaderForTableColumn(TmdlObject table, TmdlObject column)
		{
			return new TmdlObjectReader(column, TmdlObjectReader.IsCalcTableObject(table));
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00099F94 File Offset: 0x00098194
		internal static bool TryIdentifyCrossReferenceTargetType(ObjectType @object, string propertyName, out ObjectType target)
		{
			target = ObjectType.Null;
			if (@object <= ObjectType.Level)
			{
				switch (@object)
				{
				case ObjectType.Model:
					if (propertyName == "defaultMeasure")
					{
						target = ObjectType.Measure;
					}
					break;
				case ObjectType.DataSource:
				case ObjectType.Table:
				case ObjectType.AttributeHierarchy:
					break;
				case ObjectType.Column:
					if (propertyName == "columnOrigin" || propertyName == "sortByColumn")
					{
						target = ObjectType.Column;
					}
					break;
				case ObjectType.Partition:
					if (!(propertyName == "queryGroup"))
					{
						if (!(propertyName == "dataSource"))
						{
							if (propertyName == "expressionSource")
							{
								target = ObjectType.Expression;
							}
						}
						else
						{
							target = ObjectType.DataSource;
						}
					}
					else
					{
						target = ObjectType.QueryGroup;
					}
					break;
				case ObjectType.Relationship:
					if (propertyName == "fromColumn" || propertyName == "toColumn")
					{
						target = ObjectType.Column;
					}
					break;
				default:
					if (@object == ObjectType.Level)
					{
						if (propertyName == "column")
						{
							target = ObjectType.Column;
						}
					}
					break;
				}
			}
			else if (@object != ObjectType.Variation)
			{
				if (@object != ObjectType.Expression)
				{
					if (@object == ObjectType.AlternateOf)
					{
						if (!(propertyName == "baseColumn"))
						{
							if (propertyName == "baseTable")
							{
								target = ObjectType.Table;
							}
						}
						else
						{
							target = ObjectType.Column;
						}
					}
				}
				else if (!(propertyName == "parameterValuesColumn"))
				{
					if (!(propertyName == "queryGroup"))
					{
						if (propertyName == "expressionSource")
						{
							target = ObjectType.Expression;
						}
					}
					else
					{
						target = ObjectType.QueryGroup;
					}
				}
				else
				{
					target = ObjectType.Column;
				}
			}
			else if (!(propertyName == "defaultHierarchy"))
			{
				if (!(propertyName == "defaultColumn"))
				{
					if (propertyName == "relationship")
					{
						target = ObjectType.Relationship;
					}
				}
				else
				{
					target = ObjectType.Column;
				}
			}
			else
			{
				target = ObjectType.Hierarchy;
			}
			return target > ObjectType.Null;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0009A14D File Offset: 0x0009834D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsCalcTableObject(TmdlObject table)
		{
			if (table.HasAnyChild(false))
			{
				return table.Children.Any(delegate(TmdlObject c)
				{
					if (c.ObjectType == ObjectType.Partition && c.DefaultProperty != null && c.DefaultProperty.Value.Type == TmdlValueType.Scalar)
					{
						TmdlEnumValue tmdlEnumValue = c.DefaultProperty.Value as TmdlEnumValue;
						if (tmdlEnumValue != null && tmdlEnumValue.Value != null)
						{
							object value = tmdlEnumValue.Value;
							if (value is PartitionSourceType)
							{
								PartitionSourceType partitionSourceType = (PartitionSourceType)value;
								return partitionSourceType == PartitionSourceType.Calculated;
							}
						}
					}
					return false;
				});
			}
			return false;
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0009A184 File Offset: 0x00098384
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryIdentifyPropertyNature(TmdlValue tmdlValue, out MetadataPropertyNature nature)
		{
			switch (tmdlValue.Type)
			{
			case TmdlValueType.String:
			case TmdlValueType.Scalar:
				nature = MetadataPropertyNature.RegularProperty;
				break;
			case TmdlValueType.Struct:
			case TmdlValueType.TranslationRoot:
				nature = (MetadataPropertyNature)67108870;
				break;
			case TmdlValueType.Collection:
				nature = (MetadataPropertyNature)536870912;
				break;
			case TmdlValueType.MetadataObject:
				nature = (MetadataPropertyNature)268435462;
				break;
			case TmdlValueType.ModelReference:
				nature = MetadataPropertyNature.CrossLinkProperty;
				break;
			default:
				nature = MetadataPropertyNature.None;
				break;
			}
			return nature > MetadataPropertyNature.None;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0009A1EC File Offset: 0x000983EC
		private static bool TryParseModelDataAccessOptions(ICollection<TmdlProperty> tmdlProperties, JObject options)
		{
			foreach (TmdlProperty tmdlProperty in tmdlProperties)
			{
				string name = tmdlProperty.Name;
				if (name == "fastCombine")
				{
					if (tmdlProperty.Value != null)
					{
						TmdlScalarValue<bool> tmdlScalarValue = tmdlProperty.Value as TmdlScalarValue<bool>;
						if (tmdlScalarValue != null)
						{
							JToken jtoken;
							if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken(TmdlObjectReader.ParseTmdlBoolValue(tmdlScalarValue), true, out jtoken))
							{
								return false;
							}
							options.Add("fastCombine", jtoken);
							continue;
						}
					}
					return false;
				}
				if (name == "legacyRedirects")
				{
					if (tmdlProperty.Value != null)
					{
						TmdlScalarValue<bool> tmdlScalarValue2 = tmdlProperty.Value as TmdlScalarValue<bool>;
						if (tmdlScalarValue2 != null)
						{
							JToken jtoken2;
							if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken(TmdlObjectReader.ParseTmdlBoolValue(tmdlScalarValue2), true, out jtoken2))
							{
								return false;
							}
							options.Add("legacyRedirects", jtoken2);
							continue;
						}
					}
					return false;
				}
				if (!(name == "returnErrorValuesAsNull"))
				{
					return false;
				}
				if (tmdlProperty.Value != null)
				{
					TmdlScalarValue<bool> tmdlScalarValue3 = tmdlProperty.Value as TmdlScalarValue<bool>;
					if (tmdlScalarValue3 != null)
					{
						JToken jtoken3;
						if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken(TmdlObjectReader.ParseTmdlBoolValue(tmdlScalarValue3), true, out jtoken3))
						{
							return false;
						}
						options.Add("returnErrorValuesAsNull", jtoken3);
						continue;
					}
				}
				return false;
			}
			return options.Count > 0;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0009A36C File Offset: 0x0009856C
		private static bool TryParseModelAutomaticAggregationOptions(ICollection<TmdlProperty> tmdlProperties, JObject options)
		{
			foreach (TmdlProperty tmdlProperty in tmdlProperties)
			{
				string name = tmdlProperty.Name;
				if (name == "queryCoverage")
				{
					if (tmdlProperty.Value != null)
					{
						TmdlScalarValue<double> tmdlScalarValue = tmdlProperty.Value as TmdlScalarValue<double>;
						if (tmdlScalarValue != null)
						{
							double? value = tmdlScalarValue.GetValue();
							JToken jtoken;
							if (value == null || !CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken(value.Value, true, out jtoken))
							{
								return false;
							}
							options.Add("queryCoverage", jtoken);
							continue;
						}
					}
					return false;
				}
				if (name == "detailTableMinRows")
				{
					if (tmdlProperty.Value != null)
					{
						TmdlScalarValue<long> tmdlScalarValue2 = tmdlProperty.Value as TmdlScalarValue<long>;
						if (tmdlScalarValue2 != null)
						{
							long? value2 = tmdlScalarValue2.GetValue();
							JToken jtoken2;
							if (value2 == null || !CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken(value2.Value, true, out jtoken2))
							{
								return false;
							}
							options.Add("detailTableMinRows", jtoken2);
							continue;
						}
					}
					return false;
				}
				if (name == "aggregationTableMaxRows")
				{
					if (tmdlProperty.Value != null)
					{
						TmdlScalarValue<long> tmdlScalarValue3 = tmdlProperty.Value as TmdlScalarValue<long>;
						if (tmdlScalarValue3 != null)
						{
							long? value3 = tmdlScalarValue3.GetValue();
							JToken jtoken3;
							if (value3 == null || !CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken(value3.Value, true, out jtoken3))
							{
								return false;
							}
							options.Add("aggregationTableMaxRows", jtoken3);
							continue;
						}
					}
					return false;
				}
				if (!(name == "aggregationTableSizeLimit"))
				{
					return false;
				}
				if (tmdlProperty.Value != null)
				{
					TmdlScalarValue<long> tmdlScalarValue4 = tmdlProperty.Value as TmdlScalarValue<long>;
					if (tmdlScalarValue4 != null)
					{
						long? value4 = tmdlScalarValue4.GetValue();
						JToken jtoken4;
						if (value4 == null || !CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken(value4.Value, true, out jtoken4))
						{
							return false;
						}
						options.Add("aggregationTableSizeLimit", jtoken4);
						continue;
					}
				}
				return false;
			}
			return options.Count > 0;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0009A59C File Offset: 0x0009879C
		private static bool TryParseStructuredDataSourceConnectionDetails(ICollection<TmdlProperty> tmdlProperties, JObject details)
		{
			foreach (TmdlProperty tmdlProperty in tmdlProperties)
			{
				string name = tmdlProperty.Name;
				if (name == "protocol")
				{
					if (tmdlProperty.Value != null)
					{
						TmdlStringValue tmdlStringValue = tmdlProperty.Value as TmdlStringValue;
						if (tmdlStringValue != null)
						{
							JToken jtoken;
							if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue.Value, true, out jtoken))
							{
								return false;
							}
							details.Add("protocol", jtoken);
							continue;
						}
					}
					return false;
				}
				if (!(name == "address"))
				{
					return false;
				}
				if (tmdlProperty.Value != null)
				{
					TmdlStructValue tmdlStructValue = tmdlProperty.Value as TmdlStructValue;
					if (tmdlStructValue != null)
					{
						JToken jtoken2;
						JObject jobject;
						if (details.TryGetValue("address", StringComparison.InvariantCultureIgnoreCase, ref jtoken2))
						{
							jobject = (JObject)jtoken2;
						}
						else
						{
							jobject = new JObject();
						}
						foreach (TmdlProperty tmdlProperty2 in tmdlStructValue.Properties)
						{
							if (tmdlProperty2.Value != null)
							{
								TmdlStringValue tmdlStringValue2 = tmdlProperty2.Value as TmdlStringValue;
								if (tmdlStringValue2 != null)
								{
									string name2 = tmdlProperty2.Name;
									if (name2 != null)
									{
										switch (name2.Length)
										{
										case 3:
											if (name2 == "url")
											{
												JToken jtoken3;
												if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken3))
												{
													return false;
												}
												jobject.Add("url", jtoken3);
												continue;
											}
											break;
										case 4:
										{
											char c = name2[0];
											if (c != 'p')
											{
												if (c == 'v')
												{
													if (name2 == "view")
													{
														JToken jtoken4;
														if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken4))
														{
															return false;
														}
														jobject.Add("view", jtoken4);
														continue;
													}
												}
											}
											else if (name2 == "path")
											{
												JToken jtoken5;
												if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken5))
												{
													return false;
												}
												jobject.Add("path", jtoken5);
												continue;
											}
											break;
										}
										case 5:
											if (name2 == "model")
											{
												JToken jtoken6;
												if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken6))
												{
													return false;
												}
												jobject.Add("model", jtoken6);
												continue;
											}
											break;
										case 6:
										{
											char c = name2[1];
											switch (c)
											{
											case 'b':
												if (name2 == "object")
												{
													JToken jtoken7;
													if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken7))
													{
														return false;
													}
													jobject.Add("object", jtoken7);
													continue;
												}
												break;
											case 'c':
												if (name2 == "schema")
												{
													JToken jtoken8;
													if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken8))
													{
														return false;
													}
													jobject.Add("schema", jtoken8);
													continue;
												}
												break;
											case 'd':
												break;
											case 'e':
												if (name2 == "server")
												{
													JToken jtoken9;
													if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken9))
													{
														return false;
													}
													jobject.Add("server", jtoken9);
													continue;
												}
												break;
											default:
												if (c == 'o')
												{
													if (name2 == "domain")
													{
														JToken jtoken10;
														if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken10))
														{
															return false;
														}
														jobject.Add("domain", jtoken10);
														continue;
													}
												}
												break;
											}
											break;
										}
										case 7:
											if (name2 == "account")
											{
												JToken jtoken11;
												if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken11))
												{
													return false;
												}
												jobject.Add("account", jtoken11);
												continue;
											}
											break;
										case 8:
										{
											char c = name2[0];
											if (c != 'd')
											{
												if (c != 'p')
												{
													if (c == 'r')
													{
														if (name2 == "resource")
														{
															JToken jtoken12;
															if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken12))
															{
																return false;
															}
															jobject.Add("resource", jtoken12);
															continue;
														}
													}
												}
												else if (name2 == "property")
												{
													JToken jtoken13;
													if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken13))
													{
														return false;
													}
													jobject.Add("property", jtoken13);
													continue;
												}
											}
											else if (name2 == "database")
											{
												JToken jtoken14;
												if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken14))
												{
													return false;
												}
												jobject.Add("database", jtoken14);
												continue;
											}
											break;
										}
										case 11:
											if (name2 == "contentType")
											{
												JToken jtoken15;
												if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken15))
												{
													return false;
												}
												jobject.Add("contentType", jtoken15);
												continue;
											}
											break;
										case 12:
											if (name2 == "emailAddress")
											{
												JToken jtoken16;
												if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken16))
												{
													return false;
												}
												jobject.Add("emailAddress", jtoken16);
												continue;
											}
											break;
										case 16:
											if (name2 == "connectionstring")
											{
												JToken jtoken17;
												if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken17))
												{
													return false;
												}
												jobject.Add("connectionstring", jtoken17);
												continue;
											}
											break;
										}
									}
									return false;
								}
							}
							return false;
						}
						if (jobject.Count > 0 && jobject.Parent == null)
						{
							details.Add("address", jobject);
							continue;
						}
						continue;
					}
				}
				return false;
			}
			return details.Count > 0;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0009AC4C File Offset: 0x00098E4C
		private static bool TryParseStructuredDataSourceOptions(ICollection<TmdlProperty> tmdlProperties, JObject options)
		{
			return options.Count > 0;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0009AC58 File Offset: 0x00098E58
		private static bool TryParseStructuredDataSourceCredential(ICollection<TmdlProperty> tmdlProperties, JObject credential)
		{
			foreach (TmdlProperty tmdlProperty in tmdlProperties)
			{
				string text = tmdlProperty.Name.ToCSharpCase();
				if (text == "AuthenticationKind")
				{
					if (tmdlProperty.Value != null)
					{
						TmdlStringValue tmdlStringValue = tmdlProperty.Value as TmdlStringValue;
						if (tmdlStringValue != null)
						{
							JToken jtoken;
							if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue.Value, true, out jtoken))
							{
								return false;
							}
							credential.Add("AuthenticationKind", jtoken);
							continue;
						}
					}
					return false;
				}
				if (text == "PrivacySetting")
				{
					if (tmdlProperty.Value != null)
					{
						TmdlStringValue tmdlStringValue2 = tmdlProperty.Value as TmdlStringValue;
						if (tmdlStringValue2 != null)
						{
							JToken jtoken2;
							if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue2.Value, true, out jtoken2))
							{
								return false;
							}
							credential.Add("PrivacySetting", jtoken2);
							continue;
						}
					}
					return false;
				}
				if (text == "Username")
				{
					if (tmdlProperty.Value != null)
					{
						TmdlStringValue tmdlStringValue3 = tmdlProperty.Value as TmdlStringValue;
						if (tmdlStringValue3 != null)
						{
							JToken jtoken3;
							if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue3.Value, true, out jtoken3))
							{
								return false;
							}
							credential.Add("Username", jtoken3);
							continue;
						}
					}
					return false;
				}
				if (text == "Password")
				{
					if (tmdlProperty.Value != null)
					{
						TmdlStringValue tmdlStringValue4 = tmdlProperty.Value as TmdlStringValue;
						if (tmdlStringValue4 != null)
						{
							JToken jtoken4;
							if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken((string)tmdlStringValue4.Value, true, out jtoken4))
							{
								return false;
							}
							credential.Add("Password", jtoken4);
							continue;
						}
					}
					return false;
				}
				if (!(text == "EncryptConnection"))
				{
					return false;
				}
				if (tmdlProperty.Value != null)
				{
					TmdlScalarValue<bool> tmdlScalarValue = tmdlProperty.Value as TmdlScalarValue<bool>;
					if (tmdlScalarValue != null)
					{
						JToken jtoken5;
						if (!CustomJsonPropertyHelper.TryConvertPrimitiveValueToToken(TmdlObjectReader.ParseTmdlBoolValue(tmdlScalarValue), true, out jtoken5))
						{
							return false;
						}
						credential.Add("EncryptConnection", jtoken5);
						continue;
					}
				}
				return false;
			}
			return credential.Count > 0;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x0009AEA4 File Offset: 0x000990A4
		private static bool TryGetCustomJsonPropertyAdditionalProperties(TmdlObject customJsonProperty, out JObject additionalProperties)
		{
			if (customJsonProperty.DefaultProperty != null)
			{
				TmdlStringValue tmdlStringValue = customJsonProperty.DefaultProperty.Value as TmdlStringValue;
				if (tmdlStringValue != null && tmdlStringValue.Lines != null && !tmdlStringValue.Lines.IsEmpty())
				{
					try
					{
						additionalProperties = JObject.Parse(string.Join(Environment.NewLine, tmdlStringValue.Lines));
						return true;
					}
					catch (JsonReaderException)
					{
						additionalProperties = null;
						return false;
					}
				}
			}
			additionalProperties = new JObject();
			return true;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0009AF20 File Offset: 0x00099120
		private static ObjectPath CreateCrossReferenceObjectPath(ObjectName path, ObjectType target, string propertyName, TmdlSourceLocation location)
		{
			ObjectPath objectPath = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
			if (target != ObjectType.Column && target - ObjectType.Measure > 1)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyInvalidTarget(target.ToString("G"), propertyName), location);
			}
			if (path.parts.Length != 2)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchTarget(target.ToString("G"), propertyName, ClientHostingManager.MarkAsRestrictedInformation(path.FullyQualifiedName, InfoRestrictionType.CCON)), location);
			}
			objectPath.Push(ObjectType.Table, path.parts[0]);
			objectPath.Push(target, path.parts[1]);
			return objectPath;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0009AFB5 File Offset: 0x000991B5
		private static IEnumerable<TMetadataObject> ReadChildCollectionPropertyImpl<TMetadataObject>(SerializationActivityContext context, bool isTable, ICollection<TmdlObject> objects) where TMetadataObject : MetadataObject
		{
			try
			{
				foreach (TmdlObject tmdlObject in objects)
				{
					context.ActivityInfo["SerializationActivity::ChildDeserialization"] = tmdlObject;
					yield return MetadataObject.CreateFromMetadataStream<TMetadataObject>(context, tmdlObject.ObjectType, new TmdlObjectReader(tmdlObject));
				}
				IEnumerator<TmdlObject> enumerator = null;
			}
			finally
			{
				if (isTable)
				{
					context.ActivityInfo.Remove("SerializationActivity::ReaderActiveTable");
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x0009AFD3 File Offset: 0x000991D3
		private static IEnumerable<TMetadataObject> ReadTableColumnsProperty<TMetadataObject>(SerializationActivityContext context, bool isCalcTableObject, ICollection<TmdlObject> objects) where TMetadataObject : MetadataObject
		{
			try
			{
				foreach (TmdlObject tmdlObject in objects)
				{
					context.ActivityInfo["SerializationActivity::ChildDeserialization"] = tmdlObject;
					yield return MetadataObject.CreateFromMetadataStream<TMetadataObject>(context, ObjectType.Column, new TmdlObjectReader(tmdlObject, isCalcTableObject));
				}
				IEnumerator<TmdlObject> enumerator = null;
			}
			finally
			{
				context.ActivityInfo.Remove("SerializationActivity::ReaderActiveTable");
			}
			yield break;
			yield break;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0009AFF1 File Offset: 0x000991F1
		private static IEnumerable<TMetadataObject> ReadRoleMembersProperty<TMetadataObject>(SerializationActivityContext context, ICollection<TmdlObject> objects) where TMetadataObject : MetadataObject
		{
			foreach (TmdlObject tmdlObject in objects)
			{
				context.ActivityInfo["SerializationActivity::ChildDeserialization"] = tmdlObject;
				TmdlProperty tmdlProperty;
				if (!tmdlObject.HasAnyProperty(false))
				{
					tmdlProperty = null;
				}
				else
				{
					tmdlProperty = tmdlObject.Properties.FirstOrDefault((TmdlProperty p) => string.Compare(p.Name, "identityProvider", StringComparison.InvariantCultureIgnoreCase) == 0);
				}
				TmdlProperty tmdlProperty2 = tmdlProperty;
				ModelRoleMember modelRoleMember;
				if (tmdlObject.DefaultProperty != null)
				{
					if (tmdlObject.DefaultProperty.Value != null && tmdlObject.DefaultProperty.Value.Type == TmdlValueType.Scalar)
					{
						TmdlEnumValue tmdlEnumValue = tmdlObject.DefaultProperty.Value as TmdlEnumValue;
						if (tmdlEnumValue != null)
						{
							object value = tmdlEnumValue.Value;
							if (!(value is TmdlRoleMemberType))
							{
								throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchEnumType(tmdlObject.DefaultProperty.Name, typeof(TmdlRoleMemberType).Name, tmdlEnumValue.Value.GetType().Name), tmdlObject.DefaultProperty.SourceLocation);
							}
							TmdlRoleMemberType tmdlRoleMemberType = (TmdlRoleMemberType)value;
							switch (tmdlRoleMemberType)
							{
							case TmdlRoleMemberType.Auto:
								modelRoleMember = new ExternalModelRoleMember
								{
									MemberName = tmdlObject.Name.Name,
									MemberType = RoleMemberType.Auto,
									IdentityProvider = ((tmdlProperty2 == null) ? "AzureAD" : ((TmdlStringValue)tmdlProperty2.Value).Lines.JoinLines(null))
								};
								goto IL_031F;
							case TmdlRoleMemberType.User:
								modelRoleMember = new ExternalModelRoleMember
								{
									MemberName = tmdlObject.Name.Name,
									MemberType = RoleMemberType.User,
									IdentityProvider = ((tmdlProperty2 == null) ? "AzureAD" : ((TmdlStringValue)tmdlProperty2.Value).Lines.JoinLines(null))
								};
								goto IL_031F;
							case TmdlRoleMemberType.Group:
								modelRoleMember = new ExternalModelRoleMember
								{
									MemberName = tmdlObject.Name.Name,
									MemberType = RoleMemberType.Group,
									IdentityProvider = ((tmdlProperty2 == null) ? "AzureAD" : ((TmdlStringValue)tmdlProperty2.Value).Lines.JoinLines(null))
								};
								goto IL_031F;
							case TmdlRoleMemberType.ActiveDirectory:
								if (tmdlProperty2 != null)
								{
									throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyUnexpected(tmdlProperty2.Name), tmdlProperty2.SourceLocation);
								}
								modelRoleMember = new WindowsModelRoleMember
								{
									MemberName = tmdlObject.Name.Name
								};
								goto IL_031F;
							default:
								throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyInvalidEnumValue(tmdlObject.DefaultProperty.Name, typeof(TmdlRoleMemberType).Name, tmdlRoleMemberType.ToString("G")), tmdlObject.DefaultProperty.SourceLocation);
							}
						}
					}
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(tmdlObject.DefaultProperty.Name), tmdlObject.DefaultProperty.SourceLocation);
				}
				modelRoleMember = new ExternalModelRoleMember
				{
					MemberName = tmdlObject.Name.Name,
					MemberType = RoleMemberType.User,
					IdentityProvider = ((tmdlProperty2 == null) ? "AzureAD" : ((TmdlStringValue)tmdlProperty2.Value).Lines.JoinLines(null))
				};
				IL_031F:
				yield return (TMetadataObject)((object)modelRoleMember);
			}
			IEnumerator<TmdlObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0009B008 File Offset: 0x00099208
		private static IEnumerable<TMetadataObject> ReadRoleMembersProperty<TMetadataObject>(SerializationActivityContext context, TmdlCollectionValue collection, TmdlSourceLocation location) where TMetadataObject : MetadataObject
		{
			foreach (TmdlProperty[] array in collection.Items)
			{
				TmdlProperty tmdlProperty = array.FirstOrDefault((TmdlProperty p) => string.Compare(p.Name, "adMember", StringComparison.Ordinal) == 0);
				ModelRoleMember modelRoleMember;
				if (tmdlProperty != null)
				{
					if (tmdlProperty.Value != null && tmdlProperty.Value.Type == TmdlValueType.String)
					{
						TmdlStringValue tmdlStringValue = tmdlProperty.Value as TmdlStringValue;
						if (tmdlStringValue != null)
						{
							context.ActivityInfo["SerializationActivity::ChildDeserialization"] = tmdlProperty;
							modelRoleMember = new WindowsModelRoleMember
							{
								MemberName = string.Join(Environment.NewLine, tmdlStringValue.Lines)
							};
							goto IL_03BA;
						}
					}
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("adMember"), location);
				}
				TmdlProperty tmdlProperty2 = array.FirstOrDefault((TmdlProperty p) => string.Compare(p.Name, "user", StringComparison.Ordinal) == 0);
				TmdlProperty tmdlProperty3 = array.FirstOrDefault((TmdlProperty p) => string.Compare(p.Name, "group", StringComparison.Ordinal) == 0);
				TmdlProperty tmdlProperty4 = array.FirstOrDefault((TmdlProperty p) => string.Compare(p.Name, "external", StringComparison.Ordinal) == 0);
				TmdlProperty tmdlProperty5 = array.FirstOrDefault((TmdlProperty p) => string.Compare(p.Name, "identityProvider", StringComparison.Ordinal) == 0);
				string text;
				RoleMemberType roleMemberType;
				if (tmdlProperty2 != null)
				{
					if (tmdlProperty3 != null || tmdlProperty4 != null)
					{
						throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchCollectionElements("members"), location);
					}
					if (tmdlProperty2.Value != null && tmdlProperty2.Value.Type == TmdlValueType.String)
					{
						TmdlStringValue tmdlStringValue2 = tmdlProperty2.Value as TmdlStringValue;
						if (tmdlStringValue2 != null)
						{
							context.ActivityInfo["SerializationActivity::ChildDeserialization"] = tmdlProperty2;
							text = string.Join(Environment.NewLine, tmdlStringValue2.Lines);
							roleMemberType = RoleMemberType.User;
							goto IL_033D;
						}
					}
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("user"), location);
				}
				else if (tmdlProperty3 != null)
				{
					if (tmdlProperty4 != null)
					{
						throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchCollectionElements("members"), location);
					}
					if (tmdlProperty3.Value != null && tmdlProperty3.Value.Type == TmdlValueType.String)
					{
						TmdlStringValue tmdlStringValue3 = tmdlProperty3.Value as TmdlStringValue;
						if (tmdlStringValue3 != null)
						{
							context.ActivityInfo["SerializationActivity::ChildDeserialization"] = tmdlProperty3;
							text = string.Join(Environment.NewLine, tmdlStringValue3.Lines);
							roleMemberType = RoleMemberType.Group;
							goto IL_033D;
						}
					}
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("group"), location);
				}
				else
				{
					if (tmdlProperty4 != null)
					{
						if (tmdlProperty4.Value != null && tmdlProperty4.Value.Type == TmdlValueType.String)
						{
							TmdlStringValue tmdlStringValue4 = tmdlProperty4.Value as TmdlStringValue;
							if (tmdlStringValue4 != null)
							{
								context.ActivityInfo["SerializationActivity::ChildDeserialization"] = tmdlProperty4;
								text = string.Join(Environment.NewLine, tmdlStringValue4.Lines);
								roleMemberType = RoleMemberType.Auto;
								goto IL_033D;
							}
						}
						throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("external"), location);
					}
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchCollectionElements("members"), location);
				}
				IL_033D:
				string text2;
				if (tmdlProperty5 != null)
				{
					if (tmdlProperty5.Value != null && tmdlProperty5.Value.Type == TmdlValueType.String)
					{
						TmdlStringValue tmdlStringValue5 = tmdlProperty5.Value as TmdlStringValue;
						if (tmdlStringValue5 != null)
						{
							text2 = string.Join(Environment.NewLine, tmdlStringValue5.Lines);
							goto IL_039C;
						}
					}
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("identityProvider"), location);
				}
				text2 = "AzureAD";
				IL_039C:
				modelRoleMember = new ExternalModelRoleMember
				{
					MemberName = text,
					IdentityProvider = text2,
					MemberType = roleMemberType
				};
				IL_03BA:
				yield return (TMetadataObject)((object)modelRoleMember);
			}
			IEnumerator<TmdlProperty[]> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0009B026 File Offset: 0x00099226
		private static IEnumerable<TMetadataObject> ReadRelatedColumnDetailsGroupByColumnsProperty<TMetadataObject>(SerializationActivityContext context, string activeTable, TmdlCollectionValue collection, TmdlSourceLocation location) where TMetadataObject : MetadataObject
		{
			foreach (TmdlProperty[] itemsGroup in collection.Items)
			{
				int i = 0;
				while (i < itemsGroup.Length)
				{
					if (itemsGroup[i] != null && itemsGroup[i].Value != null && itemsGroup[i].Value.Type == TmdlValueType.ModelReference)
					{
						TmdlModelReferenceValue tmdlModelReferenceValue = itemsGroup[i].Value as TmdlModelReferenceValue;
						if (tmdlModelReferenceValue != null)
						{
							if (tmdlModelReferenceValue.ObjectName.IsEmpty)
							{
								throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("groupByColumns"), location);
							}
							GroupByColumn groupByColumn = new GroupByColumn();
							groupByColumn.body.GroupingColumnID.Path = new ObjectPath(new KeyValuePair<ObjectType, string>[]
							{
								new KeyValuePair<ObjectType, string>(ObjectType.Table, activeTable),
								new KeyValuePair<ObjectType, string>(ObjectType.Column, tmdlModelReferenceValue.ObjectName.Name)
							});
							context.ActivityInfo["SerializationActivity::ChildDeserialization"] = itemsGroup[i];
							yield return (TMetadataObject)((object)groupByColumn);
							int num = i;
							i = num + 1;
							continue;
						}
					}
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("groupByColumns"), location);
				}
				itemsGroup = null;
			}
			IEnumerator<TmdlProperty[]> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0009B04C File Offset: 0x0009924C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Exception CreateUnexpectedPropertyExceptionImpl(SerializationActivityContext context, UnexpectedPropertyClassification classification, string propertyName, object value, TmdlSourceLocation location)
		{
			switch (classification)
			{
			case UnexpectedPropertyClassification.UnknownProperty:
				return new TmdlSerializationException(TomSR.Exception_TmdlPropertyUnknown(propertyName), location);
			case UnexpectedPropertyClassification.IncompatibleProperty:
				return new TmdlSerializationException(TomSR.Exception_TmdlPropertyIncompatible(propertyName, context.CompatibilityMode.ToString("G"), context.DbCompatibilityLevel.ToString()), location);
			case UnexpectedPropertyClassification.IncompatiblePropertyValue:
				return new TmdlSerializationException(TomSR.Exception_TmdlPropertyIncompatibleValue(propertyName, context.CompatibilityMode.ToString("G"), context.DbCompatibilityLevel.ToString(), ((Enum)value).ToString("G")), location);
			default:
				return new TmdlSerializationException(TomSR.Exception_TmdlPropertyUnexpected(propertyName), location);
			}
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0009B0FF File Offset: 0x000992FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Exception CreateInvalidDataExceptionImpl(SerializationActivityContext context, string error, TmdlSourceLocation location, Exception e)
		{
			if (e != null)
			{
				return new TmdlSerializationException(error, location, e);
			}
			return new TmdlSerializationException(error, location);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0009B114 File Offset: 0x00099314
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ParseTmdlBoolValue(TmdlScalarValue<bool> boolValue)
		{
			bool? value = boolValue.GetValue();
			return value == null || value.Value;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0009B13C File Offset: 0x0009933C
		private void Initialize(bool isCalcTableColumn)
		{
			this.properties = new List<TmdlObjectReader.Property>();
			if (!this.tmdlObject.Name.IsEmpty && !ObjectTreeHelper.IsKeyedObject(this.tmdlObject.ObjectType))
			{
				if (this.tmdlObject.ObjectType == ObjectType.QueryGroup)
				{
					this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("folder", MetadataPropertyNature.RegularProperty, new TmdlProperty("folder", new TmdlStringValue(this.tmdlObject.Name.Name, null, false))));
				}
				else
				{
					this.properties.Add(TmdlObjectReader.Property.CreateStubProperty("name", MetadataPropertyNature.NameProperty));
				}
			}
			if (this.tmdlObject.Description != null && !this.tmdlObject.Description.IsEmpty())
			{
				this.properties.Add(TmdlObjectReader.Property.CreateStubProperty("description", (MetadataPropertyNature)16777216));
			}
			ObjectType objectType;
			if (this.tmdlObject.Ordinal != null)
			{
				objectType = this.tmdlObject.ObjectType;
				if (objectType != ObjectType.Level)
				{
					if (objectType == ObjectType.CalculationItem)
					{
						this.properties.Add(TmdlObjectReader.Property.CreateStubProperty("ordinal", (MetadataPropertyNature)33554432));
					}
				}
				else
				{
					this.properties.Add(TmdlObjectReader.Property.CreateStubProperty("ordinal", (MetadataPropertyNature)33554432));
				}
			}
			objectType = this.tmdlObject.ObjectType;
			if (objectType <= ObjectType.Role)
			{
				switch (objectType)
				{
				case ObjectType.Model:
					this.ParseProperties(TmdlObjectReader.modelCustomJsonProperties);
					goto IL_02FF;
				case ObjectType.DataSource:
					this.ParseDataSourceObjectProperties();
					goto IL_02FF;
				case ObjectType.Table:
				case ObjectType.AttributeHierarchy:
					break;
				case ObjectType.Column:
					this.ParseProperties(null);
					if (this.properties.Any((TmdlObjectReader.Property p) => string.Compare(p.Name, "type", StringComparison.Ordinal) == 0))
					{
						goto IL_02FF;
					}
					if (this.tmdlObject.DefaultProperty != null)
					{
						this.properties.Insert(0, TmdlObjectReader.Property.CreateRegularProperty("type", MetadataPropertyNature.RegularProperty, new TmdlProperty("type", TmdlValue.FromEnum<ColumnType>(ColumnType.Calculated))));
						goto IL_02FF;
					}
					if (isCalcTableColumn)
					{
						this.properties.Insert(0, TmdlObjectReader.Property.CreateRegularProperty("type", MetadataPropertyNature.RegularProperty, new TmdlProperty("type", TmdlValue.FromEnum<ColumnType>(ColumnType.CalculatedTableColumn))));
						goto IL_02FF;
					}
					goto IL_02FF;
				case ObjectType.Partition:
					this.ParsePartitionObjectProperties();
					goto IL_02FF;
				default:
					if (objectType == ObjectType.Role)
					{
						this.ParseProperties(null);
						TmdlProperty tmdlProperty;
						if (this.tmdlObject.TryGetDeprecatedProperty("members", out tmdlProperty))
						{
							this.AddProperty(tmdlProperty);
							goto IL_02FF;
						}
						goto IL_02FF;
					}
					break;
				}
			}
			else
			{
				switch (objectType)
				{
				case ObjectType.ExtendedProperty:
					this.ParseExtendedPropertyObjectProperties();
					goto IL_02FF;
				case ObjectType.Expression:
					this.ParseProperties(null);
					if (!this.properties.Any((TmdlObjectReader.Property p) => string.Compare(p.Name, "kind", StringComparison.Ordinal) == 0))
					{
						this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("kind", MetadataPropertyNature.RegularProperty, new TmdlProperty("kind", TmdlValue.FromEnum<ExpressionKind>(ExpressionKind.M))));
						goto IL_02FF;
					}
					goto IL_02FF;
				case ObjectType.ColumnPermission:
				case ObjectType.DetailRowsDefinition:
					break;
				case ObjectType.RelatedColumnDetails:
					this.ParseRelatedColumnDetailsObjectProperties();
					goto IL_02FF;
				default:
					if (objectType == ObjectType.TimeUnitColumnAssociation)
					{
						this.ParseTimeUnitColumnAssociationObjectProperties();
						goto IL_02FF;
					}
					break;
				}
			}
			this.ParseProperties(null);
			IL_02FF:
			if (this.tmdlObject.HasAnyChild(false))
			{
				IList<KeyValuePair<ObjectType, IList<TmdlObject>>> list = TmdlSerializationHelper.MergeAndGroupChildObject(this.tmdlObject.Children.Where((TmdlObject c) => c.ObjectType > ObjectType.Null), this.tmdlObject.SourceLocation);
				bool flag = this.tmdlObject.ObjectType == ObjectType.Table;
				bool flag2 = false;
				bool flag3 = false;
				foreach (KeyValuePair<ObjectType, IList<TmdlObject>> keyValuePair in list)
				{
					for (int i = keyValuePair.Value.Count - 1; i >= 0; i--)
					{
						if (keyValuePair.Value[i].IsEmptyReferenceStub())
						{
							keyValuePair.Value.RemoveAt(i);
						}
					}
					objectType = keyValuePair.Key;
					if (objectType <= ObjectType.Level)
					{
						if (objectType == ObjectType.Partition)
						{
							for (int j = 0; j < keyValuePair.Value.Count; j++)
							{
								if (TmdlObjectReader.IsCalcGroupPartition(keyValuePair.Value[j]))
								{
									flag3 = true;
								}
							}
							continue;
						}
						if (objectType != ObjectType.Level)
						{
							continue;
						}
					}
					else if (objectType != ObjectType.RoleMembership)
					{
						if (objectType == ObjectType.CalculationGroup)
						{
							flag2 = true;
							continue;
						}
						if (objectType != ObjectType.CalculationItem)
						{
							continue;
						}
					}
					else
					{
						TmdlObjectReader.Property property = this.properties.FirstOrDefault((TmdlObjectReader.Property p) => string.Compare(p.Name, "members", StringComparison.Ordinal) == 0);
						if (property.IsValid)
						{
							throw TmdlSerializationException.CreateAmbiguousSourceException(TomSR.TmdlAmbiguousSourceError_RoleMembers, keyValuePair.Value[0], property.GetProperty());
						}
						continue;
					}
					for (int k = 0; k < keyValuePair.Value.Count; k++)
					{
						keyValuePair.Value[k].Ordinal = new int?(k);
					}
				}
				if (flag && flag2 && !flag3)
				{
					TmdlObject tmdlObject = new TmdlObject(ObjectType.Partition);
					tmdlObject.DefaultProperty = new TmdlProperty("sourceType", TmdlValue.FromEnum<PartitionSourceType>(PartitionSourceType.CalculationGroup));
					tmdlObject.Properties.Add(new TmdlProperty("mode", TmdlValue.FromEnum<ModeType>(ModeType.Import)));
					IList<TmdlObject> list2 = (from kvp in list
						where kvp.Key == ObjectType.Partition
						select kvp.Value).FirstOrDefault<IList<TmdlObject>>();
					if (list2 == null)
					{
						list2 = new List<TmdlObject>();
						list.Add(new KeyValuePair<ObjectType, IList<TmdlObject>>(ObjectType.Partition, list2));
					}
					list2.Add(tmdlObject);
				}
				for (int l = 0; l < list.Count; l++)
				{
					string text3;
					if (list[l].Value.Count > 1)
					{
						string text;
						if (!ObjectTreeHelper.TryGetChildCollectionJsonPropertyName(this.tmdlObject.ObjectType, list[l].Key, out text))
						{
							if (this.tmdlObject.ObjectType == ObjectType.CalculationGroup && list[l].Key == ObjectType.CalculationExpression)
							{
								using (IEnumerator<TmdlObject> enumerator2 = list[l].Value.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										TmdlObject tmdlObject2 = enumerator2.Current;
										if (tmdlObject2.Name.IsEmpty)
										{
											throw new TmdlSerializationException(TomSR.Exception_TmdlObjectNoNameForChild(ObjectType.CalculationExpression.ToString("G"), ObjectType.CalculationGroup.ToString("G")), this.tmdlObject.SourceLocation);
										}
										string text2 = tmdlObject2.Name.Name;
										if (!(text2 == "multipleOrEmptySelectionExpression"))
										{
											if (!(text2 == "noSelectionExpression"))
											{
												throw new TmdlSerializationException(TomSR.Exception_TmdlObjectInvalidNameForChild(ObjectType.CalculationExpression.ToString("G"), ObjectType.CalculationGroup.ToString("G"), tmdlObject2.Name.Name), this.tmdlObject.SourceLocation);
											}
											this.properties.Add(TmdlObjectReader.Property.CreateChildProperty("noSelectionExpression", MetadataPropertyNature.ChildProperty, tmdlObject2, null));
										}
										else
										{
											this.properties.Add(TmdlObjectReader.Property.CreateChildProperty("multipleOrEmptySelectionExpression", MetadataPropertyNature.ChildProperty, tmdlObject2, null));
										}
									}
									goto IL_0A44;
								}
							}
							throw new TmdlSerializationException(TomSR.Exception_TmdlObjectInvalidChild(list[l].Key.ToString("G"), this.tmdlObject.ObjectType.ToString("G")), this.tmdlObject.SourceLocation);
						}
						this.properties.Add(TmdlObjectReader.Property.CreateChildCollectionProperty(text, MetadataPropertyNature.ChildCollection, list[l].Value));
					}
					else if (ObjectTreeHelper.TryGetChildJsonPropertyName(this.tmdlObject.ObjectType, list[l].Key, out text3))
					{
						this.properties.Add(TmdlObjectReader.Property.CreateChildProperty(text3, MetadataPropertyNature.ChildProperty, list[l].Value[0], null));
					}
					else if (ObjectTreeHelper.TryGetChildCollectionJsonPropertyName(this.tmdlObject.ObjectType, list[l].Key, out text3))
					{
						this.properties.Add(TmdlObjectReader.Property.CreateChildCollectionProperty(text3, MetadataPropertyNature.ChildCollection, list[l].Value));
					}
					else
					{
						if (this.tmdlObject.ObjectType != ObjectType.CalculationGroup || list[l].Key != ObjectType.CalculationExpression)
						{
							throw new TmdlSerializationException(TomSR.Exception_TmdlObjectInvalidChild(list[l].Key.ToString("G"), this.tmdlObject.ObjectType.ToString("G")), this.tmdlObject.SourceLocation);
						}
						if (list[l].Value[0].Name.IsEmpty)
						{
							throw new TmdlSerializationException(TomSR.Exception_TmdlObjectNoNameForChild(ObjectType.CalculationExpression.ToString("G"), ObjectType.CalculationGroup.ToString("G")), this.tmdlObject.SourceLocation);
						}
						string text2 = list[l].Value[0].Name.Name;
						if (!(text2 == "multipleOrEmptySelectionExpression"))
						{
							if (!(text2 == "noSelectionExpression"))
							{
								throw new TmdlSerializationException(TomSR.Exception_TmdlObjectInvalidNameForChild(ObjectType.CalculationExpression.ToString("G"), ObjectType.CalculationGroup.ToString("G"), list[l].Value[0].Name.Name), this.tmdlObject.SourceLocation);
							}
							this.properties.Add(TmdlObjectReader.Property.CreateChildProperty("noSelectionExpression", MetadataPropertyNature.ChildProperty, list[l].Value[0], null));
						}
						else
						{
							this.properties.Add(TmdlObjectReader.Property.CreateChildProperty("multipleOrEmptySelectionExpression", MetadataPropertyNature.ChildProperty, list[l].Value[0], null));
						}
					}
					IL_0A44:;
				}
			}
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x0009BBD8 File Offset: 0x00099DD8
		private void ParseDataSourceObjectProperties()
		{
			if (this.tmdlObject.DefaultProperty != null)
			{
				TmdlEnumValue tmdlEnumValue = this.tmdlObject.DefaultProperty.Value as TmdlEnumValue;
				if (tmdlEnumValue == null)
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.tmdlObject.DefaultProperty.Name), this.GetCurrentLocationImpl(this.tmdlObject.DefaultProperty.SourceLocation));
				}
				object value = tmdlEnumValue.Value;
				if (!(value is DataSourceType))
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchEnumType(this.tmdlObject.DefaultProperty.Name, typeof(DataSourceType).FullName, tmdlEnumValue.Value.GetType().FullName), this.GetCurrentLocationImpl(this.tmdlObject.DefaultProperty.SourceLocation));
				}
				DataSourceType dataSourceType = (DataSourceType)value;
				if (dataSourceType != DataSourceType.Provider)
				{
					this.properties.Insert(0, TmdlObjectReader.Property.CreateRegularProperty("type", MetadataPropertyNature.RegularProperty, new TmdlProperty("type", TmdlValue.FromEnum<DataSourceType>(dataSourceType))));
				}
			}
			else
			{
				this.properties.Insert(0, TmdlObjectReader.Property.CreateRegularProperty("type", MetadataPropertyNature.RegularProperty, new TmdlProperty("type", TmdlValue.FromEnum<DataSourceType>(DataSourceType.Structured))));
			}
			this.ParsePropertiesImpl(TmdlObjectReader.dataSourceCustomJsonProperties);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0009BD08 File Offset: 0x00099F08
		private void ParsePartitionObjectProperties()
		{
			TmdlStructValue tmdlStructValue = new TmdlStructValue();
			TmdlProperty tmdlProperty = this.tmdlObject.DefaultProperty;
			if (tmdlProperty == null && this.tmdlObject.HasAnyProperty(false))
			{
				tmdlProperty = this.tmdlObject.Properties.FirstOrDefault((TmdlProperty p) => string.Compare(p.Name, "sourceType", StringComparison.InvariantCultureIgnoreCase) == 0);
				if (tmdlProperty != null)
				{
					this.tmdlObject.Properties.Remove(tmdlProperty);
				}
			}
			if (tmdlProperty != null)
			{
				TmdlEnumValue tmdlEnumValue = tmdlProperty.Value as TmdlEnumValue;
				if (tmdlEnumValue == null)
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(tmdlProperty.Name), this.GetCurrentLocationImpl(tmdlProperty.SourceLocation));
				}
				object value = tmdlEnumValue.Value;
				if (!(value is PartitionSourceType))
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchEnumType(tmdlProperty.Name, typeof(PartitionSourceType).FullName, tmdlEnumValue.Value.GetType().FullName), this.GetCurrentLocationImpl(tmdlProperty.SourceLocation));
				}
				PartitionSourceType partitionSourceType = (PartitionSourceType)value;
				TmdlObject tmdlObject;
				if (this.tmdlObject.HasAnyChild(true))
				{
					tmdlObject = this.tmdlObject.Children.FirstOrDefault((TmdlObject c) => c.ObjectType == ObjectType.Null && string.Compare(c.Name.Name, "source", StringComparison.InvariantCultureIgnoreCase) == 0);
				}
				else
				{
					tmdlObject = null;
				}
				tmdlStructValue.Properties.Add(new TmdlProperty("type", tmdlEnumValue));
				if (tmdlObject != null)
				{
					if (tmdlObject.DefaultProperty != null)
					{
						tmdlStructValue.Properties.Add(tmdlObject.DefaultProperty);
					}
					if (!tmdlObject.HasAnyProperty(false))
					{
						goto IL_02EE;
					}
					using (IEnumerator<TmdlProperty> enumerator = tmdlObject.Properties.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TmdlProperty tmdlProperty2 = enumerator.Current;
							tmdlStructValue.Properties.Add(tmdlProperty2);
						}
						goto IL_02EE;
					}
				}
				IList<string> list = new List<string>();
				switch (partitionSourceType)
				{
				case PartitionSourceType.Query:
					list.Add("query");
					list.Add("dataSource");
					break;
				case PartitionSourceType.Calculated:
					list.Add("expression");
					list.Add("retainDataTillForceCalculate");
					break;
				case PartitionSourceType.M:
					list.Add("expression");
					list.Add("attributes");
					break;
				case PartitionSourceType.Entity:
					list.Add("entityName");
					list.Add("schemaName");
					list.Add("dataSource");
					list.Add("expressionSource");
					break;
				case PartitionSourceType.PolicyRange:
					list.Add("start");
					list.Add("end");
					list.Add("granularity");
					list.Add("refreshBookmark");
					break;
				case PartitionSourceType.Parquet:
					list.Add("location");
					break;
				}
				for (int i = 0; i < list.Count; i++)
				{
					TmdlProperty tmdlProperty3;
					if (this.tmdlObject.TryGetDeprecatedProperty(list[i], out tmdlProperty3))
					{
						tmdlStructValue.Properties.Add(tmdlProperty3);
					}
				}
			}
			IL_02EE:
			this.ParsePropertiesImpl(null);
			if (!tmdlStructValue.IsEmpty)
			{
				this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("source", (MetadataPropertyNature)67108867, new TmdlProperty("source", tmdlStructValue)));
			}
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x0009C048 File Offset: 0x0009A248
		private void ParseExtendedPropertyObjectProperties()
		{
			TmdlProperty defaultProperty = this.tmdlObject.DefaultProperty;
			string text = null;
			if (defaultProperty != null && defaultProperty.Value != null)
			{
				if (defaultProperty.Value.Type == TmdlValueType.String)
				{
					TmdlStringValue tmdlStringValue = defaultProperty.Value as TmdlStringValue;
					if (tmdlStringValue != null)
					{
						if (!tmdlStringValue.IsNull)
						{
							text = string.Join("\n", tmdlStringValue.Lines);
							goto IL_0079;
						}
						goto IL_0079;
					}
				}
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.tmdlObject.DefaultProperty.Name), this.GetCurrentLocationImpl(defaultProperty.SourceLocation));
			}
			IL_0079:
			TmdlProperty propertyByName = this.tmdlObject.GetPropertyByName("type", StringComparison.Ordinal);
			ExtendedPropertyType extendedPropertyType;
			if (propertyByName == null)
			{
				JToken jtoken;
				if (text != null && TmdlSerializationHelper.TryParseJsonObject(text, out jtoken))
				{
					this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("type", MetadataPropertyNature.RegularProperty, new TmdlProperty("type", TmdlValue.FromEnum<ExtendedPropertyType>(ExtendedPropertyType.Json))));
					extendedPropertyType = ExtendedPropertyType.Json;
				}
				else
				{
					extendedPropertyType = ExtendedPropertyType.String;
				}
			}
			else
			{
				TmdlEnumValue tmdlEnumValue = propertyByName.Value as TmdlEnumValue;
				if (tmdlEnumValue == null)
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType("type"), this.GetCurrentLocationImpl(propertyByName.SourceLocation));
				}
				object value = tmdlEnumValue.Value;
				if (!(value is ExtendedPropertyType))
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchEnumType("type", typeof(ExtendedPropertyType).FullName, tmdlEnumValue.Value.GetType().FullName), this.GetCurrentLocationImpl(propertyByName.SourceLocation));
				}
				ExtendedPropertyType extendedPropertyType2 = (ExtendedPropertyType)value;
				if (extendedPropertyType2 != ExtendedPropertyType.String)
				{
					this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("type", MetadataPropertyNature.RegularProperty, propertyByName));
					extendedPropertyType = extendedPropertyType2;
				}
				else
				{
					extendedPropertyType = ExtendedPropertyType.String;
				}
			}
			if (defaultProperty != null)
			{
				if (extendedPropertyType != ExtendedPropertyType.String)
				{
					if (extendedPropertyType == ExtendedPropertyType.Json)
					{
						this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("value", MetadataPropertyNature.RegularProperty, defaultProperty));
					}
				}
				else
				{
					this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("value", MetadataPropertyNature.RegularProperty, defaultProperty));
				}
			}
			if (this.tmdlObject.HasAnyProperty(false))
			{
				foreach (TmdlProperty tmdlProperty in this.tmdlObject.Properties.Where((TmdlProperty p) => string.Compare(p.Name, "type", StringComparison.Ordinal) != 0))
				{
					this.AddProperty(tmdlProperty);
				}
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0009C288 File Offset: 0x0009A488
		private void ParseRelatedColumnDetailsObjectProperties()
		{
			List<TmdlProperty> list = new List<TmdlProperty>();
			if (this.tmdlObject.HasAnyProperty(false))
			{
				foreach (TmdlProperty tmdlProperty in this.tmdlObject.Properties)
				{
					if (tmdlProperty.Name == "groupByColumn")
					{
						list.Add(tmdlProperty);
					}
					else
					{
						this.AddProperty(tmdlProperty);
					}
				}
			}
			if (list.Count > 0)
			{
				TmdlCollectionValue tmdlCollectionValue = new TmdlCollectionValue();
				tmdlCollectionValue.Items.Add(list.ToArray());
				this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("groupByColumns", (MetadataPropertyNature)536870912, new TmdlProperty("groupByColumns", tmdlCollectionValue)));
				return;
			}
			TmdlProperty tmdlProperty2;
			if (this.tmdlObject.TryGetDeprecatedProperty("groupByColumns", out tmdlProperty2))
			{
				this.AddProperty(tmdlProperty2);
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0009C36C File Offset: 0x0009A56C
		private void ParseTimeUnitColumnAssociationObjectProperties()
		{
			TimeUnit timeUnit;
			if (!Enum.TryParse<TimeUnit>(this.tmdlObject.Name.Name, false, out timeUnit))
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyInvalidObjectKeyValue(ObjectType.TimeUnitColumnAssociation.ToString("G"), this.tmdlObject.Name.Name), this.tmdlObject.SourceLocation);
			}
			this.AddProperty(new TmdlProperty("timeUnit", TmdlValue.FromEnum<TimeUnit>(timeUnit)));
			if (this.tmdlObject.HasAnyProperty(false))
			{
				List<string> list = new List<string>();
				foreach (TmdlProperty tmdlProperty in this.tmdlObject.Properties)
				{
					string name = tmdlProperty.Name;
					if (!(name == "primaryColumn"))
					{
						if (!(name == "associatedColumn"))
						{
							this.AddProperty(tmdlProperty);
						}
						else
						{
							TmdlModelReferenceValue tmdlModelReferenceValue = tmdlProperty.Value as TmdlModelReferenceValue;
							if (tmdlModelReferenceValue == null)
							{
								throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchNature(tmdlProperty.Name), this.GetCurrentLocationImpl(tmdlProperty.SourceLocation));
							}
							list.Add(tmdlModelReferenceValue.ObjectName.Name);
						}
					}
					else
					{
						TmdlModelReferenceValue tmdlModelReferenceValue2 = tmdlProperty.Value as TmdlModelReferenceValue;
						if (tmdlModelReferenceValue2 == null)
						{
							throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchNature(tmdlProperty.Name), this.GetCurrentLocationImpl(tmdlProperty.SourceLocation));
						}
						this.AddProperty(new TmdlProperty(tmdlProperty.Name, new TmdlStringValue(tmdlModelReferenceValue2.ObjectName.Name, null, false)).WithSourceLocation(tmdlProperty.SourceLocation));
					}
				}
				if (list.Count > 0)
				{
					this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("associatedColumns", (MetadataPropertyNature)536870912, new TmdlProperty("associatedColumns", new TmdlStringValue(list.ToArray(), TmdlStringFormat.Inline, false))));
				}
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x0009C560 File Offset: 0x0009A760
		private void ParseProperties(ICollection<string> customJsonProperties)
		{
			if (this.tmdlObject.DefaultProperty != null)
			{
				ObjectType objectType = this.tmdlObject.ObjectType;
				if (objectType <= ObjectType.TablePermission)
				{
					if (objectType <= ObjectType.Measure)
					{
						if (objectType == ObjectType.Column)
						{
							this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("expression", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
							goto IL_02BB;
						}
						if (objectType == ObjectType.Measure)
						{
							this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("expression", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
							goto IL_02BB;
						}
					}
					else
					{
						if (objectType == ObjectType.Annotation)
						{
							this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("value", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
							goto IL_02BB;
						}
						if (objectType == ObjectType.LinguisticMetadata)
						{
							this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("content", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
							goto IL_02BB;
						}
						if (objectType == ObjectType.TablePermission)
						{
							this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("filterExpression", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
							goto IL_02BB;
						}
					}
				}
				else if (objectType <= ObjectType.FormatStringDefinition)
				{
					switch (objectType)
					{
					case ObjectType.Expression:
						this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("expression", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
						goto IL_02BB;
					case ObjectType.ColumnPermission:
						this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("metadataPermission", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
						goto IL_02BB;
					case ObjectType.DetailRowsDefinition:
						this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("expression", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
						goto IL_02BB;
					case ObjectType.RelatedColumnDetails:
					case ObjectType.GroupByColumn:
					case ObjectType.CalculationGroup:
						break;
					case ObjectType.CalculationItem:
						this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("expression", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
						goto IL_02BB;
					default:
						if (objectType == ObjectType.FormatStringDefinition)
						{
							this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("expression", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
							goto IL_02BB;
						}
						break;
					}
				}
				else
				{
					if (objectType == ObjectType.ChangedProperty)
					{
						this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("property", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
						goto IL_02BB;
					}
					if (objectType == ObjectType.DataCoverageDefinition)
					{
						this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("expression", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
						goto IL_02BB;
					}
					if (objectType == ObjectType.CalculationExpression)
					{
						this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty("expression", MetadataPropertyNature.RegularProperty, this.tmdlObject.DefaultProperty));
						goto IL_02BB;
					}
				}
				throw new TmdlSerializationException(TomSR.Exception_TmdlObjectInvalidDefaultProperty(this.tmdlObject.ObjectType.ToString("G")), this.tmdlObject.SourceLocation);
			}
			IL_02BB:
			this.ParsePropertiesImpl(customJsonProperties);
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x0009C830 File Offset: 0x0009AA30
		private void ParsePropertiesImpl(ICollection<string> customJsonProperties)
		{
			if (this.tmdlObject.HasAnyProperty(false))
			{
				foreach (TmdlProperty tmdlProperty in this.tmdlObject.Properties)
				{
					this.AddProperty(tmdlProperty);
				}
			}
			if (customJsonProperties != null)
			{
				if (this.tmdlObject.HasAnyChild(true))
				{
					IEnumerable<TmdlObject> children = this.tmdlObject.Children;
					Func<TmdlObject, bool> <>9__0;
					Func<TmdlObject, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (TmdlObject c) => c.ObjectType == ObjectType.Null && customJsonProperties.Contains(c.Name.Name));
					}
					foreach (TmdlObject tmdlObject in children.Where(func))
					{
						this.AddCustomJsonProperty(tmdlObject);
					}
				}
				foreach (string text in customJsonProperties)
				{
					TmdlProperty tmdlProperty2;
					if (this.tmdlObject.TryGetDeprecatedProperty(text, out tmdlProperty2))
					{
						this.AddCustomJsonProperty(tmdlProperty2);
					}
				}
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0009C974 File Offset: 0x0009AB74
		private void AddCustomJsonProperty(TmdlObject customJsonProperty)
		{
			JObject jobject;
			if (!TmdlObjectReader.TryGetCustomJsonPropertyAdditionalProperties(customJsonProperty, out jobject) || (customJsonProperty.HasAnyProperty(false) && !this.AddCustomJsonPropertyImpl(customJsonProperty.Name.Name, customJsonProperty.Properties, jobject)))
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlObjectInvalidCustomJsonProperty(this.tmdlObject.ObjectType.ToString("G"), customJsonProperty.Name.Name), this.GetCurrentLocationImpl(customJsonProperty.SourceLocation));
			}
			this.properties.Add(TmdlObjectReader.Property.CreateCustomJsonProperty(customJsonProperty.Name.Name, MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString, customJsonProperty, jobject));
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0009CA18 File Offset: 0x0009AC18
		private void AddCustomJsonProperty(TmdlProperty property)
		{
			if (property.Value == null || property.Value.Type != TmdlValueType.Struct)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(property.Name), this.GetCurrentLocationImpl(property.SourceLocation));
			}
			JObject jobject = new JObject();
			if (!this.AddCustomJsonPropertyImpl(property.Name, ((TmdlStructValue)property.Value).Properties, jobject))
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlObjectInvalidCustomJsonProperty(this.tmdlObject.ObjectType.ToString("G"), property.Name), this.GetCurrentLocationImpl(property.SourceLocation));
			}
			this.properties.Add(TmdlObjectReader.Property.CreateCustomJsonProperty(property.Name, MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString, property, jobject));
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0009CAD4 File Offset: 0x0009ACD4
		private bool AddCustomJsonPropertyImpl(string propertyName, ICollection<TmdlProperty> tmdlProperties, JObject json)
		{
			ObjectType objectType = this.tmdlObject.ObjectType;
			if (objectType != ObjectType.Model)
			{
				if (objectType != ObjectType.DataSource)
				{
					throw TomInternalException.Create("Invalid custom JSON property - owner={0}, name='{1}'", new object[]
					{
						this.tmdlObject.ObjectType,
						propertyName
					});
				}
				if (propertyName == "connectionDetails")
				{
					return TmdlObjectReader.TryParseStructuredDataSourceConnectionDetails(tmdlProperties, json);
				}
				if (propertyName == "options")
				{
					return TmdlObjectReader.TryParseStructuredDataSourceOptions(tmdlProperties, json);
				}
				if (!(propertyName == "credential"))
				{
					throw TomInternalException.Create("Invalid custom JSON property - owner={0}, name='{1}'", new object[]
					{
						ObjectType.DataSource,
						propertyName
					});
				}
				return TmdlObjectReader.TryParseStructuredDataSourceCredential(tmdlProperties, json);
			}
			else
			{
				if (propertyName == "dataAccessOptions")
				{
					return TmdlObjectReader.TryParseModelDataAccessOptions(tmdlProperties, json);
				}
				if (!(propertyName == "automaticAggregationOptions"))
				{
					throw TomInternalException.Create("Invalid custom JSON property - owner={0}, name='{1}'", new object[]
					{
						ObjectType.Model,
						propertyName
					});
				}
				return TmdlObjectReader.TryParseModelAutomaticAggregationOptions(tmdlProperties, json);
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0009CBCC File Offset: 0x0009ADCC
		private void AddProperty(TmdlProperty property)
		{
			if (property.Value == null)
			{
				return;
			}
			MetadataPropertyNature metadataPropertyNature;
			if (!TmdlObjectReader.TryIdentifyPropertyNature(property.Value, out metadataPropertyNature))
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyUnknownNature(property.Name), this.GetCurrentLocationImpl(property.SourceLocation));
			}
			if (metadataPropertyNature != (MetadataPropertyNature)268435462)
			{
				this.properties.Add(TmdlObjectReader.Property.CreateRegularProperty(property.Name, metadataPropertyNature, property));
				return;
			}
			TmdlMetadataObjectValue tmdlMetadataObjectValue = property.Value as TmdlMetadataObjectValue;
			if (tmdlMetadataObjectValue == null)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(property.Name), this.GetCurrentLocationImpl(property.SourceLocation));
			}
			this.properties.Add(TmdlObjectReader.Property.CreateChildProperty(property.Name, metadataPropertyNature, tmdlMetadataObjectValue.Object, property));
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x0009CC7C File Offset: 0x0009AE7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private string GetCurrentPropertyName()
		{
			if (this.propertyIndex < this.properties.Count)
			{
				return this.properties[this.propertyIndex].Name;
			}
			return null;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0009CCB7 File Offset: 0x0009AEB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void EnsureCanRead()
		{
			if (this.propertyIndex == this.properties.Count)
			{
				throw new InvalidOperationException(TomSR.Exception_TmdlReaderEOF);
			}
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0009CCD8 File Offset: 0x0009AED8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void EnsureValidPropertyNatureAndValueType(MetadataPropertyNature nature, TmdlValueType? valueType)
		{
			if (this.properties[this.propertyIndex].Nature != nature)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchNature(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			if (valueType != null)
			{
				if (!this.properties[this.propertyIndex].IsProperty)
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
				}
				TmdlValue value = this.properties[this.propertyIndex].GetProperty().Value;
				if (value == null || value.Type != valueType.Value)
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
				}
			}
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0009CD9C File Offset: 0x0009AF9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private TValue ReturnValueAndMoveReaderPosition<TValue>(TValue value)
		{
			try
			{
			}
			finally
			{
				this.propertyIndex++;
			}
			return value;
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0009CDD0 File Offset: 0x0009AFD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private string ReadStringPropertyImpl()
		{
			string currentPropertyName = this.GetCurrentPropertyName();
			if (currentPropertyName == "name")
			{
				this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.NameProperty, null);
				return this.ReturnValueAndMoveReaderPosition<string>(this.tmdlObject.Name.Name);
			}
			if (!(currentPropertyName == "description"))
			{
				this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.RegularProperty, new TmdlValueType?(TmdlValueType.String));
				TmdlStringValue tmdlStringValue = (TmdlStringValue)this.properties[this.propertyIndex].GetProperty().Value;
				return this.ReturnValueAndMoveReaderPosition<string>(tmdlStringValue.IsNull ? null : string.Join("\n", tmdlStringValue.Lines));
			}
			this.EnsureValidPropertyNatureAndValueType((MetadataPropertyNature)16777216, null);
			return this.ReturnValueAndMoveReaderPosition<string>(string.Join("\n", this.tmdlObject.Description));
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0009CEB0 File Offset: 0x0009B0B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int ReadInt32PropertyImpl()
		{
			ObjectType objectType = this.tmdlObject.ObjectType;
			if (objectType != ObjectType.Level)
			{
				if (objectType == ObjectType.CalculationItem)
				{
					if (string.Compare(this.GetCurrentPropertyName(), "ordinal", StringComparison.Ordinal) == 0)
					{
						return this.ReturnValueAndMoveReaderPosition<int>(this.tmdlObject.Ordinal.Value);
					}
				}
			}
			else if (string.Compare(this.GetCurrentPropertyName(), "ordinal", StringComparison.Ordinal) == 0)
			{
				return this.ReturnValueAndMoveReaderPosition<int>(this.tmdlObject.Ordinal.Value);
			}
			this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.RegularProperty, new TmdlValueType?(TmdlValueType.Scalar));
			TmdlScalarValue<int> tmdlScalarValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlScalarValue<int>;
			if (tmdlScalarValue == null)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			return this.ReturnValueAndMoveReaderPosition<int>(tmdlScalarValue.GetValue().Value);
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0009CF90 File Offset: 0x0009B190
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private uint ReadUInt32PropertyImpl()
		{
			this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.RegularProperty, new TmdlValueType?(TmdlValueType.Scalar));
			TmdlScalarValue<uint> tmdlScalarValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlScalarValue<uint>;
			if (tmdlScalarValue == null)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			return this.ReturnValueAndMoveReaderPosition<uint>(tmdlScalarValue.GetValue().Value);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0009CFFC File Offset: 0x0009B1FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private long ReadInt64PropertyImpl()
		{
			this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.RegularProperty, new TmdlValueType?(TmdlValueType.Scalar));
			TmdlScalarValue<long> tmdlScalarValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlScalarValue<long>;
			if (tmdlScalarValue == null)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			return this.ReturnValueAndMoveReaderPosition<long>(tmdlScalarValue.GetValue().Value);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0009D068 File Offset: 0x0009B268
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ulong ReadUInt64PropertyImpl()
		{
			this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.RegularProperty, new TmdlValueType?(TmdlValueType.Scalar));
			TmdlScalarValue<ulong> tmdlScalarValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlScalarValue<ulong>;
			if (tmdlScalarValue == null)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			return this.ReturnValueAndMoveReaderPosition<ulong>(tmdlScalarValue.GetValue().Value);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0009D0D4 File Offset: 0x0009B2D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool ReadBooleanPropertyImpl()
		{
			this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.RegularProperty, new TmdlValueType?(TmdlValueType.Scalar));
			TmdlScalarValue<bool> tmdlScalarValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlScalarValue<bool>;
			if (tmdlScalarValue == null)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			return this.ReturnValueAndMoveReaderPosition<bool>(TmdlObjectReader.ParseTmdlBoolValue(tmdlScalarValue));
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0009D138 File Offset: 0x0009B338
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private DateTime ReadDateTimePropertyImpl()
		{
			this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.RegularProperty, new TmdlValueType?(TmdlValueType.Scalar));
			TmdlScalarValue<DateTime> tmdlScalarValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlScalarValue<DateTime>;
			if (tmdlScalarValue == null)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			return this.ReturnValueAndMoveReaderPosition<DateTime>(tmdlScalarValue.GetValue().Value);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0009D1A4 File Offset: 0x0009B3A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private double ReadDoublePropertyImpl()
		{
			this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.RegularProperty, new TmdlValueType?(TmdlValueType.Scalar));
			TmdlScalarValue<double> tmdlScalarValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlScalarValue<double>;
			if (tmdlScalarValue == null)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			return this.ReturnValueAndMoveReaderPosition<double>(tmdlScalarValue.GetValue().Value);
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0009D210 File Offset: 0x0009B410
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private TEnum ReadEnumPropertyImpl<TEnum>()
		{
			this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.RegularProperty, new TmdlValueType?(TmdlValueType.Scalar));
			TmdlEnumValue tmdlEnumValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlEnumValue;
			if (tmdlEnumValue != null)
			{
				object value = tmdlEnumValue.Value;
				if (value is TEnum)
				{
					TEnum tenum = (TEnum)((object)value);
					return this.ReturnValueAndMoveReaderPosition<TEnum>(tenum);
				}
			}
			throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0009D288 File Offset: 0x0009B488
		private object ReadPropertyImpl(Type type)
		{
			if (type == typeof(string))
			{
				return this.ReadStringPropertyImpl();
			}
			if (type == typeof(int))
			{
				return this.ReadInt32PropertyImpl();
			}
			if (type == typeof(uint))
			{
				return this.ReadUInt32PropertyImpl();
			}
			if (type == typeof(long))
			{
				return this.ReadInt64PropertyImpl();
			}
			if (type == typeof(ulong))
			{
				return this.ReadUInt64PropertyImpl();
			}
			if (type == typeof(bool))
			{
				return this.ReadBooleanPropertyImpl();
			}
			if (type == typeof(DateTime))
			{
				return this.ReadDateTimePropertyImpl();
			}
			if (type.IsEnum)
			{
				this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.RegularProperty, new TmdlValueType?(TmdlValueType.Scalar));
				TmdlEnumValue tmdlEnumValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlEnumValue;
				if (tmdlEnumValue == null || !type.IsAssignableFrom(tmdlEnumValue.EnumType))
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
				}
				return this.ReturnValueAndMoveReaderPosition<object>(tmdlEnumValue.Value);
			}
			else
			{
				if (type == typeof(IEnumerable<string>))
				{
					this.EnsureValidPropertyNatureAndValueType((MetadataPropertyNature)536870912, null);
					TmdlProperty property = this.properties[this.propertyIndex].GetProperty();
					TmdlStringValue tmdlStringValue = property.Value as TmdlStringValue;
					IEnumerable<string> enumerable;
					if (tmdlStringValue != null)
					{
						enumerable = tmdlStringValue.Lines;
					}
					else
					{
						TmdlCollectionValue tmdlCollectionValue = property.Value as TmdlCollectionValue;
						if (tmdlCollectionValue == null)
						{
							throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
						}
						List<string> list = new List<string>(tmdlCollectionValue.Items.Count);
						foreach (TmdlProperty[] array in tmdlCollectionValue.Items)
						{
							int i = 0;
							while (i < array.Length)
							{
								if (array[i] != null && array[i].Value != null && array[i].Value.Type == TmdlValueType.String)
								{
									TmdlStringValue tmdlStringValue2 = array[i].Value as TmdlStringValue;
									if (tmdlStringValue2 != null)
									{
										if (!tmdlStringValue2.IsNull)
										{
											list.Add(string.Join("\n", tmdlStringValue2.Lines));
										}
										i++;
										continue;
									}
								}
								throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
							}
						}
						enumerable = list;
					}
					return this.ReturnValueAndMoveReaderPosition<IEnumerable<string>>(enumerable);
				}
				if (type == typeof(ObjectPath))
				{
					return this.ReadCrossLinkPropertyImpl();
				}
				throw TomInternalException.Create("Invalid property type - {0} is not a valid type for TMDL serialization", new object[] { type.FullName });
			}
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0009D57C File Offset: 0x0009B77C
		private ObjectPath ReadCrossLinkPropertyImpl()
		{
			this.EnsureValidPropertyNatureAndValueType(MetadataPropertyNature.CrossLinkProperty, new TmdlValueType?(TmdlValueType.ModelReference));
			TmdlModelReferenceValue tmdlModelReferenceValue = this.properties[this.propertyIndex].GetProperty().Value as TmdlModelReferenceValue;
			if (tmdlModelReferenceValue == null || tmdlModelReferenceValue.ObjectName.IsEmpty)
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			ObjectType objectType;
			if (!TmdlObjectReader.TryIdentifyCrossReferenceTargetType(this.tmdlObject.ObjectType, this.GetCurrentPropertyName(), out objectType))
			{
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyUnknownTarget(this.GetCurrentPropertyName()), this.GetCurrentLocation());
			}
			return this.ReturnValueAndMoveReaderPosition<ObjectPath>(TmdlObjectReader.CreateCrossReferenceObjectPath(tmdlModelReferenceValue.ObjectName, objectType, this.GetCurrentPropertyName(), this.GetCurrentLocation()));
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0009D634 File Offset: 0x0009B834
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private TmdlSourceLocation GetCurrentLocation()
		{
			if (this.propertyIndex < this.properties.Count)
			{
				MetadataPropertyNature nature = this.properties[this.propertyIndex].Nature;
				switch (nature)
				{
				case MetadataPropertyNature.RegularProperty:
				case MetadataPropertyNature.CrossLinkProperty:
					break;
				case MetadataPropertyNature.ParentProperty:
					goto IL_00DB;
				case MetadataPropertyNature.ChildProperty:
					if (this.properties[this.propertyIndex].IsChildProperty)
					{
						return this.GetCurrentLocationImpl(this.properties[this.propertyIndex].GetChild().SourceLocation);
					}
					goto IL_00DB;
				default:
					if (nature != (MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.JsonString) && nature != (MetadataPropertyNature)268435462)
					{
						goto IL_00DB;
					}
					break;
				}
				if (this.properties[this.propertyIndex].IsProperty)
				{
					return this.GetCurrentLocationImpl(this.properties[this.propertyIndex].GetProperty().SourceLocation);
				}
			}
			IL_00DB:
			return this.tmdlObject.SourceLocation;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0009D727 File Offset: 0x0009B927
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private TmdlSourceLocation GetCurrentLocationImpl(TmdlSourceLocation location)
		{
			if (!location.IsValid)
			{
				return this.tmdlObject.SourceLocation;
			}
			return location;
		}

		// Token: 0x04000431 RID: 1073
		private const string SerializationActivityInfoKey_CurrentTable = "SerializationActivity::ReaderActiveTable";

		// Token: 0x04000432 RID: 1074
		private const MetadataPropertyNature MetadataPropertyNature_Description = (MetadataPropertyNature)16777216;

		// Token: 0x04000433 RID: 1075
		private const MetadataPropertyNature MetadataPropertyNature_Ordinal = (MetadataPropertyNature)33554432;

		// Token: 0x04000434 RID: 1076
		private const MetadataPropertyNature MetadataPropertyNature_Complex = (MetadataPropertyNature)67108864;

		// Token: 0x04000435 RID: 1077
		private const MetadataPropertyNature MetadataPropertyNature_FoldedChild = (MetadataPropertyNature)268435456;

		// Token: 0x04000436 RID: 1078
		private const MetadataPropertyNature MetadataPropertyNature_RawCollection = (MetadataPropertyNature)536870912;

		// Token: 0x04000437 RID: 1079
		private static readonly ICollection<string> modelCustomJsonProperties = new List<string>(2) { "dataAccessOptions", "automaticAggregationOptions" };

		// Token: 0x04000438 RID: 1080
		private static readonly ICollection<string> dataSourceCustomJsonProperties = new List<string>(3) { "connectionDetails", "credential", "options" };

		// Token: 0x04000439 RID: 1081
		private readonly TmdlObject tmdlObject;

		// Token: 0x0400043A RID: 1082
		private IList<TmdlObjectReader.Property> properties;

		// Token: 0x0400043B RID: 1083
		private int propertyIndex;

		// Token: 0x02000353 RID: 851
		private struct Property
		{
			// Token: 0x060025C5 RID: 9669 RVA: 0x000E8D01 File Offset: 0x000E6F01
			private Property(string name, MetadataPropertyNature nature, TmdlProperty property, JToken token, TmdlObject child, ICollection<TmdlObject> children)
			{
				this.name = name;
				this.nature = nature;
				this.property = property;
				this.token = token;
				this.child = child;
				this.children = children;
			}

			// Token: 0x1700079E RID: 1950
			// (get) Token: 0x060025C6 RID: 9670 RVA: 0x000E8D30 File Offset: 0x000E6F30
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x1700079F RID: 1951
			// (get) Token: 0x060025C7 RID: 9671 RVA: 0x000E8D38 File Offset: 0x000E6F38
			public MetadataPropertyNature Nature
			{
				get
				{
					return this.nature;
				}
			}

			// Token: 0x170007A0 RID: 1952
			// (get) Token: 0x060025C8 RID: 9672 RVA: 0x000E8D40 File Offset: 0x000E6F40
			public bool IsProperty
			{
				get
				{
					return this.property != null;
				}
			}

			// Token: 0x170007A1 RID: 1953
			// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000E8D4B File Offset: 0x000E6F4B
			public bool IsCustomJsonProperty
			{
				get
				{
					return (this.property != null || this.child != null) && this.token != null;
				}
			}

			// Token: 0x170007A2 RID: 1954
			// (get) Token: 0x060025CA RID: 9674 RVA: 0x000E8D68 File Offset: 0x000E6F68
			public bool IsChildProperty
			{
				get
				{
					return this.child != null;
				}
			}

			// Token: 0x170007A3 RID: 1955
			// (get) Token: 0x060025CB RID: 9675 RVA: 0x000E8D73 File Offset: 0x000E6F73
			public bool IsChildCollectionProperty
			{
				get
				{
					return this.children != null;
				}
			}

			// Token: 0x170007A4 RID: 1956
			// (get) Token: 0x060025CC RID: 9676 RVA: 0x000E8D7E File Offset: 0x000E6F7E
			internal bool IsValid
			{
				get
				{
					return !string.IsNullOrEmpty(this.name);
				}
			}

			// Token: 0x060025CD RID: 9677 RVA: 0x000E8D8E File Offset: 0x000E6F8E
			public static TmdlObjectReader.Property CreateStubProperty(string name, MetadataPropertyNature nature)
			{
				return new TmdlObjectReader.Property(name, nature, null, null, null, null);
			}

			// Token: 0x060025CE RID: 9678 RVA: 0x000E8D9B File Offset: 0x000E6F9B
			public static TmdlObjectReader.Property CreateRegularProperty(string name, MetadataPropertyNature nature, TmdlProperty property)
			{
				return new TmdlObjectReader.Property(name, nature, property, null, null, null);
			}

			// Token: 0x060025CF RID: 9679 RVA: 0x000E8DA8 File Offset: 0x000E6FA8
			public static TmdlObjectReader.Property CreateCustomJsonProperty(string name, MetadataPropertyNature nature, TmdlProperty property, JToken token)
			{
				return new TmdlObjectReader.Property(name, nature, property, token, null, null);
			}

			// Token: 0x060025D0 RID: 9680 RVA: 0x000E8DB5 File Offset: 0x000E6FB5
			public static TmdlObjectReader.Property CreateCustomJsonProperty(string name, MetadataPropertyNature nature, TmdlObject customJsonProperty, JToken token)
			{
				return new TmdlObjectReader.Property(name, nature, null, token, customJsonProperty, null);
			}

			// Token: 0x060025D1 RID: 9681 RVA: 0x000E8DC2 File Offset: 0x000E6FC2
			public static TmdlObjectReader.Property CreateChildProperty(string name, MetadataPropertyNature nature, TmdlObject child, TmdlProperty property = null)
			{
				return new TmdlObjectReader.Property(name, nature, property, null, child, null);
			}

			// Token: 0x060025D2 RID: 9682 RVA: 0x000E8DCF File Offset: 0x000E6FCF
			public static TmdlObjectReader.Property CreateChildCollectionProperty(string name, MetadataPropertyNature nature, ICollection<TmdlObject> children)
			{
				return new TmdlObjectReader.Property(name, nature, null, null, null, children);
			}

			// Token: 0x060025D3 RID: 9683 RVA: 0x000E8DDC File Offset: 0x000E6FDC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TmdlProperty GetProperty()
			{
				Utils.Verify(this.property != null);
				return this.property;
			}

			// Token: 0x060025D4 RID: 9684 RVA: 0x000E8DF2 File Offset: 0x000E6FF2
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool TryGetCustomJsonPropertyToken(out JToken token)
			{
				token = this.token;
				return token != null;
			}

			// Token: 0x060025D5 RID: 9685 RVA: 0x000E8E01 File Offset: 0x000E7001
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TmdlObject GetChild()
			{
				Utils.Verify(this.child != null);
				return this.child;
			}

			// Token: 0x060025D6 RID: 9686 RVA: 0x000E8E17 File Offset: 0x000E7017
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ICollection<TmdlObject> GetChildren()
			{
				Utils.Verify(this.children != null);
				return this.children;
			}

			// Token: 0x04000E77 RID: 3703
			private readonly string name;

			// Token: 0x04000E78 RID: 3704
			private readonly MetadataPropertyNature nature;

			// Token: 0x04000E79 RID: 3705
			private readonly TmdlProperty property;

			// Token: 0x04000E7A RID: 3706
			private readonly JToken token;

			// Token: 0x04000E7B RID: 3707
			private readonly TmdlObject child;

			// Token: 0x04000E7C RID: 3708
			private readonly ICollection<TmdlObject> children;
		}

		// Token: 0x02000354 RID: 852
		private struct TranslationProperty
		{
			// Token: 0x060025D7 RID: 9687 RVA: 0x000E8E2D File Offset: 0x000E702D
			private TranslationProperty(string name, MetadataPropertyNature nature, TmdlProperty property, TmdlTranslationElement child, ICollection<TmdlTranslationElement> children)
			{
				this.name = name;
				this.nature = nature;
				this.property = property;
				this.child = child;
				this.children = children;
			}

			// Token: 0x170007A5 RID: 1957
			// (get) Token: 0x060025D8 RID: 9688 RVA: 0x000E8E54 File Offset: 0x000E7054
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170007A6 RID: 1958
			// (get) Token: 0x060025D9 RID: 9689 RVA: 0x000E8E5C File Offset: 0x000E705C
			public MetadataPropertyNature Nature
			{
				get
				{
					return this.nature;
				}
			}

			// Token: 0x170007A7 RID: 1959
			// (get) Token: 0x060025DA RID: 9690 RVA: 0x000E8E64 File Offset: 0x000E7064
			public bool IsProperty
			{
				get
				{
					return this.property != null;
				}
			}

			// Token: 0x170007A8 RID: 1960
			// (get) Token: 0x060025DB RID: 9691 RVA: 0x000E8E6F File Offset: 0x000E706F
			public bool IsChildProperty
			{
				get
				{
					return this.child != null;
				}
			}

			// Token: 0x170007A9 RID: 1961
			// (get) Token: 0x060025DC RID: 9692 RVA: 0x000E8E7A File Offset: 0x000E707A
			public bool IsChildCollectionProperty
			{
				get
				{
					return this.children != null;
				}
			}

			// Token: 0x170007AA RID: 1962
			// (get) Token: 0x060025DD RID: 9693 RVA: 0x000E8E85 File Offset: 0x000E7085
			internal bool IsValid
			{
				get
				{
					return !string.IsNullOrEmpty(this.name);
				}
			}

			// Token: 0x060025DE RID: 9694 RVA: 0x000E8E95 File Offset: 0x000E7095
			public static TmdlObjectReader.TranslationProperty CreateStubProperty(string name, MetadataPropertyNature nature)
			{
				return new TmdlObjectReader.TranslationProperty(name, nature, null, null, null);
			}

			// Token: 0x060025DF RID: 9695 RVA: 0x000E8EA1 File Offset: 0x000E70A1
			public static TmdlObjectReader.TranslationProperty CreateRegularProperty(string name, MetadataPropertyNature nature, TmdlProperty property)
			{
				return new TmdlObjectReader.TranslationProperty(name, nature, property, null, null);
			}

			// Token: 0x060025E0 RID: 9696 RVA: 0x000E8EAD File Offset: 0x000E70AD
			public static TmdlObjectReader.TranslationProperty CreateChildProperty(string name, MetadataPropertyNature nature, TmdlTranslationElement child)
			{
				return new TmdlObjectReader.TranslationProperty(name, nature, null, child, null);
			}

			// Token: 0x060025E1 RID: 9697 RVA: 0x000E8EB9 File Offset: 0x000E70B9
			public static TmdlObjectReader.TranslationProperty CreateChildCollectionProperty(string name, MetadataPropertyNature nature, ICollection<TmdlTranslationElement> children)
			{
				return new TmdlObjectReader.TranslationProperty(name, nature, null, null, children);
			}

			// Token: 0x060025E2 RID: 9698 RVA: 0x000E8EC5 File Offset: 0x000E70C5
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TmdlProperty GetProperty()
			{
				Utils.Verify(this.property != null);
				return this.property;
			}

			// Token: 0x060025E3 RID: 9699 RVA: 0x000E8EDB File Offset: 0x000E70DB
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TmdlTranslationElement GetChild()
			{
				Utils.Verify(this.child != null);
				return this.child;
			}

			// Token: 0x060025E4 RID: 9700 RVA: 0x000E8EF1 File Offset: 0x000E70F1
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ICollection<TmdlTranslationElement> GetChildren()
			{
				Utils.Verify(this.children != null);
				return this.children;
			}

			// Token: 0x04000E7D RID: 3709
			private readonly string name;

			// Token: 0x04000E7E RID: 3710
			private readonly MetadataPropertyNature nature;

			// Token: 0x04000E7F RID: 3711
			private readonly TmdlProperty property;

			// Token: 0x04000E80 RID: 3712
			private readonly TmdlTranslationElement child;

			// Token: 0x04000E81 RID: 3713
			private readonly ICollection<TmdlTranslationElement> children;
		}

		// Token: 0x02000355 RID: 853
		private sealed class StructPropertyReader : IMetadataReader
		{
			// Token: 0x060025E5 RID: 9701 RVA: 0x000E8F07 File Offset: 0x000E7107
			public StructPropertyReader(TmdlStructValue @struct, TmdlSourceLocation location)
			{
				this.@struct = @struct;
				this.location = location;
				this.enumerator = @struct.Properties.GetEnumerator();
				this.state = TmdlObjectReader.StructPropertyReader.ReadState.Start;
			}

			// Token: 0x170007AB RID: 1963
			// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000E8F35 File Offset: 0x000E7135
			public string PropertyName
			{
				get
				{
					this.EnsureStarted();
					TmdlProperty tmdlProperty = this.enumerator.Current;
					if (tmdlProperty == null)
					{
						return null;
					}
					return tmdlProperty.Name;
				}
			}

			// Token: 0x170007AC RID: 1964
			// (get) Token: 0x060025E7 RID: 9703 RVA: 0x000E8F53 File Offset: 0x000E7153
			public bool CanReset
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060025E8 RID: 9704 RVA: 0x000E8F56 File Offset: 0x000E7156
			public ObjectId ReadObjectIdProperty()
			{
				throw new TomInternalException("ReadObjectIdProperty should never be called in TMDL serialization");
			}

			// Token: 0x060025E9 RID: 9705 RVA: 0x000E8F62 File Offset: 0x000E7162
			public ObjectType ReadObjectTypeProperty()
			{
				throw new TomInternalException("ReadObjectTypeProperty should never be called in TMDL serialization");
			}

			// Token: 0x060025EA RID: 9706 RVA: 0x000E8F6E File Offset: 0x000E716E
			public string ReadStringProperty()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				return this.ReadStringPropertyImpl();
			}

			// Token: 0x060025EB RID: 9707 RVA: 0x000E8F82 File Offset: 0x000E7182
			public int ReadInt32Property()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				return this.ReadInt32PropertyImpl();
			}

			// Token: 0x060025EC RID: 9708 RVA: 0x000E8F96 File Offset: 0x000E7196
			public uint ReadUInt32Property()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				return this.ReadUInt32PropertyImpl();
			}

			// Token: 0x060025ED RID: 9709 RVA: 0x000E8FAA File Offset: 0x000E71AA
			public long ReadInt64Property()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				return this.ReadInt64PropertyImpl();
			}

			// Token: 0x060025EE RID: 9710 RVA: 0x000E8FBE File Offset: 0x000E71BE
			public ulong ReadUInt64Property()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				return this.ReadUInt64PropertyImpl();
			}

			// Token: 0x060025EF RID: 9711 RVA: 0x000E8FD2 File Offset: 0x000E71D2
			public bool ReadBooleanProperty()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				return this.ReadBooleanPropertyImpl();
			}

			// Token: 0x060025F0 RID: 9712 RVA: 0x000E8FE6 File Offset: 0x000E71E6
			public DateTime ReadDateTimeProperty()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				return this.ReadDateTimePropertyImpl();
			}

			// Token: 0x060025F1 RID: 9713 RVA: 0x000E8FFA File Offset: 0x000E71FA
			public double ReadDoubleProperty()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				return this.ReadDoublePropertyImpl();
			}

			// Token: 0x060025F2 RID: 9714 RVA: 0x000E900E File Offset: 0x000E720E
			public TEnum ReadEnumProperty<TEnum>()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				return this.ReadEnumPropertyImpl<TEnum>();
			}

			// Token: 0x060025F3 RID: 9715 RVA: 0x000E9022 File Offset: 0x000E7222
			public TPropertyValue ReadProperty<TPropertyValue>()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				return (TPropertyValue)((object)this.ReadPropertyImpl(typeof(TPropertyValue)));
			}

			// Token: 0x060025F4 RID: 9716 RVA: 0x000E9045 File Offset: 0x000E7245
			public ObjectPath ReadCrossLinkProperty()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				return this.ReadCrossLinkPropertyImpl();
			}

			// Token: 0x060025F5 RID: 9717 RVA: 0x000E905C File Offset: 0x000E725C
			public ObjectPath ReadCrossLinkProperty(Func<string, ObjectPath> nameToPathConverter)
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				TmdlModelReferenceValue tmdlModelReferenceValue = this.EnsureCurrentPropertyValue<TmdlModelReferenceValue>(TmdlValueType.ModelReference);
				return this.ReturnValueAndMoveToTheNextProperty<ObjectPath>(nameToPathConverter(tmdlModelReferenceValue.ObjectName.Name));
			}

			// Token: 0x060025F6 RID: 9718 RVA: 0x000E9097 File Offset: 0x000E7297
			public bool TryReadCustomJsonProperty(out JToken token)
			{
				throw new TomInternalException("TryReadCustomJsonProperty is not supported when reading a partition-source!");
			}

			// Token: 0x060025F7 RID: 9719 RVA: 0x000E90A3 File Offset: 0x000E72A3
			public TMetadataObject ReadSingleChildProperty<TMetadataObject>(SerializationActivityContext context) where TMetadataObject : MetadataObject
			{
				throw new TomInternalException("ReadSingleChildProperty is not supported when reading a partition-source!");
			}

			// Token: 0x060025F8 RID: 9720 RVA: 0x000E90AF File Offset: 0x000E72AF
			public IEnumerable<TMetadataObject> ReadChildCollectionProperty<TMetadataObject>(SerializationActivityContext context) where TMetadataObject : MetadataObject
			{
				throw new TomInternalException("ReadChildCollectionProperty is not supported when reading a partition-source!");
			}

			// Token: 0x060025F9 RID: 9721 RVA: 0x000E90BB File Offset: 0x000E72BB
			public IMetadataReader ReadComplexProperty(bool canReset)
			{
				throw new TomInternalException("ReadComplexProperty is not supported when reading a partition-source!");
			}

			// Token: 0x060025FA RID: 9722 RVA: 0x000E90C7 File Offset: 0x000E72C7
			public IEnumerable<IMetadataReader> ReadComplexPropertyCollection(bool canReset)
			{
				throw new TomInternalException("ReadComplexPropertyCollection is not supported when reading a partition-source!");
			}

			// Token: 0x060025FB RID: 9723 RVA: 0x000E90D3 File Offset: 0x000E72D3
			public void Skip()
			{
				this.EnsureStarted();
				this.EnsureCanRead();
				this.state = (this.enumerator.MoveNext() ? TmdlObjectReader.StructPropertyReader.ReadState.Property : TmdlObjectReader.StructPropertyReader.ReadState.End);
			}

			// Token: 0x060025FC RID: 9724 RVA: 0x000E90F8 File Offset: 0x000E72F8
			public void Reset()
			{
				this.enumerator.Reset();
				this.state = TmdlObjectReader.StructPropertyReader.ReadState.Start;
			}

			// Token: 0x060025FD RID: 9725 RVA: 0x000E910C File Offset: 0x000E730C
			public Exception CreateUnexpectedPropertyException(SerializationActivityContext context, UnexpectedPropertyClassification classification)
			{
				if (classification == UnexpectedPropertyClassification.IncompatiblePropertyValue)
				{
					TmdlProperty tmdlProperty = null;
					using (IEnumerator<TmdlProperty> enumerator = this.@struct.Properties.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TmdlProperty tmdlProperty2 = enumerator.Current;
							if (tmdlProperty2 == this.enumerator.Current)
							{
								return TmdlObjectReader.CreateUnexpectedPropertyExceptionImpl(context, classification, tmdlProperty.Name, tmdlProperty.Value.Value, this.location);
							}
							tmdlProperty = tmdlProperty2;
						}
					}
				}
				TmdlProperty tmdlProperty3 = this.enumerator.Current;
				return TmdlObjectReader.CreateUnexpectedPropertyExceptionImpl(context, classification, (tmdlProperty3 != null) ? tmdlProperty3.Name : null, null, this.location);
			}

			// Token: 0x060025FE RID: 9726 RVA: 0x000E91B8 File Offset: 0x000E73B8
			public Exception CreateInvalidDataException(SerializationActivityContext context, string error, Exception e = null)
			{
				return TmdlObjectReader.CreateInvalidDataExceptionImpl(context, error, this.location, e);
			}

			// Token: 0x060025FF RID: 9727 RVA: 0x000E91C8 File Offset: 0x000E73C8
			public Exception CreateInvalidChildException(SerializationActivityContext context, MetadataObject child, string error, Exception e = null)
			{
				throw new TomInternalException("CreateInvalidChildException is not expected when reading a partition-source!");
			}

			// Token: 0x06002600 RID: 9728 RVA: 0x000E91D4 File Offset: 0x000E73D4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void EnsureStarted()
			{
				if (this.state == TmdlObjectReader.StructPropertyReader.ReadState.Start)
				{
					this.state = (this.enumerator.MoveNext() ? TmdlObjectReader.StructPropertyReader.ReadState.Property : TmdlObjectReader.StructPropertyReader.ReadState.End);
				}
			}

			// Token: 0x06002601 RID: 9729 RVA: 0x000E91F5 File Offset: 0x000E73F5
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void EnsureCanRead()
			{
				if (this.state == TmdlObjectReader.StructPropertyReader.ReadState.End)
				{
					throw new InvalidOperationException(TomSR.Exception_TmdlReaderEOF);
				}
			}

			// Token: 0x06002602 RID: 9730 RVA: 0x000E920C File Offset: 0x000E740C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private TTmdlValue EnsureCurrentPropertyValue<TTmdlValue>(TmdlValueType valueType)
			{
				if (this.enumerator.Current.Value != null && this.enumerator.Current.Value.Type == valueType)
				{
					TmdlValue value = this.enumerator.Current.Value;
					if (value is TTmdlValue)
					{
						return value as TTmdlValue;
					}
				}
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.enumerator.Current.Name), this.location);
			}

			// Token: 0x06002603 RID: 9731 RVA: 0x000E928C File Offset: 0x000E748C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private TValue ReturnValueAndMoveToTheNextProperty<TValue>(TValue value)
			{
				try
				{
				}
				finally
				{
					this.state = (this.enumerator.MoveNext() ? TmdlObjectReader.StructPropertyReader.ReadState.Property : TmdlObjectReader.StructPropertyReader.ReadState.End);
				}
				return value;
			}

			// Token: 0x06002604 RID: 9732 RVA: 0x000E92C8 File Offset: 0x000E74C8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private string ReadStringPropertyImpl()
			{
				TmdlStringValue tmdlStringValue = this.EnsureCurrentPropertyValue<TmdlStringValue>(TmdlValueType.String);
				return this.ReturnValueAndMoveToTheNextProperty<string>(tmdlStringValue.IsNull ? null : string.Join("\n", tmdlStringValue.Lines));
			}

			// Token: 0x06002605 RID: 9733 RVA: 0x000E9300 File Offset: 0x000E7500
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private int ReadInt32PropertyImpl()
			{
				TmdlScalarValue<int> tmdlScalarValue = this.EnsureCurrentPropertyValue<TmdlScalarValue<int>>(TmdlValueType.Scalar);
				return this.ReturnValueAndMoveToTheNextProperty<int>(tmdlScalarValue.GetValue().Value);
			}

			// Token: 0x06002606 RID: 9734 RVA: 0x000E932C File Offset: 0x000E752C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private uint ReadUInt32PropertyImpl()
			{
				TmdlScalarValue<uint> tmdlScalarValue = this.EnsureCurrentPropertyValue<TmdlScalarValue<uint>>(TmdlValueType.Scalar);
				return this.ReturnValueAndMoveToTheNextProperty<uint>(tmdlScalarValue.GetValue().Value);
			}

			// Token: 0x06002607 RID: 9735 RVA: 0x000E9358 File Offset: 0x000E7558
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private long ReadInt64PropertyImpl()
			{
				TmdlScalarValue<long> tmdlScalarValue = this.EnsureCurrentPropertyValue<TmdlScalarValue<long>>(TmdlValueType.Scalar);
				return this.ReturnValueAndMoveToTheNextProperty<long>(tmdlScalarValue.GetValue().Value);
			}

			// Token: 0x06002608 RID: 9736 RVA: 0x000E9384 File Offset: 0x000E7584
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private ulong ReadUInt64PropertyImpl()
			{
				TmdlScalarValue<ulong> tmdlScalarValue = this.EnsureCurrentPropertyValue<TmdlScalarValue<ulong>>(TmdlValueType.Scalar);
				return this.ReturnValueAndMoveToTheNextProperty<ulong>(tmdlScalarValue.GetValue().Value);
			}

			// Token: 0x06002609 RID: 9737 RVA: 0x000E93B0 File Offset: 0x000E75B0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private bool ReadBooleanPropertyImpl()
			{
				TmdlScalarValue<bool> tmdlScalarValue = this.EnsureCurrentPropertyValue<TmdlScalarValue<bool>>(TmdlValueType.Scalar);
				return this.ReturnValueAndMoveToTheNextProperty<bool>(TmdlObjectReader.ParseTmdlBoolValue(tmdlScalarValue));
			}

			// Token: 0x0600260A RID: 9738 RVA: 0x000E93D4 File Offset: 0x000E75D4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private DateTime ReadDateTimePropertyImpl()
			{
				TmdlScalarValue<DateTime> tmdlScalarValue = this.EnsureCurrentPropertyValue<TmdlScalarValue<DateTime>>(TmdlValueType.Scalar);
				return this.ReturnValueAndMoveToTheNextProperty<DateTime>(tmdlScalarValue.GetValue().Value);
			}

			// Token: 0x0600260B RID: 9739 RVA: 0x000E9400 File Offset: 0x000E7600
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private double ReadDoublePropertyImpl()
			{
				TmdlScalarValue<double> tmdlScalarValue = this.EnsureCurrentPropertyValue<TmdlScalarValue<double>>(TmdlValueType.Scalar);
				return this.ReturnValueAndMoveToTheNextProperty<double>(tmdlScalarValue.GetValue().Value);
			}

			// Token: 0x0600260C RID: 9740 RVA: 0x000E942C File Offset: 0x000E762C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private TEnum ReadEnumPropertyImpl<TEnum>()
			{
				object value = this.EnsureCurrentPropertyValue<TmdlEnumValue>(TmdlValueType.Scalar).Value;
				if (value is TEnum)
				{
					TEnum tenum = (TEnum)((object)value);
					return this.ReturnValueAndMoveToTheNextProperty<TEnum>(tenum);
				}
				throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.enumerator.Current.Name), this.location);
			}

			// Token: 0x0600260D RID: 9741 RVA: 0x000E9480 File Offset: 0x000E7680
			private object ReadPropertyImpl(Type type)
			{
				if (type == typeof(string))
				{
					return this.ReadStringPropertyImpl();
				}
				if (type == typeof(int))
				{
					return this.ReadInt32PropertyImpl();
				}
				if (type == typeof(uint))
				{
					return this.ReadUInt32PropertyImpl();
				}
				if (type == typeof(long))
				{
					return this.ReadInt64PropertyImpl();
				}
				if (type == typeof(ulong))
				{
					return this.ReadUInt64PropertyImpl();
				}
				if (type == typeof(bool))
				{
					return this.ReadBooleanPropertyImpl();
				}
				if (type == typeof(DateTime))
				{
					return this.ReadDateTimePropertyImpl();
				}
				if (type.IsEnum)
				{
					TmdlEnumValue tmdlEnumValue = this.EnsureCurrentPropertyValue<TmdlEnumValue>(TmdlValueType.Scalar);
					if (!type.IsAssignableFrom(tmdlEnumValue.EnumType))
					{
						throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.enumerator.Current.Name), this.location);
					}
					return this.ReturnValueAndMoveToTheNextProperty<object>(tmdlEnumValue.Value);
				}
				else
				{
					if (type == typeof(IEnumerable<string>))
					{
						throw new TomInternalException("Reading a collection of strings is not supported when reading a partition-source!");
					}
					if (type == typeof(ObjectPath))
					{
						return this.ReadCrossLinkPropertyImpl();
					}
					throw TomInternalException.Create("Invalid property type - {0} is not a valid type for TMDL serialization", new object[] { type.FullName });
				}
			}

			// Token: 0x0600260E RID: 9742 RVA: 0x000E95F8 File Offset: 0x000E77F8
			private ObjectPath ReadCrossLinkPropertyImpl()
			{
				TmdlModelReferenceValue tmdlModelReferenceValue = this.EnsureCurrentPropertyValue<TmdlModelReferenceValue>(TmdlValueType.ModelReference);
				ObjectType objectType;
				if (!TmdlObjectReader.TryIdentifyCrossReferenceTargetType(ObjectType.Partition, this.enumerator.Current.Name, out objectType))
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyUnknownTarget(this.enumerator.Current.Name), this.location);
				}
				return this.ReturnValueAndMoveToTheNextProperty<ObjectPath>(TmdlObjectReader.CreateCrossReferenceObjectPath(tmdlModelReferenceValue.ObjectName, objectType, this.enumerator.Current.Name, this.location));
			}

			// Token: 0x04000E82 RID: 3714
			private readonly TmdlStructValue @struct;

			// Token: 0x04000E83 RID: 3715
			private readonly TmdlSourceLocation location;

			// Token: 0x04000E84 RID: 3716
			private IEnumerator<TmdlProperty> enumerator;

			// Token: 0x04000E85 RID: 3717
			private TmdlObjectReader.StructPropertyReader.ReadState state;

			// Token: 0x02000463 RID: 1123
			private enum ReadState
			{
				// Token: 0x04001499 RID: 5273
				Start,
				// Token: 0x0400149A RID: 5274
				Property,
				// Token: 0x0400149B RID: 5275
				End
			}
		}

		// Token: 0x02000356 RID: 854
		private sealed class TranslationsRootReader : IMetadataReader
		{
			// Token: 0x0600260F RID: 9743 RVA: 0x000E9670 File Offset: 0x000E7870
			public TranslationsRootReader(TmdlTranslationRootValue translations, TmdlSourceLocation location)
			{
				this.model = translations.Root;
				this.location = location;
				Utils.Verify(this.model.ObjectType == ObjectType.Model);
			}

			// Token: 0x170007AD RID: 1965
			// (get) Token: 0x06002610 RID: 9744 RVA: 0x000E969E File Offset: 0x000E789E
			public string PropertyName
			{
				get
				{
					if (!this.modelWasRead)
					{
						return "model";
					}
					return null;
				}
			}

			// Token: 0x170007AE RID: 1966
			// (get) Token: 0x06002611 RID: 9745 RVA: 0x000E96AF File Offset: 0x000E78AF
			public bool CanReset
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06002612 RID: 9746 RVA: 0x000E96B2 File Offset: 0x000E78B2
			public ObjectId ReadObjectIdProperty()
			{
				throw new TomInternalException("ReadObjectIdProperty should never be called in TMDL serialization");
			}

			// Token: 0x06002613 RID: 9747 RVA: 0x000E96BE File Offset: 0x000E78BE
			public ObjectType ReadObjectTypeProperty()
			{
				throw new TomInternalException("ReadObjectTypeProperty should never be called in TMDL serialization");
			}

			// Token: 0x06002614 RID: 9748 RVA: 0x000E96CA File Offset: 0x000E78CA
			public string ReadStringProperty()
			{
				throw new TomInternalException("ReadStringProperty is not supported when reading a translations-root!");
			}

			// Token: 0x06002615 RID: 9749 RVA: 0x000E96D6 File Offset: 0x000E78D6
			public int ReadInt32Property()
			{
				throw new TomInternalException("ReadInt32Property is not supported when reading a translations-root!");
			}

			// Token: 0x06002616 RID: 9750 RVA: 0x000E96E2 File Offset: 0x000E78E2
			public uint ReadUInt32Property()
			{
				throw new TomInternalException("ReadUInt32Property is not supported when reading a translations-root!");
			}

			// Token: 0x06002617 RID: 9751 RVA: 0x000E96EE File Offset: 0x000E78EE
			public long ReadInt64Property()
			{
				throw new TomInternalException("ReadInt64Property is not supported when reading a translations-root!");
			}

			// Token: 0x06002618 RID: 9752 RVA: 0x000E96FA File Offset: 0x000E78FA
			public ulong ReadUInt64Property()
			{
				throw new TomInternalException("ReadUInt64Property is not supported when reading a translations-root!");
			}

			// Token: 0x06002619 RID: 9753 RVA: 0x000E9706 File Offset: 0x000E7906
			public bool ReadBooleanProperty()
			{
				throw new TomInternalException("ReadBooleanProperty is not supported when reading a translations-root!");
			}

			// Token: 0x0600261A RID: 9754 RVA: 0x000E9712 File Offset: 0x000E7912
			public DateTime ReadDateTimeProperty()
			{
				throw new TomInternalException("ReadDateTimeProperty is not supported when reading a translations-root!");
			}

			// Token: 0x0600261B RID: 9755 RVA: 0x000E971E File Offset: 0x000E791E
			public double ReadDoubleProperty()
			{
				throw new TomInternalException("ReadDoubleProperty is not supported when reading a translations-root!");
			}

			// Token: 0x0600261C RID: 9756 RVA: 0x000E972A File Offset: 0x000E792A
			public TEnum ReadEnumProperty<TEnum>()
			{
				throw new TomInternalException("ReadEnumProperty is not supported when reading a translations-root!");
			}

			// Token: 0x0600261D RID: 9757 RVA: 0x000E9736 File Offset: 0x000E7936
			public TPropertyValue ReadProperty<TPropertyValue>()
			{
				throw new TomInternalException("ReadProperty is not supported when reading a translations-root!");
			}

			// Token: 0x0600261E RID: 9758 RVA: 0x000E9742 File Offset: 0x000E7942
			public ObjectPath ReadCrossLinkProperty()
			{
				throw new TomInternalException("ReadCrossLinkProperty is not supported when reading a translations-root!");
			}

			// Token: 0x0600261F RID: 9759 RVA: 0x000E974E File Offset: 0x000E794E
			public ObjectPath ReadCrossLinkProperty(Func<string, ObjectPath> nameToPathConverter)
			{
				throw new TomInternalException("ReadCrossLinkProperty is not supported when reading a translations-root!");
			}

			// Token: 0x06002620 RID: 9760 RVA: 0x000E975A File Offset: 0x000E795A
			public bool TryReadCustomJsonProperty(out JToken token)
			{
				throw new TomInternalException("TryReadCustomJsonProperty is not supported when reading a translations-root!");
			}

			// Token: 0x06002621 RID: 9761 RVA: 0x000E9766 File Offset: 0x000E7966
			public TMetadataObject ReadSingleChildProperty<TMetadataObject>(SerializationActivityContext context) where TMetadataObject : MetadataObject
			{
				throw new TomInternalException("ReadSingleChildProperty is not supported when reading a translations-root!");
			}

			// Token: 0x06002622 RID: 9762 RVA: 0x000E9772 File Offset: 0x000E7972
			public IEnumerable<TMetadataObject> ReadChildCollectionProperty<TMetadataObject>(SerializationActivityContext context) where TMetadataObject : MetadataObject
			{
				throw new TomInternalException("ReadChildCollectionProperty is not supported when reading a translations-root!");
			}

			// Token: 0x06002623 RID: 9763 RVA: 0x000E9780 File Offset: 0x000E7980
			public IMetadataReader ReadComplexProperty(bool canReset)
			{
				this.EnsureOnModel();
				IMetadataReader metadataReader;
				try
				{
					metadataReader = new TmdlObjectReader.TranslationElementReader(this.model);
				}
				finally
				{
					this.modelWasRead = true;
				}
				return metadataReader;
			}

			// Token: 0x06002624 RID: 9764 RVA: 0x000E97BC File Offset: 0x000E79BC
			public IEnumerable<IMetadataReader> ReadComplexPropertyCollection(bool canReset)
			{
				throw new TomInternalException("ReadComplexPropertyCollection is not supported when reading a translations-root!");
			}

			// Token: 0x06002625 RID: 9765 RVA: 0x000E97C8 File Offset: 0x000E79C8
			public void Skip()
			{
				this.EnsureOnModel();
				this.modelWasRead = true;
			}

			// Token: 0x06002626 RID: 9766 RVA: 0x000E97D7 File Offset: 0x000E79D7
			public void Reset()
			{
				this.modelWasRead = false;
			}

			// Token: 0x06002627 RID: 9767 RVA: 0x000E97E0 File Offset: 0x000E79E0
			public Exception CreateUnexpectedPropertyException(SerializationActivityContext context, UnexpectedPropertyClassification classification)
			{
				return TmdlObjectReader.CreateUnexpectedPropertyExceptionImpl(context, classification, this.modelWasRead ? null : "model", null, this.location);
			}

			// Token: 0x06002628 RID: 9768 RVA: 0x000E9800 File Offset: 0x000E7A00
			public Exception CreateInvalidDataException(SerializationActivityContext context, string error, Exception e = null)
			{
				return TmdlObjectReader.CreateInvalidDataExceptionImpl(context, error, this.location, e);
			}

			// Token: 0x06002629 RID: 9769 RVA: 0x000E9810 File Offset: 0x000E7A10
			public Exception CreateInvalidChildException(SerializationActivityContext context, MetadataObject child, string error, Exception e = null)
			{
				throw new TomInternalException("CreateInvalidChildException is not expected when reading a translation-root!");
			}

			// Token: 0x0600262A RID: 9770 RVA: 0x000E981C File Offset: 0x000E7A1C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void EnsureOnModel()
			{
				if (this.modelWasRead)
				{
					throw new InvalidOperationException(TomSR.Exception_TmdlReaderEOF);
				}
			}

			// Token: 0x04000E86 RID: 3718
			private readonly TmdlTranslationElement model;

			// Token: 0x04000E87 RID: 3719
			private readonly TmdlSourceLocation location;

			// Token: 0x04000E88 RID: 3720
			private bool modelWasRead;
		}

		// Token: 0x02000357 RID: 855
		private sealed class TranslationElementReader : IMetadataReader
		{
			// Token: 0x0600262B RID: 9771 RVA: 0x000E9831 File Offset: 0x000E7A31
			public TranslationElementReader(TmdlTranslationElement translationElement)
			{
				this.translationElement = translationElement;
				this.Initialize();
			}

			// Token: 0x170007AF RID: 1967
			// (get) Token: 0x0600262C RID: 9772 RVA: 0x000E9846 File Offset: 0x000E7A46
			public string PropertyName
			{
				get
				{
					return this.GetPropertyName();
				}
			}

			// Token: 0x170007B0 RID: 1968
			// (get) Token: 0x0600262D RID: 9773 RVA: 0x000E984E File Offset: 0x000E7A4E
			public bool CanReset
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600262E RID: 9774 RVA: 0x000E9851 File Offset: 0x000E7A51
			public ObjectId ReadObjectIdProperty()
			{
				throw new TomInternalException("ReadObjectIdProperty should never be called in TMDL serialization");
			}

			// Token: 0x0600262F RID: 9775 RVA: 0x000E985D File Offset: 0x000E7A5D
			public ObjectType ReadObjectTypeProperty()
			{
				throw new TomInternalException("ReadObjectTypeProperty should never be called in TMDL serialization");
			}

			// Token: 0x06002630 RID: 9776 RVA: 0x000E9869 File Offset: 0x000E7A69
			public string ReadStringProperty()
			{
				this.EnsureOnProperty();
				return this.ReadTranslationPropertyImpl();
			}

			// Token: 0x06002631 RID: 9777 RVA: 0x000E9877 File Offset: 0x000E7A77
			public int ReadInt32Property()
			{
				throw new TomInternalException("ReadInt32Property should never be called in translation tree deserialization");
			}

			// Token: 0x06002632 RID: 9778 RVA: 0x000E9883 File Offset: 0x000E7A83
			public uint ReadUInt32Property()
			{
				throw new TomInternalException("ReadUInt32Property should never be called in translation tree deserialization");
			}

			// Token: 0x06002633 RID: 9779 RVA: 0x000E988F File Offset: 0x000E7A8F
			public long ReadInt64Property()
			{
				throw new TomInternalException("ReadInt64Property should never be called in translation tree deserialization");
			}

			// Token: 0x06002634 RID: 9780 RVA: 0x000E989B File Offset: 0x000E7A9B
			public ulong ReadUInt64Property()
			{
				throw new TomInternalException("ReadUInt64Property should never be called in translation tree deserialization");
			}

			// Token: 0x06002635 RID: 9781 RVA: 0x000E98A7 File Offset: 0x000E7AA7
			public bool ReadBooleanProperty()
			{
				throw new TomInternalException("ReadBooleanProperty should never be called in translation tree deserialization");
			}

			// Token: 0x06002636 RID: 9782 RVA: 0x000E98B3 File Offset: 0x000E7AB3
			public DateTime ReadDateTimeProperty()
			{
				throw new TomInternalException("ReadDateTimeProperty should never be called in translation tree deserialization");
			}

			// Token: 0x06002637 RID: 9783 RVA: 0x000E98BF File Offset: 0x000E7ABF
			public double ReadDoubleProperty()
			{
				throw new TomInternalException("ReadDoubleProperty should never be called in translation tree deserialization");
			}

			// Token: 0x06002638 RID: 9784 RVA: 0x000E98CB File Offset: 0x000E7ACB
			public TEnum ReadEnumProperty<TEnum>()
			{
				throw new TomInternalException("ReadEnumProperty should never be called in translation tree deserialization");
			}

			// Token: 0x06002639 RID: 9785 RVA: 0x000E98D7 File Offset: 0x000E7AD7
			public TPropertyValue ReadProperty<TPropertyValue>()
			{
				this.EnsureOnProperty();
				return (TPropertyValue)((object)this.ReadPropertyImpl(typeof(TPropertyValue)));
			}

			// Token: 0x0600263A RID: 9786 RVA: 0x000E98F4 File Offset: 0x000E7AF4
			public ObjectPath ReadCrossLinkProperty()
			{
				throw new TomInternalException("ReadCrossLinkProperty should never be called in translation tree deserialization");
			}

			// Token: 0x0600263B RID: 9787 RVA: 0x000E9900 File Offset: 0x000E7B00
			public ObjectPath ReadCrossLinkProperty(Func<string, ObjectPath> nameToPathConverter)
			{
				throw new TomInternalException("ReadCrossLinkProperty should never be called in translation tree deserialization");
			}

			// Token: 0x0600263C RID: 9788 RVA: 0x000E990C File Offset: 0x000E7B0C
			public bool TryReadCustomJsonProperty(out JToken token)
			{
				throw new TomInternalException("TryReadCustomJsonProperty should never be called in translation tree deserialization");
			}

			// Token: 0x0600263D RID: 9789 RVA: 0x000E9918 File Offset: 0x000E7B18
			public TMetadataObject ReadSingleChildProperty<TMetadataObject>(SerializationActivityContext context) where TMetadataObject : MetadataObject
			{
				throw new TomInternalException("ReadSingleChildProperty should never be called in translation tree deserialization");
			}

			// Token: 0x0600263E RID: 9790 RVA: 0x000E9924 File Offset: 0x000E7B24
			public IEnumerable<TMetadataObject> ReadChildCollectionProperty<TMetadataObject>(SerializationActivityContext context) where TMetadataObject : MetadataObject
			{
				throw new TomInternalException("ReadChildCollectionProperty should never be called in translation tree deserialization");
			}

			// Token: 0x0600263F RID: 9791 RVA: 0x000E9930 File Offset: 0x000E7B30
			public IMetadataReader ReadComplexProperty(bool canReset)
			{
				this.EnsureOnProperty();
				this.EnsureNatureAndValueType((MetadataPropertyNature)67108870, null);
				return this.ReturnValueAndMovePosition<TmdlObjectReader.TranslationElementReader>(new TmdlObjectReader.TranslationElementReader(this.properties[this.propertyIndex].GetChild()));
			}

			// Token: 0x06002640 RID: 9792 RVA: 0x000E997C File Offset: 0x000E7B7C
			public IEnumerable<IMetadataReader> ReadComplexPropertyCollection(bool canReset)
			{
				this.EnsureOnProperty();
				this.EnsureNatureAndValueType((MetadataPropertyNature)67108872, null);
				return this.ReturnValueAndMovePosition<IEnumerable<IMetadataReader>>(TmdlObjectReader.TranslationElementReader.ReadTranslationElementCollectionImpl(this.properties[this.propertyIndex].GetChildren()));
			}

			// Token: 0x06002641 RID: 9793 RVA: 0x000E99C7 File Offset: 0x000E7BC7
			public void Skip()
			{
				this.EnsureOnProperty();
				this.propertyIndex++;
			}

			// Token: 0x06002642 RID: 9794 RVA: 0x000E99DD File Offset: 0x000E7BDD
			public void Reset()
			{
				this.propertyIndex = 0;
			}

			// Token: 0x06002643 RID: 9795 RVA: 0x000E99E6 File Offset: 0x000E7BE6
			public Exception CreateUnexpectedPropertyException(SerializationActivityContext context, UnexpectedPropertyClassification classification)
			{
				return TmdlObjectReader.CreateUnexpectedPropertyExceptionImpl(context, classification, this.GetPropertyName(), null, this.GetLocation());
			}

			// Token: 0x06002644 RID: 9796 RVA: 0x000E99FC File Offset: 0x000E7BFC
			public Exception CreateInvalidDataException(SerializationActivityContext context, string error, Exception e = null)
			{
				return TmdlObjectReader.CreateInvalidDataExceptionImpl(context, error, this.GetLocation(), e);
			}

			// Token: 0x06002645 RID: 9797 RVA: 0x000E9A0C File Offset: 0x000E7C0C
			public Exception CreateInvalidChildException(SerializationActivityContext context, MetadataObject child, string error, Exception e = null)
			{
				throw new TomInternalException("CreateInvalidChildException is not expected when reading a translation-element!");
			}

			// Token: 0x06002646 RID: 9798 RVA: 0x000E9A18 File Offset: 0x000E7C18
			private static IEnumerable<IMetadataReader> ReadTranslationElementCollectionImpl(ICollection<TmdlTranslationElement> elements)
			{
				foreach (TmdlTranslationElement tmdlTranslationElement in elements)
				{
					yield return new TmdlObjectReader.TranslationElementReader(tmdlTranslationElement);
				}
				IEnumerator<TmdlTranslationElement> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06002647 RID: 9799 RVA: 0x000E9A28 File Offset: 0x000E7C28
			private void Initialize()
			{
				this.properties = new List<TmdlObjectReader.TranslationProperty>();
				if (!this.translationElement.Name.IsEmpty)
				{
					this.properties.Add(TmdlObjectReader.TranslationProperty.CreateStubProperty("name", MetadataPropertyNature.NameProperty));
				}
				foreach (TmdlProperty tmdlProperty in this.translationElement.Properties)
				{
					this.AddTranslationProperty(tmdlProperty);
				}
				if (this.translationElement.Children.Count > 0)
				{
					List<KeyValuePair<ObjectType, List<TmdlTranslationElement>>> list = new List<KeyValuePair<ObjectType, List<TmdlTranslationElement>>>();
					using (IEnumerator<TmdlTranslationElement> enumerator2 = this.translationElement.Children.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							TmdlTranslationElement child = enumerator2.Current;
							List<TmdlTranslationElement> list2 = (from kvp in list
								where kvp.Key == child.ObjectType
								select kvp.Value).FirstOrDefault<List<TmdlTranslationElement>>();
							if (list2 == null)
							{
								list2 = new List<TmdlTranslationElement>();
								list.Add(new KeyValuePair<ObjectType, List<TmdlTranslationElement>>(child.ObjectType, list2));
							}
							if (ObjectTreeHelper.IsNamedObject(child.ObjectType) || ObjectTreeHelper.IsKeyedObject(child.ObjectType))
							{
								int num = list2.IndexOf(delegate(TmdlTranslationElement te)
								{
									if (child.Name.IsEmpty)
									{
										return te.Name.IsEmpty;
									}
									return !te.Name.IsEmpty && string.Compare(child.Name.Name, te.Name.Name, StringComparison.InvariantCulture) == 0;
								});
								if (num == -1)
								{
									list2.Add(child);
								}
								else
								{
									list2[num].AddContentOf(child);
								}
							}
							else if (list2.Count > 0)
							{
								list2[0].AddContentOf(child);
							}
							else
							{
								list2.Add(child);
							}
						}
					}
					for (int i = 0; i < list.Count; i++)
					{
						string text3;
						if (list[i].Value.Count > 1)
						{
							string text;
							if (!ObjectTreeHelper.TryGetChildCollectionJsonPropertyName(this.translationElement.ObjectType, list[i].Key, out text))
							{
								if (this.translationElement.ObjectType == ObjectType.CalculationGroup && list[i].Key == ObjectType.CalculationExpression)
								{
									MetadataPropertyNature metadataPropertyNature = (MetadataPropertyNature)67108870;
									using (List<TmdlTranslationElement>.Enumerator enumerator3 = list[i].Value.GetEnumerator())
									{
										while (enumerator3.MoveNext())
										{
											TmdlTranslationElement tmdlTranslationElement = enumerator3.Current;
											if (tmdlTranslationElement.Name.IsEmpty)
											{
												throw new TmdlSerializationException(TomSR.Exception_TmdlObjectNoNameForChild(ObjectType.CalculationExpression.ToString("G"), ObjectType.CalculationGroup.ToString("G")), this.translationElement.SourceLocation);
											}
											string text2 = tmdlTranslationElement.Name.Name;
											if (!(text2 == "multipleOrEmptySelectionExpression"))
											{
												if (!(text2 == "noSelectionExpression"))
												{
													throw new TmdlSerializationException(TomSR.Exception_TmdlObjectInvalidNameForChild(ObjectType.CalculationExpression.ToString("G"), ObjectType.CalculationGroup.ToString("G"), tmdlTranslationElement.Name.Name), this.translationElement.SourceLocation);
												}
												this.properties.Add(TmdlObjectReader.TranslationProperty.CreateChildProperty("noSelectionExpression", metadataPropertyNature, tmdlTranslationElement));
											}
											else
											{
												this.properties.Add(TmdlObjectReader.TranslationProperty.CreateChildProperty("multipleOrEmptySelectionExpression", metadataPropertyNature, tmdlTranslationElement));
											}
										}
										goto IL_0684;
									}
								}
								throw new TmdlSerializationException(TomSR.Exception_TmdlObjectInvalidChild(list[i].Key.ToString("G"), this.translationElement.ObjectType.ToString("G")), this.translationElement.SourceLocation);
							}
							this.properties.Add(TmdlObjectReader.TranslationProperty.CreateChildCollectionProperty(text, (MetadataPropertyNature)67108872, list[i].Value));
						}
						else if (ObjectTreeHelper.TryGetChildJsonPropertyName(this.translationElement.ObjectType, list[i].Key, out text3))
						{
							this.properties.Add(TmdlObjectReader.TranslationProperty.CreateChildProperty(text3, (MetadataPropertyNature)67108870, list[i].Value[0]));
						}
						else if (ObjectTreeHelper.TryGetChildCollectionJsonPropertyName(this.translationElement.ObjectType, list[i].Key, out text3))
						{
							this.properties.Add(TmdlObjectReader.TranslationProperty.CreateChildCollectionProperty(text3, (MetadataPropertyNature)67108872, list[i].Value));
						}
						else
						{
							if (this.translationElement.ObjectType != ObjectType.CalculationGroup || list[i].Key != ObjectType.CalculationExpression)
							{
								throw new TmdlSerializationException(TomSR.Exception_TmdlObjectInvalidChild(list[i].Key.ToString("G"), this.translationElement.ObjectType.ToString("G")), this.translationElement.SourceLocation);
							}
							if (list[i].Value[0].Name.IsEmpty)
							{
								throw new TmdlSerializationException(TomSR.Exception_TmdlObjectNoNameForChild(ObjectType.CalculationExpression.ToString("G"), ObjectType.CalculationGroup.ToString("G")), this.translationElement.SourceLocation);
							}
							string text2 = list[i].Value[0].Name.Name;
							if (!(text2 == "multipleOrEmptySelectionExpression"))
							{
								if (!(text2 == "noSelectionExpression"))
								{
									throw new TmdlSerializationException(TomSR.Exception_TmdlObjectInvalidNameForChild(ObjectType.CalculationExpression.ToString("G"), ObjectType.CalculationGroup.ToString("G"), list[i].Value[0].Name.Name), this.translationElement.SourceLocation);
								}
								this.properties.Add(TmdlObjectReader.TranslationProperty.CreateChildProperty("noSelectionExpression", (MetadataPropertyNature)67108870, list[i].Value[0]));
							}
							else
							{
								this.properties.Add(TmdlObjectReader.TranslationProperty.CreateChildProperty("multipleOrEmptySelectionExpression", (MetadataPropertyNature)67108870, list[i].Value[0]));
							}
						}
						IL_0684:;
					}
				}
			}

			// Token: 0x06002648 RID: 9800 RVA: 0x000EA118 File Offset: 0x000E8318
			private void AddTranslationProperty(TmdlProperty property)
			{
				if (property.Value == null)
				{
					return;
				}
				MetadataPropertyNature metadataPropertyNature;
				if (!TmdlObjectReader.TryIdentifyPropertyNature(property.Value, out metadataPropertyNature))
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyUnknownNature(property.Name), this.GetLocationImpl(property.SourceLocation));
				}
				this.properties.Add(TmdlObjectReader.TranslationProperty.CreateRegularProperty(property.Name, metadataPropertyNature, property));
			}

			// Token: 0x06002649 RID: 9801 RVA: 0x000EA174 File Offset: 0x000E8374
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private string GetPropertyName()
			{
				if (this.propertyIndex < this.properties.Count)
				{
					return this.properties[this.propertyIndex].Name;
				}
				return null;
			}

			// Token: 0x0600264A RID: 9802 RVA: 0x000EA1AF File Offset: 0x000E83AF
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void EnsureOnProperty()
			{
				if (this.propertyIndex == this.properties.Count)
				{
					throw new InvalidOperationException(TomSR.Exception_TmdlReaderEOF);
				}
			}

			// Token: 0x0600264B RID: 9803 RVA: 0x000EA1D0 File Offset: 0x000E83D0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void EnsureNatureAndValueType(MetadataPropertyNature nature, TmdlValueType? valueType)
			{
				if (this.properties[this.propertyIndex].Nature != nature)
				{
					throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchNature(this.GetPropertyName()), this.GetLocation());
				}
				if (valueType != null)
				{
					if (!this.properties[this.propertyIndex].IsProperty)
					{
						throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetPropertyName()), this.GetLocation());
					}
					TmdlValue value = this.properties[this.propertyIndex].GetProperty().Value;
					if (value == null || value.Type != valueType.Value)
					{
						throw new TmdlSerializationException(TomSR.Exception_TmdlPropertyMismatchValueType(this.GetPropertyName()), this.GetLocation());
					}
				}
			}

			// Token: 0x0600264C RID: 9804 RVA: 0x000EA294 File Offset: 0x000E8494
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private TValue ReturnValueAndMovePosition<TValue>(TValue value)
			{
				try
				{
				}
				finally
				{
					this.propertyIndex++;
				}
				return value;
			}

			// Token: 0x0600264D RID: 9805 RVA: 0x000EA2C8 File Offset: 0x000E84C8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private string ReadTranslationPropertyImpl()
			{
				if (this.GetPropertyName() == "name")
				{
					this.EnsureNatureAndValueType(MetadataPropertyNature.NameProperty, null);
					return this.ReturnValueAndMovePosition<string>(this.translationElement.Name.Name);
				}
				this.EnsureNatureAndValueType(MetadataPropertyNature.RegularProperty, new TmdlValueType?(TmdlValueType.String));
				TmdlStringValue tmdlStringValue = (TmdlStringValue)this.properties[this.propertyIndex].GetProperty().Value;
				return this.ReturnValueAndMovePosition<string>(tmdlStringValue.IsNull ? null : string.Join("\n", tmdlStringValue.Lines));
			}

			// Token: 0x0600264E RID: 9806 RVA: 0x000EA363 File Offset: 0x000E8563
			private object ReadPropertyImpl(Type type)
			{
				if (type == typeof(string))
				{
					return this.ReadTranslationPropertyImpl();
				}
				throw TomInternalException.Create("Invalid property type - {0} is not a valid type for TMDL translation tree deserialization", new object[] { type.FullName });
			}

			// Token: 0x0600264F RID: 9807 RVA: 0x000EA398 File Offset: 0x000E8598
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private TmdlSourceLocation GetLocation()
			{
				if (this.propertyIndex < this.properties.Count)
				{
					MetadataPropertyNature nature = this.properties[this.propertyIndex].Nature;
					if (nature != MetadataPropertyNature.RegularProperty)
					{
						if (nature == (MetadataPropertyNature)67108870)
						{
							if (this.properties[this.propertyIndex].IsChildProperty)
							{
								return this.GetLocationImpl(this.properties[this.propertyIndex].GetChild().SourceLocation);
							}
						}
					}
					else if (this.properties[this.propertyIndex].IsProperty)
					{
						return this.GetLocationImpl(this.properties[this.propertyIndex].GetProperty().SourceLocation);
					}
				}
				return this.translationElement.SourceLocation;
			}

			// Token: 0x06002650 RID: 9808 RVA: 0x000EA471 File Offset: 0x000E8671
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private TmdlSourceLocation GetLocationImpl(TmdlSourceLocation location)
			{
				if (!location.IsValid)
				{
					return this.translationElement.SourceLocation;
				}
				return location;
			}

			// Token: 0x04000E89 RID: 3721
			private readonly TmdlTranslationElement translationElement;

			// Token: 0x04000E8A RID: 3722
			private IList<TmdlObjectReader.TranslationProperty> properties;

			// Token: 0x04000E8B RID: 3723
			private int propertyIndex;
		}
	}
}
