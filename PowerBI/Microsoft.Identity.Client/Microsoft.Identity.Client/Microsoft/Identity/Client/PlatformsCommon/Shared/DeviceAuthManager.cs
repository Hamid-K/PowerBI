using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001F0 RID: 496
	internal class DeviceAuthManager : IDeviceAuthManager
	{
		// Token: 0x0600153F RID: 5439 RVA: 0x00046C38 File Offset: 0x00044E38
		public DeviceAuthManager(ICryptographyManager cryptographyManager)
		{
			this._cryptographyManager = cryptographyManager;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00046C48 File Offset: 0x00044E48
		public bool TryCreateDeviceAuthChallengeResponse(HttpResponseHeaders responseHeaders, Uri endpointUri, out string responseHeader)
		{
			responseHeader = string.Empty;
			X509Certificate2 x509Certificate = null;
			if (!DeviceAuthHelper.IsDeviceAuthChallenge(responseHeaders))
			{
				return false;
			}
			if (!DeviceAuthHelper.CanOSPerformPKeyAuth())
			{
				responseHeader = DeviceAuthHelper.GetBypassChallengeResponse(responseHeaders);
				return true;
			}
			IDictionary<string, string> dictionary = DeviceAuthHelper.ParseChallengeData(responseHeaders);
			string absoluteUri;
			if (!dictionary.TryGetValue("SubmitUrl", out absoluteUri))
			{
				absoluteUri = endpointUri.AbsoluteUri;
			}
			try
			{
				x509Certificate = DeviceAuthManager.FindCertificate(dictionary);
			}
			catch (MsalException ex)
			{
				if (ex.ErrorCode == "device_certificate_not_found")
				{
					responseHeader = DeviceAuthHelper.GetBypassChallengeResponse(responseHeaders);
					return true;
				}
			}
			string responseToSign = DeviceAuthManager.GetDeviceAuthJwtResponse(absoluteUri, dictionary["nonce"], x509Certificate).GetResponseToSign();
			DeviceAuthManager.FormatResponseHeader(this._cryptographyManager.SignWithCertificate(responseToSign, x509Certificate, RSASignaturePadding.Pkcs1), dictionary, responseToSign, out responseHeader);
			return true;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00046D08 File Offset: 0x00044F08
		private static DeviceAuthJWTResponse GetDeviceAuthJwtResponse(string submitUrl, string nonce, X509Certificate2 certificate)
		{
			return new DeviceAuthJWTResponse(submitUrl, nonce, Convert.ToBase64String(certificate.GetRawCertData()));
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00046D1C File Offset: 0x00044F1C
		private static void FormatResponseHeader(byte[] signedResponse, IDictionary<string, string> challengeData, string responseToSign, out string responseHeader)
		{
			string text = Base64UrlHelpers.Encode(signedResponse);
			responseHeader = string.Concat(new string[]
			{
				"PKeyAuth AuthToken=\"",
				responseToSign,
				".",
				text,
				"\", Context=\"",
				challengeData["Context"],
				"\", Version=\"",
				challengeData["Version"],
				"\""
			});
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00046D8C File Offset: 0x00044F8C
		private static X509Certificate2 FindCertificate(IDictionary<string, string> challengeData)
		{
			X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
			X509Certificate2 x509Certificate;
			try
			{
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection certificates = x509Store.Certificates;
				if (challengeData.ContainsKey("CertAuthorities"))
				{
					x509Certificate = DeviceAuthManager.FindCertificateByCertAuthorities(challengeData, certificates);
				}
				else
				{
					X509Certificate2Collection x509Certificate2Collection = certificates.Find(X509FindType.FindByThumbprint, challengeData["CertThumbprint"], false);
					if (x509Certificate2Collection.Count == 0)
					{
						throw new MsalException("device_certificate_not_found", string.Format(CultureInfo.CurrentCulture, "Device Certificate was not found for {0}. ", "Cert thumbprint:" + challengeData["CertThumbprint"]));
					}
					x509Certificate = x509Certificate2Collection[0];
				}
			}
			finally
			{
				x509Store.Close();
			}
			return x509Certificate;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00046E34 File Offset: 0x00045034
		private static X509Certificate2 FindCertificateByCertAuthorities(IDictionary<string, string> challengeData, X509Certificate2Collection certCollection)
		{
			X509Certificate2Collection x509Certificate2Collection = null;
			string[] array = challengeData["CertAuthorities"].Split(new string[] { ";" }, StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new string[] { "," }, StringSplitOptions.None);
				string text = array2[array2.Length - 1];
				for (int j = array2.Length - 2; j >= 0; j--)
				{
					text = text + " + " + array2[j].Trim();
				}
				x509Certificate2Collection = certCollection.Find(X509FindType.FindByIssuerDistinguishedName, text, false);
				if (x509Certificate2Collection.Count > 0)
				{
					break;
				}
			}
			if (x509Certificate2Collection == null || x509Certificate2Collection.Count == 0)
			{
				throw new MsalException("device_certificate_not_found", string.Format(CultureInfo.CurrentCulture, "Device Certificate was not found for {0}. ", "Cert Authorities:" + challengeData["CertAuthorities"]));
			}
			return x509Certificate2Collection[0];
		}

		// Token: 0x040008C6 RID: 2246
		private readonly ICryptographyManager _cryptographyManager;
	}
}
