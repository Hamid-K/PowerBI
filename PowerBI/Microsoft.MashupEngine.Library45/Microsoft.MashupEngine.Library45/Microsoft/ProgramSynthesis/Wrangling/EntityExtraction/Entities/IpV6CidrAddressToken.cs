using System;
using System.Net;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001EC RID: 492
	public class IpV6CidrAddressToken : IpV6AddressToken
	{
		// Token: 0x06000AAB RID: 2731 RVA: 0x0002046E File Offset: 0x0001E66E
		public IpV6CidrAddressToken(string source, int start, int end, IPAddress address, int subnetBits)
			: base(source, start, end, address)
		{
			this.SubnetBits = subnetBits;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00020483 File Offset: 0x0001E683
		public int SubnetBits { get; }

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002048C File Offset: 0x0001E68C
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
			IpV6CidrAddressToken ipV6CidrAddressToken = (IpV6CidrAddressToken)other;
			return ipV6CidrAddressToken.Address.Equals(base.Address) && ipV6CidrAddressToken.SubnetBits == this.SubnetBits;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000204E4 File Offset: 0x0001E6E4
		public override int ValueBasedHashCode()
		{
			return (((base.GetType().GetHashCode() * 37361) ^ base.Address.GetHashCode()) * 38431) ^ this.SubnetBits.GetHashCode();
		}
	}
}
