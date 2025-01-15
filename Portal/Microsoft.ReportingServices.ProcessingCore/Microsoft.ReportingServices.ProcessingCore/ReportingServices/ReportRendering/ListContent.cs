using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000033 RID: 51
	internal sealed class ListContent : Group, IDocumentMapEntry
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x0000EA24 File Offset: 0x0000CC24
		internal ListContent(List owner, int instanceIndex)
			: base(owner, ((List)owner.ReportItemDef).Grouping, owner.ReportItemDef.Visibility)
		{
			if (owner.ReportItemInstance != null)
			{
				ListContentInstanceList listContents = ((ListInstance)owner.ReportItemInstance).ListContents;
				if (listContents != null)
				{
					if (instanceIndex < listContents.Count)
					{
						this.m_listContentInstance = listContents[instanceIndex];
						if (this.m_listContentInstance != null)
						{
							this.m_uniqueName = this.m_listContentInstance.UniqueName;
							return;
						}
					}
					else
					{
						Global.Tracer.Assert(listContents.Count == 0);
					}
				}
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
		public override string DataElementName
		{
			get
			{
				List list = (List)base.OwnerDataRegion.ReportItemDef;
				if (list.Grouping == null)
				{
					return list.DataInstanceName;
				}
				return list.Grouping.DataElementName;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				List list = (List)base.OwnerDataRegion.ReportItemDef;
				if (list.Grouping == null)
				{
					return list.DataInstanceElementOutput;
				}
				return list.Grouping.DataElementOutput;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000EB24 File Offset: 0x0000CD24
		public override string ID
		{
			get
			{
				if (base.OwnerDataRegion.ReportItemDef.RenderingModelID == null)
				{
					base.OwnerDataRegion.ReportItemDef.RenderingModelID = ((List)base.OwnerDataRegion.ReportItemDef).ListContentID.ToString(CultureInfo.InvariantCulture);
				}
				return base.OwnerDataRegion.ReportItemDef.RenderingModelID;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000EB88 File Offset: 0x0000CD88
		public ReportItemCollection ReportItemCollection
		{
			get
			{
				ReportItemCollection reportItemCollection = this.m_reportItemCollection;
				if (this.m_reportItemCollection == null)
				{
					ReportItemColInstance reportItemColInstance = null;
					if (this.m_listContentInstance != null)
					{
						reportItemColInstance = this.m_listContentInstance.ReportItemColInstance;
					}
					reportItemCollection = new ReportItemCollection(((List)base.OwnerDataRegion.ReportItemDef).ReportItems, reportItemColInstance, base.OwnerDataRegion.RenderingContext, null);
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_reportItemCollection = reportItemCollection;
					}
				}
				return reportItemCollection;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000EBFC File Offset: 0x0000CDFC
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
					else if (this.m_listContentInstance == null)
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

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000EC5F File Offset: 0x0000CE5F
		public bool InDocumentMap
		{
			get
			{
				return this.m_listContentInstance != null && this.m_groupingDef != null && this.m_groupingDef.GroupLabel != null;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000EC84 File Offset: 0x0000CE84
		public override bool Hidden
		{
			get
			{
				if (this.m_listContentInstance == null)
				{
					return RenderingContext.GetDefinitionHidden(base.OwnerDataRegion.ReportItemDef.Visibility);
				}
				if (base.OwnerDataRegion.ReportItemDef.Visibility == null)
				{
					return false;
				}
				if (base.OwnerDataRegion.ReportItemDef.Visibility.Toggle != null)
				{
					return base.OwnerDataRegion.RenderingContext.IsItemHidden(this.m_listContentInstance.UniqueName, false);
				}
				return this.InstanceInfo.StartHidden;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000ED04 File Offset: 0x0000CF04
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				CustomPropertyCollection customPropertyCollection = this.m_customProperties;
				if (this.m_customProperties == null)
				{
					if (this.m_groupingDef == null || this.m_groupingDef.CustomProperties == null)
					{
						return null;
					}
					if (this.m_listContentInstance == null)
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_groupingDef.CustomProperties, null);
					}
					else
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_groupingDef.CustomProperties, this.InstanceInfo.CustomPropertyInstances);
					}
					if (base.OwnerDataRegion.RenderingContext.CacheState)
					{
						this.m_customProperties = customPropertyCollection;
					}
				}
				return customPropertyCollection;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000ED89 File Offset: 0x0000CF89
		internal ListContentInstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_listContentInstance == null)
				{
					return null;
				}
				if (this.m_listContentInstanceInfo == null)
				{
					this.m_listContentInstanceInfo = this.m_listContentInstance.GetInstanceInfo(base.OwnerDataRegion.RenderingContext.ChunkManager);
				}
				return this.m_listContentInstanceInfo;
			}
		}

		// Token: 0x040000F2 RID: 242
		private ListContentInstance m_listContentInstance;

		// Token: 0x040000F3 RID: 243
		private ListContentInstanceInfo m_listContentInstanceInfo;

		// Token: 0x040000F4 RID: 244
		private ReportItemCollection m_reportItemCollection;
	}
}
