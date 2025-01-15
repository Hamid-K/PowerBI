using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000431 RID: 1073
	[Serializable]
	internal class MapSpatialData : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002FBF RID: 12223 RVA: 0x000D7F5B File Offset: 0x000D615B
		internal MapSpatialData()
		{
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000D7F63 File Offset: 0x000D6163
		internal MapSpatialData(MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_map = map;
			this.m_mapVectorLayer = mapVectorLayer;
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000D7F79 File Offset: 0x000D6179
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x000D7F86 File Offset: 0x000D6186
		internal MapSpatialDataExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000D7F8E File Offset: 0x000D618E
		internal virtual void Initialize(InitializationContext context)
		{
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000D7F90 File Offset: 0x000D6190
		internal virtual void InitializeMapMember(InitializationContext context)
		{
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000D7F92 File Offset: 0x000D6192
		internal virtual object PublishClone(AutomaticSubtotalContext context)
		{
			MapSpatialData mapSpatialData = (MapSpatialData)base.MemberwiseClone();
			mapSpatialData.m_map = context.CurrentMapClone;
			mapSpatialData.m_mapVectorLayer = context.CurrentMapVectorLayerClone;
			return mapSpatialData;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000D7FB9 File Offset: 0x000D61B9
		internal virtual void SetExprHost(MapSpatialDataExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000D7FBB File Offset: 0x000D61BB
		internal virtual void SetExprHostMapMember(MapSpatialDataExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000D7FBD File Offset: 0x000D61BD
		protected void SetExprHostInternal(MapSpatialDataExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000D7FEC File Offset: 0x000D61EC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialData, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference),
				new MemberInfo(MemberName.MapVectorLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapVectorLayer, Token.Reference)
			});
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000D8038 File Offset: 0x000D6238
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapSpatialData.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Map)
				{
					if (memberName != MemberName.MapVectorLayer)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.WriteReference(this.m_mapVectorLayer);
					}
				}
				else
				{
					writer.WriteReference(this.m_map);
				}
			}
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000D80A4 File Offset: 0x000D62A4
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapSpatialData.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Map)
				{
					if (memberName != MemberName.MapVectorLayer)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_mapVectorLayer = reader.ReadReference<MapVectorLayer>(this);
					}
				}
				else
				{
					this.m_map = reader.ReadReference<Map>(this);
				}
			}
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000D8114 File Offset: 0x000D6314
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapSpatialData.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.Map)
					{
						if (memberName != MemberName.MapVectorLayer)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							this.m_mapVectorLayer = (MapVectorLayer)referenceableItems[memberReference.RefID];
						}
					}
					else
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_map = (Map)referenceableItems[memberReference.RefID];
					}
				}
			}
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000D81FC File Offset: 0x000D63FC
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialData;
		}

		// Token: 0x040018CB RID: 6347
		[NonSerialized]
		protected MapSpatialDataExprHost m_exprHost;

		// Token: 0x040018CC RID: 6348
		[Reference]
		protected Map m_map;

		// Token: 0x040018CD RID: 6349
		[Reference]
		protected MapVectorLayer m_mapVectorLayer;

		// Token: 0x040018CE RID: 6350
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapSpatialData.GetDeclaration();
	}
}
