using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000029 RID: 41
	internal sealed class ODataAtomAnnotationReader
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00005090 File Offset: 0x00003290
		internal ODataAtomAnnotationReader(ODataAtomInputContext inputContext, ODataAtomPropertyAndValueDeserializer propertyAndValueDeserializer)
		{
			this.inputContext = inputContext;
			this.propertyAndValueDeserializer = propertyAndValueDeserializer;
			BufferingXmlReader xmlReader = this.inputContext.XmlReader;
			xmlReader.NameTable.Add("target");
			xmlReader.NameTable.Add("term");
			xmlReader.NameTable.Add("type");
			xmlReader.NameTable.Add("null");
			xmlReader.NameTable.Add("string");
			xmlReader.NameTable.Add("bool");
			xmlReader.NameTable.Add("decimal");
			xmlReader.NameTable.Add("int");
			xmlReader.NameTable.Add("float");
			this.odataMetadataNamespace = xmlReader.NameTable.Add("http://docs.oasis-open.org/odata/ns/metadata");
			this.attributeElementName = xmlReader.NameTable.Add("annotation");
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005184 File Offset: 0x00003384
		internal bool TryReadAnnotation(out AtomInstanceAnnotation annotation)
		{
			BufferingXmlReader xmlReader = this.inputContext.XmlReader;
			annotation = null;
			if (this.propertyAndValueDeserializer.MessageReaderSettings.ShouldIncludeAnnotation != null && xmlReader.NamespaceEquals(this.odataMetadataNamespace) && xmlReader.LocalNameEquals(this.attributeElementName))
			{
				annotation = AtomInstanceAnnotation.CreateFrom(this.inputContext, this.propertyAndValueDeserializer);
			}
			return annotation != null;
		}

		// Token: 0x0400010A RID: 266
		private readonly ODataAtomInputContext inputContext;

		// Token: 0x0400010B RID: 267
		private readonly string odataMetadataNamespace;

		// Token: 0x0400010C RID: 268
		private readonly string attributeElementName;

		// Token: 0x0400010D RID: 269
		private readonly ODataAtomPropertyAndValueDeserializer propertyAndValueDeserializer;
	}
}
