using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.BIServer.Configuration.Catalog;
using Microsoft.ReportingServices.CatalogAccess.DataAccessObject;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x0200002B RID: 43
	public sealed class PolicyAcessor : IPolicyAcessor
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00006DF0 File Offset: 0x00004FF0
		public async Task<IEnumerable<PolicyEntity>> GetItemPolicies(Guid itemId, int authType)
		{
			var <>f__AnonymousType = new
			{
				ItemId = itemId,
				AuthType = authType
			};
			XmlPolicyEntity xmlPolicyEntity = await CatalogAccessFactory.QueryFirstOrDefaultAsync<XmlPolicyEntity>("GetPolicyByItemId", <>f__AnonymousType);
			IEnumerable<PolicyEntity> enumerable;
			if (xmlPolicyEntity == null)
			{
				enumerable = Enumerable.Empty<PolicyEntity>();
			}
			else
			{
				List<PolicyEntity> list = new List<PolicyEntity>();
				foreach (XElement xelement in from p in XDocument.Parse(xmlPolicyEntity.XmlDescription).Descendants()
					where p.Name.LocalName.Equals("Policy")
					select p)
				{
					list.Add(new PolicyEntity
					{
						GroupUserName = this.ReadChildElement(xelement, "GroupUserName"),
						GroupUserId = this.ReadChildElement(xelement, "GroupUserId"),
						Roles = this.ReadRoles(xelement, "Role")
					});
				}
				enumerable = list;
			}
			return enumerable;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006E45 File Offset: 0x00005045
		private IEnumerable<RoleEntity> ReadRoles(XElement parent, string elementName)
		{
			foreach (XElement xelement in parent.Descendants(XName.Get(elementName, parent.Name.NamespaceName)))
			{
				yield return new RoleEntity
				{
					Description = this.ReadChildElement(xelement, "Description"),
					Name = this.ReadChildElement(xelement, "Name")
				};
			}
			IEnumerator<XElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006E64 File Offset: 0x00005064
		private string ReadChildElement(XElement parent, string elementName)
		{
			XElement xelement = parent.Elements(XName.Get(elementName, parent.Name.NamespaceName)).FirstOrDefault<XElement>();
			if (xelement != null)
			{
				return xelement.Value;
			}
			return null;
		}
	}
}
