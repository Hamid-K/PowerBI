using System;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200029D RID: 669
	public sealed class ManagementRoles
	{
		// Token: 0x06001787 RID: 6023 RVA: 0x0003FEBC File Offset: 0x0003EEBC
		public ManagementRoles(string userRole)
		{
			EwsUtilities.ValidateParam(userRole, "userRole");
			this.userRoles = new string[] { userRole };
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0003FEEC File Offset: 0x0003EEEC
		public ManagementRoles(string userRole, string applicationRole)
		{
			if (userRole != null)
			{
				EwsUtilities.ValidateParam(userRole, "userRole");
				this.userRoles = new string[] { userRole };
			}
			if (applicationRole != null)
			{
				EwsUtilities.ValidateParam(applicationRole, "applicationRole");
				this.applicationRoles = new string[] { applicationRole };
			}
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0003FF3F File Offset: 0x0003EF3F
		public ManagementRoles(string[] userRoles, string[] applicationRoles)
		{
			if (userRoles != null)
			{
				this.userRoles = Enumerable.ToArray<string>(userRoles);
			}
			if (applicationRoles != null)
			{
				this.applicationRoles = Enumerable.ToArray<string>(applicationRoles);
			}
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0003FF65 File Offset: 0x0003EF65
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, "ManagementRole");
			this.WriteRolesToXml(writer, this.userRoles, "UserRoles");
			this.WriteRolesToXml(writer, this.applicationRoles, "ApplicationRoles");
			writer.WriteEndElement();
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0003FFA0 File Offset: 0x0003EFA0
		private void WriteRolesToXml(EwsServiceXmlWriter writer, string[] roles, string elementName)
		{
			if (roles != null)
			{
				writer.WriteStartElement(XmlNamespace.Types, elementName);
				foreach (string text in roles)
				{
					writer.WriteElementValue(XmlNamespace.Types, "Role", text);
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0003FFE0 File Offset: 0x0003EFE0
		internal JsonObject ToJsonObject()
		{
			JsonObject jsonObject = new JsonObject();
			if (this.userRoles != null)
			{
				jsonObject.Add("UserRoles", this.userRoles);
			}
			if (this.applicationRoles != null)
			{
				jsonObject.Add("ApplicationRoles", this.applicationRoles);
			}
			return jsonObject;
		}

		// Token: 0x0400135B RID: 4955
		private readonly string[] userRoles;

		// Token: 0x0400135C RID: 4956
		private readonly string[] applicationRoles;
	}
}
