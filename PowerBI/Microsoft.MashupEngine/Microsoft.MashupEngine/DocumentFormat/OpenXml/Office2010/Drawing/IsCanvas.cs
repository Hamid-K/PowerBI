using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002345 RID: 9029
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class IsCanvas : OpenXmlLeafElement
	{
		// Token: 0x17004976 RID: 18806
		// (get) Token: 0x06010277 RID: 66167 RVA: 0x002E0487 File Offset: 0x002DE687
		public override string LocalName
		{
			get
			{
				return "isCanvas";
			}
		}

		// Token: 0x17004977 RID: 18807
		// (get) Token: 0x06010278 RID: 66168 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004978 RID: 18808
		// (get) Token: 0x06010279 RID: 66169 RVA: 0x002E048E File Offset: 0x002DE68E
		internal override int ElementTypeId
		{
			get
			{
				return 12714;
			}
		}

		// Token: 0x0601027A RID: 66170 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004979 RID: 18809
		// (get) Token: 0x0601027B RID: 66171 RVA: 0x002E0495 File Offset: 0x002DE695
		internal override string[] AttributeTagNames
		{
			get
			{
				return IsCanvas.attributeTagNames;
			}
		}

		// Token: 0x1700497A RID: 18810
		// (get) Token: 0x0601027C RID: 66172 RVA: 0x002E049C File Offset: 0x002DE69C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return IsCanvas.attributeNamespaceIds;
			}
		}

		// Token: 0x1700497B RID: 18811
		// (get) Token: 0x0601027D RID: 66173 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601027E RID: 66174 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06010280 RID: 66176 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010281 RID: 66177 RVA: 0x002E04A3 File Offset: 0x002DE6A3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IsCanvas>(deep);
		}

		// Token: 0x06010282 RID: 66178 RVA: 0x002E04AC File Offset: 0x002DE6AC
		// Note: this type is marked as 'beforefieldinit'.
		static IsCanvas()
		{
			byte[] array = new byte[1];
			IsCanvas.attributeNamespaceIds = array;
		}

		// Token: 0x04007352 RID: 29522
		private const string tagName = "isCanvas";

		// Token: 0x04007353 RID: 29523
		private const byte tagNsId = 48;

		// Token: 0x04007354 RID: 29524
		internal const int ElementTypeIdConst = 12714;

		// Token: 0x04007355 RID: 29525
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007356 RID: 29526
		private static byte[] attributeNamespaceIds;
	}
}
