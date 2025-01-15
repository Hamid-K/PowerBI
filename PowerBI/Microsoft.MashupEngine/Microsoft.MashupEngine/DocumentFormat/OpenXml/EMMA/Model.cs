using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x0200307C RID: 12412
	[GeneratedCode("DomGen", "2.0")]
	internal class Model : OpenXmlCompositeElement
	{
		// Token: 0x17009715 RID: 38677
		// (get) Token: 0x0601AF28 RID: 110376 RVA: 0x00369D93 File Offset: 0x00367F93
		public override string LocalName
		{
			get
			{
				return "model";
			}
		}

		// Token: 0x17009716 RID: 38678
		// (get) Token: 0x0601AF29 RID: 110377 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x17009717 RID: 38679
		// (get) Token: 0x0601AF2A RID: 110378 RVA: 0x00369D9A File Offset: 0x00367F9A
		internal override int ElementTypeId
		{
			get
			{
				return 12681;
			}
		}

		// Token: 0x0601AF2B RID: 110379 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009718 RID: 38680
		// (get) Token: 0x0601AF2C RID: 110380 RVA: 0x00369DA1 File Offset: 0x00367FA1
		internal override string[] AttributeTagNames
		{
			get
			{
				return Model.attributeTagNames;
			}
		}

		// Token: 0x17009719 RID: 38681
		// (get) Token: 0x0601AF2D RID: 110381 RVA: 0x00369DA8 File Offset: 0x00367FA8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Model.attributeNamespaceIds;
			}
		}

		// Token: 0x1700971A RID: 38682
		// (get) Token: 0x0601AF2E RID: 110382 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AF2F RID: 110383 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x1700971B RID: 38683
		// (get) Token: 0x0601AF30 RID: 110384 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AF31 RID: 110385 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ref")]
		public StringValue Reference
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601AF32 RID: 110386 RVA: 0x00293ECF File Offset: 0x002920CF
		public Model()
		{
		}

		// Token: 0x0601AF33 RID: 110387 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Model(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF34 RID: 110388 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Model(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AF35 RID: 110389 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Model(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AF36 RID: 110390 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x0601AF37 RID: 110391 RVA: 0x00369D1B File Offset: 0x00367F1B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AF38 RID: 110392 RVA: 0x00369DAF File Offset: 0x00367FAF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Model>(deep);
		}

		// Token: 0x0601AF39 RID: 110393 RVA: 0x00369DB8 File Offset: 0x00367FB8
		// Note: this type is marked as 'beforefieldinit'.
		static Model()
		{
			byte[] array = new byte[2];
			Model.attributeNamespaceIds = array;
		}

		// Token: 0x0400B23E RID: 45630
		private const string tagName = "model";

		// Token: 0x0400B23F RID: 45631
		private const byte tagNsId = 44;

		// Token: 0x0400B240 RID: 45632
		internal const int ElementTypeIdConst = 12681;

		// Token: 0x0400B241 RID: 45633
		private static string[] attributeTagNames = new string[] { "id", "ref" };

		// Token: 0x0400B242 RID: 45634
		private static byte[] attributeNamespaceIds;
	}
}
