using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200041B RID: 1051
	[Serializable]
	internal sealed class MapRow : Row, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002DE8 RID: 11752 RVA: 0x000D1B1F File Offset: 0x000CFD1F
		internal MapRow()
		{
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000D1B27 File Offset: 0x000CFD27
		internal MapRow(int id, MapDataRegion mapDataRegion)
			: base(id)
		{
			this.m_mapDataRegion = mapDataRegion;
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x000D1B37 File Offset: 0x000CFD37
		internal override CellList Cells
		{
			get
			{
				return this.m_cells;
			}
		}

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000D1B3F File Offset: 0x000CFD3F
		// (set) Token: 0x06002DEC RID: 11756 RVA: 0x000D1B6A File Offset: 0x000CFD6A
		internal MapCell Cell
		{
			get
			{
				if (this.m_cells != null && this.m_cells.Count == 1)
				{
					return (MapCell)this.m_cells[0];
				}
				return null;
			}
			set
			{
				if (this.m_cells == null)
				{
					this.m_cells = new CellList();
				}
				else
				{
					this.m_cells.Clear();
				}
				this.m_cells.Add(value);
			}
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000D1B9C File Offset: 0x000CFD9C
		[SkipMemberStaticValidation(MemberName.MapCell)]
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Row, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapCell),
				new MemberInfo(MemberName.MapDataRegion, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapDataRegion, Token.Reference)
			});
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000D1BEC File Offset: 0x000CFDEC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapRow mapRow = (MapRow)base.PublishClone(context);
			if (this.m_cells != null)
			{
				mapRow.m_cells = new CellList();
				mapRow.Cell = (MapCell)this.Cell.PublishClone(context);
			}
			return mapRow;
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000D1C34 File Offset: 0x000CFE34
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapRow.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.MapDataRegion)
				{
					if (memberName == MemberName.MapCell)
					{
						writer.Write(this.Cell);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.WriteReference(this.m_mapDataRegion);
				}
			}
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000D1CA8 File Offset: 0x000CFEA8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapRow.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.MapDataRegion)
				{
					if (memberName == MemberName.MapCell)
					{
						this.Cell = (MapCell)reader.ReadRIFObject();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.m_mapDataRegion = reader.ReadReference<MapDataRegion>(this);
				}
			}
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000D1D20 File Offset: 0x000CFF20
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapRow.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.MapDataRegion)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_mapDataRegion = (MapDataRegion)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000D1DC4 File Offset: 0x000CFFC4
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapRow;
		}

		// Token: 0x04001853 RID: 6227
		[NonSerialized]
		private CellList m_cells;

		// Token: 0x04001854 RID: 6228
		[Reference]
		private MapDataRegion m_mapDataRegion;

		// Token: 0x04001855 RID: 6229
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapRow.GetDeclaration();
	}
}
