using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000479 RID: 1145
	internal sealed class BandNavigationCell : TablixCellBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060034AD RID: 13485 RVA: 0x000E79CB File Offset: 0x000E5BCB
		internal BandNavigationCell()
		{
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000E79D3 File Offset: 0x000E5BD3
		internal BandNavigationCell(int id, DataRegion dataRegion)
			: base(id, dataRegion)
		{
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000E79DD File Offset: 0x000E5BDD
		internal void Initialize(InitializationContext context)
		{
			base.Initialize(0, 0, 0, 0, context);
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000E79EA File Offset: 0x000E5BEA
		protected override void StartExprHost(InitializationContext context)
		{
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000E79EC File Offset: 0x000E5BEC
		protected override void EndExprHost(InitializationContext context)
		{
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000E79EE File Offset: 0x000E5BEE
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			return (BandNavigationCell)base.PublishClone(context);
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x000E79FC File Offset: 0x000E5BFC
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BandNavigationCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixCellBase, list);
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000E7A1F File Offset: 0x000E5C1F
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(BandNavigationCell.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000E7A57 File Offset: 0x000E5C57
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(BandNavigationCell.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000E7A8F File Offset: 0x000E5C8F
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000E7A99 File Offset: 0x000E5C99
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BandNavigationCell;
		}

		// Token: 0x04001A15 RID: 6677
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = BandNavigationCell.GetDeclaration();
	}
}
