using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A11 RID: 10769
	[GeneratedCode("DomGen", "2.0")]
	internal class RuntimeNodeTrigger : OpenXmlLeafElement
	{
		// Token: 0x17006FBF RID: 28607
		// (get) Token: 0x060158C3 RID: 88259 RVA: 0x003206FB File Offset: 0x0031E8FB
		public override string LocalName
		{
			get
			{
				return "rtn";
			}
		}

		// Token: 0x17006FC0 RID: 28608
		// (get) Token: 0x060158C4 RID: 88260 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006FC1 RID: 28609
		// (get) Token: 0x060158C5 RID: 88261 RVA: 0x00320702 File Offset: 0x0031E902
		internal override int ElementTypeId
		{
			get
			{
				return 12197;
			}
		}

		// Token: 0x060158C6 RID: 88262 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006FC2 RID: 28610
		// (get) Token: 0x060158C7 RID: 88263 RVA: 0x00320709 File Offset: 0x0031E909
		internal override string[] AttributeTagNames
		{
			get
			{
				return RuntimeNodeTrigger.attributeTagNames;
			}
		}

		// Token: 0x17006FC3 RID: 28611
		// (get) Token: 0x060158C8 RID: 88264 RVA: 0x00320710 File Offset: 0x0031E910
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RuntimeNodeTrigger.attributeNamespaceIds;
			}
		}

		// Token: 0x17006FC4 RID: 28612
		// (get) Token: 0x060158C9 RID: 88265 RVA: 0x00320717 File Offset: 0x0031E917
		// (set) Token: 0x060158CA RID: 88266 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<TriggerRuntimeNodeValues> Val
		{
			get
			{
				return (EnumValue<TriggerRuntimeNodeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060158CC RID: 88268 RVA: 0x00320726 File Offset: 0x0031E926
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<TriggerRuntimeNodeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060158CD RID: 88269 RVA: 0x00320746 File Offset: 0x0031E946
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RuntimeNodeTrigger>(deep);
		}

		// Token: 0x060158CE RID: 88270 RVA: 0x00320750 File Offset: 0x0031E950
		// Note: this type is marked as 'beforefieldinit'.
		static RuntimeNodeTrigger()
		{
			byte[] array = new byte[1];
			RuntimeNodeTrigger.attributeNamespaceIds = array;
		}

		// Token: 0x040093D3 RID: 37843
		private const string tagName = "rtn";

		// Token: 0x040093D4 RID: 37844
		private const byte tagNsId = 24;

		// Token: 0x040093D5 RID: 37845
		internal const int ElementTypeIdConst = 12197;

		// Token: 0x040093D6 RID: 37846
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040093D7 RID: 37847
		private static byte[] attributeNamespaceIds;
	}
}
