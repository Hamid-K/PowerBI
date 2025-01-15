using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Typeflow
{
	// Token: 0x020017B3 RID: 6067
	internal class LogicalAstToCachedTypeflowResultCursor : ITypeflowEnvironment
	{
		// Token: 0x06009957 RID: 39255 RVA: 0x001FAA77 File Offset: 0x001F8C77
		public static LogicalAstToCachedTypeflowResultCursor Create(IExpression expression)
		{
			return new LogicalAstToCachedTypeflowResultCursor(expression);
		}

		// Token: 0x06009958 RID: 39256 RVA: 0x001FAA7F File Offset: 0x001F8C7F
		private LogicalAstToCachedTypeflowResultCursor(IExpression expression)
		{
			this.VisitExpression(expression);
		}

		// Token: 0x06009959 RID: 39257 RVA: 0x001FAA9C File Offset: 0x001F8C9C
		public TypeValue[] GetParameterResults(IInvocationExpression invocation)
		{
			TypeValue[] array = new TypeValue[invocation.Arguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.types[invocation.Arguments[i]];
			}
			return array;
		}

		// Token: 0x0600995A RID: 39258 RVA: 0x001FAAE4 File Offset: 0x001F8CE4
		public TypeValue GetResult(IExpression syntaxNode)
		{
			TypeValue typeValue;
			if (!this.TryGetResult(syntaxNode, out typeValue))
			{
				throw new InvalidOperationException();
			}
			return typeValue;
		}

		// Token: 0x0600995B RID: 39259 RVA: 0x001FAB03 File Offset: 0x001F8D03
		public bool TryGetResult(IExpression syntaxNode, out TypeValue result)
		{
			return this.types.TryGetValue(syntaxNode, out result);
		}

		// Token: 0x0600995C RID: 39260 RVA: 0x0000336E File Offset: 0x0000156E
		public void Push(IInvocationExpression invocation)
		{
		}

		// Token: 0x0600995D RID: 39261 RVA: 0x0000336E File Offset: 0x0000156E
		public void Push(IFunctionExpression function, IList<IExpression> args)
		{
		}

		// Token: 0x0600995E RID: 39262 RVA: 0x0000336E File Offset: 0x0000156E
		public void Push(IFunctionExpression function, params TypeValue[] argResults)
		{
		}

		// Token: 0x0600995F RID: 39263 RVA: 0x0000336E File Offset: 0x0000156E
		public void Push(ILetExpression let, Identifier variable)
		{
		}

		// Token: 0x06009960 RID: 39264 RVA: 0x0000336E File Offset: 0x0000156E
		public void Push(ILetExpression let)
		{
		}

		// Token: 0x06009961 RID: 39265 RVA: 0x0000336E File Offset: 0x0000156E
		public void Push(IRecordExpression record, Identifier field)
		{
		}

		// Token: 0x06009962 RID: 39266 RVA: 0x0000336E File Offset: 0x0000156E
		public void Push(IListExpression list, int index)
		{
		}

		// Token: 0x06009963 RID: 39267 RVA: 0x0000336E File Offset: 0x0000156E
		public void Pop()
		{
		}

		// Token: 0x06009964 RID: 39268 RVA: 0x001FAB14 File Offset: 0x001F8D14
		private TypeValue VisitExpression(IExpression expression)
		{
			TypeValue typeValue = this._VisitExpression(expression);
			this.types[expression] = typeValue;
			return typeValue;
		}

		// Token: 0x06009965 RID: 39269 RVA: 0x001FAB38 File Offset: 0x001F8D38
		private TypeValue _VisitExpression(IExpression expression)
		{
			switch (expression.Kind)
			{
			case ExpressionKind.Binary:
				return this.VisitBinary((IBinaryExpression)expression);
			case ExpressionKind.Constant:
				return this.VisitConstant((IConstantExpression)expression);
			case ExpressionKind.FieldAccess:
				return this.VisitFieldAccess((IFieldAccessExpression)expression);
			case ExpressionKind.Function:
				return this.VisitFunction((IFunctionExpression)expression);
			case ExpressionKind.Identifier:
				return this.argument;
			case ExpressionKind.If:
				return this.VisitIf((IIfExpression)expression);
			case ExpressionKind.Invocation:
				return this.VisitInvocation((IInvocationExpression)expression);
			case ExpressionKind.List:
				return this.VisitList((IListExpression)expression);
			case ExpressionKind.MultiFieldRecordProjection:
				return this.VisitProjection((IMultiFieldRecordProjectionExpression)expression);
			case ExpressionKind.Record:
				return this.VisitRecord((IRecordExpression)expression);
			case ExpressionKind.Unary:
				return this.VisitUnary((IUnaryExpression)expression);
			}
			return TypeValue.None;
		}

		// Token: 0x06009966 RID: 39270 RVA: 0x001FAC38 File Offset: 0x001F8E38
		private TypeValue VisitInvocation(IInvocationExpression invocation)
		{
			TypeValue typeValue = this.VisitExpression(invocation.Function);
			IConstantExpression constantExpression = invocation.Function as IConstantExpression;
			if (constantExpression != null && constantExpression.Value.IsFunction)
			{
				return this.VisitInvocation(invocation, typeValue, constantExpression.Value.AsFunction);
			}
			return TypeValue.Any;
		}

		// Token: 0x06009967 RID: 39271 RVA: 0x001FAC88 File Offset: 0x001F8E88
		private TypeValue VisitInvocation(IInvocationExpression invocation, TypeValue functionType, FunctionValue function)
		{
			Value value;
			TableTypeAlgebra.JoinKind joinKind;
			if (function.Equals(TableModule.Table.Join) && invocation.Arguments.Count >= 5 && invocation.Arguments.Count <= 7 && invocation.Arguments[4].TryGetConstant(out value) && Library.JoinKind.Type.TryGetValue(value, out joinKind))
			{
				TypeValue typeValue = this.VisitExpression(invocation.Arguments[0]);
				this.VisitExpression(invocation.Arguments[1]);
				TypeValue typeValue2 = this.VisitExpression(invocation.Arguments[2]);
				this.VisitExpression(invocation.Arguments[3]);
				if (invocation.Arguments.Count > 6)
				{
					this.VisitExpression(invocation.Arguments[6]);
				}
				TypeValue typeValue3 = typeValue.AsTableType.ItemType;
				TypeValue typeValue4 = typeValue2.AsTableType.ItemType;
				switch (joinKind)
				{
				case TableTypeAlgebra.JoinKind.LeftOuter:
				case TableTypeAlgebra.JoinKind.LeftAnti:
				case TableTypeAlgebra.JoinKind.LeftSemi:
					typeValue4 = RecordTypeAlgebra.EnsureNullableFields(typeValue4);
					break;
				case TableTypeAlgebra.JoinKind.FullOuter:
					typeValue3 = RecordTypeAlgebra.EnsureNullableFields(typeValue3);
					typeValue4 = RecordTypeAlgebra.EnsureNullableFields(typeValue4);
					break;
				case TableTypeAlgebra.JoinKind.RightOuter:
				case TableTypeAlgebra.JoinKind.RightAnti:
				case TableTypeAlgebra.JoinKind.RightSemi:
					typeValue3 = RecordTypeAlgebra.EnsureNullableFields(typeValue3);
					break;
				}
				return TableTypeValue.New(TypeAlgebra.Concatenate(typeValue3, typeValue4).AsRecordType);
			}
			if (function.Equals(TableModule.Table.Group) && invocation.Arguments.Count == 3 && invocation.Arguments[1] is IFunctionExpression && invocation.Arguments[2] is IFunctionExpression)
			{
				TypeValue typeValue5 = this.VisitExpression(invocation.Arguments[0]);
				using (this.NewArgumentScope(typeValue5.AsTableType.ItemType))
				{
					this.VisitExpression(invocation.Arguments[1]);
				}
				using (this.NewArgumentScope(typeValue5.AsTableType))
				{
					return TableTypeValue.New(this.VisitExpression(invocation.Arguments[2]).AsFunctionType.ReturnType.AsRecordType);
				}
			}
			if (function.Equals(Library.ListRuntime.Transform) && invocation.Arguments.Count == 2 && invocation.Arguments[1] is IFunctionExpression)
			{
				TypeValue typeValue6 = this.VisitExpression(invocation.Arguments[0]);
				using (this.NewArgumentScope(typeValue6.AsTableType.ItemType))
				{
					return TableTypeValue.New(this.VisitExpression(invocation.Arguments[1]).AsFunctionType.ReturnType.AsRecordType);
				}
			}
			if (function.Equals(TableModule.Table.SelectRows) && invocation.Arguments.Count == 2)
			{
				TypeValue typeValue7 = this.VisitExpression(invocation.Arguments[0]);
				using (this.NewArgumentScope(typeValue7.AsTableType.ItemType))
				{
					TypeValue typeValue8 = this.VisitExpression(invocation.Arguments[1]);
					if (typeValue8.TypeKind == ValueKind.Function && !typeValue8.AsFunctionType.ReturnType.NonNullable.Equals(TypeValue.Logical) && !typeValue8.AsFunctionType.ReturnType.Equals(TypeValue.Null))
					{
						return ListTypeValue.New(TypeValue.None);
					}
				}
				return typeValue7;
			}
			if ((function.Equals(TableModule.Table.Sort) || function.Equals(TableModule.Table.ForceColumns)) && invocation.Arguments.Count == 2)
			{
				TypeValue typeValue9 = this.VisitExpression(invocation.Arguments[0]);
				using (this.NewArgumentScope(typeValue9.AsTableType.ItemType))
				{
					this.VisitExpression(invocation.Arguments[1]);
				}
				return typeValue9;
			}
			if (function.Equals(ActionModule.TableAction.UpdateRows) && invocation.Arguments.Count == 2)
			{
				TypeValue typeValue10 = this.VisitExpression(invocation.Arguments[0]);
				using (this.NewArgumentScope(typeValue10.AsTableType.ItemType))
				{
					this.VisitExpression(invocation.Arguments[1]);
				}
				return typeValue10;
			}
			if (function.Equals(Library._Value.NativeQuery) && invocation.Arguments.Count >= 2 && invocation.Arguments.Count <= 4)
			{
				Value[] array = new Value[invocation.Arguments.Count];
				for (int i = 0; i < array.Length; i++)
				{
					IConstantExpression constantExpression = invocation.Arguments[i] as IConstantExpression;
					if (constantExpression == null)
					{
						array = null;
						break;
					}
					array[i] = constantExpression.Value;
				}
				if (array != null)
				{
					return Library._Value.NativeQuery.Invoke(array).Type;
				}
			}
			for (int j = 0; j < invocation.Arguments.Count; j++)
			{
				this.VisitExpression(invocation.Arguments[j]);
			}
			return this.GetReturnType(function, invocation.Arguments);
		}

		// Token: 0x06009968 RID: 39272 RVA: 0x001FB204 File Offset: 0x001F9404
		private TypeValue VisitFunction(IFunctionExpression function)
		{
			return FunctionTypeValue.New(this.VisitExpression(function.Expression), RecordValue.New(Keys.New(function.FunctionType.Parameters[0].Identifier.Name), new Value[] { this.argument }), 1);
		}

		// Token: 0x06009969 RID: 39273 RVA: 0x001FB258 File Offset: 0x001F9458
		private TypeValue VisitProjection(IMultiFieldRecordProjectionExpression projection)
		{
			TypeValue typeValue = this.VisitExpression(projection.Expression);
			if (typeValue.TypeKind == ValueKind.Record)
			{
				RecordTypeValue asRecordType = typeValue.AsRecordType;
				KeysBuilder keysBuilder = default(KeysBuilder);
				Value[] array = new Value[projection.MemberNames.Count];
				for (int i = 0; i < array.Length; i++)
				{
					string name = projection.MemberNames[i].Name;
					keysBuilder.Add(name);
					array[i] = asRecordType.Fields[name];
				}
				return RecordTypeValue.New(RecordValue.New(keysBuilder.ToKeys(), array));
			}
			TableTypeValue asTableType = typeValue.AsTableType;
			Value[] array2 = new Value[projection.MemberNames.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = TextValue.New(projection.MemberNames[j].Name);
			}
			return ListValue.Empty.ToTable(asTableType).SelectColumns(ListValue.New(array2), MissingFieldMode.Error).Type;
		}

		// Token: 0x0600996A RID: 39274 RVA: 0x001FB356 File Offset: 0x001F9556
		private TypeValue VisitBinary(IBinaryExpression binary)
		{
			return OperatorTypeflowModels.Binary(binary.Operator, this.VisitExpression(binary.Left), this.VisitExpression(binary.Right));
		}

		// Token: 0x0600996B RID: 39275 RVA: 0x001FB37B File Offset: 0x001F957B
		private TypeValue VisitConstant(IConstantExpression constant)
		{
			return constant.Value.Type;
		}

		// Token: 0x0600996C RID: 39276 RVA: 0x001FB388 File Offset: 0x001F9588
		private TypeValue VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			TypeValue typeValue = this.VisitExpression(fieldAccess.Expression);
			if (typeValue.TypeKind == ValueKind.Record)
			{
				return typeValue.AsRecordType.Fields[fieldAccess.MemberName.Name]["Type"].AsType;
			}
			return ListTypeValue.New(typeValue.AsTableType.ItemType.Fields[fieldAccess.MemberName.Name]["Type"].AsType);
		}

		// Token: 0x0600996D RID: 39277 RVA: 0x001FB40C File Offset: 0x001F960C
		private TypeValue VisitIf(IIfExpression ifExpression)
		{
			this.VisitExpression(ifExpression.Condition);
			TypeValue typeValue = this.VisitExpression(ifExpression.TrueCase);
			TypeValue typeValue2 = this.VisitExpression(ifExpression.FalseCase);
			return TypeAlgebra.Union(typeValue, typeValue2);
		}

		// Token: 0x0600996E RID: 39278 RVA: 0x001FB445 File Offset: 0x001F9645
		private TypeValue VisitUnary(IUnaryExpression unary)
		{
			return OperatorTypeflowModels.Unary(unary.Operator, this.VisitExpression(unary.Expression));
		}

		// Token: 0x0600996F RID: 39279 RVA: 0x001FB460 File Offset: 0x001F9660
		private TypeValue VisitRecord(IRecordExpression expression)
		{
			Value[] array = new Value[expression.Members.Count];
			KeysBuilder keysBuilder = default(KeysBuilder);
			for (int i = 0; i < array.Length; i++)
			{
				keysBuilder.Add(expression.Members[i].Name);
				array[i] = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					this.VisitExpression(expression.Members[i].Value),
					LogicalValue.False
				});
			}
			return RecordTypeValue.New(RecordValue.New(keysBuilder.ToKeys(), array));
		}

		// Token: 0x06009970 RID: 39280 RVA: 0x001FB500 File Offset: 0x001F9700
		private TypeValue VisitList(IListExpression expression)
		{
			TypeValue typeValue = TypeValue.Any;
			if (expression.Members.Count > 0)
			{
				typeValue = this.VisitExpression(expression.Members[0]);
			}
			for (int i = 1; i < expression.Members.Count; i++)
			{
				typeValue = TypeAlgebra.Union(typeValue, this.VisitExpression(expression.Members[i]));
			}
			return ListTypeValue.New(typeValue);
		}

		// Token: 0x06009971 RID: 39281 RVA: 0x001FB56C File Offset: 0x001F976C
		TypeValue ITypeflowEnvironment.GetType(IExpression expression)
		{
			TypeValue typeValue;
			if (!this.types.TryGetValue(expression, out typeValue))
			{
				typeValue = this.VisitExpression(expression);
			}
			return typeValue;
		}

		// Token: 0x06009972 RID: 39282 RVA: 0x001FB592 File Offset: 0x001F9792
		private LogicalAstToCachedTypeflowResultCursor.ArgumentScope NewArgumentScope(TypeValue argument)
		{
			return new LogicalAstToCachedTypeflowResultCursor.ArgumentScope(this, argument);
		}

		// Token: 0x04005146 RID: 20806
		private Dictionary<IExpression, TypeValue> types = new Dictionary<IExpression, TypeValue>();

		// Token: 0x04005147 RID: 20807
		private TypeValue argument;

		// Token: 0x020017B4 RID: 6068
		private struct ArgumentScope : IDisposable
		{
			// Token: 0x06009973 RID: 39283 RVA: 0x001FB59B File Offset: 0x001F979B
			public ArgumentScope(LogicalAstToCachedTypeflowResultCursor cursor, TypeValue argument)
			{
				this.cursor = cursor;
				this.previousArgument = this.cursor.argument;
				this.cursor.argument = argument;
			}

			// Token: 0x06009974 RID: 39284 RVA: 0x001FB5C1 File Offset: 0x001F97C1
			public void Dispose()
			{
				this.cursor.argument = this.previousArgument;
			}

			// Token: 0x04005148 RID: 20808
			private readonly LogicalAstToCachedTypeflowResultCursor cursor;

			// Token: 0x04005149 RID: 20809
			private readonly TypeValue previousArgument;
		}
	}
}
