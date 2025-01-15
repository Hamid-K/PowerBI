using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Runtime.Typeflow
{
	// Token: 0x020016E7 RID: 5863
	internal static class TypeAlgebra
	{
		// Token: 0x060094EF RID: 38127 RVA: 0x001EBD79 File Offset: 0x001E9F79
		public static TypeValue Concatenate(TypeValue type1, TypeValue type2)
		{
			if (type1.Equals(type2) && type1.TypeKind != ValueKind.Text && type2.TypeKind != ValueKind.Text)
			{
				return type2;
			}
			return TypeAlgebra.Concatenate(type1, type2, new HashSet<TypeValue>());
		}

		// Token: 0x060094F0 RID: 38128 RVA: 0x001EBDA4 File Offset: 0x001E9FA4
		public static TypeValue Intersect(TypeValue type1, TypeValue type2)
		{
			if (type1.Equals(type2))
			{
				return type2;
			}
			return TypeAlgebra.Intersect(type1, type2, new HashSet<TypeValue>());
		}

		// Token: 0x060094F1 RID: 38129 RVA: 0x001EBDBD File Offset: 0x001E9FBD
		public static TypeValue Union(TypeValue type1, TypeValue type2)
		{
			if (type1.Equals(type2))
			{
				return type2;
			}
			return TypeAlgebra.Union(type1, type2, new HashSet<TypeValue>());
		}

		// Token: 0x060094F2 RID: 38130 RVA: 0x001EBDD8 File Offset: 0x001E9FD8
		public static TypeValue Union(TypeValue[] types)
		{
			if (types.Length == 0)
			{
				return TypeValue.None;
			}
			TypeValue typeValue = types[0];
			for (int i = 1; i < types.Length; i++)
			{
				typeValue = TypeAlgebra.Union(types[i], typeValue);
			}
			return typeValue;
		}

		// Token: 0x060094F3 RID: 38131 RVA: 0x001EBE0C File Offset: 0x001EA00C
		public static TypeValue Concatenate(TypeValue type1, TypeValue type2, HashSet<TypeValue> visited)
		{
			if (type1.Equals(type2) && type1.TypeKind != ValueKind.Text && type2.TypeKind != ValueKind.Text)
			{
				return type2;
			}
			if (visited.Contains(type1) || visited.Contains(type2))
			{
				return TypeValue.Any;
			}
			if (!type1.IsNullable && !type2.IsNullable)
			{
				return TypeAlgebra.ConcatenateCore(type1, type2, visited);
			}
			TypeValue typeValue = TypeAlgebra.StripNullable(type1);
			TypeValue typeValue2 = TypeAlgebra.StripNullable(type2);
			TypeValue typeValue3 = TypeAlgebra.ConcatenateCore(typeValue, typeValue2, visited);
			if (typeValue3.Equals(typeValue2) && type2.IsNullable)
			{
				return type2;
			}
			if (typeValue3.Equals(typeValue) && type1.IsNullable)
			{
				return type1;
			}
			return typeValue3.Nullable;
		}

		// Token: 0x060094F4 RID: 38132 RVA: 0x001EBEAC File Offset: 0x001EA0AC
		private static TypeValue ConcatenateCore(TypeValue type1, TypeValue type2, HashSet<TypeValue> visited)
		{
			if (type1.TypeKind == ValueKind.None || type2.TypeKind == ValueKind.None)
			{
				return TypeValue.None;
			}
			switch (type1.TypeKind)
			{
			case ValueKind.Any:
				switch (type2.TypeKind)
				{
				case ValueKind.Any:
				case ValueKind.Null:
				case ValueKind.List:
					return type2;
				case ValueKind.Time:
				case ValueKind.Date:
					return NullableTypeValue.DateTime;
				case ValueKind.Text:
					return NullableTypeValue.Text;
				case ValueKind.Record:
					return TypeAlgebra.Concatenate(TypeValue.Record, type2);
				}
				break;
			case ValueKind.Null:
			{
				ValueKind valueKind = type2.TypeKind;
				if (valueKind - ValueKind.Any <= 3 || valueKind == ValueKind.Text)
				{
					return type1;
				}
				break;
			}
			case ValueKind.Time:
				switch (type2.TypeKind)
				{
				case ValueKind.Any:
					return NullableTypeValue.DateTime;
				case ValueKind.Null:
					return TypeValue.Null;
				case ValueKind.Date:
					return TypeValue.DateTime;
				}
				break;
			case ValueKind.Date:
				switch (type2.TypeKind)
				{
				case ValueKind.Any:
					return NullableTypeValue.DateTime;
				case ValueKind.Null:
					return TypeValue.Null;
				case ValueKind.Time:
					return TypeValue.DateTime;
				}
				break;
			case ValueKind.Text:
			{
				ValueKind valueKind = type2.TypeKind;
				if (valueKind == ValueKind.Any)
				{
					return NullableTypeValue.Text;
				}
				if (valueKind == ValueKind.Null)
				{
					return type2;
				}
				if (valueKind == ValueKind.Text)
				{
					return TypeValue.Text;
				}
				break;
			}
			case ValueKind.List:
			{
				ValueKind valueKind = type2.TypeKind;
				if (valueKind == ValueKind.Any)
				{
					return type1;
				}
				if (valueKind == ValueKind.List)
				{
					visited.Add(type1);
					visited.Add(type2);
					TypeValue typeValue = TypeAlgebra.UnionListTypes(type1.AsListType, type2.AsListType, visited);
					visited.Remove(type1);
					visited.Remove(type2);
					return typeValue;
				}
				break;
			}
			case ValueKind.Record:
			{
				ValueKind valueKind = type2.TypeKind;
				if (valueKind == ValueKind.Any)
				{
					return TypeAlgebra.Concatenate(type1, TypeValue.Record);
				}
				if (valueKind == ValueKind.Record)
				{
					visited.Add(type1);
					visited.Add(type2);
					TypeValue typeValue2 = TypeAlgebra.ConcatenateRecordTypes(type1.AsRecordType, type2.AsRecordType, visited);
					visited.Remove(type1);
					visited.Remove(type2);
					return typeValue2;
				}
				break;
			}
			}
			return TypeValue.None;
		}

		// Token: 0x060094F5 RID: 38133 RVA: 0x001EC0B7 File Offset: 0x001EA2B7
		private static RecordTypeValue ConcatenateRecordTypes(RecordTypeValue recordType1, RecordTypeValue recordType2, HashSet<TypeValue> visited)
		{
			return TypeAlgebra.CombineRecordTypes(new RecordTypeValue[] { recordType1, recordType2 }, visited);
		}

		// Token: 0x060094F6 RID: 38134 RVA: 0x001EC0CD File Offset: 0x001EA2CD
		public static RecordTypeValue CombineRecordTypes(RecordTypeValue[] recordTypes)
		{
			return TypeAlgebra.CombineRecordTypes(recordTypes, new HashSet<TypeValue>());
		}

		// Token: 0x060094F7 RID: 38135 RVA: 0x001EC0DC File Offset: 0x001EA2DC
		private static RecordTypeValue CombineRecordTypes(RecordTypeValue[] recordTypes, HashSet<TypeValue> visited)
		{
			Dictionary<string, RecordValue> dictionary = new Dictionary<string, RecordValue>();
			bool flag = false;
			for (int i = 0; i < recordTypes.Length; i++)
			{
				if (recordTypes[i].Open)
				{
					flag = true;
				}
			}
			for (int i = 0; i < recordTypes.Length; i++)
			{
				RecordValue fields = recordTypes[i].Fields;
				for (int j = 0; j < fields.Count; j++)
				{
					string text = fields.Keys[j];
					RecordValue asRecord = fields[j].AsRecord;
					RecordValue recordValue;
					if (dictionary.TryGetValue(text, out recordValue))
					{
						if (!RecordTypeAlgebra.IsOptional(asRecord))
						{
							dictionary[text] = asRecord;
						}
						else
						{
							bool flag2 = RecordTypeAlgebra.IsOptional(recordValue);
							TypeValue typeValue = TypeAlgebra.Union(RecordTypeAlgebra.FieldType(recordValue), RecordTypeAlgebra.FieldType(asRecord), visited);
							dictionary[text] = RecordTypeAlgebra.NewField(typeValue, flag2);
						}
					}
					else
					{
						TypeValue typeValue2 = (flag ? TypeValue.Any : RecordTypeAlgebra.FieldType(asRecord));
						dictionary.Add(text, RecordTypeAlgebra.NewField(typeValue2, RecordTypeAlgebra.IsOptional(asRecord)));
					}
				}
			}
			return TypeAlgebra.NewRecordType(dictionary, flag);
		}

		// Token: 0x060094F8 RID: 38136 RVA: 0x001EC1EC File Offset: 0x001EA3EC
		private static TypeValue Intersect(TypeValue type1, TypeValue type2, HashSet<TypeValue> visited)
		{
			if (type1 == type2)
			{
				return type2;
			}
			if (type1.IsNullable && type2.TypeKind == ValueKind.Null)
			{
				return type2;
			}
			if (type1.TypeKind == ValueKind.Null && type2.IsNullable)
			{
				return type1;
			}
			if (visited.Contains(type1) || visited.Contains(type2))
			{
				return TypeValue.Any;
			}
			if (!type1.IsNullable && !type2.IsNullable)
			{
				return TypeAlgebra.IntersectCore(type1, type2, visited);
			}
			TypeValue typeValue = TypeAlgebra.StripNullable(type1);
			TypeValue typeValue2 = TypeAlgebra.StripNullable(type2);
			TypeValue typeValue3 = TypeAlgebra.Intersect(typeValue, typeValue2, visited);
			if (typeValue3.Equals(typeValue2) && !TypeAlgebra.HasMeta(type1) && (!type2.IsNullable || type1.TypeKind == ValueKind.Any || type1.IsNullable))
			{
				return type2;
			}
			if (typeValue3.Equals(typeValue) && !TypeAlgebra.HasMeta(type2) && (!type1.IsNullable || type2.TypeKind == ValueKind.Any || type2.IsNullable))
			{
				return type1;
			}
			RecordValue recordValue = TypeAlgebra.UnionMeta(type1, type2);
			if (type1.IsNullable && type2.IsNullable)
			{
				return typeValue3.Nullable.NewMeta(recordValue).AsType;
			}
			return typeValue3.NewMeta(recordValue).AsType;
		}

		// Token: 0x060094F9 RID: 38137 RVA: 0x001EC2FC File Offset: 0x001EA4FC
		private static TypeValue IntersectCore(TypeValue type1, TypeValue type2, HashSet<TypeValue> visited)
		{
			switch (type1.TypeKind)
			{
			case ValueKind.None:
			case ValueKind.Null:
			case ValueKind.Time:
			case ValueKind.Date:
			case ValueKind.DateTime:
			case ValueKind.DateTimeZone:
			case ValueKind.Duration:
			case ValueKind.Number:
			case ValueKind.Logical:
			case ValueKind.Text:
			case ValueKind.Binary:
			case ValueKind.Type:
			case ValueKind.Action:
				if (type2.TypeKind == type1.TypeKind)
				{
					return type2;
				}
				break;
			case ValueKind.Any:
				return type2;
			case ValueKind.List:
				if (type2.TypeKind == ValueKind.List)
				{
					visited.Add(type1);
					visited.Add(type2);
					TypeValue typeValue = TypeAlgebra.IntersectListTypes(type1.AsListType, type2.AsListType, visited);
					visited.Remove(type1);
					visited.Remove(type2);
					return typeValue;
				}
				break;
			case ValueKind.Record:
				if (type2.TypeKind == ValueKind.Record)
				{
					visited.Add(type1);
					visited.Add(type2);
					TypeValue typeValue2 = TypeAlgebra.IntersectRecordTypes(type1.AsRecordType, type2.AsRecordType, visited);
					visited.Remove(type1);
					visited.Remove(type2);
					return typeValue2;
				}
				break;
			case ValueKind.Table:
				if (type2.TypeKind == ValueKind.Table)
				{
					visited.Add(type1);
					visited.Add(type2);
					TypeValue typeValue3 = TypeAlgebra.IntersectTableTypes(type1.AsTableType, type2.AsTableType, visited);
					visited.Remove(type1);
					visited.Remove(type2);
					return typeValue3;
				}
				break;
			case ValueKind.Function:
				if (type2.TypeKind == ValueKind.Function)
				{
					visited.Add(type1);
					visited.Add(type2);
					TypeValue typeValue4 = TypeAlgebra.IntersectFunctionTypes(type1.AsFunctionType, type2.AsFunctionType, visited);
					visited.Remove(type1);
					visited.Remove(type2);
					return typeValue4;
				}
				break;
			default:
				throw new NotImplementedException();
			}
			if (type2.TypeKind == ValueKind.Any)
			{
				return type1;
			}
			return TypeAlgebra.PreferredResult(TypeValue.None, type1, type2);
		}

		// Token: 0x060094FA RID: 38138 RVA: 0x001EC494 File Offset: 0x001EA694
		private static TypeValue IntersectFunctionTypes(FunctionTypeValue functionType1, FunctionTypeValue functionType2, HashSet<TypeValue> visited)
		{
			if (functionType1.Equals(FunctionTypeValue.Any))
			{
				return functionType2;
			}
			if (functionType2.Equals(FunctionTypeValue.Any))
			{
				return functionType1;
			}
			int num = Math.Max(functionType1.Min, functionType2.Min);
			int num2 = Math.Min(functionType1.Max, functionType2.Max);
			if (num > num2)
			{
				return TypeValue.None;
			}
			TypeValue typeValue = TypeAlgebra.Intersect(functionType1.ReturnType, functionType2.ReturnType, visited);
			bool flag = functionType1.Min == num && functionType1.Max == num2 && functionType1.ReturnType.Equals(typeValue);
			bool flag2 = functionType2.Min == num && functionType2.Max == num2 && functionType2.ReturnType.Equals(typeValue);
			TypeValue[] array = new TypeValue[num2];
			for (int i = 0; i < array.Length; i++)
			{
				TypeValue typeValue2 = functionType1.ParameterType(i);
				TypeValue typeValue3 = functionType2.ParameterType(i);
				TypeValue typeValue4 = TypeAlgebra.Intersect(typeValue2, typeValue3, visited);
				array[i] = typeValue4;
				flag &= typeValue2.Equals(typeValue4);
				flag2 &= typeValue3.Equals(typeValue4);
			}
			if (flag2)
			{
				return functionType2;
			}
			if (flag)
			{
				return functionType1;
			}
			return TypeAlgebra.CombinedFunctionType(functionType1, functionType2, typeValue, num, num2, array);
		}

		// Token: 0x060094FB RID: 38139 RVA: 0x001EC5B8 File Offset: 0x001EA7B8
		private static ListTypeValue IntersectListTypes(ListTypeValue listType1, ListTypeValue listType2, HashSet<TypeValue> visited)
		{
			TypeValue typeValue = TypeAlgebra.Intersect(listType1.ItemType, listType2.ItemType, visited);
			if (typeValue.Equals(listType2.ItemType))
			{
				return listType2;
			}
			if (typeValue.Equals(listType1.ItemType))
			{
				return listType1;
			}
			return ListTypeValue.New(typeValue);
		}

		// Token: 0x060094FC RID: 38140 RVA: 0x001EC600 File Offset: 0x001EA800
		private static TableTypeValue IntersectTableTypes(TableTypeValue tableType1, TableTypeValue tableType2, HashSet<TypeValue> visited)
		{
			RecordTypeValue recordTypeValue = TypeAlgebra.IntersectRecordTypes(tableType1.ItemType, tableType2.ItemType, visited);
			if (recordTypeValue.Equals(tableType2.ItemType))
			{
				return tableType2;
			}
			if (recordTypeValue.Equals(tableType1.ItemType))
			{
				return tableType1;
			}
			return TableTypeValue.New(recordTypeValue);
		}

		// Token: 0x060094FD RID: 38141 RVA: 0x001EC648 File Offset: 0x001EA848
		private static RecordTypeValue IntersectRecordTypes(RecordTypeValue recordType1, RecordTypeValue recordType2, HashSet<TypeValue> visited)
		{
			RecordValue fields = recordType1.Fields;
			RecordValue fields2 = recordType2.Fields;
			Dictionary<string, RecordValue> dictionary = TypeAlgebra.DictionaryFromRecordValue(fields);
			bool flag = recordType1.Open && recordType2.Open;
			bool flag2 = flag == recordType1.Open && fields.Count >= fields2.Count;
			bool flag3 = flag == recordType2.Open && fields2.Count >= fields.Count;
			for (int i = 0; i < fields2.Count; i++)
			{
				string text = fields2.Keys[i];
				RecordValue asRecord = fields2[i].AsRecord;
				RecordValue recordValue;
				if (!dictionary.TryGetValue(text, out recordValue))
				{
					if (recordType1.Open || RecordTypeAlgebra.IsOptional(asRecord))
					{
						recordValue = RecordTypeAlgebra.OptionalFieldRecord;
					}
					else
					{
						recordValue = RecordTypeAlgebra.NoFieldRecord;
					}
				}
				RecordValue recordValue2 = TypeAlgebra.IntersectFields(recordValue, asRecord, visited);
				dictionary[text] = recordValue2;
				flag2 &= visited.Count < 5 && recordValue2.Equals(recordValue);
				flag3 &= visited.Count < 5 && recordValue2.Equals(asRecord);
			}
			Dictionary<string, RecordValue> dictionary2 = TypeAlgebra.DictionaryFromRecordValue(fields2);
			for (int j = 0; j < fields.Count; j++)
			{
				string text2 = fields.Keys[j];
				RecordValue asRecord2 = fields[j].AsRecord;
				RecordValue recordValue3;
				if (!dictionary2.TryGetValue(text2, out recordValue3))
				{
					if (recordType2.Open || RecordTypeAlgebra.IsOptional(asRecord2))
					{
						recordValue3 = RecordTypeAlgebra.OptionalFieldRecord;
					}
					else
					{
						recordValue3 = RecordTypeAlgebra.NoFieldRecord;
					}
				}
				RecordValue recordValue4 = TypeAlgebra.IntersectFields(asRecord2, recordValue3, visited);
				dictionary[text2] = recordValue4;
				flag2 &= visited.Count < 5 && recordValue4.Equals(asRecord2);
				flag3 &= visited.Count < 5 && recordValue4.Equals(recordValue3);
			}
			if (flag3)
			{
				return recordType2;
			}
			if (flag2)
			{
				return recordType1;
			}
			return TypeAlgebra.NewRecordType(dictionary, flag);
		}

		// Token: 0x060094FE RID: 38142 RVA: 0x001EC838 File Offset: 0x001EAA38
		private static RecordValue IntersectFields(RecordValue field1, RecordValue field2, HashSet<TypeValue> visited)
		{
			if (visited.Count >= 5)
			{
				RecordValue intersectedField = null;
				return RecordValue.New(RecordTypeValue.RecordFieldKeys, delegate(int index)
				{
					if (intersectedField == null)
					{
						intersectedField = TypeAlgebra.IntersectFieldsImmediate(field1, field2, visited);
					}
					if (index == 0)
					{
						return intersectedField.Item0;
					}
					return intersectedField.Item1;
				});
			}
			return TypeAlgebra.IntersectFieldsImmediate(field1, field2, visited);
		}

		// Token: 0x060094FF RID: 38143 RVA: 0x001EC8A4 File Offset: 0x001EAAA4
		private static RecordValue IntersectFieldsImmediate(RecordValue field1, RecordValue field2, HashSet<TypeValue> visited)
		{
			TypeValue typeValue = RecordTypeAlgebra.FieldType(field1);
			TypeValue typeValue2 = RecordTypeAlgebra.FieldType(field2);
			TypeValue typeValue3 = TypeAlgebra.Intersect(typeValue, typeValue2, visited);
			bool flag = RecordTypeAlgebra.IsOptional(field1);
			bool flag2 = RecordTypeAlgebra.IsOptional(field2);
			bool flag3 = flag && flag2;
			if (flag3 == flag2 && typeValue3.Equals(typeValue2))
			{
				return field2;
			}
			if (flag3 == flag && typeValue3.Equals(typeValue))
			{
				return field1;
			}
			return RecordTypeAlgebra.NewField(typeValue3, flag3);
		}

		// Token: 0x06009500 RID: 38144 RVA: 0x001EC908 File Offset: 0x001EAB08
		public static TypeValue Union(TypeValue type1, TypeValue type2, HashSet<TypeValue> visited)
		{
			if (type1 == type2)
			{
				return type2;
			}
			if (visited.Contains(type1) || visited.Contains(type2))
			{
				return TypeValue.Any;
			}
			if (type1.IsNullable || type2.IsNullable)
			{
				TypeValue typeValue = TypeAlgebra.StripNullable(type1);
				TypeValue typeValue2 = TypeAlgebra.StripNullable(type2);
				TypeValue typeValue3 = TypeAlgebra.Union(typeValue, typeValue2, visited);
				if (typeValue3.Equals(typeValue2) && type2.IsNullable)
				{
					return type2;
				}
				if (typeValue3.Equals(typeValue) && type1.IsNullable)
				{
					return type1;
				}
				return typeValue3.Nullable;
			}
			else
			{
				Value domain = TypeServices.GetDomain(type1);
				Value domain2 = TypeServices.GetDomain(type2);
				TypeValue typeValue4 = TypeServices.ClearDomain(TypeAlgebra.UnionCore(type1, type2, visited));
				if (!domain.IsNull && !domain2.IsNull)
				{
					ListValue asList = Library.List.Union.Invoke(ListValue.New(new Value[] { domain, domain2 })).AsList;
					return TypeServices.SetDomain(typeValue4, asList);
				}
				return typeValue4;
			}
		}

		// Token: 0x06009501 RID: 38145 RVA: 0x001EC9E8 File Offset: 0x001EABE8
		private static TypeValue UnionCore(TypeValue type1, TypeValue type2, HashSet<TypeValue> visited)
		{
			switch (type1.TypeKind)
			{
			case ValueKind.None:
				return type2;
			case ValueKind.Any:
			case ValueKind.Time:
			case ValueKind.Date:
			case ValueKind.DateTime:
			case ValueKind.DateTimeZone:
			case ValueKind.Duration:
			case ValueKind.Logical:
			case ValueKind.Binary:
			case ValueKind.Type:
			case ValueKind.Action:
				if (type2.TypeKind == type1.TypeKind)
				{
					return type2;
				}
				break;
			case ValueKind.Null:
				if (type2.TypeKind == ValueKind.Null || type2.TypeKind == ValueKind.Any)
				{
					return type2;
				}
				if (type2.TypeKind != ValueKind.None)
				{
					return type2.Nullable;
				}
				break;
			case ValueKind.Number:
				if (type2.TypeKind == type1.TypeKind)
				{
					return TypeValue.Number;
				}
				break;
			case ValueKind.Text:
				if (type2.TypeKind == type1.TypeKind)
				{
					return TypeValue.Text;
				}
				break;
			case ValueKind.List:
				if (type2.TypeKind == ValueKind.List)
				{
					visited.Add(type1);
					visited.Add(type2);
					TypeValue typeValue = TypeAlgebra.UnionListTypes(type1.AsListType, type2.AsListType, visited);
					visited.Remove(type1);
					visited.Remove(type2);
					return typeValue;
				}
				break;
			case ValueKind.Record:
				if (type2.TypeKind == ValueKind.Record)
				{
					visited.Add(type1);
					visited.Add(type2);
					TypeValue typeValue2 = TypeAlgebra.UnionRecordTypes(type1.AsRecordType, type2.AsRecordType, visited);
					visited.Remove(type1);
					visited.Remove(type2);
					return typeValue2;
				}
				break;
			case ValueKind.Table:
				if (type2.TypeKind == ValueKind.Table)
				{
					visited.Add(type1);
					visited.Add(type2);
					TypeValue typeValue3 = TypeAlgebra.UnionTableTypes(type1.AsTableType, type2.AsTableType, visited);
					visited.Remove(type1);
					visited.Remove(type2);
					return typeValue3;
				}
				break;
			case ValueKind.Function:
				if (type2.TypeKind == ValueKind.Function)
				{
					visited.Add(type1);
					visited.Add(type2);
					TypeValue typeValue4 = TypeAlgebra.UnionFunctionTypes(type1.AsFunctionType, type2.AsFunctionType, visited);
					visited.Remove(type1);
					visited.Remove(type2);
					return typeValue4;
				}
				break;
			default:
				throw new NotImplementedException();
			}
			ValueKind typeKind = type2.TypeKind;
			if (typeKind == ValueKind.None)
			{
				return type1;
			}
			if (typeKind != ValueKind.Null)
			{
				return TypeAlgebra.PreferredResult(TypeValue.Any, type1, type2);
			}
			return type1.Nullable;
		}

		// Token: 0x06009502 RID: 38146 RVA: 0x001ECBDC File Offset: 0x001EADDC
		private static FunctionTypeValue UnionFunctionTypes(FunctionTypeValue functionType1, FunctionTypeValue functionType2, HashSet<TypeValue> visited)
		{
			if (functionType1.Equals(FunctionTypeValue.Any))
			{
				return functionType2;
			}
			if (functionType2.Equals(FunctionTypeValue.Any))
			{
				return functionType1;
			}
			int num = Math.Min(functionType1.Min, functionType2.Min);
			int num2 = Math.Max(functionType1.Max, functionType2.Max);
			TypeValue typeValue = TypeAlgebra.Union(functionType1.ReturnType, functionType2.ReturnType, visited);
			bool flag = functionType1.Min == num && functionType1.Max == num2 && functionType1.ReturnType.Equals(typeValue);
			bool flag2 = functionType2.Min == num && functionType2.Max == num2 && functionType2.ReturnType.Equals(typeValue);
			TypeValue[] array = new TypeValue[Math.Min(functionType1.Max, functionType2.Max)];
			for (int i = 0; i < array.Length; i++)
			{
				TypeValue typeValue2 = functionType1.ParameterType(i);
				TypeValue typeValue3 = functionType2.ParameterType(i);
				TypeValue typeValue4 = TypeAlgebra.Union(typeValue2, typeValue3, visited);
				array[i] = typeValue4;
				flag &= typeValue2.Equals(typeValue4);
				flag2 &= typeValue3.Equals(typeValue4);
			}
			if (flag2)
			{
				return functionType2;
			}
			if (flag)
			{
				return functionType1;
			}
			return TypeAlgebra.CombinedFunctionType(functionType1, functionType2, typeValue, num, num2, array);
		}

		// Token: 0x06009503 RID: 38147 RVA: 0x001ECD08 File Offset: 0x001EAF08
		private static ListTypeValue UnionListTypes(ListTypeValue listType1, ListTypeValue listType2, HashSet<TypeValue> visited)
		{
			TypeValue typeValue = TypeAlgebra.Union(listType1.ItemType, listType2.ItemType, visited);
			if (typeValue.Equals(listType1.ItemType))
			{
				return listType1;
			}
			if (typeValue.Equals(listType2.ItemType))
			{
				return listType2;
			}
			return ListTypeValue.New(typeValue);
		}

		// Token: 0x06009504 RID: 38148 RVA: 0x001ECD50 File Offset: 0x001EAF50
		private static TableTypeValue UnionTableTypes(TableTypeValue tableType1, TableTypeValue tableType2, HashSet<TypeValue> visited)
		{
			TypeValue typeValue = TypeAlgebra.Union(tableType1.ItemType, tableType2.ItemType, visited);
			if (typeValue.Equals(tableType1.ItemType))
			{
				return tableType1;
			}
			if (typeValue.Equals(tableType2.ItemType))
			{
				return tableType2;
			}
			if (typeValue.TypeKind == ValueKind.Record && RecordTypeAlgebra.IsClosedRecordWithRequiredFieldsOnly(typeValue.AsRecordType))
			{
				return TableTypeValue.New(typeValue.AsRecordType);
			}
			return TypeValue.Table;
		}

		// Token: 0x06009505 RID: 38149 RVA: 0x001ECDB8 File Offset: 0x001EAFB8
		private static RecordTypeValue UnionRecordTypes(RecordTypeValue recordType1, RecordTypeValue recordType2, HashSet<TypeValue> visited)
		{
			RecordValue fields = recordType1.Fields;
			RecordValue fields2 = recordType2.Fields;
			Dictionary<string, RecordValue> dictionary = TypeAlgebra.DictionaryFromRecordValue(fields);
			bool flag = recordType1.Open || recordType2.Open;
			bool flag2 = flag == recordType1.Open && fields.Count <= fields2.Count;
			bool flag3 = flag == recordType2.Open && fields2.Count <= fields.Count;
			for (int i = 0; i < fields2.Count; i++)
			{
				string text = fields2.Keys[i];
				RecordValue asRecord = fields2[i].AsRecord;
				RecordValue recordValue;
				if (!dictionary.TryGetValue(text, out recordValue))
				{
					recordValue = (recordType1.Open ? RecordTypeAlgebra.OptionalFieldRecord : RecordTypeAlgebra.OptionalNoFieldRecord);
				}
				RecordValue recordValue2 = TypeAlgebra.UnionFields(recordValue, asRecord, visited);
				dictionary[text] = recordValue2;
				flag2 &= visited.Count < 5 && recordValue2.Equals(recordValue);
				flag3 &= visited.Count < 5 && recordValue2.Equals(asRecord);
			}
			Dictionary<string, RecordValue> dictionary2 = TypeAlgebra.DictionaryFromRecordValue(fields2);
			for (int j = 0; j < fields.Count; j++)
			{
				string text2 = fields.Keys[j];
				RecordValue asRecord2 = fields[j].AsRecord;
				RecordValue recordValue3;
				if (!dictionary2.TryGetValue(text2, out recordValue3))
				{
					recordValue3 = (recordType2.Open ? RecordTypeAlgebra.OptionalFieldRecord : RecordTypeAlgebra.OptionalNoFieldRecord);
				}
				RecordValue recordValue4 = TypeAlgebra.UnionFields(asRecord2, recordValue3, visited);
				dictionary[text2] = recordValue4;
				flag2 &= visited.Count < 5 && recordValue4.Equals(asRecord2);
				flag3 &= visited.Count < 5 && recordValue4.Equals(recordValue3);
			}
			if (flag3)
			{
				return recordType2;
			}
			if (flag2)
			{
				return recordType1;
			}
			return TypeAlgebra.NewRecordType(dictionary, flag);
		}

		// Token: 0x06009506 RID: 38150 RVA: 0x001ECF90 File Offset: 0x001EB190
		private static RecordValue UnionFields(RecordValue field1, RecordValue field2, HashSet<TypeValue> visited)
		{
			if (visited.Count >= 5)
			{
				RecordValue unionedField = null;
				return RecordValue.New(RecordTypeValue.RecordFieldKeys, delegate(int index)
				{
					if (unionedField == null)
					{
						unionedField = TypeAlgebra.UnionFieldsImmediate(field1, field2, visited);
					}
					if (index == 0)
					{
						return unionedField.Item0;
					}
					return unionedField.Item1;
				});
			}
			return TypeAlgebra.UnionFieldsImmediate(field1, field2, visited);
		}

		// Token: 0x06009507 RID: 38151 RVA: 0x001ECFFC File Offset: 0x001EB1FC
		private static RecordValue UnionFieldsImmediate(RecordValue field1, RecordValue field2, HashSet<TypeValue> visited)
		{
			TypeValue typeValue = RecordTypeAlgebra.FieldType(field1);
			TypeValue typeValue2 = RecordTypeAlgebra.FieldType(field2);
			TypeValue typeValue3 = TypeAlgebra.Union(typeValue, typeValue2, visited);
			bool flag = RecordTypeAlgebra.IsOptional(field1);
			bool flag2 = RecordTypeAlgebra.IsOptional(field2);
			bool flag3 = flag || flag2;
			if (flag3 == flag2 && typeValue3.Equals(typeValue2))
			{
				return field2;
			}
			if (flag3 == flag && typeValue3.Equals(typeValue))
			{
				return field1;
			}
			return RecordTypeAlgebra.NewField(typeValue3, flag3);
		}

		// Token: 0x06009508 RID: 38152 RVA: 0x001ED060 File Offset: 0x001EB260
		public static Dictionary<string, RecordValue> DictionaryFromRecordValue(RecordValue RecordValue)
		{
			Dictionary<string, RecordValue> dictionary = new Dictionary<string, RecordValue>(RecordValue.Count);
			for (int i = 0; i < RecordValue.Count; i++)
			{
				dictionary.Add(RecordValue.Keys[i], RecordValue[i].AsRecord);
			}
			return dictionary;
		}

		// Token: 0x06009509 RID: 38153 RVA: 0x001ED0AC File Offset: 0x001EB2AC
		public static RecordTypeValue NewRecordType(Dictionary<string, RecordValue> fields, bool open)
		{
			string[] array = new string[fields.Count];
			fields.Keys.CopyTo(array, 0);
			RecordValue[] array2 = new RecordValue[array.Length];
			fields.Values.CopyTo(array2, 0);
			Keys keys = Keys.New(array);
			Value[] array3 = array2;
			return RecordTypeValue.New(RecordValue.New(keys, array3), open);
		}

		// Token: 0x0600950A RID: 38154 RVA: 0x001ED0FC File Offset: 0x001EB2FC
		public static TableTypeValue NewTableType(TypeValue itemType)
		{
			return TableTypeValue.New(itemType.AsRecordType);
		}

		// Token: 0x0600950B RID: 38155 RVA: 0x001ED109 File Offset: 0x001EB309
		public static ListTypeValue NewTableOrListType(TypeValue itemType)
		{
			if (RecordTypeAlgebra.IsClosedRecordWithRequiredFieldsOnly(itemType))
			{
				return TableTypeValue.New(itemType.AsRecordType).AsType.AsListType;
			}
			return ListTypeValue.New(itemType).AsListType;
		}

		// Token: 0x0600950C RID: 38156 RVA: 0x001ED134 File Offset: 0x001EB334
		private static bool HasMeta(Value value)
		{
			return value.MetaValue.Keys.Length > 0;
		}

		// Token: 0x0600950D RID: 38157 RVA: 0x001ED149 File Offset: 0x001EB349
		private static RecordValue UnionMeta(Value value1, Value value2)
		{
			return TypeAlgebra.UnionMetaValues(value1.MetaValue, value2.MetaValue);
		}

		// Token: 0x0600950E RID: 38158 RVA: 0x001ED15C File Offset: 0x001EB35C
		private static RecordValue UnionMetaValues(RecordValue meta1, RecordValue meta2)
		{
			Keys keys = meta1.Keys;
			Keys keys2 = meta2.Keys;
			IList<NamedValue> list = new List<NamedValue>(keys.Length + keys2.Length);
			for (int i = 0; i < keys.Length; i++)
			{
				string text = keys[i];
				if (!keys2.Contains(text) || meta1[text].Equals(meta2[text]))
				{
					list.Add(new NamedValue(text, meta1[text]));
				}
			}
			for (int j = 0; j < keys2.Length; j++)
			{
				string text2 = keys2[j];
				if (!keys.Contains(text2))
				{
					list.Add(new NamedValue(text2, meta2[text2]));
				}
			}
			return RecordValue.New(list.ToArray<NamedValue>());
		}

		// Token: 0x0600950F RID: 38159 RVA: 0x001E4DFA File Offset: 0x001E2FFA
		public static TypeValue StripNullable(TypeValue type)
		{
			return type.NonNullable;
		}

		// Token: 0x06009510 RID: 38160 RVA: 0x001ED223 File Offset: 0x001EB423
		public static TypeValue PreferredResult(TypeValue result, TypeValue type)
		{
			if (type.TypeKind == result.TypeKind)
			{
				return type;
			}
			return result;
		}

		// Token: 0x06009511 RID: 38161 RVA: 0x001ED236 File Offset: 0x001EB436
		public static TypeValue PreferredResult(TypeValue result, TypeValue type1, TypeValue type2)
		{
			if (type2.TypeKind == result.TypeKind)
			{
				return type2;
			}
			if (type1.TypeKind == result.TypeKind)
			{
				return type1;
			}
			return result;
		}

		// Token: 0x06009512 RID: 38162 RVA: 0x001ED25C File Offset: 0x001EB45C
		private static string UniqueParameterName(Keys paramKeys1, Keys paramKeys2, ref int seed, int limit)
		{
			string text;
			if (seed < limit && (text = paramKeys1[seed]) == paramKeys2[seed])
			{
				return text;
			}
			do
			{
				string text2 = "p";
				int num = seed;
				seed = num + 1;
				text = text2 + num.ToString();
			}
			while (paramKeys1.Contains(text) || paramKeys2.Contains(text));
			return text;
		}

		// Token: 0x06009513 RID: 38163 RVA: 0x001ED2B8 File Offset: 0x001EB4B8
		private static FunctionTypeValue CombinedFunctionType(FunctionTypeValue functionType1, FunctionTypeValue functionType2, TypeValue combinedReturnType, int combinedMin, int combinedMax, TypeValue[] combinedParameterTypes)
		{
			NamedValue[] array = new NamedValue[combinedMax];
			int num = 0;
			for (int i = 0; i < combinedMax; i++)
			{
				if (num < i)
				{
					num = i;
				}
				array[i] = new NamedValue(TypeAlgebra.UniqueParameterName(functionType1.Parameters.Keys, functionType2.Parameters.Keys, ref num, combinedParameterTypes.Length), (i < combinedParameterTypes.Length) ? combinedParameterTypes[i] : TypeValue.Any);
			}
			return FunctionTypeValue.New(combinedReturnType, RecordValue.New(array), combinedMin);
		}

		// Token: 0x04004F3C RID: 20284
		private const int delayedDepth = 5;
	}
}
