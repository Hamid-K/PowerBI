using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000525 RID: 1317
	internal sealed class TablixCornerCell : TablixCellBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600470C RID: 18188 RVA: 0x0012A0D1 File Offset: 0x001282D1
		internal TablixCornerCell()
		{
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x0012A0D9 File Offset: 0x001282D9
		internal TablixCornerCell(int id, DataRegion dataRegion)
			: base(id, dataRegion)
		{
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x0012A0E3 File Offset: 0x001282E3
		internal override void InternalInitialize(int parentRowID, int parentColumnID, int rowindex, int colIndex, InitializationContext context)
		{
			base.InternalInitialize(parentRowID, parentColumnID, rowindex, colIndex, context);
			this.m_hasInnerGroupTreeHierarchy = Cell.ContainsInnerGroupTreeHierarchy(this.m_cellContents) | Cell.ContainsInnerGroupTreeHierarchy(this.m_altCellContents);
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x0012A10F File Offset: 0x0012830F
		protected override void StartExprHost(InitializationContext context)
		{
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x0012A111 File Offset: 0x00128311
		protected override void EndExprHost(InitializationContext context)
		{
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x0012A113 File Offset: 0x00128313
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			return (TablixCornerCell)base.PublishClone(context);
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x0012A124 File Offset: 0x00128324
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixCornerCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixCellBase, list);
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x0012A147 File Offset: 0x00128347
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(TablixCornerCell.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x0012A17F File Offset: 0x0012837F
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(TablixCornerCell.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x0012A1B7 File Offset: 0x001283B7
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x0012A1C1 File Offset: 0x001283C1
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixCornerCell;
		}

		// Token: 0x04001FC7 RID: 8135
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = TablixCornerCell.GetDeclaration();
	}
}
