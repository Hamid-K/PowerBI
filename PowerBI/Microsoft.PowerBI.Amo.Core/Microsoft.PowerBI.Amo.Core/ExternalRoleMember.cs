using System;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000087 RID: 135
	[Guid("91A69874-1837-4cba-8491-96D4776A950E")]
	public class ExternalRoleMember : ICloneable
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x000241CD File Offset: 0x000223CD
		private void CopyContentsTo(ExternalRoleMember obj)
		{
			obj.identityProvider = this.identityProvider;
			obj.id = this.id;
			obj.name = this.name;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000241F3 File Offset: 0x000223F3
		public ExternalRoleMember()
		{
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000241FB File Offset: 0x000223FB
		public ExternalRoleMember(string identityProvider, string id, string name)
		{
			this.identityProvider = Utils.Trim(identityProvider);
			this.id = Utils.Trim(id);
			this.name = Utils.Trim(name);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00024228 File Offset: 0x00022428
		object ICloneable.Clone()
		{
			ExternalRoleMember externalRoleMember = new ExternalRoleMember();
			this.CopyContentsTo(externalRoleMember);
			return externalRoleMember;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00024243 File Offset: 0x00022443
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0002424B File Offset: 0x0002244B
		[XmlElement(IsNullable = false)]
		public string IdentityProvider
		{
			get
			{
				return this.identityProvider;
			}
			set
			{
				this.identityProvider = Utils.Trim(value);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00024259 File Offset: 0x00022459
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00024261 File Offset: 0x00022461
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine", IsNullable = false)]
		public string ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = Utils.Trim(value);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0002426F File Offset: 0x0002246F
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00024277 File Offset: 0x00022477
		[XmlElement(Namespace = "http://schemas.microsoft.com/analysisservices/2003/engine", IsNullable = false)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = Utils.Trim(value);
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00024285 File Offset: 0x00022485
		public ExternalRoleMember CopyTo(ExternalRoleMember obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (obj == this)
			{
				throw new InvalidOperationException(SR.Copy_DestinationIsSelf);
			}
			this.CopyContentsTo(obj);
			return obj;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000242AC File Offset: 0x000224AC
		public ExternalRoleMember Clone()
		{
			ExternalRoleMember externalRoleMember = new ExternalRoleMember();
			this.CopyContentsTo(externalRoleMember);
			return externalRoleMember;
		}

		// Token: 0x0400045C RID: 1116
		private string identityProvider;

		// Token: 0x0400045D RID: 1117
		private string id;

		// Token: 0x0400045E RID: 1118
		private string name;
	}
}
