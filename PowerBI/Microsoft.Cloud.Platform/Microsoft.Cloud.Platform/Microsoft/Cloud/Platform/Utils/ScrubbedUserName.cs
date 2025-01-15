using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001B4 RID: 436
	[DataContract]
	[Serializable]
	public class ScrubbedUserName : IContainsPrivateInformation
	{
		// Token: 0x06000B3F RID: 2879 RVA: 0x00027497 File Offset: 0x00025697
		public ScrubbedUserName(string userName)
		{
			this.m_plainUserName = userName;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x000274A6 File Offset: 0x000256A6
		public override string ToString()
		{
			return this.m_plainUserName;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x000274B0 File Offset: 0x000256B0
		public string ToPrivateString()
		{
			if (this.m_scrubbedUserName == null)
			{
				if (this.m_plainUserName == null)
				{
					return this.m_plainUserName.MarkAsPrivate();
				}
				int num = this.m_plainUserName.IndexOf("@", StringComparison.Ordinal);
				if (num == -1)
				{
					if (!this.IsPuid())
					{
						return this.m_plainUserName.MarkAsPrivate();
					}
					return this.m_plainUserName.MarkAsInternal();
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(this.m_plainUserName.Substring(0, num).MarkAsPrivate());
					stringBuilder.Append('@');
					stringBuilder.Append(this.m_plainUserName.Substring(num + 1).MarkAsInternal());
					this.m_scrubbedUserName = stringBuilder.ToString();
				}
			}
			return this.m_scrubbedUserName;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00027566 File Offset: 0x00025766
		public string ToInternalString()
		{
			return this.m_plainUserName.MarkAsInternal();
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000274A6 File Offset: 0x000256A6
		public string ToOriginalString()
		{
			return this.m_plainUserName;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00027573 File Offset: 0x00025773
		private bool IsPuid()
		{
			return ScrubbedUserName.s_puidRegex.IsMatch(this.m_plainUserName);
		}

		// Token: 0x0400046A RID: 1130
		private static readonly Regex s_puidRegex = new Regex("^[0-9a-fA-F]{16}$", RegexOptions.Compiled);

		// Token: 0x0400046B RID: 1131
		private string m_scrubbedUserName;

		// Token: 0x0400046C RID: 1132
		[DataMember]
		private readonly string m_plainUserName;
	}
}
