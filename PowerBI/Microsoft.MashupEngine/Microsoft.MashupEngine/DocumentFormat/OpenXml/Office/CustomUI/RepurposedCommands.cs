using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002299 RID: 8857
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RepurposedCommand))]
	internal class RepurposedCommands : OpenXmlCompositeElement
	{
		// Token: 0x170040DB RID: 16603
		// (get) Token: 0x0600F007 RID: 61447 RVA: 0x002D0606 File Offset: 0x002CE806
		public override string LocalName
		{
			get
			{
				return "commands";
			}
		}

		// Token: 0x170040DC RID: 16604
		// (get) Token: 0x0600F008 RID: 61448 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170040DD RID: 16605
		// (get) Token: 0x0600F009 RID: 61449 RVA: 0x002D060D File Offset: 0x002CE80D
		internal override int ElementTypeId
		{
			get
			{
				return 12615;
			}
		}

		// Token: 0x0600F00A RID: 61450 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F00B RID: 61451 RVA: 0x00293ECF File Offset: 0x002920CF
		public RepurposedCommands()
		{
		}

		// Token: 0x0600F00C RID: 61452 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RepurposedCommands(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F00D RID: 61453 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RepurposedCommands(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F00E RID: 61454 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RepurposedCommands(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F00F RID: 61455 RVA: 0x002D0614 File Offset: 0x002CE814
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "command" == name)
			{
				return new RepurposedCommand();
			}
			return null;
		}

		// Token: 0x0600F010 RID: 61456 RVA: 0x002D062F File Offset: 0x002CE82F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RepurposedCommands>(deep);
		}

		// Token: 0x0400704F RID: 28751
		private const string tagName = "commands";

		// Token: 0x04007050 RID: 28752
		private const byte tagNsId = 34;

		// Token: 0x04007051 RID: 28753
		internal const int ElementTypeIdConst = 12615;
	}
}
