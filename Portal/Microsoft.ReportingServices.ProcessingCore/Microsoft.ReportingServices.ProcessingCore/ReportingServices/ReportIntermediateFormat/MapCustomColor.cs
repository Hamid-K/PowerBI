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
	// Token: 0x02000444 RID: 1092
	[Serializable]
	internal sealed class MapCustomColor : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600314D RID: 12621 RVA: 0x000DD6B7 File Offset: 0x000DB8B7
		internal MapCustomColor()
		{
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000DD6C6 File Offset: 0x000DB8C6
		internal MapCustomColor(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x000DD6DC File Offset: 0x000DB8DC
		// (set) Token: 0x06003150 RID: 12624 RVA: 0x000DD6E4 File Offset: 0x000DB8E4
		internal ExpressionInfo Color
		{
			get
			{
				return this.m_color;
			}
			set
			{
				this.m_color = value;
			}
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000DD6ED File Offset: 0x000DB8ED
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x000DD6FA File Offset: 0x000DB8FA
		internal MapCustomColorExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x000DD702 File Offset: 0x000DB902
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000DD70C File Offset: 0x000DB90C
		internal void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.MapCustomColorStart(index.ToString(CultureInfo.InvariantCulture.NumberFormat));
			if (this.m_color != null)
			{
				this.m_color.Initialize("Color", context);
				context.ExprHostBuilder.MapCustomColorColor(this.m_color);
			}
			this.m_exprHostID = context.ExprHostBuilder.MapCustomColorEnd();
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000DD774 File Offset: 0x000DB974
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapCustomColor mapCustomColor = (MapCustomColor)base.MemberwiseClone();
			mapCustomColor.m_map = context.CurrentMapClone;
			if (this.m_color != null)
			{
				mapCustomColor.m_color = (ExpressionInfo)this.m_color.PublishClone(context);
			}
			return mapCustomColor;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000DD7BA File Offset: 0x000DB9BA
		internal void SetExprHost(MapCustomColorExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000DD7E8 File Offset: 0x000DB9E8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapCustomColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Color, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000DD848 File Offset: 0x000DBA48
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapCustomColor.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Color)
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
					writer.Write(this.m_color);
				}
			}
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000DD8CC File Offset: 0x000DBACC
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapCustomColor.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Color)
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
					this.m_color = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000DD954 File Offset: 0x000DBB54
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapCustomColor.m_Declaration.ObjectType, out list))
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

		// Token: 0x0600315B RID: 12635 RVA: 0x000DD9F8 File Offset: 0x000DBBF8
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapCustomColor;
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000DD9FF File Offset: 0x000DBBFF
		internal string EvaluateColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapCustomColorColorExpression(this, this.m_map.Name);
		}

		// Token: 0x04001932 RID: 6450
		private int m_exprHostID = -1;

		// Token: 0x04001933 RID: 6451
		[NonSerialized]
		private MapCustomColorExprHost m_exprHost;

		// Token: 0x04001934 RID: 6452
		[Reference]
		private Map m_map;

		// Token: 0x04001935 RID: 6453
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapCustomColor.GetDeclaration();

		// Token: 0x04001936 RID: 6454
		private ExpressionInfo m_color;
	}
}
