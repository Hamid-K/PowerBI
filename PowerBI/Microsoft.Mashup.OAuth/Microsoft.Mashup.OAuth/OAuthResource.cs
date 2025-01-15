using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200001C RID: 28
	public sealed class OAuthResource
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00004A34 File Offset: 0x00002C34
		internal OAuthResource(string resource, string scope, ISecureTokenService tokenService, Dictionary<string, string> properties)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			if (scope == null)
			{
				throw new ArgumentNullException("scope");
			}
			this.tokenService = tokenService;
			this.resource = resource;
			this.scope = scope;
			this.properties = properties ?? new Dictionary<string, string>();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004A8C File Offset: 0x00002C8C
		public static OAuthResource CreateResource(Uri authorizationUri, Uri tokenUri, Uri logoutUri, string resource, string scope)
		{
			ISecureTokenService secureTokenService = new SecureTokenService(authorizationUri.GetLeftPart(UriPartial.Authority), authorizationUri.AbsoluteUri, tokenUri.AbsoluteUri, logoutUri.AbsoluteUri, "{TENANT}", "common");
			return new OAuthResource(resource, scope, secureTokenService, new Dictionary<string, string>());
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004AD0 File Offset: 0x00002CD0
		public string Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public string Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public ISecureTokenService TokenService
		{
			get
			{
				return this.tokenService;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004AE8 File Offset: 0x00002CE8
		public Dictionary<string, string> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public IEnumerable<string> Resources
		{
			get
			{
				if (this.resources == null)
				{
					KeyValuePair<string, string>[] array;
					if (this.resource.Length > 0)
					{
						this.resources = new string[] { this.resource };
						this.scopes = new string[] { this.scope };
					}
					else if (OAuthResource.TryExtractResourceFromScopes(this.scope, out array))
					{
						this.resources = new string[array.Length];
						this.scopes = new string[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							this.resources[i] = array[i].Key;
							this.scopes[i] = array[i].Value;
						}
					}
					else
					{
						this.resources = new string[0];
						this.scopes = this.resources;
					}
				}
				return this.resources;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004BC4 File Offset: 0x00002DC4
		public static bool TryExtractResourceFromScopes(string scope, out KeyValuePair<string, string>[] resources)
		{
			scope = ((scope == null) ? null : scope.Trim().ToLowerInvariant());
			if (string.IsNullOrEmpty(scope))
			{
				resources = null;
				return false;
			}
			string[] array = scope.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			Dictionary<string, string> dictionary = new Dictionary<string, string>(array.Length);
			List<string> list = new List<string>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				string text = null;
				string text2 = null;
				Uri uri;
				if (Uri.TryCreate(array[i], UriKind.Absolute, out uri) && uri.Segments.Length != 1 && !uri.Segments[uri.Segments.Length - 1].EndsWith("/", StringComparison.Ordinal))
				{
					int num = uri.AbsoluteUri.LastIndexOf('/');
					text = uri.AbsoluteUri.Substring(0, num);
					text2 = uri.AbsoluteUri.Substring(num + 1);
				}
				else
				{
					int num2 = array[i].LastIndexOf('/');
					if (num2 >= 0)
					{
						text = array[i].Substring(0, num2);
						text2 = array[i].Substring(num2 + 1);
						try
						{
							new Guid(text);
						}
						catch (FormatException)
						{
							text = null;
							text2 = null;
						}
					}
				}
				if (text == null)
				{
					resources = null;
					return false;
				}
				string text3;
				if (dictionary.TryGetValue(text, out text3))
				{
					text3 = text3 + " " + text2;
				}
				else
				{
					text3 = text2;
					list.Add(text);
				}
				dictionary[text] = text3;
			}
			resources = new KeyValuePair<string, string>[dictionary.Count];
			for (int j = 0; j < dictionary.Count; j++)
			{
				resources[j] = new KeyValuePair<string, string>(list[j], dictionary[list[j]]);
			}
			return true;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004D74 File Offset: 0x00002F74
		public bool TryGetScopeForResource(string resource, out string scope)
		{
			string[] array = (string[])this.Resources;
			for (int i = 0; i < array.Length; i++)
			{
				if (string.Equals(resource, array[i], StringComparison.OrdinalIgnoreCase))
				{
					scope = this.scopes[i];
					return true;
				}
			}
			scope = null;
			return false;
		}

		// Token: 0x040000B6 RID: 182
		private readonly string resource;

		// Token: 0x040000B7 RID: 183
		private readonly string scope;

		// Token: 0x040000B8 RID: 184
		private readonly ISecureTokenService tokenService;

		// Token: 0x040000B9 RID: 185
		private readonly Dictionary<string, string> properties;

		// Token: 0x040000BA RID: 186
		private string[] resources;

		// Token: 0x040000BB RID: 187
		private string[] scopes;
	}
}
