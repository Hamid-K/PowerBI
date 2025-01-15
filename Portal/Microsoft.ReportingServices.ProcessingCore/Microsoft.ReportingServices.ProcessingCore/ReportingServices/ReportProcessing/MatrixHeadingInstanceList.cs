using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006B3 RID: 1715
	[Serializable]
	internal sealed class MatrixHeadingInstanceList : ArrayList, ISearchByUniqueName
	{
		// Token: 0x06005CB7 RID: 23735 RVA: 0x00179FBC File Offset: 0x001781BC
		internal MatrixHeadingInstanceList()
		{
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x00179FC4 File Offset: 0x001781C4
		internal MatrixHeadingInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700208E RID: 8334
		internal MatrixHeadingInstance this[int index]
		{
			get
			{
				return (MatrixHeadingInstance)base[index];
			}
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x00179FDC File Offset: 0x001781DC
		internal void Add(MatrixHeadingInstance matrixHeadingInstance, ReportProcessing.ProcessingContext pc)
		{
			if (this.m_lastHeadingInstance != null)
			{
				this.m_lastHeadingInstance.InstanceInfo.HeadingSpan = matrixHeadingInstance.InstanceInfo.HeadingCellIndex - this.m_lastHeadingInstance.InstanceInfo.HeadingCellIndex;
				bool flag = true;
				MatrixHeading matrixHeadingDef = this.m_lastHeadingInstance.MatrixHeadingDef;
				if (pc.ReportItemsReferenced)
				{
					Matrix matrix = (Matrix)matrixHeadingDef.DataRegionDef;
					if (matrixHeadingDef.IsColumn)
					{
						if (matrix.ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Column)
						{
							flag = false;
						}
					}
					else if (matrix.ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Row)
					{
						flag = false;
					}
				}
				if (flag)
				{
					bool flag2;
					if (this.m_lastHeadingInstance.IsSubtotal)
					{
						flag2 = this.m_lastHeadingInstance.MatrixHeadingDef.Subtotal.FirstInstance;
						this.m_lastHeadingInstance.MatrixHeadingDef.Subtotal.FirstInstance = false;
					}
					else
					{
						BoolList firstHeadingInstances = this.m_lastHeadingInstance.MatrixHeadingDef.FirstHeadingInstances;
						flag2 = firstHeadingInstances[this.m_lastHeadingInstance.HeadingIndex];
						firstHeadingInstances[this.m_lastHeadingInstance.HeadingIndex] = false;
					}
					pc.ChunkManager.AddInstance(this.m_lastHeadingInstance.InstanceInfo, this.m_lastHeadingInstance, flag2 || matrixHeadingDef.InFirstPage, pc.InPageSection);
				}
			}
			base.Add(matrixHeadingInstance);
			this.m_lastHeadingInstance = matrixHeadingInstance;
			matrixHeadingInstance.MatrixHeadingDef.InFirstPage = pc.ChunkManager.InFirstPage;
		}

		// Token: 0x06005CBB RID: 23739 RVA: 0x0017A128 File Offset: 0x00178328
		internal void SetLastHeadingSpan(int currentCellIndex, ReportProcessing.ProcessingContext pc)
		{
			if (this.m_lastHeadingInstance != null)
			{
				this.m_lastHeadingInstance.InstanceInfo.HeadingSpan = currentCellIndex - this.m_lastHeadingInstance.InstanceInfo.HeadingCellIndex;
				bool flag = true;
				MatrixHeading matrixHeadingDef = this.m_lastHeadingInstance.MatrixHeadingDef;
				if (pc.ReportItemsReferenced)
				{
					Matrix matrix = (Matrix)matrixHeadingDef.DataRegionDef;
					if (matrixHeadingDef.IsColumn)
					{
						if (matrix.ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Column)
						{
							flag = false;
						}
					}
					else if (matrix.ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Row)
					{
						flag = false;
					}
				}
				if (flag)
				{
					bool flag2;
					if (this.m_lastHeadingInstance.IsSubtotal)
					{
						flag2 = this.m_lastHeadingInstance.MatrixHeadingDef.Subtotal.FirstInstance;
						this.m_lastHeadingInstance.MatrixHeadingDef.Subtotal.FirstInstance = false;
					}
					else
					{
						BoolList firstHeadingInstances = this.m_lastHeadingInstance.MatrixHeadingDef.FirstHeadingInstances;
						flag2 = firstHeadingInstances[this.m_lastHeadingInstance.HeadingIndex];
						firstHeadingInstances[this.m_lastHeadingInstance.HeadingIndex] = false;
					}
					pc.ChunkManager.AddInstance(this.m_lastHeadingInstance.InstanceInfo, this.m_lastHeadingInstance, flag2 || matrixHeadingDef.InFirstPage, pc.InPageSection);
				}
			}
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x0017A248 File Offset: 0x00178448
		object ISearchByUniqueName.Find(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			int count = this.Count;
			object obj = null;
			for (int i = 0; i < count; i++)
			{
				obj = this[i].Find(i, targetUniqueName, ref nonCompNames, chunkManager);
				if (obj != null)
				{
					break;
				}
			}
			return obj;
		}

		// Token: 0x04002F98 RID: 12184
		[NonSerialized]
		private MatrixHeadingInstance m_lastHeadingInstance;
	}
}
