using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000133 RID: 307
	internal sealed class ResolveNamesRequest : MultiResponseServiceRequest<ResolveNamesResponse>, IJsonSerializable
	{
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0002CA62 File Offset: 0x0002BA62
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateNonBlankStringParam(this.NameToResolve, "NameToResolve");
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002CA7A File Offset: 0x0002BA7A
		internal override ResolveNamesResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ResolveNamesResponse(service);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0002CA82 File Offset: 0x0002BA82
		internal override string GetXmlElementName()
		{
			return "ResolveNames";
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0002CA89 File Offset: 0x0002BA89
		internal override string GetResponseXmlElementName()
		{
			return "ResolveNamesResponse";
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0002CA90 File Offset: 0x0002BA90
		internal override string GetResponseMessageXmlElementName()
		{
			return "ResolveNamesResponseMessage";
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0002CA97 File Offset: 0x0002BA97
		internal ResolveNamesRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0002CAAC File Offset: 0x0002BAAC
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0002CAB0 File Offset: 0x0002BAB0
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("ReturnFullContactData", this.ReturnFullContactData);
			string text = null;
			ResolveNamesRequest.searchScopeMap.Member.TryGetValue(this.SearchLocation, ref text);
			EwsUtilities.Assert(!string.IsNullOrEmpty(text), "ResolveNameRequest.WriteAttributesToXml", "The specified search location cannot be mapped to an EWS search scope.");
			string text2 = null;
			if (this.contactDataPropertySet != null)
			{
				PropertySet.DefaultPropertySetMap.Member.TryGetValue(this.contactDataPropertySet.BasePropertySet, ref text2);
			}
			if (!base.Service.Exchange2007CompatibilityMode)
			{
				writer.WriteAttributeValue("SearchScope", text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				writer.WriteAttributeValue("ContactDataShape", text2);
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002CB59 File Offset: 0x0002BB59
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.ParentFolderIds.WriteToXml(writer, XmlNamespace.Messages, "ParentFolderIds");
			writer.WriteElementValue(XmlNamespace.Messages, "UnresolvedEntry", this.NameToResolve);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0002CB80 File Offset: 0x0002BB80
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			if (this.ParentFolderIds.Count > 0)
			{
				jsonObject.Add("ParentFolderIds", this.ParentFolderIds.InternalToJson(service));
			}
			jsonObject.Add("UnresolvedEntry", this.NameToResolve);
			jsonObject.Add("ReturnFullContactData", this.ReturnFullContactData);
			string text = null;
			ResolveNamesRequest.searchScopeMap.Member.TryGetValue(this.SearchLocation, ref text);
			EwsUtilities.Assert(!string.IsNullOrEmpty(text), "ResolveNameRequest.ToJson", "The specified search location cannot be mapped to an EWS search scope.");
			string text2 = null;
			if (this.contactDataPropertySet != null)
			{
				PropertySet.DefaultPropertySetMap.Member.TryGetValue(this.contactDataPropertySet.BasePropertySet, ref text2);
			}
			if (!base.Service.Exchange2007CompatibilityMode)
			{
				jsonObject.Add("SearchScope", text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				jsonObject.Add("ContactDataShape", text2);
			}
			return jsonObject;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0002CC61 File Offset: 0x0002BC61
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0002CC64 File Offset: 0x0002BC64
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x0002CC6C File Offset: 0x0002BC6C
		public string NameToResolve
		{
			get
			{
				return this.nameToResolve;
			}
			set
			{
				this.nameToResolve = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0002CC75 File Offset: 0x0002BC75
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x0002CC7D File Offset: 0x0002BC7D
		public bool ReturnFullContactData
		{
			get
			{
				return this.returnFullContactData;
			}
			set
			{
				this.returnFullContactData = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0002CC86 File Offset: 0x0002BC86
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x0002CC8E File Offset: 0x0002BC8E
		public ResolveNameSearchLocation SearchLocation
		{
			get
			{
				return this.searchLocation;
			}
			set
			{
				this.searchLocation = value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0002CC97 File Offset: 0x0002BC97
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x0002CC9F File Offset: 0x0002BC9F
		public PropertySet ContactDataPropertySet
		{
			get
			{
				return this.contactDataPropertySet;
			}
			set
			{
				this.contactDataPropertySet = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0002CCA8 File Offset: 0x0002BCA8
		public FolderIdWrapperList ParentFolderIds
		{
			get
			{
				return this.parentFolderIds;
			}
		}

		// Token: 0x04000941 RID: 2369
		private static LazyMember<Dictionary<ResolveNameSearchLocation, string>> searchScopeMap = new LazyMember<Dictionary<ResolveNameSearchLocation, string>>(delegate
		{
			Dictionary<ResolveNameSearchLocation, string> dictionary = new Dictionary<ResolveNameSearchLocation, string>();
			dictionary.Add(ResolveNameSearchLocation.DirectoryOnly, "ActiveDirectory");
			dictionary.Add(ResolveNameSearchLocation.DirectoryThenContacts, "ActiveDirectoryContacts");
			dictionary.Add(ResolveNameSearchLocation.ContactsOnly, "Contacts");
			dictionary.Add(ResolveNameSearchLocation.ContactsThenDirectory, "ContactsActiveDirectory");
			return dictionary;
		});

		// Token: 0x04000942 RID: 2370
		private string nameToResolve;

		// Token: 0x04000943 RID: 2371
		private bool returnFullContactData;

		// Token: 0x04000944 RID: 2372
		private ResolveNameSearchLocation searchLocation;

		// Token: 0x04000945 RID: 2373
		private PropertySet contactDataPropertySet;

		// Token: 0x04000946 RID: 2374
		private FolderIdWrapperList parentFolderIds = new FolderIdWrapperList();
	}
}
