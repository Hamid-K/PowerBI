using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200048C RID: 1164
	internal sealed class ClrEntityType : EntityType
	{
		// Token: 0x060039B2 RID: 14770 RVA: 0x000BDF74 File Offset: 0x000BC174
		internal ClrEntityType(Type type, string cspaceNamespaceName, string cspaceTypeName)
			: base(Check.NotNull<Type>(type, "type").Name, type.NestingNamespace() ?? string.Empty, DataSpace.OSpace)
		{
			this._type = type;
			this._cspaceNamespaceName = cspaceNamespaceName;
			this._cspaceTypeName = cspaceNamespaceName + "." + cspaceTypeName;
			base.Abstract = type.IsAbstract();
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000BDFD3 File Offset: 0x000BC1D3
		// (set) Token: 0x060039B4 RID: 14772 RVA: 0x000BDFDB File Offset: 0x000BC1DB
		internal Func<object> Constructor
		{
			get
			{
				return this._constructor;
			}
			set
			{
				Interlocked.CompareExchange<Func<object>>(ref this._constructor, value, null);
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x000BDFEB File Offset: 0x000BC1EB
		internal override Type ClrType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060039B6 RID: 14774 RVA: 0x000BDFF3 File Offset: 0x000BC1F3
		internal string CSpaceTypeName
		{
			get
			{
				return this._cspaceTypeName;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060039B7 RID: 14775 RVA: 0x000BDFFB File Offset: 0x000BC1FB
		internal string CSpaceNamespaceName
		{
			get
			{
				return this._cspaceNamespaceName;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x000BE003 File Offset: 0x000BC203
		internal string HashedDescription
		{
			get
			{
				if (this._hash == null)
				{
					Interlocked.CompareExchange<string>(ref this._hash, this.BuildEntityTypeHash(), null);
				}
				return this._hash;
			}
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x000BE028 File Offset: 0x000BC228
		private string BuildEntityTypeHash()
		{
			string text;
			using (SHA256 sha = MetadataHelper.CreateSHA256HashAlgorithm())
			{
				byte[] array = sha.ComputeHash(Encoding.ASCII.GetBytes(this.BuildEntityTypeDescription()));
				StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
				foreach (byte b in array)
				{
					stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000BE0B0 File Offset: 0x000BC2B0
		private string BuildEntityTypeDescription()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("CLR:").Append(this.ClrType.FullName);
			stringBuilder.Append("Conceptual:").Append(this.CSpaceTypeName);
			SortedSet<string> sortedSet = new SortedSet<string>();
			foreach (NavigationProperty navigationProperty in base.NavigationProperties)
			{
				sortedSet.Add(string.Concat(new string[]
				{
					navigationProperty.Name,
					"*",
					navigationProperty.FromEndMember.Name,
					"*",
					navigationProperty.FromEndMember.RelationshipMultiplicity.ToString(),
					"*",
					navigationProperty.ToEndMember.Name,
					"*",
					navigationProperty.ToEndMember.RelationshipMultiplicity.ToString(),
					"*"
				}));
			}
			stringBuilder.Append("NavProps:");
			foreach (string text in sortedSet)
			{
				stringBuilder.Append(text);
			}
			SortedSet<string> sortedSet2 = new SortedSet<string>();
			foreach (string text2 in this.KeyMemberNames)
			{
				sortedSet2.Add(text2);
			}
			stringBuilder.Append("Keys:");
			foreach (string text3 in sortedSet2)
			{
				stringBuilder.Append(text3 + "*");
			}
			SortedSet<string> sortedSet3 = new SortedSet<string>();
			foreach (EdmMember edmMember in base.Members)
			{
				if (!sortedSet2.Contains(edmMember.Name))
				{
					sortedSet3.Add(edmMember.Name + "*");
				}
			}
			stringBuilder.Append("Scalars:");
			foreach (string text4 in sortedSet3)
			{
				stringBuilder.Append(text4 + "*");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001341 RID: 4929
		private readonly Type _type;

		// Token: 0x04001342 RID: 4930
		private Func<object> _constructor;

		// Token: 0x04001343 RID: 4931
		private readonly string _cspaceTypeName;

		// Token: 0x04001344 RID: 4932
		private readonly string _cspaceNamespaceName;

		// Token: 0x04001345 RID: 4933
		private string _hash;
	}
}
