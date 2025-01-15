using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002293 RID: 8851
	[ChildElementInfo(typeof(Tab))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ContextualTabSet : OpenXmlCompositeElement
	{
		// Token: 0x170040B9 RID: 16569
		// (get) Token: 0x0600EFB1 RID: 61361 RVA: 0x002D0246 File Offset: 0x002CE446
		public override string LocalName
		{
			get
			{
				return "tabSet";
			}
		}

		// Token: 0x170040BA RID: 16570
		// (get) Token: 0x0600EFB2 RID: 61362 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x170040BB RID: 16571
		// (get) Token: 0x0600EFB3 RID: 61363 RVA: 0x002D024D File Offset: 0x002CE44D
		internal override int ElementTypeId
		{
			get
			{
				return 12609;
			}
		}

		// Token: 0x0600EFB4 RID: 61364 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170040BC RID: 16572
		// (get) Token: 0x0600EFB5 RID: 61365 RVA: 0x002D0254 File Offset: 0x002CE454
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContextualTabSet.attributeTagNames;
			}
		}

		// Token: 0x170040BD RID: 16573
		// (get) Token: 0x0600EFB6 RID: 61366 RVA: 0x002D025B File Offset: 0x002CE45B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContextualTabSet.attributeNamespaceIds;
			}
		}

		// Token: 0x170040BE RID: 16574
		// (get) Token: 0x0600EFB7 RID: 61367 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600EFB8 RID: 61368 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x170040BF RID: 16575
		// (get) Token: 0x0600EFB9 RID: 61369 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0600EFBA RID: 61370 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170040C0 RID: 16576
		// (get) Token: 0x0600EFBB RID: 61371 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EFBC RID: 61372 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0600EFBD RID: 61373 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContextualTabSet()
		{
		}

		// Token: 0x0600EFBE RID: 61374 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContextualTabSet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EFBF RID: 61375 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContextualTabSet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600EFC0 RID: 61376 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContextualTabSet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600EFC1 RID: 61377 RVA: 0x002D0262 File Offset: 0x002CE462
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (34 == namespaceId && "tab" == name)
			{
				return new Tab();
			}
			return null;
		}

		// Token: 0x0600EFC2 RID: 61378 RVA: 0x002D0280 File Offset: 0x002CE480
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600EFC3 RID: 61379 RVA: 0x002D02D7 File Offset: 0x002CE4D7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContextualTabSet>(deep);
		}

		// Token: 0x0600EFC4 RID: 61380 RVA: 0x002D02E0 File Offset: 0x002CE4E0
		// Note: this type is marked as 'beforefieldinit'.
		static ContextualTabSet()
		{
			byte[] array = new byte[3];
			ContextualTabSet.attributeNamespaceIds = array;
		}

		// Token: 0x04007037 RID: 28727
		private const string tagName = "tabSet";

		// Token: 0x04007038 RID: 28728
		private const byte tagNsId = 34;

		// Token: 0x04007039 RID: 28729
		internal const int ElementTypeIdConst = 12609;

		// Token: 0x0400703A RID: 28730
		private static string[] attributeTagNames = new string[] { "idMso", "visible", "getVisible" };

		// Token: 0x0400703B RID: 28731
		private static byte[] attributeNamespaceIds;
	}
}
