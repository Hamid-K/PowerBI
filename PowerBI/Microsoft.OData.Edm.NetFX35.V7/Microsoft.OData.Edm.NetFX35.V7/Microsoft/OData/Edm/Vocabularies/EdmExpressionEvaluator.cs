using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm.Csdl;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000EC RID: 236
	public class EdmExpressionEvaluator
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x00011ED9 File Offset: 0x000100D9
		public EdmExpressionEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions)
		{
			this.builtInFunctions = builtInFunctions;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00011F18 File Offset: 0x00010118
		public EdmExpressionEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions, Func<string, IEdmValue[], IEdmValue> lastChanceOperationApplier)
			: this(builtInFunctions)
		{
			this.lastChanceOperationApplier = lastChanceOperationApplier;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00011F28 File Offset: 0x00010128
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x00011F30 File Offset: 0x00010130
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

		// Token: 0x060006B9 RID: 1721 RVA: 0x00011F39 File Offset: 0x00010139
		public IEdmValue Evaluate(IEdmExpression expression)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			return this.Eval(expression, null);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00011F4F File Offset: 0x0001014F
		public IEdmValue Evaluate(IEdmExpression expression, IEdmStructuredValue context)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			return this.Eval(expression, context);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00011F65 File Offset: 0x00010165
		public IEdmValue Evaluate(IEdmExpression expression, IEdmStructuredValue context, IEdmTypeReference targetType)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(targetType, "targetType");
			return EdmExpressionEvaluator.Cast(targetType, this.Eval(expression, context));
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00011F8D File Offset: 0x0001018D
		protected static IEdmType FindEdmType(string edmTypeName, IEdmModel edmModel)
		{
			return edmModel.FindDeclaredType(edmTypeName);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00011F96 File Offset: 0x00010196
		private static bool InRange(long value, long min, long max)
		{
			return value >= min && value <= max;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00011FA5 File Offset: 0x000101A5
		private static bool FitsInSingle(double value)
		{
			return value >= -3.4028234663852886E+38 && value <= 3.4028234663852886E+38;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00011FC4 File Offset: 0x000101C4
		private static bool MatchesType(IEdmTypeReference targetType, IEdmValue operand)
		{
			return EdmExpressionEvaluator.MatchesType(targetType, operand, true);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00011FD0 File Offset: 0x000101D0
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

		// Token: 0x060006C1 RID: 1729 RVA: 0x000122BC File Offset: 0x000104BC
		private static IEdmValue Cast(IEdmTypeReference targetType, IEdmValue operand)
		{
			IEdmTypeReference type = operand.Type;
			EdmValueKind valueKind = operand.ValueKind;
			if ((type != null && valueKind != EdmValueKind.Null && type.Definition.IsOrInheritsFrom(targetType.Definition)) || targetType.TypeKind() == EdmTypeKind.None)
			{
				return operand;
			}
			bool flag;
			if (valueKind != EdmValueKind.Collection)
			{
				if (valueKind != EdmValueKind.Structured)
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

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001237C File Offset: 0x0001057C
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

		// Token: 0x060006C3 RID: 1731 RVA: 0x00012544 File Offset: 0x00010744
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
				foreach (string text in edmPathExpression.PathSegments)
				{
					edmValue = EdmExpressionEvaluator.FindProperty(text, edmValue);
					if (edmValue == null)
					{
						throw new InvalidOperationException(Strings.Edm_Evaluator_UnboundPath(text));
					}
				}
				return edmValue;
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
			case EdmExpressionKind.FunctionApplication:
			{
				IEdmApplyExpression edmApplyExpression = (IEdmApplyExpression)expression;
				IEdmFunction appliedFunction = edmApplyExpression.AppliedFunction;
				if (appliedFunction != null)
				{
					IList<IEdmExpression> list3 = Enumerable.ToList<IEdmExpression>(edmApplyExpression.Arguments);
					IEdmValue[] array = new IEdmValue[Enumerable.Count<IEdmExpression>(list3)];
					int num = 0;
					foreach (IEdmExpression edmExpression2 in list3)
					{
						array[num++] = this.Eval(edmExpression2, context);
					}
					Func<IEdmValue[], IEdmValue> func;
					if (this.builtInFunctions.TryGetValue(appliedFunction, ref func))
					{
						return func.Invoke(array);
					}
					if (this.lastChanceOperationApplier != null)
					{
						return this.lastChanceOperationApplier.Invoke(appliedFunction.FullName(), array);
					}
				}
				throw new InvalidOperationException(Strings.Edm_Evaluator_UnboundFunction((appliedFunction != null) ? appliedFunction.ToTraceString() : string.Empty));
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
				foreach (string text2 in edmPathExpression2.PathSegments)
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
				IEdmEnumTypeReference edmEnumTypeReference = new EdmEnumTypeReference(declaringType, false);
				if (Enumerable.Count<IEdmEnumMember>(list4) == 1)
				{
					return new EdmEnumValue(edmEnumTypeReference, Enumerable.Single<IEdmEnumMember>(edmEnumMemberExpression.EnumMembers));
				}
				if (!declaringType.IsFlags || !EdmEnumValueParser.IsEnumIntegerType(declaringType))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Type {0} cannot be assigned with multi-values.", new object[] { declaringType.FullName() }));
				}
				long num2 = 0L;
				foreach (IEdmEnumMember edmEnumMember in list4)
				{
					long value = edmEnumMember.Value.Value;
					num2 |= value;
				}
				return new EdmEnumValue(edmEnumTypeReference, new EdmEnumMemberValue(num2));
			}
			default:
				throw new InvalidOperationException(Strings.Edm_Evaluator_UnrecognizedExpressionKind(((int)expression.ExpressionKind).ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00012AAC File Offset: 0x00010CAC
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

		// Token: 0x060006C5 RID: 1733 RVA: 0x00012B04 File Offset: 0x00010D04
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

		// Token: 0x040003FD RID: 1021
		private readonly IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions;

		// Token: 0x040003FE RID: 1022
		private readonly Dictionary<IEdmLabeledExpression, EdmExpressionEvaluator.DelayedValue> labeledValues = new Dictionary<IEdmLabeledExpression, EdmExpressionEvaluator.DelayedValue>();

		// Token: 0x040003FF RID: 1023
		private readonly Func<string, IEdmValue[], IEdmValue> lastChanceOperationApplier;

		// Token: 0x04000400 RID: 1024
		private Func<string, IEdmModel, IEdmType> resolveTypeFromName = (string typeName, IEdmModel edmModel) => EdmExpressionEvaluator.FindEdmType(typeName, edmModel);

		// Token: 0x02000280 RID: 640
		private class DelayedExpressionContext
		{
			// Token: 0x06000F39 RID: 3897 RVA: 0x0002A720 File Offset: 0x00028920
			public DelayedExpressionContext(EdmExpressionEvaluator expressionEvaluator, IEdmStructuredValue context)
			{
				this.expressionEvaluator = expressionEvaluator;
				this.context = context;
			}

			// Token: 0x06000F3A RID: 3898 RVA: 0x0002A736 File Offset: 0x00028936
			public IEdmValue Eval(IEdmExpression expression)
			{
				return this.expressionEvaluator.Eval(expression, this.context);
			}

			// Token: 0x040007C9 RID: 1993
			private readonly EdmExpressionEvaluator expressionEvaluator;

			// Token: 0x040007CA RID: 1994
			private readonly IEdmStructuredValue context;
		}

		// Token: 0x02000281 RID: 641
		private abstract class DelayedValue : IEdmDelayedValue
		{
			// Token: 0x06000F3B RID: 3899 RVA: 0x0002A74A File Offset: 0x0002894A
			public DelayedValue(EdmExpressionEvaluator.DelayedExpressionContext context)
			{
				this.context = context;
			}

			// Token: 0x170004A3 RID: 1187
			// (get) Token: 0x06000F3C RID: 3900
			public abstract IEdmExpression Expression { get; }

			// Token: 0x170004A4 RID: 1188
			// (get) Token: 0x06000F3D RID: 3901 RVA: 0x0002A759 File Offset: 0x00028959
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

			// Token: 0x040007CB RID: 1995
			private readonly EdmExpressionEvaluator.DelayedExpressionContext context;

			// Token: 0x040007CC RID: 1996
			private IEdmValue value;
		}

		// Token: 0x02000282 RID: 642
		private class DelayedRecordProperty : EdmExpressionEvaluator.DelayedValue, IEdmPropertyValue, IEdmDelayedValue
		{
			// Token: 0x06000F3E RID: 3902 RVA: 0x0002A780 File Offset: 0x00028980
			public DelayedRecordProperty(EdmExpressionEvaluator.DelayedExpressionContext context, IEdmPropertyConstructor constructor)
				: base(context)
			{
				this.constructor = constructor;
			}

			// Token: 0x170004A5 RID: 1189
			// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0002A790 File Offset: 0x00028990
			public string Name
			{
				get
				{
					return this.constructor.Name;
				}
			}

			// Token: 0x170004A6 RID: 1190
			// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0002A79D File Offset: 0x0002899D
			public override IEdmExpression Expression
			{
				get
				{
					return this.constructor.Value;
				}
			}

			// Token: 0x040007CD RID: 1997
			private readonly IEdmPropertyConstructor constructor;
		}

		// Token: 0x02000283 RID: 643
		private class DelayedCollectionElement : EdmExpressionEvaluator.DelayedValue
		{
			// Token: 0x06000F41 RID: 3905 RVA: 0x0002A7AA File Offset: 0x000289AA
			public DelayedCollectionElement(EdmExpressionEvaluator.DelayedExpressionContext context, IEdmExpression expression)
				: base(context)
			{
				this.expression = expression;
			}

			// Token: 0x170004A7 RID: 1191
			// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0002A7BA File Offset: 0x000289BA
			public override IEdmExpression Expression
			{
				get
				{
					return this.expression;
				}
			}

			// Token: 0x040007CE RID: 1998
			private readonly IEdmExpression expression;
		}

		// Token: 0x02000284 RID: 644
		private class CastCollectionValue : EdmElement, IEdmCollectionValue, IEdmValue, IEdmElement, IEnumerable<IEdmDelayedValue>, IEnumerable
		{
			// Token: 0x06000F43 RID: 3907 RVA: 0x0002A7C2 File Offset: 0x000289C2
			public CastCollectionValue(IEdmCollectionTypeReference targetCollectionType, IEdmCollectionValue collectionValue)
			{
				this.targetCollectionType = targetCollectionType;
				this.collectionValue = collectionValue;
			}

			// Token: 0x170004A8 RID: 1192
			// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0001402B File Offset: 0x0001222B
			IEnumerable<IEdmDelayedValue> IEdmCollectionValue.Elements
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170004A9 RID: 1193
			// (get) Token: 0x06000F45 RID: 3909 RVA: 0x0002A7D8 File Offset: 0x000289D8
			IEdmTypeReference IEdmValue.Type
			{
				get
				{
					return this.targetCollectionType;
				}
			}

			// Token: 0x170004AA RID: 1194
			// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00009097 File Offset: 0x00007297
			EdmValueKind IEdmValue.ValueKind
			{
				get
				{
					return EdmValueKind.Collection;
				}
			}

			// Token: 0x06000F47 RID: 3911 RVA: 0x0002A7E0 File Offset: 0x000289E0
			IEnumerator<IEdmDelayedValue> IEnumerable<IEdmDelayedValue>.GetEnumerator()
			{
				return new EdmExpressionEvaluator.CastCollectionValue.CastCollectionValueEnumerator(this);
			}

			// Token: 0x06000F48 RID: 3912 RVA: 0x0002A7E0 File Offset: 0x000289E0
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new EdmExpressionEvaluator.CastCollectionValue.CastCollectionValueEnumerator(this);
			}

			// Token: 0x040007CF RID: 1999
			private readonly IEdmCollectionTypeReference targetCollectionType;

			// Token: 0x040007D0 RID: 2000
			private readonly IEdmCollectionValue collectionValue;

			// Token: 0x020002ED RID: 749
			private class CastCollectionValueEnumerator : IEnumerator<IEdmDelayedValue>, IDisposable, IEnumerator
			{
				// Token: 0x060010D2 RID: 4306 RVA: 0x0002C09A File Offset: 0x0002A29A
				public CastCollectionValueEnumerator(EdmExpressionEvaluator.CastCollectionValue value)
				{
					this.value = value;
					this.enumerator = value.collectionValue.Elements.GetEnumerator();
				}

				// Token: 0x170004BE RID: 1214
				// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0002C0BF File Offset: 0x0002A2BF
				public IEdmDelayedValue Current
				{
					get
					{
						return new EdmExpressionEvaluator.CastCollectionValue.CastCollectionValueEnumerator.DelayedCast(this.value.targetCollectionType.ElementType(), this.enumerator.Current);
					}
				}

				// Token: 0x170004BF RID: 1215
				// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0002C0E1 File Offset: 0x0002A2E1
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060010D5 RID: 4309 RVA: 0x0002C0E9 File Offset: 0x0002A2E9
				bool IEnumerator.MoveNext()
				{
					return this.enumerator.MoveNext();
				}

				// Token: 0x060010D6 RID: 4310 RVA: 0x0002C0F6 File Offset: 0x0002A2F6
				void IEnumerator.Reset()
				{
					this.enumerator.Reset();
				}

				// Token: 0x060010D7 RID: 4311 RVA: 0x0002C103 File Offset: 0x0002A303
				void IDisposable.Dispose()
				{
					this.enumerator.Dispose();
				}

				// Token: 0x04000885 RID: 2181
				private readonly EdmExpressionEvaluator.CastCollectionValue value;

				// Token: 0x04000886 RID: 2182
				private readonly IEnumerator<IEdmDelayedValue> enumerator;

				// Token: 0x020002EF RID: 751
				private class DelayedCast : IEdmDelayedValue
				{
					// Token: 0x060010DA RID: 4314 RVA: 0x0002C110 File Offset: 0x0002A310
					public DelayedCast(IEdmTypeReference targetType, IEdmDelayedValue value)
					{
						this.delayedValue = value;
						this.targetType = targetType;
					}

					// Token: 0x170004C1 RID: 1217
					// (get) Token: 0x060010DB RID: 4315 RVA: 0x0002C126 File Offset: 0x0002A326
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

					// Token: 0x04000887 RID: 2183
					private readonly IEdmDelayedValue delayedValue;

					// Token: 0x04000888 RID: 2184
					private readonly IEdmTypeReference targetType;

					// Token: 0x04000889 RID: 2185
					private IEdmValue value;
				}
			}
		}
	}
}
