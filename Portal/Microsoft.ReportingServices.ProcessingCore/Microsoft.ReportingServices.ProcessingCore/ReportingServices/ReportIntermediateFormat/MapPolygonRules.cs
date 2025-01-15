using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200043E RID: 1086
	[Serializable]
	internal sealed class MapPolygonRules : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060030D5 RID: 12501 RVA: 0x000DBC7F File Offset: 0x000D9E7F
		internal MapPolygonRules()
		{
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000DBC87 File Offset: 0x000D9E87
		internal MapPolygonRules(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x000DBC96 File Offset: 0x000D9E96
		// (set) Token: 0x060030D8 RID: 12504 RVA: 0x000DBC9E File Offset: 0x000D9E9E
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

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x060030D9 RID: 12505 RVA: 0x000DBCA7 File Offset: 0x000D9EA7
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x000DBCB4 File Offset: 0x000D9EB4
		internal MapPolygonRulesExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000DBCBC File Offset: 0x000D9EBC
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapPolygonRulesStart();
			if (this.m_mapColorRule != null)
			{
				this.m_mapColorRule.Initialize(context);
			}
			context.ExprHostBuilder.MapPolygonRulesEnd();
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000DBCEA File Offset: 0x000D9EEA
		internal void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapPolygonRulesStart();
			if (this.m_mapColorRule != null)
			{
				this.m_mapColorRule.InitializeMapMember(context);
			}
			context.ExprHostBuilder.MapPolygonRulesEnd();
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000DBD18 File Offset: 0x000D9F18
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapPolygonRules mapPolygonRules = (MapPolygonRules)base.MemberwiseClone();
			mapPolygonRules.m_map = context.CurrentMapClone;
			if (this.m_mapColorRule != null)
			{
				mapPolygonRules.m_mapColorRule = (MapColorRule)this.m_mapColorRule.PublishClone(context);
			}
			return mapPolygonRules;
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000DBD60 File Offset: 0x000D9F60
		internal void SetExprHost(MapPolygonRulesExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_mapColorRule != null && this.ExprHost.MapColorRuleHost != null)
			{
				this.m_mapColorRule.SetExprHost(this.ExprHost.MapColorRuleHost, reportObjectModel);
			}
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000DBDC8 File Offset: 0x000D9FC8
		internal void SetExprHostMapMember(MapPolygonRulesExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHostMapMember = exprHost;
			this.m_exprHostMapMember.SetReportObjectModel(reportObjectModel);
			if (this.m_mapColorRule != null && this.m_exprHostMapMember.MapColorRuleHost != null)
			{
				this.m_mapColorRule.SetExprHostMapMember(this.m_exprHostMapMember.MapColorRuleHost, reportObjectModel);
			}
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000DBE30 File Offset: 0x000DA030
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonRules, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapColorRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorRule),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference)
			});
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000DBE7C File Offset: 0x000DA07C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapPolygonRules.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Map)
				{
					if (memberName != MemberName.MapColorRule)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_mapColorRule);
					}
				}
				else
				{
					writer.WriteReference(this.m_map);
				}
			}
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000DBEE8 File Offset: 0x000DA0E8
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapPolygonRules.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Map)
				{
					if (memberName != MemberName.MapColorRule)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_mapColorRule = (MapColorRule)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_map = reader.ReadReference<Map>(this);
				}
			}
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000DBF5C File Offset: 0x000DA15C
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapPolygonRules.m_Declaration.ObjectType, out list))
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

		// Token: 0x060030E4 RID: 12516 RVA: 0x000DC000 File Offset: 0x000DA200
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonRules;
		}

		// Token: 0x04001913 RID: 6419
		[NonSerialized]
		private MapPolygonRulesExprHost m_exprHost;

		// Token: 0x04001914 RID: 6420
		[NonSerialized]
		private MapPolygonRulesExprHost m_exprHostMapMember;

		// Token: 0x04001915 RID: 6421
		[Reference]
		private Map m_map;

		// Token: 0x04001916 RID: 6422
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapPolygonRules.GetDeclaration();

		// Token: 0x04001917 RID: 6423
		private MapColorRule m_mapColorRule;
	}
}
