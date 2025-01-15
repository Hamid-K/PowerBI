using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Mdx;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004A3 RID: 1187
	internal sealed class SapBwCubeExpressionMdxCompiler : MdxCubeExpressionMdxCompiler2
	{
		// Token: 0x06002716 RID: 10006 RVA: 0x00072228 File Offset: 0x00070428
		public SapBwCubeExpressionMdxCompiler(SapBwCubeContextProvider contextProvider, IList<ParameterArguments> parameters)
			: base(contextProvider.Cube, true, !contextProvider.Service.ScaleMeasures)
		{
			this.contextProvider = contextProvider;
			this.parameterArguments = parameters;
			this.objectCache = contextProvider.Service.Host.QueryService<ICacheSets>().Data.PersistentObjectCache;
			this.removeNotEmptyVisitor = new SapBwCubeExpressionMdxCompiler.RemoveNotEmptyVisitor();
			this.captionToUniqueIdVisitor = new SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor(this);
			this.dateFilterVisitor = new SapBwCubeExpressionMdxCompiler.DateTimeFilterVisitor(this);
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x000722A1 File Offset: 0x000704A1
		private SapBwMdxCube Cube
		{
			get
			{
				return (SapBwMdxCube)this.cube;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06002718 RID: 10008 RVA: 0x000722AE File Offset: 0x000704AE
		private IList<SapBwMdxVariableExpression> Variables
		{
			get
			{
				if (this.variables == null)
				{
					this.variables = this.CompileVariables();
				}
				return this.variables;
			}
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000722CC File Offset: 0x000704CC
		public override bool CanCompile(QueryCubeExpression expression, out Dictionary<string, string> aliases)
		{
			bool flag = this.canCompile;
			bool flag2;
			try
			{
				this.canCompile = true;
				flag2 = base.CanCompile(expression, out aliases);
			}
			finally
			{
				this.canCompile = flag;
			}
			return flag2;
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x0007230C File Offset: 0x0007050C
		protected override bool TryCompile(QueryCubeExpression expression, RowRange rowRange, out MdxExpression mdx, out Dictionary<string, string> aliases)
		{
			this.orderByDeclarations = new List<MdxDeclaration>();
			if (base.TryCompile(expression, rowRange, out mdx, out aliases))
			{
				SelectMdxExpression selectMdxExpression = null;
				SelectMdxExpression selectMdxExpression2 = mdx as SelectMdxExpression;
				if (selectMdxExpression2 != null)
				{
					selectMdxExpression = selectMdxExpression2.From as SelectMdxExpression;
				}
				if (selectMdxExpression == null || this.Variables.Count == 0)
				{
					mdx = new SapBwMdxSelectExpression(mdx, this.Variables);
					return true;
				}
			}
			mdx = null;
			aliases = null;
			return false;
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x00072373 File Offset: 0x00070573
		protected override MdxExpression CompileDateConstant(DateTime dateTime)
		{
			return new ConstantMdxExpression(dateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x0007238B File Offset: 0x0007058B
		protected override bool TryCompileConstant(ConstantCubeExpression constant, out MdxExpression mdx)
		{
			if (constant.Value.Kind == ValueKind.Time)
			{
				mdx = new ConstantMdxExpression(constant.Value.AsTime.ToString("HHmmss", CultureInfo.InvariantCulture));
				return true;
			}
			return base.TryCompileConstant(constant, out mdx);
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x000723C8 File Offset: 0x000705C8
		protected override AxisMdxExpression CompileDimensionAxis(MdxExpression dimensionSet, MdxExpression nonDefaultMeasureSet, IEnumerable<IdentifierCubeExpression> properties)
		{
			bool flag = nonDefaultMeasureSet != null;
			List<string> list = new List<string> { "MEMBER_CAPTION", "MEMBER_UNIQUE_NAME" };
			list.AddRange(properties.Select((IdentifierCubeExpression property) => property.Identifier));
			return new AxisMdxExpression(flag, dimensionSet, list.ToArray());
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x0007242C File Offset: 0x0007062C
		protected override string GetPropertyIdentifier(MdxProperty property)
		{
			return property.Level.Hierarchy.Dimension.MdxIdentifier + "." + property.Name;
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x00072453 File Offset: 0x00070653
		protected override MdxCubeExpressionMdxCompiler2.MdxGenerator NewMdxGenerator()
		{
			return new SapBwCubeExpressionMdxCompiler.SapBwMdxGenerator(this);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x0007245B File Offset: 0x0007065B
		protected override SetCompiler NewSetCompiler()
		{
			return new SapBwCubeExpressionMdxCompiler.SapBwSetCompiler(this);
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x00072464 File Offset: 0x00070664
		private bool HasMultipleHierarchiesFromOneDimension(QueryCubeExpression expression)
		{
			Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>();
			HashSet<string> hashSet = null;
			foreach (IdentifierCubeExpression identifierCubeExpression in expression.DimensionAttributes)
			{
				MdxLevel mdxLevel = this.cube.GetObject(identifierCubeExpression.Identifier) as MdxLevel;
				if (mdxLevel != null)
				{
					MdxHierarchy hierarchy = mdxLevel.Hierarchy;
					string mdxIdentifier = hierarchy.Dimension.MdxIdentifier;
					if (!dictionary.TryGetValue(mdxIdentifier, out hashSet))
					{
						hashSet = new HashSet<string>();
						dictionary[mdxIdentifier] = hashSet;
					}
					hashSet.Add(hierarchy.UniqueIdentifier);
					if (hashSet.Count > 1)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x00072520 File Offset: 0x00070720
		protected override void VerifyNonVisibleSetIsSupported(Set set)
		{
			foreach (Set set2 in set.GetSubsets())
			{
				if (set2 is UnionSet && set2.Dimensionality.HierarchyCount > 1)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x00072584 File Offset: 0x00070784
		private bool RequiresMoreThanOneHierarchy(QueryCubeExpression expression)
		{
			MdxHierarchy mdxHierarchy = null;
			bool flag = false;
			foreach (IdentifierCubeExpression identifierCubeExpression in expression.DimensionAttributes)
			{
				flag = true;
				MdxHierarchy hierarchy = ((MdxLevel)this.Cube.GetObject(identifierCubeExpression.Identifier)).Hierarchy;
				if (mdxHierarchy != null && !mdxHierarchy.Equals(hierarchy))
				{
					mdxHierarchy = null;
					break;
				}
				mdxHierarchy = hierarchy;
			}
			if (expression.Filter != null && (mdxHierarchy != null || !flag))
			{
				foreach (IdentifierCubeExpression identifierCubeExpression2 in expression.Filter.GetReferences())
				{
					MdxCubeObject @object = this.contextProvider.Cube.GetObject(identifierCubeExpression2.Identifier);
					MdxCubeObjectKind kind = @object.Kind;
					MdxHierarchy mdxHierarchy2;
					if (kind != MdxCubeObjectKind.Level)
					{
						if (kind != MdxCubeObjectKind.Property)
						{
							mdxHierarchy2 = null;
						}
						else
						{
							mdxHierarchy2 = ((MdxProperty)@object).Level.Hierarchy;
						}
					}
					else
					{
						mdxHierarchy2 = ((MdxLevel)@object).Hierarchy;
					}
					if (mdxHierarchy2 != null)
					{
						flag = true;
						if (mdxHierarchy != null && !mdxHierarchy.Equals(mdxHierarchy2))
						{
							mdxHierarchy = null;
							break;
						}
						mdxHierarchy = mdxHierarchy2;
					}
				}
			}
			return mdxHierarchy == null && flag;
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000726C8 File Offset: 0x000708C8
		private IList<SapBwMdxVariableExpression> CompileVariables()
		{
			List<SapBwMdxVariableExpression> list = new List<SapBwMdxVariableExpression>();
			for (int i = 0; i < this.parameterArguments.Count; i++)
			{
				ParameterArguments parameterArguments = this.parameterArguments[i];
				SapBwVariable sapBwVariable = this.Cube.Variables[parameterArguments.Parameter.Identifier];
				list.AddRange(this.CompileVariable(sapBwVariable, parameterArguments));
			}
			return list;
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x0007272C File Offset: 0x0007092C
		private IList<SapBwMdxVariableExpression> CompileVariable(SapBwVariable variable, ParameterArguments arguments)
		{
			switch (variable.SelectionType)
			{
			case SapBwVariableSelectionType.Value:
				return this.CompileSingleValueVariable(variable, arguments);
			case SapBwVariableSelectionType.Interval:
				return this.CompileIntervalVariable(variable, arguments);
			case SapBwVariableSelectionType.SelectionOption:
				return this.CompileSelectionOptionVariable(variable, arguments);
			case SapBwVariableSelectionType.SeveralSingleValues:
				return this.CompileSeveralSingleValuesVariable(variable, arguments);
			default:
				throw new InvalidOperationException(variable.SelectionType.ToString());
			}
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x00072798 File Offset: 0x00070998
		private IList<SapBwMdxVariableExpression> CompileSingleValueVariable(SapBwVariable variable, ParameterArguments arguments)
		{
			return new SapBwMdxVariableExpression[]
			{
				new SapBwMdxVariableExpression(new IdentifierMdxExpression(variable.MdxIdentifier), SapBwMdxVariableExpressionSign.Including, this.CompileVariableValue(variable, arguments.Values[0]))
			};
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000727D0 File Offset: 0x000709D0
		private IList<SapBwMdxVariableExpression> CompileIntervalVariable(SapBwVariable variable, ParameterArguments arguments)
		{
			return new SapBwMdxVariableExpression[]
			{
				new SapBwMdxVariableExpression(new IdentifierMdxExpression(variable.MdxIdentifier), SapBwMdxVariableExpressionSign.Including, this.CompileInterval(variable, arguments.Values[0], arguments.Values[1]))
			};
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x00072810 File Offset: 0x00070A10
		private IList<SapBwMdxVariableExpression> CompileSeveralSingleValuesVariable(SapBwVariable variable, ParameterArguments arguments)
		{
			ListValue asList = arguments.Values[0].AsList;
			List<SapBwMdxVariableExpression> list = new List<SapBwMdxVariableExpression>();
			foreach (IValueReference valueReference in asList)
			{
				MdxExpression mdxExpression = this.CompileVariableValue(variable, valueReference.Value);
				list.Add(new SapBwMdxVariableExpression(new IdentifierMdxExpression(variable.MdxIdentifier), SapBwMdxVariableExpressionSign.Including, mdxExpression));
			}
			return list;
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x0007288C File Offset: 0x00070A8C
		private IList<SapBwMdxVariableExpression> CompileSelectionOptionVariable(SapBwVariable variable, ParameterArguments arguments)
		{
			ListValue asList = arguments.Values[0].AsList;
			List<SapBwMdxVariableExpression> list = new List<SapBwMdxVariableExpression>();
			foreach (IValueReference valueReference in asList)
			{
				Value value = valueReference.Value;
				if (value.IsList)
				{
					if (value.AsList.Count != 2)
					{
						throw new NotSupportedException();
					}
					MdxExpression mdxExpression = this.CompileInterval(variable, value[0], value[1]);
					list.Add(new SapBwMdxVariableExpression(new IdentifierMdxExpression(variable.MdxIdentifier), SapBwMdxVariableExpressionSign.Including, mdxExpression));
				}
				else
				{
					MdxExpression mdxExpression2 = this.CompileVariableValue(variable, value);
					list.Add(new SapBwMdxVariableExpression(new IdentifierMdxExpression(variable.MdxIdentifier), SapBwMdxVariableExpressionSign.Including, mdxExpression2));
				}
			}
			return list;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x00072958 File Offset: 0x00070B58
		private MdxExpression CompileInterval(SapBwVariable variable, Value start, Value end)
		{
			return new SapBwMdxVariableRangeExpression(this.CompileVariableValue(variable, start), this.CompileVariableValue(variable, end));
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x00072970 File Offset: 0x00070B70
		private MdxExpression CompileVariableValue(SapBwVariable variable, Value value)
		{
			if (variable.Type != SapBwVariableType.Text && variable.Type != SapBwVariableType.Formula)
			{
				if (variable.IsDate && value.IsDate && MdxIdentifier.IsValidIdentifier(variable.Dimension))
				{
					string text;
					value = TextValue.New(string.Format(CultureInfo.InvariantCulture, "{0}.{1}", MdxIdentifier.TryQuoteValue(variable.Dimension, out text) ? text : variable.Dimension, MdxIdentifier.QuotePart(value.AsDate.AsClrDateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture))));
				}
				else if (variable.Type == SapBwVariableType.Hierarchy)
				{
					try
					{
						value = TextValue.New(MdxIdentifier.QuotePart(SapBwIdentifier.Parse(value.AsString).HierarchyName));
					}
					catch (FormatException)
					{
						throw ValueException.NewDataFormatError<Message0>(Strings.SapBwParameterValueNotValid, value, null);
					}
				}
				return this.CompileVariableIdentifierValue(value.AsText);
			}
			MdxExpression mdxExpression;
			if (!this.TryCompileConstant(new ConstantCubeExpression(value), out mdxExpression))
			{
				throw new NotSupportedException();
			}
			return mdxExpression;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x00072A68 File Offset: 0x00070C68
		private MdxExpression CompileVariableIdentifierValue(TextValue value)
		{
			string text;
			MdxIdentifier mdxIdentifier;
			if (!MdxIdentifier.TryGetQuotedValue(value, out text) || !MdxIdentifier.TryParse(text, out mdxIdentifier))
			{
				throw ValueException.NewExpressionError<Message0>(Strings.SapBwParameterValueNotValid, value, null);
			}
			return new IdentifierMdxExpression(text);
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x00072A9C File Offset: 0x00070C9C
		private bool TryGetCaptionUniqueIds(IdentifierCubeExpression dimensionAttribute, SapBwCubeExpressionMdxCompiler.CaptionFilterKind filterKind, Value filterValue, out ConstantCubeExpression[] uniqueIds)
		{
			string text = PersistentCacheKey.SapBw.Qualify(new string[]
			{
				this.contextProvider.Service.ConnectionString,
				"CaptionUniqueIds",
				this.Cube.MdxIdentifier,
				dimensionAttribute.Identifier,
				filterKind.ToString(),
				filterValue.IsText.ToString(),
				filterValue.IsText ? filterValue.AsString : string.Empty
			});
			object obj;
			if (this.objectCache.TryGetValue(text, (Stream s) => SapBwCubeExpressionMdxCompiler.DeserializeUniqueIds(s), out obj))
			{
				uniqueIds = (ConstantCubeExpression[])obj;
				return true;
			}
			CubeExpression cubeExpression;
			switch (filterKind)
			{
			case SapBwCubeExpressionMdxCompiler.CaptionFilterKind.Equals:
				cubeExpression = new BinaryCubeExpression(BinaryOperator2.Equals, dimensionAttribute, new ConstantCubeExpression(filterValue));
				goto IL_0145;
			case SapBwCubeExpressionMdxCompiler.CaptionFilterKind.Contains:
				if (filterValue.IsText)
				{
					cubeExpression = new InvocationCubeExpression(new ConstantCubeExpression(Library.Text.Contains), new CubeExpression[]
					{
						dimensionAttribute,
						new ConstantCubeExpression(filterValue)
					});
					goto IL_0145;
				}
				break;
			case SapBwCubeExpressionMdxCompiler.CaptionFilterKind.StartsWith:
				if (filterValue.IsText)
				{
					cubeExpression = new InvocationCubeExpression(new ConstantCubeExpression(Library.Text.StartsWith), new CubeExpression[]
					{
						dimensionAttribute,
						new ConstantCubeExpression(filterValue)
					});
					goto IL_0145;
				}
				break;
			}
			throw new InvalidOperationException();
			IL_0145:
			IdentifierCubeExpression property = this.contextProvider.GetProperty(dimensionAttribute, CubePropertyKind.UniqueId, null);
			QueryCubeExpression queryCubeExpression = new QueryCubeExpression(new IdentifierCubeExpression(this.Cube.MdxIdentifier), new IdentifierCubeExpression[] { dimensionAttribute }, new IdentifierCubeExpression[] { property }, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, cubeExpression, EmptyArray<CubeSortOrder>.Instance, RowRange.All);
			uniqueIds = EmptyArray<ConstantCubeExpression>.Instance;
			int num = 0;
			while (uniqueIds.Length == 0 && num < 2)
			{
				CubeContext cubeContext;
				if (this.contextProvider.TryCreateContext(queryCubeExpression, this.parameterArguments, false, num > 0, out cubeContext))
				{
					using (IEnumerator<IValueReference> enumerator = cubeContext.Evaluate())
					{
						while (enumerator.MoveNext())
						{
							IValueReference valueReference = enumerator.Current;
							ConstantCubeExpression constantCubeExpression = new ConstantCubeExpression(valueReference.Value.AsRecord[property.Identifier]);
							uniqueIds = uniqueIds.Add(constantCubeExpression);
						}
						goto IL_021A;
					}
					goto IL_0214;
					IL_021A:
					num++;
					continue;
				}
				IL_0214:
				throw new NotSupportedException();
			}
			this.objectCache.CommitValue(text, delegate(Stream s, object o)
			{
				SapBwCubeExpressionMdxCompiler.SerializeUniqueIds(s, (ConstantCubeExpression[])o);
			}, uniqueIds);
			return true;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x00072D1C File Offset: 0x00070F1C
		private static ConstantCubeExpression[] DeserializeUniqueIds(Stream s)
		{
			return new BinaryReader(s).ReadArray((BinaryReader r) => new ConstantCubeExpression(TextValue.New(r.ReadString())));
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x00072D48 File Offset: 0x00070F48
		private static void SerializeUniqueIds(Stream s, ConstantCubeExpression[] uniqueIds)
		{
			new BinaryWriter(s).WriteArray(uniqueIds, delegate(BinaryWriter w, ConstantCubeExpression id)
			{
				w.WriteString(id.Value.AsString);
			});
		}

		// Token: 0x04001071 RID: 4209
		private const int captionFilterRetryCount = 2;

		// Token: 0x04001072 RID: 4210
		private readonly SapBwCubeContextProvider contextProvider;

		// Token: 0x04001073 RID: 4211
		private readonly IList<ParameterArguments> parameterArguments;

		// Token: 0x04001074 RID: 4212
		private readonly IPersistentObjectCache objectCache;

		// Token: 0x04001075 RID: 4213
		private readonly SapBwCubeExpressionMdxCompiler.RemoveNotEmptyVisitor removeNotEmptyVisitor;

		// Token: 0x04001076 RID: 4214
		private readonly SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor captionToUniqueIdVisitor;

		// Token: 0x04001077 RID: 4215
		private readonly SapBwCubeExpressionMdxCompiler.DateTimeFilterVisitor dateFilterVisitor;

		// Token: 0x04001078 RID: 4216
		private IList<SapBwMdxVariableExpression> variables;

		// Token: 0x04001079 RID: 4217
		private bool canCompile;

		// Token: 0x020004A4 RID: 1188
		private class SapBwSetCompiler : MdxCubeExpressionMdxCompiler2.MdxSetCompiler
		{
			// Token: 0x06002730 RID: 10032 RVA: 0x00072D75 File Offset: 0x00070F75
			public SapBwSetCompiler(SapBwCubeExpressionMdxCompiler compiler)
				: base(compiler)
			{
			}

			// Token: 0x17000F78 RID: 3960
			// (get) Token: 0x06002731 RID: 10033 RVA: 0x00072D7E File Offset: 0x00070F7E
			private SapBwCubeExpressionMdxCompiler Compiler
			{
				get
				{
					return (SapBwCubeExpressionMdxCompiler)this.compiler;
				}
			}

			// Token: 0x06002732 RID: 10034 RVA: 0x00072D8B File Offset: 0x00070F8B
			protected override Set NewSelect(Dimensionality visibleDimensionality)
			{
				return new MdxCubeExpressionMdxCompiler2.SingleSelectSet(this.compiler, visibleDimensionality);
			}

			// Token: 0x06002733 RID: 10035 RVA: 0x00072D9C File Offset: 0x00070F9C
			protected override bool TryCompileCubeQuery(QueryCubeExpression expression, out Set set)
			{
				if (!this.Compiler.canCompile && expression.Filter != null && this.Compiler.RequiresMoreThanOneHierarchy(expression))
				{
					CubeExpression cubeExpression = this.Compiler.removeNotEmptyVisitor.RemoveNotNullFilters(expression.Filter);
					cubeExpression = this.Compiler.captionToUniqueIdVisitor.ReplaceCaptionsWithUniqueIds(cubeExpression);
					expression = new QueryCubeExpression(expression.From, expression.DimensionAttributes, expression.Properties, expression.Measures, expression.MeasureProperties, cubeExpression, expression.Sort, expression.RowRange);
				}
				if (expression.Filter != null && expression.Filter.Kind == CubeExpressionKind.Binary)
				{
					CubeExpression cubeExpression2 = this.Compiler.dateFilterVisitor.UpdateDateTimeExpressionsInFilter(expression.Filter);
					expression = new QueryCubeExpression(expression.From, expression.DimensionAttributes, expression.Properties, expression.Measures, expression.MeasureProperties, cubeExpression2, expression.Sort, expression.RowRange);
				}
				if (!this.Compiler.canCompile && this.Compiler.HasMultipleHierarchiesFromOneDimension(expression))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.SapBwUnsupportedQuerySameDimensionMultipleHierarchies, null, null);
				}
				return base.TryCompileCubeQuery(expression, out set);
			}
		}

		// Token: 0x020004A5 RID: 1189
		private class SapBwMdxGenerator : MdxCubeExpressionMdxCompiler2.MdxGenerator
		{
			// Token: 0x06002734 RID: 10036 RVA: 0x00072EB6 File Offset: 0x000710B6
			public SapBwMdxGenerator(SapBwCubeExpressionMdxCompiler compiler)
				: base(compiler)
			{
			}

			// Token: 0x06002735 RID: 10037 RVA: 0x0000A6A5 File Offset: 0x000088A5
			protected override MdxExpression NewAddCalculatedMembers(MdxExpression mdxExpression)
			{
				return mdxExpression;
			}

			// Token: 0x06002736 RID: 10038 RVA: 0x00072EBF File Offset: 0x000710BF
			protected override MdxExpression NewAllMembers(MdxExpression mdxExpression)
			{
				return new InvocationMdxExpression(MdxFunction.AllMembers, new MdxExpression[] { mdxExpression });
			}
		}

		// Token: 0x020004A6 RID: 1190
		private class RemoveNotEmptyVisitor : CubeExpressionVisitor
		{
			// Token: 0x06002737 RID: 10039 RVA: 0x00072ED2 File Offset: 0x000710D2
			public CubeExpression RemoveNotNullFilters(CubeExpression expression)
			{
				return this.Visit(expression);
			}

			// Token: 0x06002738 RID: 10040 RVA: 0x00072EDB File Offset: 0x000710DB
			protected override CubeExpression VisitBinary(BinaryCubeExpression binary)
			{
				if (binary.Operator == BinaryOperator2.Or && this.HasAllNotNull(binary.Left) && this.HasAllNotNull(binary.Right))
				{
					return ConstantCubeExpression.True;
				}
				return base.VisitBinary(binary);
			}

			// Token: 0x06002739 RID: 10041 RVA: 0x00072F10 File Offset: 0x00071110
			private bool HasAllNotNull(CubeExpression expression)
			{
				if (expression.Kind != CubeExpressionKind.Binary)
				{
					return false;
				}
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)expression;
				BinaryOperator2 @operator = binaryCubeExpression.Operator;
				if (@operator != BinaryOperator2.NotEquals)
				{
					return @operator == BinaryOperator2.Or && this.HasAllNotNull(binaryCubeExpression.Left) && this.HasAllNotNull(binaryCubeExpression.Right);
				}
				return this.IsIdentifierNotNull(binaryCubeExpression.Left, binaryCubeExpression.Right);
			}

			// Token: 0x0600273A RID: 10042 RVA: 0x00072F74 File Offset: 0x00071174
			private bool IsIdentifierNotNull(CubeExpression left, CubeExpression right)
			{
				if (left.Kind == CubeExpressionKind.Identifier && right.Kind == CubeExpressionKind.Constant)
				{
					return ((ConstantCubeExpression)right).Value.IsNull;
				}
				return right.Kind == CubeExpressionKind.Identifier && left.Kind == CubeExpressionKind.Constant && ((ConstantCubeExpression)left).Value.IsNull;
			}
		}

		// Token: 0x020004A7 RID: 1191
		private class CaptionToUniqueIdVisitor : CubeExpressionVisitor
		{
			// Token: 0x0600273C RID: 10044 RVA: 0x00072FCE File Offset: 0x000711CE
			public CaptionToUniqueIdVisitor(SapBwCubeExpressionMdxCompiler compiler)
			{
				this.compiler = compiler;
			}

			// Token: 0x0600273D RID: 10045 RVA: 0x00072ED2 File Offset: 0x000710D2
			public CubeExpression ReplaceCaptionsWithUniqueIds(CubeExpression expression)
			{
				return this.Visit(expression);
			}

			// Token: 0x0600273E RID: 10046 RVA: 0x00072FE0 File Offset: 0x000711E0
			protected override CubeExpression VisitBinary(BinaryCubeExpression binary)
			{
				binary = (BinaryCubeExpression)base.VisitBinary(binary);
				bool flag = false;
				switch (binary.Operator)
				{
				case BinaryOperator2.Equals:
					break;
				case BinaryOperator2.NotEquals:
					flag = true;
					break;
				case BinaryOperator2.And:
					return binary;
				case BinaryOperator2.Or:
					return this.RemoveDuplicateOrFilters(binary);
				default:
					return binary;
				}
				IdentifierCubeExpression identifierCubeExpression;
				Value value;
				if ((this.TryGetCaption(binary.Left, out identifierCubeExpression) && SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.TryGetConstant(binary.Right, out value) && (value.IsText || value.IsNull)) || (this.TryGetCaption(binary.Right, out identifierCubeExpression) && SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.TryGetConstant(binary.Left, out value) && (value.IsText || value.IsNull)))
				{
					return this.CreateUniqueIdFilter(identifierCubeExpression, SapBwCubeExpressionMdxCompiler.CaptionFilterKind.Equals, flag, value);
				}
				CubeExpression cubeExpression = null;
				if (SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.TryGetConstant(binary.Right, out value) && value.IsLogical)
				{
					cubeExpression = binary.Left;
				}
				else if (SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.TryGetConstant(binary.Left, out value) && value.IsLogical)
				{
					cubeExpression = binary.Right;
				}
				if (cubeExpression != null && !value.AsBoolean)
				{
					SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.TryNot(cubeExpression, out cubeExpression);
				}
				if (cubeExpression != null)
				{
					return cubeExpression;
				}
				return binary;
			}

			// Token: 0x0600273F RID: 10047 RVA: 0x000730F0 File Offset: 0x000712F0
			protected override CubeExpression VisitInvocation(InvocationCubeExpression invocation)
			{
				ConstantCubeExpression constantCubeExpression = invocation.Function as ConstantCubeExpression;
				if (constantCubeExpression != null)
				{
					CubeExpression cubeExpression = null;
					if (constantCubeExpression.Value.AsFunction.FunctionIdentity.Equals(Library.Text.Contains.FunctionIdentity))
					{
						cubeExpression = this.VisitTextContainsOrStartsWith(invocation, SapBwCubeExpressionMdxCompiler.CaptionFilterKind.Contains);
					}
					else if (constantCubeExpression.Value.AsFunction.FunctionIdentity.Equals(Library.Text.StartsWith.FunctionIdentity))
					{
						cubeExpression = this.VisitTextContainsOrStartsWith(invocation, SapBwCubeExpressionMdxCompiler.CaptionFilterKind.StartsWith);
					}
					if (cubeExpression != null)
					{
						return cubeExpression;
					}
				}
				return base.VisitInvocation(invocation);
			}

			// Token: 0x06002740 RID: 10048 RVA: 0x00073170 File Offset: 0x00071370
			private CubeExpression CreateUniqueIdFilter(IdentifierCubeExpression caption, SapBwCubeExpressionMdxCompiler.CaptionFilterKind filterKind, bool invert, Value filterValue)
			{
				CubeExpression cubeExpression = null;
				IdentifierCubeExpression property = this.compiler.contextProvider.GetProperty(caption, CubePropertyKind.UniqueId, null);
				ConstantCubeExpression[] array;
				if (filterValue.IsNull)
				{
					cubeExpression = new BinaryCubeExpression(BinaryOperator2.Equals, property, ConstantCubeExpression.Null);
				}
				else if (this.compiler.TryGetCaptionUniqueIds(caption, filterKind, filterValue, out array))
				{
					for (int i = 0; i < array.Length; i++)
					{
						cubeExpression = cubeExpression.Or(new BinaryCubeExpression(BinaryOperator2.Equals, property, array[i]));
					}
				}
				if (cubeExpression == null)
				{
					cubeExpression = ConstantCubeExpression.False;
				}
				if (invert)
				{
					cubeExpression = SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.Not(cubeExpression);
				}
				return cubeExpression;
			}

			// Token: 0x06002741 RID: 10049 RVA: 0x000731F4 File Offset: 0x000713F4
			private CubeExpression VisitTextContainsOrStartsWith(InvocationCubeExpression invocation, SapBwCubeExpressionMdxCompiler.CaptionFilterKind filterKind)
			{
				IdentifierCubeExpression identifierCubeExpression;
				Value value;
				if (invocation.Arguments.Count == 2 && this.TryGetCaption(invocation.Arguments[0], out identifierCubeExpression) && SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.TryGetConstant(invocation.Arguments[1], out value) && value.IsText)
				{
					return this.CreateUniqueIdFilter(identifierCubeExpression, filterKind, false, value);
				}
				return null;
			}

			// Token: 0x06002742 RID: 10050 RVA: 0x00073250 File Offset: 0x00071450
			private CubeExpression RemoveDuplicateOrFilters(CubeExpression expression)
			{
				CubeExpression cubeExpression = null;
				HashSet<IdentifierCubeExpression> hashSet = new HashSet<IdentifierCubeExpression>();
				Queue<CubeExpression> queue = new Queue<CubeExpression>();
				queue.Enqueue(expression);
				while (queue.Count > 0)
				{
					CubeExpression cubeExpression2 = queue.Dequeue();
					if (cubeExpression2.Kind == CubeExpressionKind.Binary)
					{
						BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)cubeExpression2;
						BinaryOperator2 @operator = binaryCubeExpression.Operator;
						IdentifierCubeExpression identifierCubeExpression;
						Value value;
						if (@operator != BinaryOperator2.NotEquals)
						{
							if (@operator == BinaryOperator2.Or)
							{
								queue.Enqueue(binaryCubeExpression.Left);
								queue.Enqueue(binaryCubeExpression.Right);
								continue;
							}
						}
						else if (((this.TryGetUniqueId(binaryCubeExpression.Left, out identifierCubeExpression) && SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.TryGetConstant(binaryCubeExpression.Right, out value) && value.IsNull) || (this.TryGetUniqueId(binaryCubeExpression.Right, out identifierCubeExpression) && SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.TryGetConstant(binaryCubeExpression.Left, out value) && value.IsNull)) && !hashSet.Add(identifierCubeExpression))
						{
							continue;
						}
						cubeExpression = cubeExpression.Or(cubeExpression2);
					}
					else
					{
						cubeExpression = cubeExpression.Or(cubeExpression2);
					}
				}
				return cubeExpression;
			}

			// Token: 0x06002743 RID: 10051 RVA: 0x00073340 File Offset: 0x00071540
			private bool TryGetCaption(CubeExpression expr, out IdentifierCubeExpression caption)
			{
				caption = expr as IdentifierCubeExpression;
				if (caption != null)
				{
					CubeObjectKind objectKind = this.compiler.contextProvider.GetObjectKind(caption);
					if (objectKind == CubeObjectKind.DimensionAttribute)
					{
						return true;
					}
					if (objectKind == CubeObjectKind.Property)
					{
						if (this.compiler.contextProvider.GetPropertyKind(caption) == CubePropertyKind.Caption)
						{
							caption = this.compiler.contextProvider.GetPropertyDimensionAttribute(caption);
							return true;
						}
					}
				}
				caption = null;
				return false;
			}

			// Token: 0x06002744 RID: 10052 RVA: 0x000733A6 File Offset: 0x000715A6
			private bool TryGetUniqueId(CubeExpression expr, out IdentifierCubeExpression uniqueId)
			{
				uniqueId = expr as IdentifierCubeExpression;
				if (uniqueId != null && this.compiler.contextProvider.GetObjectKind(uniqueId) == CubeObjectKind.Property && this.compiler.contextProvider.GetPropertyKind(uniqueId) == CubePropertyKind.UniqueId)
				{
					return true;
				}
				uniqueId = null;
				return false;
			}

			// Token: 0x06002745 RID: 10053 RVA: 0x000733E4 File Offset: 0x000715E4
			private static bool TryGetConstant(CubeExpression expr, out Value value)
			{
				ConstantCubeExpression constantCubeExpression = expr as ConstantCubeExpression;
				if (constantCubeExpression != null)
				{
					value = constantCubeExpression.Value;
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x06002746 RID: 10054 RVA: 0x00073409 File Offset: 0x00071609
			private static CubeExpression Not(CubeExpression expression)
			{
				if (!SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.TryNot(expression, out expression))
				{
					throw new NotSupportedException();
				}
				return expression;
			}

			// Token: 0x06002747 RID: 10055 RVA: 0x0007341C File Offset: 0x0007161C
			private static bool TryNot(CubeExpression expression, out CubeExpression result)
			{
				CubeExpressionKind kind = expression.Kind;
				if (kind != CubeExpressionKind.Constant)
				{
					if (kind == CubeExpressionKind.Binary)
					{
						BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)expression;
						switch (binaryCubeExpression.Operator)
						{
						case BinaryOperator2.Equals:
							result = new BinaryCubeExpression(BinaryOperator2.NotEquals, binaryCubeExpression.Left, binaryCubeExpression.Right);
							return true;
						case BinaryOperator2.NotEquals:
							result = new BinaryCubeExpression(BinaryOperator2.Equals, binaryCubeExpression.Left, binaryCubeExpression.Right);
							return true;
						case BinaryOperator2.And:
							result = new BinaryCubeExpression(BinaryOperator2.Or, SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.Not(binaryCubeExpression.Left), SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.Not(binaryCubeExpression.Right));
							return true;
						case BinaryOperator2.Or:
							result = new BinaryCubeExpression(BinaryOperator2.And, SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.Not(binaryCubeExpression.Left), SapBwCubeExpressionMdxCompiler.CaptionToUniqueIdVisitor.Not(binaryCubeExpression.Right));
							return true;
						}
					}
				}
				else
				{
					ConstantCubeExpression constantCubeExpression = (ConstantCubeExpression)expression;
					if (constantCubeExpression.Value.IsLogical)
					{
						result = (constantCubeExpression.Value.AsBoolean ? ConstantCubeExpression.False : ConstantCubeExpression.True);
						return true;
					}
				}
				result = null;
				return false;
			}

			// Token: 0x0400107A RID: 4218
			private readonly SapBwCubeExpressionMdxCompiler compiler;
		}

		// Token: 0x020004A8 RID: 1192
		private enum CaptionFilterKind
		{
			// Token: 0x0400107C RID: 4220
			Equals,
			// Token: 0x0400107D RID: 4221
			Contains,
			// Token: 0x0400107E RID: 4222
			StartsWith
		}

		// Token: 0x020004A9 RID: 1193
		private class DateTimeFilterVisitor : CubeExpressionVisitor
		{
			// Token: 0x06002748 RID: 10056 RVA: 0x0007350B File Offset: 0x0007170B
			public DateTimeFilterVisitor(SapBwCubeExpressionMdxCompiler compiler)
			{
				this.compiler = compiler;
			}

			// Token: 0x06002749 RID: 10057 RVA: 0x00072ED2 File Offset: 0x000710D2
			public CubeExpression UpdateDateTimeExpressionsInFilter(CubeExpression expression)
			{
				return this.Visit(expression);
			}

			// Token: 0x0600274A RID: 10058 RVA: 0x0007351C File Offset: 0x0007171C
			protected override CubeExpression VisitBinary(BinaryCubeExpression binary)
			{
				CubeExpression cubeExpression = this.Visit(binary.Left);
				CubeExpression cubeExpression2 = this.Visit(binary.Right);
				ConstantCubeExpression constantCubeExpression = null;
				if (cubeExpression2.Kind == CubeExpressionKind.Constant && cubeExpression.Kind == CubeExpressionKind.Identifier)
				{
					IdentifierCubeExpression identifierCubeExpression = (IdentifierCubeExpression)cubeExpression;
					constantCubeExpression = (ConstantCubeExpression)cubeExpression2;
					ConstantCubeExpression constantCubeExpression2;
					if (this.TryExtractTimeFromDateTime(identifierCubeExpression, constantCubeExpression, out constantCubeExpression2))
					{
						cubeExpression2 = constantCubeExpression2;
					}
				}
				else if (cubeExpression.Kind == CubeExpressionKind.Constant && cubeExpression2.Kind == CubeExpressionKind.Identifier)
				{
					IdentifierCubeExpression identifierCubeExpression2 = (IdentifierCubeExpression)cubeExpression2;
					constantCubeExpression = (ConstantCubeExpression)cubeExpression;
					ConstantCubeExpression constantCubeExpression3;
					if (this.TryExtractTimeFromDateTime(identifierCubeExpression2, constantCubeExpression, out constantCubeExpression3))
					{
						cubeExpression = constantCubeExpression3;
					}
				}
				if (constantCubeExpression != null && constantCubeExpression.Value != null && constantCubeExpression.Value.Kind != ValueKind.Date && constantCubeExpression.Value.Kind != ValueKind.DateTime && constantCubeExpression.Value.Kind != ValueKind.Time)
				{
					return binary;
				}
				return new BinaryCubeExpression(binary.Operator, cubeExpression, cubeExpression2);
			}

			// Token: 0x0600274B RID: 10059 RVA: 0x000735EC File Offset: 0x000717EC
			private bool TryExtractTimeFromDateTime(IdentifierCubeExpression identifierExpression, ConstantCubeExpression constExpression, out ConstantCubeExpression extractedTimeConstExpression)
			{
				IdentifierCubeExpression identifierCubeExpression;
				if (identifierExpression.TryGetIdentifier(out identifierCubeExpression))
				{
					MdxProperty mdxProperty = this.compiler.contextProvider.Cube.GetObject(identifierCubeExpression.Identifier) as MdxProperty;
					if (mdxProperty != null)
					{
						MdxIdentifier mdxIdentifier = MdxIdentifier.Parse(mdxProperty.Level.MdxIdentifier);
						string text = mdxProperty.Level.MdxIdentifier + "." + MdxIdentifier.QuotePart("2" + mdxIdentifier.DimensionName);
						MdxCubeObject mdxCubeObject;
						if (this.compiler.contextProvider.Cube.TryGetObject(text, out mdxCubeObject) && constExpression.Value.Kind == ValueKind.DateTime)
						{
							MdxProperty mdxProperty2 = mdxCubeObject as MdxProperty;
							extractedTimeConstExpression = new ConstantCubeExpression(TimeValue.New(constExpression.Value.AsDateTime.AsClrDateTime.TimeOfDay));
							return mdxProperty2 != null && mdxProperty2.Type == OleDbType.DBTime;
						}
					}
				}
				extractedTimeConstExpression = null;
				return false;
			}

			// Token: 0x0600274C RID: 10060 RVA: 0x000736D8 File Offset: 0x000718D8
			protected override CubeExpression VisitInvocation(InvocationCubeExpression invocation)
			{
				ConstantCubeExpression constantCubeExpression = invocation.Function as ConstantCubeExpression;
				FunctionValue functionValue;
				if (constantCubeExpression != null && constantCubeExpression.Value != null && constantCubeExpression.Value.IsFunction && constantCubeExpression.Value.AsFunction.TryGetAs<FunctionValue>(out functionValue) && (functionValue is Library.DateTime.FromFunctionValue || functionValue is Library.Time.FromFunctionValue) && invocation.Arguments.Count == 1 && invocation.Arguments[0] != null && (invocation.Arguments[0].Kind == CubeExpressionKind.Identifier || invocation.Arguments[0].Kind == CubeExpressionKind.Constant))
				{
					return this.Visit(invocation.Arguments[0]);
				}
				return invocation;
			}

			// Token: 0x0600274D RID: 10061 RVA: 0x0007378C File Offset: 0x0007198C
			protected override CubeExpression VisitIdentifier(IdentifierCubeExpression identifier)
			{
				MdxCubeObject @object = this.compiler.cube.GetObject(identifier.Identifier);
				if (@object.Kind == MdxCubeObjectKind.Property)
				{
					MdxProperty mdxProperty = (MdxProperty)@object;
					if (mdxProperty.PropertyKind == MdxPropertyKind.UserDefined && (mdxProperty.Type == OleDbType.DBDate || mdxProperty.Type == OleDbType.DBTime))
					{
						return new IdentifierCubeExpression(this.CreateAndAddMemberNameProperty(mdxProperty));
					}
				}
				return identifier;
			}

			// Token: 0x0600274E RID: 10062 RVA: 0x000737F4 File Offset: 0x000719F4
			private string CreateAndAddMemberNameProperty(MdxProperty sourceProperty)
			{
				string text = InvocationMdxExpression.ToString(MdxFunction.MemberName);
				string text2 = sourceProperty.Level.MdxIdentifier + "." + MdxIdentifier.QuotePart(text);
				MdxCubeObject mdxCubeObject;
				if (!this.compiler.cube.TryGetObject(text2, out mdxCubeObject))
				{
					MdxProperty mdxProperty = new MdxProperty(MdxPropertyKind.UserDefined, MdxIdentifier.QuotePart(text), text2, sourceProperty.Level.Caption + "." + text, sourceProperty.Level, OleDbType.BSTR, sourceProperty.Key);
					this.compiler.Cube.AddObject(mdxProperty);
				}
				return text2;
			}

			// Token: 0x0400107F RID: 4223
			private readonly SapBwCubeExpressionMdxCompiler compiler;
		}
	}
}
