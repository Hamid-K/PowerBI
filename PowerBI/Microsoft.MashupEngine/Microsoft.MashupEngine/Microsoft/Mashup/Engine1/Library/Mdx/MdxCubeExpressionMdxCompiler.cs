using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x0200096F RID: 2415
	internal abstract class MdxCubeExpressionMdxCompiler
	{
		// Token: 0x060044DC RID: 17628 RVA: 0x000E7846 File Offset: 0x000E5A46
		protected MdxCubeExpressionMdxCompiler(MdxCube cube)
		{
			this.cube = cube;
		}

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x000E7855 File Offset: 0x000E5A55
		protected string MeasuresDimensionName
		{
			get
			{
				return this.cube.MeasuresDimensionName;
			}
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x060044DE RID: 17630 RVA: 0x000E7862 File Offset: 0x000E5A62
		protected IdentifierMdxExpression MeasureOfOne
		{
			get
			{
				return new IdentifierMdxExpression(this.cube.MeasureOfOneMeasure.MdxIdentifier);
			}
		}

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x060044DF RID: 17631 RVA: 0x000E7879 File Offset: 0x000E5A79
		protected MdxDeclaration[] MeasureOfOneDeclaration
		{
			get
			{
				if (this.measureOfOneDeclaration == null)
				{
					this.measureOfOneDeclaration = new MdxDeclaration[]
					{
						new MdxMemberDeclaration(this.MeasureOfOne, this.One, false)
					};
				}
				return this.measureOfOneDeclaration;
			}
		}

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x060044E0 RID: 17632 RVA: 0x000E78AA File Offset: 0x000E5AAA
		protected virtual MdxFunction MemberCaption
		{
			get
			{
				return MdxFunction.MemberCaption;
			}
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x000E78AE File Offset: 0x000E5AAE
		protected virtual MdxFunction InStr
		{
			get
			{
				return MdxFunction.InStr;
			}
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x000E78B2 File Offset: 0x000E5AB2
		protected virtual MdxFunction UniqueName
		{
			get
			{
				return MdxFunction.UniqueName;
			}
		}

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x060044E3 RID: 17635 RVA: 0x000E78B6 File Offset: 0x000E5AB6
		protected virtual ConstantMdxExpression One
		{
			get
			{
				return new ConstantMdxExpression(1);
			}
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x000E78C0 File Offset: 0x000E5AC0
		public bool CanCompile(QueryCubeExpression expression)
		{
			MdxExpression mdxExpression;
			return this.TryCompile(expression, RowRange.All, out mdxExpression);
		}

		// Token: 0x060044E5 RID: 17637 RVA: 0x000E78DC File Offset: 0x000E5ADC
		public MdxExpression Compile(QueryCubeExpression expression, RowRange rowRange)
		{
			MdxExpression mdxExpression;
			if (!this.TryCompile(expression, rowRange, out mdxExpression))
			{
				throw new InvalidOperationException();
			}
			return mdxExpression;
		}

		// Token: 0x060044E6 RID: 17638
		protected abstract bool TryCompile(QueryCubeExpression expression, RowRange rowRange, out MdxExpression mdx);

		// Token: 0x060044E7 RID: 17639 RVA: 0x000E78FC File Offset: 0x000E5AFC
		protected QueryCubeExpression ApplyRowRange(QueryCubeExpression expression, RowRange rowRange)
		{
			if (rowRange.IsAll)
			{
				return expression;
			}
			if (expression.Filter == null)
			{
				rowRange = expression.RowRange.Skip(rowRange.SkipCount).Take(rowRange.TakeCount);
				return new QueryCubeExpression(expression.From, expression.DimensionAttributes, expression.Properties, expression.Measures, expression.MeasureProperties, null, expression.Sort, rowRange);
			}
			return new QueryCubeExpression(expression, expression.DimensionAttributes, expression.Properties, expression.Measures, expression.MeasureProperties, null, EmptyArray<CubeSortOrder>.Instance, rowRange);
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x000E7994 File Offset: 0x000E5B94
		protected QueryCubeExpression LiftOrderingsUpwards(QueryCubeExpression query)
		{
			List<CubeSortOrder> list = new List<CubeSortOrder>();
			list.AddRange(query.Sort);
			return new QueryCubeExpression(this.RemoveOrdering(query.From, list), query.DimensionAttributes, query.Properties, query.Measures, query.MeasureProperties, query.Filter, list, query.RowRange);
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x000E79EC File Offset: 0x000E5BEC
		protected CubeExpression RemoveOrdering(CubeExpression expression, List<CubeSortOrder> sortOrders)
		{
			if (expression.Kind == CubeExpressionKind.Query)
			{
				QueryCubeExpression queryCubeExpression = (QueryCubeExpression)expression;
				sortOrders.AddRange(queryCubeExpression.Sort);
				CubeExpression cubeExpression = this.RemoveOrdering(queryCubeExpression.From, sortOrders);
				if (queryCubeExpression.Filter == null && queryCubeExpression.RowRange.IsAll)
				{
					expression = cubeExpression;
				}
				else
				{
					expression = new QueryCubeExpression(cubeExpression, queryCubeExpression.DimensionAttributes, queryCubeExpression.Properties, queryCubeExpression.Measures, queryCubeExpression.MeasureProperties, queryCubeExpression.Filter, EmptyArray<CubeSortOrder>.Instance, queryCubeExpression.RowRange);
				}
			}
			return expression;
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x000E7A74 File Offset: 0x000E5C74
		protected bool TryCompileOrdering(MdxExpression set, IList<CubeSortOrder> sortOrders, out MdxExpression orderedSet, out IEnumerable<MdxDeclaration> declarations)
		{
			this.inOrderByExpression = true;
			bool flag;
			try
			{
				orderedSet = set;
				for (int i = sortOrders.Count - 1; i >= 0; i--)
				{
					CubeSortOrder cubeSortOrder = sortOrders[i];
					TypeValue typeValue;
					MdxExpression mdxExpression;
					if (!this.TryCompileFilterExpression(cubeSortOrder.Expression, out typeValue, out mdxExpression))
					{
						orderedSet = null;
						declarations = null;
						return false;
					}
					ConstantMdxExpression constantMdxExpression = (cubeSortOrder.Ascending ? ConstantMdxExpression.Basc : ConstantMdxExpression.Bdesc);
					orderedSet = new InvocationMdxExpression(MdxFunction.Order, new MdxExpression[] { orderedSet, mdxExpression, constantMdxExpression });
				}
				IEnumerable<MdxDeclaration> enumerable = this.orderByDeclarations;
				declarations = enumerable ?? EmptyArray<MdxDeclaration>.Instance;
				flag = true;
			}
			finally
			{
				this.inOrderByExpression = false;
			}
			return flag;
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x000E7B2C File Offset: 0x000E5D2C
		protected bool TryCompileSkipTake(RowRange rowRange, MdxExpression set, out MdxExpression subset)
		{
			if (rowRange.TakeCount.IsInfinite && rowRange.SkipCount.IsZero)
			{
				subset = set;
			}
			else
			{
				List<MdxExpression> list = new List<MdxExpression>();
				list.Add(set);
				MdxExpression mdxExpression;
				if (!MdxCubeExpressionMdxCompiler.TryCompileNumericExpression(rowRange.SkipCount.Value, out mdxExpression))
				{
					subset = null;
					return false;
				}
				list.Add(mdxExpression);
				if (!rowRange.TakeCount.IsInfinite)
				{
					if (!MdxCubeExpressionMdxCompiler.TryCompileNumericExpression(rowRange.TakeCount.Value, out mdxExpression))
					{
						subset = null;
						return false;
					}
					list.Add(mdxExpression);
				}
				subset = new InvocationMdxExpression(MdxFunction.Subset, list.ToArray());
			}
			return true;
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x000E7BD5 File Offset: 0x000E5DD5
		private static bool TryCompileNumericExpression(long number, out MdxExpression mdx)
		{
			if (number > 2147483647L || number < -2147483648L)
			{
				mdx = null;
				return false;
			}
			mdx = new ConstantMdxExpression((int)number);
			return true;
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x000E7BF8 File Offset: 0x000E5DF8
		protected bool TryCompileFilterExpression(CubeExpression expression, out MdxExpression mdx)
		{
			TypeValue typeValue;
			return this.TryCompileFilterExpression(expression, out typeValue, out mdx) && typeValue.Equals(TypeValue.Logical);
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x000E7C20 File Offset: 0x000E5E20
		private bool TryCompileFilterExpression(CubeExpression expression, out TypeValue type, out MdxExpression mdx)
		{
			switch (expression.Kind)
			{
			case CubeExpressionKind.Constant:
			{
				ConstantCubeExpression constantCubeExpression = (ConstantCubeExpression)expression;
				type = constantCubeExpression.Value.Type;
				return this.TryCompileConstant((ConstantCubeExpression)expression, out mdx);
			}
			case CubeExpressionKind.Identifier:
				return this.TryCompileObjectValue((IdentifierCubeExpression)expression, out type, out mdx);
			case CubeExpressionKind.Binary:
				type = TypeValue.Logical;
				return this.TryCompileFilterBinary((BinaryCubeExpression)expression, out mdx);
			case CubeExpressionKind.Invocation:
				return this.TryCompileFilterInvocation((InvocationCubeExpression)expression, out type, out mdx);
			case CubeExpressionKind.If:
				return this.TryCompileFilterIf((IfCubeExpression)expression, out type, out mdx);
			}
			type = null;
			mdx = null;
			return false;
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x000E7CC0 File Offset: 0x000E5EC0
		private bool TryCompileObjectValue(IdentifierCubeExpression identifier, out TypeValue typeValue, out MdxExpression mdx)
		{
			MdxCubeObject @object = this.cube.GetObject(identifier.Identifier);
			switch (@object.Kind)
			{
			case MdxCubeObjectKind.Measure:
			{
				MdxMeasure mdxMeasure = (MdxMeasure)@object;
				mdx = new IdentifierMdxExpression(mdxMeasure.MdxIdentifier);
				typeValue = mdxMeasure.Type.GetTypeValue();
				return true;
			}
			case MdxCubeObjectKind.Level:
			{
				MdxLevel mdxLevel = (MdxLevel)@object;
				mdx = this.CompileLevelMember(mdxLevel);
				mdx = new InvocationMdxExpression(this.MemberCaption, new MdxExpression[] { mdx });
				typeValue = TypeValue.Text;
				if (this.inOrderByExpression && this.orderByDeclarations != null)
				{
					IdentifierMdxExpression identifierMdxExpression = new IdentifierMdxExpression("[Measures].[Microsoft.Mashup.Engine.order." + this.orderByDeclarations.Count.ToString() + "]");
					this.orderByDeclarations.Add(new MdxMemberDeclaration(identifierMdxExpression, mdx, false));
					mdx = identifierMdxExpression;
				}
				return true;
			}
			case MdxCubeObjectKind.Property:
			{
				MdxProperty mdxProperty = (MdxProperty)@object;
				mdx = this.CompileLevelMember(mdxProperty.Level);
				switch (mdxProperty.PropertyKind)
				{
				case MdxPropertyKind.MemberUniqueName:
					mdx = new InvocationMdxExpression(this.UniqueName, new MdxExpression[] { mdx });
					typeValue = TypeValue.Text;
					return true;
				case MdxPropertyKind.MemberCaption:
					mdx = new InvocationMdxExpression(this.MemberCaption, new MdxExpression[] { mdx });
					typeValue = TypeValue.Text;
					return true;
				case MdxPropertyKind.UserDefined:
					if (mdxProperty.Name == MdxIdentifier.QuotePart(InvocationMdxExpression.ToString(MdxFunction.MemberName)))
					{
						mdx = new InvocationMdxExpression(MdxFunction.MemberName, new MdxExpression[] { mdx });
					}
					else
					{
						mdx = this.cube.CompileLevelMemberUserDefined(identifier, mdx, mdxProperty);
					}
					typeValue = TypeValue.Text;
					return true;
				}
				break;
			}
			case MdxCubeObjectKind.CellProperty:
			{
				MdxCellProperty mdxCellProperty = (MdxCellProperty)@object;
				mdx = new IdentifierMdxExpression(mdxCellProperty.MdxIdentifier);
				typeValue = mdxCellProperty.Type.GetTypeValue();
				return true;
			}
			}
			mdx = null;
			typeValue = null;
			return false;
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x000E7EA0 File Offset: 0x000E60A0
		private MdxExpression CompileLevelMember(MdxLevel level)
		{
			InvocationMdxExpression invocationMdxExpression = new InvocationMdxExpression(MdxFunction.CurrentMember, new MdxExpression[]
			{
				new IdentifierMdxExpression(level.Hierarchy.MdxIdentifier)
			});
			if (level.Hierarchy.Type == MdxHierarchyType.Attribute)
			{
				return invocationMdxExpression;
			}
			return new InvocationMdxExpression(MdxFunction.Ancestor, new MdxExpression[]
			{
				invocationMdxExpression,
				new IdentifierMdxExpression(level.MdxIdentifier)
			});
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x000E7EFC File Offset: 0x000E60FC
		protected string[] CompileCellProperties(IList<IdentifierCubeExpression> identifiers)
		{
			HashSet<string> hashSet = new HashSet<string>(MdxCubeExpressionMdxCompiler.DefaultCellProperties);
			foreach (IdentifierCubeExpression identifierCubeExpression in identifiers)
			{
				MdxCellProperty mdxCellProperty = (MdxCellProperty)this.cube.GetObject(identifierCubeExpression.Identifier);
				hashSet.Add(mdxCellProperty.Name);
			}
			return hashSet.ToArray<string>();
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x000E7F74 File Offset: 0x000E6174
		protected virtual bool TryCompileConstant(ConstantCubeExpression constant, out MdxExpression mdx)
		{
			switch (constant.Value.Kind)
			{
			case ValueKind.Null:
				mdx = ConstantMdxExpression.Null;
				return true;
			case ValueKind.Date:
				mdx = this.CompileDateConstant(constant.Value.AsDate.AsClrDateTime);
				return true;
			case ValueKind.DateTime:
				mdx = this.CompileDateConstant(constant.Value.AsDateTime.AsClrDateTime);
				return true;
			case ValueKind.Number:
			{
				NumberValue asNumber = constant.Value.AsNumber;
				switch (asNumber.NumberKind)
				{
				case NumberKind.Int32:
					mdx = new ConstantMdxExpression(asNumber.AsInteger32);
					return true;
				case NumberKind.Double:
					mdx = new ConstantMdxExpression(asNumber.AsDouble);
					return true;
				case NumberKind.Decimal:
					mdx = new ConstantMdxExpression(asNumber.AsDecimal);
					return true;
				default:
					mdx = null;
					return false;
				}
				break;
			}
			case ValueKind.Logical:
				mdx = (constant.Value.AsBoolean ? ConstantMdxExpression.True : ConstantMdxExpression.False);
				return true;
			case ValueKind.Text:
				mdx = new ConstantMdxExpression(constant.Value.AsString);
				return true;
			}
			mdx = null;
			return false;
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x000E8088 File Offset: 0x000E6288
		protected virtual MdxExpression CompileDateConstant(DateTime dateTime)
		{
			ConstantMdxExpression constantMdxExpression = new ConstantMdxExpression(dateTime.ToString("MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture));
			return new InvocationMdxExpression(MdxFunction.VbaCDate, new MdxExpression[] { constantMdxExpression });
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x000E80BC File Offset: 0x000E62BC
		protected bool TryCompileFilterBinary(BinaryCubeExpression binary, out MdxExpression mdx)
		{
			if (binary.Operator == BinaryOperator2.And || binary.Operator == BinaryOperator2.Or)
			{
				return this.TryCompileLogicalBinaryFilter(binary, out mdx);
			}
			return this.TryCompileComparisonBinaryFilter(binary, out mdx);
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x000E80E4 File Offset: 0x000E62E4
		private bool TryCompileLogicalBinaryFilter(BinaryCubeExpression binary, out MdxExpression mdx)
		{
			TypeValue typeValue;
			MdxExpression mdxExpression;
			MdxExpression mdxExpression2;
			if (!this.TryCompileFilterExpression(binary.Left, out typeValue, out mdxExpression) || !typeValue.Equals(TypeValue.Logical) || !this.TryCompileFilterExpression(binary.Right, out typeValue, out mdxExpression2) || !typeValue.Equals(TypeValue.Logical))
			{
				mdx = null;
				return false;
			}
			mdx = new BinaryMdxExpression(binary.Operator, mdxExpression, mdxExpression2);
			return true;
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x000E8144 File Offset: 0x000E6344
		private bool TryCompileComparisonBinaryFilter(BinaryCubeExpression binary, out MdxExpression mdx)
		{
			TypeValue typeValue;
			MdxExpression mdxExpression;
			TypeValue typeValue2;
			MdxExpression mdxExpression2;
			if (!this.TryCompileFilterExpression(binary.Left, out typeValue, out mdxExpression) || !this.TryCompileFilterExpression(binary.Right, out typeValue2, out mdxExpression2))
			{
				mdx = null;
				return false;
			}
			switch (binary.Operator)
			{
			case BinaryOperator2.GreaterThan:
			case BinaryOperator2.LessThan:
			case BinaryOperator2.GreaterThanOrEquals:
			case BinaryOperator2.LessThanOrEquals:
				return this.TryCompileInequalityFilter(binary.Operator, mdxExpression, mdxExpression2, out mdx);
			case BinaryOperator2.Equals:
				return this.TryCompileValueEqualityExpression(mdxExpression, mdxExpression2, out mdx);
			case BinaryOperator2.NotEquals:
				if (!this.TryCompileValueEqualityExpression(mdxExpression, mdxExpression2, out mdx))
				{
					return false;
				}
				mdx = mdx.Not();
				return true;
			default:
				mdx = null;
				return false;
			}
		}

		// Token: 0x060044F7 RID: 17655
		protected abstract bool TryCompileValueEqualityExpression(MdxExpression left, MdxExpression right, out MdxExpression mdx);

		// Token: 0x060044F8 RID: 17656
		protected abstract bool TryCompileInequalityFilter(BinaryOperator2 inequalityOperator, MdxExpression left, MdxExpression right, out MdxExpression mdx);

		// Token: 0x060044F9 RID: 17657 RVA: 0x000E81DC File Offset: 0x000E63DC
		private bool TryCompileFilterIf(IfCubeExpression @if, out TypeValue typeValue, out MdxExpression mdx)
		{
			TypeValue typeValue2;
			MdxExpression mdxExpression;
			TypeValue typeValue3;
			MdxExpression mdxExpression2;
			TypeValue typeValue4;
			MdxExpression mdxExpression3;
			if (this.TryCompileFilterExpression(@if.Condition, out typeValue2, out mdxExpression) && this.TryCompileFilterExpression(@if.TrueCase, out typeValue3, out mdxExpression2) && this.TryCompileFilterExpression(@if.FalseCase, out typeValue4, out mdxExpression3))
			{
				if (ConstantMdxExpression.True.Equals(mdxExpression))
				{
					mdx = mdxExpression2;
					typeValue = typeValue3;
				}
				else if (ConstantMdxExpression.False.Equals(mdxExpression))
				{
					mdx = mdxExpression3;
					typeValue = typeValue4;
				}
				else
				{
					mdx = new InvocationMdxExpression(MdxFunction.IIf, new MdxExpression[] { mdxExpression, mdxExpression2, mdxExpression3 });
					typeValue = TypeAlgebra.Union(typeValue3, typeValue4);
				}
				return true;
			}
			typeValue = null;
			mdx = null;
			return false;
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x000E827C File Offset: 0x000E647C
		private bool TryCompileFilterInvocation(InvocationCubeExpression invocation, out TypeValue typeValue, out MdxExpression mdx)
		{
			ConstantCubeExpression constantCubeExpression = invocation.Function as ConstantCubeExpression;
			if (constantCubeExpression != null && constantCubeExpression.Value.IsFunction)
			{
				if (constantCubeExpression.Value.AsFunction.FunctionIdentity.Equals(Library.Text.Contains.FunctionIdentity))
				{
					return this.TryCompileFilterInStr(invocation, BinaryOperator2.GreaterThan, 0, out typeValue, out mdx);
				}
				if (constantCubeExpression.Value.AsFunction.FunctionIdentity.Equals(Library.Text.StartsWith.FunctionIdentity))
				{
					return this.TryCompileFilterInStr(invocation, BinaryOperator2.Equals, 1, out typeValue, out mdx);
				}
			}
			typeValue = null;
			mdx = null;
			return false;
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x000E8308 File Offset: 0x000E6508
		private bool TryCompileFilterInStr(InvocationCubeExpression invocation, BinaryOperator2 op, int position, out TypeValue typeValue, out MdxExpression mdx)
		{
			if (invocation.Arguments.Count == 2)
			{
				CubeExpression cubeExpression = invocation.Arguments[0];
				CubeExpression cubeExpression2 = invocation.Arguments[1];
				CubeExpression cubeExpression3 = new BinaryCubeExpression(BinaryOperator2.NotEquals, cubeExpression, ConstantCubeExpression.Null);
				CubeExpression cubeExpression4 = new BinaryCubeExpression(BinaryOperator2.NotEquals, cubeExpression2, ConstantCubeExpression.Null);
				TypeValue typeValue2;
				MdxExpression mdxExpression;
				TypeValue typeValue3;
				MdxExpression mdxExpression2;
				TypeValue typeValue4;
				MdxExpression mdxExpression3;
				TypeValue typeValue5;
				MdxExpression mdxExpression4;
				if (this.TryCompileFilterExpression(cubeExpression3, out typeValue2, out mdxExpression) && this.TryCompileFilterExpression(cubeExpression4, out typeValue3, out mdxExpression2) && this.TryCompileFilterExpression(cubeExpression, out typeValue4, out mdxExpression3) && this.TryCompileFilterExpression(cubeExpression2, out typeValue5, out mdxExpression4) && typeValue4.TypeKind == ValueKind.Text && typeValue5.TypeKind == ValueKind.Text)
				{
					typeValue = TypeValue.Logical;
					MdxExpression mdxExpression5 = new BinaryMdxExpression(op, new InvocationMdxExpression(this.InStr, new MdxExpression[] { mdxExpression3, mdxExpression4 }), new ConstantMdxExpression(position));
					mdx = mdxExpression.And(mdxExpression2).And(mdxExpression5);
					return true;
				}
			}
			typeValue = null;
			mdx = null;
			return false;
		}

		// Token: 0x040024AF RID: 9391
		protected static readonly string[] DefaultCellProperties = new string[] { "VALUE" };

		// Token: 0x040024B0 RID: 9392
		protected readonly MdxCube cube;

		// Token: 0x040024B1 RID: 9393
		protected bool inOrderByExpression;

		// Token: 0x040024B2 RID: 9394
		protected List<MdxDeclaration> orderByDeclarations;

		// Token: 0x040024B3 RID: 9395
		private MdxDeclaration[] measureOfOneDeclaration;
	}
}
