using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000060 RID: 96
	public sealed class FolderId : ServiceId
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0000FB68 File Offset: 0x0000EB68
		internal FolderId()
		{
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000FB70 File Offset: 0x0000EB70
		public FolderId(string uniqueId)
			: base(uniqueId)
		{
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000FB79 File Offset: 0x0000EB79
		public FolderId(WellKnownFolderName folderName)
		{
			this.folderName = new WellKnownFolderName?(folderName);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000FB8D File Offset: 0x0000EB8D
		public FolderId(WellKnownFolderName folderName, Mailbox mailbox)
			: this(folderName)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000FBA0 File Offset: 0x0000EBA0
		internal override string GetXmlElementName()
		{
			if (this.FolderName == null)
			{
				return "FolderId";
			}
			return "DistinguishedFolderId";
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000FBC8 File Offset: 0x0000EBC8
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			if (this.FolderName != null)
			{
				writer.WriteAttributeValue("Id", this.FolderName.Value.ToString().ToLowerInvariant());
				if (this.Mailbox != null)
				{
					this.Mailbox.WriteToXml(writer, "Mailbox");
					return;
				}
			}
			else
			{
				base.WriteAttributesToXml(writer);
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000FC30 File Offset: 0x0000EC30
		internal override object InternalToJson(ExchangeService service)
		{
			if (this.FolderName != null)
			{
				JsonObject jsonObject = new JsonObject();
				jsonObject.AddTypeParameter(this.GetXmlElementName());
				jsonObject.Add("Id", this.FolderName.Value.ToString().ToLowerInvariant());
				if (this.Mailbox != null)
				{
					jsonObject.Add("Mailbox", this.Mailbox.InternalToJson(service));
				}
				return jsonObject;
			}
			return base.InternalToJson(service);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000FCB0 File Offset: 0x0000ECB0
		internal void Validate(ExchangeVersion version)
		{
			if (this.FolderName != null)
			{
				EwsUtilities.ValidateEnumVersionValue(this.FolderName.Value, version);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000FCE6 File Offset: 0x0000ECE6
		public WellKnownFolderName? FolderName
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000FCEE File Offset: 0x0000ECEE
		public Mailbox Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000FCF6 File Offset: 0x0000ECF6
		public static implicit operator FolderId(string uniqueId)
		{
			return new FolderId(uniqueId);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000FCFE File Offset: 0x0000ECFE
		public static implicit operator FolderId(WellKnownFolderName folderName)
		{
			return new FolderId(folderName);
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000FD08 File Offset: 0x0000ED08
		internal override bool IsValid
		{
			get
			{
				if (this.FolderName != null)
				{
					return this.Mailbox == null || this.Mailbox.IsValid;
				}
				return base.IsValid;
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000FD44 File Offset: 0x0000ED44
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			FolderId folderId = obj as FolderId;
			if (folderId == null)
			{
				return false;
			}
			if (this.FolderName != null)
			{
				if (folderId.FolderName != null && this.FolderName.Value.Equals(folderId.FolderName.Value))
				{
					if (this.Mailbox != null)
					{
						return this.Mailbox.Equals(folderId.Mailbox);
					}
					if (folderId.Mailbox == null)
					{
						return true;
					}
				}
			}
			else if (base.Equals(folderId))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000FDE8 File Offset: 0x0000EDE8
		public override int GetHashCode()
		{
			int num;
			if (this.FolderName != null)
			{
				num = this.FolderName.Value.GetHashCode();
				if (this.Mailbox != null && this.Mailbox.IsValid)
				{
					num ^= this.Mailbox.GetHashCode();
				}
			}
			else
			{
				num = base.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000FE4C File Offset: 0x0000EE4C
		public override string ToString()
		{
			if (!this.IsValid)
			{
				return string.Empty;
			}
			if (this.FolderName == null)
			{
				return base.ToString();
			}
			if (this.Mailbox != null && this.mailbox.IsValid)
			{
				return string.Format("{0} ({1})", this.folderName.Value, this.Mailbox.ToString());
			}
			return this.FolderName.Value.ToString();
		}

		// Token: 0x04000199 RID: 409
		private WellKnownFolderName? folderName;

		// Token: 0x0400019A RID: 410
		private Mailbox mailbox;
	}
}
