using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200047C RID: 1148
	public abstract class Row : IDOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600350A RID: 13578 RVA: 0x000E8B3A File Offset: 0x000E6D3A
		internal Row()
		{
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000E8B42 File Offset: 0x000E6D42
		internal Row(int id)
			: base(id)
		{
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x0600350C RID: 13580
		internal abstract CellList Cells { get; }

		// Token: 0x0600350D RID: 13581 RVA: 0x000E8B4B File Offset: 0x000E6D4B
		internal virtual void Initialize(InitializationContext context)
		{
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000E8B50 File Offset: 0x000E6D50
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Row, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, list);
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000E8B6F File Offset: 0x000E6D6F
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Row.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000E8BA7 File Offset: 0x000E6DA7
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Row.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000E8BDF File Offset: 0x000E6DDF
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x000E8BEC File Offset: 0x000E6DEC
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Row;
		}

		// Token: 0x04001A30 RID: 6704
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Row.GetDeclaration();
	}
}
