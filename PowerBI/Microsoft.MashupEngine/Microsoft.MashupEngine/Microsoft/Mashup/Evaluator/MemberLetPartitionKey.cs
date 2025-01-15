using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CEF RID: 7407
	internal sealed class MemberLetPartitionKey : IMemberLetPartitionKey, IPartitionKey, IEquatable<IPartitionKey>
	{
		// Token: 0x0600B8F3 RID: 47347 RVA: 0x00257DB7 File Offset: 0x00255FB7
		public MemberLetPartitionKey(string section, string member)
			: this(section, member, EmptyArray<string>.Instance)
		{
		}

		// Token: 0x0600B8F4 RID: 47348 RVA: 0x00257DC6 File Offset: 0x00255FC6
		public MemberLetPartitionKey(string section, string member, params string[] lets)
		{
			this.section = section;
			this.member = member;
			this.lets = lets;
		}

		// Token: 0x17002DC2 RID: 11714
		// (get) Token: 0x0600B8F5 RID: 47349 RVA: 0x00002139 File Offset: 0x00000339
		public PartitioningScheme PartitioningScheme
		{
			get
			{
				return PartitioningScheme.MemberLet1;
			}
		}

		// Token: 0x0600B8F6 RID: 47350 RVA: 0x00257DE3 File Offset: 0x00255FE3
		public override string ToString()
		{
			return this.ToSerializedString();
		}

		// Token: 0x17002DC3 RID: 11715
		// (get) Token: 0x0600B8F7 RID: 47351 RVA: 0x00257DEB File Offset: 0x00255FEB
		public string Section
		{
			get
			{
				return this.section;
			}
		}

		// Token: 0x17002DC4 RID: 11716
		// (get) Token: 0x0600B8F8 RID: 47352 RVA: 0x00257DF3 File Offset: 0x00255FF3
		public string Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x17002DC5 RID: 11717
		// (get) Token: 0x0600B8F9 RID: 47353 RVA: 0x00257DFB File Offset: 0x00255FFB
		public IList<string> Lets
		{
			get
			{
				return this.lets;
			}
		}

		// Token: 0x0600B8FA RID: 47354 RVA: 0x00257E03 File Offset: 0x00256003
		public bool Equals(IPartitionKey partitionKey)
		{
			return PartitionKeyEqualityComparer.Instance.Equals(this, partitionKey);
		}

		// Token: 0x04005E2A RID: 24106
		private readonly string section;

		// Token: 0x04005E2B RID: 24107
		private readonly string member;

		// Token: 0x04005E2C RID: 24108
		private readonly string[] lets;
	}
}
