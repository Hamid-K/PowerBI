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
	// Token: 0x02000422 RID: 1058
	[Serializable]
	internal class MapSubItem : MapStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002E63 RID: 11875 RVA: 0x000D32E1 File Offset: 0x000D14E1
		internal MapSubItem()
		{
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000D32F0 File Offset: 0x000D14F0
		internal MapSubItem(Map map)
			: base(map)
		{
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x000D3300 File Offset: 0x000D1500
		// (set) Token: 0x06002E66 RID: 11878 RVA: 0x000D3308 File Offset: 0x000D1508
		internal MapLocation MapLocation
		{
			get
			{
				return this.m_mapLocation;
			}
			set
			{
				this.m_mapLocation = value;
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000D3311 File Offset: 0x000D1511
		// (set) Token: 0x06002E68 RID: 11880 RVA: 0x000D3319 File Offset: 0x000D1519
		internal MapSize MapSize
		{
			get
			{
				return this.m_mapSize;
			}
			set
			{
				this.m_mapSize = value;
			}
		}

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x000D3322 File Offset: 0x000D1522
		// (set) Token: 0x06002E6A RID: 11882 RVA: 0x000D332A File Offset: 0x000D152A
		internal ExpressionInfo LeftMargin
		{
			get
			{
				return this.m_leftMargin;
			}
			set
			{
				this.m_leftMargin = value;
			}
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x000D3333 File Offset: 0x000D1533
		// (set) Token: 0x06002E6C RID: 11884 RVA: 0x000D333B File Offset: 0x000D153B
		internal ExpressionInfo RightMargin
		{
			get
			{
				return this.m_rightMargin;
			}
			set
			{
				this.m_rightMargin = value;
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x000D3344 File Offset: 0x000D1544
		// (set) Token: 0x06002E6E RID: 11886 RVA: 0x000D334C File Offset: 0x000D154C
		internal ExpressionInfo TopMargin
		{
			get
			{
				return this.m_topMargin;
			}
			set
			{
				this.m_topMargin = value;
			}
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x000D3355 File Offset: 0x000D1555
		// (set) Token: 0x06002E70 RID: 11888 RVA: 0x000D335D File Offset: 0x000D155D
		internal ExpressionInfo BottomMargin
		{
			get
			{
				return this.m_bottomMargin;
			}
			set
			{
				this.m_bottomMargin = value;
			}
		}

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x000D3366 File Offset: 0x000D1566
		// (set) Token: 0x06002E72 RID: 11890 RVA: 0x000D336E File Offset: 0x000D156E
		internal ExpressionInfo ZIndex
		{
			get
			{
				return this.m_zIndex;
			}
			set
			{
				this.m_zIndex = value;
			}
		}

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000D3377 File Offset: 0x000D1577
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x000D3384 File Offset: 0x000D1584
		internal MapSubItemExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x000D338C File Offset: 0x000D158C
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000D3394 File Offset: 0x000D1594
		internal override void Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_mapLocation != null)
			{
				this.m_mapLocation.Initialize(context);
			}
			if (this.m_mapSize != null)
			{
				this.m_mapSize.Initialize(context);
			}
			if (this.m_leftMargin != null)
			{
				this.m_leftMargin.Initialize("LeftMargin", context);
				context.ExprHostBuilder.MapSubItemLeftMargin(this.m_leftMargin);
			}
			if (this.m_rightMargin != null)
			{
				this.m_rightMargin.Initialize("RightMargin", context);
				context.ExprHostBuilder.MapSubItemRightMargin(this.m_rightMargin);
			}
			if (this.m_topMargin != null)
			{
				this.m_topMargin.Initialize("TopMargin", context);
				context.ExprHostBuilder.MapSubItemTopMargin(this.m_topMargin);
			}
			if (this.m_bottomMargin != null)
			{
				this.m_bottomMargin.Initialize("BottomMargin", context);
				context.ExprHostBuilder.MapSubItemBottomMargin(this.m_bottomMargin);
			}
			if (this.m_zIndex != null)
			{
				this.m_zIndex.Initialize("ZIndex", context);
				context.ExprHostBuilder.MapSubItemZIndex(this.m_zIndex);
			}
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000D34A8 File Offset: 0x000D16A8
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapSubItem mapSubItem = (MapSubItem)base.PublishClone(context);
			if (this.m_mapLocation != null)
			{
				mapSubItem.m_mapLocation = (MapLocation)this.m_mapLocation.PublishClone(context);
			}
			if (this.m_mapSize != null)
			{
				mapSubItem.m_mapSize = (MapSize)this.m_mapSize.PublishClone(context);
			}
			if (this.m_leftMargin != null)
			{
				mapSubItem.m_leftMargin = (ExpressionInfo)this.m_leftMargin.PublishClone(context);
			}
			if (this.m_rightMargin != null)
			{
				mapSubItem.m_rightMargin = (ExpressionInfo)this.m_rightMargin.PublishClone(context);
			}
			if (this.m_topMargin != null)
			{
				mapSubItem.m_topMargin = (ExpressionInfo)this.m_topMargin.PublishClone(context);
			}
			if (this.m_bottomMargin != null)
			{
				mapSubItem.m_bottomMargin = (ExpressionInfo)this.m_bottomMargin.PublishClone(context);
			}
			if (this.m_zIndex != null)
			{
				mapSubItem.m_zIndex = (ExpressionInfo)this.m_zIndex.PublishClone(context);
			}
			return mapSubItem;
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000D359C File Offset: 0x000D179C
		internal void SetExprHost(MapSubItemExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapLocation != null && this.ExprHost.MapLocationHost != null)
			{
				this.m_mapLocation.SetExprHost(this.ExprHost.MapLocationHost, reportObjectModel);
			}
			if (this.m_mapSize != null && this.ExprHost.MapSizeHost != null)
			{
				this.m_mapSize.SetExprHost(this.ExprHost.MapSizeHost, reportObjectModel);
			}
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000D362C File Offset: 0x000D182C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSubItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapLocation, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLocation),
				new MemberInfo(MemberName.MapSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSize),
				new MemberInfo(MemberName.LeftMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.RightMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TopMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.BottomMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ZIndex, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000D36F8 File Offset: 0x000D18F8
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapSubItem.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.BottomMargin)
				{
					if (memberName == MemberName.ZIndex)
					{
						writer.Write(this.m_zIndex);
						continue;
					}
					switch (memberName)
					{
					case MemberName.LeftMargin:
						writer.Write(this.m_leftMargin);
						continue;
					case MemberName.RightMargin:
						writer.Write(this.m_rightMargin);
						continue;
					case MemberName.TopMargin:
						writer.Write(this.m_topMargin);
						continue;
					case MemberName.BottomMargin:
						writer.Write(this.m_bottomMargin);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
					if (memberName == MemberName.MapLocation)
					{
						writer.Write(this.m_mapLocation);
						continue;
					}
					if (memberName == MemberName.MapSize)
					{
						writer.Write(this.m_mapSize);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000D3814 File Offset: 0x000D1A14
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapSubItem.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.BottomMargin)
				{
					if (memberName == MemberName.ZIndex)
					{
						this.m_zIndex = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.LeftMargin:
						this.m_leftMargin = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.RightMargin:
						this.m_rightMargin = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.TopMargin:
						this.m_topMargin = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.BottomMargin:
						this.m_bottomMargin = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.MapLocation)
					{
						this.m_mapLocation = (MapLocation)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.MapSize)
					{
						this.m_mapSize = (MapSize)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000D395B File Offset: 0x000D1B5B
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSubItem;
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000D3962 File Offset: 0x000D1B62
		internal string EvaluateLeftMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSubItemLeftMarginExpression(this, this.m_map.Name);
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000D3988 File Offset: 0x000D1B88
		internal string EvaluateRightMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSubItemRightMarginExpression(this, this.m_map.Name);
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000D39AE File Offset: 0x000D1BAE
		internal string EvaluateTopMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSubItemTopMarginExpression(this, this.m_map.Name);
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000D39D4 File Offset: 0x000D1BD4
		internal string EvaluateBottomMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSubItemBottomMarginExpression(this, this.m_map.Name);
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000D39FA File Offset: 0x000D1BFA
		internal int EvaluateZIndex(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSubItemZIndexExpression(this, this.m_map.Name);
		}

		// Token: 0x04001872 RID: 6258
		protected int m_exprHostID = -1;

		// Token: 0x04001873 RID: 6259
		[NonSerialized]
		protected MapSubItemExprHost m_exprHost;

		// Token: 0x04001874 RID: 6260
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapSubItem.GetDeclaration();

		// Token: 0x04001875 RID: 6261
		private MapLocation m_mapLocation;

		// Token: 0x04001876 RID: 6262
		private MapSize m_mapSize;

		// Token: 0x04001877 RID: 6263
		private ExpressionInfo m_leftMargin;

		// Token: 0x04001878 RID: 6264
		private ExpressionInfo m_rightMargin;

		// Token: 0x04001879 RID: 6265
		private ExpressionInfo m_topMargin;

		// Token: 0x0400187A RID: 6266
		private ExpressionInfo m_bottomMargin;

		// Token: 0x0400187B RID: 6267
		private ExpressionInfo m_zIndex;
	}
}
