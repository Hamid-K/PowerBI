using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000738 RID: 1848
	[Serializable]
	internal sealed class MatrixInstance : ReportItemInstance, IShowHideContainer, IPageItem
	{
		// Token: 0x0600669B RID: 26267 RVA: 0x00191B20 File Offset: 0x0018FD20
		internal MatrixInstance(ReportProcessing.ProcessingContext pc, Matrix reportItemDef)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new MatrixInstanceInfo(pc, reportItemDef, this);
			pc.Pagination.EnterIgnoreHeight(reportItemDef.StartHidden);
			this.m_columnInstances = new MatrixHeadingInstanceList();
			this.m_rowInstances = new MatrixHeadingInstanceList();
			this.m_cells = new MatrixCellInstancesList();
			this.m_renderingPages = new RenderingPagesRangesList();
			reportItemDef.CurrentPage = reportItemDef.StartPage;
			this.m_startPage = reportItemDef.StartPage;
			if (reportItemDef.FirstCellInstances == null)
			{
				int count = reportItemDef.CellReportItems.Count;
				reportItemDef.FirstCellInstances = new BoolList(count);
				for (int i = 0; i < count; i++)
				{
					reportItemDef.FirstCellInstances.Add(true);
				}
			}
			this.m_inFirstPage = pc.ChunkManager.InFirstPage;
		}

		// Token: 0x0600669C RID: 26268 RVA: 0x00191BFA File Offset: 0x0018FDFA
		internal MatrixInstance()
		{
		}

		// Token: 0x17002442 RID: 9282
		// (get) Token: 0x0600669D RID: 26269 RVA: 0x00191C10 File Offset: 0x0018FE10
		// (set) Token: 0x0600669E RID: 26270 RVA: 0x00191C18 File Offset: 0x0018FE18
		internal ReportItemInstance CornerContent
		{
			get
			{
				return this.m_cornerContent;
			}
			set
			{
				this.m_cornerContent = value;
			}
		}

		// Token: 0x17002443 RID: 9283
		// (get) Token: 0x0600669F RID: 26271 RVA: 0x00191C21 File Offset: 0x0018FE21
		// (set) Token: 0x060066A0 RID: 26272 RVA: 0x00191C29 File Offset: 0x0018FE29
		internal MatrixHeadingInstanceList ColumnInstances
		{
			get
			{
				return this.m_columnInstances;
			}
			set
			{
				this.m_columnInstances = value;
			}
		}

		// Token: 0x17002444 RID: 9284
		// (get) Token: 0x060066A1 RID: 26273 RVA: 0x00191C32 File Offset: 0x0018FE32
		// (set) Token: 0x060066A2 RID: 26274 RVA: 0x00191C3A File Offset: 0x0018FE3A
		internal MatrixHeadingInstanceList RowInstances
		{
			get
			{
				return this.m_rowInstances;
			}
			set
			{
				this.m_rowInstances = value;
			}
		}

		// Token: 0x17002445 RID: 9285
		// (get) Token: 0x060066A3 RID: 26275 RVA: 0x00191C43 File Offset: 0x0018FE43
		// (set) Token: 0x060066A4 RID: 26276 RVA: 0x00191C4B File Offset: 0x0018FE4B
		internal MatrixCellInstancesList Cells
		{
			get
			{
				return this.m_cells;
			}
			set
			{
				this.m_cells = value;
			}
		}

		// Token: 0x17002446 RID: 9286
		// (get) Token: 0x060066A5 RID: 26277 RVA: 0x00191C54 File Offset: 0x0018FE54
		internal int CellColumnCount
		{
			get
			{
				if (0 < this.m_cells.Count)
				{
					return this.m_cells[0].Count;
				}
				return 0;
			}
		}

		// Token: 0x17002447 RID: 9287
		// (get) Token: 0x060066A6 RID: 26278 RVA: 0x00191C77 File Offset: 0x0018FE77
		internal int CellRowCount
		{
			get
			{
				return this.m_cells.Count;
			}
		}

		// Token: 0x17002448 RID: 9288
		// (get) Token: 0x060066A7 RID: 26279 RVA: 0x00191C84 File Offset: 0x0018FE84
		// (set) Token: 0x060066A8 RID: 26280 RVA: 0x00191C8C File Offset: 0x0018FE8C
		internal int InstanceCountOfInnerRowWithPageBreak
		{
			get
			{
				return this.m_instanceCountOfInnerRowWithPageBreak;
			}
			set
			{
				this.m_instanceCountOfInnerRowWithPageBreak = value;
			}
		}

		// Token: 0x17002449 RID: 9289
		// (get) Token: 0x060066A9 RID: 26281 RVA: 0x00191C95 File Offset: 0x0018FE95
		// (set) Token: 0x060066AA RID: 26282 RVA: 0x00191C9D File Offset: 0x0018FE9D
		internal RenderingPagesRangesList ChildrenStartAndEndPages
		{
			get
			{
				return this.m_renderingPages;
			}
			set
			{
				this.m_renderingPages = value;
			}
		}

		// Token: 0x1700244A RID: 9290
		// (get) Token: 0x060066AB RID: 26283 RVA: 0x00191CA6 File Offset: 0x0018FEA6
		internal int CurrentCellOuterIndex
		{
			get
			{
				return this.m_currentCellOuterIndex;
			}
		}

		// Token: 0x1700244B RID: 9291
		// (get) Token: 0x060066AC RID: 26284 RVA: 0x00191CAE File Offset: 0x0018FEAE
		internal int CurrentCellInnerIndex
		{
			get
			{
				return this.m_currentCellInnerIndex;
			}
		}

		// Token: 0x1700244C RID: 9292
		// (set) Token: 0x060066AD RID: 26285 RVA: 0x00191CB6 File Offset: 0x0018FEB6
		internal int CurrentOuterStaticIndex
		{
			set
			{
				this.m_currentOuterStaticIndex = value;
			}
		}

		// Token: 0x1700244D RID: 9293
		// (set) Token: 0x060066AE RID: 26286 RVA: 0x00191CBF File Offset: 0x0018FEBF
		internal int CurrentInnerStaticIndex
		{
			set
			{
				this.m_currentInnerStaticIndex = value;
			}
		}

		// Token: 0x1700244E RID: 9294
		// (get) Token: 0x060066AF RID: 26287 RVA: 0x00191CC8 File Offset: 0x0018FEC8
		// (set) Token: 0x060066B0 RID: 26288 RVA: 0x00191CD0 File Offset: 0x0018FED0
		internal MatrixHeadingInstanceList InnerHeadingInstanceList
		{
			get
			{
				return this.m_innerHeadingInstanceList;
			}
			set
			{
				this.m_innerHeadingInstanceList = value;
			}
		}

		// Token: 0x1700244F RID: 9295
		// (get) Token: 0x060066B1 RID: 26289 RVA: 0x00191CD9 File Offset: 0x0018FED9
		// (set) Token: 0x060066B2 RID: 26290 RVA: 0x00191CE1 File Offset: 0x0018FEE1
		internal bool InFirstPage
		{
			get
			{
				return this.m_inFirstPage;
			}
			set
			{
				this.m_inFirstPage = value;
			}
		}

		// Token: 0x17002450 RID: 9296
		// (get) Token: 0x060066B3 RID: 26291 RVA: 0x00191CEA File Offset: 0x0018FEEA
		// (set) Token: 0x060066B4 RID: 26292 RVA: 0x00191D0F File Offset: 0x0018FF0F
		internal int ExtraPagesFilled
		{
			get
			{
				if (this.m_extraPagesFilled < 1)
				{
					return 0;
				}
				if (this.m_numberOfChildrenOnThisPage > 1)
				{
					return this.m_extraPagesFilled;
				}
				return this.m_extraPagesFilled - 1;
			}
			set
			{
				this.m_extraPagesFilled = value;
			}
		}

		// Token: 0x17002451 RID: 9297
		// (get) Token: 0x060066B5 RID: 26293 RVA: 0x00191D18 File Offset: 0x0018FF18
		// (set) Token: 0x060066B6 RID: 26294 RVA: 0x00191D20 File Offset: 0x0018FF20
		internal int NumberOfChildrenOnThisPage
		{
			get
			{
				return this.m_numberOfChildrenOnThisPage;
			}
			set
			{
				this.m_numberOfChildrenOnThisPage = value;
			}
		}

		// Token: 0x17002452 RID: 9298
		// (get) Token: 0x060066B7 RID: 26295 RVA: 0x00191D29 File Offset: 0x0018FF29
		// (set) Token: 0x060066B8 RID: 26296 RVA: 0x00191D31 File Offset: 0x0018FF31
		int IPageItem.StartPage
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		// Token: 0x17002453 RID: 9299
		// (get) Token: 0x060066B9 RID: 26297 RVA: 0x00191D3A File Offset: 0x0018FF3A
		// (set) Token: 0x060066BA RID: 26298 RVA: 0x00191D42 File Offset: 0x0018FF42
		int IPageItem.EndPage
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		// Token: 0x17002454 RID: 9300
		// (get) Token: 0x060066BB RID: 26299 RVA: 0x00191D4B File Offset: 0x0018FF4B
		internal MatrixInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (MatrixInstanceInfo)this.m_instanceInfo;
			}
		}

		// Token: 0x17002455 RID: 9301
		// (get) Token: 0x060066BC RID: 26300 RVA: 0x00191D77 File Offset: 0x0018FF77
		internal Matrix MatrixDef
		{
			get
			{
				return this.m_reportItemDef as Matrix;
			}
		}

		// Token: 0x060066BD RID: 26301 RVA: 0x00191D84 File Offset: 0x0018FF84
		protected override object SearchChildren(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			Matrix matrix = (Matrix)base.ReportItemDef;
			object obj;
			if (matrix.CornerReportItems.Count > 0)
			{
				if (this.m_cornerContent != null)
				{
					obj = ((ISearchByUniqueName)this.m_cornerContent).Find(targetUniqueName, ref nonCompNames, chunkManager);
					if (obj != null)
					{
						return obj;
					}
				}
				else
				{
					NonComputedUniqueNames cornerNonComputedNames = ((MatrixInstanceInfo)base.GetInstanceInfo(chunkManager, false)).CornerNonComputedNames;
					obj = ((ISearchByUniqueName)matrix.CornerReportItems[0]).Find(targetUniqueName, ref cornerNonComputedNames, chunkManager);
					if (obj != null)
					{
						nonCompNames = cornerNonComputedNames;
						return obj;
					}
				}
			}
			obj = ((ISearchByUniqueName)this.m_columnInstances).Find(targetUniqueName, ref nonCompNames, chunkManager);
			if (obj != null)
			{
				return obj;
			}
			obj = ((ISearchByUniqueName)this.m_rowInstances).Find(targetUniqueName, ref nonCompNames, chunkManager);
			if (obj != null)
			{
				return obj;
			}
			int count = this.m_cells.Count;
			for (int i = 0; i < count; i++)
			{
				MatrixCellInstanceList matrixCellInstanceList = this.m_cells[i];
				int count2 = matrixCellInstanceList.Count;
				for (int j = 0; j < count2; j++)
				{
					MatrixCellInstance matrixCellInstance = matrixCellInstanceList[j];
					MatrixCellInstanceInfo instanceInfo = matrixCellInstance.GetInstanceInfo(chunkManager);
					int num = instanceInfo.RowIndex * matrix.MatrixColumns.Count + instanceInfo.ColumnIndex;
					if (matrix.CellReportItems.IsReportItemComputed(num))
					{
						if (matrixCellInstance.Content != null)
						{
							obj = ((ISearchByUniqueName)matrixCellInstance.Content).Find(targetUniqueName, ref nonCompNames, chunkManager);
							if (obj != null)
							{
								return obj;
							}
						}
					}
					else
					{
						NonComputedUniqueNames contentUniqueNames = instanceInfo.ContentUniqueNames;
						obj = ((ISearchByUniqueName)matrix.CellReportItems[num]).Find(targetUniqueName, ref contentUniqueNames, chunkManager);
						if (obj != null)
						{
							nonCompNames = contentUniqueNames;
							return obj;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060066BE RID: 26302 RVA: 0x00191EFC File Offset: 0x001900FC
		void IShowHideContainer.BeginProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.BeginProcessContainer(this.m_uniqueName, this.m_reportItemDef.Visibility);
		}

		// Token: 0x060066BF RID: 26303 RVA: 0x00191F15 File Offset: 0x00190115
		void IShowHideContainer.EndProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.EndProcessContainer(this.m_uniqueName, this.m_reportItemDef.Visibility);
		}

		// Token: 0x060066C0 RID: 26304 RVA: 0x00191F30 File Offset: 0x00190130
		internal ReportItem GetCellReportItemDef(int cellRIIndex, out bool computed)
		{
			if (-1 == cellRIIndex)
			{
				cellRIIndex = this.GetCurrentCellRIIndex();
			}
			computed = false;
			int num;
			ReportItem reportItem;
			((Matrix)this.m_reportItemDef).CellReportItems.GetReportItem(cellRIIndex, out computed, out num, out reportItem);
			return reportItem;
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x00191F68 File Offset: 0x00190168
		internal MatrixCellInstance AddCell(ReportProcessing.ProcessingContext pc, out NonComputedUniqueNames cellNonComputedUniqueNames)
		{
			Matrix matrix = (Matrix)this.m_reportItemDef;
			int currentCellRIIndex = this.GetCurrentCellRIIndex();
			bool flag = matrix.ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Column;
			int num;
			int num2;
			if (flag)
			{
				num = this.m_currentOuterStaticIndex;
				num2 = this.m_currentInnerStaticIndex;
			}
			else
			{
				num2 = this.m_currentOuterStaticIndex;
				num = this.m_currentInnerStaticIndex;
			}
			MatrixCellInstance matrixCellInstance;
			if (pc.HeadingInstance != null && pc.HeadingInstance.MatrixHeadingDef.Subtotal.StyleClass != null)
			{
				matrixCellInstance = new MatrixSubtotalCellInstance(num, num2, matrix, currentCellRIIndex, pc, out cellNonComputedUniqueNames);
			}
			else
			{
				matrixCellInstance = new MatrixCellInstance(num, num2, matrix, currentCellRIIndex, pc, out cellNonComputedUniqueNames);
			}
			if ((!flag && this.m_currentCellOuterIndex == 0) || (flag && this.m_currentCellInnerIndex == 0))
			{
				if (!pc.Pagination.IgnoreHeight)
				{
					pc.Pagination.AddToCurrentPageHeight(matrix, matrix.MatrixRows[num].HeightValue);
				}
				if (!pc.Pagination.IgnorePageBreak && pc.Pagination.CurrentPageHeight >= pc.Pagination.PageHeight && this.m_rowInstances.Count > 1)
				{
					pc.Pagination.SetCurrentPageHeight(matrix, 0.0);
					this.m_extraPagesFilled++;
					matrix.CurrentPage = this.m_startPage + this.m_extraPagesFilled;
					this.m_numberOfChildrenOnThisPage = 0;
				}
				else
				{
					this.m_numberOfChildrenOnThisPage++;
				}
			}
			if (matrix.ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Column)
			{
				this.m_cells[this.m_currentCellOuterIndex].Add(matrixCellInstance);
			}
			else
			{
				if (this.m_currentCellOuterIndex == 0)
				{
					Global.Tracer.Assert(this.m_cells.Count == this.m_currentCellInnerIndex);
					MatrixCellInstanceList matrixCellInstanceList = new MatrixCellInstanceList();
					this.m_cells.Add(matrixCellInstanceList);
				}
				this.m_cells[this.m_currentCellInnerIndex].Add(matrixCellInstance);
			}
			this.m_currentCellInnerIndex++;
			return matrixCellInstance;
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x00192144 File Offset: 0x00190344
		internal void NewOuterCells()
		{
			if (0 < this.m_currentCellInnerIndex || this.m_cells.Count == 0)
			{
				if (((Matrix)this.m_reportItemDef).ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Column)
				{
					MatrixCellInstanceList matrixCellInstanceList = new MatrixCellInstanceList();
					this.m_cells.Add(matrixCellInstanceList);
				}
				if (0 < this.m_currentCellInnerIndex)
				{
					this.m_currentCellOuterIndex++;
					this.m_currentCellInnerIndex = 0;
				}
			}
		}

		// Token: 0x060066C3 RID: 26307 RVA: 0x001921AC File Offset: 0x001903AC
		internal int GetCurrentCellRIIndex()
		{
			Matrix matrix = (Matrix)this.m_reportItemDef;
			int count = matrix.MatrixColumns.Count;
			int num;
			if (matrix.ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Column)
			{
				num = this.m_currentOuterStaticIndex * count + this.m_currentInnerStaticIndex;
			}
			else
			{
				num = this.m_currentInnerStaticIndex * count + this.m_currentOuterStaticIndex;
			}
			return num;
		}

		// Token: 0x060066C4 RID: 26308 RVA: 0x001921FC File Offset: 0x001903FC
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstance, new MemberInfoList
			{
				new MemberInfo(MemberName.CornerContent, ObjectType.ReportItemInstance),
				new MemberInfo(MemberName.ColumnInstances, ObjectType.MatrixHeadingInstanceList),
				new MemberInfo(MemberName.RowInstances, ObjectType.MatrixHeadingInstanceList),
				new MemberInfo(MemberName.Cells, ObjectType.MatrixCellInstancesList),
				new MemberInfo(MemberName.InstanceCountOfInnerRowWithPageBreak, Token.Int32),
				new MemberInfo(MemberName.ChildrenStartAndEndPages, ObjectType.RenderingPagesRangesList)
			});
		}

		// Token: 0x060066C5 RID: 26309 RVA: 0x0019228F File Offset: 0x0019048F
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadMatrixInstanceInfo((Matrix)this.m_reportItemDef);
		}

		// Token: 0x04003308 RID: 13064
		private ReportItemInstance m_cornerContent;

		// Token: 0x04003309 RID: 13065
		private MatrixHeadingInstanceList m_columnInstances;

		// Token: 0x0400330A RID: 13066
		private MatrixHeadingInstanceList m_rowInstances;

		// Token: 0x0400330B RID: 13067
		private MatrixCellInstancesList m_cells;

		// Token: 0x0400330C RID: 13068
		private int m_instanceCountOfInnerRowWithPageBreak;

		// Token: 0x0400330D RID: 13069
		private RenderingPagesRangesList m_renderingPages;

		// Token: 0x0400330E RID: 13070
		[NonSerialized]
		private int m_currentCellOuterIndex;

		// Token: 0x0400330F RID: 13071
		[NonSerialized]
		private int m_currentCellInnerIndex;

		// Token: 0x04003310 RID: 13072
		[NonSerialized]
		private int m_currentOuterStaticIndex;

		// Token: 0x04003311 RID: 13073
		[NonSerialized]
		private int m_currentInnerStaticIndex;

		// Token: 0x04003312 RID: 13074
		[NonSerialized]
		private MatrixHeadingInstanceList m_innerHeadingInstanceList;

		// Token: 0x04003313 RID: 13075
		[NonSerialized]
		private bool m_inFirstPage;

		// Token: 0x04003314 RID: 13076
		[NonSerialized]
		private int m_extraPagesFilled;

		// Token: 0x04003315 RID: 13077
		[NonSerialized]
		private int m_numberOfChildrenOnThisPage;

		// Token: 0x04003316 RID: 13078
		[NonSerialized]
		private int m_startPage = -1;

		// Token: 0x04003317 RID: 13079
		[NonSerialized]
		private int m_endPage = -1;
	}
}
