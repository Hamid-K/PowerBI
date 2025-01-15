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
	// Token: 0x020003FB RID: 1019
	[SkipStaticValidation]
	[Serializable]
	internal sealed class NumericIndicator : GaugePanelItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002B21 RID: 11041 RVA: 0x000C7D10 File Offset: 0x000C5F10
		internal NumericIndicator()
		{
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000C7D18 File Offset: 0x000C5F18
		internal NumericIndicator(GaugePanel gaugePanel, int id)
			: base(gaugePanel, id)
		{
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x000C7D22 File Offset: 0x000C5F22
		// (set) Token: 0x06002B24 RID: 11044 RVA: 0x000C7D2A File Offset: 0x000C5F2A
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

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x000C7D33 File Offset: 0x000C5F33
		// (set) Token: 0x06002B26 RID: 11046 RVA: 0x000C7D3B File Offset: 0x000C5F3B
		internal List<NumericIndicatorRange> NumericIndicatorRanges
		{
			get
			{
				return this.m_numericIndicatorRanges;
			}
			set
			{
				this.m_numericIndicatorRanges = value;
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x000C7D44 File Offset: 0x000C5F44
		// (set) Token: 0x06002B28 RID: 11048 RVA: 0x000C7D4C File Offset: 0x000C5F4C
		internal ExpressionInfo DecimalDigitColor
		{
			get
			{
				return this.m_decimalDigitColor;
			}
			set
			{
				this.m_decimalDigitColor = value;
			}
		}

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x000C7D55 File Offset: 0x000C5F55
		// (set) Token: 0x06002B2A RID: 11050 RVA: 0x000C7D5D File Offset: 0x000C5F5D
		internal ExpressionInfo DigitColor
		{
			get
			{
				return this.m_digitColor;
			}
			set
			{
				this.m_digitColor = value;
			}
		}

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000C7D66 File Offset: 0x000C5F66
		// (set) Token: 0x06002B2C RID: 11052 RVA: 0x000C7D6E File Offset: 0x000C5F6E
		internal ExpressionInfo UseFontPercent
		{
			get
			{
				return this.m_useFontPercent;
			}
			set
			{
				this.m_useFontPercent = value;
			}
		}

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x000C7D77 File Offset: 0x000C5F77
		// (set) Token: 0x06002B2E RID: 11054 RVA: 0x000C7D7F File Offset: 0x000C5F7F
		internal ExpressionInfo DecimalDigits
		{
			get
			{
				return this.m_decimalDigits;
			}
			set
			{
				this.m_decimalDigits = value;
			}
		}

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x000C7D88 File Offset: 0x000C5F88
		// (set) Token: 0x06002B30 RID: 11056 RVA: 0x000C7D90 File Offset: 0x000C5F90
		internal ExpressionInfo Digits
		{
			get
			{
				return this.m_digits;
			}
			set
			{
				this.m_digits = value;
			}
		}

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x000C7D99 File Offset: 0x000C5F99
		// (set) Token: 0x06002B32 RID: 11058 RVA: 0x000C7DA1 File Offset: 0x000C5FA1
		internal GaugeInputValue MinimumValue
		{
			get
			{
				return this.m_minimumValue;
			}
			set
			{
				this.m_minimumValue = value;
			}
		}

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000C7DAA File Offset: 0x000C5FAA
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x000C7DB2 File Offset: 0x000C5FB2
		internal GaugeInputValue MaximumValue
		{
			get
			{
				return this.m_maximumValue;
			}
			set
			{
				this.m_maximumValue = value;
			}
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000C7DBB File Offset: 0x000C5FBB
		// (set) Token: 0x06002B36 RID: 11062 RVA: 0x000C7DC3 File Offset: 0x000C5FC3
		internal ExpressionInfo Multiplier
		{
			get
			{
				return this.m_multiplier;
			}
			set
			{
				this.m_multiplier = value;
			}
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000C7DCC File Offset: 0x000C5FCC
		// (set) Token: 0x06002B38 RID: 11064 RVA: 0x000C7DD4 File Offset: 0x000C5FD4
		internal ExpressionInfo NonNumericString
		{
			get
			{
				return this.m_nonNumericString;
			}
			set
			{
				this.m_nonNumericString = value;
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x000C7DDD File Offset: 0x000C5FDD
		// (set) Token: 0x06002B3A RID: 11066 RVA: 0x000C7DE5 File Offset: 0x000C5FE5
		internal ExpressionInfo OutOfRangeString
		{
			get
			{
				return this.m_outOfRangeString;
			}
			set
			{
				this.m_outOfRangeString = value;
			}
		}

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000C7DEE File Offset: 0x000C5FEE
		// (set) Token: 0x06002B3C RID: 11068 RVA: 0x000C7DF6 File Offset: 0x000C5FF6
		internal ExpressionInfo ResizeMode
		{
			get
			{
				return this.m_resizeMode;
			}
			set
			{
				this.m_resizeMode = value;
			}
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x000C7DFF File Offset: 0x000C5FFF
		// (set) Token: 0x06002B3E RID: 11070 RVA: 0x000C7E07 File Offset: 0x000C6007
		internal ExpressionInfo ShowDecimalPoint
		{
			get
			{
				return this.m_showDecimalPoint;
			}
			set
			{
				this.m_showDecimalPoint = value;
			}
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x000C7E10 File Offset: 0x000C6010
		// (set) Token: 0x06002B40 RID: 11072 RVA: 0x000C7E18 File Offset: 0x000C6018
		internal ExpressionInfo ShowLeadingZeros
		{
			get
			{
				return this.m_showLeadingZeros;
			}
			set
			{
				this.m_showLeadingZeros = value;
			}
		}

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x000C7E21 File Offset: 0x000C6021
		// (set) Token: 0x06002B42 RID: 11074 RVA: 0x000C7E29 File Offset: 0x000C6029
		internal ExpressionInfo IndicatorStyle
		{
			get
			{
				return this.m_indicatorStyle;
			}
			set
			{
				this.m_indicatorStyle = value;
			}
		}

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x000C7E32 File Offset: 0x000C6032
		// (set) Token: 0x06002B44 RID: 11076 RVA: 0x000C7E3A File Offset: 0x000C603A
		internal ExpressionInfo ShowSign
		{
			get
			{
				return this.m_showSign;
			}
			set
			{
				this.m_showSign = value;
			}
		}

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x000C7E43 File Offset: 0x000C6043
		// (set) Token: 0x06002B46 RID: 11078 RVA: 0x000C7E4B File Offset: 0x000C604B
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

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x000C7E54 File Offset: 0x000C6054
		// (set) Token: 0x06002B48 RID: 11080 RVA: 0x000C7E5C File Offset: 0x000C605C
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

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x000C7E65 File Offset: 0x000C6065
		// (set) Token: 0x06002B4A RID: 11082 RVA: 0x000C7E6D File Offset: 0x000C606D
		internal ExpressionInfo LedDimColor
		{
			get
			{
				return this.m_ledDimColor;
			}
			set
			{
				this.m_ledDimColor = value;
			}
		}

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000C7E76 File Offset: 0x000C6076
		// (set) Token: 0x06002B4C RID: 11084 RVA: 0x000C7E7E File Offset: 0x000C607E
		internal ExpressionInfo SeparatorWidth
		{
			get
			{
				return this.m_separatorWidth;
			}
			set
			{
				this.m_separatorWidth = value;
			}
		}

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x000C7E87 File Offset: 0x000C6087
		// (set) Token: 0x06002B4E RID: 11086 RVA: 0x000C7E8F File Offset: 0x000C608F
		internal ExpressionInfo SeparatorColor
		{
			get
			{
				return this.m_separatorColor;
			}
			set
			{
				this.m_separatorColor = value;
			}
		}

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x000C7E98 File Offset: 0x000C6098
		internal new NumericIndicatorExprHost ExprHost
		{
			get
			{
				return (NumericIndicatorExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000C7EA8 File Offset: 0x000C60A8
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.NumericIndicatorStart(this.m_name);
			base.Initialize(context);
			if (this.m_numericIndicatorRanges != null)
			{
				for (int i = 0; i < this.m_numericIndicatorRanges.Count; i++)
				{
					this.m_numericIndicatorRanges[i].Initialize(context);
				}
			}
			if (this.m_decimalDigitColor != null)
			{
				this.m_decimalDigitColor.Initialize("DecimalDigitColor", context);
				context.ExprHostBuilder.NumericIndicatorDecimalDigitColor(this.m_decimalDigitColor);
			}
			if (this.m_digitColor != null)
			{
				this.m_digitColor.Initialize("DigitColor", context);
				context.ExprHostBuilder.NumericIndicatorDigitColor(this.m_digitColor);
			}
			if (this.m_useFontPercent != null)
			{
				this.m_useFontPercent.Initialize("UseFontPercent", context);
				context.ExprHostBuilder.NumericIndicatorUseFontPercent(this.m_useFontPercent);
			}
			if (this.m_decimalDigits != null)
			{
				this.m_decimalDigits.Initialize("DecimalDigits", context);
				context.ExprHostBuilder.NumericIndicatorDecimalDigits(this.m_decimalDigits);
			}
			if (this.m_digits != null)
			{
				this.m_digits.Initialize("Digits", context);
				context.ExprHostBuilder.NumericIndicatorDigits(this.m_digits);
			}
			if (this.m_multiplier != null)
			{
				this.m_multiplier.Initialize("Multiplier", context);
				context.ExprHostBuilder.NumericIndicatorMultiplier(this.m_multiplier);
			}
			if (this.m_nonNumericString != null)
			{
				this.m_nonNumericString.Initialize("NonNumericString", context);
				context.ExprHostBuilder.NumericIndicatorNonNumericString(this.m_nonNumericString);
			}
			if (this.m_outOfRangeString != null)
			{
				this.m_outOfRangeString.Initialize("OutOfRangeString", context);
				context.ExprHostBuilder.NumericIndicatorOutOfRangeString(this.m_outOfRangeString);
			}
			if (this.m_resizeMode != null)
			{
				this.m_resizeMode.Initialize("ResizeMode", context);
				context.ExprHostBuilder.NumericIndicatorResizeMode(this.m_resizeMode);
			}
			if (this.m_showDecimalPoint != null)
			{
				this.m_showDecimalPoint.Initialize("ShowDecimalPoint", context);
				context.ExprHostBuilder.NumericIndicatorShowDecimalPoint(this.m_showDecimalPoint);
			}
			if (this.m_showLeadingZeros != null)
			{
				this.m_showLeadingZeros.Initialize("ShowLeadingZeros", context);
				context.ExprHostBuilder.NumericIndicatorShowLeadingZeros(this.m_showLeadingZeros);
			}
			if (this.m_indicatorStyle != null)
			{
				this.m_indicatorStyle.Initialize("IndicatorStyle", context);
				context.ExprHostBuilder.NumericIndicatorIndicatorStyle(this.m_indicatorStyle);
			}
			if (this.m_showSign != null)
			{
				this.m_showSign.Initialize("ShowSign", context);
				context.ExprHostBuilder.NumericIndicatorShowSign(this.m_showSign);
			}
			if (this.m_snappingEnabled != null)
			{
				this.m_snappingEnabled.Initialize("SnappingEnabled", context);
				context.ExprHostBuilder.NumericIndicatorSnappingEnabled(this.m_snappingEnabled);
			}
			if (this.m_snappingInterval != null)
			{
				this.m_snappingInterval.Initialize("SnappingInterval", context);
				context.ExprHostBuilder.NumericIndicatorSnappingInterval(this.m_snappingInterval);
			}
			if (this.m_ledDimColor != null)
			{
				this.m_ledDimColor.Initialize("LedDimColor", context);
				context.ExprHostBuilder.NumericIndicatorLedDimColor(this.m_ledDimColor);
			}
			if (this.m_separatorWidth != null)
			{
				this.m_separatorWidth.Initialize("SeparatorWidth", context);
				context.ExprHostBuilder.NumericIndicatorSeparatorWidth(this.m_separatorWidth);
			}
			if (this.m_separatorColor != null)
			{
				this.m_separatorColor.Initialize("SeparatorColor", context);
				context.ExprHostBuilder.NumericIndicatorSeparatorColor(this.m_separatorColor);
			}
			this.m_exprHostID = context.ExprHostBuilder.NumericIndicatorEnd();
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000C8218 File Offset: 0x000C6418
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			NumericIndicator numericIndicator = (NumericIndicator)base.PublishClone(context);
			if (this.m_gaugeInputValue != null)
			{
				numericIndicator.m_gaugeInputValue = (GaugeInputValue)this.m_gaugeInputValue.PublishClone(context);
			}
			if (this.m_numericIndicatorRanges != null)
			{
				numericIndicator.m_numericIndicatorRanges = new List<NumericIndicatorRange>(this.m_numericIndicatorRanges.Count);
				foreach (NumericIndicatorRange numericIndicatorRange in this.m_numericIndicatorRanges)
				{
					numericIndicator.m_numericIndicatorRanges.Add((NumericIndicatorRange)numericIndicatorRange.PublishClone(context));
				}
			}
			if (this.m_decimalDigitColor != null)
			{
				numericIndicator.m_decimalDigitColor = (ExpressionInfo)this.m_decimalDigitColor.PublishClone(context);
			}
			if (this.m_digitColor != null)
			{
				numericIndicator.m_digitColor = (ExpressionInfo)this.m_digitColor.PublishClone(context);
			}
			if (this.m_useFontPercent != null)
			{
				numericIndicator.m_useFontPercent = (ExpressionInfo)this.m_useFontPercent.PublishClone(context);
			}
			if (this.m_decimalDigits != null)
			{
				numericIndicator.m_decimalDigits = (ExpressionInfo)this.m_decimalDigits.PublishClone(context);
			}
			if (this.m_digits != null)
			{
				numericIndicator.m_digits = (ExpressionInfo)this.m_digits.PublishClone(context);
			}
			if (this.m_minimumValue != null)
			{
				numericIndicator.m_minimumValue = (GaugeInputValue)this.m_minimumValue.PublishClone(context);
			}
			if (this.m_maximumValue != null)
			{
				numericIndicator.m_maximumValue = (GaugeInputValue)this.m_maximumValue.PublishClone(context);
			}
			if (this.m_multiplier != null)
			{
				numericIndicator.m_multiplier = (ExpressionInfo)this.m_multiplier.PublishClone(context);
			}
			if (this.m_nonNumericString != null)
			{
				numericIndicator.m_nonNumericString = (ExpressionInfo)this.m_nonNumericString.PublishClone(context);
			}
			if (this.m_outOfRangeString != null)
			{
				numericIndicator.m_outOfRangeString = (ExpressionInfo)this.m_outOfRangeString.PublishClone(context);
			}
			if (this.m_resizeMode != null)
			{
				numericIndicator.m_resizeMode = (ExpressionInfo)this.m_resizeMode.PublishClone(context);
			}
			if (this.m_showDecimalPoint != null)
			{
				numericIndicator.m_showDecimalPoint = (ExpressionInfo)this.m_showDecimalPoint.PublishClone(context);
			}
			if (this.m_showLeadingZeros != null)
			{
				numericIndicator.m_showLeadingZeros = (ExpressionInfo)this.m_showLeadingZeros.PublishClone(context);
			}
			if (this.m_indicatorStyle != null)
			{
				numericIndicator.m_indicatorStyle = (ExpressionInfo)this.m_indicatorStyle.PublishClone(context);
			}
			if (this.m_showSign != null)
			{
				numericIndicator.m_showSign = (ExpressionInfo)this.m_showSign.PublishClone(context);
			}
			if (this.m_snappingEnabled != null)
			{
				numericIndicator.m_snappingEnabled = (ExpressionInfo)this.m_snappingEnabled.PublishClone(context);
			}
			if (this.m_snappingInterval != null)
			{
				numericIndicator.m_snappingInterval = (ExpressionInfo)this.m_snappingInterval.PublishClone(context);
			}
			if (this.m_ledDimColor != null)
			{
				numericIndicator.m_ledDimColor = (ExpressionInfo)this.m_ledDimColor.PublishClone(context);
			}
			if (this.m_separatorWidth != null)
			{
				numericIndicator.m_separatorWidth = (ExpressionInfo)this.m_separatorWidth.PublishClone(context);
			}
			if (this.m_separatorColor != null)
			{
				numericIndicator.m_separatorColor = (ExpressionInfo)this.m_separatorColor.PublishClone(context);
			}
			return numericIndicator;
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000C8534 File Offset: 0x000C6734
		internal void SetExprHost(NumericIndicatorExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_gaugeInputValue != null && this.ExprHost.GaugeInputValueHost != null)
			{
				this.m_gaugeInputValue.SetExprHost(this.ExprHost.GaugeInputValueHost, reportObjectModel);
			}
			IList<NumericIndicatorRangeExprHost> numericIndicatorRangesHostsRemotable = this.ExprHost.NumericIndicatorRangesHostsRemotable;
			if (this.m_numericIndicatorRanges != null && numericIndicatorRangesHostsRemotable != null)
			{
				for (int i = 0; i < this.m_numericIndicatorRanges.Count; i++)
				{
					NumericIndicatorRange numericIndicatorRange = this.m_numericIndicatorRanges[i];
					if (numericIndicatorRange != null && numericIndicatorRange.ExpressionHostID > -1)
					{
						numericIndicatorRange.SetExprHost(numericIndicatorRangesHostsRemotable[numericIndicatorRange.ExpressionHostID], reportObjectModel);
					}
				}
			}
			if (this.m_minimumValue != null && this.ExprHost.MinimumValueHost != null)
			{
				this.m_minimumValue.SetExprHost(this.ExprHost.MinimumValueHost, reportObjectModel);
			}
			if (this.m_maximumValue != null && this.ExprHost.MaximumValueHost != null)
			{
				this.m_maximumValue.SetExprHost(this.ExprHost.MaximumValueHost, reportObjectModel);
			}
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000C8640 File Offset: 0x000C6840
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.NumericIndicator, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelItem, list);
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000C8664 File Offset: 0x000C6864
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(NumericIndicator.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Multiplier)
				{
					if (memberName == MemberName.SeparatorColor)
					{
						writer.Write(this.m_separatorColor);
						continue;
					}
					if (memberName == MemberName.GaugeInputValue)
					{
						writer.Write(this.m_gaugeInputValue);
						continue;
					}
					switch (memberName)
					{
					case MemberName.SnappingEnabled:
						writer.Write(this.m_snappingEnabled);
						continue;
					case MemberName.SnappingInterval:
						writer.Write(this.m_snappingInterval);
						continue;
					case MemberName.MaximumValue:
						writer.Write(this.m_maximumValue);
						continue;
					case MemberName.MinimumValue:
						writer.Write(this.m_minimumValue);
						continue;
					case MemberName.Multiplier:
						writer.Write(this.m_multiplier);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.UseFontPercent)
					{
						writer.Write(this.m_useFontPercent);
						continue;
					}
					if (memberName == MemberName.ResizeMode)
					{
						writer.Write(this.m_resizeMode);
						continue;
					}
					switch (memberName)
					{
					case MemberName.NumericIndicatorRanges:
						writer.Write<NumericIndicatorRange>(this.m_numericIndicatorRanges);
						continue;
					case MemberName.DecimalDigitColor:
						writer.Write(this.m_decimalDigitColor);
						continue;
					case MemberName.DigitColor:
						writer.Write(this.m_digitColor);
						continue;
					case MemberName.DecimalDigits:
						writer.Write(this.m_decimalDigits);
						continue;
					case MemberName.Digits:
						writer.Write(this.m_digits);
						continue;
					case MemberName.NonNumericString:
						writer.Write(this.m_nonNumericString);
						continue;
					case MemberName.OutOfRangeString:
						writer.Write(this.m_outOfRangeString);
						continue;
					case MemberName.ShowDecimalPoint:
						writer.Write(this.m_showDecimalPoint);
						continue;
					case MemberName.ShowLeadingZeros:
						writer.Write(this.m_showLeadingZeros);
						continue;
					case MemberName.IndicatorStyle:
						writer.Write(this.m_indicatorStyle);
						continue;
					case MemberName.ShowSign:
						writer.Write(this.m_showSign);
						continue;
					case MemberName.LedDimColor:
						writer.Write(this.m_ledDimColor);
						continue;
					case MemberName.SeparatorWidth:
						writer.Write(this.m_separatorWidth);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000C88D0 File Offset: 0x000C6AD0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(NumericIndicator.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Multiplier)
				{
					if (memberName == MemberName.SeparatorColor)
					{
						this.m_separatorColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.GaugeInputValue)
					{
						this.m_gaugeInputValue = (GaugeInputValue)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.SnappingEnabled:
						this.m_snappingEnabled = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.SnappingInterval:
						this.m_snappingInterval = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.MaximumValue:
						this.m_maximumValue = (GaugeInputValue)reader.ReadRIFObject();
						continue;
					case MemberName.MinimumValue:
						this.m_minimumValue = (GaugeInputValue)reader.ReadRIFObject();
						continue;
					case MemberName.Multiplier:
						this.m_multiplier = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.UseFontPercent)
					{
						this.m_useFontPercent = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ResizeMode)
					{
						this.m_resizeMode = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.NumericIndicatorRanges:
						this.m_numericIndicatorRanges = reader.ReadGenericListOfRIFObjects<NumericIndicatorRange>();
						continue;
					case MemberName.DecimalDigitColor:
						this.m_decimalDigitColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DigitColor:
						this.m_digitColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DecimalDigits:
						this.m_decimalDigits = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.Digits:
						this.m_digits = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.NonNumericString:
						this.m_nonNumericString = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.OutOfRangeString:
						this.m_outOfRangeString = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ShowDecimalPoint:
						this.m_showDecimalPoint = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ShowLeadingZeros:
						this.m_showLeadingZeros = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.IndicatorStyle:
						this.m_indicatorStyle = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ShowSign:
						this.m_showSign = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.LedDimColor:
						this.m_ledDimColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.SeparatorWidth:
						this.m_separatorWidth = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000C8BAA File Offset: 0x000C6DAA
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.NumericIndicator;
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000C8BB1 File Offset: 0x000C6DB1
		internal string EvaluateDecimalDigitColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorDecimalDigitColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000C8BD7 File Offset: 0x000C6DD7
		internal string EvaluateDigitColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorDigitColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000C8BFD File Offset: 0x000C6DFD
		internal bool EvaluateUseFontPercent(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorUseFontPercentExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000C8C23 File Offset: 0x000C6E23
		internal int EvaluateDecimalDigits(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorDecimalDigitsExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000C8C49 File Offset: 0x000C6E49
		internal int EvaluateDigits(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorDigitsExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000C8C6F File Offset: 0x000C6E6F
		internal double EvaluateMultiplier(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorMultiplierExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000C8C95 File Offset: 0x000C6E95
		internal string EvaluateNonNumericString(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorNonNumericStringExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000C8CBB File Offset: 0x000C6EBB
		internal string EvaluateOutOfRangeString(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorOutOfRangeStringExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000C8CE1 File Offset: 0x000C6EE1
		internal GaugeResizeModes EvaluateResizeMode(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeResizeModes(context.ReportRuntime.EvaluateNumericIndicatorResizeModeExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x000C8D12 File Offset: 0x000C6F12
		internal bool EvaluateShowDecimalPoint(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorShowDecimalPointExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x000C8D38 File Offset: 0x000C6F38
		internal bool EvaluateShowLeadingZeros(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorShowLeadingZerosExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000C8D5E File Offset: 0x000C6F5E
		internal GaugeIndicatorStyles EvaluateIndicatorStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeIndicatorStyles(context.ReportRuntime.EvaluateNumericIndicatorIndicatorStyleExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000C8D8F File Offset: 0x000C6F8F
		internal GaugeShowSigns EvaluateShowSign(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeShowSigns(context.ReportRuntime.EvaluateNumericIndicatorShowSignExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000C8DC0 File Offset: 0x000C6FC0
		internal bool EvaluateSnappingEnabled(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorSnappingEnabledExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000C8DE6 File Offset: 0x000C6FE6
		internal double EvaluateSnappingInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorSnappingIntervalExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x000C8E0C File Offset: 0x000C700C
		internal string EvaluateLedDimColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorLedDimColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000C8E32 File Offset: 0x000C7032
		internal double EvaluateSeparatorWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorSeparatorWidthExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000C8E58 File Offset: 0x000C7058
		internal string EvaluateSeparatorColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateNumericIndicatorSeparatorColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x04001782 RID: 6018
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = NumericIndicator.GetDeclaration();

		// Token: 0x04001783 RID: 6019
		private GaugeInputValue m_gaugeInputValue;

		// Token: 0x04001784 RID: 6020
		private List<NumericIndicatorRange> m_numericIndicatorRanges;

		// Token: 0x04001785 RID: 6021
		private ExpressionInfo m_decimalDigitColor;

		// Token: 0x04001786 RID: 6022
		private ExpressionInfo m_digitColor;

		// Token: 0x04001787 RID: 6023
		private ExpressionInfo m_useFontPercent;

		// Token: 0x04001788 RID: 6024
		private ExpressionInfo m_decimalDigits;

		// Token: 0x04001789 RID: 6025
		private ExpressionInfo m_digits;

		// Token: 0x0400178A RID: 6026
		private GaugeInputValue m_minimumValue;

		// Token: 0x0400178B RID: 6027
		private GaugeInputValue m_maximumValue;

		// Token: 0x0400178C RID: 6028
		private ExpressionInfo m_multiplier;

		// Token: 0x0400178D RID: 6029
		private ExpressionInfo m_nonNumericString;

		// Token: 0x0400178E RID: 6030
		private ExpressionInfo m_outOfRangeString;

		// Token: 0x0400178F RID: 6031
		private ExpressionInfo m_resizeMode;

		// Token: 0x04001790 RID: 6032
		private ExpressionInfo m_showDecimalPoint;

		// Token: 0x04001791 RID: 6033
		private ExpressionInfo m_showLeadingZeros;

		// Token: 0x04001792 RID: 6034
		private ExpressionInfo m_indicatorStyle;

		// Token: 0x04001793 RID: 6035
		private ExpressionInfo m_showSign;

		// Token: 0x04001794 RID: 6036
		private ExpressionInfo m_snappingEnabled;

		// Token: 0x04001795 RID: 6037
		private ExpressionInfo m_snappingInterval;

		// Token: 0x04001796 RID: 6038
		private ExpressionInfo m_ledDimColor;

		// Token: 0x04001797 RID: 6039
		private ExpressionInfo m_separatorWidth;

		// Token: 0x04001798 RID: 6040
		private ExpressionInfo m_separatorColor;
	}
}
