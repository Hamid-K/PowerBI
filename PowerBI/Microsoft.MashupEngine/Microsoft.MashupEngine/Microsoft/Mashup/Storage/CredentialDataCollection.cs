using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002051 RID: 8273
	[XmlRoot("Credentials")]
	public class CredentialDataCollection : XmlRoot
	{
		// Token: 0x0600CA65 RID: 51813 RVA: 0x002871FB File Offset: 0x002853FB
		public CredentialDataCollection()
		{
			this.credentials = new List<CredentialData>();
		}

		// Token: 0x170030AF RID: 12463
		// (get) Token: 0x0600CA66 RID: 51814 RVA: 0x0028720E File Offset: 0x0028540E
		[XmlArray("Credentials")]
		[XmlArrayItem("Credential")]
		public List<CredentialData> Credentials
		{
			get
			{
				return this.credentials;
			}
		}

		// Token: 0x0600CA67 RID: 51815 RVA: 0x00287218 File Offset: 0x00285418
		public CredentialDataCollection Merge(CredentialDataCollection newCredentials)
		{
			List<CredentialData> list = new List<CredentialData>();
			if (newCredentials != null)
			{
				for (int i = 0; i < newCredentials.Credentials.Count; i++)
				{
					if (i < this.Credentials.Count && this.Credentials[i].TryMergeWith(newCredentials.Credentials[i]))
					{
						this.credentials[i].Validate();
						list.Add(this.Credentials[i]);
					}
					else
					{
						newCredentials.Credentials[i].InitializeWithDefaults();
						newCredentials.Credentials[i].Validate();
						list.Add(newCredentials.Credentials[i]);
					}
				}
			}
			CredentialDataCollection credentialDataCollection = new CredentialDataCollection();
			credentialDataCollection.Credentials.AddRange(list);
			return credentialDataCollection;
		}

		// Token: 0x0600CA68 RID: 51816 RVA: 0x002872E4 File Offset: 0x002854E4
		public ResourceCredentialCollection ToResourceCredentials(IResource resource, IdentityContext context = null, bool removeUserAnnotations = true)
		{
			List<IResourceCredential> list = new List<IResourceCredential>();
			for (int i = 0; i < this.Credentials.Count; i++)
			{
				if (!removeUserAnnotations || !(this.Credentials[i] is UserAnnotationAdornmentCredentialData))
				{
					list.AddRange(this.Credentials[i].ToResourceCredentialArray(context));
				}
			}
			return new ResourceCredentialCollection(resource, list);
		}

		// Token: 0x0600CA69 RID: 51817 RVA: 0x00287344 File Offset: 0x00285544
		public CredentialData RemoveAdornments(out CredentialData[] adornments)
		{
			CredentialData credentialData;
			if (this.credentials.Count == 0)
			{
				credentialData = null;
				adornments = EmptyArray<CredentialData>.Instance;
			}
			else if (this.credentials[0].Kind == CredentialDataKind.Credential)
			{
				credentialData = this.credentials[0];
				adornments = new CredentialData[this.credentials.Count - 1];
				this.credentials.CopyTo(1, adornments, 0, adornments.Length);
			}
			else
			{
				credentialData = null;
				adornments = this.credentials.ToArray();
			}
			return credentialData;
		}

		// Token: 0x040066FC RID: 26364
		private List<CredentialData> credentials;
	}
}
