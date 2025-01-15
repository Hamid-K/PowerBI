using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004BC RID: 1212
	public abstract class EntityTypeBase : StructuralType
	{
		// Token: 0x06003BED RID: 15341 RVA: 0x000C6ABA File Offset: 0x000C4CBA
		internal EntityTypeBase(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
			this._keyMembers = new ReadOnlyMetadataCollection<EdmMember>(new MetadataCollection<EdmMember>());
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06003BEE RID: 15342 RVA: 0x000C6AE0 File Offset: 0x000C4CE0
		[MetadataProperty(BuiltInTypeKind.EdmMember, true)]
		public virtual ReadOnlyMetadataCollection<EdmMember> KeyMembers
		{
			get
			{
				if (this.BaseType != null && ((EntityTypeBase)this.BaseType).KeyMembers.Count != 0)
				{
					return ((EntityTypeBase)this.BaseType).KeyMembers;
				}
				return this._keyMembers;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x000C6B18 File Offset: 0x000C4D18
		public virtual ReadOnlyMetadataCollection<EdmProperty> KeyProperties
		{
			get
			{
				ReadOnlyMetadataCollection<EdmProperty> readOnlyMetadataCollection = this._keyProperties;
				if (readOnlyMetadataCollection == null)
				{
					object keyPropertiesSync = this._keyPropertiesSync;
					lock (keyPropertiesSync)
					{
						if (this._keyProperties == null)
						{
							this.KeyMembers.SourceAccessed += this.KeyMembersSourceAccessedEventHandler;
							this._keyProperties = new ReadOnlyMetadataCollection<EdmProperty>(this.KeyMembers.Cast<EdmProperty>().ToList<EdmProperty>());
						}
						readOnlyMetadataCollection = this._keyProperties;
					}
				}
				return readOnlyMetadataCollection;
			}
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x000C6BA0 File Offset: 0x000C4DA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ResetKeyPropertiesCache()
		{
			if (this._keyProperties != null)
			{
				object keyPropertiesSync = this._keyPropertiesSync;
				lock (keyPropertiesSync)
				{
					if (this._keyProperties != null)
					{
						this._keyProperties = null;
						this.KeyMembers.SourceAccessed -= this.KeyMembersSourceAccessedEventHandler;
					}
				}
			}
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x000C6C08 File Offset: 0x000C4E08
		private void KeyMembersSourceAccessedEventHandler(object sender, EventArgs e)
		{
			this.ResetKeyPropertiesCache();
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000C6C10 File Offset: 0x000C4E10
		internal virtual string[] KeyMemberNames
		{
			get
			{
				if (this._keyMemberNames == null)
				{
					string[] array = new string[this.KeyMembers.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this.KeyMembers[i].Name;
					}
					this._keyMemberNames = array;
				}
				return this._keyMemberNames;
			}
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x000C6C67 File Offset: 0x000C4E67
		public void AddKeyMember(EdmMember member)
		{
			Check.NotNull<EdmMember>(member, "member");
			Util.ThrowIfReadOnly(this);
			if (!base.Members.Contains(member))
			{
				base.AddMember(member);
			}
			this._keyMembers.Source.Add(member);
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x000C6CA1 File Offset: 0x000C4EA1
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				this._keyMembers.Source.SetReadOnly();
				base.SetReadOnly();
			}
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x000C6CC4 File Offset: 0x000C4EC4
		internal static void CheckAndAddMembers(IEnumerable<EdmMember> members, EntityType entityType)
		{
			foreach (EdmMember edmMember in members)
			{
				if (edmMember == null)
				{
					throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("members"));
				}
				entityType.AddMember(edmMember);
			}
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x000C6D20 File Offset: 0x000C4F20
		internal void CheckAndAddKeyMembers(IEnumerable<string> keyMembers)
		{
			foreach (string text in keyMembers)
			{
				if (text == null)
				{
					throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("keyMembers"));
				}
				EdmMember edmMember;
				if (!base.Members.TryGetValue(text, false, out edmMember))
				{
					throw new ArgumentException(Strings.InvalidKeyMember(text));
				}
				this.AddKeyMember(edmMember);
			}
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x000C6D98 File Offset: 0x000C4F98
		public override void RemoveMember(EdmMember member)
		{
			Check.NotNull<EdmMember>(member, "member");
			Util.ThrowIfReadOnly(this);
			if (this._keyMembers.Contains(member))
			{
				this._keyMembers.Source.Remove(member);
			}
			base.RemoveMember(member);
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x000C6DD3 File Offset: 0x000C4FD3
		internal override void NotifyItemIdentityChanged(EdmMember item, string initialIdentity)
		{
			base.NotifyItemIdentityChanged(item, initialIdentity);
			this._keyMembers.Source.HandleIdentityChange(item, initialIdentity);
		}

		// Token: 0x040014A3 RID: 5283
		private readonly ReadOnlyMetadataCollection<EdmMember> _keyMembers;

		// Token: 0x040014A4 RID: 5284
		private readonly object _keyPropertiesSync = new object();

		// Token: 0x040014A5 RID: 5285
		private ReadOnlyMetadataCollection<EdmProperty> _keyProperties;

		// Token: 0x040014A6 RID: 5286
		private string[] _keyMemberNames;
	}
}
