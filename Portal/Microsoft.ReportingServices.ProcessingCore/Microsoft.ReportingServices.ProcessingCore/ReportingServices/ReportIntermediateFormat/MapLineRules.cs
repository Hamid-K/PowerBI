using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200043D RID: 1085
	[Serializable]
	internal sealed class MapLineRules : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060030C2 RID: 12482 RVA: 0x000DB7D6 File Offset: 0x000D99D6
		internal MapLineRules()
		{
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000DB7DE File Offset: 0x000D99DE
		internal MapLineRules(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x000DB7ED File Offset: 0x000D99ED
		// (set) Token: 0x060030C5 RID: 12485 RVA: 0x000DB7F5 File Offset: 0x000D99F5
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

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x060030C6 RID: 12486 RVA: 0x000DB7FE File Offset: 0x000D99FE
		// (set) Token: 0x060030C7 RID: 12487 RVA: 0x000DB806 File Offset: 0x000D9A06
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

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000DB80F File Offset: 0x000D9A0F
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x000DB81C File Offset: 0x000D9A1C
		internal MapLineRulesExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000DB824 File Offset: 0x000D9A24
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapLineRulesStart();
			if (this.m_mapSizeRule != null)
			{
				this.m_mapSizeRule.Initialize(context);
			}
			if (this.m_mapColorRule != null)
			{
				this.m_mapColorRule.Initialize(context);
			}
			context.ExprHostBuilder.MapLineRulesEnd();
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000DB874 File Offset: 0x000D9A74
		internal void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapLineRulesStart();
			if (this.m_mapSizeRule != null)
			{
				this.m_mapSizeRule.InitializeMapMember(context);
			}
			if (this.m_mapColorRule != null)
			{
				this.m_mapColorRule.InitializeMapMember(context);
			}
			context.ExprHostBuilder.MapLineRulesEnd();
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000DB8C4 File Offset: 0x000D9AC4
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapLineRules mapLineRules = (MapLineRules)base.MemberwiseClone();
			mapLineRules.m_map = context.CurrentMapClone;
			if (this.m_mapSizeRule != null)
			{
				mapLineRules.m_mapSizeRule = (MapSizeRule)this.m_mapSizeRule.PublishClone(context);
			}
			if (this.m_mapColorRule != null)
			{
				mapLineRules.m_mapColorRule = (MapColorRule)this.m_mapColorRule.PublishClone(context);
			}
			return mapLineRules;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000DB92C File Offset: 0x000D9B2C
		internal void SetExprHost(MapLineRulesExprHost exprHost, ObjectModelImpl reportObjectModel)
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
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000DB9C0 File Offset: 0x000D9BC0
		internal void SetExprHostMapMember(MapLineRulesExprHost exprHost, ObjectModelImpl reportObjectModel)
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
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000DBA54 File Offset: 0x000D9C54
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLineRules, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapSizeRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSizeRule),
				new MemberInfo(MemberName.MapColorRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorRule),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference)
			});
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000DBAB4 File Offset: 0x000D9CB4
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapLineRules.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Map)
				{
					if (memberName != MemberName.MapSizeRule)
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
						writer.Write(this.m_mapSizeRule);
					}
				}
				else
				{
					writer.WriteReference(this.m_map);
				}
			}
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000DBB38 File Offset: 0x000D9D38
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapLineRules.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Map)
				{
					if (memberName != MemberName.MapSizeRule)
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
						this.m_mapSizeRule = (MapSizeRule)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_map = reader.ReadReference<Map>(this);
				}
			}
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000DBBC8 File Offset: 0x000D9DC8
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapLineRules.m_Declaration.ObjectType, out list))
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

		// Token: 0x060030D3 RID: 12499 RVA: 0x000DBC6C File Offset: 0x000D9E6C
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLineRules;
		}

		// Token: 0x0400190D RID: 6413
		[NonSerialized]
		private MapLineRulesExprHost m_exprHost;

		// Token: 0x0400190E RID: 6414
		[NonSerialized]
		private MapLineRulesExprHost m_exprHostMapMember;

		// Token: 0x0400190F RID: 6415
		[Reference]
		private Map m_map;

		// Token: 0x04001910 RID: 6416
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapLineRules.GetDeclaration();

		// Token: 0x04001911 RID: 6417
		private MapSizeRule m_mapSizeRule;

		// Token: 0x04001912 RID: 6418
		private MapColorRule m_mapColorRule;
	}
}
