using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity
{
	// Token: 0x02001D71 RID: 7537
	public static class EntityMappings
	{
		// Token: 0x17002A41 RID: 10817
		// (get) Token: 0x0600FD94 RID: 64916 RVA: 0x00362B9E File Offset: 0x00360D9E
		public static Dictionary<EntityType, EntityDescriptor> EntityDescriptors
		{
			get
			{
				return EntityMappings.EntityDescriptorsLazy.Value;
			}
		}

		// Token: 0x17002A42 RID: 10818
		// (get) Token: 0x0600FD95 RID: 64917 RVA: 0x00362BAA File Offset: 0x00360DAA
		public static Dictionary<Type, EntityType> TypeToEntityType
		{
			get
			{
				return EntityMappings.TypeToEntityTypeLazy.Value;
			}
		}

		// Token: 0x0600FD96 RID: 64918 RVA: 0x00362BB8 File Offset: 0x00360DB8
		private static Dictionary<Type, EntityType> BuildTypeToEntityType()
		{
			return EntityMappings.EntityDescriptors.ToDictionary((KeyValuePair<EntityType, EntityDescriptor> kvp) => kvp.Value.Type, (KeyValuePair<EntityType, EntityDescriptor> kvp) => kvp.Key);
		}

		// Token: 0x0600FD97 RID: 64919 RVA: 0x00362C10 File Offset: 0x00360E10
		private static Dictionary<EntityType, EntityDescriptor> BuildEntityDescriptors()
		{
			Dictionary<EntityType, EntityDescriptor> dictionary = new Dictionary<EntityType, EntityDescriptor>();
			dictionary[EntityType.DomainName] = new EntityDescriptor("Domain Name", typeof(DomainNameToken), EntityType.DomainName, typeof(DomainNameTokenizer));
			dictionary[EntityType.Url] = new EntityDescriptor("URL", typeof(UrlToken), EntityType.Url, typeof(UrlTokenizer));
			dictionary[EntityType.CreditCardNumber] = new EntityDescriptor("Credit Card Number", typeof(CreditCardNumberToken), EntityType.CreditCardNumber, typeof(DashedNumbersTokenizer));
			dictionary[EntityType.MaskedCreditCardNumber] = new EntityDescriptor("Masked Credit Card Number", typeof(MaskedCreditCardNumberToken), EntityType.MaskedCreditCardNumber, typeof(DashedNumbersTokenizer));
			dictionary[EntityType.SocialSecurityNumber] = new EntityDescriptor("Social Security Number", typeof(SocialSecurityNumberToken), EntityType.SocialSecurityNumber, typeof(DashedNumbersTokenizer));
			dictionary[EntityType.MaskedSocialSecurityNumber] = new EntityDescriptor("Masked Social Security Number", typeof(MaskedSocialSecurityNumberToken), EntityType.MaskedSocialSecurityNumber, typeof(DashedNumbersTokenizer));
			dictionary[EntityType.Date] = new EntityDescriptor("Date", typeof(DateToken), EntityType.Date, typeof(DateTokenizer));
			dictionary[EntityType.Time] = new EntityDescriptor("Time", typeof(TimeToken), EntityType.Time, typeof(TimeTokenizer));
			dictionary[EntityType.EmailAddress] = new EntityDescriptor("Email Address", typeof(EmailToken), EntityType.EmailAddress, typeof(EmailTokenizer));
			dictionary[EntityType.Path] = new EntityDescriptor("Path", typeof(PathToken), EntityType.Path, typeof(PathTokenizer));
			dictionary[EntityType.FileName] = new EntityDescriptor("File Name", typeof(FileNameToken), EntityType.FileName, typeof(PathTokenizer));
			dictionary[EntityType.Guid] = new EntityDescriptor("GUID", typeof(GuidToken), EntityType.Guid, typeof(DashedNumbersTokenizer));
			dictionary[EntityType.HexadecimalNumber] = new EntityDescriptor("Hexadecimal Number", typeof(HexNumberToken), EntityType.HexadecimalNumber, typeof(NumericTokenizer));
			dictionary[EntityType.IpV4Address] = new EntityDescriptor("IP V4 Address", typeof(IpV4AddressToken), EntityType.IpV4Address, typeof(IpAddressTokenizer));
			dictionary[EntityType.IpV4CidrAddress] = new EntityDescriptor("IP V4 CIDR Address", typeof(IpV4CidrAddressToken), EntityType.IpV4CidrAddress, typeof(IpAddressTokenizer));
			dictionary[EntityType.IpV6Address] = new EntityDescriptor("IP V6 Address", typeof(IpV6AddressToken), EntityType.IpV6Address, typeof(IpAddressTokenizer));
			dictionary[EntityType.IpV6CidrAddress] = new EntityDescriptor("IP V6 CIDR Address", typeof(IpV6CidrAddressToken), EntityType.IpV6CidrAddress, typeof(IpAddressTokenizer));
			dictionary[EntityType.MacAddress] = new EntityDescriptor("MAC Address", typeof(MacAddressToken), EntityType.MacAddress, typeof(MacAddressTokenizer));
			dictionary[EntityType.Currency] = new EntityDescriptor("Currency", typeof(CurrencyToken), EntityType.Currency, typeof(CurrencyTokenizer));
			dictionary[EntityType.Number] = new EntityDescriptor("Number", typeof(NumericToken), EntityType.Number, typeof(NumericTokenizer));
			dictionary[EntityType.PhoneNumber] = new EntityDescriptor("Phone Number", typeof(PhoneNumberToken), EntityType.PhoneNumber, typeof(PhoneNumberTokenizer));
			return dictionary;
		}

		// Token: 0x04005EC2 RID: 24258
		private static readonly Lazy<Dictionary<EntityType, EntityDescriptor>> EntityDescriptorsLazy = new Lazy<Dictionary<EntityType, EntityDescriptor>>(new Func<Dictionary<EntityType, EntityDescriptor>>(EntityMappings.BuildEntityDescriptors));

		// Token: 0x04005EC3 RID: 24259
		private static readonly Lazy<Dictionary<Type, EntityType>> TypeToEntityTypeLazy = new Lazy<Dictionary<Type, EntityType>>(new Func<Dictionary<Type, EntityType>>(EntityMappings.BuildTypeToEntityType));
	}
}
