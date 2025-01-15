using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000970 RID: 2416
	internal class MdxCubeExpressionMdxCompiler1 : MdxCubeExpressionMdxCompiler
	{
		// Token: 0x060044FD RID: 17661 RVA: 0x000E840B File Offset: 0x000E660B
		public MdxCubeExpressionMdxCompiler1(MdxCube cube)
			: base(cube)
		{
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x000E8414 File Offset: 0x000E6614
		protected override bool TryCompile(QueryCubeExpression expression, RowRange rowRange, out MdxExpression mdx)
		{
			expression = expression.PushFiltersToSubSelect();
			expression = base.ApplyRowRange(expression, rowRange);
			expression = expression.PropagateMeasuresDownwards();
			expression = base.LiftOrderingsUpwards(expression);
			return this.TryCompileSelect(expression, out mdx);
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x000E8444 File Offset: 0x000E6644
		protected virtual bool TryCompileSelect(QueryCubeExpression expression, out MdxExpression mdx)
		{
			if (expression.Filter != null)
			{
				mdx = null;
				return false;
			}
			List<AxisMdxExpression> list = new List<AxisMdxExpression>();
			HashSet<IdentifierCubeExpression> hashSet = new HashSet<IdentifierCubeExpression>(expression.DimensionAttributes);
			MdxDeclaration[] array;
			MdxExpression mdxExpression;
			if (expression.Measures.Count == 0)
			{
				array = base.MeasureOfOneDeclaration;
				mdxExpression = null;
				list.Add(new AxisMdxExpression(base.MeasureOfOne, Array.Empty<string>()));
			}
			else
			{
				array = EmptyArray<MdxDeclaration>.Instance;
				mdxExpression = this.CompileMeasureSet(expression.Measures);
				list.Add(new AxisMdxExpression(mdxExpression, Array.Empty<string>()));
			}
			if (expression.DimensionAttributes.Count != 0)
			{
				AxisMdxExpression axisMdxExpression;
				IEnumerable<MdxDeclaration> enumerable;
				if (!this.TryCompileDimensionAxis(expression, mdxExpression, hashSet, out axisMdxExpression, out enumerable))
				{
					mdx = null;
					return false;
				}
				list.Add(axisMdxExpression);
				array = array.Concat(enumerable).ToArray<MdxDeclaration>();
			}
			MdxExpression mdxExpression2;
			if (!this.TryCompileSubSelect(expression.From, out mdxExpression2))
			{
				mdx = null;
				return false;
			}
			mdx = new SelectMdxExpression(array, list.ToArray(), mdxExpression2, null, base.CompileCellProperties(expression.MeasureProperties));
			return true;
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x000E852C File Offset: 0x000E672C
		protected virtual bool TryCompileDimensionAxis(QueryCubeExpression expression, MdxExpression nonDefaultMeasureSet, HashSet<IdentifierCubeExpression> dimensionAttributes, out AxisMdxExpression mdx, out IEnumerable<MdxDeclaration> declarations)
		{
			MdxExpression mdxExpression = this.CompileCrossJoin(dimensionAttributes);
			if (nonDefaultMeasureSet != null)
			{
				mdxExpression = new InvocationMdxExpression(MdxFunction.NonEmpty, new MdxExpression[] { mdxExpression, nonDefaultMeasureSet });
			}
			if (base.TryCompileOrdering(mdxExpression, expression.Sort, out mdxExpression, out declarations) && base.TryCompileSkipTake(expression.RowRange, mdxExpression, out mdxExpression))
			{
				mdx = new AxisMdxExpression(mdxExpression, new string[] { "MEMBER_CAPTION", "MEMBER_UNIQUE_NAME" });
				return true;
			}
			mdx = null;
			declarations = null;
			return false;
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x000E85A7 File Offset: 0x000E67A7
		protected bool TryCompileSubSelect(CubeExpression expression, out MdxExpression mdx)
		{
			if (expression.Kind == CubeExpressionKind.Identifier)
			{
				return this.TryCompileCubeIdentifier((IdentifierCubeExpression)expression, out mdx);
			}
			return this.TryCompileQuerySubSelect((QueryCubeExpression)expression, out mdx);
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x000E85D0 File Offset: 0x000E67D0
		private bool TryCompileQuerySubSelect(QueryCubeExpression expression, out MdxExpression mdx)
		{
			if (!expression.RowRange.IsAll)
			{
				mdx = null;
				return false;
			}
			HashSet<IdentifierCubeExpression> hashSet = new HashSet<IdentifierCubeExpression>(expression.DimensionAttributes);
			MdxExpression mdxExpression;
			if (expression.DimensionAttributes.Count == 0)
			{
				mdxExpression = this.CompileMeasureSet(expression.Measures);
			}
			else
			{
				mdxExpression = this.CompileCrossJoin(hashSet);
			}
			IList<MdxExpression> list;
			if (!this.TryCompileFilter(hashSet, expression.Filter, mdxExpression, out list) || !this.TryCompileSubSelect(expression.From, out mdx))
			{
				mdx = null;
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				AxisMdxExpression axisMdxExpression = new AxisMdxExpression(list[i], Array.Empty<string>());
				mdx = new SelectMdxExpression(EmptyArray<MdxDeclaration>.Instance, new AxisMdxExpression[] { axisMdxExpression }, mdx, null, null);
			}
			return true;
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x000E868D File Offset: 0x000E688D
		private bool TryCompileCubeIdentifier(IdentifierCubeExpression expression, out MdxExpression mdx)
		{
			if (expression.Identifier != this.cube.MdxIdentifier)
			{
				mdx = null;
				return false;
			}
			mdx = new IdentifierMdxExpression(this.cube.MdxIdentifier);
			return true;
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x000E86C0 File Offset: 0x000E68C0
		protected MdxExpression CompileMeasureSet(IList<IdentifierCubeExpression> identifiers)
		{
			MdxExpression[] array = new MdxExpression[identifiers.Count];
			for (int i = 0; i < identifiers.Count; i++)
			{
				IdentifierCubeExpression identifierCubeExpression = identifiers[i];
				MdxMeasure mdxMeasure = (MdxMeasure)this.cube.GetObject(identifierCubeExpression.Identifier);
				array[i] = new IdentifierMdxExpression(mdxMeasure.MdxIdentifier);
			}
			return new SetMdxExpression(array);
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x000E8720 File Offset: 0x000E6920
		private bool TryCompileFilter(HashSet<IdentifierCubeExpression> dimensionAttributes, CubeExpression filterExpression, MdxExpression subselectSet, out IList<MdxExpression> filteredSets)
		{
			ConstantCubeExpression constantCubeExpression = filterExpression as ConstantCubeExpression;
			if (constantCubeExpression != null && constantCubeExpression.Value.IsLogical && constantCubeExpression.Value.AsLogical.Boolean)
			{
				filteredSets = new List<MdxExpression>();
				return true;
			}
			IEnumerable<CubeExpression> conjunctiveNF = filterExpression.GetConjunctiveNF();
			MdxExpression mdxExpression = null;
			Dictionary<MdxHierarchy, List<MdxExpression>> dictionary = new Dictionary<MdxHierarchy, List<MdxExpression>>();
			foreach (CubeExpression cubeExpression in conjunctiveNF)
			{
				MdxLevel mdxLevel;
				MdxExpression mdxExpression2;
				if (this.TryCompileSetExpression(cubeExpression, out mdxLevel, out mdxExpression2))
				{
					List<MdxExpression> list;
					if (!dictionary.TryGetValue(mdxLevel.Hierarchy, out list))
					{
						list = new List<MdxExpression>();
						dictionary[mdxLevel.Hierarchy] = list;
					}
					list.Add(mdxExpression2);
				}
				else
				{
					MdxExpression mdxExpression3;
					if (!base.TryCompileFilterExpression(cubeExpression, out mdxExpression3))
					{
						filteredSets = null;
						return false;
					}
					if (mdxExpression == null)
					{
						mdxExpression = mdxExpression3;
					}
					else
					{
						mdxExpression = new BinaryMdxExpression(BinaryOperator2.And, mdxExpression, mdxExpression3);
					}
				}
			}
			List<MdxExpression> list2 = new List<MdxExpression>();
			if (mdxExpression != null)
			{
				mdxExpression = new InvocationMdxExpression(MdxFunction.Filter, new MdxExpression[] { subselectSet, mdxExpression });
				list2.Add(mdxExpression);
			}
			int num = 0;
			for (;;)
			{
				List<MdxExpression> list3 = new List<MdxExpression>();
				foreach (List<MdxExpression> list4 in dictionary.Values)
				{
					if (list4.Count > num)
					{
						list3.Add(list4[num]);
					}
				}
				if (list3.Count == 0)
				{
					break;
				}
				list2.Add(new TupleMdxExpression(list3.ToArray()));
				num++;
			}
			filteredSets = list2;
			return true;
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x000E88C8 File Offset: 0x000E6AC8
		protected MdxExpression CompileCrossJoin(HashSet<IdentifierCubeExpression> dimensionAttributes)
		{
			Dictionary<MdxHierarchy, MdxCubeExpressionMdxCompiler1.HierarchyBounds> dictionary = new Dictionary<MdxHierarchy, MdxCubeExpressionMdxCompiler1.HierarchyBounds>();
			HashSet<MdxLevel> hashSet = new HashSet<MdxLevel>();
			foreach (IdentifierCubeExpression identifierCubeExpression in dimensionAttributes)
			{
				MdxLevel mdxLevel = (MdxLevel)this.cube.GetObject(identifierCubeExpression.Identifier);
				MdxCubeExpressionMdxCompiler1.HierarchyBounds hierarchyBounds;
				if (mdxLevel.Hierarchy.Type == MdxHierarchyType.Attribute)
				{
					hashSet.Add(mdxLevel);
				}
				else if (dictionary.TryGetValue(mdxLevel.Hierarchy, out hierarchyBounds))
				{
					if (mdxLevel.Number < hierarchyBounds.Upper.Number)
					{
						hierarchyBounds.Upper = mdxLevel;
					}
					else if (mdxLevel.Number > hierarchyBounds.Lower.Number)
					{
						hierarchyBounds.Lower = mdxLevel;
					}
				}
				else
				{
					dictionary[mdxLevel.Hierarchy] = new MdxCubeExpressionMdxCompiler1.HierarchyBounds(mdxLevel);
				}
			}
			MdxExpression[] array = new MdxExpression[hashSet.Count + dictionary.Values.Count];
			int num = 0;
			foreach (MdxLevel mdxLevel2 in hashSet)
			{
				array[num++] = new InvocationMdxExpression(MdxFunction.AllMembers, new MdxExpression[]
				{
					new IdentifierMdxExpression(mdxLevel2.MdxIdentifier)
				});
			}
			foreach (MdxCubeExpressionMdxCompiler1.HierarchyBounds hierarchyBounds2 in dictionary.Values)
			{
				array[num++] = new InvocationMdxExpression(MdxFunction.AddCalculatedMembers, new MdxExpression[]
				{
					new InvocationMdxExpression(MdxFunction.Descendants, new MdxExpression[]
					{
						new InvocationMdxExpression(MdxFunction.AllMembers, new MdxExpression[]
						{
							new IdentifierMdxExpression(hierarchyBounds2.Upper.MdxIdentifier)
						}),
						new IdentifierMdxExpression(hierarchyBounds2.Lower.MdxIdentifier),
						ConstantMdxExpression.Leaves
					})
				});
			}
			MdxExpression mdxExpression = array[0];
			for (int i = 1; i < array.Length; i++)
			{
				mdxExpression = new InvocationMdxExpression(MdxFunction.Crossjoin, new MdxExpression[]
				{
					mdxExpression,
					array[i]
				});
			}
			return mdxExpression;
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x000E8B04 File Offset: 0x000E6D04
		protected override bool TryCompileValueEqualityExpression(MdxExpression left, MdxExpression right, out MdxExpression mdx)
		{
			MdxExpression mdxExpression = new InvocationMdxExpression(MdxFunction.IsEmpty, new MdxExpression[] { left });
			MdxExpression mdxExpression2 = new InvocationMdxExpression(MdxFunction.IsEmpty, new MdxExpression[] { right });
			mdx = new BinaryMdxExpression(BinaryOperator2.Or, new BinaryMdxExpression(BinaryOperator2.And, new BinaryMdxExpression(BinaryOperator2.Or, mdxExpression, mdxExpression2), new BinaryMdxExpression(BinaryOperator2.And, mdxExpression, mdxExpression2)), new BinaryMdxExpression(BinaryOperator2.And, new UnaryMdxExpression(MdxUnaryOperators.Not, new BinaryMdxExpression(BinaryOperator2.Or, mdxExpression, mdxExpression2)), new BinaryMdxExpression(BinaryOperator2.Equals, left, right)));
			return true;
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x000E8B74 File Offset: 0x000E6D74
		private bool TryCompileSetExpression(CubeExpression cubeExpression, out MdxLevel level, out MdxExpression mdx)
		{
			if (cubeExpression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)cubeExpression;
				BinaryOperator2 @operator = binaryCubeExpression.Operator;
				if (@operator - BinaryOperator2.Equals <= 1)
				{
					return this.TryCompileUniqueNameSetExpression(binaryCubeExpression.Left, binaryCubeExpression.Right, binaryCubeExpression.Operator, out level, out mdx) || this.TryCompileUniqueNameSetExpression(binaryCubeExpression.Right, binaryCubeExpression.Left, binaryCubeExpression.Operator, out level, out mdx);
				}
				if (@operator == BinaryOperator2.Or)
				{
					MdxLevel mdxLevel;
					MdxExpression mdxExpression;
					MdxLevel mdxLevel2;
					MdxExpression mdxExpression2;
					if (this.TryCompileSetExpression(binaryCubeExpression.Left, out mdxLevel, out mdxExpression) && this.TryCompileSetExpression(binaryCubeExpression.Right, out mdxLevel2, out mdxExpression2) && mdxLevel.Hierarchy.Equals(mdxLevel2.Hierarchy))
					{
						level = mdxLevel;
						mdx = new SetMdxExpression(new MdxExpression[] { mdxExpression, mdxExpression2 }).Flatten();
						return true;
					}
				}
			}
			level = null;
			mdx = null;
			return false;
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x000E8C48 File Offset: 0x000E6E48
		private bool TryCompileUniqueNameSetExpression(CubeExpression left, CubeExpression right, BinaryOperator2 op, out MdxLevel level, out MdxExpression mdx)
		{
			MdxCubeObject mdxCubeObject;
			if (left.Kind == CubeExpressionKind.Identifier && right.Kind == CubeExpressionKind.Constant && (op == BinaryOperator2.Equals || op == BinaryOperator2.NotEquals) && this.cube.TryGetObject(((IdentifierCubeExpression)left).Identifier, out mdxCubeObject) && mdxCubeObject.Kind == MdxCubeObjectKind.Property)
			{
				MdxProperty mdxProperty = (MdxProperty)mdxCubeObject;
				Value value = ((ConstantCubeExpression)right).Value;
				MdxIdentifier mdxIdentifier;
				if (mdxProperty.PropertyKind == MdxPropertyKind.MemberUniqueName && value.Kind == ValueKind.Text && MdxIdentifier.TryParse(value.AsString, out mdxIdentifier))
				{
					level = mdxProperty.Level;
					mdx = new IdentifierMdxExpression(value.AsString);
					if (op == BinaryOperator2.NotEquals)
					{
						MdxExpression mdxExpression = new InvocationMdxExpression(MdxFunction.AllMembers, new MdxExpression[]
						{
							new IdentifierMdxExpression(level.MdxIdentifier)
						});
						mdx = new InvocationMdxExpression(MdxFunction.Except, new MdxExpression[] { mdxExpression, mdx });
					}
					return true;
				}
			}
			mdx = null;
			level = null;
			return false;
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x000E8D38 File Offset: 0x000E6F38
		protected override bool TryCompileInequalityFilter(BinaryOperator2 inequalityOperator, MdxExpression left, MdxExpression right, out MdxExpression mdx)
		{
			MdxExpression mdxExpression = new InvocationMdxExpression(MdxFunction.IsEmpty, new MdxExpression[] { left });
			MdxExpression mdxExpression2 = new InvocationMdxExpression(MdxFunction.IsEmpty, new MdxExpression[] { right });
			mdx = new BinaryMdxExpression(BinaryOperator2.And, new UnaryMdxExpression(MdxUnaryOperators.Not, new BinaryMdxExpression(BinaryOperator2.Or, mdxExpression, mdxExpression2)), new BinaryMdxExpression(inequalityOperator, left, right));
			if (inequalityOperator == BinaryOperator2.GreaterThanOrEquals || inequalityOperator == BinaryOperator2.LessThanOrEquals)
			{
				mdx = new BinaryMdxExpression(BinaryOperator2.Or, new BinaryMdxExpression(BinaryOperator2.And, new BinaryMdxExpression(BinaryOperator2.Or, mdxExpression, mdxExpression2), new BinaryMdxExpression(BinaryOperator2.And, mdxExpression, mdxExpression2)), mdx);
			}
			return true;
		}

		// Token: 0x02000971 RID: 2417
		private class HierarchyBounds
		{
			// Token: 0x0600450B RID: 17675 RVA: 0x000E8DB7 File Offset: 0x000E6FB7
			public HierarchyBounds(MdxLevel level)
			{
				this.Upper = level;
				this.Lower = level;
			}

			// Token: 0x040024B4 RID: 9396
			public MdxLevel Upper;

			// Token: 0x040024B5 RID: 9397
			public MdxLevel Lower;
		}
	}
}
