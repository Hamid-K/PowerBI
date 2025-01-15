using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003BF RID: 959
	[Serializable]
	internal sealed class UpdatedVariableValues : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060026D3 RID: 9939 RVA: 0x000B9D22 File Offset: 0x000B7F22
		internal UpdatedVariableValues()
		{
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x000B9D2A File Offset: 0x000B7F2A
		// (set) Token: 0x060026D5 RID: 9941 RVA: 0x000B9D32 File Offset: 0x000B7F32
		internal Dictionary<int, object> VariableValues
		{
			get
			{
				return this.m_variableValues;
			}
			set
			{
				this.m_variableValues = value;
			}
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x000B9D3C File Offset: 0x000B7F3C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.UpdatedVariableValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.UpdatedVariableValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Int32SerializableDictionary, Token.Serializable)
			});
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x000B9D78 File Offset: 0x000B7F78
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(UpdatedVariableValues.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.UpdatedVariableValues)
				{
					writer.Int32SerializableDictionary(this.m_variableValues);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000B9DCC File Offset: 0x000B7FCC
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(UpdatedVariableValues.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.UpdatedVariableValues)
				{
					this.m_variableValues = reader.Int32SerializableDictionary();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000B9E1D File Offset: 0x000B801D
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000B9E2A File Offset: 0x000B802A
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.UpdatedVariableValues;
		}

		// Token: 0x0400165B RID: 5723
		private Dictionary<int, object> m_variableValues;

		// Token: 0x0400165C RID: 5724
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = UpdatedVariableValues.GetDeclaration();
	}
}
