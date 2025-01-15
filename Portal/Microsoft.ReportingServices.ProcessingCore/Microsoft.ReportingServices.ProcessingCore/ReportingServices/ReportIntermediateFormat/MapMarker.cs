using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000441 RID: 1089
	[Serializable]
	internal sealed class MapMarker : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003115 RID: 12565 RVA: 0x000DC997 File Offset: 0x000DAB97
		internal MapMarker()
		{
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000DC9A6 File Offset: 0x000DABA6
		internal MapMarker(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x000DC9BC File Offset: 0x000DABBC
		// (set) Token: 0x06003118 RID: 12568 RVA: 0x000DC9C4 File Offset: 0x000DABC4
		internal ExpressionInfo MapMarkerStyle
		{
			get
			{
				return this.m_mapMarkerStyle;
			}
			set
			{
				this.m_mapMarkerStyle = value;
			}
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x000DC9CD File Offset: 0x000DABCD
		// (set) Token: 0x0600311A RID: 12570 RVA: 0x000DC9D5 File Offset: 0x000DABD5
		internal MapMarkerImage MapMarkerImage
		{
			get
			{
				return this.m_mapMarkerImage;
			}
			set
			{
				this.m_mapMarkerImage = value;
			}
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x000DC9DE File Offset: 0x000DABDE
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x000DC9EB File Offset: 0x000DABEB
		internal MapMarkerExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x000DC9F3 File Offset: 0x000DABF3
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000DC9FB File Offset: 0x000DABFB
		internal void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.MapMarkerInCollectionStart(index.ToString(CultureInfo.InvariantCulture.NumberFormat));
			this.InnerInitialize(context);
			this.m_exprHostID = context.ExprHostBuilder.MapMarkerInCollectionEnd();
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000DCA33 File Offset: 0x000DAC33
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapMarkerStart();
			this.InnerInitialize(context);
			context.ExprHostBuilder.MapMarkerEnd();
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000DCA54 File Offset: 0x000DAC54
		private void InnerInitialize(InitializationContext context)
		{
			if (this.m_mapMarkerStyle != null)
			{
				this.m_mapMarkerStyle.Initialize("MapMarkerStyle", context);
				context.ExprHostBuilder.MapMarkerMapMarkerStyle(this.m_mapMarkerStyle);
			}
			if (this.m_mapMarkerImage != null)
			{
				this.m_mapMarkerImage.Initialize(context);
			}
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000DCAA0 File Offset: 0x000DACA0
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapMarker mapMarker = (MapMarker)base.MemberwiseClone();
			mapMarker.m_map = context.CurrentMapClone;
			if (this.m_mapMarkerStyle != null)
			{
				mapMarker.m_mapMarkerStyle = (ExpressionInfo)this.m_mapMarkerStyle.PublishClone(context);
			}
			if (this.m_mapMarkerImage != null)
			{
				mapMarker.m_mapMarkerImage = (MapMarkerImage)this.m_mapMarkerImage.PublishClone(context);
			}
			return mapMarker;
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x000DCB08 File Offset: 0x000DAD08
		internal void SetExprHost(MapMarkerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_mapMarkerImage != null && this.ExprHost.MapMarkerImageHost != null)
			{
				this.m_mapMarkerImage.SetExprHost(this.ExprHost.MapMarkerImageHost, reportObjectModel);
			}
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000DCB70 File Offset: 0x000DAD70
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarker, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapMarkerStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapMarkerlImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarkerImage),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000DCBE4 File Offset: 0x000DADE4
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapMarker.m_Declaration);
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
					if (memberName == MemberName.MapMarkerStyle)
					{
						writer.Write(this.m_mapMarkerStyle);
						continue;
					}
					if (memberName == MemberName.MapMarkerlImage)
					{
						writer.Write(this.m_mapMarkerImage);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000DCC90 File Offset: 0x000DAE90
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapMarker.m_Declaration);
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
					if (memberName == MemberName.MapMarkerStyle)
					{
						this.m_mapMarkerStyle = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.MapMarkerlImage)
					{
						this.m_mapMarkerImage = (MapMarkerImage)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000DCD48 File Offset: 0x000DAF48
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapMarker.m_Declaration.ObjectType, out list))
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

		// Token: 0x06003127 RID: 12583 RVA: 0x000DCDEC File Offset: 0x000DAFEC
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarker;
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000DCDF3 File Offset: 0x000DAFF3
		internal MapMarkerStyle EvaluateMapMarkerStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapMarkerStyle(context.ReportRuntime.EvaluateMapMarkerMapMarkerStyleExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x04001923 RID: 6435
		private int m_exprHostID = -1;

		// Token: 0x04001924 RID: 6436
		[NonSerialized]
		private MapMarkerExprHost m_exprHost;

		// Token: 0x04001925 RID: 6437
		[Reference]
		private Map m_map;

		// Token: 0x04001926 RID: 6438
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapMarker.GetDeclaration();

		// Token: 0x04001927 RID: 6439
		private ExpressionInfo m_mapMarkerStyle;

		// Token: 0x04001928 RID: 6440
		private MapMarkerImage m_mapMarkerImage;
	}
}
