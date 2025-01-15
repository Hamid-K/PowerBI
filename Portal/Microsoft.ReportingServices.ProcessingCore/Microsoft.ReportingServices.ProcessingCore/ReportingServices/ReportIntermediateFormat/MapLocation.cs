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
	// Token: 0x0200041D RID: 1053
	[Serializable]
	internal sealed class MapLocation : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002DFE RID: 11774 RVA: 0x000D1E36 File Offset: 0x000D0036
		internal MapLocation()
		{
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x000D1E3E File Offset: 0x000D003E
		internal MapLocation(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x000D1E4D File Offset: 0x000D004D
		// (set) Token: 0x06002E01 RID: 11777 RVA: 0x000D1E55 File Offset: 0x000D0055
		internal ExpressionInfo Left
		{
			get
			{
				return this.m_left;
			}
			set
			{
				this.m_left = value;
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x000D1E5E File Offset: 0x000D005E
		// (set) Token: 0x06002E03 RID: 11779 RVA: 0x000D1E66 File Offset: 0x000D0066
		internal ExpressionInfo Top
		{
			get
			{
				return this.m_top;
			}
			set
			{
				this.m_top = value;
			}
		}

		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000D1E6F File Offset: 0x000D006F
		// (set) Token: 0x06002E05 RID: 11781 RVA: 0x000D1E77 File Offset: 0x000D0077
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

		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x000D1E80 File Offset: 0x000D0080
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170015F1 RID: 5617
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x000D1E8D File Offset: 0x000D008D
		internal MapLocationExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000D1E98 File Offset: 0x000D0098
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapLocationStart();
			if (this.m_left != null)
			{
				this.m_left.Initialize("Left", context);
				context.ExprHostBuilder.MapLocationLeft(this.m_left);
			}
			if (this.m_top != null)
			{
				this.m_top.Initialize("Top", context);
				context.ExprHostBuilder.MapLocationTop(this.m_top);
			}
			if (this.m_unit != null)
			{
				this.m_unit.Initialize("Unit", context);
				context.ExprHostBuilder.MapLocationUnit(this.m_unit);
			}
			context.ExprHostBuilder.MapLocationEnd();
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000D1F40 File Offset: 0x000D0140
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapLocation mapLocation = (MapLocation)base.MemberwiseClone();
			mapLocation.m_map = context.CurrentMapClone;
			if (this.m_left != null)
			{
				mapLocation.m_left = (ExpressionInfo)this.m_left.PublishClone(context);
			}
			if (this.m_top != null)
			{
				mapLocation.m_top = (ExpressionInfo)this.m_top.PublishClone(context);
			}
			if (this.m_unit != null)
			{
				mapLocation.m_unit = (ExpressionInfo)this.m_unit.PublishClone(context);
			}
			return mapLocation;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000D1FC4 File Offset: 0x000D01C4
		internal void SetExprHost(MapLocationExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000D1FF4 File Offset: 0x000D01F4
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLocation, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Left, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Top, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Unit, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference)
			});
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000D2068 File Offset: 0x000D0268
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapLocation.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Left)
				{
					if (memberName == MemberName.Top)
					{
						writer.Write(this.m_top);
						continue;
					}
					if (memberName == MemberName.Left)
					{
						writer.Write(this.m_left);
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

		// Token: 0x06002E0D RID: 11789 RVA: 0x000D2114 File Offset: 0x000D0314
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapLocation.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Left)
				{
					if (memberName == MemberName.Top)
					{
						this.m_top = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Left)
					{
						this.m_left = (ExpressionInfo)reader.ReadRIFObject();
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

		// Token: 0x06002E0E RID: 11790 RVA: 0x000D21D0 File Offset: 0x000D03D0
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapLocation.m_Declaration.ObjectType, out list))
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

		// Token: 0x06002E0F RID: 11791 RVA: 0x000D2274 File Offset: 0x000D0474
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLocation;
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000D227B File Offset: 0x000D047B
		internal double EvaluateLeft(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLocationLeftExpression(this, this.m_map.Name);
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x000D22A1 File Offset: 0x000D04A1
		internal double EvaluateTop(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLocationTopExpression(this, this.m_map.Name);
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x000D22C7 File Offset: 0x000D04C7
		internal Unit EvaluateUnit(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateUnit(context.ReportRuntime.EvaluateMapLocationUnitExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x04001857 RID: 6231
		[NonSerialized]
		private MapLocationExprHost m_exprHost;

		// Token: 0x04001858 RID: 6232
		[Reference]
		private Map m_map;

		// Token: 0x04001859 RID: 6233
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapLocation.GetDeclaration();

		// Token: 0x0400185A RID: 6234
		private ExpressionInfo m_left;

		// Token: 0x0400185B RID: 6235
		private ExpressionInfo m_top;

		// Token: 0x0400185C RID: 6236
		private ExpressionInfo m_unit;
	}
}
