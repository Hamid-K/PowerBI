using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x0200008E RID: 142
	internal class X509Certificate2FromFileProvider : IX509Certificate2Provider
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000E24A File Offset: 0x0000C44A
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0000E252 File Offset: 0x0000C452
		private X509Certificate2 Certificate { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000E25B File Offset: 0x0000C45B
		internal string CertificatePath { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000E263 File Offset: 0x0000C463
		internal string CertificatePassword { get; }

		// Token: 0x0600049A RID: 1178 RVA: 0x0000E26B File Offset: 0x0000C46B
		public X509Certificate2FromFileProvider(string clientCertificatePath, string certificatePassword)
		{
			Argument.AssertNotNull<string>(clientCertificatePath, "clientCertificatePath");
			if (clientCertificatePath == null)
			{
				throw new ArgumentNullException("clientCertificatePath");
			}
			this.CertificatePath = clientCertificatePath;
			this.CertificatePassword = certificatePassword;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000E29C File Offset: 0x0000C49C
		public ValueTask<X509Certificate2> GetCertificateAsync(bool async, CancellationToken cancellationToken)
		{
			if (this.Certificate != null)
			{
				return new ValueTask<X509Certificate2>(this.Certificate);
			}
			string text = Path.GetExtension(this.CertificatePath).ToLowerInvariant();
			if (text == ".pfx")
			{
				return this.LoadCertificateFromPfxFileAsync(async, this.CertificatePath, this.CertificatePassword, cancellationToken);
			}
			if (!(text == ".pem"))
			{
				throw new CredentialUnavailableException("Only .pfx and .pem files are supported.");
			}
			if (this.CertificatePassword != null)
			{
				throw new CredentialUnavailableException("Password protection for PEM encoded certificates is not supported.");
			}
			return this.LoadCertificateFromPemFileAsync(async, this.CertificatePath, cancellationToken);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000E32C File Offset: 0x0000C52C
		private async ValueTask<X509Certificate2> LoadCertificateFromPfxFileAsync(bool async, string clientCertificatePath, string certificatePassword, CancellationToken cancellationToken)
		{
			X509Certificate2 x509Certificate;
			if (this.Certificate != null)
			{
				x509Certificate = this.Certificate;
			}
			else
			{
				try
				{
					if (!async)
					{
						this.Certificate = new X509Certificate2(clientCertificatePath, certificatePassword);
					}
					else
					{
						List<byte> certContents = new List<byte>();
						byte[] buf = new byte[4096];
						int offset = 0;
						using (Stream s = File.OpenRead(clientCertificatePath))
						{
							int num;
							do
							{
								num = await s.ReadAsync(buf, offset, buf.Length, cancellationToken).ConfigureAwait(false);
								for (int i = 0; i < num; i++)
								{
									certContents.Add(buf[i]);
								}
							}
							while (num != 0);
						}
						Stream s = null;
						this.Certificate = new X509Certificate2(certContents.ToArray(), certificatePassword);
						certContents = null;
						buf = null;
					}
					x509Certificate = this.Certificate;
				}
				catch (Exception ex) when (!(ex is OperationCanceledException))
				{
					throw new CredentialUnavailableException("Could not load certificate file", ex);
				}
			}
			return x509Certificate;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000E390 File Offset: 0x0000C590
		private async ValueTask<X509Certificate2> LoadCertificateFromPemFileAsync(bool async, string clientCertificatePath, CancellationToken cancellationToken)
		{
			X509Certificate2 x509Certificate;
			if (this.Certificate != null)
			{
				x509Certificate = this.Certificate;
			}
			else
			{
				try
				{
					string text;
					if (!async)
					{
						text = File.ReadAllText(clientCertificatePath);
					}
					else
					{
						cancellationToken.ThrowIfCancellationRequested();
						using (StreamReader sr = new StreamReader(clientCertificatePath))
						{
							text = await sr.ReadToEndAsync().ConfigureAwait(false);
						}
						StreamReader sr = null;
					}
					this.Certificate = PemReader.LoadCertificate(MemoryExtensions.AsSpan(text), null, PemReader.KeyType.RSA, false, X509KeyStorageFlags.DefaultKeySet);
					x509Certificate = this.Certificate;
				}
				catch (Exception ex) when (!(ex is OperationCanceledException))
				{
					throw new CredentialUnavailableException("Could not load certificate file", ex);
				}
			}
			return x509Certificate;
		}

		// Token: 0x02000138 RID: 312
		// (Invoke) Token: 0x0600063E RID: 1598
		private delegate void ImportPkcs8PrivateKeyDelegate(ReadOnlySpan<byte> blob, out int bytesRead);
	}
}
