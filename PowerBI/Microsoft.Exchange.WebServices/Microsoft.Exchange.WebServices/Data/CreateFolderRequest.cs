using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000F4 RID: 244
	internal sealed class CreateFolderRequest : CreateRequest<Folder, ServiceResponse>
	{
		// Token: 0x06000C4F RID: 3151 RVA: 0x00028D30 File Offset: 0x00027D30
		internal CreateFolderRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00028D3C File Offset: 0x00027D3C
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.Folders, "Folders");
			foreach (Folder folder in this.Folders)
			{
				folder.Validate();
			}
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00028DA0 File Offset: 0x00027DA0
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new CreateFolderResponse((Folder)EwsUtilities.GetEnumeratedObjectAt(this.Folders, responseIndex));
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00028DB8 File Offset: 0x00027DB8
		internal override string GetXmlElementName()
		{
			return "CreateFolder";
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00028DBF File Offset: 0x00027DBF
		internal override string GetResponseXmlElementName()
		{
			return "CreateFolderResponse";
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00028DC6 File Offset: 0x00027DC6
		internal override string GetResponseMessageXmlElementName()
		{
			return "CreateFolderResponseMessage";
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00028DCD File Offset: 0x00027DCD
		internal override string GetParentFolderXmlElementName()
		{
			return "ParentFolderId";
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00028DD4 File Offset: 0x00027DD4
		internal override string GetObjectCollectionXmlElementName()
		{
			return "Folders";
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00028DDB File Offset: 0x00027DDB
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00028DDE File Offset: 0x00027DDE
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x00028DE6 File Offset: 0x00027DE6
		public IEnumerable<Folder> Folders
		{
			get
			{
				return base.Objects;
			}
			set
			{
				base.Objects = value;
			}
		}
	}
}
