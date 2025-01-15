using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E28 RID: 11816
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TabStop))]
	internal class Tabs : OpenXmlCompositeElement
	{
		// Token: 0x17008940 RID: 35136
		// (get) Token: 0x06019144 RID: 102724 RVA: 0x002D05BD File Offset: 0x002CE7BD
		public override string LocalName
		{
			get
			{
				return "tabs";
			}
		}

		// Token: 0x17008941 RID: 35137
		// (get) Token: 0x06019145 RID: 102725 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008942 RID: 35138
		// (get) Token: 0x06019146 RID: 102726 RVA: 0x003461A6 File Offset: 0x003443A6
		internal override int ElementTypeId
		{
			get
			{
				return 11502;
			}
		}

		// Token: 0x06019147 RID: 102727 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019148 RID: 102728 RVA: 0x00293ECF File Offset: 0x002920CF
		public Tabs()
		{
		}

		// Token: 0x06019149 RID: 102729 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Tabs(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601914A RID: 102730 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Tabs(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601914B RID: 102731 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Tabs(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601914C RID: 102732 RVA: 0x003461AD File Offset: 0x003443AD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "tab" == name)
			{
				return new TabStop();
			}
			return null;
		}

		// Token: 0x0601914D RID: 102733 RVA: 0x003461C8 File Offset: 0x003443C8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tabs>(deep);
		}

		// Token: 0x0400A6EB RID: 42731
		private const string tagName = "tabs";

		// Token: 0x0400A6EC RID: 42732
		private const byte tagNsId = 23;

		// Token: 0x0400A6ED RID: 42733
		internal const int ElementTypeIdConst = 11502;
	}
}
