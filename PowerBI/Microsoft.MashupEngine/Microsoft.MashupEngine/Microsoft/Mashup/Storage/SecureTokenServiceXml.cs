using System;
using System.Xml.Serialization;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200208F RID: 8335
	public class SecureTokenServiceXml
	{
		// Token: 0x0600CBFD RID: 52221 RVA: 0x000020FD File Offset: 0x000002FD
		public SecureTokenServiceXml()
		{
		}

		// Token: 0x0600CBFE RID: 52222 RVA: 0x002897AC File Offset: 0x002879AC
		public SecureTokenServiceXml(ISecureTokenService tokenService)
		{
			this.authorityId = tokenService.AuthorityId;
			this.authorizeUri = tokenService.GetAuthorizeUri(string.Empty).AbsoluteUri;
			this.tokenUri = tokenService.GetTokenUri(string.Empty).AbsoluteUri;
			this.logoutUri = tokenService.GetLogoutUri(string.Empty).AbsoluteUri;
		}

		// Token: 0x1700311C RID: 12572
		// (get) Token: 0x0600CBFF RID: 52223 RVA: 0x0028980D File Offset: 0x00287A0D
		// (set) Token: 0x0600CC00 RID: 52224 RVA: 0x00289815 File Offset: 0x00287A15
		[XmlAttribute("AuthorityId")]
		public string AuthorityId
		{
			get
			{
				return this.authorityId;
			}
			set
			{
				this.authorityId = value;
			}
		}

		// Token: 0x1700311D RID: 12573
		// (get) Token: 0x0600CC01 RID: 52225 RVA: 0x0028981E File Offset: 0x00287A1E
		// (set) Token: 0x0600CC02 RID: 52226 RVA: 0x00289826 File Offset: 0x00287A26
		[XmlAttribute("AuthorizeUri")]
		public string AuthorizeUri
		{
			get
			{
				return this.authorizeUri;
			}
			set
			{
				this.authorizeUri = value;
			}
		}

		// Token: 0x1700311E RID: 12574
		// (get) Token: 0x0600CC03 RID: 52227 RVA: 0x0028982F File Offset: 0x00287A2F
		// (set) Token: 0x0600CC04 RID: 52228 RVA: 0x00289837 File Offset: 0x00287A37
		[XmlAttribute("TokenUri")]
		public string TokenUri
		{
			get
			{
				return this.tokenUri;
			}
			set
			{
				this.tokenUri = value;
			}
		}

		// Token: 0x1700311F RID: 12575
		// (get) Token: 0x0600CC05 RID: 52229 RVA: 0x00289840 File Offset: 0x00287A40
		// (set) Token: 0x0600CC06 RID: 52230 RVA: 0x00289848 File Offset: 0x00287A48
		[XmlAttribute("LogoutUri")]
		public string LogoutUri
		{
			get
			{
				return this.logoutUri;
			}
			set
			{
				this.logoutUri = value;
			}
		}

		// Token: 0x0600CC07 RID: 52231 RVA: 0x00289854 File Offset: 0x00287A54
		public override bool Equals(object obj)
		{
			SecureTokenServiceXml secureTokenServiceXml = obj as SecureTokenServiceXml;
			return obj != null && (this.AuthorityId == secureTokenServiceXml.AuthorityId && this.AuthorizeUri == secureTokenServiceXml.AuthorizeUri && this.TokenUri == secureTokenServiceXml.TokenUri) && this.LogoutUri == secureTokenServiceXml.LogoutUri;
		}

		// Token: 0x0600CC08 RID: 52232 RVA: 0x002898B9 File Offset: 0x00287AB9
		public override int GetHashCode()
		{
			return this.AuthorityId.GetHashCode() ^ this.AuthorizeUri.GetHashCode() ^ this.TokenUri.GetHashCode() ^ this.LogoutUri.GetHashCode();
		}

		// Token: 0x0400676C RID: 26476
		private string authorityId;

		// Token: 0x0400676D RID: 26477
		private string authorizeUri;

		// Token: 0x0400676E RID: 26478
		private string tokenUri;

		// Token: 0x0400676F RID: 26479
		private string logoutUri;
	}
}
