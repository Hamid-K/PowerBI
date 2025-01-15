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
	// Token: 0x02000456 RID: 1110
	[Serializable]
	internal class MapView : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060032C1 RID: 12993 RVA: 0x000E2299 File Offset: 0x000E0499
		internal MapView()
		{
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000E22A1 File Offset: 0x000E04A1
		internal MapView(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000E22B0 File Offset: 0x000E04B0
		// (set) Token: 0x060032C4 RID: 12996 RVA: 0x000E22B8 File Offset: 0x000E04B8
		internal ExpressionInfo Zoom
		{
			get
			{
				return this.m_zoom;
			}
			set
			{
				this.m_zoom = value;
			}
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x060032C5 RID: 12997 RVA: 0x000E22C1 File Offset: 0x000E04C1
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000E22CE File Offset: 0x000E04CE
		internal MapViewExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000E22D6 File Offset: 0x000E04D6
		internal virtual void Initialize(InitializationContext context)
		{
			if (this.m_zoom != null)
			{
				this.m_zoom.Initialize("Zoom", context);
				context.ExprHostBuilder.MapViewZoom(this.m_zoom);
			}
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000E2304 File Offset: 0x000E0504
		internal virtual object PublishClone(AutomaticSubtotalContext context)
		{
			MapView mapView = (MapView)base.MemberwiseClone();
			mapView.m_map = context.CurrentMapClone;
			if (this.m_zoom != null)
			{
				mapView.m_zoom = (ExpressionInfo)this.m_zoom.PublishClone(context);
			}
			return mapView;
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000E234A File Offset: 0x000E054A
		internal virtual void SetExprHost(MapViewExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000E2378 File Offset: 0x000E0578
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapView, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Zoom, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference)
			});
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000E23C4 File Offset: 0x000E05C4
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapView.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Map)
				{
					if (memberName != MemberName.Zoom)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_zoom);
					}
				}
				else
				{
					writer.WriteReference(this.m_map);
				}
			}
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000E2430 File Offset: 0x000E0630
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapView.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Map)
				{
					if (memberName != MemberName.Zoom)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_zoom = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_map = reader.ReadReference<Map>(this);
				}
			}
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000E24A4 File Offset: 0x000E06A4
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapView.m_Declaration.ObjectType, out list))
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

		// Token: 0x060032CE RID: 13006 RVA: 0x000E2548 File Offset: 0x000E0748
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapView;
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000E254F File Offset: 0x000E074F
		internal double EvaluateZoom(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewZoomExpression(this, this.m_map.Name);
		}

		// Token: 0x0400198E RID: 6542
		[NonSerialized]
		protected MapViewExprHost m_exprHost;

		// Token: 0x0400198F RID: 6543
		[Reference]
		protected Map m_map;

		// Token: 0x04001990 RID: 6544
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapView.GetDeclaration();

		// Token: 0x04001991 RID: 6545
		private ExpressionInfo m_zoom;
	}
}
