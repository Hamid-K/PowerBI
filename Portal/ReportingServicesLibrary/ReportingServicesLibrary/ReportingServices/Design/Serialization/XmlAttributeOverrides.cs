using System;
using System.Collections;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x02000396 RID: 918
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class XmlAttributeOverrides
	{
		// Token: 0x06001E47 RID: 7751 RVA: 0x0007C421 File Offset: 0x0007A621
		public void Add(Type type, XmlAttributes attributes)
		{
			this.Add(type, string.Empty, attributes);
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x0007C430 File Offset: 0x0007A630
		public void Add(Type type, string member, XmlAttributes attributes)
		{
			Hashtable hashtable = (Hashtable)this.types[type];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this.types.Add(type, hashtable);
			}
			else if (hashtable[member] != null)
			{
				throw new InvalidOperationException();
			}
			hashtable.Add(member, attributes);
		}

		// Token: 0x1700087D RID: 2173
		public XmlAttributes this[Type type]
		{
			get
			{
				return this[type, string.Empty];
			}
		}

		// Token: 0x1700087E RID: 2174
		public XmlAttributes this[Type type, string member]
		{
			get
			{
				Hashtable hashtable = (Hashtable)this.types[type];
				if (hashtable == null)
				{
					return null;
				}
				return (XmlAttributes)hashtable[member];
			}
		}

		// Token: 0x04000CD6 RID: 3286
		private Hashtable types = new Hashtable();
	}
}
