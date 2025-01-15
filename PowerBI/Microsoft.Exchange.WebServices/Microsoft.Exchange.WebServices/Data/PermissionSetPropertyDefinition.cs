using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002D7 RID: 727
	internal class PermissionSetPropertyDefinition : ComplexPropertyDefinitionBase
	{
		// Token: 0x060019CA RID: 6602 RVA: 0x00045FE8 File Offset: 0x00044FE8
		internal PermissionSetPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00045FF8 File Offset: 0x00044FF8
		internal override ComplexProperty CreatePropertyInstance(ServiceObject owner)
		{
			Folder folder = owner as Folder;
			EwsUtilities.Assert(folder != null, "PermissionCollectionPropertyDefinition.CreatePropertyInstance", "The owner parameter is not of type Folder or a derived class.");
			return new FolderPermissionCollection(folder);
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x00046028 File Offset: 0x00045028
		public override Type Type
		{
			get
			{
				return typeof(FolderPermissionCollection);
			}
		}
	}
}
