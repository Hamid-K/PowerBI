using System;
using System.Collections.Generic;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CE5 RID: 3301
	internal class QueryCubeExpression : CubeExpression
	{
		// Token: 0x0600598D RID: 22925 RVA: 0x00139814 File Offset: 0x00137A14
		public QueryCubeExpression(CubeExpression from)
			: this(from, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, null, EmptyArray<CubeSortOrder>.Instance, RowRange.All)
		{
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x00139848 File Offset: 0x00137A48
		public QueryCubeExpression(CubeExpression from, IList<IdentifierCubeExpression> dimensionAttributes, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> measureProperties, CubeExpression filter, IList<CubeSortOrder> sort, RowRange range)
		{
			this.from = from;
			this.dimensionAttributes = dimensionAttributes.ToArray<IdentifierCubeExpression>();
			this.properties = properties.ToArray<IdentifierCubeExpression>();
			this.measures = measures.ToArray<IdentifierCubeExpression>();
			this.measureProperties = measureProperties.ToArray<IdentifierCubeExpression>();
			this.filter = filter;
			this.sort = sort.ToArray<CubeSortOrder>();
			this.rowRange = range;
		}

		// Token: 0x17001AC9 RID: 6857
		// (get) Token: 0x0600598F RID: 22927 RVA: 0x0000240C File Offset: 0x0000060C
		public override CubeExpressionKind Kind
		{
			get
			{
				return CubeExpressionKind.Query;
			}
		}

		// Token: 0x17001ACA RID: 6858
		// (get) Token: 0x06005990 RID: 22928 RVA: 0x001398B1 File Offset: 0x00137AB1
		public CubeExpression From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x17001ACB RID: 6859
		// (get) Token: 0x06005991 RID: 22929 RVA: 0x001398B9 File Offset: 0x00137AB9
		public IList<IdentifierCubeExpression> DimensionAttributes
		{
			get
			{
				return this.dimensionAttributes;
			}
		}

		// Token: 0x17001ACC RID: 6860
		// (get) Token: 0x06005992 RID: 22930 RVA: 0x001398C1 File Offset: 0x00137AC1
		public IList<IdentifierCubeExpression> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x06005993 RID: 22931 RVA: 0x001398C9 File Offset: 0x00137AC9
		public IList<IdentifierCubeExpression> Measures
		{
			get
			{
				return this.measures;
			}
		}

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x06005994 RID: 22932 RVA: 0x001398D1 File Offset: 0x00137AD1
		public IList<IdentifierCubeExpression> MeasureProperties
		{
			get
			{
				return this.measureProperties;
			}
		}

		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x06005995 RID: 22933 RVA: 0x001398D9 File Offset: 0x00137AD9
		public CubeExpression Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x17001AD0 RID: 6864
		// (get) Token: 0x06005996 RID: 22934 RVA: 0x001398E1 File Offset: 0x00137AE1
		public IList<CubeSortOrder> Sort
		{
			get
			{
				return this.sort;
			}
		}

		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x06005997 RID: 22935 RVA: 0x001398E9 File Offset: 0x00137AE9
		public RowRange RowRange
		{
			get
			{
				return this.rowRange;
			}
		}

		// Token: 0x04003221 RID: 12833
		private readonly CubeExpression from;

		// Token: 0x04003222 RID: 12834
		private readonly IdentifierCubeExpression[] dimensionAttributes;

		// Token: 0x04003223 RID: 12835
		private readonly IdentifierCubeExpression[] properties;

		// Token: 0x04003224 RID: 12836
		private readonly IdentifierCubeExpression[] measures;

		// Token: 0x04003225 RID: 12837
		private readonly IdentifierCubeExpression[] measureProperties;

		// Token: 0x04003226 RID: 12838
		private readonly CubeExpression filter;

		// Token: 0x04003227 RID: 12839
		private readonly CubeSortOrder[] sort;

		// Token: 0x04003228 RID: 12840
		private readonly RowRange rowRange;
	}
}
