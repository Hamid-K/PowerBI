using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000725 RID: 1829
	[Serializable]
	internal sealed class TextBoxInstance : ReportItemInstance
	{
		// Token: 0x060065F5 RID: 26101 RVA: 0x00190A17 File Offset: 0x0018EC17
		internal TextBoxInstance(ReportProcessing.ProcessingContext pc, TextBox reportItemDef, int index)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			if (reportItemDef.IsSimpleTextBox())
			{
				this.m_instanceInfo = new SimpleTextBoxInstanceInfo(pc, reportItemDef, this, index);
				return;
			}
			this.m_instanceInfo = new TextBoxInstanceInfo(pc, reportItemDef, this, index);
		}

		// Token: 0x060065F6 RID: 26102 RVA: 0x00190A4D File Offset: 0x0018EC4D
		internal TextBoxInstance()
		{
		}

		// Token: 0x17002413 RID: 9235
		// (get) Token: 0x060065F7 RID: 26103 RVA: 0x00190A58 File Offset: 0x0018EC58
		internal InstanceInfo InstanceInfo
		{
			get
			{
				if (this.m_instanceInfo is OffsetInfo)
				{
					Global.Tracer.Assert(false, string.Empty);
					return null;
				}
				if (this.m_instanceInfo is SimpleTextBoxInstanceInfo)
				{
					return (SimpleTextBoxInstanceInfo)this.m_instanceInfo;
				}
				return (TextBoxInstanceInfo)this.m_instanceInfo;
			}
		}

		// Token: 0x060065F8 RID: 26104 RVA: 0x00190AA8 File Offset: 0x0018ECA8
		internal SimpleTextBoxInstanceInfo UpgradeToSimpleTextbox(TextBoxInstanceInfo instanceInfo, out bool isSimple)
		{
			isSimple = false;
			TextBox textBox = base.ReportItemDef as TextBox;
			if (textBox.IsSimpleTextBox())
			{
				isSimple = true;
				SimpleTextBoxInstanceInfo simpleTextBoxInstanceInfo = new SimpleTextBoxInstanceInfo(textBox, instanceInfo);
				this.m_instanceInfo = simpleTextBoxInstanceInfo;
				return simpleTextBoxInstanceInfo;
			}
			return null;
		}

		// Token: 0x060065F9 RID: 26105 RVA: 0x00190AE4 File Offset: 0x0018ECE4
		internal new static Declaration GetDeclaration()
		{
			MemberInfoList memberInfoList = new MemberInfoList();
			return new Declaration(ObjectType.ReportItemInstance, memberInfoList);
		}

		// Token: 0x060065FA RID: 26106 RVA: 0x00190B00 File Offset: 0x0018ED00
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			if (((TextBox)this.m_reportItemDef).IsSimpleTextBox(reader.IntermediateFormatVersion))
			{
				return null;
			}
			return reader.ReadTextBoxInstanceInfo((TextBox)this.m_reportItemDef);
		}

		// Token: 0x060065FB RID: 26107 RVA: 0x00190B50 File Offset: 0x0018ED50
		internal SimpleTextBoxInstanceInfo GetSimpleInstanceInfo(ChunkManager.RenderingChunkManager chunkManager, bool inPageSection)
		{
			Global.Tracer.Assert(((TextBox)this.m_reportItemDef).IsSimpleTextBox());
			if (this.m_instanceInfo is OffsetInfo)
			{
				Global.Tracer.Assert(chunkManager != null);
				IntermediateFormatReader intermediateFormatReader;
				if (inPageSection)
				{
					intermediateFormatReader = chunkManager.GetPageSectionInstanceReader(((OffsetInfo)this.m_instanceInfo).Offset);
				}
				else
				{
					intermediateFormatReader = chunkManager.GetReader(((OffsetInfo)this.m_instanceInfo).Offset);
				}
				return intermediateFormatReader.ReadSimpleTextBoxInstanceInfo((TextBox)this.m_reportItemDef);
			}
			return (SimpleTextBoxInstanceInfo)this.m_instanceInfo;
		}
	}
}
