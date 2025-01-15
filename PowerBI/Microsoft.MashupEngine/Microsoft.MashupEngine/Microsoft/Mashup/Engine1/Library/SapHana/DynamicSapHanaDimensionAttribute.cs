using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000416 RID: 1046
	internal sealed class DynamicSapHanaDimensionAttribute : SapHanaDimensionAttribute
	{
		// Token: 0x060023BC RID: 9148 RVA: 0x00064E97 File Offset: 0x00063097
		public DynamicSapHanaDimensionAttribute(SapHanaDimension dimension, string name, string caption, CubeExpression expression, TypeValue typeValue)
			: base(dimension, name, caption)
		{
			this.expression = expression;
			this.typeValue = typeValue;
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x00064EB2 File Offset: 0x000630B2
		public CubeExpression Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x00064EBA File Offset: 0x000630BA
		public TypeValue TypeValue
		{
			get
			{
				return this.typeValue;
			}
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x00064EC4 File Offset: 0x000630C4
		public static bool TryNew(SapHanaCubeBase cube, SapHanaDimension dimension, string name, string caption, CubeExpression expression, TypeValue typeValue, out DynamicSapHanaDimensionAttribute dynamicSapHanaDimensionAttribute)
		{
			try
			{
				new DynamicSapHanaDimensionAttribute.Visitor(cube).GetQueryExpression(expression);
			}
			catch (NotSupportedException)
			{
				dynamicSapHanaDimensionAttribute = null;
				return false;
			}
			dynamicSapHanaDimensionAttribute = new DynamicSapHanaDimensionAttribute(dimension, name, caption, expression, typeValue);
			return true;
		}

		// Token: 0x04000E5A RID: 3674
		private readonly CubeExpression expression;

		// Token: 0x04000E5B RID: 3675
		private readonly TypeValue typeValue;

		// Token: 0x02000417 RID: 1047
		private class Visitor : CubeExpressionVisitor<QueryExpression, object>
		{
			// Token: 0x060023C0 RID: 9152 RVA: 0x00064F0C File Offset: 0x0006310C
			public Visitor(SapHanaCubeBase cube)
			{
				this.cube = cube;
			}

			// Token: 0x060023C1 RID: 9153 RVA: 0x00064F1B File Offset: 0x0006311B
			public QueryExpression GetQueryExpression(CubeExpression expression)
			{
				return this.Visit(expression);
			}

			// Token: 0x060023C2 RID: 9154 RVA: 0x00064F24 File Offset: 0x00063124
			protected override QueryExpression NewBinary(BinaryOperator2 op, QueryExpression left, QueryExpression right)
			{
				return new BinaryQueryExpression(op, left, right);
			}

			// Token: 0x060023C3 RID: 9155 RVA: 0x00064F2E File Offset: 0x0006312E
			protected override QueryExpression NewConstant(Value constant)
			{
				return new ConstantQueryExpression(constant);
			}

			// Token: 0x060023C4 RID: 9156 RVA: 0x00064F38 File Offset: 0x00063138
			protected override QueryExpression NewIdentifier(IdentifierCubeExpression identifier)
			{
				ICubeObject cubeObject;
				if (this.cube.TryGetObject(identifier, out cubeObject) && cubeObject.Kind == CubeObjectKind.Measure)
				{
					throw new NotSupportedException();
				}
				return new ColumnAccessQueryExpression(0);
			}

			// Token: 0x060023C5 RID: 9157 RVA: 0x00064F6A File Offset: 0x0006316A
			protected override QueryExpression NewIf(QueryExpression condition, QueryExpression trueCase, QueryExpression falseCase)
			{
				return new IfQueryExpression(condition, trueCase, falseCase);
			}

			// Token: 0x060023C6 RID: 9158 RVA: 0x00064F74 File Offset: 0x00063174
			protected override QueryExpression NewInvocation(QueryExpression function, QueryExpression[] arguments)
			{
				return new InvocationQueryExpression(function, arguments);
			}

			// Token: 0x060023C7 RID: 9159 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override QueryExpression NewQuery(QueryExpression from, IList<IdentifierCubeExpression> dimensionAttributes, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> measureProperties, QueryExpression filter, object[] sortOrders, RowRange rowRange)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060023C8 RID: 9160 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override object NewSortOrder(QueryExpression expression, bool ascending)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04000E5C RID: 3676
			private readonly SapHanaCubeBase cube;
		}
	}
}
