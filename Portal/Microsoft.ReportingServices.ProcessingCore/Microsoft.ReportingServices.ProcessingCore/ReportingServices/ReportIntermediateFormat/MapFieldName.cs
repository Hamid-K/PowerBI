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
	// Token: 0x0200042B RID: 1067
	[Serializable]
	internal sealed class MapFieldName : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002F45 RID: 12101 RVA: 0x000D610F File Offset: 0x000D430F
		internal MapFieldName()
		{
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000D611E File Offset: 0x000D431E
		internal MapFieldName(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x000D6134 File Offset: 0x000D4334
		// (set) Token: 0x06002F48 RID: 12104 RVA: 0x000D613C File Offset: 0x000D433C
		internal ExpressionInfo Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06002F49 RID: 12105 RVA: 0x000D6145 File Offset: 0x000D4345
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x000D6152 File Offset: 0x000D4352
		internal MapFieldNameExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x000D615A File Offset: 0x000D435A
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000D6164 File Offset: 0x000D4364
		internal void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.MapFieldNameStart(index.ToString(CultureInfo.InvariantCulture.NumberFormat));
			if (this.m_name != null)
			{
				this.m_name.Initialize("Name", context);
				context.ExprHostBuilder.MapFieldNameName(this.m_name);
			}
			this.m_exprHostID = context.ExprHostBuilder.MapFieldNameEnd();
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000D61CC File Offset: 0x000D43CC
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapFieldName mapFieldName = (MapFieldName)base.MemberwiseClone();
			mapFieldName.m_map = context.CurrentMapClone;
			if (this.m_name != null)
			{
				mapFieldName.m_name = (ExpressionInfo)this.m_name.PublishClone(context);
			}
			return mapFieldName;
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000D6212 File Offset: 0x000D4412
		internal void SetExprHost(MapFieldNameExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000D6240 File Offset: 0x000D4440
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapFieldName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000D629C File Offset: 0x000D449C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapFieldName.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.ExprHostID)
					{
						if (memberName == MemberName.Map)
						{
							writer.WriteReference(this.m_map);
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						writer.Write(this.m_exprHostID);
					}
				}
				else
				{
					writer.Write(this.m_name);
				}
			}
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000D631C File Offset: 0x000D451C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapFieldName.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Name)
				{
					if (memberName != MemberName.ExprHostID)
					{
						if (memberName == MemberName.Map)
						{
							this.m_map = reader.ReadReference<Map>(this);
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						this.m_exprHostID = reader.ReadInt32();
					}
				}
				else
				{
					this.m_name = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000D63A0 File Offset: 0x000D45A0
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapFieldName.m_Declaration.ObjectType, out list))
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

		// Token: 0x06002F53 RID: 12115 RVA: 0x000D6444 File Offset: 0x000D4644
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapFieldName;
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000D644B File Offset: 0x000D464B
		internal string EvaluateName(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapFieldNameNameExpression(this, this.m_map.Name);
		}

		// Token: 0x040018AD RID: 6317
		private int m_exprHostID = -1;

		// Token: 0x040018AE RID: 6318
		[NonSerialized]
		private MapFieldNameExprHost m_exprHost;

		// Token: 0x040018AF RID: 6319
		[Reference]
		private Map m_map;

		// Token: 0x040018B0 RID: 6320
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapFieldName.GetDeclaration();

		// Token: 0x040018B1 RID: 6321
		private ExpressionInfo m_name;
	}
}
