using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004C9 RID: 1225
	internal sealed class ScopedFieldInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x0010A395 File Offset: 0x00108595
		// (set) Token: 0x06003E39 RID: 15929 RVA: 0x0010A39D File Offset: 0x0010859D
		public int FieldIndex
		{
			get
			{
				return this.m_fieldIndex;
			}
			set
			{
				this.m_fieldIndex = value;
			}
		}

		// Token: 0x17001A76 RID: 6774
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x0010A3A6 File Offset: 0x001085A6
		// (set) Token: 0x06003E3B RID: 15931 RVA: 0x0010A3AE File Offset: 0x001085AE
		public string FieldName
		{
			get
			{
				return this.m_fieldName;
			}
			set
			{
				this.m_fieldName = value;
			}
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x0010A3B8 File Offset: 0x001085B8
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (ScopedFieldInfo.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopedFieldInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.FieldIndex, Token.Int32, Lifetime.AddedIn(200))
				});
			}
			return ScopedFieldInfo.m_declaration;
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x0010A404 File Offset: 0x00108604
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ScopedFieldInfo.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.FieldIndex)
				{
					writer.Write(this.m_fieldIndex);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x0010A458 File Offset: 0x00108658
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ScopedFieldInfo.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.FieldIndex)
				{
					this.m_fieldIndex = reader.ReadInt32();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x0010A4A9 File Offset: 0x001086A9
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x0010A4B6 File Offset: 0x001086B6
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopedFieldInfo;
		}

		// Token: 0x04001CFE RID: 7422
		private int m_fieldIndex;

		// Token: 0x04001CFF RID: 7423
		[NonSerialized]
		private string m_fieldName;

		// Token: 0x04001D00 RID: 7424
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = ScopedFieldInfo.GetDeclaration();
	}
}
