using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000133 RID: 307
	public sealed class ScaleRange : GaugePanelObjectCollectionItem, IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x06000D54 RID: 3412 RVA: 0x00038F8C File Offset: 0x0003718C
		internal ScaleRange(ScaleRange defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00038FA2 File Offset: 0x000371A2
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_gaugePanel, this.m_gaugePanel, this.m_defObject, this.m_gaugePanel.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00038FDC File Offset: 0x000371DC
		public string UniqueName
		{
			get
			{
				return this.m_gaugePanel.GaugePanelDef.UniqueName + "x" + this.m_defObject.ID.ToString();
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00039018 File Offset: 0x00037218
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && this.m_defObject.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_gaugePanel.RenderingContext, this.m_gaugePanel, this.m_defObject.Action, this.m_gaugePanel.GaugePanelDef, this.m_gaugePanel, ObjectType.GaugePanel, this.m_gaugePanel.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00039086 File Offset: 0x00037286
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00039089 File Offset: 0x00037289
		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x00039096 File Offset: 0x00037296
		public ReportDoubleProperty DistanceFromScale
		{
			get
			{
				if (this.m_distanceFromScale == null && this.m_defObject.DistanceFromScale != null)
				{
					this.m_distanceFromScale = new ReportDoubleProperty(this.m_defObject.DistanceFromScale);
				}
				return this.m_distanceFromScale;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x000390C9 File Offset: 0x000372C9
		public GaugeInputValue StartValue
		{
			get
			{
				if (this.m_startValue == null && this.m_defObject.StartValue != null)
				{
					this.m_startValue = new GaugeInputValue(this.m_defObject.StartValue, this.m_gaugePanel);
				}
				return this.m_startValue;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00039102 File Offset: 0x00037302
		public GaugeInputValue EndValue
		{
			get
			{
				if (this.m_endValue == null && this.m_defObject.EndValue != null)
				{
					this.m_endValue = new GaugeInputValue(this.m_defObject.EndValue, this.m_gaugePanel);
				}
				return this.m_endValue;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0003913B File Offset: 0x0003733B
		public ReportDoubleProperty StartWidth
		{
			get
			{
				if (this.m_startWidth == null && this.m_defObject.StartWidth != null)
				{
					this.m_startWidth = new ReportDoubleProperty(this.m_defObject.StartWidth);
				}
				return this.m_startWidth;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0003916E File Offset: 0x0003736E
		public ReportDoubleProperty EndWidth
		{
			get
			{
				if (this.m_endWidth == null && this.m_defObject.EndWidth != null)
				{
					this.m_endWidth = new ReportDoubleProperty(this.m_defObject.EndWidth);
				}
				return this.m_endWidth;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000391A4 File Offset: 0x000373A4
		public ReportColorProperty InRangeBarPointerColor
		{
			get
			{
				if (this.m_inRangeBarPointerColor == null && this.m_defObject.InRangeBarPointerColor != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo inRangeBarPointerColor = this.m_defObject.InRangeBarPointerColor;
					if (inRangeBarPointerColor != null)
					{
						this.m_inRangeBarPointerColor = new ReportColorProperty(inRangeBarPointerColor.IsExpression, inRangeBarPointerColor.OriginalText, inRangeBarPointerColor.IsExpression ? null : new ReportColor(inRangeBarPointerColor.StringValue.Trim(), true), inRangeBarPointerColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_inRangeBarPointerColor;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0003922C File Offset: 0x0003742C
		public ReportColorProperty InRangeLabelColor
		{
			get
			{
				if (this.m_inRangeLabelColor == null && this.m_defObject.InRangeLabelColor != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo inRangeLabelColor = this.m_defObject.InRangeLabelColor;
					if (inRangeLabelColor != null)
					{
						this.m_inRangeLabelColor = new ReportColorProperty(inRangeLabelColor.IsExpression, inRangeLabelColor.OriginalText, inRangeLabelColor.IsExpression ? null : new ReportColor(inRangeLabelColor.StringValue.Trim(), true), inRangeLabelColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_inRangeLabelColor;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x000392B4 File Offset: 0x000374B4
		public ReportColorProperty InRangeTickMarksColor
		{
			get
			{
				if (this.m_inRangeTickMarksColor == null && this.m_defObject.InRangeTickMarksColor != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo inRangeTickMarksColor = this.m_defObject.InRangeTickMarksColor;
					if (inRangeTickMarksColor != null)
					{
						this.m_inRangeTickMarksColor = new ReportColorProperty(inRangeTickMarksColor.IsExpression, inRangeTickMarksColor.OriginalText, inRangeTickMarksColor.IsExpression ? null : new ReportColor(inRangeTickMarksColor.StringValue.Trim(), true), inRangeTickMarksColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_inRangeTickMarksColor;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0003933C File Offset: 0x0003753C
		public ReportEnumProperty<BackgroundGradientTypes> BackgroundGradientType
		{
			get
			{
				if (this.m_backgroundGradientType == null && this.m_defObject.BackgroundGradientType != null)
				{
					this.m_backgroundGradientType = new ReportEnumProperty<BackgroundGradientTypes>(this.m_defObject.BackgroundGradientType.IsExpression, this.m_defObject.BackgroundGradientType.OriginalText, EnumTranslator.TranslateBackgroundGradientTypes(this.m_defObject.BackgroundGradientType.StringValue, null));
				}
				return this.m_backgroundGradientType;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000393A8 File Offset: 0x000375A8
		public ReportEnumProperty<ScaleRangePlacements> Placement
		{
			get
			{
				if (this.m_placement == null && this.m_defObject.Placement != null)
				{
					this.m_placement = new ReportEnumProperty<ScaleRangePlacements>(this.m_defObject.Placement.IsExpression, this.m_defObject.Placement.OriginalText, EnumTranslator.TranslateScaleRangePlacements(this.m_defObject.Placement.StringValue, null));
				}
				return this.m_placement;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x00039411 File Offset: 0x00037611
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && this.m_defObject.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_defObject.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00039444 File Offset: 0x00037644
		public ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_hidden == null && this.m_defObject.Hidden != null)
				{
					this.m_hidden = new ReportBoolProperty(this.m_defObject.Hidden);
				}
				return this.m_hidden;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00039477 File Offset: 0x00037677
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0003947F File Offset: 0x0003767F
		internal ScaleRange ScaleRangeDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00039487 File Offset: 0x00037687
		public ScaleRangeInstance Instance
		{
			get
			{
				if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ScaleRangeInstance(this);
				}
				return (ScaleRangeInstance)this.m_instance;
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000394BC File Offset: 0x000376BC
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
			if (this.m_startValue != null)
			{
				this.m_startValue.SetNewContext();
			}
			if (this.m_endValue != null)
			{
				this.m_endValue.SetNewContext();
			}
		}

		// Token: 0x0400060C RID: 1548
		private GaugePanel m_gaugePanel;

		// Token: 0x0400060D RID: 1549
		private ScaleRange m_defObject;

		// Token: 0x0400060E RID: 1550
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x0400060F RID: 1551
		private ActionInfo m_actionInfo;

		// Token: 0x04000610 RID: 1552
		private ReportDoubleProperty m_distanceFromScale;

		// Token: 0x04000611 RID: 1553
		private GaugeInputValue m_startValue;

		// Token: 0x04000612 RID: 1554
		private GaugeInputValue m_endValue;

		// Token: 0x04000613 RID: 1555
		private ReportDoubleProperty m_startWidth;

		// Token: 0x04000614 RID: 1556
		private ReportDoubleProperty m_endWidth;

		// Token: 0x04000615 RID: 1557
		private ReportColorProperty m_inRangeBarPointerColor;

		// Token: 0x04000616 RID: 1558
		private ReportColorProperty m_inRangeLabelColor;

		// Token: 0x04000617 RID: 1559
		private ReportColorProperty m_inRangeTickMarksColor;

		// Token: 0x04000618 RID: 1560
		private ReportEnumProperty<ScaleRangePlacements> m_placement;

		// Token: 0x04000619 RID: 1561
		private ReportStringProperty m_toolTip;

		// Token: 0x0400061A RID: 1562
		private ReportBoolProperty m_hidden;

		// Token: 0x0400061B RID: 1563
		private ReportEnumProperty<BackgroundGradientTypes> m_backgroundGradientType;
	}
}
