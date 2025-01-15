using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004A4 RID: 1188
	[Serializable]
	internal struct CodeClass : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003A57 RID: 14935 RVA: 0x000FD4DC File Offset: 0x000FB6DC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CodeClass, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ClassName, Token.String),
				new MemberInfo(MemberName.InstanceName, Token.String)
			});
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x000FD528 File Offset: 0x000FB728
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(CodeClass.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ClassName)
				{
					if (memberName != MemberName.InstanceName)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.InstanceName);
					}
				}
				else
				{
					writer.Write(this.ClassName);
				}
			}
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x000FD594 File Offset: 0x000FB794
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(CodeClass.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ClassName)
				{
					if (memberName != MemberName.InstanceName)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.InstanceName = reader.ReadString();
					}
				}
				else
				{
					this.ClassName = reader.ReadString();
				}
			}
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000FD600 File Offset: 0x000FB800
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000FD60D File Offset: 0x000FB80D
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CodeClass;
		}

		// Token: 0x04001BDC RID: 7132
		internal string ClassName;

		// Token: 0x04001BDD RID: 7133
		internal string InstanceName;

		// Token: 0x04001BDE RID: 7134
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = CodeClass.GetDeclaration();
	}
}
