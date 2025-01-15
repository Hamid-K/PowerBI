using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Library.Values;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Evaluation
{
	// Token: 0x020000DE RID: 222
	public class EdmExpressionEvaluator
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0000B750 File Offset: 0x00009950
		public EdmExpressionEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions)
		{
			this.builtInFunctions = builtInFunctions;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000B78D File Offset: 0x0000998D
		public EdmExpressionEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions, Func<string, IEdmValue[], IEdmValue> lastChanceOperationApplier)
			: this(builtInFunctions)
		{
			this.lastChanceOperationApplier = lastChanceOperationApplier;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000B79D File Offset: 0x0000999D
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000B7A5 File Offset: 0x000099A5
		protected Func<string, IEdmModel, IEdmType> ResolveTypeFromName
		{
			get
			{
				return this.resolveTypeFromName;
			}
			set
			{
				this.resolveTypeFromName = value;
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000B7AE File Offset: 0x000099AE
		public IEdmValue Evaluate(IEdmExpression expression)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			return this.Eval(expression, null);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000B7C4 File Offset: 0x000099C4
		public IEdmValue Evaluate(IEdmExpression expression, IEdmStructuredValue context)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			return this.Eval(expression, context);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000B7DA File Offset: 0x000099DA
		public IEdmValue Evaluate(IEdmExpression expression, IEdmStructuredValue context, IEdmTypeReference targetType)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(targetType, "targetType");
			return EdmExpressionEvaluator.Cast(targetType, this.Eval(expression, context));
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000B802 File Offset: 0x00009A02
		protected static IEdmType FindEdmType(string edmTypeName, IEdmModel edmModel)
		{
			return edmModel.FindDeclaredType(edmTypeName);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000B80B File Offset: 0x00009A0B
		private static bool InRange(long value, long min, long max)
		{
			return value >= min && value <= max;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000B81A File Offset: 0x00009A1A
		private static bool FitsInSingle(double value)
		{
			return value >= -3.4028234663852886E+38 && value <= 3.4028234663852886E+38;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000B839 File Offset: 0x00009A39
		private static bool MatchesType(IEdmTypeReference targetType, IEdmValue operand)
		{
			return EdmExpressionEvaluator.MatchesType(targetType, operand, true);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000B844 File Offset: 0x00009A44
		private static bool MatchesType(IEdmTypeReference targetType, IEdmValue operand, bool testPropertyTypes)
		{
			IEdmTypeReference type = operand.Type;
			EdmValueKind valueKind = operand.ValueKind;
			if (type != null && valueKind != EdmValueKind.Null && type.Definition.IsOrInheritsFrom(targetType.Definition))
			{
				return true;
			}
			switch (valueKind)
			{
			case EdmValueKind.Binary:
				if (targetType.IsBinary())
				{
					IEdmBinaryTypeReference edmBinaryTypeReference = targetType.AsBinary();
					return edmBinaryTypeReference.IsUnbounded || edmBinaryTypeReference.MaxLength == null || edmBinaryTypeReference.MaxLength.Value >= ((IEdmBinaryValue)operand).Value.Length;
				}
				break;
			case EdmValueKind.Boolean:
				return targetType.IsBoolean();
			case EdmValueKind.Collection:
				if (targetType.IsCollection())
				{
					IEdmTypeReference edmTypeReference = targetType.AsCollection().ElementType();
					foreach (IEdmDelayedValue edmDelayedValue in ((IEdmCollectionValue)operand).Elements)
					{
						if (!EdmExpressionEvaluator.MatchesType(edmTypeReference, edmDelayedValue.Value))
						{
							return false;
						}
					}
					return true;
				}
				break;
			case EdmValueKind.DateTimeOffset:
				return targetType.IsDateTimeOffset();
			case EdmValueKind.Decimal:
				return targetType.IsDecimal();
			case EdmValueKind.Enum:
				return ((IEdmEnumValue)operand).Type.Definition.IsEquivalentTo(targetType.Definition);
			case EdmValueKind.Floating:
				return targetType.IsDouble() || (targetType.IsSingle() && EdmExpressionEvaluator.FitsInSingle(((IEdmFloatingValue)operand).Value));
			case EdmValueKind.Guid:
				return targetType.IsGuid();
			case EdmValueKind.Integer:
				if (targetType.TypeKind() == EdmTypeKind.Primitive)
				{
					switch (targetType.AsPrimitive().PrimitiveKind())
					{
					case EdmPrimitiveTypeKind.Byte:
						return EdmExpressionEvaluator.InRange(((IEdmIntegerValue)operand).Value, 0L, 255L);
					case EdmPrimitiveTypeKind.Double:
					case EdmPrimitiveTypeKind.Int64:
					case EdmPrimitiveTypeKind.Single:
						return true;
					case EdmPrimitiveTypeKind.Int16:
						return EdmExpressionEvaluator.InRange(((IEdmIntegerValue)operand).Value, -32768L, 32767L);
					case EdmPrimitiveTypeKind.Int32:
						return EdmExpressionEvaluator.InRange(((IEdmIntegerValue)operand).Value, -2147483648L, 2147483647L);
					case EdmPrimitiveTypeKind.SByte:
						return EdmExpressionEvaluator.InRange(((IEdmIntegerValue)operand).Value, -128L, 127L);
					}
				}
				break;
			case EdmValueKind.Null:
				return targetType.IsNullable;
			case EdmValueKind.String:
				if (targetType.IsString())
				{
					IEdmStringTypeReference edmStringTypeReference = targetType.AsString();
					return edmStringTypeReference.IsUnbounded || edmStringTypeReference.MaxLength == null || edmStringTypeReference.MaxLength.Value >= ((IEdmStringValue)operand).Value.Length;
				}
				break;
			case EdmValueKind.Structured:
				if (targetType.IsStructured())
				{
					return EdmExpressionEvaluator.AssertOrMatchStructuredType(targetType.AsStructured(), (IEdmStructuredValue)operand, testPropertyTypes, null);
				}
				break;
			case EdmValueKind.Duration:
				return targetType.IsDuration();
			case EdmValueKind.Date:
				return targetType.IsDate();
			case EdmValueKind.TimeOfDay:
				return targetType.IsTimeOfDay();
			}
			return false;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000BB34 File Offset: 0x00009D34
		private static IEdmValue Cast(IEdmTypeReference targetType, IEdmValue operand)
		{
			IEdmTypeReference type = operand.Type;
			EdmValueKind valueKind = operand.ValueKind;
			if ((type != null && valueKind != EdmValueKind.Null && type.Definition.IsOrInheritsFrom(targetType.Definition)) || targetType.TypeKind() == EdmTypeKind.None)
			{
				return operand;
			}
			EdmValueKind edmValueKind = valueKind;
			bool flag;
			if (edmValueKind != EdmValueKind.Collection)
			{
				if (edmValueKind != EdmValueKind.Structured)
				{
					flag = EdmExpressionEvaluator.MatchesType(targetType, operand);
				}
				else if (targetType.IsStructured())
				{
					IEdmStructuredTypeReference edmStructuredTypeReference = targetType.AsStructured();
					List<IEdmPropertyValue> list = new List<IEdmPropertyValue>();
					flag = EdmExpressionEvaluator.AssertOrMatchStructuredType(edmStructuredTypeReference, (IEdmStructuredValue)operand, true, list);
					if (flag)
					{
						return new EdmStructuredValue(edmStructuredTypeReference, list);
					}
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				if (targetType.IsCollection())
				{
					return new EdmExpressionEvaluator.CastCollectionValue(targetType.AsCollection(), (IEdmCollectionValue)operand);
				}
				flag = false;
			}
			if (!flag)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_FailedTypeAssertion(targetType.ToTraceString()));
			}
			return operand;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		private static bool AssertOrMatchStructuredType(IEdmStructuredTypeReference structuredTargetType, IEdmStructuredValue structuredValue, bool testPropertyTypes, List<IEdmPropertyValue> newProperties)
		{
			IEdmTypeReference type = structuredValue.Type;
			if (type != null && !structuredTargetType.StructuredDefinition().InheritsFrom(type.AsStructured().StructuredDefinition()))
			{
				return false;
			}
			HashSetInternal<IEdmPropertyValue> hashSetInternal = new HashSetInternal<IEdmPropertyValue>();
			foreach (IEdmProperty edmProperty in structuredTargetType.StructuralProperties())
			{
				IEdmPropertyValue edmPropertyValue = structuredValue.FindPropertyValue(edmProperty.Name);
				if (edmPropertyValue == null)
				{
					return false;
				}
				hashSetInternal.Add(edmPropertyValue);
				if (testPropertyTypes)
				{
					if (newProperties != null)
					{
						newProperties.Add(new EdmPropertyValue(edmPropertyValue.Name, EdmExpressionEvaluator.Cast(edmProperty.Type, edmPropertyValue.Value)));
					}
					else if (!EdmExpressionEvaluator.MatchesType(edmProperty.Type, edmPropertyValue.Value))
					{
						return false;
					}
				}
			}
			if (structuredTargetType.IsEntity())
			{
				foreach (IEdmNavigationProperty edmNavigationProperty in structuredTargetType.AsEntity().NavigationProperties())
				{
					IEdmPropertyValue edmPropertyValue2 = structuredValue.FindPropertyValue(edmNavigationProperty.Name);
					if (edmPropertyValue2 == null)
					{
						return false;
					}
					if (testPropertyTypes && !EdmExpressionEvaluator.MatchesType(edmNavigationProperty.Type, edmPropertyValue2.Value, false))
					{
						return false;
					}
					hashSetInternal.Add(edmPropertyValue2);
					if (newProperties != null)
					{
						newProperties.Add(edmPropertyValue2);
					}
				}
			}
			if (newProperties != null)
			{
				foreach (IEdmPropertyValue edmPropertyValue3 in structuredValue.PropertyValues)
				{
					if (!hashSetInternal.Contains(edmPropertyValue3))
					{
						newProperties.Add(edmPropertyValue3);
					}
				}
			}
			return true;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		private IEdmValue Eval(IEdmExpression expression, IEdmStructuredValue context)
		{
			switch (expression.ExpressionKind)
			{
			case EdmExpressionKind.BinaryConstant:
				return (IEdmBinaryConstantExpression)expression;
			case EdmExpressionKind.BooleanConstant:
				return (IEdmBooleanConstantExpression)expression;
			case EdmExpressionKind.DateTimeOffsetConstant:
				return (IEdmDateTimeOffsetConstantExpression)expression;
			case EdmExpressionKind.DecimalConstant:
				return (IEdmDecimalConstantExpression)expression;
			case EdmExpressionKind.FloatingConstant:
				return (IEdmFloatingConstantExpression)expression;
			case EdmExpressionKind.GuidConstant:
				return (IEdmGuidConstantExpression)expression;
			case EdmExpressionKind.IntegerConstant:
				return (IEdmIntegerConstantExpression)expression;
			case EdmExpressionKind.StringConstant:
				return (IEdmStringConstantExpression)expression;
			case EdmExpressionKind.DurationConstant:
				return (IEdmDurationConstantExpression)expression;
			case EdmExpressionKind.Null:
				return (IEdmNullExpression)expression;
			case EdmExpressionKind.Record:
			{
				IEdmRecordExpression edmRecordExpression = (IEdmRecordExpression)expression;
				EdmExpressionEvaluator.DelayedExpressionContext delayedExpressionContext = new EdmExpressionEvaluator.DelayedExpressionContext(this, context);
				List<IEdmPropertyValue> list = new List<IEdmPropertyValue>();
				foreach (IEdmPropertyConstructor edmPropertyConstructor in edmRecordExpression.Properties)
				{
					list.Add(new EdmExpressionEvaluator.DelayedRecordProperty(delayedExpressionContext, edmPropertyConstructor));
				}
				return new EdmStructuredValue((edmRecordExpression.DeclaredType != null) ? edmRecordExpression.DeclaredType.AsStructured() : null, list);
			}
			case EdmExpressionKind.Collection:
			{
				IEdmCollectionExpression edmCollectionExpression = (IEdmCollectionExpression)expression;
				EdmExpressionEvaluator.DelayedExpressionContext delayedExpressionContext2 = new EdmExpressionEvaluator.DelayedExpressionContext(this, context);
				List<IEdmDelayedValue> list2 = new List<IEdmDelayedValue>();
				foreach (IEdmExpression edmExpression in edmCollectionExpression.Elements)
				{
					list2.Add(this.MapLabeledExpressionToDelayedValue(edmExpression, delayedExpressionContext2, context));
				}
				return new EdmCollectionValue((edmCollectionExpression.DeclaredType != null) ? edmCollectionExpression.DeclaredType.AsCollection() : null, list2);
			}
			case EdmExpressionKind.Path:
			{
				if (context == null)
				{
					throw new InvalidOperationException(Strings.Edm_Evaluator_NoContextPath);
				}
				IEdmPathExpression edmPathExpression = (IEdmPathExpression)expression;
				IEdmValue edmValue = context;
				foreach (string text in edmPathExpression.Path)
				{
					edmValue = EdmExpressionEvaluator.FindProperty(text, edmValue);
					if (edmValue == null)
					{
						throw new InvalidOperationException(Strings.Edm_Evaluator_UnboundPath(text));
					}
				}
				return edmValue;
			}
			case EdmExpressionKind.ParameterReference:
			case EdmExpressionKind.OperationReference:
			case EdmExpressionKind.PropertyReference:
			case EdmExpressionKind.ValueTermReference:
			case EdmExpressionKind.EntitySetReference:
				throw new InvalidOperationException(Strings.Edm_Evaluator_UnrecognizedExpressionKind(((int)expression.ExpressionKind).ToString(CultureInfo.InvariantCulture)));
			case EdmExpressionKind.EnumMemberReference:
			{
				IEdmEnumMemberReferenceExpression edmEnumMemberReferenceExpression = (IEdmEnumMemberReferenceExpression)expression;
				IEdmEnumMember referencedEnumMember = edmEnumMemberReferenceExpression.ReferencedEnumMember;
				IEdmEnumTypeReference edmEnumTypeReference = new EdmEnumTypeReference(referencedEnumMember.DeclaringType, false);
				return new EdmEnumValue(edmEnumTypeReference, edmEnumMemberReferenceExpression.ReferencedEnumMember);
			}
			case EdmExpressionKind.If:
			{
				IEdmIfExpression edmIfExpression = (IEdmIfExpression)expression;
				if (((IEdmBooleanValue)this.Eval(edmIfExpression.TestExpression, context)).Value)
				{
					return this.Eval(edmIfExpression.TrueExpression, context);
				}
				return this.Eval(edmIfExpression.FalseExpression, context);
			}
			case EdmExpressionKind.Cast:
			{
				IEdmCastExpression edmCastExpression = (IEdmCastExpression)expression;
				IEdmValue edmValue2 = this.Eval(edmCastExpression.Operand, context);
				IEdmTypeReference type = edmCastExpression.Type;
				return EdmExpressionEvaluator.Cast(type, edmValue2);
			}
			case EdmExpressionKind.IsType:
			{
				IEdmIsTypeExpression edmIsTypeExpression = (IEdmIsTypeExpression)expression;
				IEdmValue edmValue3 = this.Eval(edmIsTypeExpression.Operand, context);
				IEdmTypeReference type2 = edmIsTypeExpression.Type;
				return new EdmBooleanConstant(EdmExpressionEvaluator.MatchesType(type2, edmValue3));
			}
			case EdmExpressionKind.OperationApplication:
			{
				IEdmApplyExpression edmApplyExpression = (IEdmApplyExpression)expression;
				IEdmExpression appliedOperation = edmApplyExpression.AppliedOperation;
				IEdmOperationReferenceExpression edmOperationReferenceExpression = appliedOperation as IEdmOperationReferenceExpression;
				if (edmOperationReferenceExpression != null)
				{
					IList<IEdmExpression> list3 = Enumerable.ToList<IEdmExpression>(edmApplyExpression.Arguments);
					IEdmValue[] array = new IEdmValue[Enumerable.Count<IEdmExpression>(list3)];
					int num = 0;
					foreach (IEdmExpression edmExpression2 in list3)
					{
						array[num++] = this.Eval(edmExpression2, context);
					}
					IEdmOperation referencedOperation = edmOperationReferenceExpression.ReferencedOperation;
					Func<IEdmValue[], IEdmValue> func;
					if (this.builtInFunctions.TryGetValue(referencedOperation, ref func))
					{
						return func.Invoke(array);
					}
					if (this.lastChanceOperationApplier != null)
					{
						return this.lastChanceOperationApplier.Invoke(referencedOperation.FullName(), array);
					}
				}
				throw new InvalidOperationException(Strings.Edm_Evaluator_UnboundFunction((edmOperationReferenceExpression != null) ? edmOperationReferenceExpression.ReferencedOperation.ToTraceString() : string.Empty));
			}
			case EdmExpressionKind.LabeledExpressionReference:
				return this.MapLabeledExpressionToDelayedValue(((IEdmLabeledExpressionReferenceExpression)expression).ReferencedLabeledExpression, null, context).Value;
			case EdmExpressionKind.Labeled:
				return this.MapLabeledExpressionToDelayedValue(expression, new EdmExpressionEvaluator.DelayedExpressionContext(this, context), context).Value;
			case EdmExpressionKind.PropertyPath:
			case EdmExpressionKind.NavigationPropertyPath:
			{
				EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
				IEdmPathExpression edmPathExpression2 = (IEdmPathExpression)expression;
				IEdmValue edmValue4 = context;
				foreach (string text2 in edmPathExpression2.Path)
				{
					edmValue4 = EdmExpressionEvaluator.FindProperty(text2, edmValue4);
					if (edmValue4 == null)
					{
						throw new InvalidOperationException(Strings.Edm_Evaluator_UnboundPath(text2));
					}
				}
				return edmValue4;
			}
			case EdmExpressionKind.DateConstant:
				return (IEdmDateConstantExpression)expression;
			case EdmExpressionKind.TimeOfDayConstant:
				return (IEdmTimeOfDayConstantExpression)expression;
			case EdmExpressionKind.EnumMember:
			{
				IEdmEnumMemberExpression edmEnumMemberExpression = (IEdmEnumMemberExpression)expression;
				List<IEdmEnumMember> list4 = Enumerable.ToList<IEdmEnumMember>(edmEnumMemberExpression.EnumMembers);
				IEdmEnumType declaringType = Enumerable.First<IEdmEnumMember>(list4).DeclaringType;
				IEdmEnumTypeReference edmEnumTypeReference2 = new EdmEnumTypeReference(declaringType, false);
				if (Enumerable.Count<IEdmEnumMember>(list4) == 1)
				{
					return new EdmEnumValue(edmEnumTypeReference2, Enumerable.Single<IEdmEnumMember>(edmEnumMemberExpression.EnumMembers));
				}
				if (!declaringType.IsFlags || !EdmEnumValueParser.IsEnumIntergeType(declaringType))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Type {0} cannot be assigned with multi-values.", new object[] { declaringType.FullName() }));
				}
				long num2 = 0L;
				foreach (IEdmEnumMember edmEnumMember in list4)
				{
					long value = (edmEnumMember.Value as EdmIntegerConstant).Value;
					num2 |= value;
				}
				return new EdmEnumValue(edmEnumTypeReference2, new EdmIntegerConstant(num2));
			}
			default:
				throw new InvalidOperationException(Strings.Edm_Evaluator_UnrecognizedExpressionKind(((int)expression.ExpressionKind).ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		private IEdmDelayedValue MapLabeledExpressionToDelayedValue(IEdmExpression expression, EdmExpressionEvaluator.DelayedExpressionContext delayedContext, IEdmStructuredValue context)
		{
			IEdmLabeledExpression edmLabeledExpression = expression as IEdmLabeledExpression;
			if (edmLabeledExpression == null)
			{
				return new EdmExpressionEvaluator.DelayedCollectionElement(delayedContext, expression);
			}
			EdmExpressionEvaluator.DelayedValue delayedValue;
			if (this.labeledValues.TryGetValue(edmLabeledExpression, ref delayedValue))
			{
				return delayedValue;
			}
			delayedValue = new EdmExpressionEvaluator.DelayedCollectionElement(delayedContext ?? new EdmExpressionEvaluator.DelayedExpressionContext(this, context), edmLabeledExpression.Expression);
			this.labeledValues[edmLabeledExpression] = delayedValue;
			return delayedValue;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000C408 File Offset: 0x0000A608
		private static IEdmValue FindProperty(string name, IEdmValue context)
		{
			IEdmValue edmValue = null;
			IEdmStructuredValue edmStructuredValue = context as IEdmStructuredValue;
			if (edmStructuredValue != null)
			{
				IEdmPropertyValue edmPropertyValue = edmStructuredValue.FindPropertyValue(name);
				if (edmPropertyValue != null)
				{
					edmValue = edmPropertyValue.Value;
				}
			}
			return edmValue;
		}

		// Token: 0x040001B6 RID: 438
		private readonly IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions;

		// Token: 0x040001B7 RID: 439
		private readonly Dictionary<IEdmLabeledExpression, EdmExpressionEvaluator.DelayedValue> labeledValues = new Dictionary<IEdmLabeledExpression, EdmExpressionEvaluator.DelayedValue>();

		// Token: 0x040001B8 RID: 440
		private readonly Func<string, IEdmValue[], IEdmValue> lastChanceOperationApplier;

		// Token: 0x040001B9 RID: 441
		private Func<string, IEdmModel, IEdmType> resolveTypeFromName = (string typeName, IEdmModel edmModel) => EdmExpressionEvaluator.FindEdmType(typeName, edmModel);

		// Token: 0x020000DF RID: 223
		private class DelayedExpressionContext
		{
			// Token: 0x06000493 RID: 1171 RVA: 0x0000C434 File Offset: 0x0000A634
			public DelayedExpressionContext(EdmExpressionEvaluator expressionEvaluator, IEdmStructuredValue context)
			{
				this.expressionEvaluator = expressionEvaluator;
				this.context = context;
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x0000C44A File Offset: 0x0000A64A
			public IEdmValue Eval(IEdmExpression expression)
			{
				return this.expressionEvaluator.Eval(expression, this.context);
			}

			// Token: 0x040001BB RID: 443
			private readonly EdmExpressionEvaluator expressionEvaluator;

			// Token: 0x040001BC RID: 444
			private readonly IEdmStructuredValue context;
		}

		// Token: 0x020000E1 RID: 225
		private abstract class DelayedValue : IEdmDelayedValue
		{
			// Token: 0x06000496 RID: 1174 RVA: 0x0000C45E File Offset: 0x0000A65E
			public DelayedValue(EdmExpressionEvaluator.DelayedExpressionContext context)
			{
				this.context = context;
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x06000497 RID: 1175
			public abstract IEdmExpression Expression { get; }

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000C46D File Offset: 0x0000A66D
			public IEdmValue Value
			{
				get
				{
					if (this.value == null)
					{
						this.value = this.context.Eval(this.Expression);
					}
					return this.value;
				}
			}

			// Token: 0x040001BD RID: 445
			private readonly EdmExpressionEvaluator.DelayedExpressionContext context;

			// Token: 0x040001BE RID: 446
			private IEdmValue value;
		}

		// Token: 0x020000E3 RID: 227
		private class DelayedRecordProperty : EdmExpressionEvaluator.DelayedValue, IEdmPropertyValue, IEdmDelayedValue
		{
			// Token: 0x0600049A RID: 1178 RVA: 0x0000C494 File Offset: 0x0000A694
			public DelayedRecordProperty(EdmExpressionEvaluator.DelayedExpressionContext context, IEdmPropertyConstructor constructor)
				: base(context)
			{
				this.constructor = constructor;
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
			public string Name
			{
				get
				{
					return this.constructor.Name;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000C4B1 File Offset: 0x0000A6B1
			public override IEdmExpression Expression
			{
				get
				{
					return this.constructor.Value;
				}
			}

			// Token: 0x040001BF RID: 447
			private readonly IEdmPropertyConstructor constructor;
		}

		// Token: 0x020000E4 RID: 228
		private class DelayedCollectionElement : EdmExpressionEvaluator.DelayedValue
		{
			// Token: 0x0600049D RID: 1181 RVA: 0x0000C4BE File Offset: 0x0000A6BE
			public DelayedCollectionElement(EdmExpressionEvaluator.DelayedExpressionContext context, IEdmExpression expression)
				: base(context)
			{
				this.expression = expression;
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000C4CE File Offset: 0x0000A6CE
			public override IEdmExpression Expression
			{
				get
				{
					return this.expression;
				}
			}

			// Token: 0x040001C0 RID: 448
			private readonly IEdmExpression expression;
		}

		// Token: 0x020000E6 RID: 230
		private class CastCollectionValue : EdmElement, IEdmCollectionValue, IEdmValue, IEdmElement, IEnumerable<IEdmDelayedValue>, IEnumerable
		{
			// Token: 0x060004A0 RID: 1184 RVA: 0x0000C4D6 File Offset: 0x0000A6D6
			public CastCollectionValue(IEdmCollectionTypeReference targetCollectionType, IEdmCollectionValue collectionValue)
			{
				this.targetCollectionType = targetCollectionType;
				this.collectionValue = collectionValue;
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000C4EC File Offset: 0x0000A6EC
			IEnumerable<IEdmDelayedValue> IEdmCollectionValue.Elements
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000C4EF File Offset: 0x0000A6EF
			IEdmTypeReference IEdmValue.Type
			{
				get
				{
					return this.targetCollectionType;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000C4F7 File Offset: 0x0000A6F7
			EdmValueKind IEdmValue.ValueKind
			{
				get
				{
					return EdmValueKind.Collection;
				}
			}

			// Token: 0x060004A4 RID: 1188 RVA: 0x0000C4FA File Offset: 0x0000A6FA
			IEnumerator<IEdmDelayedValue> IEnumerable<IEdmDelayedValue>.GetEnumerator()
			{
				return new EdmExpressionEvaluator.CastCollectionValue.CastCollectionValueEnumerator(this);
			}

			// Token: 0x060004A5 RID: 1189 RVA: 0x0000C502 File Offset: 0x0000A702
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new EdmExpressionEvaluator.CastCollectionValue.CastCollectionValueEnumerator(this);
			}

			// Token: 0x040001C1 RID: 449
			private readonly IEdmCollectionTypeReference targetCollectionType;

			// Token: 0x040001C2 RID: 450
			private readonly IEdmCollectionValue collectionValue;

			// Token: 0x020000E7 RID: 231
			private class CastCollectionValueEnumerator : IEnumerator<IEdmDelayedValue>, IDisposable, IEnumerator
			{
				// Token: 0x060004A6 RID: 1190 RVA: 0x0000C50A File Offset: 0x0000A70A
				public CastCollectionValueEnumerator(EdmExpressionEvaluator.CastCollectionValue value)
				{
					this.value = value;
					this.enumerator = value.collectionValue.Elements.GetEnumerator();
				}

				// Token: 0x170001E3 RID: 483
				// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000C52F File Offset: 0x0000A72F
				public IEdmDelayedValue Current
				{
					get
					{
						return new EdmExpressionEvaluator.CastCollectionValue.CastCollectionValueEnumerator.DelayedCast(this.value.targetCollectionType.ElementType(), this.enumerator.Current);
					}
				}

				// Token: 0x170001E4 RID: 484
				// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000C551 File Offset: 0x0000A751
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060004A9 RID: 1193 RVA: 0x0000C559 File Offset: 0x0000A759
				bool IEnumerator.MoveNext()
				{
					return this.enumerator.MoveNext();
				}

				// Token: 0x060004AA RID: 1194 RVA: 0x0000C566 File Offset: 0x0000A766
				void IEnumerator.Reset()
				{
					this.enumerator.Reset();
				}

				// Token: 0x060004AB RID: 1195 RVA: 0x0000C573 File Offset: 0x0000A773
				void IDisposable.Dispose()
				{
					this.enumerator.Dispose();
				}

				// Token: 0x040001C3 RID: 451
				private readonly EdmExpressionEvaluator.CastCollectionValue value;

				// Token: 0x040001C4 RID: 452
				private readonly IEnumerator<IEdmDelayedValue> enumerator;

				// Token: 0x020000E8 RID: 232
				private class DelayedCast : IEdmDelayedValue
				{
					// Token: 0x060004AC RID: 1196 RVA: 0x0000C580 File Offset: 0x0000A780
					public DelayedCast(IEdmTypeReference targetType, IEdmDelayedValue value)
					{
						this.delayedValue = value;
						this.targetType = targetType;
					}

					// Token: 0x170001E5 RID: 485
					// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000C596 File Offset: 0x0000A796
					public IEdmValue Value
					{
						get
						{
							if (this.value == null)
							{
								this.value = EdmExpressionEvaluator.Cast(this.targetType, this.delayedValue.Value);
							}
							return this.value;
						}
					}

					// Token: 0x040001C5 RID: 453
					private readonly IEdmDelayedValue delayedValue;

					// Token: 0x040001C6 RID: 454
					private readonly IEdmTypeReference targetType;

					// Token: 0x040001C7 RID: 455
					private IEdmValue value;
				}
			}
		}
	}
}
