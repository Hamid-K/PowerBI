using System;
using System.Xml.Serialization;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x0200206D RID: 8301
	public class CredentialsConversion : XmlRoot
	{
		// Token: 0x0600CB33 RID: 52019 RVA: 0x00288389 File Offset: 0x00286589
		public CredentialsConversion()
			: this(1, false)
		{
		}

		// Token: 0x0600CB34 RID: 52020 RVA: 0x00288393 File Offset: 0x00286593
		public CredentialsConversion(int credentialStorageVersion)
			: this(credentialStorageVersion, false)
		{
		}

		// Token: 0x0600CB35 RID: 52021 RVA: 0x0028839D File Offset: 0x0028659D
		public CredentialsConversion(int credentialStorageVersion, bool isCertificateRemoved)
		{
			this.credentialStorageVersion = credentialStorageVersion;
			this.isCertificateRemoved = isCertificateRemoved;
		}

		// Token: 0x170030EE RID: 12526
		// (get) Token: 0x0600CB36 RID: 52022 RVA: 0x002883B3 File Offset: 0x002865B3
		// (set) Token: 0x0600CB37 RID: 52023 RVA: 0x002883BB File Offset: 0x002865BB
		[XmlElement("Entry")]
		public int CredentialStorageVersion
		{
			get
			{
				return this.credentialStorageVersion;
			}
			set
			{
				this.credentialStorageVersion = value;
			}
		}

		// Token: 0x170030EF RID: 12527
		// (get) Token: 0x0600CB38 RID: 52024 RVA: 0x002883C4 File Offset: 0x002865C4
		// (set) Token: 0x0600CB39 RID: 52025 RVA: 0x002883CC File Offset: 0x002865CC
		[XmlElement("IsCertificateRemovedEntry")]
		public bool IsCertificateRemoved
		{
			get
			{
				return this.isCertificateRemoved;
			}
			set
			{
				this.isCertificateRemoved = value;
			}
		}

		// Token: 0x04006729 RID: 26409
		private int credentialStorageVersion;

		// Token: 0x0400672A RID: 26410
		private bool isCertificateRemoved;
	}
}
