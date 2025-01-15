using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.AzureBase;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureBlobs
{
	// Token: 0x02000EF4 RID: 3828
	internal sealed class ContainerRecordValue
	{
		// Token: 0x06006585 RID: 25989 RVA: 0x0015C9D4 File Offset: 0x0015ABD4
		public static RecordValue New(IEngineHost host, IResource resource, RecordValue record, OptionsRecord options, bool oldAttributes)
		{
			TextValue textValue = record["Name"].AsText;
			TextValue textValue2 = record["FolderPath"].AsText;
			if (resource.Kind == "HDInsight")
			{
				int num = textValue.String.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1;
				if (num >= 0)
				{
					TextValue textValue3 = textValue2;
					textValue2 = TextValue.New(((textValue3 != null) ? textValue3.ToString() : null) + AzureBaseHelper.EscapeBlobName(textValue.String.Substring(0, num)));
					textValue = TextValue.New(textValue.String.Substring(num));
				}
			}
			TextValue textValue4;
			try
			{
				textValue4 = FileHelper.GetFileExtension(record["Name"].AsText.String);
			}
			catch (ValueException)
			{
				textValue4 = TextValue.Empty;
			}
			Value value = record["Content-Encoding"];
			BinaryValue binaryValue = BlobBinaryValue.New(host, resource, record["Url"].AsText, options, null, new long?(record["Content-Length"].AsNumber.AsInteger64), record["etag"].AsString, value.IsNull ? null : value.AsString);
			RecordValue recordValue;
			Value value2;
			if (oldAttributes)
			{
				recordValue = RecordValue.New(AzureBaseHelper.ContentsAttributeKeys, new Value[]
				{
					record["Content-Type"],
					record["Content-Length"]
				});
			}
			else if (record.TryGetValue("Content-Type", out value2))
			{
				recordValue = RecordValue.New(AzureBaseHelper.AttributeKeys, new Value[]
				{
					value2,
					FileHelper.GetFileKind(record["Content-Type"].AsString),
					record["Content-Length"]
				});
			}
			else
			{
				recordValue = RecordValue.New(BlobsHelper.ListEntryAttributeKeys, new Value[] { record["Content-Length"] });
			}
			return RecordValue.New(FileHelper.FileEntryKeys, new Value[]
			{
				binaryValue,
				textValue,
				textValue4,
				Value.Null,
				record["Last-Modified"],
				Value.Null,
				recordValue,
				textValue2
			});
		}
	}
}
