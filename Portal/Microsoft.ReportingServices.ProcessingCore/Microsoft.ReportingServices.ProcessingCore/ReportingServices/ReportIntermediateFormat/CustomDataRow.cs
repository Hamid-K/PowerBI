using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004AB RID: 1195
	[Serializable]
	internal sealed class CustomDataRow : Row
	{
		// Token: 0x06003AB6 RID: 15030 RVA: 0x000FE904 File Offset: 0x000FCB04
		internal CustomDataRow()
		{
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000FE90C File Offset: 0x000FCB0C
		internal CustomDataRow(int id)
			: base(id)
		{
		}

		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x000FE915 File Offset: 0x000FCB15
		internal override CellList Cells
		{
			get
			{
				return this.m_cells;
			}
		}

		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x000FE91D File Offset: 0x000FCB1D
		// (set) Token: 0x06003ABA RID: 15034 RVA: 0x000FE925 File Offset: 0x000FCB25
		internal DataCellList DataCells
		{
			get
			{
				return this.m_cells;
			}
			set
			{
				this.m_cells = value;
			}
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000FE930 File Offset: 0x000FCB30
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CustomDataRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Row, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Cells, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataCell)
			});
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000FE968 File Offset: 0x000FCB68
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(CustomDataRow.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Cells)
				{
					writer.Write(this.m_cells);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000FE9C0 File Offset: 0x000FCBC0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(CustomDataRow.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Cells)
				{
					this.m_cells = reader.ReadListOfRIFObjects<DataCellList>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x000FEA15 File Offset: 0x000FCC15
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000FEA22 File Offset: 0x000FCC22
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CustomDataRow;
		}

		// Token: 0x04001BF2 RID: 7154
		private DataCellList m_cells;

		// Token: 0x04001BF3 RID: 7155
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = CustomDataRow.GetDeclaration();
	}
}
