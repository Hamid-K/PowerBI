using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000063 RID: 99
	public sealed class CustomReportItem : ReportItem
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x00019A89 File Offset: 0x00017C89
		internal CustomReportItem(CustomReportItem criDef, CustomReportItemInstance criInstance, CustomReportItemInstanceInfo instanceInfo)
			: base(criDef, criInstance, instanceInfo)
		{
			this.m_isProcessing = true;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00019A9B File Offset: 0x00017C9B
		internal CustomReportItem(string uniqueName, int intUniqueName, ReportItem reportItemDef, ReportItemInstance reportItemInstance, RenderingContext renderingContext, NonComputedUniqueNames[] childrenNonComputedUniqueNames)
			: base(uniqueName, intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
			this.m_isProcessing = false;
			this.m_childrenNonComputedUniqueNames = childrenNonComputedUniqueNames;
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00019AB9 File Offset: 0x00017CB9
		public string Type
		{
			get
			{
				return ((CustomReportItem)base.ReportItemDef).Type;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00019ACC File Offset: 0x00017CCC
		public ReportItem AltReportItem
		{
			get
			{
				if (this.m_isProcessing)
				{
					return null;
				}
				ReportItem reportItem = this.m_altReportItem;
				if (this.m_altReportItem == null)
				{
					CustomReportItem customReportItem = (CustomReportItem)base.ReportItemDef;
					ReportItem reportItem2 = null;
					Global.Tracer.Assert(customReportItem.RenderReportItem != null || customReportItem.AltReportItem != null);
					if (customReportItem.RenderReportItem != null && 1 == customReportItem.RenderReportItem.Count)
					{
						reportItem2 = customReportItem.RenderReportItem[0];
					}
					else if (customReportItem.AltReportItem != null && 1 == customReportItem.AltReportItem.Count)
					{
						Global.Tracer.Assert(customReportItem.RenderReportItem == null);
						reportItem2 = customReportItem.AltReportItem[0];
					}
					if (reportItem2 != null)
					{
						ReportItemInstance reportItemInstance = null;
						NonComputedUniqueNames[] array = this.m_childrenNonComputedUniqueNames;
						if (base.ReportItemInstance != null)
						{
							CustomReportItemInstance customReportItemInstance = (CustomReportItemInstance)base.ReportItemInstance;
							Global.Tracer.Assert(customReportItemInstance != null);
							if (customReportItemInstance.AltReportItemColInstance != null)
							{
								if (customReportItemInstance.AltReportItemColInstance.ReportItemInstances != null && 0 < customReportItemInstance.AltReportItemColInstance.ReportItemInstances.Count)
								{
									reportItemInstance = customReportItemInstance.AltReportItemColInstance[0];
								}
								else
								{
									if (customReportItemInstance.AltReportItemColInstance.ChildrenNonComputedUniqueNames != null)
									{
										array = customReportItemInstance.AltReportItemColInstance.ChildrenNonComputedUniqueNames;
									}
									if (array == null)
									{
										array = customReportItemInstance.AltReportItemColInstance.GetInstanceInfo(this.RenderingContext.ChunkManager, this.RenderingContext.InPageSection).ChildrenNonComputedUniqueNames;
									}
								}
							}
						}
						reportItem = ReportItem.CreateItem(0, reportItem2, reportItemInstance, this.RenderingContext, (array == null) ? null : array[0]);
						if (base.UseCache)
						{
							this.m_altReportItem = reportItem;
						}
					}
				}
				return reportItem;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00019C64 File Offset: 0x00017E64
		public CustomData CustomData
		{
			get
			{
				CustomData customData = this.m_customData;
				if (this.m_customData == null)
				{
					customData = new CustomData(this);
					if (base.UseCache)
					{
						this.m_customData = customData;
					}
				}
				return customData;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00019C97 File Offset: 0x00017E97
		public override bool Hidden
		{
			get
			{
				if (!this.m_isProcessing)
				{
					return base.Hidden;
				}
				return base.ReportItemDef.Visibility != null && base.InstanceInfo.StartHidden;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00019CC2 File Offset: 0x00017EC2
		internal new TextBox ToggleParent
		{
			get
			{
				if (!this.m_isProcessing)
				{
					return base.ToggleParent;
				}
				return null;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00019CD4 File Offset: 0x00017ED4
		public new bool IsToggleChild
		{
			get
			{
				return !this.m_isProcessing && base.IsToggleChild;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00019CE6 File Offset: 0x00017EE6
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x00019CF8 File Offset: 0x00017EF8
		public override object SharedRenderingInfo
		{
			get
			{
				if (!this.m_isProcessing)
				{
					return base.SharedRenderingInfo;
				}
				return null;
			}
			set
			{
				if (!this.m_isProcessing)
				{
					base.SharedRenderingInfo = value;
					return;
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00019D0F File Offset: 0x00017F0F
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00019D21 File Offset: 0x00017F21
		public new object RenderingInfo
		{
			get
			{
				if (!this.m_isProcessing)
				{
					return base.RenderingInfo;
				}
				return null;
			}
			set
			{
				if (!this.m_isProcessing)
				{
					base.RenderingInfo = value;
					return;
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00019D38 File Offset: 0x00017F38
		internal CustomReportItem CriDefinition
		{
			get
			{
				return base.ReportItemDef as CustomReportItem;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00019D45 File Offset: 0x00017F45
		internal CustomReportItemInstance CriInstance
		{
			get
			{
				return base.ReportItemInstance as CustomReportItemInstance;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00019D52 File Offset: 0x00017F52
		internal new RenderingContext RenderingContext
		{
			get
			{
				if (this.m_isProcessing)
				{
					return null;
				}
				return base.RenderingContext;
			}
		}

		// Token: 0x040001C6 RID: 454
		private ReportItem m_altReportItem;

		// Token: 0x040001C7 RID: 455
		private CustomData m_customData;

		// Token: 0x040001C8 RID: 456
		private bool m_isProcessing;

		// Token: 0x040001C9 RID: 457
		private NonComputedUniqueNames[] m_childrenNonComputedUniqueNames;
	}
}
