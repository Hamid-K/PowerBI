using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.AzureBase;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000ED3 RID: 3795
	internal sealed class AdlsTableRow
	{
		// Token: 0x060064D1 RID: 25809 RVA: 0x00159750 File Offset: 0x00157950
		public static RecordValue New(IEngineHost host, string resourceKind, RecordValue record, OptionsRecord options, AdlsVersions.Version version, Value content = null)
		{
			bool asBoolean = record["isDirectory"].AsBoolean;
			TextValue textValue = record["Name"].AsText;
			TextValue textValue2 = record["FolderPath"].AsText;
			IResource resource = Resource.New(resourceKind, record["Url"].AsString);
			int num = textValue.String.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1;
			if (num >= 0)
			{
				TextValue textValue3 = textValue2;
				textValue2 = TextValue.New(((textValue3 != null) ? textValue3.ToString() : null) + AzureBaseHelper.EscapeBlobName(textValue.String.Substring(0, num)));
				textValue = TextValue.New(textValue.String.Substring(num));
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
			Value @null;
			Value value;
			if (record.TryGetValue("Content-Type", out @null))
			{
				value = FileHelper.GetFileKind(@null.AsString);
			}
			else
			{
				@null = Value.Null;
				value = Value.Null;
			}
			Value null2;
			if (!record.TryGetValue("group", out null2))
			{
				null2 = Value.Null;
			}
			Value null3;
			if (!record.TryGetValue("owner", out null3))
			{
				null3 = Value.Null;
			}
			Value null4;
			if (!record.TryGetValue("permissions", out null4))
			{
				null4 = Value.Null;
			}
			RecordValue recordValue = RecordValue.New(AdlsHelper.ListEntryAttributeKeys, new Value[]
			{
				@null,
				value,
				record["Content-Length"],
				null2,
				null3,
				null4
			});
			if (!asBoolean)
			{
				if (content == null)
				{
					content = AdlsBinaryValue.New(host, resource, record["Url"].AsText, options, false, version);
				}
				return RecordValue.New(FileHelper.FileEntryKeys, new Value[]
				{
					content,
					textValue,
					textValue4,
					Value.Null,
					record["Last-Modified"],
					Value.Null,
					recordValue,
					textValue2
				});
			}
			AdlsEndpoint adlsEndpoint;
			if (!AdlsEndpoint.TryCreateWithFileSystem(record["Url"].AsText, out adlsEndpoint))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.AzureInvalidAccountURL, record["Url"].AsText, null);
			}
			if (content == null)
			{
				content = new AdlsTableValue(host, new List<AdlsEndpoint> { adlsEndpoint }, resourceKind, options, true, version);
			}
			return RecordValue.New(FileHelper.FileEntryKeys, new Value[]
			{
				content,
				textValue,
				TextValue.Empty,
				Value.Null,
				record["Last-Modified"],
				Value.Null,
				recordValue,
				textValue2
			});
		}
	}
}
