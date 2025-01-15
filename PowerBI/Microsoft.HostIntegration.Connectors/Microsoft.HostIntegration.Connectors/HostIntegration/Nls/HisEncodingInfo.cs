using System;

namespace Microsoft.HostIntegration.Nls
{
	// Token: 0x0200062E RID: 1582
	public sealed class HisEncodingInfo
	{
		// Token: 0x06003554 RID: 13652 RVA: 0x000B331C File Offset: 0x000B151C
		internal HisEncodingInfo(int cp, string displayname, string name)
		{
			if (cp <= 5035)
			{
				if (cp <= 875)
				{
					if (cp <= 290)
					{
						if (cp != 37 && cp != 290)
						{
							goto IL_0179;
						}
					}
					else if (cp != 500 && cp != 870 && cp != 875)
					{
						goto IL_0179;
					}
				}
				else if (cp <= 1027)
				{
					switch (cp)
					{
					case 930:
					case 931:
					case 933:
					case 935:
					case 937:
					case 939:
						break;
					case 932:
					case 934:
					case 936:
					case 938:
						goto IL_0179;
					default:
						if (cp - 1026 > 1)
						{
							goto IL_0179;
						}
						break;
					}
				}
				else if (cp - 1140 > 9 && cp != 5026 && cp != 5035)
				{
					goto IL_0179;
				}
			}
			else if (cp <= 20424)
			{
				if (cp <= 20285)
				{
					if (cp != 20273)
					{
						switch (cp)
						{
						case 20277:
						case 20278:
						case 20280:
						case 20284:
						case 20285:
							break;
						case 20279:
						case 20281:
						case 20282:
						case 20283:
							goto IL_0179;
						default:
							goto IL_0179;
						}
					}
				}
				else if (cp != 20297 && cp != 20420 && cp - 20423 > 1)
				{
					goto IL_0179;
				}
			}
			else if (cp <= 20871)
			{
				if (cp != 20838 && cp != 20871)
				{
					goto IL_0179;
				}
			}
			else if (cp != 20880 && cp != 20905 && cp != 21025)
			{
				goto IL_0179;
			}
			bool flag = true;
			goto IL_017B;
			IL_0179:
			flag = false;
			IL_017B:
			this.InternalConstructor(cp, displayname, name, flag, false);
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000B34AF File Offset: 0x000B16AF
		internal HisEncodingInfo(int cp, string displayname, string name, bool customCodePage)
		{
			this.InternalConstructor(cp, displayname, name, true, customCodePage);
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000B34C3 File Offset: 0x000B16C3
		private void InternalConstructor(int cp, string displayname, string name, bool ebcidicCodePage, bool customCodePage)
		{
			this.codePage = cp;
			this.displayName = displayname;
			this.encodingName = name;
			this.isEbcdic = ebcidicCodePage;
			this.isCustomCodePage = customCodePage;
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06003557 RID: 13655 RVA: 0x000B34EA File Offset: 0x000B16EA
		public int CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06003558 RID: 13656 RVA: 0x000B34F2 File Offset: 0x000B16F2
		public string DisplayName
		{
			get
			{
				if (this.displayName != null)
				{
					return this.displayName;
				}
				return this.encodingName;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000B3509 File Offset: 0x000B1709
		public string Name
		{
			get
			{
				return this.encodingName;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600355A RID: 13658 RVA: 0x000B3511 File Offset: 0x000B1711
		public bool IsEbcdic
		{
			get
			{
				return this.isEbcdic;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x0600355B RID: 13659 RVA: 0x000B3519 File Offset: 0x000B1719
		public bool IsCustomCodePage
		{
			get
			{
				return this.isCustomCodePage;
			}
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000B3521 File Offset: 0x000B1721
		public HisEncoding GetEncoding()
		{
			return HisEncoding.GetEncoding(this.codePage);
		}

		// Token: 0x04001E90 RID: 7824
		private int codePage;

		// Token: 0x04001E91 RID: 7825
		private string displayName;

		// Token: 0x04001E92 RID: 7826
		private string encodingName;

		// Token: 0x04001E93 RID: 7827
		private bool isEbcdic;

		// Token: 0x04001E94 RID: 7828
		private bool isCustomCodePage;
	}
}
