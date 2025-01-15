using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000478 RID: 1144
	[Serializable]
	internal sealed class LabelData : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000E777D File Offset: 0x000E597D
		// (set) Token: 0x060034A0 RID: 13472 RVA: 0x000E7785 File Offset: 0x000E5985
		internal string DataSetName
		{
			get
			{
				return this.m_dataSetName;
			}
			set
			{
				this.m_dataSetName = value;
			}
		}

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x060034A1 RID: 13473 RVA: 0x000E778E File Offset: 0x000E598E
		// (set) Token: 0x060034A2 RID: 13474 RVA: 0x000E7796 File Offset: 0x000E5996
		internal List<string> KeyFields
		{
			get
			{
				return this.m_keyFields;
			}
			set
			{
				this.m_keyFields = value;
			}
		}

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x060034A3 RID: 13475 RVA: 0x000E779F File Offset: 0x000E599F
		// (set) Token: 0x060034A4 RID: 13476 RVA: 0x000E77A7 File Offset: 0x000E59A7
		internal string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000E77B0 File Offset: 0x000E59B0
		internal void Initialize(Tablix tablix, InitializationContext context)
		{
			context.ValidateSliderLabelData(tablix, this);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000E77BC File Offset: 0x000E59BC
		[SkipMemberStaticValidation(MemberName.Key)]
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LabelData, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataSetName, Token.String),
				new MemberInfo(MemberName.Key, Token.String, Lifetime.RemovedIn(200)),
				new MemberInfo(MemberName.Label, Token.String),
				new MemberInfo(MemberName.KeyFields, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String, Lifetime.AddedIn(200))
			});
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000E7844 File Offset: 0x000E5A44
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(LabelData.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Label)
				{
					if (memberName == MemberName.Key)
					{
						writer.Write(this.m_keyFields[0]);
						continue;
					}
					if (memberName == MemberName.Label)
					{
						writer.Write(this.m_label);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataSetName)
					{
						writer.Write(this.m_dataSetName);
						continue;
					}
					if (memberName == MemberName.KeyFields)
					{
						writer.WriteListOfPrimitives<string>(this.m_keyFields);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000E78F4 File Offset: 0x000E5AF4
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(LabelData.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Label)
				{
					if (memberName == MemberName.Key)
					{
						string text = reader.ReadString();
						this.m_keyFields = new List<string>(1);
						this.m_keyFields.Add(text);
						continue;
					}
					if (memberName == MemberName.Label)
					{
						this.m_label = reader.ReadString();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataSetName)
					{
						this.m_dataSetName = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.KeyFields)
					{
						this.m_keyFields = reader.ReadListOfPrimitives<string>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000E79AE File Offset: 0x000E5BAE
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000E79B0 File Offset: 0x000E5BB0
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LabelData;
		}

		// Token: 0x04001A11 RID: 6673
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = LabelData.GetDeclaration();

		// Token: 0x04001A12 RID: 6674
		private string m_dataSetName;

		// Token: 0x04001A13 RID: 6675
		private List<string> m_keyFields;

		// Token: 0x04001A14 RID: 6676
		private string m_label;
	}
}
