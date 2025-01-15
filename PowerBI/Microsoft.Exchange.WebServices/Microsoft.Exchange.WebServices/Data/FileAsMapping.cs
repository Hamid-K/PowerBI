using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000208 RID: 520
	public enum FileAsMapping
	{
		// Token: 0x04000E06 RID: 3590
		None,
		// Token: 0x04000E07 RID: 3591
		[EwsEnum("LastCommaFirst")]
		SurnameCommaGivenName,
		// Token: 0x04000E08 RID: 3592
		[EwsEnum("FirstSpaceLast")]
		GivenNameSpaceSurname,
		// Token: 0x04000E09 RID: 3593
		Company,
		// Token: 0x04000E0A RID: 3594
		[EwsEnum("LastCommaFirstCompany")]
		SurnameCommaGivenNameCompany,
		// Token: 0x04000E0B RID: 3595
		[EwsEnum("CompanyLastFirst")]
		CompanySurnameGivenName,
		// Token: 0x04000E0C RID: 3596
		[EwsEnum("LastFirst")]
		SurnameGivenName,
		// Token: 0x04000E0D RID: 3597
		[EwsEnum("LastFirstCompany")]
		SurnameGivenNameCompany,
		// Token: 0x04000E0E RID: 3598
		[EwsEnum("CompanyLastCommaFirst")]
		CompanySurnameCommaGivenName,
		// Token: 0x04000E0F RID: 3599
		[EwsEnum("LastFirstSuffix")]
		SurnameGivenNameSuffix,
		// Token: 0x04000E10 RID: 3600
		[EwsEnum("LastSpaceFirstCompany")]
		SurnameSpaceGivenNameCompany,
		// Token: 0x04000E11 RID: 3601
		[EwsEnum("CompanyLastSpaceFirst")]
		CompanySurnameSpaceGivenName,
		// Token: 0x04000E12 RID: 3602
		[EwsEnum("LastSpaceFirst")]
		SurnameSpaceGivenName,
		// Token: 0x04000E13 RID: 3603
		[RequiredServerVersion(ExchangeVersion.Exchange2010)]
		DisplayName,
		// Token: 0x04000E14 RID: 3604
		[RequiredServerVersion(ExchangeVersion.Exchange2010)]
		[EwsEnum("FirstName")]
		GivenName,
		// Token: 0x04000E15 RID: 3605
		[EwsEnum("LastFirstMiddleSuffix")]
		[RequiredServerVersion(ExchangeVersion.Exchange2010)]
		SurnameGivenNameMiddleSuffix,
		// Token: 0x04000E16 RID: 3606
		[EwsEnum("LastName")]
		[RequiredServerVersion(ExchangeVersion.Exchange2010)]
		Surname,
		// Token: 0x04000E17 RID: 3607
		[RequiredServerVersion(ExchangeVersion.Exchange2010)]
		Empty
	}
}
