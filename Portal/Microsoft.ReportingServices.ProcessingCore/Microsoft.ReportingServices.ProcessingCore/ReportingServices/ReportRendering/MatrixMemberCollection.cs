using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200003B RID: 59
	internal sealed class MatrixMemberCollection
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x00010450 File Offset: 0x0000E650
		internal MatrixMemberCollection(Matrix owner, MatrixMember parent, MatrixHeading headingDef, MatrixHeadingInstanceList headingInstances, List<int> memberMapping, bool isParentSubTotal)
		{
			this.m_owner = owner;
			this.m_parent = parent;
			this.m_headingInstances = headingInstances;
			this.m_headingDef = headingDef;
			this.m_memberMapping = memberMapping;
			this.m_isParentSubTotal = isParentSubTotal;
			if (owner.NoRows)
			{
				Subtotal subtotal = this.m_headingDef.Subtotal;
				if (subtotal != null)
				{
					if (subtotal.Position == Subtotal.PositionType.After)
					{
						this.m_subTotalPosition = 1;
						return;
					}
					this.m_subTotalPosition = 0;
				}
			}
		}

		// Token: 0x1700040D RID: 1037
		public MatrixMember this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				MatrixMember matrixMember = null;
				if (index == 0)
				{
					matrixMember = this.m_firstMember;
				}
				else if (this.m_members != null)
				{
					matrixMember = this.m_members[index - 1];
				}
				if (matrixMember == null)
				{
					bool flag = false;
					MatrixHeadingInstance matrixHeadingInstance = null;
					if (this.m_memberMapping != null && index < this.m_memberMapping.Count)
					{
						matrixHeadingInstance = this.m_headingInstances[this.m_memberMapping[index]];
						flag = matrixHeadingInstance.IsSubtotal;
					}
					else if (this.m_subTotalPosition >= 0 && index == this.m_subTotalPosition)
					{
						flag = true;
					}
					matrixMember = new MatrixMember(this.m_owner, this.m_parent, this.m_headingDef, matrixHeadingInstance, flag, this.m_isParentSubTotal, index);
					if (this.m_owner.RenderingContext.CacheState)
					{
						if (index == 0)
						{
							this.m_firstMember = matrixMember;
						}
						else
						{
							if (this.m_members == null)
							{
								this.m_members = new MatrixMember[this.Count - 1];
							}
							this.m_members[index - 1] = matrixMember;
						}
					}
				}
				return matrixMember;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x000105F0 File Offset: 0x0000E7F0
		public int Count
		{
			get
			{
				if (!this.m_owner.NoRows)
				{
					return this.m_memberMapping.Count;
				}
				if (this.m_headingDef.Subtotal != null)
				{
					return 2;
				}
				if (this.m_headingDef.Grouping == null)
				{
					return this.m_headingDef.ReportItems.Count;
				}
				return 1;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00010644 File Offset: 0x0000E844
		internal MatrixHeading MatrixHeadingDef
		{
			get
			{
				return this.m_headingDef;
			}
		}

		// Token: 0x04000114 RID: 276
		private Matrix m_owner;

		// Token: 0x04000115 RID: 277
		private MatrixHeading m_headingDef;

		// Token: 0x04000116 RID: 278
		private MatrixHeadingInstanceList m_headingInstances;

		// Token: 0x04000117 RID: 279
		private MatrixMember[] m_members;

		// Token: 0x04000118 RID: 280
		private MatrixMember m_firstMember;

		// Token: 0x04000119 RID: 281
		private MatrixMember m_parent;

		// Token: 0x0400011A RID: 282
		private int m_subTotalPosition = -1;

		// Token: 0x0400011B RID: 283
		private bool m_isParentSubTotal;

		// Token: 0x0400011C RID: 284
		private List<int> m_memberMapping;
	}
}
