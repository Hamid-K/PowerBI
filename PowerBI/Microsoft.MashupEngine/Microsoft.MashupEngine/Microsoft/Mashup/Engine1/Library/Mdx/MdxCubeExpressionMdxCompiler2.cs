using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000972 RID: 2418
	internal class MdxCubeExpressionMdxCompiler2 : MdxCubeExpressionMdxCompiler
	{
		// Token: 0x0600450C RID: 17676 RVA: 0x000E8DCD File Offset: 0x000E6FCD
		public MdxCubeExpressionMdxCompiler2(MdxCube cube, bool nonUniqueHierarchies, bool unscaleMeasures)
			: base(cube)
		{
			this.nonUniqueHierarchies = nonUniqueHierarchies;
			this.unscaleMeasures = unscaleMeasures;
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x0600450D RID: 17677 RVA: 0x000E8DE4 File Offset: 0x000E6FE4
		protected IdentifierCubeExpression MeasureOfOneCubeExpression
		{
			get
			{
				if (this.measureOfOneCubeExpression == null)
				{
					this.measureOfOneCubeExpression = new IdentifierCubeExpression(base.MeasureOfOne.Identifier);
				}
				return this.measureOfOneCubeExpression;
			}
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x000E8E0C File Offset: 0x000E700C
		public virtual bool CanCompile(QueryCubeExpression expression, out Dictionary<string, string> aliases)
		{
			MdxExpression mdxExpression;
			return this.TryCompile(expression, RowRange.All, out mdxExpression, out aliases);
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x000E8E28 File Offset: 0x000E7028
		public virtual MdxExpression Compile(QueryCubeExpression expression, RowRange rowRange, out Dictionary<string, string> aliases)
		{
			MdxExpression mdxExpression;
			if (!this.TryCompile(expression, rowRange, out mdxExpression, out aliases))
			{
				throw new InvalidOperationException();
			}
			return mdxExpression;
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x000E8E49 File Offset: 0x000E7049
		protected virtual SetCompiler NewSetCompiler()
		{
			return new MdxCubeExpressionMdxCompiler2.MdxSetCompiler(this);
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x000E8E51 File Offset: 0x000E7051
		protected virtual MdxCubeExpressionMdxCompiler2.MdxGenerator NewMdxGenerator()
		{
			return new MdxCubeExpressionMdxCompiler2.MdxGenerator(this);
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x000E8E5C File Offset: 0x000E705C
		protected override bool TryCompile(QueryCubeExpression expression, RowRange rowRange, out MdxExpression mdx)
		{
			Dictionary<string, string> dictionary;
			if (this.TryCompile(expression, rowRange, out mdx, out dictionary) && dictionary.Count == 0)
			{
				return true;
			}
			mdx = null;
			return false;
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x000E8E84 File Offset: 0x000E7084
		protected virtual bool TryCompile(QueryCubeExpression expression, RowRange rowRange, out MdxExpression mdx, out Dictionary<string, string> aliases)
		{
			List<MdxDeclaration> list = new List<MdxDeclaration>();
			if (expression.Measures.Count == 0)
			{
				list.AddRange(base.MeasureOfOneDeclaration);
				expression = new QueryCubeExpression(expression.From, expression.DimensionAttributes, expression.Properties, new IdentifierCubeExpression[] { this.MeasureOfOneCubeExpression }, expression.MeasureProperties, expression.Filter, expression.Sort, expression.RowRange);
			}
			Set set;
			if (this.NewSetCompiler().TryCompile(expression, out set))
			{
				try
				{
					set = set.SkipTake(rowRange);
					aliases = new Dictionary<string, string>();
					SelectMdxExpression selectMdxExpression = this.NewMdxGenerator().GenerateMdx(set, list, aliases) as SelectMdxExpression;
					if (selectMdxExpression != null)
					{
						mdx = new SelectMdxExpression(list.ToArray(), selectMdxExpression.Axes, selectMdxExpression.From, selectMdxExpression.Where, selectMdxExpression.CellProperties);
						return true;
					}
				}
				catch (NotSupportedException)
				{
				}
			}
			mdx = null;
			aliases = null;
			return false;
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x000767A6 File Offset: 0x000749A6
		protected virtual string GetPropertyIdentifier(MdxProperty property)
		{
			return property.MdxIdentifier;
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x000E8F74 File Offset: 0x000E7174
		protected virtual AxisMdxExpression CompileDimensionAxis(MdxExpression dimensionSet, MdxExpression nonDefaultMeasureSet, IEnumerable<IdentifierCubeExpression> properties)
		{
			if (nonDefaultMeasureSet != null)
			{
				dimensionSet = new InvocationMdxExpression(MdxFunction.NonEmpty, new MdxExpression[] { dimensionSet, nonDefaultMeasureSet });
			}
			List<string> list = new List<string> { "MEMBER_CAPTION", "MEMBER_UNIQUE_NAME" };
			list.AddRange(properties.Select((IdentifierCubeExpression property) => property.Identifier));
			return new AxisMdxExpression(dimensionSet, list.ToArray());
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x000E8FF0 File Offset: 0x000E71F0
		protected override bool TryCompileInequalityFilter(BinaryOperator2 inequalityOperator, MdxExpression left, MdxExpression right, out MdxExpression mdx)
		{
			MdxExpression mdxExpression = left.IsEmpty();
			MdxExpression mdxExpression2 = right.IsEmpty();
			mdx = mdxExpression.Not().And(mdxExpression2.Not()).And(new BinaryMdxExpression(inequalityOperator, left, right));
			if (inequalityOperator == BinaryOperator2.GreaterThanOrEquals || inequalityOperator == BinaryOperator2.LessThanOrEquals)
			{
				MdxExpression mdxExpression3;
				if (this.TrySimplifyEqualityExpression(left, right, out mdxExpression3))
				{
					mdx = mdx.Or(mdxExpression3);
				}
				else
				{
					mdx = mdxExpression.And(mdxExpression2).Or(mdx);
				}
			}
			return true;
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x000E9064 File Offset: 0x000E7264
		protected bool TrySimplifyEqualityExpression(MdxExpression left, MdxExpression right, out MdxExpression mdx)
		{
			if (ConstantMdxExpression.False.Equals(right))
			{
				mdx = left.Not();
				return true;
			}
			if (ConstantMdxExpression.False.Equals(left))
			{
				mdx = right.Not();
				return true;
			}
			if (ConstantMdxExpression.True.Equals(right))
			{
				mdx = left;
				return true;
			}
			if (ConstantMdxExpression.True.Equals(left))
			{
				mdx = right;
				return true;
			}
			mdx = null;
			return false;
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x000E90C8 File Offset: 0x000E72C8
		protected override bool TryCompileValueEqualityExpression(MdxExpression left, MdxExpression right, out MdxExpression mdx)
		{
			if (this.TrySimplifyEqualityExpression(left, right, out mdx))
			{
				return true;
			}
			MdxExpression mdxExpression = left.IsEmpty();
			MdxExpression mdxExpression2 = right.IsEmpty();
			mdx = mdxExpression.And(mdxExpression2).Or(mdxExpression.Not().And(mdxExpression2.Not()).And(left.Equals(right)));
			return true;
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void VerifyNonVisibleSetIsSupported(Set set)
		{
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x000E7BD5 File Offset: 0x000E5DD5
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

		// Token: 0x0600451B RID: 17691 RVA: 0x000E911C File Offset: 0x000E731C
		private static MdxExpression CrossJoin(MdxExpression set1, MdxExpression set2)
		{
			if (set1 == null)
			{
				return set2;
			}
			if (set2 == null)
			{
				return set1;
			}
			return new InvocationMdxExpression(MdxFunction.Crossjoin, new MdxExpression[] { set1, set2 });
		}

		// Token: 0x040024B6 RID: 9398
		private IdentifierCubeExpression measureOfOneCubeExpression;

		// Token: 0x040024B7 RID: 9399
		private readonly bool nonUniqueHierarchies;

		// Token: 0x040024B8 RID: 9400
		private readonly bool unscaleMeasures;

		// Token: 0x02000973 RID: 2419
		protected class MdxSetCompiler : SetCompiler
		{
			// Token: 0x0600451C RID: 17692 RVA: 0x000E913C File Offset: 0x000E733C
			public MdxSetCompiler(MdxCubeExpressionMdxCompiler2 compiler)
				: base(compiler.cube)
			{
				this.compiler = compiler;
			}

			// Token: 0x0600451D RID: 17693 RVA: 0x000E9151 File Offset: 0x000E7351
			protected override Set NewSelect(Dimensionality visibleDimensionality)
			{
				return new MdxCubeExpressionMdxCompiler2.SubSelectSet(this.compiler, visibleDimensionality);
			}

			// Token: 0x040024B9 RID: 9401
			protected readonly MdxCubeExpressionMdxCompiler2 compiler;
		}

		// Token: 0x02000974 RID: 2420
		protected class MdxGenerator
		{
			// Token: 0x0600451E RID: 17694 RVA: 0x000E915F File Offset: 0x000E735F
			public MdxGenerator(MdxCubeExpressionMdxCompiler2 compiler)
			{
				this.compiler = compiler;
			}

			// Token: 0x17001608 RID: 5640
			// (get) Token: 0x0600451F RID: 17695 RVA: 0x000E916E File Offset: 0x000E736E
			protected MdxCubeExpressionMdxCompiler2.MdxGenerator.Visitor SetVisitor
			{
				get
				{
					if (this.setVisitor == null)
					{
						this.setVisitor = this.CreateVisitor();
					}
					return this.setVisitor;
				}
			}

			// Token: 0x06004520 RID: 17696 RVA: 0x000E918A File Offset: 0x000E738A
			protected virtual MdxCubeExpressionMdxCompiler2.MdxGenerator.Visitor CreateVisitor()
			{
				return new MdxCubeExpressionMdxCompiler2.MdxGenerator.Visitor(this);
			}

			// Token: 0x06004521 RID: 17697 RVA: 0x000E9194 File Offset: 0x000E7394
			public MdxExpression GenerateMdx(Set set, List<MdxDeclaration> declarations, Dictionary<string, string> aliases)
			{
				List<MdxDeclaration> list = this.declarations;
				Dictionary<string, string> dictionary = this.aliases;
				this.declarations = declarations;
				this.aliases = aliases;
				this.depth++;
				MdxExpression mdxExpression;
				try
				{
					mdxExpression = this.SetVisitor.Visit(set);
				}
				finally
				{
					this.depth--;
					this.aliases = dictionary;
					this.declarations = list;
				}
				return mdxExpression;
			}

			// Token: 0x06004522 RID: 17698 RVA: 0x000E9208 File Offset: 0x000E7408
			public virtual MdxExpression NewFilter(MdxExpression set, MdxExpression expression)
			{
				if (ConstantMdxExpression.False.Equals(expression))
				{
					expression = new BinaryMdxExpression(BinaryOperator2.Equals, new ConstantMdxExpression(1), new ConstantMdxExpression(0));
				}
				return new InvocationMdxExpression(MdxFunction.Filter, new MdxExpression[] { set, expression });
			}

			// Token: 0x06004523 RID: 17699 RVA: 0x000E923F File Offset: 0x000E743F
			public virtual IdentifierMdxExpression NewIdentifier(string identifier)
			{
				return new IdentifierMdxExpression(identifier);
			}

			// Token: 0x06004524 RID: 17700 RVA: 0x000E9247 File Offset: 0x000E7447
			public virtual AxisMdxExpression NewMeasureAxis(MdxExpression measureSet)
			{
				return new AxisMdxExpression(measureSet, Array.Empty<string>());
			}

			// Token: 0x06004525 RID: 17701 RVA: 0x000E9254 File Offset: 0x000E7454
			protected virtual MdxExpression NewAddCalculatedMembers(MdxExpression mdxExpression)
			{
				return new InvocationMdxExpression(MdxFunction.AddCalculatedMembers, new MdxExpression[] { mdxExpression });
			}

			// Token: 0x06004526 RID: 17702 RVA: 0x00072EBF File Offset: 0x000710BF
			protected virtual MdxExpression NewAllMembers(MdxExpression mdxExpression)
			{
				return new InvocationMdxExpression(MdxFunction.AllMembers, new MdxExpression[] { mdxExpression });
			}

			// Token: 0x06004527 RID: 17703 RVA: 0x000E9267 File Offset: 0x000E7467
			protected virtual MdxDeclaration NewSetDeclaration(IdentifierMdxExpression identifier, MdxExpression definition, bool isSingledQuoted = false)
			{
				return new MdxSetDeclaration(identifier, definition, isSingledQuoted);
			}

			// Token: 0x06004528 RID: 17704 RVA: 0x000E9271 File Offset: 0x000E7471
			protected virtual MdxDeclaration NewMemberDeclaration(IdentifierMdxExpression identifier, MdxExpression definition, bool isSingledQuoted = false)
			{
				return new MdxMemberDeclaration(identifier, definition, isSingledQuoted);
			}

			// Token: 0x040024BA RID: 9402
			private readonly MdxCubeExpressionMdxCompiler2 compiler;

			// Token: 0x040024BB RID: 9403
			private MdxCubeExpressionMdxCompiler2.MdxGenerator.Visitor setVisitor;

			// Token: 0x040024BC RID: 9404
			private List<MdxDeclaration> declarations;

			// Token: 0x040024BD RID: 9405
			private Dictionary<string, string> aliases;

			// Token: 0x040024BE RID: 9406
			private int depth;

			// Token: 0x02000975 RID: 2421
			protected class Visitor : SetVisitor<MdxExpression, MdxExpression, CubeSortOrder>
			{
				// Token: 0x06004529 RID: 17705 RVA: 0x000E927B File Offset: 0x000E747B
				public Visitor(MdxCubeExpressionMdxCompiler2.MdxGenerator generator)
				{
					this.generator = generator;
				}

				// Token: 0x17001609 RID: 5641
				// (get) Token: 0x0600452A RID: 17706 RVA: 0x000E928A File Offset: 0x000E748A
				private MdxCubeExpressionMdxCompiler2 Compiler
				{
					get
					{
						return this.Generator.compiler;
					}
				}

				// Token: 0x1700160A RID: 5642
				// (get) Token: 0x0600452B RID: 17707 RVA: 0x000E9297 File Offset: 0x000E7497
				private List<MdxDeclaration> Declarations
				{
					get
					{
						return this.Generator.declarations;
					}
				}

				// Token: 0x1700160B RID: 5643
				// (get) Token: 0x0600452C RID: 17708 RVA: 0x000E92A4 File Offset: 0x000E74A4
				private Dictionary<string, string> Aliases
				{
					get
					{
						return this.Generator.aliases;
					}
				}

				// Token: 0x1700160C RID: 5644
				// (get) Token: 0x0600452D RID: 17709 RVA: 0x000E92B1 File Offset: 0x000E74B1
				private bool IsSubSelect
				{
					get
					{
						return this.Generator.depth > 1;
					}
				}

				// Token: 0x1700160D RID: 5645
				// (get) Token: 0x0600452E RID: 17710 RVA: 0x000E92C1 File Offset: 0x000E74C1
				protected MdxCubeExpressionMdxCompiler2.MdxGenerator Generator
				{
					get
					{
						return this.generator;
					}
				}

				// Token: 0x0600452F RID: 17711 RVA: 0x000E92CC File Offset: 0x000E74CC
				protected override MdxExpression NewCrossJoin(MdxExpression[] sets)
				{
					MdxExpression mdxExpression = null;
					for (int i = 0; i < sets.Length; i++)
					{
						mdxExpression = MdxCubeExpressionMdxCompiler2.CrossJoin(mdxExpression, sets[i]);
					}
					return mdxExpression;
				}

				// Token: 0x06004530 RID: 17712 RVA: 0x000E92F4 File Offset: 0x000E74F4
				protected override MdxExpression NewDescendTo(MdxExpression set, Dimensionality from, Dimensionality to)
				{
					if (to.HierarchyCount == 1)
					{
						MdxHierarchy mdxHierarchy = (MdxHierarchy)to.Hierarchies.Single<ICubeHierarchy>();
						MdxLevel mdxLevel = (MdxLevel)to.GetLevelRange(mdxHierarchy).Fine;
						return this.Generator.NewAddCalculatedMembers(this.NewDescendants(set, this.Generator.NewIdentifier(mdxLevel.MdxIdentifier)));
					}
					return this.NewDescendToMultipleHierarchies(set, from, to);
				}

				// Token: 0x06004531 RID: 17713 RVA: 0x000E935C File Offset: 0x000E755C
				protected MdxExpression NewDescendToMultipleHierarchies(MdxExpression set, Dimensionality from, Dimensionality to)
				{
					MdxExpression mdxExpression = null;
					foreach (ICubeHierarchy cubeHierarchy in from.Hierarchies)
					{
						MdxHierarchy mdxHierarchy = (MdxHierarchy)cubeHierarchy;
						MdxExpression mdxExpression2 = this.NewCurrentMember(this.Generator.NewIdentifier(mdxHierarchy.MdxIdentifier));
						MdxLevel mdxLevel = (MdxLevel)from.GetLevelRange(mdxHierarchy).Fine;
						MdxLevel mdxLevel2 = (MdxLevel)to.GetLevelRange(mdxHierarchy).Fine;
						if (mdxLevel2.Number > mdxLevel.Number)
						{
							mdxExpression2 = this.Generator.NewAddCalculatedMembers(this.NewDescendants(mdxExpression2, this.Generator.NewIdentifier(mdxLevel2.MdxIdentifier)));
						}
						mdxExpression = this.NewCrossJoin(new MdxExpression[] { mdxExpression, mdxExpression2 });
					}
					return this.NewGenerate(set, mdxExpression);
				}

				// Token: 0x06004532 RID: 17714 RVA: 0x000E9440 File Offset: 0x000E7640
				protected override MdxExpression NewDistinct(MdxExpression set)
				{
					return new InvocationMdxExpression(MdxFunction.Distinct, new MdxExpression[] { set });
				}

				// Token: 0x06004533 RID: 17715 RVA: 0x000020FA File Offset: 0x000002FA
				protected override MdxExpression NewEverything()
				{
					return null;
				}

				// Token: 0x06004534 RID: 17716 RVA: 0x000E9453 File Offset: 0x000E7653
				protected override MdxExpression NewExcept(MdxExpression set, MdxExpression except)
				{
					return new InvocationMdxExpression(MdxFunction.Except, new MdxExpression[] { set, except });
				}

				// Token: 0x06004535 RID: 17717 RVA: 0x000E946A File Offset: 0x000E766A
				protected override MdxExpression NewFilter(MdxExpression set, MdxExpression expression)
				{
					return this.Generator.NewFilter(set, expression);
				}

				// Token: 0x06004536 RID: 17718 RVA: 0x000E9479 File Offset: 0x000E7679
				protected override MdxExpression NewIntersect(MdxExpression set1, MdxExpression set2)
				{
					return new InvocationMdxExpression(MdxFunction.Intersect, new MdxExpression[] { set1, set2 });
				}

				// Token: 0x06004537 RID: 17719 RVA: 0x000E9490 File Offset: 0x000E7690
				protected override MdxExpression NewLevel(ICubeLevel level)
				{
					MdxLevel mdxLevel = (MdxLevel)level;
					return this.Generator.NewAllMembers(this.Generator.NewIdentifier(mdxLevel.MdxIdentifier));
				}

				// Token: 0x06004538 RID: 17720 RVA: 0x000E94C0 File Offset: 0x000E76C0
				protected override MdxExpression NewMember(ICubeLevel level, Value member)
				{
					if (member.Equals(MdxCubeExpressionMdxCompiler2.CurrentMemberSet.CurrentMember))
					{
						MdxLevel mdxLevel = (MdxLevel)level;
						return this.NewCurrentMember(this.Generator.NewIdentifier(mdxLevel.MdxIdentifier));
					}
					return this.Generator.NewIdentifier(member.AsString);
				}

				// Token: 0x06004539 RID: 17721 RVA: 0x000E950C File Offset: 0x000E770C
				protected override MdxExpression NewOrderBy(MdxExpression set, CubeSortOrder[] order)
				{
					SelectMdxExpression selectMdxExpression = set as SelectMdxExpression;
					if (selectMdxExpression != null)
					{
						int num = selectMdxExpression.Axes.Length - 1;
						AxisMdxExpression axisMdxExpression = selectMdxExpression.Axes[num];
						MdxExpression set2 = axisMdxExpression.Set;
						IEnumerable<MdxDeclaration> enumerable;
						if (this.Compiler.TryCompileOrdering(set2, order, out set2, out enumerable))
						{
							this.Declarations.AddRange(enumerable);
							AxisMdxExpression[] array = (AxisMdxExpression[])selectMdxExpression.Axes.Clone();
							array[num] = new AxisMdxExpression(axisMdxExpression.NonEmpty, set2, axisMdxExpression.Properties);
							return new SelectMdxExpression(selectMdxExpression.Declarations, array, selectMdxExpression.From, selectMdxExpression.Where, selectMdxExpression.CellProperties);
						}
					}
					throw new NotSupportedException();
				}

				// Token: 0x0600453A RID: 17722 RVA: 0x000E95B0 File Offset: 0x000E77B0
				protected override MdxExpression NewOrderHierarchies(MdxExpression set, Dimensionality from, Dimensionality to)
				{
					MdxExpression mdxExpression = null;
					foreach (ICubeHierarchy cubeHierarchy in to.Hierarchies)
					{
						MdxHierarchy mdxHierarchy = (MdxHierarchy)cubeHierarchy;
						MdxExpression mdxExpression2 = this.NewCurrentMember(this.Generator.NewIdentifier(mdxHierarchy.MdxIdentifier));
						mdxExpression = this.NewCrossJoin(new MdxExpression[] { mdxExpression, mdxExpression2 });
					}
					return this.NewGenerate(set, mdxExpression);
				}

				// Token: 0x0600453B RID: 17723 RVA: 0x000033E7 File Offset: 0x000015E7
				protected override MdxExpression NewProject(MdxExpression set, IEnumerable<ICubeObject> objects)
				{
					throw new NotSupportedException();
				}

				// Token: 0x0600453C RID: 17724 RVA: 0x000E9634 File Offset: 0x000E7834
				protected override MdxExpression NewSkipTake(MdxExpression set, RowRange rowRange)
				{
					SelectMdxExpression selectMdxExpression = set as SelectMdxExpression;
					if (selectMdxExpression != null)
					{
						int num = selectMdxExpression.Axes.Length - 1;
						AxisMdxExpression axisMdxExpression = selectMdxExpression.Axes[num];
						MdxExpression set2 = axisMdxExpression.Set;
						if (this.Compiler.TryCompileSkipTake(rowRange, set2, out set2))
						{
							AxisMdxExpression[] array = (AxisMdxExpression[])selectMdxExpression.Axes.Clone();
							array[num] = new AxisMdxExpression(axisMdxExpression.NonEmpty, set2, axisMdxExpression.Properties);
							return new SelectMdxExpression(selectMdxExpression.Declarations, array, selectMdxExpression.From, selectMdxExpression.Where, selectMdxExpression.CellProperties);
						}
					}
					throw new NotSupportedException();
				}

				// Token: 0x0600453D RID: 17725 RVA: 0x000E96C4 File Offset: 0x000E78C4
				protected override MdxExpression NewUnion(MdxExpression[] sets)
				{
					return new SetMdxExpression(sets);
				}

				// Token: 0x0600453E RID: 17726 RVA: 0x000E96CC File Offset: 0x000E78CC
				protected override MdxExpression VisitFilter(MdxExpression set, CubeExpression expression)
				{
					MdxExpression mdxExpression;
					if (this.Compiler.TryCompileFilterExpression(expression, out mdxExpression))
					{
						return mdxExpression;
					}
					throw new NotSupportedException();
				}

				// Token: 0x0600453F RID: 17727 RVA: 0x000020F7 File Offset: 0x000002F7
				protected override CubeSortOrder VisitOrder(MdxExpression set, CubeSortOrder order)
				{
					return order;
				}

				// Token: 0x06004540 RID: 17728 RVA: 0x000E96F0 File Offset: 0x000E78F0
				protected override MdxExpression VisitOther(Set set)
				{
					MdxCubeExpressionMdxCompiler2.MeasuresSet measuresSet = set as MdxCubeExpressionMdxCompiler2.MeasuresSet;
					if (measuresSet != null)
					{
						MdxExpression[] measures = measuresSet.Measures;
						return new SetMdxExpression(measures);
					}
					MdxCubeExpressionMdxCompiler2.SelectSet selectSet = set as MdxCubeExpressionMdxCompiler2.SelectSet;
					if (selectSet != null)
					{
						return this.VisitSelect(selectSet);
					}
					throw new NotSupportedException();
				}

				// Token: 0x06004541 RID: 17729 RVA: 0x000E972C File Offset: 0x000E792C
				protected override MdxExpression VisitUnion(UnionSet union)
				{
					Set set = null;
					CubeExpression cubeExpression = null;
					for (int i = 0; i < union.Sets.Length; i++)
					{
						FilterSet filterSet = union.Sets[i] as FilterSet;
						if (filterSet == null || (set != null && !set.Equals(filterSet.Set)))
						{
							set = null;
							cubeExpression = null;
							break;
						}
						set = filterSet.Set;
						cubeExpression = cubeExpression.Or(filterSet.Predicate);
					}
					if (set != null)
					{
						MdxExpression mdxExpression = this.Visit(set);
						return this.NewFilter(mdxExpression, this.VisitFilter(mdxExpression, cubeExpression));
					}
					return base.VisitUnion(union);
				}

				// Token: 0x06004542 RID: 17730 RVA: 0x000E97B4 File Offset: 0x000E79B4
				private MdxExpression VisitSelect(MdxCubeExpressionMdxCompiler2.SelectSet select)
				{
					this.Compiler.VerifyNonVisibleSetIsSupported(select.OffGrainVisibleAxis);
					this.Compiler.VerifyNonVisibleSetIsSupported(select.SlicerAxis);
					this.Compiler.VerifyNonVisibleSetIsSupported(select.SubSelect);
					IdentifierMdxExpression[] array = EmptyArray<IdentifierMdxExpression>.Instance;
					MdxExpression mdxExpression = null;
					bool flag = false;
					if (select.Measures.Count > 0)
					{
						array = new IdentifierMdxExpression[select.Measures.Count];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = this.Generator.NewIdentifier(select.Measures[i].Identifier);
						}
						IdentifierMdxExpression[] array2 = new IdentifierMdxExpression[array.Length];
						for (int j = 0; j < array.Length; j++)
						{
							IdentifierMdxExpression identifierMdxExpression = array[j];
							if (this.Compiler.cube.CountMeasure.MdxIdentifier.Equals(identifierMdxExpression.Identifier))
							{
								Set set = EverythingSet.Instance;
								foreach (ICubeHierarchy cubeHierarchy in select.VisibleAxis.Dimensionality.Hierarchies)
								{
									MdxHierarchy mdxHierarchy = (MdxHierarchy)cubeHierarchy;
									CubeLevelRange levelRange = select.VisibleAxis.Dimensionality.GetLevelRange(mdxHierarchy);
									set = set.ExpandTo(new Dimensionality(new CubeLevelRange[]
									{
										new CubeLevelRange(levelRange.Fine, mdxHierarchy.Levels.Last<MdxLevel>())
									}));
									set = set.Intersect(new MdxCubeExpressionMdxCompiler2.CurrentMemberSet(levelRange.Fine));
								}
								IdentifierMdxExpression identifierMdxExpression2 = this.Generator.NewIdentifier(this.Compiler.MeasuresDimensionName + ".[Microsoft.Mashup.Engine.count." + j.ToString() + "]");
								MdxExpression mdxExpression2 = new InvocationMdxExpression(MdxFunction.Count, new MdxExpression[]
								{
									this.CreateMdxExpression(set),
									ConstantMdxExpression.IncludeEmpty
								});
								this.Declarations.Add(this.Generator.NewMemberDeclaration(identifierMdxExpression2, mdxExpression2, false));
								array2[j] = identifierMdxExpression2;
								this.Aliases.Add(identifierMdxExpression2.Identifier, identifierMdxExpression.Identifier);
							}
							else
							{
								array2[j] = identifierMdxExpression;
								if (!this.Compiler.MeasureOfOne.Equals(identifierMdxExpression))
								{
									flag = true;
									if (this.Compiler.unscaleMeasures)
									{
										MdxMeasure mdxMeasure = (MdxMeasure)this.Compiler.cube.GetObject(identifierMdxExpression.Identifier);
										IdentifierMdxExpression identifierMdxExpression3 = this.Generator.NewIdentifier(this.Compiler.MeasuresDimensionName + ".[Microsoft.Mashup.Engine.unscaled." + j.ToString() + "]");
										MdxExpression mdxExpression3 = new BinaryMdxExpression(BinaryOperator2.Multiply, identifierMdxExpression, ConstantMdxExpression.OnePointZero);
										this.Declarations.Add(this.Generator.NewMemberDeclaration(identifierMdxExpression3, mdxExpression3, false));
										array2[j] = identifierMdxExpression3;
										this.Aliases.Add(identifierMdxExpression3.Identifier, identifierMdxExpression.Identifier);
									}
								}
							}
						}
						array = array2;
						if (select.OffGrainVisibleAxis.GetSubsets().Any<Set>())
						{
							Set set2 = select.OffGrainVisibleAxis;
							foreach (ICubeHierarchy cubeHierarchy2 in select.VisibleAxis.Dimensionality.Hierarchies)
							{
								MdxHierarchy mdxHierarchy2 = (MdxHierarchy)cubeHierarchy2;
								CubeLevelRange levelRange2 = select.VisibleAxis.Dimensionality.GetLevelRange(mdxHierarchy2);
								set2 = set2.Intersect(new MdxCubeExpressionMdxCompiler2.CurrentMemberSet(levelRange2.Fine));
							}
							if (flag)
							{
								IdentifierMdxExpression identifierMdxExpression4 = this.Generator.NewIdentifier("[Microsoft.Mashup.Engine.set]");
								this.Declarations.Add(this.Generator.NewSetDeclaration(identifierMdxExpression4, this.CreateMdxExpression(set2), false));
								array2 = new IdentifierMdxExpression[array.Length];
								for (int k = 0; k < array2.Length; k++)
								{
									IdentifierMdxExpression identifierMdxExpression5 = array[k];
									IdentifierMdxExpression identifierMdxExpression6 = this.Generator.NewIdentifier(this.Compiler.MeasuresDimensionName + ".[Microsoft.Mashup.Engine." + k.ToString() + "]");
									MdxExpression mdxExpression4 = new InvocationMdxExpression(MdxFunction.Aggregate, new MdxExpression[] { identifierMdxExpression4, identifierMdxExpression5 });
									array2[k] = identifierMdxExpression6;
									this.Declarations.Add(this.Generator.NewMemberDeclaration(identifierMdxExpression6, mdxExpression4, false));
									this.Aliases.Add(identifierMdxExpression6.Identifier, identifierMdxExpression5.Identifier);
								}
								array = array2;
							}
							else
							{
								mdxExpression = this.CreateMdxExpression(set2);
							}
						}
					}
					Set set3 = MdxCubeExpressionMdxCompiler2.MeasuresSet.New(array);
					foreach (Set set4 in select.MeasureAxis.GetSubsets())
					{
						set3 = set3.Intersect(set4);
					}
					MdxExpression mdxExpression5 = this.CreateMdxExpression(set3);
					MdxExpression mdxExpression6;
					if (!this.IsSubSelect && select.VisibleAxis.Dimensionality.HierarchyCount == 0)
					{
						if (mdxExpression5 == null)
						{
							throw new NotSupportedException();
						}
						mdxExpression6 = mdxExpression5;
					}
					else
					{
						mdxExpression6 = this.CreateMdxExpression(select.VisibleAxis);
					}
					if (mdxExpression != null)
					{
						mdxExpression6 = this.Generator.NewFilter(mdxExpression6, new BinaryMdxExpression(BinaryOperator2.GreaterThan, new InvocationMdxExpression(MdxFunction.Count, new MdxExpression[] { mdxExpression }), new ConstantMdxExpression(0)));
					}
					MdxExpression mdxExpression7 = this.CreateMdxExpression(select.SlicerAxis);
					MdxExpression mdxExpression8 = this.CreateMdxExpression(select.SubSelect) ?? this.Generator.NewIdentifier(this.Compiler.cube.MdxIdentifier);
					ArrayBuilder<AxisMdxExpression> arrayBuilder = default(ArrayBuilder<AxisMdxExpression>);
					if (this.IsSubSelect || select.VisibleAxis.Dimensionality.HierarchyCount == 0)
					{
						if (mdxExpression5 != null)
						{
							arrayBuilder.Add(this.Generator.NewMeasureAxis(mdxExpression5));
						}
						else
						{
							arrayBuilder.Add(new AxisMdxExpression(mdxExpression6, Array.Empty<string>()));
						}
					}
					else
					{
						if (mdxExpression5 != null)
						{
							arrayBuilder.Add(this.Generator.NewMeasureAxis(mdxExpression5));
						}
						arrayBuilder.Add(this.Compiler.CompileDimensionAxis(mdxExpression6, flag ? mdxExpression5 : null, select.Properties));
					}
					string[] array3 = (this.IsSubSelect ? null : this.Compiler.CompileCellProperties(select.MeasureProperties));
					return new SelectMdxExpression(EmptyArray<MdxDeclaration>.Instance, arrayBuilder.ToArray(), mdxExpression8, mdxExpression7, array3);
				}

				// Token: 0x06004543 RID: 17731 RVA: 0x000E9DEC File Offset: 0x000E7FEC
				private MdxExpression CreateMdxExpression(Set set)
				{
					if (this.Compiler.nonUniqueHierarchies)
					{
						set = set.EnsureUniqueHierarchyMembers();
					}
					return this.Generator.GenerateMdx(set, this.Declarations, this.Aliases);
				}

				// Token: 0x06004544 RID: 17732 RVA: 0x000E9E1B File Offset: 0x000E801B
				protected MdxExpression NewCurrentMember(MdxExpression mdxExpression)
				{
					return new InvocationMdxExpression(MdxFunction.CurrentMember, new MdxExpression[] { mdxExpression });
				}

				// Token: 0x06004545 RID: 17733 RVA: 0x000E9E2E File Offset: 0x000E802E
				protected MdxExpression NewDescendants(MdxExpression hierarchy, MdxExpression level)
				{
					return new InvocationMdxExpression(MdxFunction.Descendants, new MdxExpression[]
					{
						hierarchy,
						level,
						ConstantMdxExpression.Leaves
					});
				}

				// Token: 0x06004546 RID: 17734 RVA: 0x000E9E4D File Offset: 0x000E804D
				protected virtual MdxExpression NewGenerate(MdxExpression mdxExpression, MdxExpression generateSet)
				{
					return new InvocationMdxExpression(MdxFunction.Generate, new MdxExpression[]
					{
						mdxExpression,
						generateSet,
						ConstantMdxExpression.All
					});
				}

				// Token: 0x06004547 RID: 17735 RVA: 0x000E9E6C File Offset: 0x000E806C
				private MdxExpression NewSet(IdentifierMdxExpression[] mdxExpressions)
				{
					return new SetMdxExpression(mdxExpressions);
				}

				// Token: 0x040024BF RID: 9407
				private readonly MdxCubeExpressionMdxCompiler2.MdxGenerator generator;
			}
		}

		// Token: 0x02000976 RID: 2422
		protected class CurrentMemberSet : MemberSet
		{
			// Token: 0x06004548 RID: 17736 RVA: 0x000E9E81 File Offset: 0x000E8081
			public CurrentMemberSet(ICubeLevel level)
				: base(level, MdxCubeExpressionMdxCompiler2.CurrentMemberSet.CurrentMember)
			{
			}

			// Token: 0x040024C0 RID: 9408
			public static readonly TextValue CurrentMember = TextValue.New("CurrentMember");
		}

		// Token: 0x02000977 RID: 2423
		private class MeasuresSet : Set
		{
			// Token: 0x0600454A RID: 17738 RVA: 0x000E9EA0 File Offset: 0x000E80A0
			public static Set New(IdentifierMdxExpression[] measures)
			{
				if (measures.Length != 0)
				{
					return new MdxCubeExpressionMdxCompiler2.MeasuresSet(measures);
				}
				return EverythingSet.Instance;
			}

			// Token: 0x0600454B RID: 17739 RVA: 0x000E9EB2 File Offset: 0x000E80B2
			private MeasuresSet(IdentifierMdxExpression[] measures)
			{
				this.measures = measures;
			}

			// Token: 0x1700160E RID: 5646
			// (get) Token: 0x0600454C RID: 17740 RVA: 0x0006808E File Offset: 0x0006628E
			public override SetKind Kind
			{
				get
				{
					return SetKind.Other;
				}
			}

			// Token: 0x1700160F RID: 5647
			// (get) Token: 0x0600454D RID: 17741 RVA: 0x000E9EC1 File Offset: 0x000E80C1
			public override double Cardinality
			{
				get
				{
					return (double)this.measures.Length;
				}
			}

			// Token: 0x17001610 RID: 5648
			// (get) Token: 0x0600454E RID: 17742 RVA: 0x000E9ECC File Offset: 0x000E80CC
			public override Dimensionality Dimensionality
			{
				get
				{
					return Dimensionality.Empty;
				}
			}

			// Token: 0x17001611 RID: 5649
			// (get) Token: 0x0600454F RID: 17743 RVA: 0x00002105 File Offset: 0x00000305
			public override bool HasMeasureFilter
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001612 RID: 5650
			// (get) Token: 0x06004550 RID: 17744 RVA: 0x000E9ED3 File Offset: 0x000E80D3
			public IdentifierMdxExpression[] Measures
			{
				get
				{
					return this.measures;
				}
			}

			// Token: 0x06004551 RID: 17745 RVA: 0x000E9EDB File Offset: 0x000E80DB
			public override IEnumerable<Set> GetSubsets()
			{
				yield return this;
				yield break;
			}

			// Token: 0x06004552 RID: 17746 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override Set EnsureUniqueHierarchyMembers()
			{
				return this;
			}

			// Token: 0x06004553 RID: 17747 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override Set Unordered()
			{
				return this;
			}

			// Token: 0x06004554 RID: 17748 RVA: 0x000033E7 File Offset: 0x000015E7
			public override Set NewScope(string scope)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06004555 RID: 17749 RVA: 0x000E9EEC File Offset: 0x000E80EC
			public bool Equals(MdxCubeExpressionMdxCompiler2.MeasuresSet other)
			{
				bool flag = other != null && this.measures.Length == other.measures.Length;
				int num = 0;
				while (flag && num < this.measures.Length)
				{
					flag &= this.measures[num].Equals(other.measures[num]);
					num++;
				}
				return flag;
			}

			// Token: 0x06004556 RID: 17750 RVA: 0x000E9F41 File Offset: 0x000E8141
			public override bool Equals(object other)
			{
				return this.Equals(other as MdxCubeExpressionMdxCompiler2.MeasuresSet);
			}

			// Token: 0x06004557 RID: 17751 RVA: 0x000E9F50 File Offset: 0x000E8150
			public override int GetHashCode()
			{
				int num = 5011 * this.measures.Length;
				for (int i = 0; i < this.measures.Length; i++)
				{
					num += this.measures[i].GetHashCode();
				}
				return num;
			}

			// Token: 0x040024C1 RID: 9409
			private readonly IdentifierMdxExpression[] measures;
		}

		// Token: 0x02000979 RID: 2425
		protected abstract class SelectSet : Set
		{
			// Token: 0x06004560 RID: 17760 RVA: 0x000EA043 File Offset: 0x000E8243
			protected SelectSet(MdxCubeExpressionMdxCompiler2 compiler, Set measureAxis, Set visibleAxis, IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measureProperties)
			{
				this.compiler = compiler;
				this.measureAxis = measureAxis;
				this.visibleAxis = visibleAxis;
				this.measures = measures;
				this.properties = properties;
				this.measureProperties = measureProperties;
			}

			// Token: 0x17001615 RID: 5653
			// (get) Token: 0x06004561 RID: 17761 RVA: 0x0006808E File Offset: 0x0006628E
			public override SetKind Kind
			{
				get
				{
					return SetKind.Other;
				}
			}

			// Token: 0x17001616 RID: 5654
			// (get) Token: 0x06004562 RID: 17762 RVA: 0x000EA078 File Offset: 0x000E8278
			public override double Cardinality
			{
				get
				{
					return this.VisibleAxis.Cardinality;
				}
			}

			// Token: 0x17001617 RID: 5655
			// (get) Token: 0x06004563 RID: 17763 RVA: 0x000EA085 File Offset: 0x000E8285
			public override Dimensionality Dimensionality
			{
				get
				{
					return this.VisibleAxis.Dimensionality;
				}
			}

			// Token: 0x17001618 RID: 5656
			// (get) Token: 0x06004564 RID: 17764 RVA: 0x000EA094 File Offset: 0x000E8294
			public override bool HasMeasureFilter
			{
				get
				{
					return this.MeasureAxis.HasMeasureFilter || this.VisibleAxis.HasMeasureFilter || this.OffGrainVisibleAxis.HasMeasureFilter || this.SlicerAxis.HasMeasureFilter || this.SubSelect.HasMeasureFilter;
				}
			}

			// Token: 0x17001619 RID: 5657
			// (get) Token: 0x06004565 RID: 17765 RVA: 0x000EA0E2 File Offset: 0x000E82E2
			public Set MeasureAxis
			{
				get
				{
					return this.measureAxis;
				}
			}

			// Token: 0x1700161A RID: 5658
			// (get) Token: 0x06004566 RID: 17766 RVA: 0x000EA0EA File Offset: 0x000E82EA
			public Set VisibleAxis
			{
				get
				{
					return this.visibleAxis;
				}
			}

			// Token: 0x1700161B RID: 5659
			// (get) Token: 0x06004567 RID: 17767
			public abstract Set OffGrainVisibleAxis { get; }

			// Token: 0x1700161C RID: 5660
			// (get) Token: 0x06004568 RID: 17768
			public abstract Set SlicerAxis { get; }

			// Token: 0x1700161D RID: 5661
			// (get) Token: 0x06004569 RID: 17769
			public abstract Set SubSelect { get; }

			// Token: 0x1700161E RID: 5662
			// (get) Token: 0x0600456A RID: 17770 RVA: 0x000EA0F2 File Offset: 0x000E82F2
			public IList<IdentifierCubeExpression> Measures
			{
				get
				{
					return this.measures;
				}
			}

			// Token: 0x1700161F RID: 5663
			// (get) Token: 0x0600456B RID: 17771 RVA: 0x000EA0FA File Offset: 0x000E82FA
			public IList<IdentifierCubeExpression> Properties
			{
				get
				{
					return this.properties;
				}
			}

			// Token: 0x17001620 RID: 5664
			// (get) Token: 0x0600456C RID: 17772 RVA: 0x000EA102 File Offset: 0x000E8302
			public IList<IdentifierCubeExpression> MeasureProperties
			{
				get
				{
					return this.measureProperties;
				}
			}

			// Token: 0x0600456D RID: 17773 RVA: 0x000EA10A File Offset: 0x000E830A
			public override IEnumerable<Set> GetSubsets()
			{
				foreach (Set set in this.MeasureAxis.GetSubsets())
				{
					yield return set;
				}
				IEnumerator<Set> enumerator = null;
				foreach (Set set2 in this.VisibleAxis.GetSubsets())
				{
					yield return set2;
				}
				enumerator = null;
				foreach (Set set3 in this.OffGrainVisibleAxis.GetSubsets())
				{
					if (!(set3 is MdxCubeExpressionMdxCompiler2.CurrentMemberSet))
					{
						yield return set3;
					}
				}
				enumerator = null;
				foreach (Set set4 in this.SlicerAxis.GetSubsets())
				{
					yield return set4;
				}
				enumerator = null;
				foreach (Set set5 in this.SubSelect.GetSubsets())
				{
					yield return set5;
				}
				enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0600456E RID: 17774 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override Set CrossJoinAsLeft(Set other)
			{
				return this;
			}

			// Token: 0x0600456F RID: 17775 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override Set CrossJoinAsRight(Set other)
			{
				return this;
			}

			// Token: 0x06004570 RID: 17776 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override Set DescendTo(Dimensionality newDimensionality)
			{
				return this;
			}

			// Token: 0x06004571 RID: 17777 RVA: 0x000EA11A File Offset: 0x000E831A
			public override Set IntersectAsLeft(Set other)
			{
				return this.IntersectCommon(other);
			}

			// Token: 0x06004572 RID: 17778 RVA: 0x000EA11A File Offset: 0x000E831A
			public override Set IntersectAsRight(Set other)
			{
				return this.IntersectCommon(other);
			}

			// Token: 0x06004573 RID: 17779
			protected abstract Set IntersectCommon(Set other);

			// Token: 0x06004574 RID: 17780 RVA: 0x000EA124 File Offset: 0x000E8324
			public override Set Project(IEnumerable<ICubeObject> projected)
			{
				HashSet<IdentifierCubeExpression> hashSet = new HashSet<IdentifierCubeExpression>(this.measures);
				HashSet<IdentifierCubeExpression> hashSet2 = new HashSet<IdentifierCubeExpression>(this.properties);
				HashSet<IdentifierCubeExpression> hashSet3 = new HashSet<IdentifierCubeExpression>(this.measureProperties);
				foreach (ICubeObject cubeObject in projected)
				{
					switch (cubeObject.Kind)
					{
					case CubeObjectKind.Property:
					{
						MdxProperty mdxProperty = (MdxProperty)cubeObject;
						if (mdxProperty.PropertyKind == MdxPropertyKind.UserDefined)
						{
							hashSet2.Add(new IdentifierCubeExpression(this.compiler.GetPropertyIdentifier(mdxProperty)));
							if (mdxProperty.Key != null && mdxProperty.Key.Name != MdxMemberProperties.QuotedMemberUniqueName)
							{
								hashSet2.Add(new IdentifierCubeExpression(this.compiler.GetPropertyIdentifier(mdxProperty.Key)));
							}
						}
						break;
					}
					case CubeObjectKind.Measure:
					{
						MdxMeasure mdxMeasure = (MdxMeasure)cubeObject;
						hashSet.Add(new IdentifierCubeExpression(mdxMeasure.MdxIdentifier));
						break;
					}
					case CubeObjectKind.MeasureProperty:
					{
						MdxCellProperty mdxCellProperty = (MdxCellProperty)cubeObject;
						hashSet3.Add(new IdentifierCubeExpression(mdxCellProperty.MdxIdentifier));
						break;
					}
					default:
						throw new NotSupportedException();
					}
				}
				return this.NewMeasuresAndProperties(hashSet.ToArray<IdentifierCubeExpression>(), hashSet2.ToArray<IdentifierCubeExpression>(), hashSet3.ToArray<IdentifierCubeExpression>());
			}

			// Token: 0x06004575 RID: 17781 RVA: 0x000033E7 File Offset: 0x000015E7
			public override Set NewScope(string scope)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06004576 RID: 17782 RVA: 0x000EA280 File Offset: 0x000E8480
			public bool Equals(MdxCubeExpressionMdxCompiler2.SelectSet other)
			{
				return other != null && this.MeasureAxis.Equals(other.MeasureAxis) && this.VisibleAxis.Equals(other.VisibleAxis) && this.OffGrainVisibleAxis.Equals(other.OffGrainVisibleAxis) && this.SlicerAxis.Equals(other.SlicerAxis) && this.SubSelect.Equals(other.SubSelect) && this.Measures.SequenceEqual(other.Measures) && this.Properties.SequenceEqual(other.Properties);
			}

			// Token: 0x06004577 RID: 17783 RVA: 0x000EA318 File Offset: 0x000E8518
			public override bool Equals(object other)
			{
				return this.Equals(other as MdxCubeExpressionMdxCompiler2.SelectSet);
			}

			// Token: 0x06004578 RID: 17784 RVA: 0x000EA328 File Offset: 0x000E8528
			public override int GetHashCode()
			{
				return this.MeasureAxis.GetHashCode() + 37 * this.VisibleAxis.GetHashCode() + 139 * this.OffGrainVisibleAxis.GetHashCode() + 5011 * this.SlicerAxis.GetHashCode() + 19373 * this.SubSelect.GetHashCode();
			}

			// Token: 0x06004579 RID: 17785
			protected abstract Set NewMeasuresAndProperties(IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measureProperties);

			// Token: 0x040024C6 RID: 9414
			protected readonly MdxCubeExpressionMdxCompiler2 compiler;

			// Token: 0x040024C7 RID: 9415
			private readonly Set measureAxis;

			// Token: 0x040024C8 RID: 9416
			private readonly Set visibleAxis;

			// Token: 0x040024C9 RID: 9417
			private readonly IList<IdentifierCubeExpression> measures;

			// Token: 0x040024CA RID: 9418
			private readonly IList<IdentifierCubeExpression> properties;

			// Token: 0x040024CB RID: 9419
			private readonly IList<IdentifierCubeExpression> measureProperties;
		}

		// Token: 0x0200097B RID: 2427
		protected class SubSelectSet : MdxCubeExpressionMdxCompiler2.SelectSet
		{
			// Token: 0x06004587 RID: 17799 RVA: 0x000EA73F File Offset: 0x000E893F
			public SubSelectSet(MdxCubeExpressionMdxCompiler2 compiler, Dimensionality visibleAxisDimensionality)
				: this(compiler, EverythingSet.Instance, EverythingSet.Instance.ExpandTo(visibleAxisDimensionality), new MdxCubeExpressionMdxCompiler2.SubSelectSet.SelectEverythingSet(compiler), EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance)
			{
			}

			// Token: 0x06004588 RID: 17800 RVA: 0x000EA76D File Offset: 0x000E896D
			private SubSelectSet(MdxCubeExpressionMdxCompiler2 compiler, Set measureAxis, Set visibleAxis, Set subSelect, IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measureProperties)
				: base(compiler, measureAxis, visibleAxis, measures, properties, measureProperties)
			{
				this.subSelect = subSelect;
			}

			// Token: 0x17001623 RID: 5667
			// (get) Token: 0x06004589 RID: 17801 RVA: 0x000EA786 File Offset: 0x000E8986
			public override Set OffGrainVisibleAxis
			{
				get
				{
					return EverythingSet.Instance;
				}
			}

			// Token: 0x17001624 RID: 5668
			// (get) Token: 0x0600458A RID: 17802 RVA: 0x000EA786 File Offset: 0x000E8986
			public override Set SlicerAxis
			{
				get
				{
					return EverythingSet.Instance;
				}
			}

			// Token: 0x17001625 RID: 5669
			// (get) Token: 0x0600458B RID: 17803 RVA: 0x000EA78D File Offset: 0x000E898D
			public override Set SubSelect
			{
				get
				{
					return this.subSelect;
				}
			}

			// Token: 0x0600458C RID: 17804 RVA: 0x000EA798 File Offset: 0x000E8998
			protected override Set IntersectCommon(Set other)
			{
				Set set = base.MeasureAxis;
				Set set2 = base.VisibleAxis;
				Set set3 = this.SubSelect;
				if (other.Dimensionality.HierarchyCount == 0)
				{
					set = set.Intersect(other);
				}
				else
				{
					bool flag = false;
					foreach (ICubeHierarchy cubeHierarchy in other.Dimensionality.Hierarchies)
					{
						CubeLevelRange levelRange = other.Dimensionality.GetLevelRange(cubeHierarchy);
						CubeLevelRange cubeLevelRange;
						if (!set2.Dimensionality.TryGetLevelRange(cubeHierarchy, out cubeLevelRange) || levelRange.Coarse.Number < cubeLevelRange.Coarse.Number || levelRange.Fine.Number > cubeLevelRange.Fine.Number)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						set3 = set3.Intersect(other);
					}
					else
					{
						set2 = set2.Intersect(other);
					}
				}
				return new MdxCubeExpressionMdxCompiler2.SubSelectSet(this.compiler, set, set2, set3, base.Measures, base.Properties, base.MeasureProperties);
			}

			// Token: 0x0600458D RID: 17805 RVA: 0x000EA8A8 File Offset: 0x000E8AA8
			public override Set EnsureUniqueHierarchyMembers()
			{
				return new MdxCubeExpressionMdxCompiler2.SubSelectSet(this.compiler, base.MeasureAxis.EnsureUniqueHierarchyMembers(), base.VisibleAxis.EnsureUniqueHierarchyMembers(), this.SubSelect.EnsureUniqueHierarchyMembers(), base.Measures, base.Properties, base.MeasureProperties);
			}

			// Token: 0x0600458E RID: 17806 RVA: 0x000EA8E8 File Offset: 0x000E8AE8
			public override Set Unordered()
			{
				return new MdxCubeExpressionMdxCompiler2.SubSelectSet(this.compiler, base.MeasureAxis.Unordered(), base.VisibleAxis.Unordered(), this.SubSelect.Unordered(), base.Measures, base.Properties, base.MeasureProperties);
			}

			// Token: 0x0600458F RID: 17807 RVA: 0x000EA928 File Offset: 0x000E8B28
			protected override Set NewMeasuresAndProperties(IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measureProperties)
			{
				return new MdxCubeExpressionMdxCompiler2.SubSelectSet(this.compiler, base.MeasureAxis, base.VisibleAxis, this.SubSelect, measures, properties, measureProperties);
			}

			// Token: 0x040024D1 RID: 9425
			private readonly Set subSelect;

			// Token: 0x0200097C RID: 2428
			private sealed class SelectEverythingSet : EverythingSet
			{
				// Token: 0x06004590 RID: 17808 RVA: 0x000EA94A File Offset: 0x000E8B4A
				public SelectEverythingSet(MdxCubeExpressionMdxCompiler2 compiler)
				{
					this.compiler = compiler;
				}

				// Token: 0x06004591 RID: 17809 RVA: 0x000EA959 File Offset: 0x000E8B59
				public override Set IntersectAsLeft(Set other)
				{
					return new MdxCubeExpressionMdxCompiler2.SubSelectSet(this.compiler, other.Dimensionality).Intersect(other);
				}

				// Token: 0x06004592 RID: 17810 RVA: 0x000EA959 File Offset: 0x000E8B59
				public override Set IntersectAsRight(Set other)
				{
					return new MdxCubeExpressionMdxCompiler2.SubSelectSet(this.compiler, other.Dimensionality).Intersect(other);
				}

				// Token: 0x040024D2 RID: 9426
				private readonly MdxCubeExpressionMdxCompiler2 compiler;
			}
		}

		// Token: 0x0200097D RID: 2429
		protected class SingleSelectSet : MdxCubeExpressionMdxCompiler2.SelectSet
		{
			// Token: 0x06004593 RID: 17811 RVA: 0x000EA974 File Offset: 0x000E8B74
			public SingleSelectSet(MdxCubeExpressionMdxCompiler2 compiler, Dimensionality visibleAxisDimensionality)
				: this(compiler, EverythingSet.Instance, EverythingSet.Instance.ExpandTo(visibleAxisDimensionality), EverythingSet.Instance, EverythingSet.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance)
			{
			}

			// Token: 0x06004594 RID: 17812 RVA: 0x000EA9B1 File Offset: 0x000E8BB1
			public SingleSelectSet(MdxCubeExpressionMdxCompiler2 compiler, Set measureAxis, Set visibleAxis, Set offGrainVisibleAxis, Set slicerAxis, IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measureProperties)
				: base(compiler, measureAxis, visibleAxis, measures, properties, measureProperties)
			{
				this.offGrainVisibleAxis = offGrainVisibleAxis;
				this.slicerAxis = slicerAxis;
			}

			// Token: 0x17001626 RID: 5670
			// (get) Token: 0x06004595 RID: 17813 RVA: 0x000EA9D2 File Offset: 0x000E8BD2
			public override Set OffGrainVisibleAxis
			{
				get
				{
					return this.offGrainVisibleAxis;
				}
			}

			// Token: 0x17001627 RID: 5671
			// (get) Token: 0x06004596 RID: 17814 RVA: 0x000EA9DA File Offset: 0x000E8BDA
			public override Set SlicerAxis
			{
				get
				{
					return this.slicerAxis;
				}
			}

			// Token: 0x17001628 RID: 5672
			// (get) Token: 0x06004597 RID: 17815 RVA: 0x000EA786 File Offset: 0x000E8986
			public override Set SubSelect
			{
				get
				{
					return EverythingSet.Instance;
				}
			}

			// Token: 0x06004598 RID: 17816 RVA: 0x000EA9E4 File Offset: 0x000E8BE4
			protected override Set IntersectCommon(Set other)
			{
				Set set = base.MeasureAxis;
				Set set2 = base.VisibleAxis;
				Set set3 = this.OffGrainVisibleAxis;
				Set set4 = this.SlicerAxis;
				if (other.Dimensionality.HierarchyCount == 0)
				{
					set = set.Intersect(other);
				}
				else
				{
					bool flag = false;
					bool flag2 = false;
					foreach (ICubeHierarchy cubeHierarchy in other.Dimensionality.Hierarchies)
					{
						CubeLevelRange levelRange = other.Dimensionality.GetLevelRange(cubeHierarchy);
						CubeLevelRange cubeLevelRange;
						if (set2.Dimensionality.TryGetLevelRange(cubeHierarchy, out cubeLevelRange))
						{
							flag = true;
						}
						if (cubeLevelRange == null || levelRange.Fine.Number > cubeLevelRange.Fine.Number)
						{
							flag2 = true;
						}
					}
					if (flag && flag2)
					{
						set3 = set3.Intersect(other);
					}
					else if (flag)
					{
						set2 = set2.Intersect(other);
					}
					else
					{
						set4 = set4.Intersect(other);
					}
				}
				return new MdxCubeExpressionMdxCompiler2.SingleSelectSet(this.compiler, set, set2, set3, set4, base.Measures, base.Properties, base.MeasureProperties);
			}

			// Token: 0x06004599 RID: 17817 RVA: 0x000EAB00 File Offset: 0x000E8D00
			public override Set EnsureUniqueHierarchyMembers()
			{
				return new MdxCubeExpressionMdxCompiler2.SingleSelectSet(this.compiler, base.MeasureAxis.EnsureUniqueHierarchyMembers(), base.VisibleAxis.EnsureUniqueHierarchyMembers(), this.OffGrainVisibleAxis.EnsureUniqueHierarchyMembers(), this.SlicerAxis.EnsureUniqueHierarchyMembers(), base.Measures, base.Properties, base.MeasureProperties);
			}

			// Token: 0x0600459A RID: 17818 RVA: 0x000EAB58 File Offset: 0x000E8D58
			public override Set Unordered()
			{
				return new MdxCubeExpressionMdxCompiler2.SingleSelectSet(this.compiler, base.MeasureAxis.Unordered(), base.VisibleAxis.Unordered(), this.OffGrainVisibleAxis.Unordered(), this.SlicerAxis.Unordered(), base.Measures, base.Properties, base.MeasureProperties);
			}

			// Token: 0x0600459B RID: 17819 RVA: 0x000EABAE File Offset: 0x000E8DAE
			protected override Set NewMeasuresAndProperties(IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measureProperties)
			{
				return new MdxCubeExpressionMdxCompiler2.SingleSelectSet(this.compiler, base.MeasureAxis, base.VisibleAxis, this.OffGrainVisibleAxis, this.SlicerAxis, measures, properties, measureProperties);
			}

			// Token: 0x040024D3 RID: 9427
			private readonly Set offGrainVisibleAxis;

			// Token: 0x040024D4 RID: 9428
			private readonly Set slicerAxis;
		}
	}
}
