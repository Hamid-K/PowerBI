using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004FC RID: 1276
	[Serializable]
	public abstract class ReportItem : IDOwner, IStyleContainer, IComparable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ICustomPropertiesHolder, IVisibilityOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable, IStaticReferenceable
	{
		// Token: 0x0600417A RID: 16762 RVA: 0x00113AF4 File Offset: 0x00111CF4
		protected ReportItem(int id, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(id)
		{
			this.m_parent = parent;
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x00113B24 File Offset: 0x00111D24
		protected ReportItem(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
		{
			this.m_parent = parent;
		}

		// Token: 0x17001B90 RID: 7056
		// (get) Token: 0x0600417C RID: 16764 RVA: 0x00113B53 File Offset: 0x00111D53
		// (set) Token: 0x0600417D RID: 16765 RVA: 0x00113B5B File Offset: 0x00111D5B
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17001B91 RID: 7057
		// (get) Token: 0x0600417E RID: 16766 RVA: 0x00113B64 File Offset: 0x00111D64
		// (set) Token: 0x0600417F RID: 16767 RVA: 0x00113B6C File Offset: 0x00111D6C
		public Microsoft.ReportingServices.ReportIntermediateFormat.Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x17001B92 RID: 7058
		// (get) Token: 0x06004180 RID: 16768 RVA: 0x00113B75 File Offset: 0x00111D75
		IInstancePath IStyleContainer.InstancePath
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001B93 RID: 7059
		// (get) Token: 0x06004181 RID: 16769 RVA: 0x00113B78 File Offset: 0x00111D78
		Microsoft.ReportingServices.ReportProcessing.ObjectType IStyleContainer.ObjectType
		{
			get
			{
				return this.ObjectType;
			}
		}

		// Token: 0x17001B94 RID: 7060
		// (get) Token: 0x06004182 RID: 16770 RVA: 0x00113B80 File Offset: 0x00111D80
		string IStyleContainer.Name
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17001B95 RID: 7061
		// (get) Token: 0x06004183 RID: 16771 RVA: 0x00113B88 File Offset: 0x00111D88
		// (set) Token: 0x06004184 RID: 16772 RVA: 0x00113B90 File Offset: 0x00111D90
		internal string Top
		{
			get
			{
				return this.m_top;
			}
			set
			{
				this.m_top = value;
			}
		}

		// Token: 0x17001B96 RID: 7062
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x00113B99 File Offset: 0x00111D99
		// (set) Token: 0x06004186 RID: 16774 RVA: 0x00113BA1 File Offset: 0x00111DA1
		internal double TopValue
		{
			get
			{
				return this.m_topValue;
			}
			set
			{
				this.m_topValue = value;
			}
		}

		// Token: 0x17001B97 RID: 7063
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x00113BAA File Offset: 0x00111DAA
		// (set) Token: 0x06004188 RID: 16776 RVA: 0x00113BB2 File Offset: 0x00111DB2
		internal string Left
		{
			get
			{
				return this.m_left;
			}
			set
			{
				this.m_left = value;
			}
		}

		// Token: 0x17001B98 RID: 7064
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x00113BBB File Offset: 0x00111DBB
		// (set) Token: 0x0600418A RID: 16778 RVA: 0x00113BC3 File Offset: 0x00111DC3
		internal double LeftValue
		{
			get
			{
				return this.m_leftValue;
			}
			set
			{
				this.m_leftValue = value;
			}
		}

		// Token: 0x17001B99 RID: 7065
		// (get) Token: 0x0600418B RID: 16779 RVA: 0x00113BCC File Offset: 0x00111DCC
		// (set) Token: 0x0600418C RID: 16780 RVA: 0x00113BD4 File Offset: 0x00111DD4
		internal string Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x17001B9A RID: 7066
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x00113BDD File Offset: 0x00111DDD
		// (set) Token: 0x0600418E RID: 16782 RVA: 0x00113BE5 File Offset: 0x00111DE5
		internal double HeightValue
		{
			get
			{
				return this.m_heightValue;
			}
			set
			{
				this.m_heightValue = value;
			}
		}

		// Token: 0x17001B9B RID: 7067
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x00113BEE File Offset: 0x00111DEE
		// (set) Token: 0x06004190 RID: 16784 RVA: 0x00113BF6 File Offset: 0x00111DF6
		internal string Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x17001B9C RID: 7068
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x00113BFF File Offset: 0x00111DFF
		// (set) Token: 0x06004192 RID: 16786 RVA: 0x00113C07 File Offset: 0x00111E07
		internal double WidthValue
		{
			get
			{
				return this.m_widthValue;
			}
			set
			{
				this.m_widthValue = value;
			}
		}

		// Token: 0x17001B9D RID: 7069
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x00113C10 File Offset: 0x00111E10
		internal double AbsoluteTopValue
		{
			get
			{
				if (this.m_heightValue < 0.0)
				{
					return Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.RoundSize(this.m_topValue + this.m_heightValue);
				}
				return Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.RoundSize(this.m_topValue);
			}
		}

		// Token: 0x17001B9E RID: 7070
		// (get) Token: 0x06004194 RID: 16788 RVA: 0x00113C41 File Offset: 0x00111E41
		internal double AbsoluteLeftValue
		{
			get
			{
				if (this.m_widthValue < 0.0)
				{
					return Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.RoundSize(this.m_leftValue + this.m_widthValue);
				}
				return Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.RoundSize(this.m_leftValue);
			}
		}

		// Token: 0x17001B9F RID: 7071
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x00113C72 File Offset: 0x00111E72
		internal double AbsoluteBottomValue
		{
			get
			{
				if (this.m_heightValue < 0.0)
				{
					return Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.RoundSize(this.m_topValue);
				}
				return Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.RoundSize(this.m_topValue + this.m_heightValue);
			}
		}

		// Token: 0x17001BA0 RID: 7072
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x00113CA3 File Offset: 0x00111EA3
		internal double AbsoluteRightValue
		{
			get
			{
				if (this.m_widthValue < 0.0)
				{
					return Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.RoundSize(this.m_leftValue);
				}
				return Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.RoundSize(this.m_leftValue + this.m_widthValue);
			}
		}

		// Token: 0x17001BA1 RID: 7073
		// (get) Token: 0x06004197 RID: 16791 RVA: 0x00113CD4 File Offset: 0x00111ED4
		// (set) Token: 0x06004198 RID: 16792 RVA: 0x00113CDC File Offset: 0x00111EDC
		internal int ZIndex
		{
			get
			{
				return this.m_zIndex;
			}
			set
			{
				this.m_zIndex = value;
			}
		}

		// Token: 0x17001BA2 RID: 7074
		// (get) Token: 0x06004199 RID: 16793 RVA: 0x00113CE5 File Offset: 0x00111EE5
		// (set) Token: 0x0600419A RID: 16794 RVA: 0x00113CED File Offset: 0x00111EED
		internal ExpressionInfo ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				this.m_toolTip = value;
			}
		}

		// Token: 0x17001BA3 RID: 7075
		// (get) Token: 0x0600419B RID: 16795 RVA: 0x00113CF6 File Offset: 0x00111EF6
		// (set) Token: 0x0600419C RID: 16796 RVA: 0x00113CFE File Offset: 0x00111EFE
		public Microsoft.ReportingServices.ReportIntermediateFormat.Visibility Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				this.m_visibility = value;
			}
		}

		// Token: 0x17001BA4 RID: 7076
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x00113D07 File Offset: 0x00111F07
		// (set) Token: 0x0600419E RID: 16798 RVA: 0x00113D0F File Offset: 0x00111F0F
		internal ExpressionInfo DocumentMapLabel
		{
			get
			{
				return this.m_documentMapLabel;
			}
			set
			{
				this.m_documentMapLabel = value;
			}
		}

		// Token: 0x17001BA5 RID: 7077
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x00113D18 File Offset: 0x00111F18
		// (set) Token: 0x060041A0 RID: 16800 RVA: 0x00113D20 File Offset: 0x00111F20
		internal ExpressionInfo Bookmark
		{
			get
			{
				return this.m_bookmark;
			}
			set
			{
				this.m_bookmark = value;
			}
		}

		// Token: 0x17001BA6 RID: 7078
		// (get) Token: 0x060041A1 RID: 16801 RVA: 0x00113D29 File Offset: 0x00111F29
		// (set) Token: 0x060041A2 RID: 16802 RVA: 0x00113D31 File Offset: 0x00111F31
		internal bool RepeatedSibling
		{
			get
			{
				return this.m_repeatedSibling;
			}
			set
			{
				this.m_repeatedSibling = value;
			}
		}

		// Token: 0x17001BA7 RID: 7079
		// (get) Token: 0x060041A3 RID: 16803 RVA: 0x00113D3A File Offset: 0x00111F3A
		// (set) Token: 0x060041A4 RID: 16804 RVA: 0x00113D42 File Offset: 0x00111F42
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x17001BA8 RID: 7080
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x00113D4B File Offset: 0x00111F4B
		// (set) Token: 0x060041A6 RID: 16806 RVA: 0x00113D53 File Offset: 0x00111F53
		internal string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x17001BA9 RID: 7081
		// (get) Token: 0x060041A7 RID: 16807 RVA: 0x00113D5C File Offset: 0x00111F5C
		internal virtual string DataElementNameDefault
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17001BAA RID: 7082
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x00113D64 File Offset: 0x00111F64
		// (set) Token: 0x060041A9 RID: 16809 RVA: 0x00113D6C File Offset: 0x00111F6C
		internal DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		// Token: 0x17001BAB RID: 7083
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x00113D75 File Offset: 0x00111F75
		// (set) Token: 0x060041AB RID: 16811 RVA: 0x00113D7D File Offset: 0x00111F7D
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
			}
		}

		// Token: 0x17001BAC RID: 7084
		// (get) Token: 0x060041AC RID: 16812 RVA: 0x00113D86 File Offset: 0x00111F86
		// (set) Token: 0x060041AD RID: 16813 RVA: 0x00113D8E File Offset: 0x00111F8E
		internal bool Computed
		{
			get
			{
				return this.m_computed;
			}
			set
			{
				this.m_computed = value;
			}
		}

		// Token: 0x17001BAD RID: 7085
		// (get) Token: 0x060041AE RID: 16814 RVA: 0x00113D97 File Offset: 0x00111F97
		internal virtual bool IsDataRegion
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x00113D9C File Offset: 0x00111F9C
		internal bool IsOrContainsDataRegionOrSubReport()
		{
			if (this.IsDataRegion)
			{
				return true;
			}
			if (this.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Map)
			{
				return ((Map)this).ContainsMapDataRegion();
			}
			return this.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Subreport || (this.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle && ((Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)this).ContainsDataRegionOrSubReport());
		}

		// Token: 0x17001BAE RID: 7086
		// (get) Token: 0x060041B0 RID: 16816 RVA: 0x00113DEA File Offset: 0x00111FEA
		// (set) Token: 0x060041B1 RID: 16817 RVA: 0x00113DF2 File Offset: 0x00111FF2
		internal string RepeatWith
		{
			get
			{
				return this.m_repeatWith;
			}
			set
			{
				this.m_repeatWith = value;
			}
		}

		// Token: 0x17001BAF RID: 7087
		// (get) Token: 0x060041B2 RID: 16818 RVA: 0x00113DFB File Offset: 0x00111FFB
		internal ReportItemExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001BB0 RID: 7088
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x00113E03 File Offset: 0x00112003
		// (set) Token: 0x060041B4 RID: 16820 RVA: 0x00113E0B File Offset: 0x0011200B
		internal virtual bool SoftPageBreak
		{
			get
			{
				return this.m_softPageBreak;
			}
			set
			{
				this.m_softPageBreak = value;
			}
		}

		// Token: 0x17001BB1 RID: 7089
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x00113E14 File Offset: 0x00112014
		// (set) Token: 0x060041B6 RID: 16822 RVA: 0x00113E1C File Offset: 0x0011201C
		internal virtual bool ShareMyLastPage
		{
			get
			{
				return this.m_shareMyLastPage;
			}
			set
			{
				this.m_shareMyLastPage = value;
			}
		}

		// Token: 0x17001BB2 RID: 7090
		// (get) Token: 0x060041B7 RID: 16823 RVA: 0x00113E25 File Offset: 0x00112025
		// (set) Token: 0x060041B8 RID: 16824 RVA: 0x00113E2D File Offset: 0x0011202D
		internal bool StartHidden
		{
			get
			{
				return this.m_startHidden;
			}
			set
			{
				this.m_startHidden = value;
			}
		}

		// Token: 0x17001BB3 RID: 7091
		// (get) Token: 0x060041B9 RID: 16825 RVA: 0x00113E36 File Offset: 0x00112036
		// (set) Token: 0x060041BA RID: 16826 RVA: 0x00113E3E File Offset: 0x0011203E
		internal StyleProperties SharedStyleProperties
		{
			get
			{
				return this.m_sharedStyleProperties;
			}
			set
			{
				this.m_sharedStyleProperties = value;
			}
		}

		// Token: 0x17001BB4 RID: 7092
		// (get) Token: 0x060041BB RID: 16827 RVA: 0x00113E47 File Offset: 0x00112047
		// (set) Token: 0x060041BC RID: 16828 RVA: 0x00113E4F File Offset: 0x0011204F
		internal bool NoNonSharedStyleProps
		{
			get
			{
				return this.m_noNonSharedStyleProps;
			}
			set
			{
				this.m_noNonSharedStyleProps = value;
			}
		}

		// Token: 0x17001BB5 RID: 7093
		// (get) Token: 0x060041BD RID: 16829 RVA: 0x00113E58 File Offset: 0x00112058
		// (set) Token: 0x060041BE RID: 16830 RVA: 0x00113E60 File Offset: 0x00112060
		internal ReportSize HeightForRendering
		{
			get
			{
				return this.m_heightForRendering;
			}
			set
			{
				this.m_heightForRendering = value;
			}
		}

		// Token: 0x17001BB6 RID: 7094
		// (get) Token: 0x060041BF RID: 16831 RVA: 0x00113E69 File Offset: 0x00112069
		// (set) Token: 0x060041C0 RID: 16832 RVA: 0x00113E71 File Offset: 0x00112071
		internal ReportSize WidthForRendering
		{
			get
			{
				return this.m_widthForRendering;
			}
			set
			{
				this.m_widthForRendering = value;
			}
		}

		// Token: 0x17001BB7 RID: 7095
		// (get) Token: 0x060041C1 RID: 16833 RVA: 0x00113E7A File Offset: 0x0011207A
		// (set) Token: 0x060041C2 RID: 16834 RVA: 0x00113E82 File Offset: 0x00112082
		internal ReportSize TopForRendering
		{
			get
			{
				return this.m_topForRendering;
			}
			set
			{
				this.m_topForRendering = value;
			}
		}

		// Token: 0x17001BB8 RID: 7096
		// (get) Token: 0x060041C3 RID: 16835 RVA: 0x00113E8B File Offset: 0x0011208B
		// (set) Token: 0x060041C4 RID: 16836 RVA: 0x00113E93 File Offset: 0x00112093
		internal ReportSize LeftForRendering
		{
			get
			{
				return this.m_leftForRendering;
			}
			set
			{
				this.m_leftForRendering = value;
			}
		}

		// Token: 0x17001BB9 RID: 7097
		// (get) Token: 0x060041C5 RID: 16837 RVA: 0x00113E9C File Offset: 0x0011209C
		internal virtual DataElementOutputTypes DataElementOutputDefault
		{
			get
			{
				return DataElementOutputTypes.Output;
			}
		}

		// Token: 0x17001BBA RID: 7098
		// (get) Token: 0x060041C6 RID: 16838 RVA: 0x00113E9F File Offset: 0x0011209F
		// (set) Token: 0x060041C7 RID: 16839 RVA: 0x00113EA7 File Offset: 0x001120A7
		internal double TopInStartPage
		{
			get
			{
				return this.m_topInPage;
			}
			set
			{
				this.m_topInPage = value;
			}
		}

		// Token: 0x17001BBB RID: 7099
		// (get) Token: 0x060041C8 RID: 16840 RVA: 0x00113EB0 File Offset: 0x001120B0
		// (set) Token: 0x060041C9 RID: 16841 RVA: 0x00113EB8 File Offset: 0x001120B8
		internal double BottomInEndPage
		{
			get
			{
				return this.m_bottomInPage;
			}
			set
			{
				this.m_bottomInPage = value;
			}
		}

		// Token: 0x17001BBC RID: 7100
		// (get) Token: 0x060041CA RID: 16842 RVA: 0x00113EC1 File Offset: 0x001120C1
		DataValueList ICustomPropertiesHolder.CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
		}

		// Token: 0x17001BBD RID: 7101
		// (get) Token: 0x060041CB RID: 16843 RVA: 0x00113EC9 File Offset: 0x001120C9
		IInstancePath ICustomPropertiesHolder.InstancePath
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001BBE RID: 7102
		// (get) Token: 0x060041CC RID: 16844 RVA: 0x00113ECC File Offset: 0x001120CC
		// (set) Token: 0x060041CD RID: 16845 RVA: 0x00113ED4 File Offset: 0x001120D4
		internal DataValueList CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
			set
			{
				this.m_customProperties = value;
			}
		}

		// Token: 0x17001BBF RID: 7103
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x00113EDD File Offset: 0x001120DD
		// (set) Token: 0x060041CF RID: 16847 RVA: 0x00113EE5 File Offset: 0x001120E5
		internal ReportProcessing.PageTextboxes RepeatedSiblingTextboxes
		{
			get
			{
				return this.m_repeatedSiblingTextboxes;
			}
			set
			{
				this.m_repeatedSiblingTextboxes = value;
			}
		}

		// Token: 0x17001BC0 RID: 7104
		// (get) Token: 0x060041D0 RID: 16848
		internal abstract Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType { get; }

		// Token: 0x17001BC1 RID: 7105
		// (get) Token: 0x060041D1 RID: 16849 RVA: 0x00113EEE File Offset: 0x001120EE
		// (set) Token: 0x060041D2 RID: 16850 RVA: 0x00113EF6 File Offset: 0x001120F6
		public IReportScopeInstance ROMScopeInstance
		{
			get
			{
				return this.m_romScopeInstance;
			}
			set
			{
				this.m_romScopeInstance = value;
				if (this.IsVisibilityCacheInstancePathInvalid())
				{
					this.ResetVisibilityComputationCache();
				}
			}
		}

		// Token: 0x17001BC2 RID: 7106
		// (get) Token: 0x060041D3 RID: 16851 RVA: 0x00113F0D File Offset: 0x0011210D
		// (set) Token: 0x060041D4 RID: 16852 RVA: 0x00113F15 File Offset: 0x00112115
		public IVisibilityOwner ContainingDynamicVisibility
		{
			get
			{
				return this.m_containingDynamicVisibility;
			}
			set
			{
				this.m_containingDynamicVisibility = value;
			}
		}

		// Token: 0x17001BC3 RID: 7107
		// (get) Token: 0x060041D5 RID: 16853 RVA: 0x00113F1E File Offset: 0x0011211E
		// (set) Token: 0x060041D6 RID: 16854 RVA: 0x00113F26 File Offset: 0x00112126
		public IVisibilityOwner ContainingDynamicColumnVisibility
		{
			get
			{
				return this.m_containingDynamicColumnVisibility;
			}
			set
			{
				this.m_containingDynamicColumnVisibility = value;
			}
		}

		// Token: 0x17001BC4 RID: 7108
		// (get) Token: 0x060041D7 RID: 16855 RVA: 0x00113F2F File Offset: 0x0011212F
		// (set) Token: 0x060041D8 RID: 16856 RVA: 0x00113F37 File Offset: 0x00112137
		public IVisibilityOwner ContainingDynamicRowVisibility
		{
			get
			{
				return this.m_containingDynamicRowVisibility;
			}
			set
			{
				this.m_containingDynamicRowVisibility = value;
			}
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x00113F40 File Offset: 0x00112140
		public bool ComputeHidden(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ToggleCascadeDirection direction)
		{
			if (!this.CanUseCachedVisibilityData(this.m_hasCachedHiddenValue))
			{
				this.UpdateVisibilityDataCacheFlag(ref this.m_hasCachedHiddenValue);
				bool flag;
				this.m_cachedHiddenValue = Microsoft.ReportingServices.ReportIntermediateFormat.Visibility.ComputeHidden(this, renderingContext, direction, out flag);
				if (flag)
				{
					this.m_hasCachedDeepHiddenValue = true;
					this.m_cachedDeepHiddenValue = this.m_cachedHiddenValue;
				}
			}
			return this.m_cachedHiddenValue;
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x00113F94 File Offset: 0x00112194
		public bool ComputeDeepHidden(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ToggleCascadeDirection direction)
		{
			if (!this.CanUseCachedVisibilityData(this.m_hasCachedDeepHiddenValue))
			{
				bool flag = this.ComputeHidden(renderingContext, direction);
				if (!this.m_hasCachedDeepHiddenValue)
				{
					this.m_hasCachedDeepHiddenValue = true;
					this.m_cachedDeepHiddenValue = Microsoft.ReportingServices.ReportIntermediateFormat.Visibility.ComputeDeepHidden(flag, this, direction, renderingContext);
				}
			}
			return this.m_cachedDeepHiddenValue;
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x00113FDC File Offset: 0x001121DC
		public bool ComputeStartHidden(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			if (!this.CanUseCachedVisibilityData(this.m_hasCachedStartHiddenValue))
			{
				this.UpdateVisibilityDataCacheFlag(ref this.m_hasCachedStartHiddenValue);
				if (this.m_visibility == null || this.m_visibility.Hidden == null)
				{
					this.m_cachedStartHiddenValue = false;
				}
				else if (!this.m_visibility.Hidden.IsExpression)
				{
					this.m_cachedStartHiddenValue = this.m_visibility.Hidden.BoolValue;
				}
				else
				{
					this.m_cachedStartHiddenValue = this.EvaluateStartHidden(this.m_romScopeInstance, renderingContext.OdpContext);
				}
			}
			return this.m_cachedStartHiddenValue;
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x00114069 File Offset: 0x00112269
		private bool CanUseCachedVisibilityData(bool cacheHasValue)
		{
			if (!cacheHasValue)
			{
				return false;
			}
			if ((this.m_romScopeInstance == null || this.m_romScopeInstance.IsNewContext) && this.IsVisibilityCacheInstancePathInvalid())
			{
				this.ResetVisibilityComputationCache();
				return false;
			}
			return true;
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x00114096 File Offset: 0x00112296
		private bool IsVisibilityCacheInstancePathInvalid()
		{
			return this.m_visibilityCacheLastInstancePath == null || (this.m_visibilityCacheLastInstancePath.Count > 0 && !InstancePathItem.IsSamePath(this.InstancePath, this.m_visibilityCacheLastInstancePath));
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x001140C6 File Offset: 0x001122C6
		private void UpdateVisibilityDataCacheFlag(ref bool cacheHasValue)
		{
			cacheHasValue = true;
			if (this.m_romScopeInstance == null || this.m_romScopeInstance.IsNewContext)
			{
				InstancePathItem.DeepCopyPath(this.InstancePath, ref this.m_visibilityCacheLastInstancePath);
			}
		}

		// Token: 0x17001BC5 RID: 7109
		// (get) Token: 0x060041DF RID: 16863 RVA: 0x001140F4 File Offset: 0x001122F4
		public string SenderUniqueName
		{
			get
			{
				if (this.m_visibility != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.TextBox toggleSender = this.m_visibility.ToggleSender;
					if (toggleSender != null)
					{
						return toggleSender.UniqueName;
					}
				}
				return null;
			}
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x00114120 File Offset: 0x00112320
		internal void ResetVisibilityComputationCache()
		{
			this.m_hasCachedHiddenValue = false;
			this.m_hasCachedDeepHiddenValue = false;
			this.m_hasCachedStartHiddenValue = false;
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x00114138 File Offset: 0x00112338
		internal virtual bool Initialize(InitializationContext context)
		{
			if (this.m_top == null)
			{
				this.m_top = "0mm";
				this.m_topValue = 0.0;
			}
			else
			{
				this.m_topValue = context.ValidateSize(ref this.m_top, "Top");
			}
			if (this.m_left == null)
			{
				this.m_left = "0mm";
				this.m_leftValue = 0.0;
			}
			else
			{
				this.m_leftValue = context.ValidateSize(ref this.m_left, "Left");
			}
			this.ValidateItemSizeAndBoundaries(context);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			if (this.m_documentMapLabel != null)
			{
				this.m_documentMapLabel.Initialize("Label", context);
				context.ExprHostBuilder.GenericLabel(this.m_documentMapLabel);
			}
			if (this.m_bookmark != null)
			{
				this.m_bookmark.Initialize("Bookmark", context);
				context.ExprHostBuilder.ReportItemBookmark(this.m_bookmark);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ReportItemToolTip(this.m_toolTip);
			}
			if (this.m_customProperties != null)
			{
				this.m_customProperties.Initialize(null, context);
			}
			this.DataRendererInitialize(context);
			return false;
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x00114276 File Offset: 0x00112476
		internal virtual void TraverseScopes(IRIFScopeVisitor visitor)
		{
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x00114278 File Offset: 0x00112478
		private void ValidateItemSizeAndBoundaries(InitializationContext context)
		{
			if (context.PublishingContext.PublishingContextKind != PublishingContextKind.DataShape)
			{
				if (this.m_parent != null)
				{
					bool flag = true;
					if (this.m_width == null)
					{
						if ((context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablix) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
						{
							if (Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix == context.ObjectType)
							{
								this.m_width = "0mm";
								this.m_widthValue = 0.0;
								flag = false;
							}
							else if (Microsoft.ReportingServices.ReportProcessing.ObjectType.PageHeader == context.ObjectType || Microsoft.ReportingServices.ReportProcessing.ObjectType.PageFooter == context.ObjectType)
							{
								ReportSection reportSection = this.m_parent as ReportSection;
								this.m_widthValue = reportSection.PageSectionWidth;
								this.m_width = Microsoft.ReportingServices.ReportPublishing.Converter.ConvertSize(this.m_widthValue);
							}
							else
							{
								this.m_widthValue = Math.Round(this.m_parent.m_widthValue - this.m_leftValue, 10);
								this.m_width = Microsoft.ReportingServices.ReportPublishing.Converter.ConvertSize(this.m_widthValue);
							}
						}
						else
						{
							flag = false;
						}
					}
					if (flag)
					{
						this.m_widthValue = context.ValidateSize(this.m_width, "Width");
					}
					flag = true;
					if (this.m_height == null)
					{
						if ((context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablix) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
						{
							if (Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix == context.ObjectType)
							{
								this.m_height = "0mm";
								this.m_heightValue = 0.0;
								flag = false;
							}
							else
							{
								this.m_heightValue = Math.Round(this.m_parent.m_heightValue - this.m_topValue, 10);
								this.m_height = Microsoft.ReportingServices.ReportPublishing.Converter.ConvertSize(this.m_heightValue);
							}
						}
						else
						{
							flag = false;
						}
					}
					if (flag)
					{
						this.m_heightValue = context.ValidateSize(this.m_height, "Height");
					}
				}
				else
				{
					this.m_widthValue = context.ValidateSize(ref this.m_width, "Width");
					this.m_heightValue = context.ValidateSize(ref this.m_height, "Height");
				}
				if ((context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablix) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					this.ValidateParentBoundaries(context, context.ObjectType, context.ObjectName);
				}
			}
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x00114460 File Offset: 0x00112660
		private void ValidateParentBoundaries(InitializationContext context, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			if (this.m_parent != null && !(this.m_parent is Microsoft.ReportingServices.ReportIntermediateFormat.Report) && !(this.m_parent is ReportSection))
			{
				if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Line)
				{
					if (this.AbsoluteTopValue < 0.0)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsReportItemOutsideContainer, Severity.Warning, objectType, objectName, "Top".ToLowerInvariant(), Array.Empty<string>());
					}
					if (this.AbsoluteLeftValue < 0.0)
					{
						context.ErrorContext.Register(ProcessingErrorCode.rsReportItemOutsideContainer, Severity.Warning, objectType, objectName, "Left".ToLowerInvariant(), Array.Empty<string>());
					}
				}
				if (this.AbsoluteBottomValue > Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.RoundSize(this.m_parent.HeightValue))
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsReportItemOutsideContainer, Severity.Warning, objectType, objectName, "Bottom".ToLowerInvariant(), Array.Empty<string>());
				}
				if (this.AbsoluteRightValue > Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.RoundSize(this.m_parent.WidthValue))
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsReportItemOutsideContainer, Severity.Warning, objectType, objectName, "Right".ToLowerInvariant(), Array.Empty<string>());
				}
			}
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x0011457E File Offset: 0x0011277E
		protected static double RoundSize(double size)
		{
			return Math.Round(Math.Round(size, 4), 1);
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x00114590 File Offset: 0x00112790
		protected virtual void DataRendererInitialize(InitializationContext context)
		{
			Microsoft.ReportingServices.ReportPublishing.CLSNameValidator.ValidateDataElementName(ref this.m_dataElementName, this.DataElementNameDefault, context.ObjectType, context.ObjectName, "DataElementName", context.ErrorContext);
			if (this.m_dataElementOutput == DataElementOutputTypes.Auto)
			{
				if ((this.m_visibility != null && this.m_visibility.Hidden != null && this.m_visibility.Hidden.Type == ExpressionInfo.Types.Constant && this.m_visibility.Hidden.BoolValue && this.m_visibility.Toggle == null) || (context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InNonToggleableHiddenStaticTablixMember) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					this.m_dataElementOutput = DataElementOutputTypes.NoOutput;
					return;
				}
				this.m_dataElementOutput = this.DataElementOutputDefault;
			}
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x0011463C File Offset: 0x0011283C
		internal virtual void CalculateSizes(double width, double height, InitializationContext context, bool overwrite)
		{
			if (overwrite)
			{
				this.m_top = "0mm";
				this.m_topValue = 0.0;
				this.m_left = "0mm";
				this.m_leftValue = 0.0;
			}
			if (this.m_width == null || (overwrite && this.m_widthValue != width))
			{
				this.m_width = width.ToString("f5", CultureInfo.InvariantCulture) + "mm";
				this.m_widthValue = context.ValidateSize(ref this.m_width, "Width");
			}
			if (this.m_height == null || (overwrite && this.m_heightValue != height))
			{
				this.m_height = height.ToString("f5", CultureInfo.InvariantCulture) + "mm";
				this.m_heightValue = context.ValidateSize(ref this.m_height, "Height");
			}
			this.ValidateParentBoundaries(context, this.ObjectType, this.Name);
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x00114730 File Offset: 0x00112930
		internal void CalculateSizes(InitializationContext context, bool overwrite)
		{
			double num = this.m_widthValue;
			double num2 = this.m_heightValue;
			if (this.m_width == null)
			{
				num = Math.Round(Math.Max(0.0, this.m_parent.m_widthValue - this.m_leftValue), 10);
			}
			if (this.m_height == null)
			{
				num2 = Math.Round(Math.Max(0.0, this.m_parent.m_heightValue - this.m_topValue), 10);
			}
			this.CalculateSizes(num, num2, context, overwrite);
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x001147B5 File Offset: 0x001129B5
		internal virtual void InitializeRVDirectionDependentItems(InitializationContext context)
		{
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x001147B7 File Offset: 0x001129B7
		internal virtual void DetermineGroupingExprValueCount(InitializationContext context, int groupingExprCount)
		{
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x001147BC File Offset: 0x001129BC
		int IComparable.CompareTo(object obj)
		{
			if (!(obj is Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem))
			{
				throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem)obj;
			if (this.m_topValue < reportItem.m_topValue)
			{
				return -1;
			}
			if (this.m_topValue > reportItem.m_topValue)
			{
				return 1;
			}
			if (this.m_leftValue < reportItem.m_leftValue)
			{
				return -1;
			}
			if (this.m_leftValue > reportItem.m_leftValue)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x00114824 File Offset: 0x00112A24
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem)base.PublishClone(context);
			reportItem.m_name = context.CreateUniqueReportItemName(this.m_name, this.m_isClone);
			if (this.m_styleClass != null)
			{
				reportItem.m_styleClass = (Microsoft.ReportingServices.ReportIntermediateFormat.Style)this.m_styleClass.PublishClone(context);
			}
			if (this.m_top != null)
			{
				reportItem.m_top = (string)this.m_top.Clone();
			}
			if (this.m_left != null)
			{
				reportItem.m_left = (string)this.m_left.Clone();
			}
			if (this.m_height != null)
			{
				reportItem.m_height = (string)this.m_height.Clone();
			}
			if (this.m_width != null)
			{
				reportItem.m_width = (string)this.m_width.Clone();
			}
			if (this.m_toolTip != null)
			{
				reportItem.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_visibility != null)
			{
				reportItem.m_visibility = (Microsoft.ReportingServices.ReportIntermediateFormat.Visibility)this.m_visibility.PublishClone(context, false);
			}
			reportItem.m_documentMapLabel = null;
			reportItem.m_bookmark = null;
			if (this.m_dataElementName != null)
			{
				reportItem.m_dataElementName = (string)this.m_dataElementName.Clone();
			}
			if (this.m_repeatWith != null)
			{
				context.AddReportItemWithRepeatWithToUpdate(reportItem);
				reportItem.m_repeatWith = (string)this.m_repeatWith.Clone();
			}
			if (this.m_customProperties != null)
			{
				reportItem.m_customProperties = new DataValueList(this.m_customProperties.Count);
				foreach (object obj in this.m_customProperties)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValue = (Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)obj;
					reportItem.m_customProperties.Add((Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)dataValue.PublishClone(context));
				}
			}
			return reportItem;
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x001149FC File Offset: 0x00112BFC
		internal override void SetupCriRenderItemDef(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem)
		{
			base.SetupCriRenderItemDef(reportItem);
			reportItem.Name = this.Name + "." + reportItem.Name;
			reportItem.DataElementName = reportItem.Name;
			reportItem.DataElementOutput = this.DataElementOutput;
			reportItem.RepeatWith = this.RepeatWith;
			reportItem.RepeatedSibling = this.RepeatedSibling;
			reportItem.Top = this.Top;
			reportItem.TopValue = this.TopValue;
			reportItem.Left = this.Left;
			reportItem.LeftValue = this.LeftValue;
			reportItem.Height = this.Height;
			reportItem.HeightValue = this.HeightValue;
			reportItem.Width = this.Width;
			reportItem.WidthValue = this.WidthValue;
			reportItem.ZIndex = this.ZIndex;
			reportItem.Visibility = this.Visibility;
			reportItem.Computed = true;
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x00114ADB File Offset: 0x00112CDB
		internal void UpdateRepeatWithReference(AutomaticSubtotalContext context)
		{
			if (this.m_repeatWith != null)
			{
				this.m_repeatWith = context.GetNewReportItemName(this.m_repeatWith);
			}
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x00114AF8 File Offset: 0x00112CF8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.StyleClass, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style),
				new MemberInfo(MemberName.Top, Token.String),
				new MemberInfo(MemberName.TopValue, Token.Double),
				new MemberInfo(MemberName.Left, Token.String),
				new MemberInfo(MemberName.LeftValue, Token.Double),
				new MemberInfo(MemberName.Height, Token.String),
				new MemberInfo(MemberName.HeightValue, Token.Double),
				new MemberInfo(MemberName.Width, Token.String),
				new MemberInfo(MemberName.WidthValue, Token.Double),
				new MemberInfo(MemberName.ZIndex, Token.Int32),
				new MemberInfo(MemberName.Visibility, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Visibility),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Label, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Bookmark, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.RepeatedSibling, Token.Boolean),
				new MemberInfo(MemberName.IsFullSize, Token.Boolean),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum),
				new MemberInfo(MemberName.CustomProperties, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataValue),
				new MemberInfo(MemberName.Computed, Token.Boolean),
				new MemberInfo(MemberName.ContainingDynamicVisibility, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IVisibilityOwner, Token.Reference),
				new MemberInfo(MemberName.ContainingDynamicRowVisibility, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IVisibilityOwner, Token.Reference),
				new MemberInfo(MemberName.ContainingDynamicColumnVisibility, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IVisibilityOwner, Token.Reference),
				new MemberInfo(MemberName.RepeatWith, Token.String)
			});
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x00114D3C File Offset: 0x00112F3C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					if (memberName <= MemberName.ToolTip)
					{
						if (memberName == MemberName.Name)
						{
							writer.Write(this.m_name);
							continue;
						}
						switch (memberName)
						{
						case MemberName.StyleClass:
							writer.Write(this.m_styleClass);
							continue;
						case MemberName.Top:
							writer.Write(this.m_top);
							continue;
						case MemberName.TopValue:
							writer.Write(this.m_topValue);
							continue;
						case MemberName.Left:
							writer.Write(this.m_left);
							continue;
						case MemberName.LeftValue:
							writer.Write(this.m_leftValue);
							continue;
						case MemberName.Height:
							writer.Write(this.m_height);
							continue;
						case MemberName.HeightValue:
							writer.Write(this.m_heightValue);
							continue;
						case MemberName.Width:
							writer.Write(this.m_width);
							continue;
						case MemberName.WidthValue:
							writer.Write(this.m_widthValue);
							continue;
						case MemberName.ZIndex:
							writer.Write(this.m_zIndex);
							continue;
						case MemberName.Visibility:
							writer.Write(this.m_visibility);
							continue;
						case MemberName.Label:
							writer.Write(this.m_documentMapLabel);
							continue;
						case MemberName.RepeatedSibling:
							writer.Write(this.m_repeatedSibling);
							continue;
						default:
							if (memberName == MemberName.ToolTip)
							{
								writer.Write(this.m_toolTip);
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.Bookmark)
						{
							writer.Write(this.m_bookmark);
							continue;
						}
						if (memberName == MemberName.IsFullSize)
						{
							writer.Write(this.m_isFullSize);
							continue;
						}
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.CustomProperties)
				{
					if (memberName == MemberName.DataElementName)
					{
						writer.Write(this.m_dataElementName);
						continue;
					}
					if (memberName == MemberName.DataElementOutput)
					{
						writer.WriteEnum((int)this.m_dataElementOutput);
						continue;
					}
					if (memberName == MemberName.CustomProperties)
					{
						writer.Write(this.m_customProperties);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Computed)
					{
						writer.Write(this.m_computed);
						continue;
					}
					switch (memberName)
					{
					case MemberName.ContainingDynamicVisibility:
						writer.WriteReference(this.m_containingDynamicVisibility);
						continue;
					case MemberName.ContainingDynamicRowVisibility:
						writer.WriteReference(this.m_containingDynamicRowVisibility);
						continue;
					case MemberName.ContainingDynamicColumnVisibility:
						writer.WriteReference(this.m_containingDynamicColumnVisibility);
						continue;
					default:
						if (memberName == MemberName.RepeatWith)
						{
							writer.Write(this.m_repeatWith);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x00115034 File Offset: 0x00113234
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					if (memberName <= MemberName.ToolTip)
					{
						if (memberName == MemberName.Name)
						{
							this.m_name = reader.ReadString();
							continue;
						}
						switch (memberName)
						{
						case MemberName.StyleClass:
							this.m_styleClass = (Microsoft.ReportingServices.ReportIntermediateFormat.Style)reader.ReadRIFObject();
							continue;
						case MemberName.Top:
							this.m_top = reader.ReadString();
							continue;
						case MemberName.TopValue:
							this.m_topValue = reader.ReadDouble();
							continue;
						case MemberName.Left:
							this.m_left = reader.ReadString();
							continue;
						case MemberName.LeftValue:
							this.m_leftValue = reader.ReadDouble();
							continue;
						case MemberName.Height:
							this.m_height = reader.ReadString();
							continue;
						case MemberName.HeightValue:
							this.m_heightValue = reader.ReadDouble();
							continue;
						case MemberName.Width:
							this.m_width = reader.ReadString();
							continue;
						case MemberName.WidthValue:
							this.m_widthValue = reader.ReadDouble();
							continue;
						case MemberName.ZIndex:
							this.m_zIndex = reader.ReadInt32();
							continue;
						case MemberName.Visibility:
							this.m_visibility = (Microsoft.ReportingServices.ReportIntermediateFormat.Visibility)reader.ReadRIFObject();
							continue;
						case MemberName.Label:
							this.m_documentMapLabel = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.RepeatedSibling:
							this.m_repeatedSibling = reader.ReadBoolean();
							continue;
						default:
							if (memberName == MemberName.ToolTip)
							{
								this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.Bookmark)
						{
							this.m_bookmark = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.IsFullSize)
						{
							this.m_isFullSize = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.CustomProperties)
				{
					if (memberName == MemberName.DataElementName)
					{
						this.m_dataElementName = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.DataElementOutput)
					{
						this.m_dataElementOutput = (DataElementOutputTypes)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.CustomProperties)
					{
						this.m_customProperties = reader.ReadListOfRIFObjects<DataValueList>();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Computed)
					{
						this.m_computed = reader.ReadBoolean();
						continue;
					}
					switch (memberName)
					{
					case MemberName.ContainingDynamicVisibility:
						this.m_containingDynamicVisibility = reader.ReadReference<IVisibilityOwner>(this);
						continue;
					case MemberName.ContainingDynamicRowVisibility:
						this.m_containingDynamicRowVisibility = reader.ReadReference<IVisibilityOwner>(this);
						continue;
					case MemberName.ContainingDynamicColumnVisibility:
						this.m_containingDynamicColumnVisibility = reader.ReadReference<IVisibilityOwner>(this);
						continue;
					default:
						if (memberName == MemberName.RepeatWith)
						{
							this.m_repeatWith = reader.ReadString();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x00115348 File Offset: 0x00113548
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					switch (memberReference.MemberName)
					{
					case MemberName.ContainingDynamicVisibility:
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
						if (referenceableItems.TryGetValue(memberReference.RefID, out referenceable))
						{
							this.m_containingDynamicVisibility = referenceable as IVisibilityOwner;
						}
						break;
					}
					case MemberName.ContainingDynamicRowVisibility:
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable2;
						if (referenceableItems.TryGetValue(memberReference.RefID, out referenceable2))
						{
							this.m_containingDynamicRowVisibility = referenceable2 as IVisibilityOwner;
						}
						break;
					}
					case MemberName.ContainingDynamicColumnVisibility:
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable3;
						if (referenceableItems.TryGetValue(memberReference.RefID, out referenceable3))
						{
							this.m_containingDynamicColumnVisibility = referenceable3 as IVisibilityOwner;
						}
						break;
					}
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
			}
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x00115438 File Offset: 0x00113638
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem;
		}

		// Token: 0x060041F4 RID: 16884
		internal abstract void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel);

		// Token: 0x060041F5 RID: 16885 RVA: 0x00115440 File Offset: 0x00113640
		protected void ReportItemSetExprHost(ReportItemExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(this.m_exprHost);
			}
			if (this.m_exprHost.CustomPropertyHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_customProperties != null, "(null != m_customProperties)");
				this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x001154C3 File Offset: 0x001136C3
		internal bool EvaluateStartHidden(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, romInstance);
			return context.ReportRuntime.EvaluateStartHiddenExpression(this.Visibility, this.m_exprHost, this.ObjectType, this.m_name);
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x001154F0 File Offset: 0x001136F0
		internal string EvaluateBookmark(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, romInstance);
			return context.ReportRuntime.EvaluateReportItemBookmarkExpression(this);
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x00115506 File Offset: 0x00113706
		internal string EvaluateDocumentMapLabel(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, romInstance);
			return context.ReportRuntime.EvaluateReportItemDocumentMapLabelExpression(this);
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x0011551C File Offset: 0x0011371C
		internal string EvaluateToolTip(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, romInstance);
			return context.ReportRuntime.EvaluateReportItemToolTipExpression(this);
		}

		// Token: 0x17001BC6 RID: 7110
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x00115532 File Offset: 0x00113732
		int IStaticReferenceable.ID
		{
			get
			{
				return this.m_staticRefId;
			}
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x0011553A File Offset: 0x0011373A
		void IStaticReferenceable.SetID(int id)
		{
			this.m_staticRefId = id;
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x00115543 File Offset: 0x00113743
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType IStaticReferenceable.GetObjectType()
		{
			return this.GetObjectType();
		}

		// Token: 0x04001DFD RID: 7677
		private const string ZeroSize = "0mm";

		// Token: 0x04001DFE RID: 7678
		private const int OverlapDetectionRounding = 1;

		// Token: 0x04001DFF RID: 7679
		protected string m_name;

		// Token: 0x04001E00 RID: 7680
		protected Microsoft.ReportingServices.ReportIntermediateFormat.Style m_styleClass;

		// Token: 0x04001E01 RID: 7681
		protected string m_top;

		// Token: 0x04001E02 RID: 7682
		protected double m_topValue;

		// Token: 0x04001E03 RID: 7683
		protected string m_left;

		// Token: 0x04001E04 RID: 7684
		protected double m_leftValue;

		// Token: 0x04001E05 RID: 7685
		protected string m_height;

		// Token: 0x04001E06 RID: 7686
		protected double m_heightValue;

		// Token: 0x04001E07 RID: 7687
		protected string m_width;

		// Token: 0x04001E08 RID: 7688
		protected double m_widthValue;

		// Token: 0x04001E09 RID: 7689
		protected int m_zIndex;

		// Token: 0x04001E0A RID: 7690
		protected ExpressionInfo m_toolTip;

		// Token: 0x04001E0B RID: 7691
		protected Microsoft.ReportingServices.ReportIntermediateFormat.Visibility m_visibility;

		// Token: 0x04001E0C RID: 7692
		protected ExpressionInfo m_documentMapLabel;

		// Token: 0x04001E0D RID: 7693
		protected ExpressionInfo m_bookmark;

		// Token: 0x04001E0E RID: 7694
		protected bool m_repeatedSibling;

		// Token: 0x04001E0F RID: 7695
		protected bool m_isFullSize;

		// Token: 0x04001E10 RID: 7696
		private int m_exprHostID = -1;

		// Token: 0x04001E11 RID: 7697
		protected string m_dataElementName;

		// Token: 0x04001E12 RID: 7698
		protected DataElementOutputTypes m_dataElementOutput = DataElementOutputTypes.Auto;

		// Token: 0x04001E13 RID: 7699
		protected DataValueList m_customProperties;

		// Token: 0x04001E14 RID: 7700
		protected bool m_computed;

		// Token: 0x04001E15 RID: 7701
		protected string m_repeatWith;

		// Token: 0x04001E16 RID: 7702
		[Reference]
		private IVisibilityOwner m_containingDynamicVisibility;

		// Token: 0x04001E17 RID: 7703
		[Reference]
		private IVisibilityOwner m_containingDynamicRowVisibility;

		// Token: 0x04001E18 RID: 7704
		[Reference]
		private IVisibilityOwner m_containingDynamicColumnVisibility;

		// Token: 0x04001E19 RID: 7705
		[NonSerialized]
		protected Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem m_parent;

		// Token: 0x04001E1A RID: 7706
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem.GetDeclaration();

		// Token: 0x04001E1B RID: 7707
		[NonSerialized]
		private ReportItemExprHost m_exprHost;

		// Token: 0x04001E1C RID: 7708
		[NonSerialized]
		protected bool m_softPageBreak;

		// Token: 0x04001E1D RID: 7709
		[NonSerialized]
		protected bool m_shareMyLastPage = true;

		// Token: 0x04001E1E RID: 7710
		[NonSerialized]
		protected bool m_startHidden;

		// Token: 0x04001E1F RID: 7711
		[NonSerialized]
		protected double m_topInPage;

		// Token: 0x04001E20 RID: 7712
		[NonSerialized]
		protected double m_bottomInPage;

		// Token: 0x04001E21 RID: 7713
		[NonSerialized]
		private ReportProcessing.PageTextboxes m_repeatedSiblingTextboxes;

		// Token: 0x04001E22 RID: 7714
		[NonSerialized]
		private int m_staticRefId = int.MinValue;

		// Token: 0x04001E23 RID: 7715
		[NonSerialized]
		private IReportScopeInstance m_romScopeInstance;

		// Token: 0x04001E24 RID: 7716
		[NonSerialized]
		private bool m_cachedHiddenValue;

		// Token: 0x04001E25 RID: 7717
		[NonSerialized]
		private bool m_cachedDeepHiddenValue;

		// Token: 0x04001E26 RID: 7718
		[NonSerialized]
		private bool m_cachedStartHiddenValue;

		// Token: 0x04001E27 RID: 7719
		[NonSerialized]
		private bool m_hasCachedHiddenValue;

		// Token: 0x04001E28 RID: 7720
		[NonSerialized]
		private bool m_hasCachedDeepHiddenValue;

		// Token: 0x04001E29 RID: 7721
		[NonSerialized]
		private bool m_hasCachedStartHiddenValue;

		// Token: 0x04001E2A RID: 7722
		[NonSerialized]
		private List<InstancePathItem> m_visibilityCacheLastInstancePath;

		// Token: 0x04001E2B RID: 7723
		[NonSerialized]
		protected StyleProperties m_sharedStyleProperties;

		// Token: 0x04001E2C RID: 7724
		[NonSerialized]
		protected bool m_noNonSharedStyleProps;

		// Token: 0x04001E2D RID: 7725
		[NonSerialized]
		protected ReportSize m_heightForRendering;

		// Token: 0x04001E2E RID: 7726
		[NonSerialized]
		protected ReportSize m_widthForRendering;

		// Token: 0x04001E2F RID: 7727
		[NonSerialized]
		protected ReportSize m_topForRendering;

		// Token: 0x04001E30 RID: 7728
		[NonSerialized]
		protected ReportSize m_leftForRendering;

		// Token: 0x0200097E RID: 2430
		internal enum DataElementStyles
		{
			// Token: 0x040040FB RID: 16635
			Attribute,
			// Token: 0x040040FC RID: 16636
			Element,
			// Token: 0x040040FD RID: 16637
			Auto
		}
	}
}
