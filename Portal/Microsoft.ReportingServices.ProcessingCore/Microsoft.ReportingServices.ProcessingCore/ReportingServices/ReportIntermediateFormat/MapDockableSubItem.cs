using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000423 RID: 1059
	[Serializable]
	internal class MapDockableSubItem : MapSubItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IActionOwner
	{
		// Token: 0x06002E83 RID: 11907 RVA: 0x000D3A2C File Offset: 0x000D1C2C
		internal MapDockableSubItem()
		{
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000D3A34 File Offset: 0x000D1C34
		internal MapDockableSubItem(Map map, int id)
			: base(map)
		{
			this.m_id = id;
		}

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x000D3A44 File Offset: 0x000D1C44
		// (set) Token: 0x06002E86 RID: 11910 RVA: 0x000D3A4C File Offset: 0x000D1C4C
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x000D3A55 File Offset: 0x000D1C55
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x000D3A5D File Offset: 0x000D1C5D
		// (set) Token: 0x06002E89 RID: 11913 RVA: 0x000D3A65 File Offset: 0x000D1C65
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return this.m_fieldsUsedInValueExpression;
			}
			set
			{
				this.m_fieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000D3A6E File Offset: 0x000D1C6E
		internal int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000D3A76 File Offset: 0x000D1C76
		// (set) Token: 0x06002E8C RID: 11916 RVA: 0x000D3A7E File Offset: 0x000D1C7E
		internal ExpressionInfo Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x000D3A87 File Offset: 0x000D1C87
		// (set) Token: 0x06002E8E RID: 11918 RVA: 0x000D3A8F File Offset: 0x000D1C8F
		internal ExpressionInfo DockOutsideViewport
		{
			get
			{
				return this.m_dockOutsideViewport;
			}
			set
			{
				this.m_dockOutsideViewport = value;
			}
		}

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x000D3A98 File Offset: 0x000D1C98
		// (set) Token: 0x06002E90 RID: 11920 RVA: 0x000D3AA0 File Offset: 0x000D1CA0
		internal ExpressionInfo Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x000D3AA9 File Offset: 0x000D1CA9
		// (set) Token: 0x06002E92 RID: 11922 RVA: 0x000D3AB1 File Offset: 0x000D1CB1
		internal ExpressionInfo ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				this.m_toolTip = value;
			}
		}

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x000D3ABA File Offset: 0x000D1CBA
		internal new MapDockableSubItemExprHost ExprHost
		{
			get
			{
				return (MapDockableSubItemExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000D3AC8 File Offset: 0x000D1CC8
		internal override void Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_position != null)
			{
				this.m_position.Initialize("Position", context);
				context.ExprHostBuilder.MapDockableSubItemPosition(this.m_position);
			}
			if (this.m_dockOutsideViewport != null)
			{
				this.m_dockOutsideViewport.Initialize("DockOutsideViewport", context);
				context.ExprHostBuilder.MapDockableSubItemDockOutsideViewport(this.m_dockOutsideViewport);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.MapDockableSubItemHidden(this.m_hidden);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.MapDockableSubItemToolTip(this.m_toolTip);
			}
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000D3B9C File Offset: 0x000D1D9C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapDockableSubItem mapDockableSubItem = (MapDockableSubItem)base.PublishClone(context);
			if (this.m_action != null)
			{
				mapDockableSubItem.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			if (this.m_position != null)
			{
				mapDockableSubItem.m_position = (ExpressionInfo)this.m_position.PublishClone(context);
			}
			if (this.m_dockOutsideViewport != null)
			{
				mapDockableSubItem.m_dockOutsideViewport = (ExpressionInfo)this.m_dockOutsideViewport.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				mapDockableSubItem.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				mapDockableSubItem.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			return mapDockableSubItem;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000D3C54 File Offset: 0x000D1E54
		internal void SetExprHost(MapDockableSubItemExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_action != null && this.ExprHost.ActionInfoHost != null)
			{
				this.m_action.SetExprHost(this.ExprHost.ActionInfoHost, reportObjectModel);
			}
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000D3CB0 File Offset: 0x000D1EB0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDockableSubItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSubItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.ID, Token.Int32),
				new MemberInfo(MemberName.Position, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DockOutsideViewport, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000D3D50 File Offset: 0x000D1F50
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapDockableSubItem.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ToolTip)
				{
					if (memberName == MemberName.ID)
					{
						writer.Write(this.m_id);
						continue;
					}
					if (memberName == MemberName.Position)
					{
						writer.Write(this.m_position);
						continue;
					}
					if (memberName == MemberName.ToolTip)
					{
						writer.Write(this.m_toolTip);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Hidden)
					{
						writer.Write(this.m_hidden);
						continue;
					}
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
					if (memberName == MemberName.DockOutsideViewport)
					{
						writer.Write(this.m_dockOutsideViewport);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000D3E2C File Offset: 0x000D202C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapDockableSubItem.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ToolTip)
				{
					if (memberName == MemberName.ID)
					{
						this.m_id = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.Position)
					{
						this.m_position = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ToolTip)
					{
						this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Hidden)
					{
						this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.DockOutsideViewport)
					{
						this.m_dockOutsideViewport = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000D3F22 File Offset: 0x000D2122
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDockableSubItem;
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000D3F29 File Offset: 0x000D2129
		internal MapPosition EvaluatePosition(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapPosition(context.ReportRuntime.EvaluateMapDockableSubItemPositionExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000D3F5A File Offset: 0x000D215A
		internal bool EvaluateDockOutsideViewport(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapDockableSubItemDockOutsideViewportExpression(this, this.m_map.Name);
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000D3F80 File Offset: 0x000D2180
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapDockableSubItemHiddenExpression(this, this.m_map.Name);
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000D3FA8 File Offset: 0x000D21A8
		internal string EvaluateToolTip(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateMapDockableSubItemToolTipExpression(this, this.m_map.Name);
			return this.m_map.GetFormattedStringFromValue(ref variantResult, context);
		}

		// Token: 0x0400187C RID: 6268
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapDockableSubItem.GetDeclaration();

		// Token: 0x0400187D RID: 6269
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x0400187E RID: 6270
		private int m_id;

		// Token: 0x0400187F RID: 6271
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;

		// Token: 0x04001880 RID: 6272
		private ExpressionInfo m_position;

		// Token: 0x04001881 RID: 6273
		private ExpressionInfo m_dockOutsideViewport;

		// Token: 0x04001882 RID: 6274
		private ExpressionInfo m_hidden;

		// Token: 0x04001883 RID: 6275
		private ExpressionInfo m_toolTip;
	}
}
