using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000AFB RID: 2811
	internal sealed class GoogleAnalyticsCubeContextProvider : CubeContextProvider
	{
		// Token: 0x06004E01 RID: 19969 RVA: 0x00102AFC File Offset: 0x00100CFC
		public GoogleAnalyticsCubeContextProvider(GoogleAnalyticsCube cube, IGoogleAnalyticsQueryCompiler compiler)
		{
			this.cube = cube;
			this.cubeId = new IdentifierCubeExpression(cube.Name);
			this.compiler = compiler;
		}

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x06004E02 RID: 19970 RVA: 0x00102B23 File Offset: 0x00100D23
		public CubeContext DefaultContext
		{
			get
			{
				return new GoogleAnalyticsCubeContextProvider.GoogleAnalyticsCubeContext(this, new QueryCubeExpression(this.cubeId), this.cube.ResultEnumeratorFactory);
			}
		}

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x06004E03 RID: 19971 RVA: 0x00102B41 File Offset: 0x00100D41
		public override IEngineHost EngineHost
		{
			get
			{
				return this.cube.Host;
			}
		}

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x06004E04 RID: 19972 RVA: 0x00102B50 File Offset: 0x00100D50
		private TableValue Dimensions
		{
			get
			{
				if (this.dimensionsTable == null)
				{
					CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.New();
					foreach (GoogleAnalyticsCubeObject googleAnalyticsCubeObject in this.cube.Dimensions)
					{
						List<IdentifierCubeExpression> list = new List<IdentifierCubeExpression>();
						KeysBuilder keysBuilder = default(KeysBuilder);
						list.Add(new IdentifierCubeExpression(googleAnalyticsCubeObject.ID));
						keysBuilder.Add(googleAnalyticsCubeObject.ID);
						CubeValue cubeValue = CubeContextCubeValue.New(this, new GoogleAnalyticsCubeContextProvider.GoogleAnalyticsCubeContext(this, new QueryCubeExpression(this.cubeId, list, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, null, EmptyArray<CubeSortOrder>.Instance, RowRange.All), this.cube.ResultEnumeratorFactory), keysBuilder.ToKeys());
						cubeObjectTableBuilder.AddDimension(googleAnalyticsCubeObject.ID, googleAnalyticsCubeObject.Name, cubeValue);
					}
					this.dimensionsTable = cubeObjectTableBuilder.ToTable();
				}
				return this.dimensionsTable;
			}
		}

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x06004E05 RID: 19973 RVA: 0x00102C4C File Offset: 0x00100E4C
		private TableValue Measures
		{
			get
			{
				if (this.measuresTable == null)
				{
					CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.New();
					foreach (GoogleAnalyticsCubeObject googleAnalyticsCubeObject in this.cube.Measures)
					{
						IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(googleAnalyticsCubeObject.ID);
						MeasureValue measureValue = new MeasureValue(identifierCubeExpression, TypeValue.Any);
						cubeObjectTableBuilder.AddMeasure(identifierCubeExpression.Identifier, googleAnalyticsCubeObject.Name, googleAnalyticsCubeObject.Description, measureValue);
					}
					this.measuresTable = cubeObjectTableBuilder.ToTable();
				}
				return this.measuresTable;
			}
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x00102CEC File Offset: 0x00100EEC
		private static void PreparePathFolders(SortedDictionary<GoogleAnalyticsCubeObjectPath, CubeObjectTableBuilder> folders, GoogleAnalyticsCubeObjectPath path, out CubeObjectTableBuilder tableBuilder)
		{
			if (!folders.TryGetValue(path, out tableBuilder))
			{
				tableBuilder = CubeObjectTableBuilder.NewWithoutLink();
				folders[path] = tableBuilder;
				if (!path.IsRoot)
				{
					CubeObjectTableBuilder cubeObjectTableBuilder;
					GoogleAnalyticsCubeContextProvider.PreparePathFolders(folders, path.Parent, out cubeObjectTableBuilder);
				}
			}
		}

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x06004E07 RID: 19975 RVA: 0x00102D2C File Offset: 0x00100F2C
		private TableValue DisplayFolders
		{
			get
			{
				if (this.displayFolders == null)
				{
					SortedDictionary<GoogleAnalyticsCubeObjectPath, CubeObjectTableBuilder> sortedDictionary = new SortedDictionary<GoogleAnalyticsCubeObjectPath, CubeObjectTableBuilder>();
					foreach (GoogleAnalyticsCubeObject googleAnalyticsCubeObject in from o in this.cube.Measures.Concat(this.cube.Dimensions)
						orderby o.BaseName
						select o)
					{
						if (googleAnalyticsCubeObject.Status == GoogleAnalyticsCubeObjectStatus.Public)
						{
							CubeObjectTableBuilder cubeObjectTableBuilder;
							GoogleAnalyticsCubeContextProvider.PreparePathFolders(sortedDictionary, googleAnalyticsCubeObject.Path, out cubeObjectTableBuilder);
							if (!sortedDictionary.TryGetValue(googleAnalyticsCubeObject.Path, out cubeObjectTableBuilder))
							{
								cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
								sortedDictionary[googleAnalyticsCubeObject.Path] = cubeObjectTableBuilder;
							}
							if (googleAnalyticsCubeObject.Kind == GoogleAnalyticsCubeObjectKind.Dimension)
							{
								CubeObjectTableBuilder cubeObjectTableBuilder2 = CubeObjectTableBuilder.NewWithoutLink();
								cubeObjectTableBuilder2.AddDimensionAttribute(googleAnalyticsCubeObject.ID, googleAnalyticsCubeObject.Name);
								cubeObjectTableBuilder.AddDimensionFolder(googleAnalyticsCubeObject.ID, googleAnalyticsCubeObject.Name, googleAnalyticsCubeObject.Description, cubeObjectTableBuilder2.ToTable());
							}
							else
							{
								MeasureValue measureValue = new MeasureValue(new IdentifierCubeExpression(googleAnalyticsCubeObject.ID), TypeValue.Any);
								cubeObjectTableBuilder.AddMeasure(googleAnalyticsCubeObject.ID, googleAnalyticsCubeObject.Name, googleAnalyticsCubeObject.Description, measureValue);
							}
						}
					}
					foreach (KeyValuePair<GoogleAnalyticsCubeObjectPath, CubeObjectTableBuilder> keyValuePair in sortedDictionary)
					{
						if (!keyValuePair.Key.IsRoot)
						{
							sortedDictionary[keyValuePair.Key.Parent].AddFolder(keyValuePair.Key.Last(), keyValuePair.Value.ToTable());
						}
					}
					this.displayFolders = sortedDictionary[GoogleAnalyticsCubeObjectPath.Root].ToTable();
				}
				return this.displayFolders;
			}
		}

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x06004E08 RID: 19976 RVA: 0x00102F08 File Offset: 0x00101108
		public override IResource Resource
		{
			get
			{
				return GoogleAnalyticsServiceV1.resource;
			}
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x00102F0F File Offset: 0x0010110F
		public override string GetDisplayName(IdentifierCubeExpression identifier)
		{
			return this.cube.GetObject(identifier.Identifier).Name;
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x00102F28 File Offset: 0x00101128
		public override TypeValue GetType(IdentifierCubeExpression identifier)
		{
			GoogleAnalyticsCubeObject @object = this.cube.GetObject(identifier.Identifier);
			return this.MarshallGoogleAnalyticsDataType(@object.Type);
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x00102F54 File Offset: 0x00101154
		private TypeValue MarshallGoogleAnalyticsDataType(GoogleAnalyticsDataType dataType)
		{
			switch (dataType)
			{
			case GoogleAnalyticsDataType.Currency:
				return TypeValue.Currency;
			case GoogleAnalyticsDataType.Date:
				return TypeValue.Date;
			case GoogleAnalyticsDataType.Float:
				return TypeValue.Double;
			case GoogleAnalyticsDataType.Integer:
				return TypeValue.Int32;
			case GoogleAnalyticsDataType.Percent:
				return TypeValue.Double;
			case GoogleAnalyticsDataType.String:
				return TypeValue.Text;
			case GoogleAnalyticsDataType.Time:
				return TypeValue.Duration;
			}
			return TypeValue.Text;
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x00102FC0 File Offset: 0x001011C0
		public override CubeObjectKind GetObjectKind(IdentifierCubeExpression identifier)
		{
			GoogleAnalyticsCubeObjectKind kind = this.cube.GetObject(identifier.Identifier).Kind;
			if (kind == GoogleAnalyticsCubeObjectKind.Measure)
			{
				return CubeObjectKind.Measure;
			}
			if (kind == GoogleAnalyticsCubeObjectKind.Dimension)
			{
				return CubeObjectKind.DimensionAttribute;
			}
			return CubeObjectKind.Other;
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x000033E7 File Offset: 0x000015E7
		public override IdentifierCubeExpression GetProperty(IdentifierCubeExpression dimensionAttribute, CubePropertyKind kind, string userDefinedIdentifier = null)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x000033E7 File Offset: 0x000015E7
		public override IdentifierCubeExpression GetPropertyDimensionAttribute(IdentifierCubeExpression property)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x000033E7 File Offset: 0x000015E7
		public override CubePropertyKind GetPropertyKind(IdentifierCubeExpression property)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x00102FF0 File Offset: 0x001011F0
		public override bool TryCreateContext(QueryCubeExpression expression, IList<ParameterArguments> parameters, out CubeContext context)
		{
			if (this.compiler.CanCompile(expression))
			{
				context = new GoogleAnalyticsCubeContextProvider.GoogleAnalyticsCubeContext(this, expression, this.cube.ResultEnumeratorFactory);
				return true;
			}
			context = null;
			return false;
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x0010301A File Offset: 0x0010121A
		public static IEnumerator<IValueReference> CreateV1ResultEnumerator(GoogleAnalyticsCube cube, GoogleAnalyticsQueryExpression compiledExpression, CubeExpression filterExpression, Keys keys)
		{
			return new GoogleAnalyticsCubeContextProvider.GoogleAnalyticsResultEnumeratorV1(cube, compiledExpression, filterExpression, keys);
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x00103025 File Offset: 0x00101225
		public static IEnumerator<IValueReference> CreateV2ResultEnumerator(GoogleAnalyticsCube cube, GoogleAnalyticsQueryExpression compiledExpression, CubeExpression filterExpression, Keys keys)
		{
			return new GoogleAnalyticsCubeContextProvider.GoogleAnalyticsResultEnumeratorV2(cube, compiledExpression, filterExpression, keys);
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x00103030 File Offset: 0x00101230
		private static bool RowSatisfiesDateFilter(IValueReference dateDimensionValue, CubeExpression filterExpression)
		{
			if (dateDimensionValue is ExceptionValueReference)
			{
				return true;
			}
			if (filterExpression.Kind == CubeExpressionKind.Binary)
			{
				BinaryCubeExpression binaryCubeExpression = (BinaryCubeExpression)filterExpression;
				if (binaryCubeExpression.Operator == BinaryOperator2.And)
				{
					return GoogleAnalyticsCubeContextProvider.RowSatisfiesDateFilter(dateDimensionValue, binaryCubeExpression.Left) && GoogleAnalyticsCubeContextProvider.RowSatisfiesDateFilter(dateDimensionValue, binaryCubeExpression.Right);
				}
				if (binaryCubeExpression.Operator == BinaryOperator2.Or)
				{
					return GoogleAnalyticsCubeContextProvider.RowSatisfiesDateFilter(dateDimensionValue, binaryCubeExpression.Left) || GoogleAnalyticsCubeContextProvider.RowSatisfiesDateFilter(dateDimensionValue, binaryCubeExpression.Right);
				}
				IdentifierCubeExpression identifierCubeExpression;
				ConstantCubeExpression constantCubeExpression;
				if (binaryCubeExpression.Left.Kind == CubeExpressionKind.Identifier && binaryCubeExpression.Right.Kind == CubeExpressionKind.Constant)
				{
					identifierCubeExpression = (IdentifierCubeExpression)binaryCubeExpression.Left;
					constantCubeExpression = (ConstantCubeExpression)binaryCubeExpression.Right;
				}
				else
				{
					if (binaryCubeExpression.Left.Kind != CubeExpressionKind.Constant || binaryCubeExpression.Right.Kind != CubeExpressionKind.Identifier)
					{
						return false;
					}
					identifierCubeExpression = (IdentifierCubeExpression)binaryCubeExpression.Right;
					constantCubeExpression = (ConstantCubeExpression)binaryCubeExpression.Left;
				}
				if (!(identifierCubeExpression.Identifier == "ga:date") && !(identifierCubeExpression.Identifier == "date"))
				{
					return true;
				}
				Value value = ((binaryCubeExpression.Left.Kind == CubeExpressionKind.Identifier) ? dateDimensionValue.Value : constantCubeExpression.Value);
				Value value2 = ((binaryCubeExpression.Right.Kind == CubeExpressionKind.Constant) ? constantCubeExpression.Value : dateDimensionValue.Value);
				switch (binaryCubeExpression.Operator)
				{
				case BinaryOperator2.GreaterThan:
					return value.GreaterThan(value2);
				case BinaryOperator2.LessThan:
					return value.LessThan(value2);
				case BinaryOperator2.GreaterThanOrEquals:
					return value.GreaterThanOrEqual(value2);
				case BinaryOperator2.LessThanOrEquals:
					return value.LessThanOrEqual(value2);
				case BinaryOperator2.Equals:
					return value.Equals(value2);
				case BinaryOperator2.NotEquals:
					return !value.Equals(value2);
				}
			}
			return false;
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x000091AE File Offset: 0x000073AE
		public override IdentifierCubeExpression GetMeasureProperty(IdentifierCubeExpression measureIdentifier, string propertyIdentifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040029D9 RID: 10713
		private readonly GoogleAnalyticsCube cube;

		// Token: 0x040029DA RID: 10714
		private readonly IdentifierCubeExpression cubeId;

		// Token: 0x040029DB RID: 10715
		private readonly IGoogleAnalyticsQueryCompiler compiler;

		// Token: 0x040029DC RID: 10716
		private TableValue displayFolders;

		// Token: 0x040029DD RID: 10717
		private TableValue dimensionsTable;

		// Token: 0x040029DE RID: 10718
		private TableValue measuresTable;

		// Token: 0x02000AFC RID: 2812
		private sealed class GoogleAnalyticsCubeContext : CubeContext
		{
			// Token: 0x06004E15 RID: 19989 RVA: 0x001031DC File Offset: 0x001013DC
			public GoogleAnalyticsCubeContext(GoogleAnalyticsCubeContextProvider contextProvider, QueryCubeExpression expression, Func<GoogleAnalyticsCube, GoogleAnalyticsQueryExpression, CubeExpression, Keys, IEnumerator<IValueReference>> resultEnumeratorFactory)
				: base(expression)
			{
				this.contextProvider = contextProvider;
				this.resultEnumeratorFactory = resultEnumeratorFactory;
			}

			// Token: 0x17001871 RID: 6257
			// (get) Token: 0x06004E16 RID: 19990 RVA: 0x001031F3 File Offset: 0x001013F3
			public override TableValue DisplayFolders
			{
				get
				{
					return this.contextProvider.DisplayFolders;
				}
			}

			// Token: 0x17001872 RID: 6258
			// (get) Token: 0x06004E17 RID: 19991 RVA: 0x00103200 File Offset: 0x00101400
			public override TableValue Dimensions
			{
				get
				{
					return this.contextProvider.Dimensions;
				}
			}

			// Token: 0x17001873 RID: 6259
			// (get) Token: 0x06004E18 RID: 19992 RVA: 0x0010320D File Offset: 0x0010140D
			public override TableValue Measures
			{
				get
				{
					return this.contextProvider.Measures;
				}
			}

			// Token: 0x17001874 RID: 6260
			// (get) Token: 0x06004E19 RID: 19993 RVA: 0x00066554 File Offset: 0x00064754
			public override TableValue MeasureGroups
			{
				get
				{
					return TableValue.Empty;
				}
			}

			// Token: 0x17001875 RID: 6261
			// (get) Token: 0x06004E1A RID: 19994 RVA: 0x0010321A File Offset: 0x0010141A
			public override CubeContextProvider ContextProvider
			{
				get
				{
					return this.contextProvider;
				}
			}

			// Token: 0x17001876 RID: 6262
			// (get) Token: 0x06004E1B RID: 19995 RVA: 0x00103222 File Offset: 0x00101422
			public override IEngineHost EngineHost
			{
				get
				{
					return this.contextProvider.EngineHost;
				}
			}

			// Token: 0x06004E1C RID: 19996 RVA: 0x0010322F File Offset: 0x0010142F
			public override TableValue GetAvailableProperties()
			{
				return CubePropertiesTableValue.Empty;
			}

			// Token: 0x06004E1D RID: 19997 RVA: 0x00066802 File Offset: 0x00064A02
			public override TableValue GetAvailableMeasureProperties()
			{
				return CubeMeasurePropertiesTableValue.Empty;
			}

			// Token: 0x06004E1E RID: 19998 RVA: 0x00103236 File Offset: 0x00101436
			public override IEnumerator<IValueReference> Evaluate()
			{
				return new GoogleAnalyticsCubeContextProvider.GoogleAnalyticsCubeContext.PagingResultEnumerator(this.contextProvider.compiler, this.contextProvider.cube, base.CubeExpression, CubeContext.GetKeys(base.CubeExpression), this.resultEnumeratorFactory);
			}

			// Token: 0x040029DF RID: 10719
			private readonly GoogleAnalyticsCubeContextProvider contextProvider;

			// Token: 0x040029E0 RID: 10720
			private readonly Func<GoogleAnalyticsCube, GoogleAnalyticsQueryExpression, CubeExpression, Keys, IEnumerator<IValueReference>> resultEnumeratorFactory;

			// Token: 0x02000AFD RID: 2813
			private class PagingResultEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06004E1F RID: 19999 RVA: 0x0010326A File Offset: 0x0010146A
				public PagingResultEnumerator(IGoogleAnalyticsQueryCompiler compiler, GoogleAnalyticsCube cube, QueryCubeExpression expression, Keys keys, Func<GoogleAnalyticsCube, GoogleAnalyticsQueryExpression, CubeExpression, Keys, IEnumerator<IValueReference>> resultEnumeratorFactory)
				{
					this.compiler = compiler;
					this.cube = cube;
					this.expression = expression;
					this.keys = keys;
					this.resultEnumeratorFactory = resultEnumeratorFactory;
				}

				// Token: 0x17001877 RID: 6263
				// (get) Token: 0x06004E20 RID: 20000 RVA: 0x00103297 File Offset: 0x00101497
				public IValueReference Current
				{
					get
					{
						if (this.currentEnumerator == null)
						{
							return null;
						}
						return this.currentEnumerator.Current;
					}
				}

				// Token: 0x17001878 RID: 6264
				// (get) Token: 0x06004E21 RID: 20001 RVA: 0x001032AE File Offset: 0x001014AE
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06004E22 RID: 20002 RVA: 0x001032B6 File Offset: 0x001014B6
				public void Dispose()
				{
					if (this.currentEnumerator != null)
					{
						this.currentEnumerator.Dispose();
						this.currentEnumerator = null;
						this.isDone = true;
					}
				}

				// Token: 0x06004E23 RID: 20003 RVA: 0x001032DC File Offset: 0x001014DC
				public bool MoveNext()
				{
					if (this.isDone)
					{
						return false;
					}
					if (this.currentEnumerator == null)
					{
						RowRange rowRange = this.expression.RowRange.Skip(new RowCount(this.rowsReturned)).Take(new RowCount(10000L));
						QueryCubeExpression queryCubeExpression = new QueryCubeExpression(this.expression.From, this.expression.DimensionAttributes, this.expression.Properties, this.expression.Measures, this.expression.MeasureProperties, this.expression.Filter, this.expression.Sort, rowRange);
						GoogleAnalyticsQueryExpression googleAnalyticsQueryExpression = (GoogleAnalyticsQueryExpression)this.compiler.Compile(queryCubeExpression);
						if (googleAnalyticsQueryExpression.IsFalse)
						{
							return false;
						}
						this.rowsReturnedInPage = 0L;
						this.currentEnumerator = this.resultEnumeratorFactory(this.cube, googleAnalyticsQueryExpression, googleAnalyticsQueryExpression.Filter, this.keys);
					}
					if (this.currentEnumerator.MoveNext())
					{
						this.rowsReturnedInPage += 1L;
						this.rowsReturned += 1L;
						return true;
					}
					if (this.rowsReturnedInPage < 10000L)
					{
						this.isDone = true;
						return false;
					}
					this.currentEnumerator.Dispose();
					this.currentEnumerator = null;
					return this.MoveNext();
				}

				// Token: 0x06004E24 RID: 20004 RVA: 0x000033E7 File Offset: 0x000015E7
				public void Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x040029E1 RID: 10721
				private const int PageSize = 10000;

				// Token: 0x040029E2 RID: 10722
				private readonly IGoogleAnalyticsQueryCompiler compiler;

				// Token: 0x040029E3 RID: 10723
				private readonly GoogleAnalyticsCube cube;

				// Token: 0x040029E4 RID: 10724
				private readonly QueryCubeExpression expression;

				// Token: 0x040029E5 RID: 10725
				private readonly Keys keys;

				// Token: 0x040029E6 RID: 10726
				private readonly Func<GoogleAnalyticsCube, GoogleAnalyticsQueryExpression, CubeExpression, Keys, IEnumerator<IValueReference>> resultEnumeratorFactory;

				// Token: 0x040029E7 RID: 10727
				private IEnumerator<IValueReference> currentEnumerator;

				// Token: 0x040029E8 RID: 10728
				private bool isDone;

				// Token: 0x040029E9 RID: 10729
				private long rowsReturned;

				// Token: 0x040029EA RID: 10730
				private long rowsReturnedInPage;
			}
		}

		// Token: 0x02000AFE RID: 2814
		private abstract class GoogleAnalyticsResultEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06004E25 RID: 20005 RVA: 0x00103428 File Offset: 0x00101628
			public GoogleAnalyticsResultEnumerator(GoogleAnalyticsCube cube, GoogleAnalyticsQueryExpression compiledExpression, CubeExpression filterExpression, Keys keys)
			{
				this.cube = cube;
				this.compiledExpression = compiledExpression;
				this.filterExpression = filterExpression;
				this.keys = keys;
			}

			// Token: 0x17001879 RID: 6265
			// (get) Token: 0x06004E26 RID: 20006 RVA: 0x0010344D File Offset: 0x0010164D
			public IValueReference Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x1700187A RID: 6266
			// (get) Token: 0x06004E27 RID: 20007 RVA: 0x00103455 File Offset: 0x00101655
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06004E28 RID: 20008
			protected abstract ListValue GetResult(Value webResult);

			// Token: 0x06004E29 RID: 20009
			protected abstract IValueReference[] ExtractRowValues(RecordValue row);

			// Token: 0x06004E2A RID: 20010 RVA: 0x0010345D File Offset: 0x0010165D
			public void Dispose()
			{
				this.current = null;
			}

			// Token: 0x06004E2B RID: 20011 RVA: 0x00103468 File Offset: 0x00101668
			public bool MoveNext()
			{
				if (this.rows == null)
				{
					Value value = this.cube.Query(this.compiledExpression);
					Value value2;
					if (value.TryGetValue("error", out value2))
					{
						throw DataSourceException.NewDataSourceError(this.cube.Host, value2["errors"][0]["message"].AsString, GoogleAnalyticsServiceV1.resource, "error", value2, TypeValue.Text, null);
					}
					this.rows = this.GetResult(value);
					if (this.rows == null)
					{
						return false;
					}
				}
				while (this.rowNumber < this.rows.Count)
				{
					Value value3 = this.rows;
					int num = this.rowNumber;
					this.rowNumber = num + 1;
					RecordValue asRecord = value3[num].AsRecord;
					IValueReference[] array = this.ExtractRowValues(asRecord);
					if (!this.compiledExpression.ShouldFilterLocally || GoogleAnalyticsCubeContextProvider.RowSatisfiesDateFilter(array[this.dateDimensionColumn], this.filterExpression))
					{
						this.current = RecordValue.New(this.keys, array);
						return true;
					}
				}
				return false;
			}

			// Token: 0x06004E2C RID: 20012 RVA: 0x000091AE File Offset: 0x000073AE
			public void Reset()
			{
				throw new NotImplementedException();
			}

			// Token: 0x040029EB RID: 10731
			private readonly GoogleAnalyticsCube cube;

			// Token: 0x040029EC RID: 10732
			private readonly GoogleAnalyticsQueryExpression compiledExpression;

			// Token: 0x040029ED RID: 10733
			private readonly CubeExpression filterExpression;

			// Token: 0x040029EE RID: 10734
			protected readonly Keys keys;

			// Token: 0x040029EF RID: 10735
			protected const string ErrorKey = "error";

			// Token: 0x040029F0 RID: 10736
			protected const string RowsKey = "rows";

			// Token: 0x040029F1 RID: 10737
			private ListValue rows;

			// Token: 0x040029F2 RID: 10738
			protected GoogleAnalyticsDataType[] columnDataTypes;

			// Token: 0x040029F3 RID: 10739
			protected bool[] dimensionColumns;

			// Token: 0x040029F4 RID: 10740
			private int rowNumber;

			// Token: 0x040029F5 RID: 10741
			private IValueReference current;

			// Token: 0x040029F6 RID: 10742
			protected int dateDimensionColumn;
		}

		// Token: 0x02000AFF RID: 2815
		private sealed class GoogleAnalyticsResultEnumeratorV1 : GoogleAnalyticsCubeContextProvider.GoogleAnalyticsResultEnumerator
		{
			// Token: 0x06004E2D RID: 20013 RVA: 0x0010356E File Offset: 0x0010176E
			public GoogleAnalyticsResultEnumeratorV1(GoogleAnalyticsCube cube, GoogleAnalyticsQueryExpression compiledExpression, CubeExpression filterExpression, Keys keys)
				: base(cube, compiledExpression, filterExpression, keys)
			{
			}

			// Token: 0x06004E2E RID: 20014 RVA: 0x0010357C File Offset: 0x0010177C
			protected override ListValue GetResult(Value webResult)
			{
				Value value = webResult["reports"][0];
				Value value2;
				if (!value["data"].TryGetValue("rows", out value2))
				{
					return null;
				}
				RecordValue asRecord = value["columnHeader"].AsRecord;
				ListValue asList = asRecord["dimensions"].AsList;
				ListValue asList2 = asRecord["metricHeader"].AsRecord["metricHeaderEntries"].AsList;
				int count = asList.Count;
				int count2 = asList2.Count;
				this.columnDataTypes = new GoogleAnalyticsDataType[count + count2];
				this.dimensionColumns = new bool[this.columnDataTypes.Length];
				for (int i = 0; i < this.columnDataTypes.Length; i++)
				{
					string text;
					string text2;
					if (i < count)
					{
						text = asList[i].AsText.AsString;
						text2 = "STRING";
						this.dimensionColumns[i] = true;
						if (text == "ga:date")
						{
							this.dateDimensionColumn = i;
						}
					}
					else
					{
						RecordValue asRecord2 = asList2[i - count].AsRecord;
						text = asRecord2["name"].AsText.AsString;
						text2 = asRecord2["type"].AsText.AsString;
						this.dimensionColumns[i] = false;
					}
					this.columnDataTypes[i] = GoogleAnalyticsServiceV1.DecodeGoogleAnalyticsDataType(text, text2);
				}
				return value2.AsList;
			}

			// Token: 0x06004E2F RID: 20015 RVA: 0x001036E8 File Offset: 0x001018E8
			protected override IValueReference[] ExtractRowValues(RecordValue row)
			{
				IValueReference[] array = new IValueReference[this.keys.Length];
				ListValue asList = row["dimensions"].AsList;
				ListValue asList2 = row["metrics"].AsList[0].AsRecord["values"].AsList;
				for (int i = 0; i < asList.Count + asList2.Count; i++)
				{
					array[i] = GoogleAnalyticsServiceV1.MarshallGoogleAnalyticsDataType(((i < asList.Count) ? asList[i] : asList2[i - asList.Count]).AsText, this.columnDataTypes[i], this.dimensionColumns[i]);
				}
				return array;
			}
		}

		// Token: 0x02000B00 RID: 2816
		private sealed class GoogleAnalyticsResultEnumeratorV2 : GoogleAnalyticsCubeContextProvider.GoogleAnalyticsResultEnumerator
		{
			// Token: 0x06004E30 RID: 20016 RVA: 0x0010356E File Offset: 0x0010176E
			public GoogleAnalyticsResultEnumeratorV2(GoogleAnalyticsCube cube, GoogleAnalyticsQueryExpression compiledExpression, CubeExpression filterExpression, Keys keys)
				: base(cube, compiledExpression, filterExpression, keys)
			{
			}

			// Token: 0x06004E31 RID: 20017 RVA: 0x00103798 File Offset: 0x00101998
			protected override ListValue GetResult(Value webResult)
			{
				Value value;
				if (!webResult.TryGetValue("rows", out value))
				{
					return null;
				}
				Value value2;
				ListValue listValue;
				if (webResult.TryGetValue("dimensionHeaders", out value2))
				{
					listValue = value2.AsList;
				}
				else
				{
					listValue = ListValue.Empty;
				}
				ListValue asList = webResult["metricHeaders"].AsList;
				int count = listValue.Count;
				int count2 = asList.Count;
				this.columnDataTypes = new GoogleAnalyticsDataType[count + count2];
				this.dimensionColumns = new bool[this.columnDataTypes.Length];
				for (int i = 0; i < this.columnDataTypes.Length; i++)
				{
					string text;
					string text2;
					if (i < count)
					{
						text = listValue[i]["name"].AsText.AsString;
						text2 = "STRING";
						this.dimensionColumns[i] = true;
						if (text == "date")
						{
							this.dateDimensionColumn = i;
						}
					}
					else
					{
						RecordValue asRecord = asList[i - count].AsRecord;
						text = asRecord["name"].AsText.AsString;
						text2 = asRecord["type"].AsText.AsString;
						this.dimensionColumns[i] = false;
					}
					this.columnDataTypes[i] = GoogleAnalyticsServiceV2.DecodeGoogleAnalyticsDataType(text, text2);
				}
				return value.AsList;
			}

			// Token: 0x06004E32 RID: 20018 RVA: 0x001038E4 File Offset: 0x00101AE4
			protected override IValueReference[] ExtractRowValues(RecordValue row)
			{
				IValueReference[] array = new IValueReference[this.keys.Length];
				Value value;
				ListValue listValue;
				if (row.TryGetValue("dimensionValues", out value))
				{
					listValue = value.AsList;
				}
				else
				{
					listValue = ListValue.Empty;
				}
				ListValue asList = row["metricValues"].AsList;
				for (int i = 0; i < listValue.Count + asList.Count; i++)
				{
					Value value2 = ((i < listValue.Count) ? listValue[i] : asList[i - listValue.Count]);
					array[i] = GoogleAnalyticsServiceV2.MarshallGoogleAnalyticsDataType(value2["value"].AsText, this.columnDataTypes[i], this.dimensionColumns[i]);
				}
				return array;
			}
		}
	}
}
