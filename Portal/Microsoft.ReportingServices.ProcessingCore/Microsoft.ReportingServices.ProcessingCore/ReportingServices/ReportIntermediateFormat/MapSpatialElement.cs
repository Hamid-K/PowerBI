using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200044E RID: 1102
	[Serializable]
	internal class MapSpatialElement : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003212 RID: 12818 RVA: 0x000DFDD2 File Offset: 0x000DDFD2
		internal MapSpatialElement()
		{
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000DFDE1 File Offset: 0x000DDFE1
		internal MapSpatialElement(MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_map = map;
			this.m_mapVectorLayer = mapVectorLayer;
		}

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x000DFDFE File Offset: 0x000DDFFE
		// (set) Token: 0x06003215 RID: 12821 RVA: 0x000DFE06 File Offset: 0x000DE006
		internal string VectorData
		{
			get
			{
				return this.m_vectorData;
			}
			set
			{
				this.m_vectorData = value;
			}
		}

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x000DFE0F File Offset: 0x000DE00F
		// (set) Token: 0x06003217 RID: 12823 RVA: 0x000DFE17 File Offset: 0x000DE017
		internal List<MapField> MapFields
		{
			get
			{
				return this.m_mapFields;
			}
			set
			{
				this.m_mapFields = value;
			}
		}

		// Token: 0x170016EE RID: 5870
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x000DFE20 File Offset: 0x000DE020
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000DFE2D File Offset: 0x000DE02D
		internal MapSpatialElementExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x000DFE35 File Offset: 0x000DE035
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x170016F1 RID: 5873
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x000DFE3D File Offset: 0x000DE03D
		protected IInstancePath InstancePath
		{
			get
			{
				return this.m_mapVectorLayer.InstancePath;
			}
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000DFE4A File Offset: 0x000DE04A
		internal virtual void Initialize(InitializationContext context, int index)
		{
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000DFE4C File Offset: 0x000DE04C
		internal virtual object PublishClone(AutomaticSubtotalContext context)
		{
			MapSpatialElement mapSpatialElement = (MapSpatialElement)base.MemberwiseClone();
			mapSpatialElement.m_map = context.CurrentMapClone;
			mapSpatialElement.m_mapVectorLayer = context.CurrentMapVectorLayerClone;
			if (this.m_mapFields != null)
			{
				mapSpatialElement.m_mapFields = new List<MapField>(this.m_mapFields.Count);
				foreach (MapField mapField in this.m_mapFields)
				{
					mapSpatialElement.m_mapFields.Add((MapField)mapField.PublishClone(context));
				}
			}
			return mapSpatialElement;
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000DFEF4 File Offset: 0x000DE0F4
		internal void SetExprHost(MapSpatialElementExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000DFF24 File Offset: 0x000DE124
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialElement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.VectorData, Token.String),
				new MemberInfo(MemberName.MapFields, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapField),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference),
				new MemberInfo(MemberName.MapVectorLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapVectorLayer, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000DFFB0 File Offset: 0x000DE1B0
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapSpatialElement.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Map)
				{
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
					if (memberName == MemberName.Map)
					{
						writer.WriteReference(this.m_map);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.VectorData)
					{
						writer.Write(this.m_vectorData);
						continue;
					}
					if (memberName == MemberName.MapFields)
					{
						writer.Write<MapField>(this.m_mapFields);
						continue;
					}
					if (memberName == MemberName.MapVectorLayer)
					{
						writer.WriteReference(this.m_mapVectorLayer);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000E0074 File Offset: 0x000DE274
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapSpatialElement.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Map)
				{
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.Map)
					{
						this.m_map = reader.ReadReference<Map>(this);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.VectorData)
					{
						this.m_vectorData = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.MapFields)
					{
						this.m_mapFields = reader.ReadGenericListOfRIFObjects<MapField>();
						continue;
					}
					if (memberName == MemberName.MapVectorLayer)
					{
						this.m_mapVectorLayer = reader.ReadReference<MapVectorLayer>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000E0138 File Offset: 0x000DE338
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapSpatialElement.m_Declaration.ObjectType, out list))
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

		// Token: 0x06003223 RID: 12835 RVA: 0x000E0220 File Offset: 0x000DE420
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialElement;
		}

		// Token: 0x04001961 RID: 6497
		protected int m_exprHostID = -1;

		// Token: 0x04001962 RID: 6498
		[NonSerialized]
		protected MapSpatialElementExprHost m_exprHost;

		// Token: 0x04001963 RID: 6499
		[Reference]
		protected Map m_map;

		// Token: 0x04001964 RID: 6500
		[Reference]
		protected MapVectorLayer m_mapVectorLayer;

		// Token: 0x04001965 RID: 6501
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapSpatialElement.GetDeclaration();

		// Token: 0x04001966 RID: 6502
		private string m_vectorData;

		// Token: 0x04001967 RID: 6503
		private List<MapField> m_mapFields;
	}
}
