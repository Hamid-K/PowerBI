using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200002C RID: 44
	public sealed class Rectangle : ReportItem
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x0000DD96 File Offset: 0x0000BF96
		internal Rectangle(string uniqueName, int intUniqueName, ReportItem reportItemDef, ReportItemInstance reportItemInstance, RenderingContext renderingContext, NonComputedUniqueNames[] childrenNonComputedUniqueNames)
			: base(uniqueName, intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
			this.m_childrenNonComputedUniqueNames = childrenNonComputedUniqueNames;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0000DE10 File Offset: 0x0000C010
		public override object SharedRenderingInfo
		{
			get
			{
				Hashtable sharedRenderingInfo = base.RenderingContext.RenderingInfoManager.SharedRenderingInfo;
				if (base.ReportItemDef is Report)
				{
					return sharedRenderingInfo[((Report)base.ReportItemDef).BodyID];
				}
				return sharedRenderingInfo[base.ReportItemDef.ID];
			}
			set
			{
				Hashtable sharedRenderingInfo = base.RenderingContext.RenderingInfoManager.SharedRenderingInfo;
				if (base.ReportItemDef is Report)
				{
					sharedRenderingInfo[((Report)base.ReportItemDef).BodyID] = value;
					return;
				}
				sharedRenderingInfo[base.ReportItemDef.ID] = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000DE70 File Offset: 0x0000C070
		public ReportItemCollection ReportItemCollection
		{
			get
			{
				ReportItemCollection reportItemCollection = this.m_reportItemCollection;
				if (this.m_reportItemCollection == null)
				{
					RenderingContext renderingContext;
					if (base.RenderingContext.InPageSection)
					{
						renderingContext = new RenderingContext(base.RenderingContext, base.UniqueName);
					}
					else
					{
						renderingContext = base.RenderingContext;
					}
					ReportItemCollection reportItemCollection2;
					if (base.ReportItemDef is Report)
					{
						reportItemCollection2 = ((Report)base.ReportItemDef).ReportItems;
					}
					else
					{
						Global.Tracer.Assert(base.ReportItemDef is Rectangle);
						reportItemCollection2 = ((Rectangle)base.ReportItemDef).ReportItems;
					}
					ReportItemColInstance reportItemColInstance = null;
					if (base.ReportItemInstance != null)
					{
						if (base.ReportItemDef is Report)
						{
							reportItemColInstance = ((ReportInstance)base.ReportItemInstance).ReportItemColInstance;
						}
						else
						{
							Global.Tracer.Assert(base.ReportItemDef is Rectangle);
							reportItemColInstance = ((RectangleInstance)base.ReportItemInstance).ReportItemColInstance;
						}
					}
					reportItemCollection = new ReportItemCollection(reportItemCollection2, reportItemColInstance, renderingContext, this.m_childrenNonComputedUniqueNames);
					if (base.RenderingContext.CacheState)
					{
						this.m_reportItemCollection = reportItemCollection;
					}
				}
				return reportItemCollection;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000DF77 File Offset: 0x0000C177
		public bool PageBreakAtEnd
		{
			get
			{
				return base.ReportItemDef is Rectangle && ((Rectangle)base.ReportItemDef).PageBreakAtEnd;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000DF98 File Offset: 0x0000C198
		public bool PageBreakAtStart
		{
			get
			{
				return base.ReportItemDef is Rectangle && ((Rectangle)base.ReportItemDef).PageBreakAtStart;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000DFB9 File Offset: 0x0000C1B9
		public override int LinkToChild
		{
			get
			{
				if (base.ReportItemDef is Rectangle)
				{
					return ((Rectangle)base.ReportItemDef).LinkToChild;
				}
				return -1;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		internal override bool Search(SearchContext searchContext)
		{
			if (base.SkipSearch)
			{
				return false;
			}
			ReportItemCollection reportItemCollection = this.ReportItemCollection;
			return reportItemCollection != null && reportItemCollection.Search(searchContext);
		}

		// Token: 0x040000E3 RID: 227
		private NonComputedUniqueNames[] m_childrenNonComputedUniqueNames;

		// Token: 0x040000E4 RID: 228
		private ReportItemCollection m_reportItemCollection;
	}
}
