using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200008B RID: 139
	internal sealed class ClrEntityType : EntityType
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x000181C0 File Offset: 0x000163C0
		internal ClrEntityType(Type type, string cspaceNamespaceName, string cspaceTypeName)
			: base(EntityUtil.GenericCheckArgumentNull<Type>(type, "type").Name, type.Namespace ?? string.Empty, DataSpace.OSpace)
		{
			this._type = type.TypeHandle;
			this._cspaceNamespaceName = cspaceNamespaceName;
			this._cspaceTypeName = cspaceNamespaceName + "." + cspaceTypeName;
			base.Abstract = type.IsAbstract;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00018224 File Offset: 0x00016424
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x0001822C File Offset: 0x0001642C
		internal Delegate Constructor
		{
			get
			{
				return this._constructor;
			}
			set
			{
				Interlocked.CompareExchange<Delegate>(ref this._constructor, value, null);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0001823C File Offset: 0x0001643C
		internal override Type ClrType
		{
			get
			{
				return Type.GetTypeFromHandle(this._type);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00018249 File Offset: 0x00016449
		internal string CSpaceTypeName
		{
			get
			{
				return this._cspaceTypeName;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00018251 File Offset: 0x00016451
		internal string CSpaceNamespaceName
		{
			get
			{
				return this._cspaceNamespaceName;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00018259 File Offset: 0x00016459
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

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001827C File Offset: 0x0001647C
		private string BuildEntityTypeHash()
		{
			string text = this.GetHashCode().ToString(CultureInfo.InvariantCulture);
			StringBuilder stringBuilder = new StringBuilder(text.Length * 2);
			string text2 = text;
			for (int i = 0; i < text2.Length; i++)
			{
				stringBuilder.Append(((byte)text2[i]).ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400082B RID: 2091
		private readonly RuntimeTypeHandle _type;

		// Token: 0x0400082C RID: 2092
		private Delegate _constructor;

		// Token: 0x0400082D RID: 2093
		private readonly string _cspaceTypeName;

		// Token: 0x0400082E RID: 2094
		private readonly string _cspaceNamespaceName;

		// Token: 0x0400082F RID: 2095
		private string _hash;
	}
}
