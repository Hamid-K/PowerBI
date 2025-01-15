using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000614 RID: 1556
	internal class RIFVariantContainer : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600555C RID: 21852 RVA: 0x001684A0 File Offset: 0x001666A0
		internal RIFVariantContainer()
		{
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x001684A8 File Offset: 0x001666A8
		internal RIFVariantContainer(object value)
		{
			this.m_value = value;
		}

		// Token: 0x17001F3E RID: 7998
		// (get) Token: 0x0600555E RID: 21854 RVA: 0x001684B7 File Offset: 0x001666B7
		internal object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x001684C0 File Offset: 0x001666C0
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFVariantContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Variant)
			});
		}

		// Token: 0x06005560 RID: 21856 RVA: 0x001684F4 File Offset: 0x001666F4
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RIFVariantContainer.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.Value)
				{
					writer.Write(this.m_value);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06005561 RID: 21857 RVA: 0x00168544 File Offset: 0x00166744
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RIFVariantContainer.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.Value)
				{
					this.m_value = reader.ReadVariant();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06005562 RID: 21858 RVA: 0x00168592 File Offset: 0x00166792
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x0016859F File Offset: 0x0016679F
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFVariantContainer;
		}

		// Token: 0x04002D5D RID: 11613
		private object m_value;

		// Token: 0x04002D5E RID: 11614
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = RIFVariantContainer.GetDeclaration();
	}
}
