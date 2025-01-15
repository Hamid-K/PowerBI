using System;
using System.Collections.Generic;
using System.Globalization;
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
	// Token: 0x02000420 RID: 1056
	[Serializable]
	internal sealed class MapBindingFieldPair : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002E3A RID: 11834 RVA: 0x000D2A7F File Offset: 0x000D0C7F
		internal MapBindingFieldPair()
		{
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000D2A95 File Offset: 0x000D0C95
		internal MapBindingFieldPair(Map map, MapVectorLayer mapVectorLayer)
		{
			this.m_map = map;
			this.m_mapVectorLayer = mapVectorLayer;
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x000D2AB9 File Offset: 0x000D0CB9
		// (set) Token: 0x06002E3D RID: 11837 RVA: 0x000D2AC1 File Offset: 0x000D0CC1
		internal ExpressionInfo FieldName
		{
			get
			{
				return this.m_fieldName;
			}
			set
			{
				this.m_fieldName = value;
			}
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x000D2ACA File Offset: 0x000D0CCA
		// (set) Token: 0x06002E3F RID: 11839 RVA: 0x000D2AD2 File Offset: 0x000D0CD2
		internal ExpressionInfo BindingExpression
		{
			get
			{
				return this.m_bindingExpression;
			}
			set
			{
				this.m_bindingExpression = value;
			}
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x000D2ADB File Offset: 0x000D0CDB
		internal bool InElementView
		{
			get
			{
				return this.m_mapVectorLayer == null;
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06002E41 RID: 11841 RVA: 0x000D2AE6 File Offset: 0x000D0CE6
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06002E42 RID: 11842 RVA: 0x000D2AF3 File Offset: 0x000D0CF3
		internal MapBindingFieldPairExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x000D2AFB File Offset: 0x000D0CFB
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x000D2B03 File Offset: 0x000D0D03
		internal MapBindingFieldPairExprHost ExprHostMapMember
		{
			get
			{
				return this.m_exprHostMapMember;
			}
		}

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x000D2B0B File Offset: 0x000D0D0B
		internal int ExpressionHostMapMemberID
		{
			get
			{
				return this.m_exprHostMapMemberID;
			}
		}

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x000D2B13 File Offset: 0x000D0D13
		internal IInstancePath InstancePath
		{
			get
			{
				if (this.m_mapVectorLayer != null)
				{
					return this.m_mapVectorLayer.InstancePath;
				}
				return this.m_map;
			}
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000D2B30 File Offset: 0x000D0D30
		internal void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.MapBindingFieldPairStart(index.ToString(CultureInfo.InvariantCulture.NumberFormat));
			if (this.m_fieldName != null)
			{
				this.m_fieldName.Initialize("FieldName", context);
				context.ExprHostBuilder.MapBindingFieldPairFieldName(this.m_fieldName);
			}
			if (this.InElementView && this.m_bindingExpression != null)
			{
				this.m_bindingExpression.Initialize("BindingExpression", context);
				context.ExprHostBuilder.MapBindingFieldPairBindingExpression(this.m_bindingExpression);
			}
			this.m_exprHostID = context.ExprHostBuilder.MapBindingFieldPairEnd();
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000D2BCC File Offset: 0x000D0DCC
		internal void InitializeMapMember(InitializationContext context, int index)
		{
			context.ExprHostBuilder.MapBindingFieldPairStart(index.ToString(CultureInfo.InvariantCulture.NumberFormat));
			if (!this.InElementView && this.m_bindingExpression != null)
			{
				this.m_bindingExpression.Initialize("BindingExpression", context);
				context.ExprHostBuilder.MapBindingFieldPairBindingExpression(this.m_bindingExpression);
			}
			this.m_exprHostMapMemberID = context.ExprHostBuilder.MapBindingFieldPairEnd();
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000D2C3C File Offset: 0x000D0E3C
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapBindingFieldPair mapBindingFieldPair = (MapBindingFieldPair)base.MemberwiseClone();
			mapBindingFieldPair.m_map = context.CurrentMapClone;
			if (this.m_mapVectorLayer != null)
			{
				mapBindingFieldPair.m_mapVectorLayer = context.CurrentMapVectorLayerClone;
			}
			if (this.m_fieldName != null)
			{
				mapBindingFieldPair.m_fieldName = (ExpressionInfo)this.m_fieldName.PublishClone(context);
			}
			if (this.m_bindingExpression != null)
			{
				mapBindingFieldPair.m_bindingExpression = (ExpressionInfo)this.m_bindingExpression.PublishClone(context);
			}
			return mapBindingFieldPair;
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000D2CB6 File Offset: 0x000D0EB6
		internal void SetExprHost(MapBindingFieldPairExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000D2CE4 File Offset: 0x000D0EE4
		internal void SetExprHostMapMember(MapBindingFieldPairExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHostMapMember = exprHost;
			this.m_exprHostMapMember.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000D2D14 File Offset: 0x000D0F14
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBindingFieldPair, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.FieldName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.BindingExpression, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference),
				new MemberInfo(MemberName.MapVectorLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapVectorLayer, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ExprHostMapMemberID, Token.Int32)
			});
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000D2DB4 File Offset: 0x000D0FB4
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapBindingFieldPair.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.FieldName)
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
					if (memberName == MemberName.FieldName)
					{
						writer.Write(this.m_fieldName);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.BindingExpression)
					{
						writer.Write(this.m_bindingExpression);
						continue;
					}
					if (memberName == MemberName.ExprHostMapMemberID)
					{
						writer.Write(this.m_exprHostMapMemberID);
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

		// Token: 0x06002E4E RID: 11854 RVA: 0x000D2E8C File Offset: 0x000D108C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapBindingFieldPair.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.FieldName)
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
					if (memberName == MemberName.FieldName)
					{
						this.m_fieldName = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.BindingExpression)
					{
						this.m_bindingExpression = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ExprHostMapMemberID)
					{
						this.m_exprHostMapMemberID = reader.ReadInt32();
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

		// Token: 0x06002E4F RID: 11855 RVA: 0x000D2F74 File Offset: 0x000D1174
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapBindingFieldPair.m_Declaration.ObjectType, out list))
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

		// Token: 0x06002E50 RID: 11856 RVA: 0x000D305C File Offset: 0x000D125C
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBindingFieldPair;
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x000D3063 File Offset: 0x000D1263
		internal string EvaluateFieldName(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapBindingFieldPairFieldNameExpression(this, this.m_map.Name);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x000D3089 File Offset: 0x000D1289
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateBindingExpression(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapBindingFieldPairBindingExpressionExpression(this, this.m_map.Name);
		}

		// Token: 0x04001866 RID: 6246
		private int m_exprHostID = -1;

		// Token: 0x04001867 RID: 6247
		private int m_exprHostMapMemberID = -1;

		// Token: 0x04001868 RID: 6248
		[NonSerialized]
		private MapBindingFieldPairExprHost m_exprHost;

		// Token: 0x04001869 RID: 6249
		[NonSerialized]
		private MapBindingFieldPairExprHost m_exprHostMapMember;

		// Token: 0x0400186A RID: 6250
		[Reference]
		private Map m_map;

		// Token: 0x0400186B RID: 6251
		[Reference]
		private MapVectorLayer m_mapVectorLayer;

		// Token: 0x0400186C RID: 6252
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapBindingFieldPair.GetDeclaration();

		// Token: 0x0400186D RID: 6253
		private ExpressionInfo m_fieldName;

		// Token: 0x0400186E RID: 6254
		private ExpressionInfo m_bindingExpression;
	}
}
