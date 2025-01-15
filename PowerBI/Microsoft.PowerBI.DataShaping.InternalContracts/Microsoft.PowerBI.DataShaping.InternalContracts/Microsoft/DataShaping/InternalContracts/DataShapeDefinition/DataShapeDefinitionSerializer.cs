using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x02000120 RID: 288
	internal sealed class DataShapeDefinitionSerializer
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x0000FA70 File Offset: 0x0000DC70
		public static void Serialize(DataShapeDefinition dataShapeDefinition, Stream stream)
		{
			DataShapeDefinitionSerializer.Serialize(dataShapeDefinition, DataShapeDefinitionSerializer.Serializer, () => XmlWriter.Create(stream, DataShapeDefinitionSerializer.WriterSettings));
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
		public static void Serialize(DataShapeDefinition dataShapeDefinition, StringBuilder builder)
		{
			DataShapeDefinitionSerializer.Serialize(dataShapeDefinition, DataShapeDefinitionSerializer.Serializer, () => XmlWriter.Create(builder, DataShapeDefinitionSerializer.WriterSettings));
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		public static void Serialize(ExpressionNode expression, Stream stream)
		{
			DataShapeDefinitionSerializer.Serialize(expression, DataShapeDefinitionSerializer.ExpressionSerializer, () => XmlWriter.Create(stream, DataShapeDefinitionSerializer.WriterSettings));
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0000FB0C File Offset: 0x0000DD0C
		public static void Serialize(ExpressionNode expression, StringBuilder builder)
		{
			DataShapeDefinitionSerializer.Serialize(expression, DataShapeDefinitionSerializer.ExpressionSerializer, () => XmlWriter.Create(builder, DataShapeDefinitionSerializer.WriterSettings));
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0000FB40 File Offset: 0x0000DD40
		private static void Serialize(object root, DataContractSerializer serializer, Func<XmlWriter> writerCreator)
		{
			using (XmlWriter xmlWriter = writerCreator())
			{
				serializer.WriteObject(xmlWriter, root);
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0000FB78 File Offset: 0x0000DD78
		public static DataShapeDefinition Deserialize(Stream stream)
		{
			return DataShapeDefinitionSerializer.Deserialize(() => XmlReader.Create(stream, DataShapeDefinitionSerializer.ReaderSettings));
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0000FB98 File Offset: 0x0000DD98
		public static DataShapeDefinition Deserialize(string serializedXml)
		{
			DataShapeDefinition dataShapeDefinition;
			using (StringReader stringReader = new StringReader(serializedXml))
			{
				dataShapeDefinition = DataShapeDefinitionSerializer.Deserialize(() => XmlReader.Create(stringReader, DataShapeDefinitionSerializer.ReaderSettings));
			}
			return dataShapeDefinition;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		private static DataShapeDefinition Deserialize(Func<XmlReader> readerCreator)
		{
			DataShapeDefinition dataShapeDefinition;
			using (XmlReader xmlReader = readerCreator())
			{
				dataShapeDefinition = (DataShapeDefinition)DataShapeDefinitionSerializer.Serializer.ReadObject(xmlReader);
			}
			return dataShapeDefinition;
		}

		// Token: 0x0400031F RID: 799
		private static readonly DataContractSerializer Serializer = new DataContractSerializer(typeof(DataShapeDefinition));

		// Token: 0x04000320 RID: 800
		private static readonly DataContractSerializer ExpressionSerializer = new DataContractSerializer(typeof(ExpressionNode));

		// Token: 0x04000321 RID: 801
		private static readonly XmlWriterSettings WriterSettings = new XmlWriterSettings
		{
			Indent = true,
			CloseOutput = false,
			ConformanceLevel = ConformanceLevel.Document
		};

		// Token: 0x04000322 RID: 802
		private static readonly XmlReaderSettings ReaderSettings = XmlUtils.ApplyDtdDosDefense(new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Document,
			IgnoreComments = true,
			IgnoreProcessingInstructions = true,
			IgnoreWhitespace = true,
			DtdProcessing = DtdProcessing.Prohibit
		});
	}
}
