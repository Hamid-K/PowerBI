using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200058E RID: 1422
	internal sealed class DataShapeDefinitionSerializer
	{
		// Token: 0x060051A8 RID: 20904 RVA: 0x00159DBC File Offset: 0x00157FBC
		public static void Serialize(DataShapeDefinition dataShapeDefinition, Stream stream)
		{
			DataShapeDefinitionSerializer.Serialize(dataShapeDefinition, () => XmlWriter.Create(stream, DataShapeDefinitionSerializer.writerSettings));
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x00159DE8 File Offset: 0x00157FE8
		public static void Serialize(DataShapeDefinition dataShapeDefinition, StringBuilder builder)
		{
			DataShapeDefinitionSerializer.Serialize(dataShapeDefinition, () => XmlWriter.Create(builder, DataShapeDefinitionSerializer.writerSettings));
		}

		// Token: 0x060051AA RID: 20906 RVA: 0x00159E14 File Offset: 0x00158014
		private static void Serialize(DataShapeDefinition dataShapeDefinition, Func<XmlWriter> writerCreator)
		{
			using (XmlWriter xmlWriter = writerCreator())
			{
				DataShapeDefinitionSerializer.CreateSerializer().WriteObject(xmlWriter, dataShapeDefinition);
			}
		}

		// Token: 0x060051AB RID: 20907 RVA: 0x00159E50 File Offset: 0x00158050
		public static DataShapeDefinition Deserialize(Stream stream)
		{
			return DataShapeDefinitionSerializer.Deserialize(() => XmlReader.Create(stream, DataShapeDefinitionSerializer.readerSettings));
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x00159E70 File Offset: 0x00158070
		public static DataShapeDefinition Deserialize(string serializedXml)
		{
			DataShapeDefinition dataShapeDefinition;
			using (StringReader stringReader = new StringReader(serializedXml))
			{
				dataShapeDefinition = DataShapeDefinitionSerializer.Deserialize(() => XmlReader.Create(stringReader, DataShapeDefinitionSerializer.readerSettings));
			}
			return dataShapeDefinition;
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x00159EC8 File Offset: 0x001580C8
		private static DataShapeDefinition Deserialize(Func<XmlReader> readerCreator)
		{
			DataShapeDefinition dataShapeDefinition;
			using (XmlReader xmlReader = readerCreator())
			{
				dataShapeDefinition = (DataShapeDefinition)DataShapeDefinitionSerializer.CreateSerializer().ReadObject(xmlReader);
			}
			return dataShapeDefinition;
		}

		// Token: 0x060051AE RID: 20910 RVA: 0x00159F0C File Offset: 0x0015810C
		private static DataContractSerializer CreateSerializer()
		{
			return new DataContractSerializer(typeof(DataShapeDefinition));
		}

		// Token: 0x0400293B RID: 10555
		private static readonly XmlWriterSettings writerSettings = new XmlWriterSettings
		{
			Indent = true,
			CloseOutput = false,
			ConformanceLevel = ConformanceLevel.Document
		};

		// Token: 0x0400293C RID: 10556
		private static readonly XmlReaderSettings readerSettings = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Document,
			IgnoreComments = true,
			IgnoreProcessingInstructions = true,
			IgnoreWhitespace = true,
			ProhibitDtd = true,
			XmlResolver = null
		};
	}
}
