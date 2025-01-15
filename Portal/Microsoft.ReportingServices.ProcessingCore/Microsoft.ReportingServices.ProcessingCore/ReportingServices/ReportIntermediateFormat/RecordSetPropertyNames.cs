using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004F7 RID: 1271
	[Serializable]
	internal sealed class RecordSetPropertyNames : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060040A2 RID: 16546 RVA: 0x001108E2 File Offset: 0x0010EAE2
		internal RecordSetPropertyNames()
		{
		}

		// Token: 0x17001B39 RID: 6969
		// (get) Token: 0x060040A3 RID: 16547 RVA: 0x001108EA File Offset: 0x0010EAEA
		// (set) Token: 0x060040A4 RID: 16548 RVA: 0x001108F2 File Offset: 0x0010EAF2
		internal List<string> PropertyNames
		{
			get
			{
				return this.m_propertyNames;
			}
			set
			{
				this.m_propertyNames = value;
			}
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x001108FC File Offset: 0x0010EAFC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RecordSetPropertyNames, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.PropertyNames, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String)
			});
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x0011092C File Offset: 0x0010EB2C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RecordSetPropertyNames.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.PropertyNames)
				{
					writer.WriteListOfPrimitives<string>(this.m_propertyNames);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x0011097C File Offset: 0x0010EB7C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RecordSetPropertyNames.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.PropertyNames)
				{
					this.m_propertyNames = reader.ReadListOfPrimitives<string>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x001109C9 File Offset: 0x0010EBC9
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x001109D6 File Offset: 0x0010EBD6
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RecordSetPropertyNames;
		}

		// Token: 0x04001DA6 RID: 7590
		private List<string> m_propertyNames;

		// Token: 0x04001DA7 RID: 7591
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = RecordSetPropertyNames.GetDeclaration();
	}
}
