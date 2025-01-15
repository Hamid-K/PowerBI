using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D68 RID: 3432
	internal abstract class SetCompiler
	{
		// Token: 0x06005D2C RID: 23852 RVA: 0x0014286F File Offset: 0x00140A6F
		public SetCompiler(ICube cube)
		{
			this.cube = cube;
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x0014287E File Offset: 0x00140A7E
		public bool TryCompile(QueryCubeExpression expression, out Set set)
		{
			return this.TryCompileCubeQuery(expression, out set);
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x00142888 File Offset: 0x00140A88
		public bool TryCompileScalarExpression(Dimensionality measureDimensionality, CubeExpression expression, out Set set)
		{
			return this.TryCompileSet(measureDimensionality, expression, out set);
		}

		// Token: 0x06005D2F RID: 23855
		protected abstract Set NewSelect(Dimensionality visibleDimensionality);

		// Token: 0x06005D30 RID: 23856 RVA: 0x00142894 File Offset: 0x00140A94
		protected virtual bool TryCompileCubeQuery(QueryCubeExpression expression, out Set set)
		{
			Set set2;
			if (!this.TryCompileFrom(expression.From, out set2))
			{
				set = null;
				return false;
			}
			Dictionary<ICubeHierarchy, CubeLevelRange> dictionary = new Dictionary<ICubeHierarchy, CubeLevelRange>();
			foreach (IdentifierCubeExpression identifierCubeExpression in expression.DimensionAttributes)
			{
				ICubeLevel cubeLevel = (ICubeLevel)this.GetObject(identifierCubeExpression);
				CubeLevelRange cubeLevelRange = new CubeLevelRange(cubeLevel, cubeLevel);
				CubeLevelRange cubeLevelRange2;
				if (dictionary.TryGetValue(cubeLevel.Hierarchy, out cubeLevelRange2))
				{
					cubeLevelRange2 = cubeLevelRange2.Union(cubeLevelRange);
				}
				else
				{
					cubeLevelRange2 = cubeLevelRange;
				}
				dictionary[cubeLevel.Hierarchy] = cubeLevelRange2;
			}
			Dimensionality dimensionality = new Dimensionality(dictionary.Values);
			if (set2.Dimensionality.Equals(dimensionality))
			{
				set = set2;
			}
			else
			{
				set = this.NewSelect(dimensionality);
				foreach (Set set3 in set2.GetSubsets())
				{
					set = set.Intersect(set3);
				}
			}
			if (expression.Filter != null)
			{
				Set set4;
				if (!this.TryCompileSet(dimensionality, expression.Filter, out set4))
				{
					set = null;
					return false;
				}
				set = set.Intersect(set4);
			}
			set = set.Project(this.GetObjects(expression.Properties));
			set = set.Project(this.GetObjects(expression.Measures));
			set = set.Project(this.GetObjects(expression.MeasureProperties));
			set = set.OrderBy(expression.Sort);
			set = set.SkipTake(expression.RowRange);
			return true;
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x00142A38 File Offset: 0x00140C38
		private bool TryCompileCubeIdentifier(IdentifierCubeExpression expression, out Set set)
		{
			if (expression.Equals(this.cube.Identifier))
			{
				set = this.NewSelect(Dimensionality.Empty);
				return true;
			}
			set = null;
			return false;
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x00142A60 File Offset: 0x00140C60
		private bool TryCompileFrom(CubeExpression expression, out Set set)
		{
			CubeExpressionKind kind = expression.Kind;
			if (kind == CubeExpressionKind.Identifier)
			{
				return this.TryCompileCubeIdentifier((IdentifierCubeExpression)expression, out set);
			}
			if (kind != CubeExpressionKind.Query)
			{
				set = null;
				return false;
			}
			return this.TryCompileCubeQuery((QueryCubeExpression)expression, out set);
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x00142AA0 File Offset: 0x00140CA0
		private bool TryCompileSet(Dimensionality measureDimensionality, CubeExpression expression, out Set set)
		{
			if (expression.Kind == CubeExpressionKind.Binary && this.TryCompileBinarySet(measureDimensionality, (BinaryCubeExpression)expression, out set))
			{
				return true;
			}
			bool flag;
			Dimensionality dimensionality = new SetCompiler.DimensionalityVisitor(this).GetDimensionality(measureDimensionality, expression, out flag);
			set = FilterSet.New(EverythingSet.Instance.ExpandTo(dimensionality), dimensionality, expression, flag);
			return true;
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x00142AF0 File Offset: 0x00140CF0
		private bool TryCompileBinarySet(Dimensionality measureDimensionality, BinaryCubeExpression binary, out Set set)
		{
			BinaryOperator2 @operator = binary.Operator;
			if (@operator - BinaryOperator2.Equals <= 1)
			{
				return this.TryCompileUniqueIdSet(binary.Operator, binary.Left, binary.Right, out set) || this.TryCompileUniqueIdSet(binary.Operator, binary.Right, binary.Left, out set);
			}
			if (@operator - BinaryOperator2.And > 1)
			{
				set = null;
				return false;
			}
			return this.TryCompileLogicalSet(measureDimensionality, binary.Operator, binary.Left, binary.Right, out set);
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x00142B6C File Offset: 0x00140D6C
		private bool TryCompileLogicalSet(Dimensionality measureDimensionality, BinaryOperator2 op, CubeExpression left, CubeExpression right, out Set set)
		{
			Set set2;
			Set set3;
			if (this.TryCompileSet(measureDimensionality, left, out set2) && this.TryCompileSet(measureDimensionality, right, out set3))
			{
				if (op == BinaryOperator2.And)
				{
					set = set2.Intersect(set3);
					return true;
				}
				if (op == BinaryOperator2.Or)
				{
					set = set2.Union(set3);
					return true;
				}
			}
			set = null;
			return false;
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x00142BBC File Offset: 0x00140DBC
		private bool TryCompileUniqueIdSet(BinaryOperator2 op, CubeExpression left, CubeExpression right, out Set set)
		{
			ICubeObject cubeObject;
			if (left.Kind == CubeExpressionKind.Identifier && right.Kind == CubeExpressionKind.Constant && (op == BinaryOperator2.Equals || op == BinaryOperator2.NotEquals) && this.cube.TryGetObject((IdentifierCubeExpression)left, out cubeObject) && cubeObject.Kind == CubeObjectKind.Property)
			{
				ICubeProperty cubeProperty = (ICubeProperty)cubeObject;
				Value value = ((ConstantCubeExpression)right).Value;
				if (cubeProperty.PropertyKind == CubePropertyKind.UniqueId && !value.IsNull)
				{
					set = new MemberSet(cubeProperty.Level, value);
					if (op == BinaryOperator2.NotEquals)
					{
						set = new LevelSet(cubeProperty.Level).Except(set);
					}
					return true;
				}
			}
			set = null;
			return false;
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x00142C58 File Offset: 0x00140E58
		private ICubeObject GetObject(IdentifierCubeExpression identifier)
		{
			ICubeObject cubeObject;
			if (this.cube.TryGetObject(identifier, out cubeObject))
			{
				return cubeObject;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x00142C7C File Offset: 0x00140E7C
		private IEnumerable<ICubeObject> GetObjects(IEnumerable<IdentifierCubeExpression> identifiers)
		{
			return identifiers.Select((IdentifierCubeExpression i) => this.GetObject(i));
		}

		// Token: 0x0400335E RID: 13150
		private readonly ICube cube;

		// Token: 0x02000D69 RID: 3433
		private class DimensionalityVisitor : CubeExpressionVisitor
		{
			// Token: 0x06005D3A RID: 23866 RVA: 0x00142C99 File Offset: 0x00140E99
			public DimensionalityVisitor(SetCompiler compiler)
			{
				this.compiler = compiler;
			}

			// Token: 0x06005D3B RID: 23867 RVA: 0x00142CA8 File Offset: 0x00140EA8
			public Dimensionality GetDimensionality(Dimensionality measureDimensionality, CubeExpression expression, out bool hasMeasureFilter)
			{
				this.measureDimensionality = measureDimensionality;
				this.currentDimensionality = Dimensionality.Empty;
				this.hasMeasureFilter = false;
				this.Visit(expression);
				hasMeasureFilter = this.hasMeasureFilter;
				return this.currentDimensionality;
			}

			// Token: 0x06005D3C RID: 23868 RVA: 0x00142CDC File Offset: 0x00140EDC
			protected override CubeExpression VisitIdentifier(IdentifierCubeExpression identifier)
			{
				ICubeObject @object = this.compiler.GetObject(identifier);
				switch (@object.Kind)
				{
				case CubeObjectKind.DimensionAttribute:
				{
					ICubeLevel cubeLevel = (ICubeLevel)@object;
					this.currentDimensionality = this.currentDimensionality.Union(new Dimensionality(new CubeLevelRange[]
					{
						new CubeLevelRange(cubeLevel, cubeLevel)
					}));
					break;
				}
				case CubeObjectKind.Property:
				{
					ICubeProperty cubeProperty = (ICubeProperty)@object;
					this.currentDimensionality = this.currentDimensionality.Union(new Dimensionality(new CubeLevelRange[]
					{
						new CubeLevelRange(cubeProperty.Level, cubeProperty.Level)
					}));
					break;
				}
				case CubeObjectKind.Measure:
					this.currentDimensionality = this.currentDimensionality.Union(this.measureDimensionality);
					this.hasMeasureFilter = true;
					break;
				default:
					throw new InvalidOperationException();
				}
				return identifier;
			}

			// Token: 0x0400335F RID: 13151
			private readonly SetCompiler compiler;

			// Token: 0x04003360 RID: 13152
			private Dimensionality measureDimensionality;

			// Token: 0x04003361 RID: 13153
			private Dimensionality currentDimensionality;

			// Token: 0x04003362 RID: 13154
			private bool hasMeasureFilter;
		}
	}
}
