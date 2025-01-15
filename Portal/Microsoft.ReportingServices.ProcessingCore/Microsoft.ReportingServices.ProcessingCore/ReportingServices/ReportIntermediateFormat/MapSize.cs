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
	// Token: 0x0200041E RID: 1054
	[Serializable]
	internal sealed class MapSize : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002E14 RID: 11796 RVA: 0x000D2304 File Offset: 0x000D0504
		internal MapSize()
		{
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000D230C File Offset: 0x000D050C
		internal MapSize(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x170015F2 RID: 5618
		// (get) Token: 0x06002E16 RID: 11798 RVA: 0x000D231B File Offset: 0x000D051B
		// (set) Token: 0x06002E17 RID: 11799 RVA: 0x000D2323 File Offset: 0x000D0523
		internal ExpressionInfo Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x170015F3 RID: 5619
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x000D232C File Offset: 0x000D052C
		// (set) Token: 0x06002E19 RID: 11801 RVA: 0x000D2334 File Offset: 0x000D0534
		internal ExpressionInfo Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x000D233D File Offset: 0x000D053D
		// (set) Token: 0x06002E1B RID: 11803 RVA: 0x000D2345 File Offset: 0x000D0545
		internal ExpressionInfo Unit
		{
			get
			{
				return this.m_unit;
			}
			set
			{
				this.m_unit = value;
			}
		}

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x000D234E File Offset: 0x000D054E
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x000D235B File Offset: 0x000D055B
		internal MapSizeExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000D2364 File Offset: 0x000D0564
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapSizeStart();
			if (this.m_width != null)
			{
				this.m_width.Initialize("Width", context);
				context.ExprHostBuilder.MapSizeWidth(this.m_width);
			}
			if (this.m_height != null)
			{
				this.m_height.Initialize("Height", context);
				context.ExprHostBuilder.MapSizeHeight(this.m_height);
			}
			if (this.m_unit != null)
			{
				this.m_unit.Initialize("Unit", context);
				context.ExprHostBuilder.MapSizeUnit(this.m_unit);
			}
			context.ExprHostBuilder.MapSizeEnd();
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000D240C File Offset: 0x000D060C
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapSize mapSize = (MapSize)base.MemberwiseClone();
			mapSize.m_map = context.CurrentMapClone;
			if (this.m_width != null)
			{
				mapSize.m_width = (ExpressionInfo)this.m_width.PublishClone(context);
			}
			if (this.m_height != null)
			{
				mapSize.m_height = (ExpressionInfo)this.m_height.PublishClone(context);
			}
			if (this.m_unit != null)
			{
				mapSize.m_unit = (ExpressionInfo)this.m_unit.PublishClone(context);
			}
			return mapSize;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000D2490 File Offset: 0x000D0690
		internal void SetExprHost(MapSizeExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000D24C0 File Offset: 0x000D06C0
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Width, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Height, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Unit, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference)
			});
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000D2534 File Offset: 0x000D0734
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapSize.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Width)
				{
					if (memberName == MemberName.Height)
					{
						writer.Write(this.m_height);
						continue;
					}
					if (memberName == MemberName.Width)
					{
						writer.Write(this.m_width);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Map)
					{
						writer.WriteReference(this.m_map);
						continue;
					}
					if (memberName == MemberName.Unit)
					{
						writer.Write(this.m_unit);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000D25E0 File Offset: 0x000D07E0
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapSize.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Width)
				{
					if (memberName == MemberName.Height)
					{
						this.m_height = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Width)
					{
						this.m_width = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Map)
					{
						this.m_map = reader.ReadReference<Map>(this);
						continue;
					}
					if (memberName == MemberName.Unit)
					{
						this.m_unit = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000D269C File Offset: 0x000D089C
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapSize.m_Declaration.ObjectType, out list))
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

		// Token: 0x06002E25 RID: 11813 RVA: 0x000D2740 File Offset: 0x000D0940
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSize;
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000D2747 File Offset: 0x000D0947
		internal double EvaluateWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSizeWidthExpression(this, this.m_map.Name);
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000D276D File Offset: 0x000D096D
		internal double EvaluateHeight(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSizeHeightExpression(this, this.m_map.Name);
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000D2793 File Offset: 0x000D0993
		internal Unit EvaluateUnit(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateUnit(context.ReportRuntime.EvaluateMapSizeUnitExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x0400185D RID: 6237
		[NonSerialized]
		private MapSizeExprHost m_exprHost;

		// Token: 0x0400185E RID: 6238
		[Reference]
		private Map m_map;

		// Token: 0x0400185F RID: 6239
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapSize.GetDeclaration();

		// Token: 0x04001860 RID: 6240
		private ExpressionInfo m_width;

		// Token: 0x04001861 RID: 6241
		private ExpressionInfo m_height;

		// Token: 0x04001862 RID: 6242
		private ExpressionInfo m_unit;
	}
}
