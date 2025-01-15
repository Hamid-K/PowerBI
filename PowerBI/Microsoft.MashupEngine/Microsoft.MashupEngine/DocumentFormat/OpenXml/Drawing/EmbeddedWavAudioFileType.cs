using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026CD RID: 9933
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class EmbeddedWavAudioFileType : OpenXmlLeafElement
	{
		// Token: 0x17005D6F RID: 23919
		// (get) Token: 0x06012F03 RID: 77571 RVA: 0x0030132C File Offset: 0x002FF52C
		internal override string[] AttributeTagNames
		{
			get
			{
				return EmbeddedWavAudioFileType.attributeTagNames;
			}
		}

		// Token: 0x17005D70 RID: 23920
		// (get) Token: 0x06012F04 RID: 77572 RVA: 0x00301333 File Offset: 0x002FF533
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EmbeddedWavAudioFileType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005D71 RID: 23921
		// (get) Token: 0x06012F05 RID: 77573 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012F06 RID: 77574 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "embed")]
		public StringValue Embed
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

		// Token: 0x17005D72 RID: 23922
		// (get) Token: 0x06012F07 RID: 77575 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012F08 RID: 77576 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17005D73 RID: 23923
		// (get) Token: 0x06012F09 RID: 77577 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06012F0A RID: 77578 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "builtIn")]
		public BooleanValue BuiltIn
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06012F0B RID: 77579 RVA: 0x0030133C File Offset: 0x002FF53C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "embed" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "builtIn" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012F0D RID: 77581 RVA: 0x00301398 File Offset: 0x002FF598
		// Note: this type is marked as 'beforefieldinit'.
		static EmbeddedWavAudioFileType()
		{
			byte[] array = new byte[3];
			array[0] = 19;
			EmbeddedWavAudioFileType.attributeNamespaceIds = array;
		}

		// Token: 0x040083C3 RID: 33731
		private static string[] attributeTagNames = new string[] { "embed", "name", "builtIn" };

		// Token: 0x040083C4 RID: 33732
		private static byte[] attributeNamespaceIds;
	}
}
