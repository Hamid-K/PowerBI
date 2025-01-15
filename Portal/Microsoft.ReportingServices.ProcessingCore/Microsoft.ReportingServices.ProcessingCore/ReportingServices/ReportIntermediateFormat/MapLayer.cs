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
	// Token: 0x0200042C RID: 1068
	[Serializable]
	internal class MapLayer : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002F56 RID: 12118 RVA: 0x000D647D File Offset: 0x000D467D
		internal MapLayer()
		{
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000D648C File Offset: 0x000D468C
		internal MapLayer(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x000D64A2 File Offset: 0x000D46A2
		// (set) Token: 0x06002F59 RID: 12121 RVA: 0x000D64AA File Offset: 0x000D46AA
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

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x000D64B3 File Offset: 0x000D46B3
		// (set) Token: 0x06002F5B RID: 12123 RVA: 0x000D64BB File Offset: 0x000D46BB
		internal ExpressionInfo VisibilityMode
		{
			get
			{
				return this.m_visibilityMode;
			}
			set
			{
				this.m_visibilityMode = value;
			}
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x000D64C4 File Offset: 0x000D46C4
		// (set) Token: 0x06002F5D RID: 12125 RVA: 0x000D64CC File Offset: 0x000D46CC
		internal ExpressionInfo MinimumZoom
		{
			get
			{
				return this.m_minimumZoom;
			}
			set
			{
				this.m_minimumZoom = value;
			}
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x000D64D5 File Offset: 0x000D46D5
		// (set) Token: 0x06002F5F RID: 12127 RVA: 0x000D64DD File Offset: 0x000D46DD
		internal ExpressionInfo MaximumZoom
		{
			get
			{
				return this.m_maximumZoom;
			}
			set
			{
				this.m_maximumZoom = value;
			}
		}

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x000D64E6 File Offset: 0x000D46E6
		// (set) Token: 0x06002F61 RID: 12129 RVA: 0x000D64EE File Offset: 0x000D46EE
		internal ExpressionInfo Transparency
		{
			get
			{
				return this.m_transparency;
			}
			set
			{
				this.m_transparency = value;
			}
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06002F62 RID: 12130 RVA: 0x000D64F7 File Offset: 0x000D46F7
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06002F63 RID: 12131 RVA: 0x000D6504 File Offset: 0x000D4704
		internal MapLayerExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x000D650C File Offset: 0x000D470C
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000D6514 File Offset: 0x000D4714
		internal virtual void Initialize(InitializationContext context)
		{
			if (this.m_visibilityMode != null)
			{
				this.m_visibilityMode.Initialize("VisibilityMode", context);
				context.ExprHostBuilder.MapLayerVisibilityMode(this.m_visibilityMode);
			}
			if (this.m_minimumZoom != null)
			{
				this.m_minimumZoom.Initialize("MinimumZoom", context);
				context.ExprHostBuilder.MapLayerMinimumZoom(this.m_minimumZoom);
			}
			if (this.m_maximumZoom != null)
			{
				this.m_maximumZoom.Initialize("MaximumZoom", context);
				context.ExprHostBuilder.MapLayerMaximumZoom(this.m_maximumZoom);
			}
			if (this.m_transparency != null)
			{
				this.m_transparency.Initialize("Transparency", context);
				context.ExprHostBuilder.MapLayerTransparency(this.m_transparency);
			}
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000D65D0 File Offset: 0x000D47D0
		internal virtual object PublishClone(AutomaticSubtotalContext context)
		{
			MapLayer mapLayer = (MapLayer)base.MemberwiseClone();
			mapLayer.m_map = context.CurrentMapClone;
			if (this.m_visibilityMode != null)
			{
				mapLayer.m_visibilityMode = (ExpressionInfo)this.m_visibilityMode.PublishClone(context);
			}
			if (this.m_minimumZoom != null)
			{
				mapLayer.m_minimumZoom = (ExpressionInfo)this.m_minimumZoom.PublishClone(context);
			}
			if (this.m_maximumZoom != null)
			{
				mapLayer.m_maximumZoom = (ExpressionInfo)this.m_maximumZoom.PublishClone(context);
			}
			if (this.m_transparency != null)
			{
				mapLayer.m_transparency = (ExpressionInfo)this.m_transparency.PublishClone(context);
			}
			return mapLayer;
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000D6673 File Offset: 0x000D4873
		internal virtual void SetExprHost(MapLayerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000D66A4 File Offset: 0x000D48A4
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.VisibilityMode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MinimumZoom, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MaximumZoom, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Transparency, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000D6754 File Offset: 0x000D4954
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapLayer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Transparency)
				{
					if (memberName == MemberName.Name)
					{
						writer.Write(this.m_name);
						continue;
					}
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
					if (memberName == MemberName.Transparency)
					{
						writer.Write(this.m_transparency);
						continue;
					}
				}
				else if (memberName <= MemberName.MaximumZoom)
				{
					if (memberName == MemberName.Map)
					{
						writer.WriteReference(this.m_map);
						continue;
					}
					if (memberName == MemberName.MaximumZoom)
					{
						writer.Write(this.m_maximumZoom);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.MinimumZoom)
					{
						writer.Write(this.m_minimumZoom);
						continue;
					}
					if (memberName == MemberName.VisibilityMode)
					{
						writer.Write(this.m_visibilityMode);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x000D6850 File Offset: 0x000D4A50
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapLayer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Transparency)
				{
					if (memberName == MemberName.Name)
					{
						this.m_name = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.Transparency)
					{
						this.m_transparency = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else if (memberName <= MemberName.MaximumZoom)
				{
					if (memberName == MemberName.Map)
					{
						this.m_map = reader.ReadReference<Map>(this);
						continue;
					}
					if (memberName == MemberName.MaximumZoom)
					{
						this.m_maximumZoom = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.MinimumZoom)
					{
						this.m_minimumZoom = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.VisibilityMode)
					{
						this.m_visibilityMode = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x000D6968 File Offset: 0x000D4B68
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapLayer.m_Declaration.ObjectType, out list))
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

		// Token: 0x06002F6C RID: 12140 RVA: 0x000D6A0C File Offset: 0x000D4C0C
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLayer;
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x000D6A13 File Offset: 0x000D4C13
		internal MapVisibilityMode EvaluateVisibilityMode(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapVisibilityMode(context.ReportRuntime.EvaluateMapLayerVisibilityModeExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x000D6A44 File Offset: 0x000D4C44
		internal double EvaluateMinimumZoom(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLayerMinimumZoomExpression(this, this.m_map.Name);
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x000D6A6A File Offset: 0x000D4C6A
		internal double EvaluateMaximumZoom(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLayerMaximumZoomExpression(this, this.m_map.Name);
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x000D6A90 File Offset: 0x000D4C90
		internal double EvaluateTransparency(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLayerTransparencyExpression(this, this.m_map.Name);
		}

		// Token: 0x040018B2 RID: 6322
		protected int m_exprHostID = -1;

		// Token: 0x040018B3 RID: 6323
		[NonSerialized]
		protected MapLayerExprHost m_exprHost;

		// Token: 0x040018B4 RID: 6324
		[Reference]
		protected Map m_map;

		// Token: 0x040018B5 RID: 6325
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapLayer.GetDeclaration();

		// Token: 0x040018B6 RID: 6326
		protected string m_name;

		// Token: 0x040018B7 RID: 6327
		private ExpressionInfo m_visibilityMode;

		// Token: 0x040018B8 RID: 6328
		private ExpressionInfo m_minimumZoom;

		// Token: 0x040018B9 RID: 6329
		private ExpressionInfo m_maximumZoom;

		// Token: 0x040018BA RID: 6330
		private ExpressionInfo m_transparency;
	}
}
