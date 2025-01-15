using System;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x0200024F RID: 591
	public enum UserSettingName
	{
		// Token: 0x04001212 RID: 4626
		UserDisplayName,
		// Token: 0x04001213 RID: 4627
		UserDN,
		// Token: 0x04001214 RID: 4628
		UserDeploymentId,
		// Token: 0x04001215 RID: 4629
		InternalMailboxServer,
		// Token: 0x04001216 RID: 4630
		InternalRpcClientServer,
		// Token: 0x04001217 RID: 4631
		InternalMailboxServerDN,
		// Token: 0x04001218 RID: 4632
		InternalEcpUrl,
		// Token: 0x04001219 RID: 4633
		InternalEcpVoicemailUrl,
		// Token: 0x0400121A RID: 4634
		InternalEcpEmailSubscriptionsUrl,
		// Token: 0x0400121B RID: 4635
		InternalEcpTextMessagingUrl,
		// Token: 0x0400121C RID: 4636
		InternalEcpDeliveryReportUrl,
		// Token: 0x0400121D RID: 4637
		InternalEcpRetentionPolicyTagsUrl,
		// Token: 0x0400121E RID: 4638
		InternalEcpPublishingUrl,
		// Token: 0x0400121F RID: 4639
		InternalEcpPhotoUrl,
		// Token: 0x04001220 RID: 4640
		InternalEcpConnectUrl,
		// Token: 0x04001221 RID: 4641
		InternalEcpTeamMailboxUrl,
		// Token: 0x04001222 RID: 4642
		InternalEcpTeamMailboxCreatingUrl,
		// Token: 0x04001223 RID: 4643
		InternalEcpTeamMailboxEditingUrl,
		// Token: 0x04001224 RID: 4644
		InternalEcpTeamMailboxHidingUrl,
		// Token: 0x04001225 RID: 4645
		InternalEcpExtensionInstallationUrl,
		// Token: 0x04001226 RID: 4646
		InternalEwsUrl,
		// Token: 0x04001227 RID: 4647
		InternalEmwsUrl,
		// Token: 0x04001228 RID: 4648
		InternalOABUrl,
		// Token: 0x04001229 RID: 4649
		InternalPhotosUrl,
		// Token: 0x0400122A RID: 4650
		InternalUMUrl,
		// Token: 0x0400122B RID: 4651
		InternalWebClientUrls,
		// Token: 0x0400122C RID: 4652
		MailboxDN,
		// Token: 0x0400122D RID: 4653
		PublicFolderServer,
		// Token: 0x0400122E RID: 4654
		ActiveDirectoryServer,
		// Token: 0x0400122F RID: 4655
		ExternalMailboxServer,
		// Token: 0x04001230 RID: 4656
		ExternalMailboxServerRequiresSSL,
		// Token: 0x04001231 RID: 4657
		ExternalMailboxServerAuthenticationMethods,
		// Token: 0x04001232 RID: 4658
		EcpVoicemailUrlFragment,
		// Token: 0x04001233 RID: 4659
		EcpEmailSubscriptionsUrlFragment,
		// Token: 0x04001234 RID: 4660
		EcpTextMessagingUrlFragment,
		// Token: 0x04001235 RID: 4661
		EcpDeliveryReportUrlFragment,
		// Token: 0x04001236 RID: 4662
		EcpRetentionPolicyTagsUrlFragment,
		// Token: 0x04001237 RID: 4663
		EcpPublishingUrlFragment,
		// Token: 0x04001238 RID: 4664
		EcpPhotoUrlFragment,
		// Token: 0x04001239 RID: 4665
		EcpConnectUrlFragment,
		// Token: 0x0400123A RID: 4666
		EcpTeamMailboxUrlFragment,
		// Token: 0x0400123B RID: 4667
		EcpTeamMailboxCreatingUrlFragment,
		// Token: 0x0400123C RID: 4668
		EcpTeamMailboxEditingUrlFragment,
		// Token: 0x0400123D RID: 4669
		EcpExtensionInstallationUrlFragment,
		// Token: 0x0400123E RID: 4670
		ExternalEcpUrl,
		// Token: 0x0400123F RID: 4671
		ExternalEcpVoicemailUrl,
		// Token: 0x04001240 RID: 4672
		ExternalEcpEmailSubscriptionsUrl,
		// Token: 0x04001241 RID: 4673
		ExternalEcpTextMessagingUrl,
		// Token: 0x04001242 RID: 4674
		ExternalEcpDeliveryReportUrl,
		// Token: 0x04001243 RID: 4675
		ExternalEcpRetentionPolicyTagsUrl,
		// Token: 0x04001244 RID: 4676
		ExternalEcpPublishingUrl,
		// Token: 0x04001245 RID: 4677
		ExternalEcpPhotoUrl,
		// Token: 0x04001246 RID: 4678
		ExternalEcpConnectUrl,
		// Token: 0x04001247 RID: 4679
		ExternalEcpTeamMailboxUrl,
		// Token: 0x04001248 RID: 4680
		ExternalEcpTeamMailboxCreatingUrl,
		// Token: 0x04001249 RID: 4681
		ExternalEcpTeamMailboxEditingUrl,
		// Token: 0x0400124A RID: 4682
		ExternalEcpTeamMailboxHidingUrl,
		// Token: 0x0400124B RID: 4683
		ExternalEcpExtensionInstallationUrl,
		// Token: 0x0400124C RID: 4684
		ExternalEwsUrl,
		// Token: 0x0400124D RID: 4685
		ExternalEmwsUrl,
		// Token: 0x0400124E RID: 4686
		ExternalOABUrl,
		// Token: 0x0400124F RID: 4687
		ExternalPhotosUrl,
		// Token: 0x04001250 RID: 4688
		ExternalUMUrl,
		// Token: 0x04001251 RID: 4689
		ExternalWebClientUrls,
		// Token: 0x04001252 RID: 4690
		CrossOrganizationSharingEnabled,
		// Token: 0x04001253 RID: 4691
		AlternateMailboxes,
		// Token: 0x04001254 RID: 4692
		CasVersion,
		// Token: 0x04001255 RID: 4693
		EwsSupportedSchemas,
		// Token: 0x04001256 RID: 4694
		InternalPop3Connections,
		// Token: 0x04001257 RID: 4695
		ExternalPop3Connections,
		// Token: 0x04001258 RID: 4696
		InternalImap4Connections,
		// Token: 0x04001259 RID: 4697
		ExternalImap4Connections,
		// Token: 0x0400125A RID: 4698
		InternalSmtpConnections,
		// Token: 0x0400125B RID: 4699
		ExternalSmtpConnections,
		// Token: 0x0400125C RID: 4700
		InternalServerExclusiveConnect,
		// Token: 0x0400125D RID: 4701
		ExternalEwsVersion,
		// Token: 0x0400125E RID: 4702
		MobileMailboxPolicy,
		// Token: 0x0400125F RID: 4703
		DocumentSharingLocations,
		// Token: 0x04001260 RID: 4704
		UserMSOnline,
		// Token: 0x04001261 RID: 4705
		InternalMailboxServerAuthenticationMethods,
		// Token: 0x04001262 RID: 4706
		MailboxVersion,
		// Token: 0x04001263 RID: 4707
		SPMySiteHostURL,
		// Token: 0x04001264 RID: 4708
		SiteMailboxCreationURL,
		// Token: 0x04001265 RID: 4709
		InternalRpcHttpServer,
		// Token: 0x04001266 RID: 4710
		InternalRpcHttpConnectivityRequiresSsl,
		// Token: 0x04001267 RID: 4711
		InternalRpcHttpAuthenticationMethod,
		// Token: 0x04001268 RID: 4712
		ExternalServerExclusiveConnect,
		// Token: 0x04001269 RID: 4713
		ExchangeRpcUrl,
		// Token: 0x0400126A RID: 4714
		ShowGalAsDefaultView,
		// Token: 0x0400126B RID: 4715
		AutoDiscoverSMTPAddress,
		// Token: 0x0400126C RID: 4716
		InteropExternalEwsUrl,
		// Token: 0x0400126D RID: 4717
		InteropExternalEwsVersion,
		// Token: 0x0400126E RID: 4718
		PublicFolderInformation,
		// Token: 0x0400126F RID: 4719
		RedirectUrl,
		// Token: 0x04001270 RID: 4720
		EwsPartnerUrl,
		// Token: 0x04001271 RID: 4721
		CertPrincipalName,
		// Token: 0x04001272 RID: 4722
		GroupingInformation
	}
}
