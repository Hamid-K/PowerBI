using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C09 RID: 11273
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Color))]
	internal class GradientStop : OpenXmlCompositeElement
	{
		// Token: 0x17007F96 RID: 32662
		// (get) Token: 0x06017BB8 RID: 97208 RVA: 0x0033A74F File Offset: 0x0033894F
		public override string LocalName
		{
			get
			{
				return "stop";
			}
		}

		// Token: 0x17007F97 RID: 32663
		// (get) Token: 0x06017BB9 RID: 97209 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F98 RID: 32664
		// (get) Token: 0x06017BBA RID: 97210 RVA: 0x0033A756 File Offset: 0x00338956
		internal override int ElementTypeId
		{
			get
			{
				return 11254;
			}
		}

		// Token: 0x06017BBB RID: 97211 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F99 RID: 32665
		// (get) Token: 0x06017BBC RID: 97212 RVA: 0x0033A75D File Offset: 0x0033895D
		internal override string[] AttributeTagNames
		{
			get
			{
				return GradientStop.attributeTagNames;
			}
		}

		// Token: 0x17007F9A RID: 32666
		// (get) Token: 0x06017BBD RID: 97213 RVA: 0x0033A764 File Offset: 0x00338964
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GradientStop.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F9B RID: 32667
		// (get) Token: 0x06017BBE RID: 97214 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x06017BBF RID: 97215 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "position")]
		public DoubleValue Position
		{
			get
			{
				return (DoubleValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06017BC0 RID: 97216 RVA: 0x00293ECF File Offset: 0x002920CF
		public GradientStop()
		{
		}

		// Token: 0x06017BC1 RID: 97217 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GradientStop(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017BC2 RID: 97218 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GradientStop(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017BC3 RID: 97219 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GradientStop(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017BC4 RID: 97220 RVA: 0x0033A76B File Offset: 0x0033896B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "color" == name)
			{
				return new Color();
			}
			return null;
		}

		// Token: 0x17007F9C RID: 32668
		// (get) Token: 0x06017BC5 RID: 97221 RVA: 0x0033A786 File Offset: 0x00338986
		internal override string[] ElementTagNames
		{
			get
			{
				return GradientStop.eleTagNames;
			}
		}

		// Token: 0x17007F9D RID: 32669
		// (get) Token: 0x06017BC6 RID: 97222 RVA: 0x0033A78D File Offset: 0x0033898D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GradientStop.eleNamespaceIds;
			}
		}

		// Token: 0x17007F9E RID: 32670
		// (get) Token: 0x06017BC7 RID: 97223 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007F9F RID: 32671
		// (get) Token: 0x06017BC8 RID: 97224 RVA: 0x0033A794 File Offset: 0x00338994
		// (set) Token: 0x06017BC9 RID: 97225 RVA: 0x0033A79D File Offset: 0x0033899D
		public Color Color
		{
			get
			{
				return base.GetElement<Color>(0);
			}
			set
			{
				base.SetElement<Color>(0, value);
			}
		}

		// Token: 0x06017BCA RID: 97226 RVA: 0x0033A7A7 File Offset: 0x003389A7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "position" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017BCB RID: 97227 RVA: 0x0033A7C7 File Offset: 0x003389C7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GradientStop>(deep);
		}

		// Token: 0x06017BCC RID: 97228 RVA: 0x0033A7D0 File Offset: 0x003389D0
		// Note: this type is marked as 'beforefieldinit'.
		static GradientStop()
		{
			byte[] array = new byte[1];
			GradientStop.attributeNamespaceIds = array;
			GradientStop.eleTagNames = new string[] { "color" };
			GradientStop.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009D5C RID: 40284
		private const string tagName = "stop";

		// Token: 0x04009D5D RID: 40285
		private const byte tagNsId = 22;

		// Token: 0x04009D5E RID: 40286
		internal const int ElementTypeIdConst = 11254;

		// Token: 0x04009D5F RID: 40287
		private static string[] attributeTagNames = new string[] { "position" };

		// Token: 0x04009D60 RID: 40288
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009D61 RID: 40289
		private static readonly string[] eleTagNames;

		// Token: 0x04009D62 RID: 40290
		private static readonly byte[] eleNamespaceIds;
	}
}
