using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000072 RID: 114
	internal sealed class ProjectedDsqExpressionValue : IProjectedDsqExpressionValue
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x00012278 File Offset: 0x00010478
		internal ProjectedDsqExpressionValue(ExpressionNode dsqExpr, string formatString, IConceptualProperty lineageProperty)
		{
			this._dsqExpr = dsqExpr;
			this._formatString = formatString;
			this._lineageProperty = lineageProperty;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00012295 File Offset: 0x00010495
		public ExpressionNode DsqExpression
		{
			get
			{
				return this._dsqExpr;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0001229D File Offset: 0x0001049D
		public string FormatString
		{
			get
			{
				return this._formatString;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000122A5 File Offset: 0x000104A5
		public IConceptualProperty LineageProperty
		{
			get
			{
				return this._lineageProperty;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x000122AD File Offset: 0x000104AD
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x000122B5 File Offset: 0x000104B5
		public ProjectedDsqExpression DynamicFormatString
		{
			get
			{
				return this._dynamicFormatString;
			}
			set
			{
				this._dynamicFormatString = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x000122BE File Offset: 0x000104BE
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x000122C6 File Offset: 0x000104C6
		public ProjectedDsqExpression DynamicFormatCulture
		{
			get
			{
				return this._dynamicFormatCulture;
			}
			set
			{
				this._dynamicFormatCulture = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x000122CF File Offset: 0x000104CF
		public bool IsEmpty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000122D2 File Offset: 0x000104D2
		public IProjectedDsqExpressionValue CloneWithOverrides(ExpressionNode dsqExpression = null)
		{
			return new ProjectedDsqExpressionValue(dsqExpression ?? this._dsqExpr, this._formatString, this._lineageProperty);
		}

		// Token: 0x040002A6 RID: 678
		private readonly ExpressionNode _dsqExpr;

		// Token: 0x040002A7 RID: 679
		private readonly string _formatString;

		// Token: 0x040002A8 RID: 680
		private readonly IConceptualProperty _lineageProperty;

		// Token: 0x040002A9 RID: 681
		private ProjectedDsqExpression _dynamicFormatString;

		// Token: 0x040002AA RID: 682
		private ProjectedDsqExpression _dynamicFormatCulture;
	}
}
