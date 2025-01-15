using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x02002307 RID: 8967
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BackstageGroup), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TaskGroup), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SimpleGroups : OpenXmlCompositeElement
	{
		// Token: 0x170047CF RID: 18383
		// (get) Token: 0x0600FED4 RID: 65236 RVA: 0x002DD76F File Offset: 0x002DB96F
		public override string LocalName
		{
			get
			{
				return "secondColumn";
			}
		}

		// Token: 0x170047D0 RID: 18384
		// (get) Token: 0x0600FED5 RID: 65237 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170047D1 RID: 18385
		// (get) Token: 0x0600FED6 RID: 65238 RVA: 0x002DD776 File Offset: 0x002DB976
		internal override int ElementTypeId
		{
			get
			{
				return 13109;
			}
		}

		// Token: 0x0600FED7 RID: 65239 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FED8 RID: 65240 RVA: 0x00293ECF File Offset: 0x002920CF
		public SimpleGroups()
		{
		}

		// Token: 0x0600FED9 RID: 65241 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SimpleGroups(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FEDA RID: 65242 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SimpleGroups(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FEDB RID: 65243 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SimpleGroups(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FEDC RID: 65244 RVA: 0x002DD77D File Offset: 0x002DB97D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "group" == name)
			{
				return new BackstageGroup();
			}
			if (57 == namespaceId && "taskGroup" == name)
			{
				return new TaskGroup();
			}
			return null;
		}

		// Token: 0x0600FEDD RID: 65245 RVA: 0x002DD7B0 File Offset: 0x002DB9B0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SimpleGroups>(deep);
		}

		// Token: 0x04007233 RID: 29235
		private const string tagName = "secondColumn";

		// Token: 0x04007234 RID: 29236
		private const byte tagNsId = 57;

		// Token: 0x04007235 RID: 29237
		internal const int ElementTypeIdConst = 13109;
	}
}
