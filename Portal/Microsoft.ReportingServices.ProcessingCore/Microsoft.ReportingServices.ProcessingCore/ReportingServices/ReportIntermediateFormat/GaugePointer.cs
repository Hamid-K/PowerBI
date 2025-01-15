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
	// Token: 0x020003EE RID: 1006
	[Serializable]
	internal class GaugePointer : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x060029A3 RID: 10659 RVA: 0x000C26BB File Offset: 0x000C08BB
		internal GaugePointer()
		{
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000C26C3 File Offset: 0x000C08C3
		internal GaugePointer(GaugePanel gaugePanel, int id)
			: base(gaugePanel)
		{
			this.m_id = id;
		}

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x000C26D3 File Offset: 0x000C08D3
		// (set) Token: 0x060029A6 RID: 10662 RVA: 0x000C26DB File Offset: 0x000C08DB
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

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x060029A7 RID: 10663 RVA: 0x000C26E4 File Offset: 0x000C08E4
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x000C26EC File Offset: 0x000C08EC
		// (set) Token: 0x060029A9 RID: 10665 RVA: 0x000C26F4 File Offset: 0x000C08F4
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

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x000C26FD File Offset: 0x000C08FD
		// (set) Token: 0x060029AB RID: 10667 RVA: 0x000C2705 File Offset: 0x000C0905
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

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x000C270E File Offset: 0x000C090E
		internal int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x000C2716 File Offset: 0x000C0916
		// (set) Token: 0x060029AE RID: 10670 RVA: 0x000C271E File Offset: 0x000C091E
		internal GaugeInputValue GaugeInputValue
		{
			get
			{
				return this.m_gaugeInputValue;
			}
			set
			{
				this.m_gaugeInputValue = value;
			}
		}

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x000C2727 File Offset: 0x000C0927
		// (set) Token: 0x060029B0 RID: 10672 RVA: 0x000C272F File Offset: 0x000C092F
		internal ExpressionInfo BarStart
		{
			get
			{
				return this.m_barStart;
			}
			set
			{
				this.m_barStart = value;
			}
		}

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x000C2738 File Offset: 0x000C0938
		// (set) Token: 0x060029B2 RID: 10674 RVA: 0x000C2740 File Offset: 0x000C0940
		internal ExpressionInfo DistanceFromScale
		{
			get
			{
				return this.m_distanceFromScale;
			}
			set
			{
				this.m_distanceFromScale = value;
			}
		}

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x000C2749 File Offset: 0x000C0949
		// (set) Token: 0x060029B4 RID: 10676 RVA: 0x000C2751 File Offset: 0x000C0951
		internal PointerImage PointerImage
		{
			get
			{
				return this.m_pointerImage;
			}
			set
			{
				this.m_pointerImage = value;
			}
		}

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x000C275A File Offset: 0x000C095A
		// (set) Token: 0x060029B6 RID: 10678 RVA: 0x000C2762 File Offset: 0x000C0962
		internal ExpressionInfo MarkerLength
		{
			get
			{
				return this.m_markerLength;
			}
			set
			{
				this.m_markerLength = value;
			}
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x000C276B File Offset: 0x000C096B
		// (set) Token: 0x060029B8 RID: 10680 RVA: 0x000C2773 File Offset: 0x000C0973
		internal ExpressionInfo MarkerStyle
		{
			get
			{
				return this.m_markerStyle;
			}
			set
			{
				this.m_markerStyle = value;
			}
		}

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x000C277C File Offset: 0x000C097C
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x000C2784 File Offset: 0x000C0984
		internal ExpressionInfo Placement
		{
			get
			{
				return this.m_placement;
			}
			set
			{
				this.m_placement = value;
			}
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x000C278D File Offset: 0x000C098D
		// (set) Token: 0x060029BC RID: 10684 RVA: 0x000C2795 File Offset: 0x000C0995
		internal ExpressionInfo SnappingEnabled
		{
			get
			{
				return this.m_snappingEnabled;
			}
			set
			{
				this.m_snappingEnabled = value;
			}
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x060029BD RID: 10685 RVA: 0x000C279E File Offset: 0x000C099E
		// (set) Token: 0x060029BE RID: 10686 RVA: 0x000C27A6 File Offset: 0x000C09A6
		internal ExpressionInfo SnappingInterval
		{
			get
			{
				return this.m_snappingInterval;
			}
			set
			{
				this.m_snappingInterval = value;
			}
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x060029BF RID: 10687 RVA: 0x000C27AF File Offset: 0x000C09AF
		// (set) Token: 0x060029C0 RID: 10688 RVA: 0x000C27B7 File Offset: 0x000C09B7
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

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x060029C1 RID: 10689 RVA: 0x000C27C0 File Offset: 0x000C09C0
		// (set) Token: 0x060029C2 RID: 10690 RVA: 0x000C27C8 File Offset: 0x000C09C8
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

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x060029C3 RID: 10691 RVA: 0x000C27D1 File Offset: 0x000C09D1
		// (set) Token: 0x060029C4 RID: 10692 RVA: 0x000C27D9 File Offset: 0x000C09D9
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

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x060029C5 RID: 10693 RVA: 0x000C27E2 File Offset: 0x000C09E2
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x060029C6 RID: 10694 RVA: 0x000C27EF File Offset: 0x000C09EF
		internal GaugePointerExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x060029C7 RID: 10695 RVA: 0x000C27F7 File Offset: 0x000C09F7
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000C2800 File Offset: 0x000C0A00
		internal override void Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_barStart != null)
			{
				this.m_barStart.Initialize("BarStart", context);
				context.ExprHostBuilder.GaugePointerBarStart(this.m_barStart);
			}
			if (this.m_distanceFromScale != null)
			{
				this.m_distanceFromScale.Initialize("DistanceFromScale", context);
				context.ExprHostBuilder.GaugePointerDistanceFromScale(this.m_distanceFromScale);
			}
			if (this.m_pointerImage != null)
			{
				this.m_pointerImage.Initialize(context);
			}
			if (this.m_markerLength != null)
			{
				this.m_markerLength.Initialize("MarkerLength", context);
				context.ExprHostBuilder.GaugePointerMarkerLength(this.m_markerLength);
			}
			if (this.m_markerStyle != null)
			{
				this.m_markerStyle.Initialize("MarkerStyle", context);
				context.ExprHostBuilder.GaugePointerMarkerStyle(this.m_markerStyle);
			}
			if (this.m_placement != null)
			{
				this.m_placement.Initialize("Placement", context);
				context.ExprHostBuilder.GaugePointerPlacement(this.m_placement);
			}
			if (this.m_snappingEnabled != null)
			{
				this.m_snappingEnabled.Initialize("SnappingEnabled", context);
				context.ExprHostBuilder.GaugePointerSnappingEnabled(this.m_snappingEnabled);
			}
			if (this.m_snappingInterval != null)
			{
				this.m_snappingInterval.Initialize("SnappingInterval", context);
				context.ExprHostBuilder.GaugePointerSnappingInterval(this.m_snappingInterval);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.GaugePointerToolTip(this.m_toolTip);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.GaugePointerHidden(this.m_hidden);
			}
			if (this.m_width != null)
			{
				this.m_width.Initialize("Width", context);
				context.ExprHostBuilder.GaugePointerWidth(this.m_width);
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000C29EC File Offset: 0x000C0BEC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			GaugePointer gaugePointer = (GaugePointer)base.PublishClone(context);
			if (this.m_action != null)
			{
				gaugePointer.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_gaugeInputValue != null)
			{
				gaugePointer.m_gaugeInputValue = (GaugeInputValue)this.m_gaugeInputValue.PublishClone(context);
			}
			if (this.m_barStart != null)
			{
				gaugePointer.m_barStart = (ExpressionInfo)this.m_barStart.PublishClone(context);
			}
			if (this.m_distanceFromScale != null)
			{
				gaugePointer.m_distanceFromScale = (ExpressionInfo)this.m_distanceFromScale.PublishClone(context);
			}
			if (this.m_pointerImage != null)
			{
				gaugePointer.m_pointerImage = (PointerImage)this.m_pointerImage.PublishClone(context);
			}
			if (this.m_markerLength != null)
			{
				gaugePointer.m_markerLength = (ExpressionInfo)this.m_markerLength.PublishClone(context);
			}
			if (this.m_markerStyle != null)
			{
				gaugePointer.m_markerStyle = (ExpressionInfo)this.m_markerStyle.PublishClone(context);
			}
			if (this.m_placement != null)
			{
				gaugePointer.m_placement = (ExpressionInfo)this.m_placement.PublishClone(context);
			}
			if (this.m_snappingEnabled != null)
			{
				gaugePointer.m_snappingEnabled = (ExpressionInfo)this.m_snappingEnabled.PublishClone(context);
			}
			if (this.m_snappingInterval != null)
			{
				gaugePointer.m_snappingInterval = (ExpressionInfo)this.m_snappingInterval.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				gaugePointer.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				gaugePointer.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_width != null)
			{
				gaugePointer.m_width = (ExpressionInfo)this.m_width.PublishClone(context);
			}
			return gaugePointer;
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000C2B9C File Offset: 0x000C0D9C
		internal void SetExprHost(GaugePointerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_pointerImage != null && this.m_exprHost.PointerImageHost != null)
			{
				this.m_pointerImage.SetExprHost(this.m_exprHost.PointerImageHost, reportObjectModel);
			}
			if (this.m_action != null && exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(exprHost.ActionInfoHost, reportObjectModel);
			}
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x000C2C1C File Offset: 0x000C0E1C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePointer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.GaugeInputValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.BarStart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DistanceFromScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.PointerImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PointerImage),
				new MemberInfo(MemberName.MarkerLength, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MarkerStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Placement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SnappingEnabled, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SnappingInterval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Width, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ID, Token.Int32)
			});
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x000C2D88 File Offset: 0x000C0F88
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(GaugePointer.m_Declaration);
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
						if (memberName == MemberName.Width)
						{
							writer.Write(this.m_width);
							continue;
						}
						if (memberName == MemberName.ToolTip)
						{
							writer.Write(this.m_toolTip);
							continue;
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
					switch (memberName)
					{
					case MemberName.GaugeInputValue:
						writer.Write(this.m_gaugeInputValue);
						continue;
					case MemberName.BarStart:
						writer.Write(this.m_barStart);
						continue;
					case MemberName.DistanceFromScale:
						writer.Write(this.m_distanceFromScale);
						continue;
					case MemberName.PointerImage:
						writer.Write(this.m_pointerImage);
						continue;
					case MemberName.MarkerLength:
						writer.Write(this.m_markerLength);
						continue;
					case MemberName.MarkerStyle:
						writer.Write(this.m_markerStyle);
						continue;
					case MemberName.SnappingEnabled:
						writer.Write(this.m_snappingEnabled);
						continue;
					case MemberName.SnappingInterval:
						writer.Write(this.m_snappingInterval);
						continue;
					default:
						if (memberName == MemberName.Placement)
						{
							writer.Write(this.m_placement);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000C2F7C File Offset: 0x000C117C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(GaugePointer.m_Declaration);
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
						if (memberName == MemberName.Width)
						{
							this.m_width = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.ToolTip)
						{
							this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
							continue;
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
					switch (memberName)
					{
					case MemberName.GaugeInputValue:
						this.m_gaugeInputValue = (GaugeInputValue)reader.ReadRIFObject();
						continue;
					case MemberName.BarStart:
						this.m_barStart = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DistanceFromScale:
						this.m_distanceFromScale = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.PointerImage:
						this.m_pointerImage = (PointerImage)reader.ReadRIFObject();
						continue;
					case MemberName.MarkerLength:
						this.m_markerLength = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.MarkerStyle:
						this.m_markerStyle = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.SnappingEnabled:
						this.m_snappingEnabled = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.SnappingInterval:
						this.m_snappingInterval = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.Placement)
						{
							this.m_placement = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000C31B3 File Offset: 0x000C13B3
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			if (this.m_id == 0)
			{
				this.m_id = this.m_gaugePanel.GenerateActionOwnerID();
			}
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000C31D6 File Offset: 0x000C13D6
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePointer;
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000C31DD File Offset: 0x000C13DD
		internal GaugeBarStarts EvaluateBarStart(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeBarStarts(context.ReportRuntime.EvaluateGaugePointerBarStartExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000C320E File Offset: 0x000C140E
		internal double EvaluateDistanceFromScale(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePointerDistanceFromScaleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x000C3234 File Offset: 0x000C1434
		internal double EvaluateMarkerLength(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePointerMarkerLengthExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x000C325A File Offset: 0x000C145A
		internal GaugeMarkerStyles EvaluateMarkerStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeMarkerStyles(context.ReportRuntime.EvaluateGaugePointerMarkerStyleExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x000C328B File Offset: 0x000C148B
		internal GaugePointerPlacements EvaluatePlacement(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugePointerPlacements(context.ReportRuntime.EvaluateGaugePointerPlacementExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x000C32BC File Offset: 0x000C14BC
		internal bool EvaluateSnappingEnabled(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePointerSnappingEnabledExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000C32E2 File Offset: 0x000C14E2
		internal double EvaluateSnappingInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePointerSnappingIntervalExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000C3308 File Offset: 0x000C1508
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePointerToolTipExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000C332E File Offset: 0x000C152E
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePointerHiddenExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000C3354 File Offset: 0x000C1554
		internal double EvaluateWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugePointerWidthExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x04001716 RID: 5910
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001717 RID: 5911
		protected int m_exprHostID;

		// Token: 0x04001718 RID: 5912
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x04001719 RID: 5913
		[NonSerialized]
		protected GaugePointerExprHost m_exprHost;

		// Token: 0x0400171A RID: 5914
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugePointer.GetDeclaration();

		// Token: 0x0400171B RID: 5915
		protected string m_name;

		// Token: 0x0400171C RID: 5916
		private GaugeInputValue m_gaugeInputValue;

		// Token: 0x0400171D RID: 5917
		private ExpressionInfo m_barStart;

		// Token: 0x0400171E RID: 5918
		private ExpressionInfo m_distanceFromScale;

		// Token: 0x0400171F RID: 5919
		private PointerImage m_pointerImage;

		// Token: 0x04001720 RID: 5920
		private ExpressionInfo m_markerLength;

		// Token: 0x04001721 RID: 5921
		private ExpressionInfo m_markerStyle;

		// Token: 0x04001722 RID: 5922
		private ExpressionInfo m_placement;

		// Token: 0x04001723 RID: 5923
		private ExpressionInfo m_snappingEnabled;

		// Token: 0x04001724 RID: 5924
		private ExpressionInfo m_snappingInterval;

		// Token: 0x04001725 RID: 5925
		private ExpressionInfo m_toolTip;

		// Token: 0x04001726 RID: 5926
		private ExpressionInfo m_hidden;

		// Token: 0x04001727 RID: 5927
		private ExpressionInfo m_width;

		// Token: 0x04001728 RID: 5928
		private int m_id;
	}
}
