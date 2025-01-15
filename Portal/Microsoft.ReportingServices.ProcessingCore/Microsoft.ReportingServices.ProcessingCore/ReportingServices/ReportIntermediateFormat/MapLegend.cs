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
	// Token: 0x02000428 RID: 1064
	[Serializable]
	internal sealed class MapLegend : MapDockableSubItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002EFE RID: 12030 RVA: 0x000D5288 File Offset: 0x000D3488
		internal MapLegend()
		{
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000D5290 File Offset: 0x000D3490
		internal MapLegend(Map map, int id)
			: base(map, id)
		{
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06002F00 RID: 12032 RVA: 0x000D529A File Offset: 0x000D349A
		// (set) Token: 0x06002F01 RID: 12033 RVA: 0x000D52A2 File Offset: 0x000D34A2
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

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x06002F02 RID: 12034 RVA: 0x000D52AB File Offset: 0x000D34AB
		// (set) Token: 0x06002F03 RID: 12035 RVA: 0x000D52B3 File Offset: 0x000D34B3
		internal ExpressionInfo Layout
		{
			get
			{
				return this.m_layout;
			}
			set
			{
				this.m_layout = value;
			}
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000D52BC File Offset: 0x000D34BC
		// (set) Token: 0x06002F05 RID: 12037 RVA: 0x000D52C4 File Offset: 0x000D34C4
		internal MapLegendTitle MapLegendTitle
		{
			get
			{
				return this.m_mapLegendTitle;
			}
			set
			{
				this.m_mapLegendTitle = value;
			}
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x000D52CD File Offset: 0x000D34CD
		// (set) Token: 0x06002F07 RID: 12039 RVA: 0x000D52D5 File Offset: 0x000D34D5
		internal ExpressionInfo AutoFitTextDisabled
		{
			get
			{
				return this.m_autoFitTextDisabled;
			}
			set
			{
				this.m_autoFitTextDisabled = value;
			}
		}

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000D52DE File Offset: 0x000D34DE
		// (set) Token: 0x06002F09 RID: 12041 RVA: 0x000D52E6 File Offset: 0x000D34E6
		internal ExpressionInfo MinFontSize
		{
			get
			{
				return this.m_minFontSize;
			}
			set
			{
				this.m_minFontSize = value;
			}
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x000D52EF File Offset: 0x000D34EF
		// (set) Token: 0x06002F0B RID: 12043 RVA: 0x000D52F7 File Offset: 0x000D34F7
		internal ExpressionInfo InterlacedRows
		{
			get
			{
				return this.m_interlacedRows;
			}
			set
			{
				this.m_interlacedRows = value;
			}
		}

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x000D5300 File Offset: 0x000D3500
		// (set) Token: 0x06002F0D RID: 12045 RVA: 0x000D5308 File Offset: 0x000D3508
		internal ExpressionInfo InterlacedRowsColor
		{
			get
			{
				return this.m_interlacedRowsColor;
			}
			set
			{
				this.m_interlacedRowsColor = value;
			}
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x000D5311 File Offset: 0x000D3511
		// (set) Token: 0x06002F0F RID: 12047 RVA: 0x000D5319 File Offset: 0x000D3519
		internal ExpressionInfo EquallySpacedItems
		{
			get
			{
				return this.m_equallySpacedItems;
			}
			set
			{
				this.m_equallySpacedItems = value;
			}
		}

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x000D5322 File Offset: 0x000D3522
		// (set) Token: 0x06002F11 RID: 12049 RVA: 0x000D532A File Offset: 0x000D352A
		internal ExpressionInfo TextWrapThreshold
		{
			get
			{
				return this.m_textWrapThreshold;
			}
			set
			{
				this.m_textWrapThreshold = value;
			}
		}

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x000D5333 File Offset: 0x000D3533
		internal new MapLegendExprHost ExprHost
		{
			get
			{
				return (MapLegendExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000D5340 File Offset: 0x000D3540
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapLegendStart(this.m_name);
			base.Initialize(context);
			if (this.m_layout != null)
			{
				this.m_layout.Initialize("Layout", context);
				context.ExprHostBuilder.MapLegendLayout(this.m_layout);
			}
			if (this.m_mapLegendTitle != null)
			{
				this.m_mapLegendTitle.Initialize(context);
			}
			if (this.m_autoFitTextDisabled != null)
			{
				this.m_autoFitTextDisabled.Initialize("AutoFitTextDisabled", context);
				context.ExprHostBuilder.MapLegendAutoFitTextDisabled(this.m_autoFitTextDisabled);
			}
			if (this.m_minFontSize != null)
			{
				this.m_minFontSize.Initialize("MinFontSize", context);
				context.ExprHostBuilder.MapLegendMinFontSize(this.m_minFontSize);
			}
			if (this.m_interlacedRows != null)
			{
				this.m_interlacedRows.Initialize("InterlacedRows", context);
				context.ExprHostBuilder.MapLegendInterlacedRows(this.m_interlacedRows);
			}
			if (this.m_interlacedRowsColor != null)
			{
				this.m_interlacedRowsColor.Initialize("InterlacedRowsColor", context);
				context.ExprHostBuilder.MapLegendInterlacedRowsColor(this.m_interlacedRowsColor);
			}
			if (this.m_equallySpacedItems != null)
			{
				this.m_equallySpacedItems.Initialize("EquallySpacedItems", context);
				context.ExprHostBuilder.MapLegendEquallySpacedItems(this.m_equallySpacedItems);
			}
			if (this.m_textWrapThreshold != null)
			{
				this.m_textWrapThreshold.Initialize("TextWrapThreshold", context);
				context.ExprHostBuilder.MapLegendTextWrapThreshold(this.m_textWrapThreshold);
			}
			this.m_exprHostID = context.ExprHostBuilder.MapLegendEnd();
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000D54BC File Offset: 0x000D36BC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapLegend mapLegend = (MapLegend)base.PublishClone(context);
			if (this.m_layout != null)
			{
				mapLegend.m_layout = (ExpressionInfo)this.m_layout.PublishClone(context);
			}
			if (this.m_mapLegendTitle != null)
			{
				mapLegend.m_mapLegendTitle = (MapLegendTitle)this.m_mapLegendTitle.PublishClone(context);
			}
			if (this.m_autoFitTextDisabled != null)
			{
				mapLegend.m_autoFitTextDisabled = (ExpressionInfo)this.m_autoFitTextDisabled.PublishClone(context);
			}
			if (this.m_minFontSize != null)
			{
				mapLegend.m_minFontSize = (ExpressionInfo)this.m_minFontSize.PublishClone(context);
			}
			if (this.m_interlacedRows != null)
			{
				mapLegend.m_interlacedRows = (ExpressionInfo)this.m_interlacedRows.PublishClone(context);
			}
			if (this.m_interlacedRowsColor != null)
			{
				mapLegend.m_interlacedRowsColor = (ExpressionInfo)this.m_interlacedRowsColor.PublishClone(context);
			}
			if (this.m_equallySpacedItems != null)
			{
				mapLegend.m_equallySpacedItems = (ExpressionInfo)this.m_equallySpacedItems.PublishClone(context);
			}
			if (this.m_textWrapThreshold != null)
			{
				mapLegend.m_textWrapThreshold = (ExpressionInfo)this.m_textWrapThreshold.PublishClone(context);
			}
			return mapLegend;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000D55D0 File Offset: 0x000D37D0
		internal void SetExprHost(MapLegendExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapLegendTitle != null && this.ExprHost.MapLegendTitleHost != null)
			{
				this.m_mapLegendTitle.SetExprHost(this.ExprHost.MapLegendTitleHost, reportObjectModel);
			}
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000D562C File Offset: 0x000D382C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLegend, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDockableSubItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Layout, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapLegendTitle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLegendTitle),
				new MemberInfo(MemberName.AutoFitTextDisabled, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MinFontSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.InterlacedRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.InterlacedRowsColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.EquallySpacedItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TextWrapThreshold, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000D570C File Offset: 0x000D390C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapLegend.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Layout)
				{
					if (memberName == MemberName.Name)
					{
						writer.Write(this.m_name);
						continue;
					}
					if (memberName == MemberName.MinFontSize)
					{
						writer.Write(this.m_minFontSize);
						continue;
					}
					if (memberName == MemberName.Layout)
					{
						writer.Write(this.m_layout);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.AutoFitTextDisabled)
					{
						writer.Write(this.m_autoFitTextDisabled);
						continue;
					}
					switch (memberName)
					{
					case MemberName.InterlacedRows:
						writer.Write(this.m_interlacedRows);
						continue;
					case MemberName.InterlacedRowsColor:
						writer.Write(this.m_interlacedRowsColor);
						continue;
					case MemberName.EquallySpacedItems:
						writer.Write(this.m_equallySpacedItems);
						continue;
					case MemberName.Reversed:
					case MemberName.MaxAutoSize:
						break;
					case MemberName.TextWrapThreshold:
						writer.Write(this.m_textWrapThreshold);
						continue;
					default:
						if (memberName == MemberName.MapLegendTitle)
						{
							writer.Write(this.m_mapLegendTitle);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000D5840 File Offset: 0x000D3A40
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapLegend.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Layout)
				{
					if (memberName == MemberName.Name)
					{
						this.m_name = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.MinFontSize)
					{
						this.m_minFontSize = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Layout)
					{
						this.m_layout = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.AutoFitTextDisabled)
					{
						this.m_autoFitTextDisabled = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.InterlacedRows:
						this.m_interlacedRows = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.InterlacedRowsColor:
						this.m_interlacedRowsColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.EquallySpacedItems:
						this.m_equallySpacedItems = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.Reversed:
					case MemberName.MaxAutoSize:
						break;
					case MemberName.TextWrapThreshold:
						this.m_textWrapThreshold = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.MapLegendTitle)
						{
							this.m_mapLegendTitle = (MapLegendTitle)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000D599F File Offset: 0x000D3B9F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLegend;
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000D59A6 File Offset: 0x000D3BA6
		internal MapLegendLayout EvaluateLayout(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapLegendLayout(context.ReportRuntime.EvaluateMapLegendLayoutExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000D59D7 File Offset: 0x000D3BD7
		internal bool EvaluateAutoFitTextDisabled(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLegendAutoFitTextDisabledExpression(this, this.m_map.Name);
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000D59FD File Offset: 0x000D3BFD
		internal string EvaluateMinFontSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLegendMinFontSizeExpression(this, this.m_map.Name);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000D5A23 File Offset: 0x000D3C23
		internal bool EvaluateInterlacedRows(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLegendInterlacedRowsExpression(this, this.m_map.Name);
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000D5A49 File Offset: 0x000D3C49
		internal string EvaluateInterlacedRowsColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLegendInterlacedRowsColorExpression(this, this.m_map.Name);
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000D5A6F File Offset: 0x000D3C6F
		internal bool EvaluateEquallySpacedItems(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLegendEquallySpacedItemsExpression(this, this.m_map.Name);
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000D5A95 File Offset: 0x000D3C95
		internal int EvaluateTextWrapThreshold(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLegendTextWrapThresholdExpression(this, this.m_map.Name);
		}

		// Token: 0x0400189A RID: 6298
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapLegend.GetDeclaration();

		// Token: 0x0400189B RID: 6299
		private ExpressionInfo m_layout;

		// Token: 0x0400189C RID: 6300
		private MapLegendTitle m_mapLegendTitle;

		// Token: 0x0400189D RID: 6301
		private ExpressionInfo m_autoFitTextDisabled;

		// Token: 0x0400189E RID: 6302
		private ExpressionInfo m_minFontSize;

		// Token: 0x0400189F RID: 6303
		private ExpressionInfo m_interlacedRows;

		// Token: 0x040018A0 RID: 6304
		private ExpressionInfo m_interlacedRowsColor;

		// Token: 0x040018A1 RID: 6305
		private ExpressionInfo m_equallySpacedItems;

		// Token: 0x040018A2 RID: 6306
		private ExpressionInfo m_textWrapThreshold;

		// Token: 0x040018A3 RID: 6307
		private string m_name;
	}
}
