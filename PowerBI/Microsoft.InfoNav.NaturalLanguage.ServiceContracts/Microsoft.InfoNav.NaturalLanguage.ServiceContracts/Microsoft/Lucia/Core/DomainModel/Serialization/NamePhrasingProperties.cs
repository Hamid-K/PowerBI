using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001BB RID: 443
	public sealed class NamePhrasingProperties : PhrasingProperties
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00011C42 File Offset: 0x0000FE42
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x00011C4A File Offset: 0x0000FE4A
		[JsonProperty(Required = Required.Always)]
		public RoleReference Subject { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00011C53 File Offset: 0x0000FE53
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x00011C5B File Offset: 0x0000FE5B
		[JsonProperty(Required = Required.Always)]
		public RoleReference Name { get; set; }

		// Token: 0x06000935 RID: 2357 RVA: 0x00011C64 File Offset: 0x0000FE64
		public override void Accept(IPhrasingVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00011C6D File Offset: 0x0000FE6D
		public override T Accept<T>(IPhrasingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00011C76 File Offset: 0x0000FE76
		public override T Accept<T, TArg>(IPhrasingVisitor<T, TArg> visitor, TArg arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00011C80 File Offset: 0x0000FE80
		internal override IEnumerable<RoleReference> GetRoleReferences()
		{
			if (this.Subject != null)
			{
				yield return this.Subject;
			}
			if (this.Name != null)
			{
				yield return this.Name;
			}
			yield break;
		}
	}
}
