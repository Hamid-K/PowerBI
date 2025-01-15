using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000523 RID: 1315
	internal abstract class TablixCellBase : Cell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060046DB RID: 18139 RVA: 0x00129999 File Offset: 0x00127B99
		internal TablixCellBase()
		{
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x001299A1 File Offset: 0x00127BA1
		internal TablixCellBase(int id, DataRegion dataRegion)
			: base(id, dataRegion)
		{
		}

		// Token: 0x17001D86 RID: 7558
		// (get) Token: 0x060046DD RID: 18141 RVA: 0x001299AB File Offset: 0x00127BAB
		// (set) Token: 0x060046DE RID: 18142 RVA: 0x001299B3 File Offset: 0x00127BB3
		internal int ColSpan
		{
			get
			{
				return this.m_colSpan;
			}
			set
			{
				this.m_colSpan = value;
			}
		}

		// Token: 0x17001D87 RID: 7559
		// (get) Token: 0x060046DF RID: 18143 RVA: 0x001299BC File Offset: 0x00127BBC
		// (set) Token: 0x060046E0 RID: 18144 RVA: 0x001299C4 File Offset: 0x00127BC4
		internal int RowSpan
		{
			get
			{
				return this.m_rowSpan;
			}
			set
			{
				this.m_rowSpan = value;
			}
		}

		// Token: 0x17001D88 RID: 7560
		// (get) Token: 0x060046E1 RID: 18145 RVA: 0x001299CD File Offset: 0x00127BCD
		// (set) Token: 0x060046E2 RID: 18146 RVA: 0x001299D5 File Offset: 0x00127BD5
		internal ReportItem CellContents
		{
			get
			{
				return this.m_cellContents;
			}
			set
			{
				this.m_cellContents = value;
			}
		}

		// Token: 0x17001D89 RID: 7561
		// (get) Token: 0x060046E3 RID: 18147 RVA: 0x001299DE File Offset: 0x00127BDE
		// (set) Token: 0x060046E4 RID: 18148 RVA: 0x001299E6 File Offset: 0x00127BE6
		internal ReportItem AltCellContents
		{
			get
			{
				return this.m_altCellContents;
			}
			set
			{
				this.m_altCellContents = value;
			}
		}

		// Token: 0x17001D8A RID: 7562
		// (get) Token: 0x060046E5 RID: 18149 RVA: 0x001299F0 File Offset: 0x00127BF0
		internal override List<ReportItem> CellContentCollection
		{
			get
			{
				if (this.m_cellContentCollection == null && this.m_hasInnerGroupTreeHierarchy && this.m_cellContents != null)
				{
					this.m_cellContentCollection = new List<ReportItem>((this.m_altCellContents == null) ? 1 : 2);
					if (this.m_cellContents != null)
					{
						this.m_cellContentCollection.Add(this.m_cellContents);
					}
					if (this.m_altCellContents != null)
					{
						this.m_cellContentCollection.Add(this.m_altCellContents);
					}
				}
				return this.m_cellContentCollection;
			}
		}

		// Token: 0x17001D8B RID: 7563
		// (get) Token: 0x060046E6 RID: 18150 RVA: 0x00129A64 File Offset: 0x00127C64
		public override Microsoft.ReportingServices.ReportProcessing.ObjectType DataScopeObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.TablixCell;
			}
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x00129A68 File Offset: 0x00127C68
		protected override void TraverseNestedScopes(IRIFScopeVisitor visitor)
		{
			if (this.m_cellContents != null)
			{
				this.m_cellContents.TraverseScopes(visitor);
			}
			if (this.m_altCellContents != null)
			{
				this.m_altCellContents.TraverseScopes(visitor);
			}
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x00129A94 File Offset: 0x00127C94
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			TablixCellBase tablixCellBase = (TablixCellBase)base.PublishClone(context);
			if (this.m_cellContents != null)
			{
				tablixCellBase.m_cellContents = (ReportItem)this.m_cellContents.PublishClone(context);
				if (this.m_altCellContents != null)
				{
					Global.Tracer.Assert(tablixCellBase.m_cellContents is CustomReportItem);
					tablixCellBase.m_altCellContents = (ReportItem)this.m_altCellContents.PublishClone(context);
					((CustomReportItem)tablixCellBase.m_cellContents).AltReportItem = tablixCellBase.m_altCellContents;
				}
			}
			return tablixCellBase;
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x00129B1C File Offset: 0x00127D1C
		internal override void InternalInitialize(int parentRowID, int parentColumnID, int rowindex, int colIndex, InitializationContext context)
		{
			if (this.m_cellContents != null)
			{
				if (this.IsDataRegionBodyCell)
				{
					context.IsTopLevelCellContents = true;
				}
				this.m_cellContents.Initialize(context);
				this.DataRendererInitialize(context);
				if (this.m_altCellContents != null)
				{
					this.m_altCellContents.Initialize(context);
				}
				this.m_hasInnerGroupTreeHierarchy = Cell.ContainsInnerGroupTreeHierarchy(this.m_cellContents) | Cell.ContainsInnerGroupTreeHierarchy(this.m_altCellContents);
			}
		}

		// Token: 0x17001D8C RID: 7564
		// (get) Token: 0x060046EA RID: 18154 RVA: 0x00129B8A File Offset: 0x00127D8A
		protected override Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode ExprHostDataRegionMode
		{
			get
			{
				return Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.Tablix;
			}
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x00129B8D File Offset: 0x00127D8D
		internal virtual void DataRendererInitialize(InitializationContext context)
		{
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x00129B90 File Offset: 0x00127D90
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixCellBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Cell, new List<MemberInfo>
			{
				new MemberInfo(MemberName.RowSpan, Token.Int32),
				new MemberInfo(MemberName.ColSpan, Token.Int32),
				new MemberInfo(MemberName.CellContents, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem),
				new MemberInfo(MemberName.AltCellContents, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem)
			});
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x00129C08 File Offset: 0x00127E08
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(TablixCellBase.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ColSpan)
				{
					if (memberName == MemberName.CellContents)
					{
						writer.Write(this.m_cellContents);
						continue;
					}
					if (memberName == MemberName.ColSpan)
					{
						writer.Write(this.m_colSpan);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.RowSpan)
					{
						writer.Write(this.m_rowSpan);
						continue;
					}
					if (memberName == MemberName.AltCellContents)
					{
						writer.Write(this.m_altCellContents);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x00129CBC File Offset: 0x00127EBC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(TablixCellBase.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ColSpan)
				{
					if (memberName == MemberName.CellContents)
					{
						this.m_cellContents = (ReportItem)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ColSpan)
					{
						this.m_colSpan = reader.ReadInt32();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.RowSpan)
					{
						this.m_rowSpan = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.AltCellContents)
					{
						this.m_altCellContents = (ReportItem)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x00129D77 File Offset: 0x00127F77
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x00129D81 File Offset: 0x00127F81
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixCellBase;
		}

		// Token: 0x04001FBA RID: 8122
		protected int m_rowSpan;

		// Token: 0x04001FBB RID: 8123
		protected int m_colSpan;

		// Token: 0x04001FBC RID: 8124
		protected ReportItem m_cellContents;

		// Token: 0x04001FBD RID: 8125
		protected ReportItem m_altCellContents;

		// Token: 0x04001FBE RID: 8126
		[NonSerialized]
		private List<ReportItem> m_cellContentCollection;

		// Token: 0x04001FBF RID: 8127
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = TablixCellBase.GetDeclaration();
	}
}
