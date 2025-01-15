using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200000B RID: 11
	public sealed class UsernamePasswordAuthenticationInfo : AuthenticationInfo
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002117 File Offset: 0x00000317
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000211F File Offset: 0x0000031F
		public string UsernameLabel { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002130 File Offset: 0x00000330
		public string PasswordLabel { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002139 File Offset: 0x00000339
		public override AuthenticationKind AuthenticationKind
		{
			get
			{
				return AuthenticationKind.UsernamePassword;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000213C File Offset: 0x0000033C
		public override string Name
		{
			get
			{
				return "UsernamePassword";
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002144 File Offset: 0x00000344
		public override IResourceCredential Normalize(string resourceKind, IResourceCredential credential)
		{
			UsernamePasswordCredential usernamePasswordCredential = (UsernamePasswordCredential)credential;
			if (resourceKind != null)
			{
				int length = resourceKind.Length;
				switch (length)
				{
				case 3:
				{
					char c = resourceKind[0];
					if (c <= 'F')
					{
						if (c != 'D')
						{
							if (c != 'F')
							{
								goto IL_0202;
							}
							if (!(resourceKind == "Ftp"))
							{
								goto IL_0202;
							}
							return new FtpLoginAuthCredential(usernamePasswordCredential.Username, usernamePasswordCredential.Password);
						}
						else
						{
							if (!(resourceKind == "DB2"))
							{
								goto IL_0202;
							}
							goto IL_01DE;
						}
					}
					else if (c != 'S')
					{
						if (c != 'W')
						{
							goto IL_0202;
						}
						if (!(resourceKind == "Web"))
						{
							goto IL_0202;
						}
					}
					else
					{
						if (!(resourceKind == "SQL"))
						{
							goto IL_0202;
						}
						goto IL_01DE;
					}
					break;
				}
				case 4:
				case 7:
				case 9:
					goto IL_0202;
				case 5:
				{
					char c = resourceKind[0];
					if (c != 'M')
					{
						if (c != 'O')
						{
							goto IL_0202;
						}
						if (!(resourceKind == "OData"))
						{
							goto IL_0202;
						}
					}
					else
					{
						if (!(resourceKind == "MySql"))
						{
							goto IL_0202;
						}
						goto IL_01DE;
					}
					break;
				}
				case 6:
				{
					char c = resourceKind[0];
					if (c != 'O')
					{
						if (c != 'S')
						{
							goto IL_0202;
						}
						if (!(resourceKind == "Sybase"))
						{
							goto IL_0202;
						}
						goto IL_01DE;
					}
					else
					{
						if (!(resourceKind == "Oracle"))
						{
							goto IL_0202;
						}
						goto IL_01DE;
					}
					break;
				}
				case 8:
				{
					char c = resourceKind[0];
					if (c != 'I')
					{
						if (c != 'T')
						{
							goto IL_0202;
						}
						if (!(resourceKind == "Teradata"))
						{
							goto IL_0202;
						}
						goto IL_01DE;
					}
					else
					{
						if (!(resourceKind == "Informix"))
						{
							goto IL_0202;
						}
						goto IL_01DE;
					}
					break;
				}
				case 10:
					if (!(resourceKind == "PostgreSQL"))
					{
						goto IL_0202;
					}
					goto IL_01DE;
				default:
					if (length != 15)
					{
						if (length != 16)
						{
							goto IL_0202;
						}
						if (!(resourceKind == "AnalysisServices"))
						{
							goto IL_0202;
						}
					}
					else if (!(resourceKind == "ActiveDirectory"))
					{
						goto IL_0202;
					}
					break;
				}
				return new BasicAuthCredential(usernamePasswordCredential.Username, usernamePasswordCredential.Password);
				IL_01DE:
				return new SqlAuthCredential(usernamePasswordCredential.Username, usernamePasswordCredential.Password);
			}
			IL_0202:
			return new BasicAuthCredential(usernamePasswordCredential.Username, usernamePasswordCredential.Password);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002364 File Offset: 0x00000564
		public override string GetPropertyLabel(string propertyName)
		{
			if (propertyName == "Username")
			{
				return this.UsernameLabel;
			}
			if (!(propertyName == "Password"))
			{
				return null;
			}
			return this.PasswordLabel;
		}
	}
}
