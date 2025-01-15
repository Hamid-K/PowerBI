using System;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000026 RID: 38
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ReportItem : IDocumentMapEntry
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000C444 File Offset: 0x0000A644
		protected ReportItem(string definitionName, string instanceName)
		{
			this.m_members = new ReportItemProcessing();
			this.Processing.DefinitionName = definitionName;
			this.m_uniqueName = instanceName;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000C46A File Offset: 0x0000A66A
		protected ReportItem()
		{
			this.m_members = new ReportItemProcessing();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000C480 File Offset: 0x0000A680
		internal ReportItem(CustomReportItem criDef, CustomReportItemInstance criInstance, CustomReportItemInstanceInfo instanceInfo)
		{
			this.m_members = new ReportItemRendering();
			this.Rendering.m_reportItemDef = criDef;
			this.Rendering.m_reportItemInstance = criInstance;
			this.Rendering.m_reportItemInstanceInfo = instanceInfo;
			this.m_intUniqueName = criInstance.UniqueName;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		internal ReportItem(string uniqueName, int intUniqueName, ReportItem reportItemDef, ReportItemInstance reportItemInstance, RenderingContext renderingContext)
		{
			this.m_members = new ReportItemRendering();
			this.m_uniqueName = uniqueName;
			this.m_intUniqueName = intUniqueName;
			this.Rendering.m_renderingContext = renderingContext;
			this.Rendering.m_reportItemDef = reportItemDef;
			this.Rendering.m_reportItemInstance = reportItemInstance;
			this.Rendering.m_headingInstance = renderingContext.HeadingInstance;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000C534 File Offset: 0x0000A734
		public string Name
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.DefinitionName;
				}
				return this.ReportItemDef.Name;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000C558 File Offset: 0x0000A758
		public string ID
		{
			get
			{
				if (this.IsCustomControl)
				{
					return null;
				}
				if (this.ReportItemDef.RenderingModelID == null)
				{
					this.ReportItemDef.RenderingModelID = this.ReportItemDef.ID.ToString(CultureInfo.InvariantCulture);
				}
				return this.ReportItemDef.RenderingModelID;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000C5AA File Offset: 0x0000A7AA
		public bool InDocumentMap
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.Label != null;
				}
				return this.Label != null;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		public bool IsFullSize
		{
			get
			{
				return !this.IsCustomControl && this.ReportItemDef.IsFullSize;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000C5E4 File Offset: 0x0000A7E4
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000C653 File Offset: 0x0000A853
		public string Label
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.Label;
				}
				string text = null;
				if (this.ReportItemDef.Label != null)
				{
					if (this.ReportItemDef.Label.Type == ExpressionInfo.Types.Constant)
					{
						text = this.ReportItemDef.Label.Value;
					}
					else if (this.ReportItemInstance == null)
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
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.Label = value;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000C674 File Offset: 0x0000A874
		public virtual int LinkToChild
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000C678 File Offset: 0x0000A878
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000C6E7 File Offset: 0x0000A8E7
		public string Bookmark
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.Bookmark;
				}
				string text = null;
				if (this.ReportItemDef.Bookmark != null)
				{
					if (this.ReportItemDef.Bookmark.Type == ExpressionInfo.Types.Constant)
					{
						text = this.ReportItemDef.Bookmark.Value;
					}
					else if (this.ReportItemInstance == null)
					{
						text = null;
					}
					else
					{
						text = this.InstanceInfo.Bookmark;
					}
				}
				return text;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.Bookmark = value;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000C708 File Offset: 0x0000A908
		public string UniqueName
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.m_uniqueName;
				}
				string text = this.m_uniqueName;
				if (this.m_uniqueName == null && this.m_intUniqueName != 0)
				{
					text = this.m_intUniqueName.ToString(CultureInfo.InvariantCulture);
					if (this.UseCache)
					{
						this.m_uniqueName = text;
					}
				}
				return text;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000C75C File Offset: 0x0000A95C
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000C7BB File Offset: 0x0000A9BB
		public ReportSize Height
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.Height;
				}
				if (this.ReportItemDef.HeightForRendering == null)
				{
					this.ReportItemDef.HeightForRendering = new ReportSize(this.ReportItemDef.Height, this.ReportItemDef.HeightValue);
				}
				return this.ReportItemDef.HeightForRendering;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.Height = value;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0000C83B File Offset: 0x0000AA3B
		public ReportSize Width
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.Width;
				}
				if (this.ReportItemDef.WidthForRendering == null)
				{
					this.ReportItemDef.WidthForRendering = new ReportSize(this.ReportItemDef.Width, this.ReportItemDef.WidthValue);
				}
				return this.ReportItemDef.WidthForRendering;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.Width = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000C85C File Offset: 0x0000AA5C
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000C8BB File Offset: 0x0000AABB
		public ReportSize Top
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.Top;
				}
				if (this.ReportItemDef.TopForRendering == null)
				{
					this.ReportItemDef.TopForRendering = new ReportSize(this.ReportItemDef.Top, this.ReportItemDef.TopValue);
				}
				return this.ReportItemDef.TopForRendering;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.Top = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000C8DC File Offset: 0x0000AADC
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000C93B File Offset: 0x0000AB3B
		public ReportSize Left
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.Left;
				}
				if (this.ReportItemDef.LeftForRendering == null)
				{
					this.ReportItemDef.LeftForRendering = new ReportSize(this.ReportItemDef.Left, this.ReportItemDef.LeftValue);
				}
				return this.ReportItemDef.LeftForRendering;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.Left = value;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000C95C File Offset: 0x0000AB5C
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000C97D File Offset: 0x0000AB7D
		public int ZIndex
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.ZIndex;
				}
				return this.ReportItemDef.ZIndex;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.ZIndex = value;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x0000C9EE File Offset: 0x0000ABEE
		public Style Style
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.m_style;
				}
				Style style = this.m_style;
				if (this.m_style == null)
				{
					style = new Style(this, this.ReportItemDef, this.RenderingContext);
					if (this.UseCache)
					{
						this.m_style = style;
					}
				}
				return style;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_style = value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000CA0C File Offset: 0x0000AC0C
		public string Custom
		{
			get
			{
				if (this.IsCustomControl)
				{
					return null;
				}
				string text = this.ReportItemDef.Custom;
				if (text == null && this.CustomProperties != null)
				{
					CustomProperty customProperty = this.CustomProperties["Custom"];
					if (customProperty != null && customProperty.Value != null)
					{
						text = DataTypeUtility.ConvertToInvariantString(customProperty.Value);
					}
				}
				return text;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000CA64 File Offset: 0x0000AC64
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		public CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.m_customProperties;
				}
				CustomPropertyCollection customPropertyCollection = this.m_customProperties;
				if (this.m_customProperties == null && this.ReportItemDef.CustomProperties != null)
				{
					if (this.ReportItemInstance != null)
					{
						customPropertyCollection = new CustomPropertyCollection(this.ReportItemDef.CustomProperties, this.InstanceInfo.CustomPropertyInstances);
					}
					else
					{
						customPropertyCollection = new CustomPropertyCollection(this.ReportItemDef.CustomProperties, null);
					}
					if (this.UseCache)
					{
						this.m_customProperties = customPropertyCollection;
					}
				}
				return customPropertyCollection;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_customProperties = value;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000CB00 File Offset: 0x0000AD00
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000CB6F File Offset: 0x0000AD6F
		public string ToolTip
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.Tooltip;
				}
				string text = null;
				if (this.ReportItemDef.ToolTip != null)
				{
					if (ExpressionInfo.Types.Constant == this.ReportItemDef.ToolTip.Type)
					{
						text = this.ReportItemDef.ToolTip.Value;
					}
					else if (this.ReportItemInstance == null)
					{
						text = null;
					}
					else
					{
						text = this.InstanceInfo.ToolTip;
					}
				}
				return text;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.Tooltip = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000CB90 File Offset: 0x0000AD90
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x0000CC2A File Offset: 0x0000AE2A
		public virtual bool Hidden
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.Hidden;
				}
				if (this.ReportItemInstance == null)
				{
					return RenderingContext.GetDefinitionHidden(this.ReportItemDef.Visibility);
				}
				if (this.ReportItemDef.Visibility == null)
				{
					return false;
				}
				if (this.RenderingContext != null && this.ReportItemDef.Visibility.Toggle != null)
				{
					return this.RenderingContext.IsItemHidden(this.ReportItemInstance.UniqueName, false);
				}
				return RenderingContext.GetDefinitionHidden(this.ReportItemDef.Visibility) || this.InstanceInfo.StartHidden;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.Hidden = value;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000CC4B File Offset: 0x0000AE4B
		public bool HasToggle
		{
			get
			{
				return !this.IsCustomControl && Visibility.HasToggle(this.ReportItemDef.Visibility);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000CC67 File Offset: 0x0000AE67
		public string ToggleItem
		{
			get
			{
				if (this.IsCustomControl)
				{
					return null;
				}
				if (this.ReportItemDef.Visibility == null)
				{
					return null;
				}
				return this.ReportItemDef.Visibility.Toggle;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000CC92 File Offset: 0x0000AE92
		internal TextBox ToggleParent
		{
			get
			{
				if (!this.HasToggle)
				{
					return null;
				}
				if (this.ReportItemInstance == null)
				{
					return null;
				}
				if (this.RenderingContext == null)
				{
					return null;
				}
				return this.RenderingContext.GetToggleParent(this.ReportItemInstance.UniqueName);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000CCEE File Offset: 0x0000AEEE
		public SharedHiddenState SharedHidden
		{
			get
			{
				if (this.IsCustomControl)
				{
					return this.Processing.SharedHidden;
				}
				return Visibility.GetSharedHidden(this.ReportItemDef.Visibility);
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.SharedHidden = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000CD0F File Offset: 0x0000AF0F
		public bool IsToggleChild
		{
			get
			{
				return !this.IsCustomControl && this.ReportItemInstance != null && this.RenderingContext.IsToggleChild(this.ReportItemInstance.UniqueName);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000CD3B File Offset: 0x0000AF3B
		public bool RepeatedSibling
		{
			get
			{
				return !this.IsCustomControl && this.ReportItemDef.RepeatedSibling;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000CD54 File Offset: 0x0000AF54
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		public virtual object SharedRenderingInfo
		{
			get
			{
				if (this.IsCustomControl)
				{
					return null;
				}
				Global.Tracer.Assert(this.RenderingContext != null);
				return this.RenderingContext.RenderingInfoManager.SharedRenderingInfo[this.ReportItemDef.ID];
			}
			set
			{
				if (this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				Global.Tracer.Assert(this.RenderingContext != null);
				this.RenderingContext.RenderingInfoManager.SharedRenderingInfo[this.ReportItemDef.ID] = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000CE00 File Offset: 0x0000B000
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000CE90 File Offset: 0x0000B090
		public object RenderingInfo
		{
			get
			{
				if (this.IsCustomControl)
				{
					return null;
				}
				Global.Tracer.Assert(this.RenderingContext != null);
				if (this.RenderingContext.InPageSection)
				{
					return this.RenderingContext.RenderingInfoManager.PageSectionRenderingInfo[this.m_uniqueName];
				}
				if (this.m_intUniqueName == 0)
				{
					return null;
				}
				Global.Tracer.Assert(this.m_intUniqueName != 0);
				return this.RenderingContext.RenderingInfoManager.RenderingInfo[this.m_intUniqueName];
			}
			set
			{
				if (this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				Global.Tracer.Assert(this.RenderingContext != null);
				if (this.RenderingContext.InPageSection)
				{
					this.RenderingContext.RenderingInfoManager.PageSectionRenderingInfo[this.m_uniqueName] = value;
					return;
				}
				if (this.m_intUniqueName == 0)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				Global.Tracer.Assert(this.m_intUniqueName != 0);
				this.RenderingContext.RenderingInfoManager.RenderingInfo[this.m_intUniqueName] = value;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000CF34 File Offset: 0x0000B134
		public string DataElementName
		{
			get
			{
				if (this.IsCustomControl)
				{
					return null;
				}
				return this.ReportItemDef.DataElementName;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000CF4B File Offset: 0x0000B14B
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.IsCustomControl)
				{
					return DataElementOutputTypes.NoOutput;
				}
				return this.ReportItemDef.DataElementOutput;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000CF62 File Offset: 0x0000B162
		internal ReportItem ReportItemDef
		{
			get
			{
				return this.Rendering.m_reportItemDef;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000CF6F File Offset: 0x0000B16F
		internal ReportItemInstance ReportItemInstance
		{
			get
			{
				return this.Rendering.m_reportItemInstance;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000CF7C File Offset: 0x0000B17C
		internal ReportItemInstanceInfo InstanceInfo
		{
			get
			{
				if (this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				if (this.Rendering.m_reportItemInstance == null)
				{
					return null;
				}
				if (this.Rendering.m_reportItemInstanceInfo == null)
				{
					this.Rendering.m_reportItemInstanceInfo = this.Rendering.m_reportItemInstance.GetInstanceInfo(this.RenderingContext.ChunkManager, this.RenderingContext.InPageSection);
				}
				return this.Rendering.m_reportItemInstanceInfo;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		internal RenderingContext RenderingContext
		{
			get
			{
				if (this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.Rendering.m_renderingContext;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000D014 File Offset: 0x0000B214
		internal MatrixHeadingInstance HeadingInstance
		{
			get
			{
				if (this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.Rendering.m_headingInstance;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000D034 File Offset: 0x0000B234
		private ReportItemRendering Rendering
		{
			get
			{
				ReportItemRendering reportItemRendering;
				try
				{
					reportItemRendering = (ReportItemRendering)this.m_members;
				}
				catch
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return reportItemRendering;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000D06C File Offset: 0x0000B26C
		internal ReportItemProcessing Processing
		{
			get
			{
				ReportItemProcessing reportItemProcessing;
				try
				{
					reportItemProcessing = (ReportItemProcessing)this.m_members;
				}
				catch
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return reportItemProcessing;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		internal bool UseCache
		{
			get
			{
				if (this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.Rendering.m_renderingContext == null || this.Rendering.m_renderingContext.CacheState;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		protected internal bool IsCustomControl
		{
			get
			{
				return this.m_members.IsCustomControl;
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		protected void DeepClone(ReportItem clone)
		{
			if (clone == null || !this.IsCustomControl)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			if (this.m_uniqueName != null)
			{
				clone.m_uniqueName = string.Copy(this.m_uniqueName);
			}
			clone.m_intUniqueName = this.m_intUniqueName;
			clone.m_canClick = this.m_canClick;
			clone.m_canEdit = this.m_canEdit;
			clone.m_canDrag = this.m_canDrag;
			clone.m_dropTarget = this.m_dropTarget;
			Global.Tracer.Assert(this.m_members is ReportItemProcessing);
			clone.m_members = ((ReportItemProcessing)this.m_members).DeepClone();
			if (this.m_style != null)
			{
				this.m_style.ExtractRenderStyles(out clone.Processing.SharedStyles, out clone.Processing.NonSharedStyles);
			}
			if (this.m_customProperties != null)
			{
				clone.m_customProperties = this.m_customProperties.DeepClone();
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		internal virtual bool Search(SearchContext searchContext)
		{
			return false;
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000D1D3 File Offset: 0x0000B3D3
		internal bool SkipSearch
		{
			get
			{
				return this.SharedHidden == SharedHiddenState.Always || (this.SharedHidden == SharedHiddenState.Sometimes && this.Hidden);
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000D1F4 File Offset: 0x0000B3F4
		internal static int StringToInt(string intAsString)
		{
			int num = -1;
			if (int.TryParse(intAsString, NumberStyles.None, CultureInfo.InvariantCulture, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000D218 File Offset: 0x0000B418
		internal static ReportItem CreateItem(int indexIntoParentCollection, ReportItem reportItemDef, ReportItemInstance reportItemInstance, RenderingContext renderingContext, NonComputedUniqueNames nonComputedUniqueNames)
		{
			string text = null;
			if (renderingContext.InPageSection)
			{
				text = renderingContext.UniqueNamePrefix + "a" + indexIntoParentCollection.ToString(CultureInfo.InvariantCulture);
			}
			return ReportItem.CreateItem(text, reportItemDef, reportItemInstance, renderingContext, nonComputedUniqueNames);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000D258 File Offset: 0x0000B458
		internal static ReportItem CreateItem(string uniqueName, ReportItem reportItemDef, ReportItemInstance reportItemInstance, RenderingContext renderingContext, NonComputedUniqueNames nonComputedUniqueNames)
		{
			if (reportItemDef == null)
			{
				return null;
			}
			Global.Tracer.Assert(renderingContext != null);
			ReportItem reportItem = null;
			int num = 0;
			NonComputedUniqueNames[] array = null;
			if (reportItemInstance != null)
			{
				num = reportItemInstance.UniqueName;
			}
			else if (nonComputedUniqueNames != null)
			{
				num = nonComputedUniqueNames.UniqueName;
				array = nonComputedUniqueNames.ChildrenUniqueNames;
			}
			if (reportItemDef is Line)
			{
				Line line = (Line)reportItemDef;
				LineInstance lineInstance = (LineInstance)reportItemInstance;
				reportItem = new Line(uniqueName, num, line, lineInstance, renderingContext);
			}
			else if (reportItemDef is CheckBox)
			{
				CheckBox checkBox = (CheckBox)reportItemDef;
				CheckBoxInstance checkBoxInstance = (CheckBoxInstance)reportItemInstance;
				reportItem = new CheckBox(uniqueName, num, checkBox, checkBoxInstance, renderingContext);
			}
			else if (reportItemDef is Image)
			{
				Image image = (Image)reportItemDef;
				ImageInstance imageInstance = (ImageInstance)reportItemInstance;
				reportItem = new Image(uniqueName, num, image, imageInstance, renderingContext);
			}
			else if (reportItemDef is TextBox)
			{
				TextBox textBox = (TextBox)reportItemDef;
				TextBoxInstance textBoxInstance = (TextBoxInstance)reportItemInstance;
				reportItem = new TextBox(uniqueName, num, textBox, textBoxInstance, renderingContext);
			}
			else if (reportItemDef is Rectangle)
			{
				Rectangle rectangle = (Rectangle)reportItemDef;
				RectangleInstance rectangleInstance = (RectangleInstance)reportItemInstance;
				reportItem = new Rectangle(uniqueName, num, rectangle, rectangleInstance, renderingContext, array);
			}
			else if (reportItemDef is ActiveXControl)
			{
				ActiveXControl activeXControl = (ActiveXControl)reportItemDef;
				ActiveXControlInstance activeXControlInstance = (ActiveXControlInstance)reportItemInstance;
				reportItem = new ActiveXControl(uniqueName, num, activeXControl, activeXControlInstance, renderingContext);
			}
			else if (reportItemDef is SubReport)
			{
				SubReport subReport = (SubReport)reportItemDef;
				SubReportInstance subReportInstance = (SubReportInstance)reportItemInstance;
				bool flag = false;
				Report report;
				if (SubReport.Status.Retrieved != subReport.RetrievalStatus)
				{
					report = null;
					flag = true;
				}
				else
				{
					if (subReport.ReportContext == null && renderingContext.CurrentReportContext != null)
					{
						subReport.ReportContext = renderingContext.CurrentReportContext.GetSubreportContext(subReport.ReportPath);
					}
					ICatalogItemContext reportContext = subReport.ReportContext;
					RenderingContext renderingContext2 = new RenderingContext(renderingContext, subReport.Uri, subReport.Report.EmbeddedImages, subReport.Report.ImageStreamNames, reportContext);
					if (subReportInstance == null)
					{
						report = new Report(subReport.Report, null, renderingContext2, subReport.ReportName, subReport.Description, null);
					}
					else if (subReportInstance.ReportInstance == null)
					{
						flag = true;
						report = new Report(subReport.Report, null, renderingContext2, subReport.ReportName, subReport.Description, null);
					}
					else
					{
						report = new Report(subReport.Report, subReportInstance.ReportInstance, renderingContext2, subReport.ReportName, subReport.Description, null);
					}
				}
				reportItem = new SubReport(num, subReport, subReportInstance, renderingContext, report, flag);
			}
			else if (reportItemDef is List)
			{
				List list = (List)reportItemDef;
				ListInstance listInstance = (ListInstance)reportItemInstance;
				reportItem = new List(num, list, listInstance, renderingContext);
			}
			else if (reportItemDef is Matrix)
			{
				Matrix matrix = (Matrix)reportItemDef;
				MatrixInstance matrixInstance = (MatrixInstance)reportItemInstance;
				reportItem = new Matrix(num, matrix, matrixInstance, renderingContext);
			}
			else if (reportItemDef is Table)
			{
				Table table = (Table)reportItemDef;
				TableInstance tableInstance = (TableInstance)reportItemInstance;
				reportItem = new Table(num, table, tableInstance, renderingContext);
			}
			else if (reportItemDef is OWCChart)
			{
				OWCChart owcchart = (OWCChart)reportItemDef;
				OWCChartInstance owcchartInstance = (OWCChartInstance)reportItemInstance;
				reportItem = new OWCChart(num, owcchart, owcchartInstance, renderingContext);
			}
			else if (reportItemDef is Chart)
			{
				Chart chart = (Chart)reportItemDef;
				ChartInstance chartInstance = (ChartInstance)reportItemInstance;
				reportItem = new Chart(num, chart, chartInstance, renderingContext);
			}
			else if (reportItemDef is CustomReportItem)
			{
				CustomReportItem customReportItem = (CustomReportItem)reportItemDef;
				CustomReportItemInstance customReportItemInstance = (CustomReportItemInstance)reportItemInstance;
				reportItem = new CustomReportItem(uniqueName, num, customReportItem, customReportItemInstance, renderingContext, array);
				if (!renderingContext.NativeAllCRITypes && (renderingContext.NativeCRITypes == null || !renderingContext.NativeCRITypes.ContainsKey(((CustomReportItem)reportItem).Type)))
				{
					reportItem = ((CustomReportItem)reportItem).AltReportItem;
				}
			}
			return reportItem;
		}

		// Token: 0x040000C4 RID: 196
		private string m_uniqueName;

		// Token: 0x040000C5 RID: 197
		protected int m_intUniqueName;

		// Token: 0x040000C6 RID: 198
		private Style m_style;

		// Token: 0x040000C7 RID: 199
		private CustomPropertyCollection m_customProperties;

		// Token: 0x040000C8 RID: 200
		protected bool m_canClick;

		// Token: 0x040000C9 RID: 201
		protected bool m_canEdit;

		// Token: 0x040000CA RID: 202
		protected bool m_canDrag;

		// Token: 0x040000CB RID: 203
		protected bool m_dropTarget;

		// Token: 0x040000CC RID: 204
		private MemberBase m_members;
	}
}
