using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000521 RID: 1313
	internal sealed class TablixRow : Row, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060046C5 RID: 18117 RVA: 0x001296B2 File Offset: 0x001278B2
		internal TablixRow()
		{
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x001296BA File Offset: 0x001278BA
		internal TablixRow(int id)
			: base(id)
		{
		}

		// Token: 0x17001D80 RID: 7552
		// (get) Token: 0x060046C7 RID: 18119 RVA: 0x001296C3 File Offset: 0x001278C3
		// (set) Token: 0x060046C8 RID: 18120 RVA: 0x001296CB File Offset: 0x001278CB
		internal string Height
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

		// Token: 0x17001D81 RID: 7553
		// (get) Token: 0x060046C9 RID: 18121 RVA: 0x001296D4 File Offset: 0x001278D4
		// (set) Token: 0x060046CA RID: 18122 RVA: 0x001296DC File Offset: 0x001278DC
		internal double HeightValue
		{
			get
			{
				return this.m_heightValue;
			}
			set
			{
				this.m_heightValue = value;
			}
		}

		// Token: 0x17001D82 RID: 7554
		// (get) Token: 0x060046CB RID: 18123 RVA: 0x001296E5 File Offset: 0x001278E5
		internal override CellList Cells
		{
			get
			{
				return this.m_cells;
			}
		}

		// Token: 0x17001D83 RID: 7555
		// (get) Token: 0x060046CC RID: 18124 RVA: 0x001296ED File Offset: 0x001278ED
		// (set) Token: 0x060046CD RID: 18125 RVA: 0x001296F5 File Offset: 0x001278F5
		internal TablixCellList TablixCells
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

		// Token: 0x17001D84 RID: 7556
		// (get) Token: 0x060046CE RID: 18126 RVA: 0x001296FE File Offset: 0x001278FE
		// (set) Token: 0x060046CF RID: 18127 RVA: 0x00129706 File Offset: 0x00127906
		internal bool ForAutoSubtotal
		{
			get
			{
				return this.m_forAutoSubtotal;
			}
			set
			{
				this.m_forAutoSubtotal = value;
			}
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x0012970F File Offset: 0x0012790F
		internal override void Initialize(InitializationContext context)
		{
			this.m_heightValue = context.ValidateSize(this.m_height, "Height");
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x0012972C File Offset: 0x0012792C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			TablixRow tablixRow = (TablixRow)base.PublishClone(context);
			if (this.m_height != null)
			{
				tablixRow.m_height = (string)this.m_height.Clone();
			}
			if (this.m_cells != null)
			{
				tablixRow.m_cells = new TablixCellList(this.m_cells.Count);
				foreach (object obj in this.m_cells)
				{
					TablixCell tablixCell = (TablixCell)obj;
					tablixRow.m_cells.Add((TablixCell)tablixCell.PublishClone(context));
				}
			}
			return tablixRow;
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x001297E0 File Offset: 0x001279E0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Row, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Height, Token.String),
				new MemberInfo(MemberName.HeightValue, Token.Double),
				new MemberInfo(MemberName.TablixCells, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixCell)
			});
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x00129844 File Offset: 0x00127A44
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(TablixRow.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Height)
				{
					if (memberName != MemberName.HeightValue)
					{
						if (memberName != MemberName.TablixCells)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_cells);
						}
					}
					else
					{
						writer.Write(this.m_heightValue);
					}
				}
				else
				{
					writer.Write(this.m_height);
				}
			}
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x001298D0 File Offset: 0x00127AD0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(TablixRow.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Height)
				{
					if (memberName != MemberName.HeightValue)
					{
						if (memberName != MemberName.TablixCells)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_cells = reader.ReadListOfRIFObjects<TablixCellList>();
						}
					}
					else
					{
						this.m_heightValue = reader.ReadDouble();
					}
				}
				else
				{
					this.m_height = reader.ReadString();
				}
			}
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x0012995A File Offset: 0x00127B5A
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x00129967 File Offset: 0x00127B67
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixRow;
		}

		// Token: 0x04001FB5 RID: 8117
		private string m_height;

		// Token: 0x04001FB6 RID: 8118
		private double m_heightValue;

		// Token: 0x04001FB7 RID: 8119
		private TablixCellList m_cells;

		// Token: 0x04001FB8 RID: 8120
		[NonSerialized]
		private bool m_forAutoSubtotal;

		// Token: 0x04001FB9 RID: 8121
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = TablixRow.GetDeclaration();
	}
}
