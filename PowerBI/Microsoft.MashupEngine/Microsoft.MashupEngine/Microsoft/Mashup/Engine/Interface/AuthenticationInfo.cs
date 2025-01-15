using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000009 RID: 9
	public abstract class AuthenticationInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		public abstract string Name { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020B3 File Offset: 0x000002B3
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020BB File Offset: 0x000002BB
		public string Label { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020C4 File Offset: 0x000002C4
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020CC File Offset: 0x000002CC
		public string Description { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8
		public abstract AuthenticationKind AuthenticationKind { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020D5 File Offset: 0x000002D5
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020DD File Offset: 0x000002DD
		public IList<CredentialProperty> Properties { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020E6 File Offset: 0x000002E6
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020EE File Offset: 0x000002EE
		public IList<CredentialProperty> ApplicationProperties { get; set; }

		// Token: 0x0600000D RID: 13 RVA: 0x000020F7 File Offset: 0x000002F7
		public virtual IResourceCredential Normalize(string resourceKind, IResourceCredential credential)
		{
			return credential;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual string GetPropertyLabel(string propertyName)
		{
			return null;
		}
	}
}
