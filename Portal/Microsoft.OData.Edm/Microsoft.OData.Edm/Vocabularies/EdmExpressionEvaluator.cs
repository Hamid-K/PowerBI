using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm.Csdl;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E4 RID: 228
	public class EdmExpressionEvaluator
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x00010009 File Offset: 0x0000E209
		public EdmExpressionEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions)
		{
			this.builtInFunctions = builtInFunctions;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00010048 File Offset: 0x0000E248
		public EdmExpressionEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions, Func<string, IEdmValue[], IEdmValue> lastChanceOperationApplier)
			: this(builtInFunctions)
		{
			this.lastChanceOperationApplier = lastChanceOperationApplier;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00010058 File Offset: 0x0000E258
		public EdmExpressionEvaluator(IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions, Func<string, IEdmValue[], IEdmValue> lastChanceOperationApplier, Func<IEdmModel, IEdmType, string, string, IEdmExpression> getAnnotationExpressionForType, Func<IEdmModel, IEdmType, string, string, string, IEdmExpression> getAnnotationExpressionForProperty, IEdmModel edmModel)
			: this(builtInFunctions, lastChanceOperationApplier)
		{
			this.getAnnotationExpressionForType = getAnnotationExpressionForType;
			this.getAnnotationExpressionForProperty = getAnnotationExpressionForProperty;
			this.edmModel = edmModel;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00010079 File Offset: 0x0000E279
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00010081 File Offset: 0x0000E281
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

		// Token: 0x060006DA RID: 1754 RVA: 0x0001008A File Offset: 0x0000E28A
		public IEdmValue Evaluate(IEdmExpression expression)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			return this.Eval(expression, null);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000100A0 File Offset: 0x0000E2A0
		public IEdmValue Evaluate(IEdmExpression expression, IEdmStructuredValue context)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			return this.Eval(expression, context);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000100B6 File Offset: 0x0000E2B6
		public IEdmValue Evaluate(IEdmExpression expression, IEdmStructuredValue context, IEdmTypeReference targetType)
		{
			EdmUtil.CheckArgumentNull<IEdmExpression>(expression, "expression");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(targetType, "targetType");
			return EdmExpressionEvaluator.Cast(targetType, this.Eval(expression, context));
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000100DE File Offset: 0x0000E2DE
		protected static IEdmType FindEdmType(string edmTypeName, IEdmModel edmModel)
		{
			return edmModel.FindDeclaredType(edmTypeName);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000100E7 File Offset: 0x0000E2E7
		private static bool InRange(long value, long min, long max)
		{
			return value >= min && value <= max;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000100F6 File Offset: 0x0000E2F6
		private static bool FitsInSingle(double value)
		{
			return value >= -3.4028234663852886E+38 && value <= 3.4028234663852886E+38;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00010115 File Offset: 0x0000E315
		private static bool MatchesType(IEdmTypeReference targetType, IEdmValue operand)
		{
			return EdmExpressionEvaluator.MatchesType(targetType, operand, true);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00010120 File Offset: 0x0000E320
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

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001040C File Offset: 0x0000E60C
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

		// Token: 0x060006E3 RID: 1763 RVA: 0x000104CC File Offset: 0x0000E6CC
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

		// Token: 0x060006E4 RID: 1764 RVA: 0x00010694 File Offset: 0x0000E894
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
					if (text.Contains("@"))
					{
						string[] array = text.Split(new char[] { '@' });
						string text2 = array[0];
						string text3 = array[1];
						IEdmExpression edmExpression2 = null;
						if (!string.IsNullOrWhiteSpace(text3))
						{
							string[] array2 = text3.Split(new char[] { '#' });
							if (array2.Length <= 2)
							{
								string text4 = array2[0];
								string text5 = ((array2.Length == 2) ? array2[1] : null);
								if (string.IsNullOrWhiteSpace(text2) && this.getAnnotationExpressionForType != null)
								{
									edmExpression2 = this.getAnnotationExpressionForType(this.edmModel, context.Type.Definition, text4, text5);
								}
								else if (!string.IsNullOrWhiteSpace(text2) && this.getAnnotationExpressionForProperty != null)
								{
									edmExpression2 = this.getAnnotationExpressionForProperty(this.edmModel, context.Type.Definition, text2, text4, text5);
								}
							}
						}
						if (edmExpression2 == null)
						{
							edmValue = null;
							break;
						}
						edmValue = this.Eval(edmExpression2, context);
					}
					else if (text == "$count")
					{
						IEdmCollectionValue edmCollectionValue = edmValue as IEdmCollectionValue;
						if (edmCollectionValue == null)
						{
							edmValue = null;
							break;
						}
						edmValue = new EdmIntegerConstant((long)edmCollectionValue.Elements.Count<IEdmDelayedValue>());
					}
					else if (text.Contains("."))
					{
						if (this.edmModel == null)
						{
							throw new InvalidOperationException(Strings.Edm_Evaluator_TypeCastNeedsEdmModel);
						}
						IEdmType edmType = this.resolveTypeFromName(text, this.edmModel);
						if (edmType == null)
						{
							edmValue = null;
							break;
						}
						IEdmTypeReference type = edmValue.Type;
						EdmValueKind valueKind = edmValue.ValueKind;
						if (valueKind == EdmValueKind.Collection)
						{
							List<IEdmDelayedValue> list3 = new List<IEdmDelayedValue>();
							IEdmCollectionValue edmCollectionValue2 = edmValue as IEdmCollectionValue;
							foreach (IEdmDelayedValue edmDelayedValue in edmCollectionValue2.Elements)
							{
								if (edmDelayedValue.Value.Type.Definition.IsOrInheritsFrom(edmType))
								{
									list3.Add(edmDelayedValue);
								}
							}
							edmValue = new EdmCollectionValue(new EdmCollectionTypeReference(new EdmCollectionType(edmType.GetTypeReference(false))), list3);
						}
						else if (valueKind != EdmValueKind.Structured || (valueKind == EdmValueKind.Structured && !type.Definition.IsOrInheritsFrom(edmType)))
						{
							edmValue = null;
							break;
						}
					}
					else
					{
						edmValue = EdmExpressionEvaluator.FindProperty(text, edmValue);
						if (edmValue == null)
						{
							throw new InvalidOperationException(Strings.Edm_Evaluator_UnboundPath(text));
						}
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
				IEdmTypeReference type2 = edmCastExpression.Type;
				return EdmExpressionEvaluator.Cast(type2, edmValue2);
			}
			case EdmExpressionKind.IsType:
			{
				IEdmIsTypeExpression edmIsTypeExpression = (IEdmIsTypeExpression)expression;
				IEdmValue edmValue3 = this.Eval(edmIsTypeExpression.Operand, context);
				IEdmTypeReference type3 = edmIsTypeExpression.Type;
				return new EdmBooleanConstant(EdmExpressionEvaluator.MatchesType(type3, edmValue3));
			}
			case EdmExpressionKind.FunctionApplication:
			{
				IEdmApplyExpression edmApplyExpression = (IEdmApplyExpression)expression;
				IEdmFunction appliedFunction = edmApplyExpression.AppliedFunction;
				if (appliedFunction != null)
				{
					IList<IEdmExpression> list4 = edmApplyExpression.Arguments.ToList<IEdmExpression>();
					IEdmValue[] array3 = new IEdmValue[list4.Count<IEdmExpression>()];
					int num = 0;
					foreach (IEdmExpression edmExpression3 in list4)
					{
						array3[num++] = this.Eval(edmExpression3, context);
					}
					Func<IEdmValue[], IEdmValue> func;
					if (this.builtInFunctions.TryGetValue(appliedFunction, out func))
					{
						return func(array3);
					}
					if (this.lastChanceOperationApplier != null)
					{
						return this.lastChanceOperationApplier(appliedFunction.FullName(), array3);
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
				foreach (string text6 in edmPathExpression2.PathSegments)
				{
					edmValue4 = EdmExpressionEvaluator.FindProperty(text6, edmValue4);
					if (edmValue4 == null)
					{
						throw new InvalidOperationException(Strings.Edm_Evaluator_UnboundPath(text6));
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
				List<IEdmEnumMember> list5 = edmEnumMemberExpression.EnumMembers.ToList<IEdmEnumMember>();
				IEdmEnumType declaringType = list5.First<IEdmEnumMember>().DeclaringType;
				IEdmEnumTypeReference edmEnumTypeReference = new EdmEnumTypeReference(declaringType, false);
				if (list5.Count<IEdmEnumMember>() == 1)
				{
					return new EdmEnumValue(edmEnumTypeReference, edmEnumMemberExpression.EnumMembers.Single<IEdmEnumMember>());
				}
				if (!declaringType.IsFlags || !EdmEnumValueParser.IsEnumIntegerType(declaringType))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Type {0} cannot be assigned with multi-values.", new object[] { declaringType.FullName() }));
				}
				long num2 = 0L;
				foreach (IEdmEnumMember edmEnumMember in list5)
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

		// Token: 0x060006E5 RID: 1765 RVA: 0x00010E98 File Offset: 0x0000F098
		private IEdmDelayedValue MapLabeledExpressionToDelayedValue(IEdmExpression expression, EdmExpressionEvaluator.DelayedExpressionContext delayedContext, IEdmStructuredValue context)
		{
			IEdmLabeledExpression edmLabeledExpression = expression as IEdmLabeledExpression;
			if (edmLabeledExpression == null)
			{
				return new EdmExpressionEvaluator.DelayedCollectionElement(delayedContext, expression);
			}
			EdmExpressionEvaluator.DelayedValue delayedValue;
			if (this.labeledValues.TryGetValue(edmLabeledExpression, out delayedValue))
			{
				return delayedValue;
			}
			delayedValue = new EdmExpressionEvaluator.DelayedCollectionElement(delayedContext ?? new EdmExpressionEvaluator.DelayedExpressionContext(this, context), edmLabeledExpression.Expression);
			this.labeledValues[edmLabeledExpression] = delayedValue;
			return delayedValue;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00010EF0 File Offset: 0x0000F0F0
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

		// Token: 0x040002E9 RID: 745
		private readonly IDictionary<IEdmOperation, Func<IEdmValue[], IEdmValue>> builtInFunctions;

		// Token: 0x040002EA RID: 746
		private readonly Dictionary<IEdmLabeledExpression, EdmExpressionEvaluator.DelayedValue> labeledValues = new Dictionary<IEdmLabeledExpression, EdmExpressionEvaluator.DelayedValue>();

		// Token: 0x040002EB RID: 747
		private readonly Func<string, IEdmValue[], IEdmValue> lastChanceOperationApplier;

		// Token: 0x040002EC RID: 748
		private readonly Func<IEdmModel, IEdmType, string, string, IEdmExpression> getAnnotationExpressionForType;

		// Token: 0x040002ED RID: 749
		private readonly Func<IEdmModel, IEdmType, string, string, string, IEdmExpression> getAnnotationExpressionForProperty;

		// Token: 0x040002EE RID: 750
		private readonly IEdmModel edmModel;

		// Token: 0x040002EF RID: 751
		private Func<string, IEdmModel, IEdmType> resolveTypeFromName = (string typeName, IEdmModel edmModel) => EdmExpressionEvaluator.FindEdmType(typeName, edmModel);

		// Token: 0x02000241 RID: 577
		private class DelayedExpressionContext
		{
			// Token: 0x06000EE9 RID: 3817 RVA: 0x00027FBF File Offset: 0x000261BF
			public DelayedExpressionContext(EdmExpressionEvaluator expressionEvaluator, IEdmStructuredValue context)
			{
				this.expressionEvaluator = expressionEvaluator;
				this.context = context;
			}

			// Token: 0x06000EEA RID: 3818 RVA: 0x00027FD5 File Offset: 0x000261D5
			public IEdmValue Eval(IEdmExpression expression)
			{
				return this.expressionEvaluator.Eval(expression, this.context);
			}

			// Token: 0x04000843 RID: 2115
			private readonly EdmExpressionEvaluator expressionEvaluator;

			// Token: 0x04000844 RID: 2116
			private readonly IEdmStructuredValue context;
		}

		// Token: 0x02000242 RID: 578
		private abstract class DelayedValue : IEdmDelayedValue
		{
			// Token: 0x06000EEB RID: 3819 RVA: 0x00027FE9 File Offset: 0x000261E9
			public DelayedValue(EdmExpressionEvaluator.DelayedExpressionContext context)
			{
				this.context = context;
			}

			// Token: 0x170004DD RID: 1245
			// (get) Token: 0x06000EEC RID: 3820
			public abstract IEdmExpression Expression { get; }

			// Token: 0x170004DE RID: 1246
			// (get) Token: 0x06000EED RID: 3821 RVA: 0x00027FF8 File Offset: 0x000261F8
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

			// Token: 0x04000845 RID: 2117
			private readonly EdmExpressionEvaluator.DelayedExpressionContext context;

			// Token: 0x04000846 RID: 2118
			private IEdmValue value;
		}

		// Token: 0x02000243 RID: 579
		private class DelayedRecordProperty : EdmExpressionEvaluator.DelayedValue, IEdmPropertyValue, IEdmDelayedValue
		{
			// Token: 0x06000EEE RID: 3822 RVA: 0x0002801F File Offset: 0x0002621F
			public DelayedRecordProperty(EdmExpressionEvaluator.DelayedExpressionContext context, IEdmPropertyConstructor constructor)
				: base(context)
			{
				this.constructor = constructor;
			}

			// Token: 0x170004DF RID: 1247
			// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0002802F File Offset: 0x0002622F
			public string Name
			{
				get
				{
					return this.constructor.Name;
				}
			}

			// Token: 0x170004E0 RID: 1248
			// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0002803C File Offset: 0x0002623C
			public override IEdmExpression Expression
			{
				get
				{
					return this.constructor.Value;
				}
			}

			// Token: 0x04000847 RID: 2119
			private readonly IEdmPropertyConstructor constructor;
		}

		// Token: 0x02000244 RID: 580
		private class DelayedCollectionElement : EdmExpressionEvaluator.DelayedValue
		{
			// Token: 0x06000EF1 RID: 3825 RVA: 0x00028049 File Offset: 0x00026249
			public DelayedCollectionElement(EdmExpressionEvaluator.DelayedExpressionContext context, IEdmExpression expression)
				: base(context)
			{
				this.expression = expression;
			}

			// Token: 0x170004E1 RID: 1249
			// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00028059 File Offset: 0x00026259
			public override IEdmExpression Expression
			{
				get
				{
					return this.expression;
				}
			}

			// Token: 0x04000848 RID: 2120
			private readonly IEdmExpression expression;
		}

		// Token: 0x02000245 RID: 581
		private class CastCollectionValue : EdmElement, IEdmCollectionValue, IEdmValue, IEdmElement, IEnumerable<IEdmDelayedValue>, IEnumerable
		{
			// Token: 0x06000EF3 RID: 3827 RVA: 0x00028061 File Offset: 0x00026261
			public CastCollectionValue(IEdmCollectionTypeReference targetCollectionType, IEdmCollectionValue collectionValue)
			{
				this.targetCollectionType = targetCollectionType;
				this.collectionValue = collectionValue;
			}

			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0001250B File Offset: 0x0001070B
			IEnumerable<IEdmDelayedValue> IEdmCollectionValue.Elements
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00028077 File Offset: 0x00026277
			IEdmTypeReference IEdmValue.Type
			{
				get
				{
					return this.targetCollectionType;
				}
			}

			// Token: 0x170004E4 RID: 1252
			// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0000268B File Offset: 0x0000088B
			EdmValueKind IEdmValue.ValueKind
			{
				get
				{
					return EdmValueKind.Collection;
				}
			}

			// Token: 0x06000EF7 RID: 3831 RVA: 0x0002807F File Offset: 0x0002627F
			IEnumerator<IEdmDelayedValue> IEnumerable<IEdmDelayedValue>.GetEnumerator()
			{
				return new EdmExpressionEvaluator.CastCollectionValue.CastCollectionValueEnumerator(this);
			}

			// Token: 0x06000EF8 RID: 3832 RVA: 0x0002807F File Offset: 0x0002627F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new EdmExpressionEvaluator.CastCollectionValue.CastCollectionValueEnumerator(this);
			}

			// Token: 0x04000849 RID: 2121
			private readonly IEdmCollectionTypeReference targetCollectionType;

			// Token: 0x0400084A RID: 2122
			private readonly IEdmCollectionValue collectionValue;

			// Token: 0x02000306 RID: 774
			private class CastCollectionValueEnumerator : IEnumerator<IEdmDelayedValue>, IEnumerator, IDisposable
			{
				// Token: 0x060011B5 RID: 4533 RVA: 0x0002EA1D File Offset: 0x0002CC1D
				public CastCollectionValueEnumerator(EdmExpressionEvaluator.CastCollectionValue value)
				{
					this.value = value;
					this.enumerator = value.collectionValue.Elements.GetEnumerator();
				}

				// Token: 0x170004FA RID: 1274
				// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0002EA42 File Offset: 0x0002CC42
				public IEdmDelayedValue Current
				{
					get
					{
						return new EdmExpressionEvaluator.CastCollectionValue.CastCollectionValueEnumerator.DelayedCast(this.value.targetCollectionType.ElementType(), this.enumerator.Current);
					}
				}

				// Token: 0x170004FB RID: 1275
				// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0002EA64 File Offset: 0x0002CC64
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x060011B8 RID: 4536 RVA: 0x0002EA6C File Offset: 0x0002CC6C
				bool IEnumerator.MoveNext()
				{
					return this.enumerator.MoveNext();
				}

				// Token: 0x060011B9 RID: 4537 RVA: 0x0002EA79 File Offset: 0x0002CC79
				void IEnumerator.Reset()
				{
					this.enumerator.Reset();
				}

				// Token: 0x060011BA RID: 4538 RVA: 0x0002EA86 File Offset: 0x0002CC86
				void IDisposable.Dispose()
				{
					this.enumerator.Dispose();
				}

				// Token: 0x04000916 RID: 2326
				private readonly EdmExpressionEvaluator.CastCollectionValue value;

				// Token: 0x04000917 RID: 2327
				private readonly IEnumerator<IEdmDelayedValue> enumerator;

				// Token: 0x02000308 RID: 776
				private class DelayedCast : IEdmDelayedValue
				{
					// Token: 0x060011BD RID: 4541 RVA: 0x0002EA93 File Offset: 0x0002CC93
					public DelayedCast(IEdmTypeReference targetType, IEdmDelayedValue value)
					{
						this.delayedValue = value;
						this.targetType = targetType;
					}

					// Token: 0x170004FD RID: 1277
					// (get) Token: 0x060011BE RID: 4542 RVA: 0x0002EAA9 File Offset: 0x0002CCA9
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

					// Token: 0x04000918 RID: 2328
					private readonly IEdmDelayedValue delayedValue;

					// Token: 0x04000919 RID: 2329
					private readonly IEdmTypeReference targetType;

					// Token: 0x0400091A RID: 2330
					private IEdmValue value;
				}
			}
		}
	}
}
