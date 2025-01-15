using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001E6 RID: 486
	public abstract class IpAddressToken : ValueBasedEntityToken
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x0001FFBB File Offset: 0x0001E1BB
		protected IpAddressToken(string source, int start, int end, IPAddress address)
			: base(source, start, end)
		{
			this.Address = address;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0001FFCE File Offset: 0x0001E1CE
		public IPAddress Address { get; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A90 RID: 2704
		public abstract override string EntityName { get; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A91 RID: 2705
		public abstract override double ScoreMultiplier { get; }

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001FFD8 File Offset: 0x0001E1D8
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
			string text = this.Address.ToString();
			tree.Add(text, new CompletionInfo(text, this, 1.0, null));
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002002C File Offset: 0x0001E22C
		public static IpAddressToken Create(string source, int start, int end, IPAddress address, int? subnetBits)
		{
			if (address.AddressFamily == AddressFamily.InterNetworkV6)
			{
				if (subnetBits != null)
				{
					return new IpV6CidrAddressToken(source, start, end, address, subnetBits.Value);
				}
				return new IpV6AddressToken(source, start, end, address);
			}
			else
			{
				if (address.AddressFamily != AddressFamily.InterNetwork)
				{
					throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("IPAddress of family \"{0}\" is not supported", new object[] { address.AddressFamily })));
				}
				if (subnetBits != null)
				{
					return new IpV4CidrAddressToken(source, start, end, address, subnetBits.Value);
				}
				return new IpV4AddressToken(source, start, end, address);
			}
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000200BC File Offset: 0x0001E2BC
		public override bool ValueBasedEquality(EntityToken other)
		{
			return other == this || (other != null && !(base.GetType() != other.GetType()) && ((IpV4AddressToken)other).Address.Equals(this.Address));
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000200F4 File Offset: 0x0001E2F4
		public override int ValueBasedHashCode()
		{
			return (base.GetType().GetHashCode() * 35993) ^ this.Address.GetHashCode();
		}
	}
}
