using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200042E RID: 1070
	[Serializable]
	internal sealed class MapShapefile : MapSpatialData, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002F87 RID: 12167 RVA: 0x000D70B3 File Offset: 0x000D52B3
		internal MapShapefile()
		{
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000D70BB File Offset: 0x000D52BB
		internal MapShapefile(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06002F89 RID: 12169 RVA: 0x000D70C5 File Offset: 0x000D52C5
		// (set) Token: 0x06002F8A RID: 12170 RVA: 0x000D70CD File Offset: 0x000D52CD
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

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x06002F8B RID: 12171 RVA: 0x000D70D6 File Offset: 0x000D52D6
		// (set) Token: 0x06002F8C RID: 12172 RVA: 0x000D70DE File Offset: 0x000D52DE
		internal List<MapFieldName> MapFieldNames
		{
			get
			{
				return this.m_mapFieldNames;
			}
			set
			{
				this.m_mapFieldNames = value;
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x000D70E7 File Offset: 0x000D52E7
		internal new MapShapefileExprHost ExprHost
		{
			get
			{
				return (MapShapefileExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000D70F4 File Offset: 0x000D52F4
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapShapefileStart();
			base.Initialize(context);
			if (this.m_source != null)
			{
				this.m_source.Initialize("Source", context);
				context.ExprHostBuilder.MapShapefileSource(this.m_source);
			}
			if (this.m_mapFieldNames != null)
			{
				for (int i = 0; i < this.m_mapFieldNames.Count; i++)
				{
					this.m_mapFieldNames[i].Initialize(context, i);
				}
			}
			context.ExprHostBuilder.MapShapefileEnd();
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000D717C File Offset: 0x000D537C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapShapefile mapShapefile = (MapShapefile)base.PublishClone(context);
			if (this.m_source != null)
			{
				mapShapefile.m_source = (ExpressionInfo)this.m_source.PublishClone(context);
			}
			if (this.m_mapFieldNames != null)
			{
				mapShapefile.m_mapFieldNames = new List<MapFieldName>(this.m_mapFieldNames.Count);
				foreach (MapFieldName mapFieldName in this.m_mapFieldNames)
				{
					mapShapefile.m_mapFieldNames.Add((MapFieldName)mapFieldName.PublishClone(context));
				}
			}
			return mapShapefile;
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x000D722C File Offset: 0x000D542C
		internal override void SetExprHost(MapSpatialDataExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHostInternal(exprHost, reportObjectModel);
			IList<MapFieldNameExprHost> mapFieldNamesHostsRemotable = this.ExprHost.MapFieldNamesHostsRemotable;
			if (this.m_mapFieldNames != null && mapFieldNamesHostsRemotable != null)
			{
				for (int i = 0; i < this.m_mapFieldNames.Count; i++)
				{
					MapFieldName mapFieldName = this.m_mapFieldNames[i];
					if (mapFieldName != null && mapFieldName.ExpressionHostID > -1)
					{
						mapFieldName.SetExprHost(mapFieldNamesHostsRemotable[mapFieldName.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000D72B4 File Offset: 0x000D54B4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapShapefile, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialData, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Source, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapFieldNames, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapFieldName)
			});
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000D7304 File Offset: 0x000D5504
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapShapefile.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Source)
				{
					if (memberName != MemberName.MapFieldNames)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write<MapFieldName>(this.m_mapFieldNames);
					}
				}
				else
				{
					writer.Write(this.m_source);
				}
			}
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000D7378 File Offset: 0x000D5578
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapShapefile.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Source)
				{
					if (memberName != MemberName.MapFieldNames)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_mapFieldNames = reader.ReadGenericListOfRIFObjects<MapFieldName>();
					}
				}
				else
				{
					this.m_source = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x000D73F0 File Offset: 0x000D55F0
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapShapefile;
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000D73F7 File Offset: 0x000D55F7
		internal string EvaluateSource(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapShapefileSourceExpression(this, this.m_map.Name);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000D7420 File Offset: 0x000D5620
		internal string GetFileStreamName(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, string url)
		{
			string text = null;
			OnDemandMetadata odpMetadata = renderingContext.OdpContext.OdpMetadata;
			ShapefileInfo shapefileInfo;
			if (odpMetadata.TryGetShapefile(url, out shapefileInfo))
			{
				if (shapefileInfo.ErrorOccurred)
				{
					return null;
				}
				text = shapefileInfo.StreamName;
			}
			else
			{
				byte[] array;
				if (!this.GetFileData(renderingContext, url, out array) || array == null)
				{
					shapefileInfo = new ShapefileInfo(null);
					shapefileInfo.ErrorOccurred = true;
				}
				else
				{
					text = this.StoreShapefileInChunk(renderingContext, array);
					shapefileInfo = new ShapefileInfo(text);
				}
				odpMetadata.AddShapefile(url, shapefileInfo);
			}
			return text;
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x000D7494 File Offset: 0x000D5694
		private bool GetFileData(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, string url, out byte[] data)
		{
			data = null;
			string text = null;
			bool flag2;
			try
			{
				bool flag;
				if (!renderingContext.OdpContext.TopLevelContext.ReportContext.IsSupportedProtocol(url, true, out flag))
				{
					renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsUnsupportedProtocol, Severity.Error, this.m_map.ObjectType, this.m_map.Name, "Source", new string[] { url, "http://, https://, ftp://, file:, mailto:, or news:" });
					flag2 = false;
				}
				else
				{
					bool flag3;
					renderingContext.OdpContext.GetResource(url, out data, out text, out flag3);
					flag2 = data != null;
				}
			}
			catch (Exception ex)
			{
				renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsMapInvalidShapefileReference, Severity.Warning, this.m_map.ObjectType, this.m_map.Name, url, new string[] { ex.Message });
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000D7578 File Offset: 0x000D5778
		private string StoreShapefileInChunk(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, byte[] data)
		{
			string text = Guid.NewGuid().ToString("N");
			using (Stream stream = renderingContext.OdpContext.ChunkFactory.CreateChunk(text, ReportProcessing.ReportChunkTypes.Shapefile, null))
			{
				stream.Write(data, 0, data.Length);
			}
			return text;
		}

		// Token: 0x040018BF RID: 6335
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapShapefile.GetDeclaration();

		// Token: 0x040018C0 RID: 6336
		private ExpressionInfo m_source;

		// Token: 0x040018C1 RID: 6337
		private List<MapFieldName> m_mapFieldNames;
	}
}
