using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000A0 RID: 160
	internal sealed class Tablix : DataRegion
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0000D0E0 File Offset: 0x0000B2E0
		internal Tablix(string name, ReportItemRect rect, int zIndex, ReportParsingDiagnosticContext diagnosticContext, string dataSetName, TablixHierarchy columnHierarchy, TablixHierarchy rowHierarchy, TablixBody body, string subType, bool isCallout, string cardStyle, IFilterCondition<IRdmQueryExpression> slicerFilterCondition, BandLayoutOptions bandLayoutOptions, int fontSizeOffset)
			: base("Tablix", name, rect, zIndex, diagnosticContext, dataSetName)
		{
			this._columnHierarchy = columnHierarchy;
			this._rowHierarchy = rowHierarchy;
			this._body = body;
			this._subType = subType;
			this._isCallout = isCallout;
			this._cardStyle = cardStyle;
			this._slicerFilterCondition = slicerFilterCondition;
			this._bandLayoutOptions = bandLayoutOptions;
			this._fontSizeOffset = fontSizeOffset;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000D147 File Offset: 0x0000B347
		public TablixHierarchy ColumnHierarchy
		{
			get
			{
				return this._columnHierarchy;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000D14F File Offset: 0x0000B34F
		public TablixHierarchy RowHierarchy
		{
			get
			{
				return this._rowHierarchy;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000D157 File Offset: 0x0000B357
		public TablixBody Body
		{
			get
			{
				return this._body;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000D15F File Offset: 0x0000B35F
		public string SubType
		{
			get
			{
				return this._subType;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000D167 File Offset: 0x0000B367
		public bool IsCallout
		{
			get
			{
				return this._isCallout;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000D16F File Offset: 0x0000B36F
		public string CardStyle
		{
			get
			{
				return this._cardStyle;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000D177 File Offset: 0x0000B377
		public IFilterCondition<IRdmQueryExpression> SlicerFilterCondition
		{
			get
			{
				return this._slicerFilterCondition;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000D17F File Offset: 0x0000B37F
		public BandLayoutOptions BandLayoutOptions
		{
			get
			{
				return this._bandLayoutOptions;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000D187 File Offset: 0x0000B387
		public int FontSizeOffset
		{
			get
			{
				return this._fontSizeOffset;
			}
		}

		// Token: 0x0400020E RID: 526
		private readonly TablixHierarchy _columnHierarchy;

		// Token: 0x0400020F RID: 527
		private readonly TablixHierarchy _rowHierarchy;

		// Token: 0x04000210 RID: 528
		private readonly TablixBody _body;

		// Token: 0x04000211 RID: 529
		private readonly string _subType;

		// Token: 0x04000212 RID: 530
		private readonly bool _isCallout;

		// Token: 0x04000213 RID: 531
		private readonly string _cardStyle;

		// Token: 0x04000214 RID: 532
		private readonly IFilterCondition<IRdmQueryExpression> _slicerFilterCondition;

		// Token: 0x04000215 RID: 533
		private readonly BandLayoutOptions _bandLayoutOptions;

		// Token: 0x04000216 RID: 534
		private readonly int _fontSizeOffset;
	}
}
