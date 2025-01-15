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
	// Token: 0x02000439 RID: 1081
	[Serializable]
	internal sealed class MapBucket : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600307C RID: 12412 RVA: 0x000DAB84 File Offset: 0x000D8D84
		internal MapBucket()
		{
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000DAB93 File Offset: 0x000D8D93
		internal MapBucket(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x000DABA9 File Offset: 0x000D8DA9
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x000DABB1 File Offset: 0x000D8DB1
		internal ExpressionInfo StartValue
		{
			get
			{
				return this.m_startValue;
			}
			set
			{
				this.m_startValue = value;
			}
		}

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000DABBA File Offset: 0x000D8DBA
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x000DABC2 File Offset: 0x000D8DC2
		internal ExpressionInfo EndValue
		{
			get
			{
				return this.m_endValue;
			}
			set
			{
				this.m_endValue = value;
			}
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000DABCB File Offset: 0x000D8DCB
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x000DABD8 File Offset: 0x000D8DD8
		internal MapBucketExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000DABE0 File Offset: 0x000D8DE0
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000DABE8 File Offset: 0x000D8DE8
		internal void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.MapBucketStart(index.ToString(CultureInfo.InvariantCulture.NumberFormat));
			if (this.m_startValue != null)
			{
				this.m_startValue.Initialize("StartValue", context);
				context.ExprHostBuilder.MapBucketStartValue(this.m_startValue);
			}
			if (this.m_endValue != null)
			{
				this.m_endValue.Initialize("EndValue", context);
				context.ExprHostBuilder.MapBucketEndValue(this.m_endValue);
			}
			this.m_exprHostID = context.ExprHostBuilder.MapBucketEnd();
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000DAC7C File Offset: 0x000D8E7C
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapBucket mapBucket = (MapBucket)base.MemberwiseClone();
			mapBucket.m_map = context.CurrentMapClone;
			if (this.m_startValue != null)
			{
				mapBucket.m_startValue = (ExpressionInfo)this.m_startValue.PublishClone(context);
			}
			if (this.m_endValue != null)
			{
				mapBucket.m_endValue = (ExpressionInfo)this.m_endValue.PublishClone(context);
			}
			return mapBucket;
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000DACE1 File Offset: 0x000D8EE1
		internal void SetExprHost(MapBucketExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000DAD10 File Offset: 0x000D8F10
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBucket, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.StartValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.EndValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000DAD84 File Offset: 0x000D8F84
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapBucket.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.StartValue)
				{
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
					if (memberName == MemberName.StartValue)
					{
						writer.Write(this.m_startValue);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.EndValue)
					{
						writer.Write(this.m_endValue);
						continue;
					}
					if (memberName == MemberName.Map)
					{
						writer.WriteReference(this.m_map);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000DAE2C File Offset: 0x000D902C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapBucket.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.StartValue)
				{
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.StartValue)
					{
						this.m_startValue = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.EndValue)
					{
						this.m_endValue = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Map)
					{
						this.m_map = reader.ReadReference<Map>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000DAEE0 File Offset: 0x000D90E0
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapBucket.m_Declaration.ObjectType, out list))
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

		// Token: 0x0600308C RID: 12428 RVA: 0x000DAF84 File Offset: 0x000D9184
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBucket;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000DAF8B File Offset: 0x000D918B
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateStartValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapBucketStartValueExpression(this, this.m_map.Name);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000DAFB1 File Offset: 0x000D91B1
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateEndValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapBucketEndValueExpression(this, this.m_map.Name);
		}

		// Token: 0x040018FF RID: 6399
		private int m_exprHostID = -1;

		// Token: 0x04001900 RID: 6400
		[NonSerialized]
		private MapBucketExprHost m_exprHost;

		// Token: 0x04001901 RID: 6401
		[Reference]
		private Map m_map;

		// Token: 0x04001902 RID: 6402
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapBucket.GetDeclaration();

		// Token: 0x04001903 RID: 6403
		private ExpressionInfo m_startValue;

		// Token: 0x04001904 RID: 6404
		private ExpressionInfo m_endValue;
	}
}
