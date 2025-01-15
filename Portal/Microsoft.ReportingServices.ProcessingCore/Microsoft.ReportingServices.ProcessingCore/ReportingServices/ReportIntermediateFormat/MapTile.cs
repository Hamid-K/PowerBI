using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000435 RID: 1077
	[Serializable]
	internal sealed class MapTile : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003006 RID: 12294 RVA: 0x000D8E3B File Offset: 0x000D703B
		internal MapTile()
		{
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000D8E4A File Offset: 0x000D704A
		internal MapTile(Map map)
		{
			this.m_map = map;
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x000D8E60 File Offset: 0x000D7060
		// (set) Token: 0x06003009 RID: 12297 RVA: 0x000D8E68 File Offset: 0x000D7068
		internal string Name
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

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x000D8E71 File Offset: 0x000D7071
		// (set) Token: 0x0600300B RID: 12299 RVA: 0x000D8E79 File Offset: 0x000D7079
		internal string TileData
		{
			get
			{
				return this.m_tileData;
			}
			set
			{
				this.m_tileData = value;
			}
		}

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x000D8E82 File Offset: 0x000D7082
		// (set) Token: 0x0600300D RID: 12301 RVA: 0x000D8E8A File Offset: 0x000D708A
		internal string MIMEType
		{
			get
			{
				return this.m_mIMEType;
			}
			set
			{
				this.m_mIMEType = value;
			}
		}

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x000D8E93 File Offset: 0x000D7093
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x0600300F RID: 12303 RVA: 0x000D8EA0 File Offset: 0x000D70A0
		internal MapTileExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06003010 RID: 12304 RVA: 0x000D8EA8 File Offset: 0x000D70A8
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000D8EB0 File Offset: 0x000D70B0
		internal void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.MapTileStart(index.ToString(CultureInfo.InvariantCulture.NumberFormat));
			this.m_exprHostID = context.ExprHostBuilder.MapTileEnd();
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000D8EE1 File Offset: 0x000D70E1
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			MapTile mapTile = (MapTile)base.MemberwiseClone();
			mapTile.m_map = context.CurrentMapClone;
			return mapTile;
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000D8EFB File Offset: 0x000D70FB
		internal void SetExprHost(MapTileExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000D8F2C File Offset: 0x000D712C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapTile, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.TileData, Token.String),
				new MemberInfo(MemberName.MIMEType, Token.String),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000D8FB4 File Offset: 0x000D71B4
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapTile.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.MIMEType)
				{
					if (memberName == MemberName.Name)
					{
						writer.Write(this.m_name);
						continue;
					}
					if (memberName == MemberName.MIMEType)
					{
						writer.Write(this.m_mIMEType);
						continue;
					}
				}
				else
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
					if (memberName == MemberName.TileData)
					{
						writer.Write(this.m_tileData);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000D9074 File Offset: 0x000D7274
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapTile.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.MIMEType)
				{
					if (memberName == MemberName.Name)
					{
						this.m_name = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.MIMEType)
					{
						this.m_mIMEType = reader.ReadString();
						continue;
					}
				}
				else
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
					if (memberName == MemberName.TileData)
					{
						this.m_tileData = reader.ReadString();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000D9134 File Offset: 0x000D7334
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapTile.m_Declaration.ObjectType, out list))
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

		// Token: 0x06003018 RID: 12312 RVA: 0x000D91D8 File Offset: 0x000D73D8
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapTile;
		}

		// Token: 0x040018D9 RID: 6361
		private int m_exprHostID = -1;

		// Token: 0x040018DA RID: 6362
		[NonSerialized]
		private MapTileExprHost m_exprHost;

		// Token: 0x040018DB RID: 6363
		[Reference]
		private Map m_map;

		// Token: 0x040018DC RID: 6364
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapTile.GetDeclaration();

		// Token: 0x040018DD RID: 6365
		private string m_name;

		// Token: 0x040018DE RID: 6366
		private string m_tileData;

		// Token: 0x040018DF RID: 6367
		private string m_mIMEType;
	}
}
