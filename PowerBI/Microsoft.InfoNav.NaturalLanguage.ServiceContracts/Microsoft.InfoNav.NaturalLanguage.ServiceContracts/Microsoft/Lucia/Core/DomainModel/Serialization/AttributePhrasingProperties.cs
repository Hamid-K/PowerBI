using System;
using System.Collections.Generic;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001BA RID: 442
	public sealed class AttributePhrasingProperties : PhrasingProperties, ICustomSerializationOptions
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00011B8B File Offset: 0x0000FD8B
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x00011B93 File Offset: 0x0000FD93
		[JsonProperty(Required = Required.Always)]
		public RoleReference Subject { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00011B9C File Offset: 0x0000FD9C
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x00011BA4 File Offset: 0x0000FDA4
		[JsonProperty(Required = Required.Always)]
		public RoleReference Object { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00011BAD File Offset: 0x0000FDAD
		[JsonProperty]
		public List<PrepPhrase> PrepositionalPhrases { get; } = new List<PrepPhrase>();

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00011BB8 File Offset: 0x0000FDB8
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				string text;
				string text2;
				if (this.Subject.TryGetScalarForm(out text) && this.Object.TryGetScalarForm(out text2) && this.PrepositionalPhrases.Count == 0)
				{
					return YamlSerializationOptions.Compact;
				}
				return YamlSerializationOptions.None;
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00011BF3 File Offset: 0x0000FDF3
		public override void Accept(IPhrasingVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00011BFC File Offset: 0x0000FDFC
		public override T Accept<T>(IPhrasingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00011C05 File Offset: 0x0000FE05
		public override T Accept<T, TArg>(IPhrasingVisitor<T, TArg> visitor, TArg arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00011C0F File Offset: 0x0000FE0F
		public bool ShouldSerializePrepositionalPhrases()
		{
			return this.PrepositionalPhrases.Count > 0;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00011C1F File Offset: 0x0000FE1F
		internal override IEnumerable<RoleReference> GetRoleReferences()
		{
			if (this.Subject != null)
			{
				yield return this.Subject;
			}
			if (this.Object != null)
			{
				yield return this.Object;
			}
			if (this.PrepositionalPhrases != null)
			{
				foreach (PrepPhrase prepPhrase in this.PrepositionalPhrases)
				{
					if (prepPhrase.Object != null)
					{
						yield return prepPhrase.Object;
					}
				}
				List<PrepPhrase>.Enumerator enumerator = default(List<PrepPhrase>.Enumerator);
			}
			yield break;
			yield break;
		}
	}
}
