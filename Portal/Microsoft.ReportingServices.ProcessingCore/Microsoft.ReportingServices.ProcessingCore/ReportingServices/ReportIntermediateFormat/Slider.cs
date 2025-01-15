using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000477 RID: 1143
	[Serializable]
	internal sealed class Slider : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000E7602 File Offset: 0x000E5802
		// (set) Token: 0x06003494 RID: 13460 RVA: 0x000E760A File Offset: 0x000E580A
		internal bool Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000E7613 File Offset: 0x000E5813
		// (set) Token: 0x06003496 RID: 13462 RVA: 0x000E761B File Offset: 0x000E581B
		internal LabelData LabelData
		{
			get
			{
				return this.m_labelData;
			}
			set
			{
				this.m_labelData = value;
			}
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x000E7624 File Offset: 0x000E5824
		internal void Initialize(Tablix tablix, InitializationContext context)
		{
			if (this.m_labelData != null)
			{
				this.m_labelData.Initialize(tablix, context);
			}
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000E763C File Offset: 0x000E583C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Slider, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Hidden, Token.Boolean),
				new MemberInfo(MemberName.LabelData, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LabelData)
			});
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x000E7688 File Offset: 0x000E5888
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Slider.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Hidden)
				{
					if (memberName != MemberName.LabelData)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_labelData);
					}
				}
				else
				{
					writer.Write(this.m_hidden);
				}
			}
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000E76F4 File Offset: 0x000E58F4
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Slider.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Hidden)
				{
					if (memberName != MemberName.LabelData)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_labelData = reader.ReadRIFObject<LabelData>();
					}
				}
				else
				{
					this.m_hidden = reader.ReadBoolean();
				}
			}
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000E7760 File Offset: 0x000E5960
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000E7762 File Offset: 0x000E5962
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Slider;
		}

		// Token: 0x04001A0E RID: 6670
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Slider.GetDeclaration();

		// Token: 0x04001A0F RID: 6671
		private bool m_hidden;

		// Token: 0x04001A10 RID: 6672
		private LabelData m_labelData;
	}
}
