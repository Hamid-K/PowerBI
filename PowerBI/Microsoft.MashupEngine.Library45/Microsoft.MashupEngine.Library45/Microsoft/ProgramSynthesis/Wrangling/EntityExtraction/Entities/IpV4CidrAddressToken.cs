using System;
using System.Net;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001EA RID: 490
	public class IpV4CidrAddressToken : IpV4AddressToken
	{
		// Token: 0x06000AA4 RID: 2724 RVA: 0x000203B2 File Offset: 0x0001E5B2
		public IpV4CidrAddressToken(string source, int start, int end, IPAddress address, int subnetBits)
			: base(source, start, end, address)
		{
			this.SubnetBits = subnetBits;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x000203C7 File Offset: 0x0001E5C7
		public int SubnetBits { get; }

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000203D0 File Offset: 0x0001E5D0
		public override bool ValueBasedEquality(EntityToken other)
		{
			if (other == this)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			if (base.GetType() != other.GetType())
			{
				return false;
			}
			IpV4CidrAddressToken ipV4CidrAddressToken = (IpV4CidrAddressToken)other;
			return ipV4CidrAddressToken.Address.Equals(base.Address) && ipV4CidrAddressToken.SubnetBits == this.SubnetBits;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00020428 File Offset: 0x0001E628
		public override int ValueBasedHashCode()
		{
			return (((base.GetType().GetHashCode() * 38611) ^ base.Address.GetHashCode()) * 40559) ^ this.SubnetBits.GetHashCode();
		}
	}
}
