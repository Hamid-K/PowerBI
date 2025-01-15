using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using Microsoft.Reporting.Gauge.WebForms;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000DB RID: 219
	internal class GaugeMapper : MapperBase, IGaugeMapper, IDVMappingLayer, IDisposable
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x0002A713 File Offset: 0x00028913
		public GaugeMapper(GaugePanel gaugePanel, string defaultFontFamily)
			: base(defaultFontFamily)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002A730 File Offset: 0x00028930
		public void RenderGaugePanel()
		{
			try
			{
				if (this.m_gaugePanel != null)
				{
					this.InitializeGaugePanel();
					this.RenderBackFrame(this.m_gaugePanel.BackFrame, this.m_coreGaugeContainer.BackFrame, this.m_coreGaugeContainer);
					this.RenderGaugeLabels();
					this.RenderLinearGauges();
					this.RenderRadialGauges();
					this.RenderStateIndicators();
					this.RenderGaugePanelStyle();
					this.RenderGaugePanelTopImage();
					this.SetGaugePanelProperties();
					this.RenderData();
				}
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				throw new RenderingObjectModelException(ex);
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002A7D0 File Offset: 0x000289D0
		public void RenderDataGaugePanel()
		{
			this.RenderGaugePanel();
			this.AssignGaugeElementValues();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002A7E0 File Offset: 0x000289E0
		public Stream GetCoreXml()
		{
			Stream stream;
			try
			{
				this.m_coreGaugeContainer.Serializer.Content = 2;
				this.m_coreGaugeContainer.Serializer.NonSerializableContent = "";
				MemoryStream memoryStream = new MemoryStream();
				this.m_coreGaugeContainer.Serializer.Save(memoryStream);
				memoryStream.Position = 0L;
				stream = memoryStream;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				Global.Tracer.Trace(TraceLevel.Verbose, ex.Message);
				stream = null;
			}
			return stream;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002A868 File Offset: 0x00028A68
		public Stream GetImage(DynamicImageInstance.ImageType imageType)
		{
			Stream stream;
			try
			{
				if (this.m_coreGaugeContainer == null)
				{
					stream = null;
				}
				else
				{
					GaugeImageFormat gaugeImageFormat = 1;
					Stream stream2 = null;
					if (imageType != DynamicImageInstance.ImageType.PNG)
					{
						if (imageType == DynamicImageInstance.ImageType.EMF)
						{
							gaugeImageFormat = 5;
							stream2 = this.m_gaugePanel.RenderingContext.OdpContext.CreateStreamCallback(this.m_gaugePanel.Name, "emf", null, "image/emf", true, StreamOper.CreateOnly);
						}
					}
					else
					{
						gaugeImageFormat = 1;
						stream2 = new MemoryStream();
					}
					GaugeContainer coreGaugeContainer = this.m_coreGaugeContainer;
					coreGaugeContainer.FormatNumberHandler = (FormatNumberHandler)Delegate.Combine(coreGaugeContainer.FormatNumberHandler, new FormatNumberHandler(this.FormatNumber));
					this.m_coreGaugeContainer.MapEnabled = true;
					this.m_coreGaugeContainer.ImageResolution = base.DpiX;
					this.m_coreGaugeContainer.SaveAsImage(stream2, gaugeImageFormat);
					stream2.Position = 0L;
					stream = stream2;
				}
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				throw new RenderingObjectModelException(ex);
			}
			return stream;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002A95C File Offset: 0x00028B5C
		public ActionInfoWithDynamicImageMapCollection GetImageMaps()
		{
			return MappingHelper.GetImageMaps(this.GetMapAreaInfoList(), this.m_actions, this.m_gaugePanel);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002A975 File Offset: 0x00028B75
		internal IEnumerable<MappingHelper.MapAreaInfo> GetMapAreaInfoList()
		{
			float width = (float)this.m_coreGaugeContainer.Width;
			float height = (float)this.m_coreGaugeContainer.Height;
			foreach (object obj in this.m_coreGaugeContainer.MapAreas)
			{
				MapArea mapArea = (MapArea)obj;
				yield return new MappingHelper.MapAreaInfo(mapArea.ToolTip, mapArea.Tag, this.GetMapAreaShape(mapArea.Shape), MappingHelper.ConvertCoordinatesToRelative(mapArea.Coordinates, width, height));
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0002A988 File Offset: 0x00028B88
		private void InitializeGaugePanel()
		{
			this.m_coreGaugeContainer = new GaugeContainer();
			if (RSTrace.ProcessingTracer.TraceVerbose)
			{
				TraceManager traceManager = (TraceManager)this.m_coreGaugeContainer.gauge.GetService(typeof(TraceManager));
				traceManager.TraceContext = new GaugeMapper.TraceContext();
				traceManager.TraceContext.Write("GaugeMapper", RPRes.rsTraceGaugePanelInitialized);
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0002A9EC File Offset: 0x00028BEC
		private void RenderRadialGauges()
		{
			if (this.m_gaugePanel.RadialGauges == null)
			{
				return;
			}
			foreach (RadialGauge radialGauge in this.m_gaugePanel.RadialGauges)
			{
				this.RenderRadialGauge(radialGauge);
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002AA4C File Offset: 0x00028C4C
		private void RenderLinearGauges()
		{
			if (this.m_gaugePanel.LinearGauges == null)
			{
				return;
			}
			foreach (LinearGauge linearGauge in this.m_gaugePanel.LinearGauges)
			{
				this.RenderLinearGauge(linearGauge);
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002AAAC File Offset: 0x00028CAC
		private void RenderGaugeLabels()
		{
			if (this.m_gaugePanel.GaugeLabels == null)
			{
				return;
			}
			foreach (GaugeLabel gaugeLabel in this.m_gaugePanel.GaugeLabels)
			{
				this.RenderGaugeLabel(gaugeLabel);
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002AB0C File Offset: 0x00028D0C
		private void RenderStateIndicators()
		{
			if (this.m_gaugePanel.StateIndicators == null)
			{
				return;
			}
			foreach (StateIndicator stateIndicator in this.m_gaugePanel.StateIndicators)
			{
				this.RenderStateIndicator(stateIndicator);
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002AB6C File Offset: 0x00028D6C
		private void RenderRadialScales(RadialScaleCollection scaleCollection, CircularGauge circularGauge)
		{
			if (scaleCollection == null)
			{
				return;
			}
			foreach (RadialScale radialScale in scaleCollection)
			{
				this.RenderRadialScale(radialScale, circularGauge);
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002ABBC File Offset: 0x00028DBC
		private void RenderRadialPointers(RadialPointerCollection pointers, CircularScale circularScale, CircularGauge circularGauge)
		{
			if (pointers == null)
			{
				return;
			}
			foreach (RadialPointer radialPointer in pointers)
			{
				this.RenderRadialPointer(radialPointer, circularScale, circularGauge);
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002AC0C File Offset: 0x00028E0C
		private void RenderRadialScaleRanges(ScaleRangeCollection ranges, CircularScale circularScale, CircularGauge circularGauge)
		{
			if (ranges == null)
			{
				return;
			}
			foreach (ScaleRange scaleRange in ranges)
			{
				this.RenderRadialRange(scaleRange, circularScale, circularGauge);
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002AC5C File Offset: 0x00028E5C
		private void RenderLinearScales(LinearScaleCollection scaleCollection, LinearGauge linearGauge)
		{
			if (scaleCollection == null)
			{
				return;
			}
			foreach (LinearScale linearScale in scaleCollection)
			{
				this.RenderLinearScale(linearScale, linearGauge);
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002ACAC File Offset: 0x00028EAC
		private void RenderLinearPointers(LinearPointerCollection pointers, LinearScale linearScale, LinearGauge linearGauge)
		{
			if (pointers == null)
			{
				return;
			}
			foreach (LinearPointer linearPointer in pointers)
			{
				this.RenderLinearPointer(linearPointer, linearScale, linearGauge);
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002ACFC File Offset: 0x00028EFC
		private void RenderLinearScaleRanges(ScaleRangeCollection ranges, LinearScale linearScale, LinearGauge linearGauge)
		{
			if (ranges == null)
			{
				return;
			}
			foreach (ScaleRange scaleRange in ranges)
			{
				this.RenderLinearRange(scaleRange, linearScale, linearGauge);
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002AD4C File Offset: 0x00028F4C
		private void RenderCustomLabels(CustomLabelCollection customLabels, ScaleBase scaleBase)
		{
			if (customLabels == null)
			{
				return;
			}
			foreach (CustomLabel customLabel in customLabels)
			{
				this.RenderCustomLabel(customLabel, scaleBase);
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002AD9C File Offset: 0x00028F9C
		private void RenderRadialGauge(RadialGauge radialGauge)
		{
			CircularGauge circularGauge = new CircularGauge();
			this.m_coreGaugeContainer.CircularGauges.Add(circularGauge);
			this.RenderGauge(radialGauge, circularGauge);
			this.SetRadialGaugeProperties(radialGauge, circularGauge);
			this.RenderRadialScales(radialGauge.GaugeScales, circularGauge);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002ADE0 File Offset: 0x00028FE0
		private void RenderLinearGauge(LinearGauge linearGauge)
		{
			LinearGauge linearGauge2 = new LinearGauge();
			this.m_coreGaugeContainer.LinearGauges.Add(linearGauge2);
			this.RenderGauge(linearGauge, linearGauge2);
			this.SetLinearGaugeProperties(linearGauge, linearGauge2);
			this.RenderLinearScales(linearGauge.GaugeScales, linearGauge2);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002AE22 File Offset: 0x00029022
		private void RenderGauge(Gauge gauge, GaugeBase gaugeBase)
		{
			this.SetGaugeProperties(gauge, gaugeBase);
			this.RenderActionInfo(gauge.ActionInfo, gaugeBase.ToolTip, gaugeBase);
			this.RenderBackFrame(gauge.BackFrame, gaugeBase.BackFrame, gaugeBase);
			this.RenderGaugeTopImage(gauge.TopImage, gaugeBase);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002AE60 File Offset: 0x00029060
		private void RenderGaugeLabel(GaugeLabel gaugeLabel)
		{
			GaugeLabel gaugeLabel2 = new GaugeLabel();
			this.m_coreGaugeContainer.Labels.Add(gaugeLabel2);
			this.SetGaugeLabelProperties(gaugeLabel, gaugeLabel2);
			this.RenderActionInfo(gaugeLabel.ActionInfo, gaugeLabel2.ToolTip, gaugeLabel2);
			this.RenderGaugeLabelStyle(gaugeLabel, gaugeLabel2);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002AEA8 File Offset: 0x000290A8
		private void RenderStateIndicator(StateIndicator stateIndicator)
		{
			StateIndicator stateIndicator2 = new StateIndicator();
			this.m_coreGaugeContainer.StateIndicators.Add(stateIndicator2);
			this.SetStateIndicatorProperties(stateIndicator, stateIndicator2);
			this.RenderStateIndicatorStyle(stateIndicator, stateIndicator2);
			this.RenderActionInfo(stateIndicator.ActionInfo, stateIndicator2.ToolTip, stateIndicator2);
			this.RenderIndicatorStates(stateIndicator.IndicatorStates, stateIndicator2);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002AF00 File Offset: 0x00029100
		private void RenderStateIndicatorStyle(StateIndicator stateIndicator, StateIndicator coreIndicator)
		{
			Style style = stateIndicator.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = stateIndicator.Instance.Style;
			coreIndicator.FillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			coreIndicator.ShadowOffset = (float)this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
			coreIndicator.ShowBorder = MappingHelper.GetStyleBorderStyle(style.Border) == BorderStyles.Solid;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002AF60 File Offset: 0x00029160
		private void SetStateIndicatorProperties(StateIndicator stateIndicator, StateIndicator coreIndicator)
		{
			coreIndicator.Name = stateIndicator.Name;
			if (stateIndicator.ParentItem != null)
			{
				coreIndicator.Parent = this.GetParentName(stateIndicator.ParentItem);
			}
			else
			{
				coreIndicator.Parent = "";
			}
			coreIndicator.Location.X = this.GetPanelItemLeft(stateIndicator);
			coreIndicator.Location.Y = this.GetPanelItemTop(stateIndicator);
			coreIndicator.Size.Width = this.GetPanelItemWidth(stateIndicator);
			coreIndicator.Size.Height = this.GetPanelItemHeight(stateIndicator);
			coreIndicator.Visible = !this.GetPanelItemHidden(stateIndicator);
			int num;
			if (this.GetPanelItemZIndex(stateIndicator, out num))
			{
				coreIndicator.ZOrder = num;
			}
			string text;
			if (this.GetPanelItemToolTip(stateIndicator, out text))
			{
				coreIndicator.ToolTip = text;
			}
			ReportDoubleProperty angle = stateIndicator.Angle;
			if (angle != null)
			{
				if (!angle.IsExpression)
				{
					coreIndicator.Angle = (float)angle.Value;
				}
				else
				{
					coreIndicator.Angle = (float)stateIndicator.Instance.Angle;
				}
			}
			ReportEnumProperty<GaugeStateIndicatorStyles> indicatorStyle = stateIndicator.IndicatorStyle;
			GaugeStateIndicatorStyles gaugeStateIndicatorStyles = GaugeStateIndicatorStyles.Circle;
			if (indicatorStyle != null)
			{
				if (!indicatorStyle.IsExpression)
				{
					gaugeStateIndicatorStyles = indicatorStyle.Value;
				}
				else
				{
					gaugeStateIndicatorStyles = stateIndicator.Instance.IndicatorStyle;
				}
			}
			coreIndicator.IndicatorStyle = this.GetIndicatorStyle(gaugeStateIndicatorStyles);
			if (gaugeStateIndicatorStyles == GaugeStateIndicatorStyles.Image)
			{
				this.RenderIndicatorImage(stateIndicator.IndicatorImage, coreIndicator);
			}
			ReportDoubleProperty scaleFactor = stateIndicator.ScaleFactor;
			if (scaleFactor != null)
			{
				if (!scaleFactor.IsExpression)
				{
					coreIndicator.ScaleFactor = (float)scaleFactor.Value;
				}
				else
				{
					coreIndicator.ScaleFactor = (float)stateIndicator.Instance.ScaleFactor;
				}
			}
			ReportEnumProperty<GaugeResizeModes> resizeMode = stateIndicator.ResizeMode;
			if (resizeMode != null)
			{
				if (!resizeMode.IsExpression)
				{
					coreIndicator.ResizeMode = this.GetResizeMode(resizeMode.Value);
				}
				else
				{
					coreIndicator.ResizeMode = this.GetResizeMode(stateIndicator.Instance.ResizeMode);
				}
			}
			GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo = null;
			if (stateIndicator.GaugeInputValue != null)
			{
				inputValueOwnerInfo = this.InitializeStateIndicatorInputValues(stateIndicator, coreIndicator);
				InputValue inputValue = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue);
				inputValueOwnerInfo.CoreInputValues[0] = inputValue;
				inputValueOwnerInfo.GaugeInputValues[0] = stateIndicator.GaugeInputValue;
			}
			if (stateIndicator.MinimumValue != null)
			{
				if (inputValueOwnerInfo == null)
				{
					inputValueOwnerInfo = this.InitializeStateIndicatorInputValues(stateIndicator, coreIndicator);
				}
				InputValue inputValue2 = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue2);
				inputValueOwnerInfo.CoreInputValues[1] = inputValue2;
				inputValueOwnerInfo.GaugeInputValues[1] = stateIndicator.MinimumValue;
			}
			if (stateIndicator.MaximumValue != null)
			{
				if (inputValueOwnerInfo == null)
				{
					inputValueOwnerInfo = this.InitializeStateIndicatorInputValues(stateIndicator, coreIndicator);
				}
				InputValue inputValue3 = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue3);
				inputValueOwnerInfo.CoreInputValues[2] = inputValue3;
				inputValueOwnerInfo.GaugeInputValues[2] = stateIndicator.MaximumValue;
			}
			ReportEnumProperty<GaugeTransformationType> transformationType = stateIndicator.TransformationType;
			GaugeTransformationType gaugeTransformationType = GaugeTransformationType.Percentage;
			if (transformationType != null)
			{
				if (!transformationType.IsExpression)
				{
					gaugeTransformationType = transformationType.Value;
				}
				else
				{
					gaugeTransformationType = stateIndicator.Instance.TransformationType;
				}
			}
			coreIndicator.IsPercentBased = gaugeTransformationType == GaugeTransformationType.Percentage;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002B22C File Offset: 0x0002942C
		private GaugeMapper.InputValueOwnerInfo InitializeStateIndicatorInputValues(StateIndicator stateIndicator, StateIndicator coreIndicator)
		{
			GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo = this.CreateInputValueOwnerInfo(3);
			inputValueOwnerInfo.CoreGaugeElements = new object[] { coreIndicator };
			inputValueOwnerInfo.InputValueOwnerType = GaugeMapper.InputValueOwnerType.StateIndicator;
			inputValueOwnerInfo.InputValueOwnerDef = stateIndicator;
			return inputValueOwnerInfo;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002B260 File Offset: 0x00029460
		private void RenderIndicatorImage(IndicatorImage indicatorImage, StateIndicator coreIndicator)
		{
			if (indicatorImage == null)
			{
				return;
			}
			coreIndicator.Image = this.AddNamedImage(indicatorImage);
			Color color;
			if (this.GetBaseGaugeImageTransparentColor(indicatorImage, out color))
			{
				coreIndicator.ImageTransColor = color;
			}
			ReportColorProperty hueColor = indicatorImage.HueColor;
			if (hueColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
				{
					coreIndicator.ImageHueColor = color;
				}
				else
				{
					ReportColor hueColor2 = indicatorImage.Instance.HueColor;
					if (hueColor2 != null)
					{
						coreIndicator.ImageHueColor = hueColor2.ToColor();
					}
				}
			}
			ReportDoubleProperty transparency = indicatorImage.Transparency;
			if (transparency != null)
			{
				if (!transparency.IsExpression)
				{
					coreIndicator.ImageTransparency = (float)transparency.Value;
					return;
				}
				coreIndicator.ImageTransparency = (float)indicatorImage.Instance.Transparency;
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002B2FC File Offset: 0x000294FC
		private StateIndicatorStyle GetIndicatorStyle(GaugeStateIndicatorStyles style)
		{
			switch (style)
			{
			case GaugeStateIndicatorStyles.Flag:
				return 7;
			case GaugeStateIndicatorStyles.ArrowDown:
				return 8;
			case GaugeStateIndicatorStyles.ArrowDownIncline:
				return 9;
			case GaugeStateIndicatorStyles.ArrowSide:
				return 10;
			case GaugeStateIndicatorStyles.ArrowUp:
				return 11;
			case GaugeStateIndicatorStyles.ArrowUpIncline:
				return 12;
			case GaugeStateIndicatorStyles.BoxesAllFilled:
				return 13;
			case GaugeStateIndicatorStyles.BoxesNoneFilled:
				return 14;
			case GaugeStateIndicatorStyles.BoxesOneFilled:
				return 15;
			case GaugeStateIndicatorStyles.BoxesTwoFilled:
				return 16;
			case GaugeStateIndicatorStyles.BoxesThreeFilled:
				return 17;
			case GaugeStateIndicatorStyles.LightArrowDown:
				return 18;
			case GaugeStateIndicatorStyles.LightArrowDownIncline:
				return 19;
			case GaugeStateIndicatorStyles.LightArrowSide:
				return 20;
			case GaugeStateIndicatorStyles.LightArrowUp:
				return 21;
			case GaugeStateIndicatorStyles.LightArrowUpIncline:
				return 22;
			case GaugeStateIndicatorStyles.QuartersAllFilled:
				return 23;
			case GaugeStateIndicatorStyles.QuartersNoneFilled:
				return 24;
			case GaugeStateIndicatorStyles.QuartersOneFilled:
				return 25;
			case GaugeStateIndicatorStyles.QuartersTwoFilled:
				return 26;
			case GaugeStateIndicatorStyles.QuartersThreeFilled:
				return 27;
			case GaugeStateIndicatorStyles.SignalMeterFourFilled:
				return 28;
			case GaugeStateIndicatorStyles.SignalMeterNoneFilled:
				return 29;
			case GaugeStateIndicatorStyles.SignalMeterOneFilled:
				return 30;
			case GaugeStateIndicatorStyles.SignalMeterThreeFilled:
				return 31;
			case GaugeStateIndicatorStyles.SignalMeterTwoFilled:
				return 32;
			case GaugeStateIndicatorStyles.StarQuartersAllFilled:
				return 33;
			case GaugeStateIndicatorStyles.StarQuartersNoneFilled:
				return 34;
			case GaugeStateIndicatorStyles.StarQuartersOneFilled:
				return 35;
			case GaugeStateIndicatorStyles.StarQuartersTwoFilled:
				return 36;
			case GaugeStateIndicatorStyles.StarQuartersThreeFilled:
				return 37;
			case GaugeStateIndicatorStyles.ThreeSignsCircle:
				return 38;
			case GaugeStateIndicatorStyles.ThreeSignsDiamond:
				return 39;
			case GaugeStateIndicatorStyles.ThreeSignsTriangle:
				return 40;
			case GaugeStateIndicatorStyles.ThreeSymbolCheck:
				return 41;
			case GaugeStateIndicatorStyles.ThreeSymbolCross:
				return 42;
			case GaugeStateIndicatorStyles.ThreeSymbolExclamation:
				return 43;
			case GaugeStateIndicatorStyles.ThreeSymbolUnCircledCheck:
				return 44;
			case GaugeStateIndicatorStyles.ThreeSymbolUnCircledCross:
				return 45;
			case GaugeStateIndicatorStyles.ThreeSymbolUnCircledExclamation:
				return 46;
			case GaugeStateIndicatorStyles.TrafficLight:
				return 47;
			case GaugeStateIndicatorStyles.TrafficLightUnrimmed:
				return 48;
			case GaugeStateIndicatorStyles.TriangleDash:
				return 49;
			case GaugeStateIndicatorStyles.TriangleDown:
				return 50;
			case GaugeStateIndicatorStyles.TriangleUp:
				return 51;
			case GaugeStateIndicatorStyles.ButtonStop:
				return 52;
			case GaugeStateIndicatorStyles.ButtonPlay:
				return 53;
			case GaugeStateIndicatorStyles.ButtonPause:
				return 54;
			case GaugeStateIndicatorStyles.FaceSmile:
				return 55;
			case GaugeStateIndicatorStyles.FaceNeutral:
				return 56;
			case GaugeStateIndicatorStyles.FaceFrown:
				return 57;
			case GaugeStateIndicatorStyles.Image:
				return 5;
			case GaugeStateIndicatorStyles.None:
				return 0;
			default:
				return 6;
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002B488 File Offset: 0x00029688
		private void RenderIndicatorStates(IndicatorStateCollection states, StateIndicator coreIndicator)
		{
			if (states == null)
			{
				return;
			}
			foreach (IndicatorState indicatorState in states)
			{
				State state = new State();
				coreIndicator.States.Add(state);
				this.RenderIndicatorState(indicatorState, state);
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002B4E8 File Offset: 0x000296E8
		private void RenderIndicatorState(IndicatorState state, State coreState)
		{
			coreState.Name = state.Name;
			ReportColorProperty color = state.Color;
			if (color != null)
			{
				if (!color.IsExpression)
				{
					coreState.FillColor = color.Value.ToColor();
				}
				else
				{
					coreState.FillColor = state.Instance.Color.ToColor();
				}
			}
			ReportEnumProperty<GaugeStateIndicatorStyles> indicatorStyle = state.IndicatorStyle;
			GaugeStateIndicatorStyles gaugeStateIndicatorStyles = GaugeStateIndicatorStyles.Circle;
			if (indicatorStyle != null)
			{
				if (!indicatorStyle.IsExpression)
				{
					gaugeStateIndicatorStyles = indicatorStyle.Value;
				}
				else
				{
					gaugeStateIndicatorStyles = state.Instance.IndicatorStyle;
				}
			}
			coreState.IndicatorStyle = this.GetIndicatorStyle(gaugeStateIndicatorStyles);
			if (gaugeStateIndicatorStyles == GaugeStateIndicatorStyles.Image)
			{
				this.RenderIndicatorImage(state.IndicatorImage, coreState);
			}
			ReportDoubleProperty scaleFactor = state.ScaleFactor;
			if (scaleFactor != null)
			{
				if (!scaleFactor.IsExpression)
				{
					coreState.ScaleFactor = (float)scaleFactor.Value;
				}
				else
				{
					coreState.ScaleFactor = (float)state.Instance.ScaleFactor;
				}
			}
			GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo = null;
			coreState.StartValue = double.NegativeInfinity;
			coreState.EndValue = double.PositiveInfinity;
			if (state.StartValue != null)
			{
				inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				inputValueOwnerInfo.CoreGaugeElements = new object[] { coreState };
				InputValue inputValue = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue);
				inputValueOwnerInfo.CoreInputValues[0] = inputValue;
				inputValueOwnerInfo.GaugeInputValues[0] = state.StartValue;
				inputValueOwnerInfo.InputValueOwnerType = GaugeMapper.InputValueOwnerType.IndicatorState;
				inputValueOwnerInfo.InputValueOwnerDef = state;
			}
			if (state.EndValue != null)
			{
				if (inputValueOwnerInfo == null)
				{
					inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				}
				inputValueOwnerInfo.CoreGaugeElements = new object[] { coreState };
				InputValue inputValue2 = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue2);
				inputValueOwnerInfo.CoreInputValues[1] = inputValue2;
				inputValueOwnerInfo.GaugeInputValues[1] = state.EndValue;
				inputValueOwnerInfo.InputValueOwnerType = GaugeMapper.InputValueOwnerType.IndicatorState;
				inputValueOwnerInfo.InputValueOwnerDef = state;
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002B6A8 File Offset: 0x000298A8
		private void RenderIndicatorImage(IndicatorImage indicatorImage, State coreState)
		{
			if (indicatorImage == null)
			{
				return;
			}
			coreState.Image = this.AddNamedImage(indicatorImage);
			Color color;
			if (this.GetBaseGaugeImageTransparentColor(indicatorImage, out color))
			{
				coreState.ImageTransColor = color;
			}
			ReportColorProperty hueColor = indicatorImage.HueColor;
			if (hueColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
				{
					coreState.ImageHueColor = color;
					return;
				}
				ReportColor hueColor2 = indicatorImage.Instance.HueColor;
				if (hueColor2 != null)
				{
					coreState.ImageHueColor = hueColor2.ToColor();
				}
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002B710 File Offset: 0x00029910
		private void RenderRadialScale(RadialScale radialScale, CircularGauge circularGauge)
		{
			CircularScale circularScale = new CircularScale();
			circularGauge.Scales.Add(circularScale);
			this.RenderGaugeScale(radialScale, circularScale);
			this.SetRadialScaleProperties(radialScale, circularScale);
			this.RenderRadialScaleLabels(radialScale.ScaleLabels, circularScale.LabelStyle);
			this.RenderTickMarks(radialScale.GaugeMajorTickMarks, circularScale.MajorTickMark);
			this.RenderTickMarks(radialScale.GaugeMinorTickMarks, circularScale.MinorTickMark);
			this.RenderRadialPointers(radialScale.GaugePointers, circularScale, circularGauge);
			this.RenderRadialScaleRanges(radialScale.ScaleRanges, circularScale, circularGauge);
			this.RenderRadialScalePin(radialScale.MaximumPin, circularScale.MaximumPin);
			this.RenderRadialScalePin(radialScale.MinimumPin, circularScale.MinimumPin);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002B7B8 File Offset: 0x000299B8
		private void RenderLinearScale(LinearScale linearScale, LinearGauge linearGauge)
		{
			LinearScale linearScale2 = new LinearScale();
			linearGauge.Scales.Add(linearScale2);
			this.RenderGaugeScale(linearScale, linearScale2);
			this.SetLinearScaleProperties(linearScale, linearScale2);
			this.RenderLinearScaleLabels(linearScale.ScaleLabels, linearScale2.LabelStyle);
			this.RenderTickMarks(linearScale.GaugeMajorTickMarks, linearScale2.MajorTickMark);
			this.RenderTickMarks(linearScale.GaugeMinorTickMarks, linearScale2.MinorTickMark);
			this.RenderLinearPointers(linearScale.GaugePointers, linearScale2, linearGauge);
			this.RenderLinearScaleRanges(linearScale.ScaleRanges, linearScale2, linearGauge);
			this.RenderLinearScalePin(linearScale.MaximumPin, linearScale2.MaximumPin);
			this.RenderLinearScalePin(linearScale.MinimumPin, linearScale2.MinimumPin);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0002B85E File Offset: 0x00029A5E
		private void RenderGaugeScale(GaugeScale gaugeScale, ScaleBase scaleBase)
		{
			this.SetScaleProperties(gaugeScale, scaleBase);
			this.RenderActionInfo(gaugeScale.ActionInfo, scaleBase.ToolTip, scaleBase);
			this.RenderCustomLabels(gaugeScale.CustomLabels, scaleBase);
			this.RenderScaleStyle(gaugeScale, scaleBase);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002B890 File Offset: 0x00029A90
		private void RenderRadialPointer(RadialPointer radialPointer, CircularScale circularScale, CircularGauge circularGauge)
		{
			CircularPointer circularPointer = new CircularPointer();
			circularGauge.Pointers.Add(circularPointer);
			this.RenderGaugePointer(radialPointer, circularPointer, circularScale);
			this.SetRadialPointerProperties(radialPointer, circularPointer);
			this.RenderPointerCap(radialPointer.PointerCap, circularPointer);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002B8D0 File Offset: 0x00029AD0
		private void RenderRadialRange(ScaleRange scaleRange, CircularScale circularScale, CircularGauge circularGauge)
		{
			CircularRange circularRange = new CircularRange();
			circularGauge.Ranges.Add(circularRange);
			this.RenderScaleRange(scaleRange, circularRange, circularScale);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002B8FC File Offset: 0x00029AFC
		private void RenderLinearPointer(LinearPointer linearPointer, LinearScale linearScale, LinearGauge linearGauge)
		{
			LinearPointer linearPointer2 = new LinearPointer();
			linearGauge.Pointers.Add(linearPointer2);
			this.RenderGaugePointer(linearPointer, linearPointer2, linearScale);
			this.RenderThermometer(linearPointer.Thermometer, linearPointer2);
			this.SetLinearPointerProperties(linearPointer, linearPointer2);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002B93A File Offset: 0x00029B3A
		private void RenderThermometer(Thermometer thermometer, LinearPointer coreLinearPointer)
		{
			if (thermometer == null)
			{
				return;
			}
			this.SetThermometerProperties(thermometer, coreLinearPointer);
			this.RenderThermometerStyle(thermometer, coreLinearPointer);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0002B950 File Offset: 0x00029B50
		private void RenderLinearRange(ScaleRange scaleRange, LinearScale linearScale, LinearGauge linearGauge)
		{
			LinearRange linearRange = new LinearRange();
			linearGauge.Ranges.Add(linearRange);
			this.RenderScaleRange(scaleRange, linearRange, linearScale);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002B97C File Offset: 0x00029B7C
		private void RenderCustomLabel(CustomLabel customLabel, ScaleBase scaleBase)
		{
			CustomLabel customLabel2 = new CustomLabel();
			scaleBase.CustomLabels.Add(customLabel2);
			this.SetCustomLabelProperties(customLabel, customLabel2);
			this.RenderCustomLabelStyle(customLabel, customLabel2);
			this.RenderTickMarkStyle(customLabel.TickMarkStyle, customLabel2.TickMarkStyle);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002B9BE File Offset: 0x00029BBE
		private void RenderGaugePointer(GaugePointer gaugePointer, PointerBase pointerBase, ScaleBase parentScale)
		{
			this.SetGaugePointerProperties(gaugePointer, pointerBase, parentScale);
			this.RenderGaugePointerStyle(gaugePointer, pointerBase);
			this.RenderGaugePointerImage(gaugePointer.PointerImage, pointerBase);
			this.RenderActionInfo(gaugePointer.ActionInfo, pointerBase.ToolTip, pointerBase);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002B9F1 File Offset: 0x00029BF1
		private void RenderScaleRange(ScaleRange scaleRange, RangeBase rangeBase, ScaleBase parentScale)
		{
			this.SetScaleRangeProperties(scaleRange, rangeBase, parentScale);
			this.RenderScaleRangeStyle(scaleRange, rangeBase);
			this.RenderActionInfo(scaleRange.ActionInfo, rangeBase.ToolTip, rangeBase);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002BA17 File Offset: 0x00029C17
		private void RenderBackFrame(BackFrame backFrame, BackFrame coreBackFrame, object parent)
		{
			if (backFrame == null)
			{
				return;
			}
			this.SetBackFramePropreties(backFrame, coreBackFrame);
			this.RenderBackFrameStyle(backFrame, coreBackFrame);
			this.RenderFrameBackGroundStyle(backFrame.FrameBackground, coreBackFrame);
			this.RenderFrameImage(backFrame.FrameImage, coreBackFrame);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002BA47 File Offset: 0x00029C47
		private void RenderRadialScalePin(ScalePin scalePin, CircularSpecialPosition circularSpecialPosition)
		{
			if (scalePin == null)
			{
				return;
			}
			this.RenderTickMarkStyle(scalePin, circularSpecialPosition);
			this.SetScalePinProperties(scalePin, circularSpecialPosition);
			this.RenderRadialPinLabel(scalePin.PinLabel, circularSpecialPosition.LabelStyle);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002BA6F File Offset: 0x00029C6F
		private void RenderRadialPinLabel(PinLabel pinLabel, CircularPinLabel circularPinLabel)
		{
			if (pinLabel == null)
			{
				return;
			}
			this.RenderPinLabel(pinLabel, circularPinLabel);
			this.SetRadialPinLabelProperties(pinLabel, circularPinLabel);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002BA85 File Offset: 0x00029C85
		private void RenderLinearScalePin(ScalePin scalePin, LinearSpecialPosition linearSpecialPosition)
		{
			if (scalePin == null)
			{
				return;
			}
			this.RenderTickMarkStyle(scalePin, linearSpecialPosition);
			this.SetScalePinProperties(scalePin, linearSpecialPosition);
			this.RenderLinearPinLabel(scalePin.PinLabel, linearSpecialPosition.LabelStyle);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0002BAAD File Offset: 0x00029CAD
		private void RenderLinearPinLabel(PinLabel pinLabel, LinearPinLabel linearPinLabel)
		{
			if (pinLabel == null)
			{
				return;
			}
			this.RenderPinLabel(pinLabel, linearPinLabel);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0002BABB File Offset: 0x00029CBB
		private void RenderTickMarkStyle(TickMarkStyle tickMarkStyle, CustomTickMark customTickMark)
		{
			if (tickMarkStyle == null)
			{
				return;
			}
			this.SetTickMarkStyleProperties(tickMarkStyle, customTickMark);
			this.RenderTickMarkStyleStyle(tickMarkStyle, customTickMark);
			this.RenderTickMarkImage(tickMarkStyle.TickMarkImage, customTickMark);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002BADE File Offset: 0x00029CDE
		private void RenderPinLabel(PinLabel pinLabel, LinearPinLabel corePinLabel)
		{
			if (pinLabel == null)
			{
				return;
			}
			this.SetPinLabelProperties(pinLabel, corePinLabel);
			this.RenderPinLabelStyle(pinLabel, corePinLabel);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002BAF4 File Offset: 0x00029CF4
		private void RenderTickMarks(GaugeTickMarks tickMarks, TickMark coreTickMarks)
		{
			if (tickMarks == null)
			{
				return;
			}
			this.RenderTickMarkStyle(tickMarks, coreTickMarks);
			this.SetGaugeTickMarksProperties(tickMarks, coreTickMarks);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002BB0A File Offset: 0x00029D0A
		private void RenderRadialScaleLabels(ScaleLabels scaleLabels, CircularLabelStyle labelStyle)
		{
			if (scaleLabels == null)
			{
				return;
			}
			this.RenderScaleLabels(scaleLabels, labelStyle);
			this.SetRadialScaleLabelsProperties(scaleLabels, labelStyle);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002BB20 File Offset: 0x00029D20
		private void RenderLinearScaleLabels(ScaleLabels scaleLabels, LinearLabelStyle labelStyle)
		{
			if (scaleLabels == null)
			{
				return;
			}
			this.RenderScaleLabels(scaleLabels, labelStyle);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002BB2E File Offset: 0x00029D2E
		private void RenderScaleLabels(ScaleLabels scaleLabels, LinearLabelStyle labelStyle)
		{
			if (scaleLabels == null)
			{
				return;
			}
			this.SetScaleLabelsProperties(scaleLabels, labelStyle);
			this.RenderScaleLabelsStyle(scaleLabels, labelStyle);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002BB44 File Offset: 0x00029D44
		private void RenderPointerCap(PointerCap pointerCap, CircularPointer circularPointer)
		{
			if (pointerCap == null)
			{
				return;
			}
			this.SetPointerCapProperties(pointerCap, circularPointer);
			this.RenderPointerCapImage(pointerCap.CapImage, circularPointer);
			this.RenderPointerCapStyle(pointerCap, circularPointer);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002BB68 File Offset: 0x00029D68
		private void SetGaugePanelProperties()
		{
			if (this.m_gaugePanel.AntiAliasing != null)
			{
				if (!this.m_gaugePanel.AntiAliasing.IsExpression)
				{
					this.m_coreGaugeContainer.AntiAliasing = this.GetAntiAliasing(this.m_gaugePanel.AntiAliasing.Value);
				}
				else
				{
					this.m_coreGaugeContainer.AntiAliasing = this.GetAntiAliasing(this.m_gaugePanel.Instance.AntiAliasing);
				}
			}
			int num = 300;
			if (base.WidthOverrideInPixels != null)
			{
				num = base.WidthOverrideInPixels.Value;
			}
			else if (this.m_gaugePanel.Width != null)
			{
				num = MappingHelper.ToIntPixels(this.m_gaugePanel.Width, base.DpiX);
			}
			this.m_coreGaugeContainer.Width = num;
			int num2 = 300;
			if (base.HeightOverrideInPixels != null)
			{
				num2 = base.HeightOverrideInPixels.Value;
			}
			else if (this.m_gaugePanel.Height != null)
			{
				num2 = MappingHelper.ToIntPixels(this.m_gaugePanel.Height, base.DpiY);
			}
			this.m_coreGaugeContainer.Height = num2;
			if (this.m_gaugePanel.ShadowIntensity != null)
			{
				if (!this.m_gaugePanel.ShadowIntensity.IsExpression)
				{
					this.m_coreGaugeContainer.ShadowIntensity = (float)this.m_gaugePanel.ShadowIntensity.Value;
				}
				else
				{
					this.m_coreGaugeContainer.ShadowIntensity = (float)this.m_gaugePanel.Instance.ShadowIntensity;
				}
			}
			if (this.m_gaugePanel.TextAntiAliasingQuality != null)
			{
				if (!this.m_gaugePanel.TextAntiAliasingQuality.IsExpression)
				{
					this.m_coreGaugeContainer.TextAntiAliasingQuality = this.GetTextAntiAliasingQuality(this.m_gaugePanel.TextAntiAliasingQuality.Value);
				}
				else
				{
					this.m_coreGaugeContainer.TextAntiAliasingQuality = this.GetTextAntiAliasingQuality(this.m_gaugePanel.Instance.TextAntiAliasingQuality);
				}
			}
			if (this.m_gaugePanel.AutoLayout == null)
			{
				this.m_coreGaugeContainer.AutoLayout = false;
				return;
			}
			if (!this.m_gaugePanel.AutoLayout.IsExpression)
			{
				this.m_coreGaugeContainer.AutoLayout = this.m_gaugePanel.AutoLayout.Value;
				return;
			}
			this.m_coreGaugeContainer.AutoLayout = this.m_gaugePanel.Instance.AutoLayout;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002BDA8 File Offset: 0x00029FA8
		private void SetGaugeProperties(Gauge gauge, GaugeBase gaugeBase)
		{
			gaugeBase.Name = gauge.Name;
			if (gauge.ParentItem != null)
			{
				gaugeBase.Parent = this.GetParentName(gauge.ParentItem);
			}
			else
			{
				gaugeBase.Parent = "";
			}
			gaugeBase.Location.X = this.GetPanelItemLeft(gauge);
			gaugeBase.Location.Y = this.GetPanelItemTop(gauge);
			gaugeBase.Size.Width = this.GetPanelItemWidth(gauge);
			gaugeBase.Size.Height = this.GetPanelItemHeight(gauge);
			gaugeBase.Visible = !this.GetPanelItemHidden(gauge);
			int num;
			if (this.GetPanelItemZIndex(gauge, out num))
			{
				gaugeBase.ZOrder = num;
			}
			string text;
			if (this.GetPanelItemToolTip(gauge, out text))
			{
				gaugeBase.ToolTip = text;
			}
			if (gauge.ClipContent != null)
			{
				if (!gauge.ClipContent.IsExpression)
				{
					gaugeBase.ClipContent = gauge.ClipContent.Value;
				}
				else
				{
					gaugeBase.ClipContent = gauge.Instance.ClipContent;
				}
			}
			else
			{
				gaugeBase.ClipContent = false;
			}
			if (gauge.AspectRatio != null)
			{
				if (!gauge.AspectRatio.IsExpression)
				{
					gaugeBase.AspectRatio = (float)gauge.AspectRatio.Value;
					return;
				}
				gaugeBase.AspectRatio = (float)gauge.Instance.AspectRatio;
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002BEE0 File Offset: 0x0002A0E0
		private void SetRadialGaugeProperties(RadialGauge radialGauge, CircularGauge circularGauge)
		{
			if (radialGauge.PivotX != null)
			{
				if (!radialGauge.PivotX.IsExpression)
				{
					circularGauge.PivotPoint.X = (float)radialGauge.PivotX.Value;
				}
				else
				{
					circularGauge.PivotPoint.X = (float)radialGauge.Instance.PivotX;
				}
			}
			if (radialGauge.PivotY != null)
			{
				if (!radialGauge.PivotY.IsExpression)
				{
					circularGauge.PivotPoint.Y = (float)radialGauge.PivotY.Value;
					return;
				}
				circularGauge.PivotPoint.Y = (float)radialGauge.Instance.PivotY;
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002BF78 File Offset: 0x0002A178
		private void SetLinearGaugeProperties(LinearGauge linearGauge, LinearGauge coreLinearGauge)
		{
			ReportEnumProperty<GaugeOrientations> orientation = linearGauge.Orientation;
			if (orientation != null)
			{
				if (!orientation.IsExpression)
				{
					coreLinearGauge.Orientation = this.GetGaugeOrientation(orientation.Value);
					return;
				}
				coreLinearGauge.Orientation = this.GetGaugeOrientation(linearGauge.Instance.Orientation);
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002BFC4 File Offset: 0x0002A1C4
		private void SetGaugeLabelProperties(GaugeLabel gaugeLabel, GaugeLabel coreGaugeLabel)
		{
			coreGaugeLabel.Name = gaugeLabel.Name;
			if (gaugeLabel.ParentItem != null)
			{
				coreGaugeLabel.Parent = this.GetParentName(gaugeLabel.ParentItem);
			}
			coreGaugeLabel.Location.X = this.GetPanelItemLeft(gaugeLabel);
			coreGaugeLabel.Location.Y = this.GetPanelItemTop(gaugeLabel);
			coreGaugeLabel.Size.Width = this.GetPanelItemWidth(gaugeLabel);
			coreGaugeLabel.Size.Height = this.GetPanelItemHeight(gaugeLabel);
			coreGaugeLabel.Visible = !this.GetPanelItemHidden(gaugeLabel);
			int num;
			if (this.GetPanelItemZIndex(gaugeLabel, out num))
			{
				coreGaugeLabel.ZOrder = num;
			}
			string text;
			if (this.GetPanelItemToolTip(gaugeLabel, out text))
			{
				coreGaugeLabel.ToolTip = text;
			}
			ReportDoubleProperty angle = gaugeLabel.Angle;
			if (angle != null)
			{
				if (!angle.IsExpression)
				{
					coreGaugeLabel.Angle = (float)angle.Value;
				}
				else
				{
					coreGaugeLabel.Angle = (float)gaugeLabel.Instance.Angle;
				}
			}
			ReportBoolProperty useFontPercent = gaugeLabel.UseFontPercent;
			if (useFontPercent != null)
			{
				if (!useFontPercent.IsExpression)
				{
					coreGaugeLabel.FontUnit = this.GetFontUnit(useFontPercent.Value);
				}
				else
				{
					coreGaugeLabel.FontUnit = this.GetFontUnit(gaugeLabel.Instance.UseFontPercent);
				}
			}
			else
			{
				coreGaugeLabel.FontUnit = 1;
			}
			ReportStringProperty text2 = gaugeLabel.Text;
			if (text2 != null)
			{
				if (!text2.IsExpression)
				{
					if (text2.Value != null)
					{
						coreGaugeLabel.Text = text2.Value;
					}
				}
				else
				{
					string text3 = gaugeLabel.Instance.Text;
					if (text3 != null)
					{
						coreGaugeLabel.Text = text3;
					}
				}
			}
			ReportSizeProperty textShadowOffset = gaugeLabel.TextShadowOffset;
			if (textShadowOffset != null)
			{
				int num2;
				if (!textShadowOffset.IsExpression)
				{
					num2 = MappingHelper.ToIntPixels(textShadowOffset.Value, base.DpiX);
				}
				else
				{
					num2 = MappingHelper.ToIntPixels(gaugeLabel.Instance.TextShadowOffset, base.DpiX);
				}
				coreGaugeLabel.TextShadowOffset = this.GetValidShadowOffset(num2);
			}
			ReportEnumProperty<GaugeResizeModes> resizeMode = gaugeLabel.ResizeMode;
			if (resizeMode != null)
			{
				if (!resizeMode.IsExpression)
				{
					coreGaugeLabel.ResizeMode = this.GetResizeMode(resizeMode.Value);
					return;
				}
				coreGaugeLabel.ResizeMode = this.GetResizeMode(gaugeLabel.Instance.ResizeMode);
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
		private void SetScaleProperties(GaugeScale gaugeScale, ScaleBase scaleBase)
		{
			scaleBase.Name = gaugeScale.Name;
			if (gaugeScale.Hidden != null)
			{
				if (!gaugeScale.Hidden.IsExpression)
				{
					scaleBase.Visible = !gaugeScale.Hidden.Value;
				}
				else
				{
					scaleBase.Visible = !gaugeScale.Instance.Hidden;
				}
			}
			if (gaugeScale.Interval != null)
			{
				if (!gaugeScale.Interval.IsExpression)
				{
					scaleBase.Interval = gaugeScale.Interval.Value;
				}
				else
				{
					scaleBase.Interval = gaugeScale.Instance.Interval;
				}
			}
			if (gaugeScale.IntervalOffset != null)
			{
				if (!gaugeScale.IntervalOffset.IsExpression)
				{
					scaleBase.IntervalOffset = gaugeScale.IntervalOffset.Value;
				}
				else
				{
					scaleBase.IntervalOffset = gaugeScale.Instance.IntervalOffset;
				}
			}
			else
			{
				scaleBase.IntervalOffset = 0.0;
			}
			if (gaugeScale.Logarithmic != null)
			{
				if (!gaugeScale.Logarithmic.IsExpression)
				{
					scaleBase.Logarithmic = gaugeScale.Logarithmic.Value;
				}
				else
				{
					scaleBase.Logarithmic = gaugeScale.Instance.Logarithmic;
				}
			}
			if (gaugeScale.LogarithmicBase != null)
			{
				double num;
				if (!gaugeScale.LogarithmicBase.IsExpression)
				{
					num = gaugeScale.LogarithmicBase.Value;
				}
				else
				{
					num = gaugeScale.Instance.LogarithmicBase;
				}
				if (num >= 1.0)
				{
					scaleBase.LogarithmicBase = num;
				}
			}
			if (gaugeScale.Multiplier != null)
			{
				if (!gaugeScale.Multiplier.IsExpression)
				{
					scaleBase.Multiplier = gaugeScale.Multiplier.Value;
				}
				else
				{
					scaleBase.Multiplier = gaugeScale.Instance.Multiplier;
				}
			}
			if (gaugeScale.Reversed != null)
			{
				if (!gaugeScale.Reversed.IsExpression)
				{
					scaleBase.Reversed = gaugeScale.Reversed.Value;
				}
				else
				{
					scaleBase.Reversed = gaugeScale.Instance.Reversed;
				}
			}
			if (gaugeScale.TickMarksOnTop != null)
			{
				if (!gaugeScale.TickMarksOnTop.IsExpression)
				{
					scaleBase.TickMarksOnTop = gaugeScale.TickMarksOnTop.Value;
				}
				else
				{
					scaleBase.TickMarksOnTop = gaugeScale.Instance.TickMarksOnTop;
				}
			}
			if (gaugeScale.Width != null)
			{
				if (!gaugeScale.Width.IsExpression)
				{
					scaleBase.Width = (float)gaugeScale.Width.Value;
				}
				else
				{
					scaleBase.Width = (float)gaugeScale.Instance.Width;
				}
			}
			GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo = null;
			if (gaugeScale.MinimumValue != null)
			{
				inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				inputValueOwnerInfo.CoreGaugeElements = new object[] { scaleBase };
				InputValue inputValue = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue);
				inputValueOwnerInfo.CoreInputValues[0] = inputValue;
				inputValueOwnerInfo.GaugeInputValues[0] = gaugeScale.MinimumValue;
				inputValueOwnerInfo.InputValueOwnerType = GaugeMapper.InputValueOwnerType.Scale;
				inputValueOwnerInfo.InputValueOwnerDef = gaugeScale;
			}
			if (gaugeScale.MaximumValue != null)
			{
				if (inputValueOwnerInfo == null)
				{
					inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				}
				inputValueOwnerInfo.CoreGaugeElements = new object[] { scaleBase };
				InputValue inputValue2 = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue2);
				inputValueOwnerInfo.CoreInputValues[1] = inputValue2;
				inputValueOwnerInfo.GaugeInputValues[1] = gaugeScale.MaximumValue;
				inputValueOwnerInfo.InputValueOwnerType = GaugeMapper.InputValueOwnerType.Scale;
				inputValueOwnerInfo.InputValueOwnerDef = gaugeScale;
			}
			ReportStringProperty toolTip = gaugeScale.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						scaleBase.ToolTip = toolTip.Value;
						return;
					}
				}
				else
				{
					string toolTip2 = gaugeScale.Instance.ToolTip;
					if (toolTip2 != null)
					{
						scaleBase.ToolTip = toolTip2;
					}
				}
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002C504 File Offset: 0x0002A704
		private GaugeMapper.InputValueOwnerInfo CreateInputValueOwnerInfo(int index)
		{
			List<GaugeMapper.InputValueOwnerInfo> inputValueOwnerInfoList = this.InputValueOwnerInfoList;
			GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo = new GaugeMapper.InputValueOwnerInfo();
			inputValueOwnerInfo.GaugeInputValues = new GaugeInputValue[index];
			inputValueOwnerInfo.CoreInputValues = new InputValue[index];
			for (int i = 0; i < index; i++)
			{
				inputValueOwnerInfo.GaugeInputValues[i] = null;
				inputValueOwnerInfo.CoreInputValues[i] = null;
			}
			inputValueOwnerInfoList.Add(inputValueOwnerInfo);
			return inputValueOwnerInfo;
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002C55C File Offset: 0x0002A75C
		private void SetRadialScaleProperties(RadialScale radialScale, CircularScale circularScale)
		{
			if (radialScale.Radius != null)
			{
				if (!radialScale.Radius.IsExpression)
				{
					circularScale.Radius = (float)radialScale.Radius.Value;
				}
				else
				{
					circularScale.Radius = (float)radialScale.Instance.Radius;
				}
			}
			if (radialScale.StartAngle != null)
			{
				if (!radialScale.StartAngle.IsExpression)
				{
					circularScale.StartAngle = (float)radialScale.StartAngle.Value;
				}
				else
				{
					circularScale.StartAngle = (float)radialScale.Instance.StartAngle;
				}
			}
			if (radialScale.SweepAngle != null)
			{
				if (!radialScale.SweepAngle.IsExpression)
				{
					circularScale.SweepAngle = (float)radialScale.SweepAngle.Value;
					return;
				}
				circularScale.SweepAngle = (float)radialScale.Instance.SweepAngle;
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002C61C File Offset: 0x0002A81C
		private void SetLinearScaleProperties(LinearScale linearScale, LinearScale coreLinearScale)
		{
			ReportDoubleProperty reportDoubleProperty = linearScale.StartMargin;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					coreLinearScale.StartMargin = (float)reportDoubleProperty.Value;
				}
				else
				{
					coreLinearScale.StartMargin = (float)linearScale.Instance.StartMargin;
				}
			}
			reportDoubleProperty = linearScale.EndMargin;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					coreLinearScale.EndMargin = (float)reportDoubleProperty.Value;
				}
				else
				{
					coreLinearScale.EndMargin = (float)linearScale.Instance.EndMargin;
				}
			}
			reportDoubleProperty = linearScale.Position;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					coreLinearScale.Position = (float)reportDoubleProperty.Value;
					return;
				}
				coreLinearScale.Position = (float)linearScale.Instance.Position;
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002C6C4 File Offset: 0x0002A8C4
		private void SetGaugePointerProperties(GaugePointer gaugePointer, PointerBase pointerBase, ScaleBase parentScale)
		{
			pointerBase.Name = parentScale.Name + gaugePointer.Name;
			pointerBase.ScaleName = parentScale.Name;
			if (gaugePointer.BarStart != null)
			{
				if (!gaugePointer.BarStart.IsExpression)
				{
					pointerBase.BarStart = this.GetBarStart(gaugePointer.BarStart.Value);
				}
				else
				{
					pointerBase.BarStart = this.GetBarStart(gaugePointer.Instance.BarStart);
				}
			}
			if (gaugePointer.DistanceFromScale != null)
			{
				if (!gaugePointer.DistanceFromScale.IsExpression)
				{
					pointerBase.DistanceFromScale = (float)gaugePointer.DistanceFromScale.Value;
				}
				else
				{
					pointerBase.DistanceFromScale = (float)gaugePointer.Instance.DistanceFromScale;
				}
			}
			if (gaugePointer.MarkerLength != null)
			{
				if (!gaugePointer.MarkerLength.IsExpression)
				{
					pointerBase.MarkerLength = (float)gaugePointer.MarkerLength.Value;
				}
				else
				{
					pointerBase.MarkerLength = (float)gaugePointer.Instance.MarkerLength;
				}
			}
			if (gaugePointer.MarkerStyle != null)
			{
				if (!gaugePointer.MarkerStyle.IsExpression)
				{
					pointerBase.MarkerStyle = this.GetMarkerStyle(gaugePointer.MarkerStyle.Value);
				}
				else
				{
					pointerBase.MarkerStyle = this.GetMarkerStyle(gaugePointer.Instance.MarkerStyle);
				}
			}
			else
			{
				pointerBase.MarkerStyle = 2;
			}
			if (gaugePointer.SnappingEnabled != null)
			{
				if (!gaugePointer.SnappingEnabled.IsExpression)
				{
					pointerBase.SnappingEnabled = gaugePointer.SnappingEnabled.Value;
				}
				else
				{
					pointerBase.SnappingEnabled = gaugePointer.Instance.SnappingEnabled;
				}
			}
			if (gaugePointer.SnappingInterval != null)
			{
				if (!gaugePointer.SnappingInterval.IsExpression)
				{
					pointerBase.SnappingInterval = gaugePointer.SnappingInterval.Value;
				}
				else
				{
					pointerBase.SnappingInterval = gaugePointer.Instance.SnappingInterval;
				}
			}
			ReportStringProperty toolTip = gaugePointer.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						pointerBase.ToolTip = toolTip.Value;
					}
				}
				else
				{
					string toolTip2 = gaugePointer.Instance.ToolTip;
					if (toolTip2 != null)
					{
						pointerBase.ToolTip = toolTip2;
					}
				}
			}
			if (gaugePointer.Hidden != null)
			{
				if (!gaugePointer.Hidden.IsExpression)
				{
					pointerBase.Visible = !gaugePointer.Hidden.Value;
				}
				else
				{
					pointerBase.Visible = !gaugePointer.Instance.Hidden;
				}
			}
			if (gaugePointer.Width != null)
			{
				if (!gaugePointer.Width.IsExpression)
				{
					pointerBase.Width = (float)gaugePointer.Width.Value;
				}
				else
				{
					pointerBase.Width = (float)gaugePointer.Instance.Width;
				}
			}
			if (gaugePointer.GaugeInputValue != null)
			{
				GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo = this.CreateInputValueOwnerInfo(1);
				inputValueOwnerInfo.CoreGaugeElements = new object[] { pointerBase };
				InputValue inputValue = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue);
				inputValueOwnerInfo.CoreInputValues[0] = inputValue;
				inputValueOwnerInfo.GaugeInputValues[0] = gaugePointer.GaugeInputValue;
				inputValueOwnerInfo.InputValueOwnerType = GaugeMapper.InputValueOwnerType.Pointer;
				inputValueOwnerInfo.InputValueOwnerDef = gaugePointer;
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002C984 File Offset: 0x0002AB84
		private void SetRadialPointerProperties(RadialPointer radialPointer, CircularPointer circularPointer)
		{
			if (radialPointer.Placement != null)
			{
				if (!radialPointer.Placement.IsExpression)
				{
					circularPointer.Placement = this.GetPlacement(radialPointer.Placement.Value);
				}
				else
				{
					circularPointer.Placement = this.GetPlacement(radialPointer.Instance.Placement);
				}
			}
			if (radialPointer.Type != null)
			{
				if (!radialPointer.Type.IsExpression)
				{
					circularPointer.Type = this.GetCircularPointerType(radialPointer.Type.Value);
				}
				else
				{
					circularPointer.Type = this.GetCircularPointerType(radialPointer.Instance.Type);
				}
			}
			if (radialPointer.NeedleStyle != null)
			{
				if (!radialPointer.NeedleStyle.IsExpression)
				{
					circularPointer.NeedleStyle = this.GetNeedleStyle(radialPointer.NeedleStyle.Value);
					return;
				}
				circularPointer.NeedleStyle = this.GetNeedleStyle(radialPointer.Instance.NeedleStyle);
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002CA60 File Offset: 0x0002AC60
		private void SetLinearPointerProperties(LinearPointer linearPointer, LinearPointer coreLinearPointer)
		{
			ReportEnumProperty<GaugePointerPlacements> placement = linearPointer.Placement;
			if (placement != null)
			{
				if (!placement.IsExpression)
				{
					coreLinearPointer.Placement = this.GetPlacement(placement.Value);
				}
				else
				{
					coreLinearPointer.Placement = this.GetPlacement(linearPointer.Instance.Placement);
				}
			}
			ReportEnumProperty<LinearPointerTypes> type = linearPointer.Type;
			if (type != null)
			{
				if (!type.IsExpression)
				{
					coreLinearPointer.Type = this.GetLinearPointerType(type.Value);
					return;
				}
				coreLinearPointer.Type = this.GetLinearPointerType(linearPointer.Instance.Type);
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002CAE8 File Offset: 0x0002ACE8
		private void SetScaleRangeProperties(ScaleRange scaleRange, RangeBase rangeBase, ScaleBase scaleBase)
		{
			rangeBase.Name = scaleBase.Name + scaleRange.Name;
			rangeBase.ScaleName = scaleBase.Name;
			if (scaleRange.BackgroundGradientType != null)
			{
				if (!scaleRange.BackgroundGradientType.IsExpression)
				{
					rangeBase.FillGradientType = this.GetRangeGradientType(scaleRange.BackgroundGradientType.Value);
				}
				else
				{
					rangeBase.FillGradientType = this.GetRangeGradientType(scaleRange.Instance.BackgroundGradientType);
				}
			}
			if (scaleRange.DistanceFromScale != null)
			{
				if (!scaleRange.DistanceFromScale.IsExpression)
				{
					rangeBase.DistanceFromScale = (float)scaleRange.DistanceFromScale.Value;
				}
				else
				{
					rangeBase.DistanceFromScale = (float)scaleRange.Instance.DistanceFromScale;
				}
			}
			if (scaleRange.StartWidth != null)
			{
				if (!scaleRange.StartWidth.IsExpression)
				{
					rangeBase.StartWidth = (float)scaleRange.StartWidth.Value;
				}
				else
				{
					rangeBase.StartWidth = (float)scaleRange.Instance.StartWidth;
				}
			}
			if (scaleRange.EndWidth != null)
			{
				if (!scaleRange.EndWidth.IsExpression)
				{
					rangeBase.EndWidth = (float)scaleRange.EndWidth.Value;
				}
				else
				{
					rangeBase.EndWidth = (float)scaleRange.Instance.EndWidth;
				}
			}
			Color empty = Color.Empty;
			if (scaleRange.InRangeBarPointerColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(scaleRange.InRangeBarPointerColor, ref empty))
				{
					rangeBase.InRangeBarPointerColor = empty;
				}
				else if (scaleRange.Instance.InRangeBarPointerColor != null)
				{
					rangeBase.InRangeBarPointerColor = scaleRange.Instance.InRangeBarPointerColor.ToColor();
				}
			}
			if (scaleRange.InRangeLabelColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(scaleRange.InRangeLabelColor, ref empty))
				{
					rangeBase.InRangeLabelColor = empty;
				}
				else if (scaleRange.Instance.InRangeLabelColor != null)
				{
					rangeBase.InRangeLabelColor = scaleRange.Instance.InRangeLabelColor.ToColor();
				}
			}
			if (scaleRange.InRangeTickMarksColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(scaleRange.InRangeTickMarksColor, ref empty))
				{
					rangeBase.InRangeTickMarkColor = empty;
				}
				else if (scaleRange.Instance.InRangeTickMarksColor != null)
				{
					rangeBase.InRangeTickMarkColor = scaleRange.Instance.InRangeTickMarksColor.ToColor();
				}
			}
			if (scaleRange.Placement != null)
			{
				if (!scaleRange.Placement.IsExpression)
				{
					rangeBase.Placement = this.GetPlacement(scaleRange.Placement.Value);
				}
				else
				{
					rangeBase.Placement = this.GetPlacement(scaleRange.Instance.Placement);
				}
			}
			ReportStringProperty toolTip = scaleRange.ToolTip;
			if (toolTip != null)
			{
				if (!toolTip.IsExpression)
				{
					if (toolTip.Value != null)
					{
						rangeBase.ToolTip = toolTip.Value;
					}
				}
				else
				{
					string toolTip2 = scaleRange.Instance.ToolTip;
					if (toolTip2 != null)
					{
						rangeBase.ToolTip = toolTip2;
					}
				}
			}
			if (scaleRange.Hidden != null)
			{
				if (!scaleRange.Hidden.IsExpression)
				{
					rangeBase.Visible = !scaleRange.Hidden.Value;
				}
				else
				{
					rangeBase.Visible = !scaleRange.Instance.Hidden;
				}
			}
			GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo = null;
			if (scaleRange.StartValue != null)
			{
				inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				inputValueOwnerInfo.CoreGaugeElements = new object[] { rangeBase };
				InputValue inputValue = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue);
				inputValueOwnerInfo.CoreInputValues[0] = inputValue;
				inputValueOwnerInfo.GaugeInputValues[0] = scaleRange.StartValue;
				inputValueOwnerInfo.InputValueOwnerType = GaugeMapper.InputValueOwnerType.Range;
				inputValueOwnerInfo.InputValueOwnerDef = scaleRange;
			}
			if (scaleRange.EndValue != null)
			{
				if (inputValueOwnerInfo == null)
				{
					inputValueOwnerInfo = this.CreateInputValueOwnerInfo(2);
				}
				inputValueOwnerInfo.CoreGaugeElements = new object[] { rangeBase };
				InputValue inputValue2 = new InputValue();
				this.m_coreGaugeContainer.Values.Add(inputValue2);
				inputValueOwnerInfo.CoreInputValues[1] = inputValue2;
				inputValueOwnerInfo.GaugeInputValues[1] = scaleRange.EndValue;
				inputValueOwnerInfo.InputValueOwnerType = GaugeMapper.InputValueOwnerType.Range;
				inputValueOwnerInfo.InputValueOwnerDef = scaleRange;
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002CE64 File Offset: 0x0002B064
		private void SetBackFramePropreties(BackFrame backFrame, BackFrame coreBackFrame)
		{
			if (backFrame.FrameStyle != null)
			{
				if (!backFrame.FrameStyle.IsExpression)
				{
					coreBackFrame.FrameStyle = this.GetFrameStyle(backFrame.FrameStyle.Value);
				}
				else
				{
					coreBackFrame.FrameStyle = this.GetFrameStyle(backFrame.Instance.FrameStyle);
				}
			}
			else
			{
				coreBackFrame.FrameStyle = 0;
			}
			if (backFrame.FrameShape != null)
			{
				if (!backFrame.FrameShape.IsExpression)
				{
					coreBackFrame.FrameShape = this.GetFrameShape(backFrame.FrameShape.Value);
				}
				else
				{
					coreBackFrame.FrameShape = this.GetFrameShape(backFrame.Instance.FrameShape);
				}
			}
			if (backFrame.FrameWidth != null)
			{
				if (!backFrame.FrameWidth.IsExpression)
				{
					coreBackFrame.FrameWidth = (float)backFrame.FrameWidth.Value;
				}
				else
				{
					coreBackFrame.FrameWidth = (float)backFrame.Instance.FrameWidth;
				}
			}
			if (backFrame.GlassEffect != null)
			{
				if (!backFrame.GlassEffect.IsExpression)
				{
					coreBackFrame.GlassEffect = this.GetGlassEffect(backFrame.GlassEffect.Value);
					return;
				}
				coreBackFrame.GlassEffect = this.GetGlassEffect(backFrame.Instance.GlassEffect);
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002CF84 File Offset: 0x0002B184
		private void SetScaleLabelsProperties(ScaleLabels scaleLabels, LinearLabelStyle labelStyle)
		{
			ReportDoubleProperty reportDoubleProperty = scaleLabels.DistanceFromScale;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					labelStyle.DistanceFromScale = (float)reportDoubleProperty.Value;
				}
				else
				{
					labelStyle.DistanceFromScale = (float)scaleLabels.Instance.DistanceFromScale;
				}
			}
			reportDoubleProperty = scaleLabels.FontAngle;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					labelStyle.FontAngle = (float)reportDoubleProperty.Value;
				}
				else
				{
					labelStyle.FontAngle = (float)scaleLabels.Instance.FontAngle;
				}
			}
			reportDoubleProperty = scaleLabels.Interval;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					labelStyle.Interval = reportDoubleProperty.Value;
				}
				else
				{
					labelStyle.Interval = scaleLabels.Instance.Interval;
				}
			}
			reportDoubleProperty = scaleLabels.IntervalOffset;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					labelStyle.IntervalOffset = reportDoubleProperty.Value;
				}
				else
				{
					labelStyle.IntervalOffset = scaleLabels.Instance.IntervalOffset;
				}
			}
			else
			{
				labelStyle.IntervalOffset = 0.0;
			}
			ReportEnumProperty<GaugeLabelPlacements> placement = scaleLabels.Placement;
			if (placement != null)
			{
				if (!placement.IsExpression)
				{
					labelStyle.Placement = this.GetPlacement(placement.Value);
				}
				else
				{
					labelStyle.Placement = this.GetPlacement(scaleLabels.Instance.Placement);
				}
			}
			ReportBoolProperty reportBoolProperty = scaleLabels.Hidden;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					labelStyle.Visible = !reportBoolProperty.Value;
				}
				else
				{
					labelStyle.Visible = !scaleLabels.Instance.Hidden;
				}
			}
			else
			{
				labelStyle.Visible = true;
			}
			reportBoolProperty = scaleLabels.ShowEndLabels;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					labelStyle.ShowEndLabels = reportBoolProperty.Value;
				}
				else
				{
					labelStyle.ShowEndLabels = scaleLabels.Instance.ShowEndLabels;
				}
			}
			else
			{
				labelStyle.ShowEndLabels = false;
			}
			reportBoolProperty = scaleLabels.UseFontPercent;
			if (reportBoolProperty == null)
			{
				labelStyle.FontUnit = 1;
				return;
			}
			if (!reportBoolProperty.IsExpression)
			{
				labelStyle.FontUnit = this.GetFontUnit(reportBoolProperty.Value);
				return;
			}
			labelStyle.FontUnit = this.GetFontUnit(scaleLabels.Instance.UseFontPercent);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002D168 File Offset: 0x0002B368
		private void SetRadialScaleLabelsProperties(ScaleLabels scaleLabels, CircularLabelStyle labelStyle)
		{
			ReportBoolProperty reportBoolProperty = scaleLabels.AllowUpsideDown;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					labelStyle.AllowUpsideDown = reportBoolProperty.Value;
				}
				else
				{
					labelStyle.AllowUpsideDown = scaleLabels.Instance.AllowUpsideDown;
				}
			}
			else
			{
				labelStyle.AllowUpsideDown = false;
			}
			reportBoolProperty = scaleLabels.RotateLabels;
			if (reportBoolProperty == null)
			{
				labelStyle.RotateLabels = false;
				return;
			}
			if (!reportBoolProperty.IsExpression)
			{
				labelStyle.RotateLabels = reportBoolProperty.Value;
				return;
			}
			labelStyle.RotateLabels = scaleLabels.Instance.RotateLabels;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002D1E8 File Offset: 0x0002B3E8
		private void SetPointerCapProperties(PointerCap pointerCap, CircularPointer circularPointer)
		{
			ReportEnumProperty<GaugeCapStyles> capStyle = pointerCap.CapStyle;
			if (capStyle != null)
			{
				if (!capStyle.IsExpression)
				{
					circularPointer.CapStyle = this.GetCapStyle(capStyle.Value);
				}
				else
				{
					circularPointer.CapStyle = this.GetCapStyle(pointerCap.Instance.CapStyle);
				}
			}
			ReportBoolProperty reportBoolProperty = pointerCap.Hidden;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					circularPointer.CapVisible = !reportBoolProperty.Value;
				}
				else
				{
					circularPointer.CapVisible = !pointerCap.Instance.Hidden;
				}
			}
			else
			{
				circularPointer.CapVisible = true;
			}
			reportBoolProperty = pointerCap.OnTop;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					circularPointer.CapOnTop = reportBoolProperty.Value;
				}
				else
				{
					circularPointer.CapOnTop = pointerCap.Instance.OnTop;
				}
			}
			else
			{
				circularPointer.CapOnTop = false;
			}
			reportBoolProperty = pointerCap.Reflection;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					circularPointer.CapReflection = reportBoolProperty.Value;
				}
				else
				{
					circularPointer.CapReflection = pointerCap.Instance.Reflection;
				}
			}
			else
			{
				circularPointer.CapReflection = false;
			}
			ReportDoubleProperty width = pointerCap.Width;
			if (width != null)
			{
				if (!width.IsExpression)
				{
					circularPointer.CapWidth = (float)width.Value;
					return;
				}
				circularPointer.CapWidth = (float)pointerCap.Instance.Width;
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002D318 File Offset: 0x0002B518
		private void SetTickMarkStyleProperties(TickMarkStyle tickMarkStyle, CustomTickMark customTickMark)
		{
			ReportDoubleProperty reportDoubleProperty = tickMarkStyle.DistanceFromScale;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					customTickMark.DistanceFromScale = (float)reportDoubleProperty.Value;
				}
				else
				{
					customTickMark.DistanceFromScale = (float)tickMarkStyle.Instance.DistanceFromScale;
				}
			}
			reportDoubleProperty = tickMarkStyle.GradientDensity;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					customTickMark.GradientDensity = (float)reportDoubleProperty.Value;
				}
				else
				{
					customTickMark.GradientDensity = (float)tickMarkStyle.Instance.GradientDensity;
				}
			}
			reportDoubleProperty = tickMarkStyle.Length;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					customTickMark.Length = (float)reportDoubleProperty.Value;
				}
				else
				{
					customTickMark.Length = (float)tickMarkStyle.Instance.Length;
				}
			}
			reportDoubleProperty = tickMarkStyle.Width;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					customTickMark.Width = (float)reportDoubleProperty.Value;
				}
				else
				{
					customTickMark.Width = (float)tickMarkStyle.Instance.Width;
				}
			}
			ReportEnumProperty<GaugeLabelPlacements> placement = tickMarkStyle.Placement;
			if (placement != null)
			{
				if (!placement.IsExpression)
				{
					customTickMark.Placement = this.GetPlacement(placement.Value);
				}
				else
				{
					customTickMark.Placement = this.GetPlacement(tickMarkStyle.Instance.Placement);
				}
			}
			else
			{
				customTickMark.Placement = 0;
			}
			ReportBoolProperty reportBoolProperty = tickMarkStyle.EnableGradient;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					customTickMark.EnableGradient = reportBoolProperty.Value;
				}
				else
				{
					customTickMark.EnableGradient = tickMarkStyle.Instance.EnableGradient;
				}
			}
			else
			{
				customTickMark.EnableGradient = false;
			}
			reportBoolProperty = tickMarkStyle.Hidden;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					customTickMark.Visible = !reportBoolProperty.Value;
				}
				else
				{
					customTickMark.Visible = !tickMarkStyle.Instance.Hidden;
				}
			}
			else
			{
				customTickMark.Visible = true;
			}
			ReportEnumProperty<GaugeTickMarkShapes> shape = tickMarkStyle.Shape;
			if (shape == null)
			{
				customTickMark.Shape = 1;
				return;
			}
			if (!shape.IsExpression)
			{
				customTickMark.Shape = this.GetMarkerStyle(shape.Value);
				return;
			}
			customTickMark.Shape = this.GetMarkerStyle(tickMarkStyle.Instance.Shape);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002D4F8 File Offset: 0x0002B6F8
		private void SetScalePinProperties(ScalePin scalePin, SpecialPosition specialPosition)
		{
			ReportDoubleProperty location = scalePin.Location;
			if (location != null)
			{
				if (!location.IsExpression)
				{
					specialPosition.Location = (float)location.Value;
				}
				else
				{
					specialPosition.Location = (float)scalePin.Instance.Location;
				}
			}
			ReportBoolProperty enable = scalePin.Enable;
			if (enable == null)
			{
				specialPosition.Enable = false;
				return;
			}
			if (!enable.IsExpression)
			{
				specialPosition.Enable = enable.Value;
				return;
			}
			specialPosition.Enable = scalePin.Instance.Enable;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002D570 File Offset: 0x0002B770
		private void SetGaugeTickMarksProperties(GaugeTickMarks tickMarks, TickMark coreTickMarks)
		{
			ReportDoubleProperty reportDoubleProperty = tickMarks.Interval;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					coreTickMarks.Interval = reportDoubleProperty.Value;
				}
				else
				{
					coreTickMarks.Interval = tickMarks.Instance.Interval;
				}
			}
			reportDoubleProperty = tickMarks.IntervalOffset;
			if (reportDoubleProperty == null)
			{
				coreTickMarks.IntervalOffset = 0.0;
				return;
			}
			if (!reportDoubleProperty.IsExpression)
			{
				coreTickMarks.IntervalOffset = reportDoubleProperty.Value;
				return;
			}
			coreTickMarks.IntervalOffset = tickMarks.Instance.IntervalOffset;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
		private void SetPinLabelProperties(PinLabel pinLabel, LinearPinLabel corePinLabel)
		{
			ReportDoubleProperty reportDoubleProperty = pinLabel.DistanceFromScale;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					corePinLabel.DistanceFromScale = (float)reportDoubleProperty.Value;
				}
				else
				{
					corePinLabel.DistanceFromScale = (float)pinLabel.Instance.DistanceFromScale;
				}
			}
			reportDoubleProperty = pinLabel.FontAngle;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					corePinLabel.FontAngle = (float)reportDoubleProperty.Value;
				}
				else
				{
					corePinLabel.FontAngle = (float)pinLabel.Instance.FontAngle;
				}
			}
			ReportBoolProperty useFontPercent = pinLabel.UseFontPercent;
			if (useFontPercent != null)
			{
				if (!useFontPercent.IsExpression)
				{
					corePinLabel.FontUnit = this.GetFontUnit(useFontPercent.Value);
				}
				else
				{
					corePinLabel.FontUnit = this.GetFontUnit(pinLabel.Instance.UseFontPercent);
				}
			}
			else
			{
				corePinLabel.FontUnit = 1;
			}
			ReportStringProperty text = pinLabel.Text;
			if (text != null)
			{
				if (!text.IsExpression)
				{
					if (text.Value != null)
					{
						corePinLabel.Text = text.Value;
					}
				}
				else
				{
					string text2 = pinLabel.Instance.Text;
					if (text2 != null)
					{
						corePinLabel.Text = text2;
					}
				}
			}
			ReportEnumProperty<GaugeLabelPlacements> placement = pinLabel.Placement;
			if (placement != null)
			{
				if (!placement.IsExpression)
				{
					corePinLabel.Placement = this.GetPlacement(placement.Value);
					return;
				}
				corePinLabel.Placement = this.GetPlacement(pinLabel.Placement.Value);
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002D728 File Offset: 0x0002B928
		private void SetRadialPinLabelProperties(PinLabel pinLabel, CircularPinLabel circularPinLabel)
		{
			ReportBoolProperty reportBoolProperty = pinLabel.AllowUpsideDown;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					circularPinLabel.AllowUpsideDown = reportBoolProperty.Value;
				}
				else
				{
					circularPinLabel.AllowUpsideDown = pinLabel.Instance.AllowUpsideDown;
				}
			}
			else
			{
				circularPinLabel.AllowUpsideDown = false;
			}
			reportBoolProperty = pinLabel.RotateLabel;
			if (reportBoolProperty == null)
			{
				circularPinLabel.RotateLabel = false;
				return;
			}
			if (!reportBoolProperty.IsExpression)
			{
				circularPinLabel.RotateLabel = reportBoolProperty.Value;
				return;
			}
			circularPinLabel.RotateLabel = pinLabel.Instance.RotateLabel;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002D7A8 File Offset: 0x0002B9A8
		private void SetCustomLabelProperties(CustomLabel customLabel, CustomLabel coreCustomLabel)
		{
			if (customLabel.Name != null)
			{
				coreCustomLabel.Name = customLabel.Name;
			}
			ReportBoolProperty reportBoolProperty = customLabel.AllowUpsideDown;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					coreCustomLabel.AllowUpsideDown = reportBoolProperty.Value;
				}
				else
				{
					coreCustomLabel.AllowUpsideDown = customLabel.Instance.AllowUpsideDown;
				}
			}
			else
			{
				coreCustomLabel.AllowUpsideDown = false;
			}
			reportBoolProperty = customLabel.RotateLabel;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					coreCustomLabel.RotateLabel = reportBoolProperty.Value;
				}
				else
				{
					coreCustomLabel.RotateLabel = customLabel.Instance.RotateLabel;
				}
			}
			else
			{
				coreCustomLabel.RotateLabel = false;
			}
			reportBoolProperty = customLabel.Hidden;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					coreCustomLabel.Visible = !reportBoolProperty.Value;
				}
				else
				{
					coreCustomLabel.Visible = !customLabel.Instance.Hidden;
				}
			}
			else
			{
				coreCustomLabel.Visible = true;
			}
			reportBoolProperty = customLabel.UseFontPercent;
			if (reportBoolProperty != null)
			{
				if (!reportBoolProperty.IsExpression)
				{
					coreCustomLabel.FontUnit = this.GetFontUnit(reportBoolProperty.Value);
				}
				else
				{
					coreCustomLabel.FontUnit = this.GetFontUnit(customLabel.Instance.UseFontPercent);
				}
			}
			else
			{
				coreCustomLabel.FontUnit = 1;
			}
			ReportDoubleProperty reportDoubleProperty = customLabel.DistanceFromScale;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					coreCustomLabel.DistanceFromScale = (float)reportDoubleProperty.Value;
				}
				else
				{
					coreCustomLabel.DistanceFromScale = (float)customLabel.Instance.DistanceFromScale;
				}
			}
			reportDoubleProperty = customLabel.FontAngle;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					coreCustomLabel.FontAngle = (float)reportDoubleProperty.Value;
				}
				else
				{
					coreCustomLabel.FontAngle = (float)customLabel.Instance.FontAngle;
				}
			}
			reportDoubleProperty = customLabel.Value;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					coreCustomLabel.Value = reportDoubleProperty.Value;
				}
				else
				{
					coreCustomLabel.Value = customLabel.Instance.Value;
				}
			}
			ReportStringProperty text = customLabel.Text;
			if (text != null)
			{
				if (!text.IsExpression)
				{
					if (text.Value != null)
					{
						coreCustomLabel.Text = text.Value;
					}
				}
				else
				{
					string text2 = customLabel.Instance.Text;
					if (text2 != null)
					{
						coreCustomLabel.Text = text2;
					}
				}
			}
			ReportEnumProperty<GaugeLabelPlacements> placement = customLabel.Placement;
			if (placement != null)
			{
				if (!placement.IsExpression)
				{
					coreCustomLabel.Placement = this.GetPlacement(placement.Value);
					return;
				}
				coreCustomLabel.Placement = this.GetPlacement(customLabel.Placement.Value);
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002D9D8 File Offset: 0x0002BBD8
		private void SetThermometerProperties(Thermometer thermometer, LinearPointer linearPointer)
		{
			ReportDoubleProperty reportDoubleProperty = thermometer.BulbOffset;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					linearPointer.ThermometerBulbOffset = (float)reportDoubleProperty.Value;
				}
				else
				{
					linearPointer.ThermometerBulbOffset = (float)thermometer.Instance.BulbOffset;
				}
			}
			reportDoubleProperty = thermometer.BulbSize;
			if (reportDoubleProperty != null)
			{
				if (!reportDoubleProperty.IsExpression)
				{
					linearPointer.ThermometerBulbSize = (float)reportDoubleProperty.Value;
				}
				else
				{
					linearPointer.ThermometerBulbSize = (float)thermometer.Instance.BulbSize;
				}
			}
			ReportEnumProperty<GaugeThermometerStyles> thermometerStyle = thermometer.ThermometerStyle;
			if (thermometerStyle != null)
			{
				if (!thermometerStyle.IsExpression)
				{
					linearPointer.ThermometerStyle = this.GetThermometerStyle(thermometerStyle.Value);
					return;
				}
				linearPointer.ThermometerStyle = this.GetThermometerStyle(thermometer.Instance.ThermometerStyle);
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002DA88 File Offset: 0x0002BC88
		private void RenderGaugePanelStyle()
		{
			Style style = this.m_gaugePanel.Style;
			this.m_coreGaugeContainer.BackColor = Color.Transparent;
			if (style == null)
			{
				return;
			}
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundColor))
			{
				this.m_coreGaugeContainer.BackColor = MappingHelper.GetStyleBackgroundColor(style, this.m_gaugePanel.Instance.Style);
			}
			this.m_coreGaugeContainer.RightToLeft = MappingHelper.GetStyleDirection(style, this.m_gaugePanel.Instance.Style);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002DB04 File Offset: 0x0002BD04
		private void RenderBackFrameStyle(BackFrame backFrame, BackFrame coreBackFrame)
		{
			Style style = backFrame.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = backFrame.Instance.Style;
			coreBackFrame.FrameColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			coreBackFrame.FrameGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			coreBackFrame.FrameGradientType = this.GetGradientType(style, style2);
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
			{
				coreBackFrame.FrameHatchStyle = this.GetHatchStyle(style, style2);
			}
			coreBackFrame.ShadowOffset = (float)this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
			Border border = style.Border;
			if (border != null)
			{
				coreBackFrame.BorderColor = MappingHelper.GetStyleBorderColor(border);
				coreBackFrame.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
				if (MappingHelper.IsStylePropertyDefined(border.Style))
				{
					coreBackFrame.BorderStyle = this.GetDashStyle(border);
				}
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
		private void RenderFrameBackGroundStyle(FrameBackground frameBackground, BackFrame coreBackFrame)
		{
			if (frameBackground == null)
			{
				return;
			}
			Style style = frameBackground.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = frameBackground.Instance.Style;
			coreBackFrame.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			coreBackFrame.BackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			coreBackFrame.BackGradientType = this.GetGradientType(style, style2);
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
			{
				coreBackFrame.BackHatchStyle = this.GetHatchStyle(style, style2);
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0002DC34 File Offset: 0x0002BE34
		private void RenderGaugeLabelStyle(GaugeLabel gaugeLabel, GaugeLabel coreGaugeLabel)
		{
			Style style = gaugeLabel.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = gaugeLabel.Instance.Style;
			coreGaugeLabel.BackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			coreGaugeLabel.BackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			coreGaugeLabel.BackGradientType = this.GetGradientType(style, style2);
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
			{
				coreGaugeLabel.BackHatchStyle = this.GetHatchStyle(style, style2);
			}
			coreGaugeLabel.BackShadowOffset = this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
			Border border = style.Border;
			if (border != null)
			{
				coreGaugeLabel.BorderColor = MappingHelper.GetStyleBorderColor(border);
				coreGaugeLabel.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
				if (MappingHelper.IsStylePropertyDefined(border.Style))
				{
					coreGaugeLabel.BorderStyle = this.GetDashStyle(border);
				}
			}
			coreGaugeLabel.TextColor = MappingHelper.GetStyleColor(style, style2);
			coreGaugeLabel.Font = base.GetFont(style, style2);
			coreGaugeLabel.TextAlignment = MappingHelper.GetStyleContentAlignment(style, style2);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002DD20 File Offset: 0x0002BF20
		private void RenderScaleStyle(GaugeScale scale, ScaleBase scaleBase)
		{
			Style style = scale.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = scale.Instance.Style;
			scaleBase.FillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			scaleBase.FillGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			scaleBase.FillGradientType = this.GetGradientType(style, style2);
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
			{
				scaleBase.FillHatchStyle = this.GetHatchStyle(style, style2);
			}
			scaleBase.ShadowOffset = (float)this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
			Border border = style.Border;
			if (border != null)
			{
				scaleBase.BorderColor = MappingHelper.GetStyleBorderColor(border);
				scaleBase.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
				if (MappingHelper.IsStylePropertyDefined(border.Style))
				{
					scaleBase.BorderStyle = this.GetDashStyle(border);
				}
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002DDE4 File Offset: 0x0002BFE4
		private void RenderGaugePointerStyle(GaugePointer gaugePointer, PointerBase pointerBase)
		{
			Style style = gaugePointer.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = gaugePointer.Instance.Style;
			pointerBase.FillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			pointerBase.FillGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			pointerBase.FillGradientType = this.GetGradientType(style, style2);
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
			{
				pointerBase.FillHatchStyle = this.GetHatchStyle(style, style2);
			}
			pointerBase.ShadowOffset = (float)this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
			Border border = style.Border;
			if (border != null)
			{
				pointerBase.BorderColor = MappingHelper.GetStyleBorderColor(border);
				pointerBase.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
				if (MappingHelper.IsStylePropertyDefined(border.Style))
				{
					pointerBase.BorderStyle = this.GetDashStyle(border);
				}
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002DEA8 File Offset: 0x0002C0A8
		private void RenderPointerCapStyle(PointerCap pointerCap, CircularPointer circularPointer)
		{
			Style style = pointerCap.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = pointerCap.Instance.Style;
			circularPointer.CapFillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			circularPointer.CapFillGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			circularPointer.CapFillGradientType = this.GetGradientType(style, style2);
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
			{
				circularPointer.CapFillHatchStyle = this.GetHatchStyle(style, style2);
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002DF10 File Offset: 0x0002C110
		private void RenderScaleRangeStyle(ScaleRange scaleRange, RangeBase rangeBase)
		{
			Style style = scaleRange.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = scaleRange.Instance.Style;
			rangeBase.FillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			rangeBase.FillGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
			{
				rangeBase.FillHatchStyle = this.GetHatchStyle(style, style2);
			}
			rangeBase.ShadowOffset = (float)this.GetValidShadowOffset(MappingHelper.GetStyleShadowOffset(style, style2, base.DpiX));
			Border border = style.Border;
			if (border != null)
			{
				rangeBase.BorderColor = MappingHelper.GetStyleBorderColor(border);
				rangeBase.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
				if (MappingHelper.IsStylePropertyDefined(border.Style))
				{
					rangeBase.BorderStyle = this.GetDashStyle(border);
				}
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002DFC8 File Offset: 0x0002C1C8
		private void RenderScaleLabelsStyle(ScaleLabels scaleLabels, LinearLabelStyle labelStyle)
		{
			Style style = scaleLabels.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = scaleLabels.Instance.Style;
			labelStyle.TextColor = MappingHelper.GetStyleColor(style, style2);
			labelStyle.Font = base.GetFont(style, style2);
			if (MappingHelper.IsStylePropertyDefined(style.Format))
			{
				labelStyle.FormatString = MappingHelper.GetStyleFormat(style, style2);
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002E024 File Offset: 0x0002C224
		private void RenderCustomLabelStyle(CustomLabel customLabel, CustomLabel coreCustomLabel)
		{
			Style style = customLabel.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = customLabel.Instance.Style;
			coreCustomLabel.TextColor = MappingHelper.GetStyleColor(style, style2);
			coreCustomLabel.Font = base.GetFont(style, style2);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002E064 File Offset: 0x0002C264
		private void RenderPinLabelStyle(PinLabel pinLabel, LinearPinLabel corePinLabel)
		{
			Style style = pinLabel.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = pinLabel.Instance.Style;
			corePinLabel.TextColor = MappingHelper.GetStyleColor(style, style2);
			corePinLabel.Font = base.GetFont(style, style2);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002E0A4 File Offset: 0x0002C2A4
		private void RenderTickMarkStyleStyle(TickMarkStyle tickMarkStyle, CustomTickMark customTickMark)
		{
			Style style = tickMarkStyle.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = tickMarkStyle.Instance.Style;
			customTickMark.FillColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			Border border = style.Border;
			if (border != null)
			{
				customTickMark.BorderColor = MappingHelper.GetStyleBorderColor(border);
				customTickMark.BorderWidth = MappingHelper.GetStyleBorderWidth(border, base.DpiX);
				if (MappingHelper.IsStylePropertyDefined(border.Style))
				{
					customTickMark.BorderStyle = this.GetDashStyle(border);
				}
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002E118 File Offset: 0x0002C318
		private void RenderThermometerStyle(Thermometer thermometer, LinearPointer linearPointer)
		{
			Style style = thermometer.Style;
			if (style == null)
			{
				return;
			}
			StyleInstance style2 = thermometer.Instance.Style;
			linearPointer.ThermometerBackColor = MappingHelper.GetStyleBackgroundColor(style, style2);
			linearPointer.ThermometerBackGradientEndColor = MappingHelper.GetStyleBackGradientEndColor(style, style2);
			linearPointer.ThermometerBackGradientType = this.GetGradientType(style, style2);
			if (MappingHelper.IsStylePropertyDefined(style.BackgroundHatchType))
			{
				linearPointer.ThermometerBackHatchStyle = this.GetHatchStyle(style, style2);
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002E180 File Offset: 0x0002C380
		private void RenderActionInfo(ActionInfo actionInfo, string toolTip, IImageMapProvider imageMapProvider)
		{
			if (actionInfo == null && string.IsNullOrEmpty(toolTip))
			{
				return;
			}
			string text;
			ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap = MappingHelper.CreateActionInfoDynamic(this.m_gaugePanel, actionInfo, toolTip, out text);
			if (actionInfoWithDynamicImageMap != null)
			{
				if (text != null)
				{
					imageMapProvider.Href = text;
				}
				int count = this.m_actions.Count;
				this.m_actions.InternalList.Add(actionInfoWithDynamicImageMap);
				imageMapProvider.Tag = count;
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002E1DF File Offset: 0x0002C3DF
		private ImageMapArea.ImageMapAreaShape GetMapAreaShape(MapAreaShape shape)
		{
			if (shape == null)
			{
				return ImageMapArea.ImageMapAreaShape.Rectangle;
			}
			if (1 == shape)
			{
				return ImageMapArea.ImageMapAreaShape.Circle;
			}
			return ImageMapArea.ImageMapAreaShape.Polygon;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002E1F0 File Offset: 0x0002C3F0
		private void RenderGaugePanelTopImage()
		{
			if (this.m_gaugePanel.TopImage == null)
			{
				return;
			}
			TopImage topImage = this.m_gaugePanel.TopImage;
			this.m_coreGaugeContainer.TopImage = this.AddNamedImage(topImage);
			Color color;
			if (this.GetBaseGaugeImageTransparentColor(topImage, out color))
			{
				this.m_coreGaugeContainer.TopImageTransColor = color;
			}
			ReportColorProperty hueColor = topImage.HueColor;
			if (hueColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
				{
					this.m_coreGaugeContainer.TopImageHueColor = color;
					return;
				}
				ReportColor hueColor2 = topImage.Instance.HueColor;
				if (hueColor2 != null)
				{
					this.m_coreGaugeContainer.TopImageHueColor = hueColor2.ToColor();
				}
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002E284 File Offset: 0x0002C484
		private void RenderPointerCapImage(CapImage capImage, CircularPointer circularPointer)
		{
			if (capImage == null)
			{
				return;
			}
			circularPointer.CapImage = this.AddNamedImage(capImage);
			Color color;
			if (this.GetBaseGaugeImageTransparentColor(capImage, out color))
			{
				circularPointer.CapImageTransColor = color;
			}
			ReportColorProperty hueColor = capImage.HueColor;
			if (hueColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
				{
					circularPointer.CapImageHueColor = color;
				}
				else
				{
					ReportColor hueColor2 = capImage.Instance.HueColor;
					if (hueColor2 != null)
					{
						circularPointer.CapImageHueColor = hueColor2.ToColor();
					}
				}
			}
			Point point = default(Point);
			ReportSizeProperty reportSizeProperty = capImage.OffsetX;
			if (reportSizeProperty != null)
			{
				if (!reportSizeProperty.IsExpression)
				{
					point.X = MappingHelper.ToIntPixels(reportSizeProperty.Value, base.DpiX);
				}
				else
				{
					point.X = MappingHelper.ToIntPixels(capImage.Instance.OffsetX, base.DpiX);
				}
			}
			reportSizeProperty = capImage.OffsetY;
			if (reportSizeProperty != null)
			{
				if (!reportSizeProperty.IsExpression)
				{
					point.Y = MappingHelper.ToIntPixels(reportSizeProperty.Value, base.DpiY);
				}
				else
				{
					point.Y = MappingHelper.ToIntPixels(capImage.Instance.OffsetY, base.DpiY);
				}
			}
			circularPointer.CapImageOrigin = point;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002E390 File Offset: 0x0002C590
		private void RenderGaugeTopImage(TopImage topImage, GaugeBase gaugeBase)
		{
			if (topImage == null)
			{
				return;
			}
			gaugeBase.TopImage = this.AddNamedImage(topImage);
			Color color;
			if (this.GetBaseGaugeImageTransparentColor(topImage, out color))
			{
				gaugeBase.TopImageTransColor = color;
			}
			ReportColorProperty hueColor = topImage.HueColor;
			if (hueColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
				{
					gaugeBase.TopImageHueColor = color;
					return;
				}
				ReportColor hueColor2 = topImage.Instance.HueColor;
				if (hueColor2 != null)
				{
					gaugeBase.TopImageHueColor = hueColor2.ToColor();
				}
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002E3F8 File Offset: 0x0002C5F8
		private void RenderGaugePointerImage(PointerImage pointerImage, PointerBase pointerBase)
		{
			if (pointerImage == null)
			{
				return;
			}
			pointerBase.Image = this.AddNamedImage(pointerImage);
			Color color;
			if (this.GetBaseGaugeImageTransparentColor(pointerImage, out color))
			{
				pointerBase.ImageTransColor = color;
			}
			ReportColorProperty hueColor = pointerImage.HueColor;
			if (hueColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
				{
					pointerBase.ImageHueColor = color;
				}
				else
				{
					ReportColor hueColor2 = pointerImage.Instance.HueColor;
					if (hueColor2 != null)
					{
						pointerBase.ImageHueColor = hueColor2.ToColor();
					}
				}
			}
			Point point = default(Point);
			ReportSizeProperty reportSizeProperty = pointerImage.OffsetX;
			if (reportSizeProperty != null)
			{
				if (!reportSizeProperty.IsExpression)
				{
					point.X = MappingHelper.ToIntPixels(reportSizeProperty.Value, base.DpiX);
				}
				else
				{
					point.X = MappingHelper.ToIntPixels(pointerImage.Instance.OffsetX, base.DpiX);
				}
			}
			reportSizeProperty = pointerImage.OffsetY;
			if (reportSizeProperty != null)
			{
				if (!reportSizeProperty.IsExpression)
				{
					point.Y = MappingHelper.ToIntPixels(reportSizeProperty.Value, base.DpiY);
				}
				else
				{
					point.Y = MappingHelper.ToIntPixels(pointerImage.Instance.OffsetY, base.DpiY);
				}
			}
			pointerBase.ImageOrigin = point;
			ReportDoubleProperty transparency = pointerImage.Transparency;
			if (transparency != null)
			{
				if (!transparency.IsExpression)
				{
					pointerBase.ImageTransparency = (float)transparency.Value;
					return;
				}
				pointerBase.ImageTransparency = (float)pointerImage.Instance.Transparency;
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002E53C File Offset: 0x0002C73C
		private void RenderFrameImage(FrameImage frameImage, BackFrame coreBackFrame)
		{
			if (frameImage == null)
			{
				return;
			}
			coreBackFrame.Image = this.AddNamedImage(frameImage);
			Color color;
			if (this.GetBaseGaugeImageTransparentColor(frameImage, out color))
			{
				coreBackFrame.ImageTransColor = color;
			}
			ReportColorProperty hueColor = frameImage.HueColor;
			if (hueColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
				{
					coreBackFrame.ImageHueColor = color;
					return;
				}
				ReportColor hueColor2 = frameImage.Instance.HueColor;
				if (hueColor2 != null)
				{
					coreBackFrame.ImageHueColor = hueColor2.ToColor();
				}
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002E5A4 File Offset: 0x0002C7A4
		private void RenderTickMarkImage(TopImage tickMarkImage, CustomTickMark customTickMark)
		{
			if (tickMarkImage == null)
			{
				return;
			}
			customTickMark.Image = this.AddNamedImage(tickMarkImage);
			Color color;
			if (this.GetBaseGaugeImageTransparentColor(tickMarkImage, out color))
			{
				customTickMark.ImageTransColor = color;
			}
			ReportColorProperty hueColor = tickMarkImage.HueColor;
			if (hueColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(hueColor, ref color))
				{
					customTickMark.ImageHueColor = color;
					return;
				}
				ReportColor hueColor2 = tickMarkImage.Instance.HueColor;
				if (hueColor2 != null)
				{
					customTickMark.ImageHueColor = hueColor2.ToColor();
				}
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002E60C File Offset: 0x0002C80C
		private bool GetBaseGaugeImageTransparentColor(BaseGaugeImage baseImage, out Color color)
		{
			ReportColorProperty transparentColor = baseImage.TransparentColor;
			color = Color.Empty;
			if (transparentColor != null)
			{
				if (MappingHelper.GetColorFromReportColorProperty(transparentColor, ref color))
				{
					return true;
				}
				ReportColor transparentColor2 = baseImage.Instance.TransparentColor;
				if (transparentColor2 != null)
				{
					color = transparentColor2.ToColor();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002E658 File Offset: 0x0002C858
		private void RenderData()
		{
			DateTime now = DateTime.Now;
			this.RenderGrouping(this.m_gaugePanel.GaugeMember, ref now);
			this.AssignInputValues();
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002E684 File Offset: 0x0002C884
		private void RenderGrouping(GaugeMember gaugeMember, ref DateTime timeStamp)
		{
			if (!gaugeMember.IsStatic)
			{
				GaugeDynamicMemberInstance gaugeDynamicMemberInstance = (GaugeDynamicMemberInstance)gaugeMember.Instance;
				gaugeDynamicMemberInstance.ResetContext();
				while (gaugeDynamicMemberInstance.MoveNext())
				{
					if (gaugeMember.ChildGaugeMember != null)
					{
						this.RenderGrouping(gaugeMember.ChildGaugeMember, ref timeStamp);
					}
					else
					{
						this.RenderCell(ref timeStamp);
					}
				}
				return;
			}
			if (gaugeMember.ChildGaugeMember != null)
			{
				this.RenderGrouping(gaugeMember.ChildGaugeMember, ref timeStamp);
				return;
			}
			this.RenderCell(ref timeStamp);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002E6F4 File Offset: 0x0002C8F4
		private void RenderCell(ref DateTime timeStamp)
		{
			if (this.m_inputValueOwnerInfoList == null)
			{
				return;
			}
			foreach (GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo in this.m_inputValueOwnerInfoList)
			{
				GaugeInputValue[] gaugeInputValues = inputValueOwnerInfo.GaugeInputValues;
				InputValue[] coreInputValues = inputValueOwnerInfo.CoreInputValues;
				for (int i = 0; i < gaugeInputValues.Length; i++)
				{
					GaugeInputValue gaugeInputValue = gaugeInputValues[i];
					InputValue inputValue = coreInputValues[i];
					if (inputValue != null)
					{
						double num;
						if (!gaugeInputValue.Value.IsExpression)
						{
							num = MappingHelper.ConvertToDouble(gaugeInputValue.Value.Value, true, false);
						}
						else
						{
							object value = gaugeInputValue.Instance.Value;
							if (gaugeInputValue.Instance.ErrorOccured)
							{
								if (RSTrace.ProcessingTracer.TraceError)
								{
									RSTrace.ProcessingTracer.Trace(RPResWrapper.rsGaugePanelInvalidData(this.m_gaugePanel.Name));
								}
								throw new RenderingObjectModelException(RPResWrapper.rsGaugePanelInvalidData(this.m_gaugePanel.Name));
							}
							num = MappingHelper.ConvertToDouble(value, true, false);
						}
						if (!double.IsNaN(num))
						{
							inputValue.HistoryDepth += 1L;
							inputValue.SetValue(num, timeStamp);
						}
					}
				}
			}
			timeStamp = timeStamp.AddMilliseconds(1.0);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002E848 File Offset: 0x0002CA48
		private GaugeInputValueFormulas GetFormula(GaugeInputValue gaugeInputValue)
		{
			if (gaugeInputValue.Formula == null)
			{
				return GaugeInputValueFormulas.None;
			}
			if (!gaugeInputValue.Formula.IsExpression)
			{
				return gaugeInputValue.Formula.Value;
			}
			return gaugeInputValue.Instance.Formula;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002E878 File Offset: 0x0002CA78
		private double GetAddConstant(GaugeInputValue gaugeInputValue)
		{
			if (gaugeInputValue.AddConstant == null)
			{
				return 0.0;
			}
			if (!gaugeInputValue.AddConstant.IsExpression)
			{
				return gaugeInputValue.AddConstant.Value;
			}
			return gaugeInputValue.Instance.AddConstant;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002E8B0 File Offset: 0x0002CAB0
		private double GetMultiplier(GaugeInputValue gaugeInputValue)
		{
			if (gaugeInputValue.Multiplier == null)
			{
				return 1.0;
			}
			if (!gaugeInputValue.Multiplier.IsExpression)
			{
				return gaugeInputValue.Multiplier.Value;
			}
			return gaugeInputValue.Instance.Multiplier;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002E8E8 File Offset: 0x0002CAE8
		private bool GetMinPercent(GaugeInputValue gaugeInputValue, out double minPercent)
		{
			if (gaugeInputValue.MinPercent != null)
			{
				if (!gaugeInputValue.MinPercent.IsExpression)
				{
					minPercent = gaugeInputValue.MinPercent.Value;
				}
				else
				{
					minPercent = gaugeInputValue.Instance.MinPercent;
				}
				return true;
			}
			minPercent = double.NaN;
			return false;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002E934 File Offset: 0x0002CB34
		private bool IsSampleVariance(GaugeInputValue gaugeInputValue)
		{
			return false;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002E938 File Offset: 0x0002CB38
		private bool GetMaxPercent(GaugeInputValue gaugeInputValue, out double maxPercent)
		{
			if (gaugeInputValue.MaxPercent != null)
			{
				if (!gaugeInputValue.MaxPercent.IsExpression)
				{
					maxPercent = gaugeInputValue.MaxPercent.Value;
				}
				else
				{
					maxPercent = gaugeInputValue.Instance.MaxPercent;
				}
				return true;
			}
			maxPercent = double.NaN;
			return false;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002E984 File Offset: 0x0002CB84
		private void AssignGaugeElementValues()
		{
			if (this.m_inputValueOwnerInfoList == null)
			{
				return;
			}
			foreach (GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo in this.m_inputValueOwnerInfoList)
			{
				this.AssignGaugeElementValuesToInputValues(inputValueOwnerInfo);
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002E9E0 File Offset: 0x0002CBE0
		private void AssignGaugeElementValuesToInputValues(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			switch (inputValueOwnerInfo.InputValueOwnerType)
			{
			case GaugeMapper.InputValueOwnerType.Pointer:
				this.AssignPointerElementValue(inputValueOwnerInfo);
				return;
			case GaugeMapper.InputValueOwnerType.Scale:
				this.AssignScaleElementValues(inputValueOwnerInfo);
				return;
			case GaugeMapper.InputValueOwnerType.Range:
				this.AssignRangeElementValues(inputValueOwnerInfo);
				return;
			case GaugeMapper.InputValueOwnerType.NumericIndicator:
			case GaugeMapper.InputValueOwnerType.NumericIndicatorRange:
				break;
			case GaugeMapper.InputValueOwnerType.StateIndicator:
				this.AssignStateIndicatorElementValue(inputValueOwnerInfo);
				return;
			case GaugeMapper.InputValueOwnerType.IndicatorState:
				this.AssignIndicatorStateElementValues(inputValueOwnerInfo);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002EA40 File Offset: 0x0002CC40
		private void AssignStateIndicatorElementValue(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			StateIndicator stateIndicator = (StateIndicator)inputValueOwnerInfo.CoreGaugeElements[0];
			RSTrace.ProcessingTracer.Assert(inputValueOwnerInfo.GaugeInputValues.Length == 3, "Unexpected amount of GaugeInputValue objects" + inputValueOwnerInfo.GaugeInputValues.Length.ToString());
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[0], stateIndicator.IsPercentBased ? stateIndicator.GetValueInPercents() : stateIndicator.Value);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[1], stateIndicator.Minimum);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[2], stateIndicator.Maximum);
			State currentState = stateIndicator.GetCurrentState();
			if (currentState != null)
			{
				((StateIndicator)inputValueOwnerInfo.InputValueOwnerDef).CompiledStateName = currentState.Name;
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002EAF8 File Offset: 0x0002CCF8
		private void AssignIndicatorStateElementValues(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			State state = (State)inputValueOwnerInfo.CoreGaugeElements[0];
			RSTrace.ProcessingTracer.Assert(inputValueOwnerInfo.GaugeInputValues.Length == 2, "Unexpected amount of GaugeInputValue objects" + inputValueOwnerInfo.GaugeInputValues.Length.ToString());
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[0], state.StartValue);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[1], state.EndValue);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002EB6C File Offset: 0x0002CD6C
		private void AssignPointerElementValue(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			if (inputValueOwnerInfo.CoreGaugeElements.Length == 1)
			{
				PointerBase pointerBase = (PointerBase)inputValueOwnerInfo.CoreGaugeElements[0];
				RSTrace.ProcessingTracer.Assert(inputValueOwnerInfo.GaugeInputValues.Length == 1, "Unexpected amount of GaugeInputValue objects" + inputValueOwnerInfo.GaugeInputValues.Length.ToString());
				this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[0], pointerBase.Value, pointerBase.ValueSource);
				return;
			}
			CompiledGaugePointerInstance[] array = new CompiledGaugePointerInstance[inputValueOwnerInfo.CoreGaugeElements.Length];
			for (int i = 0; i < inputValueOwnerInfo.CoreGaugeElements.Length; i++)
			{
				PointerBase pointerBase2 = (PointerBase)inputValueOwnerInfo.CoreGaugeElements[i];
				CompiledGaugePointerInstance compiledGaugePointerInstance = new CompiledGaugePointerInstance();
				CompiledGaugeInputValueInstance compiledGaugeInputValueInstance = new CompiledGaugeInputValueInstance(pointerBase2.Value);
				compiledGaugePointerInstance.GaugeInputValue = compiledGaugeInputValueInstance;
				array[i] = compiledGaugePointerInstance;
			}
			((GaugePointer)inputValueOwnerInfo.InputValueOwnerDef).CompiledInstances = array;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002EC44 File Offset: 0x0002CE44
		private void AssignScaleElementValues(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			ScaleBase scaleBase = (ScaleBase)inputValueOwnerInfo.CoreGaugeElements[0];
			RSTrace.ProcessingTracer.Assert(inputValueOwnerInfo.GaugeInputValues.Length == 2, "Unexpected amount of GaugeInputValue objects" + inputValueOwnerInfo.GaugeInputValues.Length.ToString());
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[0], scaleBase.Minimum);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[1], scaleBase.Maximum);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002ECB8 File Offset: 0x0002CEB8
		private void AssignRangeElementValues(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			RangeBase rangeBase = (RangeBase)inputValueOwnerInfo.CoreGaugeElements[0];
			RSTrace.ProcessingTracer.Assert(inputValueOwnerInfo.GaugeInputValues.Length == 2, "Unexpected amount of GaugeInputValue objects" + inputValueOwnerInfo.GaugeInputValues.Length.ToString());
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[0], rangeBase.StartValue);
			this.AssignCoreElementValue(inputValueOwnerInfo.GaugeInputValues[1], rangeBase.EndValue);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002ED2A File Offset: 0x0002CF2A
		private void AssignCoreElementValue(GaugeInputValue gaugeInputValue, double gaugeElementValue)
		{
			this.AssignCoreElementValue(gaugeInputValue, gaugeElementValue, null);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002ED38 File Offset: 0x0002CF38
		private void AssignCoreElementValue(GaugeInputValue gaugeInputValue, double gaugeElementValue, string valueSource)
		{
			if (gaugeInputValue == null || gaugeInputValue.Instance.Value == null || (gaugeInputValue.Instance.Value is string && (string)gaugeInputValue.Instance.Value == string.Empty))
			{
				return;
			}
			double num = gaugeElementValue;
			if (!string.IsNullOrEmpty(valueSource))
			{
				num = this.GetInputValueValue(this.m_coreGaugeContainer.Values[valueSource.Split(new char[] { '.' })[0]]);
			}
			gaugeInputValue.CompiledInstance = new CompiledGaugeInputValueInstance(num);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0002EDCC File Offset: 0x0002CFCC
		private void AssignInputValues()
		{
			if (this.m_inputValueOwnerInfoList == null)
			{
				return;
			}
			foreach (GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo in this.m_inputValueOwnerInfoList)
			{
				this.AssignInputValuesToGaugeElement(inputValueOwnerInfo);
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002EE28 File Offset: 0x0002D028
		private void AssignInputValuesToGaugeElement(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			switch (inputValueOwnerInfo.InputValueOwnerType)
			{
			case GaugeMapper.InputValueOwnerType.Pointer:
				this.AssignPointerValue(inputValueOwnerInfo);
				return;
			case GaugeMapper.InputValueOwnerType.Scale:
				this.AssignScaleValues(inputValueOwnerInfo);
				return;
			case GaugeMapper.InputValueOwnerType.Range:
				this.AssignRangeValues(inputValueOwnerInfo);
				return;
			case GaugeMapper.InputValueOwnerType.NumericIndicator:
			case GaugeMapper.InputValueOwnerType.NumericIndicatorRange:
				break;
			case GaugeMapper.InputValueOwnerType.StateIndicator:
				this.AssignStateIndicatorValue(inputValueOwnerInfo);
				return;
			case GaugeMapper.InputValueOwnerType.IndicatorState:
				this.AssignIndicatorStateValues(inputValueOwnerInfo);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002EE88 File Offset: 0x0002D088
		private void AssignRangeValues(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			double num = double.NaN;
			double num2 = double.NaN;
			RangeBase rangeBase = (RangeBase)inputValueOwnerInfo.CoreGaugeElements[0];
			if (inputValueOwnerInfo.CoreInputValues.Length != 0 && inputValueOwnerInfo.CoreInputValues[0] != null)
			{
				num = this.GetValue(inputValueOwnerInfo.CoreInputValues[0], inputValueOwnerInfo.GaugeInputValues[0]);
			}
			if (!double.IsNaN(num))
			{
				rangeBase.StartValue = num;
			}
			if (inputValueOwnerInfo.CoreInputValues.Length > 1 && inputValueOwnerInfo.CoreInputValues[1] != null)
			{
				num2 = this.GetValue(inputValueOwnerInfo.CoreInputValues[1], inputValueOwnerInfo.GaugeInputValues[1]);
			}
			if (!double.IsNaN(num2))
			{
				rangeBase.EndValue = num2;
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002EF2C File Offset: 0x0002D12C
		private void AssignScaleValues(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			double num = double.NaN;
			double num2 = double.NaN;
			ScaleBase scaleBase = (ScaleBase)inputValueOwnerInfo.CoreGaugeElements[0];
			if (inputValueOwnerInfo.CoreInputValues.Length != 0 && inputValueOwnerInfo.CoreInputValues[0] != null)
			{
				num = this.GetValue(inputValueOwnerInfo.CoreInputValues[0], inputValueOwnerInfo.GaugeInputValues[0]);
			}
			if (inputValueOwnerInfo.CoreInputValues.Length > 1 && inputValueOwnerInfo.CoreInputValues[1] != null)
			{
				num2 = this.GetValue(inputValueOwnerInfo.CoreInputValues[1], inputValueOwnerInfo.GaugeInputValues[1]);
			}
			if (!double.IsNaN(num) && !double.IsNaN(num2) && num >= scaleBase.Maximum)
			{
				scaleBase.Maximum = num + 1.0;
			}
			if (num >= num2)
			{
				if (RSTrace.ProcessingTracer.TraceError)
				{
					RSTrace.ProcessingTracer.Trace(RPResWrapper.rsGaugePanelInvalidMinMaxScale(this.m_gaugePanel.Name));
				}
				throw new RenderingObjectModelException(RPResWrapper.rsGaugePanelInvalidMinMaxScale(this.m_gaugePanel.Name));
			}
			if (!double.IsNaN(num))
			{
				scaleBase.Minimum = num;
			}
			if (!double.IsNaN(num2))
			{
				scaleBase.Maximum = num2;
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002F03C File Offset: 0x0002D23C
		private void AssignPointerValue(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			if (inputValueOwnerInfo.CoreInputValues.Length != 0)
			{
				GaugeInputValueFormulas formula = this.GetFormula(inputValueOwnerInfo.GaugeInputValues[0]);
				if (this.IsBuiltInFormula(formula))
				{
					((PointerBase)inputValueOwnerInfo.CoreGaugeElements[0]).ValueSource = this.GetBuiltInFormulaValueSourceName(inputValueOwnerInfo.CoreInputValues[0], inputValueOwnerInfo.GaugeInputValues[0], formula);
					return;
				}
				this.AssignPointerToCustomFormula(inputValueOwnerInfo, formula);
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002F09C File Offset: 0x0002D29C
		private void AssignStateIndicatorValue(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			StateIndicator stateIndicator = (StateIndicator)inputValueOwnerInfo.CoreGaugeElements[0];
			if (inputValueOwnerInfo.CoreInputValues.Length != 0)
			{
				InputValue inputValue = inputValueOwnerInfo.CoreInputValues[0];
				if (inputValue != null)
				{
					stateIndicator.Value = this.GetValue(inputValue, inputValueOwnerInfo.GaugeInputValues[0]);
				}
			}
			if (stateIndicator.IsPercentBased)
			{
				double num = double.NaN;
				double num2 = double.NaN;
				if (inputValueOwnerInfo.CoreInputValues.Length > 1)
				{
					InputValue inputValue = inputValueOwnerInfo.CoreInputValues[1];
					if (inputValue != null)
					{
						num = this.GetValue(inputValue, inputValueOwnerInfo.GaugeInputValues[1]);
					}
				}
				if (inputValueOwnerInfo.CoreInputValues.Length > 2)
				{
					InputValue inputValue = inputValueOwnerInfo.CoreInputValues[2];
					if (inputValue != null)
					{
						num2 = this.GetValue(inputValue, inputValueOwnerInfo.GaugeInputValues[2]);
					}
				}
				if (!double.IsNaN(num) && !double.IsNaN(num2) && num >= stateIndicator.Maximum)
				{
					stateIndicator.Maximum = num + 1.0;
				}
				if (num > num2 || (num == num2 && num2 != stateIndicator.Value))
				{
					string text = RPResWrapper.rsStateIndicatorInvalidMinMax(this.m_gaugePanel.Name, stateIndicator.Name);
					if (RSTrace.ProcessingTracer.TraceError)
					{
						RSTrace.ProcessingTracer.Trace(text);
					}
					throw new RenderingObjectModelException(text);
				}
				if (!double.IsNaN(num))
				{
					stateIndicator.Minimum = num;
				}
				if (!double.IsNaN(num2))
				{
					stateIndicator.Maximum = num2;
				}
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002F1E0 File Offset: 0x0002D3E0
		private void AssignIndicatorStateValues(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo)
		{
			double num = double.NaN;
			double num2 = double.NaN;
			State state = (State)inputValueOwnerInfo.CoreGaugeElements[0];
			if (inputValueOwnerInfo.CoreInputValues.Length != 0 && inputValueOwnerInfo.CoreInputValues[0] != null)
			{
				num = this.GetValue(inputValueOwnerInfo.CoreInputValues[0], inputValueOwnerInfo.GaugeInputValues[0]);
			}
			if (!double.IsNaN(num))
			{
				state.StartValue = num;
			}
			if (inputValueOwnerInfo.CoreInputValues.Length > 1 && inputValueOwnerInfo.CoreInputValues[1] != null)
			{
				num2 = this.GetValue(inputValueOwnerInfo.CoreInputValues[1], inputValueOwnerInfo.GaugeInputValues[1]);
			}
			if (!double.IsNaN(num2))
			{
				state.EndValue = num2;
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0002F283 File Offset: 0x0002D483
		private bool IsBuiltInFormula(GaugeInputValueFormulas formula)
		{
			return formula == GaugeInputValueFormulas.Integral || formula == GaugeInputValueFormulas.Linear || formula == GaugeInputValueFormulas.Max || formula == GaugeInputValueFormulas.Min || formula == GaugeInputValueFormulas.None || formula == GaugeInputValueFormulas.RateOfChange;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002F2A0 File Offset: 0x0002D4A0
		private void AssignPointerToCustomFormula(GaugeMapper.InputValueOwnerInfo inputValueOwnerInfo, GaugeInputValueFormulas formula)
		{
			PointerBase pointerBase = (PointerBase)inputValueOwnerInfo.CoreGaugeElements[0];
			GaugeInputValue gaugeInputValue = inputValueOwnerInfo.GaugeInputValues[0];
			InputValue inputValue = inputValueOwnerInfo.CoreInputValues[0];
			switch (formula)
			{
			case GaugeInputValueFormulas.Average:
				if (inputValue.History.Count > 0)
				{
					pointerBase.Value = GaugeMapper.FormulaHelper.Mean(this.GetValues(inputValue.History));
					return;
				}
				break;
			case GaugeInputValueFormulas.Linear:
			case GaugeInputValueFormulas.Max:
			case GaugeInputValueFormulas.Min:
				break;
			case GaugeInputValueFormulas.Median:
				if (inputValue.History.Count > 0)
				{
					pointerBase.Value = GaugeMapper.FormulaHelper.Median(this.GetValues(inputValue.History));
					return;
				}
				break;
			case GaugeInputValueFormulas.OpenClose:
				if (inputValue.History.Count > 0)
				{
					PointerBase[] array = this.CreateMultiplePointers(pointerBase, new double[]
					{
						inputValue.History[0].Value,
						inputValue.History[inputValue.History.Count - 1].Value
					});
					object[] array2 = array;
					inputValueOwnerInfo.CoreGaugeElements = array2;
				}
				break;
			case GaugeInputValueFormulas.Percentile:
				if (inputValue.History.Count > 0)
				{
					double[] percentileParameters = this.GetPercentileParameters(gaugeInputValue);
					if (percentileParameters != null)
					{
						double[] array3 = GaugeMapper.FormulaHelper.Percentile(this.GetValues(inputValue.History), percentileParameters);
						PointerBase[] array4 = this.CreateMultiplePointers(pointerBase, array3);
						object[] array2 = array4;
						inputValueOwnerInfo.CoreGaugeElements = array2;
						return;
					}
				}
				break;
			case GaugeInputValueFormulas.Variance:
				if (inputValue.History.Count > 0)
				{
					pointerBase.Value = GaugeMapper.FormulaHelper.Variance(this.GetValues(inputValue.History), this.IsSampleVariance(gaugeInputValue));
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002F424 File Offset: 0x0002D624
		private PointerBase[] CreateMultiplePointers(PointerBase pointer, double[] values)
		{
			pointer.Value = values[0];
			PointerBase[] array = new PointerBase[values.Length];
			array[0] = pointer;
			if (pointer is CircularPointer)
			{
				CircularGauge circularGauge = (CircularGauge)pointer.ParentElement;
				for (int i = 1; i < values.Length; i++)
				{
					CircularPointer circularPointer = (CircularPointer)pointer.Clone();
					circularPointer.Name = circularGauge.Pointers.GenerateUniqueName(circularPointer);
					circularPointer.Value = values[i];
					circularGauge.Pointers.Add(circularPointer);
					array[i] = circularPointer;
				}
			}
			else if (pointer is LinearPointer)
			{
				LinearGauge linearGauge = (LinearGauge)pointer.ParentElement;
				for (int j = 1; j < values.Length; j++)
				{
					LinearPointer linearPointer = (LinearPointer)pointer.Clone();
					linearPointer.Name = linearGauge.Pointers.GenerateUniqueName(linearPointer);
					linearPointer.Value = values[j];
					linearGauge.Pointers.Add(linearPointer);
					array[j] = linearPointer;
				}
			}
			return array;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002F50D File Offset: 0x0002D70D
		public override void Dispose()
		{
			if (this.m_coreGaugeContainer != null)
			{
				this.m_coreGaugeContainer.Dispose();
			}
			this.m_coreGaugeContainer = null;
			base.Dispose();
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002F52F File Offset: 0x0002D72F
		private global::System.Drawing.Image GetImageFromStream(BaseGaugeImage baseGaugeImage)
		{
			if (baseGaugeImage.Instance.ImageData == null)
			{
				return null;
			}
			return global::System.Drawing.Image.FromStream(new MemoryStream(baseGaugeImage.Instance.ImageData, false));
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0002F558 File Offset: 0x0002D758
		private string GetBuiltInFormulaValueSourceName(InputValue inputValue, GaugeInputValue gaugeInputValue, GaugeInputValueFormulas formula)
		{
			this.CreateBuiltInFormula(inputValue, gaugeInputValue, formula);
			string text = inputValue.Name;
			if (inputValue.CalculatedValues.Count != 0)
			{
				text = text + "." + inputValue.CalculatedValues[0].Name;
			}
			return text;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0002F5A5 File Offset: 0x0002D7A5
		private double GetBuiltInFormulaValue(InputValue inputValue, GaugeInputValue gaugeInputValue, GaugeInputValueFormulas formula)
		{
			this.CreateBuiltInFormula(inputValue, gaugeInputValue, formula);
			return this.GetInputValueValue(inputValue);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002F5B7 File Offset: 0x0002D7B7
		private double GetInputValueValue(InputValue inputValue)
		{
			if (inputValue.CalculatedValues.Count == 0)
			{
				return inputValue.Value;
			}
			return inputValue.CalculatedValues[0].Value;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002F5E4 File Offset: 0x0002D7E4
		private void CreateBuiltInFormula(InputValue inputValue, GaugeInputValue gaugeInputValue, GaugeInputValueFormulas formula)
		{
			switch (formula)
			{
			case GaugeInputValueFormulas.Linear:
			{
				CalculatedValueLinear calculatedValueLinear = new CalculatedValueLinear();
				calculatedValueLinear.AddConstant = this.GetAddConstant(gaugeInputValue);
				calculatedValueLinear.Multiplier = this.GetMultiplier(gaugeInputValue);
				inputValue.CalculatedValues.Add(calculatedValueLinear);
				return;
			}
			case GaugeInputValueFormulas.Max:
				inputValue.CalculatedValues.Add(new CalculatedValueMax());
				return;
			case GaugeInputValueFormulas.Min:
				inputValue.CalculatedValues.Add(new CalculatedValueMin());
				return;
			case GaugeInputValueFormulas.Median:
			case GaugeInputValueFormulas.OpenClose:
			case GaugeInputValueFormulas.Percentile:
			case GaugeInputValueFormulas.Variance:
				break;
			case GaugeInputValueFormulas.RateOfChange:
			{
				CalculatedValueRateOfChange calculatedValueRateOfChange = new CalculatedValueRateOfChange();
				inputValue.CalculatedValues.Add(calculatedValueRateOfChange);
				break;
			}
			case GaugeInputValueFormulas.Integral:
			{
				CalculatedValueIntegral calculatedValueIntegral = new CalculatedValueIntegral();
				inputValue.CalculatedValues.Add(calculatedValueIntegral);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002F698 File Offset: 0x0002D898
		private double GetValue(InputValue inputValue, GaugeInputValue gaugeInputValue)
		{
			GaugeInputValueFormulas formula = this.GetFormula(gaugeInputValue);
			if (this.IsBuiltInFormula(formula))
			{
				return this.GetBuiltInFormulaValue(inputValue, gaugeInputValue, formula);
			}
			switch (formula)
			{
			case GaugeInputValueFormulas.Average:
				if (inputValue.History.Count > 0)
				{
					return GaugeMapper.FormulaHelper.Mean(this.GetValues(inputValue.History));
				}
				break;
			case GaugeInputValueFormulas.Median:
				if (inputValue.History.Count > 0)
				{
					return GaugeMapper.FormulaHelper.Median(this.GetValues(inputValue.History));
				}
				break;
			case GaugeInputValueFormulas.OpenClose:
				if (inputValue.History.Count > 0)
				{
					return inputValue.History[0].Value;
				}
				break;
			case GaugeInputValueFormulas.Percentile:
				if (inputValue.History.Count > 0)
				{
					double[] percentileParameters = this.GetPercentileParameters(gaugeInputValue);
					if (percentileParameters != null)
					{
						return GaugeMapper.FormulaHelper.Percentile(this.GetValues(inputValue.History), percentileParameters)[0];
					}
				}
				break;
			case GaugeInputValueFormulas.Variance:
				if (inputValue.History.Count > 0)
				{
					return GaugeMapper.FormulaHelper.Variance(this.GetValues(inputValue.History), this.IsSampleVariance(gaugeInputValue));
				}
				break;
			}
			return double.NaN;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002F7B4 File Offset: 0x0002D9B4
		private double[] GetPercentileParameters(GaugeInputValue gaugeInputValue)
		{
			double naN = double.NaN;
			double naN2 = double.NaN;
			double[] array = null;
			bool minPercent = this.GetMinPercent(gaugeInputValue, out naN);
			bool maxPercent = this.GetMaxPercent(gaugeInputValue, out naN2);
			if (minPercent && maxPercent)
			{
				array = new double[] { naN, naN2 };
			}
			else if (minPercent)
			{
				array = new double[] { naN };
			}
			else if (maxPercent)
			{
				array = new double[] { naN2 };
			}
			return array;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0002F824 File Offset: 0x0002DA24
		private double[] GetValues(HistoryCollection historyColletion)
		{
			double[] array = new double[historyColletion.Count];
			for (int i = 0; i < historyColletion.Count; i++)
			{
				array[i] = historyColletion[i].Value;
			}
			return array;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002F85E File Offset: 0x0002DA5E
		private float GetPanelItemLeft(GaugePanelItem gaugePanelItem)
		{
			if (gaugePanelItem.Left == null)
			{
				return 0f;
			}
			if (!gaugePanelItem.Left.IsExpression)
			{
				return (float)gaugePanelItem.Left.Value;
			}
			return (float)gaugePanelItem.Instance.Left;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002F894 File Offset: 0x0002DA94
		private float GetPanelItemTop(GaugePanelItem gaugePanelItem)
		{
			if (gaugePanelItem.Top == null)
			{
				return 0f;
			}
			if (!gaugePanelItem.Top.IsExpression)
			{
				return (float)gaugePanelItem.Top.Value;
			}
			return (float)gaugePanelItem.Instance.Top;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002F8CA File Offset: 0x0002DACA
		private float GetPanelItemWidth(GaugePanelItem gaugePanelItem)
		{
			if (gaugePanelItem.Width == null)
			{
				return 0f;
			}
			if (!gaugePanelItem.Width.IsExpression)
			{
				return (float)gaugePanelItem.Width.Value;
			}
			return (float)gaugePanelItem.Instance.Width;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002F900 File Offset: 0x0002DB00
		private float GetPanelItemHeight(GaugePanelItem gaugePanelItem)
		{
			if (gaugePanelItem.Height == null)
			{
				return 0f;
			}
			if (!gaugePanelItem.Height.IsExpression)
			{
				return (float)gaugePanelItem.Height.Value;
			}
			return (float)gaugePanelItem.Instance.Height;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002F936 File Offset: 0x0002DB36
		private bool GetPanelItemHidden(GaugePanelItem gaugePanelItem)
		{
			if (gaugePanelItem.Hidden == null)
			{
				return false;
			}
			if (!gaugePanelItem.Hidden.IsExpression)
			{
				return gaugePanelItem.Hidden.Value;
			}
			return gaugePanelItem.Instance.Hidden;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002F966 File Offset: 0x0002DB66
		private bool GetPanelItemZIndex(GaugePanelItem gaugePanelItem, out int zIndex)
		{
			if (gaugePanelItem.ZIndex != null)
			{
				if (!gaugePanelItem.ZIndex.IsExpression)
				{
					zIndex = gaugePanelItem.ZIndex.Value;
				}
				else
				{
					zIndex = gaugePanelItem.Instance.ZIndex;
				}
				return true;
			}
			zIndex = 0;
			return false;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002F9A0 File Offset: 0x0002DBA0
		private bool GetPanelItemToolTip(GaugePanelItem gaugePanelItem, out string toolTip)
		{
			toolTip = null;
			ReportStringProperty toolTip2 = gaugePanelItem.ToolTip;
			if (toolTip2 != null)
			{
				if (!toolTip2.IsExpression)
				{
					toolTip = toolTip2.Value;
				}
				else
				{
					toolTip = gaugePanelItem.Instance.ToolTip;
				}
			}
			return toolTip != null;
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0002F9DE File Offset: 0x0002DBDE
		private List<GaugeMapper.InputValueOwnerInfo> InputValueOwnerInfoList
		{
			get
			{
				if (this.m_inputValueOwnerInfoList == null)
				{
					this.m_inputValueOwnerInfoList = new List<GaugeMapper.InputValueOwnerInfo>();
				}
				return this.m_inputValueOwnerInfoList;
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002F9F9 File Offset: 0x0002DBF9
		private AntiAliasing GetAntiAliasing(GaugeAntiAliasings gaugeAntiAliasing)
		{
			switch (gaugeAntiAliasing)
			{
			case GaugeAntiAliasings.None:
				return 0;
			case GaugeAntiAliasings.Text:
				return 1;
			case GaugeAntiAliasings.Graphics:
				return 2;
			default:
				return 3;
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002FA18 File Offset: 0x0002DC18
		private TextAntiAliasingQuality GetTextAntiAliasingQuality(TextAntiAliasingQualities textAntiAliasingQuality)
		{
			if (textAntiAliasingQuality == TextAntiAliasingQualities.Normal)
			{
				return 0;
			}
			if (textAntiAliasingQuality != TextAntiAliasingQualities.SystemDefault)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002FA29 File Offset: 0x0002DC29
		private BarStart GetBarStart(GaugeBarStarts barStart)
		{
			if (barStart == GaugeBarStarts.Zero)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002FA32 File Offset: 0x0002DC32
		private MarkerStyle GetMarkerStyle(GaugeMarkerStyles markerStyle)
		{
			switch (markerStyle)
			{
			case GaugeMarkerStyles.None:
				return 0;
			case GaugeMarkerStyles.Rectangle:
				return 1;
			case GaugeMarkerStyles.Circle:
				return 3;
			case GaugeMarkerStyles.Diamond:
				return 4;
			case GaugeMarkerStyles.Trapezoid:
				return 5;
			case GaugeMarkerStyles.Star:
				return 6;
			case GaugeMarkerStyles.Wedge:
				return 7;
			case GaugeMarkerStyles.Pentagon:
				return 8;
			default:
				return 2;
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002FA6F File Offset: 0x0002DC6F
		private Placement GetPlacement(GaugePointerPlacements placement)
		{
			if (placement == GaugePointerPlacements.Outside)
			{
				return 1;
			}
			if (placement == GaugePointerPlacements.Inside)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002FA7E File Offset: 0x0002DC7E
		private Placement GetPlacement(ScaleRangePlacements placement)
		{
			if (placement == ScaleRangePlacements.Outside)
			{
				return 1;
			}
			if (placement == ScaleRangePlacements.Cross)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002FA8D File Offset: 0x0002DC8D
		private Placement GetPlacement(GaugeLabelPlacements placement)
		{
			if (placement == GaugeLabelPlacements.Outside)
			{
				return 1;
			}
			if (placement == GaugeLabelPlacements.Cross)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002FA9C File Offset: 0x0002DC9C
		private CircularPointerType GetCircularPointerType(RadialPointerTypes pointerType)
		{
			if (pointerType == RadialPointerTypes.Marker)
			{
				return 1;
			}
			if (pointerType == RadialPointerTypes.Bar)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002FAAC File Offset: 0x0002DCAC
		private NeedleStyle GetNeedleStyle(RadialPointerNeedleStyles needleStyle)
		{
			switch (needleStyle)
			{
			case RadialPointerNeedleStyles.Rectangular:
				return 1;
			case RadialPointerNeedleStyles.TaperedWithTail:
				return 2;
			case RadialPointerNeedleStyles.Tapered:
				return 3;
			case RadialPointerNeedleStyles.ArrowWithTail:
				return 4;
			case RadialPointerNeedleStyles.Arrow:
				return 5;
			case RadialPointerNeedleStyles.StealthArrowWithTail:
				return 6;
			case RadialPointerNeedleStyles.StealthArrow:
				return 7;
			case RadialPointerNeedleStyles.TaperedWithStealthArrow:
				return 8;
			case RadialPointerNeedleStyles.StealthArrowWithWideTail:
				return 9;
			case RadialPointerNeedleStyles.TaperedWithRoundedPoint:
				return 10;
			default:
				return 0;
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002FB02 File Offset: 0x0002DD02
		private BackFrameStyle GetFrameStyle(GaugeFrameStyles gaugeFrameStyles)
		{
			if (gaugeFrameStyles == GaugeFrameStyles.Simple)
			{
				return 1;
			}
			if (gaugeFrameStyles == GaugeFrameStyles.Edged)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002FB14 File Offset: 0x0002DD14
		private BackFrameShape GetFrameShape(GaugeFrameShapes gaugeFrameShapes)
		{
			switch (gaugeFrameShapes)
			{
			case GaugeFrameShapes.Rectangular:
				return 1;
			case GaugeFrameShapes.RoundedRectangular:
				return 2;
			case GaugeFrameShapes.AutoShape:
				return 3;
			case GaugeFrameShapes.CustomCircular1:
				return 1001;
			case GaugeFrameShapes.CustomCircular2:
				return 1002;
			case GaugeFrameShapes.CustomCircular3:
				return 1003;
			case GaugeFrameShapes.CustomCircular4:
				return 1004;
			case GaugeFrameShapes.CustomCircular5:
				return 1005;
			case GaugeFrameShapes.CustomCircular6:
				return 1006;
			case GaugeFrameShapes.CustomCircular7:
				return 1007;
			case GaugeFrameShapes.CustomCircular8:
				return 1008;
			case GaugeFrameShapes.CustomCircular9:
				return 1009;
			case GaugeFrameShapes.CustomCircular10:
				return 1010;
			case GaugeFrameShapes.CustomCircular11:
				return 1011;
			case GaugeFrameShapes.CustomCircular12:
				return 1012;
			case GaugeFrameShapes.CustomCircular13:
				return 1013;
			case GaugeFrameShapes.CustomCircular14:
				return 1014;
			case GaugeFrameShapes.CustomCircular15:
				return 1015;
			case GaugeFrameShapes.CustomSemiCircularN1:
				return 2001;
			case GaugeFrameShapes.CustomSemiCircularN2:
				return 2002;
			case GaugeFrameShapes.CustomSemiCircularN3:
				return 2003;
			case GaugeFrameShapes.CustomSemiCircularN4:
				return 2004;
			case GaugeFrameShapes.CustomSemiCircularS1:
				return 2101;
			case GaugeFrameShapes.CustomSemiCircularS2:
				return 2102;
			case GaugeFrameShapes.CustomSemiCircularS3:
				return 2103;
			case GaugeFrameShapes.CustomSemiCircularS4:
				return 2104;
			case GaugeFrameShapes.CustomSemiCircularE1:
				return 2201;
			case GaugeFrameShapes.CustomSemiCircularE2:
				return 2202;
			case GaugeFrameShapes.CustomSemiCircularE3:
				return 2203;
			case GaugeFrameShapes.CustomSemiCircularE4:
				return 2204;
			case GaugeFrameShapes.CustomSemiCircularW1:
				return 2301;
			case GaugeFrameShapes.CustomSemiCircularW2:
				return 2302;
			case GaugeFrameShapes.CustomSemiCircularW3:
				return 2303;
			case GaugeFrameShapes.CustomSemiCircularW4:
				return 2304;
			case GaugeFrameShapes.CustomQuarterCircularNE1:
				return 3001;
			case GaugeFrameShapes.CustomQuarterCircularNE2:
				return 3002;
			case GaugeFrameShapes.CustomQuarterCircularNE3:
				return 3003;
			case GaugeFrameShapes.CustomQuarterCircularNE4:
				return 3004;
			case GaugeFrameShapes.CustomQuarterCircularNW1:
				return 3101;
			case GaugeFrameShapes.CustomQuarterCircularNW2:
				return 3102;
			case GaugeFrameShapes.CustomQuarterCircularNW3:
				return 3103;
			case GaugeFrameShapes.CustomQuarterCircularNW4:
				return 3104;
			case GaugeFrameShapes.CustomQuarterCircularSE1:
				return 3201;
			case GaugeFrameShapes.CustomQuarterCircularSE2:
				return 3202;
			case GaugeFrameShapes.CustomQuarterCircularSE3:
				return 3203;
			case GaugeFrameShapes.CustomQuarterCircularSE4:
				return 3204;
			case GaugeFrameShapes.CustomQuarterCircularSW1:
				return 3301;
			case GaugeFrameShapes.CustomQuarterCircularSW2:
				return 3302;
			case GaugeFrameShapes.CustomQuarterCircularSW3:
				return 3303;
			case GaugeFrameShapes.CustomQuarterCircularSW4:
				return 3304;
			default:
				return 0;
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002FD17 File Offset: 0x0002DF17
		private GlassEffect GetGlassEffect(GaugeGlassEffects gaugeGlassEffects)
		{
			if (gaugeGlassEffects == GaugeGlassEffects.Simple)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002FD20 File Offset: 0x0002DF20
		private FontUnit GetFontUnit(bool useFontAsPercent)
		{
			if (useFontAsPercent)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002FD28 File Offset: 0x0002DF28
		private CapStyle GetCapStyle(GaugeCapStyles capStyle)
		{
			switch (capStyle)
			{
			case GaugeCapStyles.Rounded:
				return 1;
			case GaugeCapStyles.RoundedLight:
				return 2;
			case GaugeCapStyles.RoundedWithAdditionalTop:
				return 3;
			case GaugeCapStyles.RoundedWithWideIndentation:
				return 4;
			case GaugeCapStyles.FlattenedWithIndentation:
				return 5;
			case GaugeCapStyles.FlattenedWithWideIndentation:
				return 6;
			case GaugeCapStyles.RoundedGlossyWithIndentation:
				return 7;
			case GaugeCapStyles.RoundedWithIndentation:
				return 8;
			default:
				return 0;
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002FD65 File Offset: 0x0002DF65
		private MarkerStyle GetMarkerStyle(GaugeTickMarkShapes shape)
		{
			switch (shape)
			{
			case GaugeTickMarkShapes.None:
				return 0;
			case GaugeTickMarkShapes.Triangle:
				return 2;
			case GaugeTickMarkShapes.Circle:
				return 3;
			case GaugeTickMarkShapes.Diamond:
				return 4;
			case GaugeTickMarkShapes.Trapezoid:
				return 5;
			case GaugeTickMarkShapes.Star:
				return 6;
			case GaugeTickMarkShapes.Wedge:
				return 7;
			case GaugeTickMarkShapes.Pentagon:
				return 8;
			default:
				return 1;
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002FDA4 File Offset: 0x0002DFA4
		private GaugeDashStyle GetDashStyle(Border border)
		{
			switch (MappingHelper.GetStyleBorderStyle(border))
			{
			case BorderStyles.Dotted:
				return 4;
			case BorderStyles.Dashed:
				return 1;
			case BorderStyles.Solid:
			case BorderStyles.Double:
				return 5;
			case BorderStyles.DashDot:
				return 2;
			case BorderStyles.DashDotDot:
				return 3;
			default:
				return 0;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002FDE8 File Offset: 0x0002DFE8
		private GaugeHatchStyle GetHatchStyle(Style style, StyleInstance styleInstance)
		{
			switch (MappingHelper.GetStyleBackgroundHatchType(style, styleInstance))
			{
			case BackgroundHatchTypes.BackwardDiagonal:
				return 1;
			case BackgroundHatchTypes.Cross:
				return 2;
			case BackgroundHatchTypes.DarkDownwardDiagonal:
				return 3;
			case BackgroundHatchTypes.DarkHorizontal:
				return 4;
			case BackgroundHatchTypes.DarkUpwardDiagonal:
				return 5;
			case BackgroundHatchTypes.DarkVertical:
				return 6;
			case BackgroundHatchTypes.DashedDownwardDiagonal:
				return 7;
			case BackgroundHatchTypes.DashedHorizontal:
				return 8;
			case BackgroundHatchTypes.DashedUpwardDiagonal:
				return 9;
			case BackgroundHatchTypes.DashedVertical:
				return 10;
			case BackgroundHatchTypes.DiagonalBrick:
				return 11;
			case BackgroundHatchTypes.DiagonalCross:
				return 12;
			case BackgroundHatchTypes.Divot:
				return 13;
			case BackgroundHatchTypes.DottedDiamond:
				return 14;
			case BackgroundHatchTypes.DottedGrid:
				return 15;
			case BackgroundHatchTypes.ForwardDiagonal:
				return 16;
			case BackgroundHatchTypes.Horizontal:
				return 17;
			case BackgroundHatchTypes.HorizontalBrick:
				return 18;
			case BackgroundHatchTypes.LargeCheckerBoard:
				return 19;
			case BackgroundHatchTypes.LargeConfetti:
				return 20;
			case BackgroundHatchTypes.LargeGrid:
				return 21;
			case BackgroundHatchTypes.LightDownwardDiagonal:
				return 22;
			case BackgroundHatchTypes.LightHorizontal:
				return 23;
			case BackgroundHatchTypes.LightUpwardDiagonal:
				return 24;
			case BackgroundHatchTypes.LightVertical:
				return 25;
			case BackgroundHatchTypes.NarrowHorizontal:
				return 26;
			case BackgroundHatchTypes.NarrowVertical:
				return 27;
			case BackgroundHatchTypes.OutlinedDiamond:
				return 28;
			case BackgroundHatchTypes.Percent05:
				return 29;
			case BackgroundHatchTypes.Percent10:
				return 30;
			case BackgroundHatchTypes.Percent20:
				return 31;
			case BackgroundHatchTypes.Percent25:
				return 32;
			case BackgroundHatchTypes.Percent30:
				return 33;
			case BackgroundHatchTypes.Percent40:
				return 34;
			case BackgroundHatchTypes.Percent50:
				return 35;
			case BackgroundHatchTypes.Percent60:
				return 36;
			case BackgroundHatchTypes.Percent70:
				return 37;
			case BackgroundHatchTypes.Percent75:
				return 38;
			case BackgroundHatchTypes.Percent80:
				return 39;
			case BackgroundHatchTypes.Percent90:
				return 40;
			case BackgroundHatchTypes.Plaid:
				return 41;
			case BackgroundHatchTypes.Shingle:
				return 42;
			case BackgroundHatchTypes.SmallCheckerBoard:
				return 43;
			case BackgroundHatchTypes.SmallConfetti:
				return 44;
			case BackgroundHatchTypes.SmallGrid:
				return 45;
			case BackgroundHatchTypes.SolidDiamond:
				return 46;
			case BackgroundHatchTypes.Sphere:
				return 47;
			case BackgroundHatchTypes.Trellis:
				return 48;
			case BackgroundHatchTypes.Vertical:
				return 49;
			case BackgroundHatchTypes.Wave:
				return 50;
			case BackgroundHatchTypes.Weave:
				return 51;
			case BackgroundHatchTypes.WideDownwardDiagonal:
				return 52;
			case BackgroundHatchTypes.WideUpwardDiagonal:
				return 53;
			case BackgroundHatchTypes.ZigZag:
				return 54;
			default:
				return 0;
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002FF80 File Offset: 0x0002E180
		private GradientType GetGradientType(Style style, StyleInstance styleInstance)
		{
			switch (MappingHelper.GetStyleBackGradientType(style, styleInstance))
			{
			case BackgroundGradients.LeftRight:
				return 1;
			case BackgroundGradients.TopBottom:
				return 2;
			case BackgroundGradients.Center:
				return 3;
			case BackgroundGradients.DiagonalLeft:
				return 4;
			case BackgroundGradients.DiagonalRight:
				return 5;
			case BackgroundGradients.HorizontalCenter:
				return 6;
			case BackgroundGradients.VerticalCenter:
				return 7;
			default:
				return 0;
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002FFCA File Offset: 0x0002E1CA
		private RangeGradientType GetRangeGradientType(BackgroundGradientTypes gradient)
		{
			switch (gradient)
			{
			case BackgroundGradientTypes.StartToEnd:
				return 1;
			case BackgroundGradientTypes.LeftRight:
				return 2;
			case BackgroundGradientTypes.TopBottom:
				return 3;
			case BackgroundGradientTypes.Center:
				return 4;
			case BackgroundGradientTypes.DiagonalLeft:
				return 5;
			case BackgroundGradientTypes.DiagonalRight:
				return 6;
			case BackgroundGradientTypes.HorizontalCenter:
				return 7;
			case BackgroundGradientTypes.VerticalCenter:
				return 8;
			}
			return 0;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00030009 File Offset: 0x0002E209
		private GaugeOrientation GetGaugeOrientation(GaugeOrientations gaugeOrientation)
		{
			if (gaugeOrientation == GaugeOrientations.Horizontal)
			{
				return 0;
			}
			if (gaugeOrientation != GaugeOrientations.Vertical)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0003001A File Offset: 0x0002E21A
		private LinearPointerType GetLinearPointerType(LinearPointerTypes type)
		{
			if (type == LinearPointerTypes.Bar)
			{
				return 1;
			}
			if (type != LinearPointerTypes.Thermometer)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0003002B File Offset: 0x0002E22B
		private ThermometerStyle GetThermometerStyle(GaugeThermometerStyles thermometerStyle)
		{
			if (thermometerStyle == GaugeThermometerStyles.Flask)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00030034 File Offset: 0x0002E234
		private ResizeMode GetResizeMode(GaugeResizeModes resizeMode)
		{
			if (resizeMode == GaugeResizeModes.None)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00030040 File Offset: 0x0002E240
		private string GetParentName(string parentItemName)
		{
			string[] array = parentItemName.Split(new char[] { '.' });
			if (array.Length == 2 && array[0] == GaugeMapper.m_RadialGaugesName)
			{
				return GaugeMapper.m_CircularGaugesName + "." + array[1];
			}
			return parentItemName;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00030088 File Offset: 0x0002E288
		private string AddNamedImage(BaseGaugeImage topImage)
		{
			global::System.Drawing.Image imageFromStream = this.GetImageFromStream(topImage);
			if (imageFromStream == null)
			{
				return "";
			}
			string text = "image" + this.m_coreGaugeContainer.NamedImages.Count.ToString(CultureInfo.InvariantCulture);
			NamedImage namedImage = new NamedImage(text, imageFromStream);
			this.m_coreGaugeContainer.NamedImages.Add(namedImage);
			return text;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000300E7 File Offset: 0x0002E2E7
		private int GetValidShadowOffset(int shadowOffset)
		{
			return Math.Min(shadowOffset, 100);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000300F4 File Offset: 0x0002E2F4
		private string FormatNumber(object sender, double value, string format)
		{
			if (this.m_formatter == null)
			{
				this.m_formatter = new Formatter(this.m_gaugePanel.GaugePanelDef.StyleClass, this.m_gaugePanel.RenderingContext.OdpContext, ObjectType.GaugePanel, this.m_gaugePanel.Name);
			}
			return this.m_formatter.FormatValue(value, format, TypeCode.Double);
		}

		// Token: 0x04000468 RID: 1128
		private GaugePanel m_gaugePanel;

		// Token: 0x04000469 RID: 1129
		private GaugeContainer m_coreGaugeContainer;

		// Token: 0x0400046A RID: 1130
		private ActionInfoWithDynamicImageMapCollection m_actions = new ActionInfoWithDynamicImageMapCollection();

		// Token: 0x0400046B RID: 1131
		private Formatter m_formatter;

		// Token: 0x0400046C RID: 1132
		private static string m_CircularGaugesName = "CircularGauges";

		// Token: 0x0400046D RID: 1133
		private static string m_RadialGaugesName = "RadialGauges";

		// Token: 0x0400046E RID: 1134
		private List<GaugeMapper.InputValueOwnerInfo> m_inputValueOwnerInfoList;

		// Token: 0x02000928 RID: 2344
		private class InputValueOwnerInfo
		{
			// Token: 0x04003F80 RID: 16256
			public object[] CoreGaugeElements;

			// Token: 0x04003F81 RID: 16257
			public GaugeInputValue[] GaugeInputValues;

			// Token: 0x04003F82 RID: 16258
			public InputValue[] CoreInputValues;

			// Token: 0x04003F83 RID: 16259
			public GaugeMapper.InputValueOwnerType InputValueOwnerType;

			// Token: 0x04003F84 RID: 16260
			public object InputValueOwnerDef;
		}

		// Token: 0x02000929 RID: 2345
		private enum InputValueOwnerType
		{
			// Token: 0x04003F86 RID: 16262
			Pointer,
			// Token: 0x04003F87 RID: 16263
			Scale,
			// Token: 0x04003F88 RID: 16264
			Range,
			// Token: 0x04003F89 RID: 16265
			NumericIndicator,
			// Token: 0x04003F8A RID: 16266
			NumericIndicatorRange,
			// Token: 0x04003F8B RID: 16267
			StateIndicator,
			// Token: 0x04003F8C RID: 16268
			IndicatorState
		}

		// Token: 0x0200092A RID: 2346
		private static class FormulaHelper
		{
			// Token: 0x06007F59 RID: 32601 RVA: 0x0020D258 File Offset: 0x0020B458
			public static double[] Percentile(double[] values, double[] requiredPercentile)
			{
				double[] array = new double[requiredPercentile.Length];
				Array.Sort<double>(values);
				int num = 0;
				foreach (double num2 in requiredPercentile)
				{
					double num3 = ((double)values.Length - 1.0) / 100.0 * num2;
					double num4 = Math.Floor(num3);
					double num5 = num3 - num4;
					array[num] = 0.0;
					if ((int)num4 < values.Length)
					{
						array[num] += (1.0 - num5) * values[(int)num4];
					}
					if ((int)(num4 + 1.0) < values.Length)
					{
						array[num] += num5 * values[(int)num4 + 1];
					}
					num++;
				}
				return array;
			}

			// Token: 0x06007F5A RID: 32602 RVA: 0x0020D318 File Offset: 0x0020B518
			public static double Variance(double[] values, bool sampleVariance)
			{
				double num = 0.0;
				double num2 = GaugeMapper.FormulaHelper.Mean(values);
				foreach (double num3 in values)
				{
					num += (num3 - num2) * (num3 - num2);
				}
				if (sampleVariance)
				{
					return num / (double)(values.Length - 1);
				}
				return num / (double)values.Length;
			}

			// Token: 0x06007F5B RID: 32603 RVA: 0x0020D36C File Offset: 0x0020B56C
			public static double Median(double[] values)
			{
				GaugeMapper.FormulaHelper.Sort(ref values);
				int num = values.Length / 2;
				if (values.Length % 2 == 0)
				{
					return (values[num - 1] + values[num]) / 2.0;
				}
				return values[num];
			}

			// Token: 0x06007F5C RID: 32604 RVA: 0x0020D3A4 File Offset: 0x0020B5A4
			public static double Mean(double[] values)
			{
				double num = 0.0;
				foreach (double num2 in values)
				{
					num += num2;
				}
				return num / (double)values.Length;
			}

			// Token: 0x06007F5D RID: 32605 RVA: 0x0020D3DC File Offset: 0x0020B5DC
			private static void Sort(ref double[] values)
			{
				for (int i = 0; i < values.Length; i++)
				{
					for (int j = i + 1; j < values.Length; j++)
					{
						if (values[i] > values[j])
						{
							double num = values[i];
							values[i] = values[j];
							values[j] = num;
						}
					}
				}
			}
		}

		// Token: 0x0200092B RID: 2347
		private class TraceContext : ITraceContext
		{
			// Token: 0x06007F5E RID: 32606 RVA: 0x0020D428 File Offset: 0x0020B628
			public TraceContext()
			{
				this.m_startTime = (this.m_lastOperation = DateTime.Now);
			}

			// Token: 0x1700295E RID: 10590
			// (get) Token: 0x06007F5F RID: 32607 RVA: 0x0020D44F File Offset: 0x0020B64F
			public bool TraceEnabled
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06007F60 RID: 32608 RVA: 0x0020D454 File Offset: 0x0020B654
			public void Write(string category, string message)
			{
				RSTrace.ProcessingTracer.Trace(string.Concat(new string[]
				{
					category,
					"; ",
					message,
					"; ",
					(DateTime.Now - this.m_startTime).TotalMilliseconds.ToString(),
					"; ",
					(DateTime.Now - this.m_lastOperation).TotalMilliseconds.ToString()
				}));
				this.m_lastOperation = DateTime.Now;
			}

			// Token: 0x04003F8D RID: 16269
			private DateTime m_startTime;

			// Token: 0x04003F8E RID: 16270
			private DateTime m_lastOperation;
		}
	}
}
