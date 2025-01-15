using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x020017AA RID: 6058
	public sealed class ValueCreator
	{
		// Token: 0x0600992A RID: 39210 RVA: 0x001F9F1A File Offset: 0x001F811A
		public static Value CreateValue(RecordValue library, IExpression expression)
		{
			return new ValueCreator(library).CreateValueForExpression(expression);
		}

		// Token: 0x0600992B RID: 39211 RVA: 0x001F9F28 File Offset: 0x001F8128
		private ValueCreator(RecordValue library)
		{
			this.knownIdentifiers = library;
		}

		// Token: 0x170027AE RID: 10158
		// (get) Token: 0x0600992C RID: 39212 RVA: 0x001F9F37 File Offset: 0x001F8137
		private RecordValue KnownIdentifiers
		{
			get
			{
				return this.knownIdentifiers;
			}
		}

		// Token: 0x0600992D RID: 39213 RVA: 0x001F9F3F File Offset: 0x001F813F
		private Value CreateValueForBinary(IBinaryExpression binary)
		{
			if (binary.Operator == BinaryOperator2.MetadataAdd)
			{
				return this.CreateValueForMetadataAdd(binary.Left, binary.Right);
			}
			throw ValueCreator.NotSupported(binary.Operator);
		}

		// Token: 0x0600992E RID: 39214 RVA: 0x001F9F6E File Offset: 0x001F816E
		private Value CreateValueForConstant(IConstantExpression constant)
		{
			return constant.Value;
		}

		// Token: 0x0600992F RID: 39215 RVA: 0x001F9F78 File Offset: 0x001F8178
		private Value CreateValueForExpression(IExpression expression)
		{
			switch (expression.Kind)
			{
			case ExpressionKind.Binary:
				return this.CreateValueForBinary((IBinaryExpression)expression);
			case ExpressionKind.Constant:
				return this.CreateValueForConstant((IConstantExpression)expression);
			case ExpressionKind.FieldAccess:
				return this.CreateValueForFieldAccess((IFieldAccessExpression)expression);
			case ExpressionKind.Function:
				return this.CreateValueForFunction((IFunctionExpression)expression);
			case ExpressionKind.Identifier:
				return this.CreateValueForIdentifier((IIdentifierExpression)expression);
			case ExpressionKind.Invocation:
				return this.CreateValueForInvocation((IInvocationExpression)expression);
			case ExpressionKind.Let:
				return this.CreateValueForLet((ILetExpression)expression);
			case ExpressionKind.List:
				return this.CreateValueForList((IListExpression)expression);
			case ExpressionKind.NotImplemented:
				return this.CreateValueForNotImplemented((INotImplementedExpression)expression);
			case ExpressionKind.Parentheses:
				return this.CreateValueForParentheses((IParenthesesExpression)expression);
			case ExpressionKind.Record:
				return this.CreateValueForRecord((IRecordExpression)expression);
			case ExpressionKind.Throw:
				return this.CreateValueForThrow((IThrowExpression)expression);
			case ExpressionKind.Unary:
				return this.CreateValueForUnary((IUnaryExpression)expression);
			case ExpressionKind.Type:
				return this.CreateValueForType((ITypeExpression)expression);
			case ExpressionKind.RecordType:
				return this.CreateValueForRecordType((IRecordTypeExpression)expression);
			case ExpressionKind.ListType:
				return this.CreateValueForListType((IListTypeExpression)expression);
			case ExpressionKind.TableType:
				return this.CreateValueForTableType((ITableTypeExpression)expression);
			case ExpressionKind.NullableType:
				return this.CreateValueForNullableType((INullableTypeExpression)expression);
			case ExpressionKind.FunctionType:
				return this.CreateValueForFunctionType((IFunctionTypeExpression)expression);
			}
			throw ValueCreator.NotSupported(expression.Kind);
		}

		// Token: 0x06009930 RID: 39216 RVA: 0x001FA10E File Offset: 0x001F830E
		private Value CreateValueForFieldAccess(IFieldAccessExpression fieldAccess)
		{
			return this.CreateValueForExpression(fieldAccess.Expression).AsRecord[fieldAccess.MemberName.Name];
		}

		// Token: 0x06009931 RID: 39217 RVA: 0x001FA134 File Offset: 0x001F8334
		private Value CreateValueForFunction(IFunctionExpression function)
		{
			if (function.Expression.Kind == ExpressionKind.NotImplemented)
			{
				IFunctionTypeExpression functionType = function.FunctionType;
				IList<IParameter> parameters = functionType.Parameters;
				string[] array = new string[parameters.Count];
				TypeValue[] array2 = new TypeValue[parameters.Count];
				for (int i = 0; i < array.Length; i++)
				{
					IParameter parameter = parameters[i];
					array[i] = parameter.Identifier.Name;
					array2[i] = ((parameter.Type != null) ? this.CreateValueForExpression(parameter.Type).AsType : TypeValue.Any);
				}
				TypeValue typeValue = ((functionType.ReturnType != null) ? this.CreateValueForExpression(functionType.ReturnType).AsType : TypeValue.Any);
				return new ValueCreator.NotImplementedFunctionValue(functionType.Min, array, array2, typeValue);
			}
			throw ValueCreator.NotSupported(function);
		}

		// Token: 0x06009932 RID: 39218 RVA: 0x001FA204 File Offset: 0x001F8404
		private Value CreateValueForIdentifier(IIdentifierExpression identifier)
		{
			if (identifier.Name.Name == "_v")
			{
				return this.values;
			}
			Value value;
			if (!this.KnownIdentifiers.TryGetValue(identifier.Name.Name, out value))
			{
				throw ValueCreator.NotSupported(identifier.Name.Name);
			}
			return value;
		}

		// Token: 0x06009933 RID: 39219 RVA: 0x001FA25C File Offset: 0x001F845C
		private Value CreateValueForInvocation(IInvocationExpression invocation)
		{
			FunctionValue asFunction = this.CreateValueForExpression(invocation.Function).AsFunction;
			IList<IExpression> arguments = invocation.Arguments;
			if (asFunction.Equals(TableModule.Table.FromRows))
			{
				IListExpression listExpression = (IListExpression)arguments[0];
				if (arguments.Count == 1 && listExpression.Members.Count == 0)
				{
					return ListValue.Empty.ToTable();
				}
				TableTypeValue tableTypeValue = (TableTypeValue)this.CreateValueForExpression(arguments[1]);
				return this.CreateValueForTable(listExpression, tableTypeValue);
			}
			else
			{
				int count = arguments.Count;
				switch (count)
				{
				case 0:
					return asFunction.Invoke();
				case 1:
					return asFunction.Invoke(this.CreateValueForExpression(arguments[0]));
				case 2:
					return asFunction.Invoke(this.CreateValueForExpression(arguments[0]), this.CreateValueForExpression(arguments[1]));
				case 3:
					return asFunction.Invoke(this.CreateValueForExpression(arguments[0]), this.CreateValueForExpression(arguments[1]), this.CreateValueForExpression(arguments[2]));
				case 4:
					return asFunction.Invoke(this.CreateValueForExpression(arguments[0]), this.CreateValueForExpression(arguments[1]), this.CreateValueForExpression(arguments[2]), this.CreateValueForExpression(arguments[3]));
				case 5:
					return asFunction.Invoke(this.CreateValueForExpression(arguments[0]), this.CreateValueForExpression(arguments[1]), this.CreateValueForExpression(arguments[2]), this.CreateValueForExpression(arguments[3]), this.CreateValueForExpression(arguments[4]));
				default:
				{
					Value[] array = new Value[asFunction.Type.AsFunctionType.Parameters.Count];
					for (int i = 0; i < count; i++)
					{
						array[i] = this.CreateValueForExpression(arguments[i]);
					}
					for (int j = count; j < array.Length; j++)
					{
						array[j] = Value.Null;
					}
					return asFunction.Invoke(array);
				}
				}
			}
		}

		// Token: 0x06009934 RID: 39220 RVA: 0x001FA458 File Offset: 0x001F8658
		private Value CreateValueForLet(ILetExpression let)
		{
			if (let.Variables.Count == 1 && let.Variables[0].Name.Name == "_v" && this.values == null && let.Variables[0].Value.Kind == ExpressionKind.Record)
			{
				IRecordExpression valuesRecord = (IRecordExpression)let.Variables[0].Value;
				string[] array = new string[valuesRecord.Members.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = valuesRecord.Members[i].Name;
				}
				this.values = RecordValue.New(Keys.New(array), (int index) => this.CreateValueForExpression(valuesRecord.Members[index].Value));
				return this.CreateValueForExpression(let.Expression);
			}
			throw ValueCreator.NotSupported(let);
		}

		// Token: 0x06009935 RID: 39221 RVA: 0x001FA56C File Offset: 0x001F876C
		private Value CreateValueForList(IListExpression list)
		{
			return ListValue.New(list.Members.Count, (int index) => this.CreateValueForExpression(list.Members[index]));
		}

		// Token: 0x06009936 RID: 39222 RVA: 0x001FA5B0 File Offset: 0x001F87B0
		private Value CreateValueForListType(IListTypeExpression listType)
		{
			return ListTypeValue.New(ListValue.New(1, (int index) => this.CreateValueForExpression(listType.ItemType)));
		}

		// Token: 0x06009937 RID: 39223 RVA: 0x001FA5E8 File Offset: 0x001F87E8
		private Value CreateValueForFunctionType(IFunctionTypeExpression functionType)
		{
			IList<IParameter> parameters = functionType.Parameters;
			string[] array = new string[parameters.Count];
			Value[] array2 = new TypeValue[parameters.Count];
			Value[] array3 = array2;
			for (int i = 0; i < array.Length; i++)
			{
				IParameter parameter = parameters[i];
				array[i] = parameter.Identifier.Name;
				array3[i] = ((parameter.Type != null) ? this.CreateValueForExpression(parameter.Type).AsType : TypeValue.Any);
			}
			return FunctionTypeValue.New((functionType.ReturnType != null) ? this.CreateValueForExpression(functionType.ReturnType).AsType : TypeValue.Any, RecordValue.New(Keys.New(array), array3), functionType.Min);
		}

		// Token: 0x06009938 RID: 39224 RVA: 0x001FA6A0 File Offset: 0x001F88A0
		private Value CreateValueForMetadataAdd(IExpression value, IExpression metadata)
		{
			Value value2 = this.CreateValueForExpression(value);
			RecordValue asRecord = this.CreateValueForExpression(metadata).AsRecord;
			return value2.NewMeta(asRecord);
		}

		// Token: 0x06009939 RID: 39225 RVA: 0x001FA6C7 File Offset: 0x001F88C7
		private Value CreateValueForNegative(IExpression expression)
		{
			return this.CreateValueForExpression(expression).Negate();
		}

		// Token: 0x0600993A RID: 39226 RVA: 0x001FA6D5 File Offset: 0x001F88D5
		private Value CreateValueForNotImplemented(INotImplementedExpression expression)
		{
			throw ValueException.New(Library.Expression.NotImplemented.Invoke().AsRecord, null);
		}

		// Token: 0x0600993B RID: 39227 RVA: 0x001FA6EC File Offset: 0x001F88EC
		private Value CreateValueForNullableType(INullableTypeExpression nullableType)
		{
			return this.CreateValueForExpression(nullableType.ItemType).AsType.Nullable;
		}

		// Token: 0x0600993C RID: 39228 RVA: 0x001FA704 File Offset: 0x001F8904
		private Value CreateValueForParentheses(IParenthesesExpression parentheses)
		{
			return this.CreateValueForExpression(parentheses.Expression);
		}

		// Token: 0x0600993D RID: 39229 RVA: 0x001FA714 File Offset: 0x001F8914
		private Value CreateValueForRecord(IRecordExpression record)
		{
			string[] array = new string[record.Members.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = record.Members[i].Name.Name;
			}
			return RecordValue.New(Keys.New(array), (int index) => this.CreateValueForExpression(record.Members[index].Value));
		}

		// Token: 0x0600993E RID: 39230 RVA: 0x001FA794 File Offset: 0x001F8994
		private Value CreateValueForRecordType(IRecordTypeExpression recordType)
		{
			IList<IFieldType> fields = recordType.Fields;
			string[] array = new string[fields.Count];
			Value[] array2 = new Value[fields.Count];
			for (int i = 0; i < array.Length; i++)
			{
				IFieldType field = fields[i];
				array[i] = field.Name;
				array2[i] = RecordValue.New(RecordTypeValue.RecordFieldKeys, delegate(int index)
				{
					if (index == 0)
					{
						return this.CreateValueForExpression(field.Type);
					}
					return LogicalValue.New(field.Optional);
				});
			}
			return RecordTypeValue.New(RecordValue.New(Keys.New(array), array2), recordType.Wildcard);
		}

		// Token: 0x0600993F RID: 39231 RVA: 0x001FA830 File Offset: 0x001F8A30
		private Value CreateValueForTable(IListExpression rows, TableTypeValue tableType)
		{
			RecordTypeValue itemType = tableType.ItemType;
			Keys columnKeys = itemType.Fields.Keys;
			return ListValue.New(rows.Members.Count, delegate(int rowIndex)
			{
				ListValue cellValues = this.CreateValueForExpression(rows.Members[rowIndex]).AsList;
				return RecordValue.New(columnKeys, (int columnIndex) => cellValues[columnIndex]);
			}).ToTable(tableType);
		}

		// Token: 0x06009940 RID: 39232 RVA: 0x001FA890 File Offset: 0x001F8A90
		private Value CreateValueForTableType(ITableTypeExpression tableType)
		{
			return TableTypeValue.New(this.CreateValueForExpression(tableType.RowType).AsType.AsRecordType);
		}

		// Token: 0x06009941 RID: 39233 RVA: 0x001FA8AD File Offset: 0x001F8AAD
		private Value CreateValueForThrow(IThrowExpression throwExpr)
		{
			throw ValueException.New(this.CreateValueForExpression(throwExpr.Expression).AsRecord, null);
		}

		// Token: 0x06009942 RID: 39234 RVA: 0x001FA8C6 File Offset: 0x001F8AC6
		private Value CreateValueForType(ITypeExpression type)
		{
			return this.CreateValueForExpression(type.Expression).AsType;
		}

		// Token: 0x06009943 RID: 39235 RVA: 0x001FA8D9 File Offset: 0x001F8AD9
		private Value CreateValueForUnary(IUnaryExpression unary)
		{
			if (unary.Operator == UnaryOperator2.Negative)
			{
				return this.CreateValueForNegative(unary.Expression);
			}
			throw ValueCreator.NotSupported(unary.Operator);
		}

		// Token: 0x06009944 RID: 39236 RVA: 0x001FA901 File Offset: 0x001F8B01
		private static Exception NotSupported(object detail)
		{
			return new FileFormatException(string.Format(CultureInfo.InvariantCulture, "Serialization of '{0}' is not supported", detail));
		}

		// Token: 0x04005133 RID: 20787
		private const string badFormatMessage = "Serialization of '{0}' is not supported";

		// Token: 0x04005134 RID: 20788
		private RecordValue knownIdentifiers;

		// Token: 0x04005135 RID: 20789
		private RecordValue values;

		// Token: 0x020017AB RID: 6059
		private sealed class NotImplementedFunctionValue : NativeFunctionValueN
		{
			// Token: 0x06009945 RID: 39237 RVA: 0x001FA918 File Offset: 0x001F8B18
			public NotImplementedFunctionValue(int min, string[] parameterNames, TypeValue[] parameterTypes, TypeValue returnType)
				: base(min, parameterNames)
			{
				this.parameterTypes = parameterTypes;
				this.returnType = returnType;
			}

			// Token: 0x170027AF RID: 10159
			// (get) Token: 0x06009946 RID: 39238 RVA: 0x001FA931 File Offset: 0x001F8B31
			protected override TypeValue ReturnType
			{
				get
				{
					return this.returnType;
				}
			}

			// Token: 0x06009947 RID: 39239 RVA: 0x001FA939 File Offset: 0x001F8B39
			protected override TypeValue ParamType(int index)
			{
				return this.parameterTypes[index];
			}

			// Token: 0x06009948 RID: 39240 RVA: 0x001FA943 File Offset: 0x001F8B43
			protected override Value InvokeN(Value[] args)
			{
				return Library.Expression.NotImplemented.Invoke();
			}

			// Token: 0x04005136 RID: 20790
			private readonly TypeValue[] parameterTypes;

			// Token: 0x04005137 RID: 20791
			private readonly TypeValue returnType;
		}
	}
}
