using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.LegacyCompatibility
{
	// Token: 0x02002645 RID: 9797
	[GeneratedCode("DomGen", "2.0")]
	internal class LegacyDrawing : OpenXmlLeafElement
	{
		// Token: 0x17005AF6 RID: 23286
		// (get) Token: 0x06012951 RID: 76113 RVA: 0x002FCDC2 File Offset: 0x002FAFC2
		public override string LocalName
		{
			get
			{
				return "legacyDrawing";
			}
		}

		// Token: 0x17005AF7 RID: 23287
		// (get) Token: 0x06012952 RID: 76114 RVA: 0x00140DB6 File Offset: 0x0013EFB6
		internal override byte NamespaceId
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x17005AF8 RID: 23288
		// (get) Token: 0x06012953 RID: 76115 RVA: 0x002FCDC9 File Offset: 0x002FAFC9
		internal override int ElementTypeId
		{
			get
			{
				return 10615;
			}
		}

		// Token: 0x06012954 RID: 76116 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005AF9 RID: 23289
		// (get) Token: 0x06012955 RID: 76117 RVA: 0x002FCDD0 File Offset: 0x002FAFD0
		internal override string[] AttributeTagNames
		{
			get
			{
				return LegacyDrawing.attributeTagNames;
			}
		}

		// Token: 0x17005AFA RID: 23290
		// (get) Token: 0x06012956 RID: 76118 RVA: 0x002FCDD7 File Offset: 0x002FAFD7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LegacyDrawing.attributeNamespaceIds;
			}
		}

		// Token: 0x17005AFB RID: 23291
		// (get) Token: 0x06012957 RID: 76119 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012958 RID: 76120 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spid")]
		public StringValue ShapeId
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601295A RID: 76122 RVA: 0x002E015B File Offset: 0x002DE35B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spid" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601295B RID: 76123 RVA: 0x002FCDDE File Offset: 0x002FAFDE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LegacyDrawing>(deep);
		}

		// Token: 0x0601295C RID: 76124 RVA: 0x002FCDE8 File Offset: 0x002FAFE8
		// Note: this type is marked as 'beforefieldinit'.
		static LegacyDrawing()
		{
			byte[] array = new byte[1];
			LegacyDrawing.attributeNamespaceIds = array;
		}

		// Token: 0x040080C6 RID: 32966
		private const string tagName = "legacyDrawing";

		// Token: 0x040080C7 RID: 32967
		private const byte tagNsId = 13;

		// Token: 0x040080C8 RID: 32968
		internal const int ElementTypeIdConst = 10615;

		// Token: 0x040080C9 RID: 32969
		private static string[] attributeTagNames = new string[] { "spid" };

		// Token: 0x040080CA RID: 32970
		private static byte[] attributeNamespaceIds;
	}
}
