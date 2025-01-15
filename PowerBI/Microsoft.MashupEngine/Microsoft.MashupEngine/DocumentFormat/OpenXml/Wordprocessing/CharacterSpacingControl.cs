using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FEC RID: 12268
	[GeneratedCode("DomGen", "2.0")]
	internal class CharacterSpacingControl : OpenXmlLeafElement
	{
		// Token: 0x17009524 RID: 38180
		// (get) Token: 0x0601AAE7 RID: 109287 RVA: 0x00365D92 File Offset: 0x00363F92
		public override string LocalName
		{
			get
			{
				return "characterSpacingControl";
			}
		}

		// Token: 0x17009525 RID: 38181
		// (get) Token: 0x0601AAE8 RID: 109288 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009526 RID: 38182
		// (get) Token: 0x0601AAE9 RID: 109289 RVA: 0x00365D99 File Offset: 0x00363F99
		internal override int ElementTypeId
		{
			get
			{
				return 12018;
			}
		}

		// Token: 0x0601AAEA RID: 109290 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009527 RID: 38183
		// (get) Token: 0x0601AAEB RID: 109291 RVA: 0x00365DA0 File Offset: 0x00363FA0
		internal override string[] AttributeTagNames
		{
			get
			{
				return CharacterSpacingControl.attributeTagNames;
			}
		}

		// Token: 0x17009528 RID: 38184
		// (get) Token: 0x0601AAEC RID: 109292 RVA: 0x00365DA7 File Offset: 0x00363FA7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CharacterSpacingControl.attributeNamespaceIds;
			}
		}

		// Token: 0x17009529 RID: 38185
		// (get) Token: 0x0601AAED RID: 109293 RVA: 0x00365DAE File Offset: 0x00363FAE
		// (set) Token: 0x0601AAEE RID: 109294 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<CharacterSpacingValues> Val
		{
			get
			{
				return (EnumValue<CharacterSpacingValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601AAF0 RID: 109296 RVA: 0x00365DBD File Offset: 0x00363FBD
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<CharacterSpacingValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AAF1 RID: 109297 RVA: 0x00365DDF File Offset: 0x00363FDF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CharacterSpacingControl>(deep);
		}

		// Token: 0x0400ADFD RID: 44541
		private const string tagName = "characterSpacingControl";

		// Token: 0x0400ADFE RID: 44542
		private const byte tagNsId = 23;

		// Token: 0x0400ADFF RID: 44543
		internal const int ElementTypeIdConst = 12018;

		// Token: 0x0400AE00 RID: 44544
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AE01 RID: 44545
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
