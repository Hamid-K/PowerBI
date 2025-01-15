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
	// Token: 0x020003FD RID: 1021
	[Serializable]
	internal sealed class ScaleRange : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x06002B84 RID: 11140 RVA: 0x000C94AB File Offset: 0x000C76AB
		internal ScaleRange()
		{
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000C94B3 File Offset: 0x000C76B3
		internal ScaleRange(GaugePanel gaugePanel, int id)
			: base(gaugePanel)
		{
			this.m_id = id;
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x000C94C3 File Offset: 0x000C76C3
		// (set) Token: 0x06002B87 RID: 11143 RVA: 0x000C94CB File Offset: 0x000C76CB
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

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x000C94D4 File Offset: 0x000C76D4
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x000C94DC File Offset: 0x000C76DC
		// (set) Token: 0x06002B8A RID: 11146 RVA: 0x000C94E4 File Offset: 0x000C76E4
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

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x000C94ED File Offset: 0x000C76ED
		// (set) Token: 0x06002B8C RID: 11148 RVA: 0x000C94F5 File Offset: 0x000C76F5
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

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x000C94FE File Offset: 0x000C76FE
		internal int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x000C9506 File Offset: 0x000C7706
		// (set) Token: 0x06002B8F RID: 11151 RVA: 0x000C950E File Offset: 0x000C770E
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

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x000C9517 File Offset: 0x000C7717
		// (set) Token: 0x06002B91 RID: 11153 RVA: 0x000C951F File Offset: 0x000C771F
		internal GaugeInputValue StartValue
		{
			get
			{
				return this.m_startValue;
			}
			set
			{
				this.m_startValue = value;
			}
		}

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000C9528 File Offset: 0x000C7728
		// (set) Token: 0x06002B93 RID: 11155 RVA: 0x000C9530 File Offset: 0x000C7730
		internal GaugeInputValue EndValue
		{
			get
			{
				return this.m_endValue;
			}
			set
			{
				this.m_endValue = value;
			}
		}

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x000C9539 File Offset: 0x000C7739
		// (set) Token: 0x06002B95 RID: 11157 RVA: 0x000C9541 File Offset: 0x000C7741
		internal ExpressionInfo StartWidth
		{
			get
			{
				return this.m_startWidth;
			}
			set
			{
				this.m_startWidth = value;
			}
		}

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x06002B96 RID: 11158 RVA: 0x000C954A File Offset: 0x000C774A
		// (set) Token: 0x06002B97 RID: 11159 RVA: 0x000C9552 File Offset: 0x000C7752
		internal ExpressionInfo EndWidth
		{
			get
			{
				return this.m_endWidth;
			}
			set
			{
				this.m_endWidth = value;
			}
		}

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000C955B File Offset: 0x000C775B
		// (set) Token: 0x06002B99 RID: 11161 RVA: 0x000C9563 File Offset: 0x000C7763
		internal ExpressionInfo InRangeBarPointerColor
		{
			get
			{
				return this.m_inRangeBarPointerColor;
			}
			set
			{
				this.m_inRangeBarPointerColor = value;
			}
		}

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x06002B9A RID: 11162 RVA: 0x000C956C File Offset: 0x000C776C
		// (set) Token: 0x06002B9B RID: 11163 RVA: 0x000C9574 File Offset: 0x000C7774
		internal ExpressionInfo InRangeLabelColor
		{
			get
			{
				return this.m_inRangeLabelColor;
			}
			set
			{
				this.m_inRangeLabelColor = value;
			}
		}

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x000C957D File Offset: 0x000C777D
		// (set) Token: 0x06002B9D RID: 11165 RVA: 0x000C9585 File Offset: 0x000C7785
		internal ExpressionInfo InRangeTickMarksColor
		{
			get
			{
				return this.m_inRangeTickMarksColor;
			}
			set
			{
				this.m_inRangeTickMarksColor = value;
			}
		}

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x000C958E File Offset: 0x000C778E
		// (set) Token: 0x06002B9F RID: 11167 RVA: 0x000C9596 File Offset: 0x000C7796
		internal ExpressionInfo BackgroundGradientType
		{
			get
			{
				return this.m_backgroundGradientType;
			}
			set
			{
				this.m_backgroundGradientType = value;
			}
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x000C959F File Offset: 0x000C779F
		// (set) Token: 0x06002BA1 RID: 11169 RVA: 0x000C95A7 File Offset: 0x000C77A7
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

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x000C95B0 File Offset: 0x000C77B0
		// (set) Token: 0x06002BA3 RID: 11171 RVA: 0x000C95B8 File Offset: 0x000C77B8
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

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x000C95C1 File Offset: 0x000C77C1
		// (set) Token: 0x06002BA5 RID: 11173 RVA: 0x000C95C9 File Offset: 0x000C77C9
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

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x000C95D2 File Offset: 0x000C77D2
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x000C95DF File Offset: 0x000C77DF
		internal ScaleRangeExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000C95E7 File Offset: 0x000C77E7
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000C95F0 File Offset: 0x000C77F0
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ScaleRangeStart(this.m_name);
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_distanceFromScale != null)
			{
				this.m_distanceFromScale.Initialize("DistanceFromScale", context);
				context.ExprHostBuilder.ScaleRangeDistanceFromScale(this.m_distanceFromScale);
			}
			if (this.m_startWidth != null)
			{
				this.m_startWidth.Initialize("StartWidth", context);
				context.ExprHostBuilder.ScaleRangeStartWidth(this.m_startWidth);
			}
			if (this.m_endWidth != null)
			{
				this.m_endWidth.Initialize("EndWidth", context);
				context.ExprHostBuilder.ScaleRangeEndWidth(this.m_endWidth);
			}
			if (this.m_inRangeBarPointerColor != null)
			{
				this.m_inRangeBarPointerColor.Initialize("InRangeBarPointerColor", context);
				context.ExprHostBuilder.ScaleRangeInRangeBarPointerColor(this.m_inRangeBarPointerColor);
			}
			if (this.m_inRangeLabelColor != null)
			{
				this.m_inRangeLabelColor.Initialize("InRangeLabelColor", context);
				context.ExprHostBuilder.ScaleRangeInRangeLabelColor(this.m_inRangeLabelColor);
			}
			if (this.m_inRangeTickMarksColor != null)
			{
				this.m_inRangeTickMarksColor.Initialize("InRangeTickMarksColor", context);
				context.ExprHostBuilder.ScaleRangeInRangeTickMarksColor(this.m_inRangeTickMarksColor);
			}
			if (this.m_backgroundGradientType != null)
			{
				this.m_backgroundGradientType.Initialize("BackgroundGradientType", context);
				context.ExprHostBuilder.ScaleRangeBackgroundGradientType(this.m_backgroundGradientType);
			}
			if (this.m_placement != null)
			{
				this.m_placement.Initialize("Placement", context);
				context.ExprHostBuilder.ScaleRangePlacement(this.m_placement);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.ScaleRangeToolTip(this.m_toolTip);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.ScaleRangeHidden(this.m_hidden);
			}
			this.m_exprHostID = context.ExprHostBuilder.ScaleRangeEnd();
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000C97EC File Offset: 0x000C79EC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ScaleRange scaleRange = (ScaleRange)base.PublishClone(context);
			if (this.m_action != null)
			{
				scaleRange.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_distanceFromScale != null)
			{
				scaleRange.m_distanceFromScale = (ExpressionInfo)this.m_distanceFromScale.PublishClone(context);
			}
			if (this.m_startValue != null)
			{
				scaleRange.m_startValue = (GaugeInputValue)this.m_startValue.PublishClone(context);
			}
			if (this.m_endValue != null)
			{
				scaleRange.m_endValue = (GaugeInputValue)this.m_endValue.PublishClone(context);
			}
			if (this.m_startWidth != null)
			{
				scaleRange.m_startWidth = (ExpressionInfo)this.m_startWidth.PublishClone(context);
			}
			if (this.m_endWidth != null)
			{
				scaleRange.m_endWidth = (ExpressionInfo)this.m_endWidth.PublishClone(context);
			}
			if (this.m_inRangeBarPointerColor != null)
			{
				scaleRange.m_inRangeBarPointerColor = (ExpressionInfo)this.m_inRangeBarPointerColor.PublishClone(context);
			}
			if (this.m_inRangeLabelColor != null)
			{
				scaleRange.m_inRangeLabelColor = (ExpressionInfo)this.m_inRangeLabelColor.PublishClone(context);
			}
			if (this.m_inRangeTickMarksColor != null)
			{
				scaleRange.m_inRangeTickMarksColor = (ExpressionInfo)this.m_inRangeTickMarksColor.PublishClone(context);
			}
			if (this.m_backgroundGradientType != null)
			{
				scaleRange.m_backgroundGradientType = (ExpressionInfo)this.m_backgroundGradientType.PublishClone(context);
			}
			if (this.m_placement != null)
			{
				scaleRange.m_placement = (ExpressionInfo)this.m_placement.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				scaleRange.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				scaleRange.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			return scaleRange;
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000C999C File Offset: 0x000C7B9C
		internal void SetExprHost(ScaleRangeExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_action != null && exprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(exprHost.ActionInfoHost, reportObjectModel);
			}
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000C99F0 File Offset: 0x000C7BF0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScaleRange, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.DistanceFromScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.StartValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.EndValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeInputValue),
				new MemberInfo(MemberName.StartWidth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.EndWidth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.InRangeBarPointerColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.InRangeLabelColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.InRangeTickMarksColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.BackgroundGradientType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Placement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ID, Token.Int32)
			});
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000C9B5C File Offset: 0x000C7D5C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ScaleRange.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Hidden)
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
						if (memberName == MemberName.ToolTip)
						{
							writer.Write(this.m_toolTip);
							continue;
						}
						if (memberName == MemberName.Hidden)
						{
							writer.Write(this.m_hidden);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.Action)
				{
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DistanceFromScale)
					{
						writer.Write(this.m_distanceFromScale);
						continue;
					}
					if (memberName == MemberName.Placement)
					{
						writer.Write(this.m_placement);
						continue;
					}
					switch (memberName)
					{
					case MemberName.StartValue:
						writer.Write(this.m_startValue);
						continue;
					case MemberName.EndValue:
						writer.Write(this.m_endValue);
						continue;
					case MemberName.StartWidth:
						writer.Write(this.m_startWidth);
						continue;
					case MemberName.EndWidth:
						writer.Write(this.m_endWidth);
						continue;
					case MemberName.InRangeBarPointerColor:
						writer.Write(this.m_inRangeBarPointerColor);
						continue;
					case MemberName.InRangeLabelColor:
						writer.Write(this.m_inRangeLabelColor);
						continue;
					case MemberName.InRangeTickMarksColor:
						writer.Write(this.m_inRangeTickMarksColor);
						continue;
					case MemberName.BackgroundGradientType:
						writer.Write(this.m_backgroundGradientType);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000C9D4C File Offset: 0x000C7F4C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ScaleRange.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Hidden)
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
						if (memberName == MemberName.ToolTip)
						{
							this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.Hidden)
						{
							this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.Action)
				{
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DistanceFromScale)
					{
						this.m_distanceFromScale = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Placement)
					{
						this.m_placement = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.StartValue:
						this.m_startValue = (GaugeInputValue)reader.ReadRIFObject();
						continue;
					case MemberName.EndValue:
						this.m_endValue = (GaugeInputValue)reader.ReadRIFObject();
						continue;
					case MemberName.StartWidth:
						this.m_startWidth = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.EndWidth:
						this.m_endWidth = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.InRangeBarPointerColor:
						this.m_inRangeBarPointerColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.InRangeLabelColor:
						this.m_inRangeLabelColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.InRangeTickMarksColor:
						this.m_inRangeTickMarksColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.BackgroundGradientType:
						this.m_backgroundGradientType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000C9F80 File Offset: 0x000C8180
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			if (this.m_id == 0)
			{
				this.m_id = this.m_gaugePanel.GenerateActionOwnerID();
			}
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000C9FA3 File Offset: 0x000C81A3
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScaleRange;
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000C9FAA File Offset: 0x000C81AA
		internal double EvaluateDistanceFromScale(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleRangeDistanceFromScaleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000C9FD0 File Offset: 0x000C81D0
		internal double EvaluateStartWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleRangeStartWidthExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000C9FF6 File Offset: 0x000C81F6
		internal double EvaluateEndWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleRangeEndWidthExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000CA01C File Offset: 0x000C821C
		internal string EvaluateInRangeBarPointerColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleRangeInRangeBarPointerColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000CA042 File Offset: 0x000C8242
		internal string EvaluateInRangeLabelColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleRangeInRangeLabelColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000CA068 File Offset: 0x000C8268
		internal string EvaluateInRangeTickMarksColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleRangeInRangeTickMarksColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000CA08E File Offset: 0x000C828E
		internal BackgroundGradientTypes EvaluateBackgroundGradientType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateBackgroundGradientTypes(context.ReportRuntime.EvaluateScaleRangeBackgroundGradientTypeExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000CA0BF File Offset: 0x000C82BF
		internal ScaleRangePlacements EvaluatePlacement(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateScaleRangePlacements(context.ReportRuntime.EvaluateScaleRangePlacementExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000CA0F0 File Offset: 0x000C82F0
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleRangeToolTipExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000CA116 File Offset: 0x000C8316
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateScaleRangeHiddenExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040017A2 RID: 6050
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x040017A3 RID: 6051
		private int m_exprHostID;

		// Token: 0x040017A4 RID: 6052
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x040017A5 RID: 6053
		[NonSerialized]
		private ScaleRangeExprHost m_exprHost;

		// Token: 0x040017A6 RID: 6054
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ScaleRange.GetDeclaration();

		// Token: 0x040017A7 RID: 6055
		private string m_name;

		// Token: 0x040017A8 RID: 6056
		private ExpressionInfo m_distanceFromScale;

		// Token: 0x040017A9 RID: 6057
		private GaugeInputValue m_startValue;

		// Token: 0x040017AA RID: 6058
		private GaugeInputValue m_endValue;

		// Token: 0x040017AB RID: 6059
		private ExpressionInfo m_startWidth;

		// Token: 0x040017AC RID: 6060
		private ExpressionInfo m_endWidth;

		// Token: 0x040017AD RID: 6061
		private ExpressionInfo m_inRangeBarPointerColor;

		// Token: 0x040017AE RID: 6062
		private ExpressionInfo m_inRangeLabelColor;

		// Token: 0x040017AF RID: 6063
		private ExpressionInfo m_inRangeTickMarksColor;

		// Token: 0x040017B0 RID: 6064
		private ExpressionInfo m_backgroundGradientType;

		// Token: 0x040017B1 RID: 6065
		private ExpressionInfo m_placement;

		// Token: 0x040017B2 RID: 6066
		private ExpressionInfo m_toolTip;

		// Token: 0x040017B3 RID: 6067
		private ExpressionInfo m_hidden;

		// Token: 0x040017B4 RID: 6068
		private int m_id;
	}
}
