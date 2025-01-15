using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022ED RID: 8941
	[ChildElementInfo(typeof(Separator), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ControlCloneQat), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ButtonRegular), FileFormatVersions.Office2010)]
	internal abstract class QatItemsType : OpenXmlCompositeElement
	{
		// Token: 0x0600FC8B RID: 64651 RVA: 0x002DBA64 File Offset: 0x002D9C64
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "control" == name)
			{
				return new ControlCloneQat();
			}
			if (57 == namespaceId && "button" == name)
			{
				return new ButtonRegular();
			}
			if (57 == namespaceId && "separator" == name)
			{
				return new Separator();
			}
			return null;
		}

		// Token: 0x0600FC8C RID: 64652 RVA: 0x00293ECF File Offset: 0x002920CF
		protected QatItemsType()
		{
		}

		// Token: 0x0600FC8D RID: 64653 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected QatItemsType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FC8E RID: 64654 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected QatItemsType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FC8F RID: 64655 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected QatItemsType(string outerXml)
			: base(outerXml)
		{
		}
	}
}
