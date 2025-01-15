using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004F3 RID: 1267
	[SkipStaticValidation]
	internal class ParameterImplWrapper : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600406D RID: 16493 RVA: 0x0010FDFD File Offset: 0x0010DFFD
		internal ParameterImplWrapper()
		{
			this.m_odpParameter = new ParameterImpl();
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x0010FE10 File Offset: 0x0010E010
		internal ParameterImplWrapper(ParameterImpl odpParameter)
		{
			this.m_odpParameter = odpParameter;
		}

		// Token: 0x17001B2A RID: 6954
		// (get) Token: 0x0600406F RID: 16495 RVA: 0x0010FE1F File Offset: 0x0010E01F
		// (set) Token: 0x06004070 RID: 16496 RVA: 0x0010FE27 File Offset: 0x0010E027
		internal ParameterImpl WrappedParameterImpl
		{
			get
			{
				return this.m_odpParameter;
			}
			set
			{
				this.m_odpParameter = value;
			}
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x0010FE30 File Offset: 0x0010E030
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Parameter, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.Object),
				new MemberInfo(MemberName.Label, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.String),
				new MemberInfo(MemberName.IsMultiValue, Token.Boolean),
				new MemberInfo(MemberName.Prompt, Token.String),
				new MemberInfo(MemberName.IsUserSupplied, Token.Boolean)
			});
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x0010FEB4 File Offset: 0x0010E0B4
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ParameterImplWrapper.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Label)
				{
					if (memberName == MemberName.Value)
					{
						writer.Write(this.m_odpParameter.GetValues());
						continue;
					}
					if (memberName == MemberName.Label)
					{
						writer.Write(this.m_odpParameter.GetLabels());
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Prompt)
					{
						writer.Write(this.m_odpParameter.Prompt);
						continue;
					}
					if (memberName == MemberName.IsUserSupplied)
					{
						writer.Write(this.m_odpParameter.IsUserSupplied);
						continue;
					}
					if (memberName == MemberName.IsMultiValue)
					{
						writer.Write(this.m_odpParameter.IsMultiValue);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x0010FF8C File Offset: 0x0010E18C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ParameterImplWrapper.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Label)
				{
					if (memberName == MemberName.Value)
					{
						this.m_odpParameter.SetValues(reader.ReadVariantArray());
						continue;
					}
					if (memberName == MemberName.Label)
					{
						this.m_odpParameter.SetLabels(reader.ReadStringArray());
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Prompt)
					{
						this.m_odpParameter.SetPrompt(reader.ReadString());
						continue;
					}
					if (memberName == MemberName.IsUserSupplied)
					{
						this.m_odpParameter.SetIsUserSupplied(reader.ReadBoolean());
						continue;
					}
					if (memberName == MemberName.IsMultiValue)
					{
						this.m_odpParameter.SetIsMultiValue(reader.ReadBoolean());
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x00110063 File Offset: 0x0010E263
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x00110070 File Offset: 0x0010E270
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Parameter;
		}

		// Token: 0x04001D95 RID: 7573
		private ParameterImpl m_odpParameter;

		// Token: 0x04001D96 RID: 7574
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ParameterImplWrapper.GetDeclaration();
	}
}
