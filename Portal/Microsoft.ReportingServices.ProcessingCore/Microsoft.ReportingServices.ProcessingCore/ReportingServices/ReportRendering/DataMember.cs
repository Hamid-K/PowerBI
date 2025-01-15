using System;
using System.Collections;
using System.Globalization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000067 RID: 103
	public sealed class DataMember : Group
	{
		// Token: 0x060006D1 RID: 1745 RVA: 0x0001A220 File Offset: 0x00018420
		internal DataMember(CustomReportItem owner, DataMember parent, CustomReportItemHeading headingDef, CustomReportItemHeadingInstance headingInstance, bool isSubtotal, int index)
			: base(owner, headingDef.Grouping)
		{
			Global.Tracer.Assert(headingDef != null);
			this.m_parent = parent;
			this.m_headingDef = headingDef;
			this.m_headingInstance = headingInstance;
			this.m_index = index;
			this.m_isSubtotal = isSubtotal;
			this.m_uniqueName = -1;
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001A278 File Offset: 0x00018478
		public override string ID
		{
			get
			{
				return this.m_headingDef.ID.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001A29D File Offset: 0x0001849D
		internal override TextBox ToggleParent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001A2A0 File Offset: 0x000184A0
		public override bool IsToggleChild
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001A2A3 File Offset: 0x000184A3
		public override SharedHiddenState SharedHidden
		{
			get
			{
				return SharedHiddenState.Never;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001A2A6 File Offset: 0x000184A6
		public override bool Hidden
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001A2AC File Offset: 0x000184AC
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				CustomPropertyCollection customPropertyCollection = this.m_customProperties;
				if (this.m_customProperties == null)
				{
					if (this.m_headingDef.CustomProperties == null)
					{
						return null;
					}
					if (this.m_headingInstance == null)
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_headingDef.CustomProperties, null);
					}
					else
					{
						customPropertyCollection = new CustomPropertyCollection(this.m_headingDef.CustomProperties, this.m_headingInstance.CustomPropertyInstances);
					}
					if (this.m_ownerItem.UseCache)
					{
						this.m_customProperties = customPropertyCollection;
					}
				}
				return customPropertyCollection;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001A324 File Offset: 0x00018524
		public ValueCollection GroupValues
		{
			get
			{
				if (this.m_groupingDef == null || this.m_groupingDef.GroupExpressions == null || this.m_groupingDef.GroupExpressions.Count == 0)
				{
					return null;
				}
				int count = this.m_groupingDef.GroupExpressions.Count;
				ArrayList arrayList = new ArrayList(count);
				for (int i = 0; i < count; i++)
				{
					object obj;
					if (this.m_groupingDef.GroupExpressions[i].Type == ExpressionInfo.Types.Constant)
					{
						obj = this.m_groupingDef.GroupExpressions[i].Value;
					}
					else if (this.m_headingInstance == null || this.m_headingInstance.GroupExpressionValues == null)
					{
						obj = null;
					}
					else
					{
						obj = this.m_headingInstance.GroupExpressionValues[i];
					}
					arrayList.Add(obj);
				}
				return new ValueCollection(arrayList);
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001A3EC File Offset: 0x000185EC
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
						text = this.m_headingInstance.Label;
					}
				}
				return text;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001A44F File Offset: 0x0001864F
		public DataMember Parent
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001A458 File Offset: 0x00018658
		public DataGroupingCollection Children
		{
			get
			{
				CustomReportItemHeadingList innerHeadings = this.m_headingDef.InnerHeadings;
				if (innerHeadings == null)
				{
					return null;
				}
				DataGroupingCollection dataGroupingCollection = this.m_children;
				if (this.m_children == null)
				{
					CustomReportItemHeadingInstanceList customReportItemHeadingInstanceList = null;
					if (this.m_headingInstance == null)
					{
						return null;
					}
					if (this.m_headingInstance != null)
					{
						customReportItemHeadingInstanceList = this.m_headingInstance.SubHeadingInstances;
					}
					dataGroupingCollection = new DataGroupingCollection((CustomReportItem)this.m_ownerItem, this, innerHeadings, customReportItemHeadingInstanceList);
					if (this.m_ownerItem.UseCache)
					{
						this.m_children = dataGroupingCollection;
					}
				}
				return dataGroupingCollection;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001A4CE File Offset: 0x000186CE
		public bool IsTotal
		{
			get
			{
				Global.Tracer.Assert((this.m_isSubtotal && !this.m_headingDef.Subtotal) || !this.m_isSubtotal);
				return this.m_isSubtotal;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0001A501 File Offset: 0x00018701
		public int MemberCellIndex
		{
			get
			{
				if (this.m_headingInstance == null)
				{
					return -1;
				}
				return this.m_headingInstance.HeadingCellIndex;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001A518 File Offset: 0x00018718
		public int MemberHeadingSpan
		{
			get
			{
				if (this.m_headingInstance == null)
				{
					return -1;
				}
				return this.m_headingInstance.HeadingSpan;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001A530 File Offset: 0x00018730
		public override string DataElementName
		{
			get
			{
				if (this.m_headingDef.Grouping != null)
				{
					return base.DataElementName;
				}
				if (this.m_headingInstance != null && this.m_headingInstance.Label != null)
				{
					return this.m_headingInstance.Label;
				}
				if (!this.m_headingDef.IsColumn)
				{
					return "Row" + this.m_index.ToString(CultureInfo.InvariantCulture);
				}
				return "Column" + this.m_index.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001A5B4 File Offset: 0x000187B4
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.m_headingDef.Grouping == null)
				{
					return DataElementOutputTypes.Output;
				}
				return base.DataElementOutput;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001A5CB File Offset: 0x000187CB
		public bool IsStatic
		{
			get
			{
				return this.m_headingDef.Static;
			}
		}

		// Token: 0x040001DB RID: 475
		private CustomReportItemHeading m_headingDef;

		// Token: 0x040001DC RID: 476
		private CustomReportItemHeadingInstance m_headingInstance;

		// Token: 0x040001DD RID: 477
		private DataGroupingCollection m_children;

		// Token: 0x040001DE RID: 478
		private DataMember m_parent;

		// Token: 0x040001DF RID: 479
		private bool m_isSubtotal;

		// Token: 0x040001E0 RID: 480
		private int m_index;
	}
}
