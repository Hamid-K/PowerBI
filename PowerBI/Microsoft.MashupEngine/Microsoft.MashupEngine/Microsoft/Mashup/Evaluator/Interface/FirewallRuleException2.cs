using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DE2 RID: 7650
	[Serializable]
	public class FirewallRuleException2 : FirewallException2
	{
		// Token: 0x0600BD89 RID: 48521 RVA: 0x00266BBF File Offset: 0x00264DBF
		public FirewallRuleException2(IResource[] resources, string message)
			: base(message, null)
		{
			this.resources = resources;
		}

		// Token: 0x0600BD8A RID: 48522 RVA: 0x00266BD0 File Offset: 0x00264DD0
		protected FirewallRuleException2(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			byte[] array = (byte[])info.GetValue("resources", typeof(byte[]));
			this.resources = BinarySerializer.Deserialize<IResource[]>(array, (BinaryReader reader) => reader.ReadArray((BinaryReader r) => r.ReadIResource()));
		}

		// Token: 0x17002EA3 RID: 11939
		// (get) Token: 0x0600BD8B RID: 48523 RVA: 0x00266C2B File Offset: 0x00264E2B
		public IEnumerable<IResource> Resources
		{
			get
			{
				return this.resources;
			}
		}

		// Token: 0x0600BD8C RID: 48524 RVA: 0x00266C34 File Offset: 0x00264E34
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			byte[] array = BinarySerializer.Serialize(delegate(BinaryWriter writer)
			{
				writer.WriteArray(this.resources, delegate(BinaryWriter w, IResource r)
				{
					w.WriteIResource(r);
				});
			});
			info.AddValue("resources", array, typeof(byte[]));
			base.GetObjectData(info, context);
		}

		// Token: 0x040060AC RID: 24748
		private const string resourcesName = "resources";

		// Token: 0x040060AD RID: 24749
		private readonly IResource[] resources;
	}
}
