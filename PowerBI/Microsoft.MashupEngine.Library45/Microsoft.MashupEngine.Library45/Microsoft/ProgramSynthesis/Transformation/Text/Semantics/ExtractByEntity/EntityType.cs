using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity
{
	// Token: 0x02001D73 RID: 7539
	public enum EntityType
	{
		// Token: 0x04005EC8 RID: 24264
		Unknown,
		// Token: 0x04005EC9 RID: 24265
		DomainName,
		// Token: 0x04005ECA RID: 24266
		Url,
		// Token: 0x04005ECB RID: 24267
		CreditCardNumber,
		// Token: 0x04005ECC RID: 24268
		MaskedCreditCardNumber,
		// Token: 0x04005ECD RID: 24269
		SocialSecurityNumber,
		// Token: 0x04005ECE RID: 24270
		MaskedSocialSecurityNumber,
		// Token: 0x04005ECF RID: 24271
		Date,
		// Token: 0x04005ED0 RID: 24272
		Time,
		// Token: 0x04005ED1 RID: 24273
		EmailAddress,
		// Token: 0x04005ED2 RID: 24274
		Path,
		// Token: 0x04005ED3 RID: 24275
		FileName,
		// Token: 0x04005ED4 RID: 24276
		Guid,
		// Token: 0x04005ED5 RID: 24277
		HexadecimalNumber,
		// Token: 0x04005ED6 RID: 24278
		IpV4Address,
		// Token: 0x04005ED7 RID: 24279
		IpV4CidrAddress,
		// Token: 0x04005ED8 RID: 24280
		IpV6Address,
		// Token: 0x04005ED9 RID: 24281
		IpV6CidrAddress,
		// Token: 0x04005EDA RID: 24282
		MacAddress,
		// Token: 0x04005EDB RID: 24283
		Currency,
		// Token: 0x04005EDC RID: 24284
		Number,
		// Token: 0x04005EDD RID: 24285
		PhoneNumber
	}
}
