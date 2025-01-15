using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002068 RID: 8296
	public abstract class UserAnnotationAdornmentCredentialData : CredentialData
	{
		// Token: 0x0600CB23 RID: 52003 RVA: 0x00287642 File Offset: 0x00285842
		public UserAnnotationAdornmentCredentialData()
		{
		}

		// Token: 0x170030E9 RID: 12521
		// (get) Token: 0x0600CB24 RID: 52004 RVA: 0x00002139 File Offset: 0x00000339
		public sealed override CredentialDataKind Kind
		{
			get
			{
				return CredentialDataKind.Adornment;
			}
		}

		// Token: 0x0600CB25 RID: 52005 RVA: 0x002882F8 File Offset: 0x002864F8
		public sealed override IResourceCredential ToResourceCredential(IdentityContext context = null)
		{
			return new UserAnnotationAdornmentCredential(this);
		}
	}
}
