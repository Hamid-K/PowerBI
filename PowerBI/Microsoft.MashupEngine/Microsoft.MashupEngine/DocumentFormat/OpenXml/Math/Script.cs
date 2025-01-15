using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200294F RID: 10575
	[GeneratedCode("DomGen", "2.0")]
	internal class Script : OpenXmlLeafElement
	{
		// Token: 0x17006B71 RID: 27505
		// (get) Token: 0x06014F3F RID: 85823 RVA: 0x00318FDB File Offset: 0x003171DB
		public override string LocalName
		{
			get
			{
				return "scr";
			}
		}

		// Token: 0x17006B72 RID: 27506
		// (get) Token: 0x06014F40 RID: 85824 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006B73 RID: 27507
		// (get) Token: 0x06014F41 RID: 85825 RVA: 0x00318FE2 File Offset: 0x003171E2
		internal override int ElementTypeId
		{
			get
			{
				return 10839;
			}
		}

		// Token: 0x06014F42 RID: 85826 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006B74 RID: 27508
		// (get) Token: 0x06014F43 RID: 85827 RVA: 0x00318FE9 File Offset: 0x003171E9
		internal override string[] AttributeTagNames
		{
			get
			{
				return Script.attributeTagNames;
			}
		}

		// Token: 0x17006B75 RID: 27509
		// (get) Token: 0x06014F44 RID: 85828 RVA: 0x00318FF0 File Offset: 0x003171F0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Script.attributeNamespaceIds;
			}
		}

		// Token: 0x17006B76 RID: 27510
		// (get) Token: 0x06014F45 RID: 85829 RVA: 0x00318FF7 File Offset: 0x003171F7
		// (set) Token: 0x06014F46 RID: 85830 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<ScriptValues> Val
		{
			get
			{
				return (EnumValue<ScriptValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06014F48 RID: 85832 RVA: 0x00319006 File Offset: 0x00317206
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<ScriptValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014F49 RID: 85833 RVA: 0x00319028 File Offset: 0x00317228
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Script>(deep);
		}

		// Token: 0x040090CA RID: 37066
		private const string tagName = "scr";

		// Token: 0x040090CB RID: 37067
		private const byte tagNsId = 21;

		// Token: 0x040090CC RID: 37068
		internal const int ElementTypeIdConst = 10839;

		// Token: 0x040090CD RID: 37069
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040090CE RID: 37070
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
