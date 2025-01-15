using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DDC RID: 7644
	[Serializable]
	public sealed class FirewallFlowException2 : FirewallException2
	{
		// Token: 0x0600BD64 RID: 48484 RVA: 0x00266756 File Offset: 0x00264956
		public FirewallFlowException2(IResource[] resources, string message)
			: base(message, null)
		{
			this.resources = resources;
		}

		// Token: 0x0600BD65 RID: 48485 RVA: 0x00266768 File Offset: 0x00264968
		public FirewallFlowException2(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.resources = BinarySerializer.Deserialize<IResource[]>((byte[])info.GetValue("resources", typeof(byte[])), (BinaryReader reader) => reader.ReadArray((BinaryReader r) => r.ReadIResource()));
		}

		// Token: 0x17002E96 RID: 11926
		// (get) Token: 0x0600BD66 RID: 48486 RVA: 0x002667C1 File Offset: 0x002649C1
		public IResource[] Resources
		{
			get
			{
				return this.resources;
			}
		}

		// Token: 0x0600BD67 RID: 48487 RVA: 0x002667C9 File Offset: 0x002649C9
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("resources", BinarySerializer.Serialize(delegate(BinaryWriter writer)
			{
				writer.WriteArray(this.resources, delegate(BinaryWriter w, IResource r)
				{
					w.WriteIResource(r);
				});
			}), typeof(byte[]));
			base.GetObjectData(info, context);
		}

		// Token: 0x04006093 RID: 24723
		private const string resourcesName = "resources";

		// Token: 0x04006094 RID: 24724
		private readonly IResource[] resources;
	}
}
