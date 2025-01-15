using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200003C RID: 60
	internal sealed class MatrixMember : Group, IDocumentMapEntry
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x0001064C File Offset: 0x0000E84C
		internal MatrixMember(Matrix owner, MatrixMember parent, MatrixHeading headingDef, MatrixHeadingInstance headingInstance, bool isSubtotal, bool isParentSubTotal, int index)
			: base(owner, headingDef.Grouping, headingDef.Visibility)
		{
			this.m_parent = parent;
			this.m_headingDef = headingDef;
			this.m_headingInstance = headingInstance;
			this.m_isSubtotal = isSubtotal;
			this.m_isParentSubTotal = isParentSubTotal;
			this.m_index = index;
			if (this.m_headingInstance != null)
			{
				this.m_uniqueName = this.m_headingInstance.UniqueName;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x000106BC File Offset: 0x0000E8BC
		public override string ID
		{
			get
			{
				if (this.m_isSubtotal)
				{
					if (this.m_headingDef.Subtotal.RenderingModelID == null)
					{
						this.m_headingDef.Subtotal.RenderingModelID = this.m_headingDef.Subtotal.ID.ToString(CultureInfo.InvariantCulture);
					}
					return this.m_headingDef.Subtotal.RenderingModelID;
				}
				if (this.m_headingDef.Grouping == null)
				{
					if (this.m_headingDef.RenderingModelIDs == null)
					{
						this.m_headingDef.RenderingModelIDs = new string[this.m_headingDef.ReportItems.Count];
					}
					if (this.m_headingDef.RenderingModelIDs[this.m_index] == null)
					{
						this.m_headingDef.RenderingModelIDs[this.m_index] = this.m_headingDef.IDs[this.m_index].ToString(CultureInfo.InvariantCulture);
					}
					return this.m_headingDef.RenderingModelIDs[this.m_index];
				}
				if (this.m_headingDef.RenderingModelID == null)
				{
					this.m_headingDef.RenderingModelID = this.m_headingDef.ID.ToString(CultureInfo.InvariantCulture);
				}
				return this.m_headingDef.RenderingModelID;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x000107F8 File Offset: 0x0000E9F8
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00010874 File Offset: 0x0000EA74
		public object SharedRenderingInfo
		{
			get
			{
				int num;
				if (this.m_isSubtotal)
				{
					num = this.m_headingDef.Subtotal.ID;
				}
				else if (this.m_headingDef.Grouping == null)
				{
					num = this.m_headingDef.IDs[this.m_index];
				}
				else
				{
					num = this.m_headingDef.ID;
				}
				return base.OwnerDataRegion.RenderingContext.RenderingInfoManager.SharedRenderingInfo[num];
			}
			set
			{
				int num;
				if (this.m_isSubtotal)
				{
					num = this.m_headingDef.Subtotal.ID;
				}
				else if (this.m_headingDef.Grouping == null)
				{
					num = this.m_headingDef.IDs[this.m_index];
				}
				else
				{
					num = this.m_headingDef.ID;
				}
				base.OwnerDataRegion.RenderingContext.RenderingInfoManager.SharedRenderingInfo[num] = value;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000108EF File Offset: 0x0000EAEF
		internal ReportSize Size
		{
			get
			{
				if (this.m_headingDef.SizeForRendering == null)
				{
					this.m_headingDef.SizeForRendering = new ReportSize(this.m_headingDef.Size, this.m_headingDef.SizeValue);
				}
				return this.m_headingDef.SizeForRendering;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001092F File Offset: 0x0000EB2F
		internal override TextBox ToggleParent
		{
			get
			{
				if (this.m_isSubtotal || this.IsStatic)
				{
					return null;
				}
				if (Visibility.HasToggle(this.m_visibilityDef))
				{
					return base.OwnerDataRegion.RenderingContext.GetToggleParent(this.m_uniqueName);
				}
				return null;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00010968 File Offset: 0x0000EB68
		public override bool HasToggle
		{
			get
			{
				return !this.m_isSubtotal && !this.IsStatic && Visibility.HasToggle(this.m_visibilityDef);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00010987 File Offset: 0x0000EB87
		public override string ToggleItem
		{
			get
			{
				if (this.m_isSubtotal || this.IsStatic)
				{
					return null;
				}
				return base.ToggleItem;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x000109A1 File Offset: 0x0000EBA1
		public override SharedHiddenState SharedHidden
		{
			get
			{
				if (this.m_isSubtotal)
				{
					if (!this.m_headingDef.Subtotal.AutoDerived)
					{
						return SharedHiddenState.Never;
					}
					return SharedHiddenState.Always;
				}
				else
				{
					if (this.IsStatic)
					{
						return SharedHiddenState.Never;
					}
					return Visibility.GetSharedHidden(this.m_visibilityDef);
				}
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x000109D6 File Offset: 0x0000EBD6
		public override bool IsToggleChild
		{
			get
			{
				return !this.m_isSubtotal && !this.IsStatic && base.OwnerDataRegion.RenderingContext.IsToggleChild(this.m_uniqueName);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00010A00 File Offset: 0x0000EC00
		public override bool Hidden
		{
			get
			{
				if (this.m_isSubtotal)
				{
					return this.m_headingDef.Subtotal.AutoDerived;
				}
				if (this.m_headingInstance == null)
				{
					return RenderingContext.GetDefinitionHidden(this.m_headingDef.Visibility);
				}
				if (this.m_headingDef.Visibility == null)
				{
					return false;
				}
				if (this.m_headingDef.Visibility.Toggle != null)
				{
					return base.OwnerDataRegion.RenderingContext.IsItemHidden(this.m_headingInstance.UniqueName, false);
				}
				return this.InstanceInfo.StartHidden;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00010A88 File Offset: 0x0000EC88
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				CustomPropertyCollection customPropertyCollection = this.m_customProperties;
				if (this.m_customProperties == null)
				{
					if (this.m_headingDef.Grouping == null || this.m_headingDef.Grouping.CustomProperties == null)
					{
						return null;
					}
					if (this.m_headingInstance == null)
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_headingDef.Grouping.CustomProperties, null);
					}
					else
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_headingDef.Grouping.CustomProperties, this.InstanceInfo.CustomPropertyInstances);
					}
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_customProperties = customPropertyCollection;
					}
				}
				return customPropertyCollection;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00010B24 File Offset: 0x0000ED24
		public ReportItem ReportItem
		{
			get
			{
				ReportItem reportItem = this.m_reportItem;
				if (this.m_reportItem == null)
				{
					ReportItemInstance reportItemInstance = null;
					NonComputedUniqueNames nonComputedUniqueNames = null;
					ReportItem reportItem2;
					if (this.m_isSubtotal)
					{
						reportItem2 = this.m_headingDef.Subtotal.ReportItem;
					}
					else if (this.m_headingDef.Grouping == null)
					{
						reportItem2 = this.m_headingDef.ReportItems[this.m_index];
					}
					else
					{
						reportItem2 = this.m_headingDef.ReportItem;
					}
					if (this.m_headingInstance != null)
					{
						nonComputedUniqueNames = this.InstanceInfo.ContentUniqueNames;
						reportItemInstance = this.m_headingInstance.Content;
					}
					if (reportItem2 != null)
					{
						reportItem = ReportItem.CreateItem(0, reportItem2, reportItemInstance, base.OwnerDataRegion.RenderingContext, nonComputedUniqueNames);
					}
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_reportItem = reportItem;
					}
				}
				return reportItem;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00010BE8 File Offset: 0x0000EDE8
		public override string Label
		{
			get
			{
				string text = null;
				if (this.m_groupingDef != null && this.m_groupingDef.GroupLabel != null)
				{
					if (this.m_groupingDef.GroupLabel.Type == ExpressionInfo.Types.Constant)
					{
						text = this.m_groupingDef.GroupLabel.Value;
					}
					else if (this.m_headingInstance == null)
					{
						text = null;
					}
					else
					{
						text = this.InstanceInfo.Label;
					}
				}
				return text;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00010C4B File Offset: 0x0000EE4B
		public bool InDocumentMap
		{
			get
			{
				return this.m_headingInstance != null && this.m_groupingDef != null && this.m_groupingDef.GroupLabel != null && !this.m_isSubtotal;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00010C75 File Offset: 0x0000EE75
		public MatrixMember Parent
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00010C80 File Offset: 0x0000EE80
		public MatrixMemberCollection Children
		{
			get
			{
				MatrixHeading matrixHeading = this.m_headingDef.SubHeading;
				if (matrixHeading == null)
				{
					return null;
				}
				MatrixMemberCollection matrixMemberCollection = this.m_children;
				if (this.m_children == null)
				{
					MatrixHeadingInstanceList matrixHeadingInstanceList = null;
					if (this.m_headingInstance != null)
					{
						if (this.m_headingInstance.SubHeadingInstances == null || this.m_headingInstance.SubHeadingInstances.Count == 0)
						{
							return this.m_children;
						}
						matrixHeadingInstanceList = this.m_headingInstance.SubHeadingInstances;
						if (this.m_headingInstance.IsSubtotal)
						{
							matrixHeading = (MatrixHeading)this.m_headingDef.GetInnerStaticHeading();
						}
					}
					else if (this.m_isSubtotal)
					{
						return this.m_children;
					}
					List<int> list = Matrix.CalculateMapping(matrixHeading, matrixHeadingInstanceList, this.m_isSubtotal || this.m_isParentSubTotal);
					matrixMemberCollection = new MatrixMemberCollection((Matrix)base.OwnerDataRegion, this, matrixHeading, matrixHeadingInstanceList, list, this.m_isSubtotal);
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_children = matrixMemberCollection;
					}
				}
				return matrixMemberCollection;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00010D69 File Offset: 0x0000EF69
		public int MemberCellIndex
		{
			get
			{
				if (this.m_headingInstance != null)
				{
					return this.InstanceInfo.HeadingCellIndex;
				}
				if (this.m_headingDef.Grouping == null)
				{
					return this.m_index;
				}
				return 0;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00010D94 File Offset: 0x0000EF94
		internal int CachedMemberCellIndex
		{
			get
			{
				if (this.m_cachedMemberCellIndex < 0)
				{
					this.m_cachedMemberCellIndex = this.MemberCellIndex;
				}
				return this.m_cachedMemberCellIndex;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00010DB4 File Offset: 0x0000EFB4
		public int ColumnSpan
		{
			get
			{
				Matrix matrix = (Matrix)base.OwnerDataRegion.ReportItemDef;
				if (this.m_headingDef.IsColumn)
				{
					if (this.m_headingInstance != null)
					{
						return this.InstanceInfo.HeadingSpan;
					}
					MatrixHeading matrixHeading = (MatrixHeading)this.m_headingDef.GetInnerStaticHeading();
					if (matrixHeading != null)
					{
						return matrix.MatrixColumns.Count;
					}
				}
				else if (this.m_isSubtotal || this.m_isParentSubTotal)
				{
					return this.m_headingDef.SubtotalSpan;
				}
				return 1;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00010E34 File Offset: 0x0000F034
		public int RowSpan
		{
			get
			{
				Matrix matrix = (Matrix)base.OwnerDataRegion.ReportItemDef;
				if (this.m_headingDef.IsColumn)
				{
					if (this.m_isSubtotal || this.m_isParentSubTotal)
					{
						return this.m_headingDef.SubtotalSpan;
					}
				}
				else
				{
					if (this.m_headingInstance != null)
					{
						return this.InstanceInfo.HeadingSpan;
					}
					MatrixHeading matrixHeading = (MatrixHeading)this.m_headingDef.GetInnerStaticHeading();
					if (matrixHeading != null)
					{
						return matrix.MatrixRows.Count;
					}
				}
				return 1;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00010EB2 File Offset: 0x0000F0B2
		public bool IsTotal
		{
			get
			{
				return this.m_isSubtotal;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00010EBA File Offset: 0x0000F0BA
		public bool IsStatic
		{
			get
			{
				return this.m_headingDef.Grouping == null;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00010ECC File Offset: 0x0000F0CC
		public ReportSize Width
		{
			get
			{
				ReportSize reportSize = this.m_width;
				if (this.m_width == null)
				{
					if (this.m_headingDef.IsColumn)
					{
						double num = 0.0;
						SizeCollection cellWidths = ((Matrix)base.OwnerDataRegion).CellWidths;
						if ((MatrixHeading)this.m_headingDef.GetInnerStaticHeading() == null)
						{
							num = (double)this.ColumnSpan * cellWidths[this.MemberCellIndex].ToMillimeters();
							num = Math.Round(num, Validator.DecimalPrecision);
						}
						else
						{
							for (int i = 0; i < this.ColumnSpan; i++)
							{
								num += cellWidths[this.MemberCellIndex + i].ToMillimeters();
								num = Math.Round(num, Validator.DecimalPrecision);
							}
						}
						reportSize = new ReportSize(num.ToString() + "mm", num);
					}
					else if ((this.m_isSubtotal || this.m_isParentSubTotal) && 1 != this.m_headingDef.SubtotalSpan)
					{
						double num2 = 0.0;
						MatrixHeading matrixHeading = this.m_headingDef;
						for (int j = 0; j < this.m_headingDef.SubtotalSpan; j++)
						{
							num2 += matrixHeading.SizeValue;
							num2 = Math.Round(num2, Validator.DecimalPrecision);
							matrixHeading = matrixHeading.SubHeading;
						}
						reportSize = new ReportSize(num2.ToString() + "mm", num2);
					}
					else
					{
						reportSize = this.Size;
					}
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_width = reportSize;
					}
				}
				return reportSize;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00011054 File Offset: 0x0000F254
		public ReportSize Height
		{
			get
			{
				ReportSize reportSize = this.m_height;
				if (this.m_height == null)
				{
					if (this.m_headingDef.IsColumn)
					{
						if ((this.m_isSubtotal || this.m_isParentSubTotal) && 1 != this.m_headingDef.SubtotalSpan)
						{
							double num = 0.0;
							MatrixHeading matrixHeading = this.m_headingDef;
							for (int i = 0; i < this.m_headingDef.SubtotalSpan; i++)
							{
								num += matrixHeading.SizeValue;
								num = Math.Round(num, Validator.DecimalPrecision);
								matrixHeading = matrixHeading.SubHeading;
							}
							reportSize = new ReportSize(num.ToString() + "mm", num);
						}
						else
						{
							reportSize = this.Size;
						}
					}
					else
					{
						double num2 = 0.0;
						SizeCollection cellHeights = ((Matrix)base.OwnerDataRegion).CellHeights;
						if ((MatrixHeading)this.m_headingDef.GetInnerStaticHeading() == null)
						{
							num2 = (double)this.RowSpan * cellHeights[this.MemberCellIndex].ToMillimeters();
							num2 = Math.Round(num2, Validator.DecimalPrecision);
						}
						else
						{
							for (int j = 0; j < this.RowSpan; j++)
							{
								num2 += cellHeights[this.MemberCellIndex + j].ToMillimeters();
								num2 = Math.Round(num2, Validator.DecimalPrecision);
							}
						}
						reportSize = new ReportSize(num2.ToString() + "mm", num2);
					}
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_height = reportSize;
					}
				}
				return reportSize;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x000111DC File Offset: 0x0000F3DC
		public object GroupValue
		{
			get
			{
				if (!this.m_isSubtotal && this.m_headingDef.OwcGroupExpression)
				{
					return this.InstanceInfo.GroupExpressionValue;
				}
				return null;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00011200 File Offset: 0x0000F400
		public MatrixMember.SortOrders SortOrder
		{
			get
			{
				MatrixMember.SortOrders sortOrders = MatrixMember.SortOrders.None;
				if (!this.IsStatic)
				{
					BoolList boolList;
					if (this.m_headingDef.Sorting != null)
					{
						boolList = this.m_headingDef.Sorting.SortDirections;
					}
					else
					{
						boolList = this.m_headingDef.Grouping.SortDirections;
					}
					if (boolList != null && 0 < boolList.Count)
					{
						if (boolList[0])
						{
							sortOrders = MatrixMember.SortOrders.Ascending;
						}
						else
						{
							sortOrders = MatrixMember.SortOrders.Descending;
						}
					}
				}
				return sortOrders;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00011264 File Offset: 0x0000F464
		public override string DataElementName
		{
			get
			{
				if (this.IsTotal)
				{
					return this.m_headingDef.Subtotal.DataElementName;
				}
				if (this.IsStatic)
				{
					return this.m_headingDef.ReportItems[this.m_index].DataElementName;
				}
				return base.DataElementName;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000112B4 File Offset: 0x0000F4B4
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				Global.Tracer.Assert(!this.IsTotal || !this.IsStatic);
				if (this.IsTotal)
				{
					return this.m_headingDef.Subtotal.DataElementOutput;
				}
				if (this.IsStatic)
				{
					return this.DataElementOutputForStatic(null);
				}
				return base.DataElementOutput;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001130E File Offset: 0x0000F50E
		internal MatrixHeadingInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_headingInstance == null)
				{
					return null;
				}
				if (this.m_headingInstanceInfo == null)
				{
					this.m_headingInstanceInfo = this.m_headingInstance.GetInstanceInfo(base.OwnerDataRegion.RenderingContext.ChunkManager);
				}
				return this.m_headingInstanceInfo;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00011349 File Offset: 0x0000F549
		internal bool IsParentSubtotal
		{
			get
			{
				return this.m_isParentSubTotal;
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00011354 File Offset: 0x0000F554
		public DataElementOutputTypes DataElementOutputForStatic(MatrixMember staticHeading)
		{
			if (!this.IsStatic)
			{
				return this.DataElementOutput;
			}
			if (staticHeading != null && (!staticHeading.IsStatic || staticHeading.Parent == this.Parent))
			{
				staticHeading = null;
			}
			if (staticHeading != null)
			{
				int num;
				int num2;
				if (this.m_headingDef.IsColumn)
				{
					num = staticHeading.m_index;
					num2 = this.m_index;
				}
				else
				{
					num = this.m_index;
					num2 = staticHeading.m_index;
				}
				return this.GetDataElementOutputTypeFromCell(num, num2);
			}
			Matrix matrix = (Matrix)base.OwnerDataRegion.ReportItemDef;
			if (matrix.PivotStaticColumns == null || matrix.PivotStaticRows == null)
			{
				return this.GetDataElementOutputTypeFromCell(0, this.m_index);
			}
			Global.Tracer.Assert(matrix.PivotStaticColumns != null && matrix.PivotStaticRows != null);
			return this.GetDataElementOutputTypeForRowCol(this.m_index);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001141C File Offset: 0x0000F61C
		public bool IsRowMemberOnThisPage(int memberIndex, int pageNumber, out int startPage, out int endPage)
		{
			startPage = -1;
			endPage = -1;
			RenderingPagesRangesList childrenStartAndEndPages = this.m_headingInstance.ChildrenStartAndEndPages;
			if (childrenStartAndEndPages == null)
			{
				return true;
			}
			Global.Tracer.Assert(memberIndex >= 0 && memberIndex < childrenStartAndEndPages.Count);
			if (memberIndex >= childrenStartAndEndPages.Count)
			{
				return false;
			}
			RenderingPagesRanges renderingPagesRanges = childrenStartAndEndPages[memberIndex];
			startPage = renderingPagesRanges.StartPage;
			endPage = renderingPagesRanges.EndPage;
			return pageNumber >= startPage && pageNumber <= endPage;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00011491 File Offset: 0x0000F691
		private DataElementOutputTypes GetDataElementOutputTypeFromCell(int rowIndex, int columnIndex)
		{
			return ((Matrix)base.OwnerDataRegion.ReportItemDef).GetCellReportItem(rowIndex, columnIndex).DataElementOutput;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000114B0 File Offset: 0x0000F6B0
		private DataElementOutputTypes GetDataElementOutputTypeForRowCol(int index)
		{
			Matrix matrix = (Matrix)base.OwnerDataRegion.ReportItemDef;
			int num;
			int num2;
			int num3;
			if (this.m_headingDef.IsColumn)
			{
				num = 0;
				num2 = index;
				num3 = matrix.MatrixRows.Count;
			}
			else
			{
				num = index;
				num2 = 0;
				num3 = matrix.MatrixColumns.Count;
			}
			while (matrix.GetCellReportItem(num, num2).DataElementOutput == DataElementOutputTypes.NoOutput)
			{
				if (this.m_headingDef.IsColumn)
				{
					num++;
					if (num < num3)
					{
						continue;
					}
				}
				else
				{
					num2++;
					if (num2 < num3)
					{
						continue;
					}
				}
				return DataElementOutputTypes.NoOutput;
			}
			return DataElementOutputTypes.Output;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00011530 File Offset: 0x0000F730
		public void GetChildRowMembersOnPage(int page, out int startChild, out int endChild)
		{
			startChild = -1;
			endChild = -1;
			if (this.m_headingInstance == null)
			{
				return;
			}
			RenderingPagesRangesList childrenStartAndEndPages = this.m_headingInstance.ChildrenStartAndEndPages;
			if (childrenStartAndEndPages == null)
			{
				return;
			}
			RenderingContext.FindRange(childrenStartAndEndPages, 0, childrenStartAndEndPages.Count - 1, page, ref startChild, ref endChild);
		}

		// Token: 0x0400011D RID: 285
		private MatrixHeading m_headingDef;

		// Token: 0x0400011E RID: 286
		private MatrixHeadingInstance m_headingInstance;

		// Token: 0x0400011F RID: 287
		private MatrixHeadingInstanceInfo m_headingInstanceInfo;

		// Token: 0x04000120 RID: 288
		private ReportItem m_reportItem;

		// Token: 0x04000121 RID: 289
		private MatrixMemberCollection m_children;

		// Token: 0x04000122 RID: 290
		private MatrixMember m_parent;

		// Token: 0x04000123 RID: 291
		private ReportSize m_width;

		// Token: 0x04000124 RID: 292
		private ReportSize m_height;

		// Token: 0x04000125 RID: 293
		private bool m_isSubtotal;

		// Token: 0x04000126 RID: 294
		private bool m_isParentSubTotal;

		// Token: 0x04000127 RID: 295
		private int m_index;

		// Token: 0x04000128 RID: 296
		private int m_cachedMemberCellIndex = -1;

		// Token: 0x0200090F RID: 2319
		public enum SortOrders
		{
			// Token: 0x04003EED RID: 16109
			None,
			// Token: 0x04003EEE RID: 16110
			Ascending,
			// Token: 0x04003EEF RID: 16111
			Descending
		}
	}
}
