using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003E8 RID: 1000
	[Serializable]
	internal sealed class GaugeRow : Row, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002935 RID: 10549 RVA: 0x000C0E3B File Offset: 0x000BF03B
		internal GaugeRow()
		{
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000C0E43 File Offset: 0x000BF043
		internal GaugeRow(int id, GaugePanel gaugePanel)
			: base(id)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x000C0E53 File Offset: 0x000BF053
		internal override CellList Cells
		{
			get
			{
				return this.m_cells;
			}
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x000C0E5B File Offset: 0x000BF05B
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x000C0E81 File Offset: 0x000BF081
		internal GaugeCell GaugeCell
		{
			get
			{
				if (this.m_cells != null && this.m_cells.Count > 0)
				{
					return this.m_cells[0];
				}
				return null;
			}
			set
			{
				if (this.m_cells == null)
				{
					this.m_cells = new GaugeCellList();
				}
				else
				{
					this.m_cells.Clear();
				}
				this.m_cells.Add(value);
			}
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000C0EB0 File Offset: 0x000BF0B0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Row, new List<MemberInfo>
			{
				new ReadOnlyMemberInfo(MemberName.GaugeCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeCell),
				new MemberInfo(MemberName.GaugePanel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanel, Token.Reference),
				new MemberInfo(MemberName.Cells, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeCell)
			});
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000C0F14 File Offset: 0x000BF114
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			GaugeRow gaugeRow = (GaugeRow)base.PublishClone(context);
			if (this.m_cells != null)
			{
				gaugeRow.m_cells = new GaugeCellList();
				gaugeRow.GaugeCell = (GaugeCell)this.GaugeCell.PublishClone(context);
			}
			return gaugeRow;
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000C0F5C File Offset: 0x000BF15C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(GaugeRow.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Cells)
				{
					if (memberName == MemberName.GaugePanel)
					{
						writer.WriteReference(this.m_gaugePanel);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.Write(this.m_cells);
				}
			}
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000C0FCC File Offset: 0x000BF1CC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(GaugeRow.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Cells)
				{
					if (memberName != MemberName.GaugePanel)
					{
						if (memberName == MemberName.GaugeCell)
						{
							this.GaugeCell = (GaugeCell)reader.ReadRIFObject();
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						this.m_gaugePanel = reader.ReadReference<GaugePanel>(this);
					}
				}
				else
				{
					this.m_cells = reader.ReadListOfRIFObjects<GaugeCellList>();
				}
			}
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000C1058 File Offset: 0x000BF258
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(GaugeRow.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.GaugePanel)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_gaugePanel = (GaugePanel)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000C10FC File Offset: 0x000BF2FC
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeRow;
		}

		// Token: 0x040016F4 RID: 5876
		private GaugeCellList m_cells;

		// Token: 0x040016F5 RID: 5877
		[Reference]
		private GaugePanel m_gaugePanel;

		// Token: 0x040016F6 RID: 5878
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugeRow.GetDeclaration();
	}
}
