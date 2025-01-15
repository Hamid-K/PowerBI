using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023D4 RID: 9172
	[ChildElementInfo(typeof(DataValidation), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DataValidations : OpenXmlCompositeElement
	{
		// Token: 0x17004D23 RID: 19747
		// (get) Token: 0x06010A71 RID: 68209 RVA: 0x002E5AF1 File Offset: 0x002E3CF1
		public override string LocalName
		{
			get
			{
				return "dataValidations";
			}
		}

		// Token: 0x17004D24 RID: 19748
		// (get) Token: 0x06010A72 RID: 68210 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D25 RID: 19749
		// (get) Token: 0x06010A73 RID: 68211 RVA: 0x002E5AF8 File Offset: 0x002E3CF8
		internal override int ElementTypeId
		{
			get
			{
				return 12898;
			}
		}

		// Token: 0x06010A74 RID: 68212 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004D26 RID: 19750
		// (get) Token: 0x06010A75 RID: 68213 RVA: 0x002E5AFF File Offset: 0x002E3CFF
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataValidations.attributeTagNames;
			}
		}

		// Token: 0x17004D27 RID: 19751
		// (get) Token: 0x06010A76 RID: 68214 RVA: 0x002E5B06 File Offset: 0x002E3D06
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataValidations.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D28 RID: 19752
		// (get) Token: 0x06010A77 RID: 68215 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010A78 RID: 68216 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "disablePrompts")]
		public BooleanValue DisablePrompts
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004D29 RID: 19753
		// (get) Token: 0x06010A79 RID: 68217 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06010A7A RID: 68218 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "xWindow")]
		public UInt32Value XWindow
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004D2A RID: 19754
		// (get) Token: 0x06010A7B RID: 68219 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06010A7C RID: 68220 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "yWindow")]
		public UInt32Value YWindow
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004D2B RID: 19755
		// (get) Token: 0x06010A7D RID: 68221 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06010A7E RID: 68222 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06010A7F RID: 68223 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataValidations()
		{
		}

		// Token: 0x06010A80 RID: 68224 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataValidations(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010A81 RID: 68225 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataValidations(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010A82 RID: 68226 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataValidations(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010A83 RID: 68227 RVA: 0x002E5B1C File Offset: 0x002E3D1C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "dataValidation" == name)
			{
				return new DataValidation();
			}
			return null;
		}

		// Token: 0x06010A84 RID: 68228 RVA: 0x002E5B38 File Offset: 0x002E3D38
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "disablePrompts" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "xWindow" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "yWindow" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010A85 RID: 68229 RVA: 0x002E5BA5 File Offset: 0x002E3DA5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataValidations>(deep);
		}

		// Token: 0x06010A86 RID: 68230 RVA: 0x002E5BB0 File Offset: 0x002E3DB0
		// Note: this type is marked as 'beforefieldinit'.
		static DataValidations()
		{
			byte[] array = new byte[4];
			DataValidations.attributeNamespaceIds = array;
		}

		// Token: 0x040075C3 RID: 30147
		private const string tagName = "dataValidations";

		// Token: 0x040075C4 RID: 30148
		private const byte tagNsId = 53;

		// Token: 0x040075C5 RID: 30149
		internal const int ElementTypeIdConst = 12898;

		// Token: 0x040075C6 RID: 30150
		private static string[] attributeTagNames = new string[] { "disablePrompts", "xWindow", "yWindow", "count" };

		// Token: 0x040075C7 RID: 30151
		private static byte[] attributeNamespaceIds;
	}
}
