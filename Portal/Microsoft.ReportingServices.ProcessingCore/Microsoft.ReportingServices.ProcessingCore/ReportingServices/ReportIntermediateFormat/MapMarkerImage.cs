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
	// Token: 0x02000440 RID: 1088
	[Serializable]
	internal sealed class MapMarkerImage : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060030F8 RID: 12536 RVA: 0x000DC314 File Offset: 0x000DA514
		internal MapMarkerImage()
		{
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000DC31C File Offset: 0x000DA51C
		internal MapMarkerImage(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x000DC32B File Offset: 0x000DA52B
		// (set) Token: 0x060030FB RID: 12539 RVA: 0x000DC333 File Offset: 0x000DA533
		internal ExpressionInfo Source
		{
			get
			{
				return this.m_source;
			}
			set
			{
				this.m_source = value;
			}
		}

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x000DC33C File Offset: 0x000DA53C
		// (set) Token: 0x060030FD RID: 12541 RVA: 0x000DC344 File Offset: 0x000DA544
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000DC34D File Offset: 0x000DA54D
		// (set) Token: 0x060030FF RID: 12543 RVA: 0x000DC355 File Offset: 0x000DA555
		internal ExpressionInfo MIMEType
		{
			get
			{
				return this.m_mIMEType;
			}
			set
			{
				this.m_mIMEType = value;
			}
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000DC35E File Offset: 0x000DA55E
		// (set) Token: 0x06003101 RID: 12545 RVA: 0x000DC366 File Offset: 0x000DA566
		internal ExpressionInfo TransparentColor
		{
			get
			{
				return this.m_transparentColor;
			}
			set
			{
				this.m_transparentColor = value;
			}
		}

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000DC36F File Offset: 0x000DA56F
		// (set) Token: 0x06003103 RID: 12547 RVA: 0x000DC377 File Offset: 0x000DA577
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

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000DC380 File Offset: 0x000DA580
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x000DC38D File Offset: 0x000DA58D
		internal MapMarkerImageExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000DC398 File Offset: 0x000DA598
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapMarkerImageStart();
			if (this.m_source != null)
			{
				this.m_source.Initialize("Source", context);
				context.ExprHostBuilder.MapMarkerImageSource(this.m_source);
			}
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
				context.ExprHostBuilder.MapMarkerImageValue(this.m_value);
			}
			if (this.m_mIMEType != null)
			{
				this.m_mIMEType.Initialize("MIMEType", context);
				context.ExprHostBuilder.MapMarkerImageMIMEType(this.m_mIMEType);
			}
			if (this.m_transparentColor != null)
			{
				this.m_transparentColor.Initialize("TransparentColor", context);
				context.ExprHostBuilder.MapMarkerImageTransparentColor(this.m_transparentColor);
			}
			if (this.m_resizeMode != null)
			{
				this.m_resizeMode.Initialize("ResizeMode", context);
				context.ExprHostBuilder.MapMarkerImageResizeMode(this.m_resizeMode);
			}
			context.ExprHostBuilder.MapMarkerImageEnd();
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000DC494 File Offset: 0x000DA694
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapMarkerImage mapMarkerImage = (MapMarkerImage)base.MemberwiseClone();
			mapMarkerImage.m_map = context.CurrentMapClone;
			if (this.m_source != null)
			{
				mapMarkerImage.m_source = (ExpressionInfo)this.m_source.PublishClone(context);
			}
			if (this.m_value != null)
			{
				mapMarkerImage.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			if (this.m_mIMEType != null)
			{
				mapMarkerImage.m_mIMEType = (ExpressionInfo)this.m_mIMEType.PublishClone(context);
			}
			if (this.m_transparentColor != null)
			{
				mapMarkerImage.m_transparentColor = (ExpressionInfo)this.m_transparentColor.PublishClone(context);
			}
			if (this.m_resizeMode != null)
			{
				mapMarkerImage.m_resizeMode = (ExpressionInfo)this.m_resizeMode.PublishClone(context);
			}
			return mapMarkerImage;
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000DC556 File Offset: 0x000DA756
		internal void SetExprHost(MapMarkerImageExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000DC584 File Offset: 0x000DA784
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarkerImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Source, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MIMEType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TransparentColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ResizeMode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference)
			});
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000DC620 File Offset: 0x000DA820
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapMarkerImage.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.MIMEType)
				{
					if (memberName == MemberName.Value)
					{
						writer.Write(this.m_value);
						continue;
					}
					if (memberName == MemberName.Source)
					{
						writer.Write(this.m_source);
						continue;
					}
					if (memberName == MemberName.MIMEType)
					{
						writer.Write(this.m_mIMEType);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.TransparentColor)
					{
						writer.Write(this.m_transparentColor);
						continue;
					}
					if (memberName == MemberName.ResizeMode)
					{
						writer.Write(this.m_resizeMode);
						continue;
					}
					if (memberName == MemberName.Map)
					{
						writer.WriteReference(this.m_map);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000DC6F4 File Offset: 0x000DA8F4
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapMarkerImage.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.MIMEType)
				{
					if (memberName == MemberName.Value)
					{
						this.m_value = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Source)
					{
						this.m_source = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.MIMEType)
					{
						this.m_mIMEType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.TransparentColor)
					{
						this.m_transparentColor = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ResizeMode)
					{
						this.m_resizeMode = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Map)
					{
						this.m_map = reader.ReadReference<Map>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000DC7E4 File Offset: 0x000DA9E4
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapMarkerImage.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.Map)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_map = (Map)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000DC888 File Offset: 0x000DAA88
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarkerImage;
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000DC88F File Offset: 0x000DAA8F
		internal Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType EvaluateSource(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateImageSourceType(context.ReportRuntime.EvaluateMapMarkerImageSourceExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000DC8C0 File Offset: 0x000DAAC0
		internal string EvaluateStringValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context, out bool errorOccurred)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapMarkerImageStringValueExpression(this, this.m_map.Name, out errorOccurred);
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000DC8E7 File Offset: 0x000DAAE7
		internal byte[] EvaluateBinaryValue(IReportScopeInstance romInstance, OnDemandProcessingContext context, out bool errOccurred)
		{
			context.SetupContext(this.m_map, romInstance);
			return context.ReportRuntime.EvaluateMapMarkerImageBinaryValueExpression(this, this.m_map.Name, out errOccurred);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000DC90E File Offset: 0x000DAB0E
		internal string EvaluateMIMEType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapMarkerImageMIMETypeExpression(this, this.m_map.Name);
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000DC934 File Offset: 0x000DAB34
		internal string EvaluateTransparentColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapMarkerImageTransparentColorExpression(this, this.m_map.Name);
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000DC95A File Offset: 0x000DAB5A
		internal MapResizeMode EvaluateResizeMode(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapResizeMode(context.ReportRuntime.EvaluateMapMarkerImageResizeModeExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x0400191B RID: 6427
		[NonSerialized]
		private MapMarkerImageExprHost m_exprHost;

		// Token: 0x0400191C RID: 6428
		[Reference]
		private Map m_map;

		// Token: 0x0400191D RID: 6429
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapMarkerImage.GetDeclaration();

		// Token: 0x0400191E RID: 6430
		private ExpressionInfo m_source;

		// Token: 0x0400191F RID: 6431
		private ExpressionInfo m_value;

		// Token: 0x04001920 RID: 6432
		private ExpressionInfo m_mIMEType;

		// Token: 0x04001921 RID: 6433
		private ExpressionInfo m_transparentColor;

		// Token: 0x04001922 RID: 6434
		private ExpressionInfo m_resizeMode;
	}
}
