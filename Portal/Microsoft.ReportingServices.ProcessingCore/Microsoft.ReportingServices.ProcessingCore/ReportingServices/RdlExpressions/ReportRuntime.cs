using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;
using Microsoft.SqlServer.Types;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000565 RID: 1381
	internal sealed class ReportRuntime : IErrorContext, IStaticReferenceable
	{
		// Token: 0x06004DCE RID: 19918 RVA: 0x0013EDC4 File Offset: 0x0013CFC4
		internal string EvaluateBaseGaugeImageSourceExpression(Microsoft.ReportingServices.ReportIntermediateFormat.BaseGaugeImage baseGaugeImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(baseGaugeImage.Source, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Source", out variantResult))
			{
				this.EvaluateComplexExpression(baseGaugeImage.Source, ref variantResult, baseGaugeImage.ExprHost, () => baseGaugeImage.ExprHost.SourceExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x0013EE34 File Offset: 0x0013D034
		internal string EvaluateBaseGaugeImageStringValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.BaseGaugeImage baseGaugeImage, string objectName, out bool errorOccurred)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(baseGaugeImage.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(baseGaugeImage.Value, ref variantResult, baseGaugeImage.ExprHost, () => baseGaugeImage.ExprHost.ValueExpr);
			}
			StringResult stringResult = this.ProcessStringResult(variantResult);
			errorOccurred = stringResult.ErrorOccurred;
			return stringResult.Value;
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x0013EEAC File Offset: 0x0013D0AC
		internal byte[] EvaluateBaseGaugeImageBinaryValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.BaseGaugeImage baseGaugeImage, string objectName, out bool errorOccurred)
		{
			VariantResult variantResult;
			if (!this.EvaluateBinaryExpression(baseGaugeImage.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(baseGaugeImage.Value, ref variantResult, baseGaugeImage.ExprHost, () => baseGaugeImage.ExprHost.ValueExpr);
			}
			BinaryResult binaryResult = this.ProcessBinaryResult(variantResult);
			errorOccurred = binaryResult.ErrorOccurred;
			return binaryResult.Value;
		}

		// Token: 0x06004DD1 RID: 19921 RVA: 0x0013EF24 File Offset: 0x0013D124
		internal string EvaluateBaseGaugeImageMIMETypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.BaseGaugeImage baseGaugeImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(baseGaugeImage.MIMEType, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "MIMEType", out variantResult))
			{
				this.EvaluateComplexExpression(baseGaugeImage.MIMEType, ref variantResult, baseGaugeImage.ExprHost, () => baseGaugeImage.ExprHost.MIMETypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x0013EF94 File Offset: 0x0013D194
		internal string EvaluateBaseGaugeImageTransparentColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.BaseGaugeImage baseGaugeImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(baseGaugeImage.TransparentColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "TransparentColor", out variantResult))
			{
				this.EvaluateComplexExpression(baseGaugeImage.TransparentColor, ref variantResult, baseGaugeImage.ExprHost, () => baseGaugeImage.ExprHost.TransparentColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x0013F008 File Offset: 0x0013D208
		internal string EvaluateGaugeImageSourceExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeImage gaugeImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeImage.Source, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Source", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeImage.Source, ref variantResult, gaugeImage.ExprHost, () => ((GaugeImageExprHost)gaugeImage.ExprHost).SourceExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x0013F078 File Offset: 0x0013D278
		internal VariantResult EvaluateGaugeImageValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeImage gaugeImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeImage.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeImage.Value, ref variantResult, gaugeImage.ExprHost, () => ((GaugeImageExprHost)gaugeImage.ExprHost).ValueExpr);
			}
			this.ProcessVariantResult(gaugeImage.Value, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004DD5 RID: 19925 RVA: 0x0013F0F0 File Offset: 0x0013D2F0
		internal string EvaluateGaugeImageTransparentColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeImage gaugeImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeImage.TransparentColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "TransparentColor", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeImage.TransparentColor, ref variantResult, gaugeImage.ExprHost, () => ((GaugeImageExprHost)gaugeImage.ExprHost).TransparentColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004DD6 RID: 19926 RVA: 0x0013F164 File Offset: 0x0013D364
		internal string EvaluateCapImageHueColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CapImage capImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(capImage.HueColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "HueColor", out variantResult))
			{
				this.EvaluateComplexExpression(capImage.HueColor, ref variantResult, capImage.ExprHost, () => ((CapImageExprHost)capImage.ExprHost).HueColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x0013F1D8 File Offset: 0x0013D3D8
		internal string EvaluateCapImageOffsetXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CapImage capImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(capImage.OffsetX, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "OffsetX", out variantResult))
			{
				this.EvaluateComplexExpression(capImage.OffsetX, ref variantResult, capImage.ExprHost, () => ((CapImageExprHost)capImage.ExprHost).OffsetXExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x0013F24C File Offset: 0x0013D44C
		internal string EvaluateCapImageOffsetYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CapImage capImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(capImage.OffsetY, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "OffsetY", out variantResult))
			{
				this.EvaluateComplexExpression(capImage.OffsetY, ref variantResult, capImage.ExprHost, () => ((CapImageExprHost)capImage.ExprHost).OffsetYExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x0013F2C0 File Offset: 0x0013D4C0
		internal string EvaluateFrameImageHueColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.FrameImage frameImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(frameImage.HueColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "HueColor", out variantResult))
			{
				this.EvaluateComplexExpression(frameImage.HueColor, ref variantResult, frameImage.ExprHost, () => ((FrameImageExprHost)frameImage.ExprHost).HueColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x0013F334 File Offset: 0x0013D534
		internal double EvaluateFrameImageTransparencyExpression(Microsoft.ReportingServices.ReportIntermediateFormat.FrameImage frameImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(frameImage.Transparency, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Transparency", out variantResult))
			{
				this.EvaluateComplexExpression(frameImage.Transparency, ref variantResult, frameImage.ExprHost, () => ((FrameImageExprHost)frameImage.ExprHost).TransparencyExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x0013F3A4 File Offset: 0x0013D5A4
		internal bool EvaluateFrameImageClipImageExpression(Microsoft.ReportingServices.ReportIntermediateFormat.FrameImage frameImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(frameImage.ClipImage, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ClipImage", out variantResult))
			{
				this.EvaluateComplexExpression(frameImage.ClipImage, ref variantResult, frameImage.ExprHost, () => ((FrameImageExprHost)frameImage.ExprHost).ClipImageExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x0013F414 File Offset: 0x0013D614
		internal string EvaluateTopImageHueColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TopImage topImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(topImage.HueColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "HueColor", out variantResult))
			{
				this.EvaluateComplexExpression(topImage.HueColor, ref variantResult, topImage.ExprHost, () => ((TopImageExprHost)topImage.ExprHost).HueColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x0013F488 File Offset: 0x0013D688
		internal string EvaluateBackFrameFrameStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.BackFrame backFrame, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(backFrame.FrameStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "FrameStyle", out variantResult))
			{
				this.EvaluateComplexExpression(backFrame.FrameStyle, ref variantResult, backFrame.ExprHost, () => backFrame.ExprHost.FrameStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x0013F4F8 File Offset: 0x0013D6F8
		internal string EvaluateBackFrameFrameShapeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.BackFrame backFrame, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(backFrame.FrameShape, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "FrameShape", out variantResult))
			{
				this.EvaluateComplexExpression(backFrame.FrameShape, ref variantResult, backFrame.ExprHost, () => backFrame.ExprHost.FrameShapeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x0013F568 File Offset: 0x0013D768
		internal double EvaluateBackFrameFrameWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.BackFrame backFrame, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(backFrame.FrameWidth, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "FrameWidth", out variantResult))
			{
				this.EvaluateComplexExpression(backFrame.FrameWidth, ref variantResult, backFrame.ExprHost, () => backFrame.ExprHost.FrameWidthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x0013F5D8 File Offset: 0x0013D7D8
		internal string EvaluateBackFrameGlassEffectExpression(Microsoft.ReportingServices.ReportIntermediateFormat.BackFrame backFrame, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(backFrame.GlassEffect, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "GlassEffect", out variantResult))
			{
				this.EvaluateComplexExpression(backFrame.GlassEffect, ref variantResult, backFrame.ExprHost, () => backFrame.ExprHost.GlassEffectExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x0013F648 File Offset: 0x0013D848
		internal string EvaluateGaugePanelAntiAliasingExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanel gaugePanel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePanel.AntiAliasing, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "AntiAliasing", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePanel.AntiAliasing, ref variantResult, gaugePanel.GaugePanelExprHost, () => gaugePanel.GaugePanelExprHost.AntiAliasingExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x0013F6B8 File Offset: 0x0013D8B8
		internal bool EvaluateGaugePanelAutoLayoutExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanel gaugePanel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePanel.AutoLayout, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "AutoLayout", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePanel.AutoLayout, ref variantResult, gaugePanel.GaugePanelExprHost, () => gaugePanel.GaugePanelExprHost.AutoLayoutExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x0013F728 File Offset: 0x0013D928
		internal double EvaluateGaugePanelShadowIntensityExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanel gaugePanel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePanel.ShadowIntensity, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ShadowIntensity", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePanel.ShadowIntensity, ref variantResult, gaugePanel.GaugePanelExprHost, () => gaugePanel.GaugePanelExprHost.ShadowIntensityExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x0013F798 File Offset: 0x0013D998
		internal string EvaluateGaugePanelTextAntiAliasingQualityExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanel gaugePanel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePanel.TextAntiAliasingQuality, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "TextAntiAliasingQuality", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePanel.TextAntiAliasingQuality, ref variantResult, gaugePanel.GaugePanelExprHost, () => gaugePanel.GaugePanelExprHost.TextAntiAliasingQualityExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x0013F808 File Offset: 0x0013DA08
		internal double EvaluateGaugePanelItemTopExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanelItem gaugePanelItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePanelItem.Top, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Top", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePanelItem.Top, ref variantResult, gaugePanelItem.ExprHost, () => gaugePanelItem.ExprHost.TopExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x0013F878 File Offset: 0x0013DA78
		internal double EvaluateGaugePanelItemLeftExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanelItem gaugePanelItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePanelItem.Left, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Left", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePanelItem.Left, ref variantResult, gaugePanelItem.ExprHost, () => gaugePanelItem.ExprHost.LeftExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x0013F8E8 File Offset: 0x0013DAE8
		internal double EvaluateGaugePanelItemHeightExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanelItem gaugePanelItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePanelItem.Height, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Height", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePanelItem.Height, ref variantResult, gaugePanelItem.ExprHost, () => gaugePanelItem.ExprHost.HeightExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x0013F958 File Offset: 0x0013DB58
		internal double EvaluateGaugePanelItemWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanelItem gaugePanelItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePanelItem.Width, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Width", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePanelItem.Width, ref variantResult, gaugePanelItem.ExprHost, () => gaugePanelItem.ExprHost.WidthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x0013F9C8 File Offset: 0x0013DBC8
		internal int EvaluateGaugePanelItemZIndexExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanelItem gaugePanelItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePanelItem.ZIndex, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ZIndex", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePanelItem.ZIndex, ref variantResult, gaugePanelItem.ExprHost, () => gaugePanelItem.ExprHost.ZIndexExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x0013FA38 File Offset: 0x0013DC38
		internal bool EvaluateGaugePanelItemHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanelItem gaugePanelItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePanelItem.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePanelItem.Hidden, ref variantResult, gaugePanelItem.ExprHost, () => gaugePanelItem.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x0013FAA8 File Offset: 0x0013DCA8
		internal string EvaluateGaugePanelItemToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePanelItem gaugePanelItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePanelItem.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePanelItem.ToolTip, ref variantResult, gaugePanelItem.ExprHost, () => gaugePanelItem.ExprHost.ToolTipExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DEC RID: 19948 RVA: 0x0013FB18 File Offset: 0x0013DD18
		internal string EvaluateGaugePointerBarStartExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePointer gaugePointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePointer.BarStart, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "BarStart", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePointer.BarStart, ref variantResult, gaugePointer.ExprHost, () => gaugePointer.ExprHost.BarStartExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x0013FB88 File Offset: 0x0013DD88
		internal double EvaluateGaugePointerDistanceFromScaleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePointer gaugePointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePointer.DistanceFromScale, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "DistanceFromScale", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePointer.DistanceFromScale, ref variantResult, gaugePointer.ExprHost, () => gaugePointer.ExprHost.DistanceFromScaleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x0013FBF8 File Offset: 0x0013DDF8
		internal double EvaluateGaugePointerMarkerLengthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePointer gaugePointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePointer.MarkerLength, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "MarkerLength", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePointer.MarkerLength, ref variantResult, gaugePointer.ExprHost, () => gaugePointer.ExprHost.MarkerLengthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x0013FC68 File Offset: 0x0013DE68
		internal string EvaluateGaugePointerMarkerStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePointer gaugePointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePointer.MarkerStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "MarkerStyle", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePointer.MarkerStyle, ref variantResult, gaugePointer.ExprHost, () => gaugePointer.ExprHost.MarkerStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DF0 RID: 19952 RVA: 0x0013FCD8 File Offset: 0x0013DED8
		internal string EvaluateGaugePointerPlacementExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePointer gaugePointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePointer.Placement, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Placement", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePointer.Placement, ref variantResult, gaugePointer.ExprHost, () => gaugePointer.ExprHost.PlacementExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x0013FD48 File Offset: 0x0013DF48
		internal bool EvaluateGaugePointerSnappingEnabledExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePointer gaugePointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePointer.SnappingEnabled, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "SnappingEnabled", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePointer.SnappingEnabled, ref variantResult, gaugePointer.ExprHost, () => gaugePointer.ExprHost.SnappingEnabledExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x0013FDB8 File Offset: 0x0013DFB8
		internal double EvaluateGaugePointerSnappingIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePointer gaugePointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePointer.SnappingInterval, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "SnappingInterval", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePointer.SnappingInterval, ref variantResult, gaugePointer.ExprHost, () => gaugePointer.ExprHost.SnappingIntervalExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x0013FE28 File Offset: 0x0013E028
		internal string EvaluateGaugePointerToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePointer gaugePointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePointer.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePointer.ToolTip, ref variantResult, gaugePointer.ExprHost, () => gaugePointer.ExprHost.ToolTipExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x0013FE98 File Offset: 0x0013E098
		internal bool EvaluateGaugePointerHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePointer gaugePointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePointer.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePointer.Hidden, ref variantResult, gaugePointer.ExprHost, () => gaugePointer.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x0013FF08 File Offset: 0x0013E108
		internal double EvaluateGaugePointerWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugePointer gaugePointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugePointer.Width, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Width", out variantResult))
			{
				this.EvaluateComplexExpression(gaugePointer.Width, ref variantResult, gaugePointer.ExprHost, () => gaugePointer.ExprHost.WidthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x0013FF78 File Offset: 0x0013E178
		internal double EvaluateGaugeScaleIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeScale gaugeScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeScale.Interval, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Interval", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeScale.Interval, ref variantResult, gaugeScale.ExprHost, () => gaugeScale.ExprHost.IntervalExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x0013FFE8 File Offset: 0x0013E1E8
		internal double EvaluateGaugeScaleIntervalOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeScale gaugeScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeScale.IntervalOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "IntervalOffset", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeScale.IntervalOffset, ref variantResult, gaugeScale.ExprHost, () => gaugeScale.ExprHost.IntervalOffsetExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x00140058 File Offset: 0x0013E258
		internal bool EvaluateGaugeScaleLogarithmicExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeScale gaugeScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeScale.Logarithmic, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Logarithmic", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeScale.Logarithmic, ref variantResult, gaugeScale.ExprHost, () => gaugeScale.ExprHost.LogarithmicExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x001400C8 File Offset: 0x0013E2C8
		internal double EvaluateGaugeScaleLogarithmicBaseExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeScale gaugeScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeScale.LogarithmicBase, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "LogarithmicBase", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeScale.LogarithmicBase, ref variantResult, gaugeScale.ExprHost, () => gaugeScale.ExprHost.LogarithmicBaseExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x00140138 File Offset: 0x0013E338
		internal double EvaluateGaugeScaleMultiplierExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeScale gaugeScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeScale.Multiplier, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Multiplier", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeScale.Multiplier, ref variantResult, gaugeScale.ExprHost, () => gaugeScale.ExprHost.MultiplierExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x001401A8 File Offset: 0x0013E3A8
		internal bool EvaluateGaugeScaleReversedExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeScale gaugeScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeScale.Reversed, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Reversed", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeScale.Reversed, ref variantResult, gaugeScale.ExprHost, () => gaugeScale.ExprHost.ReversedExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x00140218 File Offset: 0x0013E418
		internal bool EvaluateGaugeScaleTickMarksOnTopExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeScale gaugeScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeScale.TickMarksOnTop, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "TickMarksOnTop", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeScale.TickMarksOnTop, ref variantResult, gaugeScale.ExprHost, () => gaugeScale.ExprHost.TickMarksOnTopExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x00140288 File Offset: 0x0013E488
		internal string EvaluateGaugeScaleToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeScale gaugeScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeScale.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeScale.ToolTip, ref variantResult, gaugeScale.ExprHost, () => gaugeScale.ExprHost.ToolTipExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x001402F8 File Offset: 0x0013E4F8
		internal bool EvaluateGaugeScaleHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeScale gaugeScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeScale.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeScale.Hidden, ref variantResult, gaugeScale.ExprHost, () => gaugeScale.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x00140368 File Offset: 0x0013E568
		internal double EvaluateGaugeScaleWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeScale gaugeScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeScale.Width, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Width", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeScale.Width, ref variantResult, gaugeScale.ExprHost, () => gaugeScale.ExprHost.WidthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x001403D8 File Offset: 0x0013E5D8
		internal double EvaluateGaugeTickMarksIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeTickMarks gaugeTickMarks, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeTickMarks.Interval, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Interval", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeTickMarks.Interval, ref variantResult, gaugeTickMarks.ExprHost, () => ((GaugeTickMarksExprHost)gaugeTickMarks.ExprHost).IntervalExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x00140448 File Offset: 0x0013E648
		internal double EvaluateGaugeTickMarksIntervalOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeTickMarks gaugeTickMarks, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeTickMarks.IntervalOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "IntervalOffset", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeTickMarks.IntervalOffset, ref variantResult, gaugeTickMarks.ExprHost, () => ((GaugeTickMarksExprHost)gaugeTickMarks.ExprHost).IntervalOffsetExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x001404B8 File Offset: 0x0013E6B8
		internal string EvaluateLinearGaugeOrientationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.LinearGauge linearGauge, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(linearGauge.Orientation, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Orientation", out variantResult))
			{
				this.EvaluateComplexExpression(linearGauge.Orientation, ref variantResult, linearGauge.ExprHost, () => ((LinearGaugeExprHost)linearGauge.ExprHost).OrientationExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x00140528 File Offset: 0x0013E728
		internal string EvaluateLinearPointerTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.LinearPointer linearPointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(linearPointer.Type, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Type", out variantResult))
			{
				this.EvaluateComplexExpression(linearPointer.Type, ref variantResult, linearPointer.ExprHost, () => ((LinearPointerExprHost)linearPointer.ExprHost).TypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x00140598 File Offset: 0x0013E798
		internal double EvaluateLinearScaleStartMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.LinearScale linearScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(linearScale.StartMargin, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "StartMargin", out variantResult))
			{
				this.EvaluateComplexExpression(linearScale.StartMargin, ref variantResult, linearScale.ExprHost, () => ((LinearScaleExprHost)linearScale.ExprHost).StartMarginExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x00140608 File Offset: 0x0013E808
		internal double EvaluateLinearScaleEndMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.LinearScale linearScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(linearScale.EndMargin, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "EndMargin", out variantResult))
			{
				this.EvaluateComplexExpression(linearScale.EndMargin, ref variantResult, linearScale.ExprHost, () => ((LinearScaleExprHost)linearScale.ExprHost).EndMarginExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x00140678 File Offset: 0x0013E878
		internal double EvaluateLinearScalePositionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.LinearScale linearScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(linearScale.Position, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Position", out variantResult))
			{
				this.EvaluateComplexExpression(linearScale.Position, ref variantResult, linearScale.ExprHost, () => ((LinearScaleExprHost)linearScale.ExprHost).PositionExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x001406E8 File Offset: 0x0013E8E8
		internal string EvaluatePinLabelTextExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PinLabel pinLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pinLabel.Text, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Text", out variantResult))
			{
				this.EvaluateComplexExpression(pinLabel.Text, ref variantResult, pinLabel.ExprHost, () => pinLabel.ExprHost.TextExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E08 RID: 19976 RVA: 0x00140758 File Offset: 0x0013E958
		internal bool EvaluatePinLabelAllowUpsideDownExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PinLabel pinLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pinLabel.AllowUpsideDown, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "AllowUpsideDown", out variantResult))
			{
				this.EvaluateComplexExpression(pinLabel.AllowUpsideDown, ref variantResult, pinLabel.ExprHost, () => pinLabel.ExprHost.AllowUpsideDownExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x001407C8 File Offset: 0x0013E9C8
		internal double EvaluatePinLabelDistanceFromScaleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PinLabel pinLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pinLabel.DistanceFromScale, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "DistanceFromScale", out variantResult))
			{
				this.EvaluateComplexExpression(pinLabel.DistanceFromScale, ref variantResult, pinLabel.ExprHost, () => pinLabel.ExprHost.DistanceFromScaleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x00140838 File Offset: 0x0013EA38
		internal double EvaluatePinLabelFontAngleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PinLabel pinLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pinLabel.FontAngle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "FontAngle", out variantResult))
			{
				this.EvaluateComplexExpression(pinLabel.FontAngle, ref variantResult, pinLabel.ExprHost, () => pinLabel.ExprHost.FontAngleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x001408A8 File Offset: 0x0013EAA8
		internal string EvaluatePinLabelPlacementExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PinLabel pinLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pinLabel.Placement, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Placement", out variantResult))
			{
				this.EvaluateComplexExpression(pinLabel.Placement, ref variantResult, pinLabel.ExprHost, () => pinLabel.ExprHost.PlacementExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x00140918 File Offset: 0x0013EB18
		internal bool EvaluatePinLabelRotateLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PinLabel pinLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pinLabel.RotateLabel, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "RotateLabel", out variantResult))
			{
				this.EvaluateComplexExpression(pinLabel.RotateLabel, ref variantResult, pinLabel.ExprHost, () => pinLabel.ExprHost.RotateLabelExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x00140988 File Offset: 0x0013EB88
		internal bool EvaluatePinLabelUseFontPercentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PinLabel pinLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pinLabel.UseFontPercent, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "UseFontPercent", out variantResult))
			{
				this.EvaluateComplexExpression(pinLabel.UseFontPercent, ref variantResult, pinLabel.ExprHost, () => pinLabel.ExprHost.UseFontPercentExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x001409F8 File Offset: 0x0013EBF8
		internal bool EvaluatePointerCapOnTopExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PointerCap pointerCap, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pointerCap.OnTop, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "OnTop", out variantResult))
			{
				this.EvaluateComplexExpression(pointerCap.OnTop, ref variantResult, pointerCap.ExprHost, () => pointerCap.ExprHost.OnTopExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x00140A68 File Offset: 0x0013EC68
		internal bool EvaluatePointerCapReflectionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PointerCap pointerCap, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pointerCap.Reflection, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Reflection", out variantResult))
			{
				this.EvaluateComplexExpression(pointerCap.Reflection, ref variantResult, pointerCap.ExprHost, () => pointerCap.ExprHost.ReflectionExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x00140AD8 File Offset: 0x0013ECD8
		internal string EvaluatePointerCapCapStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PointerCap pointerCap, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pointerCap.CapStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "CapStyle", out variantResult))
			{
				this.EvaluateComplexExpression(pointerCap.CapStyle, ref variantResult, pointerCap.ExprHost, () => pointerCap.ExprHost.CapStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x00140B48 File Offset: 0x0013ED48
		internal bool EvaluatePointerCapHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PointerCap pointerCap, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pointerCap.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(pointerCap.Hidden, ref variantResult, pointerCap.ExprHost, () => pointerCap.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x00140BB8 File Offset: 0x0013EDB8
		internal double EvaluatePointerCapWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PointerCap pointerCap, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pointerCap.Width, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Width", out variantResult))
			{
				this.EvaluateComplexExpression(pointerCap.Width, ref variantResult, pointerCap.ExprHost, () => pointerCap.ExprHost.WidthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x00140C28 File Offset: 0x0013EE28
		internal string EvaluatePointerImageHueColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PointerImage pointerImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pointerImage.HueColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "HueColor", out variantResult))
			{
				this.EvaluateComplexExpression(pointerImage.HueColor, ref variantResult, pointerImage.ExprHost, () => ((PointerImageExprHost)pointerImage.ExprHost).HueColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x00140C9C File Offset: 0x0013EE9C
		internal double EvaluatePointerImageTransparencyExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PointerImage pointerImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pointerImage.Transparency, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Transparency", out variantResult))
			{
				this.EvaluateComplexExpression(pointerImage.Transparency, ref variantResult, pointerImage.ExprHost, () => ((PointerImageExprHost)pointerImage.ExprHost).TransparencyExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x00140D0C File Offset: 0x0013EF0C
		internal string EvaluatePointerImageOffsetXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PointerImage pointerImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pointerImage.OffsetX, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "OffsetX", out variantResult))
			{
				this.EvaluateComplexExpression(pointerImage.OffsetX, ref variantResult, pointerImage.ExprHost, () => ((PointerImageExprHost)pointerImage.ExprHost).OffsetXExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x00140D80 File Offset: 0x0013EF80
		internal string EvaluatePointerImageOffsetYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PointerImage pointerImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(pointerImage.OffsetY, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "OffsetY", out variantResult))
			{
				this.EvaluateComplexExpression(pointerImage.OffsetY, ref variantResult, pointerImage.ExprHost, () => ((PointerImageExprHost)pointerImage.ExprHost).OffsetYExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x00140DF4 File Offset: 0x0013EFF4
		internal double EvaluateRadialGaugePivotXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.RadialGauge radialGauge, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(radialGauge.PivotX, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "PivotX", out variantResult))
			{
				this.EvaluateComplexExpression(radialGauge.PivotX, ref variantResult, radialGauge.ExprHost, () => ((RadialGaugeExprHost)radialGauge.ExprHost).PivotXExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E18 RID: 19992 RVA: 0x00140E64 File Offset: 0x0013F064
		internal double EvaluateRadialGaugePivotYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.RadialGauge radialGauge, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(radialGauge.PivotY, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "PivotY", out variantResult))
			{
				this.EvaluateComplexExpression(radialGauge.PivotY, ref variantResult, radialGauge.ExprHost, () => ((RadialGaugeExprHost)radialGauge.ExprHost).PivotYExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E19 RID: 19993 RVA: 0x00140ED4 File Offset: 0x0013F0D4
		internal string EvaluateRadialPointerTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.RadialPointer radialPointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(radialPointer.Type, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Type", out variantResult))
			{
				this.EvaluateComplexExpression(radialPointer.Type, ref variantResult, radialPointer.ExprHost, () => ((RadialPointerExprHost)radialPointer.ExprHost).TypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x00140F44 File Offset: 0x0013F144
		internal string EvaluateRadialPointerNeedleStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.RadialPointer radialPointer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(radialPointer.NeedleStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "NeedleStyle", out variantResult))
			{
				this.EvaluateComplexExpression(radialPointer.NeedleStyle, ref variantResult, radialPointer.ExprHost, () => ((RadialPointerExprHost)radialPointer.ExprHost).NeedleStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E1B RID: 19995 RVA: 0x00140FB4 File Offset: 0x0013F1B4
		internal double EvaluateRadialScaleRadiusExpression(Microsoft.ReportingServices.ReportIntermediateFormat.RadialScale radialScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(radialScale.Radius, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Radius", out variantResult))
			{
				this.EvaluateComplexExpression(radialScale.Radius, ref variantResult, radialScale.ExprHost, () => ((RadialScaleExprHost)radialScale.ExprHost).RadiusExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x00141024 File Offset: 0x0013F224
		internal double EvaluateRadialScaleStartAngleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.RadialScale radialScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(radialScale.StartAngle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "StartAngle", out variantResult))
			{
				this.EvaluateComplexExpression(radialScale.StartAngle, ref variantResult, radialScale.ExprHost, () => ((RadialScaleExprHost)radialScale.ExprHost).StartAngleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x00141094 File Offset: 0x0013F294
		internal double EvaluateRadialScaleSweepAngleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.RadialScale radialScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(radialScale.SweepAngle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "SweepAngle", out variantResult))
			{
				this.EvaluateComplexExpression(radialScale.SweepAngle, ref variantResult, radialScale.ExprHost, () => ((RadialScaleExprHost)radialScale.ExprHost).SweepAngleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x00141104 File Offset: 0x0013F304
		internal double EvaluateScaleLabelsIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleLabels scaleLabels, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleLabels.Interval, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Interval", out variantResult))
			{
				this.EvaluateComplexExpression(scaleLabels.Interval, ref variantResult, scaleLabels.ExprHost, () => scaleLabels.ExprHost.IntervalExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x00141174 File Offset: 0x0013F374
		internal double EvaluateScaleLabelsIntervalOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleLabels scaleLabels, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleLabels.IntervalOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "IntervalOffset", out variantResult))
			{
				this.EvaluateComplexExpression(scaleLabels.IntervalOffset, ref variantResult, scaleLabels.ExprHost, () => scaleLabels.ExprHost.IntervalOffsetExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x001411E4 File Offset: 0x0013F3E4
		internal bool EvaluateScaleLabelsAllowUpsideDownExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleLabels scaleLabels, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleLabels.AllowUpsideDown, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "AllowUpsideDown", out variantResult))
			{
				this.EvaluateComplexExpression(scaleLabels.AllowUpsideDown, ref variantResult, scaleLabels.ExprHost, () => scaleLabels.ExprHost.AllowUpsideDownExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x00141254 File Offset: 0x0013F454
		internal double EvaluateScaleLabelsDistanceFromScaleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleLabels scaleLabels, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleLabels.DistanceFromScale, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "DistanceFromScale", out variantResult))
			{
				this.EvaluateComplexExpression(scaleLabels.DistanceFromScale, ref variantResult, scaleLabels.ExprHost, () => scaleLabels.ExprHost.DistanceFromScaleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x001412C4 File Offset: 0x0013F4C4
		internal double EvaluateScaleLabelsFontAngleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleLabels scaleLabels, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleLabels.FontAngle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "FontAngle", out variantResult))
			{
				this.EvaluateComplexExpression(scaleLabels.FontAngle, ref variantResult, scaleLabels.ExprHost, () => scaleLabels.ExprHost.FontAngleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x00141334 File Offset: 0x0013F534
		internal string EvaluateScaleLabelsPlacementExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleLabels scaleLabels, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleLabels.Placement, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Placement", out variantResult))
			{
				this.EvaluateComplexExpression(scaleLabels.Placement, ref variantResult, scaleLabels.ExprHost, () => scaleLabels.ExprHost.PlacementExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x001413A4 File Offset: 0x0013F5A4
		internal bool EvaluateScaleLabelsRotateLabelsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleLabels scaleLabels, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleLabels.RotateLabels, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "RotateLabels", out variantResult))
			{
				this.EvaluateComplexExpression(scaleLabels.RotateLabels, ref variantResult, scaleLabels.ExprHost, () => scaleLabels.ExprHost.RotateLabelsExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x00141414 File Offset: 0x0013F614
		internal bool EvaluateScaleLabelsShowEndLabelsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleLabels scaleLabels, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleLabels.ShowEndLabels, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ShowEndLabels", out variantResult))
			{
				this.EvaluateComplexExpression(scaleLabels.ShowEndLabels, ref variantResult, scaleLabels.ExprHost, () => scaleLabels.ExprHost.ShowEndLabelsExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x00141484 File Offset: 0x0013F684
		internal bool EvaluateScaleLabelsHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleLabels scaleLabels, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleLabels.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(scaleLabels.Hidden, ref variantResult, scaleLabels.ExprHost, () => scaleLabels.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x001414F4 File Offset: 0x0013F6F4
		internal bool EvaluateScaleLabelsUseFontPercentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleLabels scaleLabels, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleLabels.UseFontPercent, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "UseFontPercent", out variantResult))
			{
				this.EvaluateComplexExpression(scaleLabels.UseFontPercent, ref variantResult, scaleLabels.ExprHost, () => scaleLabels.ExprHost.UseFontPercentExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x00141564 File Offset: 0x0013F764
		internal double EvaluateScalePinLocationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScalePin scalePin, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scalePin.Location, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Location", out variantResult))
			{
				this.EvaluateComplexExpression(scalePin.Location, ref variantResult, scalePin.ExprHost, () => ((ScalePinExprHost)scalePin.ExprHost).LocationExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x001415D4 File Offset: 0x0013F7D4
		internal bool EvaluateScalePinEnableExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScalePin scalePin, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scalePin.Enable, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Enable", out variantResult))
			{
				this.EvaluateComplexExpression(scalePin.Enable, ref variantResult, scalePin.ExprHost, () => ((ScalePinExprHost)scalePin.ExprHost).EnableExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x00141644 File Offset: 0x0013F844
		internal double EvaluateScaleRangeDistanceFromScaleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleRange scaleRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleRange.DistanceFromScale, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "DistanceFromScale", out variantResult))
			{
				this.EvaluateComplexExpression(scaleRange.DistanceFromScale, ref variantResult, scaleRange.ExprHost, () => scaleRange.ExprHost.DistanceFromScaleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x001416B4 File Offset: 0x0013F8B4
		internal double EvaluateScaleRangeStartWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleRange scaleRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleRange.StartWidth, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "StartWidth", out variantResult))
			{
				this.EvaluateComplexExpression(scaleRange.StartWidth, ref variantResult, scaleRange.ExprHost, () => scaleRange.ExprHost.StartWidthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x00141724 File Offset: 0x0013F924
		internal double EvaluateScaleRangeEndWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleRange scaleRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleRange.EndWidth, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "EndWidth", out variantResult))
			{
				this.EvaluateComplexExpression(scaleRange.EndWidth, ref variantResult, scaleRange.ExprHost, () => scaleRange.ExprHost.EndWidthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x00141794 File Offset: 0x0013F994
		internal string EvaluateScaleRangeInRangeBarPointerColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleRange scaleRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleRange.InRangeBarPointerColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "InRangeBarPointerColor", out variantResult))
			{
				this.EvaluateComplexExpression(scaleRange.InRangeBarPointerColor, ref variantResult, scaleRange.ExprHost, () => scaleRange.ExprHost.InRangeBarPointerColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x00141808 File Offset: 0x0013FA08
		internal string EvaluateScaleRangeInRangeLabelColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleRange scaleRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleRange.InRangeLabelColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "InRangeLabelColor", out variantResult))
			{
				this.EvaluateComplexExpression(scaleRange.InRangeLabelColor, ref variantResult, scaleRange.ExprHost, () => scaleRange.ExprHost.InRangeLabelColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x0014187C File Offset: 0x0013FA7C
		internal string EvaluateScaleRangeInRangeTickMarksColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleRange scaleRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleRange.InRangeTickMarksColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "InRangeTickMarksColor", out variantResult))
			{
				this.EvaluateComplexExpression(scaleRange.InRangeTickMarksColor, ref variantResult, scaleRange.ExprHost, () => scaleRange.ExprHost.InRangeTickMarksColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x001418F0 File Offset: 0x0013FAF0
		internal string EvaluateScaleRangePlacementExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleRange scaleRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleRange.Placement, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Placement", out variantResult))
			{
				this.EvaluateComplexExpression(scaleRange.Placement, ref variantResult, scaleRange.ExprHost, () => scaleRange.ExprHost.PlacementExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x00141960 File Offset: 0x0013FB60
		internal string EvaluateScaleRangeToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleRange scaleRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleRange.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(scaleRange.ToolTip, ref variantResult, scaleRange.ExprHost, () => scaleRange.ExprHost.ToolTipExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x001419D0 File Offset: 0x0013FBD0
		internal bool EvaluateScaleRangeHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleRange scaleRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleRange.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(scaleRange.Hidden, ref variantResult, scaleRange.ExprHost, () => scaleRange.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x00141A40 File Offset: 0x0013FC40
		internal string EvaluateScaleRangeBackgroundGradientTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ScaleRange scaleRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scaleRange.BackgroundGradientType, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "BackgroundGradientType", out variantResult))
			{
				this.EvaluateComplexExpression(scaleRange.BackgroundGradientType, ref variantResult, scaleRange.ExprHost, () => scaleRange.ExprHost.BackgroundGradientTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x00141AB0 File Offset: 0x0013FCB0
		internal double EvaluateThermometerBulbOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Thermometer thermometer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(thermometer.BulbOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "BulbOffset", out variantResult))
			{
				this.EvaluateComplexExpression(thermometer.BulbOffset, ref variantResult, thermometer.ExprHost, () => thermometer.ExprHost.BulbOffsetExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x00141B20 File Offset: 0x0013FD20
		internal double EvaluateThermometerBulbSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Thermometer thermometer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(thermometer.BulbSize, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "BulbSize", out variantResult))
			{
				this.EvaluateComplexExpression(thermometer.BulbSize, ref variantResult, thermometer.ExprHost, () => thermometer.ExprHost.BulbSizeExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x00141B90 File Offset: 0x0013FD90
		internal string EvaluateThermometerThermometerStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Thermometer thermometer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(thermometer.ThermometerStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ThermometerStyle", out variantResult))
			{
				this.EvaluateComplexExpression(thermometer.ThermometerStyle, ref variantResult, thermometer.ExprHost, () => thermometer.ExprHost.ThermometerStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x00141C00 File Offset: 0x0013FE00
		internal double EvaluateTickMarkStyleDistanceFromScaleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TickMarkStyle tickMarkStyle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(tickMarkStyle.DistanceFromScale, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "DistanceFromScale", out variantResult))
			{
				this.EvaluateComplexExpression(tickMarkStyle.DistanceFromScale, ref variantResult, tickMarkStyle.ExprHost, () => tickMarkStyle.ExprHost.DistanceFromScaleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x00141C70 File Offset: 0x0013FE70
		internal string EvaluateTickMarkStylePlacementExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TickMarkStyle tickMarkStyle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(tickMarkStyle.Placement, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Placement", out variantResult))
			{
				this.EvaluateComplexExpression(tickMarkStyle.Placement, ref variantResult, tickMarkStyle.ExprHost, () => tickMarkStyle.ExprHost.PlacementExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x00141CE0 File Offset: 0x0013FEE0
		internal bool EvaluateTickMarkStyleEnableGradientExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TickMarkStyle tickMarkStyle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(tickMarkStyle.EnableGradient, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "EnableGradient", out variantResult))
			{
				this.EvaluateComplexExpression(tickMarkStyle.EnableGradient, ref variantResult, tickMarkStyle.ExprHost, () => tickMarkStyle.ExprHost.EnableGradientExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x00141D50 File Offset: 0x0013FF50
		internal double EvaluateTickMarkStyleGradientDensityExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TickMarkStyle tickMarkStyle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(tickMarkStyle.GradientDensity, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "GradientDensity", out variantResult))
			{
				this.EvaluateComplexExpression(tickMarkStyle.GradientDensity, ref variantResult, tickMarkStyle.ExprHost, () => tickMarkStyle.ExprHost.GradientDensityExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x00141DC0 File Offset: 0x0013FFC0
		internal double EvaluateTickMarkStyleLengthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TickMarkStyle tickMarkStyle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(tickMarkStyle.Length, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Length", out variantResult))
			{
				this.EvaluateComplexExpression(tickMarkStyle.Length, ref variantResult, tickMarkStyle.ExprHost, () => tickMarkStyle.ExprHost.LengthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x00141E30 File Offset: 0x00140030
		internal double EvaluateTickMarkStyleWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TickMarkStyle tickMarkStyle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(tickMarkStyle.Width, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Width", out variantResult))
			{
				this.EvaluateComplexExpression(tickMarkStyle.Width, ref variantResult, tickMarkStyle.ExprHost, () => tickMarkStyle.ExprHost.WidthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x00141EA0 File Offset: 0x001400A0
		internal string EvaluateTickMarkStyleShapeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TickMarkStyle tickMarkStyle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(tickMarkStyle.Shape, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Shape", out variantResult))
			{
				this.EvaluateComplexExpression(tickMarkStyle.Shape, ref variantResult, tickMarkStyle.ExprHost, () => tickMarkStyle.ExprHost.ShapeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x00141F10 File Offset: 0x00140110
		internal bool EvaluateTickMarkStyleHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TickMarkStyle tickMarkStyle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(tickMarkStyle.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(tickMarkStyle.Hidden, ref variantResult, tickMarkStyle.ExprHost, () => tickMarkStyle.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x00141F80 File Offset: 0x00140180
		internal VariantResult EvaluateCustomLabelTextExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CustomLabel customLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(customLabel.Text, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Text", out variantResult))
			{
				this.EvaluateComplexExpression(customLabel.Text, ref variantResult, customLabel.ExprHost, () => customLabel.ExprHost.TextExpr);
			}
			this.ProcessVariantResult(customLabel.Text, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x00141FF8 File Offset: 0x001401F8
		internal bool EvaluateCustomLabelAllowUpsideDownExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CustomLabel customLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(customLabel.AllowUpsideDown, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "AllowUpsideDown", out variantResult))
			{
				this.EvaluateComplexExpression(customLabel.AllowUpsideDown, ref variantResult, customLabel.ExprHost, () => customLabel.ExprHost.AllowUpsideDownExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x00142068 File Offset: 0x00140268
		internal double EvaluateCustomLabelDistanceFromScaleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CustomLabel customLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(customLabel.DistanceFromScale, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "DistanceFromScale", out variantResult))
			{
				this.EvaluateComplexExpression(customLabel.DistanceFromScale, ref variantResult, customLabel.ExprHost, () => customLabel.ExprHost.DistanceFromScaleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x001420D8 File Offset: 0x001402D8
		internal double EvaluateCustomLabelFontAngleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CustomLabel customLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(customLabel.FontAngle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "FontAngle", out variantResult))
			{
				this.EvaluateComplexExpression(customLabel.FontAngle, ref variantResult, customLabel.ExprHost, () => customLabel.ExprHost.FontAngleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x00142148 File Offset: 0x00140348
		internal string EvaluateCustomLabelPlacementExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CustomLabel customLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(customLabel.Placement, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Placement", out variantResult))
			{
				this.EvaluateComplexExpression(customLabel.Placement, ref variantResult, customLabel.ExprHost, () => customLabel.ExprHost.PlacementExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x001421B8 File Offset: 0x001403B8
		internal bool EvaluateCustomLabelRotateLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CustomLabel customLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(customLabel.RotateLabel, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "RotateLabel", out variantResult))
			{
				this.EvaluateComplexExpression(customLabel.RotateLabel, ref variantResult, customLabel.ExprHost, () => customLabel.ExprHost.RotateLabelExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x00142228 File Offset: 0x00140428
		internal double EvaluateCustomLabelValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CustomLabel customLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(customLabel.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(customLabel.Value, ref variantResult, customLabel.ExprHost, () => customLabel.ExprHost.ValueExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x00142298 File Offset: 0x00140498
		internal bool EvaluateCustomLabelHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CustomLabel customLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(customLabel.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(customLabel.Hidden, ref variantResult, customLabel.ExprHost, () => customLabel.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x00142308 File Offset: 0x00140508
		internal bool EvaluateCustomLabelUseFontPercentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.CustomLabel customLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(customLabel.UseFontPercent, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "UseFontPercent", out variantResult))
			{
				this.EvaluateComplexExpression(customLabel.UseFontPercent, ref variantResult, customLabel.ExprHost, () => customLabel.ExprHost.UseFontPercentExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x00142378 File Offset: 0x00140578
		internal bool EvaluateGaugeClipContentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Gauge gauge, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gauge.ClipContent, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ClipContent", out variantResult))
			{
				this.EvaluateComplexExpression(gauge.ClipContent, ref variantResult, gauge.ExprHost, () => ((GaugeExprHost)gauge.ExprHost).ClipContentExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x001423E8 File Offset: 0x001405E8
		internal double EvaluateGaugeAspectRatioExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Gauge gauge, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gauge.AspectRatio, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "AspectRatio", out variantResult))
			{
				this.EvaluateComplexExpression(gauge.AspectRatio, ref variantResult, gauge.ExprHost, () => ((GaugeExprHost)gauge.ExprHost).AspectRatioExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x00142458 File Offset: 0x00140658
		internal VariantResult EvaluateGaugeInputValueValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeInputValue gaugeInputValue, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeInputValue.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeInputValue.Value, ref variantResult, gaugeInputValue.ExprHost, () => gaugeInputValue.ExprHost.ValueExpr);
			}
			this.ProcessVariantResult(gaugeInputValue.Value, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004E4B RID: 20043 RVA: 0x001424D0 File Offset: 0x001406D0
		internal string EvaluateGaugeInputValueFormulaExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeInputValue gaugeInputValue, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeInputValue.Formula, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Formula", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeInputValue.Formula, ref variantResult, gaugeInputValue.ExprHost, () => gaugeInputValue.ExprHost.FormulaExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x00142540 File Offset: 0x00140740
		internal double EvaluateGaugeInputValueMinPercentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeInputValue gaugeInputValue, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeInputValue.MinPercent, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "MinPercent", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeInputValue.MinPercent, ref variantResult, gaugeInputValue.ExprHost, () => gaugeInputValue.ExprHost.MinPercentExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x001425B0 File Offset: 0x001407B0
		internal double EvaluateGaugeInputValueMaxPercentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeInputValue gaugeInputValue, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeInputValue.MaxPercent, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "MaxPercent", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeInputValue.MaxPercent, ref variantResult, gaugeInputValue.ExprHost, () => gaugeInputValue.ExprHost.MaxPercentExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x00142620 File Offset: 0x00140820
		internal double EvaluateGaugeInputValueMultiplierExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeInputValue gaugeInputValue, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeInputValue.Multiplier, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Multiplier", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeInputValue.Multiplier, ref variantResult, gaugeInputValue.ExprHost, () => gaugeInputValue.ExprHost.MultiplierExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x00142690 File Offset: 0x00140890
		internal double EvaluateGaugeInputValueAddConstantExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeInputValue gaugeInputValue, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeInputValue.AddConstant, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "AddConstant", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeInputValue.AddConstant, ref variantResult, gaugeInputValue.ExprHost, () => gaugeInputValue.ExprHost.AddConstantExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x00142700 File Offset: 0x00140900
		internal VariantResult EvaluateGaugeLabelTextExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeLabel gaugeLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeLabel.Text, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Text", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeLabel.Text, ref variantResult, gaugeLabel.ExprHost, () => ((GaugeLabelExprHost)gaugeLabel.ExprHost).TextExpr);
			}
			this.ProcessVariantResult(gaugeLabel.Text, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x00142778 File Offset: 0x00140978
		internal double EvaluateGaugeLabelAngleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeLabel gaugeLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeLabel.Angle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Angle", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeLabel.Angle, ref variantResult, gaugeLabel.ExprHost, () => ((GaugeLabelExprHost)gaugeLabel.ExprHost).AngleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x001427E8 File Offset: 0x001409E8
		internal string EvaluateGaugeLabelResizeModeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeLabel gaugeLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeLabel.ResizeMode, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ResizeMode", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeLabel.ResizeMode, ref variantResult, gaugeLabel.ExprHost, () => ((GaugeLabelExprHost)gaugeLabel.ExprHost).ResizeModeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x00142858 File Offset: 0x00140A58
		internal string EvaluateGaugeLabelTextShadowOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeLabel gaugeLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeLabel.TextShadowOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "TextShadowOffset", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeLabel.TextShadowOffset, ref variantResult, gaugeLabel.ExprHost, () => ((GaugeLabelExprHost)gaugeLabel.ExprHost).TextShadowOffsetExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x001428CC File Offset: 0x00140ACC
		internal bool EvaluateGaugeLabelUseFontPercentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.GaugeLabel gaugeLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(gaugeLabel.UseFontPercent, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "UseFontPercent", out variantResult))
			{
				this.EvaluateComplexExpression(gaugeLabel.UseFontPercent, ref variantResult, gaugeLabel.ExprHost, () => ((GaugeLabelExprHost)gaugeLabel.ExprHost).UseFontPercentExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x0014293C File Offset: 0x00140B3C
		internal string EvaluateNumericIndicatorDecimalDigitColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.DecimalDigitColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "DecimalDigitColor", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.DecimalDigitColor, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.DecimalDigitColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x001429B0 File Offset: 0x00140BB0
		internal string EvaluateNumericIndicatorDigitColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.DigitColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "DigitColor", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.DigitColor, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.DigitColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x00142A24 File Offset: 0x00140C24
		internal bool EvaluateNumericIndicatorUseFontPercentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.UseFontPercent, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "UseFontPercent", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.UseFontPercent, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.UseFontPercentExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x00142A94 File Offset: 0x00140C94
		internal int EvaluateNumericIndicatorDecimalDigitsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.DecimalDigits, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "DecimalDigits", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.DecimalDigits, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.DecimalDigitsExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x00142B04 File Offset: 0x00140D04
		internal int EvaluateNumericIndicatorDigitsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.Digits, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Digits", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.Digits, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.DigitsExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x00142B74 File Offset: 0x00140D74
		internal double EvaluateNumericIndicatorMultiplierExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.Multiplier, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Multiplier", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.Multiplier, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.MultiplierExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x00142BE4 File Offset: 0x00140DE4
		internal string EvaluateNumericIndicatorNonNumericStringExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.NonNumericString, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "NonNumericString", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.NonNumericString, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.NonNumericStringExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x00142C54 File Offset: 0x00140E54
		internal string EvaluateNumericIndicatorOutOfRangeStringExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.OutOfRangeString, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "OutOfRangeString", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.OutOfRangeString, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.OutOfRangeStringExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x00142CC4 File Offset: 0x00140EC4
		internal string EvaluateNumericIndicatorResizeModeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.ResizeMode, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ResizeMode", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.ResizeMode, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.ResizeModeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x00142D34 File Offset: 0x00140F34
		internal bool EvaluateNumericIndicatorShowDecimalPointExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.ShowDecimalPoint, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ShowDecimalPoint", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.ShowDecimalPoint, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.ShowDecimalPointExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x00142DA4 File Offset: 0x00140FA4
		internal bool EvaluateNumericIndicatorShowLeadingZerosExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.ShowLeadingZeros, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ShowLeadingZeros", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.ShowLeadingZeros, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.ShowLeadingZerosExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x00142E14 File Offset: 0x00141014
		internal string EvaluateNumericIndicatorIndicatorStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.IndicatorStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "IndicatorStyle", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.IndicatorStyle, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.IndicatorStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x00142E84 File Offset: 0x00141084
		internal string EvaluateNumericIndicatorShowSignExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.ShowSign, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ShowSign", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.ShowSign, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.ShowSignExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x00142EF4 File Offset: 0x001410F4
		internal bool EvaluateNumericIndicatorSnappingEnabledExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.SnappingEnabled, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "SnappingEnabled", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.SnappingEnabled, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.SnappingEnabledExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x00142F64 File Offset: 0x00141164
		internal double EvaluateNumericIndicatorSnappingIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.SnappingInterval, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "SnappingInterval", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.SnappingInterval, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.SnappingIntervalExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x00142FD4 File Offset: 0x001411D4
		internal string EvaluateNumericIndicatorLedDimColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.LedDimColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "LedDimColor", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.LedDimColor, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.LedDimColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x00143048 File Offset: 0x00141248
		internal double EvaluateNumericIndicatorSeparatorWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.SeparatorWidth, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "SeparatorWidth", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.SeparatorWidth, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.SeparatorWidthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x001430B8 File Offset: 0x001412B8
		internal string EvaluateNumericIndicatorSeparatorColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.NumericIndicator numericIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicator.SeparatorColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "SeparatorColor", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicator.SeparatorColor, ref variantResult, numericIndicator.ExprHost, () => numericIndicator.ExprHost.SeparatorColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x0014312C File Offset: 0x0014132C
		internal string EvaluateNumericIndicatorRangeDecimalDigitColorExpression(NumericIndicatorRange numericIndicatorRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicatorRange.DecimalDigitColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "DecimalDigitColor", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicatorRange.DecimalDigitColor, ref variantResult, numericIndicatorRange.ExprHost, () => numericIndicatorRange.ExprHost.DecimalDigitColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x001431A0 File Offset: 0x001413A0
		internal string EvaluateNumericIndicatorRangeDigitColorExpression(NumericIndicatorRange numericIndicatorRange, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(numericIndicatorRange.DigitColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "DigitColor", out variantResult))
			{
				this.EvaluateComplexExpression(numericIndicatorRange.DigitColor, ref variantResult, numericIndicatorRange.ExprHost, () => numericIndicatorRange.ExprHost.DigitColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x00143214 File Offset: 0x00141414
		internal string EvaluateIndicatorImageHueColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.IndicatorImage indicatorImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(indicatorImage.HueColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "HueColor", out variantResult))
			{
				this.EvaluateComplexExpression(indicatorImage.HueColor, ref variantResult, indicatorImage.ExprHost, () => indicatorImage.ExprHost.HueColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x00143288 File Offset: 0x00141488
		internal double EvaluateIndicatorImageTransparencyExpression(Microsoft.ReportingServices.ReportIntermediateFormat.IndicatorImage indicatorImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(indicatorImage.Transparency, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Transparency", out variantResult))
			{
				this.EvaluateComplexExpression(indicatorImage.Transparency, ref variantResult, indicatorImage.ExprHost, () => indicatorImage.ExprHost.TransparencyExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x001432F8 File Offset: 0x001414F8
		internal string EvaluateStateIndicatorTransformationTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.StateIndicator stateIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(stateIndicator.TransformationType, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "TransformationType", out variantResult))
			{
				this.EvaluateComplexExpression(stateIndicator.TransformationType, ref variantResult, stateIndicator.ExprHost, () => stateIndicator.ExprHost.TransformationTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x00143368 File Offset: 0x00141568
		internal string EvaluateStateIndicatorIndicatorStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.StateIndicator stateIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(stateIndicator.IndicatorStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "IndicatorStyle", out variantResult))
			{
				this.EvaluateComplexExpression(stateIndicator.IndicatorStyle, ref variantResult, stateIndicator.ExprHost, () => stateIndicator.ExprHost.IndicatorStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x001433D8 File Offset: 0x001415D8
		internal double EvaluateStateIndicatorScaleFactorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.StateIndicator stateIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(stateIndicator.ScaleFactor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ScaleFactor", out variantResult))
			{
				this.EvaluateComplexExpression(stateIndicator.ScaleFactor, ref variantResult, stateIndicator.ExprHost, () => stateIndicator.ExprHost.ScaleFactorExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x00143448 File Offset: 0x00141648
		internal string EvaluateStateIndicatorResizeModeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.StateIndicator stateIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(stateIndicator.ResizeMode, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ResizeMode", out variantResult))
			{
				this.EvaluateComplexExpression(stateIndicator.ResizeMode, ref variantResult, stateIndicator.ExprHost, () => stateIndicator.ExprHost.ResizeModeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x001434B8 File Offset: 0x001416B8
		internal double EvaluateStateIndicatorAngleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.StateIndicator stateIndicator, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(stateIndicator.Angle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Angle", out variantResult))
			{
				this.EvaluateComplexExpression(stateIndicator.Angle, ref variantResult, stateIndicator.ExprHost, () => stateIndicator.ExprHost.AngleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x00143528 File Offset: 0x00141728
		internal string EvaluateIndicatorStateColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.IndicatorState indicatorState, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(indicatorState.Color, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "Color", out variantResult))
			{
				this.EvaluateComplexExpression(indicatorState.Color, ref variantResult, indicatorState.ExprHost, () => indicatorState.ExprHost.ColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x0014359C File Offset: 0x0014179C
		internal double EvaluateIndicatorStateScaleFactorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.IndicatorState indicatorState, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(indicatorState.ScaleFactor, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "ScaleFactor", out variantResult))
			{
				this.EvaluateComplexExpression(indicatorState.ScaleFactor, ref variantResult, indicatorState.ExprHost, () => indicatorState.ExprHost.ScaleFactorExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x0014360C File Offset: 0x0014180C
		internal string EvaluateIndicatorStateIndicatorStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.IndicatorState indicatorState, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(indicatorState.IndicatorStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, objectName, "IndicatorStyle", out variantResult))
			{
				this.EvaluateComplexExpression(indicatorState.IndicatorStyle, ref variantResult, indicatorState.ExprHost, () => indicatorState.ExprHost.IndicatorStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x0014367C File Offset: 0x0014187C
		internal double EvaluateMapLocationLeftExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLocation mapLocation, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLocation.Left, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Left", out variantResult))
			{
				this.EvaluateComplexExpression(mapLocation.Left, ref variantResult, mapLocation.ExprHost, () => mapLocation.ExprHost.LeftExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x001436EC File Offset: 0x001418EC
		internal double EvaluateMapLocationTopExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLocation mapLocation, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLocation.Top, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Top", out variantResult))
			{
				this.EvaluateComplexExpression(mapLocation.Top, ref variantResult, mapLocation.ExprHost, () => mapLocation.ExprHost.TopExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x0014375C File Offset: 0x0014195C
		internal string EvaluateMapLocationUnitExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLocation mapLocation, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLocation.Unit, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Unit", out variantResult))
			{
				this.EvaluateComplexExpression(mapLocation.Unit, ref variantResult, mapLocation.ExprHost, () => mapLocation.ExprHost.UnitExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x001437CC File Offset: 0x001419CC
		internal double EvaluateMapSizeWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSize mapSize, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSize.Width, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Width", out variantResult))
			{
				this.EvaluateComplexExpression(mapSize.Width, ref variantResult, mapSize.ExprHost, () => mapSize.ExprHost.WidthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x0014383C File Offset: 0x00141A3C
		internal double EvaluateMapSizeHeightExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSize mapSize, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSize.Height, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Height", out variantResult))
			{
				this.EvaluateComplexExpression(mapSize.Height, ref variantResult, mapSize.ExprHost, () => mapSize.ExprHost.HeightExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x001438AC File Offset: 0x00141AAC
		internal string EvaluateMapSizeUnitExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSize mapSize, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSize.Unit, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Unit", out variantResult))
			{
				this.EvaluateComplexExpression(mapSize.Unit, ref variantResult, mapSize.ExprHost, () => mapSize.ExprHost.UnitExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x0014391C File Offset: 0x00141B1C
		internal bool EvaluateMapGridLinesHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapGridLines mapGridLines, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapGridLines.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(mapGridLines.Hidden, ref variantResult, mapGridLines.ExprHost, () => mapGridLines.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x0014398C File Offset: 0x00141B8C
		internal double EvaluateMapGridLinesIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapGridLines mapGridLines, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapGridLines.Interval, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Interval", out variantResult))
			{
				this.EvaluateComplexExpression(mapGridLines.Interval, ref variantResult, mapGridLines.ExprHost, () => mapGridLines.ExprHost.IntervalExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x001439FC File Offset: 0x00141BFC
		internal bool EvaluateMapGridLinesShowLabelsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapGridLines mapGridLines, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapGridLines.ShowLabels, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ShowLabels", out variantResult))
			{
				this.EvaluateComplexExpression(mapGridLines.ShowLabels, ref variantResult, mapGridLines.ExprHost, () => mapGridLines.ExprHost.ShowLabelsExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x00143A6C File Offset: 0x00141C6C
		internal string EvaluateMapGridLinesLabelPositionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapGridLines mapGridLines, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapGridLines.LabelPosition, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LabelPosition", out variantResult))
			{
				this.EvaluateComplexExpression(mapGridLines.LabelPosition, ref variantResult, mapGridLines.ExprHost, () => mapGridLines.ExprHost.LabelPositionExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x00143ADC File Offset: 0x00141CDC
		internal string EvaluateMapDockableSubItemPositionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapDockableSubItem mapDockableSubItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapDockableSubItem.Position, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Position", out variantResult))
			{
				this.EvaluateComplexExpression(mapDockableSubItem.Position, ref variantResult, mapDockableSubItem.ExprHost, () => mapDockableSubItem.ExprHost.MapPositionExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x00143B4C File Offset: 0x00141D4C
		internal bool EvaluateMapDockableSubItemDockOutsideViewportExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapDockableSubItem mapDockableSubItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapDockableSubItem.DockOutsideViewport, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "DockOutsideViewport", out variantResult))
			{
				this.EvaluateComplexExpression(mapDockableSubItem.DockOutsideViewport, ref variantResult, mapDockableSubItem.ExprHost, () => mapDockableSubItem.ExprHost.DockOutsideViewportExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x00143BBC File Offset: 0x00141DBC
		internal bool EvaluateMapDockableSubItemHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapDockableSubItem mapDockableSubItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapDockableSubItem.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(mapDockableSubItem.Hidden, ref variantResult, mapDockableSubItem.ExprHost, () => mapDockableSubItem.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x00143C2C File Offset: 0x00141E2C
		internal VariantResult EvaluateMapDockableSubItemToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapDockableSubItem mapDockableSubItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapDockableSubItem.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(mapDockableSubItem.ToolTip, ref variantResult, mapDockableSubItem.ExprHost, () => mapDockableSubItem.ExprHost.ToolTipExpr);
			}
			return variantResult;
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x00143C90 File Offset: 0x00141E90
		internal string EvaluateMapSubItemLeftMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSubItem mapSubItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSubItem.LeftMargin, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LeftMargin", out variantResult))
			{
				this.EvaluateComplexExpression(mapSubItem.LeftMargin, ref variantResult, mapSubItem.ExprHost, () => mapSubItem.ExprHost.LeftMarginExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x00143D04 File Offset: 0x00141F04
		internal string EvaluateMapSubItemRightMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSubItem mapSubItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSubItem.RightMargin, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "RightMargin", out variantResult))
			{
				this.EvaluateComplexExpression(mapSubItem.RightMargin, ref variantResult, mapSubItem.ExprHost, () => mapSubItem.ExprHost.RightMarginExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x00143D78 File Offset: 0x00141F78
		internal string EvaluateMapSubItemTopMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSubItem mapSubItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSubItem.TopMargin, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "TopMargin", out variantResult))
			{
				this.EvaluateComplexExpression(mapSubItem.TopMargin, ref variantResult, mapSubItem.ExprHost, () => mapSubItem.ExprHost.TopMarginExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x00143DEC File Offset: 0x00141FEC
		internal string EvaluateMapSubItemBottomMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSubItem mapSubItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSubItem.BottomMargin, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "BottomMargin", out variantResult))
			{
				this.EvaluateComplexExpression(mapSubItem.BottomMargin, ref variantResult, mapSubItem.ExprHost, () => mapSubItem.ExprHost.BottomMarginExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x00143E60 File Offset: 0x00142060
		internal int EvaluateMapSubItemZIndexExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSubItem mapSubItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSubItem.ZIndex, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ZIndex", out variantResult))
			{
				this.EvaluateComplexExpression(mapSubItem.ZIndex, ref variantResult, mapSubItem.ExprHost, () => mapSubItem.ExprHost.ZIndexExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x00143ED0 File Offset: 0x001420D0
		internal string EvaluateMapBindingFieldPairFieldNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapBindingFieldPair mapBindingFieldPair, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapBindingFieldPair.FieldName, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "FieldName", out variantResult))
			{
				this.EvaluateComplexExpression(mapBindingFieldPair.FieldName, ref variantResult, mapBindingFieldPair.ExprHost, () => mapBindingFieldPair.ExprHost.FieldNameExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x00143F40 File Offset: 0x00142140
		internal VariantResult EvaluateMapBindingFieldPairBindingExpressionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapBindingFieldPair mapBindingFieldPair, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapBindingFieldPair.BindingExpression, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "BindingExpression", out variantResult))
			{
				if (mapBindingFieldPair.InElementView)
				{
					this.EvaluateComplexExpression(mapBindingFieldPair.BindingExpression, ref variantResult, mapBindingFieldPair.ExprHost, () => mapBindingFieldPair.ExprHost.BindingExpressionExpr);
				}
				else
				{
					this.EvaluateComplexExpression(mapBindingFieldPair.BindingExpression, ref variantResult, mapBindingFieldPair.ExprHostMapMember, () => mapBindingFieldPair.ExprHostMapMember.BindingExpressionExpr);
				}
			}
			this.ProcessVariantResult(mapBindingFieldPair.BindingExpression, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x00143FF0 File Offset: 0x001421F0
		internal string EvaluateMapViewportMapCoordinateSystemExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapViewport mapViewport, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapViewport.MapCoordinateSystem, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MapCoordinateSystem", out variantResult))
			{
				this.EvaluateComplexExpression(mapViewport.MapCoordinateSystem, ref variantResult, mapViewport.ExprHost, () => mapViewport.ExprHost.MapCoordinateSystemExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x00144060 File Offset: 0x00142260
		internal string EvaluateMapViewportMapProjectionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapViewport mapViewport, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapViewport.MapProjection, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MapProjection", out variantResult))
			{
				this.EvaluateComplexExpression(mapViewport.MapProjection, ref variantResult, mapViewport.ExprHost, () => mapViewport.ExprHost.MapProjectionExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x001440D0 File Offset: 0x001422D0
		internal double EvaluateMapViewportProjectionCenterXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapViewport mapViewport, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapViewport.ProjectionCenterX, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ProjectionCenterX", out variantResult))
			{
				this.EvaluateComplexExpression(mapViewport.ProjectionCenterX, ref variantResult, mapViewport.ExprHost, () => mapViewport.ExprHost.ProjectionCenterXExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x00144140 File Offset: 0x00142340
		internal double EvaluateMapViewportProjectionCenterYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapViewport mapViewport, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapViewport.ProjectionCenterY, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ProjectionCenterY", out variantResult))
			{
				this.EvaluateComplexExpression(mapViewport.ProjectionCenterY, ref variantResult, mapViewport.ExprHost, () => mapViewport.ExprHost.ProjectionCenterYExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x001441B0 File Offset: 0x001423B0
		internal double EvaluateMapViewportMaximumZoomExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapViewport mapViewport, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapViewport.MaximumZoom, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MaximumZoom", out variantResult))
			{
				this.EvaluateComplexExpression(mapViewport.MaximumZoom, ref variantResult, mapViewport.ExprHost, () => mapViewport.ExprHost.MaximumZoomExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x00144220 File Offset: 0x00142420
		internal double EvaluateMapViewportMinimumZoomExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapViewport mapViewport, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapViewport.MinimumZoom, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MinimumZoom", out variantResult))
			{
				this.EvaluateComplexExpression(mapViewport.MinimumZoom, ref variantResult, mapViewport.ExprHost, () => mapViewport.ExprHost.MinimumZoomExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x00144290 File Offset: 0x00142490
		internal double EvaluateMapViewportSimplificationResolutionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapViewport mapViewport, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapViewport.SimplificationResolution, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "SimplificationResolution", out variantResult))
			{
				this.EvaluateComplexExpression(mapViewport.SimplificationResolution, ref variantResult, mapViewport.ExprHost, () => mapViewport.ExprHost.SimplificationResolutionExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x00144300 File Offset: 0x00142500
		internal string EvaluateMapViewportContentMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapViewport mapViewport, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapViewport.ContentMargin, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ContentMargin", out variantResult))
			{
				this.EvaluateComplexExpression(mapViewport.ContentMargin, ref variantResult, mapViewport.ExprHost, () => mapViewport.ExprHost.ContentMarginExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x00144374 File Offset: 0x00142574
		internal bool EvaluateMapViewportGridUnderContentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapViewport mapViewport, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapViewport.GridUnderContent, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "GridUnderContent", out variantResult))
			{
				this.EvaluateComplexExpression(mapViewport.GridUnderContent, ref variantResult, mapViewport.ExprHost, () => mapViewport.ExprHost.GridUnderContentExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x001443E4 File Offset: 0x001425E4
		internal double EvaluateMapLimitsMinimumXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLimits mapLimits, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLimits.MinimumX, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MinimumX", out variantResult))
			{
				this.EvaluateComplexExpression(mapLimits.MinimumX, ref variantResult, mapLimits.ExprHost, () => mapLimits.ExprHost.MinimumXExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E92 RID: 20114 RVA: 0x00144454 File Offset: 0x00142654
		internal double EvaluateMapLimitsMinimumYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLimits mapLimits, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLimits.MinimumY, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MinimumY", out variantResult))
			{
				this.EvaluateComplexExpression(mapLimits.MinimumY, ref variantResult, mapLimits.ExprHost, () => mapLimits.ExprHost.MinimumYExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x001444C4 File Offset: 0x001426C4
		internal double EvaluateMapLimitsMaximumXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLimits mapLimits, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLimits.MaximumX, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MaximumX", out variantResult))
			{
				this.EvaluateComplexExpression(mapLimits.MaximumX, ref variantResult, mapLimits.ExprHost, () => mapLimits.ExprHost.MaximumXExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x00144534 File Offset: 0x00142734
		internal double EvaluateMapLimitsMaximumYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLimits mapLimits, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLimits.MaximumY, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MaximumY", out variantResult))
			{
				this.EvaluateComplexExpression(mapLimits.MaximumY, ref variantResult, mapLimits.ExprHost, () => mapLimits.ExprHost.MaximumYExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x001445A4 File Offset: 0x001427A4
		internal bool EvaluateMapLimitsLimitToDataExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLimits mapLimits, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLimits.LimitToData, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LimitToData", out variantResult))
			{
				this.EvaluateComplexExpression(mapLimits.LimitToData, ref variantResult, mapLimits.ExprHost, () => mapLimits.ExprHost.LimitToDataExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x00144614 File Offset: 0x00142814
		internal string EvaluateMapColorScaleTickMarkLengthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorScale mapColorScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorScale.TickMarkLength, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "TickMarkLength", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorScale.TickMarkLength, ref variantResult, mapColorScale.ExprHost, () => mapColorScale.ExprHost.TickMarkLengthExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x00144688 File Offset: 0x00142888
		internal string EvaluateMapColorScaleColorBarBorderColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorScale mapColorScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorScale.ColorBarBorderColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ColorBarBorderColor", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorScale.ColorBarBorderColor, ref variantResult, mapColorScale.ExprHost, () => mapColorScale.ExprHost.ColorBarBorderColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x001446FC File Offset: 0x001428FC
		internal int EvaluateMapColorScaleLabelIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorScale mapColorScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorScale.LabelInterval, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LabelInterval", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorScale.LabelInterval, ref variantResult, mapColorScale.ExprHost, () => mapColorScale.ExprHost.LabelIntervalExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x0014476C File Offset: 0x0014296C
		internal string EvaluateMapColorScaleLabelFormatExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorScale mapColorScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorScale.LabelFormat, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LabelFormat", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorScale.LabelFormat, ref variantResult, mapColorScale.ExprHost, () => mapColorScale.ExprHost.LabelFormatExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x001447DC File Offset: 0x001429DC
		internal string EvaluateMapColorScaleLabelPlacementExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorScale mapColorScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorScale.LabelPlacement, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LabelPlacement", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorScale.LabelPlacement, ref variantResult, mapColorScale.ExprHost, () => mapColorScale.ExprHost.LabelPlacementExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x0014484C File Offset: 0x00142A4C
		internal string EvaluateMapColorScaleLabelBehaviorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorScale mapColorScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorScale.LabelBehavior, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LabelBehavior", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorScale.LabelBehavior, ref variantResult, mapColorScale.ExprHost, () => mapColorScale.ExprHost.LabelBehaviorExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x001448BC File Offset: 0x00142ABC
		internal bool EvaluateMapColorScaleHideEndLabelsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorScale mapColorScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorScale.HideEndLabels, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "HideEndLabels", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorScale.HideEndLabels, ref variantResult, mapColorScale.ExprHost, () => mapColorScale.ExprHost.HideEndLabelsExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x0014492C File Offset: 0x00142B2C
		internal string EvaluateMapColorScaleRangeGapColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorScale mapColorScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorScale.RangeGapColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "RangeGapColor", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorScale.RangeGapColor, ref variantResult, mapColorScale.ExprHost, () => mapColorScale.ExprHost.RangeGapColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x001449A0 File Offset: 0x00142BA0
		internal string EvaluateMapColorScaleNoDataTextExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorScale mapColorScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorScale.NoDataText, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "NoDataText", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorScale.NoDataText, ref variantResult, mapColorScale.ExprHost, () => mapColorScale.ExprHost.NoDataTextExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x00144A10 File Offset: 0x00142C10
		internal VariantResult EvaluateMapColorScaleTitleCaptionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorScaleTitle mapColorScaleTitle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorScaleTitle.Caption, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Caption", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorScaleTitle.Caption, ref variantResult, mapColorScaleTitle.ExprHost, () => mapColorScaleTitle.ExprHost.CaptionExpr);
			}
			return variantResult;
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x00144A74 File Offset: 0x00142C74
		internal string EvaluateMapDistanceScaleScaleColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapDistanceScale mapDistanceScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapDistanceScale.ScaleColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ScaleColor", out variantResult))
			{
				this.EvaluateComplexExpression(mapDistanceScale.ScaleColor, ref variantResult, mapDistanceScale.ExprHost, () => mapDistanceScale.ExprHost.ScaleColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x00144AE8 File Offset: 0x00142CE8
		internal string EvaluateMapDistanceScaleScaleBorderColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapDistanceScale mapDistanceScale, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapDistanceScale.ScaleBorderColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ScaleBorderColor", out variantResult))
			{
				this.EvaluateComplexExpression(mapDistanceScale.ScaleBorderColor, ref variantResult, mapDistanceScale.ExprHost, () => mapDistanceScale.ExprHost.ScaleBorderColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x00144B5C File Offset: 0x00142D5C
		internal VariantResult EvaluateMapTitleTextExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapTitle mapTitle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapTitle.Text, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Text", out variantResult))
			{
				this.EvaluateComplexExpression(mapTitle.Text, ref variantResult, mapTitle.ExprHost, () => mapTitle.ExprHost.TextExpr);
			}
			return variantResult;
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x00144BC0 File Offset: 0x00142DC0
		internal double EvaluateMapTitleAngleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapTitle mapTitle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapTitle.Angle, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Angle", out variantResult))
			{
				this.EvaluateComplexExpression(mapTitle.Angle, ref variantResult, mapTitle.ExprHost, () => mapTitle.ExprHost.AngleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x00144C30 File Offset: 0x00142E30
		internal string EvaluateMapTitleTextShadowOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapTitle mapTitle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapTitle.TextShadowOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "TextShadowOffset", out variantResult))
			{
				this.EvaluateComplexExpression(mapTitle.TextShadowOffset, ref variantResult, mapTitle.ExprHost, () => mapTitle.ExprHost.TextShadowOffsetExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x00144CA4 File Offset: 0x00142EA4
		internal string EvaluateMapLegendLayoutExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLegend mapLegend, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLegend.Layout, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Layout", out variantResult))
			{
				this.EvaluateComplexExpression(mapLegend.Layout, ref variantResult, mapLegend.ExprHost, () => mapLegend.ExprHost.LayoutExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x00144D14 File Offset: 0x00142F14
		internal bool EvaluateMapLegendAutoFitTextDisabledExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLegend mapLegend, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLegend.AutoFitTextDisabled, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "AutoFitTextDisabled", out variantResult))
			{
				this.EvaluateComplexExpression(mapLegend.AutoFitTextDisabled, ref variantResult, mapLegend.ExprHost, () => mapLegend.ExprHost.AutoFitTextDisabledExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x00144D84 File Offset: 0x00142F84
		internal string EvaluateMapLegendMinFontSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLegend mapLegend, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLegend.MinFontSize, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MinFontSize", out variantResult))
			{
				this.EvaluateComplexExpression(mapLegend.MinFontSize, ref variantResult, mapLegend.ExprHost, () => mapLegend.ExprHost.MinFontSizeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x00144DF8 File Offset: 0x00142FF8
		internal bool EvaluateMapLegendInterlacedRowsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLegend mapLegend, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLegend.InterlacedRows, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "InterlacedRows", out variantResult))
			{
				this.EvaluateComplexExpression(mapLegend.InterlacedRows, ref variantResult, mapLegend.ExprHost, () => mapLegend.ExprHost.InterlacedRowsExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x00144E68 File Offset: 0x00143068
		internal string EvaluateMapLegendInterlacedRowsColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLegend mapLegend, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLegend.InterlacedRowsColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "InterlacedRowsColor", out variantResult))
			{
				this.EvaluateComplexExpression(mapLegend.InterlacedRowsColor, ref variantResult, mapLegend.ExprHost, () => mapLegend.ExprHost.InterlacedRowsColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x00144EDC File Offset: 0x001430DC
		internal bool EvaluateMapLegendEquallySpacedItemsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLegend mapLegend, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLegend.EquallySpacedItems, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "EquallySpacedItems", out variantResult))
			{
				this.EvaluateComplexExpression(mapLegend.EquallySpacedItems, ref variantResult, mapLegend.ExprHost, () => mapLegend.ExprHost.EquallySpacedItemsExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x00144F4C File Offset: 0x0014314C
		internal int EvaluateMapLegendTextWrapThresholdExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLegend mapLegend, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLegend.TextWrapThreshold, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "TextWrapThreshold", out variantResult))
			{
				this.EvaluateComplexExpression(mapLegend.TextWrapThreshold, ref variantResult, mapLegend.ExprHost, () => mapLegend.ExprHost.TextWrapThresholdExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x00144FBC File Offset: 0x001431BC
		internal VariantResult EvaluateMapLegendTitleCaptionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLegendTitle mapLegendTitle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLegendTitle.Caption, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Caption", out variantResult))
			{
				this.EvaluateComplexExpression(mapLegendTitle.Caption, ref variantResult, mapLegendTitle.ExprHost, () => mapLegendTitle.ExprHost.CaptionExpr);
			}
			return variantResult;
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x00145020 File Offset: 0x00143220
		internal string EvaluateMapLegendTitleTitleSeparatorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLegendTitle mapLegendTitle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLegendTitle.TitleSeparator, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "TitleSeparator", out variantResult))
			{
				this.EvaluateComplexExpression(mapLegendTitle.TitleSeparator, ref variantResult, mapLegendTitle.ExprHost, () => mapLegendTitle.ExprHost.TitleSeparatorExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x00145090 File Offset: 0x00143290
		internal string EvaluateMapLegendTitleTitleSeparatorColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLegendTitle mapLegendTitle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLegendTitle.TitleSeparatorColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "TitleSeparatorColor", out variantResult))
			{
				this.EvaluateComplexExpression(mapLegendTitle.TitleSeparatorColor, ref variantResult, mapLegendTitle.ExprHost, () => mapLegendTitle.ExprHost.TitleSeparatorColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x00145104 File Offset: 0x00143304
		internal VariantResult EvaluateMapAppearanceRuleDataValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapAppearanceRule mapAppearanceRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapAppearanceRule.DataValue, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "DataValue", out variantResult))
			{
				if (mapAppearanceRule.ExprHostMapMember != null)
				{
					this.EvaluateComplexExpression(mapAppearanceRule.DataValue, ref variantResult, mapAppearanceRule.ExprHostMapMember, () => mapAppearanceRule.ExprHostMapMember.DataValueExpr);
				}
				else
				{
					this.EvaluateComplexExpression(mapAppearanceRule.DataValue, ref variantResult, mapAppearanceRule.ExprHostMapMember, () => mapAppearanceRule.ExprHost.DataValueExpr);
				}
			}
			this.ProcessVariantResult(mapAppearanceRule.DataValue, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x001451B4 File Offset: 0x001433B4
		internal string EvaluateMapAppearanceRuleDistributionTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapAppearanceRule mapAppearanceRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapAppearanceRule.DistributionType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "DistributionType", out variantResult))
			{
				this.EvaluateComplexExpression(mapAppearanceRule.DistributionType, ref variantResult, mapAppearanceRule.ExprHost, () => mapAppearanceRule.ExprHost.DistributionTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x00145224 File Offset: 0x00143424
		internal int EvaluateMapAppearanceRuleBucketCountExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapAppearanceRule mapAppearanceRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapAppearanceRule.BucketCount, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "BucketCount", out variantResult))
			{
				this.EvaluateComplexExpression(mapAppearanceRule.BucketCount, ref variantResult, mapAppearanceRule.ExprHost, () => mapAppearanceRule.ExprHost.BucketCountExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x00145294 File Offset: 0x00143494
		internal VariantResult EvaluateMapAppearanceRuleStartValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapAppearanceRule mapAppearanceRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapAppearanceRule.StartValue, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "StartValue", out variantResult))
			{
				this.EvaluateComplexExpression(mapAppearanceRule.StartValue, ref variantResult, mapAppearanceRule.ExprHost, () => mapAppearanceRule.ExprHost.StartValueExpr);
			}
			this.ProcessVariantResult(mapAppearanceRule.StartValue, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x0014530C File Offset: 0x0014350C
		internal VariantResult EvaluateMapAppearanceRuleEndValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapAppearanceRule mapAppearanceRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapAppearanceRule.EndValue, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "EndValue", out variantResult))
			{
				this.EvaluateComplexExpression(mapAppearanceRule.EndValue, ref variantResult, mapAppearanceRule.ExprHost, () => mapAppearanceRule.ExprHost.EndValueExpr);
			}
			this.ProcessVariantResult(mapAppearanceRule.EndValue, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x00145384 File Offset: 0x00143584
		internal VariantResult EvaluateMapAppearanceRuleLegendTextExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapAppearanceRule mapAppearanceRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapAppearanceRule.LegendText, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LegendText", out variantResult))
			{
				this.EvaluateComplexExpression(mapAppearanceRule.LegendText, ref variantResult, mapAppearanceRule.ExprHost, () => mapAppearanceRule.ExprHost.LegendTextExpr);
			}
			return variantResult;
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x001453E8 File Offset: 0x001435E8
		internal VariantResult EvaluateMapBucketStartValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapBucket mapBucket, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapBucket.StartValue, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "StartValue", out variantResult))
			{
				this.EvaluateComplexExpression(mapBucket.StartValue, ref variantResult, mapBucket.ExprHost, () => mapBucket.ExprHost.StartValueExpr);
			}
			this.ProcessVariantResult(mapBucket.StartValue, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x00145460 File Offset: 0x00143660
		internal VariantResult EvaluateMapBucketEndValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapBucket mapBucket, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapBucket.EndValue, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "EndValue", out variantResult))
			{
				this.EvaluateComplexExpression(mapBucket.EndValue, ref variantResult, mapBucket.ExprHost, () => mapBucket.ExprHost.EndValueExpr);
			}
			this.ProcessVariantResult(mapBucket.EndValue, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x001454D8 File Offset: 0x001436D8
		internal string EvaluateMapColorPaletteRulePaletteExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorPaletteRule mapColorPaletteRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorPaletteRule.Palette, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Palette", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorPaletteRule.Palette, ref variantResult, mapColorPaletteRule.ExprHost, () => mapColorPaletteRule.ExprHost.PaletteExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x00145548 File Offset: 0x00143748
		internal string EvaluateMapColorRangeRuleStartColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorRangeRule mapColorRangeRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorRangeRule.StartColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "StartColor", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorRangeRule.StartColor, ref variantResult, mapColorRangeRule.ExprHost, () => mapColorRangeRule.ExprHost.StartColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x001455BC File Offset: 0x001437BC
		internal string EvaluateMapColorRangeRuleMiddleColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorRangeRule mapColorRangeRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorRangeRule.MiddleColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MiddleColor", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorRangeRule.MiddleColor, ref variantResult, mapColorRangeRule.ExprHost, () => mapColorRangeRule.ExprHost.MiddleColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x00145630 File Offset: 0x00143830
		internal string EvaluateMapColorRangeRuleEndColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorRangeRule mapColorRangeRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorRangeRule.EndColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "EndColor", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorRangeRule.EndColor, ref variantResult, mapColorRangeRule.ExprHost, () => mapColorRangeRule.ExprHost.EndColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x001456A4 File Offset: 0x001438A4
		internal bool EvaluateMapColorRuleShowInColorScaleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapColorRule mapColorRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapColorRule.ShowInColorScale, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ShowInColorScale", out variantResult))
			{
				this.EvaluateComplexExpression(mapColorRule.ShowInColorScale, ref variantResult, mapColorRule.ExprHost, () => mapColorRule.ExprHost.ShowInColorScaleExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x00145714 File Offset: 0x00143914
		internal string EvaluateMapSizeRuleStartSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSizeRule mapSizeRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSizeRule.StartSize, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "StartSize", out variantResult))
			{
				this.EvaluateComplexExpression(mapSizeRule.StartSize, ref variantResult, mapSizeRule.ExprHost, () => mapSizeRule.ExprHost.StartSizeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x00145788 File Offset: 0x00143988
		internal string EvaluateMapSizeRuleEndSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSizeRule mapSizeRule, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSizeRule.EndSize, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "EndSize", out variantResult))
			{
				this.EvaluateComplexExpression(mapSizeRule.EndSize, ref variantResult, mapSizeRule.ExprHost, () => mapSizeRule.ExprHost.EndSizeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x001457FC File Offset: 0x001439FC
		internal string EvaluateMapMarkerImageSourceExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapMarkerImage mapMarkerImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapMarkerImage.Source, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Source", out variantResult))
			{
				this.EvaluateComplexExpression(mapMarkerImage.Source, ref variantResult, mapMarkerImage.ExprHost, () => mapMarkerImage.ExprHost.SourceExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x0014586C File Offset: 0x00143A6C
		internal VariantResult EvaluateMapMarkerImageValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapMarkerImage mapMarkerImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapMarkerImage.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(mapMarkerImage.Value, ref variantResult, mapMarkerImage.ExprHost, () => mapMarkerImage.ExprHost.ValueExpr);
			}
			this.ProcessVariantResult(mapMarkerImage.Value, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x001458E4 File Offset: 0x00143AE4
		internal string EvaluateMapMarkerImageStringValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapMarkerImage mapMarkerImage, string objectName, out bool errorOccurred)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapMarkerImage.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(mapMarkerImage.Value, ref variantResult, mapMarkerImage.ExprHost, () => mapMarkerImage.ExprHost.ValueExpr);
			}
			StringResult stringResult = this.ProcessStringResult(variantResult);
			errorOccurred = stringResult.ErrorOccurred;
			return stringResult.Value;
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x0014595C File Offset: 0x00143B5C
		internal byte[] EvaluateMapMarkerImageBinaryValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapMarkerImage mapMarkerImage, string objectName, out bool errorOccurred)
		{
			VariantResult variantResult;
			if (!this.EvaluateBinaryExpression(mapMarkerImage.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(mapMarkerImage.Value, ref variantResult, mapMarkerImage.ExprHost, () => mapMarkerImage.ExprHost.ValueExpr);
			}
			BinaryResult binaryResult = this.ProcessBinaryResult(variantResult);
			errorOccurred = binaryResult.ErrorOccurred;
			return binaryResult.Value;
		}

		// Token: 0x06004EC2 RID: 20162 RVA: 0x001459D4 File Offset: 0x00143BD4
		internal string EvaluateMapMarkerImageMIMETypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapMarkerImage mapMarkerImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapMarkerImage.MIMEType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MIMEType", out variantResult))
			{
				this.EvaluateComplexExpression(mapMarkerImage.MIMEType, ref variantResult, mapMarkerImage.ExprHost, () => mapMarkerImage.ExprHost.MIMETypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EC3 RID: 20163 RVA: 0x00145A44 File Offset: 0x00143C44
		internal string EvaluateMapMarkerImageTransparentColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapMarkerImage mapMarkerImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapMarkerImage.TransparentColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "TransparentColor", out variantResult))
			{
				this.EvaluateComplexExpression(mapMarkerImage.TransparentColor, ref variantResult, mapMarkerImage.ExprHost, () => mapMarkerImage.ExprHost.TransparentColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004EC4 RID: 20164 RVA: 0x00145AB8 File Offset: 0x00143CB8
		internal string EvaluateMapMarkerImageResizeModeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapMarkerImage mapMarkerImage, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapMarkerImage.ResizeMode, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ResizeMode", out variantResult))
			{
				this.EvaluateComplexExpression(mapMarkerImage.ResizeMode, ref variantResult, mapMarkerImage.ExprHost, () => mapMarkerImage.ExprHost.ResizeModeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x00145B28 File Offset: 0x00143D28
		internal string EvaluateMapMarkerMapMarkerStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapMarker mapMarker, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapMarker.MapMarkerStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MapMarkerStyle", out variantResult))
			{
				this.EvaluateComplexExpression(mapMarker.MapMarkerStyle, ref variantResult, mapMarker.ExprHost, () => mapMarker.ExprHost.MapMarkerStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x00145B98 File Offset: 0x00143D98
		internal string EvaluateMapCustomColorColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapCustomColor mapCustomColor, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapCustomColor.Color, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Color", out variantResult))
			{
				this.EvaluateComplexExpression(mapCustomColor.Color, ref variantResult, mapCustomColor.ExprHost, () => mapCustomColor.ExprHost.ColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x00145C0C File Offset: 0x00143E0C
		internal string EvaluateMapLineTemplateWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLineTemplate mapLineTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLineTemplate.Width, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Width", out variantResult))
			{
				this.EvaluateComplexExpression(mapLineTemplate.Width, ref variantResult, mapLineTemplate.ExprHost, () => mapLineTemplate.ExprHost.WidthExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x00145C80 File Offset: 0x00143E80
		internal string EvaluateMapLineTemplateLabelPlacementExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLineTemplate mapLineTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLineTemplate.LabelPlacement, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LabelPlacement", out variantResult))
			{
				this.EvaluateComplexExpression(mapLineTemplate.LabelPlacement, ref variantResult, mapLineTemplate.ExprHost, () => mapLineTemplate.ExprHost.LabelPlacementExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x00145CF0 File Offset: 0x00143EF0
		internal double EvaluateMapPolygonTemplateScaleFactorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapPolygonTemplate mapPolygonTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapPolygonTemplate.ScaleFactor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ScaleFactor", out variantResult))
			{
				this.EvaluateComplexExpression(mapPolygonTemplate.ScaleFactor, ref variantResult, mapPolygonTemplate.ExprHost, () => mapPolygonTemplate.ExprHost.ScaleFactorExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x00145D60 File Offset: 0x00143F60
		internal double EvaluateMapPolygonTemplateCenterPointOffsetXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapPolygonTemplate mapPolygonTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapPolygonTemplate.CenterPointOffsetX, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "CenterPointOffsetX", out variantResult))
			{
				this.EvaluateComplexExpression(mapPolygonTemplate.CenterPointOffsetX, ref variantResult, mapPolygonTemplate.ExprHost, () => mapPolygonTemplate.ExprHost.CenterPointOffsetXExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x00145DD0 File Offset: 0x00143FD0
		internal double EvaluateMapPolygonTemplateCenterPointOffsetYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapPolygonTemplate mapPolygonTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapPolygonTemplate.CenterPointOffsetY, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "CenterPointOffsetY", out variantResult))
			{
				this.EvaluateComplexExpression(mapPolygonTemplate.CenterPointOffsetY, ref variantResult, mapPolygonTemplate.ExprHost, () => mapPolygonTemplate.ExprHost.CenterPointOffsetYExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x00145E40 File Offset: 0x00144040
		internal string EvaluateMapPolygonTemplateShowLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapPolygonTemplate mapPolygonTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapPolygonTemplate.ShowLabel, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ShowLabel", out variantResult))
			{
				this.EvaluateComplexExpression(mapPolygonTemplate.ShowLabel, ref variantResult, mapPolygonTemplate.ExprHost, () => mapPolygonTemplate.ExprHost.ShowLabelExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x00145EB0 File Offset: 0x001440B0
		internal string EvaluateMapPolygonTemplateLabelPlacementExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapPolygonTemplate mapPolygonTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapPolygonTemplate.LabelPlacement, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LabelPlacement", out variantResult))
			{
				this.EvaluateComplexExpression(mapPolygonTemplate.LabelPlacement, ref variantResult, mapPolygonTemplate.ExprHost, () => mapPolygonTemplate.ExprHost.LabelPlacementExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x00145F20 File Offset: 0x00144120
		internal bool EvaluateMapSpatialElementTemplateHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSpatialElementTemplate mapSpatialElementTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSpatialElementTemplate.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(mapSpatialElementTemplate.Hidden, ref variantResult, mapSpatialElementTemplate.ExprHost, () => mapSpatialElementTemplate.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x00145F90 File Offset: 0x00144190
		internal double EvaluateMapSpatialElementTemplateOffsetXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSpatialElementTemplate mapSpatialElementTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSpatialElementTemplate.OffsetX, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "OffsetX", out variantResult))
			{
				this.EvaluateComplexExpression(mapSpatialElementTemplate.OffsetX, ref variantResult, mapSpatialElementTemplate.ExprHost, () => mapSpatialElementTemplate.ExprHost.OffsetXExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x00146000 File Offset: 0x00144200
		internal double EvaluateMapSpatialElementTemplateOffsetYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSpatialElementTemplate mapSpatialElementTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSpatialElementTemplate.OffsetY, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "OffsetY", out variantResult))
			{
				this.EvaluateComplexExpression(mapSpatialElementTemplate.OffsetY, ref variantResult, mapSpatialElementTemplate.ExprHost, () => mapSpatialElementTemplate.ExprHost.OffsetYExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x00146070 File Offset: 0x00144270
		internal VariantResult EvaluateMapSpatialElementTemplateLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSpatialElementTemplate mapSpatialElementTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSpatialElementTemplate.Label, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Label", out variantResult))
			{
				this.EvaluateComplexExpression(mapSpatialElementTemplate.Label, ref variantResult, mapSpatialElementTemplate.ExprHost, () => mapSpatialElementTemplate.ExprHost.LabelExpr);
			}
			return variantResult;
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x001460D4 File Offset: 0x001442D4
		internal VariantResult EvaluateMapSpatialElementTemplateDataElementLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSpatialElementTemplate mapSpatialElementTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSpatialElementTemplate.DataElementLabel, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "DataElementLabel", out variantResult))
			{
				this.EvaluateComplexExpression(mapSpatialElementTemplate.DataElementLabel, ref variantResult, mapSpatialElementTemplate.ExprHost, () => mapSpatialElementTemplate.ExprHost.DataElementLabelExpr);
			}
			return variantResult;
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x00146138 File Offset: 0x00144338
		internal VariantResult EvaluateMapSpatialElementTemplateToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSpatialElementTemplate mapSpatialElementTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSpatialElementTemplate.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(mapSpatialElementTemplate.ToolTip, ref variantResult, mapSpatialElementTemplate.ExprHost, () => mapSpatialElementTemplate.ExprHost.ToolTipExpr);
			}
			return variantResult;
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x0014619C File Offset: 0x0014439C
		internal string EvaluateMapPointTemplateSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapPointTemplate mapPointTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapPointTemplate.Size, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Size", out variantResult))
			{
				this.EvaluateComplexExpression(mapPointTemplate.Size, ref variantResult, mapPointTemplate.ExprHost, () => mapPointTemplate.ExprHost.SizeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x00146210 File Offset: 0x00144410
		internal string EvaluateMapPointTemplateLabelPlacementExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapPointTemplate mapPointTemplate, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapPointTemplate.LabelPlacement, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LabelPlacement", out variantResult))
			{
				this.EvaluateComplexExpression(mapPointTemplate.LabelPlacement, ref variantResult, mapPointTemplate.ExprHost, () => mapPointTemplate.ExprHost.LabelPlacementExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x00146280 File Offset: 0x00144480
		internal bool EvaluateMapLineUseCustomLineTemplateExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLine mapLine, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLine.UseCustomLineTemplate, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "UseCustomLineTemplate", out variantResult))
			{
				this.EvaluateComplexExpression(mapLine.UseCustomLineTemplate, ref variantResult, mapLine.ExprHost, () => mapLine.ExprHost.UseCustomLineTemplateExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x001462F0 File Offset: 0x001444F0
		internal bool EvaluateMapPolygonUseCustomPolygonTemplateExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapPolygon mapPolygon, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapPolygon.UseCustomPolygonTemplate, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "UseCustomPolygonTemplate", out variantResult))
			{
				this.EvaluateComplexExpression(mapPolygon.UseCustomPolygonTemplate, ref variantResult, mapPolygon.ExprHost, () => mapPolygon.ExprHost.UseCustomPolygonTemplateExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x00146360 File Offset: 0x00144560
		internal bool EvaluateMapPolygonUseCustomPointTemplateExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapPolygon mapPolygon, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapPolygon.UseCustomCenterPointTemplate, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "UseCustomCenterPointTemplate", out variantResult))
			{
				this.EvaluateComplexExpression(mapPolygon.UseCustomCenterPointTemplate, ref variantResult, mapPolygon.ExprHost, () => mapPolygon.ExprHost.UseCustomPointTemplateExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x001463D0 File Offset: 0x001445D0
		internal bool EvaluateMapPointUseCustomPointTemplateExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapPoint mapPoint, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapPoint.UseCustomPointTemplate, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "UseCustomPointTemplate", out variantResult))
			{
				this.EvaluateComplexExpression(mapPoint.UseCustomPointTemplate, ref variantResult, mapPoint.ExprHost, () => mapPoint.ExprHost.UseCustomPointTemplateExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x00146440 File Offset: 0x00144640
		internal string EvaluateMapFieldNameNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapFieldName mapFieldName, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapFieldName.Name, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Name", out variantResult))
			{
				this.EvaluateComplexExpression(mapFieldName.Name, ref variantResult, mapFieldName.ExprHost, () => mapFieldName.ExprHost.NameExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x001464B0 File Offset: 0x001446B0
		internal string EvaluateMapLayerVisibilityModeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLayer mapLayer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLayer.VisibilityMode, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "VisibilityMode", out variantResult))
			{
				this.EvaluateComplexExpression(mapLayer.VisibilityMode, ref variantResult, mapLayer.ExprHost, () => mapLayer.ExprHost.VisibilityModeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x00146520 File Offset: 0x00144720
		internal double EvaluateMapLayerMinimumZoomExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLayer mapLayer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLayer.MinimumZoom, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MinimumZoom", out variantResult))
			{
				this.EvaluateComplexExpression(mapLayer.MinimumZoom, ref variantResult, mapLayer.ExprHost, () => mapLayer.ExprHost.MinimumZoomExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x00146590 File Offset: 0x00144790
		internal double EvaluateMapLayerMaximumZoomExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLayer mapLayer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLayer.MaximumZoom, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MaximumZoom", out variantResult))
			{
				this.EvaluateComplexExpression(mapLayer.MaximumZoom, ref variantResult, mapLayer.ExprHost, () => mapLayer.ExprHost.MaximumZoomExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x00146600 File Offset: 0x00144800
		internal double EvaluateMapLayerTransparencyExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapLayer mapLayer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapLayer.Transparency, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Transparency", out variantResult))
			{
				this.EvaluateComplexExpression(mapLayer.Transparency, ref variantResult, mapLayer.ExprHost, () => mapLayer.ExprHost.TransparencyExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x00146670 File Offset: 0x00144870
		internal string EvaluateMapShapefileSourceExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapShapefile mapShapefile, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapShapefile.Source, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Source", out variantResult))
			{
				this.EvaluateComplexExpression(mapShapefile.Source, ref variantResult, mapShapefile.ExprHost, () => mapShapefile.ExprHost.SourceExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x001466E0 File Offset: 0x001448E0
		internal VariantResult EvaluateMapSpatialDataRegionVectorDataExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSpatialDataRegion mapSpatialDataRegion, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSpatialDataRegion.VectorData, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "VectorData", out variantResult))
			{
				this.EvaluateComplexExpression(mapSpatialDataRegion.VectorData, ref variantResult, mapSpatialDataRegion.ExprHost, () => mapSpatialDataRegion.ExprHost.VectorDataExpr);
			}
			this.ProcessVariantResult(mapSpatialDataRegion.VectorData, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x00146758 File Offset: 0x00144958
		internal string EvaluateMapSpatialDataSetDataSetNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSpatialDataSet mapSpatialDataSet, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSpatialDataSet.DataSetName, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "DataSetName", out variantResult))
			{
				this.EvaluateComplexExpression(mapSpatialDataSet.DataSetName, ref variantResult, mapSpatialDataSet.ExprHost, () => mapSpatialDataSet.ExprHost.DataSetNameExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x001467C8 File Offset: 0x001449C8
		internal string EvaluateMapSpatialDataSetSpatialFieldExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapSpatialDataSet mapSpatialDataSet, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapSpatialDataSet.SpatialField, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "SpatialField", out variantResult))
			{
				this.EvaluateComplexExpression(mapSpatialDataSet.SpatialField, ref variantResult, mapSpatialDataSet.ExprHost, () => mapSpatialDataSet.ExprHost.SpatialFieldExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x00146838 File Offset: 0x00144A38
		internal string EvaluateMapTileLayerServiceUrlExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapTileLayer mapTileLayer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapTileLayer.ServiceUrl, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ServiceUrl", out variantResult))
			{
				this.EvaluateComplexExpression(mapTileLayer.ServiceUrl, ref variantResult, mapTileLayer.ExprHost, () => mapTileLayer.ExprHost.ServiceUrlExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x001468A8 File Offset: 0x00144AA8
		internal string EvaluateMapTileLayerTileStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapTileLayer mapTileLayer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapTileLayer.TileStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "TileStyle", out variantResult))
			{
				this.EvaluateComplexExpression(mapTileLayer.TileStyle, ref variantResult, mapTileLayer.ExprHost, () => mapTileLayer.ExprHost.TileStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x00146918 File Offset: 0x00144B18
		internal bool EvaluateMapTileLayerUseSecureConnectionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapTileLayer mapTileLayer, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapTileLayer.UseSecureConnection, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "UseSecureConnection", out variantResult))
			{
				this.EvaluateComplexExpression(mapTileLayer.UseSecureConnection, ref variantResult, mapTileLayer.ExprHost, () => mapTileLayer.ExprHost.UseSecureConnectionExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x00146988 File Offset: 0x00144B88
		internal string EvaluateMapAntiAliasingExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Map map, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(map.AntiAliasing, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "AntiAliasing", out variantResult))
			{
				this.EvaluateComplexExpression(map.AntiAliasing, ref variantResult, map.MapExprHost, () => map.MapExprHost.AntiAliasingExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x001469F8 File Offset: 0x00144BF8
		internal string EvaluateMapTextAntiAliasingQualityExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Map map, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(map.TextAntiAliasingQuality, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "TextAntiAliasingQuality", out variantResult))
			{
				this.EvaluateComplexExpression(map.TextAntiAliasingQuality, ref variantResult, map.MapExprHost, () => map.MapExprHost.TextAntiAliasingQualityExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x00146A68 File Offset: 0x00144C68
		internal double EvaluateMapShadowIntensityExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Map map, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(map.ShadowIntensity, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "ShadowIntensity", out variantResult))
			{
				this.EvaluateComplexExpression(map.ShadowIntensity, ref variantResult, map.MapExprHost, () => map.MapExprHost.ShadowIntensityExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x00146AD8 File Offset: 0x00144CD8
		internal string EvaluateMapTileLanguageExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Map map, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(map.TileLanguage, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "TileLanguage", out variantResult))
			{
				this.EvaluateComplexExpression(map.TileLanguage, ref variantResult, map.MapExprHost, () => map.MapExprHost.TileLanguageExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x00146B48 File Offset: 0x00144D48
		internal string EvaluateMapBorderSkinMapBorderSkinTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapBorderSkin mapBorderSkin, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapBorderSkin.MapBorderSkinType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "MapBorderSkinType", out variantResult))
			{
				this.EvaluateComplexExpression(mapBorderSkin.MapBorderSkinType, ref variantResult, mapBorderSkin.ExprHost, () => mapBorderSkin.ExprHost.MapBorderSkinTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x00146BB8 File Offset: 0x00144DB8
		internal double EvaluateMapCustomViewCenterXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapCustomView mapCustomView, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapCustomView.CenterX, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "CenterX", out variantResult))
			{
				this.EvaluateComplexExpression(mapCustomView.CenterX, ref variantResult, mapCustomView.ExprHost, () => mapCustomView.ExprHost.CenterXExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x00146C28 File Offset: 0x00144E28
		internal double EvaluateMapCustomViewCenterYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapCustomView mapCustomView, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapCustomView.CenterY, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "CenterY", out variantResult))
			{
				this.EvaluateComplexExpression(mapCustomView.CenterY, ref variantResult, mapCustomView.ExprHost, () => mapCustomView.ExprHost.CenterYExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x00146C98 File Offset: 0x00144E98
		internal string EvaluateMapElementViewLayerNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapElementView mapElementView, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapElementView.LayerName, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "LayerName", out variantResult))
			{
				this.EvaluateComplexExpression(mapElementView.LayerName, ref variantResult, mapElementView.ExprHost, () => mapElementView.ExprHost.LayerNameExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x00146D08 File Offset: 0x00144F08
		internal double EvaluateMapViewZoomExpression(Microsoft.ReportingServices.ReportIntermediateFormat.MapView mapView, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(mapView.Zoom, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "Zoom", out variantResult))
			{
				this.EvaluateComplexExpression(mapView.Zoom, ref variantResult, mapView.ExprHost, () => mapView.ExprHost.ZoomExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x00146D78 File Offset: 0x00144F78
		internal string EvaluateMapPageNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Map map, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, string objectName)
		{
			bool isUnrestrictedRenderFormatReferenceMode = this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode;
			this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = false;
			string value;
			try
			{
				VariantResult variantResult;
				if (!this.EvaluateSimpleExpression(expression, Microsoft.ReportingServices.ReportProcessing.ObjectType.Map, objectName, "PageName", out variantResult))
				{
					this.EvaluateComplexExpression(expression, ref variantResult, map.ExprHost, () => map.ExprHost.PageNameExpr);
				}
				value = this.ProcessStringResult(variantResult, true).Value;
			}
			finally
			{
				this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = isUnrestrictedRenderFormatReferenceMode;
			}
			return value;
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x00146E1C File Offset: 0x0014501C
		internal ReportRuntime(Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel.ObjectModelImpl reportObjectModel, ErrorContext errorContext)
		{
			this.m_objectType = objectType;
			this.m_reportObjectModel = reportObjectModel;
			this.m_errorContext = errorContext;
			if (reportObjectModel.OdpContext.IsRdlSandboxingEnabled())
			{
				this.m_rdlSandboxingEnabled = true;
				IRdlSandboxConfig rdlSandboxing = reportObjectModel.OdpContext.Configuration.RdlSandboxing;
				this.m_maxStringResultLength = rdlSandboxing.MaxStringResultLength;
				this.m_maxArrayResultLength = rdlSandboxing.MaxArrayResultLength;
			}
			if (reportObjectModel.OdpContext.Configuration != null)
			{
				this.m_isSerializableValuesProhibited = reportObjectModel.OdpContext.Configuration.ProhibitSerializableValues;
				this.m_useSafeExpressions = reportObjectModel.OdpContext.Configuration.UseSafeExpressionsRuntime;
				if (this.m_useSafeExpressions)
				{
					RSTrace.SanitizedRdlEngineHostTracer.Trace(TraceLevel.Info, "Using SafeExpressionsRuntime");
					this.m_safeExpressionRuntime = new SafeExpressionsRuntime(reportObjectModel);
				}
			}
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x00146EF7 File Offset: 0x001450F7
		internal ReportRuntime(Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel.ObjectModelImpl reportObjectModel, ErrorContext errorContext, ReportExprHost copyReportExprHost, ReportRuntime topLevelReportRuntime)
			: this(objectType, reportObjectModel, errorContext)
		{
			this.m_reportExprHost = copyReportExprHost;
			this.m_topLevelReportRuntime = topLevelReportRuntime;
		}

		// Token: 0x17001E0F RID: 7695
		// (get) Token: 0x06004EF2 RID: 20210 RVA: 0x00146F12 File Offset: 0x00145112
		internal ReportExprHost ReportExprHost
		{
			get
			{
				return this.m_reportExprHost;
			}
		}

		// Token: 0x17001E10 RID: 7696
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x00146F1A File Offset: 0x0014511A
		// (set) Token: 0x06004EF4 RID: 20212 RVA: 0x00146F22 File Offset: 0x00145122
		internal bool VariableReferenceMode
		{
			get
			{
				return this.m_variableReferenceMode;
			}
			set
			{
				this.m_variableReferenceMode = value;
			}
		}

		// Token: 0x17001E11 RID: 7697
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x00146F2B File Offset: 0x0014512B
		// (set) Token: 0x06004EF6 RID: 20214 RVA: 0x00146F33 File Offset: 0x00145133
		internal bool UnfulfilledDependency
		{
			get
			{
				return this.m_unfulfilledDependency;
			}
			set
			{
				this.m_unfulfilledDependency = value;
			}
		}

		// Token: 0x17001E12 RID: 7698
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x00146F3C File Offset: 0x0014513C
		// (set) Token: 0x06004EF8 RID: 20216 RVA: 0x00146F44 File Offset: 0x00145144
		internal bool ContextUpdated
		{
			get
			{
				return this.m_contextUpdated;
			}
			set
			{
				this.m_contextUpdated = value;
			}
		}

		// Token: 0x17001E13 RID: 7699
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x00146F4D File Offset: 0x0014514D
		// (set) Token: 0x06004EFA RID: 20218 RVA: 0x00146F55 File Offset: 0x00145155
		internal IScope CurrentScope
		{
			get
			{
				return this.m_currentScope;
			}
			set
			{
				this.m_currentScope = value;
			}
		}

		// Token: 0x17001E14 RID: 7700
		// (get) Token: 0x06004EFB RID: 20219 RVA: 0x00146F5E File Offset: 0x0014515E
		// (set) Token: 0x06004EFC RID: 20220 RVA: 0x00146F66 File Offset: 0x00145166
		internal Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
			set
			{
				this.m_objectType = value;
			}
		}

		// Token: 0x17001E15 RID: 7701
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x00146F6F File Offset: 0x0014516F
		// (set) Token: 0x06004EFE RID: 20222 RVA: 0x00146F77 File Offset: 0x00145177
		internal string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
			set
			{
				this.m_objectName = value;
			}
		}

		// Token: 0x17001E16 RID: 7702
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x00146F80 File Offset: 0x00145180
		// (set) Token: 0x06004F00 RID: 20224 RVA: 0x00146F88 File Offset: 0x00145188
		internal string PropertyName
		{
			get
			{
				return this.m_propertyName;
			}
			set
			{
				this.m_propertyName = value;
			}
		}

		// Token: 0x17001E17 RID: 7703
		// (get) Token: 0x06004F01 RID: 20225 RVA: 0x00146F91 File Offset: 0x00145191
		internal Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel.ObjectModelImpl ReportObjectModel
		{
			get
			{
				return this.m_reportObjectModel;
			}
		}

		// Token: 0x17001E18 RID: 7704
		// (get) Token: 0x06004F02 RID: 20226 RVA: 0x00146F99 File Offset: 0x00145199
		internal ErrorContext RuntimeErrorContext
		{
			get
			{
				return this.m_errorContext;
			}
		}

		// Token: 0x17001E19 RID: 7705
		// (get) Token: 0x06004F03 RID: 20227 RVA: 0x00146FA1 File Offset: 0x001451A1
		// (set) Token: 0x06004F04 RID: 20228 RVA: 0x00146FA9 File Offset: 0x001451A9
		internal List<string> FieldsUsedInCurrentActionOwnerValue
		{
			get
			{
				return this.m_fieldsUsedInCurrentActionOwnerValue;
			}
			set
			{
				this.m_fieldsUsedInCurrentActionOwnerValue = value;
			}
		}

		// Token: 0x17001E1A RID: 7706
		// (get) Token: 0x06004F05 RID: 20229 RVA: 0x00146FB2 File Offset: 0x001451B2
		internal SafeExpressionsComparator SafeExpressionsComparator
		{
			get
			{
				if (this.m_safeExpressionsComparator == null)
				{
					this.m_safeExpressionsComparator = new SafeExpressionsComparator(new SafeExpressionsReportContext(this.m_reportObjectModel));
				}
				return this.m_safeExpressionsComparator;
			}
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x00146FD8 File Offset: 0x001451D8
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, params string[] arguments)
		{
			if (this.m_delayedErrorContext == null)
			{
				this.m_errorContext.Register(code, severity, this.m_objectType, this.m_objectName, this.m_propertyName, arguments);
				return;
			}
			this.m_delayedErrorContext.Register(code, severity, arguments);
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x00147012 File Offset: 0x00145212
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, params string[] arguments)
		{
			if (this.m_delayedErrorContext == null)
			{
				this.m_errorContext.Register(code, severity, objectType, objectName, propertyName, arguments);
				return;
			}
			this.m_delayedErrorContext.Register(code, severity, objectType, objectName, propertyName, arguments);
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x00147046 File Offset: 0x00145246
		internal static string GetErrorName(DataFieldStatus status, string exceptionMessage)
		{
			if (exceptionMessage != null)
			{
				return exceptionMessage;
			}
			if (status == DataFieldStatus.Overflow)
			{
				return "OverflowException.";
			}
			if (status == DataFieldStatus.UnSupportedDataType)
			{
				return "UnsupportedDatatypeException.";
			}
			if (status != DataFieldStatus.IsError)
			{
				return null;
			}
			return "FieldValueException.";
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x00147070 File Offset: 0x00145270
		internal string EvaluateReportLanguageExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, out CultureInfo language)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(report.Language, report.ObjectType, report.Name, "Language", out variantResult))
			{
				this.EvaluateComplexExpression(report.Language, ref variantResult, report.ReportExprHost, () => report.ReportExprHost.ReportLanguageExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSpecificLanguage(this.ProcessStringResult(variantResult).Value, this, out language);
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x001470F8 File Offset: 0x001452F8
		internal int EvaluateReportAutoRefreshExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Report report)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(report.AutoRefreshExpression, report.ObjectType, report.Name, "AutoRefresh", out variantResult))
			{
				this.EvaluateComplexExpression(report.AutoRefreshExpression, ref variantResult, report.ReportExprHost, () => report.ReportExprHost.AutoRefreshExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x00147178 File Offset: 0x00145378
		internal string EvaluateInitialPageNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Report report)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(report.InitialPageName, report.ObjectType, report.Name, "InitialPageName", out variantResult))
			{
				this.EvaluateComplexExpression(report.InitialPageName, ref variantResult, report.ReportExprHost, () => report.ReportExprHost.InitialPageNameExpr);
			}
			return this.ProcessStringResult(variantResult, true).Value;
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x001471FC File Offset: 0x001453FC
		internal string EvaluateParamPrompt(Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef paramDef)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paramDef.PromptExpression, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, paramDef.Name, "Prompt", out variantResult))
			{
				this.EvaluateComplexExpression(paramDef.PromptExpression, ref variantResult, paramDef.ExprHost, () => paramDef.ExprHost.PromptExpr);
			}
			return this.ProcessAutocastStringResult(variantResult).Value;
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x00147274 File Offset: 0x00145474
		internal VariantResult EvaluateParamDefaultValue(Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef paramDef, int index)
		{
			Global.Tracer.Assert(paramDef.DefaultExpressions != null, "(paramDef.DefaultExpressions != null)");
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo defaultExpression = paramDef.DefaultExpressions[index];
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(defaultExpression, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, paramDef.Name, "DefaultValue", out variantResult))
			{
				this.EvaluateComplexExpressionForParams(defaultExpression, ref variantResult, paramDef.ExprHost, paramDef.UsedInQuery, () => paramDef.ExprHost[defaultExpression.ExprHostID]);
			}
			this.ProcessReportParameterVariantResult(defaultExpression, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x00147324 File Offset: 0x00145524
		internal VariantResult EvaluateParamValidValue(Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef paramDef, int index)
		{
			Global.Tracer.Assert(paramDef.ValidValuesValueExpressions != null, "(paramDef.ValidValuesValueExpressions != null)");
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression = paramDef.ValidValuesValueExpressions[index];
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, paramDef.Name, "ValidValue", out variantResult))
			{
				this.EvaluateComplexExpressionForParams(expression, ref variantResult, paramDef.ExprHost.ValidValuesHost, paramDef.UsedInQuery, delegate
				{
					Global.Tracer.Assert(paramDef.ExprHost.ValidValuesHost != null);
					return paramDef.ExprHost.ValidValuesHost[expression.ExprHostID];
				});
			}
			this.ProcessReportParameterVariantResult(expression, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x001473D8 File Offset: 0x001455D8
		internal VariantResult EvaluateParamValidValueLabel(Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef paramDef, int index)
		{
			Global.Tracer.Assert(paramDef.ValidValuesLabelExpressions != null, "(paramDef.ValidValuesLabelExpressions != null)");
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression = paramDef.ValidValuesLabelExpressions[index];
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, paramDef.Name, "Label", out variantResult))
			{
				this.EvaluateComplexExpressionForParams(expression, ref variantResult, paramDef.ExprHost.ValidValueLabelsHost, paramDef.UsedInQuery, delegate
				{
					Global.Tracer.Assert(paramDef.ExprHost.ValidValueLabelsHost != null);
					return paramDef.ExprHost.ValidValueLabelsHost[expression.ExprHostID];
				});
			}
			this.ProcessReportParameterVariantResult(expression, ref variantResult);
			if (!variantResult.ErrorOccurred && (variantResult.Value is object[] || variantResult.Value is IList))
			{
				object[] asObjectArray = ReportRuntime.GetAsObjectArray(ref variantResult);
				try
				{
					VariantResult variantResult2 = default(VariantResult);
					for (int i = 0; i < asObjectArray.Length; i++)
					{
						variantResult2.Value = asObjectArray[i];
						this.ProcessLabelResult(ref variantResult2);
						if (variantResult2.ErrorOccurred)
						{
							variantResult.ErrorOccurred = true;
							return variantResult;
						}
						asObjectArray[i] = variantResult2.Value;
					}
					variantResult.Value = asObjectArray;
					return variantResult;
				}
				catch (Exception)
				{
					this.RegisterInvalidExpressionDataTypeWarning();
					variantResult.ErrorOccurred = true;
					return variantResult;
				}
			}
			if (!variantResult.ErrorOccurred)
			{
				this.ProcessLabelResult(ref variantResult);
			}
			return variantResult;
		}

		// Token: 0x06004F10 RID: 20240 RVA: 0x00147544 File Offset: 0x00145744
		internal object EvaluateDataValueValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.DataValue value, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, out TypeCode typeCode)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(value.Value, objectType, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(value.Value, ref variantResult, value.ExprHost, () => value.ExprHost.DataValueValueExpr);
			}
			this.ProcessVariantResult(value.Value, ref variantResult);
			typeCode = variantResult.TypeCode;
			return variantResult.Value;
		}

		// Token: 0x06004F11 RID: 20241 RVA: 0x001475C4 File Offset: 0x001457C4
		internal string EvaluateDataValueNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.DataValue value, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(value.Name, objectType, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(value.Name, ref variantResult, value.ExprHost, () => value.ExprHost.DataValueNameExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F12 RID: 20242 RVA: 0x00147630 File Offset: 0x00145830
		internal VariantResult EvaluateFilterVariantExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Filter filter, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(filter.Expression, objectType, objectName, "FilterExpression", out variantResult))
			{
				this.EvaluateComplexExpression(filter.Expression, ref variantResult, filter.ExprHost, () => filter.ExprHost.FilterExpressionExpr);
			}
			this.ProcessVariantResult(filter.Expression, ref variantResult, false);
			return variantResult;
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x001476A8 File Offset: 0x001458A8
		internal StringResult EvaluateFilterStringExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Filter filter, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(filter.Expression, objectType, objectName, "FilterExpression", out variantResult))
			{
				this.EvaluateComplexExpression(filter.Expression, ref variantResult, filter.ExprHost, () => filter.ExprHost.FilterExpressionExpr);
			}
			return this.ProcessStringResult(variantResult);
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x00147710 File Offset: 0x00145910
		internal VariantResult EvaluateFilterVariantValue(Microsoft.ReportingServices.ReportIntermediateFormat.Filter filter, int index, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			Global.Tracer.Assert(filter.Values != null, "(filter.Values != null)");
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo valueExpression = filter.Values[index].Value;
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(valueExpression, objectType, objectName, "FilterValue", out variantResult))
			{
				this.EvaluateComplexExpressionWithExprHostId(valueExpression, ref variantResult, filter.ExprHost, () => filter.ExprHost[valueExpression.ExprHostID]);
			}
			this.ProcessVariantResult(valueExpression, ref variantResult, true);
			return variantResult;
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x001477B0 File Offset: 0x001459B0
		internal FloatResult EvaluateFilterIntegerOrFloatValue(Microsoft.ReportingServices.ReportIntermediateFormat.Filter filter, int index, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			Global.Tracer.Assert(filter.Values != null, "(filter.Values != null)");
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo valueExpression = filter.Values[index].Value;
			VariantResult variantResult;
			if (!this.EvaluateIntegerOrFloatExpression(valueExpression, objectType, objectName, "FilterValue", out variantResult))
			{
				this.EvaluateComplexExpressionWithExprHostId(valueExpression, ref variantResult, filter.ExprHost, () => filter.ExprHost[valueExpression.ExprHostID]);
			}
			return this.ProcessIntegerOrFloatResult(variantResult);
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x00147848 File Offset: 0x00145A48
		internal IntegerResult EvaluateFilterIntegerValue(Microsoft.ReportingServices.ReportIntermediateFormat.Filter filter, int index, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			Global.Tracer.Assert(filter.Values != null, "(filter.Values != null)");
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo valueExpression = filter.Values[index].Value;
			VariantResult variantResult;
			if (!this.EvaluateIntegerExpression(valueExpression, objectType, objectName, "FilterValue", out variantResult))
			{
				this.EvaluateComplexExpressionWithExprHostId(valueExpression, ref variantResult, filter.ExprHost, () => filter.ExprHost[valueExpression.ExprHostID]);
			}
			return this.ProcessIntegerResult(variantResult);
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x001478E0 File Offset: 0x00145AE0
		internal StringResult EvaluateFilterStringValue(Microsoft.ReportingServices.ReportIntermediateFormat.Filter filter, int index, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			Global.Tracer.Assert(filter.Values != null, "(filter.Values != null)");
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo valueExpression = filter.Values[index].Value;
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(valueExpression, objectType, objectName, "FilterValue", out variantResult))
			{
				this.EvaluateComplexExpressionWithExprHostId(valueExpression, ref variantResult, filter.ExprHost, () => filter.ExprHost[valueExpression.ExprHostID]);
			}
			return this.ProcessStringResult(variantResult);
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x00147978 File Offset: 0x00145B78
		internal object EvaluateQueryParamValue(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo paramValue, IndexedExprHost queryParamsExprHost, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paramValue, objectType, objectName, "Value", out variantResult))
			{
				try
				{
					this.EvaluateComplexExpressionForDataset(paramValue, ref variantResult, queryParamsExprHost, delegate
					{
						Global.Tracer.Assert(paramValue.ExprHostID >= 0, "(paramValue.ExprHostID >= 0)");
						return queryParamsExprHost[paramValue.ExprHostID];
					});
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpressionAndStop(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(paramValue, ref variantResult, true);
			return variantResult.Value;
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x00147A04 File Offset: 0x00145C04
		internal StringResult EvaluateConnectString(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(dataSource.ConnectStringExpression, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSource, dataSource.Name, "ConnectString", out variantResult))
			{
				try
				{
					this.EvaluateComplexExpressionForDataset(dataSource.ConnectStringExpression, ref variantResult, dataSource.ExprHost, () => dataSource.ExprHost.ConnectStringExpr);
				}
				catch (Exception ex)
				{
					if (ex is ReportProcessingException_NonExistingParameterReference)
					{
						this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
					}
					else
					{
						variantResult = new VariantResult(true, null);
					}
				}
			}
			return this.ProcessStringResult(variantResult);
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x00147AA8 File Offset: 0x00145CA8
		internal StringResult EvaluateCommandText(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(dataSet.Query.CommandText, Microsoft.ReportingServices.ReportProcessing.ObjectType.Query, dataSet.Name, "CommandText", out variantResult))
			{
				try
				{
					this.EvaluateComplexExpressionForDataset(dataSet.Query.CommandText, ref variantResult, dataSet.ExprHost, () => dataSet.ExprHost.QueryCommandTextExpr);
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			return this.ProcessStringResult(variantResult);
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x00147B44 File Offset: 0x00145D44
		internal VariantResult EvaluateFieldValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Field field)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(field.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.Field, field.Name, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(field.Value, ref variantResult, field.ExprHost, delegate
				{
					field.EnsureExprHostReportObjectModelBinding(this.m_reportObjectModel);
					return field.ExprHost.ValueExpr;
				});
			}
			this.ProcessVariantResult(field.Value, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x00147BCC File Offset: 0x00145DCC
		internal VariantResult EvaluateAggregateVariantOrBinaryParamExpr(Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo aggregateInfo, int index, IErrorContext errorContext)
		{
			IErrorContext delayedErrorContext = this.m_delayedErrorContext;
			VariantResult variantResult2;
			try
			{
				this.m_delayedErrorContext = errorContext;
				Global.Tracer.Assert(aggregateInfo.Expressions != null, "(aggregateInfo.Expressions != null)");
				Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = aggregateInfo.Expressions[index];
				VariantResult variantResult;
				if (!this.EvaluateSimpleExpression(expressionInfo, out variantResult))
				{
					try
					{
						if (!this.EvaluateComplexExpression(expressionInfo, ref variantResult, null))
						{
							Global.Tracer.Assert(aggregateInfo.ExpressionHosts != null && expressionInfo.ExprHostID >= 0, "(aggregateInfo.ExpressionHosts != null && aggParamExpression.ExprHostID >= 0)");
							if (this.m_exprHostInSandboxAppDomain)
							{
								aggregateInfo.ExpressionHosts[index].SetReportObjectModel(this.m_reportObjectModel);
							}
							variantResult.Value = aggregateInfo.ExpressionHosts[index].ValueExpr;
							if (this.m_reportObjectModel.OdpContext.CompareSafeExpressionsToLegacy)
							{
								this.SafeExpressionsComparator.Compare(expressionInfo, variantResult.Value);
							}
						}
					}
					catch (ReportProcessingException_MissingAggregateDependency)
					{
						throw;
					}
					catch (Exception ex)
					{
						this.RegisterRuntimeErrorInExpression(ref variantResult, ex, errorContext, false);
					}
				}
				this.ProcessVariantOrBinaryResult(expressionInfo, ref variantResult, true, false);
				variantResult2 = variantResult;
			}
			finally
			{
				this.m_delayedErrorContext = delayedErrorContext;
			}
			return variantResult2;
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x00147CF4 File Offset: 0x00145EF4
		internal VariantResult EvaluateLookupDestExpression(LookupDestinationInfo lookupDestInfo, IErrorContext errorContext)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo destinationExpr = lookupDestInfo.DestinationExpr;
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(destinationExpr, out variantResult))
			{
				this.EvaluateComplexExpression(destinationExpr, ref variantResult, lookupDestInfo.ExprHost, () => lookupDestInfo.ExprHost.DestExpr);
			}
			this.ProcessLookupVariantResult(destinationExpr, errorContext, false, false, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x00147D54 File Offset: 0x00145F54
		internal VariantResult EvaluateLookupSourceExpression(LookupInfo lookupInfo)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo sourceExpr = lookupInfo.SourceExpr;
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(sourceExpr, out variantResult))
			{
				this.EvaluateComplexExpression(sourceExpr, ref variantResult, lookupInfo.ExprHost, delegate
				{
					lookupInfo.ExprHost.SetReportObjectModel(this.m_reportObjectModel);
					return lookupInfo.ExprHost.SourceExpr;
				});
			}
			this.ProcessLookupVariantResult(sourceExpr, this, lookupInfo.LookupType == LookupType.MultiLookup, false, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x00147DC8 File Offset: 0x00145FC8
		internal VariantResult EvaluateLookupResultExpression(LookupInfo lookupInfo)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo resultExpr = lookupInfo.ResultExpr;
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(resultExpr, out variantResult))
			{
				this.EvaluateComplexExpression(resultExpr, ref variantResult, lookupInfo.ExprHost, delegate
				{
					lookupInfo.ExprHost.SetReportObjectModel(this.m_reportObjectModel);
					return lookupInfo.ExprHost.ResultExpr;
				});
			}
			this.ProcessLookupVariantResult(resultExpr, this, false, true, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F20 RID: 20256 RVA: 0x00147E30 File Offset: 0x00146030
		internal bool EvaluateParamValueOmitExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue paramVal, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateBooleanExpression(paramVal.Omit, true, objectType, objectName, "ParameterOmit", out variantResult))
			{
				this.EvaluateComplexExpression(paramVal.Omit, ref variantResult, paramVal.ExprHost, () => paramVal.ExprHost.OmitExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x00147EA0 File Offset: 0x001460A0
		internal ParameterValueResult EvaluateParameterValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue paramVal, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paramVal.Value, objectType, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(paramVal.Value, ref variantResult, paramVal.ExprHost, () => paramVal.ExprHost.ValueExpr);
			}
			return this.ProcessParameterValueResult(paramVal.Value, paramVal.Name, variantResult);
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x00147F1C File Offset: 0x0014611C
		internal bool EvaluateStartHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility, IVisibilityHiddenExprHost hiddenExprHostRI, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			bool isUnrestrictedRenderFormatReferenceMode = this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode;
			this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = false;
			bool value;
			try
			{
				VariantResult variantResult;
				if (!this.EvaluateBooleanExpression(visibility.Hidden, true, objectType, objectName, "Hidden", out variantResult))
				{
					this.EvaluateFatalComplexExpression(visibility.Hidden, ref variantResult, (ReportObjectModelProxy)hiddenExprHostRI, () => hiddenExprHostRI.VisibilityHiddenExpr);
				}
				value = this.ProcessBooleanResult(variantResult, true, objectType, objectName).Value;
			}
			finally
			{
				this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = isUnrestrictedRenderFormatReferenceMode;
			}
			return value;
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x00147FCC File Offset: 0x001461CC
		internal bool EvaluateStartHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility, IndexedExprHost hiddenExprHostIdx, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			bool isUnrestrictedRenderFormatReferenceMode = this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode;
			this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = false;
			bool value;
			try
			{
				VariantResult variantResult;
				if (!this.EvaluateBooleanExpression(visibility.Hidden, true, objectType, objectName, "Hidden", out variantResult))
				{
					this.EvaluateFatalComplexExpressionWithExprHostId(visibility.Hidden, ref variantResult, hiddenExprHostIdx, () => hiddenExprHostIdx[visibility.Hidden.ExprHostID]);
				}
				value = this.ProcessBooleanResult(variantResult, true, objectType, objectName).Value;
			}
			finally
			{
				this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = isUnrestrictedRenderFormatReferenceMode;
			}
			return value;
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x00148088 File Offset: 0x00146288
		internal string EvaluateReportItemDocumentMapLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem)
		{
			UserProfileState userProfileState = this.m_reportObjectModel.UserImpl.UpdateUserProfileLocationWithoutLocking(UserProfileState.InReport);
			bool isUnrestrictedRenderFormatReferenceMode = this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode;
			this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = false;
			string value;
			try
			{
				VariantResult variantResult;
				if (!this.EvaluateSimpleExpression(reportItem.DocumentMapLabel, reportItem.ObjectType, reportItem.Name, "Label", out variantResult))
				{
					this.EvaluateComplexExpression(reportItem.DocumentMapLabel, ref variantResult, reportItem.ExprHost, () => reportItem.ExprHost.LabelExpr);
				}
				value = this.ProcessAutocastStringResult(variantResult).Value;
			}
			finally
			{
				this.m_reportObjectModel.UserImpl.UpdateUserProfileLocationWithoutLocking(userProfileState);
				this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = isUnrestrictedRenderFormatReferenceMode;
			}
			return value;
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x00148178 File Offset: 0x00146378
		internal string EvaluateReportItemBookmarkExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem)
		{
			bool isUnrestrictedRenderFormatReferenceMode = this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode;
			this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = false;
			string value;
			try
			{
				VariantResult variantResult;
				if (!this.EvaluateSimpleExpression(reportItem.Bookmark, reportItem.ObjectType, reportItem.Name, "Bookmark", out variantResult))
				{
					this.EvaluateComplexExpression(reportItem.Bookmark, ref variantResult, reportItem.ExprHost, () => reportItem.ExprHost.BookmarkExpr);
				}
				value = this.ProcessStringResult(variantResult).Value;
			}
			finally
			{
				this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = isUnrestrictedRenderFormatReferenceMode;
			}
			return value;
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x00148240 File Offset: 0x00146440
		internal string EvaluateReportItemToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(reportItem.ToolTip, reportItem.ObjectType, reportItem.Name, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(reportItem.ToolTip, ref variantResult, reportItem.ExprHost, () => reportItem.ExprHost.ToolTipExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x001482C0 File Offset: 0x001464C0
		internal string EvaluateActionLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem actionItem, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Label", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, actionItem.ExprHost, () => actionItem.ExprHost.LabelExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x0014831C File Offset: 0x0014651C
		internal string EvaluateReportItemHyperlinkURLExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem actionItem, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Hyperlink", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, actionItem.ExprHost, () => actionItem.ExprHost.HyperlinkExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x00148378 File Offset: 0x00146578
		internal string EvaluateReportItemDrillthroughReportName(Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem actionItem, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "DrillthroughReportName", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, actionItem.ExprHost, () => actionItem.ExprHost.DrillThroughReportNameExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x001483D4 File Offset: 0x001465D4
		internal string EvaluateReportItemBookmarkLinkExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ActionItem actionItem, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BookmarkLink", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, actionItem.ExprHost, () => actionItem.ExprHost.BookmarkLinkExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x00148430 File Offset: 0x00146630
		internal string EvaluateImageStringValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Image image, out bool errorOccurred)
		{
			errorOccurred = false;
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(image.Value, image.ObjectType, image.Name, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(image.Value, ref variantResult, image.ImageExprHost, () => image.ImageExprHost.ValueExpr);
			}
			StringResult stringResult = this.ProcessStringResult(variantResult);
			errorOccurred = stringResult.ErrorOccurred;
			return stringResult.Value;
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x001484C0 File Offset: 0x001466C0
		internal VariantResult EvaluateImageTagExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Image image, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo tag)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(tag, image.ObjectType, image.Name, "Tag", out variantResult))
			{
				this.EvaluateComplexExpression(tag, ref variantResult, image.ImageExprHost, delegate
				{
					Global.Tracer.Assert(image.Tags.Count == 1, "Only a single Tag expression host is allowed from old snapshots");
					return image.ImageExprHost.TagExpr;
				});
			}
			this.ProcessVariantResult(tag, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x0014852C File Offset: 0x0014672C
		internal byte[] EvaluateImageBinaryValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Image image, out bool errorOccurred)
		{
			VariantResult variantResult;
			if (!this.EvaluateBinaryExpression(image.Value, image.ObjectType, image.Name, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(image.Value, ref variantResult, image.ImageExprHost, () => image.ImageExprHost.ValueExpr);
			}
			BinaryResult binaryResult = this.ProcessBinaryResult(variantResult);
			errorOccurred = binaryResult.ErrorOccurred;
			return binaryResult.Value;
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x001485B8 File Offset: 0x001467B8
		internal string EvaluateImageMIMETypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Image image)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(image.MIMEType, image.ObjectType, image.Name, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(image.MIMEType, ref variantResult, image.ImageExprHost, () => image.ImageExprHost.MIMETypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x00148638 File Offset: 0x00146838
		internal bool EvaluateTextBoxInitialToggleStateExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textBox)
		{
			VariantResult variantResult;
			if (!this.EvaluateBooleanExpression(textBox.InitialToggleState, true, textBox.ObjectType, textBox.Name, "InitialState", out variantResult))
			{
				this.EvaluateComplexExpression(textBox.InitialToggleState, ref variantResult, textBox.ExprHost, () => textBox.TextBoxExprHost.ToggleImageInitialStateExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x001486BC File Offset: 0x001468BC
		internal object EvaluateUserSortExpression(IInScopeEventSource eventSource)
		{
			int sortExpressionIndex = eventSource.UserSort.SortExpressionIndex;
			Microsoft.ReportingServices.ReportIntermediateFormat.ISortFilterScope sortTarget = eventSource.UserSort.SortTarget;
			Global.Tracer.Assert(sortTarget.UserSortExpressions != null && 0 <= sortExpressionIndex && sortExpressionIndex < sortTarget.UserSortExpressions.Count);
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo sortExpression = sortTarget.UserSortExpressions[sortExpressionIndex];
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(sortExpression, eventSource.ObjectType, eventSource.Name, "SortExpression", out variantResult))
			{
				this.EvaluateFatalComplexExpression(sortExpression, ref variantResult, sortTarget.UserSortExpressionsHost, () => sortTarget.UserSortExpressionsHost[sortExpression.ExprHostID]);
			}
			this.ProcessVariantResult(sortExpression, ref variantResult);
			if (variantResult.Value == null)
			{
				variantResult.Value = DBNull.Value;
			}
			return variantResult.Value;
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x001487A4 File Offset: 0x001469A4
		internal string EvaluateGroupingLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			UserProfileState userProfileState = this.m_reportObjectModel.UserImpl.UpdateUserProfileLocationWithoutLocking(UserProfileState.InReport);
			string value;
			try
			{
				VariantResult variantResult;
				if (!this.EvaluateSimpleExpression(grouping.GroupLabel, objectType, objectName, "Label", out variantResult))
				{
					this.EvaluateComplexExpression(grouping.GroupLabel, ref variantResult, grouping.ExprHost, () => grouping.ExprHost.LabelExpr);
				}
				value = this.ProcessAutocastStringResult(variantResult).Value;
			}
			finally
			{
				this.m_reportObjectModel.UserImpl.UpdateUserProfileLocationWithoutLocking(userProfileState);
			}
			return value;
		}

		// Token: 0x06004F32 RID: 20274 RVA: 0x0014884C File Offset: 0x00146A4C
		internal string EvaluateGroupingPageNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, string objectName)
		{
			bool isUnrestrictedRenderFormatReferenceMode = this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode;
			this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = false;
			string value;
			try
			{
				VariantResult variantResult;
				if (!this.EvaluateSimpleExpression(expression, Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping, objectName, "PageName", out variantResult))
				{
					this.EvaluateComplexExpression(expression, ref variantResult, grouping.ExprHost, () => grouping.ExprHost.PageNameExpr);
				}
				value = this.ProcessStringResult(variantResult, true).Value;
			}
			finally
			{
				this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = isUnrestrictedRenderFormatReferenceMode;
			}
			return value;
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x001488F0 File Offset: 0x00146AF0
		internal object EvaluateRuntimeExpression(RuntimeExpressionInfo runtimeExpression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(runtimeExpression.Expression, objectType, objectName, propertyName, out variantResult))
			{
				this.EvaluateFatalComplexExpressionWithExprHostId(runtimeExpression.Expression, ref variantResult, runtimeExpression.ExpressionsHost, () => runtimeExpression.ExpressionsHost[runtimeExpression.Expression.ExprHostID]);
			}
			this.ProcessVariantResult(runtimeExpression.Expression, ref variantResult);
			this.VerifyVariantResultAndStopOnError(ref variantResult);
			return variantResult.Value;
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x00148970 File Offset: 0x00146B70
		internal VariantResult EvaluateVariableValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Variable variable, IndexedExprHost variableValuesHost, Microsoft.ReportingServices.ReportProcessing.ObjectType parentObjectType, string parentObjectName, bool isReportScope)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(variable.Value, parentObjectType, parentObjectName, variable.GetPropertyName(), out variantResult))
			{
				this.EvaluateFatalComplexExpressionWithExprHostId(variable.Value, ref variantResult, variableValuesHost, () => variableValuesHost[variable.Value.ExprHostID]);
			}
			this.ProcessSerializableResult(variable.Value, isReportScope, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x001489F0 File Offset: 0x00146BF0
		internal string EvaluateSubReportNoRowsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(subReport.NoRowsMessage, Microsoft.ReportingServices.ReportProcessing.ObjectType.Subreport, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(subReport.NoRowsMessage, ref variantResult, subReport.SubReportExprHost, () => subReport.SubReportExprHost.NoRowsExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F36 RID: 20278 RVA: 0x00148A58 File Offset: 0x00146C58
		internal string EvaluateDataRegionNoRowsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion region, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(region.NoRowsMessage, objectType, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(region.NoRowsMessage, ref variantResult, region.ExprHost, () => region.EvaluateNoRowsMessageExpression());
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x00148AC4 File Offset: 0x00146CC4
		internal string EvaluateDataRegionPageNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			bool isUnrestrictedRenderFormatReferenceMode = this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode;
			this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = false;
			string value;
			try
			{
				VariantResult variantResult;
				if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "PageName", out variantResult))
				{
					this.EvaluateComplexExpression(expression, ref variantResult, dataRegion.ExprHost, () => dataRegion.ExprHost.PageNameExpr);
				}
				value = this.ProcessStringResult(variantResult, true).Value;
			}
			finally
			{
				this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = isUnrestrictedRenderFormatReferenceMode;
			}
			return value;
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x00148B68 File Offset: 0x00146D68
		internal string EvaluateTablixMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Tablix tablix, Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition marginPosition)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo = null;
			switch (marginPosition)
			{
			case Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.TopMargin:
				expressionInfo = tablix.TopMargin;
				break;
			case Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.BottomMargin:
				expressionInfo = tablix.BottomMargin;
				break;
			case Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.LeftMargin:
				expressionInfo = tablix.LeftMargin;
				break;
			case Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.RightMargin:
				expressionInfo = tablix.RightMargin;
				break;
			}
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expressionInfo, tablix.ObjectType, tablix.Name, marginPosition.ToString(), out variantResult))
			{
				Func<object> func = null;
				switch (marginPosition)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.TopMargin:
					func = () => tablix.TablixExprHost.TopMarginExpr;
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.BottomMargin:
					func = () => tablix.TablixExprHost.BottomMarginExpr;
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.LeftMargin:
					func = () => tablix.TablixExprHost.LeftMarginExpr;
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.Tablix.MarginPosition.RightMargin:
					func = () => tablix.TablixExprHost.RightMarginExpr;
					break;
				}
				this.EvaluateComplexExpression(expressionInfo, ref variantResult, tablix.ExprHost, func);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x00148C7C File Offset: 0x00146E7C
		internal string EvaluateChartDynamicSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expr, string propertyName, bool isDynamicWidth)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expr, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, chart.Name, propertyName, out variantResult))
			{
				Func<object> func;
				if (isDynamicWidth)
				{
					func = () => chart.ChartExprHost.DynamicWidthExpr;
				}
				else
				{
					func = () => chart.ChartExprHost.DynamicHeightExpr;
				}
				this.EvaluateComplexExpression(expr, ref variantResult, chart.ExprHost, func);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x00148CF8 File Offset: 0x00146EF8
		internal VariantResult EvaluateChartDynamicMemberLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartMember chartMember, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "DocumentMapLabel", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, chartMember.ExprHost, () => chartMember.ExprHost.MemberLabelExpr);
			}
			this.ProcessVariantResult(expression, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x00148D50 File Offset: 0x00146F50
		internal string EvaluateChartPaletteExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chart.Palette, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Palette", out variantResult))
			{
				this.EvaluateComplexExpression(chart.Palette, ref variantResult, chart.ExprHost, () => chart.ChartExprHost.PaletteExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F3C RID: 20284 RVA: 0x00148DC0 File Offset: 0x00146FC0
		internal string EvaluateChartPaletteHatchBehaviorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chart.PaletteHatchBehavior, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "PaletteHatchBehavior", out variantResult))
			{
				this.EvaluateComplexExpression(chart.PaletteHatchBehavior, ref variantResult, chart.ExprHost, () => chart.ChartExprHost.PaletteHatchBehaviorExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x00148E30 File Offset: 0x00147030
		internal VariantResult EvaluateChartTitleCaptionExpression(ChartTitleBase title, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(title.Caption, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(title.Caption, ref variantResult, title.ExprHost, () => title.ExprHost.CaptionExpr);
			}
			this.ProcessVariantResult(title.Caption, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x00148EA4 File Offset: 0x001470A4
		internal bool EvaluateEvaluateChartTitleHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle title, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(title.Caption, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(title.Hidden, ref variantResult, title.ExprHost, () => ((ChartTitleExprHost)title.ExprHost).HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x00148F10 File Offset: 0x00147110
		internal string EvaluateChartTitleDockingExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle title, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(title.Position, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(title.Docking, ref variantResult, title.ExprHost, () => ((ChartTitleExprHost)title.ExprHost).DockingExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x00148F7C File Offset: 0x0014717C
		internal string EvaluateChartTitlePositionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle title, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(title.Caption, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(title.Position, ref variantResult, title.ExprHost, () => ((ChartTitleExprHost)title.ExprHost).ChartTitlePositionExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x00148FE8 File Offset: 0x001471E8
		internal string EvaluateChartTitlePositionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxisTitle title, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(title.Caption, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(title.Position, ref variantResult, title.ExprHost, () => ((ChartTitleExprHost)title.ExprHost).ChartTitlePositionExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x00149054 File Offset: 0x00147254
		internal bool EvaluateChartTitleDockOutsideChartAreaExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle title, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(title.Caption, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(title.DockOutsideChartArea, ref variantResult, title.ExprHost, () => ((ChartTitleExprHost)title.ExprHost).DockOutsideChartAreaExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x001490C0 File Offset: 0x001472C0
		internal int EvaluateChartTitleDockOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle title, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(title.Caption, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(title.DockOffset, ref variantResult, title.ExprHost, () => ((ChartTitleExprHost)title.ExprHost).DockingOffsetExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x0014912C File Offset: 0x0014732C
		internal string EvaluateChartTitleToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle title, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(title.Caption, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(title.ToolTip, ref variantResult, title.ExprHost, () => ((ChartTitleExprHost)title.ExprHost).ToolTipExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x00149198 File Offset: 0x00147398
		internal string EvaluateChartTitleTextOrientationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTitle chartTitle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartTitle.TextOrientation, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "TextOrientation", out variantResult))
			{
				this.EvaluateComplexExpression(chartTitle.TextOrientation, ref variantResult, chartTitle.ExprHost, () => ((ChartTitleExprHost)chartTitle.ExprHost).TextOrientationExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x00149208 File Offset: 0x00147408
		internal string EvaluateChartAxisTitlePositionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxisTitle title, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(title.Caption, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(title.Position, ref variantResult, title.ExprHost, () => ((ChartAxisTitleExprHost)title.ExprHost).ChartTitlePositionExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x00149274 File Offset: 0x00147474
		internal string EvaluateChartAxisTitleTextOrientationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxisTitle chartAxisTitle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxisTitle.TextOrientation, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "TextOrientation", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxisTitle.TextOrientation, ref variantResult, chartAxisTitle.ExprHost, () => ((ChartAxisTitleExprHost)chartAxisTitle.ExprHost).TextOrientationExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x001492E4 File Offset: 0x001474E4
		internal string EvaluateChartLegendTitleTitleSeparatorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendTitle chartLegendTitle, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendTitle.TitleSeparator, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ChartTitleSeparator", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendTitle.TitleSeparator, ref variantResult, chartLegendTitle.ExprHost, () => ((ChartLegendTitleExprHost)chartLegendTitle.ExprHost).TitleSeparatorExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x00149354 File Offset: 0x00147554
		internal VariantResult EvaluateChartDataLabelLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel chartDataLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartDataLabel.Label, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Label", out variantResult))
			{
				this.EvaluateComplexExpression(chartDataLabel.Label, ref variantResult, chartDataLabel.ExprHost, () => chartDataLabel.ExprHost.LabelExpr);
			}
			this.ProcessVariantResult(chartDataLabel.Label, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x001493CC File Offset: 0x001475CC
		internal string EvaluateChartDataLabePositionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel chartDataLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartDataLabel.Position, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Position", out variantResult))
			{
				this.EvaluateComplexExpression(chartDataLabel.Position, ref variantResult, chartDataLabel.ExprHost, () => chartDataLabel.ExprHost.ChartDataLabelPositionExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x0014943C File Offset: 0x0014763C
		internal int EvaluateChartDataLabelRotationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel chartDataLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartDataLabel.Rotation, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Rotation", out variantResult))
			{
				this.EvaluateComplexExpression(chartDataLabel.Rotation, ref variantResult, chartDataLabel.ExprHost, () => chartDataLabel.ExprHost.RotationExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x001494AC File Offset: 0x001476AC
		internal bool EvaluateChartDataLabelUseValueAsLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel chartDataLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartDataLabel.UseValueAsLabel, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "UseValueAsLabel", out variantResult))
			{
				this.EvaluateComplexExpression(chartDataLabel.UseValueAsLabel, ref variantResult, chartDataLabel.ExprHost, () => chartDataLabel.ExprHost.UseValueAsLabelExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x0014951C File Offset: 0x0014771C
		internal bool EvaluateChartDataLabelVisibleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel chartDataLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartDataLabel.Visible, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Visible", out variantResult))
			{
				this.EvaluateComplexExpression(chartDataLabel.Visible, ref variantResult, chartDataLabel.ExprHost, () => chartDataLabel.ExprHost.VisibleExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x0014958C File Offset: 0x0014778C
		internal VariantResult EvaluateChartDataLabelToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataLabel chartDataLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartDataLabel.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(chartDataLabel.ToolTip, ref variantResult, chartDataLabel.ExprHost, () => chartDataLabel.ExprHost.ToolTipExpr);
			}
			return variantResult;
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x001495EE File Offset: 0x001477EE
		internal VariantResult EvaluateChartDataPointValuesXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsVariant(dataPoint, dataPoint.DataPointValues.X, objectName, "X", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesXExpr);
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x00149628 File Offset: 0x00147828
		internal string EvaluateChartTickMarksEnabledExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTickMarks chartTickMarks, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartTickMarks.Enabled, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Enabled", out variantResult))
			{
				this.EvaluateComplexExpression(chartTickMarks.Enabled, ref variantResult, chartTickMarks.ExprHost, () => chartTickMarks.ExprHost.EnabledExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x00149698 File Offset: 0x00147898
		internal string EvaluateChartTickMarksTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTickMarks chartTickMarks, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartTickMarks.Type, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Type", out variantResult))
			{
				this.EvaluateComplexExpression(chartTickMarks.Type, ref variantResult, chartTickMarks.ExprHost, () => chartTickMarks.ExprHost.TypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x00149708 File Offset: 0x00147908
		internal double EvaluateChartTickMarksLengthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTickMarks chartTickMarks, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartTickMarks.Length, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Length", out variantResult))
			{
				this.EvaluateComplexExpression(chartTickMarks.Length, ref variantResult, chartTickMarks.ExprHost, () => chartTickMarks.ExprHost.LengthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x00149778 File Offset: 0x00147978
		internal double EvaluateChartTickMarksIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTickMarks chartTickMarks, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartTickMarks.Interval, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Interval", out variantResult))
			{
				this.EvaluateComplexExpression(chartTickMarks.Interval, ref variantResult, chartTickMarks.ExprHost, () => chartTickMarks.ExprHost.IntervalExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x001497E8 File Offset: 0x001479E8
		internal string EvaluateChartTickMarksIntervalTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTickMarks chartTickMarks, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartTickMarks.IntervalType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "IntervalType", out variantResult))
			{
				this.EvaluateComplexExpression(chartTickMarks.IntervalType, ref variantResult, chartTickMarks.ExprHost, () => chartTickMarks.ExprHost.IntervalTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x00149858 File Offset: 0x00147A58
		internal double EvaluateChartTickMarksIntervalOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTickMarks chartTickMarks, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartTickMarks.IntervalOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "IntervalOffset", out variantResult))
			{
				this.EvaluateComplexExpression(chartTickMarks.IntervalOffset, ref variantResult, chartTickMarks.ExprHost, () => chartTickMarks.ExprHost.IntervalOffsetExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x001498C8 File Offset: 0x00147AC8
		internal string EvaluateChartTickMarksIntervalOffsetTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartTickMarks chartTickMarks, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartTickMarks.IntervalOffsetType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "IntervalOffsetType", out variantResult))
			{
				this.EvaluateComplexExpression(chartTickMarks.IntervalOffsetType, ref variantResult, chartTickMarks.ExprHost, () => chartTickMarks.ExprHost.IntervalOffsetTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x00149938 File Offset: 0x00147B38
		internal string EvaluateChartItemInLegendLegendTextExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartItemInLegend chartItemInLegend, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartItemInLegend.LegendText, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "LegendText", out variantResult))
			{
				this.EvaluateComplexExpression(chartItemInLegend.LegendText, ref variantResult, chartItemInLegend.ExprHost, () => chartItemInLegend.ExprHost.LegendTextExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x001499A8 File Offset: 0x00147BA8
		internal VariantResult EvaluateChartItemInLegendToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartItemInLegend chartItemInLegend, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartItemInLegend.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(chartItemInLegend.ToolTip, ref variantResult, chartItemInLegend.ExprHost, () => chartItemInLegend.ExprHost.ToolTipExpr);
			}
			return variantResult;
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x00149A0C File Offset: 0x00147C0C
		internal bool EvaluateChartItemInLegendHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartItemInLegend chartItemInLegend, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartItemInLegend.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(chartItemInLegend.Hidden, ref variantResult, chartItemInLegend.ExprHost, () => chartItemInLegend.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x00149A7C File Offset: 0x00147C7C
		internal VariantResult EvaluateChartEmptyPointsAxisLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartEmptyPoints chartEmptyPoints, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartEmptyPoints.AxisLabel, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "AxisLabel", out variantResult))
			{
				this.EvaluateComplexExpression(chartEmptyPoints.AxisLabel, ref variantResult, chartEmptyPoints.ExprHost, () => chartEmptyPoints.ExprHost.AxisLabelExpr);
			}
			this.ProcessVariantResult(chartEmptyPoints.AxisLabel, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x00149AF4 File Offset: 0x00147CF4
		internal VariantResult EvaluateChartEmptyPointsToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartEmptyPoints chartEmptyPoints, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartEmptyPoints.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(chartEmptyPoints.ToolTip, ref variantResult, chartEmptyPoints.ExprHost, () => chartEmptyPoints.ExprHost.ToolTipExpr);
			}
			return variantResult;
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x00149B58 File Offset: 0x00147D58
		internal VariantResult EvaluateChartFormulaParameterValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartFormulaParameter chartFormulaParameter, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartFormulaParameter.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(chartFormulaParameter.Value, ref variantResult, chartFormulaParameter.ExprHost, () => chartFormulaParameter.ExprHost.ValueExpr);
			}
			this.ProcessVariantResult(chartFormulaParameter.Value, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x00149BD0 File Offset: 0x00147DD0
		internal double EvaluateChartElementPositionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo, string propertyName, ChartElementPositionExprHost exprHost, Microsoft.ReportingServices.ReportIntermediateFormat.ChartElementPosition.Position position, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expressionInfo, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				Func<object> func = null;
				switch (position)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.ChartElementPosition.Position.Top:
					func = () => exprHost.TopExpr;
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.ChartElementPosition.Position.Left:
					func = () => exprHost.LeftExpr;
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.ChartElementPosition.Position.Height:
					func = () => exprHost.HeightExpr;
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.ChartElementPosition.Position.Width:
					func = () => exprHost.WidthExpr;
					break;
				}
				this.EvaluateComplexExpression(expressionInfo, ref variantResult, exprHost, func);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x00149C6C File Offset: 0x00147E6C
		internal string EvaluateChartSmartLabelAllowOutSidePlotAreaExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.AllowOutSidePlotArea, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "AllowOutSidePlotArea", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.AllowOutSidePlotArea, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.AllowOutSidePlotAreaExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x00149CDC File Offset: 0x00147EDC
		internal string EvaluateChartSmartLabelCalloutBackColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.CalloutBackColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "CalloutBackColor", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.CalloutBackColor, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.CalloutBackColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x00149D50 File Offset: 0x00147F50
		internal string EvaluateChartSmartLabelCalloutLineAnchorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.CalloutLineAnchor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "CalloutLineAnchor", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.CalloutLineAnchor, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.CalloutLineAnchorExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x00149DC0 File Offset: 0x00147FC0
		internal string EvaluateChartSmartLabelCalloutLineColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.CalloutLineColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "CalloutLineColor", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.CalloutLineColor, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.CalloutLineColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x00149E34 File Offset: 0x00148034
		internal string EvaluateChartSmartLabelCalloutLineStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.CalloutLineStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "CalloutLineStyle", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.CalloutLineStyle, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.CalloutLineStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x00149EA4 File Offset: 0x001480A4
		internal string EvaluateChartSmartLabelCalloutLineWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.CalloutLineWidth, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "CalloutLineWidth", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.CalloutLineWidth, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.CalloutLineWidthExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x00149F18 File Offset: 0x00148118
		internal string EvaluateChartSmartLabelCalloutStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.CalloutStyle, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "CalloutStyle", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.CalloutStyle, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.CalloutStyleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x00149F88 File Offset: 0x00148188
		internal bool EvaluateChartSmartLabelShowOverlappedExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.ShowOverlapped, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ShowOverlapped", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.ShowOverlapped, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.ShowOverlappedExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x00149FF8 File Offset: 0x001481F8
		internal bool EvaluateChartSmartLabelMarkerOverlappingExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.MarkerOverlapping, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "MarkerOverlapping", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.MarkerOverlapping, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.MarkerOverlappingExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x0014A068 File Offset: 0x00148268
		internal bool EvaluateChartSmartLabelDisabledExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.Disabled, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Disabled", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.Disabled, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.DisabledExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x0014A0D8 File Offset: 0x001482D8
		internal string EvaluateChartSmartLabelMaxMovingDistanceExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.MaxMovingDistance, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "MaxMovingDistance", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.MaxMovingDistance, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.MaxMovingDistanceExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x0014A14C File Offset: 0x0014834C
		internal string EvaluateChartSmartLabelMinMovingDistanceExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSmartLabel chartSmartLabel, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSmartLabel.MinMovingDistance, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "MinMovingDistance", out variantResult))
			{
				this.EvaluateComplexExpression(chartSmartLabel.MinMovingDistance, ref variantResult, chartSmartLabel.ExprHost, () => chartSmartLabel.ExprHost.MinMovingDistanceExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x0014A1C0 File Offset: 0x001483C0
		internal bool EvaluateChartLegendHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.Hidden, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x0014A22C File Offset: 0x0014842C
		internal string EvaluateChartLegendPositionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.Position, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.Position, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.ChartLegendPositionExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x0014A298 File Offset: 0x00148498
		internal string EvaluateChartLegendLayoutExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.Layout, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.Layout, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.LayoutExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x0014A304 File Offset: 0x00148504
		internal bool EvaluateChartLegendDockOutsideChartAreaExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.DockOutsideChartArea, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.DockOutsideChartArea, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.DockOutsideChartAreaExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x0014A370 File Offset: 0x00148570
		internal bool EvaluateChartLegendAutoFitTextDisabledExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.AutoFitTextDisabled, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.AutoFitTextDisabled, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.AutoFitTextDisabledExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x0014A3DC File Offset: 0x001485DC
		internal string EvaluateChartLegendMinFontSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.MinFontSize, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.MinFontSize, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.MinFontSizeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x0014A44C File Offset: 0x0014864C
		internal string EvaluateChartLegendHeaderSeparatorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.HeaderSeparator, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.HeaderSeparator, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.HeaderSeparatorExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x0014A4B8 File Offset: 0x001486B8
		internal string EvaluateChartLegendHeaderSeparatorColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.HeaderSeparatorColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.HeaderSeparatorColor, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.HeaderSeparatorColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x0014A528 File Offset: 0x00148728
		internal string EvaluateChartLegendColumnSeparatorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.ColumnSeparator, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.ColumnSeparator, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.ColumnSeparatorExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x0014A594 File Offset: 0x00148794
		internal string EvaluateChartLegendColumnSeparatorColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.ColumnSeparatorColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.ColumnSeparatorColor, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.ColumnSeparatorColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x0014A604 File Offset: 0x00148804
		internal int EvaluateChartLegendColumnSpacingExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.ColumnSpacing, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.ColumnSpacing, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.ColumnSpacingExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x0014A670 File Offset: 0x00148870
		internal bool EvaluateChartLegendInterlacedRowsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.InterlacedRows, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.InterlacedRows, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.InterlacedRowsExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x0014A6DC File Offset: 0x001488DC
		internal string EvaluateChartLegendInterlacedRowsColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.InterlacedRowsColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.InterlacedRowsColor, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.InterlacedRowsColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x0014A74C File Offset: 0x0014894C
		internal bool EvaluateChartLegendEquallySpacedItemsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.EquallySpacedItems, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.EquallySpacedItems, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.EquallySpacedItemsExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x0014A7B8 File Offset: 0x001489B8
		internal string EvaluateChartLegendReversedExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.Reversed, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Reversed", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.Reversed, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.ReversedExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x0014A828 File Offset: 0x00148A28
		internal int EvaluateChartLegendMaxAutoSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.MaxAutoSize, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.MaxAutoSize, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.MaxAutoSizeExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x0014A894 File Offset: 0x00148A94
		internal int EvaluateChartLegendTextWrapThresholdExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegend chartLegend, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegend.TextWrapThreshold, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartLegend.TextWrapThreshold, ref variantResult, chartLegend.ExprHost, () => chartLegend.ExprHost.TextWrapThresholdExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x0014A900 File Offset: 0x00148B00
		internal string EvaluateChartLegendColumnHeaderValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendColumnHeader chartLegendColumnHeader, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendColumnHeader.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendColumnHeader.Value, ref variantResult, chartLegendColumnHeader.ExprHost, () => chartLegendColumnHeader.ExprHost.ValueExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x0014A970 File Offset: 0x00148B70
		internal string EvaluateChartLegendColumnColumnTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendColumn chartLegendColumn, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendColumn.ColumnType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ColumnType", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendColumn.ColumnType, ref variantResult, chartLegendColumn.ExprHost, () => chartLegendColumn.ExprHost.ColumnTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x0014A9E0 File Offset: 0x00148BE0
		internal string EvaluateChartLegendColumnValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendColumn chartLegendColumn, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendColumn.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendColumn.Value, ref variantResult, chartLegendColumn.ExprHost, () => chartLegendColumn.ExprHost.ValueExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x0014AA50 File Offset: 0x00148C50
		internal string EvaluateChartLegendColumnToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendColumn chartLegendColumn, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendColumn.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendColumn.ToolTip, ref variantResult, chartLegendColumn.ExprHost, () => chartLegendColumn.ExprHost.ToolTipExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x0014AAC0 File Offset: 0x00148CC0
		internal string EvaluateChartLegendColumnMinimumWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendColumn chartLegendColumn, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendColumn.MinimumWidth, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "MinimumWidth", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendColumn.MinimumWidth, ref variantResult, chartLegendColumn.ExprHost, () => chartLegendColumn.ExprHost.MinimumWidthExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x0014AB34 File Offset: 0x00148D34
		internal string EvaluateChartLegendColumnMaximumWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendColumn chartLegendColumn, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendColumn.MaximumWidth, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "MaximumWidth", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendColumn.MaximumWidth, ref variantResult, chartLegendColumn.ExprHost, () => chartLegendColumn.ExprHost.MaximumWidthExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x0014ABA8 File Offset: 0x00148DA8
		internal int EvaluateChartLegendColumnSeriesSymbolWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendColumn chartLegendColumn, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendColumn.SeriesSymbolWidth, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "SeriesSymbolWidth", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendColumn.SeriesSymbolWidth, ref variantResult, chartLegendColumn.ExprHost, () => chartLegendColumn.ExprHost.SeriesSymbolWidthExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x0014AC18 File Offset: 0x00148E18
		internal int EvaluateChartLegendColumnSeriesSymbolHeightExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendColumn chartLegendColumn, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendColumn.SeriesSymbolHeight, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "SeriesSymbolHeight", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendColumn.SeriesSymbolHeight, ref variantResult, chartLegendColumn.ExprHost, () => chartLegendColumn.ExprHost.SeriesSymbolHeightExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x0014AC88 File Offset: 0x00148E88
		internal string EvaluateChartLegendCustomItemCellCellTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.CellType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "CellType", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.CellType, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.CellTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x0014ACF8 File Offset: 0x00148EF8
		internal string EvaluateChartLegendCustomItemCellTextExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.Text, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Text", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.Text, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.TextExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x0014AD68 File Offset: 0x00148F68
		internal int EvaluateChartLegendCustomItemCellCellSpanExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.CellSpan, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "CellSpan", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.CellSpan, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.CellSpanExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x0014ADD8 File Offset: 0x00148FD8
		internal string EvaluateChartLegendCustomItemCellToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.ToolTip, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.ToolTipExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x0014AE48 File Offset: 0x00149048
		internal int EvaluateChartLegendCustomItemCellImageWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.ImageWidth, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ImageWidth", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.ImageWidth, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.ImageWidthExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x0014AEB8 File Offset: 0x001490B8
		internal int EvaluateChartLegendCustomItemCellImageHeightExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.ImageHeight, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ImageHeight", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.ImageHeight, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.ImageHeightExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x0014AF28 File Offset: 0x00149128
		internal int EvaluateChartLegendCustomItemCellSymbolHeightExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.SymbolHeight, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "SymbolHeight", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.SymbolHeight, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.SymbolHeightExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x0014AF98 File Offset: 0x00149198
		internal int EvaluateChartLegendCustomItemCellSymbolWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.SymbolWidth, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "SymbolWidth", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.SymbolWidth, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.SymbolWidthExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x0014B008 File Offset: 0x00149208
		internal string EvaluateChartLegendCustomItemCellAlignmentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.Alignment, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Alignment", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.Alignment, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.AlignmentExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x0014B078 File Offset: 0x00149278
		internal int EvaluateChartLegendCustomItemCellTopMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.TopMargin, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "TopMargin", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.TopMargin, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.TopMarginExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x0014B0E8 File Offset: 0x001492E8
		internal int EvaluateChartLegendCustomItemCellBottomMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.BottomMargin, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "BottomMargin", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.BottomMargin, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.BottomMarginExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x0014B158 File Offset: 0x00149358
		internal int EvaluateChartLegendCustomItemCellLeftMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.LeftMargin, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "LeftMargin", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.LeftMargin, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.LeftMarginExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x0014B1C8 File Offset: 0x001493C8
		internal int EvaluateChartLegendCustomItemCellRightMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItemCell chartLegendCustomItemCell, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItemCell.RightMargin, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "RightMargin", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItemCell.RightMargin, ref variantResult, chartLegendCustomItemCell.ExprHost, () => chartLegendCustomItemCell.ExprHost.RightMarginExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x0014B238 File Offset: 0x00149438
		internal bool EvaluateChartNoMoveDirectionsUpExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartNoMoveDirections chartNoMoveDirections, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartNoMoveDirections.Up, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Up", out variantResult))
			{
				this.EvaluateComplexExpression(chartNoMoveDirections.Up, ref variantResult, chartNoMoveDirections.ExprHost, () => chartNoMoveDirections.ExprHost.UpExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F91 RID: 20369 RVA: 0x0014B2A8 File Offset: 0x001494A8
		internal bool EvaluateChartNoMoveDirectionsDownExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartNoMoveDirections chartNoMoveDirections, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartNoMoveDirections.Down, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Down", out variantResult))
			{
				this.EvaluateComplexExpression(chartNoMoveDirections.Down, ref variantResult, chartNoMoveDirections.ExprHost, () => chartNoMoveDirections.ExprHost.DownExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x0014B318 File Offset: 0x00149518
		internal bool EvaluateChartNoMoveDirectionsLeftExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartNoMoveDirections chartNoMoveDirections, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartNoMoveDirections.Left, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Left", out variantResult))
			{
				this.EvaluateComplexExpression(chartNoMoveDirections.Left, ref variantResult, chartNoMoveDirections.ExprHost, () => chartNoMoveDirections.ExprHost.LeftExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x0014B388 File Offset: 0x00149588
		internal bool EvaluateChartNoMoveDirectionsRightExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartNoMoveDirections chartNoMoveDirections, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartNoMoveDirections.Right, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Right", out variantResult))
			{
				this.EvaluateComplexExpression(chartNoMoveDirections.Right, ref variantResult, chartNoMoveDirections.ExprHost, () => chartNoMoveDirections.ExprHost.RightExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x0014B3F8 File Offset: 0x001495F8
		internal bool EvaluateChartNoMoveDirectionsUpLeftExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartNoMoveDirections chartNoMoveDirections, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartNoMoveDirections.UpLeft, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "UpLeft", out variantResult))
			{
				this.EvaluateComplexExpression(chartNoMoveDirections.UpLeft, ref variantResult, chartNoMoveDirections.ExprHost, () => chartNoMoveDirections.ExprHost.UpLeftExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x0014B468 File Offset: 0x00149668
		internal bool EvaluateChartNoMoveDirectionsUpRightExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartNoMoveDirections chartNoMoveDirections, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartNoMoveDirections.UpRight, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "UpRight", out variantResult))
			{
				this.EvaluateComplexExpression(chartNoMoveDirections.UpRight, ref variantResult, chartNoMoveDirections.ExprHost, () => chartNoMoveDirections.ExprHost.UpRightExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x0014B4D8 File Offset: 0x001496D8
		internal bool EvaluateChartNoMoveDirectionsDownLeftExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartNoMoveDirections chartNoMoveDirections, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartNoMoveDirections.DownLeft, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "DownLeft", out variantResult))
			{
				this.EvaluateComplexExpression(chartNoMoveDirections.DownLeft, ref variantResult, chartNoMoveDirections.ExprHost, () => chartNoMoveDirections.ExprHost.DownLeftExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x0014B548 File Offset: 0x00149748
		internal bool EvaluateChartNoMoveDirectionsDownRightExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartNoMoveDirections chartNoMoveDirections, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartNoMoveDirections.DownRight, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "DownRight", out variantResult))
			{
				this.EvaluateComplexExpression(chartNoMoveDirections.DownRight, ref variantResult, chartNoMoveDirections.ExprHost, () => chartNoMoveDirections.ExprHost.DownRightExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x0014B5B8 File Offset: 0x001497B8
		internal string EvaluateChartStripLineTitleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartStripLine chartStripLine, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartStripLine.Title, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Title", out variantResult))
			{
				this.EvaluateComplexExpression(chartStripLine.Title, ref variantResult, chartStripLine.ExprHost, () => chartStripLine.ExprHost.TitleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x0014B628 File Offset: 0x00149828
		internal int EvaluateChartStripLineTitleAngleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartStripLine chartStripLine, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartStripLine.TitleAngle, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "TitleAngle", out variantResult))
			{
				this.EvaluateComplexExpression(chartStripLine.TitleAngle, ref variantResult, chartStripLine.ExprHost, () => chartStripLine.ExprHost.TitleAngleExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x0014B698 File Offset: 0x00149898
		internal string EvaluateChartStripLineTextOrientationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartStripLine chartStripLine, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartStripLine.TextOrientation, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "TextOrientation", out variantResult))
			{
				this.EvaluateComplexExpression(chartStripLine.TextOrientation, ref variantResult, chartStripLine.ExprHost, () => chartStripLine.ExprHost.TextOrientationExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x0014B708 File Offset: 0x00149908
		internal string EvaluateChartStripLineToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartStripLine chartStripLine, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartStripLine.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(chartStripLine.ToolTip, ref variantResult, chartStripLine.ExprHost, () => chartStripLine.ExprHost.ToolTipExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x0014B778 File Offset: 0x00149978
		internal double EvaluateChartStripLineIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartStripLine chartStripLine, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartStripLine.Interval, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Interval", out variantResult))
			{
				this.EvaluateComplexExpression(chartStripLine.Interval, ref variantResult, chartStripLine.ExprHost, () => chartStripLine.ExprHost.IntervalExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x0014B7E8 File Offset: 0x001499E8
		internal string EvaluateChartStripLineIntervalTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartStripLine chartStripLine, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartStripLine.IntervalType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "IntervalType", out variantResult))
			{
				this.EvaluateComplexExpression(chartStripLine.IntervalType, ref variantResult, chartStripLine.ExprHost, () => chartStripLine.ExprHost.IntervalTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x0014B858 File Offset: 0x00149A58
		internal double EvaluateChartStripLineIntervalOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartStripLine chartStripLine, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartStripLine.IntervalOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "IntervalOffset", out variantResult))
			{
				this.EvaluateComplexExpression(chartStripLine.IntervalOffset, ref variantResult, chartStripLine.ExprHost, () => chartStripLine.ExprHost.IntervalOffsetExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x0014B8C8 File Offset: 0x00149AC8
		internal string EvaluateChartStripLineIntervalOffsetTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartStripLine chartStripLine, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartStripLine.IntervalOffsetType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "IntervalOffsetType", out variantResult))
			{
				this.EvaluateComplexExpression(chartStripLine.IntervalOffsetType, ref variantResult, chartStripLine.ExprHost, () => chartStripLine.ExprHost.IntervalOffsetTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x0014B938 File Offset: 0x00149B38
		internal double EvaluateChartStripLineStripWidthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartStripLine chartStripLine, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartStripLine.StripWidth, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "StripWidth", out variantResult))
			{
				this.EvaluateComplexExpression(chartStripLine.StripWidth, ref variantResult, chartStripLine.ExprHost, () => chartStripLine.ExprHost.StripWidthExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x0014B9A8 File Offset: 0x00149BA8
		internal string EvaluateChartStripLineStripWidthTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartStripLine chartStripLine, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartStripLine.StripWidthType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "StripWidthType", out variantResult))
			{
				this.EvaluateComplexExpression(chartStripLine.StripWidthType, ref variantResult, chartStripLine.ExprHost, () => chartStripLine.ExprHost.StripWidthTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x0014BA18 File Offset: 0x00149C18
		internal string EvaluateChartLegendCustomItemSeparatorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItem chartLegendCustomItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItem.Separator, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Separator", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItem.Separator, ref variantResult, chartLegendCustomItem.ExprHost, () => chartLegendCustomItem.ExprHost.SeparatorExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x0014BA88 File Offset: 0x00149C88
		internal string EvaluateChartLegendCustomItemSeparatorColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItem chartLegendCustomItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItem.SeparatorColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "SeparatorColor", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItem.SeparatorColor, ref variantResult, chartLegendCustomItem.ExprHost, () => chartLegendCustomItem.ExprHost.SeparatorColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x0014BAFC File Offset: 0x00149CFC
		internal string EvaluateChartLegendCustomItemToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartLegendCustomItem chartLegendCustomItem, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartLegendCustomItem.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(chartLegendCustomItem.ToolTip, ref variantResult, chartLegendCustomItem.ExprHost, () => chartLegendCustomItem.ExprHost.ToolTipExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x0014BB6C File Offset: 0x00149D6C
		internal string EvaluateChartSeriesTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSeries chartSeries, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSeries.Type, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Type", out variantResult))
			{
				this.EvaluateComplexExpression(chartSeries.Type, ref variantResult, chartSeries.ExprHost, () => chartSeries.ExprHost.TypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x0014BBDC File Offset: 0x00149DDC
		internal string EvaluateChartSeriesSubtypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSeries chartSeries, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSeries.Subtype, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Subtype", out variantResult))
			{
				this.EvaluateComplexExpression(chartSeries.Subtype, ref variantResult, chartSeries.ExprHost, () => chartSeries.ExprHost.SubtypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x0014BC4C File Offset: 0x00149E4C
		internal string EvaluateChartSeriesLegendNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSeries chartSeries, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSeries.LegendName, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "LegendName", out variantResult))
			{
				this.EvaluateComplexExpression(chartSeries.LegendName, ref variantResult, chartSeries.ExprHost, () => chartSeries.ExprHost.LegendNameExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x0014BCBC File Offset: 0x00149EBC
		internal VariantResult EvaluateChartSeriesLegendTextExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSeries chartSeries, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSeries.LegendText, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "LegendText", out variantResult))
			{
				this.EvaluateComplexExpression(chartSeries.LegendText, ref variantResult, chartSeries.ExprHost, () => chartSeries.ExprHost.LegendTextExpr);
			}
			return variantResult;
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x0014BD20 File Offset: 0x00149F20
		internal string EvaluateChartSeriesChartAreaNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSeries chartSeries, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSeries.ChartAreaName, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ChartAreaName", out variantResult))
			{
				this.EvaluateComplexExpression(chartSeries.ChartAreaName, ref variantResult, chartSeries.ExprHost, () => chartSeries.ExprHost.ChartAreaNameExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x0014BD90 File Offset: 0x00149F90
		internal string EvaluateChartSeriesValueAxisNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSeries chartSeries, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSeries.ValueAxisName, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ValueAxisName", out variantResult))
			{
				this.EvaluateComplexExpression(chartSeries.ValueAxisName, ref variantResult, chartSeries.ExprHost, () => chartSeries.ExprHost.ValueAxisNameExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x0014BE00 File Offset: 0x0014A000
		internal VariantResult EvaluateChartSeriesToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSeries chartSeries, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSeries.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(chartSeries.ToolTip, ref variantResult, chartSeries.ExprHost, () => chartSeries.ExprHost.ToolTipExpr);
			}
			return variantResult;
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x0014BE64 File Offset: 0x0014A064
		internal string EvaluateChartSeriesCategoryAxisNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSeries chartSeries, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSeries.CategoryAxisName, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "CategoryAxisName", out variantResult))
			{
				this.EvaluateComplexExpression(chartSeries.CategoryAxisName, ref variantResult, chartSeries.ExprHost, () => chartSeries.ExprHost.CategoryAxisNameExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x0014BED4 File Offset: 0x0014A0D4
		internal bool EvaluateChartSeriesHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSeries chartSeries, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSeries.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Hidden", out variantResult))
			{
				this.EvaluateComplexExpression(chartSeries.Hidden, ref variantResult, chartSeries.ExprHost, () => chartSeries.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x0014BF44 File Offset: 0x0014A144
		internal bool EvaluateChartSeriesHideInLegendExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartSeries chartSeries, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartSeries.HideInLegend, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "HideInLegend", out variantResult))
			{
				this.EvaluateComplexExpression(chartSeries.HideInLegend, ref variantResult, chartSeries.ExprHost, () => chartSeries.ExprHost.HideInLegendExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x0014BFB4 File Offset: 0x0014A1B4
		internal string EvaluateChartBorderSkinBorderSkinTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartBorderSkin chartBorderSkin, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartBorderSkin.BorderSkinType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ChartBorderSkinType", out variantResult))
			{
				this.EvaluateComplexExpression(chartBorderSkin.BorderSkinType, ref variantResult, chartBorderSkin.ExprHost, () => chartBorderSkin.ExprHost.BorderSkinTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x0014C024 File Offset: 0x0014A224
		internal bool EvaluateChartAxisScaleBreakEnabledExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxisScaleBreak chartAxisScaleBreak, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxisScaleBreak.Enabled, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Enabled", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxisScaleBreak.Enabled, ref variantResult, chartAxisScaleBreak.ExprHost, () => chartAxisScaleBreak.ExprHost.EnabledExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x0014C094 File Offset: 0x0014A294
		internal string EvaluateChartAxisScaleBreakBreakLineTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxisScaleBreak chartAxisScaleBreak, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxisScaleBreak.BreakLineType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "BreakLineType", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxisScaleBreak.BreakLineType, ref variantResult, chartAxisScaleBreak.ExprHost, () => chartAxisScaleBreak.ExprHost.BreakLineTypeExpr);
			}
			return this.ProcessStringResult(variantResult, true).Value;
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x0014C104 File Offset: 0x0014A304
		internal int EvaluateChartAxisScaleBreakCollapsibleSpaceThresholdExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxisScaleBreak chartAxisScaleBreak, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxisScaleBreak.CollapsibleSpaceThreshold, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "CollapsibleSpaceThreshold", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxisScaleBreak.CollapsibleSpaceThreshold, ref variantResult, chartAxisScaleBreak.ExprHost, () => chartAxisScaleBreak.ExprHost.CollapsibleSpaceThresholdExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x0014C174 File Offset: 0x0014A374
		internal int EvaluateChartAxisScaleBreakMaxNumberOfBreaksExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxisScaleBreak chartAxisScaleBreak, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxisScaleBreak.MaxNumberOfBreaks, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "MaxNumberOfBreaks", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxisScaleBreak.MaxNumberOfBreaks, ref variantResult, chartAxisScaleBreak.ExprHost, () => chartAxisScaleBreak.ExprHost.MaxNumberOfBreaksExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x0014C1E4 File Offset: 0x0014A3E4
		internal double EvaluateChartAxisScaleBreakSpacingExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxisScaleBreak chartAxisScaleBreak, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxisScaleBreak.Spacing, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Spacing", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxisScaleBreak.Spacing, ref variantResult, chartAxisScaleBreak.ExprHost, () => chartAxisScaleBreak.ExprHost.SpacingExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x0014C254 File Offset: 0x0014A454
		internal string EvaluateChartAxisScaleBreakIncludeZeroExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxisScaleBreak chartAxisScaleBreak, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxisScaleBreak.IncludeZero, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "IncludeZero", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxisScaleBreak.IncludeZero, ref variantResult, chartAxisScaleBreak.ExprHost, () => chartAxisScaleBreak.ExprHost.IncludeZeroExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x0014C2C4 File Offset: 0x0014A4C4
		internal string EvaluateChartCustomPaletteColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartCustomPaletteColor customPaletteColor, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(customPaletteColor.Color, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Color", out variantResult))
			{
				this.EvaluateComplexExpression(customPaletteColor.Color, ref variantResult, customPaletteColor.ExprHost, () => customPaletteColor.ExprHost.ColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x0014C338 File Offset: 0x0014A538
		internal VariantResult EvaluateChartDataPointAxisLabelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint chartDataPoint, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartDataPoint.AxisLabel, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "AxisLabel", out variantResult))
			{
				this.EvaluateComplexExpression(chartDataPoint.AxisLabel, ref variantResult, chartDataPoint.ExprHost, () => chartDataPoint.ExprHost.AxisLabelExpr);
			}
			this.ProcessVariantResult(chartDataPoint.AxisLabel, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x0014C3B0 File Offset: 0x0014A5B0
		internal VariantResult EvaluateChartDataPointToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint chartDataPoint, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartDataPoint.ToolTip, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(chartDataPoint.ToolTip, ref variantResult, chartDataPoint.ExprHost, () => chartDataPoint.ExprHost.ToolTipExpr);
			}
			return variantResult;
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x0014C412 File Offset: 0x0014A612
		internal VariantResult EvaluateChartDataPointValuesYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsVariant(dataPoint, dataPoint.DataPointValues.Y, objectName, "Y", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesYExpr);
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x0014C44B File Offset: 0x0014A64B
		internal VariantResult EvaluateChartDataPointValueSizesExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsVariant(dataPoint, dataPoint.DataPointValues.Size, objectName, "Size", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesSizeExpr);
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x0014C484 File Offset: 0x0014A684
		internal VariantResult EvaluateChartDataPointValuesHighExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(dataPoint.DataPointValues.High, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "High", out variantResult))
			{
				this.EvaluateComplexExpression(dataPoint.DataPointValues.High, ref variantResult, dataPoint.ExprHost, () => dataPoint.ExprHost.DataPointValuesHighExpr);
			}
			this.ProcessVariantResult(dataPoint.DataPointValues.High, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x0014C508 File Offset: 0x0014A708
		internal VariantResult EvaluateChartDataPointValuesLowExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsVariant(dataPoint, dataPoint.DataPointValues.Low, objectName, "Low", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesLowExpr);
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x0014C541 File Offset: 0x0014A741
		internal VariantResult EvaluateChartDataPointValuesStartExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsVariant(dataPoint, dataPoint.DataPointValues.Start, objectName, "Start", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesStartExpr);
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x0014C57A File Offset: 0x0014A77A
		internal VariantResult EvaluateChartDataPointValuesEndExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsVariant(dataPoint, dataPoint.DataPointValues.End, objectName, "End", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesEndExpr);
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x0014C5B3 File Offset: 0x0014A7B3
		internal VariantResult EvaluateChartDataPointValuesMeanExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsVariant(dataPoint, dataPoint.DataPointValues.Mean, objectName, "Mean", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesMeanExpr);
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x0014C5EC File Offset: 0x0014A7EC
		internal VariantResult EvaluateChartDataPointValuesMedianExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsVariant(dataPoint, dataPoint.DataPointValues.Median, objectName, "Median", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesMedianExpr);
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x0014C625 File Offset: 0x0014A825
		internal VariantResult EvaluateChartDataPointValuesHighlightXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsVariant(dataPoint, dataPoint.DataPointValues.HighlightX, objectName, "HighlightX", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesHighlightXExpr);
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x0014C65E File Offset: 0x0014A85E
		internal VariantResult EvaluateChartDataPointValuesHighlightYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsVariant(dataPoint, dataPoint.DataPointValues.HighlightY, objectName, "HighlightY", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesHighlightYExpr);
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x0014C697 File Offset: 0x0014A897
		internal VariantResult EvaluateChartDataPointValuesHighlightSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsVariant(dataPoint, dataPoint.DataPointValues.HighlightSize, objectName, "HighlightSize", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesHighlightSizeExpr);
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x0014C6D0 File Offset: 0x0014A8D0
		internal string EvaluateChartDataPointValuesFormatXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsString(dataPoint, dataPoint.DataPointValues.FormatX, objectName, "FormatX", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesFormatXExpr);
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x0014C709 File Offset: 0x0014A909
		internal string EvaluateChartDataPointValuesFormatYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsString(dataPoint, dataPoint.DataPointValues.FormatY, objectName, "FormatY", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesFormatYExpr);
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x0014C742 File Offset: 0x0014A942
		internal string EvaluateChartDataPointValuesFormatSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsString(dataPoint, dataPoint.DataPointValues.FormatSize, objectName, "FormatSize", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesFormatSizeExpr);
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x0014C77B File Offset: 0x0014A97B
		internal string EvaluateChartDataPointValuesCurrencyLanguageXExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsString(dataPoint, dataPoint.DataPointValues.CurrencyLanguageX, objectName, "CurrencyLanguageX", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesCurrencyLanguageXExpr);
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x0014C7B4 File Offset: 0x0014A9B4
		internal string EvaluateChartDataPointValuesCurrencyLanguageYExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsString(dataPoint, dataPoint.DataPointValues.CurrencyLanguageY, objectName, "CurrencyLanguageY", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesCurrencyLanguageYExpr);
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x0014C7ED File Offset: 0x0014A9ED
		internal string EvaluateChartDataPointValuesCurrencyLanguageSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, string objectName)
		{
			return this.EvaluateChartDataPointValuesExpressionAsString(dataPoint, dataPoint.DataPointValues.CurrencyLanguageSize, objectName, "CurrencyLanguageSize", (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dp) => dp.ExprHost.DataPointValuesCurrencyLanguageSizeExpr);
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x0014C828 File Offset: 0x0014AA28
		private VariantResult EvaluateChartDataPointValuesExpressionAsVariant(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo, string objectName, string propertyName, ReportRuntime.EvalulateDataPoint expressionFunction)
		{
			VariantResult variantResult = this.EvaluateChartDataPointValuesExpression(dataPoint, expressionInfo, objectName, propertyName, expressionFunction);
			this.ProcessVariantResult(expressionInfo, ref variantResult);
			return variantResult;
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x0014C850 File Offset: 0x0014AA50
		private string EvaluateChartDataPointValuesExpressionAsString(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo, string objectName, string propertyName, ReportRuntime.EvalulateDataPoint expressionFunction)
		{
			VariantResult variantResult = this.EvaluateChartDataPointValuesExpression(dataPoint, expressionInfo, objectName, propertyName, expressionFunction);
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x0014C878 File Offset: 0x0014AA78
		private VariantResult EvaluateChartDataPointValuesExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo, string objectName, string propertyName, ReportRuntime.EvalulateDataPoint expressionFunction)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expressionInfo, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(expressionInfo, ref variantResult, dataPoint.ExprHost, () => expressionFunction(dataPoint));
			}
			return variantResult;
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x0014C8CC File Offset: 0x0014AACC
		internal string EvaluateChartMarkerSize(Microsoft.ReportingServices.ReportIntermediateFormat.ChartMarker chartMarker, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartMarker.Size, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Size", out variantResult))
			{
				this.EvaluateComplexExpression(chartMarker.Size, ref variantResult, chartMarker.ExprHost, () => chartMarker.ExprHost.SizeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x0014C940 File Offset: 0x0014AB40
		internal string EvaluateChartMarkerType(Microsoft.ReportingServices.ReportIntermediateFormat.ChartMarker chartMarker, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartMarker.Type, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Type", out variantResult))
			{
				this.EvaluateComplexExpression(chartMarker.Type, ref variantResult, chartMarker.ExprHost, () => chartMarker.ExprHost.TypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x0014C9B0 File Offset: 0x0014ABB0
		internal string EvaluateChartAxisVisibleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.Visible, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Visible", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.Visible, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.VisibleExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x0014CA20 File Offset: 0x0014AC20
		internal string EvaluateChartAxisMarginExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.Margin, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Margin", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.Margin, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.MarginExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x0014CA90 File Offset: 0x0014AC90
		internal double EvaluateChartAxisIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.Interval, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Interval", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.Interval, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.IntervalExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x0014CB00 File Offset: 0x0014AD00
		internal string EvaluateChartAxisIntervalTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.IntervalType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "IntervalType", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.IntervalType, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.IntervalTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x0014CB70 File Offset: 0x0014AD70
		internal double EvaluateChartAxisIntervalOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.IntervalOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "IntervalOffset", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.IntervalOffset, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.IntervalOffsetExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x0014CBE0 File Offset: 0x0014ADE0
		internal string EvaluateChartAxisIntervalOffsetTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.IntervalOffsetType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "IntervalOffsetType", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.IntervalOffsetType, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.IntervalOffsetTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x0014CC50 File Offset: 0x0014AE50
		internal bool EvaluateChartAxisMarksAlwaysAtPlotEdgeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.MarksAlwaysAtPlotEdge, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "MarksAlwaysAtPlotEdge", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.MarksAlwaysAtPlotEdge, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.MarksAlwaysAtPlotEdgeExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0014CCC0 File Offset: 0x0014AEC0
		internal bool EvaluateChartAxisReverseExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.Reverse, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Reverse", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.Reverse, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.ReverseExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0014CD30 File Offset: 0x0014AF30
		internal string EvaluateChartAxisLocationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.Location, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Location", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.Location, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.LocationExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0014CDA0 File Offset: 0x0014AFA0
		internal bool EvaluateChartAxisInterlacedExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.Interlaced, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Interlaced", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.Interlaced, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.InterlacedExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0014CE10 File Offset: 0x0014B010
		internal string EvaluateChartAxisInterlacedColorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.InterlacedColor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "InterlacedColor", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.InterlacedColor, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.InterlacedColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, true);
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x0014CE84 File Offset: 0x0014B084
		internal bool EvaluateChartAxisLogScaleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.LogScale, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "LogScale", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.LogScale, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.LogScaleExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0014CEF4 File Offset: 0x0014B0F4
		internal double EvaluateChartAxisLogBaseExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.LogBase, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "LogBase", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.LogBase, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.LogBaseExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x0014CF64 File Offset: 0x0014B164
		internal bool EvaluateChartAxisHideLabelsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.HideLabels, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "HideLabels", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.HideLabels, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.HideLabelsExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x0014CFD4 File Offset: 0x0014B1D4
		internal double EvaluateChartAxisAngleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.Angle, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Angle", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.Angle, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.AngleExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x0014D044 File Offset: 0x0014B244
		internal string EvaluateChartAxisArrowsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.Arrows, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Arrows", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.Arrows, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.ArrowsExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0014D0B4 File Offset: 0x0014B2B4
		internal bool EvaluateChartAxisPreventFontShrinkExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.PreventFontShrink, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "PreventFontShrink", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.PreventFontShrink, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.PreventFontShrinkExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x0014D124 File Offset: 0x0014B324
		internal bool EvaluateChartAxisPreventFontGrowExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.PreventFontGrow, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "PreventFontGrow", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.PreventFontGrow, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.PreventFontGrowExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x0014D194 File Offset: 0x0014B394
		internal bool EvaluateChartAxisPreventLabelOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.PreventLabelOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "PreventLabelOffset", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.PreventLabelOffset, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.PreventLabelOffsetExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x0014D204 File Offset: 0x0014B404
		internal bool EvaluateChartAxisPreventWordWrapExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.PreventWordWrap, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "PreventWordWrap", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.PreventWordWrap, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.PreventWordWrapExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x0014D274 File Offset: 0x0014B474
		internal string EvaluateChartAxisAllowLabelRotationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.AllowLabelRotation, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "AllowLabelRotation", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.AllowLabelRotation, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.AllowLabelRotationExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x0014D2E4 File Offset: 0x0014B4E4
		internal bool EvaluateChartAxisIncludeZeroExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.IncludeZero, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "IncludeZero", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.IncludeZero, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.IncludeZeroExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x0014D354 File Offset: 0x0014B554
		internal bool EvaluateChartAxisLabelsAutoFitDisabledExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.LabelsAutoFitDisabled, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "LabelsAutoFitDisabled", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.LabelsAutoFitDisabled, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.LabelsAutoFitDisabledExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x0014D3C4 File Offset: 0x0014B5C4
		internal string EvaluateChartAxisMinFontSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.MinFontSize, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "MinFontSize", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.MinFontSize, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.MinFontSizeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x0014D438 File Offset: 0x0014B638
		internal string EvaluateChartAxisMaxFontSizeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.MaxFontSize, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "MaxFontSize", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.MaxFontSize, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.MaxFontSizeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x0014D4AC File Offset: 0x0014B6AC
		internal bool EvaluateChartAxisOffsetLabelsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.OffsetLabels, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "OffsetLabels", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.OffsetLabels, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.OffsetLabelsExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x0014D51C File Offset: 0x0014B71C
		internal bool EvaluateChartAxisHideEndLabelsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.HideEndLabels, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "HideEndLabels", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.HideEndLabels, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.HideEndLabelsExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x0014D58C File Offset: 0x0014B78C
		internal bool EvaluateChartAxisVariableAutoIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.VariableAutoInterval, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "VariableAutoInterval", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.VariableAutoInterval, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.VariableAutoIntervalExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x0014D5FC File Offset: 0x0014B7FC
		internal double EvaluateChartAxisLabelIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.LabelInterval, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "LabelInterval", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.LabelInterval, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.LabelIntervalExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x0014D66C File Offset: 0x0014B86C
		internal string EvaluateChartAxisLabelIntervalTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.LabelIntervalType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "LabelIntervalType", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.LabelIntervalType, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.LabelIntervalTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x0014D6DC File Offset: 0x0014B8DC
		internal double EvaluateChartAxisLabelIntervalOffsetsExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.LabelIntervalOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "LabelIntervalOffset", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.LabelIntervalOffset, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.LabelIntervalOffsetExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x0014D74C File Offset: 0x0014B94C
		internal string EvaluateChartAxisLabelIntervalOffsetTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis chartAxis, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAxis.LabelIntervalOffsetType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "LabelIntervalOffsetType", out variantResult))
			{
				this.EvaluateComplexExpression(chartAxis.LabelIntervalOffsetType, ref variantResult, chartAxis.ExprHost, () => chartAxis.ExprHost.LabelIntervalOffsetTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x0014D7BC File Offset: 0x0014B9BC
		internal object EvaluateChartAxisValueExpression(ChartAxisExprHost exprHost, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, string objectName, string propertyName, Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis.ExpressionType type)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				Func<object> func = null;
				switch (type)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis.ExpressionType.Min:
					func = () => exprHost.AxisMinExpr;
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis.ExpressionType.Max:
					func = () => exprHost.AxisMaxExpr;
					break;
				case Microsoft.ReportingServices.ReportIntermediateFormat.ChartAxis.ExpressionType.CrossAt:
					func = () => exprHost.AxisCrossAtExpr;
					break;
				}
				this.EvaluateComplexExpression(expression, ref variantResult, exprHost, func);
			}
			this.ProcessVariantResult(expression, ref variantResult);
			return variantResult.Value;
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0014D848 File Offset: 0x0014BA48
		internal bool EvaluateChartAreaEquallySizedAxesFontExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartArea chartArea, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartArea.EquallySizedAxesFont, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartArea.EquallySizedAxesFont, ref variantResult, chartArea.ExprHost, () => chartArea.ExprHost.EquallySizedAxesFontExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x0014D8B4 File Offset: 0x0014BAB4
		internal string EvaluateChartAreaAlignOrientationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartArea chartArea, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartArea.AlignOrientation, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartArea.AlignOrientation, ref variantResult, chartArea.ExprHost, () => chartArea.ExprHost.AlignOrientationExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0014D920 File Offset: 0x0014BB20
		internal bool EvaluateChartAreaHiddenExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartArea chartArea, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartArea.Hidden, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartArea.Hidden, ref variantResult, chartArea.ExprHost, () => chartArea.ExprHost.HiddenExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x0014D98C File Offset: 0x0014BB8C
		internal bool EvaluateChartAlignTypeAxesViewExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAlignType chartAlignType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAlignType.AxesView, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartAlignType.AxesView, ref variantResult, chartAlignType.ExprHost, () => chartAlignType.ExprHost.AxesViewExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x0014D9F8 File Offset: 0x0014BBF8
		internal bool EvaluateChartAlignTypeCursorExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAlignType chartAlignType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAlignType.Cursor, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartAlignType.Cursor, ref variantResult, chartAlignType.ExprHost, () => chartAlignType.ExprHost.CursorExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x0014DA64 File Offset: 0x0014BC64
		internal bool EvaluateChartAlignTypePositionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAlignType chartAlignType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAlignType.Position, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartAlignType.Position, ref variantResult, chartAlignType.ExprHost, () => chartAlignType.ExprHost.ChartAlignTypePositionExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x0014DAD0 File Offset: 0x0014BCD0
		internal bool EvaluateChartAlignTypeInnerPlotPositionExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartAlignType chartAlignType, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartAlignType.InnerPlotPosition, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartAlignType.InnerPlotPosition, ref variantResult, chartAlignType.ExprHost, () => chartAlignType.ExprHost.InnerPlotPositionExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x0014DB3C File Offset: 0x0014BD3C
		internal string EvaluateChartGridLinesEnabledExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartGridLines chartGridLines, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartGridLines.Enabled, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, "Enabled", out variantResult))
			{
				this.EvaluateComplexExpression(chartGridLines.Enabled, ref variantResult, chartGridLines.ExprHost, () => chartGridLines.ExprHost.EnabledExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0014DBAC File Offset: 0x0014BDAC
		internal double EvaluateChartGridLinesIntervalExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartGridLines chartGridLines, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartGridLines.Interval, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartGridLines.Interval, ref variantResult, chartGridLines.ExprHost, () => chartGridLines.ExprHost.IntervalExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0014DC18 File Offset: 0x0014BE18
		internal string EvaluateChartGridLinesIntervalTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartGridLines chartGridLines, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartGridLines.IntervalType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartGridLines.IntervalType, ref variantResult, chartGridLines.ExprHost, () => chartGridLines.ExprHost.IntervalTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0014DC84 File Offset: 0x0014BE84
		internal double EvaluateChartGridLinesIntervalOffsetExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartGridLines chartGridLines, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartGridLines.IntervalOffset, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartGridLines.IntervalOffset, ref variantResult, chartGridLines.ExprHost, () => chartGridLines.ExprHost.IntervalOffsetExpr);
			}
			return this.ProcessIntegerOrFloatResult(variantResult).Value;
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0014DCF0 File Offset: 0x0014BEF0
		internal string EvaluateChartGridLinesIntervalOffsetTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartGridLines chartGridLines, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartGridLines.IntervalOffsetType, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartGridLines.IntervalOffsetType, ref variantResult, chartGridLines.ExprHost, () => chartGridLines.ExprHost.IntervalOffsetTypeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x0014DD5C File Offset: 0x0014BF5C
		internal bool EvaluateChartThreeDPropertiesEnabledExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartThreeDProperties chartThreeDProperties, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartThreeDProperties.Enabled, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartThreeDProperties.Enabled, ref variantResult, chartThreeDProperties.ExprHost, () => chartThreeDProperties.ExprHost.EnabledExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x0014DDC8 File Offset: 0x0014BFC8
		internal string EvaluateChartThreeDPropertiesProjectionModeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartThreeDProperties chartThreeDProperties, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartThreeDProperties.ProjectionMode, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartThreeDProperties.ProjectionMode, ref variantResult, chartThreeDProperties.ExprHost, () => chartThreeDProperties.ExprHost.ProjectionModeExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x0014DE34 File Offset: 0x0014C034
		internal int EvaluateChartThreeDPropertiesRotationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartThreeDProperties chartThreeDProperties, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartThreeDProperties.Rotation, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartThreeDProperties.Rotation, ref variantResult, chartThreeDProperties.ExprHost, () => chartThreeDProperties.ExprHost.RotationExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x0014DEA0 File Offset: 0x0014C0A0
		internal int EvaluateChartThreeDPropertiesInclinationExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartThreeDProperties chartThreeDProperties, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartThreeDProperties.Inclination, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartThreeDProperties.Inclination, ref variantResult, chartThreeDProperties.ExprHost, () => chartThreeDProperties.ExprHost.InclinationExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x0014DF0C File Offset: 0x0014C10C
		internal int EvaluateChartThreeDPropertiesPerspectiveExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartThreeDProperties chartThreeDProperties, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartThreeDProperties.Perspective, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartThreeDProperties.Perspective, ref variantResult, chartThreeDProperties.ExprHost, () => chartThreeDProperties.ExprHost.PerspectiveExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x0014DF78 File Offset: 0x0014C178
		internal int EvaluateChartThreeDPropertiesDepthRatioExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartThreeDProperties chartThreeDProperties, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartThreeDProperties.DepthRatio, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartThreeDProperties.DepthRatio, ref variantResult, chartThreeDProperties.ExprHost, () => chartThreeDProperties.ExprHost.DepthRatioExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x0014DFE4 File Offset: 0x0014C1E4
		internal string EvaluateChartThreeDPropertiesShadingExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartThreeDProperties chartThreeDProperties, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartThreeDProperties.Shading, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartThreeDProperties.Shading, ref variantResult, chartThreeDProperties.ExprHost, () => chartThreeDProperties.ExprHost.ShadingExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0014E050 File Offset: 0x0014C250
		internal int EvaluateChartThreeDPropertiesGapDepthExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartThreeDProperties chartThreeDProperties, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartThreeDProperties.GapDepth, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartThreeDProperties.GapDepth, ref variantResult, chartThreeDProperties.ExprHost, () => chartThreeDProperties.ExprHost.GapDepthExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0014E0BC File Offset: 0x0014C2BC
		internal int EvaluateChartThreeDPropertiesWallThicknessExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartThreeDProperties chartThreeDProperties, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartThreeDProperties.WallThickness, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartThreeDProperties.WallThickness, ref variantResult, chartThreeDProperties.ExprHost, () => chartThreeDProperties.ExprHost.WallThicknessExpr);
			}
			return this.ProcessIntegerResult(variantResult).Value;
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x0014E128 File Offset: 0x0014C328
		internal bool EvaluateChartThreeDPropertiesClusteredExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ChartThreeDProperties chartThreeDProperties, string objectName, string propertyName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(chartThreeDProperties.Clustered, Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart, objectName, propertyName, out variantResult))
			{
				this.EvaluateComplexExpression(chartThreeDProperties.Clustered, ref variantResult, chartThreeDProperties.ExprHost, () => chartThreeDProperties.ExprHost.ClusteredExpr);
			}
			return this.ProcessBooleanResult(variantResult).Value;
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x0014E194 File Offset: 0x0014C394
		internal string EvaluateRectanglePageNameExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle rectangle, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, string objectName)
		{
			bool isUnrestrictedRenderFormatReferenceMode = this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode;
			this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = false;
			string value;
			try
			{
				VariantResult variantResult;
				if (!this.EvaluateSimpleExpression(expression, Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle, objectName, "PageName", out variantResult))
				{
					this.EvaluateComplexExpression(expression, ref variantResult, rectangle.ExprHost, () => rectangle.ExprHost.PageNameExpr);
				}
				value = this.ProcessStringResult(variantResult, true).Value;
			}
			finally
			{
				this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = isUnrestrictedRenderFormatReferenceMode;
			}
			return value;
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0014E238 File Offset: 0x0014C438
		internal string EvaluateStyleBorderColor(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderColor", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageReportItem(objectType));
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x0014E2A0 File Offset: 0x0014C4A0
		internal string EvaluateStyleBorderColorLeft(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderColor", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderColorLeftExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageReportItem(objectType));
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x0014E308 File Offset: 0x0014C508
		internal string EvaluateStyleBorderColorRight(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderColor", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderColorRightExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageReportItem(objectType));
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x0014E370 File Offset: 0x0014C570
		internal string EvaluateStyleBorderColorTop(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderColor", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderColorTopExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageReportItem(objectType));
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x0014E3D8 File Offset: 0x0014C5D8
		internal string EvaluateStyleBorderColorBottom(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderColor", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderColorBottomExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageReportItem(objectType));
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x0014E440 File Offset: 0x0014C640
		internal BorderStyles EvaluateStyleBorderStyle(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderStyle", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderStyleExpr);
			}
			return StyleTranslator.TranslateBorderStyle(this.ProcessStringResult(variantResult).Value, (Microsoft.ReportingServices.ReportProcessing.ObjectType.Line == objectType) ? BorderStyles.Solid : BorderStyles.None, this);
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x0014E4A8 File Offset: 0x0014C6A8
		internal BorderStyles EvaluateStyleBorderStyleLeft(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderStyle", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderStyleLeftExpr);
			}
			return StyleTranslator.TranslateBorderStyle(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x0014E508 File Offset: 0x0014C708
		internal BorderStyles EvaluateStyleBorderStyleRight(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderStyle", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderStyleRightExpr);
			}
			return StyleTranslator.TranslateBorderStyle(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x0014E568 File Offset: 0x0014C768
		internal BorderStyles EvaluateStyleBorderStyleTop(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderStyle", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderStyleTopExpr);
			}
			return StyleTranslator.TranslateBorderStyle(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x0014E5C8 File Offset: 0x0014C7C8
		internal BorderStyles EvaluateStyleBorderStyleBottom(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderStyle", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderStyleBottomExpr);
			}
			return StyleTranslator.TranslateBorderStyle(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x0014E628 File Offset: 0x0014C828
		internal string EvaluateStyleBorderWidth(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderWidth", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderWidthExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateBorderWidth(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x0014E688 File Offset: 0x0014C888
		internal string EvaluateStyleBorderWidthLeft(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderWidth", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderWidthLeftExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateBorderWidth(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x0014E6E8 File Offset: 0x0014C8E8
		internal string EvaluateStyleBorderWidthRight(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderWidth", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderWidthRightExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateBorderWidth(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x0014E748 File Offset: 0x0014C948
		internal string EvaluateStyleBorderWidthTop(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderWidth", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderWidthTopExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateBorderWidth(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x0014E7A8 File Offset: 0x0014C9A8
		internal string EvaluateStyleBorderWidthBottom(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BorderWidth", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BorderWidthBottomExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateBorderWidth(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x0014E808 File Offset: 0x0014CA08
		internal string EvaluateStyleBackgroundColor(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundColor", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BackgroundColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageReportItem(objectType));
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x0014E870 File Offset: 0x0014CA70
		internal string EvaluateStyleBackgroundGradientEndColor(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundGradientEndColor", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BackgroundGradientEndColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageReportItem(objectType));
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x0014E8D8 File Offset: 0x0014CAD8
		internal BackgroundGradients EvaluateStyleBackgroundGradientType(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundGradientType", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BackgroundGradientTypeExpr);
			}
			return StyleTranslator.TranslateBackgroundGradientType(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x0014E938 File Offset: 0x0014CB38
		internal BackgroundRepeatTypes EvaluateStyleBackgroundRepeat(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundRepeat", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BackgroundRepeatExpr);
			}
			return StyleTranslator.TranslateBackgroundRepeat(this.ProcessStringResult(variantResult).Value, this, objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart);
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x0014E99C File Offset: 0x0014CB9C
		internal FontStyles EvaluateStyleFontStyle(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "FontStyle", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.FontStyleExpr);
			}
			return StyleTranslator.TranslateFontStyle(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x0014E9FC File Offset: 0x0014CBFC
		internal string EvaluateStyleFontFamily(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "FontFamily", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.FontFamilyExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x0014EA58 File Offset: 0x0014CC58
		internal string EvaluateStyleFontSize(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "FontSize", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.FontSizeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateFontSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x0014EAB8 File Offset: 0x0014CCB8
		internal FontWeights EvaluateStyleFontWeight(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "FontWeight", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.FontWeightExpr);
			}
			return StyleTranslator.TranslateFontWeight(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x0014EB18 File Offset: 0x0014CD18
		internal string EvaluateStyleFormat(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Format", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.FormatExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x0014EB74 File Offset: 0x0014CD74
		internal TextDecorations EvaluateStyleTextDecoration(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "TextDecoration", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.TextDecorationExpr);
			}
			return StyleTranslator.TranslateTextDecoration(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x0014EBD4 File Offset: 0x0014CDD4
		internal TextAlignments EvaluateStyleTextAlign(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "TextAlign", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.TextAlignExpr);
			}
			return StyleTranslator.TranslateTextAlign(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x0014EC34 File Offset: 0x0014CE34
		internal VerticalAlignments EvaluateStyleVerticalAlign(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "VerticalAlign", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.VerticalAlignExpr);
			}
			return StyleTranslator.TranslateVerticalAlign(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x0014EC94 File Offset: 0x0014CE94
		internal string EvaluateStyleColor(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Color", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.ColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageReportItem(objectType));
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x0014ECFC File Offset: 0x0014CEFC
		internal string EvaluateStylePaddingLeft(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "PaddingLeft", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.PaddingLeftExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidatePadding(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x0014ED5C File Offset: 0x0014CF5C
		internal string EvaluateStylePaddingRight(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "PaddingRight", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.PaddingRightExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidatePadding(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x0014EDBC File Offset: 0x0014CFBC
		internal string EvaluateStylePaddingTop(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "PaddingTop", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.PaddingTopExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidatePadding(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x0014EE1C File Offset: 0x0014D01C
		internal string EvaluateStylePaddingBottom(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "PaddingBottom", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.PaddingBottomExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidatePadding(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x0014EE7C File Offset: 0x0014D07C
		internal string EvaluateStyleLineHeight(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "LineHeight", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.LineHeightExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateLineHeight(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x0014EEDC File Offset: 0x0014D0DC
		internal Directions EvaluateStyleDirection(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Direction", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.DirectionExpr);
			}
			return StyleTranslator.TranslateDirection(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x0014EF3C File Offset: 0x0014D13C
		internal WritingModes EvaluateStyleWritingMode(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "WritingMode", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.WritingModeExpr);
			}
			return StyleTranslator.TranslateWritingMode(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x0014EF9C File Offset: 0x0014D19C
		internal string EvaluateStyleLanguage(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Language", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.LanguageExpr);
			}
			CultureInfo cultureInfo;
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSpecificLanguage(this.ProcessStringResult(variantResult).Value, this, out cultureInfo);
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x0014F000 File Offset: 0x0014D200
		internal UnicodeBiDiTypes EvaluateStyleUnicodeBiDi(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "UnicodeBiDi", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.UnicodeBiDiExpr);
			}
			return StyleTranslator.TranslateUnicodeBiDi(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x0014F060 File Offset: 0x0014D260
		internal Calendars EvaluateStyleCalendar(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Calendar", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.CalendarExpr);
			}
			return StyleTranslator.TranslateCalendar(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x0014F0C0 File Offset: 0x0014D2C0
		internal string EvaluateStyleCurrencyLanguage(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "CurrencyLanguage", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, delegate
				{
					Global.Tracer.Assert(false, "(style.ExprHost should not be invoked for CurrencyLanguage.)");
					return null;
				});
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x0014F11C File Offset: 0x0014D31C
		internal string EvaluateStyleNumeralLanguage(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "NumeralLanguage", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.NumeralLanguageExpr);
			}
			CultureInfo cultureInfo;
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateLanguage(this.ProcessStringResult(variantResult).Value, this, out cultureInfo);
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x0014F180 File Offset: 0x0014D380
		internal object EvaluateStyleNumeralVariant(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateIntegerExpression(expression, objectType, objectName, "NumeralVariant", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.NumeralVariantExpr);
			}
			if (variantResult.Value == null)
			{
				return null;
			}
			IntegerResult integerResult = this.ProcessIntegerResult(variantResult);
			if (integerResult.ErrorOccurred)
			{
				return null;
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateNumeralVariant(integerResult.Value, this);
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x0014F1F8 File Offset: 0x0014D3F8
		internal object EvaluateTransparentColor(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "TransparentColor", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.TransparentColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageReportItem(objectType));
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x0014F260 File Offset: 0x0014D460
		internal object EvaluatePosition(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateIntegerExpression(expression, objectType, objectName, "Position", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.PositionExpr);
			}
			return StyleTranslator.TranslatePosition(this.ProcessStringResult(variantResult).Value, this, objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart);
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x0014F2CC File Offset: 0x0014D4CC
		internal string EvaluateStyleBackgroundUrlImageValue(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundImageValue", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BackgroundImageValueExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x0014F328 File Offset: 0x0014D528
		internal string EvaluateStyleBackgroundEmbeddedImageValue(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo> embeddedImages, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundImageValue", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BackgroundImageValueExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateEmbeddedImageName(this.ProcessStringResult(variantResult).Value, embeddedImages, this);
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x0014F38C File Offset: 0x0014D58C
		internal byte[] EvaluateStyleBackgroundDatabaseImageValue(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundImageValue", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BackgroundImageValueExpr);
			}
			return this.ProcessBinaryResult(variantResult).Value;
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x0014F3E8 File Offset: 0x0014D5E8
		internal string EvaluateStyleBackgroundImageMIMEType(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundImageMIMEType", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BackgroundImageMIMETypeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateMimeType(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x0014F448 File Offset: 0x0014D648
		internal string EvaluateStyleTextEffect(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "TextEffect", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.TextEffectExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateTextEffect(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x0014F4A8 File Offset: 0x0014D6A8
		internal string EvaluateStyleShadowColor(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "ShadowColor", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.ShadowColorExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateColor(this.ProcessStringResult(variantResult).Value, this, Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageReportItem(objectType));
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x0014F510 File Offset: 0x0014D710
		internal string EvaluateStyleShadowOffset(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "ShadowOffset", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.ShadowOffsetExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x0014F570 File Offset: 0x0014D770
		internal string EvaluateStyleBackgroundHatchType(Microsoft.ReportingServices.ReportIntermediateFormat.Style style, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "BackgroundHatchType", out variantResult))
			{
				this.EvaluateComplexExpression(expression, ref variantResult, style.ExprHost, () => style.ExprHost.BackgroundHatchTypeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateBackgroundHatchType(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x0014F5D0 File Offset: 0x0014D7D0
		internal string EvaluateParagraphLeftIndentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paragraph.LeftIndent, paragraph.ObjectType, paragraph.Name, "LeftIndent", out variantResult))
			{
				this.EvaluateComplexExpression(paragraph.LeftIndent, ref variantResult, paragraph.ExprHost, () => paragraph.ExprHost.LeftIndentExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x0014F658 File Offset: 0x0014D858
		internal string EvaluateParagraphRightIndentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paragraph.RightIndent, paragraph.ObjectType, paragraph.Name, "RightIndent", out variantResult))
			{
				this.EvaluateComplexExpression(paragraph.RightIndent, ref variantResult, paragraph.ExprHost, () => paragraph.ExprHost.RightIndentExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x0014F6E0 File Offset: 0x0014D8E0
		internal string EvaluateParagraphHangingIndentExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paragraph.HangingIndent, paragraph.ObjectType, paragraph.Name, "HangingIndent", out variantResult))
			{
				this.EvaluateComplexExpression(paragraph.HangingIndent, ref variantResult, paragraph.ExprHost, () => paragraph.ExprHost.HangingIndentExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, true, this);
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x0014F768 File Offset: 0x0014D968
		internal string EvaluateParagraphSpaceBeforeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paragraph.SpaceBefore, paragraph.ObjectType, paragraph.Name, "SpaceBefore", out variantResult))
			{
				this.EvaluateComplexExpression(paragraph.SpaceBefore, ref variantResult, paragraph.ExprHost, () => paragraph.ExprHost.SpaceBeforeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x0014F7F0 File Offset: 0x0014D9F0
		internal string EvaluateParagraphSpaceAfterExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paragraph.SpaceAfter, paragraph.ObjectType, paragraph.Name, "SpaceAfter", out variantResult))
			{
				this.EvaluateComplexExpression(paragraph.SpaceAfter, ref variantResult, paragraph.ExprHost, () => paragraph.ExprHost.SpaceAfterExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateSize(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x0014F878 File Offset: 0x0014DA78
		internal int? EvaluateParagraphListLevelExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paragraph.ListLevel, paragraph.ObjectType, paragraph.Name, "ListLevel", out variantResult))
			{
				this.EvaluateComplexExpression(paragraph.ListLevel, ref variantResult, paragraph.ExprHost, () => paragraph.ExprHost.ListLevelExpr);
			}
			IntegerResult integerResult = this.ProcessIntegerResult(variantResult);
			if (integerResult.ErrorOccurred)
			{
				return null;
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateParagraphListLevel(integerResult.Value, this);
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x0014F914 File Offset: 0x0014DB14
		internal string EvaluateParagraphListStyleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(paragraph.ListStyle, paragraph.ObjectType, paragraph.Name, "ListStyle", out variantResult))
			{
				this.EvaluateComplexExpression(paragraph.ListStyle, ref variantResult, paragraph.ExprHost, () => paragraph.ExprHost.ListStyleExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateParagraphListStyle(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x0014F99C File Offset: 0x0014DB9C
		internal string EvaluateTextRunToolTipExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TextRun textRun)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(textRun.ToolTip, textRun.ObjectType, textRun.Name, "ToolTip", out variantResult))
			{
				this.EvaluateComplexExpression(textRun.ToolTip, ref variantResult, textRun.ExprHost, () => textRun.ExprHost.ToolTipExpr);
			}
			return this.ProcessStringResult(variantResult).Value;
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x0014FA1C File Offset: 0x0014DC1C
		internal string EvaluateTextRunMarkupTypeExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TextRun textRun)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(textRun.MarkupType, textRun.ObjectType, textRun.Name, "MarkupType", out variantResult))
			{
				this.EvaluateComplexExpression(textRun.MarkupType, ref variantResult, textRun.ExprHost, () => textRun.ExprHost.MarkupTypeExpr);
			}
			return Microsoft.ReportingServices.ReportPublishing.ProcessingValidator.ValidateTextRunMarkupType(this.ProcessStringResult(variantResult).Value, this);
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x0014FAA4 File Offset: 0x0014DCA4
		internal VariantResult EvaluateTextRunValueExpression(Microsoft.ReportingServices.ReportIntermediateFormat.TextRun textRun)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo value = textRun.Value;
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(value, textRun.ObjectType, textRun.Name, "Value", out variantResult))
			{
				this.EvaluateComplexExpression(value, ref variantResult, textRun.ExprHost, () => textRun.ExprHost.ValueExpr);
			}
			this.ProcessVariantResult(value, ref variantResult);
			return variantResult;
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x0014FB1C File Offset: 0x0014DD1C
		internal bool EvaluatePageBreakDisabledExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PageBreak pageBreak, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			bool isUnrestrictedRenderFormatReferenceMode = this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode;
			this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = false;
			bool value;
			try
			{
				VariantResult variantResult;
				if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "Disabled", out variantResult))
				{
					this.EvaluateComplexExpression(expression, ref variantResult, pageBreak.ExprHost, () => pageBreak.ExprHost.DisabledExpr);
				}
				value = this.ProcessBooleanResult(variantResult).Value;
			}
			finally
			{
				this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = isUnrestrictedRenderFormatReferenceMode;
			}
			return value;
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x0014FBC0 File Offset: 0x0014DDC0
		internal bool EvaluatePageBreakResetPageNumberExpression(Microsoft.ReportingServices.ReportIntermediateFormat.PageBreak pageBreak, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			bool isUnrestrictedRenderFormatReferenceMode = this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode;
			this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = false;
			bool value;
			try
			{
				VariantResult variantResult;
				if (!this.EvaluateSimpleExpression(expression, objectType, objectName, "ResetPageNumber", out variantResult))
				{
					this.EvaluateComplexExpression(expression, ref variantResult, pageBreak.ExprHost, () => pageBreak.ExprHost.ResetPageNumberExpr);
				}
				value = this.ProcessBooleanResult(variantResult).Value;
			}
			finally
			{
				this.m_reportObjectModel.OdpContext.IsUnrestrictedRenderFormatReferenceMode = isUnrestrictedRenderFormatReferenceMode;
			}
			return value;
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x0014FC64 File Offset: 0x0014DE64
		internal VariantResult EvaluateJoinConditionForeignKeyExpression(Relationship.JoinCondition joinCondition)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo foreignKeyExpression = joinCondition.ForeignKeyExpression;
			JoinConditionExprHost exprHost = joinCondition.ExprHost;
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(foreignKeyExpression, out variantResult))
			{
				this.EvaluateComplexExpression(foreignKeyExpression, ref variantResult, exprHost, () => exprHost.ForeignKeyExpr);
			}
			this.ProcessVariantResult(foreignKeyExpression, ref variantResult);
			this.VerifyVariantResultAndStopOnError(ref variantResult);
			return variantResult;
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x0014FCC4 File Offset: 0x0014DEC4
		internal VariantResult EvaluateJoinConditionPrimaryKeyExpression(Relationship.JoinCondition joinCondition)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo primaryKeyExpression = joinCondition.PrimaryKeyExpression;
			JoinConditionExprHost exprHost = joinCondition.ExprHost;
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(primaryKeyExpression, out variantResult))
			{
				this.EvaluateComplexExpression(primaryKeyExpression, ref variantResult, exprHost, () => exprHost.PrimaryKeyExpr);
			}
			this.ProcessVariantResult(primaryKeyExpression, ref variantResult);
			this.VerifyVariantResultAndStopOnError(ref variantResult);
			return variantResult;
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x0014FD24 File Offset: 0x0014DF24
		internal object EvaluateDataShapeCalculationValue(Calculation calculation)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(calculation.Expression, Microsoft.ReportingServices.ReportProcessing.ObjectType.Calculation, calculation.Name, "Calculation", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(calculation.Expression, ref variantResult, null))
					{
						Global.Tracer.Assert(false, "Data shape calculation is complex.");
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantOrBinaryResult(calculation.Expression, ref variantResult, false, true);
			return variantResult.Value;
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x0014FDA4 File Offset: 0x0014DFA4
		internal object EvaluateScopeValueDefinition(ScopeValueDefinition scopeValueDefinition, string objectName)
		{
			VariantResult variantResult;
			if (!this.EvaluateSimpleExpression(scopeValueDefinition.Value, Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping, objectName, "Group", out variantResult))
			{
				try
				{
					if (!this.EvaluateComplexExpression(scopeValueDefinition.Value, ref variantResult, null))
					{
						Global.Tracer.Assert(false, "Scope value definition value is complex.");
					}
				}
				catch (Exception ex)
				{
					this.RegisterRuntimeErrorInExpression(ref variantResult, ex);
				}
			}
			this.ProcessVariantResult(scopeValueDefinition.Value, ref variantResult);
			return variantResult.Value;
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x0014FE1C File Offset: 0x0014E01C
		private bool RouteExpressionEvaluation(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result)
		{
			if (this.m_useSafeExpressions)
			{
				this.m_safeExpressionRuntime.EvaluateExpression(expression, ref result);
				return true;
			}
			return false;
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x0014FE38 File Offset: 0x0014E038
		private bool EvaluateSimpleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			this.m_objectType = objectType;
			this.m_objectName = objectName;
			this.m_propertyName = propertyName;
			if (this.m_topLevelReportRuntime != null)
			{
				this.m_topLevelReportRuntime.ObjectType = objectType;
				this.m_topLevelReportRuntime.ObjectName = objectName;
				this.m_topLevelReportRuntime.PropertyName = propertyName;
			}
			return this.EvaluateSimpleExpression(expression, out result);
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x0014FE94 File Offset: 0x0014E094
		private bool EvaluateSimpleExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, out VariantResult result)
		{
			result = default(VariantResult);
			if (expression == null)
			{
				return true;
			}
			switch (expression.Type)
			{
			case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression:
				return this.RouteExpressionEvaluation(expression, ref result);
			case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Field:
				this.EvaluateSimpleFieldReference(expression.IntValue, ref result);
				return true;
			case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Aggregate:
				return false;
			case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant:
				result.Value = expression.Value;
				result.TypeCode = expression.ConstantTypeCode;
				return true;
			case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Token:
			{
				Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSet dataSet = this.m_reportObjectModel.DataSetsImpl[expression.StringValue];
				result.Value = dataSet.RewrittenCommandText;
				return true;
			}
			case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Lookup_OneValue:
			case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Lookup_MultiValue:
				return false;
			case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.RdlFunction:
				this.EvaluateRdlFunction(expression, ref result);
				return true;
			case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.ScopedFieldReference:
				return false;
			case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Literal:
				result.Value = expression.LiteralInfo.Value;
				return true;
			default:
				Global.Tracer.Assert(false);
				return true;
			}
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x0014FF70 File Offset: 0x0014E170
		internal void EvaluateSimpleFieldReference(int fieldIndex, ref VariantResult result)
		{
			try
			{
				Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel.FieldImpl fieldImpl = this.m_reportObjectModel.FieldsImpl[fieldIndex];
				if (fieldImpl.IsMissing)
				{
					result.Value = null;
				}
				else if (fieldImpl.FieldStatus != DataFieldStatus.None)
				{
					result.ErrorOccurred = true;
					result.FieldStatus = fieldImpl.FieldStatus;
					result.ExceptionMessage = fieldImpl.ExceptionMessage;
					result.Value = null;
				}
				else
				{
					result.Value = fieldImpl.Value;
				}
			}
			catch (ReportProcessingException_NoRowsFieldAccess reportProcessingException_NoRowsFieldAccess)
			{
				this.RegisterRuntimeWarning(reportProcessingException_NoRowsFieldAccess, this);
				result.Value = null;
			}
			catch (ReportProcessingException_InvalidOperationException)
			{
				result.Value = null;
				result.ErrorOccurred = true;
			}
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x00150020 File Offset: 0x0014E220
		private bool EvaluateComplexExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result, ReportObjectModelProxy exprHost)
		{
			if (expression != null)
			{
				switch (expression.Type)
				{
				case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Expression:
					if (this.m_exprHostInSandboxAppDomain && exprHost != null)
					{
						exprHost.SetReportObjectModel(this.m_reportObjectModel);
					}
					return this.RouteExpressionEvaluation(expression, ref result);
				case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Aggregate:
					result.Value = this.m_reportObjectModel.AggregatesImpl[expression.StringValue];
					return true;
				case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Lookup_OneValue:
				{
					Lookup lookup = this.m_reportObjectModel.LookupsImpl[expression.StringValue];
					result.Value = lookup.Value;
					return true;
				}
				case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Lookup_MultiValue:
				{
					Lookup lookup2 = this.m_reportObjectModel.LookupsImpl[expression.StringValue];
					result.Value = lookup2.Values;
					return true;
				}
				case Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.ScopedFieldReference:
					try
					{
						this.m_reportObjectModel.OdpContext.StateManager.EvaluateScopedFieldReference(expression.StringValue, expression.ScopedFieldInfo.FieldIndex, ref result);
					}
					catch (ReportProcessingException_NonExistingScopeReference reportProcessingException_NonExistingScopeReference)
					{
						result.Value = null;
						result.ErrorOccurred = true;
						result.ExceptionMessage = reportProcessingException_NonExistingScopeReference.Message;
					}
					catch (ReportProcessingException_InvalidScopeReference reportProcessingException_InvalidScopeReference)
					{
						result.Value = null;
						result.ErrorOccurred = true;
						result.ExceptionMessage = reportProcessingException_InvalidScopeReference.Message;
					}
					return true;
				}
				Global.Tracer.Assert(false);
				return true;
			}
			return true;
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x00150184 File Offset: 0x0014E384
		private void EvaluateComplexExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result, ReportObjectModelProxy exprHost, Func<object> expressionExecution)
		{
			try
			{
				if (!this.EvaluateComplexExpression(expression, ref result, exprHost))
				{
					Global.Tracer.Assert(exprHost != null, "(exprHost != null)");
					result.Value = expressionExecution();
					if (this.m_reportObjectModel.OdpContext.CompareSafeExpressionsToLegacy)
					{
						this.SafeExpressionsComparator.Compare(expression, result.Value);
					}
				}
			}
			catch (Exception ex)
			{
				this.RegisterRuntimeErrorInExpression(ref result, ex);
			}
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x00150200 File Offset: 0x0014E400
		private void EvaluateComplexExpressionForParams(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result, ReportObjectModelProxy exprHost, bool usedInQuery, Func<object> expressionExecution)
		{
			try
			{
				if (!this.EvaluateComplexExpression(expression, ref result, exprHost))
				{
					Global.Tracer.Assert(exprHost != null && expression.ExprHostID >= 0);
					this.m_reportObjectModel.UserImpl.IndirectQueryReference = usedInQuery;
					result.Value = expressionExecution();
					this.m_reportObjectModel.UserImpl.IndirectQueryReference = false;
					if (this.m_reportObjectModel.OdpContext.CompareSafeExpressionsToLegacy)
					{
						this.SafeExpressionsComparator.Compare(expression, result.Value);
					}
				}
			}
			catch (Exception ex)
			{
				this.RegisterRuntimeErrorInExpression(ref result, ex);
			}
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x001502A8 File Offset: 0x0014E4A8
		private void EvaluateComplexExpressionWithExprHostId(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result, ReportObjectModelProxy exprHost, Func<object> expressionExecution)
		{
			try
			{
				if (!this.EvaluateComplexExpression(expression, ref result, exprHost))
				{
					Global.Tracer.Assert(exprHost != null && expression.ExprHostID >= 0, "(exprHost != null && expression.ExprHostID >= 0)");
					result.Value = expressionExecution();
					if (this.m_reportObjectModel.OdpContext.CompareSafeExpressionsToLegacy)
					{
						this.SafeExpressionsComparator.Compare(expression, result.Value);
					}
				}
			}
			catch (Exception ex)
			{
				this.RegisterRuntimeErrorInExpression(ref result, ex);
			}
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x00150330 File Offset: 0x0014E530
		private void EvaluateComplexExpressionForDataset(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result, ReportObjectModelProxy exprHost, Func<object> expressionExecution)
		{
			if (!this.EvaluateComplexExpression(expression, ref result, exprHost))
			{
				using (this.m_reportObjectModel.UserImpl.UpdateUserProfileLocation(UserProfileState.InQuery))
				{
					Global.Tracer.Assert(exprHost != null, "(exprHost != null)");
					result.Value = expressionExecution();
					if (this.m_reportObjectModel.OdpContext.CompareSafeExpressionsToLegacy)
					{
						this.SafeExpressionsComparator.Compare(expression, result.Value);
					}
				}
			}
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x001503BC File Offset: 0x0014E5BC
		private void EvaluateFatalComplexExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result, ReportObjectModelProxy exprHost, Func<object> expressionExecution)
		{
			try
			{
				if (!this.EvaluateComplexExpression(expression, ref result, exprHost))
				{
					Global.Tracer.Assert(exprHost != null, "(exprHost != null)");
					result.Value = expressionExecution();
					if (this.m_reportObjectModel.OdpContext.CompareSafeExpressionsToLegacy)
					{
						this.SafeExpressionsComparator.Compare(expression, result.Value);
					}
				}
			}
			catch (Exception ex)
			{
				this.RegisterRuntimeErrorInExpressionAndStop(ref result, ex);
			}
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x00150438 File Offset: 0x0014E638
		private void EvaluateFatalComplexExpressionWithExprHostId(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result, ReportObjectModelProxy exprHost, Func<object> expressionExecution)
		{
			try
			{
				if (!this.EvaluateComplexExpression(expression, ref result, exprHost))
				{
					Global.Tracer.Assert(exprHost != null && expression.ExprHostID >= 0, "(exprHost != null && expression.ExprHostID >= 0)");
					result.Value = expressionExecution();
					if (this.m_reportObjectModel.OdpContext.CompareSafeExpressionsToLegacy)
					{
						this.SafeExpressionsComparator.Compare(expression, result.Value);
					}
				}
			}
			catch (Exception ex)
			{
				this.RegisterRuntimeErrorInExpressionAndStop(ref result, ex);
			}
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x001504C0 File Offset: 0x0014E6C0
		private void EvaluateRdlFunction(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> expressions = expression.RdlFunctionInfo.Expressions;
			object[] array = new object[expressions.Count];
			for (int i = 0; i < expressions.Count; i++)
			{
				VariantResult variantResult;
				if (!this.EvaluateSimpleExpression(expressions[i], out variantResult) && !this.EvaluateComplexExpression(expressions[i], ref variantResult, null))
				{
					Global.Tracer.Assert(false, "Rdl function argument is complex.");
				}
				array[i] = variantResult.Value;
				if (variantResult.ErrorOccurred)
				{
					result = variantResult;
					return;
				}
			}
			switch (expression.RdlFunctionInfo.FunctionType)
			{
			case RdlFunctionInfo.RdlFunctionType.MinValue:
				result.Value = this.MinValue(array);
				return;
			case RdlFunctionInfo.RdlFunctionType.MaxValue:
				result.Value = this.MaxValue(array);
				return;
			case RdlFunctionInfo.RdlFunctionType.ScopeKeys:
			case RdlFunctionInfo.RdlFunctionType.Comparable:
				result.Value = this.Comparable(array);
				return;
			case RdlFunctionInfo.RdlFunctionType.Array:
				result.Value = array;
				return;
			default:
				Global.Tracer.Assert(false, "No case for: " + expression.RdlFunctionInfo.FunctionType.ToString());
				return;
			}
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x001505D0 File Offset: 0x0014E7D0
		private void RegisterRuntimeWarning(Exception e, IErrorContext iErrorContext)
		{
			if (e is ReportProcessingException_NoRowsFieldAccess)
			{
				iErrorContext.Register(ProcessingErrorCode.rsRuntimeErrorInExpression, Severity.Warning, new string[] { e.Message });
				return;
			}
			if (Global.Tracer.TraceError)
			{
				Global.Tracer.Trace("Caught unexpected exception inside of RegisterRuntimeWarning.");
			}
			throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x00150628 File Offset: 0x0014E828
		private void RegisterRuntimeErrorInExpressionAndStop(ref VariantResult result, Exception e)
		{
			this.RegisterRuntimeErrorInExpression(ref result, e, this, true);
			if (!result.ErrorOccurred)
			{
				return;
			}
			ProcessingMessageList messages = this.m_errorContext.Messages;
			if (messages != null && messages.Count > 0)
			{
				throw new ReportProcessingException(messages[0].Message, messages, ErrorCode.rsProcessingError);
			}
			throw new ReportProcessingException(messages);
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x0015067B File Offset: 0x0014E87B
		private void RegisterRuntimeErrorInExpression(ref VariantResult result, Exception e)
		{
			this.RegisterRuntimeErrorInExpression(ref result, e, this, false);
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x00150688 File Offset: 0x0014E888
		private void RegisterRuntimeErrorInExpression(ref VariantResult result, Exception e, IErrorContext iErrorContext, bool isError)
		{
			if (e is RSException || AsynchronousExceptionDetection.IsStoppingException(e))
			{
				throw new ReportProcessingException(e.GetType().FullName + ": " + e.Message, ErrorCode.rsProcessingError, e);
			}
			if (e is ReportProcessingException_FieldError)
			{
				result.FieldStatus = ((ReportProcessingException_FieldError)e).Status;
				if (DataFieldStatus.IsMissing == result.FieldStatus)
				{
					result = new VariantResult(false, null);
					return;
				}
				result = new VariantResult(true, null);
				return;
			}
			else
			{
				if (e is ReportProcessingException_InvalidOperationException)
				{
					result = new VariantResult(true, null);
					return;
				}
				if (e is ReportProcessingException_UserProfilesDependencies)
				{
					iErrorContext.Register(ProcessingErrorCode.rsRuntimeUserProfileDependency, Severity.Error, null);
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
				string text;
				if (e != null)
				{
					try
					{
						text = ((e.InnerException == null) ? e.Message : e.InnerException.Message);
						goto IL_00DE;
					}
					catch
					{
						text = RPRes.NonClsCompliantException;
						goto IL_00DE;
					}
				}
				text = RPRes.NonClsCompliantException;
				IL_00DE:
				iErrorContext.Register(ProcessingErrorCode.rsRuntimeErrorInExpression, isError ? Severity.Error : Severity.Warning, new string[] { text });
				if (e is ReportProcessingException_NoRowsFieldAccess)
				{
					result = new VariantResult(false, null);
					return;
				}
				if (isError)
				{
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
				result = new VariantResult(true, null);
				StringBuilder stringBuilder = new StringBuilder();
				for (Exception ex = e; ex != null; ex = ex.InnerException)
				{
					if (ex.Message != null)
					{
						stringBuilder.Append(ex.Message);
						stringBuilder.Append(";");
					}
					if (ex.Source != null)
					{
						stringBuilder.Append(" Source: ");
						stringBuilder.Append(ex.Source);
						stringBuilder.Append(";");
					}
				}
				result.ExceptionMessage = stringBuilder.ToString();
				return;
			}
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x00150848 File Offset: 0x0014EA48
		private bool EvaluateBooleanExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, bool canBeConstant, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			if (expression != null && expression.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant)
			{
				result = default(VariantResult);
				if (canBeConstant)
				{
					result.Value = expression.BoolValue;
					result.TypeCode = TypeCode.Boolean;
				}
				else
				{
					result.ErrorOccurred = true;
					this.RegisterInvalidExpressionDataTypeWarning();
				}
				return true;
			}
			return this.EvaluateSimpleExpression(expression, objectType, objectName, propertyName, out result);
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x001508A6 File Offset: 0x0014EAA6
		private BooleanResult ProcessBooleanResult(VariantResult result)
		{
			return this.ProcessBooleanResult(result, false, this.m_objectType, null);
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x001508B8 File Offset: 0x0014EAB8
		private BooleanResult ProcessBooleanResult(VariantResult result, bool stopOnError, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName)
		{
			BooleanResult booleanResult = default(BooleanResult);
			bool flag;
			if (result.ErrorOccurred)
			{
				booleanResult.ErrorOccurred = true;
				if (stopOnError && result.FieldStatus != DataFieldStatus.None)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsFieldErrorInExpression, Severity.Error, objectType, objectName, "Hidden", new string[] { ReportRuntime.GetErrorName(result.FieldStatus, result.ExceptionMessage) });
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
			}
			else if (ReportRuntime.TryProcessObjectToBoolean(result.Value, out flag))
			{
				booleanResult.Value = flag;
			}
			else
			{
				booleanResult.ErrorOccurred = true;
				if (stopOnError)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidExpressionDataType, Severity.Error, objectType, objectName, "Hidden", Array.Empty<string>());
					throw new ReportProcessingException(this.m_errorContext.Messages);
				}
				this.RegisterInvalidExpressionDataTypeWarning();
			}
			return booleanResult;
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x0015098E File Offset: 0x0014EB8E
		internal static bool TryProcessObjectToBoolean(object value, out bool processedValue)
		{
			if (value is bool)
			{
				processedValue = (bool)value;
				return true;
			}
			if (value == null || DBNull.Value == value)
			{
				processedValue = false;
				return true;
			}
			processedValue = false;
			return false;
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x001509B6 File Offset: 0x0014EBB6
		private bool EvaluateBinaryExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			return this.EvaluateNoConstantExpression(expression, objectType, objectName, propertyName, out result);
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x001509C8 File Offset: 0x0014EBC8
		private BinaryResult ProcessBinaryResult(VariantResult result)
		{
			BinaryResult binaryResult = default(BinaryResult);
			object value = result.Value;
			if (result.ErrorOccurred)
			{
				binaryResult.ErrorOccurred = true;
				binaryResult.FieldStatus = result.FieldStatus;
			}
			else if (value is byte[])
			{
				byte[] array = (byte[])value;
				if (this.ViolatesMaxArrayResultLength(array.Length))
				{
					binaryResult.ErrorOccurred = true;
					binaryResult.Value = null;
					this.RegisterSandboxMaxArrayLengthWarning();
				}
				else
				{
					binaryResult.Value = array;
				}
			}
			else if (value == null || DBNull.Value == value)
			{
				binaryResult.Value = null;
			}
			else
			{
				if (value is string)
				{
					try
					{
						string text = (string)value;
						if (this.ViolatesMaxStringResultLength(text))
						{
							binaryResult.ErrorOccurred = true;
							binaryResult.Value = null;
							this.RegisterSandboxMaxStringLengthWarning();
						}
						else
						{
							byte[] array2 = Convert.FromBase64String(text);
							if (array2 != null && this.ViolatesMaxArrayResultLength(array2.Length))
							{
								binaryResult.ErrorOccurred = true;
								binaryResult.Value = null;
								this.RegisterSandboxMaxArrayLengthWarning();
							}
							else
							{
								binaryResult.Value = array2;
							}
						}
						return binaryResult;
					}
					catch (FormatException)
					{
						binaryResult.ErrorOccurred = true;
						this.RegisterInvalidExpressionDataTypeWarning();
						return binaryResult;
					}
				}
				binaryResult.ErrorOccurred = true;
				this.RegisterInvalidExpressionDataTypeWarning();
			}
			return binaryResult;
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x00150AFC File Offset: 0x0014ECFC
		private StringResult ProcessAutocastStringResult(VariantResult result)
		{
			return this.ProcessStringResult(result, true);
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x00150B06 File Offset: 0x0014ED06
		private StringResult ProcessStringResult(VariantResult result)
		{
			return this.ProcessStringResult(result, false);
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x00150B10 File Offset: 0x0014ED10
		private StringResult ProcessStringResult(VariantResult result, bool autocast)
		{
			bool flag;
			return new StringResult
			{
				Value = this.ProcessVariantResultToString(result, autocast, Severity.Warning, out flag),
				ErrorOccurred = flag,
				FieldStatus = result.FieldStatus
			};
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x00150B50 File Offset: 0x0014ED50
		private void ProcessLabelResult(ref VariantResult result)
		{
			bool flag;
			result.Value = this.ProcessVariantResultToString(result, true, Severity.Error, out flag);
			result.ErrorOccurred = flag;
			if (flag)
			{
				throw new ReportProcessingException(this.m_errorContext.Messages);
			}
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x00150B90 File Offset: 0x0014ED90
		private string ProcessVariantResultToString(VariantResult result, bool autocast, Severity severity, out bool errorOccured)
		{
			string text = null;
			if (result.ErrorOccurred)
			{
				errorOccured = true;
			}
			else
			{
				errorOccured = !ReportRuntime.ProcessObjectToString(result.Value, autocast, out text);
				if (errorOccured)
				{
					this.RegisterInvalidExpressionDataTypeWarning(ProcessingErrorCode.rsInvalidExpressionDataType, severity);
				}
				else if (this.ViolatesMaxStringResultLength(text))
				{
					this.RegisterSandboxMaxStringLengthWarning();
				}
			}
			return text;
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x00150BE4 File Offset: 0x0014EDE4
		internal static bool ProcessObjectToString(object value, bool autocast, out string output)
		{
			output = null;
			bool flag = false;
			if (value == null || DBNull.Value == value)
			{
				output = null;
			}
			else if (value is string)
			{
				output = (string)value;
			}
			else if (value is char)
			{
				output = Convert.ToString((char)value, CultureInfo.CurrentCulture);
			}
			else if (value is Guid)
			{
				output = ((Guid)value).ToString();
			}
			else if (autocast)
			{
				output = value.ToString();
			}
			else
			{
				flag = true;
			}
			return !flag;
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x00150C68 File Offset: 0x0014EE68
		private bool EvaluateIntegerExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			if (expression != null && expression.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant)
			{
				result = default(VariantResult);
				if (expression.ConstantType == DataType.Integer)
				{
					result.Value = expression.IntValue;
					result.TypeCode = expression.ConstantTypeCode;
				}
				else
				{
					result.ErrorOccurred = true;
					result.FieldStatus = DataFieldStatus.UnSupportedDataType;
					this.RegisterInvalidExpressionDataTypeWarning();
				}
				return true;
			}
			return this.EvaluateSimpleExpression(expression, objectType, objectName, propertyName, out result);
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x00150CDC File Offset: 0x0014EEDC
		private IntegerResult ProcessIntegerResult(VariantResult result)
		{
			IntegerResult integerResult = default(IntegerResult);
			if (result.ErrorOccurred)
			{
				integerResult.ErrorOccurred = true;
				integerResult.FieldStatus = result.FieldStatus;
				return integerResult;
			}
			if (result.Value == null || result.Value == DBNull.Value)
			{
				return integerResult;
			}
			if (!ReportRuntime.SetVariantType(ref result))
			{
				integerResult.ErrorOccurred = true;
				this.RegisterInvalidExpressionDataTypeWarning();
				return integerResult;
			}
			if (result.TypeCode == TypeCode.Object)
			{
				this.ConvertFromSqlTypes(ref result);
			}
			switch (result.TypeCode)
			{
			case TypeCode.SByte:
				integerResult.Value = Convert.ToInt32((sbyte)result.Value);
				return integerResult;
			case TypeCode.Byte:
				integerResult.Value = Convert.ToInt32((byte)result.Value);
				return integerResult;
			case TypeCode.Int16:
				integerResult.Value = Convert.ToInt32((short)result.Value);
				return integerResult;
			case TypeCode.UInt16:
				integerResult.Value = Convert.ToInt32((ushort)result.Value);
				return integerResult;
			case TypeCode.Int32:
				integerResult.Value = (int)result.Value;
				return integerResult;
			case TypeCode.UInt32:
				try
				{
					integerResult.Value = Convert.ToInt32((uint)result.Value);
					return integerResult;
				}
				catch (OverflowException)
				{
					integerResult.ErrorOccurred = true;
					return integerResult;
				}
				break;
			case TypeCode.Int64:
				break;
			case TypeCode.UInt64:
				goto IL_0186;
			case TypeCode.Single:
				goto IL_01DC;
			case TypeCode.Double:
				goto IL_01B1;
			case TypeCode.Decimal:
				goto IL_0201;
			default:
				goto IL_0226;
			}
			try
			{
				integerResult.Value = Convert.ToInt32((long)result.Value);
				return integerResult;
			}
			catch (OverflowException)
			{
				integerResult.ErrorOccurred = true;
				return integerResult;
			}
			IL_0186:
			try
			{
				integerResult.Value = Convert.ToInt32((ulong)result.Value);
				return integerResult;
			}
			catch (OverflowException)
			{
				integerResult.ErrorOccurred = true;
				return integerResult;
			}
			IL_01B1:
			try
			{
				integerResult.Value = Convert.ToInt32((double)result.Value);
				return integerResult;
			}
			catch (OverflowException)
			{
				integerResult.ErrorOccurred = true;
				return integerResult;
			}
			IL_01DC:
			try
			{
				integerResult.Value = Convert.ToInt32((float)result.Value);
				return integerResult;
			}
			catch (OverflowException)
			{
				integerResult.ErrorOccurred = true;
				return integerResult;
			}
			IL_0201:
			try
			{
				integerResult.Value = Convert.ToInt32((decimal)result.Value);
				return integerResult;
			}
			catch (OverflowException)
			{
				integerResult.ErrorOccurred = true;
				return integerResult;
			}
			IL_0226:
			if (result.Value is TimeSpan)
			{
				try
				{
					integerResult.Value = Convert.ToInt32(((TimeSpan)result.Value).Ticks);
					return integerResult;
				}
				catch (OverflowException)
				{
					integerResult.ErrorOccurred = true;
					return integerResult;
				}
			}
			integerResult.ErrorOccurred = true;
			this.RegisterInvalidExpressionDataTypeWarning();
			return integerResult;
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x00150FB0 File Offset: 0x0014F1B0
		private bool EvaluateIntegerOrFloatExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			if (expression != null && expression.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant)
			{
				result = default(VariantResult);
				if (expression.ConstantType == DataType.Integer)
				{
					result.Value = expression.IntValue;
					result.TypeCode = expression.ConstantTypeCode;
				}
				else if (expression.ConstantType == DataType.Float)
				{
					result.Value = expression.FloatValue;
					result.TypeCode = expression.ConstantTypeCode;
				}
				else
				{
					result = default(VariantResult);
					result.ErrorOccurred = true;
					result.FieldStatus = DataFieldStatus.UnSupportedDataType;
					this.RegisterInvalidExpressionDataTypeWarning();
				}
				return true;
			}
			return this.EvaluateSimpleExpression(expression, objectType, objectName, propertyName, out result);
		}

		// Token: 0x06005069 RID: 20585 RVA: 0x00151058 File Offset: 0x0014F258
		private FloatResult ProcessIntegerOrFloatResult(VariantResult result)
		{
			FloatResult floatResult = default(FloatResult);
			if (result.ErrorOccurred)
			{
				floatResult.ErrorOccurred = true;
				floatResult.FieldStatus = result.FieldStatus;
				return floatResult;
			}
			if (result.Value == null || result.Value == DBNull.Value)
			{
				return floatResult;
			}
			if (!ReportRuntime.SetVariantType(ref result))
			{
				floatResult.ErrorOccurred = true;
				this.RegisterInvalidExpressionDataTypeWarning();
				return floatResult;
			}
			if (result.TypeCode == TypeCode.Object)
			{
				this.ConvertFromSqlTypes(ref result);
			}
			switch (result.TypeCode)
			{
			case TypeCode.SByte:
				floatResult.Value = (double)Convert.ToInt32((sbyte)result.Value);
				return floatResult;
			case TypeCode.Byte:
				floatResult.Value = (double)Convert.ToInt32((byte)result.Value);
				return floatResult;
			case TypeCode.Int16:
				floatResult.Value = (double)Convert.ToInt32((short)result.Value);
				return floatResult;
			case TypeCode.UInt16:
				floatResult.Value = (double)Convert.ToInt32((ushort)result.Value);
				return floatResult;
			case TypeCode.Int32:
				floatResult.Value = (double)((int)result.Value);
				return floatResult;
			case TypeCode.UInt32:
				try
				{
					floatResult.Value = (double)Convert.ToInt32((uint)result.Value);
					return floatResult;
				}
				catch (OverflowException)
				{
					floatResult.ErrorOccurred = true;
					return floatResult;
				}
				break;
			case TypeCode.Int64:
				break;
			case TypeCode.UInt64:
				goto IL_01C0;
			case TypeCode.Single:
				floatResult.Value = Convert.ToDouble((float)result.Value);
				return floatResult;
			case TypeCode.Double:
				floatResult.Value = (double)result.Value;
				return floatResult;
			case TypeCode.Decimal:
				goto IL_01E6;
			default:
				goto IL_020B;
			}
			try
			{
				floatResult.Value = (double)Convert.ToInt32((long)result.Value);
				return floatResult;
			}
			catch (OverflowException)
			{
				floatResult.ErrorOccurred = true;
				return floatResult;
			}
			IL_01C0:
			try
			{
				floatResult.Value = (double)Convert.ToInt32((ulong)result.Value);
				return floatResult;
			}
			catch (OverflowException)
			{
				floatResult.ErrorOccurred = true;
				return floatResult;
			}
			IL_01E6:
			try
			{
				floatResult.Value = Convert.ToDouble((decimal)result.Value);
				return floatResult;
			}
			catch (OverflowException)
			{
				floatResult.ErrorOccurred = true;
				return floatResult;
			}
			IL_020B:
			if (result.Value is TimeSpan)
			{
				try
				{
					floatResult.Value = (double)Convert.ToInt32(((TimeSpan)result.Value).Ticks);
					return floatResult;
				}
				catch (OverflowException)
				{
					floatResult.ErrorOccurred = true;
					return floatResult;
				}
			}
			floatResult.ErrorOccurred = true;
			this.RegisterInvalidExpressionDataTypeWarning();
			return floatResult;
		}

		// Token: 0x0600506A RID: 20586 RVA: 0x001512FC File Offset: 0x0014F4FC
		private void ProcessLookupVariantResult(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, IErrorContext errorContext, bool isArrayRequired, bool normalizeDBNullToNull, ref VariantResult result)
		{
			if (expression != null && expression.IsExpression && !result.ErrorOccurred)
			{
				object obj;
				TypeCode typeCode;
				ReportRuntime.NormalizationCode normalizationCode;
				if (!this.NormalizeVariantValue(result, isArrayRequired, isArrayRequired, false, normalizeDBNullToNull, out obj, out typeCode, out normalizationCode))
				{
					result.ErrorOccurred = true;
					switch (normalizationCode)
					{
					case ReportRuntime.NormalizationCode.InvalidType:
						result.FieldStatus = DataFieldStatus.UnSupportedDataType;
						errorContext.Register(ProcessingErrorCode.rsLookupOfInvalidExpressionDataType, Severity.Warning, Array.Empty<string>());
						break;
					case ReportRuntime.NormalizationCode.StringLengthViolation:
						errorContext.Register(ProcessingErrorCode.rsSandboxingStringResultExceedsMaximumLength, Severity.Warning, new string[] { Convert.ToString(this.m_maxStringResultLength, CultureInfo.InvariantCulture) });
						break;
					case ReportRuntime.NormalizationCode.ArrayLengthViolation:
						errorContext.Register(ProcessingErrorCode.rsSandboxingArrayResultExceedsMaximumLength, Severity.Warning, new string[] { Convert.ToString(this.m_maxArrayResultLength, CultureInfo.InvariantCulture) });
						break;
					}
				}
				result.Value = obj;
				result.TypeCode = typeCode;
			}
		}

		// Token: 0x0600506B RID: 20587 RVA: 0x001513D8 File Offset: 0x0014F5D8
		private bool NormalizeVariantValue(VariantResult result, bool isArrayAllowed, bool isArrayRequired, bool isByteArrayAllowed, bool normalizeDBNullToNull, out object normalizedValue, out TypeCode typeCode, out ReportRuntime.NormalizationCode normalCode)
		{
			if (!isArrayRequired && ReportRuntime.GetVariantTypeCode(result.Value, out typeCode))
			{
				if (typeCode == TypeCode.String && this.ViolatesMaxStringResultLength((string)result.Value))
				{
					normalizedValue = null;
					typeCode = TypeCode.Empty;
					normalCode = ReportRuntime.NormalizationCode.StringLengthViolation;
					return false;
				}
				normalizedValue = result.Value;
			}
			else if (!isArrayRequired && (result.Value == null || result.Value == DBNull.Value))
			{
				if (normalizeDBNullToNull)
				{
					normalizedValue = null;
				}
				else
				{
					normalizedValue = DBNull.Value;
				}
				typeCode = TypeCode.Empty;
			}
			else if (isArrayAllowed && result.Value is object[])
			{
				object[] array = (object[])result.Value;
				if (this.ViolatesMaxArrayResultLength(array.Length))
				{
					normalizedValue = null;
					typeCode = TypeCode.Empty;
					normalCode = ReportRuntime.NormalizationCode.ArrayLengthViolation;
					return false;
				}
				if (!this.NormalizeVariantArray(array.GetEnumerator(), array, normalizeDBNullToNull, out normalizedValue, out typeCode, out normalCode))
				{
					return false;
				}
			}
			else if (!isArrayRequired && isByteArrayAllowed && result.Value is byte[])
			{
				byte[] array2 = (byte[])result.Value;
				if (this.ViolatesMaxArrayResultLength(array2.Length))
				{
					normalizedValue = null;
					typeCode = TypeCode.Empty;
					normalCode = ReportRuntime.NormalizationCode.ArrayLengthViolation;
					return false;
				}
				normalizedValue = result.Value;
				typeCode = TypeCode.Object;
			}
			else if (isArrayAllowed && result.Value is IList)
			{
				IList list = (IList)result.Value;
				if (this.ViolatesMaxArrayResultLength(list.Count))
				{
					normalizedValue = null;
					typeCode = TypeCode.Empty;
					normalCode = ReportRuntime.NormalizationCode.ArrayLengthViolation;
					return false;
				}
				object[] array3 = new object[list.Count];
				if (!this.NormalizeVariantArray(list.GetEnumerator(), array3, normalizeDBNullToNull, out normalizedValue, out typeCode, out normalCode))
				{
					return false;
				}
			}
			else if (!isArrayRequired && result.Value is Guid)
			{
				normalizedValue = ((Guid)result.Value).ToString();
				typeCode = TypeCode.String;
			}
			else
			{
				if (result.Value == null || !this.ConvertFromSqlTypes(ref result))
				{
					typeCode = TypeCode.Empty;
					normalizedValue = null;
					normalCode = ReportRuntime.NormalizationCode.InvalidType;
					return false;
				}
				typeCode = TypeCode.Object;
				normalizedValue = result.Value;
			}
			normalCode = ReportRuntime.NormalizationCode.Success;
			return true;
		}

		// Token: 0x0600506C RID: 20588 RVA: 0x001515C4 File Offset: 0x0014F7C4
		private bool NormalizeVariantArray(IEnumerator source, object[] destArr, bool normalizeDBNullToNull, out object normalizedValue, out TypeCode typeCode, out ReportRuntime.NormalizationCode normalCode)
		{
			int num = 0;
			while (source.MoveNext())
			{
				if (!this.NormalizeVariantValue(new VariantResult
				{
					Value = source.Current
				}, false, false, false, normalizeDBNullToNull, out normalizedValue, out typeCode, out normalCode))
				{
					return false;
				}
				destArr[num] = normalizedValue;
				num++;
			}
			normalizedValue = destArr;
			typeCode = TypeCode.Object;
			normalCode = ReportRuntime.NormalizationCode.Success;
			return true;
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x00151620 File Offset: 0x0014F820
		private void ProcessReportParameterVariantResult(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result)
		{
			if (result.ErrorOccurred)
			{
				throw new ReportProcessingException(this.m_errorContext.Messages);
			}
			if (expression != null && expression.IsExpression && !result.ErrorOccurred)
			{
				object obj;
				TypeCode typeCode;
				ReportRuntime.NormalizationCode normalizationCode;
				if (!this.NormalizeVariantValue(result, true, false, false, true, out obj, out typeCode, out normalizationCode))
				{
					result.ErrorOccurred = true;
					switch (normalizationCode)
					{
					case ReportRuntime.NormalizationCode.InvalidType:
						this.RegisterInvalidExpressionDataTypeWarning(ProcessingErrorCode.rsInvalidExpressionDataType, Severity.Error);
						break;
					case ReportRuntime.NormalizationCode.StringLengthViolation:
						this.RegisterSandboxMaxStringLengthWarning();
						break;
					case ReportRuntime.NormalizationCode.ArrayLengthViolation:
						this.RegisterSandboxMaxArrayLengthWarning();
						break;
					}
				}
				result.Value = obj;
				result.TypeCode = typeCode;
			}
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x001516B8 File Offset: 0x0014F8B8
		private bool ViolatesMaxStringResultLength(string value)
		{
			return this.m_maxStringResultLength != -1 && value != null && value.Length > this.m_maxStringResultLength;
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x001516D6 File Offset: 0x0014F8D6
		private bool ViolatesMaxArrayResultLength(int count)
		{
			return this.m_maxArrayResultLength != -1 && count > this.m_maxArrayResultLength;
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x001516EC File Offset: 0x0014F8EC
		private void ProcessSerializableResult(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, bool isReportScope, ref VariantResult result)
		{
			if (expression != null && expression.IsExpression && !result.ErrorOccurred)
			{
				this.ProcessSerializableResult(isReportScope, ref result);
			}
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x0015170C File Offset: 0x0014F90C
		internal bool ProcessSerializableResult(bool isReportScope, ref VariantResult result)
		{
			bool flag = false;
			object obj;
			TypeCode typeCode;
			ReportRuntime.NormalizationCode normalizationCode;
			if (!this.NormalizeVariantValue(result, true, false, !this.m_rdlSandboxingEnabled, true, out obj, out typeCode, out normalizationCode))
			{
				result.ErrorOccurred = true;
				switch (normalizationCode)
				{
				case ReportRuntime.NormalizationCode.InvalidType:
					if (isReportScope)
					{
						try
						{
							if (result.Value is ISerializable || (result.Value.GetType().Attributes & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
							{
								result.TypeCode = TypeCode.Object;
								if (this.m_isSerializableValuesProhibited)
								{
									((IErrorContext)this).Register(ProcessingErrorCode.rsSerializableTypeNotSupported, Severity.Error, new string[]
									{
										this.m_objectType.ToString(),
										this.m_objectName
									});
									result.Value = null;
									return false;
								}
								if (!this.m_rdlSandboxingEnabled)
								{
									result.ErrorOccurred = false;
									return false;
								}
							}
							else
							{
								flag = true;
								result.Value = null;
							}
							goto IL_0107;
						}
						catch (Exception ex)
						{
							((IErrorContext)this).Register(ProcessingErrorCode.rsUnexpectedSerializationError, Severity.Warning, new string[] { ex.Message });
							result.Value = null;
							goto IL_0107;
						}
					}
					result.Value = null;
					IL_0107:
					this.RegisterInvalidExpressionDataTypeWarning();
					break;
				case ReportRuntime.NormalizationCode.StringLengthViolation:
					this.RegisterSandboxMaxStringLengthWarning();
					break;
				case ReportRuntime.NormalizationCode.ArrayLengthViolation:
					this.RegisterSandboxMaxArrayLengthWarning();
					break;
				}
			}
			else
			{
				result.Value = obj;
				result.TypeCode = typeCode;
			}
			return flag;
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x0015185C File Offset: 0x0014FA5C
		private void ProcessVariantResult(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result)
		{
			this.ProcessVariantResult(expression, ref result, false);
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x00151868 File Offset: 0x0014FA68
		private void ProcessVariantResult(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result, bool isArrayAllowed)
		{
			if (expression != null && expression.IsExpression && !result.ErrorOccurred)
			{
				object obj;
				TypeCode typeCode;
				ReportRuntime.NormalizationCode normalizationCode;
				if (!this.NormalizeVariantValue(result, isArrayAllowed, false, false, true, out obj, out typeCode, out normalizationCode))
				{
					result.ErrorOccurred = true;
					switch (normalizationCode)
					{
					case ReportRuntime.NormalizationCode.InvalidType:
						this.RegisterInvalidExpressionDataTypeWarning();
						break;
					case ReportRuntime.NormalizationCode.StringLengthViolation:
						this.RegisterSandboxMaxStringLengthWarning();
						break;
					case ReportRuntime.NormalizationCode.ArrayLengthViolation:
						this.RegisterSandboxMaxArrayLengthWarning();
						break;
					}
				}
				result.Value = obj;
				result.TypeCode = typeCode;
			}
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x001518E4 File Offset: 0x0014FAE4
		private void VerifyVariantResultAndStopOnError(ref VariantResult result)
		{
			if (result.ErrorOccurred)
			{
				if (result.FieldStatus != DataFieldStatus.None)
				{
					((IErrorContext)this).Register(ProcessingErrorCode.rsFieldErrorInExpression, Severity.Error, new string[] { ReportRuntime.GetErrorName(result.FieldStatus, result.ExceptionMessage) });
				}
				else
				{
					((IErrorContext)this).Register(ProcessingErrorCode.rsInvalidExpressionDataType, Severity.Error, Array.Empty<string>());
				}
				throw new ReportProcessingException(this.m_errorContext.Messages);
			}
			if (result.Value == null)
			{
				result.Value = DBNull.Value;
			}
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x00151960 File Offset: 0x0014FB60
		private void ProcessVariantOrBinaryResult(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, ref VariantResult result, bool isAggregate, bool allowArray)
		{
			if (expression != null && expression.IsExpression && !result.ErrorOccurred)
			{
				object obj;
				TypeCode typeCode;
				ReportRuntime.NormalizationCode normalizationCode;
				if (!this.NormalizeVariantValue(result, allowArray, false, true, true, out obj, out typeCode, out normalizationCode))
				{
					result.ErrorOccurred = true;
					if (isAggregate)
					{
						result.FieldStatus = DataFieldStatus.UnSupportedDataType;
					}
					else
					{
						switch (normalizationCode)
						{
						case ReportRuntime.NormalizationCode.InvalidType:
							this.RegisterInvalidExpressionDataTypeWarning();
							break;
						case ReportRuntime.NormalizationCode.StringLengthViolation:
							this.RegisterSandboxMaxStringLengthWarning();
							break;
						case ReportRuntime.NormalizationCode.ArrayLengthViolation:
							this.RegisterSandboxMaxArrayLengthWarning();
							break;
						}
					}
				}
				result.Value = obj;
				result.TypeCode = typeCode;
			}
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x001519E8 File Offset: 0x0014FBE8
		private ParameterValueResult ProcessParameterValueResult(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, string paramName, VariantResult result)
		{
			ParameterValueResult parameterValueResult = default(ParameterValueResult);
			if (expression != null)
			{
				if (expression.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant)
				{
					parameterValueResult.Value = result.Value;
					parameterValueResult.Type = expression.ConstantType;
				}
				else if (result.ErrorOccurred)
				{
					parameterValueResult.ErrorOccurred = true;
				}
				else
				{
					object obj;
					DataType dataType;
					if (!this.NormalizeParameterVariantValue(result.Value, paramName, out obj, out dataType) && Type.GetTypeCode(result.Value.GetType()) == TypeCode.Object && !this.ConvertFromSqlTypes(ref result))
					{
						parameterValueResult.ErrorOccurred = true;
					}
					parameterValueResult.Value = obj;
					parameterValueResult.Type = dataType;
				}
			}
			return parameterValueResult;
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x00151A80 File Offset: 0x0014FC80
		private bool NormalizeParameterVariantValue(object value, string paramName, out object normalizedValue, out DataType dataType)
		{
			if (value == null || value == DBNull.Value)
			{
				normalizedValue = null;
				dataType = DataType.String;
			}
			else if (value is bool)
			{
				normalizedValue = value;
				dataType = DataType.Boolean;
			}
			else if (value is DateTime)
			{
				normalizedValue = value;
				dataType = DataType.DateTime;
			}
			else if (value is double || value is float || value is decimal)
			{
				normalizedValue = Convert.ToDouble(value, CultureInfo.CurrentCulture);
				dataType = DataType.Float;
			}
			else if (value is string)
			{
				string text = (string)value;
				if (this.ViolatesMaxStringResultLength(text))
				{
					dataType = DataType.String;
					normalizedValue = null;
					this.RegisterSandboxMaxStringLengthWarning();
					return false;
				}
				normalizedValue = text;
				dataType = DataType.String;
			}
			else if (value is char)
			{
				normalizedValue = Convert.ToString(value, CultureInfo.CurrentCulture);
				dataType = DataType.String;
			}
			else
			{
				if (!(value is int) && !(value is short) && !(value is byte) && !(value is sbyte) && !(value is ushort))
				{
					if (value is uint || value is long || value is ulong)
					{
						try
						{
							normalizedValue = Convert.ToInt32(value, CultureInfo.CurrentCulture);
							dataType = DataType.Integer;
							return true;
						}
						catch (OverflowException)
						{
							((IErrorContext)this).Register(ProcessingErrorCode.rsParameterValueCastFailure, Severity.Warning, this.m_objectType, this.m_objectName, this.m_propertyName, new string[] { paramName });
							normalizedValue = value;
							dataType = DataType.Integer;
							return false;
						}
					}
					if (value is TimeSpan)
					{
						try
						{
							normalizedValue = Convert.ToString(value, CultureInfo.CurrentCulture);
							dataType = DataType.String;
							return true;
						}
						catch (FormatException)
						{
							this.RegisterInvalidExpressionDataTypeWarning();
							normalizedValue = null;
							dataType = DataType.String;
							return false;
						}
					}
					if (value is DateTimeOffset)
					{
						normalizedValue = value;
						dataType = DataType.DateTime;
						return true;
					}
					if (value is Guid)
					{
						normalizedValue = ((Guid)value).ToString();
						dataType = DataType.String;
						return true;
					}
					if (value is object[])
					{
						object[] array = (object[])value;
						if (this.ViolatesMaxArrayResultLength(array.Length))
						{
							normalizedValue = null;
							dataType = DataType.String;
							return false;
						}
						if (!this.NormalizeParameterVariantArray(array.GetEnumerator(), array, paramName, out normalizedValue, out dataType))
						{
							return false;
						}
						return true;
					}
					else
					{
						if (!(value is IList))
						{
							this.RegisterInvalidExpressionDataTypeWarning();
							normalizedValue = null;
							dataType = DataType.String;
							return false;
						}
						IList list = (IList)value;
						if (this.ViolatesMaxArrayResultLength(list.Count))
						{
							normalizedValue = null;
							dataType = DataType.String;
							return false;
						}
						object[] array2 = new object[list.Count];
						if (!this.NormalizeParameterVariantArray(list.GetEnumerator(), array2, paramName, out normalizedValue, out dataType))
						{
							return false;
						}
						return true;
					}
					bool flag;
					return flag;
				}
				normalizedValue = Convert.ToInt32(value, CultureInfo.CurrentCulture);
				dataType = DataType.Integer;
			}
			return true;
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x00151D30 File Offset: 0x0014FF30
		private bool NormalizeParameterVariantArray(IEnumerator source, object[] destArr, string paramName, out object normalizedValue, out DataType dataType)
		{
			dataType = DataType.String;
			int num = 0;
			while (source.MoveNext())
			{
				if (!this.NormalizeParameterVariantValue(source.Current, paramName, out normalizedValue, out dataType))
				{
					return false;
				}
				destArr[num] = normalizedValue;
				num++;
			}
			normalizedValue = destArr;
			return true;
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x00151D74 File Offset: 0x0014FF74
		private static object[] GetAsObjectArray(ref VariantResult result)
		{
			object[] array = result.Value as object[];
			if (array == null)
			{
				IList list = result.Value as IList;
				if (list != null)
				{
					array = new object[list.Count];
					list.CopyTo(array, 0);
				}
			}
			return array;
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x00151DB4 File Offset: 0x0014FFB4
		private DataType GetDataType(object obj)
		{
			TypeCode typeCode = TypeCode.Empty;
			if (obj != null)
			{
				typeCode = Type.GetTypeCode(obj.GetType());
			}
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
			case TypeCode.Char:
			case TypeCode.String:
				return DataType.String;
			case TypeCode.Boolean:
				return DataType.Boolean;
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				return DataType.Integer;
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				return DataType.Float;
			case TypeCode.DateTime:
				return DataType.DateTime;
			}
			if (obj is TimeSpan)
			{
				return DataType.Integer;
			}
			if (obj is DateTimeOffset)
			{
				return DataType.DateTime;
			}
			return DataType.String;
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x00151E4C File Offset: 0x0015004C
		private void SetNullResult(ref VariantResult result)
		{
			result.Value = null;
			result.TypeCode = TypeCode.Empty;
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x00151E5C File Offset: 0x0015005C
		private void SetGuidResult(ref VariantResult result)
		{
			result.Value = ((Guid)result.Value).ToString();
			result.TypeCode = TypeCode.String;
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x00151E90 File Offset: 0x00150090
		private bool ConvertFromSqlTypes(ref VariantResult result)
		{
			if (result.Value is SqlInt32)
			{
				if (((SqlInt32)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					result.TypeCode = TypeCode.Int32;
					result.Value = ((SqlInt32)result.Value).Value;
				}
			}
			else if (result.Value is SqlInt16)
			{
				if (((SqlInt16)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					result.TypeCode = TypeCode.Int16;
					result.Value = ((SqlInt16)result.Value).Value;
				}
			}
			else if (result.Value is SqlInt64)
			{
				if (((SqlInt64)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					result.TypeCode = TypeCode.Int64;
					result.Value = ((SqlInt64)result.Value).Value;
				}
			}
			else if (result.Value is SqlBoolean)
			{
				if (((SqlBoolean)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					result.TypeCode = TypeCode.Boolean;
					result.Value = ((SqlBoolean)result.Value).Value;
				}
			}
			else if (result.Value is SqlDecimal)
			{
				if (((SqlDecimal)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					result.TypeCode = TypeCode.Decimal;
					result.Value = ((SqlDecimal)result.Value).Value;
				}
			}
			else if (result.Value is SqlDouble)
			{
				if (((SqlDouble)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					result.TypeCode = TypeCode.Double;
					result.Value = ((SqlDouble)result.Value).Value;
				}
			}
			else if (result.Value is SqlDateTime)
			{
				if (((SqlDateTime)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					result.TypeCode = TypeCode.DateTime;
					result.Value = ((SqlDateTime)result.Value).Value;
				}
			}
			else if (result.Value is SqlMoney)
			{
				if (((SqlMoney)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					result.TypeCode = TypeCode.Decimal;
					result.Value = ((SqlMoney)result.Value).Value;
				}
			}
			else if (result.Value is SqlSingle)
			{
				if (((SqlSingle)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					result.TypeCode = TypeCode.Single;
					result.Value = ((SqlSingle)result.Value).Value;
				}
			}
			else if (result.Value is SqlByte)
			{
				if (((SqlByte)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					result.TypeCode = TypeCode.Byte;
					result.Value = ((SqlByte)result.Value).Value;
				}
			}
			else if (result.Value is SqlString)
			{
				if (((SqlString)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					result.TypeCode = TypeCode.String;
					string text = ((SqlString)result.Value).Value;
					if (this.ViolatesMaxStringResultLength(text))
					{
						text = null;
						this.RegisterSandboxMaxStringLengthWarning();
					}
					result.Value = text;
				}
			}
			else
			{
				if (!(result.Value is SqlGuid))
				{
					return false;
				}
				if (((SqlGuid)result.Value).IsNull)
				{
					this.SetNullResult(ref result);
				}
				else
				{
					this.SetGuidResult(ref result);
				}
			}
			return true;
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x001522BF File Offset: 0x001504BF
		private bool EvaluateNoConstantExpression(Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, out VariantResult result)
		{
			if (expression != null && expression.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant)
			{
				result = new VariantResult(true, null);
				this.RegisterInvalidExpressionDataTypeWarning();
				return true;
			}
			return this.EvaluateSimpleExpression(expression, objectType, objectName, propertyName, out result);
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x001522F0 File Offset: 0x001504F0
		internal static bool GetVariantTypeCode(object o, out TypeCode typeCode)
		{
			if (o == null)
			{
				typeCode = TypeCode.Empty;
			}
			else
			{
				Type type = o.GetType();
				typeCode = Type.GetTypeCode(type);
				switch (typeCode)
				{
				case TypeCode.Empty:
				case TypeCode.DBNull:
					return false;
				case TypeCode.Boolean:
				case TypeCode.Char:
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
				case TypeCode.DateTime:
				case TypeCode.String:
					return true;
				}
				if (o is TimeSpan || o is DateTimeOffset)
				{
					return true;
				}
				if (o is SqlGeography || o is SqlGeometry)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x00152398 File Offset: 0x00150598
		internal static bool SetVariantType(ref VariantResult result)
		{
			TypeCode typeCode;
			if (ReportRuntime.GetVariantTypeCode(result.Value, out typeCode))
			{
				result.TypeCode = typeCode;
				return true;
			}
			return false;
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x001523C0 File Offset: 0x001505C0
		internal static string ConvertToStringFallBack(object value)
		{
			string text;
			try
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}", value);
			}
			catch (Exception)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x001523F8 File Offset: 0x001505F8
		private void RegisterInvalidExpressionDataTypeWarning()
		{
			this.RegisterInvalidExpressionDataTypeWarning(ProcessingErrorCode.rsInvalidExpressionDataType, Severity.Warning);
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x00152406 File Offset: 0x00150606
		private void RegisterInvalidExpressionDataTypeWarning(ProcessingErrorCode errorCode, Severity severity)
		{
			((IErrorContext)this).Register(errorCode, severity, Array.Empty<string>());
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x00152415 File Offset: 0x00150615
		private void RegisterSandboxMaxStringLengthWarning()
		{
			this.ReportObjectModel.OdpContext.TraceOneTimeWarning(ProcessingErrorCode.rsSandboxingStringResultExceedsMaximumLength);
			((IErrorContext)this).Register(ProcessingErrorCode.rsSandboxingStringResultExceedsMaximumLength, Severity.Warning, new string[] { Convert.ToString(this.m_maxStringResultLength, CultureInfo.InvariantCulture) });
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x00152451 File Offset: 0x00150651
		private void RegisterSandboxMaxArrayLengthWarning()
		{
			this.ReportObjectModel.OdpContext.TraceOneTimeWarning(ProcessingErrorCode.rsSandboxingArrayResultExceedsMaximumLength);
			((IErrorContext)this).Register(ProcessingErrorCode.rsSandboxingArrayResultExceedsMaximumLength, Severity.Warning, new string[] { Convert.ToString(this.m_maxArrayResultLength, CultureInfo.InvariantCulture) });
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x00152490 File Offset: 0x00150690
		internal object MinValue(params object[] arguments)
		{
			if (arguments != null)
			{
				object obj = arguments[0];
				for (int i = 1; i < arguments.Length; i++)
				{
					if (this.CompareWithExtendedTypesAndStopOnError(obj, arguments[i]) > 0)
					{
						obj = arguments[i];
					}
				}
				return obj;
			}
			return null;
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x001524C8 File Offset: 0x001506C8
		internal object MaxValue(params object[] arguments)
		{
			if (arguments != null)
			{
				object obj = arguments[0];
				for (int i = 1; i < arguments.Length; i++)
				{
					if (this.CompareWithExtendedTypesAndStopOnError(obj, arguments[i]) < 0)
					{
						obj = arguments[i];
					}
				}
				return obj;
			}
			return null;
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x00152500 File Offset: 0x00150700
		private object Comparable(params object[] arguments)
		{
			if (arguments != null)
			{
				string[] array = new string[arguments.Length];
				for (int i = 0; i < arguments.Length; i++)
				{
					array[i] = this.m_reportObjectModel.OdpContext.StringKeyGenerator.GetKey(arguments[i]);
				}
				return array;
			}
			return null;
		}

		// Token: 0x06005089 RID: 20617 RVA: 0x00152545 File Offset: 0x00150745
		private int CompareWithExtendedTypesAndStopOnError(object x, object y)
		{
			return this.m_reportObjectModel.OdpContext.CompareAndStopOnError(x, y, this.m_objectType, this.m_objectName, this.m_propertyName, true);
		}

		// Token: 0x0600508A RID: 20618 RVA: 0x0015256C File Offset: 0x0015076C
		internal int RecursiveLevel(string scope)
		{
			if (this.m_currentScope == null)
			{
				return 0;
			}
			int num = this.m_currentScope.RecursiveLevel(scope);
			if (-1 == num)
			{
				return 0;
			}
			return num;
		}

		// Token: 0x0600508B RID: 20619 RVA: 0x00152597 File Offset: 0x00150797
		internal string CreateDrillthroughContext()
		{
			if (this.m_drillthroughContextBuilder == null)
			{
				this.m_drillthroughContextBuilder = new Microsoft.ReportingServices.ReportIntermediateFormat.DrillthroughContextBuilder();
			}
			return this.m_drillthroughContextBuilder.CreateDrillthroughContext(this.m_fieldsUsedInCurrentActionOwnerValue, this.m_reportObjectModel.OdpContext.GetCurrentSpecialGroupingValues());
		}

		// Token: 0x0600508C RID: 20620 RVA: 0x001525D0 File Offset: 0x001507D0
		[PermissionSet(SecurityAction.Assert, Name = "FullTrust")]
		internal void LoadCompiledCode(IExpressionHostAssemblyHolder expressionHostAssemblyHolder, bool includeParameters, bool parametersOnly, Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel.ObjectModelImpl reportObjectModel, ReportRuntimeSetup runtimeSetup)
		{
			bool flag;
			if (expressionHostAssemblyHolder == null)
			{
				flag = false;
			}
			else
			{
				byte[] compiledCode = expressionHostAssemblyHolder.CompiledCode;
				int? num = ((compiledCode != null) ? new int?(compiledCode.Length) : null);
				int num2 = 0;
				flag = (num.GetValueOrDefault() > num2) & (num != null);
			}
			bool flag2 = flag;
			RSTrace.SanitizedRdlEngineHostTracer.Trace(string.Format("ProcessingCore, HasCompiledCode: {0}", flag2));
			if (flag2 && !this.m_useSafeExpressions)
			{
				try
				{
					if (runtimeSetup.RequireExpressionHostWithRefusedPermissions && !expressionHostAssemblyHolder.CompiledCodeGeneratedWithRefusedPermissions)
					{
						if (Global.Tracer.TraceError)
						{
							Global.Tracer.Trace("Expression host generated with refused permissions is required.");
						}
						throw new ReportProcessingException(ErrorCode.rsInvalidOperation);
					}
					if (runtimeSetup.ExprHostAppDomain == null || runtimeSetup.ExprHostAppDomain == AppDomain.CurrentDomain)
					{
						this.m_exprHostInSandboxAppDomain = false;
						if (expressionHostAssemblyHolder.CodeModules != null)
						{
							for (int i = 0; i < expressionHostAssemblyHolder.CodeModules.Count; i++)
							{
								if (!runtimeSetup.CheckCodeModuleIsTrustedInCurrentAppDomain(expressionHostAssemblyHolder.CodeModules[i]))
								{
									this.m_errorContext.Register(ProcessingErrorCode.rsUntrustedCodeModule, Severity.Error, expressionHostAssemblyHolder.ObjectType, null, null, new string[] { expressionHostAssemblyHolder.CodeModules[i] });
									throw new ReportProcessingException(this.m_errorContext.Messages);
								}
							}
						}
						this.m_reportExprHost = ReportRuntime.ExpressionHostLoader.LoadExprHostIntoCurrentAppDomain(expressionHostAssemblyHolder.CompiledCode, expressionHostAssemblyHolder.ExprHostAssemblyName, runtimeSetup.ExprHostEvidence, includeParameters, parametersOnly, reportObjectModel, expressionHostAssemblyHolder.CodeModules);
					}
					else
					{
						this.m_exprHostInSandboxAppDomain = true;
						this.m_reportExprHost = ReportRuntime.ExpressionHostLoader.LoadExprHost(expressionHostAssemblyHolder.CompiledCode, expressionHostAssemblyHolder.ExprHostAssemblyName, includeParameters, parametersOnly, reportObjectModel, expressionHostAssemblyHolder.CodeModules, runtimeSetup.ExprHostAppDomain);
					}
				}
				catch (ReportProcessingException)
				{
					throw;
				}
				catch (Exception ex)
				{
					this.ProcessLoadingExprHostException(expressionHostAssemblyHolder.ObjectType, ex, ProcessingErrorCode.rsErrorLoadingExprHostAssembly);
				}
			}
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x001527BC File Offset: 0x001509BC
		internal void CustomCodeOnInit(IExpressionHostAssemblyHolder expressionHostAssemblyHolder)
		{
			if (expressionHostAssemblyHolder.CompiledCode.Length != 0 && !this.m_useSafeExpressions)
			{
				try
				{
					this.m_reportExprHost.CustomCodeOnInit();
				}
				catch (ReportProcessingException)
				{
					throw;
				}
				catch (Exception ex)
				{
					this.ProcessLoadingExprHostException(expressionHostAssemblyHolder.ObjectType, ex, ProcessingErrorCode.rsErrorInOnInit);
				}
			}
		}

		// Token: 0x0600508E RID: 20622 RVA: 0x0015281C File Offset: 0x00150A1C
		internal void LogMetrics()
		{
			if (this.m_safeExpressionRuntime != null)
			{
				this.m_safeExpressionRuntime.LogMetrics();
			}
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x00152834 File Offset: 0x00150A34
		private void ProcessLoadingExprHostException(Microsoft.ReportingServices.ReportProcessing.ObjectType assemblyHolderObjectType, Exception e, ProcessingErrorCode errorCode)
		{
			if (e != null && e is TargetInvocationException && e.InnerException != null)
			{
				e = e.InnerException;
			}
			string text = null;
			string text2;
			if (e != null)
			{
				try
				{
					text2 = e.Message;
					text = e.ToString();
					goto IL_003F;
				}
				catch
				{
					text2 = RPRes.NonClsCompliantException;
					goto IL_003F;
				}
			}
			text2 = RPRes.NonClsCompliantException;
			IL_003F:
			ProcessingMessage processingMessage = this.m_errorContext.Register(errorCode, Severity.Error, assemblyHolderObjectType, null, null, new string[] { text2 });
			if (Global.Tracer.TraceError && processingMessage != null)
			{
				Global.Tracer.Trace(TraceLevel.Error, processingMessage.Message + Environment.NewLine + text);
			}
			throw new ReportProcessingException(this.m_errorContext.Messages);
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x001528E8 File Offset: 0x00150AE8
		internal void Close()
		{
			this.m_reportExprHost = null;
		}

		// Token: 0x17001E1B RID: 7707
		// (get) Token: 0x06005091 RID: 20625 RVA: 0x001528F1 File Offset: 0x00150AF1
		public int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x001528F9 File Offset: 0x00150AF9
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportRuntime;
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x00152900 File Offset: 0x00150B00
		public void SetID(int id)
		{
			this.m_id = id;
		}

		// Token: 0x04002873 RID: 10355
		private bool m_exprHostInSandboxAppDomain;

		// Token: 0x04002874 RID: 10356
		private ReportExprHost m_reportExprHost;

		// Token: 0x04002875 RID: 10357
		private Microsoft.ReportingServices.ReportProcessing.ObjectType m_objectType;

		// Token: 0x04002876 RID: 10358
		private string m_objectName;

		// Token: 0x04002877 RID: 10359
		private string m_propertyName;

		// Token: 0x04002878 RID: 10360
		private Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel.ObjectModelImpl m_reportObjectModel;

		// Token: 0x04002879 RID: 10361
		private ErrorContext m_errorContext;

		// Token: 0x0400287A RID: 10362
		private IErrorContext m_delayedErrorContext;

		// Token: 0x0400287B RID: 10363
		private bool m_contextUpdated;

		// Token: 0x0400287C RID: 10364
		private IScope m_currentScope;

		// Token: 0x0400287D RID: 10365
		private bool m_variableReferenceMode;

		// Token: 0x0400287E RID: 10366
		private bool m_unfulfilledDependency;

		// Token: 0x0400287F RID: 10367
		private ReportRuntime m_topLevelReportRuntime;

		// Token: 0x04002880 RID: 10368
		private Microsoft.ReportingServices.ReportIntermediateFormat.DrillthroughContextBuilder m_drillthroughContextBuilder;

		// Token: 0x04002881 RID: 10369
		private List<string> m_fieldsUsedInCurrentActionOwnerValue;

		// Token: 0x04002882 RID: 10370
		private int m_id = int.MinValue;

		// Token: 0x04002883 RID: 10371
		private int m_maxStringResultLength = -1;

		// Token: 0x04002884 RID: 10372
		private int m_maxArrayResultLength = -1;

		// Token: 0x04002885 RID: 10373
		private bool m_rdlSandboxingEnabled;

		// Token: 0x04002886 RID: 10374
		private bool m_isSerializableValuesProhibited;

		// Token: 0x04002887 RID: 10375
		private bool m_useSafeExpressions;

		// Token: 0x04002888 RID: 10376
		private readonly SafeExpressionsRuntime m_safeExpressionRuntime;

		// Token: 0x04002889 RID: 10377
		private SafeExpressionsComparator m_safeExpressionsComparator;

		// Token: 0x0400288A RID: 10378
		private const int UnrestrictedStringResultLength = -1;

		// Token: 0x0400288B RID: 10379
		private const int UnrestrictedArrayResultLength = -1;

		// Token: 0x020009AD RID: 2477
		// (Invoke) Token: 0x0600814B RID: 33099
		private delegate object EvalulateDataPoint(Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint);

		// Token: 0x020009AE RID: 2478
		internal sealed class TextRunExprHostWrapper : TextRunExprHost
		{
			// Token: 0x0600814E RID: 33102 RVA: 0x00214186 File Offset: 0x00212386
			internal TextRunExprHostWrapper(TextBoxExprHost textBoxExprHost)
			{
				this.m_textBoxExprHost = textBoxExprHost;
			}

			// Token: 0x170029C3 RID: 10691
			// (get) Token: 0x0600814F RID: 33103 RVA: 0x00214195 File Offset: 0x00212395
			public override object ValueExpr
			{
				get
				{
					return this.m_textBoxExprHost.ValueExpr;
				}
			}

			// Token: 0x04004530 RID: 17712
			private TextBoxExprHost m_textBoxExprHost;
		}

		// Token: 0x020009AF RID: 2479
		private enum NormalizationCode
		{
			// Token: 0x04004532 RID: 17714
			Success,
			// Token: 0x04004533 RID: 17715
			InvalidType,
			// Token: 0x04004534 RID: 17716
			StringLengthViolation,
			// Token: 0x04004535 RID: 17717
			ArrayLengthViolation
		}

		// Token: 0x020009B0 RID: 2480
		private sealed class ExpressionHostLoader : MarshalByRefObject
		{
			// Token: 0x06008150 RID: 33104 RVA: 0x002141A4 File Offset: 0x002123A4
			internal static ReportExprHost LoadExprHost(byte[] exprHostBytes, string exprHostAssemblyName, bool includeParameters, bool parametersOnly, OnDemandObjectModel objectModel, List<string> codeModules, AppDomain targetAppDomain)
			{
				Type expressionHostLoaderType = typeof(ReportRuntime.ExpressionHostLoader);
				ReportRuntime.ExpressionHostLoader remoteEHL = null;
				RevertImpersonationContext.RunFromRestrictedCasContext(delegate
				{
					remoteEHL = (ReportRuntime.ExpressionHostLoader)Activator.CreateInstance(targetAppDomain, expressionHostLoaderType.Assembly.FullName, expressionHostLoaderType.FullName).Unwrap();
				});
				return remoteEHL.LoadExprHostRemoteEntryPoint(exprHostBytes, exprHostAssemblyName, includeParameters, parametersOnly, objectModel, codeModules);
			}

			// Token: 0x06008151 RID: 33105 RVA: 0x002141F8 File Offset: 0x002123F8
			internal static ReportExprHost LoadExprHostIntoCurrentAppDomain(byte[] exprHostBytes, string exprHostAssemblyName, Evidence evidence, bool includeParameters, bool parametersOnly, OnDemandObjectModel objectModel, List<string> codeModules)
			{
				if (codeModules != null && 0 < codeModules.Count)
				{
					RevertImpersonationContext.RunFromRestrictedCasContext(delegate
					{
						for (int i = codeModules.Count - 1; i >= 0; i--)
						{
							Assembly.Load(codeModules[i]);
						}
					});
				}
				return (ReportExprHost)ReportRuntime.ExpressionHostLoader.LoadExprHostAssembly(exprHostBytes, exprHostAssemblyName, evidence).GetType("ReportExprHostImpl").GetConstructors()[0].Invoke(new object[] { includeParameters, parametersOnly, objectModel });
			}

			// Token: 0x06008152 RID: 33106 RVA: 0x0021427C File Offset: 0x0021247C
			private static Assembly LoadExprHostAssembly(byte[] exprHostBytes, string exprHostAssemblyName, Evidence evidence)
			{
				object syncRoot = ReportRuntime.ExpressionHostLoader.ExpressionHosts.SyncRoot;
				Assembly assembly2;
				lock (syncRoot)
				{
					Assembly assembly = (Assembly)ReportRuntime.ExpressionHostLoader.ExpressionHosts[exprHostAssemblyName];
					if (assembly == null)
					{
						if (evidence == null)
						{
							evidence = ReportRuntime.ExpressionHostLoader.CreateDefaultExpressionHostEvidence(exprHostAssemblyName);
						}
						try
						{
							new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Assert();
							assembly = Assembly.Load(exprHostBytes, null, evidence);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						ReportRuntime.ExpressionHostLoader.ExpressionHosts.Add(exprHostAssemblyName, assembly);
					}
					assembly2 = assembly;
				}
				return assembly2;
			}

			// Token: 0x06008153 RID: 33107 RVA: 0x00214318 File Offset: 0x00212518
			private static Evidence CreateDefaultExpressionHostEvidence(string exprHostAssemblyName)
			{
				Evidence evidence = new Evidence();
				evidence.AddHost(new Zone(SecurityZone.MyComputer));
				evidence.AddHost(new StrongName(new StrongNamePublicKeyBlob(ReportRuntime.ExpressionHostLoader.ReportExpressionsDefaultEvidencePK), exprHostAssemblyName, new Version("1.0.0.0")));
				return evidence;
			}

			// Token: 0x06008154 RID: 33108 RVA: 0x0021434B File Offset: 0x0021254B
			private ReportExprHost LoadExprHostRemoteEntryPoint(byte[] exprHostBytes, string exprHostAssemblyName, bool includeParameters, bool parametersOnly, OnDemandObjectModel objectModel, List<string> codeModules)
			{
				return ReportRuntime.ExpressionHostLoader.LoadExprHostIntoCurrentAppDomain(exprHostBytes, exprHostAssemblyName, null, includeParameters, parametersOnly, objectModel, codeModules);
			}

			// Token: 0x04004536 RID: 17718
			private static readonly Hashtable ExpressionHosts = new Hashtable();

			// Token: 0x04004537 RID: 17719
			private const string ExprHostRootType = "ReportExprHostImpl";

			// Token: 0x04004538 RID: 17720
			private static readonly byte[] ReportExpressionsDefaultEvidencePK = new byte[]
			{
				0, 36, 0, 0, 4, 128, 0, 0, 148, 0,
				0, 0, 6, 2, 0, 0, 0, 36, 0, 0,
				82, 83, 65, 49, 0, 4, 0, 0, 1, 0,
				1, 0, 81, 44, 142, 135, 46, 40, 86, 158,
				115, 59, 203, 18, 55, 148, 218, 181, 81, 17,
				160, 87, 11, 59, 61, 77, 227, 121, 65, 83,
				222, 165, 239, 183, 195, 254, 169, 242, 216, 35,
				108, byte.MaxValue, 50, 12, 79, 208, 234, 213, 246, 119,
				136, 11, 246, 193, 129, 242, 150, 199, 81, 197,
				246, 230, 91, 4, 211, 131, 76, 2, 247, 146,
				254, 224, 254, 69, 41, 21, 212, 74, 254, 116,
				160, 194, 126, 13, 142, 75, 141, 4, 236, 82,
				168, 226, 129, 224, 31, 244, 126, 125, 105, 78,
				108, 114, 117, 160, 154, 252, 191, 216, 204, 130,
				112, 90, 6, 178, 15, 214, 239, 97, 235, 186,
				104, 115, 226, 156, 140, 15, 44, 174, 221, 162
			};
		}
	}
}
