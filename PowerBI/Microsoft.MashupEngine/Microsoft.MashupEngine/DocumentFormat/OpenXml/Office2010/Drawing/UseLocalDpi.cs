using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200234E RID: 9038
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class UseLocalDpi : OpenXmlLeafElement
	{
		// Token: 0x170049CF RID: 18895
		// (get) Token: 0x06010337 RID: 66359 RVA: 0x002E0F78 File Offset: 0x002DF178
		public override string LocalName
		{
			get
			{
				return "useLocalDpi";
			}
		}

		// Token: 0x170049D0 RID: 18896
		// (get) Token: 0x06010338 RID: 66360 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x170049D1 RID: 18897
		// (get) Token: 0x06010339 RID: 66361 RVA: 0x002E0F7F File Offset: 0x002DF17F
		internal override int ElementTypeId
		{
			get
			{
				return 12723;
			}
		}

		// Token: 0x0601033A RID: 66362 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170049D2 RID: 18898
		// (get) Token: 0x0601033B RID: 66363 RVA: 0x002E0F86 File Offset: 0x002DF186
		internal override string[] AttributeTagNames
		{
			get
			{
				return UseLocalDpi.attributeTagNames;
			}
		}

		// Token: 0x170049D3 RID: 18899
		// (get) Token: 0x0601033C RID: 66364 RVA: 0x002E0F8D File Offset: 0x002DF18D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UseLocalDpi.attributeNamespaceIds;
			}
		}

		// Token: 0x170049D4 RID: 18900
		// (get) Token: 0x0601033D RID: 66365 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601033E RID: 66366 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public BooleanValue Val
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

		// Token: 0x06010340 RID: 66368 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010341 RID: 66369 RVA: 0x002E0F94 File Offset: 0x002DF194
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UseLocalDpi>(deep);
		}

		// Token: 0x06010342 RID: 66370 RVA: 0x002E0FA0 File Offset: 0x002DF1A0
		// Note: this type is marked as 'beforefieldinit'.
		static UseLocalDpi()
		{
			byte[] array = new byte[1];
			UseLocalDpi.attributeNamespaceIds = array;
		}

		// Token: 0x04007383 RID: 29571
		private const string tagName = "useLocalDpi";

		// Token: 0x04007384 RID: 29572
		private const byte tagNsId = 48;

		// Token: 0x04007385 RID: 29573
		internal const int ElementTypeIdConst = 12723;

		// Token: 0x04007386 RID: 29574
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007387 RID: 29575
		private static byte[] attributeNamespaceIds;
	}
}
