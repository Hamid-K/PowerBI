using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003ED RID: 1005
	[Serializable]
	internal class GaugePanelItem : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x06002976 RID: 10614 RVA: 0x000C1D2D File Offset: 0x000BFF2D
		internal GaugePanelItem()
		{
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000C1D35 File Offset: 0x000BFF35
		internal GaugePanelItem(GaugePanel gaugePanel, int id)
			: base(gaugePanel)
		{
			this.m_id = id;
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x06002978 RID: 10616 RVA: 0x000C1D45 File Offset: 0x000BFF45
		// (set) Token: 0x06002979 RID: 10617 RVA: 0x000C1D4D File Offset: 0x000BFF4D
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x000C1D56 File Offset: 0x000BFF56
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x000C1D5E File Offset: 0x000BFF5E
		// (set) Token: 0x0600297C RID: 10620 RVA: 0x000C1D66 File Offset: 0x000BFF66
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return this.m_fieldsUsedInValueExpression;
			}
			set
			{
				this.m_fieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x000C1D6F File Offset: 0x000BFF6F
		// (set) Token: 0x0600297E RID: 10622 RVA: 0x000C1D77 File Offset: 0x000BFF77
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

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x000C1D80 File Offset: 0x000BFF80
		internal int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x000C1D88 File Offset: 0x000BFF88
		// (set) Token: 0x06002981 RID: 10625 RVA: 0x000C1D90 File Offset: 0x000BFF90
		internal ExpressionInfo Top
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

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x000C1D99 File Offset: 0x000BFF99
		// (set) Token: 0x06002983 RID: 10627 RVA: 0x000C1DA1 File Offset: 0x000BFFA1
		internal ExpressionInfo Left
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

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x000C1DAA File Offset: 0x000BFFAA
		// (set) Token: 0x06002985 RID: 10629 RVA: 0x000C1DB2 File Offset: 0x000BFFB2
		internal ExpressionInfo Height
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

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x000C1DBB File Offset: 0x000BFFBB
		// (set) Token: 0x06002987 RID: 10631 RVA: 0x000C1DC3 File Offset: 0x000BFFC3
		internal ExpressionInfo Width
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

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x000C1DCC File Offset: 0x000BFFCC
		// (set) Token: 0x06002989 RID: 10633 RVA: 0x000C1DD4 File Offset: 0x000BFFD4
		internal ExpressionInfo ZIndex
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

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x000C1DDD File Offset: 0x000BFFDD
		// (set) Token: 0x0600298B RID: 10635 RVA: 0x000C1DE5 File Offset: 0x000BFFE5
		internal ExpressionInfo Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x000C1DEE File Offset: 0x000BFFEE
		// (set) Token: 0x0600298D RID: 10637 RVA: 0x000C1DF6 File Offset: 0x000BFFF6
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

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x000C1DFF File Offset: 0x000BFFFF
		// (set) Token: 0x0600298F RID: 10639 RVA: 0x000C1E07 File Offset: 0x000C0007
		internal string ParentItem
		{
			get
			{
				return this.m_parentItem;
			}
			set
			{
				this.m_parentItem = value;
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x000C1E10 File Offset: 0x000C0010
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x06002991 RID: 10641 RVA: 0x000C1E1D File Offset: 0x000C001D
		internal GaugePanelItemExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x000C1E25 File Offset: 0x000C0025
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000C1E30 File Offset: 0x000C0030
		internal override void Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_top != null)
			{
				this.m_top.Initialize("Top", context);
				context.ExprHostBuilder.GaugePanelItemTop(this.m_top);
			}
			if (this.m_left != null)
			{
				this.m_left.Initialize("Left", context);
				context.ExprHostBuilder.GaugePanelItemLeft(this.m_left);
			}
			if (this.m_height != null)
			{
				this.m_height.Initialize("Height", context);
				context.ExprHostBuilder.GaugePanelItemHeight(this.m_height);
			}
			if (this.m_width != null)
			{
				this.m_width.Initialize("Width", context);
				context.ExprHostBuilder.GaugePanelItemWidth(this.m_width);
			}
			if (this.m_zIndex != null)
			{
				this.m_zIndex.Initialize("ZIndex", context);
				context.ExprHostBuilder.GaugePanelItemZIndex(this.m_zIndex);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.GaugePanelItemHidden(this.m_hidden);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.GaugePanelItemToolTip(this.m_toolTip);
			}
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000C1F88 File Offset: 0x000C0188
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			GaugePanelItem gaugePanelItem = (GaugePanelItem)base.PublishClone(context);
			if (this.m_action != null)
			{
				gaugePanelItem.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_top != null)
			{
				gaugePanelItem.m_top = (ExpressionInfo)this.m_top.PublishClone(context);
			}
			if (this.m_left != null)
			{
				gaugePanelItem.m_left = (ExpressionInfo)this.m_left.PublishClone(context);
			}
			if (this.m_height != null)
			{
				gaugePanelItem.m_height = (ExpressionInfo)this.m_height.PublishClone(context);
			}
			if (this.m_width != null)
			{
				gaugePanelItem.m_width = (ExpressionInfo)this.m_width.PublishClone(context);
			}
			if (this.m_zIndex != null)
			{
				gaugePanelItem.m_zIndex = (ExpressionInfo)this.m_zIndex.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				gaugePanelItem.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				gaugePanelItem.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			return gaugePanelItem;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000C209C File Offset: 0x000C029C
		internal void SetExprHost(GaugePanelItemExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_action != null && exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(exprHost.ActionInfoHost, reportObjectModel);
			}
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000C20F0 File Offset: 0x000C02F0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.Top, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Left, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Height, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Width, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ZIndex, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ParentItem, Token.String),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ID, Token.Int32)
			});
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000C2208 File Offset: 0x000C0408
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(GaugePanelItem.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ToolTip)
				{
					if (memberName <= MemberName.Name)
					{
						if (memberName == MemberName.ID)
						{
							writer.Write(this.m_id);
							continue;
						}
						if (memberName == MemberName.Name)
						{
							writer.Write(this.m_name);
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.Top:
							writer.Write(this.m_top);
							continue;
						case MemberName.TopValue:
						case MemberName.LeftValue:
						case MemberName.HeightValue:
						case MemberName.WidthValue:
							break;
						case MemberName.Left:
							writer.Write(this.m_left);
							continue;
						case MemberName.Height:
							writer.Write(this.m_height);
							continue;
						case MemberName.Width:
							writer.Write(this.m_width);
							continue;
						case MemberName.ZIndex:
							writer.Write(this.m_zIndex);
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
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					if (memberName == MemberName.Hidden)
					{
						writer.Write(this.m_hidden);
						continue;
					}
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
					if (memberName == MemberName.ParentItem)
					{
						writer.Write(this.m_parentItem);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000C23AC File Offset: 0x000C05AC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(GaugePanelItem.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ToolTip)
				{
					if (memberName <= MemberName.Name)
					{
						if (memberName == MemberName.ID)
						{
							this.m_id = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.Name)
						{
							this.m_name = reader.ReadString();
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.Top:
							this.m_top = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.TopValue:
						case MemberName.LeftValue:
						case MemberName.HeightValue:
						case MemberName.WidthValue:
							break;
						case MemberName.Left:
							this.m_left = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Height:
							this.m_height = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Width:
							this.m_width = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.ZIndex:
							this.m_zIndex = (ExpressionInfo)reader.ReadRIFObject();
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
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					if (memberName == MemberName.Hidden)
					{
						this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ParentItem)
					{
						this.m_parentItem = reader.ReadString();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000C257B File Offset: 0x000C077B
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			if (this.m_id == 0)
			{
				this.m_id = this.m_gaugePanel.GenerateActionOwnerID();
			}
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000C259E File Offset: 0x000C079E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelItem;
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000C25A5 File Offset: 0x000C07A5
		internal double EvaluateTop(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePanelItemTopExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000C25CB File Offset: 0x000C07CB
		internal double EvaluateLeft(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePanelItemLeftExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000C25F1 File Offset: 0x000C07F1
		internal double EvaluateHeight(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePanelItemHeightExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000C2617 File Offset: 0x000C0817
		internal double EvaluateWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePanelItemWidthExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000C263D File Offset: 0x000C083D
		internal int EvaluateZIndex(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePanelItemZIndexExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000C2663 File Offset: 0x000C0863
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePanelItemHiddenExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000C2689 File Offset: 0x000C0889
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePanelItemToolTipExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x04001707 RID: 5895
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001708 RID: 5896
		protected int m_exprHostID;

		// Token: 0x04001709 RID: 5897
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x0400170A RID: 5898
		[NonSerialized]
		protected GaugePanelItemExprHost m_exprHost;

		// Token: 0x0400170B RID: 5899
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugePanelItem.GetDeclaration();

		// Token: 0x0400170C RID: 5900
		protected string m_name;

		// Token: 0x0400170D RID: 5901
		private ExpressionInfo m_top;

		// Token: 0x0400170E RID: 5902
		private ExpressionInfo m_left;

		// Token: 0x0400170F RID: 5903
		private ExpressionInfo m_height;

		// Token: 0x04001710 RID: 5904
		private ExpressionInfo m_width;

		// Token: 0x04001711 RID: 5905
		private ExpressionInfo m_zIndex;

		// Token: 0x04001712 RID: 5906
		private ExpressionInfo m_hidden;

		// Token: 0x04001713 RID: 5907
		private ExpressionInfo m_toolTip;

		// Token: 0x04001714 RID: 5908
		private string m_parentItem;

		// Token: 0x04001715 RID: 5909
		private int m_id;
	}
}
