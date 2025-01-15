using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200206A RID: 8298
	public class UserAnnotationAdornmentCredential : IResourceCredential, IEquatable<IResourceCredential>
	{
		// Token: 0x0600CB2F RID: 52015 RVA: 0x00288372 File Offset: 0x00286572
		public UserAnnotationAdornmentCredential(UserAnnotationAdornmentCredentialData annotation)
		{
			this.annotation = annotation;
		}

		// Token: 0x170030ED RID: 12525
		// (get) Token: 0x0600CB30 RID: 52016 RVA: 0x00288381 File Offset: 0x00286581
		public UserAnnotationAdornmentCredentialData Annotation
		{
			get
			{
				return this.annotation;
			}
		}

		// Token: 0x0600CB31 RID: 52017 RVA: 0x00191195 File Offset: 0x0018F395
		public IEnumerable<string> GetCacheParts()
		{
			return EmptyArray<string>.Instance;
		}

		// Token: 0x0600CB32 RID: 52018 RVA: 0x00002105 File Offset: 0x00000305
		public bool Equals(IResourceCredential other)
		{
			return false;
		}

		// Token: 0x04006719 RID: 26393
		private UserAnnotationAdornmentCredentialData annotation;
	}
}
