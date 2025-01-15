using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A09 RID: 10761
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class EmbeddedWavAudioFileType : OpenXmlLeafElement
	{
		// Token: 0x17006F8D RID: 28557
		// (get) Token: 0x0601585B RID: 88155 RVA: 0x003202E0 File Offset: 0x0031E4E0
		internal override string[] AttributeTagNames
		{
			get
			{
				return EmbeddedWavAudioFileType.attributeTagNames;
			}
		}

		// Token: 0x17006F8E RID: 28558
		// (get) Token: 0x0601585C RID: 88156 RVA: 0x003202E7 File Offset: 0x0031E4E7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EmbeddedWavAudioFileType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006F8F RID: 28559
		// (get) Token: 0x0601585D RID: 88157 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601585E RID: 88158 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17006F90 RID: 28560
		// (get) Token: 0x0601585F RID: 88159 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06015860 RID: 88160 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17006F91 RID: 28561
		// (get) Token: 0x06015861 RID: 88161 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06015862 RID: 88162 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x06015863 RID: 88163 RVA: 0x003202F0 File Offset: 0x0031E4F0
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

		// Token: 0x06015865 RID: 88165 RVA: 0x0032034C File Offset: 0x0031E54C
		// Note: this type is marked as 'beforefieldinit'.
		static EmbeddedWavAudioFileType()
		{
			byte[] array = new byte[3];
			array[0] = 19;
			EmbeddedWavAudioFileType.attributeNamespaceIds = array;
		}

		// Token: 0x040093B0 RID: 37808
		private static string[] attributeTagNames = new string[] { "embed", "name", "builtIn" };

		// Token: 0x040093B1 RID: 37809
		private static byte[] attributeNamespaceIds;
	}
}
