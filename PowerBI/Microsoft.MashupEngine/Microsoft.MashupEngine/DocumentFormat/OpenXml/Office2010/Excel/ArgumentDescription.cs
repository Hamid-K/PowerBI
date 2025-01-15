using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002411 RID: 9233
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ArgumentDescription : OpenXmlLeafTextElement
	{
		// Token: 0x17004EF0 RID: 20208
		// (get) Token: 0x06010E74 RID: 69236 RVA: 0x002E87BB File Offset: 0x002E69BB
		public override string LocalName
		{
			get
			{
				return "argumentDescription";
			}
		}

		// Token: 0x17004EF1 RID: 20209
		// (get) Token: 0x06010E75 RID: 69237 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EF2 RID: 20210
		// (get) Token: 0x06010E76 RID: 69238 RVA: 0x002E87C2 File Offset: 0x002E69C2
		internal override int ElementTypeId
		{
			get
			{
				return 12951;
			}
		}

		// Token: 0x06010E77 RID: 69239 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004EF3 RID: 20211
		// (get) Token: 0x06010E78 RID: 69240 RVA: 0x002E87C9 File Offset: 0x002E69C9
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArgumentDescription.attributeTagNames;
			}
		}

		// Token: 0x17004EF4 RID: 20212
		// (get) Token: 0x06010E79 RID: 69241 RVA: 0x002E87D0 File Offset: 0x002E69D0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArgumentDescription.attributeNamespaceIds;
			}
		}

		// Token: 0x17004EF5 RID: 20213
		// (get) Token: 0x06010E7A RID: 69242 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010E7B RID: 69243 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "index")]
		public UInt32Value Index
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

		// Token: 0x06010E7C RID: 69244 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ArgumentDescription()
		{
		}

		// Token: 0x06010E7D RID: 69245 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ArgumentDescription(string text)
			: base(text)
		{
		}

		// Token: 0x06010E7E RID: 69246 RVA: 0x002E87D8 File Offset: 0x002E69D8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06010E7F RID: 69247 RVA: 0x002E87F3 File Offset: 0x002E69F3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "index" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010E80 RID: 69248 RVA: 0x002E8813 File Offset: 0x002E6A13
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArgumentDescription>(deep);
		}

		// Token: 0x06010E81 RID: 69249 RVA: 0x002E881C File Offset: 0x002E6A1C
		// Note: this type is marked as 'beforefieldinit'.
		static ArgumentDescription()
		{
			byte[] array = new byte[1];
			ArgumentDescription.attributeNamespaceIds = array;
		}

		// Token: 0x040076CE RID: 30414
		private const string tagName = "argumentDescription";

		// Token: 0x040076CF RID: 30415
		private const byte tagNsId = 53;

		// Token: 0x040076D0 RID: 30416
		internal const int ElementTypeIdConst = 12951;

		// Token: 0x040076D1 RID: 30417
		private static string[] attributeTagNames = new string[] { "index" };

		// Token: 0x040076D2 RID: 30418
		private static byte[] attributeNamespaceIds;
	}
}
