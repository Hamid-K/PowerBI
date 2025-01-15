using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000027 RID: 39
	public class WebConfigFile
	{
		// Token: 0x06000149 RID: 329 RVA: 0x000057A0 File Offset: 0x000039A0
		public string GetAuthenticationType()
		{
			XElement node = this.GetNode("system.web/authentication");
			return this.GetAttributeValue(node, "mode");
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000057C8 File Offset: 0x000039C8
		public string GetLoginUrl()
		{
			XElement node = this.GetNode("system.web/authentication/forms");
			return this.GetAttributeValue(node, "loginUrl");
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000057F0 File Offset: 0x000039F0
		public string GetFormsCookieName()
		{
			XElement node = this.GetNode("system.web/authentication/forms");
			return this.GetAttributeValue(node, "name");
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005818 File Offset: 0x00003A18
		public int GetFormsCookieTimeoutMinutes()
		{
			XElement node = this.GetNode("system.web/authentication/forms");
			string attributeValue = this.GetAttributeValue(node, "timeout");
			int num = 0;
			if (int.TryParse(attributeValue, out num))
			{
				return num;
			}
			return 60;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000584C File Offset: 0x00003A4C
		public bool GetFormsCookieSlidingExpiration()
		{
			XElement node = this.GetNode("system.web/authentication/forms");
			string attributeValue = this.GetAttributeValue(node, "slidingExpiration");
			bool flag = false;
			return bool.TryParse(attributeValue, out flag) && flag;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005880 File Offset: 0x00003A80
		public string GetFormsCookiePath()
		{
			XElement node = this.GetNode("system.web/authentication/forms");
			return this.GetAttributeValue(node, "path");
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000058A5 File Offset: 0x00003AA5
		private XElement GetNode(string xPath)
		{
			return this._rootElement.XPathSelectElement(xPath);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000058B4 File Offset: 0x00003AB4
		private string GetAttributeValue(XElement node, string name)
		{
			if (node == null)
			{
				return string.Empty;
			}
			XAttribute xattribute = node.Attribute(name);
			if (xattribute != null)
			{
				return xattribute.Value;
			}
			return string.Empty;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000058E6 File Offset: 0x00003AE6
		public WebConfigFile(string webConfigFilePath)
		{
			this._fullWebConfigFilePath = webConfigFilePath;
			this._rootElement = XElement.Load(webConfigFilePath);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005901 File Offset: 0x00003B01
		public WebConfigFile(TextReader webConfigFileReader)
		{
			this._fullWebConfigFilePath = string.Empty;
			this._rootElement = XElement.Load(webConfigFileReader);
		}

		// Token: 0x04000103 RID: 259
		private const string FormsAuthPath = "system.web/authentication/forms";

		// Token: 0x04000104 RID: 260
		private readonly string _fullWebConfigFilePath;

		// Token: 0x04000105 RID: 261
		private XElement _rootElement;
	}
}
