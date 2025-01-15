using System;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.JsonWebTokens
{
	// Token: 0x02000002 RID: 2
	internal static class ClaimTypeMapping
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		static ClaimTypeMapping()
		{
			foreach (KeyValuePair<string, string> keyValuePair in ClaimTypeMapping.shortToLongClaimTypeMapping)
			{
				if (!ClaimTypeMapping.longToShortClaimTypeMapping.ContainsKey(keyValuePair.Value))
				{
					ClaimTypeMapping.longToShortClaimTypeMapping.Add(keyValuePair.Value, keyValuePair.Key);
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000257C File Offset: 0x0000077C
		public static IDictionary<string, string> InboundClaimTypeMap
		{
			get
			{
				return ClaimTypeMapping.shortToLongClaimTypeMapping;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002583 File Offset: 0x00000783
		public static IDictionary<string, string> OutboundClaimTypeMap
		{
			get
			{
				return ClaimTypeMapping.longToShortClaimTypeMapping;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000258A File Offset: 0x0000078A
		public static ISet<string> InboundClaimFilter
		{
			get
			{
				return ClaimTypeMapping.inboundClaimFilter;
			}
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<string, string> shortToLongClaimTypeMapping = new Dictionary<string, string>
		{
			{ "actort", "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor" },
			{ "birthdate", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth" },
			{ "email", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress" },
			{ "family_name", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname" },
			{ "gender", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender" },
			{ "given_name", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname" },
			{ "nameid", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" },
			{ "sub", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" },
			{ "website", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage" },
			{ "unique_name", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" },
			{ "oid", "http://schemas.microsoft.com/identity/claims/objectidentifier" },
			{ "scp", "http://schemas.microsoft.com/identity/claims/scope" },
			{ "tid", "http://schemas.microsoft.com/identity/claims/tenantid" },
			{ "acr", "http://schemas.microsoft.com/claims/authnclassreference" },
			{ "adfs1email", "http://schemas.xmlsoap.org/claims/EmailAddress" },
			{ "adfs1upn", "http://schemas.xmlsoap.org/claims/UPN" },
			{ "amr", "http://schemas.microsoft.com/claims/authnmethodsreferences" },
			{ "authmethod", "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod" },
			{ "certapppolicy", "http://schemas.microsoft.com/2012/12/certificatecontext/extension/applicationpolicy" },
			{ "certauthoritykeyidentifier", "http://schemas.microsoft.com/2012/12/certificatecontext/extension/authoritykeyidentifier" },
			{ "certbasicconstraints", "http://schemas.microsoft.com/2012/12/certificatecontext/extension/basicconstraints" },
			{ "certeku", "http://schemas.microsoft.com/2012/12/certificatecontext/extension/eku" },
			{ "certissuer", "http://schemas.microsoft.com/2012/12/certificatecontext/field/issuer" },
			{ "certissuername", "http://schemas.microsoft.com/2012/12/certificatecontext/field/issuername" },
			{ "certkeyusage", "http://schemas.microsoft.com/2012/12/certificatecontext/extension/keyusage" },
			{ "certnotafter", "http://schemas.microsoft.com/2012/12/certificatecontext/field/notafter" },
			{ "certnotbefore", "http://schemas.microsoft.com/2012/12/certificatecontext/field/notbefore" },
			{ "certpolicy", "http://schemas.microsoft.com/2012/12/certificatecontext/extension/certificatepolicy" },
			{ "certpublickey", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa" },
			{ "certrawdata", "http://schemas.microsoft.com/2012/12/certificatecontext/field/rawdata" },
			{ "certserialnumber", "http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber" },
			{ "certsignaturealgorithm", "http://schemas.microsoft.com/2012/12/certificatecontext/field/signaturealgorithm" },
			{ "certsubject", "http://schemas.microsoft.com/2012/12/certificatecontext/field/subject" },
			{ "certsubjectaltname", "http://schemas.microsoft.com/2012/12/certificatecontext/extension/san" },
			{ "certsubjectkeyidentifier", "http://schemas.microsoft.com/2012/12/certificatecontext/extension/subjectkeyidentifier" },
			{ "certsubjectname", "http://schemas.microsoft.com/2012/12/certificatecontext/field/subjectname" },
			{ "certtemplateinformation", "http://schemas.microsoft.com/2012/12/certificatecontext/extension/certificatetemplateinformation" },
			{ "certtemplatename", "http://schemas.microsoft.com/2012/12/certificatecontext/extension/certificatetemplatename" },
			{ "certthumbprint", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint" },
			{ "certx509version", "http://schemas.microsoft.com/2012/12/certificatecontext/field/x509version" },
			{ "clientapplication", "http://schemas.microsoft.com/2012/01/requestcontext/claims/x-ms-client-application" },
			{ "clientip", "http://schemas.microsoft.com/2012/01/requestcontext/claims/x-ms-client-ip" },
			{ "clientuseragent", "http://schemas.microsoft.com/2012/01/requestcontext/claims/x-ms-client-user-agent" },
			{ "commonname", "http://schemas.xmlsoap.org/claims/CommonName" },
			{ "denyonlyprimarygroupsid", "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid" },
			{ "denyonlyprimarysid", "http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid" },
			{ "denyonlysid", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid" },
			{ "devicedispname", "http://schemas.microsoft.com/2012/01/devicecontext/claims/displayname" },
			{ "deviceid", "http://schemas.microsoft.com/2012/01/devicecontext/claims/identifier" },
			{ "deviceismanaged", "http://schemas.microsoft.com/2012/01/devicecontext/claims/ismanaged" },
			{ "deviceostype", "http://schemas.microsoft.com/2012/01/devicecontext/claims/ostype" },
			{ "deviceosver", "http://schemas.microsoft.com/2012/01/devicecontext/claims/osversion" },
			{ "deviceowner", "http://schemas.microsoft.com/2012/01/devicecontext/claims/userowner" },
			{ "deviceregid", "http://schemas.microsoft.com/2012/01/devicecontext/claims/registrationid" },
			{ "endpointpath", "http://schemas.microsoft.com/2012/01/requestcontext/claims/x-ms-endpoint-absolute-path" },
			{ "forwardedclientip", "http://schemas.microsoft.com/2012/01/requestcontext/claims/x-ms-forwarded-client-ip" },
			{ "group", "http://schemas.xmlsoap.org/claims/Group" },
			{ "groupsid", "http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid" },
			{ "idp", "http://schemas.microsoft.com/identity/claims/identityprovider" },
			{ "insidecorporatenetwork", "http://schemas.microsoft.com/ws/2012/01/insidecorporatenetwork" },
			{ "isregistereduser", "http://schemas.microsoft.com/2012/01/devicecontext/claims/isregistereduser" },
			{ "ppid", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/privatepersonalidentifier" },
			{ "primarygroupsid", "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid" },
			{ "primarysid", "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid" },
			{ "proxy", "http://schemas.microsoft.com/2012/01/requestcontext/claims/x-ms-proxy" },
			{ "pwdchgurl", "http://schemas.microsoft.com/ws/2012/01/passwordchangeurl" },
			{ "pwdexpdays", "http://schemas.microsoft.com/ws/2012/01/passwordexpirationdays" },
			{ "pwdexptime", "http://schemas.microsoft.com/ws/2012/01/passwordexpirationtime" },
			{ "relyingpartytrustid", "http://schemas.microsoft.com/2012/01/requestcontext/claims/relyingpartytrustid" },
			{ "role", "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" },
			{ "roles", "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" },
			{ "upn", "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn" },
			{ "winaccountname", "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname" }
		};

		// Token: 0x04000002 RID: 2
		private static IDictionary<string, string> longToShortClaimTypeMapping = new Dictionary<string, string>();

		// Token: 0x04000003 RID: 3
		private static HashSet<string> inboundClaimFilter = (ClaimTypeMapping.inboundClaimFilter = new HashSet<string>());
	}
}
