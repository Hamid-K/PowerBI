using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000147 RID: 327
	internal sealed class UpdateFolderRequest : MultiResponseServiceRequest<ServiceResponse>, IJsonSerializable
	{
		// Token: 0x06000FDF RID: 4063 RVA: 0x0002E97C File Offset: 0x0002D97C
		internal UpdateFolderRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0002E994 File Offset: 0x0002D994
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParamCollection(this.Folders, "Folders");
			for (int i = 0; i < this.Folders.Count; i++)
			{
				Folder folder = this.Folders[i];
				if (folder == null || folder.IsNew)
				{
					throw new ArgumentException(string.Format(Strings.FolderToUpdateCannotBeNullOrNew, i));
				}
				folder.Validate();
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0002EA06 File Offset: 0x0002DA06
		internal override ServiceResponse CreateServiceResponse(ExchangeService session, int responseIndex)
		{
			return new UpdateFolderResponse(this.Folders[responseIndex]);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0002EA19 File Offset: 0x0002DA19
		internal override string GetXmlElementName()
		{
			return "UpdateFolder";
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0002EA20 File Offset: 0x0002DA20
		internal override string GetResponseXmlElementName()
		{
			return "UpdateFolderResponse";
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0002EA27 File Offset: 0x0002DA27
		internal override string GetResponseMessageXmlElementName()
		{
			return "UpdateFolderResponseMessage";
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0002EA2E File Offset: 0x0002DA2E
		internal override int GetExpectedResponseMessageCount()
		{
			return this.folders.Count;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0002EA3C File Offset: 0x0002DA3C
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "FolderChanges");
			foreach (Folder folder in this.folders)
			{
				folder.WriteToXmlForUpdate(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0002EAA4 File Offset: 0x0002DAA4
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			List<object> list = new List<object>();
			foreach (Folder folder in this.folders)
			{
				list.Add(folder.WriteToJsonForUpdate(service));
			}
			jsonObject.Add("FolderChanges", list.ToArray());
			return jsonObject;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0002EB1C File Offset: 0x0002DB1C
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0002EB1F File Offset: 0x0002DB1F
		public List<Folder> Folders
		{
			get
			{
				return this.folders;
			}
		}

		// Token: 0x0400097E RID: 2430
		private List<Folder> folders = new List<Folder>();
	}
}
