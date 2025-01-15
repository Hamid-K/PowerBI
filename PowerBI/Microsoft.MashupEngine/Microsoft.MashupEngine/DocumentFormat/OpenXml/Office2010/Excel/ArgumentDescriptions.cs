using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002410 RID: 9232
	[ChildElementInfo(typeof(ArgumentDescription), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArgumentDescriptions : OpenXmlCompositeElement
	{
		// Token: 0x17004EEA RID: 20202
		// (get) Token: 0x06010E64 RID: 69220 RVA: 0x002E874A File Offset: 0x002E694A
		public override string LocalName
		{
			get
			{
				return "argumentDescriptions";
			}
		}

		// Token: 0x17004EEB RID: 20203
		// (get) Token: 0x06010E65 RID: 69221 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EEC RID: 20204
		// (get) Token: 0x06010E66 RID: 69222 RVA: 0x002E8751 File Offset: 0x002E6951
		internal override int ElementTypeId
		{
			get
			{
				return 12950;
			}
		}

		// Token: 0x06010E67 RID: 69223 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004EED RID: 20205
		// (get) Token: 0x06010E68 RID: 69224 RVA: 0x002E8758 File Offset: 0x002E6958
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArgumentDescriptions.attributeTagNames;
			}
		}

		// Token: 0x17004EEE RID: 20206
		// (get) Token: 0x06010E69 RID: 69225 RVA: 0x002E875F File Offset: 0x002E695F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArgumentDescriptions.attributeNamespaceIds;
			}
		}

		// Token: 0x17004EEF RID: 20207
		// (get) Token: 0x06010E6A RID: 69226 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010E6B RID: 69227 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010E6C RID: 69228 RVA: 0x00293ECF File Offset: 0x002920CF
		public ArgumentDescriptions()
		{
		}

		// Token: 0x06010E6D RID: 69229 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ArgumentDescriptions(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010E6E RID: 69230 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ArgumentDescriptions(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010E6F RID: 69231 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ArgumentDescriptions(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010E70 RID: 69232 RVA: 0x002E8766 File Offset: 0x002E6966
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "argumentDescription" == name)
			{
				return new ArgumentDescription();
			}
			return null;
		}

		// Token: 0x06010E71 RID: 69233 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010E72 RID: 69234 RVA: 0x002E8781 File Offset: 0x002E6981
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArgumentDescriptions>(deep);
		}

		// Token: 0x06010E73 RID: 69235 RVA: 0x002E878C File Offset: 0x002E698C
		// Note: this type is marked as 'beforefieldinit'.
		static ArgumentDescriptions()
		{
			byte[] array = new byte[1];
			ArgumentDescriptions.attributeNamespaceIds = array;
		}

		// Token: 0x040076C9 RID: 30409
		private const string tagName = "argumentDescriptions";

		// Token: 0x040076CA RID: 30410
		private const byte tagNsId = 53;

		// Token: 0x040076CB RID: 30411
		internal const int ElementTypeIdConst = 12950;

		// Token: 0x040076CC RID: 30412
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x040076CD RID: 30413
		private static byte[] attributeNamespaceIds;
	}
}
