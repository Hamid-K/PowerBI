using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.AzureBase;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureBlobs
{
	// Token: 0x02000EF5 RID: 3829
	internal sealed class ContainersTableValue : TableValue
	{
		// Token: 0x06006587 RID: 25991 RVA: 0x0015CBF4 File Offset: 0x0015ADF4
		public ContainersTableValue(IEngineHost host, TextValue accountUrl, string resourceKind, OptionsRecord options, bool oldAttributes = false)
		{
			this.host = host;
			this.accountUrl = accountUrl;
			this.resourceKind = resourceKind;
			this.options = options;
			this.oldAttributes = oldAttributes;
		}

		// Token: 0x17001D87 RID: 7559
		// (get) Token: 0x06006588 RID: 25992 RVA: 0x0015CC21 File Offset: 0x0015AE21
		public override TypeValue Type
		{
			get
			{
				return NavigationTableServices.DefaultTypeValue;
			}
		}

		// Token: 0x06006589 RID: 25993 RVA: 0x0015CC28 File Offset: 0x0015AE28
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return this.GetContainersNavTable().GetEnumerator();
		}

		// Token: 0x0600658A RID: 25994 RVA: 0x0015CC38 File Offset: 0x0015AE38
		public override bool TryGetValue(Value key, out Value value)
		{
			if (key.IsRecord)
			{
				RecordValue asRecord = key.AsRecord;
				Value value2;
				if (asRecord.TryGetValue("Name", out value2) && value2.IsText && asRecord.Keys.Length == 1)
				{
					value = RecordValue.New(NavigationTableServices.MetadataValues, new Value[]
					{
						value2,
						new ContainerTableValue(this.host, AzureBlobsService.GetHttpUri(this.accountUrl, value2.AsText), this.resourceKind, this.options, RecordValue.Empty, false, this.oldAttributes)
					});
					return true;
				}
			}
			return base.TryGetValue(key, out value);
		}

		// Token: 0x0600658B RID: 25995 RVA: 0x0015CCD0 File Offset: 0x0015AED0
		private TableValue GetContainersNavTable()
		{
			IResource resource = Resource.New(this.resourceKind, this.accountUrl.String);
			Request request = BlobsHelper.CreateRequest(this.host, resource, this.accountUrl, BlobsHelper.ListDirectoryQuery, null, null);
			List<Value> list = new List<Value>();
			for (;;)
			{
				using (XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(AzureBaseHelper.GetResponse(request, new Request.SecurityExceptionCreator(BlobsHelper.TryCreateSecurityException), null).GetResponseStream()))
				{
					XmlDocument xmlDocument = XmlHelperUtility.CreateXmlDocument();
					try
					{
						xmlDocument.Load(xmlReader);
					}
					catch (XmlException ex)
					{
						Message3 message = Strings.HDInsightFailedXmlException(this.resourceKind, request.InitialUri, ex.Message);
						throw HttpServices.NewDataSourceError<Message3>(this.host, message, request.RequestResource, request.InitialUri);
					}
					BlobsHelper.CheckResponseForErrors(this.host, xmlDocument, resource);
					XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Container");
					XmlNodeList elementsByTagName2 = xmlDocument.GetElementsByTagName("NextMarker");
					for (int i = 0; i < elementsByTagName.Count; i++)
					{
						TextValue textValue = TextValue.New(elementsByTagName[i]["Name"].InnerText);
						RecordValue recordValue = RecordValue.New(NavigationTableServices.MetadataValues, new Value[]
						{
							textValue,
							new ContainerTableValue(this.host, AzureBlobsService.GetHttpUri(this.accountUrl, textValue), this.resourceKind, this.options, RecordValue.Empty, false, this.oldAttributes)
						});
						list.Add(recordValue);
					}
					if (elementsByTagName2.Count > 0 && elementsByTagName2[0].FirstChild != null)
					{
						RecordValue recordValue2 = RecordValue.New(Keys.New("comp", "marker"), new Value[]
						{
							TextValue.New("list"),
							TextValue.New(elementsByTagName2[0].FirstChild.InnerText)
						});
						request = BlobsHelper.CreateRequest(this.host, resource, this.accountUrl, recordValue2, null, null);
						continue;
					}
				}
				break;
			}
			return ListValue.New(list.ToArray()).ToTable(this.Type.AsTableType);
		}

		// Token: 0x040037B5 RID: 14261
		private readonly IEngineHost host;

		// Token: 0x040037B6 RID: 14262
		private readonly TextValue accountUrl;

		// Token: 0x040037B7 RID: 14263
		private readonly string resourceKind;

		// Token: 0x040037B8 RID: 14264
		private readonly OptionsRecord options;

		// Token: 0x040037B9 RID: 14265
		private readonly bool oldAttributes;
	}
}
