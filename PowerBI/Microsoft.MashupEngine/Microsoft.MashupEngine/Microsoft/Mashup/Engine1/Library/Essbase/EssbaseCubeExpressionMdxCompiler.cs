using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Essbase
{
	// Token: 0x02000C68 RID: 3176
	internal class EssbaseCubeExpressionMdxCompiler : MdxCubeExpressionMdxCompiler2
	{
		// Token: 0x0600562F RID: 22063 RVA: 0x0012AEEB File Offset: 0x001290EB
		public EssbaseCubeExpressionMdxCompiler(EssbaseCubeContextProvider contextProvider, IList<ParameterArguments> parameters)
			: base(contextProvider.Cube, true, false)
		{
			this.contextProvider = contextProvider;
			this.parameterArguments = parameters;
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x06005630 RID: 22064 RVA: 0x0012AF09 File Offset: 0x00129109
		protected override MdxFunction MemberCaption
		{
			get
			{
				return MdxFunction.MemberAlias;
			}
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x06005631 RID: 22065 RVA: 0x0012AF0D File Offset: 0x0012910D
		protected override MdxFunction InStr
		{
			get
			{
				return MdxFunction.InString;
			}
		}

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x06005632 RID: 22066 RVA: 0x0012AF11 File Offset: 0x00129111
		protected override MdxFunction UniqueName
		{
			get
			{
				return MdxFunction.MemberUniqueName;
			}
		}

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x06005633 RID: 22067 RVA: 0x0012AF15 File Offset: 0x00129115
		protected override ConstantMdxExpression One
		{
			get
			{
				return new EssbaseCubeExpressionMdxCompiler.SingleQuotedConstantMdxExpression(1);
			}
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x0012AF20 File Offset: 0x00129120
		protected override AxisMdxExpression CompileDimensionAxis(MdxExpression dimensionSet, MdxExpression nonDefaultMeasureSet, IEnumerable<IdentifierCubeExpression> properties)
		{
			List<string> list = new List<string> { "MEMBER_CAPTION", "MEMBER_UNIQUE_NAME" };
			list.AddRange(properties.Select((IdentifierCubeExpression property) => property.Identifier));
			return new AxisMdxExpression(true, dimensionSet, list.ToArray());
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x0012AF81 File Offset: 0x00129181
		protected override MdxCubeExpressionMdxCompiler2.MdxGenerator NewMdxGenerator()
		{
			return new EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator(this);
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x0012AF89 File Offset: 0x00129189
		protected override SetCompiler NewSetCompiler()
		{
			return new EssbaseCubeExpressionMdxCompiler.EssbaseSetCompiler(this);
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x0012AF94 File Offset: 0x00129194
		protected override bool TryCompileInequalityFilter(BinaryOperator2 inequalityOperator, MdxExpression left, MdxExpression right, out MdxExpression mdx)
		{
			MdxExpression mdxExpression = left.IsValid();
			MdxExpression mdxExpression2 = right.IsValid();
			mdx = mdxExpression.And(mdxExpression2).And(new BinaryMdxExpression(inequalityOperator, left, right));
			if (inequalityOperator == BinaryOperator2.GreaterThanOrEquals || inequalityOperator == BinaryOperator2.LessThanOrEquals)
			{
				MdxExpression mdxExpression3;
				if (base.TrySimplifyEqualityExpression(left, right, out mdxExpression3))
				{
					mdx = mdx.Or(mdxExpression3);
				}
				else
				{
					mdx = mdxExpression.Not().And(mdxExpression2.Not()).Or(mdx);
				}
			}
			return true;
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x0012B008 File Offset: 0x00129208
		protected override bool TryCompileValueEqualityExpression(MdxExpression left, MdxExpression right, out MdxExpression mdx)
		{
			if (base.TrySimplifyEqualityExpression(left, right, out mdx))
			{
				return true;
			}
			MdxExpression mdxExpression = left.IsValid();
			MdxExpression mdxExpression2 = right.IsValid();
			InvocationMdxExpression invocationMdxExpression = left as InvocationMdxExpression;
			ConstantMdxExpression constantMdxExpression = right as ConstantMdxExpression;
			if (invocationMdxExpression != null && constantMdxExpression != null && constantMdxExpression.Type == MdxConstantType.String && invocationMdxExpression.Function == MdxFunction.MemberUniqueName)
			{
				right = new EssbaseCubeExpressionMdxCompiler.EssbaseMemberUniqueNameConstantMdxExpression((string)constantMdxExpression.Value);
			}
			mdx = mdxExpression.Not().And(mdxExpression2.Not()).Or(mdxExpression.And(mdxExpression2).And(left.Equals(right)));
			return true;
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x0012B098 File Offset: 0x00129298
		protected override bool TryCompile(QueryCubeExpression expression, RowRange rowRange, out MdxExpression mdx, out Dictionary<string, string> aliases)
		{
			if (!base.TryCompile(expression, rowRange, out mdx, out aliases))
			{
				return false;
			}
			SelectMdxExpression selectMdxExpression = mdx as SelectMdxExpression;
			if (selectMdxExpression == null || selectMdxExpression.Where == null)
			{
				return true;
			}
			List<MdxDeclaration> list = new List<MdxDeclaration>();
			if (expression.Measures.Count == 0)
			{
				list.AddRange(base.MeasureOfOneDeclaration);
				expression = new QueryCubeExpression(expression.From, expression.DimensionAttributes, expression.Properties, new IdentifierCubeExpression[] { base.MeasureOfOneCubeExpression }, expression.MeasureProperties, expression.Filter, expression.Sort, expression.RowRange);
			}
			Set set;
			if (this.NewSetCompiler().TryCompile(expression, out set))
			{
				try
				{
					MdxCubeExpressionMdxCompiler2.SingleSelectSet singleSelectSet = set as MdxCubeExpressionMdxCompiler2.SingleSelectSet;
					if (singleSelectSet != null && singleSelectSet.SlicerAxis.Kind != SetKind.Everything)
					{
						MdxHierarchy mdxHierarchy = singleSelectSet.SlicerAxis.Dimensionality.Hierarchies.First<ICubeHierarchy>() as MdxHierarchy;
						ICubeLevel cubeLevel = mdxHierarchy.Levels[mdxHierarchy.Levels.Count - 1];
						string text = mdxHierarchy.MdxIdentifier + ".[Microsoft.Mashup.Engine.FilterContext]";
						IdentifierMdxExpression identifierMdxExpression = new IdentifierMdxExpression(text);
						MdxDeclaration[] array = new MdxDeclaration[]
						{
							new MdxMemberDeclaration(identifierMdxExpression, new EssbaseCubeExpressionMdxCompiler.SingleQuotedInvocationMdxExpression(MdxFunction.Aggregate, new MdxExpression[] { EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.GetSetExpression(selectMdxExpression.Where) }), false)
						};
						list.AddRange(array);
						TextValue textValue = TextValue.New(text);
						MemberSet memberSet = new MemberSet(cubeLevel, textValue);
						set = new MdxCubeExpressionMdxCompiler2.SingleSelectSet(this, singleSelectSet.MeasureAxis, singleSelectSet.VisibleAxis.CrossJoin(memberSet), singleSelectSet.OffGrainVisibleAxis, EverythingSet.Instance, singleSelectSet.Measures, singleSelectSet.Properties, singleSelectSet.MeasureProperties);
						set = set.SkipTake(rowRange);
						aliases = new Dictionary<string, string>();
						SelectMdxExpression selectMdxExpression2 = this.NewMdxGenerator().GenerateMdx(set, list, aliases) as SelectMdxExpression;
						if (selectMdxExpression2 != null)
						{
							mdx = new SelectMdxExpression(list.ToArray(), selectMdxExpression2.Axes, selectMdxExpression2.From, selectMdxExpression2.Where, selectMdxExpression2.CellProperties);
							return true;
						}
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

		// Token: 0x0400308A RID: 12426
		protected const string FilterContextName = ".[Microsoft.Mashup.Engine.FilterContext]";

		// Token: 0x0400308B RID: 12427
		private readonly EssbaseCubeContextProvider contextProvider;

		// Token: 0x0400308C RID: 12428
		private readonly IList<ParameterArguments> parameterArguments;

		// Token: 0x02000C69 RID: 3177
		private class EssbaseMdxGenerator : MdxCubeExpressionMdxCompiler2.MdxGenerator
		{
			// Token: 0x0600563A RID: 22074 RVA: 0x00072EB6 File Offset: 0x000710B6
			public EssbaseMdxGenerator(MdxCubeExpressionMdxCompiler2 compiler)
				: base(compiler)
			{
			}

			// Token: 0x0600563B RID: 22075 RVA: 0x0012B2B4 File Offset: 0x001294B4
			protected override MdxCubeExpressionMdxCompiler2.MdxGenerator.Visitor CreateVisitor()
			{
				return new EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.EssbaseVisitor(this);
			}

			// Token: 0x0600563C RID: 22076 RVA: 0x0012B2BC File Offset: 0x001294BC
			public override IdentifierMdxExpression NewIdentifier(string identifier)
			{
				if (EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.sharedMemberSyntaxRegex.IsMatch(identifier))
				{
					identifier = identifier.Substring(1, identifier.Length - 2);
				}
				return base.NewIdentifier(identifier);
			}

			// Token: 0x0600563D RID: 22077 RVA: 0x0012B2E3 File Offset: 0x001294E3
			public override AxisMdxExpression NewMeasureAxis(MdxExpression measureSet)
			{
				return new AxisMdxExpression(true, measureSet, Array.Empty<string>());
			}

			// Token: 0x0600563E RID: 22078 RVA: 0x0000A6A5 File Offset: 0x000088A5
			protected override MdxExpression NewAddCalculatedMembers(MdxExpression mdxExpression)
			{
				return mdxExpression;
			}

			// Token: 0x0600563F RID: 22079 RVA: 0x0012B2F1 File Offset: 0x001294F1
			public override MdxExpression NewFilter(MdxExpression set, MdxExpression expression)
			{
				return base.NewFilter(EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.GetSetExpression(set), expression);
			}

			// Token: 0x06005640 RID: 22080 RVA: 0x0012B300 File Offset: 0x00129500
			protected override MdxDeclaration NewSetDeclaration(IdentifierMdxExpression identifier, MdxExpression definition, bool isSingledQuoted = false)
			{
				return base.NewSetDeclaration(identifier, definition, true);
			}

			// Token: 0x06005641 RID: 22081 RVA: 0x0012B30B File Offset: 0x0012950B
			protected override MdxDeclaration NewMemberDeclaration(IdentifierMdxExpression identifier, MdxExpression definition, bool isSingledQuoted = false)
			{
				return base.NewMemberDeclaration(identifier, definition, true);
			}

			// Token: 0x06005642 RID: 22082 RVA: 0x0012B316 File Offset: 0x00129516
			public static MdxExpression GetSetExpression(MdxExpression set)
			{
				if (set != null && !EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.ExpressionReturnsSet(set))
				{
					return new SetMdxExpression(new MdxExpression[] { set });
				}
				return set;
			}

			// Token: 0x06005643 RID: 22083 RVA: 0x0012B334 File Offset: 0x00129534
			private static bool ExpressionReturnsSet(MdxExpression set)
			{
				return !(set is IdentifierMdxExpression) && (!(set is InvocationMdxExpression) || EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.FunctionReturnsSet(((InvocationMdxExpression)set).Function));
			}

			// Token: 0x06005644 RID: 22084 RVA: 0x0012B35C File Offset: 0x0012955C
			private static bool FunctionReturnsSet(MdxFunction function)
			{
				switch (function)
				{
				case MdxFunction.Crossjoin:
				case MdxFunction.Filter:
				case MdxFunction.Subset:
				case MdxFunction.Intersect:
				case MdxFunction.Union:
				case MdxFunction.Order:
				case MdxFunction.Except:
				case MdxFunction.Descendants:
				case MdxFunction.Distinct:
				case MdxFunction.AddCalculatedMembers:
				case MdxFunction.Generate:
				case MdxFunction.IIf:
					break;
				case MdxFunction.IsEmpty:
				case MdxFunction.VbaCDate:
				case MdxFunction.NonEmpty:
				case MdxFunction.Ancestor:
				case MdxFunction.Aggregate:
				case MdxFunction.Count:
				case MdxFunction.InStr:
				case MdxFunction.InString:
					return false;
				default:
					if (function - MdxFunction.AllMembers > 1)
					{
						return false;
					}
					break;
				}
				return true;
			}

			// Token: 0x0400308D RID: 12429
			private static readonly Regex sharedMemberSyntaxRegex = new Regex("\\[\\[[^\\[\\]]+\\]\\.\\[[^\\[\\]]+\\]\\]", RegexOptions.Compiled);

			// Token: 0x02000C6A RID: 3178
			private sealed class EssbaseVisitor : MdxCubeExpressionMdxCompiler2.MdxGenerator.Visitor
			{
				// Token: 0x06005646 RID: 22086 RVA: 0x0012B3DB File Offset: 0x001295DB
				public EssbaseVisitor(MdxCubeExpressionMdxCompiler2.MdxGenerator generator)
					: base(generator)
				{
				}

				// Token: 0x06005647 RID: 22087 RVA: 0x0012B3E4 File Offset: 0x001295E4
				protected override MdxExpression NewCrossJoin(MdxExpression[] sets)
				{
					for (int i = 0; i < sets.Length; i++)
					{
						sets[i] = EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.GetSetExpression(sets[i]);
					}
					return base.NewCrossJoin(sets);
				}

				// Token: 0x06005648 RID: 22088 RVA: 0x0012B411 File Offset: 0x00129611
				protected override MdxExpression NewDistinct(MdxExpression set)
				{
					return base.NewDistinct(EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.GetSetExpression(set));
				}

				// Token: 0x06005649 RID: 22089 RVA: 0x0012B41F File Offset: 0x0012961F
				protected override MdxExpression NewExcept(MdxExpression set, MdxExpression except)
				{
					return base.NewExcept(EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.GetSetExpression(set), EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.GetSetExpression(except));
				}

				// Token: 0x0600564A RID: 22090 RVA: 0x0012B433 File Offset: 0x00129633
				protected override MdxExpression NewFilter(MdxExpression set, MdxExpression expression)
				{
					return base.NewFilter(EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.GetSetExpression(set), expression);
				}

				// Token: 0x0600564B RID: 22091 RVA: 0x0012B442 File Offset: 0x00129642
				protected override MdxExpression NewIntersect(MdxExpression set1, MdxExpression set2)
				{
					return base.NewIntersect(EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.GetSetExpression(set1), EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.GetSetExpression(set2));
				}

				// Token: 0x0600564C RID: 22092 RVA: 0x0012B458 File Offset: 0x00129658
				protected override MdxExpression NewDescendTo(MdxExpression set, Dimensionality from, Dimensionality to)
				{
					if (to.HierarchyCount == 1)
					{
						MdxHierarchy mdxHierarchy = (MdxHierarchy)from.Hierarchies.Single<ICubeHierarchy>();
						MdxHierarchy mdxHierarchy2 = (MdxHierarchy)to.Hierarchies.Single<ICubeHierarchy>();
						MdxLevel mdxLevel = (MdxLevel)to.GetLevelRange(mdxHierarchy2).Fine;
						return this.NewGenerate(set, base.NewDescendants(base.NewCurrentMember(base.Generator.NewIdentifier(mdxHierarchy.MdxIdentifier)), base.Generator.NewIdentifier(mdxLevel.MdxIdentifier)));
					}
					return base.NewDescendToMultipleHierarchies(set, from, to);
				}

				// Token: 0x0600564D RID: 22093 RVA: 0x0012B4E1 File Offset: 0x001296E1
				protected override MdxExpression NewGenerate(MdxExpression mdxExpression, MdxExpression generateSet)
				{
					return new InvocationMdxExpression(MdxFunction.Generate, new MdxExpression[]
					{
						EssbaseCubeExpressionMdxCompiler.EssbaseMdxGenerator.GetSetExpression(mdxExpression),
						generateSet
					});
				}

				// Token: 0x0600564E RID: 22094 RVA: 0x0012B500 File Offset: 0x00129700
				protected override MdxExpression NewMember(ICubeLevel level, Value member)
				{
					if (member.Equals(MdxCubeExpressionMdxCompiler2.CurrentMemberSet.CurrentMember))
					{
						MdxLevel mdxLevel = (MdxLevel)level;
						return base.NewCurrentMember(base.Generator.NewIdentifier(mdxLevel.Hierarchy.MdxIdentifier));
					}
					return base.Generator.NewIdentifier(member.AsString);
				}
			}
		}

		// Token: 0x02000C6B RID: 3179
		private class EssbaseMemberUniqueNameConstantMdxExpression : ConstantMdxExpression
		{
			// Token: 0x0600564F RID: 22095 RVA: 0x0012B54F File Offset: 0x0012974F
			public EssbaseMemberUniqueNameConstantMdxExpression(string value)
				: base(value)
			{
			}

			// Token: 0x06005650 RID: 22096 RVA: 0x0012B558 File Offset: 0x00129758
			public override void Write(MdxExpressionWriter writer)
			{
				if (base.Type == MdxConstantType.String)
				{
					string text = "StrToMbr(\"" + ((string)base.Value).Replace("\"", "\"\"") + "\").MEMBER_UNIQUE_NAME";
					writer.Write(text);
					return;
				}
				base.Write(writer);
			}
		}

		// Token: 0x02000C6C RID: 3180
		private class SingleQuotedConstantMdxExpression : ConstantMdxExpression
		{
			// Token: 0x06005651 RID: 22097 RVA: 0x0012B54F File Offset: 0x0012974F
			public SingleQuotedConstantMdxExpression(string value)
				: base(value)
			{
			}

			// Token: 0x06005652 RID: 22098 RVA: 0x0012B5A7 File Offset: 0x001297A7
			public SingleQuotedConstantMdxExpression(int value)
				: base(value)
			{
			}

			// Token: 0x06005653 RID: 22099 RVA: 0x0012B5B0 File Offset: 0x001297B0
			public SingleQuotedConstantMdxExpression(double dbl)
				: base(dbl)
			{
			}

			// Token: 0x06005654 RID: 22100 RVA: 0x0012B5B9 File Offset: 0x001297B9
			public SingleQuotedConstantMdxExpression(decimal dec)
				: base(dec)
			{
			}

			// Token: 0x06005655 RID: 22101 RVA: 0x0012B5C4 File Offset: 0x001297C4
			public override void Write(MdxExpressionWriter writer)
			{
				switch (base.Type)
				{
				case MdxConstantType.Int32:
				case MdxConstantType.Double:
				case MdxConstantType.Decimal:
					writer.Write(string.Format(CultureInfo.InvariantCulture, "'{0}'", base.Value));
					return;
				case MdxConstantType.String:
				{
					string text = "'" + ((string)base.Value).Replace("'", "''") + "'";
					writer.Write(text);
					return;
				}
				}
				throw new InvalidOperationException(base.Type.ToString());
			}
		}

		// Token: 0x02000C6D RID: 3181
		private class SingleQuotedInvocationMdxExpression : InvocationMdxExpression
		{
			// Token: 0x06005656 RID: 22102 RVA: 0x0012B65B File Offset: 0x0012985B
			public SingleQuotedInvocationMdxExpression(MdxFunction function, params MdxExpression[] arguments)
				: base(function, arguments)
			{
			}

			// Token: 0x06005657 RID: 22103 RVA: 0x0012B665 File Offset: 0x00129865
			public override void Write(MdxExpressionWriter writer)
			{
				writer.Write("'");
				base.Write(writer);
				writer.Write("'");
			}
		}

		// Token: 0x02000C6E RID: 3182
		private class EssbaseSetCompiler : MdxCubeExpressionMdxCompiler2.MdxSetCompiler
		{
			// Token: 0x06005658 RID: 22104 RVA: 0x00072D75 File Offset: 0x00070F75
			public EssbaseSetCompiler(EssbaseCubeExpressionMdxCompiler compiler)
				: base(compiler)
			{
			}

			// Token: 0x06005659 RID: 22105 RVA: 0x00072D8B File Offset: 0x00070F8B
			protected override Set NewSelect(Dimensionality visibleDimensionality)
			{
				return new MdxCubeExpressionMdxCompiler2.SingleSelectSet(this.compiler, visibleDimensionality);
			}
		}
	}
}
