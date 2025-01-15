using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200011F RID: 287
	public class StructuredCacheKey : IEquatable<StructuredCacheKey>
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x0000762A File Offset: 0x0000582A
		public StructuredCacheKey(ResourceCredentialCollection credentials, params string[] parts)
		{
			if (parts == null)
			{
				throw new ArgumentNullException("parts");
			}
			this.credentials = credentials;
			this.parts = parts;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000764E File Offset: 0x0000584E
		public ResourceCredentialCollection Credentials
		{
			get
			{
				return this.credentials;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00007656 File Offset: 0x00005856
		public string[] Parts
		{
			get
			{
				return this.parts;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00007660 File Offset: 0x00005860
		public override int GetHashCode()
		{
			ResourceCredentialCollection resourceCredentialCollection = this.credentials;
			int num = ((resourceCredentialCollection != null) ? resourceCredentialCollection.GetHashCode() : 0);
			for (int i = 0; i < this.parts.Length; i++)
			{
				num = num * 17 + this.parts[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000076A7 File Offset: 0x000058A7
		public override bool Equals(object obj)
		{
			return base.Equals(obj as StructuredCacheKey);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000076B8 File Offset: 0x000058B8
		public bool Equals(StructuredCacheKey other)
		{
			if (other == null || this.parts.Length != other.parts.Length || this.credentials == null != (other.credentials == null) || (this.credentials != null && StructuredCacheKey.AreEqual(this.credentials, other.credentials)))
			{
				return false;
			}
			for (int i = 0; i < this.parts.Length; i++)
			{
				if (this.parts[i] != other.parts[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00007738 File Offset: 0x00005938
		private static bool AreEqual(ResourceCredentialCollection x, ResourceCredentialCollection y)
		{
			bool flag3;
			using (IEnumerator<string> enumerator = x.SelectMany((IResourceCredential c) => c.GetCacheParts()).GetEnumerator())
			{
				using (IEnumerator<string> enumerator2 = y.SelectMany((IResourceCredential c) => c.GetCacheParts()).GetEnumerator())
				{
					for (;;)
					{
						bool flag = enumerator.MoveNext();
						bool flag2 = enumerator2.MoveNext();
						if (flag != flag2)
						{
							break;
						}
						if (!flag)
						{
							goto Block_7;
						}
						if (enumerator.Current != enumerator2.Current)
						{
							goto Block_8;
						}
					}
					return false;
					Block_7:
					return true;
					Block_8:
					flag3 = false;
				}
			}
			return flag3;
		}

		// Token: 0x040002C8 RID: 712
		private readonly ResourceCredentialCollection credentials;

		// Token: 0x040002C9 RID: 713
		private readonly string[] parts;
	}
}
