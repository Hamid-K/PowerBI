using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200073A RID: 1850
	[Serializable]
	internal sealed class MatrixHeadingInstance : InstanceInfoOwner, IShowHideContainer
	{
		// Token: 0x060066CD RID: 26317 RVA: 0x001923E4 File Offset: 0x001905E4
		internal MatrixHeadingInstance(ReportProcessing.ProcessingContext pc, int headingCellIndex, MatrixHeading matrixHeadingDef, bool isSubtotal, int reportItemDefIndex, VariantList groupExpressionValues, out NonComputedUniqueNames nonComputedUniqueNames)
		{
			this.m_uniqueName = pc.CreateUniqueName();
			if (isSubtotal && matrixHeadingDef.Subtotal.StyleClass != null)
			{
				this.m_instanceInfo = new MatrixSubtotalHeadingInstanceInfo(pc, headingCellIndex, matrixHeadingDef, this, isSubtotal, reportItemDefIndex, groupExpressionValues, out nonComputedUniqueNames);
				if (matrixHeadingDef.GetInnerStaticHeading() != null)
				{
					this.m_subHeadingInstances = new MatrixHeadingInstanceList();
				}
			}
			else
			{
				this.m_instanceInfo = new MatrixHeadingInstanceInfo(pc, headingCellIndex, matrixHeadingDef, this, isSubtotal, reportItemDefIndex, groupExpressionValues, out nonComputedUniqueNames);
				if (matrixHeadingDef.SubHeading != null)
				{
					this.m_subHeadingInstances = new MatrixHeadingInstanceList();
				}
			}
			this.m_renderingPages = new RenderingPagesRangesList();
			this.m_matrixHeadingDef = matrixHeadingDef;
			this.m_isSubtotal = isSubtotal;
			this.m_headingDefIndex = reportItemDefIndex;
			if (!matrixHeadingDef.IsColumn)
			{
				pc.Pagination.EnterIgnoreHeight(matrixHeadingDef.StartHidden);
			}
			if (matrixHeadingDef.FirstHeadingInstances == null)
			{
				int count = matrixHeadingDef.ReportItems.Count;
				matrixHeadingDef.FirstHeadingInstances = new BoolList(count);
				for (int i = 0; i < count; i++)
				{
					matrixHeadingDef.FirstHeadingInstances.Add(true);
				}
			}
		}

		// Token: 0x060066CE RID: 26318 RVA: 0x001924E3 File Offset: 0x001906E3
		internal MatrixHeadingInstance()
		{
		}

		// Token: 0x17002458 RID: 9304
		// (get) Token: 0x060066CF RID: 26319 RVA: 0x001924EB File Offset: 0x001906EB
		// (set) Token: 0x060066D0 RID: 26320 RVA: 0x001924F3 File Offset: 0x001906F3
		internal int UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
			set
			{
				this.m_uniqueName = value;
			}
		}

		// Token: 0x17002459 RID: 9305
		// (get) Token: 0x060066D1 RID: 26321 RVA: 0x001924FC File Offset: 0x001906FC
		// (set) Token: 0x060066D2 RID: 26322 RVA: 0x00192504 File Offset: 0x00190704
		internal MatrixHeading MatrixHeadingDef
		{
			get
			{
				return this.m_matrixHeadingDef;
			}
			set
			{
				this.m_matrixHeadingDef = value;
			}
		}

		// Token: 0x1700245A RID: 9306
		// (get) Token: 0x060066D3 RID: 26323 RVA: 0x0019250D File Offset: 0x0019070D
		// (set) Token: 0x060066D4 RID: 26324 RVA: 0x00192515 File Offset: 0x00190715
		internal ReportItemInstance Content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x1700245B RID: 9307
		// (get) Token: 0x060066D5 RID: 26325 RVA: 0x0019251E File Offset: 0x0019071E
		// (set) Token: 0x060066D6 RID: 26326 RVA: 0x00192526 File Offset: 0x00190726
		internal MatrixHeadingInstanceList SubHeadingInstances
		{
			get
			{
				return this.m_subHeadingInstances;
			}
			set
			{
				this.m_subHeadingInstances = value;
			}
		}

		// Token: 0x1700245C RID: 9308
		// (get) Token: 0x060066D7 RID: 26327 RVA: 0x0019252F File Offset: 0x0019072F
		// (set) Token: 0x060066D8 RID: 26328 RVA: 0x00192537 File Offset: 0x00190737
		internal bool IsSubtotal
		{
			get
			{
				return this.m_isSubtotal;
			}
			set
			{
				this.m_isSubtotal = value;
			}
		}

		// Token: 0x1700245D RID: 9309
		// (get) Token: 0x060066D9 RID: 26329 RVA: 0x00192540 File Offset: 0x00190740
		internal MatrixHeadingInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				return (MatrixHeadingInstanceInfo)this.m_instanceInfo;
			}
		}

		// Token: 0x1700245E RID: 9310
		// (get) Token: 0x060066DA RID: 26330 RVA: 0x0019256C File Offset: 0x0019076C
		// (set) Token: 0x060066DB RID: 26331 RVA: 0x00192574 File Offset: 0x00190774
		internal int HeadingIndex
		{
			get
			{
				return this.m_headingDefIndex;
			}
			set
			{
				this.m_headingDefIndex = value;
			}
		}

		// Token: 0x1700245F RID: 9311
		// (get) Token: 0x060066DC RID: 26332 RVA: 0x0019257D File Offset: 0x0019077D
		// (set) Token: 0x060066DD RID: 26333 RVA: 0x00192585 File Offset: 0x00190785
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

		// Token: 0x060066DE RID: 26334 RVA: 0x0019258E File Offset: 0x0019078E
		void IShowHideContainer.BeginProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.BeginProcessContainer(this.m_uniqueName, this.m_matrixHeadingDef.Visibility);
		}

		// Token: 0x060066DF RID: 26335 RVA: 0x001925A7 File Offset: 0x001907A7
		void IShowHideContainer.EndProcessContainer(ReportProcessing.ProcessingContext context)
		{
			context.EndProcessContainer(this.m_uniqueName, this.m_matrixHeadingDef.Visibility);
		}

		// Token: 0x060066E0 RID: 26336 RVA: 0x001925C0 File Offset: 0x001907C0
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfoOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.UniqueName, Token.Int32),
				new MemberInfo(MemberName.Content, ObjectType.ReportItemInstance),
				new MemberInfo(MemberName.SubHeadingInstances, ObjectType.MatrixHeadingInstanceList),
				new MemberInfo(MemberName.IsSubtotal, Token.Boolean),
				new MemberInfo(MemberName.ChildrenStartAndEndPages, ObjectType.RenderingPagesRangesList)
			});
		}

		// Token: 0x060066E1 RID: 26337 RVA: 0x00192644 File Offset: 0x00190844
		internal object Find(int index, int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			ReportItemCollection reportItemCollection;
			if (this.IsSubtotal)
			{
				reportItemCollection = this.MatrixHeadingDef.Subtotal.ReportItems;
			}
			else
			{
				reportItemCollection = this.MatrixHeadingDef.ReportItems;
			}
			if (reportItemCollection.Count > 0)
			{
				if (reportItemCollection.Count == 1)
				{
					index = 0;
				}
				if (reportItemCollection.IsReportItemComputed(index))
				{
					Global.Tracer.Assert(this.m_content != null, "The instance of a computed report item cannot be null.");
					object obj = ((ISearchByUniqueName)this.m_content).Find(targetUniqueName, ref nonCompNames, chunkManager);
					if (obj != null)
					{
						return obj;
					}
				}
				else
				{
					NonComputedUniqueNames contentUniqueNames = this.GetInstanceInfo(chunkManager).ContentUniqueNames;
					object obj = ((ISearchByUniqueName)reportItemCollection[index]).Find(targetUniqueName, ref contentUniqueNames, chunkManager);
					if (obj != null)
					{
						nonCompNames = contentUniqueNames;
						return obj;
					}
				}
			}
			if (this.m_subHeadingInstances != null)
			{
				return ((ISearchByUniqueName)this.m_subHeadingInstances).Find(targetUniqueName, ref nonCompNames, chunkManager);
			}
			return null;
		}

		// Token: 0x060066E2 RID: 26338 RVA: 0x00192706 File Offset: 0x00190906
		internal MatrixHeadingInstanceInfo GetInstanceInfo(ChunkManager.RenderingChunkManager chunkManager)
		{
			if (this.m_instanceInfo is OffsetInfo)
			{
				return chunkManager.GetReader(((OffsetInfo)this.m_instanceInfo).Offset).ReadMatrixHeadingInstanceInfoBase();
			}
			return (MatrixHeadingInstanceInfo)this.m_instanceInfo;
		}

		// Token: 0x0400331A RID: 13082
		private int m_uniqueName;

		// Token: 0x0400331B RID: 13083
		private ReportItemInstance m_content;

		// Token: 0x0400331C RID: 13084
		private MatrixHeadingInstanceList m_subHeadingInstances;

		// Token: 0x0400331D RID: 13085
		private bool m_isSubtotal;

		// Token: 0x0400331E RID: 13086
		private RenderingPagesRangesList m_renderingPages;

		// Token: 0x0400331F RID: 13087
		[Reference]
		[NonSerialized]
		private MatrixHeading m_matrixHeadingDef;

		// Token: 0x04003320 RID: 13088
		[NonSerialized]
		private int m_headingDefIndex;
	}
}
