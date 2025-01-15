using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Mashup.Security;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B4D RID: 6989
	public class ValidateLibraryEventArgs : EventArgs
	{
		// Token: 0x0600AED6 RID: 44758 RVA: 0x0023CA92 File Offset: 0x0023AC92
		static ValidateLibraryEventArgs()
		{
			ValidateLibraryEventArgs.ResetThumbprints();
		}

		// Token: 0x0600AED7 RID: 44759 RVA: 0x0023CAB8 File Offset: 0x0023ACB8
		public ValidateLibraryEventArgs(LibraryDescription description)
		{
			this.description = description;
			this.category = 0;
			List<string> list = new List<string>(description.Signers.Length);
			foreach (X509Certificate2 x509Certificate in description.Signers)
			{
				int num = ValidateLibraryEventArgs.CategorizeCertificate(x509Certificate);
				if (num > this.category)
				{
					this.category = num;
				}
				if (num == 0)
				{
					list.Add(ValidateLibraryEventArgs.GetKeyTokenFromFullKey(x509Certificate.GetPublicKey()));
				}
			}
			this.tokens = list.ToArray();
		}

		// Token: 0x17002BDA RID: 11226
		// (get) Token: 0x0600AED8 RID: 44760 RVA: 0x0023CB3B File Offset: 0x0023AD3B
		public LibraryDescription LibraryDescription
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17002BDB RID: 11227
		// (get) Token: 0x0600AED9 RID: 44761 RVA: 0x0023CB43 File Offset: 0x0023AD43
		public bool IsCertified
		{
			get
			{
				return this.category == 2;
			}
		}

		// Token: 0x17002BDC RID: 11228
		// (get) Token: 0x0600AEDA RID: 44762 RVA: 0x0023CB4E File Offset: 0x0023AD4E
		public bool IsFirstParty
		{
			get
			{
				return this.category == 3;
			}
		}

		// Token: 0x17002BDD RID: 11229
		// (get) Token: 0x0600AEDB RID: 44763 RVA: 0x0023CB59 File Offset: 0x0023AD59
		public bool IsTrusted
		{
			get
			{
				return this.category == 1;
			}
		}

		// Token: 0x17002BDE RID: 11230
		// (get) Token: 0x0600AEDC RID: 44764 RVA: 0x0023CB64 File Offset: 0x0023AD64
		public bool IsStrongNamed
		{
			get
			{
				string token = ((this.IsCertified && this.description.Signers.Length == 2) ? this.tokens[0] : null);
				return token != null && token == ValidateLibraryEventArgs.ExtractToken(this.description.ModuleName) && this.description.DataSourceKinds.All((string kind) => token == ValidateLibraryEventArgs.ExtractToken(kind));
			}
		}

		// Token: 0x17002BDF RID: 11231
		// (get) Token: 0x0600AEDD RID: 44765 RVA: 0x0023CBE3 File Offset: 0x0023ADE3
		public string[] Tokens
		{
			get
			{
				return this.tokens;
			}
		}

		// Token: 0x17002BE0 RID: 11232
		// (get) Token: 0x0600AEDE RID: 44766 RVA: 0x0023CBEB File Offset: 0x0023ADEB
		// (set) Token: 0x0600AEDF RID: 44767 RVA: 0x0023CBF3 File Offset: 0x0023ADF3
		public bool Validated { get; set; }

		// Token: 0x0600AEE0 RID: 44768 RVA: 0x0023CBFC File Offset: 0x0023ADFC
		public bool Validate()
		{
			int num = this.description.Signers.Length;
			return this.description.Verification == VerifyResult.Success && (((this.IsFirstParty || this.IsCertified) && num == 1) || (this.IsCertified && num == 2 && this.IsStrongNamed));
		}

		// Token: 0x0600AEE1 RID: 44769 RVA: 0x0023CC50 File Offset: 0x0023AE50
		public static string ExtractToken(string identifier)
		{
			if (ValidateLibraryEventArgs.strongNameRegex.IsMatch(identifier))
			{
				return identifier.Substring(identifier.Length - 16);
			}
			return null;
		}

		// Token: 0x0600AEE2 RID: 44770 RVA: 0x0023CC70 File Offset: 0x0023AE70
		internal static void ResetThumbprints()
		{
			Dictionary<string, int> dictionary = ValidateLibraryEventArgs.thumbprints;
			lock (dictionary)
			{
				ValidateLibraryEventArgs.thumbprints.Clear();
				ValidateLibraryEventArgs.thumbprints.Add("8BFE3107712B3C886B1C96AAEC89984914DC9B6B", 3);
				ValidateLibraryEventArgs.thumbprints.Add("F252E794FE438E35ACE6E53762C0A234A2C52135", 2);
			}
		}

		// Token: 0x0600AEE3 RID: 44771 RVA: 0x0023CCD4 File Offset: 0x0023AED4
		internal static void AddFirstPartyThumbprint(string thumbprint)
		{
			Dictionary<string, int> dictionary = ValidateLibraryEventArgs.thumbprints;
			lock (dictionary)
			{
				ValidateLibraryEventArgs.thumbprints[thumbprint] = 3;
			}
		}

		// Token: 0x0600AEE4 RID: 44772 RVA: 0x0023CD1C File Offset: 0x0023AF1C
		internal static void AddCertifiedThumbprint(string thumbprint)
		{
			Dictionary<string, int> dictionary = ValidateLibraryEventArgs.thumbprints;
			lock (dictionary)
			{
				ValidateLibraryEventArgs.thumbprints[thumbprint] = 2;
			}
		}

		// Token: 0x0600AEE5 RID: 44773 RVA: 0x0023CD64 File Offset: 0x0023AF64
		internal static void AddTrustedThumbprint(string thumbprint)
		{
			Dictionary<string, int> dictionary = ValidateLibraryEventArgs.thumbprints;
			lock (dictionary)
			{
				ValidateLibraryEventArgs.thumbprints[thumbprint] = 1;
			}
		}

		// Token: 0x0600AEE6 RID: 44774 RVA: 0x0023CDAC File Offset: 0x0023AFAC
		private static string GetKeyTokenFromFullKey(byte[] fullKey)
		{
			string text;
			using (HashAlgorithm hashAlgorithm = SHA256CryptoProvider.Create())
			{
				byte[] array = hashAlgorithm.ComputeHash(fullKey);
				StringBuilder stringBuilder = new StringBuilder(16);
				for (int i = 0; i < 8; i++)
				{
					byte b = array[array.Length - (i + 1)];
					stringBuilder.Append("0123456789abcdef"[b >> 4]);
					stringBuilder.Append("0123456789abcdef"[(int)(b & 15)]);
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600AEE7 RID: 44775 RVA: 0x0023CE38 File Offset: 0x0023B038
		private static int CategorizeCertificate(X509Certificate2 certificate)
		{
			Dictionary<string, int> dictionary = ValidateLibraryEventArgs.thumbprints;
			int num3;
			lock (dictionary)
			{
				int num;
				if (!ValidateLibraryEventArgs.thumbprints.TryGetValue(certificate.Thumbprint, out num))
				{
					X509Chain x509Chain = new X509Chain();
					x509Chain.Build(certificate);
					int num2 = 0;
					for (int i = 1; i < x509Chain.ChainElements.Count; i++)
					{
						if (ValidateLibraryEventArgs.thumbprints.TryGetValue(x509Chain.ChainElements[i].Certificate.Thumbprint, out num))
						{
							num2 = i;
							break;
						}
					}
					if (num2 == 0)
					{
						num2 = x509Chain.ChainElements.Count;
						num = 0;
					}
					for (int j = 0; j < num2; j++)
					{
						ValidateLibraryEventArgs.thumbprints[x509Chain.ChainElements[j].Certificate.Thumbprint] = num;
					}
				}
				num3 = num;
			}
			return num3;
		}

		// Token: 0x04005A22 RID: 23074
		private const string hexDigits = "0123456789abcdef";

		// Token: 0x04005A23 RID: 23075
		private const int unknownCategory = 0;

		// Token: 0x04005A24 RID: 23076
		private const int trustedCategory = 1;

		// Token: 0x04005A25 RID: 23077
		private const int certifiedCategory = 2;

		// Token: 0x04005A26 RID: 23078
		private const int firstPartyCategory = 3;

		// Token: 0x04005A27 RID: 23079
		private static readonly Regex strongNameRegex = new Regex("^.*_[0-9a-f]{16}$");

		// Token: 0x04005A28 RID: 23080
		private static readonly Dictionary<string, int> thumbprints = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04005A29 RID: 23081
		private readonly LibraryDescription description;

		// Token: 0x04005A2A RID: 23082
		private readonly string[] tokens;

		// Token: 0x04005A2B RID: 23083
		private readonly int category;
	}
}
