using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000170 RID: 368
	public static class SecurityAlgorithms
	{
		// Token: 0x04000623 RID: 1571
		public const string Aes128Encryption = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";

		// Token: 0x04000624 RID: 1572
		public const string Aes192Encryption = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";

		// Token: 0x04000625 RID: 1573
		public const string Aes256Encryption = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		// Token: 0x04000626 RID: 1574
		public const string DesEncryption = "http://www.w3.org/2001/04/xmlenc#des-cbc";

		// Token: 0x04000627 RID: 1575
		public const string Aes128KeyWrap = "http://www.w3.org/2001/04/xmlenc#kw-aes128";

		// Token: 0x04000628 RID: 1576
		public const string Aes192KeyWrap = "http://www.w3.org/2001/04/xmlenc#kw-aes192";

		// Token: 0x04000629 RID: 1577
		public const string Aes256KeyWrap = "http://www.w3.org/2001/04/xmlenc#kw-aes256";

		// Token: 0x0400062A RID: 1578
		public const string RsaV15KeyWrap = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";

		// Token: 0x0400062B RID: 1579
		public const string Ripemd160Digest = "http://www.w3.org/2001/04/xmlenc#ripemd160";

		// Token: 0x0400062C RID: 1580
		public const string RsaOaepKeyWrap = "http://www.w3.org/2001/04/xmlenc#rsa-oaep";

		// Token: 0x0400062D RID: 1581
		public const string Aes128KW = "A128KW";

		// Token: 0x0400062E RID: 1582
		public const string Aes192KW = "A192KW";

		// Token: 0x0400062F RID: 1583
		public const string Aes256KW = "A256KW";

		// Token: 0x04000630 RID: 1584
		public const string RsaPKCS1 = "RSA1_5";

		// Token: 0x04000631 RID: 1585
		public const string RsaOAEP = "RSA-OAEP";

		// Token: 0x04000632 RID: 1586
		public const string ExclusiveC14n = "http://www.w3.org/2001/10/xml-exc-c14n#";

		// Token: 0x04000633 RID: 1587
		public const string ExclusiveC14nWithComments = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";

		// Token: 0x04000634 RID: 1588
		public const string EnvelopedSignature = "http://www.w3.org/2000/09/xmldsig#enveloped-signature";

		// Token: 0x04000635 RID: 1589
		public const string Sha256Digest = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x04000636 RID: 1590
		public const string Sha384Digest = "http://www.w3.org/2001/04/xmldsig-more#sha384";

		// Token: 0x04000637 RID: 1591
		public const string Sha512Digest = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x04000638 RID: 1592
		public const string Sha256 = "SHA256";

		// Token: 0x04000639 RID: 1593
		public const string Sha384 = "SHA384";

		// Token: 0x0400063A RID: 1594
		public const string Sha512 = "SHA512";

		// Token: 0x0400063B RID: 1595
		public const string EcdsaSha256Signature = "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha256";

		// Token: 0x0400063C RID: 1596
		public const string EcdsaSha384Signature = "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha384";

		// Token: 0x0400063D RID: 1597
		public const string EcdsaSha512Signature = "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha512";

		// Token: 0x0400063E RID: 1598
		public const string HmacSha256Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";

		// Token: 0x0400063F RID: 1599
		public const string HmacSha384Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384";

		// Token: 0x04000640 RID: 1600
		public const string HmacSha512Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";

		// Token: 0x04000641 RID: 1601
		public const string RsaSha256Signature = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

		// Token: 0x04000642 RID: 1602
		public const string RsaSha384Signature = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384";

		// Token: 0x04000643 RID: 1603
		public const string RsaSha512Signature = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512";

		// Token: 0x04000644 RID: 1604
		public const string RsaSsaPssSha256Signature = "http://www.w3.org/2007/05/xmldsig-more#sha256-rsa-MGF1";

		// Token: 0x04000645 RID: 1605
		public const string RsaSsaPssSha384Signature = "http://www.w3.org/2007/05/xmldsig-more#sha384-rsa-MGF1";

		// Token: 0x04000646 RID: 1606
		public const string RsaSsaPssSha512Signature = "http://www.w3.org/2007/05/xmldsig-more#sha512-rsa-MGF1";

		// Token: 0x04000647 RID: 1607
		public const string EcdsaSha256 = "ES256";

		// Token: 0x04000648 RID: 1608
		public const string EcdsaSha384 = "ES384";

		// Token: 0x04000649 RID: 1609
		public const string EcdsaSha512 = "ES512";

		// Token: 0x0400064A RID: 1610
		public const string HmacSha256 = "HS256";

		// Token: 0x0400064B RID: 1611
		public const string HmacSha384 = "HS384";

		// Token: 0x0400064C RID: 1612
		public const string HmacSha512 = "HS512";

		// Token: 0x0400064D RID: 1613
		public const string None = "none";

		// Token: 0x0400064E RID: 1614
		public const string RsaSha256 = "RS256";

		// Token: 0x0400064F RID: 1615
		public const string RsaSha384 = "RS384";

		// Token: 0x04000650 RID: 1616
		public const string RsaSha512 = "RS512";

		// Token: 0x04000651 RID: 1617
		public const string RsaSsaPssSha256 = "PS256";

		// Token: 0x04000652 RID: 1618
		public const string RsaSsaPssSha384 = "PS384";

		// Token: 0x04000653 RID: 1619
		public const string RsaSsaPssSha512 = "PS512";

		// Token: 0x04000654 RID: 1620
		public const string Aes128CbcHmacSha256 = "A128CBC-HS256";

		// Token: 0x04000655 RID: 1621
		public const string Aes192CbcHmacSha384 = "A192CBC-HS384";

		// Token: 0x04000656 RID: 1622
		public const string Aes256CbcHmacSha512 = "A256CBC-HS512";

		// Token: 0x04000657 RID: 1623
		public const string Aes128Gcm = "A128GCM";

		// Token: 0x04000658 RID: 1624
		public const string Aes192Gcm = "A192GCM";

		// Token: 0x04000659 RID: 1625
		public const string Aes256Gcm = "A256GCM";

		// Token: 0x0400065A RID: 1626
		internal const string DefaultAsymmetricKeyWrapAlgorithm = "http://www.w3.org/2001/04/xmlenc#rsa-oaep";

		// Token: 0x0400065B RID: 1627
		internal const string DefaultSymmetricEncryptionAlgorithm = "A128CBC-HS256";

		// Token: 0x0400065C RID: 1628
		public const string EcdhEsA128kw = "ECDH-ES+A128KW";

		// Token: 0x0400065D RID: 1629
		public const string EcdhEsA192kw = "ECDH-ES+A192KW";

		// Token: 0x0400065E RID: 1630
		public const string EcdhEsA256kw = "ECDH-ES+A256KW";

		// Token: 0x0400065F RID: 1631
		public const string EcdhEs = "ECDH-ES";
	}
}
