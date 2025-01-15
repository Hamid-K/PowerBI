using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000443 RID: 1091
	[Serializable]
	internal sealed class MapPointRules : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003138 RID: 12600 RVA: 0x000DD0F7 File Offset: 0x000DB2F7
		internal MapPointRules()
		{
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000DD0FF File Offset: 0x000DB2FF
		internal MapPointRules(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x000DD10E File Offset: 0x000DB30E
		// (set) Token: 0x0600313B RID: 12603 RVA: 0x000DD116 File Offset: 0x000DB316
		internal MapSizeRule MapSizeRule
		{
			get
			{
				return this.m_mapSizeRule;
			}
			set
			{
				this.m_mapSizeRule = value;
			}
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x000DD11F File Offset: 0x000DB31F
		// (set) Token: 0x0600313D RID: 12605 RVA: 0x000DD127 File Offset: 0x000DB327
		internal MapColorRule MapColorRule
		{
			get
			{
				return this.m_mapColorRule;
			}
			set
			{
				this.m_mapColorRule = value;
			}
		}

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x0600313E RID: 12606 RVA: 0x000DD130 File Offset: 0x000DB330
		// (set) Token: 0x0600313F RID: 12607 RVA: 0x000DD138 File Offset: 0x000DB338
		internal MapMarkerRule MapMarkerRule
		{
			get
			{
				return this.m_mapMarkerRule;
			}
			set
			{
				this.m_mapMarkerRule = value;
			}
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x000DD141 File Offset: 0x000DB341
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000DD14E File Offset: 0x000DB34E
		internal MapPointRulesExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000DD158 File Offset: 0x000DB358
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapPointRulesStart();
			if (this.m_mapSizeRule != null)
			{
				this.m_mapSizeRule.Initialize(context);
			}
			if (this.m_mapColorRule != null)
			{
				this.m_mapColorRule.Initialize(context);
			}
			if (this.m_mapMarkerRule != null)
			{
				this.m_mapMarkerRule.Initialize(context);
			}
			context.ExprHostBuilder.MapPointRulesEnd();
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x000DD1BC File Offset: 0x000DB3BC
		internal void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapPointRulesStart();
			if (this.m_mapSizeRule != null)
			{
				this.m_mapSizeRule.InitializeMapMember(context);
			}
			if (this.m_mapColorRule != null)
			{
				this.m_mapColorRule.InitializeMapMember(context);
			}
			if (this.m_mapMarkerRule != null)
			{
				this.m_mapMarkerRule.InitializeMapMember(context);
			}
			context.ExprHostBuilder.MapPointRulesEnd();
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000DD220 File Offset: 0x000DB420
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapPointRules mapPointRules = (MapPointRules)base.MemberwiseClone();
			mapPointRules.m_map = context.CurrentMapClone;
			if (this.m_mapSizeRule != null)
			{
				mapPointRules.m_mapSizeRule = (MapSizeRule)this.m_mapSizeRule.PublishClone(context);
			}
			if (this.m_mapColorRule != null)
			{
				mapPointRules.m_mapColorRule = (MapColorRule)this.m_mapColorRule.PublishClone(context);
			}
			if (this.m_mapMarkerRule != null)
			{
				mapPointRules.m_mapMarkerRule = (MapMarkerRule)this.m_mapMarkerRule.PublishClone(context);
			}
			return mapPointRules;
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000DD2A4 File Offset: 0x000DB4A4
		internal void SetExprHost(MapPointRulesExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_mapSizeRule != null && this.ExprHost.MapSizeRuleHost != null)
			{
				this.m_mapSizeRule.SetExprHost(this.ExprHost.MapSizeRuleHost, reportObjectModel);
			}
			if (this.m_mapColorRule != null && this.ExprHost.MapColorRuleHost != null)
			{
				this.m_mapColorRule.SetExprHost(this.ExprHost.MapColorRuleHost, reportObjectModel);
			}
			if (this.m_mapMarkerRule != null && this.ExprHost.MapMarkerRuleHost != null)
			{
				this.m_mapMarkerRule.SetExprHost(this.ExprHost.MapMarkerRuleHost, reportObjectModel);
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x000DD364 File Offset: 0x000DB564
		internal void SetExprHostMapMember(MapPointRulesExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHostMapMember = exprHost;
			this.m_exprHostMapMember.SetReportObjectModel(reportObjectModel);
			if (this.m_mapSizeRule != null && this.m_exprHostMapMember.MapSizeRuleHost != null)
			{
				this.m_mapSizeRule.SetExprHostMapMember(this.m_exprHostMapMember.MapSizeRuleHost, reportObjectModel);
			}
			if (this.m_mapColorRule != null && this.m_exprHostMapMember.MapColorRuleHost != null)
			{
				this.m_mapColorRule.SetExprHostMapMember(this.m_exprHostMapMember.MapColorRuleHost, reportObjectModel);
			}
			if (this.m_mapMarkerRule != null && this.m_exprHostMapMember.MapMarkerRuleHost != null)
			{
				this.m_mapMarkerRule.SetExprHostMapMember(this.m_exprHostMapMember.MapMarkerRuleHost, reportObjectModel);
			}
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000DD424 File Offset: 0x000DB624
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointRules, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapSizeRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSizeRule),
				new MemberInfo(MemberName.MapColorRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorRule),
				new MemberInfo(MemberName.MapMarkerRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarkerRule),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference)
			});
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000DD498 File Offset: 0x000DB698
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapPointRules.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.MapSizeRule)
				{
					if (memberName == MemberName.Map)
					{
						writer.WriteReference(this.m_map);
						continue;
					}
					if (memberName == MemberName.MapSizeRule)
					{
						writer.Write(this.m_mapSizeRule);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.MapColorRule)
					{
						writer.Write(this.m_mapColorRule);
						continue;
					}
					if (memberName == MemberName.MapMarkerRule)
					{
						writer.Write(this.m_mapMarkerRule);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000DD544 File Offset: 0x000DB744
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapPointRules.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.MapSizeRule)
				{
					if (memberName == MemberName.Map)
					{
						this.m_map = reader.ReadReference<Map>(this);
						continue;
					}
					if (memberName == MemberName.MapSizeRule)
					{
						this.m_mapSizeRule = (MapSizeRule)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.MapColorRule)
					{
						this.m_mapColorRule = (MapColorRule)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.MapMarkerRule)
					{
						this.m_mapMarkerRule = (MapMarkerRule)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000DD600 File Offset: 0x000DB800
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapPointRules.m_Declaration.ObjectType, out list))
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

		// Token: 0x0600314B RID: 12619 RVA: 0x000DD6A4 File Offset: 0x000DB8A4
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointRules;
		}

		// Token: 0x0400192B RID: 6443
		[NonSerialized]
		private MapPointRulesExprHost m_exprHost;

		// Token: 0x0400192C RID: 6444
		[NonSerialized]
		private MapPointRulesExprHost m_exprHostMapMember;

		// Token: 0x0400192D RID: 6445
		[Reference]
		private Map m_map;

		// Token: 0x0400192E RID: 6446
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapPointRules.GetDeclaration();

		// Token: 0x0400192F RID: 6447
		private MapSizeRule m_mapSizeRule;

		// Token: 0x04001930 RID: 6448
		private MapColorRule m_mapColorRule;

		// Token: 0x04001931 RID: 6449
		private MapMarkerRule m_mapMarkerRule;
	}
}
