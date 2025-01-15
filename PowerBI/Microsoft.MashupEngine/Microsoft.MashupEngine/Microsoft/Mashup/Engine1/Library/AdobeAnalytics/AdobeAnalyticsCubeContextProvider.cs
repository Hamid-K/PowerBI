using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F54 RID: 3924
	internal abstract class AdobeAnalyticsCubeContextProvider<TDescription> : CubeContextProvider where TDescription : class
	{
		// Token: 0x060067A8 RID: 26536 RVA: 0x00164E18 File Offset: 0x00163018
		public AdobeAnalyticsCubeContextProvider(AdobeAnalyticsCube cube)
		{
			this.CubeId = new IdentifierCubeExpression(cube.Id);
			this.cube = cube;
		}

		// Token: 0x060067A9 RID: 26537
		protected abstract void AddDateGranularityDimension(CubeObjectTableBuilder dimensionsTableBuilder);

		// Token: 0x060067AA RID: 26538
		protected abstract void AddDateGranularityDisplayFolder(CubeObjectTableBuilder rootBuilder);

		// Token: 0x060067AB RID: 26539
		protected abstract AdobeAnalyticsQueryCompiler<TDescription> CreateCompiler(AdobeAnalyticsCube cube, IList<ParameterArguments> parameters);

		// Token: 0x060067AC RID: 26540
		protected abstract IEnumerator<IValueReference> GetCubeContextEnumerator(TDescription compiledReport, Keys keys);

		// Token: 0x060067AD RID: 26541
		protected abstract bool IsDateGranularity(string dimension);

		// Token: 0x17001DFC RID: 7676
		// (get) Token: 0x060067AE RID: 26542 RVA: 0x00164E38 File Offset: 0x00163038
		public override IResource Resource
		{
			get
			{
				return AdobeAnalyticsService.Resource;
			}
		}

		// Token: 0x17001DFD RID: 7677
		// (get) Token: 0x060067AF RID: 26543 RVA: 0x00164E3F File Offset: 0x0016303F
		private TableValue Dimensions
		{
			get
			{
				if (this.dimensionsTable == null)
				{
					this.CreateDimensionTable();
				}
				return this.dimensionsTable;
			}
		}

		// Token: 0x17001DFE RID: 7678
		// (get) Token: 0x060067B0 RID: 26544 RVA: 0x00164E55 File Offset: 0x00163055
		private TableValue Measures
		{
			get
			{
				if (this.measuresTable == null)
				{
					this.CreateMeasureTable();
				}
				return this.measuresTable;
			}
		}

		// Token: 0x17001DFF RID: 7679
		// (get) Token: 0x060067B1 RID: 26545 RVA: 0x00164E6B File Offset: 0x0016306B
		private TableValue DisplayFolders
		{
			get
			{
				if (this.displayFolders == null)
				{
					this.CreateDisplayFolders();
				}
				return this.displayFolders;
			}
		}

		// Token: 0x060067B2 RID: 26546 RVA: 0x00164E81 File Offset: 0x00163081
		public override bool TryCreateContext(QueryCubeExpression expression, IList<ParameterArguments> parameters, out CubeContext context)
		{
			if (this.CreateCompiler(this.cube, parameters).CanCompile(expression))
			{
				context = new AdobeAnalyticsCubeContextProvider<TDescription>.AdobeAnalyticsCubeContext(this, expression, parameters);
				return true;
			}
			context = null;
			return false;
		}

		// Token: 0x060067B3 RID: 26547 RVA: 0x00164EA8 File Offset: 0x001630A8
		public override CubeObjectKind GetObjectKind(IdentifierCubeExpression identifier)
		{
			switch (this.cube.GetCubeObject(identifier.Identifier).Kind)
			{
			case AdobeAnalyticsCubeObjectKind.Measure:
				return CubeObjectKind.Measure;
			case AdobeAnalyticsCubeObjectKind.Dimension:
				return CubeObjectKind.DimensionAttribute;
			case AdobeAnalyticsCubeObjectKind.Segment:
				return CubeObjectKind.Other;
			default:
				return CubeObjectKind.Other;
			}
		}

		// Token: 0x060067B4 RID: 26548 RVA: 0x00164EE7 File Offset: 0x001630E7
		public override string GetDisplayName(IdentifierCubeExpression identifier)
		{
			return this.cube.GetCubeObject(identifier.Identifier).Name;
		}

		// Token: 0x060067B5 RID: 26549 RVA: 0x00164F00 File Offset: 0x00163100
		public override TypeValue GetType(IdentifierCubeExpression identifier)
		{
			AdobeAnalyticsCubeObjectKind kind = this.cube.GetCubeObject(identifier.Identifier).Kind;
			if (kind == AdobeAnalyticsCubeObjectKind.Measure)
			{
				return TypeValue.Number;
			}
			if (kind != AdobeAnalyticsCubeObjectKind.Dimension)
			{
				return TypeValue.Any;
			}
			if (this.IsDateGranularity(identifier.Identifier))
			{
				return TypeValue.Number;
			}
			return TypeValue.Text;
		}

		// Token: 0x060067B6 RID: 26550 RVA: 0x00164F50 File Offset: 0x00163150
		public override IdentifierCubeExpression GetProperty(IdentifierCubeExpression dimensionAttribute, CubePropertyKind kind, string userDefinedIdentifier = null)
		{
			AdobeAnalyticsCubeObject cubeObject = this.cube.GetCubeObject(dimensionAttribute.Identifier);
			if (kind == CubePropertyKind.UniqueId)
			{
				return new IdentifierCubeExpression(cubeObject.Id);
			}
			if (kind != CubePropertyKind.Caption)
			{
				throw new InvalidOperationException("Unexpected MdxPropertyKind: " + kind.ToString());
			}
			return new IdentifierCubeExpression(cubeObject.Name);
		}

		// Token: 0x060067B7 RID: 26551 RVA: 0x000091AE File Offset: 0x000073AE
		public override IdentifierCubeExpression GetPropertyDimensionAttribute(IdentifierCubeExpression property)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060067B8 RID: 26552 RVA: 0x000091AE File Offset: 0x000073AE
		public override CubePropertyKind GetPropertyKind(IdentifierCubeExpression property)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060067B9 RID: 26553 RVA: 0x00164FAC File Offset: 0x001631AC
		protected virtual void CreateDimensionTable()
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.New();
			this.AddDateGranularityDimension(cubeObjectTableBuilder);
			foreach (AdobeAnalyticsCubeObject adobeAnalyticsCubeObject in this.cube.Dimensions)
			{
				List<IdentifierCubeExpression> list = new List<IdentifierCubeExpression>();
				KeysBuilder keysBuilder = default(KeysBuilder);
				list.Add(new IdentifierCubeExpression(adobeAnalyticsCubeObject.Id));
				keysBuilder.Add(adobeAnalyticsCubeObject.Id);
				CubeValue cubeValue = CubeContextCubeValue.New(this, new AdobeAnalyticsCubeContextProvider<TDescription>.AdobeAnalyticsCubeContext(this, new QueryCubeExpression(this.CubeId, list, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, null, EmptyArray<CubeSortOrder>.Instance, RowRange.All), EmptyArray<ParameterArguments>.Instance), keysBuilder.ToKeys());
				cubeObjectTableBuilder.AddDimension(adobeAnalyticsCubeObject.Id, adobeAnalyticsCubeObject.Name, cubeValue);
			}
			this.dimensionsTable = cubeObjectTableBuilder.ToTable();
		}

		// Token: 0x060067BA RID: 26554 RVA: 0x00165098 File Offset: 0x00163298
		protected virtual void CreateMeasureTable()
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.New();
			foreach (AdobeAnalyticsCubeObject adobeAnalyticsCubeObject in this.cube.Measures)
			{
				IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(adobeAnalyticsCubeObject.Id);
				MeasureValue measureValue = new MeasureValue(identifierCubeExpression, TypeValue.Any);
				cubeObjectTableBuilder.AddMeasure(identifierCubeExpression.Identifier, adobeAnalyticsCubeObject.Name, adobeAnalyticsCubeObject.Name, measureValue);
			}
			this.measuresTable = cubeObjectTableBuilder.ToTable();
		}

		// Token: 0x060067BB RID: 26555 RVA: 0x00165128 File Offset: 0x00163328
		protected virtual void CreateDisplayFolders()
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			this.AddMeasureDisplayFolder(cubeObjectTableBuilder);
			this.AddDateGranularityDisplayFolder(cubeObjectTableBuilder);
			this.AddDimensionDisplayFolder(cubeObjectTableBuilder);
			this.displayFolders = cubeObjectTableBuilder.ToTable();
			NavigationTableTypeValueBuilder navigationTableTypeValueBuilder = new NavigationTableTypeValueBuilder(this.displayFolders.Type.AsTableType, 1);
			navigationTableTypeValueBuilder.AddPreviewIdColumnEnabledDefault(true);
			this.displayFolders = this.displayFolders.NewType(navigationTableTypeValueBuilder.ToTypeValue()).AsTable;
		}

		// Token: 0x060067BC RID: 26556 RVA: 0x0016519C File Offset: 0x0016339C
		protected virtual void AddDimensionDisplayFolder(CubeObjectTableBuilder rootBuilder)
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			foreach (AdobeAnalyticsDimension adobeAnalyticsDimension in this.cube.Dimensions)
			{
				if (!this.IsDateGranularity(adobeAnalyticsDimension.Id))
				{
					CubeObjectTableBuilder cubeObjectTableBuilder2 = CubeObjectTableBuilder.NewWithoutLink();
					cubeObjectTableBuilder2.AddDimensionAttribute(adobeAnalyticsDimension.Id, adobeAnalyticsDimension.Name);
					cubeObjectTableBuilder.AddDimensionFolder(adobeAnalyticsDimension.Id, adobeAnalyticsDimension.Name, adobeAnalyticsDimension.Name, cubeObjectTableBuilder2.ToTable());
				}
			}
			rootBuilder.AddFolder("Dimensions", cubeObjectTableBuilder.ToTable());
		}

		// Token: 0x060067BD RID: 26557 RVA: 0x00165244 File Offset: 0x00163444
		protected virtual void AddMeasureDisplayFolder(CubeObjectTableBuilder rootBuilder)
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			foreach (AdobeAnalyticsMeasure adobeAnalyticsMeasure in this.cube.Measures)
			{
				IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(adobeAnalyticsMeasure.Id);
				MeasureValue measureValue = new MeasureValue(identifierCubeExpression, TypeValue.Any);
				cubeObjectTableBuilder.AddMeasure(identifierCubeExpression.Identifier, adobeAnalyticsMeasure.Name, measureValue);
			}
			rootBuilder.AddFolder("Measures", cubeObjectTableBuilder.ToTable());
		}

		// Token: 0x060067BE RID: 26558 RVA: 0x000091AE File Offset: 0x000073AE
		public override IdentifierCubeExpression GetMeasureProperty(IdentifierCubeExpression measureIdentifier, string propertyIdentifier)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04003914 RID: 14612
		public readonly IdentifierCubeExpression CubeId;

		// Token: 0x04003915 RID: 14613
		protected readonly AdobeAnalyticsCube cube;

		// Token: 0x04003916 RID: 14614
		private TableValue displayFolders;

		// Token: 0x04003917 RID: 14615
		private TableValue dimensionsTable;

		// Token: 0x04003918 RID: 14616
		private TableValue measuresTable;

		// Token: 0x02000F55 RID: 3925
		public sealed class AdobeAnalyticsCubeContext : CubeContext
		{
			// Token: 0x060067BF RID: 26559 RVA: 0x001652D4 File Offset: 0x001634D4
			public AdobeAnalyticsCubeContext(AdobeAnalyticsCubeContextProvider<TDescription> contextProvider, QueryCubeExpression expression, IList<ParameterArguments> arguments)
				: base(expression, arguments)
			{
				this.contextProvider = contextProvider;
			}

			// Token: 0x17001E00 RID: 7680
			// (get) Token: 0x060067C0 RID: 26560 RVA: 0x001652E5 File Offset: 0x001634E5
			public override TableValue DisplayFolders
			{
				get
				{
					return this.contextProvider.DisplayFolders;
				}
			}

			// Token: 0x17001E01 RID: 7681
			// (get) Token: 0x060067C1 RID: 26561 RVA: 0x00066554 File Offset: 0x00064754
			public override TableValue MeasureGroups
			{
				get
				{
					return TableValue.Empty;
				}
			}

			// Token: 0x17001E02 RID: 7682
			// (get) Token: 0x060067C2 RID: 26562 RVA: 0x001652F2 File Offset: 0x001634F2
			public override TableValue Dimensions
			{
				get
				{
					return this.contextProvider.Dimensions;
				}
			}

			// Token: 0x17001E03 RID: 7683
			// (get) Token: 0x060067C3 RID: 26563 RVA: 0x001652FF File Offset: 0x001634FF
			public override TableValue Measures
			{
				get
				{
					return this.contextProvider.Measures;
				}
			}

			// Token: 0x17001E04 RID: 7684
			// (get) Token: 0x060067C4 RID: 26564 RVA: 0x0016530C File Offset: 0x0016350C
			public override CubeContextProvider ContextProvider
			{
				get
				{
					return this.contextProvider;
				}
			}

			// Token: 0x17001E05 RID: 7685
			// (get) Token: 0x060067C5 RID: 26565 RVA: 0x00165314 File Offset: 0x00163514
			public override IEngineHost EngineHost
			{
				get
				{
					return this.contextProvider.EngineHost;
				}
			}

			// Token: 0x060067C6 RID: 26566 RVA: 0x00165321 File Offset: 0x00163521
			public override TableValue GetParameters(CubeValue cube)
			{
				if (this.parametersTable == null)
				{
					this.parametersTable = AdobeAnalyticsParametersTableValue.New(cube, this.contextProvider.cube);
				}
				return this.parametersTable;
			}

			// Token: 0x060067C7 RID: 26567 RVA: 0x0010322F File Offset: 0x0010142F
			public override TableValue GetAvailableProperties()
			{
				return CubePropertiesTableValue.Empty;
			}

			// Token: 0x060067C8 RID: 26568 RVA: 0x00066802 File Offset: 0x00064A02
			public override TableValue GetAvailableMeasureProperties()
			{
				return CubeMeasurePropertiesTableValue.Empty;
			}

			// Token: 0x060067C9 RID: 26569 RVA: 0x00165348 File Offset: 0x00163548
			public override IEnumerator<IValueReference> Evaluate()
			{
				AdobeAnalyticsQueryCompiler<TDescription> adobeAnalyticsQueryCompiler = this.contextProvider.CreateCompiler(this.contextProvider.cube, base.ParameterArguments);
				return this.contextProvider.GetCubeContextEnumerator(adobeAnalyticsQueryCompiler.Compile(base.CubeExpression), CubeContext.GetKeys(base.CubeExpression));
			}

			// Token: 0x04003919 RID: 14617
			private readonly AdobeAnalyticsCubeContextProvider<TDescription> contextProvider;

			// Token: 0x0400391A RID: 14618
			private TableValue parametersTable;
		}
	}
}
