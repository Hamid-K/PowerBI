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
	// Token: 0x02000452 RID: 1106
	[Serializable]
	internal sealed class MapLimits : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600327B RID: 12923 RVA: 0x000E153D File Offset: 0x000DF73D
		internal MapLimits()
		{
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000E1545 File Offset: 0x000DF745
		internal MapLimits(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000E1554 File Offset: 0x000DF754
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x000E155C File Offset: 0x000DF75C
		internal ExpressionInfo MinimumX
		{
			get
			{
				return this.m_minimumX;
			}
			set
			{
				this.m_minimumX = value;
			}
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000E1565 File Offset: 0x000DF765
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x000E156D File Offset: 0x000DF76D
		internal ExpressionInfo MinimumY
		{
			get
			{
				return this.m_minimumY;
			}
			set
			{
				this.m_minimumY = value;
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000E1576 File Offset: 0x000DF776
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x000E157E File Offset: 0x000DF77E
		internal ExpressionInfo MaximumX
		{
			get
			{
				return this.m_maximumX;
			}
			set
			{
				this.m_maximumX = value;
			}
		}

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000E1587 File Offset: 0x000DF787
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x000E158F File Offset: 0x000DF78F
		internal ExpressionInfo MaximumY
		{
			get
			{
				return this.m_maximumY;
			}
			set
			{
				this.m_maximumY = value;
			}
		}

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000E1598 File Offset: 0x000DF798
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x000E15A0 File Offset: 0x000DF7A0
		internal ExpressionInfo LimitToData
		{
			get
			{
				return this.m_limitToData;
			}
			set
			{
				this.m_limitToData = value;
			}
		}

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000E15A9 File Offset: 0x000DF7A9
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06003288 RID: 12936 RVA: 0x000E15B6 File Offset: 0x000DF7B6
		internal MapLimitsExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000E15C0 File Offset: 0x000DF7C0
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapLimitsStart();
			if (this.m_minimumX != null)
			{
				this.m_minimumX.Initialize("MinimumX", context);
				context.ExprHostBuilder.MapLimitsMinimumX(this.m_minimumX);
			}
			if (this.m_minimumY != null)
			{
				this.m_minimumY.Initialize("MinimumY", context);
				context.ExprHostBuilder.MapLimitsMinimumY(this.m_minimumY);
			}
			if (this.m_maximumX != null)
			{
				this.m_maximumX.Initialize("MaximumX", context);
				context.ExprHostBuilder.MapLimitsMaximumX(this.m_maximumX);
			}
			if (this.m_maximumY != null)
			{
				this.m_maximumY.Initialize("MaximumY", context);
				context.ExprHostBuilder.MapLimitsMaximumY(this.m_maximumY);
			}
			if (this.m_limitToData != null)
			{
				this.m_limitToData.Initialize("LimitToData", context);
				context.ExprHostBuilder.MapLimitsLimitToData(this.m_limitToData);
			}
			context.ExprHostBuilder.MapLimitsEnd();
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000E16BC File Offset: 0x000DF8BC
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapLimits mapLimits = (MapLimits)base.MemberwiseClone();
			mapLimits.m_map = context.CurrentMapClone;
			if (this.m_minimumX != null)
			{
				mapLimits.m_minimumX = (ExpressionInfo)this.m_minimumX.PublishClone(context);
			}
			if (this.m_minimumY != null)
			{
				mapLimits.m_minimumY = (ExpressionInfo)this.m_minimumY.PublishClone(context);
			}
			if (this.m_maximumX != null)
			{
				mapLimits.m_maximumX = (ExpressionInfo)this.m_maximumX.PublishClone(context);
			}
			if (this.m_maximumY != null)
			{
				mapLimits.m_maximumY = (ExpressionInfo)this.m_maximumY.PublishClone(context);
			}
			if (this.m_limitToData != null)
			{
				mapLimits.m_limitToData = (ExpressionInfo)this.m_limitToData.PublishClone(context);
			}
			return mapLimits;
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000E177E File Offset: 0x000DF97E
		internal void SetExprHost(MapLimitsExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000E17AC File Offset: 0x000DF9AC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLimits, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MinimumX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MinimumY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MaximumX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MaximumY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LimitToData, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference)
			});
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000E184C File Offset: 0x000DFA4C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapLimits.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Map)
				{
					switch (memberName)
					{
					case MemberName.MinimumX:
						writer.Write(this.m_minimumX);
						break;
					case MemberName.MinimumY:
						writer.Write(this.m_minimumY);
						break;
					case MemberName.MaximumX:
						writer.Write(this.m_maximumX);
						break;
					case MemberName.MaximumY:
						writer.Write(this.m_maximumY);
						break;
					case MemberName.LimitToData:
						writer.Write(this.m_limitToData);
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
				else
				{
					writer.WriteReference(this.m_map);
				}
			}
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000E1914 File Offset: 0x000DFB14
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapLimits.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Map)
				{
					switch (memberName)
					{
					case MemberName.MinimumX:
						this.m_minimumX = (ExpressionInfo)reader.ReadRIFObject();
						break;
					case MemberName.MinimumY:
						this.m_minimumY = (ExpressionInfo)reader.ReadRIFObject();
						break;
					case MemberName.MaximumX:
						this.m_maximumX = (ExpressionInfo)reader.ReadRIFObject();
						break;
					case MemberName.MaximumY:
						this.m_maximumY = (ExpressionInfo)reader.ReadRIFObject();
						break;
					case MemberName.LimitToData:
						this.m_limitToData = (ExpressionInfo)reader.ReadRIFObject();
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
				else
				{
					this.m_map = reader.ReadReference<Map>(this);
				}
			}
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000E19F4 File Offset: 0x000DFBF4
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapLimits.m_Declaration.ObjectType, out list))
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

		// Token: 0x06003290 RID: 12944 RVA: 0x000E1A98 File Offset: 0x000DFC98
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLimits;
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000E1A9F File Offset: 0x000DFC9F
		internal double EvaluateMinimumX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLimitsMinimumXExpression(this, this.m_map.Name);
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000E1AC5 File Offset: 0x000DFCC5
		internal double EvaluateMinimumY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLimitsMinimumYExpression(this, this.m_map.Name);
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000E1AEB File Offset: 0x000DFCEB
		internal double EvaluateMaximumX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLimitsMaximumXExpression(this, this.m_map.Name);
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000E1B11 File Offset: 0x000DFD11
		internal double EvaluateMaximumY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLimitsMaximumYExpression(this, this.m_map.Name);
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000E1B37 File Offset: 0x000DFD37
		internal bool EvaluateLimitToData(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLimitsLimitToDataExpression(this, this.m_map.Name);
		}

		// Token: 0x0400197F RID: 6527
		[NonSerialized]
		private MapLimitsExprHost m_exprHost;

		// Token: 0x04001980 RID: 6528
		[Reference]
		private Map m_map;

		// Token: 0x04001981 RID: 6529
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapLimits.GetDeclaration();

		// Token: 0x04001982 RID: 6530
		private ExpressionInfo m_minimumX;

		// Token: 0x04001983 RID: 6531
		private ExpressionInfo m_minimumY;

		// Token: 0x04001984 RID: 6532
		private ExpressionInfo m_maximumX;

		// Token: 0x04001985 RID: 6533
		private ExpressionInfo m_maximumY;

		// Token: 0x04001986 RID: 6534
		private ExpressionInfo m_limitToData;
	}
}
